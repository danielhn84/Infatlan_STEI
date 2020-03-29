using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infatlan_STEI_ATM.clases;
using System.Data;
using System.IO;
using System.Configuration;

namespace Infatlan_STEI_ATM
{
    public partial class PRUEBAIMG : System.Web.UI.Page
    {
        bd vConexion = new bd();
        protected void Page_Load(object sender, EventArgs e)
        {
            ControlImagenes();
        }
        void ControlImagenes()
        {
            RBLDiscoDuro.SelectedValue = "1";
            RBLATMDesarmadoPS.SelectedValue = "1";
            RBLATMDesarmadoPI.SelectedValue = "1";
            RBLVendor.SelectedValue = "1";
            RBLSystemInfo.SelectedValue = "1";
            RBLAntiSkimming.SelectedValue = "1";
            RBLMonitorFiltro.SelectedValue = "1";
            RBLPadleWheel.SelectedValue = "1";
            RBLDispDesarmado.SelectedValue = "1";
            RBLTeclado.SelectedValue = "1";
            if (RBLClimatizacion.SelectedValue == "1")
            {
                FUClimatizacion.Enabled = true;
            }
            else
            {
                FUClimatizacion.Enabled = false;
            }
            if (RBLEnergiaElectrica.SelectedValue == "1")
            {
                FUEnergia.Enabled = true;
            }
            else
            {
                FUEnergia.Enabled = false;
            }
        }
        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }
        private string GetExtension(string Extension)
        {
            switch (Extension)
            {
                case ".doc":
                    return "application/ms-word";
                case ".xls":
                    return "application/vnd.ms-excel";
                case ".ppt":
                    return "application/mspowerpoint";
                case "jpeg":
                    return "image/jpeg";
                case ".bmp":
                    return "image/bmp";
                case ".zip":
                    return "application/zip";
                case ".log":
                    return "text/HTML";
                case ".txt":
                    return "text/plain";
                case ".tiff":
                case ".tif":
                    return "image/tiff";
                case ".asf":
                    return "video/x-ms-asf";
                case ".avi":
                    return "video/avi";
                case ".gif":
                    return "image/gif";
                case ".jpg":
                case ".wav":
                    return "audio/wav";
                case ".pdf":
                    return "application/pdf";
                case ".fdf":
                    return "application/vnd.fdf";
                case ".dwg":
                    return "image/vnd.dwg";
                case ".msg":
                    return "application/msoutlook";
                case ".xml":
                    return "application/xml";
                default:
                    return "application/octet-stream";
            }
        }
        public bool ThumbnailCallback()
        {
            return false;
        }
        protected void btnguardar_Click(object sender, EventArgs e)
        {
            //IMAGENES1
            String vNombreDepot1 = String.Empty;
            HttpPostedFile bufferDeposito1T = FUDiscoDuro.PostedFile;
            byte[] vFileDeposito1 = null;
            string vExtension = string.Empty;

            if (bufferDeposito1T != null)
            {
                vNombreDepot1 = FUDiscoDuro.FileName;
                Stream vStream = bufferDeposito1T.InputStream;
                BinaryReader vReader = new BinaryReader(vStream);
                vFileDeposito1 = vReader.ReadBytes((int)vStream.Length);
                vExtension = System.IO.Path.GetExtension(FUDiscoDuro.FileName);
            }

            ///THUMBNAILS///
            //----------        Getting the Image File
            System.Drawing.Image img = System.Drawing.Image.FromFile(Server.MapPath("~/IMG_PRUEBAS/1.png"));

            //----------        Getting Size of Original Image
            double imgHeight = img.Size.Height;
            double imgWidth = img.Size.Width;

            //----------        Getting Decreased Size
            double x = imgWidth / 200;
            int newWidth = Convert.ToInt32(imgWidth / x);
            int newHeight = Convert.ToInt32(imgHeight / x);

            //----------        Creating Small Image
            System.Drawing.Image.GetThumbnailImageAbort myCallback = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);
            System.Drawing.Image myThumbnail = img.GetThumbnailImage(newWidth, newHeight, myCallback, IntPtr.Zero);

            //----------        Saving Image
            myThumbnail.Save(Server.MapPath("~/IMG_PRUEBAS/NewImage.jpg"));
        }

    }

}