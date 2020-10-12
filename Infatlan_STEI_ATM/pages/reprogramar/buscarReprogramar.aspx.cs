using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
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
                vDatos = vConexion.ObtenerTabla("STEISP_ATM_Generales 18");
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
                            Session["UsuResponsable"] = item["Usuario"].ToString();
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
        void CorreoReprogramar()
        {
            SmtpService vService = new SmtpService();
            string vQueryD = "STEISP_ATM_Generales 33,'" + Session["UsuResponsable"] + "'";
            DataTable vDatosTecnicoResponsable = vConexion.ObtenerTabla(vQueryD);
            DataTable vDatos = (DataTable)Session["AUTHCLASS"];
            string vQueryTecnicos = "STEISP_ATM_Generales 39,'" + Session["codNotificacionRE"] + "'";
            DataTable vDatosTecnicos = vConexion.ObtenerTabla(vQueryTecnicos);
            string vQueryJefes = "STEISP_ATM_Generales 38,'" + Session["codNotificacionRE"] + "'";
            DataTable vDatosJefeAgencias = vConexion.ObtenerTabla(vQueryJefes);
            
            if (vDatos.Rows.Count > 0)
            {
                foreach (DataRow item in vDatos.Rows)
                {
                    //ENVIAR A JEFE
                    if (!item["correo"].ToString().Trim().Equals(""))
                    {
                        vService.EnviarMensaje(
                        item["correo"].ToString(),
                        typeBody.ATM,
                        "Notificación de Mantenimiento ATM",
                        "Buen día, se le notifica que se ha reprogramado solicitud de mantenimiento, el encargado es " + vDatosTecnicoResponsable.Rows[0]["nombre"].ToString() + ", mantenimiento al ATM " + lbModalNomATM.Text + " para la nueva fecha " + txtNewFechaInicio.Text,
                        "El usuario <b>" + item["Nombre"].ToString() + "</b> reprogramó: <br> Notificación de Mantenimiento",
                         "",
                         "/sites/ATM/pages/mantenimiento/notificacion.aspx"
                        );

                        //vFlagEnvioSupervisor = true;
                    }
                    //ENVIAR A EDWIN
                    //string vNombre = "EDWIN ALBERTO URREA PENA";
                    vService.EnviarMensaje(ConfigurationManager.AppSettings["STEIMail"],
                            typeBody.ATM,
                            "Notificación de Mantenimiento ATM",
                            "Buen día, se le notifica que se ha reprogramado solicitud de mantenimiento, el encargado es " + vDatosTecnicoResponsable.Rows[0]["nombre"].ToString() + ", mantenimiento al ATM " + lbModalNomATM.Text + " para la fecha " + txtNewFechaInicio.Text,
                              "El usuario <b>" + item["Nombre"].ToString() + "</b> reprogramó: <br> Notificación de Mantenimiento",
                              "",
                              "/sites/ATM/pages/mantenimiento/notificacion.aspx"
                            );
                    //ENVIAR A TECNICOS ASIGNADOS
                    //string vNombreJefe = "ELVIS ALEXANDER MONTOYA PEREIRA";

                }
            }
            if (vDatosTecnicoResponsable.Rows.Count > 0)
            {
                foreach (DataRow item in vDatosTecnicoResponsable.Rows)
                {
                    //ENVIAR A RESPONSABLE
                    vService.EnviarMensaje(item["correo"].ToString(),
                        typeBody.ATM,
                       "Notificación de Mantenimiento ATM",
                        "Buen día, se le notifica que se ha reprogramado solicitud de mantenimiento, el encargado es " + item["nombre"].ToString() + ", mantenimiento al ATM " + lbModalNomATM.Text + " para la fecha " + txtNewFechaInicio.Text,
                          "El usuario <b>" + vDatos.Rows[0]["Nombre"].ToString() + "</b> reprogramó: <br> Notificación de Mantenimiento de ATM al que ha sido asignado como responsable.",
                          "",
                         "/login.aspx"
                        );
                }
            }
            if (vDatosTecnicos.Rows.Count > 0)
            {
                foreach (DataRow itemT in vDatosTecnicos.Rows)
                {
                    vService.EnviarMensaje(itemT["correo"].ToString(),
                        typeBody.ATM,
                        "Notificación de Mantenimiento ATM",
                        "Buen día, se le notifica que se ha reprogramado solicitud de mantenimiento, el encargado es " + vDatosTecnicoResponsable.Rows[0]["nombre"].ToString() + ", mantenimiento al ATM " + lbModalNomATM.Text + " para la fecha " + txtNewFechaInicio.Text,
                          "El usuario <b>" + vDatos.Rows[0]["Nombre"].ToString() + "</b> reprogramó: <br> Notificación de Mantenimiento de ATM al que ha sido asignado como parte del equipo de trabajo.",
                          "",
                         "/login.aspx"
                        );
                }
            }
            if (vDatosJefeAgencias.Rows.Count > 0)
            {
                foreach (DataRow item in vDatosJefeAgencias.Rows)
                {
                    //ENVIAR A JEFES DE AGENCIA
                    if (!item["correoJefe"].ToString().Trim().Equals(""))
                    {
                        vService.EnviarMensaje(item["correoJefe"].ToString(),
                            typeBody.ATM,
                            "Notificación de Mantenimiento ATM",
                            "Buen día, se le notifica que se ha reprogramado solicitud de mantenimiento, el encargado es " + vDatosTecnicoResponsable.Rows[0]["nombre"].ToString() + ", mantenimiento al ATM " + lbModalNomATM.Text + " para la fecha " + txtNewFechaInicio.Text,
                            "Se le informa que dicho mantenimiento se haría en la agencia al que usted se encuentra asignado.",
                            "",
                            ""
                            );
                    }
                }
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
                    string vQuery = "STEISP_ATM_Reprogramacion 1, '" + Session["codNotificacionRE"] + "','" + NewFecha + "', '" + Session["USUARIO"].ToString() + "'";
                    Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    if (vInfo != 0)
                    {
                         CorreoReprogramar();
                        //string vQuery2 = "STEISP_ATM_VerificacionTotal 8, '" + Session["codNotificacionRE"] + "'";
                        // vConexion.ejecutarSQL(vQuery2);
                        string vQuery3 = "STEISP_ATM_Reprogramacion 4, '" + Session["codNotificacionRE"] + "'";
                        vConexion.ejecutarSQL(vQuery3);

                        H5Alerta.Visible = false;
                        txtAlerta1.Visible = false;                       
                        H5Alerta.Visible = false;
                        Session["ATM_IDZONA_REPROGRAMAR"] = null;
                        Session["UsuResponsable"] = null;
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