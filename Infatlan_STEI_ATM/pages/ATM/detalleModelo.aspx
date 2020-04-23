﻿<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="detalleModelo.aspx.cs" Inherits="Infatlan_STEI_ATM.pages.ATM.detalleModelo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <!--PARA LLAMAR MODAL-->
    <script type="text/javascript">
        function openModal() { $('#modaldetalleMATM').modal('show'); }
        function openModal2() { $('#modaldetalleM2ATM').modal('show'); }
    </script>
    <!--PARA CERRAR MODAL-->
    <script type="text/javascript">
        function closeModal() { $('#modaldetalleMATM').modal('hide'); }
        function closeModal2() { $('#modaldetalleM2ATM').modal('hide'); }
    </script>

    <link href="/css/GridStyle.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">

    <div class="row page-titles">
        <div class="col-md-5 align-self-center">
            <h4 class="text-themecolor">Ingresar nuevo detalle de modelo ATM</h4>
        </div>
        <div class="col-md-7 align-self-center text-right">
            <div class="d-flex justify-content-end align-items-center">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Inicio</a></li>
                    <li class="breadcrumb-item active">Ingresar nuevo detalle de modelo ATM</li>
                </ol>

            </div>
        </div>
    </div>
    <!--/ENCABEZADO-->

    <div class="card">
        <br />
        <div class="row col-12" style="margin-left: 10px; margin-left: 10px;">
            <h3 class="text-themecolor" style="color: #808080;"><i class="fa fa-save"></i>Ingresar nuevo detalle de modelo</h3>
        </div>
        <br />
        <hr />
        <br />

        <!--DATAGRID-->
        <asp:UpdatePanel ID="UpdateDivBusquedas" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="row col-12">
                    <label class="col-3 col-form-label">Detalles de modelo creadas</label>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                        <ContentTemplate>
                            <asp:LinkButton runat="server" ID="btnnewdetModeloATM" OnClick="btnnewdetModeloATM_Click" CssClass="btn btn-info icon-plus mr-2"></asp:LinkButton>
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
                                                    GridLines="None" OnPageIndexChanging="GVBusqueda_PageIndexChanging"
                                                    PageSize="10" OnRowCommand="GVBusqueda_RowCommand">
                                                    <Columns>
                                                       
                                                        <asp:BoundField DataField="ID" HeaderText="Código detalle de modelo" ItemStyle-HorizontalAlign="center" />
                                                        <asp:BoundField DataField="NOMBRE" HeaderText="Detalle de Modelo" ItemStyle-HorizontalAlign="center" />
                                                        <asp:BoundField DataField="MODELO" HeaderText="Modelo ATM" ItemStyle-HorizontalAlign="center" />
                                                        <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btndetModelo" runat="server" CssClass="btn btn-info ti-pencil-alt mr-2" Text="" CommandArgument='<%# Eval("ID") %>' CommandName="Codigo"></asp:LinkButton>
                                                                <%--<asp:Button ID="BtnUsuarioPassword" runat="server" Text="De baja" CssClass="btn btn-block btn-outline-danger" CommandArgument='<%# Eval("codATM") %>' CommandName="Baja" />--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

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
        <div class="modal bs-example-modal-lg" id="modaldetalleMATM" tabindex="-1" data-backdrop="static" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" style="display: none;">
            <div class="modal-dialog modal-xl">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title" id="myLargeModalLabel">¿Seguro que desea modificarlo?</h4>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="row col-12">
                                <asp:Label runat="server" BorderStyle="None" class="col form-control col-6"><strong>Código detalle de modelo: </strong></asp:Label>
                                <asp:Label runat="server" BorderStyle="None" ID="lbcoddetMATM" class="col form-control col-6"></asp:Label>
                            </div>
                            <div class="row col-12">
                                <asp:Label runat="server" BorderStyle="None" class="col form-control col-6"><strong>Detalle de modelo: </strong></asp:Label>
                                <asp:Label runat="server" BorderStyle="None" ID="lbNombredetMATM" class="col form-control col-6"></asp:Label>
                            </div>
                            <div class="row col-12">
                                <asp:Label runat="server" BorderStyle="None" class="col form-control col-6"><strong>Modelo: </strong></asp:Label>
                                <asp:DropDownList runat="server" ID="DDLModeloATM" CssClass="form-control col-6"></asp:DropDownList>
                            </div>
                            <div class="row col-12">
                                <asp:Label runat="server" BorderStyle="None" class="col form-control col-6"><strong>Nuevo detalle de modelo: </strong></asp:Label>
                                <asp:TextBox runat="server" ID="txtModalNewdetMATM" CssClass="form-control col-6"></asp:TextBox>
                            </div>
                            <div class="col-md-6 align-self-center" style="margin-left: auto; margin-right: auto">
                                <br />
                                <h5 runat="server" visible="false" id="H5Alerta1" class="text-danger"><i class="fa fa-exclamation-triangle"></i>Ingrese nuevo detalle.</h5>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <div class="modal-footer col-12">
                                <div class="row col-3">
                                    <asp:Button runat="server" ID="btnModalEnviardetMATM" OnClick="btnModalEnviardetMATM_Click" CssClass="btn btn-success mr-2" Text="Modificar" />
                                </div>
                                <div class="row col-3">
                                    <asp:Button runat="server" ID="btnModalCerrardetMATM" OnClick="btnModalCerrardetMATM_Click" CssClass="btn btn-danger mr-2" Text="Cancelar" />
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
        <div class="modal bs-example-modal-lg" id="modaldetalleM2ATM" tabindex="-1" data-backdrop="static" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" style="display: none;">
            <div class="modal-dialog modal-xl">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title" id="myLargeModalLabel1">Nueva marca</h4>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <div class="row col-12">
                                <asp:Label runat="server" BorderStyle="None" class="col form-control col-6"><strong>Modelo: </strong></asp:Label>
                                <asp:DropDownList runat="server" ID="DDLNewModelo" CssClass="form-control col-6"></asp:DropDownList>
                            </div>
                            <div class="row col-12">
                                <asp:Label runat="server" BorderStyle="None" class="col form-control col-6"><strong>Nuevo detalle de modelo: </strong></asp:Label>
                                <asp:TextBox runat="server" ID="txtNewdetMATM" CssClass="form-control col-6"></asp:TextBox>
                            </div>
                           <div class="col-md-6 align-self-center" style="margin-left: auto; margin-right: auto">
                                <br />
                                <h5 runat="server" visible="false" id="H5Alerta2" class="text-danger"><i class="fa fa-exclamation-triangle"></i>Ingrese nuevo detalle.</h5>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <div class="modal-footer col-12">
                                <div class="row col-3">
                                    <asp:Button runat="server" ID="btnModalNuevidetMATM" OnClick="btnModalNuevidetMATM_Click" CssClass="btn btn-success mr-2" Text="Agregar" />
                                </div>
                                <div class="row col-3">
                                    <asp:Button runat="server" ID="btnModalCerrarNuevidetMATM" OnClick="btnModalCerrarNuevidetMATM_Click" CssClass="btn btn-danger mr-2" Text="Cancelar" />
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
