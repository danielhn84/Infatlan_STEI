using Infatlan_STEI_Agencias.classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Infatlan_STEI_Agencias.pages.configuraciones
{
    public partial class avances : System.Web.UI.Page
    {
        db vConexion = new db();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["AVANCEAG"] = null;
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
            vDatos2 = vConexion.obtenerDataTable("STEISP_AGENCIA_Generales 1");
            GVAvances.DataSource = vDatos2;
            GVAvances.DataBind();
            Session["AG_AVANCES"] = vDatos2;

            if (HttpContext.Current.Session["AVANCEAG"] == null)
            {
                DDLFiltroEstado.Items.Clear();
                String vQuery = "STEISP_AGENCIA_Generales 2";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                DDLFiltroEstado.Items.Add(new ListItem { Value = "0", Text = "Seleccione Estado..." });
                foreach (DataRow item in vDatos.Rows)
                {
                    DDLFiltroEstado.Items.Add(new ListItem { Value = item["idMantenimientoEstado"].ToString(), Text = item["nombre"].ToString() });
                }
                Session["AVANCEAG"] = "1";
            }
        }

        protected void DDLFiltroEstado_TextChanged(object sender, EventArgs e)
        {
            if (DDLFiltroEstado.SelectedValue == "0")
            {
                DataTable vDatos2 = new DataTable();
                vDatos2 = vConexion.obtenerDataTable("STEISP_AGENCIA_Generales 1");
                GVAvances.DataSource = vDatos2;
                GVAvances.DataBind();
                Session["AG_AVANCES"] = vDatos2;
            }
            else
            {
                DataTable vDatos2 = new DataTable();
                vDatos2 = vConexion.obtenerDataTable("STEISP_AGENCIA_Generales 3,'" + DDLFiltroEstado.SelectedValue + "'");
                GVAvances.DataSource = vDatos2;
                GVAvances.DataBind();
                Session["AG_AVANCES"] = vDatos2;
            }
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
                    vEInfo = vRSE.LoadReport("/STEI/reporteAgenciasGlobal", null);


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
                    Response.AddHeader("Content-disposition", "attachment;filename=EstadoMantenimientoGlobal.xls");
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
                    vEInfo = vRSE.LoadReport("/STEI/reporteAgenciasEstado", null);


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
                    Response.AddHeader("Content-disposition", "attachment;filename=EstadoMantenimiento" + DDLFiltroEstado.SelectedItem.Text + ".xls");
                    //Response.AddHeader("Content-disposition", "attachment;filename=DescargaPDFArea.pdf");
                    Response.End();
                }
                catch (Exception Ex) { vError = Ex.Message; }
                DDLFiltroEstado.SelectedValue = "0";
            }
        }

        protected void GVAvances_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GVAvances.PageIndex = e.NewPageIndex;
                GVAvances.DataSource = (DataTable)Session["AG_AVANCES"];
                GVAvances.DataBind();
            }
            catch (Exception Ex)
            {

            }
        }
    }
}