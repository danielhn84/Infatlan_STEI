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
using Excel;

namespace Infatlan_STEI.paginas.reportes
{
    public partial class cursosEmpleados : System.Web.UI.Page
    {
        db vConexion = new db();
        Security vSecurity = new Security();
        protected void Page_Load(object sender, EventArgs e){
            if (!Page.IsPostBack){
                if (Convert.ToBoolean(Session["AUTH"])){
                    if (vSecurity.ObtenerPermiso(Session["USUARIO"].ToString(), 5).Creacion)
                        DivCrear.Visible = true;


                    //CURSOS
                    String vQuery = "[STEISP_CUMPLIMIENTO_Cursos] 1";
                    DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                    if (vDatos.Rows.Count > 0){
                        if (DDLSortCurso.Items.Count < 1){
                            DDLSortCurso.Items.Add(new ListItem { Value = "0", Text = "Todos los cursos." });
                            foreach (DataRow item in vDatos.Rows){
                                DDLSortCurso.Items.Add(new ListItem { Value = item["idCurso"].ToString(), Text = item["nombre"].ToString() });
                            }
                        }
                    }

                    cargarDatos();
                }else {
                    Response.Redirect("/login.aspx");
                }
            }
        }

        private void cargarDatos() {
            try{
                String vQuery = "";

                if (DDLSortCurso.SelectedValue == "0")
                    vQuery = "[STEISP_CUMPLIMIENTO_Evaluaciones] 1";
                else
                    vQuery = "[STEISP_CUMPLIMIENTO_Evaluaciones] 8," + DDLSortCurso.SelectedValue;
                
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                if (vDatos.Rows.Count > 0){
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                    if (vSecurity.ObtenerPermiso(Session["USUARIO"].ToString(), 5).Edicion){
                        foreach (GridViewRow item in GVBusqueda.Rows){
                            LinkButton LbEdit = item.FindControl("BtnEditar") as LinkButton;
                            LbEdit.Visible = true;
                        }
                    }
                    if (vSecurity.ObtenerPermiso(Session["USUARIO"].ToString(), 5).Borrado){
                        foreach (GridViewRow item in GVBusqueda.Rows){
                            LinkButton LbDelete = item.FindControl("BtnBorrar") as LinkButton;
                            LbDelete.Visible = true;
                        }
                    }
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
            try{
                if (HttpContext.Current.Session["CUMPL_EVALUACION_ASIGNAR"] == null)
                    throw new Exception("Favor agregue empleados.");
                
                DataTable vData = (DataTable)Session["CUMPL_EVALUACION_ASIGNAR"];
                if (vData.Rows.Count > 0){
                    String vQuery = "";
                    int vInfo = 0;
                    for (int i = 0; i < vData.Rows.Count; i++){
                        vQuery = "[STEISP_CUMPLIMIENTO_Evaluaciones] 3" +
                                "," + vData.Rows[i]["idCurso"].ToString() +
                                ",'" + vData.Rows[i]["idUsuario"].ToString() + "'" +
                                ",0,0,'" + Session["USUARIO"].ToString() + "'";
                        vInfo = vConexion.ejecutarSql(vQuery);
                    }

                    if (vInfo == 1){
                        Mensaje("Curso asignado con éxito.", WarningType.Success);
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "cerrarModal();", true);
                        cargarDatos();
                    }
                }else
                    throw new Exception("Favor agregue empleados.");
                
            }catch (Exception ex){
                LbAdvertencia.Text = ex.Message;
                DivMensaje.Visible = true;
            }
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
            GvAsignar.DataSource = null;
            GvAsignar.DataBind();
            DivMensaje.Visible = false;
        }

        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e){
            try{
                string vIdEvaluacion = e.CommandArgument.ToString();
                Session["CUMPL_EVALUACION_ID"] = vIdEvaluacion;
                if (e.CommandName == "EditarEvaluacion") {
                    DivMensajeNota.Visible = false;
                    String vQuery = "[STEISP_CUMPLIMIENTO_Evaluaciones] 2," + vIdEvaluacion;
                    DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                    if (vDatos.Rows.Count > 0){
                        for (int i = 0; i < vDatos.Rows.Count; i++) {
                            TxCurso.Text = vDatos.Rows[i]["nombre"].ToString();
                            TxEmpleado.Text = vDatos.Rows[i]["empleado"].ToString();
                            TxNota.Text = vDatos.Rows[i]["nota"].ToString();
                        }
                        LbTituloNotas.Text = "Evaluar a " + vDatos.Rows[0]["empleado"].ToString();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalNotas();", true);
                    }else
                        Mensaje("El Empleado ya fue evaluado.",WarningType.Warning);
                }else if (e.CommandName == "borrarAsignacion") {
                    LbConfirmacion.Text = "Seguro que desea borrar la asignación?";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openConfirmacion();", true);
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
                Session["CUMPL_EVALUACION_ASIGNAR"] = vDatos;
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void GvAsignar_PageIndexChanging(object sender, GridViewPageEventArgs e){
            try{
                GvAsignar.PageIndex = e.NewPageIndex;
                GvAsignar.DataSource = (DataTable)Session["CUMPL_EVALUACION_ASIGNAR"];
                GvAsignar.DataBind();
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
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

                Boolean vFlag = true;
                if (vData.Rows.Count > 0){
                    for (int i = 0; i < vData.Rows.Count; i++){
                        if (vData.Rows[i]["idUsuario"].ToString() == DDLEmpleado.SelectedValue && vData.Rows[i]["idCurso"].ToString() == DDLCursos.SelectedValue) { 
                            vFlag = false;
                            break;
                        }
                    }
                    vFlag = vFlag ? verificarAsignacion(vFlag) : vFlag;
                    if (vFlag)
                        vData.Rows.Add(vData.Rows.Count + 1, DDLCursos.SelectedValue, DDLCursos.SelectedItem, DDLEmpleado.SelectedValue, DDLEmpleado.SelectedItem);
                }else{
                    vFlag = verificarAsignacion(vFlag);
                    if (vFlag)
                        vData.Rows.Add(vData.Rows.Count + 1, DDLCursos.SelectedValue, DDLCursos.SelectedItem, DDLEmpleado.SelectedValue, DDLEmpleado.SelectedItem);
                }

                if (vData.Rows.Count > 0 && vFlag){
                    Session["CUMPL_EVALUACION_ASIGNAR"] = vData;
                    GvAsignar.DataSource = vData;
                    GvAsignar.DataBind();
                }

            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        private Boolean verificarAsignacion(Boolean vFlag) {
            try{
                String vQuery = "[STEISP_CUMPLIMIENTO_Evaluaciones] 5,'" + DDLEmpleado.SelectedValue + "'," + DDLCursos.SelectedValue;
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                if (vDatos.Rows.Count > 0 && vDatos.Rows[0][0].ToString() == "1"){
                    vFlag = false;
                }
            }catch (Exception ex){

            }
            return vFlag;
        }

        protected void BtnEvaluar_Click(object sender, EventArgs e){
            try{
                if (TxNota.Text == string.Empty || TxNota.Text == "")
                    throw new Exception("Favor ingrese la nota.");
                

                String vQuery = "[STEISP_CUMPLIMIENTO_Evaluaciones] 4" + 
                    "," + Session["CUMPL_EVALUACION_ID"].ToString() + 
                    "," + TxNota.Text + 
                    ",1,'" + Session["USUARIO"].ToString() + "'";
                int vInfo = vConexion.ejecutarSql(vQuery);
                if (vInfo == 1){
                    Mensaje("Evaluación realizada exitosamente.", WarningType.Success);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "cerrarModalNotas();", true);
                    cargarDatos();
                }
            }catch (Exception ex){
                LbAdvertenciaNota.Text = ex.Message;
                DivMensajeNota.Visible = true;
            }
        }

        protected void BtnCornfirmar_Click(object sender, EventArgs e){
            try{
                String vQuery = "[STEISP_CUMPLIMIENTO_Evaluaciones] 6" + 
                    "," + Session["CUMPL_EVALUACION_ID"].ToString() + 
                    ",3,'" + Session["USUARIO"].ToString() + "'";
                int vInfo = vConexion.ejecutarSql(vQuery);
                if (vInfo == 1){
                    Mensaje("Asignación borrada con éxito.", WarningType.Success);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "cerrarConfirmacion();", true);
                    cargarDatos();
                }
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void DDLSortCurso_SelectedIndexChanged(object sender, EventArgs e){
            try{
                cargarDatos();
            }catch (Exception ex){
                
            }
        }

        protected void BtnInsertar_Click(object sender, EventArgs e){
            String archivoLog = string.Format("{0}_{1}", Convert.ToString(Session["USUARIO"]), DateTime.Now.ToString("yyyyMMddHHmmss"));
            try{
                String vDireccionCarga = ConfigurationManager.AppSettings["RUTA_SERVER"].ToString();
                if (FUEvaluar.HasFile){
                    String vNombreArchivo = FUEvaluar.FileName;
                    vDireccionCarga += "/" + archivoLog + "_" + vNombreArchivo;

                    FUEvaluar.SaveAs(vDireccionCarga);
                    String vTipoProceso = "EVALUACIONES";
                    Boolean vCargado = false;
                    int vSuccess = 0, vError = 0;
                    if (File.Exists(vDireccionCarga))
                        vCargado = cargarArchivo(vDireccionCarga, ref vSuccess, ref vError, Convert.ToString(Session["USUARIO"]), vTipoProceso);

                    if (vCargado) {
                        Mensaje("Archivo cargado con éxito!", WarningType.Success);
                    }
                }else{
                    Response.Redirect("/reportes/cursosEmpleados.aspx?ex=1");
                }
            }catch (Exception Ex){
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        public Boolean cargarArchivo(String DireccionCarga, ref int vSuccess, ref int vError, String vUsuario, String TipoProceso){
            Boolean vResultado = false;
            try{
                FileStream stream = File.Open(DireccionCarga, FileMode.Open, FileAccess.Read);
                IExcelDataReader excelReader;
                if (DireccionCarga.Contains("xlsx"))
                    excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);   //2007
                else
                    excelReader = ExcelReaderFactory.CreateBinaryReader(stream);    //97-2003

                excelReader.IsFirstRowAsColumnNames = true;
                DataSet vDatosExcel = excelReader.AsDataSet();
                excelReader.Close();

                DataSet vDatosVerificacion = vDatosExcel.Copy();
                for (int i = 0; i < vDatosVerificacion.Tables[0].Rows.Count; i++){
                    if (verificarRow(vDatosVerificacion.Tables[0].Rows[i]))
                        vDatosExcel.Tables[0].Rows[i].Delete();
                }
                vDatosExcel.Tables[0].AcceptChanges();
                vResultado = procesarArchivo(vDatosExcel, ref vSuccess, DireccionCarga, TipoProceso, vUsuario);

            }catch (Exception Ex){
                throw new Exception(Ex.ToString());
            }
            return vResultado;
        }

        private bool verificarRow(DataRow dr){
            int contador = 0;
            foreach (var value in dr.ItemArray){
                if (value.ToString() != ""){
                    contador++;
                }
            }

            if (contador > 0)
                return false;
            else
                return true;
        }

        public Boolean procesarArchivo(DataSet vArchivo, ref int vSuccess, string DireccionCarga, string TipoProceso, string vUsuario){
            Boolean vRes = false;
            try{
                if (vArchivo.Tables[0].Rows.Count > 0){
                    DataTable vDatos = vArchivo.Tables[0];
                    string vQuery = "";
                    Boolean par1 = false, par2 = false, par3 = false;

                    // EVALUACIONES
                    if (TipoProceso == "EVALUACIONES"){
                        foreach (DataColumn item in vDatos.Columns){
                            if (item.ColumnName.ToString() == "ID_CURSO")
                                par1 = true;
                            if (item.ColumnName.ToString() == "ID_USUARIO")
                                par2 = true;
                            if (item.ColumnName.ToString() == "NOTA")
                                par3 = true;
                        }

                        if (par1 && par2 && par3){
                            for (int i = 0; i < vDatos.Rows.Count; i++){
                                Int32 vInfo;
                                vQuery = "[STEISP_CUMPLIMIENTO_Evaluaciones] 3" +
                                "," + vDatos.Rows[i]["ID_CURSO"].ToString() +
                                ",'" + vDatos.Rows[i]["ID_USUARIO"].ToString() + "'" +
                                ",1," + vDatos.Rows[i]["NOTA"].ToString() +
                                ",'" + vUsuario + "'";
                                vInfo = vConexion.ejecutarSql(vQuery);
                                if (vInfo == 1){
                                    vRes = true;
                                }
                            }
                        }
                    }
                }else
                    throw new Exception("No contiene ninguna hoja de excel.");
            }catch (Exception){
                throw;
            }
            return vRes;
        }
    }
}