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
    public partial class creacionTareas : System.Web.UI.Page
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
                cargarInicial();
               
            }
        }

        void cargarInicial()
        {
            try
            {
                TxFechaSolicitud.Text = Convert.ToString( DateTime.Now);
                Session["USUARIO"] = "mgarcia";
                String vQuery = "STEISP_GESTIONES_Generales 1,'" + Session["USUARIO"] + "'";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                DdlResponsable.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                if (vDatos.Rows.Count > 0)
                {
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DdlResponsable.Items.Add(new ListItem { Value = item["idUsuario"].ToString(), Text = item["nombre"].ToString() });
                    }
                }

                vQuery = "STEISP_GESTIONES_Generales 2";
                DataTable vDatosTipo = vConexion.obtenerDataTable(vQuery);
                DdlTipoGestion.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                if (vDatosTipo.Rows.Count > 0)
                {
                    foreach (DataRow item in vDatosTipo.Rows)
                    {
                        DdlTipoGestion.Items.Add(new ListItem { Value = item["idTipoGestion"].ToString(), Text = item["nombreGestion"].ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }

        }
        protected void BtnAddAdjunto_Click(object sender, EventArgs e)
        {
            try
            {
                String vNombreArchivo = String.Empty;
                HttpPostedFile bufferDeposito1T = FuAdjunto.PostedFile;
                byte[] vFileDepositoAdjunto = null;
                String vExtensionAdjunto = String.Empty;

                if (FuAdjunto.HasFile)
                {
                    if (bufferDeposito1T != null)
                    {
                        vNombreArchivo = FuAdjunto.FileName;
                        Stream vStream = bufferDeposito1T.InputStream;
                        BinaryReader vReader = new BinaryReader(vStream);
                        vFileDepositoAdjunto = vReader.ReadBytes((int)vStream.Length);
                        vExtensionAdjunto = System.IO.Path.GetExtension(FuAdjunto.FileName);
                    }

                    String vArchivoAdjunto = String.Empty;
                    if (vFileDepositoAdjunto != null)
                    {
                        vArchivoAdjunto = Convert.ToBase64String(vFileDepositoAdjunto);
                    }
                    else
                    {
                        vArchivoAdjunto = "";
                    }

                    DataTable vData = new DataTable();
                    DataTable vDatos = (DataTable)Session["GESTIONES_TAREAS_ADJUNTO"];
                    vData.Columns.Add("idAdjunto");
                    vData.Columns.Add("nombre");
                    vData.Columns.Add("ruta");

                    if (vDatos == null)
                        vDatos = vData.Clone();

                    if (vDatos != null)
                    {
                        if (vDatos.Rows.Count < 1)
                        {
                            vDatos.Rows.Add("1", vNombreArchivo, vArchivoAdjunto);
                        }
                        else
                        {
                            Boolean vRegistered = false;
                            //for (int i = 0; i < vDatos.Rows.Count; i++)
                            //{
                            //    if (vNombreArchivo == vDatos.Rows[i]["nombre"].ToString())
                            //    {
                            //        //vDatos.Rows[i]["cantidad"] = Convert.ToDecimal(vDatos.Rows[i]["cantidad"].ToString()) + Convert.ToDecimal(TxCantidad.Text);

                            //        lbCantidad.Text = "El material seleccionado: " + vNombreMaterialMatriz + " ya esta agregado en la lista, favor verificar";
                            //        DivAlertaCantidad.Visible = true;
                            //        UpCantidadMaxima.Update();

                            //        vRegistered = true;
                            //    }
                            //}

                            if (!vRegistered)
                                vDatos.Rows.Add((vDatos.Rows.Count) + 1, vNombreArchivo, vArchivoAdjunto);

                        }
                    }

                    GvAdjunto.DataSource = vDatos;
                    GvAdjunto.DataBind();
                    Session["GESTIONES_TAREAS_ADJUNTO"] = vDatos;
                    divAdjunto.Visible = true;
                    UpdatePanel2.Update();
                    //Response.Redirect("/sites/gestiones/pages/creacionTareas.aspx#adjunto");



                }
            }
            catch (Exception Ex) { Mensaje(Ex.Message, WarningType.Danger); }
        }
        protected void BtnAddComentario_Click(object sender, EventArgs e)
        {
            try
            {
                string hora = DateTime.Now.ToString("hh:mm:ss");
                DataTable vData = new DataTable();
                DataTable vDatos = (DataTable)Session["GESTIONES_TAREAS_COMENTARIOS"];
                vData.Columns.Add("idComentario");
                vData.Columns.Add("usuario");
                vData.Columns.Add("comentario");

                if (vDatos == null)
                    vDatos = vData.Clone();

                if (vDatos != null)
                {
                    if (vDatos.Rows.Count < 1)
                    {
                        vDatos.Rows.Add("1", Session["USUARIO"].ToString()+ ' '+ hora, TxComentario.Text);
                    }
                    else
                    {
                            vDatos.Rows.Add((vDatos.Rows.Count) + 1, Session["USUARIO"].ToString() + ' ' + hora, TxComentario.Text);
                    }
                }

                GvComentario.DataSource = vDatos;
                GvComentario.DataBind();
                Session["GESTIONES_TAREAS_COMENTARIOS"] = vDatos;
                divComentario.Visible = true;

            }
            catch (Exception Ex) { Mensaje(Ex.Message, WarningType.Danger); }
        }

        protected void BtnEnviar_Click(object sender, EventArgs e)
        {
            try
            {

                TxTituloModal.Text = TxTitulo.Text;
                TxMinProductivoModal.Text=TxMinProductivo.Text;
                TxEntregaModal.Text=TxFechaEntrega.Text;
                UpdatePanel3.Update();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "abrirModalConfirmacion();", true);
   


                //String vQuery1 = "STEISP_GESTIONES_Solicitud 1,'"
                //                   + TxTitulo.Text
                //                   + "','" + TxDescripcion.Text 
                //                   + "','" + DdlTipoGestion.SelectedValue 
                //                   + "','" + DdlResponsable.SelectedValue 
                //                   + "','" + DdlPrioridad.SelectedValue 
                //                   + "','" + TxFechaEntrega.Text 
                //                   + "','" + Session["USUARIO"].ToString()
                //                   + "','" + TxFechaSolicitud.Text + "'";
                //Int32 vInformacion1 = vConexion.ejecutarSql(vQuery1);

                //String vQuery2 = "STEISP_GESTIONES_Solicitud 2,'" + TxTitulo.Text
                //                     + "','" + DdlTipoGestion.SelectedValue
                //                     + "','" + Session["USUARIO"].ToString()
                //                     + "','" + TxFechaSolicitud.Text + "'";
                //DataTable vDatos = vConexion.obtenerDataTable(vQuery2);
                //string idSolicitud = vDatos.Rows[0]["idSolicitud"].ToString();


                //DataTable vDatosComentarios = (DataTable)Session["GESTIONES_TAREAS_COMENTARIOS"];
                //if (vDatosComentarios.Rows.Count > 0)
                //{
                //    for (int num = 0; num < vDatosComentarios.Rows.Count; num++)
                //    {
                //        string usuario = vDatosComentarios.Rows[num]["usuario"].ToString();
                //        string comentario = vDatosComentarios.Rows[num]["comentario"].ToString();

                //        String vQuery3 = "STEISP_GESTIONES_Solicitud 3,'" + idSolicitud +
                //            "','" + comentario +
                //            "','" + usuario + "'";
                //        Int32 vInfo2 = vConexion.ejecutarSql(vQuery3);
                //    }
                //}

                //DataTable vDatosAdjuntos = (DataTable)Session["GESTIONES_TAREAS_ADJUNTO"];
                //if (vDatosAdjuntos.Rows.Count > 0)
                //{
                //    for (int num = 0; num < vDatosAdjuntos.Rows.Count; num++)
                //    {
                //        string ruta = vDatosAdjuntos.Rows[num]["ruta"].ToString();
                //        string nombre = vDatosAdjuntos.Rows[num]["nombre"].ToString();

                //        String vQuery4 = "STEISP_GESTIONES_Solicitud 4,'" + idSolicitud +
                //            "','" + ruta +
                //            "','" + nombre + "'";
                //        Int32 vInfo2 = vConexion.ejecutarSql(vQuery4);
                //    }
                //}

            }
            catch (Exception Ex) { Mensaje(Ex.Message, WarningType.Danger); }
        }

        protected void BtnConfirmarTarea_Click(object sender, EventArgs e)
        {
            try
            {
                String vQuery1 = "STEISP_GESTIONES_Solicitud 1,'"
                                   + TxTitulo.Text
                                   + "','" + TxDescripcion.Text
                                   + "','" + DdlTipoGestion.SelectedValue
                                   + "','" + DdlResponsable.SelectedValue
                                   + "','" + DdlPrioridad.SelectedValue
                                   + "','" + TxFechaEntrega.Text
                                   + "','" + Session["USUARIO"].ToString()
                                   + "','" + TxFechaSolicitud.Text + "'"
                                   + ",'" + TxMinProductivo.Text + "'"
                                   + ",'1'";
                Int32 vInformacion1 = vConexion.ejecutarSql(vQuery1);

                String vQuery2 = "STEISP_GESTIONES_Solicitud 2,'" + TxTitulo.Text
                                     + "','" + DdlTipoGestion.SelectedValue
                                     + "','" + Session["USUARIO"].ToString()
                                     + "','" + TxFechaSolicitud.Text + "'";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery2);
                string idSolicitud = vDatos.Rows[0]["idSolicitud"].ToString();


                DataTable vDatosComentarios = (DataTable)Session["GESTIONES_TAREAS_COMENTARIOS"];
                if (vDatosComentarios.Rows.Count > 0)
                {
                    for (int num = 0; num < vDatosComentarios.Rows.Count; num++)
                    {
                        string usuario = vDatosComentarios.Rows[num]["usuario"].ToString();
                        string comentario = vDatosComentarios.Rows[num]["comentario"].ToString();

                        String vQuery3 = "STEISP_GESTIONES_Solicitud 3,'" + idSolicitud +
                            "','" + comentario +
                            "','" + usuario + "'";
                        Int32 vInfo2 = vConexion.ejecutarSql(vQuery3);
                    }
                }

                DataTable vDatosAdjuntos = (DataTable)Session["GESTIONES_TAREAS_ADJUNTO"];
                if (vDatosAdjuntos.Rows.Count > 0)
                {
                    for (int num = 0; num < vDatosAdjuntos.Rows.Count; num++)
                    {
                        string ruta = vDatosAdjuntos.Rows[num]["ruta"].ToString();
                        string nombre = vDatosAdjuntos.Rows[num]["nombre"].ToString();

                        String vQuery4 = "STEISP_GESTIONES_Solicitud 4,'" + idSolicitud +
                            "','" + ruta +
                            "','" + nombre + "'";
                        Int32 vInfo2 = vConexion.ejecutarSql(vQuery4);
                    }
                }

                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "cerrarModalConfirmacion();", true);


            }
            catch (Exception Ex) { Mensaje(Ex.Message, WarningType.Danger); }
        }
    }
}