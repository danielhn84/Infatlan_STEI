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

using Infatlan_STEI_Inventario.clases;
using System.Globalization;

namespace Infatlan_STEI_CableadoEstructurado.paginas
{
    public partial class aprobacionContabilidad : System.Web.UI.Page
    {
        db vConexion = new db();

        Boolean vRegistro = false;
        int vColor = 0;
        decimal vCantidadEditada;

        protected void Page_Load(object sender, EventArgs e){
            if (!Page.IsPostBack){
                if (Convert.ToBoolean(Session["AUTH"])){
                    if (Request.QueryString["io"] != null){
                        Session["CE_IDESTUDIOPRINCIPAL"] = Request.QueryString["io"];
                        Label15.Text = "Oferta Económica";
                        Label16.Text = "Editar";
                        ObtenerDatos();
                    }else{
                        Session["CE_IDESTUDIOPRINCIPAL"] = Request.QueryString["i"];
                        Label15.Text = "Cotización";
                        Label16.Text = "Estudio";
                    }

                    DataTable vDatos = new DataTable();
                    vDatos = vConexion.obtenerDataTable("STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 32, '" + Session["CE_IDESTUDIOPRINCIPAL"] + "'");

                    lbNombre.Text = vDatos.Rows[0]["nombre"].ToString();
                    LbResponsable.Text = vDatos.Rows[0]["responsable"].ToString();

                    CargarProceso();
                   // BindItemList();
                }else {
                    Response.Redirect("/login.aspx");
                }
            }

            if (vColor == 1){
                navCostos.Visible = false;
            }
            //string.Format(CultureInfo.CurrentCulture, "{0:C}", double.Parse(txtCostoTotalMateriales.Text));

        }

        //void BindItemList()
        //{
        //    DataTable vDatos = new DataTable();
        //    if (ViewState["CostoTotalMateriales"] == null)
        //    {
        //        Session["CE_IDESTUDIOPRINCIPAL"] = Request.QueryString["i"].ToString();

        //        vDatos = vConexion.obtenerDataTable("STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 5, '" + Session["CE_IDESTUDIOPRINCIPAL"] + "'");

        //        Decimal Price = 0;
        //        for (int i = 0; i < vDatos.Rows.Count; i++)
        //        {
        //            Price += vDatos.Rows[i].Field<Decimal>(2);
        //        }
               
        //        ViewState["CostoTotalMateriales"] = Price;
        //    }
        //    GVContabilidad.DataSource = vDatos;
        //    GVContabilidad.DataBind();
        //}

        //Datos
  
