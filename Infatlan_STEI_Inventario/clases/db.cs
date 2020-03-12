﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Infatlan_STEI_Inventario.clases
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
        SqlConnection vConexion;
        public db()
        {
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
    }
}