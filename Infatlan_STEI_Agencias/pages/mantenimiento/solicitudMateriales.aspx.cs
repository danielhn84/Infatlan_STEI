using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using Infatlan_STEI_Agencias.classes;

namespace Infatlan_STEI_Agencias.pages.mantenimiento
{
    public partial class solicitudMateriales : System.Web.UI.Page
    {
        db vConexion = new db();

        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            cargarDatos();
        }


        private void cargarDatos()
        {
            try
            {

                //STOCK
                String vQuery = "[STEISP_INVENTARIO_Stock] 1";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0)
                {
                    DDLArticulo.Items.Clear();
                    DDLArticulo.Items.Add(new ListItem { Value = "0", Text = "Seleccione" });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLArticulo.Items.Add(new ListItem { Value = item["idStock"].ToString(), Text = item["TipoStock"].ToString() + " - " + item["modelo"].ToString() + " (" + item["cantidad"].ToString() + ")" });
                    }
                }

                //UBICACIONES
                vQuery = "[STEISP_INVENTARIO_Ubicaciones] 1";
                vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0)
                {
                    DDLUbicacion.Items.Clear();
                    DDLUbicacion.Items.Add(new ListItem { Value = "0", Text = "Seleccione" });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLUbicacion.Items.Add(new ListItem { Value = item["idUbicacion"].ToString(), Text = item["codigo"].ToString() });
                    }
                }

                


                // TIPO TRANSACCION
                vQuery = "[STEISP_INVENTARIO_Generales] 8,1";
                vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0)
                {
                    DDLTipoTransaccion.Items.Clear();
                    DDLTipoTransaccion.Items.Add(new ListItem { Value = "0", Text = "Seleccione" });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLTipoTransaccion.Items.Add(new ListItem { Value = item["idTipoTransaccion"].ToString(), Text = item["nombre"].ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
    }
}