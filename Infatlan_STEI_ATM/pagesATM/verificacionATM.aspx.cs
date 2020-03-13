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
       

        void limpiarModalVerificacion()
        {
            dropMantPendiente.SelectedValue = "0";
            dropMotivo.SelectedValue = "0";
            dropcambioPor.SelectedValue = "0";
            dropNewTecnico.SelectedValue = "0";
            txtdetalleCancela.Text = string.Empty;
        }

        bd vConexion = new bd();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["VERIF"] = null;
            if (!Page.IsPostBack)
            {
                //limpiarModalVerificacion();
                CargarVerificacion();
            }
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
                    droptipoTeclado.Items.Add(new ListItem { Value = "0", Text = "Seleccione sucursal..." });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        droptipoTeclado.Items.Add(new ListItem { Value = item["idTecladoATM"].ToString(), Text = item["nombreTecladoATM"].ToString() });
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
                    dropNewTecnico.Items.Clear();
                    String vQuery = "STEISP_AGENCIA_CreacionNotificacion 5";
                    DataTable vDatos = vConexion.ObtenerTabla(vQuery);
                    dropNewTecnico.Items.Add(new ListItem { Value = "0", Text = "Seleccione nuevo técnico..." });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        dropNewTecnico.Items.Add(new ListItem { Value = item["idUsuario"].ToString(), Text = item["nombre"].ToString() });
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
                    droptipoProc.Items.Add(new ListItem { Value = "0", Text = "Seleccione tipo de procesador..." });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        droptipoProc.Items.Add(new ListItem { Value = item["idProcesadorATM"].ToString(), Text = item["nombreProcesadorATM"].ToString() });
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
            limpiarModalVerificacion();
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);
            
        }

        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        protected void btnMantSinRealizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtdetalleCancela.Text != "")
                {
                    limpiarModalVerificacion();
                }
                else
                {
                    Mensaje("No deje campos vacios", WarningType.Warning);
                   
                }
            }
            catch (Exception EX)
            {
                Mensaje(EX.Message, WarningType.Danger);
            }
           
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