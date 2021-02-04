using Infatlan_STEI_ATM.clases;
using System;
using System.Data;
using System.Web.UI;

namespace Infatlan_STEI
{
    public partial class main : System.Web.UI.MasterPage
    {
        bd vConexion = new bd();
        Security vSecurity = new Security();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Convert.ToBoolean(Session["AUTH"]))
                    Response.Redirect("/login.aspx");

                DataTable vDatosP = (DataTable)Session["AUTHCLASS"];
                string ver = Session["USUARIO"].ToString();

                if (!Page.IsPostBack)
                {
                    if (!vSecurity.ObtenerPermiso(Session["USUARIO"].ToString(), 3).Consulta)
                        Response.Redirect("/default.aspx");
                    if (vSecurity.ObtenerPermiso(Session["USUARIO"].ToString(), 3).Creacion)
                    {
                        //LIAgregar.Visible = true;
                        LIModCrear.Visible = true;
                        LIVerCrear.Visible = true;
                        LIMatSolicitar.Visible = true;
                        LIDevoluciones.Visible = true;
                    }
                    if (vSecurity.ObtenerPermiso(Session["USUARIO"].ToString(), 3).Edicion)
                    {
                        LIModAprobar.Visible = true;
                        LIVerAprobar.Visible = true;
                        LIMatAprobar.Visible = true;
                        LIReprogramar.Visible = true;
                        LICalendario.Visible = true;
                    }

                    DataTable vDatos = (DataTable)Session["AUTHCLASS"];
                    LitUsuario.Text = vDatos.Rows[0]["nombre"].ToString().ToUpper() + " " + vDatos.Rows[0]["apellidos"].ToString().ToUpper();

                    String vString = "", vPointer = "";
                    String vQuery = "[STEISP_Mensajes] 3,'" + Session["USUARIO"].ToString() + "'";
                    vDatos = vConexion.ObtenerTabla(vQuery);

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

                    LIPermisos.Visible = false;
                    LIMenuATM.Visible = false;
                    LINotifATM.Visible = false;
                    LIModCrear.Visible = false;
                    LINotifATM.Visible = false;
                    LIModAprobar.Visible = false;
                    LIVerifATM.Visible = false;
                    LIVerCrear.Visible = false;
                    LIVerifATM.Visible = false;
                    LIVerCrear.Visible = false;
                    LIDevoluciones.Visible = false;
                    LIVerifATM.Visible = false;
                    LIVerAprobar.Visible = false;
                    LIReprogramar.Visible = false;
                    LICalendario.Visible = false;
                    LIAvances.Visible = false;
                    LICancelar.Visible = false;
                    LICambiarFecha.Visible = false;

                    LIAvancesCorrectivo.Visible = false;
                    LICorrectivoNotif.Visible = false;
                    LICorrectivoVerifCrea.Visible = false;
                    LICorrectivoVerifAprobar.Visible = false;
                    LICorrectivoVerifDevolver.Visible = false;




                    DataTable vDatosMain = new DataTable();
                    String vQueryMain = "[STEISP_ATM_Generales] 46,'" + vUsuario + "'";
                    vDatosMain = vConexion.ObtenerTabla(vQueryMain);
                    foreach (DataRow item in vDatosMain.Rows)
                    {
                        if (item["permisos"].ToString() == "True")
                        {
                            LIPermisos.Visible = true;
                            LICancelar.Visible = true;
                            LICambiarFecha.Visible = true;
                        }
                        if (item["ATM"].ToString() == "True")
                        {
                            LIMenuATM.Visible = true;
                        }
                        if (item["crearNotif"].ToString() == "True")
                        {
                            LINotifATM.Visible = true;
                            LIModCrear.Visible = true;
                            LIModAprobar.Visible = true;
                            LICorrectivoNotif.Visible = true;
                        }
                        //if (item["aprobarNotif"].ToString() == "True")
                        //{
                        //    LINotifATM.Visible = true;
                        //    LIModAprobar.Visible = true;
                        //}
                        if (item["crearVerif"].ToString() == "True")
                        {
                            LIVerifATM.Visible = true;
                            LIVerCrear.Visible = true;
                            LICorrectivoVerifCrea.Visible = true;
                            LICorrectivoVerifDevolver.Visible = true;
                            LIDevoluciones.Visible = true;
                        }
                        //if (item["crearVerif"].ToString() == "True")
                        //{
                        //    LIVerifATM.Visible = true;
                        //    LIVerCrear.Visible = true;
                        //    LIDevoluciones.Visible = true;
                        //}
                        if (item["aprobarVerif"].ToString() == "True")
                        {
                            LIVerifATM.Visible = true;
                            LIVerAprobar.Visible = true;
                            LICorrectivoVerifAprobar.Visible = true;
                        }
                        if (item["reprogramar"].ToString() == "True")
                        {
                            LIReprogramar.Visible = true;
                        }
                        if (item["calendario"].ToString() == "True")
                        {
                            LICalendario.Visible = true;
                        }
                        if (item["avance"].ToString() == "True")
                        {
                            LIAvances.Visible = true;
                            LIAvancesCorrectivo.Visible = true;
                        }

                    }

                    //if (vUsuario== "jmembreno")
                    //{
                    //    LINotifATM.Visible = true;
                    //    LIVerAprobar.Visible = false;
                    //}

                }
            }
            catch (Exception ex)
            {
                String vError = ex.Message;
            }
        }


    }
}