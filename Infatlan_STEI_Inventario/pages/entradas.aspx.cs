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
                String vQuery = "[STEISP_INVENTARIO_Principal] 1";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0){
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                    Session["INV_ENTRADAS"] = vDatos;
                }

                //PROVEEDOR
                vQuery = "[STEISP_INVENTARIO_Proveedores] 1";
                vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0){
                    DDLProveedor.Items.Clear();
                    DDLProveedor.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    foreach (DataRow item in vDatos.Rows){
                        DDLProveedor.Items.Add(new ListItem { Value = item["idProveedor"].ToString(), Text = item["nombre"].ToString() });
                    }
                }

                //STOCK
                vQuery = "[STEISP_INVENTARIO_Stock] 1";
                vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0){
                    DDLProducto.Items.Clear();
                    DDLProducto.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    foreach (DataRow item in vDatos.Rows){
                        DDLProducto.Items.Add(new ListItem { Value = item["idStock"].ToString(), Text = item["TipoStock"].ToString() + " - " + item["modelo"].ToString() });
                    }
                }

                //UBICACIONES
                vQuery = "[STEISP_INVENTARIO_Ubicacaiones] 1";
                vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0){
                    DDLUbicacion.Items.Clear();
                    DDLUbicacion.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    foreach (DataRow item in vDatos.Rows){
                        DDLUbicacion.Items.Add(new ListItem { Value = item["idUbicacion"].ToString(), Text = item["codigo"].ToString() });
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

                // TIPO UBICACION
                vQuery = "[STEISP_INVENTARIO_Generales] 3";
                vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0){
                    DDLTipoUbic.Items.Clear();
                    DDLTipoUbic.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    foreach (DataRow item in vDatos.Rows){
                        DDLTipoUbic.Items.Add(new ListItem { Value = item["idTipoUbicacion"].ToString(), Text = item["nombre"].ToString() });
                    }
                }
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        public void Mensaje(string vMensaje, WarningType type){
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        protected void BtnAddProveedor_Click(object sender, EventArgs e){
            limpiarModalProv();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalP();", true);
        }

        private void limpiarModalProv() { 
        
        }

        protected void BtnAddArticulo_Click(object sender, EventArgs e){
            limpiarModalArt();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalA();", true);
        }

        private void limpiarModalArt(){

        }

        protected void BtnAddUbicacion_Click(object sender, EventArgs e){
            limpiarModalUbic();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalU();", true);
        }

        private void limpiarModalUbic(){

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