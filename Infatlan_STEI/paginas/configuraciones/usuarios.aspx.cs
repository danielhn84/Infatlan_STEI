using Infatlan_STEI.classes;
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

namespace Infatlan_STEI.paginas.configuraciones
{
    public partial class usuarios : System.Web.UI.Page
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

        private void cargarDatos() {
            try{
                String vQuery = "[STEISP_Usuarios] 1,0";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0){
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                    Session["STEI_USUARIOS"] = vDatos;
                }
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        public void Mensaje(string vMensaje, WarningType type){
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        protected void GVBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e){
            try{
                GVBusqueda.PageIndex = e.NewPageIndex;
                GVBusqueda.DataSource = (DataTable)Session["STEI_USUARIOS"];
                GVBusqueda.DataBind();

            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e){
            try{
                DataTable vDatos = new DataTable();
                String vQuery = "";
                string vIdUsuario = e.CommandArgument.ToString();
                
                if (e.CommandName == "EditarUser"){
                    DivMensaje.Visible = false;
                    LbIdMarca.Text = "Editar Usuario " + vIdUsuario;
                    Session["STEI_USER_ACCION"] = vIdUsuario;
                    DivEstado.Visible = true;
                    vQuery = "[STEISP_Usuarios] 2," + vIdUsuario;
                    vDatos = vConexion.obtenerDataTable(vQuery);

                    for (int i = 0; i < vDatos.Rows.Count; i++){
                        TxUsuario.Text = vDatos.Rows[i]["idUsuario"].ToString();
                        TxNombres.Text = vDatos.Rows[i]["nombre"].ToString();
                        TxApellidos.Text = vDatos.Rows[i]["apellidos"].ToString();
                        TxTelefono.Text = vDatos.Rows[i]["telefono"].ToString();
                        TxCorreo.Text = vDatos.Rows[i]["correo"].ToString();
                        TxIdentidad.Text = vDatos.Rows[i]["identidad"].ToString();
                        DDLEstado.SelectedValue = vDatos.Rows[i]["estado"].ToString();
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void BtnNuevo_Click(object sender, EventArgs e){
            try{
                limpiarModal();
                LbIdMarca.Text = "Crear Nuevo Usuario";
                DivEstado.Visible = false;
                Session["STEI_USER_ACCION"] = null;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        void limpiarModal(){
            TxUsuario.Text = string.Empty;
            TxNombres.Text = string.Empty;
            TxApellidos.Text = string.Empty;
            TxIdentidad.Text = string.Empty;
            TxCorreo.Text = string.Empty;
            TxTelefono.Text = string.Empty;
            DDLDepartamento.Text = string.Empty;
            DivMensaje.Visible = false;
        }

        protected void TxBusqueda_TextChanged(object sender, EventArgs e){

        }

        protected void BtnAceptar_Click(object sender, EventArgs e){
            try{
                validarDatos();
                String vQuery = "", vMensaje = "";
                int vInfo;
                DataTable vDatos = new DataTable();
                vQuery = "STEISP_INVENTARIO_Marcas {0}" +
                        ",'" + TxNombres.Text.ToUpper() + "'";

                if (HttpContext.Current.Session["STEI_USER_ACCION"] == null){
                    vQuery = string.Format(vQuery, "3");
                    vInfo = vConexion.ejecutarSql(vQuery);
                    vMensaje = "Usuario registrada con éxito";
                }else{
                    vQuery = string.Format(vQuery, "4");
                    vInfo = vConexion.ejecutarSql(vQuery);
                    vMensaje = "Usuario actualizado con éxito";
                }

                if (vInfo == 1){
                    Mensaje(vMensaje, WarningType.Success);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "cerrarModal();", true);
                    cargarDatos();
                }
            }
            catch (Exception ex){
                LbAdvertencia.Text = ex.Message;
                DivMensaje.Visible = true;
            }
        }

        private void validarDatos(){
            if (TxUsuario.Text == "" || TxUsuario.Text == string.Empty)
                throw new Exception("Favor ingrese el usuario.");
            if (TxNombres.Text == "" || TxNombres.Text == string.Empty)
                throw new Exception("Favor ingrese el nombre del usuario.");
            if (TxApellidos.Text == "" || TxApellidos.Text == string.Empty)
                throw new Exception("Favor ingrese los apellidos.");
            if (TxIdentidad.Text == "" || TxIdentidad.Text == string.Empty)
                throw new Exception("Favor ingrese el número de identidad.");
            if (TxCorreo.Text == "" || TxCorreo.Text == string.Empty)
                throw new Exception("Favor ingrese el correo del usuario.");
            if (TxTelefono.Text == "" || TxTelefono.Text == string.Empty)
                throw new Exception("Favor ingrese el telefono.");
            if (DDLDepartamento.SelectedValue == "0")
                throw new Exception("Favor ingrese el telefono.");
        }
    }
}