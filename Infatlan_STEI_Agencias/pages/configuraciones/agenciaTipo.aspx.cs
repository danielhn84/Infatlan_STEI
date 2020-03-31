using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infatlan_STEI_Agencias.classes;
using System.Data;


namespace Infatlan_STEI_Agencias.pages.configuraciones
{
    public partial class agenciaTipo : System.Web.UI.Page
    {

        db vConexion = new db();
        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                cargar();
            }
        }

        protected void BtnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                validarGuardarTipoAgencia();
                String vQuery1 = " STEISP_AGENCIA_TiposAgencia 1,'"
                                   + TxAgencia.Text + "'";
                Int32 vInformacion1 = vConexion.ejecutarSql(vQuery1);

                if (vInformacion1 == 1)
                {
                    Mensaje("Creación de tipo de agencia con exito. ", WarningType.Success);
                    limpiarFormularioTipoAgencia();
                    cargar();
                }
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }


        private void validarGuardarTipoAgencia()
        {
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
                GVTipoAgencias.DataSource = vDatos;
                GVTipoAgencias.DataBind();               
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }

        }

        protected void GVTipoAgencias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Modifcar")
            {
                string vIdTipoAgenciaModificar = e.CommandArgument.ToString();
                Session["AG_TA_ID_AREA_MODIFICAR"] = vIdTipoAgenciaModificar;

                try
                {
                    String vQuery2 = " STEISP_AGENCIA_TiposAgencia 3," + vIdTipoAgenciaModificar;
                    DataTable vDatos = vConexion.obtenerDataTable(vQuery2);
                    TxIdTipoAgenciaModal.Text = vDatos.Rows[0]["idTipoAgencia"].ToString();
                    TxTipoAgenciaModal.Text = vDatos.Rows[0]["nombre"].ToString();
                    DdlEstadoTipoAgencia.SelectedValue = vDatos.Rows[0]["estado"].ToString();
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModalModificarTipoAgencia();", true);
                }
                catch (Exception ex)
                {
                    Mensaje(ex.Message, WarningType.Danger);
                }
            }
        }

        protected void btnModalModificarTipoAgencia_Click(object sender, EventArgs e)
        {
            try

            {
                string estado = "";
                if (DdlEstadoTipoAgencia.SelectedValue == "True")
                { estado = "1"; }
                else
                {
                    estado = "0";
                }

                String vQuery3 = " STEISP_AGENCIA_TiposAgencia 4,"
                                   + Session["AG_TA_ID_AREA_MODIFICAR"] +
                                   ",'" + TxTipoAgenciaModal.Text +
                                   "'," + estado;

                Int32 vInformacion3 = vConexion.ejecutarSql(vQuery3);

                if (vInformacion3 == 1)
                {
                    Mensaje("Tipo de agencia actualizado con exito. ", WarningType.Success);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModalModificarTipoAgencia();", true);
                    cargar();
                }

            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
    }
}