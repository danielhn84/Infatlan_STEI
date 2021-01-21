using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using Infatlan_STEI_ATM.clases;

namespace Infatlan_STEI_ATM.pages.ATM
{
    public partial class updateTotal : System.Web.UI.Page
    {
        bd vConexion = new bd();
        bd vConexionATM = new bd();
        protected void Page_Load(object sender, EventArgs e){
            
            if (!Page.IsPostBack){
                if (Convert.ToBoolean(Session["AUTH"])){
                    Session["ATM"] = null;
                    //Session["codATM"] = null;
                    cargarDataATM();
                    llenarForm();
                }else {
                    Response.Redirect("/login.aspx");
                }
            }
        }

        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        void llenarForm()
        {
            try
            {
                DataTable vDatos = new DataTable();
                String vQuery = "SPSTEI_ATM 1,'" + Session["codATM"] + "'";
                vDatos = vConexionATM.ObtenerTablaATM(vQuery);
                foreach (DataRow item in vDatos.Rows)
                {
                    txtcodATM.Text = item["codATM"].ToString();
                    txtnombreATM.Text = item["nombreATM"].ToString();
                    DDLestado.SelectedIndex = CargarInformacionDDL(DDLestado, item["idEstado"].ToString());
                    DDLsucursalATM.SelectedIndex = CargarInformacionDDL(DDLsucursalATM, item["sucursalATM"].ToString());
                    DDLUbicacionATM.SelectedIndex = CargarInformacionDDL(DDLUbicacionATM, item["ubicacionATM"].ToString());
                    DDLTipoATM.SelectedIndex = CargarInformacionDDL(DDLTipoATM, item["tipoATM"].ToString());
                    DDLModeloATM.SelectedIndex = CargarInformacionDDL(DDLModeloATM, item["ModeloATM"].ToString());
                    DDLTipoCarga.SelectedIndex = CargarInformacionDDL(DDLTipoCarga, item["tipoCargaATM"].ToString());
                    DDLProcesadorATM.SelectedIndex = CargarInformacionDDL(DDLProcesadorATM, item["procesadorATM"].ToString());
                    DDLTecladoATM.SelectedIndex = CargarInformacionDDL(DDLTecladoATM, item["tecladoATM"].ToString());
                    txtserieATM.Text = item["serieATM"].ToString();

                    string MyString = item["ramATM"].ToString();
                    char[] MyChar = { 'G', 'B', ' ' };
                    string NewString = MyString.TrimEnd(MyChar);
                    txtramATM.Text = NewString;

                    string capacidad = item["capacidadDiscoATM"].ToString();
                    char[] MyChar1 = { 'G', 'B', ' ' };
                    string Newcapacidad = capacidad.TrimEnd(MyChar1);
                    txtcapacidadDisco.Text = Newcapacidad;

                    DDLso.SelectedIndex = CargarInformacionDDL(DDLso, item["idSO"].ToString());
                    txtserieDisco.Text = item["serieDiscoATM"].ToString();

                    DDLmarca.SelectedIndex = CargarInformacionDDL(DDLmarca, item["idMarca"].ToString());
                    txtIP.Text = item["ipATM"].ToString();
                    txtpuerto.Text = item["puertoATM"].ToString();
                    txtlatitud.Text = item["latitudATM"].ToString();
                    txtlongitud.Text = item["longitudATM"].ToString();
                    txtdireccion.Text = item["direccionATM"].ToString();
                    txtinventarioATM.Text = item["inventarioATM"].ToString();
                    DDLversionSw.SelectedIndex = CargarInformacionDDL(DDLversionSw, item["idVersionSw"].ToString());
                    DDLUbicacionATM.SelectedIndex = CargarInformacionDDL(DDLUbicacionATM, item["ubicacionATM"].ToString());
                    txtcodUbicacion.Text = item["codUbicacion"].ToString();
                    

                    String vModelo = "";
                    String vQueryModelo = "STEISP_ATM_Generales 37,'" + item["modeloATM"].ToString() + "'";
                    DataTable vDatosModelo = vConexion.ObtenerTabla(vQueryModelo);
                    foreach (DataRow itemMod in vDatosModelo.Rows)
                    {
                        vModelo= itemMod["idModeloATM"].ToString();
                    }
                    DDLModeloATM.SelectedIndex = CargarInformacionDDL(DDLModeloATM, vModelo);

                    //String vQueryDet = "STEISP_ATM_Generales 3,'" + DDLModeloATM.SelectedValue + "'";
                    //    DataTable vDatosDet = vConexion.ObtenerTabla(vQueryDet);
                    //    DDLDetalleModelo.Items.Clear();
                    //    DDLDetalleModelo.Items.Add(new ListItem { Value = "0", Text = "Seleccione detalle de modelo..." });
                    //    foreach (DataRow itemDet in vDatosDet.Rows)
                    //    {
                    //        DDLDetalleModelo.Items.Add(new ListItem { Value = itemDet["idDetalleModeloATM"].ToString(), Text = itemDet["nombreDetalleModeloATM"].ToString() });
                    //    }
                    
                    //DDLDetalleModelo.SelectedIndex = CargarInformacionDDL(DDLDetalleModelo, item["modeloATM"].ToString());

                    
                }
                limpiarImagen();

                DataTable vDatos2 = new DataTable();
                String vQuery2 = "SPSTEI_ATM 33,'" + Session["codATM"] + "'";
                vDatos2 = vConexionATM.ObtenerTablaATM(vQuery2);
                foreach (DataRow item in vDatos2.Rows)
                {
                    string vImagen1 = item["imgMapaATM"].ToString();
                    if (vImagen1 != "")
                    {
                        string srcImgen1 = "data:image;base64," + vImagen1;
                        imgMapaATM.Src = srcImgen1;
                        HFMapa.Value = "si";
                    }
                }

            }
            catch (Exception Ex)
            {

                Mensaje(Ex.Message, WarningType.Danger);
            }

        }

