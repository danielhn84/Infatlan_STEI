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
                                    <asp:TextBox runat="server" Text="" TextMode="MultiLine" Rows="3" ID="TxMensaje" CssClass="form-control" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-12">
                        <asp:Button runat="server" ID="BtnEnviar" CssClass="btn btn-success" Text="Enviar" OnClick="BtnEnviar_Click" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
