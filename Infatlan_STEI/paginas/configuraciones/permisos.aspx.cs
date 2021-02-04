using Infatlan_STEI.classes;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Infatlan_STEI.paginas.configuraciones
{
    public partial class permisos : System.Web.UI.Page
    {
        db vConexion = new db();
        Security vSecurity = new Security();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Convert.ToBoolean(Session["AUTH"]))
                {
                    if (!vSecurity.ObtenerPermiso(Session["USUARIO"].ToString(), 6).Consulta)
                        Response.Redirect("/default.aspx");
                    if (vSecurity.ObtenerPermiso(Session["USUARIO"].ToString(), 6).Edicion)
                        BtnAceptar.Visible = true;
                    cargarDatos();
                }
            }
        }

        private void cargarDatos()
        {
            try
            {
                String vQuery = "[STEISP_INVENTARIO_Generales] 13";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0)
                {
                    DDLUsuarios.Items.Clear();
                    DDLUsuarios.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLUsuarios.Items.Add(new ListItem { Value = item["idUsuario"].ToString(), Text = item["nombre"].ToString() + " " + item["apellidos"].ToString() });
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

        public void MensajeBlock(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        protected void BtnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                validarDatos();
                DataTable vDatos = (DataTable)Session["STEI_PERMISOS"];
                String vQuery = "[STEISP_Permisos] 6,'" + DDLUsuarios.SelectedValue + "'";
                DataTable vData = vConexion.obtenerDataTable(vQuery);
                int vInfo = 0;

                if (vData.Rows[0][0].ToString() == "0")
                {
                    int vCuenta = 0;

                    foreach (GridViewRow row in GVBusqueda.Rows)
                    {
                        CheckBox CBConsulta = row.Cells[2].FindControl("CBxConsulta") as CheckBox;
                        CheckBox CBCrear = row.Cells[2].FindControl("CBxCrear") as CheckBox;
                        CheckBox CBEditar = row.Cells[2].FindControl("CBxEditar") as CheckBox;
                        CheckBox CBBorrar = row.Cells[2].FindControl("CBxBorrar") as CheckBox;

                        vQuery = "[STEISP_Permisos] 1,'" + DDLUsuarios.SelectedValue + "'" +
                        "," + vDatos.Rows[row.RowIndex]["idAplicacion"].ToString() +
                        ",'" + Session["USUARIO"].ToString() + "'" +
                        "," + CBConsulta.Checked +
                        "," + CBCrear.Checked +
                        "," + CBEditar.Checked +
                        "," + CBBorrar.Checked;
                        vInfo = vConexion.ejecutarSql(vQuery);
                        vCuenta++;
                    }
                    if (vCuenta == vDatos.Rows.Count)
                    {
                        cargarDatos();
                        GVBusqueda.DataSource = null;
                        GVBusqueda.DataBind();
                        Mensaje("Permisos ingresados con éxito.", WarningType.Success);
                    }
                }
                else
                {
                    int vCuenta = 0;
                    foreach (GridViewRow row in GVBusqueda.Rows)
                    {
                        CheckBox CBConsulta = row.Cells[2].FindControl("CBxConsulta") as CheckBox;
                        CheckBox CBCrear = row.Cells[2].FindControl("CBxCrear") as CheckBox;
                        CheckBox CBEditar = row.Cells[2].FindControl("CBxEditar") as CheckBox;
                        CheckBox CBBorrar = row.Cells[2].FindControl("CBxBorrar") as CheckBox;

                        vQuery = "[STEISP_Permisos] 2,'" + DDLUsuarios.SelectedValue + "'" +
                        "," + vDatos.Rows[row.RowIndex]["idAplicacion"].ToString() +
                        ",'" + CBConsulta.Checked + "'" +
                        ",'" + CBCrear.Checked + "'" +
                        ",'" + CBEditar.Checked + "'" +
                        ",'" + CBBorrar.Checked + "'" +
                        ",'" + Session["USUARIO"].ToString() + "'";
                        vInfo = vConexion.ejecutarSql(vQuery);
                        vCuenta++;
                    }

                    vQuery = "[STEISP_Permisos] 5";
                    vData = vConexion.obtenerDataTable(vQuery);
                    if (vData.Rows.Count != vDatos.Rows.Count)
                    {
                        for (int i = 0; i < vData.Rows.Count; i++)
                        {
                            if (vDatos.Rows.Count < i + 1)
                            {
                                vQuery = "[STEISP_Permisos] 1,'" + DDLUsuarios.SelectedValue + "'" +
                                    "," + vData.Rows[i]["idAplicacion"].ToString() +
                                    ",'" + Session["USUARIO"].ToString() + "'" +
                                    ",'false'" +
                                    ",'false'" +
                                    ",'false'" +
                                    ",'false'";
                                vInfo = vConexion.ejecutarSql(vQuery);
                                vCuenta++;
                            }
                        }
                    }

                    if (vCuenta == vData.Rows.Count)
                    {
                        cargarDatos();
                        GVBusqueda.DataSource = null;
                        GVBusqueda.DataBind();
                        Mensaje("Permisos actualizados con éxito.", WarningType.Success);
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        private void validarDatos()
        {
            if (DDLUsuarios.SelectedValue == "0")
                throw new Exception("Favor seleccione el usuario");
        }

        protected void DDLUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String vQuery = DDLUsuarios.SelectedValue == "0" ? "[STEISP_Permisos] 5" : "[STEISP_Permisos] 3,'" + DDLUsuarios.SelectedValue + "'";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0)
                {
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                    Session["STEI_PERMISOS"] = vDatos;
                }
                else
                {
                    vQuery = "[STEISP_Permisos] 5";
                    vDatos = vConexion.obtenerDataTable(vQuery);

                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                    Session["STEI_PERMISOS"] = vDatos;
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}