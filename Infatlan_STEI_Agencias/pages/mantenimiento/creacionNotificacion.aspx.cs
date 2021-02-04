using Infatlan_STEI_Agencias.classes;
using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Infatlan_STEI_Agencias.pages
{
    public partial class CreacionNotificacion : System.Web.UI.Page
    {
        db vConexion = new db();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Convert.ToBoolean(Session["AUTH"]))
                {
                    //GVjefesAgencias.DataSource = null;
                    //GVjefesAgencias.DataBind();
                    //GVJefesAD.DataSource = null;
                    //GVJefesAD.DataBind();
                    //GVBusqueda.DataSource = null;
                    //GVBusqueda.DataBind();
                    //Session["AG_CN_TECNICOS_PARTICIPANTES"] = "";
                    //Session["AG_CN_CORREO_JEFE_AD_NOTI"] = "";

                    cargarData();
                }
                else
                {
                    Response.Redirect("/login.aspx");
                }
            }
        }

        internal void cargarData()
        {
            try
            {
                DDLNombreResponsable.Items.Clear();
                String vQuery = "STEISP_AGENCIA_CreacionNotificacion 5";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                DDLNombreResponsable.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                DDLNombreParticipantes.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                if (vDatos.Rows.Count > 0)
                {
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLNombreResponsable.Items.Add(new ListItem { Value = item["idUsuario"].ToString(), Text = item["nombre"].ToString() + "  " + item["apellidos"].ToString() });
                        DDLNombreParticipantes.Items.Add(new ListItem { Value = item["idUsuario"].ToString(), Text = item["nombre"].ToString() + "  " + item["apellidos"].ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //Session["AG_CN_TECNICOS_PARTICIPANTES"] = "";
                //Session["AG_CN_CORREO_JEFE_AD_NOTI"] = "";
                //GVjefesAgencias.DataSource = null;
                //GVjefesAgencias.DataBind();              
                //GVBusqueda.DataSource = null;
                //GVBusqueda.DataBind();
                DDLCodigoAgencia.Items.Clear();
                String vTest = TextBox1.Text;
                String v2 = Convert.ToDateTime(vTest).ToString("yyyy-MM-dd");

                String vQuery = "STEISP_AGENCIA_CreacionNotificacion 1,'" + v2 + "'";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                DDLCodigoAgencia.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                if (vDatos.Rows.Count > 0)
                {
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLCodigoAgencia.Items.Add(new ListItem { Value = item["idMantenimiento"].ToString(), Text = item["codigoAgencia"].ToString() + " - " + item["nombre"].ToString() });

                    }

                    DDLCodigoAgencia.Enabled = true;
                    TxSysAid.ReadOnly = false;
                    TxHoraInicio.ReadOnly = false;
                    TxHoraFin.ReadOnly = false;
                    DDLNombreResponsable.Enabled = true;
                    DDLNombreParticipantes.Enabled = true;
                    txtbuscarJefeNotif.ReadOnly = false;
                    //btnBuscarJefe.Enabled = false;

                    TxMantEquipoComu.Text = "Si";
                    TxHoraInicio.Text = "07:00";
                    TxHoraFin.Text = "08:30";
                    UpdatePanel1.Update();
                }
                else
                {
                    Mensaje("No hay mantenimientos disponibles en la fecha que selecciono: " + TextBox1.Text + " , ingresar otra fecha.", WarningType.Info);
                }
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void DDLCodigoAgencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                TxLugar.Text = DDLCodigoAgencia.SelectedItem.Text;
                String vQuery = "STEISP_AGENCIA_CreacionNotificacion 2," + DDLCodigoAgencia.SelectedValue;
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                TxArea.Text = vDatos.Rows[0]["nombre"].ToString();

                String vQuery1 = "STEISP_AGENCIA_CreacionNotificacion 3," + DDLCodigoAgencia.SelectedValue;
                DataTable vDatos1 = vConexion.obtenerDataTable(vQuery1);
                TxDepartamento.Text = vDatos1.Rows[0]["nombre"].ToString();

                String vQuery2 = "STEISP_AGENCIA_CreacionNotificacion 4," + DDLCodigoAgencia.SelectedValue;
                DataTable vDatos2 = vConexion.obtenerDataTable(vQuery2);
                TxDireccion.Text = vDatos2.Rows[0]["direccion"].ToString();
                TxTelefono.Text = vDatos2.Rows[0]["telefono"].ToString();
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void DDLNombreResponsable_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String vQuery2 = "STEISP_AGENCIA_CreacionNotificacion 6 ,'" + DDLNombreResponsable.SelectedValue + "'";
                DataTable vDatos2 = vConexion.obtenerDataTable(vQuery2);
                TxIdentidadResponsable.Text = vDatos2.Rows[0]["identidad"].ToString();
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void DDLNombreParticipantes_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (DDLNombreParticipantes.SelectedValue.Equals("0"))
                    throw new Exception("Por favor seleccione tecnico participante.");
                else
                {

                    foreach (GridViewRow item in GVBusqueda.Rows)
                    {
                        if (item.Cells[1].Text.Equals(DDLNombreParticipantes.Text))
                        {
                            throw new Exception("Ya existe tecnico responsable.");
                        }
                    }

                    String vQuery2 = "STEISP_AGENCIA_CreacionNotificacion 7 ," + DDLNombreParticipantes.SelectedValue;
                    DataTable vDatos = vConexion.obtenerDataTable(vQuery2);
                    DataTable vDatos2 = new DataTable();

                    String vId = vDatos.Rows[0]["idUsuario"].ToString();
                    String vNombre = vDatos.Rows[0]["nombre"].ToString();
                    String vIdentidad = vDatos.Rows[0]["identidad"].ToString();
                    string vCorreo = vDatos.Rows[0]["correo"].ToString();

                    if (HttpContext.Current.Session["AG_CN_TECNICOS_PARTICIPANTES"] == null)
                    {
                        vDatos2.Columns.Add("idUsuario");
                        vDatos2.Columns.Add("nombre");
                        vDatos2.Columns.Add("identidad");
                        vDatos2.Columns.Add("correo");

                        vDatos2.Rows.Add(vId, vNombre, vIdentidad, vCorreo);
                    }
                    else
                    {
                        vDatos2 = (DataTable)Session["AG_CN_TECNICOS_PARTICIPANTES"];
                        vDatos2.Rows.Add(vId, vNombre, vIdentidad, vCorreo);
                    }
                    GVBusqueda.DataSource = vDatos2;
                    GVBusqueda.DataBind();
                    Session["AG_CN_TECNICOS_PARTICIPANTES"] = vDatos2;
                    DDLNombreParticipantes.SelectedIndex = -1;
                }
            }
            catch (Exception Ex) { Mensaje(Ex.Message, WarningType.Danger); }
        }

        //protected void btnAgregar_Click(object sender, EventArgs e)
        //{
        //    try
        //    {             
        //        if (TxCorreo.Text.Equals(""))
        //            throw new Exception("Por favor llene el campo de correo.");

        //        foreach (GridViewRow item in GVCorreoJefeAgencia.Rows)
        //        {
        //            if (item.Cells[1].Text.Equals(TxCorreo.Text))
        //            {
        //                throw new Exception("Ya existe este correo.");
        //            }
        //        }

        //        DataTable vDatos = new DataTable();
        //        vDatos.Columns.Add("CorreoJefeAgencia");
        //        if (HttpContext.Current.Session["CORREOS"] == null)
        //        {
        //            vDatos.Rows.Add(TxCorreo.Text);
        //        }
        //        else
        //        {
        //            vDatos = (DataTable)Session["CORREOS"];                  
        //            vDatos.Rows.Add(new Object[] { TxCorreo.Text });
        //        }
        //        GVCorreoJefeAgencia.DataSource = vDatos;
        //        GVCorreoJefeAgencia.DataBind();
        //        Session["CORREOS"] = vDatos;
        //        UpdatePanel1.Update();
        //        TxCorreo.Text = String.Empty;
        //    }
        //    catch (Exception Ex) { Mensaje(Ex.Message, WarningType.Danger); }
        //}


        //protected void GVCorreoJefeAgencia_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    try
        //    {
        //        GVCorreoJefeAgencia.DataSource = (DataTable)Session["CORREOS"];
        //        GVCorreoJefeAgencia.PageIndex = e.NewPageIndex;
        //        GVCorreoJefeAgencia.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        Mensaje(ex.Message, WarningType.Danger);
        //    }
        //}


        //protected void GVCorreoJefeAgencia_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    DataTable vDatos = (DataTable)Session["CORREOS"];
        //    if (e.CommandName == "Eliminar")
        //    {
        //        String vCorreo = e.CommandArgument.ToString();
        //        if (Session["CORREOS"] != null)
        //        {
        //            DataRow[] result = vDatos.Select("CorreoJefeAgencia = '" + vCorreo + "'");
        //            foreach (DataRow row in result)
        //            {
        //                if (row["CorreoJefeAgencia"].ToString().Contains(vCorreo))
        //                    vDatos.Rows.Remove(row);
        //            }
        //        }
        //    }
        //    GVCorreoJefeAgencia.DataSource = vDatos;
        //    GVCorreoJefeAgencia.DataBind();
        //    Session["CORREOS"] = vDatos;
        //}

        private void validaciones()
        {
            if (TextBox1.Text == "" || TextBox1.Text == string.Empty)
                throw new Exception("Favor seleccione fecha del mantenimiento.");
            if (DDLCodigoAgencia.SelectedValue.Equals("0"))
                throw new Exception("Favor seleccione Código/Lugar Agencia.");
            if (TxSysAid.Text == "" || TxSysAid.Text == string.Empty)
                throw new Exception("Favor ingrese número de SysAid.");
            //if (GVCorreoJefeAgencia.Rows.Count==0)
            //    throw new Exception("Favor ingrese correo de jefe de agencia.");
            if (DDLNombreResponsable.SelectedValue.Equals("0"))
                throw new Exception("Favor seleccione tecnico responsable.");
        }

        protected void BtnEnviarNotificacion_Click(object sender, EventArgs e)
        {
            try
            {

                validaciones();

                TxLugar.Text = DDLCodigoAgencia.SelectedItem.Text;
                TxFecha.Text = TextBox1.Text;
                TxHrInicioModal.Text = TxHoraInicio.Text;
                TxHrFinModal.Text = TxHoraFin.Text;
                TxResponsable.Text = DDLNombreResponsable.SelectedItem.Text;
                lbTitulo.Text = "Crear Notificación " + TxLugar.Text;
                UpdatePanel2.Update();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);

            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DataTable vDatos = (DataTable)Session["AG_CN_TECNICOS_PARTICIPANTES"];
            if (e.CommandName == "Eliminar")
            {
                String vUsuario = e.CommandArgument.ToString();
                if (Session["AG_CN_TECNICOS_PARTICIPANTES"] != null)
                {

                    DataRow[] result = vDatos.Select("idUsuario = '" + vUsuario + "'");
                    foreach (DataRow row in result)
                    {
                        if (row["idUsuario"].ToString().Contains(vUsuario))
                            vDatos.Rows.Remove(row);
                    }
                }
            }
            GVBusqueda.DataSource = vDatos;
            GVBusqueda.DataBind();
            Session["AG_CN_TECNICOS_PARTICIPANTES"] = vDatos;
        }

        protected void GVBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GVBusqueda.DataSource = (DataTable)Session["AG_CN_TECNICOS_PARTICIPANTES"];
                GVBusqueda.PageIndex = e.NewPageIndex;
                GVBusqueda.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        void EnviarCorreo()
        {
            SmtpService vService = new SmtpService();
            string vZonaAgencia = "";
            string vIDMantenimiento = DDLCodigoAgencia.SelectedValue;

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
                        "Notificación de Mantenimiento Agencia",
                        "Buen día, se le notifica que se creó solicitud de mantenimiento, el encargado es " + vDatosTecnicoResponsable.Rows[0]["Nombre"].ToString() + ", mantenimiento a Agencia " + DDLCodigoAgencia.SelectedItem.Text + " para la fecha " + TxFecha.Text,
                        "El usuario <b>" + item["Nombre"].ToString() + "</b> creó: <br> Notificación de Mantenimiento",
                        "",
                        "/sites/agencias/pages/mantenimiento/creacionNotificacion.aspx"
                        );

                        //vFlagEnvioSupervisor = true;
                    }
                    //ENVIAR A EDWIN
                    //string vNombre = "EDWIN ALBERTO URREA PENA";
                    vService.EnviarMensaje(ConfigurationManager.AppSettings["STEIMail"],
                            typeBody.EnvioCorreo,
                            "Notificación de Mantenimiento Agencia",
                            "Buen día, se le notifica que se creó solicitud de mantenimiento, el encargado es " + vDatosTecnicoResponsable.Rows[0]["Nombre"].ToString() + ", mantenimiento a Agencia " + DDLCodigoAgencia.SelectedItem.Text + " para la fecha " + TxFecha.Text,
                              "El usuario <b>" + item["Nombre"].ToString() + "</b> creó: <br> Notificación de Mantenimiento",
                               vCorreoEncargadoZona,
                               "/sites/agencias/pages/mantenimiento/aprobarNotificacion.aspx"
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
                       "Notificación de Mantenimiento Agencia",
                        "Buen día, se le notifica que se creó solicitud de mantenimiento, el encargado es " + item["Nombre"].ToString() + ", mantenimiento a agencia " + DDLCodigoAgencia.SelectedItem.Text + " para la fecha " + TxFecha.Text,
                          "El usuario <b>" + vDatos.Rows[0]["Nombre"].ToString() + "</b> creó: <br> Notificación de Mantenimiento de Agencia al que ha sido asignado como responsable.",
                            "",
                        "/login.spx"
                        );
                }
            }
            if (vDatosTecnicos.Rows.Count > 0)
            {
                foreach (DataRow itemT in vDatosTecnicos.Rows)
                {
                    vService.EnviarMensaje(itemT["correo"].ToString(),
                        typeBody.EnvioCorreo,
                        "Notificación de Mantenimiento Agencia",
                        "Buen día, se le notifica que se creó solicitud de mantenimiento, el encargado es " + vDatosTecnicoResponsable.Rows[0]["Nombre"].ToString() + ", mantenimiento a Agencia " + DDLCodigoAgencia.SelectedItem.Text + " para la fecha " + TxFecha.Text,
                          "El usuario <b>" + vDatos.Rows[0]["Nombre"].ToString() + "</b> creó: <br> Notificación de Mantenimiento de Agencia al que ha sido asignado como parte del equipo de trabajo",
                            "",
                        "/login.aspx"
                        );
                }
            }
            if (vDatosJefeAgencias.Rows.Count > 0)
            {
                foreach (DataRow item in vDatosJefeAgencias.Rows)
                {
                    //ENVIAR A JEFES DE AGENCIA
                    if (!item["CorreoJefe"].ToString().Trim().Equals(""))
                    {
                        vService.EnviarMensaje(item["CorreoJefe"].ToString(),
                            typeBody.EnvioCorreo,
                            "Notificación de Mantenimiento Agencia",
                                "Buen día, se le notifica que se creó solicitud de mantenimiento, el encargado es " + vDatosTecnicoResponsable.Rows[0]["Nombre"].ToString() + ", mantenimiento a Agencia " + DDLCodigoAgencia.SelectedItem.Text + " para la fecha " + TxFecha.Text,
                                  "Se le informa que dicho mantenimiento se hará en la agencia al que usted se encuentra asignado.",
                                   "",
                                   ""
                            );
                    }
                }
            }

        }

        protected void btnModalEnviarNotificacion_Click(object sender, EventArgs e)
        {
            try
            {
                String vQuery2 = "STEISP_AGENCIA_CreacionNotificacion 10, '" + TxHoraInicio.Text + "','" + TxHoraFin.Text + "','" + DDLNombreResponsable.SelectedValue + "','" + TxSysAid.Text + "','" + DDLCodigoAgencia.SelectedValue + "'";
                Int32 vInfo2 = vConexion.ejecutarSql(vQuery2);

                if (Session["AG_CN_CORREO_JEFE_AD_NOTI"] != null)
                {
                    DataTable vDatosJefesAgencia = (DataTable)Session["AG_CN_CORREO_JEFE_AD_NOTI"];
                    if (vDatosJefesAgencia.Rows.Count > 0)
                    {
                        for (int num = 0; num < vDatosJefesAgencia.Rows.Count; num++)
                        {
                            string vCorreo = vDatosJefesAgencia.Rows[num]["Correo"].ToString();
                            string vNombre = vDatosJefesAgencia.Rows[num]["Nombre"].ToString();
                            string vApellido = vDatosJefesAgencia.Rows[num]["Apellido"].ToString();

                            String vQuery4 = "STEISP_AGENCIA_CreacionNotificacion 8," + DDLCodigoAgencia.SelectedValue + "," + "'" + vCorreo + "','" + vNombre + "','" + vApellido + "'";
                            Int32 vInfo4 = vConexion.ejecutarSql(vQuery4);

                        }
                    }
                }
                if (Session["AG_CN_TECNICOS_PARTICIPANTES"] != null)
                {
                    DataTable vDatosTecnicosParticipantes = (DataTable)Session["AG_CN_TECNICOS_PARTICIPANTES"];
                    if (vDatosTecnicosParticipantes.Rows.Count > 0)
                    {
                        for (int num = 0; num < vDatosTecnicosParticipantes.Rows.Count; num++)
                        {
                            string idUsuario = vDatosTecnicosParticipantes.Rows[num]["idUsuario"].ToString();
                            string vCorreo = vDatosTecnicosParticipantes.Rows[num]["correo"].ToString();

                            String vQuery3 = "STEISP_AGENCIA_CreacionNotificacion 9,'" + DDLCodigoAgencia.SelectedValue + "','" + idUsuario + "','" + vCorreo + "'";
                            vConexion.ejecutarSql(vQuery3);
                        }
                    }
                }
                if (vInfo2 == 1)
                {
                    EnviarCorreo();
                    Mensaje("Notificación enviada con éxito.", WarningType.Success);
                    Limpiar();
                    BloquearCampos();
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
                }
                else
                {
                    Mensaje("No se pudo enviar la notificación, favor contactarse con el administrador del sistema", WarningType.Danger);
                }
            }

            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }

        }

        protected void btnModalCerrarNotificacion_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
        }

        private void BloquearCampos()
        {
            DDLCodigoAgencia.Enabled = false;
            TxSysAid.ReadOnly = true;
            TxHoraInicio.ReadOnly = true;
            TxHoraFin.ReadOnly = true;
            DDLNombreResponsable.Enabled = false;
            DDLNombreParticipantes.Enabled = false;
            //TxCorreo.ReadOnly = true;
            //LBAgregar.Enabled = false;
            UpdatePanel1.Update();
            txtbuscarJefeNotif.ReadOnly = true;
            //btnBuscarJefe.Enabled = true;

        }

        private void Limpiar()
        {
            DDLNombreResponsable.SelectedIndex = -1;
            TxIdentidadResponsable.Text = String.Empty;

            TextBox1.Text = String.Empty;
            DDLCodigoAgencia.SelectedIndex = -1;
            TxSysAid.Text = String.Empty;
            TxArea.Text = String.Empty;
            TxDepartamento.Text = String.Empty;
            TxMantEquipoComu.Text = String.Empty;
            TxDireccion.Text = String.Empty;
            TxTelefono.Text = String.Empty;
            TxHoraInicio.Text = String.Empty;
            TxHoraFin.Text = String.Empty;

            DDLNombreResponsable.SelectedIndex = -1;
            TxIdentidadResponsable.Text = String.Empty;

            DDLNombreParticipantes.SelectedIndex = -1;
            DDLCodigoAgencia.Items.Clear();

            Session["AG_CN_TECNICOS_PARTICIPANTES"] = null;
            Session["CORREOS"] = null;

            GVBusqueda.DataSource = null;
            GVBusqueda.DataBind();

            GVjefesAgencias.DataSource = null;
            GVjefesAgencias.DataBind();

            UpdatePanel1.Update();

            txtbuscarJefeNotif.Text = "";
            GVJefesAD.DataSource = null;
            GVJefesAD.DataBind();
            Session["AG_CN_TECNICOS_PARTICIPANTES"] = "";
            Session["AG_CN_CORREO_JEFE_AD_NOTI"] = "";
        }

        protected void BtnCancelarNotificacion_Click(object sender, EventArgs e)
        {
            Limpiar();
            Mensaje("Notificación cancelada con exito", WarningType.Info);
            BloquearCampos();
        }

        protected void btnBuscarJefe_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtbuscarJefeNotif.Text != "" || txtbuscarJefeNotif.Text != string.Empty)
                {

                    // Session["NotifJefeAgenciaATM"] = null;
                    classes.LdapService vService = new classes.LdapService();
                    DataTable vDatos = vService.GetDatosUsuario("adbancat.hn", txtbuscarJefeNotif.Text);


                    GVJefesAD.DataSource = vDatos;
                    GVJefesAD.DataBind();
                    Session["AG_CN_CORREO_JEFE_AD"] = vDatos;
                    UpdatePanel1.Update();

                }
            }
            catch (Exception Ex)
            {

                Mensaje(Ex.Message, WarningType.Danger);
            }

        }

        protected void GVJefesAD_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GVJefesAD.PageIndex = e.NewPageIndex;
                GVJefesAD.DataSource = (DataTable)Session["AG_CN_CORREO_JEFE_AD"];
                GVJefesAD.DataBind();
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        protected void GVJefesAD_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            string correoJefe = e.CommandArgument.ToString();

            if (e.CommandName == "correos")
            {
                try
                {
                    foreach (GridViewRow item in GVjefesAgencias.Rows)
                    {
                        if (item.Cells[1].Text.Equals(correoJefe))
                        {
                            //txtbuscarJefeNotif.Text = "";
                            //GVJefesAD.DataSource = null;
                            //GVJefesAD.DataBind();
                            throw new Exception("Ya existe jefe de agencia.");

                        }
                    }

                    DataTable vData = new DataTable();
                    DataTable vDatos = (DataTable)Session["AG_CN_CORREO_JEFE_AD_NOTI"];
                    DataTable vDatosJefes = (DataTable)Session["AG_CN_CORREO_JEFE_AD"];
                    string CorreoJefe = correoJefe;
                    String vNombre = "";
                    String vApellido = "";

                    DataRow[] result = vDatosJefes.Select("mail = '" + CorreoJefe + "'");
                    foreach (DataRow row in result)
                    {
                        if (row["mail"].ToString().Contains(CorreoJefe))
                        {
                            vNombre = row["givenName"].ToString();
                            vApellido = row["sn"].ToString();
                        }
                    }

                    vData.Columns.Add("Correo");
                    vData.Columns.Add("Nombre");
                    vData.Columns.Add("Apellido");
                    //vData.Columns.Add("Nombre");
                    if (vDatos == null)
                        vDatos = vData.Clone();
                    if (vDatos != null)
                    {
                        vDatos.Rows.Add(CorreoJefe, vNombre, vApellido);

                    }



                    GVjefesAgencias.DataSource = vDatos;
                    GVjefesAgencias.DataBind();
                    Session["AG_CN_CORREO_JEFE_AD_NOTI"] = vDatos;
                    UpdatePanel1.Update();
                    txtbuscarJefeNotif.Text = "";


                    //GVJefesAD.DataSource = null;
                    //GVJefesAD.DataBind();

                }
                catch (Exception Ex)
                {

                    // DLLTecnicoParticipante.SelectedValue = "0";
                    Mensaje(Ex.Message, WarningType.Danger);
                }
            }



        }

        protected void GVjefesAgencias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DataTable vDatos = (DataTable)Session["AG_CN_CORREO_JEFE_AD_NOTI"];
            if (e.CommandName == "eliminar")
            {
                String vCorreo = e.CommandArgument.ToString();
                if (Session["AG_CN_CORREO_JEFE_AD_NOTI"] != null)
                {

                    DataRow[] result = vDatos.Select("Correo = '" + vCorreo + "'");
                    foreach (DataRow row in result)
                    {
                        if (row["Correo"].ToString().Contains(vCorreo))
                            vDatos.Rows.Remove(row);
                    }
                }
            }
            GVjefesAgencias.DataSource = vDatos;
            GVjefesAgencias.DataBind();
            Session["AG_CN_CORREO_JEFE_AD_NOTI"] = vDatos;

        }

        protected void GVjefesAgencias_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GVjefesAgencias.PageIndex = e.NewPageIndex;
                GVjefesAgencias.DataSource = (DataTable)Session["AG_CN_CORREO_JEFE_AD_NOTI"];
                GVjefesAgencias.DataBind();
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }
    }
}
