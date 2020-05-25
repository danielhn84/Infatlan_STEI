<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="contratos.aspx.cs" Inherits="Infatlan_STEI_Inventario.pages.Configuracion.contratos" %>
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
        function openModal() { $('#ModalContratos').modal('show'); }
        function openModalCond() { $('#ModalCondiciones').modal('show'); }
        function cerrarModal() { $('#ModalContratos').modal('hide'); }
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
    <br /> 
    <asp:UpdatePanel runat="server" ID="UpdatePanel">
        <ContentTemplate>
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Contratos</h4>
                    <h6 class="card-subtitle">Relacionados al producto existente en inventario.</h6>
                    <br />
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
                                <asp:BoundField DataField="idContrato" HeaderText="No."/>
                                <asp:BoundField DataField="contrato" HeaderText="Contrato"/>
                                <asp:BoundField DataField="tipoContrato" HeaderText="Tipo"/>
                                <asp:BoundField DataField="proveedor" HeaderText="Proveedor"/>
                                <asp:BoundField DataField="fechaInicio" HeaderText="Inicio"/>
                                <asp:BoundField DataField="fechaFinal" HeaderText="Fin"/>
                                <asp:TemplateField HeaderText="Seleccione">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="BtnEditar" runat="server" class="btn btn-info mr-2" Title="Editar" CommandArgument='<%# Eval("idContrato") %>' CommandName="EditarContrato">
                                            <i class="icon-pencil" ></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="BtnCondiciones" runat="server" class="btn btn-success mr-2" Title="Condiciones" CommandArgument='<%# Eval("idContrato") %>' CommandName="verCondiciones">
                                            <i class="icon-layers" ></i>
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

    <%--MODAL DE MODIFICACION--%>
    <div class="modal fade" id="ModalContratos" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="ModalLabelModificacion">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="LbIdContrato" runat="server" Text=""></asp:Label>
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
                                <div class="col-12">
                                    <div class="form-group row">
                                        <div class="col-3" style="margin-left:2%">
                                            <label class="col-form-label">Contrato No.</labe>
                                        </div>
                                        <div class="col-8">
                                            <asp:TextBox ID="TxContrato" class="form-control text-uppercase" runat="server"></asp:TextBox>                                            
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="form-group row">
                                        <div class="col-3" style="margin-left:2%">
                                            <label class="col-form-label">Tipo de contrato</label>
                                        </div>
                                        <div class="col-8">
                                            <asp:DropDownList runat="server" ID="DDLTipoContrato" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="form-group row">
                                        <div class="col-3" style="margin-left:2%">
                                            <label class="col-form-label">Proveedor</label>
                                        </div>
                                        <div class="col-8">
                                            <asp:DropDownList runat="server" ID="DDLProveedores" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="form-group row">
                                        <div class="col-3" style="margin-left:2%">
                                            <label class="col-form-label">Fecha Inicial</label>
                                        </div>
                                        <div class="col-8">
                                            <asp:TextBox ID="TxFechaInicio" AutoPostBack="true" class="form-control" runat="server" TextMode="Date"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="form-group row">
                                        <div class="col-3" style="margin-left:2%">
                                            <label class="col-form-label">Fecha Final</label>
                                        </div>
                                        <div class="col-8">
                                            <asp:TextBox ID="TxFechaFin" AutoPostBack="true" class="form-control" runat="server" TextMode="Date"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="form-group row">
                                        <div class="col-3" style="margin-left:2%">
                                            <label class="col-form-label">Condiciones</label>
                                        </div>
                                        <div class="col-8">
                                            <asp:TextBox ID="TxCondiciones" class="form-control" runat="server" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12" runat="server" visible="false" id="DivEstado">
                                    <div class="form-group row">
                                        <div class="col-2" style="margin-left:2%">
                                            <label class="col-form-label">Estado</label>
                                        </div>
                                        <div class="col-9">
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

    <%--MODAL CONDICIONES--%>
    <div class="modal fade" id="ModalCondiciones" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="ModalLabelCondiciones">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="LbTituloCondicion" runat="server" Text=""></asp:Label>
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
                                <div class="col-12">  
                                    <asp:Label ID="LbContenido" Text="" runat="server" CssClass="col-form-label"/>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
