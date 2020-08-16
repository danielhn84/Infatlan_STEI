using Infatlan_STEI_Inventario.clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace Infatlan_STEI_Inventario.pages.Configuracion
{
    public partial class tipoArticulo : System.Web.UI.Page
    {
        Security vSecurity = new Security();
        db vConexion = new db();
        protected void Page_Load(object sender, EventArgs e){
            if (!Page.IsPostBack){
                if (Convert.ToBoolean(Session["AUTH"])){
                    if (vSecurity.ObtenerPermiso(Session["USUARIO"].ToString(), 1).Creacion)
                        BtnNuevo.Visible = true;

                    cargarDatos();
                }else {
                    Response.Redirect("/login.aspx");
                }
            }
        }

        private void cargarDatos() {
            try{
                String vQuery = "";

                if (DDLProceso.SelectedValue == "0")
                    vQuery = "[STEISP_INVENTARIO_TipoArticulos] 1";
                else if (DDLProceso.SelectedValue == "1")
                    vQuery = "[STEISP_INVENTARIO_TipoArticulos] 5, 'False'";
                else if (DDLProceso.SelectedValue == "2")
                    vQuery = "[STEISP_INVENTARIO_TipoArticulos] 5, 'True'";

                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                if (vDatos.Rows.Count > 0){
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                    if (vSecurity.ObtenerPermiso(Session["USUARIO"].ToString(), 1).Edicion){
                        foreach (GridViewRow item in GVBusqueda.Rows){
                            LinkButton LbEdit = item.FindControl("BtnEditar") as LinkButton;
                            LbEdit.Visible = true;
                        }
                    }
                    Session["INV_TIPO_ARTICULO"] = vDatos;
                }
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        public void Mensaje(string vMensaje, WarningType type){
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        protected void TxBusqueda_TextChanged(object sender, EventArgs e){
            try{
                cargarDatos();
                String vBusqueda = TxBusqueda.Text;
                DataTable vDatos = (DataTable)Session["INV_TIPO_ARTICULO"];
                if (vBusqueda.Equals("")){
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                }else{ 
                    EnumerableRowCollection<DataRow> filtered = vDatos.AsEnumerable().Where(r => r.Field<String>("nombre").Contains(vBusqueda.ToUpper()));

                    Boolean isNumeric = int.TryParse(vBusqueda, out int n);

                    if (isNumeric){
                        if (filtered.Count() == 0){
                            filtered = vDatos.AsEnumerable().Where(r =>
                                Convert.ToInt32(r["idTipoStock"]) == Convert.ToInt32(vBusqueda));
                        }
                    }

                    DataTable vDatosFiltrados = new DataTable();
                    vDatosFiltrados.Columns.Add("idTipoStock");
                    vDatosFiltrados.Columns.Add("nombre");
                    vDatosFiltrados.Columns.Add("descripcion");
                    vDatosFiltrados.Columns.Add("estadoDesc");
                    vDatosFiltrados.Columns.Add("edc");

                    foreach (DataRow item in filtered){
                        vDatosFiltrados.Rows.Add(
                            item["idTipoStock"].ToString(),
                            item["nombre"].ToString(),
                            item["descripcion"].ToString(),
                            item["estadoDesc"].ToString(),
                            item["edc"].ToString()
                            );
                    }

                    GVBusqueda.DataSource = vDatosFiltrados;
                    GVBusqueda.DataBind();
                    Session["INV_TIPO_ARTICULO"] = vDatosFiltrados;
                }
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void BtnNuevo_Click(object sender, EventArgs e){
            try{
                limpiarModal();
                LbIdTA.Text = "Crear Nuevo Tipo de Articulo";
                DivEstado.Visible = false;
                Session["INV_IDTA"] = null;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e){
            try{
                DataTable vDatos = new DataTable();
                String vQuery = "";
                string vIdTA = e.CommandArgument.ToString();
                
                if (e.CommandName == "EditarArticulo"){
                    DivMensaje.Visible = false;
                    LbIdTA.Text = "Editar Tipo de Articulo " + vIdTA;
                    Session["INV_IDTA"] = vIdTA;
                    DivEstado.Visible = true;
                    vQuery = "[STEISP_INVENTARIO_TipoArticulos] 2," + vIdTA + "";
                    vDatos = vConexion.obtenerDataTable(vQuery);

                    for (int i = 0; i < vDatos.Rows.Count; i++){
                        TxNombre.Text = vDatos.Rows[i]["nombre"].ToString();
                        TxDescripcion.Text = vDatos.Rows[i]["descripcion"].ToString();
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void GVBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e){
            try{
                GVBusqueda.PageIndex = e.NewPageIndex;
                GVBusqueda.DataSource = (DataTable)Session["INV_TIPO_ARTICULO"];
                GVBusqueda.DataBind();

            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void BtnAceptar_Click(object sender, EventArgs e){
            try{
                validarDatos();
                String vQuery = "", vMensaje = "";
                int vInfo;
                DataTable vDatos = new DataTable();
                vQuery = "[STEISP_INVENTARIO_TipoArticulos] {0}" +
                        ",'" + TxNombre.Text.ToUpper() + "'" +
                        ",'" + TxDescripcion.Text + "'" +
                        "," + DDLEstado.SelectedValue +
                        "," + DDLArticulosEDC.SelectedValue + "{1}";

                if (HttpContext.Current.Session["INV_IDTA"] == null){
                    vQuery = string.Format(vQuery, "3" , "");
                    vInfo = vConexion.ejecutarSql(vQuery);
                    vMensaje = "Tipo de artículo registrado con éxito";
                }else{
                    vQuery = string.Format(vQuery, "4", "," + Session["INV_IDTA"].ToString());
                    vInfo = vConexion.ejecutarSql(vQuery);
                    vMensaje = "Tipo de artículo actualizado con éxito";
                }

                if (vInfo == 1){
                    Mensaje(vMensaje, WarningType.Success);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "cerrarModal();", true);
                    cargarDatos();
                }
            }
            catch (Exception ex){
                LbAdvertencia.Text = ex.Message;
                DivMensaje.Visible = true;
            }
        }

        void limpiarModal(){
            TxNombre.Text = string.Empty;
            TxDescripcion.Text = string.Empty;
            DivMensaje.Visible = false;
            DDLArticulosEDC.SelectedValue = "0";
        }

        private void validarDatos(){
            if (TxNombre.Text == "" || TxNombre.Text == string.Empty)
                throw new Exception("Favor ingrese el nombre.");
            if (TxDescripcion.Text == "" || TxDescripcion.Text == string.Empty)
                throw new Exception("Favor ingrese la descripción.");
        }

        protected void DDLProceso_SelectedIndexChanged(object sender, EventArgs e){
            try{
                cargarDatos();
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
    }
}