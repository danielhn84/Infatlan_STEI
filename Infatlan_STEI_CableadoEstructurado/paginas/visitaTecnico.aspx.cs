using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Infatlan_STEI_CableadoEstructurado.clases;

namespace Infatlan_STEI_CableadoEstructurado
{
    public partial class visitaTecnico : System.Web.UI.Page
    {
        db vConexion = new db();
        protected void Page_Load(object sender, EventArgs e)
        {
            Limpiar();
            CargarResponsable();
            CargarAgencia();
        }

        void CargarAgencia(){
            try{
                String vQuery = "STEISP_CABLESTRUCTURADO_Datos 1";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                ddlAgencia.Items.Clear();
                ddlAgencia.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                foreach (DataRow item in vDatos.Rows){
                    ddlAgencia.Items.Add(new ListItem { Value = item["idAgencia"].ToString(), Text = item["nombre"].ToString() });
                }

            }catch (Exception Ex){
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        void CargarResponsable(){
           try{
                String vQuery = "STEISP_CABLESTRUCTURADO_Datos 2";

                DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                //ddlResponsable.Items.Clear();
                ddlResponsable.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                foreach (DataRow item in vDatos.Rows){
                    ddlResponsable.Items.Add(new ListItem { Value = item["idUsuario"].ToString(), Text = item["nombre"].ToString() +" "+ item["apellidos"].ToString() });
                }

            }catch (Exception Ex){
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        public void Limpiar()
        {
            //rbExpuestoRDNo.Checked = false;
            
        }

        protected void ddlResponsable_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                String vQuery = "STEISP_CABLESTRUCTURADO_Datos 3, '"+ ddlResponsable.SelectedValue +"'";

                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                txtIdentidad.Text = vDatos.Rows[0]["identidad"].ToString();            
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }

        }
    }
}