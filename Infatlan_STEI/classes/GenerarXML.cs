using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.IO;

namespace Infatlan_STEI.classes
{
    public class GenerarXML
    {
        public String ObtenerReporteCumplimiento(Object[] vDatos){
            String vResultado = "";
            try{
                using (StringWriter sw = new StringWriter()){
                    XmlTextWriter vXmlTW = new XmlTextWriter(sw);
                    vXmlTW.Formatting = Formatting.None;

                    vXmlTW.WriteStartDocument();
                    vXmlTW.WriteStartElement("DATOS");

                    vXmlTW.WriteStartElement("callAtendidas");
                    vXmlTW.WriteString(Convert.ToString(vDatos[0]));
                    vXmlTW.WriteEndElement();

                    vXmlTW.WriteStartElement("callPerdidas");
                    vXmlTW.WriteString(Convert.ToString(vDatos[1]));
                    vXmlTW.WriteEndElement();

                    vXmlTW.WriteStartElement("callComentario");
                    vXmlTW.WriteString(Convert.ToString(vDatos[2]));
                    vXmlTW.WriteEndElement();

                    vXmlTW.WriteStartElement("atmCompletos");
                    vXmlTW.WriteString(Convert.ToString(vDatos[3]));
                    vXmlTW.WriteEndElement();

                    vXmlTW.WriteStartElement("atmIncompletos");
                    vXmlTW.WriteString(Convert.ToString(vDatos[4]));
                    vXmlTW.WriteEndElement();

                    vXmlTW.WriteStartElement("atmComentario");
                    vXmlTW.WriteString(Convert.ToString(vDatos[5]));
                    vXmlTW.WriteEndElement();

                    vXmlTW.WriteStartElement("abaCompletos");
                    vXmlTW.WriteString(Convert.ToString(vDatos[6]));
                    vXmlTW.WriteEndElement();

                    vXmlTW.WriteStartElement("abaIncompletos");
                    vXmlTW.WriteString(Convert.ToString(vDatos[7]));
                    vXmlTW.WriteEndElement();

                    vXmlTW.WriteStartElement("abaComentario");
                    vXmlTW.WriteString(Convert.ToString(vDatos[8]));
                    vXmlTW.WriteEndElement();

                    vXmlTW.WriteStartElement("cajaCompletos");
                    vXmlTW.WriteString(Convert.ToString(vDatos[9]));
                    vXmlTW.WriteEndElement();
                    
                    vXmlTW.WriteStartElement("cajaIncompletos");
                    vXmlTW.WriteString(Convert.ToString(vDatos[10]));
                    vXmlTW.WriteEndElement();

                    vXmlTW.WriteStartElement("cajaComentario");
                    vXmlTW.WriteString(Convert.ToString(vDatos[11]));
                    vXmlTW.WriteEndElement();

                    vXmlTW.WriteStartElement("kpiCompletas");
                    vXmlTW.WriteString(Convert.ToString(vDatos[12]));
                    vXmlTW.WriteEndElement();                    
                    
                    vXmlTW.WriteStartElement("kpiIncompletas");
                    vXmlTW.WriteString(Convert.ToString(vDatos[13]));
                    vXmlTW.WriteEndElement();
                    
                    vXmlTW.WriteStartElement("kpiComentario");
                    vXmlTW.WriteString(Convert.ToString(vDatos[14]));
                    vXmlTW.WriteEndElement();
                    
                    vXmlTW.WriteStartElement("estado");
                    vXmlTW.WriteString(Convert.ToString(vDatos[15]));
                    vXmlTW.WriteEndElement();
                    
                    vXmlTW.WriteStartElement("usuarioRegistro");
                    vXmlTW.WriteString(Convert.ToString(vDatos[16]));
                    vXmlTW.WriteEndElement();
                    
                    vXmlTW.WriteEndElement();
                    vXmlTW.WriteEndDocument();
                    vResultado = sw.ToString();
                }
            }catch{
                throw;
            }
            return vResultado;
        }

