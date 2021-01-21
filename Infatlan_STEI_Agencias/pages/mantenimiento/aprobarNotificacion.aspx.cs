using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Configuration;
using Infatlan_STEI_Agencias.classes;


namespace Infatlan_STEI_Agencias.pages
{
    public partial class AprobarNotificacion : System.Web.UI.Page
    {
        db vConexion = new db();
        protected void Page_Load(object sender, EventArgs e){
            if (!Page.IsPostBack){
                if (Convert.ToBoolean(Session["AUTH"])){
                    cargarDatos();
                }else {
                    Response.Redirect("/login.aspx");
                }
            }
        }
        
        private void cargarDatos() {
            try{

                String vQuery = "STEISP_AGENCIA_AprobarNotificacion 1";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                GVBusqueda.DataSource = vDatos;
                GVBusqueda.DataBind();
                Session["AG_CN_MANTENIMIENTOS_PENDIENTES_APROBAR"] = vDatos;


                DDLMotivo.Items.Clear();
                vQuery = "STEISP_AGENCIA_AprobarNotificacion 7";
                vDatos = vConexion.obtenerDataTable(vQuery);
                DDLMotivo.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });

                if (vDatos.Rows.Count > 0)
                {
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLMotivo.Items.Add(new ListItem { Value = item["id"].ToString(), Text = item["motivo"].ToString() });                       
                    }
                }

            }
            catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
            
        }
        
        public void Mensaje(string vMensaje, WarningType type){
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        void CorreoSuscripcion()
        {
            int vIDMantenimiento = Convert.ToInt32(Session["AG_CN_ID_MANTENIMIENTO"]);

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
            for (int i = 0; i < vDatosZona.Rows.Count; i++)
            {
                vZonaAgencia = vDatosZona.Rows[i]["Zona"].ToString();
            }
                string vCorreoEncargadoZona = "";
                if ( vZonaAgencia== "1")
                    vCorreoEncargadoZona = "emontoya@bancatlan.hn";
                if (vZonaAgencia == "2")
                    vCorreoEncargadoZona = "jdgarcia@bancatlan.hn";
                if (vZonaAgencia == "3")
                    vCorreoEncargadoZona = "acalderon@bancatlan.hn";

                //string vDepto = "";
                //DataTable vDatosDepto = new DataTable();
                //String vQueryDepto = "STEISP_ATM_Generales 48,'" + txtcodATMNotif.Text + "'";
                //vDatosDepto = vConexion.ObtenerTabla(vQueryDepto);
                //foreach (DataRow item in vDatosDepto.Rows)
                //{
                //    vDepto = item["Depto"].ToString();
                //}
                //if (vDepto == "18")
                //    vCorreoEncargadoZona = "acalderon@bancatlan.hn;jdgarcia@bancatlan.hn";

                string vReporteViaticos = "Notificacion";
                string vCorreoAdmin = "acedillo@bancatlan.hn";
            //string vCorreoCopia = "acamador@bancatlan.hn"+";";
            //string vCorreoCopia = "eurrea@bancatlan.hn;unidadatmkiosco@bancatlan.hn;" + vCorreoEncargadoZona;
            string vCorreoCopia = "eurrea@bancatlan.hn;" + vCorreoEncargadoZona;
            //vCorreosTodos = vCorreosTecnicos + vCorreosJefes + vCorreoAdmin;
            vCorreosTodos = vCorreoResponsable + vCorreosTecnicos + vCorreosJefes;
                string vAsuntoRV = "Formato de notificación";
                string vBody = "Formato de notificación";

                string vQueryRep = "STEISP_AGENCIA_AprobarNotificacion 13,'"+ vIDMantenimiento + "','" + vReporteViaticos + "','" + vCorreosTodos + "','" + vCorreoCopia + "','" + vAsuntoRV + "','" + vBody + "'";
                vConexion.ejecutarSql(vQueryRep);
            
        }

        void EnviarCorreo()
        {
            SmtpService vService = new SmtpService();
            string vZonaAgencia = "";
            string vIDMantenimiento = Convert.ToString(Session["AG_CN_ID_MANTENIMIENTO"]);
            string vLugar = Session["vLugar"].ToString();

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
                            "Notificación de Mantenimiento Agencia",
                            "Buen día, se le notifica que se aprobó solicitud de mantenimiento, el encargado es " + vDatosTecnicoResponsable.Rows[0]["Nombre"].ToString() + ", mantenimiento a Agencia " + vLugar,
                              "El usuario <b>" + item["Nombre"].ToString() + "</b> aprobó: <br> Notificación de Mantenimiento",
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
                        "Buen día, se le notifica que se aprobó solicitud de mantenimiento, el encargado es " + item["Nombre"].ToString() + ", mantenimiento a agencia " +vLugar,
                          "El usuario <b>" + vDatos.Rows[0]["Nombre"].ToString() + "</b> aprobó: <br> Notificación de Mantenimiento de Agencia al que ha sido asignado como responsable.",
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
                        "Buen día, se le notifica que se aprobó solicitud de mantenimiento, el encargado es " + vDatosTecnicoResponsable.Rows[0]["Nombre"].ToString() + ", mantenimiento a Agencia " + vLugar,
                          "El usuario <b>" + vDatos.Rows[0]["Nombre"].ToString() + "</b> aprobó: <br> Notificación de Mantenimiento de Agencia al que ha sido asignado como parte del equipo de trabajo",
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
                                "Buen día, se le notifica que se aprobó solicitud de mantenimiento, el encargado es " + vDatosTecnicoResponsable.Rows[0]["Nombre"].ToString() + ", mantenimiento a Agencia " + vLugar,
                                  "Se le informa que dicho mantenimiento se hará en la agencia al que usted se encuentra asignado.",
                                   "",
                                   ""
                            );
                    }
                }
            }

        }

        void EnviarCorreoCancelar()
        {
            SmtpService vService = new SmtpService();
            string vZonaAgencia = "";
            string vIDMantenimiento = Convert.ToString(Session["AG_CN_ID_MANTENIMIENTO"]);
            string vLugar = Session["vLugar"].ToString();

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
                        //string vNombre = "EDWIN ALBERTO URREA PENA";
                        vService.EnviarMensaje(ConfigurationManager.AppSettings["STEIMail"],
                                typeBody.EnvioCorreo,
                                "Notificación de Mantenimiento Agencia",
                                "Buen día, se le notifica que se canceló solicitud de mantenimiento, el encargado es " + vDatosTecnicoResponsable.Rows[0]["Nombre"].ToString() + ", mantenimiento a Agencia " + vLugar,
                                  "El usuario <b>" + item["Nombre"].ToString() + "</b> canceló: <br> Notificación de Mantenimiento<br>Motivo: " + TxDetalle.Text,
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
                        "Buen día, se le notifica que se canceló solicitud de mantenimiento, el encargado es " + item["Nombre"].ToString() + ", mantenimiento a agencia " + vLugar,
                          "El usuario <b>" + vDatos.Rows[0]["Nombre"].ToString() + "</b> canceló: <br> Notificación de Mantenimiento de Agencia al que ha sido asignado como responsable.<br>Motivo: " + TxDetalle.Text,
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
                        "Buen día, se le notifica que se canceló solicitud de mantenimiento, el encargado es " + vDatosTecnicoResponsable.Rows[0]["Nombre"].ToString() + ", mantenimiento a Agencia " + vLugar,
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
                                "Buen día, se le notifica que se canceló solicitud de mantenimiento, el encargado es " + vDatosTecnicoResponsable.Rows[0]["Nombre"].ToString() + ", mantenimiento a Agencia " + vLugar,
                                  "Se le informa que dicho mantenimiento se canceló en la agencia al que usted se encuentra asignado.<br>Motivo: " + TxDetalle.Text,
                                   "",
                                   ""
                            );
                    }
                }
            }

        }

        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e){
            if (e.CommandName == "Aprobar"){
                limpiarModalAprobarNotificacion();
                string vIdMantenimiento = e.CommandArgument.ToString();
                Session["AG_CN_ID_MANTENIMIENTO"] = vIdMantenimiento;

                String vQuery = "STEISP_AGENCIA_AprobarNotificacion 3," + vIdMantenimiento;
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                foreach (DataRow item in vDatos.Rows)
                {
                    string vIdMantenimientoSelect = item["id_Mantenimiento"].ToString();
                    string vLugar = item["Lugar"].ToString();
                    Session["vLugar"] = item["Lugar"].ToString();
                    string vFecha = item["fecha"].ToString();
                    string vResponsable = item["Responsable"].ToString();
                    string vArea = item["Area"].ToString();

                    TxIdMant.Text = vIdMantenimientoSelect;
                    //TxLugar.Text = vLugar;
                    TxFecha.Text = vFecha;
                    TxTecnicoResponsable.Text = vResponsable;
                    TxArea.Text = vArea;
                    lbTitulo.Text = "Aprobar Notificación " + vLugar;
                    UpdatePanel1.Update();
                }

                String vQuery2 = "STEISP_AGENCIA_AprobarNotificacion 5," + vIdMantenimiento;
                DataTable vDatos2 = vConexion.obtenerDataTable(vQuery2);
                foreach (DataRow item in vDatos2.Rows)
                {
                    string vTecnicoParticipantes = item["Participantes"].ToString();                    
                    TxParticipantes.Text = TxParticipantes.Text + vTecnicoParticipantes + "\n";                
                }

                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);

               
            }else if(e.CommandName == "Cancelar"){
                Div2.Visible = false;
                UpdateModal.Update();

                limpiarModalCancelarNotificacion();
            
                string vIdMantenimiento = e.CommandArgument.ToString();
                Session["AG_CN_ID_MANTENIMIENTO"] = vIdMantenimiento;
                String vQuery = "STEISP_AGENCIA_AprobarNotificacion 3," + vIdMantenimiento;
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                string vLugar = vDatos.Rows[0]["Lugar"].ToString();
                Session["vLugar"] = vDatos.Rows[0]["Lugar"].ToString();
                lbTituloCancelar.Text = "Cancelar Notificación " + vLugar;
                UpdatePanel6.Update();

                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModalCancelacion();", true);
            }

        }
        
        protected void btnModalAprobarNotificacion_Click(object sender, EventArgs e){
            try{
                String vQuery = "STEISP_AGENCIA_AprobarNotificacion 2," + Session["AG_CN_ID_MANTENIMIENTO"] +"," + Session["USUARIO"];
                Int32 vInfo = vConexion.ejecutarSql(vQuery);

                if (vInfo == 1){
                    CorreoSuscripcion();
                    EnviarCorreo();
                    Mensaje("Notificación aprobada con exito. ", WarningType.Success);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
                }

                cargarDatos();
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
        
        protected void TxBuscarAgencia_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cargarDatos();
                String vBusqueda = TxBuscarAgencia.Text;
                DataTable vDatos = (DataTable)Session["AG_CN_MANTENIMIENTOS_PENDIENTES_APROBAR"];

                if (vBusqueda.Equals(""))
                {
                   
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                    UPGvBusqueda.Update();
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
                    vDatosFiltrados.Columns.Add("Hr_Inicio");
                    vDatosFiltrados.Columns.Add("Hr_Fin");
                    vDatosFiltrados.Columns.Add("Lugar");
                    vDatosFiltrados.Columns.Add("Cod_Agencia");
                    vDatosFiltrados.Columns.Add("Responsable");
                    vDatosFiltrados.Columns.Add("Area");

                    foreach (DataRow item in filtered)
                    {
                        vDatosFiltrados.Rows.Add(
                            item["id_Mantenimiento"].ToString(),
                            item["fecha"].ToString(),
                            item["Hr_Inicio"].ToString(),
                            item["Hr_Fin"].ToString(),
                            item["Lugar"].ToString(),
                            item["Cod_Agencia"].ToString(),
                            item["Responsable"].ToString(),
                            item["Area"].ToString()
                            );
                    }

                    GVBusqueda.DataSource = vDatosFiltrados;
                    GVBusqueda.DataBind();
                    Session["AG_CN_MANTENIMIENTOS_PENDIENTES_APROBAR"] = vDatosFiltrados;
                    UPGvBusqueda.Update();
                }


            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }
        
        private void validaciones()
        {           
            if (DDLMotivo.SelectedValue.Equals("0"))
                throw new Exception("Favor seleccionar motivo de cancelación del mantenimiento. ");
            
            if (TxDetalle.Text.Equals(""))
                throw new Exception("Favor ingrese detalle de la cancelación del mantenimiento. ");
          
        }
        
        protected void BtnCancelarNoti_Click(object sender, EventArgs e)
        {
            try
            {
                validaciones();
                EnviarCorreoCancelar();
                String vQuery = "STEISP_AGENCIA_AprobarNotificacion  4," + Session["AG_CN_ID_MANTENIMIENTO"] + "," + Session["USUARIO"] + "," + "'" + DDLMotivo.SelectedItem.Text + "'" + "," + "'" + TxDetalle.Text + "'";
                Int32 vInfo = vConexion.ejecutarSql(vQuery);

                //String vQuery1 = "STEISP_AGENCIA_AprobarNotificacion  6," + Session["AG_CN_ID_MANTENIMIENTO"] ;
                //Int32 vInfo1 = vConexion.ejecutarSql(vQuery1);

                if (vInfo == 1)
                {
                    Mensaje("Notificacón cancelada con exito, esta pendiente que el jefe o suplente reprogramen el mantenimiento", WarningType.Success);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModalCancelacion();", true);
                }
                cargarDatos();
            }
            catch (Exception ex)
            {
                LbMensajeModalError.Text = ex.Message;
                Div2.Visible = true;
                UpdateModal.Update();
               
            }
        }
        
        private void limpiarModalAprobarNotificacion()
        {
            TxIdMant.Text = string.Empty;
            //TxLugar.Text = string.Empty;
            TxArea.Text = string.Empty;
            TxFecha.Text = string.Empty;
            TxTecnicoResponsable.Text = string.Empty;
            TxParticipantes.Text = string.Empty;
        }
        
        private void limpiarModalCancelarNotificacion()
        {
            DDLMotivo.SelectedIndex = -1;
            TxDetalle.Text = string.Empty;
       
        }
        
        protected void GVBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GVBusqueda.PageIndex = e.NewPageIndex;
                GVBusqueda.DataSource = (DataTable)Session["AG_CN_MANTENIMIENTOS_PENDIENTES_APROBAR"];
                GVBusqueda.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void DDLMotivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Div2.Visible = false;
            UpdateModal.Update();
        }
    }
}