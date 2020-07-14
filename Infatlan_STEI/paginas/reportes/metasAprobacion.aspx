<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="metasAprobacion.aspx.cs" Inherits="Infatlan_STEI.paginas.reportes.metasAprobacion" %>
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
                    <li class="breadcrumb-item active">Aprobación</li>
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
                                            AllowPaging="true" 
                                            GridLines="None" OnPageIndexChanging="GvRuptura_PageIndexChanging"
                                            PageSize="10">
                                            <Columns>
                                                <asp:BoundField HeaderText="Orden" ItemStyle-Width="50"/>
                                                <asp:BoundField HeaderText="Respuesta" ItemStyle-Width="50"/>
                                                <asp:BoundField HeaderText="Atención" ItemStyle-Width="50"/>
                                                <asp:BoundField HeaderText="Responsable" ItemStyle-Width="50"/>
                                                <asp:BoundField HeaderText="Nota" ItemStyle-Width="50"/>
                                                <asp:BoundField HeaderText="Razon" ItemStyle-Width="150"/>
                                                <asp:BoundField HeaderText="Obs"/>
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
                                        GridLines="None"
                                        PageSize="10" OnPageIndexChanging="GvOSER_PageIndexChanging">
                                        <Columns>
                                            <asp:BoundField  HeaderText="Orden" ItemStyle-Width="50" />
                                            <asp:BoundField  HeaderText="Tiempo" ItemStyle-Width="10" />
                                            <asp:BoundField  HeaderText="Responsable" ItemStyle-Width="100" />
                                            <asp:BoundField  HeaderText="Razón" ItemStyle-Width="100" />
                                            <asp:BoundField  HeaderText="Obs" ItemStyle-Width="100" />
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

    <%--GENERAL--%>
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
                                            <asp:BoundField  Visible="false" />
                                            <asp:BoundField  HeaderText="Nombre" HeaderStyle-Width="100"/>
                                            <asp:BoundField  HeaderText="Cursos"/>
                                            <asp:BoundField  HeaderText="Tareas"/>
                                            <asp:BoundField  HeaderText="Rupturas"/>
                                            <asp:BoundField  HeaderText="%Sin Respuesta"/>
                                            <asp:BoundField  HeaderText="%Satisfaccion"/>
                                            <asp:BoundField  HeaderText="%Produccion"/>
                                            <asp:BoundField  HeaderText="%Eficiencia"/>
                                            <asp:BoundField  HeaderText="Total"/>
                                            <asp:BoundField  HeaderText="Observaciones"/>
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>

                        <div class="row m-t-10">
                            <asp:Button Text="Aprobar" runat="server" ID="BtnAprobar" CssClass="btn btn-primary" OnClick="BtnAprobar_Click"/>
                            <asp:Button Text="Rechazar" runat="server" ID="BtnRechazar" CssClass="btn btn-danger"/>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
