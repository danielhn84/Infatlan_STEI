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

namespace Infatlan_STEI.paginas.reportes.ajustes
{
    public partial class cursos : System.Web.UI.Page
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
                String vQuery = "[STEISP_CUMPLIMIENTO_Ajustes] 7";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                if (vDatos.Rows.Count > 0){
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                    Session["CUMPL_CURSOS"] = vDatos;
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
                DataTable vDatos = (DataTable)Session["CUMPL_CURSOS"];
                if (vBusqueda.Equals("")){
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                }else{ 
                    EnumerableRowCollection<DataRow> filtered = vDatos.AsEnumerable()
                        .Where(r => r.Field<String>("nombre").Contains(vBusqueda.ToUpper()));

                    Boolean isNumeric = int.TryParse(vBusqueda, out int n);

                    if (isNumeric){
                        if (filtered.Count() == 0){
                            filtered = vDatos.AsEnumerable().Where(r =>
                                Convert.ToInt32(r["idCurso"]) == Convert.ToInt32(vBusqueda));
                        }
                    }

                    DataTable vDatosFiltrados = new DataTable();
                    vDatosFiltrados.Columns.Add("idCurso");
                    vDatosFiltrados.Columns.Add("nombre");
                    vDatosFiltrados.Columns.Add("estado");

                    foreach (DataRow item in filtered){
                        vDatosFiltrados.Rows.Add(
                            item["idCurso"].ToString(),
                            item["nombre"].ToString(),
                            item["estado"].ToString()
                        );
                    }

                    GVBusqueda.DataSource = vDatosFiltrados;
                    GVBusqueda.DataBind();
                    Session["CUMPL_CURSOS"] = vDatosFiltrados;
                }

            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void BtnNuevo_Click(object sender, EventArgs e){
            try{
                limpiarModal();
                LbTituloModal.Text = "Crear Nuevo Curso";
                DivEstado.Visible = false;
                Session["CUMPL_CURSOS_ID"] = null;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        void limpiarModal(){
            TxNombre.Text = string.Empty;
            DDLEstado.SelectedValue = "1";
            DivMensaje.Visible = false;
        }

        protected void BtnAceptar_Click(object sender, EventArgs e){
            try{
                validarDatos();
                String vQuery = "", vMensaje = "";
                int vInfo;
                DataTable vDatos = new DataTable();
                String vUser = Session["USUARIO"].ToString();

                vQuery = "[STEISP_CUMPLIMIENTO_Ajustes] {0}" +
                        ",'" + TxNombre.Text.ToString().ToUpper() + "'" +
                        "," + DDLEstado.SelectedValue +
                        ",'" + Session["USUARIO"].ToString() + "'";

                if (HttpContext.Current.Session["CUMPL_CURSOS_ID"] == null){
                    vQuery = string.Format(vQuery, "9");
                    vInfo = vConexion.ejecutarSql(vQuery);
                    vMensaje = "Curso registrado con éxito.";
                }else{
                    vQuery = string.Format(vQuery, "10," + Session["CUMPL_CURSOS_ID"].ToString());
                    vInfo = vConexion.ejecutarSql(vQuery);
                    vMensaje = "Curso actualizado con éxito.";
                }

                //ACTUALIZAR TODOS LOS QUE ESTEN ASIGNADOS AL CURSO
                if (DDLEstado.SelectedValue == "0"){
                    vQuery = "[STEISP_CUMPLIMIENTO_Evaluaciones] 7" +
                        "," + Session["CUMPL_CURSOS_ID"].ToString() +
                        ",2,'" + Session["USUARIO"].ToString() +"'";
                    int vInfo2 = vConexion.ejecutarSql(vQuery);
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
            if (TxNombre.Text == string.Empty || TxNombre.Text == "")
                throw new Exception("Favor ingrese el nombre del curso.");
        }

        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e){
            try{
                string vIdCurso = e.CommandArgument.ToString();
                if (e.CommandName == "EditarCurso"){
                    DivMensaje.Visible = false;
                    LbTituloModal.Text = "Editar Curso " + vIdCurso;
                    Session["CUMPL_CURSOS_ID"] = vIdCurso;
                    DivEstado.Visible = true;

                    String vQuery = "[STEISP_CUMPLIMIENTO_Ajustes] 8," + vIdCurso + "";
                    DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                    for (int i = 0; i < vDatos.Rows.Count; i++){
                        TxNombre.Text = vDatos.Rows[i]["nombre"].ToString();
                        DDLEstado.SelectedValue = vDatos.Rows[i]["estado"].ToString();
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void GVBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e){
            try{
                GVBusqueda.PageIndex = e.NewPageIndex;
                GVBusqueda.DataSource = (DataTable)Session["CUMPL_MOTIVOS"];
                GVBusqueda.DataBind();
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
    }
}