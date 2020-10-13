using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infatlan_STEI_ATM.clases;
using System.Data;
using System.IO;
using System.Configuration;

namespace Infatlan_STEI_ATM.pages.mantenimiento
{
    public partial class verificacion : System.Web.UI.Page
    {
        bd vConexion = new bd();
        bd vConexionATM = new bd();
        protected void Page_Load(object sender, EventArgs e) {
            string id = Request.QueryString["id"];
            string tipo = Request.QueryString["tipo"];
            Session["VERIF"] = null;

            if (!Page.IsPostBack) {
                if (Convert.ToBoolean(Session["AUTH"]))
                {
                    RBEnergias.SelectedValue = "1";
                    RBClima.SelectedValue = "1";
                    vaciarValorImg();
                    if (dropantiskimming.SelectedValue == "1")
                    {
                        txtantiSkimming.Enabled = true;
                    }
                    else
                    {
                        txtantiSkimming.Enabled = false;
                    }
                    //limpiarModalVerificacion();
                    CargarVerificacion();
                    llenarForm();
                    switch (tipo)
                    {
                        case "2":

                            llenarFormRechazado();
                            llenarImagenes();
                            break;
                        case "4":
                            RBClima.Enabled = false;
                            RBEnergias.Enabled = false;
                            FUATMLinea.Enabled = false;
                            FUmapaATM.Enabled = false;
                            txtcomentarioATMLinea.Enabled = false;
                            btnRechazarVerif.Visible = true;
                            GVNewMateriales.Enabled = false;
                            llenarFormRechazado();
                            llenarImagenes();
                            aprobacionCampos();
                            txtlatitudATM.Enabled = false;
                            txtlongitudATM.Enabled = false;
                            break;
                    }
                }
                else
                {
                    Response.Redirect("/login.aspx");
                }
        }
    }


        void vaciarValorImg()
        {
            HFDiscoDuro.Value = "";
            HFATMDesarmadoPS.Value = "";
            HFATMDesarmadoPI.Value = "";
            HFDispositivoVendor.Value = "";
            HFSYSTEMINFO.Value = "";
            HFAntiskimmin.Value = "";
            HFMonitorFiltro.Value = "";
            HFPadlewheel.Value = "";
            HFDispDesarmado.Value = "";
            HFTeclado.Value = "";
            HFATMLinea.Value = "";
            HFEnergia.Value = "";
            HFClima.Value ="";
            HFMapa.Value = "";
            RBEnergias.SelectedValue = "1";
            RBClima.SelectedValue = "1";
        }
        
        void aprobacionCampos()
        {
            txtHllegadaInfatlan.Enabled = false;
            txthsalidaInfa.Enabled = false;
            TxFechaInicio.Enabled = false;
            TxFechaRegreso.Enabled = false;
            dropantiskimming.Enabled = false;
            txtantiSkimming.Enabled = false;
            DIVPreguntas.EnableViewState = false;
            //tblImg.EnableViewState = false;
            txtobseracionesVerif.Enabled = false;
            DDLmarcaDiscoDuro.Enabled = false;
            DDLtipoCargaVerif.Enabled = false;
            DDLtipoProc.Enabled = false;
            DDLtipoTeclado.Enabled = false;
            txtserieATM.Enabled = false;
            txtSerieDiscoDuro.Enabled = false;
            txtcapacidadDiscoVerif.Enabled = false;
            txtinventarioVerif.Enabled = false;
            txtramVerif.Enabled = false;
            FUDiscoDuro.Enabled = false;
            FUATMDesarmadoPS.Enabled = false;
            FUATMDesarmadoPI.Enabled = false;
            FUDispositivoVendor.Enabled = false;
            FUSYSTEMINFO.Enabled = false;
            FUDispDesarmado.Enabled = false;
            FUAntiskimmin.Enabled = false;
            FUMonitorFiltro.Enabled = false;
            FUPadlewheel.Enabled = false;
            FUTeclado.Enabled = false;
            FUClimatizacion.Enabled = false;
            FUEnergia.Enabled = false;
            ckpasos1.Enabled = false;
            ckpasos2.Enabled = false;
            ckpasos3.Enabled = false;
            ckpasos4.Enabled = false;
            ckpasos5.Enabled = false;
            ckpasos6.Enabled = false;
            ckpasos7.Enabled = false;
            ckpasos8.Enabled = false;
            ckpasos9.Enabled = false;
            ckpasos10.Enabled = false;
            ckpasos11.Enabled = false;
            ckpasos12.Enabled = false;
            ckpasos13.Enabled = false;
            ckpasos14.Enabled = false;
            ckpasos15.Enabled = false;
            ckpasos16.Enabled = false;
            ckpasos17.Enabled = false;
            ckpasos18.Enabled = false;
            ckpasos19.Enabled = false;
            ckpasos20.Enabled = false;
        }

        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        void llenarImagenes()
        {
            //td1img1.Visible = true;
            //td2img1.Visible = false;
            //IMAGEN1
            string vImagen1 = Session["ATM_VERIF_IMG1"].ToString();
            string srcImgen1 = "data:image;base64," + vImagen1;
            imgDiscoDuro.Src = srcImgen1;
            HFDiscoDuro.Value = "si";
            //IMAGEN2
            string vImagen2 = Session["ATM_VERIF_IMG2"].ToString();
            string srcImgen2 = "data:image;base64," + vImagen2;
            imgATMDesarmadoPS.Src = srcImgen2;
            HFATMDesarmadoPS.Value = "si";
            //IMAGEN3
            string vImagen3 = Session["ATM_VERIF_IMG3"].ToString();
            string srcImgen3 = "data:image;base64," + vImagen3;
            imgATMDesarmadoPI.Src = srcImgen3;
            HFATMDesarmadoPI.Value = "si";
            //IMAGEN4
            string vImagen4 = Session["ATM_VERIF_IMG4"].ToString();
            string srcImgen4 = "data:image;base64," + vImagen4;
            imgDispositivoVendor.Src = srcImgen4;
            HFDispositivoVendor.Value = "si";
            //IMAGEN5
            string vImagen5 = Session["ATM_VERIF_IMG5"].ToString();
            string srcImgen5 = "data:image;base64," + vImagen5;
            imgSYSTEMINFO.Src = srcImgen5;
            HFSYSTEMINFO.Value = "si";
            //IMAGEN6
            string vImagen6 = Session["ATM_VERIF_IMG6"].ToString();
            string srcImgen6 = "data:image;base64," + vImagen6;
            imgAntiskimmin.Src = srcImgen6;
            HFAntiskimmin.Value = "si";
            //IMAGEN7
            string vImagen7 = Session["ATM_VERIF_IMG7"].ToString();
            string srcImgen7 = "data:image;base64," + vImagen7;
            imgMonitorFiltro.Src = srcImgen7;
            HFMonitorFiltro.Value = "si";
            //IMAGEN8
            string vImagen8 = Session["ATM_VERIF_IMG8"].ToString();
            string srcImgen8 = "data:image;base64," + vImagen8;
            imgPadlewheel.Src = srcImgen8;
            HFPadlewheel.Value = "si";
            //IMAGEN9
            string vImagen9 = Session["ATM_VERIF_IMG9"].ToString();
            string srcImgen9 = "data:image;base64," + vImagen9;
            imgDispDesarmado.Src = srcImgen9;
            HFDispDesarmado.Value = "si";
            //IMAGEN10
            string vImagen10 = Session["ATM_VERIF_IMG10"].ToString();
            string srcImgen10 = "data:image;base64," + vImagen10;
            imgTeclado.Src = srcImgen10;
            HFTeclado.Value = "si";
            //IMAGEN11
            string vImagen11 = Session["ATM_VERIF_IMG21"].ToString();
            string srcImgen11 = "data:image;base64," + vImagen11;
            HFEnergia.Value = "si";
            if (vImagen11 == "")
                
                imgClimatizacion.Src = "../../assets/images/vistaPrevia1.JPG";
            else
                imgClimatizacion.Src = srcImgen11;
            //IMAGEN12
            string vImagen12 = Session["ATM_VERIF_IMG22"].ToString();
            string srcImgen12 = "data:image;base64," + vImagen12;
            HFClima.Value = "si";
            if (vImagen12 == "")
                
                imgEnergia.Src = "../../assets/images/vistaPrevia1.JPG";
            else
                imgEnergia.Src = srcImgen12;
            //IMAGEN13
            string vImagen13 = Session["ATM_VERIF_IMG11"].ToString();
            string srcImgen13 = "data:image;base64," + vImagen13;
            imgATMLinea.Src = srcImgen13;
            HFATMLinea.Value = "si";

        }
        
        void llenarFormRechazado()
        {
            txthsalidaInfa.Text = Session["ATM_HRSALIDAINFA_VERIF_CREAR"].ToString();
            txtHllegadaInfatlan.Text = Session["ATM_HRENTRADAINFA_VERIF_CREAR"].ToString();
            TxFechaInicio.Text = Session["ATM_HRINICIO_VERIF_CREAR"].ToString();
            TxFechaRegreso.Text = Session["ATM_HRFIN_VERIF_CREAR"].ToString();
            txtobseracionesVerif.Text = Session["ATM_OBSERVACIONES_VERIF_CREAR"].ToString();

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
            if (Session["ATM_VERIF_PREG23"].ToString() == "Si")
            {
                dropantiskimming.SelectedValue = "1";
                txtantiSkimming.Enabled = true;
            }
            else
            {
                dropantiskimming.SelectedValue = "2";
                txtantiSkimming.Enabled = false;
            }
            txtantiSkimming.Text = Session["ATM_VERIF_RESP23"].ToString();
            if (Session["ATM_VERIF_PREG21"].ToString() == "Si")
                RBClima.SelectedValue = "1";
            else
                RBClima.SelectedValue = "2";
           
            if (Session["ATM_VERIF_PREG22"].ToString() == "Si")
                RBEnergias.SelectedValue = "1";
            else
                RBEnergias.SelectedValue = "2";

        }
        
