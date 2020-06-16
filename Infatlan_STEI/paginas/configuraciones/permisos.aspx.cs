using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Infatlan_STEI.classes;

namespace Infatlan_STEI.paginas.configuraciones
{
    public partial class permisos : System.Web.UI.Page
    {
        db vConexion = new db();
        protected void Page_Load(object sender, EventArgs e){
            if (!Page.IsPostBack){
                if (Convert.ToBoolean(Session["AUTH"])){
                    cargarDatos();
                }
            }
        }

        private void cargarDatos() {
            try{
                String vQuery = "[STEISP_INVENTARIO_Generales] 14";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0){
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                    Session["STEI_PERMISOS"] = vDatos;
                }

                vQuery = "[STEISP_INVENTARIO_Generales] 13";
                vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0){
                    DDLUsuarios.Items.Clear();
                    DDLUsuarios.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    foreach (DataRow item in vDatos.Rows){
                        DDLUsuarios.Items.Add(new ListItem { Value = item["idUsuario"].ToString(), Text = item["nombre"].ToString() + " " + item["apellidos"].ToString() });
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

        protected void BtnAceptar_Click(object sender, EventArgs e) {
            try{
                validarDatos();
                DataTable vDatos = (DataTable)Session["STEI_PERMISOS"];
                String vQuery = "[STEISP_Permisos] 3,'" + DDLUsuarios.SelectedValue + "'";
                int vInfo = vConexion.ejecutarSql(vQuery);

                if (vInfo < 1){ //CREAR
                    int vCuenta = 0;

                    foreach (GridViewRow row in GVBusqueda.Rows){
                        CheckBox CBConsulta = row.Cells[2].FindControl("CBxConsulta") as CheckBox;
                        CheckBox CBCrear = row.Cells[2].FindControl("CBxCrear") as CheckBox;
                        CheckBox CBEditar = row.Cells[2].FindControl("CBxEditar") as CheckBox;
                        CheckBox CBBorrar = row.Cells[2].FindControl("CBxBorrar") as CheckBox;
                        int ind= row.RowIndex;

                        vQuery = "[STEISP_Permisos] 1,'" + DDLUsuarios.SelectedValue + "'" +
                        "," + vDatos.Rows[row.RowIndex]["idAplicacion"].ToString() +
                        ",'" + Session["USUARIO"].ToString() + "'" +
                        "," + CBConsulta.Checked +
                        "," + CBCrear.Checked +
                        "," + CBEditar.Checked +
                        "," + CBBorrar.Checked;
                        vInfo = vConexion.ejecutarSql(vQuery);
                        vCuenta++;
                    }
                    if (vCuenta == vDatos.Rows.Count){
                        cargarDatos();
                        Mensaje("Permisos ingresados con éxito.", WarningType.Success);
                    }
                }else{ //ACTUALIZAR
                    vQuery = "";
                }
            }catch (Exception ex){
                MensajeBlock(ex.Message, WarningType.Danger);
            }
        }

        private void validarDatos(){
            if (DDLUsuarios.SelectedValue == "0")
                throw new Exception("Favor seleccione el usuario");
        }

        protected void DDLUsuarios_SelectedIndexChanged(object sender, EventArgs e){
            try{
                GVBusqueda.DataBind();
            }catch (Exception ex){
                
            }
        }

        protected void GVBusqueda_RowDataBound(object sender, GridViewRowEventArgs e){
            try{
                if (e.Row.RowType == DataControlRowType.DataRow){
                    /*
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    String asd = drv.Row[2].ToString();

                    TableCell vCel = new TableCell();
                    if (drv["CBxConsulta"].ToString().Equals("True"))
                        e.Row.Cells[2].Attributes.Add("Checked", "true"); // CssStyle.Value = "Checked : true;";

                    String vQuery = "[STEISP_Permisos] 3,'" + Session["USUARIO"].ToString() + "'";
                    
                    DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                    if (vDatos.Rows.Count > 0){
                        if (vDatos.Rows[0]["consulta"].ToString() == "True")
                            GVBusqueda.Rows[2].FindControl("CBxConsulta") = true;
                        if (vDatos.Rows[1]["consulta"].ToString() == "True")
                            LIAgencias.Visible = true;
                        if (vDatos.Rows[2]["consulta"].ToString() == "True")
                            LIATM.Visible = true;
                        if (vDatos.Rows[3]["consulta"].ToString() == "True")
                            LICableado.Visible = true;
                    }
                    */
                }
            }catch (Exception Ex){
                throw new Exception(Ex.Message);
            }
        }
    }
}