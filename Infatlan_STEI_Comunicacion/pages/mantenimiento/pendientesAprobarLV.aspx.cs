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
    public partial class pendientesAprobarLV : System.Web.UI.Page
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
                    UpPendientesAprobarLV.Update();
                }
                else if (vEx.Equals("1"))
                {
                    vEx = null;
                    String vRe = "Lista de verificación aprobada con éxito.";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Pop", "window.alert('" + vRe + "')", true);
                    cargarDatos();
                    UpPendientesAprobarLV.Update();
                }
                else if (vEx.Equals("2"))
                {
                    vEx = null;
                    String vRe = "No se pudo enviar la lista de verificación, favor contactarse con el administrador del sistema.";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Pop", "window.alert('" + vRe + "')", true);
                    cargarDatos();
                    UpPendientesAprobarLV.Update();
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
                String vQuery = "STEISP_COMUNICACION_AprobalLV 1" ;
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                GvPendientesAprobarLV.DataSource = vDatos;
                GvPendientesAprobarLV.DataBind();
                UpPendientesAprobarLV.Update();
                Session["COMUNICACION_PALV_PENDIENTES_APROBAR_LV"] = vDatos;
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void GvPendientesAprobarLV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GvPendientesAprobarLV.PageIndex = e.NewPageIndex;
                GvPendientesAprobarLV.DataSource = (DataTable)Session["COMUNICACION_PALV_PENDIENTES_APROBAR_LV"];
                GvPendientesAprobarLV.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void GvPendientesAprobarLV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Aprobar")
            {
                string vIdMantenimientoAprobarLV = e.CommandArgument.ToString();
                String vQuery = "STEISP_COMUNICACION_AprobalLV 2,'" + vIdMantenimientoAprobarLV + "'";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                Session["COMUNICACION_PALV_COMPLETAR_LV_INDIVIDUAL"] = vDatos;



                Response.Redirect("/sites/comunicaciones/pages/mantenimiento/lvIndividual.aspx?ex=2");
            }
        }
    }
}