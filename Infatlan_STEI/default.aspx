<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Infatlan_STEI._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div class="row page-titles">
        <div class="col-md-5 align-self-center">
            <h4 class="text-themecolor">Dashboard</h4>
        </div>
        <div class="col-md-7 align-self-center text-right">
            <div class="d-flex justify-content-end align-items-center">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Inicio</a></li>
                    <li class="breadcrumb-item active">Dashboard</li>
                </ol>
                <button type="button" class="btn btn-primary d-none d-lg-block m-l-15" onclick="$('#ModalBugs').modal('show');"><i class="fa fa-bug mr-2"></i>Bugs</button>
            </div>
        </div>
    </div>

    <div class="card-group">
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="d-flex no-block align-items-center">
                            <div>
                                <h3><i class="icon-screen-desktop"></i></h3>
                                <p class="text-muted">Mantenimientos Agencias</p>
                            </div>
                            <div class="ml-auto">
                                <h2 class="counter text-primary">0</h2>
                            </div>
                        </div>
                    </div>
                    <div class="col-12">
                        <div class="progress">
                            <div class="progress-bar bg-primary" role="progressbar" style="width: 85%; height: 6px;" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="d-flex no-block align-items-center">
                            <div>
                                <h3><i class="icon-note"></i></h3>
                                <p class="text-muted">Manteniminetos ATM</p>
                            </div>
                            <div class="ml-auto">
                                <h2 class="counter text-cyan">0</h2>
                            </div>
                        </div>
                    </div>
                    <div class="col-12">
                        <div class="progress">
                            <div class="progress-bar bg-cyan" role="progressbar" style="width: 85%; height: 6px;" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="d-flex no-block align-items-center">
                            <div>
                                <h3><i class="icon-doc"></i></h3>
                                <p class="text-muted">Inventarios</p>
                            </div>
                            <div class="ml-auto">
                                <h2 class="counter text-purple">0</h2>
                            </div>
                        </div>
                    </div>
                    <div class="col-12">
                        <div class="progress">
                            <div class="progress-bar bg-purple" role="progressbar" style="width: 85%; height: 6px;" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="d-flex no-block align-items-center">
                            <div>
                                <h3><i class="icon-bag"></i></h3>
                                <p class="text-muted">Ordenes</p>
                            </div>
                            <div class="ml-auto">
                                <h2 class="counter text-success">0</h2>
                            </div>
                        </div>
                    </div>
                    <div class="col-12">
                        <div class="progress">
                            <div class="progress-bar bg-success" role="progressbar" style="width: 85%; height: 6px;" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="card">
        <div class="card-body">
            <nav>
                <div class="nav nav-pills" id="nav-tab" role="tablist">
                    <a class="nav-item nav-link active" id="agencias" data-toggle="tab" href="#navAgencias" role="tab" aria-controls="nav-profile" aria-selected="false"><i class="icon-home"></i>Agencias</a>
                    <a class="nav-item nav-link" id="atm" data-toggle="tab" href="#navATM" role="tab" aria-controls="nav-profile" aria-selected="false"><i style="margin-right: 5px" class="icon-screen-desktop"></i>ATM</a>
                    <a class="nav-item nav-link" id="cableado" data-toggle="tab" href="#navCableado" role="tab" aria-controls="nav-profile" aria-selected="false"><i style="margin-right: 5px" class="ti-plug"></i>Cableado</a>
                    <a class="nav-item nav-link" id="invenrario" data-toggle="tab" href="#navInventario" role="tab" aria-controls="nav-profile" aria-selected="false"><i style="margin-right: 5px" class="ti-package"></i>Inventarios</a>
                </div>
            </nav>
            <div class="tab-content" id="nav-tabContent">
                <div class="tab-pane fade show active" id="navAgencias" role="tabpanel" aria-labelledby="nav-cargar-tab">
                    <div class="card-group mt-3">
                        <div class="card">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="d-flex no-block align-items-center">
                                            <div>
                                                <h3><i class="icon-home"></i></h3>
                                                <p class="text-muted">Mantenimientos Agencias</p>
                                            </div>
                                            <div class="ml-auto">
                                                <h2 class="counter text-primary">0</h2>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12">
                                        <div class="progress">
                                            <div class="progress-bar bg-primary" role="progressbar" style="width: 85%; height: 6px;" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="tab-pane fade" id="navATM" role="tabpanel" aria-labelledby="nav-cargar-tab">
                    <div class="card-group mt-3">
                        <div class="card">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="d-flex no-block align-items-center">
                                            <div>
                                                <h3><i class="icon-screen-desktop"></i></h3>
                                                <p class="text-muted">Manteniminetos ATM</p>
                                            </div>
                                            <div class="ml-auto">
                                                <h2 class="counter text-cyan">0</h2>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12">
                                        <div class="progress">
                                            <div class="progress-bar bg-cyan" role="progressbar" style="width: 85%; height: 6px;" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="tab-pane fade" id="navCableado" role="tabpanel" aria-labelledby="nav-cargar-tab">
                    <div class="card-group">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="d-flex no-block align-items-center">
                                        <div>
                                            <h4 class="text-muted">Fecha</h4>
                                            <div class="ml-auto">
                                                <i class="icon-lg mr-3 text-primary"></i>
                                                <h2 class="counter text-cyan">
                                                    <asp:Label runat="server" ID="LbFechaDashboard"></asp:Label>
                                                </h2>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="d-flex no-block align-items-center">
                                        <div>
                                            <h3><i class="icon-chart"></i></h3>
                                            <p class="text-muted">
                                                <asp:Label ID="txtCreadas" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>
                                        <div class="ml-auto">
                                            <h2 class="counter text-primary">
                                                <asp:Label runat="server" ID="lbCreadas"></asp:Label>
                                            </h2>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="progress">
                                        <div class="progress-bar bg-primary" id="CSSCotizacion" runat="server" role="progressbar" style="width: 20%; height: 6px;" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="d-flex no-block align-items-center">
                                        <div>
                                            <h3><i class="icon-note"></i></h3>
                                            <p class="text-muted">
                                                <asp:Label ID="txtPendientes" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>
                                        <div class="ml-auto">
                                            <h2 class="counter text-cyan">
                                                <asp:Label runat="server" ID="lbPendientes"></asp:Label>
                                            </h2>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="progress">
                                        <div class="progress-bar bg-cyan" role="progressbar" style="width: 25%; height: 6px;" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>

                <div class="tab-pane fade" id="navInventario" role="tabpanel" aria-labelledby="nav-cargar-tab">
                    <div class="row">
                        <div class="col-lg-3 col-md-6">
                            <div class="card-body">
                                <h5 class="card-title">TOTAL VISIT</h5>
                                <div class="d-flex no-block align-items-center m-t-20 m-b-20">
                                    <div id="sparklinedash"></div>
                                    <div class="ml-auto">
                                        <h2 class="text-success"><i class="ti-arrow-up"></i> <span class="counter">8659</span></h2>
                                    </div>
                                </div>
                            </div>
                            <div id="sparkline9" class="sparkchart"></div>
                        </div>
                        <div class="col-lg-3 col-md-6">
                            <div class="card-body">
                                <h5 class="card-title">TOTAL PAGE VIEWS</h5>
                                <div class="d-flex no-block align-items-center m-t-20 m-b-20">
                                    <div id="sparklinedash2"></div>
                                    <div class="ml-auto">
                                        <h2 class="text-purple"><i class="ti-arrow-up"></i> <span class="counter">7469</span></h2>
                                    </div>
                                </div>
                            </div>
                            <div id="sparkline10" class="sparkchart"></div>
                        </div>
                        <div class="col-lg-3 col-md-6">
                            <div class="card-body">
                                <h5 class="card-title">UNIQUE VISITOR</h5>
                                <div class="d-flex no-block align-items-center m-t-20 m-b-20">
                                    <div id="sparklinedash3"></div>
                                    <div class="ml-auto">
                                        <h2 class="text-info"><i class="ti-arrow-up"></i> <span class="counter">6011</span></h2>
                                    </div>
                                </div>
                            </div>
                            <div id="sparkline11" class="sparkchart"></div>
                        </div>
                        <div class="col-lg-3 col-md-6">
                            <div class="card-body">
                                <h5 class="card-title">BOUNCE RATE</h5>
                                <div class="d-flex no-block align-items-center m-t-20 m-b-20">
                                    <div id="sparklinedash4"></div>
                                    <div class="ml-auto">
                                        <h2 class="text-danger"><i class="ti-arrow-down"></i> <span class="counter">18%</span></h2>
                                    </div>
                                </div>
                            </div>
                            <div id="sparkline12" class="sparkchart"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--MODAL BUGS--%>
    <div class="modal fade" id="ModalBugs" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="ModalLabel">Enviar Incidente
                    </h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel28" runat="server">
                        <ContentTemplate>
                            <div class="row col-12">
                                <label class="col-3 col-form-label">Tipo</label>
                                <div class="col-9">
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="DDLTipo">
                                        <asp:ListItem Value="0" Text="Seleccione"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Acceso a STEI"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Agencias"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="ATM"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="Cableado"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="Inventarios"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="row col-12 mt-3">
                                <label class="col-3 col-form-label">Mensaje</label>
                                <div class="col-9">
                                    <asp:TextBox runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control" ID="TxMensaje"></asp:TextBox>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel29" runat="server">
                        <ContentTemplate>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                            <asp:Button ID="BtnEnviarBug" runat="server" Text="Aceptar" class="btn btn-success" OnClick="BtnEnviarBug_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
