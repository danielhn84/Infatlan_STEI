using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infatlan_STEI_Comunicacion.classes;
using System.Data;

namespace Infatlan_STEI_Comunicacion.pages.mantenimiento
{
    public partial class pendientesModificarLV : System.Web.UI.Page
    {
        db vConexion = new db();
        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["USUARIO"] = "atercero";
            String vEx = Request.QueryString["ex"];

            if (!Page.IsPostBack)
            {
                //if (Convert.ToBoolean(Session["AUTH"]))
                //{

                if (vEx == null)
                {
                    cargarDatos();
                    UpPendientesModificarLV.Update();
                }
               
            }

            //}
            //else
            //{
            //    Response.Redirect("/login.aspx");
            //}
        }

        private void cargarDatos()
        {
            try
            {
                String vQuery = "STEISP_COMUNICACION_ModificarLV 1,'"+Session["USUARIO"].ToString()+"'";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                GvPendientesModificarLV.DataSource = vDatos;
                GvPendientesModificarLV.DataBind();
                UpPendientesModificarLV.Update();
                Session["COMUNICACION_PMLV_PENDIENTES_MODIFICAR_LV"] = vDatos;
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
    }
}