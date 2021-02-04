using Infatlan_STEI_ATM.clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Infatlan_STEI_ATM.pages.calendario
{
    public partial class avances : System.Web.UI.Page
    {
        bd vConexion = new bd();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["AVANCE"] = null;
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
            vDatos2 = vConexion.ObtenerTabla("[STEISP_ATM_Generales] 41");
            GVAvances.DataSource = vDatos2;
            GVAvances.DataBind();
            Session["ATM_AVANCES"] = vDatos2;

            if (HttpContext.Current.Session["AVANCE"] == null)
            {
                DDLFiltroEstado.Items.Clear();
                String vQuery = "[STEISP_ATM_Generales] 44";
                DataTable vDatos = vConexion.ObtenerTabla(vQuery);
                DDLFiltroEstado.Items.Add(new ListItem { Value = "0", Text = "Seleccione Estado..." });
                foreach (DataRow item in vDatos.Rows)
                {
                    DDLFiltroEstado.Items.Add(new ListItem { Value = item["idEstadoMantenimiento"].ToString(), Text = item["nombreEstado"].ToString() });
                }
                Session["AVANCE"] = "1";
            }
        }

        protected void GVAvances_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GVAvances.PageIndex = e.NewPageIndex;
                GVAvances.DataSource = (DataTable)Session["ATM_AVANCES"];
                GVAvances.DataBind();
            }
            catch (Exception Ex)
            {

            }
        }

        protected void DDLFiltroEstado_TextChanged(object sender, EventArgs e)
        {
            if (DDLFiltroEstado.SelectedValue == "0")
            {
                DataTable vDatos2 = new DataTable();
                vDatos2 = vConexion.ObtenerTabla("[STEISP_ATM_Generales] 41");
                GVAvances.DataSource = vDatos2;
                GVAvances.DataBind();
                Session["ATM_AVANCES"] = vDatos2;
            }
            else
            {
                DataTable vDatos2 = new DataTable();
                vDatos2 = vConexion.ObtenerTabla("[STEISP_ATM_Generales] 45,'" + DDLFiltroEstado.SelectedValue + "'");
                GVAvances.DataSource = vDatos2;
                GVAvances.DataBind();
                Session["ATM_AVANCES"] = vDatos2;
            }
        }

        protected void GVAvances_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string IDMantenimiento = e.CommandArgument.ToString();
                string vEstado = "";
                if (e.CommandName == "VerMotivo")
                {
                    DataTable vDatos = new DataTable();
                    String vQuery = "STEISP_ATM_CancelarMantenimiento 2,'" + IDMantenimiento + "'";
                    vDatos = vConexion.ObtenerTabla(vQuery);
                    foreach (DataRow item in vDatos.Rows)
                    {
                        vEstado = item["Estado"].ToString();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
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
                    vEInfo = vRSE.LoadReport("/STEI/reporteMantenimientoPrevGlobal", null);


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
                    Response.AddHeader("Content-disposition", "attachment;filename=EstadoMantenimientoPrevGlobal.xls");
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
                    vEInfo = vRSE.LoadReport("/STEI/reporteEstadoATM", null);


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
                    Response.AddHeader("Content-disposition", "attachment;filename=EstadoMantenimientoPrev" + DDLFiltroEstado.SelectedItem.Text + ".xls");
                    //Response.AddHeader("Content-disposition", "attachment;filename=DescargaPDFArea.pdf");
                    Response.End();
                }
                catch (Exception Ex) { vError = Ex.Message; }
                DDLFiltroEstado.SelectedValue = "0";
            }
        }
    }
}