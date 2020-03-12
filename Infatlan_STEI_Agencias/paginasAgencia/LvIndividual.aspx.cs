using Infatlan_STEI_Agencias.classes;
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Infatlan_STEI_Agencias.paginasAgencia
{
    public partial class LvIndividual : System.Web.UI.Page
    {

        db vConexion = new db();
        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                cargarData();
            }
        }

        private void cargarData()
        {
            try
            {
                //Label vTitulo = new Label();
                //TextBox vNuevo = new TextBox();
                //vNuevo.CssClass = "form-control col-6";
                //vTitulo.Text = "Pregunta 1:";

                //PHCampos.Controls.Add(vTitulo);
                //PHCampos.Controls.Add(vNuevo);

                String vQuery = "STEISP_AGENCIA_CompletarListaVerificacion 2";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                Session["AG_PREGUNTAS_SECCION1"] = vDatos;
                GVDatosTecnicos.DataSource = vDatos;
                GVDatosTecnicos.DataBind();

                int cant = vDatos.Rows.Count;
                foreach (GridViewRow item in GVDatosTecnicos.Rows)
                {
                    if (item.Cells[0].Text.Equals("1"))
                    {
                        TextBox tx = (TextBox)item.FindControl("TextBox2") as TextBox;
                        tx.Visible = true;
                    }
                    else
                    {
                        LinkButton rb = (LinkButton)item.FindControl("LBRadio") as LinkButton;
                        rb.Visible = true;

                        //TextBox tx2 = (TextBox)item.FindControl("TextBox4") as TextBox;
                        //tx2.Visible = true;

                        //FileUpload FU= (FileUpload)item.FindControl("FileUpload5") as FileUpload;
                        //FU.Visible = true;

                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

 

        protected void RBLManEquipoComu_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (RBLManEquipoComu.SelectedValue.Equals("Si"))
            {
                DivImagenNoMantEquipoComu.Visible = true;
                DivMotivoMantEquipoComu.Visible = false;
                UpdatePanel2.Update();
            }
            else
            {
                DivMotivoMantEquipoComu.Visible = true;
                DivImagenNoMantEquipoComu.Visible = false;
                UpdatePanel2.Update();
            }


        }

        protected void RBProbaronEquipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RBProbaronEquipo.SelectedValue.Equals("Si"))
            {

                LbMotivoNoProbaronEquipo.Visible = false;
                TxMotivoNoProbaronEquipo.Visible = false;
                UpNoProbaronEquipo.Update();
           }
            else
            {
                LbMotivoNoProbaronEquipo.Visible = true;
                TxMotivoNoProbaronEquipo.Visible = true;
                UpNoProbaronEquipo.Update();
            }

        }

        protected void RblClimatizacionAdecuada_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (RblClimatizacionAdecuada.SelectedValue.Equals("No"))
            {
                FuClimatizacion.Visible = false;
                DivAireAcondicionado.Visible = false;
                UpClimatizacion.Update();
            }
            else
            {
                FuClimatizacion.Visible = true;
                DivAireAcondicionado.Visible = true;
                UpClimatizacion.Update();
            }            
        }


        protected void RblUPS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RblUPS.SelectedValue.Equals("No"))
            {
                FuUPS.Visible = false;
                DivUPS.Visible = false;
                UpUPS.Update();
            }
            else
            {
                FuUPS.Visible = true;
                DivUPS.Visible = true;
                UpUPS.Update();
            }
        }

        protected void RbPolvoSuciedad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RbPolvoSuciedad.SelectedValue.Equals("No"))
            {
                FuPolvoSuciedad.Visible = false;
                DivPolvoSuciedad.Visible = false;
                UpPolvoSuciedad.Update();
            }
            else
            {
                FuPolvoSuciedad.Visible = true;
                DivPolvoSuciedad.Visible = true;
                UpPolvoSuciedad.Update();
            }
        }

 

        protected void RblHumedadSustancias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RblHumedadSustancias.SelectedValue.Equals("No"))
            {
                FuHumedadSustancias.Visible = false;
                DivHumedadSustancias.Visible = false;
                UpHumedadSustancias.Update();
            }
            else
            {
                FuHumedadSustancias.Visible = true;
                DivHumedadSustancias.Visible = true;
                UpHumedadSustancias.Update();
            }
        }
    }
}