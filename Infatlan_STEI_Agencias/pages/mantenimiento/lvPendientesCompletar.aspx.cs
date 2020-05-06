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
    public partial class LvCompletar : System.Web.UI.Page
    {
        db vConexion = new db();

        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["USUARIO"] = "acamador";
            if (!Page.IsPostBack)
            {
                cargarDatos();
            }
        }

        private void cargarDatos()
        {
            try
            {
                String vQuery = "STEISP_AGENCIA_CompletarListaVerificacion 1," + Session["USUARIO"];
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                GVListaVerificacion.DataSource = vDatos;
                GVListaVerificacion.DataBind();
                Session["AG_LvPC_LISTAS_PENDIENTES_TECNICO"] = vDatos;
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void GVListaVerificacion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Completar")
            {            
                string vIdMantenimientoCompletar = e.CommandArgument.ToString();
                Session["AG_LvPC_ID_MANTENIMIENTO_LV_COMPLETAR"] = vIdMantenimientoCompletar;
                try
                {
                    //DATOS GENERALES
                    String vQuery = "STEISP_AGENCIA_CompletarListaVerificacion 2," + vIdMantenimientoCompletar;
                    DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                    Session["AG_LvPC_DATOS_GENERALES"] = vDatos;
                    string idTecnicoResponsable = vDatos.Rows[0]["idUsuario"].ToString();

                    //IDENTIDAD TECNICO RESPONSABLE
                    String vQuery1 = "STEISP_AGENCIA_CompletarListaVerificacion 3," + idTecnicoResponsable;
                    DataTable vDatos1 = vConexion.obtenerDataTable(vQuery1);
                    Session["AG_LvPC_TECNICO_RESPONSABLE"] = vDatos1;               

                    //TECNICOS PARTICIPANTES
                    String vQuery2 = "STEISP_AGENCIA_CompletarListaVerificacion 4," + vIdMantenimientoCompletar;
                    DataTable vDatos2 = vConexion.obtenerDataTable(vQuery2);           
                    Session["AG_LvPC_TECNICOS_PARTICIPANTES"] = vDatos2;

                    Response.Redirect("/sites/agencias/pages/mantenimiento/lvIndividual.aspx?ex=1");
                }
                catch (Exception ex)
                {
                    Mensaje(ex.Message, WarningType.Danger);
                }

            }else if(e.CommandName == "Cancelar"){
                string vIdMantenimientoCompletar = e.CommandArgument.ToString();
                Session["AG_LvPC_ID_MANTENIMIENTO_LV_COMPLETAR"] = vIdMantenimientoCompletar;

                String vQuery = "STEISP_AGENCIA_CompletarListaVerificacion 2," + vIdMantenimientoCompletar;
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                Session["AG_LvPC_USUARIO_RESPONSABLE"] = vDatos.Rows[0]["idUsuario"].ToString();

                TxIdMantenimiento.Text = vIdMantenimientoCompletar;
                Titulo.Text = " " + vDatos.Rows[0]["Lugar"].ToString();
                UpdatePanel3.Update();
                limpiarModalCancelar();
                tecnicosResponsable();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModalCancelarLV();", true);
                }
          }

        private void validarModalCancelacion()
        {
            if (DDLMotivo.SelectedValue.Equals("0"))
                throw new Exception("Falta completar campos, favor seleccione  un motivo de cancelación de la lista desplegable.");

            if (DDLMotivo.SelectedValue.Equals("4") && DDLNombreResponsable.SelectedValue.Equals(""))
                throw new Exception("Falta completar campos, favor seleccione el nuevo tecnico responsable.");

            if (TxDetalle.Text == "" || TxDetalle.Text == string.Empty)
                throw new Exception("Falta completar campos, favor ingrese un detalle del motivo de cancelación.");
        }

        protected void DDLMotivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLMotivo.SelectedValue.Equals("4"))
            {
                asterisco.Visible = true;
                DDLNombreResponsable.Visible = true;
                etiqueta.Visible = true;
            }else {
                asterisco.Visible = false;
                DDLNombreResponsable.Visible = false;
                etiqueta.Visible = false;
                funcionCambioTecnicoResponsable();                     
            }
        }

        protected void TxDetalle_TextChanged(object sender, EventArgs e)
        {
            funcionCambioTecnicoResponsable();
        }

        protected void DDLNombreResponsable_SelectedIndexChanged(object sender, EventArgs e)
        {
            funcionCambioTecnicoResponsable();            
        }

        private void funcionCambioTecnicoResponsable(){
            LbMensajeModalErrorReprogramar.Text = "";
            UpdateModal.Visible = false;
            UpdateModal.Update();
        }

        private void limpiarModalCancelar(){
            DDLMotivo.SelectedIndex = -1;
            DDLNombreResponsable.SelectedIndex = -1;
            TxDetalle.Text = String.Empty;
            LbMensajeModalErrorReprogramar.Text = "";
            UpdateModal.Visible = false;
            UpdateModal.Update();
            asterisco.Visible = false;
            DDLNombreResponsable.Visible = false;
            etiqueta.Visible = false;
        }

        private void tecnicosResponsable()
        {
             try
            {
                DDLNombreResponsable.Items.Clear();
                String vQuery = "STEISP_AGENCIA_CreacionNotificacion 5";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                DDLNombreResponsable.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
              
                if (vDatos.Rows.Count > 0)
                {
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLNombreResponsable.Items.Add(new ListItem { Value = item["idUsuario"].ToString(), Text = item["nombre"].ToString() + "  " + item["apellidos"].ToString() });
                        
                    }
                }

            }
            catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
                }
        }

        protected void BtnCancelarLV_Click(object sender, EventArgs e)
        {
            try
            {
                validarModalCancelacion();

                string idTecnicoResponsable= String.Empty;
                string idEstadoMantenimiento = String.Empty;
                if (DDLMotivo.SelectedValue.Equals("4"))
                {
                   idTecnicoResponsable = DDLNombreResponsable.SelectedValue;
                   idEstadoMantenimiento = "7";
                }
                else
                {                  
                    idTecnicoResponsable = Session["AG_LvPC_USUARIO_RESPONSABLE"].ToString();
                    idEstadoMantenimiento = "5";
                }


                String vQuery = "STEISP_AGENCIA_CompletarListaVerificacion 11," + Session["AG_LvPC_ID_MANTENIMIENTO_LV_COMPLETAR"] +
                               "," + DDLMotivo.SelectedValue +
                               ",'" + TxDetalle.Text +
                               "'," + idEstadoMantenimiento +
                                "," + idTecnicoResponsable;
                Int32 vInfo = vConexion.ejecutarSql(vQuery);


                if (vInfo == 1)
                {
                    Mensaje("Lista de verificación cancelada con exito. ", WarningType.Success);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModalCancelarLV();", true);
                    cargarDatos();
                }


            }
            catch (Exception ex)
            {

                LbMensajeModalErrorReprogramar.Text = ex.Message;
                UpdateModal.Visible = true;
                UpdateModal.Update();


            }
        }

        protected void TxBuscarAgencia_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cargarDatos();
                String vBusqueda = TxBuscarAgencia.Text;
                DataTable vDatos = (DataTable)Session["AG_LvPC_LISTAS_PENDIENTES_TECNICO"];
                if (vBusqueda.Equals(""))
                {
                    GVListaVerificacion.DataSource = vDatos;
                    GVListaVerificacion.DataBind();
                    UPListaVerificacion.Update();
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
                    vDatosFiltrados.Columns.Add("Cod_Agencia");
                    vDatosFiltrados.Columns.Add("Lugar");
                    vDatosFiltrados.Columns.Add("Area");
                    vDatosFiltrados.Columns.Add("sysAid");
                    vDatosFiltrados.Columns.Add("Responsable");
                    vDatosFiltrados.Columns.Add("idUsuario");

                    foreach (DataRow item in filtered)
                    {
                        vDatosFiltrados.Rows.Add(
                            item["id_Mantenimiento"].ToString(),
                            item["fecha"].ToString(),
                            item["Cod_Agencia"].ToString(),
                            item["Lugar"].ToString(),
                            item["Area"].ToString(),
                            item["sysAid"].ToString(),
                            item["Responsable"].ToString(),
                            item["idUsuario"].ToString()

                            );
                    }

                    GVListaVerificacion.DataSource = vDatosFiltrados;
                    GVListaVerificacion.DataBind();
                    Session["AG_LvPC_LISTAS_PENDIENTES_TECNICO"] = vDatosFiltrados;
                    UPListaVerificacion.Update();
                }

            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void GVListaVerificacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GVListaVerificacion.PageIndex = e.NewPageIndex;
                GVListaVerificacion.DataSource = (DataTable)Session["AG_LvPC_LISTAS_PENDIENTES_TECNICO"];
                GVListaVerificacion.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
    }
}
