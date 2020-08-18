using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infatlan_STEI.classes;
using System.Configuration;

namespace Infatlan_STEI.paginas.reportes
{
    public partial class metasCumplimiento : System.Web.UI.Page
    {
        db vConexion = new db();
        Security vSecurity = new Security();
        GenerarXML vMaestro = new GenerarXML();

        protected void Page_Load(object sender, EventArgs e){
            if (!Page.IsPostBack){
                if (Convert.ToBoolean(Session["AUTH"])){
                    if (!vSecurity.ObtenerPermiso(Session["USUARIO"].ToString(), 5).Consulta)
                        Response.Redirect("/default.aspx");

                    String vEx = Request.QueryString["ex"];
                    if (vEx == "1")
                        ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "alert('Las llamadas atendidas son mayores que las totales')", true);
                    else if (vEx == "2") {
                        ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "alert('Reporte enviado con éxito.')", true);
                        cargarData();
                    } else if (vEx == "3") {
                        ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "alert('Favor ingrese las observaciones de las llamadas.')", true);
                        cargarData();
                    }else {
                        String vQuery = "[STEISP_CUMPLIMIENTO_Reportes] 9, '" + Session["USUARIO"].ToString() + "'";
                        int vInfo = vConexion.obtenerId(vQuery);
                        if (vInfo == 1) { 
                            Session["CUMPL_FLAG_PENDIENTE"] = null;
                            cargarData();
                        }else { 
                            Session["CUMPL_FLAG_PENDIENTE"] = true;
                            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "alert('Ya tiene un reporte registrado hoy. Regrese mañana.')", true);
                        }
                    }  
                }else{
                    Response.Redirect("/login.aspx");
                }
            }
        }

        private void cargarData(){
            try{
                String vFecha = "";
                DataTable vDatos = (DataTable)Session["AUTHCLASS"];

                if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
                    vFecha = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");
                else if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday) 
                    vFecha = DateTime.Now.AddDays(-2).ToString("dd/MM/yyyy");
                else if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
                    vFecha = DateTime.Now.AddDays(-3).ToString("dd/MM/yyyy");
                else
                    vFecha = DateTime.Now.ToString("dd/MM/yyyy");
                
                vFecha = "08/06/2020";

                String vAssignedGroup = vDatos.Rows[0]["assignedGroup"].ToString();
                //cargarCalls();
                cargarMediosPago(vFecha, vAssignedGroup);
                cargarKPI(vFecha, vAssignedGroup);
                cargarRuptura(vFecha, vAssignedGroup);
                cargarOSER(vFecha, vAssignedGroup);
                cargarInsatisfaccion(vFecha, vAssignedGroup);
            }catch (Exception Ex){
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        public void Mensaje(string vMensaje, WarningType type){
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }
        
        private void cargarMediosPago(String vFecha, String vAssignedGroup) {
            String vQuery = "[STEISP_CUMPLIMIENTO_Generales] 4,'" + vAssignedGroup + "','" + vFecha + "'";
            DataSet vDSMedios = vConexion.obtenerDataSetSA(vQuery);

            int vCumplidas = Convert.ToInt32(vDSMedios.Tables[0].Rows[0]["Cumplimiento"].ToString());
            int vIncumplidas = Convert.ToInt32(vDSMedios.Tables[1].Rows[0]["Incumplimiento"].ToString());
            Decimal vPorcentaje = obtenerPorcentaje(vCumplidas, vIncumplidas);

            TxATMCumplimiento.Text = vCumplidas.ToString();
            TxATMCumplimientoNo.Text = vIncumplidas.ToString();
            TxATMTotal.Text = Convert.ToString(vCumplidas + vIncumplidas);
            TxATMPorcentaje.Text = vPorcentaje.ToString();
            TxATMObs.Text = vPorcentaje > 90 ? "Meta Alcanzada" : "";
            CATM.Attributes.Add("data-percent", vPorcentaje.ToString());
            //LitATM.Text = "<div class='chart easy-pie-chart-4' data-percent='" + vPorcentaje.ToString() + "'><span class='percent'></span></div>";

            vQuery = "[STEISP_CUMPLIMIENTO_Generales] 5,'" + vAssignedGroup + "','" + vFecha + "'";
            vDSMedios = vConexion.obtenerDataSetSA(vQuery);

            vCumplidas = Convert.ToInt32(vDSMedios.Tables[0].Rows[0]["Cumplimiento"].ToString());
            vIncumplidas = Convert.ToInt32(vDSMedios.Tables[1].Rows[0]["Incumplimiento"].ToString());
            vPorcentaje = obtenerPorcentaje(vCumplidas, vIncumplidas);

            TxABACumplimiento.Text = vCumplidas.ToString();
            TxABACumplimientoNo.Text = vIncumplidas.ToString();
            TxABATotal.Text = Convert.ToString(vCumplidas + vIncumplidas);
            TxABAPorcentaje.Text = vPorcentaje.ToString();
            TxABAObs.Text = vPorcentaje > 90 ? "Meta Alcanzada" : "";
            LitABA.Text = "<div class='chart easy-pie-chart-4' data-percent='" + vPorcentaje.ToString() + "'><span class='percent'></span></div>";

            vQuery = "[STEISP_CUMPLIMIENTO_Generales] 6,'" + vAssignedGroup + "','" + vFecha + "'";
            vDSMedios = vConexion.obtenerDataSetSA(vQuery);

            vCumplidas = Convert.ToInt32(vDSMedios.Tables[0].Rows[0]["Cumplimiento"].ToString());
            vIncumplidas = Convert.ToInt32(vDSMedios.Tables[1].Rows[0]["Incumplimiento"].ToString());
            vPorcentaje = obtenerPorcentaje(vCumplidas, vIncumplidas);

            TxCajaCumplidas.Text = vCumplidas.ToString();
            TxCajaCumplidasNo.Text = vIncumplidas.ToString();
            TxCajaTotal.Text = Convert.ToString(vCumplidas + vIncumplidas);
            TxCajaPorcentaje.Text = vPorcentaje.ToString();
            TxCajaObs.Text = vPorcentaje > 90 ? "Meta Alcanzada" : "";
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

            TxKPIPorcentaje.Text = vPorcentaje.ToString();
            TxKPITotal.Text = Convert.ToString(vSuma);
            TxKPICumplimiento.Text = vCumplidas.ToString();
            TxKPICumplimientoNo.Text = vIncumplidas.ToString();
            TxKPIObs.Text = vPorcentaje > 90 ? "Meta Alcanzada" : "";
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
            if (vDatos.Rows.Count > 0){
                vDatos.Columns.Add("idRazon");
                vDatos.Columns.Add("comentario");
                LbResOSER.Text = "";
                GvOSER.DataSource = vDatos;
                GvOSER.DataBind();
                Session["CUMPL_OSER"] = vDatos;
            }
        }

        private void cargarInsatisfaccion(String vFecha, String vAssignedGroup) { 
            try{
                String vQuery = "[STEISP_CUMPLIMIENTO_Generales] 15,'" + vAssignedGroup + "'" +
                    ",'" + vFecha + "'";
                DataTable vDatos = vConexion.obtenerDataTableSA(vQuery);
                if (vDatos.Rows.Count > 0){
                    vDatos.Columns.Add("observaciones");
                    LbInsatisfaccion.Text = "";
                    GvInsatisfacciones.DataSource = vDatos;
                    GvInsatisfacciones.DataBind();
                    Session["CUMPL_INSATISCACCION"] = vDatos;
                }
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void TxCallAtendidas_TextChanged(object sender, EventArgs e){
            try{
                if (Convert.ToInt32(TxCallAtendidas.Text) > Convert.ToInt32(TxCallTotal.Text))
                    Response.Redirect("metasCumplimiento.aspx?ex=1");
                    

                int vNoAtendidas = Convert.ToInt32(TxCallTotal.Text) - Convert.ToInt32(TxCallAtendidas.Text);
                TxCallAtendidasNo.Text = vNoAtendidas.ToString();
                
                float vAtendidasProm = float.Parse(TxCallAtendidas.Text) / float.Parse(TxCallTotal.Text) * 100;
                TxCallPorcentajeSi.Text = Convert.ToDecimal(Math.Round(vAtendidasProm, 2)).ToString();

                float vAtendidasPromNo = 100 - float.Parse(TxCallPorcentajeSi.Text);
                TxCallPorcentajeNo.Text = Convert.ToDecimal(Math.Round(vAtendidasPromNo, 2)).ToString();
                TxCallObs.Text = vAtendidasProm > 90 ? "Meta Alcanzada" : "";
                CCall.Attributes.Add("data-percent", TxCallPorcentajeSi.Text);

                String vFecha = "";
                if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
                    vFecha = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");
                else if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday) 
                    vFecha = DateTime.Now.AddDays(-2).ToString("dd/MM/yyyy");
                else if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
                    vFecha = DateTime.Now.AddDays(-3).ToString("dd/MM/yyyy");
                else
                    vFecha = DateTime.Now.ToString("dd/MM/yyyy");
                
                vFecha = "08/06/2020";
                DataTable vData = cargarRendimiento(vFecha);
                if (vData.Rows.Count > 0){
                    graficos(vData);
                }
                UPCalls.Update();
                UPanelRendimiento.Update();
            }catch (Exception ex){
                ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + ex.Message + "','" + WarningType.Danger.ToString().ToLower() + "')", true);
            }
        }

        private DataTable cargarRendimiento(String vFecha) {
            DataTable vData = new DataTable();
            try{
                String vQuery = "[STEISP_CUMPLIMIENTO_Rendimiento] 1,'" + Session["USUARIO"].ToString() + "'";
                vData = vConexion.obtenerDataTable(vQuery);
                if (vData.Rows.Count > 0){
                    for (int i = 0; i < vData.Rows.Count; i++){
                        promedioNotas(vData, i);
                        obtenerTareas(vData, i, vFecha);
                        obtenerRuptura(vData, i, vFecha);
                        obtenerOpinion(vData, i, vFecha);
                        obtenerProductividad(vData, i, vFecha);
                        obtenerEficiencia(vData, i, vFecha);
                        obtenerTotal(vData, i, vFecha);
                    }
                    
                    GvRendimiento.DataSource = vData;
                    GvRendimiento.DataBind();
                    Session["CUMPL_RENDIMIENTO"] = vData;
                }

            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
            return vData;
        }

        protected void GvOSER_RowDataBound(object sender, GridViewRowEventArgs e){
            if (e.Row.RowType == DataControlRowType.DataRow){
                var vDropDown = e.Row.Cells[2].FindControl("DDLRazonER") as DropDownList;
                var vComment = e.Row.Cells[3].FindControl("TxOSERObs") as TextBox;
                if (vDropDown != null){
                    String vQuery = "[STEISP_CUMPLIMIENTO_Ajustes] 11, 5";
                    DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                    vDropDown.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    foreach (DataRow item in vDatos.Rows){
                        vDropDown.Items.Add(new ListItem { Value = item["idMotivo"].ToString(), Text = item["nombre"].ToString() });
                    }
                    if (HttpContext.Current.Session["CUMPL_OSER_PAGING"] != null ){
                        DataTable vData = (DataTable)Session["CUMPL_OSER"];
                        for (int i = 0; i < vData.Rows.Count; i++){
                            DataRowView drv = e.Row.DataItem as DataRowView;

                            if (vData.Rows[i]["id"].ToString() == drv["id"].ToString()){
                                vDropDown.SelectedValue = vData.Rows[i]["idRazon"].ToString();
                                vComment.Text = vData.Rows[i]["comentario"].ToString();
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
                    TextBox TxComment = (TextBox)row.Cells[3].FindControl("TxOSERObs");
                    vData.Rows[vCont]["idRazon"] = DDLRazon.SelectedValue;
                    vData.Rows[vCont]["comentario"] = TxComment.Text;
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
                var vComment = e.Row.Cells[3].FindControl("TxRupturaObs") as TextBox;
                if (vDropDown != null){
                    String vQuery = "[STEISP_CUMPLIMIENTO_Ajustes] 11, 4";
                    DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                    
                    if (e.Row.Cells[1].Text != ""){
                        Decimal vCelda2 = Convert.ToDecimal(e.Row.Cells[1].Text.ToString());
                        if (vCelda2 > 1){
                            vDropDown.Items.Add(new ListItem { Value = vDatos.Rows[0]["idMotivo"].ToString(), Text = vDatos.Rows[0]["nombre"].ToString() });
                        }
                    }

                    if (vDropDown.Items.Count < 1){
                        vDropDown.Items.Add(new ListItem { Value = "0", Text = "Seleccione..." });
                        foreach (DataRow item in vDatos.Rows){
                            vDropDown.Items.Add(new ListItem { Value = item["idMotivo"].ToString(), Text = item["nombre"].ToString() });
                        }
                    }

                    if (HttpContext.Current.Session["CUMPL_RUPTURA_PAGING"] != null ){
                        DataTable vData = (DataTable)Session["CUMPL_RUPTURA"];
                        for (int i = 0; i < vData.Rows.Count; i++){
                            DataRowView drv = e.Row.DataItem as DataRowView;

                            if (vData.Rows[i]["id"].ToString() == drv["id"].ToString()){
                                vDropDown.SelectedValue = vData.Rows[i]["idRazon"].ToString();
                                vComment.Text = vData.Rows[i]["comentario"].ToString();
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
                    TextBox TxComment = (TextBox)row.Cells[3].FindControl("TxRupturaObs");
                    vData.Rows[vCont]["idRazon"] = DDLRazonRuptura.SelectedValue;
                    vData.Rows[vCont]["comentario"] = TxComment.Text;
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
                validaciones();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);
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

                    String vXML = vMaestro.ObtenerCumplimientoKPI(vDatosReporte);
                    vXML = vXML.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
                    String vQuery = "[STEISP_CUMPLIMIENTO_Reportes] 5, 0, 0" +
                        ",'" + vXML + "'";
                    int vInfo = vConexion.ejecutarSql(vQuery);
                }
            }catch (Exception ex){
                throw new Exception(ex.Message);
            }
        }

        private void insertarRupturas(DataTable vDatos, int vId) {
            try{
                for (int i = 0; i < vDatos.Rows.Count; i++){
                    Object[] vDatosReporte = new object[8];
                    vDatosReporte[0] = vId.ToString();
                    vDatosReporte[1] = vDatos.Rows[i]["id"].ToString();
                    vDatosReporte[2] = vDatos.Rows[i]["tiempoRespuesta"].ToString();
                    vDatosReporte[3] = vDatos.Rows[i]["tiempoAtencion"].ToString();
                    vDatosReporte[4] = vDatos.Rows[i]["responsibility"].ToString();
                    vDatosReporte[5] = vDatos.Rows[i]["Satisfaccion"].ToString();
                    vDatosReporte[6] = vDatos.Rows[i]["idRazon"].ToString();
                    vDatosReporte[7] = vDatos.Rows[i]["comentario"].ToString();

                    String vXML = vMaestro.ObtenerCumplimientoRupturas(vDatosReporte);
                    vXML = vXML.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
                    String vQuery = "[STEISP_CUMPLIMIENTO_Reportes] 6, 0, 0" +
                        ",'" + vXML + "'";
                    int vInfo = vConexion.ejecutarSql(vQuery);
                }
            }catch (Exception ex){
                throw new Exception(ex.Message);
            }
        }

        private void insertarOSER(DataTable vDatos, int vId) {
            try{
                for (int i = 0; i < vDatos.Rows.Count; i++){
                    Object[] vDatosReporte = new object[6];
                    vDatosReporte[0] = vId.ToString();
                    vDatosReporte[1] = vDatos.Rows[i]["id"].ToString();
                    vDatosReporte[2] = vDatos.Rows[i]["tiempoRespuesta"].ToString();
                    vDatosReporte[3] = vDatos.Rows[i]["responsibility"].ToString();
                    vDatosReporte[4] = vDatos.Rows[i]["idRazon"];
                    vDatosReporte[5] = vDatos.Rows[i]["comentario"];
            
                    String vXML = vMaestro.ObtenerCumplimientoOSER(vDatosReporte);
                    vXML = vXML.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
                    String vQuery = "[STEISP_CUMPLIMIENTO_Reportes] 7, 0, 0" +
                        ",'" + vXML + "'";
                    int vInfo = vConexion.ejecutarSql(vQuery);
                }
            }catch (Exception ex){
                throw new Exception(ex.Message);
            }
        }

        private void insertarInsatisfacciones(DataTable vDatos, int vId) {
            try{
                for (int i = 0; i < vDatos.Rows.Count; i++){
                    Object[] vDatosReporte = new object[6];
                    vDatosReporte[0] = vId.ToString();
                    vDatosReporte[1] = vDatos.Rows[i]["id"].ToString();
                    vDatosReporte[2] = vDatos.Rows[i]["respuesta"].ToString();
                    vDatosReporte[3] = vDatos.Rows[i]["responsibility"].ToString();
                    vDatosReporte[4] = vDatos.Rows[i]["comments"];
                    vDatosReporte[5] = vDatos.Rows[i]["observaciones"];
            
                    String vXML = vMaestro.ObtenerCumplimientoSatisfaccion(vDatosReporte);
                    vXML = vXML.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
                    String vQuery = "[STEISP_CUMPLIMIENTO_Reportes] 10, 0, 0" +
                        ",'" + vXML + "'";
                    int vInfo = vConexion.ejecutarSql(vQuery);
                }
            }catch (Exception ex){
                throw new Exception(ex.Message);
            }
        }

        private void insertarRendimiento(DataTable vDatos, int vId) {
            try{
                for (int i = 0; i < vDatos.Rows.Count; i++){
                    Object[] vDatosReporte = new object[15];
                    vDatosReporte[0] = vId.ToString();
                    vDatosReporte[1] = vDatos.Rows[i]["idUsuario"].ToString();
                    vDatosReporte[2] = vDatos.Rows[i]["conocimientoProm"].ToString();
                    vDatosReporte[3] = vDatos.Rows[i]["tareas"].ToString();
                    vDatosReporte[4] = vDatos.Rows[i]["rupturas"].ToString();
                    vDatosReporte[5] = vDatos.Rows[i]["sinRupturaProm"].ToString().Replace("%","");
                    vDatosReporte[6] = vDatos.Rows[i]["satisfaccionProm"].ToString();
                    vDatosReporte[7] = vDatos.Rows[i]["produccion"].ToString().Replace("%","");
                    vDatosReporte[8] = vDatos.Rows[i]["eficiencia"].ToString().Replace("%","");
                    vDatosReporte[9] = vDatos.Rows[i]["total"].ToString().Replace("%","");
                    vDatosReporte[10] = vDatos.Rows[i]["eficienciaRup"].ToString().Replace("%","");
                    vDatosReporte[11] = vDatos.Rows[i]["eficienciaNoRup"].ToString().Replace("%","");
                    vDatosReporte[12] = vDatos.Rows[i]["tiempoReal"].ToString().Replace("%","");
                    vDatosReporte[13] = vDatos.Rows[i]["tiempoTransporte"].ToString().Replace("%","");
                    vDatosReporte[14] = vDatos.Rows[i]["comentario"].ToString();

                    String vXML = vMaestro.ObtenerCumplimientoUsuarios(vDatosReporte);
                    vXML = vXML.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
                    String vQuery = "[STEISP_CUMPLIMIENTO_Reportes] 8, 0, 0" +
                        ",'" + vXML + "'";
                    int vInfo = vConexion.ejecutarSql(vQuery);
                }
            }catch (Exception ex){
                throw new Exception(ex.Message);
            }
        }

        //RENDIMIENTOS
        private void promedioNotas(DataTable vData, int i) {
            Decimal vPromedio = Convert.ToDecimal(vData.Rows[i]["conocimiento"].ToString().Replace("%",""));
            if (vPromedio >= 90)
                vData.Rows[i]["conocimientoProm"] = vPromedio * 25 / 100;
            else
                vData.Rows[i]["conocimientoProm"] = vPromedio * (25 - (90 - vPromedio)) / 100;
        }
        
        private void obtenerTareas(DataTable vData, int i, String vFecha) {
            try{
                String vQuery2 = "[STEISP_CUMPLIMIENTO_Generales] 8" +
                    ",'" + vData.Rows[i]["AssignedGroup"].ToString() + "'" +
                    ",'" + vFecha + "'" +
                    ",'" + vData.Rows[i]["sysAid"].ToString() + "'";
                DataTable vRes = vConexion.obtenerDataTableSA(vQuery2);
                if (vRes.Rows.Count > 0){
                    vData.Rows[i]["tareas"] = vRes.Rows[0][0].ToString();
                }
            }catch (Exception ex){
                throw new Exception(ex.Message);
            }
        }

        private void obtenerRuptura(DataTable vData, int i, String vFecha) {
            try{
                String vQuery2 = "[STEISP_CUMPLIMIENTO_Generales] 10" +
                        ",'" + vData.Rows[i]["AssignedGroup"].ToString() + "'" + 
                        ",'" + vFecha + "'" +
                        ",'" + vData.Rows[i]["sysAid"].ToString() + "'";
                DataTable vRes = vConexion.obtenerDataTableSA(vQuery2);
                int vCantRup = 0, vNoRup = 0, vTotalRup = 0;
                if (vRes.Rows.Count > 0){
                    for (int j = 0; j < vRes.Rows.Count; j++){
                        Decimal vAtencion = Convert.ToDecimal(vRes.Rows[j]["Atencion"].ToString());
                        Decimal vAsignacion = Convert.ToDecimal(vRes.Rows[j]["Asignacion"].ToString());
                        Decimal vNuevoAtencion = 0;

                        vNuevoAtencion = vAsignacion <= 60 ? vAtencion - (60 - vAsignacion) : vAtencion;
                        if (vNuevoAtencion > 180)
                            vCantRup++;
                        else 
                            vNoRup++;
                    }
                }
                vQuery2 = "[STEISP_CUMPLIMIENTO_Generales] 11" +
                        ",'" + vData.Rows[i]["AssignedGroup"].ToString() + "'" +
                        ",'" + vFecha + "'" +
                        ",'" + vData.Rows[i]["sysAid"].ToString() + "'";
                vRes = vConexion.obtenerDataTableSA(vQuery2);
                if (vRes.Rows.Count > 0){
                    for (int j = 0; j < vRes.Rows.Count; j++){
                        Decimal vAtencion = Convert.ToDecimal(vRes.Rows[j]["Atencion"].ToString());
                        Decimal vAsignacion = Convert.ToDecimal(vRes.Rows[j]["Asignacion"].ToString());
                        Decimal vNuevoAtencion = 0;

                        vNuevoAtencion = vAsignacion <= 60 ? vAtencion - (60 - vAsignacion) : vAtencion;
                        if (vNuevoAtencion > 420)
                            vCantRup++;
                        else
                            vNoRup++;
                    }
                }
                vQuery2 = "[STEISP_CUMPLIMIENTO_Generales] 12" +
                        ",'" + vData.Rows[i]["AssignedGroup"].ToString() + "'" +
                        ",'" + vFecha + "'" +
                        ",'" + vData.Rows[i]["sysAid"].ToString() + "'";
                vRes = vConexion.obtenerDataTableSA(vQuery2);
                if (vRes.Rows.Count > 0){
                    for (int j = 0; j < vRes.Rows.Count; j++){
                        Decimal vAtencion = Convert.ToDecimal(vRes.Rows[j]["Atencion"].ToString());
                        Decimal vAsignacion = Convert.ToDecimal(vRes.Rows[j]["Asignacion"].ToString());
                        Decimal vNuevoAtencion = 0;

                        vNuevoAtencion = vAsignacion <= 60 ? vAtencion - (60 - vAsignacion) : vAtencion;
                        if (vNuevoAtencion > 900)
                            vCantRup++;
                        else
                            vNoRup++;
                    }
                }
                vQuery2 = "[STEISP_CUMPLIMIENTO_Generales] 13" +
                        ",'" + vData.Rows[i]["AssignedGroup"].ToString() + "'" +
                        ",'" + vFecha + "'" +
                        ",'" + vData.Rows[i]["sysAid"].ToString() + "'";
                vRes = vConexion.obtenerDataTableSA(vQuery2);
                if (vRes.Rows.Count > 0){
                    for (int j = 0; j < vRes.Rows.Count; j++){
                        Decimal vAtencion = Convert.ToDecimal(vRes.Rows[j]["Atencion"].ToString());
                        Decimal vAsignacion = Convert.ToDecimal(vRes.Rows[j]["Asignacion"].ToString());
                        Decimal vNuevoAtencion = 0;

                        vNuevoAtencion = vAsignacion <= 60 ? vAtencion - (60 - vAsignacion) : vAtencion;
                        if (vNuevoAtencion > 240)
                            vCantRup++;
                        else
                            vNoRup++;
                    }
                }
                vData.Rows[i]["rupturas"] = vCantRup;
                float sinRupturaProm = 0;
                int vTareas = Convert.ToInt32(vData.Rows[i]["tareas"].ToString());
                if (vTareas >= 1 && vCantRup == 0 )
                    sinRupturaProm = 100;
                else if (vCantRup == 0 && vTareas == 0 )
                    sinRupturaProm = 0;        
                else if (vCantRup >= 1 && vTareas >= 1 )
                    sinRupturaProm = 100- vCantRup * 100 / vTareas;
                
                vData.Rows[i]["sinRupturaProm"] = sinRupturaProm.ToString() + "%";
            }catch (Exception ex){
                throw new Exception(ex.Message);
            }
        }

        private void obtenerOpinion(DataTable vData, int i, String vFecha) {
            try{
                String vQuery2 = "[STEISP_CUMPLIMIENTO_Generales] 9" +
                        ",'" + vData.Rows[i]["sysAid"].ToString() + "'" + 
                        ",'" + vFecha + "'";
                DataTable vRes = vConexion.obtenerDataTableSA(vQuery2);
                if (vRes.Rows.Count > 0){
                    Decimal vSatisfaccion = 0;
                    float vSat = 0;
                    if (vRes.Rows[0]["respuestas"].ToString() != "0" && vRes.Rows[0]["totalFilas"].ToString() != "0"){
                        vSat = float.Parse(vRes.Rows[0]["respuestas"].ToString()) / float.Parse(vRes.Rows[0]["totalFilas"].ToString());
                        vSatisfaccion = Convert.ToDecimal(Math.Round(vSat / 5 * 100, 2));
                    }
                    int vRango1 = 91, vRango2 = 85, vRango3 = 75;
                    int valor1 = 85, valor2 = 50, valor3 = 0;

                    if (vSatisfaccion >= vRango1 ){
                        vSatisfaccion = vSatisfaccion;
                    }else if (vSatisfaccion >= vRango2 && vSatisfaccion < vRango1 ){
                        vSatisfaccion = valor1;
                    }else if (vSatisfaccion >= vRango3 && vSatisfaccion < vRango2){
                        vSatisfaccion = valor2;
                    }else if (vSatisfaccion < vRango3) {
                        vSatisfaccion = valor3;
                    }
                    vData.Rows[i]["satisfaccion"] = vSatisfaccion.ToString() + "%";
                    vData.Rows[i]["satisfaccionProm"] = vSatisfaccion.ToString();
                }
            }catch (Exception ex){
                throw new Exception(ex.Message);
            }
        }

        private void obtenerProductividad(DataTable vData, int i, String vFecha) {
            try{
                String vQuery2 = "[STEISP_CUMPLIMIENTO_Generales] 14" +
                        ",'" + vData.Rows[i]["AssignedGroup"].ToString() + "'" +
                        ",'" + vFecha + "'" +
                        ",'" + vData.Rows[i]["sysAid"].ToString() + "'";
                DataTable vRes = vConexion.obtenerDataTableSA(vQuery2);
                if (vRes.Rows.Count > 0){
                    int SumTiempoReal = Convert.ToInt32(vRes.Rows[0]["tiempoReal"].ToString());
                    int SumTiempoTransporte = Convert.ToInt32(vRes.Rows[0]["tiempoTransporte"].ToString());
                    int TotalTiempoInvertido = SumTiempoReal + SumTiempoTransporte;

                    float vPromedioProductividad = float.Parse(TotalTiempoInvertido.ToString()) / 360 * 100;
                    Decimal vPromedioTotal = Convert.ToDecimal(Math.Round(vPromedioProductividad, 2));
                    
                    String vTotal = "";
                    if (vPromedioTotal >= 1 && vPromedioTotal < 100)
                        vTotal = vPromedioTotal.ToString() + "%";
                    else if (vPromedioTotal >= 100)
                        vTotal = "100%";
                    else 
                        vTotal = "0%";

                    vData.Rows[i]["produccion"] = vTotal;
                    vData.Rows[i]["tiempoReal"] = SumTiempoReal;
                    vData.Rows[i]["tiempoTransporte"] = SumTiempoTransporte;
                }

            }catch (Exception ex){
                throw new Exception(ex.Message);
            }
        }

        private void obtenerEficiencia(DataTable vData, int i, String vFecha){
            try{
                String Categoria1 = "", Categoria2 = "", Categoria3 = "";
                int TimeReal = 0;
                String vQuery = "[STEISP_CUMPLIMIENTO_Generales] 7" +
                            ",'" + vData.Rows[i]["AssignedGroup"].ToString() + "'" +
                            ",'" + vFecha + "'" +
                            ",'" + vData.Rows[i]["sysAid"].ToString() + "'";
                DataTable vDatosSA = vConexion.obtenerDataTableSA(vQuery);
                int RupEficiencia = 0, NoRupEficiencia = 0;
                if (vDatosSA.Rows.Count > 0){
                    for (int j = 0; j < vDatosSA.Rows.Count; j++){
                        Categoria1 = vDatosSA.Rows[j]["problem_type"].ToString();
                        Categoria2 = vDatosSA.Rows[j]["problem_sub_type"].ToString();
                        Categoria3 = vDatosSA.Rows[j]["third_level_category"].ToString();
                        TimeReal = Convert.ToInt32(vDatosSA.Rows[j]["cust_int1"].ToString());

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
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Office 365" && Categoria3 == "migración de cuenta de correo" && TimeReal > 20)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "OPen text" && Categoria3 == "Instalación" && TimeReal > 30)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "OPen text" && Categoria3 == "revisión" && TimeReal > 30)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Paris" && Categoria3 == " " && TimeReal > 20)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Paris" && Categoria3 == "acceso" && TimeReal > 20)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Paris" && Categoria3 == "mantenimientos" && TimeReal > 20)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "PC" && Categoria3 == "Instalacion programas" && TimeReal > 20)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Planilla Inprema" && Categoria3 == "Problema impresion planilla" && TimeReal > 20)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "SAP" && Categoria3 == "Instalación" && TimeReal > 40)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "SAP" && Categoria3 == "Problemas de conexión" && TimeReal > 40)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Single Sign On" && Categoria3 == "Instalación" && TimeReal > 50)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Single Sign On" && Categoria3 == "Instalación" && TimeReal > 50)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Single Sign On" && Categoria3 == "revisión" && TimeReal > 50)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Sistema de colas qflow" && Categoria3 == "Instalación" && TimeReal > 50)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "SysAid" && Categoria3 == " " && TimeReal > 20)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "SYSCARD" && Categoria3 == "USUARIO" && TimeReal > 20)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Telesoft" && Categoria3 == " " && TimeReal > 30)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Telesoft" && Categoria3 == "Activar Clave" && TimeReal > 20)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Telesoft" && Categoria3 == "documentos" && TimeReal > 20)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "usuario mcafee cifrado" && Categoria3 == "agregar / eliminar usuario" && TimeReal > 20)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "usuario mcafee cifrado" && Categoria3 == "habilitación maquina" && TimeReal > 20)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Virus" && Categoria3 == " " && TimeReal > 20)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Virus" && Categoria3 == "Estudio de maquina con virus" && TimeReal > 30)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Virus" && Categoria3 == "Instalacion Antivirus" && TimeReal > 20)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Virus" && Categoria3 == "Revision Antivirus" && TimeReal > 20)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Webex" && Categoria3 == "reseteo de contraseña" && TimeReal > 20)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Windows" && Categoria3 == " " && TimeReal > 20)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Windows" && Categoria3 == "accesos" && TimeReal > 20)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Agentes bancarios" && Categoria3 == " " && TimeReal > 20)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Agentes bancarios" && Categoria3 == "cambio de chip" && TimeReal > 30)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Agentes bancarios" && Categoria3 == "Cambio de equipo" && TimeReal > 30)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Agentes bancarios" && Categoria3 == "Instalacion" && TimeReal > 20)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Agentes bancarios" && Categoria3 == "Mantenimiento" && TimeReal > 20)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Agentes bancarios" && Categoria3 == "Problemas de Software" && TimeReal > 20)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "ATM" && Categoria3 == "Lectora de Tarjetas" && TimeReal > 80)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "ATM" && Categoria3 == "Mantenimiento Preventivo Programado" && TimeReal > 180)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "ATM" && Categoria3 == "No enciende" && TimeReal > 50)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "ATM" && Categoria3 == "Offline - Out of service" && TimeReal > 50)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "ATM" && Categoria3 == "Otras Solicitudes" && TimeReal > 50)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "ATM" && Categoria3 == "Problemas de comunicación" && TimeReal > 50)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "ATM" && Categoria3 == "Revision de UPS" && TimeReal > 30)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "ATM" && Categoria3 == "Revision EDC" && TimeReal > 20)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "CPU" && Categoria3 == "Instalacion Maquina Contingencia" && TimeReal > 30)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "CPU" && Categoria3 == "Instalación" && TimeReal > 30)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "CPU" && Categoria3 == "Recuperacion Maquina Contingenci" && TimeReal > 30)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "CPU" && Categoria3 == "Reemplazo" && TimeReal > 30)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "CPU" && Categoria3 == "Reparación" && TimeReal > 30)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Impresoras" && Categoria3 == "configuracion" && TimeReal > 30)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Impresoras" && Categoria3 == "Instalación" && TimeReal > 30)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Impresoras" && Categoria3 == "Reemplazo" && TimeReal > 30)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Impresoras" && Categoria3 == "Reparación" && TimeReal > 30)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos colas agencias" && Categoria3 == "Instalación" && TimeReal > 40)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos colas agencias" && Categoria3 == "Reparación" && TimeReal > 40)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos colas agencias" && Categoria3 == "Actualización" && TimeReal > 50)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos medios de pago" && Categoria3 == "Impresora" && TimeReal > 30)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos medios de pago" && Categoria3 == "Instalación" && TimeReal > 40)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos medios de pago" && Categoria3 == "Lectora de Tarjeta" && TimeReal > 30)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos medios de pago" && Categoria3 == "Monitor" && TimeReal > 20)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos medios de pago" && Categoria3 == "Offline" && TimeReal > 30)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos medios de pago" && Categoria3 == "Problemas con Transacciones Caja Empresarial" && TimeReal > 30)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos medios de pago" && Categoria3 == "Problemas de Comunicación" && TimeReal > 30)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos medios de pago" && Categoria3 == "Problemas de Software" && TimeReal > 40)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos medios de pago" && Categoria3 == "Revisión" && TimeReal > 30)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos medios de pago" && Categoria3 == "Revisión de UPS" && TimeReal > 20)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Mantenimiento Preventivo" && Categoria3 == "Agencia Completa" && TimeReal > 90)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Mantenimiento Preventivo" && Categoria3 == "DATACARD" && TimeReal > 30)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Mantenimiento Preventivo" && Categoria3 == "Kiosco transaccional" && TimeReal > 40)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Mantenimiento Preventivo" && Categoria3 == "Piso Data Center" && TimeReal > 40)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Monitores" && Categoria3 == "Instalación" && TimeReal > 20)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Monitores" && Categoria3 == "Reemplazo" && TimeReal > 20)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Monitores" && Categoria3 == "Reparación" && TimeReal > 30)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Multi-ATM" && Categoria3 == "Depositos" && TimeReal > 40)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Multi-ATM" && Categoria3 == "Dispensador" && TimeReal > 40)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Multi-ATM" && Categoria3 == "Equipo Pegado" && TimeReal > 40)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Multi-ATM" && Categoria3 == "Lectora" && TimeReal > 40)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Multi-ATM" && Categoria3 == "Pagos tarjetas" && TimeReal > 40)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Perifericos" && Categoria3 == " " && TimeReal > 20)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Perifericos" && Categoria3 == "Instalación" && TimeReal > 20)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Perifericos" && Categoria3 == "Reemplazo" && TimeReal > 20)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Relojes biometricos" && Categoria3 == "instalación" && TimeReal > 30)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Relojes biometricos" && Categoria3 == "Reemplazo" && TimeReal > 30)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Relojes biometricos" && Categoria3 == "Revisión" && TimeReal > 30)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Scanners" && Categoria3 == " " && TimeReal > 30)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Scanners" && Categoria3 == "Instalación" && TimeReal > 30)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Scanners" && Categoria3 == "Reemplazo" && TimeReal > 30)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Scanners" && Categoria3 == "Reparación" && TimeReal > 30)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "UPS" && Categoria3 == " " && TimeReal > 30)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "UPS" && Categoria3 == "Instalación" && TimeReal > 30)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "UPS" && Categoria3 == "Reemplazo" && TimeReal > 30)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Call center" && Categoria3 == "estadísticas" && TimeReal > 20)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Call center" && Categoria3 == "evaluaciones agentes" && TimeReal > 90)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Call center" && Categoria3 == "nuevo agente" && TimeReal > 90)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Call center" && Categoria3 == "problemas de grabación" && TimeReal > 40)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Call center" && Categoria3 == "workforce" && TimeReal > 65)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Enlaces de Datos" && Categoria3 == " " && TimeReal > 40)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Enlaces de Datos" && Categoria3 == "Caida de agencia" && TimeReal > 40)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Enlaces de Datos" && Categoria3 == "Intermitencia" && TimeReal > 40)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Enlaces de Datos" && Categoria3 == "Revisión de redundancia" && TimeReal > 40)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Equipos de Comunicacion" && Categoria3 == " " && TimeReal > 40)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Equipos de Comunicacion" && Categoria3 == "Configuración" && TimeReal > 40)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Equipos de Comunicacion" && Categoria3 == "Documentación" && TimeReal > 30)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Equipos de Comunicacion" && Categoria3 == "Instalación" && TimeReal > 40)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Equipos de Comunicacion" && Categoria3 == "Mantenimiento Preventivo" && TimeReal > 30)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Equipos de Comunicacion" && Categoria3 == "Revisión" && TimeReal > 40)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Monitoreo" && Categoria3 == "Monitoreo de interfaces caidas" && TimeReal > 30)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Redes" && Categoria3 == " " && TimeReal > 20)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Redes" && Categoria3 == "802.1X" && TimeReal > 20)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Redes" && Categoria3 == "Conexion de DVR ATM" && TimeReal > 20)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Redes" && Categoria3 == "Instalación punto de red" && TimeReal > 40)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Redes" && Categoria3 == "Permisos de Red" && TimeReal > 20)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Redes" && Categoria3 == "Reparación de punto de red" && TimeReal > 20)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Redes" && Categoria3 == "Solicitud de acceso a internet Wireless" && TimeReal > 40)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Redes" && Categoria3 == "VPN" && TimeReal > 40)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Remesas" && Categoria3 == "Revision de Comunicacion" && TimeReal > 60)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Telefonia" && Categoria3 == "Configuracion" && TimeReal > 20)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Telefonia" && Categoria3 == "Generación de clave de telefono" && TimeReal > 20)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Telefonia" && Categoria3 == "Instalación" && TimeReal > 30)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Telefonia" && Categoria3 == "intermitencia" && TimeReal > 40)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Telefonia" && Categoria3 == "Otros" && TimeReal > 30)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Telefonia" && Categoria3 == "Pickup group" && TimeReal > 30)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Telefonia" && Categoria3 == "Planta IP Plaza Bancatlan" && TimeReal > 40)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Telefonia" && Categoria3 == "Reparación" && TimeReal > 40)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Telefonia" && Categoria3 == "Softphone Telesoft" && TimeReal > 30)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Test" && Categoria3 == "test" && TimeReal > 30)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Wireless" && Categoria3 == "Creación de usuario externo" && TimeReal > 20)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Wireless" && Categoria3 == "Creación de usuario interno" && TimeReal > 20)
                            RupEficiencia = RupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Active Directory" && Categoria3 == " " && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Active Directory" && Categoria3 == "Desbloqueo Usuario" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Active Directory" && Categoria3 == "Incorporación al dominio" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Active Directory" && Categoria3 == "Personalizacion de usuario" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "actualizaciones" && Categoria3 == "servidores" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "antivirus" && Categoria3 == "revisar o actualizar" && TimeReal <= 40)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "ATMS" && Categoria3 == "errores" && TimeReal <= 30)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Caja Empresarial" && Categoria3 == " " && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Camara Compensación" && Categoria3 == " " && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Camara Compensación" && Categoria3 == "Configuracion Cliente de Oracle" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Camara Compensación" && Categoria3 == "Configuracion de Minilectoras" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Camara Compensación" && Categoria3 == "Configuracion de Minilectoras" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Camara Compensación" && Categoria3 == "Configuracion DIPS" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Camara Compensación" && Categoria3 == "Configuracion IMS" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Camara Compensación" && Categoria3 == "Configurar Camara Entrante" && TimeReal <= 40)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Camara Compensación" && Categoria3 == "Configurar Camara Saliente" && TimeReal <= 40)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Camara Compensación" && Categoria3 == "Configurar Transacciones de Dips" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Camara Compensación" && Categoria3 == "Crear ODBC para Cliente Access" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Capturadores de la CNBS" && Categoria3 == " " && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Capturadores de la CNBS" && Categoria3 == "Capturador de Creditos" && TimeReal <= 30)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Capturadores de la CNBS" && Categoria3 == "Contable" && TimeReal <= 30)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Credit Scoring" && Categoria3 == " " && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Docuware" && Categoria3 == " " && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Docuware" && Categoria3 == "no puede ingresar" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "equipo" && Categoria3 == "preparar equipo" && TimeReal <= 60)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "equipo" && Categoria3 == "revisar equipo" && TimeReal <= 60)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Fenix" && Categoria3 == "Instalacion" && TimeReal <= 40)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Fenix" && Categoria3 == "Personalizacion del Usuario" && TimeReal <= 30)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "IBS" && Categoria3 == " " && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "IBS" && Categoria3 == "Acceso a IBS" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "IBS" && Categoria3 == "Correccion en Cta de ahorros" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "IBS" && Categoria3 == "estados de cuenta" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "IBS" && Categoria3 == "Problema de impresión" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "IBS" && Categoria3 == "Reportes Cierre IBS" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "IBS" && Categoria3 == "SSL" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "ICS" && Categoria3 == " " && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "ICS" && Categoria3 == "Cambio de Moneda" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "ICS" && Categoria3 == "Instalación" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "ICS" && Categoria3 == "Problemas con JAVA" && TimeReal <= 30)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "ICS" && Categoria3 == "Revisión" && TimeReal <= 30)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Jteller" && Categoria3 == " " && TimeReal <= 30)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Jteller" && Categoria3 == "Activar Agencia" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Jteller" && Categoria3 == "Activar Cajero" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Jteller" && Categoria3 == "Activar Icono de Monitor" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Jteller" && Categoria3 == "Agencia OFFLINE" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Jteller" && Categoria3 == "Cajero Atrapado en IBS" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Jteller" && Categoria3 == "cambio a server de ventanillas" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Jteller" && Categoria3 == "Cambio de agencia" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Jteller" && Categoria3 == "Cambio de Password" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Lotus Notes" && Categoria3 == "Archivado" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Lotus Notes" && Categoria3 == "Cambio de Contraseña" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Lotus Notes" && Categoria3 == "Correo Externo" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Lotus Notes" && Categoria3 == "Creacion Web" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Lotus Notes" && Categoria3 == "Cuotas llenas" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Lotus Notes" && Categoria3 == "Instalación" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Lotus Notes" && Categoria3 == "mantenimientos" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Lotus Notes" && Categoria3 == "Modificar cuota" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Lotus Notes" && Categoria3 == "no se puede abrir el correo" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Lotus Notes" && Categoria3 == "Reinstalación" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Lotus Symphony" && Categoria3 == " " && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Microsoft Office" && Categoria3 == " " && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Microsoft Office" && Categoria3 == "Instalacion" && TimeReal <= 40)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Microsoft Office" && Categoria3 == "autorizacion" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "monitoreos" && Categoria3 == "solicitud de permisos" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Navegacion Internet" && Categoria3 == " " && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "PC" && Categoria3 == "Instalacion de programas" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "PC" && Categoria3 == "Instalacion de programas" && TimeReal <= 60)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Office 365" && Categoria3 == "Configuración de Outlook" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Office 365" && Categoria3 == "migración de archivado" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Office 365" && Categoria3 == "migración de cuenta de correo" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "OPen text" && Categoria3 == "Instalación" && TimeReal <= 30)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "OPen text" && Categoria3 == "revisión" && TimeReal <= 30)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Paris" && Categoria3 == " " && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Paris" && Categoria3 == "acceso" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Paris" && Categoria3 == "mantenimientos" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "PC" && Categoria3 == "Instalacion programas" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Planilla Inprema" && Categoria3 == "Problema impresion planilla" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "SAP" && Categoria3 == "Instalación" && TimeReal <= 40)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "SAP" && Categoria3 == "Problemas de conexión" && TimeReal <= 40)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Single Sign On" && Categoria3 == "Instalación" && TimeReal <= 50)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Single Sign On" && Categoria3 == "Instalación" && TimeReal <= 50)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Single Sign On" && Categoria3 == "revisión" && TimeReal <= 50)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Sistema de colas qflow" && Categoria3 == "Instalación" && TimeReal <= 50)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "SysAid" && Categoria3 == " " && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "SYSCARD" && Categoria3 == "USUARIO" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Telesoft" && Categoria3 == " " && TimeReal <= 30)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Telesoft" && Categoria3 == "Activar Clave" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Telesoft" && Categoria3 == "documentos" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "usuario mcafee cifrado" && Categoria3 == "agregar / eliminar usuario" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "usuario mcafee cifrado" && Categoria3 == "habilitación maquina" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Virus" && Categoria3 == " " && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Virus" && Categoria3 == "Estudio de maquina con virus" && TimeReal <= 30)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Virus" && Categoria3 == "Instalacion Antivirus" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Virus" && Categoria3 == "Revision Antivirus" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Webex" && Categoria3 == "reseteo de contraseña" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Windows" && Categoria3 == " " && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Software" && Categoria2 == "Windows" && Categoria3 == "accesos" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Agentes bancarios" && Categoria3 == " " && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Agentes bancarios" && Categoria3 == "cambio de chip" && TimeReal <= 30)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Agentes bancarios" && Categoria3 == "Cambio de equipo" && TimeReal <= 30)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Agentes bancarios" && Categoria3 == "Instalacion" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Agentes bancarios" && Categoria3 == "Mantenimiento" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Agentes bancarios" && Categoria3 == "Problemas de Software" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "ATM" && Categoria3 == "Lectora de Tarjetas" && TimeReal <= 80)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "ATM" && Categoria3 == "Mantenimiento Preventivo Programado" && TimeReal <= 180)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "ATM" && Categoria3 == "No enciende" && TimeReal <= 50)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "ATM" && Categoria3 == "Offline - Out of service" && TimeReal <= 50)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "ATM" && Categoria3 == "Otras Solicitudes" && TimeReal <= 50)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "ATM" && Categoria3 == "Problemas de comunicación" && TimeReal <= 50)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "ATM" && Categoria3 == "Revision de UPS" && TimeReal <= 30)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "ATM" && Categoria3 == "Revision EDC" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "CPU" && Categoria3 == "Instalacion Maquina Contingencia" && TimeReal <= 30)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "CPU" && Categoria3 == "Instalación" && TimeReal <= 30)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "CPU" && Categoria3 == "Recuperacion Maquina Contingenci" && TimeReal <= 30)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "CPU" && Categoria3 == "Reemplazo" && TimeReal <= 30)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "CPU" && Categoria3 == "Reparación" && TimeReal <= 30)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Impresoras" && Categoria3 == "configuracion" && TimeReal <= 30)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Impresoras" && Categoria3 == "Instalación" && TimeReal <= 30)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Impresoras" && Categoria3 == "Reemplazo" && TimeReal <= 30)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Impresoras" && Categoria3 == "Reparación" && TimeReal <= 30)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos colas agencias" && Categoria3 == "Instalación" && TimeReal <= 40)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos colas agencias" && Categoria3 == "Reparación" && TimeReal <= 40)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos colas agencias" && Categoria3 == "Actualización" && TimeReal <= 50)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos medios de pago" && Categoria3 == "Impresora" && TimeReal <= 30)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos medios de pago" && Categoria3 == "Instalación" && TimeReal <= 40)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos medios de pago" && Categoria3 == "Lectora de Tarjeta" && TimeReal <= 30)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos medios de pago" && Categoria3 == "Monitor" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos medios de pago" && Categoria3 == "Offline" && TimeReal <= 30)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos medios de pago" && Categoria3 == "Problemas con Transacciones Caja Empresarial" && TimeReal <= 30)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos medios de pago" && Categoria3 == "Problemas de Comunicación" && TimeReal <= 30)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos medios de pago" && Categoria3 == "Problemas de Software" && TimeReal <= 40)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos medios de pago" && Categoria3 == "Revisión" && TimeReal <= 30)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Kioscos medios de pago" && Categoria3 == "Revisión de UPS" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Mantenimiento Preventivo" && Categoria3 == "Agencia Completa" && TimeReal <= 90)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Mantenimiento Preventivo" && Categoria3 == "DATACARD" && TimeReal <= 30)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Mantenimiento Preventivo" && Categoria3 == "Kiosco transaccional" && TimeReal <= 40)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Mantenimiento Preventivo" && Categoria3 == "Piso Data Center" && TimeReal <= 40)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Monitores" && Categoria3 == "Instalación" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Monitores" && Categoria3 == "Reemplazo" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Monitores" && Categoria3 == "Reparación" && TimeReal <= 30)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Multi-ATM" && Categoria3 == "Depositos" && TimeReal <= 40)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Multi-ATM" && Categoria3 == "Dispensador" && TimeReal <= 40)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Multi-ATM" && Categoria3 == "Equipo Pegado" && TimeReal <= 40)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Multi-ATM" && Categoria3 == "Lectora" && TimeReal <= 40)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Multi-ATM" && Categoria3 == "Pagos tarjetas" && TimeReal <= 40)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Perifericos" && Categoria3 == " " && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Perifericos" && Categoria3 == "Instalación" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Perifericos" && Categoria3 == "Reemplazo" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Relojes biometricos" && Categoria3 == "instalación" && TimeReal <= 30)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Relojes biometricos" && Categoria3 == "Reemplazo" && TimeReal <= 30)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Relojes biometricos" && Categoria3 == "Revisión" && TimeReal <= 30)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Scanners" && Categoria3 == " " && TimeReal <= 30)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Scanners" && Categoria3 == "Instalación" && TimeReal <= 30)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Scanners" && Categoria3 == "Reemplazo" && TimeReal <= 30)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "Scanners" && Categoria3 == "Reparación" && TimeReal <= 30)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "UPS" && Categoria3 == " " && TimeReal <= 30)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "UPS" && Categoria3 == "Instalación" && TimeReal <= 30)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Hardware" && Categoria2 == "UPS" && Categoria3 == "Reemplazo" && TimeReal <= 30)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Call center" && Categoria3 == "estadísticas" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Call center" && Categoria3 == "evaluaciones agentes" && TimeReal <= 90)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Call center" && Categoria3 == "nuevo agente" && TimeReal <= 90)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Call center" && Categoria3 == "problemas de grabación" && TimeReal <= 40)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Call center" && Categoria3 == "workforce" && TimeReal <= 65)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Enlaces de Datos" && Categoria3 == " " && TimeReal <= 40)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Enlaces de Datos" && Categoria3 == "Caida de agencia" && TimeReal <= 40)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Enlaces de Datos" && Categoria3 == "Intermitencia" && TimeReal <= 40)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Enlaces de Datos" && Categoria3 == "Revisión de redundancia" && TimeReal <= 40)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Equipos de Comunicacion" && Categoria3 == " " && TimeReal <= 40)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Equipos de Comunicacion" && Categoria3 == "Configuración" && TimeReal <= 40)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Equipos de Comunicacion" && Categoria3 == "Documentación" && TimeReal <= 30)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Equipos de Comunicacion" && Categoria3 == "Instalación" && TimeReal <= 40)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Equipos de Comunicacion" && Categoria3 == "Mantenimiento Preventivo" && TimeReal <= 30)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Equipos de Comunicacion" && Categoria3 == "Revisión" && TimeReal <= 40)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Monitoreo" && Categoria3 == "Monitoreo de interfaces caidas" && TimeReal <= 30)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Redes" && Categoria3 == " " && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Redes" && Categoria3 == "802.1X" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Redes" && Categoria3 == "Conexion de DVR ATM" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Redes" && Categoria3 == "Instalación punto de red" && TimeReal <= 40)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Redes" && Categoria3 == "Permisos de Red" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Redes" && Categoria3 == "Reparación de punto de red" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Redes" && Categoria3 == "Solicitud de acceso a internet Wireless" && TimeReal <= 40)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Redes" && Categoria3 == "VPN" && TimeReal <= 40)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Remesas" && Categoria3 == "Revision de Comunicacion" && TimeReal <= 60)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Telefonia" && Categoria3 == "Configuracion" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Telefonia" && Categoria3 == "Generación de clave de telefono" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Telefonia" && Categoria3 == "Instalación" && TimeReal <= 30)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Telefonia" && Categoria3 == "intermitencia" && TimeReal <= 40)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Telefonia" && Categoria3 == "Otros" && TimeReal <= 30)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Telefonia" && Categoria3 == "Pickup group" && TimeReal <= 30)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Telefonia" && Categoria3 == "Planta IP Plaza Bancatlan" && TimeReal <= 40)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Telefonia" && Categoria3 == "Reparación" && TimeReal <= 40)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Telefonia" && Categoria3 == "Softphone Telesoft" && TimeReal <= 30)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Test" && Categoria3 == "test" && TimeReal <= 30)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Wireless" && Categoria3 == "Creación de usuario externo" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (Categoria1 == "Comunicaciones" && Categoria2 == "Wireless" && Categoria3 == "Creación de usuario interno" && TimeReal <= 20)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (TimeReal > 30)
                            NoRupEficiencia = NoRupEficiencia + 1;
                        else if (TimeReal <= 30)
                            NoRupEficiencia = NoRupEficiencia + 1;
                    }
                }
                float vEficienciaProm1 = vDatosSA.Rows.Count > 0 ? (vDatosSA.Rows.Count - float.Parse(RupEficiencia.ToString())) / vDatosSA.Rows.Count * 100 : 0;
                Decimal vEficienciaProm = Convert.ToDecimal(Math.Round(vEficienciaProm1, 2));
                vData.Rows[i]["eficiencia"] = vEficienciaProm >= 1 ? vEficienciaProm.ToString() + "%" : "0%";
                vData.Rows[i]["eficienciaProm"] = vEficienciaProm >= 1 ? vEficienciaProm.ToString() : "0";
                vData.Rows[i]["eficienciaRup"] = RupEficiencia.ToString();
                vData.Rows[i]["eficienciaNoRup"] = NoRupEficiencia.ToString();

            }catch (Exception ex){
                throw new Exception(ex.Message);
            }
        }

        private void obtenerTotal(DataTable vData, int i, String vFecha){
            try{
                Boolean vForaneo = false;
                float vTotal, RG_Ruptura, RG_Eficiencia, RG_Productividad, RG_Satisfaccion;
                vForaneo = Convert.ToBoolean(vData.Rows[i]["flagForaneo"].ToString()) == true ? true : false;
                
                String vAssignedGroup = vData.Rows[i]["AssignedGroup"].ToString();
                float vSatProm =  float.Parse(vData.Rows[i]["satisfaccionProm"].ToString());
                if (vForaneo){
                    if (vSatProm > 1){
                        RG_Satisfaccion = vSatProm * float.Parse("0,20");
                        RG_Ruptura = float.Parse(vData.Rows[i]["sinRupturaProm"].ToString().Replace("%", "")) * float.Parse("0,30");
                        RG_Eficiencia = float.Parse(vData.Rows[i]["eficienciaProm"].ToString()) * float.Parse("0,15");
                        RG_Productividad = float.Parse(vData.Rows[i]["produccion"].ToString().Replace("%", "")) * float.Parse("0,10");
                        vTotal = RG_Satisfaccion + float.Parse(vData.Rows[i]["conocimientoProm"].ToString()) + RG_Ruptura + RG_Eficiencia + RG_Productividad;
                    }else{
                        RG_Ruptura = float.Parse(vData.Rows[i]["sinRupturaProm"].ToString().Replace("%", "")) * float.Parse("0,40");
                        RG_Eficiencia = float.Parse(vData.Rows[i]["eficienciaProm"].ToString()) * float.Parse("0,20");
                        RG_Productividad = float.Parse(vData.Rows[i]["produccion"].ToString().Replace("%", "")) * float.Parse("0,15");
                        vTotal = float.Parse(vData.Rows[i]["conocimientoProm"].ToString()) + RG_Ruptura + RG_Eficiencia + RG_Productividad;
                    }
                    vData.Rows[i]["total"] = (Convert.ToDecimal(Math.Round(vTotal, 2))).ToString() + "%";

                }else{
                    if (vAssignedGroup == "Infa-COMUNICACIONES"){
                        if (vSatProm > 1){
                            RG_Satisfaccion = vSatProm * float.Parse("0,20");
                            RG_Ruptura = float.Parse(vData.Rows[i]["sinRupturaProm"].ToString().Replace("%","")) * float.Parse("0,30");
                            RG_Eficiencia = float.Parse(vData.Rows[i]["eficienciaProm"].ToString()) * float.Parse("0,15");
                            RG_Productividad = float.Parse(vData.Rows[i]["produccion"].ToString().Replace("%","")) * float.Parse("0,10");
                            vTotal = RG_Satisfaccion + float.Parse(vData.Rows[i]["conocimientoProm"].ToString()) + RG_Ruptura + RG_Eficiencia + RG_Productividad;
                        }else{
                            RG_Ruptura = float.Parse(vData.Rows[i]["sinRupturaProm"].ToString().Replace("%", "")) * float.Parse("0,40");
                            RG_Eficiencia = float.Parse(vData.Rows[i]["eficienciaProm"].ToString()) * float.Parse("0,20");
                            RG_Productividad = float.Parse(vData.Rows[i]["produccion"].ToString().Replace("%", "")) * float.Parse("0,15");
                            vTotal = float.Parse(vData.Rows[i]["conocimientoProm"].ToString()) + RG_Ruptura + RG_Eficiencia + RG_Productividad;
                        }
                        vData.Rows[i]["total"] = (Convert.ToDecimal(Math.Round(vTotal, 2))).ToString() + "%";

                    }else{
                        float RG_Llamadas = float.Parse(TxCallPorcentajeSi.Text) * float.Parse("0,10");
                        if (vSatProm > 1){
                            RG_Satisfaccion = vSatProm * float.Parse("0,20");
                            RG_Ruptura = float.Parse(vData.Rows[i]["sinRupturaProm"].ToString().Replace("%","")) * float.Parse("0,20");
                            RG_Eficiencia = float.Parse(vData.Rows[i]["eficienciaProm"].ToString()) * float.Parse("0,15");
                            RG_Productividad = float.Parse(vData.Rows[i]["produccion"].ToString().Replace("%","")) * float.Parse("0,10");
                            vTotal = RG_Satisfaccion + float.Parse(vData.Rows[i]["conocimientoProm"].ToString()) + RG_Ruptura + RG_Eficiencia + RG_Productividad + RG_Llamadas;
                        }else{
                            RG_Ruptura = float.Parse(vData.Rows[i]["sinRupturaProm"].ToString().Replace("%", "")) * float.Parse("0,3");
                            RG_Eficiencia = float.Parse(vData.Rows[i]["eficienciaProm"].ToString()) * float.Parse("0,20");
                            RG_Productividad = float.Parse(vData.Rows[i]["produccion"].ToString().Replace("%", "")) * float.Parse("0,15");
                            vTotal = float.Parse(vData.Rows[i]["conocimientoProm"].ToString()) + RG_Ruptura + RG_Eficiencia + RG_Productividad + RG_Llamadas;
                        }
                        vData.Rows[i]["total"] = (Convert.ToDecimal(Math.Round(vTotal, 2))).ToString() + "%";
                    }
                }
                
            }catch (Exception ex){
                throw new Exception(ex.Message);
            }
        }

        public void graficos(DataTable vData) { 
            try{
                int vTotTR = 0, vTotTT = 0, vTotRup = 0, vTotNoRup = 0, vSolTareas = 0, vSolRupturas = 0;
                int vHorasEmpleados = vData.Rows.Count * 360;
                for (int i = 0; i < vData.Rows.Count; i++){
                    //SOLICITUDES
                    vSolTareas += Convert.ToInt32(vData.Rows[i]["tareas"].ToString());
                    vSolRupturas += Convert.ToInt32(vData.Rows[i]["rupturas"].ToString());

                    // EFICIENCIA
                    vTotRup += Convert.ToInt32(vData.Rows[i]["eficienciaRup"].ToString());
                    vTotNoRup += Convert.ToInt32(vData.Rows[i]["eficienciaNoRup"].ToString());
                        
                    // PRODUCTIVIDAD
                    vTotTR += Convert.ToInt32(vData.Rows[i]["tiempoReal"].ToString());
                    vTotTT += Convert.ToInt32(vData.Rows[i]["tiempoTransporte"].ToString());
                }

                if (vSolTareas > 0){
                    float vSolPromCR = float.Parse(vSolRupturas.ToString()) / float.Parse(vSolTareas.ToString()) * 100;
                    Decimal SolPromCR = Convert.ToDecimal(Math.Round(vSolPromCR, 2));
                    Decimal vSolPromSR = 100 - SolPromCR;
                    TxGraf1.Value = SolPromCR.ToString().Replace(",", ".");
                    TxGraf2.Value = vSolPromSR.ToString().Replace(",",".");
                }

                int vTotPeticiones = vTotNoRup + vTotRup;
                if (vTotPeticiones > 0){
                    float vRupProm = float.Parse(vTotRup.ToString()) / float.Parse(vTotPeticiones.ToString()) * 100;
                    Decimal vRupPromTotal = Convert.ToDecimal(Math.Round(vRupProm, 2));
                    TxGraf3.Value = vRupPromTotal.ToString().Replace(",",".");

                    float vNoRupProm = float.Parse(vTotNoRup.ToString()) / float.Parse(vTotPeticiones.ToString()) * 100;
                    Decimal vNoRupPromTotal = Convert.ToDecimal(Math.Round(vNoRupProm, 2));
                    TxGraf4.Value = vNoRupPromTotal.ToString().Replace(",", ".");
                }

                float vTR = float.Parse(vTotTR.ToString()) / float.Parse(vHorasEmpleados.ToString()) * 100;
                Decimal vTotalTR = Convert.ToDecimal(Math.Round(vTR, 2));
                TxGraf5.Value = vTotalTR.ToString().Replace(",", ".");
                    
                float vTT = float.Parse(vTotTT.ToString()) / float.Parse(vHorasEmpleados.ToString()) * 100;
                Decimal vTotalTT = Convert.ToDecimal(Math.Round(vTT, 2));
                TxGraf6.Value = vTotalTT.ToString().Replace(",", ".");

                float vTNP = 100 - (vTR + vTT);
                Decimal vTotalTNP = Convert.ToDecimal(Math.Round(vTNP, 2));
                TxGraf7.Value = vTotalTNP.ToString().Replace(",", ".");

                DivGraficos.Visible = true;
                UPanelRendimientoGrafic.Update();
            }catch (Exception ex){
                throw new Exception(ex.Message);
            }
        }

        private void limpiarForm() {
            try{
                Session["CUMPL_KPI"] = null;
                Session["CUMPL_RUPTURA"] = null;
                Session["CUMPL_RUPTURA_PAGING"] = null;
                Session["CUMPL_OSER"] = null;
                Session["CUMPL_OSER_PAGING"] = null;
                Session["CUMPL_RENDIMIENTO"] = null;

                TxCallAtendidas.Text = string.Empty;
                TxCallAtendidasNo.Text = string.Empty;
                TxCallObs.Text = string.Empty;
                TxCallPorcentajeNo.Text = string.Empty;
                TxCallPorcentajeSi.Text = string.Empty;
                TxCallTotal.Text = string.Empty;

                TxATMCumplimiento.Text = string.Empty;
                TxATMCumplimientoNo.Text = string.Empty;
                TxATMObs.Text = string.Empty;
                TxATMPorcentaje.Text = string.Empty;
                TxATMTotal.Text = string.Empty;
                
                TxABACumplimiento.Text = string.Empty;
                TxABACumplimientoNo.Text = string.Empty;
                TxABAObs.Text = string.Empty;
                TxABAPorcentaje.Text = string.Empty;
                TxABATotal.Text = string.Empty;

                TxCajaCumplidas.Text = string.Empty;
                TxCajaCumplidasNo.Text = string.Empty;
                TxCajaObs.Text = string.Empty;
                TxCajaPorcentaje.Text = string.Empty;
                TxCajaTotal.Text = string.Empty;

                TxKPICumplimiento.Text = string.Empty;
                TxKPICumplimientoNo.Text = string.Empty;
                TxKPIObs.Text = string.Empty;
                TxKPIPorcentaje.Text = string.Empty;
                TxKPITotal.Text = string.Empty;

                UPCalls.Update();
            }catch (Exception ex){
                throw new Exception(ex.Message);
            }
        
        }

        private void validaciones() {
            if (Convert.ToBoolean(HttpContext.Current.Session["CUMPL_FLAG_PENDIENTE"]))
                throw new Exception("Ya tiene un reporte registrado hoy. Regrese mañana.");
            if (TxCallTotal.Text == string.Empty || TxCallTotal.Text == "")
                throw new Exception("Favor ingrese el total de llamadas.");
            if (TxCallAtendidas.Text == string.Empty || TxCallAtendidas.Text == "")
                throw new Exception("Favor ingrese las llamadas atendidas.");
            if (float.Parse(TxCallPorcentajeSi.Text) <= 90 && TxCallObs.Text == "")
                throw new Exception("Favor ingrese las observaciones de las llamadas.");
            if (float.Parse(TxATMPorcentaje.Text) <= 90 && TxATMObs.Text == "")
                throw new Exception("Favor ingrese las observaciones de ATM en Medios de pago.");
            if (float.Parse(TxABAPorcentaje.Text) <= 90 && TxABAObs.Text == "")
                throw new Exception("Favor ingrese las observaciones de ABA en Medios de pago.");
            if (float.Parse(TxCajaPorcentaje.Text) <= 90 && TxCajaObs.Text == "")
                throw new Exception("Favor ingrese las observaciones de Caja en Medios de pago.");
            if (float.Parse(TxKPIPorcentaje.Text) <= 90 && TxKPIObs.Text == "")
                throw new Exception("Favor ingrese las observaciones de KPI.");
            DataTable vDatos = (DataTable)Session["CUMPL_RUPTURA"];
            if (vDatos != null && vDatos.Rows.Count > 0){
                int vCont = GvRuptura.PageIndex != 0 ? GvRuptura.PageIndex * 10 : 0;
                foreach (GridViewRow row in GvRuptura.Rows){
                    DropDownList DDLRazonRuptura = (DropDownList)row.Cells[2].FindControl("DDLRazonRuptura");
                    TextBox TxComment = (TextBox)row.Cells[3].FindControl("TxRupturaObs");
                    vDatos.Rows[vCont]["idRazon"] = DDLRazonRuptura.SelectedValue;
                    vDatos.Rows[vCont]["comentario"] = TxComment.Text;
                    vCont++;
                }

                for (int i = 0; i < vDatos.Rows.Count; i++){
                    if (vDatos.Rows[i]["idRazon"].ToString() == "" || vDatos.Rows[i]["idRazon"].ToString() == "0" || vDatos.Rows[i]["comentario"].ToString() == "")
                        throw new Exception("Favor complete la información de Rupturas.");
                }
            }
            vDatos = (DataTable)Session["CUMPL_OSER"];
            if (vDatos != null && vDatos.Rows.Count > 0){
                int vCont = GvOSER.PageIndex != 0 ? GvOSER.PageIndex * 10 : 0;
                foreach (GridViewRow row in GvOSER.Rows){
                    DropDownList DDLRazonOSER = (DropDownList)row.Cells[2].FindControl("DDLRazonER");
                    TextBox TxComment = (TextBox)row.Cells[3].FindControl("TxOSERObs");
                    vDatos.Rows[vCont]["idRazon"] = DDLRazonOSER.SelectedValue;
                    vDatos.Rows[vCont]["comentario"] = TxComment.Text;
                    vCont++;
                }

                for (int i = 0; i < vDatos.Rows.Count; i++){
                    if (vDatos.Rows[i]["idRazon"].ToString() == "" || vDatos.Rows[i]["idRazon"].ToString() == "0" || vDatos.Rows[i]["comentario"].ToString() == "")
                        throw new Exception("Favor complete la información de las Ordenes de Servicio Esperando Respuesta.");
                }
            }
            vDatos = (DataTable)Session["CUMPL_INSATISCACCION"];
            if (vDatos != null && vDatos.Rows.Count > 0){
                int vCont = GvInsatisfacciones.PageIndex != 0 ? GvInsatisfacciones.PageIndex * 10 : 0;
                foreach (GridViewRow row in GvInsatisfacciones.Rows){
                    TextBox TxComment = (TextBox)row.Cells[3].FindControl("TxSatisfaccionObs");
                    vDatos.Rows[vCont]["observaciones"] = TxComment.Text;
                    vCont++;
                }
                for (int i = 0; i < vDatos.Rows.Count; i++){
                    if (vDatos.Rows[i]["observaciones"].ToString() == "")
                        throw new Exception("Favor ingrese las observaciones de las ordenes de servicio con baja calificacion.");
                }
            }
            vDatos = (DataTable)Session["CUMPL_RENDIMIENTO"];
            if (vDatos != null && vDatos.Rows.Count > 0){
                int vCont = GvRendimiento.PageIndex != 0 ? GvRendimiento.PageIndex * 10 : 0;
                foreach (GridViewRow row in GvRendimiento.Rows){
                    TextBox TxComment = (TextBox)row.Cells[2].FindControl("TxRGObs");
                    vDatos.Rows[vCont]["comentario"] = TxComment.Text;
                    vCont++;
                }
                for (int i = 0; i < vDatos.Rows.Count; i++){
                    if (vDatos.Rows[i]["comentario"].ToString() == "")
                        throw new Exception("Favor ingrese las observaciones del Rendimiento General.");
                }
            }

        }

        protected void BtnConfirmar_Click(object sender, EventArgs e) {
            try{
                Object[] vDatosReporte = new object[17];
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
                String vXML = vMaestro.ObtenerCumplimiento(vDatosReporte);
                vXML = vXML.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
                String vQuery = "[STEISP_CUMPLIMIENTO_Reportes] 1, 0, 0" +
                    ",'" + vXML + "'";
                int vInfo = vConexion.obtenerId(vQuery);

                if (vInfo > 0){
                    DataTable vDatos = (DataTable)Session["CUMPL_KPI"];
                    if (vDatos.Rows.Count > 0)
                        insertarKPI(vDatos, vInfo);

                    vDatos = (DataTable)Session["CUMPL_RUPTURA"];
                    if (vDatos != null && vDatos.Rows.Count > 0)
                        insertarRupturas(vDatos, vInfo);

                    vDatos = (DataTable)Session["CUMPL_OSER"];
                    if (vDatos != null && vDatos.Rows.Count > 0)
                        insertarOSER(vDatos, vInfo);

                    vDatos = (DataTable)Session["CUMPL_RENDIMIENTO"];
                    if (vDatos != null &&  vDatos.Rows.Count > 0)
                        insertarRendimiento(vDatos, vInfo);

                    vDatos = (DataTable)Session["CUMPL_INSATISCACCION"];
                    if (vDatos != null && vDatos.Rows.Count > 0)
                        insertarInsatisfacciones(vDatos, vInfo);

                    String vMensaje = "Reporte creado con éxito, ";
                    SmtpService vService = new SmtpService();
                    Boolean vFlag = false;
                    vQuery = "[STEISP_Login] 3,'" + Session["USUARIO"].ToString() + "'";
                    vDatos = vConexion.obtenerDataTable(vQuery);

                    vService.EnviarMensaje(
                        ConfigurationManager.AppSettings["MAIL_CUMPLIMIENTO"].ToString(),
                        typeBody.Cumplimiento,
                        "Reporte de Metas de cumplimiento",
                        vDatos.Rows[0]["idJefe"].ToString(),
                        "El usuario <b>" + vDatos.Rows[0]["nombreEmpleado"].ToString() + "</b> ha registrado un nuevo reporte de metas de cumplimiento.<br>Favor revisar en la sección de pendientes."
                    );

                    vFlag = true;
                    if (vFlag)
                        Mensaje(vMensaje + "se envió la notificación", WarningType.Success);
                    else
                        Mensaje(vMensaje + "no se envió notificación. comuníquese con sistemas.", WarningType.Danger);

                    limpiarForm();
                }else 
                    Mensaje("Reporte no se pudo enviar. Comuníquese con sistemas.", WarningType.Danger);
                
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void GvInsatisfacciones_PageIndexChanging(object sender, GridViewPageEventArgs e){
            try{
                GvRendimiento.PageIndex = e.NewPageIndex;
                GvRendimiento.DataSource = (DataTable)Session["CUMPL_INSATISCACCION"];
                GvRendimiento.DataBind();
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
    }
}