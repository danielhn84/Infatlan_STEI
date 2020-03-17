<%@ Page Title="" Language="C#" MasterPageFile="~/mainATM.Master" AutoEventWireup="true" CodeBehind="buscarVerificacionATM.aspx.cs" Inherits="Infatlan_STEI_ATM.buscarVerificacionATM" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <link href="/css/GridStyle.css" rel="stylesheet" />
    <link href="/css/pager.css" rel="stylesheet" />
    <link href="/css/breadcrumb.css" rel="stylesheet" />
    <link href="/css/fstdropdown.css" rel="stylesheet" />
    <link href="/css/alert.css" rel="stylesheet" />
    <link href="/css/GridStyle.css" rel="stylesheet" />
    <script type="text/javascript">
        var updateProgress = null;

        function postbackButtonClick() {
            updateProgress = $find("<%= UpdateProgress1.ClientID %>");
            window.setTimeout("updateProgress.set_visible(true)", updateProgress.get_displayAfter());
            return true;
        }
    </script>
    <script type="text/javascript">
        function openModal() {
            $('#modalverificacion').modal('show');
        }

        function closeModal() {
            $('#modalverificacion').modal('hide');
        }

        var url = document.location.toString();
        if (url.match('#')) {
            $('.nav-tabs a[href="#' + url.split('#')[1] + '"]').tab('show');
        }

        $('.nav-tabs a').on('shown.bs.tab', function (e) {
            window.location.hash = e.target.hash;
        })
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
            <h4 class="text-themecolor">Lista de verificación</h4>
        </div>
        <div class="col-md-7 align-self-center text-right">
            <div class="d-flex justify-content-end align-items-center">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Inicio</a></li>
                    <li class="breadcrumb-item active">Lista de verificación</li>
                </ol>
               
            </div> 
        </div>
    </div>
    <!--/ENCABEZADO-->

    <div class="card" >
        <br />
        <div class="row col-12" style="margin-left:10px; margin-left:10px;">
            <h3 class="text-themecolor" style="color: #808080;"><i class="fa fa-undo"></i> Crear lista de verificación</h3>
        </div>   

        <!--DATAGRID-->
        <asp:UpdatePanel ID="UpdateDivBusquedas" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="row">
                    <div class="col-12 grid-margin stretch-card">
                        <div class="card" id="21">
                            <div class="card-body">
                                <h4 class="card-title">Lista de verificación</h4>

                                <div class="col-md-6">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">Buscar</label>
                                        <div class="col-sm-9">
                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="TxBuscarTecnicoATM" OnTextChanged="TxBuscarTecnicoATM_TextChanged"  runat="server" placeholder="ingrese nombre de ATM - Presione afuera para proceder" class="form-control" AutoPostBack="true"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-12 grid-margin stretch-card">
                        <div class="card" id="212">
                            <div class="card-body">
                                <div class="row">
                                    <div class="table-responsive">
                                        <asp:UpdatePanel ID="UpdateGridView" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:GridView ID="GVBusqueda" runat="server"
                                                    CssClass="table table-bordered"
                                                    PagerStyle-CssClass="pgr"
                                                    HeaderStyle-CssClass="table" HeaderStyle-HorizontalAlign="center"
                                                    RowStyle-CssClass="rows"
                                                    AutoGenerateColumns="false"
                                                    AllowPaging="true"
                                                    GridLines="None" OnPageIndexChanging="GVBusqueda_PageIndexChanging"
                                                    PageSize="10" OnRowCommand="GVBusqueda_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="center">
                                                            <ItemTemplate>                                                               
                                                                <asp:LinkButton runat="server" ID="btnVerifATM" Text="Crear lista" CssClass="btn btn-info mr-2" CommandArgument='<%# Eval("ID") %>' CommandName="Aprobar"></asp:LinkButton>
                                                                 <asp:LinkButton runat="server" ID="btnReprogramar" Text="Reprogramar" CssClass="btn btn-danger mr-2" CommandArgument='<%# Eval("ID") %>' CommandName="Reprogramar"></asp:LinkButton>
                                                                <%-- <asp:Button ID="BtnUsuarioModificar" runat="server" Text="Modificar" CssClass="btn btn-rounded btn-block btn-success" CommandArgument='<%# Eval("codATM") %>' CommandName="Modificar" />--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="ID" HeaderText="Código" Visible="false"  ItemStyle-HorizontalAlign="center"/>
                                                        <asp:BoundField DataField="Codigo" HeaderText="Código de ATM"  ItemStyle-HorizontalAlign="center"/>
                                                        <asp:BoundField DataField="NomATM" HeaderText="Nombre"  ItemStyle-HorizontalAlign="center"/>
                                                        <asp:BoundField DataField="Tecnico" HeaderText="Técnico Responsable"  ItemStyle-HorizontalAlign="center"/>
                                                        <asp:BoundField DataField="Sucursal" HeaderText="Sucursal"  ItemStyle-HorizontalAlign="center"/>
                                                        <asp:BoundField DataField="Ubicacion" HeaderText="Ubicación"  ItemStyle-HorizontalAlign="center"/>
                                                            
                                                    </Columns>
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <!--/DATAGRID-->
         <!-- Modal al no realizar trabajo -->
    <div class="modal bs-example-modal-lg" id="modalverificacion" tabindex="-1" data-backdrop="static" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myLargeModalLabel">Motivo por el que se canceló el mantenimiento</h4>
                    <button type="button" id="btnexitModal" class="close" data-dismiss="modal" aria-hidden="true">X</button>
                </div>

                <asp:UpdatePanel runat="server" ID="UPModal">
                    <ContentTemplate>
                        <div class="modal-body">
                            <div>
                                <asp:Label runat="server">Mantenimiento a reprogramar</asp:Label>
                            </div>
                            <div>
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtModalATM" Enabled="false"></asp:TextBox>
                            </div>
                            <br />
                            <div>
                                <asp:Label runat="server">Motivo de Cancelación de Mantenimiento</asp:Label>
                            </div>
                            <div>
                                <asp:DropDownList ID="DDLModalMotivo" runat="server" CssClass="form-control col-12">
                                    <asp:ListItem Value="0" Text="Seleccione motivo de cancelación...."></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <br />
                            <div>Cambio realizado por</div>
                            <div>
                                <asp:DropDownList ID="DDLModalcambioPor" runat="server" CssClass="form-control col-12">
                                    <asp:ListItem Value="0" Text="Seleccione personal responsable...."></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Técnico Responsable"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Jefe de soporte de zona"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Suplente jefe de zona"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <br />
                            <div>
                                <label>Nuevo Técnico Responsable</label>
                            </div>
                            
                            <div>
                                <asp:DropDownList ID="DDLModalNewTecnico" runat="server" CssClass="form-control col-12">
                                    <asp:ListItem Value="0" Text="Seleccione nuevo personal responsable...."></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <br />
                            <div>
                                <asp:Label runat="server">Detalle de motivo de cancelación</asp:Label>
                            </div>
                            <div>
                                <asp:TextBox ID="txtdetalleCancela" runat="server" TextMode="Multiline" Rows="5" CssClass="form-control col-12"></asp:TextBox>
                            </div>
                            <div class="col-md-6 align-self-center" style="margin-left:auto; margin-right:auto">
                                <asp:Label runat="server" BorderStyle="None" style="color:red;" Visible="false" ID="lbValidarModal" CssClass="col form-control"></asp:Label>
                            </div>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>

                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <div class="modal-footer">
                            <asp:Button runat="server" ID="btnMantSinRealizar" OnClick="btnMantSinRealizar_Click" CssClass="btn btn-success col-12" Text="Enviar" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
            <!-- /.modal-content -->
        </div>
        <!--/.modal-dialog -->
    </div>
    <!-- /Modal al no realizar trabajo -->
    </div>
    <br />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
<%--<script src="/js/fstdropdown.js"></script>
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
    </script>--%>
</asp:Content>
