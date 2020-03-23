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
        //public Image LoadImage()
        //{
        //    ////////////////////CONVERTIR IMAGENES///////////////////
        //    byte[] bytes = Convert.FromBase64String(Session["ATM_VERIF_IMG21"].ToString());
        //    Image image;
        //    using (MemoryStream ms = new MemoryStream(bytes))
        //    {
        //        image = Image.FromStream(ms);
        //    }
        //    return image
        //    ////////////////////CONVERTIR IMAGENES///////////////////
        //}
        void llenarForm()
        {
            byte[] bytes = Convert.FromBase64String("R0lGODlhAQABAIAAAAAAAAAAACH5BAAAAAAALAAAAAABAAEAAAICTAEAOw==");

            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }
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
            

            //Session["ATM_VERIF_IMG22"]
            txtpuertoVerif.Text= Session["ATM_PUERTOVERIF"].ToString();
           txtSerieDiscoDuro.Text= Session["ATM_SERIEDISCOVERIF"].ToString();
           txtcapacidadDiscoVerif.Text= Session["ATM_CAPACIDADDISCODUROVERIF"].ToString();
           txtserieATM.Text= Session["ATM_SERIEATMVERIF"].ToString();
           txtinventarioVerif.Text= Session["ATM_INVENTARIOVERIF"].ToString();
           txtramVerif.Text= Session["ATM_RAMVERIF"].ToString();
           txtlongitudATM.Text= Session["ATM_LONGITUDVERIF"].ToString();
           txtlatitudATM.Text= Session["ATM_LATITUDVERIF"].ToString();
           txtsoVerif.Text= Session["ATM_SOVERIF"].ToString();
           txtversionswVerif.Text= Session["ATM_VERSIONVERIF"].ToString();
           txtTecladoVerif.Text= Session["ATM_TECLADOVERIF"].ToString();
           txtTipoProcesadorVerif.Text= Session["ATM_PROCESADORVERIF"].ToString();
           txtTipoCargaVerif.Text= Session["ATM_TIPOCARGAVERIF"].ToString();
           txtmarcaVerif.Text= Session["ATM_MARCAVERIF"].ToString();

            if (Session["ATM_VERIF_PREG1"].ToString() == "Si")
                ckpasos1.SelectedValue = "1";
            if (Session["ATM_VERIF_PREG2"].ToString() == "Si")
                ckpasos2.SelectedValue = "2";
            if (Session["ATM_VERIF_PREG3"].ToString() == "Si")
                ckpasos3.SelectedValue = "3";
            if (Session["ATM_VERIF_PREG4"].ToString() == "Si")
                ckpasos4.SelectedValue = "4";
            if (Session["ATM_VERIF_PREG5"].ToString() == "Si")
                ckpasos5.SelectedValue = "5";
            if (Session["ATM_VERIF_PREG6"].ToString() == "Si")
                ckpasos6.SelectedValue = "6";
            if (Session["ATM_VERIF_PREG7"].ToString() == "Si")
                ckpasos7.SelectedValue = "7";
            if (Session["ATM_VERIF_PREG8"].ToString() == "Si")
                ckpasos8.SelectedValue = "8";
            if (Session["ATM_VERIF_PREG9"].ToString() == "Si")
                ckpasos9.SelectedValue = "9";
            if (Session["ATM_VERIF_PREG10"].ToString() == "Si")
                ckpasos10.SelectedValue = "10";
            if (Session["ATM_VERIF_PREG11"].ToString() == "Si")
                ckpasos11.SelectedValue = "11";
            if (Session["ATM_VERIF_PREG12"].ToString() == "Si")
                ckpasos12.SelectedValue = "12";
            if (Session["ATM_VERIF_PREG13"].ToString() == "Si")
                ckpasos13.SelectedValue = "13";
            if (Session["ATM_VERIF_PREG14"].ToString() == "Si")
                ckpasos14.SelectedValue = "14";
            if (Session["ATM_VERIF_PREG15"].ToString() == "Si")
                ckpasos15.SelectedValue = "15";
            if (Session["ATM_VERIF_PREG16"].ToString() == "Si")
                ckpasos16.SelectedValue = "16";
            if (Session["ATM_VERIF_PREG17"].ToString() == "Si")
                ckpasos17.SelectedValue = "17";
            if (Session["ATM_VERIF_PREG18"].ToString() == "Si")
                ckpasos18.SelectedValue = "18";
            if (Session["ATM_VERIF_PREG19"].ToString() == "Si")
                ckpasos19.SelectedValue = "19";
            if (Session["ATM_VERIF_PREG20"].ToString() == "Si")
                ckpasos20.SelectedValue = "20";
            if (Session["ATM_VERIF_PREG21"].ToString() == "Si")
                RBLClimatizacion.SelectedValue = "1";
            else
                RBLClimatizacion.SelectedValue = "2";
            if (Session["ATM_VERIF_PREG22"].ToString() == "Si")
                RBLEnergiaElectrica.SelectedValue = "1";
            else
                RBLEnergiaElectrica.SelectedValue = "2";
            txtPreguntaAntiskimming.Text = Session["ATM_VERIF_PREG23"].ToString();
            txtantiSkimming.Text = Session["ATM_VERIF_RESP23"].ToString();
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