        void llenarForm()
        {
            txtcodATM.Text = Session["ATM_CODATM_VERIF_CREAR"].ToString();
            txtnomATM.Text = Session["ATM_NOMATM_VERIF_CREAR"].ToString();
            txtdireccion.Text = Session["ATM_DIRECCION_VERIF_CREAR"].ToString();
            txtip.Text = Session["ATM_IP_VERIF_CREAR"].ToString();
            txtpuertoVerif.Text = Session["ATM_PUERTOATM_VERIF_CREAR"].ToString();
            DDLtipoTeclado.SelectedIndex = CargarInformacionDDL(DDLtipoTeclado, Session["ATM_TECLADO_VERIF_CREAR"].ToString());
            DDLtipoProc.SelectedIndex = CargarInformacionDDL(DDLtipoProc, Session["ATM_PROCESADOR_VERIF_CREAR"].ToString());
            DDLtipoCargaVerif.SelectedIndex = CargarInformacionDDL(DDLtipoCargaVerif, Session["ATM_TIPOCARGA_VERIF_CREAR"].ToString());
            DDLmarcaDiscoDuro.SelectedIndex = CargarInformacionDDL(DDLmarcaDiscoDuro, Session["ATM_MARCA_VERIF_CREAR"].ToString());
            txtSerieDiscoDuro.Text = Session["ATM_SERIEDISCO_VERIF_CREAR"].ToString();
            txtserieATM.Text = Session["ATM_SERIEATM_VERIF_CREAR"].ToString();

            string MyString = Session["ATM_CAPACIDADDISCO_VERIF_CREAR"].ToString();
            char[] MyChar = { 'G', 'B', ' ' };
            string NewString = MyString.TrimEnd(MyChar);

            txtcapacidadDiscoVerif.Text = NewString;
            txtinventarioVerif.Text = Session["ATM_INVENTARIO_VERIF_CREAR"].ToString();

            string MyString2 = Session["ATM_RAM_VERIF_CREAR"].ToString();
            char[] MyChar2 = { 'G', 'B', ' ' };
            string NewString2 = MyString2.TrimEnd(MyChar2);

            txtramVerif.Text = NewString2;
            txtlatitudATM.Text = Session["ATM_LATITUD_VERIF_CREAR"].ToString();
            txtlongitudATM.Text = Session["ATM_LONGITUD_VERIF_CREAR"].ToString();
            txtUbicacionATM.Text = Session["ATM_UBICACION_VERIF_CREAR"].ToString();
            txtsucursal.Text = Session["ATM_SUCURSAL_VERIF_CREAR"].ToString();
            txtzonaVerif.Text = Session["ATM_ZONA_VERIF_CREAR"].ToString();
            txtsysaid.Text = Session["ATM_SYSAID_VERIF_CREAR"].ToString();
            txtTecnicoResponsable.Text = Session["ATM_TECNICO_VERIF_CREAR"].ToString();
            //Session["ATM_USUARIO_VERIF_CREAR"] 
            txtidentidad.Text = Session["ATM_IDENTIDAD_VERIF_CREAR"].ToString();
            txtsoVerif.Text = Session["ATM_SO_VERIF_CREAR"].ToString();
            txtversionswVerif.Text = Session["ATM_VERSIONSW_VERIF_CREAR"].ToString();
            txtcomentarioATMLinea.Text = Session["ATM_ATMACTIVO_VERIF_CREAR"].ToString();
            //txtcodATM.Text = Session["codATM"].ToString();
            // DDLsucursalATM.SelectedIndex = CargarInformacionDDL(DDLsucursalATM, Session["sucursalATM"].ToString());

            //MATERAILES
            DataTable vDatos2 = new DataTable();
            vDatos2 = vConexion.ObtenerTabla("STEISP_ATM_VERIFICACION 13, '" + Session["ATM_IDMANT_VERIF_CREAR"].ToString() + "'");
            GVNewMateriales.DataSource = vDatos2;
            GVNewMateriales.DataBind();
            Session["ATM_DEVOLVER_MATERIALES_VERIF"] = vDatos2;

            DataTable vDatosImg = new DataTable();
            String vQueryImg = "SPSTEI_ATM 33,'" + txtcodATM.Text + "'";
            vDatosImg = vConexionATM.ObtenerTablaATM(vQueryImg);
            foreach (DataRow item in vDatosImg.Rows)
            {
                string vImagen14 = item["imgMapaATM"].ToString();
                if (vImagen14 != "")
                {
                    string srcImgen14 = "data:image;base64," + vImagen14;
                    imgMapaATM.Src = srcImgen14;
                    HFMapa.Value = "si";
                }
            }

        }

        void validar()
        {               
            if (txthsalidaInfa.Text == "" || txthsalidaInfa.Text == string.Empty)
                    throw new Exception("Favor ingrese la hora que salio de Infatlan.");
            if (txtHllegadaInfatlan.Text == "" || txtHllegadaInfatlan.Text == string.Empty)
                throw new Exception("Favor ingrese la hora que llego a Infatlan.");
            if (TxFechaInicio.Text == "" || TxFechaInicio.Text == string.Empty)
                throw new Exception("Favor ingrese la hora en la que inicio mantenimiento.");
            if (TxFechaRegreso.Text == "" || TxFechaRegreso.Text == string.Empty)
                throw new Exception("Favor ingrese la hora en la que termino mantenimiento.");
            if (DDLtipoTeclado.SelectedValue == "0")
                throw new Exception("Favor seleccione teclado.");
            if (DDLtipoProc.SelectedValue == "0")
                throw new Exception("Favor seleccione procesador.");
            if (DDLtipoCargaVerif.SelectedValue == "0")
                throw new Exception("Favor seleccione tipo de carga.");
            if (DDLmarcaDiscoDuro.SelectedValue == "0")
                throw new Exception("Favor seleccione marca.");
            if (DDLtipoTeclado.SelectedValue == "0")
                throw new Exception("Favor seleccione teclado.");
            if (txtSerieDiscoDuro.Text == "" || txtSerieDiscoDuro.Text == string.Empty)
                throw new Exception("Favor ingrese la serie de disco duro.");
            if (txtcapacidadDiscoVerif.Text == "" || txtcapacidadDiscoVerif.Text == string.Empty)
                throw new Exception("Favor ingrese la capacidad de memoria de disco duro.");
            if (txtserieATM.Text == "" || txtserieATM.Text == string.Empty)
                throw new Exception("Favor ingrese la serie de ATM.");
            if (txtinventarioVerif.Text == "" || txtinventarioVerif.Text == string.Empty)
                throw new Exception("Favor ingrese el número de inventario.");
            if (txtramVerif.Text == "" || txtramVerif.Text == string.Empty)
                throw new Exception("Favor ingrese la capacidad RAM.");
            if (dropantiskimming.SelectedValue == "1")
            {
                if (txtantiSkimming.Text == "" || txtantiSkimming.Text == string.Empty)
                    throw new Exception("Favor ingrese comentario de AntiSkimming.");
            }
            else if (dropantiskimming.SelectedValue == "0")
            {
                throw new Exception("Favor seleccione una respuesta de AntiSkimming.");
            }

            if (txtobseracionesVerif.Text == "" || txtobseracionesVerif.Text == string.Empty)
                throw new Exception("Favor ingrese sus observaciones del caso.");
            if (HFDiscoDuro.Value== string.Empty)
                throw new Exception("Favor agregar imagen de disco duro.");
            if (HFATMDesarmadoPS.Value == string.Empty)
                throw new Exception("Favor agregar imagen de ATM desarmado parte superior.");
            if (HFATMDesarmadoPI.Value == string.Empty)
                throw new Exception("Favor agregar imagen de ATM desarmado parte inferior.");
            if (HFDispositivoVendor.Value == string.Empty)
                throw new Exception("Favor agregar imagen de dispositivo vendor.");
            if (HFSYSTEMINFO.Value == string.Empty)
                throw new Exception("Favor agregar imagen de SistemInfo.");
            if (HFAntiskimmin.Value == string.Empty)
                throw new Exception("Favor agregar imagen de antiskimming.");
            if (HFMonitorFiltro.Value == string.Empty)
                throw new Exception("Favor agregar imagen de filtro de monitor.");
            if (HFPadlewheel.Value == string.Empty)
                throw new Exception("Favor agregar imagen de Padlewheel.");
            if (HFDispDesarmado.Value == string.Empty)
                throw new Exception("Favor agregar imagen de dispositivo desarmado.");
            if (HFTeclado.Value == string.Empty)
                throw new Exception("Favor agregar imagen de teclado.");
            if (HFEnergia.Value == string.Empty)
                throw new Exception("Favor agregar imagen de energia.");
            if (HFClima.Value == string.Empty)
                throw new Exception("Favor agregar imagen de climatización.");
            if (HFATMLinea.Value == string.Empty)
                throw new Exception("Favor agregar imagen de ATM en línea.");
            if (HFMapa.Value == string.Empty)
                throw new Exception("Favor agregar imagen del mapa de ubicación de ATM.");
            if (txtcomentarioATMLinea.Text == "" || txtcomentarioATMLinea.Text == string.Empty)
                throw new Exception("Favor ingrese comentario sobre ATM en línea.");

            //string vDevolver = "";
            //String vAdvertencia = "";
            //DataTable vDatos = (DataTable)Session["ATM_DEVOLVER_MATERIALES_VERIF"];
            //for (int i = 0; i < vDatos.Rows.Count; i++)
            //{
            //    vDevolver = vDatos.Rows[i]["devolver"].ToString();
            //    if (vDevolver == "")
            //        vAdvertencia = "No";
            //}
            //if (vAdvertencia == "No")
            //    throw new Exception("Ingrese todos los materiales utilizados, no deje vacíos.");
        }

