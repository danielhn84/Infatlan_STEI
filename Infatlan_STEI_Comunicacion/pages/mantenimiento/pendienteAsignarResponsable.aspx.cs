using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infatlan_STEI_Comunicacion.classes;
using System.Data;


namespace Infatlan_STEI_Comunicacion.pages.mantenimiento
{
    public partial class pendienteAsignarResponsable : System.Web.UI.Page
    {
        db vConexion = new db();
        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            Session["USUARIO"] = "acamador";
            if (!Page.IsPostBack)
            {
                //if (Convert.ToBoolean(Session["AUTH"]))
                //{
                    cargarDatos();
                UpdatePanel.Update();
                //}
                //else
                //{
                //    Response.Redirect("/login.aspx");
                //}
            }
        }
        private void cargarDatos()
        {
            try
            {
                String vQuery = "STEISP_COMUNICACION_AsignarResponsable 1";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                GVAsignacion.DataSource = vDatos;
                GVAsignacion.DataBind();
                Session["COMUNICACION_ASIGNACION_INGENIERO"] = vDatos;
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
        protected void GVAsignacion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var vDropDown = e.Row.Cells[8].FindControl("DDLResponsable") as DropDownList;
                if (vDropDown != null)
                {
                    String vQuery = "STEISP_COMUNICACION_AsignarResponsable 2";
                    DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                    vDropDown.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        vDropDown.Items.Add(new ListItem { Value = item["idUsuario"].ToString(), Text = item["nombre"].ToString() });
                    }
                    if (HttpContext.Current.Session["COMUNICACION_PAGINACION"] != null)
                    {
                        DataTable vData = (DataTable)Session["COMUNICACION_ASIGNACION_INGENIERO"];
                        for (int i = 0; i < vData.Rows.Count; i++)
                        {
                            DataRowView drv = e.Row.DataItem as DataRowView;
                            String vCelda = drv["idMantenimiento"].ToString();

                            if (vData.Rows[i]["idMantenimiento"].ToString() == drv["idMantenimiento"].ToString())
                            {
                                vDropDown.SelectedValue = vData.Rows[i]["responsable"].ToString();
                            }
                        }
                    }
                    
                }
            }
        }
        protected void GVAsignacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                DataTable vData = (DataTable)Session["COMUNICACION_ASIGNACION_INGENIERO"];
                int vCont = GVAsignacion.PageIndex != 0 ? GVAsignacion.PageIndex * 10 : 0;

                foreach (GridViewRow row in GVAsignacion.Rows)
                {
                    DropDownList DDLAsignado = (DropDownList)row.Cells[8].FindControl("DDLResponsable");
                    vData.Rows[vCont]["responsable"] = DDLAsignado.SelectedValue;
                    vCont++;
                }
                Session["COMUNICACION_PAGINACION"] = true;

                GVAsignacion.PageIndex = e.NewPageIndex;
                GVAsignacion.DataSource = vData;
                Session["COMUNICACION_ASIGNACION_INGENIERO"] = vData;
                GVAsignacion.DataBind();


            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        private void validacion()
        {
            int vacios = 0;
            foreach (GridViewRow row in GVAsignacion.Rows)
            {       
                DropDownList DDLResponsableAsignado = (DropDownList)row.Cells[8].FindControl("DDLResponsable");
               if (DDLResponsableAsignado.Text=="0")
                {
                    vacios = vacios + 1;
                }                             
            }
       

            if (vacios>0)
                throw new Exception("Falta que asigne ingeniero responsable para los mantenimientos programados.");
        }

        protected void BtnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                validacion();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModalConfirmar();", true);
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void BtnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                int vCont = GVAsignacion.PageIndex != 0 ? GVAsignacion.PageIndex * 10 : 0;
                DataTable vData = (DataTable)Session["COMUNICACION_ASIGNACION_INGENIERO"];
                int vCantidad = Convert.ToInt32 (this.GVAsignacion.Rows.Count.ToString());
                int vAcumulador = 0;

                foreach (GridViewRow row in GVAsignacion.Rows)
                {
                    DropDownList DDLAsignado = (DropDownList)row.Cells[8].FindControl("DDLResponsable");
                    string vResponsable= DDLAsignado.SelectedValue;
                    string vIdMantenimiento = row.Cells[0].Text;

                    String vQuery = "STEISP_COMUNICACION_AsignarResponsable 3," + vIdMantenimiento + ",'" + vResponsable + "','" + Session["USUARIO"] + "'";
                    Int32 vInformacion1 = vConexion.ejecutarSql(vQuery);
                    if (vInformacion1==1)
                    {
                        vAcumulador = vAcumulador + 1;
                        vCont++;
                    }
                                                         
                }
                if(vAcumulador== vCantidad)
                {
                    Mensaje("Se guardaron exitosamente los registros.", WarningType.Success);
                }
                else
                {
                    Mensaje("Favor contactarse con el administrador, hubo un problema al guardar los registros.", WarningType.Danger);
                }


                cargarDatos();
                UpdatePanel.Update();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModalConfirmar();", true);

            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
    }
}