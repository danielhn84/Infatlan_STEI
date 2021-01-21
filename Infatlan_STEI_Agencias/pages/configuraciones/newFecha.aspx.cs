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
    public partial class newFecha : System.Web.UI.Page
    {
        db vConexion = new db();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Convert.ToBoolean(Session["AUTH"]))
                {
                    cargarData();
                }
                else
                {
                    Response.Redirect("/login.aspx");
                }
            }
        }

        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        void cargarData()
        {

            try
            {
                DataTable vDatos = new DataTable();
                vDatos = vConexion.obtenerDataTable("STEISP_AGENCIA_Generales 8");
                GVMantenimientos.DataSource = vDatos;
                GVMantenimientos.DataBind();
       
                Session["AG_CAMBIAR_FECHA"] = vDatos;
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }

        }

        protected void TxBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cargarData();

                String vBusqueda = TxBuscar.Text.ToUpper().ToString();
                DataTable vDatos = (DataTable)Session["AG_CAMBIAR_FECHA"];

                if (vBusqueda.Equals(""))
                {
                    GVMantenimientos.DataSource = vDatos;
                    GVMantenimientos.DataBind();

                }
                else
                {
                    EnumerableRowCollection<DataRow> filtered = vDatos.AsEnumerable()
                        .Where(r => r.Field<String>("Agencia").Contains(vBusqueda));

                    DataTable vDatosFiltrados = new DataTable();
                    vDatosFiltrados.Columns.Add("ID");
                    vDatosFiltrados.Columns.Add("Agencia");
                    vDatosFiltrados.Columns.Add("Fecha");
                    foreach (DataRow item in filtered)
                    {
                        vDatosFiltrados.Rows.Add(
                            item["ID"].ToString(),
                            item["Agencia"].ToString(),
                            item["Fecha"].ToString()
                            );
                    }

                    GVMantenimientos.DataSource = vDatosFiltrados;
                    GVMantenimientos.DataBind();
                    Session["AG_CAMBIAR_FECHA"] = vDatosFiltrados;
                }


            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        protected void GVMantenimientos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GVMantenimientos.PageIndex = e.NewPageIndex;
                GVMantenimientos.DataSource = (DataTable)Session["AG_CAMBIAR_FECHA"];
                GVMantenimientos.DataBind();
            }
            catch (Exception Ex)
            {

            }
        }

        protected void GVMantenimientos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string codMantenimiento = e.CommandArgument.ToString();
                txtMotivoCambio.Text = "";
                txtNewFecha.Text = "";
                txtAlerta2.Visible = false;
                if (e.CommandName == "Select")
                {
                    H4Titulo.InnerText = "Cambiar fecha de Mantenimiento-" + codMantenimiento;
                    DataTable vDatos = new DataTable();
                    String vQuery = "STEISP_AGENCIAS_CancelarMantenimiento 10,'" + codMantenimiento + "'";
                    vDatos = vConexion.obtenerDataTable(vQuery);
                    foreach (DataRow item in vDatos.Rows)
                    {
                        Session["ID_MANTENIMIENTO_CAMBIO_AG"] = codMantenimiento;
                        Session["FECHA_MANTENIMIENTO_CAMBIO_AG"] = item["fechaMantenimiento"].ToString();
                    }
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);
                }
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            txtAlerta2.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
        }

        protected void btnNuevaFecha_Click(object sender, EventArgs e)
        {
            if (txtMotivoCambio.Text == "" || txtNewFecha.Text == "")
            {
                txtAlerta2.Visible = true;
            }
            else
            {
                String vFormato = "yyyy/MM/dd"; //"dd/MM/yyyy HH:mm:ss"
                String vNewFecha = Convert.ToDateTime(txtNewFecha.Text).ToString(vFormato);
                String vOriginalFecha = Convert.ToDateTime(Session["FECHA_MANTENIMIENTO_CAMBIO_AG"]).ToString(vFormato);

                string vQuery = "STEISP_AGENCIAS_CancelarMantenimiento 8, '" + Session["ID_MANTENIMIENTO_CAMBIO_AG"] + "','" + vOriginalFecha + "'," +
                    "'" + vNewFecha + "','" + txtMotivoCambio.Text + "','" + Session["USUARIO"] + "'";
                Int32 vInfo = vConexion.ejecutarSql(vQuery);
                if (vInfo != 0)
                {
                    string estado = "";
                    if (Convert.ToDateTime(txtNewFecha.Text) >= DateTime.Now)
                        estado = "1";
                    else
                        estado = "13";

                    string vQuery2 = "STEISP_AGENCIAS_CancelarMantenimiento 9, '" + Session["ID_MANTENIMIENTO_CAMBIO_AG"] + "','" + estado + "'";
                    vConexion.ejecutarSql(vQuery2);

                    txtMotivoCambio.Text = "";
                    txtNewFecha.Text = "";
                    txtAlerta2.Visible = false;
                    cargarData();
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
                    Mensaje("Se cambió fecha exitósamente.", WarningType.Success);
                }
            }
        }
    }
}