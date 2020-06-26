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

        function openModalEDC() { $('#ModalArticulosEDC').modal('show'); }
        function cerrarModalEDC() { $('#ModalArticulosEDC').modal('hide'); }
        function openModalInfoEDC() { $('#ModalInfoEDC').modal('show'); }

        function openModalEnlace() { $('#ModalArticulosEnlaces').modal('show'); }
        function cerrarModalEnlace() { $('#ModalArticulosEnlaces').modal('hide'); }
        function openModalInfoEnlace() { $('#ModalInfoENL').modal('show'); }
        function openModalAdjunto() { $('#ModalCarga').modal('show'); }

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
    <link href="../assets/node_module/select2/dist/css/select2.min.css" rel="stylesheet" type="text/css" />
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
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Inventario</a></li>
                    <li class="breadcrumb-item active">Almacén</li>
                </ol>
            </div>
        </div>
    </div>

    <div class="card">
        <div class="card-body">
            <h4 class="card-title">Almacén</h4>
            <h6 class="card-subtitle">Material o equipo que forma parte del inventario.</h6>
            <nav>
                <div class="nav nav-pills " id="nav-tab" role="tablist">
                    <a class="nav-item nav-link active" id="nav_cargar_tab" data-toggle="tab" href="#navNuevo" role="tab" aria-controls="nav-profile" aria-selected="false"><i class="icon-plus"> </i>General</a>
                    <a runat="server" visible="true" class="nav-item nav-link" id="Registros" data-toggle="tab" href="#navEDC" role="tab" aria-controls="nav-profile" aria-selected="false"><i style="margin-right:5px" class="icon-puzzle"></i>Equipos de comunicación</a>
                    <a runat="server" visible="true" class="nav-item nav-link" id="Enlaces" data-toggle="tab" href="#navEnlace" role="tab" aria-controls="nav-profile" aria-selected="false"><i style="margin-right:5px" class="icon-vector"></i>Enlaces</a>
                </div>
            </nav>
            <hr />
            <div class="tab-content" id="nav-tabContent">
                <div class="tab-pane fade show active" id="navNuevo" role="tabpanel" aria-labelledby="nav-cargar-tab">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel">
                        <ContentTemplate>
                            <div class="card-body">
                                <div class="row col-7">
                                    <label class="col-2 col-form-label">Búsqueda</label>
                                    <div class="col-8">
                                        <asp:TextBox runat="server" PlaceHolder="Ingrese Id o Tipo y presione Enter" ID="TxBusqueda" AutoPostBack="true" OnTextChanged="TxBusqueda_TextChanged" CssClass="form-control form-control-line"></asp:TextBox>
                                    </div>
                                    <asp:Button runat="server" ID="BtnNuevo" CssClass="btn btn-success" Text="Nuevo" OnClick="BtnNuevo_Click" />
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
                                            <asp:BoundField DataField="idStock" HeaderText="Id" />
                                            <asp:BoundField DataField="TipoStock" HeaderText="Tipo" />
                                            <asp:BoundField DataField="Marca" HeaderText="Marca" />
                                            <asp:BoundField DataField="modelo" HeaderText="Modelo" />
                                            <asp:BoundField DataField="cantidad" HeaderText="Cant" />
                                            <asp:BoundField DataField="precioUnit" HeaderText="Precio" />
                                            <asp:BoundField DataField="Proveedor" HeaderText="Proveedor" />
                                            <asp:BoundField DataField="descripcion" HeaderText="Detalle" />
                                            <asp:BoundField DataField="series" HeaderText="Serie" />
                                            <asp:TemplateField HeaderText="" HeaderStyle-Width="120">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="BtnEditar" runat="server" class="btn btn-info" Title="Editar" CommandArgument='<%# Eval("idStock") %>' CommandName="EditarArticulo">
                                                        <i class="icon-pencil"></i>
                                                    </asp:LinkButton>

                                                    <asp:LinkButton ID="BtnEditar2" runat="server" class="btn btn-success" Title="Aregar" CommandArgument='<%# Eval("idStock") %>' CommandName="EliminarArticulo">
                                                        <i class="icon-plus" ></i>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <div class="tab-pane fade" id="navEDC" role="tabpanel" aria-labelledby="nav-tecnicos-tab">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel10">
                        <ContentTemplate>
                            <div class="card-body">
                                <div class="row col-12">
                                    <div class="row col-7">
                                        <label class="col-2 col-form-label">Búsqueda</label>
                                        <div class="col-8">
                                            <asp:TextBox runat="server" PlaceHolder="Ingrese Id o Nombre y presione Enter" ID="TxBusquedaEDC" AutoPostBack="true" OnTextChanged="TxBusquedaEDC_TextChanged" CssClass="form-control form-control-line"></asp:TextBox>
                                        </div>
                                        <asp:Button runat="server" ID="BtnNuevoEDC" CssClass="btn btn-success" Text="Nuevo" OnClick="BtnNuevoEDC_Click" />
                                    </div>
                                </div>
                            
                                <div class="table-responsive m-t-20">
                                    <asp:GridView ID="GvBusquedaEDC" runat="server"
                                        CssClass="table table-bordered"
                                        PagerStyle-CssClass="pgr"
                                        HeaderStyle-CssClass="table"
                                        RowStyle-CssClass="rows"
                                        AutoGenerateColumns="false"
                                        AllowPaging="true"
                                        GridLines="None" OnRowCommand="GvBusquedaEDC_RowCommand"
                                        PageSize="10" OnPageIndexChanging="GvBusquedaEDC_PageIndexChanging">
                                        <Columns>
                                            <asp:BoundField DataField="idStockEDC" HeaderText="No." />
                                            <asp:BoundField DataField="nombreNodo" HeaderText="Nombre" />
                                            <asp:BoundField DataField="contrato" HeaderText="Contrato" />
                                            <asp:BoundField DataField="serie" HeaderText="Serie" />
                                            <asp:BoundField DataField="ip" HeaderText="IP" />
                                            <asp:BoundField DataField="regiones" HeaderText="Region" />
                                            <asp:BoundField DataField="latitud" HeaderText="Latitud" />
                                            <asp:BoundField DataField="longitud" HeaderText="Longitud" />
                                            <asp:BoundField DataField="fechaMantenimiento" HeaderText="Mantenimiento" />
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="BtnEditar" runat="server" class="btn btn-info" Title="Editar" CommandArgument='<%# Eval("idStockEDC") %>' CommandName="EditarArticuloEDC">
                                                        <i class="icon-pencil"></i>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="BtnInfo" runat="server" class="btn btn-secondary" Title="Ver" CommandArgument='<%# Eval("idStockEDC") %>' CommandName="VerInfoEDC">
                                                        <i class="icon-info"></i>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <div class="tab-pane fade" id="navEnlace" role="tabpanel" aria-labelledby="nav-tecnicos-tab">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel26">
                        <ContentTemplate>
                            <div class="card-body">
                                <div class="row col-7">
                                    <label class="col-2 col-form-label">Búsqueda</label>
                                    <div class="col-8">
                                        <asp:TextBox runat="server" PlaceHolder="Ingrese texto y presione Enter" ID="TxBusquedaEnlace" AutoPostBack="true" OnTextChanged="TxBusquedaEnlace_TextChanged" CssClass="form-control form-control-line"></asp:TextBox>
                                    </div>
                                    <asp:Button runat="server" ID="BtnNuevoEnlace" CssClass="btn btn-success" Text="Nuevo" OnClick="BtnNuevoEnlace_Click" />
                                </div>

                                <div class="table-responsive m-t-20">
                                    <asp:GridView ID="GvEnlaces" runat="server"
                                        CssClass="table table-bordered"
                                        PagerStyle-CssClass="pgr"
                                        HeaderStyle-CssClass="table"
                                        RowStyle-CssClass="rows"
                                        AutoGenerateColumns="false"
                                        AllowPaging="true"
                                        GridLines="None" OnRowCommand="GvEnlaces_RowCommand"
                                        PageSize="10" OnPageIndexChanging="GvEnlaces_PageIndexChanging">
                                        <Columns>
                                            <asp:BoundField DataField="idEnlace" HeaderText="No." />
                                            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                            <asp:BoundField DataField="proveedor" HeaderText="Proveedor" />
                                            <asp:BoundField DataField="tipoEnlace" HeaderText="Tipo" />
                                            <asp:BoundField DataField="origen" HeaderText="Origen" />
                                            <asp:BoundField DataField="destino" HeaderText="Destino" />
                                            <asp:BoundField DataField="servicios" HeaderText="servicios" />
                                            <asp:BoundField DataField="contacto" HeaderText="Contacto" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="BtnEditar" runat="server" class="btn btn-info" Title="Editar" CommandArgument='<%# Eval("idEnlace") %>' CommandName="EditarEnlace">
                                                        <i class="icon-pencil"></i>
                                                    </asp:LinkButton>

                                                    <asp:LinkButton ID="BtnAdjunto" runat="server" class="btn btn-primary" Title="Adjunto" CommandArgument='<%# Eval("idEnlace") %>' CommandName="SubirAdjunto">
                                                        <i class="icon-arrow-up-circle"></i>
                                                    </asp:LinkButton>

                                                    <asp:LinkButton ID="BtnInfo" runat="server" class="btn btn-secondary" Title="Info" CommandArgument='<%# Eval("idEnlace") %>' CommandName="VerInfo">
                                                        <i class="icon-info"></i>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
            
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
                                            <asp:TextBox ID="TxPrecio" placeholder="" TextMode="Number" step="0.01" class="form-control" runat="server"></asp:TextBox>
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

    <%--MODAL DE ARTICULOS EDC--%>
    <div class="modal fade" id="ModalArticulosEDC" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="ModalLabelModificacionEDC">
                        <asp:UpdatePanel ID="UpdatePanel11" runat="server" >
                            <ContentTemplate>
                                <asp:Label ID="LbIdArticuloEDC" runat="server" Text=""></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-6">
                            <div class="form-group row">
                                <div class="col-3" style="margin-left:2%">
                                    <label class="col-form-label">Nombre</label>
                                </div>
                                <div class="col-8">
                                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="TxNombreNodo" class="form-control" runat="server"></asp:TextBox>                                            
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="form-group row">
                                <div class="col-3">
                                    <label class="col-form-label">Tipo</label>
                                </div>
                                <div class="col-8">
                                    <asp:DropDownList ID="DDLTipoEquipoEDC" runat="server" class="select2 form-control custom-select" style="width: 100%"></asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="col-6">
                            <div class="form-group row">
                                <div class="col-3" style="margin-left:2%">
                                    <label class="col-form-label">Contratos</label>
                                </div>
                                <div class="col-8">
                                    <asp:UpdatePanel ID="UpdatePanel16" runat="server" >
                                        <ContentTemplate>
                                            <asp:DropDownList ID="DDLContratos" runat="server" class="select2 form-control custom-select" style="width: 100%"></asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="form-group row">
                                <div class="col-3">
                                    <label class="col-form-label">Serie</label>
                                </div>
                                <div class="col-8">
                                    <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="TxSerieEDC" class="form-control" runat="server"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                                
                        <div class="col-6">
                            <div class="form-group row">
                                <div class="col-3" style="margin-left:2%">
                                    <label class="col-form-label">IP</label>
                                </div>
                                <div class="col-8">
                                    <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="TxIP" placeholder="" class="form-control" runat="server"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="form-group row">
                                <div class="col-3">
                                    <label class="col-form-label">Región</label>
                                </div>
                                <div class="col-8">
                                    <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="DDLRegion" runat="server" class="form-control"></asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                                
                        <div class="col-6">
                            <div class="form-group row">
                                <div class="col-3" style="margin-left:2%">
                                    <label class="col-form-label">IOS Image</label>
                                </div>
                                <div class="col-8">
                                    <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="TxIOSImage" class="form-control" runat="server"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="form-group row">
                                <div class="col-3">
                                    <label class="col-form-label">IOS Version</label>
                                </div>
                                <div class="col-8">
                                    <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="TxIOSVersion" class="form-control" runat="server"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                                
                        <div class="col-6">
                            <div class="form-group row">
                                <div class="col-3" style="margin-left:2%">
                                    <label class="col-form-label">Latitud</label>
                                </div>
                                <div class="col-8">
                                    <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="TxLatitud" class="form-control" runat="server"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="form-group row">
                                <div class="col-3">
                                    <label class="col-form-label">Longitud</label>
                                </div>
                                <div class="col-8">
                                    <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="TxLongitud" class="form-control" runat="server"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>

                        <div class="col-6">
                            <div class="form-group row">
                                <div class="col-3" style="margin-left:2%">
                                    <label class="col-form-label">Ubicación</label>
                                </div>
                                <div class="col-8">
                                    <asp:DropDownList ID="DDLUbicacionEDC" runat="server" class="select2 form-control custom-select" style="width: 100%"></asp:DropDownList>                                            
                                </div>
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="form-group row">
                                <div class="col-4">
                                    <label>Ultimo Mantenimiento</label>
                                </div>
                                <div class="col-7">
                                    <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="TxFechaMant" TextMode="Date" class="form-control" runat="server"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <div class="col-6">
                            <asp:UpdatePanel ID="UpdatePanel50" runat="server">
                                <ContentTemplate>
                                        <div class="form-group row" runat="server" visible="false" id="DivEstadoEDC">
                                            <div class="col-3" style="margin-left:2%">
                                                <label class="col-form-label">Estado</label>
                                            </div>
                                            <div class="col-8">
                                                <asp:DropDownList runat="server" ID="DDLEstadoEDC" CssClass="form-control">
                                                    <asp:ListItem Value="1" Text="Activo"></asp:ListItem>
                                                    <asp:ListItem Value="0" Text="Inactivo"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        
                        <div class="col-12">
                            <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                <ContentTemplate>
                                    <div class="col-12" runat="server" id="DivMensajeEDC" visible="false" style="display: flex; background-color:tomato; justify-content:center">
                                        <asp:Label runat="server" CssClass="col-form-label text-white" ID="LbMensajeEDC"></asp:Label>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                        <ContentTemplate>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                            <asp:Button ID="BtnAceptarEDC" runat="server" Text="Aceptar" class="btn btn-success" OnClick="BtnAceptarEDC_Click"/>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <%--MODAL INFO EDC--%>
    <div class="modal fade" id="ModalInfoEDC" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="ModalLabelCondiciones">
                        <asp:UpdatePanel ID="UpdatePanel27" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="LbTituloEDC" runat="server" Text=""></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel28" runat="server">
                        <ContentTemplate>
                            <div class="row">   
                                <div class="col-6">  
                                    <label class="col-4">Nombre</label>
                                    <asp:Label ID="LbNombre" Text="" runat="server" CssClass="col-form-label"/>
                                </div>
                                <div class="col-6">  
                                    <label class="col-4">Tipo Equipo</label>
                                    <asp:Label ID="LbTipo" Text="" runat="server" CssClass="col-form-label"/>
                                </div>
                            </div>
                            <div class="row">   
                                <div class="col-6">  
                                    <label class="col-4">Contrato</label>
                                    <asp:Label ID="LbContrato" Text="" runat="server" CssClass="col-form-label"/>
                                </div>
                                <div class="col-6">  
                                    <label class="col-4">Serie</label>
                                    <asp:Label ID="LbSerie" Text="" runat="server" CssClass="col-form-label"/>
                                </div>
                            </div>
                            <div class="row">   
                                <div class="col-6">  
                                    <label class="col-4">IP</label>
                                    <asp:Label ID="LbIP" Text="" runat="server" CssClass="col-form-label"/>
                                </div>
                                <div class="col-6">  
                                    <label class="col-4">Región</label>
                                    <asp:Label ID="LbRegion" Text="" runat="server" CssClass="col-form-label"/>
                                </div>
                            </div>
                            <div class="row">   
                                <div class="col-6">  
                                    <label class="col-4">IOS Image</label>
                                    <asp:Label ID="LbIOSImage" Text="" runat="server" CssClass="col-form-label"/>
                                </div>
                                <div class="col-6">  
                                    <label class="col-4">IOS Version</label>
                                    <asp:Label ID="LbIOSVersion" Text="" runat="server" CssClass="col-form-label"/>
                                </div>
                            </div>
                            <div class="row">   
                                <div class="col-6">  
                                    <label class="col-4">Latitud</label>
                                    <asp:Label ID="LbLatitud" Text="" runat="server" CssClass="col-form-label"/>
                                </div>
                                <div class="col-6">  
                                    <label class="col-4">Longitud</label>
                                    <asp:Label ID="LbLongitud" Text="" runat="server" CssClass="col-form-label"/>
                                </div>
                            </div>
                            <div class="row">   
                                <div class="col-6">  
                                    <label class="col-4">Fecha Mant.</label>
                                    <asp:Label ID="LbFechaMant" Text="" runat="server" CssClass="col-form-label"/>
                                </div>
                                <div class="col-6">  
                                    <label class="col-4">Direccion</label>
                                    <asp:Label ID="LbDireccion" Text="" runat="server" CssClass="col-form-label"/>
                                </div>
                            </div>
                            <div class="row">   
                                <div class="col-6">  
                                    <label class="col-4">Estado</label>
                                    <asp:Label ID="LbEstadoEDC" Text="" runat="server" CssClass="col-form-label"/>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel29" runat="server">
                        <ContentTemplate>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <%--MODAL DE ENLACES--%>
    <div class="modal fade" id="ModalArticulosEnlaces" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="ModalLabelModificacionEnlaces">
                        <asp:UpdatePanel ID="UpdatePanel15" runat="server" >
                            <ContentTemplate>
                                <asp:Label ID="LbIdArticuloENL" runat="server" Text=""></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-6">
                            <div class="form-group row">
                                <div class="col-3" style="margin-left:2%">
                                    <label class="col-form-label">Nombre</label>
                                </div>
                                <div class="col-8">
                                    <asp:UpdatePanel ID="UpdatePanel30" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="TxNombreENL" class="form-control" runat="server"></asp:TextBox>                                            
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="form-group row">
                                <div class="col-3">
                                    <label class="col-form-label">Descripción</label>
                                </div>
                                <div class="col-8">
                                    <asp:UpdatePanel ID="UpdatePanel43" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="TxDescripcionENL" TextMode="MultiLine" class="form-control" runat="server"></asp:TextBox>                                            
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>

                        <div class="col-6">
                            <div class="form-group row">
                                <div class="col-3" style="margin-left:2%">
                                    <label class="col-form-label">Proveedor</label>
                                </div>
                                <div class="col-8">
                                    <asp:UpdatePanel ID="UpdatePanel31" runat="server" >
                                        <ContentTemplate>
                                            <asp:DropDownList ID="DDLProveedorENL" runat="server" class="select2 form-control custom-select" style="width: 100%"></asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="form-group row">
                                <div class="col-3">
                                    <label class="col-form-label">Tipo</label>
                                </div>
                                <div class="col-8">
                                    <asp:UpdatePanel ID="UpdatePanel44" runat="server" >
                                        <ContentTemplate>
                                            <asp:DropDownList ID="DDLTipoEnlace" AutoPostBack="true" runat="server" OnSelectedIndexChanged="DDLTipoEnlace_SelectedIndexChanged" class="form-control"></asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>

                        <div class="col-6">
                            <div class="form-group row">
                                <div class="col-3" style="margin-left:2%">
                                    <label class="col-form-label">Origen</label>
                                </div>
                                <div class="col-8">
                                    <asp:UpdatePanel ID="UpdatePanel32" runat="server" >
                                        <ContentTemplate>
                                            <asp:DropDownList ID="DDLOrigen" runat="server" class="select2 form-control custom-select" style="width: 100%"></asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="form-group row">
                                <div class="col-3">
                                    <label class="col-form-label">IP Origen</label>
                                </div>
                                <div class="col-8">
                                    <asp:UpdatePanel ID="UpdatePanel33" runat="server" >
                                        <ContentTemplate>
                                            <asp:TextBox ID="TxIPOrigen" class="form-control" runat="server"></asp:TextBox>                                            
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>

                        <div class="col-6">
                            <div class="form-group row">
                                <div class="col-3" style="margin-left:2%">
                                    <label class="col-form-label"> Destino</label>
                                </div>
                                <div class="col-8">
                                    <asp:UpdatePanel ID="UpdatePanel34" runat="server" >
                                        <ContentTemplate>
                                            <asp:DropDownList ID="DDLDestino" runat="server" class="select2 form-control custom-select" style="width: 100%"></asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="form-group row">
                                <div class="col-3">
                                    <label class="col-form-label">IP Destino</label>
                                </div>
                                <div class="col-8">
                                    <asp:UpdatePanel ID="UpdatePanel35" runat="server" >
                                        <ContentTemplate>
                                            <asp:TextBox ID="TxIPDestino" class="form-control" runat="server"></asp:TextBox>                                            
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>

                        <div class="col-6">
                            <div class="form-group row">
                                <div class="col-3" style="margin-left:2%">
                                    <label class="col-form-label">Contacto</label>
                                </div>
                                <div class="col-8">
                                    <asp:UpdatePanel ID="UpdatePanel36" runat="server" >
                                        <ContentTemplate>
                                            <asp:TextBox ID="TxContacto" class="form-control" runat="server"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="form-group row">
                                <div class="col-3">
                                    <label class="col-form-label">Telefono</label>
                                </div>
                                <div class="col-8">
                                    <asp:UpdatePanel ID="UpdatePanel37" runat="server" >
                                        <ContentTemplate>
                                            <asp:TextBox ID="TxTelefono" class="form-control" runat="server"></asp:TextBox>                                            
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>

                        <div class="col-6">
                            <div class="form-group row">
                                <div class="col-3" style="margin-left:2%">
                                    <label class="col-form-label">Servicios</label>
                                </div>
                                <div class="col-8">
                                    <asp:UpdatePanel ID="UpdatePanel38" runat="server" >
                                        <ContentTemplate>
                                            <asp:TextBox ID="TxServicios" class="form-control" runat="server"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <div class="col-6">
                            <asp:UpdatePanel ID="UpdatePanel40" runat="server">
                                <ContentTemplate>
                                        <div class="form-group row" runat="server" visible="false" id="DivEstadoENL">
                                            <div class="col-3">
                                                <label class="col-form-label">Estado</label>
                                            </div>
                                            <div class="col-8">
                                                <asp:DropDownList runat="server" ID="DDLEstadoEnlace" CssClass="form-control">
                                                    <asp:ListItem Value="1" Text="Activo"></asp:ListItem>
                                                    <asp:ListItem Value="0" Text="Inactivo"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        

                        <div class="col-12">
                            <asp:UpdatePanel ID="UpdatePanel41" runat="server">
                                <ContentTemplate>
                                    <div class="col-12" runat="server" id="DivMensajeENL" visible="false" style="display: flex; background-color:tomato; justify-content:center">
                                        <asp:Label runat="server" CssClass="col-form-label text-white" ID="LbMensajeENL"></asp:Label>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel42" runat="server">
                        <ContentTemplate>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                            <asp:Button ID="BtnAceptarEnlace" runat="server" Text="Aceptar" class="btn btn-success" OnClick="BtnAceptarEnlace_Click"/>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <%--MODAL INFO ENLACES--%>
    <div class="modal fade" id="ModalInfoENL" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="ModalLabelInfoENL">
                        <asp:UpdatePanel ID="UpdatePanel39" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="LbTituloENL" runat="server" Text=""></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel45" runat="server">
                        <ContentTemplate>
                            <div class="row">   
                                <div class="col-6">  
                                    <label class="col-4">Nombre</label>
                                    <asp:Label ID="LbNombreENL" Text="" runat="server" CssClass="col-form-label"/>
                                </div>
                                <div class="col-6">  
                                    <label class="col-4">Descripción</label>
                                    <asp:Label ID="LbDescripcionENL" Text="" runat="server" CssClass="col-form-label"/>
                                </div>
                            </div>
                            <div class="row">   
                                <div class="col-6">  
                                    <label class="col-4">Proveedor</label>
                                    <asp:Label ID="LbProveedorENL" Text="" runat="server" CssClass="col-form-label"/>
                                </div>
                                <div class="col-6">  
                                    <label class="col-4">Tipo</label>
                                    <asp:Label ID="LbTipoENL" Text="" runat="server" CssClass="col-form-label"/>
                                </div>
                            </div>
                            <div class="row">   
                                <div class="col-6">  
                                    <label class="col-4">Origen</label>
                                    <asp:Label ID="LbOrigenENL" Text="" runat="server" CssClass="col-form-label"/>
                                </div>
                                <div class="col-6">  
                                    <label class="col-4">Destino</label>
                                    <asp:Label ID="LbDestinoENL" Text="" runat="server" CssClass="col-form-label"/>
                                </div>
                            </div>
                            <div class="row">   
                                <div class="col-6">  
                                    <label class="col-4">IP Origen</label>
                                    <asp:Label ID="LbIPOrigenENL" Text="" runat="server" CssClass="col-form-label"/>
                                </div>
                                <div class="col-6">  
                                    <label class="col-4">IP Destino</label>
                                    <asp:Label ID="LbIPDestinoENL" Text="" runat="server" CssClass="col-form-label"/>
                                </div>
                            </div>
                            <div class="row">   
                                <div class="col-6">  
                                    <label class="col-4">Servicios</label>
                                    <asp:Label ID="LbServiciosENL" Text="" runat="server" CssClass="col-form-label"/>
                                </div>
                                <div class="col-6">  
                                    <label class="col-4">Contacto</label>
                                    <asp:Label ID="LbContactoENL" Text="" runat="server" CssClass="col-form-label"/>
                                </div>
                            </div>
                            <div class="row">   
                                <div class="col-6">  
                                    <label class="col-4">Teléfono</label>
                                    <asp:Label ID="LbTelefonoENL" Text="" runat="server" CssClass="col-form-label"/>
                                </div>
                                <div class="col-6">  
                                    <label class="col-4">Archivo</label>
                                    <asp:LinkButton ID="LBAdjuntoENL" Text="" CssClass="col-form-label" runat="server" OnClick="LBAdjuntoENL_Click" />
                                </div>
                            </div>
                            <div class="row">   
                                <div class="col-6">  
                                    <label class="col-4">Creación</label>
                                    <asp:Label ID="LbFechaENL" Text="" runat="server" CssClass="col-form-label"/>
                                </div>
                                <div class="col-6">  
                                    <label class="col-4">Usuario</label>
                                    <asp:Label ID="LbUsuarioENL" Text="" runat="server" CssClass="col-form-label"/>
                                </div>
                            </div>
                            <div class="row">   
                                <div class="col-6">  
                                    <label class="col-4">Estado</label>
                                    <asp:Label ID="LbEstadoENL" Text="" runat="server" CssClass="col-form-label"/>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel46" runat="server">
                        <ContentTemplate>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <%--MODAL DE CARGA ARCHIVO--%>
    <div class="modal fade" id="ModalCarga" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <asp:UpdatePanel ID="UpdatePanel47" runat="server">
                        <ContentTemplate>
                            <h4 class="modal-title" id="ModalLabelCargar">
                                <b><asp:Label runat="server" ID="LbTituloCarga" CssClass="col-form-label" Text="Cargar Archivo"></asp:Label></b>
                            </h4>
                            <asp:Label runat="server" ID="Label16" CssClass="col-form-label"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label class="control-label col-12">Carga</label>
                        <div class="col-9">
                            <asp:FileUpload ID="FUCarga" CssClass="form-control" runat="server" AllowMultiple="false"/>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12">
                            <asp:UpdatePanel ID="UpdatePanel48" runat="server">
                                <ContentTemplate>
                                    <div class="col-12" runat="server" id="DivMensajeCarga" visible="false" style="display: flex; background-color: tomato; justify-content: center">
                                        <asp:Label runat="server" CssClass="col-form-label text-white" ID="LbAdvertenciaCarga"></asp:Label>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel49" runat="server">
                        <ContentTemplate>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                            <asp:Button ID="BtnCargar" runat="server" Text="Aceptar" class="btn btn-info" OnClick="BtnCargar_Click"/>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="BtnCargar" />
                        </Triggers>
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
    <%--COMBO BUSCADOR--%>
    <script src="../assets/node_module/select2/dist/js/select2.full.min.js" type="text/javascript"></script>
    <style>
        .select2-selection__rendered {line-height: 31px !important;}
        .select2-container .select2-selection--single {height: 35px !important;}
        .select2-selection__arrow {height: 34px !important;}
    </style>
    <script>
        $(function () {
            $(".select2").select2();
            $(".ajax").select2({
                ajax: {
                    url: "https://api.github.com/search/repositories",
                    dataType: 'json',
                    delay: 250,
                    data: function (params) {
                        return {
                            q: params.term, // search term
                            page: params.page
                        };
                    },
                    processResults: function (data, params) {
                        params.page = params.page || 1;
                        return {
                            results: data.items,
                            pagination: {
                                more: (params.page * 30) < data.total_count
                            }
                        };
                    },
                    cache: true
                },
                escapeMarkup: function (markup) {
                    return markup;
                },
                minimumInputLength: 1,
            });
        });
    </script>
</asp:Content>
