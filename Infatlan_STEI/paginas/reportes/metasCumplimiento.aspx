<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="metasCumplimiento.aspx.cs" Inherits="Infatlan_STEI.paginas.reportes.metasCumplimiento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <link href="/assets/node_modules/css-chart/css-chart.css" rel="stylesheet">
    <link href="dist/css/pages/easy-pie-chart.css" rel="stylesheet">
    <style>
        .columnas {
            width: 19%;
            margin: 0% 1% 2% 0%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div class="row page-titles">
        <div class="col-md-5 align-self-center">
            <h4 class="text-themecolor">STEI</h4>
        </div>
        <div class="col-md-7 align-self-center text-right">
            <div class="d-flex justify-content-end align-items-center">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Reportes</a></li>
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Cumplimiento</a></li>
                    <li class="breadcrumb-item active">Formulario</li>
                </ol>
            </div>
        </div>
    </div>
    
    <%--LLAMADAS--%>
    <div class="card">
        <div class="card-header" role="tab" id="headingOne11">
            <a class="link" data-toggle="collapse" data-parent="#accordion2" href="#collapseOne11" aria-expanded="true" aria-controls="collapseOne11">
                <h4 class="card-title">Llamadas Telefónicas</h4>
            </a>
        </div>
        <div id="collapseOne11" class="collapse show" role="tabpanel" aria-labelledby="headingOne11">
            <div class="card-body">
                <div class="card-body">
                    <div class="row col-12">
                        <div class="columnas">
                            <label>Total</label>
                            <asp:TextBox runat="server" ID="TxCallTotal" CssClass="form-control" />
                        </div>
                        <div class="columnas">
                            <label>Atendidas</label>
                            <asp:TextBox runat="server" ID="TxCallAtendidas" CssClass="form-control" />
                        </div>
                        <div class="columnas">
                            <label>No Atendidas</label>
                            <asp:TextBox runat="server" ID="TxCallAtendidasNo" CssClass="form-control" />
                        </div>
                        <div class="columnas">
                            <label>% Atendidas</label>
                            <asp:TextBox runat="server" ID="TxCallPorcentajeSi" CssClass="form-control" />
                        </div>
                        <div class="columnas">
                            <label>% No Atendidas</label>
                            <asp:TextBox runat="server" ID="TxCallPorcentajeNo" CssClass="form-control" />
                        </div>
                        <div style="width: 60%">
                            <label>Observaciones</label>
                            <asp:TextBox runat="server" ID="TxCallObs" TextMode="MultiLine" Rows="2" CssClass="form-control" />
                        </div>
                        <div style="width: 40%">
                            <div class="text-center" runat="server" visible="true" id="DivCallGrafic">
                                <asp:Literal Text="" ID="LitCall" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--Medios de Pago--%>
    <div class="card">
        <div class="card-header" role="tab" id="heading2">
            <a class="link" data-toggle="collapse" data-parent="#accordion2" href="#collapse2" aria-expanded="true" aria-controls="collapseOne11">
                <h4 class="card-title">Medios de Pago</h4>
            </a>
        </div>
        <div id="collapse2" class="collapse show" role="tabpanel" aria-labelledby="headingOne11">
            <div class="card-body">
                <div class="card-body">
                    <h4 class="card-subtitle"><b> ATM</b></h4>
                    <hr />
                    <div class="row col-12">
                        <div class="columnas">
                            <label>SLA</label>
                            <asp:TextBox runat="server" Text="ATMs (8 Horas)" ReadOnly="true" CssClass="form-control" />
                        </div>
                        <div class="columnas">
                            <label>% Cumplimiento</label>
                            <asp:TextBox runat="server" ID="TxATMPorcentaje" ReadOnly="true" CssClass="form-control" />
                        </div>
                        <div class="columnas">
                            <label>En Cumplimiento</label>
                            <asp:TextBox runat="server" ID="TxATMCumplimiento" ReadOnly="true" CssClass="form-control" />
                        </div>
                        <div class="columnas">
                            <label>No Cumplimiento</label>
                            <asp:TextBox runat="server" ID="TxATMCumplimientoNo" ReadOnly="true" CssClass="form-control" />
                        </div>
                        <div class="columnas">
                            <label>Total</label>
                            <asp:TextBox runat="server" ID="TxATMTotal" ReadOnly="true" CssClass="form-control" />
                        </div>
                        <div style="width: 60%">
                            <label>Observaciones</label>
                            <asp:TextBox runat="server" ID="TxATMObs" TextMode="MultiLine" Rows="2" CssClass="form-control" />
                        </div>
                        <div style="width: 40%;">
                            <div class="text-center">
                                <asp:Literal Text="" ID="LitATM" runat="server" />
                            </div>
                        </div>
                    </div>
                    <h4 class="card-subtitle m-t-20"><b>ABA </b></h4>
                    <hr />
                    <div class="row col-12">
                        <div class="columnas">
                            <label>SLA</label>
                            <asp:TextBox runat="server" Text="ABAs (16 Horas)" ReadOnly="true" CssClass="form-control" />
                        </div>
                        <div class="columnas">
                            <label>% Cumplimiento</label>
                            <asp:TextBox runat="server" ID="TxABAPorcentaje" ReadOnly="true" CssClass="form-control" />
                        </div>
                        <div class="columnas">
                            <label>En Cumplimiento</label>
                            <asp:TextBox runat="server" ID="TxABACumplimiento" ReadOnly="true" CssClass="form-control" />
                        </div>
                        <div class="columnas">
                            <label>No Cumplimiento</label>
                            <asp:TextBox runat="server" ID="TxABACumplimientoNo" ReadOnly="true" CssClass="form-control" />
                        </div>
                        <div class="columnas">
                            <label>Total</label>
                            <asp:TextBox runat="server" ID="TxABATotal" ReadOnly="true" CssClass="form-control" />
                        </div>
                        <div style="width: 60%">
                            <label>Observaciones</label>
                            <asp:TextBox runat="server" ID="TxABAObs" TextMode="MultiLine" Rows="2" CssClass="form-control" />
                        </div>
                        <div style="width: 40%;">
                            <div class="text-center">
                                <asp:Literal Text="" ID="LitABA" runat="server" />
                            </div>
                        </div>
                    </div>
                    <h4 class="card-subtitle m-t-20"><b>CAJA</b></h4>
                    <hr />
                    <div class="row col-12">
                        <div class="columnas">
                            <label>SLA</label>
                            <asp:TextBox runat="server" Text="CAJA (4 Horas)" ReadOnly="true" CssClass="form-control" />
                        </div>
                        <div class="columnas">
                            <label>% Cumplimiento</label>
                            <asp:TextBox runat="server" ID="TxCajaPorcentaje" ReadOnly="true" CssClass="form-control" />
                        </div>
                        <div class="columnas">
                            <label>En Cumplimiento</label>
                            <asp:TextBox runat="server" ID="TxCajaCumplidas" ReadOnly="true" CssClass="form-control" />
                        </div>
                        <div class="columnas">
                            <label>No Cumplimiento</label>
                            <asp:TextBox runat="server" ID="TxCajaCumplidasNo" ReadOnly="true" CssClass="form-control" />
                        </div>
                        <div class="columnas">
                            <label>Total</label>
                            <asp:TextBox runat="server" ID="TxCajaTotal" ReadOnly="true" CssClass="form-control" />
                        </div>
                        <div style="width: 60%">
                            <label>Observaciones</label>
                            <asp:TextBox runat="server" ID="TxCajaObs" TextMode="MultiLine" Rows="2" CssClass="form-control" />
                        </div>
                        <div style="width: 40%;">
                            <div class="text-center">
                                <asp:Literal Text="" ID="LitCaja" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--KPI--%>
    <div class="card">
        <div class="card-header" role="tab" id="heading3">
            <a class="link" data-toggle="collapse" data-parent="#accordion2" href="#collapse3" aria-expanded="true" aria-controls="collapse3">
                <h4 class="card-title">KPIs Globales</h4>
            </a>
        </div>
        <div id="collapse3" class="collapse show" role="tabpanel" aria-labelledby="headingOne11">
            <div class="card-body">
                <div class="card-body">
                    <div class="row col-12">
                        <div class="columnas">
                            <label>KPIs</label>
                            <asp:TextBox runat="server" Text="Tiempo de Respuesta" ReadOnly="true" CssClass="form-control" />
                        </div>
                        <div class="columnas">
                            <label>% de Cumplimiento</label>
                            <asp:TextBox runat="server" ID="TxKPIPorcentaje" ReadOnly="true" CssClass="form-control" />
                        </div>
                        <div class="columnas">
                            <label>En Cumplimiento</label>
                            <asp:TextBox runat="server" ID="TxKPICumplimiento" ReadOnly="true" CssClass="form-control" />
                        </div>
                        <div class="columnas">
                            <label>No Cumplimiento</label>
                            <asp:TextBox runat="server" ID="TxKPICumplimientoNo" ReadOnly="true" CssClass="form-control" />
                        </div>
                        <div class="columnas">
                            <label>Total</label>
                            <asp:TextBox runat="server" ID="TxKPITotal" ReadOnly="true" CssClass="form-control" />
                        </div>
                        <div style="width: 100%">
                            <label>Observaciones</label>
                            <asp:TextBox runat="server" ID="TxKPIObs" TextMode="MultiLine" Rows="2" CssClass="form-control" />
                        </div>

                        <asp:UpdatePanel runat="server" ID="UPanelKPI">
                            <ContentTemplate>
                                <div runat="server" id="DivKPI" visible="false">
                                    <h4 class="card-subtitle m-t-20"><b>Solicitudes en "No Cumpliento"</b></h4>
                                    <hr />
                                    <div class="table-responsive m-t-20">
                                        <asp:GridView ID="GvKPISolicitudes" runat="server"
                                            CssClass="table table-bordered"
                                            PagerStyle-CssClass="pgr"
                                            HeaderStyle-CssClass="table table-success"
                                            RowStyle-CssClass="rows"
                                            AutoGenerateColumns="false"
                                            AllowPaging="true"
                                            GridLines="None" OnPageIndexChanging="GvKPISolicitudes_PageIndexChanging"
                                            PageSize="10" >
                                            <Columns>
                                                <asp:BoundField DataField="id" HeaderText="Orden" ItemStyle-Width="50"/>
                                                <asp:BoundField DataField="tiempo" HeaderText="Tiempo" ItemStyle-Width="150"/>
                                                <asp:BoundField DataField="problem_type" HeaderText="Categoría 1" ItemStyle-Width="250"/>
                                                <asp:BoundField DataField="problem_sub_type" HeaderText="Categoría 2" ItemStyle-Width="250"/>
                                                <asp:BoundField DataField="third_level_category" HeaderText="Categoría 3" ItemStyle-Width="250"/>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--OS CON RUPTURA--%>
    <div class="card">
        <div class="card-header" role="tab" id="heading4">
            <a class="link" data-toggle="collapse" data-parent="#accordion2" href="#collapse4" aria-expanded="true" aria-controls="collapse4">
                <h4 class="card-title">Ordenes de Servicio con Rupturas</h4>
            </a>
        </div>
        <div id="collapse4" class="collapse show" role="tabpanel" aria-labelledby="headingOne11">
            <div class="card-body">
                <div class="card-body">
                    <asp:Label Text="No hay ordenes de servicio con ruptura" runat="server" ID="LbResRuptura"/>
                    <div class="row col-12">
                        <div runat="server" id="DivRuptura" visible="false">
                            <div class="table-responsive">
                                <asp:UpdatePanel runat="server" ID="UPanelRuptura">
                                    <ContentTemplate>
                                        <asp:GridView ID="GvRuptura" runat="server"
                                            CssClass="table table-bordered"
                                            PagerStyle-CssClass="pgr"
                                            HeaderStyle-CssClass="table-success"
                                            RowStyle-CssClass="rows"
                                            AutoGenerateColumns="false"
                                            AllowPaging="true" OnPageIndexChanging="GvRuptura_PageIndexChanging"
                                            GridLines="None" OnRowDataBound="GvRuptura_RowDataBound"
                                            PageSize="10">
                                            <Columns>
                                                <asp:BoundField DataField="id" HeaderText="Orden" ItemStyle-Width="50"/>
                                                <asp:BoundField DataField="tiempoRespuesta" HeaderText="Respuesta" ItemStyle-Width="50"/>
                                                <asp:BoundField DataField="tiempoAtencion" HeaderText="Atención" ItemStyle-Width="50"/>
                                                <asp:BoundField DataField="responsibility" HeaderText="Responsable" ItemStyle-Width="50"/>
                                                <asp:BoundField DataField="Satisfaccion" HeaderText="Nota" ItemStyle-Width="50"/>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200">
                                                    <HeaderTemplate>Razón</HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:DropDownList runat="server" ID="DDLRazonRuptura" CssClass="form-control"></asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                <HeaderTemplate>Observaciones</HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="TxRupturaObs"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--Esperando Respuesta--%>
    <div class="card">
        <div class="card-header" role="tab" id="heading5">
            <a class="link" data-toggle="collapse" data-parent="#accordion2" href="#collapse5" aria-expanded="true" aria-controls="collapse5">
                <h4 class="card-title">Ordenes de Servicio Esperando Respuesta</h4>
            </a>
        </div>
        <div id="collapse5" class="collapse show" role="tabpanel" aria-labelledby="headingOne11">
            <div class="card-body">
                <div class="card-body">
                    <asp:Label Text="No hay ordenes de servicio esperando respuesta" runat="server" ID="LbResOSER" />
                    <div class="row col-12">
                        <div class="table-responsive">
                            <asp:UpdatePanel runat="server" ID="UPanelOSER">
                                <ContentTemplate>
                                    <asp:GridView ID="GvOSER" runat="server"
                                        CssClass="table table-bordered"
                                        PagerStyle-CssClass="pgr"
                                        HeaderStyle-CssClass="table-success"
                                        RowStyle-CssClass="rows"
                                        AutoGenerateColumns="false"
                                        AllowPaging="true"
                                        GridLines="None" OnRowDataBound="GvOSER_RowDataBound"
                                        PageSize="10" OnPageIndexChanging="GvOSER_PageIndexChanging">
                                        <Columns>
                                            <asp:BoundField DataField="id" HeaderText="Orden" ItemStyle-Width="50" />
                                            <asp:BoundField DataField="tiempoRespuesta" HeaderText="Tiempo" ItemStyle-Width="10" />
                                            <asp:BoundField DataField="responsibility" HeaderText="Responsable" ItemStyle-Width="100" />
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="240">
                                                <HeaderTemplate>Razón</HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:DropDownList runat="server" ID="DDLRazonER" CssClass="form-control"></asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>Observaciones</HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="TxOSERObs"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--RENDIMIENTO--%>
    <div class="card">
        <div class="card-header" role="tab" id="heading6">
            <a class="link" data-toggle="collapse" data-parent="#accordion2" href="#collapse6" aria-expanded="true" aria-controls="collapse6">
                <h4 class="card-title">Rendimiento General</h4>
            </a>
        </div>
        <div id="collapse6" class="collapse show" role="tabpanel" aria-labelledby="headingOne11">
            <div class="card-body">
                <div class="card-body">
                    <div class="row col-12">
                        <div class="table-responsive">
                            <asp:UpdatePanel runat="server" ID="UPanelRendimiento">
                                <ContentTemplate>
                                    <asp:GridView ID="GvRendimiento" runat="server"
                                        CssClass="table table-bordered"
                                        PagerStyle-CssClass="pgr"
                                        HeaderStyle-CssClass="table-success"
                                        RowStyle-CssClass="rows"
                                        AutoGenerateColumns="false"
                                        AllowPaging="true"
                                        GridLines="None"
                                        PageSize="10" OnPageIndexChanging="GvRendimiento_PageIndexChanging">
                                        <Columns>
                                            <asp:BoundField DataField="idUsuario" Visible="false" />
                                            <asp:BoundField DataField="nombre" HeaderText="Nombre" ControlStyle-Width="200" ItemStyle-Width="200%"/>
                                            <asp:BoundField DataField="conocimiento" HeaderText="Cursos"/>
                                            <asp:BoundField DataField="tareas" HeaderText="Tareas"/>
                                            <asp:BoundField DataField="rupturas" HeaderText="Rupturas"/>
                                            <asp:BoundField DataField="sinRupturaProm" HeaderText="Sin Ruptura"/>
                                            <asp:BoundField DataField="satisfaccion" HeaderText="Satisfaccion"/>
                                            <asp:BoundField DataField="produccion" HeaderText="Produccion"/>
                                            <asp:BoundField DataField="eficiencia" HeaderText="Eficiencia"/>
                                            <asp:BoundField  HeaderText="Total"/>
                                            <asp:TemplateField>
                                                <HeaderTemplate>Observaciones</HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="TxRGObs"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card-footer">
                <div class="row m-t-10">
                    <asp:Button Text="Enviar" runat="server" ID="BtnEnviar" CssClass="btn btn-primary" OnClick="BtnEnviar_Click"/>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <script src="/assets/node_modules/jquery.easy-pie-chart/dist/jquery.easypiechart.min.js"></script>
    <script src="/assets/node_modules/jquery.easy-pie-chart/easy-pie-chart.init.js"></script>
</asp:Content>
