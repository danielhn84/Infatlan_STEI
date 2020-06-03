using System;
using System.Security.Cryptography;
using System.Text;

namespace Infatlan_STEI.classes
{
    public class generales
    {
        public generales() { }
        public Boolean getAccess(int vAplicacion, roles vRoles, ref getRoles vRolesAplicacion)
        {
            Boolean vAcceso = false;
            try
            {
                foreach (rolAplicacion item in vRoles.Aplicaciones)
                {
                    if (item.Aplicacion.Equals(vAplicacion))
                    {
                        vAcceso = true;
                        vRolesAplicacion = new getRoles()
                        {
                            Escritura = item.escritura,
                            Consulta = item.consulta,
                            Borrar = item.borrar,
                            Edicion = item.edicion
                        };
                    }
                }
            }
            catch { }
            return vAcceso;
        }

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
    }


}