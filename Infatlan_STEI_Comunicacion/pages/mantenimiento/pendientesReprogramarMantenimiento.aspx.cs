using Infatlan_STEI_Comunicacion.classes;
using System;
using System.Data;
using System.Web.UI;


namespace Infatlan_STEI_Comunicacion.pages.mantenimiento
{
    public partial class pendientesReprogramarMantenimiento : System.Web.UI.Page
    {
        db vConexion = new db();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Convert.ToBoolean(Session["AUTH"]))
                {
                    cargarDatos();
                }
            }
        }

        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        private void cargarDatos()
        {
            try
            {
                String vQuery = "STEISP_COMUNICACION_ReprogramarLV 1";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                GvPendientesReprogramar.DataSource = vDatos;
                GvPendientesReprogramar.DataBind();
                //GvPendientesReprogramar.Update();
                //Session["COMUNICACION_PMLV_PENDIENTES_MODIFICAR_LV"] = vDatos;
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
    }


}