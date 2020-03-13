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

namespace Infatlan_STEI_ATM.pagesATM
{
    public partial class procesadorATM : System.Web.UI.Page
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
                vDatos = vConexion.ObtenerTabla("STEISP_ATM_Generales 5, 1");
                GVBusqueda.DataSource = vDatos;
                GVBusqueda.DataBind();
                Session["procesadorATM"] = vDatos;
                Session["UPDATEATM"] = 1;

            }
            catch (Exception Ex)
            {

            }

        }
        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            lbprocesador1.Visible = false;
            lbprocesador2.Visible = false;
            DataTable vDataa = (DataTable)Session["procesadorATM"];
            string codProcesadorATMs = e.CommandArgument.ToString();


            if (e.CommandName == "Codigo")
            {
                string nom = "";
                string usu = "acedillo";
                try
                {
                    DataTable vDatos = new DataTable();
                    String vQuery = "STEISP_ATMAdminComponentesATM 10,'" + codProcesadorATMs + "', '" + nom + "', '" + usu + "'";
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
            lbprocesador1.Visible = false;
            lbprocesador2.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal2();", true);
           
        }

        protected void btnModalCerrarprocesadorATM_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
        }

        protected void btnModalEnviarprocesadorATM_Click(object sender, EventArgs e)
        {
            if (txtModalNewprocesadorATM.Text == "" || txtModalNewprocesadorATM.Text == string.Empty)
            {
               lbprocesador1.Text="Ingrese el nuevo procesador ATM";
                lbprocesador1.Visible = true;
            }
            else
            {
                string usu = "acedillo";
                try
                {
                    string vQuery = "STEISP_ATMAdminComponentesATM 12, '" + Session["codprocesadorATM"] + "','" + txtModalNewprocesadorATM.Text + "', '" + usu + "'";
                    Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    if (vInfo == 1)
                    {
                        lbprocesador1.Visible = false;
                        txtModalNewprocesadorATM.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
                        Mensaje("Tipo de carga ATM modificado con éxito", WarningType.Success);
                        UpdateGridView.Update();
                        cargarData();
                    }
                    else
                    {
                       lbprocesador1.Text="No se pudo modificar el procesador ATM";
                        lbprocesador1.Visible = true;
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
            string usu = "acedillo";
            if (txtNewProcesadorATM.Text == "" || txtNewProcesadorATM.Text == string.Empty)
            {
               lbprocesador2.Text="Ingrese el nuevo procesador ATM";
                lbprocesador2.Visible = true;
            }
            else
            {
                try
                {
                    string vQuery = "STEISP_ATMAdminComponentesATM 11, '" + Session["codprocesadorATM"] + "','" + txtNewProcesadorATM.Text + "','" + usu + "'";
                    Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    if (vInfo == 1)
                    {
                        lbprocesador2.Visible = false;
                        txtNewProcesadorATM.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal2();", true);
                        Mensaje("Procesador ATM creada con éxito", WarningType.Success);
                        UpdateGridView.Update();
                        cargarData();

                    }
                    else
                    {
                       lbprocesador2.Text="No se pudo crear elprocesador ATM";
                        lbprocesador2.Visible = true;
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