using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using System.Data;
using Infatlan_STEI_Agencias.classes;
using Excel;



namespace Infatlan_STEI_Agencias.pages.configuraciones
{
    public partial class calendarioMantenimientoAnual : System.Web.UI.Page
    {

        db vConexion = new db();
        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["USUARIO"] = "acamador";
            if (!Page.IsPostBack)
            {

            }
        }

 

        public Boolean cargarArchivo(String DireccionCarga, ref int vSuccess, ref int vError, String vUsuario, String TipoProceso)
        {
            Boolean vResultado = false;
            try
            {
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
                for (int i = 0; i < vDatosVerificacion.Tables[0].Rows.Count; i++)
                {
                    if (verificarRow(vDatosVerificacion.Tables[0].Rows[i]))
                        vDatosExcel.Tables[0].Rows[i].Delete();
                }
                vDatosExcel.Tables[0].AcceptChanges();

                procesarArchivo(vDatosExcel, ref vSuccess, DireccionCarga, TipoProceso);

                vResultado = true;

            }
            catch (Exception Ex)
            {
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

                    Session["AG_CMA_COD_AGENCIA_SUBIDO"] = "Completo";
                    Session["AG_CMA_FECHA_SUBIDO"] = "Completo";
                    Session["AG_CMA_AREA_SUBIDO"] = "Completo";

                    for (int i = 0; i < vDatos.Rows.Count; i++)
                    {

                        String CodAgencia = vDatos.Rows[i]["codigoAgencia"].ToString();
                        String Fecha = vDatos.Rows[i]["fechaMantenimiento"].ToString();
                        String Area = vDatos.Rows[i]["idAreaAgencia"].ToString();
                        //String vFormato = "yyyy/MM/dd"; //"dd/MM/yyyy HH:mm:ss"
                        string vFechaMant = Convert.ToDateTime(Fecha).Year.ToString();

                        string vCodigoValidar = "";
                        string vAreaValidar = "";

                        String vQuery2 = "STEISP_AGENCIA_CreacionAgencia 8, " + CodAgencia;
                        DataTable vDatos2 = vConexion.obtenerDataTable(vQuery2);
                        foreach (DataRow item in vDatos2.Rows)
                        {
                            vCodigoValidar = item["codigoAgencia"].ToString();
                        }

                        if (vCodigoValidar != CodAgencia)
                        {
                            if (Session["AG_CMA_COD_AGENCIA_SUBIDO"].ToString() == "Completo")
                                Session["AG_CMA_COD_AGENCIA_SUBIDO"] = "";

                            Session["AG_CMA_COD_AGENCIA_SUBIDO"] = Session["AG_CMA_COD_AGENCIA_SUBIDO"] + ", " + CodAgencia;

                        }

                        String vQuery3 = "STEISP_AGENCIA_CreacionAgencia 9, " + Area;
                        DataTable vDatos3 = vConexion.obtenerDataTable(vQuery3);
                        foreach (DataRow item in vDatos3.Rows)
                        {
                            vAreaValidar = item["idAreaAgencia"].ToString();
                        }

                        if (vAreaValidar != Area)
                        {
                            if (Session["AG_CMA_AREA_SUBIDO"].ToString() == "Completo")
                                Session["AG_CMA_AREA_SUBIDO"] = "";

                            Session["AG_CMA_AREA_SUBIDO"] = Session["AG_CMA_AREA_SUBIDO"] + ", " + Area;
                        }

                        if (vFechaMant != "2020")
                        {
                            if (Session["AG_CMA_FECHA_SUBIDO"].ToString() == "Completo")
                                Session["AG_CMA_FECHA_SUBIDO"] = "";


                            Session["AG_CMA_FECHA_SUBIDO"] = Session["AG_CMA_FECHA_SUBIDO"] + ", " + CodAgencia;
                        }

                    }

                    if (Session["AG_CMA_COD_AGENCIA_SUBIDO"].ToString() != "Completo" || Session["AG_CMA_FECHA_SUBIDO"].ToString() != "Completo" || Session["AG_CMA_AREA_SUBIDO"].ToString() != "Completo")
                        throw new Exception("Los mantenimientos no se guardaron, se detectaron los siguientes inconvenientes: ");
                    else
                    {
                        if (TipoProceso == "LISTA_MANTENIMIENTOS")
                        {
                            Boolean vcodigoAgencia = false, vFecha = false, varea = false;

                            foreach (DataColumn item in vDatos.Columns)
                            {
                                if (item.ColumnName.ToString() == "codigoAgencia")
                                    vcodigoAgencia = true;
                                if (item.ColumnName.ToString() == "fechaMantenimiento")
                                    vFecha = true;

                                if (item.ColumnName.ToString() == "idAreaAgencia")
                                    varea = true;
                            }

                            if (vcodigoAgencia && vFecha && varea)
                            {
                                for (int i = 0; i < vDatos.Rows.Count; i++)
                                {
                                    String codigoAgencia = vDatos.Rows[i]["codigoAgencia"].ToString();
                                    String fechaMantenimiento = vDatos.Rows[i]["fechaMantenimiento"].ToString();
                                    String idAreaAgencia = vDatos.Rows[i]["idAreaAgencia"].ToString();

                                    String vFormato = "yyyy/MM/dd"; //"dd/MM/yyyy HH:mm:ss"
                                    String vFechaMant = Convert.ToDateTime(fechaMantenimiento).ToString(vFormato);

                                    vQuery = "STEISP_AGENCIA_CreacionAgencia 7, '" + codigoAgencia + "'" +
                                        ",'" + vFechaMant + "'" +
                                        ",'" + idAreaAgencia + "'";

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
        

        protected void BtnEnviar_Click1(object sender, EventArgs e)
        {
            String archivoLog = string.Format("{0}_{1}", Convert.ToString(Session["USUARIO"]), DateTime.Now.ToString("yyyyMMdd"));

            try
            {
                String vDireccionCarga = ConfigurationManager.AppSettings["RUTA_SERVER"].ToString();
                if (FUMantenimientosAgencia.HasFile)
                {
                    String vNombreArchivo = FUMantenimientosAgencia.FileName;
                    vDireccionCarga += "/" + archivoLog + "_" + vNombreArchivo;
                    FUMantenimientosAgencia.SaveAs(vDireccionCarga);
                    String vTipoPermiso = "LISTA_MANTENIMIENTOS";
                    Boolean vCargado = false;
                    int vSuccess = 0, vError = 0;
                    if (File.Exists(vDireccionCarga))
                        vCargado = cargarArchivo(vDireccionCarga, ref vSuccess, ref vError, Convert.ToString(Session["USUARIO"]), vTipoPermiso);

                    if (vSuccess == 0)
                    {
                        if (Session["AG_CMA_COD_AGENCIA_SUBIDO"].ToString() != "Completo")
                            LbMensaje.Text = LbMensaje.Text + "Código: " + Session["AG_CMA_COD_AGENCIA_SUBIDO"].ToString() + " no existe, favor verificar que este agregado en la seccion de agencias." + "&emsp;";

                        if (Session["AG_CMA_FECHA_SUBIDO"].ToString() != "Completo")
                            LbMensaje.Text = LbMensaje.Text + "Código " + Session["AG_CMA_FECHA_SUBIDO"].ToString() + " fechas erroneas, favor verificar que sean fechas del año actual." + "&emsp;";

                        if (Session["AG_CMA_AREA_SUBIDO"].ToString() != "Completo")
                            LbMensaje.Text = LbMensaje.Text + "Area " + Session["AG_CMA_AREA_SUBIDO"].ToString() + " tipo area no existe, favor verificar que esten ingresadas y activas." + "&emsp;";
                        DivAlerta.Visible = true;
                        UpdateModal.Update();

                    }
                    else
                    {
                        //LbMensaje.Text = "Archivo cargado con exito." + "<br>" + "<b style='color:green;'>Success:</b> " + vSuccess.ToString() + "&emsp;";
                        Mensaje("Tipo de agencia actualizado con exito. ", WarningType.Success);
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

             }
        }

      

        protected void BtnCancelar_Click1(object sender, EventArgs e)
        {
            try
            {
                Mensaje("Acción cancelado con exito. ", WarningType.Success);
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
    }
}