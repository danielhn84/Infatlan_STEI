using Infatlan_STEI_CableadoEstructurado.clases;
using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataTable = System.Data.DataTable;
using Page = System.Web.UI.Page;

namespace Infatlan_STEI_CableadoEstructurado.page.visita
{
    public partial class principalVisitaTecnica : System.Web.UI.Page
    {
        db vConexion = new db();
        Security vSecurity = new Security();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                if (Convert.ToBoolean(Session["AUTH"]))
                {
                    if (vSecurity.ObtenerPermiso(Session["USUARIO"].ToString(), 4).Creacion)
                        btnNuevo.Visible = true;

                    CargarProceso();
                }
                else
                {
                    Response.Redirect("/login.aspx");
                }
            }
        }

        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        void CargarProceso()
        {
            try
            {
                //DataTable vDatos = new DataTable();
                String vQueryId = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 25" /*,'" + Session["USUARIO"] + "'"*/;
                DataTable vDatos = vConexion.obtenerDataTable(vQueryId);

                GVPrincipalVisita.DataSource = vDatos;
                GVPrincipalVisita.DataBind();
                Session["CE_BUSCAQUEDAVISITA"] = vDatos;
                udpPrincipalVisita.Update();

                if (vDatos.Rows.Count == 0)
                {
                    LbDescripcionEdicion.Text = "No hay estudios pendientes";
                }
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        protected void TxBuscarVisita_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CargarProceso();
                String vBusqueda = TxBuscarVisita.Text;
                DataTable vDatos = (DataTable)Session["CE_BUSCAQUEDAVISITA"];
                if (vBusqueda.Equals(""))
                {
                    GVPrincipalVisita.DataSource = vDatos;
                    GVPrincipalVisita.DataBind();
                    udpPrincipalVisita.Update();
                }
                else
                {
                    EnumerableRowCollection<DataRow> filtered = vDatos.AsEnumerable()
                       .Where(r => r.Field<String>("agencia").Contains(vBusqueda.ToUpper()));

                    Boolean isNumeric = int.TryParse(vBusqueda, out int n);

                    if (isNumeric)
                    {
                        if (filtered.Count() == 0)
                        {
                            filtered = vDatos.AsEnumerable().Where(r =>
                                Convert.ToInt32(r["idEstudio"]) == Convert.ToInt32(vBusqueda));
                        }
                    }

                    DataTable vDatosFiltrados = new DataTable();
                    vDatosFiltrados.Columns.Add("idEstudio");
                    vDatosFiltrados.Columns.Add("nombre");
                    vDatosFiltrados.Columns.Add("agencia");
                    vDatosFiltrados.Columns.Add("observacion");
                    vDatosFiltrados.Columns.Add("fechaCreacion");

                    foreach (DataRow item in filtered)
                    {
                        vDatosFiltrados.Rows.Add(
                            item["idEstudio"].ToString(),
                            item["nombre"].ToString(),
                            item["agencia"].ToString(),
                            item["observacion"].ToString(),
                            item["fechaCreacion"].ToString()
                            );
                    }

                    GVPrincipalVisita.DataSource = vDatosFiltrados;
                    GVPrincipalVisita.DataBind();
                    Session["CE_BUSCAQUEDAVISITA"] = vDatosFiltrados;
                    //udpContabilidad.Update();
                }

            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("/sites/cableado/page/visita/visitaTecnica.aspx");
        }

        protected void GVPrincipalVisita_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            try
            {
                GVPrincipalVisita.DataSource = (DataTable)Session["CE_BUSCAQUEDAVISITA"];
                GVPrincipalVisita.PageIndex = e.NewPageIndex;
                GVPrincipalVisita.DataBind();
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }

        }

        protected void GVPrincipalVisita_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            try
            {

                DataTable vDatos = (DataTable)Session["CE_BUSCAQUEDAVISITA"];
                if (e.CommandName == "Entrar")
                {

                    String vId = e.CommandArgument.ToString();
                    string vCondicion = Convert.ToString(3);
                    Response.Redirect("/sites/cableado/page/visita/visitaTecnica.aspx?e=" + vId + "&c=" + vCondicion);

                }

            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}