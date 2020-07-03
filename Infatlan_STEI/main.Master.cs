using System;
using System.Data;
using System.Web.UI;
using Infatlan_STEI.classes;

namespace Infatlan_STEI
{
    public partial class main : System.Web.UI.MasterPage
    {
        db vConexion = new db();
        protected void Page_Load(object sender, EventArgs e){
            if (!Convert.ToBoolean(Session["AUTH"])){
                Response.Redirect("/login.aspx");
            }

            if (!Page.IsPostBack){
                String vError = "";
                try{
                    DataTable vDatos = (DataTable)Session["AUTHCLASS"];
                    LitUsuario.Text = vDatos.Rows[0]["nombre"].ToString().ToUpper() + " " + vDatos.Rows[0]["apellidos"].ToString().ToUpper();
                    TxUser.Value = vDatos.Rows[0]["idUsuario"].ToString();

                    String vString = "", vPointer = "";
                    String vQuery = "[STEISP_Mensajes] 3,'" + Session["USUARIO"].ToString() + "'";
                    vDatos = vConexion.obtenerDataTable(vQuery);

                    for (int i = 0; i < vDatos.Rows.Count; i++){
                        vPointer = "<span class='heartbit'></span><span class='point'></span>";

                        String vColor = "", vLogo = "";
                        if (vDatos.Rows[i]["idAplicacion"].ToString() == "1"){
                            vColor = "primary";
                            vLogo = "ti ti-package";
                        }else if (vDatos.Rows[i]["idAplicacion"].ToString() == "2"){
                            vColor = "success";
                            vLogo = "icon icon-home";
                        }else if (vDatos.Rows[i]["idAplicacion"].ToString() == "3"){
                            vColor = "info";
                            vLogo = "icon icon-screen-desktop";
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

                    vQuery = "[STEISP_Permisos] 3,'" + Session["USUARIO"].ToString() + "'";
                    vDatos = vConexion.obtenerDataTable(vQuery);

                    if (vDatos.Rows.Count > 0){
                        if (vDatos.Rows[0]["consulta"].ToString() == "True")
                            LIInventario.Visible = true;
                        if (vDatos.Rows[1]["consulta"].ToString() == "True")
                            LIAgencias.Visible = true;
                        if (vDatos.Rows[2]["consulta"].ToString() == "True")
                            LIATM.Visible = true;
                        if (vDatos.Rows[3]["consulta"].ToString() == "True")
                            LICableado.Visible = true;
                    }
                }catch (Exception ex){
                    vError = ex.Message;
                }
            }
        }
    }
}