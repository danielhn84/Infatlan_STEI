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
    public partial class lvPendientesAprobarJefes : System.Web.UI.Page
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
                String vQuery = "STEISP_AGENCIA_AprobarLvJefesSuplentes 1";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                GvLvPendentesAprobar.DataSource = vDatos;
                GvLvPendentesAprobar.DataBind();
                Session["AG_LvPA_LISTAS_PENDIENTES_APROBAR_JEFE"] = vDatos;
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void GvLvPendentesAprobar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Aprobar"){
                string vIdMantenimientoAprobar = e.CommandArgument.ToString();
                Session["AG_LvPC_ID_MANTENIMIENTO_LV_APROBAR_JEFE"] = vIdMantenimientoAprobar;
                try
                {
                    //DATOS GENERALES
                    String vQuery = "STEISP_AGENCIA_AprobarLvJefesSuplentes 2," + vIdMantenimientoAprobar;
                    DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                    Session["AG_LvPA_DATOS_GENERALES"] = vDatos;
                    Session["AG_LvPA_USUARIO_RESPONSABLE"] = vDatos.Rows[0]["idUsuario"].ToString();               

                    //TECNICO RESPONSABLE
                    String vQuery1 = "STEISP_AGENCIA_AprobarLvJefesSuplentes 3," + Session["AG_LvPA_USUARIO_RESPONSABLE"];
                    DataTable vDatos1 = vConexion.obtenerDataTable(vQuery1);
                    Session["AG_LvPA_DATOS_TECNICO_RESPONSABLE"] = vDatos1;

                    //TECNICOS PARTICIPANTES
                    String vQuery2 = "STEISP_AGENCIA_AprobarLvJefesSuplentes 4," + vIdMantenimientoAprobar;
                    DataTable vDatos2 = vConexion.obtenerDataTable(vQuery2);
                    Session["AG_LvPA_DATOS_TECNICOS_PARTICIPANTES"] = vDatos2;

                    //DATOS TECNICOS PREGUNTA 
                    String vQuery3 = "STEISP_AGENCIA_AprobarLvJefesSuplentes 5," + vIdMantenimientoAprobar;
                    DataTable vDatos3 = vConexion.obtenerDataTable(vQuery3);
                    Session["AG_LvPA_DATOS_TECNICOS_PREGUNTAS"] = vDatos3;

                    //DATOS PRUEBAS DE PC
                    String vQuery4 = "STEISP_AGENCIA_AprobarLvJefesSuplentes 6," + vIdMantenimientoAprobar;
                    DataTable vDatos4 = vConexion.obtenerDataTable(vQuery4);
                    Session["AG_LvPA_DATOS_PRUEBAS_PC"] = vDatos4;
             
                    //DATOS EQUIPO DE COMUNICACION
                    String vQuery5 = "STEISP_AGENCIA_AprobarLvJefesSuplentes 7," + vIdMantenimientoAprobar;
                    DataTable vDatos5 = vConexion.obtenerDataTable(vQuery5);
                    Session["AG_LvPA_DATOS_EQUIPO_COMUNICACION"] = vDatos5;

                    //DATOS ENTORNOO CUARTO DE COMUNICACION
                    String vQuery6 = "STEISP_AGENCIA_AprobarLvJefesSuplentes 8," + vIdMantenimientoAprobar;
                    DataTable vDatos6 = vConexion.obtenerDataTable(vQuery6);
                    Session["AG_LvPA_DATOS_ENTORNO_COMUNICACION"] = vDatos6;
                   
                    //IMAGENES OBLIGATORIAS
                    String vQuery7 = "STEISP_AGENCIA_AprobarLvJefesSuplentes 9," + vIdMantenimientoAprobar;
                    DataTable vDatos7 = vConexion.obtenerDataTable(vQuery7);
                    Session["AG_LvPA_DATOS_IMAGENES_OBLIGATORIAS"] = vDatos7;
                  
                    Response.Redirect("/sites/agencias/pages/mantenimiento/lvIndividual.aspx?ex=2");
                    
                }catch (Exception ex){
                    Mensaje(ex.Message, WarningType.Danger);
                }

            }
        
        }

        protected void TxBuscarAgencia_TextChanged(object sender, EventArgs e)
        {

        }

        protected void GvLvPendentesAprobar_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GvLvPendentesAprobar.PageIndex = e.NewPageIndex;
                GvLvPendentesAprobar.DataSource = (DataTable)Session["AG_LvPA_LISTAS_PENDIENTES_APROBAR_JEFE"];
                GvLvPendentesAprobar.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
    }
}