using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Configuration;
using System.Data;
using Infatlan_STEI_Inventario.clases;

namespace Infatlan_STEI_Inventario.clases
{
    public class SapConnector{

        db vConexion = new db();
        public SapConnector(){

        }

        public String getInformacion(String vFechaInicio, String vFechaFin){
            String vResultado = String.Empty;
            try{
                SapService.ZWS_AF_CONSULTA vConsulta = new SapService.ZWS_AF_CONSULTA();
                vConsulta.FECHA_INICIO = vFechaInicio;
                vConsulta.FECHA_FINAL = vFechaFin;

                SapService.ZWS_AF_INF vRequest = new SapService.ZWS_AF_INF();
                SapService.ZWS_AF_CONSULTAResponse vResponse = vRequest.ZWS_AF_CONSULTA(vConsulta);
                SapService.ZWS_AF_INFT[] vResActivo = vResponse.ZAF_DATA_TAB;
                SapService.ZWS_AF_ACRE[] vResProv = vResponse.ZAF_DATA_PROV;
                SapService.ZWS_AF_TAC[] vResTipo = vResponse.ZAF_DATA_TIPOA;

                int vContador = 0, vCounter = 0;
                foreach (var item in vResActivo){
                    SapService.ZWS_AF_INFT vResul2 = vResActivo[vContador];
                    vContador++;

                    String vFecha = Convert.ToDateTime(item.FECHA).ToString("yyyy-MM-dd");
                    String vQuery = "[STEISP_INVENTARIO_ObtenerSTOCK] 1" +
                        ",'" + item.IDTIPOARTICULO + "'" +
                        ",'" + item.IDACREEDOR + "'" +
                        ",'" + item.DESCRIPCION + "'" +
                        "," + item.PRECIO.ToString().Replace(",",".") +
                        ",'" + vFecha + "'" +
                        ",'" + item.SERIE + "'" +
                        ",'" + item.INVENTARIO + "'";
                    if (vConexion.ejecutarSql(vQuery) == 1)
                        vCounter++;
                }
                vResultado = "Se han ingresado " + vCounter + " registros. ";
                vContador = 0; 
                vCounter = 0;
                foreach (var item in vResProv){
                    SapService.ZWS_AF_ACRE vResul2 = vResProv[vContador];
                    vContador++;

                    String vQuery = "[STEISP_INVENTARIO_ObtenerSTOCK] 2" +
                        ",'" + item.IDACREEDOR + "'" +
                        ",'" + item.ACREEDOR + "'" +
                        ",'" + item.DIRECCION + "'" +
                        ",'" + item.TELEFONO + "'" +
                        ",'" + item.RESPONSABLE + "'";
                    if (vConexion.ejecutarSql(vQuery) == 1)
                        vCounter++;
                }
                vResultado += vCounter + " Proveedores. ";
                vContador = 0; 
                vCounter = 0;
                foreach (var item in vResTipo){
                    SapService.ZWS_AF_TAC vResul2 = vResTipo[vContador];
                    vContador++;

                    String vQuery = "[STEISP_INVENTARIO_ObtenerSTOCK] 3" +
                        ",'" + item.IDTIPOARTICULO + "'" +
                        ",'" + item.TIPOARTICULO + "'";
                    if (vConexion.ejecutarSql(vQuery) == 1)
                        vCounter++;
                }
                vResultado += vCounter + " Tipos de Artículo.";

            }catch (Exception Ex){
                String vError = Ex.Message;
                throw;
            }
            return vResultado;
        }
    }

}