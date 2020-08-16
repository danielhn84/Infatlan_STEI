<%@ Page Title="" Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="aprobacionContabilidad.aspx.cs" Inherits="Infatlan_STEI_CableadoEstructurado.paginas.aprobacionContabilidad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">

    <script type="text/javascript">
        var updateProgress = null;
        function postbackButtonClick() {
            updateProgress = $find("<%= UpdateProgress1.ClientID %>");
            window.setTimeout("updateProgress.set_visible(true)", updateProgress.get_displayAfter());
            return true;
        }
    </script>
    <script type="text/javascript">
        var url = document.location.toString();
        if (url.match('#')) {
            $('.nav-tabs a[href="#' + url.split('#')[1] + '"]').tab('show');
        }

        $('.nav-tabs a').on('shown.bs.tab', function (e) {
            window.location.hash = e.target.hash;
        })
    </script>
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
    <link href="../../assets/node_module/select2/dist/css/select2.min.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #ffffff; opacity: 0.7; margin: 0;">
                <span style="display: inline-block; height: 100%; vertical-align: middle;"></span>
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="../../assets/images/loading.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="display: inline-block; vertical-align: middle;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <div class="row page-titles">
        <div class="col-md-5 align-self-center">
            <h4 class="text-themecolor">STEI</h4>
        </div>
        <div class="col-md-7 align-self-center text-right">
            <div class="d-flex justify-content-end align-items-center">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Módulos</a></li>
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Cableado</a></li>
                    <li class="breadcrumb-item"><a href="javascript:void(0)">
                        <asp:Label ID="Label15" runat="server" /></a></li>
                    <li class="breadcrumb-item active">
                        <asp:Label ID="Label16" runat="server" /></li>
                </ol>
            </div>
        </div>
    </div>

    <div class="card">
        <div class="card-body">
            <h4 class="card-title">Cotización Estudios</h4>
            <h6 class="card-subtitle">Contabilidad</h6>

            <div class="row">
                <div class="col-md-12">
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="nav-item"><a class="nav-link active" data-toggle="tab" href="#nav_Contabilidad" role="tab"><span class="hidden-sm-up"><i class="ti-money"></i></span><span class="hidden-xs-down">Presupuesto</span></a> </li>
                        <li class="nav-item"><a runat="server" id="navCostos" visible="true" class="nav-link" data-toggle="tab" href="#nav_Costos" role="tab"><span class="hidden-sm-up"><i class="ti-bar-chart-alt"></i></span><span class="hidden-xs-down">Costos</span></a> </li>
                    </ul>

                    <div class="tab-content tabcontent-border">
                        <%-- Sección 1 --%>
                        <div class="tab-pane active" id="nav_Contabilidad" role="tabpanel">
                            <div class="p-20">

                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="card">
                                            <div class="card-body">

                                                <div class="row">

                                                    <div class="col-md-12">
                                                        <asp:UpdatePanel ID="udpContabilidad" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>

                                                                <h3>Presupuesto de Trabajo, 
                                                            <b>
                                                                <asp:Label Text="" ID="lbNombre" runat="server" /></b>
                                                                </h3>
                                                                <br />

                                                                <p>
                                                                    Técnico Responsable: 
                                                            <b>
                                                                <asp:Label Text="" ID="LbResponsable" runat="server" /></b>
                                                                </p>
                                                                <br />
                                                                <br />

                                                                <div class="row" runat="server" id="DivEditar" visible="false">
                                                                    <div class="col-sm-12" style="text-align: right;">
                                                                        <asp:Button runat="server" ID="btnEnviarTecnico" Text="Enviar Técnico" class="btn btn-primary mr-2" OnClick="btnEnviarTecnico_Click" />
                                                                        <asp:Button runat="server" ID="btnAgregarMaterial" Text="Material (+)" class="btn btn-primary mr-2" OnClick="btnAgregarMaterial_Click" />
                                                                    </div>
                                                                </div>
                                                                <br />

                                                                <%--<h4 class="card-title">
                                                                    <asp:Label runat="server" ID="LbTitulo" Text="Lista de Materiales"></asp:Label>
                                                                </h4>--%>
                                                                <p>
                                                                    <asp:Label runat="server" ID="LbDescripcion" Text="Materiales agregados por el Técnico."></asp:Label>
                                                                </p>
                                                                <br />
                                                                <asp:UpdatePanel ID="udpGVContabilidad" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>

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
                                                                                OnPageIndexChanging="GVContabilidad_PageIndexChanging"
                                                                                OnRowCommand="GVContabilidad_RowCommand">
                                                                                <%--OnRowDataBound="GVContabilidad_RowDataBound"--%>
                                                                                <Columns>

                                                                                    <asp:BoundField DataField="idStock" HeaderText="Id" Visible="false" />
                                                                                    <asp:BoundField DataField="proveedor" HeaderText="Proveedor" />
                                                                                    <asp:BoundField DataField="codigo" HeaderText="Codigo" />
                                                                                    <asp:BoundField DataField="material" HeaderText="Material Solicitado" />
                                                                                    <%-- <asp:BoundField DataField="cantidadStock" HeaderText="Cantidad Disponible" />--%>
                                                                                    <asp:BoundField DataField="cantidad" ItemStyle-CssClass="text-center" HeaderText="Cantidad" />
                                                                                    <asp:BoundField DataField="precio" ItemStyle-CssClass="text-right" HeaderText="Costo Unitario" ItemStyle-Width="15%" ControlStyle-Width="300px" DataFormatString="{0:C}" />
                                                                                    <asp:BoundField DataField="costoTotal" HeaderText="Costo Total" ItemStyle-CssClass="text-right" ItemStyle-Width="15%" ControlStyle-Width="300px" DataFormatString="{0:C}" />
                                                                                   <%-- <asp:TemplateField HeaderStyle-Width="140px" Visible="true">--%>
                                                                                     <asp:TemplateField HeaderStyle-Width="60px" Visible="true">
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="BtnModificar" Visible="false" runat="server" Text="Modificar" class="btn btn-primary mr-2" CommandArgument='<%#Eval("idEstudio") + ";" +Eval("idStock")%>' CommandName="Modificar" Title="Modificar">
                                                                                                            <i class="icon-pencil" ></i>
                                                                                            </asp:LinkButton>
                                                                                            <%--<asp:LinkButton ID="BtnBorrar" runat="server" class="btn btn-success mr-2" CommandArgument='<%#Eval("idEstudio") + ";" +Eval("idStock")%>' CommandName="Eliminar" Title="Eliminar">
                                                                                                            <i class="icon-trash" ></i>
                                                                                            </asp:LinkButton>--%>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </div>

                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>

                                                    <div class="col-12">
                                                        <div class="form-group row">
                                                            <label class="col-10 col-form-label" style="text-align: right"><b>Costo Total Materiales </b></label>
                                                            <div class="col-sm-2">
                                                                <asp:UpdatePanel ID="udpCostoTotalMateriales" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox Style="text-align: right" runat="server" ID="txtCostoTotalMateriales" class="form-control text-right color:black" min="0" ReadOnly="true" BorderStyle="None" BackColor="Transparent" AutoPostBack="true"></asp:TextBox>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-12 align-content-center">
                                                        <div class="form-group">
                                                            <h4 class="card-title">
                                                                <asp:Label runat="server" ID="lbplano" Text="Plano"></asp:Label>
                                                            </h4>
                                                            <p>PDF ingresado por el Técnico</p>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12 align-content-center">
                                                        <div class="form-group">
                                                            <iframe runat="server" id="IframePlano" style="width: 100%; height: 500px; border: none;"></iframe>
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
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="card">
                                        <div class="card-body">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <br />
                                                    <h6 class="card-subtitle">
                                                        <asp:Label runat="server" ID="Label14" Text="Calculos de los costos que genera un estudio"></asp:Label></h6>
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

                                                            <table class="table color-bordered-table muted-bordered-table table-bordered" data-tablesaw-mode="swipe" style="width: 70%; margin: 0 auto;">
                                                                <thead>
                                                                    <tr>
                                                                        <th scope="col" data-tablesaw-sortable-coldata-tablesaw-priority="persist" class="border text-xl-center">Descripción
                                                                        </th>
                                                                        <th scope="col" data-tablesaw-sortable-coldata-tablesaw-priority="2" class="border text-xl-center" style="width: 30%;">Costos
                                                                        </th>

                                                                    </tr>
                                                                </thead>

                                                                <tbody>

                                                                    <tr>

                                                                        <td>
                                                                            <asp:Label runat="server" class="control-label" ID="Label1">Costo Total</asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:UpdatePanel ID="udpCostoTotal" runat="server" UpdateMode="Conditional">
                                                                                <ContentTemplate>
                                                                                    <asp:TextBox runat="server" ID="txtCostoTotal" class="form-control" Style="text-align: right" BorderStyle="None" BackColor="Transparent"></asp:TextBox>
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
                                                                                    <asp:TextBox ID="txtIsvGanancia" runat="server" class="form-control" Style="text-align: right" BorderStyle="None" BackColor="Transparent"></asp:TextBox>
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
                                                                                    <asp:TextBox runat="server" ID="txtPropuesta" class="form-control" Style="text-align: right" BorderStyle="None" BackColor="Transparent"></asp:TextBox>
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
                                                                                    <asp:TextBox runat="server" ID="txtisvCostoTotal" class="form-control" Style="text-align: right" BorderStyle="None" BackColor="Transparent"></asp:TextBox>
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
                                                                                    <asp:TextBox runat="server" ID="txtTotalCot" class="form-control" Style="text-align: right" BorderStyle="None" BackColor="Transparent"></asp:TextBox>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>
                                                                    </tr>

                                                                </tbody>
                                                            </table>

                                                            <br />
                                                            <br />

                                                            <%-- TABLA Total Cotizacion --%>

                                                            <table class="table color-bordered-table muted-bordered-table table-bordered" data-tablesaw-mode="swipe" style="width: 70%; margin: 0 auto;">
                                                                <thead>
                                                                    <tr>
                                                                        <th scope="col" data-tablesaw-sortable-coldata-tablesaw-priority="persist" class="border text-xl-center">Descripción
                                                                        </th>
                                                                        <th scope="col" data-tablesaw-sortable-coldata-tablesaw-priority="2" class="border text-xl-center" style="width: 30%;">Costos
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
                                                                                    <asp:TextBox ID="txtTotalMateriales" runat="server" class="form-control" Style="text-align: right" BorderStyle="None" BackColor="Transparent"></asp:TextBox>
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
                                                                                    <asp:TextBox ID="txtCostosManoObra" runat="server" class="form-control" Style="text-align: right" BorderStyle="None" BackColor="Transparent"></asp:TextBox>
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
                                                                                    <asp:TextBox ID="txtCostoTotalProyecto" runat="server" class="form-control" Style="text-align: right" BorderStyle="None" BackColor="Transparent"></asp:TextBox>
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
                                                                                    <asp:TextBox ID="txtIsvCotizacion" runat="server" class="form-control" Style="text-align: right" BorderStyle="None" BackColor="Transparent"></asp:TextBox>
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
                                                                                    <asp:TextBox ID="txtCostoTotalCotizacion" runat="server" class="form-control" Style="text-align: right" BorderStyle="None" BackColor="Transparent"></asp:TextBox>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>
                                                                    </tr>

                                                                </tbody>
                                                            </table>

                                                            <br />
                                                            <br />

                                                            <%-- TABLA Nodos --%>

                                                            <table class="table color-bordered-table muted-bordered-table table-bordered" data-tablesaw-mode="swipe" style="width: 70%; margin: 0 auto;">
                                                                <thead>
                                                                    <tr>
                                                                        <th scope="col" data-tablesaw-sortable-coldata-tablesaw-priority="persist" class="border text-xl-center">Descripción
                                                                        </th>
                                                                        <th scope="col" data-tablesaw-sortable-coldata-tablesaw-priority="2" class="border text-xl-center" style="width: 30%;">Costos
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
                                                                                    <asp:TextBox runat="server" ID="txtCostoNodoLps" class="form-control" Style="text-align: right" BorderStyle="None" BackColor="Transparent"></asp:TextBox>
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
                                                                                    <asp:TextBox ID="txtCostoNodoUsd" runat="server" class="form-control" Style="text-align: right" BorderStyle="None" BackColor="Transparent"></asp:TextBox>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>
                                                                    </tr>

                                                                </tbody>
                                                            </table>

                                                            <br />
                                                            <br />

                                                            <%-- TABLA Ganancia --%>

                                                            <table class="table color-bordered-table muted-bordered-table table-bordered" data-tablesaw-mode="swipe" style="width: 70%; margin: 0 auto;">
                                                                <thead>
                                                                    <tr>
                                                                        <th scope="col" data-tablesaw-sortable-coldata-tablesaw-priority="persist" class="border text-xl-center">Descripción
                                                                        </th>
                                                                        <th scope="col" data-tablesaw-sortable-coldata-tablesaw-priority="2" class="border text-xl-center" style="width: 30%;">Costos
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
                                                                                    <asp:TextBox runat="server" ID="txtCostoGanancia" class="form-control " Style="text-align: right" BorderStyle="None" BackColor="Transparent"></asp:TextBox>
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
                                                                                    <asp:TextBox ID="txtCostoIsvGanancia" runat="server" class="form-control" Style="text-align: right" BorderStyle="None" BackColor="Transparent"></asp:TextBox>
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
                                                                                    <asp:TextBox ID="txtCostoManosObraGanancia" runat="server" class="form-control" Style="text-align: right" BorderStyle="None" BackColor="Transparent"></asp:TextBox>
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
                                                    </asp:UpdatePanel>

                                                    <div class="col-12" style="align-content:center">
                                                        <asp:UpdatePanel runat="server" ID="UpdatePanel11" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <%--<div class="col-md-12 align-self-center" style="margin-left: auto; margin-right: auto">--%>
                                                                 <div style="margin-left: auto; margin-right: auto; text-align: center; width: 40%;">
                                                                    <div class="row">
                                                                        <asp:Button runat="server" Visible="false" ID="btnGuardarCotizacion" Text="Guardar" CssClass="btn  btn-block btn-success" OnClick="btnGuardarCotizacion_Click" />
                                                                    </div>
                                                                </div>
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
                            <%-- ETIQUETA TODA LA PAGINA --%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
