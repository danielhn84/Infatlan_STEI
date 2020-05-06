using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using Infatlan_STEI_Agencias.classes;
using System.Configuration;

namespace Infatlan_STEI_Agencias.pages.mantenimiento
{
    public partial class solicitudMateriales : System.Web.UI.Page
    {
        db vConexion = new db();
        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["USUARIO"] = "acamador";
            DDLArticulo.CssClass = "select2 form-control custom-select";
            if (!Page.IsPostBack)
            {              
                cargarDatos();
            }
        }

        private void cargarDatos()
        {
            try
            {

                String vQuery = "STEISP_AGENCIA_Materiales 1";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);

                GVBusqueda.DataSource = vDatos;
                GVBusqueda.DataBind();
                //Session["AG_CN_MANTENIMIENTOS_PENDIENTES_APROBAR"] = vDatos;


                vQuery = "STEISP_AGENCIA_Materiales 3";
                vDatos = vConexion.obtenerDataTable(vQuery);

                if (vDatos.Rows.Count > 0)
                {
                    DDLArticulo.Items.Clear();
                    DDLArticulo.Items.Add(new ListItem { Value = "0", Text = "Seleccione" });
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DDLArticulo.Items.Add(new ListItem { Value = item["idStock"].ToString(), Text = item["TipoStock"].ToString() + "  " + item["modelo"].ToString()  +" - " + item["marca"].ToString() + " (" + item["cantidad"].ToString() + ")" });
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
        
        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string vIdMantenimiento = e.CommandArgument.ToString();
            Session["AG_SM_ID_MANTENIMIENTO"] = vIdMantenimiento;

            if (e.CommandName == "Aprobar")
            {
                String vQuery = "STEISP_AGENCIA_Materiales 2," + vIdMantenimiento;
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
     
                TxIdMant.Text = vDatos.Rows[0]["id_Mantenimiento"].ToString(); 
                TxAgencia.Text = vDatos.Rows[0]["Lugar"].ToString();
                TxFecha.Text = vDatos.Rows[0]["fecha"].ToString();
                TxArea.Text = vDatos.Rows[0]["Area"].ToString();
                TxUbicacion.Text = vDatos.Rows[0]["codigoUbicacion"].ToString();
                Int32 RbConductorConverido = Convert.ToInt32(vDatos.Rows[0]["requiereConductor"]);
                RbConductor.SelectedValue = RbConductorConverido.ToString();
                lbTitulo.Text = "Solicitud de Materiales " + TxAgencia.Text;
                UpdatePanel1.Update();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModalMaterial();", true);
             }
        }
        
        protected void btnAgregar_Click(object sender, EventArgs e)
        {          
            try
            {
                validaciones();
                if (Convert.ToInt32(TxCantidad.Text) > Convert.ToInt32(Session["AG_SM_CANTIDAD_MATERIALES"]))
                {
                    TxCantidad.Text = string.Empty;
                    lbCantidad.Text = "La cantidad solicitada: "+ TxCantidad.Text + " excede a la cantidad en existencia, favor verificar la cantidad a solicitar";
                    DivAlertaCantidad.Visible = true;
                    UpCantidadMaxima.Update();
                }
                else
                {
                    DataTable vData = new DataTable();
                    DataTable vDatos = (DataTable)Session["AG_SM_MATERIALES"];
                    //string vNombreMaterial = DDLArticulo.SelectedItem.Text;
                    Char delimiter = '(';
                    string[] vNombreMaterial = DDLArticulo.SelectedItem.Text.Split(delimiter);

                    string vNombreMaterialMatriz = vNombreMaterial[0];

                    vData.Columns.Add("idStock");
                    vData.Columns.Add("nombre");
                    vData.Columns.Add("cantidad");


                    if (vDatos == null)
                        vDatos = vData.Clone();

                    if (vDatos != null)
                    {
                        if (vDatos.Rows.Count < 1)
                            vDatos.Rows.Add(DDLArticulo.SelectedValue, vNombreMaterialMatriz, TxCantidad.Text);
                        else
                        {
                            Boolean vRegistered = false;
                            for (int i = 0; i < vDatos.Rows.Count; i++)
                            {
                                if (vNombreMaterialMatriz == vDatos.Rows[i]["nombre"].ToString())
                                {
                                    //vDatos.Rows[i]["cantidad"] = Convert.ToDecimal(vDatos.Rows[i]["cantidad"].ToString()) + Convert.ToDecimal(TxCantidad.Text);

                                    lbCantidad.Text = "El material seleccionado: " + vNombreMaterialMatriz + " ya esta agregado en la lista, favor verificar";
                                    DivAlertaCantidad.Visible = true;
                                    UpCantidadMaxima.Update();

                                    vRegistered = true;
                                }
                            }

                            if (!vRegistered)
                                vDatos.Rows.Add(DDLArticulo.SelectedValue, vNombreMaterialMatriz, TxCantidad.Text);
                        }
                    }

                    GVNewMateriales.DataSource = vDatos;
                    GVNewMateriales.DataBind();
                    Session["AG_SM_MATERIALES"] = vDatos;
                    UPMateriales.Update();

                    DDLArticulo.SelectedIndex = -1;
                    TxCantidad.Text = "";
                }               

            }
            catch (Exception ex)
            {
                lbCantidad.Text = ex.Message;
                DivAlertaCantidad.Visible = true;
                UpCantidadMaxima.Update();

            }  
        }

        protected void TxCantidad_TextChanged(object sender, EventArgs e)
        {
            DivAlertaCantidad.Visible = false;
            UpCantidadMaxima.Update();
        }

        protected void DDLArticulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //STOCK
            String vQuery = "STEISP_AGENCIA_Materiales 4, '" + DDLArticulo.SelectedValue + "'";
            DataTable vDatos = vConexion.obtenerDataTable(vQuery);
            int cant=Convert.ToInt32(vDatos.Rows[0]["cantidad"].ToString());
            Session["AG_SM_CANTIDAD_MATERIALES"] = cant;

            DivAlertaCantidad.Visible = false;
            UpCantidadMaxima.Update();
        }

