<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="pendientesCrearNotificacion.aspx.cs" Inherits="Infatlan_STEI_Comunicacion.pages.mantenimiento.pendientesCrearNotificacion" %>
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
        function abrirModalCancelarNoti() { $('#ModalCancelacionNotificacion').modal('show'); }
        function cerrarModalCancelarNoti() { $('#ModalCancelacionNotificacion').modal('hide'); }
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
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Comunicación</a></li>
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Mantenimiento</a></li>
                    <li class="breadcrumb-item active">Crear Notificación</li>
                </ol>
            </div>
        </div>
    </div>


    <asp:UpdatePanel runat="server" ID="UpPendientesCrearNotificacion" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Mantenimientos Pendientes Crear Notificación</h4>
                    <h6 class="card-subtitle">Mantenimientos pendientes que estan asignadas a su persona.</h6>
                    <div class="card-body">
                        <div class="row col-7">
                            <label class="col-2 col-form-label">Búsqueda</label>
                            <div class="col-8">
                                <asp:TextBox runat="server" PlaceHolder="Ingrese texto y presione Enter" ID="TxBusqueda" AutoPostBack="true" CssClass="form-control form-control-line"></asp:TextBox>
                            </div>
                        </div>

                        <div class="table-responsive m-t-20">
                            <asp:GridView ID="GvPendientesCrearNotificacion" runat="server"
                                CssClass="table table-bordered"
                                PagerStyle-CssClass="pgr"
                                HeaderStyle-CssClass="table"
                                RowStyle-CssClass="rows"
                                AutoGenerateColumns="false"
                                AllowPaging="true" OnPageIndexChanging="GvPendientesCrearNotificacion_PageIndexChanging" 
                                GridLines="None" OnRowCommand="GvPendientesCrearNotificacion_RowCommand"
                                PageSize="10">
                                <Columns>
                                    <asp:BoundField DataField="idMantenimiento" HeaderText="Id" />
                                    <asp:BoundField DataField="nombreNodo" HeaderText="Nodo" />
                                    <asp:BoundField DataField="serie" HeaderText="Serie" />
                                    <asp:BoundField DataField="ip" HeaderText="Ip" />
                                    <asp:BoundField DataField="regiones" HeaderText="Region" />
                                    <asp:BoundField DataField="tipoStock" HeaderText="Tipo" />
                                    <asp:BoundField DataField="direccion" HeaderText="Direccion" />
                                    <asp:BoundField DataField="fechaMantenimiento" HeaderText="Mantenimiento" />
                                    <asp:TemplateField HeaderText="Acción" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LBModificar" runat="server" class="btn btn-info mr-2" CommandName="Crear" CommandArgument='<%# Eval("idMantenimiento") %>'>
                                                       <i class="icon-pencil" ></i>
                                            </asp:LinkButton>

                                             <asp:LinkButton ID="LBCancelar" runat="server" class="btn btn-primary mr-2" CommandName="Cancelar" CommandArgument='<%# Eval("idMantenimiento") %>'>
                                                       <i class="icon-close" ></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>

                                        <ItemStyle Width="15%" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


    <!-- Cancelar Notificacion -->
    <div class="modal fade" id="ModalCancelacionNotificacion" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="width: 600px; top: 320px; left: 50%; transform: translate(-50%, -50%);">
                <div class="modal-header" >
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <h4 class="modal-title" id="exampleModalLabel" >Cancelar Notificación  
                                <asp:Label ID="Titulo" runat="server" Text="" Style="margin-left: auto; margin-right: auto"></asp:Label></h4>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>

                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
                            <div class="form-group row">
                                <div class="col-12" runat="server">
                                    <div class="form-group row">
                                        <div class="col-3">
                                            <label class="control-label">Id Mantenimiento:</label></label>                                           
                                        </div>
                                        <div class="col-9">
                                            <asp:TextBox ID="TxIdMantenimiento" AutoPostBack="true" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12" runat="server">
                                    <div class="form-group row">
                                        <div class="col-md-3">
                                            <label class="control-label text-danger" runat="server" id="lbMotivo">*</label><label class="control-label">Motivo:</label></label>                                      
                                        </div>
                                        <div class="col-sm-9">
                                            <asp:DropDownList ID="DdlMotivo" runat="server" AutoPostBack="true" CssClass="form-control" Visible="true" OnSelectedIndexChanged="DdlMotivo_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12" runat="server">
                                    <div class="form-group row">
                                        <div class="col-md-3">
                                            <label class="control-label   text-danger" runat="server" visible="false" id="asterisco">*</label><label class="control-label" runat="server" visible="false" id="etiqueta">Nuevo Técnico:</label></label>                                     
                                        </div>
                                        <div class="col-md-9">
                                            <asp:DropDownList ID="DDLNombreResponsable" runat="server" AutoPostBack="true" CssClass="form-control" Visible="false"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12" runat="server">
                                    <div class="form-group row">
                                        <div class="col-md-3">
                                            <label class="control-label   text-danger">*</label><label class="control-label">Detalle motivo:</label></label>                                    
                                        </div>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="TxDetalle" class="form-control" runat="server" TextMode="MultiLine" Rows="5" AutoPostBack="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                                 
                                        <div class="col-md-12" runat="server" id="DivMensaje" visible="false" style="display: flex; background-color: tomato; justify-content: center">
                                            <asp:Label runat="server" CssClass="col-form-label text-white" ID="LbValidacion"></asp:Label>
                                        </div>



                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                        <ContentTemplate>
                            <button type="button" class="btn btn-light" data-dismiss="modal">Cerrar</button>
                            <asp:Button ID="BtnCancelarNotificacion" runat="server" Text="Enviar" class="btn btn-success" OnClick="BtnCancelarNotificacion_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
