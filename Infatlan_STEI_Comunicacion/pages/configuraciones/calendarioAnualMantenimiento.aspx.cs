using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infatlan_STEI_Comunicacion.classes;
using System.Data;
using System.IO;
using Excel;
using System.Configuration;


namespace Infatlan_STEI_Comunicacion.pages.configuraciones
{
    public partial class calendarioAnualMantenimiento : System.Web.UI.Page
    {
        db vConexion = new db();
        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Session["USUARIO"] = "acamador";
            if (!Page.IsPostBack)
            {
                if (Convert.ToBoolean(Session["AUTH"]))
                {
                    cargar();
                }
                else
                {
                    Response.Redirect("/login.aspx");
                }


 
            }
        }

        private void cargar()
        {
            try
            {
                DdlResponsable.Items.Clear();
                DDLNodo.Items.Clear();
                String vQuery = "STEISP_COMUNICACION_AsignarResponsable 2";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                DdlResponsable.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                if (vDatos.Rows.Count > 0)
                {
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DdlResponsable.Items.Add(new ListItem { Value = item["idUsuario"].ToString(), Text = item["nombre"].ToString() });
                    }
                }

                vQuery = "STEISP_COMUNICACION_AsignarResponsable 6";
                vDatos = vConexion.obtenerDataTable(vQuery);
                DDLNodo.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                if (vDatos.Rows.Count > 0)
                {
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLNodo.Items.Add(new ListItem { Value = item["idStockEDC"].ToString(), Text = item["nombreNodo"].ToString() + " (" + item["tipoStock"].ToString() + ")" });
                    }
                }


            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void CBEmergencia_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LbValidacion.Text = "";
                DivMensaje.Visible = false;
                UpdatePanel6.Update();

                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "abrirModalEmergencia();", true);
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void DDLNodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String vQuery = "STEISP_COMUNICACION_AsignarResponsable 7,"+ DDLNodo.SelectedValue;
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                TxLugar.Text= vDatos.Rows[0]["direccion"].ToString();

            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        private void validacionesEmergencia()
        {
            if (DDLNodo.SelectedValue.Equals("0"))
                throw new Exception("Favor seleccione nodo al que se le va proporcionar mantenimiento.");

            if (TxFechaMantenimiento.Text == "" || TxFechaMantenimiento.Text == string.Empty)
                throw new Exception("Favor seleccione fecha del mantenimiento.");

            if (DdlResponsable.SelectedValue.Equals("0"))
                throw new Exception("Favor seleccione tecnico responsable.");

            if (TxMotivo.Text == "" || TxMotivo.Text == string.Empty)
                throw new Exception("Favor ingrese detalle del motivo.");
        }

        private void limpiarEmergencia()
        {
            TxMotivo.Text = string.Empty;
            DdlResponsable.SelectedValue.Equals("0");
            TxFechaMantenimiento.Text = string.Empty;
            DDLNodo.SelectedValue.Equals("0");
            CBEmergencia.Checked = false;
            UpdatePanel2.Update();

        }

        protected void BtnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                validacionesEmergencia();
                String vFormato = "yyyy/MM/dd"; //"dd/MM/yyyy HH:mm:ss"
                String vFechaMant = Convert.ToDateTime(TxFechaMantenimiento.Text).ToString(vFormato);

                String vQuery = "STEISP_COMUNICACION_AsignarResponsable 8," 
                                + DDLNodo.SelectedValue 
                                +",'"+ vFechaMant
                                +"','"+ Session["USUARIO"]
                                + "','" + DdlResponsable.SelectedValue
                                + "','" + Session["USUARIO"]
                                +"','1','" + TxMotivo.Text + "'";
                Int32 vInformacion1 = vConexion.ejecutarSql(vQuery);
                limpiarEmergencia();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "cerrarModalEmergencia();", true);
                if (vInformacion1 == 1)
                {
                    Mensaje("Se guardaron exitosamente los registros.", WarningType.Success);
                }
                else
                {
                    Mensaje("Favor contactarse con el administrador, hubo un problema al guardar los registros.", WarningType.Danger);
                }
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
                LbValidacion.Text = ex.Message;
                DivMensaje.Visible = true;
                UpdatePanel6.Update();
            }
        }

        public Boolean cargarArchivo(String DireccionCarga, ref int vSuccess, ref int vError, String vUsuario, String TipoProceso)
        {

            Div1.Visible = false;
            UpdatePanel1.Update();
            DivAlerta.Visible = false;
            UpdateModal.Update();

            Boolean vResultado = false;
            try{
                FileStream stream = File.Open(DireccionCarga, FileMode.Open, FileAccess.Read);
                IExcelDataReader excelReader;
                if (DireccionCarga.Contains("xlsx"))
                    excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);   //2007
                else
                    excelReader = ExcelReaderFactory.CreateBinaryReader(stream);    //97-2003

                excelReader.IsFirstRowAsColumnNames = true;
                DataSet vDatosExcel = excelReader.AsDataSet();
                excelReader.Close();

                DataSet vDatosVerificacion = vDatosExcel.Copy();
                for (int i = 0; i < vDatosVerificacion.Tables[0].Rows.Count; i++){
                    if (verificarRow(vDatosVerificacion.Tables[0].Rows[i]))
                        vDatosExcel.Tables[0].Rows[i].Delete();
                }
                vDatosExcel.Tables[0].AcceptChanges();

                procesarArchivo(vDatosExcel, ref vSuccess, DireccionCarga, TipoProceso);

                vResultado = true;

            }catch (Exception Ex){
                throw new Exception(Ex.ToString());
            }
            return vResultado;
        }

        private bool verificarRow(DataRow dr)
        {
            int contador = 0;
            foreach (var value in dr.ItemArray)
            {
                if (value.ToString() != "")
                {
                    contador++;
                }
            }

            if (contador > 0)
                return false;
            else
                return true;
        }

        public void procesarArchivo(DataSet vArchivo, ref int vSuccess, string DireccionCarga, string TipoProceso)
        {
            try
            {
                if (vArchivo.Tables[0].Rows.Count > 0)
                {
                    DataTable vDatos = vArchivo.Tables[0];
                    string vQuery = "";
                    Session["COMUNICACION_CAM_COD_SUBIDO"] = "Completo";
                    Session["COMUNICACION_CAM_FECHA_SUBIDO"] = "Completo";
  
                    for (int i = 0; i < vDatos.Rows.Count; i++)
                    {
                        String CodEquipoComu = vDatos.Rows[i]["idStockEDC"].ToString();
                        String Fecha = vDatos.Rows[i]["fechaMantenimiento"].ToString();
                        string vFechaMant = Convert.ToDateTime(Fecha).Year.ToString();

                        string vCodigoValidar = "";
                        String vQuery2 = "STEISP_COMUNICACION_AsignarResponsable 9, " + CodEquipoComu;
                        DataTable vDatos2 = vConexion.obtenerDataTable(vQuery2);
                        foreach (DataRow item in vDatos2.Rows)
                        {
                            vCodigoValidar = item["idStockEDC"].ToString();
                        }

                        if (vCodigoValidar != CodEquipoComu)
                        {
                            if (Session["COMUNICACION_CAM_COD_EQUIPO_SUBIDO"].ToString() == "Completo")
                                Session["COMUNICACION_CAM_COD_EQUIPO_SUBIDO"] = "";

                            Session["COMUNICACION_CAM_COD_EQUIPO_SUBIDO"] = Session["COMUNICACION_CAM_COD_EQUIPO_SUBIDO"] + ", " + CodEquipoComu;
                        }

                        DateTime vFechaActual = DateTime.Now.Date;
                        string vAño =Convert.ToString(vFechaActual.Year);
                        if (vFechaMant != vAño)
                        {
                            if (Session["COMUNICACION_CAM_FECHA_SUBIDO"].ToString() == "Completo")
                                Session["COMUNICACION_CAM_FECHA_SUBIDO"] = "";

                            Session["COMUNICACION_CAM_FECHA_SUBIDO"] = Session["COMUNICACION_CAM_FECHA_SUBIDO"] + ", " + CodEquipoComu;
                        }
                    }





                    if (Session["COMUNICACION_CAM_COD_SUBIDO"].ToString() != "Completo" || Session["COMUNICACION_CAM_FECHA_SUBIDO"].ToString() != "Completo")
                    {
                        throw new Exception("Los mantenimientos preventivos no se guardaron, se detectaron los siguientes inconvenientes: ");
                    }               
                    else
                    {
                        if (TipoProceso == "LISTADO_MANTENIMIENTOS")
                        {
                            Boolean vcodigoEDC = false, vFecha = false;

                            foreach (DataColumn item in vDatos.Columns)
                            {
                                if (item.ColumnName.ToString() == "idStockEDC")
                                    vcodigoEDC = true;
                                if (item.ColumnName.ToString() == "fechaMantenimiento")
                                    vFecha = true;
                            }

                            if (vcodigoEDC && vFecha)
                            {
                                for (int i = 0; i < vDatos.Rows.Count; i++)
                                {
                                    String codigoEDC = vDatos.Rows[i]["idStockEDC"].ToString();
                                    String fechaMantenimiento = vDatos.Rows[i]["fechaMantenimiento"].ToString();

                                    String vFormato = "yyyy/MM/dd"; //"dd/MM/yyyy HH:mm:ss"
                                    String vFechaMant = Convert.ToDateTime(fechaMantenimiento).ToString(vFormato);

                                    vQuery = "STEISP_COMUNICACION_AsignarResponsable 10, '" + codigoEDC + "'" +
                                        ",'" + vFechaMant + "'" ;

                                    int vRespuesta = vConexion.ejecutarSql(vQuery);
                                    if (vRespuesta == 1)
                                        vSuccess++;
                                }
                            }
                        }
                    }
                }
                else
                    throw new Exception("No contiene ninguna hoja de excel.");
            }
            catch (Exception ex)
            {
                LbMensaje.Text = ex.Message;
            }
        }

        protected void BtnEnviar_Click1(object sender, EventArgs e){
            String archivoLog = string.Format("{0}_{1}", Convert.ToString(Session["USUARIO"]), DateTime.Now.ToString("yyyyMMdd"));
            try{
                Div1.Visible = false;
                UpdatePanel1.Update();
                DivAlerta.Visible = false;
                UpdateModal.Update();

                String vDireccionCarga = ConfigurationManager.AppSettings["RUTA_SERVER"].ToString();
                if (FUMantenimientosComunicacion.HasFile){
                    String vNombreArchivo = FUMantenimientosComunicacion.FileName;
                    vDireccionCarga += "/" + archivoLog + "_" + vNombreArchivo;
                    FUMantenimientosComunicacion.SaveAs(vDireccionCarga);
                    String vTipoPermiso = "LISTADO_MANTENIMIENTOS";
                    Boolean vCargado = false;
                    int vSuccess = 0, vError = 0;
                    if (File.Exists(vDireccionCarga))
                        vCargado = cargarArchivo(vDireccionCarga, ref vSuccess, ref vError, Convert.ToString(Session["USUARIO"]), vTipoPermiso);

                    if (vSuccess == 0)
                    {
                        if (Session["COMUNICACION_CAM_COD_EQUIPO_SUBIDO"].ToString() != "Completo")
                            LbMensaje.Text = LbMensaje.Text + "Código: " + Session["COMUNICACION_CAM_COD_EQUIPO_SUBIDO"].ToString() + " no existe, favor verificar que este agregado en la base de datos el equipo de comunicacion." + "&emsp;";

                        if (Session["COMUNICACION_CAM_FECHA_SUBIDO"].ToString() != "Completo")
                            LbMensaje.Text = LbMensaje.Text + "Código " + Session["COMUNICACION_CAM_FECHA_SUBIDO"].ToString() + " fechas erroneas, favor verificar que sean fechas del año actual." + "&emsp;";

                        DivAlerta.Visible = true;
                        UpdateModal.Update();

                    }
                    else
                    {
                        LbMensajeSuccsess.Text = "Archivo mantenimiento preventivo cargado con exito." + "<br>" + "<b style='color:white;'>Registros exitosamente: </b> " + vSuccess.ToString() + "&emsp;";
                        Div1.Visible = true;
                        UpdatePanel1.Update();
                    }
                }
                else
                {
                    LbMensaje.Text = "No se encontró ningún archivo a cargar.";
                    DivAlerta.Visible = true;
                    UpdateModal.Update();
                }
            }
            catch (Exception ex)
            {
                LbMensaje.Text = ex.Message;
                DivAlerta.Visible = true;
                UpdateModal.Update();
            }
        }
    }
}