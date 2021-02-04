using Infatlan_STEI_ATM.clases;
using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Infatlan_STEI_ATM.pages.mantenimiento
{
    public partial class buscarAprobarVerificacion : System.Web.UI.Page
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

        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }
        void cargarData()
        {
            try
            {
                DataTable vDatos = new DataTable();
                vDatos = vConexion.ObtenerTabla("STEISP_ATM_Generales 22, 1");
                GVBusqueda.DataSource = vDatos;
                GVBusqueda.DataBind();
                Session["ATM_APROBVERIF_CARGAR"] = vDatos;
                Session["UPDATEATM"] = 1;

            }
            catch (Exception Ex)
            {

            }
        }
        protected void TxBuscarTecnicoATM_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cargarData();

                String vBusqueda = TxBuscarTecnicoATM.Text;
                DataTable vDatos = (DataTable)Session["ATM_APROBVERIF_CARGAR"];

                if (vBusqueda.Equals(""))
                {
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                    UpdateGridView.Update();
                    //cargarData();
                }
                else
                {
                    EnumerableRowCollection<DataRow> filtered = vDatos.AsEnumerable()
                        .Where(r => r.Field<String>("Tecnico").Contains(vBusqueda));

                    DataTable vDatosFiltrados = new DataTable();
                    vDatosFiltrados.Columns.Add("ID");
                    vDatosFiltrados.Columns.Add("Codigo");
                    vDatosFiltrados.Columns.Add("NomATM");
                    vDatosFiltrados.Columns.Add("Ubicacion");
                    vDatosFiltrados.Columns.Add("Sucursal");
                    vDatosFiltrados.Columns.Add("Tecnico");
                    foreach (DataRow item in filtered)
                    {
                        vDatosFiltrados.Rows.Add(
                            item["ID"].ToString(),
                            item["Codigo"].ToString(),
                            item["NomATM"].ToString(),
                            item["Ubicacion"].ToString(),
                            item["Sucursal"].ToString(),
                            item["Tecnico"].ToString()
                            );
                    }

                    GVBusqueda.DataSource = vDatosFiltrados;
                    GVBusqueda.DataBind();
                    Session["ATM_APROBVERIF_CARGAR"] = vDatosFiltrados;
                    UpdateGridView.Update();
                }


            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        protected void GVBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GVBusqueda.PageIndex = e.NewPageIndex;
                GVBusqueda.DataSource = (DataTable)Session["ATM_APROBVERIF_CARGAR"];
                GVBusqueda.DataBind();
            }
            catch (Exception Ex)
            {

            }
        }

        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            DataTable vDataaaa = (DataTable)Session["ATM_APROBVERIF_CARGAR"];
            string codVerificacion = e.CommandArgument.ToString();

            if (e.CommandName == "Aprobar")
            {
                try
                {
                    DataTable vDatos = new DataTable();
                    String vQuery = "STEISP_ATM_Generales 23,'" + codVerificacion + "'";
                    vDatos = vConexion.ObtenerTabla(vQuery);
                    foreach (DataRow item in vDatos.Rows)
                    {
                        Session["ATM_COD_VERIF"] = codVerificacion;
                        Session["ATM_CODATM_VERIF_CREAR"] = item["Codigo"].ToString();
                        Session["ATM_NOMATM_VERIF_CREAR"] = item["NomATM"].ToString();
                        Session["ATM_DIRECCION_VERIF_CREAR"] = item["Direccion"].ToString();
                        Session["ATM_IP_VERIF_CREAR"] = item["IP"].ToString();
                        Session["ATM_PUERTOATM_VERIF_CREAR"] = item["Puerto"].ToString();
                        Session["ATM_TECLADO_VERIF_CREAR"] = item["IDTeclado"].ToString();
                        Session["ATM_PROCESADOR_VERIF_CREAR"] = item["IDProcesador"].ToString();
                        Session["ATM_TIPOCARGA_VERIF_CREAR"] = item["IDTipoCarga"].ToString();
                        Session["ATM_MARCA_VERIF_CREAR"] = item["IDMarca"].ToString();
                        Session["ATM_SERIEDISCO_VERIF_CREAR"] = item["SerieDisco"].ToString();
                        Session["ATM_SERIEATM_VERIF_CREAR"] = item["SerieATM"].ToString();
                        Session["ATM_CAPACIDADDISCO_VERIF_CREAR"] = item["CapacidadDiscoDuro"].ToString();
                        Session["ATM_INVENTARIO_VERIF_CREAR"] = item["Inventario"].ToString();
                        Session["ATM_RAM_VERIF_CREAR"] = item["RAM"].ToString();
                        Session["ATM_LATITUD_VERIF_CREAR"] = item["Latitud"].ToString();
                        Session["ATM_LONGITUD_VERIF_CREAR"] = item["Longitud"].ToString();
                        Session["ATM_UBICACION_VERIF_CREAR"] = item["Ubicacion"].ToString();
                        Session["ATM_OBSERVACIONES_VERIF_CREAR"] = item["ObservacionesVerif"].ToString();
                        Session["ATM_SUCURSAL_VERIF_CREAR"] = item["Sucursal"].ToString();
                        Session["ATM_HRSALIDAINFA_VERIF_CREAR"] = item["HoraSalidaInfa"].ToString();
                        Session["ATM_HRENTRADAINFA_VERIF_CREAR"] = item["HoraEntradaInfa"].ToString();
                        Session["ATM_ZONA_VERIF_CREAR"] = item["Zona"].ToString();
                        Session["ATM_IDZONA_VERIF_CREAR"] = item["IDZona"].ToString();
                        Session["ATM_IDMANT_VERIF_CREAR"] = codVerificacion;
                        Session["ATM_ESTADO_VERIF_CREAR"] = item["Estado"].ToString();
                        Session["ATM_FECHAMANT_VERIF_CREAR"] = Convert.ToDateTime(item["FechaMantenimiento"]).ToString("yyyy/MM/dd");
                        Session["ATM_HRINICIO_VERIF_CREAR"] = item["HrInicio"].ToString();
                        Session["ATM_HRFIN_VERIF_CREAR"] = item["HrFin"].ToString();
                        Session["ATM_AUTORIZADO_VERIF_CREAR"] = item["Autorizado"].ToString();
                        Session["ATM_SYSAID_VERIF_CREAR"] = item["SysAid"].ToString();
                        Session["ATM_TECNICO_VERIF_CREAR"] = item["Tecnico"].ToString();
                        Session["ATM_USUARIO_VERIF_CREAR"] = item["Usuario"].ToString();
                        Session["ATM_USUCORREO_VERIF_CREAR"] = item["CorreoTecnico"].ToString();
                        Session["ATM_IDENTIDAD_VERIF_CREAR"] = item["Identidad"].ToString();
                        Session["ATM_SO_VERIF_CREAR"] = item["SO"].ToString();
                        Session["ATM_VERSIONSW_VERIF_CREAR"] = item["VersionSw"].ToString();
                        Session["ATM_USUCREADOR_VERIF_CREAR"] = item["UsuarioCreador"].ToString();
                        Session["ATM_ATMACTIVO_VERIF_CREAR"] = item["ATMActivo"].ToString();
                        Session["ATM_USU_RESPONSABLE_MANT"] = item["UsuResponsable"].ToString();

                    }

                    DataTable vDatos4 = new DataTable();
                    String vQuery4 = "STEISP_AGENCIA_CreacionNotificacion 6,'" + Session["ATM_USUCREADOR_VERIF_CREAR"].ToString() + "'";
                    vDatos4 = vConexion.ObtenerTabla(vQuery4);
                    foreach (DataRow item in vDatos4.Rows)
                    {
                        Session["ATM_NOMBRECREADOR_VERIF"] = item["nombre"].ToString();
                        Session["ATM_APELLIDOCREADOR_VERIF"] = item["apellidos"].ToString();
                        Session["ATM_CORREOCREADOR_VERIF"] = item["correo"].ToString();
                    }

                    DataTable vDatos2 = new DataTable();
                    string vQuery2 = "STEISP_ATM_Generales 25, '" + codVerificacion + "'";
                    vDatos2 = vConexion.ObtenerTabla(vQuery2);
                    foreach (DataRow item2 in vDatos2.Rows)
                    {
                        Session["ATM_VERIF_PREG1"] = item2["pregunta1"].ToString();
                        Session["ATM_VERIF_PREG2"] = item2["pregunta2"].ToString();
                        Session["ATM_VERIF_PREG3"] = item2["pregunta3"].ToString();
                        Session["ATM_VERIF_PREG4"] = item2["pregunta4"].ToString();
                        Session["ATM_VERIF_PREG5"] = item2["pregunta5"].ToString();
                        Session["ATM_VERIF_PREG6"] = item2["pregunta6"].ToString();
                        Session["ATM_VERIF_PREG7"] = item2["pregunta7"].ToString();
                        Session["ATM_VERIF_PREG8"] = item2["pregunta8"].ToString();
                        Session["ATM_VERIF_PREG9"] = item2["pregunta9"].ToString();
                        Session["ATM_VERIF_PREG10"] = item2["pregunta10"].ToString();
                        Session["ATM_VERIF_PREG11"] = item2["pregunta11"].ToString();
                        Session["ATM_VERIF_PREG12"] = item2["pregunta12"].ToString();
                        Session["ATM_VERIF_PREG13"] = item2["pregunta13"].ToString();
                        Session["ATM_VERIF_PREG14"] = item2["pregunta14"].ToString();
                        Session["ATM_VERIF_PREG15"] = item2["pregunta15"].ToString();
                        Session["ATM_VERIF_PREG16"] = item2["pregunta16"].ToString();
                        Session["ATM_VERIF_PREG17"] = item2["pregunta17"].ToString();
                        Session["ATM_VERIF_PREG18"] = item2["pregunta18"].ToString();
                        Session["ATM_VERIF_PREG19"] = item2["pregunta19"].ToString();
                        Session["ATM_VERIF_PREG20"] = item2["pregunta20"].ToString();
                        Session["ATM_VERIF_PREG21"] = item2["pregunta21"].ToString();
                        Session["ATM_VERIF_IMG21"] = item2["imagen21"].ToString();
                        Session["ATM_VERIF_PREG22"] = item2["pregunta22"].ToString();
                        Session["ATM_VERIF_IMG22"] = item2["imagen22"].ToString();
                        Session["ATM_VERIF_PREG23"] = item2["pregunta23"].ToString();
                        Session["ATM_VERIF_RESP23"] = item2["respuesta23"].ToString();
                    }

                    DataTable vDatos3 = new DataTable();
                    string vQuery3 = "STEISP_ATM_Generales 24, '" + codVerificacion + "'";
                    vDatos3 = vConexion.ObtenerTabla(vQuery3);
                    foreach (DataRow item3 in vDatos3.Rows)
                    {
                        Session["ATM_VERIF_IMG1"] = item3["discoDuroImg"].ToString();
                        Session["ATM_VERIF_IMG2"] = item3["atmDesarmadoPSImg"].ToString();
                        Session["ATM_VERIF_IMG3"] = item3["atmDesarmadoPIImg"].ToString();
                        Session["ATM_VERIF_IMG4"] = item3["vendorImg"].ToString();
                        Session["ATM_VERIF_IMG5"] = item3["systemInfoImg"].ToString();
                        Session["ATM_VERIF_IMG6"] = item3["antiSkimmingImg"].ToString();
                        Session["ATM_VERIF_IMG7"] = item3["monitorFiltroImg"].ToString();
                        Session["ATM_VERIF_IMG8"] = item3["padleWheelImg"].ToString();
                        Session["ATM_VERIF_IMG9"] = item3["dispositivoDesarmadoImg"].ToString();
                        Session["ATM_VERIF_IMG10"] = item3["teclado"].ToString();
                        Session["ATM_VERIF_IMG11"] = item3["ATMActivo"].ToString();
                    }


                    TxBuscarTecnicoATM.Text = string.Empty;
                    Response.Redirect("verificacion.aspx?id=3&tipo=4");
                    //Response.Redirect("aprobarVerificacion.aspx");

                }
                catch (Exception Ex)
                {
                    Mensaje(Ex.Message, WarningType.Danger);
                }

            }
        }
    }
}