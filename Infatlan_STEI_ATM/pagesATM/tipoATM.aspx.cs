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
    public partial class tipoATM : System.Web.UI.Page
    {
        bd vConexion = new bd();
        protected void Page_Load(object sender, EventArgs e)
        {
            cargarData();
        }

        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }
        void cargarData()
        {
            //lbNoCrearTipo.Visible = false;
            try
            {
                DataTable vDatos = new DataTable();
                vDatos = vConexion.ObtenerTabla("STEISP_ATM_Generales 1, 1");
                GVBusqueda.DataSource = vDatos;
                GVBusqueda.DataBind();
                Session["tipoATM"] = vDatos;
                Session["UPDATEATM"] = 1;

            }
            catch (Exception Ex)
            {

            }

        }
        protected void GVBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DataTable vDataa = (DataTable)Session["tipoATM"];
            string codtipoATMs = e.CommandArgument.ToString();

            lbNoTipoATM2.Visible = false;
            lbNoCrearTipo.Visible = false;
            if (e.CommandName == "Codigo")
            {
                string nom = "";
                string usu = "acedillo";
                try
                {
                    DataTable vDatos = new DataTable();
                    String vQuery = "STEISP_ATMAdminComponentesATM 1,'" + codtipoATMs + "', '"+nom+"', '"+usu+"'";
                    vDatos = vConexion.ObtenerTabla(vQuery);
                    foreach (DataRow item in vDatos.Rows)
                    {
                        Session["codtipoATM"] = codtipoATMs;
                        Session["nombretipoATM"] = item["nombreTipoATM"].ToString();                        
                    }                  
                }
                catch (Exception)
                {

                    throw;
                }

                lbcodtipoATM.Text = codtipoATMs;
                lbNombretipoATM.Text = Session["nombretipoATM"].ToString();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);
            }
               
        }

        protected void btnModalEnviartipoATM_Click(object sender, EventArgs e)
        {
            if (txtModalNewTipoATM.Text == "" || txtModalNewTipoATM.Text == string.Empty)
            {
                lbNoCrearTipo.Text = "Ingrese el nuevo tipo de ATM";
                lbNoCrearTipo.Visible = true;
            }
            else
            {
                string usu = "acedillo";
                try
                {
                    string vQuery = "STEISP_ATMAdminComponentesATM 3, '" + Session["codtipoATM"] + "','" + txtModalNewTipoATM.Text + "', '" + usu + "'";
                    Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    if (vInfo == 1)
                    {
                        lbNoCrearTipo.Visible = false;
                        txtModalNewTipoATM.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
                        Mensaje("Tipo ATM modificado con éxito", WarningType.Success);
                        UpdateGridView.Update();
                        cargarData();
                    }
                    else
                    {
                        lbNoCrearTipo.Text = "No se pudo modificar el tipo de ATM";
                        lbNoCrearTipo.Visible = true;
                    }
                }
                catch (Exception Ex)
                {
                    throw;
                }
            }
        }

        protected void btnModalCerrartipoATM_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
        }

        protected void btnguardartipoATM_Click(object sender, EventArgs e)
        {
            lbNoTipoATM2.Visible = false;
            lbNoCrearTipo.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal2();", true);
          
            }

        protected void btnModalNueviTipoATM_Click(object sender, EventArgs e)
        {
            string usu = "acedillo";
            if (txtNewTipoATM.Text == "" || txtNewTipoATM.Text == string.Empty)
            {
                lbNoTipoATM2.Text = "Ingrese el nuevo tipo de ATM";
                lbNoTipoATM2.Visible = true;
            }
            else
            {
                try
                {
                    string vQuery = "STEISP_ATMAdminComponentesATM 2, '" + Session["codtipoATM"] + "','" + txtNewTipoATM.Text + "','" + usu + "'";
                    Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    if (vInfo == 1)
                    {
                        lbNoTipoATM2.Visible=false;
                        txtNewTipoATM.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal2();", true);
                        Mensaje("Tipo de ATM creada con éxito", WarningType.Success);
                        UpdateGridView.Update();
                        cargarData();

                    }
                    else
                    {
                        lbNoTipoATM2.Text = "No se pudo crear el tipo de ATM";
                        lbNoTipoATM2.Visible = true;
                    }
                }
                catch (Exception Ex)
                {
                    throw;
                }
            }
            }

        protected void btnModalCerrarNueviTipoATM_Click1(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal2();", true);
        }
    }
    
}