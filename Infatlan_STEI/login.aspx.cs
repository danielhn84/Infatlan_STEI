using Infatlan_STEI.classes;
using System;
using System.Data;
using System.Web.UI;

namespace Infatlan_STEI
{
    public partial class login : System.Web.UI.Page
    {
        db vConexion = new db();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

            }
        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                generales vGenerales = new generales();
                LdapService vLdap = new LdapService();
                //Boolean vLogin = vLdap.ValidateCredentials("ADBancat.hn", TxUsername.Text, TxPassword.Text);
                Boolean vLogin = true;

                if (vLogin)
                {
                    DataTable vDatos = new DataTable();
                    String vQuery = "STEISP_Login '" + TxUsername.Text + "','" + vGenerales.MD5Hash(TxPassword.Text) + "'";
                    vDatos = vConexion.obtenerDataTable(vQuery);

                    if (vDatos.Rows.Count < 1)
                    {
                        Session["AUTH"] = false;
                        throw new Exception("Usuario o contraseña incorrecta.");
                    }

                    foreach (DataRow item in vDatos.Rows)
                    {
                        Session["AUTHCLASS"] = vDatos;
                        Session["USUARIO"] = item["idUsuario"].ToString();
                        Session["AUTH"] = true;

                        Response.Redirect("/default.aspx");
                    }
                }
                else
                {
                    Session["AUTH"] = false;
                    throw new Exception("Usuario o contraseña incorrecta.");
                }
            }
            catch (Exception Ex)
            {
                LbMensaje.Text = "Usuario o contraseña incorrecta.";
                String vErrorLog = Ex.Message;
            }
        }
    }
}