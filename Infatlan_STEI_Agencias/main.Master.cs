using Infatlan_STEI_Agencias.classes;
using System;
using System.Data;
using System.Web.UI;

namespace Infatlan_STEI
{
    public partial class main : System.Web.UI.MasterPage
    {
        db vConexion = new db();
        Security vSecurity = new Security();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Convert.ToBoolean(Session["AUTH"]))
                    Response.Redirect("/login.aspx");

                if (!Page.IsPostBack)
                {
                    if (!vSecurity.ObtenerPermiso(Session["USUARIO"].ToString(), 2).Consulta)
                        Response.Redirect("/default.aspx");

                    DataTable vDatos = (DataTable)Session["AUTHCLASS"];
                    LitUsuario.Text = vDatos.Rows[0]["nombre"].ToString().ToUpper() + " " + vDatos.Rows[0]["apellidos"].ToString().ToUpper();

                    String vString = "", vPointer = "";
                    String vQuery = "[STEISP_Mensajes] 3,'" + Session["USUARIO"].ToString() + "'";
                    vDatos = vConexion.obtenerDataTable(vQuery);

                    for (int i = 0; i < vDatos.Rows.Count; i++)
                    {
                        vPointer = "<span class='heartbit'></span><span class='point'></span>";

                        String vColor = "", vLogo = "";
                        if (vDatos.Rows[i]["idAplicacion"].ToString() == "1")
                        {
                            vColor = "primary";
                            vLogo = "ti ti-shopping-cart";
                        }
                        else if (vDatos.Rows[i]["idAplicacion"].ToString() == "2")
                        {
                            vColor = "success";
                            vLogo = "ti ti-home";
                        }
                        else if (vDatos.Rows[i]["idAplicacion"].ToString() == "3")
                        {
                            vColor = "info";
                            vLogo = "ti ti-desktop";
                        }
                        else if (vDatos.Rows[i]["idAplicacion"].ToString() == "4")
                        {
                            vColor = "danger";
                            vLogo = "ti ti-plug";
                        }

                        vString += "<a href = 'javascript:void(0)'>" +
                                    "<div class='btn btn-" + vColor + " btn-circle'><i class='" + vLogo + "'></i></div>" +
                                    "<div class='mail-contnet'>" +
                                    "<h5>" + vDatos.Rows[i]["asunto"].ToString() + "</h5>" +
                                    "<span class='mail-desc'>" + vDatos.Rows[i]["mensaje"].ToString() +
                                    "</span> <span class='time'>" + vDatos.Rows[i]["nombre"].ToString() + "</span>" +
                                    "</div>" +
                                    "</a>";
                    }
                    LitNotificaciones.Text = vString;
                    LitPointer.Text = vPointer;

                    string vUsuario = Session["USUARIO"].ToString();
                    LIConfig.Visible = false;
                    LICreaNotif.Visible = false;
                    LIAprobarNotif.Visible = false;
                    LICreaVerif.Visible = false;
                    LIApruebaVerif.Visible = false;
                    LIDevolverVerif.Visible = false;
                    LIReprogramar.Visible = false;
                    LIPermisos.Visible = false;


                    DataTable vDatosMain = new DataTable();
                    String vQueryMain = "[STEISP_Agencia_Permisos] 3,'" + vUsuario + "'";
                    vDatosMain = vConexion.obtenerDataTable(vQueryMain);
                    foreach (DataRow item in vDatosMain.Rows)
                    {
                        if (item["permisos"].ToString() == "True")
                        {
                            LIPermisos.Visible = true;
                            LICancelar.Visible = true;
                            LICambiarFecha.Visible = true;
                        }
                        if (item["Agencia"].ToString() == "True")
                        {
                            LIConfig.Visible = true;
                        }
                        if (item["crearNotif"].ToString() == "True")
                        {
                            LIAprobarNotif.Visible = true;
                            LICreaNotif.Visible = true;
                            //LIModAprobar.Visible = true;
                            //LICorrectivoNotif.Visible = true;
                        }
                        //if (item["aprobarNotif"].ToString() == "True")
                        //{
                        //    LINotifATM.Visible = true;
                        //    LIModAprobar.Visible = true;
                        //}
                        if (item["crearVerif"].ToString() == "True")
                        {
                            LICreaVerif.Visible = true;
                            LIDevolverVerif.Visible = true;
                            //LICorrectivoVerifCrea.Visible = true;
                            //LICorrectivoVerifDevolver.Visible = true;
                            //LIDevoluciones.Visible = true;
                        }
                        //if (item["crearVerif"].ToString() == "True")
                        //{
                        //    LIVerifATM.Visible = true;
                        //    LIVerCrear.Visible = true;
                        //    LIDevoluciones.Visible = true;
                        //}
                        if (item["aprobarVerif"].ToString() == "True")
                        {
                            LICreaVerif.Visible = true;
                            LIApruebaVerif.Visible = true;
                            //LICorrectivoVerifAprobar.Visible = true;
                        }
                        if (item["reprogramar"].ToString() == "True")
                        {
                            LIReprogramar.Visible = true;
                        }
                        if (item["calendario"].ToString() == "True")
                        {
                            LICalendario.Visible = true;
                        }
                        if (item["avances"].ToString() == "True")
                        {
                            LIAvances.Visible = true;
                            //LIAvancesCorrectivo.Visible = true;
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                String vError = ex.Message;
            }
        }
    }
}