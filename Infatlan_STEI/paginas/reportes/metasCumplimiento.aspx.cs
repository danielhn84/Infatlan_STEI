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
                LitCaja.Text = "<div class='chart easy-pie-chart-4' data-percent='30'><span class='percent'></span></div>";
                LitABA.Text = "<div class='chart easy-pie-chart-4' data-percent='30'><span class='percent'></span></div>";
                LitATM.Text = "<div class='chart easy-pie-chart-4' data-percent='30'><span class='percent'></span></div>";
                LitCall.Text = "<div class='chart easy-pie-chart-4' data-percent='30'><span class='percent'></span></div>";

                String vAssignedGroup = "Infa-tgu";
                String vQuery = "[STEISP_CUMPLIMIENTO_Generales] 1,'" + vAssignedGroup + "'";
                DataTable vDatos = vConexion.obtenerDataTableSA(vQuery);
                if (vDatos.Rows.Count > 0){
                    LbResOSER.Text = "";
                    GvOSER.DataSource = vDatos;
                    GvOSER.DataBind();
                    Session["CUMPL_EP"] = vDatos;
                }

                String vFecha = DateTime.Now.ToString("MM/dd/yyyy");
                vQuery = "[STEISP_CUMPLIMIENTO_Generales] 2,'" + vAssignedGroup + "','" + vFecha + "'";
                DataSet vDSKPI = vConexion.obtenerDataSetSA(vQuery);

                int vCumplidas = Convert.ToInt32(vDSKPI.Tables[1].Rows[0]["Cumplidas"].ToString());
                int vIncumplidas = Convert.ToInt32(vDSKPI.Tables[0].Rows[0]["Incumplidas"].ToString());
                int vSuma = vCumplidas + vIncumplidas;

                float vKPI = float.Parse(vCumplidas.ToString()) / float.Parse(vSuma.ToString()) * 100;
                Decimal vKPIPercent = Convert.ToDecimal(Math.Round(vKPI, 2));

                TxKPIPorcentaje.Text = vKPIPercent.ToString() + "%";
                TxKPITotal.Text = Convert.ToString(vCumplidas + vIncumplidas);
                TxKPICumplimiento.Text = vCumplidas.ToString();
                TxKPICumplimientoNo.Text = vIncumplidas.ToString();

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
                }
            }
        }
    }
}