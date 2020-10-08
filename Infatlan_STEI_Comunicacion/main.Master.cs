using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Infatlan_STEI_Comunicacion.classes;
using System.Web.Security;



namespace Infatlan_STEI_Comunicacion
{
    public partial class main : System.Web.UI.MasterPage
    {
        db vConexion = new db();
        Security vSecurity = new Security();

       

        //private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        //private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        //private string _antiXsrfTokenValue;

        //protected void Page_Init(object sender, EventArgs e)
        //{
        //    // The code below helps to protect against XSRF attacks
        //    var requestCookie = Request.Cookies[AntiXsrfTokenKey];
        //    Guid requestCookieGuidValue;
        //    if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
        //    {
        //        // Use the Anti-XSRF token from the cookie
        //        _antiXsrfTokenValue = requestCookie.Value;
        //        Page.ViewStateUserKey = _antiXsrfTokenValue;
        //    }
        //    else
        //    {
        //        // Generate a new Anti-XSRF token and save to the cookie
        //        _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
        //        Page.ViewStateUserKey = _antiXsrfTokenValue;

        //        var responseCookie = new HttpCookie(AntiXsrfTokenKey)
        //        {
        //            HttpOnly = true,
        //            Value = _antiXsrfTokenValue
        //        };
        //        if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
        //        {
        //            responseCookie.Secure = true;
        //        }
        //        Response.Cookies.Set(responseCookie);
        //    }

        //    Page.PreLoad += master_Page_PreLoad;
        //}

        //protected void master_Page_PreLoad(object sender, EventArgs e)
        //{
        //    if (!IsPostBack)
        //    {
        //        // Set Anti-XSRF token
        //        ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
        //        ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
        //    }
        //    else
        //    {
        //        // Validate the Anti-XSRF token
        //        if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
        //            || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
        //        {
        //            throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
        //        }
        //    }
        //}
        protected void Page_Load(object sender, EventArgs e){
            try{
                if (!Convert.ToBoolean(Session["AUTH"]))
                    Response.Redirect("/login.aspx");
        
                if (!Page.IsPostBack){
                    if (!vSecurity.ObtenerPermiso(Session["USUARIO"].ToString(), 7).Consulta)
                        Response.Redirect("/default.aspx");
                    if (vSecurity.ObtenerPermiso(Session["USUARIO"].ToString(), 7).Creacion) { 
                        LiCrear.Visible = true;
                        LiCompletar.Visible = true;
                        LiModificar.Visible = true;
                    }
                    if (vSecurity.ObtenerPermiso(Session["USUARIO"].ToString(), 7).Edicion) { 
                        LiConfig.Visible = true;
                        LiAsignar.Visible = true;
                        LiAprobar.Visible = true;
                        LiReprogramar.Visible = true;
                    }

                    DataTable vDatos = (DataTable)Session["AUTHCLASS"];
                    LitUsuario.Text = vDatos.Rows[0]["nombre"].ToString().ToUpper() + " " + vDatos.Rows[0]["apellidos"].ToString().ToUpper();

                    String vString = "", vPointer = "";
                    String vQuery = "[STEISP_Mensajes] 3,'" + Session["USUARIO"].ToString() + "'";
                    vDatos = vConexion.obtenerDataTable(vQuery);

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

