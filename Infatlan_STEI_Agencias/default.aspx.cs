using Infatlan_STEI_Agencias.classes;
using System;
using System.Data;
using System.Web.UI;

namespace Infatlan_STEI_Agencias
{
    public partial class _default : System.Web.UI.Page
    {
        db vConexion = new db();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {

                    String vUsuario = Request.QueryString["u"];
                    String vQuery = "[STEISP_Login] 3, '" + vUsuario + "'";
                    DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                    if (vDatos.Rows.Count > 0)
                    {
                        if (vDatos.Rows[0]["auth"].ToString() != "1")
                        {
                            Response.Redirect("/login.aspx");
                        }
                        else
                        {
                            Session["AUTHCLASS"] = vDatos;
                            Session["USUARIO"] = vDatos.Rows[0]["idUsuario"].ToString();
                            Session["AUTH"] = true;
                            Contar();
                        }
                    }
                    else
                    {
                        Response.Redirect("/login.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
                String vError = ex.Message;
            }
        }

        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        void Contar()
        {
            try
            {
                String vQuery = "STEISP_Agencia_Conteo 1";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                H2Agencias.InnerText = vDatos.Rows[0]["Contar"].ToString();

                String vQuery2 = "STEISP_Agencia_Conteo 2, '" + Session["USUARIO"].ToString() + "'";
                DataTable vDatos2 = vConexion.obtenerDataTable(vQuery2);
                H2Asignados.InnerText = vDatos2.Rows[0]["Contar"].ToString();

                String vQuery3 = "STEISP_Agencia_Conteo 3, '" + Session["USUARIO"].ToString() + "'";
                DataTable vDatos3 = vConexion.obtenerDataTable(vQuery3);
                H2Finalizados.InnerText = vDatos3.Rows[0]["Contar"].ToString();

                DataTable vDatos4 = new DataTable();
                vDatos4 = vConexion.obtenerDataTable("STEISP_Agencia_Conteo 4, '" + Session["USUARIO"].ToString() + "'");
                GVMantenimiento.DataSource = vDatos4;
                GVMantenimiento.DataBind();


            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }
    }
}