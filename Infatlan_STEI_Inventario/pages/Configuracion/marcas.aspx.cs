using Infatlan_STEI_Inventario.clases;
using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Infatlan_STEI_Inventario.pages.Configuracion
{
    public partial class marcas : System.Web.UI.Page
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
                String vQuery = "[STEISP_INVENTARIO_Generales] 5";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0)
                {
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                    if (vSecurity.ObtenerPermiso(Session["USUARIO"].ToString(), 1).Edicion)
                    {
                        foreach (GridViewRow item in GVBusqueda.Rows)
                        {
                            LinkButton LbEdit = item.FindControl("BtnEditar") as LinkButton;
                            LbEdit.Visible = true;
                        }
                    }
                    if (vSecurity.ObtenerPermiso(Session["USUARIO"].ToString(), 1).Borrado)
                    {
                        foreach (GridViewRow item in GVBusqueda.Rows)
                        {
                            LinkButton LbDelete = item.FindControl("BtnBorrar") as LinkButton;
                            LbDelete.Visible = true;
                        }
                    }
                    Session["INV_MARCAS"] = vDatos;
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
            try
            {
                cargarDatos();
                String vBusqueda = TxBusqueda.Text;
                DataTable vDatos = (DataTable)Session["INV_MARCAS"];
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
                                Convert.ToInt32(r["idMarca"]) == Convert.ToInt32(vBusqueda));
                        }
                    }

                    DataTable vDatosFiltrados = new DataTable();
                    vDatosFiltrados.Columns.Add("idMarca");
                    vDatosFiltrados.Columns.Add("nombre");
                    vDatosFiltrados.Columns.Add("estado");
                    vDatosFiltrados.Columns.Add("fechaCreacion");

                    foreach (DataRow item in filtered)
                    {
                        vDatosFiltrados.Rows.Add(
                            item["idMarca"].ToString(),
                            item["nombre"].ToString(),
                            item["estado"].ToString(),
                            item["fechaCreacion"].ToString()
                            );
                    }

                    GVBusqueda.DataSource = vDatosFiltrados;
                    GVBusqueda.DataBind();
                    Session["INV_MARCAS"] = vDatosFiltrados;
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
                GVBusqueda.DataSource = (DataTable)Session["INV_MARCAS"];
                GVBusqueda.DataBind();

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
                LbIdMarca.Text = "Crear Nueva Marca";
                DivEstado.Visible = false;
                Session["INV_MARCA_ID"] = null;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);
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
                vQuery = "STEISP_INVENTARIO_Marcas {0}" +
                        ",'" + TxNombre.Text.ToUpper() + "'" +
                        ",'',{1}";

                if (HttpContext.Current.Session["INV_MARCA_ID"] == null)
                {
                    vQuery = string.Format(vQuery, "3", "1");
                    vInfo = vConexion.ejecutarSql(vQuery);
                    vMensaje = "Marca registrada con éxito";
                }
                else
                {
                    vQuery = string.Format(vQuery, "4", DDLEstado.SelectedValue + "," + Session["INV_MARCA_ID"].ToString());
                    vInfo = vConexion.ejecutarSql(vQuery);
                    vMensaje = "Marca actualizada con éxito";
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
            if (TxNombre.Text == "" || TxNombre.Text == string.Empty)
                throw new Exception("Favor ingrese el nombre de la marca.");
        }

        void limpiarModal()
        {
            TxNombre.Text = string.Empty;
            DivMensaje.Visible = false;
        }

        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DataTable vDatos = new DataTable();
                String vQuery = "";
                string vIdMarca = e.CommandArgument.ToString();

                if (e.CommandName == "EditarMarca")
                {
                    DivMensaje.Visible = false;
                    LbIdMarca.Text = "Editar Marca " + vIdMarca;
                    Session["INV_MARCA_ID"] = vIdMarca;
                    DivEstado.Visible = true;
                    vQuery = "[STEISP_INVENTARIO_Marcas] 2," + vIdMarca + "";
                    vDatos = vConexion.obtenerDataTable(vQuery);

                    for (int i = 0; i < vDatos.Rows.Count; i++)
                    {
                        TxNombre.Text = vDatos.Rows[i]["nombre"].ToString();
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else if (e.CommandName == "EliminarMarca")
                {
                    LbTitulo.Text = "Eliminar Marca?";
                    LbMensaje.Text = "No podrá reversar los cambios.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ModalConfirmar();", true);
                }
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void BtnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
    }
}