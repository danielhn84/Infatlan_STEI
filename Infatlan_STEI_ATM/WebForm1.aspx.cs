using Infatlan_STEI_ATM.clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace Infatlan_STEI_ATM
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btncorreo_Click(object sender, EventArgs e)
        {
            SmtpService vService = new SmtpService();
            string vEmpresa = "yujuu@bancatlan.hn";
            string vNombre = "Adan Cedillo";
            string vUsuario = "acedillo";
            string vMotivo = " ha solicitado permiso para realizar mantenimiento en agencia";

            //-PROD- Boolean vFlagEnvioSupervisor = true;
            //Boolean vFlagEnvioSupervisor = false;

            //foreach (DataRow item in vDatosJefatura.Rows)
            //{
            //    if (!item["emailEmpresa"].ToString().Trim().Equals(""))
            //    {
            vService.EnviarMensaje(vEmpresa,
                        typeBody.JefeAgencia,
                        vUsuario,
                        vNombre,
                        vMotivo,
                        txtcoreoBody.Text
                        );
            //        vFlagEnvioSupervisor = true;
            //    }
            //}

            //if (vFlagEnvioSupervisor)
            //{
            //    foreach (DataRow item in vDatosEmpleado.Rows)
            //    {
            //        if (!item["emailEmpresa"].ToString().Trim().Equals(""))
                        vService.EnviarMensaje(vEmpresa,
                            typeBody.Solicitante,
                            vUsuario,
                            vNombre,
                            vMotivo,
                            txtcoreoBody.Text
                            );
            //    }
            //}
        }
    }
}