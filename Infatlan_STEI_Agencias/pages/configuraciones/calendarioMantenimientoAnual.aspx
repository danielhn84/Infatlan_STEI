<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="calendarioMantenimientoAnual.aspx.cs" Inherits="Infatlan_STEI_Agencias.pages.configuraciones.calendarioMantenimientoAnual" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">

    <script>
        var updateProgress = null;
        function postbackButtonClick() {
            updateProgress = $find("<%= UpdateProgress1.ClientID %>");
            window.setTimeout("updateProgress.set_visible(true)", updateProgress.get_displayAfter());
            return true;
        }

        function clearFileInputField(divId) {
            document.getElementById(divId).innerHTML = document.getElementById(tagId).innerHTML;
        }
    </script>

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
        <div class="col-md-5 align-self-center">
            <h4 class="text-themecolor">STEI</h4>
        </div>
        <div class="col-md-7 align-self-center text-right">
            <div class="d-flex justify-content-end align-items-center">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Módulos</a></li>
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Agencias</a></li>
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Configuraciones</a></li>
                    <li class="breadcrumb-item active">Calendario Anual</li>
                </ol>
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

            <div class="col-md-12" style="text-align: center">
                <label class="control-label text-danger" style="text-align: center">Los campos con (*) son obligatorios</label>
            </div>



            <asp:UpdatePanel ID="UpdateModal111" runat="server">
                <ContentTemplate>
                    <div class="col-md-12" runat="server" id="DivAlerta" visible="false" style="display: flex; background-color: tomato; justify-content: center">
                        <asp:Label runat="server" CssClass="col-form-label" ID="LbMensaje"></asp:Label>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>




            <asp:UpdatePanel ID="UpdatePanel111" runat="server">
                <ContentTemplate>
                    <div class="col-md-12" runat="server" id="Div1" visible="false" >
                        <asp:Label runat="server" CssClass="col-form-label" ID="LbMensajeSuccsess"></asp:Label>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>



<%--            <div class="row p-t-20 col-md-12">
                <div class="col-md-4">
                    <asp:Button ID="BtnEnviar" class="btn btn-block btn-success" runat="server" Text="Enviar" OnClick="BtnEnviar_Click1" />
                </div>
                <div class="col-md-4">
                    <asp:Button ID="BtnCancelar" class="btn btn-block btn-danger" runat="server" Text="Cancelar"  onclientclick="clearFileInputField(divFileUp3)" OnClick="BtnCancelar_Click1" />
                </div>
                <div class="col-md-4">
                    <a href="../../default.aspx" class="btn  btn-block btn-primary">Volver</a>
                 </div>
            </div>--%>
            <br />
            <div class="row p-t-20">
                <div class="col-md-12">
                    <asp:Label Text="Nota: Recuerda descargar la plantilla de Excel para poder subir los mantenimientos anuales." runat="server" />
                    <a href="../plantilla/PlantillaAgencia.xlsx">DAR CLIC AQUI PARA DESCARGAR </a>
                </div>
            </div>
            <br />

            <div class="modal-footer">
            <%--    <asp:UpdatePanel ID="UpdateModificacionBotones" runat="server">
                    <ContentTemplate>--%>
                        <%--<button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>--%>
                        <asp:Button ID="BtnCancelar" runat="server" Text="Cancelar" class="btn  btn-dark" onclientclick="clearFileInputField(divFileUp3)" OnClick="BtnCancelar_Click1" />
                        <asp:Button ID="BtnEnviar" runat="server" Text="Enviar" class="btn btn-success" OnClick="BtnEnviar_Click1" />
             <%--       </ContentTemplate>
                </asp:UpdatePanel>--%>
            </div>
            <br />

                           

        </div>
    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
