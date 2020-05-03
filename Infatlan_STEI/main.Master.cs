using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Infatlan_STEI
{
    public partial class main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Unnamed_Click(object sender, EventArgs e){
            try
            {
                Response.Redirect("localhost:44344/Infatlan_STEI_Inventario/default.aspx");

            }
            catch (Exception ex)
            {
                String error = ex.Message;
            }
        }
    }
}