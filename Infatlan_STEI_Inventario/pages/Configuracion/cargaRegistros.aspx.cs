using Infatlan_STEI_Inventario.clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Excel;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace Infatlan_STEI_Inventario.pages.Configuracion
{
    public partial class cargaRegistros : System.Web.UI.Page
    {
        db vConexion = new db();
        protected void Page_Load(object sender, EventArgs e){
            if (!Page.IsPostBack){
                if (Convert.ToBoolean(Session["AUTH"])){
                    cargarDatos();
                }else {
                    Response.Redirect("/login.aspx");
                }
            }
        }

        private void cargarDatos(){
            try{
                DataTable vDatos = new DataTable();
                vDatos.Columns.Add("id");
                vDatos.Columns.Add("proceso");

                vDatos.Rows.Add("1", "Stock");
                vDatos.Rows.Add("2", "Equipo de comunicación.");
                vDatos.Rows.Add("3", "Enlaces");
                vDatos.Rows.Add("4", "Proveedores");
                vDatos.Rows.Add("5", "Contratos");
                vDatos.Rows.Add("6", "Tipo de Equipo");

                if (vDatos.Rows.Count > 0){
                    GvBusqueda.DataSource = vDatos;
                    GvBusqueda.DataBind();
                    Session["INV_CARGA"] = vDatos;
                }
                
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        public void Mensaje(string vMensaje, WarningType type){
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        protected void GvBusqueda_RowCommand(object sender, GridViewCommandEventArgs e){
            try{
                string vId = e.CommandArgument.ToString();
                if (e.CommandName == "DescargarPlantilla"){
                    if (vId == "1"){
                        Response.Redirect("/sites/inventario/pages/plantillas/plantillaArticulos.xlsx");
                    }else if (vId == "2") { 
                        Response.Redirect("/sites/inventario/pages/plantillas/plantillaEDC.xlsx");
                    }else if (vId == "3") { 
                        Response.Redirect("/sites/inventario/pages/plantillas/plantillaEnlaces.xlsx");
                    }else if (vId == "4") { 
                        Response.Redirect("/sites/inventario/pages/plantillas/plantillaProveedores.xlsx");
                    }else if (vId == "5") { 
                        Response.Redirect("/sites/inventario/pages/plantillas/plantillaContratos.xlsx");
                    }
                }else if (e.CommandName == "CargarRegistros"){
                    DivMensajeCarga.Visible = false;
                    if (vId == "1")
                        Session["INV_PROCESO_CARGA"] = "STOCK";
                    else if (vId == "2")  
                        Session["INV_PROCESO_CARGA"] = "EDC";
                    else if (vId == "3") 
                        Session["INV_PROCESO_CARGA"] = "ENLACE";
                    else if (vId == "4") 
                        Session["INV_PROCESO_CARGA"] = "PROVEEDOR";
                    else if (vId == "5")
                        Session["INV_PROCESO_CARGA"] = "CONTRATO";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalCarga();", true);
                }
                Session["INV_STOCK_ID"] = vId;
            }catch (Exception Ex){
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        protected void BtnCargar_Click(object sender, EventArgs e){
            String archivoLog = string.Format("{0}_{1}", Convert.ToString(Session["USUARIO"]), DateTime.Now.ToString("yyyyMMddHHmmss"));
            try{
                String vDireccionCarga = ConfigurationManager.AppSettings["RUTA_SERVER"].ToString();
                if (FUCarga.HasFile){
                    String vNombreArchivo = FUCarga.FileName;
                    vDireccionCarga += "/" + archivoLog + "_" + vNombreArchivo;

                    FUCarga.SaveAs(vDireccionCarga);
                    String vTipoProceso = Session["INV_PROCESO_CARGA"].ToString();
                    Boolean vCargado = false;
                    int vSuccess = 0, vError = 0;
                    if (File.Exists(vDireccionCarga))
                        vCargado = cargarArchivo(vDireccionCarga, ref vSuccess, ref vError, Convert.ToString(Session["USUARIO"]), vTipoProceso);

                    if (vCargado) {
                        Mensaje("Archivo cargado con éxito!", WarningType.Success);
                    }
                }else{
                    DivMensajeCarga.Visible = true;
                    LbAdvertenciaCarga.Text = "No se encontró ningún archivo a cargar.";
                }
            }catch (Exception Ex){
                DivMensajeCarga.Visible = true;
                LbAdvertenciaCarga.Text = Ex.Message;
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

        public void procesarArchivo(DataSet vArchivo, ref int vSuccess, string DireccionCarga, string TipoProceso){
            try{
                db vConexion = new db();
                if (vArchivo.Tables[0].Rows.Count > 0){
                    generarxml vMaestro = new generarxml();
                    DataTable vDatos = vArchivo.Tables[0];
                    string vQuery = "";
                    Boolean par1 = false, par2 = false, par3 = false, par4 = false, par5 = false, par6 = false,
                    par7 = false, par8 = false, par9 = false, par10 = false, par11 = false, par12 = false;

                    //STOCK
                    if (TipoProceso == "STOCK"){
                        foreach (DataColumn item in vDatos.Columns){
                            if (item.ColumnName.ToString() == "ID_TIPO_STOCK")
                                par1 = true;
                            if (item.ColumnName.ToString() == "ID_PROVEEDOR")
                                par2 = true;
                            if (item.ColumnName.ToString() == "MODELO")
                                par3 = true;                            
                            if (item.ColumnName.ToString() == "ID_MARCA")
                                par4 = true;                            
                            if (item.ColumnName.ToString() == "DESCRIPCION")
                                par5 = true;
                            if (item.ColumnName.ToString() == "SERIE")
                                par6 = true;
                            if (item.ColumnName.ToString() == "PRECIO_UNIT")
                                par7 = true;
                            if (item.ColumnName.ToString() == "ID_ESTADO")
                                par8 = true;
                            if (item.ColumnName.ToString() == "CABLEADO")
                                par9 = true;
                            if (item.ColumnName.ToString() == "ATM")
                                par10 = true;
                            if (item.ColumnName.ToString() == "AGENCIA")
                                par11 = true;
                        }

                        if (par1 && par2 && par3 && par4 && par5 && par6 && par7 && par8 && par9 && par10 && par11){
                            for (int i = 0; i < vDatos.Rows.Count; i++){
                                vQuery = "[STEISP_INVENTARIO_Stock] 3" +
                                        "," + vDatos.Rows[i]["ID_TIPO_STOCK"].ToString() +
                                        "," + vDatos.Rows[i]["ID_PROVEEDOR"].ToString() +
                                        ",'" + vDatos.Rows[i]["MODELO"].ToString() + "'" +
                                        "," + vDatos.Rows[i]["ID_MARCA"].ToString() +
                                        ",'" + vDatos.Rows[i]["DESCRIPCION"].ToString() + "'" +
                                        ",'" + vDatos.Rows[i]["SERIE"].ToString() + "'" +
                                        "," + vDatos.Rows[i]["PRECIO_UNIT"].ToString() +
                                        ",'" + Session["USUARIO"].ToString() + "'" +
                                        "," + vDatos.Rows[i]["ID_ESTADO"].ToString() +
                                        ",'" + vDatos.Rows[i]["ATM"].ToString() + "'" +
                                        ",'" + vDatos.Rows[i]["AGENCIA"].ToString()  + "'" +
                                        ",'" + vDatos.Rows[i]["CABLEADO"].ToString()  + "'";

                                int vRespuesta = vConexion.ejecutarSql(vQuery);
                                if (vRespuesta == 1)
                                    vSuccess++;
                            }
                        }
                    }

                    // EDC
                    if (TipoProceso == "EDC"){
                        foreach (DataColumn item in vDatos.Columns){
                            if (item.ColumnName.ToString() == "NOMBRE_NODO")
                                par1 = true;
                            if (item.ColumnName.ToString() == "ID_TIPO")
                                par2 = true;
                            if (item.ColumnName.ToString() == "SERIE")
                                par3 = true;
                            if (item.ColumnName.ToString() == "DIRECCION_IP")
                                par4 = true;
                            if (item.ColumnName.ToString() == "ID_REGION")
                                par5 = true;
                            if (item.ColumnName.ToString() == "IOS_IMAGE")
                                par6 = true;
                            if (item.ColumnName.ToString() == "IOS_VERSION")
                                par7 = true;
                            if (item.ColumnName.ToString() == "LATITUD")
                                par8 = true;
                            if (item.ColumnName.ToString() == "LONGITUD")
                                par9 = true;
                            if (item.ColumnName.ToString() == "FECHA_MANTENIMIENTO")
                                par10 = true;
                            if (item.ColumnName.ToString() == "ID_CONTRATO")
                                par11 = true;
                            if (item.ColumnName.ToString() == "ID_UBICACION")
                                par12 = true;
                        }

                        if (par1 && par2 && par3 && par4 && par5 && par6 && par7 && par8 && par9 && par10 && par11 && par12){
                            for (int i = 0; i < vDatos.Rows.Count; i++){
                                Object[] vDatosMaestro = new object[13];
                                vDatosMaestro[0] = vDatos.Rows[i]["ID_CONTRATO"].ToString();
                                vDatosMaestro[1] = vDatos.Rows[i]["NOMBRE_NODO"].ToString();
                                vDatosMaestro[2] = vDatos.Rows[i]["ID_TIPO"].ToString();
                                vDatosMaestro[3] = vDatos.Rows[i]["SERIE"].ToString();
                                vDatosMaestro[4] = vDatos.Rows[i]["DIRECCION_IP"].ToString();
                                vDatosMaestro[5] = vDatos.Rows[i]["ID_REGION"].ToString();
                                vDatosMaestro[6] = vDatos.Rows[i]["IOS_IMAGE"].ToString();
                                vDatosMaestro[7] = vDatos.Rows[i]["IOS_VERSION"].ToString();
                                vDatosMaestro[8] = 1;
                                vDatosMaestro[9] = vDatos.Rows[i]["LATITUD"].ToString();
                                vDatosMaestro[10] = vDatos.Rows[i]["LONGITUD"].ToString();
                                vDatosMaestro[11] = Convert.ToDateTime(vDatos.Rows[i]["FECHA_MANTENIMIENTO"]).ToString("MM-dd-yyyy");
                                vDatosMaestro[12] = Session["USUARIO"].ToString();
                                String vXML = vMaestro.ObtenerMaestroStringEDC(vDatosMaestro);
                                vXML = vXML.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");

                                Int32 vInfo;
                                vQuery = "[STEISP_INVENTARIO_StockEDC] 3,0," +
                                                "'" + vXML + "'";

                                vInfo = vConexion.ejecutarSql1(vQuery);
                                articulos vArt = new articulos();
                                if (vInfo > 0){
                                    if (vArt.insertarInventario(vInfo, vDatos.Rows[i]["UBICACION"].ToString(), "CREACION DE EDC", vDatos.Rows[i]["SERIE"].ToString()) == 2){
                                        vSuccess++;
                                    }
                                }
                            }
                        }
                    }

                    // Enlaces
                    if (TipoProceso == "ENLACE"){
                        foreach (DataColumn item in vDatos.Columns){
                            if (item.ColumnName.ToString() == "NOMBRE")
                                par1 = true;
                            if (item.ColumnName.ToString() == "DESCRIPCION")
                                par2 = true;
                            if (item.ColumnName.ToString() == "ID_TIPO")
                                par3 = true;
                            if (item.ColumnName.ToString() == "ID_PROVEEDOR")
                                par4 = true;
                            if (item.ColumnName.ToString() == "ID_ORIGEN")
                                par5 = true;
                            if (item.ColumnName.ToString() == "ID_DESTINO")
                                par6 = true;
                            if (item.ColumnName.ToString() == "IP_ORIGEN")
                                par7 = true;
                            if (item.ColumnName.ToString() == "IP_DESTINO")
                                par8 = true;
                            if (item.ColumnName.ToString() == "SERVICIOS")
                                par9 = true;
                            if (item.ColumnName.ToString() == "CONTACTO")
                                par10 = true;
                            if (item.ColumnName.ToString() == "TELEFONO")
                                par11 = true;
                        }

                        if (par1 && par2 && par3 && par4 && par5 && par6 && par7 && par8 && par9 && par10 && par11){
                            for (int i = 0; i < vDatos.Rows.Count; i++){
                                Object[] vDatosMaestro = new object[13];
                                vDatosMaestro[0] = vDatos.Rows[i]["ID_TIPO"].ToString();
                                vDatosMaestro[1] = vDatos.Rows[i]["ID_PROVEEDOR"].ToString();
                                vDatosMaestro[2] = vDatos.Rows[i]["NOMBRE"].ToString();
                                vDatosMaestro[3] = vDatos.Rows[i]["DESCRIPCION"].ToString();
                                vDatosMaestro[4] = vDatos.Rows[i]["ID_ORIGEN"].ToString();
                                vDatosMaestro[5] = vDatos.Rows[i]["ID_DESTINO"].ToString();
                                vDatosMaestro[6] = vDatos.Rows[i]["IP_ORIGEN"].ToString();
                                vDatosMaestro[7] = vDatos.Rows[i]["IP_DESTINO"].ToString();
                                vDatosMaestro[8] = vDatos.Rows[i]["SERVICIOS"].ToString();
                                vDatosMaestro[9] = vDatos.Rows[i]["CONTACTO"].ToString();
                                vDatosMaestro[10] = vDatos.Rows[i]["TELEFONO"].ToString();
                                vDatosMaestro[11] = Session["USUARIO"].ToString();
                                vDatosMaestro[12] = 1;
                                String vXML = vMaestro.ObtenerMaestroStringEDC(vDatosMaestro);
                                vXML = vXML.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");

                                Int32 vInfo;
                                vQuery = "[STEISP_INVENTARIO_Enlaces] {0},{1}," +
                                    "'" + vXML + "'";

                                vInfo = vConexion.ejecutarSql1(vQuery);
                                articulos vArt = new articulos();
                                if (vInfo > 0){
                                    if (vArt.insertarInventario(vInfo, "0", "CREACION DE ENLACE", "") == 2){
                                        vSuccess++;
                                    }
                                }
                            }
                        }
                    }

                    // Proveedor
                    if (TipoProceso == "PROVEEDOR"){
                        foreach (DataColumn item in vDatos.Columns){
                            if (item.ColumnName.ToString() == "NOMBRE")
                                par1 = true;
                            if (item.ColumnName.ToString() == "DIRECCION")
                                par2 = true;
                            if (item.ColumnName.ToString() == "TELEFONO")
                                par3 = true;
                            if (item.ColumnName.ToString() == "RESPONSABLE")
                                par4 = true;
                        }

                        if (par1 && par2 && par3 && par4){
                            for (int i = 0; i < vDatos.Rows.Count; i++){
                                Int32 vInfo;
                                vQuery = "STEISP_INVENTARIO_Proveedores 3" +
                                ",'" + vDatos.Rows[i]["NOMBRE"].ToString().ToUpper() + "'" +
                                ",'" + vDatos.Rows[i]["DIRECCION"].ToString() + "'" +
                                ",'" + vDatos.Rows[i]["TELEFONO"].ToString() + "'" +
                                ",'" + vDatos.Rows[i]["RESPONSABLE"].ToString() + "'";
                                vInfo = vConexion.ejecutarSql(vQuery);
                                if (vInfo == 1){
                                    vSuccess++;
                                }
                            }
                        }
                    }

                    // Contrato
                    if (TipoProceso == "CONTRATO"){
                        foreach (DataColumn item in vDatos.Columns){
                            if (item.ColumnName.ToString() == "NOMBRE_CONTRATO")
                                par1 = true;
                            if (item.ColumnName.ToString() == "ID_TIPO")
                                par2 = true;
                            if (item.ColumnName.ToString() == "ID_PROVEEDOR")
                                par3 = true;
                            if (item.ColumnName.ToString() == "FECHA_INICIO")
                                par4 = true;
                            if (item.ColumnName.ToString() == "FECHA_FINAL")
                                par5 = true;
                            if (item.ColumnName.ToString() == "CONDICIONES")
                                par6 = true;
                        }

                        if (par1 && par2 && par3 && par4 && par5 && par6){
                            for (int i = 0; i < vDatos.Rows.Count; i++){
                                int vInfo;
                                vQuery = "[STEISP_INVENTARIO_Contratos] 3" +
                                        "," + vDatos.Rows[i]["ID_PROVEEDOR"].ToString() +
                                        ",'" + vDatos.Rows[i]["NOMBRE_CONTRATO"].ToString().ToUpper() + "'" +
                                        ",'" + Convert.ToDateTime(vDatos.Rows[i]["FECHA_INICIO"]).ToString("yyyy-MM-dd") + "'" +
                                        ",'" + Convert.ToDateTime(vDatos.Rows[i]["FECHA_FINAL"]).ToString("yyyy-MM-dd") + "'" +
                                        ",'" + vDatos.Rows[i]["CONDICIONES"].ToString() + "'" +
                                        "," + vDatos.Rows[i]["ID_TIPO"].ToString() +
                                        ",'" + Session["USUARIO"].ToString() + "'" +
                                        ",1";
                                vInfo = vConexion.ejecutarSql(vQuery);
                                if (vInfo == 1){
                                    vSuccess++;
                                }
                            }
                        }
                    }

                }else
                    throw new Exception("No contiene ninguna hoja de excel.");
            }catch (Exception){
                throw;
            }
        }

    }
}