        void CargarProceso()
        {
            try
            {
                var sumTotal = 0.00;

                //Session["CE_IDESTUDIOPRINCIPAL"] = Request.QueryString["i"].ToString();

                DataTable vDatos = new DataTable();
                vDatos = vConexion.obtenerDataTable("STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 5, '" + Session["CE_IDESTUDIOPRINCIPAL"] + "'");


                if (txtModCantidad.Text != "" & LbModificarMaterial.Text == "Agregar Material")
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
               // udpContabilidad.Update();
                udpCostoTotalMateriales.Update();
                Session["CE_PRESUPUESTO"] = vDatos;
                GVContabilidad.Columns[4].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                GVContabilidad.Columns[5].ItemStyle.HorizontalAlign = HorizontalAlign.Center;

                CargarCosto();

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

        void CargarCosto() 
        {
            var sumTotal = 0.00;

            //Session["CE_IDESTUDIOPRINCIPAL"] = Request.QueryString["i"].ToString();

            DataTable vDatos = new DataTable();
            vDatos = vConexion.obtenerDataTable("STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 5, '" + Session["CE_IDESTUDIOPRINCIPAL"] + "'");

            for (int i = 0; i < vDatos.Rows.Count; i++)
            {
                if (vDatos.Rows.Count == 1 )
                {
                    string vValor = Convert.ToString(Convert.ToDecimal(vDatos.Rows[i]["costoTotal"].ToString()));
                    txtCostoTotalMateriales.Text = "L " + Convert.ToDecimal(vValor).ToString("##,###.00");
                }
                else
                {
                    sumTotal += Convert.ToDouble(Convert.ToDecimal(vDatos.Rows[i]["costoTotal"]));
                    txtCostoTotalMateriales.Text = "L " + Convert.ToDecimal(sumTotal).ToString("##,###.00");

                    //txtCostoTotalMateriales.Text = Convert.ToString(Math.Round(Convert.ToDecimal(sumTotal), 2));
                }
            }
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

        void ObtenerDatos()
        {
            try
            {
                DataTable vDatos = new DataTable();
                vDatos = vConexion.obtenerDataTable("STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 35, '" + Session["CE_IDESTUDIOPRINCIPAL"] + "'");

                txtHorasExtras.Text= vDatos.Rows[0]["horasExtras"].ToString();
                txtGastoViaje.Text = vDatos.Rows[0]["gastosViaje"].ToString();
                txtManoObraContra.Text = vDatos.Rows[0]["manoObraContra"].ToString();
                txtTransporte.Text = vDatos.Rows[0]["transporte"].ToString();
                txtAlimentacion.Text = vDatos.Rows[0]["alimentacion"].ToString();
                txtHospedaje.Text = vDatos.Rows[0]["hospedaje"].ToString();
                txtImprevistos.Text = vDatos.Rows[0]["imprevistos"].ToString();
                txtTotalNodos.Text = vDatos.Rows[0]["nodos"].ToString();
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

                    CargarddlMaterial();

                    foreach (DataRow item in vDatos.Rows)
                    {
                        txtModCantidad.Text = item["cantidad"].ToString();
                        txtModCostoUnitario.Text = item["precio"].ToString();
                        ddlModMaterial.SelectedValue = item["material"].ToString();
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
              
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }

        }
  
        //protected void GVContabilidad_RowDataBound(object sender, GridViewRowEventArgs e)
        //{

        //    try
        //    {

        //        //if (e.Row.RowType == DataControlRowType.DataRow)
        //        //{
        //        //    Label lblPageTotal = (Label)e.Row.FindControl("txtCostoTotalMateriales1");
        //        //    Page_Sum += Decimal.Parse(lblPageTotal.Text);
        //        //}
        //        //if (e.Row.RowType == DataControlRowType.Footer)
        //        //{
        //        //    if (ViewState["CostoTotalMateriales"] != null && Page_Sum != 0)
        //        //    {

        //        //        Label txtCostoTotalMateriales = (Label)e.Row.FindControl("txtCostoTotalMateriales1");
        //        //        txtCostoTotalMateriales.Text = ViewState["CostoTotalMateriales"].ToString();
        //        //    }
        //        // }
                

        //            //CAMBIAR COLOR SI LAS CANTIDADES SON DIFERENTES
        //            //if (e.Row.RowType == DataControlRowType.DataRow)
        //            //{
        //            //string vCantidadActual = "";
        //            //string vCantidadInventario = "";
        //            ////e.Row.Attributes.Add("onmouseover","this.originalcolor=this.style.backgroundColor;" + " this.style.backgroundColor='#edf6ff;");
        //            ////e.Row.Attributes.Add("onmouseout","this.style.backgroundColor=this.originalcolor;");

        //            //vCantidadActual = Convert.ToString(System.Web.UI.DataBinder.Eval(e.Row.DataItem, "cantidad"));
        //            //vCantidadInventario = Convert.ToString(System.Web.UI.DataBinder.Eval(e.Row.DataItem, "cantidadStock"));

        //            //if (Convert.ToDecimal(vCantidadActual) > Convert.ToDecimal(vCantidadInventario))
        //            //{
        //            //    e.Row.Cells[4].ForeColor = Color.Red;
        //            //    e.Row.Cells[5].ForeColor = Color.Red;
        //            //    vColor = 1;
        //            //    throw new Exception("La Cantidad Solicitada es Mayor a Inventario.");
        //            //}
        //            //vColor = 0;
        //            ////if (vCantidadActual == "4.00")
        //            ////{
        //            ////    e.Row.Cells[5].BackColor = Color.FromName("#f74557");
        //            ////}
        //        //}



        //    }
        //    catch (Exception Ex) { Mensaje(Ex.Message, WarningType.Danger); }

        //}

        //Validaciones

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

        void ValidarModalContabilidad()
        {

            try
            {
                if (ddlModMaterial.SelectedValue == "")
                {
                    DivAlertaContabilidad.Visible = true;
                    LbAlertaContabilidad.Text = "Por favor ingrese el material";
                   
                }
                if (txtModCantidad.Text == "" || txtModCantidad.Text == string.Empty)
                {
                    DivAlertaContabilidad.Visible = true;
                    LbAlertaContabilidad.Text = "Por favor ingrese la cantidad del material";
                }
            }

            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
                DivAlertaContabilidad.Visible = true;
                LbAlertaContabilidad.Text = Ex.Message;
            }
        }

        //Botones
        protected void BtnModModificarMaterial_Click(object sender, EventArgs e)
        {
            try
            {
                string vMensaje = "";
                Int32 vInformacion;
                String vQuery;

                CargarProceso();
                ValidarModalContabilidad();

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


                //vMensaje = "Actualizado con Exito!";

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
                    CargarCosto();
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

        protected void btnCalcular_Click(object sender, EventArgs e)
        {

            try
            {

                string vCosto = txtCostoTotalMateriales.Text;
                String[] vResult = vCosto.Split(' ');

                //Calcular Costos Totales
                Session["CE_COSTO"] = Convert.ToString(Convert.ToDecimal(vResult[1]) + Convert.ToDecimal(txtHorasExtras.Text) + Convert.ToDecimal(txtGastoViaje.Text) + Convert.ToDecimal(txtManoObraContra.Text) + Convert.ToDecimal(txtTransporte.Text) + Convert.ToDecimal(txtAlimentacion.Text) + Convert.ToDecimal(txtHospedaje.Text) + Convert.ToDecimal(txtImprevistos.Text));

                txtCostoTotal.Text = "L.  " + Convert.ToString(Math.Round((Convert.ToDecimal(Session["CE_COSTO"])), 2));

                Session["CE_GANANCIA"] = Convert.ToDecimal(txtHorasExtras.Text) + Convert.ToDecimal(txtGastoViaje.Text) + Convert.ToDecimal(txtManoObraContra.Text) + Convert.ToDecimal(txtTransporte.Text) + Convert.ToDecimal(txtAlimentacion.Text) + Convert.ToDecimal(txtHospedaje.Text) + Convert.ToDecimal(txtImprevistos.Text);

                //txtGanancia.Text = "Lps.  " + Session["CE_GANANCIA"].ToString();

                Session["CE_ISVGANANCIA"] = Convert.ToString(Math.Round((Convert.ToDecimal(Session["CE_GANANCIA"]) * Convert.ToDecimal(0.15)), 2));


                txtIsvGanancia.Text = "L.  " + Session["CE_ISVGANANCIA"];

                Session["CE_PROPUESTA"] = Convert.ToString(Math.Round((Convert.ToDecimal(Session["CE_COSTO"]) + Convert.ToDecimal(Session["CE_ISVGANANCIA"])), 2));

                txtPropuesta.Text = "L.   " + Session["CE_PROPUESTA"];

                Session["CE_ISVCOSTOTOTAL"] = Convert.ToString(Math.Round((Convert.ToDecimal(Session["CE_PROPUESTA"]) * Convert.ToDecimal(0.15)), 2));

                txtisvCostoTotal.Text = "L.  " + Session["CE_ISVCOSTOTOTAL"];

                Session["CE_TOTALCOT"] = Convert.ToString(Math.Round((Convert.ToDecimal(Session["CE_PROPUESTA"]) + Convert.ToDecimal(Session["CE_ISVCOSTOTOTAL"])), 2));

                txtTotalCot.Text = "L.  " + Session["CE_TOTALCOT"];
                //Costo Nodos

                Session["CE_NODOLPS"] = Convert.ToString(Math.Round((Convert.ToDecimal(Session["CE_TOTALCOT"]) / Convert.ToDecimal(txtTotalNodos.Text)), 2));

                txtCostoNodoLps.Text = "L.  " + Session["CE_NODOLPS"];

                Session["CE_NODOUSD"] = Convert.ToString(Math.Round((Convert.ToDecimal(Session["CE_NODOLPS"]) / Convert.ToDecimal(24.5)), 2));

                txtCostoNodoUsd.Text = "L.  " + Session["CE_NODOUSD"];


                //Calculo Total Cotización

                Session["CE_TOTALMATERIALES"] = Convert.ToString(Math.Round((Convert.ToDecimal(vResult[1])), 2));

                txtTotalMateriales.Text = "L.  " + Session["CE_TOTALMATERIALES"];

                Session["CE_COSTOMANOOBRA"] = Convert.ToString(Math.Round((Convert.ToDecimal(Session["CE_GANANCIA"]) + Convert.ToDecimal(Session["CE_ISVGANANCIA"])), 2));

                txtCostosManoObra.Text = "L.  " + Session["CE_COSTOMANOOBRA"];

                Session["CE_COSTOTOTALPROYECTO"] = Convert.ToString(Math.Round((Convert.ToDecimal(Session["CE_COSTOMANOOBRA"]) + Convert.ToDecimal(Session["CE_TOTALMATERIALES"])), 2));

                txtCostoTotalProyecto.Text = "L.  " + Session["CE_COSTOTOTALPROYECTO"];

                Session["CE_ISVCOTIZACION"] = Convert.ToString(Math.Round((Convert.ToDecimal(Session["CE_COSTOTOTALPROYECTO"]) * Convert.ToDecimal(0.15)), 2));

                txtIsvCotizacion.Text = "L.  " + Session["CE_ISVCOTIZACION"];

                Session["CE_COSTOTOTALCOTIZACION"] = Convert.ToString(Math.Round((Convert.ToDecimal(Session["CE_COSTOTOTALPROYECTO"]) + Convert.ToDecimal(Session["CE_ISVCOTIZACION"])), 2));

                txtCostoTotalCotizacion.Text = "L.  " + Session["CE_COSTOTOTALCOTIZACION"];

                //Costo Ganancia

                txtCostoGanancia.Text = "L.  " + Session["CE_GANANCIA"];
                txtCostoIsvGanancia.Text = "L.  " + Session["CE_ISVGANANCIA"];
                txtCostoManosObraGanancia.Text = "L.  " + Session["CE_COSTOMANOOBRA"];


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
                    if (txtModObservaciones.Text == "")
                    {
                        DivAlertaDescriptor.Visible = true;
                        LbAlertaDescriptor.Text = "Favor ingresar comentario";
                    }
                    else
                    {

                        String vQuery = "STEISP_CABLESTRUCTURADO_IngresarDatosEstudio 13,'" + Session["CE_IDESTUDIOPRINCIPAL"] + "'," +
                                                                                          "'EdicionTecnico'," +
                                                                                          "'" + txtModObservaciones.Text + "'";
                        Int32 vInformacion = vConexion.ejecutarSql(vQuery);

                        Response.Redirect("/sites/cableado/page/cotizacion/principalPresupuestos.aspx");

                    }

                }
                else
                {
                    if (vColor == 0)
                    {
                        string vCosto = txtCostoTotalMateriales.Text;
                        String[] vResult = vCosto.Split(' ');
                        string vCostoTotal = vResult[1].Replace(",", "");
                        Session["CE_IDESTUDIOPRINCIPAL"] = null;
                        int vEdicion = 0;

                        if (Request.QueryString["io"] != null)
                        {
                            Session["CE_IDESTUDIOPRINCIPAL"] = Request.QueryString["io"];
                            vEdicion = 1;
                        }
                        else
                        {
                            Session["CE_IDESTUDIOPRINCIPAL"] = Request.QueryString["i"];
                        }

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
                                                                                  "'" + vCostoTotal.ToString() + "'," +
                                                                                  "'" + Session["CE_COSTOMANOOBRA"] + "'," +
                                                                                  "'" + Session["CE_COSTOTOTALPROYECTO"] + "'," +
                                                                                  "'" + Session["CE_ISVCOTIZACION"] + "'," +
                                                                                  "'" + Session["CE_COSTOTOTALCOTIZACION"] + "'," +
                                                                                  "'" + Session["CE_IDESTUDIOPRINCIPAL"] + "'," +
                                                                                  "'AprobadoContabilidad','"+vEdicion+"'";


                        Int32 vInformacion1 = vConexion.ejecutarSql(vQuery1);


                        String vQuery = "STEISP_CABLESTRUCTURADO_IngresarDatosEstudio 8," + Session["CE_IDESTUDIOPRINCIPAL"]+ ",'"+vEdicion+"'";
                        Int32 vInformacion = vConexion.ejecutarSql(vQuery);

                        //}
                        Limpiar();
                        udpContabilidad.Update();

                        int vCondicion = 2;
                        string vId;

                        if (Request.QueryString["io"] != null)
                        {
                            vId = Request.QueryString["io"];
                        }
                        else
                        {
                            vId = Request.QueryString["i"];
                        }

                        Response.Redirect("/sites/cableado/page/cotizacion/ofertaEconomica.aspx?a=" + vCondicion + "&i=" + vId);
                    }
                    else
                    {
                        Limpiar();
                        throw new Exception("Cantidad solicitada mayor a cantidad de inventario, solicitar nuevos materiales");

                        //vMensaje = "No cumple con los requerimientos";
                    }

                }

            }
            catch (Exception Ex) { Mensaje(Ex.Message, WarningType.Danger); }

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            CerrarModal("MensajeAceptacionModalBanco");

        }

        protected void btnAgregarMaterial_Click(object sender, EventArgs e)
        {
            txtModCantidad.Text = "";
            txtModCostoUnitario.Visible = false;
            lbModCosto.Visible = false;
            BtnModModificarMaterial.Text = "Agregar";
            UpdModModificar.Update();
            LbModificarMaterial.Text = "Agregar Material";

            udpCostoTotalMateriales.Update();

            ddlModMaterial.Items.Clear();
            ddlModMaterial.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción " });
            CargarddlMaterial();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

        }

        protected void btnEnviarTecnico_Click(object sender, EventArgs e)
        {

            lbMensaje.Text = "El estudio se enviará al responsable para que realice la modificación correspondiente.";
            // lbAlerta.Text = "Se enviará al responsable para que realice la modificación correspondiente.";
            BtnModGuardarConta.Text = "Enviar";
            txtModObservaciones.Visible = true;
            lbComentario.Visible = true;
            udpModGuardar.Update();
            udpModMensajes.Update();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalConta();", true);


        }

        //Otros
        protected void txtTotalNodos_TextChanged(object sender, EventArgs e)
        {


        }

        protected void ddlModMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {

            DataTable vDatos = (DataTable)Session["CE_PRESUPUESTO"];
            string vNombreMaterial = ddlModMaterial.SelectedItem.Text;
            String[] vResult = vNombreMaterial.Split('-');

            if (vDatos != null)
            {
                for (int i = 0; i < vDatos.Rows.Count; i++)
                {
                    txtAlimentacion.Text = vDatos.Rows[i]["material"].ToString();
                    txtCostoGanancia.Text = vResult[0].Trim();

                    if (vResult[0].Trim().ToUpper() == vDatos.Rows[i]["material"].ToString().ToUpper() && txtModCantidad.Text != "")
                    {
                        vDatos.Rows[i]["cantidad"] = Convert.ToInt32(vDatos.Rows[i]["cantidad"].ToString()) + Convert.ToInt32(txtModCantidad.Text);
                    }
                }
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

        public void CerrarModal(String vModal)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Pop", "$('#" + vModal + "').modal('hide');", true);
        }

        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }
    }
 } 
