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
    public partial class articulos : System.Web.UI.Page
    {
        db vConexion = new db();
        protected void Page_Load(object sender, EventArgs e){
            Session["AUTH"] = true;
            Session["USUARIO"] = "wpadilla";
            if (!Page.IsPostBack){
                if (Convert.ToBoolean(Session["AUTH"])){
                    limpiarSessiones();
                    cargarDatos();
                }
            }
        }

        private void limpiarSessiones() { 
            
        }

        private void cargarDatos(){
            try{
                String vQuery = "[STEISP_INVENTARIO_Stock] 1";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0){
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                    Session["INV_STOCK"] = vDatos;
                }

                //TIPO STOCK
                vQuery = "STEISP_INVENTARIO_Generales 7";
                vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0){
                    DDLTipo.Items.Clear();
                    DDLTipo.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    foreach (DataRow item in vDatos.Rows){
                        DDLTipo.Items.Add(new ListItem { Value = item["idTipoStock"].ToString(), Text = item["nombre"].ToString() });
                    }
                }

                //PROVEEDOR
                vQuery = "[STEISP_INVENTARIO_Generales] 4";
                vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0){
                    DDLProveedor.Items.Clear();
                    DDLProveedor.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    foreach (DataRow item in vDatos.Rows){
                        DDLProveedor.Items.Add(new ListItem { Value = item["idProveedor"].ToString(), Text = item["nombre"].ToString() });
                    }
                }

                //MARCAS
                vQuery = "[STEISP_INVENTARIO_Generales] 5";
                vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0){
                    DDLMarca.Items.Clear();
                    DDLMarca.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    foreach (DataRow item in vDatos.Rows){
                        DDLMarca.Items.Add(new ListItem { Value = item["idMarca"].ToString(), Text = item["nombre"].ToString() });
                    }
                }

                //ESTADO
                vQuery = "[STEISP_INVENTARIO_Generales] 6";
                vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0){
                    DDLEstado.Items.Clear();
                    DDLEstado.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    foreach (DataRow item in vDatos.Rows){
                        DDLEstado.Items.Add(new ListItem { Value = item["idEstadoStock"].ToString(), Text = item["nombre"].ToString() });
                    }
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
                string vQuery = "";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void GVBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e){
            try{
                GVBusqueda.PageIndex = e.NewPageIndex;
                GVBusqueda.DataSource = (DataTable)Session["INV_STOCK"];
                GVBusqueda.DataBind();

            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void BtnNuevo_Click(object sender, EventArgs e){
            limpiarModal();
            LbIdArticulo.Text = "Crear Nuevo Artículo";
            Session["INV_STOCK_ID"] = null;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);
        }

        protected void BtnAceptar_Click(object sender, EventArgs e){
            try{
                validarDatos();
                String vQuery, vMensaje = "";
                DataTable vDatos = new DataTable();
                int vInfo;
                vQuery = "[STEISP_INVENTARIO_Stock] {0}" +
                        "," + DDLTipo.SelectedValue +
                        "," + DDLProveedor.SelectedValue +
                        ",'" + TxModelo.Text + "'" +
                        "," + DDLMarca.SelectedValue +
                        "," + TxCantidad.Text +
                        ",'" + TxDetalle.Text + "'" +
                        ",'" + TxSerie.Text + "'" +
                        ",'" + Session["USUARIO"].ToString() + "'" +
                        "," + DDLEstado.Text + "{1}";

                if (HttpContext.Current.Session["INV_STOCK_ID"] == null){
                    vQuery = string.Format(vQuery, "3","");
                    vInfo = vConexion.ejecutarSql(vQuery);
                    vMensaje = "Articulo registrado con éxito";
                }else{
                    vQuery = string.Format(vQuery, "4," + Session["INV_STOCK_ID"].ToString(), "," + DDLEstado.SelectedValue);
                    vInfo = vConexion.ejecutarSql(vQuery);
                    vMensaje = "Articulo actualizado con éxito";
                }

                if (vInfo == 1){
                    Mensaje(vMensaje, WarningType.Success);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "cerrarModal();", true);
                    cargarDatos();
                }
            }catch (Exception ex){
                LbAdvertencia.Text = ex.Message;
                DivMensaje.Visible = true;
            }
        }

        private void validarDatos() {
            if (DDLTipo.SelectedValue == "0")
                throw new Exception("Favor seleccione el tipo de producto.");
            if (DDLProveedor.SelectedValue == "0")
                throw new Exception("Favor seleccione el proveedor.");
            if (TxModelo.Text == "" || TxModelo.Text == string.Empty)
                throw new Exception("Favor ingrese el modelo del artículo.");
            if (DDLMarca.SelectedValue == "0")
                throw new Exception("Favor seleccione la marca.");
            if (TxCantidad.Text == "" || TxCantidad.Text == string.Empty)
                throw new Exception("Favor ingrese la cantidad de artículos.");
            if (TxSerie.Text == "" || TxSerie.Text == string.Empty)
                throw new Exception("Favor ingrese la serie del artículo.");
            if (DDLEstado.SelectedValue == "0")
                throw new Exception("Favor seleccione el estado del artículo.");
        }

        void limpiarModal(){
            DDLTipo.SelectedValue = "0";
            DDLProveedor.SelectedValue = "0";
            DDLMarca.SelectedValue = "0";
            DDLEstado.SelectedValue = "0";
            TxCantidad.Text = string.Empty;
            TxDetalle.Text = string.Empty;
            TxModelo.Text = string.Empty;
            TxSerie.Text = string.Empty;
            DivMensaje.Visible = false;
        }

        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e){
            try{
                DataTable vDatos = new DataTable();
                String vQuery = "";

                string vIdArticulo = e.CommandArgument.ToString();
                if (e.CommandName == "EditarArticulo"){
                    LbIdArticulo.Text = "Editar Articulo " + vIdArticulo;
                    Session["ACCION"] = "1";

                    vQuery = "[STEISP_INVENTARIO_Stock] 2," + vIdArticulo + "";
                    vDatos = vConexion.obtenerDataTable(vQuery);

                    for (int i = 0; i < vDatos.Rows.Count; i++){
                        DDLEstado.SelectedValue = vDatos.Rows[i]["idEstadoStock"].ToString();
                        DDLTipo.SelectedValue = vDatos.Rows[i]["idTipoStock"].ToString();
                        DDLProveedor.SelectedValue = vDatos.Rows[i]["idProveedor"].ToString();
                        DDLMarca.SelectedValue = vDatos.Rows[i]["idMarca"].ToString();
                        TxCantidad.Text = vDatos.Rows[i]["cantidad"].ToString();
                        TxModelo.Text = vDatos.Rows[i]["modelo"].ToString();
                        TxDetalle.Text = vDatos.Rows[i]["descripcion"].ToString();
                        TxSerie.Text = vDatos.Rows[i]["series"].ToString();
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }else if (e.CommandName == "EliminarArticulo"){
                    LbTitulo.Text = "Eliminar Articulo?";
                    LbMensaje.Text = "No podrá reversar los cambios.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ModalConfirmar();", true);
                }

            }catch (Exception Ex){
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        protected void BtnConfirmar_Click(object sender, EventArgs e)
        {

        }
    }
}