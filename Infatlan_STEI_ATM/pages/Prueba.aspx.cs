using Infatlan_STEI_ATM.clases;
using System;
using System.Web.UI;

namespace Infatlan_STEI_ATM.pagesATM
{
    public partial class Prueba : System.Web.UI.Page
    {
        bd vConexion = new bd();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Convert.ToBoolean(Session["AUTH"]))
                {

                }
                else
                {
                    Response.Redirect("/login.aspx");
                }
            }
        }
    }
}