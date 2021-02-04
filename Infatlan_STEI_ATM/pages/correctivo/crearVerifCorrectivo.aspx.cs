using Infatlan_STEI_ATM.clases;
using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Infatlan_STEI_ATM.pages.correctivo
{
    public partial class crearVerifCorrectivo : System.Web.UI.Page
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

        void cargarData()
        {
            try
            {
                DataTable vDatos = new DataTable();
                vDatos = vConexion.ObtenerTabla("STEISP_ATM_GeneralesCorrectivo 1,'" + Session["USUARIO"] + "'");
                GVBusqueda.DataSource = vDatos;
                GVBusqueda.DataBind();
                Session["ATM_CREAVERIFCOR"] = vDatos;

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
                DataTable vDatos = (DataTable)Session["ATM_CREAVERIFCOR"];

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
                    Session["ATM_CREAVERIFCOR"] = vDatosFiltrados;
                }


            }
            catch (Exception Ex)
            {
                //Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        protected void GVBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GVBusqueda.PageIndex = e.NewPageIndex;
                GVBusqueda.DataSource = (DataTable)Session["ATM_CREAVERIFCOR"];
                GVBusqueda.DataBind();
            }
            catch (Exception Ex)
            {

            }
        }

        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string ID = e.CommandArgument.ToString();
                if (e.CommandName == "Aprobar")
                {
                    TxBuscarTecnicoATM.Text = string.Empty;
                    Response.Redirect("mantCorrectivoVerificacion.aspx?cod=" + ID);
                }
                if (e.CommandName == "Cancelar")
                {
                    txtAlerta.Visible = false;
                    DataTable vDatos = new DataTable();
                    String vQuery = "STEISP_ATM_GeneralesCorrectivo 2,'" + ID + "'";
                    vDatos = vConexion.ObtenerTabla(vQuery);
                    foreach (DataRow item in vDatos.Rows)
                    {
                        txtModalATM.Text = item["Codigo"].ToString() + " - " + item["NomATM"].ToString();
                        Session["COD_COR"] = item["Codigo"].ToString();
                    }
                    myLargeModalLabel.InnerText = "Cancelar Mantenimiento No. " + ID;

                    Session["ID_COR"] = ID;
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);
                }


            }
            catch (Exception Ex)
            {
                //Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        void EnviarCorreo()
        {
            string id = Request.QueryString["id"];
            string tipo = Request.QueryString["tipo"];
            string codATM = Request.QueryString["cod"];
            Boolean vFlagEnvio = false;
            String vDestino = "";
            SmtpService vService = new SmtpService();

            String vQuery7 = "[STEISP_ATM_GeneralesCorrectivo] 15, '" + Session["COD_COR"] + "'";
            DataTable vDatos7 = vConexion.ObtenerTabla(vQuery7);

            string vCorreoEncargadoZona = "";
            if (vDatos7.Rows[0]["IDZona"].ToString() == "1")
                vCorreoEncargadoZona = "emontoya@bancatlan.hn";
            if (vDatos7.Rows[0]["IDZona"].ToString() == "2")
                vCorreoEncargadoZona = "jdgarcia@bancatlan.hn";
            if (vDatos7.Rows[0]["IDZona"].ToString() == "3")
                vCorreoEncargadoZona = "acalderon@bancatlan.hn";

            String vQuery6 = "[STEISP_ATM_GeneralesCorrectivo] 14, '" + Session["ID_COR"] + "'";
            DataTable vDatos6 = vConexion.ObtenerTabla(vQuery6);


            string vQueryD = "STEISP_ATM_Generales 33,'" + Session["USUARIO"] + "'";
            DataTable vDatosTecnicoResponsable = vConexion.ObtenerTabla(vQueryD);
            DataTable vDatos = (DataTable)Session["AUTHCLASS"];
            string vQueryJefes = "[STEISP_ATM_GeneralesCorrectivo] 13,'" + Session["COD_COR"] + "','" + vDatos6.Rows[0]["sysaid"].ToString() + "'";
            DataTable vDatosJefeAgencias = vConexion.ObtenerTabla(vQueryJefes);

            if (vDatos.Rows.Count > 0)
            {
                foreach (DataRow item in vDatos.Rows)
                {
                    //ENVIAR CREADOR
                    if (!item["correo"].ToString().Trim().Equals(""))
                    {
                        vService.EnviarMensaje(item["correo"].ToString(),
                        typeBody.ATM,
                        "Notificación de Mantenimiento Correctivo ATM",
                        "Buen día, se le notifica que se canceló una solicitud de mantenimiento correctivo, el encargado es " + vDatosTecnicoResponsable.Rows[0]["nombre"].ToString() + ", mantenimiento al ATM " + txtModalATM.Text,
                        "El usuario <b>" + item["Nombre"].ToString() + "</b> canceló: <br> Notificación de Mantenimiento Correctivo</br>Motivo cancela mantenimiento: " + txtMotivo.Text,
                        "",
                        "/sites/ATM/pages/correctivo/crearVerifCorrectivo.aspx"
                        );

                        //vFlagEnvioSupervisor = true;
                    }
                    //ENVIAR A EDWIN
                    //string vNombre = "EDWIN ALBERTO URREA PENA";
                    vService.EnviarMensaje(ConfigurationManager.AppSettings["STEIMail"],
                            typeBody.ATM,
                            "Notificación de Mantenimiento correctivo ATM",
                            "Buen día, se le notifica que se canceló solicitud de mantenimiento correctivo, el encargado es " + vDatosTecnicoResponsable.Rows[0]["nombre"].ToString() + ", mantenimiento al ATM " + txtModalATM.Text,
                              "El usuario <b>" + item["Nombre"].ToString() + "</b> canceló: <br> Notificación de Mantenimiento</br>Motivo cancela mantenimiento: " + txtMotivo.Text,
                               vCorreoEncargadoZona,
                               "/sites/ATM/pages/correctivo/crearVerifCorrectivo.aspx"
                            );

                    //PRSONAL ENCARGADO DE ATM
                    //String vKioskos = "unidadatmkiosco@bancatlan.hn";
                    //vService.EnviarMensaje(vKioskos,
                    //       typeBody.ATM,
                    //       "Notificación de Mantenimiento correctivo ATM",
                    //       "Buen día, se le notifica que se ha creado una solicitud de mantenimiento correctivo, el encargado es " + vDatosTecnicoResponsable.Rows[0]["nombre"].ToString() + ", mantenimiento al ATM " + txtATM.Text,
                    //         "El usuario <b>" + item["Nombre"].ToString() + "</b> creó: <br> Notificación de Mantenimiento",
                    //          "",
                    //       ""
                    //       );


                }
            }
            if (vDatosJefeAgencias.Rows.Count > 0)
            {
                foreach (DataRow item in vDatosJefeAgencias.Rows)
                {
                    //ENVIAR A JEFES DE AGENCIA
                    if (!item["correo"].ToString().Trim().Equals(""))
                    {
                        vService.EnviarMensaje(item["correo"].ToString(),
                            typeBody.ATM,
                            "Notificación de Mantenimiento ATM",
                                "Buen día, se le notifica que se canceló solicitud de mantenimiento correctivo, el encargado es " + vDatosTecnicoResponsable.Rows[0]["nombre"].ToString() + ", mantenimiento al ATM " + txtModalATM.Text,
                                  "Solicitud de mantenimiento correctivo a ATM fué cancelado</br>Motivo cancela mantenimiento: " + txtMotivo.Text,
                                   "",
                                   ""
                            );
                    }
                }
            }


        }

        protected void btnMantSinRealizar_Click(object sender, EventArgs e)
        {

            if (txtMotivo.Text == "")
            {
                txtAlerta.Visible = true;
            }
            else
            {
                string vQuery = "STEISP_ATM_NotificacionCorrectivo 4, '" + Session["ID_COR"] + "','" + txtMotivo.Text + "', '" + Session["USUARIO"].ToString() + "'";
                Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                if (vInfo == 1)
                {
                    EnviarCorreo();
                    txtMotivo.Text = "";
                    cargarData();
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
                    Mensaje("Mantenimiento fue cancelado exitosamente.", WarningType.Success);
                }
            }
        }
    }
}