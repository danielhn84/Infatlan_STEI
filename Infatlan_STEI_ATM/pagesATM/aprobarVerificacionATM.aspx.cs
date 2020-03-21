using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using Infatlan_STEI_ATM.clases;

namespace Infatlan_STEI_ATM.pagesATM
{
    public partial class aprobarVerificacionATM : System.Web.UI.Page
    {
        bd vConexion = new bd();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ControlImagenes();
                llenarForm();
            }
        }

        void llenarForm()
        {
           // Session["ATM_CODVERIF"].ToString(); 
           txtnomATM.Text= Session["ATM_NOMBREVERIF"].ToString();
           txtdireccion.Text= Session["ATM_DIRECCIONVERIF"].ToString();
           txtip.Text= Session["ATM_IPVERIF"].ToString();
           txtUbicacionATM.Text= Session["ATM_UBICACIONVERIF"].ToString();
           txtsucursal.Text= Session["ATM_SUCURSALVERIF"].ToString();
           txtzonaVerif.Text= Session["ATM_ZONAVERIF"].ToString();
           // Session["ATM_FECHAMANTVERIF"].ToString();
           TxFechaInicio.Text= Session["ATM_HRINICIOVERIF"].ToString();
           TxFechaRegreso.Text= Session["ATM_HRFINVERIF"].ToString();
           // Session["ATM_AUTORIZADOVERIF"].ToString();
            //Session["ATM_CANCELARVERIF"].ToString();
           txtsysaid.Text= Session["ATM_SYSAIDVERIF"].ToString();
           txtTecnicoResponsable.Text= Session["ATM_TECNICOVERIF"].ToString();
           // Session["ATM_USUARIOVERIF"].ToString();
           txtidentidad.Text= Session["ATM_IDENTIDADVERIF"].ToString();
           txtcodATM.Text= Session["ATM_CODATMVERIF"].ToString();
           txtobseracionesVerif.Text= Session["ATM_OBSERVACIONESVERIF"].ToString();
           txthsalidaInfa.Text= Session["ATM_HRSALIDAINFAVERIF"].ToString();
           txtHllegadaInfatlan.Text= Session["ATM_HRENTRADAINFAVERIF"].ToString();
           // Session["ATM_IDPREGUNTAVERIF"].ToString();
           // Session["ATM_RESPUESTAVERIF"].ToString();
           // Session["ATM_IMGVERIF"].ToString();
           //txtantiSkimming.Text= Session["ATM_COMENTARIOPREGUNTASVERIF"].ToString();
        }
        void ControlImagenes()
        {
            RBLDiscoDuro.SelectedValue = "1";
            RBLATMDesarmadoPS.SelectedValue = "1";
            RBLATMDesarmadoPI.SelectedValue = "1";
            RBLVendor.SelectedValue = "1";
            RBLSystemInfo.SelectedValue = "1";
            RBLAntiSkimming.SelectedValue = "1";
            RBLMonitorFiltro.SelectedValue = "1";
            RBLPadleWheel.SelectedValue = "1";
            RBLDispDesarmado.SelectedValue = "1";
            RBLTeclado.SelectedValue = "1";

        }
        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }
        protected void btnEnviarVerif_Click(object sender, EventArgs e)
        {
            lbcodATM.Text = txtcodATM.Text;
            lbNombreATM.Text = txtnomATM.Text;
            lbsucursalATM.Text = txtsucursal.Text;
            lbInventarioATM.Text = txtinventarioVerif.Text;
            lbtecnico.Text = txtTecnicoResponsable.Text;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);
        }

        protected void btnModalVerif_Click(object sender, EventArgs e)
        {

        }

        protected void btnModalCerrarVerif_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
        }
    }
}