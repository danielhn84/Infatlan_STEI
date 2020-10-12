using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using Infatlan_STEI_ATM.clases;

namespace Infatlan_STEI_ATM.pages.ATM
{
    public partial class tipo : System.Web.UI.Page
    {
        bd vConexion = new bd();
        bd vConexionATM = new bd();
        Security vSecurity = new Security();
        protected void Page_Load(object sender, EventArgs e){
            Session["TIPO_ATM"] = null;
            if (!Page.IsPostBack){
                if (Convert.ToBoolean(Session["AUTH"])){
                    if (vSecurity.ObtenerPermiso(Session["USUARIO"].ToString(), 3).Creacion)
                        btnguardartipoATM.Visible = true;

                    cargarData();
                }else {
                    Response.Redirect("/login.aspx");
                }
            }
        }

        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        void cargarData(){
            if (HttpContext.Current.Session["TIPO_ATM"] == null){
                try{
                    DataTable vDatos = new DataTable();
                    vDatos = vConexionATM.ObtenerTablaATM("SPSTEI_ATM 4");
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                    if (vSecurity.ObtenerPermiso(Session["USUARIO"].ToString(), 3).Edicion){
                        foreach (GridViewRow item in GVBusqueda.Rows){
                            LinkButton LbEdit = item.FindControl("btnbajaATM") as LinkButton;
                            LbEdit.Visible = true;
                        }
                    }
                    Session["tipoATM"] = vDatos;
                }catch (Exception Ex){

                }
                Session["TIPO_ATM"] = 1;
            }
        }

        protected void GVBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e){

        }

        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e){
            DataTable vDataa = (DataTable)Session["tipoATM"];
            string codtipoATMs = e.CommandArgument.ToString();

            txtAlerta1.Visible = false;
            txtAlerta2.Visible = false;
            if (e.CommandName == "Codigo"){
                try{
                    DataTable vDatos = new DataTable();
                    String vQuery = "SPSTEI_ATM 12,'" + codtipoATMs + "'";
                    vDatos = vConexionATM.ObtenerTablaATM(vQuery);
                    foreach (DataRow item in vDatos.Rows){
                        Session["codtipoATM"] = codtipoATMs;
                        Session["nombretipoATM"] = item["Descripcion"].ToString();                        
                    }                  
                }catch (Exception){
                    throw;
                }

                lbcodtipoATM.Text = codtipoATMs;
                lbNombretipoATM.Text = Session["nombretipoATM"].ToString();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);
            }
        }

        protected void btnModalEnviartipoATM_Click(object sender, EventArgs e){
            if (txtModalNewTipoATM.Text == "" || txtModalNewTipoATM.Text == string.Empty){
                txtAlerta1.Visible = true;
                //H5Alerta1.Visible = true;
            }else{
                try{
                    string vQuery = "SPSTEI_ATM 13, '" + Session["codtipoATM"] + "','" + txtModalNewTipoATM.Text + "'";
                    Int32 vInfo = vConexionATM.ejecutarSQLATM(vQuery);
                    if (vInfo == 1){
                        //H5Alerta2.Visible = false;
                        txtModalNewTipoATM.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
                        Mensaje("Tipo ATM modificado con éxito", WarningType.Success);
                        UpdateGridView.Update();
                        cargarData();
                    }else{
                        txtAlerta1.Text = "No se pudo modificar el tipo de ATM";
                        txtAlerta1.Visible = true;
                    }
                }catch (Exception Ex){
                    throw;
                }
            }
        }

        protected void btnModalCerrartipoATM_Click(object sender, EventArgs e){
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
        }

        protected void btnguardartipoATM_Click(object sender, EventArgs e){
            txtAlerta1.Visible = false;
            txtAlerta2.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal2();", true);
          
        }

        protected void btnModalNueviTipoATM_Click(object sender, EventArgs e){
            if (txtNewTipoATM.Text == "" || txtNewTipoATM.Text == string.Empty){
                txtAlerta2.Visible = true;
                //H5Alerta2.Visible = true;
            }else{
                try{
                    string vQuery = "SPSTEI_ATM 14, '" + txtNewTipoATM.Text + "'";
                    Int32 vInfo = vConexionATM.ejecutarSQLATM(vQuery);
                    if (vInfo == 1){
                        //H5Alerta2.Visible=false;
                        txtNewTipoATM.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal2();", true);
                        Mensaje("Tipo de ATM creada con éxito", WarningType.Success);
                        UpdateGridView.Update();
                        cargarData();
                    }else{
                        txtAlerta2.Text = "No se pudo crear el tipo de ATM";
                        txtAlerta2.Visible = true;
                    }
                }catch (Exception Ex){
                    throw;
                }
            }
        }

        protected void btnModalCerrarNueviTipoATM_Click1(object sender, EventArgs e){
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal2();", true);
        }
    }
    
}