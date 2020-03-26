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

namespace Infatlan_STEI_ATM.pagesATM
{
    public partial class updateATMTotal : System.Web.UI.Page
    {
        bd vConexion = new bd();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["ATM"] = null;
            if (!Page.IsPostBack)
            {
                //Limpiar();
                cargarDataATM();
                llenarForm();
               
            }
        }
        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        void llenarForm()
        {
            txtcodATM.Text = Session["codATM"].ToString() ;
            DDLsucursalATM.SelectedIndex = CargarInformacionDDL(DDLsucursalATM, Session["sucursalATM"].ToString());
            txtnombreATM.Text = Session["nombreATM"].ToString();
            DDLestado.SelectedIndex = CargarInformacionDDL(DDLestado, Session["idEstado"].ToString());
            DDLUbicacionATM.SelectedIndex = CargarInformacionDDL(DDLUbicacionATM, Session["ubicacionATM"].ToString());
            DDLTipoATM.SelectedIndex = CargarInformacionDDL(DDLTipoATM, Session["tipoATM"].ToString());
            DDLDetalleModelo.SelectedIndex = CargarInformacionDDL(DDLDetalleModelo, Session["DetalleModelo"].ToString());
            DDLModeloATM.SelectedIndex = CargarInformacionDDL(DDLModeloATM, Session["modeloATM"].ToString());
            
            DDLTipoCarga.SelectedIndex = CargarInformacionDDL(DDLTipoCarga, Session["tipoCarga"].ToString());
            DDLProcesadorATM.SelectedIndex = CargarInformacionDDL(DDLProcesadorATM, Session["procesador"].ToString());
            DDLTecladoATM.SelectedIndex = CargarInformacionDDL(DDLTecladoATM, Session["teclado"].ToString());
            txtserieATM.Text = Session["serieatm"].ToString();

            string MyString = Session["ram"].ToString();
            char[] MyChar = { 'G', 'B', ' ' };
            string NewString = MyString.TrimEnd(MyChar);

            txtramATM.Text = NewString;          
            DDLso.SelectedIndex = CargarInformacionDDL(DDLso, Session["so"].ToString());
            txtserieDisco.Text = Session["serieDisco"].ToString();

            string capacidad = Session["capacidadDisco"].ToString();
            char[] MyChar1 = { 'G', 'B', ' ' };
            string Newcapacidad = capacidad.TrimEnd(MyChar1);

            txtcapacidadDisco.Text = Newcapacidad;
            DDLmarca.SelectedIndex = CargarInformacionDDL(DDLmarca, Session["marca"].ToString());
            txtIP.Text = Session["ip"].ToString();
            txtpuerto.Text = Session["puerto"].ToString();
            txtlatitud.Text = Session["latitud"].ToString();
            txtlongitud.Text = Session["longitud"].ToString();
            txtdireccion.Text = Session["direccion"].ToString();
            txtinventarioATM.Text = Session["inventario"].ToString();
            DDLversionSw.SelectedIndex = CargarInformacionDDL(DDLversionSw, Session["versionSw"].ToString());
            try
            {
                String vQuery = "STEISP_ATM_Generales 3,'" + DDLModeloATM.SelectedValue + "'";
                DataTable vDatos = vConexion.ObtenerTabla(vQuery);
                DDLDetalleModelo.Items.Clear();
                DDLDetalleModelo.Items.Add(new ListItem { Value = "0", Text = "Seleccione detalle de modelo..." });
                foreach (DataRow item in vDatos.Rows)
                {
                    DDLDetalleModelo.Items.Add(new ListItem { Value = item["idDetalleModeloATM"].ToString(), Text = item["nombreDetalleModeloATM"].ToString() });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            DDLDetalleModelo.SelectedIndex = CargarInformacionDDL(DDLDetalleModelo, Session["DetalleModelo"].ToString());
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
            DDLDetalleModelo.Items.Clear();
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
                    String vQuery = "STEISP_ATM_Generales 1,1";
                    DataTable vDatos = vConexion.ObtenerTabla(vQuery);
                    DDLTipoATM.Items.Add(new ListItem { Value = "0", Text = "Seleccione tipo de ATM..." });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLTipoATM.Items.Add(new ListItem { Value = item["idTipoATM"].ToString(), Text = item["nombreTipoATM"].ToString() });
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
                    String vQuery = "STEISP_ATM_Generales 2,1";
                    DataTable vDatos = vConexion.ObtenerTabla(vQuery);
                    DDLModeloATM.Items.Add(new ListItem { Value = "0", Text = "Seleccione modelo..." });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLModeloATM.Items.Add(new ListItem { Value = item["idModeloATM"].ToString(), Text = item["nombreModeloATM"].ToString() });
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
                    String vQuery = "STEISP_ATM_Generales 4,1";
                    DataTable vDatos = vConexion.ObtenerTabla(vQuery);
                    DDLTipoCarga.Items.Add(new ListItem { Value = "0", Text = "Seleccione tipo de carga..." });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLTipoCarga.Items.Add(new ListItem { Value = item["idTipoCargaATM"].ToString(), Text = item["nombreTipoCargaATM"].ToString() });
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
                    String vQuery = "STEISP_ATM_Generales 5,1";
                    DataTable vDatos = vConexion.ObtenerTabla(vQuery);
                    DDLProcesadorATM.Items.Add(new ListItem { Value = "0", Text = "Seleccione tipo de procesador..." });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLProcesadorATM.Items.Add(new ListItem { Value = item["idProcesadorATM"].ToString(), Text = item["nombreProcesadorATM"].ToString() });
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
                    String vQuery = "STEISP_ATM_Generales 6,1";
                    DataTable vDatos = vConexion.ObtenerTabla(vQuery);
                    DDLTecladoATM.Items.Add(new ListItem { Value = "0", Text = "Seleccione tipo de teclado..." });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLTecladoATM.Items.Add(new ListItem { Value = item["idTecladoATM"].ToString(), Text = item["nombreTecladoATM"].ToString() });
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
                    String vQuery = "STEISP_ATM_Generales 10,1";
                    DataTable vDatos = vConexion.ObtenerTabla(vQuery);
                    DDLversionSw.Items.Add(new ListItem { Value = "0", Text = "Seleccione version del software..." });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLversionSw.Items.Add(new ListItem { Value = item["idVersion"].ToString(), Text = item["nombreVersion"].ToString() });
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
                    String vQuery = "STEISP_ATM_Generales 11,1";
                    DataTable vDatos = vConexion.ObtenerTabla(vQuery);
                    DDLmarca.Items.Add(new ListItem { Value = "0", Text = "Seleccione la marca..." });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLmarca.Items.Add(new ListItem { Value = item["idMarca"].ToString(), Text = item["nombreMarca"].ToString() });
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
                    String vQuery = "STEISP_ATM_Generales 8,1";
                    DataTable vDatos = vConexion.ObtenerTabla(vQuery);
                    DDLestado.Items.Add(new ListItem { Value = "0", Text = "Seleccione estado de ATM..." });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLestado.Items.Add(new ListItem { Value = item["idEstado"].ToString(), Text = item["nombreEstado"].ToString() });
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
                String vQuery = "STEISP_ATM_Generales 3," + DDLModeloATM.SelectedValue + "";
                DataTable vDatos = vConexion.ObtenerTabla(vQuery);
                DDLDetalleModelo.Items.Clear();
                DDLDetalleModelo.Items.Add(new ListItem { Value = "0", Text = "Seleccione detalle de modelo..." });
                foreach (DataRow item in vDatos.Rows)
                {
                    DDLDetalleModelo.Items.Add(new ListItem { Value = item["idDetalleModeloATM"].ToString(), Text = item["nombreDetalleModeloATM"].ToString() });
                }
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
            if (DDLTipoATM.SelectedValue == "0")
                throw new Exception("Favor seleccione tipo de ATM.");
            if (DDLModeloATM.SelectedValue == "0")
                throw new Exception("Favor seleccione modelo de ATM.");
            if (DDLDetalleModelo.SelectedValue == "0")
                throw new Exception("Favor seleccione detalle de modelo.");
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
            if (txtnombreATM.Text == "")
                throw new Exception("Favor ingrese nombre de ATM.");
            if (txtIP.Text == "")
                throw new Exception("Favor ingrese IP.");
            if (txtpuerto.Text == "")
                throw new Exception("Favor ingrese puerto.");
            if (DDLestado.SelectedValue == "0")
                throw new Exception("Favor seleccione estado de ATM.");
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

        protected void btnModalModificarATM_Click(object sender, EventArgs e)
        {
            string usu = "acedillo";
            try
            {
                string vQuery = "STEISP_ATM_CrearATM 2, '" + txtcodATM.Text + "','" + txtnombreATM.Text + "'," +
                    "" + DDLestado.SelectedValue + "," + DDLsucursalATM.SelectedValue + "," +
                    "" + DDLUbicacionATM.SelectedValue + "," + DDLTipoATM.SelectedValue + "," +
                    "" + DDLDetalleModelo.SelectedValue + ", " + DDLTipoCarga.SelectedValue + "," +
                    "" + DDLProcesadorATM.SelectedValue + "," + DDLTecladoATM.SelectedValue + "," +
                    "'" + txtserieATM.Text + "','" + txtramATM.Text + " GB" + "', " + DDLso.SelectedValue + "," +
                    "'" + txtserieDisco.Text + "','" + txtcapacidadDisco.Text + " GB" + "'," + DDLmarca.SelectedValue + "," +
                    "'" + txtIP.Text + "','" + txtpuerto.Text + "','" + txtlatitud.Text + "','" + txtlongitud.Text + "'," +
                    "'" + txtdireccion.Text + "','" + usu + "','" + txtinventarioATM.Text + "', '" + DDLversionSw.SelectedValue + "'";
                Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                if (vInfo == 1)
                {
                    Limpiar();
                    cargarDataATM();

                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);                   
                    Mensaje("ATM modificado con éxito", WarningType.Success);
                    Response.Redirect("updateATM.aspx");
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
    }
}