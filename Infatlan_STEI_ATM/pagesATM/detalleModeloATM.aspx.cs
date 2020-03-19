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
    public partial class detalleModeloATM : System.Web.UI.Page
    {
        bd vConexion = new bd();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["DETALLE_MODELO_ATM"] = null;
            if (!Page.IsPostBack)
            {
                cargarData();
            }
        }
        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }
        int CargarInformacionDDL(DropDownList vList, String vValue)
        {
            int vIndex = 0;
            try
            {
                int vContador = 0;
                foreach (ListItem item in vList.Items)
                {
                    if (item.Value.Equals(vValue))
                    {
                        vIndex = vContador;
                    }
                    vContador++;
                }
            }
            catch { throw; }
            return vIndex;
        }
        void cargarData()
        {
            if (HttpContext.Current.Session["DETALLE_MODELO_ATM"] == null)
            {
                //lbdetalle1.Visible = false;
                //lbdetalle2.Visible = false;
                string det ="";
            string usu = "acedillo";
            try
            {
                DataTable vDatos = new DataTable();
                vDatos = vConexion.ObtenerTabla("STEISP_ATM_DetalleModelo 4,1,1,'"+det+"','"+usu+"'");
                GVBusqueda.DataSource = vDatos;
                GVBusqueda.DataBind();
                Session["detMATM"] = vDatos;
                Session["UPDATEATM"] = 1;

            }
            catch (Exception Ex)
            {

            }
           
                try
                {
                    String vQuery = "STEISP_ATM_Generales 2,1";
                    DataTable vDatos = vConexion.ObtenerTabla(vQuery);
                    DDLModeloATM.Items.Add(new ListItem { Value = "0", Text = "Seleccione modelo..." });
                    DDLNewModelo.Items.Add(new ListItem { Value = "0", Text = "Seleccione modelo..." });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLModeloATM.Items.Add(new ListItem { Value = item["idModeloATM"].ToString(), Text = item["nombreModeloATM"].ToString() });
                        DDLNewModelo.Items.Add(new ListItem { Value = item["idModeloATM"].ToString(), Text = item["nombreModeloATM"].ToString() });
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
                Session["DETALLE_MODELO_ATM"] = "1";
            }

        }

        protected void btnnewdetModeloATM_Click(object sender, EventArgs e)
        {
            lbdetalle2.Visible = false;
            lbdetalle1.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal2();", true);
        }

        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            lbdetalle2.Visible = false;
            lbdetalle1.Visible = false;
            DataTable vDataa = (DataTable)Session["detMATM"];
            string coddetM = e.CommandArgument.ToString();


            if (e.CommandName == "Codigo")
            {
                string nom = "";
                string usu = "acedillo";
                try
                {
                    DataTable vDatos = new DataTable();
                    String vQuery = "STEISP_ATM_DetalleModelo 1,1,'" + coddetM + "', '" + nom + "', '" + usu + "'";
                    vDatos = vConexion.ObtenerTabla(vQuery);
                    foreach (DataRow item in vDatos.Rows)
                    {
                        Session["coddetM"] = coddetM;
                        Session["nombredetM"] = item["NOMBRE"].ToString();
                        Session["idModelo"] = item["IDM"].ToString();
                    }
                }
                catch (Exception)
                {

                    throw;
                }

                lbcoddetMATM.Text = coddetM;
                lbNombredetMATM.Text = Session["nombredetM"].ToString();
                DDLModeloATM.SelectedIndex= CargarInformacionDDL(DDLModeloATM, Session["idModelo"].ToString());
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);
            }
        }

        protected void btnModalEnviardetMATM_Click(object sender, EventArgs e)
        {
            if (txtModalNewdetMATM.Text == "" || txtModalNewdetMATM.Text == string.Empty || DDLModeloATM.SelectedValue == "0")
            {
               lbdetalle1.Text="Ingrese el nuevo detalle de modelo";
                lbdetalle1.Visible = true;
            }
            else
            {
                string usu = "acedillo";
                try
                {
                    string vQuery = "STEISP_ATM_DetalleModelo 3, '" + DDLModeloATM.SelectedValue + "','"+ Session["coddetM"] + "','" + txtModalNewdetMATM.Text + "','" + usu + "'";
                    Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    if (vInfo == 1)
                    {
                        lbdetalle1.Visible = false;
                        txtModalNewdetMATM.Text = string.Empty;
                        DDLModeloATM.SelectedValue = "0";
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
                        Mensaje("Detalle de modelo modificado con éxito", WarningType.Success);
                        UpdateGridView.Update();
                        cargarData();
                    }
                    else
                    {
                       lbdetalle1.Text="No se pudo modificar el detalle de modelo";
                    }
                }
                catch (Exception Ex)
                {
                    throw;
                }
            }
        }

        protected void btnModalCerrardetMATM_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
        }

        protected void btnModalNuevidetMATM_Click(object sender, EventArgs e)
        {
            
            string usu = "acedillo";
            if (txtNewdetMATM.Text == "" || txtNewdetMATM.Text == string.Empty || DDLNewModelo.SelectedValue == "0")
            {
               lbdetalle2.Text="Ingrese el nuevo detalle de modelo";
                lbdetalle2.Visible = true;
            }
            else
            {
                try
                {
                    string vQuery = "STEISP_ATM_DetalleModelo 2, '" + DDLNewModelo.SelectedValue + "',6,'" + txtNewdetMATM.Text + "','" + usu + "'";
                    Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    if (vInfo == 1)
                    {
                        lbdetalle2.Visible = false;
                        txtNewdetMATM.Text = string.Empty;
                        DDLNewModelo.SelectedValue = "0";
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal2();", true);
                        Mensaje("Detalle de modelo creada con éxito", WarningType.Success);
                        UpdateGridView.Update();
                        cargarData();

                    }
                    else
                    {
                        lbdetalle2.Text = "No se pudo crear el detalle de modelo";
                        lbdetalle2.Visible = true;
                    }
                }
                catch (Exception Ex)
                {
                    throw;
                }
            }
        }

        protected void btnModalCerrarNuevidetMATM_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal2();", true);
        }

        protected void GVBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GVBusqueda.PageIndex = e.NewPageIndex;
                GVBusqueda.DataSource = (DataTable)Session["detMATM"];
                GVBusqueda.DataBind();
            }
            catch (Exception Ex)
            {

            }
        }
    }
}