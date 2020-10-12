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
    public partial class modelo : System.Web.UI.Page
    { 
        bd vConexion = new bd();
        bd vConexionATM = new bd();
        Security vSecurity = new Security();
        protected void Page_Load(object sender, EventArgs e){
            Session["MODELO_ATM"] = null;
            if (!Page.IsPostBack){
                if (Convert.ToBoolean(Session["AUTH"])){
                    if (vSecurity.ObtenerPermiso(Session["USUARIO"].ToString(), 3).Creacion)
                        btnguardarmodeloATM.Visible = true;

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
            if (HttpContext.Current.Session["MODELO_ATM"] == null)
            {
                try
                {
                    DataTable vDatos = new DataTable();
                    vDatos = vConexionATM.ObtenerTablaATM("SPSTEI_ATM 10");
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                    if (vSecurity.ObtenerPermiso(Session["USUARIO"].ToString(), 3).Edicion){
                        foreach (GridViewRow item in GVBusqueda.Rows){
                            LinkButton LbEdit = item.FindControl("BtnEditar") as LinkButton;
                            LbEdit.Visible = true;
                        }
                    }
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
            //H5Alerta1.Visible = false;
            //H5Alerta2.Visible = false;
            txtAlerta1.Visible = false;
            txtAlerta2.Visible = false;
            DataTable vDataa = (DataTable)Session["modeloATM"];
            string codmodeloATMs = e.CommandArgument.ToString();


            if (e.CommandName == "Codigo")
            {
                
                
                try
                {
                    DataTable vDatos = new DataTable();
                    String vQuery = "SPSTEI_ATM 15,'" + codmodeloATMs + "'";
                    vDatos = vConexionATM.ObtenerTablaATM(vQuery);
                    foreach (DataRow item in vDatos.Rows)
                    {
                        Session["codmodeloATM"] = codmodeloATMs;
                        Session["nombremodeloATM"] = item["Descripcion"].ToString();
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
            //H5Alerta1.Visible = false;
            //H5Alerta2.Visible = false;
            txtAlerta1.Visible = false;
            txtAlerta2.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal2();", true);
           
        }

        protected void btnModalEnviarModeloATM_Click(object sender, EventArgs e)
        {
            if (txtModalNewModeloATM.Text == "" || txtModalNewModeloATM.Text == string.Empty)
            {
              
                //H5Alerta1.Visible = true;
                //H5Alerta2.Visible = true;
                txtAlerta1.Visible = true;
            }
            else
            {
               
                try
                {
                    string vQuery = "SPSTEI_ATM 16, '" + Session["codmodeloATM"] + "','" + txtModalNewModeloATM.Text + "'";
                    Int32 vInfo = vConexionATM.ejecutarSQLATM(vQuery);
                    if (vInfo == 1)
                    {

                        txtModalNewModeloATM.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
                        Mensaje("Modelo de ATM modificado con éxito", WarningType.Success);
                        UpdateGridView.Update();
                        cargarData();
                        txtAlerta1.Visible = false;
                        txtAlerta2.Visible = false;
                    }
                    else
                    {
                        txtAlerta1.Text = "No se pudo modificar el modelo";
                        txtAlerta1.Visible = true;
                       
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
            txtAlerta1.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
        }

        protected void btnModalNueviModeloATM_Click(object sender, EventArgs e)
        {
            
            if (txtNewModeloATM.Text == "" || txtNewModeloATM.Text == string.Empty)
            {
              
               
                txtAlerta2.Visible = true;
            }
            else
            {
                try
                {
                    string vQuery = "SPSTEI_ATM 17, '" + txtNewModeloATM.Text + "'";
                    Int32 vInfo = vConexionATM.ejecutarSQLATM(vQuery);
                    if (vInfo == 1)
                    {
                        txtAlerta2.Visible = false;
                        txtNewModeloATM.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal2();", true);
                        Mensaje("Modelo de ATM creada con éxito", WarningType.Success);
                        UpdateGridView.Update();
                        cargarData();

                    }
                    else
                    {
                      txtAlerta2.Text="No se pudo crear el modelo";
                        txtAlerta2.Visible = true;
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
            txtAlerta2.Visible = false;
            
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal2();", true);
        }
    }
}