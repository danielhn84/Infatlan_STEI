using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Xml;

namespace Infatlan_STEI_Inventario.clases
{
    public class generarxml
    {
        public String ObtenerMaestroString(Object[] vDatos){
            String vResultado = "";
            try{
                using (StringWriter sw = new StringWriter()){
                    XmlTextWriter vXmlTW = new XmlTextWriter(sw);
                    vXmlTW.Formatting = Formatting.None;

                    vXmlTW.WriteStartDocument();
                    vXmlTW.WriteStartElement("DATOS");

                    vXmlTW.WriteStartElement("codigoInventario");
                    vXmlTW.WriteString(Convert.ToString(vDatos[0]));
                    vXmlTW.WriteEndElement();

                    vXmlTW.WriteStartElement("idStock");
                    vXmlTW.WriteString(Convert.ToString(vDatos[1]));
                    vXmlTW.WriteEndElement();

                    vXmlTW.WriteStartElement("idUbicacion");
                    vXmlTW.WriteString(Convert.ToString(vDatos[2]));
                    vXmlTW.WriteEndElement();

                    vXmlTW.WriteStartElement("idResponsable");
                    vXmlTW.WriteString(Convert.ToString(vDatos[3]));
                    vXmlTW.WriteEndElement();

                    vXmlTW.WriteStartElement("descripcion");
                    vXmlTW.WriteString(Convert.ToString(vDatos[4]));
                    vXmlTW.WriteEndElement();

                    vXmlTW.WriteStartElement("observaciones");
                    vXmlTW.WriteString(Convert.ToString(vDatos[5]));
                    vXmlTW.WriteEndElement();

                    vXmlTW.WriteStartElement("serie");
                    vXmlTW.WriteString(Convert.ToString(vDatos[6]));
                    vXmlTW.WriteEndElement();

                    vXmlTW.WriteStartElement("precio");
                    vXmlTW.WriteString(Convert.ToString(vDatos[7]));
                    vXmlTW.WriteEndElement();

                    vXmlTW.WriteStartElement("usuarioCreacion");
                    vXmlTW.WriteString(Convert.ToString(vDatos[8]));
                    vXmlTW.WriteEndElement();

                    vXmlTW.WriteStartElement("idTipoTransaccion");
                    vXmlTW.WriteString(Convert.ToString(vDatos[9]));
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