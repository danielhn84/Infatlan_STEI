using System;
using System.Data;

namespace Infatlan_STEI.classes
{
    public class Security
    {
        db vConexion = new db();
        public permisos ObtenerPermiso(String vUsuario, int idAplicacion)
        {
            permisos vPermiso = new permisos();
            try
            {
                String vQuery = "[STEISP_Permisos] 4" +
                    ",'" + vUsuario + "'" +
                    "," + idAplicacion;
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                foreach (DataRow item in vDatos.Rows)
                {
                    if (Convert.ToBoolean(item["consulta"]))
                        vPermiso.Consulta = true;
                    if (Convert.ToBoolean(item["escritura"]))
                        vPermiso.Creacion = true;
                    if (Convert.ToBoolean(item["edicion"]))
                        vPermiso.Edicion = true;
                    if (Convert.ToBoolean(item["borrar"]))
                        vPermiso.Borrado = true;
                }
            }
            catch
            {
                vPermiso = new permisos();
            }
            return vPermiso;
        }
    }

    public class permisos
    {
        public bool Consulta { get; set; } = false;
        public bool Creacion { get; set; } = false;
        public bool Edicion { get; set; } = false;
        public bool Borrado { get; set; } = false;
    }
}