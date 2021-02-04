using System;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web.UI;

namespace Infatlan_STEI_ATM.clases
{
    public enum typeBody
    {
        ATM,
        Alertas
    }

    public class SmtpService : Page
    {

        public SmtpService() { }

        public Boolean EnviarMensaje(String To, typeBody Body, String Titulo, String Nombre, String Descripcion, String vCopia = null, String vLink = null)
        {
            Boolean vRespuesta = false;
            try
            {
                MailMessage mail = new MailMessage("STEI<" + ConfigurationManager.AppSettings["SmtpFrom"] + ">", To);
                SmtpClient client = new SmtpClient();
                client.Port = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"]);

                if (!String.IsNullOrEmpty(vCopia))
                {
                    mail.CC.Add(vCopia);
                }

                client.UseDefaultCredentials = false;
                client.Host = ConfigurationManager.AppSettings["SmtpServer"];
                mail.Subject = "STEI - Sistema de Telecomunicaciones e Inventario";
                mail.IsBodyHtml = true;

                switch (Body)
                {

                    case typeBody.ATM:
                        mail.AlternateViews.Add(CreateHtmlMessage(PopulateBody(
                            Titulo,
                            Nombre,
                            Descripcion,
                            ConfigurationManager.AppSettings["Host"] + vLink
                            ), Server.MapPath("/assets/images/logo.png")));
                        break;
                    case typeBody.Alertas:
                        mail.AlternateViews.Add(CreateHtmlMessage(PopulateBodyAlerta(
                            Nombre,
                            Titulo,
                            Descripcion
                            ), Server.MapPath("/assets/images/logo.png")));
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

        private AlternateView CreateHtmlMessage(string message, string logoPath)
        {
            var inline = new LinkedResource(logoPath, "image/png");
            inline.ContentId = "companyLogo";

            var alternateView = AlternateView.CreateAlternateViewFromString(
                                    message,
                                    Encoding.UTF8,
                                    MediaTypeNames.Text.Html);
            alternateView.LinkedResources.Add(inline);

            return alternateView;
        }

        public string PopulateBody(string vTitulo, string vNombre, string vDescripcion, string vLink)
        {
            string body = string.Empty;
            String vDireccion = Server.MapPath("../../email/TemplateMail.htm");
            using (StreamReader reader = new StreamReader(Server.MapPath("../../email/TemplateMail.html")))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{Host}", ConfigurationManager.AppSettings["Host"]);
            body = body.Replace("{Titulo}", vTitulo);
            body = body.Replace("{Nombre}", vNombre);
            body = body.Replace("{Descripcion}", vDescripcion);
            body = body.Replace("{vLink}", vLink);
            return body;
        }

        public string PopulateBodyAlerta(string vNombre, string vTitulo, string vDescripcion)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("../../email/TemplateMailAlertas.html")))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{Host}", ConfigurationManager.AppSettings["Host"]);
            body = body.Replace("{Nombre}", vNombre);
            body = body.Replace("{Titulo}", vTitulo);
            body = body.Replace("{Descripcion}", vDescripcion);
            return body;
        }


    }
}