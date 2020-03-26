using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    }

    
}