using Infatlan_STEI_ATM.clases;
using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Infatlan_STEI_ATM.pages.correctivo
{
    public partial class mantCorrectivoVerificacion : System.Web.UI.Page
    {
        bd vConexion = new bd();
        bd vConexionATM = new bd();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["VERIFCorrectivo"] = null;
                if (Convert.ToBoolean(Session["AUTH"]))
                {
                    CargarATM();
                    cargarData();
                    string tipo = Request.QueryString["tipo"];
                    switch (tipo)
                    {
                        case "2":
                            aprobacionCampos();
                            llenarForm();
                            llenarImagenes();
                            btnEnviarVerif.Text = "Aprobar verificación";
                            break;
                        case "3":
                            llenarForm();
                            llenarImagenes();
                            if (DDLCambioPiezas.SelectedValue == "1")
                                txtCambioMateriales.Enabled = true;
                            if (dropantiskimming.SelectedValue == "1")
                                txtantiSkimming.Enabled = true;
                            break;
                    }
                }
                else
                {
                    Response.Redirect("/login.aspx");
                }
            }
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

        void aprobacionCampos()
        {
            btnRechazarVerif.Visible = true;
            txtlatitudATM.Enabled = false;
            txtlongitudATM.Enabled = false;
            DDLSo.Enabled = false;
            DDLVersionSW.Enabled = false;
            txtHllegadaInfatlan.Enabled = false;
            txthsalidaInfa.Enabled = false;
            TxFechaInicio.Enabled = false;
            TxFechaRegreso.Enabled = false;
            dropantiskimming.Enabled = false;
            txtantiSkimming.Enabled = false;
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
            FUPickerLimpio.Enabled = false;
            FUPresentadorLimpio.Enabled = false;
            FUBandas.Enabled = false;
            FUATMSinDesarmarPS.Enabled = false;
            FUmapaATM.Enabled = false;
            DDLCambioDiscoDuro.Enabled = false;
            DDLCambioPiezas.Enabled = false;
            txtCambioMateriales.Enabled = false;
        }

        void llenarForm()
        {
            string ID = Request.QueryString["cod"];

            DataTable vDatos = new DataTable();
            vDatos = vConexion.ObtenerTabla("STEISP_ATM_GeneralesCorrectivo 6,'" + ID + "'");
            foreach (DataRow item in vDatos.Rows)
            {
                TxFechaInicio.Text = item["HrInicio"].ToString();
                TxFechaRegreso.Text = item["HrFin"].ToString();
                txtHllegadaInfatlan.Text = item["HrEntradaInfa"].ToString();
                txthsalidaInfa.Text = item["HrSalidaInfa"].ToString();
                dropantiskimming.SelectedValue = item["antiskimming"].ToString();
                txtantiSkimming.Text = item["ComentarioAntis"].ToString();
                DDLCambioPiezas.SelectedValue = item["piezas"].ToString();
                txtCambioMateriales.Text = item["ComentarioPiezas"].ToString();
                DDLCambioDiscoDuro.SelectedValue = item["DiscoDuro"].ToString();
                txtobseracionesVerif.Text = item["Comentario"].ToString();
            }

            if (DDLCambioDiscoDuro.SelectedValue == "0")
            {
                TRDiscoDuro.Visible = false;
                TRPicker.Visible = true;
                TRPresentador.Visible = true;
                TRParteSuperior.Visible = true;
                TRBandas.Visible = true;
            }
            else if (DDLCambioDiscoDuro.SelectedValue == "2")
            {
                TRDiscoDuro.Visible = true;
                TRPicker.Visible = true;
                TRPresentador.Visible = true;
                TRParteSuperior.Visible = true;
                TRBandas.Visible = true;
            }
            else
            {
                TRDiscoDuro.Visible = true;
                TRPicker.Visible = false;
                TRPresentador.Visible = false;
                TRParteSuperior.Visible = false;
                TRBandas.Visible = false;
            }
        }

        void llenarImagenes()
        {
            string ID = Request.QueryString["cod"];

            DataTable vDatos = new DataTable();
            vDatos = vConexion.ObtenerTabla("STEISP_ATM_NotificacionCorrectivo 14,'" + ID + "'");
            foreach (DataRow item in vDatos.Rows)
            {
                if (DDLCambioDiscoDuro.SelectedValue == "0" || DDLCambioDiscoDuro.SelectedValue == "2")
                {
                    string vImagen1 = item["imgPicker"].ToString();
                    string srcImgen1 = "data:image;base64," + vImagen1;
                    imgPickerLimpio.Src = srcImgen1;
                    HFPicker.Value = "si";

                    string vImagen2 = item["imgPresentador"].ToString();
                    string srcImgen2 = "data:image;base64," + vImagen2;
                    imgPresentadorLimpio.Src = srcImgen2;
                    HFPresentador.Value = "si";

                    string vImagen3 = item["imgSuperiorSinDesarmar"].ToString();
                    string srcImgen3 = "data:image;base64," + vImagen3;
                    imgTMSinDesarmarPS.Src = srcImgen3;
                    HFATMSinDesarmar.Value = "si";

                    string vImagen4 = item["imgBandas"].ToString();
                    string srcImgen4 = "data:image;base64," + vImagen4;
                    imgBandas.Src = srcImgen4;
                    HFBandas.Value = "si";

                    if (DDLCambioDiscoDuro.SelectedValue == "2")
                    {
                        string vImagen5 = item["imgDiscoDuro"].ToString();
                        string srcImgen5 = "data:image;base64," + vImagen5;
                        imgDiscoDuro.Src = srcImgen5;
                        HFDiscoDuro.Value = "si";
                    }
                }

                else
                {
                    string vImagen5 = item["imgDiscoDuro"].ToString();
                    string srcImgen5 = "data:image;base64," + vImagen5;
                    imgDiscoDuro.Src = srcImgen5;
                    HFDiscoDuro.Value = "si";
                }

            }

            //UPIMG.Update();
        }

        void cargarData()
        {
            try
            {
                string ID = Request.QueryString["cod"];

                DataTable vDatos = new DataTable();
                vDatos = vConexion.ObtenerTabla("STEISP_ATM_GeneralesCorrectivo 3,'" + ID + "'");
                foreach (DataRow item in vDatos.Rows)
                {
                    Session["ATM_CORRECTIVO"] = item["Codigo"].ToString();
                    txtnomATM.Text = item["Codigo"].ToString() + " - " + item["NomATM"].ToString();
                    txtdireccion.Text = item["Direccion"].ToString();
                    txtip.Text = item["IP"].ToString();
                    txtpuertoVerif.Text = item["Puerto"].ToString();
                    DDLtipoTeclado.SelectedIndex = CargarInformacionDDL(DDLtipoTeclado, item["Teclado"].ToString());
                    DDLtipoProc.SelectedValue = item["Procesador"].ToString();
                    DDLtipoCargaVerif.SelectedValue = item["TipoCarga"].ToString();
                    DDLmarcaDiscoDuro.SelectedValue = item["Marca"].ToString();
                    txtSerieDiscoDuro.Text = item["SerieDisco"].ToString();
                    txtserieATM.Text = item["SerieATM"].ToString();

                    string MyString = item["CapacidadDisco"].ToString();
                    char[] MyChar = { 'G', 'B', ' ' };
                    string NewString = MyString.TrimEnd(MyChar);

                    txtcapacidadDiscoVerif.Text = NewString;

                    string MyString2 = item["Ram"].ToString();
                    char[] MyChar2 = { 'G', 'B', ' ' };
                    string NewString2 = MyString2.TrimEnd(MyChar2);

                    txtramVerif.Text = NewString2;

                    txtinventarioVerif.Text = item["Inventario"].ToString();
                    txtlatitudATM.Text = item["Latitud"].ToString();
                    txtlongitudATM.Text = item["Longitud"].ToString();
                    txtUbicacionATM.Text = item["Ubicacion"].ToString();
                    //item["IdUbi"].ToString();
                    txtsucursal.Text = item["Sucursal"].ToString();
                    //Session["ATM_DEPTO_VERIF_CREAR"] = item["Departamento"].ToString();
                    txtzonaVerif.Text = item["Zona"].ToString();
                    //Session["ATM_IDZONA_VERIF_CREAR"] = item["IDZona"].ToString();
                    //Session["ATM_IDMANT_VERIF_CREAR"] = codVerif;
                    //Session["ATM_ESTADO_VERIF_CREAR"] = item["Estado"].ToString();

                    //Session["ATM_HRINICIO_VERIF_CREAR"] = item["HrInicio"].ToString();
                    //Session["ATM_HRFIN_VERIF_CREAR"] = item["HrFin"].ToString();
                    //Session["ATM_AUTORIZADO_VERIF_CREAR"] = item["Autorizado"].ToString();
                    txtsysaid.Text = item["SysAid"].ToString();
                    txtTecnicoResponsable.Text = item["Tecnico"].ToString();
                    //Session["ATM_USUARIO_VERIF_CREAR"] = item["Usuario"].ToString();
                    txtidentidad.Text = item["Identidad"].ToString();
                    DDLSo.SelectedValue = item["SO"].ToString();
                    DDLVersionSW.SelectedValue = item["VersionSw"].ToString();

                    DataTable vDatos2 = new DataTable();
                    vDatos2 = vConexion.ObtenerTabla("STEISP_ATM_GeneralesCorrectivo 4,'" + item["Codigo"].ToString() + "'");
                    foreach (DataRow item2 in vDatos2.Rows)
                    {
                        //IMAGEN1
                        string vImagen1 = item2["imgMapaATM"].ToString();
                        string srcImgen1 = "data:image;base64," + vImagen1;
                        if (vImagen1 == "")
                            imgMapaATM.Src = "../../assets/images/vistaPrevia1.JPG";
                        else
                        {
                            imgMapaATM.Src = srcImgen1;
                            HFMapa.Value = "si";
                        }
                    }

                }


                if (DDLCambioDiscoDuro.SelectedValue == "0")
                {
                    TRDiscoDuro.Visible = false;
                    TRPicker.Visible = true;
                    TRPresentador.Visible = true;
                    TRParteSuperior.Visible = true;
                    TRBandas.Visible = true;
                }
                else if (DDLCambioDiscoDuro.SelectedValue == "2")
                {
                    TRDiscoDuro.Visible = true;
                    TRPicker.Visible = true;
                    TRPresentador.Visible = true;
                    TRParteSuperior.Visible = true;
                    TRBandas.Visible = true;
                }
                else
                {
                    TRDiscoDuro.Visible = true;
                    TRPicker.Visible = false;
                    TRPresentador.Visible = false;
                    TRParteSuperior.Visible = false;
                    TRBandas.Visible = false;
                }

            }
            catch (Exception ex)
            {
                throw;
            }

        }

        void CargarATM()
        {
            if (HttpContext.Current.Session["VERIFCorrectivo"] == null)
            {
                try
                {

                    String vQuery = "SPSTEI_ATM 7";
                    DataTable vDatos = vConexionATM.ObtenerTablaATM(vQuery);
                    DDLtipoTeclado.Items.Add(new ListItem { Value = "0", Text = "Seleccione teclado..." });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLtipoTeclado.Items.Add(new ListItem { Value = item["Id_Teclado_ATM"].ToString(), Text = item["Descripcion"].ToString() });
                        //}
                        //    Session["SUCURSAL"] = "1";
                    }

                    String vQuerySO = "STEISP_ATM_Generales 9";
                    DataTable vDatosSO = vConexion.ObtenerTabla(vQuerySO);
                    DDLSo.Items.Add(new ListItem { Value = "0", Text = "Seleccione sistema operativo..." });
                    foreach (DataRow item in vDatosSO.Rows)
                    {
                        DDLSo.Items.Add(new ListItem { Value = item["idSO"].ToString(), Text = item["nombreSO"].ToString() });
                    }

                    String vQuerySW = "SPSTEI_ATM 8";
                    DataTable vDatosSW = vConexionATM.ObtenerTablaATM(vQuerySW);
                    DDLVersionSW.Items.Add(new ListItem { Value = "0", Text = "Seleccione version del software..." });
                    foreach (DataRow item in vDatosSW.Rows)
                    {
                        DDLVersionSW.Items.Add(new ListItem { Value = item["Id_Software_ATM"].ToString(), Text = item["Descripcion"].ToString() });
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
            Session["VERIFCorrectivo"] = "1";
        }

        void ActualizarATM()
        {

            try
            {
                string vQuery = "SPSTEI_ATM 35, '" + Session["ATM_CORRECTIVO"] + "','" + DDLtipoTeclado.SelectedValue + "','" + DDLtipoProc.SelectedValue + "'," +
                    "'" + DDLtipoCargaVerif.SelectedValue + "','" + DDLmarcaDiscoDuro.SelectedValue + "','" + txtSerieDiscoDuro.Text + "','" + txtcapacidadDiscoVerif.Text + " GB" + "','" + txtserieATM.Text + "'," +
                    "'" + txtinventarioVerif.Text + "','" + txtramVerif.Text + " GB" + "','" + Session["USUARIO"].ToString() + "','" + txtlatitudATM.Text + "','" + txtlongitudATM.Text + "'," +
                    "'" + DDLSo.SelectedValue + "','" + DDLVersionSW.SelectedValue + "'";
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
                        string vQuery2 = "SPSTEI_ATM 34, '" + Session["ATM_CORRECTIVO"] + "','" + vArchivo + "'";
                        vConexionATM.ejecutarSQLATM(vQuery2);
                    }
                }

            }
            catch (Exception Ex)
            {
                throw;
            }
        }

        void AgregarImagenes()
        {
            string ID = Request.QueryString["cod"];
            string tipo = Request.QueryString["tipo"];
            string vArchivo = "";
            string vArchivo2 = "";
            string vArchivo3 = "";
            string vArchivo4 = "";
            string vArchivo5 = "";

            if (DDLCambioDiscoDuro.SelectedValue == "2")
            {
                //////////////////////////////////////////////////////////////////////////////
                //IMAGENES1
                if (FUDiscoDuro.FileName != "")
                {
                    Bitmap originalBMP = new Bitmap(FUDiscoDuro.FileContent);
                    byte[] imageData;
                    Bitmap originalBMPReducido = null; ;
                    long imgTamano;
                    using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
                    {
                        if (originalBMP.RawFormat.Equals(ImageFormat.Jpeg))
                        {
                            originalBMP.Save(stream, ImageFormat.Jpeg);
                            //originalBMP.SetResolution(100, 100);
                        }
                        else
                        {
                            originalBMP.Save(stream, ImageFormat.Png);
                        }

                        imgTamano = stream.Length;
                        double imgPeso = (double)imgTamano / 1024;
                        if (imgPeso >= 1000)
                        {
                            originalBMP.Save(stream, ImageFormat.Png);
                            stream.Position = 0;
                            imageData = new byte[stream.Length];
                            stream.Read(imageData, 0, imageData.Length);
                            stream.Close();
                        }
                    }
                    double imgKB = (double)imgTamano / 1024.0;
                    if (imgKB < 1000)
                    {
                        originalBMPReducido = new Bitmap(FUPickerLimpio.FileContent);
                    }
                    else
                    {
                        var newHeight = originalBMP.Height / 2;
                        var newWidth = originalBMP.Width / 2;
                        originalBMPReducido = new Bitmap(originalBMP.GetThumbnailImage(newWidth, newHeight, null, IntPtr.Zero));
                    }
                    using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
                    {
                        originalBMPReducido.Save(stream, ImageFormat.Jpeg);
                        stream.Position = 0;
                        imageData = new byte[stream.Length];
                        stream.Read(imageData, 0, imageData.Length);
                        stream.Close();
                    }
                    vArchivo = Convert.ToBase64String(imageData);
                }
                //////////////////////////////////////////////////////////////////////////////
            }
            //IMAGENES2
            if (FUPickerLimpio.FileName != "")
            {
                Bitmap originalBMP2 = new Bitmap(FUPickerLimpio.FileContent);
                byte[] imageData2;
                Bitmap originalBMPReducido2 = null; ;
                long imgTamano;
                using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
                {
                    if (originalBMP2.RawFormat.Equals(ImageFormat.Jpeg))
                    {
                        originalBMP2.Save(stream, ImageFormat.Jpeg);
                        //originalBMP.SetResolution(100, 100);
                    }
                    else
                    {
                        originalBMP2.Save(stream, ImageFormat.Png);
                    }

                    imgTamano = stream.Length;
                    double imgPeso = (double)imgTamano / 1024;
                    if (imgPeso >= 1000)
                    {
                        originalBMP2.Save(stream, ImageFormat.Png);
                        stream.Position = 0;
                        imageData2 = new byte[stream.Length];
                        stream.Read(imageData2, 0, imageData2.Length);
                        stream.Close();
                    }
                }
                double imgKB = (double)imgTamano / 1024.0;
                if (imgKB < 1000)
                {
                    originalBMPReducido2 = new Bitmap(FUPickerLimpio.FileContent);
                }
                else
                {
                    var newHeight2 = originalBMP2.Height / 2;
                    var newWidth2 = originalBMP2.Width / 2;
                    originalBMPReducido2 = new Bitmap(originalBMP2.GetThumbnailImage(newWidth2, newHeight2, null, IntPtr.Zero));
                }
                using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
                {
                    originalBMPReducido2.Save(stream, ImageFormat.Jpeg);
                    stream.Position = 0;
                    imageData2 = new byte[stream.Length];
                    stream.Read(imageData2, 0, imageData2.Length);
                    stream.Close();
                }
                vArchivo2 = Convert.ToBase64String(imageData2);
            }
            ////////////////////////////////////////////////////////////////////////////////
            //IMAGENES3
            if (FUPresentadorLimpio.FileName != "")
            {
                Bitmap originalBMP3 = new Bitmap(FUPresentadorLimpio.FileContent);
                byte[] imageData3;
                Bitmap originalBMPReducido3 = null;
                long imgTamano;

                using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
                {
                    if (originalBMP3.RawFormat.Equals(ImageFormat.Jpeg))
                    {
                        originalBMP3.Save(stream, ImageFormat.Jpeg);
                    }
                    else
                    {
                        originalBMP3.Save(stream, ImageFormat.Png);
                    }

                    imgTamano = stream.Length;
                    double imgPeso = (double)imgTamano / 1024;
                    if (imgPeso >= 1000)
                    {
                        originalBMP3.Save(stream, ImageFormat.Png);
                        stream.Position = 0;
                        imageData3 = new byte[stream.Length];
                        stream.Read(imageData3, 0, imageData3.Length);
                        stream.Close();
                    }
                }
                double imgKB = (double)imgTamano / 1024.0;
                if (imgKB < 1000)
                {
                    originalBMPReducido3 = new Bitmap(FUPresentadorLimpio.FileContent);
                }
                else
                {

                    var newHeight3 = originalBMP3.Height / 2;
                    var newWidth3 = originalBMP3.Width / 2;
                    originalBMPReducido3 = new Bitmap(originalBMP3.GetThumbnailImage(newWidth3, newHeight3, null, IntPtr.Zero));
                }

                using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
                {
                    originalBMPReducido3.Save(stream, ImageFormat.Jpeg);
                    stream.Position = 0;
                    imageData3 = new byte[stream.Length];
                    stream.Read(imageData3, 0, imageData3.Length);
                    stream.Close();
                }
                vArchivo3 = Convert.ToBase64String(imageData3);
            }
            ////////////////////////////////////////////////////////////////////////////////
            //IMAGENES4
            if (FUATMSinDesarmarPS.FileName != "")
            {
                Bitmap originalBMP4 = new Bitmap(FUATMSinDesarmarPS.FileContent);
                byte[] imageData4;
                Bitmap originalBMPReducido4 = null;
                long imgTamano;

                using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
                {
                    if (originalBMP4.RawFormat.Equals(ImageFormat.Jpeg))
                    {
                        originalBMP4.Save(stream, ImageFormat.Jpeg);
                    }
                    else
                    {
                        originalBMP4.Save(stream, ImageFormat.Png);
                    }

                    imgTamano = stream.Length;
                    double imgPeso = (double)imgTamano / 1024;
                    if (imgPeso >= 1000)
                    {
                        originalBMP4.Save(stream, ImageFormat.Png);
                        stream.Position = 0;
                        imageData4 = new byte[stream.Length];
                        stream.Read(imageData4, 0, imageData4.Length);
                        stream.Close();
                    }
                }
                double imgKB = (double)imgTamano / 1024.0;
                if (imgKB < 1000)
                {
                    originalBMPReducido4 = new Bitmap(FUATMSinDesarmarPS.FileContent);
                }
                else
                {
                    var newHeight4 = originalBMP4.Height / 2;
                    var newWidth4 = originalBMP4.Width / 2;
                    originalBMPReducido4 = new Bitmap(originalBMP4.GetThumbnailImage(newWidth4, newHeight4, null, IntPtr.Zero));
                }
                using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
                {
                    originalBMPReducido4.Save(stream, ImageFormat.Jpeg);
                    stream.Position = 0;
                    imageData4 = new byte[stream.Length];
                    stream.Read(imageData4, 0, imageData4.Length);
                    stream.Close();
                }
                vArchivo4 = Convert.ToBase64String(imageData4);
            }

            ////////////////////////////////////////////////////////////////////////////////
            //IMAGENES5
            if (FUBandas.FileName != "")
            {
                Bitmap originalBMP5 = new Bitmap(FUBandas.FileContent);
                byte[] imageData5;
                Bitmap originalBMPReducido5 = null;
                long imgTamano;

                using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
                {
                    if (originalBMP5.RawFormat.Equals(ImageFormat.Jpeg))
                    {
                        originalBMP5.Save(stream, ImageFormat.Jpeg);
                    }
                    else
                    {
                        originalBMP5.Save(stream, ImageFormat.Png);
                    }

                    imgTamano = stream.Length;
                    double imgPeso = (double)imgTamano / 1024;
                    if (imgPeso >= 1000)
                    {
                        originalBMP5.Save(stream, ImageFormat.Png);
                        stream.Position = 0;
                        imageData5 = new byte[stream.Length];
                        stream.Read(imageData5, 0, imageData5.Length);
                        stream.Close();
                    }
                }
                double imgKB = (double)imgTamano / 1024.0;
                if (imgKB < 1000)
                {
                    originalBMPReducido5 = new Bitmap(FUBandas.FileContent);
                }
                else
                {
                    var newHeight5 = originalBMP5.Height;
                    var newWidth5 = originalBMP5.Width;
                    originalBMPReducido5 = new Bitmap(originalBMP5.GetThumbnailImage(newWidth5, newHeight5, null, IntPtr.Zero));
                }
                using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
                {
                    originalBMPReducido5.Save(stream, ImageFormat.Jpeg);
                    stream.Position = 0;
                    imageData5 = new byte[stream.Length];
                    stream.Read(imageData5, 0, imageData5.Length);
                    stream.Close();
                }
                vArchivo5 = Convert.ToBase64String(imageData5);
            }

            ////////////////////////////////////////////////////////////////////////////////

            if (tipo == "3")
            {
                if (FUDiscoDuro.HasFile != false)
                {
                    string vQuery = "STEISP_ATM_NotificacionCorrectivo 12, '" + ID + "','" + vArchivo + "'";
                    vConexion.ejecutarSQL(vQuery);
                }
                if (FUPickerLimpio.HasFile != false)
                {
                    string vQuery = "STEISP_ATM_NotificacionCorrectivo 7, '" + ID + "','" + vArchivo2 + "'";
                    vConexion.ejecutarSQL(vQuery);
                }
                if (FUPresentadorLimpio.HasFile != false)
                {
                    string vQuery = "STEISP_ATM_NotificacionCorrectivo 9, '" + ID + "','" + vArchivo3 + "'";
                    vConexion.ejecutarSQL(vQuery);
                }
                if (FUATMSinDesarmarPS.HasFile != false)
                {
                    string vQuery = "STEISP_ATM_NotificacionCorrectivo 10, '" + ID + "','" + vArchivo4 + "'";
                    vConexion.ejecutarSQL(vQuery);
                }
                if (FUBandas.HasFile != false)
                {
                    string vQuery = "STEISP_ATM_NotificacionCorrectivo 11, '" + ID + "','" + vArchivo5 + "'";
                    vConexion.ejecutarSQL(vQuery);
                }
            }
            else
            {
                string vQuery = "STEISP_ATM_NotificacionCorrectivo 6, '" + ID + "','" + vArchivo2 + "','" + vArchivo3 + "'," +
                       "'" + vArchivo4 + "','" + vArchivo5 + "'";
                Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                if (vInfo == 1)
                {
                    if (FUDiscoDuro.HasFile != false)
                    {
                        string vQuery2 = "STEISP_ATM_NotificacionCorrectivo 12, '" + ID + "','" + vArchivo + "'";
                        vConexion.ejecutarSQL(vQuery2);
                    }
                }
            }
        }


        void AgregarImagenDiscoDuro()
        {
            string ID = Request.QueryString["cod"];
            string tipo = Request.QueryString["tipo"];
            //IMAGENES1
            String vNombreDepot11 = String.Empty;
            HttpPostedFile bufferDeposito11 = FUDiscoDuro.PostedFile;
            byte[] vFileDeposito11 = null;
            string vExtension11 = string.Empty;

            if (bufferDeposito11 != null)
            {
                vNombreDepot11 = FUDiscoDuro.FileName;
                Stream vStream11 = bufferDeposito11.InputStream;
                BinaryReader vReader11 = new BinaryReader(vStream11);
                vFileDeposito11 = vReader11.ReadBytes((int)vStream11.Length);
                vExtension11 = System.IO.Path.GetExtension(FUDiscoDuro.FileName);
            }
            String vArchivo = String.Empty;
            if (vFileDeposito11 != null)
                vArchivo = Convert.ToBase64String(vFileDeposito11);

            if (tipo == "3")
            {
                if (FUDiscoDuro.HasFile != false)
                {
                    string vQuery = "STEISP_ATM_NotificacionCorrectivo 12, '" + ID + "','" + vArchivo + "'";
                    vConexion.ejecutarSQL(vQuery);
                }
            }
            else
            {
                if (FUDiscoDuro.HasFile != false)
                {
                    string vQuery = "STEISP_ATM_NotificacionCorrectivo 13, '" + ID + "','" + vArchivo + "'";
                    vConexion.ejecutarSQL(vQuery);
                }
            }
        }

        void vaciarValorImg()
        {
            HFDiscoDuro.Value = "";
            HFPicker.Value = "";
            HFPresentador.Value = "";
            HFBandas.Value = "";
            HFATMSinDesarmar.Value = "";
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
            if (DDLSo.SelectedValue == "0")
                throw new Exception("Favor seleccione sistema operativo.");
            if (DDLVersionSW.SelectedValue == "0")
                throw new Exception("Favor seleccione versión del software.");
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
            //if (txtinventarioVerif.Text == "" || txtinventarioVerif.Text == string.Empty)
            //    throw new Exception("Favor ingrese el número de inventario.");
            if (txtramVerif.Text == "" || txtramVerif.Text == string.Empty)
                throw new Exception("Favor ingrese la capacidad RAM.");
            else if (dropantiskimming.SelectedValue == "0")
                throw new Exception("Favor seleccione una respuesta de AntiSkimming.");
            if (dropantiskimming.SelectedValue == "1")
            {
                if (txtantiSkimming.Text == "" || txtantiSkimming.Text == string.Empty)
                    throw new Exception("Favor ingrese comentario de AntiSkimming.");
            }
            if (DDLCambioPiezas.SelectedValue == "0")
                throw new Exception("Favor seleccione opción de cambio de piezas.");
            if (DDLCambioPiezas.SelectedValue == "1")
            {
                if (txtCambioMateriales.Text == "" || txtCambioMateriales.Text == string.Empty)
                    throw new Exception("Favor ingrese materiales que utilizó en mantenimiento.");
            }
            if (txtobseracionesVerif.Text == "" || txtobseracionesVerif.Text == string.Empty)
                throw new Exception("Favor ingrese sus observaciones del caso.");
            //if(HFMapa.Value==string.Empty)
            //    throw new Exception("Favor ingrese imagen del mapa de ubicación de ATM.");
            if (DDLCambioDiscoDuro.SelectedValue == "0")
            {
                if (HFPicker.Value == string.Empty)
                    throw new Exception("Favor agregar imagen de Picker con rodo limpio.");
                if (HFPresentador.Value == string.Empty)
                    throw new Exception("Favor agregar imagen de presentador limpio.");
                if (HFBandas.Value == string.Empty)
                    throw new Exception("Favor agregar imagen de las bandas.");
                if (HFATMSinDesarmar.Value == string.Empty)
                    throw new Exception("Favor agregar imagen de parte superior de ATM sin desarmar y limpio.");
            }
            else if (DDLCambioDiscoDuro.SelectedValue == "1")
            {
                if (HFDiscoDuro.Value == string.Empty)
                    throw new Exception("Favor agregar imagen de disco duro.");
            }
            else
            {
                if (HFPicker.Value == string.Empty)
                    throw new Exception("Favor agregar imagen de Picker con rodo limpio.");
                if (HFPresentador.Value == string.Empty)
                    throw new Exception("Favor agregar imagen de presentador limpio.");
                if (HFBandas.Value == string.Empty)
                    throw new Exception("Favor agregar imagen de las bandas.");
                if (HFATMSinDesarmar.Value == string.Empty)
                    throw new Exception("Favor agregar imagen de parte superior de ATM sin desarmar y limpio.");
                if (HFDiscoDuro.Value == string.Empty)
                    throw new Exception("Favor agregar imagen de disco duro.");
            }



        }

        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        protected void dropantiskimming_TextChanged(object sender, EventArgs e)
        {
            if (dropantiskimming.SelectedValue == "1")
                txtantiSkimming.Enabled = true;
            else
                txtantiSkimming.Enabled = false;
        }

        protected void DDLCambioPiezas_TextChanged(object sender, EventArgs e)
        {
            if (DDLCambioPiezas.SelectedValue == "1")
                txtCambioMateriales.Enabled = true;
            else
                txtCambioMateriales.Enabled = false;
        }

        protected void btnEnviarVerif_Click(object sender, EventArgs e)
        {
            try
            {
                validar();
                lbcodATM.Text = txtnomATM.Text;
                lbsucursalATM.Text = txtsucursal.Text;
                lbInventarioATM.Text = txtinventarioVerif.Text;
                lbtecnico.Text = txtTecnicoResponsable.Text;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }

        }

        protected void btnRechazarVerif_Click(object sender, EventArgs e)
        {
            txtmotivoRechazo.Text = "";
            txtAlerta2.Visible = false;
            lbcodATM2.Text = txtnomATM.Text;
            lbsucursalATM2.Text = txtsucursal.Text;
            lbInventarioATM2.Text = txtinventarioVerif.Text;
            lbtecnico2.Text = txtTecnicoResponsable.Text;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal2();", true);
        }

        protected void btnModalCerrarVerif_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
        }

        void EnviarCorreo()
        {
            string ID = Request.QueryString["cod"];
            string vTipo = Request.QueryString["tipo"];

            Boolean vFlagEnvio = false;
            String vDestino = "";
            SmtpService vService = new SmtpService();

            String vQuery7 = "[STEISP_ATM_GeneralesCorrectivo] 15, '" + Session["ATM_CORRECTIVO"] + "'";
            DataTable vDatos7 = vConexion.ObtenerTabla(vQuery7);

            string vCorreoEncargadoZona = "";
            if (vDatos7.Rows[0]["IDZona"].ToString() == "1")
                vCorreoEncargadoZona = "emontoya@bancatlan.hn";
            if (vDatos7.Rows[0]["IDZona"].ToString() == "2")
                vCorreoEncargadoZona = "jdgarcia@bancatlan.hn";
            if (vDatos7.Rows[0]["IDZona"].ToString() == "3")
                vCorreoEncargadoZona = "acalderon@bancatlan.hn";

            String vQuery6 = "[STEISP_ATM_GeneralesCorrectivo] 14, '" + ID + "'";
            DataTable vDatos6 = vConexion.ObtenerTabla(vQuery6);

            string vQueryD = "STEISP_ATM_Generales 33,'" + vDatos6.Rows[0]["usuarioResponsable"].ToString() + "'";
            DataTable vDatosTecnicoResponsable = vConexion.ObtenerTabla(vQueryD);
            DataTable vDatos = (DataTable)Session["AUTHCLASS"];
            string vQueryJefes = "[STEISP_ATM_GeneralesCorrectivo] 13,'" + Session["ATM_CORRECTIVO"] + "','" + txtsysaid.Text + "'";
            DataTable vDatosJefeAgencias = vConexion.ObtenerTabla(vQueryJefes);

            if (vTipo == "2")
            {

                if (vDatos.Rows.Count > 0)
                {
                    foreach (DataRow item in vDatos.Rows)
                    {
                        //ENVIAR A JEFE CREADOR
                        if (Session["USUARIO"].ToString() == "eurrea")
                        {
                            if (!item["correo"].ToString().Trim().Equals(""))
                            {
                                vService.EnviarMensaje(item["correo"].ToString(),
                                typeBody.ATM,
                                "Notificación de Mantenimiento Correctivo ATM",
                                "Buen día, se le notifica que se aprobó una solicitud de mantenimiento correctivo, el encargado es " + vDatosTecnicoResponsable.Rows[0]["nombre"].ToString() + ", mantenimiento al ATM " + txtnomATM.Text,
                                "El usuario <b>" + item["Nombre"].ToString() + "</b> aprobó: <br> Notificación de Mantenimiento Correctivo.",
                                vCorreoEncargadoZona,
                                "/sites/ATM/pages/correctivo/aprobarVerifCorrectivo.aspx"
                                );

                                //vFlagEnvioSupervisor = true;
                            }
                        }
                        else
                        {
                            if (!item["correo"].ToString().Trim().Equals(""))
                            {
                                vService.EnviarMensaje(item["correo"].ToString(),
                                typeBody.ATM,
                                "Notificación de Mantenimiento Correctivo ATM",
                                "Buen día, se le notifica que se aprobó una solicitud de mantenimiento correctivo, el encargado es " + vDatosTecnicoResponsable.Rows[0]["nombre"].ToString() + ", mantenimiento al ATM " + txtnomATM.Text,
                                "El usuario <b>" + item["Nombre"].ToString() + "</b> aprobó: <br> Notificación de Mantenimiento Correctivo.",
                                ConfigurationManager.AppSettings["STEIMail"],
                                "/sites/ATM/pages/correctivo/aprobarVerifCorrectivo.aspx"
                                );

                                //vFlagEnvioSupervisor = true;
                            }
                        }

                        //PERSONAL ENCARGADO DE ATM
                        String vKioskos = "unidadatmkiosco@bancatlan.hn";
                        vService.EnviarMensaje(vKioskos,
                               typeBody.ATM,
                               "Notificación de Mantenimiento correctivo ATM",
                               "Buen día, se le notifica que se ha aprobado una solicitud de mantenimiento correctivo, el encargado es " + vDatosTecnicoResponsable.Rows[0]["nombre"].ToString() + ", mantenimiento al ATM " + txtnomATM.Text,
                                 "El usuario <b>" + item["Nombre"].ToString() + "</b> aprobó: <br> Notificación de Mantenimiento",
                                  "",
                               ""
                               );


                    }
                }
                if (vDatosTecnicoResponsable.Rows.Count > 0)
                {
                    foreach (DataRow item in vDatosTecnicoResponsable.Rows)
                    {
                        //ENVIAR A RESPONSABLE
                        vService.EnviarMensaje(item["correo"].ToString(),
                            typeBody.ATM,
                           "Notificación de Mantenimiento correctivo ATM",
                            "Buen día, se le notifica que se aprobó solicitud de mantenimientocorrectivo, el encargado es " + item["nombre"].ToString() + ", mantenimiento al ATM " + txtnomATM.Text,
                              "El usuario <b>" + vDatos.Rows[0]["Nombre"].ToString() + "</b> aprobó: <br> Notificación de Mantenimiento de ATM al que ha sido asignado como responsable.",
                                "",
                            "/login.spx"
                            );
                    }
                }
                if (vDatosJefeAgencias.Rows.Count > 0)
                {
                    foreach (DataRow item in vDatosJefeAgencias.Rows)
                    {
                        //ENVIAR A JEFES DE AGENCIA
                        if (!item["correo"].ToString().Trim().Equals(""))
                        {
                            vService.EnviarMensaje(item["correo"].ToString(),
                                typeBody.ATM,
                                "Notificación de Mantenimiento ATM",
                                    "Buen día, se le notifica que se aprobó solicitud de mantenimiento correctivo, el encargado es " + vDatosTecnicoResponsable.Rows[0]["nombre"].ToString() + ", mantenimiento al ATM " + txtnomATM.Text,
                                      "Solicitud de mantenimiento correctivo a ATM.",
                                       "",
                                       ""
                                );
                        }
                    }
                }
            }
            else
            {
                if (vDatos.Rows.Count > 0)
                {
                    foreach (DataRow item in vDatos.Rows)
                    {
                        //ENVIAR A JEFE CREADOR

                        if (!item["correo"].ToString().Trim().Equals(""))
                        {
                            vService.EnviarMensaje(item["correo"].ToString(),
                            typeBody.ATM,
                            "Notificación de Mantenimiento Correctivo ATM",
                            "Buen día, se le notifica que se creó una solicitud de mantenimiento correctivo, el encargado es " + vDatosTecnicoResponsable.Rows[0]["nombre"].ToString() + ", mantenimiento al ATM " + txtnomATM.Text,
                            "El usuario <b>" + item["Nombre"].ToString() + "</b> créo: <br> Notificación de Mantenimiento Correctivo.",
                            "",
                            "/sites/ATM/pages/correctivo/crearVerifCorrectivo.aspx"
                            );

                            //vFlagEnvioSupervisor = true;
                        }


                        //ENVIAR A EDWIN
                        //string vNombre = "EDWIN ALBERTO URREA PENA";
                        vService.EnviarMensaje(ConfigurationManager.AppSettings["STEIMail"],
                                typeBody.ATM,
                                "Notificación de Mantenimiento correctivo ATM",
                                "Buen día, se le notifica que se creó solicitud de mantenimiento correctivo, el encargado es " + vDatosTecnicoResponsable.Rows[0]["nombre"].ToString() + ", mantenimiento al ATM " + txtnomATM.Text,
                                  "El usuario <b>" + item["Nombre"].ToString() + "</b> creó: <br> Notificación de Mantenimiento.",
                                   vCorreoEncargadoZona,
                                   "/sites/ATM/pages/correctivo/crearVerifCorrectivo.aspx"
                                );

                        //PRSONAL ENCARGADO DE ATM
                        //String vKioskos = "unidadatmkiosco@bancatlan.hn";
                        //vService.EnviarMensaje(vKioskos,
                        //       typeBody.ATM,
                        //       "Notificación de Mantenimiento correctivo ATM",
                        //       "Buen día, se le notifica que se ha aprobado una solicitud de mantenimiento correctivo, el encargado es " + vDatosTecnicoResponsable.Rows[0]["nombre"].ToString() + ", mantenimiento al ATM " + txtnomATM.Text,
                        //         "El usuario <b>" + item["Nombre"].ToString() + "</b> aprobó: <br> Notificación de Mantenimiento",
                        //          "",
                        //       ""
                        //       );


                    }


                }

            }

        }

        void CorreoSuscripcion()
        {
            string ID = Request.QueryString["cod"];
            string vTipo = Request.QueryString["tipo"];
            string vEstado = "";
            DataTable vDatos = new DataTable();
            String vQuery = "[STEISP_ATM_GeneralesCorrectivo] 14,'" + ID + "'";
            vDatos = vConexion.ObtenerTabla(vQuery);
            foreach (DataRow item in vDatos.Rows)
            {
                vEstado = item["estado"].ToString();
            }

            if (vEstado == "5")
            {

                String vQuery7 = "[STEISP_ATM_GeneralesCorrectivo] 15, '" + Session["ATM_CORRECTIVO"] + "'";
                DataTable vDatos7 = vConexion.ObtenerTabla(vQuery7);

                string vCorreoEncargadoZona = "";
                if (vDatos7.Rows[0]["IDZona"].ToString() == "1")
                    vCorreoEncargadoZona = "emontoya@bancatlan.hn";
                if (vDatos7.Rows[0]["IDZona"].ToString() == "2")
                    vCorreoEncargadoZona = "jdgarcia@bancatlan.hn";
                if (vDatos7.Rows[0]["IDZona"].ToString() == "3")
                    vCorreoEncargadoZona = "acalderon@bancatlan.hn";

                String vQuery6 = "[STEISP_ATM_GeneralesCorrectivo] 14, '" + ID + "'";
                DataTable vDatos6 = vConexion.ObtenerTabla(vQuery6);

                string vQueryD = "STEISP_ATM_Generales 33,'" + vDatos6.Rows[0]["usuarioResponsable"].ToString() + "'";
                DataTable vDatosTecnicoResponsable = vConexion.ObtenerTabla(vQueryD);
                DataTable vDatos1 = (DataTable)Session["AUTHCLASS"];
                string vQueryJefes = "[STEISP_ATM_GeneralesCorrectivo] 13,'" + Session["ATM_CORRECTIVO"] + "','" + txtsysaid.Text + "'";
                DataTable vDatosJefeAgencias = vConexion.ObtenerTabla(vQueryJefes);


                string vCorreosTecnicos = "";
                string vCorreosJefes = "";
                string vCorreosTodos = "";
                string vCorreoResponsable = "";
                for (int i = 0; i < vDatosTecnicoResponsable.Rows.Count; i++)
                {
                    vCorreoResponsable = vDatosTecnicoResponsable.Rows[i]["correo"].ToString() + ";";

                }

                for (int i = 0; i < vDatosJefeAgencias.Rows.Count; i++)
                {
                    string vCorreo = vDatosJefeAgencias.Rows[i]["correo"].ToString() + ";";
                    vCorreosJefes = vCorreosJefes + vCorreo;
                    if (vCorreosJefes == ";")
                        vCorreosJefes = "";
                }


                string vDepto = "";
                DataTable vDatosDepto = new DataTable();
                String vQueryDepto = "STEISP_ATM_Generales 48,'" + Session["ATM_CORRECTIVO"] + "'";
                vDatosDepto = vConexion.ObtenerTabla(vQueryDepto);
                foreach (DataRow item in vDatosDepto.Rows)
                {
                    vDepto = item["Depto"].ToString();
                }
                if (vDepto == "18")
                    vCorreoEncargadoZona = "acalderon@bancatlan.hn;jdgarcia@bancatlan.hn";

                string vReporte = "";
                if (DDLCambioDiscoDuro.SelectedValue == "1")
                {
                    vReporte = "DiscoDuro";
                }
                else
                {
                    vReporte = "Correctivo";
                }

                //vCorreosTodos = "acedillo@bancatlan.hn";
                //string vCorreoCopia = "acamador@bancatlan.hn"+";";
                string vCorreoCopia = "eurrea@bancatlan.hn;unidadatmkiosco@bancatlan.hn;" + vCorreoEncargadoZona;
                //vCorreosTodos = vCorreosTecnicos + vCorreosJefes + vCorreoAdmin;
                vCorreosTodos = vCorreoResponsable + vCorreosJefes;
                string vAsuntoRV = "Formato de mantenimiento correctivo";
                string vBody = "Formato de mantenimiento correctivo";
                int vEstadoSuscripcion = 0;
                string vQueryRep = "STEISP_ATM_Generales 35, '" + vReporte + "','" + vCorreosTodos + "','" + vCorreoCopia + "','" + vAsuntoRV + "','" + vBody + "','" + vEstadoSuscripcion + "','" + ID + "'";
                vConexion.ejecutarSQL(vQueryRep);

            }
        }

        protected void btnModalVerif_Click(object sender, EventArgs e)
        {
            string ID = Request.QueryString["cod"];
            string vTipo = Request.QueryString["tipo"];


            if (vTipo == "2") //APROBAR
            {
                string vQuery = "STEISP_ATM_NotificacionCorrectivo 16, '" + ID + "','" + Session["USUARIO"].ToString() + "'";
                Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                if (vInfo == 1)
                {
                    EnviarCorreo();
                    CorreoSuscripcion();
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
                    Response.Redirect("aprobarVerifCorrectivo.aspx");
                }
            }
            else //CREAR Y MODIFICAR
            {
                string vQuery = "STEISP_ATM_NotificacionCorrectivo 5, '" + ID + "','" + TxFechaInicio.Text + "','" + TxFechaRegreso.Text + "'," +
                                   "'" + txthsalidaInfa.Text + "','" + txtHllegadaInfatlan.Text + "','" + Session["USUARIO"].ToString() + "','" + dropantiskimming.SelectedValue + "','" + txtantiSkimming.Text + "'," +
                                   "'" + DDLCambioPiezas.SelectedValue + "','" + txtCambioMateriales.Text + "','" + DDLCambioDiscoDuro.SelectedValue + "','" + txtobseracionesVerif.Text + "'";
                Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                if (vInfo == 1)
                {

                    ActualizarATM();
                    if (DDLCambioDiscoDuro.SelectedValue == "1")
                        AgregarImagenDiscoDuro();
                    else
                        AgregarImagenes();

                    EnviarCorreo();
                    vaciarValorImg();
                    Response.Redirect("crearVerifCorrectivo.aspx");
                }
            }


        }

        protected void btnCerrarRechazoModal_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal2();", true);
        }
        void EnviarCorreoDevolver()
        {
            string ID = Request.QueryString["cod"];

            Boolean vFlagEnvio = false;
            String vDestino = "";
            SmtpService vService = new SmtpService();

            String vQuery7 = "[STEISP_ATM_GeneralesCorrectivo] 15, '" + Session["ATM_CORRECTIVO"] + "'";
            DataTable vDatos7 = vConexion.ObtenerTabla(vQuery7);

            string vCorreoEncargadoZona = "";
            if (vDatos7.Rows[0]["IDZona"].ToString() == "1")
                vCorreoEncargadoZona = "emontoya@bancatlan.hn";
            if (vDatos7.Rows[0]["IDZona"].ToString() == "2")
                vCorreoEncargadoZona = "jdgarcia@bancatlan.hn";
            if (vDatos7.Rows[0]["IDZona"].ToString() == "3")
                vCorreoEncargadoZona = "acalderon@bancatlan.hn";

            String vQuery6 = "[STEISP_ATM_GeneralesCorrectivo] 14, '" + ID + "'";
            DataTable vDatos6 = vConexion.ObtenerTabla(vQuery6);

            string vQueryD = "STEISP_ATM_Generales 33,'" + vDatos6.Rows[0]["usuarioResponsable"].ToString() + "'";
            DataTable vDatosTecnicoResponsable = vConexion.ObtenerTabla(vQueryD);
            DataTable vDatos = (DataTable)Session["AUTHCLASS"];
            string vQueryJefes = "[STEISP_ATM_GeneralesCorrectivo] 13,'" + Session["ATM_CORRECTIVO"] + "','" + txtsysaid.Text + "'";
            DataTable vDatosJefeAgencias = vConexion.ObtenerTabla(vQueryJefes);

            if (vDatos.Rows.Count > 0)
            {
                foreach (DataRow item in vDatos.Rows)
                {
                    //ENVIAR A JEFE CREADOR
                    if (Session["USUARIO"].ToString() == "eurrea")
                    {
                        if (!item["correo"].ToString().Trim().Equals(""))
                        {
                            vService.EnviarMensaje(item["correo"].ToString(),
                            typeBody.ATM,
                            "Notificación de Mantenimiento Correctivo ATM",
                            "Buen día, se le notifica que se devolvió una solicitud de mantenimiento correctivo, el encargado es " + vDatosTecnicoResponsable.Rows[0]["nombre"].ToString() + ", mantenimiento al ATM " + txtnomATM.Text,
                            "El usuario <b>" + item["Nombre"].ToString() + "</b> devolvió: <br> Notificación de Mantenimiento Correctivo</br>Motivo de devolución: " + txtmotivoRechazo.Text,
                            vCorreoEncargadoZona,
                            "/sites/ATM/pages/correctivo/aprobarVerifCorrectivo.aspx"
                            );

                            //vFlagEnvioSupervisor = true;
                        }
                    }
                    else
                    {
                        if (!item["correo"].ToString().Trim().Equals(""))
                        {
                            vService.EnviarMensaje(item["correo"].ToString(),
                            typeBody.ATM,
                            "Notificación de Mantenimiento Correctivo ATM",
                            "Buen día, se le notifica que se devolvió una solicitud de mantenimiento correctivo, el encargado es " + vDatosTecnicoResponsable.Rows[0]["nombre"].ToString() + ", mantenimiento al ATM " + txtnomATM.Text,
                            "El usuario <b>" + item["Nombre"].ToString() + "</b> devolvió: <br> Notificación de Mantenimiento Correctivo.</br>Motivo de devolución: " + txtmotivoRechazo.Text,
                            ConfigurationManager.AppSettings["STEIMail"],
                            "/sites/ATM/pages/correctivo/aprobarVerifCorrectivo.aspx"
                            );

                            //vFlagEnvioSupervisor = true;
                        }
                    }


                }
            }
            if (vDatosTecnicoResponsable.Rows.Count > 0)
            {
                foreach (DataRow item in vDatosTecnicoResponsable.Rows)
                {
                    //ENVIAR A RESPONSABLE
                    vService.EnviarMensaje(item["correo"].ToString(),
                        typeBody.ATM,
                       "Notificación de Mantenimiento correctivo ATM",
                        "Buen día, se le notifica que se devolvió solicitud de mantenimientocorrectivo, el encargado es " + item["nombre"].ToString() + ", mantenimiento al ATM " + txtnomATM.Text,
                          "El usuario <b>" + vDatos.Rows[0]["Nombre"].ToString() + "</b> devolvió: <br> Notificación de Mantenimiento de ATM al que ha sido asignado como responsable.</br>Motivo de devolución: " + txtmotivoRechazo.Text,
                            "",
                        "/login.spx"
                        );
                }
            }


        }
        protected void btnRechazarModal_Click(object sender, EventArgs e)
        {
            if (txtmotivoRechazo.Text == "")
            {
                txtAlerta2.Visible = true;
            }
            else
            {
                string ID = Request.QueryString["cod"];
                string vQuery = "STEISP_ATM_NotificacionCorrectivo 15, '" + ID + "','" + txtmotivoRechazo.Text + "','" + Session["USUARIO"].ToString() + "'";
                Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                if (vInfo == 1)
                {
                    EnviarCorreoDevolver();
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal2();", true);
                    Response.Redirect("aprobarVerifCorrectivo.aspx");
                }
            }
        }

        protected void DDLCambioDiscoDuro_TextChanged(object sender, EventArgs e)
        {
            if (DDLCambioDiscoDuro.SelectedValue == "0")
            {
                TRDiscoDuro.Visible = false;
                TRPicker.Visible = true;
                TRPresentador.Visible = true;
                TRParteSuperior.Visible = true;
                TRBandas.Visible = true;
            }
            else if (DDLCambioDiscoDuro.SelectedValue == "2")
            {
                TRDiscoDuro.Visible = true;
                TRPicker.Visible = true;
                TRPresentador.Visible = true;
                TRParteSuperior.Visible = true;
                TRBandas.Visible = true;
            }
            else
            {
                TRDiscoDuro.Visible = true;
                TRPicker.Visible = false;
                TRPresentador.Visible = false;
                TRParteSuperior.Visible = false;
                TRBandas.Visible = false;
            }
            UPIMG.Update();

        }
    }
}