using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Infatlan_STEI_Agencias.classes;

namespace Infatlan_STEI_Agencias.pages.configuraciones
{
    public partial class tiposAreas : System.Web.UI.Page
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
                validarGuardarArea();
                String vQuery1 = " STEISP_AGENCIA_AreasMantenimiento 1,'"
                                   + TxArea.Text + "'";                           
                Int32 vInformacion1 = vConexion.ejecutarSql(vQuery1);

                if (vInformacion1 == 1)
                {
                    Mensaje("Creación de area de mantenimiento con exito. ", WarningType.Success);
                    limpiarFormularioArea();
                    cargar();
                }
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
        private void validarGuardarArea()
        {
            if (TxArea.Text == "" || TxArea.Text == string.Empty)
                throw new Exception("Falta ingresar el area de mantenimiento.");           
        }
        private void limpiarFormularioArea()
        {
            TxArea.Text = string.Empty;
        }
        void cargar()
        {
            try
            {
                String vQuery = "STEISP_AGENCIA_AreasMantenimiento 2";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                GVAreas.DataSource = vDatos;
                GVAreas.DataBind();
                Session["AG_TA_AREAS_MANTENIMIENTO"] = vDatos;
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }

        }
        protected void GVAreas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Modifcar")
            {
                string vIdAreaModificar = e.CommandArgument.ToString();
                Session["AG_TA_ID_AREA_MODIFICAR"] = vIdAreaModificar;

                try
                {
                    String vQuery2 = " STEISP_AGENCIA_AreasMantenimiento 3," + vIdAreaModificar;
                    DataTable vDatos = vConexion.obtenerDataTable(vQuery2);
                    TxIdAreaModal.Text = vDatos.Rows[0]["idAreaAgencia"].ToString();
                    TxAreaModal.Text = vDatos.Rows[0]["nombre"].ToString();
                    DdlEstadoArea.SelectedValue = vDatos.Rows[0]["estado"].ToString();
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModalModificarArea();", true);
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
                if (DdlEstadoArea.SelectedValue == "True")
                { estado = "1"; }
                else
                {
                    estado = "0";
                }

                String vQuery3 = " STEISP_AGENCIA_AreasMantenimiento 4,"
                                   + Session["AG_TA_ID_AREA_MODIFICAR"] +
                                   ",'" + TxAreaModal.Text +
                                   "'," + estado;

                Int32 vInformacion3 = vConexion.ejecutarSql(vQuery3);

                if (vInformacion3 == 1)
                {
                    Mensaje("Area actualizado con exito. ", WarningType.Success);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModalModificarArea();", true);
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
                limpiarFormularioArea();
                Mensaje("Acción cancelado con exito. ", WarningType.Success);
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }


        }
        protected void GVAreas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GVAreas.PageIndex = e.NewPageIndex;
                GVAreas.DataSource = (DataTable)Session["AG_TA_AREAS_MANTENIMIENTO"];
                GVAreas.DataBind();
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
                DataTable vDatos = (DataTable)Session["AG_TA_AREAS_MANTENIMIENTO"];
                if (vBusqueda.Equals(""))
                {
                    GVAreas.DataSource = vDatos;
                    GVAreas.DataBind();
                    UpdatePanel5.Update();
                }
                else
                {
                    EnumerableRowCollection<DataRow> filtered = vDatos.AsEnumerable()
                        .Where(r => r.Field<String>("nombre").Contains(vBusqueda));

                    Boolean isNumeric = int.TryParse(vBusqueda, out int n);

                    if (isNumeric)
                    {
                        if (filtered.Count() == 0)
                        {
                            filtered = vDatos.AsEnumerable().Where(r =>
                                Convert.ToInt32(r["idAreaAgencia"]) == Convert.ToInt32(vBusqueda));
                        }
                    }

                    DataTable vDatosFiltrados = new DataTable();
                    vDatosFiltrados.Columns.Add("idAreaAgencia");
                    vDatosFiltrados.Columns.Add("nombre");
                    vDatosFiltrados.Columns.Add("estado");

                    foreach (DataRow item in filtered)
                    {
                        vDatosFiltrados.Rows.Add(
                            item["idAreaAgencia"].ToString(),
                            item["nombre"].ToString(),
                            item["estado"].ToString()
                            );
                    }

                    GVAreas.DataSource = vDatosFiltrados;
                    GVAreas.DataBind();
                    Session["AG_TA_AREAS_MANTENIMIENTO"] = vDatosFiltrados;
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