        int CargarInformacionDDL(DropDownList vList, String vValue)
        {
            int vIndex = 0;
            try
            {
                int vContador = 0;
                foreach (ListItem item in vList.Items)
                {
                    if (item.Value.Equals(vValue))
                    {
                        vIndex = vContador;
                    }
                    vContador++;
                }
            }
            catch { throw; }
            return vIndex;
        }

        void CargarVerificacion()
            {
            if (HttpContext.Current.Session["VERIF"] == null)
            {
                try
                {
                    //if (HttpContext.Current.Session["SUCURSAL"] == null)
                    //{
                    String vQuery = "SPSTEI_ATM 7";
                    DataTable vDatos = vConexionATM.ObtenerTablaATM(vQuery);
                    DDLtipoTeclado.Items.Add(new ListItem { Value = "0", Text = "Seleccione teclado..." });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLtipoTeclado.Items.Add(new ListItem { Value = item["Id_Teclado_ATM"].ToString(), Text = item["Descripcion"].ToString() });
                        //}
                        //    Session["SUCURSAL"] = "1";
                    }

                }
                catch (Exception ex)
                {
                    throw;
                }



                try
                {
                    //if (HttpContext.Current.Session["CARGA"] == null)
                    //{
                    String vQuery = "SPSTEI_ATM 5";
                    DataTable vDatos = vConexionATM.ObtenerTablaATM(vQuery);
                    DDLtipoCargaVerif.Items.Add(new ListItem { Value = "0", Text = "Seleccione tipo de carga..." });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLtipoCargaVerif.Items.Add(new ListItem { Value = item["Id_Carga_ATM"].ToString(), Text = item["Descripcion"].ToString() });
                        //    }
                        //    Session["CARGA"] = "1";
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }

                try
                {
                    //if (HttpContext.Current.Session["PROCESADOR"] == null)
                    //{
                    String vQuery = "SPSTEI_ATM 6";
                    DataTable vDatos = vConexionATM.ObtenerTablaATM(vQuery);
                    DDLtipoProc.Items.Add(new ListItem { Value = "0", Text = "Seleccione tipo de procesador..." });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLtipoProc.Items.Add(new ListItem { Value = item["Id_Procesador_ATM"].ToString(), Text = item["Descripcion"].ToString() });
                        //    }
                        //    Session["PROCESADOR"] = "1";
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }

                try
                {
                    //if (HttpContext.Current.Session["MARCA"] == null)
                    //{
                    String vQuery = "SPSTEI_ATM 11";
                    DataTable vDatos = vConexionATM.ObtenerTablaATM(vQuery);
                    DDLmarcaDiscoDuro.Items.Add(new ListItem { Value = "0", Text = "Seleccione la marca..." });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLmarcaDiscoDuro.Items.Add(new ListItem { Value = item["Id_Disco_ATM"].ToString(), Text = item["Descripcion"].ToString() });
                    }
                    //    Session["MARCA"] = "1";
                    //}
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            Session["VERIF"] = "1";
        }

        protected void dropantiskimming_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private string GetExtension(string Extension)
        {
            switch (Extension)
            {
                case ".doc":
                    return "application/ms-word";
                case ".xls":
                    return "application/vnd.ms-excel";
                case ".ppt":
                    return "application/mspowerpoint";
                case "jpeg":
                    return "image/jpeg";
                case ".bmp":
                    return "image/bmp";
                case ".zip":
                    return "application/zip";
                case ".log":
                    return "text/HTML";
                case ".txt":
                    return "text/plain";
                case ".tiff":
                case ".tif":
                    return "image/tiff";
                case ".asf":
                    return "video/x-ms-asf";
                case ".avi":
                    return "video/avi";
                case ".gif":
                    return "image/gif";
                case ".jpg":
                case ".wav":
                    return "audio/wav";
                case ".pdf":
                    return "application/pdf";
                case ".fdf":
                    return "application/vnd.fdf";
                case ".dwg":
                    return "image/vnd.dwg";
                case ".msg":
                    return "application/msoutlook";
                case ".xml":
                    return "application/xml";
                default:
                    return "application/octet-stream";
            }
        }

        void EnviarCorreo()
        {
            string vCorreoEncargadoZona = "";
            if (Convert.ToString(Session["ATM_IDZONA_VERIF_CREAR"]) == "1")
                vCorreoEncargadoZona = "emontoya@bancatlan.hn";
            if (Convert.ToString(Session["ATM_IDZONA_VERIF_CREAR"]) == "2")
                vCorreoEncargadoZona = "jdgarcia@bancatlan.hn";
            if (Convert.ToString(Session["ATM_IDZONA_VERIF_CREAR"]) == "3")
                vCorreoEncargadoZona = "acalderon@bancatlan.hn";

            string tipo = Request.QueryString["tipo"];
            SmtpService vService = new SmtpService();
            string vQueryD = "STEISP_ATM_Generales 33,'" + Session["ATM_USUARIO_VERIF_CREAR"] + "'";
            DataTable vDatosTecnicoResponsable = vConexion.ObtenerTabla(vQueryD);
            DataTable vDatos = (DataTable)Session["AUTHCLASS"];
            string vQueryTecnicos = "STEISP_ATM_Generales 39,'" + Session["ATM_IDMANT_VERIF_CREAR"] + "'";
            DataTable vDatosTecnicos = vConexion.ObtenerTabla(vQueryTecnicos);
            string vQueryJefes = "STEISP_ATM_Generales 38,'" + Session["ATM_IDMANT_VERIF_CREAR"] + "'";
            DataTable vDatosJefeAgencias = vConexion.ObtenerTabla(vQueryJefes);

            if (tipo == "4")//APROBAR
            {         
                
                if (vDatos.Rows.Count > 0)
                {
                    foreach (DataRow item in vDatos.Rows)
                    {
                        //ENVIAR A JEFE
                        if (!item["correo"].ToString().Trim().Equals(""))
                        {
                            vService.EnviarMensaje(
                            item["correo"].ToString(),
                            typeBody.ATM,
                            "Notificación de Mantenimiento ATM",
                            "Buen día, se le notifica que ha aprobado solicitud de mantenimiento, el encargado es " + vDatosTecnicoResponsable.Rows[0]["nombre"].ToString() + ", mantenimiento al ATM " + txtnomATM.Text ,
                            "El usuario <b>" + item["Nombre"].ToString() + "</b> aprobó: <br> Verificación de Mantenimiento",
                             "",
                             "/sites/ATM/pages/mantenimiento/buscarAprobarVerificacion.aspx"
                            );

                            //vFlagEnvioSupervisor = true;
                        }
                        //ENVIAR A EDWIN
                        //string vNombre = "EDWIN ALBERTO URREA PENA";
                        vService.EnviarMensaje(ConfigurationManager.AppSettings["STEIMail"],
                                typeBody.ATM,
                                "Notificación de Mantenimiento ATM",
                                   "Buen día, se le notifica que ha aprobado solicitud de mantenimiento, el encargado es " + vDatosTecnicoResponsable.Rows[0]["nombre"].ToString() + ", mantenimiento al ATM " + txtnomATM.Text,
                                "El usuario <b>" + item["Nombre"].ToString() + "</b> aprobó: <br> Verificación de Mantenimiento",
                                  vCorreoEncargadoZona,
                                  "/sites/ATM/pages/mantenimiento/buscarAprobarVerificacion.aspx"
                                );
                        //ENVIAR A TECNICOS ASIGNADOS
                        //string vNombreJefe = "ELVIS ALEXANDER MONTOYA PEREIRA";

                    }
                }
                if (vDatosTecnicoResponsable.Rows.Count > 0)
                {
                    foreach (DataRow item in vDatosTecnicoResponsable.Rows)
                    {
                        //ENVIAR A RESPONSABLE
                        vService.EnviarMensaje(item["correo"].ToString(),
                            typeBody.ATM,
                           "Notificación de Mantenimiento ATM",
                            "Buen día, se le notifica que se ha aprobado solicitud de mantenimiento, el encargado es " + item["nombre"].ToString() + ", mantenimiento al ATM " + txtnomATM.Text,
                              "El usuario <b>" + vDatos.Rows[0]["Nombre"].ToString() + "</b> aprobó: <br> Verificación de Mantenimiento de ATM al que ha sido asignado como responsable.",
                              "",
                             "/login.aspx"
                            );
                    }
                }
                if (vDatosTecnicos.Rows.Count > 0)
                {
                    foreach (DataRow itemT in vDatosTecnicos.Rows)
                    {
                        vService.EnviarMensaje(itemT["correo"].ToString(),
                            typeBody.ATM,
                            "Notificación de Mantenimiento ATM",
                            "Buen día, se le notifica que se ha aprobado solicitud de mantenimiento, el encargado es " + vDatosTecnicoResponsable.Rows[0]["nombre"].ToString() + ", mantenimiento al ATM " + txtnomATM.Text,
                              "El usuario <b>" + vDatos.Rows[0]["Nombre"].ToString() + "</b> aprobó: <br> Verificación de Mantenimiento de ATM al que ha sido asignado como parte del equipo de trabajo.",
                              "",
                             "/login.aspx"
                            );
                    }
                }              
            }
            else//CREAR
            {
                if (vDatos.Rows.Count > 0)
                {
                    foreach (DataRow item in vDatos.Rows)
                    {
                        //ENVIAR A JEFE
                        if (!item["correo"].ToString().Trim().Equals(""))
                        {
                            vService.EnviarMensaje(
                            item["correo"].ToString(),
                            typeBody.ATM,
                            "Notificación de Mantenimiento ATM",
                            "Buen día, se le notifica que ha creado una verificación de mantenimiento, el encargado es " + vDatosTecnicoResponsable.Rows[0]["nombre"].ToString() + ", mantenimiento al ATM " + txtnomATM.Text,
                            "El usuario <b>" + item["Nombre"].ToString() + "</b> creó: <br> Verificación de Mantenimiento",
                             "",
                             "/login.aspx"
                            );

                            //vFlagEnvioSupervisor = true;
                        }
                        //ENVIAR A EDWIN
                        //string vNombre = "EDWIN ALBERTO URREA PENA";
                        vService.EnviarMensaje(ConfigurationManager.AppSettings["STEIMail"],
                                typeBody.ATM,
                                "Notificación de Mantenimiento ATM",
                                   "Buen día, se le notifica que ha creado una verificación de mantenimiento, el encargado es " + vDatosTecnicoResponsable.Rows[0]["nombre"].ToString() + ", mantenimiento al ATM " + txtnomATM.Text,
                                "El usuario <b>" + item["Nombre"].ToString() + "</b> creó: <br> Verificación de Mantenimiento",
                                  vCorreoEncargadoZona + ";unidadatmkiosco@bancatlan.hn",
                                  "/sites/ATM/pages/mantenimiento/buscarVerificacion.aspx"
                                );
                        //ENVIAR A TECNICOS ASIGNADOS
                        //string vNombreJefe = "ELVIS ALEXANDER MONTOYA PEREIRA";

                    }
                }              
                if (vDatosTecnicos.Rows.Count > 0)
                {
                    foreach (DataRow itemT in vDatosTecnicos.Rows)
                    {
                        vService.EnviarMensaje(itemT["correo"].ToString(),
                            typeBody.ATM,
                            "Notificación de Mantenimiento ATM",
                            "Buen día, se le notifica que se ha creado una verificación de mantenimiento, el encargado es " + vDatosTecnicoResponsable.Rows[0]["nombre"].ToString() + ", mantenimiento al ATM " + txtnomATM.Text,
                              "El usuario <b>" + vDatos.Rows[0]["Nombre"].ToString() + "</b> creó: <br> Verificación de Mantenimiento de ATM al que ha sido asignado como parte del equipo de trabajo.",
                              "",
                             "/login.aspx"
                            );
                    }
                }
                

            }
        }
        void ActualizarATM()
            {


            try
            {

                string vQuery = "SPSTEI_ATM 35, '" + Session["ATM_CODATM_VERIF_CREAR"] + "','" + DDLtipoTeclado.SelectedValue + "','" + DDLtipoProc.SelectedValue + "'," +
                    "'" + DDLtipoCargaVerif.SelectedValue + "','" + DDLmarcaDiscoDuro.SelectedValue + "','" + txtSerieDiscoDuro.Text + "','" + txtcapacidadDiscoVerif.Text + " GB" + "','" + txtserieATM.Text + "'," +
                    "'" + txtinventarioVerif.Text + "','" + txtramVerif.Text + " GB" + "','" + Session["USUARIO"].ToString() + "','" + txtlatitudATM.Text + "','" + txtlongitudATM.Text + "'";
                Int32 vInfo = vConexionATM.ejecutarSQLATM(vQuery);
                if (vInfo == 1)
                {
                    //IMAGENES1
                    String vNombreDepot11 = String.Empty;
                    HttpPostedFile bufferDeposito11 = FUmapaATM.PostedFile;
                    byte[] vFileDeposito11 = null;
                    string vExtension11 = string.Empty;

                    if (bufferDeposito11 != null)
                    {
                        vNombreDepot11 = FUmapaATM.FileName;
                        Stream vStream11 = bufferDeposito11.InputStream;
                        BinaryReader vReader11 = new BinaryReader(vStream11);
                        vFileDeposito11 = vReader11.ReadBytes((int)vStream11.Length);
                        vExtension11 = System.IO.Path.GetExtension(FUmapaATM.FileName);
                    }
                    String vArchivo = String.Empty;
                    if (vFileDeposito11 != null)
                        vArchivo = Convert.ToBase64String(vFileDeposito11);

                    if (FUmapaATM.HasFile != false)
                    {
                        string vQuery2 = "SPSTEI_ATM 34, '" + txtcodATM.Text + "','" + vArchivo + "'";
                        vConexionATM.ejecutarSQLATM(vQuery2);
                    }
                }

            }
            catch (Exception Ex)
            {
                throw;
            }
        }

