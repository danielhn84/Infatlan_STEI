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


namespace Infatlan_STEI_CableadoEstructurado.paginas
{
    public partial class principalPresupuestos : System.Web.UI.Page
    {
        db vConexion = new db();
        //private object Content;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarProceso();

                DataTable vDatos = new DataTable();
                vDatos = vConexion.obtenerDataTable("STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 27");

                lbCotizacionRealizadas.Text = vDatos.Rows[0]["realizados"].ToString();
                lbCotizacionPendientes.Text = vDatos.Rows[0]["pendientes"].ToString();

                //CSSCotizacion.Style.Size.width = 300;
                //CSSCotizacion.Style.Value = width:300;
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

                DataTable vDatos = new DataTable();
                vDatos = vConexion.obtenerDataTable("STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 15 ");

                GVPrincipal.DataSource = vDatos;
                GVPrincipal.DataBind();
                Session["CE_BUSCAQUEDAESTUDIO"] = vDatos;
               udpContabilidad.Update();

            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        protected void GVPrincipal_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            try
            {
                GVPrincipal.DataSource = (DataTable)Session["CE_BUSCAQUEDAESTUDIO"];
                GVPrincipal.PageIndex = e.NewPageIndex;
                GVPrincipal.DataBind();
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }

        }

        protected void GVPrincipal_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            try
            {
                DataTable vDatos = (DataTable)Session["CE_BUSCAQUEDAESTUDIO"];
                if (e.CommandName == "Entrar")
                {

                    string[] arg = new string[2];
                    arg = e.CommandArgument.ToString().Split(';');
                    String vId = arg[0];
                    String vNombre = arg[1];
                    //String vMaterial = e.CommandArgument.ToString();

                    Response.Redirect("/sites/cableado/page/cotizacion/aprobacionContabilidad.aspx?i=" + vId + "&n=" + vNombre);

                }

            //    string vDatoPrincipal = e.CommandArgument.ToString();

                //    string vIdEstudio = e.CommandArgument.ToString();
                //    if (e.CommandName == "Descargar")
                //    {

                //        DateTime fecha = DateTime.Now;
                //        string vFecha = fecha.ToString("dd MMMM yyy");

                //        DataTable vDatos1 = new DataTable();

                //        String vQueryStock = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 17," + vIdEstudio;
                //        DataTable vDatosStock = vConexion.obtenerDataTable(vQueryStock);


                //        for (int i = 0; i < vDatosStock.Rows.Count; i++)
                //        {
                //            Session["CE_IDSTOCKBANCO"] = vDatosStock.Rows[i]["idStock"].ToString();
                //        }

                //        string vMensaje = "";

                //        String vQueryCosto = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 16, " + vIdEstudio;

                //        Int32 vInformacion = vConexion.ejecutarSql(vQueryCosto);
                //        DataTable vDataCosto = vConexion.obtenerDataTable(vQueryCosto);

                //        //if (vDataCosto.Rows.Count == 0)
                //        //{
                //        //    vMensaje = "Pendiente de realizar la Cotización del Estudio";
                //        //    Mensaje(vMensaje, WarningType.Danger);
                //        //}

                //        //Ingreso Datos Texto
                //        string vCostoTotalCotizacion, vNodo, vParticipantes, vNombreEstudio;
                //        string  vTotalMateriales, vManoObra, vCostoTotalProyecto, vIsv, vTotalCotizacion;

                //        vCostoTotalCotizacion = vDataCosto.Rows[0]["costoTotalCotizacion"].ToString();
                //        vNodo = vDataCosto.Rows[0]["nodos"].ToString();
                //        vParticipantes = vDataCosto.Rows[0]["numeroParticipantes"].ToString();
                //        vTotalMateriales = vDataCosto.Rows[0]["costoTotalMaterial"].ToString();
                //        vManoObra = vDataCosto.Rows[0]["costoManoObra"].ToString();
                //        vCostoTotalProyecto = vDataCosto.Rows[0]["costoTotalProyecto"].ToString();
                //        vIsv = vDataCosto.Rows[0]["isvCostoFinal"].ToString();
                //        vTotalCotizacion = vDataCosto.Rows[0]["costoTotalCotizacion"].ToString();
                //        vNombreEstudio = vDataCosto.Rows[0]["nombre"].ToString();

                //        //Ingreso Datos Materiales y Costos

                //        String vQueryMaterial = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 18, " + vIdEstudio;
                //        DataTable vDataMaterial = vConexion.obtenerDataTable(vQueryMaterial);


                //        object ObjMIss = System.Reflection.Missing.Value;
                //        Word.Application ObjWord = new Word.Application();

                //        string path = HttpRuntime.AppDomainAppPath + "page\\plantillas\\PlantillaAprobacionBanco.docx";

                //        object parametro = path;
                //        object vObjCostoCotizacion = "costoTotalCotizacion";
                //        object vObjNodos = "nodos";
                //        object vObjParticipantes = "participantes";
                //        object vObjNodo = "nodo";
                //        object vObjFecha = "fecha";
                //        object vObjTotalMateriales = "totalMateriales";
                //        object vObjManoObra = "manoObraContra";
                //        object vObjCostoTotalProyecto = "costoTotalProyecto";
                //        object vObjISV = "isv";
                //        object vObjTotalCotizacion = "totalCotizacion";
                //        object vObjNombre = "nombreEstudio";
                //        object vObjNombreMat = "nombreEstudioMat";

                //        Word.Document ObjDoc = ObjWord.Documents.Open(parametro, ObjMIss);

                //        Word.Range vRangeCostoTotalCotizacion = ObjDoc.Bookmarks.get_Item(ref vObjCostoCotizacion).Range;
                //        vRangeCostoTotalCotizacion.Text = vCostoTotalCotizacion;

                //        Word.Range vRangeNodos = ObjDoc.Bookmarks.get_Item(ref vObjNodos).Range;
                //        vRangeNodos.Text = vNodo;

                //        Word.Range vRangeParticipantes = ObjDoc.Bookmarks.get_Item(ref vObjParticipantes).Range;
                //        vRangeParticipantes.Text = vParticipantes;

                //        Word.Range vRangeNodo = ObjDoc.Bookmarks.get_Item(ref vObjNodo).Range;
                //        vRangeNodo.Text = vNodo;

                //        Word.Range vRangeFecha = ObjDoc.Bookmarks.get_Item(ref vObjFecha).Range;
                //        vRangeFecha.Text = Convert.ToString(vFecha);

                //        Word.Range vRangeTotalMateriales = ObjDoc.Bookmarks.get_Item(ref vObjTotalMateriales).Range;
                //        vRangeTotalMateriales.Text = vTotalMateriales;

                //        Word.Range vRangeManoObra = ObjDoc.Bookmarks.get_Item(ref vObjManoObra).Range;
                //        vRangeManoObra.Text = vManoObra;

                //        Word.Range vRangeCostoTotalProyecto = ObjDoc.Bookmarks.get_Item(ref vObjCostoTotalProyecto).Range;
                //        vRangeCostoTotalProyecto.Text = vCostoTotalProyecto;

                //        Word.Range vRangeIsv = ObjDoc.Bookmarks.get_Item(ref vObjISV).Range;
                //        vRangeIsv.Text = vIsv;

                //        Word.Range vRangeTotalCotizacion = ObjDoc.Bookmarks.get_Item(ref vObjTotalCotizacion).Range;
                //        vRangeTotalCotizacion.Text = vTotalCotizacion;

                //        Word.Range vRangeNombre = ObjDoc.Bookmarks.get_Item(ref vObjNombre).Range;
                //        vRangeNombre.Text = vNombreEstudio;

                //        Word.Range vRangeNombreMat = ObjDoc.Bookmarks.get_Item(ref vObjNombreMat).Range;
                //        vRangeNombreMat.Text = vNombreEstudio;

                //        //Parametros Tabla Dinamica

                //        object rango1 = vRangeFecha;
                //        object rango2 = vRangeCostoTotalCotizacion;
                //        object rango3 = vRangeNodos;
                //        object rango4 = vRangeParticipantes;
                //        object rango5 = vRangeNodo;
                //        object rango6 = vRangeTotalMateriales;
                //        object rango7 = vRangeManoObra;
                //        object rango8 = vRangeCostoTotalProyecto;
                //        object rango9 = vRangeIsv;
                //        object rango10 = vRangeTotalCotizacion;
                //        object rango11 = vRangeNombre;
                //        object rango12 = vRangeNombreMat;

                //        ObjDoc.Bookmarks.Add("fecha", ref rango1);
                //        ObjDoc.Bookmarks.Add("costoTotalCotizacion", ref rango2);
                //        ObjDoc.Bookmarks.Add("nodos", ref rango3);
                //        ObjDoc.Bookmarks.Add("participantes", ref rango4);
                //        ObjDoc.Bookmarks.Add("nodo", ref rango5);
                //        ObjDoc.Bookmarks.Add("totalMateriales", ref rango6);
                //        ObjDoc.Bookmarks.Add("manoObraContra", ref rango7);
                //        ObjDoc.Bookmarks.Add("costoTotalProyecto", ref rango8);
                //        ObjDoc.Bookmarks.Add("isv", ref rango9);
                //        ObjDoc.Bookmarks.Add("totalCotizacion", ref rango10);
                //        ObjDoc.Bookmarks.Add("nombreEstudio", ref rango11);
                //        ObjDoc.Bookmarks.Add("nombreEstudioMat", ref rango12);

                //        //Rando de donde comenzara la tabla dinamica
                //        object start = 1070;
                //        object end = 1071;

                //        Word.Range rng = ObjDoc.Range(ref start, ref end);
                //        Word.Table tbl = ObjDoc.Tables.Add(rng, 1, 4, ref ObjMIss, ref ObjMIss);

                //        //Titutlo de la tabla dinamica
                //        SetHeadings(tbl.Cell(1, 1), "Material");

                //        SetHeadings(tbl.Cell(1, 2), "Q");

                //        SetHeadings(tbl.Cell(1, 3), "Costo U");

                //        SetHeadings(tbl.Cell(1, 4), "Costo Total");


                //        for (int i = 0; i < vDataMaterial.Rows.Count; i++)

                //        {
                //            //Row de la tabla dinamica
                //            Word.Row newRow = ObjDoc.Tables[1].Rows.Add(ref ObjMIss);

                //            newRow.Range.Bold = 0;
                //            newRow.Range.Font.Name = "Arial";
                //            newRow.Range.Font.Size = 12;
                //            newRow.Range.Borders.InsideLineStyle = WdLineStyle.wdLineStyleSingle;
                //            newRow.Range.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;
                //            newRow.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

                //            newRow.Cells[1].Range.Text = vDataMaterial.Rows[i][0].ToString();

                //            newRow.Cells[2].Range.Text = vDataMaterial.Rows[i][1].ToString();

                //            newRow.Cells[3].Range.Text = vDataMaterial.Rows[i][2].ToString();

                //            newRow.Cells[4].Range.Text = vDataMaterial.Rows[i][3].ToString();

                //        }

                //        ObjWord.Visible = true;

                //    }

            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
                //}

                //void SetHeadings(Word.Cell tblCell, string text)
                //{

                //    tblCell.Range.Text = text;
                //    tblCell.Range.Bold = 1;
                //    tblCell.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                //    tblCell.Column.Width = 100;
                //    tblCell.Range.Font.Name = "Arial";
                //    tblCell.Range.Font.Size = 12;
                //    tblCell.Range.Borders.InsideLineStyle = WdLineStyle.wdLineStyleSingle;
                //    tblCell.Range.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;


            }

        }

        protected void TxBuscarEstudio_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CargarProceso();
                String vBusqueda = TxBuscarEstudio.Text;
                DataTable vDatos = (DataTable)Session["CE_BUSCAQUEDAESTUDIO"];
                if (vBusqueda.Equals(""))
                {
                    GVPrincipal.DataSource = vDatos;
                    GVPrincipal.DataBind();
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

                    GVPrincipal.DataSource = vDatosFiltrados;
                    GVPrincipal.DataBind();
                    Session["CE_BUSCAQUEDAESTUDIO"] = vDatosFiltrados;
                    udpContabilidad.Update();
                }

            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        //void CargarEstudios()
        //{
        //    try
        //    {
        //        DataTable vDatos = new DataTable();
        //        vDatos = vConexion.obtenerDataTable("STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 15");

        //        GVPrincipal.DataSource = vDatos;
        //        GVPrincipal.DataBind();
        //        Session["CE_BUSCAQUEDAESTUDIO"] = vDatos;

        //        //vDatos = vConexion.obtenerDataTable("RSP_ObtenerGenerales 12");
               
        //    }
        //    catch (Exception Ex)
        //    {
        //        Mensaje(Ex.Message, WarningType.Danger);
        //    }

        //}
    }
}