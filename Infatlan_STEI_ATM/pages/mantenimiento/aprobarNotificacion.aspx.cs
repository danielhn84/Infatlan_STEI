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

namespace Infatlan_STEI_ATM.pages.mantenimiento
{
    public partial class aprobarNotificacion : System.Web.UI.Page
    {
        bd vConexion = new bd();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Limpiar();
                cargarDataNotificacion();
                llenarForm();

            }
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
        void cargarDataNotificacion()
        {
            try
            {
                DataTable vDatos = new DataTable();
                vDatos = vConexion.ObtenerTabla("STEISP_ATM_Generales 16, '" + Session["codNotificacion"] + "'");
                GVBusqueda.DataSource = vDatos;
                GVBusqueda.DataBind();
                Session["ATM_NOTIF_TECNICOS"] = vDatos;
                Session["UPDATEATM"] = 1;

            }
            catch (Exception Ex)
            {

            }

            try
            {
                DataTable vDatos = new DataTable();
                vDatos = vConexion.ObtenerTabla("STEISP_ATM_Generales 17, '" + Session["codNotificacion"] + "'");
                GVLlenaJefeApruebaNotif.DataSource = vDatos;
                GVLlenaJefeApruebaNotif.DataBind();
                Session["ATM_NOTIFJEFESAGEN"] = vDatos;
                Session["UPDATEATM"] = 1;

            }
            catch (Exception Ex)
            {

            }

        }
        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        void llenarForm()
        {
            txtFechaInicio.Text = Session["fechaMantATM"].ToString();           
            txtAprobNotifMantenimiento.Text= Session["autorizadoATM"].ToString();
            if (txtAprobNotifMantenimiento.Equals(true))
            {
                txtAprobNotifMantenimiento.Text = "Si";
            }
            else
            {
                txtAprobNotifMantenimiento.Text = "No";
            }
           

            txtcancelarNotif.Text = Session["cancelaATM"].ToString();
            txtHrInicioMant.Text = Session["hrInicioATM"].ToString();
            txtHrFinMant.Text = Session["hrfinATM"].ToString();
            txtsysaid.Text = Session["sysaidATM"].ToString();
            txtAprobNotifATM.Text = Session["NomATM"].ToString();
            txtcodATMNotif.Text = Session["codATM"].ToString();
            txtUbicacionATM.Text = Session["ubicacionATM"].ToString();
            txtdireccion.Text = Session["direccionATM"].ToString();
            txtsucursalNotif.Text = Session["SucursalATM"].ToString();
            txtipNotif.Text = Session["IPATM"].ToString();
            txtzonaNotif.Text = Session["zonaATM"].ToString();
            txtAprobNotifTecnicoResponsable.Text = Session["tecnicoATM"].ToString();
            txtidentidadTecResponsable.Text = Session["identidad"].ToString();

        }

        protected void btnEnviarAprobNotif_Click(object sender, EventArgs e)
        {
            Session["restaAprobNotifATM"] = null;
            txtcomentarioReprogramaNotif.Text = string.Empty;
            try
            {
                if (Session["autorizadoATM"].ToString() =="1" )
                {                  
                        lbTecnicoResp.Text = "No disponible";
                        lbHrMantenimiento.Text = "No disponible";
                        MostrarModal();
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);
                }
                else
                {                   
                    TimeSpan horainicio = TimeSpan.Parse(txtHrInicioMant.Text);
                    TimeSpan horafinal = TimeSpan.Parse(txtHrFinMant.Text);
                    Session["restaAprobNotifATM"] = horafinal - horainicio;
                    lbHrMantenimiento.Text = Session["restaAprobNotifATM"].ToString() + " horas";
                    lbTecnicoResp.Text = txtAprobNotifTecnicoResponsable.Text;
                    MostrarModal();
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);
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
            lbNombreATM.Text = txtAprobNotifATM.Text;
            lbsucursalATM.Text = txtsucursalNotif.Text;
            //lbTecnicoResp.Text = DLLtecResponsable.SelectedItem.Text;
            //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);
        }
        void MostrarModalReprogramar()
        {
            lbFchAprobNotif.Text = txtFechaInicio.Text;
            lbCodATMAprobNotif.Text = txtcodATMNotif.Text;
            lbNomATMAprobNotif.Text = txtAprobNotifATM.Text;
            lbSucursalAprobNotif.Text = txtsucursalNotif.Text;
            //lbTecnicoResp.Text = DLLtecResponsable.SelectedItem.Text;
            //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);
        }

