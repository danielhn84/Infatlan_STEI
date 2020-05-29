using System;

namespace Infatlan_STEI
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string usu = Convert.ToString(Session["USUARIO"]);
            classes.rolAplicacion[] vRolAplicacion = new classes.rolAplicacion[2];
            vRolAplicacion[0] = new classes.rolAplicacion()
            {
                NombreAplicacion = "Agencias",
                Aplicacion = 1,
                escritura = 1,
                edicion = 0,
                consulta = 1,
                borrar = 0
            };
            vRolAplicacion[1] = new classes.rolAplicacion()
            {
                NombreAplicacion = "ATMs",
                Aplicacion = 2,
                escritura = 1,
                edicion = 0,
                consulta = 1,
                borrar = 0
            };

            classes.roles vRol = new classes.roles()
            {
                Nombre = "Daniel Henriquez",
                Usuario = "dehenriquez",
                Correo = "dehenriquez@hotmail.com",
                Telefono = "94767348",
                Identidad = "0801198406577",
                Departamento = "8",
                Aplicaciones = vRolAplicacion
            };

            Session["ROL"] = vRol;
            getRol();
        }


        public void getRol()
        {
            classes.roles vRol = (classes.roles)Session["ROL"];
            classes.getRoles vRolesAplicacion = new classes.getRoles();
            classes.generales vGenerales = new classes.generales();
            Boolean vTieneAcceso = vGenerales.getAccess(1, (classes.roles)Session["ROL"], ref vRolesAplicacion);

            if (!vTieneAcceso)
                Response.Redirect("../default.aspx");


            if (!vRolesAplicacion.Escritura.Equals(1))
            {
                throw new Exception("");
            }

        }

    }
}