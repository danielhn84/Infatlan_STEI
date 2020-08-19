using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infatlan_STEI_Comunicacion.classes;
using System.Data;

namespace Infatlan_STEI_Comunicacion.pages.mantenimiento
{
    public partial class crearNotificacion : System.Web.UI.Page
    {
        db vConexion = new db();
        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            String vEx = Request.QueryString["ex"];

            if (!Page.IsPostBack)
            {
                if (vEx != null)
                {
                    if (vEx.Equals("1"))//CREAR NOTIFICACIÓN
                    {

                        cargarDataCrearNotificacion();
                    }

                }
            }
        }
        void cargarDataCrearNotificacion()
        {
            try
            {
                DataTable vDatos = new DataTable();
                vDatos = (DataTable)Session["COMUNICACION_PCN_CREAR_NOTIFICACION_INDIVIDUAL"];
                TxFechaMantenimiento.Text = vDatos.Rows[0]["fechaMantenimiento"].ToString();
                TxNodo.Text = vDatos.Rows[0]["nombreNodo"].ToString();
                TxZona.Text = vDatos.Rows[0]["regiones"].ToString();
                TxIp.Text = vDatos.Rows[0]["ip"].ToString();
                TxImagen.Text = vDatos.Rows[0]["IOSImage"].ToString();
                TXVersion.Text = vDatos.Rows[0]["IOSVersion"].ToString();
                TxTipo.Text = vDatos.Rows[0]["tipoStock"].ToString();
                TxDirección.Text = vDatos.Rows[0]["direccion"].ToString();
                TxResponsable.Text = vDatos.Rows[0]["nombreResponsable"].ToString();
                TxIdentidadResponsable.Text = vDatos.Rows[0]["identidad"].ToString();

                DDLNombreParticipantes.Items.Clear();
                String vQuery = "STEISP_COMUNICACION_CrearNotificacion 2";
                vDatos = vConexion.obtenerDataTable(vQuery);
                DDLNombreParticipantes.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                if (vDatos.Rows.Count > 0)
                {
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLNombreParticipantes.Items.Add(new ListItem { Value = item["idUsuario"].ToString(), Text = item["nombre"].ToString() + "  " + item["apellidos"].ToString() });
                    }
                }



                DdlExternos.Items.Clear();
                vQuery = "STEISP_COMUNICACION_CrearNotificacion 9";
                vDatos = vConexion.obtenerDataTable(vQuery);
                DdlExternos.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                if (vDatos.Rows.Count > 0)
                {
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DdlExternos.Items.Add(new ListItem { Value = item["idUsuarioExterno"].ToString(), Text = item["nombre"].ToString()  });
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
        protected void DDLNombreParticipantes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (DDLNombreParticipantes.SelectedValue.Equals("0"))
                    throw new Exception("Por favor seleccione tecnico participante.");

                foreach (GridViewRow item in GVParticipantes.Rows)
                {
                    if (item.Cells[1].Text.Equals(DDLNombreParticipantes.Text))
                    {
                        throw new Exception("Ya existe tecnico responsable.");
                    }
                }

                String vQuery2 = "STEISP_COMUNICACION_CrearNotificacion 3 ," + DDLNombreParticipantes.SelectedValue;
                DataTable vDatos = vConexion.obtenerDataTable(vQuery2);               
                DataTable vDatos2 = new DataTable();

                String vId = vDatos.Rows[0]["idUsuario"].ToString();
                String vNombre = vDatos.Rows[0]["nombre"].ToString();
                String vIdentidad = vDatos.Rows[0]["identidad"].ToString();

                if (HttpContext.Current.Session["COMUNICACION_CN_TECNICOS_PARTICIPANTES"] == null)
                {
                    vDatos2.Columns.Add("idUsuario");
                    vDatos2.Columns.Add("nombre");
                    vDatos2.Columns.Add("identidad");

                    vDatos2.Rows.Add(vId, vNombre, vIdentidad);
                }
                else
                {
                    vDatos2 = (DataTable)Session["COMUNICACION_CN_TECNICOS_PARTICIPANTES"];
                    vDatos2.Rows.Add(vId, vNombre, vIdentidad);
                }
                GVParticipantes.DataSource = vDatos2;
                GVParticipantes.DataBind();
                Session["COMUNICACION_CN_TECNICOS_PARTICIPANTES"] = vDatos2;
                DDLNombreParticipantes.SelectedIndex = -1;
            }
            catch (Exception Ex) { Mensaje(Ex.Message, WarningType.Danger); }
        }
        protected void GVParticipantes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GVParticipantes.DataSource = (DataTable)Session["COMUNICACION_CN_TECNICOS_PARTICIPANTES"];
                GVParticipantes.PageIndex = e.NewPageIndex;
                GVParticipantes.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
        protected void GVParticipantes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DataTable vDatos = (DataTable)Session["COMUNICACION_CN_TECNICOS_PARTICIPANTES"];
            if (e.CommandName == "Eliminar")
            {
                String vUsuario = e.CommandArgument.ToString();
                if (Session["COMUNICACION_CN_TECNICOS_PARTICIPANTES"] != null)
                {

                    DataRow[] result = vDatos.Select("idUsuario = '" + vUsuario + "'");
                    foreach (DataRow row in result)
                    {
                        if (row["idUsuario"].ToString().Contains(vUsuario))
                            vDatos.Rows.Remove(row);
                    }
                }
            }
            GVParticipantes.DataSource = vDatos;
            GVParticipantes.DataBind();
            Session["COMUNICACION_CN_TECNICOS_PARTICIPANTES"] = vDatos;
        }
        protected void btnBuscarJefe_Click(object sender, EventArgs e)
        {
            try
            {
                if (txBuscarJefe.Text != "" || txBuscarJefe.Text != string.Empty)
                {
                    classes.LdapService vService = new classes.LdapService();
                    DataTable vDatos = vService.GetDatosUsuario("adbancat.hn", txBuscarJefe.Text);

                    GVJefesAD.DataSource = vDatos;
                    GVJefesAD.DataBind();
                    Session["COMUNICACION_CN_CORREO_JEFE_AD"] = vDatos;
                  
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
                GVJefesAD.DataSource = (DataTable)Session["COMUNICACION_CN_CORREO_JEFE_AD"];
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
                            txBuscarJefe.Text = "";
                            GVJefesAD.DataSource = null;
                            GVJefesAD.DataBind();
                            throw new Exception("Ya existe jefe de agencia.");
                        }
                    }

                    DataTable vData = new DataTable();
                    DataTable vDatos = (DataTable)Session["COMUNICACION_CN_CORREO_JEFE_AD_NOTI"];
                    string CorreoJefe = correoJefe;

                    vData.Columns.Add("Correo");
                    if (vDatos == null)
                        vDatos = vData.Clone();
                    if (vDatos != null)
                    {
                        vDatos.Rows.Add(CorreoJefe);
                    }

                    GVjefesAgencias.DataSource = vDatos;
                    GVjefesAgencias.DataBind();
                    Session["COMUNICACION_CN_CORREO_JEFE_AD_NOTI"] = vDatos;
                    UpdatePanel1.Update();
                    txBuscarJefe.Text = "";

                    GVJefesAD.DataSource = null;
                    GVJefesAD.DataBind();

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
            DataTable vDatos = (DataTable)Session["COMUNICACION_CN_CORREO_JEFE_AD_NOTI"];
            if (e.CommandName == "eliminar")
            {
                String vCorreo = e.CommandArgument.ToString();
                if (Session["COMUNICACION_CN_CORREO_JEFE_AD_NOTI"] != null)
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
            Session["COMUNICACION_CN_CORREO_JEFE_AD_NOTI"] = vDatos;
        }
        protected void GVjefesAgencias_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GVjefesAgencias.PageIndex = e.NewPageIndex;
                GVjefesAgencias.DataSource = (DataTable)Session["COMUNICACION_CN_CORREO_JEFE_AD_NOTI"];
                GVjefesAgencias.DataBind();
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }
        private void validaciones()
        {
            if (TxSysAid.Text == "" || TxSysAid.Text == string.Empty)
                throw new Exception("Favor ingrese número de SysAid.");
         
            if (TxControlCambio.Text == "" || TxControlCambio.Text == string.Empty)
                throw new Exception("Favor ingrese número del control de cambio.");

            if (TxHoraInicio.Text == "" || TxHoraInicio.Text == string.Empty)
                throw new Exception("Favor ingrese hora de inicio del mantenimiento.");

            if (TxHoraFin.Text == "" || TxHoraFin.Text == string.Empty)
                throw new Exception("Favor ingrese hora de fin del mantenimiento.");

            if (TxImpacto.Text == "" || TxImpacto.Text == string.Empty)
                throw new Exception("Favor ingrese impacto del mantenimiento.");

            if (GVjefesAgencias.Rows.Count == 0)
                throw new Exception("Favor ingrese correo de jefe de agencia.");
        }
        private void limpiarCreacionNotificacion()
        {
            TxHoraFin.Text = String.Empty;
            TxHoraInicio.Text = String.Empty;
            TxSysAid.Text = String.Empty;
            TxControlCambio.Text = String.Empty;
            TxImpacto.Text = String.Empty;

            GVParticipantes.DataSource = null;
            GVParticipantes.DataBind();

            GVJefesAD.DataSource = null;
            GVJefesAD.DataBind();

            GVjefesAgencias.DataSource = null;
            GVjefesAgencias.DataBind();

            Session["COMUNICACION_CN_TECNICOS_PARTICIPANTES"] = null;
            Session["COMUNICACION_CN_CORREO_JEFE_AD_NOTI"] = null;
            Session["COMUNICACION_CN_CORREO_JEFE_AD"] = null;

        }
        protected void BtnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                validaciones();
                LbTitulo.Text = "Enviar Notificación "+ TxNodo.Text;
                UpdatePanel3.Update();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "abrirModal();", true);
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }
        protected void BtnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                int vAcumuladorParticipantes = 0;
                int vAcumuladorJefes = 0;
                int vAcumuladorUsuarioExterno = 0;
                int vTotJefes = 0;
                int vTotParticipantes= 0;
                int vTotUsuariosExternos = 0;
                string vCorreos = "";
                DataTable vDatos = new DataTable();
                vDatos = (DataTable)Session["COMUNICACION_PCN_CREAR_NOTIFICACION_INDIVIDUAL"];
                string vidMantenimiento = vDatos.Rows[0]["idMantenimiento"].ToString();

                String vQuery1 = "STEISP_COMUNICACION_CrearNotificacion 4," + vidMantenimiento +
                                  ",'"+ TxSysAid.Text + "','" + TxControlCambio.Text+"','"+ TxHoraInicio.Text+ "','"+ TxHoraFin.Text + "','" + TxImpacto.Text + "'";
                Int32 vInfo1 = vConexion.ejecutarSql(vQuery1);


                if (Session["COMUNICACION_CN_TECNICOS_PARTICIPANTES"] != null)
                {
                    DataTable vDatosParticipantes = (DataTable)Session["COMUNICACION_CN_TECNICOS_PARTICIPANTES"];
                    vTotParticipantes = vDatosParticipantes.Rows.Count;
                    if (vDatosParticipantes.Rows.Count > 0)
                    {
                        for (int num = 0; num < vDatosParticipantes.Rows.Count; num++)
                        {
                            string vUsuario = vDatosParticipantes.Rows[num]["idUsuario"].ToString();

                            String vQuery2 = "STEISP_COMUNICACION_CrearNotificacion 5," + vidMantenimiento + ",'" + vUsuario + "'";
                            Int32 vInfo2 = vConexion.ejecutarSql(vQuery2);

                            String vQueryAcumCorreo = "STEISP_COMUNICACION_CrearNotificacion 13,'"+ vUsuario + "'";
                            DataTable vDatosAcumCorreo = vConexion.obtenerDataTable(vQueryAcumCorreo);
                            vCorreos = vCorreos + vDatosAcumCorreo.Rows[0]["correo"].ToString() + ";";

                            if (vInfo2 == 1)
                                vAcumuladorParticipantes = vAcumuladorParticipantes + 1;
                        }
                    }
                }

                if (Session["COMUNICACION_CN_CORREO_JEFE_AD_NOTI"] != null)
                {
                    DataTable vDatosJefes = (DataTable)Session["COMUNICACION_CN_CORREO_JEFE_AD_NOTI"];
                    vTotJefes = vDatosJefes.Rows.Count;
                    if (vDatosJefes.Rows.Count > 0)
                    {
                        for (int num = 0; num < vDatosJefes.Rows.Count; num++)
                        {
                            string vCorreoJefe = vDatosJefes.Rows[num]["Correo"].ToString();

                            String vQuery2 = "STEISP_COMUNICACION_CrearNotificacion 6," + vidMantenimiento+",'" + vCorreoJefe + "'";
                            Int32 vInfo2 = vConexion.ejecutarSql(vQuery2);

                            vCorreos = vCorreos + vCorreoJefe + ";";

                            if (vInfo2 == 1)
                                vAcumuladorJefes = vAcumuladorJefes + 1;
                        }
                    }
                }


                if (Session["COMUNICACION_CN_PERSONAL_EXTERNO"] != null)
                {
                    DataTable vDatosUsuariosExternos = (DataTable)Session["COMUNICACION_CN_PERSONAL_EXTERNO"];
                    vTotUsuariosExternos = GvExterno.Rows.Count;
                    if (GvExterno.Rows.Count > 0)
                    {
                        for (int num = 0; num < GvExterno.Rows.Count; num++)
                        {
                            string vIdUsuarioExterno = vDatosUsuariosExternos.Rows[num]["idUsuarioExterno"].ToString();

                            String vQuery4 = "STEISP_COMUNICACION_CrearNotificacion 11," + vidMantenimiento + "," + vIdUsuarioExterno ;
                            Int32 vInfo4 = vConexion.ejecutarSql(vQuery4);
                            
                            if(vInfo4==1)
                                vAcumuladorUsuarioExterno = vAcumuladorUsuarioExterno + 1;
                        }
                    }
                }

                DataTable vDatosResponsable = (DataTable)Session["COMUNICACION_PCN_CREAR_NOTIFICACION_INDIVIDUAL"];
                string vUsuarioResponsable = vDatosResponsable.Rows[0]["responsable"].ToString();

                String vQueryCorreoResponsable = "STEISP_COMUNICACION_CrearNotificacion 13,'" + vUsuarioResponsable + "'";
                DataTable vDatosCorreoResponsable = vConexion.obtenerDataTable(vQueryCorreoResponsable);
                string vCorreoResponsable = vDatosCorreoResponsable.Rows[0]["correo"].ToString();

                String vQuerySuscripcion = "STEISP_COMUNICACION_CrearNotificacion 12,'" +vCorreos+"','"+ vCorreoResponsable+ "','Notificación Mantenimiento Equipo Comunicacion',0,"+ vidMantenimiento;
                Int32 vInfoSuscripcion = vConexion.ejecutarSql(vQuerySuscripcion);



                if (vInfo1 == 1 && vAcumuladorParticipantes== vTotParticipantes && vAcumuladorJefes == vTotJefes && vAcumuladorUsuarioExterno== vTotUsuariosExternos)
                {
                    limpiarCreacionNotificacion();    
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "cerrarModal();", true);
                    Response.Redirect("/pages/mantenimiento/pendientesCrearNotificacion.aspx?ex=1");
                    
                }
                else
                {
                    limpiarCreacionNotificacion();
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "cerrarModal();", true);
                    Response.Redirect("/pages/mantenimiento/pendientesCrearNotificacion.aspx?ex=2");
                }
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }
        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
               limpiarCreacionNotificacion();
               Response.Redirect("/pages/mantenimiento/pendientesCrearNotificacion.aspx");
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }
        protected void DdlExternos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (DdlExternos.SelectedValue.Equals("0"))
                    throw new Exception("Por favor seleccione tecnico participante.");

                foreach (GridViewRow item in GvExterno.Rows)
                {
                    String vEvaluar = DdlExternos.SelectedItem.Text;

                    if (item.Cells[2].Text.Equals(vEvaluar))
                    {
                        throw new Exception("Ya existe usuario externo agregado en la lista.");
                    }
                }

                String vQuery2 = "STEISP_COMUNICACION_CrearNotificacion 10 ," + DdlExternos.SelectedValue;
                DataTable vDatos = vConexion.obtenerDataTable(vQuery2);
                DataTable vDatos2 = new DataTable();

                String vId = vDatos.Rows[0]["idUsuarioExterno"].ToString();
                String vNombre = vDatos.Rows[0]["nombre"].ToString();
                String vIdentidad = vDatos.Rows[0]["identidad"].ToString();
                String vEmpresa = vDatos.Rows[0]["empresa"].ToString();

                if (HttpContext.Current.Session["COMUNICACION_CN_PERSONAL_EXTERNO"] == null)
                {
                    vDatos2.Columns.Add("idUsuarioExterno");
                    vDatos2.Columns.Add("nombre");
                    vDatos2.Columns.Add("identidad");
                    vDatos2.Columns.Add("empresa");

                    vDatos2.Rows.Add(vId, vNombre, vIdentidad,vEmpresa);
                }
                else
                {
                    vDatos2 = (DataTable)Session["COMUNICACION_CN_PERSONAL_EXTERNO"];
                    vDatos2.Rows.Add(vId, vNombre, vIdentidad, vEmpresa);
                }
                GvExterno.DataSource = vDatos2;
                GvExterno.DataBind();
                Session["COMUNICACION_CN_PERSONAL_EXTERNO"] = vDatos2;
                DdlExternos.SelectedIndex = -1;
            }
            catch (Exception Ex) { Mensaje(Ex.Message, WarningType.Danger); }
        }
        protected void GvExterno_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GvExterno.DataSource = (DataTable)Session["COMUNICACION_CN_PERSONAL_EXTERNO"];
                GvExterno.PageIndex = e.NewPageIndex;
                GvExterno.DataBind();
            }
            catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
        protected void GvExterno_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DataTable vDatos = (DataTable)Session["COMUNICACION_CN_PERSONAL_EXTERNO"];
            if (e.CommandName == "Eliminar")
            {
                String vIdUsuarioExterno = e.CommandArgument.ToString();
                if (Session["COMUNICACION_CN_PERSONAL_EXTERNO"] != null)
                {

                    DataRow[] result = vDatos.Select("idUsuarioExterno = '" + vIdUsuarioExterno + "'");
                    foreach (DataRow row in result)
                    {
                        if (row["idUsuarioExterno"].ToString().Contains(vIdUsuarioExterno))
                            vDatos.Rows.Remove(row);
                    }
                }
            }
            GvExterno.DataSource = vDatos;
            GvExterno.DataBind();
            Session["COMUNICACION_CN_PERSONAL_EXTERNO"] = vDatos;
        }

        protected void LbAñadirCorreoFiliar_Click(object sender, EventArgs e)
        {
            try
            {
                if (TxCorreo.Text == "" || TxCorreo.Text == string.Empty)
                    throw new Exception("Favor ingrese correo de la persona a quien quiere notificar.");

                DataTable vData = new DataTable();
                DataTable vDatos = (DataTable)Session["COMUNICACION_CORREOS_FILIARES"];

                vData.Columns.Add("correo");

                if (vDatos == null)
                    vDatos = vData.Clone();
                if (vDatos != null)
                {
                    if (vDatos.Rows.Count < 1)
                        vDatos.Rows.Add(TxCorreo.Text);
                    else
                    {
                        Boolean vRegisteredFilial = false;
                        for (int i = 0; i < vDatos.Rows.Count; i++)
                        {
                            if (TxCorreo.Text == vDatos.Rows[i]["correo"].ToString())
                            {
                                TxCorreo.Text = string.Empty;
                                UpdatePanel5.Update();
                                vRegisteredFilial = true;

                                throw new Exception("Correo ya esta agregado a la lista, favor ingrese otro.");
                                     

                            }
                        }
                        if (!vRegisteredFilial)
                            vDatos.Rows.Add(TxCorreo.Text);
                    }
                }
                GvFiliares.DataSource = vDatos;
                GvFiliares.DataBind();
                Session["COMUNICACION_CORREOS_FILIARES"] = vDatos;

                TxCorreo.Text = string.Empty;
                UpdatePanel5.Update();



            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }


    }
}
