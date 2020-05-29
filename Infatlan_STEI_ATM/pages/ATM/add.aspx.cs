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

namespace Infatlan_STEI_ATM.pages.ATM
{
    public partial class add : System.Web.UI.Page
    {
        bd vConexion = new bd();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["ATM_ADD"] = null;
            if (!Page.IsPostBack)
            {
                //Limpiar();
                cargarDataATM();
            }
        }

        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }
        
        void cargarDataATM(){
            
            if (HttpContext.Current.Session["ATM_ADD"] == null)
            { 
                try
                {              
               
                    String vQuery = "STEISP_ATM_Generales 12,1";
                DataTable vDatos = vConexion.ObtenerTabla(vQuery);
                DDLsucursalATM.Items.Add(new ListItem { Value = "0", Text = "Seleccione sucursal..." });               
                foreach (DataRow item in vDatos.Rows){
                    DDLsucursalATM.Items.Add(new ListItem { Value = item["idMunicipio"].ToString(), Text = item["nombre"].ToString() });                   
               
                }
            }
            catch (Exception Ex){
                throw;
            }
            //UBICACIONES
            try
            {
               
                    String vQuery = "STEISP_ATM_Generales 7,1";
                DataTable vDatos = vConexion.ObtenerTabla(vQuery);
                DDLUbicacionATM.Items.Add(new ListItem { Value = "0", Text = "Seleccione ubicación..." });
                foreach (DataRow item in vDatos.Rows){
                    DDLUbicacionATM.Items.Add(new ListItem { Value = item["idUbicacion"].ToString(), Text = item["nombreUbicacion"].ToString() });
               
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            //TIPO ATM
            try
            {
               
                    String vQuery = "STEISP_ATM_Generales 1,1";
                DataTable vDatos = vConexion.ObtenerTabla(vQuery);
                DDLTipoATM.Items.Add(new ListItem { Value = "0", Text = "Seleccione tipo de ATM..." });
                foreach (DataRow item in vDatos.Rows)
                {
                    DDLTipoATM.Items.Add(new ListItem { Value = item["idTipoATM"].ToString(), Text = item["nombreTipoATM"].ToString() });
               
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            //MODELO ATM
            try
            {
                
                    String vQuery = "STEISP_ATM_Generales 2,1";
                    DataTable vDatos = vConexion.ObtenerTabla(vQuery);
                    DDLModeloATM.Items.Add(new ListItem { Value = "0", Text = "Seleccione modelo..." });
                    foreach (DataRow item in vDatos.Rows){
                        DDLModeloATM.Items.Add(new ListItem { Value = item["idModeloATM"].ToString(), Text = item["nombreModeloATM"].ToString() });
               
                }
            }catch (Exception ex){
                throw;
            }

            //TIPO CARGA ATM
            try
            {
               
                    String vQuery = "STEISP_ATM_Generales 4,1";
                    DataTable vDatos = vConexion.ObtenerTabla(vQuery);
                    DDLTipoCarga.Items.Add(new ListItem { Value = "0", Text = "Seleccione tipo de carga..." });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLTipoCarga.Items.Add(new ListItem { Value = item["idTipoCargaATM"].ToString(), Text = item["nombreTipoCargaATM"].ToString() });
               
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            //PROCESADOR ATM
            try
            {
               
                    String vQuery = "STEISP_ATM_Generales 5,1";
                    DataTable vDatos = vConexion.ObtenerTabla(vQuery);
                    DDLProcesadorATM.Items.Add(new ListItem { Value = "0", Text = "Seleccione tipo de procesador..." });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLProcesadorATM.Items.Add(new ListItem { Value = item["idProcesadorATM"].ToString(), Text = item["nombreProcesadorATM"].ToString() });
               
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            

            //TECLADO ATM
            try
            {
               
                    String vQuery = "STEISP_ATM_Generales 6,1";
                    DataTable vDatos = vConexion.ObtenerTabla(vQuery);
                    DDLTecladoATM.Items.Add(new ListItem { Value = "0", Text = "Seleccione tipo de teclado..." });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLTecladoATM.Items.Add(new ListItem { Value = item["idTecladoATM"].ToString(), Text = item["nombreTecladoATM"].ToString() });
                   
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            //SISTEMA OPERATIVO
            try
            {
                
                    String vQuery = "STEISP_ATM_Generales 9,1";
                    DataTable vDatos = vConexion.ObtenerTabla(vQuery);
                    DDLso.Items.Add(new ListItem { Value = "0", Text = "Seleccione sistema operativo..." });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLso.Items.Add(new ListItem { Value = item["idSO"].ToString(), Text = item["nombreSO"].ToString() });
                    }
                
            }
            catch (Exception ex)
            {
                throw;
            }

            //VERSION SO
            try
            {
               
                    String vQuery = "STEISP_ATM_Generales 10,1";
                    DataTable vDatos = vConexion.ObtenerTabla(vQuery);
                    DDLversionSw.Items.Add(new ListItem { Value = "0", Text = "Seleccione version del software..." });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLversionSw.Items.Add(new ListItem { Value = item["idVersion"].ToString(), Text = item["nombreVersion"].ToString() });
                    }
               
            }
            catch (Exception ex)
            {
                throw;
            }

            //MARCAS
            try
            {
                
                    String vQuery = "STEISP_ATM_Generales 11,1";
                    DataTable vDatos = vConexion.ObtenerTabla(vQuery);
                    DDLmarca.Items.Add(new ListItem { Value = "0", Text = "Seleccione la marca..." });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLmarca.Items.Add(new ListItem { Value = item["idMarca"].ToString(), Text = item["nombreMarca"].ToString() });
                    }
               
            }
            catch (Exception ex)
            {
                throw;
            }

            //ESTADOS
            try
            {
                
                    String vQuery = "STEISP_ATM_Generales 8,1";
                    DataTable vDatos = vConexion.ObtenerTabla(vQuery);
                    DDLestado.Items.Add(new ListItem { Value = "0", Text = "Seleccione estado de ATM..." });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLestado.Items.Add(new ListItem { Value = item["idEstado"].ToString(), Text = item["nombreEstado"].ToString() });
                    }
                
            }
            catch (Exception ex)
            {
                throw;
            }
                Session["ATM_ADD"] = "1";
                }
            }

        protected void DDLModeloATM_SelectedIndexChanged(object sender, EventArgs e){
        //DETALLE MODELO ATM
        try{
            String vQuery = "STEISP_ATM_Generales 3," + DDLModeloATM.SelectedValue + "";
            DataTable vDatos = vConexion.ObtenerTabla(vQuery);
            DDLDetalleModelo.Items.Clear();
            DDLDetalleModelo.Items.Add(new ListItem { Value = "0", Text = "Seleccione detalle de modelo..." });
            foreach (DataRow item in vDatos.Rows){
                DDLDetalleModelo.Items.Add(new ListItem { Value = item["idDetalleModeloATM"].ToString(), Text = item["nombreDetalleModeloATM"].ToString() });
            }
        }catch (Exception ex){
            throw;
        }
    }

        void Limpiar(){
            Session["ATM_ADD"] = null;
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

        protected void btnguardarATM_Click(object sender, EventArgs e){
            try{
                validarFromulario();
                lbcodATM.Text = txtcodATM.Text;
                lbNombreATM.Text = txtnombreATM.Text;
                lbsucursalATM.Text = DDLsucursalATM.SelectedItem.Text;
                lbTipoATM.Text = DDLTipoATM.SelectedItem.Text;
                lbInventarioATM.Text = txtinventarioATM.Text;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        private void validarFromulario() {
            

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
            if (txtserieDisco.Text=="")
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

        protected void btnModalCerrarNotificacion_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
        }

        protected void btnModalEnviarNotificacion_Click(object sender, EventArgs e){
            
            try
            {
                string vQuery = "STEISP_ATM_CrearATM 1, '" + txtcodATM.Text + "','" + txtnombreATM.Text + "'," +
                    "" + DDLestado.SelectedValue + "," + DDLsucursalATM.SelectedValue + "," +
                    "" + DDLUbicacionATM.SelectedValue + "," + DDLTipoATM.SelectedValue + "," +
                    "" + DDLDetalleModelo.SelectedValue + ", " + DDLTipoCarga.SelectedValue + "," +
                    "" + DDLProcesadorATM.SelectedValue + "," + DDLTecladoATM.SelectedValue + "," +
                    "'" + txtserieATM.Text + "','" + txtramATM.Text + " GB" + "', " + DDLso.SelectedValue + "," +
                    "'" + txtserieDisco.Text + "','" + txtcapacidadDisco.Text + " GB" + "'," + DDLmarca.SelectedValue + "," +
                    "'" + txtIP.Text + "','" + txtpuerto.Text + "','" + txtlatitud.Text + "','" + txtlongitud.Text + "'," +
                    "'" + txtdireccion.Text + "','" + Session["usuATM"].ToString() + "','"+txtinventarioATM.Text+"', '"+ DDLversionSw.SelectedValue +"','"+txtcodUbicacion.Text+"'";
                Int32 vInfo = vConexion.ejecutarSQL(vQuery);

                //VALIDA QUE ATM ESTE ACTIVO
                String vQuery2 = "STEISP_ATM_VERIFICACION 9, '" + txtcodATM.Text + "',1";
                DataTable vDatos2 = vConexion.ObtenerTabla(vQuery2);

                //INGRESAR UBICACIONES
                string IDUbi = "5";
                String vQuery3 = "STEISP_INVENTARIO_Ubicaciones 3, '"+IDUbi+"', '" + DDLsucursalATM.SelectedValue + "','"+txtcodUbicacion.Text+"','"+txtdireccion.Text+"','" + Session["usuATM"].ToString() + "','"+txtnombreATM.Text+"'";
                DataTable vDatos3 = vConexion.ObtenerTabla(vQuery3);

                if (vInfo == 1){
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
                    Limpiar();
                    cargarDataATM();
                    
                    
                    Mensaje("ATM creada con éxito", WarningType.Success);
                    
                }else{
                    Mensaje("No se pudo crear el ATM", WarningType.Warning);
                }
            }
            catch (Exception Ex)
            {
                throw;
            }
        }

        protected void DDLsucursalATM_TextChanged(object sender, EventArgs e)
        {
            string idDepa = "";
            txtcodUbicacion.Text = string.Empty;
            try
            {
                String vQuery2 = "STEISP_ATM_Generales 27, '" + DDLsucursalATM.SelectedValue + "'";
                DataTable vDatos2 = vConexion.ObtenerTabla(vQuery2);
                foreach (DataRow item in vDatos2.Rows)
                {
                   idDepa  = item["idDepartamento"].ToString();
                }
                String vQuery = "STEISP_INVENTARIO_Ubicaciones 6, '" + idDepa + "'";
                DataTable vDatos = vConexion.ObtenerTabla(vQuery);
                foreach (DataRow item in vDatos.Rows)
                {
                    txtcodUbicacion.Text = item["codigo"].ToString();
                }
                
            }
            catch (Exception Ex)
            {

                throw;
            }
        }
    }
}