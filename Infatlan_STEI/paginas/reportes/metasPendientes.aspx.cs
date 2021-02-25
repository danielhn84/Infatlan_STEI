using Infatlan_STEI.classes;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;

namespace Infatlan_STEI.paginas.reportes
{
    public partial class metasPendientes : System.Web.UI.Page
    {
        db vConexion = new db();
        Security vSecurity = new Security();

        protected void Page_Load(object sender, EventArgs e){
            if (!Page.IsPostBack){
                if (Convert.ToBoolean(Session["AUTH"])){
                    if (!vSecurity.ObtenerPermiso(Session["USUARIO"].ToString(), 5).Consulta)
                        Response.Redirect("/default.aspx");

                    cargarDatos();
                }else {
                    Response.Redirect("/login.aspx");
                }
            }
        }

        private void cargarDatos() {
            try{
                String vQuery = "[STEISP_CUMPLIMIENTO_Reportes] 3";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0){
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                    Session["CUMPL_PENDIENTES"] = vDatos;
                }
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        public void Mensaje(string vMensaje, WarningType type){
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        protected void TxBusqueda_TextChanged(object sender, EventArgs e){
            try{
                cargarDatos();

                String vBusqueda = TxBusqueda.Text;
                DataTable vDatos = (DataTable)Session["CUMPL_PENDIENTES"];

                if (vBusqueda.Equals("")){
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                }else{
                    EnumerableRowCollection<DataRow> filtered = vDatos.AsEnumerable()
                        .Where(r => r.Field<String>("nombre").Contains(vBusqueda.ToUpper()));

                    Boolean isNumeric = int.TryParse(vBusqueda, out int n);

                    if (isNumeric){
                        if (filtered.Count() == 0){
                            filtered = vDatos.AsEnumerable().Where(r =>
                                Convert.ToInt32(r["idReporte"]) == Convert.ToInt32(vBusqueda));
                        }
                    }


                    DataTable vDatosFiltrados = new DataTable();
                    vDatosFiltrados.Columns.Add("idReporte");
                    vDatosFiltrados.Columns.Add("nombre");
                    vDatosFiltrados.Columns.Add("fechaRegistro");

                    foreach (DataRow item in filtered){
                        vDatosFiltrados.Rows.Add(
                            item["idReporte"].ToString(),
                            item["nombre"].ToString(),
                            item["fechaRegistro"].ToString()
                            );
                    }

                    GVBusqueda.DataSource = vDatosFiltrados;
                    GVBusqueda.DataBind();
                    Session["CUMPL_PENDIENTES"] = vDatosFiltrados;
                }
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void GVBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e){
            try{
                GVBusqueda.PageIndex = e.NewPageIndex;
                GVBusqueda.DataSource = (DataTable)Session["CUMPL_PENDIENTES"];
                GVBusqueda.DataBind();
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e){
            try{
                string vIdReporte = e.CommandArgument.ToString();
                if (e.CommandName == "verReporte"){
                    Session["CUMPL_ID_REPORTE"] = vIdReporte;
                    Response.Redirect("metasAprobacion.aspx");
                }
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
    }
}