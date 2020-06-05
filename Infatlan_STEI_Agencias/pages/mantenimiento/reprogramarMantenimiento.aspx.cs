using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using Infatlan_STEI_Agencias.classes;
namespace Infatlan_STEI_Agencias.pages
{
    public partial class reprogramarMantenimiento : System.Web.UI.Page
    {
        db vConexion = new db();
        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        protected void Page_Load(object sender, EventArgs e){
            if (!Page.IsPostBack){
                if (Convert.ToBoolean(Session["AUTH"])){
                    cargarDatos();
                }else {
                    Response.Redirect("/login.aspx");
                }
            }
        }
        
        private void cargarDatos()
        {
            try
            {
                String vQuery = "STEISP_AGENCIA_ReprogramarMantenimiento 1";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                GvMantPendientesReprogramar.DataSource = vDatos;
                GvMantPendientesReprogramar.DataBind();
                Session["AG_RM_MANTENIMIENTOS_PENDIENTES_REPROGRAMAR"] = vDatos;
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        private void validaciones()
        {         
            if (TxNuevaFecha.Text.Equals(""))
                throw new Exception("Falta completar datos,  Favor ingresar la nueva fecha de reprogramación del mantenimiento preventivo. ");
        }

        protected void GvMantPendientesReprogramar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Reprogramar")
            {
                LimpiarModalReprogramarMantenimiento();
                string vIdMantenimientoReprogramar = e.CommandArgument.ToString();
                Session["AG_RM_ID_MANTENIMIENTO"] = vIdMantenimientoReprogramar;

                String vQuery = "STEISP_AGENCIA_ReprogramarMantenimiento 2," + vIdMantenimientoReprogramar;
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                foreach (DataRow item in vDatos.Rows)
                {

                    TxIdMant.Text = item["id_Mantenimiento"].ToString();
                    TxLugar.Text = item["Lugar1"].ToString();
                    TxFecha.Text = item["fecha"].ToString();
                    TxArea.Text = item["Area"].ToString();
                    TxMotivo.Text= item["motivoCancelacion"].ToString();
                    TxDetalle.Text = item["detalleCancelación"].ToString();

                    TituloModalReprogramar.Text= item["Lugar1"].ToString();

                }


                UpTituloReprogramar.Update();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModalReprogramarMantenimiento();", true);

            }
        }

        private void LimpiarModalReprogramarMantenimiento()
        {
            TxNuevaFecha.Text = string.Empty;
            UpdateModal.Visible = false;
        }

        protected void btnModalReprogramarMantenimiento_Click(object sender, EventArgs e)
        {
            try
            {
                validaciones();
                string vNuevaFecha = TxNuevaFecha.Text;
                String vFormato = "yyyy/MM/dd";
                String vFechaMant = Convert.ToDateTime(TxNuevaFecha.Text).ToString(vFormato); 

                String vQuery = "STEISP_AGENCIA_ReprogramarMantenimiento  3," + Session["AG_RM_ID_MANTENIMIENTO"] + ",'" + Session["USUARIO"] + "','" + vFechaMant+"'";
                Int32 vInfo = vConexion.ejecutarSql(vQuery);

                if (vInfo == 1)
                {
                    Mensaje("Mantenimiento reprogramado con exito.", WarningType.Success);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModalReprogramarMantenimiento();", true);
                }
                LimpiarModalReprogramarMantenimiento();
                cargarDatos();




            }
            catch (Exception ex)
            {
                LbMensajeModalErrorReprogramar.Text = ex.Message;
                DivAlerta.Visible = true;
                UpdateModal.Visible = true;
                UpdateModal.Update();

            }
        }

        protected void TxNuevaFecha_TextChanged(object sender, EventArgs e)
        {
            DivAlerta.Visible = false;
            UpdateModal.Update();
        }
    }
}