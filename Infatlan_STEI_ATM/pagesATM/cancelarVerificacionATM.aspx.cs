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
    public partial class cancelarVerificacionATM : System.Web.UI.Page
    {
        bd vConexion = new bd();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CANCELARVERIF_ATM"] = null;
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
            if (HttpContext.Current.Session["CANCELARVERIF_ATM"] == null)
            {
                try
                {
                    DataTable vDatos = new DataTable();
                    vDatos = vConexion.ObtenerTabla("STEISP_ATM_Generales 20, 1");
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                    Session["MotivoCancelarATM"] = vDatos;


                }
                catch (Exception Ex)
                {

                }
                Session["CANCELARVERIF_ATM"] = 1;
            }
        }

        protected void btnCancelarVerif_Click(object sender, EventArgs e)
        {
            lbmotivo1.Visible = false;
            lbmotivo2.Visible = false;
            txtModalNewmotivoATM.Text = string.Empty;
            txtNewMotivoCancelATM.Text = string.Empty;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal2();", true);
        }

        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DataTable vDataa = (DataTable)Session["MotivoCancelarATM"];
            string codmotivo = e.CommandArgument.ToString();
            string nom = "";
            string usu = "acedillo";

            if (e.CommandName == "Codigo")
            {
               
                try
                {
                    DataTable vDatos = new DataTable();
                    String vQuery = "STEISP_ATMAdminComponentesATM 25,'" + codmotivo + "', '" + nom + "', '" + usu + "'";
                    vDatos = vConexion.ObtenerTabla(vQuery);
                    foreach (DataRow item in vDatos.Rows)
                    {
                        Session["ATMCODMOTIVO"] = codmotivo;
                        lbNombremotivoATM.Text = item["nombreCancelar"].ToString();
                    }
                }
                catch (Exception)
                {

                    throw;
                }

                lbcodmotivoATM.Text = codmotivo;
                //lbNombremotivoATM.Text = Session["nombreMarca"].ToString();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);
            }
        }

        protected void btnModalEnviarCancelarATM_Click(object sender, EventArgs e)
        {
            if(txtModalNewmotivoATM.Text=="" || txtModalNewmotivoATM.Text == string.Empty)
            {
                lbmotivo1.Visible = true;
                lbmotivo1.Text = "No puede dejar campos vacios";
            }
            else
            {
                string usu = "acedillo";
                try
                {
                    string vQuery = "STEISP_ATMAdminComponentesATM 27, '" + Session["ATMCODMOTIVO"] + "','" + txtModalNewmotivoATM.Text + "', '" + usu + "'";
                    Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    if (vInfo == 1)
                    {
                        lbmotivo1.Visible = false;
                        txtModalNewmotivoATM.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
                        Mensaje("motivo de cancelación modificado con éxito", WarningType.Success);
                        UpdateGridView.Update();
                        cargarData();
                    }
                    else
                    {
                        lbmotivo1.Text = "No se pudo modificar la marca";
                        lbmotivo1.Visible = true;
                    }
                }
                catch (Exception Ex)
                {
                    throw;
                }
            }
        }

        protected void btnModalCerrarCancelarATM_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
        }

        protected void btnModalCancelarMotivoATM_Click(object sender, EventArgs e)
        {
            string usu = "acedillo";
            if (txtNewMotivoCancelATM.Text == "" || txtNewMotivoCancelATM.Text == string.Empty)
            {
                lbmotivo2.Text = "Ingrese nuevo motivo";
                lbmotivo2.Visible = true;
            }
            else
            {
                try
                {
                    string vQuery = "STEISP_ATMAdminComponentesATM 26, '" + Session["ATMCODMOTIVO"] + "','" + txtNewMotivoCancelATM.Text + "','" + usu + "'";
                    Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    if (vInfo == 1)
                    {
                        lbmotivo2.Visible = false;
                        txtNewMotivoCancelATM.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal2();", true);
                        Mensaje("Motivo de cancelación creada con éxito", WarningType.Success);
                        UpdateGridView.Update();
                        cargarData();

                    }
                    else
                    {
                        lbmotivo2.Text = "No se pudo crear el motivo";
                        lbmotivo2.Visible = true;
                    }
                }
                catch (Exception Ex)
                {
                    throw;
                }
            }
        }

        protected void btnModalCerrarCancelarMotivoATM_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal2();", true);
        }
    }
}