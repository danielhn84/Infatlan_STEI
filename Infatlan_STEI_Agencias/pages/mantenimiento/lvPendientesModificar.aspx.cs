using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using Infatlan_STEI_Agencias.classes;
using System.Drawing;

namespace Infatlan_STEI_Agencias.pages
{
    public partial class lvPendientesModificar : System.Web.UI.Page
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
                String vQuery = "STEISP_AGENCIA_ModificarListaVerificacion 1," + Session["USUARIO"];
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                GvLvPendentesModificar.DataSource = vDatos;
                GvLvPendentesModificar.DataBind();
                Session["AG_LvPM_LISTAS_PENDIENTES_MODIFICAR_TECNICO"] = vDatos;
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void GvLvPendentesModificar_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Modificar")
            {
                string vIdMantenimientoModificar = e.CommandArgument.ToString();
                Session["AG_LvPM_ID_MANTENIMIENTO_LV_MODIFICAR"] = vIdMantenimientoModificar;

                try
                {
                    //DATOS GENERALES
                    String vQuery = "STEISP_AGENCIA_AprobarLvJefesSuplentes 2," + vIdMantenimientoModificar;
                    DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                    Session["AG_LvPM_DATOS_GENERALES"] = vDatos;
                    Session["AG_LvPM_USUARIO_RESPONSABLE"] = vDatos.Rows[0]["idUsuario"].ToString();

                    //TECNICO RESPONSABLE
                    String vQuery1 = "STEISP_AGENCIA_AprobarLvJefesSuplentes 3," + Session["AG_LvPM_USUARIO_RESPONSABLE"];
                    DataTable vDatos1 = vConexion.obtenerDataTable(vQuery1);
                    Session["AG_LvPM_DATOS_TECNICO_RESPONSABLE"] = vDatos1;

                    //TECNICOS PARTICIPANTES
                    String vQuery2 = "STEISP_AGENCIA_AprobarLvJefesSuplentes 4," + vIdMantenimientoModificar;
                    DataTable vDatos2 = vConexion.obtenerDataTable(vQuery2);
                    Session["AG_LvPM_DATOS_TECNICOS_PARTICIPANTES"] = vDatos2;

                    //DATOS TECNICOS PREGUNTA 
                    String vQuery3 = "STEISP_AGENCIA_AprobarLvJefesSuplentes 5," + vIdMantenimientoModificar;
                    DataTable vDatos3 = vConexion.obtenerDataTable(vQuery3);
                    Session["AG_LvPM_DATOS_TECNICOS_PREGUNTAS"] = vDatos3;

                    //DATOS PRUEBAS DE PC
                    String vQuery4 = "STEISP_AGENCIA_AprobarLvJefesSuplentes 6," + vIdMantenimientoModificar;
                    DataTable vDatos4 = vConexion.obtenerDataTable(vQuery4);
                    Session["AG_LvPM_DATOS_PRUEBAS_PC"] = vDatos4;

                    //DATOS EQUIPO DE COMUNICACION
                    String vQuery5 = "STEISP_AGENCIA_AprobarLvJefesSuplentes 7," + vIdMantenimientoModificar;
                    DataTable vDatos5 = vConexion.obtenerDataTable(vQuery5);
                    Session["AG_LvPM_DATOS_EQUIPO_COMUNICACION"] = vDatos5;

                    //DATOS ENTORNOO CUARTO DE COMUNICACION
                    String vQuery6 = "STEISP_AGENCIA_AprobarLvJefesSuplentes 8," + vIdMantenimientoModificar;
                    DataTable vDatos6 = vConexion.obtenerDataTable(vQuery6);
                    Session["AG_LvPM_DATOS_ENTORNO_COMUNICACION"] = vDatos6;

                    //IMAGENES OBLIGATORIAS
                    String vQuery7 = "STEISP_AGENCIA_AprobarLvJefesSuplentes 9," + vIdMantenimientoModificar;
                    DataTable vDatos7 = vConexion.obtenerDataTable(vQuery7);
                    Session["AG_LvPM_DATOS_IMAGENES_OBLIGATORIAS"] = vDatos7;

                    Response.Redirect("/sites/agencias/pages/mantenimiento/lvIndividual.aspx?ex=3");

                }
                catch (Exception ex)
                {
                    Mensaje(ex.Message, WarningType.Danger);
                }




            }
        }

        protected void GvLvPendentesModificar_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GvLvPendentesModificar.PageIndex = e.NewPageIndex;
                GvLvPendentesModificar.DataSource = (DataTable)Session["AG_LvPM_LISTAS_PENDIENTES_MODIFICAR_TECNICO"];
                GvLvPendentesModificar.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }

        }

        //protected void TxBuscarAgencia_TextChanged(object sender, EventArgs e)
        //{

        //    try
        //    {
        //        cargarDatos();
        //        String vBusqueda = TxBuscarAgencia.Text;
        //        DataTable vDatos = (DataTable)Session["AG_LvPM_LISTAS_PENDIENTES_MODIFICAR_TECNICO"];
        //        if (vBusqueda.Equals(""))
        //        {
        //            GvLvPendentesModificar.DataSource = vDatos;
        //            GvLvPendentesModificar.DataBind();
        //            UpLvPendientesModificar.Update();
        //        }
        //        else
        //        {
        //            EnumerableRowCollection<DataRow> filtered = vDatos.AsEnumerable()
        //                .Where(r => r.Field<String>("Lugar").Contains(vBusqueda));

        //            Boolean isNumeric = int.TryParse(vBusqueda, out int n);

        //            if (isNumeric)
        //            {
        //                if (filtered.Count() == 0)
        //                {
        //                    filtered = vDatos.AsEnumerable().Where(r =>
        //                        Convert.ToInt32(r["Cod_Agencia"]) == Convert.ToInt32(vBusqueda));
        //                }
        //            }

        //            DataTable vDatosFiltrados = new DataTable();
        //            vDatosFiltrados.Columns.Add("idTipoAgencia");
        //            vDatosFiltrados.Columns.Add("nombre");
        //            vDatosFiltrados.Columns.Add("estado");

        //            foreach (DataRow item in filtered)
        //            {
        //                vDatosFiltrados.Rows.Add(
        //                    item["id_Mantenimiento"].ToString(),
        //                    item["fecha"].ToString(),
        //                    item["Cod_Agencia"].ToString(),
        //                                                item["Cod_Agencia"].ToString()
        //                                                                            item["Cod_Agencia"].ToString()
        //                                                                                                        item["Cod_Agencia"].ToString()
        //                    );
        //            }

        //            GvLvPendentesModificar.DataSource = vDatosFiltrados;
        //            GvLvPendentesModificar.DataBind();
        //            Session["AG_LvPM_LISTAS_PENDIENTES_MODIFICAR_TECNICO"] = vDatosFiltrados;
        //            UpLvPendientesModificar.Update();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Mensaje(ex.Message, WarningType.Danger);
        //    }

        //}
    }
}