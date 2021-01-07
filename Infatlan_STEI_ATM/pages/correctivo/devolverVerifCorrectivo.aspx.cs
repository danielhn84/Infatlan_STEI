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

namespace Infatlan_STEI_ATM.pages.correctivo
{
    public partial class devolverVerifCorrectivo : System.Web.UI.Page
    {
        bd vConexion = new bd();
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

        void cargarData()
        {
            try
            {
                DataTable vDatos = new DataTable();
                vDatos = vConexion.ObtenerTabla("STEISP_ATM_GeneralesCorrectivo 7,'"+Session["USUARIO"]+"'");
                GVBusqueda.DataSource = vDatos;
                GVBusqueda.DataBind();
                Session["ATM_DEVOLVERVERIFCOR"] = vDatos;

            }
            catch (Exception Ex)
            {

            }

        }

        protected void TxBuscarTecnicoATM_TextChanged(object sender, EventArgs e)
        {

        }

        protected void GVBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GVBusqueda.PageIndex = e.NewPageIndex;
                GVBusqueda.DataSource = (DataTable)Session["ATM_DEVOLVERVERIFCOR"];
                GVBusqueda.DataBind();
            }
            catch (Exception Ex)
            {

            }
        }

        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string ID = e.CommandArgument.ToString();
                if (e.CommandName == "Aprobar")
                {
                    TxBuscarTecnicoATM.Text = string.Empty;
                    Response.Redirect("mantCorrectivoVerificacion.aspx?tipo=3&cod=" + ID);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}