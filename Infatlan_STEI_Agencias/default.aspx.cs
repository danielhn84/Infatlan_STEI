using Infatlan_STEI_Agencias.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Infatlan_STEI_Agencias
{
    public partial class _default : System.Web.UI.Page
    {
        db vConexion = new db();



        protected void Page_Load(object sender, EventArgs e)
        {
            Session["USUARIO"] = "acamador";
        }
    }
}