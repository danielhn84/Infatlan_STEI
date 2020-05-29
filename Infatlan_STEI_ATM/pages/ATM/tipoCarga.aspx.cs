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
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["TIPO_CARGA_ATM"] = null;
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
            if (HttpContext.Current.Session["TIPO_CARGA_ATM"] == null)
            {
                try
                {
                    DataTable vDatos = new DataTable();
                    vDatos = vConexion.ObtenerTabla("STEISP_ATM_Generales 4, 1");
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
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
            H5Alerta1.Visible = false;
            H5Alerta2.Visible = false;
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
                    String vQuery = "STEISP_ATMAdminComponentesATM 7,'" + codtipocargaATMs + "'";
                    vDatos = vConexion.ObtenerTabla(vQuery);
                    foreach (DataRow item in vDatos.Rows)
                    {
                        Session["codtipocargaATM"] = codtipocargaATMs;
                        Session["nombretipocargaATM"] = item["nombreTipoCargaATM"].ToString();
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
               
                H5Alerta1.Visible = true;
                txtAlerta1.Visible = true;
            }
            else
            {
               
                try
                {
                    string vQuery = "STEISP_ATMAdminComponentesATM 9, '" + Session["codtipocargaATM"] + "','" + txtModalNewTipoCargaATM.Text + "', '" + Session["usuATM"].ToString() + "'";
                    Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    if (vInfo == 1)
                    {
                        H5Alerta1.Visible = false;
                        txtModalNewTipoCargaATM.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
                        Mensaje("Tipo de carga ATM modificado con éxito", WarningType.Success);
                        UpdateGridView.Update();
                        cargarData();
                    }
                    else
                    {
                       H5Alerta1.InnerText="No se pudo modificar el tipo de carga ATM";
                        H5Alerta1.Visible = true;
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
            H5Alerta1.Visible = false;
            H5Alerta2.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal2();", true);
            
        }

        protected void btnModalNueviTipoCargaATM_Click(object sender, EventArgs e)
        {
            
            if (txtNewtipocargaATM.Text == "" || txtNewtipocargaATM.Text == string.Empty)
            {
              
                H5Alerta2.Visible = true;
                txtAlerta2.Visible = true;
            }
            else
            {
                try
                {
                    string vQuery = "STEISP_ATMAdminComponentesATM 8, '" + Session["codtipocargaATM"] + "','" + txtNewtipocargaATM.Text + "','" + Session["usuATM"].ToString() + "'";
                    Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    if (vInfo == 1)
                    {
                        H5Alerta2.Visible = false;
                        txtNewtipocargaATM.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal2();", true);
                        Mensaje("Tipo de carga ATM creada con éxito", WarningType.Success);
                        UpdateGridView.Update();
                        cargarData();

                    }
                    else
                    {
                       H5Alerta2.InnerText="No se pudo crear el tipo de carga ATM";
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