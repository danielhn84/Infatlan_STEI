using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using Infatlan_STEI_Agencias.classes;


namespace Infatlan_STEI_Agencias.pages
{
    public partial class AprobarNotificacion : System.Web.UI.Page
    {
        db vConexion = new db();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["USUARIO"] = "acamador";

            if (!Page.IsPostBack)
            {
                cargarDatos();
            }
        }
        private void cargarDatos() {
            try{

                String vQuery = "STEISP_AGENCIA_AprobarNotificacion 1";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                GVBusqueda.DataSource = vDatos;
                GVBusqueda.DataBind();
                Session["AG_CN_MANTENIMIENTOS_PENDIENTES_APROBAR"] = vDatos;
                
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
            
        }
        public void Mensaje(string vMensaje, WarningType type){
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }
        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e){
            if (e.CommandName == "Aprobar"){
                limpiarModalAprobarNotificacion();
                string vIdMantenimiento = e.CommandArgument.ToString();
                Session["AG_CN_ID_MANTENIMIENTO"] = vIdMantenimiento;

                String vQuery = "STEISP_AGENCIA_AprobarNotificacion 3," + vIdMantenimiento;
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                foreach (DataRow item in vDatos.Rows)
                {
                    string vIdMantenimientoSelect = item["id_Mantenimiento"].ToString();
                    string vLugar = item["Lugar"].ToString();
                    string vFecha = item["fecha"].ToString();
                    string vResponsable = item["Responsable"].ToString();
                    string vArea = item["Area"].ToString();

                    TxIdMant.Text = vIdMantenimientoSelect;
                    //TxLugar.Text = vLugar;
                    TxFecha.Text = vFecha;
                    TxTecnicoResponsable.Text = vResponsable;
                    TxArea.Text = vArea;
                    lbTitulo.Text = "Aprobar Notificación " + vLugar;
                    UpdatePanel1.Update();
                }

                String vQuery2 = "STEISP_AGENCIA_AprobarNotificacion 5," + vIdMantenimiento;
                DataTable vDatos2 = vConexion.obtenerDataTable(vQuery2);
                foreach (DataRow item in vDatos2.Rows)
                {
                    string vTecnicoParticipantes = item["Participantes"].ToString();                    
                    TxParticipantes.Text = TxParticipantes.Text + vTecnicoParticipantes + "\n";                
                }

                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);

               
            }else if(e.CommandName == "Cancelar"){
            

                limpiarModalCancelarNotificacion();
            
                string vIdMantenimiento = e.CommandArgument.ToString();
                Session["AG_CN_ID_MANTENIMIENTO"] = vIdMantenimiento;
                String vQuery = "STEISP_AGENCIA_AprobarNotificacion 3," + vIdMantenimiento;
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                string vLugar = vDatos.Rows[0]["Lugar"].ToString();

                lbTituloCancelar.Text = "Cancelar Notificación " + vLugar;
                UpdatePanel6.Update();

                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModalCancelacion();", true);
            }

        }
        protected void btnModalAprobarNotificacion_Click(object sender, EventArgs e){
            try{
                String vQuery = "STEISP_AGENCIA_AprobarNotificacion 2," + Session["AG_CN_ID_MANTENIMIENTO"] +"," + Session["USUARIO"];
                Int32 vInfo = vConexion.ejecutarSql(vQuery);

                if (vInfo == 1){
                    Mensaje("Notificación aprobada con exito. ", WarningType.Success);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
                }

                cargarDatos();
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
        protected void TxBuscarAgencia_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cargarDatos();
                String vBusqueda = TxBuscarAgencia.Text;
                DataTable vDatos = (DataTable)Session["AG_CN_MANTENIMIENTOS_PENDIENTES_APROBAR"];

                if (vBusqueda.Equals(""))
                {
                   
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                    UPGvBusqueda.Update();
                }
                else
                {
                    EnumerableRowCollection<DataRow> filtered = vDatos.AsEnumerable()
                        .Where(r => r.Field<String>("Lugar").Contains(vBusqueda));

                    Boolean isNumeric = int.TryParse(vBusqueda, out int n);
                    if (isNumeric)
                    {
                        if (filtered.Count() == 0)
                        {
                            filtered = vDatos.AsEnumerable().Where(r =>
                                Convert.ToInt32(r["id_Mantenimiento"]) == Convert.ToInt32(vBusqueda));
                        }
                    }


                    DataTable vDatosFiltrados = new DataTable();
                    vDatosFiltrados.Columns.Add("id_Mantenimiento");
                    vDatosFiltrados.Columns.Add("fecha");
                    vDatosFiltrados.Columns.Add("Hr_Inicio");
                    vDatosFiltrados.Columns.Add("Hr_Fin");
                    vDatosFiltrados.Columns.Add("Lugar");
                    vDatosFiltrados.Columns.Add("Cod_Agencia");
                    vDatosFiltrados.Columns.Add("Responsable");
                    vDatosFiltrados.Columns.Add("Area");

                    foreach (DataRow item in filtered)
                    {
                        vDatosFiltrados.Rows.Add(
                            item["id_Mantenimiento"].ToString(),
                            item["fecha"].ToString(),
                            item["Hr_Inicio"].ToString(),
                            item["Hr_Fin"].ToString(),
                            item["Lugar"].ToString(),
                            item["Cod_Agencia"].ToString(),
                            item["Responsable"].ToString(),
                            item["Area"].ToString()
                            );
                    }

                    GVBusqueda.DataSource = vDatosFiltrados;
                    GVBusqueda.DataBind();
                    Session["AG_CN_MANTENIMIENTOS_PENDIENTES_APROBAR"] = vDatosFiltrados;
                    UPGvBusqueda.Update();
                }


            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }
        private void validaciones()
        {           
            if (DDLMotivo.SelectedValue.Equals("0"))
                throw new Exception("Falta completar datos, Favor seleccionar un motivo de cancelación del mantenimiento. ");
            
            if (TxDetalle.Text.Equals(""))
                throw new Exception("Falta completar datos, Favor ingrese detalle de la cancelación del mantenimiento. ");
          
        }
        protected void BtnCancelarNoti_Click(object sender, EventArgs e)
        {
            try
            {
                validaciones();
                String vQuery = "STEISP_AGENCIA_AprobarNotificacion  4," + Session["AG_CN_ID_MANTENIMIENTO"] +"," +Session["USUARIO"]+ "," + "'"+  DDLMotivo.SelectedItem.Text+ "'"+ "," + "'"+ TxDetalle.Text + "'";
                Int32 vInfo = vConexion.ejecutarSql(vQuery);

                String vQuery1 = "STEISP_AGENCIA_AprobarNotificacion  6," + Session["AG_CN_ID_MANTENIMIENTO"] ;
                Int32 vInfo1 = vConexion.ejecutarSql(vQuery1);

                if (vInfo == 1)
                {
                    Mensaje("Notificacón cancelada con exito, esta pendiente que el jefe o suplente reprogramen el mantenimiento", WarningType.Success);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModalCancelacion();", true);
                }
                cargarDatos();
            }
            catch (Exception ex)
            {
                LbMensajeModalError.Text = ex.Message;
                UpdateModal.Visible = true;
                UpdateModal.Update();
               
            }
        }
        private void limpiarModalAprobarNotificacion()
        {
            TxIdMant.Text = string.Empty;
            //TxLugar.Text = string.Empty;
            TxArea.Text = string.Empty;
            TxFecha.Text = string.Empty;
            TxTecnicoResponsable.Text = string.Empty;
            TxParticipantes.Text = string.Empty;
        }
        private void limpiarModalCancelarNotificacion()
        {
            DDLMotivo.SelectedIndex = -1;
            TxDetalle.Text = string.Empty;
            UpdateModal.Visible = false;
        }
        protected void GVBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GVBusqueda.PageIndex = e.NewPageIndex;
                GVBusqueda.DataSource = (DataTable)Session["AG_CN_MANTENIMIENTOS_PENDIENTES_APROBAR"];
                GVBusqueda.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
    }
}