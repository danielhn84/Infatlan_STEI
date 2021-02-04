using Infatlan_STEI_ATM.clases;
using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Infatlan_STEI_ATM.pages.mantenimiento
{

    public partial class cancelarMantenimiento : System.Web.UI.Page
    {
        bd vConexion = new bd();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Convert.ToBoolean(Session["AUTH"]))
                {
                    cargarData();
                }
                else
                {
                    Response.Redirect("/login.aspx");
                }
            }
        }

        void cargarData()
        {
            //LISTA DE MANTENIMIENTOS A CANCELAR
            DataTable vDatos2 = new DataTable();
            vDatos2 = vConexion.ObtenerTabla("[STEISP_ATM_CancelarMantenimiento] 1");
            GVCancelar.DataSource = vDatos2;
            GVCancelar.DataBind();
            Session["ATM_CANCELAR"] = vDatos2;

            //LISTA DE MANTENIMIENTOS CANCELADOS
            DataTable vDatos = new DataTable();
            vDatos = vConexion.ObtenerTabla("[STEISP_ATM_CancelarMantenimiento] 6");
            GVMantenimientoCancelado.DataSource = vDatos;
            GVMantenimientoCancelado.DataBind();
            Session["ATM_CANCELADO"] = vDatos;
        }

        protected void GVCancelar_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GVCancelar.PageIndex = e.NewPageIndex;
                GVCancelar.DataSource = (DataTable)Session["ATM_CANCELAR"];
                GVCancelar.DataBind();
            }
            catch (Exception Ex)
            {

            }
        }

        protected void GVCancelar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DataTable vDataaaa = (DataTable)Session["ATM_CANCELAR"];
                string IDMantenimiento = e.CommandArgument.ToString();

                if (e.CommandName == "cancelar")
                {
                    try
                    {
                        DataTable vDatos = new DataTable();
                        String vQuery = "STEISP_ATM_CancelarMantenimiento 2,'" + IDMantenimiento + "'";
                        vDatos = vConexion.ObtenerTabla(vQuery);
                        foreach (DataRow item in vDatos.Rows)
                        {
                            Session["ID_MANT_CANCELAR"] = item["ID"].ToString();
                            LBTitleModal.InnerText = "¿Desea cancelar mantenimiento No. " + item["ID"].ToString() + "?";
                            LbATM.Text = item["Codigo"].ToString() + " - " + item["Nombre"].ToString();

                        }
                        txtAlerta.Visible = false;
                        txtMotivo.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);
                        //Response.Redirect("aprobarNotificacionATM.aspx");
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }


            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        protected void TxBuscarATM_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cargarData();

                String vBusqueda = TxBuscarATM.Text.ToUpper().ToString();
                DataTable vDatos = (DataTable)Session["ATM_CANCELAR"];

                if (vBusqueda.Equals(""))
                {
                    GVCancelar.DataSource = vDatos;
                    GVCancelar.DataBind();
                }
                else
                {
                    EnumerableRowCollection<DataRow> filtered = vDatos.AsEnumerable()
                        .Where(r => r.Field<String>("Codigo").Contains(vBusqueda));

                    DataTable vDatosFiltrados = new DataTable();
                    vDatosFiltrados.Columns.Add("ID");
                    vDatosFiltrados.Columns.Add("Codigo");
                    vDatosFiltrados.Columns.Add("Nombre");
                    vDatosFiltrados.Columns.Add("Fecha");
                    vDatosFiltrados.Columns.Add("Avance");
                    foreach (DataRow item in filtered)
                    {
                        vDatosFiltrados.Rows.Add(
                            item["ID"].ToString(),
                            item["Codigo"].ToString(),
                            item["Nombre"].ToString(),
                            item["Fecha"].ToString(),
                            item["Avance"].ToString()
                            );
                    }

                    GVCancelar.DataSource = vDatosFiltrados;
                    GVCancelar.DataBind();
                    Session["ATM_CANCELAR"] = vDatosFiltrados;
                }


            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        protected void btnModalCerrar_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
        }

        protected void btnModalCncelar_Click(object sender, EventArgs e)
        {
            if (txtMotivo.Text == "" || txtMotivo.Text == string.Empty)
            {
                txtAlerta.Visible = true;
            }
            else
            {
                txtAlerta.Visible = false;
                try
                {
                    string vQuery = "STEISP_ATM_CancelarMantenimiento 3,'" + txtMotivo.Text + "','" + Session["USUARIO"] + "', '" + Session["ID_MANT_CANCELAR"] + "'";
                    Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    if (vInfo == 1)
                    {
                        string vQuery2 = "STEISP_ATM_CancelarMantenimiento 4,'" + Session["ID_MANT_CANCELAR"] + "'";
                        vConexion.ejecutarSQL(vQuery2);
                        txtMotivo.Text = string.Empty;
                        TxBuscarATM.Text = "";
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
                        Mensaje("Mantenimiento de ATM cancelado con éxito", WarningType.Success);
                        cargarData();
                        txtAlerta.Visible = false;
                    }
                    else
                    {
                        txtAlerta.Text = "No se pudo modificar el modelo";
                        txtAlerta.Visible = true;
                    }
                }
                catch (Exception Ex)
                {
                    throw;
                }
            }
        }

        protected void txtBuscarCancelado_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cargarData();

                String vBusqueda = txtBuscarCancelado.Text.ToUpper().ToString();
                DataTable vDatos = (DataTable)Session["ATM_CANCELADO"];

                if (vBusqueda.Equals(""))
                {
                    GVMantenimientoCancelado.DataSource = vDatos;
                    GVMantenimientoCancelado.DataBind();
                }
                else
                {
                    EnumerableRowCollection<DataRow> filtered = vDatos.AsEnumerable()
                        .Where(r => r.Field<String>("Codigo").Contains(vBusqueda));

                    DataTable vDatosFiltrados = new DataTable();
                    vDatosFiltrados.Columns.Add("ID");
                    vDatosFiltrados.Columns.Add("Codigo");
                    vDatosFiltrados.Columns.Add("Nombre");
                    vDatosFiltrados.Columns.Add("Fecha");
                    vDatosFiltrados.Columns.Add("Avance");
                    foreach (DataRow item in filtered)
                    {
                        vDatosFiltrados.Rows.Add(
                            item["ID"].ToString(),
                            item["Codigo"].ToString(),
                            item["Nombre"].ToString(),
                            item["Fecha"].ToString(),
                            item["Avance"].ToString()
                            );
                    }

                    GVMantenimientoCancelado.DataSource = vDatosFiltrados;
                    GVMantenimientoCancelado.DataBind();
                    Session["ATM_CANCELADO"] = vDatosFiltrados;
                }


            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        protected void GVMantenimientoCancelado_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GVMantenimientoCancelado.PageIndex = e.NewPageIndex;
                GVMantenimientoCancelado.DataSource = (DataTable)Session["ATM_CANCELADO"];
                GVMantenimientoCancelado.DataBind();
            }
            catch (Exception Ex)
            {

            }
        }

        protected void GVMantenimientoCancelado_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string IDMantenimiento = e.CommandArgument.ToString();

                if (e.CommandName == "ver")
                {
                    try
                    {
                        DataTable vDatos = new DataTable();
                        String vQuery = "STEISP_ATM_CancelarMantenimiento 5,'" + IDMantenimiento + "'";
                        vDatos = vConexion.ObtenerTabla(vQuery);
                        foreach (DataRow item in vDatos.Rows)
                        {
                            H1VerMotivo.InnerText = "Mantenimiento cancelado No. " + item["ID"].ToString();
                            LbATMCancelado.Text = item["Codigo"].ToString() + " - " + item["Nombre"].ToString();
                            txtMotivoCancelado.Text = item["Motivo"].ToString();
                        }
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openVerMotivo();", true);
                        //Response.Redirect("aprobarNotificacionATM.aspx");
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }


            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeVerMotivo();", true);
        }
    }
}