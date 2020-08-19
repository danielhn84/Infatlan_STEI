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
                String vUser = Session["USUARIO"].ToString();
                String vQuery = "[STEISP_Login] 2,'" + vUser + "'";
                int vInfo = vConexion.ejecutarSql(vQuery);
                Session.RemoveAll();
                if (vInfo == 1){
                    Response.Redirect("/login.aspx");
                }
            }catch (Exception ex){

            }
        }
    }
}