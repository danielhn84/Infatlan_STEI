using Infatlan_STEI_ATM.clases;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Infatlan_STEI_ATM.pages.permisos
{
    public partial class permisos : System.Web.UI.Page
    {
        bd vConexion = new bd();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Convert.ToBoolean(Session["AUTH"]))
                {
                    cargarDatos();
                }
                else
                {
                    Response.Redirect("/login.aspx");
                }
            }
        }
        private void cargarDatos()
        {
            try
            {
                String vQuery = "[STEISP_INVENTARIO_Generales] 13";
                DataTable vDatos = vConexion.ObtenerTabla(vQuery);

                if (vDatos.Rows.Count > 0)
                {
                    DDLUsuarios.Items.Clear();
                    DDLUsuarios.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLUsuarios.Items.Add(new ListItem { Value = item["idUsuario"].ToString(), Text = item["nombre"].ToString() + " " + item["apellidos"].ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }
        private void validarDatos()
        {
            if (DDLUsuarios.SelectedValue == "0")
                throw new Exception("Favor seleccione el usuario");
        }
        void limpiar()
        {
            CBPermisos.Checked = false;
            CBMantenimiento.Checked = false;
            //CBAprovarNotif.Checked = false;
            CBCreaNotif.Checked = false;
            CBAprobarVerif.Checked = false;
            CBCreaVerif.Checked = false;
            CBReprogramar.Checked = false;
            CBCalendario.Checked = false;
            CBAvance.Checked = false;
        }
        protected void DDLUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string vUsuario = "";
                if (DDLUsuarios.SelectedValue == "0")
                {
                    limpiar();
                    DIVTable.Visible = false;
                    BtnAceptar.Visible = false;
                }
                else
                {
                    limpiar();
                    DIVTable.Visible = true;
                    BtnAceptar.Visible = true;


                    DataTable vDatos = new DataTable();
                    String vQuery = "[STEISP_ATM_Generales] 46,'" + DDLUsuarios.SelectedValue + "'";
                    vDatos = vConexion.ObtenerTabla(vQuery);
                    foreach (DataRow item in vDatos.Rows)
                    {
                        vUsuario = item["idUsuario"].ToString();
                        CBPermisos.Checked = Convert.ToBoolean(item["permisos"].ToString());
                        CBMantenimiento.Checked = Convert.ToBoolean(item["ATM"].ToString());
                        CBCreaNotif.Checked = Convert.ToBoolean(item["crearNotif"].ToString());
                        //CBAprovarNotif.Checked = Convert.ToBoolean(item["aprobarNotif"].ToString());
                        CBCreaVerif.Checked = Convert.ToBoolean(item["crearVerif"].ToString());
                        CBAprobarVerif.Checked = Convert.ToBoolean(item["aprobarVerif"].ToString());
                        CBReprogramar.Checked = Convert.ToBoolean(item["reprogramar"].ToString());
                        CBCalendario.Checked = Convert.ToBoolean(item["calendario"].ToString());
                        CBAvance.Checked = Convert.ToBoolean(item["avance"].ToString());
                    }

                }



            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void BtnAceptar_Click(object sender, EventArgs e)
        {
            if (DDLUsuarios.SelectedValue == "0")
            {
                Mensaje("Seleccione un usuario", WarningType.Success);
            }
            else
            {
                String vUsuario = "";
                DataTable vDatos2 = new DataTable();
                String vQuery2 = "[STEISP_ATM_Generales] 46,'" + DDLUsuarios.SelectedValue + "'";
                vDatos2 = vConexion.ObtenerTabla(vQuery2);
                foreach (DataRow item in vDatos2.Rows)
                {
                    vUsuario = item["idUsuario"].ToString();
                }

                if (vUsuario == "")
                {
                    string vQuery = "[STEISP_ATM_SELECCIONES] 4, '" + DDLUsuarios.SelectedValue + "','" + Session["USUARIO"] + "', '" + CBPermisos.Checked + "'," +
                     "'" + CBMantenimiento.Checked + "','" + CBCreaNotif.Checked + "', " + CBCreaVerif.Checked + ",'" + CBAprobarVerif.Checked + "'," +
                     "'" + CBReprogramar.Checked + "','" + CBCalendario.Checked + "','" + CBAvance.Checked + "'";
                    Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    if (vInfo == 1)
                    {
                        Mensaje("Permiso creado con éxito", WarningType.Success);
                        limpiar();
                        TBLPermisos.Visible = false;
                        BtnAceptar.Visible = false;
                        DDLUsuarios.SelectedValue = "0";
                    }
                }
                else
                {
                    string vQuery = "[STEISP_ATM_SELECCIONES] 5, '" + DDLUsuarios.SelectedValue + "', '" + CBPermisos.Checked + "'," +
                                        "'" + CBMantenimiento.Checked + "','" + CBCreaNotif.Checked + "', " + CBCreaVerif.Checked + ",'" + CBAprobarVerif.Checked + "'," +
                                        "'" + CBReprogramar.Checked + "','" + CBCalendario.Checked + "','" + CBAvance.Checked + "','" + Session["USUARIO"] + "'";
                    Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    if (vInfo == 1)
                    {
                        Mensaje("Permiso modificado con éxito", WarningType.Success);
                        limpiar();
                        TBLPermisos.Visible = false;
                        BtnAceptar.Visible = false;
                        DDLUsuarios.SelectedValue = "0";
                    }
                }
            }
        }
    }
}