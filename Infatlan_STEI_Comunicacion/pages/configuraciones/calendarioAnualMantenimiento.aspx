<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="calendarioAnualMantenimiento.aspx.cs" Inherits="Infatlan_STEI_Comunicacion.pages.configuraciones.calendarioAnualMantenimiento" %>
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
        function abrirModalEmergencia() { $('#ModalEmergencia').modal('show'); }
        function cerrarModalEmergencia() { $('#ModalEmergencia').modal('hide'); }
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
            <h4 class="text-themecolor">Calendario Mantenimiento Anual</h4>
        </div>
        <div class="col-md-7 align-self-center text-right">
            <div class="d-flex justify-content-end align-items-center">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Módulos</a></li>
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Comunicación</a></li>
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
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:CheckBox ID="CBEmergencia" OnCheckedChanged="CBEmergencia_CheckedChanged" AutoPostBack="true" runat="server" Text=" Presione aqui si el mantenimiento es de emergencia" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <br>
            <div class="row p-t-20" id="divFileUp3">
                <div class="col-md-2">
                    <label class="control-label  text-danger">*</label><label class="control-label">Archivo:</label>
                </div>

                <div class="col-md-10">
                    <asp:FileUpload runat="server" ID="FUMantenimientosComunicacion" AllowMultiple="false" ClientIDMode="AutoID" CssClass="form-control" />
                </div>
            </div>
            <br>
            <br>

            <div class="col-md-12" style="text-align: center">
                <label class="control-label text-danger" style="text-align: center">Los campos con (*) son obligatorios</label>
            </div>

            <asp:UpdatePanel ID="UpdateModal" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="col-md-12" runat="server" id="DivAlerta" visible="false" style="display: flex; background-color: tomato; justify-content: center">
                        <asp:Label runat="server" CssClass="col-form-label text-white" ID="LbMensaje"></asp:Label>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="col-md-12" runat="server" id="Div1" visible="false" style="display: flex; background-color: darkgreen; justify-content: center">
                        <asp:Label runat="server" CssClass="col-form-label text-white" ID="LbMensajeSuccsess"></asp:Label>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br>
            <asp:UpdatePanel runat="server" ID="UpdatePanel7" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Button Text="Enviar" class="btn btn-success" ID="BtnEnviar" runat="server" />
                    <button type="button" class="btn btn-primary">Cancelar</button>
                </ContentTemplate>
            </asp:UpdatePanel>

            <br>
            <br>

            <hr>
            <div class="row p-t-20">
                <div class="col-md-12">
                    <asp:Label Text="Nota: Recuerda descargar la plantilla de Excel para poder subir los mantenimientos anuales." runat="server" />
                    <a href="../plantilla/PlantillaComunicacion.xlsx">DAR CLIC AQUI PARA DESCARGAR </a>
                </div>
            </div>
        </div>
    </div>


    <%-- Inicio Modal  --%>
    <div class="modal fade" id="ModalEmergencia" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 >
                                <b>Mantenimiento de emergencia </b>
                            </h4>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="modal-body">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel6" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-6">
                                        <label class="control-label text-danger">*</label><label class="control-label">Nodo:</label>
                                       <asp:DropDownList ID="DDLNodo" OnSelectedIndexChanged="DDLNodo_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="fstdropdown-select form-control" Enabled="true"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-6">
                                        <label class="control-label   text-danger">*</label><label class="control-label">Fecha Mantenimiento:</label>
                                        <asp:TextBox ID="TxFechaMantenimiento" TextMode="Date" AutoPostBack="true" runat="server"  class="form-control"></asp:TextBox>
                                    </div>
                                </div>                             
                            </div>
                             <br>
                            <div class="col-12">
                                <div class="row">                                   
                                    <div class="col-md-6">
                                        <label class="control-label   text-danger">*</label><label class="control-label">Responsable:</label>
                                        <asp:DropDownList ID="DdlResponsable" AutoPostBack="true" runat="server" CssClass="fstdropdown-select form-control" Enabled="true"></asp:DropDownList>
                                    </div>
                                     <div class="col-6">
                                        <label class="control-label text-danger">*</label><label class="control-label">Lugar:</label>
                                        <asp:TextBox ID="TxLugar" ReadOnly="true"  TextMode="MultiLine" Rows="2" AutoPostBack="true" runat="server"  class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12">
                                <div class="row">
                                    <div class="col-md-12">
                                        <label class="control-label   text-danger">*</label><label class="control-label">Motivo:</label>
                                        <asp:TextBox ID="TxMotivo" TextMode="MultiLine" Rows="2" AutoPostBack="true" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                              <br>
                            <div class="col-md-12" runat="server" id="DivMensaje" visible="false" style="display: flex; background-color: tomato; justify-content: center">
                                <asp:Label runat="server" CssClass="col-form-label text-white" ID="LbValidacion"></asp:Label>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <br>




                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                            <asp:Button ID="BtnConfirmar" runat="server" Text="Enviar" class="btn btn-success" OnClick="BtnConfirmar_Click" />
                        </ContentTemplate>                       
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
