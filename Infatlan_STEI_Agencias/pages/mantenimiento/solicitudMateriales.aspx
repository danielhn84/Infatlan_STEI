﻿<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="solicitudMateriales.aspx.cs" Inherits="Infatlan_STEI_Agencias.pages.mantenimiento.solicitudMateriales" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <script type="text/javascript">
        var updateProgress = null;
        function postbackButtonClick() {
            updateProgress = $find("<%= UpdateProgress1.ClientID %>");
            window.setTimeout("updateProgress.set_visible(true)", updateProgress.get_displayAfter());
            return true;
        }

        function openModalMaterial() { $('#modalModificarAgencia').modal('show'); }
        function closeModalMaterial() { $('#modalModificarAgencia').modal('hide'); }
    </script>

    <link href="../../assets/node_module/select2/dist/css/select2.min.css" rel="stylesheet" type="text/css"  />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #ffffff; opacity: 0.7; margin: 0;">
                <span style="display: inline-block; height: 100%; vertical-align: middle;"></span>
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="/assets/images/loading.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="display: inline-block; vertical-align: middle;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <div class="row page-titles">
        <div class="col-md-7 align-self-center">
            <h2 class="text-themecolor">Material que saldrá del almacén</h2>
            <div class="mr-md-3 mr-xl-5">
                <p class="mb-md-0">Soporte Técnico y Comunicaciones</p>
            </div>
        </div>
    </div>

    <div class="card">
        <div class="row" id="DivBusqueda" runat="server">
            <div class="col-12 grid-margin stretch-card">
                <div class="card">
                    <div class="card-body">
                        <h4 class="card-title">Solicitudes Pendientes</h4>
                        <p>Materiales pendientes de solicitar.</p>
                        <div class="col-md-12">
                            <div class="form-group row">
                                <div class="col-sm-12">
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <ContentTemplate>
                                            <div class="row p-t-20">
                                                <div class="col-md-1">
                                                    <label class="control-label   text-danger">*</label><label class="control-label">Buscar:</label></label>                                      
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="TxBuscarAgencia" runat="server" placeholder="Búsqueda por agencia o codigo, luego presione Enter..." class="form-control" AutoPostBack="true"></asp:TextBox>
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

        <div class="card">
            <div class="row col-12">
                <div class="col-12 grid-margin stretch-card">
                    <div class="table-responsive">
                        <asp:UpdatePanel runat="server" ID="UPGvBusqueda">
                            <ContentTemplate>
                                <asp:GridView ID="GVBusqueda" runat="server"
                                    CssClass="table table-bordered"
                                    PagerStyle-CssClass="pgr"
                                    HeaderStyle-CssClass="table" HeaderStyle-HorizontalAlign="center"
                                    RowStyle-CssClass="rows"
                                    AutoGenerateColumns="false"
                                    AllowPaging="true"
                                    GridLines="None"
                                    PageSize="10" OnRowCommand="GVBusqueda_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Acción" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LBAprobar" class="btn btn-success mr-2" runat="server" CommandName="Aprobar" CommandArgument='<%# Eval("id_Mantenimiento") %>'>
                                                        <i class="icon-check" ></i> 
                                                </asp:LinkButton>

                                                <asp:LinkButton ID="LBCancelar" class="btn btn-primary mr-2" runat="server" CommandName="Cancelar" CommandArgument='<%# Eval("id_Mantenimiento") %>'>
                                                        <i class="icon-close" ></i> 
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="13%" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="id_Mantenimiento" HeaderText="Id" />
                                        <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                                        <asp:BoundField DataField="Hr_Inicio" HeaderText="Hora Inicio" />
                                        <asp:BoundField DataField="Hr_Fin" HeaderText="Hora Fin" />
                                        <asp:BoundField DataField="Lugar" HeaderText="Lugar" />
                                        <asp:BoundField DataField="Cod_Agencia" HeaderText="Codigo Agencia" />
                                        <asp:BoundField DataField="Responsable" HeaderText="Responsable" />
                                        <asp:BoundField DataField="Area" HeaderText="Area" />
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                    <div class="row p-t-20 col-md-12">
                        <div class="col-md-4 " style="margin-left: auto; margin-right: auto">
                            <%-- <a href="../../default.aspx"" class="btn  btn-block btn-primary">Volver</a>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--INICIO MODAL ENVIAR LV--%>
    <div class="modal bs-example-modal-lg" id="modalModificarAgencia" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-xl" role="document">
            <div class="modal-content" style="width: 1300px; top: 320px; left: 50%; transform: translate(-50%, -50%);">
                <div class="modal-header bg-dark">

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <h3 class="modal-title" style="color: white">
                                <asp:Label ID="lbTitulo" runat="server" Text="" Style="margin-left: auto; margin-right: auto"></asp:Label></h3>
                        </ContentTemplate>
                    </asp:UpdatePanel>


                    <button type="button" class="close" style="color: white" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                        <ContentTemplate>

                            <div class="col-md-12" runat="server">
                                <div class="form-group row">
                                    <label class="col-md-1 col-form-label">Id Mant:</label>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="TxIdMant" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1  ">
                                        <label class="col-form-label">Agencia:</label>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="TxAgencia" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1  ">
                                        <label class="col-form-label">Fecha:</label>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="TxFecha" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12" runat="server">
                                <div class="form-group row">
                                    <div class="col-md-1  ">
                                        <label class="col-form-label">Area:</label>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="TxArea" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1  ">
                                        <label class="col-form-label">Ubicación:</label>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="TxUbicacion" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <label class="col-form-label">Conductor:</label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:RadioButtonList ID="RbConductor" RepeatDirection="Horizontal" Width="90px" runat="server" AutoPostBack="True" Enabled="false">
                                            <asp:ListItem Value="1">Si</asp:ListItem>
                                            <asp:ListItem Value="0">No</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>

                            <hr>
                            <hr>

                            <div class="col-md-12" runat="server">
                                <div class="form-group row">
                                    <div class="col-md-2  ">
                                        <label class="control-label text-danger">*</label><label class="col-form-label">Artículo del almacén:</label>
                                    </div>

                                    <div class="col-md-6">
                                        <asp:DropDownList runat="server" ID="DDLArticulo" CssClass="select2 form-control custom-select" Style="width: 100%" OnSelectedIndexChanged="DDLArticulo_SelectedIndexChanged"></asp:DropDownList>
                                    </div>

                                    <div class="col-md-1">
                                        <label class="control-label text-danger">*</label><label class="col-form-label">Cantidad:</label>
                                    </div>

                                    <div class="col-md-2">
                                        <asp:TextBox ID="TxCantidad" TextMode="Number" class="form-control" runat="server" OnTextChanged="TxCantidad_TextChanged"></asp:TextBox>
                                    </div>

                                    <div class="col-md-1">
                                        <asp:LinkButton runat="server" ID="btnAgregar" CssClass="btn  btn-success mr-2" OnClick="btnAgregar_Click"><i class="fa fa-plus"></i></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <asp:UpdatePanel ID="UpCantidadMaxima" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="col-md-12" style="align-self: center" runat="server" id="DivAlertaCantidad" visible="false">
                                <div class="alert alert-danger   align-content-md-center">
                                    <h3 class="text-danger" style="text-align: center"><i class="fa fa-exclamation-triangle"></i>Warning</h3>
                                    <asp:Label ID="lbCantidad" runat="server" Text="" Width="100%"></asp:Label>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <asp:UpdatePanel runat="server" ID="UPMateriales" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="col-md-12 align-self-center" style="margin-left: auto; margin-right: auto">
                                <div class="table-responsive">
                                    <!--<table id="bootstrap-data-table" class="table table-striped table-bordered"> -->
                                    <asp:GridView ID="GVNewMateriales" runat="server"
                                        CssClass="table table-bordered"
                                        PagerStyle-CssClass="pgr"
                                        HeaderStyle-CssClass="table"
                                        RowStyle-CssClass="rows"
                                        AutoGenerateColumns="false"
                                        AllowPaging="true"
                                        GridLines="None"
                                        HeaderStyle-HorizontalAlign="center"
                                        PageSize="10"
                                        Style="margin: 30px 0px 20px 0px" OnRowCommand="GVNewMateriales_RowCommand">
                                        <Columns>

                                            <asp:BoundField DataField="idStock" HeaderText="Id Stock" Visible="true" ItemStyle-HorizontalAlign="center" />
                                            <asp:BoundField DataField="nombre" HeaderText="Material" ItemStyle-HorizontalAlign="center" />
                                            <asp:BoundField DataField="cantidad" HeaderText="Cantidad" ItemStyle-HorizontalAlign="center" />
                                            <asp:TemplateField HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="Btnseleccionar" Enabled="true" runat="server" Text="" class="btn btn-danger mr-2" CommandArgument='<%# Eval("idStock") %>' CommandName="eliminar"><i class="icon-trash"></i></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="10%" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
                <asp:UpdatePanel ID="UpdateModal" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="col-md-12" style="align-self: center" runat="server" id="DivAlerta" visible="false">
                            <div class="alert alert-danger   align-content-md-center">
                                <h3 class="text-danger" style="text-align: center"><i class="fa fa-exclamation-triangle"></i>Warning</h3>
                                <asp:Label ID="LbMensajeModalError" runat="server" Text="" Width="100%"></asp:Label>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <button type="button" class="btn btn-light" data-dismiss="modal">
                                Close</button>
                            <asp:Button ID="btnModalEnviar" runat="server" Text="Enviar" class="btn btn-dark" OnClick="btnModalEnviar_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <%--FIN MODAL ENVIAR LV--%>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
     <script src="../../assets/node_module/select2/dist/js/select2.full.min.js" type="text/javascript"></script>
    <style>
        .select2-selection__rendered {line-height: 31px !important;}
        .select2-container .select2-selection--single {height: 35px !important;}
        .select2-selection__arrow {height: 34px !important;}
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