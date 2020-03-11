using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infatlan_STEI.classes
{

    public class roles
    {
        public String Nombre { get; set; }
        public String Usuario { get; set; }
        public String Correo { get; set; }
        public String Telefono { get; set; }
        public String Identidad { get; set; }
        public String Departamento { get; set; }
        public rolAplicacion[] Aplicaciones { get; set; }
    }
    public class rolAplicacion
    {
        public String NombreAplicacion { get; set; }
        public int Aplicacion { get; set; }
        public int escritura { get; set; }
        public int borrar { get; set; }
        public int edicion { get; set; }
        public int consulta { get; set; }
    }

    public class getRoles
    {
        public int Escritura { get; set; }
        public int Edicion { get; set; }
        public int Borrar { get; set; }
        public int Consulta { get; set; }
    }
}