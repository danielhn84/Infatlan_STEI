using Infatlan_STEI.classes;
using System;
using System.Data;
using System.Web.UI;
using System.Web.Helpers;


namespace Infatlan_STEI
{
    public partial class login : System.Web.UI.Page
    {
        db vConexion = new db();

       
        protected void Page_Load(object sender, EventArgs e)
        {
            string CSRF_Token = System.Guid.NewGuid().ToString();
            string page_name = System.IO.Path.GetFileName(System.
            Web.HttpContext.Current.Request.Url.AbsolutePath);
            string page_token = page_name + "_ID";
            Session[page_token] = CSRF_Token;
            HiddenField1.Value = CSRF_Token;
        }
        protected void BtnLogin_Click(object sender, EventArgs e){
            try{

             
                    string Page_Token = System.IO.Path.GetFileName(System.Web.HttpContext.Current.Request.Url.AbsolutePath) + "_ID";

                    if (HiddenField1.Value.ToString() != Session[Page_Token].ToString())
                    {
                        Session.Abandon();
                        Session.Clear();
                        Response.Redirect("/login.aspx");
                }
                else{

                    generales vGenerales = new generales();
                    LdapService vLdap = new LdapService();
                    Boolean vLogin = vLdap.ValidateCredentials("ADBancat.hn", TxUsername.Text, TxPassword.Text);
                    //Boolean vLogin = true;

                    if (vLogin){
                        DataTable vDatos = new DataTable();
                        String vQuery = "STEISP_Login 1,'" + TxUsername.Text + "','" + vGenerales.MD5Hash(TxPassword.Text) + "'";
                        vDatos = vConexion.obtenerDataTable(vQuery);

                        if (vDatos.Rows.Count < 1){
                            Session["AUTH"] = false;
                            throw new Exception("Usuario o contraseña incorrecta.");
                        }

                        foreach (DataRow item in vDatos.Rows){
                            Session["AUTHCLASS"] = vDatos;
                            Session["USUARIO"] = item["idUsuario"].ToString();
                            Session["AUTH"] = true;

                            Response.Redirect("/default.aspx");
                        }
                    }else{
                        Session["AUTH"] = false;
                        throw new Exception("Usuario o contraseña incorrecta.");
                    }
                //}
 
            }catch (Exception Ex){
                LbMensaje.Text = "Usuario o contraseña incorrecta.";
                String vErrorLog = Ex.Message;
            }
        }
    }
}