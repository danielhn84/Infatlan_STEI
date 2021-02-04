using Infatlan_STEI_CableadoEstructurado.clases;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Infatlan_STEI_CableadoEstructurado
{
    public partial class _default : System.Web.UI.Page
    {
        db vConexion = new db();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                String vUser = Request.QueryString["u"];
                String vQuery = "[STEISP_Login] 3, '" + vUser + "'";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                if (vDatos.Rows.Count > 0)
                {
                    if (vDatos.Rows[0]["auth"].ToString() != "1")
                    {
                        Response.Redirect("/login.aspx");
                    }
                    else
                    {
                        Session["AUTHCLASS"] = vDatos;
                        Session["USUARIO"] = vDatos.Rows[0]["idUsuario"].ToString();
                        Session["AUTH"] = true;

                        String vQueryUsuario = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 28 ,'" + Session["USUARIO"] + "'";
                        DataTable vDatosUsuario = vConexion.obtenerDataTable(vQueryUsuario);

                        int vUsuario = 1;

                        if (vUsuario == 1)
                        {
                            CargarDatos();

                            vQuery = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 28 ,'" + Session["USUARIO"].ToString() + "'";
                            vDatos = vConexion.obtenerDataTable(vQuery);

                            LbDescripcionDashb.Text = "Detalle de los estudios pendientes de modificar.";
                            txtCreadas.Text = "Estudio Creados";
                            txtPendientes.Text = "Estudios Pendientes de Edición";
                            lbCreadas.Text = vDatos.Rows[0]["creados"].ToString();
                            lbPendientes.Text = vDatos.Rows[0]["edicion"].ToString();
                            LbFechaDashboard.Text = DateTime.Now.ToString("dd-MM-yyyy");
                        }

                        if (vUsuario == 2)
                        {
                            CargarDatos();

                            vQuery = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 29 ,'" + Session["USUARIO"].ToString() + "'";
                            vDatos = vConexion.obtenerDataTable(vQuery);

                            txtCreadas.Text = "Estudio Revisados";
                            txtPendientes.Text = "Revisiones Pendientes";
                            lbCreadas.Text = vDatos.Rows[0]["revisados"].ToString();
                            lbPendientes.Text = vDatos.Rows[0]["revisionpendiente"].ToString();
                            LbFechaDashboard.Text = DateTime.Now.ToString("dd-MM-yyyy");
                        }

                        if (vUsuario == 3)
                        {
                            CargarDatos();

                            vQuery = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 27 ,'" + Session["USUARIO"].ToString() + "'";
                            vDatos = vConexion.obtenerDataTable(vQuery);

                            txtCreadas.Text = "Cotizaciones Realizadas";
                            txtPendientes.Text = "Cotizaciones Pendientes";
                            lbCreadas.Text = vDatos.Rows[0]["realizados"].ToString(); ;
                            lbPendientes.Text = vDatos.Rows[0]["pendientes"].ToString();
                            LbFechaDashboard.Text = DateTime.Now.ToString("dd-MM-yyyy");
                        }
                    }
                }
                else
                {
                    Response.Redirect("/login.aspx");
                }
            }
        }

        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        void CargarDatos()
        {
            try
            {
                int vUsuario = 1;

                if (vUsuario == 1)
                {
                    DataTable vDatos = new DataTable();
                    vDatos = vConexion.obtenerDataTable("STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 25");




                    GVPrincipal.DataSource = vDatos;
                    GVPrincipal.DataBind();
                    Session["CE_BUSCAQUEDAESTUDIO"] = vDatos;
                    udpGVDashboard.Update();

                    if (vDatos.Rows.Count == 0)
                    {
                        LbTituloDashb.Visible = false;
                        LbDescripcionDashb.Text = "No hay estudios pendientes";
                    }

                }

                if (vUsuario == 2)
                {
                    DataTable vDatos = new DataTable();
                    vDatos = vConexion.obtenerDataTable("STEISP_CABLESTRUCTURADO_Aprobacion 4");

                    GVPrincipal.DataSource = vDatos;
                    GVPrincipal.DataBind();
                    Session["CE_BUSCAQUEDAESTUDIO"] = vDatos;
                    udpGVDashboard.Update();

                    if (vDatos.Rows.Count == 0)
                    {
                        LbTituloDashb.Visible = false;
                        LbDescripcionDashb.Text = "No hay estudios pendientes";
                    }

                }

                if (vUsuario == 3)
                {

                    DataTable vDatos = new DataTable();
                    vDatos = vConexion.obtenerDataTable("STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 15 ");

                    GVPrincipal.DataSource = vDatos;
                    GVPrincipal.DataBind();
                    Session["CE_BUSCAQUEDAESTUDIO"] = vDatos;
                    udpGVDashboard.Update();

                    if (vDatos.Rows.Count == 0)
                    {
                        LbTituloDashb.Visible = false;
                        LbDescripcionDashb.Text = "No hay estudios pendientes";
                    }

                }

            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        protected void GVPrincipal_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            try
            {
                GVPrincipal.DataSource = (DataTable)Session["CE_BUSCAQUEDAESTUDIO"];
                GVPrincipal.PageIndex = e.NewPageIndex;
                GVPrincipal.DataBind();
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }

        }

        protected void GVPrincipal_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);

            }
        }

        protected void GVPrincipal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int vUsuario = 1;

            if (vUsuario == 1)
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    e.Row.Cells[3].Text = "Observación";

                }
            }

        }
    }
}