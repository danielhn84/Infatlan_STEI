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

namespace Infatlan_STEI_ATM
{
    public partial class buscarVerificacionATM : System.Web.UI.Page
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
            string usu = "acedillo";
            try
            {
                DataTable vDatos = new DataTable();
                vDatos = vConexion.ObtenerTabla("STEISP_ATM_VERIFICACION 1, '"+usu+"','"+Session["COD_VERIFMANTE_ATM"]+"'");
                GVBusqueda.DataSource = vDatos;
                GVBusqueda.DataBind();
                Session["ATM_VERIF_CARGAR"] = vDatos;
                //Session["UPDATEATM"] = 1;

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
                DataTable vDatos = (DataTable)Session["ATM_VERIF_CARGAR"];

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
                    Session["ATM_VERIF_CARGAR"] = vDatosFiltrados;
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
                GVBusqueda.DataSource = (DataTable)Session["ATM_VERIF_CARGAR"];
                GVBusqueda.DataBind();
            }
            catch (Exception Ex)
            {

            }
        }

        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string usu = "acedillo";
            try
            {
                DataTable vDataaaa = (DataTable)Session["ATM_VERIF_CARGAR"];
                string codVerif = e.CommandArgument.ToString();

                if (e.CommandName == "Aprobar")
                {
                    try
                    {
                        DataTable vDatos = new DataTable();
                        vDatos = vConexion.ObtenerTabla("STEISP_ATM_VERIFICACION 2, '" + usu + "','" + codVerif + "'");
                        //vDatos = vConexion.ObtenerTabla(vQuery);
                        foreach (DataRow item in vDatos.Rows)
                        {
                            Session["ATM_COD_VERIF_CREAR"] = codVerif;
                            //lbModalNomATM.Text = item["NomATM"].ToString();
                            //lbModalFechaMan.Text = Convert.ToDateTime(item["FechaMantenimiento"]).ToString("yyyy/MM/dd");
                            //lbModalCodATM.Text = item["Codigo"].ToString();
                        }
                        TxBuscarTecnicoATM.Text = string.Empty;
                       
                        Response.Redirect("verificacionATM.aspx");
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
    }
}