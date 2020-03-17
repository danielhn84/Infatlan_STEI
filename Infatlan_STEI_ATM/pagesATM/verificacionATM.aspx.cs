using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infatlan_STEI_ATM.clases;
using System.Data;

namespace Infatlan_STEI_ATM
{
    public partial class defaultATM : System.Web.UI.Page
    {
       

       

        bd vConexion = new bd();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["VERIF"] = null;
            if (!Page.IsPostBack)
            {
                //limpiarModalVerificacion();
                CargarVerificacion();
                llenarForm();
            }
        }
        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        void llenarForm()
        {
            txtcodATM.Text = Session["ATM_CODATM_VERIF_CREAR"].ToString();
            txtnomATM.Text = Session["ATM_NOMATM_VERIF_CREAR"].ToString();
            txtdireccion.Text = Session["ATM_DIRECCION_VERIF_CREAR"].ToString();
            txtip.Text = Session["ATM_IP_VERIF_CREAR"].ToString();
            txtpuertoVerif.Text = Session["ATM_PUERTOATM_VERIF_CREAR"].ToString();
            DDLtipoTeclado.SelectedIndex= CargarInformacionDDL(DDLtipoTeclado, Session["ATM_TECLADO_VERIF_CREAR"].ToString()); 
            DDLtipoProc.SelectedIndex= CargarInformacionDDL(DDLtipoProc, Session["ATM_PROCESADOR_VERIF_CREAR"].ToString()); 
            DDLtipoCargaVerif.SelectedIndex= CargarInformacionDDL(DDLtipoCargaVerif, Session["ATM_TIPOCARGA_VERIF_CREAR"].ToString()); 
            DDLmarcaDiscoDuro.SelectedIndex= CargarInformacionDDL(DDLmarcaDiscoDuro, Session["ATM_MARCA_VERIF_CREAR"].ToString());
            txtSerieDiscoDuro.Text = Session["ATM_SERIEDISCO_VERIF_CREAR"].ToString();
            txtserieATM.Text = Session["ATM_SERIEATM_VERIF_CREAR"].ToString();
            txtcapacidadDiscoVerif.Text = Session["ATM_CAPACIDADDISCO_VERIF_CREAR"].ToString();
            txtinventarioVerif.Text = Session["ATM_INVENTARIO_VERIF_CREAR"].ToString();
            txtramVerif.Text = Session["ATM_RAM_VERIF_CREAR"].ToString();
            txtlatitudATM.Text = Session["ATM_LATITUD_VERIF_CREAR"].ToString();
            txtlongitudATM.Text = Session["ATM_LONGITUD_VERIF_CREAR"].ToString();
            txtUbicacionATM.Text = Session["ATM_UBICACION_VERIF_CREAR"].ToString();
            //Session["ATM_IDUBI_VERIF_CREAR"] 
            txtsucursal.Text = Session["ATM_SUCURSAL_VERIF_CREAR"].ToString();
            //Session["ATM_DEPTO_VERIF_CREAR"] 
            txtzonaVerif.Text = Session["ATM_ZONA_VERIF_CREAR"].ToString();
            //Session["ATM_IDMANT_VERIF_CREAR"]
            //Session["ATM_ESTADO_VERIF_CREAR"] 
            //Session["ATM_FECHAMANT_VERIF_CREAR"]
            //Session["ATM_HRINICIO_VERIF_CREAR"]
            //Session["ATM_HRFIN_VERIF_CREAR"] 
            //Session["ATM_AUTORIZADO_VERIF_CREAR"] 
            txtsysaid.Text = Session["ATM_SYSAID_VERIF_CREAR"].ToString();
            txtTecnicoResponsable.Text = Session["ATM_TECNICO_VERIF_CREAR"].ToString();
            //Session["ATM_USUARIO_VERIF_CREAR"] 
            txtidentidad.Text = Session["ATM_IDENTIDAD_VERIF_CREAR"].ToString();
            txtsoVerif.Text = Session["ATM_SO_VERIF_CREAR"].ToString();
            txtversionswVerif.Text = Session["ATM_VERSIONSW_VERIF_CREAR"].ToString();
            //txtcodATM.Text = Session["codATM"].ToString();
            // DDLsucursalATM.SelectedIndex = CargarInformacionDDL(DDLsucursalATM, Session["sucursalATM"].ToString());
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

        void CargarVerificacion()
        {
            if (HttpContext.Current.Session["VERIF"] == null)
            {
                try
                {
                    //if (HttpContext.Current.Session["SUCURSAL"] == null)
                    //{
                    String vQuery = "STEISP_ATM_Generales 6,1";
                    DataTable vDatos = vConexion.ObtenerTabla(vQuery);
                    DDLtipoTeclado.Items.Add(new ListItem { Value = "0", Text = "Seleccione teclado..." });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLtipoTeclado.Items.Add(new ListItem { Value = item["idTecladoATM"].ToString(), Text = item["nombreTecladoATM"].ToString() });
                        //}
                        //    Session["SUCURSAL"] = "1";
                    }

                }
                catch (Exception ex)
                {
                    throw;
                }

                

                try
                {
                    //if (HttpContext.Current.Session["CARGA"] == null)
                    //{
                    String vQuery = "STEISP_ATM_Generales 4,1";
                    DataTable vDatos = vConexion.ObtenerTabla(vQuery);
                    DDLtipoCargaVerif.Items.Add(new ListItem { Value = "0", Text = "Seleccione tipo de carga..." });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLtipoCargaVerif.Items.Add(new ListItem { Value = item["idTipoCargaATM"].ToString(), Text = item["nombreTipoCargaATM"].ToString() });
                        //    }
                        //    Session["CARGA"] = "1";
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }

                try
                {
                    //if (HttpContext.Current.Session["PROCESADOR"] == null)
                    //{
                    String vQuery = "STEISP_ATM_Generales 5,1";
                    DataTable vDatos = vConexion.ObtenerTabla(vQuery);
                    DDLtipoProc.Items.Add(new ListItem { Value = "0", Text = "Seleccione tipo de procesador..." });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLtipoProc.Items.Add(new ListItem { Value = item["idProcesadorATM"].ToString(), Text = item["nombreProcesadorATM"].ToString() });
                        //    }
                        //    Session["PROCESADOR"] = "1";
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }

                try
                {
                    //if (HttpContext.Current.Session["MARCA"] == null)
                    //{
                    String vQuery = "STEISP_ATM_Generales 11,1";
                    DataTable vDatos = vConexion.ObtenerTabla(vQuery);
                    DDLmarcaDiscoDuro.Items.Add(new ListItem { Value = "0", Text = "Seleccione la marca..." });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLmarcaDiscoDuro.Items.Add(new ListItem { Value = item["idMarca"].ToString(), Text = item["nombreMarca"].ToString() });
                    }
                    //    Session["MARCA"] = "1";
                    //}
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
                Session["VERIF"] = "1";
            }
            

            protected void dropantiskimming_SelectedIndexChanged(object sender, EventArgs e)
        {
           
      
        }

        protected void ddlclimatizacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlclimatizacion.SelectedValue == "1")
                {
                    //imgclimatizacion.CssClass = 1;// "dropify";
                    imgclimatizacion.Attributes.Add("CssClass" , "dropify");

                    imgclimatizacion.Visible = true;
                    UpdatePanel1.Update();
                }
                else
                {
                    imgclimatizacion.Visible = false;
                }
            }
            catch (Exception)
            {

                
            }
           
        }

        protected void btnEnviarVerif_Click(object sender, EventArgs e)
        {

            dropantiskimming.SelectedIndex = 0;
            txtantiSkimming.Enabled = false;
            txtantiSkimming.Text = "";
        }

        protected void dropantiskimming_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dropantiskimming.SelectedValue == "1")
                {
                    txtantiSkimming.Enabled = true;
                }
                else
                {
                    txtantiSkimming.Enabled = false;
                }
            }
            catch (Exception)
            {

            }
        }
     
            

        protected void btnmodal_Click(object sender, EventArgs e)
        {
            
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);
            
        }

       
       

        protected void ddlenergia_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlenergia.SelectedValue == "1")
                {
                    imgproteccionEnergia.Visible = true;
                }
                else
                {
                    imgproteccionEnergia.Visible = false;
                }
            }
            catch (Exception Ex)
            {

                
            }
        }
    }
}