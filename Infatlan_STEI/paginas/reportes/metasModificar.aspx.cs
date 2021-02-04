using Infatlan_STEI.classes;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Infatlan_STEI.paginas.reportes
{
    public partial class metasModificar : System.Web.UI.Page
    {
        db vConexion = new db();
        Security vSecurity = new Security();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Convert.ToBoolean(Session["AUTH"]))
                {
                    if (!vSecurity.ObtenerPermiso(Session["USUARIO"].ToString(), 5).Consulta)
                        Response.Redirect("/default.aspx");

                    cargarDatos();
                }
                else
                {
                    Response.Redirect("/login.aspx");
                }
            }
        }

        private void cargarDatos()
        {
            try
            {
                String vQuery = "[STEISP_CUMPLIMIENTO_Reportes] 13,'" + Session["USUARIO"].ToString() + "'";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0)
                {
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                    Session["CUMPL_REPORTE_MODIFICACION"] = vDatos;
                }
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        protected void TxBusqueda_TextChanged(object sender, EventArgs e)
        {

        }

        protected void GVBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string vIdReporte = e.CommandArgument.ToString();
                if (e.CommandName == "verReporte")
                {
                    Session["CUMPL_ID_REPORTE"] = vIdReporte;
                    Response.Redirect("metasCumplimiento.aspx?id=" + vIdReporte);
                }
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
    }
}