        void ActualizarMateriales()
        {
            DataTable vDatos = (DataTable)Session["ATM_DEVOLVER_MATERIALES_VERIF"];
            for (int i = 0; i < vDatos.Rows.Count; i++)
            {
                string vMantenimiento = vDatos.Rows[i]["idMantenimiento"].ToString();
                string vStock = vDatos.Rows[i]["idStock"].ToString();
                int vSolicitado = Convert.ToInt32(vDatos.Rows[i]["cantidad"].ToString());
                int vDevolver = Convert.ToInt32(vDatos.Rows[i]["devolver"].ToString());

                string vQuery = "STEISP_ATM_VERIFICACION 15, '" + vMantenimiento + "','" + vStock + "','" + vDevolver + "'";                
                Int32 vInfo = vConexion.ejecutarSQL(vQuery);
               
            }
        }

        void ActualizarVerifATM()
        {


            try
            {
                string vQuery = "STEISP_ATM_VerificacionTotal 1, '" + Session["ATM_IDMANT_VERIF_CREAR"] + "','" + txthsalidaInfa.Text + "','" + txtHllegadaInfatlan.Text + "'," +
                    "'" + TxFechaInicio.Text + "','" + TxFechaRegreso.Text + "','" + txtobseracionesVerif.Text + "','" + Session["USUARIO"].ToString() + "','" + txtcomentarioATMLinea.Text + "'";
                    
                Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                if (vInfo == 1)
                {
                    //EnviarCorreo();
                }

            }
            catch (Exception Ex)
            {
                throw;
            }
        }

