using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace Infatlan_STEI_ATM.clases
{
    public enum WarningType
    {
        Success,
        Info,
        Warning,
        Danger
    }
   
    public class bd
    {
        SqlConnection vConexion;
        public bd()
        {
            vConexion = new SqlConnection(ConfigurationManager.AppSettings["SQLServer"]);
        }
        public DataTable ObtenerTabla(string vQuery)
        {
            DataTable vDatos = new DataTable();
            try
            {
                SqlDataAdapter vDataAdapter = new SqlDataAdapter(vQuery, vConexion);
                vDataAdapter.Fill(vDatos);
            }
            catch (Exception)
            {

                throw;
            }
            return vDatos;
        }
        public int ejecutarSQL(string vQuery)
        {
            int vResultado = 0;
            try
            {
                SqlCommand vSqlCommand = new SqlCommand(vQuery, vConexion);
                vSqlCommand.CommandType = CommandType.Text;

                vConexion.Open();
                vResultado = vSqlCommand.ExecuteNonQuery();
                vConexion.Close();
            }
            catch (Exception Ex)
            {
                string vError = Ex.Message;
                vConexion.Close();
                throw;
            }
            return vResultado;
        }
    }
}