using Infatlan_STEI_ATM.clases;
using System;
using System.Web.UI;


namespace Infatlan_STEI_ATM.pages.mantenimiento
{
    public partial class aprobarVerificacion : System.Web.UI.Page
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

        //public Image LoadImage()
        //{

        //    //////////////////CONVERTIR IMAGENES///////////////////


        //    //MemoryStream ms = new MemoryStream();
        //    //byte[] bytes = Convert.FromBase64String(Session["ATM_VERIF_IMG21"].ToString());

        //    //MemoryStream ms = new MemoryStream(bytes);
        //    //imgDisco.ImageUrl = Image.FromStream(ms);

        //    //using (MemoryStream ms = new MemoryStream(bytes))
        //    //{
        //    //    imgDisco.ImageUrl = Image.FromStream(ms);
        //    //}

        //    //////////////////CONVERTIR IMAGENES///////////////////

        //}
        void llenarForm()
        {
            //IMAGEN1
            string vImagen1 = Session["ATM_VERIF_IMG1"].ToString();
            string srcImgen1 = "data:image;base64," + vImagen1;
            imgDiscoDuro.Src = srcImgen1;
            //IMAGEN2
            string vImagen2 = Session["ATM_VERIF_IMG2"].ToString();
            string srcImgen2 = "data:image;base64," + vImagen2;
            imgATMDesarmadoPS.Src = srcImgen2;
            //IMAGEN3
            string vImagen3 = Session["ATM_VERIF_IMG3"].ToString();
            string srcImgen3 = "data:image;base64," + vImagen3;
            imgATMDesarmadoPI.Src = srcImgen3;
            //IMAGEN4
            string vImagen4 = Session["ATM_VERIF_IMG4"].ToString();
            string srcImgen4 = "data:image;base64," + vImagen4;
            imgDispositivoVendor.Src = srcImgen4;
            //IMAGEN5
            string vImagen5 = Session["ATM_VERIF_IMG5"].ToString();
            string srcImgen5 = "data:image;base64," + vImagen5;
            imgSYSTEMINFO.Src = srcImgen5;
            //IMAGEN6
            string vImagen6 = Session["ATM_VERIF_IMG6"].ToString();
            string srcImgen6 = "data:image;base64," + vImagen6;
            imgAntiskimmin.Src = srcImgen6;
            //IMAGEN7
            string vImagen7 = Session["ATM_VERIF_IMG7"].ToString();
            string srcImgen7 = "data:image;base64," + vImagen7;
            imgMonitorFiltro.Src = srcImgen7;
            //IMAGEN8
            string vImagen8 = Session["ATM_VERIF_IMG8"].ToString();
            string srcImgen8 = "data:image;base64," + vImagen8;
            imgPadlewheel.Src = srcImgen8;
            //IMAGEN9
            string vImagen9 = Session["ATM_VERIF_IMG9"].ToString();
            string srcImgen9 = "data:image;base64," + vImagen9;
            imgDispDesarmado.Src = srcImgen9;
            //IMAGEN10
            string vImagen10 = Session["ATM_VERIF_IMG10"].ToString();
            string srcImgen10 = "data:image;base64," + vImagen10;
            imgTeclado.Src = srcImgen10;
            //IMAGEN11
            string vImagen11 = Session["ATM_VERIF_IMG21"].ToString();
            string srcImgen11 = "data:image;base64," + vImagen11;
            imgClimatizacion.Src = srcImgen11;
            //IMAGEN12
            string vImagen12 = Session["ATM_VERIF_IMG22"].ToString();
            string srcImgen12 = "data:image;base64," + vImagen12;
            imgEnergia.Src = srcImgen12;

            // Session["ATM_CODVERIF"].ToString(); 
            txtnomATM.Text = Session["ATM_NOMBREVERIF"].ToString();
            txtdireccion.Text = Session["ATM_DIRECCIONVERIF"].ToString();
            txtip.Text = Session["ATM_IPVERIF"].ToString();
            txtUbicacionATM.Text = Session["ATM_UBICACIONVERIF"].ToString();
            txtsucursal.Text = Session["ATM_SUCURSALVERIF"].ToString();
            txtzonaVerif.Text = Session["ATM_ZONAVERIF"].ToString();
            TxFechaInicio.Text = Session["ATM_HRINICIOVERIF"].ToString();
            TxFechaRegreso.Text = Session["ATM_HRFINVERIF"].ToString();
            txtsysaid.Text = Session["ATM_SYSAIDVERIF"].ToString();
            txtTecnicoResponsable.Text = Session["ATM_TECNICOVERIF"].ToString();
            txtidentidad.Text = Session["ATM_IDENTIDADVERIF"].ToString();
            txtcodATM.Text = Session["ATM_CODATMVERIF"].ToString();
            txtobseracionesVerif.Text = Session["ATM_OBSERVACIONESVERIF"].ToString();
            txthsalidaInfa.Text = Session["ATM_HRSALIDAINFAVERIF"].ToString();
            txtHllegadaInfatlan.Text = Session["ATM_HRENTRADAINFAVERIF"].ToString();
            txtpuertoVerif.Text = Session["ATM_PUERTOVERIF"].ToString();
            txtSerieDiscoDuro.Text = Session["ATM_SERIEDISCOVERIF"].ToString();
            txtcapacidadDiscoVerif.Text = Session["ATM_CAPACIDADDISCODUROVERIF"].ToString();
            txtserieATM.Text = Session["ATM_SERIEATMVERIF"].ToString();
            txtinventarioVerif.Text = Session["ATM_INVENTARIOVERIF"].ToString();
            txtramVerif.Text = Session["ATM_RAMVERIF"].ToString();
            txtlongitudATM.Text = Session["ATM_LONGITUDVERIF"].ToString();
            txtlatitudATM.Text = Session["ATM_LATITUDVERIF"].ToString();
            txtsoVerif.Text = Session["ATM_SOVERIF"].ToString();
            txtversionswVerif.Text = Session["ATM_VERSIONVERIF"].ToString();
            txtTecladoVerif.Text = Session["ATM_TECLADOVERIF"].ToString();
            txtTipoProcesadorVerif.Text = Session["ATM_PROCESADORVERIF"].ToString();
            txtTipoCargaVerif.Text = Session["ATM_TIPOCARGAVERIF"].ToString();
            txtmarcaVerif.Text = Session["ATM_MARCAVERIF"].ToString();

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

            txtPreguntaAntiskimming.Text = Session["ATM_VERIF_PREG23"].ToString();
            txtantiSkimming.Text = Session["ATM_VERIF_RESP23"].ToString();
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

            try
            {
                string vQuery = "STEISP_ATM_VERIFICACION 5, '" + Session["USUARIO"].ToString() + "','" + Session["ATM_CODVERIF"] + "'";
                Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                if (vInfo == 1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
                    Mensaje("Lista de verificación finalizada con éxito", WarningType.Success);
                    Response.Redirect("buscarAprobarVerificacion.aspx");
                }
                else
                {
                    Mensaje("No se pudo finalizar lista de verificación", WarningType.Warning);
                }
            }
            catch (Exception Ex)
            {
                throw;
            }
        }

