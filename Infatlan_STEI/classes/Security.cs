using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Infatlan_STEI.classes
{
    public class Security{
        db vConexion = new db();
        public permisos ObtenerPermiso(String vUsuario, int idAplicacion){
            permisos vPermiso = new permisos();
            try{
                String vQuery = "[STEISP_Permisos] 4" +
                    ",'" + vUsuario + "'" +
                    "," + idAplicacion;
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                foreach (DataRow item in vDatos.Rows){
                    if (item["consulta"].ToString().Equals("1"))
                        vPermiso.Consulta = true;
                    if (item["creacion"].ToString().Equals("1"))
                        vPermiso.Consulta = true;
                    if (item["edicion"].ToString().Equals("1"))
                        vPermiso.Consulta = true;
                    if (item["borrado"].ToString().Equals("1"))
                        vPermiso.Consulta = true;
                }
            }catch{
                vPermiso = new permisos();
            }
            return vPermiso;
        }
    }

    public class permisos{
        public bool Consulta { get; set; } = false;
        public bool Creacion { get; set; } = false;
        public bool Edicion { get; set; } = false;
        public bool Borrado { get; set; } = false;
    }
}