<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="mantenimientos.aspx.cs" Inherits="Infatlan_STEI_ATM.pages.calendario.mantenimientos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
     <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #ffffff; opacity: 0.7; margin: 0;">
                <span style="display: inline-block; height: 100%; vertical-align: middle;"></span>
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="../../assets/images/loading.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="display: inline-block; vertical-align: middle;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
     <div class="row page-titles">
        <div class="col-md-5 align-self-center">
            <h3 class="text-themecolor col-12">Mantenimiento de ATM</h3>
            <h6 class="text-themecolor col-12">Ingresar mantenimientos programados de ATM</h6>
        </div>
       
    </div>
    <!--/ENCABEZADO-->

    <div class="card">
        <br />
        <div class=" col-12 align-self-center" style="margin-left: auto; margin-right: auto">
            <div class="row">
                <div class="col-12 grid-margin stretch-card">
                    <div class="card">
                        <div class="card-body">
        <div class="row col-12" style="margin-left: 10px; margin-left: 10px;">
            <%--<asp:UpdatePanel runat="server" ID="UPFile">
                <ContentTemplate>--%>
            <asp:FileUpload runat="server" ID="FUMantenimientos" AllowMultiple="false" ClientIDMode="AutoID" CssClass="col-5" />

            <div class="row col-6">
                <asp:Button Text="Enviar" ID="BtnEnviar" CssClass="btn btn-success" runat="server" OnClick="BtnEnviar_Click1" />
            </div>
            <%--</ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID=""/>
                </Triggers>
            </asp:UpdatePanel>--%>
        </div>
        <br />
        <div class="row col-12" style="margin-left: 10px; margin-left: 10px;">
            <div class="footer">
                <div class="stats">
                    <asp:Label Text="Descargue la plantilla" runat="server" />
                    <a href="../plantillas/PlantillaATM.xlsx">AQUI</a>
                </div>
                <div class="stats col-12">
                    <i class="fa fa-info"></i>
                    <asp:Label ID="LbMensaje" runat="server" CssClass="col-10" Text="Recuerda verificar el Excel"></asp:Label>
                </div>
            </div>
            <br />
        </div>
                         </div>
                    </div>
                </div>
            </div>
        </div>
    </div>



</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
