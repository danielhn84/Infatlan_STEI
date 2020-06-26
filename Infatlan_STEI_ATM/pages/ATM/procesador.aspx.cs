using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using Infatlan_STEI_ATM.clases;

namespace Infatlan_STEI_ATM.pages.ATM
{
    public partial class procesador : System.Web.UI.Page
    {
        bd vConexion = new bd();
        protected void Page_Load(object sender, EventArgs e){
            Session["PROCESADOR_ATM"] = null;
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
            if (HttpContext.Current.Session["PROCESADOR_ATM"] == null)
            {
                try
                {
                    DataTable vDatos = new DataTable();
                    vDatos = vConexion.ObtenerTabla("STEISP_ATM_Generales 5, 1");
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                    Session["procesadorATM"] = vDatos;


                }
                catch (Exception Ex)
                {

                }
                Session["PROCESADOR_ATM"] = 1;
            }
        }
        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            H5Alerta1.Visible = false;
            H5Alerta2.Visible = false;
            txtAlerta1.Visible = false;
            txtAlerta2.Visible = false;
            DataTable vDataa = (DataTable)Session["procesadorATM"];
            string codProcesadorATMs = e.CommandArgument.ToString();


            if (e.CommandName == "Codigo")
            {
              
                
                try
                {
                    DataTable vDatos = new DataTable();
                    String vQuery = "STEISP_ATMAdminComponentesATM 10,'" + codProcesadorATMs + "'";
                    vDatos = vConexion.ObtenerTabla(vQuery);
                    foreach (DataRow item in vDatos.Rows)
                    {
                        Session["codprocesadorATM"] = codProcesadorATMs;
                        Session["nombreprocesadorATM"] = item["nombreProcesadorATM"].ToString();
                    }
                }
                catch (Exception)
                {

                    throw;
                }

                lbcodprocesadorATM.Text = codProcesadorATMs;
                lbNombreprocesadorATM.Text = Session["nombreprocesadorATM"].ToString();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);
            }
        }

        protected void btnguardarProcesadorATM_Click(object sender, EventArgs e)
        {
            H5Alerta1.Visible = false;
            H5Alerta2.Visible = false;
            txtAlerta1.Visible = false;
            txtAlerta2.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal2();", true);
           
        }

        protected void btnModalCerrarprocesadorATM_Click(object sender, EventArgs e)
        {
            H5Alerta1.Visible = false;
            H5Alerta2.Visible = false;
            txtAlerta1.Visible = false;
            txtAlerta2.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
        }

        protected void btnModalEnviarprocesadorATM_Click(object sender, EventArgs e)
        {
            if (txtModalNewprocesadorATM.Text == "" || txtModalNewprocesadorATM.Text == string.Empty)
            {
              
                H5Alerta1.Visible = true;
                txtAlerta1.Visible = true;
            }
            else
            {
                
                try
                {
                    string vQuery = "STEISP_ATMAdminComponentesATM 12, '" + Session["codprocesadorATM"] + "','" + txtModalNewprocesadorATM.Text + "', '" + Session["USUARIO"].ToString() + "'";
                    Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    if (vInfo == 1)
                    {
                        H5Alerta1.Visible = false;
                        txtModalNewprocesadorATM.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
                        Mensaje("Tipo de carga ATM modificado con éxito", WarningType.Success);
                        UpdateGridView.Update();
                        cargarData();
                    }
                    else
                    {
                       H5Alerta1.InnerText="No se pudo modificar el procesador ATM";
                        H5Alerta1.Visible = true;
                    }
                }
                catch (Exception Ex)
                {
                    throw;
                }
            }
        }

        protected void btnModalNueviProcesadorATM_Click(object sender, EventArgs e)
        {
            
            if (txtNewProcesadorATM.Text == "" || txtNewProcesadorATM.Text == string.Empty)
            {
              
                H5Alerta2.Visible = true;
                txtAlerta2.Visible = true;
            }
            else
            {
                try
                {
                    string vQuery = "STEISP_ATMAdminComponentesATM 11, '" + Session["codprocesadorATM"] + "','" + txtNewProcesadorATM.Text + "','" + Session["USUARIO"].ToString() + "'";
                    Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    if (vInfo == 1)
                    {
                        H5Alerta2.Visible = false;
                        txtNewProcesadorATM.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal2();", true);
                        Mensaje("Procesador ATM creada con éxito", WarningType.Success);
                        UpdateGridView.Update();
                        cargarData();

                    }
                    else
                    {
                       H5Alerta2.InnerText="No se pudo crear elprocesador ATM";
                        H5Alerta2.Visible = true;
                    }
                }
                catch (Exception Ex)
                {
                    throw;
                }
            }
        }

        protected void btnModalCerrarNueviProcesadorATM_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal2();", true);
        }
    }
}