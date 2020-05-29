using System;

namespace Infatlan_STEI
{
    public partial class logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("/login.aspx");
        }
    }
}