using Infatlan_STEI_Comunicacion.classes;
using System;
using System.Data;
using System.Web.UI;

namespace Infatlan_STEI_Comunicacion
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
    }
}