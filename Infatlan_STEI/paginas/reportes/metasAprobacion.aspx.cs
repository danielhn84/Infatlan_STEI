using Infatlan_STEI.classes;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Infatlan_STEI.paginas.reportes
{
    public partial class metasAprobacion : System.Web.UI.Page
    {
        db vConexion = new db();
        Security vSecurity = new Security();
        GenerarXML vMaestro = new GenerarXML();

        protected void Page_Load(object sender, EventArgs e){
            try{
                if (!Page.IsPostBack){
                    if (Convert.ToBoolean(Session["AUTH"])){
                        if (!vSecurity.ObtenerPermiso(Session["USUARIO"].ToString(), 5).Consulta)
                            Response.Redirect("/default.aspx");

                        String vId = Session["CUMPL_ID_REPORTE"].ToString();
                        cargarDatos(vId);
                    }else{
                        Response.Redirect("/login.aspx");
                    }
                }
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        public void cargarDatos(String vId){
            try{
                String vQuery = "[STEISP_CUMPLIMIENTO_Reportes] 4," + vId;
                DataSet vDSet = vConexion.obtenerDataSet(vQuery);
                cargarTextos(vDSet.Tables[0]);

                if (vDSet.Tables[1].Rows.Count > 0){
                    GvKPISolicitudes.DataSource = vDSet.Tables[1];
                    GvKPISolicitudes.DataBind();
                    Session["CUMPL_APR_KPI"] = vDSet.Tables[1];
                    DivKPI.Visible = true;
                }

                if (vDSet.Tables[2].Rows.Count > 0){
                    GvRuptura.DataSource = vDSet.Tables[2];
                    GvRuptura.DataBind();
                    Session["CUMPL_APR_RUPTURA"] = vDSet.Tables[2];
                    LbResRuptura.Visible = false;
                    DivRuptura.Visible = true;
                }

                if (vDSet.Tables[3].Rows.Count > 0){
                    GvOSER.DataSource = vDSet.Tables[3];
                    Session["CUMPL_APR_OSER"] = vDSet.Tables[3];
                    GvOSER.DataBind();
                    LbResOSER.Visible = false;
                }

                if (vDSet.Tables[4].Rows.Count > 0){
                    GvRendimiento.DataSource = vDSet.Tables[4];
                    GvRendimiento.DataBind();
                    Session["CUMPL_APR_RENDIMIENTO"] = vDSet.Tables[4];
                    graficos(vDSet.Tables[4]);
                }

                if (vDSet.Tables[5].Rows.Count > 0){
                    LbInsatisfaccion.Visible = false;
                    GvInsatisfacciones.DataSource = vDSet.Tables[5];
                    Session["CUMPL_APR_SATISFACCION"] = vDSet.Tables[5];
                    GvInsatisfacciones.DataBind();
                }
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        public void Mensaje(string vMensaje, WarningType type){
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }

        protected void GvKPISolicitudes_PageIndexChanging(object sender, GridViewPageEventArgs e){
            try{
                GvKPISolicitudes.PageIndex = e.NewPageIndex;
                GvKPISolicitudes.DataSource = (DataTable)Session["CUMPL_APR_KPI"];
                GvKPISolicitudes.DataBind();
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void GvOSER_PageIndexChanging(object sender, GridViewPageEventArgs e){
            try{
                GvOSER.PageIndex = e.NewPageIndex;
                GvOSER.DataSource = (DataTable)Session["CUMPL_APR_OSER"];
                GvOSER.DataBind();
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void GvRuptura_PageIndexChanging(object sender, GridViewPageEventArgs e){
            try{
                GvRuptura.PageIndex = e.NewPageIndex;
                GvRuptura.DataSource = (DataTable)Session["CUMPL_APR_RUPTURA"];
                GvRuptura.DataBind();
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void GvInsatisfacciones_PageIndexChanging(object sender, GridViewPageEventArgs e){
            try{
                GvInsatisfacciones.PageIndex = e.NewPageIndex;
                GvInsatisfacciones.DataSource = (DataTable)Session["CUMPL_APR_SATISFACCION"];
                GvInsatisfacciones.DataBind();
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void GvRendimiento_PageIndexChanging(object sender, GridViewPageEventArgs e){
            try{
                GvRendimiento.PageIndex = e.NewPageIndex;
                GvRendimiento.DataSource = (DataTable)Session["CUMPL_APR_RENDIMIENTO"];
                GvRendimiento.DataBind();
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        private void cargarTextos(DataTable vData){
            try{
                TxCallAtendidas.Text = vData.Rows[0]["callAtendidas"].ToString();
                TxCallAtendidasNo.Text = vData.Rows[0]["callPerdidas"].ToString();
                TxCallObs.Text = vData.Rows[0]["callComentario"].ToString();
                TxCallTotal.Text = vData.Rows[0]["callTotal"].ToString();
                if (Convert.ToInt32(TxCallTotal.Text) > 0){
                    float vProm = float.Parse(TxCallAtendidas.Text) / float.Parse(TxCallTotal.Text) * 100;
                    Decimal vCallProm = Convert.ToDecimal(Math.Round(vProm));
                    TxCallPorcentajeSi.Text = vCallProm.ToString();
                    TxCallPorcentajeNo.Text = (100 - vCallProm).ToString();
                    CCall.Attributes.Add("data-percent", TxCallPorcentajeSi.Text);
                }

                TxATMCumplimiento.Text = vData.Rows[0]["atmCompletos"].ToString();
                TxATMCumplimientoNo.Text = vData.Rows[0]["atmIncompletos"].ToString();
                TxATMObs.Text = vData.Rows[0]["atmComentario"].ToString();
                TxATMTotal.Text = vData.Rows[0]["atmTotal"].ToString();
                if (Convert.ToInt32(TxATMTotal.Text) > 0){
                    Decimal vATMProm = Convert.ToDecimal(Math.Round(float.Parse(TxATMCumplimiento.Text) / float.Parse(TxATMTotal.Text) * 100));
                    TxATMPorcentaje.Text = vATMProm.ToString();
                    LitATM.Text = "<div class='chart easy-pie-chart-4' data-percent='" + vATMProm.ToString() + "'><span class='percent'></span></div>";
                }else
                    TxATMPorcentaje.Text = "100";

                TxABACumplimiento.Text = vData.Rows[0]["abaCompletos"].ToString();
                TxABACumplimientoNo.Text = vData.Rows[0]["abaIncompletos"].ToString();
                TxABAObs.Text = vData.Rows[0]["abaComentario"].ToString();
                TxABATotal.Text = vData.Rows[0]["abaTotal"].ToString();
                if (Convert.ToInt32(TxABATotal.Text) > 0){
                    Decimal vABAProm = Convert.ToDecimal(Math.Round(float.Parse(TxABACumplimiento.Text) / float.Parse(TxABATotal.Text) * 100));
                    TxABAPorcentaje.Text = vABAProm.ToString();
                }else
                    TxABAPorcentaje.Text = "100";

                LitABA.Text = "<div class='chart easy-pie-chart-4' data-percent='" + TxABAPorcentaje.Text + "'><span class='percent'></span></div>";

                TxCajaCumplidas.Text = vData.Rows[0]["cajaCompletos"].ToString();
                TxCajaCumplidasNo.Text = vData.Rows[0]["cajaIncompletos"].ToString();
                TxCajaObs.Text = vData.Rows[0]["cajaComentario"].ToString();
                TxCajaTotal.Text = vData.Rows[0]["cajaTotal"].ToString();
                if (Convert.ToInt32(TxCajaTotal.Text) > 0){
                    Decimal vCajaProm = Convert.ToDecimal(Math.Round(float.Parse(TxCajaCumplidas.Text) / float.Parse(TxCajaTotal.Text) * 100));
                    TxCajaPorcentaje.Text = vCajaProm.ToString();
                }else
                    TxCajaPorcentaje.Text = "100";

                LitCaja.Text = "<div class='chart easy-pie-chart-4' data-percent='" + TxCajaPorcentaje.Text + "'><span class='percent'></span></div>";

                TxKPICumplimiento.Text = vData.Rows[0]["kpiCompletas"].ToString();
                TxKPICumplimientoNo.Text = vData.Rows[0]["kpiIncompletas"].ToString();
                TxKPIObs.Text = vData.Rows[0]["kpiComentario"].ToString();
                TxKPITotal.Text = vData.Rows[0]["kpiTotal"].ToString();
                if (Convert.ToInt32(TxCajaTotal.Text) > 0){
                    Decimal vKPIProm = Convert.ToDecimal(Math.Round(float.Parse(TxKPICumplimiento.Text) / float.Parse(TxKPITotal.Text)));
                    TxKPIPorcentaje.Text = vKPIProm.ToString();
                }else
                    TxKPIPorcentaje.Text = "100";
            }catch (Exception ex){
                throw new Exception(ex.Message);
            }

        }

        public void graficos(DataTable vData){
            try{
                int vTotTR = 0, vTotTT = 0, vTotRup = 0, vTotNoRup = 0, vSolTareas = 0, vSolRupturas = 0;
                int vHorasEmpleados = vData.Rows.Count * 360;
                for (int i = 0; i < vData.Rows.Count; i++){
                    vSolTareas += Convert.ToInt32(vData.Rows[i]["tareas"].ToString());
                    vSolRupturas += Convert.ToInt32(vData.Rows[i]["rupturas"].ToString());
                    vTotRup += Convert.ToInt32(vData.Rows[i]["eficienciaRup"].ToString());
                    vTotNoRup += Convert.ToInt32(vData.Rows[i]["eficienciaNoRup"].ToString());
                    vTotTR += Convert.ToInt32(vData.Rows[i]["tiempoReal"].ToString());
                    vTotTT += Convert.ToInt32(vData.Rows[i]["tiempoTransporte"].ToString());
                }

                if (vSolTareas > 0){
                    float vSolPromCR = float.Parse(vSolRupturas.ToString()) / float.Parse(vSolTareas.ToString()) * 100;
                    Decimal SolPromCR = Convert.ToDecimal(Math.Round(vSolPromCR, 2));
                    Decimal vSolPromSR = 100 - SolPromCR;
                    TxGraf1.Value = SolPromCR.ToString().Replace(",", ".");
                    TxGraf2.Value = vSolPromSR.ToString().Replace(",", ".");
                }

                int vTotPeticiones = vTotNoRup + vTotRup;
                if (vTotPeticiones > 0){
                    float vRupProm = float.Parse(vTotRup.ToString()) / float.Parse(vTotPeticiones.ToString()) * 100;
                    Decimal vRupPromTotal = Convert.ToDecimal(Math.Round(vRupProm, 2));
                    TxGraf3.Value = vRupPromTotal.ToString().Replace(",", ".");

                    float vNoRupProm = float.Parse(vTotNoRup.ToString()) / float.Parse(vTotPeticiones.ToString()) * 100;
                    Decimal vNoRupPromTotal = Convert.ToDecimal(Math.Round(vNoRupProm, 2));
                    TxGraf4.Value = vNoRupPromTotal.ToString().Replace(",", ".");
                }

                float vTR = float.Parse(vTotTR.ToString()) / float.Parse(vHorasEmpleados.ToString()) * 100;
                Decimal vTotalTR = Convert.ToDecimal(Math.Round(vTR, 2));
                TxGraf5.Value = vTotalTR.ToString().Replace(",", ".");

                float vTT = float.Parse(vTotTT.ToString()) / float.Parse(vHorasEmpleados.ToString()) * 100;
                Decimal vTotalTT = Convert.ToDecimal(Math.Round(vTT, 2));
                TxGraf6.Value = vTotalTT.ToString().Replace(",", ".");

                float vTNP = 100 - (vTR + vTT);
                Decimal vTotalTNP = Convert.ToDecimal(Math.Round(vTNP, 2));
                TxGraf7.Value = vTotalTNP.ToString().Replace(",", ".");

                DivGraficos.Visible = true;
                UPanelRendimientoGrafic.Update();
            }catch (Exception ex){
                throw new Exception(ex.Message);
            }
        }

        protected void BtnConfirmar_Click(object sender, EventArgs e){
            try{
                if (TxComentario.Text == string.Empty || TxComentario.Text == "" && DDLAccion.SelectedValue == "1")
                    throw new Exception("Favor ingrese el comentario de aprobacion.");

                String vAccion = DDLAccion.SelectedValue == "0" ? "2" : "3";
                String vMensaje = DDLAccion.SelectedValue == "0" ? "Aprobado" : "Rechazado";
                String vQuery = "[STEISP_CUMPLIMIENTO_Reportes] 2" +
                    "," + Session["CUMPL_ID_REPORTE"].ToString() +
                    "," + vAccion + "" +
                    ",null" +
                    ",'" + TxComentario.Text + "'" +
                    ",'" + Session["USUARIO"].ToString() + "'";
                int vInfo = vConexion.ejecutarSql(vQuery);
                if (vInfo == 1){
                    SmtpService vService = new SmtpService();
                    Boolean vFlag = false;

                    vQuery = "[STEISP_CUMPLIMIENTO_Reportes] 11," + Session["CUMPL_ID_REPORTE"].ToString();
                    DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                    String vMensajeCorreo = DDLAccion.SelectedValue == "0" ? "El reporte enviado el " + vDatos.Rows[0]["fechaRegistro"].ToString() + " ha sido <b>APROBADO</b>" : "El reporte enviado el " + vDatos.Rows[0]["fechaRegistro"].ToString() + " ha sido <b>RECHAZADO</b><br>Favor llenarlo nuevamente.";

                    vService.EnviarMensaje(
                        vDatos.Rows[0]["correo"].ToString(),
                        typeBody.Cumplimiento,
                        "Evaluación de Reporte de Metas de cumplimiento",
                        vDatos.Rows[0]["nombre"].ToString(),
                        vMensajeCorreo
                    );
                    vFlag = true;
                    if (vFlag)
                        Response.Redirect("metasPendientes.aspx", false);
                }else
                    Mensaje("Ha ocurrido un error. Comuníquese con sistemas", WarningType.Danger);

            }catch (Exception ex){
                LbMensaje.Text = ex.Message;
                DivMensaje.Visible = true;
            }
        }

        protected void BtnAprobar_Click(object sender, EventArgs e){
            try{
                DivMensaje.Visible = false;
                DDLAccion.SelectedValue = "0";
                TxComentario.Text = string.Empty;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);
            }catch (Exception ex){
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
    }
}