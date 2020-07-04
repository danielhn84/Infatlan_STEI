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
        protected void Page_Load(object sender, EventArgs e){
            try{
                if (!Convert.ToBoolean(Session["AUTH"]))
                    Response.Redirect("/login.aspx");
                
                if (!Page.IsPostBack){
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

                }
            }catch (Exception ex){
                String vError = ex.Message;
            }
        }
    }
}