using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using Infatlan_STEI_Agencias.classes;
using System.Configuration;

namespace Infatlan_STEI_Agencias.pages.mantenimiento
{
    public partial class solicitudMateriales : System.Web.UI.Page
    {
        db vConexion = new db();
        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }
        
        protected void Page_Load(object sender, EventArgs e){
            DDLArticulo.CssClass = "select2 form-control custom-select";

            if (!Page.IsPostBack){
                if (Convert.ToBoolean(Session["AUTH"])){
                    cargarDatos();
                }else {
                    Response.Redirect("/login.aspx");
                }
            }
        }

        private void cargarDatos()
        {
            try
            {

                String vQuery = "STEISP_AGENCIA_Materiales 1";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                GVBusqueda.DataSource = vDatos;
                GVBusqueda.DataBind();
                //Session["AG_CN_MANTENIMIENTOS_PENDIENTES_APROBAR"] = vDatos;



            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
        
        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string vIdMantenimiento = e.CommandArgument.ToString();
            Session["AG_SM_ID_MANTENIMIENTO"] = vIdMantenimiento;

            if (e.CommandName == "Aprobar")
            {

                DivAlertaCantidad.Visible = false;
                UpCantidadMaxima.Update();

                DivAlerta.Visible = false;
                UpdateModal.Update();





                GVNewMateriales.DataSource = null;
                GVNewMateriales.DataBind();
                UPMateriales.Update();


                String vQuery = "STEISP_AGENCIA_Materiales 2," + vIdMantenimiento;
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
     
                TxIdMant.Text = vDatos.Rows[0]["id_Mantenimiento"].ToString(); 
                TxAgencia.Text = vDatos.Rows[0]["Lugar"].ToString();
                TxFecha.Text = vDatos.Rows[0]["fecha"].ToString();
                TxArea.Text = vDatos.Rows[0]["Area"].ToString();
                TxUbicacion.Text = vDatos.Rows[0]["codigoUbicacion"].ToString();
                Session["AG_SM_MUNICIPIO"] = vDatos.Rows[0]["idMunicipio"].ToString(); 
                Int32 RbConductorConverido = Convert.ToInt32(vDatos.Rows[0]["requiereConductor"]);
                RbConductor.SelectedValue = RbConductorConverido.ToString();
                lbTitulo.Text = "Solicitud de Materiales " + TxAgencia.Text;
                UpdatePanel1.Update();


                vQuery = "STEISP_AGENCIA_Materiales 11, '" + Session["AG_SM_MUNICIPIO"] + "'";
                vDatos = vConexion.obtenerDataTable(vQuery);
                Session["AG_SM_CODIGO_DESCONTAR"] = vDatos.Rows[0]["idUbicacion"].ToString();

                vQuery = "STEISP_AGENCIA_Materiales 3, '"+ Session["AG_SM_CODIGO_DESCONTAR"] + "'";
                vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0)
                {
                    DDLArticulo.Items.Clear();
                    DDLArticulo.Items.Add(new ListItem { Value = "0", Text = "Seleccione" });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLArticulo.Items.Add(new ListItem { Value = item["idInventario"].ToString(), Text = item["TipoStock"].ToString() + "  " + item["modelo"].ToString() + " - " + item["marca"].ToString() + " (" + item["cantidad"].ToString() + ")" });
                    }
                }


                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModalMaterial();", true);
            }
            else if (e.CommandName == "Cancelar")
            {
                TxDetalle.Text = "";
                Div2.Visible = false;
                UpdatePanel4.Update();

                string vIdMantenimientoMateriales = e.CommandArgument.ToString();
                Session["AG_SM_ID_MANTENIMIENTO"] = vIdMantenimientoMateriales;

                TxIdMantenimiento.Text = vIdMantenimientoMateriales;


                DDLMotivo.Items.Clear();
                string vQuery = "STEISP_AGENCIA_AprobarNotificacion 8";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                DDLMotivo.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });

                if (vDatos.Rows.Count > 0)
                {
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLMotivo.Items.Add(new ListItem { Value = item["id"].ToString(), Text = item["motivo"].ToString() });
                    }
                }
               
                tecnicosResponsable();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModalMaterialCancelar();", true);





            }

        }
        
        protected void btnAgregar_Click(object sender, EventArgs e)
        {          
            try
            {
                validaciones();
                if (Convert.ToInt32(TxCantidad.Text) > Convert.ToInt32(Session["AG_SM_CANTIDAD_MATERIALES"]))
                {                    
                    lbCantidad.Text = "La cantidad solicitada: "+ TxCantidad.Text + " de " + DDLArticulo.SelectedItem.Text + " excede a la cantidad en existencia, favor verificar la cantidad a solicitar";
                    TxCantidad.Text = string.Empty;
                    DivAlertaCantidad.Visible = true;
                    UpCantidadMaxima.Update();
                }
                else
                {
                    DataTable vData = new DataTable();
                    DataTable vDatos = (DataTable)Session["AG_SM_MATERIALES"];
                    //string vNombreMaterial = DDLArticulo.SelectedItem.Text;
                    Char delimiter = '(';
                    string[] vNombreMaterial = DDLArticulo.SelectedItem.Text.Split(delimiter);

                    string vNombreMaterialMatriz = vNombreMaterial[0];

                    vData.Columns.Add("idInventario");
                    vData.Columns.Add("nombre");
                    vData.Columns.Add("cantidad");

                    if (vDatos == null)
                        vDatos = vData.Clone();
                    if (vDatos != null)
                    {
                        if (vDatos.Rows.Count < 1)
                            vDatos.Rows.Add(DDLArticulo.SelectedValue, vNombreMaterialMatriz, TxCantidad.Text);
                        else
                        {
                            Boolean vRegistered = false;
                            for (int i = 0; i < vDatos.Rows.Count; i++)
                            {
                                if (vNombreMaterialMatriz == vDatos.Rows[i]["nombre"].ToString())
                                {
                                    //vDatos.Rows[i]["cantidad"] = Convert.ToDecimal(vDatos.Rows[i]["cantidad"].ToString()) + Convert.ToDecimal(TxCantidad.Text);

                                    lbCantidad.Text = "El material seleccionado: " + vNombreMaterialMatriz + " ya esta agregado en la lista, favor verificar";
                                    DivAlertaCantidad.Visible = true;
                                    UpCantidadMaxima.Update();

                                    vRegistered = true;
                                }
                            }

                            if (!vRegistered)
                                vDatos.Rows.Add(DDLArticulo.SelectedValue, vNombreMaterialMatriz, TxCantidad.Text);
                        }
                    }

                    GVNewMateriales.DataSource = vDatos;
                    GVNewMateriales.DataBind();
                    Session["AG_SM_MATERIALES"] = vDatos;
                    UPMateriales.Update();

                    DDLArticulo.SelectedIndex = -1;
                    TxCantidad.Text = "";
                }               
            }
            catch (Exception ex)
            {
                lbCantidad.Text = ex.Message;
                DivAlertaCantidad.Visible = true;
                UpCantidadMaxima.Update();
            }  
        }

        protected void TxCantidad_TextChanged(object sender, EventArgs e)
        {
            DivAlertaCantidad.Visible = false;
            UpCantidadMaxima.Update();
        }

        protected void DDLArticulo_SelectedIndexChanged(object sender, EventArgs e)
        {

            DivAlertaCantidad.Visible = false;
            UpCantidadMaxima.Update();

            DivAlerta.Visible = false;
            UpdateModal.Update();

            //STOCK
            String vQuery = "STEISP_AGENCIA_Materiales 4, '" + DDLArticulo.SelectedValue + "'";
            DataTable vDatos = vConexion.obtenerDataTable(vQuery);
        
            Decimal cant = Convert.ToDecimal(vDatos.Rows[0]["cantidad"].ToString());
            Session["AG_SM_CANTIDAD_MATERIALES"] = cant;

            DivAlertaCantidad.Visible = false;
            UpCantidadMaxima.Update();
        }

        protected void GVNewMateriales_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DataTable vDatos = (DataTable)Session["AG_SM_MATERIALES"];
            if (e.CommandName == "eliminar")
            {
                String vID = e.CommandArgument.ToString();
                if (Session["AG_SM_MATERIALES"] != null)
                {

                    DataRow[] result = vDatos.Select("idStock = '" + vID + "'");
                    foreach (DataRow row in result)
                    {
                        if (row["idInventario"].ToString().Contains(vID))
                            vDatos.Rows.Remove(row);
                    }
                }
            }
            GVNewMateriales.DataSource = vDatos;
            GVNewMateriales.DataBind();
            Session["AG_SM_MATERIALES"] = vDatos;
            DivAlertaCantidad.Visible = false;
            UpCantidadMaxima.Update();
        }
        

        
        protected void btnModalEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                validarEnvio();
                DataTable vDatosMaterialesSolicitar = (DataTable)Session["AG_SM_MATERIALES"];
                string vIdMantenimiento = TxIdMant.Text;
                if (vDatosMaterialesSolicitar.Rows.Count > 0)
                {
                    for (int num = 0; num < vDatosMaterialesSolicitar.Rows.Count; num++)
                    {
                        
                        string vIdInventario = vDatosMaterialesSolicitar.Rows[num]["idInventario"].ToString();
                        string vCantidad = vDatosMaterialesSolicitar.Rows[num]["cantidad"].ToString();

                        String vQuery4 = "STEISP_AGENCIA_Materiales 5,'" + vIdMantenimiento + 
                            "','" + vIdInventario + 
                            "','"+ vCantidad+
                            "','"+ Session["USUARIO"]+ "'";
                        Int32 vInfo4 = vConexion.ejecutarSql(vQuery4);

                    }
                }

                String vQuery = "STEISP_AGENCIA_Materiales 6," + vIdMantenimiento;
                Int32 vInfo5 = vConexion.ejecutarSql(vQuery);

                if (vInfo5 == 1)
                {
                    LimpiarModalMaterial();
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModalMaterial();", true);
                    Mensaje("Solicitud de materiales con éxito.", WarningType.Success);
                    cargarDatos();
                }

            }
            catch (Exception ex)
            {
                lbCantidad.Text = ex.Message;
                DivAlertaCantidad.Visible = true;
                UpCantidadMaxima.Update();
            }
        }

        private void LimpiarModalMaterial()
        {
            DDLArticulo.SelectedIndex = -1;
            TxCantidad.Text = String.Empty;

            GVNewMateriales.DataSource = null;
            GVNewMateriales.DataBind();
            UPMateriales.Update();
        }

        private void validarEnvio()
        {
            if (Session["AG_SM_MATERIALES"] == null)
                throw new Exception("Lista de materiales vacia, favor seleccionar material a solicitar. ");
        }

        private void tecnicosResponsable()
        {
            try
            {
                DDLNombreResponsable.Items.Clear();
                String vQuery = "STEISP_AGENCIA_CreacionNotificacion 5";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                DDLNombreResponsable.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });

                if (vDatos.Rows.Count > 0)
                {
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLNombreResponsable.Items.Add(new ListItem { Value = item["idUsuario"].ToString(), Text = item["nombre"].ToString() + "  " + item["apellidos"].ToString() });

                    }
                }

            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void DDLMotivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLMotivo.SelectedValue.Equals("4"))
            {
                asterisco.Visible = true;
                DDLNombreResponsable.Visible = true;
                etiqueta.Visible = true;
            }
            else
            {
                asterisco.Visible = false;
                DDLNombreResponsable.Visible = false;
                etiqueta.Visible = false;
                funcionCambioTecnicoResponsable();
            }
        }

        private void funcionCambioTecnicoResponsable()
        {
            LbMensajeModalErrorMateriales.Text = "";
            Div2.Visible = false;
            UpdatePanel4.Update();
        }

        protected void BtnCancelarMateriales_Click(object sender, EventArgs e)
        {
            try
            {
                validacionesCancelarMateriales();
                String vQuery = "STEISP_AGENCIA_AprobarNotificacion  4," + Session["AG_SM_ID_MANTENIMIENTO"] + "," + Session["USUARIO"] + "," + "'" + DDLMotivo.SelectedItem.Text + "'" + "," + "'" + TxDetalle.Text + "'";
                Int32 vInfo = vConexion.ejecutarSql(vQuery);

                String vQuery1 = "STEISP_AGENCIA_AprobarNotificacion  6," + Session["AG_SM_ID_MANTENIMIENTO"];
                Int32 vInfo1 = vConexion.ejecutarSql(vQuery1);

                if (vInfo == 1)
                {
                    Mensaje("Solicitud cancelada con exito, esta pendiente que el jefe o suplente reprogramen el mantenimiento", WarningType.Success);
                    LimpiarModalCancelar();
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModalMaterialCancelar();", true);
                }
                cargarDatos();

            }
            catch (Exception ex)
            {
                LbMensajeModalErrorMateriales.Text = ex.Message;
                Div2.Visible = true;
                UpdatePanel4.Update();

            }
        }

        private void validaciones()
        {
            if (DDLArticulo.SelectedValue.Equals("0"))
                throw new Exception("Falta completar datos, Favor seleccionar un tipo de material a solicitar. ");

            if (TxCantidad.Text.Equals(""))
                throw new Exception("Falta completar datos, Favor ingrese la cantidad de material a solicitar. ");
        }


        private void validacionesCancelarMateriales()
        {
            if (DDLMotivo.SelectedValue.Equals("0"))
                throw new Exception("Falta completar datos, Favor seleccionar un motivo de cancelación del mantenimiento. ");


            if (TxDetalle.Text.Equals(""))
                throw new Exception("Falta completar datos, Favor ingrese detalle de la cancelación del mantenimiento. ");
        }



        private void LimpiarModalCancelar()
        {
            DDLMotivo.SelectedIndex = -1;
            DDLNombreResponsable.SelectedIndex = -1;

            TxDetalle.Text = String.Empty;
        }


    }
}