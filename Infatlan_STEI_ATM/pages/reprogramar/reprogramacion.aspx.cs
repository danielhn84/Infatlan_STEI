using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Infatlan_STEI_ATM.pages.reprogramar
{
    public partial class reprogramacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e){
            if (!Page.IsPostBack){
                if (Convert.ToBoolean(Session["AUTH"])){
                    LimpiarReprogramacion();
                }else {
                    Response.Redirect("/login.aspx");
                }
            }
        }

        void LimpiarReprogramacion()
        {
            dllmantPendientesReprogramado.SelectedValue = "0";
            txtzonaReprogramar.Text = string.Empty;
            txtdepReprogramar.Text = string.Empty;
            txtfechaMantReprogramado.Text = string.Empty;
            txtipReprogramado.Text = string.Empty;
            txtlugarReprogramar.Text = string.Empty;
            txtcodATMReprogramar.Text = string.Empty;
            txtubicacionATMReprogramar.Text = string.Empty;
            txtdireccionMReprogramar.Text = string.Empty;
            txtmotivoReprogramado.Text = string.Empty;
            txtdetMotivoReprogramado.Text = string.Empty;
            txtpersonaReprograma.Text = string.Empty;
            txtfechaNuevaReprogramacion.Text = string.Empty;
        }

        protected void btnEnviarReprogramacion_Click(object sender, EventArgs e)
        {
            LimpiarReprogramacion();
        }
    }
}