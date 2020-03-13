using Infatlan_STEI_Agencias.classes;
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Infatlan_STEI_Agencias.paginasAgencia{
    public partial class CreacionNotificacion : System.Web.UI.Page
    {

        db vConexion = new db();

        protected void Page_Load(object sender, EventArgs e){
            if (!IsPostBack){
                cargarData();
            }
        }

        internal void cargarData()
        {
            try
            {
                DDLNombreResponsable.Items.Clear();
                String vQuery = "STEISP_AGENCIA_CreacionNotificacion 5";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                DDLNombreResponsable.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                DDLNombreParticipantes.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                if (vDatos.Rows.Count > 0)
                {
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLNombreResponsable.Items.Add(new ListItem { Value = item["idUsuario"].ToString(), Text = item["nombre"].ToString() + "  " + item["apellidos"].ToString() });
                        DDLNombreParticipantes.Items.Add(new ListItem { Value = item["idUsuario"].ToString(), Text = item["nombre"].ToString() + "  " + item["apellidos"].ToString() });
                    }
                }

            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }


        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

            try
            {
                DDLCodigoAgencia.Items.Clear();
                String vTest = TextBox1.Text;
                String v2 = Convert.ToDateTime(vTest).ToString("yyyy-MM-dd");

               


                // DataTable vDatos = vConexion.obtenerDataTable("RSP_ObtenerRelojes 2," + vDuo);
                // String vQuery = "SELECT A.idMantenimiento, B.nombre ,A.idAgencia  FROM Agencias_Mantenimiento  A INNER JOIN [dbo].[Agencias_Principal] B ON A.idMantenimiento=B.codigoAgencia   WHERE fechaMantenimiento ='2020-03-01' ";
                // DataTable vDatos = vConexion.obtenerDataTable(vQuery);


                String vQuery = "STEISP_AGENCIA_CreacionNotificacion 1,'" + v2 + "'";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                DDLCodigoAgencia.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                if (vDatos.Rows.Count > 0)
                {
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLCodigoAgencia.Items.Add(new ListItem { Value = item["idMantenimiento"].ToString(), Text = item["codigoAgencia"].ToString() + " - " + item["nombre"].ToString() });
                    }


                    DDLCodigoAgencia.Enabled = true;
                    TxSysAid.ReadOnly = false;
                    TxHoraInicio.ReadOnly = false;
                    TxHoraFin.ReadOnly = false;
                    DDLNombreResponsable.Enabled = true;
                    DDLNombreParticipantes.Enabled = true;
                    TxCorreo.ReadOnly = false;
                    LBAgregar.Enabled = true;

                    TxMantEquipoComu.Text = "Si";
                    UpdatePanel1.Update();
                }
                else
                {
                    Mensaje("No hay mantenimientos disponibles en la fecha que selecciono: "+ TextBox1.Text+" , ingresar otra fecha.", WarningType.Info);
                }
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }


        protected void DDLCodigoAgencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                TxLugar.Text = DDLCodigoAgencia.SelectedItem.Text;
                String vQuery = "STEISP_AGENCIA_CreacionNotificacion 2," + DDLCodigoAgencia.SelectedValue;
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                TxArea.Text = vDatos.Rows[0]["nombre"].ToString();

                String vQuery1 = "STEISP_AGENCIA_CreacionNotificacion 3," + DDLCodigoAgencia.SelectedValue;
                DataTable vDatos1 = vConexion.obtenerDataTable(vQuery1);
                TxDepartamento.Text = vDatos1.Rows[0]["nombre"].ToString();

                String vQuery2 = "STEISP_AGENCIA_CreacionNotificacion 4," + DDLCodigoAgencia.SelectedValue;
                DataTable vDatos2 = vConexion.obtenerDataTable(vQuery2);
                TxDireccion.Text = vDatos2.Rows[0]["direccion"].ToString();
                TxTelefono.Text = vDatos2.Rows[0]["telefono"].ToString();
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void DDLNombreResponsable_SelectedIndexChanged(object sender, EventArgs e)
        {
           try
            {
                String vQuery2 = "STEISP_AGENCIA_CreacionNotificacion 6 ,'" + DDLNombreResponsable.SelectedValue + "'";
                DataTable vDatos2 = vConexion.obtenerDataTable(vQuery2);
                TxIdentidadResponsable.Text = vDatos2.Rows[0]["identidad"].ToString();
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }


        protected void DDLNombreParticipantes_TextChanged(object sender, EventArgs e)
        {
            try{
                if (DDLNombreParticipantes.SelectedValue.Equals("0"))
                    throw new Exception("Por favor seleccione tecnico participante.");

                foreach (GridViewRow item in GVBusqueda.Rows){
                    if (item.Cells[1].Text.Equals(DDLNombreParticipantes.Text)){
                        throw new Exception("Ya existe tecnico responsable.");
                    }               
                }
                
                String vQuery2 = "STEISP_AGENCIA_CreacionNotificacion 7 ," + DDLNombreParticipantes.SelectedValue;
                DataTable vDatos = vConexion.obtenerDataTable(vQuery2);
                DataTable vDatos2 = new DataTable();

                String vId = vDatos.Rows[0]["idUsuario"].ToString();
                String vNombre = vDatos.Rows[0]["nombre"].ToString();
                String vIdentidad = vDatos.Rows[0]["identidad"].ToString();

                if (HttpContext.Current.Session["TECNICOS_PARTICIPANTES"] == null){
                    vDatos2.Columns.Add("idUsuario");
                    vDatos2.Columns.Add("nombre");
                    vDatos2.Columns.Add("identidad");

                    vDatos2.Rows.Add(vId, vNombre, vIdentidad);
                }else{
                    vDatos2 = (DataTable)Session["TECNICOS_PARTICIPANTES"];
                    vDatos2.Rows.Add(vId, vNombre, vIdentidad);
                }
                GVBusqueda.DataSource= vDatos2;
                GVBusqueda.DataBind();
                Session["TECNICOS_PARTICIPANTES"] = vDatos2;
                DDLNombreParticipantes.SelectedIndex = -1;
            }
            catch (Exception Ex) { Mensaje(Ex.Message, WarningType.Danger); }
        }



        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {             
                if (TxCorreo.Text.Equals(""))
                    throw new Exception("Por favor llene el campo de correo.");

                foreach (GridViewRow item in GVCorreoJefeAgencia.Rows)
                {
                    if (item.Cells[1].Text.Equals(TxCorreo.Text))
                    {
                        throw new Exception("Ya existe este correo.");
                    }
                }

                DataTable vDatos = new DataTable();
                vDatos.Columns.Add("CorreoJefeAgencia");
                if (HttpContext.Current.Session["CORREOS"] == null)
                {
                    vDatos.Rows.Add(TxCorreo.Text);
                }
                else
                {
                    vDatos = (DataTable)Session["CORREOS"];                  
                    vDatos.Rows.Add(new Object[] { TxCorreo.Text });
                }
                GVCorreoJefeAgencia.DataSource = vDatos;
                GVCorreoJefeAgencia.DataBind();
                Session["CORREOS"] = vDatos;
                UpdatePanel1.Update();
                TxCorreo.Text = String.Empty;
            }
            catch (Exception Ex) { Mensaje(Ex.Message, WarningType.Danger); }
        }


        protected void GVCorreoJefeAgencia_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GVCorreoJefeAgencia.DataSource = (DataTable)Session["CORREOS"];
                GVCorreoJefeAgencia.PageIndex = e.NewPageIndex;
                GVCorreoJefeAgencia.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }


        protected void GVCorreoJefeAgencia_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DataTable vDatos = (DataTable)Session["CORREOS"];
            if (e.CommandName == "Eliminar")
            {
                String vCorreo = e.CommandArgument.ToString();
                if (Session["CORREOS"] != null)
                {
                    DataRow[] result = vDatos.Select("CorreoJefeAgencia = '" + vCorreo + "'");
                    foreach (DataRow row in result)
                    {
                        if (row["CorreoJefeAgencia"].ToString().Contains(vCorreo))
                            vDatos.Rows.Remove(row);
                    }
                }
            }
            GVCorreoJefeAgencia.DataSource = vDatos;
            GVCorreoJefeAgencia.DataBind();
            Session["CORREOS"] = vDatos;
        }

        private void validaciones(){
            if (TextBox1.Text == "" || TextBox1.Text == string.Empty)
                throw new Exception("Favor seleccione fecha del mantenimiento.");
            if (DDLCodigoAgencia.SelectedValue.Equals("0"))
                throw new Exception("Favor seleccione Código/Lugar Agencia.");
            if (TxSysAid.Text == "" || TxSysAid.Text == string.Empty)
                throw new Exception("Favor ingrese número de SysAid.");
            if (GVCorreoJefeAgencia.Rows.Count==0)
                throw new Exception("Favor ingrese correo de jefe de agencia.");
            if (DDLNombreResponsable.SelectedValue.Equals("0"))
                throw new Exception("Favor seleccione tecnico responsable."); 
        }

        protected void BtnEnviarNotificacion_Click(object sender, EventArgs e){
            try{
                validaciones();

                TxLugar.Text = DDLCodigoAgencia.SelectedItem.Text;
                TxFecha.Text = TextBox1.Text;
                TxHrInicioModal.Text = TxHoraInicio.Text;
                TxHrFinModal.Text = TxHoraFin.Text;
                TxResponsable.Text = DDLNombreResponsable.SelectedItem.Text;


                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);

            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DataTable vDatos = (DataTable)Session["TECNICOS_PARTICIPANTES"];
            if (e.CommandName == "Eliminar")
            {
                String vUsuario = e.CommandArgument.ToString();
                if (Session["TECNICOS_PARTICIPANTES"] != null)
                {

                    DataRow[] result = vDatos.Select("idUsuario = '" + vUsuario + "'");
                    foreach (DataRow row in result)
                    {
                        if (row["idUsuario"].ToString().Contains(vUsuario))
                            vDatos.Rows.Remove(row);
                    }
                }
            }
            GVBusqueda.DataSource = vDatos;
            GVBusqueda.DataBind();
            Session["TECNICOS_PARTICIPANTES"] = vDatos;
        }

        protected void GVBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e){
            try{
                GVBusqueda.DataSource = (DataTable)Session["TECNICOS_PARTICIPANTES"];
                GVBusqueda.PageIndex = e.NewPageIndex;
                GVBusqueda.DataBind();
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void btnModalEnviarNotificacion_Click(object sender, EventArgs e)
        {
            try
            {
                String vPrimero = "", vSegundo = "", vTercero = "";
                Int32 vInfo = 0;
                Int32 vInfo2 = 0;

                DataTable vDatos = (DataTable)Session["CORREOS"];
                if (vDatos.Rows.Count > 0){
                    for (int i = 0; i < vDatos.Rows.Count; i++){
                        string vCorreo = vDatos.Rows[i]["CorreoJefeAgencia"].ToString();
                        String vQuery = "STEISP_AGENCIA_CreacionNotificacion 8," + DDLCodigoAgencia.SelectedValue + "," + "'" + vCorreo + "'";
                        vInfo = vConexion.ejecutarSql(vQuery);

                    }
                }

                if (vInfo == 1 || vInfo == 2)
                {
                    vPrimero = "Se insertaron los jefes de agencia.";
   
                    String vQuery2 = "STEISP_AGENCIA_CreacionNotificacion 10," + "'" + TxHoraInicio.Text + "'" + "," + "'" + TxHoraFin.Text + "'" + "," + "'" + DDLNombreResponsable.SelectedValue + "'" + "," + "'" + DDLCodigoAgencia.SelectedValue + "'";
                    vInfo2 = vConexion.ejecutarSql(vQuery2);

                        if (vInfo2 == 1 || vInfo2 == 2)
                        vSegundo = "Se actualizó notificaciones";

                        if (Session["TECNICOS_PARTICIPANTES"] != null){
                        DataTable vDatosTecnicosParticipantes = (DataTable)Session["TECNICOS_PARTICIPANTES"];
                            if (vDatosTecnicosParticipantes.Rows.Count > 0){
                                 for (int num = 0; num < vDatosTecnicosParticipantes.Rows.Count; num++){
                                string idUsuario = vDatosTecnicosParticipantes.Rows[num]["idUsuario"].ToString();

                                String vQuery3 = "STEISP_AGENCIA_CreacionNotificacion 9," + DDLCodigoAgencia.SelectedValue + "," + "'" + idUsuario + "'";
                                Int32 vInfo3 = vConexion.ejecutarSql(vQuery3);
                                     if (vInfo3 == 1)
                                         vTercero = "Se agregaron tecnicos participantes.";
                                     }
                        }
                         }

            }

                if((vInfo==1 || vInfo == 2) && (vInfo2==1 || vInfo2 == 2))
                {
                    Mensaje("Notificación enviada con éxito.", WarningType.Success);
                    Limpiar();
                    BloquearCampos();
                   
                }
                else
                {
                    Mensaje("No se pudo enviar la notificación, favor contactarse con el administrador", WarningType.Danger);
        
                }



            }
            catch (Exception Ex) { Mensaje(Ex.Message, WarningType.Danger); 
           }
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
        }




        protected void btnModalCerrarNotificacion_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
        }




        private void BloquearCampos()
        {
            DDLCodigoAgencia.Enabled = false;
            TxSysAid.ReadOnly = true;
            TxHoraInicio.ReadOnly = true;
            TxHoraFin.ReadOnly = true;
            DDLNombreResponsable.Enabled = false;
            DDLNombreParticipantes.Enabled = false;
            TxCorreo.ReadOnly = true;
            LBAgregar.Enabled = false;
            UpdatePanel1.Update();
        }


        private void Limpiar()
        {
            DDLNombreResponsable.SelectedIndex = -1;
            TxIdentidadResponsable.Text = String.Empty;

            TextBox1.Text = String.Empty;
            DDLCodigoAgencia.SelectedIndex = -1;
            TxSysAid.Text = String.Empty;
            TxArea.Text = String.Empty;
            TxDepartamento.Text = String.Empty;
            TxMantEquipoComu.Text = String.Empty;
            TxDireccion.Text = String.Empty;
            TxTelefono.Text = String.Empty;
            TxHoraInicio.Text = String.Empty;
            TxHoraFin.Text = String.Empty;

            DDLNombreResponsable.SelectedIndex = -1;
            TxIdentidadResponsable.Text = String.Empty;

            DDLNombreParticipantes.SelectedIndex = -1;
            DDLCodigoAgencia.Items.Clear();

            Session["TECNICOS_PARTICIPANTES"] = null;
            Session["CORREOS"] = null;

            GVBusqueda.DataSource = null;
            GVBusqueda.DataBind();

            GVCorreoJefeAgencia.DataSource = null;
            GVCorreoJefeAgencia.DataBind();

            UpdatePanel1.Update();

        }

        protected void BtnCancelarNotificacion_Click(object sender, EventArgs e)
        {
            Limpiar();
            Mensaje("Notificación cancelada con exito", WarningType.Info);
            BloquearCampos();
        }
    }





}
