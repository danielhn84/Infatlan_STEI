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

namespace Infatlan_STEI.paginas.reportes
{
    public partial class cursosEmpleados : System.Web.UI.Page
    {
        db vConexion = new db();
        protected void Page_Load(object sender, EventArgs e){
            if (!Page.IsPostBack){
                if (Convert.ToBoolean(Session["AUTH"])){
                    cargarDatos();
                }else {
                    Response.Redirect("/login.aspx");
                }
            }
        }

        private void cargarDatos() {
            try{
                String vQuery = "[STEISP_CUMPLIMIENTO_Evaluaciones] 1";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                if (vDatos.Rows.Count > 0){
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                    Session["CUMPL_EVALUACIONES"] = vDatos;
                }

                //CURSOS
                vQuery = "[STEISP_CUMPLIMIENTO_Cursos] 1";
                vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0){
                    DDLCursos.Items.Clear();
                    DDLCursos.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    foreach (DataRow item in vDatos.Rows){
                        DDLCursos.Items.Add(new ListItem { Value = item["idCurso"].ToString(), Text = item["nombre"].ToString() });
                    }
                }

                //EMPLEADOS
                vQuery = "[STEISP_Usuarios] 1";
                vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0){
                    DDLEmpleado.Items.Clear();
                    DDLEmpleado.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    foreach (DataRow item in vDatos.Rows){
                        DDLEmpleado.Items.Add(new ListItem { Value = item["idUsuario"].ToString(), Text = item["nombre"].ToString() + " " + item["apellidos"].ToString() });
                    }
                }
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        public void Mensaje(string vMensaje, WarningType type){
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        protected void BtnAceptar_Click(object sender, EventArgs e){

        }

        protected void TxBusqueda_TextChanged(object sender, EventArgs e){

        }

        protected void BtnNuevo_Click(object sender, EventArgs e){
            try{
                limpiarModal();
                LbTituloModal.Text = "Crear Nueva Asignación";
                Session["CUMPL_EVALUACION_ID"] = null;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        void limpiarModal(){
            DDLCursos.SelectedValue = "0";
            DDLEmpleado.SelectedValue = "0";
            Session["CUMPL_EVALUACION_ASIGNAR"] = null;
            DivMensaje.Visible = false;
        }

        protected void BtnCargar_Click(object sender, EventArgs e){

        }

        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e){
            try{
                string vIdEvaluacion = e.CommandArgument.ToString();
                if (e.CommandName == "EditarEvaluacion"){
                    DivMensaje.Visible = false;
                    LbTituloModal.Text = "Editar Evaluación " + vIdEvaluacion;
                    Session["CUMPL_EVALUACION_ID"] = vIdEvaluacion;

                    String vQuery = "[STEISP_CUMPLIMIENTO_Evaluaciones] 3," + vIdEvaluacion + "";
                    DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                    for (int i = 0; i < vDatos.Rows.Count; i++){
                        DDLCursos.SelectedValue = vDatos.Rows[i]["idCurso"].ToString();
                        DDLEmpleado.SelectedValue = vDatos.Rows[i]["idUsuario"].ToString();
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void GVBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e){

        }

        protected void GvAsignar_RowCommand(object sender, GridViewCommandEventArgs e){
            try{
                DataTable vDatos = (DataTable)Session["CUMPL_EVALUACION_ASIGNAR"];
                if (e.CommandName == "BorrarAsignacion"){
                    String vID = e.CommandArgument.ToString();
                    if (Session["CUMPL_EVALUACION_ASIGNAR"] != null){
                        DataRow[] result = vDatos.Select("id = '" + vID + "'");
                        foreach (DataRow row in result){
                            if (row["id"].ToString().Contains(vID))
                                vDatos.Rows.Remove(row);
                        }
                    }
                }
                GvAsignar.DataSource = vDatos;
                GvAsignar.DataBind();
                Session["ATM_MATERIALES_VERIF"] = vDatos;
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void GvAsignar_PageIndexChanging(object sender, GridViewPageEventArgs e){

        }

        protected void BtnAsignar_Click(object sender, EventArgs e){
            try{
                DataTable vNewDatos = new DataTable();
                DataTable vData = (DataTable)Session["CUMPL_EVALUACION_ASIGNAR"];
                
                vNewDatos.Columns.Add("id");
                vNewDatos.Columns.Add("idCurso");
                vNewDatos.Columns.Add("curso");
                vNewDatos.Columns.Add("idUsuario");
                vNewDatos.Columns.Add("nombre");

                if (vData == null)
                    vData = vNewDatos.Clone();

                if (vData.Rows.Count > 0){
                    Boolean vFlag = true;
                    for (int i = 0; i < vData.Rows.Count; i++){
                        if (vData.Rows[i]["idUsuario"].ToString() == DDLEmpleado.SelectedValue && vData.Rows[i]["idCurso"].ToString() == DDLCursos.SelectedValue) { 
                            vFlag = false;
                            break;
                        }
                    }
                    
                    if (vFlag)
                        vData.Rows.Add(vData.Rows.Count + 1, DDLCursos.SelectedValue, DDLCursos.SelectedItem, DDLEmpleado.SelectedValue, DDLEmpleado.SelectedItem);
                }else
                    vData.Rows.Add(vData.Rows.Count + 1, DDLCursos.SelectedValue, DDLCursos.SelectedItem, DDLEmpleado.SelectedValue, DDLEmpleado.SelectedItem);

                if (vData.Rows.Count > 0){
                    Session["CUMPL_EVALUACION_ASIGNAR"] = vData;
                    GvAsignar.DataSource = vData;
                    GvAsignar.DataBind();
                }

            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
    }
}