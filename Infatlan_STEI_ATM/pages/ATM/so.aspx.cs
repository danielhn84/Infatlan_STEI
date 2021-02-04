using Infatlan_STEI_ATM.clases;
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Infatlan_STEI_ATM.pages.ATM
{
    public partial class so : System.Web.UI.Page
    {
        bd vConexion = new bd();
        Security vSecurity = new Security();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["SO_ATM"] = null;
            if (!Page.IsPostBack)
            {
                if (Convert.ToBoolean(Session["AUTH"]))
                {
                    if (vSecurity.ObtenerPermiso(Session["USUARIO"].ToString(), 3).Creacion)
                        btnnewsoATM.Visible = true;

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
            if (HttpContext.Current.Session["SO_ATM"] == null)
            {
                try
                {
                    DataTable vDatos = new DataTable();
                    vDatos = vConexion.ObtenerTabla("STEISP_ATM_Generales 9, 1");
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
                    Session["soATM"] = vDatos;


                }
                catch (Exception Ex)
                {

                }
                Session["SO_ATM"] = 1;
            }
        }

        protected void btnnewsoATM_Click(object sender, EventArgs e)
        {

            txtAlerta1.Visible = false;
            txtAlerta2.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal2();", true);
        }

        protected void btnModalEnviarSOATM_Click(object sender, EventArgs e)
        {
            if (txtModalNewSOATM.Text == "" || txtModalNewSOATM.Text == string.Empty)
            {
                txtAlerta1.Visible = true;
            }
            else
            {

                try
                {
                    string vQuery = "STEISP_ATMAdminComponentesATM 18, '" + Session["codsoATM"] + "','" + txtModalNewSOATM.Text + "', '" + Session["USUARIO"].ToString() + "'";
                    Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    if (vInfo == 1)
                    {
                        txtAlerta1.Visible = false;
                        txtModalNewSOATM.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
                        Mensaje("Sistema operativo modificado con éxito", WarningType.Success);
                        UpdateGridView.Update();
                        cargarData();
                    }
                    else
                    {
                        txtAlerta1.Text = "No se pudo modificar el sistema operativo";
                        txtAlerta1.Visible = true;
                    }
                }
                catch (Exception Ex)
                {
                    throw;
                }
            }
        }

        protected void btnModalCerrarSOATM_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
        }

        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            txtAlerta1.Visible = false;
            txtAlerta2.Visible = false;
            DataTable vDataa = (DataTable)Session["soATM"];
            string codsoATMs = e.CommandArgument.ToString();


            if (e.CommandName == "Codigo")
            {


                try
                {
                    DataTable vDatos = new DataTable();
                    String vQuery = "STEISP_ATMAdminComponentesATM 16,'" + codsoATMs + "'";
                    vDatos = vConexion.ObtenerTabla(vQuery);
                    foreach (DataRow item in vDatos.Rows)
                    {
                        Session["codsoATM"] = codsoATMs;
                        Session["nombresoATM"] = item["nombreSO"].ToString();
                    }
                }
                catch (Exception)
                {

                    throw;
                }

                lbcodsoATM.Text = codsoATMs;
                lbNombresoATM.Text = Session["nombresoATM"].ToString();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);
            }
        }

        protected void btnModalNueviSOATM_Click(object sender, EventArgs e)
        {

            if (txtNewSOATM.Text == "" || txtNewSOATM.Text == string.Empty)
            {
                txtAlerta2.Visible = true;
            }
            else
            {
                try
                {
                    string vQuery = "STEISP_ATMAdminComponentesATM 17, '" + Session["codsoATM"] + "','" + txtNewSOATM.Text + "','" + Session["USUARIO"].ToString() + "'";
                    Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    if (vInfo == 1)
                    {
                        txtAlerta2.Visible = false;
                        txtNewSOATM.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal2();", true);
                        Mensaje("Sistema operativo creada con éxito", WarningType.Success);
                        UpdateGridView.Update();
                        cargarData();

                    }
                    else
                    {
                        txtAlerta2.Text = "No se pudo crear el sistema operativo";
                        txtAlerta2.Visible = true;
                    }
                }
                catch (Exception Ex)
                {
                    throw;
                }
            }
        }

        protected void btnModalCerrarNueviSOATM_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal2();", true);
        }
    }
}