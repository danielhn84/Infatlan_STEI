using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Infatlan_STEI_Agencias.classes;
using System.Configuration;

namespace Infatlan_STEI_Agencias.pages.mantenimiento
{
    public partial class aprobarSolicitudMateriales : System.Web.UI.Page
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
                cargarDatos();
            }
        }
        
        private void cargarDatos()
        {
            try
            {
                String vQuery = "STEISP_AGENCIA_Materiales 7";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);                

                GVMaterialesAprobar.DataSource = vDatos;
                GVMaterialesAprobar.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
        
        protected void GVMaterialesAprobar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string vIdMantenimiento = e.CommandArgument.ToString();
            Session["AG_ASM_ID_MANTENIMIENTO"] = vIdMantenimiento;

            if (e.CommandName == "Aprobar")
            {
                String vQuery = "STEISP_AGENCIA_Materiales 8," + vIdMantenimiento;
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                TxIdMant.Text = vDatos.Rows[0]["id_Mantenimiento"].ToString();
                TxAgencia.Text = vDatos.Rows[0]["Lugar"].ToString();
                TxFecha.Text = vDatos.Rows[0]["fecha"].ToString();
                TxArea.Text = vDatos.Rows[0]["Area"].ToString();
                TxUbicacion.Text = vDatos.Rows[0]["codigoUbicacion"].ToString();
                Session["AG_ASM_COD_UBICACION"] = vDatos.Rows[0]["codigoUbicacion"].ToString();
                Int32 RbConductorConverido = Convert.ToInt32(vDatos.Rows[0]["requiereConductor"]);
                RbConductor.SelectedValue = RbConductorConverido.ToString();
                lbTitulo.Text = "Aprobar Solicitud de Materiales " + TxAgencia.Text;
                UpdatePanel1.Update();
                if (RbConductorConverido==1)
                {
                    DivConductor.Visible = true;
                }

                 vQuery = "STEISP_AGENCIA_Materiales 9," + vIdMantenimiento;
                 vDatos = vConexion.obtenerDataTable(vQuery);
                Session["AG_ASM_MATERIALES_SOLICITADOS"] = vDatos;
                GVNewMaterialesAprobar.DataSource = vDatos;
                GVNewMaterialesAprobar.DataBind();
                UPMaterialesAprobar.Update();

                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModalAprobarMaterial();", true);
            }
        }
        
        private void validaciones()
        {
            if (RbConductor.SelectedValue.Equals("1")  && DDLConductor.SelectedValue.Equals("0"))
                throw new Exception("Falta completar datos, Favor seleccionar el conductor asignado. ");

        }
        
        protected void btnModalAprobar_Click(object sender, EventArgs e)
        {
            try
            {
            validaciones();
            TransaccionInventario();

            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModalAprobarMaterial();", true);
            limpiarModal();
            cargarDatos();
            }
            catch (Exception ex)
            {
                LbMensajeModalError.Text = ex.Message;
                DivAlerta.Visible = true;
                UpdateModal.Update();
            }            
        }
        
        protected void DDLConductor_TextChanged(object sender, EventArgs e)
        {
            DivAlerta.Visible = false;
            UpdateModal.Update();
        }
        
        void limpiarModal()
        {
            GVNewMaterialesAprobar.DataSource = null;
            GVNewMaterialesAprobar.DataBind();
            UPMaterialesAprobar.Update();

            DDLConductor.SelectedIndex = -1;

        }
        
        void TransaccionInventario()
        {
            try
            {
                DataTable vDatos = (DataTable)Session["AG_ASM_MATERIALES_SOLICITADOS"];
                for (int i = 0; i < vDatos.Rows.Count; i++)
                {
                    //string vMantenimiento = vDatos.Rows[i]["idMantenimiento"].ToString();
                    string vStock = vDatos.Rows[i]["idStock"].ToString();
                    int vCantidadSolicitada = Convert.ToInt32(vDatos.Rows[i]["cantidad"].ToString());

                    String vQuery = "[STEISP_INVENTARIO_Stock] 2," + vStock;
                    DataTable vDataStock = vConexion.obtenerDataTable(vQuery);
                    Decimal vCantidad = Convert.ToDecimal(vDataStock.Rows[0]["cantidad"].ToString());
                    Decimal vCantidadActual = vCantidad - Convert.ToDecimal(vCantidadSolicitada);
                    String vNombreMaterial = vDataStock.Rows[0]["descripcion"].ToString();
                    if (vCantidadActual < 0)
                    {
                        throw new Exception("No hay suficiente material de " + vNombreMaterial + " en existencia");
                    }
                    Decimal vPrecioDec = Convert.ToDecimal(vCantidadSolicitada) * Convert.ToDecimal(vDataStock.Rows[0]["precioUnit"].ToString());
                    String vPrecio = vPrecioDec.ToString().Replace(",", ".");
                    String vCodigoUbi = Session["AG_ASM_COD_UBICACION"].ToString();
                    String vUsuario = Session["USUARIO"].ToString();
                    generarxml vMaestro = new generarxml();
                    Object[] vDatosMaestro = new object[10];
                    vDatosMaestro[0] = vCodigoUbi;
                    vDatosMaestro[1] = vStock;
                    vDatosMaestro[2] = "6";
                    vDatosMaestro[3] = ""; //Responsable
                    vDatosMaestro[4] = "Solicitud de materiales para mantenimiento preventivo en agencia";
                    vDatosMaestro[5] = vCantidadSolicitada;
                    vDatosMaestro[6] = ""; // Serie
                    vDatosMaestro[7] = vPrecio;
                    vDatosMaestro[8] = vUsuario;
                    vDatosMaestro[9] = "6";
                    String vXML = vMaestro.ObtenerMaestroString(vDatosMaestro);
                    vXML = vXML.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");

                    vQuery = "[STEISP_INVENTARIO_Principal] 1" +
                        "," + vStock +
                        "," + vCantidadActual +
                        ",'" + vXML + "'";
                    Int32 vInfo = vConexion.ejecutarSql(vQuery);

                    vQuery = "STEISP_AGENCIA_Materiales 10," + Session["AG_ASM_ID_MANTENIMIENTO"] +","+ DDLConductor.SelectedValue + ",'"+ Session["USUARIO"]+"'";
                    Int32 vInfo1 = vConexion.ejecutarSql(vQuery);

                }
            }
            catch (Exception ex)
            {
                LbMensajeModalError.Text = ex.Message;
                DivAlerta.Visible = true;
                UpdateModal.Update();
            }
        }
    }
}