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
        bd vConexionATM = new bd();
        Security vSecurity = new Security();
        protected void Page_Load(object sender, EventArgs e){
            Session["UPDATEATM"] = null;
            if (!Page.IsPostBack){
                if (Convert.ToBoolean(Session["AUTH"])){
                    cargarData();
                }else {
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
        
        void cargarData(){
            try{
                DataTable vDatos = new DataTable();
                vDatos = vConexionATM.ObtenerTablaATM("SPSTEI_ATM 2");
                GVBusqueda.DataSource = vDatos;
                GVBusqueda.DataBind();
               
                if (vSecurity.ObtenerPermiso(Session["USUARIO"].ToString(), 3).Edicion){
                    foreach (GridViewRow item in GVBusqueda.Rows){
                        //LinkButton LbEdit = item.FindControl("btnbajaATM") as LinkButton;
                        LinkButton LbEdit2 = item.FindControl("btnmodificarATM") as LinkButton;
                        //LbEdit.Visible = true;
                        LbEdit2.Visible = true;
                    }
                }
                 Session["ATM"] = vDatos;
                Session["UPDATEATM"] = 1;

            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
            
        }

        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e){
            try
            {
                DataTable vDataaaa = (DataTable)Session["ATM"];
                string codATMs = e.CommandArgument.ToString();


                if (e.CommandName == "Baja")
                {
                    //try
                    //{                      
                    //    string vQuery = "STEISP_ATM_Estado 1,'" + codATMs +"'";
                    //    Int32 vInfo = vConexion.ejecutarSQL(vQuery);

                    //    //VALIDA QUE ATM ESTE ACTIVO
                    //    String vQuery2 = "STEISP_ATM_VERIFICACION 8, '" + codATMs + "',1";
                    //    DataTable vDatos2 = vConexion.ObtenerTabla(vQuery2);
                    //    if (vInfo == 1)
                    //    {                          
                    //        Mensaje("ATM fue dado de baja exitosamente", WarningType.Success);
                    //        UpdateDivBusquedas.Update();
                    //        Session["ATM"] = null;
                    //        cargarData();
                    //        TxBuscarATM.Text = string.Empty;
                    //    }
                    //    else
                    //    {
                    //        Mensaje("No se pudo dar de baja el ATM", WarningType.Warning);
                    //    }
                    //}
                    //catch (Exception Ex)
                    //{
                    //    throw;
                    //}
                }

                if (e.CommandName == "Modificar")
                {
                    Session["codATM"] = codATMs;
                    TxBuscarATM.Text = string.Empty;
                    Response.Redirect("updateTotal.aspx");

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
                        .Where(r => r.Field<String>("Nombre").Contains(vBusqueda));

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