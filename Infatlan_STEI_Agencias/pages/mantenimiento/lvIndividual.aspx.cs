using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using Infatlan_STEI_Agencias.classes;
using System.Drawing;
 
namespace Infatlan_STEI_Agencias.pages
{
    public partial class LvIndividual : System.Web.UI.Page
    {
        db vConexion = new db();
        public void Mensaje(string vMensaje, WarningType type){
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["USUARIO"] = "acamador";
            String vEx = Request.QueryString["ex"];

            if (!Page.IsPostBack){              


                if (vEx != null)
                {
                    if (vEx.Equals("2"))
                    {
                        modoVistaCampos(true);
                        cargarDataVista();
                    }
                    else if (vEx.Equals("1"))
                    {
                        cargarDataLlenado();
                        OcultarTarjeta();
                        mostrarAsteriscos();
                    }
                    else if (vEx.Equals("3"))
                    {
                        cargarDataModificar();
                        OcultarTarjeta();
                        mostrarAsteriscos();
                        habilitarFU();
                    }
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
            
            RBLManEquipoComu.Enabled = false;
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

            TituloPagina.Text = "Aprobar Lista de Verificación";
            UpTitulo.Update();

            BtnEnviarLv.Visible = false;
            BtnRegresarCompletarLV.Visible = false;
            DivAprobacion.Visible = true;
        }
        
        void cargarDataVista()
        {
            try
            {
                //DATOS GENERALES
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
                if (RBLManEquipoComuRespuesta.Equals("True")){
                    RBLManEquipoComu.SelectedValue = "1";
                    ImgPreviewNoMantEquipoComu.Visible = true;
                } else {
                    RBLManEquipoComu.SelectedValue = "0";
                    ImgPreviewNoMantEquipoComu.Visible = false;               
                    TxMotivoNoMantEquipoComu.Visible = true;
                }


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

                if (RblClimatizacionAdecuadaRespuesta.Equals("True")){
                    RblClimatizacionAdecuada.SelectedValue = "1";
                    ImgPreviewClimatizacion.Visible = true;
                } else{
                    RblClimatizacionAdecuada.SelectedValue = "0";
                    ImgPreviewClimatizacion.Visible = false;
                }


                string RblUPSRespuesta = vDatos4.Rows[0]["energiaElectricaUPS"].ToString();
                String vDocumentoUPS = vDatos4.Rows[0]["fotoEnergiaElectricaUPS"].ToString();
                string srcUPS = "data:image;base64," + vDocumentoUPS;
                ImgPreviewUPS.Src = srcUPS;

                if (RblUPSRespuesta.Equals("True")){
                    RblUPS.SelectedValue = "1";
                    ImgPreviewUPS.Visible = true;
                }else {
                    RblUPS.SelectedValue = "0";
                    ImgPreviewUPS.Visible = false;
                }

            //SECCION ENTORNO COMUNICACION
                DataTable vDatos5 = new DataTable();
                vDatos5 = (DataTable)Session["AG_LvPA_DATOS_ENTORNO_COMUNICACION"];

                string RbPolvoSuciedadRespuesta = vDatos5.Rows[0]["expuestoPolvoSuciedad"].ToString();
                String vDocumentoPolvoSuciedad = vDatos5.Rows[0]["fotoExpuestoPolvoSuciedad"].ToString();
                string srcPolvoSuciedad = "data:image;base64," + vDocumentoPolvoSuciedad;
                ImgPreviewPolvoSuciedad.Src = srcPolvoSuciedad;

                if (RbPolvoSuciedadRespuesta.Equals("True")){
                    RbPolvoSuciedad.SelectedValue = "1";
                    ImgPreviewPolvoSuciedad.Visible = true;
                }else{
                    RbPolvoSuciedad.SelectedValue = "0";
                    ImgPreviewPolvoSuciedad.Visible = false;
                }


                string RblHumedadSustanciasRespuesta = vDatos5.Rows[0]["rastrosHumedadSustancias"].ToString();
                String vDocumentoHumedadSustancias = vDatos5.Rows[0]["fotoRastrosHumedadSustancias"].ToString();
                string srcHumedadSustancias = "data:image;base64," + vDocumentoHumedadSustancias;
                ImgPreviewHumedadSustancias.Src = srcHumedadSustancias;

                if (RblHumedadSustanciasRespuesta.Equals("True")){
                    RblHumedadSustancias.SelectedValue = "1";
                    ImgPreviewHumedadSustancias.Visible = true;
                }else{
                    RblHumedadSustancias.SelectedValue = "0";
                    ImgPreviewHumedadSustancias.Visible = false;
                }


                string RblRoboDañoRespuesta = vDatos5.Rows[0]["expuestoRoboDaño"].ToString();
                String vDocumentoRoboDaño = vDatos5.Rows[0]["fotoExpuestoRoboDaño"].ToString();
                string srcRoboDaño= "data:image;base64," + vDocumentoRoboDaño;
                ImgPreviewRoboDaño.Src = srcRoboDaño;

                if (RblRoboDañoRespuesta.Equals("True")){
                    RblRoboDaño.SelectedValue = "1";
                    ImgPreviewRoboDaño.Visible = true;
                }else{
                    RblRoboDaño.SelectedValue = "0";
                    ImgPreviewRoboDaño.Visible = false;
                }


                string RblElementosAjenosRespuesta = vDatos5.Rows[0]["encontroElementosExtraños"].ToString();
                String vDocumentoElementosAjenos = vDatos5.Rows[0]["fotoElementosExtraños"].ToString();
                string srcElementosAjenos = "data:image;base64," + vDocumentoElementosAjenos;
                ImgPreviewElementosAjenos.Src = srcElementosAjenos;

                if (RblRoboDañoRespuesta.Equals("True")) {
                    RblElementosAjenos.SelectedValue = "1";
                    ImgPreviewElementosAjenos.Visible = true;
                }else{
                    RblElementosAjenos.SelectedValue = "0";
                    ImgPreviewElementosAjenos.Visible = false;                   
                }


            //IMAGENES OBLIGATORIAS
                DataTable vDatos6 = new DataTable();
                vDatos6 = (DataTable)Session["AG_LvPA_DATOS_IMAGENES_OBLIGATORIAS"];

                String vDocumentoRack = vDatos6.Rows[0]["fotoRackComunicacion"].ToString();
                string srcRack= "data:image;base64," + vDocumentoRack;
                ImgPreviewRack.Src = srcRack;
                ImgPreviewRack.Visible = true;

                String vDocumentoEntorno = vDatos6.Rows[0]["fotoEntorno"].ToString();
                string srcEntorno = "data:image;base64," + vDocumentoEntorno;
                ImgPreviewEspacioFisico.Src = srcEntorno;
                ImgPreviewEspacioFisico.Visible = true;


                TxObservacionesGenerales.Text = vDatos6.Rows[0]["observaciones"].ToString();
                if (TxObservacionesGenerales.Text.Equals(""))
                    TxObservacionesGenerales.Text = "Ninguna";

            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
        
        private void validacionesAprobarLV()
        {
            if (RblAprobarLV.SelectedValue.Equals(""))
                throw new Exception("Falta completar opción ¿Desea aprobar LV?.");

            if (RblAprobarLV.SelectedValue.Equals("0") && (TxMotivoCancelacionLV.Text == "" || TxMotivoCancelacionLV.Text == string.Empty))
                throw new Exception("Falta que ingrese el motivo de cancelacion de la lista de verificación");
        }
        
        protected void BtnEnviarAprobacion_Click(object sender, EventArgs e)
        {
            try
            {
                validacionesAprobarLV();
                Lugar.Text = TxLugar.Text;
                TxIdMantenimiento.Text = Session["AG_LvPC_ID_MANTENIMIENTO_LV_APROBAR_JEFE"].ToString();
                TxFechaModal.Text = TxFechaMant.Text;
                TxAreaModal.Text = TxArea.Text;
                TxResponsableModal.Text = TxNombreTecnicoResponsable.Text;
                TxMotivoRegreso.Text = TxMotivoCancelacionLV.Text;

                if (Titulo.Text == "Regresar LV") {
                    DivMotivo.Visible = true;
                    DivRegresarLV.Visible = true;
                    DivAprobarLV.Visible = false;
                } else {
                    DivMotivo.Visible = false;
                    DivAprobarLV.Visible = true;
                    DivRegresarLV.Visible = false;
                }
             
                UpTituloAprobar.Update();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModalAprobacionLv();", true);

            }
            catch (Exception ex){
                    Mensaje(ex.Message, WarningType.Danger);
            }
        }
        
        protected void RblAprobarLV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RblAprobarLV.SelectedValue.Equals("1"))
            {
                DivEtiqueta.Visible = false;
                DivTexto.Visible = false;
                Titulo.Text = "Aprobar LV";
                TxMotivoCancelacionLV.Text = String.Empty;
            }
            else
            {
                DivEtiqueta.Visible = true;
                DivTexto.Visible = true;
                Titulo.Text = "Regresar LV";
                TxMotivoCancelacionLV.Text = String.Empty;
            }
        }
        
        private void LimpiarAprobarLV()
        {
            RblAprobarLV.SelectedIndex = -1;
            TxMotivoCancelacionLV.Text = String.Empty;
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
            LimpiarAprobarLV();
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModalAprobarRegresarLv();", true);
            Response.Redirect("/sites/agencias/pages/mantenimiento/lvPendientesAprobarJefes.aspx");          
        }
        
        protected void BtnRegresarPendienteAprobar_Click(object sender, EventArgs e){
            LimpiarAprobarLV();
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

                TituloPagina.Text = "Completar Lista de Verificación";

            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
        
        void OcultarTarjeta()
        {
            DivAprobacion.Visible = false;
            ocultarBotonVolver1.Visible = true;
            UpdatePanel5.Update();
        }
        
        void mostrarAsteriscos()
        {
            lbHoraSalida.Visible = true;
            lbHoraLlegada.Visible = true;
            lbInicioMant.Visible = true;
            lbFinMant.Visible = true;
            lbCantMaquinas.Visible = true;
            lbCantImpresora.Visible = true;
            lbCantEscaner.Visible = true;
            lbCantDatacard.Visible = true;
            lbRealizoMantEquipoComu.Visible = true;
            lbProbaronEquipo.Visible = true;
            lbClimatizacion.Visible = true;
            lbUps.Visible = true;
            lbPolvo.Visible = true;
            lbHumedad.Visible = true;
            lbRobo.Visible = true;
            lbElementos.Visible = true;
            lbRack.Visible = true;
            lbEntorno.Visible = true;

            //DivRack.Visible = true;
            //DivEspacio.Visible = true;
        }
        
        protected void RBLManEquipoComu_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (RBLManEquipoComu.SelectedValue.Equals("1"))
            {
                ImgPreviewNoMantEquipoComu.Visible = true;
                TxMotivoNoMantEquipoComu.Visible = false;
                UpdatePanel2.Update();
                FuImageNoMantEquipoComu.Visible = true;
                //TxResNoManEquipoComu.Text = String.Empty;
                TxResNoManEquipoComu1.Value = String.Empty;
                UpdatePanel8.Update();
                ImgPreviewNoMantEquipoComu.Src = "/assets/images/vistaPrevia1.JPG";
            }
            else
            {
                TxMotivoNoMantEquipoComu.Visible = true;
                ImgPreviewNoMantEquipoComu.Visible = false;
                FuImageNoMantEquipoComu.Visible = false;
                UpdatePanel2.Update();
                //TxResNoManEquipoComu.Text = String.Empty;
                TxResNoManEquipoComu1.Value = String.Empty;
                UpdatePanel8.Update();
            }


        }
        
        protected void RBProbaronEquipo_SelectedIndexChanged(object sender, EventArgs e)
        {
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
        
        protected void RblClimatizacionAdecuada_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RblClimatizacionAdecuada.SelectedValue.Equals("0"))
            {
                FuClimatizacion.Visible = false;
                ImgPreviewClimatizacion.Visible = false;
                UpClimatizacion.Update();
                //TxClimatizacion.Text = String.Empty;
                TxClimatizacion1.Value = String.Empty;
                UpdatePanel8.Update();
            } else{
                FuClimatizacion.Visible = true;
                ImgPreviewClimatizacion.Visible = true;
                UpClimatizacion.Update();
                //TxClimatizacion.Text = String.Empty;
                TxClimatizacion1.Value = String.Empty;
                UpdatePanel8.Update();
                ImgPreviewClimatizacion.Src = "/assets/images/vistaPrevia1.JPG";
            }            
        }
        
        protected void RblUPS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RblUPS.SelectedValue.Equals("0"))
            {
                FuUPS.Visible = false;
                ImgPreviewUPS.Visible = false;
                UpUPS.Update();
                //TxUPS.Text = String.Empty;
                TxUPS1.Value = String.Empty;
                UpdatePanel8.Update();
            }else{
                FuUPS.Visible = true;
                ImgPreviewUPS.Visible = true;
                UpUPS.Update();
                //TxUPS.Text = String.Empty;
                TxUPS1.Value = String.Empty;
                UpdatePanel8.Update();
                ImgPreviewUPS.Src = "/assets/images/vistaPrevia1.JPG";
            }
        }
        
