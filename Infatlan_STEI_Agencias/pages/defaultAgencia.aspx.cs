using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Infatlan_STEI_Agencias.pages
{
    public partial class defaulAgencia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void dropzonaATM_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropzonaATM.SelectedValue == "1")
            {
                txtprueba.Enabled = true;
            }else
            {
                txtprueba.Enabled = false;
            }
        }
    }
}