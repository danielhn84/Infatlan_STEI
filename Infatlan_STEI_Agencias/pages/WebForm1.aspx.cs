using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infatlan_STEI_Agencias.classes;

namespace Infatlan_STEI_Agencias.pages
{
    public partial class WebForm1 : System.Web.UI.Page
    {
    

        db vConexion = new db();
        protected void Page_Load(object sender, EventArgs e)
        {

            HFReubicar.Value = string.Empty;
            HFDesordenado.Value = string.Empty;
            HFExpuestoHumedo.Value = string.Empty;
            HFExpuestoRobo.Value = string.Empty;
            HFEquiposAjeno.Value = string.Empty;

        }

        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        //RADIOBUTTONLIST
        protected void rblDesordenado_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (rblDesordenado.SelectedValue == "si")
                {
                    fuDesordenado.Visible = true;
                    imgDesordenado.Visible = true;
                    //udpDesordenado.Update();
                }
                else
                {
                    fuDesordenado.Visible = false;
                    imgDesordenado.Visible = false;
                    //udpDesordenado.Update();
                }
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        protected void rblExpuestoHumedad_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (rblExpuestoHumedo.SelectedValue == "si")
                {

                    fuExpuestoHumedo.Visible = true;
                    imgExpuestoHumedo.Visible = true;
                }
                else
                {
                    fuExpuestoHumedo.Visible = false;
                    imgExpuestoHumedo.Visible = false;
                }
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }

        }

        protected void rblExpuestoRobo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (rblExpuestoRobo.SelectedValue == "si")
                {
                    fuExpuestoRobo.Visible = true;
                    imgExpuestoRobo.Visible = true;

                }
                else
                {
                    fuExpuestoRobo.Visible = false;
                    imgExpuestoRobo.Visible = false;
                }
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }


        }

        protected void rblElementosAjenos_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (rblElementoAjenos.SelectedValue == "si")
                {
                    fuElemetoAjenos.Visible = true;
                    imgElementoAjeno.Visible = true;
                }
                else
                {
                    fuElemetoAjenos.Visible = false;
                    imgElementoAjeno.Visible = false;
                }
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }
        }

        protected void rblReubicar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (rblReubicar.SelectedValue == "si")
                {

                    Div1.Visible = true;
                }
                else
                {
                    Div1.Visible = false;
                }

            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger);
            }

        }
    }
}