using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infatlan_STEI_CableadoEstructurado.clases;
using System.Data;
using System.Data.Sql;

namespace Infatlan_STEI_CableadoEstructurado.paginas
{
    public partial class prueba : System.Web.UI.Page
    {
        db vConexion = new db();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void FillGrid()
        {

            String vQuery = "STEISP_CABLESTRUCTURADO_Datos 6, ";
            DataTable vDatos = vConexion.obtenerDataTable(vQuery);
            DataTable vData = (DataTable)Session["CE_CABLEADO"];

            //ContactTableAdapter contact = new ContactTableAdapter();
            //DataTable contacts = vDatos.GetData();

            if (vData.Rows.Count > 0)
            {
                GVContabilidad.DataSource = vData;
                GVContabilidad.DataBind();
            }
            else
            {
                vData.Rows.Add(vData.NewRow());
                GVContabilidad.DataSource = vData;
                GVContabilidad.DataBind();

                int TotalColumns = GVContabilidad.Rows[0].Cells.Count;
                GVContabilidad.Rows[0].Cells.Clear();
                GVContabilidad.Rows[0].Cells.Add(new TableCell());
                GVContabilidad.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                GVContabilidad.Rows[0].Cells[0].Text = "No Record Found";
            }
        }

        protected void GVContabilidad_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void GVContabilidad_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            String vQuery = "STEISP_CABLESTRUCTURADO_Datos 6, ";
            DataTable vDatos = vConexion.obtenerDataTable(vQuery);
            DataTable vData = (DataTable)Session["CE_CABLEADO"];

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblType = (Label)e.Row.FindControl("lblType");
                if (lblType != null)
                {
                    int typeId = Convert.ToInt32(lblType.Text);
                   // lblType.Text = (string)vData.GetTypeById(typeId);
                }
                DropDownList cmbType = (DropDownList)e.Row.FindControl("cmbType");
                if (cmbType != null)
                {
                    cmbType.DataSource = vData;
                    cmbType.DataTextField = "TypeName";
                    cmbType.DataValueField = "Id";
                    cmbType.DataBind();
                    cmbType.SelectedValue =
                      GVContabilidad.DataKeys[e.Row.RowIndex].Values[1].ToString();
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                DropDownList cmbNewType = (DropDownList)e.Row.FindControl("cmbNewType");
                cmbNewType.DataSource = vData;
                cmbNewType.DataBind();
            }

        }

        protected void GVContabilidad_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void GVContabilidad_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void GVContabilidad_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

       

        

    }
}