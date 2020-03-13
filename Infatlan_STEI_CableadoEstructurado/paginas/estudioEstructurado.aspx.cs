using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Infatlan_STEI_CableadoEstructurado.clases;
using System.Data.Sql;


namespace Infatlan_STEI_CableadoEstructurado.paginas
{
    public partial class estudioEstructurado : System.Web.UI.Page
    {
        db vConexion = new db();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                EliminarSesiones();
                CargarDatos();
                CargarProceso();
            }

        }

        public void EliminarSesiones()
        {
            Session["CE_MATERIALES"] = null;
            Session["CE_CABLEADO"] = null;
            Session["CE_ROLES"] = null;
            Session["CE_IDRESPONSABLE"] = null;
            Session["CE_IDAGENCIA"] = null;
            Session["CE_IDEPARTAMENTO"] = null;
            Session["CE_IDMATERIAL"] = null;
        }
        

        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        public void Limpiar()
        {
            //rbExpuestoRDNo.Checked = false;

        }

        void CargarDatos()
        {
            if (HttpContext.Current.Session["CE_CABLEADO"] == null)
            {
                //Obtener Materiales
                try
                {
                    String vQuery = "STEISP_CABLESTRUCTURADO_Datos 6, " + ddlMateriales.SelectedValue;
                    DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                    DataTable vData = (DataTable)Session["CE_CABLEADO"];

                    if (vData == null)
                        vData = vDatos.Clone();

                    if (vDatos != null)
                        vData.Rows.Add(vDatos.Rows[0]["idMaterial"].ToString(), vDatos.Rows[0]["nombre"].ToString(), vDatos.Rows[0]["cantidad"].ToString(), vDatos.Rows[0]["medidas"].ToString() + " " + vDatos.Rows[0]["unidades"].ToString());


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
                    String vQuery = "STEISP_CABLESTRUCTURADO_Datos 1";
                    DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                    ddlAgencia.Items.Clear();
                    ddlAgencia.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        ddlAgencia.Items.Add(new ListItem { Value = item["idAgencia"].ToString(), Text = item["nombre"].ToString() });
                    }

                }
                catch (Exception Ex)
                {
                    Mensaje(Ex.Message, WarningType.Danger);
                }

                //Obtener todos los Responsables.
                try
                {
                    String vQuery = "STEISP_CABLESTRUCTURADO_Datos 2";

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

                //Obtener materiales
                try
                {
                    String vQuery = "STEISP_CABLESTRUCTURADO_Datos 4";

                    DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                    //ddlResponsable.Items.Clear();
                    ddlMateriales.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        ddlMateriales.Items.Add(new ListItem { Value = item["idMaterial"].ToString(), Text = item["nombre"].ToString() });
                    }

                }
                catch (Exception Ex)
                {
                    Mensaje(Ex.Message, WarningType.Danger);
                }

                Session["CE_CABLEADO"] = "1";
            }
        }

        protected void ddlResponsable_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            { 
                String vQuery = "STEISP_CABLESTRUCTURADO_CrearEstudio 3, '" + ddlResponsable.SelectedValue + "'";

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
                String vQuery = "STEISP_CABLESTRUCTURADO_CrearEstudio 1, '" + ddlAgencia.SelectedValue + "'";

                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                Session["CE_IDAGENCIA"] = vDatos.Rows[0]["idAgencia"].ToString();
                Session["CE_IDEPARTAMENTO"] = vDatos.Rows[0]["idDepartamento"].ToString();
                txtDireccion.Text = vDatos.Rows[0]["direccion"].ToString();
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
                String vQuery = "STEISP_CABLESTRUCTURADO_CrearEstudio 4, '" + ddlMateriales.SelectedValue + "'";
              
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                Session["CE_IDMATERIAL"] = vDatos.Rows[0]["  idMaterial"].ToString();
                
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        protected void GVMateriales_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {

                GVMateriales.DataSource = (DataTable)Session["CE_CABLEADO"];
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

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {

                DataTable vData = new DataTable();
                DataTable vDatos = (DataTable)Session["CE_MATERIALES"];
                string vMaterial = ddlMateriales.SelectedValue;
                string vNombreMaterial = ddlMateriales.SelectedItem.Text;

                vData.Columns.Add("numero");
                vData.Columns.Add("nombre");
                vData.Columns.Add("cantidad");

                if (vDatos == null)
                    vDatos = vData.Clone();

                if (vDatos != null)
                {
                    if (vDatos.Rows.Count < 1)
                        vDatos.Rows.Add(ddlMateriales.SelectedValue, ddlMateriales.SelectedItem.Text, txtCantidad.Text);
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
                            vDatos.Rows.Add(ddlMateriales.SelectedValue, ddlMateriales.SelectedItem.Text, txtCantidad.Text);
                    }
                }

                GVMateriales.DataSource = vDatos;
                GVMateriales.DataBind();
                Session["CE_MATERIALES"] = vDatos;
                UpdateDivMateriales.Update();

            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Success);
            }
        }

        protected void rblDesordenado_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (rblDesordenado.SelectedValue == "si")
                {
                    imgDesordenado.Disabled = false;
                }
                else
                {
                    imgDesordenado.Disabled = true;
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
                if (rblExpuestoHumedad.SelectedValue == "si")
                {
                    imgExpuestoHumedad.Disabled = false;
                }
                else
                {
                    imgExpuestoHumedad.Disabled = true;
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
                    imgExpuestoRobo.Disabled = false;
                }
                else
                {
                    imgExpuestoRobo.Disabled = true;
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
                if (rblElementosAjenos.SelectedValue == "si")
                {
                    imgElementosAjenos.Disabled = false;
                }
                else
                {
                    imgElementosAjenos.Disabled = true;
                }
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        protected void rdlReubicar_TextChanged1(object sender, EventArgs e)
        {
            try
            {
                if (rdlReubicar.SelectedValue == "si")
                {
                    DivFormEstudio.Visible = true;
                }
                else
                {
                    DivFormEstudio.Visible = false;
                }
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }

        }

        
        protected void GVContabilidad_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            try
            {

                GVMateriales.DataSource = (DataTable)Session["CE_CONTABILIDAD"];
                GVMateriales.PageIndex = e.NewPageIndex;
                GVMateriales.DataBind();
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }

        }

        void CargarProceso()
        {
            try
            {
                DataTable vDatos = new DataTable();
                vDatos = vConexion.obtenerDataTable("STEISP_CABLESTRUCTURADO_CrearEstudio 5 ");

                GVContabilidad.DataSource = vDatos;
                GVContabilidad.DataBind();
                Session["CE_CONTABILIDAD"] = vDatos;

            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        protected void GVContabilidad_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string vContabilidad = e.CommandArgument.ToString();
                if (e.CommandName == "Modificar")
                {
                    txtModProveedor.Text = vContabilidad;

                    DataTable vDatos = new DataTable();
                    vDatos = vConexion.obtenerDataTable("STEISP_CABLESTRUCTURADO_CrearEstudio 6, '" + vContabilidad + "'");

                    foreach (DataRow item in vDatos.Rows)
                    {
                        Session["PRUEBA"] = item["proveedor"].ToString();
                        txtModMarca.Text = item["marca"].ToString();
                        txtModCostoUnitario.Text = item["precio"].ToString();
                    }

                }
                txtModIdInventario.Text = vContabilidad;
                txtModProveedor.Text = Session["PRUEBA"].ToString();



                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                //if (e.CommandName == "Modificar")
                //{
                //    txtModProveedor.Text = vContabilidad;
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModal();", true);
                //}


            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        protected void txtBuscarProceso_TextChanged(object sender, EventArgs e)
        {
            try
            {

                CargarProceso();

                String vBusqueda = txtBuscarProceso.Text;
                DataTable vDatos = (DataTable)Session["CE_CONTABILIDAD"];

                if (vBusqueda.Equals(""))
                {
                    GVContabilidad.DataSource = vDatos;
                    GVContabilidad.DataBind();
                    udpContabilidad.Update();
                }
                else
                {
                    EnumerableRowCollection<DataRow> filtered = vDatos.AsEnumerable()
                        .Where(r => r.Field<String>("marca").Contains(vBusqueda.ToUpper()));

                    Boolean isNumeric = int.TryParse(vBusqueda, out int n);

                    if (isNumeric)
                    {
                        if (filtered.Count() == 0)
                        {
                            filtered = vDatos.AsEnumerable().Where(r =>
                                Convert.ToInt32(r["idInventario"]) == Convert.ToInt32(vBusqueda));
                        }
                    }


                    DataTable vDatosFiltrados = new DataTable();
                    vDatosFiltrados.Columns.Add("idInventario");
                    vDatosFiltrados.Columns.Add("proveedor");
                    vDatosFiltrados.Columns.Add("marca");
                    vDatosFiltrados.Columns.Add("precio");
                    foreach (DataRow item in filtered)
                    {
                        vDatosFiltrados.Rows.Add(
                            item["idInventario"].ToString(),
                            item["proveedor"].ToString(),
                            item["marca"].ToString(),
                            item["precio"].ToString()
                            );
                    }

                    GVContabilidad.DataSource = vDatosFiltrados;
                    GVContabilidad.DataBind();
                    Session["CE_CONTABILIDAD"] = vDatosFiltrados;
                    udpContabilidad.Update();
                }
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }

        }

        public void CerrarModal(String vModal)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Pop", "$('#" + vModal + "').modal('hide');", true);
        }

        protected void BtnModCambiarMaterial_Click(object sender, EventArgs e)
        {
            try
            {
                if (!txtModIdInventario.Text.Equals(""))
                {
                    string vMensaje = "";

                    String vQuery = "STEISP_CABLESTRUCTURADO_CrearEstudio 9," + txtModIdInventario.Text + "," +

                                                                     "'" + txtModProveedor.Text + "'," +
                                                                     "'" + txtModMarca.Text + "'," +
                                                                     "'" + txtModCostoUnitario.Text + "'";

                    Int32 vInformacion = vConexion.ejecutarSql(vQuery);

                    vMensaje = "Actualizado con Exito!";

                    if (vInformacion == 1)
                    {

                        vMensaje = "Actualizado con Exito!";
                        CerrarModal("ModificarMaterialModal");
                        Mensaje("Actualizado con Exito!", WarningType.Success);
                    }
                    else
                    {
                        vMensaje = "No se pudo actualizar!";
                        CerrarModal("ModificarMaterialModal");
                    }

                }
                else
                {
                    throw new Exception("Las contraseñas ingresadas no coinciden.");
                }
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        void ValidarFormDatosGenerales()
        {

            if (ddlResponsable.SelectedIndex == 0)
            {
                throw new Exception("Seleccione el Responsable");
            }

            if (ddlAgencia.SelectedIndex == 0)
            {
                throw new Exception("Seleccione la Agencia");
            }

            if (txtFechaEstudio.Text == "" || txtFechaEnvio.Text == string.Empty)
            {
                throw new Exception("Ingrese la Fecha de Envío");
            }
            if (txtFechaEstudio.Text == "" || txtFechaEstudio.Text == string.Empty)
            {
                throw new Exception("Ingrese la Fecha de Estudio");
            }

        }

        void ValidarFormMateriales()
        {

            if (ddlMateriales.SelectedIndex == 0)
            {
                throw new Exception("Seleccione un Material");
            }
            if (txtCantidad.Text == "" || txtCantidad.Text == string.Empty)
            {
                throw new Exception("Ingrese una cantidad");
            }
           
        }

        void ValidarFormEstimacionRecursos()
        {
            if (txtHorasTrabajo.Text == "" || txtHorasTrabajo.Text == string.Empty)
            {
                throw new Exception("Ingrese las Horas Trabajadas");
            }
            if (txtParticipantes.Text == "" || txtParticipantes.Text == string.Empty)
            {
                throw new Exception("Ingrese los Participantes");
            }
            if (rblTranporte.SelectedValue== "")
            {
                throw new Exception("Seleccione si utilizó transporte");
            }
            if (rblALimentación.SelectedValue == "")
            {
                throw new Exception("Seleccione si utilizó Alimentación");
            }
        }

        void ValidarFormAprobacion()
        {
            if (rblAprobada.SelectedValue == "")
            {
                throw new Exception("Seleccione si es aprobado el estudio");
            }
        }

        protected void btnEnviarAprobacion_Click(object sender, EventArgs e)
        {
            ValidarFormDatosGenerales();
            ValidarFormMateriales();
            //ValidarFormEstudioPrevio();
            ValidarFormEstimacionRecursos();
            ValidarFormAprobacion();
        }
    }
}