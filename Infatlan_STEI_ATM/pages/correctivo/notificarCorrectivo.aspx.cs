using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using Infatlan_STEI_ATM.clases;

namespace Infatlan_STEI_ATM.pages.correctivo
{
    public partial class notificarCorrectivo : System.Web.UI.Page
    {
        bd vConexionATM = new bd();
        bd vConexion = new bd();
        Security vSecurity = new Security();
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

        int CargarInformacionDDL(DropDownList vList, String vValue)
        {
            int vIndex = 0;
            try
            {
                int vContador = 0;
                foreach (ListItem item in vList.Items)
                {
                    if (item.Value.Equals(vValue))
                    {
                        vIndex = vContador;
                    }
                    vContador++;
                }
            }
            catch { throw; }
            return vIndex;
        }

        void cargarData()
        {
            try
            {
                DataTable vDatos = new DataTable();
                vDatos = vConexionATM.ObtenerTablaATM("SPSTEI_ATM 37");
                GVBusqueda.DataSource = vDatos;
                GVBusqueda.DataBind();

                if (vSecurity.ObtenerPermiso(Session["USUARIO"].ToString(), 3).Edicion)
                {
                    foreach (GridViewRow item in GVBusqueda.Rows)
                    {
                        //LinkButton LbEdit = item.FindControl("btnbajaATM") as LinkButton;
                        LinkButton LbEdit2 = item.FindControl("btnmodificarATM") as LinkButton;
                        //LbEdit.Visible = true;
                        LbEdit2.Visible = true;
                    }
                }
                Session["ATMCorrectivo"] = vDatos;
                Session["UPDATEATM"] = 1;

            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }

        }

        protected void TxBuscarATM_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cargarData();

                String vBusqueda = TxBuscarATM.Text.ToUpper().ToString();
                DataTable vDatos = (DataTable)Session["ATMCorrectivo"];

                if (vBusqueda.Equals(""))
                {
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                    //cargarData();
                }
                else
                {
                    EnumerableRowCollection<DataRow> filtered = vDatos.AsEnumerable()
                        .Where(r => r.Field<String>("Codigo").Contains(vBusqueda));

                    DataTable vDatosFiltrados = new DataTable();
                    vDatosFiltrados.Columns.Add("Codigo");
                    vDatosFiltrados.Columns.Add("Nombre");
                    vDatosFiltrados.Columns.Add("TipoATM");
                    vDatosFiltrados.Columns.Add("Estado");
                    foreach (DataRow item in filtered)
                    {
                        vDatosFiltrados.Rows.Add(
                            item["Codigo"].ToString(),
                            item["Nombre"].ToString(),
                            item["TipoATM"].ToString(),
                            item["Estado"].ToString()
                            );
                    }

                    GVBusqueda.DataSource = vDatosFiltrados;
                    GVBusqueda.DataBind();
                    Session["ATMCorrectivo"] = vDatosFiltrados;
                }


            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        protected void GVBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GVBusqueda.PageIndex = e.NewPageIndex;
                GVBusqueda.DataSource = (DataTable)Session["ATMCorrectivo"];
                GVBusqueda.DataBind();
            }
            catch (Exception Ex)
            {

            }
        }

        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string vEstado = "";
                string codATMs = e.CommandArgument.ToString();             
                if (e.CommandName == "Modificar")
                {
                    DataTable vDatos = new DataTable();
                    String vQuery = "STEISP_ATM_NotificacionCorrectivo 3,'" + codATMs + "'";
                    vDatos = vConexion.ObtenerTabla(vQuery);
                    foreach (DataRow item in vDatos.Rows)
                    {
                        vEstado = item["estado"].ToString();
                    }

                    if (vEstado == "5" || vEstado == "4" || vEstado=="")
                    {
                        TxBuscarATM.Text = string.Empty;
                        Response.Redirect("mantCorrectivoNotificacion.aspx?cod=" + codATMs);
                    }            
                    else
                    {
                        Mensaje("¡ALERTA! ATM "+ codATMs+" ya está en proceso de mantenimiento, no puede seleccionarlo hasta finalizar mantenimiento pendiente.",WarningType.Danger);
                    }
                }


            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }
    }
}