using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using Infatlan_STEI_ATM.clases;


namespace Infatlan_STEI_ATM.pagesATM
{
    public partial class solicitudATM : System.Web.UI.Page
    {
        bd vConexion = new bd();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["NOTI"] = null;

            if (!Page.IsPostBack)
            {
                //LimpiarNotificacion();
                //Session["ATM_EMPLEADOS"] = null;
                cargarData();
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
                    vData.Rows.Add(vDatos.Rows[0]["idUsuario"].ToString(), vDatos.Rows[0]["nombre"].ToString(), vDatos.Rows[0]["identidad"].ToString());
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

            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }

        }

        protected void btnEnviarNotificacion_Click(object sender, EventArgs e)
        {
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
                    validacionesNotificacion();
                    TimeSpan horainicio = TimeSpan.Parse(txtHrInicioMant.Text);
                    TimeSpan horafinal = TimeSpan.Parse(txtHrFinMant.Text);
                    Session["resta"] = horafinal - horainicio;
                    lbHrMantenimiento.Text = Session["resta"].ToString() + " horas";
                    lbTecnicoResp.Text = DLLtecResponsable.SelectedItem.Text;
                    MostrarModal();
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
            lbNombreATM.Text = Session["NomATM"].ToString();
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

        protected void btnModalEnviarNotificacion_Click(object sender, EventArgs e)
        {
            string usu = "acedillo";
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
                        "'" + autorizarATM + "','" + DLLtecResponsable.SelectedValue + "','" + txtsysaid.Text + "', " + estado + ",'" + usu + "'";
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
                        Mensaje("Mantenimiento modificado con éxito", WarningType.Success);
                        LimpiarNotificacion();
                        UpNotif.Update();
                        cargarData();
                        txtbuscarJefeNotif.Visible = false;
                        btnBuscarJefe.Visible = false;
                        DIVBuscarJefes.Visible = false;
                        lbJefeAgencia.Visible = false;
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
                Session.Clear();
            }
            else
            {
                CancelarNotificacion();

                Session.Clear();
            }
        }
        void CancelarNotificacion()
        {
            string usu = "acedillo";
            string autorizarATM = "";
            int estado = 1;
            try
            {
                string vQuery = "STEISP_ATM_Notificaciones 2, '" + Session["ID"] + "','" + txtcancelarNotif.Text + "', '" + txtHrFinMant.Text + "'," +
                    "'" + autorizarATM + "','" + DLLtecResponsable.SelectedItem.Text + "','" + txtsysaid.Text + "', " + estado + ",'" + usu + "'";
                Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                if (vInfo == 1)
                {
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
                    string vQuery = "STEISP_ATM_UsuariosMantenimiento 2, '" + Session["ID"].ToString() + "','" + correos + "'";
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

        protected void txtFechaInicio_TextChanged(object sender, EventArgs e)
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
                lbJefeAgencia.Visible = false;
                limpiarDatosATM();
                DIVBuscarJefes.Visible = false;
                lbJefeAgencia.Visible = false;
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


        //protected void DDLjefesAgencias_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {

        //        String vQuery2 = "STEISP_AGENCIA_CreacionNotificacion 7, " + DDLjefesAgencias.SelectedValue;
        //        DataTable vDatos2 = vConexion.ObtenerTabla(vQuery2);
        //        DataTable vData2 = (DataTable)Session["ATM_EMPLEADOS2"];
        //        if (vData2 == null)
        //            vData2 = vDatos2.Clone();
        //        if (vDatos2 != null)
        //            vData2.Rows.Add(vDatos2.Rows[0]["correo"].ToString(), vDatos2.Rows[0]["nombre"].ToString(), vDatos2.Rows[0]["identidad"].ToString());
        //        GVjefesAgencias.DataSource = vData2;
        //        GVjefesAgencias.DataBind();
        //        Session["ATM_EMPLEADOS2"] = vData2;
        //    }
        //    catch (Exception Ex)
        //    {
        //    }
        //}

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
                        lbJefeAgencia.Visible = false;
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
                        lbJefeAgencia.Visible = true;
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
                        lbJefeAgencia.Visible = false;
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
    }
}