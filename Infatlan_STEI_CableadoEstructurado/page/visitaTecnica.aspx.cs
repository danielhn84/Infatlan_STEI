using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Infatlan_STEI_CableadoEstructurado.clases;
using System.Data.Sql;
using System.IO;



namespace Infatlan_STEI_CableadoEstructurado.paginas
{
    public partial class estudioEstructurado : System.Web.UI.Page
    {
        db vConexion = new db();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                HFCuartoTelecomunicaciones.Value = string.Empty;
                HFReubicar.Value = string.Empty;
                HFDesordenado.Value = string.Empty;
                HFExpuestoHumedo.Value = string.Empty;
                HFExpuestoRobo.Value = string.Empty;
                HFEquiposAjeno.Value = string.Empty;
                HFPlano.Value = string.Empty;
                EliminarSesiones();
                CargarDatos();


                //String vQuery = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 11," + "'acamador'"+"";

                //int caseSwitch = 1;

                //Int32 vInformacion = vConexion.ejecutarSql(vQuery);

                //if (vInformacion == 1)
                //{
                //    caseSwitch = 2;
                //}


                //    switch (caseSwitch)
                //    {
                //    case 1:
                //        Console.WriteLine("Case 1");
                //        break;
                //    case 2:
                //        Console.WriteLine("Case 2");
                //        break;
                //    default:
                //        Console.WriteLine("Default case");
                //        break;
                //}

            }

        }

        public void EliminarSesiones()
        {
            Session["CE_MATERIALES"] = null;
            Session["CE_CABLEADO"] = null;
            Session["CE_ROLES"] = null;
            Session["CE_IDRESPONSABLE"] = null;
            Session["CE_IDUBICACION"] = null;
            Session["CE_IDEPARTAMENTO"] = null;
            Session["CE_IDMATERIAL"] = null;
        }

        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        public void CerrarModal(String vModal)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Pop", "$('#" + vModal + "').modal('hide');", true);
        }

        public void Limpiar()
        {
            ddlResponsable.SelectedIndex = -1;
            txtIdentidad.Text = "";
            txtZona.Text = "";
            //udpResposable.Update();

            ddlAgencia.SelectedIndex = 0;
            txtDireccion.Text = "";
            udpAgencia.Update();

            txtFechaEstudio.Text = "";
            txtFechaEnvio.Text = "";
            udpFechasEstudio.Update();

            //imgCuartoTelecomunicaciones.Src = string.Empty;
            fuCuartoTelecomunicaciones.Attributes.Clear();
            //udpCuartoTelecomunicaciones.Update();

            //udpTodoCuartoTelecomunicaciones.Update();

            rblReubicar.ClearSelection();
            //udpReubicar.Update();

            //udpEtiquetado.Update();
            rblEtiquetado.ClearSelection();
            //rblEtiquetado.ClearSelection();

            //udpImgReubicar.Update();

            udpDesordenado.Update();
            rblDesordenado.ClearSelection();
                       
            rblExpuestoHumedo.ClearSelection();
            //udpExpuestpHumedo.Update();

            rblExpuestoRobo.ClearSelection();
            //udpExpuestoRobo.Update();

            rblElementoAjenos.ClearSelection();
            //udpElemetoAjeno.Update();

            //udpUps.Update();
            rblUps.ClearSelection();

           // udpAire.Update();
            rblAire.ClearSelection();

           // udpCategoria.Update();
            rblCategoria.ClearSelection();

            //udpRotulacion.Update();
            rblRotulacion.ClearSelection();
           
            ddlMateriales.SelectedIndex = -1;
            udpMetariales.Update();

            udpCantidad.Update();
            txtCantidad.Text = "";

            ddlMedidas.SelectedIndex = -1;
            udpUnidades.Update();

            GVMateriales.DataBind();
            GVMateriales.DataSource = null;
            UpdateDivMateriales.Update();

            txtHorasTrabajo.Text = "";
            udpDuracion.Update();

            txtParticipantes.Text = "";
            udpParticipantes.Update();

            rblTransporte.ClearSelection();
            udpTransporte.Update();

            updAlimentacion.Update();
            rblALimentación.ClearSelection();

            udpObservaciones.Update();
            txtObservaciones.Text = "";

           
        }

        void CargarDatos()
        {
            
            if (HttpContext.Current.Session["CE_CABLEADO"] == null)
            {
                //Obtener Materiales
                try
                {
                    String vQuery = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 6, " + ddlMateriales.SelectedValue;
                    DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                    DataTable vData = (DataTable)Session["CE_CABLEADO"];

                    if (vData == null)
                        vData = vDatos.Clone();

                    if (vDatos != null)
                        vData.Rows.Add(vDatos.Rows[0]["idStock"].ToString(), vDatos.Rows[0]["nombre"].ToString(), vDatos.Rows[0]["cantidad"].ToString(), vDatos.Rows[0]["medidas"].ToString() + " " + vDatos.Rows[0]["unidades"].ToString());


                    GVMateriales.DataSource = vData;
                    GVMateriales.DataBind();
                    Session["CE_CABLEADO"] = vData;

                }
                catch (Exception Ex)
                {
                    Mensaje(Ex.Message, WarningType.Danger);
                }


                //Obtener todos las Agencias.
                try
                {
                    String vQuery = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 19";
                    DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                    ddlAgencia.Items.Clear();
                    ddlAgencia.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        ddlAgencia.Items.Add(new ListItem { Value = item["idUbicacion"].ToString(), Text = item["nombre"].ToString() });
                    }

                }
                catch (Exception Ex)
                {
                    Mensaje(Ex.Message, WarningType.Danger);
                }

                //Obtener todos los Responsables.
                try
                {
                    String vQuery = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 2";

                    DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                    //ddlResponsable.Items.Clear();
                    ddlResponsable.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        ddlResponsable.Items.Add(new ListItem { Value = item["idUsuario"].ToString(), Text = item["nombre"].ToString() + " " + item["apellidos"].ToString() });
                    }

                }
                catch (Exception Ex)
                {
                    Mensaje(Ex.Message, WarningType.Danger);
                }


                //Obtener Unidades
                try
                {
                    String vQuery = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 8";

                    DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                    //ddlResponsable.Items.Clear();
                    ddlMedidas.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        ddlMedidas.Items.Add(new ListItem { Value = item["idMedida"].ToString(), Text = item["medida"].ToString() });
                    }

                }
                catch (Exception Ex)
                {
                    Mensaje(Ex.Message, WarningType.Danger);
                }


                //Obtener materiales
                try
                {
                    String vQuery = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 7";
                    DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                    if (vDatos.Rows.Count > 0)
                    {
                        ddlMateriales.Items.Clear();
                        ddlMateriales.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción " });
                        foreach (DataRow item in vDatos.Rows)
                        {
                            ddlMateriales.Items.Add(new ListItem { Value = item["idStock"].ToString(), Text = item["material"].ToString() + " - " + item["modelo"].ToString() + " - " + item["marca"].ToString() });
                        }
                    }

                    //ddlMateriales.Items.Add(new ListItem { Value = item["idStock"].ToString(), Text = item["nombre"].ToString() + " - " + item["modelo"].ToString() + " (" + item["cantidad"]ToString() + ")" });

                }
                catch (Exception Ex)
                {
                    Mensaje(Ex.Message, WarningType.Danger);
                }

                Session["CE_CABLEADO"] = "1";
            }
        }

        //DROPDOWNLIST
        protected void ddlResponsable_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String vQuery = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 3, '" + ddlResponsable.SelectedValue + "'";

                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                Session["CE_IDRESPONSABLE"] = vDatos.Rows[0]["idUsuario"].ToString();
                txtIdentidad.Text = vDatos.Rows[0]["identidad"].ToString();
                txtZona.Text = vDatos.Rows[0]["nombre"].ToString();
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        protected void ddlAgencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String vQuery = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 1, '" + ddlAgencia.SelectedValue + "'";

                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                Session["CE_IDUBICACION"] = vDatos.Rows[0]["idUbicacion"].ToString();
                Session["CE_IDEPARTAMENTO"] = vDatos.Rows[0]["idDepartamento"].ToString();

                txtDireccion.Text = vDatos.Rows[0]["direccion"].ToString();

                String vQueryUbicacion = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 21, '" + ddlAgencia.SelectedValue + "'";

                DataTable vDatosUbicacion = vConexion.obtenerDataTable(vQueryUbicacion);
                Session["CE_NOMBREESTUDIO"] = vDatosUbicacion.Rows[0]["codigo"].ToString();

            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }

        }

        protected void ddlMateriales_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String vQuery = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 4, '" + ddlMateriales.SelectedValue + "'";

                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                Session["CE_IDMATERIAL"] = vDatos.Rows[0]["idMaterial"].ToString();

             


            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }

        }

        protected void ddlMedidas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String vQuery = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 9, '" + ddlMedidas.SelectedValue + "'";

                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                Session["CE_IDMEDIDA"] = vDatos.Rows[0]["idMedida"].ToString();

            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }


        //GRIDVIEW 
        protected void GVMateriales_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GVMateriales.DataSource = (DataTable)Session["CE_MATERIAL"];
                GVMateriales.PageIndex = e.NewPageIndex;
                GVMateriales.DataBind();
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        protected void GVMateriales_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                DataTable vDatos = (DataTable)Session["CE_MATERIALES"];
                if (e.CommandName == "Eliminar")
                {
                    String vMaterial = e.CommandArgument.ToString();

                    if (Session["CE_MATERIALES"] != null)
                    {
                        DataRow[] result = vDatos.Select("numero = '" + vMaterial + "'");

                        foreach (DataRow row in result)
                        {
                            if (row["numero"].ToString().Contains(vMaterial))
                                vDatos.Rows.Remove(row);
                        }
                    }
                }
                GVMateriales.DataSource = vDatos;
                GVMateriales.DataBind();
                Session["CE_MATERIALES"] = vDatos;

            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }


        //RADIOBUTTONLIST
        protected void rblDesordenado_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (rblDesordenado.SelectedValue == "si")
                {
                    fuDesordenado.Visible = true;
                    imgDesordenado.Visible = true;
                    //udpDesordenado.Update();
                }
                else
                {
                    fuDesordenado.Visible = false;
                    imgDesordenado.Visible = false;
                    //udpDesordenado.Update();
                }
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        protected void rblExpuestoHumedad_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (rblExpuestoHumedo.SelectedValue == "si")
                {

                    fuExpuestoHumedo.Visible = true;
                    imgExpuestoHumedo.Visible = true;
                }
                else
                {
                    fuExpuestoHumedo.Visible = false;
                    imgExpuestoHumedo.Visible = false;
                }
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }

        }

        protected void rblExpuestoRobo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (rblExpuestoRobo.SelectedValue == "si")
                {
                    fuExpuestoRobo.Visible = true;
                    imgExpuestoRobo.Visible = true;
                   
                }
                else
                {
                    fuExpuestoRobo.Visible = false;
                    imgExpuestoRobo.Visible = false;
                }
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }


        }

        protected void rblElementosAjenos_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (rblElementoAjenos.SelectedValue == "si")
                {
                    fuElemetoAjenos.Visible = true;
                    imgElementoAjeno.Visible = true;
                }
                else
                {
                    fuElemetoAjenos.Visible = false;
                    imgElementoAjeno.Visible = false;
                }
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        protected void rblReubicar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (rblReubicar.SelectedValue == "si")
                {

                    fuReubicar.Visible = true;
                    imgReubicar.Visible = true;
                     Div1.Visible = true;
                    udpVisible.Update();
                }
                else
                {
                    fuReubicar.Visible = false;
                    imgReubicar.Visible = false;
                    Div1.Visible = false;
                    udpVisible.Update();
                }
               
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }

        }

        //VALIDACIONES
        void ValidarFormDatosGenerales()
        {
            try
            {
                Session["CE_ERRORGENERALES"] = 0;
            
                if (ddlResponsable.SelectedIndex == 0)
                {
                    throw new Exception("Por favor seleccione el Responsable");

                }

                if (ddlAgencia.SelectedIndex == 0)
                {
                    throw new Exception("Por favor seleccione la Agencia");
                }

                if (txtFechaEstudio.Text == "" || txtFechaEnvio.Text == string.Empty)
                {
                    throw new Exception("Por favor ingrese la Fecha de Envío");
                }
                if (txtFechaEstudio.Text == "" || txtFechaEstudio.Text == string.Empty)
                {
                    throw new Exception("Por favor ingrese la Fecha de Estudio");
                }
            }
            catch (Exception Ex) { Mensaje(Ex.Message, WarningType.Danger);
                Session["CE_ERRORGENERALES"] = 1;
            }
        }

        void ValidarFormMateriales()
        {
            try
            {
                Session["CE_ERRORMATERIALES"] = 0;
               
                if (ddlMateriales.SelectedIndex == 0)
                {
                    throw new Exception("Por favor seleccione un Material");
                }
                if (txtCantidad.Text == "" || txtCantidad.Text == string.Empty)
                {
                    throw new Exception("Por favor ingrese una cantidad");
                }
                if (ddlMedidas.SelectedIndex == 0)
                {
                    throw new Exception("Por favor seleccione las Unidades");
                }
            }
            catch (Exception Ex) { Mensaje(Ex.Message, WarningType.Danger);
                Session["CE_ERRORMATERIALES"] = 1;
            }
        }
        
        void ValidarFormAprobacion()
        {
            try
            {

                if (rblAprobada.SelectedValue == "")
                {
                    throw new Exception("Por favor seleccione si es aprobado el estudio");
                }
                if (txtObservacionesAprobacion.Text == "" || txtObservacionesAprobacion.Text == string.Empty)
                {
                    throw new Exception("Por favor ingrese una Observación sobre la Aprobación ");
                }
            }
            catch (Exception Ex) { Mensaje(Ex.Message, WarningType.Danger);
                Session["CE_ERRORAPROBACION"] = 1;
            }
        }

        void ValidarFormEstimacionRecursos()
        {
            try
            {
                Session["CE_ERRORRECURSOS"] = 0;

                if (txtHorasTrabajo.Text == "" || txtHorasTrabajo.Text == string.Empty)
                {
                    throw new Exception("Por favor ingrese las Horas Trabajadas");
                }
                if (txtParticipantes.Text == "" || txtParticipantes.Text == string.Empty)
                {
                    throw new Exception("Por favor ingrese los Participantes");
                }
                if (rblTransporte.SelectedValue == "")
                {
                    throw new Exception("Por favor seleccione si utilizó transporte");
                }
                if (rblALimentación.SelectedValue == "")
                {
                    throw new Exception("Por favor seleccione si utilizó Alimentación");
                }
            }
            catch (Exception Ex) { Mensaje(Ex.Message, WarningType.Danger);
                Session["CE_ERRORRECURSOS"] = 1;
            }
        }

        //BUTTON
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                String vMensaje = "";

                DataTable vData = new DataTable();
                DataTable vDatos = (DataTable)Session["CE_MATERIALES"];
                string vMaterial = ddlMateriales.SelectedValue;
                string vNombreMaterial = ddlMateriales.SelectedItem.Text;

                vData.Columns.Add("numero");
                vData.Columns.Add("nombre");
                vData.Columns.Add("cantidad");
                vData.Columns.Add("medida");

                if (vDatos == null)
                    vDatos = vData.Clone();

                if (vDatos != null)
                {
                    if (vDatos.Rows.Count < 1)
                        vDatos.Rows.Add(ddlMateriales.SelectedValue, ddlMateriales.SelectedItem.Text, txtCantidad.Text, ddlMedidas.SelectedItem.Text);
                    else
                    {
                        Boolean vRegistered = false;
                        for (int i = 0; i < vDatos.Rows.Count; i++)
                        {
                            if (vNombreMaterial == vDatos.Rows[i]["nombre"].ToString())
                            {
                                vDatos.Rows[i]["cantidad"] = Convert.ToDecimal(vDatos.Rows[i]["cantidad"].ToString()) + Convert.ToDecimal(txtCantidad.Text);
                                vRegistered = true;
                            }
                        }

                        if (!vRegistered)
                            vDatos.Rows.Add(ddlMateriales.SelectedValue, ddlMateriales.SelectedItem.Text, txtCantidad.Text, ddlMedidas.SelectedItem.Text);
                    }
                }

                GVMateriales.DataSource = vDatos;
                GVMateriales.DataBind();
                Session["CE_MATERIALES"] = vDatos;
                UpdateDivMateriales.Update();

                ddlMateriales.SelectedIndex = -1;
                udpMetariales.Update();

                udpCantidad.Update();
                txtCantidad.Text = "";

                ddlMedidas.SelectedIndex = -1;
                udpUnidades.Update();

                GVMateriales.DataBind();
                GVMateriales.DataSource = null;
                UpdateDivMateriales.Update();

            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Success);
            }
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
           
            //Guardar los datos que ingressa el Tecnico
            try
            {
                ValidarFormAprobacion();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalAprobacion();", true);

               
            }
            catch (Exception Ex) { Mensaje(Ex.Message, WarningType.Danger); }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            //Guardar los datos que ingresa el Tecnico
            try
            {
                    ValidarFormDatosGenerales();
                    ValidarFormMateriales();
                    ValidarFormEstimacionRecursos();
                if ((Session["CE_ERRORGENERALES"].ToString() == Convert.ToString(0)) && (Session["CE_ERRORMATERIALES"].ToString() == Convert.ToString(0)) && (Session["CE_ERRORRECURSOS"].ToString() == Convert.ToString(0)))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDatosGenerales();", true);
                }
              
            }
            catch (Exception Ex) { Mensaje(Ex.Message, WarningType.Danger); }

            
        }

        protected void BtnModGuardar_Click(object sender, EventArgs e)
        {


            //IMAGENES1
            String vNombreDepot = String.Empty;
            HttpPostedFile bufferDepositoT = fuCuartoTelecomunicaciones.PostedFile;
            byte[] vFileDeposito = null;
            string vExtension = string.Empty;

            if (bufferDepositoT != null)
            {
                vNombreDepot = fuCuartoTelecomunicaciones.FileName;
                Stream vStream = bufferDepositoT.InputStream;
                BinaryReader vReader = new BinaryReader(vStream);
                vFileDeposito = vReader.ReadBytes((int)vStream.Length);
                vExtension = System.IO.Path.GetExtension(fuCuartoTelecomunicaciones.FileName);
            }
            String vArchivo = String.Empty;
            if (vFileDeposito != null)
                vArchivo = Convert.ToBase64String(vFileDeposito);

            //IMAGENES2
            String vNombreDepot1 = String.Empty;
            HttpPostedFile bufferDepositoT1 = fuReubicar.PostedFile;
            byte[] vFileDeposito1 = null;
            string vExtension1 = string.Empty;

            if (bufferDepositoT1 != null)
            {
                vNombreDepot1 = fuReubicar.FileName;
                Stream vStream1 = bufferDepositoT1.InputStream;
                BinaryReader vReader1 = new BinaryReader(vStream1);
                vFileDeposito1 = vReader1.ReadBytes((int)vStream1.Length);
                vExtension1 = System.IO.Path.GetExtension(fuReubicar.FileName);
            }
            String vArchivo1 = String.Empty;
            if (vFileDeposito1 != null)
                vArchivo1 = Convert.ToBase64String(vFileDeposito1);

            //IMAGENES3
            String vNombreDepot2 = String.Empty;
            HttpPostedFile bufferDepositoT2 = fuDesordenado.PostedFile;
            byte[] vFileDeposito2 = null;
            string vExtension2 = string.Empty;

            if (bufferDepositoT2 != null)
            {
                vNombreDepot2 = fuDesordenado.FileName;
                Stream vStream2 = bufferDepositoT2.InputStream;
                BinaryReader vReader2 = new BinaryReader(vStream2);
                vFileDeposito2 = vReader2.ReadBytes((int)vStream2.Length);
                vExtension2 = System.IO.Path.GetExtension(fuDesordenado.FileName);
            }
            String vArchivo2 = String.Empty;
            if (vFileDeposito2 != null)
                vArchivo2 = Convert.ToBase64String(vFileDeposito2);

            //IMAGENES4
            String vNombreDepot3 = String.Empty;
            HttpPostedFile bufferDepositoT3 = fuExpuestoHumedo.PostedFile;
            byte[] vFileDeposito3 = null;
            string vExtension3 = string.Empty;

            if (bufferDepositoT3 != null)
            {
                vNombreDepot3 = fuExpuestoHumedo.FileName;
                Stream vStream3 = bufferDepositoT3.InputStream;
                BinaryReader vReader3 = new BinaryReader(vStream3);
                vFileDeposito3 = vReader3.ReadBytes((int)vStream3.Length);
                vExtension3 = System.IO.Path.GetExtension(fuExpuestoHumedo.FileName);
            }
            String vArchivo3 = String.Empty;
            if (vFileDeposito2 != null)
                vArchivo3 = Convert.ToBase64String(vFileDeposito3);

            //IMAGENES5
            String vNombreDepot4 = String.Empty;
            HttpPostedFile bufferDepositoT4 = fuExpuestoRobo.PostedFile;
            byte[] vFileDeposito4 = null;
            string vExtension4 = string.Empty;

            if (bufferDepositoT4 != null)
            {
                vNombreDepot4 = fuExpuestoRobo.FileName;
                Stream vStream4 = bufferDepositoT4.InputStream;
                BinaryReader vReader4 = new BinaryReader(vStream4);
                vFileDeposito4 = vReader4.ReadBytes((int)vStream4.Length);
                vExtension4 = System.IO.Path.GetExtension(fuExpuestoRobo.FileName);
            }
            String vArchivo4 = String.Empty;
            if (vFileDeposito4 != null)
                vArchivo4 = Convert.ToBase64String(vFileDeposito4);

            //IMAGENES6
            String vNombreDepot5 = String.Empty;
            HttpPostedFile bufferDepositoT5 = fuElemetoAjenos.PostedFile;
            byte[] vFileDeposito5 = null;
            string vExtension5 = string.Empty;

            if (bufferDepositoT5 != null)
            {
                vNombreDepot5 = fuElemetoAjenos.FileName;
                Stream vStream5 = bufferDepositoT5.InputStream;
                BinaryReader vReader5 = new BinaryReader(vStream5);
                vFileDeposito5 = vReader5.ReadBytes((int)vStream5.Length);
                vExtension5 = System.IO.Path.GetExtension(fuElemetoAjenos.FileName);
            }
            String vArchivo5 = String.Empty;
            if (vFileDeposito5 != null)
                vArchivo5 = Convert.ToBase64String(vFileDeposito5);

            //IMAGENES7
            String vNombreDepot6 = String.Empty;
            HttpPostedFile bufferDepositoT6 =  fuPlano.PostedFile;
            byte[] vFileDeposito6 = null;
            string vExtension6 = string.Empty;

            if (bufferDepositoT6 != null)
            {
                vNombreDepot6 = fuPlano.FileName;
                Stream vStream6 = bufferDepositoT6.InputStream;
                BinaryReader vReader6 = new BinaryReader(vStream6);
                vFileDeposito6 = vReader6.ReadBytes((int)vStream6.Length);
                vExtension6 = System.IO.Path.GetExtension(fuPlano.FileName);
            }
            String vArchivo6 = String.Empty;
            if (vFileDeposito6 != null)
                vArchivo6 = Convert.ToBase64String(vFileDeposito6);

            //FIN

            //Ingresa los datos generales

            String vQuery = "STEISP_CABLESTRUCTURADO_IngresarDatosEstudio 1," + Session["CE_IDEPARTAMENTO"] + "," +
                                                                           "'" + Session["CE_IDUBICACION"] + "'," +
                                                                           "'" + Session["CE_IDRESPONSABLE"] + "'," +
                                                                           "'" + Session["CE_NOMBREESTUDIO"] + "'," +
                                                                           "'" + txtFechaEstudio.Text + "'," +
                                                                           "'" + txtFechaEnvio.Text + "'," +
                                                                           "'" + txtHorasTrabajo.Text + "'," +
                                                                           "'" + txtParticipantes.Text + "'," +
                                                                           "'" + rblALimentación.SelectedValue + "'," +
                                                                           "'" + rblTransporte.SelectedValue + "'," +
                                                                           "'" + txtObservaciones.Text + "'," +
                                                                           "'IngresoTecnico'";

            DataTable vDatos = vConexion.obtenerDataTable(vQuery);

            //Obtiene IdEstudio

            String vQuery1 = "STEISP_CABLESTRUCTURADO_IngresarDatosEstudio 3";

            DataTable vDatos1 = vConexion.obtenerDataTable(vQuery1);
            Session["CE_IDESTUDIO"] = (vDatos1.Rows[0]["Id"].ToString());


            //Ingresa los datos del material 
            DataTable vDatosMaterial = (DataTable)Session["CE_MATERIALES"];

            for (int i = 0; i < vDatosMaterial.Rows.Count; i++)
            {
                string vMaterial;
                string vMedidas;
                string vCantidad;
                string vIdMaterial;

                vIdMaterial = (vDatosMaterial.Rows[i]["numero"].ToString());

                vMaterial = (vDatosMaterial.Rows[i]["nombre"].ToString());

                vCantidad = (vDatosMaterial.Rows[i]["cantidad"].ToString());

                vMedidas = (vDatosMaterial.Rows[i]["medida"].ToString());

                String vQuery2 = "STEISP_CABLESTRUCTURADO_IngresarDatosEstudio 5," + Session["CE_IDESTUDIO"] + "," +
                                                                                 "'" + vIdMaterial + "'," +
                                                                                 "'" + vMaterial + "'," +
                                                                                 "'" + vMedidas + "'," +
                                                                                 "'" + vCantidad + "'";

                DataTable vDatos2 = vConexion.obtenerDataTable(vQuery2);
            }

            //Ingreso de Preguntaas
            String vQuery3 = "STEISP_CABLESTRUCTURADO_IngresarDatosEstudio 4," + Session["CE_IDESTUDIO"] + "," +
                                                                             "'" + vArchivo + "'," +
                                                                            "'" + 1 + "'," +
                                                                            "'" + rblReubicar.SelectedValue + "'," +
                                                                            "'" + vArchivo1 + "'," +
                                                                            "'" + rblDesordenado.SelectedValue + "'," +
                                                                            "'" + vArchivo2 + "'," +
                                                                            "'" + rblExpuestoHumedo.SelectedValue + "'," +
                                                                            "'" + vArchivo3 + "'," +
                                                                            "'" + rblExpuestoRobo.SelectedValue + "'," +
                                                                            "'" + vArchivo4 + "'," +
                                                                            "'" + rblElementoAjenos.SelectedValue + "'," +
                                                                            "'" + vArchivo5 + "'," +
                                                                            "'" + rblUps.SelectedValue + "'," +
                                                                            "'" + rblAire.SelectedValue + "'," +
                                                                            "'" + rblCategoria.SelectedValue + "'," +
                                                                            "'" + rblRotulacion.SelectedValue + "'," +
                                                                           "'Disponible'";
            DataTable vDatos3 = vConexion.obtenerDataTable(vQuery3);

            String vQuery4 = "STEISP_CABLESTRUCTURADO_IngresarDatosEstudio 7," + Session["CE_IDESTUDIO"] + "," +
                                                                             "'" + Session["CE_IDRESPONSABLE"] + "'";

            DataTable vDatos4 = vConexion.obtenerDataTable(vQuery4);

            //Int32 vGeneral = vConexion.ejecutarSql(vQuery);
            //Int32 vEstudio = vConexion.ejecutarSql(vQuery3);

            //if (vEstudio ==1)
            //{ 


            Limpiar();
            Mensaje("Actualizado con Exito!", WarningType.Success);
            CerrarModal("MensajeAceptacionModal");
            Session["CE_MATERIALES"] = null;
            //}
            //else
            //{
            //    Mensaje("No se pudo actualizar!", WarningType.Success);
            //}

        }

        protected void btnModAproGuardar_Click(object sender, EventArgs e)
        {
            string vMensaje = "";

            //Guardar los datos que ingressa el Tecnico
            try
            {
                ValidarFormAprobacion();

                //Inserta datos a la tabla Aprobacion
                String vQuery = "STEISP_CABLESTRUCTURADO_IngresarDatosEstudio 2," + rblAprobada.SelectedValue + "'," +
                                                                                 "'" + txtObservacionesAprobacion.Text + "'," +
                                                                                 "'AprobaciónJefe";

                Int32 vInformacion = vConexion.ejecutarSql(vQuery);

                //Trae IdAprobacion
                String vQueryId = "STEISP_CABLESTRUCTURADO_IngresarDatosEstudio 8,";
                DataTable vDatosId = vConexion.obtenerDataTable(vQueryId);

                Int32 vInforId = vConexion.ejecutarSql(vQueryId);

                string vIdAprobacion = (vDatosId.Rows[0]["Id"].ToString());

                //Obtener IdEstudio
                string vIdEstudio = "";

                //Inserta datos en la tabla EstudioRelAprobacion
                String vQueryAprobacion = "STEISP_CABLESTRUCTURADO_IngresarDatosEstudio 9," + vIdEstudio + "'," +
                                                                                          "'" + vIdAprobacion + "'," +
                                                                                           "'EstudioAprobado'";

                Int32 vInforApro = vConexion.ejecutarSql(vQueryAprobacion);


                if (vInformacion == 1 && vInforApro==1 && vInforId==1)
                {
                    vMensaje = "Actualizado con Exito!";
                    rblAprobada.ClearSelection();
                    udpAprobado.Update();
                    txtObservacionesAprobacion.Text = "";
                    udpObservacionesAprobacion.Update();

                    //Mensaje("Actualizado con Exito!", WarningType.Success);

                }
                else
                {
                    vMensaje = "No se pudo actualizar!";
                    //Mensaje("No se pudo actualizar!", WarningType.Success);

                }

               // Mensaje("Actualizado con Exito!", WarningType.Success);
                CerrarModal("MensajeAceptacionModalApro");

                Mensaje(vMensaje, WarningType.Success);
            }
            catch (Exception Ex) { Mensaje(Ex.Message, WarningType.Danger); }
        }
    }
    
}
   