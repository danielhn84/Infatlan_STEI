<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="usuarios.aspx.cs" Inherits="Infatlan_STEI.paginas.configuraciones.usuarios" %>
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
        function openModal() { $('#ModalUser').modal('show'); }
        function cerrarModal() { $('#ModalUser').modal('hide'); }
        function ModalConfirmar() { $('#ModalConfirmar').modal('show'); }
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
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Configuración</a></li>
                    <li class="breadcrumb-item active">Usuarios</li>
                </ol>
            </div> 
        </div>
    </div>

    <asp:UpdatePanel runat="server" ID="UpdatePanel">
        <ContentTemplate>
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Usuarios</h4>
                    <h6 class="card-subtitle">Usuarios del sistema.</h6>
                    <div class="card-body">
                        <div class="row col-7"> 
                            <label class="col-2 col-form-label">Búsqueda</label>
                            <div class="col-8">
                                <asp:TextBox runat="server" PlaceHolder="Ingrese texto y presione Enter" ID="TxBusqueda" AutoPostBack="true" OnTextChanged="TxBusqueda_TextChanged" CssClass="form-control form-control-line"></asp:TextBox>
                            </div>
                            <asp:Button runat="server" ID="BtnNuevo" CssClass="btn btn-success" Text="Nuevo" OnClick="BtnNuevo_Click" />
                        </div>

                        <div class="table-responsive m-t-40">
                            <asp:GridView ID="GVBusqueda" runat="server"
                                CssClass="table table-bordered"
                                PagerStyle-CssClass="pgr"
                                HeaderStyle-CssClass="table"
                                RowStyle-CssClass="rows"
                                AutoGenerateColumns="false"
                                AllowPaging="true"
                                GridLines="None" OnRowCommand="GVBusqueda_RowCommand"
                                PageSize="10" OnPageIndexChanging="GVBusqueda_PageIndexChanging">
                                <Columns>
                                    <asp:BoundField DataField="idUsuario" HeaderText="No."/>
                                    <asp:BoundField DataField="nombre" HeaderText="Nombres"/>
                                    <asp:BoundField DataField="apellidos" HeaderText="Apellidos"/>
                                    <asp:BoundField DataField="telefono" HeaderText="Telefono"/>
                                    <asp:BoundField DataField="correo" HeaderText="Correo"/>
                                    <asp:BoundField DataField="identidad" HeaderText="Identidad"/>
                                    <asp:BoundField DataField="EsadoDesc" HeaderText="Estado"/>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="BtnEditar" runat="server" Title="Editar" class="btn btn-info" CommandArgument='<%# Eval("idUsuario") %>' CommandName="EditarUser">
                                                <i class="icon-pencil"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <%--MODAL DE MODIFICACION--%>
    <div class="modal fade" id="ModalUser" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="ModalLabelModificacion">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="LbIdUser" runat="server" Text=""></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanelModal" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="form-group row col-6">
                                    <div class="col-3" style="margin-left:2%">
                                        <label class="col-form-label">Usuario</label>
                                    </div>
                                    <div class="col-8">
                                        <asp:TextBox ID="TxUsuario" class="form-control" runat="server"></asp:TextBox>                                            
                                    </div>
                                </div>
                                <div class="form-group row col-6">
                                    <div class="col-4">
                                        <label class="col-form-label">SysAid</label>
                                    </div>
                                    <div class="col-8">
                                        <asp:TextBox ID="TxSysAid" class="form-control" runat="server"></asp:TextBox>                                            
                                    </div>
                                </div>
                                <div class="form-group row col-6">
                                    <div class="col-3" style="margin-left:2%">
                                        <label class="col-form-label">Nombres</label>
                                    </div>
                                    <div class="col-8">
                                        <asp:TextBox ID="TxNombres" class="form-control text-uppercase" runat="server"></asp:TextBox>                                            
                                    </div>
                                </div>
                                <div class="form-group row col-6">
                                    <div class="col-4">
                                        <label class="col-form-label">Apellidos</label>
                                    </div>
                                    <div class="col-8">
                                        <asp:TextBox ID="TxApellidos" class="form-control text-uppercase" runat="server"></asp:TextBox>                                            
                                    </div>
                                </div>
                                <div class="form-group row col-6">
                                    <div class="col-3" style="margin-left:2%">
                                        <label class="col-form-label">Identidad</label>
                                    </div>
                                    <div class="col-8">
                                        <asp:TextBox ID="TxIdentidad" class="form-control" runat="server"></asp:TextBox>                                            
                                    </div>
                                </div>
                                <div class="form-group row col-6">
                                    <div class="col-4">
                                        <label class="col-form-label">Telefono</label>
                                    </div>
                                    <div class="col-8">
                                        <asp:TextBox ID="TxTelefono" class="form-control" runat="server"></asp:TextBox>                                            
                                    </div>
                                </div>
                                <div class="form-group row col-6">
                                    <div class="col-3" style="margin-left:2%">
                                        <label class="col-form-label">Correo</label>
                                    </div>
                                    <div class="col-8">
                                        <asp:TextBox ID="TxCorreo" class="form-control" runat="server"></asp:TextBox>                                            
                                    </div>
                                </div>
                                <div class="form-group row col-6">
                                    <div class="col-4">
                                        <label class="col-form-label">Departamento</label>
                                    </div>
                                    <div class="col-8">
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="DDLDepartamento"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group row col-6">
                                    <div class="col-3" style="margin-left:2%">
                                        <label class="col-form-label">Jefe</label>
                                    </div>
                                    <div class="col-8">
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="DDLJefe" OnSelectedIndexChanged="DDLJefe_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group row col-6">
                                    <div class="col-4">
                                        <label class="col-form-label">Grupo</label>
                                    </div>
                                    <div class="col-8">
                                        <asp:DropDownList runat="server" ID="DDLGroups" CssClass="form-control">
                                            <asp:ListItem Value="0" Text="Seleccione una opción"></asp:ListItem>
                                            <asp:ListItem Value="Infa-TGU" Text="Infa-TGU"></asp:ListItem>
                                            <asp:ListItem Value="Infa-SPS" Text="Infa-SPS"></asp:ListItem>
                                            <asp:ListItem Value="Infa-CBA" Text="Infa-CBA"></asp:ListItem>
                                            <asp:ListItem Value="Infa-COMUNICACIONES" Text="Infa-COMUNICACIONES"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group row col-6">
                                    <div class="col-3" style="margin-left:2%">
                                        <label class="col-form-label">Foráneo</label>
                                    </div>
                                    <div class="col-8">
                                        <label class="custom-control custom-radio">
                                            <input type="radio" runat="server" id="CBxForaneo" class="custom-control-input">
                                            <span class="custom-control-label" for="CBxForaneo">SI</span>
                                        </label>

                                        <label class="custom-control custom-radio">
                                            <input type="radio" runat="server" id="CBxNoForaneo" class="custom-control-input">
                                            <span class="custom-control-label" for="CBxNoForaneo">NO</span>
                                        </label>
                                    </div>
                                </div>

                                <div class="form-group row col-6" runat="server" visible="false" id="DivEstado">
                                    <div class="col-4">
                                        <label class="col-form-label">Estado</label>
                                    </div>
                                    <div class="col-8">
                                        <asp:DropDownList runat="server" ID="DDLEstado" CssClass="form-control">
                                            <asp:ListItem Value="1" Text="Activo"></asp:ListItem>
                                            <asp:ListItem Value="0" Text="Inactivo"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12" runat="server" id="DivMensaje" visible="false" style="display: flex; background-color:tomato; justify-content:center">
                                <asp:Label runat="server" CssClass="col-form-label text-white" ID="LbAdvertencia"></asp:Label>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdateModificacionBotones" runat="server">
                        <ContentTemplate>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                            <asp:Button ID="BtnAceptar" runat="server" Text="Aceptar" class="btn btn-success" OnClick="BtnAceptar_Click"/>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
