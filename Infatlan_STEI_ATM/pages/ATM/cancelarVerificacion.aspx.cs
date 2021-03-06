﻿using Infatlan_STEI_ATM.clases;
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Infatlan_STEI_ATM.pages.ATM
{
    public partial class cancelarVerificacion : System.Web.UI.Page
    {
        bd vConexion = new bd();
        Security vSecurity = new Security();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CANCELARVERIF_ATM"] = null;
            if (!Page.IsPostBack)
            {
                if (Convert.ToBoolean(Session["AUTH"]))
                {
                    if (vSecurity.ObtenerPermiso(Session["USUARIO"].ToString(), 3).Creacion)
                        btnCancelarVerif.Visible = true;

                    cargarData();
                }
                else
                {
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
            if (HttpContext.Current.Session["CANCELARVERIF_ATM"] == null)
            {
                try
                {
                    DataTable vDatos = new DataTable();
                    vDatos = vConexion.ObtenerTabla("STEISP_ATM_Generales 20, 1");
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                    if (vSecurity.ObtenerPermiso(Session["USUARIO"].ToString(), 3).Edicion)
                    {
                        foreach (GridViewRow item in GVBusqueda.Rows)
                        {
                            LinkButton LbEdit = item.FindControl("BtnEditar") as LinkButton;
                            LbEdit.Visible = true;
                        }
                    }
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
            txtAlerta1.Visible = false;
            txtAlerta2.Visible = false;
            txtModalNewmotivoATM.Text = string.Empty;
            txtNewMotivoCancelATM.Text = string.Empty;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal2();", true);
        }

        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DataTable vDataa = (DataTable)Session["MotivoCancelarATM"];
            string codmotivo = e.CommandArgument.ToString();

            if (e.CommandName == "Codigo")
            {
                try
                {
                    DataTable vDatos = new DataTable();
                    String vQuery = "STEISP_ATMAdminComponentesATM 25,'" + codmotivo + "'";
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
            if (txtModalNewmotivoATM.Text == "" || txtModalNewmotivoATM.Text == string.Empty)
            {
                txtAlerta1.Visible = true;
            }
            else
            {

                try
                {
                    string vQuery = "STEISP_ATMAdminComponentesATM 27, '" + Session["ATMCODMOTIVO"] + "','" + txtModalNewmotivoATM.Text + "', '" + Session["USUARIO"].ToString() + "'";
                    Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    if (vInfo == 1)
                    {
                        txtAlerta1.Visible = false;
                        txtModalNewmotivoATM.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
                        Mensaje("motivo de cancelación modificado con éxito", WarningType.Success);
                        UpdateGridView.Update();
                        cargarData();
                    }
                    else
                    {
                        txtAlerta1.Text = "No se pudo modificar la marca";
                        txtAlerta1.Visible = true;
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

            if (txtNewMotivoCancelATM.Text == "" || txtNewMotivoCancelATM.Text == string.Empty)
            {
                txtAlerta2.Visible = true;
            }
            else
            {
                try
                {
                    string vQuery = "STEISP_ATMAdminComponentesATM 26, '" + Session["ATMCODMOTIVO"] + "','" + txtNewMotivoCancelATM.Text + "','" + Session["USUARIO"].ToString() + "'";
                    Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    if (vInfo == 1)
                    {
                        txtAlerta2.Visible = false;
                        txtNewMotivoCancelATM.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal2();", true);
                        Mensaje("Motivo de cancelación creada con éxito", WarningType.Success);
                        UpdateGridView.Update();
                        cargarData();

                    }
                    else
                    {
                        txtAlerta2.Text = "No se pudo crear el motivo";
                        txtAlerta2.Visible = true;
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