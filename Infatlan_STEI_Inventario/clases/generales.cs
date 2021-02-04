﻿using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace Infatlan_STEI_Inventario.clases
{
    public class generales
    {
        public generales() { }

        public string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            byte[] result = md5.Hash;
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

        public bool PermisosRecursosHumanos(DataTable vDatosLogin)
        {
            Boolean vFlag = false;
            try
            {
                DataTable vDatos = vDatosLogin;

                if (!vDatos.Rows[0]["tipoEmpleado"].ToString().Equals(""))
                {
                    if (vDatos.Rows[0]["tipoEmpleado"].ToString().Equals("1"))
                    {
                        vFlag = true;
                    }
                }
                else
                {
                    vFlag = false;
                }
            }
            catch { vFlag = false; }
            return vFlag;
        }

        public bool PermisosPersonalSeguridad(DataTable vDatosLogin)
        {
            Boolean vFlag = false;
            try
            {
                DataTable vDatos = vDatosLogin;

                if (!vDatos.Rows[0]["tipoEmpleado"].ToString().Equals(""))
                {
                    if (vDatos.Rows[0]["tipoEmpleado"].ToString().Equals("2") || vDatos.Rows[0]["tipoEmpleado"].ToString().Equals("1") || vDatos.Rows[0]["tipoEmpleado"].ToString().Equals("3"))
                    {
                        vFlag = true;
                    }
                }
                else
                {
                    vFlag = false;
                }
            }
            catch { vFlag = false; }
            return vFlag;
        }

        /// <summary>
        /// Calculates number of business days, taking into account:
        ///  - weekends (Saturdays and Sundays)
        ///  - bank holidays in the middle of the week
        /// </summary>
        /// <param name="firstDay">First day in the time interval</param>
        /// <param name="lastDay">Last day in the time interval</param>
        /// <param name="bankHolidays">List of bank holidays excluding weekends</param>
        /// <returns>Number of business days during the 'span'</returns>
        public int BusinessDaysUntil(DateTime firstDay, DateTime lastDay, params DateTime[] bankHolidays)
        {
            firstDay = firstDay.Date;
            lastDay = lastDay.Date;
            if (firstDay > lastDay)
                throw new ArgumentException("Incorrect last day " + lastDay);

            TimeSpan span = lastDay - firstDay;
            int businessDays = span.Days + 1;
            int fullWeekCount = businessDays / 7;

            if (businessDays > fullWeekCount * 7)
            {
                int firstDayOfWeek = (int)firstDay.DayOfWeek;
                int lastDayOfWeek = (int)lastDay.DayOfWeek;
                if (lastDayOfWeek < firstDayOfWeek)
                    lastDayOfWeek += 7;
                if (firstDayOfWeek <= 6)
                {
                    if (lastDayOfWeek >= 7)
                        businessDays -= 2;
                    else if (lastDayOfWeek >= 6)
                        businessDays -= 1;
                }
                else if (firstDayOfWeek <= 7 && lastDayOfWeek >= 7)
                    businessDays -= 1;
            }

            businessDays -= fullWeekCount + fullWeekCount;
            foreach (DateTime bankHoliday in bankHolidays)
            {
                DateTime bh = bankHoliday.Date;
                if (firstDay <= bh && bh <= lastDay)
                    --businessDays;
            }

            return businessDays;
        }
    }
}