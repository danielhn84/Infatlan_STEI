﻿using System;
using System.Data;
using Infatlan_STEI.classes;
using System.Web.UI;
using System.Configuration;


namespace Infatlan_STEI
{
    public partial class _default : System.Web.UI.Page
    {
        db vConexion = new db();
        protected void Page_Load(object sender, EventArgs e){
            try{
                string usu = Convert.ToString(Session["USUARIO"]);
                bool au = Convert.ToBoolean(Session["AUTH"]);

                classes.rolAplicacion[] vRolAplicacion = new classes.rolAplicacion[2];
                vRolAplicacion[0] = new classes.rolAplicacion(){
                    NombreAplicacion = "Agencias",
                    Aplicacion = 1,
                    escritura = 1,
                    edicion = 0,
                    consulta = 1,
                    borrar = 0
                };
                vRolAplicacion[1] = new classes.rolAplicacion(){
                    NombreAplicacion = "ATMs",
                    Aplicacion = 2,
                    escritura = 1,
                    edicion = 0,
                    consulta = 1,
                    borrar = 0
                };

                classes.roles vRol = new classes.roles(){
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

                cargarCableado();
                cargarInventario();
            }catch (Exception ex){

            }
        }
        
        public void Mensaje(string vMensaje, WarningType type){
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
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

        protected void BtnEnviarBug_Click(object sender, EventArgs e){
            try{
                DataTable vDatos = (DataTable)Session["AUTHCLASS"];
                if (vDatos.Rows.Count > 0){
                    SmtpService vService = new SmtpService();
                    Boolean vFlagEnvio = false;
                    String vDestino = "";

                    if (DDLTipo.SelectedValue == "1")
                        vDestino = ConfigurationManager.AppSettings["SmtpSTEI"].ToString();
                    else if(DDLTipo.SelectedValue == "2")
                        vDestino = ConfigurationManager.AppSettings["SmtpAGENCIAS"].ToString();
                    else if (DDLTipo.SelectedValue == "3")
                        vDestino = ConfigurationManager.AppSettings["SmtpATM"].ToString();
                    else if (DDLTipo.SelectedValue == "4")
                        vDestino = ConfigurationManager.AppSettings["SmtpCABLEADO"].ToString();
                    else if (DDLTipo.SelectedValue == "5")
                        vDestino = ConfigurationManager.AppSettings["SmtpINVENTARIO"].ToString();

                    foreach (DataRow item in vDatos.Rows){
                        if (!item["emailEmpresa"].ToString().Trim().Equals("")){
                            vService.EnviarMensaje(
                                vDestino,
                                typeBody.Bugs,
                                item["nombre"].ToString(),
                                vDatos.Rows[0]["nombre"].ToString()
                                );
                            vFlagEnvio = true;
                        }
                    }

                    if (vFlagEnvio)
                        Mensaje("Insidencia enviada con éxito.", WarningType.Success);
                    else
                        Mensaje("El Mensaje no se pudo enviar. Favor intente de nuevo.", WarningType.Success);
                }
            }catch (Exception ex){

            }
        }

        private void cargarCableado() {
            String vQueryUsuario = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 28 ,'" + Session["USUARIO"] + "'";
            DataTable vDatosUsuario = vConexion.obtenerDataTable(vQueryUsuario);
                    
            int vUsuario = 1;
            String vQuery = "";
            DataTable vDatos = new DataTable();
            if (vUsuario == 1){
                vQuery = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 28 ,'" + Session["USUARIO"].ToString() + "'";
                vDatos = vConexion.obtenerDataTable(vQuery);

                txtCreadas.Text = "Estudio Creados";
                txtPendientes.Text = "Estudios Pendientes de Edición";
                lbCreadas.Text = vDatos.Rows[0]["creados"].ToString();
                lbPendientes.Text = vDatos.Rows[0]["edicion"].ToString();
                LbFechaDashboard.Text = DateTime.Now.ToString("dd-MM-yyyy");
            }

            if (vUsuario == 2){
                vQuery = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 29 ,'" + Session["USUARIO"].ToString() + "'";
                vDatos = vConexion.obtenerDataTable(vQuery);

                txtCreadas.Text = "Estudio Revisados";
                txtPendientes.Text = "Revisiones Pendientes";
                lbCreadas.Text = vDatos.Rows[0]["revisados"].ToString(); 
                lbPendientes.Text = vDatos.Rows[0]["revisionpendiente"].ToString();
                LbFechaDashboard.Text = DateTime.Now.ToString("dd-MM-yyyy");
            }

            if (vUsuario == 3){
                vQuery = "STEISP_CABLESTRUCTURADO_ConsultaDatosEstudio 27 ,'" + Session["USUARIO"].ToString() + "'";
                vDatos = vConexion.obtenerDataTable(vQuery);

                txtCreadas.Text = "Cotizaciones Realizadas";
                txtPendientes.Text = "Cotizaciones Pendientes";
                lbCreadas.Text = vDatos.Rows[0]["realizados"].ToString(); ;
                lbPendientes.Text = vDatos.Rows[0]["pendientes"].ToString(); 
                LbFechaDashboard.Text = DateTime.Now.ToString("dd-MM-yyyy");
            }
        }

        private void cargarInventario() {
            String vQuery = "[STEISP_INVENTARIO_Generales] 15";
            DataTable vDatos = vConexion.obtenerDataTable(vQuery);
            LbStock.Text = vDatos.Rows[0]["Stock"].ToString();
            LbEDC.Text = vDatos.Rows[0]["EDC"].ToString();
            LbEnlace.Text = vDatos.Rows[0]["Enl"].ToString();
            LbTran.Text = vDatos.Rows[0]["Trans"].ToString();
        }
    }
}