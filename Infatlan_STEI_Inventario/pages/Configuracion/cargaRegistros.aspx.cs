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
    public partial class cargaRegistros : System.Web.UI.Page
    {
        db vConexion = new db();
        protected void Page_Load(object sender, EventArgs e){
            Session["AUTH"] = true;
            Session["USUARIO"] = "wpadilla";
            if (!Page.IsPostBack){
                if (Convert.ToBoolean(Session["AUTH"])){
                    cargarDatos();
                }
            }
        }

        private void cargarDatos(){
            try{
                DataTable vDatos = new DataTable();
                vDatos.Columns.Add("id");
                vDatos.Columns.Add("proceso");

                vDatos.Rows.Add("1", "Equipos de Comunicacion");
                vDatos.Rows.Add("2", "Enlaces");
                vDatos.Rows.Add("3", "Contratos");
                vDatos.Rows.Add("4", "Proveedores");

                if (vDatos.Rows.Count > 0){
                    GvBusqueda.DataSource = vDatos;
                    GvBusqueda.DataBind();
                    Session["INV_CARGA"] = vDatos;
                }
                
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        public void Mensaje(string vMensaje, WarningType type){
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        protected void GvBusqueda_RowCommand(object sender, GridViewCommandEventArgs e){
            try{
                string vId = e.CommandArgument.ToString();
                if (e.CommandName == "DescargarPlantilla"){
                    if (vId == "1"){
                        //Response.ContentType = "application/excel";
                        //Response.ContentEncoding = System.Text.Encoding.UTF8;
                        //Response.AppendHeader("NombreCabecera", "MensajeCabecera");
                        //Response.TransmitFile("/sites/inventario/pages/plantillas/plantillaEDC.xlsx");
                        //Response.End();

                        //FileInfo file = new FileInfo("/sites/inventario/pages/plantillas/plantillaEDC.xlsx");

                        //Response.Clear();
                        //Response.ClearHeaders();
                        //Response.ClearContent();
                        //Response.ContentType = "text/plain";
                        //Response.Flush();
                        //Response.TransmitFile(file.FullName);
                        //Response.End();
                        
                        Response.Redirect("/sites/inventario/pages/plantillas/plantillaEDC.xlsx");
                    }else if (vId == "2")
                        Response.Redirect("");
                    else if (vId == "3")
                        Response.Redirect("");
                }else if (e.CommandName == "CargarRegistros"){
                    
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalCarga();", true);
                }
                Session["INV_STOCK_ID"] = vId;
            }catch (Exception Ex){
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        protected void BtnCargar_Click(object sender, EventArgs e){

        }
    }
}