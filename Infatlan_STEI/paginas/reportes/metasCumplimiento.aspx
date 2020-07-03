<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="metasCumplimiento.aspx.cs" Inherits="Infatlan_STEI.paginas.reportes.metasCumplimiento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
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

    <%--MDP--%>
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
                            <asp:TextBox runat="server" ID="TextBox2" ReadOnly="true" CssClass="form-control" />
                        </div>
                        <div class="columnas">
                            <label>En Cumplimiento</label>
                            <asp:TextBox runat="server" ID="TextBox3" ReadOnly="true" CssClass="form-control" />
                        </div>
                        <div class="columnas">
                            <label>En No Cumplimiento</label>
                            <asp:TextBox runat="server" ID="TextBox4" ReadOnly="true" CssClass="form-control" />
                        </div>
                        <div class="columnas">
                            <label>Total</label>
                            <asp:TextBox runat="server" ID="TextBox5" ReadOnly="true" CssClass="form-control" />
                        </div>
                        <div style="width: 60%">
                            <label>Observaciones</label>
                            <asp:TextBox runat="server" ID="TextBox6" TextMode="MultiLine" Rows="2" CssClass="form-control" />
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
                            <asp:TextBox runat="server" ID="TextBox1" ReadOnly="true" CssClass="form-control" />
                        </div>
                        <div class="columnas">
                            <label>En Cumplimiento</label>
                            <asp:TextBox runat="server" ID="TextBox25" ReadOnly="true" CssClass="form-control" />
                        </div>
                        <div class="columnas">
                            <label>En No Cumplimiento</label>
                            <asp:TextBox runat="server" ID="TextBox26" ReadOnly="true" CssClass="form-control" />
                        </div>
                        <div class="columnas">
                            <label>Total</label>
                            <asp:TextBox runat="server" ID="TextBox27" ReadOnly="true" CssClass="form-control" />
                        </div>
                        <div style="width: 60%">
                            <label>Observaciones</label>
                            <asp:TextBox runat="server" ID="TextBox28" TextMode="MultiLine" Rows="2" CssClass="form-control" />
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
                            <asp:TextBox runat="server" ID="TextBox7" ReadOnly="true" CssClass="form-control" />
                        </div>
                        <div class="columnas">
                            <label>En Cumplimiento</label>
                            <asp:TextBox runat="server" ID="TextBox8" ReadOnly="true" CssClass="form-control" />
                        </div>
                        <div class="columnas">
                            <label>En No Cumplimiento</label>
                            <asp:TextBox runat="server" ID="TextBox9" ReadOnly="true" CssClass="form-control" />
                        </div>
                        <div class="columnas">
                            <label>Total</label>
                            <asp:TextBox runat="server" ID="TextBox10" ReadOnly="true" CssClass="form-control" />
                        </div>
                        <div style="width: 60%">
                            <label>Observaciones</label>
                            <asp:TextBox runat="server" ID="TextBox11" TextMode="MultiLine" Rows="2" CssClass="form-control" />
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
                            <asp:TextBox runat="server" ID="TextBox24" TextMode="MultiLine" Rows="2" CssClass="form-control" />
                        </div>
                    </div>
                </div>
                <h4 class="m-t-20">Solicitudes en "No Cumpliento"</h4>
                <hr />
                <div class="table-responsive m-t-20">
                    <asp:GridView ID="GVBusqueda" runat="server"
                        CssClass="table table-bordered"
                        PagerStyle-CssClass="pgr"
                        HeaderStyle-CssClass="table"
                        RowStyle-CssClass="rows"
                        AutoGenerateColumns="false"
                        AllowPaging="true"
                        GridLines="None" 
                        PageSize="10" >
                        <Columns>
                            <asp:BoundField DataField="idOrden" HeaderText="Orden"/>
                            <asp:BoundField DataField="" HeaderText="Orden"/>
                            <asp:BoundField DataField="" HeaderText="Tiempo"/>
                            <asp:BoundField DataField="" HeaderText="Categoría 1"/>
                            <asp:BoundField DataField="" HeaderText="Categoría 2"/>
                            <asp:BoundField DataField="" HeaderText="Categoría 3"/>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="BtnEditar" runat="server" class="btn btn-info" CommandArgument='<%# Eval("idOrden") %>' CommandName="EditarMotivo">
                                        <i class="icon-pencil" ></i>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>

    <%--OSR--%>
    <div class="card">
        <div class="card-header" role="tab" id="heading4">
            <a class="link" data-toggle="collapse" data-parent="#accordion2" href="#collapse4" aria-expanded="true" aria-controls="collapse4">
                <h4 class="card-title">Ordenes de Servicio con Rupturas</h4>
            </a>
        </div>
        <div id="collapse4" class="collapse show" role="tabpanel" aria-labelledby="headingOne11">
            <div class="card-body">
                <div class="card-body">
                    <asp:Label Text="No hay ordenes de servicio con ruptura" runat="server" />
                    <div class="row col-12">
                        <div class="table-responsive m-t-20">
                            <asp:GridView ID="GridView1" runat="server"
                                CssClass="table table-bordered"
                                PagerStyle-CssClass="pgr"
                                HeaderStyle-CssClass="table"
                                RowStyle-CssClass="rows"
                                AutoGenerateColumns="false"
                                AllowPaging="true"
                                GridLines="None" 
                                PageSize="10">
                                <Columns>
                                    <asp:BoundField  HeaderText="Orden"/>
                                    <asp:BoundField  HeaderText="Tiempo"/>
                                    <asp:BoundField  HeaderText="Categoría 1"/>
                                    <asp:BoundField  HeaderText="Categoría 2"/>
                                    <asp:BoundField  HeaderText="Categoría 3"/>
                                    <%--<asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="BtnEditar" runat="server" class="btn btn-info" CommandArgument='<%# Eval("idOrden") %>' CommandName="EditarMotivo">
                                                <i class="icon-pencil" ></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                </Columns>
                            </asp:GridView>
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
                            <asp:GridView ID="GvOSER" runat="server"
                                CssClass="table table-bordered"
                                PagerStyle-CssClass="pgr"
                                HeaderStyle-CssClass="table-success"
                                RowStyle-CssClass="rows"
                                AutoGenerateColumns="false"
                                AllowPaging="true"
                                GridLines="None" OnRowDataBound="GvOSER_RowDataBound"
                                PageSize="10">
                                <Columns>
                                    <asp:BoundField DataField="id" HeaderText="Orden" ItemStyle-Width="50"/>
                                    <asp:BoundField DataField="tiempoRespuesta" HeaderText="Tiempo" ItemStyle-Width="10"/>
                                    <asp:BoundField DataField="responsibility" HeaderText="Responsable" ItemStyle-Width="100"/>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="240">
                                        <HeaderTemplate>Razón</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:DropDownList runat="server" ID="DDLRazonER" CssClass="form-control"></asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderTemplate>Observaciones</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" CssClass="form-control"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
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
                        <asp:Button Text="Enviar" runat="server" ID="BtnEnviar" CssClass="btn btn-primary" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <script src="/assets/node_modules/jquery.easy-pie-chart/dist/jquery.easypiechart.min.js"></script>
    <script src="/assets/node_modules/jquery.easy-pie-chart/easy-pie-chart.init.js"></script>
</asp:Content>