        int CargarInformacionDDL(DropDownList vList, String vValue)
        {
            int vIndex = 0;
            try
            {
                int vContador = 0;
                foreach (ListItem item in vList.Items)
                {
                    if (item.Value.Equals(vValue))
                    {
                        vIndex = vContador;
                    }
                    vContador++;
                }
            }
            catch { throw; }
            return vIndex;
        }
        void Limpiar()
        {
            Session["ATM"] = null;
            txtcodATM.Text = string.Empty;
            DDLsucursalATM.Items.Clear();
            DDLUbicacionATM.Items.Clear();
            DDLTipoATM.Items.Clear();
            DDLModeloATM.Items.Clear();
            //DDLDetalleModelo.Items.Clear();
            DDLTipoCarga.Items.Clear();
            DDLProcesadorATM.Items.Clear();
            DDLTecladoATM.Items.Clear();
            txtserieATM.Text = string.Empty;
            txtramATM.Text = string.Empty;
            txtserieDisco.Text = string.Empty;
            DDLso.Items.Clear();
            DDLversionSw.Items.Clear();
            DDLmarca.Items.Clear();
            txtcapacidadDisco.Text = string.Empty;
            txtnombreATM.Text = string.Empty;
            txtIP.Text = string.Empty;
            txtpuerto.Text = string.Empty;
            txtlatitud.Text = string.Empty;
            txtlongitud.Text = string.Empty;
            txtdireccion.Text = string.Empty;
            DDLestado.Items.Clear();
            txtinventarioATM.Text = string.Empty;

            //UPSegFila.Update();
            UPtotalATM.Update();
        }

