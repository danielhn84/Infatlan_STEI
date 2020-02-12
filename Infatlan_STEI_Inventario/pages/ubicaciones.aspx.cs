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
    public partial class agencias : System.Web.UI.Page
    {
        db vConexion = new db();
        protected void Page_Load(object sender, EventArgs e){

            Session["AUTH"] = true;

            if (!Page.IsPostBack){
                if (Convert.ToBoolean(Session["AUTH"])){
                    //generales vGenerales = new generales();
                    //DataTable vDatos = (DataTable)Session["AUTHCLASS"];
                    //if (!vGenerales.PermisosPersonalSeguridad(vDatos))
                    //    Response.Redirect("/login.aspx");

                    cargarDatos();
                    Mensaje("hola mundo", WarningType.Info);
                }
            }
        }

        private void cargarDatos() {
            try{
                String vQuery = "[STEISP_INVENTARIO_Ubicacaiones] 1";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0){
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                    Session["SEG_SALIDAS"] = vDatos;
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

        protected void GVBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void BtnNuevo_Click(object sender, EventArgs e){
            limpiarModal();
            DivEstado.Visible = false;
            Session["ACCION"] = "2";
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);
        }

        protected void BtnAceptar_Click(object sender, EventArgs e){
            try{

            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        void limpiarModal() { 
        
        }
    }
}