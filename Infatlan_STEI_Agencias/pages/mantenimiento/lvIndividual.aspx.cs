using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using Infatlan_STEI_Agencias.classes;
using System.Configuration;
using System.Drawing.Imaging;
using System.Drawing;

namespace Infatlan_STEI_Agencias.pages
{
    public partial class LvIndividual : System.Web.UI.Page
    {
        db vConexion = new db();
        public void Mensaje(string vMensaje, WarningType type){
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        protected void Page_Load(object sender, EventArgs e){
            String vEx = Request.QueryString["ex"];
            
            if (!Page.IsPostBack){
                if (Convert.ToBoolean(Session["AUTH"])){
                    if (vEx != null){
                        if (vEx.Equals("2")){
                            modoVistaCampos(true);
                            cargarDataVista();
                            BtnEnviarLv.Visible = false;
                            BtnDevolver.Visible = true;
                            BtnAprobar.Visible = true;
                            DDLTipoAgencia.Enabled = false;
                            TxtTelefono.ReadOnly = true;
                            TxLatitud.ReadOnly = true;
                            TxLongitud.ReadOnly = true;
                            txtDireccion.ReadOnly = true;
                        }
                        else if (vEx.Equals("1")){
                            cargarDataLlenado();
                            //OcultarTarjeta();
                            mostrarAsteriscos();
                            RblClimatizacionAdecuada.SelectedValue = "1";
                            RblUPS.SelectedValue = "1";
                            RbPolvoSuciedad.SelectedValue = "1";
                            RblHumedadSustancias.SelectedValue = "1";
                            RblRoboDaño.SelectedValue = "1";
                            RblElementosAjenos.SelectedValue = "1";
                        }
                        else if (vEx.Equals("3")){
                            cargarDataModificar();
                            //OcultarTarjeta();
                            mostrarAsteriscos();
                            habilitarFU();
                        }
                    }
                    cargar();
                }else {
                    Response.Redirect("/login.aspx");
                }
            }
        }

        //**********************************************************************************************************************//
        //*****************************************  APROBACION LISTA DE VERIFICACION  *****************************************//
        //*********************************************************************************************************************//
        void modoVistaCampos(Boolean vActivo)
        {
            TxFechaMant.ReadOnly = vActivo;
            TxSysAid.ReadOnly = vActivo;
            TxLugar.ReadOnly = vActivo;
            TxArea.ReadOnly = vActivo;
            TxCodigoAgencia.ReadOnly = vActivo;
            TxManEquipoComunicacion.ReadOnly = vActivo;
            TxMotivo.ReadOnly = vActivo;
            TxImpacto.ReadOnly = vActivo;
            TxHoraSalidaINFA.ReadOnly = vActivo;
            TxHoraLlegadaINFA.ReadOnly = vActivo;
            TxHoraInicioMant.ReadOnly = vActivo;
            TxHoraFinMant.ReadOnly = vActivo;
            TxNombreTecnicoResponsable.ReadOnly = vActivo;
            TxIdentidadTecnicoResponsable.ReadOnly = vActivo;
            TxCantMaquinas.ReadOnly = vActivo;
            TxCantImpresoraFinanciera.ReadOnly = vActivo;
            TxCantEscaner.ReadOnly = vActivo;
            TxCantDatacard.ReadOnly = vActivo;
            
            //RBLManEquipoComu.Enabled = false;
            FuImageNoMantEquipoComu.Visible = false;            
            TxMotivoNoMantEquipoComu.ReadOnly = true;
          

            RBProbaronEquipo.Enabled = false;
            TxMotivoNoProbaronEquipo.ReadOnly = true;

            RblClimatizacionAdecuada.Enabled = false;
            FuClimatizacion.Visible = false;

            RblUPS.Enabled = false;
            FuUPS.Visible = false;

            RbPolvoSuciedad.Enabled = false;
            FuPolvoSuciedad.Visible = false;

            RblHumedadSustancias.Enabled = false;
            FuHumedadSustancias.Visible = false;

            RblRoboDaño.Enabled = false;
            FuRoboDaño.Visible = false;

            RblElementosAjenos.Enabled = false;
            FuElementosAjenos.Visible = false;

            FuRack.Visible = false; 
            FuEspacioFisico.Visible = false;

            TxObservacionesGenerales.ReadOnly = true;
           
            BtnEnviarLv.Visible = false;
           
        }

        void cargar()
        {
            DDLTipoAgencia.Items.Clear();
            String vQuery = "STEISP_AGENCIA_CreacionAgencia 1";
            DataTable vDatos = vConexion.obtenerDataTable(vQuery);
            DDLTipoAgencia.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
            if (vDatos.Rows.Count > 0)
            {
                foreach (DataRow item in vDatos.Rows)
                {
                    DDLTipoAgencia.Items.Add(new ListItem { Value = item["idTipoAgencia"].ToString(), Text = item["nombre"].ToString() });                  
                }
            }

            String vQuery2 = "[STEISP_AGENCIA_CompletarListaVerificacion] 13,'"+TxCodigoAgencia.Text+"'";
            DataTable vDatos2 = vConexion.obtenerDataTable(vQuery2);
            foreach (DataRow item in vDatos2.Rows)
            {
                DDLTipoAgencia.SelectedValue = item["idTipoAgencia"].ToString();
                TxtTelefono.Text = item["telefono"].ToString();
                txtDireccion.Text = item["direccion"].ToString();
                TxLatitud.Text = item["lat"].ToString();
                TxLongitud.Text = item["lng"].ToString();
            }
        }
        
        void cargarDataVista()
        {
            try
            {
                //DATOS GENERALESs
                DataTable vDatos = new DataTable();
                vDatos = (DataTable)Session["AG_LvPA_DATOS_GENERALES"];
                TxFechaMant.Text = vDatos.Rows[0]["fecha"].ToString();        
                TxSysAid.Text = vDatos.Rows[0]["sysAid"].ToString();
                TxLugar.Text = vDatos.Rows[0]["Lugar"].ToString();
                TxArea.Text = vDatos.Rows[0]["Area"].ToString();
                TxCodigoAgencia.Text = vDatos.Rows[0]["Cod_Agencia"].ToString();
                TxHoraSalidaINFA.Text = vDatos.Rows[0]["horaSalidaInfatlan"].ToString();
                TxHoraLlegadaINFA.Text = vDatos.Rows[0]["horaLlegadaInfatlan"].ToString();
                TxHoraInicioMant.Text = vDatos.Rows[0]["horaMantenimientoInicio"].ToString();
                TxHoraFinMant.Text = vDatos.Rows[0]["horaManteniminetoFinal"].ToString();
                TxNombreTecnicoResponsable.Text = vDatos.Rows[0]["Responsable"].ToString();
            

            //TECNICO RESPONSABLE  
                DataTable vDatos1 = new DataTable();
                vDatos1 = (DataTable)Session["AG_LvPA_DATOS_TECNICO_RESPONSABLE"];
                TxIdentidadTecnicoResponsable.Text = vDatos1.Rows[0]["identidad"].ToString();


            //TECNICOS PARTICIPANTES
                DataTable vDatosTecnicosParticipantes = (DataTable)Session["AG_LvPA_DATOS_TECNICOS_PARTICIPANTES"];
                GVTecnicosParticipantes.DataSource = vDatosTecnicosParticipantes;
           
                if (vDatosTecnicosParticipantes.Rows.Count > 0){
                    GVTecnicosParticipantes.DataBind();
                    UPTecnicosParticipantes.Update();

                    DivTecnicosParticipantes.Visible = true;
                    DivAlertaTecnicosParticipantes.Visible = false;
                }else{
                    DivTecnicosParticipantes.Visible = true;
                    DivAlertaTecnicosParticipantes.Visible = true;
                }

            //SECCION DATOS TECNICOS PREGUNTA
                DataTable vDatos2 = new DataTable();
                vDatos2 = (DataTable)Session["AG_LvPA_DATOS_TECNICOS_PREGUNTAS"];

                TxCantMaquinas.Text = vDatos2.Rows[0]["cantMaquinas"].ToString();
                TxCantImpresoraFinanciera.Text = vDatos2.Rows[0]["cantImpresorasFinancieras"].ToString();
                TxCantEscaner.Text = vDatos2.Rows[0]["cantEscanerFenix"].ToString();
                TxCantDatacard.Text = vDatos2.Rows[0]["cantDatacards"].ToString();

                string RBLManEquipoComuRespuesta = vDatos2.Rows[0]["realizoMantEquipoComu"].ToString();
                TxMotivoNoMantEquipoComu.Text = vDatos2.Rows[0]["motivoNoRealizoMantEquipoComu"].ToString();

                String vDocumentoNoMantEquipoComunicacion = vDatos2.Rows[0]["fotoNoManEquipoComu"].ToString();
                string srcNoMantEquipoComunicacion = "data:image;base64," + vDocumentoNoMantEquipoComunicacion;
                ImgPreviewNoMantEquipoComu.Src = srcNoMantEquipoComunicacion;
                //if (RBLManEquipoComuRespuesta.Equals("True"))
                //{
                //    //RBLManEquipoComu.SelectedValue = "1";
                //    ImgPreviewNoMantEquipoComu.Visible = true;
                //}
                //else
                //{
                //    //RBLManEquipoComu.SelectedValue = "0";
                //    ImgPreviewNoMantEquipoComu.Visible = false;
                //    TxMotivoNoMantEquipoComu.Visible = true;
                //}


                //SECCION PRUEBAS DE PC
                DataTable vDatos3 = new DataTable();
                vDatos3 = (DataTable)Session["AG_LvPA_DATOS_PRUEBAS_PC"];

                string RBProbaronEquipoRespuesta= vDatos3.Rows[0]["proboTodoEquipo"].ToString();
                TxMotivoNoProbaronEquipo.Text = vDatos3.Rows[0]["motivoNoProboTodoEquipo"].ToString();
                if (RBProbaronEquipoRespuesta.Equals("True")){
                    RBProbaronEquipo.SelectedValue = "1";
                    TxMotivoNoProbaronEquipo.Visible = false;           
                } else {
                    RBProbaronEquipo.SelectedValue = "0";
                    TxMotivoNoProbaronEquipo.Visible = true;
                }


            //SECCION EQUIPO DE COMUNICACION
                DataTable vDatos4 = new DataTable();
                vDatos4 = (DataTable)Session["AG_LvPA_DATOS_EQUIPO_COMUNICACION"];

                string RblClimatizacionAdecuadaRespuesta= vDatos4.Rows[0]["climatizacionAdecuada"].ToString();
                String vDocumentoClimatizacion = vDatos4.Rows[0]["fotoClimatizacionAdecuada"].ToString();
                string srcClimatizacion = "data:image;base64," + vDocumentoClimatizacion;
                ImgPreviewClimatizacion.Src = srcClimatizacion;
                ImgPreviewNoMantEquipoComu.Visible = true;

                if (RblClimatizacionAdecuadaRespuesta.Equals("True"))
                {
                    RblClimatizacionAdecuada.SelectedValue = "1";
                    //ImgPreviewClimatizacion.Visible = true;
                }
                else
                {
                    RblClimatizacionAdecuada.SelectedValue = "0";
                    //ImgPreviewClimatizacion.Visible = false;
                }


                string RblUPSRespuesta = vDatos4.Rows[0]["energiaElectricaUPS"].ToString();
                String vDocumentoUPS = vDatos4.Rows[0]["fotoEnergiaElectricaUPS"].ToString();
                string srcUPS = "data:image;base64," + vDocumentoUPS;
                ImgPreviewUPS.Src = srcUPS;

                if (RblUPSRespuesta.Equals("True"))
                {
                    RblUPS.SelectedValue = "1";
                    //ImgPreviewUPS.Visible = true;
                }
                else
                {
                    RblUPS.SelectedValue = "0";
                    //ImgPreviewUPS.Visible = false;
                }

                //SECCION ENTORNO COMUNICACION
                DataTable vDatos5 = new DataTable();
                vDatos5 = (DataTable)Session["AG_LvPA_DATOS_ENTORNO_COMUNICACION"];

                string RbPolvoSuciedadRespuesta = vDatos5.Rows[0]["expuestoPolvoSuciedad"].ToString();
                String vDocumentoPolvoSuciedad = vDatos5.Rows[0]["fotoExpuestoPolvoSuciedad"].ToString();
                string srcPolvoSuciedad = "data:image;base64," + vDocumentoPolvoSuciedad;
                ImgPreviewPolvoSuciedad.Src = srcPolvoSuciedad;

                if (RbPolvoSuciedadRespuesta.Equals("True"))
                {
                    RbPolvoSuciedad.SelectedValue = "1";
                    //ImgPreviewPolvoSuciedad.Visible = true;
                }
                else
                {
                    RbPolvoSuciedad.SelectedValue = "0";
                    //ImgPreviewPolvoSuciedad.Visible = false;
                }


                string RblHumedadSustanciasRespuesta = vDatos5.Rows[0]["rastrosHumedadSustancias"].ToString();
                String vDocumentoHumedadSustancias = vDatos5.Rows[0]["fotoRastrosHumedadSustancias"].ToString();
                string srcHumedadSustancias = "data:image;base64," + vDocumentoHumedadSustancias;
                ImgPreviewHumedadSustancias.Src = srcHumedadSustancias;

                if (RblHumedadSustanciasRespuesta.Equals("True"))
                {
                    RblHumedadSustancias.SelectedValue = "1";
                    //ImgPreviewHumedadSustancias.Visible = true;
                }
                else
                {
                    RblHumedadSustancias.SelectedValue = "0";
                    //ImgPreviewHumedadSustancias.Visible = false;
                }


                string RblRoboDañoRespuesta = vDatos5.Rows[0]["expuestoRoboDaño"].ToString();
                String vDocumentoRoboDaño = vDatos5.Rows[0]["fotoExpuestoRoboDaño"].ToString();
                string srcRoboDaño= "data:image;base64," + vDocumentoRoboDaño;
                ImgPreviewRoboDaño.Src = srcRoboDaño;

                if (RblRoboDañoRespuesta.Equals("True"))
                {
                    RblRoboDaño.SelectedValue = "1";
                    //ImgPreviewRoboDaño.Visible = true;
                }
                else
                {
                    RblRoboDaño.SelectedValue = "0";
                    //ImgPreviewRoboDaño.Visible = false;
                }


                string RblElementosAjenosRespuesta = vDatos5.Rows[0]["encontroElementosExtraños"].ToString();
                String vDocumentoElementosAjenos = vDatos5.Rows[0]["fotoElementosExtraños"].ToString();
                string srcElementosAjenos = "data:image;base64," + vDocumentoElementosAjenos;
                ImgPreviewElementosAjenos.Src = srcElementosAjenos;

                if (RblRoboDañoRespuesta.Equals("True"))
                {
                    RblElementosAjenos.SelectedValue = "1";
                    //ImgPreviewElementosAjenos.Visible = true;
                }
                else
                {
                    RblElementosAjenos.SelectedValue = "0";
                    //ImgPreviewElementosAjenos.Visible = false;
                }


                //IMAGENES OBLIGATORIAS
                DataTable vDatos6 = new DataTable();
                vDatos6 = (DataTable)Session["AG_LvPA_DATOS_IMAGENES_OBLIGATORIAS"];

                String vDocumentoRack = vDatos6.Rows[0]["fotoRackComunicacion"].ToString();
                string srcRack= "data:image;base64," + vDocumentoRack;
                ImgPreviewRack.Src = srcRack;
                //ImgPreviewRack.Visible = true;

                String vDocumentoEntorno = vDatos6.Rows[0]["fotoEntorno"].ToString();
                string srcEntorno = "data:image;base64," + vDocumentoEntorno;
                ImgPreviewEspacioFisico.Src = srcEntorno;
                //ImgPreviewEspacioFisico.Visible = true;


                TxObservacionesGenerales.Text = vDatos6.Rows[0]["observaciones"].ToString();
                if (TxObservacionesGenerales.Text.Equals(""))
                    TxObservacionesGenerales.Text = "Ninguna";

            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
        
        //private void validacionesAprobarLV()
        //{
            //if (RblAprobarLV.SelectedValue.Equals(""))
            //    throw new Exception("Falta completar opción ¿Desea aprobar LV?.");

            //if (RblAprobarLV.SelectedValue.Equals("0") && (TxMotivoCancelacionLV.Text == "" || TxMotivoCancelacionLV.Text == string.Empty))
            //    throw new Exception("Falta que ingrese el motivo de cancelacion de la lista de verificación");
        //}
        
        protected void BtnEnviarAprobacion_Click(object sender, EventArgs e)
        {
            try
            {
                //validacionesAprobarLV();
                Lugar.Text = TxLugar.Text;
                TxIdMantenimiento.Text = Session["AG_LvPC_ID_MANTENIMIENTO_LV_APROBAR_JEFE"].ToString();
                TxFechaModal.Text = TxFechaMant.Text;
                TxAreaModal.Text = TxArea.Text;
                TxResponsableModal.Text = TxNombreTecnicoResponsable.Text;
                //TxMotivoRegreso.Text = TxMotivoCancelacionLV.Text;

                if (Titulo.Text == "Regresar LV") {
                    DivMotivo.Visible = true;
                   
                } else {
                    DivMotivo.Visible = false;
                    
                }
             
                UpTituloAprobar.Update();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModalAprobacionLv();", true);

            }
            catch (Exception ex){
                    Mensaje(ex.Message, WarningType.Danger);
            }
        }
        
        //protected void RblAprobarLV_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (RblAprobarLV.SelectedValue.Equals("1"))
        //    {
        //        DivEtiqueta.Visible = false;
        //        DivTexto.Visible = false;
        //        Titulo.Text = "Aprobar LV";
        //        TxMotivoCancelacionLV.Text = String.Empty;
        //    }
        //    else
        //    {
        //        DivEtiqueta.Visible = true;
        //        DivTexto.Visible = true;
        //        Titulo.Text = "Regresar LV";
        //        TxMotivoCancelacionLV.Text = String.Empty;
        //    }
        //}
        
        //private void LimpiarAprobarLV()
        //{
        //    RblAprobarLV.SelectedIndex = -1;
        //    TxMotivoCancelacionLV.Text = String.Empty;
        //}

        void CorreoAlerta()
        {
            if (RblClimatizacionAdecuada.SelectedValue == "0" || RblUPS.SelectedValue == "0" || RbPolvoSuciedad.SelectedValue == "1" || RblHumedadSustancias.SelectedValue == "1" || RblRoboDaño.SelectedValue == "1" || RblElementosAjenos.SelectedValue == "1")
            {

                string vIDMantenimiento = Convert.ToString(Session["AG_LvPC_ID_MANTENIMIENTO_LV_APROBAR_JEFE"]);

                string vQueryD = "[STEISP_AGENCIA_AprobarNotificacion] 9,'" + vIDMantenimiento + "'";
                DataTable vDatosTecnicoResponsable = vConexion.obtenerDataTable(vQueryD);
                string vCorreoResponsable = vDatosTecnicoResponsable.Rows[0]["Nombre"].ToString();


                string Formato = "yyyy-MM-dd";
                //String vCorreoAlerta = "acedillo@bancatlan.hn";
                SmtpService vService = new SmtpService();
                String vCorreoAlerta = "aaguilarr@bancatlan.hn,drodriguez@bancatlan.hn,cfmelara@bancatlan.hn,eurrea@bancatlan.hn,jfigueroa@bancatlan.hn,megarcia@bancatlan.hn,gccoello@bancatlan.hn,dazuniga@bancatlan.hn,ojfunes@bancatlan.hn,emoyuela@bancatlan.hn,dzepeda@bancatlan.hn,acalderon@bancatlan.hn,diantunez@bancatlan.hn,rapena@bancatlan.hn," + vCorreoResponsable;
                string Climatizacion = "-No cuenta con climatización adecuada<br>";
                string UPS = "-No cuenta con protección de energía electrica<br>";
                string PolvoSuciedad = "-Se encontró rastros de polvo y suciedad en área de comunicaciones<br>";
                string HumedadSustancias = "-Se encontró rastros de humedad y sustancias no aptas en el área de comunicaciones<br>";
                string RoboDaño = "-Expuesto a robo o daños al área de comunicaciones<br>";
                string ElementosAjenos = "-Se encontraron elementos ajenos al área de comunicaciones<br>";

                if (RblClimatizacionAdecuada.SelectedValue == "1")
                    Climatizacion = "";
                if (RblUPS.SelectedValue == "1")
                    UPS = "";
                if (RbPolvoSuciedad.SelectedValue == "0")
                    PolvoSuciedad = "";
                if (RblHumedadSustancias.SelectedValue == "0")
                    HumedadSustancias = "";
                if (RblRoboDaño.SelectedValue == "0")
                    RoboDaño = "";
                if (RblElementosAjenos.SelectedValue == "0")
                    ElementosAjenos = "";

                string Alerta = Climatizacion + UPS + PolvoSuciedad + HumedadSustancias + RoboDaño + ElementosAjenos;

                vService.EnviarMensaje(
                              vCorreoAlerta,
                              typeBody.Alertas,
                               "<b>Buen día.<br> Se le notifica que Agencia (" + TxLugar.Text + ")<b> NO cuenta con una serie de protecciones que debe ser analizada, datos proporcionados por el técnico responsable: " + TxNombreTecnicoResponsable.Text + " al completar la lista de verificación del mantenimiento preventivo programado realizado el día: " + DateTime.Now.ToString(Formato) + "<br><b>Alertas detectadas en mantenimiento:<br>" + Alerta+ "<br> Favor tomar nota de la alerta para evitar inconvenientes futuros.",
                              "OBSERVACIONES A MANTENIMINETO DE AGENCIAS",
                              "Observaciones realizadas por el técnico responsable:<br>" + TxObservacionesGenerales.Text
                              );
            }
        }

        void ActualizarAgencia()
        {
            String vQuery1 = "STEISP_AGENCIA_CompletarListaVerificacion 12,'" + TxCodigoAgencia.Text + "','" + DDLTipoAgencia.SelectedValue + "','" + TxtTelefono.Text + "','" + txtDireccion.Text + "','" + TxLatitud.Text + "','" + TxLongitud.Text + "'";
            Int32 vInformacion1 = vConexion.ejecutarSql(vQuery1);
        }
        
        protected void btnModalAprobarLV_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (Titulo.Text == "Regresar LV")
                {
                    String vQuery1 = "STEISP_AGENCIA_AprobarLvJefesSuplentes 11," + Session["AG_LvPC_ID_MANTENIMIENTO_LV_APROBAR_JEFE"] +
                        "," + Session["USUARIO"] +
                        ",'" + TxMotivoRegreso.Text + "'";
                    Int32 vInformacion1 = vConexion.ejecutarSql(vQuery1);

                    if (vInformacion1 == 1){
                        Mensaje("Lista de verificación regresada con exito.", WarningType.Success);
                    }else {
                        Mensaje("Favor contactarse con el administrador, lista de verificación no se regreso exitosamente.", WarningType.Danger);
                    }
                }
                else
                {
                    String vQuery2 = "STEISP_AGENCIA_AprobarLvJefesSuplentes 10," + Session["AG_LvPC_ID_MANTENIMIENTO_LV_APROBAR_JEFE"] +
                         "," + Session["USUARIO"];
                    Int32 vInformacion2 = vConexion.ejecutarSql(vQuery2);

                    if (vInformacion2 == 1){
                        CorreoSuscripcion();
                        EnviarCorreoAprobar();
                        CorreoAlerta();
                        Mensaje("Lista de verificación aprobada con exito.", WarningType.Success);
                    } else{
                        Mensaje("Favor contactarse con el administrador, lista de verificación no se pudo aprobar con exito.", WarningType.Danger);
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
            //LimpiarAprobarLV();
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModalAprobarRegresarLv();", true);
            Response.Redirect("/sites/agencias/pages/mantenimiento/lvPendientesAprobarJefes.aspx");          
        }
        
        protected void BtnRegresarPendienteAprobar_Click(object sender, EventArgs e){
            //LimpiarAprobarLV();
            Response.Redirect("/sites/agencias/pages/mantenimiento/lvPendientesModificar.aspx");
        }
        
        //**********************************************************************************************************************//
        //*******************************************  ENVIO LISTA DE VERIFICACION  ********************************************//
        //*********************************************************************************************************************//
        
        private void cargarDataLlenado()
        {
            try
            {
                //DATOS GENERALES
                DataTable vDatos = new DataTable();
                vDatos = (DataTable)Session["AG_LvPC_DATOS_GENERALES"];
                TxFechaMant.Text = vDatos.Rows[0]["fecha"].ToString();
                TxSysAid.Text = vDatos.Rows[0]["sysAid"].ToString();
                TxLugar.Text = vDatos.Rows[0]["Lugar"].ToString();
                TxArea.Text = vDatos.Rows[0]["Area"].ToString();
                TxCodigoAgencia.Text = vDatos.Rows[0]["Cod_Agencia"].ToString();
                TxNombreTecnicoResponsable.Text = vDatos.Rows[0]["Responsable"].ToString();

                //TECNICO RESPONSABLE
                DataTable vDatos1 = new DataTable();
                vDatos1 = (DataTable)Session["AG_LvPC_TECNICO_RESPONSABLE"];
                TxIdentidadTecnicoResponsable.Text = vDatos1.Rows[0]["identidad"].ToString();

                //TECNICOS PARTICIPANTES
                DataTable vDatosTecnicosParticipantes = (DataTable)Session["AG_LvPC_TECNICOS_PARTICIPANTES"];
                GVTecnicosParticipantes.DataSource = vDatosTecnicosParticipantes;

                if (vDatosTecnicosParticipantes.Rows.Count > 0){
                    GVTecnicosParticipantes.DataBind();
                    UPTecnicosParticipantes.Update();
                    DivTecnicosParticipantes.Visible = true;
                    DivAlertaTecnicosParticipantes.Visible = false;
                } else{
                    DivTecnicosParticipantes.Visible = true;
                    DivAlertaTecnicosParticipantes.Visible = true;
                }

            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
        
        //void OcultarTarjeta()
        //{
        //    //DivAprobacion.Visible = false;
        //    //ocultarBotonVolver1.Visible = true;
        //    UpdatePanel5.Update();
        //}
        
        void mostrarAsteriscos()
        {
            lbHoraSalida.Visible = true;
            lbHoraLlegada.Visible = true;
            lbInicioMant.Visible = true;
            lbFinMant.Visible = true;
            //lbCantMaquinas.Visible = true;
            //lbCantImpresora.Visible = true;
            //lbCantEscaner.Visible = true;
            //lbCantDatacard.Visible = true;
            //lbRealizoMantEquipoComu.Visible = true;
            lbProbaronEquipo.Visible = true;

            //DivRack.Visible = true;
            //DivEspacio.Visible = true;
        }

        void CorreoSuscripcion()
        {


            int vIDMantenimiento = Convert.ToInt32(Session["AG_LvPC_ID_MANTENIMIENTO_LV_APROBAR_JEFE"]);

            string vQueryD = "[STEISP_AGENCIA_AprobarNotificacion] 9,'" + vIDMantenimiento + "'";
            DataTable vDatosTecnicoResponsable = vConexion.obtenerDataTable(vQueryD);
            string vQueryTecnicos = "[STEISP_AGENCIA_AprobarNotificacion] 10,'" + vIDMantenimiento + "'";
            DataTable vDatosTecnicos = vConexion.obtenerDataTable(vQueryTecnicos);
            string vQueryJefes = "[STEISP_AGENCIA_AprobarNotificacion] 11,'" + vIDMantenimiento + "'";
            DataTable vDatosJefeAgencias = vConexion.obtenerDataTable(vQueryJefes);
            string vQueryZona = "[STEISP_AGENCIA_AprobarNotificacion] 12,'" + vIDMantenimiento + "'";
            DataTable vDatosZona = vConexion.obtenerDataTable(vQueryZona);

            string vCorreosTecnicos = "";
            string vCorreosJefes = "";
            string vCorreosTodos = "";
            string vCorreoResponsable = "";
            for (int i = 0; i < vDatosTecnicoResponsable.Rows.Count; i++)
            {
                vCorreoResponsable = vDatosTecnicoResponsable.Rows[i]["Correo"].ToString() + ";";

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
                string vCorreo = vDatosJefeAgencias.Rows[i]["CorreoJefe"].ToString() + ";";
                vCorreosJefes = vCorreosJefes + vCorreo;
                if (vCorreosJefes == ";")
                    vCorreosJefes = "";
            }
            string vZonaAgencia = "";
            for (int i = 0; i < vDatosJefeAgencias.Rows.Count; i++)
            {
                vZonaAgencia = vDatosZona.Rows[i]["Zona"].ToString();
            }
            string vCorreoEncargadoZona = "";
            if (vZonaAgencia == "1")
                vCorreoEncargadoZona = "emontoya@bancatlan.hn";
            if (vZonaAgencia == "2")
                vCorreoEncargadoZona = "jdgarcia@bancatlan.hn";
            if (vZonaAgencia == "3")
                vCorreoEncargadoZona = "acalderon@bancatlan.hn";

            string vReporteViaticos = "Verificacion";
            string vCorreoAdmin = "acedillo@bancatlan.hn";
            //string vCorreoCopia = "acamador@bancatlan.hn"+";";
            //string vCorreoCopia = "eurrea@bancatlan.hn;unidadatmkiosco@bancatlan.hn;" + vCorreoEncargadoZona;
            string vCorreoCopia = "eurrea@bancatlan.hn;" + vCorreoEncargadoZona;
            //vCorreosTodos = vCorreosTecnicos + vCorreosJefes + vCorreoAdmin;
            vCorreosTodos = vCorreoResponsable + vCorreosTecnicos + vCorreosJefes;
            string vAsuntoRV = "Formato de verificación";
            string vBody = "Formato de verificación";

            string vQueryRep = "STEISP_AGENCIA_AprobarNotificacion 13,'" + vIDMantenimiento + "','" + vReporteViaticos + "','" + vCorreosTodos + "','" + vCorreoCopia + "','" + vAsuntoRV + "','" + vBody + "'";
            vConexion.ejecutarSql(vQueryRep);

        }

        protected void RBProbaronEquipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            TxMotivoNoProbaronEquipo.Text = "";
            UpNoProbaronEquipo.Update();

            if (RBProbaronEquipo.SelectedValue.Equals("1"))
            {

                //LbMotivoNoProbaronEquipo.Visible = false;
                TxMotivoNoProbaronEquipo.Visible = false;
                //lbAsteriscoProbaronEquipo.Visible = false;
                //UpNoProbaronEquipo.Update();
            
            }
            else
            {
              //LbMotivoNoProbaronEquipo.Visible = true;
                TxMotivoNoProbaronEquipo.Visible = true;
                //lbAsteriscoProbaronEquipo.Visible = true;
                UpNoProbaronEquipo.Update();
      
            }

        }
        
        
        protected void BtnEnviarLv_Click(object sender, EventArgs e)
        {          
            try
            {
                String vEx = Request.QueryString["ex"];
               
                validacionesEnvioLV();
                TxFechaModalEnviarLV.Text = TxFechaMant.Text;
                TxLugarModalEnviarLV.Text = TxCodigoAgencia.Text + " - " + TxLugar.Text;
                TxAreaModalEnviarLV.Text = TxArea.Text;
                TxResponsableModalEnviarLV.Text = TxNombreTecnicoResponsable.Text;


                if (vEx.Equals("3"))

                    
                {
                    TxIdMantenimientoModalEnviarLV.Text = Session["AG_LvPM_ID_MANTENIMIENTO_LV_MODIFICAR"].ToString();
                    TituloModalEnviarLV.Text = "Enviar " + TxLugar.Text;
                }
                else{
                    TxIdMantenimientoModalEnviarLV.Text = Session["AG_LvPC_ID_MANTENIMIENTO_LV_COMPLETAR"].ToString();
                    TituloModalEnviarLV.Text = "Enviar " + TxLugar.Text;
                }
                   


                UpdatePanel3.Update();
               
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModalEnvioLv();", true);
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
                //LbMensajeModalErrorReprogramar.Text = ex.Message;
                //UpdateModal.Visible = true;
                //UpdateModal.Update();
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
        
        private void validacionesEnvioLV()
        {
          

            if (TxHoraSalidaINFA.Text == "" || TxHoraSalidaINFA.Text == string.Empty)
                throw new Exception("Falta completar el campo hora Salida de Infatlan.");

            if (TxHoraLlegadaINFA.Text == "" || TxHoraLlegadaINFA.Text == string.Empty)
                throw new Exception("Falta completar el campo hora llegada a Infatlan.");

            if (TxHoraInicioMant.Text == "" || TxHoraInicioMant.Text == string.Empty)
                throw new Exception("Falta completarel campo hora inicio del mantenimiento.");

            if (TxHoraFinMant.Text == "" || TxHoraFinMant.Text == string.Empty)
                throw new Exception("Falta completar el campo hora fin del mantenimiento.");

            if (TxtTelefono.Text == "" || TxtTelefono.Text == string.Empty)
                throw new Exception("Falta completar teléfono.");

            if (DDLTipoAgencia.SelectedValue == "0")
                throw new Exception("Falta seleccionar tipo de agencia.");

            if (TxLatitud.Text == "" || TxLatitud.Text == string.Empty)
                throw new Exception("Falta completar latitud.");

            if (TxLongitud.Text == "" || TxLongitud.Text == string.Empty)
                throw new Exception("Falta completar longitud.");

            if (txtDireccion.Text == "" || txtDireccion.Text == string.Empty)
                throw new Exception("Falta completar dirección.");

            if (TxCantMaquinas.Text == "" || TxCantMaquinas.Text == string.Empty)
                throw new Exception("Falta completar el campo (Cant Maquinas).");

            if (TxCantImpresoraFinanciera.Text == "" || TxCantImpresoraFinanciera.Text == string.Empty)
                throw new Exception("Falta completar el campo (Cant Impresoras Financieras).");
            
            if (TxCantEscaner.Text == "" || TxCantEscaner.Text == string.Empty)
                throw new Exception("Falta completar el campo (Cant Escaner Fenix).");

            if (TxCantDatacard.Text == "" || TxCantDatacard.Text == string.Empty)
                throw new Exception("Falta completar el campo (Cant Datacards).");

           
            if (RBProbaronEquipo.SelectedValue.Equals(""))
                throw new Exception("Falta completar opción (¿Se probaron todos los equipos?).");

            if (RBProbaronEquipo.SelectedValue.Equals("0") && (TxMotivoNoProbaronEquipo.Text == string.Empty || TxMotivoNoProbaronEquipo.Text == ""))
                throw new Exception("Falta ingresar motivo por el cual no dio probo todo equipo.");

            if (RblClimatizacionAdecuada.SelectedValue.Equals(""))
                throw new Exception("Falta completar opción (¿El equipo de comunicación cuenta con una climatización adecuada(Aire Acondicionado)?).");

            if (RblUPS.SelectedValue.Equals(""))
                throw new Exception("Falta completar opción (¿El equipo de comunicación cuenta con protección de energía eléctrica (UPS)?).");

            if (RbPolvoSuciedad.SelectedValue.Equals(""))
                throw new Exception("Falta completar opción (¿El equipo de comunicaciones esta expuesto a polvo o suciedad?).");
            
            if (RblHumedadSustancias.SelectedValue.Equals(""))
                throw new Exception("Falta completar opción (¿Se observan rastros de humedad u otras substancias en las paredes o piso?).");

            if (RblRoboDaño.SelectedValue.Equals(""))
                throw new Exception("Falta completar opción (¿El equipo de comunicaciones esta expuesto a robos o daño?).");

            if (RblElementosAjenos.SelectedValue.Equals(""))
                throw new Exception("Falta completar opción (¿Se encuentran elementos ajenos a los equipos de comunicación en el cuarto de comunicaciones? (Ejemplo: sillas, papeles, basura, electrodomesticos, etc)?).");
          
           

            if (RblClimatizacionAdecuada.SelectedValue.Equals("1") && TxClimatizacion1.Value== string.Empty)
                throw new Exception("Falta que adjunte imagen del equipo de comunicación cuenta con una climatización adecuada.");
            
            if (RblUPS.SelectedValue.Equals("1") && TxUPS1.Value== string.Empty)
                throw new Exception("Falta que adjunte imagen del equipo de comunicación cuenta con protección de energía eléctrica (UPS).");

            if (TxRack1.Value== string.Empty)
                throw new Exception("Falta que adjunte imagen del Rack de comunicaciones.");

            if (RbPolvoSuciedad.SelectedValue.Equals("1") && TxPolvoSuciedad1.Value== string.Empty)
                throw new Exception("Falta que adjunte imagen que el equipo de comunicaciones esta expuesto a polvo o suciedad.");
         
            if (RblHumedadSustancias.SelectedValue.Equals("1") && TxHumedadSustancias1.Value== string.Empty)
                throw new Exception("Falta que adjunte imagen de los rastros de humedad u otras substancias en las paredes o piso que observo.");
            
            if (RblRoboDaño.SelectedValue.Equals("1") && TxRoboDaño1.Value== string.Empty)
                throw new Exception("Falta que adjunte imagen del equipo de comunicaciones esta expuesto a robos o daño.");
        
            if (RblElementosAjenos.SelectedValue.Equals("1") && TxElementosAjenos1.Value== string.Empty)
                throw new Exception("Falta que adjunte imagen de los elementos ajenos que encontro en el cuarto de comunicaciones? (Ejemplo: sillas, papeles, basura, electrodomesticos, etc).");
     
            if (TxEspacioFisico1.Value == string.Empty)
                throw new Exception("Falta que adjunte imagen del espacio fisico en donde se encuentra el equipo de comunicaciones (Entorno).");   
            if(TxResNoManEquipoComu1.Value==string.Empty)
                throw new Exception("Favor subir imagen de mantenimiento de equipo");
            if (TxClimatizacion1.Value == string.Empty)
                throw new Exception("Favor subir imagen de climatización");
            if (TxUPS1.Value == string.Empty)
                throw new Exception("Favor subir imagen de UPS");
            if (TxRack1.Value == string.Empty)
                throw new Exception("Favor subir imagen de rack");
            if (TxPolvoSuciedad1.Value == string.Empty)
                throw new Exception("Favor subir imagen de suciedad en el área");
            if (TxHumedadSustancias1.Value == string.Empty)
                throw new Exception("Favor subir imagen de humedad en el área");
            if (TxRoboDaño1.Value == string.Empty)
                throw new Exception("Favor subir imagen de robo/daño");
            if (TxElementosAjenos1.Value == string.Empty)
                throw new Exception("Favor subir imagen de elementos ajenos al área de trabajo");
            if (TxEspacioFisico1.Value == string.Empty)
                throw new Exception("Favor subir imagen de espacio físico");

        }

        void modificarImagenes()
        {
            string vIDMantenimiento = Session["AG_LvPM_ID_MANTENIMIENTO_LV_MODIFICAR"].ToString();

            //IMAGENES1
            String vNombreDepot1 = String.Empty;
            HttpPostedFile bufferDeposito1 = FuClimatizacion.PostedFile;
            byte[] vFileDeposito1 = null;
            string vExtClimatizacion = string.Empty;


            if (bufferDeposito1 != null)
            {
                vNombreDepot1 = FuClimatizacion.FileName;
                Stream vStream1 = bufferDeposito1.InputStream;
                BinaryReader vReader1 = new BinaryReader(vStream1);
                vFileDeposito1 = vReader1.ReadBytes((int)vStream1.Length);
                vExtClimatizacion = System.IO.Path.GetExtension(FuClimatizacion.FileName);
            }
            String vClimatizacion = String.Empty;
            if (vFileDeposito1 != null)
                vClimatizacion = Convert.ToBase64String(vFileDeposito1);
            //////////////////////////////////////////////////////////////////////////////
            //IMAGENES2
            String vNombreDepot2 = String.Empty;
            HttpPostedFile bufferDeposito2 = FuUPS.PostedFile;
            byte[] vFileDeposito2 = null;
            string vExtUPS = string.Empty;


            if (bufferDeposito2 != null)
            {
                vNombreDepot2 = FuUPS.FileName;
                Stream vStream2 = bufferDeposito2.InputStream;
                BinaryReader vReader2 = new BinaryReader(vStream2);
                vFileDeposito2 = vReader2.ReadBytes((int)vStream2.Length);
                vExtUPS = System.IO.Path.GetExtension(FuUPS.FileName);
            }
            String vUPS = String.Empty;
            if (vFileDeposito2 != null)
                vUPS = Convert.ToBase64String(vFileDeposito2);
            //////////////////////////////////////////////////////////////////////////////
            //IMAGENES3
            String vNombreDepot3 = String.Empty;
            HttpPostedFile bufferDeposito3 = FuPolvoSuciedad.PostedFile;
            byte[] vFileDeposito3 = null;
            string vExtPolvoSuciedad = string.Empty;


            if (bufferDeposito3 != null)
            {
                vNombreDepot3 = FuPolvoSuciedad.FileName;
                Stream vStream3 = bufferDeposito3.InputStream;
                BinaryReader vReader3 = new BinaryReader(vStream3);
                vFileDeposito3 = vReader3.ReadBytes((int)vStream3.Length);
                vExtPolvoSuciedad = System.IO.Path.GetExtension(FuPolvoSuciedad.FileName);
            }
            String vPolvoSuciedad = String.Empty;
            if (vFileDeposito3 != null)
                vPolvoSuciedad = Convert.ToBase64String(vFileDeposito3);
            //////////////////////////////////////////////////////////////////////////////
            //IMAGENES4
            String vNombreDepot4 = String.Empty;
            HttpPostedFile bufferDeposito4 = FuHumedadSustancias.PostedFile;
            byte[] vFileDeposito4 = null;
            string vExtHumedad = string.Empty;

            if (bufferDeposito4 != null)
            {
                vNombreDepot4 = FuHumedadSustancias.FileName;
                Stream vStream4 = bufferDeposito4.InputStream;
                BinaryReader vReader4 = new BinaryReader(vStream4);
                vFileDeposito4 = vReader4.ReadBytes((int)vStream4.Length);
                vExtHumedad = System.IO.Path.GetExtension(FuHumedadSustancias.FileName);
            }
            String vHumedadSustancias = String.Empty;
            if (vFileDeposito4 != null)
                vHumedadSustancias = Convert.ToBase64String(vFileDeposito4);
            //////////////////////////////////////////////////////////////////////////////
            //IMAGENES5
            String vNombreDepot5 = String.Empty;
            HttpPostedFile bufferDeposito5 = FuRoboDaño.PostedFile;
            byte[] vFileDeposito5 = null;
            string vExtDaños = string.Empty;

            if (bufferDeposito5 != null)
            {
                vNombreDepot5 = FuRoboDaño.FileName;
                Stream vStream5 = bufferDeposito5.InputStream;
                BinaryReader vReader5 = new BinaryReader(vStream5);
                vFileDeposito5 = vReader5.ReadBytes((int)vStream5.Length);
                vExtDaños = System.IO.Path.GetExtension(FuRoboDaño.FileName);
            }
            String vRoboDaño = String.Empty;
            if (vFileDeposito5 != null)
                vRoboDaño = Convert.ToBase64String(vFileDeposito5);
            //////////////////////////////////////////////////////////////////////////////
            //IMAGENES6
            String vNombreDepot6 = String.Empty;
            HttpPostedFile bufferDeposito6 = FuElementosAjenos.PostedFile;
            byte[] vFileDeposito6 = null;
            string vExtElementosAjenos = string.Empty;

            if (bufferDeposito6 != null)
            {
                vNombreDepot6 = FuElementosAjenos.FileName;
                Stream vStream6 = bufferDeposito6.InputStream;
                BinaryReader vReader6 = new BinaryReader(vStream6);
                vFileDeposito6 = vReader6.ReadBytes((int)vStream6.Length);
                vExtElementosAjenos = System.IO.Path.GetExtension(FuElementosAjenos.FileName);
            }
            String vElementosAjenos = String.Empty;
            if (vFileDeposito6 != null)
                vElementosAjenos = Convert.ToBase64String(vFileDeposito6);
            //////////////////////////////////////////////////////////////////////////////
            //IMAGENES7
            String vNombreDepot7 = String.Empty;
            HttpPostedFile bufferDeposito7 = FuImageNoMantEquipoComu.PostedFile;
            byte[] vFileDeposito7 = null;
            string vExtMantEquipo = string.Empty;

            if (bufferDeposito7 != null)
            {
                vNombreDepot7 = FuImageNoMantEquipoComu.FileName;
                Stream vStream7 = bufferDeposito7.InputStream;
                BinaryReader vReader7 = new BinaryReader(vStream7);
                vFileDeposito7 = vReader7.ReadBytes((int)vStream7.Length);
                vExtMantEquipo = System.IO.Path.GetExtension(FuImageNoMantEquipoComu.FileName);
            }
            String vManteEquipo = String.Empty;
            if (vFileDeposito7 != null)
                vManteEquipo = Convert.ToBase64String(vFileDeposito7);
            //////////////////////////////////////////////////////////////////////////////
            //IMAGENES8
            String vNombreDepot8 = String.Empty;
            HttpPostedFile bufferDeposito8 = FuRack.PostedFile;
            byte[] vFileDeposito8 = null;
            string vExtRack = string.Empty;

            if (bufferDeposito8 != null)
            {
                vNombreDepot8 = FuRack.FileName;
                Stream vStream8 = bufferDeposito8.InputStream;
                BinaryReader vReader8 = new BinaryReader(vStream8);
                vFileDeposito8 = vReader8.ReadBytes((int)vStream8.Length);
                vExtRack = System.IO.Path.GetExtension(FuRack.FileName);
            }
            String vRack = String.Empty;
            if (vFileDeposito8 != null)
                vRack = Convert.ToBase64String(vFileDeposito8);
            //////////////////////////////////////////////////////////////////////////////
            //IMAGENES9
            String vNombreDepot9 = String.Empty;
            HttpPostedFile bufferDeposito9 = FuEspacioFisico.PostedFile;
            byte[] vFileDeposito9 = null;
            string vExtEntorno = string.Empty;

            if (bufferDeposito9 != null)
            {
                vNombreDepot9 = FuEspacioFisico.FileName;
                Stream vStream9 = bufferDeposito9.InputStream;
                BinaryReader vReader9 = new BinaryReader(vStream9);
                vFileDeposito9 = vReader9.ReadBytes((int)vStream9.Length);
                vExtEntorno = System.IO.Path.GetExtension(FuEspacioFisico.FileName);
            }
            String vEspacio = String.Empty;
            if (vFileDeposito9 != null)
                vEspacio = Convert.ToBase64String(vFileDeposito9);

            if (vEspacio != "")
            {
                String vQueryEs = "STEISP_AGENCIA_ModificarListaVerificacion 12," + vIDMantenimiento +
                                            ",'" + vEspacio +
                                            "','" + vExtEntorno + "'";
                 vConexion.ejecutarSql(vQueryEs);
            }
            if (vManteEquipo != "")
            {
                String vQueryMant = "[STEISP_AGENCIA_ModificarListaVerificacion] 20, '" + vIDMantenimiento + "','" + vExtMantEquipo + "','" + vManteEquipo + "','" + TxMotivoNoMantEquipoComu.Text + "'";
                vConexion.ejecutarSql(vQueryMant);
            }
            if (vClimatizacion != "")
            {
                String vQueryCli = "[STEISP_AGENCIA_ModificarListaVerificacion] 14, '" + vIDMantenimiento + "','" + RblClimatizacionAdecuada.SelectedValue + "','" + vExtClimatizacion + "','" + vClimatizacion + "'";
                vConexion.ejecutarSql(vQueryCli);
            }
            if (vUPS != "")
            {
                String vQueryUPS = "[STEISP_AGENCIA_ModificarListaVerificacion] 15, '" + vIDMantenimiento + "','" + RblUPS.SelectedValue + "','" + vExtUPS + "','" + vUPS + "'";
                 vConexion.ejecutarSql(vQueryUPS);
            }
            if (vPolvoSuciedad != "")
            {
                String vQueryPolvo = "[STEISP_AGENCIA_ModificarListaVerificacion] 16, '" + vIDMantenimiento + "','" + RbPolvoSuciedad.SelectedValue + "','" + vExtPolvoSuciedad + "','" + vPolvoSuciedad + "'";
                vConexion.ejecutarSql(vQueryPolvo);
            }
            if (vHumedadSustancias != "")
            {
                String vQueryHumedad = "[STEISP_AGENCIA_ModificarListaVerificacion] 17, '" + vIDMantenimiento + "','" + RblHumedadSustancias.SelectedValue + "','" + vExtHumedad + "','" + vHumedadSustancias + "'";
                vConexion.ejecutarSql(vQueryHumedad);
            }
            if (vRoboDaño != "")
            {
                String vQueryRobo = "[STEISP_AGENCIA_ModificarListaVerificacion] 18, '" + vIDMantenimiento + "','" + RblRoboDaño.SelectedValue + "','" + vExtDaños + "','" + vRoboDaño + "'";
                vConexion.ejecutarSql(vQueryRobo);
            }
            if (vElementosAjenos != "")
            {
                String vQueryEle = "[STEISP_AGENCIA_ModificarListaVerificacion] 19, '" + vIDMantenimiento + "','" + RblElementosAjenos.SelectedValue + "','" + vExtElementosAjenos + "','" + vElementosAjenos + "'";
                vConexion.ejecutarSql(vQueryEle);
            }
            if (vRack != "")
            {
                String vQueryRack = "STEISP_AGENCIA_ModificarListaVerificacion 7," + vIDMantenimiento +
                                                         ",'" + vRack +
                                                         "','" + vExtRack + "'";
                vConexion.ejecutarSql(vQueryRack);
            }
        }

        void modificarImagenesReducido()
        {
            string vIDMantenimiento = Session["AG_LvPM_ID_MANTENIMIENTO_LV_MODIFICAR"].ToString();

            //IMAGENES1

            String vClimatizacion = String.Empty;
            String vExtClimatizacion = string.Empty;
            if (FuClimatizacion.FileName != "")
            {
                //Bitmap originalBMPReducido = null;
                //Bitmap originalBMP = new Bitmap(FuClimatizacion.FileContent);
                //byte[] imageData = null;
                //byte[] imageData2 = null;
                ////long imgTamano;
                //vExtClimatizacion = System.IO.Path.GetExtension(FuClimatizacion.FileName);               

                //using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
                //{
                //    originalBMP.Save(stream, ImageFormat.Png);
                //    stream.Position = 0;
                //    imageData = new byte[stream.Length+Convert.ToInt32(stream.Length*0.50)];
                //    stream.Read(imageData, 0, imageData.Length);
                //    stream.Close();
                //}
                //var newHeight = originalBMP.Height;
                //var newWidth = originalBMP.Width;
                //originalBMPReducido = new Bitmap(originalBMP.GetThumbnailImage(newWidth, newHeight, null, IntPtr.Zero));
                //using (System.IO.MemoryStream stream2 = new System.IO.MemoryStream())
                //{
                //    originalBMPReducido.Save(stream2, ImageFormat.Jpeg);
                //    stream2.Position = 0;
                //    imageData2 = new byte[stream2.Length];
                //    stream2.Read(imageData2, 0, imageData2.Length);
                //    stream2.Close();
                //}
                //vClimatizacion = Convert.ToBase64String(imageData2);
            }
            if (FuClimatizacion.FileName != "")
            {
                Bitmap originalBMPReducido = null;
                Bitmap originalBMP = new Bitmap(FuClimatizacion.FileContent);
                byte[] imageData = null;
                long imgTamano;
                vExtClimatizacion = System.IO.Path.GetExtension(FuClimatizacion.FileName);
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
                    originalBMPReducido = new Bitmap(FuClimatizacion.FileContent);
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
                vClimatizacion = Convert.ToBase64String(imageData);
            }

            //////////////////////////////////////////////////////////////////////////////
            //IMAGENES2
            String vUPS = String.Empty;
            string vExtUPS = string.Empty;
            if (FuUPS.FileName != "")
            {
                Bitmap originalBMPReducido = null;
                Bitmap originalBMP = new Bitmap(FuUPS.FileContent);
                byte[] imageData = null;
                long imgTamano;
                vExtUPS = System.IO.Path.GetExtension(FuUPS.FileName);
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
                    originalBMPReducido = new Bitmap(FuUPS.FileContent);
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
                vUPS = Convert.ToBase64String(imageData);
            }

            //////////////////////////////////////////////////////////////////////////////
            //IMAGENES3
            string vExtPolvoSuciedad = string.Empty;
            String vPolvoSuciedad = String.Empty;
            if (FuPolvoSuciedad.FileName != "")
            {
                Bitmap originalBMPReducido = null;
                Bitmap originalBMP = new Bitmap(FuPolvoSuciedad.FileContent);
                byte[] imageData = null;
                long imgTamano;
                vExtPolvoSuciedad = System.IO.Path.GetExtension(FuPolvoSuciedad.FileName);
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
                    originalBMPReducido = new Bitmap(FuPolvoSuciedad.FileContent);
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
                vPolvoSuciedad = Convert.ToBase64String(imageData);
            }


            //////////////////////////////////////////////////////////////////////////////
            //IMAGENES4
            String vHumedadSustancias = String.Empty;
            string vExtHumedad = string.Empty;
            if (FuHumedadSustancias.FileName != "")
            {
                Bitmap originalBMPReducido = null;
                Bitmap originalBMP = new Bitmap(FuHumedadSustancias.FileContent);
                byte[] imageData = null;
                long imgTamano;
                vExtHumedad = System.IO.Path.GetExtension(FuHumedadSustancias.FileName);
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
                    originalBMPReducido = new Bitmap(FuHumedadSustancias.FileContent);
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
                vHumedadSustancias = Convert.ToBase64String(imageData);
            }

           
            //////////////////////////////////////////////////////////////////////////////
            //IMAGENES5
            String vRoboDaño = String.Empty;
            string vExtDaños = string.Empty;
            if (FuRoboDaño.FileName != "")
            {
                Bitmap originalBMPReducido = null;
                Bitmap originalBMP = new Bitmap(FuRoboDaño.FileContent);
                byte[] imageData = null;
                long imgTamano;
                vExtDaños = System.IO.Path.GetExtension(FuRoboDaño.FileName);
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
                    originalBMPReducido = new Bitmap(FuRoboDaño.FileContent);
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
                vRoboDaño = Convert.ToBase64String(imageData);
            }

            //////////////////////////////////////////////////////////////////////////////
            //IMAGENES6
            String vElementosAjenos = String.Empty;
            string vExtElementosAjenos = string.Empty;
            if (FuElementosAjenos.FileName != "")
            {
                Bitmap originalBMPReducido = null;
                Bitmap originalBMP = new Bitmap(FuElementosAjenos.FileContent);
                byte[] imageData = null;
                long imgTamano;
                vExtElementosAjenos = System.IO.Path.GetExtension(FuElementosAjenos.FileName);
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
                    originalBMPReducido = new Bitmap(FuElementosAjenos.FileContent);
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
                vElementosAjenos = Convert.ToBase64String(imageData);
            }


            //////////////////////////////////////////////////////////////////////////////
            //IMAGENES7
            String vManteEquipo = String.Empty;
            string vExtMantEquipo = string.Empty;
            if (FuImageNoMantEquipoComu.FileName != "")
            {
                Bitmap originalBMPReducido = null;
                Bitmap originalBMP = new Bitmap(FuImageNoMantEquipoComu.FileContent);
                byte[] imageData = null;
                long imgTamano;
                vExtMantEquipo = System.IO.Path.GetExtension(FuImageNoMantEquipoComu.FileName);
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
                    originalBMPReducido = new Bitmap(FuImageNoMantEquipoComu.FileContent);
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
                vManteEquipo = Convert.ToBase64String(imageData);
            }


            //////////////////////////////////////////////////////////////////////////////
            //IMAGENES8
            String vRack = String.Empty;
            string vExtRack = string.Empty;
            if (FuRack.FileName != "")
            {
                Bitmap originalBMPReducido = null;
                Bitmap originalBMP = new Bitmap(FuRack.FileContent);
                byte[] imageData = null;
                long imgTamano;
                vExtRack = System.IO.Path.GetExtension(FuRack.FileName);
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
                    originalBMPReducido = new Bitmap(FuRack.FileContent);
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
                vRack = Convert.ToBase64String(imageData);
            }


            //////////////////////////////////////////////////////////////////////////////
            //IMAGENES9
            String vEspacio = String.Empty;
            string vExtEntorno = string.Empty;
            if (FuEspacioFisico.FileName != "")
            {
                Bitmap originalBMPReducido = null;
                Bitmap originalBMP = new Bitmap(FuEspacioFisico.FileContent);
                byte[] imageData = null;
                long imgTamano;
                vExtEntorno = System.IO.Path.GetExtension(FuEspacioFisico.FileName);
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
                    originalBMPReducido = new Bitmap(FuEspacioFisico.FileContent);
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
                vEspacio = Convert.ToBase64String(imageData);
            }


            if (vEspacio != "")
            {
                String vQueryEs = "STEISP_AGENCIA_ModificarListaVerificacion 12," + vIDMantenimiento +
                                            ",'" + vEspacio +
                                            "','" + vExtEntorno + "'";
                vConexion.ejecutarSql(vQueryEs);
            }
            if (vManteEquipo != "")
            {
                String vQueryMant = "[STEISP_AGENCIA_ModificarListaVerificacion] 20, '" + vIDMantenimiento + "','" + vExtMantEquipo + "','" + vManteEquipo + "','" + TxMotivoNoMantEquipoComu.Text + "'";
                vConexion.ejecutarSql(vQueryMant);
            }
            if (vClimatizacion != "")
            {
                String vQueryCli = "[STEISP_AGENCIA_ModificarListaVerificacion] 14, '" + vIDMantenimiento + "','" + RblClimatizacionAdecuada.SelectedValue + "','" + vExtClimatizacion + "','" + vClimatizacion + "'";
                vConexion.ejecutarSql(vQueryCli);
            }
            if (vUPS != "")
            {
                String vQueryUPS = "[STEISP_AGENCIA_ModificarListaVerificacion] 15, '" + vIDMantenimiento + "','" + RblUPS.SelectedValue + "','" + vExtUPS + "','" + vUPS + "'";
                vConexion.ejecutarSql(vQueryUPS);
            }
            if (vPolvoSuciedad != "")
            {
                String vQueryPolvo = "[STEISP_AGENCIA_ModificarListaVerificacion] 16, '" + vIDMantenimiento + "','" + RbPolvoSuciedad.SelectedValue + "','" + vExtPolvoSuciedad + "','" + vPolvoSuciedad + "'";
                vConexion.ejecutarSql(vQueryPolvo);
            }
            if (vHumedadSustancias != "")
            {
                String vQueryHumedad = "[STEISP_AGENCIA_ModificarListaVerificacion] 17, '" + vIDMantenimiento + "','" + RblHumedadSustancias.SelectedValue + "','" + vExtHumedad + "','" + vHumedadSustancias + "'";
                vConexion.ejecutarSql(vQueryHumedad);
            }
            if (vRoboDaño != "")
            {
                String vQueryRobo = "[STEISP_AGENCIA_ModificarListaVerificacion] 18, '" + vIDMantenimiento + "','" + RblRoboDaño.SelectedValue + "','" + vExtDaños + "','" + vRoboDaño + "'";
                vConexion.ejecutarSql(vQueryRobo);
            }
            if (vElementosAjenos != "")
            {
                String vQueryEle = "[STEISP_AGENCIA_ModificarListaVerificacion] 19, '" + vIDMantenimiento + "','" + RblElementosAjenos.SelectedValue + "','" + vExtElementosAjenos + "','" + vElementosAjenos + "'";
                vConexion.ejecutarSql(vQueryEle);
            }
            if (vRack != "")
            {
                String vQueryRack = "STEISP_AGENCIA_ModificarListaVerificacion 7," + vIDMantenimiento +
                                                         ",'" + vRack +
                                                         "','" + vExtRack + "'";
                vConexion.ejecutarSql(vQueryRack);
            }
        }

        void crearImagenesReducido()
        {
            string vIDMantenimiento = Session["AG_LvPC_ID_MANTENIMIENTO_LV_COMPLETAR"].ToString();

            //IMAGENES1

            String vClimatizacion = String.Empty;
            String vExtClimatizacion = string.Empty;
            if (FuClimatizacion.FileName != "")
            {
                Bitmap originalBMPReducido = null;
                Bitmap originalBMP = new Bitmap(FuClimatizacion.FileContent);
                byte[] imageData = null;
                long imgTamano;
                vExtClimatizacion = System.IO.Path.GetExtension(FuClimatizacion.FileName);
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
                    originalBMPReducido = new Bitmap(FuClimatizacion.FileContent);
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
                vClimatizacion = Convert.ToBase64String(imageData);
            }

            //////////////////////////////////////////////////////////////////////////////
            //IMAGENES2
            String vUPS = String.Empty;
            string vExtUPS = string.Empty;
            if (FuUPS.FileName != "")
            {
                Bitmap originalBMPReducido = null;
                Bitmap originalBMP = new Bitmap(FuUPS.FileContent);
                byte[] imageData = null;
                long imgTamano;
                vExtUPS = System.IO.Path.GetExtension(FuUPS.FileName);
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
                    originalBMPReducido = new Bitmap(FuUPS.FileContent);
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
                vUPS = Convert.ToBase64String(imageData);
            }

            //////////////////////////////////////////////////////////////////////////////
            //IMAGENES3
            string vExtPolvoSuciedad = string.Empty;
            String vPolvoSuciedad = String.Empty;
            if (FuPolvoSuciedad.FileName != "")
            {
                Bitmap originalBMPReducido = null;
                Bitmap originalBMP = new Bitmap(FuPolvoSuciedad.FileContent);
                byte[] imageData = null;
                long imgTamano;
                vExtPolvoSuciedad = System.IO.Path.GetExtension(FuPolvoSuciedad.FileName);
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
                    originalBMPReducido = new Bitmap(FuPolvoSuciedad.FileContent);
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
                vPolvoSuciedad = Convert.ToBase64String(imageData);
            }


            //////////////////////////////////////////////////////////////////////////////
            //IMAGENES4
            String vHumedadSustancias = String.Empty;
            string vExtHumedad = string.Empty;
            if (FuHumedadSustancias.FileName != "")
            {
                Bitmap originalBMPReducido = null;
                Bitmap originalBMP = new Bitmap(FuHumedadSustancias.FileContent);
                byte[] imageData = null;
                long imgTamano;
                vExtHumedad = System.IO.Path.GetExtension(FuHumedadSustancias.FileName);
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
                    originalBMPReducido = new Bitmap(FuHumedadSustancias.FileContent);
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
                vHumedadSustancias = Convert.ToBase64String(imageData);
            }


            //////////////////////////////////////////////////////////////////////////////
            //IMAGENES5
            String vRoboDaño = String.Empty;
            string vExtDaños = string.Empty;
            if (FuRoboDaño.FileName != "")
            {
                Bitmap originalBMPReducido = null;
                Bitmap originalBMP = new Bitmap(FuRoboDaño.FileContent);
                byte[] imageData = null;
                long imgTamano;
                vExtDaños = System.IO.Path.GetExtension(FuRoboDaño.FileName);
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
                    originalBMPReducido = new Bitmap(FuRoboDaño.FileContent);
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
                vRoboDaño = Convert.ToBase64String(imageData);
            }

            //////////////////////////////////////////////////////////////////////////////
            //IMAGENES6
            String vElementosAjenos = String.Empty;
            string vExtElementosAjenos = string.Empty;
            if (FuElementosAjenos.FileName != "")
            {
                Bitmap originalBMPReducido = null;
                Bitmap originalBMP = new Bitmap(FuElementosAjenos.FileContent);
                byte[] imageData = null;
                long imgTamano;
                vExtElementosAjenos = System.IO.Path.GetExtension(FuElementosAjenos.FileName);
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
                    originalBMPReducido = new Bitmap(FuElementosAjenos.FileContent);
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
                vElementosAjenos = Convert.ToBase64String(imageData);
            }


            //////////////////////////////////////////////////////////////////////////////
            //IMAGENES7
            String vManteEquipo = String.Empty;
            string vExtMantEquipo = string.Empty;
            if (FuImageNoMantEquipoComu.FileName != "")
            {
                Bitmap originalBMPReducido = null;
                Bitmap originalBMP = new Bitmap(FuImageNoMantEquipoComu.FileContent);
                byte[] imageData = null;
                long imgTamano;
                vExtMantEquipo = System.IO.Path.GetExtension(FuImageNoMantEquipoComu.FileName);
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
                    originalBMPReducido = new Bitmap(FuImageNoMantEquipoComu.FileContent);
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
                vManteEquipo = Convert.ToBase64String(imageData);
            }


            //////////////////////////////////////////////////////////////////////////////
            //IMAGENES8
            String vRack = String.Empty;
            string vExtRack = string.Empty;
            if (FuRack.FileName != "")
            {
                Bitmap originalBMPReducido = null;
                Bitmap originalBMP = new Bitmap(FuRack.FileContent);
                byte[] imageData = null;
                long imgTamano;
                vExtRack = System.IO.Path.GetExtension(FuRack.FileName);
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
                    originalBMPReducido = new Bitmap(FuRack.FileContent);
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
                vRack = Convert.ToBase64String(imageData);
            }


            //////////////////////////////////////////////////////////////////////////////
            //IMAGENES9
            String vEspacio = String.Empty;
            string vExtEntorno = string.Empty;
            if (FuEspacioFisico.FileName != "")
            {
                Bitmap originalBMPReducido = null;
                Bitmap originalBMP = new Bitmap(FuEspacioFisico.FileContent);
                byte[] imageData = null;
                long imgTamano;
                vExtEntorno = System.IO.Path.GetExtension(FuEspacioFisico.FileName);
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
                    originalBMPReducido = new Bitmap(FuEspacioFisico.FileContent);
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
                vEspacio = Convert.ToBase64String(imageData);
            }

            int valor = 0;
            String vQuery2 = "STEISP_AGENCIA_CompletarListaVerificacion 6," + Session["AG_LvPC_ID_MANTENIMIENTO_LV_COMPLETAR"] +
                                                  "," + TxCantMaquinas.Text +
                                                  "," + TxCantImpresoraFinanciera.Text +
                                                  "," + TxCantDatacard.Text +
                                                  "," + TxCantEscaner.Text +
                                                  "," + valor +
                                                  ",'" + TxMotivoNoMantEquipoComu.Text +
                                                  "','" + vManteEquipo +
                                                  "','" + vExtMantEquipo + "'";
            Int32 vInformacion2 = vConexion.ejecutarSql(vQuery2);

            String vQuery4 = "STEISP_AGENCIA_CompletarListaVerificacion 8," + Session["AG_LvPC_ID_MANTENIMIENTO_LV_COMPLETAR"] +
                                                "," + RblClimatizacionAdecuada.SelectedValue +
                                                ",'" + vClimatizacion +
                                                "','" + vExtClimatizacion + "'" +

                                                "," + RblUPS.SelectedValue +
                                                ",'" + vUPS +
                                                "','" + vExtUPS + "'" +

                                                ",'" + vRack +
                                                "','" + vExtRack + "'";

            Int32 vInformacion4 = vConexion.ejecutarSql(vQuery4);

            String vQuery5 = "STEISP_AGENCIA_CompletarListaVerificacion 9," + Session["AG_LvPC_ID_MANTENIMIENTO_LV_COMPLETAR"] +
                                                "," + RbPolvoSuciedad.SelectedValue +
                                                ",'" + vPolvoSuciedad +
                                                "','" + vExtPolvoSuciedad + "'" +

                                                "," + RblHumedadSustancias.SelectedValue +
                                                ",'" + vHumedadSustancias +
                                                "','" + vExtHumedad + "'" +

                                                "," + RblRoboDaño.SelectedValue +
                                                ",'" + vRoboDaño +
                                                "','" + vExtDaños + "'" +

                                                "," + RblElementosAjenos.SelectedValue +
                                                ",'" + vElementosAjenos +
                                                "','" + vExtElementosAjenos + "'" +

                                                ",'" + vEspacio +
                                                "','" + vExtEntorno + "','" +
                                                    TxObservacionesGenerales.Text + "'";

            Int32 vInformacion5 = vConexion.ejecutarSql(vQuery5);
        }

        void EnviarCorreoCrear()
        {
            SmtpService vService = new SmtpService();
            string vZonaAgencia = "";
            string vIDMantenimiento = Convert.ToString(Session["AG_LvPC_ID_MANTENIMIENTO_LV_COMPLETAR"]);

            string vQueryD = "[STEISP_AGENCIA_AprobarNotificacion] 9,'" + vIDMantenimiento + "'";
            DataTable vDatosTecnicoResponsable = vConexion.obtenerDataTable(vQueryD);
            string vQueryTecnicos = "[STEISP_AGENCIA_AprobarNotificacion] 10,'" + vIDMantenimiento + "'";
            DataTable vDatosTecnicos = vConexion.obtenerDataTable(vQueryTecnicos);
            string vQueryJefes = "[STEISP_AGENCIA_AprobarNotificacion] 11,'" + vIDMantenimiento + "'";
            DataTable vDatosJefeAgencias = vConexion.obtenerDataTable(vQueryJefes);
            string vQueryZona = "[STEISP_AGENCIA_AprobarNotificacion] 12,'" + vIDMantenimiento + "'";
            DataTable vDatosZona = vConexion.obtenerDataTable(vQueryZona);
            DataTable vDatos = (DataTable)Session["AUTHCLASS"];


            for (int i = 0; i < vDatosZona.Rows.Count; i++)
            {
                vZonaAgencia = vDatosZona.Rows[i]["Zona"].ToString();
            }
            string vCorreoEncargadoZona = "";
            if (vZonaAgencia == "1")
                vCorreoEncargadoZona = "emontoya@bancatlan.hn";
            if (vZonaAgencia == "2")
                vCorreoEncargadoZona = "jdgarcia@bancatlan.hn";
            if (vZonaAgencia == "3")
                vCorreoEncargadoZona = "acalderon@bancatlan.hn";

            if (vDatos.Rows.Count > 0)
            {
                foreach (DataRow item in vDatos.Rows)
                {
                    
                        //ENVIAR A JEFE
                        if (!item["correo"].ToString().Trim().Equals(""))
                        {
                            vService.EnviarMensaje(item["correo"].ToString(),
                            typeBody.EnvioCorreo,
                            "Verificación de Mantenimiento Agencia",
                            "Buen día, se le notifica que se creó verificación de mantenimiento, el encargado es " + vDatosTecnicoResponsable.Rows[0]["Nombre"].ToString() + ", mantenimiento a Agencia " + TxLugarModalEnviarLV.Text,
                            "El usuario <b>" + item["Nombre"].ToString() + "</b> creó: <br> Verificación de Mantenimiento",
                            "",
                            "/sites/agencias/pages/mantenimiento/lvPendientesAprobarJefes.aspx"
                            );

                            //vFlagEnvioSupervisor = true;
                        }
                        //ENVIAR A EDWIN
                        //string vNombre = "EDWIN ALBERTO URREA PENA";
                        vService.EnviarMensaje(ConfigurationManager.AppSettings["STEIMail"],
                                typeBody.EnvioCorreo,
                                "Verificación de Mantenimiento Agencia",
                                "Buen día, se le notifica que se creó verificación de mantenimiento, el encargado es " + vDatosTecnicoResponsable.Rows[0]["Nombre"].ToString() + ", mantenimiento a Agencia " + TxLugarModalEnviarLV.Text,
                                  "El usuario <b>" + item["Nombre"].ToString() + "</b> creó: <br> Verificación de Mantenimiento",
                                   vCorreoEncargadoZona,
                                   "/sites/agencias/pages/mantenimiento/lvPendientesAprobarJefes.aspx"
                                );

                    

                }
            }
            if (vDatosTecnicoResponsable.Rows.Count > 0)
            {
                //foreach (DataRow item in vDatosTecnicoResponsable.Rows)
                //{
                //    //ENVIAR A RESPONSABLE
                //    vService.EnviarMensaje(item["Correo"].ToString(),
                //        typeBody.EnvioCorreo,
                //       "Verificación de Mantenimiento Agencia",
                //        "Buen día, se le notifica que se creó verificación de mantenimiento, el encargado es " + item["Nombre"].ToString() + ", mantenimiento a agencia " + TxLugarModalEnviarLV.Text,
                //          "El usuario <b>" + vDatos.Rows[0]["Nombre"].ToString() + "</b> creó: <br> Verificación de Mantenimiento de Agencia al que ha sido asignado como responsable.",
                //            "",
                //        "/login.spx"
                //        );
                //}
            }
            if (vDatosTecnicos.Rows.Count > 0)
            {
                foreach (DataRow itemT in vDatosTecnicos.Rows)
                {
                    vService.EnviarMensaje(itemT["correo"].ToString(),
                        typeBody.EnvioCorreo,
                        "Verificación de Mantenimiento Agencia",
                        "Buen día, se le notifica que se creó verificación de mantenimiento, el encargado es " + vDatosTecnicoResponsable.Rows[0]["Nombre"].ToString() + ", mantenimiento a Agencia " + TxLugarModalEnviarLV.Text,
                          "El usuario <b>" + vDatos.Rows[0]["Nombre"].ToString() + "</b> creó: <br> Verificación de Mantenimiento de Agencia al que ha sido asignado como parte del equipo de trabajo",
                            "",
                        "/login.aspx"
                        );
                }
            }
            if (vDatosJefeAgencias.Rows.Count > 0)
            {
                //foreach (DataRow item in vDatosJefeAgencias.Rows)
                //{
                //    //ENVIAR A JEFES DE AGENCIA
                //    if (!item["CorreoJefe"].ToString().Trim().Equals(""))
                //    {
                //        vService.EnviarMensaje(item["CorreoJefe"].ToString(),
                //            typeBody.EnvioCorreo,
                //            "Verificación de Mantenimiento Agencia",
                //                "Buen día, se le notifica que se creó verificación de mantenimiento, el encargado es " + vDatosTecnicoResponsable.Rows[0]["Nombre"].ToString() + ", mantenimiento a Agencia " + TxLugarModalEnviarLV.Text,
                //                  "Se le informa que dicho mantenimiento se hará en la agencia al que usted se encuentra asignado.",
                //                   "",
                //                   ""
                //            );
                //    }
                //}
            }

        }

        void EnviarCorreoModificar()
        {
            SmtpService vService = new SmtpService();
            string vZonaAgencia = "";
            string vIDMantenimiento = Convert.ToString(Session["AG_LvPM_ID_MANTENIMIENTO_LV_MODIFICAR"]);

            string vQueryD = "[STEISP_AGENCIA_AprobarNotificacion] 9,'" + vIDMantenimiento + "'";
            DataTable vDatosTecnicoResponsable = vConexion.obtenerDataTable(vQueryD);
            string vQueryTecnicos = "[STEISP_AGENCIA_AprobarNotificacion] 10,'" + vIDMantenimiento + "'";
            DataTable vDatosTecnicos = vConexion.obtenerDataTable(vQueryTecnicos);
            string vQueryJefes = "[STEISP_AGENCIA_AprobarNotificacion] 11,'" + vIDMantenimiento + "'";
            DataTable vDatosJefeAgencias = vConexion.obtenerDataTable(vQueryJefes);
            string vQueryZona = "[STEISP_AGENCIA_AprobarNotificacion] 12,'" + vIDMantenimiento + "'";
            DataTable vDatosZona = vConexion.obtenerDataTable(vQueryZona);
            DataTable vDatos = (DataTable)Session["AUTHCLASS"];


            for (int i = 0; i < vDatosZona.Rows.Count; i++)
            {
                vZonaAgencia = vDatosZona.Rows[i]["Zona"].ToString();
            }
            string vCorreoEncargadoZona = "";
            if (vZonaAgencia == "1")
                vCorreoEncargadoZona = "emontoya@bancatlan.hn";
            if (vZonaAgencia == "2")
                vCorreoEncargadoZona = "jdgarcia@bancatlan.hn";
            if (vZonaAgencia == "3")
                vCorreoEncargadoZona = "acalderon@bancatlan.hn";

            if (vDatos.Rows.Count > 0)
            {
                foreach (DataRow item in vDatos.Rows)
                {

                    //ENVIAR A JEFE
                    if (!item["correo"].ToString().Trim().Equals(""))
                    {
                        vService.EnviarMensaje(item["correo"].ToString(),
                        typeBody.EnvioCorreo,
                        "Verificación de Mantenimiento Agencia",
                        "Buen día, se le notifica que se creó verificación de mantenimiento, el encargado es " + vDatosTecnicoResponsable.Rows[0]["Nombre"].ToString() + ", mantenimiento a Agencia " + TxLugarModalEnviarLV.Text,
                        "El usuario <b>" + item["Nombre"].ToString() + "</b> creó: <br> Verificación de Mantenimiento",
                        "",
                        "/sites/agencias/pages/mantenimiento/lvPendientesAprobarJefes.aspx"
                        );

                        //vFlagEnvioSupervisor = true;
                    }
                    //ENVIAR A EDWIN
                    //string vNombre = "EDWIN ALBERTO URREA PENA";
                    vService.EnviarMensaje(ConfigurationManager.AppSettings["STEIMail"],
                            typeBody.EnvioCorreo,
                            "Verificación de Mantenimiento Agencia",
                            "Buen día, se le notifica que se creó verificación de mantenimiento, el encargado es " + vDatosTecnicoResponsable.Rows[0]["Nombre"].ToString() + ", mantenimiento a Agencia " + TxLugarModalEnviarLV.Text,
                              "El usuario <b>" + item["Nombre"].ToString() + "</b> creó: <br> Verificación de Mantenimiento",
                               vCorreoEncargadoZona,
                               "/sites/agencias/pages/mantenimiento/lvPendientesAprobarJefes.aspx"
                            );



                }
            }
            if (vDatosTecnicoResponsable.Rows.Count > 0)
            {
                //foreach (DataRow item in vDatosTecnicoResponsable.Rows)
                //{
                //    //ENVIAR A RESPONSABLE
                //    vService.EnviarMensaje(item["Correo"].ToString(),
                //        typeBody.EnvioCorreo,
                //       "Verificación de Mantenimiento Agencia",
                //        "Buen día, se le notifica que se creó verificación de mantenimiento, el encargado es " + item["Nombre"].ToString() + ", mantenimiento a agencia " + TxLugarModalEnviarLV.Text,
                //          "El usuario <b>" + vDatos.Rows[0]["Nombre"].ToString() + "</b> creó: <br> Verificación de Mantenimiento de Agencia al que ha sido asignado como responsable.",
                //            "",
                //        "/login.spx"
                //        );
                //}
            }
            if (vDatosTecnicos.Rows.Count > 0)
            {
                foreach (DataRow itemT in vDatosTecnicos.Rows)
                {
                    vService.EnviarMensaje(itemT["correo"].ToString(),
                        typeBody.EnvioCorreo,
                        "Verificación de Mantenimiento Agencia",
                        "Buen día, se le notifica que se creó verificación de mantenimiento, el encargado es " + vDatosTecnicoResponsable.Rows[0]["Nombre"].ToString() + ", mantenimiento a Agencia " + TxLugarModalEnviarLV.Text,
                          "El usuario <b>" + vDatos.Rows[0]["Nombre"].ToString() + "</b> creó: <br> Verificación de Mantenimiento de Agencia al que ha sido asignado como parte del equipo de trabajo",
                            "",
                        "/login.aspx"
                        );
                }
            }
            if (vDatosJefeAgencias.Rows.Count > 0)
            {
                //foreach (DataRow item in vDatosJefeAgencias.Rows)
                //{
                //    //ENVIAR A JEFES DE AGENCIA
                //    if (!item["CorreoJefe"].ToString().Trim().Equals(""))
                //    {
                //        vService.EnviarMensaje(item["CorreoJefe"].ToString(),
                //            typeBody.EnvioCorreo,
                //            "Verificación de Mantenimiento Agencia",
                //                "Buen día, se le notifica que se creó verificación de mantenimiento, el encargado es " + vDatosTecnicoResponsable.Rows[0]["Nombre"].ToString() + ", mantenimiento a Agencia " + TxLugarModalEnviarLV.Text,
                //                  "Se le informa que dicho mantenimiento se hará en la agencia al que usted se encuentra asignado.",
                //                   "",
                //                   ""
                //            );
                //    }
                //}
            }

        }

        void EnviarCorreoDevolver()
        {
            SmtpService vService = new SmtpService();
            string vZonaAgencia = "";
            string vIDMantenimiento = Convert.ToString(Session["AG_LvPC_ID_MANTENIMIENTO_LV_APROBAR_JEFE"]);

            string vQueryD = "[STEISP_AGENCIA_AprobarNotificacion] 9,'" + vIDMantenimiento + "'";
            DataTable vDatosTecnicoResponsable = vConexion.obtenerDataTable(vQueryD);
            string vQueryTecnicos = "[STEISP_AGENCIA_AprobarNotificacion] 10,'" + vIDMantenimiento + "'";
            DataTable vDatosTecnicos = vConexion.obtenerDataTable(vQueryTecnicos);
            string vQueryJefes = "[STEISP_AGENCIA_AprobarNotificacion] 11,'" + vIDMantenimiento + "'";
            DataTable vDatosJefeAgencias = vConexion.obtenerDataTable(vQueryJefes);
            string vQueryZona = "[STEISP_AGENCIA_AprobarNotificacion] 12,'" + vIDMantenimiento + "'";
            DataTable vDatosZona = vConexion.obtenerDataTable(vQueryZona);
            DataTable vDatos = (DataTable)Session["AUTHCLASS"];


            for (int i = 0; i < vDatosZona.Rows.Count; i++)
            {
                vZonaAgencia = vDatosZona.Rows[i]["Zona"].ToString();
            }
            string vCorreoEncargadoZona = "";
            if (vZonaAgencia == "1")
                vCorreoEncargadoZona = "emontoya@bancatlan.hn";
            if (vZonaAgencia == "2")
                vCorreoEncargadoZona = "jdgarcia@bancatlan.hn";
            if (vZonaAgencia == "3")
                vCorreoEncargadoZona = "acalderon@bancatlan.hn";

            if (vDatos.Rows.Count > 0)
            {
                foreach (DataRow item in vDatos.Rows)
                {
                    //if (Session["USUARIO"].ToString() == "eurrea" || Session["USUARIO"].ToString() == "emontoya" || Session["USUARIO"].ToString() == "jdgarcia" || Session["USUARIO"].ToString() == "acalderon")
                    //{
                        vService.EnviarMensaje(ConfigurationManager.AppSettings["STEIMail"],
                           typeBody.EnvioCorreo,
                           "Verificación de Mantenimiento Agencia",
                           "Buen día, se le notifica que se devolvió verificación de mantenimiento, el encargado es " + vDatosTecnicoResponsable.Rows[0]["Nombre"].ToString() + ", mantenimiento a Agencia " + TxLugar.Text,
                             "El usuario <b>" + item["Nombre"].ToString() + "</b> devolvió: <br> Verificación de Mantenimiento<br>Motivo: "+TxtMotivoDevolver.Text,
                              vCorreoEncargadoZona,
                              "/sites/agencias/pages/mantenimiento/lvPendientesAprobarJefes.aspx"
                           );
                   

                }
            }
            if (vDatosTecnicoResponsable.Rows.Count > 0)
            {
                foreach (DataRow item in vDatosTecnicoResponsable.Rows)
                {
                    //ENVIAR A RESPONSABLE
                    vService.EnviarMensaje(item["Correo"].ToString(),
                        typeBody.EnvioCorreo,
                       "Verificación de Mantenimiento Agencia",
                        "Buen día, se le notifica que se devolvió verificación de mantenimiento, el encargado es " + item["Nombre"].ToString() + ", mantenimiento a agencia " + TxLugar.Text,
                          "El usuario <b>" + vDatos.Rows[0]["Nombre"].ToString() + "</b> devolvió: <br> Verificación de Mantenimiento de Agencia al que ha sido asignado como responsable.<br>Motivo: " + TxtMotivoDevolver.Text,
                            "",
                        "/login.spx"
                        );
                }
            }
            if (vDatosTecnicos.Rows.Count > 0)
            {
                //foreach (DataRow itemT in vDatosTecnicos.Rows)
                //{
                //    vService.EnviarMensaje(itemT["correo"].ToString(),
                //        typeBody.EnvioCorreo,
                //        "Verificación de Mantenimiento Agencia",
                //        "Buen día, se le notifica que se creó verificación de mantenimiento, el encargado es " + vDatosTecnicoResponsable.Rows[0]["Nombre"].ToString() + ", mantenimiento a Agencia " + TxLugarModalEnviarLV.Text,
                //          "El usuario <b>" + vDatos.Rows[0]["Nombre"].ToString() + "</b> creó: <br> Verificación de Mantenimiento de Agencia al que ha sido asignado como parte del equipo de trabajo",
                //            "",
                //        "/login.aspx"
                //        );
                //}
            }
            if (vDatosJefeAgencias.Rows.Count > 0)
            {
                //foreach (DataRow item in vDatosJefeAgencias.Rows)
                //{
                //    //ENVIAR A JEFES DE AGENCIA
                //    if (!item["CorreoJefe"].ToString().Trim().Equals(""))
                //    {
                //        vService.EnviarMensaje(item["CorreoJefe"].ToString(),
                //            typeBody.EnvioCorreo,
                //            "Verificación de Mantenimiento Agencia",
                //                "Buen día, se le notifica que se creó verificación de mantenimiento, el encargado es " + vDatosTecnicoResponsable.Rows[0]["Nombre"].ToString() + ", mantenimiento a Agencia " + TxLugarModalEnviarLV.Text,
                //                  "Se le informa que dicho mantenimiento se hará en la agencia al que usted se encuentra asignado.",
                //                   "",
                //                   ""
                //            );
                //    }
                //}
            }

        }

        void EnviarCorreoAprobar()
        {
            SmtpService vService = new SmtpService();
            string vZonaAgencia = "";
            string vIDMantenimiento = Convert.ToString(Session["AG_LvPC_ID_MANTENIMIENTO_LV_APROBAR_JEFE"]);

            string vQueryD = "[STEISP_AGENCIA_AprobarNotificacion] 9,'" + vIDMantenimiento + "'";
            DataTable vDatosTecnicoResponsable = vConexion.obtenerDataTable(vQueryD);
            string vQueryTecnicos = "[STEISP_AGENCIA_AprobarNotificacion] 10,'" + vIDMantenimiento + "'";
            DataTable vDatosTecnicos = vConexion.obtenerDataTable(vQueryTecnicos);
            string vQueryJefes = "[STEISP_AGENCIA_AprobarNotificacion] 11,'" + vIDMantenimiento + "'";
            DataTable vDatosJefeAgencias = vConexion.obtenerDataTable(vQueryJefes);
            string vQueryZona = "[STEISP_AGENCIA_AprobarNotificacion] 12,'" + vIDMantenimiento + "'";
            DataTable vDatosZona = vConexion.obtenerDataTable(vQueryZona);
            DataTable vDatos = (DataTable)Session["AUTHCLASS"];


            for (int i = 0; i < vDatosZona.Rows.Count; i++)
            {
                vZonaAgencia = vDatosZona.Rows[i]["Zona"].ToString();
            }
            string vCorreoEncargadoZona = "";
            if (vZonaAgencia == "1")
                vCorreoEncargadoZona = "emontoya@bancatlan.hn";
            if (vZonaAgencia == "2")
                vCorreoEncargadoZona = "jdgarcia@bancatlan.hn";
            if (vZonaAgencia == "3")
                vCorreoEncargadoZona = "acalderon@bancatlan.hn";

            if (vDatos.Rows.Count > 0)
            {
                foreach (DataRow item in vDatos.Rows)
                {
                    //ENVIAR A EDWIN
                    //string vNombre = "EDWIN ALBERTO URREA PENA";
                    vService.EnviarMensaje(ConfigurationManager.AppSettings["STEIMail"],
                            typeBody.EnvioCorreo,
                            "Verificación de Mantenimiento Agencia",
                            "Buen día, se le notifica que se aprobó verificación de mantenimiento, el encargado es " + vDatosTecnicoResponsable.Rows[0]["Nombre"].ToString() + ", mantenimiento a Agencia " + TxLugar.Text,
                              "El usuario <b>" + item["Nombre"].ToString() + "</b> aprobó: <br> Verificación de Mantenimiento",
                               vCorreoEncargadoZona,
                               "/sites/agencias/pages/mantenimiento/lvPendientesAprobarJefes.aspx"
                            );
                }
            }
            if (vDatosTecnicoResponsable.Rows.Count > 0)
            {
                foreach (DataRow item in vDatosTecnicoResponsable.Rows)
                {
                    //ENVIAR A RESPONSABLE
                    vService.EnviarMensaje(item["Correo"].ToString(),
                        typeBody.EnvioCorreo,
                       "Verificación de Mantenimiento Agencia",
                        "Buen día, se le notifica que se aprobó verificación de mantenimiento, el encargado es " + item["Nombre"].ToString() + ", mantenimiento a agencia " + TxLugar.Text,
                          "El usuario <b>" + vDatos.Rows[0]["Nombre"].ToString() + "</b> aprobó: <br> Verificación de Mantenimiento de Agencia al que ha sido asignado como responsable.",
                            "",
                        "/login.spx"
                        );
                }
            }
            if (vDatosTecnicos.Rows.Count > 0)
            {
                //foreach (DataRow itemT in vDatosTecnicos.Rows)
                //{
                //    vService.EnviarMensaje(itemT["correo"].ToString(),
                //        typeBody.EnvioCorreo,
                //        "Verificación de Mantenimiento Agencia",
                //        "Buen día, se le notifica que se creó verificación de mantenimiento, el encargado es " + vDatosTecnicoResponsable.Rows[0]["Nombre"].ToString() + ", mantenimiento a Agencia " + TxLugarModalEnviarLV.Text,
                //          "El usuario <b>" + vDatos.Rows[0]["Nombre"].ToString() + "</b> creó: <br> Verificación de Mantenimiento de Agencia al que ha sido asignado como parte del equipo de trabajo",
                //            "",
                //        "/login.aspx"
                //        );
                //}
            }
            if (vDatosJefeAgencias.Rows.Count > 0)
            {
                //foreach (DataRow item in vDatosJefeAgencias.Rows)
                //{
                //    //ENVIAR A JEFES DE AGENCIA
                //    if (!item["CorreoJefe"].ToString().Trim().Equals(""))
                //    {
                //        vService.EnviarMensaje(item["CorreoJefe"].ToString(),
                //            typeBody.EnvioCorreo,
                //            "Verificación de Mantenimiento Agencia",
                //                "Buen día, se le notifica que se creó verificación de mantenimiento, el encargado es " + vDatosTecnicoResponsable.Rows[0]["Nombre"].ToString() + ", mantenimiento a Agencia " + TxLugarModalEnviarLV.Text,
                //                  "Se le informa que dicho mantenimiento se hará en la agencia al que usted se encuentra asignado.",
                //                   "",
                //                   ""
                //            );
                //    }
                //}
            }

        }

        protected void btnModalEnviarLv_Click(object sender, EventArgs e)
        {
            String vEx = Request.QueryString["ex"];
            try {
                if (vEx.Equals("1"))
                {
                    //INSERTAR ID DEL MANTENIMIENTO EN LA TABLA DE PREGUNTAS
                    String vQuery1 = "STEISP_AGENCIA_CompletarListaVerificacion 5," + Session["AG_LvPC_ID_MANTENIMIENTO_LV_COMPLETAR"];
                    Int32 vInformacion1 = vConexion.ejecutarSql(vQuery1);
                    if(vInformacion1 > 0){
                        //Insertar "DATOS TECNICOS" en la tabla de preguntas
                        String vQuery3 = "STEISP_AGENCIA_CompletarListaVerificacion 7," + Session["AG_LvPC_ID_MANTENIMIENTO_LV_COMPLETAR"] +
                                                        "," + RBProbaronEquipo.SelectedValue +
                                                        ",'" + TxMotivoNoProbaronEquipo.Text + "'";
                        Int32 vInformacion3 = vConexion.ejecutarSql(vQuery3);

                        String vQuery6 = "STEISP_AGENCIA_CompletarListaVerificacion 10,'" + Session["AG_LvPC_ID_MANTENIMIENTO_LV_COMPLETAR"] +
                                                                       "','" + TxHoraInicioMant.Text +
                                                                       "','" + TxHoraFinMant.Text +
                                                                       "','" + TxHoraSalidaINFA.Text +
                                                                       "','" + TxHoraLlegadaINFA.Text + "'";
                        Int32 vInformacion6 = vConexion.ejecutarSql(vQuery6);
                        ActualizarAgencia();
                        crearImagenesReducido();
                        EnviarCorreoCrear();
                        limpiar();
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModalEnvioLv();", true);
                        Response.Redirect("/sites/agencias/pages/mantenimiento/lvPendientesCompletar.aspx");
                    }
                }
                else if (vEx.Equals("3"))
                {
                    //ACTUALIZAR DATOS TECNICOS
                    String vQuery1 = "STEISP_AGENCIA_ModificarListaVerificacion 2," + Session["AG_LvPM_ID_MANTENIMIENTO_LV_MODIFICAR"] +
                                                "," + TxCantMaquinas.Text +
                                                "," + TxCantImpresoraFinanciera.Text +
                                                "," + TxCantDatacard.Text +
                                                "," + TxCantEscaner.Text +
                                                ",'"+ TxObservacionesGenerales.Text + "'";
                    Int32 vInformacion1 = vConexion.ejecutarSql(vQuery1);

                   

                    //ACTUALIZAR TARJETA PRUEBAS
                    //Insertar "DATOS PRUEBAS" en la tabla de preguntas
                    String vQuery3 = "[STEISP_AGENCIA_ModificarListaVerificacion] 4," + Session["AG_LvPM_ID_MANTENIMIENTO_LV_MODIFICAR"] +
                                                    "," + RBProbaronEquipo.SelectedValue +
                                                    ",'" + TxMotivoNoProbaronEquipo.Text + "'";
                    Int32 vInformacion3 = vConexion.ejecutarSql(vQuery3);

                    
                    ////DATOS GENERALES
                    String vQuery12 = "STEISP_AGENCIA_ModificarListaVerificacion 13,'" + Session["AG_LvPM_ID_MANTENIMIENTO_LV_MODIFICAR"] +
                                               "','" + TxHoraInicioMant.Text +
                                               "','" + TxHoraFinMant.Text +
                                               "','" + TxHoraSalidaINFA.Text +
                                               "','" + TxHoraLlegadaINFA.Text + "'";
                    Int32 vInformacion12 = vConexion.ejecutarSql(vQuery12);
                    ActualizarAgencia();
                    modificarImagenesReducido();
                    EnviarCorreoModificar();
                    limpiar();
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModalEnvioLv();", true);
                    Response.Redirect("/sites/agencias/pages/mantenimiento/lvPendientesModificar.aspx");
                }
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }           
        }     
        
        private void limpiar()
        {

            TxFechaMant.Text = String.Empty;
            TxSysAid.Text = String.Empty;
            TxLugar.Text = String.Empty;
            TxArea.Text = String.Empty;
            TxCodigoAgencia.Text = String.Empty;
            TxNombreTecnicoResponsable.Text = String.Empty;
            TxIdentidadTecnicoResponsable.Text = String.Empty;

            TxHoraSalidaINFA.Text = String.Empty;
            TxHoraLlegadaINFA.Text = String.Empty;
            TxHoraInicioMant.Text = String.Empty;
            TxHoraFinMant.Text = String.Empty;

            TxCantMaquinas.Text = String.Empty;
            TxCantImpresoraFinanciera.Text = String.Empty;
            TxCantEscaner.Text = String.Empty;
            TxCantDatacard.Text = String.Empty;


            
            TxMotivoNoMantEquipoComu.Text = String.Empty;

            RBProbaronEquipo.SelectedIndex = -1;
            TxMotivoNoProbaronEquipo.Text = String.Empty;

            RblClimatizacionAdecuada.SelectedIndex = -1;
            RblUPS.SelectedIndex = -1;
            RbPolvoSuciedad.SelectedIndex = -1;
            RblHumedadSustancias.SelectedIndex = -1;
            RblRoboDaño.SelectedIndex = -1;
            RblElementosAjenos.SelectedIndex = -1;

            TxObservacionesGenerales.Text = String.Empty;

            //TxResNoManEquipoComu.Text = String.Empty;
            //TxClimatizacion.Text = String.Empty;
            //TxUPS.Text = String.Empty;
            //TxRack.Text = String.Empty;
            //TxPolvoSuciedad.Text = String.Empty;
            //TxHumedadSustancias.Text = String.Empty;
            //TxRoboDaño.Text = String.Empty;
            //TxElementosAjenos.Text = String.Empty;
            //TxEspacioFisico.Text = String.Empty;


            TxResNoManEquipoComu1.Value = String.Empty;
            TxClimatizacion1.Value = String.Empty;
            TxUPS1.Value = String.Empty;
            TxRack1.Value = String.Empty;
            TxPolvoSuciedad1.Value = String.Empty;
            TxHumedadSustancias1.Value = String.Empty;
            TxRoboDaño1.Value = String.Empty;
            TxElementosAjenos1.Value = String.Empty;
            TxEspacioFisico1.Value = String.Empty;

        }
        
        //protected void BtnRegresarCompletarLV_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //     limpiar();
        //     Response.Redirect("/sites/agencias/pages/mantenimiento/lvPendientesAprobarJefes.aspx");
        //    }
        //    catch (Exception Ex) { Mensaje(Ex.Message, WarningType.Danger); }
        //}

        //**********************************************************************************************************************//
        //*****************************************  MODIFICAR LISTA DE VERIFICACION  *****************************************//
        //*********************************************************************************************************************//

        void cargarDataModificar()
        {
            try
            {

                //DATOS GENERALES
                DataTable vDatos = new DataTable();
                vDatos = (DataTable)Session["AG_LvPM_DATOS_GENERALES"];
                TxFechaMant.Text = vDatos.Rows[0]["fecha"].ToString();
                TxSysAid.Text = vDatos.Rows[0]["sysAid"].ToString();
                TxLugar.Text = vDatos.Rows[0]["Lugar"].ToString();
                TxArea.Text = vDatos.Rows[0]["Area"].ToString();
                TxCodigoAgencia.Text = vDatos.Rows[0]["Cod_Agencia"].ToString();
                TxHoraSalidaINFA.Text = vDatos.Rows[0]["horaSalidaInfatlan"].ToString();
                TxHoraLlegadaINFA.Text = vDatos.Rows[0]["horaLlegadaInfatlan"].ToString();
                TxHoraInicioMant.Text = vDatos.Rows[0]["horaMantenimientoInicio"].ToString();
                TxHoraFinMant.Text = vDatos.Rows[0]["horaManteniminetoFinal"].ToString();
                TxNombreTecnicoResponsable.Text = vDatos.Rows[0]["Responsable"].ToString();

                String vQueryProbar = "[STEISP_AGENCIA_ModificarListaVerificacion] 21," + Session["AG_LvPM_ID_MANTENIMIENTO_LV_MODIFICAR"];
                DataTable vDatosProbar = vConexion.obtenerDataTable(vQueryProbar);
                //RBProbaronEquipo.SelectedValue= vDatosProbar.Rows[0]["proboTodoEquipo"].ToString();
                TxMotivoNoProbaronEquipo.Text= vDatosProbar.Rows[0]["motivoNoProboTodoEquipo"].ToString();

                if (vDatosProbar.Rows[0]["proboTodoEquipo"].ToString().Equals("True"))
                {
                    RBProbaronEquipo.SelectedValue = "1";
                    TxMotivoNoProbaronEquipo.Visible = false;
                }
                else
                {
                    RBProbaronEquipo.SelectedValue = "0";
                    TxMotivoNoProbaronEquipo.Visible = true;
                }

                //TECNICO RESPONSABLE  
                DataTable vDatos1 = new DataTable();
                vDatos1 = (DataTable)Session["AG_LvPM_DATOS_TECNICO_RESPONSABLE"];
                TxIdentidadTecnicoResponsable.Text = vDatos1.Rows[0]["identidad"].ToString();


                //TECNICOS PARTICIPANTES
                DataTable vDatosTecnicosParticipantes = (DataTable)Session["AG_LvPM_DATOS_TECNICOS_PARTICIPANTES"];
                GVTecnicosParticipantes.DataSource = vDatosTecnicosParticipantes;

                if (vDatosTecnicosParticipantes.Rows.Count > 0)
                {
                    GVTecnicosParticipantes.DataBind();
                    UPTecnicosParticipantes.Update();

                    DivTecnicosParticipantes.Visible = true;
                    DivAlertaTecnicosParticipantes.Visible = false;
                }
                else
                {
                    DivTecnicosParticipantes.Visible = true;
                    DivAlertaTecnicosParticipantes.Visible = true;
                }

                //SECCION DATOS TECNICOS PREGUNTA
                DataTable vDatos2 = new DataTable();
                vDatos2 = (DataTable)Session["AG_LvPM_DATOS_TECNICOS_PREGUNTAS"];

                TxCantMaquinas.Text = vDatos2.Rows[0]["cantMaquinas"].ToString();
                TxCantImpresoraFinanciera.Text = vDatos2.Rows[0]["cantImpresorasFinancieras"].ToString();
                TxCantEscaner.Text = vDatos2.Rows[0]["cantEscanerFenix"].ToString();
                TxCantDatacard.Text = vDatos2.Rows[0]["cantDatacards"].ToString();

                string RBLManEquipoComuRespuesta = vDatos2.Rows[0]["realizoMantEquipoComu"].ToString();
                TxMotivoNoMantEquipoComu.Text = vDatos2.Rows[0]["motivoNoRealizoMantEquipoComu"].ToString();

                String vDocumentoNoMantEquipoComunicacion = vDatos2.Rows[0]["fotoNoManEquipoComu"].ToString();
                string srcNoMantEquipoComunicacion = "data:image;base64," + vDocumentoNoMantEquipoComunicacion;
                ImgPreviewNoMantEquipoComu.Src = srcNoMantEquipoComunicacion;
                TxResNoManEquipoComu1.Value = "si";
                if (RBLManEquipoComuRespuesta.Equals("True"))
                {
                    
                    //ImgPreviewNoMantEquipoComu.Visible = true;
                    //TxResNoManEquipoComu.Text = "si";
                    TxResNoManEquipoComu1.Value = "si";
                }
                else{
                    
                    //ImgPreviewNoMantEquipoComu.Visible = false;
                    TxMotivoNoMantEquipoComu.Visible = true;
                }


                //SECCION PRUEBAS DE PC
                DataTable vDatos3 = new DataTable();
                vDatos3 = (DataTable)Session["AG_LvPM_DATOS_PRUEBAS_PC"];

                //string RBProbaronEquipoRespuesta = vDatos3.Rows[0]["proboTodoEquipo"].ToString();
                //TxMotivoNoProbaronEquipo.Text = vDatos3.Rows[0]["motivoNoProboTodoEquipo"].ToString();
                //if (RBProbaronEquipoRespuesta.Equals("True"))
                //{
                //    RBProbaronEquipo.SelectedValue = "1";
                //    TxMotivoNoProbaronEquipo.Visible = false;
                //}
                //else
                //{
                //    RBProbaronEquipo.SelectedValue = "0";
                //    TxMotivoNoProbaronEquipo.Visible = true;
                //}


                //SECCION EQUIPO DE COMUNICACION
                DataTable vDatos4 = new DataTable();
                vDatos4 = (DataTable)Session["AG_LvPM_DATOS_EQUIPO_COMUNICACION"];

                string RblClimatizacionAdecuadaRespuesta = vDatos4.Rows[0]["climatizacionAdecuada"].ToString();
                String vDocumentoClimatizacion = vDatos4.Rows[0]["fotoClimatizacionAdecuada"].ToString();
                string srcClimatizacion = "data:image;base64," + vDocumentoClimatizacion;
                ImgPreviewClimatizacion.Src = srcClimatizacion;
                TxClimatizacion1.Value = "si";

                if (RblClimatizacionAdecuadaRespuesta.Equals("True"))
                {
                    RblClimatizacionAdecuada.SelectedValue = "1";
                    //ImgPreviewClimatizacion.Visible = true;
                    //TxClimatizacion.Text = "si";
                    TxClimatizacion1.Value = "si";

                }
                else
                {
                    RblClimatizacionAdecuada.SelectedValue = "0";
                    //ImgPreviewClimatizacion.Visible = false;
                }


                string RblUPSRespuesta = vDatos4.Rows[0]["energiaElectricaUPS"].ToString();
                String vDocumentoUPS = vDatos4.Rows[0]["fotoEnergiaElectricaUPS"].ToString();
                string srcUPS = "data:image;base64," + vDocumentoUPS;
                ImgPreviewUPS.Src = srcUPS;
                TxUPS1.Value = "si";

                if (RblUPSRespuesta.Equals("True"))
                {
                    RblUPS.SelectedValue = "1";
                    //ImgPreviewUPS.Visible = true;
                    //TxUPS.Text = "si";
                    TxUPS1.Value = "si";
                }
                else
                {
                    RblUPS.SelectedValue = "0";
                    //ImgPreviewUPS.Visible = false;
                }

                //SECCION ENTORNO COMUNICACION
                DataTable vDatos5 = new DataTable();
                vDatos5 = (DataTable)Session["AG_LvPM_DATOS_ENTORNO_COMUNICACION"];

                string RbPolvoSuciedadRespuesta = vDatos5.Rows[0]["expuestoPolvoSuciedad"].ToString();
                String vDocumentoPolvoSuciedad = vDatos5.Rows[0]["fotoExpuestoPolvoSuciedad"].ToString();
                string srcPolvoSuciedad = "data:image;base64," + vDocumentoPolvoSuciedad;
                ImgPreviewPolvoSuciedad.Src = srcPolvoSuciedad;
                TxPolvoSuciedad1.Value = "si";

                if (RbPolvoSuciedadRespuesta.Equals("True"))
                {
                    RbPolvoSuciedad.SelectedValue = "1";
                    //ImgPreviewPolvoSuciedad.Visible = true;
                    //TxPolvoSuciedad.Text = "si";
                    TxPolvoSuciedad1.Value = "si";
                }
                else
                {
                    RbPolvoSuciedad.SelectedValue = "0";
                    //ImgPreviewPolvoSuciedad.Visible = false;
                }


                string RblHumedadSustanciasRespuesta = vDatos5.Rows[0]["rastrosHumedadSustancias"].ToString();
                String vDocumentoHumedadSustancias = vDatos5.Rows[0]["fotoRastrosHumedadSustancias"].ToString();
                string srcHumedadSustancias = "data:image;base64," + vDocumentoHumedadSustancias;
                ImgPreviewHumedadSustancias.Src = srcHumedadSustancias;
                TxHumedadSustancias1.Value = "si";

                if (RblHumedadSustanciasRespuesta.Equals("True"))
                {
                    RblHumedadSustancias.SelectedValue = "1";
                    //ImgPreviewHumedadSustancias.Visible = true;
                    //TxHumedadSustancias.Text = "si";
                    TxHumedadSustancias1.Value= "si";
                }
                else
                {
                    RblHumedadSustancias.SelectedValue = "0";
                    //ImgPreviewHumedadSustancias.Visible = false;
                }


                string RblRoboDañoRespuesta = vDatos5.Rows[0]["expuestoRoboDaño"].ToString();
                String vDocumentoRoboDaño = vDatos5.Rows[0]["fotoExpuestoRoboDaño"].ToString();
                string srcRoboDaño = "data:image;base64," + vDocumentoRoboDaño;
                ImgPreviewRoboDaño.Src = srcRoboDaño;
                TxRoboDaño1.Value = "si";

                if (RblRoboDañoRespuesta.Equals("True"))
                {
                    RblRoboDaño.SelectedValue = "1";
                    //ImgPreviewRoboDaño.Visible = true;
                    //TxRoboDaño.Text = "si";
                    TxRoboDaño1.Value = "si";
                }
                else
                {
                    RblRoboDaño.SelectedValue = "0";
                    //ImgPreviewRoboDaño.Visible = false;
                }


                string RblElementosAjenosRespuesta = vDatos5.Rows[0]["encontroElementosExtraños"].ToString();
                String vDocumentoElementosAjenos = vDatos5.Rows[0]["fotoElementosExtraños"].ToString();
                string srcElementosAjenos = "data:image;base64," + vDocumentoElementosAjenos;
                ImgPreviewElementosAjenos.Src = srcElementosAjenos;
                TxElementosAjenos1.Value = "si";

                if (RblElementosAjenosRespuesta.Equals("True"))
                {
                    RblElementosAjenos.SelectedValue = "1";
                    //ImgPreviewElementosAjenos.Visible = true;
                    //TxElementosAjenos.Text = "si";
                    TxElementosAjenos1.Value = "si";
                }
                else
                {
                    RblElementosAjenos.SelectedValue = "0";
                    //ImgPreviewElementosAjenos.Visible = false;
                }


                //IMAGENES OBLIGATORIAS
                DataTable vDatos6 = new DataTable();
                vDatos6 = (DataTable)Session["AG_LvPM_DATOS_IMAGENES_OBLIGATORIAS"];

                String vDocumentoRack = vDatos6.Rows[0]["fotoRackComunicacion"].ToString();
                string srcRack = "data:image;base64," + vDocumentoRack;
                ImgPreviewRack.Src = srcRack;
                ImgPreviewRack.Visible = true;
                //TxRack.Text = "si";
                TxRack1.Value = "si";


                String vDocumentoEntorno = vDatos6.Rows[0]["fotoEntorno"].ToString();
                string srcEntorno = "data:image;base64," + vDocumentoEntorno;
                ImgPreviewEspacioFisico.Src = srcEntorno;
                ImgPreviewEspacioFisico.Visible = true;
                //TxEspacioFisico.Text = "si";
                TxEspacioFisico1.Value = "si";


                TxObservacionesGenerales.Text = vDatos6.Rows[0]["observaciones"].ToString();
                if (TxObservacionesGenerales.Text.Equals(""))
                    TxObservacionesGenerales.Text = "Ninguna";

            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
        
        void habilitarFU() {
            FuClimatizacion.Visible = true;
            FuUPS.Visible = true;
            FuPolvoSuciedad.Visible = true;
            FuHumedadSustancias.Visible = true;
            FuRoboDaño.Visible = true;
            FuElementosAjenos.Visible = true;
        }

        protected void BtnDevolver_Click(object sender, EventArgs e)
        {
            DIVAlerta.Visible = false;
            TxIdMantDevolver.Text = Session["AG_LvPC_ID_MANTENIMIENTO_LV_APROBAR_JEFE"].ToString();
            LbTituloDevolver.Text = "Devolver "+TxLugar.Text;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModalDevolverLv();", true);
        }

        protected void BtnAprobar_Click(object sender, EventArgs e)
        {
            try
            {
                //validacionesAprobarLV();
                Lugar.Text = TxLugar.Text;
                TxIdMantenimiento.Text = Session["AG_LvPC_ID_MANTENIMIENTO_LV_APROBAR_JEFE"].ToString();
                TxFechaModal.Text = TxFechaMant.Text;
                TxAreaModal.Text = TxArea.Text;
                TxResponsableModal.Text = TxNombreTecnicoResponsable.Text;
                //TxMotivoRegreso.Text = TxMotivoCancelacionLV.Text;

                if (Titulo.Text == "Regresar LV")
                {
                    DivMotivo.Visible = true;
                   
                }
                else
                {
                    DivMotivo.Visible = false;
                   
                }

                UpTituloAprobar.Update();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModalAprobacionLv();", true);

            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void BtnModalDevolver_Click(object sender, EventArgs e)
        {
            if (TxtMotivoDevolver.Text == "")
                DIVAlerta.Visible = true;
            else {
                DIVAlerta.Visible = false;
                String vQuery1 = "STEISP_AGENCIA_AprobarLvJefesSuplentes 11," + Session["AG_LvPC_ID_MANTENIMIENTO_LV_APROBAR_JEFE"] +
                            "," + Session["USUARIO"] +
                            ",'" + TxtMotivoDevolver.Text + "'";
                Int32 vInformacion1 = vConexion.ejecutarSql(vQuery1);

                if (vInformacion1 == 1)
                {
                    EnviarCorreoDevolver();
                    Response.Redirect("lvPendientesAprobarJefes.aspx");
                }
            }
        }
    }




}