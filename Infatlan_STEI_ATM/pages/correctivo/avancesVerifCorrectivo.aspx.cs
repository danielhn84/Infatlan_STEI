using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using System.Data.Sql;
using System.Data.SqlClient;
using Infatlan_STEI_ATM.clases;

namespace Infatlan_STEI_ATM.pages.correctivo
{
    public partial class avancesVerifCorrectivo : System.Web.UI.Page
    {
        bd vConexion = new bd();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["AVANCE_COR"] = null;
            if (!Page.IsPostBack)
            {
                if (Convert.ToBoolean(Session["AUTH"]))
                {
                    cargarData();
                }
                else
                {
                    Response.Redirect("/login.aspx");
                }
            }
        }

        void cargarData()
        {
            //AVANCES
            DataTable vDatos2 = new DataTable();
            vDatos2 = vConexion.ObtenerTabla("[STEISP_ATM_GeneralesCorrectivo] 9");
            GVAvances.DataSource = vDatos2;
            GVAvances.DataBind();
            Session["ATM_AVANCES_COR"] = vDatos2;

            if (HttpContext.Current.Session["AVANCE_COR"] == null)
            {
               
               // DDLFiltroEstado.Items.Clear();
                String vQuery = "[STEISP_ATM_GeneralesCorrectivo] 8";
                DataTable vDatos = vConexion.ObtenerTabla(vQuery);
                DDLFiltroEstado.Items.Add(new ListItem { Value = "0", Text = "Seleccione Estado..." });
                foreach (DataRow item in vDatos.Rows)
                {
                    DDLFiltroEstado.Items.Add(new ListItem { Value = item["idEstado"].ToString(), Text = item["nombre"].ToString() });
                }
               
                Session["AVANCE_COR"] = "1";
            }
        }

        protected void DDLFiltroEstado_TextChanged(object sender, EventArgs e)
        {
            if (DDLFiltroEstado.SelectedValue == "0")
            {
                DataTable vDatos2 = new DataTable();
                vDatos2 = vConexion.ObtenerTabla("[STEISP_ATM_GeneralesCorrectivo] 9");
                GVAvances.DataSource = vDatos2;
                GVAvances.DataBind();
                Session["ATM_AVANCES_COR"] = vDatos2;
            }
            else
            {
                DataTable vDatos2 = new DataTable();
                vDatos2 = vConexion.ObtenerTabla("[STEISP_ATM_GeneralesCorrectivo] 10,'" + DDLFiltroEstado.SelectedValue + "'");
                GVAvances.DataSource = vDatos2;
                GVAvances.DataBind();
                Session["ATM_AVANCES_COR"] = vDatos2;
            }
        }

