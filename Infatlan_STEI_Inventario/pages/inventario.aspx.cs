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
    public partial class salidas : System.Web.UI.Page
    {
        db vConexion = new db();
        protected void Page_Load(object sender, EventArgs e){
            Session["AUTH"] = true;
            Session["USUARIO"] = "wpadilla";
            if (!Page.IsPostBack){
                if (Convert.ToBoolean(Session["AUTH"])){
                    limpiarSessiones();
                    cargarDatos();
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

                //STOCK
                vQuery = "[STEISP_INVENTARIO_Stock] 1";
                vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0){
                    DDLArticulo.Items.Clear();
                    DDLArticulo.Items.Add(new ListItem { Value = "0", Text = "Seleccione" });
                    foreach (DataRow item in vDatos.Rows){
                        DDLArticulo.Items.Add(new ListItem { Value = item["idStock"].ToString(), Text = item["TipoStock"].ToString() + " - " + item["modelo"].ToString() + " (" + item["cantidad"].ToString() + ")"});
                    }
                }

                //UBICACIONES
                vQuery = "[STEISP_INVENTARIO_Ubicacaiones] 1";
                vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0){
                    DDLUbicacion.Items.Clear();
                    DDLUbicacion.Items.Add(new ListItem { Value = "0", Text = "Seleccione" });
                    foreach (DataRow item in vDatos.Rows){
                        DDLUbicacion.Items.Add(new ListItem { Value = item["idUbicacion"].ToString(), Text = item["codigo"].ToString() });
                    }
                }

                // DEPARTAMENTOS
                vQuery = "STEISP_INVENTARIO_Generales 1";
                vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0){
                    DDLDepartamento.Items.Clear();
                    DDLDepartamento.Items.Add(new ListItem { Value = "0", Text = "Seleccione" });
                    foreach (DataRow item in vDatos.Rows){
                        DDLDepartamento.Items.Add(new ListItem { Value = item["idDepartamento"].ToString(), Text = item["nombre"].ToString() });
                    }
                }

                // TIPO UBICACION
                vQuery = "[STEISP_INVENTARIO_Generales] 3";
                vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0){
                    DDLTipoUbic.Items.Clear();
                    DDLTipoUbic.Items.Add(new ListItem { Value = "0", Text = "Seleccione" });
                    foreach (DataRow item in vDatos.Rows){
                        DDLTipoUbic.Items.Add(new ListItem { Value = item["idTipoUbicacion"].ToString(), Text = item["nombre"].ToString() });
                    }
                }

                // TIPO TRANSACCION
                vQuery = "[STEISP_INVENTARIO_Generales] 8";
                vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0){
                    DDLTipoTransaccion.Items.Clear();
                    DDLTipoTransaccion.Items.Add(new ListItem { Value = "0", Text = "Seleccione" });
                    foreach (DataRow item in vDatos.Rows){
                        DDLTipoTransaccion.Items.Add(new ListItem { Value = item["idTipoTransaccion"].ToString(), Text = item["nombre"].ToString() });
                    }
                }
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        public void Mensaje(string vMensaje, WarningType type){
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        protected void DDLDepartamento_SelectedIndexChanged(object sender, EventArgs e){
            try{
                cargarMunicipios(DDLDepartamento.SelectedValue);
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        private void cargarMunicipios(String vIdDepto) {
            if (vIdDepto != "0"){
                String vQuery = "STEISP_INVENTARIO_Generales 2," + DDLDepartamento.SelectedValue;
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0){
                    DDLMunicipio.Items.Clear();
                    DDLMunicipio.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    foreach (DataRow item in vDatos.Rows){
                        DDLMunicipio.Items.Add(new ListItem { Value = item["idMunicipio"].ToString(), Text = item["nombre"].ToString() });
                    }
                }
            }else
                DDLMunicipio.Items.Clear();
        }

        protected void BtnAddUbicacion_Click(object sender, EventArgs e){
            limpiarModalUbic();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalU();", true);
        }

        private void limpiarModalUbic(){
            DDLTipoUbic.SelectedValue = "0";
            DDLDepartamento.SelectedValue = "0";
            DDLMunicipio.Items.Clear();
            TxCodigoUbic.Text = string.Empty;
            TxDireccionUbic.Text = string.Empty;
            DivMensajeUbic.Visible = false;
            LbMensajeUbic.Text = string.Empty;
        }

        protected void BtnConfirmar_Click(object sender, EventArgs e){
            try{
                String vQuery = "[STEISP_INVENTARIO_Stock] 2," + DDLArticulo.SelectedValue;
                DataTable vDataStock = vConexion.obtenerDataTable(vQuery);
                Decimal vCantidad = 0;
                Decimal vCantidadActual = 0;
                if (vDataStock.Rows.Count > 0)
                    vCantidad = Convert.ToDecimal(vDataStock.Rows[0]["cantidad"].ToString());
                if (vCantidad > 0){
                    if (Convert.ToDecimal(TxCantidad.Text) <= vCantidad)
                        vCantidadActual = vCantidad - Convert.ToDecimal(TxCantidad.Text);
                    else
                        throw new Exception("La cantidad solicitada es mayor a la disponible.");
                }else
                    throw new Exception("No hay unidades disponibles de este artículo.");

                generarxml vMaestro = new generarxml();
                Object[] vDatosMaestro = new object[10];
                vDatosMaestro[0] = TxCodigo.Text;
                vDatosMaestro[1] = DDLArticulo.SelectedValue;
                vDatosMaestro[2] = DDLUbicacion.SelectedValue;
                vDatosMaestro[3] = ""; //Responsable
                vDatosMaestro[4] = TxDescripcion.Text;
                vDatosMaestro[5] = ""; // Observaciones
                vDatosMaestro[6] = ""; // Serie
                vDatosMaestro[7] = TxPrecio.Text;
                vDatosMaestro[8] = Session["USUARIO"].ToString();
                vDatosMaestro[9] = DDLTipoTransaccion.SelectedValue;
                String vXML = vMaestro.ObtenerMaestroString(vDatosMaestro);
                vXML = vXML.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");

                vQuery = "[STEISP_INVENTARIO_Principal] 1" +
                    "," + DDLArticulo.SelectedValue + 
                    "," + vCantidadActual + 
                    ",'" + vXML + "'";
                Int32 vInfo = vConexion.ejecutarSql(vQuery);
                if (vInfo == 3){
                    Mensaje("Transacción realizada con éxito.", WarningType.Success);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "cerrarModal();", true);
                }

            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void BtnGuardarInventario_Click(object sender, EventArgs e){
            try{
                validarDatos();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "abrirModal();", true);
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        private void validarDatos(){
            if (DDLArticulo.SelectedValue == "0")
                throw new Exception("Favor seleccione el articulo.");
            if (TxCantidad.Text == "" || TxCantidad.Text == string.Empty)
                throw new Exception("Favor ingrese la cantidad del artículo.");
            if (TxPrecio.Text == "" || TxPrecio.Text == string.Empty)
                throw new Exception("Favor ingrese el precio del artículo.");
            if (DDLUbicacion.SelectedValue == "0")
                throw new Exception("Favor seleccione la nueva ubicación del articulo.");
            if (DDLTipoTransaccion.SelectedValue == "0")
                throw new Exception("Favor seleccione el tipo de transaccion a realizar.");
        }

        protected void BtnAceptarUbic_Click(object sender, EventArgs e){
            try{
                validarDatosUbic();
                String vQuery = "";
                int vInfo;
                DataTable vDatos = new DataTable();
                vQuery = "[STEISP_INVENTARIO_Ubicacaiones] 3" +
                        "," + DDLTipoUbic.SelectedValue +
                        "," + DDLMunicipio.SelectedValue +
                        ",'" + TxCodigoUbic.Text.ToUpper() + "'" + 
                        ",'" + TxDireccionUbic.Text + "'" +
                        ",'" + Session["USUARIO"].ToString() + "'";

                vInfo = vConexion.ejecutarSql(vQuery);

                if (vInfo == 1){
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
                    vQuery = "[STEISP_INVENTARIO_Ubicacaiones] 1";
                    vDatos = vConexion.obtenerDataTable(vQuery);

                    if (vDatos.Rows.Count > 0){
                        DDLUbicacion.Items.Clear();
                        DDLUbicacion.Items.Add(new ListItem { Value = "0", Text = "Seleccione" });
                        foreach (DataRow item in vDatos.Rows){
                            DDLUbicacion.Items.Add(new ListItem { Value = item["idUbicacion"].ToString(), Text = item["codigo"].ToString() });
                        }
                    }
                    DDLUbicacion.SelectedValue = Convert.ToString(DDLUbicacion.Items.Count - 1);
                }
            }catch (Exception ex){
                LbMensajeUbic.Text = ex.Message;
                DivMensajeUbic.Visible = true;
            }
        }

        private void validarDatosUbic(){
            if (DDLTipoUbic.SelectedValue == "0")
                throw new Exception("Favor seleccione el tipo de ubicación.");
            if (DDLDepartamento.SelectedValue == "0")
                throw new Exception("Favor seleccione el departamento.");
            if (DDLMunicipio.SelectedValue == "0" || DDLMunicipio.SelectedValue == "")
                throw new Exception("Favor seleccione el municipio.");
            if (TxCodigoUbic.Text == "" || TxCodigoUbic.Text == string.Empty)
                throw new Exception("Favor ingrese el código.");
            if (TxDireccionUbic.Text == "" || TxDireccionUbic.Text == string.Empty)
                throw new Exception("Favor ingrese la dirección.");
        }
    }
}