        protected void btnCancelarAprobNotif_Click(object sender, EventArgs e)
        {
            lbRep1.Visible = false;
            Session["restaAprobNotifATM"] = null;
            try
            {
                if (Session["autorizadoATM"].ToString() == "1")
                {
                    lbTecnicoAprobNotif.Text = "No disponible";
                    lbFchAprobNotif.Text = "No disponible";
                    MostrarModalReprogramar();
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal2();", true);
                }
                else
                {
                    TimeSpan horainicio = TimeSpan.Parse(txtHrInicioMant.Text);
                    TimeSpan horafinal = TimeSpan.Parse(txtHrFinMant.Text);
                    Session["restaAprobNotifATM"] = horafinal - horainicio;
                    lbhorasAprobNotif.Text = Session["restaAprobNotifATM"].ToString() + " horas";
                    lbTecnicoAprobNotif.Text = txtAprobNotifTecnicoResponsable.Text;
                    MostrarModalReprogramar();
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal2();", true);
                }

            }
            catch (Exception ex)
            {

                Mensaje(ex.Message, WarningType.Danger);
            }

        }

        protected void btnModalAprobNotificacion_Click(object sender, EventArgs e)
        {
            
            try
            {
                string vQuery = "STEISP_ATM_Aprobaciones 1, '" + Session["codNotificacion"] + "','" + txtcomentarioReprogramaNotif.Text + "', '" + Session["usuATM"].ToString() + "'";                
                Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                if (vInfo == 1)
                {                   
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
                    Mensaje("Notificación aprobada con éxito", WarningType.Success);
                    LimpiarNotificacion();
                    UpNotif.Update();
                    Session.Clear();
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

        protected void btnModalCerrarAprobNotificacion_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
        }
        void LimpiarNotificacion()
        {
            Session.Clear();
            GVBusqueda.DataSource = null;
            GVBusqueda.DataBind();
            Session["NOTI"] = null;
            //DDLrealizarMant.SelectedValue = "0";
            txtcancelarNotif.Text = string.Empty;
            txtAprobNotifTecnicoResponsable.Text = string.Empty;
            txtHrInicioMant.Text = string.Empty;
            txtHrFinMant.Text = string.Empty;
            txtFechaInicio.Text = string.Empty;
            txtcodATMNotif.Text = string.Empty;
            txtsysaid.Text = string.Empty;
            txtAprobNotifATM.Text = string.Empty;
            txtUbicacionATM.Text = string.Empty;
            txtdireccion.Text = string.Empty;
            txtsucursalNotif.Text = string.Empty;
            txtipNotif.Text = string.Empty;
            txtzonaNotif.Text = string.Empty;
            txtAprobNotifMantenimiento.Text = string.Empty;
            txtidentidadTecResponsable.Text = string.Empty;
            UpNotif.Update();
        }

        protected void btnReprogramarNotif_Click(object sender, EventArgs e)
        {
           
            if (txtcomentarioReprogramaNotif.Text == "" || txtcomentarioReprogramaNotif.Text == string.Empty)
            {
                lbRep1.Text = "No puede dejar el comentario de reprogramación vacio";
                lbRep1.Visible = true;
            }
            else
            {
                try
                {
                    string vQuery = "STEISP_ATM_Aprobaciones 2, '" + Session["codNotificacion"] + "','" + txtcomentarioReprogramaNotif.Text + "', '" + Session["usuATM"].ToString() + "'";
                    Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    if (vInfo == 1)
                    {
                        lbRep1.Visible = false;
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal2();", true);
                        Mensaje("Notificación enviada a reprogramación exitoso", WarningType.Success);
                        LimpiarNotificacion();
                        UpNotif.Update();
                        Session.Clear();
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
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal2();", true);
        }
    }
}