        void PreguntasVerif(){
        string id = Request.QueryString["id"];
        string tipo = Request.QueryString["tipo"];
        string respuesta1 = null;
        string respuesta2 = null;
        string respuesta3 = null;
        string respuesta4 = null;
        string respuesta5 = null;
        string respuesta6 = null;
        string respuesta7 = null;
        string respuesta8 = null;
        string respuesta9 = null;
        string respuesta10 = null;
        string respuesta11 = null;
        string respuesta12 = null;
        string respuesta13 = null;
        string respuesta14 = null;
        string respuesta15 = null;
        string respuesta16 = null;
        string respuesta17 = null;
        string respuesta18 = null;
        string respuesta19 = null;
        string respuesta20 = null;
        for (int i = 0; i < ckpasos1.Items.Count; i++)
        {
            if (ckpasos1.Items[i].Selected)
                respuesta1 = "Si";
            else
                respuesta1 = "No";
        }
        ////////////////////////////////////////////////////////////
        for (int i = 0; i < ckpasos2.Items.Count; i++)
        {
            if (ckpasos2.Items[i].Selected)
                respuesta2 = "Si";
            else
                respuesta2 = "No";
        }
        ////////////////////////////////////////////////////////////
        for (int i = 0; i < ckpasos3.Items.Count; i++)
        {
            if (ckpasos3.Items[i].Selected)
                respuesta3 = "Si";
            else
                respuesta3 = "No";
        }
        ////////////////////////////////////////////////////////////
        for (int i = 0; i < ckpasos4.Items.Count; i++)
        {
            if (ckpasos4.Items[i].Selected)
                respuesta4 = "Si";
            else
                respuesta4 = "No";
        }
        ////////////////////////////////////////////////////////////
        for (int i = 0; i < ckpasos5.Items.Count; i++)
        {
            if (ckpasos5.Items[i].Selected)
                respuesta5 = "Si";
            else
                respuesta5 = "No";
        }
        ////////////////////////////////////////////////////////////
        for (int i = 0; i < ckpasos6.Items.Count; i++)
        {
            if (ckpasos6.Items[i].Selected)
                respuesta6 = "Si";
            else
                respuesta6 = "No";
        }
        ////////////////////////////////////////////////////////////
        for (int i = 0; i < ckpasos7.Items.Count; i++)
        {
            if (ckpasos7.Items[i].Selected)
                respuesta7 = "Si";
            else
                respuesta7 = "No";
        }
        ////////////////////////////////////////////////////////////
        for (int i = 0; i < ckpasos8.Items.Count; i++)
        {
            if (ckpasos8.Items[i].Selected)
                respuesta8 = "Si";
            else
                respuesta8 = "No";
        }
        ////////////////////////////////////////////////////////////
        for (int i = 0; i < ckpasos9.Items.Count; i++)
        {
            if (ckpasos9.Items[i].Selected)
                respuesta9 = "Si";
            else
                respuesta9 = "No";
        }
        ////////////////////////////////////////////////////////////
        for (int i = 0; i < ckpasos10.Items.Count; i++)
        {
            if (ckpasos10.Items[i].Selected)
                respuesta10 = "Si";
            else
                respuesta10 = "No";
        }
        ////////////////////////////////////////////////////////////
        for (int i = 0; i < ckpasos11.Items.Count; i++)
        {
            if (ckpasos11.Items[i].Selected)
                respuesta11 = "Si";
            else
                respuesta11 = "No";
        }
        ////////////////////////////////////////////////////////////
        for (int i = 0; i < ckpasos12.Items.Count; i++)
        {
            if (ckpasos12.Items[i].Selected)
                respuesta12 = "Si";
            else
                respuesta12 = "No";
        }
        ////////////////////////////////////////////////////////////
        for (int i = 0; i < ckpasos13.Items.Count; i++)
        {
            if (ckpasos13.Items[i].Selected)
                respuesta13 = "Si";
            else
                respuesta13 = "No";
        }
        ////////////////////////////////////////////////////////////
        for (int i = 0; i < ckpasos14.Items.Count; i++)
        {
            if (ckpasos14.Items[i].Selected)
                respuesta14 = "Si";
            else
                respuesta14 = "No";
        }
        ////////////////////////////////////////////////////////////
        for (int i = 0; i < ckpasos15.Items.Count; i++)
        {
            if (ckpasos15.Items[i].Selected)
                respuesta15 = "Si";
            else
                respuesta15 = "No";
        }
        ////////////////////////////////////////////////////////////
        for (int i = 0; i < ckpasos16.Items.Count; i++)
        {
            if (ckpasos16.Items[i].Selected)
                respuesta16 = "Si";
            else
                respuesta16 = "No";
        }
        ////////////////////////////////////////////////////////////
        for (int i = 0; i < ckpasos17.Items.Count; i++)
        {
            if (ckpasos17.Items[i].Selected)
                respuesta17 = "Si";
            else
                respuesta17 = "No";
        }
        ////////////////////////////////////////////////////////////
        for (int i = 0; i < ckpasos18.Items.Count; i++)
        {
            if (ckpasos18.Items[i].Selected)
                respuesta18 = "Si";
            else
                respuesta18 = "No";
        }
        ////////////////////////////////////////////////////////////
        for (int i = 0; i < ckpasos19.Items.Count; i++)
        {
            if (ckpasos19.Items[i].Selected)
                respuesta19 = "Si";
            else
                respuesta19 = "No";
        }
        ////////////////////////////////////////////////////////////
        for (int i = 0; i < ckpasos20.Items.Count; i++)
        {
            if (ckpasos20.Items[i].Selected)
                respuesta20 = "Si";
            else
                respuesta20 = "No";
        }
        //IMAGENES1
        String vNombreDepot1 = String.Empty;
        HttpPostedFile bufferDeposito1T = FUClimatizacion.PostedFile;
        byte[] vFileDeposito1 = null;
        string vExtension = string.Empty;

        if (bufferDeposito1T != null)
        {
            vNombreDepot1 = FUClimatizacion.FileName;
            Stream vStream = bufferDeposito1T.InputStream;
            BinaryReader vReader = new BinaryReader(vStream);
            vFileDeposito1 = vReader.ReadBytes((int)vStream.Length);
            vExtension = System.IO.Path.GetExtension(FUClimatizacion.FileName);
        }
        String vArchivo = String.Empty;
        if (vFileDeposito1 != null)
            vArchivo = Convert.ToBase64String(vFileDeposito1);
        /////////////////////////////////////////////////////////////////////
        //IMAGENES2
        String vNombreDepot2 = String.Empty;
        HttpPostedFile bufferDeposito2 = FUEnergia.PostedFile;
        byte[] vFileDeposito2 = null;
        string vExtension2 = string.Empty;

        if (bufferDeposito2 != null)
        {
            vNombreDepot2 = FUEnergia.FileName;
            Stream vStream2 = bufferDeposito2.InputStream;
            BinaryReader vReader2 = new BinaryReader(vStream2);
            vFileDeposito2 = vReader2.ReadBytes((int)vStream2.Length);
            vExtension2 = System.IO.Path.GetExtension(FUEnergia.FileName);
        }
        String vArchivo2 = String.Empty;
        if (vFileDeposito2 != null)
            vArchivo2 = Convert.ToBase64String(vFileDeposito2);
        /////////////////////////////////////////////////////////////////////
        string climatizacion = null;
        string energia = null;
        if (RBClima.SelectedValue == "2")
            climatizacion = "No";
        else
            climatizacion = "Si";

        if (RBEnergias.SelectedValue == "2")
            energia = "No";
        else
            energia = "Si";

        if (tipo == "2")
        {
            try
            {
                string vQuery = "STEISP_ATM_ListaVerificacion 2, '" + Session["ATM_IDMANT_VERIF_CREAR"] + "','" + respuesta1 + "','" + respuesta2 + "'," +
                "'" + respuesta3 + "','" + respuesta4 + "','" + respuesta5 + "','" + respuesta6 + "'," +
                "'" + respuesta7 + "','" + respuesta8 + "','" + respuesta9 + "','" + respuesta10 + "'," +
                "'" + respuesta11 + "','" + respuesta12 + "','" + respuesta13 + "','" + respuesta14 + "'," +
                "'" + respuesta15 + "','" + respuesta16 + "','" + respuesta17 + "','" + respuesta18 + "'," +
                "'" + respuesta19 + "','" + respuesta20 + "','" + climatizacion + "','" + vArchivo + "'," +
                "'" + energia + "','" + vArchivo2 + "','" + dropantiskimming.SelectedItem.Text + "','" + txtantiSkimming.Text + "'";
                Int32 vInfo = vConexion.ejecutarSQL(vQuery);

                if (FUClimatizacion.HasFile != false)
                {
                    string vQuery2 = "STEI_ATM_Actualizar_Imagenes 11, '" + Session["ATM_IDMANT_VERIF_CREAR"] + "','" + vArchivo + "'";
                    Int32 vInfo2 = vConexion.ejecutarSQL(vQuery2);
                }
                if (FUEnergia.HasFile != false)
                {
                    string vQuery3 = "STEI_ATM_Actualizar_Imagenes 12, '" + Session["ATM_IDMANT_VERIF_CREAR"] + "','" + vArchivo2 + "'";
                    Int32 vInfo3 = vConexion.ejecutarSQL(vQuery3);
                }

            }
            catch (Exception Ex)
            {
                throw;
            }
        }
        else
        {
            try
            {
                string vQuery = "STEISP_ATM_ListaVerificacion 1, '" + Session["ATM_IDMANT_VERIF_CREAR"] + "','" + respuesta1 + "','" + respuesta2 + "'," +
                "'" + respuesta3 + "','" + respuesta4 + "','" + respuesta5 + "','" + respuesta6 + "'," +
                "'" + respuesta7 + "','" + respuesta8 + "','" + respuesta9 + "','" + respuesta10 + "'," +
                "'" + respuesta11 + "','" + respuesta12 + "','" + respuesta13 + "','" + respuesta14 + "'," +
                "'" + respuesta15 + "','" + respuesta16 + "','" + respuesta17 + "','" + respuesta18 + "'," +
                "'" + respuesta19 + "','" + respuesta20 + "','" + climatizacion + "','" + vArchivo + "'," +
                "'" + energia + "','" + vArchivo2 + "','" + dropantiskimming.SelectedItem.Text + "','" + txtantiSkimming.Text + "'";
                Int32 vInfo = vConexion.ejecutarSQL(vQuery);
            }
            catch (Exception Ex)
            {
                throw;
            }

        }
    }


