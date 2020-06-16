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

                    String vQuery = "[STEISP_Permisos] 3,'" + Session["USUARIO"].ToString() + "'";
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