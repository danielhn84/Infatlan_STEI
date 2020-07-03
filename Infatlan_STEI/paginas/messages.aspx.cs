using Infatlan_STEI.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;

namespace Infatlan_STEI.paginas
{
    public partial class messages : System.Web.UI.Page
    {
        db vConexion = new db();
        protected void Page_Load(object sender, EventArgs e){
            if (!Page.IsPostBack){
                if (Convert.ToBoolean(Session["AUTH"])){
                    String vRes = Request.QueryString["ex"];
                    if (vRes == "1"){
                        MensajeBlock("Mensaje borrado exitosamente.", WarningType.Success);
                    }
                    cargarDatos();
                }else {
                    Response.Redirect("/login.aspx");
                }
            }
        }

        private void cargarDatos() {
            try{
                String vQuery = "[STEISP_Mensajes] 3,'" + Session["USUARIO"].ToString() +"'";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0){
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                    Session["MENSAJES"] = vDatos;
                }

                vQuery = "[STEISP_INVENTARIO_Generales] 14";
                vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0){
                    DDLAplicaciones.Items.Clear();
                    DDLAplicaciones.Items.Add(new ListItem { Value = "0", Text = "Seleccione" });
                    foreach (DataRow item in vDatos.Rows){
                        DDLAplicaciones.Items.Add(new ListItem { Value = item["idAplicacion"].ToString(), Text = item["nombre"].ToString() });
                    }
                }

                vQuery = "[STEISP_INVENTARIO_Generales] 13";
                vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0){
                    DDLDestino.Items.Clear();
                    DDLDestino.Items.Add(new ListItem { Value = "0", Text = "Seleccione" });
                    foreach (DataRow item in vDatos.Rows){
                        DDLDestino.Items.Add(new ListItem { Value = item["idUsuario"].ToString(), Text = item["nombre"].ToString() + " " + item["apellidos"].ToString() });
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

        protected void BtnEnviar_Click(object sender, EventArgs e){
            try{
                String vQuery = "[STEISP_Mensajes] 1" +
                    ",'" + DDLDestino.SelectedValue + "'" +
                    "," + DDLAplicaciones.SelectedValue +
                    ",'" + TxAsunto.Text + "'" +
                    ",'" + TxMensaje.Text + "'" +
                    ",0" +
                    ",'"+ Session["USUARIO"].ToString() + "'";
                int vInfo = vConexion.ejecutarSql(vQuery);

                if (vInfo == 1)
                    Mensaje("Mensaje enviado con éxito", WarningType.Success);
                else
                    Mensaje("Hubo un error, favor comunicarse con sistemas.", WarningType.Success);

                limpiarForm();

            }catch (Exception ex){
                MensajeBlock(ex.Message, WarningType.Danger);
            }
        }

        private void limpiarForm() {
            DDLDestino.SelectedValue = "0";
            DDLAplicaciones.SelectedValue = "0";
            TxAsunto.Text = string.Empty;
            TxMensaje.Text = string.Empty;
        }

        protected void GVBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e){
            try{
                string vIdArticulo = e.CommandArgument.ToString();
                if (e.CommandName == "BorrarMensaje"){
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openConfirmar();", true);
                    Session["MENSAJES_BORRAR"] = vIdArticulo;
                }
            }catch (Exception Ex){
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        protected void BtnConfirmar_Click(object sender, EventArgs e){
            try{
                String vQuery = "[STEISP_Mensajes] 2," + Session["MENSAJES_BORRAR"].ToString() + ", 1";
                int vInfo = vConexion.ejecutarSql(vQuery);
                if (vInfo == 1){
                    Mensaje("Mensaje borrado exitosamente.", WarningType.Success);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeConfirmar();", true);
                    Response.Redirect("/paginas/messages.aspx?ex=1");
                }

            }catch (Exception ex){

            }
        }
    }
}