        void cargarDataATM()
        {
            //DDLsucursalATM.Items.Clear();
            //DDLUbicacionATM.Items.Clear();
            //DDLTipoATM.Items.Clear();
            // DDLModeloATM.Items.Clear();
            //SUCURSALES
            if (HttpContext.Current.Session["ATM"] == null)
            {
                try
                {
                    //if (HttpContext.Current.Session["SUCURSAL"] == null)
                    //{
                    String vQuery = "STEISP_ATM_Generales 12,1";
                    DataTable vDatos = vConexion.ObtenerTabla(vQuery);
                    DDLsucursalATM.Items.Add(new ListItem { Value = "0", Text = "Seleccione sucursal..." });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLsucursalATM.Items.Add(new ListItem { Value = item["idMunicipio"].ToString(), Text = item["nombre"].ToString() });
                        //}
                        //    Session["SUCURSAL"] = "1";
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
                //UBICACIONES
                try
                {
                    //if (HttpContext.Current.Session["UBICACION"] == null)
                    //{
                    String vQuery = "STEISP_ATM_Generales 7,1";
                    DataTable vDatos = vConexion.ObtenerTabla(vQuery);
                    DDLUbicacionATM.Items.Add(new ListItem { Value = "0", Text = "Seleccione ubicación..." });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLUbicacionATM.Items.Add(new ListItem { Value = item["idUbicacion"].ToString(), Text = item["nombreUbicacion"].ToString() });
                        //}
                        //    Session["UBICACION"] = "1";
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
                //TIPO ATM
                try
                {
                    //if (HttpContext.Current.Session["TIPO"] == null)
                    //{
                    String vQuery = "SPSTEI_ATM 4";
                    DataTable vDatos = vConexionATM.ObtenerTablaATM(vQuery);
                    DDLTipoATM.Items.Add(new ListItem { Value = "0", Text = "Seleccione tipo de ATM..." });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLTipoATM.Items.Add(new ListItem { Value = item["Id_Tipo_ATM"].ToString(), Text = item["Descripcion"].ToString() });
                        //}
                        //    Session["TIPO"] = "1";
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
                //MODELO ATM
                try
                {
                    //if (HttpContext.Current.Session["MODELO"] == null){
                    String vQuery = "SPSTEI_ATM 10";
                    DataTable vDatos = vConexionATM.ObtenerTablaATM(vQuery);
                    DDLModeloATM.Items.Add(new ListItem { Value = "0", Text = "Seleccione modelo..." });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLModeloATM.Items.Add(new ListItem { Value = item["Id_Modelo_ATM"].ToString(), Text = item["Descripcion"].ToString() });
                        //    }
                        //    Session["MODELO"] = "1";
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }

                //TIPO CARGA ATM
                try
                {
                    //if (HttpContext.Current.Session["CARGA"] == null)
                    //{
                    String vQuery = "SPSTEI_ATM 5";
                    DataTable vDatos = vConexionATM.ObtenerTablaATM(vQuery);
                    DDLTipoCarga.Items.Add(new ListItem { Value = "0", Text = "Seleccione tipo de carga..." });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLTipoCarga.Items.Add(new ListItem { Value = item["Id_Carga_ATM"].ToString(), Text = item["Descripcion"].ToString() });
                        //    }
                        //    Session["CARGA"] = "1";
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }

                //PROCESADOR ATM
                try
                {
                    //if (HttpContext.Current.Session["PROCESADOR"] == null)
                    //{
                    String vQuery = "SPSTEI_ATM 6";
                    DataTable vDatos = vConexionATM.ObtenerTablaATM(vQuery);
                    DDLProcesadorATM.Items.Add(new ListItem { Value = "0", Text = "Seleccione tipo de procesador..." });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLProcesadorATM.Items.Add(new ListItem { Value = item["Id_Procesador_ATM"].ToString(), Text = item["Descripcion"].ToString() });
                        //    }
                        //    Session["PROCESADOR"] = "1";
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }



                //TECLADO ATM
                try
                {
                    //if (HttpContext.Current.Session["TECLADO"] == null)
                    //{
                    String vQuery = "SPSTEI_ATM 7";
                    DataTable vDatos = vConexionATM.ObtenerTablaATM(vQuery);
                    DDLTecladoATM.Items.Add(new ListItem { Value = "0", Text = "Seleccione tipo de teclado..." });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLTecladoATM.Items.Add(new ListItem { Value = item["Id_Teclado_ATM"].ToString(), Text = item["Descripcion"].ToString() });
                        //}
                        //Session["TECLADO"] = "1";
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }

                //SISTEMA OPERATIVO
                try
                {
                    //if (HttpContext.Current.Session["SO"] == null)
                    //{
                    String vQuery = "STEISP_ATM_Generales 9,1";
                    DataTable vDatos = vConexion.ObtenerTabla(vQuery);
                    DDLso.Items.Add(new ListItem { Value = "0", Text = "Seleccione sistema operativo..." });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLso.Items.Add(new ListItem { Value = item["idSO"].ToString(), Text = item["nombreSO"].ToString() });
                    }
                    //    Session["SO"] = "1";
                    //}
                }
                catch (Exception ex)
                {
                    throw;
                }

                //VERSION SO
                try
                {
                    //if (HttpContext.Current.Session["VERSO"] == null)
                    //{
                    String vQuery = "SPSTEI_ATM 8";
                    DataTable vDatos = vConexionATM.ObtenerTablaATM(vQuery);
                    DDLversionSw.Items.Add(new ListItem { Value = "0", Text = "Seleccione version del software..." });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLversionSw.Items.Add(new ListItem { Value = item["Id_Software_ATM"].ToString(), Text = item["Descripcion"].ToString() });
                    }
                    //    Session["VERSO"] = "1";
                    //}
                }
                catch (Exception ex)
                {
                    throw;
                }

                //MARCAS
                try
                {
                    //if (HttpContext.Current.Session["MARCA"] == null)
                    //{
                    String vQuery = "SPSTEI_ATM 11";
                    DataTable vDatos = vConexionATM.ObtenerTablaATM(vQuery);
                    DDLmarca.Items.Add(new ListItem { Value = "0", Text = "Seleccione la marca..." });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLmarca.Items.Add(new ListItem { Value = item["Id_Disco_ATM"].ToString(), Text = item["Descripcion"].ToString() });
                    }
                    //    Session["MARCA"] = "1";
                    //}
                }
                catch (Exception ex)
                {
                    throw;
                }

                //ESTADOS
                try
                {
                    //if (HttpContext.Current.Session["ESTADO"] == null)
                    //{
                    String vQuery = "SPSTEI_ATM 9";
                    DataTable vDatos = vConexionATM.ObtenerTablaATM(vQuery);
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLestado.Items.Add(new ListItem { Value = item["Id_Estatus_ATM"].ToString(), Text = item["Descripcion"].ToString() });
                    }
                    //    Session["ESTADO"] = "1";
                    //}
                }
                catch (Exception ex)
                {
                    throw;
                }
                Session["ATM"] = "1";
            }
        }