<%--    </div>--%>

    <%--MODAL DE MODIFICACION DE MATERIAL--%>
    <div class="modal fade" id="ModificarMaterialModal" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="width: 600px; top: 320px; left: 50%; transform: translate(-50%, -50%);">
                <div class="modal-header">
                    <h4 class="modal-title" id="ModalLabelQA">
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="LbModificarMaterial" runat="server" Text="  Modificar Material"></asp:Label>
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

                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtModIdInventario" placeholder="" class="form-control" runat="server" Visible="false" ReadOnly="true"></asp:TextBox>
                                        </div>

                                        <div class="row">

                                            <div class="col-md-12">
                                                <div class="form-group row">
                                                    <label class="col-sm-3 col-form-label">Material</label>
                                                    <div class="col-sm-9">
                                                        <asp:DropDownList ID="ddlModMaterial" runat="server" AutoPostBack="true" Style="width: 100%" OnSelectedIndexChanged="ddlModMaterial_SelectedIndexChanged" CssClass="select2 form-control custom-select"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="form-group row">
                                                    <label class="col-sm-3 col-form-label">Cantidad</label>
                                                    <div class="col-sm-9">
                                                        <asp:TextBox runat="server" ID="txtModCantidad" TextMode="Number" CssClass="form-control text-center" min="0"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12">
                                                <div class="form-group row">
                                                    <label class="col-sm-3 col-form-label" runat="server" id="lbModCosto">Costo Unitario</label>
                                                    <div class="col-sm-9">
                                                        <asp:TextBox ID="txtModCostoUnitario" placeholder="" class="form-control text-center" runat="server" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                        <div class="col-md-12">
                                            <div class="form-group row">
                                                <div class="col-sm-12" runat="server" id="DivAlertaContabilidad" visible="false" style="display: flex; background-color: tomato; justify-content: center">
                                                    <asp:Label runat="server" CssClass="col-form-label text-white" ID="LbAlertaContabilidad"></asp:Label>
                                                </div>
                                            </div>
                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdModModificar" runat="server" UpdateMode="Conditional">
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
    <div class="modal fade" id="MensajeAceptacionModalContabilidad" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <asp:UpdatePanel ID="udpModMensajes" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-body">
                            <asp:Label ID="lbMensaje" runat="server" CssClass="align-content-center" Text=""><b> ¿Está seguro de que desea guardar los cambios efectuados?</b></asp:Label>
                        </div>

                        <div class="modal-body">
                            <label class="control-label" runat="server" id="lbComentario" visible="false">Comentario</label>
                            <asp:TextBox TextMode="MultiLine" Rows="3" Columns="15" ID="txtModObservaciones" runat="server" type="text" class="form-control" Visible="false">
                            </asp:TextBox>
                        </div>

                        <%--<asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>--%>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <div class="col-sm-12" runat="server" id="DivAlertaDescriptor" visible="false" style="display: flex; background-color: tomato; justify-content: center; ">
                                            <asp:Label runat="server" CssClass="col-form-label text-white" ID="LbAlertaDescriptor"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                          <%--  </ContentTemplate>
                        </asp:UpdatePanel>--%>

                    </ContentTemplate>
                </asp:UpdatePanel>

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
    <div class="modal fade" id="MensajeAceptacionModalBanco" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
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
                            <asp:Button ID="btnGenerar" runat="server" Text="Generar" class="btn btn-primary" AutoPostBack="true"></asp:Button>
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" class="btn btn-primary" AutoPostBack="true" />
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
    <div class="modal fade" id="MensajeAlerta" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
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
    <script src="../../assets/node_module/select2/dist/js/select2.full.min.js" type="text/javascript"></script>
    <style>
        .select2-selection__rendered {
            line-height: 31px !important;
        }

        .select2-container .select2-selection--single {
            height: 35px !important;
        }

        .select2-selection__arrow {
            height: 34px !important;
        }
    </style>
    <script>
        $(function () {
            $(".select2").select2();
            $(".ajax").select2({
                ajax: {
                    url: "https://api.github.com/search/repositories",
                    dataType: 'json',
                    delay: 250,
                    data: function (params) {
                        return {
                            q: params.term, // search term
                            page: params.page
                        };
                    },
                    processResults: function (data, params) {
                        params.page = params.page || 1;
                        return {
                            results: data.items,
                            pagination: {
                                more: (params.page * 30) < data.total_count
                            }
                        };
                    },
                    cache: true
                },
                escapeMarkup: function (markup) {
                    return markup;
                },
                minimumInputLength: 1,
            });
        });
    </script>
</asp:Content>
