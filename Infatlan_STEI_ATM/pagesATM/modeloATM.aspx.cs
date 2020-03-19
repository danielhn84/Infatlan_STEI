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
namespace Infatlan_STEI_ATM.pagesATM
{
    public partial class modeloATM : System.Web.UI.Page
    { 
        bd vConexion = new bd();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["MODELO_ATM"] = null;
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
            if (HttpContext.Current.Session["MODELO_ATM"] == null)
            {
                try
                {
                    DataTable vDatos = new DataTable();
                    vDatos = vConexion.ObtenerTabla("STEISP_ATM_Generales 2, 1");
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                    Session["modeloATM"] = vDatos;
                    Session["UPDATEATM"] = 1;

                }
                catch (Exception Ex)
                {

                }
                Session["MODELO_ATM"] = 1;
            }

        }
        protected void GVBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            lbmodelo1.Visible = false;
            lbmodelo2.Visible = false;
            DataTable vDataa = (DataTable)Session["modeloATM"];
            string codmodeloATMs = e.CommandArgument.ToString();


            if (e.CommandName == "Codigo")
            {
                string nom = "";
                string usu = "acedillo";
                try
                {
                    DataTable vDatos = new DataTable();
                    String vQuery = "STEISP_ATMAdminComponentesATM 4,'" + codmodeloATMs + "', '" + nom + "', '" + usu + "'";
                    vDatos = vConexion.ObtenerTabla(vQuery);
                    foreach (DataRow item in vDatos.Rows)
                    {
                        Session["codmodeloATM"] = codmodeloATMs;
                        Session["nombremodeloATM"] = item["nombreModeloATM"].ToString();
                    }
                }
                catch (Exception)
                {

                    throw;
                }

                lbcodmodeloATM.Text = codmodeloATMs;
                lbNombremodeloATM.Text = Session["nombremodeloATM"].ToString();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);
            }

        }

        protected void btnguardarmodeloATM_Click(object sender, EventArgs e)
        {
            lbmodelo1.Visible = false;
            lbmodelo2.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal2();", true);
           
        }

        protected void btnModalEnviarModeloATM_Click(object sender, EventArgs e)
        {
            if (txtModalNewModeloATM.Text == "" || txtModalNewModeloATM.Text == string.Empty)
            {
               lbmodelo1.Text="Ingrese nuevo modelo ATM";
                lbmodelo1.Visible = true;
                lbmodelo2.Visible = true;
            }
            else
            {
                string usu = "acedillo";
                try
                {
                    string vQuery = "STEISP_ATMAdminComponentesATM 6, '" + Session["codmodeloATM"] + "','" + txtModalNewModeloATM.Text + "', '" + usu + "'";
                    Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    if (vInfo == 1)
                    {

                        txtModalNewModeloATM.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
                        Mensaje("Modelo de ATM modificado con éxito", WarningType.Success);
                        UpdateGridView.Update();
                        cargarData();
                        lbmodelo1.Visible = false;
                        lbmodelo2.Visible = false;
                    }
                    else
                    {
                        lbmodelo1.Text = "No se pudo modificar el modelo";
                        lbmodelo1.Visible = true;
                        lbmodelo2.Visible = true;
                    }
                }
                catch (Exception Ex)
                {
                    throw;
                }
            }
        }

        protected void btnModalCerrarModeloATM_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
        }

        protected void btnModalNueviModeloATM_Click(object sender, EventArgs e)
        {
            string usu = "acedillo";
            if (txtNewModeloATM.Text == "" || txtNewModeloATM.Text == string.Empty)
            {
               lbmodelo2.Text="Ingrese nuevo modelo ATM";
                lbmodelo2.Visible = true;
            }
            else
            {
                try
                {
                    string vQuery = "STEISP_ATMAdminComponentesATM 5, '" + Session["codmodeloATM"] + "','" + txtNewModeloATM.Text + "','" + usu + "'";
                    Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    if (vInfo == 1)
                    {
                        lbmodelo2.Visible = false;
                        txtNewModeloATM.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal2();", true);
                        Mensaje("Modelo de ATM creada con éxito", WarningType.Success);
                        UpdateGridView.Update();
                        cargarData();

                    }
                    else
                    {
                       lbmodelo2.Text="No se pudo crear el modelo";
                    }
                }
                catch (Exception Ex)
                {
                    throw;
                }
            }
        }

        protected void btnModalCerrarNueviModeloATM_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal2();", true);
        }
    }
}