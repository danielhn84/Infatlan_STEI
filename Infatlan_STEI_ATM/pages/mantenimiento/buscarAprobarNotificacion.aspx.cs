using Infatlan_STEI_ATM.clases;
using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Infatlan_STEI_ATM.pages.mantenimiento
{
    public partial class buscarAprobarNotificacion : System.Web.UI.Page
    {
        bd vConexion = new bd();
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
                vDatos = vConexion.ObtenerTabla("STEISP_ATM_Generales 14, 1");
                GVBusqueda.DataSource = vDatos;
                GVBusqueda.DataBind();
                Session["ATM_APROBNOTIF_CARGAR"] = vDatos;
                Session["UPDATEATM"] = 1;

            }
            catch (Exception Ex)
            {

            }

        }

        protected void GVBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GVBusqueda.PageIndex = e.NewPageIndex;
                GVBusqueda.DataSource = (DataTable)Session["ATM_APROBNOTIF_CARGAR"];
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
                DataTable vDataaaa = (DataTable)Session["ATM_APROBNOTIF_CARGAR"];
                string codNotificacion = e.CommandArgument.ToString();

                if (e.CommandName == "Aprobar")
                {
                    try
                    {
                        DataTable vDatos = new DataTable();
                        String vQuery = "STEISP_ATM_Generales 15,'" + codNotificacion + "'";
                        vDatos = vConexion.ObtenerTabla(vQuery);
                        foreach (DataRow item in vDatos.Rows)
                        {
                            Session["codNotificacion"] = codNotificacion;
                            Session["NomATM"] = item["Codigo"].ToString();
                            Session["direccionATM"] = item["Direccion"].ToString();
                            Session["IPATM"] = item["IP"].ToString();
                            Session["ubicacionATM"] = item["Ubicacion"].ToString();
                            Session["SucursalATM"] = item["Sucursal"].ToString();
                            Session["zonaATM"] = item["Zona"].ToString();
                            Session["IDZona"] = item["IDZona"].ToString();
                            Session["fechaMantATM"] = item["FechaMantenimiento"].ToString();
                            Session["hrInicioATM"] = item["HrInicio"].ToString();
                            Session["hrfinATM"] = item["HrFin"].ToString();
                            Session["autorizadoATM"] = item["Autorizado"].ToString();
                            Session["cancelaATM"] = item["Cancelado"].ToString();
                            Session["sysaidATM"] = item["SysAid"].ToString();
                            Session["tecnicoATM"] = item["Tecnico"].ToString();
                            Session["usuarioR"] = item["Usuario"].ToString();
                            Session["identidad"] = item["Identidad"].ToString();
                            Session["codATM"] = item["Codigo"].ToString();
                            Session["emailEmpleadoATM"] = item["Correo"].ToString();
                            Session["USUCREADORATM"] = item["UsuarioCreador"].ToString();
                            Session["Usu_Responsable"] = item["Responsable"].ToString();

                        }


                        DataTable vDatos2 = new DataTable();
                        String vQuery2 = "STEISP_AGENCIA_CreacionNotificacion 6,'" + Session["USUCREADORATM"].ToString() + "'";
                        vDatos2 = vConexion.ObtenerTabla(vQuery2);
                        foreach (DataRow item in vDatos2.Rows)
                        {
                            Session["ATM_NOMBRECREADOR"] = item["nombre"].ToString();
                            Session["ATM_APELLIDOCREADOR"] = item["apellidos"].ToString();
                            Session["ATM_CORREOCREADOR"] = item["correo"].ToString();
                        }


                        TxBuscarTecnicoATM.Text = string.Empty;
                        Response.Redirect("notificacion.aspx?id=2&tipo=3");
                        //Response.Redirect("aprobarNotificacion.aspx");
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }


            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        protected void TxBuscarTecnicoATM_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cargarData();

                String vBusqueda = TxBuscarTecnicoATM.Text;
                DataTable vDatos = (DataTable)Session["ATM_APROBNOTIF_CARGAR"];

                if (vBusqueda.Equals(""))
                {
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                    UpdateGridView.Update();
                    //cargarData();
                }
                else
                {
                    EnumerableRowCollection<DataRow> filtered = vDatos.AsEnumerable()
                        .Where(r => r.Field<String>("Tecnico").Contains(vBusqueda));

                    DataTable vDatosFiltrados = new DataTable();
                    vDatosFiltrados.Columns.Add("ID");
                    vDatosFiltrados.Columns.Add("Codigo");
                    vDatosFiltrados.Columns.Add("NomATM");
                    vDatosFiltrados.Columns.Add("Ubicacion");
                    vDatosFiltrados.Columns.Add("Sucursal");
                    vDatosFiltrados.Columns.Add("Tecnico");
                    foreach (DataRow item in filtered)
                    {
                        vDatosFiltrados.Rows.Add(
                            item["ID"].ToString(),
                            item["Codigo"].ToString(),
                            item["NomATM"].ToString(),
                            item["Ubicacion"].ToString(),
                            item["Sucursal"].ToString(),
                            item["Tecnico"].ToString()
                            );
                    }

                    GVBusqueda.DataSource = vDatosFiltrados;
                    GVBusqueda.DataBind();
                    Session["ATM_APROBNOTIF_CARGAR"] = vDatosFiltrados;
                    UpdateGridView.Update();
                }


            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }
    }
}