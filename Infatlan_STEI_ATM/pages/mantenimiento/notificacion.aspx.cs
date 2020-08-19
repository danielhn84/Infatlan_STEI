using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.Sql;
using System.Data.SqlClient;
using Infatlan_STEI_ATM.clases;


namespace Infatlan_STEI_ATM.pages.mantenimiento
{
    public partial class notificacion : System.Web.UI.Page
    {
        bd vConexion = new bd();
        protected void Page_Load(object sender, EventArgs e){
            Session["NOTI"] = null;
            if (!Page.IsPostBack){
                if (Convert.ToBoolean(Session["AUTH"])){
                    txtFechaInicio.TextMode = TextBoxMode.Date;
                    cargarData();
                    string id = Request.QueryString["id"];
                    string tipo = Request.QueryString["tipo"];
                    switch (tipo){
                        case "3":
                            txtFechaInicio.TextMode = TextBoxMode.SingleLine;
                            llenarForm();
                            cargarDataNotificacion();
                            habilitar();
                            btnCancelarAprobNotif.Visible = true;
                            btnCancelarAprobNotif.Visible = true;
                            break;
                    }
                }else {
                    Response.Redirect("/login.aspx");
                }
            }
        }

        void LimpiarNotificacion()
        {
            Session["ATM_EMPLEADOS"] = null;
            Session["ATM_EMPLEADOS2"] = null;
            Session["NotifJefeAgenciaATM"] = null;
            GVjefesAgencias.DataSource = null;
            GVjefesAgencias.DataBind();
            GVJefesAD.DataSource = null;
            GVJefesAD.DataBind();
            GVBusqueda.DataSource = null;
            GVBusqueda.DataBind();
            Session["NOTI"] = null;
            //DDLrealizarMant.SelectedValue = "0";
            txtcancelarNotif.Text = string.Empty;
            DLLtecResponsable.Items.Clear();
            DLLTecnicoParticipante.Items.Clear();
            //DDLjefesAgencias.Items.Clear();
            txtHrInicioMant.Text = string.Empty;
            txtHrFinMant.Text = string.Empty;
            txtFechaInicio.Text = string.Empty;
            txtcodATMNotif.Text = string.Empty;
            txtsysaid.Text = string.Empty;
            DDLmantemientoPendiente.Items.Clear();
            txtUbicacionATM.Text = string.Empty;
            txtdireccion.Text = string.Empty;
            txtsucursalNotif.Text = string.Empty;
            txtipNotif.Text = string.Empty;
            txtzonaNotif.Text = string.Empty;
            txtidentidadTecResponsable.Text = string.Empty;
            UpNotif.Update();
        }

        void habilitar()
        {
            txtFechaInicio.Enabled = false;
            txtHrInicioMant.Enabled = false;
            txtHrFinMant.Enabled = false;
            txtsysaid.Enabled = false;
            DDLmantemientoPendiente.Enabled = false;
            DLLTecnicoParticipante.Enabled = false;
            DLLtecResponsable.Enabled = false;
            txtbuscarJefeNotif.Enabled = false;
            btnBuscarJefe.Enabled = false;
            GVBusqueda.Enabled = false;
        }
        
