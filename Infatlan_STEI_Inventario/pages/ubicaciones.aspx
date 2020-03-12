<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="ubicaciones.aspx.cs" Inherits="Infatlan_STEI_Inventario.pages.agencias" %>
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
        function openModal() { $('#ModalUbicaciones').modal('show'); }
        function closeModal() { $('#ModalUbicaciones').modal('hide'); }
        function ModalConfirmar() { $('#ModalConfirmar').modal('show'); }
    </script>
    <script src="../js/bootstrap-notify.js"></script>
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
    <asp:UpdatePanel runat="server" ID="UpdatePanel">
        <ContentTemplate>
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Ubicaciones</h4>
                    <h6 class="card-subtitle">Lugar donde se encuentra el equipo.</h6>
                    <br />
                    <div class="row col-7"> 
                        <label class="col-2 col-form-label">Búsqueda</label>
                        <div class="col-8">
                            <asp:TextBox runat="server" PlaceHolder="Ingrese texto y presione Enter" ID="TxBusqueda" AutoPostBack="true" OnTextChanged="TxBusqueda_TextChanged" CssClass="form-control"></asp:TextBox>
                        </div>
                        <asp:Button runat="server" ID="BtnNuevo" CssClass="btn btn-success" Text="Nuevo" OnClick="BtnNuevo_Click" />
                    </div>

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
                                <asp:BoundField DataField="idUbicacion" HeaderText="No." />
                                <asp:BoundField DataField="tipo" HeaderText="Tipo" />
                                <asp:BoundField DataField="codigo" HeaderText="Código" />
                                <asp:BoundField DataField="direccion" HeaderText="Dirección"/>
                                <asp:TemplateField HeaderText="Seleccione" HeaderStyle-Width="13%">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="BtnEditar" runat="server" class="btn btn-info mr-2" CommandArgument='<%# Eval("idUbicacion") %>' CommandName="EditarUbicacion">
                                            <i class="icon-pencil" ></i>
                                        </asp:LinkButton>
                            
                                        <asp:LinkButton ID="BtnEditar2" runat="server" class="btn btn-primary mr-2" CommandArgument='<%# Eval("idUbicacion") %>' CommandName="EliminarUbicacion">
                                            <i class="icon-trash"></i>
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
            
    <%--MODAL DE MODIFICACION--%>
    <div class="modal bs-example-modal-lg" id="ModalUbicaciones" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="ModalLabelModificacion">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="LbIdUbicacion" runat="server" Text=""></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanelModal" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group row">
                                        <div class="col-2" style="margin-left:5%">
                                            <label class="col-form-label">Tipo</label>
                                        </div>
                                        <div class="col-sm-9">
                                            <asp:DropDownList ID="DDLTipo" runat="server" class="form-control"></asp:DropDownList>
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
                                            <asp:TextBox ID="TxCodigo" class="form-control text-uppercase" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="form-group row">
                                        <div class="col-2" style="margin-left:5%">
                                            <label class="col-form-label">Dirección</label>
                                        </div>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="TxDireccion" TextMode="MultiLine" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12" runat="server" id="DivEstado" visible="false">
                                    <div class="form-group row">
                                        <div class="col-2" style="margin-left:5%">
                                            <label class="col-form-label">Estado</label>
                                        </div>
                                        <div class="col-sm-9">
                                            <asp:DropDownList ID="DDLEstado" runat="server" class="form-control">
                                                <asp:ListItem Value="1" Text="Activo"></asp:ListItem>
                                                <asp:ListItem Value="0" Text="Inactivo"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12" runat="server" id="DivMensaje" visible="false" style="display: flex; background-color:tomato; justify-content:center">
                                    <asp:Label runat="server" CssClass="col-form-label text-white" ID="LbAdvertencia"></asp:Label>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
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

    <%--MODAL DE CONFIRMACION--%>
    <div class="modal fade" id="ModalConfirmar" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <h4 class="modal-title" id="ModalLabelConfirmar">
                                <b><asp:Label runat="server" ID="LbTitulo" CssClass="col-form-label"></asp:Label></b>
                            </h4>
                            <asp:Label runat="server" ID="LbMensaje" CssClass="col-form-label"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
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
</asp:Content>
