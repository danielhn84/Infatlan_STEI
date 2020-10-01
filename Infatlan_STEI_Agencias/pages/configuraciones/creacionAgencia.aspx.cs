using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infatlan_STEI_Agencias.classes;
using System.Data;


namespace Infatlan_STEI_Agencias.pages.configuraciones
{
    public partial class creacionAgencia : System.Web.UI.Page
    {
        db vConexion = new db();
        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }
        
        protected void Page_Load(object sender, EventArgs e){
            if (!Page.IsPostBack){
                if (Convert.ToBoolean(Session["AUTH"])){
                    cargarData();
                    cargarDataAgencias();
                }else {
                    Response.Redirect("/login.aspx");
                }
            }
        }
        
        void cargarData()
        {
            try
            {
                DDLTipoAgencia.Items.Clear();
                DDLTipoAgenciaModificar.Items.Clear();
                String vQuery = "STEISP_AGENCIA_CreacionAgencia 1";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                DDLTipoAgencia.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                
                if (vDatos.Rows.Count > 0)
                {
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLTipoAgencia.Items.Add(new ListItem { Value = item["idTipoAgencia"].ToString(), Text = item["nombre"].ToString() });
                        DDLTipoAgenciaModificar.Items.Add(new ListItem { Value = item["idTipoAgencia"].ToString(), Text = item["nombre"].ToString() });
                    }
                }

                DDLDepartamento.Items.Clear();
                DDLDepartamentoModificar.Items.Clear();
                String vQuery1 = "STEISP_AGENCIA_CreacionAgencia 2";
                DataTable vDatos1 = vConexion.obtenerDataTable(vQuery1);
                DDLDepartamento.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
              
                if (vDatos1.Rows.Count > 0)
                {
                    foreach (DataRow item in vDatos1.Rows)
                    {
                        DDLDepartamento.Items.Add(new ListItem { Value = item["idDepartamento"].ToString(), Text = item["nombre"].ToString() });
                        DDLDepartamentoModificar.Items.Add(new ListItem { Value = item["idDepartamento"].ToString(), Text = item["nombre"].ToString() });
                    }
                }

                DDLMunicipioModificar.Items.Clear();
                DDLMunicipio.Items.Clear();
                String vQuery2 = "STEISP_AGENCIA_CreacionAgencia 10";
                DataTable vDatos2 = vConexion.obtenerDataTable(vQuery2);
                DDLMunicipioModificar.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });

                if (vDatos2.Rows.Count > 0)
                {
                    foreach (DataRow item in vDatos2.Rows)
                    {
                        DDLMunicipioModificar.Items.Add(new ListItem { Value = item["idMunicipio"].ToString(), Text = item["nombre"].ToString() });
                        //DDLMunicipioModificar.Items.Add(new ListItem { Value = item["idDepartamento"].ToString(), Text = item["nombre"].ToString() });
                    }
                }



            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
        
        private void validar()
        {
            if (TxAgencia.Text == "" || TxAgencia.Text == string.Empty)
                throw new Exception("Falta ingresar el nombre de la agencia.");

            if (TxCodigo.Text == "" || TxCodigo.Text == string.Empty)
                throw new Exception("Falta ingresar el codigo de la agencia.");

            if (TxDireccion.Text == "" || TxDireccion.Text == string.Empty)
                throw new Exception("Falta ingresar la dirección de la agencia.");

            //if (TxTelefono.Text == "" || TxTelefono.Text == string.Empty)
            //    throw new Exception("Falta ingresar telefonos de la agencia.");

            if (TxLatitud.Text == "" || TxLatitud.Text == string.Empty)
                throw new Exception("Falta ingresar latitud de la agencia.");

            if (TxLongitud.Text == "" || TxLongitud.Text == string.Empty)
                throw new Exception("Falta ingresar longitud de la agencia.");
           
            if (DDLTipoAgencia.SelectedValue.Equals("0"))
                throw new Exception("Seleccione un tipo de agencia valido.");

            if (DDLDepartamento.SelectedValue.Equals("0"))
                throw new Exception("Seleccione un departamento agencia valido.");

            if (DDLMunicipio.SelectedValue.Equals("0"))
                throw new Exception("Seleccione un municipio al que pertenece la agencia.");
            
            if (RblConductor.SelectedValue.Equals(""))
                throw new Exception("Falta completar opción ¿Si requiere de conductorn para el traslado hacia la agencia?.");
        }
        
        private void cargarDataAgencias()
        {
            try
            {
                String vQueryAgencias = "STEISP_AGENCIA_CreacionAgencia 4";
                DataTable vDatosAgencias = vConexion.obtenerDataTable(vQueryAgencias);
                GVAgencias.DataSource = vDatosAgencias;
                GVAgencias.DataBind();
                Session["AG_CA_AGENCIAS_BASA"] = vDatosAgencias;
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
        
        private void limpiar()
        {
            TxAgencia.Text = String.Empty;
            TxCodigo.Text = String.Empty;
            TxDireccion.Text = String.Empty;
            TxTelefono.Text = String.Empty;
            TxLatitud.Text = String.Empty;
            TxLongitud.Text = String.Empty;
            DDLTipoAgencia.SelectedIndex = -1;
            DDLDepartamento.SelectedIndex = -1;
            DDLMunicipio.SelectedIndex = -1;
            RblConductor.SelectedIndex = -1;
        }
        
        protected void BtnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                validar();

                string codigoUbicacion = "";
                String vQuery2 = "STEISP_INVENTARIO_Ubicaciones 6," + DDLDepartamento.SelectedValue;
                DataTable vDatos2 = vConexion.obtenerDataTable(vQuery2);
                foreach (DataRow item in vDatos2.Rows)
                {
                    codigoUbicacion = item["codigo"].ToString();
                }

                String vQuery1 = "STEISP_AGENCIA_CreacionAgencia 3,'"
                                   + TxAgencia.Text +
                                   "','" + TxCodigo.Text +
                                   "','" + TxDireccion.Text +
                                   "','" + TxTelefono.Text +
                                   "'," + DDLTipoAgencia.SelectedValue +
                                   ",'" + Session["USUARIO"] +
                                   "','" + TxLatitud.Text +
                                   "','" + TxLongitud.Text +
                                   "'," + DDLDepartamento.SelectedValue +
                                   "," + RblConductor.SelectedValue +
                                    ",'" + codigoUbicacion +
                                    "'," + DDLMunicipio.SelectedValue;
                Int32 vInformacion1 = vConexion.ejecutarSql(vQuery1);


                String vQuery3 = "STEISP_INVENTARIO_Ubicaciones 3,6,"
                                   + DDLMunicipio.SelectedValue +
                                   ",'" + codigoUbicacion +
                                    "','" + TxDireccion.Text +
                                    "','" + Session["USUARIO"] +
                                    "','" + TxAgencia.Text + "'";                                    
                Int32 vInformacion3 = vConexion.ejecutarSql(vQuery3);


                if (vInformacion1 == 1)
                {
                    Mensaje("Creación de agencia con exito. ", WarningType.Success);
                    limpiar();
                    cargarDataAgencias();
                    cargarData();
                    UPAgencias.Update();
                }
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
        
        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                limpiar();
                Mensaje("Acción cancelado con exito. ", WarningType.Success);
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
        
        protected void GVAgencias_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GVAgencias.PageIndex = e.NewPageIndex;
                GVAgencias.DataSource = (DataTable)Session["AG_CA_AGENCIAS_BASA"];
                GVAgencias.DataBind();
            }             
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
        
        protected void TxBuscarAgencia_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cargarDataAgencias();
                String vBusqueda = TxBuscarAgencia.Text;
                DataTable vDatos = (DataTable)Session["AG_CA_AGENCIAS_BASA"];
                if (vBusqueda.Equals(""))
                {
                    GVAgencias.DataSource = vDatos;
                    GVAgencias.DataBind();
                    UPAgencias.Update();
                    
                }
                else
                {
                    EnumerableRowCollection<DataRow> filtered = vDatos.AsEnumerable()
                        . Where(r => r.Field<String>("nombre").Contains(vBusqueda));

                    Boolean isNumeric = int.TryParse(vBusqueda, out int n);
                    if (isNumeric)
                    {
                        if (filtered.Count() == 0)
                        {
                            filtered = vDatos.AsEnumerable().Where(r =>
                                Convert.ToInt32(r["codigoAgencia"]) == Convert.ToInt32(vBusqueda));
                        }
                    }

                    DataTable vDatosFiltrados = new DataTable();
                    vDatosFiltrados.Columns.Add("idAgencia");
                    vDatosFiltrados.Columns.Add("nombre");
                    vDatosFiltrados.Columns.Add("codigoAgencia");
                    vDatosFiltrados.Columns.Add("direccion");
                    vDatosFiltrados.Columns.Add("telefono");
                    vDatosFiltrados.Columns.Add("TipoAgencia");
                    vDatosFiltrados.Columns.Add("departamento");
                    vDatosFiltrados.Columns.Add("estado");

                    foreach (DataRow item in filtered)
                    {
                        vDatosFiltrados.Rows.Add(
                            item["idAgencia"].ToString(),
                            item["nombre"].ToString(),
                            item["codigoAgencia"].ToString(),
                            item["direccion"].ToString(),
                            item["telefono"].ToString(),
                            item["TipoAgencia"].ToString(),
                            item["departamento"].ToString(),
                            item["estado"].ToString()
                            );
                    }

                    GVAgencias.DataSource  = vDatosFiltrados;
                    GVAgencias.DataBind();
                    Session["AG_CA_AGENCIAS_BASA"] = vDatosFiltrados;
                    UPAgencias.Update();
                }

            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
        
        protected void GVAgencias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Modifcar")
            {

                DivAlerta.Visible = false;
                UpdateModal.Update();

                string vIdAgenciaModificar = e.CommandArgument.ToString();
                Session["AG_CA_ID_AGENCIA_MODIFICAR"] = vIdAgenciaModificar;

                try
                {
                    String vQuery2 = " STEISP_AGENCIA_CreacionAgencia 5," + vIdAgenciaModificar;
                    DataTable vDatos = vConexion.obtenerDataTable(vQuery2);
                    Int32 RbConductorModificarConverido = Convert.ToInt32(vDatos.Rows[0]["requiereConductor"]);
                    Int32 DDLEstadoConvertido = Convert.ToInt32(vDatos.Rows[0]["estado"]);

                    TxCodigoModificar.Text= vDatos.Rows[0]["codigoAgencia"].ToString();
                    TxAgenciaModificar.Text = vDatos.Rows[0]["nombre"].ToString();
                    TxDireccionModificar.Text = vDatos.Rows[0]["direccion"].ToString();
                    TxTelefonoModificar.Text = vDatos.Rows[0]["telefono"].ToString();
                    TxLatitudModificar.Text = vDatos.Rows[0]["lat"].ToString();
                    TxLongitudModificar.Text = vDatos.Rows[0]["lng"].ToString();
                    DDLTipoAgenciaModificar.SelectedValue= vDatos.Rows[0]["idTipoAgencia"].ToString();
                    DDLDepartamentoModificar.SelectedValue= vDatos.Rows[0]["idDepartamento"].ToString();
                    Session["AG_CA_ID_DEPARTAMENTO"] = vDatos.Rows[0]["idDepartamento"].ToString();

                    DDLMunicipioModificar.SelectedValue = vDatos.Rows[0]["municipio"].ToString();
                    txtcodUbicacion.Text= vDatos.Rows[0]["codigoUbicacion"].ToString();
                    Session["AG_CA_CODIGO_UBICACION"] = vDatos.Rows[0]["codigoUbicacion"].ToString();

                    Session["AG_CA_ID_UBICACION"] = vDatos.Rows[0]["idUbicacion"].ToString();

                    DDLEstado.SelectedValue = DDLEstadoConvertido.ToString();
                    RbConductorModificar.SelectedValue= RbConductorModificarConverido.ToString();

                    TituloModalCrearAgencia.Text = "Modificar Agencia "+ TxAgenciaModificar.Text;
                    UpdatePanel3.Update();

                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModalModificarAgencia();", true);
                }
                catch (Exception ex)
                {
                    Mensaje(ex.Message, WarningType.Danger);
                }
            }
        }
        
        private void validarModificar()
        {
            if (TxAgenciaModificar.Text == "" || TxAgenciaModificar.Text == string.Empty)
                throw new Exception("Falta ingresar el nombre de la agencia.");

            if (TxDireccionModificar.Text == "" || TxDireccionModificar.Text == string.Empty)
                throw new Exception("Falta ingresar la dirección de la agencia.");

            //if (TxTelefonoModificar.Text == "" || TxTelefonoModificar.Text == string.Empty)
            //    throw new Exception("Falta ingresar telefonos de la agencia.");

            if (TxLatitudModificar.Text == "" || TxLatitudModificar.Text == string.Empty)
                throw new Exception("Falta ingresar latitud de la agencia.");

            if (TxLongitudModificar.Text == "" || TxLongitudModificar.Text == string.Empty)
                throw new Exception("Falta ingresar longitud de la agencia.");

            if (DDLTipoAgenciaModificar.SelectedValue.Equals("0"))
                throw new Exception("Seleccione un tipo de agencia valido.");

            if (DDLDepartamentoModificar.SelectedValue.Equals("0"))
                throw new Exception("Seleccione un departamento agencia valido.");

            if (DDLMunicipioModificar.SelectedValue.Equals("0"))
                throw new Exception("Seleccione un municipio valido.");

            if (RbConductorModificar.SelectedValue.Equals(""))
                throw new Exception("Falta completar opción ¿Si requiere de conductorn para el traslado hacia la agencia?.");
        }
        
        private void limpiarDivAlertaModal()
        {
            DivAlerta.Visible = false;
            UpdateModal.Visible = false;
            UpdateModal.Update();
        }

        protected void btnModalModificar_Click(object sender, EventArgs e)
        {
            try
            {
                DivAlerta.Visible = false;
                UpdateModal.Update();
                validarModificar();
                String vQuery1 = "STEISP_AGENCIA_CreacionAgencia 6,'"
                                  + Session["AG_CA_ID_AGENCIA_MODIFICAR"] +
                                  "','" + TxAgenciaModificar.Text +
                                  "','" + TxDireccionModificar.Text +
                                  "','" + TxTelefonoModificar.Text +
                                  "'," + DDLTipoAgenciaModificar.SelectedValue +
                                  "," + DDLDepartamentoModificar.SelectedValue +
                                  ",'" + TxLatitudModificar.Text +
                                  "','" + TxLongitudModificar.Text +                             
                                  "'," + RbConductorModificar.SelectedValue +
                                  "," + DDLEstado.SelectedValue +
                                  ",'"+ Session["USUARIO"]+
                                  "',"+ DDLMunicipioModificar.SelectedValue+
                                   ",'"+ txtcodUbicacion.Text+"'";
                Int32 vInformacion1 = vConexion.ejecutarSql(vQuery1);

                String vQuery2 = "STEISP_INVENTARIO_Ubicaciones 4,"
                                  + Session["AG_CA_ID_UBICACION"].ToString() +
                                  ",6," + DDLMunicipioModificar.SelectedValue +
                                   ",'" + txtcodUbicacion.Text +
                                   "','" + TxDireccionModificar.Text +
                                   "'," + DDLEstado.SelectedValue +
                                   ",'" + Session["USUARIO"] +
                                   "','"+ TxAgencia.Text + "'";
                
                Int32 vInformacion2 = vConexion.ejecutarSql(vQuery2);



                if (vInformacion1 == 1 && vInformacion2 == 1)
                {
                    SmtpService vService = new SmtpService();

                    Mensaje("Datos actualizados de agencia con exito. ", WarningType.Success);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModalModificarAgencia();", true);
                    cargarDataAgencias();   
                    UPAgencias.Update();

                    //string motivo = "Se notifica que se ha modificado co éxito la agencia"+ txtcodUbicacion.Text + " ,ubicada:"+ TxDireccionModificar.Text;
                    //string vCorreo = "acamador@bancatlan.hn";
                    //string vNombre = "Ana Carolina Amador Mejia";                  
                    //    vService.EnviarMensaje(vCorreo,
                    //        typeBody.EnvioCorreo,
                    //        vNombre,
                    //        "Motivo: " + motivo
                    //        );
                    

                    }
            }
            catch (Exception ex)
            {
                LbMensajeModalError.Text = ex.Message;
                DivAlerta.Visible = true;
                UpdateModal.Visible = true;
                UpdateModal.Update();
            }

        }
        
        protected void DDLDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (DDLDepartamento.SelectedValue.Equals("8"))
                {
                    RblConductor.Enabled= true;
                    RblConductor.SelectedIndex = -1;
                }
                else
                {
                    RblConductor.Enabled = false;
                    RblConductor.SelectedValue = "0";
                }
                cargarMunicipios(DDLDepartamento.SelectedValue);
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }

        }
        
        private void cargarMunicipios(String vIdDepto)
        {
            if (vIdDepto != "0")
            {
                String vQuery = "STEISP_INVENTARIO_Generales 2," + DDLDepartamento.SelectedValue;
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0)
                {
                    DDLMunicipio.Items.Clear();
                    DDLMunicipio.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLMunicipio.Items.Add(new ListItem { Value = item["idMunicipio"].ToString(), Text = item["nombre"].ToString() });
                    }
                }
            }
            else
                DDLMunicipio.Items.Clear();
        }
        
        protected void DDLDepartamentoModificar_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (DDLDepartamentoModificar.SelectedValue.Equals("8"))
            {
                RbConductorModificar.Enabled = true;
                RbConductorModificar.SelectedIndex = -1;
            }
            else
            {
                RbConductorModificar.Enabled = false;
                RbConductorModificar.SelectedValue = "0";
            }

            String vQuery = "STEISP_INVENTARIO_Generales 2," + DDLDepartamentoModificar.SelectedValue;
            DataTable vDatos = vConexion.obtenerDataTable(vQuery);

            if (vDatos.Rows.Count > 0)
            {
                DDLMunicipioModificar.Items.Clear();
                DDLMunicipioModificar.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                foreach (DataRow item in vDatos.Rows)
                {
                    DDLMunicipioModificar.Items.Add(new ListItem { Value = item["idMunicipio"].ToString(), Text = item["nombre"].ToString() });
                }
            }


            string codigoUbicacion = "";
            String vQuery2 = "STEISP_INVENTARIO_Ubicaciones 6," + DDLDepartamentoModificar.SelectedValue;
            DataTable vDato2 = vConexion.obtenerDataTable(vQuery2);
            foreach (DataRow item in vDato2.Rows)
            {
                codigoUbicacion = item["codigo"].ToString();
              
            }

            if(Session["AG_CA_ID_DEPARTAMENTO"].ToString()== DDLDepartamentoModificar.SelectedValue)
            { txtcodUbicacion.Text = Session["AG_CA_CODIGO_UBICACION"].ToString();}
            else { txtcodUbicacion.Text = codigoUbicacion;
            }
        }

        protected void TxAgenciaModificar_TextChanged(object sender, EventArgs e)
        {
            limpiarDivAlertaModal();
        }
    }
}