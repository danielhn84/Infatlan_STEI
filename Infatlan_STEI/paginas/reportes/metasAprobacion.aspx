<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="metasAprobacion.aspx.cs" Inherits="Infatlan_STEI.paginas.reportes.metasAprobacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <link href="/dist/css/pages/easy-pie-chart.css" rel="stylesheet">
    <style>
        .columnas {
            width: 19%;
            margin: 0% 1% 2% 0%;
        }
    </style>

    <script type="text/javascript">
        function openModal() { $('#ModalConfirmar').modal('show'); }
        function closeModal() { $('#ModalConfirmar').modal('hide'); }
        function openModalUpdate() { $('#ModalConfirmarUpdate').modal('show'); }
        function closeModalUpdate() { $('#ModalConfirmarUpdate').modal('hide'); }
    </script>
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
                    <asp:UpdatePanel runat="server" ID="UPCalls" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="row col-12">
                                <div class="columnas">
                                    <label>Total</label>
                                    <asp:TextBox runat="server" Text="0" ID="TxCallTotal" CssClass="form-control" ReadOnly="true"/>
                                </div>
                                <div class="columnas">
                                    <label>Atendidas</label>
                                    <asp:TextBox runat="server" Text="0" ID="TxCallAtendidas" CssClass="form-control" ReadOnly="true" />
                                </div>
                                <div class="columnas">
                                    <label>No Atendidas</label>
                                    <asp:TextBox runat="server" Text="0" ID="TxCallAtendidasNo" CssClass="form-control" ReadOnly="true" />
                                </div>
                                <div class="columnas">
                                    <label>% Atendidas</label>
                                    <asp:TextBox runat="server" Text="0" ID="TxCallPorcentajeSi" CssClass="form-control" ReadOnly="true" />
                                </div>
                                <div class="columnas">
                                    <label>% No Atendidas</label>
                                    <asp:TextBox runat="server" Text="0" ID="TxCallPorcentajeNo" CssClass="form-control" ReadOnly="true"/>
                                </div>
                                <div style="width: 60%">
                                    <label>Observaciones</label>
                                    <asp:TextBox runat="server" ID="TxCallObs" TextMode="MultiLine" Rows="2" CssClass="form-control" ReadOnly="true"/>
                                </div>
                                <div style="width: 40%">
                                    <div class="text-center" runat="server" visible="true" id="DivCallGrafic">
                                        <div runat="server" id="CCall" class="chart easy-pie-chart-4" data-percent="0"><span class="percent"></span></div>
                                    </div>
                                </div>
                                <div style="display:none">
                                    <input type="text" readonly id="TxGraf1" value="0" runat="server" />
                                    <input type="text" readonly id="TxGraf2" value="0" runat="server" />
                                    <input type="text" readonly id="TxGraf3" value="0" runat="server" />
                                    <input type="text" readonly id="TxGraf4" value="0" runat="server" />
                                    <input type="text" readonly id="TxGraf5" value="0" runat="server" />
                                    <input type="text" readonly id="TxGraf6" value="0" runat="server" />
                                    <input type="text" readonly id="TxGraf7" value="0" runat="server" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
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
                            <asp:TextBox runat="server" ID="TxATMObs" TextMode="MultiLine" Rows="2" ReadOnly="true" CssClass="form-control" />
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
                            <asp:TextBox runat="server" ID="TxABAObs" TextMode="MultiLine" Rows="2" ReadOnly="true" CssClass="form-control" />
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
                            <asp:TextBox runat="server" ID="TxCajaObs" TextMode="MultiLine" Rows="2" ReadOnly="true" CssClass="form-control" />
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
                            <asp:TextBox runat="server" ID="TxKPIObs" TextMode="MultiLine" Rows="2" ReadOnly="true" CssClass="form-control" />
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
                                                <asp:BoundField DataField="orden" HeaderText="Orden" ItemStyle-Width="50"/>
                                                <asp:BoundField DataField="tiempo" HeaderText="Tiempo" ItemStyle-Width="150"/>
                                                <asp:BoundField DataField="cat1" HeaderText="Categoría 1" ItemStyle-Width="250"/>
                                                <asp:BoundField DataField="cat2" HeaderText="Categoría 2" ItemStyle-Width="250"/>
                                                <asp:BoundField DataField="cat3" HeaderText="Categoría 3" ItemStyle-Width="250"/>
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
                                            GridLines="None"
                                            PageSize="10">
                                            <Columns>
                                                <asp:BoundField DataField="orden" HeaderText="Orden" ItemStyle-Width="50"/>
                                                <asp:BoundField DataField="tiempoRespuesta" HeaderText="Respuesta" ItemStyle-Width="50"/>
                                                <asp:BoundField DataField="tiempoAtencion" HeaderText="Atención" ItemStyle-Width="50"/>
                                                <asp:BoundField DataField="responsable" HeaderText="Responsable" ItemStyle-Width="50"/>
                                                <asp:BoundField DataField="satisfaccionCliente" HeaderText="Nota" ItemStyle-Width="50"/>
                                                <asp:BoundField DataField="razon" HeaderText="Razon"/>
                                                <asp:BoundField DataField="comentario" HeaderText="Observaciones"/>
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
                                            <asp:BoundField DataField="orden" HeaderText="Orden" ItemStyle-Width="50" />
                                            <asp:BoundField DataField="tiempo" HeaderText="Tiempo" ItemStyle-Width="10" />
                                            <asp:BoundField DataField="responsable" HeaderText="Responsable" ItemStyle-Width="100" />
                                            <asp:BoundField DataField="razon" HeaderText="Razon"/>
                                            <asp:BoundField DataField="comentario" HeaderText="Obs"/>
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

    <%--Insatisfacciones--%>
    <div class="card">
        <div class="card-header" role="tab" id="heading7">
            <a class="link" data-toggle="collapse" data-parent="#accordion2" href="#collapse7" aria-expanded="true" aria-controls="collapse7">
                <h4 class="card-title">Ordenes de Servicio con Baja Calificación</h4>
            </a>
        </div>
        <div id="collapse7" class="collapse show" role="tabpanel" aria-labelledby="headingOne11">
            <div class="card-body">
                <div class="card-body">
                    <asp:Label Text="No hay ordenes de servicio con calificacion menor a 3" runat="server" ID="LbInsatisfaccion" />
                    <div class="row col-12">
                        <div class="table-responsive">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                <ContentTemplate>
                                    <asp:GridView ID="GvInsatisfacciones" runat="server"
                                        CssClass="table table-bordered"
                                        PagerStyle-CssClass="pgr"
                                        HeaderStyle-CssClass="table-success"
                                        RowStyle-CssClass="rows"
                                        AutoGenerateColumns="false"
                                        AllowPaging="true"
                                        GridLines="None" 
                                        PageSize="10" OnPageIndexChanging="GvInsatisfacciones_PageIndexChanging">
                                        <Columns>
                                            <asp:BoundField DataField="orden" HeaderText="Orden" ItemStyle-Width="50" />
                                            <asp:BoundField DataField="calificacion" HeaderText="Calificacion" ItemStyle-Width="10" />
                                            <asp:BoundField DataField="responsable" HeaderText="Responsable" ItemStyle-Width="100" />
                                            <asp:BoundField DataField="comentario" HeaderText="Comentario" />
                                            <asp:BoundField DataField="observaciones" HeaderText="Observaciones"/>
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
                    <asp:UpdatePanel runat="server" ID="UPanelRendimientoGrafic" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="row col-12" runat="server" id="DivGraficos" visible="false">
                                <div class="card-body col-4">
                                    <h6 class="card-title">Solicitudes Cerradas</h6>
                                    <div class="row form-group">
                                        <div class="col-6">
                                            <span class="label label-info"></span>
                                            <label style="font-size:smaller">Sin ruptura</label>
                                            <br />
                                            <span class="label label-warning"></span>
                                            <label style="font-size:smaller">Con ruptura</label>
                                        </div>
                                        <div class="col-6">
                                            <div id="sparkline1" class="text-center"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="card-body col-4">
                                    <h6 class="card-title">Eficiencia</h6>
                                    <div class="row form-group">
                                        <div class="col-6">
                                            <span class="label label-info"></span>
                                            <label style="font-size:smaller">Sin ruptura</label>
                                            <br />
                                            <span class="label label-warning"></span>
                                            <label style="font-size:smaller">Con ruptura</label>
                                        </div>
                                        <div class="col-6">
                                            <div id="sparkline2" class="text-center"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="card-body col-4">
                                    <h6 class="card-title">Productividad</h6>
                                    <div class="row form-group">
                                        <div class="col-6">
                                            <span class="label label-info"></span>
                                            <label style="font-size:smaller">Real Tareas</label>
                                            <br />
                                            <span class="label label-warning"></span>
                                            <label style="font-size:smaller">Transporte</label>
                                            <br />
                                            <span class="label label-danger"></span>
                                            <label style="font-size:smaller">No Procesadas</label>
                                        </div>
                                        <div class="col-6">
                                            <div id="sparkline3" class="text-center"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    
                    <div class="row col-12 m-t-20">
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
                                            <asp:BoundField DataField="nombre" HeaderText="Nombre" ControlStyle-Width="200" ItemStyle-Width="200"/>
                                            <asp:BoundField DataField="conocimiento" HeaderText="Cursos"/>
                                            <asp:BoundField DataField="tareas" HeaderText="Tareas"/>
                                            <asp:BoundField DataField="rupturas" HeaderText="Rupturas"/>
                                            <asp:BoundField DataField="noRupturas" HeaderText="Sin Ruptura"/>
                                            <asp:BoundField DataField="satisfaccion" HeaderText="Satisfaccion"/>
                                            <asp:BoundField DataField="produccion" HeaderText="Produccion"/>
                                            <asp:BoundField DataField="eficiencia" HeaderText="Eficiencia"/>
                                            <asp:BoundField DataField="total" HeaderText="Total"/>
                                            <asp:BoundField DataField="comentario" HeaderText="Comentario"/>
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
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <asp:Button Text="Aprobar" runat="server" ID="BtnAprobar" CssClass="btn btn-success" OnClick="BtnAprobar_Click"/>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <%--MODAL DE CONFIRMACION--%>
    <div class="modal fade" id="ModalConfirmar" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="ModalLabelConfirmar">
                        <b><asp:Label runat="server" ID="LbTitulo" Text="Aprobacion de Reporte" CssClass="col-form-label"></asp:Label></b>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row col-12">
                        <label class="col-12">Accion</label>
                        <asp:DropDownList runat="server" CssClass="form-control" ID="DDLAccion">
                            <asp:ListItem Text="Aprobar" Value="0" />
                            <asp:ListItem Text="Rechazar" Value="1" />
                        </asp:DropDownList>
                    </div>
                    
                    <div class="row col-12 m-t-10">
                        <label class="col-12">Comentario</label>
                        <asp:TextBox runat="server" ID="TxComentario" TextMode="MultiLine" Rows="3" CssClass="form-control col-12"></asp:TextBox>
                    </div>
                    
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="row m-t-20">
                                <div class="col-12" runat="server" id="DivMensaje" visible="false" style="display: flex; background-color:tomato; justify-content:center">
                                    <asp:Label runat="server" CssClass="col-form-label text-white" ID="LbMensaje"></asp:Label>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                            <asp:Button ID="BtnConfirmar" runat="server" Text="Aceptar" class="btn btn-info" OnClick="BtnConfirmar_Click"/>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <script src="/assets/node_modules/jquery.easy-pie-chart/dist/jquery.easypiechart.min.js"></script>
    <script src="/assets/node_modules/jquery.easy-pie-chart/easy-pie-chart.init.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var sparklineLogin = function () {
                
                var valor1 = document.getElementById('<%=TxGraf1.ClientID%>').value;
                var valor2 = document.getElementById('<%=TxGraf2.ClientID%>').value;
                var valor3 = document.getElementById('<%=TxGraf3.ClientID%>').value;
                var valor4 = document.getElementById('<%=TxGraf4.ClientID%>').value;
                var valor5 = document.getElementById('<%=TxGraf5.ClientID%>').value;
                var valor6 = document.getElementById('<%=TxGraf6.ClientID%>').value;
                var valor7 = document.getElementById('<%=TxGraf7.ClientID%>').value;

                $('#sparkline1').sparkline([valor1, valor2], {
                    type: 'pie',
                    height: '100',
                    resize: true,
                    sliceColors: ['#f0ad4e', '#5bc0de']
                });
                
                $('#sparkline2').sparkline([valor3, valor4], {
                    type: 'pie',
                    height: '100',
                    resize: true,
                    sliceColors: ['#f0ad4e', '#5bc0de']
                });
                $('#sparkline3').sparkline([valor5, valor6, valor7], {
                    type: 'pie',
                    height: '100',
                    resize: true,
                    sliceColors: ['#f0ad4e', '#5bc0de', '#d9534f']
                });
                
            }
            var sparkResize;
            $(window).resize(function (e) {
                clearTimeout(sparkResize);
                sparkResize = setTimeout(sparklineLogin, 500);
            });
            sparklineLogin();

        });
    </script>

    <script src="/assets/node_modules/sparkline/jquery.sparkline.min.js"></script>
</asp:Content>
