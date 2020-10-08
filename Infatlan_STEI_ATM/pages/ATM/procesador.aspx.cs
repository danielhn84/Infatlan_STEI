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
        bd vConexionATM = new bd();
        Security vSecurity = new Security();
        protected void Page_Load(object sender, EventArgs e){
            Session["PROCESADOR_ATM"] = null;
            if (!Page.IsPostBack){
                if (Convert.ToBoolean(Session["AUTH"])){
                    if (vSecurity.ObtenerPermiso(Session["USUARIO"].ToString(), 3).Creacion)
                        btnguardarProcesadorATM.Visible = true;

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
                    vDatos = vConexionATM.ObtenerTablaATM("SPSTEI_ATM 6");
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                    if (vSecurity.ObtenerPermiso(Session["USUARIO"].ToString(), 3).Edicion){
                        foreach (GridViewRow item in GVBusqueda.Rows){
                            LinkButton LbEdit = item.FindControl("BtnEditar") as LinkButton;
                            LbEdit.Visible = true;
                        }
                    }
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
           
            txtAlerta1.Visible = false;
            txtAlerta2.Visible = false;
            DataTable vDataa = (DataTable)Session["procesadorATM"];
            string codProcesadorATMs = e.CommandArgument.ToString();


            if (e.CommandName == "Codigo")
            {
              
                
                try
                {
                    DataTable vDatos = new DataTable();
                    String vQuery = "SPSTEI_ATM 21,'" + codProcesadorATMs + "'";
                    vDatos = vConexionATM.ObtenerTablaATM(vQuery);
                    foreach (DataRow item in vDatos.Rows)
                    {
                        Session["codprocesadorATM"] = codProcesadorATMs;
                        Session["nombreprocesadorATM"] = item["Descripcion"].ToString();
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
           
            txtAlerta1.Visible = false;
            txtAlerta2.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal2();", true);
           
        }

        protected void btnModalCerrarprocesadorATM_Click(object sender, EventArgs e)
        {
           
            txtAlerta1.Visible = false;
            txtAlerta2.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
        }

        protected void btnModalEnviarprocesadorATM_Click(object sender, EventArgs e)
        {
            if (txtModalNewprocesadorATM.Text == "" || txtModalNewprocesadorATM.Text == string.Empty)
            {
                txtAlerta1.Visible = true;
            }
            else
            {
                
                try
                {
                    string vQuery = "SPSTEI_ATM 22, '" + Session["codprocesadorATM"] + "','" + txtModalNewprocesadorATM.Text + "'";
                    Int32 vInfo = vConexionATM.ejecutarSQLATM(vQuery);
                    if (vInfo == 1)
                    {
                        txtAlerta1.Visible = false;
                        txtModalNewprocesadorATM.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
                        Mensaje("Tipo de carga ATM modificado con éxito", WarningType.Success);
                        UpdateGridView.Update();
                        cargarData();
                    }
                    else
                    {
                       txtAlerta1.Text="No se pudo modificar el procesador ATM";
                        txtAlerta1.Visible = true;
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
                txtAlerta2.Visible = true;
            }
            else
            {
                try
                {
                    string vQuery = "SPSTEI_ATM 23,'" + txtNewProcesadorATM.Text + "'";
                    Int32 vInfo = vConexionATM.ejecutarSQLATM(vQuery);
                    if (vInfo == 1)
                    {
                        txtAlerta2.Visible = false;
                        txtNewProcesadorATM.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal2();", true);
                        Mensaje("Procesador ATM creada con éxito", WarningType.Success);
                        UpdateGridView.Update();
                        cargarData();

                    }
                    else
                    {
                       txtAlerta2.Text="No se pudo crear elprocesador ATM";
                        txtAlerta2.Visible = true;
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