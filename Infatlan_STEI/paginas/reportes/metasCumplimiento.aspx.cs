using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infatlan_STEI.classes;

namespace Infatlan_STEI.paginas.reportes
{
    public partial class metasCumplimiento : System.Web.UI.Page
    {
        db vConexion = new db();
        protected void Page_Load(object sender, EventArgs e){
            if (!Page.IsPostBack){
                if (Convert.ToBoolean(Session["AUTH"])){
                    cargarData();
                }else{
                    Response.Redirect("/login.aspx");
                }
            }
        }

        private void cargarData(){
            try{
                String vFecha = DateTime.Now.AddDays(-1).ToString("MM/dd/yyyy");
                String vAssignedGroup = "Infa-tgu";
                cargarCalls(vFecha, vAssignedGroup);
                cargarKPI(vFecha, vAssignedGroup);
                cargarOSER(vFecha, vAssignedGroup);
                cargarMediosPago(vFecha, vAssignedGroup);
                cargarMediosPago(vFecha, vAssignedGroup);
            }catch (Exception Ex){
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        public void Mensaje(string vMensaje, WarningType type){
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        protected void GvOSER_RowDataBound(object sender, GridViewRowEventArgs e){
            if (e.Row.RowType == DataControlRowType.DataRow){
                var vDropDown = e.Row.Cells[2].FindControl("DDLRazonER") as DropDownList;
                if (vDropDown != null){
                    String vQuery = "[STEISP_CUMPLIMIENTO_Ajustes] 6, 5";
                    DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                    vDropDown.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    foreach (DataRow item in vDatos.Rows){
                        vDropDown.Items.Add(new ListItem { Value = item["idMotivo"].ToString(), Text = item["nombre"].ToString() });
                    }
                    if (HttpContext.Current.Session["CUMPL_OSER_PAGING"] != null ){
                        DataTable vData = (DataTable)Session["CUMPL_OSER"];
                        for (int i = 0; i < vData.Rows.Count; i++){
                            DataRowView drv = e.Row.DataItem as DataRowView;
                            String vCelda = drv["id"].ToString();

                            if (vData.Rows[i]["id"].ToString() == drv["id"].ToString()){
                                vDropDown.SelectedValue = vData.Rows[i]["idMotivo"].ToString();
                            }
                        }
                    }
                }
            }
        }

        protected void GvOSER_PageIndexChanging(object sender, GridViewPageEventArgs e){
            try{
                DataTable vData = (DataTable)Session["CUMPL_OSER"];
                int vCont = GvOSER.PageIndex != 0 ? GvOSER.PageIndex * 10 : 0;

                foreach (GridViewRow row in GvOSER.Rows){
                    DropDownList DDLRazon = (DropDownList)row.Cells[2].FindControl("DDLRazonER");
                    vData.Rows[vCont]["idMotivo"] = DDLRazon.SelectedValue;
                    vCont++;
                }
                Session["CUMPL_OSER_PAGING"] = true;

                GvOSER.PageIndex = e.NewPageIndex;
                GvOSER.DataSource = vData;
                Session["CUMPL_OSER"] = vData;
                GvOSER.DataBind();
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        private void cargarKPI(String vFecha, String vAssignedGroup) { 
            String vQuery = "[STEISP_CUMPLIMIENTO_Generales] 2,'" + vAssignedGroup + "','" + vFecha + "'";
            DataSet vDSkpi = vConexion.obtenerDataSetSA(vQuery);

            int vCumplidas = Convert.ToInt32(vDSkpi.Tables[1].Rows[0]["Cumplidas"].ToString());
            int vIncumplidas = Convert.ToInt32(vDSkpi.Tables[0].Rows[0]["Incumplidas"].ToString());
            int vSuma = vCumplidas + vIncumplidas;

            Decimal vKPIPercent = 100;
            if (vSuma != 0){
                float vKPI = float.Parse(vCumplidas.ToString()) / float.Parse(vSuma.ToString()) * 100;
                vKPIPercent = Convert.ToDecimal(Math.Round(vKPI, 2));
            }

            if (vDSkpi.Tables[2].Rows.Count > 0){
                GvKPISolicitudes.DataSource = vDSkpi.Tables[2];
                GvKPISolicitudes.DataBind();
                Session["CUMPL_KPI"] = vDSkpi.Tables[2];
            }

            TxKPIPorcentaje.Text = vKPIPercent.ToString() + "%";
            TxKPITotal.Text = Convert.ToString(vSuma);
            TxKPICumplimiento.Text = vCumplidas.ToString();
            TxKPICumplimientoNo.Text = vIncumplidas.ToString();
        }

        private void cargarMediosPago(String vFecha, String vAssignedGroup) {
            LitCaja.Text = "<div class='chart easy-pie-chart-4' data-percent='30'><span class='percent'></span></div>";
            LitABA.Text = "<div class='chart easy-pie-chart-4' data-percent='30'><span class='percent'></span></div>";
            LitATM.Text = "<div class='chart easy-pie-chart-4' data-percent='30'><span class='percent'></span></div>";
        }

        private void cargarOSER(String vFecha, String vAssignedGroup) { 
            String vQuery = "[STEISP_CUMPLIMIENTO_Generales] 1,'" + vAssignedGroup + "'";
            DataTable vDatos = vConexion.obtenerDataTableSA(vQuery);
            vDatos.Columns.Add("idMotivo");
            if (vDatos.Rows.Count > 0){
                LbResOSER.Text = "";
                GvOSER.DataSource = vDatos;
                GvOSER.DataBind();
                Session["CUMPL_OSER"] = vDatos;
            }
        }        
        
        private void cargarCalls(String vFecha, String vAssignedGroup) {

            String vColor;
            String vPorcentaje = "11";
            if (Convert.ToInt32(vPorcentaje) < 11)
                vColor = "css-bar-danger";
            else if (Convert.ToInt32(vPorcentaje) < 50)
                vColor = "css-bar-warning";
            else 
                vColor = "css-bar-success";

            String vConstruccion = "<div data-label = '" + vPorcentaje + "%' class='css-bar css-bar-" + vPorcentaje + " css-bar-lg " + vColor + "'></div>";

            LitCall.Text = vConstruccion;
        }
    }
}