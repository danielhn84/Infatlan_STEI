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

namespace Infatlan_STEI_Inventario.pages
{
    public partial class proveedores : System.Web.UI.Page
    {
        db vConexion = new db();
        protected void Page_Load(object sender, EventArgs e){
            if (!Page.IsPostBack){
                if (Convert.ToBoolean(Session["AUTH"])){
                    cargarDatos();
                }else {
                    Response.Redirect("/login.aspx");
                }
            }
        }

        private void cargarDatos() {
            try{
                String vQuery = "[STEISP_INVENTARIO_Proveedores] 1";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0){
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                    Session["INV_PROVEEDORES"] = vDatos;
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
                DataTable vDatos = (DataTable)Session["INV_PROVEEDORES"];
                if (vBusqueda.Equals("")){
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                }else{ 
                    EnumerableRowCollection<DataRow> filtered = vDatos.AsEnumerable()
                        .Where(r => r.Field<String>("nombre").Contains(vBusqueda.ToUpper()));

                    Boolean isNumeric = int.TryParse(vBusqueda, out int n);

                    if (isNumeric){
                        if (filtered.Count() == 0){
                            filtered = vDatos.AsEnumerable().Where(r =>
                                Convert.ToInt32(r["idProveedor"]) == Convert.ToInt32(vBusqueda));
                        }
                    }

                    DataTable vDatosFiltrados = new DataTable();
                    vDatosFiltrados.Columns.Add("idProveedor");
                    vDatosFiltrados.Columns.Add("nombre");
                    vDatosFiltrados.Columns.Add("direccion");
                    vDatosFiltrados.Columns.Add("telefono");
                    vDatosFiltrados.Columns.Add("responsable");
                    vDatosFiltrados.Columns.Add("estado");

                    foreach (DataRow item in filtered){
                        vDatosFiltrados.Rows.Add(
                            item["idProveedor"].ToString(),
                            item["nombre"].ToString(),
                            item["direccion"].ToString(),
                            item["telefono"].ToString(),
                            item["responsable"].ToString(),
                            item["estado"].ToString()
                            );
                    }

                    GVBusqueda.DataSource = vDatosFiltrados;
                    GVBusqueda.DataBind();
                    Session["INV_PROVEEDORES"] = vDatosFiltrados;
                }

            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void GVBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e){
            try{
                GVBusqueda.PageIndex = e.NewPageIndex;
                GVBusqueda.DataSource = (DataTable)Session["INV_PROVEEDORES"];
                GVBusqueda.DataBind();

            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void BtnNuevo_Click(object sender, EventArgs e){
            try{
                limpiarModal();
                LbIdProveedor.Text = "Crear Nuevo Proveedor";
                DivEstado.Visible = false;
                Session["INV_PROV_ID"] = null;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void BtnAceptar_Click(object sender, EventArgs e){
            try{
                validarDatos();
                String vMensaje = "";
                int vInfo;
                DataTable vDatos = new DataTable();
                String vQuery = "STEISP_INVENTARIO_Proveedores {0}" +
                                ",'" + TxNombre.Text.ToUpper() + "'" +
                                ",'" + TxDireccion.Text + "'" +
                                ",'" + TxTelefono.Text + "'" +
                                ",'" + TxResponsable.Text + "'{1}";

                if (HttpContext.Current.Session["INV_PROV_ID"] == null){
                    vQuery = string.Format(vQuery, "3","");
                    vInfo = vConexion.ejecutarSql(vQuery);
                    vMensaje = "Proveedor registrado con éxito";
                }else{
                    vQuery = string.Format(vQuery, "4," + Session["INV_PROV_ID"].ToString(), "," + DDLEstado.SelectedValue);
                    vInfo = vConexion.ejecutarSql(vQuery);
                    vMensaje = "Proveedor actualizado con éxito";
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
                //Mensaje(ex.Message, WarningType.Danger);
            }
        }

        private void validarDatos(){
            if (TxNombre.Text == "" || TxNombre.Text == string.Empty)
                throw new Exception("Favor ingrese el nombre del proveedor.");
            if (TxDireccion.Text == "" || TxDireccion.Text == string.Empty)
                throw new Exception("Favor ingrese la dirección del proveedor.");
            if (TxTelefono.Text == "" || TxTelefono.Text == string.Empty)
                throw new Exception("Favor ingrese el teléfono del proveedor.");
            if (TxResponsable.Text == "" || TxResponsable.Text == string.Empty)
                throw new Exception("Favor ingrese la persona responsable.");
        }

        void limpiarModal(){
            TxNombre.Text = string.Empty;
            TxDireccion.Text = string.Empty;
            TxTelefono.Text = string.Empty;
            TxResponsable.Text = string.Empty;
            DivMensaje.Visible = false;
        }

        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e){
            try{
                DataTable vDatos = new DataTable();
                String vQuery = "";
                string vIdProveedor = e.CommandArgument.ToString();
                
                if (e.CommandName == "EditarProveedor"){
                    DivMensaje.Visible = false;
                    LbIdProveedor.Text = "Editar Articulo " + vIdProveedor;
                    Session["INV_PROV_ID"] = vIdProveedor;
                    DivEstado.Visible = true;
                    vQuery = "[STEISP_INVENTARIO_Proveedores] 2," + vIdProveedor + "";
                    vDatos = vConexion.obtenerDataTable(vQuery);

                    for (int i = 0; i < vDatos.Rows.Count; i++){
                        TxNombre.Text = vDatos.Rows[i]["nombre"].ToString();
                        TxDireccion.Text = vDatos.Rows[i]["direccion"].ToString();
                        TxTelefono.Text = vDatos.Rows[i]["telefono"].ToString();
                        TxResponsable.Text = vDatos.Rows[i]["responsable"].ToString();
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }else if (e.CommandName == "EliminarProveedor"){
                    LbTitulo.Text = "Eliminar Articulo?";
                    LbMensaje.Text = "No podrá reversar los cambios.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ModalConfirmar();", true);
                }
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void BtnConfirmar_Click(object sender, EventArgs e){
            try{

            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
    }
}