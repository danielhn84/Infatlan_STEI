using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infatlan_STEI_GestionesTecnicas.classes;
using System.Data;
using System.IO;


namespace Infatlan_STEI_GestionesTecnicas.pages
{
    public partial class tareas : System.Web.UI.Page
    {
        db vConexion = new db();
        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //cargarInicial();
            }
        }

        //void cargarInicial()
        //{
        //    try
        //    {
        //        String vQuery = "STEISP_GESTIONES_Generales 1,'" + Session["USUARIO"] + "'";
        //        DataTable vDatos = vConexion.obtenerDataTable(vQuery);
        //        DdlResponsable.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
        //        if (vDatos.Rows.Count > 0)
        //        {
        //            foreach (DataRow item in vDatos.Rows)
        //            {
        //                DdlResponsable.Items.Add(new ListItem { Value = item["idUsuario"].ToString(), Text = item["nombre"].ToString() });
        //            }
        //        }

        //        vQuery = "STEISP_GESTIONES_Generales 2";
        //        DataTable vDatosTipo = vConexion.obtenerDataTable(vQuery);
        //        DdlTipoGestion.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
        //        if (vDatosTipo.Rows.Count > 0)
        //        {
        //            foreach (DataRow item in vDatosTipo.Rows)
        //            {
        //                DdlTipoGestion.Items.Add(new ListItem { Value = item["idTipoGestion"].ToString(), Text = item["nombreGestion"].ToString() });
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Mensaje(ex.Message, WarningType.Danger);
        //    }

        //}

        protected void BtnAddTarea_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModalAddTarea();", true);
                //String vString = "";
                //vString += "<div class='card card-body'>" +
                //                "<div class='row align-items-center'>" +
                //                    "<div class='col-md-4 col-lg-3 text-center'>" +
                //                        "<a href='app-contact-detail.html'>" +
                //                            "<img src='/assets/images/users/1.jpg' width='90' alt='user' class='img-circle img-fluid'></a>" +
                //                    "</div>" +
                //                    "<div class='col-md-8'>" +
                //                        "<h3 class='box-title m-b-0'>Johnathan Doe</h3>" +
                //                        "<small>Web Designer</small>" +
                //                        "<address>795 Folsom Ave, Suite 600 San Francisco, CADGE 94107" +
                //                       " <br />" +
                //                            "<br />" +
                //                            "<abbr title='Phone'>P:</abbr>" +
                //                            "(123) 456-7890" +
                //                        "</address>" +
                //                    "</div>" +
                //                "</div>" +
                //            "</div>";

                //LitTareas.Text = vString;
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        //protected void LbAddAdjunto_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        String vNombreArchivo = String.Empty;
        //        HttpPostedFile bufferDeposito1T = FuAdjunto.PostedFile;
        //        byte[] vFileDepositoAdjunto = null;
        //        String vExtensionAdjunto = String.Empty;

        //        if (FuAdjunto.HasFile)
        //        {
        //            if (bufferDeposito1T != null)
        //            {
        //                vNombreArchivo = FuAdjunto.FileName;
        //                Stream vStream = bufferDeposito1T.InputStream;
        //                BinaryReader vReader = new BinaryReader(vStream);
        //                vFileDepositoAdjunto = vReader.ReadBytes((int)vStream.Length);
        //                vExtensionAdjunto = System.IO.Path.GetExtension(FuAdjunto.FileName);
        //            }

        //            String vArchivoAdjunto = String.Empty;
        //            if (vFileDepositoAdjunto != null)
        //            {
        //                vArchivoAdjunto = Convert.ToBase64String(vFileDepositoAdjunto);
        //            }
        //            else
        //            {
        //                vArchivoAdjunto = "";
        //            }

        //            DataTable vData = new DataTable();
        //            DataTable vDatos = (DataTable)Session["GESTIONES_TAREAS_ADJUNTO"];
        //            vData.Columns.Add("idAdjunto");
        //            vData.Columns.Add("nombre");
        //            //vData.Columns.Add("ruta");

        //            if (vDatos == null)
        //                vDatos = vData.Clone();

        //            if (vDatos != null)
        //            {
        //                if (vDatos.Rows.Count < 1)
        //                {
        //                    vDatos.Rows.Add("1", vNombreArchivo);
        //                }
        //                else
        //                {
        //                    Boolean vRegistered = false;
        //                    //for (int i = 0; i < vDatos.Rows.Count; i++)
        //                    //{
        //                    //    if (vNombreArchivo == vDatos.Rows[i]["nombre"].ToString())
        //                    //    {
        //                    //        //vDatos.Rows[i]["cantidad"] = Convert.ToDecimal(vDatos.Rows[i]["cantidad"].ToString()) + Convert.ToDecimal(TxCantidad.Text);

        //                    //        lbCantidad.Text = "El material seleccionado: " + vNombreMaterialMatriz + " ya esta agregado en la lista, favor verificar";
        //                    //        DivAlertaCantidad.Visible = true;
        //                    //        UpCantidadMaxima.Update();

        //                    //        vRegistered = true;
        //                    //    }
        //                    //}

        //                    if (!vRegistered)
        //                        vDatos.Rows.Add((vDatos.Rows.Count) + 1, vNombreArchivo);

        //                }
        //            }

        //            GvAdjunto.DataSource = vDatos;
        //            GvAdjunto.DataBind();
        //            Session["GESTIONES_TAREAS_ADJUNTO"] = vDatos;
        //            //UpdatePanel2.Update();
        //            //UpdatePanel3.Update();
        //        }
        //    }
        //    catch (Exception Ex) { Mensaje(Ex.Message, WarningType.Danger); }
        //}

        protected void ticket_1_Click(object sender, EventArgs e)
        {

        }


        protected void Modal_Click(object sender, EventArgs e)
        {
            try
            {
                string vIdSolicitud = LbMasInfo.CommandName.ToString();

                String vQuery = "STEISP_GESTIONES_Solicitud 5,'" + vIdSolicitud + "'";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                TxTitulo.Text = vDatos.Rows[0]["titulo"].ToString();
                TxFechaSolicitud.Text = vDatos.Rows[0]["fechaCreo"].ToString();
                TxDescripcion.Text = vDatos.Rows[0]["descripcion"].ToString();
                TxResponsable.Text = vDatos.Rows[0]["responsable"].ToString();
                DdlPrioridad.SelectedValue = vDatos.Rows[0]["prioridad"].ToString();
                TxTipoGestion.Text = vDatos.Rows[0]["nombreGestion"].ToString();
                TxFechaEntrega.Text = vDatos.Rows[0]["fechaEntrega"].ToString();

                vQuery = "STEISP_GESTIONES_Solicitud 6,'" + vIdSolicitud + "'";
                DataTable vDatosAdjunto = vConexion.obtenerDataTable(vQuery);
  
                if (vDatosAdjunto.Rows.Count > 0){
                    GvAdjunto.DataSource = vDatosAdjunto;
                    GvAdjunto.DataBind();
                    divAdjunto.Visible = true;
                }else{
                    divAdjunto.Visible = false;
                }

                vQuery = "STEISP_GESTIONES_Solicitud 7,'" + vIdSolicitud + "'";
                DataTable vDatosComentarios = vConexion.obtenerDataTable(vQuery);

                if (vDatosComentarios.Rows.Count > 0)
                {
                    GvComentario.DataSource = vDatosComentarios;
                    GvComentario.DataBind();
                    divComentario.Visible = true;
                }
                else
                {
                    divComentario.Visible = false;
                }



                UpdatePanel1.Update();
                UpdatePanel2.Update();
                UpdatePanel3.Update();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModalAddTarea();", true);



            }
            catch (Exception Ex) { Mensaje(Ex.Message, WarningType.Danger); }
        }
    }
}