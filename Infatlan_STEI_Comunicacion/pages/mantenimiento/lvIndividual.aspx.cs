using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infatlan_STEI_Comunicacion.classes;
using System.Data;
using System.IO;
using System.Drawing;

namespace Infatlan_STEI_Comunicacion.pages.mantenimiento
{
    public partial class lvIndividual : System.Web.UI.Page
    {
        db vConexion = new db();
        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            String vEx = Request.QueryString["ex"];
            if (!Page.IsPostBack)
            {
                if (vEx != null)
                {
                    if (vEx.Equals("1"))//CREAR NOTIFICACIÓN
                    {
                        cargarDataCompletarLV();
                    }else if (vEx.Equals("2"))
                    {
                        cargarDataAprobarLV();
                        bloquearCamposAprobarLV();
                    }

                }
            }
        }

        void cargarDataCompletarLV()
        {
            try
            {
                DataTable vDatos = new DataTable();
                vDatos = (DataTable)Session["COMUNICACION_PCLV_COMPLETAR_LV_INDIVIDUAL"];
                TxFechaMantenimiento.Text = vDatos.Rows[0]["fechaMantenimiento"].ToString();
                TxNodo.Text = vDatos.Rows[0]["nombreNodo"].ToString();
                TxZona.Text = vDatos.Rows[0]["regiones"].ToString();
                TxIp.Text = vDatos.Rows[0]["ip"].ToString();
                TxImagen.Text = vDatos.Rows[0]["IOSImage"].ToString();
                TXVersion.Text = vDatos.Rows[0]["IOSVersion"].ToString();
                TxTipo.Text = vDatos.Rows[0]["tipoStock"].ToString();
                TxDirección.Text = vDatos.Rows[0]["direccion"].ToString();
                TxSysAid.Text = vDatos.Rows[0]["sysAid"].ToString();
                TxControlCambio.Text = vDatos.Rows[0]["controlCambio"].ToString();
                TxHoraInicio.Text = vDatos.Rows[0]["horaInicio"].ToString();
                TxHoraFin.Text = vDatos.Rows[0]["horaFin"].ToString();
                TxImpacto.Text = vDatos.Rows[0]["impacto"].ToString();
                TxUltimoMantenimiento.Text = vDatos.Rows[0]["ultimoMantenimiento"].ToString();
                TxDuracionMan.Text = vDatos.Rows[0]["tiempoEstimado"].ToString();
                mostrarCamposImagen();
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
        void mostrarCamposImagen()
        {
            String vEx = Request.QueryString["ex"];
            DataTable vDatos = new DataTable();
            if (vEx != null)
            {
                if (vEx.Equals("1"))//CREAR NOTIFICACIÓN
                {
                    vDatos = (DataTable)Session["COMUNICACION_PCLV_COMPLETAR_LV_INDIVIDUAL"];
                }
                else if (vEx.Equals("2"))
                {
                    vDatos = (DataTable)Session["COMUNICACION_PALV_COMPLETAR_LV_INDIVIDUAL"];
                }
            }

            string vTipo= vDatos.Rows[0]["tipo"].ToString();
            string vdetalle = vDatos.Rows[0]["detalle"].ToString();




            if (vTipo== "ROUTER")
            {
                RowVersionRecomendada.Visible = true;
                RowEDC_ACS.Visible = true;
                RowTablaARP.Visible = true;
                RowTablaMAC.Visible = false;
                RowVersionEquipo.Visible = true;
                RowInterfaces.Visible = true;
                RowDMVPN_Activos.Visible = true;
                RowVerify.Visible = true;

                RowVersionEquipoActualizacion.Visible = true;
                RowShowAuthentication.Visible = true;
                RowEquipoAgregadoSolarwinds.Visible = true;
                RowConfiGuardadaSolarwinds.Visible = true;
                RowAltaDisponibilidad.Visible = false;
                RowPregunta.Visible = true;

                bool vEstadoRouter891 = vdetalle.Contains("891");
                if (vEstadoRouter891 ==true){
                    RowShowAuthentication.Visible = true;
                }else{
                    RowShowAuthentication.Visible = false;
                }

            }else if  (vTipo == "SWITCH") {
                RowVersionRecomendada.Visible = true;
                RowEDC_ACS.Visible = true;
                RowTablaARP.Visible = false;
                RowTablaMAC.Visible = true;
                RowVersionEquipo.Visible = true;
                RowInterfaces.Visible = true;
                RowDMVPN_Activos.Visible = false;
                RowVerify.Visible = true;

                RowVersionEquipoActualizacion.Visible = true;
                RowShowAuthentication.Visible = true;
                RowEquipoAgregadoSolarwinds.Visible = true;
                RowConfiGuardadaSolarwinds.Visible = true;
                RowAltaDisponibilidad.Visible = false;
                RowPregunta.Visible = true;
             }else if (vTipo == "CORE"){
                RowVersionRecomendada.Visible = true;
                RowEDC_ACS.Visible = true;
                RowTablaARP.Visible = true;
                RowTablaMAC.Visible = false;
                RowVersionEquipo.Visible = true;
                RowInterfaces.Visible = true;
                RowDMVPN_Activos.Visible = false;
                RowVerify.Visible = true;

                RowVersionEquipoActualizacion.Visible = true;
                RowShowAuthentication.Visible = false;
                RowEquipoAgregadoSolarwinds.Visible = true;
                RowConfiGuardadaSolarwinds.Visible = true;
                RowAltaDisponibilidad.Visible = true;
                RowPregunta.Visible = true;
            }

        }
        void validacionEnviarLv()
        {
            DataTable vDatos = new DataTable();
            vDatos = (DataTable)Session["COMUNICACION_PCLV_COMPLETAR_LV_INDIVIDUAL"];
            string vTipo = vDatos.Rows[0]["tipo"].ToString();
            string vdetalle = vDatos.Rows[0]["detalle"].ToString();

            if (TxNewIOSVersion.Text == "" || TxNewIOSVersion.Text == string.Empty)
                throw new Exception("Falta que ingrese la versión del IOS.");

            if (TxNewIOSImagen.Text == "" || TxNewIOSImagen.Text == string.Empty)
                throw new Exception("Favor que ingrese la imagen del IOS.");

            if (vTipo == "ROUTER")
            {
                if (TxVersionRecomendada.Value == string.Empty)
                    throw new Exception("Falta que adjunte la imagen de que versión instaslada es la recomendada  por el fabricante al momento de la instalación.");

                if (TxEDC_ACS.Value == string.Empty)
                    throw new Exception("Falta que adjunte imagen que valide equipo de comunicación adicionado a ACS.");

                if (TxTablaARP.Value == string.Empty)
                    throw new Exception("Falta que adjunte imagen información capturada de la tabla ARP.");

                if (TxVersionEquipo.Value == string.Empty)
                    throw new Exception("Falta que adjunte imagen versión del equipo.");

                if (TxInterfaces.Value == string.Empty)
                    throw new Exception("Falta que adjunte imagen de la información capturada de las interfaces.");

                if (TxDMVPN_Activos.Value == string.Empty)
                    throw new Exception("Falta que adjunte imagen de los túneles DMVPN activos.");

                if (TxVerify.Value == string.Empty)
                    throw new Exception("Falta que adjunte imagen de verify de sistema operativo previo a la instalación.");

                if (TxVersionEquipoActualizacion.Value == string.Empty)
                    throw new Exception("Falta que adjunte imagen de la versión actualizada.");

                if (TxEquipoAgregadoSolarwinds.Value == string.Empty)
                    throw new Exception("Falta que adjunte imagen del equipo agregado a Solarwinds.");

                if (TxConfiGuardadaSolarwinds.Value == string.Empty)
                    throw new Exception("Falta que adjunte imagen de configuración guardada en Solarwinds  (startup-config/running-config).");

                if (RbVerificacionScrip.SelectedValue.Equals("") )
                    throw new Exception("Falta que selecciones opción si ha verficado que los scripts iniciales y finales concuerdan  y he realizado las pruebas necesarias para garantizar el funcionamiento normal del equipo.");
           
                bool vEstadoRouter891 = vdetalle.Contains("891");
                if (vEstadoRouter891 == true)
                {
                    if (TxShowAuthentication.Value == string.Empty)
                        throw new Exception("Falta que adjunte imagen del comando “show authentication sessions” ejecutado.");
                }

            }
            else if (vTipo == "SWITCH")
            {           
                if (TxVersionRecomendada.Value == string.Empty)
                    throw new Exception("Falta que adjunte la imagen de que versión instaslada es la recomendada  por el fabricante al momento de la instalación.");

                if (TxEDC_ACS.Value == string.Empty)
                    throw new Exception("Falta que adjunte imagen que valide equipo de comunicación adicionado a ACS.");

                if (TxTablaMAC.Value == string.Empty)
                    throw new Exception("Falta que adjunte imagen de la información capturada de la tabla MAC.");

                if (TxVersionEquipo.Value == string.Empty)
                    throw new Exception("Falta que adjunte imagen versión del equipo.");
               
                if (TxShowAuthentication.Value == string.Empty)
                    throw new Exception("Falta que adjunte imagen del comando “show authentication sessions” ejecutado.");

                if (TxInterfaces.Value == string.Empty)
                    throw new Exception("Falta que adjunte imagen de la información capturada de las interfaces.");

                if (TxVerify.Value == string.Empty)
                    throw new Exception("Falta que adjunte imagen de verify de sistema operativo previo a la instalación.");

                if (TxVersionEquipoActualizacion.Value == string.Empty)
                    throw new Exception("Falta que adjunte imagen de la versión actualizada.");

                if (TxEquipoAgregadoSolarwinds.Value == string.Empty)
                    throw new Exception("Falta que adjunte imagen del equipo agregado a Solarwinds.");

                if (TxConfiGuardadaSolarwinds.Value == string.Empty)
                    throw new Exception("Falta que adjunte imagen de configuración guardada en Solarwinds  (startup-config/running-config).");

                if (RbVerificacionScrip.SelectedValue.Equals(""))
                    throw new Exception("Falta que selecciones opción si ha verficado que los scripts iniciales y finales concuerdan  y he realizado las pruebas necesarias para garantizar el funcionamiento normal del equipo.");
            }
            else if (vTipo == "CORE")
            {            
                if (TxVersionRecomendada.Value == string.Empty)
                    throw new Exception("Falta que adjunte la imagen de que versión instaslada es la recomendada  por el fabricante al momento de la instalación.");

                if (TxEDC_ACS.Value == string.Empty)
                    throw new Exception("Falta que adjunte imagen que valide equipo de comunicación adicionado a ACS.");

                if (TxTablaARP.Value == string.Empty)
                    throw new Exception("Falta que adjunte imagen información capturada de la tabla ARP.");

                if (TxVersionEquipo.Value == string.Empty)
                    throw new Exception("Falta que adjunte imagen versión del equipo.");

                if (TxInterfaces.Value == string.Empty)
                    throw new Exception("Falta que adjunte imagen de la información capturada de las interfaces.");

                if (TxVerify.Value == string.Empty)
                    throw new Exception("Falta que adjunte imagen de verify de sistema operativo previo a la instalación.");

                if (TxVersionEquipoActualizacion.Value == string.Empty)
                    throw new Exception("Falta que adjunte imagen de la versión actualizada.");      

                if (TxEquipoAgregadoSolarwinds.Value == string.Empty)
                    throw new Exception("Falta que adjunte imagen del equipo agregado a Solarwinds.");

                if (TxConfiGuardadaSolarwinds.Value == string.Empty)
                    throw new Exception("Falta que adjunte imagen de configuración guardada en Solarwinds  (startup-config/running-config).");

                if (TxAltaDisponibilidad.Value == string.Empty)
                    throw new Exception("Falta que adjunte imagen de prueba de alta disponibilidad.");

                if (RbVerificacionScrip.SelectedValue.Equals(""))
                    throw new Exception("Falta que selecciones opción si ha verficado que los scripts iniciales y finales concuerdan  y he realizado las pruebas necesarias para garantizar el funcionamiento normal del equipo.");
            }

        }
        protected void BtnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                validacionEnviarLv();
                LbTitulo.Text = "Enviar lista de verificación: " + TxNodo.Text;
                LbCuerpo.Text = "¿Está seguro que desea enviar la lista de verificación?";
                Session["COMUNICACION_ESTADO_BTN_ENVIAR_MODAL"] = "Enviar LV";
                UpdatePanel4.Update();
                UpdatePanel6.Update();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "abrirModal();", true);
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
           
        }
        protected void BtnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["COMUNICACION_ESTADO_BTN_ENVIAR_MODAL"].ToString() == "Enviar LV")
                {
                    DataTable vDatos = new DataTable();
                    vDatos = (DataTable)Session["COMUNICACION_PCLV_COMPLETAR_LV_INDIVIDUAL"];
                    string vidMantenimiento = vDatos.Rows[0]["idMantenimiento"].ToString();
                    //string vidStock = vDatos.Rows[0]["idStockEDC"].ToString();
                    //string vfechaMantenimiento = Convert.ToDateTime(vDatos.Rows[0]["fechaMantenimiento"]).ToString("yyyy-MM-dd");

                    //Imagen Version Recomendada
                    String vNombreVersionRecomendada = String.Empty;
                    HttpPostedFile bufferDepositoVersionRecomendada = FuVersionRecomendada.PostedFile;
                    byte[] vFileDepositoVersionRecomendada = null;
                    String vExtensionVersionRecomendada = String.Empty;

                    if (bufferDepositoVersionRecomendada != null)
                    {
                        vNombreVersionRecomendada = FuVersionRecomendada.FileName;
                        Stream vStream = bufferDepositoVersionRecomendada.InputStream;
                        BinaryReader vReader = new BinaryReader(vStream);
                        vFileDepositoVersionRecomendada = vReader.ReadBytes((int)vStream.Length);
                        vExtensionVersionRecomendada = System.IO.Path.GetExtension(FuVersionRecomendada.FileName);
                    }

                    String vArchivoVersionRecomendada = String.Empty;
                    if (vFileDepositoVersionRecomendada != null)
                        vArchivoVersionRecomendada = Convert.ToBase64String(vFileDepositoVersionRecomendada);


                    //Imagen equipo de comunicación adicionado a ACS  
                    String vNombreEDC_ACS = String.Empty;
                    HttpPostedFile bufferDepositoEDC_ACS = FuEDC_ACS.PostedFile;
                    byte[] vFileDepositoEDC_ACS = null;
                    String vExtensionEDC_ACS = String.Empty;

                    if (bufferDepositoEDC_ACS != null)
                    {
                        vNombreEDC_ACS = FuEDC_ACS.FileName;
                        Stream vStream = bufferDepositoEDC_ACS.InputStream;
                        BinaryReader vReader = new BinaryReader(vStream);
                        vFileDepositoEDC_ACS = vReader.ReadBytes((int)vStream.Length);
                        vExtensionEDC_ACS = System.IO.Path.GetExtension(FuEDC_ACS.FileName);
                    }

                    String vArchivoEDC_ACS = String.Empty;
                    if (vFileDepositoEDC_ACS != null)
                        vArchivoEDC_ACS = Convert.ToBase64String(vFileDepositoEDC_ACS);


                    //Imagen información capturada de la tabla ARP    
                    String vNombreTablaARP = String.Empty;
                    HttpPostedFile bufferDepositoTablaARP = FuTablaARP.PostedFile;
                    byte[] vFileDepositoTablaARP = null;
                    String vExtensionTablaARP = String.Empty;

                    if (bufferDepositoTablaARP != null)
                    {
                        vNombreTablaARP = FuTablaARP.FileName;
                        Stream vStream = bufferDepositoTablaARP.InputStream;
                        BinaryReader vReader = new BinaryReader(vStream);
                        vFileDepositoTablaARP = vReader.ReadBytes((int)vStream.Length);
                        vExtensionTablaARP = System.IO.Path.GetExtension(FuTablaARP.FileName);
                    }

                    String vArchivoTablaARP = String.Empty;
                    if (vFileDepositoTablaARP != null)
                        vArchivoTablaARP = Convert.ToBase64String(vFileDepositoEDC_ACS);

                    //Imagen información capturada de la tabla MAC           
                    String vNombreTablaMAC = String.Empty;
                    HttpPostedFile bufferDepositoTablaMAC = FuTablaMAC.PostedFile;
                    byte[] vFileDepositoTablaMAC = null;
                    String vExtensionTablaMAC = String.Empty;

                    if (bufferDepositoTablaMAC != null)
                    {
                        vNombreTablaMAC = FuTablaMAC.FileName;
                        Stream vStream = bufferDepositoTablaMAC.InputStream;
                        BinaryReader vReader = new BinaryReader(vStream);
                        vFileDepositoTablaMAC = vReader.ReadBytes((int)vStream.Length);
                        vExtensionTablaMAC = System.IO.Path.GetExtension(FuTablaMAC.FileName);
                    }

                    String vArchivoTablaMAC = String.Empty;
                    if (vFileDepositoTablaMAC != null)
                        vArchivoTablaMAC = Convert.ToBase64String(vFileDepositoEDC_ACS);


                    //Imagen versión del equipo         
                    String vNombreVersionEquipo = String.Empty;
                    HttpPostedFile bufferDepositoVersionEquipo = FuVersionEquipo.PostedFile;
                    byte[] vFileDepositoVersionEquipo = null;
                    String vExtensionVersionEquipo = String.Empty;

                    if (bufferDepositoVersionEquipo != null)
                    {
                        vNombreVersionEquipo = FuVersionEquipo.FileName;
                        Stream vStream = bufferDepositoVersionEquipo.InputStream;
                        BinaryReader vReader = new BinaryReader(vStream);
                        vFileDepositoVersionEquipo = vReader.ReadBytes((int)vStream.Length);
                        vExtensionVersionEquipo = System.IO.Path.GetExtension(FuVersionEquipo.FileName);
                    }

                    String vArchivoVersionEquipo = String.Empty;
                    if (vFileDepositoVersionEquipo != null)
                        vArchivoVersionEquipo = Convert.ToBase64String(vFileDepositoEDC_ACS);


                    //Imagen Información capturada de las interfaces
                    String vNombreInterfaces = String.Empty;
                    HttpPostedFile bufferDepositoInterfaces = FuInterfaces.PostedFile;
                    byte[] vFileDepositoInterfaces = null;
                    String vExtensionInterfaces = String.Empty;

                    if (bufferDepositoInterfaces != null)
                    {
                        vNombreInterfaces = FuInterfaces.FileName;
                        Stream vStream = bufferDepositoInterfaces.InputStream;
                        BinaryReader vReader = new BinaryReader(vStream);
                        vFileDepositoInterfaces = vReader.ReadBytes((int)vStream.Length);
                        vExtensionInterfaces = System.IO.Path.GetExtension(FuInterfaces.FileName);
                    }

                    String vArchivoInterfaces = String.Empty;
                    if (vFileDepositoInterfaces != null)
                        vArchivoInterfaces = Convert.ToBase64String(vFileDepositoEDC_ACS);


                    //Imagen de los túneles DMVPN activos   
                    String vNombreDMVPN_Activos = String.Empty;
                    HttpPostedFile bufferDepositoDMVPN_Activos = FuDMVPN_Activos.PostedFile;
                    byte[] vFileDepositoDMVPN_Activos = null;
                    String vExtensionDMVPN_Activos = String.Empty;

                    if (bufferDepositoDMVPN_Activos != null)
                    {
                        vNombreDMVPN_Activos = FuDMVPN_Activos.FileName;
                        Stream vStream = bufferDepositoDMVPN_Activos.InputStream;
                        BinaryReader vReader = new BinaryReader(vStream);
                        vFileDepositoDMVPN_Activos = vReader.ReadBytes((int)vStream.Length);
                        vExtensionDMVPN_Activos = System.IO.Path.GetExtension(FuDMVPN_Activos.FileName);
                    }

                    String vArchivoDMVPN_Activos = String.Empty;
                    if (vFileDepositoDMVPN_Activos != null)
                        vArchivoDMVPN_Activos = Convert.ToBase64String(vFileDepositoEDC_ACS);

                    //Imagen de verify de sistema operativo previo a la instalacion
                    String vNombreVerify = String.Empty;
                    HttpPostedFile bufferDepositoVerify = FuVerify.PostedFile;
                    byte[] vFileDepositoVerify = null;
                    String vExtensionVerify = String.Empty;

                    if (bufferDepositoVerify != null)
                    {
                        vNombreVerify = FuVerify.FileName;
                        Stream vStream = bufferDepositoVerify.InputStream;
                        BinaryReader vReader = new BinaryReader(vStream);
                        vFileDepositoVerify = vReader.ReadBytes((int)vStream.Length);
                        vExtensionVerify = System.IO.Path.GetExtension(FuVerify.FileName);
                    }

                    String vArchivoVerify = String.Empty;
                    if (vFileDepositoVerify != null)
                        vArchivoVerify = Convert.ToBase64String(vFileDepositoEDC_ACS);


                    //Imagen de versión del equipo despues de la actualizacion
                    String vNombreVersionEquipoActualizacion = String.Empty;
                    HttpPostedFile bufferDepositoVersionEquipoActualizacion = FuVersionEquipoActualizacion.PostedFile;
                    byte[] vFileDepositoVersionEquipoActualizacion = null;
                    String vExtensionVersionEquipoActualizacion = String.Empty;

                    if (bufferDepositoVersionEquipoActualizacion != null)
                    {
                        vNombreVersionEquipoActualizacion = FuVersionEquipoActualizacion.FileName;
                        Stream vStream = bufferDepositoVersionEquipoActualizacion.InputStream;
                        BinaryReader vReader = new BinaryReader(vStream);
                        vFileDepositoVersionEquipoActualizacion = vReader.ReadBytes((int)vStream.Length);
                        vExtensionVersionEquipoActualizacion = System.IO.Path.GetExtension(FuVersionEquipoActualizacion.FileName);
                    }

                    String vArchivoVersionEquipoActualizacion = String.Empty;
                    if (vFileDepositoVersionEquipoActualizacion != null)
                        vArchivoVersionEquipoActualizacion = Convert.ToBase64String(vFileDepositoEDC_ACS);



                    //Imagen ejecutar comando “show authentication sessions”
                    String vNombreShowAuthentication = String.Empty;
                    HttpPostedFile bufferDepositoShowAuthentication = FuShowAuthentication.PostedFile;
                    byte[] vFileDepositoShowAuthentication = null;
                    String vExtensionShowAuthentication = String.Empty;

                    if (bufferDepositoShowAuthentication != null)
                    {
                        vNombreShowAuthentication = FuShowAuthentication.FileName;
                        Stream vStream = bufferDepositoShowAuthentication.InputStream;
                        BinaryReader vReader = new BinaryReader(vStream);
                        vFileDepositoShowAuthentication = vReader.ReadBytes((int)vStream.Length);
                        vExtensionShowAuthentication = System.IO.Path.GetExtension(FuShowAuthentication.FileName);
                    }

                    String vArchivoShowAuthentication = String.Empty;
                    if (vFileDepositoShowAuthentication != null)
                        vArchivoShowAuthentication = Convert.ToBase64String(vFileDepositoEDC_ACS);


                    //Imagen equipo agregado a Solarwinds
                    String vNombreEquipoAgregadoSolarwinds = String.Empty;
                    HttpPostedFile bufferDepositoEquipoAgregadoSolarwinds = FuEquipoAgregadoSolarwinds.PostedFile;
                    byte[] vFileDepositoEquipoAgregadoSolarwinds = null;
                    String vExtensionEquipoAgregadoSolarwinds = String.Empty;

                    if (bufferDepositoEquipoAgregadoSolarwinds != null)
                    {
                        vNombreEquipoAgregadoSolarwinds = FuEquipoAgregadoSolarwinds.FileName;
                        Stream vStream = bufferDepositoEquipoAgregadoSolarwinds.InputStream;
                        BinaryReader vReader = new BinaryReader(vStream);
                        vFileDepositoEquipoAgregadoSolarwinds = vReader.ReadBytes((int)vStream.Length);
                        vExtensionEquipoAgregadoSolarwinds = System.IO.Path.GetExtension(FuEquipoAgregadoSolarwinds.FileName);
                    }

                    String vArchivoEquipoAgregadoSolarwinds = String.Empty;
                    if (vFileDepositoEquipoAgregadoSolarwinds != null)
                        vArchivoEquipoAgregadoSolarwinds = Convert.ToBase64String(vFileDepositoEDC_ACS);


                    //Imagen de configuración guardada en Solarwinds
                    String vNombreConfiGuardadaSolarwinds = String.Empty;
                    HttpPostedFile bufferDepositoConfiGuardadaSolarwinds = FuConfiGuardadaSolarwinds.PostedFile;
                    byte[] vFileDepositoConfiGuardadaSolarwinds = null;
                    String vExtensionConfiGuardadaSolarwinds = String.Empty;

                    if (bufferDepositoConfiGuardadaSolarwinds != null)
                    {
                        vNombreConfiGuardadaSolarwinds = FuConfiGuardadaSolarwinds.FileName;
                        Stream vStream = bufferDepositoConfiGuardadaSolarwinds.InputStream;
                        BinaryReader vReader = new BinaryReader(vStream);
                        vFileDepositoConfiGuardadaSolarwinds = vReader.ReadBytes((int)vStream.Length);
                        vExtensionConfiGuardadaSolarwinds = System.IO.Path.GetExtension(FuConfiGuardadaSolarwinds.FileName);
                    }

                    String vArchivoConfiGuardadaSolarwinds = String.Empty;
                    if (vFileDepositoConfiGuardadaSolarwinds != null)
                        vArchivoConfiGuardadaSolarwinds = Convert.ToBase64String(vFileDepositoEDC_ACS);


                    //Imagen de Alta Disponibilidad CORE
                    String vNombreAltaDisponibilidad = String.Empty;
                    HttpPostedFile bufferDepositoAltaDisponibilidad = FuAltaDisponibilidad.PostedFile;
                    byte[] vFileDepositoAltaDisponibilidad = null;
                    String vExtensionAltaDisponibilidad = String.Empty;

                    if (bufferDepositoAltaDisponibilidad != null)
                    {
                        vNombreAltaDisponibilidad = FuAltaDisponibilidad.FileName;
                        Stream vStream = bufferDepositoAltaDisponibilidad.InputStream;
                        BinaryReader vReader = new BinaryReader(vStream);
                        vFileDepositoAltaDisponibilidad = vReader.ReadBytes((int)vStream.Length);
                        vExtensionAltaDisponibilidad = System.IO.Path.GetExtension(FuAltaDisponibilidad.FileName);
                    }

                    String vArchivoAltaDisponibilidad = String.Empty;
                    if (vFileDepositoAltaDisponibilidad != null)
                        vArchivoAltaDisponibilidad = Convert.ToBase64String(vFileDepositoEDC_ACS);



                    //INSERTAR IMAGENES ANTES DEL MANTENIMIENTO
                    String vQuery1 = "STEISP_COMUNICACION_CompletarLV 3," + vidMantenimiento
                        + ",'" + vArchivoVersionRecomendada
                        + "','" + vExtensionVersionRecomendada
                        + "','" + vNombreVersionRecomendada

                        + "','" + vArchivoEDC_ACS
                        + "','" + vExtensionEDC_ACS
                        + "','" + vNombreEDC_ACS

                        + "','" + vArchivoTablaARP
                        + "','" + vExtensionTablaARP
                        + "','" + vNombreTablaARP

                        + "','" + vArchivoTablaMAC
                        + "','" + vExtensionTablaMAC
                        + "','" + vNombreTablaMAC

                        + "','" + vArchivoVersionEquipo
                        + "','" + vExtensionVersionEquipo
                        + "','" + vNombreVersionEquipo

                        + "','" + vArchivoInterfaces
                        + "','" + vExtensionInterfaces
                        + "','" + vNombreInterfaces

                        + "','" + vArchivoDMVPN_Activos
                        + "','" + vExtensionDMVPN_Activos
                        + "','" + vNombreDMVPN_Activos

                        + "','" + vArchivoVerify
                        + "','" + vExtensionVerify
                        + "','" + vNombreVerify + "'";
                    Int32 vInformacion1 = vConexion.ejecutarSql(vQuery1);


                    //INSERTAR IMAGENES DESPUES DEL MANTENIMIENTO
                    String vQuery2 = "STEISP_COMUNICACION_CompletarLV 4,'"
                        + vArchivoVersionEquipoActualizacion
                        + "','" + vExtensionVersionEquipoActualizacion
                        + "','" + vNombreVersionEquipoActualizacion

                        + "','" + vArchivoShowAuthentication
                        + "','" + vExtensionShowAuthentication
                        + "','" + vNombreShowAuthentication

                        + "','" + vArchivoEquipoAgregadoSolarwinds
                        + "','" + vExtensionEquipoAgregadoSolarwinds
                        + "','" + vNombreEquipoAgregadoSolarwinds

                        + "','" + vArchivoConfiGuardadaSolarwinds
                        + "','" + vExtensionConfiGuardadaSolarwinds
                        + "','" + vNombreConfiGuardadaSolarwinds

                        + "','" + vArchivoAltaDisponibilidad
                        + "','" + vExtensionAltaDisponibilidad
                        + "','" + vNombreAltaDisponibilidad
                        + "'," + RbVerificacionScrip.SelectedValue
                        + "," + vidMantenimiento;
                    Int32 vInformacion2 = vConexion.ejecutarSql(vQuery2);


                    //ACTUALIZAR TABLA DE MANTENIMIENTO
                    String vQuery3 = "STEISP_COMUNICACION_CompletarLV 5," + vidMantenimiento
                        + ",'" + TxNewIOSVersion.Text
                        + "','" + TxNewIOSImagen.Text
                        + "','" + TxObservaciones.Text + "'";
                    Int32 vInformacion3 = vConexion.ejecutarSql(vQuery3);


                    if (vInformacion1 == 1 && vInformacion2 == 1 && vInformacion3 == 1 )
                    {
                        limpiarEnvioLV();
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "cerrarModal();", true);
                        Response.Redirect("/pages/mantenimiento/pendientesCompletarLV.aspx?ex=1");
                    }
                    else
                    {
                        limpiarEnvioLV();
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "cerrarModal();", true);
                        Response.Redirect("/pages/mantenimiento/pendientesCompletarLV.aspx?ex=2");
                    }
                }else if(Session["COMUNICACION_ESTADO_BTN_ENVIAR_MODAL"].ToString() == "Aprobar LV")
                {
                    DataTable vDatos = new DataTable();
                    vDatos = (DataTable)Session["COMUNICACION_PALV_COMPLETAR_LV_INDIVIDUAL"];
                    string vidMantenimiento = vDatos.Rows[0]["idMantenimiento"].ToString();
                    string vidStock = vDatos.Rows[0]["idStockEDC"].ToString();
                    string vfechaMantenimiento = Convert.ToDateTime(vDatos.Rows[0]["fechaMantenimiento"]).ToString("yyyy-MM-dd");
                    
                    //ACTUALIZAR TABLA DE MANTENIMIENTO
                    String vQuery1 = "STEISP_COMUNICACION_AprobalLV 4," + vidMantenimiento
                        + "," + RblAprobarLV.SelectedValue
                        + ",'" + TxMotivoCancelacionLV.Text
                        + "','" + Session["COMUNICACION_LV_ESTADO_BD"] + "'";
                    Int32 vInformacion1 = vConexion.ejecutarSql(vQuery1);


                    //ACTUALIZAR TABLA Inventario_StockEDC
                    String vQuery2 = "STEISP_COMUNICACION_AprobalLV 3," + vidStock
                        + ",'" + TxNewIOSImagen.Text
                        + "','" + TxNewIOSVersion.Text
                        + "','" + vfechaMantenimiento + "','" + Session["USUARIO"].ToString() + "'";
                    Int32 vInformacion2 = vConexion.ejecutarSql(vQuery2);


                    //CREAR REGISTRO TABLA SUSCRIPCION
                    string vCorreos = "";
                    String vQueryCorreoParticipantes = "STEISP_COMUNICACION_AprobalLV 5,'" + vidMantenimiento + "'";
                    DataTable vDatosCorreoParticipantes = vConexion.obtenerDataTable(vQueryCorreoParticipantes);

                    if (vDatosCorreoParticipantes.Rows.Count > 0)
                    {
                        for (int num = 0; num < vDatosCorreoParticipantes.Rows.Count; num++)
                        {
                          vCorreos = vCorreos + vDatosCorreoParticipantes.Rows[0]["correo"].ToString() + ";";
                        }
                    }

                    String vQueryCorreoJefes = "STEISP_COMUNICACION_AprobalLV 6,'" + vidMantenimiento + "'";
                    DataTable vDatosCorreoJefes = vConexion.obtenerDataTable(vQueryCorreoJefes);
                    if (vDatosCorreoJefes.Rows.Count > 0)
                    {
                        for (int num = 0; num < vDatosCorreoJefes.Rows.Count; num++)
                        {
                            vCorreos = vCorreos + vDatosCorreoJefes.Rows[0]["CorreoJefe"].ToString() + ";";
                        }
                    }

                    String vQueryCorreoResponsable = "STEISP_COMUNICACION_AprobalLV 7,'" + vidMantenimiento + "'";
                    DataTable vDatosCorreoResponsable = vConexion.obtenerDataTable(vQueryCorreoResponsable);
                    string vCorreoResponsable = vDatosCorreoResponsable.Rows[0]["correo"].ToString();


                    String vQuerySuscripcion = "STEISP_COMUNICACION_AprobalLV 8,'" + vCorreos + "','" + vCorreoResponsable + "','LV Mantenimiento Equipo Comunicacion',0," + "'Buen dia<br> Se adjunta lista de verificación, cualquier anomalia presentada en la lista de verificacion favor retornar este correo. <br><br> Infatlan 2020 <br> La información contenida en este correo electrónico, así como cualquier documento adjunto va dirigido personalmente y, por lo tanto, únicamente a su destinatario. Su uso es exclusivo del individuo o entidad a quien va dirigido. Si usted no es el destinatario indicado (1): ha de saber que cualquier divulgación, copia, distribución o cualquier acción con respecto al contenido de esta información está estrictamente prohibida y puede ser ilegal y (2): le rogamos que informe inmediatamente al remitente, además de borrar todas las copias.'," + vidMantenimiento;
                    Int32 vInfoSuscripcion = vConexion.ejecutarSql(vQuerySuscripcion);


                    if (vInformacion1 == 1 && vInformacion2 == 1 && vInfoSuscripcion == 1)
                    {
                        limpiarAprobarLV();
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "cerrarModal();", true);
                        Response.Redirect("/pages/mantenimiento/pendientesAprobarLV.aspx?ex=1");
                    }
                    else
                    {
                        limpiarAprobarLV();
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "cerrarModal();", true);
                        Response.Redirect("/pages/mantenimiento/pendientesAprobarLV.aspx?ex=2");
                    }
                }

            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
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
        private void limpiarEnvioLV()
        {
            TxNewIOSVersion.Text = String.Empty;
            TxNewIOSImagen.Text = String.Empty;
            TxObservaciones.Text = String.Empty;
            RbVerificacionScrip.SelectedIndex = -1;
            TxVersionRecomendada.Value = String.Empty;
            TxEDC_ACS.Value = String.Empty;
            TxTablaARP.Value = String.Empty;
            TxTablaMAC.Value = String.Empty;
            TxVersionEquipo.Value = String.Empty;
            TxDMVPN_Activos.Value = String.Empty;
            TxInterfaces.Value = String.Empty;
            TxVerify.Value = String.Empty;
            TxVersionEquipoActualizacion.Value = String.Empty;
            TxShowAuthentication.Value = String.Empty;
            TxEquipoAgregadoSolarwinds.Value = String.Empty;
            TxConfiGuardadaSolarwinds.Value = String.Empty;
            TxAltaDisponibilidad.Value = String.Empty;
        }
        void cargarDataAprobarLV()
        {
            try
            {
                DataTable vDatos = new DataTable();
                vDatos = (DataTable)Session["COMUNICACION_PALV_COMPLETAR_LV_INDIVIDUAL"];
                TxFechaMantenimiento.Text = vDatos.Rows[0]["fechaMantenimiento"].ToString();
                TxNodo.Text = vDatos.Rows[0]["nombreNodo"].ToString();
                TxZona.Text = vDatos.Rows[0]["regiones"].ToString();
                TxIp.Text = vDatos.Rows[0]["ip"].ToString();
                TxImagen.Text = vDatos.Rows[0]["IOSImage"].ToString();
                TXVersion.Text = vDatos.Rows[0]["IOSVersion"].ToString();
                TxTipo.Text = vDatos.Rows[0]["tipoStock"].ToString();
                TxDirección.Text = vDatos.Rows[0]["direccion"].ToString();
                TxSysAid.Text = vDatos.Rows[0]["sysAid"].ToString();
                TxControlCambio.Text = vDatos.Rows[0]["controlCambio"].ToString();
                TxHoraInicio.Text = vDatos.Rows[0]["horaInicio"].ToString();
                TxHoraFin.Text = vDatos.Rows[0]["horaFin"].ToString();
                TxImpacto.Text = vDatos.Rows[0]["impacto"].ToString();
                TxNewIOSVersion.Text = vDatos.Rows[0]["newVersionIOS"].ToString();
                TxNewIOSImagen.Text = vDatos.Rows[0]["newImagenIOS"].ToString();
                TxObservaciones.Text = vDatos.Rows[0]["observaciones"].ToString();
                TxUltimoMantenimiento.Text = vDatos.Rows[0]["ultimoMantenimiento"].ToString();
                TxDuracionMan.Text = vDatos.Rows[0]["tiempoEstimado"].ToString();

                ImgPreviewVersionRecomendada.Src = "data:image;base64," + vDatos.Rows[0]["versionRecomendada"].ToString();
                ImgPreviewEDC_ACS.Src = "data:image;base64," + vDatos.Rows[0]["edcAgregadoACS"].ToString();
                ImgPreviewTablaARP.Src = "data:image;base64," + vDatos.Rows[0]["tablaARP"].ToString();
                ImgPreviewTablaMAC.Src = "data:image;base64," + vDatos.Rows[0]["tablaMAC"].ToString();
                ImgPreviewVersionEquipo.Src = "data:image;base64," + vDatos.Rows[0]["versionEquipo"].ToString();
                ImgPreviewInterfaces.Src = "data:image;base64," + vDatos.Rows[0]["interfaces"].ToString();
                ImgPreviewDMVPN_Activos.Src = "data:image;base64," + vDatos.Rows[0]["tunelesDMVPN"].ToString();
                ImgPreviewVerify.Src = "data:image;base64," + vDatos.Rows[0]["verifySistema"].ToString();
                ImgPreviewVersionEquipoActualizacion.Src = "data:image;base64," + vDatos.Rows[0]["versionEquipoActualizada"].ToString();
                ImgPreviewShowAuthentication.Src = "data:image;base64," + vDatos.Rows[0]["showAuthentication"].ToString();
                ImgPreviewEquipoAgregadoSolarwinds.Src = "data:image;base64," + vDatos.Rows[0]["agregadoSolarwinds"].ToString();
                ImgPreviewConfiGuardadaSolarwinds.Src = "data:image;base64," + vDatos.Rows[0]["confiGuardadaSolarwinds"].ToString();
                ImgPreviewAltaDisponibilidad.Src = "data:image;base64," + vDatos.Rows[0]["altaDisponibilidadCORE"].ToString();

                RbVerificacionScrip.SelectedValue= vDatos.Rows[0]["verificacionScrips"].ToString();
                mostrarCamposImagen();
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
        void bloquearCamposAprobarLV()
        {
            FuVersionRecomendada.Visible = false;
            FuEDC_ACS.Visible = false;
            FuTablaARP.Visible = false;
            FuTablaMAC.Visible = false;
            FuVersionEquipo.Visible = false;
            FuInterfaces.Visible = false;
            FuDMVPN_Activos.Visible = false;
            FuVerify.Visible = false;
            FuVersionEquipoActualizacion.Visible = false;
            FuShowAuthentication.Visible = false;
            FuEquipoAgregadoSolarwinds.Visible = false;
            FuConfiGuardadaSolarwinds.Visible = false;
            FuAltaDisponibilidad.Visible = false;

            RbVerificacionScrip.Enabled = false;
            TxNewIOSVersion.ReadOnly = true;
            TxNewIOSImagen.ReadOnly = true;
            TxObservaciones.ReadOnly = true;

            DivEnviarLV.Visible = false;
            DivAprobarLV.Visible = true;
        }
        protected void RblAprobarLV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RblAprobarLV.SelectedValue.Equals("1"))
            {
                DivEtiqueta.Visible = false;
                DivTexto.Visible = false;
                LbCuerpo.Text = "¿Está seguro que desea aprobar la lista de verificación?";
                LbTitulo.Text = "Aprobar LV: " + TxNodo.Text;
                Session["COMUNICACION_LV_ESTADO_BD"] = "6";
                TxMotivoCancelacionLV.Text = String.Empty;
            }
            else
            {
                DivEtiqueta.Visible = true;
                DivTexto.Visible = true;
                LbCuerpo.Text = "¿Está seguro que desea regresar la lista de verificación?";
                LbTitulo.Text = "Regresar LV: " + TxNodo.Text;
                Session["COMUNICACION_LV_ESTADO_BD"] = "5";
                TxMotivoCancelacionLV.Text = String.Empty;
            }
        }
        private void validacionesAprobarLV()
        {
            if (RblAprobarLV.SelectedValue.Equals(""))
                throw new Exception("Falta completar opción ¿Desea aprobar LV?.");

            if (RblAprobarLV.SelectedValue.Equals("0") && (TxMotivoCancelacionLV.Text == "" || TxMotivoCancelacionLV.Text == string.Empty))
                throw new Exception("Falta que ingrese el motivo de cancelacion de la lista de verificación");
        }
        void limpiarAprobarLV()
        {
            RblAprobarLV.SelectedIndex = -1;
            TxMotivoCancelacionLV.Text = string.Empty;
            Session["COMUNICACION_ESTADO_BTN_ENVIAR_MODAL"] = null;
            Session["COMUNICACION_LV_ESTADO_BD"] = null;
        }
        protected void BtnEnviarAprobacion_Click(object sender, EventArgs e)
        {
            try
            {
                validacionesAprobarLV();             
                UpdatePanel4.Update();
                UpdatePanel6.Update();
                Session["COMUNICACION_ESTADO_BTN_ENVIAR_MODAL"] = "Aprobar LV";                
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "abrirModal();", true);
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
    }

}