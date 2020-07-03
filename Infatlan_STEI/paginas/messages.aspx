<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="messages.aspx.cs" Inherits="Infatlan_STEI.paginas.messages" %>

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
        function openConfirmar() { $('#ModalConfirmar').modal('show'); }
        function closeConfirmar() { $('#ModalConfirmar').modal('hide'); }
    </script>
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
    <div class="row page-titles">
        <div class="col-md-5 align-self-center">
            <h4 class="text-themecolor">STEI</h4>
        </div>
        <div class="col-md-7 align-self-center text-right">
            <div class="d-flex justify-content-end align-items-center">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item active">Mensajes</li>
                </ol>
            </div>
        </div>
    </div>

    <asp:UpdatePanel runat="server" ID="UpdatePanel">
        <ContentTemplate>
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Mensajes</h4>
                    <h6 class="card-subtitle">Mensajes que se muestran en la bandeja de notificaciones.</h6>
                    <nav>
                        <div class="nav nav-pills " id="nav-tab" role="tablist">
                            <a class="nav-item nav-link active" id="nav_cargar_tab" data-toggle="tab" href="#navMis" role="tab" aria-controls="nav-profile" aria-selected="false"><i style="margin-right: 5px;" class="icon-envelope-open"></i>Recibidos</a>
                            <a class="nav-item nav-link" id="nav_nuevo" data-toggle="tab" href="#navNuevo" role="tab" aria-controls="nav-profile" aria-selected="false"><i style="margin-right: 5px" class="icon-plus"></i>Nuevo</a>
                        </div>
                    </nav>
                    <hr />
                    <div class="tab-content" id="nav-tabContent">
                        <div class="tab-pane fade show active" id="navMis" role="tabpanel" aria-labelledby="nav-cargar-tab">
                            <div class="table-responsive m-t-20">
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
                                        <asp:BoundField DataField="idMensaje" HeaderText="idMensaje" Visible="false" />
                                        <asp:BoundField DataField="nombre" HeaderText="Emisor" />
                                        <asp:BoundField DataField="asunto" HeaderText="Asunto" />
                                        <asp:BoundField DataField="mensaje" HeaderText="Mensaje" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="BtnBorrar" runat="server" class="btn btn-primary" Title="Borrar" CommandArgument='<%# Eval("idMensaje") %>' CommandName="BorrarMensaje">
                                                        <i class="icon-trash"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>

                        <div class="tab-pane fade show" id="navNuevo" role="tabpanel" aria-labelledby="nav-cargar-tab">
                            <div class="card-body">
                                <div class="row col-12">
                                    <div class="col-6">
                                        <label class="col-form-label">Destino</label>
                                        <div class="">
                                            <asp:DropDownList runat="server" CssClass="form-control" ID="DDLDestino"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-6">
                                        <label class="col-form-label">Asunto</label>
                                        <div class="">
                                            <asp:TextBox runat="server" Text="" ID="TxAsunto" CssClass="form-control" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row col-12 m-t-10">
                                    <div class="col-6">
                                        <label class="col-form-label">Aplicacion</label>
                                        <div class="">
                                            <asp:DropDownList runat="server" CssClass="form-control" ID="DDLAplicaciones"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-6">
                                        <label class="col-form-label">Mensaje</label>
                                        <div class="">
                                            <asp:TextBox runat="server" Text="" MaxLength="500" TextMode="MultiLine" Rows="3" ID="TxMensaje" CssClass="form-control" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12">
                                <asp:Button runat="server" ID="BtnEnviar" CssClass="btn btn-success" Text="Enviar" OnClick="BtnEnviar_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <%--MODAL DE CONFIRMACION--%>
    <div class="modal fade" id="ModalConfirmar" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title"><b>Eliminar Mensaje</b></h4>
                </div>
                <div class="modal-body">
                    <div class="row col-12">
                        <h5>Desea borrar el mensaje?</h5>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                            <asp:Button ID="BtnConfirmar" runat="server" Text="Aceptar" class="btn btn-primary" OnClick="BtnConfirmar_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
