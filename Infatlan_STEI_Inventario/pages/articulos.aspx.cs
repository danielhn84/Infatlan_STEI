using Infatlan_STEI_Inventario.clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace Infatlan_STEI_Inventario.pages
{
    public partial class articulos : System.Web.UI.Page
    {
        db vConexion = new db();
        protected void Page_Load(object sender, EventArgs e){
            DDLContratos.CssClass = "select2 form-control custom-select";

            if (!Page.IsPostBack){
                if (Convert.ToBoolean(Session["AUTH"])){
                    limpiarSessiones();
                    cargarDatos();
                    cargarDatosEDC();
                    cargarDatosEnlace();
                }else {
                    Response.Redirect("/login.aspx");
                }
            }
        }

        private void limpiarSessiones() { 
            
        }

        private void cargarDatos(){
            try{
                String vQuery = "[STEISP_INVENTARIO_Stock] 1";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0){
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                    Session["INV_STOCK"] = vDatos;
                }

                //TIPO STOCK
                vQuery = "STEISP_INVENTARIO_Generales 7,'False'";
                vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0){
                    DDLTipo.Items.Clear();
                    DDLTipo.Items.Add(new ListItem { Value = "0", Text = "Seleccione" });
                    foreach (DataRow item in vDatos.Rows){
                        DDLTipo.Items.Add(new ListItem { Value = item["idTipoStock"].ToString(), Text = item["nombre"].ToString() });
                    }
                }

                //PROVEEDOR
                vQuery = "[STEISP_INVENTARIO_Generales] 4";
                vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0){
                    DDLProveedor.Items.Clear();
                    DDLProveedorENL.Items.Clear();
                    DDLProveedor.Items.Add(new ListItem { Value = "0", Text = "Seleccione" });
                    DDLProveedorENL.Items.Add(new ListItem { Value = "0", Text = "Seleccione" });
                    foreach (DataRow item in vDatos.Rows){
                        DDLProveedor.Items.Add(new ListItem { Value = item["idProveedor"].ToString(), Text = item["nombre"].ToString() });
                        DDLProveedorENL.Items.Add(new ListItem { Value = item["idProveedor"].ToString(), Text = item["nombre"].ToString() });
                    }
                }

                //MARCAS
                vQuery = "[STEISP_INVENTARIO_Generales] 5";
                vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0){
                    DDLMarca.Items.Clear();
                    DDLMarca.Items.Add(new ListItem { Value = "0", Text = "Seleccione" });
                    foreach (DataRow item in vDatos.Rows){
                        DDLMarca.Items.Add(new ListItem { Value = item["idMarca"].ToString(), Text = item["nombre"].ToString() });
                    }
                }

                // TIPO TRANSACCION
                vQuery = "[STEISP_INVENTARIO_Generales] 8,2";
                vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0){
                    DDLTipoTransaccion.Items.Clear();
                    DDLTipoTransaccion.Items.Add(new ListItem { Value = "0", Text = "Seleccione" });
                    foreach (DataRow item in vDatos.Rows){
                        DDLTipoTransaccion.Items.Add(new ListItem { Value = item["idTipoTransaccion"].ToString(), Text = item["nombre"].ToString() });
                    }
                }

                //ESTADO
                vQuery = "[STEISP_INVENTARIO_Generales] 6";
                vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0){
                    DDLEstado.Items.Clear();
                    DDLEstado.Items.Add(new ListItem { Value = "0", Text = "Seleccione" });
                    foreach (DataRow item in vDatos.Rows){
                        DDLEstado.Items.Add(new ListItem { Value = item["idEstadoStock"].ToString(), Text = item["nombre"].ToString() });
                    }
                }
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        private void cargarDatosEDC() {
            try{
                String vQuery = "[STEISP_INVENTARIO_StockEDC] 1";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0){
                    GvBusquedaEDC.DataSource = vDatos;
                    GvBusquedaEDC.DataBind();
                    Session["INV_STOCKEDC"] = vDatos;
                }

                //TIPO STOCK EDC
                vQuery = "[STEISP_INVENTARIO_Generales] 7,'True'";
                vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0){
                    DDLTipoEquipoEDC.Items.Clear();
                    DDLTipoEquipoEDC.Items.Add(new ListItem { Value = "0", Text = "Seleccione" });
                    foreach (DataRow item in vDatos.Rows){
                        DDLTipoEquipoEDC.Items.Add(new ListItem { Value = item["idTipoStock"].ToString(), Text = item["nombre"].ToString() });
                    }
                }

                // CONTRATOS
                vQuery = "[STEISP_INVENTARIO_Generales] 12";
                vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0){
                    DDLContratos.Items.Clear();
                    DDLContratos.Items.Add(new ListItem { Value = "0", Text = "Seleccione" });
                    foreach (DataRow item in vDatos.Rows){
                        DDLContratos.Items.Add(new ListItem { Value = item["idContrato"].ToString(), Text = item["contrato"].ToString() });
                    }
                }

                // UBICACIONES EDC
                vQuery = "[STEISP_INVENTARIO_Ubicaciones] 1";
                vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0){
                    DDLUbicacionEDC.Items.Clear();
                    DDLUbicacionEDC.Items.Add(new ListItem { Value = "0", Text = "Seleccione" });
                    foreach (DataRow item in vDatos.Rows){
                        int vCarac = item["direccion"].ToString().Length;
                        DDLUbicacionEDC.Items.Add(new ListItem { Value = item["idUbicacion"].ToString(), Text = item["codigo"].ToString() + " - " + item["direccion"].ToString().Substring(0, vCarac > 25 ? 25 : vCarac) });
                    }
                }

                // REGIONES EDC
                vQuery = "[STEISP_INVENTARIO_Generales] 10";
                vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0){
                    DDLRegion.Items.Clear();
                    DDLRegion.Items.Add(new ListItem { Value = "0", Text = "Seleccione" });
                    foreach (DataRow item in vDatos.Rows){
                        DDLRegion.Items.Add(new ListItem { Value = item["idRegion"].ToString(), Text = item["nombre"].ToString() });
                    }
                }
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        private void cargarDatosEnlace() {
            try{
                String vQuery = "[STEISP_INVENTARIO_Enlaces] 1";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0){
                    GvEnlaces.DataSource = vDatos;
                    GvEnlaces.DataBind();
                    Session["INV_ENLACES"] = vDatos;
                }

                //TIPO ENLACE
                vQuery = "[STEISP_INVENTARIO_Generales] 11";
                vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0){
                    DDLTipoEnlace.Items.Clear();
                    DDLTipoEnlace.Items.Add(new ListItem { Value = "0", Text = "Seleccione" });
                    foreach (DataRow item in vDatos.Rows){
                        DDLTipoEnlace.Items.Add(new ListItem { Value = item["idTipoEnlace"].ToString(), Text = item["nombre"].ToString() });
                    }
                }

                //ORIGEN Y DESTINO
                vQuery = "[STEISP_INVENTARIO_StockEDC] 1";
                vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0){
                    DDLOrigen.Items.Clear();
                    DDLDestino.Items.Clear();
                    DDLOrigen.Items.Add(new ListItem { Value = "0", Text = "Seleccione" });
                    DDLDestino.Items.Add(new ListItem { Value = "0", Text = "Seleccione" });
                    foreach (DataRow item in vDatos.Rows){
                        DDLOrigen.Items.Add(new ListItem { Value = item["idStockEDC"].ToString(), Text = item["nombreNodo"].ToString() });
                        DDLDestino.Items.Add(new ListItem { Value = item["idStockEDC"].ToString(), Text = item["nombreNodo"].ToString() });
                    }
                }
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        public void Mensaje(string vMensaje, WarningType type){
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        public void MensajeBlock(string vMensaje, WarningType type){
            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        protected void TxBusqueda_TextChanged(object sender, EventArgs e){
            try{
                cargarDatos();
                String vBusqueda = TxBusqueda.Text.ToUpper();
                DataTable vDatos = (DataTable)Session["INV_STOCK"];
                if (vBusqueda.Equals("")){
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                }else{ 
                    EnumerableRowCollection<DataRow> filtered = vDatos.AsEnumerable()
                        .Where(r => r.Field<String>("TipoStock").Contains(vBusqueda));

                    Boolean isNumeric = int.TryParse(vBusqueda, out int n);

                    if (isNumeric){
                        if (filtered.Count() == 0){
                            filtered = vDatos.AsEnumerable().Where(r =>
                                Convert.ToInt32(r["idStock"]) == Convert.ToInt32(vBusqueda));
                        }
                    }

                    DataTable vDatosFiltrados = new DataTable();
                    vDatosFiltrados.Columns.Add("idStock");
                    vDatosFiltrados.Columns.Add("tipoStock");
                    vDatosFiltrados.Columns.Add("Marca");
                    vDatosFiltrados.Columns.Add("modelo");
                    vDatosFiltrados.Columns.Add("cantidad");
                    vDatosFiltrados.Columns.Add("precioUnit");
                    vDatosFiltrados.Columns.Add("Proveedor");
                    vDatosFiltrados.Columns.Add("descripcion");
                    vDatosFiltrados.Columns.Add("series");

                    foreach (DataRow item in filtered){
                        vDatosFiltrados.Rows.Add(
                            item["idStock"].ToString(),
                            item["tipoStock"].ToString(),
                            item["Marca"].ToString(),
                            item["modelo"].ToString(),
                            item["cantidad"].ToString(),
                            item["precioUnit"].ToString(),
                            item["Proveedor"].ToString(),
                            item["descripcion"].ToString(),
                            item["series"].ToString()
                            );
                    }

                    GVBusqueda.DataSource = vDatosFiltrados;
                    GVBusqueda.DataBind();
                    Session["INV_STOCK"] = vDatosFiltrados;
                }

            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void GVBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e){
            try{
                GVBusqueda.PageIndex = e.NewPageIndex;
                GVBusqueda.DataSource = (DataTable)Session["INV_STOCK"];
                GVBusqueda.DataBind();
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void BtnNuevo_Click(object sender, EventArgs e){
            limpiarModal();
            LbIdArticulo.Text = "Crear Nuevo Artículo";
            Session["INV_STOCK_ID"] = null;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);
        }

        protected void BtnAceptar_Click(object sender, EventArgs e){
            try{
                validarDatos();
                String vQuery, vMensaje = "";
                DataTable vDatos = new DataTable();
                int vInfo;
                Boolean vATM = CBxATM.Checked;
                Boolean vAgencia = CBxAgencia.Checked;
                Boolean vCableado = CBxCE.Checked;

                vQuery = "[STEISP_INVENTARIO_Stock] {0}" +
                        "," + DDLTipo.SelectedValue +
                        "," + DDLProveedor.SelectedValue +
                        ",'" + TxModelo.Text + "'" +
                        "," + DDLMarca.SelectedValue +
                        ",'" + TxDetalle.Text + "'" +
                        ",'" + TxSerie.Text + "'" +
                        "," + TxPrecio.Text  +
                        ",'" + Session["USUARIO"].ToString() + "'" +
                        "," + DDLEstado.Text +
                        ",'" + vATM.ToString()  + "'" +
                        ",'" + vAgencia.ToString()  + "'" +
                        ",'" + vCableado.ToString()  + "'";

                if (HttpContext.Current.Session["INV_STOCK_ID"] == null){
                    vQuery = string.Format(vQuery, "3");
                    vInfo = vConexion.ejecutarSql(vQuery);
                    vMensaje = "Articulo registrado con éxito";
                }else{
                    vQuery = string.Format(vQuery, "4," + Session["INV_STOCK_ID"].ToString());
                    vInfo = vConexion.ejecutarSql(vQuery);
                    vMensaje = "Articulo actualizado con éxito";
                }

                if (vInfo == 1){
                    Mensaje(vMensaje, WarningType.Success);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "cerrarModal();", true);
                    cargarDatos();
                }
            }catch (Exception ex){
                LbAdvertencia.Text = ex.Message;
                DivMensaje.Visible = true;
            }
        }

        private void validarDatos() {
            if (DDLTipo.SelectedValue == "0")
                throw new Exception("Favor seleccione el tipo de producto.");
            if (DDLProveedor.SelectedValue == "0")
                throw new Exception("Favor seleccione el proveedor.");
            if (TxModelo.Text == "" || TxModelo.Text == string.Empty)
                throw new Exception("Favor ingrese el modelo del artículo.");
            if (DDLMarca.SelectedValue == "0")
                throw new Exception("Favor seleccione la marca.");
            if (TxPrecio.Text == "" || TxPrecio.Text == string.Empty)
                throw new Exception("Favor ingrese el precio unitario.");
            if (TxSerie.Text == "" || TxSerie.Text == string.Empty)
                throw new Exception("Favor ingrese la serie del artículo.");
            if (DDLEstado.SelectedValue == "0")
                throw new Exception("Favor seleccione el estado del artículo.");
        }

        void limpiarModal(){
            DDLTipo.SelectedValue = "0";
            DDLProveedor.SelectedValue = "0";
            DDLMarca.SelectedValue = "0";
            DDLEstado.SelectedValue = "0";
            TxPrecio.Text = string.Empty;
            TxDetalle.Text = string.Empty;
            TxModelo.Text = string.Empty;
            TxSerie.Text = string.Empty;
            DivMensaje.Visible = false;
        }

        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e){
            try{
                DataTable vDatos = new DataTable();
                String vQuery = "";

                string vIdArticulo = e.CommandArgument.ToString();
                if (e.CommandName == "EditarArticulo"){
                    DivMensaje.Visible = false;
                    LbMensaje.Text = string.Empty;
                    DivMensajeTA.Visible = false;
                    LbMensajeTA.Text = string.Empty;
                    LbIdArticulo.Text = "Editar Articulo " + vIdArticulo;
                    

                    vQuery = "[STEISP_INVENTARIO_Stock] 2," + vIdArticulo + "";
                    vDatos = vConexion.obtenerDataTable(vQuery);

                    for (int i = 0; i < vDatos.Rows.Count; i++){

                        String vPrecio = vDatos.Rows[i]["precioUnit"].ToString();
                        vPrecio = vPrecio.Replace(",",".");

                        DDLEstado.SelectedValue = vDatos.Rows[i]["idEstadoStock"].ToString();
                        DDLTipo.SelectedValue = vDatos.Rows[i]["idTipoStock"].ToString();
                        DDLProveedor.SelectedValue = vDatos.Rows[i]["idProveedor"].ToString();
                        DDLMarca.SelectedValue = vDatos.Rows[i]["idMarca"].ToString();
                        TxModelo.Text = vDatos.Rows[i]["modelo"].ToString();
                        TxDetalle.Text = vDatos.Rows[i]["descripcion"].ToString();
                        TxSerie.Text = vDatos.Rows[i]["series"].ToString();
                        TxPrecio.Text = vPrecio;
                        CBxATM.Checked = Convert.ToBoolean(vDatos.Rows[i]["atm"].ToString());
                        CBxAgencia.Checked = Convert.ToBoolean(vDatos.Rows[i]["agencias"].ToString());
                        CBxCE.Checked = Convert.ToBoolean(vDatos.Rows[i]["cableadoEstructurado"].ToString());
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }else if (e.CommandName == "EliminarArticulo"){
                    LbTitulo.Text = "Agregar Articulos?";
                    LbMensaje.Text = "Ingrese la cantidad de artículos y presione Aceptar.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ModalConfirmar();", true);
                }
                Session["INV_STOCK_ID"] = vIdArticulo;
            }catch (Exception Ex){
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        protected void BtnConfirmar_Click(object sender, EventArgs e){
            try{
                String vCantidad = Request["tch3"].ToString();
                String vQuery = "[STEISP_INVENTARIO_Stock] 6," + Session["INV_STOCK_ID"].ToString() + "," + vCantidad;
                int vInfo = vConexion.ejecutarSql(vQuery);
                if (vInfo == 1){
                    cargarDatos();
                    Mensaje("Articulo actualizado con éxito.", WarningType.Success);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeConfirmar();", true);
                }
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
            
        }

        protected void BtnAddArticulo_Click(object sender, EventArgs e){
            limpiarModalTA();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openArticuloTipo();", true);
        }

        protected void BtnAgregarTA_Click(object sender, EventArgs e){
            try{
                validarDatosTA();
                String vQuery = "[STEISP_INVENTARIO_Stock] 5" +
                    ",'" + TxNombreTA.Text.ToUpper() + "'" +
                    ",'" + TxDescripcion.Text + "'";
                int vInfo = vConexion.ejecutarSql(vQuery);
                if (vInfo == 1){
                    vQuery = "STEISP_INVENTARIO_Generales 7, 0";
                    DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                    if (vDatos.Rows.Count > 0){
                        DDLTipo.Items.Clear();
                        DDLTipo.Items.Add(new ListItem { Value = "0", Text = "Seleccione" });
                        foreach (DataRow item in vDatos.Rows){
                            DDLTipo.Items.Add(new ListItem { Value = item["idTipoStock"].ToString(), Text = item["nombre"].ToString() });
                        }
                    }
                    DDLTipo.SelectedIndex = vDatos.Rows.Count;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "cerrarArticuloTipo();", true);
                }
            }catch (Exception ex){
                LbMensajeTA.Text = ex.Message;
                DivMensajeTA.Visible = true;
            }
        }

        private void validarDatosTA() {
            if (TxNombreTA.Text == "" || TxNombreTA.Text == string.Empty)
                throw new Exception("Favor ingrese el nombre del tipo de artículo.");
            if (TxDescripcion.Text == "" || TxDescripcion.Text == string.Empty)
                throw new Exception("Favor ingrese una descripción para el tipo de artículo.");
        }

        private void limpiarModalTA() {
            TxNombreTA.Text = string.Empty;
            TxDescripcion.Text = string.Empty;
            LbMensajeTA.Text = string.Empty;
            DivMensajeTA.Visible = false;
        }

        private void limpiarModalProv() {
            TxNombreProv.Text = string.Empty;
            TxResponsableProv.Text = string.Empty;
            TxTelefonoProv.Text = string.Empty;
            TxDireccionProv.Text = string.Empty;
            LbMensajeProv.Text = string.Empty;
            DivMensajeProv.Visible = false;
        }

        protected void BtnAddProv_Click(object sender, EventArgs e){
            limpiarModalProv();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openProv();", true);
        }

        protected void BtnAgregarProv_Click(object sender, EventArgs e){
            try{
                validarDatosProv();
                String vQuery = "[STEISP_INVENTARIO_Proveedores] 3" +
                    ",'" + TxNombreProv.Text.ToUpper() + "'" +
                    ",'" + TxDireccionProv.Text + "'" +
                    ",'" + TxTelefonoProv.Text + "'" + 
                    ",'" + TxResponsableProv.Text + "'";
                int vInfo = vConexion.ejecutarSql(vQuery);
                if (vInfo == 1){
                    vQuery = "STEISP_INVENTARIO_Generales 4";
                    DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                    if (vDatos.Rows.Count > 0){
                        DDLProveedor.Items.Clear();
                        DDLProveedor.Items.Add(new ListItem { Value = "0", Text = "Seleccione" });
                        foreach (DataRow item in vDatos.Rows){
                            DDLProveedor.Items.Add(new ListItem { Value = item["idProveedor"].ToString(), Text = item["nombre"].ToString() });
                        }
                    }
                    DDLProveedor.SelectedValue = Convert.ToString(DDLProveedor.Items.Count -1);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "cerrarProv();", true);

                }
            }catch (Exception ex){
                LbMensajeProv.Text = ex.Message;
                DivMensajeProv.Visible = true;
            }
        }

        private void validarDatosProv(){
            if (TxNombreProv.Text == "" || TxNombreProv.Text == string.Empty)
                throw new Exception("Favor ingrese el nombre del proveedor.");
            if (TxDireccionProv.Text == "" || TxDireccionProv.Text == string.Empty)
                throw new Exception("Favor ingrese la dirección del proveedor.");
            if (TxTelefonoProv.Text == "" || TxTelefonoProv.Text == string.Empty)
                throw new Exception("Favor ingrese el teléfono del proveedor.");
            if (TxResponsableProv.Text == "" || TxResponsableProv.Text == string.Empty)
                throw new Exception("Favor ingrese la persona responsable.");
        }

        protected void BtnAddMarca_Click(object sender, EventArgs e){
            DivMensajeMarca.Visible = false;
            LbMensajeMarca.Text = string.Empty;
            TxNombreMarca.Text = string.Empty;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openMarca();", true);
        }

        protected void BtnAgregarMarca_Click(object sender, EventArgs e){
            try{
                if (TxNombreMarca.Text == string.Empty || TxNombreMarca.Text == "")
                    throw new Exception("Favor ingrese el nombre.");
                
                String vQuery = "[STEISP_INVENTARIO_Marcas] 3" +
                    ",'" + TxNombreMarca.Text.ToUpper() + "'" +
                    ",'" + Session["USUARIO"].ToString() + "',1";
                int vInfo = vConexion.ejecutarSql(vQuery);
                if (vInfo == 1){
                    vQuery = "[STEISP_INVENTARIO_Generales] 5";
                    DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                    if (vDatos.Rows.Count > 0){
                        DDLMarca.Items.Clear();
                        DDLMarca.Items.Add(new ListItem { Value = "0", Text = "Seleccione" });
                        foreach (DataRow item in vDatos.Rows){
                            DDLMarca.Items.Add(new ListItem { Value = item["idMarca"].ToString(), Text = item["nombre"].ToString() });
                        }
                    }
                    DDLMarca.SelectedValue = Convert.ToString(DDLMarca.Items.Count -1);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "cerrarMarca();", true);
                }
            }catch (Exception ex){
                LbMensajeMarca.Text = ex.Message;
                DivMensajeMarca.Visible = true;
            }
        }

        protected void BtnAceptarEDC_Click(object sender, EventArgs e){
            try{
                validarDatosEDC();

                generarxml vMaestro = new generarxml();
                Object[] vDatosMaestro = new object[13];
                vDatosMaestro[0] = DDLContratos.SelectedValue;
                vDatosMaestro[1] = TxNombreNodo.Text;
                vDatosMaestro[2] = DDLTipoEquipoEDC.SelectedValue;
                vDatosMaestro[3] = TxSerieEDC.Text;
                vDatosMaestro[4] = TxIP.Text;
                vDatosMaestro[5] = DDLRegion.SelectedValue;
                vDatosMaestro[6] = TxIOSImage.Text;
                vDatosMaestro[7] = TxIOSVersion.Text; 
                vDatosMaestro[8] = DDLEstadoEDC.SelectedValue;
                vDatosMaestro[9] = TxLatitud.Text;
                vDatosMaestro[10] = TxLongitud.Text;
                vDatosMaestro[11] = TxFechaMant.Text;
                vDatosMaestro[12] = Session["USUARIO"].ToString();
                String vXML = vMaestro.ObtenerMaestroStringEDC(vDatosMaestro);
                vXML = vXML.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");

                Int32 vInfo;
                String vQuery, vMensaje; 

                vQuery = "[STEISP_INVENTARIO_StockEDC] {0},{1}," +
                                "'" + vXML + "'";

                if (HttpContext.Current.Session["INV_STOCKEDC_ID"] == null){
                    vQuery = string.Format(vQuery, "3","0");
                    vInfo = vConexion.ejecutarSql1(vQuery);
                    vMensaje = "Equipo registrado con éxito";
                    if (vInfo > 0){
                        if (insertarInventario(vInfo, DDLUbicacionEDC.SelectedValue, "CREACION DE EDC", TxSerieEDC.Text) == 2){
                            cargarDatosEDC();
                            cargarDatosEnlace();
                            Mensaje(vMensaje, WarningType.Success);
                        }
                    }else{
                        Mensaje("Ha ocurrido un error. Favor comunicarse con sistemas.", WarningType.Danger);
                    }
                }else{
                    vQuery = string.Format(vQuery, "4", Session["INV_STOCKEDC_ID"].ToString());
                    vInfo = vConexion.ejecutarSql(vQuery);
                    vMensaje = "Equipo actualizado con éxito";
                    if (vInfo == 1){
                        cargarDatosEDC();
                        cargarDatosEnlace();
                        Mensaje(vMensaje, WarningType.Success);
                    }else{
                        Mensaje("Ha ocurrido un error. Favor comunicarse con sistemas.", WarningType.Danger);
                    }
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "cerrarModalEDC();", true);

            }catch (Exception ex){
                LbMensajeEDC.Text = ex.Message;
                DivMensajeEDC.Visible = true;
            }
        }

        public int insertarInventario(int vInfo, String vUbicacion, String vDescripcion, String vSerie) {
            generarxml vMaestro = new generarxml();
            Object[] vDatosMaestro = new object[10];
            vDatosMaestro[0] = "";
            vDatosMaestro[1] = vInfo;
            vDatosMaestro[2] = vUbicacion; // DDLUbicacionEDC.SelectedValue;
            vDatosMaestro[3] = ""; //Responsable
            vDatosMaestro[4] = vDescripcion; // "CREACION DE EDC";
            vDatosMaestro[5] = 1;
            vDatosMaestro[6] = vSerie; // Serie
            vDatosMaestro[7] = "";
            vDatosMaestro[8] = Session["USUARIO"].ToString();
            vDatosMaestro[9] = 9;
            String vXML = vMaestro.ObtenerMaestroString(vDatosMaestro);
            vXML = vXML.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");

            String vQuery = "[STEISP_INVENTARIO_Principal] 7" +
                ",0,0" + ",'" + vXML + "'";
            Int32 vInfo2 = vConexion.ejecutarSql(vQuery);
            return vInfo2;
        }

        protected void BtnNuevoEDC_Click(object sender, EventArgs e){
            limpiarModalEDC();
            LbIdArticuloEDC.Text = "Crear Nuevo Equipo de Comunicación";
            Session["INV_STOCKEDC_ID"] = null;

            DDLContratos.CssClass = "select2 form-control custom-select";

            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModalEDC();", true);
        }

        protected void TxBusquedaEDC_TextChanged(object sender, EventArgs e){
            try{
                cargarDatosEDC();
                String vBusqueda = TxBusquedaEDC.Text.ToUpper();
                DataTable vDatos = (DataTable)Session["INV_STOCKEDC"];
                if (vBusqueda.Equals("")){
                    GvBusquedaEDC.DataSource = vDatos;
                    GvBusquedaEDC.DataBind();
                }else{ 
                    EnumerableRowCollection<DataRow> filtered = vDatos.AsEnumerable()
                        .Where(r => r.Field<String>("nombreNodo").Contains(vBusqueda));

                    Boolean isNumeric = int.TryParse(vBusqueda, out int n);

                    if (isNumeric){
                        if (filtered.Count() == 0){
                            filtered = vDatos.AsEnumerable().Where(r =>
                                Convert.ToInt32(r["idStockEDC"]) == Convert.ToInt32(vBusqueda));
                        }
                    }

                    DataTable vDatosFiltrados = new DataTable();
                    vDatosFiltrados.Columns.Add("idStockEDC");
                    vDatosFiltrados.Columns.Add("nombreNodo");
                    vDatosFiltrados.Columns.Add("contrato");
                    vDatosFiltrados.Columns.Add("serie");
                    vDatosFiltrados.Columns.Add("ip");
                    vDatosFiltrados.Columns.Add("regiones");
                    vDatosFiltrados.Columns.Add("latitud");
                    vDatosFiltrados.Columns.Add("longitud");
                    vDatosFiltrados.Columns.Add("fechaMantenimiento");

                    foreach (DataRow item in filtered){
                        vDatosFiltrados.Rows.Add(
                            item["idStockEDC"].ToString(),
                            item["nombreNodo"].ToString(),
                            item["contrato"].ToString(),
                            item["serie"].ToString(),
                            item["ip"].ToString(),
                            item["regiones"].ToString(),
                            item["latitud"].ToString(),
                            item["longitud"].ToString(),
                            item["fechaMantenimiento"].ToString()
                            );
                    }
                    GvBusquedaEDC.DataSource = vDatosFiltrados;
                    GvBusquedaEDC.DataBind();
                    Session["INV_STOCKEDC"] = vDatosFiltrados;
                }
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        void limpiarModalEDC(){
            TxNombreNodo.Text = string.Empty;
            DDLTipoEquipoEDC.SelectedValue = "0";
            DDLContratos.SelectedValue = "0";
            TxSerieEDC.Text = string.Empty;
            TxIP.Text = string.Empty;
            DDLRegion.SelectedValue = "0";
            TxIOSImage.Text = string.Empty;
            TxIOSVersion.Text = string.Empty;
            TxLatitud.Text = string.Empty;
            TxLongitud.Text = string.Empty;
            DDLUbicacionEDC.SelectedValue = "0";
            TxFechaMant.Text = string.Empty;
            DDLEstadoEDC.SelectedValue = "1";
            DivMensajeEDC.Visible = false;
            DivEstadoEDC.Visible = false;
        }

        protected void GvBusquedaEDC_RowCommand(object sender, GridViewCommandEventArgs e){
            try{
                string vIdArticuloEDC = e.CommandArgument.ToString();
                String vQuery = "[STEISP_INVENTARIO_StockEDC] 2," + vIdArticuloEDC + "";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                if (e.CommandName == "EditarArticuloEDC") {
                    DivMensajeEDC.Visible = false;
                    LbMensajeEDC.Text = string.Empty;
                    LbIdArticuloEDC.Text = "Editar Articulo " + vIdArticuloEDC;
                    DivEstadoEDC.Visible = true;
                    for (int i = 0; i < vDatos.Rows.Count; i++) {
                        TxNombreNodo.Text = vDatos.Rows[i]["nombreNodo"].ToString();
                        DDLTipoEquipoEDC.SelectedValue = vDatos.Rows[i]["tipoEquipo"].ToString();
                        DDLContratos.SelectedValue = vDatos.Rows[i]["idContrato"].ToString();
                        TxSerieEDC.Text = vDatos.Rows[i]["serie"].ToString();
                        TxIP.Text = vDatos.Rows[i]["ip"].ToString();
                        DDLRegion.SelectedValue = vDatos.Rows[i]["region"].ToString();
                        TxIOSImage.Text = vDatos.Rows[i]["IOSImage"].ToString();
                        TxIOSVersion.Text = vDatos.Rows[i]["IOSVersion"].ToString();
                        TxLatitud.Text = vDatos.Rows[i]["latitud"].ToString();
                        TxLongitud.Text = vDatos.Rows[i]["longitud"].ToString();
                        DDLUbicacionEDC.SelectedValue = vDatos.Rows[i]["idUbicacion"].ToString();
                        TxFechaMant.Text = Convert.ToDateTime(vDatos.Rows[i]["fechaMantenimiento"]).ToString("yyyy-MM-dd");
                        DDLEstadoEDC.SelectedValue = vDatos.Rows[i]["estado"].ToString();
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalEDC();", true);
                }else if (e.CommandName == "VerInfoEDC"){
                    LbTituloEDC.Text = "Informacion del equipo " + vIdArticuloEDC;

                    for (int i = 0; i < vDatos.Rows.Count; i++) {
                        LbNombre.Text = vDatos.Rows[i]["nombreNodo"].ToString();
                        LbTipo.Text = vDatos.Rows[i]["tipoStock"].ToString();
                        LbContrato.Text = vDatos.Rows[i]["contrato"].ToString();
                        LbSerie.Text = vDatos.Rows[i]["serie"].ToString();
                        LbIP.Text = vDatos.Rows[i]["ip"].ToString();
                        LbRegion.Text = vDatos.Rows[i]["regiones"].ToString();
                        LbIOSImage.Text = vDatos.Rows[i]["IOSImage"].ToString();
                        LbIOSVersion.Text = vDatos.Rows[i]["IOSVersion"].ToString();
                        LbLatitud.Text = vDatos.Rows[i]["latitud"].ToString();
                        LbLongitud.Text = vDatos.Rows[i]["longitud"].ToString();
                        LbFechaMant.Text = Convert.ToDateTime(vDatos.Rows[i]["fechaMantenimiento"]).ToString("yyyy-MM-dd");
                        LbDireccion.Text = vDatos.Rows[i]["direccion"].ToString();
                        LbEstadoEDC.Text = vDatos.Rows[i]["estados"].ToString();
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalInfoEDC();", true);
                }
                Session["INV_STOCKEDC_ID"] = vIdArticuloEDC;
            }catch (Exception Ex){
                if (Ex.Message.Contains("'DDLContratos' tiene un SelectedValue que no es válido"))
                    MensajeBlock("El contrato está deshabilitado.", WarningType.Danger);
                else if(Ex.Message.Contains("'DDLTipoEquipoEDC' tiene un SelectedValue que no es válido"))
                    MensajeBlock("El tipo de Equipo está deshabilitado.", WarningType.Danger);
                else if (Ex.Message.Contains("'DDLRegion' tiene un SelectedValue que no es válido"))
                    MensajeBlock("La región está deshabilitada.", WarningType.Danger);
                else if (Ex.Message.Contains("'DDLUbicacionEDC' tiene un SelectedValue que no es válido"))
                    MensajeBlock("La ubicación está deshabilitada.", WarningType.Danger);
                else
                    MensajeBlock(Ex.Message, WarningType.Danger);
            }
        }

        protected void GvBusquedaEDC_PageIndexChanging(object sender, GridViewPageEventArgs e){
            try{
                GvBusquedaEDC.PageIndex = e.NewPageIndex;
                GvBusquedaEDC.DataSource = (DataTable)Session["INV_STOCKEDC"];
                GvBusquedaEDC.DataBind();

            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        private void validarDatosEDC() {
            if (TxNombreNodo.Text == "" || TxNombreNodo.Text == string.Empty)
                throw new Exception("Favor ingrese el nombre del nodo.");
            if (DDLTipoEquipoEDC.SelectedValue == "0")
                throw new Exception("Favor seleccione el tipo de equipo.");
            if (DDLContratos.SelectedValue == "0")
                throw new Exception("Favor seleccione el contrato.");
            if (TxSerieEDC.Text == "" || TxSerieEDC.Text == string.Empty)
                throw new Exception("Favor ingrese la serie.");
            if (TxIP.Text == "" || TxIP.Text == string.Empty)
                throw new Exception("Favor ingrese la dirección IP.");
            if (DDLRegion.SelectedValue == "0")
                throw new Exception("Favor seleccione la región.");
            if (TxIOSImage.Text == "" || TxIOSImage.Text == string.Empty)
                throw new Exception("Favor ingrese el nombre de la imagen del IOS.");
            if (TxIOSVersion.Text == "" || TxIOSVersion.Text == string.Empty)
                throw new Exception("Favor ingrese el nombre de la version del IOS.");
            if (TxLatitud.Text == "" || TxLatitud.Text == string.Empty)
                throw new Exception("Favor ingrese la serie del artículo.");
            if (TxLongitud.Text == "" || TxLongitud.Text == string.Empty)
                throw new Exception("Favor ingrese la serie del artículo.");
            if (DDLUbicacionEDC.SelectedValue == "0")
                throw new Exception("Favor seleccione el estado del artículo.");
            if (TxFechaMant.Text == "" || TxFechaMant.Text == string.Empty)
                throw new Exception("Favor ingrese la serie del artículo.");
        }

        protected void GvEnlaces_RowCommand(object sender, GridViewCommandEventArgs e){
            try{
                string vIdEnlace = e.CommandArgument.ToString();
                String vQuery = "[STEISP_INVENTARIO_Enlaces] 2," + vIdEnlace + "";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                if (e.CommandName == "EditarEnlace") {
                    DivMensajeENL.Visible = false;
                    LbMensajeENL.Text = string.Empty;
                    LbIdArticuloENL.Text = "Editar Enlace " + vIdEnlace;
                    DivEstadoENL.Visible = true;

                    for (int i = 0; i < vDatos.Rows.Count; i++) {
                        DDLTipoEnlace.SelectedValue = vDatos.Rows[i]["idTipoEnlace"].ToString();
                        DDLProveedorENL.SelectedValue = vDatos.Rows[i]["idProveedor"].ToString();
                        TxNombreENL.Text = vDatos.Rows[i]["nombre"].ToString();
                        TxDescripcionENL.Text = vDatos.Rows[i]["descripcion"].ToString();
                        DDLOrigen.SelectedValue = vDatos.Rows[i]["idOrigen"].ToString();
                        DDLDestino.SelectedValue = vDatos.Rows[i]["idDestino"].ToString();
                        TxIPOrigen.Text = vDatos.Rows[i]["IPOrigen"].ToString();
                        TxIPDestino.Text = vDatos.Rows[i]["IPDestino"].ToString();
                        TxServicios.Text = vDatos.Rows[i]["servicios"].ToString();
                        TxContacto.Text = vDatos.Rows[i]["contacto"].ToString();
                        TxTelefono.Text = vDatos.Rows[i]["telefonoContacto"].ToString();
                        DDLEstadoEnlace.SelectedValue = vDatos.Rows[i]["estado"].ToString();
                    }
                    activarCampos();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalEnlace();", true);
                }else if (e.CommandName == "VerInfo") {
                    for (int i = 0; i < vDatos.Rows.Count; i++) {
                        LbTituloENL.Text = "Información del Enlace " + vDatos.Rows[i]["nombre"].ToString();
                        LbNombreENL.Text = vDatos.Rows[i]["nombre"].ToString();
                        LbTipoENL.Text = vDatos.Rows[i]["tipoEnlace"].ToString();
                        LbProveedorENL.Text = vDatos.Rows[i]["proveedor"].ToString();
                        LbDescripcionENL.Text = vDatos.Rows[i]["descripcion"].ToString();
                        LbOrigenENL.Text = vDatos.Rows[i]["origen"].ToString();
                        LbDestinoENL.Text = vDatos.Rows[i]["destino"].ToString();
                        LbIPOrigenENL.Text = vDatos.Rows[i]["IPOrigen"].ToString();
                        LbIPDestinoENL.Text = vDatos.Rows[i]["IPDestino"].ToString();
                        LbServiciosENL.Text = vDatos.Rows[i]["servicios"].ToString();
                        LbContactoENL.Text = vDatos.Rows[i]["contacto"].ToString();
                        LbTelefonoENL.Text = vDatos.Rows[i]["telefonoContacto"].ToString();
                        LbFechaENL.Text = vDatos.Rows[i]["fechaCreacion"].ToString();
                        LbUsuarioENL.Text = vDatos.Rows[i]["usuarioCreacion"].ToString();
                        LBAdjuntoENL.Text = vDatos.Rows[i]["adjunto"].ToString() != "" ? "Descargar" : "";
                        LbEstadoENL.Text = vDatos.Rows[i]["estado"].ToString();
                        if (vDatos.Rows[i]["adjunto"].ToString() != "")
                            Session["INV_ENLACE_ADJUNTO"] = ConfigurationManager.AppSettings["RUTA_ENLACES2"] + vDatos.Rows[i]["adjunto"].ToString();
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalInfoEnlace();", true);
                }else if (e.CommandName == "SubirAdjunto") {
                    DivMensajeCarga.Visible = false;
                    LbAdvertenciaCarga.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalAdjunto();", true);
                }
                Session["INV_STOCKENL_ID"] = vIdEnlace;
            }catch (Exception Ex){
                if (Ex.Message.Contains("'DDLTipoEnlace' tiene un SelectedValue que no es válido"))
                    MensajeBlock("El Tipo de Enlace está deshabilitado.", WarningType.Danger);
                else if (Ex.Message.Contains("'DDLProveedorENL' tiene un SelectedValue que no es válido"))
                    MensajeBlock("El proveedor está deshabilitado.", WarningType.Danger);
                else if (Ex.Message.Contains("'DDLRegion' tiene un SelectedValue que no es válido"))
                    MensajeBlock("La región está deshabilitada.", WarningType.Danger);
                else if (Ex.Message.Contains("'DDLUbicacionEDC' tiene un SelectedValue que no es válido"))
                    MensajeBlock("La ubicación está deshabilitada.", WarningType.Danger);
                else
                    MensajeBlock(Ex.Message, WarningType.Danger);
            }
        }

        protected void GvEnlaces_PageIndexChanging(object sender, GridViewPageEventArgs e){
            try{
                GvEnlaces.PageIndex = e.NewPageIndex;
                GvEnlaces.DataSource = (DataTable)Session["INV_ENLACES"];
                GvEnlaces.DataBind();

            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void BtnNuevoEnlace_Click(object sender, EventArgs e){
            limpiarModalEnlace();
            activarCampos();
            LbIdArticuloENL.Text = "Crear Nuevo Enlace";
            Session["INV_STOCKENL_ID"] = null;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModalEnlace();", true);
        }

        protected void TxBusquedaEnlace_TextChanged(object sender, EventArgs e){
            try{
                cargarDatosEnlace();
                String vBusqueda = TxBusquedaEnlace.Text.ToUpper();
                DataTable vDatos = (DataTable)Session["INV_ENLACES"];
                if (vBusqueda.Equals("")){
                    GvEnlaces.DataSource = vDatos;
                    GvEnlaces.DataBind();
                }else{ 
                    EnumerableRowCollection<DataRow> filtered = vDatos.AsEnumerable()
                        .Where(r => r.Field<String>("nombre").Contains(vBusqueda));

                    Boolean isNumeric = int.TryParse(vBusqueda, out int n);

                    if (isNumeric){
                        if (filtered.Count() == 0){
                            filtered = vDatos.AsEnumerable().Where(r =>
                                Convert.ToInt32(r["idEnlace"]) == Convert.ToInt32(vBusqueda));
                        }
                    }

                    DataTable vDatosFiltrados = new DataTable();
                    vDatosFiltrados.Columns.Add("idEnlace");
                    vDatosFiltrados.Columns.Add("nombre");
                    vDatosFiltrados.Columns.Add("proveedor");
                    vDatosFiltrados.Columns.Add("tipoEnlace");
                    vDatosFiltrados.Columns.Add("origen");
                    vDatosFiltrados.Columns.Add("destino");
                    vDatosFiltrados.Columns.Add("servicios");
                    vDatosFiltrados.Columns.Add("contacto");

                    foreach (DataRow item in filtered){
                        vDatosFiltrados.Rows.Add(
                            item["idEnlace"].ToString(),
                            item["nombre"].ToString(),
                            item["proveedor"].ToString(),
                            item["tipoEnlace"].ToString(),
                            item["origen"].ToString(),
                            item["destino"].ToString(),
                            item["servicios"].ToString(),
                            item["contacto"].ToString()
                            );
                    }
                    GvEnlaces.DataSource = vDatosFiltrados;
                    GvEnlaces.DataBind();
                    Session["INV_ENLACES"] = vDatosFiltrados;
                }
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void BtnAceptarEnlace_Click(object sender, EventArgs e){
            try{
                validarDatosENL();

                generarxml vMaestro = new generarxml();
                Object[] vDatosMaestro = new object[13];
                vDatosMaestro[0] = DDLTipoEnlace.SelectedValue;
                vDatosMaestro[1] = DDLProveedorENL.SelectedValue;
                vDatosMaestro[2] = TxNombreENL.Text;
                vDatosMaestro[3] = TxDescripcionENL.Text;
                vDatosMaestro[4] = DDLOrigen.SelectedValue;
                vDatosMaestro[5] = DDLDestino.SelectedValue;
                vDatosMaestro[6] = TxIPOrigen.Text;
                vDatosMaestro[7] = TxIPDestino.Text;
                vDatosMaestro[8] = TxServicios.Text;
                vDatosMaestro[9] = TxContacto.Text;
                vDatosMaestro[10] = TxTelefono.Text;
                vDatosMaestro[11] = Session["USUARIO"].ToString();
                vDatosMaestro[12] = DDLEstadoEnlace.SelectedValue;
                String vXML = vMaestro.ObtenerMaestroStringENL(vDatosMaestro);
                vXML = vXML.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");

                Int32 vInfo;
                String vQuery, vMensaje; 

                vQuery = "[STEISP_INVENTARIO_Enlaces] {0},{1}," +
                                "'" + vXML + "'";

                if (HttpContext.Current.Session["INV_STOCKENL_ID"] == null){
                    vQuery = string.Format(vQuery, "3","0");
                    vInfo = vConexion.ejecutarSql1(vQuery);
                    vMensaje = "Equipo registrado con éxito";

                    if (vInfo > 0){
                        if (insertarInventario(vInfo, "0", "CREACION DE ENLACE", "") == 2){
                            cargarDatosEnlace();
                            Mensaje(vMensaje, WarningType.Success);
                        }
                    }else
                        Mensaje("Ha ocurrido un error. Favor comunicarse con sistemas.", WarningType.Danger);
                }else{
                    vQuery = string.Format(vQuery, "4", Session["INV_STOCKENL_ID"].ToString());
                    vInfo = vConexion.ejecutarSql(vQuery);
                    vMensaje = "Equipo actualizado con éxito";

                    if (vInfo == 1 ){
                        cargarDatosEnlace();
                        Mensaje(vMensaje, WarningType.Success);
                    }else
                        Mensaje("Ha ocurrido un error. Favor comunicarse con sistemas.", WarningType.Danger);
                }
                
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "cerrarModalEnlace();", true);

            }catch (Exception ex){
                LbMensajeENL.Text = ex.Message;
                DivMensajeENL.Visible = true;
            }
        }

        void limpiarModalEnlace(){
            TxNombreENL.Text = string.Empty;
            TxDescripcionENL.Text = string.Empty;
            TxIPOrigen.Text = string.Empty;
            TxIPDestino.Text = string.Empty;
            TxServicios.Text = string.Empty;
            TxContacto.Text = string.Empty;
            TxTelefono.Text = string.Empty;
            DDLProveedorENL.SelectedValue = "0";
            DDLTipoEnlace.SelectedValue = "0";
            DDLOrigen.SelectedValue = "0";
            DDLDestino.SelectedValue = "0";
            DDLEstadoEnlace.SelectedValue = "1";
            DivMensajeENL.Visible = false;
            DivEstadoENL.Visible = false;
        }

        protected void DDLTipoEnlace_SelectedIndexChanged(object sender, EventArgs e){
            try{
                activarCampos();
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        void activarCampos() {
            TxServicios.Enabled = true;
            TxContacto.Enabled = true;
            TxTelefono.Enabled = true;
            DDLDestino.Enabled = true;
            TxIPDestino.Enabled = true;
            TxIPOrigen.Enabled = true;

            if (DDLTipoEnlace.SelectedValue == "1" || DDLTipoEnlace.SelectedValue == "2"){
                    TxServicios.Enabled = false;
                    TxContacto.Enabled = false;
                    TxTelefono.Enabled = false;
                    TxServicios.CssClass = "form-control";
                    TxContacto.CssClass = "form-control";
                    TxTelefono.CssClass = "form-control";
            }else if (DDLTipoEnlace.SelectedValue == "3" || DDLTipoEnlace.SelectedValue == "6"){
                DDLDestino.Enabled = false;
                TxIPDestino.Enabled = false;
                TxIPOrigen.Enabled = false;
                TxContacto.Enabled = false;
                TxContacto.CssClass = "form-control";
                DDLDestino.CssClass = "form-control";
                TxIPDestino.CssClass = "form-control";
                TxIPOrigen.CssClass = "form-control";
            }
        }

        private void validarDatosENL() {
            if (TxNombreENL.Text == "" || TxNombreENL.Text == string.Empty)
                throw new Exception("Favor ingrese el nombre del enlace.");
            if (TxDescripcionENL.Text == "" || TxDescripcionENL.Text == string.Empty)
                throw new Exception("Favor ingrese la descripción del enlace.");
            if (DDLProveedorENL.SelectedValue == "0")
                throw new Exception("Favor seleccione el proveedor del enlace.");
            if (DDLTipoEnlace.SelectedValue == "0")
                throw new Exception("Favor seleccione el tipo de enlace.");
            if (DDLOrigen.SelectedValue == "0")
                throw new Exception("Favor seleccione el equipo de origen.");

            if (DDLTipoEnlace.SelectedValue == "1" || DDLTipoEnlace.SelectedValue == "2") {
                if (TxIPOrigen.Text == "" || TxIPOrigen.Text == string.Empty)
                    throw new Exception("Favor ingrese la IP del equipo de origen.");
                if (DDLDestino.SelectedValue == "0")
                    throw new Exception("Favor seleccione el equipo de destino.");
                if (TxIPDestino.Text == "" || TxIPDestino.Text == string.Empty)
                    throw new Exception("Favor ingrese la IP del equipo de destino.");
                if (DDLDestino.SelectedValue == DDLOrigen.SelectedValue)
                    throw new Exception("El equipo de origen debe ser diferente al de destino.");
            }else if (DDLTipoEnlace.SelectedValue == "3" || DDLTipoEnlace.SelectedValue == "6"){
                if (TxServicios.Text == "" || TxServicios.Text == string.Empty)
                    throw new Exception("Favor ingrese el servicio.");
            }else if (DDLTipoEnlace.SelectedValue == "4" || DDLTipoEnlace.SelectedValue == "5") {
                if (TxIPOrigen.Text == "" || TxIPOrigen.Text == string.Empty)
                    throw new Exception("Favor ingrese la IP del equipo de origen.");
                if (DDLDestino.SelectedValue == "0")
                    throw new Exception("Favor seleccione el equipo de destino.");
                if (TxIPDestino.Text == "" || TxIPDestino.Text == string.Empty)
                    throw new Exception("Favor ingrese la IP del equipo de destino.");
                if (TxServicios.Text == "" || TxServicios.Text == string.Empty)
                    throw new Exception("Favor ingrese el servicio.");
                if (TxContacto.Text == "" || TxContacto.Text == string.Empty)
                    throw new Exception("Favor ingrese el contacto.");
                if (TxTelefono.Text == "" || TxTelefono.Text == string.Empty)
                    throw new Exception("Favor ingrese el telefono del contacto.");
                if (DDLDestino.SelectedValue == DDLOrigen.SelectedValue)
                    throw new Exception("El equipo de origen debe ser diferente al de destino.");
            }
        }

        protected void BtnCargar_Click(object sender, EventArgs e){
            try{
                String vIdArticuloENL = Session["INV_STOCKENL_ID"].ToString();

                String archivoLog = string.Format("{0}_{1}", Convert.ToString(Session["USUARIO"]), DateTime.Now.ToString("yyyyMMddHHmmss"));
                String vDireccionCarga = ConfigurationManager.AppSettings["RUTA_ENLACES1"].ToString();
                if (FUCarga.HasFile){
                    String vNombreArchivo = FUCarga.FileName;
                    vDireccionCarga += "/" + archivoLog + "_" + vNombreArchivo;

                    FUCarga.SaveAs(vDireccionCarga);
                    if (File.Exists(vDireccionCarga)) {
                        String vQuery = "[STEISP_INVENTARIO_Enlaces] 5" +
                            "," + vIdArticuloENL + ", null " +
                            ",'" + archivoLog + "_" + vNombreArchivo + "'";
                        int vInfo = vConexion.ejecutarSql(vQuery);
                        if (vInfo == 1){
                            Mensaje("Archivo cargado con éxito!", WarningType.Success);
                        }else { 
                            Mensaje("Hubo un error al insertar en la base de datos. Favor comuníquese con sistemas.", WarningType.Danger);
                        }
                    }
                }else{
                    DivMensajeCarga.Visible = true;
                    LbAdvertenciaCarga.Text = "No se encontró ningún archivo a cargar.";
                }
            }catch (Exception ex){
                DivMensajeCarga.Visible = true;
                LbAdvertenciaCarga.Text = ex.Message;
            }
        }

        protected void LBAdjuntoENL_Click(object sender, EventArgs e){
            try{
                String vIdEnlace = Session["INV_ENLACE_ADJUNTO"].ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "window.open('" + vIdEnlace + "');", true);
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
    }
}