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

namespace Infatlan_STEI_Inventario.pages.Configuracion
{
    public partial class agencias : System.Web.UI.Page
    {
        db vConexion = new db();
        protected void Page_Load(object sender, EventArgs e){
            Session["AUTH"] = true;

            if (!Page.IsPostBack){
                if (Convert.ToBoolean(Session["AUTH"])){
                    cargarDatos();
                }
            }
        }

        private void cargarDatos(){
            try{
                String vQuery = "[STEISP_INVENTARIO_Ubicacaiones] 1";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0){
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                    Session["INV_UBICACIONES"] = vDatos;
                }

                // TIPO UBICACION
                vQuery = "[STEISP_INVENTARIO_Generales] 3";
                vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0){
                    DDLTipo.Items.Clear();
                    DDLTipo.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    foreach (DataRow item in vDatos.Rows){
                        DDLTipo.Items.Add(new ListItem { Value = item["idTipoUbicacion"].ToString(), Text = item["nombre"].ToString() });
                    }
                }

                // DEPARTAMENTOS
                vQuery = "STEISP_INVENTARIO_Generales 1";
                vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0){
                    DDLDepartamento.Items.Clear();
                    DDLDepartamento.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    foreach (DataRow item in vDatos.Rows){
                        DDLDepartamento.Items.Add(new ListItem { Value = item["idDepartamento"].ToString(), Text = item["nombre"].ToString() });
                    }
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
                DataTable vDatos = (DataTable)Session["INV_UBICACIONES"];
                if (vBusqueda.Equals("")){
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                }else{ 
                    EnumerableRowCollection<DataRow> filtered = vDatos.AsEnumerable()
                        .Where(r => r.Field<String>("codigo").Contains(vBusqueda.ToUpper()));

                    Boolean isNumeric = int.TryParse(vBusqueda, out int n);

                    if (isNumeric){
                        if (filtered.Count() == 0){
                            filtered = vDatos.AsEnumerable().Where(r =>
                                Convert.ToInt32(r["idUbicacion"]) == Convert.ToInt32(vBusqueda));
                        }
                    }

                    DataTable vDatosFiltrados = new DataTable();
                    vDatosFiltrados.Columns.Add("idUbicacion");
                    vDatosFiltrados.Columns.Add("tipo");
                    vDatosFiltrados.Columns.Add("codigo");
                    vDatosFiltrados.Columns.Add("direccion");
                    vDatosFiltrados.Columns.Add("estado");

                    foreach (DataRow item in filtered){
                        vDatosFiltrados.Rows.Add(
                            item["idUbicacion"].ToString(),
                            item["tipo"].ToString(),
                            item["codigo"].ToString(),
                            item["direccion"].ToString(),
                            item["estado"].ToString()
                            );
                    }

                    GVBusqueda.DataSource = vDatosFiltrados;
                    GVBusqueda.DataBind();
                    Session["INV_UBICACIONES"] = vDatosFiltrados;
                }

            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void GVBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e){

        }

        protected void BtnNuevo_Click(object sender, EventArgs e){
            limpiarModal();
            LbIdUbicacion.Text = "Crear Nueva Ubicacion";
            DivEstado.Visible = false;
            Session["INV_UBI_ID"] = null;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);
        }

        protected void BtnAceptar_Click(object sender, EventArgs e){
            try{
                validarDatos();
                String vQuery = "", vMensaje = "";
                int vInfo;
                DataTable vDatos = new DataTable();
                vQuery = "[STEISP_INVENTARIO_Ubicacaiones] {0}" +
                        "," + DDLTipo.SelectedValue +
                        "," + DDLMunicipio.SelectedValue +
                        ",'" + TxCodigo.Text.ToUpper() + "'" + 
                        ",'" + TxDireccion.Text + "'{1}";

                if (HttpContext.Current.Session["INV_UBI_ID"] == null){
                    vQuery = string.Format(vQuery, "3", "");
                    vInfo = vConexion.ejecutarSql(vQuery);
                    vMensaje = "Ubicación registrada con éxito.";
                }else{
                    vQuery = string.Format(vQuery, "4," + Session["INV_UBI_ID"].ToString(), "," + DDLEstado.SelectedValue);
                    vInfo = vConexion.ejecutarSql(vQuery);
                    vMensaje = "Ubicación actualizada con éxito.";
                }

                if (vInfo == 1){
                    Mensaje(vMensaje, WarningType.Info);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
                    cargarDatos();
                }
            }
            catch (Exception ex){
                LbAdvertencia.Text = ex.Message;
                DivMensaje.Visible = true;
            }
        }

        private void validarDatos(){
            if (DDLTipo.SelectedValue == "0")
                throw new Exception("Favor seleccione el tipo de ubicación.");
            if (DDLDepartamento.SelectedValue == "0")
                throw new Exception("Favor seleccione el departamento.");
            if (DDLMunicipio.SelectedValue == "0" || DDLMunicipio.SelectedValue == "")
                throw new Exception("Favor seleccione el municipio.");
            if (TxCodigo.Text == "" || TxCodigo.Text == string.Empty)
                throw new Exception("Favor ingrese el código.");
            if (TxDireccion.Text == "" || TxDireccion.Text == string.Empty)
                throw new Exception("Favor ingrese la dirección.");
        }

        void limpiarModal(){
            TxCodigo.Text = string.Empty;
            DDLMunicipio.SelectedValue = "0";
            DDLTipo.SelectedValue = "0";
            TxDireccion.Text = string.Empty;
            DivMensaje.Visible = false;
        }

        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e){
            try{
                DataTable vDatos = new DataTable();
                String vQuery = "";
                string vIdUbicacion = e.CommandArgument.ToString();
                
                if (e.CommandName == "EditarUbicacion") {
                    DivMensaje.Visible = false;
                    LbIdUbicacion.Text = "Editar Ubicacion " + vIdUbicacion;
                    Session["INV_UBI_ID"] = vIdUbicacion;
                    DivEstado.Visible = true;
                    vQuery = "[STEISP_INVENTARIO_Ubicacaiones] 2," + vIdUbicacion + "";
                    vDatos = vConexion.obtenerDataTable(vQuery);

                    for (int i = 0; i < vDatos.Rows.Count; i++){
                        TxCodigo.Text = vDatos.Rows[i]["codigo"].ToString();
                        TxDireccion.Text = vDatos.Rows[i]["direccion"].ToString();
                        DDLTipo.SelectedValue = vDatos.Rows[i]["idTipoUbicacion"].ToString();
                        DDLDepartamento.SelectedValue = vDatos.Rows[i]["idDepartamento"].ToString(); 
                        cargarMunicipios(DDLDepartamento.SelectedValue);
                        DDLMunicipio.SelectedValue = vDatos.Rows[i]["idMunicipio"].ToString();
                        string vEstado = Convert.ToBoolean(vDatos.Rows[i]["estado"].ToString()) == false ? "0" : "1";
                        DDLEstado.SelectedValue = vEstado ;
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }else if(e.CommandName == "EliminarUbicacion"){
                    LbTitulo.Text = "Eliminar Ubicación?";
                    LbMensaje.Text = "No podrá reversar los cambios.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ModalConfirmar();", true);
                }

            }catch (Exception Ex){
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        protected void BtnConfirmar_Click(object sender, EventArgs e){
            try{
                String vQuery = "";
                int vInfo = vConexion.ejecutarSql(vQuery);
                if (vInfo == 1){
                    Mensaje("El Artículo se ha eliminado con éxito.", WarningType.Success);
                    limpiarModal();
                }
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
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
    }
}