        protected void DDLModeloATM_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //String vQuery = "STEISP_ATM_Generales 3," + DDLModeloATM.SelectedValue + "";
                //DataTable vDatos = vConexion.ObtenerTabla(vQuery);
                //DDLDetalleModelo.Items.Clear();
                //DDLDetalleModelo.Items.Add(new ListItem { Value = "0", Text = "Seleccione detalle de modelo..." });
                //foreach (DataRow item in vDatos.Rows)
                //{
                //    DDLDetalleModelo.Items.Add(new ListItem { Value = item["idDetalleModeloATM"].ToString(), Text = item["nombreDetalleModeloATM"].ToString() });
                //}
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private void validarFromulario()
        {          
            if (txtcodATM.Text == "" || txtcodATM.Text == string.Empty)
                throw new Exception("Favor ingrese código del ATM.");
            if (DDLsucursalATM.SelectedValue == "0")
                throw new Exception("Favor seleccione surcursal.");
            if (DDLUbicacionATM.SelectedValue == "0")
                throw new Exception("Favor seleccione ubicación.");
            //if (DDLTipoATM.SelectedValue == "0")
            //    throw new Exception("Favor seleccione tipo de ATM.");
            if (DDLModeloATM.SelectedValue == "0")
                throw new Exception("Favor seleccione modelo de ATM.");
            //if (DDLDetalleModelo.SelectedValue == "0")
            //    throw new Exception("Favor seleccione detalle de modelo.");
            if (DDLTipoCarga.SelectedValue == "0")
                throw new Exception("Favor seleccione tipo de carga.");
            if (DDLProcesadorATM.SelectedValue == "0")
                throw new Exception("Favor seleccione procesador.");
            if (DDLTecladoATM.SelectedValue == "0")
                throw new Exception("Favor seleccione teclado.");
            if (txtserieATM.Text == "")
                throw new Exception("Favor ingrese serie de ATM.");
            if (txtramATM.Text == "")
                throw new Exception("Favor ingrese RAM de ATM.");
            if (txtserieDisco.Text == "")
                throw new Exception("Favor ingrese serie de disco.");
            if (txtcapacidadDisco.Text == "")
                throw new Exception("Favor ingrese capacidad de disco duro.");
            if (DDLso.SelectedValue == "0")
                throw new Exception("Favor seleccione sistema operativo.");
            if (DDLversionSw.SelectedValue == "0")
                throw new Exception("Favor seleccione versión del software.");
            if (DDLmarca.SelectedValue == "0")
                throw new Exception("Favor seleccione marca.");
            //if (txtnombreATM.Text == "")
            //    throw new Exception("Favor ingrese nombre de ATM.");
            //if (txtIP.Text == "")
            //    throw new Exception("Favor ingrese IP.");
            //if (txtpuerto.Text == "")
                //throw new Exception("Favor ingrese puerto.");
            //if (DDLestado.SelectedValue == "0")
            //    throw new Exception("Favor seleccione estado de ATM.");
            if (txtlatitud.Text == "")
                throw new Exception("Favor ingrese latitud.");
            if (txtlongitud.Text == "")
                throw new Exception("Favor ingrese longitud.");
            if (txtinventarioATM.Text == "")
                throw new Exception("Favor ingrese inventario.");
            if (txtdireccion.Text == "")
                throw new Exception("Favor ingrese dirección de ATM.");
           
        }
        protected void btnModificarATM_Click(object sender, EventArgs e)
        {
            try
            {
                validarFromulario();
                lbcodATM.Text = txtcodATM.Text;
                lbNombreATM.Text = txtnombreATM.Text;
                lbsucursalATM.Text = DDLsucursalATM.SelectedItem.Text;
                lbTipoATM.Text = DDLTipoATM.SelectedItem.Text;
                lbInventarioATM.Text = txtinventarioATM.Text;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        void limpiarImagen()
        {
            HFMapa.Value = "";
        }

        protected void btnModalModificarATM_Click(object sender, EventArgs e)
        {
            
            try
            {                           

                string vQuery = "STEISP_ATM_CrearATM 1,'" + txtcodATM.Text + "'," +
                    "" + DDLsucursalATM.SelectedValue + "," +
                    "" + DDLUbicacionATM.SelectedValue + "," +
                    "" + DDLModeloATM.SelectedValue + ", " + DDLTipoCarga.SelectedValue + "," +
                    "" + DDLProcesadorATM.SelectedValue + "," + DDLTecladoATM.SelectedValue + "," +
                    "'" + txtserieATM.Text + "','" + txtramATM.Text + " GB" + "', " + DDLso.SelectedValue + "," +
                    "'" + txtserieDisco.Text + "','" + txtcapacidadDisco.Text + " GB" + "'," + DDLmarca.SelectedValue + "," +
                    "'" + txtlatitud.Text + "','" + txtlongitud.Text + "'," +
                    "'" + txtdireccion.Text + "','" + Session["USUARIO"].ToString() + "','" + txtinventarioATM.Text + "'," +
                    "'" + DDLversionSw.SelectedValue + "','" + txtcodUbicacion.Text + "'";
                Int32 vInfo = vConexionATM.ejecutarSQLATM(vQuery);
                if (vInfo == 1)
                {

                    //IMAGENES1
                    String vNombreDepot11 = String.Empty;
                    HttpPostedFile bufferDeposito11 = FUMapaATM.PostedFile;
                    byte[] vFileDeposito11 = null;
                    string vExtension11 = string.Empty;

                    if (bufferDeposito11 != null)
                    {
                        vNombreDepot11 = FUMapaATM.FileName;
                        Stream vStream11 = bufferDeposito11.InputStream;
                        BinaryReader vReader11 = new BinaryReader(vStream11);
                        vFileDeposito11 = vReader11.ReadBytes((int)vStream11.Length);
                        vExtension11 = System.IO.Path.GetExtension(FUMapaATM.FileName);
                    }
                    String vArchivo = String.Empty;
                    if (vFileDeposito11 != null)
                        vArchivo = Convert.ToBase64String(vFileDeposito11);

                    if (FUMapaATM.HasFile != false)
                    {                        
                        string vQuery2 = "SPSTEI_ATM 34, '" + txtcodATM.Text + "','" + vArchivo + "'";
                        vConexionATM.ejecutarSQLATM(vQuery2);
                    }

                    string vCodUbicacion = "";

                    String vQueryCod = "SPSTEI_ATM 1, '" + txtcodATM.Text + "'";
                    DataTable vDatosCod = vConexionATM.ObtenerTablaATM(vQueryCod);
                    foreach (DataRow item in vDatosCod.Rows)
                    {
                        vCodUbicacion = item["codUbicacion"].ToString();
                    }

                    if (vCodUbicacion == "")
                    {
                        //ACTUALIZAR UBICACIONES
                        string IDUbi = "5";
                        int vEstado = 1;
                        String vQuery3 = "STEISP_INVENTARIO_Ubicaciones 4, '" + Session["InvUbicacion"] + "','" + IDUbi + "', '" + DDLsucursalATM.SelectedValue + "','" + txtcodUbicacion.Text + "','" + txtdireccion.Text + "','" + vEstado + "','" + Session["USUARIO"].ToString() + "','" + txtnombreATM.Text + "'";
                        DataTable vDatos3 = vConexion.ObtenerTabla(vQuery3);
                    }
                    //VALIDA QUE ATM ESTE ACTIVO
                    //String vQuery2 = "STEISP_ATM_VERIFICACION 9, '" + txtcodATM.Text + "',1";
                    //DataTable vDatos2 = vConexion.ObtenerTabla(vQuery2);
                    //if (vInfo == 1)
                    //{
                    Limpiar();
                    cargarDataATM();
                    limpiarImagen();
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);                   
                    Mensaje("ATM modificado con éxito", WarningType.Success);
                    Response.Redirect("update.aspx");
                }
                else
                {
                    Mensaje("No se pudo modificar el ATM", WarningType.Warning);
                }
            }
            catch (Exception Ex)
            {
                throw;
            }
        }

        protected void btnModalCerrarModificarATM_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
        }

        protected void DDLsucursalATM_TextChanged(object sender, EventArgs e)
        {
            string idDepa = "";
            string vCodUbicacion = "";

            String vQueryCod = "SPSTEI_ATM 1, '" + Session["codATM"] + "'";
            DataTable vDatosCod = vConexionATM.ObtenerTablaATM(vQueryCod);
            foreach (DataRow item in vDatosCod.Rows)
            {
                vCodUbicacion = item["codUbicacion"].ToString();
            }

            if (vCodUbicacion == "")
            {
                try
                {
                    String vQuery2 = "STEISP_ATM_Generales 27, '" + DDLsucursalATM.SelectedValue + "'";
                    DataTable vDatos2 = vConexion.ObtenerTabla(vQuery2);
                    foreach (DataRow item in vDatos2.Rows)
                    {
                        idDepa = item["idDepartamento"].ToString();
                    }
                    String vQuery = "STEISP_INVENTARIO_Ubicaciones 6, '" + idDepa + "'";
                    DataTable vDatos = vConexion.ObtenerTabla(vQuery);
                    foreach (DataRow item in vDatos.Rows)
                    {
                        txtcodUbicacion.Text = item["codigo"].ToString();
                    }

                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}