        protected void RbPolvoSuciedad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RbPolvoSuciedad.SelectedValue.Equals("0"))
            {
                FuPolvoSuciedad.Visible = false;
                ImgPreviewPolvoSuciedad.Visible = false;
                UpPolvoSuciedad.Update();
                //TxPolvoSuciedad.Text = String.Empty;
                TxPolvoSuciedad1.Value = String.Empty;
                UpdatePanel8.Update();
            }else{
                FuPolvoSuciedad.Visible = true;
                ImgPreviewPolvoSuciedad.Visible = true;
                UpPolvoSuciedad.Update();
                //TxPolvoSuciedad.Text = String.Empty;
                TxPolvoSuciedad1.Value = String.Empty;
                UpdatePanel8.Update();
                ImgPreviewPolvoSuciedad.Src = "/assets/images/vistaPrevia1.JPG";
            }
        }
        
        protected void RblHumedadSustancias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RblHumedadSustancias.SelectedValue.Equals("0"))
            {
                FuHumedadSustancias.Visible = false;
                ImgPreviewHumedadSustancias.Visible = false;
                UpHumedadSustancias.Update();
                //TxHumedadSustancias.Text = String.Empty;
                TxHumedadSustancias1.Value = String.Empty;
                UpdatePanel8.Update();
            }else{
                FuHumedadSustancias.Visible = true;
                ImgPreviewHumedadSustancias.Visible = true;
                UpHumedadSustancias.Update();
                //TxHumedadSustancias.Text = String.Empty;
                TxHumedadSustancias1.Value = String.Empty;
                UpdatePanel8.Update();
                ImgPreviewHumedadSustancias.Src = "/assets/images/vistaPrevia1.JPG";
            }
        }     
        
        protected void RblRoboDaño_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RblRoboDaño.SelectedValue.Equals("0"))
            {
                FuRoboDaño.Visible = false;
                ImgPreviewRoboDaño.Visible = false;
                UpRoboDaño.Update();
                //TxRoboDaño.Text = String.Empty;
                TxRoboDaño1.Value = String.Empty;
                UpdatePanel8.Update();
            }else{
                FuRoboDaño.Visible = true;
                ImgPreviewRoboDaño.Visible = true;
                UpRoboDaño.Update();
                //TxRoboDaño.Text = String.Empty;
                TxRoboDaño1.Value = String.Empty;
                UpdatePanel8.Update();
                ImgPreviewRoboDaño.Src = "/assets/images/vistaPrevia1.JPG";
            }
        }
        
        protected void RblElementosAjenos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RblElementosAjenos.SelectedValue.Equals("0"))
            {
                FuElementosAjenos.Visible = false;
                ImgPreviewElementosAjenos.Visible = false;
                UpElementosAjenos.Update();
                //TxElementosAjenos.Text = String.Empty;
                TxElementosAjenos1.Value = String.Empty;
                UpdatePanel8.Update();
            }
            else
            {
                FuElementosAjenos.Visible = true;
                ImgPreviewElementosAjenos.Visible = true;
                UpElementosAjenos.Update();
                //TxElementosAjenos.Text = String.Empty;
                TxElementosAjenos1.Value = String.Empty;
                UpdatePanel8.Update();
                ImgPreviewElementosAjenos.Src = "/assets/images/vistaPrevia1.JPG";
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
                    TituloModalEnviarLV.Text = "Enviar LV Modificada " + TxLugar.Text;
                }
                else{
                    TxIdMantenimientoModalEnviarLV.Text = Session["AG_LvPC_ID_MANTENIMIENTO_LV_COMPLETAR"].ToString();
                    TituloModalEnviarLV.Text = "Enviar LV  " + TxLugar.Text;
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

            if (TxCantMaquinas.Text == "" || TxCantMaquinas.Text == string.Empty)
                throw new Exception("Falta completar el campo (Cant Maquinas).");

            if (TxCantImpresoraFinanciera.Text == "" || TxCantImpresoraFinanciera.Text == string.Empty)
                throw new Exception("Falta completar el campo (Cant Impresoras Financieras).");
            
            if (TxCantEscaner.Text == "" || TxCantEscaner.Text == string.Empty)
                throw new Exception("Falta completar el campo (Cant Escaner Fenix).");

            if (TxCantDatacard.Text == "" || TxCantDatacard.Text == string.Empty)
                throw new Exception("Falta completar el campo (Cant Datacards).");

            if (RBLManEquipoComu.SelectedValue.Equals("") )
                throw new Exception("Falta completar opción (¿Realizó mantenimiento al equipo de comunicación?).");

            if (RBLManEquipoComu.SelectedValue.Equals(""))
                throw new Exception("Falta completar opción (¿Realizó mantenimiento al equipo de comunicación?).");


            if (RBLManEquipoComu.SelectedValue.Equals("0") && (TxMotivoNoMantEquipoComu.Text== string.Empty  || TxMotivoNoMantEquipoComu.Text == ""))
                throw new Exception("Falta ingresar motivo por el cual no dio mantenimiento al equipo de comunicación.");

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
          
            if (RBLManEquipoComu.SelectedValue.Equals("1") && TxResNoManEquipoComu1.Value== string.Empty)
                throw new Exception("Falta que adjunte imagen del mantenimiento al equipo de comunicación.");

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

                    ////INSERTAR "DATOS TECNICOS"
                    //Imagen No Mantenimiento EquipoComunicacion
                    String vNombreNoMantEquipoComu = String.Empty;
                    HttpPostedFile bufferDeposito1T = FuImageNoMantEquipoComu.PostedFile;
                    byte[] vFileDepositoNoMantEquipoComu = null;
                    String vExtensionNoMantEquipoComu = String.Empty;

                    if (bufferDeposito1T != null)
                    {
                        vNombreNoMantEquipoComu = FuImageNoMantEquipoComu.FileName;
                        Stream vStream = bufferDeposito1T.InputStream;
                        BinaryReader vReader = new BinaryReader(vStream);
                        vFileDepositoNoMantEquipoComu = vReader.ReadBytes((int)vStream.Length);
                        vExtensionNoMantEquipoComu = System.IO.Path.GetExtension(FuImageNoMantEquipoComu.FileName);
                    }


                    String vArchivoNoMantEquipoComu = String.Empty;
                    if (vFileDepositoNoMantEquipoComu != null)
                    {
                        vArchivoNoMantEquipoComu = Convert.ToBase64String(vFileDepositoNoMantEquipoComu);
                    }
                    else
                    {
                        vArchivoNoMantEquipoComu = "";
                    }


                    String vQuery2 = "STEISP_AGENCIA_CompletarListaVerificacion 6," + Session["AG_LvPC_ID_MANTENIMIENTO_LV_COMPLETAR"] +
                                                "," + TxCantMaquinas.Text +
                                                "," + TxCantImpresoraFinanciera.Text +
                                                "," + TxCantDatacard.Text +
                                                "," + TxCantEscaner.Text +
                                                "," + RBLManEquipoComu.SelectedValue +
                                                ",'" + TxMotivoNoMantEquipoComu.Text +
                                                "','" + vArchivoNoMantEquipoComu +
                                                "','" + vExtensionNoMantEquipoComu + "'";

                    Int32 vInformacion2 = vConexion.ejecutarSql(vQuery2);

                    //Insertar "DATOS TECNICOS" en la tabla de preguntas
                    String vQuery3 = "STEISP_AGENCIA_CompletarListaVerificacion 7," + Session["AG_LvPC_ID_MANTENIMIENTO_LV_COMPLETAR"] +
                                                    "," + RBProbaronEquipo.SelectedValue +
                                                    ",'" + TxMotivoNoProbaronEquipo.Text + "'";
                    Int32 vInformacion3 = vConexion.ejecutarSql(vQuery3);


                    // //Insertar "EQUIPO DE COMUNICACION" en la tabla de preguntas
                    // //Imagen No Mantenimiento EquipoComunicacion
                    String vNombreClimatizacion = String.Empty;
                    HttpPostedFile bufferDepositoClimatizacion = FuClimatizacion.PostedFile;
                    byte[] vFileDepositoClimatizacion = null;
                    String vExtensionClimatizacion = String.Empty;

                    if (bufferDepositoClimatizacion != null)
                    {
                        vNombreClimatizacion = FuClimatizacion.FileName;
                        Stream vStream = bufferDepositoClimatizacion.InputStream;
                        BinaryReader vReader = new BinaryReader(vStream);
                        vFileDepositoClimatizacion = vReader.ReadBytes((int)vStream.Length);
                        vExtensionClimatizacion = System.IO.Path.GetExtension(FuClimatizacion.FileName);
                    }


                    String vArchivoClimatizacion = String.Empty;
                    if (vFileDepositoClimatizacion != null)
                    {
                        vArchivoClimatizacion = Convert.ToBase64String(vFileDepositoClimatizacion);
                    }
                    else
                    {
                        vArchivoClimatizacion = "";
                    }

                    //Imagen UPS
                    String vNombreUPS = String.Empty;
                    HttpPostedFile bufferDepositoUPS = FuUPS.PostedFile;
                    byte[] vFileDepositoUPS = null;
                    String vExtensionUPS = String.Empty;

                    if (bufferDepositoUPS != null)
                    {
                        vNombreUPS = FuUPS.FileName;
                        Stream vStream = bufferDepositoUPS.InputStream;
                        BinaryReader vReader = new BinaryReader(vStream);
                        vFileDepositoUPS = vReader.ReadBytes((int)vStream.Length);
                        vExtensionUPS = System.IO.Path.GetExtension(FuUPS.FileName);
                    }


                    String vArchivoUPS = String.Empty;
                    if (vFileDepositoUPS != null)
                    {
                        vArchivoUPS = Convert.ToBase64String(vFileDepositoUPS);
                    }
                    else
                    {
                        vArchivoUPS = "";
                    }


                    //Imagen Rack
                    String vNombreRack = String.Empty;
                    HttpPostedFile bufferDepositoRack = FuRack.PostedFile;
                    byte[] vFileDepositoRack = null;
                    String vExtensionRack = String.Empty;

                    if (bufferDepositoRack != null)
                    {
                        vNombreRack = FuRack.FileName;
                        Stream vStream = bufferDepositoRack.InputStream;
                        BinaryReader vReader = new BinaryReader(vStream);
                        vFileDepositoRack = vReader.ReadBytes((int)vStream.Length);
                        vExtensionRack = System.IO.Path.GetExtension(FuRack.FileName);
                    }


                    String vArchivoRack = String.Empty;
                    if (vFileDepositoRack != null)
                    {
                        vArchivoRack = Convert.ToBase64String(vFileDepositoRack);
                    }
                    else
                    {
                        vArchivoRack = "";
                    }


                    String vQuery4 = "STEISP_AGENCIA_CompletarListaVerificacion 8," + Session["AG_LvPC_ID_MANTENIMIENTO_LV_COMPLETAR"] +
                                                "," + RblClimatizacionAdecuada.SelectedValue +
                                                ",'" + vArchivoRack +
                                                "','" + vExtensionRack + "'" +

                                                "," + RblUPS.SelectedValue +
                                                ",'" + vArchivoUPS +
                                                "','" + vExtensionUPS + "'" +

                                                ",'" + vArchivoRack +
                                                "','" + vExtensionRack + "'";

                    Int32 vInformacion4 = vConexion.ejecutarSql(vQuery4);


                    //Insertar "ENTORNO CUARTO DE COMUNICACIONES" en la tabla de preguntas
                    //Imagen Polvo o Suciedad
                    String vNombrePolvo = String.Empty;
                    HttpPostedFile bufferDepositoPolvo = FuPolvoSuciedad.PostedFile;
                    byte[] vFileDepositoPolvo = null;
                    String vExtensionPolvo = String.Empty;

                    if (bufferDepositoPolvo != null)
                    {
                        vNombrePolvo = FuPolvoSuciedad.FileName;
                        Stream vStream = bufferDepositoPolvo.InputStream;
                        BinaryReader vReader = new BinaryReader(vStream);
                        vFileDepositoPolvo = vReader.ReadBytes((int)vStream.Length);
                        vExtensionPolvo = System.IO.Path.GetExtension(FuPolvoSuciedad.FileName);
                    }


                    String vArchivoPolvo = String.Empty;
                    if (vFileDepositoPolvo != null)
                    {
                        vArchivoPolvo = Convert.ToBase64String(vFileDepositoPolvo);
                    }
                    else
                    {
                        vArchivoPolvo = "";
                    }

                    //Imagen Humedad
                    String vNombreHumedad = String.Empty;
                    HttpPostedFile bufferDepositoHumedad = FuHumedadSustancias.PostedFile;
                    byte[] vFileDepositoHumedad = null;
                    String vExtensionHumedad = String.Empty;

                    if (bufferDepositoHumedad != null)
                    {
                        vNombreHumedad = FuHumedadSustancias.FileName;
                        Stream vStream = bufferDepositoHumedad.InputStream;
                        BinaryReader vReader = new BinaryReader(vStream);
                        vFileDepositoHumedad = vReader.ReadBytes((int)vStream.Length);
                        vExtensionHumedad = System.IO.Path.GetExtension(FuHumedadSustancias.FileName);
                    }


                    String vArchivoHumedad = String.Empty;
                    if (vFileDepositoHumedad != null)
                    {
                        vArchivoHumedad = Convert.ToBase64String(vFileDepositoHumedad);
                    }
                    else
                    {
                        vArchivoHumedad = "";
                    }


                    //Imagen Robo
                    String vNombreRobo = String.Empty;
                    HttpPostedFile bufferDepositoRobo = FuRoboDaño.PostedFile;
                    byte[] vFileDepositoRobo = null;
                    String vExtensionRobo = String.Empty;

                    if (bufferDepositoRobo != null)
                    {
                        vNombreRobo = FuRoboDaño.FileName;
                        Stream vStream = bufferDepositoRobo.InputStream;
                        BinaryReader vReader = new BinaryReader(vStream);
                        vFileDepositoRobo = vReader.ReadBytes((int)vStream.Length);
                        vExtensionRobo = System.IO.Path.GetExtension(FuRoboDaño.FileName);
                    }

                    String vArchivoRobo = String.Empty;
                    if (vFileDepositoRobo != null)
                    {
                        vArchivoRobo = Convert.ToBase64String(vFileDepositoRobo);
                    }
                    else
                    {
                        vArchivoRobo = "";
                    }



                    //Imagen Elementos Ajenos
                    String vNombreElementosAjenos = String.Empty;
                    HttpPostedFile bufferDepositoElementosAjenos = FuElementosAjenos.PostedFile;
                    byte[] vFileDepositoElementosAjenos = null;
                    String vExtensionElementosAjenos = String.Empty;

                    if (bufferDepositoElementosAjenos != null)
                    {
                        vNombreElementosAjenos = FuElementosAjenos.FileName;
                        Stream vStream = bufferDepositoElementosAjenos.InputStream;
                        BinaryReader vReader = new BinaryReader(vStream);
                        vFileDepositoElementosAjenos = vReader.ReadBytes((int)vStream.Length);
                        vExtensionElementosAjenos = System.IO.Path.GetExtension(FuElementosAjenos.FileName);
                    }


                    String vArchivoElementosAjenos = String.Empty;
                    if (vFileDepositoElementosAjenos != null)
                    {
                        vArchivoElementosAjenos = Convert.ToBase64String(vFileDepositoElementosAjenos);
                    }
                    else
                    {
                        vArchivoElementosAjenos = "";
                    }



                    //Imagen Entorno
                    String vNombreDepotEntorno = String.Empty;
                    HttpPostedFile bufferDepositoEntorno = FuEspacioFisico.PostedFile;
                    byte[] vFileDepositEntorno = null;
                    String vExtensionEntorno = String.Empty;

                    if (bufferDepositoEntorno != null)
                    {
                        vNombreDepotEntorno = FuEspacioFisico.FileName;
                        Stream vStream = bufferDepositoEntorno.InputStream;
                        BinaryReader vReader = new BinaryReader(vStream);
                        vFileDepositEntorno = vReader.ReadBytes((int)vStream.Length);
                        vExtensionEntorno = System.IO.Path.GetExtension(FuEspacioFisico.FileName);
                    }


                    String vArchivoEntorno = String.Empty;
                    if (vFileDepositEntorno != null)
                    {
                        vArchivoEntorno = Convert.ToBase64String(vFileDepositEntorno);
                    }
                    else
                    {
                        vArchivoEntorno = "";
                    }

                    String vQuery5 = "STEISP_AGENCIA_CompletarListaVerificacion 9," + Session["AG_LvPC_ID_MANTENIMIENTO_LV_COMPLETAR"] +
                                                "," + RbPolvoSuciedad.SelectedValue +
                                                ",'" + vArchivoPolvo +
                                                "','" + vExtensionPolvo + "'" +

                                                "," + RblHumedadSustancias.SelectedValue +
                                                ",'" + vArchivoHumedad +
                                                "','" + vExtensionHumedad + "'" +

                                                "," + RblRoboDaño.SelectedValue +
                                                ",'" + vArchivoRobo +
                                                "','" + vExtensionRobo + "'" +

                                                "," + RblElementosAjenos.SelectedValue +
                                                ",'" + vArchivoElementosAjenos +
                                                "','" + vExtensionElementosAjenos + "'" +


                                                ",'" + vArchivoEntorno +
                                                "','" + vExtensionEntorno + "','" +
                                                    TxObservacionesGenerales.Text + "'";

                    Int32 vInformacion5 = vConexion.ejecutarSql(vQuery5);


                    String vQuery6 = "STEISP_AGENCIA_CompletarListaVerificacion 10,'" + Session["AG_LvPC_ID_MANTENIMIENTO_LV_COMPLETAR"] +
                                                                   "','" + TxHoraInicioMant.Text +
                                                                   "','" + TxHoraFinMant.Text +
                                                                   "','" + TxHoraSalidaINFA.Text +
                                                                   "','" + TxHoraLlegadaINFA.Text + "'";
                    Int32 vInformacion6 = vConexion.ejecutarSql(vQuery6);
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

                    //Imagen No Mantenimiento EquipoComunicacion
                    String vNombreNoMantEquipoComu = String.Empty;
                    HttpPostedFile bufferDeposito1T = FuImageNoMantEquipoComu.PostedFile;
                    byte[] vFileDepositoNoMantEquipoComu = null;
                    String vExtensionNoMantEquipoComu = String.Empty;
                    String vArchivoNoMantEquipoComu = String.Empty;
                    vNombreNoMantEquipoComu = FuImageNoMantEquipoComu.FileName;

                    if (vNombreNoMantEquipoComu != "" && RBLManEquipoComu.SelectedValue=="1")
                    {
                        
                        Stream vStream = bufferDeposito1T.InputStream;
                        BinaryReader vReader = new BinaryReader(vStream);
                        vFileDepositoNoMantEquipoComu = vReader.ReadBytes((int)vStream.Length);
                        vExtensionNoMantEquipoComu = System.IO.Path.GetExtension(FuImageNoMantEquipoComu.FileName);                  
                        vArchivoNoMantEquipoComu = Convert.ToBase64String(vFileDepositoNoMantEquipoComu);
                        TxMotivoNoMantEquipoComu.Text= String.Empty;



                        String vQuery2 = "STEISP_AGENCIA_ModificarListaVerificacion 3," + Session["AG_LvPM_ID_MANTENIMIENTO_LV_MODIFICAR"] +
                                           "," + RBLManEquipoComu.SelectedValue +
                                           ",'" + vArchivoNoMantEquipoComu +
                                           "','" + vExtensionNoMantEquipoComu +
                                           "','" + TxMotivoNoMantEquipoComu.Text + "'";
                        Int32 vInformacion2 = vConexion.ejecutarSql(vQuery2);


                    }
                    else if(vNombreNoMantEquipoComu == "" && RBLManEquipoComu.SelectedValue == "0")
                    {
                        vExtensionNoMantEquipoComu = "";
                        vArchivoNoMantEquipoComu = "";

                        String vQuery2 = "STEISP_AGENCIA_ModificarListaVerificacion 3," + Session["AG_LvPM_ID_MANTENIMIENTO_LV_MODIFICAR"] +
                                           "," + RBLManEquipoComu.SelectedValue +
                                           ",'" + vArchivoNoMantEquipoComu +
                                           "','" + vExtensionNoMantEquipoComu +
                                           "','" + TxMotivoNoMantEquipoComu.Text + "'";
                        Int32 vInformacion2 = vConexion.ejecutarSql(vQuery2);

                    }



                    //ACTUALIZAR TARJETA PRUEBAS
                    //Insertar "DATOS PRUEBAS" en la tabla de preguntas
                    String vQuery3 = "STEISP_AGENCIA_CompletarListaVerificacion 4," + Session["AG_LvPM_ID_MANTENIMIENTO_LV_MODIFICAR"] +
                                                    "," + RBProbaronEquipo.SelectedValue +
                                                    ",'" + TxMotivoNoProbaronEquipo.Text + "'";
                    Int32 vInformacion3 = vConexion.ejecutarSql(vQuery3);



                    // Insertar "EQUIPO DE COMUNICACION" en la tabla de preguntas
                    // Imagen No Mantenimiento EquipoComunicacion
                    String vNombreClimatizacion = String.Empty;
                    HttpPostedFile bufferDepositoClimatizacion = FuClimatizacion.PostedFile;
                    byte[] vFileDepositoClimatizacion = null;
                    String vExtensionClimatizacion = String.Empty;
                    String vArchivoClimatizacion = String.Empty;
                    vNombreClimatizacion = FuClimatizacion.FileName;

                    if (vNombreClimatizacion != "" && RblClimatizacionAdecuada.SelectedValue == "1")
                    {                       
                        Stream vStream = bufferDepositoClimatizacion.InputStream;
                        BinaryReader vReader = new BinaryReader(vStream);
                        vFileDepositoClimatizacion = vReader.ReadBytes((int)vStream.Length);
                        vExtensionClimatizacion = System.IO.Path.GetExtension(FuClimatizacion.FileName);
                        vArchivoClimatizacion = Convert.ToBase64String(vFileDepositoClimatizacion);

                        String vQuery4 = "STEISP_AGENCIA_ModificarListaVerificacion 5," + Session["AG_LvPM_ID_MANTENIMIENTO_LV_MODIFICAR"] +
                    "," + RblClimatizacionAdecuada.SelectedValue +
                    ",'" + vArchivoClimatizacion +
                    "','" + vExtensionClimatizacion + "'";
                        Int32 vInformacion4 = vConexion.ejecutarSql(vQuery4);


                    }
                    else if (vNombreClimatizacion == "" && RblClimatizacionAdecuada.SelectedValue == "0")
                    {
                        vExtensionClimatizacion ="";
                        vArchivoClimatizacion = "";

                        String vQuery4 = "STEISP_AGENCIA_ModificarListaVerificacion 5," + Session["AG_LvPM_ID_MANTENIMIENTO_LV_MODIFICAR"] +
                    "," + RblClimatizacionAdecuada.SelectedValue +
                    ",'" + vArchivoClimatizacion +
                    "','" + vExtensionClimatizacion + "'";
                        Int32 vInformacion4 = vConexion.ejecutarSql(vQuery4);

                    }




                    //Imagen UPS
                    String vNombreUPS = String.Empty;
                    HttpPostedFile bufferDepositoUPS = FuUPS.PostedFile;
                    byte[] vFileDepositoUPS = null;
                    String vExtensionUPS = String.Empty;
                    String vArchivoUPS = String.Empty;
                    vNombreUPS = FuUPS.FileName;

                    if (vNombreUPS != "" && RblUPS.SelectedValue == "1")
                    {
                        
                        Stream vStream = bufferDepositoUPS.InputStream;
                        BinaryReader vReader = new BinaryReader(vStream);
                        vFileDepositoUPS = vReader.ReadBytes((int)vStream.Length);
                        vExtensionUPS = System.IO.Path.GetExtension(FuUPS.FileName);
                        vArchivoUPS = Convert.ToBase64String(vFileDepositoUPS);
                        
                        String vQuery5 = "STEISP_AGENCIA_ModificarListaVerificacion 6," + Session["AG_LvPM_ID_MANTENIMIENTO_LV_MODIFICAR"] +
                                         "," + RblUPS.SelectedValue +
                                         ",'" + vArchivoUPS +
                                         "','" + vExtensionUPS + "'";
                        Int32 vInformacion5 = vConexion.ejecutarSql(vQuery5);
                    }
                    else if (vNombreUPS == "" && RblUPS.SelectedValue == "0")
                    {
                        vExtensionUPS = "";
                        vArchivoUPS = "";

                        String vQuery5 = "STEISP_AGENCIA_ModificarListaVerificacion 6," + Session["AG_LvPM_ID_MANTENIMIENTO_LV_MODIFICAR"] +
                                         "," + RblUPS.SelectedValue +
                                         ",'" + vArchivoUPS +
                                         "','" + vExtensionUPS + "'";
                                                Int32 vInformacion5 = vConexion.ejecutarSql(vQuery5);
                    }




                    //Imagen Rack
                    String vNombreRack = String.Empty;
                    HttpPostedFile bufferDepositoRack = FuRack.PostedFile;
                    byte[] vFileDepositoRack = null;
                    String vExtensionRack = String.Empty;
                    String vArchivoRack = String.Empty;
                    vNombreRack = FuRack.FileName;

                    if (vNombreRack != "")
                    {
                        
                        Stream vStream = bufferDepositoRack.InputStream;
                        BinaryReader vReader = new BinaryReader(vStream);
                        vFileDepositoRack = vReader.ReadBytes((int)vStream.Length);
                        vExtensionRack = System.IO.Path.GetExtension(FuRack.FileName);
                        vArchivoRack = Convert.ToBase64String(vFileDepositoRack);

                        String vQuery6 = "STEISP_AGENCIA_ModificarListaVerificacion 7," + Session["AG_LvPM_ID_MANTENIMIENTO_LV_MODIFICAR"] +
                                         ",'" + vArchivoRack +
                                         "','" + vExtensionRack + "'";
                        Int32 vInformacion6 = vConexion.ejecutarSql(vQuery6);
                    }


                    //Insertar "ENTORNO CUARTO DE COMUNICACIONES" en la tabla de preguntas
                    //Imagen Polvo o Suciedad
                    String vNombrePolvo = String.Empty;
                    HttpPostedFile bufferDepositoPolvo = FuPolvoSuciedad.PostedFile;
                    byte[] vFileDepositoPolvo = null;
                    String vExtensionPolvo = String.Empty;
                    String vArchivoPolvo = String.Empty;
                    vNombrePolvo = FuPolvoSuciedad.FileName;

                    if (vNombrePolvo != "" && RbPolvoSuciedad.SelectedValue=="1")
                    {
                        vNombrePolvo = FuPolvoSuciedad.FileName;
                        Stream vStream = bufferDepositoPolvo.InputStream;
                        BinaryReader vReader = new BinaryReader(vStream);
                        vFileDepositoPolvo = vReader.ReadBytes((int)vStream.Length);
                        vExtensionPolvo = System.IO.Path.GetExtension(FuPolvoSuciedad.FileName);
                        vArchivoPolvo = Convert.ToBase64String(vFileDepositoPolvo);
                        String vQuery7 = "STEISP_AGENCIA_ModificarListaVerificacion 8," + Session["AG_LvPM_ID_MANTENIMIENTO_LV_MODIFICAR"] +
                                            "," + RbPolvoSuciedad.SelectedValue +
                                            ",'" + vArchivoPolvo +
                                            "','" + vExtensionPolvo + "'";
                        Int32 vInformacion7 = vConexion.ejecutarSql(vQuery7);

                    }
                    else if(vNombrePolvo == "" && RbPolvoSuciedad.SelectedValue == "0")
                    {
                        vExtensionPolvo = "";
                        vArchivoPolvo = "";
                        String vQuery7 = "STEISP_AGENCIA_ModificarListaVerificacion 8," + Session["AG_LvPM_ID_MANTENIMIENTO_LV_MODIFICAR"] +
                                        "," + RbPolvoSuciedad.SelectedValue +
                                        ",'" + vArchivoPolvo +
                                        "','" + vExtensionPolvo + "'";
                        Int32 vInformacion7 = vConexion.ejecutarSql(vQuery7);
                    }




                    //Imagen Humedad
                    String vNombreHumedad = String.Empty;
                    HttpPostedFile bufferDepositoHumedad = FuHumedadSustancias.PostedFile;
                    byte[] vFileDepositoHumedad = null;
                    String vExtensionHumedad = String.Empty;
                    String vArchivoHumedad = String.Empty;
                    vNombreHumedad = FuHumedadSustancias.FileName;

                    if (vNombreHumedad != "" && RblHumedadSustancias.SelectedValue=="1")
                    {
                        
                        Stream vStream = bufferDepositoHumedad.InputStream;
                        BinaryReader vReader = new BinaryReader(vStream);
                        vFileDepositoHumedad = vReader.ReadBytes((int)vStream.Length);
                        vExtensionHumedad = System.IO.Path.GetExtension(FuHumedadSustancias.FileName);
                        vArchivoHumedad = Convert.ToBase64String(vFileDepositoHumedad);
                        String vQuery8 = "STEISP_AGENCIA_ModificarListaVerificacion 9," + Session["AG_LvPM_ID_MANTENIMIENTO_LV_MODIFICAR"] +
                                            "," + RblHumedadSustancias.SelectedValue +
                                            ",'" + vArchivoHumedad +
                                            "','" + vExtensionHumedad + "'";
                        Int32 vInformacion8 = vConexion.ejecutarSql(vQuery8);
                    }
                    else if (vNombreHumedad == "" && RblHumedadSustancias.SelectedValue == "0")
                    {
                        vExtensionHumedad = "";
                        vArchivoHumedad ="";

                        String vQuery8 = "STEISP_AGENCIA_ModificarListaVerificacion 9," + Session["AG_LvPM_ID_MANTENIMIENTO_LV_MODIFICAR"] +
                                             "," + RblHumedadSustancias.SelectedValue +
                                             ",'" + vArchivoHumedad +
                                             "','" + vExtensionHumedad + "'";
                        Int32 vInformacion8 = vConexion.ejecutarSql(vQuery8);

                    }




                    //Imagen Robo
                    String vNombreRobo = String.Empty;
                    HttpPostedFile bufferDepositoRobo = FuRoboDaño.PostedFile;
                    byte[] vFileDepositoRobo = null;
                    String vExtensionRobo = String.Empty;
                    String vArchivoRobo = String.Empty;
                    vNombreRobo = FuRoboDaño.FileName;
                    if (vNombreRobo != "" && RblRoboDaño.SelectedValue=="1")
                    {
                        vNombreRobo = FuRoboDaño.FileName;
                        Stream vStream = bufferDepositoRobo.InputStream;
                        BinaryReader vReader = new BinaryReader(vStream);
                        vFileDepositoRobo = vReader.ReadBytes((int)vStream.Length);
                        vExtensionRobo = System.IO.Path.GetExtension(FuRoboDaño.FileName);
                        vArchivoRobo = Convert.ToBase64String(vFileDepositoRobo);
                        String vQuery9 = "STEISP_AGENCIA_ModificarListaVerificacion 10," + Session["AG_LvPM_ID_MANTENIMIENTO_LV_MODIFICAR"] +
                                            "," + RblRoboDaño.SelectedValue +
                                            ",'" + vArchivoRobo +
                                            "','" + vExtensionRobo + "'";
                        Int32 vInformacion9 = vConexion.ejecutarSql(vQuery9);

                    } else if (vNombreRobo == "" && RblRoboDaño.SelectedValue == "0") {
                        vExtensionRobo = "";
                        vArchivoRobo = "";
                        String vQuery9 = "STEISP_AGENCIA_ModificarListaVerificacion 10," + Session["AG_LvPM_ID_MANTENIMIENTO_LV_MODIFICAR"] +
                                            "," + RblRoboDaño.SelectedValue +
                                            ",'" + vArchivoRobo +
                                            "','" + vExtensionRobo + "'";
                        Int32 vInformacion9 = vConexion.ejecutarSql(vQuery9);
                    }



                    //Imagen Elementos Ajenos
                    String vNombreElementosAjenos = String.Empty;
                    HttpPostedFile bufferDepositoElementosAjenos = FuElementosAjenos.PostedFile;
                    byte[] vFileDepositoElementosAjenos = null;
                    String vExtensionElementosAjenos = String.Empty;
                    String vArchivoElementosAjenos = String.Empty;
                    vNombreElementosAjenos = FuElementosAjenos.FileName;

                    if (vNombreElementosAjenos != "" && RblElementosAjenos.SelectedValue=="1")
                    {
                        
                        Stream vStream = bufferDepositoElementosAjenos.InputStream;
                        BinaryReader vReader = new BinaryReader(vStream);
                        vFileDepositoElementosAjenos = vReader.ReadBytes((int)vStream.Length);
                        vExtensionElementosAjenos = System.IO.Path.GetExtension(FuElementosAjenos.FileName);
                        vArchivoElementosAjenos = Convert.ToBase64String(vFileDepositoElementosAjenos);

                        String vQuery10 = "STEISP_AGENCIA_ModificarListaVerificacion 11," + Session["AG_LvPM_ID_MANTENIMIENTO_LV_MODIFICAR"] +
                                            "," + RblElementosAjenos.SelectedValue +
                                            ",'" + vArchivoElementosAjenos +
                                            "','" + vExtensionElementosAjenos + "'";
                        Int32 vInformacion10 = vConexion.ejecutarSql(vQuery10);

                    }
                    else if (vNombreElementosAjenos == "" && RblElementosAjenos.SelectedValue == "0"){
                        vExtensionElementosAjenos = "";
                        vArchivoElementosAjenos = "";

                        String vQuery10 = "STEISP_AGENCIA_ModificarListaVerificacion 11," + Session["AG_LvPM_ID_MANTENIMIENTO_LV_MODIFICAR"] +
                                            "," + RblElementosAjenos.SelectedValue +
                                            ",'" + vArchivoElementosAjenos +
                                            "','" + vExtensionElementosAjenos + "'";
                        Int32 vInformacion10 = vConexion.ejecutarSql(vQuery10);
                    }


                  
                    //Imagen Entorno
                    String vNombreDepotEntorno = String.Empty;
                    HttpPostedFile bufferDepositoEntorno = FuEspacioFisico.PostedFile;
                    byte[] vFileDepositEntorno = null;
                    String vExtensionEntorno = String.Empty;
                    String vArchivoEntorno = String.Empty;
                    vNombreDepotEntorno = FuEspacioFisico.FileName;

                    if (vNombreDepotEntorno != "")
                    {
                       
                        Stream vStream = bufferDepositoEntorno.InputStream;
                        BinaryReader vReader = new BinaryReader(vStream);
                        vFileDepositEntorno = vReader.ReadBytes((int)vStream.Length);
                        vExtensionEntorno = System.IO.Path.GetExtension(FuEspacioFisico.FileName);
                        vArchivoEntorno = Convert.ToBase64String(vFileDepositEntorno);

                        String vQuery11 = "STEISP_AGENCIA_ModificarListaVerificacion 12," + Session["AG_LvPM_ID_MANTENIMIENTO_LV_MODIFICAR"] +
                                            ",'" + vArchivoEntorno +
                                            "','" + vExtensionEntorno + "'";
                        Int32 vInformacion11 = vConexion.ejecutarSql(vQuery11);

                    }
                    
                    ////DATOS GENERALES
                    String vQuery12 = "STEISP_AGENCIA_ModificarListaVerificacion 13,'" + Session["AG_LvPM_ID_MANTENIMIENTO_LV_MODIFICAR"] +
                                               "','" + TxHoraInicioMant.Text +
                                               "','" + TxHoraFinMant.Text +
                                               "','" + TxHoraSalidaINFA.Text +
                                               "','" + TxHoraLlegadaINFA.Text + "'";
                    Int32 vInformacion12 = vConexion.ejecutarSql(vQuery12);

                }
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }


            limpiar();
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModalEnvioLv();", true);
            Response.Redirect("/sites/agencias/pages/mantenimiento/lvPendientesCompletar.aspx");

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


            RBLManEquipoComu.SelectedIndex = -1;
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
        
        protected void BtnRegresarCompletarLV_Click(object sender, EventArgs e)
        {
            try
            {
             limpiar();
             Response.Redirect("/sites/agencias/pages/mantenimiento/lvPendientesAprobarJefes.aspx");
            }
            catch (Exception Ex) { Mensaje(Ex.Message, WarningType.Danger); }
        }

        //**********************************************************************************************************************//
        //*****************************************  MODIFICAR LISTA DE VERIFICACION  *****************************************//
        //*********************************************************************************************************************//

        void cargarDataModificar()
        {
            try
            {

                TituloPagina.Text = "Modificar Lista de Verificación";
                UpTitulo.Update();

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
                if (RBLManEquipoComuRespuesta.Equals("True"))
                {
                    RBLManEquipoComu.SelectedValue = "1";
                    ImgPreviewNoMantEquipoComu.Visible = true;
                    //TxResNoManEquipoComu.Text = "si";
                    TxResNoManEquipoComu1.Value = "si";
                }
                else{
                    RBLManEquipoComu.SelectedValue = "0";
                    ImgPreviewNoMantEquipoComu.Visible = false;
                    TxMotivoNoMantEquipoComu.Visible = true;
                }


                //SECCION PRUEBAS DE PC
                DataTable vDatos3 = new DataTable();
                vDatos3 = (DataTable)Session["AG_LvPM_DATOS_PRUEBAS_PC"];

                string RBProbaronEquipoRespuesta = vDatos3.Rows[0]["proboTodoEquipo"].ToString();
                TxMotivoNoProbaronEquipo.Text = vDatos3.Rows[0]["motivoNoProboTodoEquipo"].ToString();
                if (RBProbaronEquipoRespuesta.Equals("True"))
                {
                    RBProbaronEquipo.SelectedValue = "1";
                    TxMotivoNoProbaronEquipo.Visible = false;
                }
                else
                {
                    RBProbaronEquipo.SelectedValue = "0";
                    TxMotivoNoProbaronEquipo.Visible = true;
                }


                //SECCION EQUIPO DE COMUNICACION
                DataTable vDatos4 = new DataTable();
                vDatos4 = (DataTable)Session["AG_LvPM_DATOS_EQUIPO_COMUNICACION"];

                string RblClimatizacionAdecuadaRespuesta = vDatos4.Rows[0]["climatizacionAdecuada"].ToString();
                String vDocumentoClimatizacion = vDatos4.Rows[0]["fotoClimatizacionAdecuada"].ToString();
                string srcClimatizacion = "data:image;base64," + vDocumentoClimatizacion;
                ImgPreviewClimatizacion.Src = srcClimatizacion;

                if (RblClimatizacionAdecuadaRespuesta.Equals("True"))
                {
                    RblClimatizacionAdecuada.SelectedValue = "1";
                    ImgPreviewClimatizacion.Visible = true;
                    //TxClimatizacion.Text = "si";
                    TxClimatizacion1.Value = "si";

                }
                else
                {
                    RblClimatizacionAdecuada.SelectedValue = "0";
                    ImgPreviewClimatizacion.Visible = false;
                }


                string RblUPSRespuesta = vDatos4.Rows[0]["energiaElectricaUPS"].ToString();
                String vDocumentoUPS = vDatos4.Rows[0]["fotoEnergiaElectricaUPS"].ToString();
                string srcUPS = "data:image;base64," + vDocumentoUPS;
                ImgPreviewUPS.Src = srcUPS;

                if (RblUPSRespuesta.Equals("True"))
                {
                    RblUPS.SelectedValue = "1";
                    ImgPreviewUPS.Visible = true;
                    //TxUPS.Text = "si";
                    TxUPS1.Value = "si";
                }
                else
                {
                    RblUPS.SelectedValue = "0";
                    ImgPreviewUPS.Visible = false;
                }

                //SECCION ENTORNO COMUNICACION
                DataTable vDatos5 = new DataTable();
                vDatos5 = (DataTable)Session["AG_LvPM_DATOS_ENTORNO_COMUNICACION"];

                string RbPolvoSuciedadRespuesta = vDatos5.Rows[0]["expuestoPolvoSuciedad"].ToString();
                String vDocumentoPolvoSuciedad = vDatos5.Rows[0]["fotoExpuestoPolvoSuciedad"].ToString();
                string srcPolvoSuciedad = "data:image;base64," + vDocumentoPolvoSuciedad;
                ImgPreviewPolvoSuciedad.Src = srcPolvoSuciedad;

                if (RbPolvoSuciedadRespuesta.Equals("True"))
                {
                    RbPolvoSuciedad.SelectedValue = "1";
                    ImgPreviewPolvoSuciedad.Visible = true;
                    //TxPolvoSuciedad.Text = "si";
                    TxPolvoSuciedad1.Value = "si";
                }
                else
                {
                    RbPolvoSuciedad.SelectedValue = "0";
                    ImgPreviewPolvoSuciedad.Visible = false;
                }


                string RblHumedadSustanciasRespuesta = vDatos5.Rows[0]["rastrosHumedadSustancias"].ToString();
                String vDocumentoHumedadSustancias = vDatos5.Rows[0]["fotoRastrosHumedadSustancias"].ToString();
                string srcHumedadSustancias = "data:image;base64," + vDocumentoHumedadSustancias;
                ImgPreviewHumedadSustancias.Src = srcHumedadSustancias;

                if (RblHumedadSustanciasRespuesta.Equals("True"))
                {
                    RblHumedadSustancias.SelectedValue = "1";
                    ImgPreviewHumedadSustancias.Visible = true;
                    //TxHumedadSustancias.Text = "si";
                    TxHumedadSustancias1.Value= "si";
                }
                else
                {
                    RblHumedadSustancias.SelectedValue = "0";
                    ImgPreviewHumedadSustancias.Visible = false;
                }


                string RblRoboDañoRespuesta = vDatos5.Rows[0]["expuestoRoboDaño"].ToString();
                String vDocumentoRoboDaño = vDatos5.Rows[0]["fotoExpuestoRoboDaño"].ToString();
                string srcRoboDaño = "data:image;base64," + vDocumentoRoboDaño;
                ImgPreviewRoboDaño.Src = srcRoboDaño;

                if (RblRoboDañoRespuesta.Equals("True"))
                {
                    RblRoboDaño.SelectedValue = "1";
                    ImgPreviewRoboDaño.Visible = true;
                    //TxRoboDaño.Text = "si";
                    TxRoboDaño1.Value = "si";
                }
                else
                {
                    RblRoboDaño.SelectedValue = "0";
                    ImgPreviewRoboDaño.Visible = false;
                }


                string RblElementosAjenosRespuesta = vDatos5.Rows[0]["encontroElementosExtraños"].ToString();
                String vDocumentoElementosAjenos = vDatos5.Rows[0]["fotoElementosExtraños"].ToString();
                string srcElementosAjenos = "data:image;base64," + vDocumentoElementosAjenos;
                ImgPreviewElementosAjenos.Src = srcElementosAjenos;

                if (RblElementosAjenosRespuesta.Equals("True"))
                {
                    RblElementosAjenos.SelectedValue = "1";
                    ImgPreviewElementosAjenos.Visible = true;
                    //TxElementosAjenos.Text = "si";
                    TxElementosAjenos1.Value = "si";
                }
                else
                {
                    RblElementosAjenos.SelectedValue = "0";
                    ImgPreviewElementosAjenos.Visible = false;
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






    }




}