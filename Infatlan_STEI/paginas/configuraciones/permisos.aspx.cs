﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Infatlan_STEI.classes;

namespace Infatlan_STEI.paginas.configuraciones
{
    public partial class permisos : System.Web.UI.Page
    {
        db vConexion = new db();
        protected void Page_Load(object sender, EventArgs e){
            if (!Page.IsPostBack){
                if (Convert.ToBoolean(Session["AUTH"])){
                    cargarDatos();
                }
            }
        }

        private void cargarDatos() {
            try{
                String vQuery = "[STEISP_INVENTARIO_Generales] 14";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0){
                    Session["STEI_PERMISOS_GRID"] = null;
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                    Session["STEI_PERMISOS"] = vDatos;
                }

                vQuery = "[STEISP_INVENTARIO_Generales] 13";
                vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0){
                    DDLUsuarios.Items.Clear();
                    DDLUsuarios.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    foreach (DataRow item in vDatos.Rows){
                        DDLUsuarios.Items.Add(new ListItem { Value = item["idUsuario"].ToString(), Text = item["nombre"].ToString() + " " + item["apellidos"].ToString() });
                    }
                }
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        public void Mensaje(string vMensaje, WarningType type){
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        public void MensajeBlock(string vMensaje, WarningType type){
            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        protected void BtnAceptar_Click(object sender, EventArgs e) {
            try{
                validarDatos();
                DataTable vDatos = (DataTable)Session["STEI_PERMISOS"];
                String vQuery = "[STEISP_Permisos] 3,'" + DDLUsuarios.SelectedValue + "'";
                DataTable vData = vConexion.obtenerDataTable(vQuery);
                int vInfo = 0;

                if (vData.Rows.Count < 1){
                    int vCuenta = 0;

                    foreach (GridViewRow row in GVBusqueda.Rows){
                        CheckBox CBConsulta = row.Cells[2].FindControl("CBxConsulta") as CheckBox;
                        CheckBox CBCrear = row.Cells[2].FindControl("CBxCrear") as CheckBox;
                        CheckBox CBEditar = row.Cells[2].FindControl("CBxEditar") as CheckBox;
                        CheckBox CBBorrar = row.Cells[2].FindControl("CBxBorrar") as CheckBox;

                        vQuery = "[STEISP_Permisos] 1,'" + DDLUsuarios.SelectedValue + "'" +
                        "," + vDatos.Rows[row.RowIndex]["idAplicacion"].ToString() +
                        ",'" + Session["USUARIO"].ToString() + "'" +
                        "," + CBConsulta.Checked +
                        "," + CBCrear.Checked +
                        "," + CBEditar.Checked +
                        "," + CBBorrar.Checked;
                        vInfo = vConexion.ejecutarSql(vQuery);
                        vCuenta++;
                    }
                    if (vCuenta == vDatos.Rows.Count){
                        cargarDatos();
                        Mensaje("Permisos ingresados con éxito.", WarningType.Success);
                    }
                }else{ 
                    int vCuenta = 0;

                    foreach (GridViewRow row in GVBusqueda.Rows){
                        CheckBox CBConsulta = row.Cells[2].FindControl("CBxConsulta") as CheckBox;
                        CheckBox CBCrear = row.Cells[2].FindControl("CBxCrear") as CheckBox;
                        CheckBox CBEditar = row.Cells[2].FindControl("CBxEditar") as CheckBox;
                        CheckBox CBBorrar = row.Cells[2].FindControl("CBxBorrar") as CheckBox;

                        vQuery = "[STEISP_Permisos] 2,'" + DDLUsuarios.SelectedValue + "'" +
                        "," + vDatos.Rows[row.RowIndex]["idAplicacion"].ToString() +
                        ",'" + CBConsulta.Checked + "'" +
                        ",'" + CBCrear.Checked + "'" +
                        ",'" + CBEditar.Checked + "'" +
                        ",'" + CBBorrar.Checked + "'" +
                        ",'" + Session["USUARIO"].ToString() + "'";
                        vInfo = vConexion.ejecutarSql(vQuery);
                        vCuenta++;
                    }
                    if (vCuenta == vDatos.Rows.Count){
                        cargarDatos();
                        Mensaje("Permisos actualizados con éxito.", WarningType.Success);
                    }
                }
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        private void validarDatos(){
            if (DDLUsuarios.SelectedValue == "0")
                throw new Exception("Favor seleccione el usuario");
        }

        protected void DDLUsuarios_SelectedIndexChanged(object sender, EventArgs e){
            try{
                String vQuery = DDLUsuarios.SelectedValue == "0" ? "[STEISP_INVENTARIO_Generales] 14" : "[STEISP_Permisos] 3,'" + DDLUsuarios.SelectedValue + "'";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0){
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                    Session["STEI_PERMISOS"] = vDatos;
                }else {
                    vQuery = "[STEISP_INVENTARIO_Generales] 14";
                    vDatos = vConexion.obtenerDataTable(vQuery);

                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                    Session["STEI_PERMISOS"] = vDatos;
                }
            }catch (Exception ex){
                
            }
        }
    }
}