using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Infatlan_STEI_ATM.clases
{
    public enum typeBody{
        Supervisor,       
        Solicitante,
        Aprobado,
        Rechazado,
        JefeAgencia,
        Reprogramacion,
        Tecnicos,
        Encargado,
        JefeAgenciaAprobar,
        ReprogramacionJefes,
        ReprogramacionSolicitante,
        ReprogramacionLista,
        EnviarVerificacionSolicitante,
        EnviarVerificacionJefe,
        AprobarVerifSolicitante,
        AprobarVerifJefe,
        DevolverSolicitante,
        DevolverJefe
    }

    public class SmtpService : Page{

        public SmtpService() { }

        public Boolean EnviarMensaje(String To, typeBody Body, String Usuario, String Nombre, String vMessage = null)
        {
            Boolean vRespuesta = false;
            try
            {
                MailMessage mail = new MailMessage("Recursos Humanos<" + ConfigurationManager.AppSettings["SmtpFrom"] + ">", To);
                SmtpClient client = new SmtpClient();
                client.Port = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"]);
                //client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Host = ConfigurationManager.AppSettings["SmtpServer"];
                mail.Subject = "STEI - Sistema de Telecomunicaciones e Inventario";
                mail.IsBodyHtml = true;

                switch (Body)
                {
                    case typeBody.Supervisor:
                        mail.AlternateViews.Add(CreateHtmlMessage(PopulateBody(
                            Usuario,
                            "El empleado " + Nombre + " ha creado una notificación de mantenimiento.",
                            ConfigurationManager.AppSettings["Host"] + "/pages/mantenimiento/buscarAprobarNotificacion.aspx",
                            "Te informamos que solicitud debe ser aprobado."
                            ), Server.MapPath("/images/logo.png")));
                        break;
                    case typeBody.Solicitante:
                        mail.AlternateViews.Add(CreateHtmlMessage(PopulateBody(
                            Usuario,
                            "Has creado una notificación de mantenimiento.",
                            ConfigurationManager.AppSettings["Host"] + "/pages/mantenimiento/buscarVerificacion.aspx",
                            "Te informamos que solicitud debe ser aprobado por su jefe inmediato."
                            ), Server.MapPath("/images/logo.png")));
                        break;
                    case typeBody.Encargado:
                        mail.AlternateViews.Add(CreateHtmlMessage(PopulateBody(
                            Usuario,
                            "El empleado " + Nombre + " ha creado una notificación de mantenimiento y lo ha asignado como encargado de mantenimiento.",
                            ConfigurationManager.AppSettings["Host"] + "/pages/mantenimiento/buscarVerificacion.aspx",
                            "Te informamos que debe llenar lista de verificación al realizar mantenimiento."
                            ), Server.MapPath("/images/logo.png")));
                        break;
                    case typeBody.Tecnicos:
                        mail.AlternateViews.Add(CreateHtmlMessage(PopulateBody(
                            Usuario,
                            "El empleado " + Nombre + " ha creado una notificación de mantenimiento.",
                            ConfigurationManager.AppSettings["Host"] + "/pages/mantenimiento/buscarVerificacion.aspx",
                            "Te informamos que formaras parte del equipo de mantenimiento."
                            ), Server.MapPath("/images/logo.png")));
                        break;
                    case typeBody.JefeAgencia:
                        mail.AlternateViews.Add(CreateHtmlMessage(PopulateBody(
                            Usuario,
                            "El empleado " + Nombre + " ha creado una notificación de mantenimiento.",
                            ConfigurationManager.AppSettings["Host"] + "/login.aspx",
                            "Se ha creado una notificación de mantenimiento."
                            ), Server.MapPath("/images/logo.png")));
                        break;
                    case typeBody.Reprogramacion:
                        mail.AlternateViews.Add(CreateHtmlMessage(PopulateBody(
                            Usuario,
                            "El empleado " + Nombre + " ha cancelado una notificación de mantenimiento.",
                            ConfigurationManager.AppSettings["Host"] + "/login.aspx",
                            "Se reprogramará una notificación de mantenimiento."
                            ), Server.MapPath("/images/logo.png")));
                        break;
                    case typeBody.ReprogramacionJefes:
                        mail.AlternateViews.Add(CreateHtmlMessage(PopulateBody(
                            Usuario,
                            "El empleado " + Nombre + " ha calcelado una notificación de mantenimiento, ingrese al link para reprogramarlo.",
                            ConfigurationManager.AppSettings["Host"] + "/pages/reprogramar/buscarReprogramar.aspx",
                            "Te informamos que debe reprogramar mantenimiento."
                            ), Server.MapPath("/images/logo.png")));
                        break;
                    case typeBody.ReprogramacionSolicitante:
                        mail.AlternateViews.Add(CreateHtmlMessage(PopulateBody(
                            Usuario,
                            "El empleado " + Nombre + " ha reprogramado mantenimiento.",
                            ConfigurationManager.AppSettings["Host"] + "/pages/mantenimiento/notificacion.aspx",
                            "Te informamos que has reprogramado mantenimiento."
                            ), Server.MapPath("/images/logo.png")));
                        break;
                    case typeBody.EnviarVerificacionSolicitante:
                        mail.AlternateViews.Add(CreateHtmlMessage(PopulateBody(
                            Usuario,
                            "Has creado una lista de verificación.",
                            ConfigurationManager.AppSettings["Host"] + "/pages/mantenimiento/buscarVerificacion.aspx",
                            "Te informamos que su solicitud debe ser aprobado por jefe."
                            ), Server.MapPath("/images/logo.png")));
                        break;
                    case typeBody.EnviarVerificacionJefe:
                        mail.AlternateViews.Add(CreateHtmlMessage(PopulateBody(
                            Usuario,
                            "El empleado " + Nombre + " ha crado una lista de verificación que necesita su aprobación.",
                            ConfigurationManager.AppSettings["Host"] + "/pages/mantenimiento/buscarAprobarVerificacion.aspx",
                            "Te informamos que debes aprobar lista de verificación solicitada."
                            ), Server.MapPath("/images/logo.png")));
                        break;
                    case typeBody.AprobarVerifSolicitante:
                        mail.AlternateViews.Add(CreateHtmlMessage(PopulateBody(
                            Usuario,
                            "El empleado " + Nombre + " ha aprobado lista de verificación.",
                            ConfigurationManager.AppSettings["Host"] + "/pages/mantenimiento/notificacion.aspx",
                            "Te informamos que su solicitud debe ser aprobado por jefe."
                            ), Server.MapPath("/images/logo.png")));
                        break;
                    case typeBody.AprobarVerifJefe:
                        mail.AlternateViews.Add(CreateHtmlMessage(PopulateBody(
                            Usuario,
                            "Se aprobó lista de verificación a empleado "+Nombre+".",
                            ConfigurationManager.AppSettings["Host"] + "/pages/mantenimiento/buscarAprobarVerificacion.aspx",
                            "Te informamos que has aprobado lista de verificación solicitada."
                            ), Server.MapPath("/images/logo.png")));
                        break;
                    case typeBody.DevolverJefe:
                        mail.AlternateViews.Add(CreateHtmlMessage(PopulateBody(
                            Usuario,
                            "Has devuelto lista de verificación a empleado " + Nombre + ".",
                            ConfigurationManager.AppSettings["Host"] + "/pages/mantenimiento/notificacion.aspx",
                            "Te informamos que solicitud se devolvió"
                            ), Server.MapPath("/images/logo.png")));
                        break;
                    case typeBody.DevolverSolicitante:
                        mail.AlternateViews.Add(CreateHtmlMessage(PopulateBody(
                            Usuario,
                            "El empleado " + Nombre + " ha devuelto lista de verificación.",
                            ConfigurationManager.AppSettings["Host"] + "/pages/mantenimiento/notificacion.aspx",
                            "Te informamos que su solicitud fué devuelto por jefe."
                            ), Server.MapPath("/images/logo.png")));
                        break;
                }
                client.Send(mail);
                vRespuesta = true;
            }
            catch (System.Net.Mail.SmtpException Ex)
            {
                String vError = Ex.Message;
                throw;
            }
            catch (Exception Ex)
            {
                throw;
            }
            return vRespuesta;
        }

        private AlternateView CreateHtmlMessage(string message, string logoPath){
            var inline = new LinkedResource(logoPath, "image/png");
            inline.ContentId = "companyLogo";

            var alternateView = AlternateView.CreateAlternateViewFromString(
                                    message,
                                    Encoding.UTF8,
                                    MediaTypeNames.Text.Html);
            alternateView.LinkedResources.Add(inline);

            return alternateView;
        }

        public string PopulateBody(string vNombre, string vTitulo, string vUrl, string vDescripcion)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("/pages/mail/TemplateMail.html")))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{Host}", ConfigurationManager.AppSettings["Host"]);
            body = body.Replace("{Nombre}", vNombre);
            body = body.Replace("{Titulo}", vTitulo);
            body = body.Replace("{Url}", vUrl);
            body = body.Replace("{Descripcion}", vDescripcion);
            return body;
        }
    }
}