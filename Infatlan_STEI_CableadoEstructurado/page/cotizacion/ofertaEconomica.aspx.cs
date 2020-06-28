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

        protected void Page_Load(object sender, EventArgs e)
        {
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

        void CargarProceso()
        {
            try
            {
                string vIdEstudio = Request.QueryString["i"];
                string vCondicion = Request.QueryString["a"];

                if (vCondicion == Convert.ToString(2))
                {

                    DataTable vDatos = new DataTable();
                    vDatos = vConexion.obtenerDataTable("STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 23," + vIdEstudio);
                    LbTituloferta.Text = "Oferta económica";
                    LbDescripcionOferta.Text = "Detalle de la oferta creada.";
                    udpContabilidad.Update();
                   
                    GVOfertaEconomica.DataSource = vDatos;
                    GVOfertaEconomica.DataBind();
                    Session["CE_DATOSESTUDIOOFERTA"] = vDatos;

                    foreach (GridViewRow row in GVOfertaEconomica.Rows)
                    {
                        LinkButton button = row.FindControl("BtnAprobar") as LinkButton;
                        button.Enabled = false;
                        button.CssClass = "btn btn-secondary";
                       // button.Title = "Aprobado";
                    }
                }
                else
                {
                    DataTable vDatos = new DataTable();
                    vDatos = vConexion.obtenerDataTable("STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 24");

                    GVOfertaEconomica.DataSource = vDatos;
                    GVOfertaEconomica.DataBind();
                    Session["CE_DATOSESTUDIOOFERTA"] = vDatos;
                    updBuscarAprobacion.Visible = true;

                    foreach (GridViewRow row in GVOfertaEconomica.Rows)
                    {
                        string  vQuery = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 34,'" + row.Cells[0].Text + "'";
                        DataTable vDatosBusqueda = vConexion.obtenerDataTable(vQuery);

                        foreach (DataRow item in vDatosBusqueda.Rows)
                        {
                            if (item["estado"].ToString() == "OfertaAprobada")
                            {
                                LinkButton button = row.FindControl("BtnAprobar") as LinkButton;
                                button.Enabled = false;
                                button.CssClass = "btn btn-secondary";

                                LinkButton button1 = row.FindControl("BtnModificar") as LinkButton;
                                button1.Enabled = false;
                                button1.CssClass = "btn btn-secondary";
                            }
                            else
                            {
                                LinkButton button = row.FindControl("BtnAprobar") as LinkButton;
                                button.Enabled = true;

                                LinkButton button1 = row.FindControl("BtnModificar") as LinkButton;
                                button1.Enabled = true;
                            }
                        }
                    }

                }

            }
            catch (Exception Ex)
            {
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

                    DateTime fecha = DateTime.Now;
                    string vFecha = fecha.ToString("dd MMMM yyy");

                    DataTable vDatos1 = new DataTable();

                    String vQueryStock = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 17," + vIdEstudio;
                    DataTable vDatosStock = vConexion.obtenerDataTable(vQueryStock);


                    for (int i = 0; i < vDatosStock.Rows.Count; i++)
                    {
                        Session["CE_IDSTOCKBANCO"] = vDatosStock.Rows[i]["idStock"].ToString();
                    }

                    string vMensaje = "";

                    String vQueryCosto = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 16, " + vIdEstudio;

                    Int32 vInformacion = vConexion.ejecutarSql(vQueryCosto);
                    DataTable vDataCosto = vConexion.obtenerDataTable(vQueryCosto);

                    //if (vDataCosto.Rows.Count == 0)
                    //{
                    //    vMensaje = "Pendiente de realizar la Cotización del Estudio";
                    //    Mensaje(vMensaje, WarningType.Danger);
                    //}

                    //Ingreso Datos Texto
                    string vCostoTotalCotizacion, vNodo, vParticipantes, vNombreEstudio;
                    string vTotalMateriales, vManoObra, vCostoTotalProyecto, vIsv, vTotalCotizacion;

                    vCostoTotalCotizacion = vDataCosto.Rows[0]["costoTotalCotizacion"].ToString();
                    vNodo = vDataCosto.Rows[0]["nodos"].ToString();
                    vParticipantes = vDataCosto.Rows[0]["numeroParticipantes"].ToString();
                    vTotalMateriales = vDataCosto.Rows[0]["costoTotalMaterial"].ToString();
                    vManoObra = vDataCosto.Rows[0]["costoManoObra"].ToString();
                    vCostoTotalProyecto = vDataCosto.Rows[0]["costoTotalProyecto"].ToString();
                    vIsv = vDataCosto.Rows[0]["isvCostoFinal"].ToString();
                    vTotalCotizacion = vDataCosto.Rows[0]["totalCotizacion"].ToString();
                    vNombreEstudio = vDataCosto.Rows[0]["nombre"].ToString();

                    //Ingreso Datos Materiales y Costos

                    String vQueryMaterial = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 18, " + vIdEstudio;
                    DataTable vDataMaterial = vConexion.obtenerDataTable(vQueryMaterial);


                    object ObjMIss = System.Reflection.Missing.Value;
                    Word.Application ObjWord = new Word.Application();

                    string path = HttpRuntime.AppDomainAppPath + "page\\plantillas\\PlantillaAprobacionBanco.docx";

                    object parametro = path;
                    object vObjCostoCotizacion = "costoTotalCotizacion";
                    object vObjNodos = "nodos";
                    object vObjParticipantes = "participantes";
                    object vObjNodo = "nodo";
                    object vObjFecha = "fecha";
                    object vObjTotalMateriales = "totalMateriales";
                    object vObjManoObra = "manoObraContra";
                    object vObjCostoTotalProyecto = "costoTotalProyecto";
                    object vObjISV = "isv";
                    object vObjTotalCotizacion = "totalCotizacion";
                    object vObjNombre = "nombreEstudio";
                    object vObjNombreMat = "nombreEstudioMat";

                    Word.Document ObjDoc = ObjWord.Documents.Open(parametro, ObjMIss);

                    Word.Range vRangeCostoTotalCotizacion = ObjDoc.Bookmarks.get_Item(ref vObjCostoCotizacion).Range;
                    vRangeCostoTotalCotizacion.Text = vCostoTotalCotizacion;

                    Word.Range vRangeNodos = ObjDoc.Bookmarks.get_Item(ref vObjNodos).Range;
                    vRangeNodos.Text = vNodo;

                    Word.Range vRangeParticipantes = ObjDoc.Bookmarks.get_Item(ref vObjParticipantes).Range;
                    vRangeParticipantes.Text = vParticipantes;

                    Word.Range vRangeNodo = ObjDoc.Bookmarks.get_Item(ref vObjNodo).Range;
                    vRangeNodo.Text = vNodo;

                    Word.Range vRangeFecha = ObjDoc.Bookmarks.get_Item(ref vObjFecha).Range;
                    vRangeFecha.Text = Convert.ToString(vFecha);

                    Word.Range vRangeTotalMateriales = ObjDoc.Bookmarks.get_Item(ref vObjTotalMateriales).Range;
                    vRangeTotalMateriales.Text = vTotalMateriales;

                    Word.Range vRangeManoObra = ObjDoc.Bookmarks.get_Item(ref vObjManoObra).Range;
                    vRangeManoObra.Text = vManoObra;

                    Word.Range vRangeCostoTotalProyecto = ObjDoc.Bookmarks.get_Item(ref vObjCostoTotalProyecto).Range;
                    vRangeCostoTotalProyecto.Text = vCostoTotalProyecto;

                    Word.Range vRangeIsv = ObjDoc.Bookmarks.get_Item(ref vObjISV).Range;
                    vRangeIsv.Text = vIsv;

                    Word.Range vRangeTotalCotizacion = ObjDoc.Bookmarks.get_Item(ref vObjTotalCotizacion).Range;
                    vRangeTotalCotizacion.Text = vTotalCotizacion;

                    Word.Range vRangeNombre = ObjDoc.Bookmarks.get_Item(ref vObjNombre).Range;
                    vRangeNombre.Text = vNombreEstudio;

                    Word.Range vRangeNombreMat = ObjDoc.Bookmarks.get_Item(ref vObjNombreMat).Range;
                    vRangeNombreMat.Text = vNombreEstudio;

                    //Parametros Tabla Dinamica

                    object rango1 = vRangeFecha;
                    object rango2 = vRangeCostoTotalCotizacion;
                    object rango3 = vRangeNodos;
                    object rango4 = vRangeParticipantes;
                    object rango5 = vRangeNodo;
                    object rango6 = vRangeTotalMateriales;
                    object rango7 = vRangeManoObra;
                    object rango8 = vRangeCostoTotalProyecto;
                    object rango9 = vRangeIsv;
                    object rango10 = vRangeTotalCotizacion;
                    object rango11 = vRangeNombre;
                    object rango12 = vRangeNombreMat;

                    ObjDoc.Bookmarks.Add("fecha", ref rango1);
                    ObjDoc.Bookmarks.Add("costoTotalCotizacion", ref rango2);
                    ObjDoc.Bookmarks.Add("nodos", ref rango3);
                    ObjDoc.Bookmarks.Add("participantes", ref rango4);
                    ObjDoc.Bookmarks.Add("nodo", ref rango5);
                    ObjDoc.Bookmarks.Add("totalMateriales", ref rango6);
                    ObjDoc.Bookmarks.Add("manoObraContra", ref rango7);
                    ObjDoc.Bookmarks.Add("costoTotalProyecto", ref rango8);
                    ObjDoc.Bookmarks.Add("isv", ref rango9);
                    ObjDoc.Bookmarks.Add("totalCotizacion", ref rango10);
                    ObjDoc.Bookmarks.Add("nombreEstudio", ref rango11);
                    ObjDoc.Bookmarks.Add("nombreEstudioMat", ref rango12);

                    //Rando de donde comenzara la tabla dinamica
                    object start = 1077;
                    object end = 1078;

                    Word.Range rng = ObjDoc.Range(ref start, ref end);
                    Word.Table tbl = ObjDoc.Tables.Add(rng, 1, 4, ref ObjMIss, ref ObjMIss);

                    //Titutlo de la tabla dinamica
                    SetHeadings(tbl.Cell(1, 1), "Material");

                    SetHeadings(tbl.Cell(1, 2), "Q");

                    SetHeadings(tbl.Cell(1, 3), "Costo U");

                    SetHeadings(tbl.Cell(1, 4), "Costo Total");


                    for (int i = 0; i < vDataMaterial.Rows.Count; i++)

                    {
                        //Row de la tabla dinamica
                        Word.Row newRow = ObjDoc.Tables[1].Rows.Add(ref ObjMIss);

                        newRow.Range.Bold = 0;
                        newRow.Range.Font.Name = "Arial";
                        newRow.Range.Font.Size = 12;
                        newRow.Range.Borders.InsideLineStyle = WdLineStyle.wdLineStyleSingle;
                        newRow.Range.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;
                        newRow.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

                        newRow.Cells[1].Range.Text = vDataMaterial.Rows[i][0].ToString();

                        newRow.Cells[2].Range.Text = vDataMaterial.Rows[i][1].ToString();

                        newRow.Cells[3].Range.Text = vDataMaterial.Rows[i][2].ToString();

                        newRow.Cells[4].Range.Text = vDataMaterial.Rows[i][3].ToString();

                    }

                    ObjWord.Visible = true;

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
            
            String vQueryXML = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 22," + Session["CE_OFERTAIDESTUDIO"];

            DataTable vDataStock = vConexion.obtenerDataTable(vQueryXML);

            for (int i = 0; i < vDataStock.Rows.Count; i++)
            {
                String vCodigo = vDataStock.Rows[i]["codigo"].ToString();
                String vIdStock = vDataStock.Rows[i]["idStock"].ToString();
                String vIdUbicacion = vDataStock.Rows[i]["idUbicacion"].ToString();
                String vIdResponsable = vDataStock.Rows[i]["idResponsable"].ToString();
                String vSerie = vDataStock.Rows[i]["series"].ToString();

                Decimal vCantidadInventario = Convert.ToDecimal(vDataStock.Rows[i]["cantidadInventario"].ToString());
                Decimal vCantidadSolicitada = Convert.ToDecimal(vDataStock.Rows[i]["cantidadSolicitada"].ToString());
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
                    vDatosMaestro[4] = "";
                    vDatosMaestro[5] = vCantidadActual;
                    vDatosMaestro[6] = vSerie;
                    vDatosMaestro[7] = vPrecio;
                    vDatosMaestro[8] = Session["USUARIO"].ToString();
                    vDatosMaestro[9] = 6;
                    String vXML = vMaestro.ObtenerMaestroString(vDatosMaestro);
                    vXML = vXML.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");

                    String vQueryPrincipal = "[STEISP_INVENTARIO_Principal] 1" +
                                            "," + vIdStock +
                                            "," + vCantidadActual +
                                            ",'" + vXML + "'";

                    Int32 vInfo = vConexion.ejecutarSql(vQueryPrincipal);

                    if (vInfo == 4)
                    {
                        //vMensaje = "Transacción realizada con éxito.";
                        DataTable vDatos = new DataTable();
                        string vQuery = "STEISP_CABLESTRUCTURADO_IngresarDatosEstudio 11 ,'" + Session["CE_OFERTAIDESTUDIO"] + "','OfertaAprobada'";
                        vDatos = vConexion.obtenerDataTable(vQuery);
                        
                    }
                    else
                    {

                        throw new Exception("Ha ocurrido un error.");
                    }
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "cerrarModal();", true);
                }
                else
                {
                    throw new Exception("La Cantidad Solicitada es Mayor a Inventario.");
                }

            }
        }

    }

 }