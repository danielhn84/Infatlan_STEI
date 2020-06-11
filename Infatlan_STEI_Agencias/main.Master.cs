using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

namespace Infatlan_STEI
{
    public partial class main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e){
            DataTable vDatos = (DataTable)Session["AUTHCLASS"];
            LitUsuario.Text = vDatos.Rows[0]["nombre"].ToString().ToUpper() + " " + vDatos.Rows[0]["apellidos"].ToString().ToUpper();
        }
    }
}