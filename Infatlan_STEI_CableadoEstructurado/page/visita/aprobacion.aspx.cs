﻿using Infatlan_STEI_CableadoEstructurado.clases;
using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Infatlan_STEI_CableadoEstructurado.page.visitaTecnica
{
    public partial class aprobacion : System.Web.UI.Page
    {
        db vConexion = new db();
        Security vSecurity = new Security();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Convert.ToBoolean(Session["AUTH"]))
                {
                    CargarProceso();
                    //String vQuery = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 29 ,'" + Session["USUARIO"] + "'";
                    //DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                    //lbRevisionesPendientes.Text = vDatos.Rows[0]["revisionpendiente"].ToString();
                    //lbEstudiosRevisados.Text = vDatos.Rows[0]["revisados"].ToString();
                    // EstudiosRevisados.Style.Width = 60 %;
                }
                else
                {
                    Response.Redirect("/login.aspx");
                }
            }
        }

        void CargarProceso()
        {
            try
            {
                String vQuery = "STEISP_CABLESTRUCTURADO_Aprobacion 4 ";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                if (vDatos.Rows.Count > 0)
                {
                    GVAprobacion.DataSource = vDatos;
                    GVAprobacion.DataBind();
                    if (vSecurity.ObtenerPermiso(Session["USUARIO"].ToString(), 4).Edicion)
                    {
                        foreach (GridViewRow item in GVAprobacion.Rows)
                        {
                            LinkButton LbEdit = item.FindControl("BtnEditar") as LinkButton;
                            LbEdit.Visible = true;
                        }
                    }
                    Session["CE_DATOSAPROBACION"] = vDatos;
                    udpAprobacion.Update();
                }
                else
                {
                    LbDescripcionGV.Text = "No hay estudios pendientes";
                }
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        protected void GVAprobacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GVAprobacion.DataSource = (DataTable)Session["CE_DATOSAPROBACION"];
                GVAprobacion.PageIndex = e.NewPageIndex;
                GVAprobacion.DataBind();
                udpAprobacion.Update();
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }

        }

        protected void GVAprobacion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DataTable vDatos = (DataTable)Session["CE_DATOSAPROBACION"];
            if (e.CommandName == "Aprobar")
            {
                String vId = e.CommandArgument.ToString();

                int vCondicion = 2;

                Response.Redirect("/sites/cableado/page/visita/visitaTecnica.aspx?a=" + vCondicion + "&ia=" + vId);

            }
        }

        protected void TxBuscarEstudio_TextChanged(object sender, EventArgs e)
        {

            try
            {
                CargarProceso();
                String vBusqueda = TxBuscarEstudio.Text;
                DataTable vDatos = (DataTable)Session["CE_DATOSAPROBACION"];
                if (vBusqueda.Equals(""))
                {
                    GVAprobacion.DataSource = vDatos;
                    GVAprobacion.DataBind();
                    udpAprobacion.Update();
                }
                else
                {
                    EnumerableRowCollection<DataRow> filtered = vDatos.AsEnumerable()
                       .Where(r => r.Field<String>("agencia").Contains(vBusqueda.ToUpper()));

                    Boolean isNumeric = int.TryParse(vBusqueda, out int n);

                    if (isNumeric)
                    {
                        if (filtered.Count() == 0)
                        {
                            filtered = vDatos.AsEnumerable().Where(r =>
                                Convert.ToInt32(r["idEstudio"]) == Convert.ToInt32(vBusqueda));
                        }
                    }

                    DataTable vDatosFiltrados = new DataTable();
                    vDatosFiltrados.Columns.Add("idEstudio");
                    vDatosFiltrados.Columns.Add("nombre");
                    vDatosFiltrados.Columns.Add("agencia");
                    vDatosFiltrados.Columns.Add("responsable");
                    vDatosFiltrados.Columns.Add("fechaCreacion");

                    foreach (DataRow item in filtered)
                    {
                        vDatosFiltrados.Rows.Add(
                            item["idEstudio"].ToString(),
                            item["nombre"].ToString(),
                            item["agencia"].ToString(),
                            item["responsable"].ToString(),
                            item["fechaCreacion"].ToString()
                            );
                    }

                    GVAprobacion.DataSource = vDatosFiltrados;
                    GVAprobacion.DataBind();
                    Session["CE_DATOSAPROBACION"] = vDatosFiltrados;
                    //udpContabilidad.Update();
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
    }
}