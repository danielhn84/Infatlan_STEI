using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using Excel;
using Infatlan_STEI_ATM.clases;
using System.Data;

namespace Infatlan_STEI_ATM.pages.calendario
{
    public partial class mantenimientos : System.Web.UI.Page
    {
        bd vConexion = new bd();
        protected void Page_Load(object sender, EventArgs e){
            if (!Page.IsPostBack){
                if (Convert.ToBoolean(Session["AUTH"])){

                }else {
                    Response.Redirect("/login.aspx");
                }
            }
        }

        public Boolean cargarArchivo(String DireccionCarga, ref int vSuccess, ref int vError, String vUsuario, String TipoProceso){
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

        private bool verificarRow(DataRow dr){
            int contador = 0;
            foreach (var value in dr.ItemArray){
                if (value.ToString() != ""){
                    contador++;
                }
            }

            if (contador > 0)
                return false;
            else
                return true;
        }
        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }
        public void procesarArchivo(DataSet vArchivo, ref int vSuccess, string DireccionCarga, string TipoProceso){
            try{
                if (vArchivo.Tables[0].Rows.Count > 0){
                    DataTable vDatos = vArchivo.Tables[0];
                    string vQuery = "";
                    //Boolean idEmpleado = false;
                    Session["CODATM_SUBIDO"] = "Completo";
                    Session["FECHA_SUBIDO"] = "Completo";
                    for (int i = 0; i < vDatos.Rows.Count; i++)
                    {
                        
                        String CodATM = vDatos.Rows[i]["CodigoATM"].ToString();
                        String Fecha = vDatos.Rows[i]["FECHA"].ToString();
                        //String vFormato = "yyyy/MM/dd"; //"dd/MM/yyyy HH:mm:ss"
                        string vFechaMant = Convert.ToDateTime(Fecha).Year.ToString();

                        String vQuery2 = "STEISP_ATM_VERIFICACION 7, '" + CodATM + "',1";
                        DataTable vDatos2 = vConexion.ObtenerTabla(vQuery2);
                        foreach (DataRow item in vDatos2.Rows)
                        {
                            Session["CODATM_MANT"] = item["codATM"].ToString();
                        }

                        if (Session["CODATM_MANT"].ToString() != CodATM)
                        {
                            if (Session["CODATM_SUBIDO"].ToString() == "Completo")
                                Session["CODATM_SUBIDO"] = "";


                            Session["CODATM_SUBIDO"] = Session["CODATM_SUBIDO"] +", "+ CodATM;
                          
                        }
                        DateTime today = DateTime.Today;
                        string vYear = Convert.ToString(today.Year);
                        if (vFechaMant!=vYear)
                        {
                            if (Session["FECHA_SUBIDO"].ToString() == "Completo")
                                Session["FECHA_SUBIDO"] = "";
                            

                            Session["FECHA_SUBIDO"] = Session["FECHA_SUBIDO"] + ", " + CodATM;
                        }

                    }



                    if (Session["CODATM_SUBIDO"].ToString()!="Completo" || Session["FECHA_SUBIDO"].ToString() != "Completo")
                        throw new Exception();
                    else
                    {
                        if (TipoProceso == "LISTA_MAN")
                        {
                            Boolean vCodATM = false, vFecha = false;

                            foreach (DataColumn item in vDatos.Columns)
                            {
                                if (item.ColumnName.ToString() == "CodigoATM")
                                    vCodATM = true;
                                if (item.ColumnName.ToString() == "FECHA")
                                    vFecha = true;
                            }

                            if (vCodATM && vFecha)
                            {
                                for (int i = 0; i < vDatos.Rows.Count; i++)
                                {
                                    String CodATM = vDatos.Rows[i]["CodigoATM"].ToString();
                                    String Fecha = vDatos.Rows[i]["FECHA"].ToString();


                                    String vFormato = "yyyy/MM/dd"; //"dd/MM/yyyy HH:mm:ss"
                                    String vFechaMant = Convert.ToDateTime(Fecha).ToString(vFormato);

                                    vQuery = "STEISP_ATM_Mantenimientos '" + vFechaMant + "'" +
                                        ",'" + CodATM + "'" +
                                        ",'" + Session["USUARIO"].ToString() + "'";

                                    
                                    int vRespuesta = vConexion.ejecutarSQL(vQuery);
                                    //VALIDA QUE ATM ESTE ACTIVO
                                    String vQuery2 = "STEISP_ATM_VERIFICACION 8, '" + CodATM + "',1";
                                    DataTable vDatos2 = vConexion.ObtenerTabla(vQuery2);
                                    if (vRespuesta == 1)
                                        vSuccess++;

                                }
                            }
                        }
                    }

                
                }
                else
                  
                throw new Exception("No contiene ninguna hoja de excel.");
            }catch (Exception ex){
                LbMensaje.Text=ex.Message;
            }
        }

        protected void BtnEnviar_Click1(object sender, EventArgs e){
            String archivoLog = string.Format("{0}_{1}", Convert.ToString(Session["USUARIO"]), DateTime.Now.ToString("yyyyMMdd"));

            try{
                String vDireccionCarga = ConfigurationManager.AppSettings["RUTA_SERVER"].ToString();
                if (FUMantenimientos.HasFile){
                    String vNombreArchivo = FUMantenimientos.FileName;
                    vDireccionCarga += "/" + archivoLog + "_" + vNombreArchivo;
                    FUMantenimientos.SaveAs(vDireccionCarga);
                    String vTipoPermiso = "LISTA_MAN";
                    Boolean vCargado = false;
                    int vSuccess = 0, vError = 0;
                    if (File.Exists(vDireccionCarga))
                        vCargado = cargarArchivo(vDireccionCarga, ref vSuccess, ref vError, Convert.ToString(Session["USUARIO"]), vTipoPermiso);

                    if (vCargado)  
                        LbMensaje.Text = "Archivo cargado con exito." + "<br>" + "<b style='color:green;'>Success:</b> " + vSuccess.ToString() + "&emsp;";
                    if(Session["CODATM_SUBIDO"].ToString()!= "Completo" && Session["FECHA_SUBIDO"].ToString() == "Completo")
                        LbMensaje.Text = "Código " + Session["CODATM_SUBIDO"].ToString() + " no existe." + "<br>" + "<b style='color:green;'>Success:</b> " + vSuccess.ToString() + "&emsp;";
                    if (Session["CODATM_SUBIDO"].ToString() == "Completo" && Session["FECHA_SUBIDO"].ToString() != "Completo")
                        LbMensaje.Text = "Fecha erronea de código " + Session["FECHA_SUBIDO"].ToString() + "<br>" + "<b style='color:green;'>Success:</b> " + vSuccess.ToString() + "&emsp;";
                    if (Session["CODATM_SUBIDO"].ToString() != "Completo" && Session["FECHA_SUBIDO"].ToString() != "Completo")
                        LbMensaje.Text = "Código " + Session["CODATM_SUBIDO"].ToString() + " no existe." + "<br>" + "Fecha erronea de código " + Session["FECHA_SUBIDO"].ToString() + "<br>" + "<b style='color:green;'>Success:</b> " + vSuccess.ToString() + "&emsp;";
                    
                        
                    
                }
                else
                    LbMensaje.Text = "No se encontró ningún archivo a cargar.";
            }catch (Exception Ex){
                LbMensaje.Text = Ex.Message;
            }
        }
    }
}