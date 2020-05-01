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

namespace Infatlan_STEI_ATM.pages.material
{
    public partial class buscarMaterial : System.Web.UI.Page
    {
        bd vConexion = new bd();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                cargarData();
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
                vDatos = vConexion.ObtenerTabla("STEISP_ATM_VERIFICACION 10, '" + Session["usuATM"].ToString() + "'");
                GVBusqueda.DataSource = vDatos;
                GVBusqueda.DataBind();
                Session["ATM_MATERIALES_MANTENIMIENTO"] = vDatos;            
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }
        protected void TxBuscarMantenimientoATM_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cargarData();

                String vBusqueda = TxBuscarMantenimientoATM.Text;
                DataTable vDatos = (DataTable)Session["ATM_MATERIALES_MANTENIMIENTO"];

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
                        .Where(r => r.Field<String>("NomATM").Contains(vBusqueda));

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
                    Session["ATM_MATERIALES_MANTENIMIENTO"] = vDatosFiltrados;
                    UpdateGridView.Update();
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
                GVBusqueda.DataSource = (DataTable)Session["ATM_MATERIALES_MANTENIMIENTO"];
                GVBusqueda.DataBind();
            }
            catch (Exception Ex)
            {

            }
        }

        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
                DataTable vDataaaa = (DataTable)Session["ATM_MATERIALES_MANTENIMIENTO"];
                string codVerif = e.CommandArgument.ToString();
                if (e.CommandName == "Aprobar")
                {
                    try
                    {
                    

                    DataTable vDatos = new DataTable();
                    String vQuery = "STEISP_ATM_VERIFICACION 2,'" + codVerif + "'";
                    vDatos = vConexion.ObtenerTabla(vQuery);
                    foreach (DataRow item in vDatos.Rows)
                        {

                            Session["ATM_CODATM_MATERIAL"] = item["Codigo"].ToString();
                            Session["ATM_NOMATM_MATERIAL"] = item["NomATM"].ToString();
                            Session["ATM_DIRECCION_MATERIAL"] = item["Direccion"].ToString();
                            Session["ATM_IP_MATERIAL"] = item["IP"].ToString();
                            Session["ATM_PUERTOATM_MATERIAL"] = item["Puerto"].ToString();
                            Session["ATM_TECLADO_MATERIAL"] = item["Teclado"].ToString();
                            Session["ATM_PROCESADOR_MATERIAL"] = item["Procesador"].ToString();
                            Session["ATM_TIPOCARGA_MATERIAL"] = item["TipoCarga"].ToString();
                            Session["ATM_MARCA_MATERIAL"] = item["Marca"].ToString();
                            Session["ATM_SERIEDISCO_MATERIAL"] = item["SerieDisco"].ToString();
                            Session["ATM_SERIEATM_MATERIAL"] = item["SerieATM"].ToString();
                            Session["ATM_CAPACIDADDISCO_MATERIAL"] = item["CapacidadDisco"].ToString();
                            Session["ATM_INVENTARIO_MATERIAL"] = item["Inventario"].ToString();
                            Session["ATM_RAM_MATERIAL"] = item["Ram"].ToString();
                            Session["ATM_LATITUD_MATERIAL"] = item["Latitud"].ToString();
                            Session["ATM_LONGITUD_MATERIAL"] = item["Longitud"].ToString();
                            Session["ATM_UBICACION_MATERIAL"] = item["Ubicacion"].ToString();
                            Session["ATM_IDUBI_MATERIAL"] = item["IdUbi"].ToString();
                            Session["ATM_SUCURSAL_MATERIAL"] = item["Sucursal"].ToString();
                            Session["ATM_DEPTO_MATERIAL"] = item["Departamento"].ToString();
                            Session["ATM_ZONA_MATERIAL"] = item["Zona"].ToString();
                            Session["ATM_IDMANT_MATERIAL"] = codVerif;
                            Session["ATM_ESTADO_MATERIAL"] = item["Estado"].ToString();
                            Session["ATM_FECHAMANT_MATERIAL"] = Convert.ToDateTime(item["FechaMantenimiento"]).ToString("yyyy/MM/dd");
                            Session["ATM_HRINICIO_MATERIAL"] = item["HrInicio"].ToString();
                            Session["ATM_HRFIN_MATERIAL"] = item["HrFin"].ToString();
                            Session["ATM_AUTORIZADO_MATERIAL"] = item["Autorizado"].ToString();
                            Session["ATM_SYSAID_MATERIAL"] = item["SysAid"].ToString();
                            Session["ATM_TECNICO_MATERIAL"] = item["Tecnico"].ToString();
                            Session["ATM_USUARIO_MATERIAL"] = item["Usuario"].ToString();
                            Session["ATM_IDENTIDAD_MATERIAL"] = item["Identidad"].ToString();
                            Session["ATM_SO_MATERIAL"] = item["SO"].ToString();
                            Session["ATM_VERSIONSW_MATERIAL"] = item["VersionSw"].ToString();
                            Session["ATM_USUCORREO_MATERIAL"] = item["CorreoTecnico"].ToString();
                            Session["ATM_USUCREADOR_MATERIAL"] = item["UsuarioCreador"].ToString();
                            Session["ATM_CODUBI_MATERIAL"] = item["CodigoUbi"].ToString();
                            Session["ATM_COMENTARIOAPRO_MATERIAL"] = item["ComentarioAprobarMateriales"].ToString();
                            Session["ATM_INVUBI_MATERIAL"] = item["InvUbi"].ToString();
                            Session["ATM_CHOFER_MATERIAL"] = item["Chofer"].ToString();
                            Session["ATM_IDCHOFER_MATERIAL"] = item["IDChofer"].ToString();


                    }

                        Response.Redirect("material.aspx");
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
        }
    }
}