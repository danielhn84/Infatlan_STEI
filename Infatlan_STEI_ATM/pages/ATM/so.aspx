<%@ Page Title="" Language="C#" MasterPageFile="~/mainATM.Master" AutoEventWireup="true" CodeBehind="so.aspx.cs" Inherits="Infatlan_STEI_ATM.pages.ATM.so" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <!--PARA LLAMAR MODAL-->
    <script type="text/javascript">
        function openModal() { $('#modalsoATM').modal('show'); }
        function openModal2() { $('#modalso2ATM').modal('show'); }
    </script>
    <!--PARA CERRAR MODAL-->
    <script type="text/javascript">
        function closeModal() { $('#modalsoATM').modal('hide'); }
        function closeModal2() { $('#modalso2ATM').modal('hide'); }
    </script>

    <link href="/css/GridStyle.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">

    <div class="row page-titles">
        <div class="col-md-5 align-self-center">
            <h4 class="text-themecolor">Ingresar nuevo sistema operativo de ATM</h4>
        </div>
        <div class="col-md-7 align-self-center text-right">
            <div class="d-flex justify-content-end align-items-center">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Inicio</a></li>
                    <li class="breadcrumb-item active">Ingresar nuevo sistema operativo ATM</li>
                </ol>

            </div>
        </div>
    </div>
    <!--/ENCABEZADO-->

    <div class="card">
        <br />
        <div class="row col-12" style="margin-left: 10px; margin-left: 10px;">
            <h3 class="text-themecolor" style="color: #808080;"><i class="fa fa-save"></i>Ingresar nuevo sistema operativo ATM</h3>
        </div>
        <br />
        <hr />
        <br />

        <!--DATAGRID-->
        <asp:UpdatePanel ID="UpdateDivBusquedas" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="row col-12">
                    <label class="col-3 col-form-label">Sistemas operativos creados</label>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                        <ContentTemplate>
                            <asp:LinkButton runat="server" ID="btnnewsoATM" OnClick="btnnewsoATM_Click" CssClass="btn btn-info icon-plus mr-2" Text=""></asp:LinkButton>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <br />
                <hr />
                <br />
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
                                                    GridLines="None"
                                                    PageSize="10" OnRowCommand="GVBusqueda_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnso" runat="server" CssClass="btn btn-info ti-pencil-alt mr-2" Text="" CommandArgument='<%# Eval("idSO") %>' CommandName="Codigo"></asp:LinkButton>
                                                                <%--<asp:Button ID="BtnUsuarioPassword" runat="server" Text="De baja" CssClass="btn btn-block btn-outline-danger" CommandArgument='<%# Eval("codATM") %>' CommandName="Baja" />--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="idSO" HeaderText="Código de Sistema Operativo" ItemStyle-HorizontalAlign="center" />
                                                        <asp:BoundField DataField="nombreSO" HeaderText="Sistema Operativo" ItemStyle-HorizontalAlign="center" />


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
        <br />
        <hr />
        <br />
        <!--MODAL GUARDAR tipoATM -->
        <div class="modal bs-example-modal-lg" id="modalsoATM" tabindex="-1" data-backdrop="static" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" style="display: none;">
            <div class="modal-dialog modal-xl">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title" id="myLargeModalLabel">¿Seguro que modificará sistema operativo?</h4>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="row col-12">
                                <asp:Label runat="server" BorderStyle="None" class="col form-control col-6"><strong>Código de Sistema Operativo: </strong></asp:Label>
                                <asp:Label runat="server" BorderStyle="None" ID="lbcodsoATM" class="col form-control col-6"></asp:Label>
                            </div>
                            <div class="row col-12">
                                <asp:Label runat="server" BorderStyle="None" class="col form-control col-6"><strong>Sistema Operativo: </strong></asp:Label>
                                <asp:Label runat="server" BorderStyle="None" ID="lbNombresoATM" class="col form-control col-6"></asp:Label>
                            </div>
                            <div class="row col-12">
                                <asp:Label runat="server" BorderStyle="None" class="col form-control col-6"><strong>Nuevo Sistema Operativo: </strong></asp:Label>
                                <asp:TextBox runat="server" ID="txtModalNewSOATM" CssClass="form-control col-6"></asp:TextBox>
                            </div>
                           <div class="col-md-10 align-self-center" style="margin-left: auto; margin-right: auto">
                                <br />
                                <h5 runat="server" visible="false" id="H5Alerta1" class="text-danger"><i class="fa fa-exclamation-triangle"></i>Ingrese nuevo sistema operativo.</h5>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <div class="modal-footer col-12">
                                <div class="row col-3">
                                    <asp:Button runat="server" ID="btnModalEnviarSOATM" OnClick="btnModalEnviarSOATM_Click" CssClass="btn btn-success mr-2" Text="Modificar" />
                                </div>
                                <div class="row col-3">
                                    <asp:Button runat="server" ID="btnModalCerrarSOATM" OnClick="btnModalCerrarSOATM_Click" CssClass="btn btn-danger mr-2" Text="Cancelar" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
                <!-- /.modal-content -->
            </div>
            <!--/.modal-dialog -->
        </div>
        <!-- /MODAL GUARDAR tipoATM -->

        <!--MODAL NUEVA MODELOATM -->
        <div class="modal bs-example-modal-lg" id="modalso2ATM" tabindex="-1" data-backdrop="static" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" style="display: none;">
            <div class="modal-dialog modal-xl">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title" id="myLargeModalLabel1">Nuevo teclado de ATM</h4>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <div class="row col-12">
                                <asp:Label runat="server" BorderStyle="None" class="col form-control col-6"><strong>Sistema Operativo: </strong></asp:Label>
                                <asp:TextBox runat="server" ID="txtNewSOATM" CssClass="form-control col-6"></asp:TextBox>
                            </div>
                           <div class="col-md-10 align-self-center" style="margin-left: auto; margin-right: auto">
                                <br />
                                <h5 runat="server" visible="false" id="H5Alerta2" class="text-danger"><i class="fa fa-exclamation-triangle"></i>Ingrese nuevo sistema operativo.</h5>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <div class="modal-footer col-12">
                                <div class="row col-3">
                                    <asp:Button runat="server" ID="btnModalNueviSOATM" OnClick="btnModalNueviSOATM_Click" CssClass="btn btn-success mr-2" Text="Agregar" />
                                </div>
                                <div class="row col-3">
                                    <asp:Button runat="server" ID="btnModalCerrarNueviSOATM" OnClick="btnModalCerrarNueviSOATM_Click" CssClass="btn btn-danger mr-2" Text="Cancelar" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
                <!-- /.modal-content -->
            </div>
            <!--/.modal-dialog -->
        </div>
        <!-- /MODAL NUEVA MODELOATM -->


    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
