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
    public partial class marcas : System.Web.UI.Page
    {
        bd vConexion = new bd();
        protected void Page_Load(object sender, EventArgs e){
            Session["MARCAS_ATM"] = null;
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
        void cargarData()
        {
            if (HttpContext.Current.Session["MARCAS_ATM"] == null)
            {
                try
                {
                    DataTable vDatos = new DataTable();
                    vDatos = vConexion.ObtenerTabla("STEISP_ATM_Generales 11, 1");
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                    Session["marcaATM"] = vDatos;


                }
                catch (Exception Ex)
                {

                }
                Session["MARCAS_ATM"] = 1;
            }
        }
        protected void btnnewmarcasATM_Click(object sender, EventArgs e)
        {
            H5Alerta2.Visible = false;
            H5Alerta.Visible = false;
            txtAlerta1.Visible = false;
            txtAlerta2.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal2();", true);
        }

        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            H5Alerta.Visible = false;
            H5Alerta2.Visible = false;
            txtAlerta1.Visible = false;
            txtAlerta2.Visible = false;
            DataTable vDataa = (DataTable)Session["soATM"];
            string codmarca = e.CommandArgument.ToString();


            if (e.CommandName == "Codigo")
            {
               
                
                try
                {
                    DataTable vDatos = new DataTable();
                    String vQuery = "STEISP_ATMAdminComponentesATM 22,'" + codmarca + "'";
                    vDatos = vConexion.ObtenerTabla(vQuery);
                    foreach (DataRow item in vDatos.Rows)
                    {
                        Session["codmarca"] = codmarca;
                        Session["nombremarca"] = item["nombreMarca"].ToString();
                    }
                }
                catch (Exception)
                {

                    throw;
                }

                lbcodmarcaATM.Text = codmarca;
                lbNombremarcaATM.Text = Session["nombreMarca"].ToString();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);
            }
        }

        protected void btnModalEnviarMarcaATM_Click(object sender, EventArgs e)
        {
            if (txtModalNewMarcaATM.Text == "" || txtModalNewMarcaATM.Text == string.Empty)
            {
                H5Alerta2.Visible = true;
                txtAlerta1.Visible = true;
            }
            else
            {
               
                try
                {
                    string vQuery = "STEISP_ATMAdminComponentesATM 24, '" + Session["codmarca"] + "','" + txtModalNewMarcaATM.Text + "', '" + Session["usuATM"].ToString() + "'";
                    Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    if (vInfo == 1)
                    {
                        H5Alerta2.Visible = false;
                        txtModalNewMarcaATM.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
                        Mensaje("Marca modificado con éxito", WarningType.Success);
                        UpdateGridView.Update();
                        cargarData();
                    }
                    else
                    {
                        H5Alerta2.InnerText = "No se pudo modificar la marca";
                        H5Alerta2.Visible = true;
                    }
                }
                catch (Exception Ex)
                {
                    throw;
                }
            }
        }

        protected void btnModalCerrarMarcaATM_Click(object sender, EventArgs e)
        {
            txtAlerta1.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
        }

        protected void btnModalNueviMarcaATM_Click(object sender, EventArgs e)
        {
            
            if (txtNewMarcaATM.Text == "" || txtNewMarcaATM.Text == string.Empty)
            {
                txtAlerta2.Visible = true;
                H5Alerta.Visible = true;
            }
            else
            {
                try
                {
                    string vQuery = "STEISP_ATMAdminComponentesATM 23, '" + Session["codmarca"] + "','" + txtNewMarcaATM.Text + "','" + Session["usuATM"].ToString() + "'";
                    Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    if (vInfo == 1)
                    {
                        H5Alerta.Visible = false;
                        txtNewMarcaATM.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal2();", true);
                        Mensaje("Marca creada con éxito", WarningType.Success);
                        UpdateGridView.Update();
                        cargarData();

                    }
                    else
                    {
                        H5Alerta.InnerText="No se pudo crear la marca";
                        H5Alerta.Visible = true;
                    }
                }
                catch (Exception Ex)
                {
                    throw;
                }
            }
        }

        protected void btnModalCerrarNueviMarcaATM_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal2();", true);
        }
    }
}