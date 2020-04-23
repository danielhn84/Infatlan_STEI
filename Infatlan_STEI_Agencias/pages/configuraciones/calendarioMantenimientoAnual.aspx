﻿<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="calendarioMantenimientoAnual.aspx.cs" Inherits="Infatlan_STEI_Agencias.pages.configuraciones.calendarioMantenimientoAnual" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">

    <script>
        function clearFileInputField(divId) {
            document.getElementById(divId).innerHTML = document.getElementById(tagId).innerHTML;
        }
        </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">

     <div class="row page-titles">
        <div class="col-md-7 align-self-center">
            <h2 class="text-themecolor">Calendario Anual</h2>
            <div class="mr-md-3 mr-xl-5">
                <%-- <h2>Creación de Notificación</h2>--%>
                <p class="mb-md-0">Soporte Técnico y Comunicaciones</p>
            </div>
        </div>
    </div>

    <div class="card">
        <div class="card-body">
            <h4 class="card-title">Listado de mantenimientos preventivos programados</h4>
            <p>Calendario de ejecucion de mantenimientos</p>

            <div class="row p-t-20" id="divFileUp3">
                <div class="col-md-3">
                    <label class="control-label   text-danger">*</label><label class="control-label">Archivo:</label>
                </div>

                <div class="col-md-8">
                    <asp:FileUpload runat="server" ID="FUMantenimientosAgencia" AllowMultiple="false" ClientIDMode="AutoID" CssClass="form-control" />
                </div>
            </div>

            <br><br>


            <asp:UpdatePanel ID="UpdateModal" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="col-md-12" style="align-self: center" runat="server" id="DivAlerta" visible="false">
                        <div class="alert alert-danger   align-content-md-center">
                            <h3 class="text-danger" style="text-align: center"><i class="fa fa-exclamation-triangle"></i>Warning</h3>
                            <asp:Label ID="LbMensaje" runat="server" Text="" Width="100%"></asp:Label>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="col-md-12" style="align-self: center" runat="server" id="Div1" visible="false">
                        <div class="alert alert-success   align-content-md-center">
                            <h3 class="text-success" style="text-align: center"><i class="fa fa-exclamation-triangle"></i>Success</h3>
                            <asp:Label ID="LbMensajeSuccsess" runat="server" Text="" Width="100%"></asp:Label>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>


            <div class="row p-t-20 col-md-12">
                <div class="col-md-4">
                    <asp:Button ID="BtnEnviar" class="btn btn-block btn-success" runat="server" Text="Enviar" OnClick="BtnEnviar_Click1" />
                </div>
                <div class="col-md-4">
                    <asp:Button ID="BtnCancelar" class="btn btn-block btn-danger" runat="server" Text="Cancelar"  onclientclick="clearFileInputField(divFileUp3)" OnClick="BtnCancelar_Click1" />
                </div>
                <div class="col-md-4">
                    <a href="../../default.aspx" class="btn  btn-block btn-primary">Volver</a>
                 </div>
            </div>
                           
            <hr>
               <div class="row p-t-20">
                <div class="col-md-12">
                    <asp:Label Text="Nota: Recuerda descargar la plantilla de Excel para poder subir los mantenimientos anuales." runat="server" />
                        <a href="../plantilla/PlantillaAgencia.xlsx">DAR CLIC AQUI PARA DESCARGAR </a>
                </div>           
            </div>
        </div>
    </div>




<%--<div id="divFileUp3">
<asp:FileUpload ID="fileUpload1" runat="server" />
</div>
<asp:Button ID="btnCancelUpload" runat="server"  onclientclick="clearFileInputField(divFileUp3)" Text="Cancel Upload" />--%>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
