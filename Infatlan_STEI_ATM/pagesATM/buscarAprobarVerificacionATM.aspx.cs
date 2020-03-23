using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using Infatlan_STEI_ATM.clases;

namespace Infatlan_STEI_ATM.pagesATM
{
    public partial class buscarAprobarVerificacionATM : System.Web.UI.Page
    {
        bd vConexion = new bd();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                cargarData();
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
                        Session["ATM_CODVERIF"] = codVerificacion;
                        Session["ATM_NOMBREVERIF"] = item["NomATM"].ToString();
                        Session["ATM_DIRECCIONVERIF"] = item["Direccion"].ToString();
                        Session["ATM_IPVERIF"] = item["IP"].ToString();
                        Session["ATM_PUERTOVERIF"] = item["Puerto"].ToString();
                        Session["ATM_SERIEDISCOVERIF"] = item["SerieDisco"].ToString();
                        Session["ATM_CAPACIDADDISCODUROVERIF"] = item["CapacidadDiscoDuro"].ToString();
                        Session["ATM_SERIEATMVERIF"] = item["SerieATM"].ToString();
                        Session["ATM_INVENTARIOVERIF"] = item["Inventario"].ToString();
                        Session["ATM_RAMVERIF"] = item["RAM"].ToString();
                        Session["ATM_LONGITUDVERIF"] = item["Longitud"].ToString();
                        Session["ATM_LATITUDVERIF"] = item["Latitud"].ToString();
                        Session["ATM_SOVERIF"] = item["SO"].ToString();
                        Session["ATM_VERSIONVERIF"] = item["VersionSW"].ToString();
                        Session["ATM_TECLADOVERIF"] = item["Teclado"].ToString();
                        Session["ATM_PROCESADORVERIF"] = item["Procesador"].ToString();
                        Session["ATM_TIPOCARGAVERIF"] = item["TipoCarga"].ToString();
                        Session["ATM_MARCAVERIF"] = item["Marca"].ToString();

                        Session["ATM_UBICACIONVERIF"] = item["Ubicacion"].ToString();
                        Session["ATM_SUCURSALVERIF"] = item["Sucursal"].ToString();
                        Session["ATM_ZONAVERIF"] = item["Zona"].ToString();
                        Session["ATM_FECHAMANTVERIF"] = item["FechaMantenimiento"].ToString();
                        Session["ATM_HRINICIOVERIF"] = item["HrInicio"].ToString();
                        Session["ATM_HRFINVERIF"] = item["HrFin"].ToString();
                        Session["ATM_AUTORIZADOVERIF"] = item["Autorizado"].ToString();
                        Session["ATM_CANCELARVERIF"] = item["Cancelado"].ToString();
                        Session["ATM_SYSAIDVERIF"] = item["SysAid"].ToString();
                        Session["ATM_TECNICOVERIF"] = item["Tecnico"].ToString();
                        Session["ATM_USUARIOVERIF"] = item["Usuario"].ToString();
                        Session["ATM_IDENTIDADVERIF"] = item["Identidad"].ToString();
                        Session["ATM_CODATMVERIF"] = item["Codigo"].ToString();
                        Session["ATM_OBSERVACIONESVERIF"] = item["ObservacionesVerif"].ToString();
                        Session["ATM_HRSALIDAINFAVERIF"] = item["HoraSalidaInfa"].ToString();
                        Session["ATM_HRENTRADAINFAVERIF"] = item["HoraEntradaInfa"].ToString();

                    }
                    
                    DataTable vDatos2 = new DataTable();
                    string vQuery2 = "STEISP_ATM_Generales 25, '" + Session["ATM_CODVERIF"] + "'";
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



                        TxBuscarTecnicoATM.Text = string.Empty;
                    Response.Redirect("aprobarVerificacionATM.aspx");

                }
                catch (Exception Ex)
                {
                    Mensaje(Ex.Message, WarningType.Danger);
                }

            }
        }
    }
}