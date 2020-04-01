using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using Infatlan_STEI_Agencias.classes;
using System.Drawing;


namespace Infatlan_STEI_Agencias.pages.configuraciones
{
    public partial class motivosCancelacionMantenimientos : System.Web.UI.Page
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
                validarGuardarMotivo();
                String vQuery1 = " STEISP_AGENCIA_MotivosCancelacion 1,'"
                                   + TxMotivoCancelacion.Text + "',"
                                   + RblTipo.SelectedValue;
                Int32 vInformacion1 = vConexion.ejecutarSql(vQuery1);

                if (vInformacion1 == 1)
                {
                    Mensaje("Motivo de cancelación creado con exito. ", WarningType.Success);   
                    limpiarFormulario();
                    cargar();
                }
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                limpiarFormulario();
                Mensaje("Acción cancelado con exito. ", WarningType.Success);
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }

        }

       private void validarGuardarMotivo()
        {
            if (TxMotivoCancelacion.Text == "" || TxMotivoCancelacion.Text == string.Empty)
                throw new Exception("Falta completar el campo Motivo de cancelación.");

            if (RblTipo.SelectedValue.Equals(""))
                throw new Exception("Falta completar opción tipo.");       
        }

       private void limpiarFormulario()
        {
            TxMotivoCancelacion.Text = string.Empty;
            RblTipo.SelectedIndex = -1;

        }

        void cargar()
        {
            try
            {
                String vQuery = "STEISP_AGENCIA_MotivosCancelacion 2";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                GVMotivos.DataSource = vDatos;
                GVMotivos.DataBind();
                Session["AG_MCM_MOTIVOS"] = vDatos;

            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }

        }



        protected void GVMotivos_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Modifcar")
            {
                string vIdEstadoModificar = e.CommandArgument.ToString();
                Session["AG_MCM_ID_ESTADO_MODIFICAR"] = vIdEstadoModificar;

                try
                {

                    String vQuery2 = " STEISP_AGENCIA_MotivosCancelacion 2," + vIdEstadoModificar;
                    DataTable vDatos = vConexion.obtenerDataTable(vQuery2);
                    Session["AG_MCM_DATA_ESTADOS"] = vDatos;
                    TxIdEstadoModal.Text = vDatos.Rows[0]["id"].ToString();
                    TxMotivoModal.Text = vDatos.Rows[0]["motivo"].ToString();
                    DDLTipo.SelectedItem.Text = vDatos.Rows[0]["tipo"].ToString();
                    DdlEstado.SelectedValue = vDatos.Rows[0]["estado"].ToString();

                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModalModificarEstado();", true);


                }
                catch (Exception ex)
                {
                    Mensaje(ex.Message, WarningType.Danger);
                }
            }
        }

        protected void btnModalModificarEstado_Click(object sender, EventArgs e)
        {
            try

            {
                string estado = "";
                if (DdlEstado.SelectedValue == "True")
                { estado = "1"; }else {
                    estado = "0"; }

                String vQuery3 = " STEISP_AGENCIA_MotivosCancelacion 4,"
                                   + Session["AG_MCM_ID_ESTADO_MODIFICAR"] +
                                   ",'" + TxMotivoModal.Text +
                                   "'," + DDLTipo.SelectedValue +
                                   "," + estado;

                Int32 vInformacion3 = vConexion.ejecutarSql(vQuery3);

                if (vInformacion3 == 1)
                {
                    Mensaje("Motivo actualizado con exito. ", WarningType.Success);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModalModificarEstado();", true);
                    cargar();
                }

            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }

        }

        protected void GVMotivos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GVMotivos.PageIndex = e.NewPageIndex;
                GVMotivos.DataSource = (DataTable)Session["AG_MCM_MOTIVOS"];
                GVMotivos.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void TxBuscarMotivo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cargar();
                String vBusqueda = TxBuscarMotivo.Text;
                DataTable vDatos = (DataTable)Session["AG_MCM_MOTIVOS"];
                if (vBusqueda.Equals(""))
                {
                    GVMotivos.DataSource = vDatos;
                    GVMotivos.DataBind();
                    UpdatePanel5.Update();
                }
                else
                {
                    EnumerableRowCollection<DataRow> filtered = vDatos.AsEnumerable()
                        .Where(r => r.Field<String>("tipo").Contains(vBusqueda));

                    Boolean isNumeric = int.TryParse(vBusqueda, out int n);

                    if (isNumeric)
                    {
                        if (filtered.Count() == 0)
                        {
                            filtered = vDatos.AsEnumerable().Where(r =>
                                Convert.ToInt32(r["id"]) == Convert.ToInt32(vBusqueda));
                        }
                    }

                    DataTable vDatosFiltrados = new DataTable();
                    vDatosFiltrados.Columns.Add("id");
                    vDatosFiltrados.Columns.Add("motivo");
                    vDatosFiltrados.Columns.Add("tipo");
                    vDatosFiltrados.Columns.Add("estado");

                    foreach (DataRow item in filtered)
                    {
                        vDatosFiltrados.Rows.Add(
                            item["id"].ToString(),
                            item["motivo"].ToString(),
                            item["tipo"].ToString(),
                            item["estado"].ToString()
                            );
                    }

                    GVMotivos.DataSource = vDatosFiltrados;
                    GVMotivos.DataBind();
                    Session["AG_MCM_MOTIVOS"] = vDatosFiltrados;
                    UpdatePanel5.Update();
                }

            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
    }
}