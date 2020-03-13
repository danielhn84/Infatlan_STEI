﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using Infatlan_STEI_ATM.clases;

namespace Infatlan_STEI_ATM.pagesATM
{
    public partial class buscarAprobarNotificacionATM : System.Web.UI.Page
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
                Session["AprobNotifATM"] = vDatos;
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
                GVBusqueda.DataSource = (DataTable)Session["AprobNotifATM"];
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
                DataTable vDataaaa = (DataTable)Session["AprobNotifATM"];
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
                            Session["NomATM"] = item["NomATM"].ToString();
                            Session["direccionATM"] = item["Direccion"].ToString();
                            Session["IPATM"] = item["IP"].ToString();
                            Session["ubicacionATM"] = item["Ubicacion"].ToString();
                            Session["SucursalATM"] = item["Sucursal"].ToString();
                            Session["zonaATM"] = item["Zona"].ToString();
                            Session["fechaMantATM"] = item["FechaMantenimiento"].ToString();
                            Session["hrInicioATM"] = item["HrInicio"].ToString();
                            Session["hrfinATM"] = item["HrFin"].ToString();
                            Session["autorizadoATM"] = item["Autorizado"].ToString();
                            Session["cancelaATM"] = item["Cancelado"].ToString();
                            Session["sysaidATM"] = item["SysAid"].ToString();
                            Session["tecnicoATM"] = item["Tecnico"].ToString();
                            Session["usuario"] = item["Usuario"].ToString();
                            Session["identidad"] = item["Identidad"].ToString();
                            Session["codATM"] = item["Codigo"].ToString();
                        }
                        TxBuscarTecnicoATM.Text = string.Empty;
                        Response.Redirect("aprobarNotificacionATM.aspx");
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
                DataTable vDatos = (DataTable)Session["AprobNotifATM"];

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
                    Session["AprobNotifATM"] = vDatosFiltrados;
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