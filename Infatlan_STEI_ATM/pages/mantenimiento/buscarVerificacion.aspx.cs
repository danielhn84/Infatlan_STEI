using Infatlan_STEI_ATM.clases;
using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Infatlan_STEI_ATM.pages.mantenimiento
{
    public partial class buscarVerificacion : System.Web.UI.Page
    {
        bd vConexion = new bd();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["ModalVerif"] = null;
            if (!Page.IsPostBack)
            {
                if (Convert.ToBoolean(Session["AUTH"]))
                {
                    cargarData();
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

        void limpiarModalVerificacion()
        {
            txtModalATM.Text = string.Empty;
            DDLModalMotivo.SelectedValue = "0";
            DDLModalcambioPor.SelectedValue = "0";
            DDLModalNewTecnico.SelectedValue = "0";
            txtdetalleCancela.Text = string.Empty;
        }
        void cargarData()
        {

            try
            {
                DataTable vDatos = new DataTable();
                vDatos = vConexion.ObtenerTabla("STEISP_ATM_VERIFICACION 1, '" + Session["USUARIO"].ToString() + "','" + Session["COD_VERIFMANTE_ATM"] + "'");
                GVBusqueda.DataSource = vDatos;
                GVBusqueda.DataBind();
                Session["ATM_VERIF_CARGAR"] = vDatos;
                //Session["UPDATEATM"] = 1;

            }
            catch (Exception Ex)
            {

            }

            if (HttpContext.Current.Session["ModalVerif"] == null)
            {
                try
                {
                    DDLModalNewTecnico.Items.Clear();
                    String vQuery = "STEISP_AGENCIA_CreacionNotificacion 5";
                    DataTable vDatos = vConexion.ObtenerTabla(vQuery);
                    DDLModalNewTecnico.Items.Add(new ListItem { Value = "0", Text = "Seleccione nuevo técnico..." });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLModalNewTecnico.Items.Add(new ListItem { Value = item["idUsuario"].ToString(), Text = item["nombre"].ToString() });
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }

                try
                {

                    String vQuery = "STEISP_ATM_Generales 20,1";
                    DataTable vDatos = vConexion.ObtenerTabla(vQuery);
                    DDLModalMotivo.Items.Add(new ListItem { Value = "0", Text = "Seleccione motivo..." });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLModalMotivo.Items.Add(new ListItem { Value = item["idCancelMant"].ToString(), Text = item["nombreCancelar"].ToString() });

                    }
                }
                catch (Exception ex)
                {
                    throw;
                }

                try
                {

                    String vQuery = "STEISP_ATM_Generales 21,1";
                    DataTable vDatos = vConexion.ObtenerTabla(vQuery);
                    DDLModalcambioPor.Items.Add(new ListItem { Value = "0", Text = "Cambio realizado por..." });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLModalcambioPor.Items.Add(new ListItem { Value = item["id"].ToString(), Text = item["nombre"].ToString() });

                    }
                }
                catch (Exception ex)
                {
                    throw;
                }


                Session["ModalVerif"] = 1;
            }

        }
        protected void TxBuscarTecnicoATM_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cargarData();

                String vBusqueda = TxBuscarTecnicoATM.Text;
                DataTable vDatos = (DataTable)Session["ATM_VERIF_CARGAR"];

                if (vBusqueda.Equals(""))
                {
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                    UpdateGridView.Update();
                    //cargarData();
                }
                else
                {
                    EnumerableRowCollection<DataRow> filtered = vDatos.AsEnumerable()
                        .Where(r => r.Field<String>("NomATM").Contains(vBusqueda));

                    DataTable vDatosFiltrados = new DataTable();
                    vDatosFiltrados.Columns.Add("ID");
                    vDatosFiltrados.Columns.Add("Codigo");
                    vDatosFiltrados.Columns.Add("NomATM");
                    vDatosFiltrados.Columns.Add("Ubicacion");
                    vDatosFiltrados.Columns.Add("Sucursal");
                    vDatosFiltrados.Columns.Add("Tecnico");
                    foreach (DataRow item in filtered)
                    {
                        vDatosFiltrados.Rows.Add(
                            item["ID"].ToString(),
                            item["Codigo"].ToString(),
                            item["NomATM"].ToString(),
                            item["Ubicacion"].ToString(),
                            item["Sucursal"].ToString(),
                            item["Tecnico"].ToString()
                            );
                    }

                    GVBusqueda.DataSource = vDatosFiltrados;
                    GVBusqueda.DataBind();
                    Session["ATM_VERIF_CARGAR"] = vDatosFiltrados;
                    UpdateGridView.Update();
                }


            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        protected void GVBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GVBusqueda.PageIndex = e.NewPageIndex;
                GVBusqueda.DataSource = (DataTable)Session["ATM_VERIF_CARGAR"];
                GVBusqueda.DataBind();
            }
            catch (Exception Ex)
            {

            }
        }

        void CorreoCancelar()
        {
            SmtpService vService = new SmtpService();

            string vCorreoEncargadoZona = "";
            if (Convert.ToString(Session["IDZona"]) == "1")
                vCorreoEncargadoZona = "emontoya@bancatlan.hn";
            if (Convert.ToString(Session["IDZona"]) == "2")
                vCorreoEncargadoZona = "jdgarcia@bancatlan.hn";
            if (Convert.ToString(Session["IDZona"]) == "3")
                vCorreoEncargadoZona = "acalderon@bancatlan.hn";

            string vQueryD = "STEISP_ATM_Generales 33,'" + Session["ATM_USU_RESPONSABLE_MANT"] + "'";
            DataTable vDatosTecnicoResponsable = vConexion.ObtenerTabla(vQueryD);
            DataTable vDatos = (DataTable)Session["AUTHCLASS"];
            string vQueryTecnicos = "STEISP_ATM_Generales 39,'" + Session["ATM_ID_CANCELAR_VERIF_MODAL"] + "'";
            DataTable vDatosTecnicos = vConexion.ObtenerTabla(vQueryTecnicos);
            string vQueryJefes = "STEISP_ATM_Generales 38,'" + Session["ATM_ID_CANCELAR_VERIF_MODAL"] + "'";
            DataTable vDatosJefeAgencias = vConexion.ObtenerTabla(vQueryJefes);

            if (DDLModalMotivo.SelectedValue == "5")
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
                            "Buen día, se le notifica que se ha sustituido encargado de mantenimiento, el encargado ahora es " + DDLModalNewTecnico.SelectedItem.Text + ", mantenimiento al ATM " + txtModalATM.Text,
                            "El usuario <b>" + item["Nombre"].ToString() + "</b> sustituyó técnico responsable de mantenimiento ATM",
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
                                "Buen día, se le notifica que se ha sustituido encargado de mantenimiento, el encargado ahora es " + DDLModalNewTecnico.SelectedItem.Text + ", mantenimiento al ATM " + txtModalATM.Text,
                                  "El usuario <b>" + item["Nombre"].ToString() + "</b> sustituyó técnico responsable de mantenimiento ATM",
                                  vCorreoEncargadoZona,
                                  "/sites/ATM/pages/reprogramar/buscarReprogramar.aspx"
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
                            "Buen día, se le notifica que se ha sustituído el encargado de mantenimiento, ahora es " + DDLModalNewTecnico.SelectedItem.Text + ", mantenimiento al ATM " + txtModalATM.Text,
                              "El usuario <b>" + vDatos.Rows[0]["Nombre"].ToString() + "</b> sustituyó técnico responsable de mantenimiento ATM al que ha sido asignado como parte del equipo de trabajo.",
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
                        if (!item["correoJefe"].ToString().Trim().Equals(""))
                        {
                            vService.EnviarMensaje(item["correoJefe"].ToString(),
                                typeBody.ATM,
                                "Notificación de Mantenimiento ATM",
                                "Buen día, se le notifica que se ha sustituído el encargado de mantenimiento, ahora es " + DDLModalNewTecnico.SelectedItem.Text + ", mantenimiento al ATM " + txtModalATM.Text,
                                "Se le informa que dicho mantenimiento se haría en la agencia al que usted se encuentra asignado.",
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
                        //ENVIAR A JEFE
                        if (!item["correo"].ToString().Trim().Equals(""))
                        {
                            vService.EnviarMensaje(
                            item["correo"].ToString(),
                            typeBody.ATM,
                            "Notificación de Mantenimiento ATM",
                            "Buen día, se le notifica que se ha cancelado solicitud de mantenimiento, el encargado es " + Session["ATM_RESPONSABLE"].ToString() + ", mantenimiento al ATM " + txtModalATM.Text,
                            "El usuario <b>" + item["Nombre"].ToString() + "</b> canceló: <br> Notificación de Mantenimiento",
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
                                "Buen día, se le notifica que se ha cancelado solicitud de mantenimiento, el encargado es " + Session["ATM_RESPONSABLE"].ToString() + ", mantenimiento al ATM " + txtModalATM.Text,
                                  "El usuario <b>" + item["Nombre"].ToString() + "</b> canceló: <br> Notificación de Mantenimiento",
                                  vCorreoEncargadoZona,
                                  "/sites/ATM/pages/reprogramar/buscarReprogramar.aspx"
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
                            "Buen día, se le notifica que se ha cancelado solicitud de mantenimiento, el encargado es " + Session["ATM_RESPONSABLE"].ToString() + ", mantenimiento al ATM " + txtModalATM.Text,
                              "El usuario <b>" + vDatos.Rows[0]["Nombre"].ToString() + "</b> canceló: <br> Notificación de Mantenimiento de ATM al que ha sido asignado como parte del equipo de trabajo.",
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
                        if (!item["correoJefe"].ToString().Trim().Equals(""))
                        {
                            vService.EnviarMensaje(item["correoJefe"].ToString(),
                                typeBody.ATM,
                                "Notificación de Mantenimiento ATM",
                                "Buen día, se le notifica que se ha cancelado solicitud de mantenimiento, el encargado es " + Session["ATM_RESPONSABLE"].ToString() + ", mantenimiento al ATM " + txtModalATM.Text,
                                "Se le informa que dicho mantenimiento se haría en la agencia al que usted se encuentra asignado.",
                                "",
                                ""
                                );
                        }
                    }
                }
            }
        }
        protected void btnMantSinRealizar_Click(object sender, EventArgs e)
        {

            try
            {
                if (txtdetalleCancela.Text == "" || DDLModalMotivo.SelectedValue == "0" || DDLModalcambioPor.SelectedValue == "0")

                {
                    H5Alerta.Visible = true;
                    txtAlerta2.Visible = true;
                }
                else
                {
                    if (DDLModalMotivo.SelectedValue == "5")
                    {
                        try
                        {
                            string vQuery = "STEISP_ATM_CancelarVerificacion 2, '" + Session["ATM_ID_CANCELAR_VERIF_MODAL"] + "','" + DDLModalMotivo.SelectedValue + "','" + DDLModalcambioPor.SelectedValue + "','" + Session["USUARIO"].ToString() + "', '" + DDLModalNewTecnico.SelectedValue + "','" + txtdetalleCancela.Text + "'";
                            Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                            if (vInfo == 1)
                            {
                                CorreoCancelar();
                                H5Alerta.Visible = false;
                                txtAlerta2.Visible = false;
                                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
                                Mensaje("Verificación cancelada con éxito", WarningType.Success);
                                UpdateGridView.Update();
                                limpiarModalVerificacion();
                                cargarData();
                            }
                            else
                            {
                                H5Alerta.InnerText = "No se pudo realizar la acción";
                                H5Alerta.Visible = true;
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
                            string vQuery = "STEISP_ATM_CancelarVerificacion 1, '" + Session["ATM_ID_CANCELAR_VERIF_MODAL"] + "','" + DDLModalMotivo.SelectedValue + "','" + DDLModalcambioPor.SelectedValue + "','" + Session["USUARIO"].ToString() + "', '" + DDLModalNewTecnico.SelectedValue + "','" + txtdetalleCancela.Text + "'";
                            Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                            if (vInfo != 0)
                            {
                                H5Alerta.Visible = false;
                                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
                                Mensaje("Verificación cancelada con éxito", WarningType.Success);
                                // CorreoCancelar();
                                UpdateGridView.Update();
                                limpiarModalVerificacion();
                                cargarData();
                            }
                            else
                            {
                                H5Alerta.InnerText = "No se pudo realizar la acción";
                                H5Alerta.Visible = true;
                            }
                        }
                        catch (Exception Ex)
                        {
                            throw;
                        }
                    }
                    H5Alerta.Visible = false;
                    txtAlerta2.Visible = false;

                }
            }
            catch (Exception EX)
            {
                Mensaje(EX.Message, WarningType.Danger);
            }
        }

        protected void DDLModalMotivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDLModalNewTecnico.SelectedValue = "0";
            if (DDLModalMotivo.SelectedValue == "5")
            {
                lbnewTecnico.Visible = true;
                DDLModalNewTecnico.Visible = true;
            }
            else
            {
                lbnewTecnico.Visible = false;
                DDLModalNewTecnico.Visible = false;
            }
        }

        protected void DDLModalNewTecnico_TextChanged(object sender, EventArgs e)
        {
            try
            {
                String vQuery = "STEISP_AGENCIA_CreacionNotificacion 6, " + DDLModalNewTecnico.SelectedValue;
                DataTable vDatos = vConexion.ObtenerTabla(vQuery);
                Session["ATM_Notif_emailTecnicoResponsable"] = vDatos.Rows[0]["correo"].ToString();
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            lbnewTecnico.Visible = false;
            DDLModalNewTecnico.Visible = false;
            limpiarModalVerificacion();
            H5Alerta.Visible = false;
            txtAlerta2.Visible = false;
            try
            {
                DataTable vDataaaa = (DataTable)Session["ATM_VERIF_CARGAR"];
                string codVerif = e.CommandArgument.ToString();
                Session["ATM_RECHAZADO_VER"] = "0";
                if (e.CommandName == "Aprobar")
                {
                    try
                    {
                        DataTable vDatos = new DataTable();
                        vDatos = vConexion.ObtenerTabla("STEISP_ATM_VERIFICACION 2,'" + codVerif + "'");
                        //vDatos = vConexion.ObtenerTabla(vQuery);
                        foreach (DataRow item in vDatos.Rows)
                        {

                            Session["ATM_CODATM_VERIF_CREAR"] = item["Codigo"].ToString();
                            Session["ATM_NOMATM_VERIF_CREAR"] = item["NomATM"].ToString();
                            Session["ATM_DIRECCION_VERIF_CREAR"] = item["Direccion"].ToString();
                            Session["ATM_IP_VERIF_CREAR"] = item["IP"].ToString();
                            Session["ATM_PUERTOATM_VERIF_CREAR"] = item["Puerto"].ToString();
                            Session["ATM_TECLADO_VERIF_CREAR"] = item["Teclado"].ToString();
                            Session["ATM_PROCESADOR_VERIF_CREAR"] = item["Procesador"].ToString();
                            Session["ATM_TIPOCARGA_VERIF_CREAR"] = item["TipoCarga"].ToString();
                            Session["ATM_MARCA_VERIF_CREAR"] = item["Marca"].ToString();
                            Session["ATM_SERIEDISCO_VERIF_CREAR"] = item["SerieDisco"].ToString();
                            Session["ATM_SERIEATM_VERIF_CREAR"] = item["SerieATM"].ToString();
                            Session["ATM_CAPACIDADDISCO_VERIF_CREAR"] = item["CapacidadDisco"].ToString();
                            Session["ATM_INVENTARIO_VERIF_CREAR"] = item["Inventario"].ToString();
                            Session["ATM_RAM_VERIF_CREAR"] = item["Ram"].ToString();
                            Session["ATM_LATITUD_VERIF_CREAR"] = item["Latitud"].ToString();
                            Session["ATM_LONGITUD_VERIF_CREAR"] = item["Longitud"].ToString();
                            Session["ATM_UBICACION_VERIF_CREAR"] = item["Ubicacion"].ToString();
                            Session["ATM_IDUBI_VERIF_CREAR"] = item["IdUbi"].ToString();
                            Session["ATM_SUCURSAL_VERIF_CREAR"] = item["Sucursal"].ToString();
                            Session["ATM_DEPTO_VERIF_CREAR"] = item["Departamento"].ToString();
                            Session["ATM_ZONA_VERIF_CREAR"] = item["Zona"].ToString();
                            Session["ATM_IDZONA_VERIF_CREAR"] = item["IDZona"].ToString();
                            Session["ATM_IDMANT_VERIF_CREAR"] = codVerif;
                            Session["ATM_ESTADO_VERIF_CREAR"] = item["Estado"].ToString();
                            Session["ATM_FECHAMANT_VERIF_CREAR"] = Convert.ToDateTime(item["FechaMantenimiento"]).ToString("yyyy/MM/dd");
                            Session["ATM_HRINICIO_VERIF_CREAR"] = item["HrInicio"].ToString();
                            Session["ATM_HRFIN_VERIF_CREAR"] = item["HrFin"].ToString();
                            Session["ATM_AUTORIZADO_VERIF_CREAR"] = item["Autorizado"].ToString();
                            Session["ATM_SYSAID_VERIF_CREAR"] = item["SysAid"].ToString();
                            Session["ATM_TECNICO_VERIF_CREAR"] = item["Tecnico"].ToString();
                            Session["ATM_USUARIO_VERIF_CREAR"] = item["Usuario"].ToString();
                            Session["ATM_IDENTIDAD_VERIF_CREAR"] = item["Identidad"].ToString();
                            Session["ATM_SO_VERIF_CREAR"] = item["SO"].ToString();
                            Session["ATM_VERSIONSW_VERIF_CREAR"] = item["VersionSw"].ToString();
                            Session["ATM_USUCORREO_VERIF_CREAR"] = item["CorreoTecnico"].ToString();
                            Session["ATM_USUCREADOR_VERIF_CREAR"] = item["UsuarioCreador"].ToString();
                            Session["ATM_ATMACTIVO_VERIF_CREAR"] = item["ATMActivo"].ToString();
                            Session["ATM_USUARIO_VERIF_CREAR"] = item["UsuResponsable"].ToString();


                        }

                        DataTable vDatos4 = new DataTable();
                        String vQuery4 = "STEISP_AGENCIA_CreacionNotificacion 6,'" + Session["ATM_USUCREADOR_VERIF_CREAR"].ToString() + "'";
                        vDatos4 = vConexion.ObtenerTabla(vQuery4);
                        foreach (DataRow item in vDatos4.Rows)
                        {
                            Session["ATM_NOMBRECREADOR_VERIF"] = item["nombre"].ToString();
                            Session["ATM_APELLIDOCREADOR_VERIF"] = item["apellidos"].ToString();
                            Session["ATM_CORREOCREADOR_VERIF"] = item["correo"].ToString();
                        }
                        TxBuscarTecnicoATM.Text = string.Empty;

                        Response.Redirect("verificacion.aspx");
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
                if (e.CommandName == "Reprogramar")
                {

                    try
                    {
                        DataTable vDatos2 = new DataTable();
                        vDatos2 = vConexion.ObtenerTabla("STEISP_ATM_VERIFICACION 2, '" + codVerif + "'");
                        //vDatos = vConexion.ObtenerTabla(vQuery);
                        foreach (DataRow item in vDatos2.Rows)
                        {
                            txtModalATM.Text = item["NomATM"].ToString();
                            Session["ATM_ID_CANCELAR_VERIF_MODAL"] = item["IDMant"].ToString();
                            Session["ATM_USURESPONSABLE_REPROGRAMAR"] = item["CorreoTecnico"].ToString();
                            Session["ATM_RESPONSABLE"] = item["Tecnico"].ToString();
                            Session["ATM_USURESPONSABLE"] = item["Usuario"].ToString();
                            Session["ATM_IDZONA_REPROGRAMAR"] = item["IDZona"].ToString();
                        }
                        TxBuscarTecnicoATM.Text = string.Empty;


                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);

                }

            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }
    }
}