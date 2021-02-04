using Infatlan_STEI_ATM.clases;
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
            LitRuta.Text = id != null ? "Aprobar" : "Solicitar";
            if (!Page.IsPostBack)
            {
                if (Convert.ToBoolean(Session["AUTH"]))
                {
                    cargarData();
                    RBConductor.SelectedValue = "1";
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
                            LBComentario.InnerText = "*Comentario: " + Convert.ToString(Session["ATM_COMENTARIO_MATERIAL"]);
                            //txtmotivo.Text= Convert.ToString(Session["ATM_COMENTARIOAPRO_MATERIAL"]);
                            txtmotivo.Text = "";
                            RBConductor.Enabled = false;
                            DDLConductor.Enabled = false;
                            DDLConductor.SelectedIndex = CargarInformacionDDL(DDLConductor, Session["ATM_IDCHOFER_MATERIAL"].ToString());
                            DIVTablaMateriales.Visible = false;
                            break;
                    }
                }
                else
                {
                    Response.Redirect("/login.aspx");
                }
            }
        }

        int CargarInformacionDDL(DropDownList vList, String vValue)
        {
            int vIndex = 0;
            try
            {
                int vContador = 0;
                foreach (ListItem item in vList.Items)
                {
                    if (item.Value.Equals(vValue))
                    {
                        vIndex = vContador;
                    }
                    vContador++;
                }
            }
            catch { throw; }
            return vIndex;
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
                    //int vUbi = Convert.ToInt32(vDatos.Rows[i]["idUbi"].ToString());




                    String vQuery = "[STEISP_INVENTARIO_Stock] 2," + vStock;
                    DataTable vDataStock = vConexion.ObtenerTabla(vQuery);
                    Decimal vCantidad = Convert.ToDecimal(vDataStock.Rows[0]["cantidad"].ToString());
                    Decimal vCantidadActual = vCantidad - Convert.ToDecimal(vCantidadSolicitada);
                    String vNombreMaterial = vDataStock.Rows[0]["descripcion"].ToString();
                    if (vCantidadActual < 0)
                    {
                        throw new Exception("No hay suficiente material de " + vNombreMaterial + " en existencia");
                    }
                    Decimal vPrecioDec = Convert.ToDecimal(vCantidadSolicitada) * Convert.ToDecimal(vDataStock.Rows[0]["precioUnit"].ToString());
                    String vPrecio = vPrecioDec.ToString().Replace(",", ".");
                    String vCodigoUbi = Session["ATM_CODUBI_MATERIAL"].ToString();
                    String vUsuario = Session["USUARIO"].ToString();
                    String vUsuResponsable = Session["ATM_USUARIO_MATERIAL"].ToString();
                    //Session["ATM_USUARIO_MATERIAL"].ToString()
                    generarxml vMaestro = new generarxml();
                    Object[] vDatosMaestro = new object[10];
                    vDatosMaestro[0] = vCodigoUbi;
                    vDatosMaestro[1] = vStock;
                    vDatosMaestro[2] = Session["ATM_INVUBI_MATERIAL"];
                    vDatosMaestro[3] = vUsuResponsable; //Responsable
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

        void TransaccionInventario2()
        {
            //try
            //{
            //    //validar la cantidad
            //    if (Convert.ToDecimal(TxCantidadActual.Text) < Convert.ToDecimal(TxCantidad.Text))
            //        throw new Exception("La cantidad solicitada es mayor que la disponible.");

            //    String vPrecio = "", vTipoTransaccion = "", vQuery = "";

            //    if (TxProceso.Text == "STOCK")
            //    {
            //        vQuery = "[STEISP_INVENTARIO_Stock] 2," + TxIdStock.Text;
            //        DataTable vDataStock = vConexion.obtenerDataTable(vQuery);
            //        Decimal vPrecioDec = Convert.ToDecimal(TxCantidad.Text) * Convert.ToDecimal(vDataStock.Rows[0]["precioUnit"].ToString());
            //        vPrecio = vPrecioDec.ToString().Replace(",", ".");
            //        vTipoTransaccion = "14";
            //    }
            //    else if (TxProceso.Text == "EDC")
            //        vTipoTransaccion = "18";
            //    else if (TxProceso.Text == "Enlace")
            //        vTipoTransaccion = "20";

            //    generarxml vMaestro = new generarxml();
            //    Object[] vDatosMaestro = new object[10];
            //    vDatosMaestro[0] = TxCodigo.Text;
            //    vDatosMaestro[1] = TxIdStock.Text;
            //    vDatosMaestro[2] = DDLNueva.SelectedValue; // NUEVA
            //    vDatosMaestro[3] = Session["USUARIO"].ToString(); //Responsable
            //    vDatosMaestro[4] = "CAMBIO UBICACION";
            //    vDatosMaestro[5] = TxCantidad.Text;
            //    vDatosMaestro[6] = ""; // Serie
            //    vDatosMaestro[7] = vPrecio;
            //    vDatosMaestro[8] = Session["USUARIO"].ToString();
            //    vDatosMaestro[9] = vTipoTransaccion;

            //    String vXML = vMaestro.ObtenerMaestroString(vDatosMaestro);
            //    vXML = vXML.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");

            //    if (Convert.ToDecimal(TxCantidadActual.Text) == Convert.ToDecimal(TxCantidad.Text))
            //    {
            //        vQuery = "[STEISP_INVENTARIO_Principal] 3" +
            //        "," + TxIdInventario.Text +
            //        "," + TxIdUbicacion.Text +  //UBICACION ANTERIOR
            //        ",'" + vXML + "'";

            //        Int32 vInfo = vConexion.ejecutarSql(vQuery);
            //        if (vInfo == 2)
            //        {
            //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "cerrarModal();", true);
            //            Mensaje("Cambio realizado con éxito.", WarningType.Success);
            //            cargarDatos(TxIdUbicacion.Text);
            //        }
            //    }
            //    else if (Convert.ToDecimal(TxCantidadActual.Text) > Convert.ToDecimal(TxCantidad.Text))
            //    {
            //        vQuery = "[STEISP_INVENTARIO_Principal] 6" +
            //        "," + TxIdInventario.Text +
            //        "," + TxIdUbicacion.Text +  //UBICACION ANTERIOR
            //        ",'" + vXML + "'";

            //        Int32 vInfo = vConexion.ejecutarSql(vQuery);
            //        if (vInfo == 4 || vInfo == 5)
            //        {
            //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "cerrarModal();", true);
            //            Mensaje("Cambio realizado con éxito.", WarningType.Success);
            //            cargarDatos(TxIdUbicacion.Text);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    DivMensaje.Visible = true;
            //    LbAdvertencia.Text = ex.Message;
            //}
        }

        void cargarData()
        {
            txtNom.Text = Session["ATM_TECNICO_MATERIAL"].ToString();
            txtnombreATM.Text = Session["ATM_NOMATM_MATERIAL"].ToString();
            txtSucursal.Text = Session["ATM_SUCURSAL_MATERIAL"].ToString();
            txtmotivo.Text = Convert.ToString(Session["ATM_COMENTARIO_MATERIAL"]);
            LBComentario.InnerText = "*Comentario: " + Convert.ToString(Session["ATM_COMENTARIOAPRO_MATERIAL"]);


            //if (Convert.ToString(Session["ATM_CHOFER_MATERIAL"])=="1")
            //{
            //    RBConductor.SelectedValue = "1";
            //}
            //else
            //{
            //    RBConductor.SelectedValue = "0";
            //}
            RBConductor.SelectedValue = Convert.ToString(Session["ATM_CHOFER_MATERIAL"]);
            UPtotalATM.Update();
            //DEVOLVER
            DataTable vDatos2 = new DataTable();
            vDatos2 = vConexion.ObtenerTabla("STEISP_ATM_VERIFICACION 12, '" + Session["ATM_IDMANT_MATERIAL"].ToString() + "'");
            GVNewMateriales.DataSource = vDatos2;
            GVNewMateriales.DataBind();
            Session["ATM_MATERIALES_VERIF"] = vDatos2;

            if (HttpContext.Current.Session["CARGAR_STOCK"] == null)
            {
                //CONDUCTORES
                String vQuery3 = "STEISP_ATM_Generales 30";
                DataTable vDatos3 = vConexion.ObtenerTabla(vQuery3);
                DDLConductor.Items.Add(new ListItem { Value = "0", Text = "Seleccione conductor..." });
                foreach (DataRow item in vDatos3.Rows)
                {
                    DDLConductor.Items.Add(new ListItem { Value = item["idConductor"].ToString(), Text = item["nombre"].ToString() });

                }

                //STOCK
                String vQuery = "STEISP_ATM_Generales 28,'" + Session["ATM_MINISTOCK_MATERIAL"].ToString() + "'";
                DataTable vDatos = vConexion.ObtenerTabla(vQuery);
                DDLStock.Items.Add(new ListItem { Value = "0", Text = "Seleccione material..." });
                foreach (DataRow item in vDatos.Rows)
                {
                    DDLStock.Items.Add(new ListItem { Value = item["IDStock"].ToString(), Text = item["Descripcion"].ToString() + "(" + item["Cantidad"].ToString() + ")" });

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
            //if(Convert.ToInt32(txtcantidad.Text)> Convert.ToInt32(Session["STOCK_CANTIDAD_ATM"]))
            //{
            //    txtcantidad.Text = string.Empty;
            //    Mensaje("Cantidad excede a equipo en existencia", WarningType.Warning);
            //}
        }

        protected void DDLStock_TextChanged(object sender, EventArgs e)
        {
            //STOCK
            String vQuery = "STEISP_ATM_Generales 29, '" + DDLStock.SelectedValue + "'";
            DataTable vDatos = vConexion.ObtenerTabla(vQuery);
            foreach (DataRow item in vDatos.Rows)
            {
                txtmarca.Text = item["Marca"].ToString();
                Session["STOCK_CANTIDAD_ATM"] = item["Cantidad"].ToString();
            }
        }

        void CorreoSuscripcion()
        {
            string vEstado = "";
            DataTable vDatos = new DataTable();
            String vQuery = "STEISP_ATM_Generales 36,'" + Session["ATM_CODMANTENIMIENTO_MATERIAL"] + "'";
            vDatos = vConexion.ObtenerTabla(vQuery);
            foreach (DataRow item in vDatos.Rows)
            {
                vEstado = item["estadoMantenimiento"].ToString();
            }

            if (vEstado == "3")
            {
                string vReporteViaticos = "Notificacion";
                string vCorreoAdmin = "acedillo@bancatlan.hn";
                string vCorreoCopia = "acamador@bancatlan.hn";
                string vAsuntoRV = "Formato de notificación";
                string vBody = "Formato de notificación";
                int vEstadoSuscripcion = 0;
                string vQueryRep = "STEISP_ATM_Generales 35, '" + vReporteViaticos + "','" + vCorreoAdmin + "','" + vCorreoCopia + "','" + vAsuntoRV + "','" + vBody + "','" + vEstadoSuscripcion + "','" + Session["ATM_CODMANTENIMIENTO_MATERIAL"] + "'";
                vConexion.ejecutarSQL(vQueryRep);
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
                    //TransaccionInventario();
                    string vQuery2 = "STEISP_ATM_VerificacionTotal 7, '" + Session["ATM_IDMANT_MATERIAL"].ToString() + "','" + Session["USUARIO"].ToString() + "','" + txtmotivo.Text + "'";
                    vConexion.ejecutarSQL(vQuery2);
                    CorreoSuscripcion();
                    //string vQuery3 = "STEISP_ATM_VerificacionTotal 6, '" + Session["ATM_IDMANT_MATERIAL"].ToString() + "'";
                    //vConexion.ejecutarSQL(vQuery3);
                }
                else
                {


                    DataTable vDatos = (DataTable)Session["ATM_MATERIALES_VERIF"];
                    for (int i = 0; i < vDatos.Rows.Count; i++)
                    {
                        string vMantenimiento = vDatos.Rows[i]["idMantenimiento"].ToString();
                        string vStock = vDatos.Rows[i]["idStock"].ToString();
                        int vCantidad = Convert.ToInt32(vDatos.Rows[i]["cantidad"].ToString());
                        //int vUbi = Convert.ToInt32(vDatos.Rows[i]["IDUbi"].ToString());

                        String vExiste = "";
                        String vQueryM = "STEISP_ATM_VerificacionTotal 9, '" + vMantenimiento + "','" + vStock + "'";
                        DataTable vDatosM = vConexion.ObtenerTabla(vQueryM);
                        foreach (DataRow item in vDatosM.Rows)
                        {
                            vExiste = item["idMateriales"].ToString();
                        }
                        if (vExiste == "")
                        {
                            string vQuery = "STEISP_ATM_VerificacionTotal 3, '" + vMantenimiento + "','" + vStock + "', '" + vCantidad + "'";
                            vConexion.ejecutarSQL(vQuery);
                        }
                        else
                        {
                            string vQuery3 = "STEISP_ATM_VerificacionTotal 6, '" + vMantenimiento + "','" + vStock + "', '" + vCantidad + "'";
                            vConexion.ejecutarSQL(vQuery3);
                        }

                    }

                    string vQuery2 = "STEISP_ATM_VerificacionTotal 4, '" + Session["ATM_IDMANT_MATERIAL"].ToString() + "','" + Session["USUARIO"].ToString() + "','" + txtmotivo.Text + "', '" + RBConductor.SelectedValue + "','" + DDLConductor.SelectedValue + "'";
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
                if (tipo == "2")
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
            if (Session["ATM_MATERIALES_VERIF"] != null || txtmotivo.Text != "")
            {
                //if(RBConductor.SelectedValue=="1" && DDLConductor.SelectedValue=="0")
                //    Mensaje("Seleccione conductor", WarningType.Warning);
                //else
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);
            }
            else
                Mensaje("No deje campos vaciosr", WarningType.Warning);
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
                        {
                            //LLENAR TABLA DE DATOS A ELIMINAR

                            //String vCodEventoConfirmar = "";
                            //String vQueryCE = "RIESGO_Generales 104,'" + vCodEvento + "'";
                            //DataTable vDatosCE = vConexion.ObtenerTabla(vQueryCE);
                            //foreach (DataRow item in vDatosCE.Rows)
                            //{
                            //    vCodEventoConfirmar = item["codEvento"].ToString();
                            //}
                            //if (vCodEventoConfirmar != "")
                            //{
                            //    try
                            //    {
                            //        String vQueryAC = "RIESGO_Generales 73,'" + vCodEvento + "'";
                            //        DataTable vDatosAC = vConexion.ObtenerTabla(vQueryAC);
                            //        foreach (DataRow item in vDatosAC.Rows)
                            //        {
                            //            Session["IDTVulnerable"] = item["IDTipoV"].ToString();
                            //        }

                            //        DataTable vData2 = new DataTable();
                            //        DataTable vDatos2 = (DataTable)Session["RIESGOSELIMINAR"];

                            //        int vTipoVulE = Convert.ToInt32(Session["IDTVulnerable"]);
                            //        vData2.Columns.Add("CodEvento");
                            //        vData2.Columns.Add("IDTipoVulnerable");

                            //        if (vDatos2 == null)
                            //            vDatos2 = vData2.Clone();

                            //        if (vDatos2 != null)
                            //        {
                            //            if (vDatos2.Rows.Count < 1)
                            //            {
                            //                vDatos2.Rows.Add(vCodEvento, vTipoVulE);
                            //            }
                            //            else
                            //            {
                            //                //string vTotalCantidad = Session["STOCK_CANTIDAD_ATM"].ToString();
                            //                Boolean vRegistered = false;
                            //                //for (int i = 0; i < vDatos2.Rows.Count; i++)
                            //                //{
                            //                //    int vExistente = Convert.ToInt32(vDatos2.Rows[i]["IDTipoVulnerable"].ToString());
                            //                //    if (vTipoVulE == vExistente)
                            //                //    {
                            //                //        vRegistered = true;
                            //                //        throw new Exception("Ya ingresó esta vulnerabilidad");
                            //                //    }

                            //                //}

                            //                if (!vRegistered)
                            //                    vDatos2.Rows.Add(vCodEvento, vTipoVulE);

                            //            }
                            //            GVEliminar.DataSource = vDatos2;
                            //            GVEliminar.DataBind();
                            //            Session["RIESGOSELIMINAR"] = vDatos2;

                            //        }
                            //    }
                            //    catch (Exception Ex)
                            //    {
                            //        Mensaje(Ex.Message, WarningType.Danger);
                            //    }
                            //}

                            //LLENAR TABLA DE DATOS A ELIMINAR
                            vDatos.Rows.Remove(row);
                        }
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
            else if (txtcantidad.Text == "0")
                Mensaje("Ingrese una cantidad mayor o igual a uno.", WarningType.Warning);
            //else if (Session["STOCK_CANTIDAD_ATM"].ToString()=="0")
            //    Mensaje("No hay material en existencia.", WarningType.Warning);
            else
            {
                try
                {
                    //String vMensaje = "";
                    Char delimiter = '(';
                    DataTable vData = new DataTable();
                    DataTable vDatos = (DataTable)Session["ATM_MATERIALES_VERIF"];
                    int vMaterial = Convert.ToInt32(DDLStock.SelectedValue);
                    //string vNombreMaterial = DDLStock.SelectedItem.Text.Split(delimiter);
                    string[] vNombreMaterialMatriz = DDLStock.SelectedItem.Text.Split(delimiter);
                    String vNombreMaterial = vNombreMaterialMatriz[0];
                    string vMarca = txtmarca.Text;
                    string vCantidad = txtcantidad.Text;
                    //int vUbic = Convert.ToInt32(Session["ATM_INVUBI_MATERIAL"]);

                    vData.Columns.Add("idMantenimiento");
                    vData.Columns.Add("idStock");
                    vData.Columns.Add("nombre");
                    vData.Columns.Add("marca");
                    vData.Columns.Add("cantidad");
                    //vData.Columns.Add("IDUbi");

                    if (vDatos == null)
                        vDatos = vData.Clone();

                    if (vDatos != null)
                    {
                        if (vDatos.Rows.Count < 1)
                            vDatos.Rows.Add(Session["ATM_IDMANT_MATERIAL"].ToString(), Convert.ToInt32(DDLStock.SelectedValue), vNombreMaterial, txtmarca.Text, txtcantidad.Text);
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
                                    if (vCantidadAcumulada > Convert.ToInt32(vTotalCantidad))
                                    {
                                        Mensaje("Cantidad excede a equipo en existencia", WarningType.Warning);
                                        vDatos.Rows[i]["cantidad"] = Convert.ToDecimal(vDatos.Rows[i]["cantidad"].ToString()) - Convert.ToDecimal(txtcantidad.Text);
                                    }

                                }
                            }

                            if (!vRegistered)
                                vDatos.Rows.Add(Session["ATM_IDMANT_MATERIAL"].ToString(), Convert.ToInt32(DDLStock.SelectedValue), vNombreMaterial, txtmarca.Text, Convert.ToInt32(txtcantidad.Text));
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
            if (txtmotivo.Text == "" || txtmotivo.Text == string.Empty)
                Mensaje("Escriba el motivo por el que devuelve solicitud.", WarningType.Warning);
            else
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal2();", true);
        }

        protected void btnModarDevolver_Click(object sender, EventArgs e)
        {
            string vQuery2 = "STEISP_ATM_VerificacionTotal 5, '" + Session["ATM_IDMANT_MATERIAL"].ToString() + "','" + Session["USUARIO"].ToString() + "','" + txtmotivo.Text + "'";
            vConexion.ejecutarSQL(vQuery2);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal2();", true);
            Response.Redirect("buscarAprobar.aspx");
        }

        protected void btnModalCerrarRechazo_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "closeModal2();", true);
        }

        protected void RBConductor_TextChanged(object sender, EventArgs e)
        {
            if (RBConductor.SelectedValue == "1")
                DDLConductor.Enabled = true;
            else
            {
                DDLConductor.Enabled = false;
                DDLConductor.SelectedValue = "0";
            }
        }
    }
}