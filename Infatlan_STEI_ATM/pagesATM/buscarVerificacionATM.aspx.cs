using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using Infatlan_STEI_ATM.clases;

namespace Infatlan_STEI_ATM
{
    public partial class buscarVerificacionATM : System.Web.UI.Page
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

        void limpiarModalVerificacion()
        {
            txtModalATM.Text=string.Empty;
            DDLModalMotivo.SelectedValue = "0";
            DDLModalcambioPor.SelectedValue = "0";
            DDLModalNewTecnico.SelectedValue = "0";
            txtdetalleCancela.Text = string.Empty;
        }
        void cargarData()
        {
            string usu = "acedillo";
            try
            {
                DataTable vDatos = new DataTable();
                vDatos = vConexion.ObtenerTabla("STEISP_ATM_VERIFICACION 1, '"+usu+"','"+Session["COD_VERIFMANTE_ATM"]+"'");
                GVBusqueda.DataSource = vDatos;
                GVBusqueda.DataBind();
                Session["ATM_VERIF_CARGAR"] = vDatos;
                //Session["UPDATEATM"] = 1;

            }
            catch (Exception Ex)
            {

            }

            if (HttpContext.Current.Session["ModalVerif"] == null)
            {
                try
                {
                    DDLModalNewTecnico.Items.Clear();
                    String vQuery = "STEISP_AGENCIA_CreacionNotificacion 5";
                    DataTable vDatos = vConexion.ObtenerTabla(vQuery);
                    DDLModalNewTecnico.Items.Add(new ListItem { Value = "0", Text = "Seleccione nuevo técnico..." });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLModalNewTecnico.Items.Add(new ListItem { Value = item["idUsuario"].ToString(), Text = item["nombre"].ToString() });
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }
                Session["ModalVerif"] = 1;
            }

        }
        protected void TxBuscarTecnicoATM_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cargarData();

