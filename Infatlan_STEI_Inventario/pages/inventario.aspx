<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="inventario.aspx.cs" Inherits="Infatlan_STEI_Inventario.pages.salidas" %>
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
        function abrirModal() { $('#ModalConfirmar').modal('show'); }
        function cerrarModal() { $('#ModalConfirmar').modal('hide'); }

        function openModalP() { $('#ModalProveedores').modal('show'); }
        function cerrarModalP() { $('#ModalProveedores').modal('hide'); }
        function openModalA() { $('#ModalArticulos').modal('show'); }
        function cerrarModalA() { $('#ModalArticulos').modal('hide'); }
        function openModal() { $('#ModalUbicaciones').modal('show'); }
        function closeModal() { $('#ModalUbicaciones').modal('hide'); }
        function openModalU() { $('#ModalUbicaciones').modal('show'); }
        function closeModalU() { $('#ModalUbicaciones').modal('hide'); }
    </script>
    <link href="/css/fstdropdown.css" rel="stylesheet" />
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
                    <h4 class="card-title">Inventario</h4>
                    <h6 class="card-subtitle">Mercadería que saldrá del almacén.</h6>
                    <br />
                    <div class="form-body col-12">
                        <div class="row">
                            <div class="col-6">
                                <div class="form-group row">
                                    <label class="col-form-label col-12">Código</label>
                                    <div class="col-10">
                                        <asp:TextBox runat="server" ID="TxCodigo" CssClass="form-control"></asp:TextBox>                                        
                                    </div>
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group row">
                                    <label class="col-form-label col-12">Artículo del almacén</label>
                                    <div class="col-10">
                                        <asp:DropDownList runat="server" ID="DDLArticulo" CssClass="fstdropdown-select form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-6">
                                <div class="form-group row">
                                    <label class="col-form-label col-12">Cantidad</label>
                                    <div class="col-10">
                                        <asp:TextBox runat="server" ID="TxCantidad" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-6" >
                                <div class="form-group row">
                                    <label class="col-form-label col-12">Precio</label>
                                    <div class="col-10">
                                        <asp:TextBox runat="server" ID="TxPrecio" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-6">
                                <div class="form-group row">
                                    <label class="col-form-label col-12">Tipo de Transaccion</label>
                                    <div class="col-10">
                                        <asp:DropDownList runat="server" ID="DDLTipoTransaccion" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group row">
                                    <label class="col-form-label col-12">Ubicación de Destino</label>
                                    <div class="col-10">
                                        <asp:DropDownList runat="server" ID="DDLUbicacion" CssClass="fstdropdown-select form-control"></asp:DropDownList>
                                    </div>
                                    <div class="col-1">
                                        <asp:Button ID="BtnAddUbicacion" Text="+" CssClass="btn btn-primary" runat="server" OnClick="BtnAddUbicacion_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-6">
                                <div class="form-group row">
                                    <label class="col-form-label col-12">Descripción</label>
                                    <div class="col-10">
                                        <asp:TextBox runat="server" ID="TxDescripcion" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="form-actions">
                    <div class="card-body">
                        <asp:Button Text="Guardar" class="btn btn-success" OnClick="BtnGuardarInventario_Click" ID="BtnGuardarInventario" runat="server" />
                        <%--<button type="submit" class="btn btn-success" onclick="abrirModal()" style="margin-right:7px"> <i class="fa fa-check" style="margin-right:5px"></i>Guardar</button>--%>
                        <button type="button" class="btn btn-dark">Cancelar</button>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Registros</h4>
                    <h6 class="card-subtitle">Todas las entradas de material y equipo.</h6>
                    <br />
                    <div class="table-responsive m-t-40">
                        <asp:GridView ID="GVBusqueda" runat="server"
                            CssClass="table table-bordered"
                            PagerStyle-CssClass="pgr"
                            HeaderStyle-CssClass="table"
                            RowStyle-CssClass="rows"
                            AutoGenerateColumns="false"
                            AllowPaging="true"
                            GridLines="None" 
                            PageSize="10" >
                            <Columns>
                                <asp:BoundField DataField="idStock" HeaderText="No." />
                                <asp:BoundField DataField="TipoStock" HeaderText="Equipo" />
                                <asp:BoundField DataField="Marca" HeaderText="Marca" />
                                <asp:BoundField DataField="modelo" HeaderText="Modelo" />
                                <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
                                <asp:BoundField DataField="Proveedor" HeaderText="Proveedor"/>
                                <asp:BoundField DataField="descripcion" HeaderText="Detalle" />
                                <asp:BoundField DataField="series" HeaderText="Serie" />
                                <asp:TemplateField HeaderText="Seleccione" HeaderStyle-Width="13%">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="BtnEditar" runat="server" class="btn btn-info mr-2" CommandArgument='<%# Eval("idStock") %>' CommandName="EditarArticulo">
                                            <i class="icon-pencil" ></i>
                                        </asp:LinkButton>
                            
                                        <asp:LinkButton ID="BtnEditar2" runat="server" class="btn btn-primary mr-2" CommandArgument='<%# Eval("idStock") %>' CommandName="EliminarArticulo">
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

    <%--MODAL DE UBICACIONES--%>
    <div class="modal fade" id="ModalUbicaciones" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">
                        <asp:Label ID="LbIdUbicacion" runat="server" Text="Crear Nueva Ubicación"></asp:Label>
                    </h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group row">
                                        <div class="col-2" style="margin-left:5%">
                                            <label class="col-form-label">Tipo</label>
                                        </div>
                                        <div class="col-sm-9">
                                            <asp:DropDownList ID="DDLTipoUbic" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="form-group row">
                                        <div class="col-3" style="margin-left:5%">
                                            <label class="col-form-label">Departamento</label>
                                        </div>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="DDLDepartamento" AutoPostBack="true" runat="server" class="form-control" OnSelectedIndexChanged="DDLDepartamento_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="form-group row">
                                        <div class="col-2" style="margin-left:5%">
                                            <label class="col-form-label">Municipio</label>
                                        </div>
                                        <div class="col-sm-9">
                                            <asp:DropDownList ID="DDLMunicipio" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="form-group row">
                                        <div class="col-2" style="margin-left:5%">
                                            <label class="col-form-label">Código</label>
                                        </div>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="TxCodigoUbic" class="form-control text-uppercase" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="form-group row">
                                        <div class="col-2" style="margin-left:5%">
                                            <label class="col-form-label">Dirección</label>
                                        </div>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="TxDireccionUbic" TextMode="MultiLine" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12" runat="server" id="DivMensajeUbic" visible="false" style="display: flex; background-color:tomato; justify-content:center">
                                    <asp:Label runat="server" CssClass="col-form-label text-white" ID="LbMensajeUbic"></asp:Label>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                        <ContentTemplate>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                            <asp:Button ID="BtnAceptarUbic" runat="server" Text="Aceptar" class="btn btn-success" OnClick="BtnAceptarUbic_Click"/>
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
                    <h4 class="modal-title" id="ModalLabelConfirmar">
                        <b><asp:Label Text="Guardar Inventario" runat="server" ID="LbTitulo" CssClass="col-form-label"></asp:Label></b>
                    </h4>
                </div>
                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                            <asp:Button ID="BtnConfirmar" runat="server" Text="Aceptar" class="btn btn-danger" OnClick="BtnConfirmar_Click"/>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <script src="/js/fstdropdown.js"></script>
    <script>
        function setDrop() {
            if (!document.getElementById('third').classList.contains("fstdropdown-select"))
                document.getElementById('third').className = 'fstdropdown-select';
            setFstDropdown();
        }
        setFstDropdown();
        function removeDrop() {
            if (document.getElementById('third').classList.contains("fstdropdown-select")) {
                document.getElementById('third').classList.remove('fstdropdown-select');
                document.getElementById("third").fstdropdown.dd.remove();
            }
        }
        function addOptions(add) {
            var select = document.getElementById("fourth");
            for (var i = 0; i < add; i++) {
                var opt = document.createElement("option");
                var o = Array.from(document.getElementById("fourth").querySelectorAll("option")).slice(-1)[0];
                var last = o == undefined ? 1 : Number(o.value) + 1;
                opt.text = opt.value = last;
                select.add(opt);
            }
        }
        function removeOptions(remove) {
            for (var i = 0; i < remove; i++) {
                var last = Array.from(document.getElementById("fourth").querySelectorAll("option")).slice(-1)[0];
                if (last == undefined)
                    break;
                Array.from(document.getElementById("fourth").querySelectorAll("option")).slice(-1)[0].remove();
            }
        }
        function updateDrop() {
            document.getElementById("fourth").fstdropdown.rebind();
        }
    </script>
</asp:Content>
