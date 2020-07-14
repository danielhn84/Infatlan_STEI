using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infatlan_STEI.classes;

namespace Infatlan_STEI.paginas.reportes
{
    public partial class metasCumplimiento : System.Web.UI.Page
    {
        db vConexion = new db();
        GenerarXML vMaestro = new GenerarXML();
        protected void Page_Load(object sender, EventArgs e){
            if (!Page.IsPostBack){
                if (Convert.ToBoolean(Session["AUTH"])){
                    cargarData();
                }else{
                    Response.Redirect("/login.aspx");
                }
            }
        }

        private void cargarData(){
            try{
                String vFecha = "";

                if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
                    vFecha = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");
                else if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday) 
                    vFecha = DateTime.Now.AddDays(-2).ToString("dd/MM/yyyy");
                else if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
                    vFecha = DateTime.Now.AddDays(-3).ToString("dd/MM/yyyy");
                else
                    vFecha = DateTime.Now.ToString("dd/MM/yyyy");

                String vAssignedGroup = "Infa-tgu";
                cargarCalls(vFecha, vAssignedGroup);
                cargarMediosPago(vFecha, vAssignedGroup);
                cargarKPI(vFecha, vAssignedGroup);
                cargarRuptura(vFecha, vAssignedGroup);
                cargarOSER(vFecha, vAssignedGroup);
                cargarRendimiento(vFecha, vAssignedGroup);
            }catch (Exception Ex){
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        public void Mensaje(string vMensaje, WarningType type){
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        private void cargarCalls(String vFecha, String vAssignedGroup) {

            String vColor;
            String vPorcentaje = "9";
            if (Convert.ToInt32(vPorcentaje) < 11)
                vColor = "css-bar-danger";
            else if (Convert.ToInt32(vPorcentaje) < 50)
                vColor = "css-bar-warning";
            else 
                vColor = "css-bar-success";

            String vConstruccion = "<div data-label = '" + vPorcentaje + "%' class='css-bar css-bar-" + vPorcentaje + " css-bar-lg " + vColor + "'></div>";
            LitCall.Text = vConstruccion;
        }
        
        private void cargarMediosPago(String vFecha, String vAssignedGroup) {
            String vQuery = "[STEISP_CUMPLIMIENTO_Generales] 4,'" + vAssignedGroup + "','" + vFecha + "'";
            DataSet vDSMedios = vConexion.obtenerDataSetSA(vQuery);

            int vCumplidas = Convert.ToInt32(vDSMedios.Tables[0].Rows[0]["Cumplimiento"].ToString());
            int vIncumplidas = Convert.ToInt32(vDSMedios.Tables[1].Rows[0]["Incumplimiento"].ToString());
            Decimal vPorcentaje = obtenerPorcentaje(vCumplidas, vIncumplidas);

            TxATMCumplimiento.Text = vCumplidas.ToString();
            TxATMCumplimientoNo.Text = vIncumplidas.ToString();
            TxATMPorcentaje.Text = vPorcentaje.ToString();
            LitATM.Text = "<div class='chart easy-pie-chart-4' data-percent='" + vPorcentaje.ToString() + "'><span class='percent'></span></div>";

            vQuery = "[STEISP_CUMPLIMIENTO_Generales] 5,'" + vAssignedGroup + "','" + vFecha + "'";
            vDSMedios = vConexion.obtenerDataSetSA(vQuery);

            vCumplidas = Convert.ToInt32(vDSMedios.Tables[0].Rows[0]["Cumplimiento"].ToString());
            vIncumplidas = Convert.ToInt32(vDSMedios.Tables[1].Rows[0]["Incumplimiento"].ToString());
            vPorcentaje = obtenerPorcentaje(vCumplidas, vIncumplidas);

            TxABACumplimiento.Text = vCumplidas.ToString();
            TxABACumplimientoNo.Text = vIncumplidas.ToString();
            TxABAPorcentaje.Text = vPorcentaje.ToString();
            LitABA.Text = "<div class='chart easy-pie-chart-4' data-percent='" + vPorcentaje.ToString() + "'><span class='percent'></span></div>";

            vQuery = "[STEISP_CUMPLIMIENTO_Generales] 6,'" + vAssignedGroup + "','" + vFecha + "'";
            vDSMedios = vConexion.obtenerDataSetSA(vQuery);

            vCumplidas = Convert.ToInt32(vDSMedios.Tables[0].Rows[0]["Cumplimiento"].ToString());
            vIncumplidas = Convert.ToInt32(vDSMedios.Tables[1].Rows[0]["Incumplimiento"].ToString());
            vPorcentaje = obtenerPorcentaje(vCumplidas, vIncumplidas);

            TxCajaCumplidas.Text = vCumplidas.ToString();
            TxCajaCumplidasNo.Text = vIncumplidas.ToString();
            TxCajaPorcentaje.Text = vPorcentaje.ToString();
            LitCaja.Text = "<div class='chart easy-pie-chart-4' data-percent='" + vPorcentaje.ToString() + "'><span class='percent'></span></div>";
        }
        
        private void cargarKPI(String vFecha, String vAssignedGroup) { 
            String vQuery = "[STEISP_CUMPLIMIENTO_Generales] 2,'" + vAssignedGroup + "','" + vFecha + "'";
            DataSet vDSkpi = vConexion.obtenerDataSetSA(vQuery);

            int vCumplidas = Convert.ToInt32(vDSkpi.Tables[1].Rows[0]["Cumplidas"].ToString());
            int vIncumplidas = Convert.ToInt32(vDSkpi.Tables[0].Rows[0]["Incumplidas"].ToString());
            Decimal vPorcentaje = obtenerPorcentaje(vCumplidas, vIncumplidas);
            int vSuma = vCumplidas + vIncumplidas;

            if (vDSkpi.Tables[2].Rows.Count > 0){
                DivKPI.Visible = true;
                GvKPISolicitudes.DataSource = vDSkpi.Tables[2];
                GvKPISolicitudes.DataBind();
                Session["CUMPL_KPI"] = vDSkpi.Tables[2];
            }

            TxKPIPorcentaje.Text = vPorcentaje.ToString() + "%";
            TxKPITotal.Text = Convert.ToString(vSuma);
            TxKPICumplimiento.Text = vCumplidas.ToString();
            TxKPICumplimientoNo.Text = vIncumplidas.ToString();
        }

        private void cargarRuptura(String vFecha, String vAssignedGroup) {
            try{
                String vQuery = "[STEISP_CUMPLIMIENTO_Generales] 3,'" + vAssignedGroup + "','" + vFecha + "'";
                DataTable vData = vConexion.obtenerDataTableSA(vQuery);

                if (vData.Rows.Count > 0){
                    vData.Columns.Add("idRazon");
                    vData.Columns.Add("comentario");
                    LbResRuptura.Text = "";
                    DivRuptura.Visible = true;
                    GvRuptura.DataSource = vData;
                    GvRuptura.DataBind();
                    Session["CUMPL_RUPTURA"] = vData;
                }
            }catch (Exception ex){
                throw new Exception(ex.Message);
            }
            
        }  
        
        private void cargarOSER(String vFecha, String vAssignedGroup) { 
            String vQuery = "[STEISP_CUMPLIMIENTO_Generales] 1,'" + vAssignedGroup + "'";
            DataTable vDatos = vConexion.obtenerDataTableSA(vQuery);
            vDatos.Columns.Add("idMotivo");
            if (vDatos.Rows.Count > 0){
                vDatos.Columns.Add("idRazon");
                vDatos.Columns.Add("comentario");
                LbResOSER.Text = "";
                GvOSER.DataSource = vDatos;
                GvOSER.DataBind();
                Session["CUMPL_OSER"] = vDatos;
            }
        }

        private void cargarRendimiento(String vFecha, String vAssignedGroup) {
            try{
                String vQuery = "[STEISP_CUMPLIMIENTO_Rendimiento] 1";
                DataTable vData = vConexion.obtenerDataTable(vQuery);
                DataTable vDatosSA = new DataTable();
                //TAREAS
                for (int i = 0; i < vData.Rows.Count; i++) { 
                    String vQuery2 = "[STEISP_CUMPLIMIENTO_Generales] 8" +
                        ",'" + vAssignedGroup + "'" +
                        ",'" + vFecha + "'" +
                        "'" + vData.Rows[i]["sysAid"].ToString();
                    DataTable vRes = vConexion.obtenerDataTableSA(vQuery2);
                    if (vRes.Rows.Count > 0){
                        vData.Rows[i]["tareas"] = vRes.Rows[0][0].ToString();
                    }
                }

                //SATISFACCION
                for (int i = 0; i < vData.Rows.Count; i++) {
                    String vQuery2 = "[STEISP_CUMPLIMIENTO_Generales] 9" +
                        ",'" + vAssignedGroup + "'" +
                        ",'" + vFecha + "'" +
                        "'" + vData.Rows[i]["sysAid"].ToString();
                    DataTable vRes = vConexion.obtenerDataTableSA(vQuery2);
                    if (vRes.Rows.Count > 0){
                        vData.Rows[i]["satisfaccion"] = vRes.Rows[0]["total"].ToString();

                        //@#Porcentaje_Satisfaccion_Marlen=PorcentajeSatisfaccion;   
                        //if (@#Porcentaje_Satisfaccion_Marlen>=@#rango1 ){
                        //    @#Valor_Satisfaccion_Marlen=@#Porcentaje_Satisfaccion_Marlen;
                        //}else if (@#Porcentaje_Satisfaccion_Marlen>=@#rango2 && @#Porcentaje_Satisfaccion_Marlen<@#rango1 ){
                        //    @#Valor_Satisfaccion_Marlen=@#valor1;
   
                        //}else if (@#Porcentaje_Satisfaccion_Marlen>=@#rango3 && @#Porcentaje_Satisfaccion_Marlen<@#rango2){
                        //    @#Valor_Satisfaccion_Marlen=@#valor2;
       
                        //}else if (@#Porcentaje_Satisfaccion_Marlen<@#rango3) {
                        //    @#Valor_Satisfaccion_Marlen=@#valor3;
                        //};

                    }

                    //SatisfaccionDiaria =SumEncuesta /TotContestadas;
                    //PorcentajeSatisfaccion = round(((SatisfaccionDiaria / 5) * 100),2);
                }

                //CONOCIMIENTO Y EFICIENCIA
                obtenerEficiencia();
                for (int i = 0; i < vData.Rows.Count; i++){
                    Decimal vPromedio = Convert.ToDecimal(vData.Rows[i]["conocimiento"].ToString());
                    if (vPromedio >= 90)
                        vData.Rows[i]["totalConocimiento"] = vPromedio * 25/ 100;
                    else
                        vData.Rows[i]["totalConocimiento"] = vPromedio * (25 - (90 - vPromedio))/ 100;

                    //CONSULTA A SYSYAID
                    vQuery = "";
                    vDatosSA = vConexion.obtenerDataTableSA(vQuery);
                    if (vDatosSA.Rows.Count > 0){
                        vData.Rows[i]["eficiencia"] = vDatosSA.Rows[0]["eficiencia"].ToString();
                    }
                }

                if (vData.Rows.Count > 0){
                    GvRendimiento.DataSource = vData;
                    GvRendimiento.DataBind();
                    Session["CUMPL_RENDIMIENTO"] = vData;
                }

            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void GvOSER_RowDataBound(object sender, GridViewRowEventArgs e){
            if (e.Row.RowType == DataControlRowType.DataRow){
                var vDropDown = e.Row.Cells[2].FindControl("DDLRazonER") as DropDownList;
                if (vDropDown != null){
                    String vQuery = "[STEISP_CUMPLIMIENTO_Ajustes] 6, 5";
                    DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                    vDropDown.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    foreach (DataRow item in vDatos.Rows){
                        vDropDown.Items.Add(new ListItem { Value = item["idMotivo"].ToString(), Text = item["nombre"].ToString() });
                    }
                    if (HttpContext.Current.Session["CUMPL_OSER_PAGING"] != null ){
                        DataTable vData = (DataTable)Session["CUMPL_OSER"];
                        for (int i = 0; i < vData.Rows.Count; i++){
                            DataRowView drv = e.Row.DataItem as DataRowView;
                            String vCelda = drv["id"].ToString();

                            if (vData.Rows[i]["id"].ToString() == drv["id"].ToString()){
                                vDropDown.SelectedValue = vData.Rows[i]["idMotivo"].ToString();
                            }
                        }
                    }
                }
            }
        }

        protected void GvOSER_PageIndexChanging(object sender, GridViewPageEventArgs e){
            try{
                DataTable vData = (DataTable)Session["CUMPL_OSER"];
                int vCont = GvOSER.PageIndex != 0 ? GvOSER.PageIndex * 10 : 0;

                foreach (GridViewRow row in GvOSER.Rows){
                    DropDownList DDLRazon = (DropDownList)row.Cells[2].FindControl("DDLRazonER");
                    vData.Rows[vCont]["idMotivo"] = DDLRazon.SelectedValue;
                    vCont++;
                }
                Session["CUMPL_OSER_PAGING"] = true;

                GvOSER.PageIndex = e.NewPageIndex;
                GvOSER.DataSource = vData;
                Session["CUMPL_OSER"] = vData;
                GvOSER.DataBind();
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void GvKPISolicitudes_PageIndexChanging(object sender, GridViewPageEventArgs e){
            try{
                GvKPISolicitudes.PageIndex = e.NewPageIndex;
                GvKPISolicitudes.DataSource = (DataTable)Session["CUMPL_KPI"];
                GvKPISolicitudes.DataBind();
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        private Decimal obtenerPorcentaje(int vCumplidas, int vIncumplidas) {
            int vSuma = vCumplidas + vIncumplidas;

            Decimal vPorcentaje = 100;
            if (vSuma != 0){
                float vPrevio = float.Parse(vCumplidas.ToString()) / float.Parse(vSuma.ToString()) * 100;
                vPorcentaje = Convert.ToDecimal(Math.Round(vPrevio, 2));
            }

            return vPorcentaje;
        }

        protected void GvRuptura_RowDataBound(object sender, GridViewRowEventArgs e){
            if (e.Row.RowType == DataControlRowType.DataRow){
                var vDropDown = e.Row.Cells[3].FindControl("DDLRazonRuptura") as DropDownList;
                if (vDropDown != null){
                    String vQuery = "[STEISP_CUMPLIMIENTO_Ajustes] 6, 4";
                    DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                    vDropDown.Items.Add(new ListItem { Value = "0", Text = "Seleccione..." });
                    foreach (DataRow item in vDatos.Rows){
                        vDropDown.Items.Add(new ListItem { Value = item["idMotivo"].ToString(), Text = item["nombre"].ToString() });
                    }
                    if (HttpContext.Current.Session["CUMPL_RUPTURA_PAGING"] != null ){
                        DataTable vData = (DataTable)Session["CUMPL_RUPTURA"];
                        for (int i = 0; i < vData.Rows.Count; i++){
                            DataRowView drv = e.Row.DataItem as DataRowView;
                            String vCelda = drv["id"].ToString();

                            if (vData.Rows[i]["id"].ToString() == drv["id"].ToString()){
                                vDropDown.SelectedValue = vData.Rows[i]["idMotivo"].ToString();
                            }
                        }
                    }
                }
            }
        }

        protected void GvRuptura_PageIndexChanging(object sender, GridViewPageEventArgs e){
            try{
                DataTable vData = (DataTable)Session["CUMPL_RUPTURA"];
                int vCont = GvRuptura.PageIndex != 0 ? GvRuptura.PageIndex * 10 : 0;

                foreach (GridViewRow row in GvRuptura.Rows){
                    DropDownList DDLRazonRuptura = (DropDownList)row.Cells[2].FindControl("DDLRazonRuptura");
                    vData.Rows[vCont]["idMotivo"] = DDLRazonRuptura.SelectedValue;
                    vCont++;
                }
                Session["CUMPL_RUPTURA_PAGING"] = true;

                GvRuptura.PageIndex = e.NewPageIndex;
                GvRuptura.DataSource = vData;
                Session["CUMPL_RUPTURA"] = vData;
                GvRuptura.DataBind();
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void GvRendimiento_PageIndexChanging(object sender, GridViewPageEventArgs e){
            try{
                GvRendimiento.PageIndex = e.NewPageIndex;
                GvRendimiento.DataSource = (DataTable)Session["CUMPL_RENDIMIENTO"];
                GvRendimiento.DataBind();
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void BtnEnviar_Click(object sender, EventArgs e){
            try{
                Object[] vDatosReporte = new object[16];
                vDatosReporte[0] = TxCallAtendidas.Text;
                vDatosReporte[1] = TxCallAtendidasNo.Text;
                vDatosReporte[2] = TxCallObs.Text;
                vDatosReporte[3] = TxATMCumplimiento.Text;
                vDatosReporte[4] = TxATMCumplimientoNo.Text;
                vDatosReporte[5] = TxATMObs.Text;
                vDatosReporte[6] = TxABACumplimiento.Text;
                vDatosReporte[7] = TxABACumplimientoNo.Text;
                vDatosReporte[8] = TxABAObs.Text;
                vDatosReporte[9] = TxCajaCumplidas.Text;
                vDatosReporte[10] = TxCajaCumplidasNo.Text;
                vDatosReporte[11] = TxCajaObs.Text;
                vDatosReporte[12] = TxKPICumplimiento.Text;
                vDatosReporte[13] = TxKPICumplimientoNo.Text;
                vDatosReporte[14] = TxKPIObs.Text;
                vDatosReporte[15] = 1;
                vDatosReporte[16] = Session["USUARIO"].ToString();
                String vXML = vMaestro.ObtenerReporteCumplimiento(vDatosReporte);
                vXML = vXML.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
                String vQuery = "[STEISP_CUMPLIMIENTO_Reportes] 1" +
                    ",'" + vXML + "'";
                int vInfo = vConexion.ejecutarSql(vQuery);

                if (vInfo > 0){
                    DataTable vDatos = (DataTable)Session["CUMPL_KPI"];
                    if (vDatos.Rows.Count > 0){
                        insertarKPI(vDatos, vInfo);
                    }
                    
                    vDatos = (DataTable)Session["CUMPL_RUPTURA"];
                    if (vDatos.Rows.Count > 0){
                        insertarRupturas(vDatos, vInfo);
                    }

                    vDatos = (DataTable)Session["CUMPL_OSER"];
                    if (vDatos.Rows.Count > 0){
                        insertarOSER(vDatos, vInfo);
                    }

                    Response.Redirect("reportes/metasPendientes.aspx?ex=1");
                }
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);   
            }
        }

        private void insertarKPI(DataTable vDatos, int vId) {
            try{
                for (int i = 0; i < vDatos.Rows.Count; i++){
                    Object[] vDatosReporte = new object[16];
                    vDatosReporte[0] = vId.ToString();
                    vDatosReporte[1] = vDatos.Rows[i]["id"].ToString();
                    vDatosReporte[2] = vDatos.Rows[i]["tiempo"].ToString();
                    vDatosReporte[3] = vDatos.Rows[i]["problem_type"].ToString();
                    vDatosReporte[4] = vDatos.Rows[i]["problem_sub_type"].ToString();
                    vDatosReporte[5] = vDatos.Rows[i]["third_level_category"].ToString();

                    String vXML = vMaestro.ObtenerReporteCumplimientoKPI(vDatosReporte);
                    vXML = vXML.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
                    String vQuery = "[STEISP_CUMPLIMIENTO_Reportes] 5, 0, 0" +
                        ",'" + vXML + "'";
                    int vInfo = vConexion.ejecutarSql(vQuery);
                }
            }catch (Exception ex){

            }
        }

        private void insertarRupturas(DataTable vDatos, int vId) {
            try{
                //INSERTAR LA RAZON Y EL COMENTARIO
                


                for (int i = 0; i < vDatos.Rows.Count; i++){
                    Object[] vDatosReporte = new object[16];
                    vDatosReporte[0] = vId.ToString();
                    vDatosReporte[1] = vDatos.Rows[i]["id"].ToString();
                    vDatosReporte[2] = vDatos.Rows[i]["tiempo"].ToString();
                    vDatosReporte[3] = vDatos.Rows[i]["responsibility"].ToString();
                    vDatosReporte[4] = vDatos.Rows[i]["idRazon"].ToString();
                    vDatosReporte[5] = vDatos.Rows[i]["comentario"].ToString();

                    String vXML = vMaestro.ObtenerReporteCumplimientoRupturas(vDatosReporte);
                    vXML = vXML.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
                    String vQuery = "[STEISP_CUMPLIMIENTO_Reportes] 6, 0, 0" +
                        ",'" + vXML + "'";
                    int vInfo = vConexion.ejecutarSql(vQuery);
                }
            }catch (Exception ex){

            }
        }

        private void insertarOSER(DataTable vDatos, int vId) {
            try{
                //INSERTAR LA RAZON Y EL COMENTARIO

                for (int i = 0; i < vDatos.Rows.Count; i++){
                    Object[] vDatosReporte = new object[16];
                    vDatosReporte[0] = TxCallAtendidas.Text;
                    vDatosReporte[1] = TxCallAtendidasNo.Text;
                    vDatosReporte[2] = TxCallObs.Text;
                    vDatosReporte[3] = TxATMCumplimiento.Text;
                    vDatosReporte[4] = TxATMCumplimientoNo.Text;
                    vDatosReporte[5] = TxATMObs.Text;
                    vDatosReporte[6] = TxATMObs.Text;
                    vDatosReporte[7] = TxATMObs.Text;
            
                    String vXML = vMaestro.ObtenerReporteCumplimientoOSER(vDatosReporte);
                    vXML = vXML.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
                    String vQuery = "[STEISP_CUMPLIMIENTO_Reportes] 7, 0, 0" +
                        ",'" + vXML + "'";
                    int vInfo = vConexion.ejecutarSql(vQuery);
                }
            }catch (Exception ex){

            }
        }

        private void obtenerEficiencia(){
            String Categoria1 = "", Categoria2 = "", Categoria3 = "";
            int TimeReal = 0;
            int RupEficiencia = 0;
            int NoRupEficiencia = 0;

            if (Categoria1 == "Software" && Categoria2 == "Active Directory" && Categoria3 == " " && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "Active Directory" && Categoria3 == "Desbloqueo Usuario" && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "Active Directory" && Categoria3 == "Incorporación al dominio" && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "Active Directory" && Categoria3 == "Personalizacion de usuario" && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "actualizaciones" && Categoria3 == "servidores" && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "antivirus" && Categoria3 == "revisar o actualizar" && TimeReal > 40)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "ATMS" && Categoria3 == "errores" && TimeReal > 30)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "Caja Empresarial" && Categoria3 == " " && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "Camara Compensación" && Categoria3 == " " && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "Camara Compensación" && Categoria3 == "Configuracion Cliente de Oracle" && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "Camara Compensación" && Categoria3 == "Configuracion de Minilectoras" && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "Camara Compensación" && Categoria3 == "Configuracion de Minilectoras" && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "Camara Compensación" && Categoria3 == "Configuracion DIPS" && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "Camara Compensación" && Categoria3 == "Configuracion IMS" && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "Camara Compensación" && Categoria3 == "Configurar Camara Entrante" && TimeReal > 40)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "Camara Compensación" && Categoria3 == "Configurar Camara Saliente" && TimeReal > 40)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "Camara Compensación" && Categoria3 == "Configurar Transacciones de Dips" && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "Camara Compensación" && Categoria3 == "Crear ODBC para Cliente Access" && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "Capturadores de la CNBS" && Categoria3 == " " && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "Capturadores de la CNBS" && Categoria3 == "Capturador de Creditos" && TimeReal > 30)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "Capturadores de la CNBS" && Categoria3 == "Contable" && TimeReal > 30)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "Credit Scoring" && Categoria3 == " " && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "Docuware" && Categoria3 == " " && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "Docuware" && Categoria3 == "no puede ingresar" && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "equipo" && Categoria3 == "preparar equipo" && TimeReal > 60)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "equipo" && Categoria3 == "revisar equipo" && TimeReal > 60)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "Fenix" && Categoria3 == "Instalacion" && TimeReal > 40)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "Fenix" && Categoria3 == "Personalizacion del Usuario" && TimeReal > 30)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "IBS" && Categoria3 == " " && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "IBS" && Categoria3 == "Acceso a IBS" && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "IBS" && Categoria3 == "Correccion en Cta de ahorros" && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "IBS" && Categoria3 == "estados de cuenta" && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "IBS" && Categoria3 == "Problema de impresión" && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "IBS" && Categoria3 == "Reportes Cierre IBS" && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "IBS" && Categoria3 == "SSL" && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "ICS" && Categoria3 == " " && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "ICS" && Categoria3 == "Cambio de Moneda" && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "ICS" && Categoria3 == "Instalación" && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "ICS" && Categoria3 == "Problemas con JAVA" && TimeReal > 30)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "ICS" && Categoria3 == "Revisión" && TimeReal > 30)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "Jteller" && Categoria3 == " " && TimeReal > 30)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "Jteller" && Categoria3 == "Activar Agencia" && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "Jteller" && Categoria3 == "Activar Cajero" && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "Jteller" && Categoria3 == "Activar Icono de Monitor" && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "Jteller" && Categoria3 == "Agencia OFFLINE" && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "Jteller" && Categoria3 == "Cajero Atrapado en IBS" && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "Jteller" && Categoria3 == "cambio a server de ventanillas" && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "Jteller" && Categoria3 == "Cambio de agencia" && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "Jteller" && Categoria3 == "Cambio de Password" && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "Lotus Notes" && Categoria3 == "Archivado" && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "Lotus Notes" && Categoria3 == "Cambio de Contraseña" && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "Lotus Notes" && Categoria3 == "Correo Externo" && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "Lotus Notes" && Categoria3 == "Creacion Web" && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "Lotus Notes" && Categoria3 == "Cuotas llenas" && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "Lotus Notes" && Categoria3 == "Instalación" && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "Lotus Notes" && Categoria3 == "mantenimientos" && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "Lotus Notes" && Categoria3 == "Modificar cuota" && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "Lotus Notes" && Categoria3 == "no se puede abrir el correo" && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "Lotus Notes" && Categoria3 == "Reinstalación" && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "Lotus Symphony" && Categoria3 == " " && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "Microsoft Office" && Categoria3 == " " && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "Microsoft Office" && Categoria3 == "Instalacion" && TimeReal > 40)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "Microsoft Office" && Categoria3 == "autorizacion" && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "monitoreos" && Categoria3 == "solicitud de permisos" && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "Navegacion Internet" && Categoria3 == " " && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "PC" && Categoria3 == "Instalacion de programas" && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "PC" && Categoria3 == "Instalacion de programas" && TimeReal > 60)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "Office 365" && Categoria3 == "Configuración de Outlook" && TimeReal > 20)
                RupEficiencia = RupEficiencia + 1;
            else if (Categoria1 == "Software" && Categoria2 == "Office 365" && Categoria3 == "migración de archivado" && TimeReal > 20)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Office 365" && Categoria3 == "migración de cuenta de correo" && TimeReal > 20)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "OPen text" && Categoria3 == "Instalación" && TimeReal > 30)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "OPen text" && Categoria3 == "revisión" && TimeReal > 30)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Paris" && Categoria3 == " " && TimeReal > 20)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Paris" && Categoria3 == "acceso" && TimeReal > 20)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Paris" && Categoria3 == "mantenimientos" && TimeReal > 20)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "PC" && Categoria3 == "Instalacion programas" && TimeReal > 20)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Planilla Inprema" && Categoria3 == "Problema impresion planilla" && TimeReal > 20)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "SAP" && Categoria3 == "Instalación" && TimeReal > 40)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "SAP" && Categoria3 == "Problemas de conexión" && TimeReal > 40)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Single Sign On" && Categoria3 == "Instalación" && TimeReal > 50)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Single Sign On" && Categoria3 == "Instalación" && TimeReal > 50)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Single Sign On" && Categoria3 == "revisión" && TimeReal > 50)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Sistema de colas qflow" && Categoria3 == "Instalación" && TimeReal > 50)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "SysAid" && Categoria3 == " " && TimeReal > 20)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "SYSCARD" && Categoria3 == "USUARIO" && TimeReal > 20)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Telesoft" && Categoria3 == " " && TimeReal > 30)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Telesoft" && Categoria3 == "Activar Clave" && TimeReal > 20)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Telesoft" && Categoria3 == "documentos" && TimeReal > 20)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "usuario mcafee cifrado" && Categoria3 == "agregar / eliminar usuario" && TimeReal > 20)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "usuario mcafee cifrado" && Categoria3 == "habilitación maquina" && TimeReal > 20)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Virus" && Categoria3 == " " && TimeReal > 20)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Virus" && Categoria3 == "Estudio de maquina con virus" && TimeReal > 30)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Virus" && Categoria3 == "Instalacion Antivirus" && TimeReal > 20)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Virus" && Categoria3 == "Revision Antivirus" && TimeReal > 20)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Webex" && Categoria3 == "reseteo de contraseña" && TimeReal > 20)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Windows" && Categoria3 == " " && TimeReal > 20)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Windows" && Categoria3 == "accesos" && TimeReal > 20)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Agentes bancarios" && Categoria3 == " " && TimeReal > 20)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Agentes bancarios" && Categoria3 == "cambio de chip" && TimeReal > 30)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Agentes bancarios" && Categoria3 == "Cambio de equipo" && TimeReal > 30)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Agentes bancarios" && Categoria3 == "Instalacion" && TimeReal > 20)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Agentes bancarios" && Categoria3 == "Mantenimiento" && TimeReal > 20)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Agentes bancarios" && Categoria3 == "Problemas de Software" && TimeReal > 20)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "ATM" && Categoria3 == "Lectora de Tarjetas" && TimeReal > 80)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "ATM" && Categoria3 == "Mantenimiento Preventivo Programado" && TimeReal > 180)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "ATM" && Categoria3 == "No enciende" && TimeReal > 50)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "ATM" && Categoria3 == "Offline - Out of service" && TimeReal > 50)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "ATM" && Categoria3 == "Otras Solicitudes" && TimeReal > 50)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "ATM" && Categoria3 == "Problemas de comunicación" && TimeReal > 50)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "ATM" && Categoria3 == "Revision de UPS" && TimeReal > 30)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "ATM" && Categoria3 == "Revision EDC" && TimeReal > 20)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "CPU" && Categoria3 == "Instalacion Maquina Contingencia" && TimeReal > 30)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "CPU" && Categoria3 == "Instalación" && TimeReal > 30)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "CPU" && Categoria3 == "Recuperacion Maquina Contingenci" && TimeReal > 30)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "CPU" && Categoria3 == "Reemplazo" && TimeReal > 30)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "CPU" && Categoria3 == "Reparación" && TimeReal > 30)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Impresoras" && Categoria3 == "configuracion" && TimeReal > 30)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Impresoras" && Categoria3 == "Instalación" && TimeReal > 30)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Impresoras" && Categoria3 == "Reemplazo" && TimeReal > 30)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Impresoras" && Categoria3 == "Reparación" && TimeReal > 30)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos colas agencias" && Categoria3 == "Instalación" && TimeReal > 40)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos colas agencias" && Categoria3 == "Reparación" && TimeReal > 40)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos colas agencias" && Categoria3 == "Actualización" && TimeReal > 50)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos medios de pago" && Categoria3 == "Impresora" && TimeReal > 30)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos medios de pago" && Categoria3 == "Instalación" && TimeReal > 40)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos medios de pago" && Categoria3 == "Lectora de Tarjeta" && TimeReal > 30)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos medios de pago" && Categoria3 == "Monitor" && TimeReal > 20)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos medios de pago" && Categoria3 == "Offline" && TimeReal > 30)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos medios de pago" && Categoria3 == "Problemas con Transacciones Caja Empresarial" && TimeReal > 30)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos medios de pago" && Categoria3 == "Problemas de Comunicación" && TimeReal > 30)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos medios de pago" && Categoria3 == "Problemas de Software" && TimeReal > 40)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos medios de pago" && Categoria3 == "Revisión" && TimeReal > 30)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos medios de pago" && Categoria3 == "Revisión de UPS" && TimeReal > 20)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Mantenimiento Preventivo" && Categoria3 == "Agencia Completa" && TimeReal > 90)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Mantenimiento Preventivo" && Categoria3 == "DATACARD" && TimeReal > 30)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Mantenimiento Preventivo" && Categoria3 == "Kiosco transaccional" && TimeReal > 40)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Mantenimiento Preventivo" && Categoria3 == "Piso Data Center" && TimeReal > 40)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Monitores" && Categoria3 == "Instalación" && TimeReal > 20)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Monitores" && Categoria3 == "Reemplazo" && TimeReal > 20)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Monitores" && Categoria3 == "Reparación" && TimeReal > 30)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Multi-ATM" && Categoria3 == "Depositos" && TimeReal > 40)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Multi-ATM" && Categoria3 == "Dispensador" && TimeReal > 40)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Multi-ATM" && Categoria3 == "Equipo Pegado" && TimeReal > 40)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Multi-ATM" && Categoria3 == "Lectora" && TimeReal > 40)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Multi-ATM" && Categoria3 == "Pagos tarjetas" && TimeReal > 40)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Perifericos" && Categoria3 == " " && TimeReal > 20)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Perifericos" && Categoria3 == "Instalación" && TimeReal > 20)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Perifericos" && Categoria3 == "Reemplazo" && TimeReal > 20)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Relojes biometricos" && Categoria3 == "instalación" && TimeReal > 30)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Relojes biometricos" && Categoria3 == "Reemplazo" && TimeReal > 30)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Relojes biometricos" && Categoria3 == "Revisión" && TimeReal > 30)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Scanners" && Categoria3 == " " && TimeReal > 30)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Scanners" && Categoria3 == "Instalación" && TimeReal > 30)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Scanners" && Categoria3 == "Reemplazo" && TimeReal > 30)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Scanners" && Categoria3 == "Reparación" && TimeReal > 30)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "UPS" && Categoria3 == " " && TimeReal > 30)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "UPS" && Categoria3 == "Instalación" && TimeReal > 30)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "UPS" && Categoria3 == "Reemplazo" && TimeReal > 30)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Call center" && Categoria3 == "estadísticas" && TimeReal > 20)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Call center" && Categoria3 == "evaluaciones agentes" && TimeReal > 90)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Call center" && Categoria3 == "nuevo agente" && TimeReal > 90)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Call center" && Categoria3 == "problemas de grabación" && TimeReal > 40)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Call center" && Categoria3 == "workforce" && TimeReal > 65)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Enlaces de Datos" && Categoria3 == " " && TimeReal > 40)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Enlaces de Datos" && Categoria3 == "Caida de agencia" && TimeReal > 40)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Enlaces de Datos" && Categoria3 == "Intermitencia" && TimeReal > 40)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Enlaces de Datos" && Categoria3 == "Revisión de redundancia" && TimeReal > 40)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Equipos de Comunicacion" && Categoria3 == " " && TimeReal > 40)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Equipos de Comunicacion" && Categoria3 == "Configuración" && TimeReal > 40)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Equipos de Comunicacion" && Categoria3 == "Documentación" && TimeReal > 30)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Equipos de Comunicacion" && Categoria3 == "Instalación" && TimeReal > 40)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Equipos de Comunicacion" && Categoria3 == "Mantenimiento Preventivo" && TimeReal > 30)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Equipos de Comunicacion" && Categoria3 == "Revisión" && TimeReal > 40)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Monitoreo" && Categoria3 == "Monitoreo de interfaces caidas" && TimeReal > 30)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Redes" && Categoria3 == " " && TimeReal > 20)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Redes" && Categoria3 == "802.1X" && TimeReal > 20)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Redes" && Categoria3 == "Conexion de DVR ATM" && TimeReal > 20)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Redes" && Categoria3 == "Instalación punto de red" && TimeReal > 40)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Redes" && Categoria3 == "Permisos de Red" && TimeReal > 20)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Redes" && Categoria3 == "Reparación de punto de red" && TimeReal > 20)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Redes" && Categoria3 == "Solicitud de acceso a internet Wireless" && TimeReal > 40)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Redes" && Categoria3 == "VPN" && TimeReal > 40)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Remesas" && Categoria3 == "Revision de Comunicacion" && TimeReal > 60)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Telefonia" && Categoria3 == "Configuracion" && TimeReal > 20)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Telefonia" && Categoria3 == "Generación de clave de telefono" && TimeReal > 20)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Telefonia" && Categoria3 == "Instalación" && TimeReal > 30)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Telefonia" && Categoria3 == "intermitencia" && TimeReal > 40)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Telefonia" && Categoria3 == "Otros" && TimeReal > 30)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Telefonia" && Categoria3 == "Pickup group" && TimeReal > 30)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Telefonia" && Categoria3 == "Planta IP Plaza Bancatlan" && TimeReal > 40)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Telefonia" && Categoria3 == "Reparación" && TimeReal > 40)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Telefonia" && Categoria3 == "Softphone Telesoft" && TimeReal > 30)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Test" && Categoria3 == "test" && TimeReal > 30)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Wireless" && Categoria3 == "Creación de usuario externo" && TimeReal > 20)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Wireless" && Categoria3 == "Creación de usuario interno" && TimeReal > 20)
            {
                RupEficiencia = RupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Active Directory" && Categoria3 == " " && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Active Directory" && Categoria3 == "Desbloqueo Usuario" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Active Directory" && Categoria3 == "Incorporación al dominio" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Active Directory" && Categoria3 == "Personalizacion de usuario" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "actualizaciones" && Categoria3 == "servidores" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "antivirus" && Categoria3 == "revisar o actualizar" && TimeReal <= 40)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "ATMS" && Categoria3 == "errores" && TimeReal <= 30)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Caja Empresarial" && Categoria3 == " " && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Camara Compensación" && Categoria3 == " " && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Camara Compensación" && Categoria3 == "Configuracion Cliente de Oracle" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Camara Compensación" && Categoria3 == "Configuracion de Minilectoras" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Camara Compensación" && Categoria3 == "Configuracion de Minilectoras" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Camara Compensación" && Categoria3 == "Configuracion DIPS" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Camara Compensación" && Categoria3 == "Configuracion IMS" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Camara Compensación" && Categoria3 == "Configurar Camara Entrante" && TimeReal <= 40)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Camara Compensación" && Categoria3 == "Configurar Camara Saliente" && TimeReal <= 40)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Camara Compensación" && Categoria3 == "Configurar Transacciones de Dips" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Camara Compensación" && Categoria3 == "Crear ODBC para Cliente Access" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Capturadores de la CNBS" && Categoria3 == " " && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Capturadores de la CNBS" && Categoria3 == "Capturador de Creditos" && TimeReal <= 30)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Capturadores de la CNBS" && Categoria3 == "Contable" && TimeReal <= 30)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Credit Scoring" && Categoria3 == " " && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Docuware" && Categoria3 == " " && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Docuware" && Categoria3 == "no puede ingresar" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "equipo" && Categoria3 == "preparar equipo" && TimeReal <= 60)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "equipo" && Categoria3 == "revisar equipo" && TimeReal <= 60)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Fenix" && Categoria3 == "Instalacion" && TimeReal <= 40)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Fenix" && Categoria3 == "Personalizacion del Usuario" && TimeReal <= 30)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "IBS" && Categoria3 == " " && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "IBS" && Categoria3 == "Acceso a IBS" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "IBS" && Categoria3 == "Correccion en Cta de ahorros" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "IBS" && Categoria3 == "estados de cuenta" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "IBS" && Categoria3 == "Problema de impresión" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "IBS" && Categoria3 == "Reportes Cierre IBS" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "IBS" && Categoria3 == "SSL" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "ICS" && Categoria3 == " " && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "ICS" && Categoria3 == "Cambio de Moneda" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "ICS" && Categoria3 == "Instalación" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "ICS" && Categoria3 == "Problemas con JAVA" && TimeReal <= 30)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "ICS" && Categoria3 == "Revisión" && TimeReal <= 30)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Jteller" && Categoria3 == " " && TimeReal <= 30)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Jteller" && Categoria3 == "Activar Agencia" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Jteller" && Categoria3 == "Activar Cajero" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Jteller" && Categoria3 == "Activar Icono de Monitor" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Jteller" && Categoria3 == "Agencia OFFLINE" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Jteller" && Categoria3 == "Cajero Atrapado en IBS" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Jteller" && Categoria3 == "cambio a server de ventanillas" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Jteller" && Categoria3 == "Cambio de agencia" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Jteller" && Categoria3 == "Cambio de Password" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Lotus Notes" && Categoria3 == "Archivado" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Lotus Notes" && Categoria3 == "Cambio de Contraseña" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Lotus Notes" && Categoria3 == "Correo Externo" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Lotus Notes" && Categoria3 == "Creacion Web" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Lotus Notes" && Categoria3 == "Cuotas llenas" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Lotus Notes" && Categoria3 == "Instalación" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Lotus Notes" && Categoria3 == "mantenimientos" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Lotus Notes" && Categoria3 == "Modificar cuota" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Lotus Notes" && Categoria3 == "no se puede abrir el correo" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Lotus Notes" && Categoria3 == "Reinstalación" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Lotus Symphony" && Categoria3 == " " && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Microsoft Office" && Categoria3 == " " && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Microsoft Office" && Categoria3 == "Instalacion" && TimeReal <= 40)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Microsoft Office" && Categoria3 == "autorizacion" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "monitoreos" && Categoria3 == "solicitud de permisos" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Navegacion Internet" && Categoria3 == " " && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "PC" && Categoria3 == "Instalacion de programas" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "PC" && Categoria3 == "Instalacion de programas" && TimeReal <= 60)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Office 365" && Categoria3 == "Configuración de Outlook" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Office 365" && Categoria3 == "migración de archivado" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Office 365" && Categoria3 == "migración de cuenta de correo" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "OPen text" && Categoria3 == "Instalación" && TimeReal <= 30)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "OPen text" && Categoria3 == "revisión" && TimeReal <= 30)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Paris" && Categoria3 == " " && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Paris" && Categoria3 == "acceso" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Paris" && Categoria3 == "mantenimientos" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "PC" && Categoria3 == "Instalacion programas" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Planilla Inprema" && Categoria3 == "Problema impresion planilla" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "SAP" && Categoria3 == "Instalación" && TimeReal <= 40)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "SAP" && Categoria3 == "Problemas de conexión" && TimeReal <= 40)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Single Sign On" && Categoria3 == "Instalación" && TimeReal <= 50)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Single Sign On" && Categoria3 == "Instalación" && TimeReal <= 50)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Single Sign On" && Categoria3 == "revisión" && TimeReal <= 50)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Sistema de colas qflow" && Categoria3 == "Instalación" && TimeReal <= 50)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "SysAid" && Categoria3 == " " && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "SYSCARD" && Categoria3 == "USUARIO" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Telesoft" && Categoria3 == " " && TimeReal <= 30)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Telesoft" && Categoria3 == "Activar Clave" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Telesoft" && Categoria3 == "documentos" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "usuario mcafee cifrado" && Categoria3 == "agregar / eliminar usuario" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "usuario mcafee cifrado" && Categoria3 == "habilitación maquina" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Virus" && Categoria3 == " " && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Virus" && Categoria3 == "Estudio de maquina con virus" && TimeReal <= 30)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Virus" && Categoria3 == "Instalacion Antivirus" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Virus" && Categoria3 == "Revision Antivirus" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Webex" && Categoria3 == "reseteo de contraseña" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Windows" && Categoria3 == " " && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Software" && Categoria2 == "Windows" && Categoria3 == "accesos" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Agentes bancarios" && Categoria3 == " " && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Agentes bancarios" && Categoria3 == "cambio de chip" && TimeReal <= 30)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Agentes bancarios" && Categoria3 == "Cambio de equipo" && TimeReal <= 30)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Agentes bancarios" && Categoria3 == "Instalacion" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Agentes bancarios" && Categoria3 == "Mantenimiento" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Agentes bancarios" && Categoria3 == "Problemas de Software" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "ATM" && Categoria3 == "Lectora de Tarjetas" && TimeReal <= 80)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "ATM" && Categoria3 == "Mantenimiento Preventivo Programado" && TimeReal <= 180)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "ATM" && Categoria3 == "No enciende" && TimeReal <= 50)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "ATM" && Categoria3 == "Offline - Out of service" && TimeReal <= 50)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "ATM" && Categoria3 == "Otras Solicitudes" && TimeReal <= 50)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "ATM" && Categoria3 == "Problemas de comunicación" && TimeReal <= 50)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "ATM" && Categoria3 == "Revision de UPS" && TimeReal <= 30)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "ATM" && Categoria3 == "Revision EDC" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "CPU" && Categoria3 == "Instalacion Maquina Contingencia" && TimeReal <= 30)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "CPU" && Categoria3 == "Instalación" && TimeReal <= 30)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "CPU" && Categoria3 == "Recuperacion Maquina Contingenci" && TimeReal <= 30)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "CPU" && Categoria3 == "Reemplazo" && TimeReal <= 30)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "CPU" && Categoria3 == "Reparación" && TimeReal <= 30)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Impresoras" && Categoria3 == "configuracion" && TimeReal <= 30)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Impresoras" && Categoria3 == "Instalación" && TimeReal <= 30)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Impresoras" && Categoria3 == "Reemplazo" && TimeReal <= 30)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Impresoras" && Categoria3 == "Reparación" && TimeReal <= 30)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos colas agencias" && Categoria3 == "Instalación" && TimeReal <= 40)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos colas agencias" && Categoria3 == "Reparación" && TimeReal <= 40)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos colas agencias" && Categoria3 == "Actualización" && TimeReal <= 50)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos medios de pago" && Categoria3 == "Impresora" && TimeReal <= 30)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos medios de pago" && Categoria3 == "Instalación" && TimeReal <= 40)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos medios de pago" && Categoria3 == "Lectora de Tarjeta" && TimeReal <= 30)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos medios de pago" && Categoria3 == "Monitor" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos medios de pago" && Categoria3 == "Offline" && TimeReal <= 30)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos medios de pago" && Categoria3 == "Problemas con Transacciones Caja Empresarial" && TimeReal <= 30)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos medios de pago" && Categoria3 == "Problemas de Comunicación" && TimeReal <= 30)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos medios de pago" && Categoria3 == "Problemas de Software" && TimeReal <= 40)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos medios de pago" && Categoria3 == "Revisión" && TimeReal <= 30)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos medios de pago" && Categoria3 == "Revisión de UPS" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Mantenimiento Preventivo" && Categoria3 == "Agencia Completa" && TimeReal <= 90)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Mantenimiento Preventivo" && Categoria3 == "DATACARD" && TimeReal <= 30)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Mantenimiento Preventivo" && Categoria3 == "Kiosco transaccional" && TimeReal <= 40)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Mantenimiento Preventivo" && Categoria3 == "Piso Data Center" && TimeReal <= 40)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Monitores" && Categoria3 == "Instalación" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Monitores" && Categoria3 == "Reemplazo" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Monitores" && Categoria3 == "Reparación" && TimeReal <= 30)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Multi-ATM" && Categoria3 == "Depositos" && TimeReal <= 40)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Multi-ATM" && Categoria3 == "Dispensador" && TimeReal <= 40)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Multi-ATM" && Categoria3 == "Equipo Pegado" && TimeReal <= 40)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Multi-ATM" && Categoria3 == "Lectora" && TimeReal <= 40)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Multi-ATM" && Categoria3 == "Pagos tarjetas" && TimeReal <= 40)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Perifericos" && Categoria3 == " " && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Perifericos" && Categoria3 == "Instalación" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Perifericos" && Categoria3 == "Reemplazo" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Relojes biometricos" && Categoria3 == "instalación" && TimeReal <= 30)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Relojes biometricos" && Categoria3 == "Reemplazo" && TimeReal <= 30)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Relojes biometricos" && Categoria3 == "Revisión" && TimeReal <= 30)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Scanners" && Categoria3 == " " && TimeReal <= 30)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Scanners" && Categoria3 == "Instalación" && TimeReal <= 30)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Scanners" && Categoria3 == "Reemplazo" && TimeReal <= 30)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "Scanners" && Categoria3 == "Reparación" && TimeReal <= 30)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "UPS" && Categoria3 == " " && TimeReal <= 30)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "UPS" && Categoria3 == "Instalación" && TimeReal <= 30)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Hardware" && Categoria2 == "UPS" && Categoria3 == "Reemplazo" && TimeReal <= 30)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Call center" && Categoria3 == "estadísticas" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Call center" && Categoria3 == "evaluaciones agentes" && TimeReal <= 90)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Call center" && Categoria3 == "nuevo agente" && TimeReal <= 90)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Call center" && Categoria3 == "problemas de grabación" && TimeReal <= 40)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Call center" && Categoria3 == "workforce" && TimeReal <= 65)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Enlaces de Datos" && Categoria3 == " " && TimeReal <= 40)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Enlaces de Datos" && Categoria3 == "Caida de agencia" && TimeReal <= 40)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Enlaces de Datos" && Categoria3 == "Intermitencia" && TimeReal <= 40)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Enlaces de Datos" && Categoria3 == "Revisión de redundancia" && TimeReal <= 40)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Equipos de Comunicacion" && Categoria3 == " " && TimeReal <= 40)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Equipos de Comunicacion" && Categoria3 == "Configuración" && TimeReal <= 40)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Equipos de Comunicacion" && Categoria3 == "Documentación" && TimeReal <= 30)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Equipos de Comunicacion" && Categoria3 == "Instalación" && TimeReal <= 40)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Equipos de Comunicacion" && Categoria3 == "Mantenimiento Preventivo" && TimeReal <= 30)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Equipos de Comunicacion" && Categoria3 == "Revisión" && TimeReal <= 40)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Monitoreo" && Categoria3 == "Monitoreo de interfaces caidas" && TimeReal <= 30)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Redes" && Categoria3 == " " && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Redes" && Categoria3 == "802.1X" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Redes" && Categoria3 == "Conexion de DVR ATM" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Redes" && Categoria3 == "Instalación punto de red" && TimeReal <= 40)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Redes" && Categoria3 == "Permisos de Red" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Redes" && Categoria3 == "Reparación de punto de red" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Redes" && Categoria3 == "Solicitud de acceso a internet Wireless" && TimeReal <= 40)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Redes" && Categoria3 == "VPN" && TimeReal <= 40)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Remesas" && Categoria3 == "Revision de Comunicacion" && TimeReal <= 60)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Telefonia" && Categoria3 == "Configuracion" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Telefonia" && Categoria3 == "Generación de clave de telefono" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Telefonia" && Categoria3 == "Instalación" && TimeReal <= 30)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Telefonia" && Categoria3 == "intermitencia" && TimeReal <= 40)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Telefonia" && Categoria3 == "Otros" && TimeReal <= 30)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Telefonia" && Categoria3 == "Pickup group" && TimeReal <= 30)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Telefonia" && Categoria3 == "Planta IP Plaza Bancatlan" && TimeReal <= 40)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Telefonia" && Categoria3 == "Reparación" && TimeReal <= 40)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Telefonia" && Categoria3 == "Softphone Telesoft" && TimeReal <= 30)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Test" && Categoria3 == "test" && TimeReal <= 30)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Wireless" && Categoria3 == "Creación de usuario externo" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (Categoria1 == "Comunicaciones" && Categoria2 == "Wireless" && Categoria3 == "Creación de usuario interno" && TimeReal <= 20)
            {
                NoRupEficiencia = NoRupEficiencia + 1;
            }
            else if (TimeReal > 30) 
            { 
                RupEficiencia = RupEficiencia + 1; 
            } 
            else if (TimeReal <= 30) 
            { 
                NoRupEficiencia = NoRupEficiencia + 1; 
            }
        }
    }
}