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
    public partial class tipoCargaATM : System.Web.UI.Page
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
            lbtipoCarga1.Visible = false;
            lbtipocarga2.Visible = false;
            DataTable vDataa = (DataTable)Session["tipoCargaATM"];
            string codtipocargaATMs = e.CommandArgument.ToString();


            if (e.CommandName == "Codigo")
            {
                string nom = "";
                string usu = "acedillo";
                try
                {
                    DataTable vDatos = new DataTable();
                    String vQuery = "STEISP_ATMAdminComponentesATM 7,'" + codtipocargaATMs + "', '" + nom + "', '" + usu + "'";
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
               lbtipoCarga1.Text="Ingrese el nuevo tipo de carga ATM";
                lbtipoCarga1.Visible = true;
            }
            else
            {
                string usu = "acedillo";
                try
                {
                    string vQuery = "STEISP_ATMAdminComponentesATM 9, '" + Session["codtipocargaATM"] + "','" + txtModalNewTipoCargaATM.Text + "', '" + usu + "'";
                    Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    if (vInfo == 1)
                    {
                        lbtipoCarga1.Visible = false;
                        txtModalNewTipoCargaATM.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
                        Mensaje("Tipo de carga ATM modificado con éxito", WarningType.Success);
                        UpdateGridView.Update();
                        cargarData();
                    }
                    else
                    {
                       lbtipoCarga1.Text="No se pudo modificar el tipo de carga ATM";
                        lbtipoCarga1.Visible = true;
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
            lbtipoCarga1.Visible = false;
            lbtipocarga2.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal2();", true);
            
        }

        protected void btnModalNueviTipoCargaATM_Click(object sender, EventArgs e)
        {
            string usu = "acedillo";
            if (txtNewtipocargaATM.Text == "" || txtNewtipocargaATM.Text == string.Empty)
            {
               lbtipocarga2.Text="Ingrese el nuevo tipo de carga ATM";
                lbtipocarga2.Visible = true;
            }
            else
            {
                try
                {
                    string vQuery = "STEISP_ATMAdminComponentesATM 8, '" + Session["codtipocargaATM"] + "','" + txtNewtipocargaATM.Text + "','" + usu + "'";
                    Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    if (vInfo == 1)
                    {
                        lbtipocarga2.Visible = false;
                        txtNewtipocargaATM.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal2();", true);
                        Mensaje("Tipo de carga ATM creada con éxito", WarningType.Success);
                        UpdateGridView.Update();
                        cargarData();

                    }
                    else
                    {
                       lbtipocarga2.Text="No se pudo crear el tipo de carga ATM";
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