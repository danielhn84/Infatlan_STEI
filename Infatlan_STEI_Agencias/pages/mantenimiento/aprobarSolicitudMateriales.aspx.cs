using Infatlan_STEI_Agencias.classes;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

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
                Session["AG_ASM_RESPONSABLE"] = vDatos.Rows[0]["usuarioResponsable"].ToString();
                Session["AG_ASM_ID_MUNICIPIO"] = vDatos.Rows[0]["idMunicipio"].ToString();
                Session["AG_ASM_LUGAR_NUEVO"] = vDatos.Rows[0]["idUbicacion"].ToString();

                Int32 RbConductorConverido = Convert.ToInt32(vDatos.Rows[0]["requiereConductor"]);
                RbConductor.SelectedValue = RbConductorConverido.ToString();
                lbTitulo.Text = "Aprobar Solicitud de Materiales " + TxAgencia.Text;
                UpdatePanel1.Update();
                //if (RbConductorConverido==1)
                //{
                //    //DivConductor.Visible = false;

                //}

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
            //if (RbConductor.SelectedValue.Equals("1")  && DDLConductor.SelectedValue.Equals("0"))
            //    throw new Exception("Falta completar datos, Favor seleccionar el conductor asignado. ");

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

            //DDLConductor.SelectedIndex = -1;

        }

        void TransaccionInventario()
        {
            try
            {
                string vAbastecerMateriales = "";
                int vCantidadAbastecer = 0;
                DataTable vDatosMateriales = (DataTable)Session["AG_ASM_MATERIALES_SOLICITADOS"];
                for (int i = 0; i < vDatosMateriales.Rows.Count; i++)
                {
                    string vIdInventario = vDatosMateriales.Rows[i]["idInventario"].ToString();
                    string vNombreMaterial = vDatosMateriales.Rows[i]["nombre"].ToString();
                    int vCantidadSolicitada = Convert.ToInt32(vDatosMateriales.Rows[i]["cantidad"].ToString());

                    String vQuery = "STEISP_AGENCIA_Materiales 12," + vIdInventario;
                    DataTable vDataInventario = vConexion.obtenerDataTable(vQuery);
                    Decimal vCantidad = Convert.ToDecimal(vDataInventario.Rows[0]["cantidad"].ToString());
                    Decimal vCantidadActual = vCantidad - Convert.ToDecimal(vCantidadSolicitada);

                    if (vCantidadActual < 0)
                    {
                        vAbastecerMateriales = vAbastecerMateriales + "No hay suficiente material de " + vNombreMaterial + " en existencia" + "<br>";
                        vCantidadAbastecer = vCantidadAbastecer + 1;
                    }
                }

                if (vCantidadAbastecer >= 1)
                {
                    throw new Exception(vAbastecerMateriales);
                }








                for (int i = 0; i < vDatosMateriales.Rows.Count; i++)
                {
                    //string vMantenimiento = vDatos.Rows[i]["idMantenimiento"].ToString();
                    string vIdInventario = vDatosMateriales.Rows[i]["idInventario"].ToString();
                    string vNombreMaterial = vDatosMateriales.Rows[i]["nombre"].ToString();
                    int vCantidadSolicitada = Convert.ToInt32(vDatosMateriales.Rows[i]["cantidad"].ToString());
                    int vStock = Convert.ToInt32(vDatosMateriales.Rows[i]["idStock"].ToString());

                    String vQuery = "STEISP_AGENCIA_Materiales 12," + vIdInventario;
                    DataTable vDataInventario = vConexion.obtenerDataTable(vQuery);
                    Decimal vCantidad = Convert.ToDecimal(vDataInventario.Rows[0]["cantidad"].ToString());
                    Decimal vCantidadActual = vCantidad - Convert.ToDecimal(vCantidadSolicitada);

                    vQuery = "[STEISP_INVENTARIO_Stock] 2," + vStock;
                    DataTable vDataStock = vConexion.obtenerDataTable(vQuery);
                    Decimal vPrecioDec = Convert.ToDecimal(vCantidadSolicitada) * Convert.ToDecimal(vDataStock.Rows[0]["precioUnit"].ToString());
                    String vPrecio = vPrecioDec.ToString().Replace(",", ".");

                    generarxml vMaestro = new generarxml();
                    Object[] vDatosMaestro = new object[10];
                    vDatosMaestro[0] = "";
                    vDatosMaestro[1] = vStock;
                    vDatosMaestro[2] = Session["AG_ASM_LUGAR_NUEVO"].ToString(); // NUEVA
                    vDatosMaestro[3] = Session["AG_ASM_RESPONSABLE"].ToString(); //Responsable
                    vDatosMaestro[4] = "TRAN_UBIC";
                    vDatosMaestro[5] = vCantidadSolicitada;
                    vDatosMaestro[6] = ""; // Serie
                    vDatosMaestro[7] = vPrecio;
                    vDatosMaestro[8] = Session["USUARIO"].ToString();
                    vDatosMaestro[9] = 14;
                    String vXML = vMaestro.ObtenerMaestroString(vDatosMaestro);
                    vXML = vXML.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");


                    vQuery = "STEISP_AGENCIA_Materiales 11, '" + Session["AG_ASM_ID_MUNICIPIO"] + "'";
                    DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                    Session["AG_ASM_CODIGO_DESCONTAR"] = vDatos.Rows[0]["idUbicacion"].ToString();

                    if (Convert.ToDecimal(vCantidadActual) == Convert.ToDecimal(vCantidadSolicitada))
                    {
                        vQuery = "[STEISP_INVENTARIO_Principal] 3" +
                        "," + vIdInventario +
                        "," + Session["AG_ASM_CODIGO_DESCONTAR"].ToString() +  //UBICACION ANTERIOR
                        ",'" + vXML + "'";

                        Int32 vInfo = vConexion.ejecutarSql(vQuery);
                        //if (vInfo == 2)
                        //{
                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "cerrarModal();", true);
                        //    Mensaje("Cambio realizado con éxito.", WarningType.Success);

                        //}
                    }
                    else if (Convert.ToDecimal(vCantidadActual) > Convert.ToDecimal(vCantidadSolicitada))
                    {
                        vQuery = "[STEISP_INVENTARIO_Principal] 6" +
                        "," + vIdInventario +
                        "," + Session["AG_ASM_CODIGO_DESCONTAR"].ToString() +  //UBICACION ANTERIOR
                        ",'" + vXML + "'";

                        Int32 vInfo = vConexion.ejecutarSql(vQuery);
                        //if (vInfo == 4)
                        //{
                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "cerrarModal();", true);
                        //    Mensaje("Cambio realizado con éxito.", WarningType.Success);

                        //}
                    }





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