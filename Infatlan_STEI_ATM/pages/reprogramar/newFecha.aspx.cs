using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infatlan_STEI_ATM.clases;
using System.Data;
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Configuration;

namespace Infatlan_STEI_ATM.pages.reprogramar
{
    public partial class newFecha : System.Web.UI.Page
    {
        bd vConexion = new bd();
        bd vConexionATM = new bd();
        Security vSecurity = new Security();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               
                if (Convert.ToBoolean(Session["AUTH"]))
                {                   
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
           
                try
                {
                    DataTable vDatos = new DataTable();
                    vDatos = vConexion.ObtenerTabla("STEISP_ATM_Generales 50");
                    GVMantenimientos.DataSource = vDatos;
                    GVMantenimientos.DataBind();
                    //if (vSecurity.ObtenerPermiso(Session["USUARIO"].ToString(), 3).Edicion)
                    //{
                    //    foreach (GridViewRow item in GVBusqueda.Rows)
                    //    {
                    //        LinkButton LbEdit = item.FindControl("btnbajaATM") as LinkButton;
                    //        LbEdit.Visible = true;
                    //    }
                    //}

                   
                    Session["ATM_CAMBIAR_FECHA"] = vDatos;
                }
                catch (Exception Ex)
                {
                   Mensaje(Ex.Message, WarningType.Danger);
                }
               
        }

        protected void TxBuscarATM_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cargarData();

                String vBusqueda = TxBuscarATM.Text.ToUpper().ToString();
                DataTable vDatos = (DataTable)Session["ATM_CAMBIAR_FECHA"];

                if (vBusqueda.Equals(""))
                {
                    GVMantenimientos.DataSource = vDatos;
                    GVMantenimientos.DataBind();

                }
                else
                {
                    EnumerableRowCollection<DataRow> filtered = vDatos.AsEnumerable()
                        .Where(r => r.Field<String>("codATM").Contains(vBusqueda));

                    DataTable vDatosFiltrados = new DataTable();
                    vDatosFiltrados.Columns.Add("idMantenimiento");
                    vDatosFiltrados.Columns.Add("codATM");
                    vDatosFiltrados.Columns.Add("fechaMantenimiento");
                    foreach (DataRow item in filtered)
                    {
                        vDatosFiltrados.Rows.Add(
                            item["idMantenimiento"].ToString(),
                            item["codATM"].ToString(),
                            item["fechaMantenimiento"].ToString()
                            );
                    }

                    GVMantenimientos.DataSource = vDatosFiltrados;
                    GVMantenimientos.DataBind();
                    Session["ATM_CAMBIAR_FECHA"] = vDatosFiltrados;
                }


            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        protected void GVMantenimientos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GVMantenimientos.PageIndex = e.NewPageIndex;
                GVMantenimientos.DataSource = (DataTable)Session["ATM_CAMBIAR_FECHA"];
                GVMantenimientos.DataBind();
            }
            catch (Exception Ex)
            {

            }
        }

        protected void GVMantenimientos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {                
                string codMantenimiento = e.CommandArgument.ToString();

                if (e.CommandName == "Select")
                {
                    H4Titulo.InnerText = "Cambiar fecha de Mantenimiento-"+codMantenimiento;
                    DataTable vDatos = new DataTable();
                    String vQuery = "STEISP_ATM_Generales 36,'" + codMantenimiento + "'";
                    vDatos = vConexion.ObtenerTabla(vQuery);
                    foreach (DataRow item in vDatos.Rows)
                    {
                        Session["ID_MANTENIMIENTO_CAMBIO"] = codMantenimiento;
                        Session["FECHA_MANTENIMIENTO_CAMBIO"] = item["fechaMantenimiento"].ToString();
                    }
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);
                }
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            txtAlerta2.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
        }

        protected void btnNuevaFecha_Click(object sender, EventArgs e)
        {
            if (txtMotivoCambio.Text == "" || txtNewFecha.Text == "")
            {
                txtAlerta2.Visible = true;
            }
            else
            {
                String vFormato = "yyyy/MM/dd"; //"dd/MM/yyyy HH:mm:ss"
                String vNewFecha = Convert.ToDateTime(txtNewFecha.Text).ToString(vFormato);
                String vOriginalFecha = Convert.ToDateTime(Session["FECHA_MANTENIMIENTO_CAMBIO"]).ToString(vFormato);

                string vQuery = "STEISP_ATM_CancelarMantenimiento 8, '" + Session["ID_MANTENIMIENTO_CAMBIO"] + "','" + vOriginalFecha + "'," +
                    "'"+ vNewFecha + "','"+txtMotivoCambio.Text+"','"+ Session["USUARIO"] + "'";
                Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                if (vInfo != 0)
                {
                    txtMotivoCambio.Text = "";
                    txtNewFecha.Text = "";
                    txtAlerta2.Visible = false;
                    cargarData();
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
                    Mensaje("Se cambió fecha exitósamente.",WarningType.Success);
                }
            }
        }
    }
}