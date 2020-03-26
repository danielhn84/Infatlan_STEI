using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using Infatlan_STEI_Agencias.classes;
namespace Infatlan_STEI_Agencias.paginasAgencia
{
    public partial class reprogramarMantenimiento : System.Web.UI.Page
    {

        db vConexion = new db();

        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            Session["USUARIO"] = "acamador";

            if (!Page.IsPostBack)
            {
                cargarDatos();
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
                    TxLugar.Text = item["Lugar"].ToString();
                    TxFecha.Text = item["fecha"].ToString();
                    TxArea.Text = item["Area"].ToString();
                    TxMotivo.Text= item["motivoCancelacion"].ToString();
                    TxDetalle.Text = item["detalleCancelación"].ToString();                    
                }
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
                String vNuevaFechaConvertida = Convert.ToDateTime(vNuevaFecha).ToString("yyyy/MM/dd");

                DateTime desde = Convert.ToDateTime(vNuevaFechaConvertida);


                String vQuery = "STEISP_AGENCIA_ReprogramarMantenimiento  3," + Session["AG_RM_ID_MANTENIMIENTO"] + ",'" + vNuevaFecha + "'," +Session["USUARIO"];
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
                UpdateModal.Visible = true;
                UpdateModal.Update();

            }
        }
    }
}