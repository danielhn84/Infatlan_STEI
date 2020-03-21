using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using Infatlan_STEI_ATM.clases;

namespace Infatlan_STEI_ATM.pagesATM
{
    public partial class buscarAprobarVerificacionATM : System.Web.UI.Page
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
                vDatos = vConexion.ObtenerTabla("STEISP_ATM_Generales 22, 1");
                GVBusqueda.DataSource = vDatos;
                GVBusqueda.DataBind();
                Session["ATM_APROBVERIF_CARGAR"] = vDatos;
                Session["UPDATEATM"] = 1;

            }
            catch (Exception Ex)
            {

            }
        }
        protected void TxBuscarTecnicoATM_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cargarData();

                String vBusqueda = TxBuscarTecnicoATM.Text;
                DataTable vDatos = (DataTable)Session["ATM_APROBVERIF_CARGAR"];

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
                    Session["ATM_APROBVERIF_CARGAR"] = vDatosFiltrados;
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
                GVBusqueda.DataSource = (DataTable)Session["ATM_APROBVERIF_CARGAR"];
                GVBusqueda.DataBind();
            }
            catch (Exception Ex)
            {

            }
        }

        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
                DataTable vDataaaa = (DataTable)Session["ATM_APROBVERIF_CARGAR"];
                string codVerificacion = e.CommandArgument.ToString();

                if (e.CommandName == "Aprobar")
                {
                    try
                    {
                        DataTable vDatos = new DataTable();
                        String vQuery = "STEISP_ATM_Generales 23,'" + codVerificacion + "'";
                        vDatos = vConexion.ObtenerTabla(vQuery);
                        foreach (DataRow item in vDatos.Rows)
                        {
                            Session["ATM_CODVERIF"] = codVerificacion;
                            Session["ATM_NOMBREVERIF"] = item["NomATM"].ToString();
                            Session["ATM_DIRECCIONVERIF"] = item["Direccion"].ToString();
                            Session["ATM_IPVERIF"] = item["IP"].ToString();
                            Session["ATM_UBICACIONVERIF"] = item["Ubicacion"].ToString();
                            Session["ATM_SUCURSALVERIF"] = item["Sucursal"].ToString();
                            Session["ATM_ZONAVERIF"] = item["Zona"].ToString();
                            Session["ATM_FECHAMANTVERIF"] = item["FechaMantenimiento"].ToString();
                            Session["ATM_HRINICIOVERIF"] = item["HrInicio"].ToString();
                            Session["ATM_HRFINVERIF"] = item["HrFin"].ToString();
                            Session["ATM_AUTORIZADOVERIF"] = item["Autorizado"].ToString();
                            Session["ATM_CANCELARVERIF"] = item["Cancelado"].ToString();
                            Session["ATM_SYSAIDVERIF"] = item["SysAid"].ToString();
                            Session["ATM_TECNICOVERIF"] = item["Tecnico"].ToString();
                            Session["ATM_USUARIOVERIF"] = item["Usuario"].ToString();
                            Session["ATM_IDENTIDADVERIF"] = item["Identidad"].ToString();
                            Session["ATM_CODATMVERIF"] = item["Codigo"].ToString();
                            Session["ATM_OBSERVACIONESVERIF"]=item["ObservacionesVerif"].ToString();
                            Session["ATM_HRSALIDAINFAVERIF"] = item["HoraSalidaInfa"].ToString();
                            Session["ATM_HRENTRADAINFAVERIF"] = item["HoraEntradaInfa"].ToString();
                        }

                         DataTable vDatos2 = new DataTable();
                         String vQuery2 = "STEISP_ATM_Generales 25,'" + codVerificacion + "'";
                        vDatos2 = vConexion.ObtenerTabla(vQuery2);
                        foreach (DataRow item2 in vDatos2.Rows)
                        {
                        Session["ATM_IDPREGUNTAVERIF"] = item2["pregunta"].ToString(); 
                        Session["ATM_RESPUESTAVERIF"] = item2["respuesta"].ToString();
                        Session["ATM_IMGVERIF"] = item2["imagen"].ToString();
                        Session["ATM_COMENTARIOPREGUNTASVERIF"] = item2["comentario"].ToString();                      
                        }
                        TxBuscarTecnicoATM.Text = string.Empty;
                        Response.Redirect("aprobarVerificacionATM.aspx");
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            
        }
    }
}