using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infatlan_STEI_Agencias.classes;
using System.Data;


namespace Infatlan_STEI_Agencias.pages.configuraciones
{
    public partial class creacionAgencia : System.Web.UI.Page
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
                cargarData();
                cargarDataAgencias();
            }
        }

        void cargarData()
        {
            try
            {
                DDLTipoAgencia.Items.Clear();
                String vQuery = "STEISP_AGENCIA_CreacionAgencia 1";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                DDLTipoAgencia.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                if (vDatos.Rows.Count > 0)
                {
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLTipoAgencia.Items.Add(new ListItem { Value = item["idTipoAgencia"].ToString(), Text = item["nombre"].ToString() });
                             }
                }


                DDLDepartamento.Items.Clear();
                String vQuery1 = "STEISP_AGENCIA_CreacionAgencia 2";
                DataTable vDatos1 = vConexion.obtenerDataTable(vQuery1);
                DDLDepartamento.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                if (vDatos1.Rows.Count > 0)
                {
                    foreach (DataRow item in vDatos1.Rows)
                    {
                        DDLDepartamento.Items.Add(new ListItem { Value = item["idDepartamento"].ToString(), Text = item["nombre"].ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
        private void validar()
        {
            if (TxAgencia.Text == "" || TxAgencia.Text == string.Empty)
                throw new Exception("Falta ingresar el nombre de la agencia.");

            if (TxCodigo.Text == "" || TxCodigo.Text == string.Empty)
                throw new Exception("Falta ingresar el codigo de la agencia.");

            if (TxDireccion.Text == "" || TxDireccion.Text == string.Empty)
                throw new Exception("Falta ingresar la dirección de la agencia.");

            if (TxTelefono.Text == "" || TxTelefono.Text == string.Empty)
                throw new Exception("Falta ingresar telefonos de la agencia.");

            if (TxLatitud.Text == "" || TxLatitud.Text == string.Empty)
                throw new Exception("Falta ingresar latitud de la agencia.");

            if (TxLongitud.Text == "" || TxLongitud.Text == string.Empty)
                throw new Exception("Falta ingresar longitud de la agencia.");
           
            if (DDLTipoAgencia.SelectedValue.Equals("0"))
                throw new Exception("Seleccione un tipo de agencia valido.");

            if (DDLDepartamento.SelectedValue.Equals("0"))
                throw new Exception("Seleccione un departamento agencia valido.");

            if (RblConductor.SelectedValue.Equals(""))
                throw new Exception("Falta completar opción ¿Si requiere de conductorn para el traslado hacia la agencia?.");
        }

        private void cargarDataAgencias()
        {
            try
            {
                String vQueryAgencias = "STEISP_AGENCIA_CreacionAgencia 4";
                DataTable vDatosAgencias = vConexion.obtenerDataTable(vQueryAgencias);
                GVAgencias.DataSource = vDatosAgencias;
                GVAgencias.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }


        private void limpiar()
        {
            TxAgencia.Text = String.Empty;
            TxCodigo.Text = String.Empty;
            TxDireccion.Text = String.Empty;
            TxTelefono.Text = String.Empty;
            TxLatitud.Text = String.Empty;
            TxLongitud.Text = String.Empty;
            DDLTipoAgencia.SelectedIndex = -1;
            DDLDepartamento.SelectedIndex = -1;
            RblConductor.SelectedIndex = -1;
        }

            protected void BtnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                validar();
                String vQuery1 = "STEISP_AGENCIA_CreacionAgencia 3,'"
                                   + TxAgencia.Text +
                                   "','" + TxCodigo.Text +
                                   "','" + TxDireccion.Text +
                                   "','" + TxTelefono.Text +
                                   "'," + DDLTipoAgencia.SelectedValue +
                                   ",'" + Session["USUARIO"] +
                                   "','" + TxLatitud.Text +
                                   "','" + TxLongitud.Text +
                                   "'," + DDLDepartamento.SelectedValue +
                                   "," + RblConductor.SelectedValue;
                Int32 vInformacion1 = vConexion.ejecutarSql(vQuery1);

                if (vInformacion1 == 1)
                {
                    Mensaje("Creación de agencia con exito. ", WarningType.Success);
                    limpiar();
                    cargarDataAgencias();
                    cargarData();
                }
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                limpiar();
                Mensaje("Acción cancelado con exito. ", WarningType.Success);
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }


    }
}