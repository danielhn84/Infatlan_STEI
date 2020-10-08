using System;
using System.Data;
using Infatlan_STEI.classes;

namespace Infatlan_STEI
{
    public partial class logout : System.Web.UI.Page
    {
        db vConexion = new db();
        protected void Page_Load(object sender, EventArgs e){
            try{
                //vConexion.ejecutarSql("[STEISP_Login] 2,'" + Session["USUARIO"].ToString() + "'");
                Session.RemoveAll();
                Response.Redirect("/login.aspx");
            }catch (Exception ex){

            }
        }
    }
}