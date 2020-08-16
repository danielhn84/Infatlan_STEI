using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Infatlan_STEI_CableadoEstructurado.clases;
using System.Data.Sql;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Web.ApplicationServices;

using Word = Microsoft.Office.Interop.Word;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.Office.Interop.Word;

using DataTable = System.Data.DataTable;
using Page = System.Web.UI.Page;
using System.IO;
using Infatlan_STEI_Inventario.clases;

namespace Infatlan_STEI_CableadoEstructurado.page.cotizacion
{
    public partial class ofertaEconomica : System.Web.UI.Page
    {
        db vConexion = new db();
        Security vSecurity = new Security();

        protected void Page_Load(object sender, EventArgs e){
            if (!Page.IsPostBack){
                if (Convert.ToBoolean(Session["AUTH"])){
                    CargarProceso();
                }else {
                    Response.Redirect("/login.aspx");
                }
            }
        }

        public void Mensaje(string vMensaje, WarningType type){
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        void CargarProceso(){
            try{
                string vIdEstudio = Request.QueryString["i"];
                string vCondicion = Request.QueryString["a"];

                if (vCondicion == Convert.ToString(2)){
                    DataTable vDatos = new DataTable();
                    vDatos = vConexion.obtenerDataTable("STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 23," + vIdEstudio);
                    LbDescripcionOferta.Text = "Detalle de la oferta creada.";
                    udpContabilidad.Update();
                   
                    GVOfertaEconomica.DataSource = vDatos;
                    GVOfertaEconomica.DataBind();
                    Session["CE_DATOSESTUDIOOFERTA"] = vDatos;

                    foreach (GridViewRow row in GVOfertaEconomica.Rows){
                        LinkButton button = row.FindControl("BtnAprobar") as LinkButton;
                        button.Enabled = false;
                        button.CssClass = "btn btn-secondary";
                    }
                }else{
                    LbDescripcion.Visible = true;
                    DataTable vDatos = new DataTable();
                    vDatos = vConexion.obtenerDataTable("STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 24");

                    GVOfertaEconomica.DataSource = vDatos;
                    GVOfertaEconomica.DataBind();
                    if (vSecurity.ObtenerPermiso(Session["USUARIO"].ToString(), 4).Edicion){
                        foreach (GridViewRow item in GVOfertaEconomica.Rows){
                            LinkButton LbEdit = item.FindControl("BtnModificar") as LinkButton;
                            LinkButton LbEdit2 = item.FindControl("BtnAprobar") as LinkButton;
                            LbEdit.Visible = true;
                            LbEdit2.Visible = true;
                        }
                    }
                    Session["CE_DATOSESTUDIOOFERTA"] = vDatos;
                    updBuscarAprobacion.Visible = true;

                    foreach (GridViewRow row in GVOfertaEconomica.Rows){
                        string  vQuery = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 34,'" + row.Cells[0].Text + "'";
                        DataTable vDatosBusqueda = vConexion.obtenerDataTable(vQuery);

                        foreach (DataRow item in vDatosBusqueda.Rows){
                            if (item["estado"].ToString() == "OfertaAprobada"){
                                LinkButton button = row.FindControl("BtnAprobar") as LinkButton;
                                button.Enabled = false;
                                button.CssClass = "btn btn-secondary";

                                LinkButton button1 = row.FindControl("BtnModificar") as LinkButton;
                                button1.Enabled = false;
                                button1.CssClass = "btn btn-secondary";
                            }else{
                                LinkButton button = row.FindControl("BtnAprobar") as LinkButton;
                                button.Enabled = true;

                                LinkButton button1 = row.FindControl("BtnModificar") as LinkButton;
                                button1.Enabled = true;
                            }
                        }
                    }
                }
            }catch (Exception Ex){
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        protected void GVOfertaEconomica_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            try
            {
                GVOfertaEconomica.DataSource = (DataTable)Session["CE_DATOSESTUDIOOFERTA"];
                GVOfertaEconomica.PageIndex = e.NewPageIndex;
                GVOfertaEconomica.DataBind();
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }

        }

        protected void GVOfertaEconomica_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            try
            {
                DataTable vDatos = (DataTable)Session["CE_DATOSESTUDIOOFERTA"];

                string vDatoPrincipal = e.CommandArgument.ToString();

                string vIdEstudio = e.CommandArgument.ToString();
                Session["CE_OFERTAIDESTUDIO"] = vIdEstudio;
                if (e.CommandName == "Descargar")
                {
                    //string vError = string.Empty;
                    //try
                    //{
                    //    ReportExecutionService.ReportExecutionService vRSE = new ReportExecutionService.ReportExecutionService();
                    //    vRSE.Credentials = new NetworkCredential("report_user", "kEbn2HUzd$Fs2T", "adbancat.hn");
                    //    vRSE.Url = "http://10.128.0.52/reportserver/reportexecution2005.asmx";

                    //    vRSE.ExecutionHeaderValue = new ReportExecutionService.ExecutionHeader();
                    //    var vEInfo = new ReportExecutionService.ExecutionInfo();
                    //    vEInfo = vRSE.LoadReport("/STEI/Oferta Economica", null);

                    //    List<ReportExecutionService.ParameterValue> vParametros = new List<ReportExecutionService.ParameterValue>();
                    //    vParametros.Add(new ReportExecutionService.ParameterValue { Name = "idEstudio", Value = vIdEstudio });

                    //    vRSE.SetExecutionParameters(vParametros.ToArray(), "en-US");

                    //    String deviceinfo = "<DeviceInfo><Toolbar>false</Toolbar></DeviceInfo>";
                    //    String mime;
                    //    String encoding;
                    //    string[] stream;
                    //    ReportExecutionService.Warning[] warning;

                    //    byte[] vResultado = vRSE.Render("Word", deviceinfo, out mime, out encoding, out encoding, out warning, out stream);

                    //    //File.WriteAllBytes("c:\\files\\test.pdf", vResultado);

                    //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    //    Response.AppendHeader("Content-Type", "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
                    //    byte[] bytFile = vResultado;
                    //    Response.OutputStream.Write(bytFile, 0, bytFile.Length);
                    //    Response.AddHeader("Content-disposition", "attachment;filename=OfertaEconomica.docx");
                    //    Response.End();
                    //}
                    //catch (Exception Ex) { vError = Ex.Message; }
                }

                   

                

                if (e.CommandName == "Aprobar")
                {
                    String vQueryCosto = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 16, " + vIdEstudio;
                    //Int32 vInformacion = vConexion.ejecutarSql(vQueryCosto);
                    DataTable vDataCosto = vConexion.obtenerDataTable(vQueryCosto);

                    LbModNombreEstudio.Text = vDataCosto.Rows[0]["nombre"].ToString();
                    string vValor = vDataCosto.Rows[0]["costoTotalCotizacion"].ToString();
                    LbModValorCotizacion.Text = "L." + Convert.ToDecimal(vValor).ToString("##,###.00");

                    UdpDatosOferta.Update();

                    //LbModResponsable.Text = vDataCosto.Rows[0]["costoTotalCotizacion"].ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                }
                if (e.CommandName == "Modificar")
                {
                    Response.Redirect("/sites/cableado/page/cotizacion/aprobacionContabilidad.aspx?io=" + vIdEstudio);
                }

            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }

            void SetHeadings(Word.Cell tblCell, string text)
            {

                tblCell.Range.Text = text;
                tblCell.Range.Bold = 1;
                tblCell.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                tblCell.Column.Width = 100;
                tblCell.Range.Font.Name = "Arial";
                tblCell.Range.Font.Size = 12;
                tblCell.Range.Borders.InsideLineStyle = WdLineStyle.wdLineStyleSingle;
                tblCell.Range.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;


            }

        }

        protected void TxBuscarOferta_TextChanged(object sender, EventArgs e)
        {

            try
            {
                CargarProceso();
                String vBusqueda = TxBuscarOferta.Text;
                DataTable vDatos = (DataTable)Session["CE_DATOSESTUDIOOFERTA"];
                if (vBusqueda.Equals(""))
                {
                    GVOfertaEconomica.DataSource = vDatos;
                    GVOfertaEconomica.DataBind();
                    udpContabilidad.Update();
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
                    vDatosFiltrados.Columns.Add("responsable");
                    vDatosFiltrados.Columns.Add("fechaCreacion");

                    foreach (DataRow item in filtered)
                    {
                        vDatosFiltrados.Rows.Add(
                            item["idEstudio"].ToString(),
                            item["nombre"].ToString(),
                            item["agencia"].ToString(),
                            item["responsable"].ToString(),
                            item["fechaCreacion"].ToString()
                            );
                    }

                    GVOfertaEconomica.DataSource = vDatosFiltrados;
                    GVOfertaEconomica.DataBind();
                    Session["CE_DATOSESTUDIOOFERTA"] = vDatosFiltrados;
                    udpContabilidad.Update();
                }

            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }

        }

        protected void BtnAprobar_Click(object sender, EventArgs e)
        {
            //GENERAR XML Para disminucion
            try
            {
                String vQueryXML = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 22," + Session["CE_OFERTAIDESTUDIO"];

                DataTable vDataStock = vConexion.obtenerDataTable(vQueryXML);

                for (int i = 0; i < vDataStock.Rows.Count; i++)
                {
                    String vCodigo = vDataStock.Rows[i]["codigo"].ToString();
                    String vIdStock = vDataStock.Rows[i]["idStock"].ToString();
                    String vIdInventario = vDataStock.Rows[i]["idInventario"].ToString();
                    String vIdUbicacion = vDataStock.Rows[i]["idUbicacion"].ToString();
                    String vIdResponsable = vDataStock.Rows[i]["idResponsable"].ToString();
                    String vSerie = vDataStock.Rows[i]["series"].ToString();

                    Decimal vCantidadSolicitada = Convert.ToDecimal(vDataStock.Rows[i]["cantidadSolicitada"].ToString());
                    Decimal vCantidadInventario = Convert.ToDecimal(vDataStock.Rows[i]["cantidadInventario"].ToString());

                    Decimal vPrecioInventario = Convert.ToDecimal(vDataStock.Rows[i]["precioUnit"].ToString());

                    if (vCantidadInventario >= vCantidadSolicitada)
                    {
                        Decimal vCantidadActual = vCantidadInventario - vCantidadSolicitada;
                        Decimal vPrecioDec = vCantidadActual * vPrecioInventario;
                        String vPrecio = vPrecioDec.ToString().Replace(",", ".");

                        generarxml vMaestro = new generarxml();
                        Object[] vDatosMaestro = new object[10];
                        vDatosMaestro[0] = vCodigo;
                        vDatosMaestro[1] = vIdStock;
                        vDatosMaestro[2] = vIdUbicacion;
                        vDatosMaestro[3] = vIdResponsable;
                        vDatosMaestro[4] = "CAMBIO UBICACION";
                        vDatosMaestro[5] = vCantidadActual;
                        vDatosMaestro[6] = vSerie;
                        vDatosMaestro[7] = vPrecio;
                        vDatosMaestro[8] = Session["USUARIO"].ToString();
                        vDatosMaestro[9] = 6;
                        String vXML = vMaestro.ObtenerMaestroString(vDatosMaestro);
                        vXML = vXML.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");

                        string vQuery;
                        if (Convert.ToDecimal(vCantidadInventario) == Convert.ToDecimal(vCantidadSolicitada))
                        {
                            vQuery = "[STEISP_INVENTARIO_Principal] 3" +
                            ",'" + vIdStock +
                            "','" + vIdUbicacion + //UBICACION ANTERIOR
                            "','" + vXML + "'";

                            Int32 vInfo = vConexion.ejecutarSql(vQuery);
                            if (vInfo == 2)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "cerrarModal();", true);
                                Mensaje("Cambio realizado con éxito.", WarningType.Success);

                                DataTable vDatosOferta = new DataTable();

                                string vQueryOferta = "STEISP_CABLESTRUCTURADO_IngresarDatosEstudio 11 ,'" + Session["CE_OFERTAIDESTUDIO"] + "','OfertaAprobada'";
                                vDatosOferta = vConexion.obtenerDataTable(vQueryOferta);
                                //cargarDatos(TxIdUbicacion.Text);
                            }
                        }
                        else if (Convert.ToDecimal(vCantidadInventario) > Convert.ToDecimal(vCantidadSolicitada))
                        {
                            vQuery = "[STEISP_INVENTARIO_Principal] 6" +
                             ",'" + vIdInventario +
                            "','" + vIdUbicacion + //UBICACION ANTERIOR
                            "','" + vXML + "'";

                            Int32 vInfo = vConexion.ejecutarSql(vQuery);
                            if (vInfo == 4 || vInfo == 5)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "cerrarModal();", true);
                                Mensaje("Cambio realizado con éxito.", WarningType.Success);
                                DataTable vDatosOferta = new DataTable();

                                string vQueryOferta = "STEISP_CABLESTRUCTURADO_IngresarDatosEstudio 11 ,'" + Session["CE_OFERTAIDESTUDIO"] + "','OfertaAprobada'";
                                vDatosOferta = vConexion.obtenerDataTable(vQueryOferta);

                                //cargarDatos(TxIdUbicacion.Text);
                            }
                        }
                        else
                        {

                            throw new Exception("No actualizo correctamente.");
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "cerrarModal();", true);
                        throw new Exception("La cantidad solicitada es mayor que la disponible.");
                    }
                    udpContabilidad.Update();
                }
                CargarProceso();
            }
            catch (Exception ex)
            {

                Mensaje(ex.Message, WarningType.Danger);
            }

        }
    }
 }