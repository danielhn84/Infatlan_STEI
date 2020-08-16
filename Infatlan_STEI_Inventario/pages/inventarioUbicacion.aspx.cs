using Infatlan_STEI_Inventario.clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace Infatlan_STEI_Inventario.pages
{
    public partial class inventarios : System.Web.UI.Page
    {
        db vConexion = new db();
        Security vSecurity = new Security();
        protected void Page_Load(object sender, EventArgs e){
            String vIdUbicacion = " " + Request.QueryString["i"];
            LbUbicacion.Text = " " + Request.QueryString["c"];
            DDLNueva.CssClass = "select2 form-control custom-select";

            if (!Page.IsPostBack){
                if (Convert.ToBoolean(Session["AUTH"])){
                    limpiarSessiones();
                    if (vSecurity.ObtenerPermiso(Session["USUARIO"].ToString(), 1).Edicion){
                        foreach (GridViewRow item in GVBusqueda.Rows){
                            LinkButton LbEdit = item.FindControl("BtnEditar") as LinkButton;
                            LbEdit.Visible = true;
                        }
                    }

                    cargarDatos(vIdUbicacion);
                }else {
                    Response.Redirect("/login.aspx");
                }
            }
        }

        private void limpiarSessiones() { 
            
        }

        private void cargarDatos(String vId){
            try{

                String vQuery = "[STEISP_INVENTARIO_Principal] 5," + vId;
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                GVBusqueda.DataSource = vDatos;
                GVBusqueda.DataBind();
                Session["INV_UBIC_ARTICULO"] = vDatos;

                //UBICACIONES
                vQuery = "[STEISP_INVENTARIO_Ubicaciones] 1";
                vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0){
                    DDLNueva.Items.Clear();
                    DDLNueva.Items.Add(new ListItem { Value = "0", Text = "Seleccione" });
                    foreach (DataRow item in vDatos.Rows){
                        int vCarac = item["direccion"].ToString().Length;
                        DDLNueva.Items.Add(new ListItem { Value = item["idUbicacion"].ToString(), Text = item["codigo"].ToString() + " - " + item["direccion"].ToString().Substring(0, vCarac > 25 ? 25 : vCarac) });
                    }
                }
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        public void Mensaje(string vMensaje, WarningType type){
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        private void limpiarModalUbic(){

        }

        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e){
            try{
                DataTable vDatos = new DataTable();
                String vQuery = "";

                string vIdInventario = e.CommandArgument.ToString();
                if (e.CommandName == "MoverArticulo"){
                    DivMensaje.Visible = false;
                    
                    //UBICACION ACTUAL
                    vQuery = "[STEISP_INVENTARIO_Ubicaciones] 5, " + vIdInventario;
                    vDatos = vConexion.obtenerDataTable(vQuery);
                    string vId = "";
                    if (vDatos.Rows.Count > 0){
                        TxActual.Text = vDatos.Rows[0]["codigo"].ToString();
                        TxIdInventario.Text = vIdInventario;
                        TxIdUbicacion.Text = vDatos.Rows[0]["idUbicacion"].ToString();
                        if (vDatos.Rows[0]["idStock"].ToString() != ""){
                            vId = vDatos.Rows[0]["idStock"].ToString();
                            TxProceso.Text = "STOCK";
                        }else if (vDatos.Rows[0]["idStockEDC"].ToString() != "") { 
                            vId = vDatos.Rows[0]["idStockEDC"].ToString();
                            TxProceso.Text = "EDC";
                        }else if (vDatos.Rows[0]["idStockEnlace"].ToString() != "") { 
                            vId = vDatos.Rows[0]["idStockEnlace"].ToString();
                            TxProceso.Text = "Enlace";
                        }

                        TxIdStock.Text = vId;
                        TxCodigo.Text = vDatos.Rows[0]["codigoInventario"].ToString();
                        TxPrecio.Text = vDatos.Rows[0]["precio"].ToString();
                        TxCantidadActual.Text = vDatos.Rows[0]["cantidad"].ToString();
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
            }catch (Exception Ex){
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        protected void BtnAceptar_Click(object sender, EventArgs e){
            try{
                //validar la cantidad
                if (Convert.ToDecimal(TxCantidadActual.Text) < Convert.ToDecimal(TxCantidad.Text))
                    throw new Exception("La cantidad solicitada es mayor que la disponible.");

                String vPrecio = "", vTipoTransaccion = "", vQuery = "";

                if (TxProceso.Text == "STOCK") {
                    vQuery = "[STEISP_INVENTARIO_Stock] 2," + TxIdStock.Text;
                    DataTable vDataStock = vConexion.obtenerDataTable(vQuery);
                    Decimal vPrecioDec = Convert.ToDecimal(TxCantidad.Text) * Convert.ToDecimal(vDataStock.Rows[0]["precioUnit"].ToString());
                    vPrecio = vPrecioDec.ToString().Replace(",", ".");
                    vTipoTransaccion = "14";
                }else if (TxProceso.Text == "EDC") 
                    vTipoTransaccion = "18"; 
                else if (TxProceso.Text == "Enlace") 
                    vTipoTransaccion = "20"; 

                generarxml vMaestro = new generarxml();
                Object[] vDatosMaestro = new object[10];
                vDatosMaestro[0] = TxCodigo.Text; 
                vDatosMaestro[1] = TxIdStock.Text;
                vDatosMaestro[2] = DDLNueva.SelectedValue; // NUEVA
                vDatosMaestro[3] = Session["USUARIO"].ToString(); //Responsable
                vDatosMaestro[4] = "CAMBIO UBICACION";
                vDatosMaestro[5] = TxCantidad.Text;
                vDatosMaestro[6] = ""; // Serie
                vDatosMaestro[7] = vPrecio;
                vDatosMaestro[8] = Session["USUARIO"].ToString();
                vDatosMaestro[9] = vTipoTransaccion;

                String vXML = vMaestro.ObtenerMaestroString(vDatosMaestro);
                vXML = vXML.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");

                if (Convert.ToDecimal(TxCantidadActual.Text) == Convert.ToDecimal(TxCantidad.Text)){
                   vQuery = "[STEISP_INVENTARIO_Principal] 3" +
                   "," + TxIdInventario.Text +
                   "," + TxIdUbicacion.Text +  //UBICACION ANTERIOR
                   ",'" + vXML + "'";

                    Int32 vInfo = vConexion.ejecutarSql(vQuery);
                    if (vInfo == 2){
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "cerrarModal();", true);
                        Mensaje("Cambio realizado con éxito.", WarningType.Success);
                        cargarDatos(TxIdUbicacion.Text);
                    }
                }else if (Convert.ToDecimal(TxCantidadActual.Text) > Convert.ToDecimal(TxCantidad.Text)){
                    vQuery = "[STEISP_INVENTARIO_Principal] 6" +
                    "," + TxIdInventario.Text +
                    "," + TxIdUbicacion.Text +  //UBICACION ANTERIOR
                    ",'" + vXML + "'";

                    Int32 vInfo = vConexion.ejecutarSql(vQuery);
                    if (vInfo == 4 || vInfo == 5){
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "cerrarModal();", true);
                        Mensaje("Cambio realizado con éxito.", WarningType.Success);
                        cargarDatos(TxIdUbicacion.Text);
                    }
                }
            }catch (Exception ex){
                DivMensaje.Visible = true;
                LbAdvertencia.Text = ex.Message;
            }
        }

        protected void GVBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e){
            try{
                GVBusqueda.PageIndex = e.NewPageIndex;
                GVBusqueda.DataSource = (DataTable)Session["INV_UBIC_ARTICULO"];
                GVBusqueda.DataBind();
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
    }
}