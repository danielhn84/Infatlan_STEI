<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="creacionTareas.aspx.cs" Inherits="Infatlan_STEI_GestionesTecnicas.pages.creacionTareas" %>

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
        function abrirModalConfirmacion() { $('#ModalConfirmar').modal('show'); }
        function cerrarModalConfirmacion() { $('#ModalConfirmar').modal('hide'); }
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
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Módulos</a></li>
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Gestiones Técnicas</a></li>
                    <li class="breadcrumb-item active">Creación de Tareas</li>
                </ol>
            </div>
        </div>
    </div>


    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-body">
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="nav-item"><a class="nav-link active" data-toggle="tab" href="#home" role="tab"><span class="hidden-sm-up"><i class="fa fa-list"></i></span><span class="hidden-xs-down"> Datos Generales</span></a> </li>
                        <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#adjunto" runat="server" role="tab"><span class="hidden-sm-up"><i class="fa fa-paperclip"></i></span><span class="hidden-xs-down"> Adjunto</span></a> </li>
                        <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#comentarios" role="tab"><span class="hidden-sm-up"><i class="fa fa-comment"></i></span><span class="hidden-xs-down"> Comentarios</span></a> </li>
                    </ul>

                    <div class="tab-content tabcontent-border" style="height: 530px">
                        <!--PRIMER CONTENIDO-->
                        <div class="tab-pane active p-20" id="home" role="tabpanel">
                            <div class="row p-t-20">
                                <div class="col-lg-12">
                                    <asp:UpdatePanel runat="server" ID="UPFormulario">
                                        <contenttemplate>
                                            <!--Inicio Fila 1-->
                                            <div class="row p-t-20">
                                                <div class="col-6">
                                                    <label class="control-label   text-danger">*</label><label class="control-label">Título:</label></label>
                                                    <asp:TextBox ID="TxTitulo" AutoPostBack="true" runat="server" class="form-control"></asp:TextBox>
                                                </div>

                                                <div class="col-6">
                                                    <label class="control-label   text-danger">*</label><label class="control-label">Fecha Solicitud:</label></label>    
                                                    <asp:TextBox ID="TxFechaSolicitud" AutoPostBack="true" ReadOnly="true" runat="server" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <!--Fin Fila 1-->

                                            <div class="row p-t-20">
                                                <div class="col-12">
                                                    <label class="control-label   text-danger">*</label><label class="control-label">Descripción:</label></label>
                                                    <asp:TextBox ID="TxDescripcion" AutoPostBack="true" TextMode="MultiLine" Rows="3" runat="server" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="row p-t-20">
                                                <div class="col-6">
                                                    <label class="control-label   text-danger">*</label><label class="control-label">Responsable:</label></label>
                                                    <asp:DropDownList runat="server" ID="DdlResponsable" CssClass="select2 form-control custom-select" Style="width: 100%"></asp:DropDownList>
                                                </div>

                                                <div class="col-6">
                                                    <label class="control-label   text-danger">*</label><label class="control-label">Prioridad:</label></label>    
                                                     <asp:DropDownList ID="DdlPrioridad" runat="server" AutoPostBack="true" CssClass="form-control">
                                                         <asp:ListItem Value="0" Text="Seleccione una opción"></asp:ListItem>
                                                         <asp:ListItem Value="1" Text="Alta"></asp:ListItem>
                                                         <asp:ListItem Value="2" Text="Normal"></asp:ListItem>
                                                         <asp:ListItem Value="3" Text="Baja"></asp:ListItem>
                                                     </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="row p-t-20">
                                                <div class="col-6">
                                                    <label class="control-label   text-danger">*</label><label class="control-label">Tipo Gestión:</label></label>
                                                    <asp:DropDownList runat="server" ID="DdlTipoGestion" CssClass="select2 form-control custom-select" Style="width: 100%"></asp:DropDownList>
                                                </div>

                                                <div class="col-6">
                                                    <label class="control-label   text-danger">*</label><label class="control-label">Entrega:</label></label>    
                                                    <asp:TextBox ID="TxFechaEntrega" AutoPostBack="true" runat="server" TextMode="DateTimeLocal" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="row p-t-20">
                                                <div class="col-6">
                                                    <label class="control-label   text-danger">*</label><label class="control-label">Tiempo Productivo(min):</label></label>
                                                    <asp:TextBox ID="TxMinProductivo" AutoPostBack="true" runat="server" class="form-control"></asp:TextBox>
                                                </div>

                                                <div class="col-6">
                                                    <label class="control-label   text-danger">*</label><label class="control-label">Estado:</label></label>    
                                                    <asp:TextBox ID="TxEstado" Text="En Ejecución" AutoPostBack="true" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                                </div>
                                            </div>
                                            <br>
                                        </contenttemplate>
                                    </asp:UpdatePanel>
                                </div>

                            </div>
                        </div>

                        <!--SEGUNDO CONTENIDO-->
                        <div class="tab-pane  p-20" id="adjunto" role="tabpanel">
                            <div class="row p-t-20">
                                <div class="col-lg-12">
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="conditional">
                                        <contenttemplate>
                                            <!--Inicio Fila 1-->
                                            <div class="row p-t-20">
                                                <div class="col-1">
                                                    <label class="col-form-label">Archivo:</label></label>
                                                </div>
                                                <div class="col-7">
                                                    <asp:FileUpload ID="FuAdjunto" runat="server" class="form-control" />
                                                </div>
                                                <div class="col-4">
                                                    <asp:Button ID="BtnAddAdjunto" runat="server" Text="+" class="btn btn-cyan" OnClick="BtnAddAdjunto_Click" />
                                                </div>
                                            </div>
                                            <!--Fin Fila 1-->

                                            <div class="col-md-12"  runat="server" id="divAdjunto" visible="false">
                                                <div class="row col-12 mt-3">
                                                    <div class="table table-bordered">
                                                        <asp:GridView ID="GvAdjunto" runat="server"
                                                            CssClass="mydatagrid"
                                                            PagerStyle-CssClass="pgr"
                                                            HeaderStyle-CssClass="header"
                                                            RowStyle-CssClass="rows"
                                                            AutoGenerateColumns="false"
                                                            AllowPaging="true"
                                                            GridLines="None"
                                                            PageSize="3">
                                                            <Columns>
                                                                <asp:BoundField DataField="idAdjunto"  Visible="false" ItemStyle-Width="27%" />
                                                                <asp:BoundField DataField="nombre" HeaderText="Archivo" Visible="true" ItemStyle-Width="95%" />
                                                                <%--<asp:BoundField DataField="ruta" Visible="false" ItemStyle-Width="27%" />--%>
                                                                <asp:TemplateField HeaderText="Seleccione" HeaderStyle-Width=""  HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="BtnEliminar" runat="server" title="Eliminar" Style="background-color: #d9534f" class="btn" CommandArgument='<%# Eval("idAdjunto") %>' CommandName="Eliminar">
                                                                <i class="mdi mdi-delete text-white"></i>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>

                                        </contenttemplate>
                                        <triggers>
                                            <asp:PostBackTrigger ControlID="BtnAddAdjunto" />
                                        </triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>

                        <!--TERCER CONTENIDO-->
                        <div class="tab-pane  p-20" id="comentarios" role="tabpanel">
                            <div class="row p-t-20">
                                <div class="col-lg-12">
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                        <contenttemplate>
                                        <!--Inicio Fila 1-->
                                          <div class="row p-t-20">
                                                <div class="col-1">
                                                    <label class="col-form-label">Comentario:</label></label>
                                                </div>
                                                <div class="col-7">
                                                    <asp:TextBox ID="TxComentario" TextMode="MultiLine" Rows="2" AutoPostBack="true" runat="server" class="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-4">
                                                    <asp:Button ID="BtnAddComentario" runat="server" Text="+" class="btn btn-cyan" OnClick="BtnAddComentario_Click" />
                                                </div>
                                            </div>

                                        <!--Fin Fila 1-->
                                        <div class="col-md-12" runat="server" id="divComentario" visible="false">
                                            <div class="row col-12 mt-3">
                                                <div class="table table-bordered">
                                                    <asp:GridView ID="GvComentario" runat="server"
                                                        CssClass="mydatagrid"
                                                        PagerStyle-CssClass="pgr"
                                                        HeaderStyle-CssClass="header"
                                                        RowStyle-CssClass="rows"
                                                        AutoGenerateColumns="false"
                                                        AllowPaging="true"
                                                        GridLines="None"
                                                        PageSize="3">
                                                        <Columns>
                                                            <asp:BoundField DataField="idComentario" Visible="false" ItemStyle-Width="27%" />
                                                            <asp:BoundField DataField="usuario" Visible="true"  HeaderText="Usuario"  ItemStyle-Width="27%" />
                                                            <asp:BoundField DataField="comentario"  HeaderText="Comentario" Visible="true" ItemStyle-Width="95%" />
                                                            <asp:TemplateField HeaderText="Seleccione" HeaderStyle-Width="">
                                                                <ItemTemplate>
                                                                       <asp:LinkButton ID="BtnEliminarComen" runat="server" title="Eliminar" Style="background-color: #d9534f" class="btn" CommandArgument='<%#Eval("idComentario")%>' CommandName="Eliminar">
                                                                <i class="mdi mdi-delete text-white"></i>
                                                                        </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </contenttemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        
                       

                        <asp:UpdatePanel ID="UpdateModificacionBotones" runat="server">
                            <ContentTemplate>
                                <asp:Button Text="Cancelar" CssClass="btn btn-light" runat="server" />
                                <asp:Button Text="Enviar" CssClass="btn btn-success" runat="server"  ID="BtnEnviar" OnClick="BtnEnviar_Click"/>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>



    <%--MODAL DE CONFIRMACION--%>
    <div class="modal fade" id="ModalConfirmar"  data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="top: 320px; left: 50%; transform: translate(-50%, -50%);">
                <div class="modal-header">
                    <h4 class="modal-title" id="ModalLabelConfirmar">
                        <b>
                            <asp:Label Text="¿Está seguro de crear la tarea?" runat="server" ID="LbTitulo" CssClass="col-form-label"></asp:Label></b>
                    </h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>

                            <div class="form-group row">
                                <div class="col-md-3">
                                    <label class="col-form-label">Título:</label>
                                </div>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="TxTituloModal" AutoPostBack="true" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="col-md-3">
                                    <label class="col-form-label">Tiempo (Min):</label>
                                </div>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="TxMinProductivoModal" AutoPostBack="true" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="col-md-3">
                                    <label class="col-form-label">Fecha Entrega:</label>
                                </div>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="TxEntregaModal" TextMode="DateTimeLocal"  AutoPostBack="true" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                            <asp:Button ID="BtnConfirmarTarea" runat="server" Text="Aceptar" class="btn btn-success" OnClick="BtnConfirmarTarea_Click"/>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>




</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    
  
</asp:Content>
