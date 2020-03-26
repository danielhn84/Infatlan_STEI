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
    public partial class versionSwATM : System.Web.UI.Page
    {
        bd vConexion = new bd();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["VERSIONSW_ATM"] = null;
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
            if (HttpContext.Current.Session["VERSIONSW_ATM"] == null)
            {
                try
                {
                    DataTable vDatos = new DataTable();
                    vDatos = vConexion.ObtenerTabla("STEISP_ATM_Generales 10, 1");
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                    Session["versionATM"] = vDatos;


                }
                catch (Exception Ex)
                {

                }
                Session["VERSIONSW_ATM"] = 1;
            }
        }
        protected void btnnewversionATM_Click(object sender, EventArgs e)
        {
            lbversion1.Visible = false;
            lbversion2.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal2();", true);
        }

        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            lbversion2.Visible = false;
            lbversion1.Visible = false;

            DataTable vDataa = (DataTable)Session["versionATM"];
            string codversionATMs = e.CommandArgument.ToString();


            if (e.CommandName == "Codigo")
            {
                string nom = "";
                string usu = "acedillo";
                try
                {
                    DataTable vDatos = new DataTable();
                    String vQuery = "STEISP_ATMAdminComponentesATM 19,'" + codversionATMs + "', '" + nom + "', '" + usu + "'";
                    vDatos = vConexion.ObtenerTabla(vQuery);
                    foreach (DataRow item in vDatos.Rows)
                    {
                        Session["codversionATM"] = codversionATMs;
                        Session["nombreversionATM"] = item["nombreVersion"].ToString();
                    }
                }
                catch (Exception)
                {

                    throw;
                }

                lbcodversionATM.Text = codversionATMs;
                lbNombreversionATM.Text = Session["nombreversionATM"].ToString();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);
            }
        }

        protected void btnModalEnviarVersionATM_Click(object sender, EventArgs e)
        {
            if (txtModalNewVersionATM.Text == "" || txtModalNewVersionATM.Text == string.Empty)
            {
               lbversion1.Text="Ingrese el nuevo Sistema Operativo";
                lbversion1.Visible = true;
            }
            else
            {
                string usu = "acedillo";
                try
                {
                    string vQuery = "STEISP_ATMAdminComponentesATM 21, '" + Session["codversionATM"] + "','" + txtModalNewVersionATM.Text + "', '" + usu + "'";
                    Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    if (vInfo == 1)
                    {
                        lbversion1.Visible = false;
                        txtModalNewVersionATM.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
                        Mensaje("Versión del software modificado con éxito", WarningType.Success);
                        UpdateGridView.Update();
                        cargarData();
                    }
                    else
                    {
                       lbversion1.Text="No se pudo modificar la versión del software";
                        lbversion1.Visible = true;
                    }
                }
                catch (Exception Ex)
                {
                    throw;
                }
            }
        }

        protected void btnModalCerrarVersionATM_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
        }

        protected void btnModalNueviVersionATM_Click(object sender, EventArgs e)
        {
            string usu = "acedillo";
            if (txtNewVersionATM.Text == "" || txtNewVersionATM.Text == string.Empty)
            {
                lbversion2.Text = "Ingrese la nueva versión del software";
                lbversion2.Visible = true;
            }
            else
            {
                try
                {
                    string vQuery = "STEISP_ATMAdminComponentesATM 20, '" + Session["codsoATM"] + "','" + txtNewVersionATM.Text + "','" + usu + "'";
                    Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    if (vInfo == 1)
                    {
                        lbversion2.Visible = false;
                        txtNewVersionATM.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal2();", true);
                        Mensaje("Versión del software creada con éxito", WarningType.Success);
                        UpdateGridView.Update();
                        cargarData();

                    }
                    else
                    {
                        lbversion2.Text = "No se pudo crear la versión del software";
                        lbversion2.Visible = true;
                    }
                }
                catch (Exception Ex)
                {
                    throw;
                }
            }
        }

        protected void btnModalCerrarNueviVersionATM_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal2();", true);
        }
    }
}