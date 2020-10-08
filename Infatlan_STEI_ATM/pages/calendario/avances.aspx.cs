using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infatlan_STEI_ATM.clases;
using System.Data;
using System.IO;
using System.Configuration;

namespace Infatlan_STEI_ATM.pages.calendario
{
    public partial class avances : System.Web.UI.Page
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
            //AVANCES
            DataTable vDatos2 = new DataTable();
            vDatos2 = vConexion.ObtenerTabla("[STEISP_ATM_Generales] 41");
            GVAvances.DataSource = vDatos2;
            GVAvances.DataBind();
            Session["ATM_AVANCES"] = vDatos2;
        }

        protected void GVAvances_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GVAvances.PageIndex = e.NewPageIndex;
                GVAvances.DataSource = (DataTable)Session["ATM_AVANCES"];
                GVAvances.DataBind();
            }
            catch (Exception Ex)
            {

            }
        }
    }
}