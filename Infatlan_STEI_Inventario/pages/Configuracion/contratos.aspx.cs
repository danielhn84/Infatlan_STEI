using Infatlan_STEI_Inventario.clases;
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Infatlan_STEI_Inventario.pages.Configuracion
{
    public partial class contratos : System.Web.UI.Page
    {
        db vConexion = new db();
        Security vSecurity = new Security();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Convert.ToBoolean(Session["AUTH"]))
                {
                    if (vSecurity.ObtenerPermiso(Session["USUARIO"].ToString(), 1).Creacion)
                        BtnNuevo.Visible = true;

                    cargarDatos();
                }
                else
                {
                    Response.Redirect("/login.aspx");
                }
            }
        }

        private void cargarDatos()
        {
            try
            {
                String vQuery = "[STEISP_INVENTARIO_Contratos] 1";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0)
                {
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                    if (vSecurity.ObtenerPermiso(Session["USUARIO"].ToString(), 1).Edicion)
                    {
                        foreach (GridViewRow item in GVBusqueda.Rows)
                        {
                            LinkButton LbEdit = item.FindControl("BtnMover") as LinkButton;
                            LbEdit.Visible = true;
                        }
                    }
                    Session["INV_CONTRATOS"] = vDatos;
                }

                //PROVEEDORES
                vQuery = "STEISP_INVENTARIO_Generales 4";
                vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0)
                {
                    DDLProveedores.Items.Clear();
                    DDLProveedores.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLProveedores.Items.Add(new ListItem { Value = item["idProveedor"].ToString(), Text = item["nombre"].ToString() });
                    }
                }

                //TIPO CONTRATO
                vQuery = "STEISP_INVENTARIO_Generales 9";
                vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0)
                {
                    DDLTipoContrato.Items.Clear();
                    DDLTipoContrato.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLTipoContrato.Items.Add(new ListItem { Value = item["idTipoContrato"].ToString(), Text = item["nombre"].ToString() });
                    }
                }

            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        protected void TxBusqueda_TextChanged(object sender, EventArgs e)
        {

        }

        protected void BtnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                limpiarModal();
                LbIdContrato.Text = "Crear Nuevo Contrato";
                DivEstado.Visible = false;
                Session["INV_CONTRATO_ID"] = null;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        void limpiarModal()
        {
            TxContrato.Text = string.Empty;
            DDLTipoContrato.SelectedValue = "0";
            DDLProveedores.SelectedValue = "0";
            TxFechaInicio.Text = string.Empty;
            TxFechaFin.Text = string.Empty;
            TxCondiciones.Text = string.Empty;
            DDLEstado.SelectedValue = "1";
            DivMensaje.Visible = false;
        }

        protected void BtnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                validarDatos();
                String vQuery = "", vMensaje = "";
                int vInfo;
                DataTable vDatos = new DataTable();
                vQuery = "[STEISP_INVENTARIO_Contratos] {0}" +
                        "," + DDLProveedores.SelectedValue +
                        ",'" + TxContrato.Text.ToUpper() + "'" +
                        ",'" + TxFechaInicio.Text + "'" +
                        ",'" + TxFechaFin.Text + "'" +
                        ",'" + TxCondiciones.Text + "'" +
                        "," + DDLTipoContrato.SelectedValue +
                        ",'" + Session["USUARIO"].ToString() + "'" +
                        "," + DDLEstado.SelectedValue +
                        "{1}";

                if (HttpContext.Current.Session["INV_CONTRATO_ID"] == null)
                {
                    vQuery = string.Format(vQuery, "3", "");
                    vInfo = vConexion.ejecutarSql(vQuery);
                    vMensaje = "Contrato registrado con éxito";
                }
                else
                {
                    vQuery = string.Format(vQuery, "4", "," + Session["INV_CONTRATO_ID"].ToString());
                    vInfo = vConexion.ejecutarSql(vQuery);
                    vMensaje = "Contrato actualizado con éxito";
                }

                if (vInfo == 1)
                {
                    Mensaje(vMensaje, WarningType.Success);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "cerrarModal();", true);
                    cargarDatos();
                }
            }
            catch (Exception ex)
            {
                LbAdvertencia.Text = ex.Message;
                DivMensaje.Visible = true;
            }
        }

        private void validarDatos()
        {
            if (TxContrato.Text == "" || TxContrato.Text == string.Empty)
                throw new Exception("Favor ingrese el nombre del contrato.");
            if (DDLTipoContrato.SelectedValue == "0")
                throw new Exception("Favor seleccione tipo de contrato.");
            if (DDLProveedores.SelectedValue == "0")
                throw new Exception("Favor seleccione el proveedor.");
            if (TxFechaInicio.Text == "" || TxFechaInicio.Text == string.Empty)
                throw new Exception("Favor ingrese la fecha inicial del contrato.");
            if (TxFechaFin.Text == "" || TxFechaFin.Text == string.Empty)
                throw new Exception("Favor ingrese la fecha final del contrato.");
            if (TxCondiciones.Text == "" || TxCondiciones.Text == string.Empty)
                throw new Exception("Favor ingrese las condiciones del contrato.");
        }

        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string vIdContrato = e.CommandArgument.ToString();
                String vQuery = "[STEISP_INVENTARIO_Contratos] 2," + vIdContrato + "";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                if (e.CommandName == "EditarContrato")
                {
                    DivMensaje.Visible = false;
                    LbIdContrato.Text = "Editar contrato " + vDatos.Rows[0]["contrato"].ToString();
                    Session["INV_CONTRATO_ID"] = vIdContrato;
                    DivEstado.Visible = true;

                    for (int i = 0; i < vDatos.Rows.Count; i++)
                    {
                        TxContrato.Text = vDatos.Rows[i]["contrato"].ToString();
                        DDLTipoContrato.SelectedValue = vDatos.Rows[i]["idTipoContrato"].ToString();
                        DDLProveedores.SelectedValue = vDatos.Rows[i]["idProveedor"].ToString();
                        TxFechaInicio.Text = Convert.ToDateTime(vDatos.Rows[i]["fechaInicio"]).ToString("yyyy-MM-dd");
                        TxFechaFin.Text = Convert.ToDateTime(vDatos.Rows[i]["fechaFinal"]).ToString("yyyy-MM-dd"); ;
                        TxCondiciones.Text = vDatos.Rows[i]["condiciones"].ToString();
                        DDLEstado.SelectedValue = vDatos.Rows[i]["estado"].ToString();
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else if (e.CommandName == "verCondiciones")
                {
                    LbTituloCondicion.Text = "Condiciones del contrato - " + vDatos.Rows[0]["contrato"].ToString();
                    LbContenido.Text = vDatos.Rows[0]["condiciones"].ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalCond();", true);
                }
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void GVBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
    }
}