        protected void GVAvances_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GVAvances.PageIndex = e.NewPageIndex;
                GVAvances.DataSource = (DataTable)Session["ATM_AVANCES_COR"];
                GVAvances.DataBind();
            }
            catch (Exception Ex)
            {

            }
        }

        protected void GVAvances_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        protected void btnReporte_Click(object sender, EventArgs e)
        {
            if (DDLFiltroEstado.SelectedValue == "0")
            {
                String vError = String.Empty;
                try
                {
                    ReportExecutionService.ReportExecutionService vRSE = new ReportExecutionService.ReportExecutionService();
                    vRSE.Credentials = new NetworkCredential("report_user", "kEbn2HUzd$Fs2T", "adbancat.hn");
                    vRSE.Url = "http://10.128.0.52/reportserver/reportexecution2005.asmx";



                    vRSE.ExecutionHeaderValue = new ReportExecutionService.ExecutionHeader();
                    var vEInfo = new ReportExecutionService.ExecutionInfo();
                    vEInfo = vRSE.LoadReport("/STEI/reporteMantenimientoCorGlobal", null);


                    //String vIDEstado = DDLFiltroEstado.SelectedValue;
                    List<ReportExecutionService.ParameterValue> vParametros = new List<ReportExecutionService.ParameterValue>();
                    //vParametros.Add(new ReportExecutionService.ParameterValue { Name = "ID", Value = vIDEstado });



                    vRSE.SetExecutionParameters(vParametros.ToArray(), "en-US");



                    String deviceinfo = "<DeviceInfo><Toolbar>false</Toolbar></DeviceInfo>";
                    String mime;
                    String encoding;
                    string[] stream;
                    ReportExecutionService.Warning[] warning;



                    byte[] vResultado = vRSE.Render("EXCEL", deviceinfo, out mime, out encoding, out encoding, out warning, out stream);
                    //byte[] vResultado = vRSE.Render("pdf", deviceinfo, out mime, out encoding, out encoding, out warning, out stream);

                    //File.WriteAllBytes("c:\\files\\test.pdf", vResultado);

                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.AppendHeader("Content-Type", "application/vnd.ms-excel");
                    //Response.AppendHeader("Content-Type", "application/pdf");
                    byte[] bytFile = vResultado;
                    Response.OutputStream.Write(bytFile, 0, bytFile.Length);
                    Response.AddHeader("Content-disposition", "attachment;filename=EstadoMantenimientoCorGlobal.xls");
                    //Response.AddHeader("Content-disposition", "attachment;filename=DescargaPDFArea.pdf");
                    Response.End();
                }
                catch (Exception Ex) { vError = Ex.Message; }
                DDLFiltroEstado.SelectedValue = "0";
            }
            else
            {
                String vError = String.Empty;
                try
                {
                    ReportExecutionService.ReportExecutionService vRSE = new ReportExecutionService.ReportExecutionService();
                    vRSE.Credentials = new NetworkCredential("report_user", "kEbn2HUzd$Fs2T", "adbancat.hn");
                    vRSE.Url = "http://10.128.0.52/reportserver/reportexecution2005.asmx";



                    vRSE.ExecutionHeaderValue = new ReportExecutionService.ExecutionHeader();
                    var vEInfo = new ReportExecutionService.ExecutionInfo();
                    vEInfo = vRSE.LoadReport("/STEI/reporteMantenimientoCorrectivo", null);


                    String vIDEstado = DDLFiltroEstado.SelectedValue;
                    List<ReportExecutionService.ParameterValue> vParametros = new List<ReportExecutionService.ParameterValue>();
                    vParametros.Add(new ReportExecutionService.ParameterValue { Name = "ID", Value = vIDEstado });



                    vRSE.SetExecutionParameters(vParametros.ToArray(), "en-US");



                    String deviceinfo = "<DeviceInfo><Toolbar>false</Toolbar></DeviceInfo>";
                    String mime;
                    String encoding;
                    string[] stream;
                    ReportExecutionService.Warning[] warning;



                    byte[] vResultado = vRSE.Render("EXCEL", deviceinfo, out mime, out encoding, out encoding, out warning, out stream);
                    //byte[] vResultado = vRSE.Render("pdf", deviceinfo, out mime, out encoding, out encoding, out warning, out stream);

                    //File.WriteAllBytes("c:\\files\\test.pdf", vResultado);

                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.AppendHeader("Content-Type", "application/vnd.ms-excel");
                    //Response.AppendHeader("Content-Type", "application/pdf");
                    byte[] bytFile = vResultado;
                    Response.OutputStream.Write(bytFile, 0, bytFile.Length);
                    Response.AddHeader("Content-disposition", "attachment;filename=EstadoMantenimientoCor" + DDLFiltroEstado.SelectedItem.Text + ".xls");
                    //Response.AddHeader("Content-disposition", "attachment;filename=DescargaPDFArea.pdf");
                    Response.End();
                }
                catch (Exception Ex) { vError = Ex.Message; }
                DDLFiltroEstado.SelectedValue = "0";
            }
        }
    }
}