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
            Session["AUTH"] = true;
            Session["USUARIO"] = "wpadilla";

            DDLUbicacionEDC.CssClass = "select2 form-control custom-select";

            if (!Page.IsPostBack){
                if (Convert.ToBoolean(Session["AUTH"])){
                    limpiarSessiones();
                    cargarDatos();
                    cargarDatosEDC();
                    cargarDatosEnlace();
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
                    DDLProveedor.Items.Add(new ListItem { Value = "0", Text = "Seleccione" });
                    foreach (DataRow item in vDatos.Rows){
                        DDLProveedor.Items.Add(new ListItem { Value = item["idProveedor"].ToString(), Text = item["nombre"].ToString() });
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
                vQuery = "[STEISP_INVENTARIO_Contratos] 1";
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
                        DDLUbicacionEDC.Items.Add(new ListItem { Value = item["idUbicacion"].ToString(), Text = item["codigo"].ToString() + " - " + item["direccion"].ToString().Substring(0, vCarac > 20 ? 20 : vCarac) });
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
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        public void Mensaje(string vMensaje, WarningType type){
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        protected void TxBusqueda_TextChanged(object sender, EventArgs e){
            try{
                cargarDatos();
                String vBusqueda = TxBusqueda.Text;
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
                        DDLEstado.SelectedValue = vDatos.Rows[i]["idEstadoStock"].ToString();
                        DDLTipo.SelectedValue = vDatos.Rows[i]["idTipoStock"].ToString();
                        DDLProveedor.SelectedValue = vDatos.Rows[i]["idProveedor"].ToString();
                        DDLMarca.SelectedValue = vDatos.Rows[i]["idMarca"].ToString();
                        TxModelo.Text = vDatos.Rows[i]["modelo"].ToString();
                        TxDetalle.Text = vDatos.Rows[i]["descripcion"].ToString();
                        TxSerie.Text = vDatos.Rows[i]["series"].ToString();
                        TxPrecio.Text = vDatos.Rows[i]["precioUnit"].ToString();
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
                    ",'" + TxNombreTA.Text + "'" +
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
                    ",'" + TxNombreProv.Text + "'" +
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

                String vQuery = "[STEISP_INVENTARIO_StockEDC] 3,0," +
                    "'" + vXML + "'";
                Int32 vInfo = vConexion.ejecutarSql(vQuery);
                if (vInfo == 1){
                    if (insertarInventario() == 2){
                        cargarDatosEDC();
                        Mensaje("Transacción realizada con éxito.", WarningType.Success);
                    }
                }else{
                    Mensaje("Ha ocurrido un error. Favor comunicarse con sistemas.", WarningType.Danger);
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "cerrarModalEDC();", true);

            }catch (Exception ex){
                LbMensajeEDC.Text = ex.Message;
                DivMensajeEDC.Visible = true;
            }
        }

        private int insertarInventario() {
            generarxml vMaestro = new generarxml();
            Object[] vDatosMaestro = new object[10];
            vDatosMaestro[0] = "";
            vDatosMaestro[1] = DDLTipoEquipoEDC.SelectedValue;
            vDatosMaestro[2] = DDLUbicacionEDC.SelectedValue;
            vDatosMaestro[3] = ""; //Responsable
            vDatosMaestro[4] = "CREACION DE EDC";
            vDatosMaestro[5] = 1;
            vDatosMaestro[6] = TxSerieEDC.Text; // Serie
            vDatosMaestro[7] = "";
            vDatosMaestro[8] = Session["USUARIO"].ToString();
            vDatosMaestro[9] = 9;
            String vXML = vMaestro.ObtenerMaestroString(vDatosMaestro);
            vXML = vXML.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");

            String vQuery = "[STEISP_INVENTARIO_Principal] 7" +
                ",0,0" + ",'" + vXML + "'";
            Int32 vInfo = vConexion.ejecutarSql(vQuery);
            return vInfo;
        }

        protected void BtnNuevoEDC_Click(object sender, EventArgs e){
            limpiarModalEDC();
            LbIdArticuloEDC.Text = "Crear Nuevo Equipo de Comunicación";
            Session["INV_STOCKEDC_ID"] = null;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModalEDC();", true);
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
        }

        protected void GvBusquedaEDC_RowCommand(object sender, GridViewCommandEventArgs e){
            try{
                DataTable vDatos = new DataTable();
                String vQuery = "";

                string vIdArticuloEDC = e.CommandArgument.ToString();
                if (e.CommandName == "EditarArticuloEDC") {
                    DivMensajeEDC.Visible = false;
                    LbMensajeEDC.Text = string.Empty;
                    LbIdArticuloEDC.Text = "Editar Articulo " + vIdArticuloEDC;

                    vQuery = "[STEISP_INVENTARIO_StockEDC] 2," + vIdArticuloEDC + "";
                    vDatos = vConexion.obtenerDataTable(vQuery);

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
                        //DDLUbicacionEDC.SelectedValue = vDatos.Rows[i]["nombreNodo"].ToString();
                        TxFechaMant.Text = vDatos.Rows[i]["fechaMantenimiento"].ToString();
                        DDLEstadoEDC.SelectedValue = vDatos.Rows[i]["estado"].ToString();
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalEDC();", true);
                }else if (e.CommandName == "VerInfoEDC"){ 
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalInfoEDC();", true);
                }
                Session["INV_STOCKEDC_ID"] = vIdArticuloEDC;
            }catch (Exception Ex){
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        protected void GvBusquedaEDC_PageIndexChanging(object sender, GridViewPageEventArgs e){

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

        }

        protected void GvEnlaces_PageIndexChanging(object sender, GridViewPageEventArgs e){

        }

        protected void BtnNuevoEnlace_Click(object sender, EventArgs e){

        }

        protected void TxBusquedaEnlace_TextChanged(object sender, EventArgs e){

        }

        protected void TxBusquedaEDC_TextChanged(object sender, EventArgs e){

        }
    }
}