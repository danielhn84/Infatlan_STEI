using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Infatlan_STEI.classes
{
    public enum WarningType
    {
        Success,
        Info,
        Warning,
        Danger
    }
    public class db
    {
        SqlConnection vConexion = new SqlConnection(ConfigurationManager.AppSettings["SQLServer"]);
        SqlConnection vConexionSysAid = new SqlConnection(ConfigurationManager.AppSettings["SQLServerSysAid"]);
        public db(){
            vConexion = new SqlConnection(ConfigurationManager.AppSettings["SQLServer"]);
        }

        public DataTable obtenerDataTable(String vQuery){
            DataTable vDatos = new DataTable();
            try{
                SqlDataAdapter vDataAdapter = new SqlDataAdapter(vQuery, vConexion);
                vDataAdapter.Fill(vDatos);
            }catch{
                throw;
            }
            return vDatos;
        }

        public int ejecutarSql(String vQuery){
            int vResultado = 0;
            try{
                SqlCommand vSqlCommand = new SqlCommand(vQuery, vConexion);
                vSqlCommand.CommandType = CommandType.Text;

                vConexion.Open();
                vResultado = vSqlCommand.ExecuteNonQuery();
                vConexion.Close();

            }catch (Exception Ex){
                String vError = Ex.Message;
                vConexion.Close();
                throw;
            }
            return vResultado;
        }

        public int obtenerId(String vQuery){
            DataTable vDatos = new DataTable();
            int vId;
            try{
                SqlDataAdapter vDataAdapter = new SqlDataAdapter(vQuery, vConexion);
                vDataAdapter.Fill(vDatos);
                vId = Convert.ToInt32(vDatos.Rows[0][0].ToString());
            }catch{
                throw;
            }
            return vId;
        }

        public DataSet obtenerDataSet(String vQuery){
            DataSet vDatos = new DataSet();
            try{
                SqlDataAdapter vDataAdapter = new SqlDataAdapter(vQuery, vConexion);
                vDataAdapter.Fill(vDatos);
            }catch{
                throw;
            }
            return vDatos;
        }

        public DataTable obtenerDataTableSA(String vQuery){
            DataTable vDatos = new DataTable();
            try{
                SqlDataAdapter vDataAdapter = new SqlDataAdapter(vQuery, vConexionSysAid);
                vDataAdapter.Fill(vDatos);
            }catch{
                throw;
            }
            return vDatos;
        }

        public DataSet obtenerDataSetSA(String vQuery){
            DataSet vDatos = new DataSet();
            try{
                SqlDataAdapter vDataAdapter = new SqlDataAdapter(vQuery, vConexionSysAid);
                vDataAdapter.Fill(vDatos);
            }catch{
                throw;
            }
            return vDatos;
        }




    }
}