                String vBusqueda = TxBuscarTecnicoATM.Text;
                DataTable vDatos = (DataTable)Session["ATM_VERIF_CARGAR"];

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
                        .Where(r => r.Field<String>("NomATM").Contains(vBusqueda));

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
                    Session["ATM_VERIF_CARGAR"] = vDatosFiltrados;
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
                GVBusqueda.DataSource = (DataTable)Session["ATM_VERIF_CARGAR"];
                GVBusqueda.DataBind();
            }
            catch (Exception Ex)
            {

            }
        }

        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            limpiarModalVerificacion();
            lbValidarModal.Visible = false;
            string usu = "acedillo";
            try
            {
                DataTable vDataaaa = (DataTable)Session["ATM_VERIF_CARGAR"];
                string codVerif = e.CommandArgument.ToString();

                if (e.CommandName == "Aprobar")
                {
                    try
                    {
                        DataTable vDatos = new DataTable();
                        vDatos = vConexion.ObtenerTabla("STEISP_ATM_VERIFICACION 2, '" + usu + "','" + codVerif + "'");
                        //vDatos = vConexion.ObtenerTabla(vQuery);
                        foreach (DataRow item in vDatos.Rows)
                        {
                            
                            Session["ATM_CODATM_VERIF_CREAR"] = item["Codigo"].ToString();
                            Session["ATM_NOMATM_VERIF_CREAR"] = item["NomATM"].ToString();
                            Session["ATM_DIRECCION_VERIF_CREAR"] = item["Direccion"].ToString();
                            Session["ATM_IP_VERIF_CREAR"] = item["IP"].ToString();
                            Session["ATM_PUERTOATM_VERIF_CREAR"] = item["Puerto"].ToString();
                            Session["ATM_TECLADO_VERIF_CREAR"] = item["Teclado"].ToString();
                            Session["ATM_PROCESADOR_VERIF_CREAR"] = item["Procesador"].ToString();
                            Session["ATM_TIPOCARGA_VERIF_CREAR"] = item["TipoCarga"].ToString();
                            Session["ATM_MARCA_VERIF_CREAR"] = item["Marca"].ToString();
                            Session["ATM_SERIEDISCO_VERIF_CREAR"] = item["SerieDisco"].ToString();
                            Session["ATM_SERIEATM_VERIF_CREAR"] = item["SerieATM"].ToString();
                            Session["ATM_CAPACIDADDISCO_VERIF_CREAR"] = item["CapacidadDisco"].ToString();
                            Session["ATM_INVENTARIO_VERIF_CREAR"] = item["Inventario"].ToString();
                            Session["ATM_RAM_VERIF_CREAR"] = item["Ram"].ToString();
                            Session["ATM_LATITUD_VERIF_CREAR"] = item["Latitud"].ToString();
                            Session["ATM_LONGITUD_VERIF_CREAR"] = item["Longitud"].ToString();
                            Session["ATM_UBICACION_VERIF_CREAR"] = item["Ubicacion"].ToString();
                            Session["ATM_IDUBI_VERIF_CREAR"] = item["IdUbi"].ToString();
                            Session["ATM_SUCURSAL_VERIF_CREAR"] = item["Sucursal"].ToString();
                            Session["ATM_DEPTO_VERIF_CREAR"] = item["Departamento"].ToString();
                            Session["ATM_ZONA_VERIF_CREAR"] = item["Zona"].ToString();
                            Session["ATM_IDMANT_VERIF_CREAR"] = codVerif;
                            Session["ATM_ESTADO_VERIF_CREAR"] = item["Estado"].ToString();
                            Session["ATM_FECHAMANT_VERIF_CREAR"] = Convert.ToDateTime(item["FechaMantenimiento"]).ToString("yyyy/MM/dd");
                            Session["ATM_HRINICIO_VERIF_CREAR"] = item["HrInicio"].ToString();
                            Session["ATM_HRFIN_VERIF_CREAR"] = item["HrFin"].ToString();
                            Session["ATM_AUTORIZADO_VERIF_CREAR"] = item["Autorizado"].ToString();
                            Session["ATM_SYSAID_VERIF_CREAR"] = item["SysAid"].ToString();
                            Session["ATM_TECNICO_VERIF_CREAR"] = item["Tecnico"].ToString();
                            Session["ATM_USUARIO_VERIF_CREAR"] = item["Usuario"].ToString();
                            Session["ATM_IDENTIDAD_VERIF_CREAR"] = item["Identidad"].ToString();
                            Session["ATM_SO_VERIF_CREAR"] = item["SO"].ToString();
                            Session["ATM_VERSIONSW_VERIF_CREAR"] = item["VersionSw"].ToString();                 
                        }
                        TxBuscarTecnicoATM.Text = string.Empty;
                       
                        Response.Redirect("verificacionATM.aspx");
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
                if (e.CommandName == "Reprogramar")
                {

                    try
                    {
                        DataTable vDatos2 = new DataTable();
                        vDatos2 = vConexion.ObtenerTabla("STEISP_ATM_VERIFICACION 2, '" + usu + "','" + codVerif + "'");
                        //vDatos = vConexion.ObtenerTabla(vQuery);
                        foreach (DataRow item in vDatos2.Rows)
                        {                            
                            txtModalATM.Text = item["NomATM"].ToString();
                            //Session["ATM_DIRECCION_VERIF_CREAR"] = item["Direccion"].ToString();
                            //Session["ATM_IP_VERIF_CREAR"] = item["IP"].ToString();                           
                        }
                        TxBuscarTecnicoATM.Text = string.Empty;                       
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);
                }

            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        protected void btnMantSinRealizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtdetalleCancela.Text == "" || DDLModalMotivo.SelectedValue=="0"|| DDLModalcambioPor.SelectedValue=="0")
                   
                {
                   lbValidarModal.Visible = true;
                    lbValidarModal.Text = "No deje campos vacios";
                }
                else
                {
                    limpiarModalVerificacion();
                    lbValidarModal.Visible = false;
                    

                }
            }
            catch (Exception EX)
            {
                Mensaje(EX.Message, WarningType.Danger);
            }
        }
    }
}