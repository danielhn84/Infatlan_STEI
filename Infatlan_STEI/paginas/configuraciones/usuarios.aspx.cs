using Infatlan_STEI.classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace Infatlan_STEI.paginas.configuraciones
{
    public partial class usuarios : System.Web.UI.Page
    {
        db vConexion = new db();
        Security vSecurity = new Security();
        protected void Page_Load(object sender, EventArgs e){
            if (!Page.IsPostBack){
                if (Convert.ToBoolean(Session["AUTH"])){
                    if (!vSecurity.ObtenerPermiso(Session["USUARIO"].ToString(), 6).Consulta)
                        Response.Redirect("/default.aspx");
                    if (vSecurity.ObtenerPermiso(Session["USUARIO"].ToString(), 6).Creacion)
                        BtnNuevo.Visible = true;

                    cargarDatos();
                }
            }
        }

        private void cargarDatos() {
            try{
                String vQuery = "[STEISP_Usuarios] 1";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0){
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();

                    if (vSecurity.ObtenerPermiso(Session["USUARIO"].ToString(), 6).Edicion){
                        foreach (GridViewRow item in GVBusqueda.Rows){
                            LinkButton LbEdit = item.FindControl("BtnEditar") as LinkButton;
                            LbEdit.Visible = true;
                        }
                    }
                    Session["STEI_USUARIOS"] = vDatos;
                }

                vQuery = "[STEISP_INVENTARIO_Generales] 1";
                vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0){
                    DDLDepartamento.Items.Clear();
                    DDLDepartamento.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    foreach (DataRow item in vDatos.Rows){
                        DDLDepartamento.Items.Add(new ListItem { Value = item["idDepartamento"].ToString(), Text = item["nombre"].ToString() });
                    }
                }
                
                vQuery = "[STEISP_Usuarios] 5";
                vDatos = vConexion.obtenerDataTable(vQuery);
                
                if (vDatos.Rows.Count > 0){
                    DDLJefe.Items.Clear();
                    DDLJefe.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    foreach (DataRow item in vDatos.Rows){
                        DDLJefe.Items.Add(new ListItem { Value = item["idUsuario"].ToString(), Text = item["nombre"].ToString() + " " + item["apellidos"].ToString() });
                    }
                }

            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        public void Mensaje(string vMensaje, WarningType type){
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        protected void TxBusqueda_TextChanged(object sender, EventArgs e){
            try{
                cargarDatos();
                String vBusqueda = TxBusqueda.Text;
                DataTable vDatos = (DataTable)Session["STEI_USUARIOS"];
                if (vBusqueda.Equals("")){
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                }else{ 
                    EnumerableRowCollection<DataRow> filtered = vDatos.AsEnumerable()
                        .Where(r => r.Field<String>("nombre").Contains(vBusqueda.ToUpper()));

                    DataTable vDatosFiltrados = new DataTable();
                    vDatosFiltrados.Columns.Add("idUsuario");
                    vDatosFiltrados.Columns.Add("nombre");
                    vDatosFiltrados.Columns.Add("apellidos");
                    vDatosFiltrados.Columns.Add("telefono");
                    vDatosFiltrados.Columns.Add("correo");
                    vDatosFiltrados.Columns.Add("identidad");

                    foreach (DataRow item in filtered){
                        vDatosFiltrados.Rows.Add(
                            item["idMarca"].ToString(),
                            item["nombre"].ToString(),
                            item["apellidos"].ToString(),
                            item["telefono"].ToString(),
                            item["correo"].ToString(),
                            item["identidad"].ToString()
                            );
                    }

                    GVBusqueda.DataSource = vDatosFiltrados;
                    GVBusqueda.DataBind();
                    Session["STEI_USUARIOS"] = vDatosFiltrados;
                }

            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void BtnNuevo_Click(object sender, EventArgs e){
            try{
                limpiarModal();
                TxUsuario.ReadOnly = false;
                LbIdUser.Text = "Crear Nuevo Usuario";
                DivEstado.Visible = false;
                Session["STEI_USUARIO_ID"] = null;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        void limpiarModal(){
            TxNombres.Text = string.Empty;
            TxApellidos.Text = string.Empty;
            TxCorreo.Text = string.Empty;
            TxIdentidad.Text = string.Empty;
            TxUsuario.Text = string.Empty;
            TxTelefono.Text = string.Empty;
            TxSysAid.Text = string.Empty;
            DDLDepartamento.SelectedValue = "0";
            DDLGroups.SelectedValue = "0";
            DDLJefe.SelectedValue = "0";
            DDLEstado.SelectedValue = "1";

            CBxNoForaneo.Checked = false;
            CBxForaneo.Checked = false;
            DivMensaje.Visible = false;
        }

        protected void BtnAceptar_Click(object sender, EventArgs e){
            try{
                validarDatos();
                String vQuery = "", vMensaje = "";
                int vInfo;

                Boolean vForaneo = CBxForaneo.Checked ? true : false;
                DataTable vDatos = new DataTable();
                vQuery = "STEISP_Usuarios {0}" +
                        ",'" + TxUsuario.Text + "'" +
                        ",'" + TxNombres.Text.ToUpper() + "'" +
                        ",'" + TxApellidos.Text.ToUpper() + "'" +
                        ",'" + TxTelefono.Text + "'" +
                        ",'" + TxCorreo.Text + "'" +
                        ",'" + TxIdentidad.Text + "'" +
                        "," + DDLEstado.SelectedValue + 
                        ",'" + Session["USUARIO"].ToString() + "'" +
                        "," + DDLDepartamento.SelectedValue + 
                        ",'" + TxSysAid.Text + "'" +
                        ",'" + DDLJefe.SelectedValue + "'" +
                        ",'" + DDLGroups.SelectedValue + "'" +
                        ",'" + vForaneo + "'";

                if (HttpContext.Current.Session["STEI_USUARIO_ID"] == null){
                    vQuery = string.Format(vQuery, "3");
                    vInfo = vConexion.ejecutarSql(vQuery);
                    vMensaje = "Usuario registrado con éxito.";
                }else{
                    vQuery = string.Format(vQuery, "4");
                    vInfo = vConexion.ejecutarSql(vQuery);
                    vMensaje = "Usuario actualizado con éxito";
                }

                if (vInfo == 1){
                    Mensaje(vMensaje, WarningType.Success);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "cerrarModal();", true);
                    cargarDatos();
                }
            }catch (Exception ex){
                LbAdvertencia.Text = ex.Message;
                DivMensaje.Visible = true;
            }
        }

        private void validarDatos(){
            if (TxUsuario.Text == "" || TxUsuario.Text == string.Empty)
                throw new Exception("Favor ingrese el usuario.");
            if (TxNombres.Text == "" || TxNombres.Text == string.Empty)
                throw new Exception("Favor ingrese el nombre del usuario.");
            if (TxApellidos.Text == "" || TxApellidos.Text == string.Empty)
                throw new Exception("Favor ingrese los apellidos.");
            if (TxIdentidad.Text == "" || TxIdentidad.Text == string.Empty)
                throw new Exception("Favor ingrese la identidad.");
            if (TxCorreo.Text == "" || TxCorreo.Text == string.Empty)
                throw new Exception("Favor ingrese el correo.");
            if (TxTelefono.Text == "" || TxTelefono.Text == string.Empty)
                throw new Exception("Favor ingrese el teléfono.");
            if (DDLDepartamento.SelectedValue == "0")
                throw new Exception("Favor seleccione el departamento.");
            if (!CBxNoForaneo.Checked && !CBxForaneo.Checked)
                throw new Exception("Favor seleccione si es foráneo.");
        }

        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e){
            try{
                DataTable vDatos = new DataTable();
                String vQuery = "";
                string vIdUser = e.CommandArgument.ToString();

                if (e.CommandName == "EditarUser") {
                    DivMensaje.Visible = false;
                    LbIdUser.Text = "Editar usuario <b>" + vIdUser + "</b>";
                    Session["STEI_USUARIO_ID"] = vIdUser;
                    DivEstado.Visible = true;
                    TxUsuario.ReadOnly = true;
                    vQuery = "[STEISP_Usuarios] 2," + vIdUser + "";
                    vDatos = vConexion.obtenerDataTable(vQuery);

                    for (int i = 0; i < vDatos.Rows.Count; i++) {

                        Boolean vForaneo = Convert.ToBoolean(vDatos.Rows[i]["flagForaneo"]);
                        TxUsuario.Text = vDatos.Rows[i]["idUsuario"].ToString();
                        TxSysAid.Text = vDatos.Rows[i]["sysAid"].ToString();
                        TxNombres.Text = vDatos.Rows[i]["nombre"].ToString();
                        TxApellidos.Text = vDatos.Rows[i]["apellidos"].ToString();
                        TxIdentidad.Text = vDatos.Rows[i]["identidad"].ToString();
                        TxTelefono.Text = vDatos.Rows[i]["telefono"].ToString();
                        TxCorreo.Text = vDatos.Rows[i]["correo"].ToString();
                        DDLDepartamento.SelectedValue = vDatos.Rows[i]["idDepartamento"].ToString();
                        DDLJefe.SelectedValue = vDatos.Rows[i]["idJefe"].ToString();
                        DDLGroups.SelectedValue = vDatos.Rows[i]["assignedGroup"].ToString();
                        DDLEstado.SelectedValue = vDatos.Rows[i]["estado"].ToString();
                        if (vForaneo)
                            CBxForaneo.Checked = true;
                        else
                            CBxNoForaneo.Checked = true;
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void GVBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e){
            try{
                GVBusqueda.PageIndex = e.NewPageIndex;
                GVBusqueda.DataSource = (DataTable)Session["STEI_USUARIOS"];
                GVBusqueda.DataBind();
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void DDLJefe_SelectedIndexChanged(object sender, EventArgs e){
            try{
                String vQuery = "[STEISP_Usuarios] 2,'" + DDLJefe.SelectedValue + "'";
                DataTable vData = vConexion.obtenerDataTable(vQuery);
                if (vData.Rows.Count > 0){
                    DDLGroups.SelectedValue = vData.Rows[0]["assignedGroup"].ToString();
                    DDLGroups.Enabled = false;
                }
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
    }
}