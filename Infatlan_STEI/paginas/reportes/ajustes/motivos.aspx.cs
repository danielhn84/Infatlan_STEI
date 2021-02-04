using Infatlan_STEI.classes;
using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Infatlan_STEI.paginas.reportes.ajustes
{
    public partial class motivos : System.Web.UI.Page
    {
        db vConexion = new db();
        Security vSecurity = new Security();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Convert.ToBoolean(Session["AUTH"]))
                {
                    if (!vSecurity.ObtenerPermiso(Session["USUARIO"].ToString(), 5).Consulta)
                        Response.Redirect("/default.aspx");
                    if (vSecurity.ObtenerPermiso(Session["USUARIO"].ToString(), 5).Creacion)
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
                String vQuery = "";

                if (DDLProceso.SelectedValue == "0")
                    vQuery = "[STEISP_CUMPLIMIENTO_Ajustes] 1";
                else if (DDLProceso.SelectedValue == "4")
                    vQuery = "[STEISP_CUMPLIMIENTO_Ajustes] 6," + DDLProceso.SelectedValue;
                else if (DDLProceso.SelectedValue == "5")
                    vQuery = "[STEISP_CUMPLIMIENTO_Ajustes] 6," + DDLProceso.SelectedValue;

                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                if (vDatos.Rows.Count > 0)
                {
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                    if (vSecurity.ObtenerPermiso(Session["USUARIO"].ToString(), 5).Edicion)
                    {
                        foreach (GridViewRow item in GVBusqueda.Rows)
                        {
                            LinkButton LbEdit = item.FindControl("BtnEditar") as LinkButton;
                            LbEdit.Visible = true;
                        }
                    }
                    Session["CUMPL_MOTIVOS"] = vDatos;
                }

                //SECCIONES
                vQuery = "[STEISP_CUMPLIMIENTO_Ajustes] 2";
                vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0)
                {
                    DDLSecciones.Items.Clear();
                    DDLSecciones.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLSecciones.Items.Add(new ListItem { Value = item["idSeccion"].ToString(), Text = item["nombre"].ToString() });
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

        protected void DDLProceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cargarDatos();
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void TxBusqueda_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cargarDatos();
                String vBusqueda = TxBusqueda.Text;
                DataTable vDatos = (DataTable)Session["CUMPL_MOTIVOS"];
                if (vBusqueda.Equals(""))
                {
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                }
                else
                {
                    EnumerableRowCollection<DataRow> filtered = vDatos.AsEnumerable()
                        .Where(r => r.Field<String>("nombre").Contains(vBusqueda.ToUpper()));

                    Boolean isNumeric = int.TryParse(vBusqueda, out int n);

                    if (isNumeric)
                    {
                        if (filtered.Count() == 0)
                        {
                            filtered = vDatos.AsEnumerable().Where(r =>
                                Convert.ToInt32(r["idMotivo"]) == Convert.ToInt32(vBusqueda));
                        }
                    }

                    DataTable vDatosFiltrados = new DataTable();
                    vDatosFiltrados.Columns.Add("idMotivo");
                    vDatosFiltrados.Columns.Add("nombre");
                    vDatosFiltrados.Columns.Add("idSeccion");
                    vDatosFiltrados.Columns.Add("estado");

                    foreach (DataRow item in filtered)
                    {
                        vDatosFiltrados.Rows.Add(
                            item["idMotivo"].ToString(),
                            item["nombre"].ToString(),
                            item["idSeccion"].ToString(),
                            item["estado"].ToString()
                        );
                    }

                    GVBusqueda.DataSource = vDatosFiltrados;
                    GVBusqueda.DataBind();
                    Session["CUMPL_MOTIVOS"] = vDatosFiltrados;
                }

            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void BtnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                limpiarModal();
                LbTituloModal.Text = "Crear Nuevo Motivo";
                DivEstado.Visible = false;
                Session["CUMPL_MOTIVO_ID"] = null;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        void limpiarModal()
        {
            TxNombre.Text = string.Empty;
            DDLEstado.SelectedValue = "1";
            DDLSecciones.SelectedValue = "0";
            DDLProceso.SelectedValue = "0";
            DivMensaje.Visible = false;
        }

        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string vIdMotivo = e.CommandArgument.ToString();
                if (e.CommandName == "EditarMotivo")
                {
                    DivMensaje.Visible = false;
                    LbTituloModal.Text = "Editar Motivo " + vIdMotivo;
                    Session["CUMPL_MOTIVO_ID"] = vIdMotivo;
                    DivEstado.Visible = true;

                    String vQuery = "[STEISP_CUMPLIMIENTO_Ajustes] 3," + vIdMotivo + "";
                    DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                    for (int i = 0; i < vDatos.Rows.Count; i++)
                    {
                        TxNombre.Text = vDatos.Rows[i]["nombre"].ToString();
                        DDLSecciones.SelectedValue = vDatos.Rows[i]["idSeccion"].ToString();
                        DDLEstado.SelectedValue = vDatos.Rows[i]["estado"].ToString();
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void GVBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GVBusqueda.PageIndex = e.NewPageIndex;
                GVBusqueda.DataSource = (DataTable)Session["CUMPL_MOTIVOS"];
                GVBusqueda.DataBind();
                if (vSecurity.ObtenerPermiso(Session["USUARIO"].ToString(), 5).Edicion)
                {
                    foreach (GridViewRow item in GVBusqueda.Rows)
                    {
                        LinkButton LbEdit = item.FindControl("BtnEditar") as LinkButton;
                        LbEdit.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void BtnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                validarDatos();
                String vQuery = "", vMensaje = "";
                int vInfo;
                DataTable vDatos = new DataTable();
                vQuery = "[STEISP_CUMPLIMIENTO_Ajustes] {0}" +
                        "," + DDLSecciones.SelectedValue +
                        ",'" + TxNombre.Text.ToString().ToUpper() + "'" +
                        "," + DDLEstado.SelectedValue +
                        ",'" + Session["USUARIO"].ToString() + "'";

                if (HttpContext.Current.Session["CUMPL_MOTIVO_ID"] == null)
                {
                    vQuery = string.Format(vQuery, "4");
                    vInfo = vConexion.ejecutarSql(vQuery);
                    vMensaje = "Curso registrado con éxito.";
                }
                else
                {
                    vQuery = string.Format(vQuery, "5," + Session["CUMPL_MOTIVO_ID"].ToString());
                    vInfo = vConexion.ejecutarSql(vQuery);
                    vMensaje = "Curso actualizado con éxito.";
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
            if (TxNombre.Text == string.Empty || TxNombre.Text == "")
                throw new Exception("Favor ingrese el nombre del motivo.");
            if (DDLSecciones.SelectedValue == "0")
                throw new Exception("Favor seleccione la sección.");
        }
    }
}