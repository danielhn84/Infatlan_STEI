﻿using System;
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

using Infatlan_STEI_Inventario.clases;

namespace Infatlan_STEI_CableadoEstructurado.paginas
{
    public partial class aprobacionContabilidad : System.Web.UI.Page
    {
        db vConexion = new db();

        Boolean vRegistro = false;
        int vColor = 0;
        decimal vCantidadEditada;

        protected void Page_Load(object sender, EventArgs e)
        {
            CargarProceso();
            Session["USUARIO"] = "svalle";

            if (vColor == 1)
            {
                navCostos.Visible = false;
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
                var sumTotal = 0.00;


                lbNombre.Text = " " + Request.QueryString["n"];
                Session["CE_IDESTUDIOPRINCIPAL"] = Request.QueryString["i"].ToString();

                DataTable vDatos = new DataTable();
                vDatos = vConexion.obtenerDataTable("STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 5, '" + Session["CE_IDESTUDIOPRINCIPAL"] + "'");


                if (txtModCantidad.Text != "")
                {
                    string vNombreMaterial = ddlModMaterial.SelectedValue;

                    for (int j = 0; j < vDatos.Rows.Count; j++)
                    {
                        if (vNombreMaterial == vDatos.Rows[j]["idStock"].ToString())
                        {
                            vDatos.Rows[j]["cantidad"] = Convert.ToDecimal(vDatos.Rows[j]["cantidad"].ToString()) + Convert.ToDecimal(txtModCantidad.Text);
                            vCantidadEditada = Convert.ToDecimal(vDatos.Rows[j]["cantidad"].ToString());
                            vRegistro = true;
                        }
                    }
                   
                }

                GVContabilidad.DataSource = vDatos;
                GVContabilidad.DataBind();
                udpGVContabilidad.Update();
                udpContabilidad.Update();
                udpCostoTotalMateriales.Update();
                Session["CE_PRESUPUESTO"] = vDatos;
                GVContabilidad.Columns[4].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                GVContabilidad.Columns[5].ItemStyle.HorizontalAlign = HorizontalAlign.Center;


               


                for (int i = 0; i < vDatos.Rows.Count; i++)
                {

                    if (vDatos.Rows.Count == 1)
                    {
                        txtCostoTotalMateriales.Text = Convert.ToString(Convert.ToDecimal(vDatos.Rows[i]["costoTotal"].ToString()));
                    }
                    else
                    {
                        sumTotal += Convert.ToDouble(Convert.ToDecimal(vDatos.Rows[i]["costoTotal"]));
                        //txtCostoTotalMateriales.Text = sumTotal.ToString();
                        txtCostoTotalMateriales.Text = Convert.ToString(Math.Round(Convert.ToDecimal(sumTotal), 2));
                    }

                    
                }


                String vQueryPDF = "STEISP_CABLESTRUCTURADO_Aprobacion 5, '" + Session["CE_IDESTUDIOPRINCIPAL"] + "'";
                DataTable vDatosPDF = vConexion.obtenerDataTable(vQueryPDF);

                String vFUPlano = vDatosPDF.Rows[0]["PDFPlano"].ToString();
                string srcPlano = "data:application/pdf;base64," + vFUPlano;
                IframePlano.Src = srcPlano;
                IframePlano.Visible = true;
                
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        protected void GVContabilidad_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            try
            {

                GVContabilidad.DataSource = (DataTable)Session["CE_PRESUPUESTO"];
                GVContabilidad.PageIndex = e.NewPageIndex;
                GVContabilidad.DataBind();
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }

        }

        protected void GVContabilidad_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            try
            {
                string[] arg = new string[2];
                arg = e.CommandArgument.ToString().Split(';');
                Session["CE_IDESTUDIOPRESUPUESTO"] = arg[0];
                Session["CE_IDSTOCKPRESUPUESTO"] = arg[1];

                

                if (e.CommandName == "Modificar")
                {
                    DataTable vDatos = new DataTable();
                    vDatos = vConexion.obtenerDataTable("STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 6," + Session["CE_IDESTUDIOPRESUPUESTO"] + "," + "'" + Session["CE_IDSTOCKPRESUPUESTO"] + "'");

                    foreach (DataRow item in vDatos.Rows)
                    {
                        txtModCantidad.Text = item["cantidad"].ToString();
                        txtModCostoUnitario.Text = item["precio"].ToString();
                        ddlModMaterial.SelectedValue = item["material"].ToString();
                        CargarddlMaterial();
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
              
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }

        }

        public void Limpiar()
        {
            try
            {

                txtHorasExtras.Text = "";
                udpHorasExtras.Update();

                txtGastoViaje.Text = "";
                udpGastoViaje.Update();

                txtManoObraContra.Text = "";
                udpManoObraContra.Update();

                txtTransporte.Text = "";
                udpTranspote.Update();

                txtAlimentacion.Text = "";
                udpALimentacion.Update();

                txtHospedaje.Text = "";
                udpHospedaje.Update();

                txtImprevistos.Text = "";
                udpImprevistos.Update();

                txtTotalNodos.Text = "";
                udpTotalNodos.Update();

                txtCostoTotal.Text = "";
                udpCostoTotal.Update();

                txtIsvGanancia.Text = "";
                udpGanancia.Update();

                txtPropuesta.Text = "";
                udpPropuesta.Update();

                txtisvCostoTotal.Text = "";
                udpIsvCostos.Update();

                txtTotalCot.Text = "";
                udpTotalCot.Update();

                txtCostoNodoLps.Text = "";
                udpCostoNodoLps.Update();

                txtCostoNodoUsd.Text = "";
                udpCostoNodoUsd.Update();

                txtTotalMateriales.Text = "";
                udpTotalMateriales.Update();

                txtCostosManoObra.Text = "";
                udpCostosManoObra.Update();

                txtCostoTotalProyecto.Text = "";
                udpCostoTotalProyecto.Update();

                txtIsvCotizacion.Text = "";
                udpIsvCotizacion.Update();

                txtCostoTotalCotizacion.Text = "";
                udpCostoTotalCotizacion.Update();

                txtCostoTotalMateriales.Text = "";
                udpCostoTotalMateriales.Update();

                GVContabilidad.DataBind();
                udpGVContabilidad.Update();
                GVContabilidad.DataSource = null;
            }
            catch (Exception)
            {

                throw;
            }

        }

        void ValidarFormContabilidad()
        {

            try
            {
                Session["CE_ERRORCONTABILIDAD"] = 0;

                if (txtHorasExtras.Text == "" || txtHorasExtras.Text == string.Empty)
                {
                    throw new Exception("Por favor ingrese el total de Horas Extras");
                }
                if (txtGastoViaje.Text == "" || txtGastoViaje.Text == string.Empty)
                {
                    throw new Exception("Por favor ingrese el total de Gastos de Viaje del Estudio");
                }
                if (txtManoObraContra.Text == "" || txtManoObraContra.Text == string.Empty)
                {
                    throw new Exception("Por favor ingrese el total de Mano de Obra Contratada");
                }
                if (txtTransporte.Text == "" || txtTransporte.Text == string.Empty)
                {
                    throw new Exception("Por favor ingrese el total de Gastos de Transporte");
                }
                if (txtAlimentacion.Text == "" || txtAlimentacion.Text == string.Empty)
                {
                    throw new Exception("Por favor ingrese el total de Gastos de Alimentación");
                }
                if (txtHospedaje.Text == "" || txtHospedaje.Text == string.Empty)
                {
                    throw new Exception("Por favor ingrese el total de Gastos de Hospedaje");
                }
                if (txtImprevistos.Text == "" || txtImprevistos.Text == string.Empty)
                {
                    throw new Exception("Por favor ingrese el total de Gastos de Imprevistos");
                }
                if (txtTotalNodos.Text == "" || txtTotalNodos.Text == string.Empty)
                {
                    throw new Exception("Por favor ingrese el total de Gastos del Total de Nodos");
                }

            }

            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
                Session["CE_ERRORCONTABILIDAD"] = 1;
            }
        }

        public void CerrarModal(String vModal)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Pop", "$('#" + vModal + "').modal('hide');", true);
        }

