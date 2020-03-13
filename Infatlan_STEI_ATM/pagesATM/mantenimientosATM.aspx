<%@ Page Title="" Language="C#" MasterPageFile="~/mainATM.Master" AutoEventWireup="true" CodeBehind="mantenimientosATM.aspx.cs" Inherits="Infatlan_STEI_ATM.pagesATM.manteniminetosATM" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
     <div class="row page-titles">
        <div class="col-md-6 align-self-center">
            <h4 class="text-themecolor">Ingresar mantenimientos programados anuales de ATM</h4>
        </div>
        <div class="col-md-6 align-self-center text-right">
            <div class="d-flex justify-content-end align-items-center">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Inicio</a></li>
                    <li class="breadcrumb-item active">Mantenimientos programados anuales de ATM</li>
                </ol>
               
            </div> 
        </div>
    </div>
    <!--/ENCABEZADO-->

    <div class="card">
        <br />
        <div class="row col-12" style="margin-left: 10px; margin-left: 10px;">
            <h3 class="text-themecolor" style="color: #808080;"><i class="fa fa-save"></i>Mantenimientos programados de ATM</h3>
        </div>
        <hr />
        <br />

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
                    <a href="plantillas/PlantillaATM.xlsx">AQUI</a>
                </div>
                <div class="stats">
                    <i class="fa fa-info"></i>
                    <asp:Label ID="LbMensaje" runat="server" Text="Recuerda verificar el Excel"></asp:Label>
                </div>
            </div>
            <br />
        </div>
    </div>
    


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