        public String ObtenerReporteCumplimientoKPI(Object[] vDatos){
            String vResultado = "";
            try{
                using (StringWriter sw = new StringWriter()){
                    XmlTextWriter vXmlTW = new XmlTextWriter(sw);
                    vXmlTW.Formatting = Formatting.None;

                    vXmlTW.WriteStartDocument();
                    vXmlTW.WriteStartElement("DATOS");

                    vXmlTW.WriteStartElement("idReporte");
                    vXmlTW.WriteString(Convert.ToString(vDatos[0]));
                    vXmlTW.WriteEndElement();

                    vXmlTW.WriteStartElement("orden");
                    vXmlTW.WriteString(Convert.ToString(vDatos[1]));
                    vXmlTW.WriteEndElement();

                    vXmlTW.WriteStartElement("tiempo");
                    vXmlTW.WriteString(Convert.ToString(vDatos[2]));
                    vXmlTW.WriteEndElement();

                    vXmlTW.WriteStartElement("cat1");
                    vXmlTW.WriteString(Convert.ToString(vDatos[3]));
                    vXmlTW.WriteEndElement();

                    vXmlTW.WriteStartElement("cat2");
                    vXmlTW.WriteString(Convert.ToString(vDatos[4]));
                    vXmlTW.WriteEndElement();

                    vXmlTW.WriteStartElement("cat3");
                    vXmlTW.WriteString(Convert.ToString(vDatos[5]));
                    vXmlTW.WriteEndElement();

                    vXmlTW.WriteEndElement();
                    vXmlTW.WriteEndDocument();
                    vResultado = sw.ToString();
                }
            }catch{
                throw;
            }
            return vResultado;
        }

        public String ObtenerReporteCumplimientoRupturas(Object[] vDatos){
            String vResultado = "";
            try{
                using (StringWriter sw = new StringWriter()){
                    XmlTextWriter vXmlTW = new XmlTextWriter(sw);
                    vXmlTW.Formatting = Formatting.None;

                    vXmlTW.WriteStartDocument();
                    vXmlTW.WriteStartElement("DATOS");

                    vXmlTW.WriteStartElement("idReporte");
                    vXmlTW.WriteString(Convert.ToString(vDatos[0]));
                    vXmlTW.WriteEndElement();

                    vXmlTW.WriteStartElement("orden");
                    vXmlTW.WriteString(Convert.ToString(vDatos[1]));
                    vXmlTW.WriteEndElement();

                    vXmlTW.WriteStartElement("tiempo");
                    vXmlTW.WriteString(Convert.ToString(vDatos[2]));
                    vXmlTW.WriteEndElement();

                    vXmlTW.WriteStartElement("responsable");
                    vXmlTW.WriteString(Convert.ToString(vDatos[3]));
                    vXmlTW.WriteEndElement();

                    vXmlTW.WriteStartElement("idRazon");
                    vXmlTW.WriteString(Convert.ToString(vDatos[4]));
                    vXmlTW.WriteEndElement();

                    vXmlTW.WriteStartElement("comentario");
                    vXmlTW.WriteString(Convert.ToString(vDatos[5]));
                    vXmlTW.WriteEndElement();

                    vXmlTW.WriteEndElement();
                    vXmlTW.WriteEndDocument();
                    vResultado = sw.ToString();
                }
            }catch{
                throw;
            }
            return vResultado;
        }

        public String ObtenerReporteCumplimientoOSER(Object[] vDatos){
            String vResultado = "";
            try{
                using (StringWriter sw = new StringWriter()){
                    XmlTextWriter vXmlTW = new XmlTextWriter(sw);
                    vXmlTW.Formatting = Formatting.None;

                    vXmlTW.WriteStartDocument();
                    vXmlTW.WriteStartElement("DATOS");

                    vXmlTW.WriteStartElement("idReporte");
                    vXmlTW.WriteString(Convert.ToString(vDatos[0]));
                    vXmlTW.WriteEndElement();

                    vXmlTW.WriteStartElement("orden");
                    vXmlTW.WriteString(Convert.ToString(vDatos[1]));
                    vXmlTW.WriteEndElement();

                    vXmlTW.WriteStartElement("tiempoRespuesta");
                    vXmlTW.WriteString(Convert.ToString(vDatos[2]));
                    vXmlTW.WriteEndElement();

                    vXmlTW.WriteStartElement("tiempoAtencion");
                    vXmlTW.WriteString(Convert.ToString(vDatos[3]));
                    vXmlTW.WriteEndElement();

                    vXmlTW.WriteStartElement("responsable");
                    vXmlTW.WriteString(Convert.ToString(vDatos[4]));
                    vXmlTW.WriteEndElement(); 
                    
                    vXmlTW.WriteStartElement("satisfaccionCliente");
                    vXmlTW.WriteString(Convert.ToString(vDatos[5]));
                    vXmlTW.WriteEndElement();

                    vXmlTW.WriteStartElement("idRazon");
                    vXmlTW.WriteString(Convert.ToString(vDatos[6]));
                    vXmlTW.WriteEndElement();

                    vXmlTW.WriteStartElement("comentario");
                    vXmlTW.WriteString(Convert.ToString(vDatos[7]));
                    vXmlTW.WriteEndElement();

                    vXmlTW.WriteEndElement();
                    vXmlTW.WriteEndDocument();
                    vResultado = sw.ToString();
                }
            }catch{
                throw;
            }
            return vResultado;
        }
    }
}