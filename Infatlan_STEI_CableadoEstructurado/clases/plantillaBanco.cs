using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Web.Mvc;

namespace Infatlan_STEI_CableadoEstructurado.clases
{
    public class plantillaBanco : Controller
    {
        // GET: plantillaBanco
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult pdf()
        {

            MemoryStream ms = new MemoryStream();

            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 0, 0, 0, 0);
            // Indicamos donde vamos a guardar el documento
            PdfWriter writer = PdfWriter.GetInstance(doc, ms);

            doc.Open();
            doc.Add(new Paragraph("Prueba PDF /n Plantilla PDF "));

            doc.Close();

            byte[] bytesStream = ms.ToArray();

            ms = new MemoryStream();
            ms.Write(bytesStream, 0, bytesStream.Length);
            ms.Position = 0;

            return new FileStreamResult(ms, "application/pdf");


        }
    }
}