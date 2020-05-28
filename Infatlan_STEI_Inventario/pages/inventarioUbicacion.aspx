<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="inventarioUbicacion.aspx.cs" Inherits="Infatlan_STEI_Inventario.pages.inventarios" %>
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
        function openModal() { $('#ModalMover').modal('show'); }
        function cerrarModal() { $('#ModalMover').modal('hide'); }
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

    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Registros de
                        <asp:Label Text="" ID="LbUbicacion" runat="server" />
                    </h4>
                    <h6 class="card-subtitle">Todas los articulos asignados a esta ubicación.</h6>
                    <div class="row">
                        <div class="col-12">
                            <a href="inventario.aspx" class="btn btn-primary"><i style="margin-right:10%" class="icon-action-undo"></i>Volver</a>
                        </div>
                    </div>
                    <div class="table-responsive m-t-40">
                        <asp:GridView ID="GVBusqueda" runat="server"
                            CssClass="table table-bordered"
                            PagerStyle-CssClass="pgr"
                            HeaderStyle-CssClass="table"
                            RowStyle-CssClass="rows"
                            AutoGenerateColumns="false"
                            AllowPaging="true" OnPageIndexChanging="GVBusqueda_PageIndexChanging"
                            GridLines="None" OnRowCommand="GVBusqueda_RowCommand"
                            PageSize="10" >
                            <Columns>
                                <asp:BoundField DataField="idInventario" HeaderText="No." />
                                <asp:BoundField DataField="codigoInventario" HeaderText="Cod." />
                                <asp:BoundField DataField="Articulo" HeaderText="Articulo" />
                                <asp:BoundField DataField="serie" HeaderText="Serie" />
                                <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
                                <asp:BoundField DataField="precio" HeaderText="Total" />
                                <asp:TemplateField HeaderText="Seleccione" HeaderStyle-Width="13%">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="BtnMover" runat="server" class="btn btn-info mr-2" Title="Mover" CommandArgument='<%# Eval("idInventario") %>' CommandName="MoverArticulo">
                                            <i class="icon-refresh" ></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <%--MODAL DE MOVER ARTICULO--%>
    <div class="modal fade" id="ModalMover" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="ModalLabelModificacion">
                        <asp:Label ID="LbIdInventario" runat="server" Text="Mover Articulo(s)"></asp:Label>
                    </h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-12">
                            <div class="form-group row">
                                <div class="col-12">
                                    <label class="col-form-label">Ubicación Actual</label>
                                </div>
                                <div class="col-12">
                                    <asp:UpdatePanel ID="UpdatePanelModal" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="TxActual" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                                            <asp:TextBox Visible="false" ID="TxIdInventario" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                                            <asp:TextBox Visible="false" ID="TxIdUbicacion" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                                            <asp:TextBox Visible="false" ID="TxIdStock" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                                            <asp:TextBox Visible="false" ID="TxCodigo" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                                            <asp:TextBox Visible="false" ID="TxPrecio" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                                            <asp:TextBox Visible="false" ID="TxCantidadActual" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                                            <asp:TextBox Visible="false" ID="TxProceso" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <div class="col-12">
                            <div class="form-group row">
                                <div class="col-12" >
                                    <label class="col-form-label">Nueva Ubicación</label>
                                </div>
                                <div class="col-12">
                                    <asp:DropDownList ID="DDLNueva" runat="server" CssClass="select2 form-control custom-select" style="width: 100%"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-12" >
                                    <label class="col-form-label">Cantidad</label>
                                </div>
                                <div class="col-12">
                                    <asp:TextBox runat="server" TextMode="Number" ID="TxCantidad" CssClass="form-control"/>
                                </div>
                            </div>
                        </div>
                        <asp:UpdatePanel runat="server" ID="UPMensaje">
                            <ContentTemplate>
                                <div class="col-12" runat="server" id="DivMensaje" visible="false" style="display: flex; background-color:tomato; justify-content:center">
                                    <asp:Label runat="server" CssClass="col-form-label text-white" ID="LbAdvertencia"></asp:Label>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdateModificacionBotones" runat="server">
                        <ContentTemplate>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                            <asp:Button ID="BtnAceptar" runat="server" Text="Aceptar" class="btn btn-success" OnClick="BtnAceptar_Click"/>
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
