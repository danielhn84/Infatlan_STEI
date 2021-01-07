using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infatlan_STEI_Agencias.classes;
using System.Data;
using System.Globalization;


namespace Infatlan_STEI_Agencias.pages.configuraciones
{
    public partial class agenciaTipo : System.Web.UI.Page
    {
        db vConexion = new db();
        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }       
        protected void Page_Load(object sender, EventArgs e){
            if (!Page.IsPostBack){
                if (Convert.ToBoolean(Session["AUTH"])){
                    cargar();
                }else {
                    Response.Redirect("/login.aspx");
                }
            }
        }       
        protected void BtnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                validarGuardarTipoAgencia();
                String vQuery1 = "STEISP_AGENCIA_TiposAgencia 1,'"
                                   + TxAgencia.Text.Substring(0, 1).ToUpper() + TxAgencia.Text.Substring(1) + 
                                   "','"+ Session["USUARIO"] + "'";
                Int32 vInformacion1 = vConexion.ejecutarSql(vQuery1);

                if (vInformacion1 == 1)
                {
                    Mensaje("Creación de tipo de agencia con exito. ", WarningType.Success);
                    limpiarFormularioTipoAgencia();
                    cargar();
                    UPTipoAgencias.Update();
                }
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }        
        private void validarGuardarTipoAgencia()
        {
            TxAgencia.Text = TxAgencia.Text.Replace("\n", "");
            if (TxAgencia.Text == "" || TxAgencia.Text == string.Empty)
                throw new Exception("Falta ingresar el tipo de agencia que desea crear.");
        }        
        private void limpiarFormularioTipoAgencia()
        {
            TxAgencia.Text = string.Empty;
        }        
        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                limpiarFormularioTipoAgencia();
                Mensaje("Acción cancelado con exito. ", WarningType.Success);
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }        
        void cargar()
        {
            try
            {
                String vQuery = "STEISP_AGENCIA_TiposAgencia 2";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);              
                TxTipoAgenciaModal.Text = vDatos.Rows[0]["nombre"].ToString();
                GVTipoAgenciasBASA.DataSource = vDatos;
                GVTipoAgenciasBASA.DataBind();
                Session["AG_TA_DATA_AGENCIA_TIPO"] = vDatos;
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }

        }       
        //protected void GVTipoAgencias_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "Modifcar")
        //    {
        //        DivAlerta.Visible = false;
        //        UpdateModal.Update();

        //        string vIdTipoAgenciaModificar = e.CommandArgument.ToString();
        //        Session["AG_TA_ID_AREA_MODIFICAR"] = vIdTipoAgenciaModificar;

        //        try
        //        {
        //            String vQuery2 = " STEISP_AGENCIA_TiposAgencia 3," + vIdTipoAgenciaModificar;
        //            DataTable vDatos = vConexion.obtenerDataTable(vQuery2);
        //            TxIdTipoAgenciaModal.Text = vDatos.Rows[0]["idTipoAgencia"].ToString();
        //            TxTipoAgenciaModal.Text = vDatos.Rows[0]["nombre"].ToString();
        //            DdlEstadoTipoAgencia.SelectedValue = vDatos.Rows[0]["estado"].ToString();
        //            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModalModificarTipoAgencia();", true);
                    
        //        }
        //        catch (Exception ex)
        //        {
        //            Mensaje(ex.Message, WarningType.Danger);
        //        }
        //    }
        //}        
        protected void btnModalModificarTipoAgencia_Click(object sender, EventArgs e)
        {
            try
            {
                DivAlerta.Visible = false;
                UpdateModal.Update();
                validarModificarTipoAgencia();
                string estado = DdlEstadoTipoAgencia.SelectedValue == "True" ? "1" : "0";
                
                String vQuery3 = " STEISP_AGENCIA_TiposAgencia 4,"
                                   + Session["AG_TA_ID_AREA_MODIFICAR"] +
                                   ",'" + TxTipoAgenciaModal.Text.Substring(0, 1).ToUpper() + TxTipoAgenciaModal.Text.Substring(1) +
                                   "'," + estado +
                                   ",'" + Session["USUARIO"]+ "'";
                Int32 vInformacion3 = vConexion.ejecutarSql(vQuery3);

                if (vInformacion3 == 1)
                {
                    Mensaje("Tipo de agencia actualizado con exito. ", WarningType.Success);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModalModificarTipoAgencia();", true);
                    cargar();
                    UPTipoAgencias.Update();
                }

            }
            catch (Exception ex)
            {
                LbMensajeModalError.Text = ex.Message;
                DivAlerta.Visible = true;
                UpdateModal.Visible = true;
                UpdateModal.Update();
            }
        }        
        protected void GVTipoAgencias_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GVTipoAgenciasBASA.PageIndex = e.NewPageIndex;
                GVTipoAgenciasBASA.DataSource = (DataTable)Session["AG_TA_DATA_AGENCIA_TIPO"];
                GVTipoAgenciasBASA.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }        
        protected void TxBuscarArea_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cargar();
                String vBusqueda = TxBuscarArea.Text;
                DataTable vDatos = (DataTable)Session["AG_TA_DATA_AGENCIA_TIPO"];
                if (vBusqueda.Equals(""))
                {
                    GVTipoAgenciasBASA.DataSource = vDatos;
                    GVTipoAgenciasBASA.DataBind();
                    UPTipoAgencias.Update();
                }
                else
                {
                    vBusqueda = TxBuscarArea.Text.Substring(0, 1).ToUpper() + TxBuscarArea.Text.Substring(1);
                    EnumerableRowCollection<DataRow> filtered = vDatos.AsEnumerable()
                        .Where(r => r.Field<String>("nombre").Contains(vBusqueda));

                 
                    Boolean isNumeric = int.TryParse(vBusqueda, out int n);

                    if (isNumeric)
                    {
                        if (filtered.Count() == 0)
                        {
                            filtered = vDatos.AsEnumerable().Where(r =>
                                Convert.ToInt32(r["idTipoAgencia"]) == Convert.ToInt32(vBusqueda));
                        }
                    }

                    DataTable vDatosFiltrados = new DataTable();
                    vDatosFiltrados.Columns.Add("idTipoAgencia");
                    vDatosFiltrados.Columns.Add("nombre");
                    vDatosFiltrados.Columns.Add("estado");

                    foreach (DataRow item in filtered)
                    {
                        vDatosFiltrados.Rows.Add(
                            item["idTipoAgencia"].ToString(),
                            item["nombre"].ToString(),
                            item["estado"].ToString()
                            );
                    }

                    GVTipoAgenciasBASA.DataSource          = vDatosFiltrados;
                    GVTipoAgenciasBASA.DataBind();
                    Session["AG_TA_DATA_AGENCIA_TIPO"] = vDatosFiltrados;
                    UPTipoAgencias.Update();
                }

            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }        
        protected void BtnRegresar_Click(object sender, EventArgs e){
            Response.Redirect("../../default.aspx");
        }        
        private void validarModificarTipoAgencia()
        {
            TxTipoAgenciaModal.Text = TxTipoAgenciaModal.Text.Replace("\n", "");
           
            if (TxTipoAgenciaModal.Text == "" || TxTipoAgenciaModal.Text == string.Empty)
                throw new Exception("Campos vacios, Favor ingresar el tipo de agencia.");
        }        
        protected void TxTipoAgenciaModal_TextChanged(object sender, EventArgs e)
        {           

                DivAlerta.Visible = false;
                UpdateModal.Update();
               
        }

        protected void GVTipoAgencias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string vIdTipoAgenciaModificar = e.CommandArgument.ToString();
        }

        protected void GVTipoAgenciasBASA_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void GVTipoAgenciasBASA_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string vIdTipoAgenciaModificar = e.CommandArgument.ToString();
        }
    }
}