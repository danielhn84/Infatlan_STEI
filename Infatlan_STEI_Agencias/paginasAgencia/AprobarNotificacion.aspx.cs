using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using Infatlan_STEI_Agencias.classes;


namespace Infatlan_STEI_Agencias.paginasAgencia
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
                Session["MANTENIMIENTOS_PENDIENTES_APROBAR"] = vDatos;
                
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
            
        }

        public void Mensaje(string vMensaje, WarningType type){
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e){
            if (e.CommandName == "Aprobar"){

                string vIdMantenimiento = e.CommandArgument.ToString();
                Session["AGENCIA_ID_MANTENIMIENTO"] = vIdMantenimiento;

                String vQuery = "STEISP_AGENCIA_AprobarNotificacion 3," + vIdMantenimiento;
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                foreach (DataRow item in vDatos.Rows)
                {
                    string vLugar = item["Lugar"].ToString();
                    string vFecha = item["fecha"].ToString();
                    string vResponsable = item["Responsable"].ToString();


                    TxLugar.Text = vLugar;
                    TxFecha.Text = vFecha;
                    TxTecnicoResponsable.Text = vResponsable;

                }
                
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);

               
            }else if(e.CommandName == "Cancelar"){

                string vIdMantenimiento = e.CommandArgument.ToString();
                Session["AGENCIA_ID_MANTENIMIENTO"] = vIdMantenimiento;

                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModalCancelacion();", true);


            }

        }

        protected void btnModalAprobarNotificacion_Click(object sender, EventArgs e){
            try{
                String vQuery = "STEISP_AGENCIA_AprobarNotificacion 2," + Session["AGENCIA_ID_MANTENIMIENTO"];
                Int32 vInfo = vConexion.ejecutarSql(vQuery);

                if (vInfo == 1){
                    Mensaje("Notificacón aprobada con exito", WarningType.Success);
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
    

                String vBusqueda = TxBuscarAgencia.Text;
                DataTable vDatos = (DataTable)Session["MANTENIMIENTOS_PENDIENTES_APROBAR"];

                if (vBusqueda.Equals(""))
                {
                    cargarDatos();
                    //GVBusqueda.DataSource = vDatos;
                    //GVBusqueda.DataBind();
                    UPGvBusqueda.Update();
                }
                else
                {
                    EnumerableRowCollection<DataRow> filtered = vDatos.AsEnumerable()
                        .Where(r => r.Field<String>("Lugar").Contains(vBusqueda.ToUpper()));

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
                    Session["MANTENIMIENTOS_PENDIENTES_APROBAR"] = vDatosFiltrados;
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
                throw new Exception("Favor seleccione motivo de cancelación.");
            if (TxDetalle.Text.Equals(""))
                throw new Exception("Favor ingrese detalle de la cancelación.");
        }


        protected void BtnCancelarNoti_Click(object sender, EventArgs e)
        {

            try
            {
                validaciones();
                String vQuery = "STEISP_AGENCIA_AprobarNotificacion  4," + Session["AGENCIA_ID_MANTENIMIENTO"] +"," +Session["USUARIO"];
                Int32 vInfo = vConexion.ejecutarSql(vQuery);

                if (vInfo == 1)
                {
                    Mensaje("Notificacón cancelada con exito", WarningType.Success);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModalCancelacion();", true);
                }
                cargarDatos();
            }
            catch (Exception ex)
            {
                LbMensajeModalError.Text = ex.Message;
                //UpdateModal.Update();
                //Mensaje(ex.Message, WarningType.Danger);
            }
        }
    }
}