        protected void btnModalCerrarVerif_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
        }

        protected void btnRechazarModal_Click(object sender, EventArgs e)
        {
            if (txtmotivoRechazo.Text == "" || txtmotivoRechazo.Text == string.Empty)
            {
                lbValidarModal2.Text = "Especifique motivo por el que devuelve lista de verificación.";
                lbValidarModal2.Visible = true;
            }
            else
            {

                try
                {
                    string vQuery = "STEISP_ATM_VERIFICACION 6, '" + Session["USUARIO"].ToString() + "','" + Session["ATM_CODVERIF"] + "'";
                    Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    if (vInfo == 1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
                        Mensaje("Lista de verificación fue rechazada", WarningType.Success);
                        Response.Redirect("buscarAprobarVerificacion.aspx");
                    }
                    else
                    {
                        Mensaje("No se pudo rechazar lista de verificación", WarningType.Warning);
                    }
                }
                catch (Exception Ex)
                {
                    throw;
                }
            }
        }

        protected void btnCerrarRechazoModal_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal2();", true);
        }

        protected void btnRechazarVerif_Click(object sender, EventArgs e)
        {
            txtmotivoRechazo.Text = string.Empty;
            lbValidarModal2.Visible = false;
            lbcodATM2.Text = txtcodATM.Text;
            lbNombreATM2.Text = txtnomATM.Text;
            lbsucursalATM2.Text = txtsucursal.Text;
            lbInventarioATM2.Text = txtinventarioVerif.Text;
            lbtecnico2.Text = txtTecnicoResponsable.Text;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal2();", true);
        }
    }
}