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

namespace Infatlan_STEI_ATM.pages.ATM
{
    public partial class update : System.Web.UI.Page
    {
        bd vConexion = new bd();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["UPDATEATM"] = null;
            if (!Page.IsPostBack)
            {
                cargarData();
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
                vDatos = vConexion.ObtenerTabla("STEISP_ATM_Generales 13, 1");
                GVBusqueda.DataSource = vDatos;
                GVBusqueda.DataBind();
                Session["ATM"] = vDatos;
                Session["UPDATEATM"] = 1;

            }
            catch (Exception Ex)
            {

            }
            
        }

        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e){
            try
            {
                DataTable vDataaaa = (DataTable)Session["ATM"];
                string codATMs = e.CommandArgument.ToString();


                if (e.CommandName == "Baja")
                {
                    try
                    {                      
                        string vQuery = "STEISP_ATM_Estado 1,'" + codATMs +"'";
                        Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                        if (vInfo == 1)
                        {                          
                            Mensaje("ATM fue dado de baja exitosamente", WarningType.Success);
                            UpdateDivBusquedas.Update();
                            Session["ATM"] = null;
                            cargarData();
                            TxBuscarATM.Text = string.Empty;
                        }
                        else
                        {
                            Mensaje("No se pudo dar de baja el ATM", WarningType.Warning);
                        }
                    }
                    catch (Exception Ex)
                    {
                        throw;
                    }
                }

                if (e.CommandName == "Modificar")
                {
                    try
                    {                
                    DataTable vDatos = new DataTable();
                    String vQuery = "STEISP_ATM_Estado 2,'" + codATMs + "'";
                    vDatos = vConexion.ObtenerTabla(vQuery);
                    foreach (DataRow item in vDatos.Rows)
                    {
                        Session["codATM"] = codATMs;
                        Session["nombreATM"] = item["nombreATM"].ToString();
                        Session["idEstado"] = item["idEstado"].ToString();
                        Session["sucursalATM"] = item["sucursalATM"].ToString();
                        Session["ubicacionATM"] = item["ubicacionATM"].ToString();
                        Session["tipoATM"] = item["tipoATM"].ToString();
                        Session["modeloATM"] = item["idModeloATM"].ToString();
                        Session["tipoCarga"] = item["tipoCargaATM"].ToString();
                        Session["procesador"] = item["procesadorATM"].ToString();
                        Session["teclado"] = item["tecladoATM"].ToString();
                        Session["serieATM"] = item["serieATM"].ToString();
                        Session["ram"] = item["ramATM"].ToString();
                        Session["so"] = item["idSO"].ToString();
                        Session["serieDisco"] = item["serieDiscoATM"].ToString();
                        Session["capacidadDisco"] = item["capacidadDiscoATM"].ToString();
                        Session["marca"] = item["idMarca"].ToString();
                        Session["ip"] = item["ipATM"].ToString();
                        Session["puerto"] = item["puertoATM"].ToString();
                        Session["latitud"] = item["latitudATM"].ToString();
                        Session["longitud"] = item["longitudATM"].ToString();
                        Session["direccion"] = item["direccionATM"].ToString();
                        Session["inventario"] = item["inventarioATM"].ToString();
                        Session["versionSw"] = item["idVersionSw"];
                        Session["DetalleModelo"] = item["modeloATM"].ToString();
                    }
                    TxBuscarATM.Text = string.Empty;
                    Response.Redirect("updateTotal.aspx");
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }


            }catch (Exception Ex){
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        protected void TxBuscarATM_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cargarData();

                String vBusqueda = TxBuscarATM.Text;
                DataTable vDatos = (DataTable)Session["ATM"];

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
                        .Where(r => r.Field<String>("Nombre").Contains(vBusqueda.ToUpper()));
                    
                    DataTable vDatosFiltrados = new DataTable();
                    vDatosFiltrados.Columns.Add("Codigo");
                    vDatosFiltrados.Columns.Add("Nombre");
                    vDatosFiltrados.Columns.Add("Inventario");
                    vDatosFiltrados.Columns.Add("Estado");
                    vDatosFiltrados.Columns.Add("Ubicacion");                  
                    foreach (DataRow item in filtered)
                    {
                        vDatosFiltrados.Rows.Add(
                            item["Codigo"].ToString(),
                            item["Nombre"].ToString(),
                            item["Inventario"].ToString(),
                            item["Estado"].ToString(),
                            item["Ubicacion"].ToString()                           
                            );
                    }

                    GVBusqueda.DataSource = vDatosFiltrados;
                    GVBusqueda.DataBind();
                    Session["ATM"] = vDatosFiltrados;
                    UpdateGridView.Update();
                }


            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        protected void GVBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e){
            try
            {
                GVBusqueda.PageIndex = e.NewPageIndex;
                GVBusqueda.DataSource = (DataTable)Session["ATM"];
                GVBusqueda.DataBind();
            }
            catch (Exception Ex)
            {

            }
        }
    }
}