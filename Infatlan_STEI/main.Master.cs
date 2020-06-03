using System;
using System.Data;
using System.Web.UI;

namespace Infatlan_STEI
{
    public partial class main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Convert.ToBoolean(Session["AUTH"]))
            {
                Response.Redirect("/login.aspx");
            }

            if (!Page.IsPostBack)
            {
                String vError = "";
                try
                {
                    DataTable vDatos = (DataTable)Session["AUTHCLASS"];
                    LitUsuario.Text = vDatos.Rows[0]["nombre"].ToString().ToUpper() + " " + vDatos.Rows[0]["apellidos"].ToString().ToUpper();

                }
                catch (Exception ex)
                {
                    vError = ex.Message;
                }
            }
        }
    }
}