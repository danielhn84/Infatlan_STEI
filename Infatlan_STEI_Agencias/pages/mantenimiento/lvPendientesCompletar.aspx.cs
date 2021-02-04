using Infatlan_STEI_Agencias.classes;
using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Infatlan_STEI_Agencias.pages
{
    public partial class LvCompletar : System.Web.UI.Page
    {
        db vConexion = new db();

        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Convert.ToBoolean(Session["AUTH"]))
                {
                    cargarDatos();
                }
                else
                {
                    Response.Redirect("/login.aspx");
                }
            }
        }

        private void cargarDatos()
        {
            try
            {
                String vQuery = "STEISP_AGENCIA_CompletarListaVerificacion 1," + Session["USUARIO"];
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                GVListaVerificacion.DataSource = vDatos;
                GVListaVerificacion.DataBind();
                Session["AG_LvPC_LISTAS_PENDIENTES_TECNICO"] = vDatos;
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        void EnviarCorreoCancelar()
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

                    vService.EnviarMensaje(ConfigurationManager.AppSettings["STEIMail"],
                           typeBody.EnvioCorreo,
                           "Notificación de Mantenimiento Agencia",
                           "Buen día, se le notifica que se canceló solicitud de mantenimiento, el encargado es " + vDatosTecnicoResponsable.Rows[0]["Nombre"].ToString() + ", mantenimiento a Agencia " + Titulo.Text,
                             "El usuario <b>" + item["Nombre"].ToString() + "</b> canceló: <br> Notificación de Mantenimiento<br>Motivo: " + TxDetalle.Text,
                              vCorreoEncargadoZona,
                              "/sites/agencias/pages/mantenimiento/lvPendientesCompletar.aspx"
                           );
                    //ENVIAR A JEFE
                    if (!item["correo"].ToString().Trim().Equals(""))
                    {
                        vService.EnviarMensaje(item["correo"].ToString(),
                        typeBody.EnvioCorreo,
                        "Notificación de Mantenimiento Agencia",
                        "Buen día, se le notifica que se canceló solicitud de mantenimiento, el encargado es " + vDatosTecnicoResponsable.Rows[0]["Nombre"].ToString() + ", mantenimiento a Agencia " + Titulo.Text,
                        "El usuario <b>" + item["Nombre"].ToString() + "</b> Canceló: <br> Notificación de Mantenimiento<br>Motivo: " + TxDetalle.Text,
                        "",
                        "/sites/agencias/pages/mantenimiento/lvPendientesCompletar.aspx"
                        );

                        //vFlagEnvioSupervisor = true;
                    }
                    //ENVIAR A EDWIN


                }
            }
            if (vDatosTecnicoResponsable.Rows.Count > 0)
            {
                //foreach (DataRow item in vDatosTecnicoResponsable.Rows)
                //{
                //    //ENVIAR A RESPONSABLE
                //    vService.EnviarMensaje(item["Correo"].ToString(),
                //        typeBody.EnvioCorreo,
                //       "Notificación de Mantenimiento Agencia",
                //        "Buen día, se le notifica que se canceló solicitud de mantenimiento, el encargado es " + item["Nombre"].ToString() + ", mantenimiento a agencia " + Titulo.Text,
                //          "El usuario <b>" + vDatos.Rows[0]["Nombre"].ToString() + "</b> canceló: <br> Notificación de Mantenimiento de Agencia al que ha sido asignado como responsable.<br>Motivo: " + TxDetalle.Text,
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
                        "Notificación de Mantenimiento Agencia",
                        "Buen día, se le notifica que se canceló solicitud de mantenimiento, el encargado es " + vDatosTecnicoResponsable.Rows[0]["Nombre"].ToString() + ", mantenimiento a Agencia " + Titulo.Text,
                          "El usuario <b>" + vDatos.Rows[0]["Nombre"].ToString() + "</b> canceló: <br> Notificación de Mantenimiento de Agencia al que ha sido asignado como parte del equipo de trabajo.<br>Motivo: " + TxDetalle.Text,
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
                                "Buen día, se le notifica que se canceló solicitud de mantenimiento, el encargado es " + vDatosTecnicoResponsable.Rows[0]["Nombre"].ToString() + ", mantenimiento a Agencia " + Titulo.Text,
                                  "Se le informa que dicho mantenimiento se canceló en la agencia al que usted se encuentra asignado.<br>Motivo: " + TxDetalle.Text,
                                   "",
                                   ""
                            );
                    }
                }
            }

        }

        protected void GVListaVerificacion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Completar")
            {
                string vIdMantenimientoCompletar = e.CommandArgument.ToString();
                Session["AG_LvPC_ID_MANTENIMIENTO_LV_COMPLETAR"] = vIdMantenimientoCompletar;
                try
                {
                    //DATOS GENERALES
                    String vQuery = "STEISP_AGENCIA_CompletarListaVerificacion 2," + vIdMantenimientoCompletar;
                    DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                    Session["AG_LvPC_DATOS_GENERALES"] = vDatos;
                    string idTecnicoResponsable = vDatos.Rows[0]["idUsuario"].ToString();

                    //IDENTIDAD TECNICO RESPONSABLE
                    String vQuery1 = "STEISP_AGENCIA_CompletarListaVerificacion 3," + idTecnicoResponsable;
                    DataTable vDatos1 = vConexion.obtenerDataTable(vQuery1);
                    Session["AG_LvPC_TECNICO_RESPONSABLE"] = vDatos1;

                    //TECNICOS PARTICIPANTES
                    String vQuery2 = "STEISP_AGENCIA_CompletarListaVerificacion 4," + vIdMantenimientoCompletar;
                    DataTable vDatos2 = vConexion.obtenerDataTable(vQuery2);
                    Session["AG_LvPC_TECNICOS_PARTICIPANTES"] = vDatos2;

                    Response.Redirect("/sites/agencias/pages/mantenimiento/lvIndividual.aspx?ex=1");
                }
                catch (Exception ex)
                {
                    Mensaje(ex.Message, WarningType.Danger);
                }

            }
            else if (e.CommandName == "Cancelar")
            {
                string vIdMantenimientoCompletar = e.CommandArgument.ToString();
                Session["AG_LvPC_ID_MANTENIMIENTO_LV_COMPLETAR"] = vIdMantenimientoCompletar;

                String vQuery = "STEISP_AGENCIA_CompletarListaVerificacion 2," + vIdMantenimientoCompletar;
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                Session["AG_LvPC_USUARIO_RESPONSABLE"] = vDatos.Rows[0]["idUsuario"].ToString();

                TxIdMantenimiento.Text = vIdMantenimientoCompletar;
                Titulo.Text = vDatos.Rows[0]["Lugar"].ToString();
                UpdatePanel3.Update();
                limpiarModalCancelar();
                tecnicosResponsable();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModalCancelarLV();", true);
            }
        }

        private void validarModalCancelacion()
        {
            if (DDLMotivo.SelectedValue.Equals("0"))
                throw new Exception("Favor seleccione motivo que cancela mantenimiento.");

            if (DDLMotivo.SelectedValue.Equals("4") && DDLNombreResponsable.SelectedValue.Equals(""))
                throw new Exception("Favor seleccione el nuevo tecnico responsable de mantenimiento.");

            if (TxDetalle.Text == "" || TxDetalle.Text == string.Empty)
                throw new Exception("Favor ingrese motivo que cancela mantenimiento.");
        }

        protected void DDLMotivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLMotivo.SelectedValue.Equals("4"))
            {

                DDLNombreResponsable.Visible = true;
                Etiqueta.Visible = true;
            }
            else
            {
                DDLNombreResponsable.Visible = false;
                Etiqueta.Visible = false;
                funcionCambioTecnicoResponsable();
            }
        }

        protected void TxDetalle_TextChanged(object sender, EventArgs e)
        {
            funcionCambioTecnicoResponsable();
        }

        protected void DDLNombreResponsable_SelectedIndexChanged(object sender, EventArgs e)
        {
            funcionCambioTecnicoResponsable();
        }

        private void funcionCambioTecnicoResponsable()
        {
            LbMensajeModalErrorReprogramar.Text = "";
            UpdateModal.Visible = false;
            UpdateModal.Update();
        }

        private void limpiarModalCancelar()
        {
            DDLMotivo.SelectedIndex = -1;
            DDLNombreResponsable.SelectedIndex = -1;
            TxDetalle.Text = String.Empty;
            LbMensajeModalErrorReprogramar.Text = "";
            UpdateModal.Visible = false;
            UpdateModal.Update();
            DDLNombreResponsable.Visible = false;
            Etiqueta.Visible = false;
        }

        private void tecnicosResponsable()
        {
            try
            {
                DDLNombreResponsable.Items.Clear();
                String vQuery = "STEISP_AGENCIA_CreacionNotificacion 5";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                DDLNombreResponsable.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });

                if (vDatos.Rows.Count > 0)
                {
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLNombreResponsable.Items.Add(new ListItem { Value = item["idUsuario"].ToString(), Text = item["nombre"].ToString() + "  " + item["apellidos"].ToString() });

                    }
                }

            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void BtnCancelarLV_Click(object sender, EventArgs e)
        {
            try
            {
                validarModalCancelacion();

                string idTecnicoResponsable = String.Empty;
                string idEstadoMantenimiento = String.Empty;
                if (DDLMotivo.SelectedValue.Equals("4"))
                {
                    idTecnicoResponsable = DDLNombreResponsable.SelectedValue;
                    idEstadoMantenimiento = "7";
                }
                else
                {
                    idTecnicoResponsable = Session["AG_LvPC_USUARIO_RESPONSABLE"].ToString();
                    idEstadoMantenimiento = "5";
                }

                EnviarCorreoCancelar();
                String vQuery = "STEISP_AGENCIA_CompletarListaVerificacion 11," + Session["AG_LvPC_ID_MANTENIMIENTO_LV_COMPLETAR"] +
                               "," + DDLMotivo.SelectedValue +
                               ",'" + TxDetalle.Text +
                               "'," + idEstadoMantenimiento +
                                "," + idTecnicoResponsable;
                Int32 vInfo = vConexion.ejecutarSql(vQuery);


                if (vInfo == 1)
                {
                    Mensaje("Lista de verificación cancelada con exito. ", WarningType.Success);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModalCancelarLV();", true);
                    cargarDatos();
                }


            }
            catch (Exception ex)
            {

                LbMensajeModalErrorReprogramar.Text = ex.Message;
                UpdateModal.Visible = true;
                UpdateModal.Update();


            }
        }

        protected void TxBuscarAgencia_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cargarDatos();
                String vBusqueda = TxBuscarAgencia.Text;
                DataTable vDatos = (DataTable)Session["AG_LvPC_LISTAS_PENDIENTES_TECNICO"];
                if (vBusqueda.Equals(""))
                {
                    GVListaVerificacion.DataSource = vDatos;
                    GVListaVerificacion.DataBind();
                    UPListaVerificacion.Update();
                }
                else
                {
                    EnumerableRowCollection<DataRow> filtered = vDatos.AsEnumerable()
                        .Where(r => r.Field<String>("Lugar").Contains(vBusqueda));

                    Boolean isNumeric = int.TryParse(vBusqueda, out int n);

                    if (isNumeric)
                    {
                        if (filtered.Count() == 0)
                        {
                            filtered = vDatos.AsEnumerable().Where(r =>
                                Convert.ToInt32(r["id_Mantenimiento"]) == Convert.ToInt32(vBusqueda));
                        }
                    }

                    DataTable vDatosFiltrados = new DataTable();
                    vDatosFiltrados.Columns.Add("id_Mantenimiento");
                    vDatosFiltrados.Columns.Add("fecha");
                    vDatosFiltrados.Columns.Add("Cod_Agencia");
                    vDatosFiltrados.Columns.Add("Lugar");
                    vDatosFiltrados.Columns.Add("Area");
                    vDatosFiltrados.Columns.Add("sysAid");
                    vDatosFiltrados.Columns.Add("Responsable");
                    vDatosFiltrados.Columns.Add("idUsuario");

                    foreach (DataRow item in filtered)
                    {
                        vDatosFiltrados.Rows.Add(
                            item["id_Mantenimiento"].ToString(),
                            item["fecha"].ToString(),
                            item["Cod_Agencia"].ToString(),
                            item["Lugar"].ToString(),
                            item["Area"].ToString(),
                            item["sysAid"].ToString(),
                            item["Responsable"].ToString(),
                            item["idUsuario"].ToString()

                            );
                    }

                    GVListaVerificacion.DataSource = vDatosFiltrados;
                    GVListaVerificacion.DataBind();
                    Session["AG_LvPC_LISTAS_PENDIENTES_TECNICO"] = vDatosFiltrados;
                    UPListaVerificacion.Update();
                }

            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void GVListaVerificacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GVListaVerificacion.PageIndex = e.NewPageIndex;
                GVListaVerificacion.DataSource = (DataTable)Session["AG_LvPC_LISTAS_PENDIENTES_TECNICO"];
                GVListaVerificacion.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
    }
}
