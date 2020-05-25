<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="inventario.aspx.cs" Inherits="Infatlan_STEI_Inventario.pages.salidas" %>
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
        function abrirModal() { $('#ModalConfirmar').modal('show'); }
        function cerrarModal() { $('#ModalConfirmar').modal('hide'); }

        function openModal() { $('#ModalUbicaciones').modal('show'); }
        function closeModal() { $('#ModalUbicaciones').modal('hide'); }
        function openModalU() { $('#ModalUbicaciones').modal('show'); }
        function closeModalU() { $('#ModalUbicaciones').modal('hide'); }
    </script>
    <link href="../assets/node_module/select2/dist/css/select2.min.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #ffffff; opacity: 0.7; margin: 0;">
                <span style="display: inline-block; height: 100%; vertical-align: middle;"></span>
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="/images/loading.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="display: inline-block; vertical-align: middle;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <br />
    <div class="card">
        <div class="card-body">
            <h4 class="card-title">Inventario</h4>
            <nav>
                <div class="nav nav-pills " id="nav-tab" role="tablist">
                    <a class="nav-item nav-link active" id="nav_cargar_tab" data-toggle="tab" href="#navNuevo" role="tab" aria-controls="nav-profile" aria-selected="false"><i class="icon-plus"> </i>Nuevo</a>
                    <a runat="server" visible="true" class="nav-item nav-link" id="Registros" data-toggle="tab" href="#navInventarios" role="tab" aria-controls="nav-profile" aria-selected="false"><i class="icon-list" > </i>Inventario</a>
                </div>
            </nav>
            <hr />
            <div class="tab-content" id="nav-tabContent">
                <div class="tab-pane fade show active" id="navNuevo" role="tabpanel" aria-labelledby="nav-cargar-tab">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel">
                        <ContentTemplate>
                            <div class="card-body">
                                <h6 class="card-subtitle">Material o equipo que saldrá del almacén.</h6>
                                <div class="form-body col-12">
                                    <div class="row">
                                        <div class="col-6">
                                            <div class="form-group row">
                                                <label class="col-form-label col-12">Código</label>
                                                <div class="col-12">
                                                    <asp:TextBox runat="server" ID="TxCodigo" CssClass="form-control"></asp:TextBox>                                        
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-6">
                                            <div class="form-group row">
                                                <label class="col-form-label col-12">Artículo del almacén</label>
                                                <div class="col-12">
                                                    <asp:DropDownList runat="server" ID="DDLArticulo" CssClass="select2 form-control custom-select" style="width: 100%"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-6">
                                            <div class="form-group row">
                                                <label class="col-form-label col-12">Cantidad</label>
                                                <div class="col-12">
                                                    <asp:TextBox runat="server" ID="TxCantidad" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-6">
                                            <div class="form-group row">
                                                <label class="col-form-label col-12">Tipo de Transaccion</label>
                                                <div class="col-12">
                                                    <asp:DropDownList runat="server" ID="DDLTipoTransaccion" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-6">
                                            <div class="form-group row">
                                                <label class="col-form-label col-12">Ubicación de Destino</label>
                                                <div class="col-10">
                                                    <asp:DropDownList runat="server" ID="DDLUbicacion" CssClass="select2 form-control custom-select" style="width: 100%"></asp:DropDownList>
                                                </div>
                                                <div class="col-1">
                                                    <asp:Button ID="BtnAddUbicacion" Text="+" CssClass="btn btn-primary" runat="server" OnClick="BtnAddUbicacion_Click" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-6">
                                            <div class="form-group row">
                                                <label class="col-form-label col-12">Descripción</label>
                                                <div class="col-12">
                                                    <asp:TextBox runat="server" ID="TxDescripcion" Rows="3" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="card-body">
                                    <asp:Button Text="Guardar" class="btn btn-success" OnClick="BtnGuardarInventario_Click" ID="BtnGuardarInventario" runat="server" />
                                    <button type="button" class="btn btn-dark">Cancelar</button>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <div class="tab-pane fade" id="navInventarios" role="tabpanel" aria-labelledby="nav-tecnicos-tab">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                        <ContentTemplate>
                            <div class="card-body">
                                <h6 class="card-subtitle">Distribución del inventario por ubicaciones.</h6>
                                <div class="table-responsive m-t-40">
                                    <asp:GridView ID="GVBusqueda" runat="server"
                                        CssClass="table table-bordered"
                                        PagerStyle-CssClass="pgr"
                                        HeaderStyle-CssClass="table"
                                        RowStyle-CssClass="rows"
                                        AutoGenerateColumns="false"
                                        AllowPaging="true"
                                        GridLines="None" OnRowCommand="GVBusqueda_RowCommand"
                                        PageSize="10" OnPageIndexChanging="GVBusqueda_PageIndexChanging">
                                        <Columns>
                                            <asp:BoundField DataField="idUbicacion" Visible="false" />
                                            <asp:BoundField DataField="codigo" HeaderText="Código" />
                                            <asp:BoundField DataField="Municipio" HeaderText="Direccion 1" />
                                            <asp:BoundField DataField="direccion" HeaderText="Direccion 2" />
                                            <asp:BoundField DataField="tipo" HeaderText="Tipo" />
                                            <asp:TemplateField HeaderText="Seleccione" HeaderStyle-Width="13%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="BtnEditar" runat="server" class="btn btn-info mr-2" Title="Ver" CommandArgument='<%# Eval("idUbicacion") %>' CommandName="VerInventario">
                                                        <i class="icon-action-redo"></i>
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
    </div>
            
    <%--MODAL DE UBICACIONES--%>
    <div class="modal fade" id="ModalUbicaciones" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">
                        <asp:Label ID="LbIdUbicacion" runat="server" Text="Crear Nueva Ubicación"></asp:Label>
                    </h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group row">
                                        <div class="col-2" style="margin-left:5%">
                                            <label class="col-form-label">Tipo</label>
                                        </div>
                                        <div class="col-sm-9">
                                            <asp:DropDownList ID="DDLTipoUbic" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="form-group row">
                                        <div class="col-3" style="margin-left:5%">
                                            <label class="col-form-label">Departamento</label>
                                        </div>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="DDLDepartamento" AutoPostBack="true" runat="server" class="form-control" OnSelectedIndexChanged="DDLDepartamento_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="form-group row">
                                        <div class="col-2" style="margin-left:5%">
                                            <label class="col-form-label">Municipio</label>
                                        </div>
                                        <div class="col-sm-9">
                                            <asp:DropDownList ID="DDLMunicipio" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="form-group row">
                                        <div class="col-2" style="margin-left:5%">
                                            <label class="col-form-label">Código</label>
                                        </div>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="TxCodigoUbic" ReadOnly="true" class="form-control text-uppercase" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="form-group row">
                                        <div class="col-2" style="margin-left:5%">
                                            <label class="col-form-label">Dirección</label>
                                        </div>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="TxDireccionUbic" TextMode="MultiLine" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12" runat="server" id="DivMensajeUbic" visible="false" style="display: flex; background-color:tomato; justify-content:center">
                                    <asp:Label runat="server" CssClass="col-form-label text-white" ID="LbMensajeUbic"></asp:Label>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                        <ContentTemplate>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                            <asp:Button ID="BtnAceptarUbic" runat="server" Text="Aceptar" class="btn btn-success" OnClick="BtnAceptarUbic_Click"/>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <%--MODAL DE CONFIRMACION--%>
    <div class="modal fade" id="ModalConfirmar" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="ModalLabelConfirmar">
                        <b><asp:Label Text="Guardar Inventario" runat="server" ID="LbTitulo" CssClass="col-form-label"></asp:Label></b>
                    </h4>
                </div>
                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                            <asp:Button ID="BtnConfirmar" runat="server" Text="Aceptar" class="btn btn-danger" OnClick="BtnConfirmar_Click"/>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <script src="../assets/node_module/select2/dist/js/select2.full.min.js" type="text/javascript"></script>
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
