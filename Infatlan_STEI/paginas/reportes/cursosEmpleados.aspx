<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="cursosEmpleados.aspx.cs" Inherits="Infatlan_STEI.paginas.reportes.cursosEmpleados" %>
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
        function openModal() { $('#ModalCursoEmpleado').modal('show'); }
        function cerrarModal() { $('#ModalCursoEmpleado').modal('hide'); }
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
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Reportes</a></li>
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Cumplimiento</a></li>
                    <li class="breadcrumb-item active">Evaluaciones</li>
                </ol>
            </div>
        </div>
    </div>

    <asp:UpdatePanel runat="server" ID="UpdatePanel">
        <ContentTemplate>
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Evaluaciones</h4>
                    <h6 class="card-subtitle">Métodos de medición para metas de cumplimiento.</h6>
                    
                    <div class="card-body">
                        <div class="row col-12"> 
                            <div class="col-1">
                                <label class="col-form-label">Búsqueda</label>
                            </div>
                            <div class="col-5">
                                <asp:TextBox runat="server" PlaceHolder="Ingrese texto y presione Enter" ID="TxBusqueda" AutoPostBack="true" OnTextChanged="TxBusqueda_TextChanged" CssClass="form-control form-control-line"></asp:TextBox>
                            </div>
                            <asp:Button runat="server" ID="BtnNuevo" CssClass="btn btn-success mr-2" Text="Nuevo" OnClick="BtnNuevo_Click" />
                            <asp:Button runat="server" ID="BtnCargar" CssClass="btn btn-primary" Text="Cargar" OnClick="BtnCargar_Click" />
                        </div>

                        <div class="table-responsive m-t-20">
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
                                    <asp:BoundField DataField="id" HeaderText="Id"/>
                                    <asp:BoundField DataField="idCurso" HeaderText="Curso"/>
                                    <asp:BoundField DataField="idEmpleado" HeaderText="Empleado"/>
                                    <asp:BoundField DataField="activo" HeaderText="Estado"/>
                                    <asp:BoundField DataField="nota" HeaderText="Nota"/>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="BtnEditar" runat="server" class="btn btn-info" CommandArgument='<%# Eval("id") %>' CommandName="EditarEvaluacion">
                                                <i class="icon-pencil" ></i>
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
    <div class="modal fade" id="ModalCursoEmpleado" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="LbTituloModal" runat="server" Text=""></asp:Label>
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
                                    <div class="col-2" style="margin-left:2%">
                                        <label class="col-form-label">Curso</label>
                                    </div>
                                    <div class="col-9">
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="DDLCursos"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group row col-6">
                                    <div class="col-3">
                                        <label class="col-form-label">Empleado</label>
                                    </div>
                                    <div class="col-7">
                                        <asp:DropDownList runat="server" ID="DDLEmpleado" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                    <div class="col-2">
                                        <asp:Button Text="+" runat="server" CssClass="btn btn-success" ID="BtnAsignar" OnClick="BtnAsignar_Click" />
                                    </div>
                                </div>
                                <div class="col-12" runat="server" id="DivMensaje" visible="false" style="display: flex; background-color:tomato; justify-content:center">
                                    <asp:Label runat="server" CssClass="col-form-label text-white" ID="LbAdvertencia"></asp:Label>
                                </div>
                                <div class="col-12">
                                    <div class="table-responsive m-t-10">
                                        <asp:GridView ID="GvAsignar" runat="server"
                                            CssClass="table table-bordered"
                                            PagerStyle-CssClass="pgr"
                                            HeaderStyle-CssClass="table"
                                            RowStyle-CssClass="rows"
                                            AutoGenerateColumns="false"
                                            AllowPaging="true"
                                            GridLines="None" OnRowCommand="GvAsignar_RowCommand"
                                            PageSize="10" OnPageIndexChanging="GvAsignar_PageIndexChanging">
                                            <Columns>
                                                <asp:BoundField DataField="id" HeaderText="Id" Visible="false"/>
                                                <asp:BoundField DataField="idUsuario" HeaderText="Usuario"/>
                                                <asp:BoundField DataField="nombre" HeaderText="Nombre"/>
                                                <asp:BoundField DataField="curso" HeaderText="Curso"/>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="BtnBorrar" runat="server" class="btn btn-danger" CommandArgument='<%# Eval("id") %>' CommandName="BorrarAsignacion">
                                                            <i class="icon-trash" ></i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
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