        void ImgVerificacion()
            {
            string id = Request.QueryString["id"];
            string tipo = Request.QueryString["tipo"];

            //IMAGENES1
            String vNombreDepot1 = String.Empty;
            HttpPostedFile bufferDeposito1 = FUDiscoDuro.PostedFile;
            byte[] vFileDeposito1 = null;
            string vExtension1 = string.Empty;

            if (bufferDeposito1 != null)
            {
                vNombreDepot1 = FUDiscoDuro.FileName;
                Stream vStream1 = bufferDeposito1.InputStream;
                BinaryReader vReader1 = new BinaryReader(vStream1);
                vFileDeposito1 = vReader1.ReadBytes((int)vStream1.Length);
                vExtension1 = System.IO.Path.GetExtension(FUDiscoDuro.FileName);
            }
            String vArchivo1 = String.Empty;
            if (vFileDeposito1 != null)
                vArchivo1 = Convert.ToBase64String(vFileDeposito1);
            //////////////////////////////////////////////////////////////////////////////
            //IMAGENES2
            String vNombreDepot2 = String.Empty;
            HttpPostedFile bufferDeposito2 = FUATMDesarmadoPS.PostedFile;
            byte[] vFileDeposito2 = null;
            string vExtension2 = string.Empty;

            if (bufferDeposito2 != null)
            {
                vNombreDepot2 = FUATMDesarmadoPS.FileName;
                Stream vStream2 = bufferDeposito2.InputStream;
                BinaryReader vReader2 = new BinaryReader(vStream2);
                vFileDeposito2 = vReader2.ReadBytes((int)vStream2.Length);
                vExtension2 = System.IO.Path.GetExtension(FUATMDesarmadoPS.FileName);
            }
            String vArchivo2 = String.Empty;
            if (vFileDeposito2 != null)
                vArchivo2 = Convert.ToBase64String(vFileDeposito2);
            ////////////////////////////////////////////////////////////////////////////////
            //IMAGENES3
            String vNombreDepot3 = String.Empty;
            HttpPostedFile bufferDeposito3 = FUATMDesarmadoPI.PostedFile;
            byte[] vFileDeposito3 = null;
            string vExtension3 = string.Empty;

            if (bufferDeposito3 != null)
            {
                vNombreDepot3 = FUATMDesarmadoPI.FileName;
                Stream vStream3 = bufferDeposito3.InputStream;
                BinaryReader vReader3 = new BinaryReader(vStream3);
                vFileDeposito3 = vReader3.ReadBytes((int)vStream3.Length);
                vExtension3 = System.IO.Path.GetExtension(FUATMDesarmadoPI.FileName);
            }
            String vArchivo3 = String.Empty;
            if (vFileDeposito3 != null)
                vArchivo3 = Convert.ToBase64String(vFileDeposito3);
            ////////////////////////////////////////////////////////////////////////////////
            //IMAGENES4
            String vNombreDepot4 = String.Empty;
            HttpPostedFile bufferDeposito4 = FUDispositivoVendor.PostedFile;
            byte[] vFileDeposito4 = null;
            string vExtension4 = string.Empty;

            if (bufferDeposito4 != null)
            {
                vNombreDepot4 = FUDispositivoVendor.FileName;
                Stream vStream4 = bufferDeposito4.InputStream;
                BinaryReader vReader4 = new BinaryReader(vStream4);
                vFileDeposito4 = vReader4.ReadBytes((int)vStream4.Length);
                vExtension4 = System.IO.Path.GetExtension(FUDispositivoVendor.FileName);
            }
            String vArchivo4 = String.Empty;
            if (vFileDeposito4 != null)
                vArchivo4 = Convert.ToBase64String(vFileDeposito4);
            ////////////////////////////////////////////////////////////////////////////////
            //IMAGENES5
            String vNombreDepot5 = String.Empty;
            HttpPostedFile bufferDeposito5 = FUSYSTEMINFO.PostedFile;
            byte[] vFileDeposito5 = null;
            string vExtension5 = string.Empty;

            if (bufferDeposito5 != null)
            {
                vNombreDepot5 = FUSYSTEMINFO.FileName;
                Stream vStream5 = bufferDeposito5.InputStream;
                BinaryReader vReader5 = new BinaryReader(vStream5);
                vFileDeposito5 = vReader5.ReadBytes((int)vStream5.Length);
                vExtension5 = System.IO.Path.GetExtension(FUSYSTEMINFO.FileName);
            }
            String vArchivo5 = String.Empty;
            if (vFileDeposito5 != null)
                vArchivo5 = Convert.ToBase64String(vFileDeposito5);
            ////////////////////////////////////////////////////////////////////////////////
            //IMAGENES6
            String vNombreDepot6 = String.Empty;
            HttpPostedFile bufferDeposito6 = FUAntiskimmin.PostedFile;
            byte[] vFileDeposito6 = null;
            string vExtension6 = string.Empty;

            if (bufferDeposito6 != null)
            {
                vNombreDepot6 = FUAntiskimmin.FileName;
                Stream vStream6 = bufferDeposito6.InputStream;
                BinaryReader vReader6 = new BinaryReader(vStream6);
                vFileDeposito6 = vReader6.ReadBytes((int)vStream6.Length);
                vExtension6 = System.IO.Path.GetExtension(FUAntiskimmin.FileName);
            }
            String vArchivo6 = String.Empty;
            if (vFileDeposito6 != null)
                vArchivo6 = Convert.ToBase64String(vFileDeposito6);
            ////////////////////////////////////////////////////////////////////////////////
            //IMAGENES7
            String vNombreDepot7 = String.Empty;
            HttpPostedFile bufferDeposito7 = FUMonitorFiltro.PostedFile;
            byte[] vFileDeposito7 = null;
            string vExtension7 = string.Empty;

            if (bufferDeposito7 != null)
            {
                vNombreDepot7 = FUMonitorFiltro.FileName;
                Stream vStream7 = bufferDeposito7.InputStream;
                BinaryReader vReader7 = new BinaryReader(vStream7);
                vFileDeposito7 = vReader7.ReadBytes((int)vStream7.Length);
                vExtension7 = System.IO.Path.GetExtension(FUMonitorFiltro.FileName);
            }
            String vArchivo7 = String.Empty;
            if (vFileDeposito7 != null)
                vArchivo7 = Convert.ToBase64String(vFileDeposito7);
            ////////////////////////////////////////////////////////////////////////////////
            //IMAGENES8
            String vNombreDepot8 = String.Empty;
            HttpPostedFile bufferDeposito8 = FUPadlewheel.PostedFile;
            byte[] vFileDeposito8 = null;
            string vExtension8 = string.Empty;

            if (bufferDeposito8 != null)
            {
                vNombreDepot8 = FUPadlewheel.FileName;
                Stream vStream8 = bufferDeposito8.InputStream;
                BinaryReader vReader8 = new BinaryReader(vStream8);
                vFileDeposito8 = vReader8.ReadBytes((int)vStream8.Length);
                vExtension8 = System.IO.Path.GetExtension(FUPadlewheel.FileName);
            }
            String vArchivo8 = String.Empty;
            if (vFileDeposito8 != null)
                vArchivo8 = Convert.ToBase64String(vFileDeposito8);
            ////////////////////////////////////////////////////////////////////////////////
            //IMAGENES9
            String vNombreDepot9 = String.Empty;
            HttpPostedFile bufferDeposito9 = FUDispDesarmado.PostedFile;
            byte[] vFileDeposito9 = null;
            string vExtension9 = string.Empty;

            if (bufferDeposito9 != null)
            {
                vNombreDepot9 = FUDispDesarmado.FileName;
                Stream vStream9 = bufferDeposito9.InputStream;
                BinaryReader vReader9 = new BinaryReader(vStream9);
                vFileDeposito9 = vReader9.ReadBytes((int)vStream9.Length);
                vExtension9 = System.IO.Path.GetExtension(FUDispDesarmado.FileName);
            }
            String vArchivo9 = String.Empty;
            if (vFileDeposito9 != null)
                vArchivo9 = Convert.ToBase64String(vFileDeposito9);
            ////////////////////////////////////////////////////////////////////////////////
            //IMAGENES10
            String vNombreDepot10 = String.Empty;
            HttpPostedFile bufferDeposito10 = FUTeclado.PostedFile;
            byte[] vFileDeposito10 = null;
            string vExtension10 = string.Empty;

            if (bufferDeposito10 != null)
            {
                vNombreDepot10 = FUTeclado.FileName;
                Stream vStream10 = bufferDeposito10.InputStream;
                BinaryReader vReader10 = new BinaryReader(vStream10);
                vFileDeposito10 = vReader10.ReadBytes((int)vStream10.Length);
                vExtension10 = System.IO.Path.GetExtension(FUTeclado.FileName);
            }
            String vArchivo10 = String.Empty;
            if (vFileDeposito10 != null)
                vArchivo10 = Convert.ToBase64String(vFileDeposito10);
            ////////////////////////////////////////////////////////////////////////////////
            //IMAGENES11
            String vNombreDepot11 = String.Empty;
            HttpPostedFile bufferDeposito11 = FUATMLinea.PostedFile;
            byte[] vFileDeposito11 = null;
            string vExtension11 = string.Empty;

            if (bufferDeposito11 != null)
            {
                vNombreDepot11 = FUATMLinea.FileName;
                Stream vStream11 = bufferDeposito11.InputStream;
                BinaryReader vReader11 = new BinaryReader(vStream11);
                vFileDeposito11 = vReader11.ReadBytes((int)vStream11.Length);
                vExtension11 = System.IO.Path.GetExtension(FUATMLinea.FileName);
            }
            String vArchivo11 = String.Empty;
            if (vFileDeposito11 != null)
                vArchivo11 = Convert.ToBase64String(vFileDeposito11);

            if (tipo == "2")
            {
                try
                {
                    if (FUDiscoDuro.HasFile != false)
                    {
                        string vQuery = "STEI_ATM_Actualizar_Imagenes 1, '" + Session["ATM_IDMANT_VERIF_CREAR"] + "','" + vArchivo1 + "'";
                        Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    }
                    if (FUATMDesarmadoPS.HasFile != false)
                    {
                        string vQuery = "STEI_ATM_Actualizar_Imagenes 2, '" + Session["ATM_IDMANT_VERIF_CREAR"] + "','" + vArchivo2 + "'";
                        Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    }
                    if (FUATMDesarmadoPI.HasFile != false)
                    {
                        string vQuery = "STEI_ATM_Actualizar_Imagenes 3, '" + Session["ATM_IDMANT_VERIF_CREAR"] + "','" + vArchivo3 + "'";
                        Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    }
                    if (FUDispositivoVendor.HasFile != false)
                    {
                        string vQuery = "STEI_ATM_Actualizar_Imagenes 4, '" + Session["ATM_IDMANT_VERIF_CREAR"] + "','" + vArchivo4 + "'";
                        Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    }
                    if (FUSYSTEMINFO.HasFile != false)
                    {
                        string vQuery = "STEI_ATM_Actualizar_Imagenes 5, '" + Session["ATM_IDMANT_VERIF_CREAR"] + "','" + vArchivo5 + "'";
                        Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    }
                    if (FUAntiskimmin.HasFile != false)
                    {
                        string vQuery = "STEI_ATM_Actualizar_Imagenes 6, '" + Session["ATM_IDMANT_VERIF_CREAR"] + "','" + vArchivo6 + "'";
                        Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    }
                    if (FUMonitorFiltro.HasFile != false)
                    {
                        string vQuery = "STEI_ATM_Actualizar_Imagenes 7, '" + Session["ATM_IDMANT_VERIF_CREAR"] + "','" + vArchivo7 + "'";
                        Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    }
                    if (FUPadlewheel.HasFile != false)
                    {
                        string vQuery = "STEI_ATM_Actualizar_Imagenes 8, '" + Session["ATM_IDMANT_VERIF_CREAR"] + "','" + vArchivo8 + "'";
                        Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    }
                    if (FUDispDesarmado.HasFile != false)
                    {
                        string vQuery = "STEI_ATM_Actualizar_Imagenes 9, '" + Session["ATM_IDMANT_VERIF_CREAR"] + "','" + vArchivo9 + "'";
                        Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    }
                    if (FUTeclado.HasFile != false)
                    {
                        string vQuery = "STEI_ATM_Actualizar_Imagenes 10, '" + Session["ATM_IDMANT_VERIF_CREAR"] + "','" + vArchivo10 + "'";
                        Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    }
                    if (FUATMLinea.HasFile != false)
                    {
                        string vQuery = "STEI_ATM_Actualizar_Imagenes 13, '" + Session["ATM_IDMANT_VERIF_CREAR"] + "','" + vArchivo11 + "'";
                        Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    }
                }
                catch (Exception Ex)
                {
                    throw;
                }
            }
            else
            {
                try
                {
                    string vQuery = "STEISP_ATM_ImagenesVerif 1, '" + Session["ATM_IDMANT_VERIF_CREAR"] + "','" + vArchivo1 + "','" + vArchivo2 + "','" + vArchivo3 + "'," +
                        "'" + vArchivo4 + "','" + vArchivo5 + "','" + vArchivo6 + "','" + vArchivo7 + "','" + vArchivo8 + "','" + vArchivo9 + "', '" + vArchivo10 + "','" + vArchivo11 + "'";
                    Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    if (vInfo == 1)
                    {

                    }

                }
                catch (Exception Ex)
                {
                    throw;
                }
            }
        }

