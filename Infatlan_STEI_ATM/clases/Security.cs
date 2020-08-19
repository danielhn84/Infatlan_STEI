﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Infatlan_STEI_ATM.clases
{
    public class Security{
        bd vConexion = new bd();
        public permisos ObtenerPermiso(String vUsuario, int idAplicacion){
            permisos vPermiso = new permisos();
            try{
                String vQuery = "[STEISP_Permisos] 4" +
                    ",'" + vUsuario + "'" +
                    "," + idAplicacion;
                DataTable vDatos = vConexion.ObtenerTabla(vQuery);

                foreach (DataRow item in vDatos.Rows){
                    if (Convert.ToBoolean(item["consulta"].ToString()))
                        vPermiso.Consulta = true;
                    if (Convert.ToBoolean(item["escritura"].ToString()))
                        vPermiso.Creacion = true;
                    if (Convert.ToBoolean(item["edicion"].ToString()))
                        vPermiso.Edicion = true;
                    if (Convert.ToBoolean(item["borrar"].ToString()))
                        vPermiso.Borrado = true;
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