        protected void BtnModModificarMaterial_Click(object sender, EventArgs e)
        {
            try
            {
               
                string vMensaje = "";
                Int32 vInformacion;
                String vQuery;

                CargarProceso();

                if (txtModCostoUnitario.Visible == true)
                {
                    vQuery = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 13," + Session["CE_IDESTUDIOPRESUPUESTO"] + "," +
                                                                                     "'" + Session["CE_IDSTOCKPRESUPUESTO"] + "'," +
                                                                                    "'" + txtModCantidad.Text + "'," +
                                                                                    "'" + ddlModMaterial.SelectedValue + "'";

                    vInformacion = vConexion.ejecutarSql(vQuery);
                }
                else
                {

                    
                    
                    if (vRegistro == true)
                    {
                       vQuery = "STEISP_CABLESTRUCTURADO_IngresarDatosEstudio 12," + Session["CE_IDESTUDIOPRINCIPAL"] + "," +
                                                                                    "'" + ddlModMaterial.SelectedValue + "'," +
                                                                                    "'" + vCantidadEditada + "'," +
                                                                                    "'" + 1 + "'";

                    }
                    else
                    {
                        vQuery = "STEISP_CABLESTRUCTURADO_IngresarDatosEstudio 12," + Session["CE_IDESTUDIOPRINCIPAL"] + "," +
                                                                                      "'" + ddlModMaterial.SelectedValue + "'," +
                                                                                      "'" + txtModCantidad.Text + "'," +
                                                                                      "'" + 0 + "'";
                    }
                  
                    vInformacion = vConexion.ejecutarSql(vQuery);

                }


                vMensaje = "Actualizado con Exito!";

                if (vInformacion == 1)
                {

                    
                    Mensaje("Actualizado con Exito!", WarningType.Success);
                    CerrarModal("ModificarMaterialModal");
                    DataTable vDatos = new DataTable();
                    vDatos = vConexion.obtenerDataTable("STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 5, '" + Session["CE_IDESTUDIOPRINCIPAL"] + "'");
                    GVContabilidad.DataSource = vDatos;
                    GVContabilidad.DataBind();
                    udpGVContabilidad.Update();
                    udpContabilidad.Update();
                    udpCostoTotalMateriales.Update();

                    if (vColor == 1)
                    {
                        navCostos.Visible = false;
                    }
                    else if (vColor == 0)
                    {
                        navCostos.Visible = true;
                    }

                }
                else
                {
                    vMensaje = "No se pudo actualizar!";
                    CerrarModal("ModificarMaterialModal");

                }


            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        protected void txtTotalNodos_TextChanged(object sender, EventArgs e)
        {


        }

        protected void btnCalcular_Click(object sender, EventArgs e)
        {

            try
            {

                //Calcular Costos Totales
                Session["CE_COSTO"] = Convert.ToString(Convert.ToDecimal(txtCostoTotalMateriales.Text) + Convert.ToDecimal(txtHorasExtras.Text) + Convert.ToDecimal(txtGastoViaje.Text) + Convert.ToDecimal(txtManoObraContra.Text) + Convert.ToDecimal(txtTransporte.Text) + Convert.ToDecimal(txtAlimentacion.Text) + Convert.ToDecimal(txtHospedaje.Text) + Convert.ToDecimal(txtImprevistos.Text));

                txtCostoTotal.Text = "Lps.  " + Convert.ToString(Math.Round((Convert.ToDecimal(Session["CE_COSTO"])), 2));

                Session["CE_GANANCIA"] = Convert.ToDecimal(txtHorasExtras.Text) + Convert.ToDecimal(txtGastoViaje.Text) + Convert.ToDecimal(txtManoObraContra.Text) + Convert.ToDecimal(txtTransporte.Text) + Convert.ToDecimal(txtAlimentacion.Text) + Convert.ToDecimal(txtHospedaje.Text) + Convert.ToDecimal(txtImprevistos.Text);

                //txtGanancia.Text = "Lps.  " + Session["CE_GANANCIA"].ToString();

                Session["CE_ISVGANANCIA"] = Convert.ToString(Math.Round((Convert.ToDecimal(Session["CE_GANANCIA"]) * Convert.ToDecimal(0.15)), 2));


                txtIsvGanancia.Text = "Lps.  " + Session["CE_ISVGANANCIA"];

                Session["CE_PROPUESTA"] = Convert.ToString(Math.Round((Convert.ToDecimal(Session["CE_COSTO"]) + Convert.ToDecimal(Session["CE_ISVGANANCIA"])), 2));

                txtPropuesta.Text = "Lps.   " + Session["CE_PROPUESTA"];

                Session["CE_ISVCOSTOTOTAL"] = Convert.ToString(Math.Round((Convert.ToDecimal(Session["CE_PROPUESTA"]) * Convert.ToDecimal(0.15)), 2));

                txtisvCostoTotal.Text = "Lps.  " + Session["CE_ISVCOSTOTOTAL"];

                Session["CE_TOTALCOT"] = Convert.ToString(Math.Round((Convert.ToDecimal(Session["CE_PROPUESTA"]) + Convert.ToDecimal(Session["CE_ISVCOSTOTOTAL"])), 2));

                txtTotalCot.Text = "Lps.  " + Session["CE_TOTALCOT"];
                //Costo Nodos

                Session["CE_NODOLPS"] = Convert.ToString(Math.Round((Convert.ToDecimal(Session["CE_TOTALCOT"]) / Convert.ToDecimal(txtTotalNodos.Text)), 2));

                txtCostoNodoLps.Text = "Lps.  " + Session["CE_NODOLPS"];

                Session["CE_NODOUSD"] = Convert.ToString(Math.Round((Convert.ToDecimal(Session["CE_NODOLPS"]) / Convert.ToDecimal(24.5)), 2));

                txtCostoNodoUsd.Text = "Lps.  " + Session["CE_NODOUSD"];


                //Calculo Total Cotización

                Session["CE_TOTALMATERIALES"] = Convert.ToString(Math.Round((Convert.ToDecimal(txtCostoTotalMateriales.Text)), 2));

                txtTotalMateriales.Text = "Lps.  " + Session["CE_TOTALMATERIALES"];

                Session["CE_COSTOMANOOBRA"] = Convert.ToString(Math.Round((Convert.ToDecimal(Session["CE_GANANCIA"]) + Convert.ToDecimal(Session["CE_ISVGANANCIA"])), 2));

                txtCostosManoObra.Text = "Lps.  " + Session["CE_COSTOMANOOBRA"];

                Session["CE_COSTOTOTALPROYECTO"] = Convert.ToString(Math.Round((Convert.ToDecimal(Session["CE_COSTOMANOOBRA"]) + Convert.ToDecimal(Session["CE_TOTALMATERIALES"])), 2));

                txtCostoTotalProyecto.Text = "Lps.  " + Session["CE_COSTOTOTALPROYECTO"];

                Session["CE_ISVCOTIZACION"] = Convert.ToString(Math.Round((Convert.ToDecimal(Session["CE_COSTOTOTALPROYECTO"]) * Convert.ToDecimal(0.15)), 2));

                txtIsvCotizacion.Text = "Lps.  " + Session["CE_ISVCOTIZACION"];

                Session["CE_COSTOTOTALCOTIZACION"] = Convert.ToString(Math.Round((Convert.ToDecimal(Session["CE_COSTOTOTALPROYECTO"]) + Convert.ToDecimal(Session["CE_ISVCOTIZACION"])), 2));

                txtCostoTotalCotizacion.Text = "Lps.  " + Session["CE_COSTOTOTALCOTIZACION"];

                //Costo Ganancia

                txtCostoGanancia.Text = "Lps.  " + Session["CE_GANANCIA"];
                txtCostoIsvGanancia.Text = "Lps.  " + Session["CE_ISVGANANCIA"];
                txtCostoManosObraGanancia.Text = "Lps.  " + Session["CE_COSTOMANOOBRA"];


            }
            catch (Exception Ex)
            {

                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        protected void btnGuardarCotizacion_Click(object sender, EventArgs e)
        {
            try
            {
                ValidarFormContabilidad();
                if ((Session["CE_ERRORCONTABILIDAD"].ToString() == Convert.ToString(0)))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalConta();", true);
                }
                else
                {
                    throw new Exception("Por favor ingrese todos los datos solicitados");
                }
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        protected void BtnModGuardarConta_Click(object sender, EventArgs e)
        {
            String vMensaje = "";

            try
            {
                if (BtnModGuardarConta.Text == "Enviar")
                {
                 
                    Response.Redirect("/sites/cableado/page/cotizacion/principalPresupuestos.aspx");
                }


                if (vColor == 0)
                {
                    

                    String vQuery1 = "STEISP_CABLESTRUCTURADO_IngresarDatosEstudio 6," + txtHorasExtras.Text + "," +
                                                                              "'" + txtGastoViaje.Text + "'," +
                                                                              "'" + txtManoObraContra.Text + "'," +
                                                                              "'" + txtTransporte.Text + "'," +
                                                                              "'" + txtAlimentacion.Text + "'," +
                                                                              "'" + txtHospedaje.Text + "'," +
                                                                              "'" + txtImprevistos.Text + "'," +
                                                                              "'" + txtTotalNodos.Text + "'," +
                                                                              "'" + Session["CE_COSTO"] + "'," +
                                                                              "'" + Session["CE_GANANCIA"] + "'," +
                                                                              "'" + Session["CE_ISVGANANCIA"] + "'," +
                                                                              "'" + Session["CE_PROPUESTA"] + "'," +
                                                                              "'" + Session["CE_ISVCOSTOTOTAL"] + "'," +
                                                                              "'" + Session["CE_TOTALCOT"] + "'," +
                                                                              "'" + Session["CE_NODOLPS"] + "'," +
                                                                              "'" + Session["CE_NODOUSD"] + "'," +
                                                                              "'" + txtCostoTotalMateriales.Text + "'," +
                                                                              "'" + Session["CE_COSTOMANOOBRA"] + "'," +
                                                                              "'" + Session["CE_COSTOTOTALPROYECTO"] + "'," +
                                                                              "'" + Session["CE_ISVCOTIZACION"] + "'," +
                                                                              "'" + Session["CE_COSTOTOTALCOTIZACION"] + "'," +
                                                                              "'" + Session["CE_IDESTUDIOPRINCIPAL"] + "'," +
                                                                              "'AprobadoContabilidad'";


                    Int32 vInformacion1 = vConexion.ejecutarSql(vQuery1);

                    String vQuery = "STEISP_CABLESTRUCTURADO_IngresarDatosEstudio 8," + Session["CE_IDESTUDIOPRINCIPAL"];
                    Int32 vInformacion = vConexion.ejecutarSql(vQuery);


                    //GENERAR XML Para disminucion


                    String vQueryXML = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 22," + Session["CE_IDESTUDIOPRINCIPAL"];

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
                        Decimal vPrecioInventario =  Convert.ToDecimal(vDataStock.Rows[i]["precioUnit"].ToString());
                  
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
                            vDatosMaestro[4] =  "";
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

                            if (vInfo == 3)
                            {
                                //vMensaje = "Transacción realizada con éxito.";

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

                   

                        if (vInformacion1 == 2 )
                        {
                            //CerrarModal("MensajeAceptacionModalContabilidad");
                           Limpiar();
                            
                            // ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalBanco();", true);
                        }
                        else
                        {
                            throw new Exception("No se pudo actualizar!");
                        }


                    }
                    Limpiar();
                    udpContabilidad.Update();

                    int vCondicion = 2;

                    string vId = Request.QueryString["i"];

                    Response.Redirect("/sites/cableado/page/cotizacion/ofertaEconomica.aspx?i=" + vId + "&a=" +vCondicion);
                }
                else
                {
                    Limpiar();
                    throw new Exception("Cantidad solicitada mayor a cantidad de inventario, solicitar nuevos materiales");
                   
                    //vMensaje = "No cumple con los requerimientos";
                }
            }
            catch (Exception Ex) { Mensaje(Ex.Message, WarningType.Danger); }

        }

        void GenerarDoc()
        {

            object ObjMIss = System.Reflection.Missing.Value;
            Word.Application ObjWord = new Word.Application();

            string path = HttpRuntime.AppDomainAppPath + "page\\plantillas\\PlantillaAprobacionBanco.docx";

            object parametro = path;
            object vNombre = "observaciones";
            object vTelefono = "trabajo";

            Word.Document ObjDoc = ObjWord.Documents.Open(parametro, ObjMIss);

            Word.Range nom = ObjDoc.Bookmarks.get_Item(ref vNombre).Range;
            nom.Text = "Parametro1";

            Word.Range tel = ObjDoc.Bookmarks.get_Item(ref vTelefono).Range;
            tel.Text = "Parametro2";

            object rango1 = nom;
            object rango2 = tel;

            ObjDoc.Bookmarks.Add("observaciones", ref rango1);
            ObjDoc.Bookmarks.Add("trabajo", ref rango2);

            ObjWord.Visible = true;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            CerrarModal("MensajeAceptacionModalBanco");

        }

        protected void GVContabilidad_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            try
            {
            
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string vCantidadActual = "";
                    string vCantidadInventario = "";
                    //e.Row.Attributes.Add("onmouseover","this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#edf6ff;");
                    //e.Row.Attributes.Add("onmouseout","this.style.backgroundColor=this.originalcolor;");

                    vCantidadActual = Convert.ToString(System.Web.UI.DataBinder.Eval(e.Row.DataItem, "cantidad"));
                    vCantidadInventario = Convert.ToString(System.Web.UI.DataBinder.Eval(e.Row.DataItem, "cantidadStock"));

                    if (Convert.ToDecimal(vCantidadActual) > Convert.ToDecimal(vCantidadInventario))
                    {
                        e.Row.Cells[4].ForeColor = Color.Red;
                        e.Row.Cells[5].ForeColor = Color.Red;
                        vColor = 1;
                         throw new Exception("La Cantidad Solicitada es Mayor a Inventario.");
                    }
                    vColor = 0;
                    //if (vCantidadActual == "4.00")
                    //{
                    //    e.Row.Cells[5].BackColor = Color.FromName("#f74557");
                    //}
                }

             

            }
            catch (Exception Ex) { Mensaje(Ex.Message, WarningType.Danger); }

        }


        protected void ddlModMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {

            
        }

        void CargarddlMaterial()
        {

            try
            {
                String vQuery = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 7";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0)
                {
                    //ddlModMaterial.Items.Clear();
                    //ddlModMaterial.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción " });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        ddlModMaterial.Items.Add(new ListItem { Value = item["idStock"].ToString(), Text = item["material"].ToString() + " - " + item["modelo"].ToString() + " - " + item["marca"].ToString() });
                    }
                }



            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }


        }

        protected void btnAgregarMaterial_Click(object sender, EventArgs e)
        {
            txtModCantidad.Text = "";
            txtModCostoUnitario.Visible = false;
            lbModCosto.Visible = false;
            BtnModModificarMaterial.Text = "Agregar";
            UpdModModificar.Update();
            LbModificarMaterial.Text = "Agregar Material";

            ddlModMaterial.Items.Clear();
            ddlModMaterial.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción " });
            CargarddlMaterial();


            

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

        }

        protected void btnEnviarTecnico_Click(object sender, EventArgs e)
        {
           
            lbMensaje.Text = "¿Está seguro que desea enviar el estudio al responsable?";
            lbAlerta.Text = "Se enviara al responsable para que realice la modificación correspondiente.";
            BtnModGuardarConta.Text = "Enviar";
            udpModGuardar.Update();
            udpModMensajes.Update();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalConta();", true);

           
        }
    }
 } 