        protected void dropantiskimming_TextChanged(object sender, EventArgs e)
            {

            try
            {
                if (dropantiskimming.SelectedValue == "1")
                {
                    txtantiSkimming.Enabled = true;
                }
                else
                {
                    txtantiSkimming.Enabled = false;
                    txtantiSkimming.Text = string.Empty;
                }
            }
            catch (Exception)
            {

            }
        }      


        protected void btnmodal_Click(object sender, EventArgs e)
            {

               ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);

            }

        void CorreoSuscripcion()
        {
            string vEstado = "";
            DataTable vDatos = new DataTable();
            String vQuery = "STEISP_ATM_Generales 36,'" + Session["ATM_COD_VERIF"] + "'";
            vDatos = vConexion.ObtenerTabla(vQuery);
            foreach (DataRow item in vDatos.Rows)
            {
                vEstado = item["estadoMantenimiento"].ToString();
            }

            if (vEstado == "7")
            {
                string vCorreoEncargadoZona = "";
                if (Convert.ToString(Session["ATM_IDZONA_VERIF_CREAR"]) == "1")
                    vCorreoEncargadoZona = "emontoya@bancatlan.hn";
                if (Convert.ToString(Session["ATM_IDZONA_VERIF_CREAR"]) == "2")
                    vCorreoEncargadoZona = "jdgarcia@bancatlan.hn";
                if (Convert.ToString(Session["ATM_IDZONA_VERIF_CREAR"]) == "3")
                    vCorreoEncargadoZona = "acalderon@bancatlan.hn";

                string vQueryD = "STEISP_ATM_Generales 33,'" + Session["ATM_USUARIO_VERIF_CREAR"] + "'";
                DataTable vDatosTecnicoResponsable = vConexion.ObtenerTabla(vQueryD);
                string vQueryTecnicos = "STEISP_ATM_Generales 39,'" + Session["ATM_COD_VERIF"] + "'";
                DataTable vDatosTecnicos = vConexion.ObtenerTabla(vQueryTecnicos);
                string vQueryJefes = "STEISP_ATM_Generales 38,'" + Session["ATM_COD_VERIF"] + "'";
                DataTable vDatosJefeAgencias = vConexion.ObtenerTabla(vQueryJefes);

                string vCorreosTecnicos = "";
                string vCorreosJefes = "";
                string vCorreosTodos = "";
                string vCorreoResponsable = "";

                for (int i = 0; i < vDatosTecnicoResponsable.Rows.Count; i++)
                {
                    vCorreoResponsable = vDatosTecnicoResponsable.Rows[i]["correo"].ToString();
                }
                for (int i = 0; i < vDatosTecnicos.Rows.Count; i++)
                {
                    string vCorreo = vDatosTecnicos.Rows[i]["correo"].ToString() + ";";
                    vCorreosTecnicos = vCorreosTecnicos + vCorreo;
                    if (vCorreosTecnicos == ";")
                        vCorreosTecnicos = "";
                }
                for (int i = 0; i < vDatosJefeAgencias.Rows.Count; i++)
                {
                    string vCorreo = vDatosJefeAgencias.Rows[i]["correoJefe"].ToString() + ";";
                    vCorreosJefes = vCorreosJefes + vCorreo;
                    if (vCorreosJefes == ";")
                        vCorreosJefes = "";
                }

                string vReporteViaticos = "Verificacion";
                string vCorreoAdmin = "acedillo@bancatlan.hn";
                //vCorreosTodos = vCorreosTecnicos + vCorreosJefes + vCorreoAdmin;
                vCorreosTodos = vCorreosTecnicos + vCorreosJefes + vCorreoResponsable;
                //string vCorreoCopia = "acamador@bancatlan.hn";
                string vCorreoCopia = "eurrea@bancatlan.hn;unidadatmkiosco@bancatlan.hn;" + vCorreoEncargadoZona;
                string vAsuntoRV = "Lista de Verificación";
                string vBody = "Lista de Verificación";
                int vEstadoSuscripcion = 0;
                string vQueryRep = "STEISP_ATM_Generales 35, '" + vReporteViaticos + "','" + vCorreosTodos + "','" + vCorreoCopia + "','" + vAsuntoRV + "','" + vBody + "','" + vEstadoSuscripcion + "','" + Session["ATM_COD_VERIF"] + "'";
                vConexion.ejecutarSQL(vQueryRep);
            }
        }

        protected void btnModalVerif_Click(object sender, EventArgs e)
            {
            if (Convert.ToString(Session["vConfirmar"]) == "0")
            {
                Session["vConfirmar"] = "1";
                try
                {
                    string id = Request.QueryString["id"];
                    string tipo = Request.QueryString["tipo"];

                    if (tipo == "4")
                    {

                        try
                        {
                            string vQuery = "STEISP_ATM_VERIFICACION 5, '" + Session["USUARIO"].ToString() + "','" + Session["ATM_IDMANT_VERIF_CREAR"] + "'";
                            Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                            if (vInfo == 1)
                            {
                                //EnviarCorreo();
                                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
                                Mensaje("Lista de verificación aprobada con éxito", WarningType.Success);
                                EnviarCorreo();
                                CorreoSuscripcion();
                                vaciarValorImg();
                                Session["vConfirmar"] = "0";
                                Response.Redirect("buscarAprobarVerificacion.aspx");
                            }
                            else
                            {
                                Mensaje("No se pudo aprobar lista de verificación", WarningType.Warning);
                            }
                        }
                        catch (Exception Ex)
                        {
                            throw;
                        }
                    }
                    else
                    {
                        ActualizarATM();
                        ActualizarMateriales();
                        ActualizarVerifATM();
                        ImgVerificacion();
                        PreguntasVerif();
                        EnviarCorreo();
                        vaciarValorImg();
                        Session["vConfirmar"] = "0";
                        if (tipo == "2")
                        {
                            Response.Redirect("../../pages/devolver/rechazados.aspx");
                        }
                        else
                        {
                            Response.Redirect("buscarVerificacion.aspx");
                        }
                    }
                }
                catch (Exception Ex)
                {
                    Mensaje(Ex.Message, WarningType.Danger);
                }
            }

        }

