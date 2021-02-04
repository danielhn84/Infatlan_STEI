using Infatlan_STEI_Agencias.classes;
using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace Infatlan_STEI_Agencias.pages
{
    public partial class reprogramarMantenimiento : System.Web.UI.Page
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
                String vQuery = "STEISP_AGENCIA_ReprogramarMantenimiento 1";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                GvMantPendientesReprogramar.DataSource = vDatos;
                GvMantPendientesReprogramar.DataBind();
                Session["AG_RM_MANTENIMIENTOS_PENDIENTES_REPROGRAMAR"] = vDatos;
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        private void validaciones()
        {
            if (TxNuevaFecha.Text.Equals(""))
                throw new Exception("Favor ingresar la nueva fecha del  mantenimiento preventivo de agencias.");
        }

        void EnviarCorreo()
        {
            SmtpService vService = new SmtpService();
            string vZonaAgencia = "";
            string vIDMantenimiento = Convert.ToString(Session["AG_RM_ID_MANTENIMIENTO"]);
            //string vLugar = Session["vLugar"].ToString();

            //string vQueryD = "[STEISP_AGENCIA_AprobarNotificacion] 9,'" + vIDMantenimiento + "'";
            //DataTable vDatosTecnicoResponsable = vConexion.obtenerDataTable(vQueryD);
            //string vQueryTecnicos = "[STEISP_AGENCIA_AprobarNotificacion] 10,'" + vIDMantenimiento + "'";
            //DataTable vDatosTecnicos = vConexion.obtenerDataTable(vQueryTecnicos);
            //string vQueryJefes = "[STEISP_AGENCIA_AprobarNotificacion] 11,'" + vIDMantenimiento + "'";
            //DataTable vDatosJefeAgencias = vConexion.obtenerDataTable(vQueryJefes);
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
                    if (Session["USUARIO"].ToString() == "eurrea" || Session["USUARIO"].ToString() == "emontoya" || Session["USUARIO"].ToString() == "jdgarcia" || Session["USUARIO"].ToString() == "acalderon")
                    {
                        vService.EnviarMensaje(ConfigurationManager.AppSettings["STEIMail"],
                            typeBody.EnvioCorreo,
                            "Notificación de Mantenimiento Agencia",
                            "Buen día, se le notifica que se reprogramó solicitud de mantenimiento  a Agencia " + TxLugar.Text + " para la fecha " + TxNuevaFecha.Text,
                              "El usuario <b>" + item["Nombre"].ToString() + "</b> reprogramó: <br> Notificación de Mantenimiento",
                               vCorreoEncargadoZona,
                               "/sites/agencias/pages/mantenimiento/creacionNotificacion.aspx"
                            );
                    }
                    else
                    {
                        //ENVIAR A JEFE
                        if (!item["correo"].ToString().Trim().Equals(""))
                        {
                            vService.EnviarMensaje(item["correo"].ToString(),
                            typeBody.EnvioCorreo,
                             "Notificación de Mantenimiento Agencia",
                            "Buen día, se le notifica que se reprogramó solicitud de mantenimiento  a Agencia " + TxLugar.Text + " para la fecha " + TxNuevaFecha.Text,
                              "El usuario <b>" + item["Nombre"].ToString() + "</b> reprogramó: <br> Notificación de Mantenimiento",
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
                            "Buen día, se le notifica que se reprogramó solicitud de mantenimiento  a Agencia " + TxLugar.Text + " para la fecha " + TxNuevaFecha.Text,
                              "El usuario <b>" + item["Nombre"].ToString() + "</b> reprogramó: <br> Notificación de Mantenimiento",
                                   vCorreoEncargadoZona,
                                   "/sites/agencias/pages/mantenimiento/creacionNotificacion.aspx"
                                );

                    }

                }
            }


        }

        protected void GvMantPendientesReprogramar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Reprogramar")
            {
                LimpiarModalReprogramarMantenimiento();
                string vIdMantenimientoReprogramar = e.CommandArgument.ToString();
                Session["AG_RM_ID_MANTENIMIENTO"] = vIdMantenimientoReprogramar;

                String vQuery = "STEISP_AGENCIA_ReprogramarMantenimiento 2," + vIdMantenimientoReprogramar;
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                foreach (DataRow item in vDatos.Rows)
                {

                    TxIdMant.Text = item["id_Mantenimiento"].ToString();
                    TxLugar.Text = item["Lugar1"].ToString();
                    TxFecha.Text = item["fecha"].ToString();
                    TxArea.Text = item["Area"].ToString();
                    TxMotivo.Text = item["motivoCancelacion"].ToString();
                    TxDetalle.Text = item["detalleCancelación"].ToString();

                    TituloModalReprogramar.Text = item["Lugar1"].ToString();

                }


                UpTituloReprogramar.Update();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModalReprogramarMantenimiento();", true);

            }
        }

        private void LimpiarModalReprogramarMantenimiento()
        {
            TxNuevaFecha.Text = string.Empty;
            UpdateModal.Visible = false;
        }

        protected void btnModalReprogramarMantenimiento_Click(object sender, EventArgs e)
        {
            try
            {
                validaciones();
                int vIDMantenimiento = Convert.ToInt32(Session["AG_RM_ID_MANTENIMIENTO"]);
                string vNuevaFecha = TxNuevaFecha.Text;
                String vFormato = "yyyy/MM/dd";
                String vFechaMant = Convert.ToDateTime(TxNuevaFecha.Text).ToString(vFormato);

                EnviarCorreo();
                String vQuery = "STEISP_AGENCIA_ReprogramarMantenimiento  3," + Session["AG_RM_ID_MANTENIMIENTO"] + ",'" + Session["USUARIO"] + "','" + vFechaMant + "'";
                Int32 vInfo = vConexion.ejecutarSql(vQuery);
                if (vInfo == 1)
                {
                    String vQuery2 = "STEISP_AGENCIA_ReprogramarMantenimiento  4," + Session["AG_RM_ID_MANTENIMIENTO"] + "";
                    Int32 vInfo2 = vConexion.ejecutarSql(vQuery2);
                    if (vInfo > 0)
                    {
                        Mensaje("Mantenimiento reprogramado con exito.", WarningType.Success);
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModalReprogramarMantenimiento();", true);
                    }
                }
                LimpiarModalReprogramarMantenimiento();
                cargarDatos();




            }
            catch (Exception ex)
            {
                LbMensajeModalErrorReprogramar.Text = ex.Message;
                DivAlerta.Visible = true;
                UpdateModal.Visible = true;
                UpdateModal.Update();

            }
        }

        protected void TxNuevaFecha_TextChanged(object sender, EventArgs e)
        {
            DivAlerta.Visible = false;
            UpdateModal.Update();
        }
    }
}