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
    public partial class marcasATM : System.Web.UI.Page
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
            try
            {
                DataTable vDatos = new DataTable();
                vDatos = vConexion.ObtenerTabla("STEISP_ATM_Generales 11, 1");
                GVBusqueda.DataSource = vDatos;
                GVBusqueda.DataBind();
                Session["marcaATM"] = vDatos;
                Session["UPDATEATM"] = 1;

            }
            catch (Exception Ex)
            {

            }

        }
        protected void btnnewmarcasATM_Click(object sender, EventArgs e)
        {
            lbmarca1.Visible = false;
            lbmarca2.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal2();", true);
        }

        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            lbmarca2.Visible = false;
            lbmarca1.Visible = false;
            DataTable vDataa = (DataTable)Session["soATM"];
            string codmarca = e.CommandArgument.ToString();


            if (e.CommandName == "Codigo")
            {
                string nom = "";
                string usu = "acedillo";
                try
                {
                    DataTable vDatos = new DataTable();
                    String vQuery = "STEISP_ATMAdminComponentesATM 22,'" + codmarca + "', '" + nom + "', '" + usu + "'";
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
               lbmarca1.Text="Ingrese nueva marca";
                lbmarca1.Visible = true;
            }
            else
            {
                string usu = "acedillo";
                try
                {
                    string vQuery = "STEISP_ATMAdminComponentesATM 24, '" + Session["codmarca"] + "','" + txtModalNewMarcaATM.Text + "', '" + usu + "'";
                    Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    if (vInfo == 1)
                    {
                        lbmarca1.Visible = false;
                        txtModalNewMarcaATM.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
                        Mensaje("Marca modificado con éxito", WarningType.Success);
                        UpdateGridView.Update();
                        cargarData();
                    }
                    else
                    {
                        lbmarca1.Text = "No se pudo modificar la marca";
                        lbmarca1.Visible = true;
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
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
        }

        protected void btnModalNueviMarcaATM_Click(object sender, EventArgs e)
        {
            string usu = "acedillo";
            if (txtNewMarcaATM.Text == "" || txtNewMarcaATM.Text == string.Empty)
            {
               lbmarca2.Text="Ingrese nueva marca";
                lbmarca2.Visible = true;
            }
            else
            {
                try
                {
                    string vQuery = "STEISP_ATMAdminComponentesATM 23, '" + Session["codmarca"] + "','" + txtNewMarcaATM.Text + "','" + usu + "'";
                    Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    if (vInfo == 1)
                    {
                        lbmarca2.Visible = false;
                        txtNewMarcaATM.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal2();", true);
                        Mensaje("Marca creada con éxito", WarningType.Success);
                        UpdateGridView.Update();
                        cargarData();

                    }
                    else
                    {
                      lbmarca2.Text="No se pudo crear la marca";
                        lbmarca2.Visible = true;
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