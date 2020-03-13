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
    public partial class tecladoATM : System.Web.UI.Page
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
                vDatos = vConexion.ObtenerTabla("STEISP_ATM_Generales 6, 1");
                GVBusqueda.DataSource = vDatos;
                GVBusqueda.DataBind();
                Session["tecladoATM"] = vDatos;
                Session["UPDATEATM"] = 1;

            }
            catch (Exception Ex)
            {

            }

        }

        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            lbteclado1.Visible = false;
            lbteclado2.Visible = false;
            DataTable vDataa = (DataTable)Session["tecladoATM"];
            string codtecladoATMs = e.CommandArgument.ToString();


            if (e.CommandName == "Codigo")
            {
                string nom = "";
                string usu = "acedillo";
                try
                {
                    DataTable vDatos = new DataTable();
                    String vQuery = "STEISP_ATMAdminComponentesATM 13,'" + codtecladoATMs + "', '" + nom + "', '" + usu + "'";
                    vDatos = vConexion.ObtenerTabla(vQuery);
                    foreach (DataRow item in vDatos.Rows)
                    {
                        Session["codtecladoATM"] = codtecladoATMs;
                        Session["nombretecladoATM"] = item["nombreTecladoATM"].ToString();
                    }
                }
                catch (Exception)
                {

                    throw;
                }

                lbcodtecladoATM.Text = codtecladoATMs;
                lbNombretecladoATM.Text = Session["nombreTecladoATM"].ToString();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);
            }
        }

        protected void btnModalEnviarTecladoATM_Click(object sender, EventArgs e)
        {
            if (txtModalNewTecladoATM.Text == "" || txtModalNewTecladoATM.Text == string.Empty)
            {
              lbteclado1.Text="Ingrese el nuevo teclado de ATM";
                lbteclado1.Visible = true;
            }
            else
            {
                string usu = "acedillo";
                try
                {
                    string vQuery = "STEISP_ATMAdminComponentesATM 15, '" + Session["codtecladoATM"] + "','" + txtModalNewTecladoATM.Text + "', '" + usu + "'";
                    Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    if (vInfo == 1)
                    {
                        lbteclado1.Visible = false;
                        txtModalNewTecladoATM.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
                        Mensaje("Teclado de ATM modificado con éxito", WarningType.Success);
                        UpdateGridView.Update();
                        cargarData();
                    }
                    else
                    {
                       lbteclado1.Text="No se pudo modificar el teclado de ATM";
                        lbteclado1.Visible = true;
                    }
                }
                catch (Exception Ex)
                {
                    throw;
                }
            }
        }

        protected void btnModalCerrarTecladoATM_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
        }

        protected void btnModalNueviTecladoATM_Click(object sender, EventArgs e)
        {
            string usu = "acedillo";
            if (txtNewTecladoATM.Text == "" || txtNewTecladoATM.Text == string.Empty)
            {
               lbteclado2.Text="Ingrese el nuevo teclado de ATM";
                lbteclado2.Visible = true;
            }
            else
            {
                try
                {
                    string vQuery = "STEISP_ATMAdminComponentesATM 14, '" + Session["codtecladoATM"] + "','" + txtNewTecladoATM.Text + "','" + usu + "'";
                    Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    if (vInfo == 1)
                    {
                        lbteclado2.Visible = false;
                        txtNewTecladoATM.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal2();", true);
                        Mensaje("Teclado de ATM creada con éxito", WarningType.Success);
                        UpdateGridView.Update();
                        cargarData();

                    }
                    else
                    {
                       lbteclado2.Text="No se pudo crear el teclado de ATM";
                        lbteclado2.Visible = true;
                    }
                }
                catch (Exception Ex)
                {
                    throw;
                }
            }
        }

        protected void btnModalCerrarNueviTecladoATM_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal2();", true);
        }

        protected void btnnewtecladoATM_Click(object sender, EventArgs e)
        {
            lbteclado1.Visible = false;
            lbteclado2.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal2();", true);
        }
    }
}