        protected void GVNewMateriales_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DataTable vDatos = (DataTable)Session["AG_SM_MATERIALES"];
            if (e.CommandName == "eliminar")
            {
                String vID = e.CommandArgument.ToString();
                if (Session["AG_SM_MATERIALES"] != null)
                {

                    DataRow[] result = vDatos.Select("idStock = '" + vID + "'");
                    foreach (DataRow row in result)
                    {
                        if (row["idStock"].ToString().Contains(vID))
                            vDatos.Rows.Remove(row);
                    }
                }
            }
            GVNewMateriales.DataSource = vDatos;
            GVNewMateriales.DataBind();
            Session["AG_SM_MATERIALES"] = vDatos;
            DivAlertaCantidad.Visible = false;
            UpCantidadMaxima.Update();
        }
        
        private void validaciones()
        {
            if (DDLArticulo.SelectedValue.Equals("0"))
                throw new Exception("Falta completar datos, Favor seleccionar un tipo de material a solicitar. ");

            if (TxCantidad.Text.Equals(""))
                throw new Exception("Falta completar datos, Favor ingrese la cantidad de material a solicitar. ");
        }
        
        protected void btnModalEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                validarEnvio();
                DataTable vDatosMaterialesSolicitar = (DataTable)Session["AG_SM_MATERIALES"];
                string vIdMantenimiento = TxIdMant.Text;
                if (vDatosMaterialesSolicitar.Rows.Count > 0)
                {
                    for (int num = 0; num < vDatosMaterialesSolicitar.Rows.Count; num++)
                    {
                        
                        string vIdStock = vDatosMaterialesSolicitar.Rows[num]["idStock"].ToString();
                        string vCantidad = vDatosMaterialesSolicitar.Rows[num]["cantidad"].ToString();

                        String vQuery4 = "STEISP_AGENCIA_Materiales 5,'" + vIdMantenimiento + 
                            "','" + vIdStock + 
                            "','"+ vCantidad+
                            "','"+ Session["USUARIO"]+ "'";
                        Int32 vInfo4 = vConexion.ejecutarSql(vQuery4);

                    }
                }

                String vQuery = "STEISP_AGENCIA_Materiales 6," + vIdMantenimiento;
                Int32 vInfo5 = vConexion.ejecutarSql(vQuery);

                if (vInfo5 == 1)
                {
                    LimpiarModalMaterial();
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModalMaterial();", true);
                    Mensaje("Solicitud de materiales con éxito.", WarningType.Success);
                    cargarDatos();
                }

            }
            catch (Exception ex)
            {
                lbCantidad.Text = ex.Message;
                DivAlertaCantidad.Visible = true;
                UpCantidadMaxima.Update();
            }
        }

        private void LimpiarModalMaterial()
        {
            DDLArticulo.SelectedIndex = -1;
            TxCantidad.Text = String.Empty;

            GVNewMateriales.DataSource = null;
            GVNewMateriales.DataBind();
            UPMateriales.Update();
        }

        private void validarEnvio()
        {
            if (Session["AG_SM_MATERIALES"] == null)
                throw new Exception("Lista de materiales vacia, favor seleccionar material a solicitar. ");
        }
    }
}