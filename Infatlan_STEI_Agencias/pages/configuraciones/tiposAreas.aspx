﻿<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="tiposAreas.aspx.cs" Inherits="Infatlan_STEI_Agencias.pages.configuraciones.tiposAreas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">

    <script type="text/javascript">

        var updateProgress = null;
        function postbackButtonClick() {
            updateProgress = $find("<%= UpdateProgress1.ClientID %>");
            window.setTimeout("updateProgress.set_visible(true)", updateProgress.get_displayAfter());
            return true;
        }

        function openModalModificarArea() { $('#modalModificarArea').modal('show'); }
        function closeModalModificarArea() { $('#modalModificarArea').modal('hide'); }
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
                    <li class="breadcrumb-item active">Areas</li>
                </ol>
            </div> 
        </div>
    </div>

    <div class="card">
        <div class="card-body">
            <h4 class="card-title">Tipos de areas</h4>
            <p>Areas del banco donde se realizara mantenimiento</p>


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
                                <div class="col-md-2  ">
                                    <label class="control-label text-danger">*</label><label class="control-label ">Area:</label>
                                </div>
                                <div class="col-md-10">
                                    <asp:TextBox runat="server" ID="TxArea" MaxLength="150"  TextMode="MultiLine" Rows="3" CssClass="form-control" placeholder="Ingrese una area.."></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <br />
                            <div class="col-md-12"  style="text-align:center">
                                <label class="control-label text-danger" style="text-align:center" >Los campos con (*) son obligatorios</label>
                            </div>

                            <div class="modal-footer">
                                <asp:UpdatePanel ID="UpdateModificacionBotones" runat="server">
                                    <ContentTemplate>
                                        <%--<button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>--%>
                                        <asp:Button ID="BtnCancelar" runat="server" Text="Cancelar" class="btn  btn-dark" OnClick="BtnCancelar_Click" />
                                        <asp:Button ID="BtnEnviar" runat="server" Text="Enviar" class="btn btn-success" OnClick="BtnEnviar_Click" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <br />

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
                     <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>

                                <div class="row p-t-20">
                                    <div class="col-md-1">
                                      <label class="control-label">Buscar:</label></label>                                      
                                    </div>
                                    <div class="col-md-6">
                                       <asp:TextBox ID="TxBuscarArea" runat="server" placeholder="Búsqueda por area o id, luego presione Enter" class="form-control" AutoPostBack="true" OnTextChanged="TxBuscarArea_TextChanged"></asp:TextBox>
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
                                <asp:UpdatePanel runat="server" ID="UPAreas">
                                    <ContentTemplate>
                                        <asp:GridView ID="GVAreas" runat="server"
                                            CssClass="table table-bordered"
                                            PagerStyle-CssClass="pgr"
                                            HeaderStyle-CssClass="table" HeaderStyle-HorizontalAlign="center"
                                            RowStyle-CssClass="rows"
                                            AutoGenerateColumns="false"
                                            AllowPaging="true"
                                            GridLines="None" OnPageIndexChanging="GVAreas_PageIndexChanging"
                                            PageSize="10" OnRowCommand="GVAreas_RowCommand">

                                            <Columns>
                                                <asp:TemplateField HeaderText="Acción" ItemStyle-HorizontalAlign="center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LbModificar" class="btn btn-info mr-2" runat="server"  CommandName="Modifcar" CommandArgument='<%# Eval("idAreaAgencia") %>'>
                                                         <i class="icon-pencil" ></i>
                                                        </asp:LinkButton>    

                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="idAreaAgencia" HeaderText="Id" ControlStyle-Width="10%" />
                                                <asp:BoundField DataField="nombre" HeaderText="Area" ControlStyle-Width="50%" />
                                                <asp:BoundField DataField="estado" HeaderText="Estado" ControlStyle-Width="30%" />                                                                          
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
    <%--INICIO MODAL --%>
    <div class="modal fade" id="modalModificarArea" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="width: 600px; top: 320px; left: 50%; transform: translate(-50%, -50%);">
                <div class="modal-header">

                    <h3 class="modal-title" id="exampleModalLabel">
                        <asp:Label ID="Titulo" runat="server" Text="Modificar Area" Style="margin-left: auto; margin-right: auto"></asp:Label>
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
                                    <label class="col-md-3 col-form-label">Id Area:</label>
                                    <div class="col-md-9">
                                        <asp:TextBox ID="TxIdAreaModal" MaxLength="150" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12" runat="server">
                                <div class="form-group row">
                                    <div class="col-md-3">
                                         <label class="control-label text-danger">*</label><label class="col-form-label">Area:</label>
                                    </div>

                                    <div class="col-md-9">
                                        <asp:TextBox ID="TxAreaModal" class="form-control" runat="server" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12" runat="server">
                                <div class="form-group row">
                                    <div class="col-md-3">
                                        <label class="control-label text-danger">*</label><label class="col-form-label">Estado:</label>
                                    </div>

                                    
                                    <div class="col-md-9">
                                       <asp:DropDownList ID="DdlEstadoArea" runat="server" class="form-control" AutoPostBack="true">                                              
                                                <asp:ListItem Value="False" Text="Inactivo"></asp:ListItem>
                                                <asp:ListItem Value="True" Text="Activo"></asp:ListItem>                                                                                              
                                            </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12"  style="text-align:center">
                                <label class="control-label text-danger" style="text-align:center" >Los campos con (*) son obligatorios</label>
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
                            <asp:Button ID="btnModalModificarEstado" runat="server" Text="Enviar" class="btn btn-info mr-2" OnClick="btnModalModificarEstado_Click" />
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
