<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="articulos.aspx.cs" Inherits="Infatlan_STEI_Inventario.pages.articulos" %>
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
        function openModal() { $('#ModalArticulos').modal('show'); }
        function cerrarModal() { $('#ModalArticulos').modal('hide'); }

        function openArticuloTipo() { $('#ModalArticulosTipo').modal('show'); }
        function cerrarArticuloTipo() { $('#ModalArticulosTipo').modal('hide'); }

        function openProv() { $('#ModalProveedores').modal('show'); }
        function cerrarProv() { $('#ModalProveedores').modal('hide'); }

        function openMarca() { $('#ModalMarcas').modal('show'); }
        function cerrarMarca() { $('#ModalMarcas').modal('hide'); }

        function ModalConfirmar() {
            $('#ModalConfirmar').modal('show');
            document.getElementById('tch3').value = '';
        }
        function closeConfirmar() { $('#ModalConfirmar').modal('hide'); }
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
                    <h4 class="card-title">Almacén</h4>
                    <h6 class="card-subtitle">Material o equipo que forma parte del inventario.</h6>
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
                            CssClass="table table-bordered embed-responsive"
                            PagerStyle-CssClass="pgr"
                            HeaderStyle-CssClass="table"
                            RowStyle-CssClass="rows"
                            AutoGenerateColumns="false"
                            AllowPaging="true"
                            GridLines="None" OnRowCommand="GVBusqueda_RowCommand"
                            PageSize="10" OnPageIndexChanging="GVBusqueda_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="idStock" HeaderText="No." />
                                <asp:BoundField DataField="TipoStock" HeaderText="TipoStock" />
                                <asp:BoundField DataField="Marca" HeaderText="Marca" />
                                <asp:BoundField DataField="modelo" HeaderText="Modelo" />
                                <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
                                <asp:BoundField DataField="precioUnit" HeaderText="Precio" />
                                <asp:BoundField DataField="Proveedor" HeaderText="Proveedor"/>
                                <asp:BoundField DataField="descripcion" HeaderText="Detalle" />
                                <asp:BoundField DataField="series" HeaderText="Serie" />
                                <asp:TemplateField HeaderText="Seleccione" >
                                    <ItemTemplate>
                                        <asp:LinkButton ID="BtnEditar" Style="padding:8%" runat="server" class="btn btn-info mr-2" Title="Editar" CommandArgument='<%# Eval("idStock") %>' CommandName="EditarArticulo">
                                            <i class="icon-pencil"></i>
                                        </asp:LinkButton>
                            
                                        <asp:LinkButton ID="BtnEditar2" Style="padding:8%" runat="server" class="btn btn-success mr-2" Title="Aregar" CommandArgument='<%# Eval("idStock") %>' CommandName="EliminarArticulo">
                                            <i class="icon-plus" ></i>
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
            
    <%--MODAL DE ARTICULOS--%>
    <div class="modal fade" id="ModalArticulos" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="ModalLabelModificacion">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="LbIdArticulo" runat="server" Text=""></asp:Label>
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
                                <div class="col-6">
                                    <div class="form-group row">
                                        <div class="col-2" style="margin-left:2%">
                                            <label>Tipo de Artículo</label>
                                        </div>
                                        <div class="col-7">
                                            <asp:DropDownList ID="DDLTipo" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                        <div class="col-1">
                                            <asp:Button Text="+" OnClick="BtnAddArticulo_Click" ID="BtnAddArticulo" CssClass="btn btn-success" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-6">
                                    <div class="form-group row">
                                        <div class="col-3">
                                            <label class="col-form-label">Proveedor</label>
                                        </div>
                                        <div class="col-7">
                                            <asp:DropDownList ID="DDLProveedor" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                        <div class="col-1">
                                            <asp:Button Text="+" OnClick="BtnAddProv_Click" ID="BtnAddProv" CssClass="btn btn-success" runat="server" />
                                        </div>
                                    </div>
                                </div>

                                <div class="col-6">
                                    <div class="form-group row">
                                        <div class="col-2" style="margin-left:2%">
                                            <label class="col-form-label">Modelo</label>
                                        </div>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="TxModelo" placeholder="" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-6">
                                    <div class="form-group row">
                                        <div class="col-2" >
                                            <label class="col-form-label">Marca</label>
                                        </div>
                                        <div class="col-8">
                                            <asp:DropDownList runat="server" ID="DDLMarca" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                        <div class="col-1">
                                            <asp:Button Text="+" OnClick="BtnAddMarca_Click" ID="BtnAddMarca" CssClass="btn btn-success" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-6">
                                    <div class="form-group row">
                                        <div class="col-3" style="margin-left:2%">
                                            <label class="">Precio Unitario</label>
                                        </div>
                                        <div class="col-8">
                                            <asp:TextBox ID="TxPrecio" placeholder="" TextMode="Number" class="form-control" runat="server"></asp:TextBox>                                            
                                        </div>
                                    </div>
                                </div>
                                <div class="col-6">
                                    <div class="form-group row">
                                        <div class="col-2">
                                            <label class="col-form-label">Detalle</label>
                                        </div>
                                        <div class="col-10">
                                            <asp:TextBox ID="TxDetalle" placeholder="" class="form-control" runat="server"></asp:TextBox>                                            
                                        </div>
                                    </div>
                                </div>
                                <div class="col-6">
                                    <div class="form-group row">
                                        <div class="col-2" style="margin-left:2%">
                                            <label class="col-form-label">Serie</label>
                                        </div>
                                        <div class="col-9">
                                            <asp:TextBox ID="TxSerie" placeholder="" class="form-control" runat="server"></asp:TextBox>                                            
                                        </div>
                                    </div>
                                </div>
                                <div class="col-6">
                                    <div class="form-group row">
                                        <div class="col-2">
                                            <label class="col-form-label">Estado</label>
                                        </div>
                                        <div class="col-10">
                                            <asp:DropDownList runat="server" ID="DDLEstado" CssClass="form-control">
                                                <asp:ListItem Value="1" Text="Activo"></asp:ListItem>
                                                <asp:ListItem Value="0" Text="Inactivo"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-group row">
                                        <label style="margin-right:5%" class="control-label text-right col-md-3">Categorías:</label>
                                        <div class="form-group">
                                            <label class="custom-control custom-checkbox m-b-0">
                                                <input type="checkbox" runat="server" id="CBxAgencia" class="custom-control-input">
                                                <span class="custom-control-label">Agencias</span>
                                            </label>

                                            <label class="custom-control custom-checkbox m-b-0">
                                                <input type="checkbox" runat="server" id="CBxATM" class="custom-control-input">
                                                <span class="custom-control-label">ATM</span>
                                            </label>

                                            <label class="custom-control custom-checkbox m-b-0">
                                                <input type="checkbox" runat="server" id="CBxCE" class="custom-control-input">
                                                <span class="custom-control-label">Cableado Estructurado</span>
                                            </label>
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

    <%--MODAL DE PROVEEDORES--%>
    <div class="modal fade" id="ModalProveedores" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header table-dark">
                    <h4 class="modal-title">
                        <asp:Label CssClass=" text-white" ID="LbIdProveedor" runat="server" Text="Agregar Proveedor"></asp:Label>
                    </h4>
                    <button type="button" class="close text-white" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-12">
                                    <div class="form-group row">
                                        <div class="col-2" style="margin-left:2%">
                                            <label class="col-form-label">Nombre</label>
                                        </div>
                                        <div class="col-9">
                                            <asp:TextBox ID="TxNombreProv" class="form-control text-uppercase" runat="server"></asp:TextBox>                                            
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="form-group row">
                                        <div class="col-3" style="margin-left:2%">
                                            <label class="col-form-label">Responsable</label>
                                        </div>
                                        <div class="col-8">
                                            <asp:TextBox ID="TxResponsableProv" class="form-control" runat="server"></asp:TextBox>                                            
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12">
                                    <div class="form-group row">
                                        <div class="col-2" style="margin-left:2%">
                                            <label class="col-form-label">Teléfono</label>
                                        </div>
                                        <div class="col-9">
                                            <asp:TextBox ID="TxTelefonoProv" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="form-group row">
                                        <div class="col-3" style="margin-left:2%">
                                            <label class="col-form-label">Direccion</label>
                                        </div>
                                        <div class="col-8">
                                            <asp:TextBox ID="TxDireccionProv" TextMode="MultiLine" class="form-control" runat="server"></asp:TextBox>                                                                                        
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12" runat="server" id="DivMensajeProv" visible="false" style="display: flex; background-color:tomato; justify-content:center">
                                    <asp:Label runat="server" CssClass="col-form-label text-white" ID="LbMensajeProv"></asp:Label>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                        <ContentTemplate>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                            <asp:Button ID="BtnAgregarProv" runat="server" Text="Aceptar" class="btn btn-success" OnClick="BtnAgregarProv_Click"/>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <%--MODAL DE TIPO ARTICULOS--%>
    <div class="modal fade" id="ModalArticulosTipo" role="dialog" aria-labelledby="exampleModalLabel">
        <div class="modal-dialog modal-sm modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header table-dark">
                    <h4 class="modal-title" id="ModalLabelModificacionTipo">
                        <asp:Label CssClass=" text-white" ID="Label1" runat="server" Text="Tipo de Artículo"></asp:Label>
                    </h4>
                    <button type="button" class="close text-white" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-12">
                                    <div class="form-group row">
                                        <div class="col-12" style="margin-left:2%">
                                            <label>Nombre</label>
                                        </div>
                                        <div class="col-12">
                                            <asp:TextBox ID="TxNombreTA" placeholder="" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="form-group row">
                                        <div class="col-12">
                                            <label class="col-form-label">Descripción</label>
                                        </div>
                                        <div class="col-12">
                                            <asp:TextBox ID="TxDescripcion" Rows="3" placeholder="" class="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>                                            
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12" runat="server" id="DivMensajeTA" visible="false" style="display: flex; background-color:tomato; justify-content:center">
                                    <asp:Label runat="server" CssClass="col-form-label text-white" ID="LbMensajeTA"></asp:Label>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                            <asp:Button ID="BtnAgregarTA" runat="server" Text="Aceptar" class="btn btn-success" OnClick="BtnAgregarTA_Click"/>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <%--MODAL DE MARCAS--%>
    <div class="modal fade" id="ModalMarcas" role="dialog" aria-labelledby="exampleModalLabel">
        <div class="modal-dialog modal-sm modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header table-dark">
                    <h4 class="modal-title" id="ModalLabelMarcas">
                        <asp:Label CssClass=" text-white" ID="Label2" runat="server" Text="Crear Marca"></asp:Label>
                    </h4>
                    <button type="button" class="close text-white" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-12">
                                    <div class="form-group row">
                                        <div class="col-12" style="margin-left:2%">
                                            <label>Nombre</label>
                                        </div>
                                        <div class="col-12">
                                            <asp:TextBox ID="TxNombreMarca" placeholder="" class="form-control text-uppercase" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12" runat="server" id="DivMensajeMarca" visible="false" style="display: flex; background-color:tomato; justify-content:center">
                                    <asp:Label runat="server" CssClass="col-form-label text-white" ID="LbMensajeMarca"></asp:Label>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                        <ContentTemplate>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                            <asp:Button ID="BtnAgregarMarca" runat="server" Text="Aceptar" class="btn btn-success" OnClick="BtnAgregarMarca_Click"/>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <%--MODAL DE CONFIRMACION--%>
    <div class="modal fade" id="ModalConfirmar" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <h4 class="modal-title" id="ModalLabelConfirmar">
                                <b><asp:Label runat="server" ID="LbTitulo" CssClass="col-form-label"></asp:Label></b>
                            </h4>
                            <asp:Label runat="server" ID="LbMensaje" CssClass="col-form-label"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label class="control-label col-12">Motivo</label>
                        <asp:DropDownList ID="DDLTipoTransaccion" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-12">Cantidad</label>
                        <input id="tch3" type="text" value="" name="tch3" data-bts-button-down-class="btn btn-secondary btn-outline" data-bts-button-up-class="btn btn-secondary btn-outline">
                    </div>
                    <div class="row">
                        <div class="col-12" runat="server" id="DivMensajeCant" visible="false" style="display: flex; background-color:tomato; justify-content:center">
                            <asp:Label runat="server" CssClass="col-form-label text-white" ID="LbMensajeCant"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                            <asp:Button ID="BtnConfirmar" runat="server" Text="Aceptar" class="btn btn-info" OnClick="BtnConfirmar_Click"/>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <script src="../assets/node_module/bootstrap-touchspin/dist/jquery.bootstrap-touchspin.js" type="text/javascript"></script>
    <script>
        $(function () {
            $("input[name='tch3']").TouchSpin();
        });
    </script>
</asp:Content>
