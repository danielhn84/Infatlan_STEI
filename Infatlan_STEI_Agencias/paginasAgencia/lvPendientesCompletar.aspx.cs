using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using Infatlan_STEI_Agencias.classes;

namespace Infatlan_STEI_Agencias.paginasAgencia
{
    public partial class LvCompletar : System.Web.UI.Page
    {
        db vConexion = new db();

        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                cargarDatos();
            }
        }



        private void cargarDatos()
        {
            try
            {
                String vQuery = "STEISP_AGENCIA_CompletarListaVerificacion 1";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                GVListaVerificacion.DataSource = vDatos;
                GVListaVerificacion.DataBind();
                Session["AGENCIA_LV_PENDIENTES"] = vDatos;

            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }

        }

        protected void GVListaVerificacion_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Completar")
            {
                string vIdMantenimientoCompletar = e.CommandArgument.ToString();
                Session["AGENCIA_ID_MANTENIMIENTO_COMPLETAR_LV"] = vIdMantenimientoCompletar;

                try
                {
                    String vQuery = "STEISP_AGENCIA_CompletarListaVerificacion 1";
                    DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                    GVListaVerificacion.DataSource = vDatos;
                    GVListaVerificacion.DataBind();
                    Session["AGENCIA_LV_PENDIENTES"] = vDatos;

                }
                catch (Exception ex)
                {
                    Mensaje(ex.Message, WarningType.Danger);
                }

            }


        }

    }
}