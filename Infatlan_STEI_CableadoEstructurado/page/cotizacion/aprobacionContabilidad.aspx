<%@ Page Title="" Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="aprobacionContabilidad.aspx.cs" Inherits="Infatlan_STEI_CableadoEstructurado.paginas.aprobacionContabilidad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">

    <script type="text/javascript">
        //Abrir modal
        function openModal() {
            $('#ModificarMaterialModal').modal('show');
        }

        // Cerrar modal
        function closeModal() {
            $('#ModificarMaterialModal').modal('hide');
        }

        //Abrir modal
        function openModalConta() {
            $('#MensajeAceptacionModalContabilidad').modal('show');
        }

        // Cerrar modal
        function closeModalConta() {
            $('#MensajeAceptacionModalContabilidad').modal('hide');
        }

        //Abrir modal
        function openModalBanco() {
            $('#MensajeAceptacionModalBanco').modal('show');
        }

        // Cerrar modal
        function closeModalBanco() {
            $('#MensajeAceptacionModalBanco').modal('hide');
        }
        function ShowPopupModal() {
            $("#BtnModGuardarConta").click();
        }

    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="row page-titles">
        <div class="col-md-12 align-self-center">
            <h3 class="text-themecolor">Cotización de Estudios</h3>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-body">

                    <ul class="nav nav-tabs" role="tablist">
                        <li class="nav-item"><a class="nav-link active" data-toggle="tab" href="#nav_Contabilidad" role="tab"><span class="hidden-sm-up"><i class="ti-money"></i></span><span class="hidden-xs-down">Presupuesto</span></a> </li>

                        <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#nav_Costos" role="tab"><span class="hidden-sm-up"><i class="ti-bar-chart-alt"></i></span><span class="hidden-xs-down">Costos</span></a> </li>
                    </ul>


                    <div class="tab-content tabcontent-border">

                        <%-- Sección 1 --%>
                        <div class="tab-pane active" id="nav_Contabilidad" role="tabpanel">
                            <div class="p-20">
                                <div class="row">
                                    <div class="col-12 grid-margin stretch-card">
                                        <div class="card">
                                            <div class="card-body">

                                                <div class="row">

                                                    <div class="col-md-12">
                                                        <asp:UpdatePanel ID="udpContabilidad" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <div class="col-12 grid-margin stretch-card">
                                                                    <div class="card">
                                                                        <h4 class="card-title">Presupuesto de Trabajo, 
                                                                           <asp:Label Text="" ID="lbNombre" runat="server" />
                                                                         </h4>
                                                                        <br />
                                                                        <br />
                                                                        <div class="card-body">
                                                                            <h4 >Materiales Agregados por el Técnico</h4>
                                                                          <br />
                                                                            <asp:UpdatePanel ID="udpGVContabilidad" runat="server" UpdateMode="Conditional">
                                                                                <ContentTemplate>
                                                                                    <div class="row">
                                                                                        <div class="table-responsive">
                                                                                            <asp:GridView ID="GVContabilidad" runat="server"
                                                                                                CssClass="table table-bordered"
                                                                                                HeaderStyle-HorizontalAlign="Center"
                                                                                                AutoPostBack="true"
                                                                                                PagerStyle-CssClass="pgr"
                                                                                                HeaderStyle-CssClass="table"
                                                                                                RowStyle-CssClass="rows"
                                                                                                AutoGenerateColumns="false"
                                                                                                AllowPaging="true"
                                                                                                GridLines="None"
                                                                                                PageSize="10"               
                                                                                                OnPageIndexChanging="GVContabilidad_PageIndexChanging" OnRowCommand="GVContabilidad_RowCommand"
                                                                                                OnRowDataBound="GVContabilidad_RowDataBound">
                                                                                                <Columns>

                                                                                                    <asp:BoundField DataField="idStock" HeaderText="Id" Visible="false" />
                                                                                                    <asp:BoundField DataField="proveedor" HeaderText="Proveedor" />
                                                                                                    <asp:BoundField DataField="codigo" HeaderText="Codigo" />
                                                                                                    <asp:BoundField DataField="material" HeaderText="Material Solicitado" />
                                                                                                     <asp:BoundField DataField="cantidadStock" HeaderText="Cantidad Disponible" />
                                                                                                     <asp:BoundField DataField="cantidad" ControlStyle-CssClass="align-text: align-content-center" HeaderText="Cantidad Solicitada" />
                                                                                                    <asp:BoundField DataField="precio" HeaderText="Costo Unitario (Lps.)" />
                                                                                                    <asp:BoundField DataField="costoTotal" HeaderText="Costo Total (Lps.)" />
                                                                                                    <asp:TemplateField HeaderStyle-Width="60px" Visible="true">
                                                                                                      
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="BtnModificar" runat="server" Text="Modificar" class="btn btn-primary mr-2" CommandArgument='<%#Eval("idEstudio") + ";" +Eval("idStock")%>' CommandName="Modificar">
                                                                                                                 <i class="icon-pencil" ></i>
                                                                                                            </asp:LinkButton>
                                                                                                            
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                            </asp:GridView>
                                                                                        </div>
                                                                                    </div>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>

                                                    <div class="col-12">
                                                        <div class="form-group row">
                                                            <label class="col-9 col-form-label" style="text-align: right"><b>Costo Total Materiales </b>Lps.</label>
                                                            <div class="col-sm-3">
                                                                <asp:UpdatePanel ID="udpCostoTotalMateriales" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox style="text-align:left" runat="server" ID="txtCostoTotalMateriales" class="form-control col-5 text-center color:black" min="0" ReadOnly="true" BorderStyle="None" BackColor="Transparent" ></asp:TextBox>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>
                                                    </div>


                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <%-- Sección 2 --%>

                        
                        <div class="tab-pane p-20" id="nav_Costos" role="tabpanel">
                            <div class="p-20">
                                <div class="row">

                                    <div class="col-12 grid-margin stretch-card">
                                        <div class="card">
                                             <h4 class="card-title">Costos de la Cotización </h4>
                                            <br />
                                            <br />

                                            <div class="row">

                                                <div class="col-md-6">
                                                    <div class="form-group row ">
                                                        <label class="col-sm-4 col-form-label">Horas Extras</label>
                                                        <div class="col-sm-6 ">
                                                            <asp:UpdatePanel ID="udpHorasExtras" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtHorasExtras" class="form-control" Style="text-align: center" min="0"></asp:TextBox>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-6">
                                                    <div class="form-group row ">
                                                        <label class="col-sm-4 col-form-label">Gastos de viaje Estudio</label>
                                                        <div class="col-sm-6 ">
                                                            <asp:UpdatePanel ID="udpGastoViaje" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtGastoViaje" class="form-control" min="0" Style="text-align: center"></asp:TextBox>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>

                                            <div class="row">

                                                <div class="col-md-6">
                                                    <div class="form-group row ">
                                                        <label class="col-sm-4 col-form-label">Mano de Obra Contra</label>
                                                        <div class="col-sm-6 ">
                                                            <asp:UpdatePanel ID="udpManoObraContra" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtManoObraContra" min="0" class="form-control" Style="text-align: center"></asp:TextBox>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-6">
                                                    <div class="form-group row ">
                                                        <label class="col-sm-4 col-form-label">Transporte</label>
                                                        <div class="col-sm-6 ">
                                                            <asp:UpdatePanel ID="udpTranspote" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtTransporte" class="form-control" min="0" Style="text-align: center"></asp:TextBox>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>

                                            <div class="row">

                                                <div class="col-md-6">
                                                    <div class="form-group row ">
                                                        <label class="col-sm-4 col-form-label">Alimentación</label>
                                                        <div class="col-sm-6 ">
                                                            <asp:UpdatePanel ID="udpALimentacion" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtAlimentacion" min="0" class="form-control" Style="text-align: center"></asp:TextBox>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-6">
                                                    <div class="form-group row ">
                                                        <label class="col-sm-4 col-form-label">Hospedaje</label>
                                                        <div class="col-sm-6 ">
                                                            <asp:UpdatePanel ID="udpHospedaje" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtHospedaje" min="0" class="form-control" Style="text-align: center"></asp:TextBox>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>

                                            <div class="row">

                                                <div class="col-md-6">
                                                    <div class="form-group row ">
                                                        <label class="col-sm-4 col-form-label">Imprevistos</label>
                                                        <div class="col-sm-6 ">
                                                            <asp:UpdatePanel ID="udpImprevistos" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtImprevistos" min="0" class="form-control" Style="text-align: center"></asp:TextBox>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-6">
                                                    <div class="form-group row ">
                                                        <label class="col-sm-4 col-form-label">Total Nodos</label>
                                                        <div class="col-sm-6 ">
                                                            <asp:UpdatePanel ID="udpTotalNodos" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <asp:TextBox runat="server" ID="txtTotalNodos" min="0" class="form-control" Style="text-align: center"></asp:TextBox>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>


                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                <ContentTemplate>

                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                        <ContentTemplate>
                                                            <div style="margin-left: auto; margin-right: auto; text-align: center; width: 40%;">
                                                                <asp:Button runat="server" ID="btnCalcular" Text="Calcular" OnClick="btnCalcular_Click" class="btn  btn-block btn-success" />
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <br />



                                                    <%-- TABLA Costos --%>


                                                    <table class="table color-bordered-table muted-bordered-table table-bordered" data-tablesaw-mode="swipe" style="width: 80%; margin: 0 auto;">
                                                        <thead>
                                                            <tr>
                                                                <th scope="col" data-tablesaw-sortable-coldata-tablesaw-priority="persist" class="border text-xl-center">Descripción
                                                                </th>
                                                                <th scope="col" data-tablesaw-sortable-coldata-tablesaw-priority="2" class="border text-xl-center" style="width: 20%;">Costos
                                                                </th>

                                                            </tr>
                                                        </thead>

                                                        <tbody>

                                                            <tr>
                                                                <td>
                                                                    <asp:Label runat="server" class="control-label" ID="lbValorGanancia">Ganancia</asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:UpdatePanel ID="udpValorGanancia" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox ID="txtGanancia" runat="server" class="form-control" Style="text-align: right"></asp:TextBox>

                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td>
                                                                    <asp:Label runat="server" class="control-label" ID="Label2">15% Ganancia</asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:UpdatePanel ID="udpGanancia" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox ID="txtIsvGanancia" runat="server" class="form-control" Style="text-align: right"></asp:TextBox>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>

                                                            <tr>

                                                                <td>
                                                                    <asp:Label runat="server" class="control-label" ID="Label1">Costo Total</asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:UpdatePanel ID="udpCostoTotal" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox runat="server" ID="txtCostoTotal" class="form-control" Style="text-align: right"></asp:TextBox>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>

                                                            </tr>

                                                            <tr>
                                                                <td>
                                                                    <asp:Label runat="server" class="control-label" ID="Label3">Propuesta</asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:UpdatePanel runat="server" ID="udpPropuesta" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox runat="server" ID="txtPropuesta" class="form-control" Style="text-align: right"></asp:TextBox>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td>
                                                                    <asp:Label runat="server" class="control-label" ID="Label4">15 % isv</asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:UpdatePanel runat="server" ID="udpIsvCostos" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox runat="server" ID="txtisvCostoTotal" class="form-control" Style="text-align: right"></asp:TextBox>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td>
                                                                    <asp:Label runat="server" class="control-label" ID="Label5">Total Cotización</asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:UpdatePanel runat="server" ID="udpTotalCot" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox runat="server" ID="txtTotalCot" class="form-control" Style="text-align: right"></asp:TextBox>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>

                                                        </tbody>
                                                    </table>

                                                    <br />
                                                    <br />


                                                    <%-- TABAL Total Cotizacion --%>

                                                    <table class="table color-bordered-table muted-bordered-table table-bordered" data-tablesaw-mode="swipe" style="width: 80%; margin: 0 auto;">
                                                        <thead>
                                                            <tr>
                                                                <th scope="col" data-tablesaw-sortable-coldata-tablesaw-priority="persist" class="border text-xl-center">Descripción
                                                                </th>
                                                                <th scope="col" data-tablesaw-sortable-coldata-tablesaw-priority="2" class="border text-xl-center" style="width: 20%;">Costos
                                                                </th>

                                                            </tr>
                                                        </thead>

                                                        <tbody>

                                                            <tr>
                                                                <td>
                                                                    <asp:Label runat="server" class="control-label" ID="lbTotalMateriales">Total Materiales</asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:UpdatePanel runat="server" ID="udpTotalMateriales" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox ID="txtTotalMateriales" runat="server" class="form-control" Style="text-align: right"></asp:TextBox>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td>
                                                                    <asp:Label runat="server" class="control-label" ID="lbManoObra">Mano de obra</asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:UpdatePanel runat="server" ID="udpCostosManoObra" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox ID="txtCostosManoObra" runat="server" class="form-control" Style="text-align: right"></asp:TextBox>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td>
                                                                    <asp:Label runat="server" class="control-label" ID="lbTotalProyecto">Costo Total Proyecto</asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:UpdatePanel runat="server" ID="udpCostoTotalProyecto" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox ID="txtCostoTotalProyecto" runat="server" class="form-control" Style="text-align: right"></asp:TextBox>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td>
                                                                    <asp:Label runat="server" class="control-label" ID="lbIsvCotizacion">15 % isv</asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:UpdatePanel runat="server" ID="udpIsvCotizacion" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox ID="txtIsvCotizacion" runat="server" class="form-control" Style="text-align: right"></asp:TextBox>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td>
                                                                    <asp:Label runat="server" class="control-label" ID="lbTotalCotizacion">Total Cotización</asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:UpdatePanel runat="server" ID="udpCostoTotalCotizacion" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox ID="txtCostoTotalCotizacion" runat="server" class="form-control" Style="text-align: right"></asp:TextBox>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>

                                                        </tbody>
                                                    </table>

                                                    <br />
                                                    <br />

                                                    <%-- TABLA Nodos --%>


                                                    <table class="table color-bordered-table muted-bordered-table table-bordered" data-tablesaw-mode="swipe" style="width: 80%; margin: 0 auto;">
                                                        <thead>
                                                            <tr>
                                                                <th scope="col" data-tablesaw-sortable-coldata-tablesaw-priority="persist" class="border text-xl-center">Descripción
                                                                </th>
                                                                <th scope="col" data-tablesaw-sortable-coldata-tablesaw-priority="2" class="border text-xl-center" style="width: 20%;">Costos
                                                                </th>

                                                            </tr>
                                                        </thead>

                                                        <tbody>

                                                            <tr>
                                                                <td>
                                                                    <asp:Label runat="server" class="control-label" ID="Label6">Costo Nodo en Lps.</asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:UpdatePanel runat="server" ID="udpCostoNodoLps" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox runat="server" ID="txtCostoNodoLps" class="form-control" Style="text-align: right"></asp:TextBox>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td>
                                                                    <asp:Label runat="server" class="control-label" ID="Label7">Costo Nodo en USD</asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:UpdatePanel runat="server" ID="udpCostoNodoUsd" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox ID="txtCostoNodoUsd" runat="server" class="form-control" Style="text-align: right"></asp:TextBox>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>

                                                        </tbody>
                                                    </table>

                                                    <br />
                                                    <br />

                                                    <%-- TABLA Ganancia --%>


                                                    <table class="table color-bordered-table muted-bordered-table table-bordered" data-tablesaw-mode="swipe" style="width: 80%; margin: 0 auto;">
                                                        <thead>
                                                            <tr>
                                                                <th scope="col" data-tablesaw-sortable-coldata-tablesaw-priority="persist" class="border text-xl-center">Descripción
                                                                </th>
                                                                <th scope="col" data-tablesaw-sortable-coldata-tablesaw-priority="2" class="border text-xl-center" style="width: 20%;">Costos
                                                                </th>

                                                            </tr>
                                                        </thead>

                                                        <tbody>

                                                            <tr>
                                                                <td>
                                                                    <asp:Label runat="server" class="control-label" ID="Label8">Ganancia</asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox runat="server" ID="txtCostoGanancia" class="form-control " Style="text-align: right"></asp:TextBox>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td>
                                                                    <asp:Label runat="server" class="control-label" ID="Label9">15% Ganancia</asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox ID="txtCostoIsvGanancia" runat="server" class="form-control" Style="text-align: right"></asp:TextBox>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>


                                                            <tr>
                                                                <td>
                                                                    <asp:Label runat="server" class="control-label" ID="Label10">Costo Mano de Obra</asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox ID="txtCostoManosObraGanancia" runat="server" class="form-control" Style="text-align: right"></asp:TextBox>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>

                                                        </tbody>
                                                    </table>

                                                    <br />
                                                    <br />


                                                    </div>

                                                </ContentTemplate>
                                                <%-- <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnCalcular" EventName="Click"/>
                                                </Triggers>--%>
                                            </asp:UpdatePanel>


                                            <asp:UpdatePanel runat="server" ID="UpdatePanel11" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div class="col-md-6 align-self-center" style="margin-left: auto; margin-right: auto">
                                                        <div class="row col-11">
                                                            <asp:Button runat="server" ID="btnGuardarCotizacion" Text="Guardar" CssClass="btn  btn-block btn-success" OnClick="btnGuardarCotizacion_Click" />
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>

                                            <%--FIN ETIQUETA CARD --%>
                                        </div>
                                    </div>
                                </div>

                                <%-- ETIQUETA TODA LA PAGINA --%>
                            </div>
                        </div>

                    </div>

                </div>
            </div>
        </div>
    </div>




    <%--MODAL DE MODIFICACION DE MATERIAL--%>
    <div class="modal fade" id="ModificarMaterialModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="width: 600px; top: 320px; left: 50%; transform: translate(-50%, -50%);">
                <div class="modal-header">
                    <h4 class="modal-title" id="ModalLabelQA">
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>
                                Modificar Material
                                <asp:Label ID="LbModificarMaterial" runat="server" Text=""></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="card">
                            <div class="card-body">
                                <asp:UpdatePanel ID="UpdateModalContabilidad" runat="server">
                                    <ContentTemplate>

                                        <div class="col-sm-9">
                                            <asp:TextBox ID="txtModIdInventario" placeholder="" class="form-control" runat="server" Visible="false" ReadOnly="true"></asp:TextBox>
                                        </div>

                                        <div class="row">

                                            <div class="col-md-12">
                                                <div class="form-group row">
                                                    <label class="col-sm-3 col-form-label">Material</label>
                                                    <div class="col-sm-9">
                                                        <asp:TextBox runat="server" ID="txtModMaterial" CssClass="form-control text-center" ReadOnly="true"></asp:TextBox>

                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12">
                                                <div class="form-group row">
                                                    <label class="col-sm-3 col-form-label">Costo Unitario</label>
                                                    <div class="col-sm-9">
                                                        <asp:TextBox ID="txtModCostoUnitario" placeholder="" class="form-control text-center" runat="server" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12">
                                                <div class="form-group row">
                                                    <label class="col-sm-3 col-form-label">Cantidad</label>
                                                    <div class="col-sm-9">
                                                        <asp:TextBox runat="server" ID="txtModCantidad" CssClass="form-control text-center"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>



                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <asp:UpdatePanel ID="UpdateUsuarioMensaje" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="form-group row">
                                        <asp:Label ID="lbUsuarioMensaje" runat="server" Text="" Class="col-sm-12" Style="color: indianred; text-align: center;"></asp:Label>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                            <asp:Button ID="BtnModModificarMaterial" runat="server" Text="Modificar" class="btn btn-primary" OnClick="BtnModModificarMaterial_Click" AutoPostBack="true" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <%-- MODAL DE MENSAJE DE CONFIRMACIÓN CONTABILIDAD --%>

    <div class="modal fade" id="MensajeAceptacionModalContabilidad" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:Label ID="lbMensaje" runat="server" CssClass="align-content-center"><b>¿Está seguro de que desea guardar los cambios efectuados? </b></asp:Label>
                </div>
                <div class="modal-body">
                    <asp:Label ID="lbAlerta" runat="server" CssClass="col-form-label text-white"><b></b></asp:Label>
                </div>

                <div class="modal-footer">
                    <asp:UpdatePanel ID="udpModGuardar" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                            <asp:Button ID="BtnModGuardarConta" runat="server" Text="Guardar" class="btn btn-primary" AutoPostBack="true" OnClick="BtnModGuardarConta_Click" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="BtnModGuardarConta" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>

            </div>
        </div>
    </div>

     <%-- MODAL DE MENSAJE DE CONFIRMACIÓN BANCO --%>

    <div class="modal fade" id="MensajeAceptacionModalBanco" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:Label ID="Label11" runat="server" CssClass="align-content-center"><b>¿Desea generar la oferta económica ? </b></asp:Label>
                </div>
                <div class="modal-body">
                    <asp:Label ID="Label12" runat="server" CssClass="col-form-label text-white"><b></b></asp:Label>
                </div>

                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Button ID="btnGenerar" runat="server" Text="Generar" class="btn btn-primary" AutoPostBack="true" ></asp:Button>
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" class="btn btn-primary" AutoPostBack="true"/>
                        </ContentTemplate>
                        <%--<Triggers>
                            <asp:PostBackTrigger ControlID="BtnModGuardarConta" />
                        </Triggers>--%>
                    </asp:UpdatePanel>
                </div>

            </div>
        </div>
    </div>

     <%-- MODAL DE MENSAJE  --%>

    <div class="modal fade" id="MensajeAlerta" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:Label ID="Label13" runat="server" CssClass="align-content-center"><b>Cantidad Solicitada Mayor a Inventario</b></asp:Label>
                </div>
                
                </div>

      
        </div>
    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