        void validacionesNotificacion()
        {
            if (DLLtecResponsable.SelectedValue == "0")
                throw new Exception("Favor seleccione técnico responsable.");
            if (Session["ATM_EMPLEADOS"] == null)
                throw new Exception("Favor seleccione técnicos participantes.");
            //if (DDLmantemientoPendiente.SelectedValue == "0")
            //    throw new Exception("Favor seleccione ATM.");          
            if (txtHrFinMant.Text == "" || txtHrFinMant.Text == string.Empty)
                throw new Exception("Favor ingrese la hora en la que termino mantenimiento.");
            if (txtHrInicioMant.Text == "" || txtHrInicioMant.Text == string.Empty)
                throw new Exception("Favor ingrese la hora en la inicio mantenimiento.");
            if (txtFechaInicio.Text == "" || txtFechaInicio.Text == string.Empty)
                throw new Exception("Favor ingrese la fecha de inicio de mantenimiento.");
            //if (txtFechaRegreso.Text == "" || txtFechaRegreso.Text == string.Empty)
            //    throw new Exception("Favor ingrese la fecha que finalizó mantenimiento.");
            if (txtsysaid.Text == "" || txtsysaid.Text == string.Empty)
                throw new Exception("Favor ingrese sysaid.");
            if (Session["idubi"].ToString() == "1")
            {
                if (Session["NotifJefeAgenciaATM"] == null)
                    throw new Exception("favor seleccione los jefes de agencia.");
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
                if (HttpContext.Current.Session["NOTI"] == null)
                {

                    String vQuery = "STEISP_AGENCIA_CreacionNotificacion 5";
                    DataTable vDatos = vConexion.ObtenerTabla(vQuery);
                    //DLLTecnicoParticipante.Items.Clear();
                    DLLTecnicoParticipante.Items.Add(new ListItem { Value = "0", Text = "Seleccione técnico participante..." });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DLLTecnicoParticipante.Items.Add(new ListItem { Value = item["idUsuario"].ToString(), Text = item["nombre"].ToString() });
                    }



                    String vQuery2 = "STEISP_AGENCIA_CreacionNotificacion 5";
                    DataTable vDatos2 = vConexion.ObtenerTabla(vQuery2);
                    //DLLtecResponsable.Items.Clear();
                    DLLtecResponsable.Items.Add(new ListItem { Value = "0", Text = "Seleccione técnico responsable..." });
                    foreach (DataRow item in vDatos2.Rows)
                    {
                        DLLtecResponsable.Items.Add(new ListItem { Value = item["idUsuario"].ToString(), Text = item["nombre"].ToString() });
                    }

                    Session["NOTI"] = "1";
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        protected void DLLTecnicoParticiante_TextChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow item in GVBusqueda.Rows)
                {
                    if (item.Cells[1].Text.Equals(DLLTecnicoParticipante.Text))
                    {
                        throw new Exception("Ya existe tecnico responsable.");

                    }
                }


                String vQuery = "STEISP_AGENCIA_CreacionNotificacion 7, " + DLLTecnicoParticipante.SelectedValue;
                DataTable vDatos = vConexion.ObtenerTabla(vQuery);
                DataTable vData = (DataTable)Session["ATM_EMPLEADOS"];
                if (vData == null)
                    vData = vDatos.Clone();
                if (vDatos != null)
                    vData.Rows.Add(vDatos.Rows[0]["idUsuario"].ToString(), vDatos.Rows[0]["nombre"].ToString(), vDatos.Rows[0]["identidad"].ToString(), vDatos.Rows[0]["correo"].ToString());
                GVBusqueda.DataSource = vData;
                GVBusqueda.DataBind();
                Session["ATM_EMPLEADOS"] = vData;
                DLLTecnicoParticipante.SelectedValue = "0";

            }
            catch (Exception Ex)
            {
                DLLTecnicoParticipante.SelectedValue = "0";
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        protected void DLLtecResponsable_TextChanged(object sender, EventArgs e)
        {
            try
            {
                String vQuery = "STEISP_AGENCIA_CreacionNotificacion 6, " + DLLtecResponsable.SelectedValue;
                DataTable vDatos = vConexion.ObtenerTabla(vQuery);
                txtidentidadTecResponsable.Text = vDatos.Rows[0]["identidad"].ToString();
                Session["ATM_Notif_emailTecnicoResponsable"]= vDatos.Rows[0]["correo"].ToString();

            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }

        }

        protected void btnEnviarNotificacion_Click(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"];
            string tipo = Request.QueryString["tipo"];
            Session["resta"] = null;
            try
            {
                if (DDLrealizarMant.SelectedValue == "2")
                {
                    if (txtcancelarNotif.Text == "" || txtcancelarNotif.Text == string.Empty)
                    {
                        Mensaje("no se puede dejar vacio el motivo de cancelación de notificación", WarningType.Warning);
                    }
                    else
                    {
                        lbTecnicoResp.Text = "No disponible";
                        lbHrMantenimiento.Text = "No disponible";
                        MostrarModal();
                    }
                }
                else
                {
                    if (tipo == "3")
                    {
                        //validacionesNotificacion();
                        TimeSpan horainicio = TimeSpan.Parse(txtHrInicioMant.Text);
                        TimeSpan horafinal = TimeSpan.Parse(txtHrFinMant.Text);
                        Session["resta"] = horafinal - horainicio;
                        lbHrMantenimiento.Text = Session["resta"].ToString() + " horas";
                        lbTecnicoResp.Text = DLLtecResponsable.SelectedItem.Text;
                        MostrarModal();
                    }
                    else
                    {
                        validacionesNotificacion();
                        TimeSpan horainicio = TimeSpan.Parse(txtHrInicioMant.Text);
                        TimeSpan horafinal = TimeSpan.Parse(txtHrFinMant.Text);
                        Session["resta"] = horafinal - horainicio;
                        lbHrMantenimiento.Text = Session["resta"].ToString() + " horas";
                        lbTecnicoResp.Text = DLLtecResponsable.SelectedItem.Text;
                        MostrarModal();
                    }
                }

            }
            catch (Exception ex)
            {

                Mensaje(ex.Message, WarningType.Danger);
            }


        }

        void MostrarModal()
        {
            lbFechaInicia.Text = txtFechaInicio.Text;
            lbcodATM.Text = txtcodATMNotif.Text;
            lbNombreATM.Text = DDLmantemientoPendiente.SelectedItem.Text;
            lbsucursalATM.Text = txtsucursalNotif.Text;
            //lbTecnicoResp.Text = DLLtecResponsable.SelectedItem.Text;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);
        }

        protected void btnModalCerrarNotificacion_Click(object sender, EventArgs e)
        {
            //DLLtecResponsable.Items.Clear();
            //DLLTecnicoParticipante.Items.Clear();
            //cargarData();
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
        }

        void EnviarCorreo()
        {
            string id = Request.QueryString["id"];
            string tipo = Request.QueryString["tipo"];
            Boolean vFlagEnvio = false;
            String vDestino = "";
            SmtpService vService = new SmtpService();
            if (tipo == "3")
            {
                string vQueryD = "STEISP_ATM_Generales 33,'" + DLLtecResponsable.SelectedValue + "'";
                DataTable vDatosTecnicoResponsable = vConexion.ObtenerTabla(vQueryD);
                DataTable vDatos = (DataTable)Session["AUTHCLASS"];
                DataTable vDatosTecnicos = (DataTable)Session["ATM_EMPLEADOS"];
                DataTable vDatosJefeAgencias = (DataTable)Session["ATM_JEFES_CARGAR"];
                if (vDatos.Rows.Count > 0)
                {
                    foreach (DataRow item in vDatos.Rows)
                    {
                        //ENVIAR A JEFE
                        if (!item["emailEmpresa"].ToString().Trim().Equals(""))
                        {
                            vService.EnviarMensaje(item["emailEmpresa"].ToString(),
                                typeBody.Supervisor,
                                item["nombre"].ToString(),
                                item["nombre"].ToString()
                                //vDatosEmpleado.Rows[0]["Nombre"].ToString()
                                );
                            //ENVIAR A SOLICITANTE
                            vService.EnviarMensaje(item["emailEmpresa"].ToString(),
                                typeBody.Supervisor,
                                item["nombre"].ToString(),
                                item["nombre"].ToString()
                                //vDatosEmpleado.Rows[0]["Nombre"].ToString()
                                );
                            //vFlagEnvioSupervisor = true;
                        }
                        //ENVIAR A EDWIN
                        string vNombre = "EDWIN ALBERTO URREA PENA";
                        vService.EnviarMensaje(ConfigurationManager.AppSettings["STEIMail"],
                                typeBody.Supervisor,
                                vNombre,
                                item["nombre"].ToString()
                                //vDatosEmpleado.Rows[0]["Nombre"].ToString()
                                );
                        //ENVIAR A ELVIS
                        string vNombreJefe = "ELVIS ALEXANDER MONTOYA PEREIRA";
                        vService.EnviarMensaje(ConfigurationManager.AppSettings["STEIJefeMail"],
                                typeBody.Supervisor,
                                vNombreJefe,
                                item["nombre"].ToString()
                                //vDatosEmpleado.Rows[0]["Nombre"].ToString()
                                );
                    }
                }
                if (vDatosTecnicoResponsable.Rows.Count > 0)
                {
                    foreach (DataRow item in vDatos.Rows)
                    {
                        //ENVIAR A TECNICO RESPONSABLE
                        if (!item["correo"].ToString().Trim().Equals(""))
                        {
                            vService.EnviarMensaje(item["correo"].ToString(),
                                typeBody.Encargado,
                                item["nombre"].ToString() + " " + item["apellidos"].ToString() + " se le informa que se hara mantenimiento la fecha " + txtFechaInicio.Text,
                                vDatos.Rows[0]["nombre"].ToString()
                                //vDatosEmpleado.Rows[0]["Nombre"].ToString()
                                );
                        }
                    }
                }
                if (vDatosTecnicos.Rows.Count > 0)
                {
                    foreach (DataRow item in vDatos.Rows)
                    {
                        //ENVIAR A TECNICOS
                        if (!item["correo"].ToString().Trim().Equals(""))
                        {
                            vService.EnviarMensaje(item["correo"].ToString(),
                                typeBody.Tecnicos,
                                item["nombre"].ToString()+" se le informa que se hara mantenimiento la fecha "+txtFechaInicio.Text,
                                vDatos.Rows[0]["nombre"].ToString()
                                //vDatosEmpleado.Rows[0]["Nombre"].ToString()
                                );
                        }
                    }
                }
                if (vDatosJefeAgencias.Rows.Count > 0)
                {
                    foreach (DataRow item in vDatos.Rows)
                    {
                        //ENVIAR A JEFES DE AGENCIA
                        if (!item["correo"].ToString().Trim().Equals(""))
                        {
                            vService.EnviarMensaje(item["correo"].ToString(),
                                typeBody.Encargado,
                                " Jefe de Agencia, se le informa que se hara mantenimiento la fecha "+txtFechaInicio.Text,
                                vDatos.Rows[0]["nombre"].ToString()
                                //vDatosEmpleado.Rows[0]["Nombre"].ToString()
                                );
                        }
                    }
                }
            }
            else
            {
                string vQueryD = "STEISP_ATM_Generales 33,'" + DLLtecResponsable.SelectedValue + "'";
                DataTable vDatosTecnicoResponsable = vConexion.ObtenerTabla(vQueryD);
                DataTable vDatos = (DataTable)Session["AUTHCLASS"];
                DataTable vDatosTecnicos = (DataTable)Session["ATM_EMPLEADOS"];
                DataTable vDatosJefeAgencias = (DataTable)Session["NotifJefeAgenciaATM"];
                if (vDatos.Rows.Count > 0)
                {
                    foreach (DataRow item in vDatos.Rows)
                    {
                        //ENVIAR A JEFE
                        if (!item["emailEmpresa"].ToString().Trim().Equals(""))
                        {
                            vService.EnviarMensaje(item["emailEmpresa"].ToString(),
                                typeBody.Supervisor,
                                item["nombre"].ToString(),
                                item["nombre"].ToString()
                                //vDatosEmpleado.Rows[0]["Nombre"].ToString()
                                );
                            //ENVIAR A SOLICITANTE
                            vService.EnviarMensaje(item["emailEmpresa"].ToString(),
                                typeBody.Supervisor,
                                item["nombre"].ToString(),
                                item["nombre"].ToString()
                                //vDatosEmpleado.Rows[0]["Nombre"].ToString()
                                );
                            //vFlagEnvioSupervisor = true;
                        }
                        //ENVIAR A EDWIN
                        string vNombre = "EDWIN ALBERTO URREA PENA";
                        vService.EnviarMensaje(ConfigurationManager.AppSettings["STEIMail"],
                                typeBody.Supervisor,
                                vNombre,
                                item["nombre"].ToString()
                                //vDatosEmpleado.Rows[0]["Nombre"].ToString()
                                );
                        //ENVIAR A ELVIS
                        string vNombreJefe = "ELVIS ALEXANDER MONTOYA PEREIRA";
                        vService.EnviarMensaje(ConfigurationManager.AppSettings["STEIJefeMail"],
                                typeBody.Supervisor,
                                vNombreJefe,
                                item["nombre"].ToString()
                                //vDatosEmpleado.Rows[0]["Nombre"].ToString()
                                );
                    }
                }
                if (vDatosTecnicoResponsable.Rows.Count > 0)
                {
                    foreach (DataRow item in vDatos.Rows)
                    {
                        //ENVIAR A TECNICO RESPONSABLE
                        if (!item["correo"].ToString().Trim().Equals(""))
                        {
                            vService.EnviarMensaje(item["correo"].ToString(),
                                typeBody.Encargado,
                                item["nombre"].ToString()+" "+ item["apellidos"].ToString() + " se le informa que se hara mantenimiento la fecha " + txtFechaInicio.Text,
                                vDatos.Rows[0]["nombre"].ToString()
                                //vDatosEmpleado.Rows[0]["Nombre"].ToString()
                                );
                        }
                    }
                }
                if (vDatosTecnicos.Rows.Count > 0)
                {
                    foreach (DataRow item in vDatos.Rows)
                    {
                        //ENVIAR A TECNICOS
                        if (!item["correo"].ToString().Trim().Equals(""))
                        {
                            vService.EnviarMensaje(item["correo"].ToString(),
                                typeBody.Tecnicos,
                                item["nombre"].ToString() + " se le informa que se hara mantenimiento la fecha " + txtFechaInicio.Text,
                                vDatos.Rows[0]["nombre"].ToString()
                                //vDatosEmpleado.Rows[0]["Nombre"].ToString()
                                );
                        }
                    }
                }
                if (vDatosJefeAgencias.Rows.Count > 0)
                {
                    foreach (DataRow item in vDatos.Rows)
                    {
                        //ENVIAR A TECNICOS
                        if (!item["correo"].ToString().Trim().Equals(""))
                        {
                            vService.EnviarMensaje(item["correo"].ToString(),
                                typeBody.Encargado,
                                " Jefe de Agencia, se le informa que se hara mantenimiento la fecha "+txtFechaInicio.Text,
                                vDatos.Rows[0]["nombre"].ToString()
                                //vDatosEmpleado.Rows[0]["Nombre"].ToString()
                                );
                        }
                    }
                }
            }
        }

        void CorreoSuscripcion()
        {
            string vEstado = "";
            DataTable vDatos = new DataTable();
            String vQuery = "STEISP_ATM_Generales 36,'" + Session["codNotificacion"] + "'";
            vDatos = vConexion.ObtenerTabla(vQuery);
            foreach (DataRow item in vDatos.Rows)
            {
                vEstado = item["estadoMantenimiento"].ToString();
            }

            if (vEstado == "3")
            {
                string vReporteViaticos = "Notificacion";
                string vCorreoAdmin = "acedillo@bancatlan.hn";
                string vCorreoCopia = "acamador@bancatlan.hn";
                string vAsuntoRV = "Formato de notificación";
                string vBody = "Formato de notificación";
                int vEstadoSuscripcion = 0;
                string vQueryRep = "STEISP_ATM_Generales 35, '" + vReporteViaticos + "','" + vCorreoAdmin + "','" + vCorreoCopia + "','" + vAsuntoRV + "','" + vBody + "','" + vEstadoSuscripcion + "','" + Session["codNotificacion"] + "'";
                vConexion.ejecutarSQL(vQueryRep);
            }
        }

        protected void btnModalEnviarNotificacion_Click(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"];
            string tipo = Request.QueryString["tipo"];
            if (tipo == "3")
            {
                
                try
                {
                    //string vEstado = "9";
                    string vEstado = "3";
                    string vQuery = "STEISP_ATM_Aprobaciones 1, '" + Session["codNotificacion"] + "','" + vEstado + "', '" + Session["USUARIO"].ToString() + "'";
                    Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    if (vInfo == 1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
                        Mensaje("Notificación aprobada con éxito", WarningType.Success);
                        //Enviar Correo
                        CorreoSuscripcion();
                        //EnviarCorreo();
                        //Enviar Correo
                        
                        LimpiarNotificacion();
                        UpNotif.Update();

                        Response.Redirect("buscarAprobarNotificacion.aspx");
                    }
                    else
                    {
                        Mensaje("No se pudo aprobar la notificación", WarningType.Warning);
                    }
                }
                catch (Exception Ex)
                {
                    Mensaje(Ex.Message, WarningType.Danger);
                }
            }
            else
            {

               
                string autorizarATM = "true";
                int estado = 2;
                if (DDLrealizarMant.SelectedValue == "1")
                {
                    autorizarATM = "true";
                }
                else
                {
                    autorizarATM = "false";
                }

                if (DDLrealizarMant.SelectedValue == "1")
                {
                    try
                    {
                        string vQuery = "STEISP_ATM_Notificaciones 1, '" + Session["ID"] + "','" + txtHrInicioMant.Text + "', '" + txtHrFinMant.Text + "'," +
                            "'" + autorizarATM + "','" + DLLtecResponsable.SelectedValue + "','" + txtsysaid.Text + "', " + estado + ",'" + Session["USUARIO"].ToString() + "'";
                        Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                        if (vInfo == 1)
                        {
                            lbNoNotif.Visible = true;
                            if (Session["idubi"].ToString() == "1")
                            {
                                usuariosJefeAgentes();
                                usuariosMantenimiento();
                            }
                            else
                            {
                                usuariosMantenimiento();
                            }
                            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
                            Mensaje("Mantenimiento creado con éxito", WarningType.Success);
                            //Enviar Correo
                            //EnviarCorreo();
                            //Enviar Correo
                            LimpiarNotificacion();
                            UpNotif.Update();
                            cargarData();
                            txtbuscarJefeNotif.Visible = false;
                            btnBuscarJefe.Visible = false;
                            DIVBuscarJefes.Visible = false;
                            H3JefeAgencia.Visible = false;
                            lbSelectJefeAge.Visible = false;
                        }
                        else
                        {
                            lbNoNotif.Visible = true;
                        }
                    }
                    catch (Exception Ex)
                    {
                        Mensaje(Ex.Message, WarningType.Danger);
                    }
                    
                }
                else
                {
                    CancelarNotificacion();

                    Session.Clear();
                }
            }
        }

        void CorreoCancelar()
        {
            SmtpService vService = new SmtpService();
            string vQueryD = "STEISP_ATM_Generales 33,'" + DLLtecResponsable.SelectedValue + "'";
            DataTable vDatosTecnicoResponsable = vConexion.ObtenerTabla(vQueryD);
            DataTable vDatos = (DataTable)Session["AUTHCLASS"];
            DataTable vDatosTecnicos = (DataTable)Session["ATM_EMPLEADOS"];
            DataTable vDatosJefeAgencias = (DataTable)Session["ATM_JEFES_CARGAR"];
            if (vDatos.Rows.Count > 0)
            {
                foreach (DataRow item in vDatos.Rows)
                {
                    //ENVIAR A JEFE
                    if (!item["emailEmpresa"].ToString().Trim().Equals(""))
                    {
                        vService.EnviarMensaje(item["emailEmpresa"].ToString(),
                            typeBody.ReprogramacionJefes,
                            item["nombre"].ToString(),
                            item["nombre"].ToString()
                            //vDatosEmpleado.Rows[0]["Nombre"].ToString()
                            );
                        //ENVIAR A SOLICITANTE
                        //vService.EnviarMensaje(item["emailEmpresa"].ToString(),
                        //    typeBody.Supervisor,
                        //    item["nombre"].ToString(),
                        //    item["nombre"].ToString()
                        //    //vDatosEmpleado.Rows[0]["Nombre"].ToString()
                        //    );
                        //vFlagEnvioSupervisor = true;
                    }
                    //ENVIAR A EDWIN
                    string vNombre = "EDWIN ALBERTO URREA PENA";
                    vService.EnviarMensaje(ConfigurationManager.AppSettings["STEIMail"],
                            typeBody.ReprogramacionJefes,
                            vNombre,
                            item["nombre"].ToString()
                            //vDatosEmpleado.Rows[0]["Nombre"].ToString()
                            );
                    //ENVIAR A ELVIS
                    string vNombreJefe = "ELVIS ALEXANDER MONTOYA PEREIRA";
                    vService.EnviarMensaje(ConfigurationManager.AppSettings["STEIJefeMail"],
                            typeBody.ReprogramacionJefes,
                            vNombreJefe,
                            item["nombre"].ToString()
                            //vDatosEmpleado.Rows[0]["Nombre"].ToString()
                            );
                }
            }
            if (vDatosTecnicoResponsable.Rows.Count > 0)
            {
                foreach (DataRow item in vDatos.Rows)
                {
                    //ENVIAR A TECNICO RESPONSABLE
                    if (!item["correo"].ToString().Trim().Equals(""))
                    {
                        vService.EnviarMensaje(item["correo"].ToString(),
                            typeBody.Reprogramacion,
                            item["nombre"].ToString() + " " + item["apellidos"].ToString(),
                            vDatos.Rows[0]["nombre"].ToString()
                            //vDatosEmpleado.Rows[0]["Nombre"].ToString()
                            );
                    }
                }
            }
            if (vDatosTecnicos.Rows.Count > 0)
            {
                foreach (DataRow item in vDatos.Rows)
                {
                    //ENVIAR A TECNICOS
                    if (!item["correo"].ToString().Trim().Equals(""))
                    {
                        vService.EnviarMensaje(item["correo"].ToString(),
                            typeBody.Reprogramacion,
                            item["nombre"].ToString(),
                            vDatos.Rows[0]["nombre"].ToString()
                            //vDatosEmpleado.Rows[0]["Nombre"].ToString()
                            );
                    }
                }
            }
            if (vDatosJefeAgencias.Rows.Count > 0)
            {
                foreach (DataRow item in vDatos.Rows)
                {
                    //ENVIAR A JEFES DE AGENCIA
                    if (!item["correo"].ToString().Trim().Equals(""))
                    {
                        vService.EnviarMensaje(item["correo"].ToString(),
                            typeBody.Reprogramacion,
                            " Jefe de Agencia, se le informa que se canceló mantenimiento",
                            vDatos.Rows[0]["nombre"].ToString()
                            //vDatosEmpleado.Rows[0]["Nombre"].ToString()
                            );
                    }
                }
            }
        }

        void CancelarNotificacion()
        {
           
            string autorizarATM = "";
            int estado = 1;
            try
            {
                string vQuery = "STEISP_ATM_Notificaciones 2, '" + Session["ID"] + "','" + txtcancelarNotif.Text + "', '" + txtHrFinMant.Text + "'," +
                    "'" + autorizarATM + "','" + DLLtecResponsable.SelectedItem.Text + "','" + txtsysaid.Text + "', " + estado + ",'" + Session["USUARIO"].ToString() + "'";
                Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                if (vInfo == 1)
                {
                    //ENVIAR CORREO

                    //CorreoCancelar();

                    //ENVIAR CORREO

                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
                    Mensaje("Mantenimiento cancelado con éxito, ahora está en lista de reprogramación", WarningType.Success);
                   
                    LimpiarNotificacion();
                    UpNotif.Update();
                    cargarData();

                }
                else
                {
                    Mensaje("No se pudo cancelar el mantenimiento", WarningType.Warning);
                }
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }

        }


        void usuariosMantenimiento()
        {
            try
            {

                DataTable vDatos = (DataTable)Session["ATM_EMPLEADOS"];

                for (int i = 0; i < vDatos.Rows.Count; i++)
                {
                    string usuarios = vDatos.Rows[i]["idUsuario"].ToString();
                    string vQuery = "STEISP_ATM_UsuariosMantenimiento 1, '" + Session["ID"].ToString() + "','" + usuarios + "'";
                    vConexion.ejecutarSQL(vQuery);
                }
            }
            catch (Exception Ex)
            {
                throw;
            }

        }

        void usuariosJefeAgentes()
        {
            try
            {

                DataTable vDatos = (DataTable)Session["NotifJefeAgenciaATM"];

                for (int i = 0; i < vDatos.Rows.Count; i++)
                {
                    string correos = vDatos.Rows[i]["Correo"].ToString();
                    string vNombre = vDatos.Rows[i]["Nombre"].ToString();
                    string vApellido = vDatos.Rows[i]["Apellido"].ToString();
                    string vQuery = "STEISP_ATM_UsuariosMantenimiento 2, '" + Session["ID"].ToString() + "','" + correos + "','"+vNombre+"','"+vApellido+"'";
                    vConexion.ejecutarSQL(vQuery);
                }
            }
            catch (Exception Ex)
            {
                throw;
            }
        }
        protected void Btnseleccionar_Click(object sender, EventArgs e)
        {


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
        void buscarATM()
        {
            try
            {
                string Fecha = Session["fechaMantATM"].ToString();
                DDLmantemientoPendiente.Items.Clear();
                Session["IDATM"] = null;
                String vFormato = "yyyy/MM/dd"; //"dd/MM/yyyy HH:mm:ss"
                String vFechaMant = Convert.ToDateTime(Fecha).ToString(vFormato);

                String vQuery2 = "STEISP_ATM_SELECCIONES 3,'" + vFechaMant + "' ";
                DataTable vDatos2 = vConexion.ObtenerTabla(vQuery2);
                DDLmantemientoPendiente.Items.Add(new ListItem { Value = "0", Text = "Seleccione Mantenimineto pendiente..." });
                txtbuscarJefeNotif.Text = "";
                GVjefesAgencias.Visible = false;
                H3JefeAgencia.Visible = false;
                limpiarDatosATM();
                DIVBuscarJefes.Visible = false;
                H3JefeAgencia.Visible = false;
                lbSelectJefeAge.Visible = false;
                Session["ATM_EMPLEADOS"] = null;
                Session["ATM_EMPLEADOS2"] = null;
                Session["NotifJefeAgenciaATM"] = null;
                GVjefesAgencias.DataSource = null;
                GVjefesAgencias.DataBind();
                GVJefesAD.DataSource = null;
                GVJefesAD.DataBind();
                GVBusqueda.DataSource = null;
                GVBusqueda.DataBind();
                foreach (DataRow item in vDatos2.Rows)
                {
                    DDLmantemientoPendiente.Items.Add(new ListItem { Value = item["Codigo"].ToString(), Text = item["Nombre"].ToString() });
                }


            }
            catch (Exception)
            {

                throw;
            }
        }
        void cargarDataNotificacion()
        {
            try
            {
                DataTable vDatos = new DataTable();
                vDatos = vConexion.ObtenerTabla("STEISP_ATM_Generales 16, '" + Session["codNotificacion"] + "'");
                GVBusqueda.DataSource = vDatos;
                GVBusqueda.DataBind();
                Session["ATM_EMPLEADOS"] = vDatos;
            }
            catch (Exception Ex)
            {

            }

            try
            {
                DataTable vDatos2 = new DataTable();
                vDatos2 = vConexion.ObtenerTabla("STEISP_ATM_Generales 17, '" + Session["codNotificacion"] + "'");
                GVLlenaJefeApruebaNotif.DataSource = vDatos2;
                GVLlenaJefeApruebaNotif.DataBind();
                Session["ATM_JEFES_CARGAR"] = vDatos2;
            }
            catch (Exception Ex)
            {

            }

            

        }
               

    
        void llenarForm()
        {
            string ver = Session["autorizadoATM"].ToString();
            if (ver == "True")
                DDLrealizarMant.SelectedValue = "1";
            else
                DDLrealizarMant.SelectedValue = "2";
            string Fecha = Session["fechaMantATM"].ToString();           
            String vFormato = "dd/MM/yyyy"; //"dd/MM/yyyy HH:mm:ss"
            String vFechaMant = Convert.ToDateTime(Fecha).ToString(vFormato);         
            txtFechaInicio.Text = vFechaMant;
            buscarATM();
            txtcancelarNotif.Text = Session["cancelaATM"].ToString();
            txtHrInicioMant.Text = Session["hrInicioATM"].ToString();
            txtHrFinMant.Text = Session["hrfinATM"].ToString();
            txtsysaid.Text = Session["sysaidATM"].ToString();
            DDLmantemientoPendiente.SelectedIndex= CargarInformacionDDL(DDLmantemientoPendiente, Session["NomATM"].ToString());           
            txtcodATMNotif.Text = Session["codATM"].ToString();
            txtUbicacionATM.Text = Session["ubicacionATM"].ToString();
            txtdireccion.Text = Session["direccionATM"].ToString();
            txtsucursalNotif.Text = Session["SucursalATM"].ToString();
            txtipNotif.Text = Session["IPATM"].ToString();
            txtzonaNotif.Text = Session["zonaATM"].ToString();
            DLLtecResponsable.SelectedIndex = CargarInformacionDDL(DLLtecResponsable, Session["usuario"].ToString());           
            txtidentidadTecResponsable.Text = Session["identidad"].ToString();

        }
        protected void txtFechaInicio_TextChanged(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"];
            string tipo = Request.QueryString["tipo"];

            if (tipo == "3")
            {
                try
                {
                    //txtFechaInicio.Text = string.Empty;
                    DDLmantemientoPendiente.Items.Clear();
                    Session["IDATM"] = null;
                    String vFormato = "yyyy/MM/dd"; //"dd/MM/yyyy HH:mm:ss"
                    String vFechaMant = Convert.ToDateTime(txtFechaInicio.Text).ToString(vFormato);

                    String vQuery2 = "STEISP_ATM_SELECCIONES 3,'" + vFechaMant + "' ";
                    DataTable vDatos2 = vConexion.ObtenerTabla(vQuery2);
                    DDLmantemientoPendiente.Items.Add(new ListItem { Value = "0", Text = "Seleccione Mantenimineto pendiente..." });
                    txtbuscarJefeNotif.Text = "";
                    GVjefesAgencias.Visible = false;
                    H3JefeAgencia.Visible = false;
                    limpiarDatosATM();
                    DIVBuscarJefes.Visible = false;
                    H3JefeAgencia.Visible = false;
                    lbSelectJefeAge.Visible = false;
                    Session["ATM_EMPLEADOS"] = null;
                    Session["ATM_EMPLEADOS2"] = null;
                    Session["NotifJefeAgenciaATM"] = null;
                    GVjefesAgencias.DataSource = null;
                    GVjefesAgencias.DataBind();
                    GVJefesAD.DataSource = null;
                    GVJefesAD.DataBind();
                    GVBusqueda.DataSource = null;
                    GVBusqueda.DataBind();
                    foreach (DataRow item in vDatos2.Rows)
                    {
                        DDLmantemientoPendiente.Items.Add(new ListItem { Value = item["Codigo"].ToString(), Text = item["Nombre"].ToString() });
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                try
                {
                    //txtFechaInicio.Text = string.Empty;
                    DDLmantemientoPendiente.Items.Clear();
                    Session["IDATM"] = null;
                    String vFormato = "yyyy/MM/dd"; //"dd/MM/yyyy HH:mm:ss"
                    String vFechaMant = Convert.ToDateTime(txtFechaInicio.Text).ToString(vFormato);

                    String vQuery2 = "STEISP_ATM_SELECCIONES 1,'" + vFechaMant + "' ";
                    DataTable vDatos2 = vConexion.ObtenerTabla(vQuery2);
                    DDLmantemientoPendiente.Items.Add(new ListItem { Value = "0", Text = "Seleccione Mantenimineto pendiente..." });
                    txtbuscarJefeNotif.Text = "";
                    GVjefesAgencias.Visible = false;
                    H3JefeAgencia.Visible = false;
                    limpiarDatosATM();
                    DIVBuscarJefes.Visible = false;
                    H3JefeAgencia.Visible = false;
                    lbSelectJefeAge.Visible = false;
                    Session["ATM_EMPLEADOS"] = null;
                    Session["ATM_EMPLEADOS2"] = null;
                    Session["NotifJefeAgenciaATM"] = null;
                    GVjefesAgencias.DataSource = null;
                    GVjefesAgencias.DataBind();
                    GVJefesAD.DataSource = null;
                    GVJefesAD.DataBind();
                    GVBusqueda.DataSource = null;
                    GVBusqueda.DataBind();
                    foreach (DataRow item in vDatos2.Rows)
                    {
                        DDLmantemientoPendiente.Items.Add(new ListItem { Value = item["Codigo"].ToString(), Text = item["Nombre"].ToString() });
                    }


                }
                catch (Exception)
                {

                    throw;
                }
            }
        }     

        void limpiarDatosATM()
        {
            txtcodATMNotif.Text = string.Empty;
            txtUbicacionATM.Text = string.Empty;
            txtdireccion.Text = string.Empty;
            txtsucursalNotif.Text = string.Empty;
            txtipNotif.Text = string.Empty;
            txtzonaNotif.Text = string.Empty;
            //GVBusqueda.Columns.Clear();
            GVBusqueda.DataSource = null;
            GVBusqueda.DataBind();
            GVjefesAgencias.DataSource = null;
            GVjefesAgencias.DataBind();
            //DDLjefesAgencias.SelectedValue = "0";
            DLLTecnicoParticipante.SelectedValue = "0";
            DLLtecResponsable.SelectedValue = "0";
            txtidentidadTecResponsable.Text = string.Empty;
            Session["ATM_EMPLEADOS2"] = null;
            Session["ATM_EMPLEADOS"] = null;
        }
        protected void DDLmantemientoPendiente_SelectedIndexChanged(object sender, EventArgs e)
        {
            limpiarDatosATM();
            try
            {
                String vQuery = "STEISP_ATM_SELECCIONES 2, " + DDLmantemientoPendiente.SelectedValue;
                DataTable vDatos = vConexion.ObtenerTabla(vQuery);

                if (DDLmantemientoPendiente.SelectedValue == "0")
                {
                    if (Session["IdUbi"].ToString() == "0" || DDLmantemientoPendiente.SelectedValue == "0")
                    {
                        DIVBuscarJefes.Visible = false;
                        H3JefeAgencia.Visible = false;
                        lbSelectJefeAge.Visible = false;
                        Session["ATM_EMPLEADOS"] = null;
                        Session["ATM_EMPLEADOS2"] = null;
                        Session["NotifJefeAgenciaATM"] = null;
                        GVjefesAgencias.DataSource = null;
                        GVjefesAgencias.DataBind();
                        GVJefesAD.DataSource = null;
                        GVJefesAD.DataBind();
                        GVBusqueda.DataSource = null;
                        GVBusqueda.DataBind();
                    }
                }
                else
                {
                    txtcodATMNotif.Text = vDatos.Rows[0]["Codigo"].ToString();
                    txtUbicacionATM.Text = vDatos.Rows[0]["Ubicacion"].ToString();
                    txtdireccion.Text = vDatos.Rows[0]["Direccion"].ToString();
                    txtsucursalNotif.Text = vDatos.Rows[0]["Sucursal"].ToString();
                    txtipNotif.Text = vDatos.Rows[0]["IP"].ToString();
                    txtzonaNotif.Text = vDatos.Rows[0]["Zona"].ToString();
                    Session["NomATM"] = vDatos.Rows[0]["NomATM"].ToString();
                    Session["IdUbi"] = vDatos.Rows[0]["IdUbi"].ToString();
                    Session["ID"] = vDatos.Rows[0]["ID"].ToString();
                    if (Session["IdUbi"].ToString() == "1")
                    {
                        lbSelectJefeAge.Visible = true;
                        GVjefesAgencias.Visible = true;
                        DIVBuscarJefes.Visible = true;
                        H3JefeAgencia.Visible = true;
                        Session["ATM_EMPLEADOS"] = null;
                        Session["ATM_EMPLEADOS2"] = null;
                        Session["NotifJefeAgenciaATM"] = null;
                        GVjefesAgencias.DataSource = null;
                        GVjefesAgencias.DataBind();
                        GVJefesAD.DataSource = null;
                        GVJefesAD.DataBind();
                        GVBusqueda.DataSource = null;
                        GVBusqueda.DataBind();
                    }
                    else
                    {
                        DIVBuscarJefes.Visible = false;
                        H3JefeAgencia.Visible = false;
                        lbSelectJefeAge.Visible = false;
                        Session["ATM_EMPLEADOS"] = null;
                        Session["ATM_EMPLEADOS2"] = null;
                        Session["NotifJefeAgenciaATM"] = null;
                        GVjefesAgencias.DataSource = null;
                        GVjefesAgencias.DataBind();
                        GVJefesAD.DataSource = null;
                        GVJefesAD.DataBind();
                        GVBusqueda.DataSource = null;
                        GVBusqueda.DataBind();
                    }
                }

            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }

        }

        protected void DDLrealizarMant_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLrealizarMant.SelectedValue == "2")
            {
                GVBusqueda.DataSource = null;
                GVBusqueda.DataBind();
                GVjefesAgencias.DataSource = null;
                GVjefesAgencias.DataBind();
                //DDLjefesAgencias.SelectedValue = "0";
                DLLTecnicoParticipante.SelectedValue = "0";
                DLLtecResponsable.SelectedValue = "0";
                txtidentidadTecResponsable.Text = string.Empty;
                Session["ATM_EMPLEADOS2"] = null;
                Session["ATM_EMPLEADOS"] = null;
                DLLTecnicoParticipante.Enabled = false;
                DLLtecResponsable.Enabled = false;
                DivCancelaNotif.Visible = true;

            }
            else
            {
                //LimpiarNotificacion();
                DLLTecnicoParticipante.Enabled = true;
                DLLtecResponsable.Enabled = true;
                DivCancelaNotif.Visible = false;
            }
        }

        protected void txtbuscarJefeNotif_TextChanged(object sender, EventArgs e)
        {

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
                    Session["ATM_BUSCAR_JEFE"] = vDatos;
                    UpdatePanel2.Update();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void GVJefesAD_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GVJefesAD.PageIndex = e.NewPageIndex;
                GVJefesAD.DataSource = (DataTable)Session["ATM_BUSCAR_JEFE"];
                GVJefesAD.DataBind();
            }
            catch (Exception Ex)
            {

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
                    DataTable vDatos = (DataTable)Session["NotifJefeAgenciaATM"];
                    DataTable vDatosJefes = (DataTable)Session["ATM_BUSCAR_JEFE"];
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
                        vDatos.Rows.Add(CorreoJefe, vNombre,vApellido);

                    }
                    GVjefesAgencias.DataSource = vDatos;
                    GVjefesAgencias.DataBind();
                    Session["NotifJefeAgenciaATM"] = vDatos;
                    UpdatePanel2.Update();
                }
                catch (Exception Ex)
                {

                    // DLLTecnicoParticipante.SelectedValue = "0";
                    Mensaje(Ex.Message, WarningType.Danger);
                }
            }
        }

        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DataTable vDatos = (DataTable)Session["ATM_EMPLEADOS"];
            if (e.CommandName == "eliminar")
            {
                String vUsuario = e.CommandArgument.ToString();
                if (Session["ATM_EMPLEADOS"] != null)
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
            Session["ATM_EMPLEADOS"] = vDatos;
        }

