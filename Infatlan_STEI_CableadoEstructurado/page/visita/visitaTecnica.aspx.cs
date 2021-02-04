using Infatlan_STEI_CableadoEstructurado.clases;
using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Infatlan_STEI_CableadoEstructurado.paginas
{
    public partial class estudioEstructurado : System.Web.UI.Page
    {
        db vConexion = new db();
        Security vSecurity = new Security();
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            navAprobacion.Visible = false;

            if (!Page.IsPostBack)
            {
                if (Convert.ToBoolean(Session["AUTH"]))
                {
                    if (vSecurity.ObtenerPermiso(Session["USUARIO"].ToString(), 4).Edicion)
                        btnGuardar.Visible = true;

                    HFCuartoTelecomunicaciones.Value = string.Empty;
                    HFReubicar.Value = string.Empty;
                    HFDesordenado.Value = string.Empty;
                    HFExpuestoHumedo.Value = string.Empty;
                    HFExpuestoRobo.Value = string.Empty;
                    HFEquiposAjeno.Value = string.Empty;
                    HFUPS.Value = string.Empty;
                    HFAire.Value = string.Empty;
                    HFPlano.Value = string.Empty;
                    EliminarSesiones();
                    CargarDatos();

                    string vPestaña = Request.QueryString["a"];
                    string vEdicion = Request.QueryString["c"];
                    Label11.Text = vEdicion == null ? "Nuevo" : "Editar";
                    Label12.Text = "Visita Técnica";

                    if (Convert.ToInt32(vPestaña) == 2)
                    {
                        Label12.Text = "Revisión";
                        Label11.Text = "Estudio";
                        LbDescripcionVisita.Text = "Revisión de la visita técnica.";
                        lbIngresarPlano.Text = "";
                        navAprobacion.Visible = true;
                        bodyMateriales.Visible = false;
                        //LbTituloMaterial.Text = "Materiales Agregados";
                        LbDescrpcionMaterial.Text = "Materiales agregados para realizar estudio.";
                        OcultarCampos();
                        ObtenerDatos();

                        GVMateriales.Columns[4].Visible = false;
                    }

                    if (Convert.ToInt32(vEdicion) == 3)
                    {
                        //LbTituloMaterial.Text = "Materiales Agregados";
                        LbDescripcionVisita.Text = "Modificación de la visita técnica.";
                        navAprobacion.Visible = true;
                        ObtenerDatos();
                    }
                }
                else
                {
                    Response.Redirect("/login.aspx");
                }
            }
        }

        void CargarDatos()
        {

            if (HttpContext.Current.Session["CE_CABLEADO"] == null)
            {
                //Obtener Materiales
                try
                {
                    String vQuery = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 6, " + ddlMateriales.SelectedValue;
                    DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                    DataTable vData = (DataTable)Session["CE_CABLEADO"];

                    if (vData == null)
                        vData = vDatos.Clone();

                    if (vDatos != null)
                        vData.Rows.Add(vDatos.Rows[0]["idStock"].ToString(), vDatos.Rows[0]["nombre"].ToString(), vDatos.Rows[0]["cantidad"].ToString(), vDatos.Rows[0]["medidas"].ToString() + " " + vDatos.Rows[0]["unidades"].ToString());


                    GVMateriales.DataSource = vData;
                    GVMateriales.DataBind();
                    Session["CE_CABLEADO"] = vData;

                }
                catch (Exception Ex)
                {
                    Mensaje(Ex.Message, WarningType.Danger);
                }


                //Obtener todos las Agencias.
                try
                {
                    String vQuery = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 19";
                    DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                    ddlAgencia.Items.Clear();
                    ddlAgencia.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        ddlAgencia.Items.Add(new ListItem { Value = item["idUbicacion"].ToString(), Text = item["nombre"].ToString() });
                    }

                }
                catch (Exception Ex)
                {
                    Mensaje(Ex.Message, WarningType.Danger);
                }

                //Obtener todos los Responsables.
                try
                {
                    String vQuery = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 2";

                    DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                    //ddlResponsable.Items.Clear();
                    ddlResponsable.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        ddlResponsable.Items.Add(new ListItem { Value = item["idUsuario"].ToString(), Text = item["nombre"].ToString() + " " + item["apellidos"].ToString() });
                    }

                }
                catch (Exception Ex)
                {
                    Mensaje(Ex.Message, WarningType.Danger);
                }


                //Obtener Unidades
                try
                {
                    String vQuery = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 8";

                    DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                    //ddlResponsable.Items.Clear();
                    ddlMedidas.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        ddlMedidas.Items.Add(new ListItem { Value = item["idMedida"].ToString(), Text = item["medida"].ToString() });
                    }

                }
                catch (Exception Ex)
                {
                    Mensaje(Ex.Message, WarningType.Danger);
                }


                //Obtener materiales
                try
                {
                    String vQuery = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 7";
                    DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                    if (vDatos.Rows.Count > 0)
                    {
                        ddlMateriales.Items.Clear();
                        ddlMateriales.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción " });
                        foreach (DataRow item in vDatos.Rows)
                        {
                            ddlMateriales.Items.Add(new ListItem { Value = item["idStock"].ToString(), Text = item["material"].ToString() + " - " + item["modelo"].ToString() + " - " + item["marca"].ToString() });
                        }
                    }

                    //ddlMateriales.Items.Add(new ListItem { Value = item["idStock"].ToString(), Text = item["nombre"].ToString() + " - " + item["modelo"].ToString() + " (" + item["cantidad"]ToString() + ")" });

                }
                catch (Exception Ex)
                {
                    Mensaje(Ex.Message, WarningType.Danger);
                }

                //Obtener Áreas.
                try
                {
                    String vQuery = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 26";
                    DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                    ddlArea.Items.Clear();
                    ddlArea.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        ddlArea.Items.Add(new ListItem { Value = item["idAreaAgencia"].ToString(), Text = item["nombre"].ToString() });
                    }

                }
                catch (Exception Ex)
                {
                    Mensaje(Ex.Message, WarningType.Danger);
                }

                Session["CE_CABLEADO"] = "1";
            }
        }

        public void OcultarCampos()
        {
            //Datos Generales
            ddlResponsable.Enabled = false;
            ddlAgencia.Enabled = false;
            ddlArea.Enabled = false;
            txtFechaEstudio.ReadOnly = true;
            txtFechaEnvio.ReadOnly = true;

            //Estudio
            fuCuartoTelecomunicaciones.Enabled = false;
            rblEtiquetado.Enabled = false;
            rblReubicar.Enabled = false;
            fuReubicar.Enabled = false;
            rblDesordenado.Enabled = false;
            fuDesordenado.Enabled = false;
            rblExpuestoHumedo.Enabled = false;
            fuExpuestoHumedo.Enabled = false;
            rblExpuestoRobo.Enabled = false;
            fuExpuestoRobo.Enabled = false;
            rblElementoAjenos.Enabled = false;
            fuElemetoAjenos.Enabled = false;
            rblUps.Enabled = false;
            fuUPS.Enabled = false;
            rblAire.Enabled = false;
            fuAire.Enabled = false;
            txtCategoria.ReadOnly = true;
            rblNuevoRemodelacion.Enabled = false;
            fuPlano.Enabled = false;
            //rblRotulacion.Enabled = false;

            //Materiales
            ddlMateriales.Visible = false;
            txtCantidad.Visible = false;
            ddlMedidas.Visible = false;
            btnAgregar.Visible = false;

            lbCantidadMaterial.Visible = false;
            LBddlMaterial.Visible = false;
            LbunidadMaterial.Visible = false;
            LbTituloMaterialesSoli.Visible = false;


            //Estimacion
            txtHorasTrabajo.ReadOnly = true;
            txtParticipantes.ReadOnly = true;
            txtObservaciones.ReadOnly = true;
            rblTransporte.Enabled = false;
            rblALimentación.Enabled = false;
            btnGuardar.Visible = false;

            //Habilitar Form Imagenes
            // Div1.Visible = true;

            imgDesordenado.Visible = true;
            imgElementoAjeno.Visible = true;
            imgExpuestoHumedo.Visible = true;
            imgExpuestoRobo.Visible = true;
            imgReubicar.Visible = true;

            txtFechaEstudio.TextMode = TextBoxMode.MultiLine;
            txtFechaEnvio.TextMode = TextBoxMode.MultiLine;

            //ddlMedidas.Visible = false;
            //ddlMateriales.Visible = false;
            //txtCantidad.Visible = true;
            //btnAgregar.Visible = true;
        }

        public void ObtenerDatos()
        {

            //Obtener Estudio
            try
            {
                String vLectura = Request.QueryString["ia"];
                String vEdicion = Request.QueryString["e"];
                string vidEstudio = "";

                if (vLectura != null)
                {
                    vidEstudio = vLectura;

                }
                else if (vEdicion != null)
                {
                    vidEstudio = vEdicion;
                    txtFechaEstudio.TextMode = TextBoxMode.MultiLine;
                    txtFechaEnvio.TextMode = TextBoxMode.MultiLine;
                    navAprobacion.Visible = false;
                }


                String vQuery = "STEISP_CABLESTRUCTURADO_Aprobacion 1," + vidEstudio;
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                ///DataTable vData = (DataTable)Session["CE_CABLEADO"];

                //Datos Generales
                ddlResponsable.Items.FindByText(vDatos.Rows[0]["responsable"].ToString()).Selected = true;
                txtIdentidad.Text = vDatos.Rows[0]["identidad"].ToString();
                txtZona.Text = vDatos.Rows[0]["zona"].ToString();
                ddlAgencia.Items.FindByText(vDatos.Rows[0]["agencia"].ToString()).Selected = true;
                txtDireccion.Text = vDatos.Rows[0]["direccion"].ToString();
                ddlArea.Items.FindByText(vDatos.Rows[0]["area"].ToString()).Selected = true;

                DateTime vFechaEs = Convert.ToDateTime(vDatos.Rows[0]["fechaEstudio"].ToString());
                DateTime vFechaEn = Convert.ToDateTime(vDatos.Rows[0]["fechaEnvio"].ToString());

                string vEstudio = vFechaEs.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
                string vEnvio = vFechaEn.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);

                txtFechaEstudio.Text = vEstudio;
                txtFechaEnvio.Text = vEstudio;

                //Estimacion de Recursos
                txtHorasTrabajo.Text = vDatos.Rows[0]["duracionTrabajo"].ToString();
                txtParticipantes.Text = vDatos.Rows[0]["numeroParticipantes"].ToString();
                txtObservaciones.Text = vDatos.Rows[0]["observacionesEstimacion"].ToString();
                rblTransporte.SelectedValue = vDatos.Rows[0]["usaTransporte"].ToString();
                rblALimentación.SelectedValue = vDatos.Rows[0]["usaAlimentacion"].ToString();
                rblNuevoRemodelacion.SelectedValue = vDatos.Rows[0]["nuevoRemodelacion"].ToString();

                if (vDatos.Rows[0]["nuevoRemodelacion"].ToString() == "nuevo")
                {

                    rblDesordenado.Visible = false;
                    fuDesordenado.Visible = false;
                    imgDesordenado.Visible = false;

                    lbEtiquetadoAs.Visible = false;
                    lbEtiquetado.Visible = false;
                    rblEtiquetado.Visible = false;

                    lbReubicarAs.Visible = false;
                    lbReubicar.Visible = false;
                    rblReubicar.Visible = false;
                    fuReubicar.Visible = false;
                    imgReubicar.Visible = false;

                    udpVisible.Visible = false;
                    udpEtiquetado.Visible = false;
                    udpCategoria.Visible = false;
                    udpOcultarEstudioPrevio.Visible = false;

                    //udpVisible.Update();
                    //udpNuevoRemodelacion.Update();
                    //udpReubicar.Update();
                }


                //Estudio
                String vQueryPreguntas = "STEISP_CABLESTRUCTURADO_Aprobacion 2," + vidEstudio;
                DataTable vDatosPreguntas = vConexion.obtenerDataTable(vQueryPreguntas);

                //Cuarto
                String vFUTelecomunicaciones = vDatosPreguntas.Rows[0]["imagenCuarto"].ToString();
                string srcTelecumicaciones = "data:image;base64," + vFUTelecomunicaciones;
                imgCuartoTelecomunicaciones.Src = srcTelecumicaciones;
                imgCuartoTelecomunicaciones.Visible = true;
                fuCuartoTelecomunicaciones.Visible = true;

                //Etiquetado
                rblEtiquetado.SelectedValue = vDatosPreguntas.Rows[0]["cableEtiquetado"].ToString();

                //Reubicar
                String vFUReubicar = vDatosPreguntas.Rows[0]["imagenReubicar"].ToString();
                string srcReubicar = "data:image;base64," + vFUReubicar;
                imgReubicar.Src = srcReubicar;
                imgReubicar.Visible = true;
                fuReubicar.Visible = true;

                rblReubicar.SelectedValue = vDatosPreguntas.Rows[0]["cableReubicar"].ToString();

                // Desordenado
                String vFUDesordenado = vDatosPreguntas.Rows[0]["imagenDesordenado"].ToString();
                string srcDesordenado = "data:image;base64," + vFUDesordenado;
                imgDesordenado.Src = srcDesordenado;
                imgDesordenado.Visible = true;
                fuDesordenado.Visible = true;

                rblDesordenado.SelectedValue = vDatosPreguntas.Rows[0]["cableDesordenado"].ToString();

                //Expuesto Humedad
                String vFUHumedad = vDatosPreguntas.Rows[0]["imagenExpHumedad"].ToString();
                string srcHumedad = "data:image;base64," + vFUHumedad;
                imgExpuestoHumedo.Src = srcHumedad;
                imgExpuestoHumedo.Visible = true;
                fuExpuestoHumedo.Visible = true;

                rblExpuestoHumedo.SelectedValue = vDatosPreguntas.Rows[0]["cableExpHumedad"].ToString();

                //Expuesto Robo
                String vFURobo = vDatosPreguntas.Rows[0]["imagenExpRobo"].ToString();
                string srcRobo = "data:image;base64," + vFURobo;
                imgExpuestoRobo.Src = srcRobo;
                imgExpuestoRobo.Visible = true;
                fuExpuestoRobo.Visible = true;

                rblExpuestoRobo.SelectedValue = vDatosPreguntas.Rows[0]["cableExpRobo"].ToString();

                //Elemento Ajeno
                String vFUAjeno = vDatosPreguntas.Rows[0]["imagenEquipoAjenos"].ToString();
                string srcAjeno = "data:image;base64," + vFUAjeno;
                imgElementoAjeno.Src = srcAjeno;
                imgElementoAjeno.Visible = true;
                fuElemetoAjenos.Visible = true;

                rblElementoAjenos.SelectedValue = vDatosPreguntas.Rows[0]["EquiposAjenos"].ToString();


                //UPS
                String vFUUps = vDatosPreguntas.Rows[0]["imagenUps"].ToString();
                string srcUps = "data:image;base64," + vFUUps;
                imgUPS.Src = srcUps;
                imgUPS.Visible = true;
                fuUPS.Visible = true;

                rblUps.SelectedValue = vDatosPreguntas.Rows[0]["cuentaUps"].ToString();

                //UPS
                String vFUAire = vDatosPreguntas.Rows[0]["imagenAireaAcondicionado"].ToString();
                string srcAire = "data:image;base64," + vFUAire;
                imgAire.Src = srcAire;
                imgAire.Visible = true;
                fuAire.Visible = true;

                rblAire.SelectedValue = vDatosPreguntas.Rows[0]["cuentaAire"].ToString();


                txtCategoria.Text = vDatosPreguntas.Rows[0]["cuentaCategoria"].ToString();

                //rblRotulacion.SelectedValue = vDatosPreguntas.Rows[0]["cuentaRotulacion"].ToString();

                //Plano
                String vFUPlano = vDatosPreguntas.Rows[0]["PDFPlano"].ToString();
                string srcPlano = "data:application/pdf;base64," + vFUPlano;
                IFramePDF.Src = srcPlano;
                IFramePDF.Visible = true;
                fuPlano.Visible = true;

                //Materiales

                String vQueryMaterial = "STEISP_CABLESTRUCTURADO_Aprobacion 3," + vidEstudio;
                DataTable vDatosMaterial = vConexion.obtenerDataTable(vQueryMaterial);
                GVMateriales.DataSource = vDatosMaterial;
                GVMateriales.DataBind();
                Session["CE_MATERIALES"] = vDatosMaterial;


                if (vDatos.Rows[0]["nuevoRemodelacion"].ToString() == "remodelacion")
                {
                    if (vDatosPreguntas.Rows[0]["cableExpHumedad"].ToString() == "no")
                    {
                        fuExpuestoHumedo.Visible = false;
                        imgExpuestoHumedo.Visible = false;
                        //udpExpuestoHumedo.Update();
                    }

                    if (vDatosPreguntas.Rows[0]["cableExpRobo"].ToString() == "no")
                    {
                        fuExpuestoRobo.Visible = false;
                        imgExpuestoRobo.Visible = false;
                        //udpExpuestoRobo.Update();
                    }

                    if (vDatosPreguntas.Rows[0]["EquiposAjenos"].ToString() == "no")
                    {
                        fuElemetoAjenos.Visible = false;
                        imgElementoAjeno.Visible = false;
                        //udpExpuestoAjenos.Update();
                    }

                    if (vDatosPreguntas.Rows[0]["cableReubicar"].ToString() == "no")
                    {
                        fuReubicar.Visible = false;
                        imgReubicar.Visible = false;
                        //udpReubicar.Update();
                    }

                    if (vDatosPreguntas.Rows[0]["cuentaAire"].ToString() == "no")
                    {
                        fuAire.Visible = false;
                        imgAire.Visible = false;
                        //udpAireAcondicionado.Update();
                    }

                    if (vDatosPreguntas.Rows[0]["cuentaUps"].ToString() == "no")
                    {
                        fuUPS.Visible = false;
                        imgUPS.Visible = false;
                        //udpUPS.Update();
                    }

                }
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }

        }

        public void OcultarDatosEstudioPrevio()
        {
            lbEtiquetadoAs.Visible = false;
            lbReubicarAs.Visible = false;
            lbEtiquetado.Visible = false;
            lbReubicar.Visible = false;
            rblReubicar.Visible = false;
            rblEtiquetado.Visible = false;
            fuReubicar.Visible = false;
            imgReubicar.Visible = false;
            udpVisible.Visible = false;
            udpEtiquetado.Visible = false;
            udpCategoria.Visible = false;
            udpDesordenado.Visible = false;

            udpVisible.Update();
            udpNuevoRemodelacion.Update();
        }

        public void Limpiar()
        {
            ddlResponsable.SelectedIndex = -1;
            txtIdentidad.Text = "";
            txtZona.Text = "";
            //udpResposable.Update();

            ddlAgencia.SelectedIndex = 0;
            ddlArea.SelectedIndex = 0;
            txtDireccion.Text = "";
            udpAgencia.Update();



            txtFechaEstudio.Text = "";
            txtFechaEnvio.Text = "";
            udpFechasEstudio.Update();

            rblNuevoRemodelacion.ClearSelection();

            //imgCuartoTelecomunicaciones.Src = string.Empty;
            fuCuartoTelecomunicaciones.Attributes.Clear();
            //udpCuartoTelecomunicaciones.Update();

            //udpTodoCuartoTelecomunicaciones.Update();

            rblReubicar.ClearSelection();

            //udpReubicar.Update();

            //udpEtiquetado.Update();
            rblEtiquetado.ClearSelection();
            //rblEtiquetado.ClearSelection();

            //udpImgReubicar.Update();

            udpDesordenado.Update();
            rblDesordenado.ClearSelection();

            rblExpuestoHumedo.ClearSelection();
            //udpExpuestpHumedo.Update();

            rblExpuestoRobo.ClearSelection();
            //udpExpuestoRobo.Update();

            rblElementoAjenos.ClearSelection();
            //udpElemetoAjeno.Update();

            //udpUps.Update();
            rblUps.ClearSelection();

            // udpAire.Update();
            rblAire.ClearSelection();

            // udpCategoria.Update();


            txtCategoria.Text = "";

            ////udpRotulacion.Update();
            //rblRotulacion.ClearSelection();

            ddlMateriales.SelectedIndex = -1;
            udpMetariales.Update();

            udpCantidad.Update();
            txtCantidad.Text = "";

            ddlMedidas.SelectedIndex = -1;
            udpUnidades.Update();

            GVMateriales.DataBind();
            GVMateriales.DataSource = null;
            UpdateDivMateriales.Update();

            txtHorasTrabajo.Text = "";
            udpDuracion.Update();

            txtParticipantes.Text = "";
            udpParticipantes.Update();

            rblTransporte.ClearSelection();
            udpTransporte.Update();

            updAlimentacion.Update();
            rblALimentación.ClearSelection();

            udpObservaciones.Update();
            txtObservaciones.Text = "";


        }

        //DROPDOWNLIST
        protected void ddlResponsable_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String vQuery = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 3, '" + ddlResponsable.SelectedValue + "'";

                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                Session["CE_IDRESPONSABLE"] = vDatos.Rows[0]["idUsuario"].ToString();
                txtIdentidad.Text = vDatos.Rows[0]["identidad"].ToString();
                txtZona.Text = vDatos.Rows[0]["nombre"].ToString();
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        protected void ddlAgencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String vQuery = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 1, '" + ddlAgencia.SelectedValue + "'";

                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                Session["CE_IDUBICACION"] = vDatos.Rows[0]["idUbicacion"].ToString();
                Session["CE_IDEPARTAMENTO"] = vDatos.Rows[0]["idDepartamento"].ToString();

                txtDireccion.Text = vDatos.Rows[0]["direccion"].ToString();

                String vQueryUbicacion = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 21, '" + ddlAgencia.SelectedValue + "'";

                DataTable vDatosUbicacion = vConexion.obtenerDataTable(vQueryUbicacion);
                Session["CE_NOMBREESTUDIO"] = vDatosUbicacion.Rows[0]["codigo"].ToString();

            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }

        }

        protected void ddlMateriales_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String vQuery = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 4, '" + ddlMateriales.SelectedValue + "'";

                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                Session["CE_IDMATERIAL"] = vDatos.Rows[0]["idMaterial"].ToString();

            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }

        }

        protected void ddlMedidas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String vQuery = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 9, '" + ddlMedidas.SelectedValue + "'";

                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                Session["CE_IDMEDIDA"] = vDatos.Rows[0]["idMedida"].ToString();

            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        //GRIDVIEW 
        protected void GVMateriales_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GVMateriales.DataSource = (DataTable)Session["CE_MATERIAL"];
                GVMateriales.PageIndex = e.NewPageIndex;
                GVMateriales.DataBind();
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        protected void GVMateriales_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DataTable vDatos = new DataTable();

                if (e.CommandName == "Eliminar")
                {
                    String vMaterial = e.CommandArgument.ToString();
                    //String vLectura = Request.QueryString["ia"];
                    //String vEdicion = Request.QueryString["e"];
                    //string vid = "";

                    //if (vLectura != null || vEdicion != null)
                    //{

                    //    if (vLectura != null)
                    //    {
                    //        vid = vLectura;
                    //    }
                    //    else if (vEdicion != null)
                    //    {
                    //        vid = vEdicion;
                    //    }

                    //    String vQueryMaterial = "STEISP_CABLESTRUCTURADO_Aprobacion 3," + vid;
                    //    vDatos = vConexion.obtenerDataTable(vQueryMaterial);

                    //    DataRow[] result = vDatos.Select("numero = '" + vMaterial + "'");

                    //    foreach (DataRow row in result)
                    //    {
                    //        if (row["numero"].ToString().Contains(vMaterial))
                    //            vDatos.Rows.Remove(row);
                    //    }

                    //}
                    //else
                    //{
                    vDatos = (DataTable)Session["CE_MATERIALES"];
                    if (GVMateriales.Rows.Count > 0)
                    {
                        DataRow[] result = vDatos.Select("numero = '" + vMaterial + "'");

                        foreach (DataRow row in result)
                        {
                            if (row["numero"].ToString().Contains(vMaterial))
                                vDatos.Rows.Remove(row);
                        }
                    }
                    //}
                }
                GVMateriales.DataSource = vDatos;
                GVMateriales.DataBind();
                Session["CE_MATERIALES"] = vDatos;

            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        //RADIOBUTTONLIST
        protected void rblDesordenado_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (rblDesordenado.SelectedValue == "si")
            //    {
            //        fuDesordenado.Visible = true;
            //        imgDesordenado.Visible = true;
            //        //udpDesordenado.Update();
            //    }
            //    else
            //    {
            //        fuDesordenado.Visible = false;
            //        imgDesordenado.Visible = false;
            //        //udpDesordenado.Update();
            //    }
            //}
            //catch (Exception Ex)
            //{
            //    Mensaje(Ex.Message, WarningType.Danger);
            //}
        }

        protected void rblExpuestoHumedad_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (rblExpuestoHumedo.SelectedValue == "si")
                {

                    fuExpuestoHumedo.Visible = true;
                    imgExpuestoHumedo.Visible = true;
                }
                else
                {
                    fuExpuestoHumedo.Visible = false;
                    imgExpuestoHumedo.Visible = false;
                }


            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }

        }

        protected void rblExpuestoRobo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (rblExpuestoRobo.SelectedValue == "si")
                {
                    fuExpuestoRobo.Visible = true;
                    imgExpuestoRobo.Visible = true;

                }
                else
                {
                    fuExpuestoRobo.Visible = false;
                    imgExpuestoRobo.Visible = false;
                }
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }


        }

        protected void rblElementosAjenos_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (rblElementoAjenos.SelectedValue == "si")
                {
                    fuElemetoAjenos.Visible = true;
                    imgElementoAjeno.Visible = true;
                }
                else
                {
                    fuElemetoAjenos.Visible = false;
                    imgElementoAjeno.Visible = false;
                }
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        protected void rblReubicar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (rblReubicar.SelectedValue == "si")
                {

                    fuReubicar.Visible = true;
                    imgReubicar.Visible = true;

                }
                else
                {
                    fuReubicar.Visible = false;
                    imgReubicar.Visible = false;

                }

            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }

        }

        protected void rblAire_TextChanged(object sender, EventArgs e)
        {

            try
            {
                if (rblAire.SelectedValue == "si")
                {
                    fuAire.Visible = true;
                    imgAire.Visible = true;
                }
                else
                {
                    fuAire.Visible = false;
                    imgAire.Visible = false;
                }
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }

        }

        protected void rblUps_TextChanged(object sender, EventArgs e)
        {

            try
            {
                if (rblUps.SelectedValue == "si")
                {
                    fuUPS.Visible = true;
                    imgUPS.Visible = true;
                }
                else
                {
                    fuUPS.Visible = false;
                    imgUPS.Visible = false;
                }
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }

        }

        protected void rblAprobada_TextChanged(object sender, EventArgs e)
        {

            try
            {
                //if (rblAprobada.SelectedValue == "no")
                //{
                //    vEdicion = 3;
                //}

            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }

        }

        protected void rblNuevoRemodelacion_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (rblNuevoRemodelacion.SelectedValue == "nuevo")
                {
                    OcultarDatosEstudioPrevio();
                    udpOcultarEstudioPrevio.Update();

                }
                else if (rblNuevoRemodelacion.SelectedValue == "remodelacion")
                {
                    lbEtiquetadoAs.Visible = true;
                    lbReubicarAs.Visible = true;
                    udpOcultarEstudioPrevio.Update();
                    lbEtiquetado.Visible = true;
                    lbReubicar.Visible = true;
                    rblReubicar.Visible = true;
                    rblEtiquetado.Visible = true;
                    fuReubicar.Visible = true;
                    imgReubicar.Visible = true;
                    udpVisible.Visible = true;
                    udpEtiquetado.Visible = true;
                    udpCategoria.Visible = true;
                    udpDesordenado.Visible = true;
                    udpVisible.Update();
                    udpNuevoRemodelacion.Update();

                    //lbEtiquetadoAs.Visible = true;
                    //lbReubicarAs.Visible = true;
                    //lbEtiquetado.Visible = true;
                    //lbReubicar.Visible = true;
                    //rblReubicar.Visible = true;
                    //rblEtiquetado.Visible = true;
                    //fuReubicar.Visible = true;
                    //imgReubicar.Visible = true;
                    //udpVisible.Visible = true;
                    //udpEtiquetado.Visible = true;
                    //udpCategoria.Visible = true;
                    //udpDesordenado.Visible = true;

                    //udpVisible.Update();
                    //udpNuevoRemodelacion.Update();

                    //fuCuartoTelecomunicaciones.Enabled = true;
                    //rblEtiquetado.Enabled = true;
                    //rblReubicar.Enabled = true;
                    //fuReubicar.Enabled = true;
                    //rblDesordenado.Enabled = true;
                    //fuDesordenado.Enabled = true;
                    //rblExpuestoHumedo.Enabled = true;
                    //fuExpuestoHumedo.Enabled = true;
                    //rblExpuestoRobo.Enabled = true;
                    //fuExpuestoRobo.Enabled = true;
                    //rblElementoAjenos.Enabled = true;
                    //fuElemetoAjenos.Enabled = true;
                    //rblUps.Enabled = true;
                    //fuUPS.Enabled = true;
                    //rblAire.Enabled = true;
                    //fuAire.Enabled = true;
                    //txtCategoria.ReadOnly = false;
                    //rblNuevoRemodelacion.Enabled = true;
                    //fuPlano.Enabled = true;
                }
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        //VALIDACIONES
        void ValidarFormDatosGenerales()
        {
            try
            {
                Session["CE_ERRORGENERALES"] = 0;

                if (ddlResponsable.SelectedIndex == 0)
                {
                    throw new Exception("Favor seleccionar responsable");
                }

                if (ddlAgencia.SelectedIndex == 0)
                {
                    throw new Exception("Favor seleccionar agencia");
                }

                if (ddlArea.SelectedIndex == 0)
                {
                    throw new Exception("Favor seleccionar área");
                }

                if (txtFechaEstudio.Text == "" || txtFechaEstudio.Text == string.Empty)
                {
                    throw new Exception("Favor ingresar fecha de estudio");
                }

                if (txtFechaEnvio.Text == "" || txtFechaEnvio.Text == string.Empty)
                {
                    throw new Exception("Favor ingresar fecha de envío");
                }

                if (rblNuevoRemodelacion.SelectedValue == "")
                {
                    throw new Exception("Favor seleccionar tipo de estudio");
                }
            }
            catch (Exception Ex)
            {
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Pop", "window.alert('" + Ex.Message + "')", true);
                Mensaje(Ex.Message, WarningType.Danger);
                Session["CE_ERRORGENERALES"] = 1;
            }
        }

        void ValidarFormEstudioPrevio()
        {
            try
            {

                Session["CE_ERRORESTUDIOPREVIO"] = 0;

                string vEdicion = Request.QueryString["c"];

                if (vEdicion != Convert.ToString(3))
                {
                    //cuarto telecomunicaciones
                    if (!fuCuartoTelecomunicaciones.HasFile)
                    {
                        throw new Exception("Favor ingresar imagen del estado del cuarto de telecomunicaciones");
                    }
                    //Plano

                    if (!fuPlano.HasFile)
                    {
                        throw new Exception("Favor ingresar plano del área o agencia a trabajar");
                    }

                    if (rblNuevoRemodelacion.SelectedValue == "remodelacion")
                    {

                        //Desrodenado
                        if (rblDesordenado.SelectedValue.Equals(""))
                        {
                            throw new Exception("Favor llenar los datos de la pregunta: ¿El cableado se encuentra desordenado?");

                        }
                        if (rblDesordenado.SelectedValue == "si" | rblDesordenado.SelectedValue == "no")
                        {
                            if (!fuDesordenado.HasFile)
                            {
                                throw new Exception("Favor ingresar imagen del cable desordenado");
                            }
                        }

                        //Re-Ubicar
                        if (rblReubicar.SelectedValue == "")
                        {
                            throw new Exception("Favor llenar los datos de la pregunta: ¿Es necesario re-ubicar el equipo de telecomunicaciones?");
                        }
                        if (rblReubicar.SelectedValue == "si")
                        {
                            if (!fuReubicar.HasFile)
                            {
                                throw new Exception("Favor ingresar imagen de re-ubicar equipo de telecomunicaciones ");
                            }
                        }

                        //Expuesto Humedad
                        if (rblExpuestoHumedo.SelectedValue == "")
                        {
                            throw new Exception("Favor llenar los datos de la pregunta: ¿El equipo se encuentra expuesto a humedad o polvo?");
                        }
                        if (rblExpuestoHumedo.SelectedValue == "si")
                        {
                            if (!fuExpuestoHumedo.HasFile)
                            {
                                throw new Exception("Favor ingresar imagen si el equipo se encuentra expuesto a humedad o polvo");
                            }

                        }

                        //Expuesto Robo
                        if (rblExpuestoRobo.SelectedValue == "")
                        {
                            throw new Exception("Favor llenar los datos de la pregunta: ¿El equipo se encuentra expuesto a robo o daño?");
                        }
                        if (rblExpuestoRobo.SelectedValue == "si")
                        {
                            if (!fuExpuestoRobo.HasFile)
                            {
                                throw new Exception("Favor ingresar imagen si el equipo se encuentra expuesto a robo o daño");
                            }
                        }

                        //Equipos Ajenos 
                        if (rblElementoAjenos.SelectedValue == "")
                        {
                            throw new Exception("Favor llenar los datos de la pregunta: ¿Se encuentra elementos ajenos al equipo de comunicaciones?");
                        }
                        if (rblElementoAjenos.SelectedValue == "si")
                        {
                            if (!fuElemetoAjenos.HasFile)
                            {
                                throw new Exception("Favor ingresar imagen si se encuentran elementos ajenos al equipo de comunicaciones");
                            }

                        }

                        // UPS
                        if (rblUps.SelectedValue == "")
                        {
                            throw new Exception("Favor llenar los datos de la pregunta: ¿Cuenta con UPS ?");
                        }
                        if (rblUps.SelectedValue == "si")
                        {
                            if (!fuUPS.HasFile)
                            {
                                throw new Exception("Favor ingresar imagen si cuenta con UPS");
                            }

                        }

                        //Aire Acondicionado
                        if (rblAire.SelectedValue == "")
                        {
                            throw new Exception("Favor llenar los datos de la pregunta: ¿Cuenta con aire acondicionado?");
                        }
                        if (rblAire.SelectedValue == "si")
                        {
                            if (!fuAire.HasFile)
                            {
                                throw new Exception("Favor ingresar imagen si cuenta con aire acondicionado");
                            }

                        }

                        //Etiquetado
                        if (rblEtiquetado.SelectedValue == "")
                        {
                            throw new Exception("Favor llenar los datos de la pregunta: ¿La red instalada se encuentra etiquetada?");
                        }

                        //Categoria
                        if (txtCategoria.Text == "")
                        {
                            throw new Exception("Favor llenar los datos de la pregunta: ¿Categorías de cables instalados en agencia?");
                        }

                    }
                }
            }
            catch (Exception Ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Pop", "window.alert('" + Ex.Message + "')", true);
                // Mensaje(Ex.Message, WarningType.Danger);
                //DivAlerta.Visible = true;
                //LbAlertaGenerales.Text = Ex.Message;
                Session["CE_ERRORESTUDIOPREVIO"] = 1;
            }
        }

        void ValidarFormMateriales()
        {
            try
            {
                Session["CE_ERRORMATERIALES"] = 0;

                if (ddlMateriales.SelectedIndex == 0)
                {
                    throw new Exception("Favor seleccionar los material");
                }
                if (txtCantidad.Text == "" || txtCantidad.Text == string.Empty)
                {
                    throw new Exception("Favor ingresar cantidad");
                }
                if (ddlMedidas.SelectedIndex == 0)
                {
                    throw new Exception("Favor seleccionar unidades");
                }
            }
            catch (Exception Ex)
            {
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Pop", "window.alert('" + Ex.Message + "')", true);
                Mensaje(Ex.Message, WarningType.Danger);
                Session["CE_ERRORMATERIALES"] = 1;
            }
        }

        void ValidarFormAprobacion()
        {
            try
            {
                Session["CE_ERRORAPROBACION"] = 0;
                if (rblAprobada.SelectedValue == "")
                {
                    throw new Exception("Favor seleccionar una ópción de aprobación.");
                }

                if (rblAprobada.SelectedValue == "no")
                {
                    if (txtObservacionesAprobacion.Text == "" || txtObservacionesAprobacion.Text == string.Empty)
                    {
                        throw new Exception("Favor ingresar observación.");
                    }
                }

            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
                Session["CE_ERRORAPROBACION"] = 1;
            }
        }

        void ValidarFormEstimacionRecursos()
        {
            try
            {
                Session["CE_ERRORRECURSOS"] = 0;

                if (Session["CE_MATERIALES"] == null)
                {
                    throw new Exception("Favor ingresar materiales");
                }

                if (txtHorasTrabajo.Text == "" || txtHorasTrabajo.Text == string.Empty)
                {
                    throw new Exception("Favor ingresar horas trabajadas");
                }
                if (txtParticipantes.Text == "" || txtParticipantes.Text == string.Empty)
                {
                    throw new Exception("Favor ingresar los participantes");
                }
                if (rblTransporte.SelectedValue == "")
                {
                    throw new Exception("Favor seleccionar si utilizó transporte");
                }
                if (rblALimentación.SelectedValue == "")
                {
                    throw new Exception("Favor seleccionar si necesito alimentación");
                }
            }
            catch (Exception Ex)
            {
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Pop", "window.alert('" + Ex.Message + "')", true);
                Mensaje(Ex.Message, WarningType.Danger);
                Session["CE_ERRORRECURSOS"] = 1;
            }
        }

        //BUTTON
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            String vMensaje;
            try
            {

                ValidarFormMateriales();
                if (Session["CE_ERRORMATERIALES"].ToString() == Convert.ToString(1))
                {
                    vMensaje = "Favor llenar todos los campos.";
                    Mensaje(vMensaje, WarningType.Success);
                }
                else
                {
                    //LbTituloMaterial.Text = "Materiales Agregados";
                    LbDescrpcionMaterial.Text = "Ingreso de materiales para realizar estudio.";
                    string vEdicion = Request.QueryString["c"];

                    DataTable vData = new DataTable();
                    DataTable vDatos = new DataTable();

                    //if (Convert.ToInt32(vEdicion) == 3)
                    //{
                    //    String vIdEstudio = Request.QueryString["e"];

                    //    String vQueryMaterial = "STEISP_CABLESTRUCTURADO_Aprobacion 3," + vIdEstudio;
                    //    vDatos = vConexion.obtenerDataTable(vQueryMaterial);
                    //}
                    //else
                    //{
                    vDatos = (DataTable)Session["CE_MATERIALES"];


                    string vMaterial = ddlMateriales.SelectedValue;
                    string vNombreMaterial = ddlMateriales.SelectedItem.Text;
                    string[] vResult = vNombreMaterial.Split('-');

                    vData.Columns.Add("numero");
                    vData.Columns.Add("nombre");
                    vData.Columns.Add("cantidad");
                    vData.Columns.Add("medida");

                    if (vDatos == null)
                        vDatos = vData.Clone();

                    if (vDatos != null)
                    {

                        if (vDatos.Rows.Count < 1 && Convert.ToInt32(vEdicion) != 3)
                        {
                            vDatos.Rows.Add(ddlMateriales.SelectedValue, ddlMateriales.SelectedItem.Text, txtCantidad.Text, ddlMedidas.SelectedItem.Text);
                        }
                        else
                        {
                            Boolean vRegistered = false;

                            for (int i = 0; i < vDatos.Rows.Count; i++)
                            {
                                if (vNombreMaterial == vDatos.Rows[i]["nombre"].ToString() || vMaterial == vDatos.Rows[i]["numero"].ToString())
                                {
                                    vDatos.Rows[i]["cantidad"] = Convert.ToDecimal(vDatos.Rows[i]["cantidad"].ToString()) + Convert.ToDecimal(txtCantidad.Text);
                                    vRegistered = true;
                                }
                            }

                            if (!vRegistered)
                            {
                                vDatos.Rows.Add(ddlMateriales.SelectedValue, vResult[0], txtCantidad.Text, ddlMedidas.SelectedItem.Text);
                            }
                        }
                    }

                    GVMateriales.DataSource = vDatos;
                    GVMateriales.DataBind();
                    Session["CE_MATERIALES"] = vDatos;
                    UpdateDivMateriales.Update();

                    ddlMateriales.SelectedIndex = -1;
                    udpMetariales.Update();

                    udpCantidad.Update();
                    txtCantidad.Text = "";

                    ddlMedidas.SelectedIndex = -1;
                    udpUnidades.Update();

                    GVMateriales.DataBind();
                    GVMateriales.DataSource = null;
                    UpdateDivMateriales.Update();
                }

            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Success);
            }
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            //Guardar los datos que ingresa el Tecnico
            try
            {
                ValidarFormAprobacion();
                if (Session["CE_ERRORAPROBACION"].ToString() == Convert.ToString(0))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalAprobacion();", true);
                }
                else
                {
                    throw new Exception("Ingrese todos los campos solicitados. ");
                }
            }
            catch (Exception Ex) { Mensaje(Ex.Message, WarningType.Danger); }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            //Guardar los datos que ingresa el Tecnico
            try
            {
                ValidarFormDatosGenerales();
                //ValidarFormEstudioPrevio();
                ValidarFormEstimacionRecursos();
                if ((Session["CE_ERRORGENERALES"].ToString() == Convert.ToString(0)) && (Session["CE_ERRORRECURSOS"].ToString() == Convert.ToString(0)))
                {
                    // ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Pop", "openModalDatosGenerales();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDatosGenerales();", true);
                }
                else
                {
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Pop", "window.alert('" + "Ingrese todos los campos solicitados." + "')", true);
                    throw new Exception("Ingrese todos los campos solicitados. ");
                }

            }
            catch (Exception Ex) { Mensaje(Ex.Message, WarningType.Danger); }


        }

        protected void BtnModGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string vArchivo = String.Empty; string vArchivo1 = String.Empty; string vArchivo2 = String.Empty;
                string vArchivo3 = String.Empty; string vArchivo4 = String.Empty; string vArchivo5 = String.Empty;
                string vArchivo6 = String.Empty; string vArchivo7 = String.Empty; string vArchivo8 = String.Empty;

                if (fuCuartoTelecomunicaciones.HasFile)
                {
                    string vNombreDepot = String.Empty;
                    HttpPostedFile bufferDepositoT = fuCuartoTelecomunicaciones.PostedFile;
                    byte[] vFileDeposito = null;
                    string vExtension = string.Empty;

                    if (bufferDepositoT != null)
                    {
                        vNombreDepot = fuCuartoTelecomunicaciones.FileName;
                        Stream vStream = bufferDepositoT.InputStream;
                        BinaryReader vReader = new BinaryReader(vStream);
                        vFileDeposito = vReader.ReadBytes((int)vStream.Length);
                        vExtension = System.IO.Path.GetExtension(fuCuartoTelecomunicaciones.FileName);
                    }

                    if (vFileDeposito != null)
                        vArchivo = Convert.ToBase64String(vFileDeposito);
                }
                else
                {
                    string[] vImg = new string[2];
                    vImg = imgCuartoTelecomunicaciones.Src.Split(',');
                    vArchivo = vImg[1];
                }

                if (rblNuevoRemodelacion.SelectedValue == "remodelacion")
                {
                    if (fuDesordenado.HasFile)
                    {
                        string vNombreDepot2 = String.Empty;
                        HttpPostedFile bufferDepositoT2 = fuDesordenado.PostedFile;
                        byte[] vFileDeposito2 = null;
                        string vExtension2 = string.Empty;

                        if (bufferDepositoT2 != null)
                        {
                            vNombreDepot2 = fuDesordenado.FileName;
                            Stream vStream2 = bufferDepositoT2.InputStream;
                            BinaryReader vReader2 = new BinaryReader(vStream2);
                            vFileDeposito2 = vReader2.ReadBytes((int)vStream2.Length);
                            vExtension2 = System.IO.Path.GetExtension(fuDesordenado.FileName);
                        }
                        if (vFileDeposito2 != null)
                            vArchivo2 = Convert.ToBase64String(vFileDeposito2);
                    }
                    else
                    {
                        string[] vImg1 = new string[2];
                        vImg1 = imgDesordenado.Src.Split(',');
                        vArchivo2 = vImg1[1];
                    }

                    if (rblReubicar.SelectedValue == "si")
                    {
                        if (fuReubicar.HasFile)
                        {
                            string vNombreDepot1 = String.Empty;
                            HttpPostedFile bufferDepositoT1 = fuReubicar.PostedFile;
                            byte[] vFileDeposito1 = null;
                            string vExtension1 = string.Empty;

                            if (bufferDepositoT1 != null)
                            {
                                vNombreDepot1 = fuReubicar.FileName;
                                Stream vStream1 = bufferDepositoT1.InputStream;
                                BinaryReader vReader1 = new BinaryReader(vStream1);
                                vFileDeposito1 = vReader1.ReadBytes((int)vStream1.Length);
                                vExtension1 = System.IO.Path.GetExtension(fuReubicar.FileName);
                            }

                            if (vFileDeposito1 != null)
                                vArchivo1 = Convert.ToBase64String(vFileDeposito1);
                        }
                        else
                        {
                            string[] vImg2 = new string[2];
                            vImg2 = imgReubicar.Src.Split(',');
                            vArchivo1 = vImg2[1];
                        }
                    }

                    //IMAGENES4
                    if (rblExpuestoHumedo.SelectedValue == "si")
                    {
                        if (fuExpuestoHumedo.HasFile)
                        {
                            string vNombreDepot3 = String.Empty;
                            HttpPostedFile bufferDepositoT3 = fuExpuestoHumedo.PostedFile;
                            byte[] vFileDeposito3 = null;
                            string vExtension3 = string.Empty;

                            if (bufferDepositoT3 != null)
                            {
                                vNombreDepot3 = fuExpuestoHumedo.FileName;
                                Stream vStream3 = bufferDepositoT3.InputStream;
                                BinaryReader vReader3 = new BinaryReader(vStream3);
                                vFileDeposito3 = vReader3.ReadBytes((int)vStream3.Length);
                                vExtension3 = System.IO.Path.GetExtension(fuExpuestoHumedo.FileName);
                            }
                            if (vFileDeposito3 != null)
                                vArchivo3 = Convert.ToBase64String(vFileDeposito3);
                        }
                        else
                        {
                            string[] vImg3 = new string[2];
                            vImg3 = imgExpuestoHumedo.Src.Split(',');
                            vArchivo3 = vImg3[1];
                        }
                    }

                    if (rblExpuestoRobo.SelectedValue == "si")
                    {
                        if (fuExpuestoRobo.HasFile)
                        {
                            string vNombreDepot4 = String.Empty;
                            HttpPostedFile bufferDepositoT4 = fuExpuestoRobo.PostedFile;
                            byte[] vFileDeposito4 = null;
                            string vExtension4 = string.Empty;

                            if (bufferDepositoT4 != null)
                            {
                                vNombreDepot4 = fuExpuestoRobo.FileName;
                                Stream vStream4 = bufferDepositoT4.InputStream;
                                BinaryReader vReader4 = new BinaryReader(vStream4);
                                vFileDeposito4 = vReader4.ReadBytes((int)vStream4.Length);
                                vExtension4 = System.IO.Path.GetExtension(fuExpuestoRobo.FileName);
                            }

                            if (vFileDeposito4 != null)
                                vArchivo4 = Convert.ToBase64String(vFileDeposito4);
                        }
                        else
                        {
                            string[] vImg4 = new string[2];
                            vImg4 = imgExpuestoRobo.Src.Split(',');
                            vArchivo4 = vImg4[1];
                        }
                    }

                    if (rblElementoAjenos.SelectedValue == "si")
                    {
                        if (fuElemetoAjenos.HasFile)
                        {
                            string vNombreDepot5 = String.Empty;
                            HttpPostedFile bufferDepositoT5 = fuElemetoAjenos.PostedFile;
                            byte[] vFileDeposito5 = null;
                            string vExtension5 = string.Empty;

                            if (bufferDepositoT5 != null)
                            {
                                vNombreDepot5 = fuElemetoAjenos.FileName;
                                Stream vStream5 = bufferDepositoT5.InputStream;
                                BinaryReader vReader5 = new BinaryReader(vStream5);
                                vFileDeposito5 = vReader5.ReadBytes((int)vStream5.Length);
                                vExtension5 = System.IO.Path.GetExtension(fuElemetoAjenos.FileName);
                            }

                            if (vFileDeposito5 != null)
                                vArchivo5 = Convert.ToBase64String(vFileDeposito5);
                        }
                        else
                        {
                            string[] vImg5 = new string[2];
                            vImg5 = imgElementoAjeno.Src.Split(',');
                            vArchivo5 = vImg5[1];
                        }
                    }

                    if (rblUps.SelectedValue == "si")
                    {
                        if (fuUPS.HasFile)
                        {
                            string vNombreDepot7 = String.Empty;
                            HttpPostedFile bufferDepositoT7 = fuUPS.PostedFile;
                            byte[] vFileDeposito7 = null;
                            string vExtension7 = string.Empty;

                            if (bufferDepositoT7 != null)
                            {
                                vNombreDepot7 = fuUPS.FileName;
                                Stream vStream7 = bufferDepositoT7.InputStream;
                                BinaryReader vReader7 = new BinaryReader(vStream7);
                                vFileDeposito7 = vReader7.ReadBytes((int)vStream7.Length);
                                vExtension7 = System.IO.Path.GetExtension(fuUPS.FileName);
                            }

                            if (vFileDeposito7 != null)
                                vArchivo7 = Convert.ToBase64String(vFileDeposito7);
                        }
                        else
                        {
                            string[] vImg6 = new string[2];
                            vImg6 = imgUPS.Src.Split(',');
                            vArchivo7 = vImg6[1];
                        }
                    }

                    if (rblAire.SelectedValue == "si")
                    {
                        if (fuAire.HasFile)
                        {
                            string vNombreDepot8 = String.Empty;
                            HttpPostedFile bufferDepositoT8 = fuAire.PostedFile;
                            byte[] vFileDeposito8 = null;
                            string vExtension8 = string.Empty;

                            if (bufferDepositoT8 != null)
                            {
                                vNombreDepot8 = fuAire.FileName;
                                Stream vStream8 = bufferDepositoT8.InputStream;
                                BinaryReader vReader8 = new BinaryReader(vStream8);
                                vFileDeposito8 = vReader8.ReadBytes((int)vStream8.Length);
                                vExtension8 = System.IO.Path.GetExtension(fuAire.FileName);
                            }

                            if (vFileDeposito8 != null)
                                vArchivo8 = Convert.ToBase64String(vFileDeposito8);
                        }
                        else
                        {
                            string[] vImg7 = new string[2];
                            vImg7 = imgAire.Src.Split(',');
                            vArchivo8 = vImg7[1];
                        }
                    }
                }

                if (fuPlano.HasFile)
                {
                    string vNombreDepot6 = String.Empty;
                    HttpPostedFile bufferDepositoT6 = fuPlano.PostedFile;
                    byte[] vFileDeposito6 = null;
                    string vExtension6 = string.Empty;

                    if (bufferDepositoT6 != null)
                    {
                        vNombreDepot6 = fuPlano.FileName;
                        Stream vStream6 = bufferDepositoT6.InputStream;
                        BinaryReader vReader6 = new BinaryReader(vStream6);
                        vFileDeposito6 = vReader6.ReadBytes((int)vStream6.Length);
                        vExtension6 = System.IO.Path.GetExtension(fuPlano.FileName);
                    }

                    if (vFileDeposito6 != null)
                        vArchivo6 = Convert.ToBase64String(vFileDeposito6);
                }
                else
                {
                    string[] vImg8 = new string[2];
                    vImg8 = IFramePDF.Src.Split(',');
                    vArchivo6 = vImg8[1];
                }

                ValidarFormEstudioPrevio();
                if (Session["CE_ERRORESTUDIOPREVIO"].ToString() == Convert.ToString(1))
                {
                    throw new Exception(LbAlertaGenerales.Text);
                }
                else
                {
                    string vIdEstudioEdicion = Request.QueryString["e"];
                    string vEdicion = Request.QueryString["c"];

                    if (vEdicion != Convert.ToString(3))
                    {
                        vIdEstudioEdicion = Convert.ToString(0);
                        vEdicion = Convert.ToString(1);

                    }
                    else if (vEdicion == Convert.ToString(3))
                    {
                        try
                        {
                            String vQueryR = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 3, '" + ddlResponsable.SelectedValue + "'";

                            DataTable vDatosR = vConexion.obtenerDataTable(vQueryR);
                            Session["CE_IDRESPONSABLE"] = vDatosR.Rows[0]["idUsuario"].ToString();

                            String vQueryA = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 1, '" + ddlAgencia.SelectedValue + "'";

                            DataTable vDatosA = vConexion.obtenerDataTable(vQueryA);
                            Session["CE_IDUBICACION"] = vDatosA.Rows[0]["idUbicacion"].ToString();
                            Session["CE_IDEPARTAMENTO"] = vDatosA.Rows[0]["idDepartamento"].ToString();


                            String vQueryUbicacion = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 21, '" + ddlAgencia.SelectedValue + "'";

                            DataTable vDatosUbicacion = vConexion.obtenerDataTable(vQueryUbicacion);
                            Session["CE_NOMBREESTUDIO"] = vDatosUbicacion.Rows[0]["codigo"].ToString();

                            String vQueryFecha = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 30, '" + vIdEstudioEdicion + "'";

                            DataTable vDatosFecha = vConexion.obtenerDataTable(vQueryFecha);
                            txtFechaEnvio.Text = vDatosFecha.Rows[0]["fechaEnvio"].ToString();
                            txtFechaEstudio.Text = vDatosFecha.Rows[0]["fechaEstudio"].ToString();

                        }
                        catch (Exception Ex)
                        {
                            Mensaje(Ex.Message, WarningType.Danger);
                        }

                    }

                    //Ingresa los datos generales

                    String vQuery = "STEISP_CABLESTRUCTURADO_IngresarDatosEstudio 1," + Session["CE_IDEPARTAMENTO"] + "," +
                                                                                    "'" + Session["CE_IDUBICACION"] + "'," +
                                                                                    "'" + Session["CE_IDRESPONSABLE"] + "'," +
                                                                                    "'" + Session["CE_NOMBREESTUDIO"] + "'," +
                                                                                    "'" + Convert.ToDateTime(txtFechaEstudio.Text) + "'," +
                                                                                    "'" + Convert.ToDateTime(txtFechaEnvio.Text) + "'," +
                                                                                    "'" + txtHorasTrabajo.Text + "'," +
                                                                                    "'" + txtParticipantes.Text + "'," +
                                                                                    "'" + rblALimentación.SelectedValue + "'," +
                                                                                    "'" + rblTransporte.SelectedValue + "'," +
                                                                                    "'" + txtObservaciones.Text + "'," +
                                                                                    "'" + rblNuevoRemodelacion.SelectedValue + "'," +
                                                                                    "'IngresoTecnico'" + "," +
                                                                                    "'" + vEdicion + "'," +
                                                                                    "" + vIdEstudioEdicion;

                    DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                    //Obtiene IdEstudio

                    String vQuery1 = "STEISP_CABLESTRUCTURADO_IngresarDatosEstudio 3";

                    DataTable vDatos1 = vConexion.obtenerDataTable(vQuery1);
                    Session["CE_IDESTUDIO"] = (vDatos1.Rows[0]["Id"].ToString());

                    if (vIdEstudioEdicion == Convert.ToString(0))
                    {
                        vIdEstudioEdicion = Session["CE_IDESTUDIO"].ToString();
                    }
                    //Ingresa los datos del material 

                    foreach (GridViewRow row in GVMateriales.Rows)
                    {
                        string vMaterial;
                        string vMedidas;
                        string vCantidad;
                        string vIdMaterial;

                        vIdMaterial = (row.Cells[0].Text);

                        vMaterial = (row.Cells[1].Text);

                        vCantidad = (row.Cells[2].Text);

                        vMedidas = (row.Cells[3].Text);

                        String[] result = vMaterial.Split('-');

                        String vQuery2 = "STEISP_CABLESTRUCTURADO_IngresarDatosEstudio 5," + vIdEstudioEdicion + "," +
                                                                                            "'" + vIdMaterial + "'," +
                                                                                            "'" + result[0] + "'," +
                                                                                            "'" + vMedidas + "'," +
                                                                                            "'" + vCantidad + "'," +
                                                                                            "" + vEdicion;

                        DataTable vDatos2 = vConexion.obtenerDataTable(vQuery2);
                    }

                    //Ingreso de Preguntaas
                    String vQuery3 = "STEISP_CABLESTRUCTURADO_IngresarDatosEstudio 4," + Session["CE_IDESTUDIO"] + "," +
                                                                                        "'" + vArchivo + "'," +
                                                                                    "'" + rblEtiquetado.SelectedValue + "'," +
                                                                                    "'" + rblReubicar.SelectedValue + "'," +
                                                                                    "'" + vArchivo1 + "'," +
                                                                                    "'" + rblDesordenado.SelectedValue + "'," +
                                                                                    "'" + vArchivo2 + "'," +
                                                                                    "'" + rblExpuestoHumedo.SelectedValue + "'," +
                                                                                    "'" + vArchivo3 + "'," +
                                                                                    "'" + rblExpuestoRobo.SelectedValue + "'," +
                                                                                    "'" + vArchivo4 + "'," +
                                                                                    "'" + rblElementoAjenos.SelectedValue + "'," +
                                                                                    "'" + vArchivo5 + "'," +
                                                                                    "'" + rblUps.SelectedValue + "'," +
                                                                                    "'" + vArchivo7 + "'," +
                                                                                    "'" + rblAire.SelectedValue + "'," +
                                                                                    "'" + vArchivo8 + "'," +
                                                                                    "'" + txtCategoria.Text.ToUpper() + "'," +
                                                                                    "'" + vArchivo6 + "'," +
                                                                                    "'Disponible'" + "," +
                                                                                    "'" + vEdicion + "'";

                    DataTable vDatos3 = vConexion.obtenerDataTable(vQuery3);

                    if (vEdicion != Convert.ToString(3))
                    {
                        String vQuery4 = "STEISP_CABLESTRUCTURADO_IngresarDatosEstudio 7," + Session["CE_IDESTUDIO"] + "," +
                                                                                    "'" + Session["CE_IDRESPONSABLE"] + "'";

                        DataTable vDatos4 = vConexion.obtenerDataTable(vQuery4);
                    }

                    Limpiar();
                    Mensaje("Actualizado con Exito!", WarningType.Success);
                    CerrarModal("MensajeAceptacionModal");
                    Session["CE_MATERIALES"] = null;
                    Response.Redirect("/sites/cableado/page/visita/principalVisitaTecnica.aspx");

                    SmtpService vService = new SmtpService();

                }

                //String vQueryMail = "RSP_ObtenerPermisos 2,"
                //        + Session["USUARIO"] + ","
                //        + LbNumeroPermiso.Text + ","
                //        + DDLOpciones.SelectedValue + ",'" + TxMotivoJefe.Text + "'";
                //int vDatosMail = vConexion.ejecutarSql(vQueryMail);

                //if (vDatos.Equals(1))
                //{
                //    if (DDLOpciones.SelectedValue.Equals("1"))
                //    {
                //        vQuery = "RSP_ObtenerPermisos 3," + Session["USUARIO"] + "," + LbNumeroPermiso.Text;
                //        DataTable vDatosBusqueda = vConexion.obtenerDataTable(vQuery);

                //        foreach (DataRow item in vDatosBusqueda.Rows)
                //        {
                //            vService.EnviarMensaje(ConfigurationManager.AppSettings["RHMail"],
                //                    typeBody.RecursosHumanos,
                //                    "Recursos Humanos",
                //                    item["Empleado"].ToString()
                //                    );
                //        }
                //    }
                //}

            }
            catch (Exception Ex) { Mensaje(Ex.Message, WarningType.Danger); }
        }

        protected void btnModAproGuardar_Click(object sender, EventArgs e)
        {
            string vMensaje = "";

            //Guardar los datos que ingressa el Tecnico
            try
            {
                ValidarFormAprobacion();
                String vIdEstudioAprobacion = Request.QueryString["ia"];

                Int32 vInformacion1 = 0;
                Int32 vConfirmacion = 0;
                Int32 vAprobacion = 0;

                String vQueryAproRel = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 33," + vIdEstudioAprobacion;
                DataTable vDatos1 = vConexion.obtenerDataTable(vQueryAproRel);

                if (rblAprobada.SelectedValue == "no")
                {

                    //String vQuery1 = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 33," + 10;


                    //vInformacion1 = vConexion.ejecutarSql(vQuery1);

                    if (vDatos1.Rows.Count == 1)
                    {
                        String vQuery2 = "STEISP_CABLESTRUCTURADO_IngresarDatosEstudio 2,'" + rblAprobada.SelectedValue + "'," +
                                                                                        "'" + txtObservacionesAprobacion.Text + "'," +
                                                                                        "'" + vIdEstudioAprobacion + "'," +
                                                                                        "'Pendiente'," +
                                                                                        "'3'";
                        Int32 vInformacion2 = vConexion.ejecutarSql(vQuery2);

                    }
                    else
                    {
                        //Inserta datos a la tabla Aprobacion
                        String vQuery2 = "STEISP_CABLESTRUCTURADO_IngresarDatosEstudio 2," + rblAprobada.SelectedValue + "," +
                                                                                  "'" + txtObservacionesAprobacion.Text + "'," +
                                                                                  "'" + vIdEstudioAprobacion + "'," +
                                                                                  "'Pendiente'," +
                                                                                  "'1'";
                        Int32 vInformacion2 = vConexion.ejecutarSql(vQuery2);

                    }

                    String vQuery = "STEISP_CABLESTRUCTURADO_IngresarDatosEstudio 11," + vIdEstudioAprobacion + "," +
                                                                                     "'EdicionTecnico'";
                    Int32 vInformacion = vConexion.ejecutarSql(vQuery);
                    vConfirmacion = 1;
                }
                else if (rblAprobada.SelectedValue == "si")
                {
                    String vQuery1 = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 31," + vIdEstudioAprobacion;
                    vInformacion1 = vConexion.ejecutarSql(vQuery1);

                    if (vInformacion1 == 1)
                    {
                        String vQuery = "STEISP_CABLESTRUCTURADO_IngresarDatosEstudio 2," + rblAprobada.SelectedValue + "," +
                                                                                        "'" + txtObservacionesAprobacion.Text + "'," +
                                                                                        "'" + vIdEstudioAprobacion + "'," +
                                                                                        "'AprobaciónJefe'," +
                                                                                        "'3'";
                        Int32 vInformacion = vConexion.ejecutarSql(vQuery);
                    }
                    else
                    {
                        //Inserta datos a la tabla Aprobacion
                        String vQuery = "STEISP_CABLESTRUCTURADO_IngresarDatosEstudio 2," + rblAprobada.SelectedValue + "," +
                                                                                        "'" + txtObservacionesAprobacion.Text + "'," +
                                                                                        "'" + vIdEstudioAprobacion + "'," +
                                                                                        "'AprobaciónJefe'," +
                                                                                        "'1'";
                        Int32 vInformacion = vConexion.ejecutarSql(vQuery);

                    }

                    String vQueryEstado = "STEISP_CABLESTRUCTURADO_IngresarDatosEstudio 11," + vIdEstudioAprobacion + "," +
                                                                                         "'AprobaciónJefe'";
                    Int32 vInformacionEstado = vConexion.ejecutarSql(vQueryEstado);
                    vAprobacion = 1;


                }
                //Trae IdAprobacion
                String vQueryId = "STEISP_CABLESTRUCTURADO_IngresarDatosEstudio 9," + vIdEstudioAprobacion;
                DataTable vDatosId = vConexion.obtenerDataTable(vQueryId);

                Int32 vInforId = vConexion.ejecutarSql(vQueryId);

                string vIdAprobacion = (vDatosId.Rows[0]["Id"].ToString());
                string vCodigo = (vDatosId.Rows[0]["codigo"].ToString());

                //Obtener IdEstudio
                Int32 vInforApro1;
                Int32 vInforApro;
                if (vDatos1.Rows.Count != 0)
                {
                    //Inserta datos en la tabla EstudioRelAprobacion
                    String vQueryAprobacion = "STEISP_CABLESTRUCTURADO_IngresarDatosEstudio 10," + vIdAprobacion + "," +
                                                                                              "'" + vIdEstudioAprobacion + "'," +
                                                                                              "'" + vCodigo + "'," +
                                                                                              "'Pendiente'," +
                                                                                              "'3'";

                    vInforApro = vConexion.ejecutarSql(vQueryAprobacion);
                }
                else
                {
                    string vEstado = "";

                    if (vConfirmacion == 0)
                    {
                        vEstado = "EstudioAprobado";
                    }
                    else if (vConfirmacion == 1)
                    {
                        vEstado = "Pendiente";
                    }
                    //Inserta datos en la tabla EstudioRelAprobacion
                    String vQueryAprobacion = "STEISP_CABLESTRUCTURADO_IngresarDatosEstudio 10," + vIdAprobacion + "," +
                                                                                              "'" + vIdEstudioAprobacion + "'," +
                                                                                              "'" + vCodigo + "'," +
                                                                                              "'" + vEstado + "'," +
                                                                                              "'1'";

                    vInforApro = vConexion.ejecutarSql(vQueryAprobacion);
                }



                if (vInforId == -1)
                {
                    vMensaje = "Actualizado con Exito!";
                    rblAprobada.ClearSelection();
                    udpAprobado.Update();
                    txtObservacionesAprobacion.Text = "";
                    udpObservacionesAprobacion.Update();

                    //Mensaje("Actualizado con Exito!", WarningType.Success);

                }
                else
                {
                    vMensaje = "No se pudo actualizar!";
                    Mensaje(vMensaje, WarningType.Danger);
                    //Mensaje("No se pudo actualizar!", WarningType.Success);

                }

                // Mensaje("Actualizado con Exito!", WarningType.Success);
                CerrarModal("MensajeAceptacionModalApro");

                Mensaje(vMensaje, WarningType.Success);
                Response.Redirect("/sites/cableado/page/visita/aprobacion.aspx");

            }
            catch (Exception Ex) { Mensaje(Ex.Message, WarningType.Danger); }
        }

        //Otros
        public void EliminarSesiones()
        {
            // Session["CE_MATERIALES"] = null;
            Session["CE_CABLEADO"] = null;
            Session["CE_ROLES"] = null;
            Session["CE_IDRESPONSABLE"] = null;
            Session["CE_IDUBICACION"] = null;
            Session["CE_IDEPARTAMENTO"] = null;
            Session["CE_IDMATERIAL"] = null;
            Session["CE_MATERIALES"] = null;
        }

        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        public void CerrarModal(String vModal)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Pop", "$('#" + vModal + "').modal('hide');", true);
        }

        //protected void LbtnPlano_Click(object sender, EventArgs e)
        //{

        //    //vidPreview.Visible = true;
        //    //IFramePDF.Src = fuPlano.HasAttributes;
        //    //udpLbPlano.Update();

        //    if (fuPlano.HasFile)
        //    {
        //        IFramePDF.Src = Path.GetFileName(fuPlano.PostedFile.FileName);

        //    }

        //}

    }

}
