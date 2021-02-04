using Infatlan_STEI_ATM.clases;
using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Infatlan_STEI_ATM.pages.correctivo
{
    public partial class mantCorrectivoNotificacion : System.Web.UI.Page
    {
        bd vConexion = new bd();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["NOTIFCORRECTIVO"] = null;
            if (!Page.IsPostBack)
            {
                if (Convert.ToBoolean(Session["AUTH"]))
                {
                    cargarData();
                    llenarForm();
                }
                else
                {
                    Response.Redirect("/login.aspx");
                }
            }
        }

        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        void cargarData()
        {
            try
            {
                if (HttpContext.Current.Session["NOTIFCORRECTIVO"] == null)
                {

                    String vQuery2 = "STEISP_AGENCIA_CreacionNotificacion 5";
                    DataTable vDatos2 = vConexion.ObtenerTabla(vQuery2);
                    //DLLtecResponsable.Items.Clear();
                    DLLtecResponsable.Items.Add(new ListItem { Value = "0", Text = "Seleccione técnico responsable..." });
                    foreach (DataRow item in vDatos2.Rows)
                    {
                        DLLtecResponsable.Items.Add(new ListItem { Value = item["idUsuario"].ToString(), Text = item["nombre"].ToString() + " " + item["apellidos"].ToString() });
                    }

                    Session["NOTIFCORRECTIVO"] = "1";
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        void llenarForm()
        {
            string codATM = Request.QueryString["cod"];

            DataTable vDatos = new DataTable();
            String vQuery = "STEISP_ATM_Generales 49,'" + codATM + "'";
            vDatos = vConexion.ObtenerTabla(vQuery);
            foreach (DataRow item in vDatos.Rows)
            {
                txtATM.Text = item["Codigo"].ToString() + " - " + item["NomATM"].ToString();
                txtdireccion.Text = item["Direccion"].ToString();
                txtipNotif.Text = item["IP"].ToString();
                txtUbicacionATM.Text = item["Ubicacion"].ToString();
                txtsucursalNotif.Text = item["Sucursal"].ToString();
                txtzonaNotif.Text = item["Zona"].ToString();


                DIVBuscarJefes.Visible = true;
            }
        }

        void LimpiarNotificacion()
        {
            Session["ATM_BUSCAR_JEFE_CORRECTIVO"] = null;
            Session["NotifJefeAgenciaATMCorrectivo"] = null;
            GVjefesAgencias.DataSource = null;
            GVjefesAgencias.DataBind();
            GVJefesAD.DataSource = null;
            GVJefesAD.DataBind();
            Session["NOTIFCORRECTIVO"] = null;
            DLLtecResponsable.Items.Clear();
            txtHrInicioMant.Text = string.Empty;
            txtHrFinMant.Text = string.Empty;
            txtATM.Text = string.Empty;
            txtsysaid.Text = string.Empty;
            txtUbicacionATM.Text = string.Empty;
            txtdireccion.Text = string.Empty;
            txtsucursalNotif.Text = string.Empty;
            txtipNotif.Text = string.Empty;
            txtzonaNotif.Text = string.Empty;
            txtidentidadTecResponsable.Text = string.Empty;
        }

        void validacionesNotificacion()
        {
            if (DLLtecResponsable.SelectedValue == "0")
                throw new Exception("Favor seleccione técnico responsable.");
            if (txtHrFinMant.Text == "" || txtHrFinMant.Text == string.Empty)
                throw new Exception("Favor ingrese la hora en la que termino mantenimiento.");
            if (txtHrInicioMant.Text == "" || txtHrInicioMant.Text == string.Empty)
                throw new Exception("Favor ingrese la hora en la inicio mantenimiento.");
            if (txtsysaid.Text == "" || txtsysaid.Text == string.Empty)
                throw new Exception("Favor ingrese sysaid.");
            if (txtUbicacionATM.Text == "Agencia")
            {
                if (Session["NotifJefeAgenciaATMCorrectivo"] == null)
                    throw new Exception("favor seleccione los jefes de agencia.");
            }


        }

        void EnviarCorreo()
        {
            string id = Request.QueryString["id"];
            string tipo = Request.QueryString["tipo"];
            string codATM = Request.QueryString["cod"];
            Boolean vFlagEnvio = false;
            String vDestino = "";
            SmtpService vService = new SmtpService();

            //string vCorreoEncargadoZona = "";
            //if (Convert.ToString(Session["IDZona"]) == "1")
            //    vCorreoEncargadoZona = "emontoya@bancatlan.hn";
            //if (Convert.ToString(Session["IDZona"]) == "2")
            //    vCorreoEncargadoZona = "jdgarcia@bancatlan.hn";
            //if (Convert.ToString(Session["IDZona"]) == "3")
            //    vCorreoEncargadoZona = "acalderon@bancatlan.hn";


            string vQueryD = "STEISP_ATM_Generales 33,'" + DLLtecResponsable.SelectedValue + "'";
            DataTable vDatosTecnicoResponsable = vConexion.ObtenerTabla(vQueryD);
            DataTable vDatos = (DataTable)Session["AUTHCLASS"];
            string vQueryJefes = "[STEISP_ATM_GeneralesCorrectivo] 13,'" + codATM + "','" + txtsysaid.Text + "'";
            DataTable vDatosJefeAgencias = vConexion.ObtenerTabla(vQueryJefes);

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
                        "Buen día, se le notifica que se creó una solicitud de mantenimiento correctivo, el encargado es " + vDatosTecnicoResponsable.Rows[0]["nombre"].ToString() + ", mantenimiento al ATM " + txtATM.Text,
                        "El usuario <b>" + item["Nombre"].ToString() + "</b> creó: <br> Notificación de Mantenimiento Correctivo",
                        "",
                        "/sites/ATM/pages/correctivo/notificarCorrectivo.aspx"
                        );

                        //vFlagEnvioSupervisor = true;
                    }
                    //ENVIAR A EDWIN
                    //string vNombre = "EDWIN ALBERTO URREA PENA";
                    vService.EnviarMensaje(ConfigurationManager.AppSettings["STEIMail"],
                            typeBody.ATM,
                            "Notificación de Mantenimiento correctivo ATM",
                            "Buen día, se le notifica que se creó solicitud de mantenimiento correctivo, el encargado es " + vDatosTecnicoResponsable.Rows[0]["nombre"].ToString() + ", mantenimiento al ATM " + txtATM.Text,
                              "El usuario <b>" + item["Nombre"].ToString() + "</b> creó: <br> Notificación de Mantenimiento",
                               "",//vCorreoEncargadoZona,
                               "/sites/ATM/pages/correctivo/notificarCorrectivo.aspx"
                            );

                    //PRSONAL ENCARGADO DE ATM
                    String vKioskos = "unidadatmkiosco@bancatlan.hn";
                    vService.EnviarMensaje(vKioskos,
                           typeBody.ATM,
                           "Notificación de Mantenimiento correctivo ATM",
                           "Buen día, se le notifica que se ha creado una solicitud de mantenimiento correctivo, el encargado es " + vDatosTecnicoResponsable.Rows[0]["nombre"].ToString() + ", mantenimiento al ATM " + txtATM.Text,
                             "El usuario <b>" + item["Nombre"].ToString() + "</b> creó: <br> Notificación de Mantenimiento",
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
                        "Buen día, se le notifica que se creó solicitud de mantenimientocorrectivo, el encargado es " + item["nombre"].ToString() + ", mantenimiento al ATM " + txtATM.Text,
                          "El usuario <b>" + vDatos.Rows[0]["Nombre"].ToString() + "</b> creó: <br> Notificación de Mantenimiento de ATM al que ha sido asignado como responsable.",
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
                                "Buen día, se le notifica que se aprobó solicitud de mantenimiento correctivo, el encargado es " + vDatosTecnicoResponsable.Rows[0]["nombre"].ToString() + ", mantenimiento al ATM " + txtATM.Text,
                                  "Solicitud de mantenimiento correctivo a ATM.",
                                   "",
                                   ""
                            );
                    }
                }
            }


        }

        protected void DLLtecResponsable_TextChanged(object sender, EventArgs e)
        {
            try
            {
                String vQuery = "STEISP_AGENCIA_CreacionNotificacion 6, " + DLLtecResponsable.SelectedValue;
                DataTable vDatos = vConexion.ObtenerTabla(vQuery);
                txtidentidadTecResponsable.Text = vDatos.Rows[0]["identidad"].ToString();
                //Session["ATM_Notif_emailTecnicoResponsable"] = vDatos.Rows[0]["correo"].ToString();

            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void btnEnviarNotificacion_Click(object sender, EventArgs e)
        {
            try
            {


                validacionesNotificacion();
                TimeSpan horainicio = TimeSpan.Parse(txtHrInicioMant.Text);
                TimeSpan horafinal = TimeSpan.Parse(txtHrFinMant.Text);
                Session["resta"] = horafinal - horainicio;
                lbHrMantenimiento.Text = Session["resta"].ToString() + " horas";
                lbTecnicoResp.Text = DLLtecResponsable.SelectedItem.Text;
                lbcodATM.Text = txtATM.Text;
                lbsucursalATM.Text = txtsucursalNotif.Text;
                //lbTecnicoResp.Text = DLLtecResponsable.SelectedItem.Text;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception ex)
            {

                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void btnModalCerrarNotificacion_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
        }

        void usuariosJefeAgentes()
        {
            try
            {
                string codATM = Request.QueryString["cod"];
                DataTable vDatos = (DataTable)Session["NotifJefeAgenciaATMCorrectivo"];
                if (vDatos != null)
                {
                    for (int i = 0; i < vDatos.Rows.Count; i++)
                    {
                        string correos = vDatos.Rows[i]["Correo"].ToString();
                        string vNombre = vDatos.Rows[i]["Nombre"].ToString();
                        string vApellido = vDatos.Rows[i]["Apellido"].ToString();
                        string vQuery = "STEISP_ATM_NotificacionCorrectivo 2, '" + codATM + "','" + correos + "','" + vNombre + "','" + vApellido + "','" + txtsysaid.Text + "'";
                        vConexion.ejecutarSQL(vQuery);
                    }
                }
            }
            catch (Exception Ex)
            {
                throw;
            }
        }

        protected void btnModalEnviarNotificacion_Click(object sender, EventArgs e)
        {
            try
            {
                string codATM = Request.QueryString["cod"];
                string vQuery = "STEISP_ATM_NotificacionCorrectivo 1, '" + codATM + "','" + txtHrInicioMant.Text + "', '" + txtHrFinMant.Text + "'," +
                                "'" + DLLtecResponsable.SelectedValue + "','" + txtsysaid.Text + "','" + Session["USUARIO"].ToString() + "'";
                Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                if (vInfo == 1)
                {
                    usuariosJefeAgentes();
                    EnviarCorreo();
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
                    LimpiarNotificacion();
                    Response.Redirect("notificarCorrectivo.aspx");
                }
            }
            catch (Exception Ex)
            {

                throw;
            }

        }

        protected void btnBuscarJefe_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtbuscarJefeNotif.Text != "" || txtbuscarJefeNotif.Text != string.Empty)
                {
                    // Session["NotifJefeAgenciaATM"] = null;
                    clases.LdapService vService = new clases.LdapService();
                    DataTable vDatos = vService.GetDatosUsuario("adbancat.hn", txtbuscarJefeNotif.Text);


                    GVJefesAD.DataSource = vDatos;
                    GVJefesAD.DataBind();
                    Session["ATM_BUSCAR_JEFE_CORRECTIVO"] = vDatos;
                    UpdatePanel2.Update();
                }
            }
            catch (Exception)
            {

                throw;
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
                            throw new Exception("Ya existe jefe de agencia.");

                        }
                    }

                    DataTable vData = new DataTable();
                    DataTable vDatos = (DataTable)Session["NotifJefeAgenciaATMCorrectivo"];
                    DataTable vDatosJefes = (DataTable)Session["ATM_BUSCAR_JEFE_CORRECTIVO"];
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
                    Session["NotifJefeAgenciaATMCorrectivo"] = vDatos;
                }
                catch (Exception Ex)
                {

                    // DLLTecnicoParticipante.SelectedValue = "0";
                    Mensaje(Ex.Message, WarningType.Danger);
                }
            }
        }

        protected void GVJefesAD_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GVJefesAD.PageIndex = e.NewPageIndex;
                GVJefesAD.DataSource = (DataTable)Session["ATM_BUSCAR_JEFE_CORRECTIVO"];
                GVJefesAD.DataBind();
            }
            catch (Exception Ex)
            {

            }
        }

        protected void GVjefesAgencias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DataTable vDatos = (DataTable)Session["NotifJefeAgenciaATMCorrectivo"];
            if (e.CommandName == "eliminar")
            {
                String vCorreo = e.CommandArgument.ToString();
                if (Session["NotifJefeAgenciaATMCorrectivo"] != null)
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
            Session["NotifJefeAgenciaATMCorrectivo"] = vDatos;
        }
    }
}