        protected void GVjefesAgencias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DataTable vDatos = (DataTable)Session["NotifJefeAgenciaATM"];
            if (e.CommandName == "eliminar")
            {
                String vCorreo = e.CommandArgument.ToString();
                if (Session["NotifJefeAgenciaATM"] != null)
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
            Session["NotifJefeAgenciaATM"] = vDatos;
        }

        protected void btnReprogramarNotif_Click(object sender, EventArgs e)
        {
           
            if (txtcomentarioReprogramaNotif.Text == "" || txtcomentarioReprogramaNotif.Text == string.Empty)
            {
                txtAlerta1.Visible = true;
                lbRep1.Visible = true;
            }
            else
            {
                try
                {
                    string vQuery = "STEISP_ATM_Aprobaciones 2, '" + Session["codNotificacion"] + "','" + txtcomentarioReprogramaNotif.Text + "', '" + Session["USUARIO"].ToString() + "'";
                    Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    if (vInfo == 13)
                    {
                        lbRep1.Visible = false;
                        txtAlerta1.Visible = false;
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal2();", true);
                        Mensaje("Notificación enviada a reprogramación exitoso", WarningType.Success);
                        ////CORREO

                       //CorreoCancelar();

                        ////CORREO
                        LimpiarNotificacion();
                        UpNotif.Update();
                        Response.Redirect("buscarAprobarNotificacion.aspx");
                    }
                    else
                    {
                        lbRep1.Text = "No se pudo reprogramar la notificación";
                        lbRep1.Visible = true;
                    }
                }
                catch (Exception Ex)
                {
                    Mensaje(Ex.Message, WarningType.Danger);
                }
            }
        }

        protected void btnCerrarReprogramarNotif_Click(object sender, EventArgs e)
        {
            txtAlerta1.Visible = false;
            lbRep1.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal2();", true);
        }

        protected void btnCancelarAprobNotif_Click(object sender, EventArgs e)
        {
            txtAlerta1.Visible = false;
            lbRep1.Visible = false;
            lbFchAprobNotif.Text = txtFechaInicio.Text;
            lbCodATMAprobNotif.Text = txtcodATMNotif.Text;
            lbNomATMAprobNotif.Text = DDLmantemientoPendiente.SelectedItem.Text;
            lbSucursalAprobNotif.Text = txtsucursalNotif.Text;
            lbTecnicoAprobNotif.Text = DLLtecResponsable.SelectedItem.Text;
            TimeSpan horainicio = TimeSpan.Parse(txtHrInicioMant.Text);
            TimeSpan horafinal = TimeSpan.Parse(txtHrFinMant.Text);
            Session["resta"] = horafinal - horainicio;
            lbhorasAprobNotif.Text = Session["resta"].ToString() + " horas";
            lbRep1.Text = "Los campos con(*) son obligatorios.";
            
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal2();", true);
        }
    }
}