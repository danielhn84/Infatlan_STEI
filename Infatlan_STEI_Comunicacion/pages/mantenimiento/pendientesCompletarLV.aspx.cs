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
    public partial class pendientesCompletarLV : System.Web.UI.Page
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
                    UpPendientesCompletarLV.Update();
                }
                else if (vEx.Equals("1"))
                {
                    vEx = null;
                    String vRe = "Lista de verificación enviada con éxito.";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Pop", "window.alert('" + vRe + "')", true);
                    cargarDatos();
                    UpPendientesCompletarLV.Update();
                }
                else if (vEx.Equals("2"))
                {
                    vEx = null;
                    String vRe = "No se pudo enviar la lista de verificación, favor contactarse con el administrador del sistema.";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Pop", "window.alert('" + vRe + "')", true);
                    cargarDatos();
                    UpPendientesCompletarLV.Update();
                }
            }

                //}
                //else
                //{
                //    Response.Redirect("/login.aspx");
                //}

        }

        private void cargarDatos()
        {
            try
            {
                String vQuery = "STEISP_COMUNICACION_CompletarLV 1,'" + Session["USUARIO"] + "'";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                GvPendientesCompletarLV.DataSource = vDatos;
                GvPendientesCompletarLV.DataBind();
                Session["COMUNICACION_PCLV_PENDIENTES_LV"] = vDatos;
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void GvPendientesCompletarLV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GvPendientesCompletarLV.PageIndex = e.NewPageIndex;
                GvPendientesCompletarLV.DataSource = (DataTable)Session["COMUNICACION_PCLV_PENDIENTES_LV_COMPLETAR"];
                GvPendientesCompletarLV.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }

        }

        protected void GvPendientesCompletarLV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Crear")
            {
                string vIdMantenimientoCompletarLV = e.CommandArgument.ToString();
                String vQuery = "STEISP_COMUNICACION_CompletarLV 2,'" + vIdMantenimientoCompletarLV + "'";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                Session["COMUNICACION_PCLV_COMPLETAR_LV_INDIVIDUAL"] = vDatos;               
            
                Response.Redirect("/sites/comunicaciones/pages/mantenimiento/lvIndividual.aspx?ex=1");
            }
        }

        protected void GvPendientesCompletarLV_RowCommand1(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}