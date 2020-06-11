using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Infatlan_STEI_Inventario.clases;

namespace Infatlan_STEI_Inventario
{
    public partial class main : System.Web.UI.MasterPage
    {

        protected void Page_Load(object sender, EventArgs e){
            try{
                DataTable vDatos = (DataTable)Session["AUTHCLASS"];
                LitUsuario.Text = vDatos.Rows[0]["nombre"].ToString().ToUpper() + " " + vDatos.Rows[0]["apellidos"].ToString().ToUpper();
            }catch (Exception ex){

            }
        }
    }
}