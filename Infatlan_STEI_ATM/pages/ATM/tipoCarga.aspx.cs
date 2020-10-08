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
    public partial class tipoCarga : System.Web.UI.Page
    {
        bd vConexion = new bd();
        bd vConexionATM = new bd();
        Security vSecurity = new Security();
        protected void Page_Load(object sender, EventArgs e){
            Session["TIPO_CARGA_ATM"] = null;
            if (!Page.IsPostBack){
                if (Convert.ToBoolean(Session["AUTH"])){
                    if (vSecurity.ObtenerPermiso(Session["USUARIO"].ToString(), 3).Creacion)
                        btnguardartipocargaATM.Visible = true;

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
            if (HttpContext.Current.Session["TIPO_CARGA_ATM"] == null)
            {
                try
                {
                    DataTable vDatos = new DataTable();
                    vDatos = vConexionATM.ObtenerTablaATM("SPSTEI_ATM 5");
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                    if (vSecurity.ObtenerPermiso(Session["USUARIO"].ToString(), 3).Edicion){
                        foreach (GridViewRow item in GVBusqueda.Rows){
                            LinkButton LbEdit = item.FindControl("BtnEditar") as LinkButton;
                            LbEdit.Visible = true;
                        }
                    }
                    Session["tipoCargaATM"] = vDatos;


                }
                catch (Exception Ex)
                {

                }
                Session["TIPO_CARGA_ATM"] = 1;
            }
        }

        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
            txtAlerta1.Visible = false;
            txtAlerta2.Visible = false;
            DataTable vDataa = (DataTable)Session["tipoCargaATM"];
            string codtipocargaATMs = e.CommandArgument.ToString();


            if (e.CommandName == "Codigo")
            {
                string nom = "";
               
                try
                {
                    DataTable vDatos = new DataTable();
                    String vQuery = "SPSTEI_ATM 18,'" + codtipocargaATMs + "'";
                    vDatos = vConexionATM.ObtenerTablaATM(vQuery);
                    foreach (DataRow item in vDatos.Rows)
                    {
                        Session["codtipocargaATM"] = codtipocargaATMs;
                        Session["nombretipocargaATM"] = item["Descripcion"].ToString();
                    }
                }
                catch (Exception)
                {

                    throw;
                }

                lbcodtipocargaATM.Text = codtipocargaATMs;
                lbNombretipocargaATM.Text = Session["nombretipocargaATM"].ToString();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);
            }
        }

        protected void btnModalEnviarTipoCargaATM_Click(object sender, EventArgs e)
        {
            if (txtModalNewTipoCargaATM.Text == "" || txtModalNewTipoCargaATM.Text == string.Empty)
            {
                              
                txtAlerta1.Visible = true;
            }
            else
            {
               
                try
                {
                    string vQuery = "SPSTEI_ATM 19, '" + Session["codtipocargaATM"] + "','" + txtModalNewTipoCargaATM.Text + "'";
                    Int32 vInfo = vConexionATM.ejecutarSQLATM(vQuery);
                    if (vInfo == 1)
                    {
                        txtAlerta1.Visible = false;
                        txtModalNewTipoCargaATM.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
                        Mensaje("Tipo de carga ATM modificado con éxito", WarningType.Success);
                        UpdateGridView.Update();
                        cargarData();
                    }
                    else
                    {
                      txtAlerta1.Text="No se pudo modificar el tipo de carga ATM";
                        txtAlerta1.Visible = true;
                    }
                }
                catch (Exception Ex)
                {
                    throw;
                }
            }
        }

        protected void btnModalCerrarTipoCargaATM_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
        }

        protected void btnguardartipocargaATM_Click(object sender, EventArgs e)
        {
           txtAlerta1.Visible = false;
            txtAlerta2.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal2();", true);
            
        }

        protected void btnModalNueviTipoCargaATM_Click(object sender, EventArgs e)
        {
            
            if (txtNewtipocargaATM.Text == "" || txtNewtipocargaATM.Text == string.Empty)
            {
                txtAlerta2.Visible = true;
            }
            else
            {
                try
                {
                    string vQuery = "SPSTEI_ATM 20,'" + txtNewtipocargaATM.Text + "'";
                    Int32 vInfo = vConexionATM.ejecutarSQLATM(vQuery);
                    if (vInfo == 1)
                    {
                        txtAlerta2.Visible = false;
                        txtNewtipocargaATM.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal2();", true);
                        Mensaje("Tipo de carga ATM creada con éxito", WarningType.Success);
                        UpdateGridView.Update();
                        cargarData();

                    }
                    else
                    {
                       txtAlerta2.Text="No se pudo crear el tipo de carga ATM";
                        txtAlerta2.Visible = true;
                    }
                }
                catch (Exception Ex)
                {
                    throw;
                }
            }
        }

        protected void btnModalCerrarNueviTipoCargaATM_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal2();", true);
        }
    }
}