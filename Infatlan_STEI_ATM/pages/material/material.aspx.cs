using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using Infatlan_STEI_ATM.clases;

namespace Infatlan_STEI_ATM.pages.material
{
    public partial class material : System.Web.UI.Page
    {
        bd vConexion = new bd();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CARGAR_STOCK"] = null;
            string id = Request.QueryString["id"];
            string tipo = Request.QueryString["tipo"];
           
            if (!Page.IsPostBack)
            {
                cargarData();
                //LBMotivo.InnerText = "Motivo por el que solicita equipo";
                switch (tipo)
                {
                    case "2":
                        DDLStock.Enabled = false;
                        txtcantidad.Enabled = false;
                        btnVerifATM.Enabled = false;
                        GVNewMateriales.Enabled = false;
                        DIVbtnRechazo.Visible = true;
                        LBMotivo.InnerText = "Motivo de rechazo";
                        LBComentario.InnerText = "*Comentario: "+Convert.ToString(Session["ATM_COMENTARIO_MATERIAL"]);
                        //txtmotivo.Text= Convert.ToString(Session["ATM_COMENTARIOAPRO_MATERIAL"]);
                        txtmotivo.Text = "";
                        break;
                }
            }
        }

        void TransaccionInventario()
        {
            
                try
                {
                    DataTable vDatos = (DataTable)Session["ATM_MATERIALES_VERIF"];
                    for (int i = 0; i < vDatos.Rows.Count; i++)
                    {
                        string vMantenimiento = vDatos.Rows[i]["idMantenimiento"].ToString();
                        string vStock = vDatos.Rows[i]["idStock"].ToString();
                        int vCantidadSolicitada = Convert.ToInt32(vDatos.Rows[i]["cantidad"].ToString());
                        



                        String vQuery = "[STEISP_INVENTARIO_Stock] 2," + vStock;
                        DataTable vDataStock = vConexion.ObtenerTabla(vQuery);
                        Decimal vCantidad = Convert.ToDecimal(vDataStock.Rows[0]["cantidad"].ToString());
                        Decimal vCantidadActual = vCantidad - Convert.ToDecimal(vCantidadSolicitada);
                        String vNombreMaterial = vDataStock.Rows[0]["descripcion"].ToString();
                    if (vCantidadActual<0)
                        {
                        throw new Exception("No hay suficiente material de "+ vNombreMaterial+" en existencia");
                    }
                        Decimal vPrecioDec = Convert.ToDecimal(vCantidadSolicitada) * Convert.ToDecimal(vDataStock.Rows[0]["precioUnit"].ToString());
                        String vPrecio = vPrecioDec.ToString().Replace(",", ".");
                        String vCodigoUbi= Session["ATM_CODUBI_MATERIAL"].ToString();
                        String vUsuario= Session["usuATM"].ToString();
                    //Session["ATM_USUARIO_MATERIAL"].ToString()
                    generarxml vMaestro = new generarxml();
                        Object[] vDatosMaestro = new object[10];
                        vDatosMaestro[0] = vCodigoUbi;
                        vDatosMaestro[1] = vStock;
                        vDatosMaestro[2] = "1";
                        vDatosMaestro[3] = ""; //Responsable
                        vDatosMaestro[4] = "Mantenimiento ATM";
                        vDatosMaestro[5] = vCantidadSolicitada;
                        vDatosMaestro[6] = ""; // Serie
                        vDatosMaestro[7] = vPrecio;
                        vDatosMaestro[8] = vUsuario;
                        vDatosMaestro[9] = "6";
                        String vXML = vMaestro.ObtenerMaestroString(vDatosMaestro);
                        vXML = vXML.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");

                        vQuery = "[STEISP_INVENTARIO_Principal] 1" +
                            "," + vStock +
                            "," + vCantidadActual +
                            ",'" + vXML + "'";
                        Int32 vInfo = vConexion.ejecutarSQL(vQuery);
                    }
                    

                }
                catch (Exception)
                {
                throw new Exception();
                }
            
        }
        void cargarData()
        {
            txtNom.Text = Session["ATM_TECNICO_MATERIAL"].ToString();
            txtnombreATM.Text = Session["ATM_NOMATM_MATERIAL"].ToString();
            txtSucursal.Text = Session["ATM_SUCURSAL_MATERIAL"].ToString();
            txtmotivo.Text = Convert.ToString(Session["ATM_COMENTARIO_MATERIAL"]);           
            LBComentario.InnerText = "*Comentario: " + Convert.ToString(Session["ATM_COMENTARIOAPRO_MATERIAL"]);

            if (HttpContext.Current.Session["CARGAR_STOCK"] == null)
            {
                //DEVOLVER
                DataTable vDatos2 = new DataTable();
                vDatos2 = vConexion.ObtenerTabla("STEISP_ATM_VERIFICACION 12, '" + Session["ATM_IDMANT_MATERIAL"].ToString() + "'");
                GVNewMateriales.DataSource = vDatos2;
                GVNewMateriales.DataBind();
                Session["ATM_MATERIALES_VERIF"] = vDatos2;

                //STOCK
                String vQuery = "STEISP_ATM_Generales 28";
                DataTable vDatos = vConexion.ObtenerTabla(vQuery);
                DDLStock.Items.Add(new ListItem { Value = "0", Text = "Seleccione material..." });
                foreach (DataRow item in vDatos.Rows)
                {
                    DDLStock.Items.Add(new ListItem { Value = item["IDStock"].ToString(), Text = item["Descripcion"].ToString() });

                }
                Session["CARGAR_STOCK"] = "1";
            }
        }
        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }
        protected void txtcantidad_TextChanged(object sender, EventArgs e)
        {
            if(Convert.ToInt32(txtcantidad.Text)> Convert.ToInt32(Session["STOCK_CANTIDAD_ATM"]))
            {
                txtcantidad.Text = string.Empty;
                Mensaje("Cantidad excede a equipo en existencia", WarningType.Warning);
            }
        }

        protected void DDLStock_TextChanged(object sender, EventArgs e)
        {
            //STOCK
            String vQuery = "STEISP_ATM_Generales 29, '" + DDLStock.SelectedValue + "'";
            DataTable vDatos = vConexion.ObtenerTabla(vQuery);
            foreach (DataRow item in vDatos.Rows)
            {
                txtmarca.Text= item["Marca"].ToString();
                Session["STOCK_CANTIDAD_ATM"]= item["Cantidad"].ToString();
            }
        }

        protected void btnModalEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                string id = Request.QueryString["id"];
                string tipo = Request.QueryString["tipo"];
                if (tipo == "2")
                {
                    TransaccionInventario();
                    string vQuery2 = "STEISP_ATM_VerificacionTotal 7, '" + Session["ATM_IDMANT_MATERIAL"].ToString() + "','" + Session["usuATM"].ToString() + "','" + txtmotivo.Text + "'";
                    vConexion.ejecutarSQL(vQuery2);
                }
                else
                {

                    string vQuery3 = "STEISP_ATM_VerificacionTotal 6, '" + Session["ATM_IDMANT_MATERIAL"].ToString() + "'";
                    vConexion.ejecutarSQL(vQuery3);
                    DataTable vDatos = (DataTable)Session["ATM_MATERIALES_VERIF"];
                    for (int i = 0; i < vDatos.Rows.Count; i++)
                    {
                        string vMantenimiento = vDatos.Rows[i]["idMantenimiento"].ToString();
                        string vStock = vDatos.Rows[i]["idStock"].ToString();
                        int vCantidad = Convert.ToInt32(vDatos.Rows[i]["cantidad"].ToString());
                        string vQuery = "STEISP_ATM_VerificacionTotal 3, '" + vMantenimiento + "','" + vStock + "', '" + vCantidad + "'";
                        vConexion.ejecutarSQL(vQuery);

                    }

                    string vQuery2 = "STEISP_ATM_VerificacionTotal 4, '" + Session["ATM_IDMANT_MATERIAL"].ToString() + "','" + Session["usuATM"].ToString() + "','" + txtmotivo.Text + "'";
                    vConexion.ejecutarSQL(vQuery2);
                }
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
                Mensaje("Materiales solicitados con éxito", WarningType.Success);
                txtmotivo.Text = string.Empty;
                Session["ATM_MATERIALES_VERIF"] = null;
                GVNewMateriales.DataSource = null;
                GVNewMateriales.DataBind();
                UPMateriales.Update();
                DDLStock.SelectedIndex = -1;
                txtcantidad.Text = "";
                txtmarca.Text = "";
                if(tipo=="2")
                Response.Redirect("buscarAprobar.aspx");
                else
                Response.Redirect("buscarMaterial.aspx");
            }
            catch (Exception Ex)
            {
                Mensaje(Ex.Message, WarningType.Danger); 
            }
        }

        protected void btnModalCerrar_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal();", true);
        }

        protected void btnguardar_Click(object sender, EventArgs e)
        {
            if(Session["ATM_MATERIALES_VERIF"]!=null || txtmotivo.Text!="")
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);           
            else
                Mensaje("Llene la lista de materiales a solicitar", WarningType.Warning);
        }

        protected void GVNewMateriales_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DataTable vDatos = (DataTable)Session["ATM_MATERIALES_VERIF"];
            if (e.CommandName == "eliminar")
            {
                String vID = e.CommandArgument.ToString();
                if (Session["ATM_MATERIALES_VERIF"] != null)
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
            Session["ATM_MATERIALES_VERIF"] = vDatos;
        }

        protected void btnVerifATM_Click(object sender, EventArgs e)
        {
            if (txtmarca.Text == "" || txtmarca.Text == string.Empty || txtcantidad.Text == "" || txtcantidad.Text == string.Empty)
                Mensaje("Seleccione materiales y su respectiva cantidad a solicitar.", WarningType.Warning);
            else if(txtcantidad.Text=="0")
                Mensaje("Ingrese una cantidad mayor o igual a uno.", WarningType.Warning);
            else if (Session["STOCK_CANTIDAD_ATM"].ToString()=="0")
                Mensaje("No hay material en existencia.", WarningType.Warning);
            else
            {
                try
                {
                    //String vMensaje = "";

                    DataTable vData = new DataTable();
                    DataTable vDatos = (DataTable)Session["ATM_MATERIALES_VERIF"];
                    int vMaterial = Convert.ToInt32(DDLStock.SelectedValue);
                    string vNombreMaterial = DDLStock.SelectedItem.Text;
                    string vMarca = txtmarca.Text;
                    string vCantidad = txtcantidad.Text;

                    vData.Columns.Add("idMantenimiento");
                    vData.Columns.Add("idStock");
                    vData.Columns.Add("nombre");
                    vData.Columns.Add("marca");
                    vData.Columns.Add("cantidad");

                    if (vDatos == null)
                        vDatos = vData.Clone();

                    if (vDatos != null)
                    {
                        if (vDatos.Rows.Count < 1)
                            vDatos.Rows.Add(Session["ATM_IDMANT_MATERIAL"].ToString(), Convert.ToInt32(DDLStock.SelectedValue), DDLStock.SelectedItem.Text, txtmarca.Text, txtcantidad.Text);
                        else
                        {
                            string vTotalCantidad = Session["STOCK_CANTIDAD_ATM"].ToString();
                            Boolean vRegistered = false;
                            for (int i = 0; i < vDatos.Rows.Count; i++)
                            {
                                if (vNombreMaterial == vDatos.Rows[i]["nombre"].ToString())
                                {
                                    vDatos.Rows[i]["cantidad"] = Convert.ToDecimal(vDatos.Rows[i]["cantidad"].ToString()) + Convert.ToDecimal(txtcantidad.Text);
                                    vRegistered = true;
                                    int vCantidadAcumulada = Convert.ToInt32(vDatos.Rows[i]["cantidad"].ToString());
                                    if (vCantidadAcumulada>Convert.ToInt32(vTotalCantidad))
                                    {
                                        Mensaje("Cantidad excede a equipo en existencia", WarningType.Warning);
                                        vDatos.Rows[i]["cantidad"] = Convert.ToDecimal(vDatos.Rows[i]["cantidad"].ToString()) - Convert.ToDecimal(txtcantidad.Text);
                                    }
                                    
                                }
                            }

                            if (!vRegistered)
                                vDatos.Rows.Add(Session["ATM_IDMANT_MATERIAL"].ToString(), Convert.ToInt32(DDLStock.SelectedValue), DDLStock.SelectedItem.Text, txtmarca.Text, Convert.ToInt32(txtcantidad.Text));
                        }
                    }

                    GVNewMateriales.DataSource = vDatos;
                    GVNewMateriales.DataBind();
                    Session["ATM_MATERIALES_VERIF"] = vDatos;
                    UPMateriales.Update();

                    DDLStock.SelectedIndex = -1;
                    txtcantidad.Text = "";
                    txtmarca.Text = "";


                }
                catch (Exception Ex)
                {
                    Mensaje(Ex.Message, WarningType.Success);
                }
            }
        }

        protected void btnRechazar_Click(object sender, EventArgs e)
        {
            if(txtmotivo.Text=="" || txtmotivo.Text==string.Empty)
                Mensaje("Escriba el motivo por el que devuelve solicitud.", WarningType.Warning);
            else
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal2();", true);
        }

        protected void btnModarDevolver_Click(object sender, EventArgs e)
        {
            string vQuery2 = "STEISP_ATM_VerificacionTotal 5, '" + Session["ATM_IDMANT_MATERIAL"].ToString() + "','" + Session["usuATM"].ToString() + "','" + txtmotivo.Text + "'";
            vConexion.ejecutarSQL(vQuery2);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal2();", true);
            Response.Redirect("buscarAprobar.aspx");
        }

        protected void btnModalCerrarRechazo_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal2();", true);
        }
    }
}