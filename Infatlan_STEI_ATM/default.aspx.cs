using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using Infatlan_STEI_ATM.clases;

namespace Infatlan_STEI_ATM
{
    public partial class @default : System.Web.UI.Page
    {
        bd vConexion = new bd();
        protected void Page_Load(object sender, EventArgs e){
            if (!Page.IsPostBack){
                if (Convert.ToBoolean(Session["AUTH"])){
                    Contar();
                }else {
                    Response.Redirect("/login.aspx");
                }
            }
        }

        void Contar(){
            try{
                String vQuery = "STEISP_ATM_ConteosDefault 1";
                DataTable vDatos = vConexion.ObtenerTabla(vQuery);
                h2ATMDisp.InnerText = vDatos.Rows[0]["Contar"].ToString();
           
                String vQuery2 = "STEISP_ATM_ConteosDefault 2, '"+ Session["USUARIO"].ToString() + "'";
                DataTable vDatos2 = vConexion.ObtenerTabla(vQuery2);
                H2MantAsignados.InnerText = vDatos2.Rows[0]["Contar"].ToString();

                String vQuery3 = "STEISP_ATM_ConteosDefault 3, '" + Session["USUARIO"].ToString() + "'";
                DataTable vDatos3 = vConexion.ObtenerTabla(vQuery3);
                H2MantRealizado.InnerText = vDatos3.Rows[0]["Contar"].ToString();

                DataTable vDatos4 = new DataTable();
                vDatos4 = vConexion.ObtenerTabla("STEISP_ATM_ConteosDefault 4, '" + Session["USUARIO"].ToString() + "'");
                GVMantenimiento.DataSource = vDatos4;
                GVMantenimiento.DataBind();
                Session["ATM_DEFAULT_MANTREALIZADO"] = vDatos4;
            }
            catch (Exception Ex)
            {
                //Mensaje(ex.Message, WarningType.Danger);
            }
        }
    }
}