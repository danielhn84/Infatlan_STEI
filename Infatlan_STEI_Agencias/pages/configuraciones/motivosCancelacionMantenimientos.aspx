﻿<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="motivosCancelacionMantenimientos.aspx.cs" Inherits="Infatlan_STEI_Agencias.pages.configuraciones.motivosCancelacionMantenimientos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">

    <script type="text/javascript">

        var updateProgress = null;
        function postbackButtonClick() {
            updateProgress = $find("<%= UpdateProgress1.ClientID %>");
            window.setTimeout("updateProgress.set_visible(true)", updateProgress.get_displayAfter());
            return true;
        }

        function openModalModificarEstado() { $('#modalModificarEstado').modal('show'); }
        function closeModalModificarEstado() { $('#modalModificarEstado').modal('hide'); }
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
                    <li class="breadcrumb-item active">Motivos</li>
                </ol>
            </div>
        </div>
    </div>

    <div class="card">
        <div class="card-body">
            <h4 class="card-title">Listado de motivos</h4>
            <p>Motivos para cancelar notificaciones o lista de verificación.</p>

            <!--MENU DE SELECCION-->

            <ul class="nav nav-tabs" role="tablist">
                <li class="nav-item"><a class="nav-link active" data-toggle="tab" href="#home" role="tab"><span class="hidden-sm-up"><i class="fa fa-save"></i></span><span class="hidden-xs-down"> Guardar</span></a> </li>
                <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#profile" role="tab"><span class="hidden-sm-up"><i class="icon-pencil"></i></span><span class="hidden-xs-down"> Modificar</span></a> </li>
            </ul>
            <div class="tab-content tabcontent-border">
                <!--PRIMER CONTENIDO-->
                <div class="tab-pane active p-20" id="home" role="tabpanel">
                    <asp:UpdatePanel runat="server" ID="UPprimercontenido">
                        <ContentTemplate>
                            <div class="row p-t-20 col-md-12">
                                <div class="col-md-2 ">
                                    <label class="control-label text-danger">*</label><label class="control-label ">Motivo:</label>
                                </div>
                                <div class="col-md-10">
                                    <asp:TextBox runat="server" ID="TxMotivoCancelacion" MaxLength="350" TextMode="MultiLine" Rows="3" CssClass="form-control" placeholder="Ingrese un motivo.."></asp:TextBox>
                                </div>
                            </div>

                            <div class="row p-t-20 col-md-12">
                                <div class="col-md-2 ">
                                    <label class="control-label text-danger">*</label><label class="control-label ">Categoria:</label>
                                </div>
                                <div class="col-md-10">
                                    <asp:RadioButtonList ID="RblTipo" RepeatDirection="Horizontal" Width="400px" runat="server" AutoPostBack="True">
                                        <asp:ListItem Value="2">Ambos</asp:ListItem>
                                        <asp:ListItem Value="1">Cancelar Notificación</asp:ListItem>
                                        <asp:ListItem Value="0">Cancelar LV</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <br />
                            <br />
                            <div class="col-md-12"  style="text-align:center">
                                <label class="control-label text-danger" style="text-align:center" >Los campos con (*) son obligatorios</label>
                            </div>
                             <br />

                            <div class="modal-footer">
                                <asp:UpdatePanel ID="UpdateModificacionBotones" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="BtnCancelar" runat="server" Text="Cancelar" class="btn  btn-dark" OnClick="BtnCancelar_Click" />
                                        <asp:Button ID="BtnEnviar" runat="server" Text="Enviar" class="btn btn-success" OnClick="BtnEnviar_Click" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>


                            <br />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <!--/PRIMER CONTENIDO-->

                <!--SEGUNDO CONTENIDO-->
                <div class="tab-pane  p-20" id="profile" role="tabpanel">
                    <div class="col-md-12">
                        <div class="form-group row">
                            <div class="col-sm-12">
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="row p-t-20">
                                            <div class="col-md-1">
                                                <label class="control-label">Buscar:</label></label>                                      
                                            </div>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="TxBuscarMotivo" runat="server" placeholder="Búsqueda por motivo o Id, luego presione Enter" class="form-control" AutoPostBack="true" OnTextChanged="TxBuscarMotivo_TextChanged"></asp:TextBox>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>

                    <div class="row col-12">
                        <div class="col-12 grid-margin stretch-card">
                            <div class="table-responsive">
                                <asp:UpdatePanel runat="server" ID="UPMotivos" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:GridView ID="GVMotivos" runat="server"
                                            CssClass="table table-bordered"
                                            PagerStyle-CssClass="pgr"
                                            HeaderStyle-CssClass="table" HeaderStyle-HorizontalAlign="center"
                                            RowStyle-CssClass="rows"
                                            AutoGenerateColumns="false"
                                            AllowPaging="true"
                                            GridLines="None" OnPageIndexChanging="GVMotivos_PageIndexChanging"
                                            PageSize="10" OnRowCommand="GVMotivos_RowCommand">

                                            <Columns>
                                                <asp:TemplateField HeaderText="Acción" ItemStyle-HorizontalAlign="center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LbModificar" Title="Modificar" class="btn btn-info mr-2" runat="server" CommandName="Modifcar" CommandArgument='<%# Eval("id") %>'>
                                                       <i class="icon-pencil" ></i>
                                                        </asp:LinkButton>

                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="id" HeaderText="Id" ControlStyle-Width="10%" />
                                                <asp:BoundField DataField="motivo" HeaderText="Motivo" ControlStyle-Width="40%" />
                                                <asp:BoundField DataField="tipo" HeaderText="Categoria" ControlStyle-Width="30%" />
                                                <asp:BoundField DataField="estado" HeaderText="Estado" ControlStyle-Width="20%" />

                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
                <!--/SEGUNDO CONTENIDO-->

            </div>
        </div>
    </div>

    <%--MODALES--%>
    <%--INICIO MODAL APROBAR LV--%>
    <div class="modal fade" id="modalModificarEstado" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="width: 600px; top: 320px; left: 50%; transform: translate(-50%, -50%);">
                <div class="modal-header">

                    <h3 class="modal-title" id="exampleModalLabel">
                        <asp:Label ID="Titulo" runat="server" Text="Modificar Estado" Style="margin-left: auto; margin-right: auto"></asp:Label>
                    </h3>

                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>

                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
                            <div class="col-md-12" runat="server">
                                <div class="form-group row">
                                    <label class="col-md-3 col-form-label">Id Estado:</label>
                                    <div class="col-md-9">
                                        <asp:TextBox ID="TxIdEstadoModal" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12" runat="server">
                                <div class="form-group row">
                                    <div class="col-md-3">
                                        <label class="control-label text-danger">*</label><label class="col-form-label">Motivo:</label>
                                    </div>
                                    <div class="col-md-9">
                                        <asp:TextBox ID="TxMotivoModal" MaxLength="350"  class="form-control" runat="server" TextMode="MultiLine" Rows="3" OnTextChanged="TxMotivoModal_TextChanged"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12" runat="server">
                                <div class="form-group row">
                                    <div class="col-md-3">
                                        <label class="control-label text-danger">*</label><label class="col-form-label">Tipo:</label>
                                    </div>
                                    <div class="col-md-9">
                                        <asp:DropDownList ID="DDLTipo" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="DDLTipo_SelectedIndexChanged">                                           
                                            <asp:ListItem Value="2" Text="Ambos"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Cancelar Notificación"></asp:ListItem>
                                             <asp:ListItem Value="0" Text="Cancelar LV"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12" runat="server">
                                <div class="form-group row">
                                    <div class="col-md-3 ">
                                        <label class="control-label text-danger">*</label><label class="col-form-label">Estado:</label>
                                    </div>

                                    <div class="col-md-9">
                                        <asp:DropDownList ID="DdlEstado" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="DdlEstado_SelectedIndexChanged">
                                            <asp:ListItem Value="1" Text="Activo"></asp:ListItem>
                                            <asp:ListItem Value="0" Text="Inactivo"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12" style="text-align: center">
                                <label class="control-label" style="text-align: center; color: tomato">Los campos con (*) son obligatorios</label>
                            </div>
                            <asp:UpdatePanel ID="UpdateModal" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>

                                     <div class="col-md-12" runat="server" id="DivAlerta" visible="false" style="display: flex; background-color: tomato; justify-content: center">
                                        <asp:Label runat="server" CssClass="col-form-label text-white" ID="LbMensajeModalError"></asp:Label>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <button type="button" class="btn btn-light"
                                data-dismiss="modal">
                                Close</button>
                            <asp:Button ID="btnModalModificarEstado" runat="server" Text="Modificar" class="btn btn-info mr-2" OnClick="btnModalModificarEstado_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <%--FIN MODAL APROBAR LV--%>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