        protected void btnModalCerrarVerif_Click(object sender, EventArgs e)
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
            }

        protected void btnRechazarVerif_Click(object sender, EventArgs e)
            {
            txtAlerta2.Visible = false;
            H5Alerta.Visible = false;
            txtmotivoRechazo.Text = string.Empty;
            H5Alerta.Visible = false;
            lbcodATM2.Text = txtcodATM.Text;
            lbNombreATM2.Text = txtnomATM.Text;
            lbsucursalATM2.Text = txtsucursal.Text;
            lbInventarioATM2.Text = txtinventarioVerif.Text;
            lbtecnico2.Text = txtTecnicoResponsable.Text;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal2();", true);
        }

        void DevolverCorreo()
        {
            string tipo = Request.QueryString["tipo"];
            SmtpService vService = new SmtpService();

            string vCorreoEncargadoZona = "";
            if (Convert.ToString(Session["ATM_IDZONA_VERIF_CREAR"]) == "1")
                vCorreoEncargadoZona = "emontoya@bancatlan.hn";
            if (Convert.ToString(Session["ATM_IDZONA_VERIF_CREAR"]) == "2")
                vCorreoEncargadoZona = "jdgarcia@bancatlan.hn";
            if (Convert.ToString(Session["ATM_IDZONA_VERIF_CREAR"]) == "3")
                vCorreoEncargadoZona = "acalderon@bancatlan.hn";

            string vQueryD = "STEISP_ATM_Generales 33,'" + Session["ATM_USUARIO_VERIF_CREAR"] + "'";
            DataTable vDatosTecnicoResponsable = vConexion.ObtenerTabla(vQueryD);
            DataTable vDatos = (DataTable)Session["AUTHCLASS"];
            string vQueryTecnicos = "STEISP_ATM_Generales 39,'" + Session["ATM_IDMANT_VERIF_CREAR"] + "'";
            DataTable vDatosTecnicos = vConexion.ObtenerTabla(vQueryTecnicos);
            string vQueryJefes = "STEISP_ATM_Generales 38,'" + Session["ATM_IDMANT_VERIF_CREAR"] + "'";
            DataTable vDatosJefeAgencias = vConexion.ObtenerTabla(vQueryJefes);

                if (vDatos.Rows.Count > 0)
                {
                    foreach (DataRow item in vDatos.Rows)
                    {
                        //ENVIAR A JEFE
                        if (!item["correo"].ToString().Trim().Equals(""))
                        {
                            vService.EnviarMensaje(
                            item["correo"].ToString(),
                            typeBody.ATM,
                            "Notificación de Mantenimiento ATM",
                            "Buen día, se le notifica que ha devuelto solicitud de mantenimiento, el encargado es " + vDatosTecnicoResponsable.Rows[0]["nombre"].ToString() + ", mantenimiento al ATM " + txtnomATM.Text,
                            "El usuario <b>" + item["Nombre"].ToString() + "</b> devolvió: <br> Verificación de Mantenimiento<br><br> Motivo= "+txtmotivoRechazo.Text,
                             "",
                             "/pages/devolver/rechazados.aspx"
                            );

                            //vFlagEnvioSupervisor = true;
                        }
                        //ENVIAR A EDWIN
                        //string vNombre = "EDWIN ALBERTO URREA PENA";
                        vService.EnviarMensaje(ConfigurationManager.AppSettings["STEIMail"],
                                typeBody.ATM,
                                "Notificación de Mantenimiento ATM",
                                   "Buen día, se le notifica que ha devuelto solicitud de mantenimiento, el encargado es " + vDatosTecnicoResponsable.Rows[0]["nombre"].ToString() + ", mantenimiento al ATM " + txtnomATM.Text,
                                "El usuario <b>" + item["Nombre"].ToString() + "</b> aprobó: <br> Verificación de Mantenimiento<br><br> Motivo= " + txtmotivoRechazo.Text,
                                  vCorreoEncargadoZona,
                                  "/sites/ATM/pages/devolver/rechazados.aspx"
                                );
                        //ENVIAR A TECNICOS ASIGNADOS
                        //string vNombreJefe = "ELVIS ALEXANDER MONTOYA PEREIRA";

                    }
                }
                if (vDatosTecnicoResponsable.Rows.Count > 0)
                {
                    foreach (DataRow item in vDatosTecnicoResponsable.Rows)
                    {
                        //ENVIAR A RESPONSABLE
                        vService.EnviarMensaje(item["correo"].ToString(),
                            typeBody.ATM,
                           "Notificación de Mantenimiento ATM",
                            "Buen día, se le notifica que se ha devuelto solicitud de mantenimiento, el encargado es " + item["nombre"].ToString() + ", mantenimiento al ATM " + txtnomATM.Text,
                              "El usuario <b>" + vDatos.Rows[0]["Nombre"].ToString() + "</b> devolvió: <br> Verificación de Mantenimiento de ATM al que ha sido asignado como responsable.<br><br> Motivo= " + txtmotivoRechazo.Text,
                              "",
                             "/sites/ATM/pages/devolver/rechazados.aspx"
                            );
                    }
                }              
        }

        protected void btnRechazarModal_Click(object sender, EventArgs e)
            {
            if (txtmotivoRechazo.Text == "" || txtmotivoRechazo.Text == string.Empty)
            {
                txtAlerta2.Visible = true;
                H5Alerta.Visible = true;
            }
            else
            {

                try
                {
                    
                    string vQuery = "STEISP_ATM_VERIFICACION 6, '" + Session["USUARIO"].ToString() + "','" + Session["ATM_IDMANT_VERIF_CREAR"].ToString() + "'";
                    Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    if (vInfo == 1)
                    {
                        DevolverCorreo();
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
                        Mensaje("Lista de verificación fue rechazada con éxito", WarningType.Success);
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
            txtAlerta2.Visible = false;
            H5Alerta.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal2();", true);
            }

        protected void btnborrar_Click(object sender, EventArgs e)
            {
                string vImagen11 = Session["ATM_VERIF_IMG21"].ToString();
                string srcImgen11 = "data:image;base64," + vImagen11;
                imgClimatizacion.Src = null;

            }       

        protected void btnBorrar1_Click(object sender, EventArgs e)
            {           
                imgEnergia.Src = string.Empty;
            }

        protected void btnEnviarVerif_Click(object sender, EventArgs e)
        {
            try
            {
                validar();
               
                lbcodATM.Text = txtcodATM.Text;
                lbNombreATM.Text = txtnomATM.Text;
                lbsucursalATM.Text = txtsucursal.Text;
                lbInventarioATM.Text = txtinventarioVerif.Text;
                lbtecnico.Text = txtTecnicoResponsable.Text;
                Session["vConfirmar"] = "0";
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception Ex)
            {

                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        protected void GVNewMateriales_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ver")
            {
                String vIDStock = e.CommandArgument.ToString();
                String vMantenimiento = Session["ATM_IDMANT_VERIF_CREAR"].ToString();
                txtAlerta3.Visible = false;
                Session["vNombreMaterial"] = null;
                Session["vCantidadMaterial"] = null;
                Session["vStockMaterial"] = null;
                txtUsadoModal.Text = "";

                String vQuery = "STEISP_ATM_VERIFICACION 14,'"+vMantenimiento+"','"+vIDStock+"'";
                DataTable vDatos = vConexion.ObtenerTabla(vQuery);
                foreach (DataRow item in vDatos.Rows)
                {
                    Session["vNombreMaterial"] = item["nombre"].ToString();
                    Session["vCantidadMaterial"] = item["cantidad"].ToString();
                    Session["vStockMaterial"] = item["idStock"].ToString();
                }
                titulo.InnerText = "¿Cuanto material de " + Session["vNombreMaterial"] + " utilizó?";
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal3();", true);
            }
        }

        protected void btnModarDevolverMaterial_Click(object sender, EventArgs e)
        {
            if (txtUsadoModal.Text == "")
            {
                txtAlerta3.Visible = true;
            }
            else
            {
                txtAlerta3.Visible = false;
                llenarGridMateriales();
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal3();", true);
            }
            
        }

        protected void btnModalCerrarMaterial_Click(object sender, EventArgs e)
        {
            Session["vNombreMaterial"] = null;
            Session["vCantidadMaterial"] = null;
            Session["vStockMaterial"] = null;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal3();", true);
        }

        void llenarGridMateriales()
        {
           
                try
                {

                    DataTable vData = new DataTable();
                    DataTable vDatos = (DataTable)Session["ATM_DEVOLVER_MATERIALES_VERIF"];
                    String vIdMantenimiento = Convert.ToString(Session["ATM_IDMANT_VERIF_CREAR"]).ToString();
                    String vStock = Convert.ToString(Session["vStockMaterial"]).ToString();
                    String vMaterial = Convert.ToString(Session["vNombreMaterial"]).ToString();
                    String vSolicitado = Convert.ToString(Session["vCantidadMaterial"]).ToString();
                    String vUtilizado = txtUsadoModal.Text;
               
                    vData.Columns.Add("idMantenimiento");
                    vData.Columns.Add("idStock");
                    vData.Columns.Add("nombre");
                    vData.Columns.Add("cantidad");
                    vData.Columns.Add("devolver");

                    if (vDatos == null)
                        vDatos = vData.Clone();

                    if (vDatos != null)
                    {
                        if (vDatos.Rows.Count < 1)
                        {
                            vDatos.Rows.Add(vIdMantenimiento, vStock, vMaterial, vSolicitado, vUtilizado);
                        }
                        else
                        {
                            //string vTotalCantidad = Session["STOCK_CANTIDAD_ATM"].ToString();
                            Boolean vRegistered = false;
                            for (int i = 0; i < vDatos.Rows.Count; i++)
                            {
                                String vExistente = Convert.ToString(vDatos.Rows[i]["idStock"].ToString());
                                if (vStock == vExistente)
                                {
                                    vRegistered = true;
                                    DataRow[] result = vDatos.Select("idStock = '" + vStock + "'");
                                    foreach (DataRow row in result)
                                    {
                                    int vCantidadExistente = Convert.ToInt32(vDatos.Rows[i]["cantidad"].ToString());
                                    if (Convert.ToInt32(txtUsadoModal.Text) > vCantidadExistente)
                                    {
                                        throw new Exception("No puede ingresar una cantidad mayor al número solicitado.");
                                    }
                                    else
                                    {
                                        if (row["idStock"].ToString().Contains(Convert.ToString(vStock)))
                                        {
                                            vDatos.Rows.Remove(row);
                                        }
                                    }
                                    }
                                    vDatos.Rows.Add(vIdMantenimiento, vStock, vMaterial, vSolicitado, vUtilizado);
                                }

                            }

                            if (!vRegistered)
                                vDatos.Rows.Add(vIdMantenimiento, vStock, vMaterial, vSolicitado, vUtilizado);
                        }

                        GVNewMateriales.DataSource = vDatos;
                        GVNewMateriales.DataBind();
                        Session["ATM_DEVOLVER_MATERIALES_VERIF"] = vDatos;
                        //DDLOcurrencia.SelectedValue = "0";
                        //txtVulnerable.Text = "";
                        //Session["IDTVulnerable"] = "";

                    }
                }
                catch (Exception Ex)
                {
                    Mensaje(Ex.Message, WarningType.Danger);
                }
           
        }
    }
}