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
    public partial class pendientesCrearNotificacion : System.Web.UI.Page
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
                //if (Convert.ToBoolean(Session["AUTH"]))
                //{

                if (vEx == null)
                {
                    cargarDatos();
                    UpPendientesCrearNotificacion.Update();
                }
                else if (vEx.Equals("1"))
                {
                    vEx = null;
                    String vRe = "Notificación enviada con éxito.";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Pop", "window.alert('" + vRe + "')", true);
                    cargarDatos();
                    UpPendientesCrearNotificacion.Update();
                }
                else if (vEx.Equals("2"))
                {
                    vEx = null;
                    String vRe = "No se pudo enviar la notificación, favor contactarse con el administrador del sistema.";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Pop", "window.alert('" + vRe + "')", true);
                    cargarDatos();
                    UpPendientesCrearNotificacion.Update();
                }
                else if (vEx.Equals("3"))
                {
                    vEx = null;
                    String vRe = "Notificación cancelada con exito.";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Pop", "window.alert('" + vRe + "')", true);
                    cargarDatos();
                    UpPendientesCrearNotificacion.Update();
                }
                else if (vEx.Equals("4"))
                {
                    vEx = null;
                    String vRe = "No se pudo cancelar la notificación, favor contactarse con el administrador del sistema.";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Pop", "window.alert('" + vRe + "')", true);
                    cargarDatos();
                    UpPendientesCrearNotificacion.Update();
                }

                //}
                //else
                //{
                //    Response.Redirect("/login.aspx");
                //}
            }

        }
        
        private void cargarDatos()
        {
            try
            {
                String vQuery = "STEISP_COMUNICACION_AsignarResponsable 4,'"+ Session["USUARIO"]+"'";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                GvPendientesCrearNotificacion.DataSource = vDatos;
                GvPendientesCrearNotificacion.DataBind();
                Session["COMUNICACION_PENDIENTES_CREAR_NOTIFICACION"] = vDatos;



            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
        
        protected void GvPendientesCrearNotificacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GvPendientesCrearNotificacion.PageIndex = e.NewPageIndex;
                GvPendientesCrearNotificacion.DataSource = (DataTable)Session["COMUNICACION_PENDIENTES_CREAR_NOTIFICACION"];
                GvPendientesCrearNotificacion.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }

        }
        
        protected void GvPendientesCrearNotificacion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string vIdMantenimiento= e.CommandArgument.ToString();
            Session["COMUNICACION_ID_MANTENIMIENTO"] = vIdMantenimiento;

            if (e.CommandName == "Crear")
            {
                String vQuery = "STEISP_COMUNICACION_CrearNotificacion 1,'" + vIdMantenimiento + "'";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);               
                Session["COMUNICACION_PCN_CREAR_NOTIFICACION_INDIVIDUAL"] = vDatos;



                Response.Redirect("/sites/comunicaciones/pages/mantenimiento/crearNotificacion.aspx?ex=1");
            }
            else if (e.CommandName == "Cancelar")
            {
                String vQuery = "STEISP_COMUNICACION_CrearNotificacion 1,'" + vIdMantenimiento + "'";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                Titulo.Text = vDatos.Rows[0]["nombreNodo"].ToString();
                TxIdMantenimiento.Text = vDatos.Rows[0]["idMantenimiento"].ToString();

                DdlMotivo.Items.Clear();
                vQuery = "STEISP_COMUNICACION_CrearNotificacion 7";
                vDatos = vConexion.obtenerDataTable(vQuery);
                DdlMotivo.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                if (vDatos.Rows.Count > 0)
                {
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DdlMotivo.Items.Add(new ListItem { Value = item["idMotivo"].ToString(), Text = item["motivo"].ToString() });
                    }
                }
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "abrirModalCancelarNoti();", true);
                UpdatePanel5.Update();
            }
        }

        protected void DdlMotivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (DdlMotivo.SelectedValue.Equals("1"))
                {
                    DDLNombreResponsable.Items.Clear();
                    string vQuery = "STEISP_COMUNICACION_AsignarResponsable 2";
                    DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                    DDLNombreResponsable.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    if (vDatos.Rows.Count > 0)
                    {
                        foreach (DataRow item in vDatos.Rows)
                        {
                            DDLNombreResponsable.Items.Add(new ListItem { Value = item["idUsuario"].ToString(), Text = item["nombre"].ToString() });
                        }
                    }

                    asterisco.Visible = true;
                    DDLNombreResponsable.Visible = true;
                    etiqueta.Visible = true;
                    UpdatePanel6.Update();
                }
                else
                {
                    asterisco.Visible = false;
                    DDLNombreResponsable.Visible = false;
                    etiqueta.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
        
        private void validacionesCancelarNotificacion()
        {
            if (DdlMotivo.SelectedValue.Equals(""))
                throw new Exception("Favor seleccione un motivo de cancelación");

            if (DdlMotivo.SelectedValue.Equals("1") && DDLNombreResponsable.SelectedValue.Equals(""))
                throw new Exception("Favor seleccione ingeniero responsable");

            if (TxDetalle.Text == "" || TxDetalle.Text == string.Empty)
                throw new Exception("Favor ingrese detalle del motivo de cancelación");
        }

        protected void BtnCancelarNotificacion_Click(object sender, EventArgs e)
        {
            try
            {
                validacionesCancelarNotificacion();
                String vResponsable = DdlMotivo.SelectedValue == "1" ?  Convert.ToString(DDLNombreResponsable.SelectedValue) : Session["USUARIO"].ToString();
                String vEstado = DdlMotivo.SelectedValue == "1" ? "3" : "7";

                string vQuery = "STEISP_COMUNICACION_CrearNotificacion 8," + Session["COMUNICACION_ID_MANTENIMIENTO"].ToString()
                                + ",'" + TxDetalle.Text + "',"+ vEstado + "," + DdlMotivo.SelectedValue+ ",'" + vResponsable+"'";
                 Int32 vInformacion1 = vConexion.ejecutarSql(vQuery);

                if (vInformacion1 == 1)
                {
                    limpiarModal();
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "cerrarModal();", true);
            

                    Response.Redirect("/sites/comunicaciones/pages/mantenimiento/pendientesCrearNotificacion.aspx?ex=3");
                }
                else
                {
                    limpiarModal();
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "cerrarModal();", true);
                    Response.Redirect("/pages/mantenimiento/pendientesCrearNotificacion.aspx?ex=4");
                }


            }
            catch (Exception ex)
            {
                LbValidacion.Text = ex.Message;
                DivMensaje.Visible = true;
            }

        }

        private void limpiarModal()
        {
            DdlMotivo.SelectedIndex = -1;
            DDLNombreResponsable.SelectedIndex = -1;
            TxDetalle.Text = string.Empty;
            Session["COMUNICACION_ID_MANTENIMIENTO"] = null;
        }
    }
}