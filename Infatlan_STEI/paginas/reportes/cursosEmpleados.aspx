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
        function openModalCarga() { $('#ModalCarga').modal('show'); }
        function openConfirmacion() { $('#ModalConfirmacion').modal('show'); }
        function cerrarConfirmacion() { $('#ModalConfirmacion').modal('hide'); }
        function openModalNotas() {$('#ModalNotas').modal('show');}
        function cerrarModalNotas() {$('#ModalNotas').modal('hide');}
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
                            <div class="col-3">
                                <asp:DropDownList runat="server" AutoPostBack="true" ID="DDLSortCurso" CssClass="form-control" OnSelectedIndexChanged="DDLSortCurso_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="col-5">
                                <asp:TextBox runat="server" PlaceHolder="Ingrese texto y presione Enter" ID="TxBusqueda" AutoPostBack="true" OnTextChanged="TxBusqueda_TextChanged" CssClass="form-control form-control-line"></asp:TextBox>
                            </div>
                            <div runat="server" id="DivCrear" visible="false">
                                <asp:Button runat="server" ID="BtnNuevo" CssClass="btn btn-success mr-2" Text="Nuevo" OnClick="BtnNuevo_Click" />
                                <button type="button" class="btn btn-primary" onclick="openModalCarga();">
                                    <span class="icon-arrow-up-circle"></span> Carga
                                </button>
                            </div>
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
                                    <asp:BoundField DataField="nombre" HeaderText="Curso"/>
                                    <asp:BoundField DataField="idUsuario" HeaderText="Empleado"/>
                                    <asp:BoundField DataField="activo" HeaderText="Estado"/>
                                    <asp:BoundField DataField="nota" HeaderText="Nota"/>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="BtnEditar" Visible="false" runat="server" class="btn btn-info" CommandArgument='<%# Eval("id") %>' CommandName="EditarEvaluacion">
                                                <i class="icon-note" ></i>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="BtnBorrar" Visible="false" runat="server" class="btn btn-primary" CommandArgument='<%# Eval("id") %>' CommandName="borrarAsignacion">
                                                <i class="icon-trash" ></i>
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

    <%--MODAL DE NOTAS--%>
    <div class="modal fade" id="ModalNotas" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="LbTituloNotas" runat="server" Text=""></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="form-group row col-12">
                                    <div class="col-3" style="margin-left:2%">
                                        <label class="col-form-label">Empleado:</label>
                                    </div>
                                    <div class="col-8">
                                        <asp:TextBox runat="server" CssClass="form-control" ReadOnly="true" ID="TxEmpleado" />
                                    </div>
                                </div>
                                <div class="form-group row col-12">
                                    <div class="col-3" style="margin-left:2%">
                                        <label class="col-form-label">Curso:</label>
                                    </div>
                                    <div class="col-8">
                                        <asp:TextBox runat="server" CssClass="form-control" ReadOnly="true" ID="TxCurso" />
                                    </div>
                                </div>
                                <div class="form-group row col-12">
                                    <div class="col-3" style="margin-left:2%">
                                        <label class="col-form-label">Nota</label>
                                    </div>
                                    <div class="col-8">
                                        <asp:TextBox runat="server" TextMode="Number" min="0" step="1" max="100" ID="TxNota" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-12" runat="server" id="DivMensajeNota" visible="false" style="display: flex; background-color:tomato; justify-content:center">
                                    <asp:Label runat="server" CssClass="col-form-label text-white" ID="LbAdvertenciaNota"></asp:Label>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                            <asp:Button ID="BtnEvaluar" runat="server" Text="Evaluar" class="btn btn-success" OnClick="BtnEvaluar_Click"/>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <%--MODAL DE CONFIRMACION--%>
    <div class="modal fade" id="ModalConfirmacion" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="LbConfirmacion" runat="server" Text=""></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </h4>
                </div>
                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                        <ContentTemplate>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                            <asp:Button ID="BtnCornfirmar" runat="server" Text="Borrar" class="btn btn-primary" OnClick="BtnCornfirmar_Click"/>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <%--MODAL DE CARGA--%>
    <div class="modal fade" id="ModalCarga" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Carga Masiva de Calificaciones</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="form-group row col-12">
                                    <div class="col-3" style="margin-left:2%">
                                        <label>Plantilla:</label>
                                    </div>
                                    <div class="col-8">
                                        <a href="../plantillas/plantillaEvaluacion.xlsx">Descargar</a>
                                    </div>
                                </div>
                                <div class="form-group row col-12">
                                    <div class="col-3" style="margin-left:2%">
                                        <label class="col-form-label">Cargar:</label>
                                    </div>
                                    <div class="col-8">
                                        <asp:FileUpload runat="server" ID="FUEvaluar" CssClass="form-control"/>
                                    </div>
                                </div>
                                <div class="col-12" runat="server" id="DivAdvertencia" visible="false" style="display: flex; background-color:tomato; justify-content:center">
                                    <asp:Label runat="server" CssClass="col-form-label text-white" ID="LbAdvertenciaCarga"></asp:Label>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                        <ContentTemplate>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                            <asp:Button ID="BtnInsertar" runat="server" Text="Aceptar" class="btn btn-success" OnClick="BtnInsertar_Click"/>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="BtnInsertar" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <script src="assets/node_modules/bootstrap-touchspin/dist/jquery.bootstrap-touchspin.min.js"></script>
    <script>
        $(function () {
            $("input[name='tch3']").TouchSpin();
        });
    </script>
</asp:Content>
