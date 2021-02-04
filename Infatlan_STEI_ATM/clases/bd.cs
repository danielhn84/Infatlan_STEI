using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


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
        SqlConnection vConexionATM;
        public bd()
        {
            vConexion = new SqlConnection(ConfigurationManager.AppSettings["SQLServer"]);
            vConexionATM = new SqlConnection(ConfigurationManager.AppSettings["SQLServerATM"]);
        }
        public void Vista()
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
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
        public DataTable ObtenerTablaATM(string vQuery)
        {
            DataTable vDatos = new DataTable();
            try
            {
                SqlDataAdapter vDataAdapter = new SqlDataAdapter(vQuery, vConexionATM);
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
        public int ejecutarSQLATM(string vQuery)
        {
            int vResultado = 0;
            try
            {
                SqlCommand vSqlCommand = new SqlCommand(vQuery, vConexionATM);
                vSqlCommand.CommandType = CommandType.Text;

                vConexionATM.Open();
                vResultado = vSqlCommand.ExecuteNonQuery();
                vConexionATM.Close();
            }
            catch (Exception Ex)
            {
                string vError = Ex.Message;
                vConexionATM.Close();
                throw;
            }
            return vResultado;
        }
    }
}