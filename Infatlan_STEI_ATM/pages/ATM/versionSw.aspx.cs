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
    public partial class versionSw : System.Web.UI.Page
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
            H5Alerta1.Visible = false;
            H5Alerta2.Visible = false;
            txtAlerta1.Visible = false;
            txtAlerta2.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal2();", true);
        }

        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            H5Alerta2.Visible = false;
            H5Alerta1.Visible = false;
            txtAlerta1.Visible = false;
            txtAlerta2.Visible = false;

            DataTable vDataa = (DataTable)Session["versionATM"];
            string codversionATMs = e.CommandArgument.ToString();


            if (e.CommandName == "Codigo")
            {
              
               
                try
                {
                    DataTable vDatos = new DataTable();
                    String vQuery = "STEISP_ATMAdminComponentesATM 19,'" + codversionATMs + "'";
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
                txtAlerta1.Visible = true;
                H5Alerta1.Visible = true;
            }
            else
            {
               
                try
                {
                    string vQuery = "STEISP_ATMAdminComponentesATM 21, '" + Session["codversionATM"] + "','" + txtModalNewVersionATM.Text + "', '" + Session["usuATM"].ToString() + "'";
                    Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    if (vInfo == 1)
                    {
                        H5Alerta1.Visible = false;
                        txtModalNewVersionATM.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
                        Mensaje("Versión del software modificado con éxito", WarningType.Success);
                        UpdateGridView.Update();
                        cargarData();
                    }
                    else
                    {
                       H5Alerta1.InnerText="No se pudo modificar la versión del software";
                        H5Alerta1.Visible = true;
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
           
            if (txtNewVersionATM.Text == "" || txtNewVersionATM.Text == string.Empty)
            {
                txtAlerta2.Visible = true;
                H5Alerta2.Visible = true;
            }
            else
            {
                try
                {
                    string vQuery = "STEISP_ATMAdminComponentesATM 20, '" + Session["codsoATM"] + "','" + txtNewVersionATM.Text + "','" + Session["usuATM"].ToString() + "'";
                    Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    if (vInfo == 1)
                    {
                        H5Alerta2.Visible = false;
                        txtNewVersionATM.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal2();", true);
                        Mensaje("Versión del software creada con éxito", WarningType.Success);
                        UpdateGridView.Update();
                        cargarData();

                    }
                    else
                    {
                        H5Alerta2.InnerText = "No se pudo crear la versión del software";
                        H5Alerta2.Visible = true;
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