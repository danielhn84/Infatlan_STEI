using Infatlan_STEI_Inventario.clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace Infatlan_STEI_Inventario.pages
{
    public partial class inventarios : System.Web.UI.Page
    {
        db vConexion = new db();
        protected void Page_Load(object sender, EventArgs e){
            String vIdUbicacion = " " + Request.QueryString["i"];
            LbUbicacion.Text = " " + Request.QueryString["c"];
            Session["AUTH"] = true;
            Session["USUARIO"] = "wpadilla";
            DDLNueva.CssClass = "select2 form-control custom-select";

            if (!Page.IsPostBack){
                if (Convert.ToBoolean(Session["AUTH"])){
                    limpiarSessiones();
                    cargarDatos(vIdUbicacion);
                }
            }
        }

        private void limpiarSessiones() { 
            
        }

        private void cargarDatos(String vId){
            try{

                String vQuery = "[STEISP_INVENTARIO_Principal] 5," + vId;
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                GVBusqueda.DataSource = vDatos;
                GVBusqueda.DataBind();
                Session["INV_UBIC_ARTICULO"] = vDatos;

                //UBICACIONES
                vQuery = "[STEISP_INVENTARIO_Ubicaciones] 1";
                vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0){
                    DDLNueva.Items.Clear();
                    DDLNueva.Items.Add(new ListItem { Value = "0", Text = "Seleccione" });
                    foreach (DataRow item in vDatos.Rows){
                        DDLNueva.Items.Add(new ListItem { Value = item["idUbicacion"].ToString(), Text = item["codigo"].ToString() });
                    }
                }
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        public void Mensaje(string vMensaje, WarningType type){
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        private void limpiarModalUbic(){

        }

        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e){
            try{
                DataTable vDatos = new DataTable();
                String vQuery = "";

                string vIdInventario = e.CommandArgument.ToString();
                if (e.CommandName == "MoverArticulo"){
                    DivMensaje.Visible = false;
                    
                    //UBICACION ACTUAL
                    vQuery = "[STEISP_INVENTARIO_Ubicaciones] 5, " + vIdInventario;
                    vDatos = vConexion.obtenerDataTable(vQuery);

                    if (vDatos.Rows.Count > 0){
                        TxActual.Text = vDatos.Rows[0]["codigo"].ToString();
                        TxIdInventario.Text = vIdInventario;
                        TxIdUbicacion.Text = vDatos.Rows[0]["idUbicacion"].ToString();
                        TxIdStock.Text = vDatos.Rows[0]["idStock"].ToString();
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
            }catch (Exception Ex){
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        protected void BtnAceptar_Click(object sender, EventArgs e){
            try{
                generarxml vMaestro = new generarxml();
                Object[] vDatosMaestro = new object[10];
                vDatosMaestro[0] = "";
                vDatosMaestro[1] = TxIdStock.Text;
                vDatosMaestro[2] = TxIdUbicacion.Text;
                vDatosMaestro[3] = ""; //Responsable
                vDatosMaestro[4] = "";
                vDatosMaestro[5] = "";
                vDatosMaestro[6] = ""; // Serie
                vDatosMaestro[7] = "";
                vDatosMaestro[8] = Session["USUARIO"].ToString();
                vDatosMaestro[9] = 5;
                String vXML = vMaestro.ObtenerMaestroString(vDatosMaestro);
                vXML = vXML.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");

                String vQuery = "[STEISP_INVENTARIO_Principal] 3" +
                    "," + TxIdInventario.Text +
                    "," + DDLNueva.SelectedValue + 
                    ",'" + vXML + "'";
                Int32 vInfo = vConexion.ejecutarSql(vQuery);
                if (vInfo == 2){
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "cerrarModal();", true);
                    Mensaje("Cambio realizado con éxito.", WarningType.Success);
                    cargarDatos(TxIdUbicacion.Text);
                }

            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void GVBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e){
            try{
                GVBusqueda.PageIndex = e.NewPageIndex;
                GVBusqueda.DataSource = (DataTable)Session["INV_UBIC_ARTICULO"];
                GVBusqueda.DataBind();
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
    }
}