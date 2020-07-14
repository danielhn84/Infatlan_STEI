using Infatlan_STEI.classes;
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

namespace Infatlan_STEI.paginas.reportes
{
    public partial class metasAprobacion : System.Web.UI.Page
    {
        db vConexion = new db();
        protected void Page_Load(object sender, EventArgs e){
            try{
                if (!Page.IsPostBack){
                    if (Convert.ToBoolean(Session["AUTH"])){
                        String vId = Session["CUMPL_ID_REPORTE"].ToString();
                        cargarDatos();
                    }else {
                        Response.Redirect("/login.aspx");
                    }
                }
            }catch (Exception ex){

            }
        }

        private void cargarDatos() { 
            
        }

        public void Mensaje(string vMensaje, WarningType type){
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        protected void GvRuptura_PageIndexChanging(object sender, GridViewPageEventArgs e){

        }

        protected void GvKPISolicitudes_PageIndexChanging(object sender, GridViewPageEventArgs e){

        }

        protected void GvOSER_PageIndexChanging(object sender, GridViewPageEventArgs e){

        }

        protected void BtnAprobar_Click(object sender, EventArgs e){
            try{
                String vQuery = "[STEISP_CUMPLIMIENTO_Reportes] 2" +
                    "," + Session["CUMPL_ID_REPORTE"].ToString() + ",2";
                int vInfo = vConexion.ejecutarSql(vQuery);
                if (vInfo == 1){
                    Response.Redirect("reportes/metasPendientes.aspx?ex=1");
                }
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);   
            }
        }
    }
}