using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infatlan_STEI_Comunicacion.classes;
using System.Data;

namespace Infatlan_STEI_Comunicacion.pages.configuraciones
{
    public partial class calendarioAnualMantenimiento : System.Web.UI.Page
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
                try
                {
                    DdlResponsable.Items.Clear();
                    DDLNodo.Items.Clear();
                    String vQuery = "STEISP_COMUNICACION_AsignarResponsable 2";
                    DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                    DdlResponsable.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    if (vDatos.Rows.Count > 0)
                    {
                        foreach (DataRow item in vDatos.Rows)
                        {
                            DdlResponsable.Items.Add(new ListItem { Value = item["idUsuario"].ToString(), Text = item["nombre"].ToString() });
                        }
                    }

                    vQuery = "STEISP_COMUNICACION_AsignarResponsable 6";
                    vDatos = vConexion.obtenerDataTable(vQuery);
                    DDLNodo.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    if (vDatos.Rows.Count > 0)
                    {
                        foreach (DataRow item in vDatos.Rows)
                        {
                            DDLNodo.Items.Add(new ListItem { Value = item["idStockEDC"].ToString(), Text = item["nombreNodo"].ToString() + " (" + item["tipoStock"].ToString() + ")" });
                        }
                    }


                }
                catch (Exception ex)
                {
                    Mensaje(ex.Message, WarningType.Danger);
                }
            }
        }

        protected void CBEmergencia_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LbValidacion.Text = "";
                DivMensaje.Visible = false;
                UpdatePanel6.Update();

                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "abrirModalEmergencia();", true);
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void DDLNodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String vQuery = "STEISP_COMUNICACION_AsignarResponsable 7,"+ DDLNodo.SelectedValue;
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                TxLugar.Text= vDatos.Rows[0]["direccion"].ToString();

            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        private void validacionesEmergencia()
        {
            if (DDLNodo.SelectedValue.Equals("0"))
                throw new Exception("Favor seleccione nodo al que se le va proporcionar mantenimiento.");

            if (TxFechaMantenimiento.Text == "" || TxFechaMantenimiento.Text == string.Empty)
                throw new Exception("Favor seleccione fecha del mantenimiento.");

            if (DdlResponsable.SelectedValue.Equals("0"))
                throw new Exception("Favor seleccione tecnico responsable.");

            if (TxMotivo.Text == "" || TxMotivo.Text == string.Empty)
                throw new Exception("Favor ingrese detalle del motivo.");
        }

        private void limpiarEmergencia()
        {
            TxMotivo.Text = string.Empty;
            DdlResponsable.SelectedValue.Equals("0");
            TxFechaMantenimiento.Text = string.Empty;
            DDLNodo.SelectedValue.Equals("0");
            CBEmergencia.Checked = false;
            UpdatePanel2.Update();

        }

            protected void BtnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                validacionesEmergencia();
                String vFormato = "yyyy/MM/dd"; //"dd/MM/yyyy HH:mm:ss"
                String vFechaMant = Convert.ToDateTime(TxFechaMantenimiento.Text).ToString(vFormato);

                String vQuery = "STEISP_COMUNICACION_AsignarResponsable 8," 
                                + DDLNodo.SelectedValue 
                                +",'"+ vFechaMant
                                +"','"+ Session["USUARIO"]
                                + "','" + DdlResponsable.SelectedValue
                                + "','" + Session["USUARIO"]
                                +"','1','" + TxMotivo.Text + "'";
                Int32 vInformacion1 = vConexion.ejecutarSql(vQuery);
                limpiarEmergencia();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "cerrarModalEmergencia();", true);
                if (vInformacion1 == 1)
                {
                    Mensaje("Se guardaron exitosamente los registros.", WarningType.Success);
                }
                else
                {
                    Mensaje("Favor contactarse con el administrador, hubo un problema al guardar los registros.", WarningType.Danger);
                }
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
                LbValidacion.Text = ex.Message;
                DivMensaje.Visible = true;
                UpdatePanel6.Update();
            }
        }
    }
}