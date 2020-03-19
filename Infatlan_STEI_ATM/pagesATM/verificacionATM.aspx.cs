using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infatlan_STEI_ATM.clases;
using System.Data;
using System.IO;
using System.Configuration;

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

            string MyString = Session["ATM_CAPACIDADDISCO_VERIF_CREAR"].ToString();
            char[] MyChar = { 'G', 'B', ' ' };
            string NewString = MyString.TrimEnd(MyChar);

            txtcapacidadDiscoVerif.Text = NewString;
            txtinventarioVerif.Text = Session["ATM_INVENTARIO_VERIF_CREAR"].ToString();

            string MyString2 = Session["ATM_RAM_VERIF_CREAR"].ToString();
            char[] MyChar2 = { 'G', 'B', ' ' };
            string NewString2 = MyString2.TrimEnd(MyChar2);

            txtramVerif.Text = NewString2;
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

        void validar()
        {
            //if (FUClimatizacion.HasFile==false)
            //    throw new Exception("Favor agregar imagen de climatización.");
            if (txthsalidaInfa.Text == "" || txthsalidaInfa.Text==string.Empty)
                throw new Exception("Favor ingrese la hora que salio de Infatlan.");
            if (txtHllegadaInfatlan.Text=="" || txtHllegadaInfatlan.Text==string.Empty)
                throw new Exception("Favor ingrese la hora que llego a Infatlan.");
            if (TxFechaInicio.Text == "" || TxFechaInicio.Text == string.Empty)
                throw new Exception("Favor ingrese la hora en la que inicio mantenimiento.");
            if (TxFechaRegreso.Text == "" || TxFechaRegreso.Text == string.Empty)
                throw new Exception("Favor ingrese la hora en la que termino mantenimiento.");
            if(DDLtipoTeclado.SelectedValue=="0")
                throw new Exception("Favor seleccione teclado.");
            if (DDLtipoProc.SelectedValue == "0")
                throw new Exception("Favor seleccione procesador.");
            if (DDLtipoCargaVerif.SelectedValue == "0")
                throw new Exception("Favor seleccione tipo de carga.");
            if (DDLmarcaDiscoDuro.SelectedValue == "0")
                throw new Exception("Favor seleccione marca.");
            if (DDLtipoTeclado.SelectedValue == "0")
                throw new Exception("Favor seleccione teclado.");
            if (txtSerieDiscoDuro.Text == "" || txtSerieDiscoDuro.Text == string.Empty)
                throw new Exception("Favor ingrese la serie de disco duro.");
            if (txtcapacidadDiscoVerif.Text == "" || txtcapacidadDiscoVerif.Text == string.Empty)
                throw new Exception("Favor ingrese la capacidad de memoria de disco duro.");
            if (txtserieATM.Text == "" || txtserieATM.Text == string.Empty)
                throw new Exception("Favor ingrese la serie de ATM.");
            if (txtinventarioVerif.Text == "" || txtinventarioVerif.Text == string.Empty)
                throw new Exception("Favor ingrese el número de inventario.");
            if (txtramVerif.Text == "" || txtramVerif.Text == string.Empty)
                throw new Exception("Favor ingrese la capacidad RAM.");
            if (dropantiskimming.SelectedValue == "1")
            {
                if (txtantiSkimming.Text == "" || txtantiSkimming.Text == string.Empty)
                    throw new Exception("Favor ingrese comentario de AntiSkimming.");
            }
            else if(dropantiskimming.SelectedValue == "0")
            {
            throw new Exception("Favor seleccione una respuesta de AntiSkimming.");
            }
                
            if (ddlclimatizacion.SelectedValue == "0")
                throw new Exception("Favor seleccione una respuesta de climatización.");
            if (ddlenergia.SelectedValue == "0")
                throw new Exception("Favor seleccione una respuesta de la energia.");
            if (txtobseracionesVerif.Text == "" || txtobseracionesVerif.Text == string.Empty)
                throw new Exception("Favor ingrese sus observaciones del caso.");
            //if (FUClimatizacion.HasFile)
            //    throw new Exception("Favor agregar imagen de climatización.");
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
                    
                    DIVImg11.Visible = true;
                    
                }
                else
                {
                    DIVImg11.Visible = false;
                }
            }
            catch (Exception)
            {

                
            }
           
        }
        private string GetExtension(string Extension)
        {
            switch (Extension)
            {
                case ".doc":
                    return "application/ms-word";
                case ".xls":
                    return "application/vnd.ms-excel";
                case ".ppt":
                    return "application/mspowerpoint";
                case "jpeg":
                    return "image/jpeg";
                case ".bmp":
                    return "image/bmp";
                case ".zip":
                    return "application/zip";
                case ".log":
                    return "text/HTML";
                case ".txt":
                    return "text/plain";
                case ".tiff":
                case ".tif":
                    return "image/tiff";
                case ".asf":
                    return "video/x-ms-asf";
                case ".avi":
                    return "video/avi";
                case ".gif":
                    return "image/gif";
                case ".jpg":
                case ".wav":
                    return "audio/wav";
                case ".pdf":
                    return "application/pdf";
                case ".fdf":
                    return "application/vnd.fdf";
                case ".dwg":
                    return "image/vnd.dwg";
                case ".msg":
                    return "application/msoutlook";
                case ".xml":
                    return "application/xml";
                default:
                    return "application/octet-stream";
            }
        }
        protected void btnEnviarVerif_Click(object sender, EventArgs e)
        {
            try
            {
                validar();
                lbcodATM.Text = txtcodATM.Text;
                lbNombreATM.Text = txtnomATM.Text;
                lbsucursalATM.Text = txtsucursal.Text;
                lbInventarioATM.Text = txtinventarioVerif.Text;
                lbtecnico.Text = txtTecnicoResponsable.Text;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception Ex)
            {

                Mensaje(Ex.Message, WarningType.Danger);
            }
           
 
        }      
       
                
            
        void ActualizarATM()
        {
                   string usu = "acedillo";
           
                try
                {
                    string vQuery = "STEISP_ATM_VerificacionTotal 2, '" + Session["ATM_CODATM_VERIF_CREAR"] + "','" + DDLtipoTeclado.SelectedValue + "','" + DDLtipoProc.SelectedValue + "'," +
                        "'" + DDLtipoCargaVerif.SelectedValue + "','" + DDLmarcaDiscoDuro.SelectedValue + "','" + txtSerieDiscoDuro.Text + "','" + txtcapacidadDiscoVerif.Text + "GB" + "','" + txtserieATM.Text + "'," +
                        "'" + txtinventarioVerif.Text + "','" + txtramVerif.Text + "GB" + "','" + usu + "'";
                    Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    if (vInfo == 1)
                    {
                        //Mensaje("antiskimming creada con éxito", WarningType.Success);
                    }

                }
                catch (Exception Ex)
                {
                    throw;
                }
        }

        void ActualizarVerifATM()
        {
            string usu = "acedillo";
          
            try
            {
                string vQuery = "STEISP_ATM_VerificacionTotal 1, '" + Session["ATM_IDMANT_VERIF_CREAR"] + "','" + txthsalidaInfa.Text + "','" + txtHllegadaInfatlan.Text + "'," +
                    "'" + TxFechaInicio.Text + "','" + TxFechaRegreso.Text + "','" + txtobseracionesVerif.Text + "','" + usu + "','" + txtserieATM.Text + "'," +
                    "'" + txtinventarioVerif.Text + "','" + txtramVerif.Text + "GB" + "','" + usu + "'";
                Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                if (vInfo == 1)
                {
                    //Mensaje("antiskimming creada con éxito", WarningType.Success);
                }

            }
            catch (Exception Ex)
            {
                throw;
            }
        }
        void PreguntasVerif()
        {
            //string usu = "acedillo";
            string comentario = null;
            string respuesta = "";
            for (int i = 0; i < ckpasos1.Items.Count; i++)
            {

                if (ckpasos1.Items[i].Selected)
                {
                     respuesta = "Si";
                }
                else
                {
                     respuesta = "No";
                }
                try
                {                                
                    string vQuery = "STEISP_ATM_ListaVerificacion 1, '" + Session["ATM_IDMANT_VERIF_CREAR"] + "','" + ckpasos1.Items[i].Value + "','" + respuesta + "','" + comentario + "'";
                    Int32 vInfo = vConexion.ejecutarSQL(vQuery);                                  
                }
                catch (Exception Ex)
                {
                   throw;
                }
            }

            


            for (int i = 0; i < ckpasos2.Items.Count; i++)
            {

                if (ckpasos2.Items[i].Selected)
                {
                    respuesta = "Si";
                }
                else
                {
                    respuesta = "No";
                }
                try
                {
                    string vQuery = "STEISP_ATM_ListaVerificacion 1, '" + Session["ATM_IDMANT_VERIF_CREAR"] + "','" + ckpasos2.Items[i].Value + "','" + respuesta + "','" + comentario + "'";
                    Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    if (vInfo == 1)
                    {

                    }

                }
                catch (Exception Ex)
                {
                    throw;
                }
            }

        }

        void PreguntasTxt_Img()
        { 
            try
            {
                string NumPregunta = "21";
                string vQuery = "STEISP_ATM_ListaVerificacion 2, '" + Session["ATM_IDMANT_VERIF_CREAR"] + "','" + NumPregunta + "','" + dropantiskimming.SelectedItem.Text + "','" + txtantiSkimming.Text + "'";
                Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                if (vInfo == 1)
                {                  
                    Mensaje("Lista de verificación ingresada con éxito", WarningType.Success);                  
                }

            }
            catch (Exception Ex)
            {
                throw;
            }


                //IMAGENES1
                String vNombreDepot1 = String.Empty;
                HttpPostedFile bufferDeposito1T = FUClimatizacion.PostedFile;
                byte[] vFileDeposito1 = null;
                string vExtension = string.Empty;

                if (bufferDeposito1T != null)
                {
                    vNombreDepot1 = FUClimatizacion.FileName;
                    Stream vStream = bufferDeposito1T.InputStream;
                    BinaryReader vReader = new BinaryReader(vStream);
                    vFileDeposito1 = vReader.ReadBytes((int)vStream.Length);
                    vExtension = System.IO.Path.GetExtension(FUClimatizacion.FileName);
                }
                String vArchivo = String.Empty;
                if (vFileDeposito1 != null)
                    vArchivo = Convert.ToBase64String(vFileDeposito1);
            try
            {
                string NumPregunta = "22";
                string vQuery = "STEISP_ATM_ListaVerificacion 3, '" + Session["ATM_IDMANT_VERIF_CREAR"] + "','" + NumPregunta + "','" + ddlclimatizacion.SelectedItem.Text + "','" + vArchivo + "'";
                Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                if (vInfo == 1)
                {
                    
                }

            }
             catch (Exception Ex)
            {
                   throw;
            }


                //IMAGENES2
                String vNombreDepot2 = String.Empty;
                HttpPostedFile bufferDeposito2 = FUEnergia.PostedFile;
                byte[] vFileDeposito2 = null;
                string vExtension2 = string.Empty;

                if (bufferDeposito2 != null)
                {
                    vNombreDepot2 = FUEnergia.FileName;
                    Stream vStream2 = bufferDeposito2.InputStream;
                    BinaryReader vReader2 = new BinaryReader(vStream2);
                    vFileDeposito2 = vReader2.ReadBytes((int)vStream2.Length);
                    vExtension2 = System.IO.Path.GetExtension(FUEnergia.FileName);
                }
                String vArchivo2 = String.Empty;
                if (vFileDeposito2 != null)
                    vArchivo2 = Convert.ToBase64String(vFileDeposito2);
                try
                {
                    string NumPregunta = "23";
                    string vQuery = "STEISP_ATM_ListaVerificacion 3, '" + Session["ATM_IDMANT_VERIF_CREAR"] + "','" + NumPregunta + "','" + ddlclimatizacion.SelectedItem.Text + "','" + vArchivo2 + "'";
                    Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    if (vInfo == 1)
                    {
                        Mensaje("antiskimming creada con éxito", WarningType.Success);
                    }

                }
                catch (Exception Ex)
                {
                    throw;
                }

           

            //  if (FileUpload1.HasFile)--VERIFICAR SI TIENE IMAGEN
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
                    txtantiSkimming.Text = string.Empty;
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
                    DIVImg12.Visible = true;
                }
                else
                {
                    DIVImg12.Visible = false;
                }
            }
            catch (Exception Ex)
            {

                
            }
        }

        protected void btnModalVerif_Click(object sender, EventArgs e)
        {
            try
            {
                
                ActualizarATM();
                ActualizarVerifATM();
                PreguntasVerif();
                PreguntasTxt_Img();
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        protected void btnModalCerrarVerif_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
        }
    }
}