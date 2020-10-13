using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Infatlan_STEI_ATM.clases;

namespace Infatlan_STEI
{
    public partial class main : System.Web.UI.MasterPage
    {
        bd vConexion = new bd();
        Security vSecurity = new Security();
        protected void Page_Load(object sender, EventArgs e){
            try{
                if (!Convert.ToBoolean(Session["AUTH"]))
                    Response.Redirect("/login.aspx");
                
                if (!Page.IsPostBack){
                    if (!vSecurity.ObtenerPermiso(Session["USUARIO"].ToString(), 3).Consulta)
                        Response.Redirect("/default.aspx");
                    if (vSecurity.ObtenerPermiso(Session["USUARIO"].ToString(), 3).Creacion){
                        //LIAgregar.Visible = true;
                        LIModCrear.Visible = true;
                        LIVerCrear.Visible = true;
                        LIMatSolicitar.Visible = true;
                        LIDevoluciones.Visible = true;
                    }
                    if (vSecurity.ObtenerPermiso(Session["USUARIO"].ToString(), 3).Edicion){
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

                    for (int i = 0; i < vDatos.Rows.Count; i++){
                        vPointer = "<span class='heartbit'></span><span class='point'></span>";

                        String vColor = "", vLogo = "";
                        if (vDatos.Rows[i]["idAplicacion"].ToString() == "1"){
                            vColor = "primary";
                            vLogo = "ti ti-shopping-cart";
                        }else if (vDatos.Rows[i]["idAplicacion"].ToString() == "2"){
                            vColor = "success";
                            vLogo = "ti ti-home";
                        }else if (vDatos.Rows[i]["idAplicacion"].ToString() == "3"){
                            vColor = "info";
                            vLogo = "ti ti-desktop";
                        }else if (vDatos.Rows[i]["idAplicacion"].ToString() == "4"){
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
                    if (vUsuario != "eurrea" && vUsuario != "mgarcia" && vUsuario != "acedillo")
                        LIMenuATM.Visible = false;
                    if (vUsuario != "eurrea" && vUsuario != "mgarcia" && vUsuario != "acedillo" && vUsuario != "emontoya" && vUsuario != "jdgarcia" && vUsuario != "acalderon" && vUsuario != "mbriceno")
                    {
                        LINotifATM.Visible = false;
                        LIReprogramar.Visible = false;
                        LICalendario.Visible = false;
                    }
                    if (vUsuario == "mbriceno")
                    {
                        LIVerifATM.Visible = false;
                        LIDevoluciones.Visible = false;
                        LIReprogramar.Visible = false;
                        LICalendario.Visible = false;
                    }
                }
            }catch (Exception ex){
                String vError = ex.Message;
            }
        }

      
    }
}