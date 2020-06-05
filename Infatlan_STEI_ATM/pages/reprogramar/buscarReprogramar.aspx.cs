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

namespace Infatlan_STEI_ATM.pages.reprogramar
{
    public partial class buscarReprogramar : System.Web.UI.Page
    {
        bd vConexion = new bd();
        protected void Page_Load(object sender, EventArgs e){
            if (!Page.IsPostBack){
                if (Convert.ToBoolean(Session["AUTH"])){
                    cargarData();
                }else {
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
                vDatos = vConexion.ObtenerTabla("STEISP_ATM_Generales 18, 1");
                GVBusqueda.DataSource = vDatos;
                GVBusqueda.DataBind();
                Session["ATM_REPROGRAMAR_CARGAR"] = vDatos;
                //Session["UPDATEATM"] = 1;

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
                DataTable vDatos = (DataTable)Session["ATM_REPROGRAMAR_CARGAR"];

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
                    Session["ATM_REPROGRAMAR_CARGAR"] = vDatosFiltrados;
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
                GVBusqueda.DataSource = (DataTable)Session["ATM_REPROGRAMAR_CARGAR"];
                GVBusqueda.DataBind();
            }
            catch (Exception Ex)
            {

            }
        }

        void EnviarCorreo()
        {
            string id = Request.QueryString["id"];
            string tipo = Request.QueryString["tipo"];
            string vCorreo = "acedillo@bancatlan.hn";
            string vNombre = "Adán Cedillo";
            string vUsu = "acedillo";
            SmtpService vService = new SmtpService();
          
                //SOLICITANTE                
                string vMotivo = "Se informa que mantenimiento fué reprogramado.";
                string vMsg = "Puede continuar con el proceso.";
                vService.EnviarMensaje(vCorreo,
                   typeBody.Aprobado,
                   Session["usuATM"].ToString(),
                   vNombre,
                   vMotivo,
                   vMsg
                   );
                //SUPERVISOR                 
                string vMotivo2 = "El empleado fué notificado de la reprogramación de mantenimiento.";
                string vMsg2 = "Solicitud fue aprobado exitosamente.";
                vService.EnviarMensaje(vCorreo,
                   typeBody.Supervisor,
                   vUsu,
                   vNombre,
                   vMotivo2,
                   vMsg2
                   );          
        }
        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            H5Alerta.Visible = false;
            txtAlerta1.Visible = false;
            txtNewFechaInicio.Text = string.Empty;
            try
            {
                DataTable vDataaaa = (DataTable)Session["ATM_REPROGRAMAR_CARGAR"];
                string codReprogramacion = e.CommandArgument.ToString();

                if (e.CommandName == "Aprobar")
                {
                    try
                    {
                        DataTable vDatos = new DataTable();
                        String vQuery = "STEISP_ATM_Generales 19,'" + codReprogramacion + "'";
                        vDatos = vConexion.ObtenerTabla(vQuery);
                        foreach (DataRow item in vDatos.Rows)
                        {                           
                            Session["codNotificacionRE"] = item["ID"].ToString();
                            lbModalNomATM.Text = item["NomATM"].ToString();                            
                            lbModalFechaMan.Text = Convert.ToDateTime(item["FechaMantenimiento"]).ToString("yyyy/MM/dd");                           
                            lbModalCodATM.Text = item["Codigo"].ToString();
                            lbQuienCancelo.Text= item["CanceladoPor"].ToString();
                            lbMotivoCancelo.Text= item["NCancelar"].ToString();
                            lbdetalle.Text= item["DetMotivo"].ToString();
                            Session["ATM_IDZONA_REPROGRAMAR"]= item["IDZona"].ToString();
                        }
                        TxBuscarTecnicoATM.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);
                        //Response.Redirect("aprobarNotificacionATM.aspx");
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }


            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        protected void btnReprogramarNotif_Click(object sender, EventArgs e)
        {
            
            //lbModalFechaMan.Text = "";
            if (txtNewFechaInicio.Text == "" || txtNewFechaInicio.Text == string.Empty)
            {
                txtAlerta1.Visible = true;
                H5Alerta.Visible = true;
            }
            else
            {
                
                String vFormato = "yyyy/MM/dd";
                string NewFecha = Convert.ToDateTime(txtNewFechaInicio.Text).ToString(vFormato);
                try
                {
                    string vQuery = "STEISP_ATM_Reprogramacion 1, '" + Session["codNotificacionRE"] + "','" + NewFecha + "', '" + Session["usuATM"].ToString() + "'";
                    Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    if (vInfo == 1)
                    {
                        H5Alerta.Visible = false;
                        txtAlerta1.Visible = false;
                        //ELIMINAR JEFES DE AGENCIA
                        string vQuery2 = "STEISP_ATM_Reprogramacion 2, '" + Session["codNotificacionRE"] + "','" + NewFecha + "', '" + Session["usuATM"].ToString() + "'";
                        Int32 vInfo2 = vConexion.ejecutarSQL(vQuery2);
                        //ELIMINAR TECNICOS PARTICIPANTES
                        string vQuery3 = "STEISP_ATM_Reprogramacion 3, '" + Session["codNotificacionRE"] + "','" + NewFecha + "', '" + Session["usuATM"].ToString() + "'";
                        Int32 vInfo3 = vConexion.ejecutarSQL(vQuery3);
                        H5Alerta.Visible = false;

                        txtNewFechaInicio.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
                        Mensaje("Mantenimiento reprogramado con éxito", WarningType.Success);
                        UpdateGridView.Update();
                        //EnviarCorreo();
                        cargarData();
                    }
                    else
                    {
                        H5Alerta.InnerText = "No se pudo reprogramar mantenimiento";
                        H5Alerta.Visible = true;
                    }
                }
                catch (Exception Ex)
                {
                    Mensaje(Ex.Message, WarningType.Danger);
                }
            }
        }

        protected void btnCerrarReprogramarNotif_Click(object sender, EventArgs e)
        {
            txtAlerta1.Visible = false;

            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
        }
    }
}