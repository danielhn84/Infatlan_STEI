﻿<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="versionSw.aspx.cs" Inherits="Infatlan_STEI_ATM.pages.ATM.versionSw" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <!--PARA LLAMAR MODAL-->
    <script type="text/javascript">
        function openModal() { $('#modalversionATM').modal('show'); }
        function openModal2() { $('#modalversion2ATM').modal('show'); }
    </script>
    <!--PARA CERRAR MODAL-->
    <script type="text/javascript">
        function closeModal() { $('#modalversionATM').modal('hide'); }
        function closeModal2() { $('#modalversion2ATM').modal('hide'); }
    </script>

    <link href="/css/GridStyle.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #ffffff; opacity: 0.7; margin: 0;">
                <span style="display: inline-block; height: 100%; vertical-align: middle;"></span>
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="../../assets/images/loading.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="display: inline-block; vertical-align: middle;" />
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
                    <li class="breadcrumb-item"><a href="javascript:void(0)">ATM</a></li>
                    <li class="breadcrumb-item"><a href="javascript:void(0)">ATM</a></li>
                    <li class="breadcrumb-item active">Versión Software</li>
                </ol>
            </div>
        </div>
    </div>

    <div class="card">
        <div class="card-body">
            <h4 class="card-title">Nueva versión de software para ATM</h4>
            <h6 class="card-subtitle">Ingresar versión de software para ATM</h6>
            <div class="row col-12">
                <div class="col-12 grid-margin stretch-card">
                    <!--DATAGRID-->
                    <asp:UpdatePanel ID="UpdateDivBusquedas" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="row col-12">
                                <label class="col-3 col-form-label">Versiones de software creados</label>
                                <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                    <ContentTemplate>
                                        <asp:LinkButton runat="server" Visible="false" ID="btnnewversionATM" OnClick="btnnewversionATM_Click" CssClass="btn btn-info icon-plus mr-2" Text=""></asp:LinkButton>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                            <div class="table-responsive m-t-20">
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
                                                <asp:BoundField DataField="Id_software_ATM" HeaderText="Código de versión de software" ItemStyle-HorizontalAlign="center" />
                                                <asp:BoundField DataField="Descripcion" HeaderText="Versión de software" ItemStyle-HorizontalAlign="center" />
                                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="BtnEditar" Visible="false" runat="server" CssClass="btn btn-info ti-pencil-alt mr-2" Text="" CommandArgument='<%# Eval("Id_software_ATM") %>' CommandName="Codigo"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <!--MODAL GUARDAR tipoATM -->
    <div class="modal fade bs-example-modal-lg" id="modalversionATM" tabindex="-1" data-backdrop="static" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myLargeModalLabel">¿Seguro que modificará versión de software?</h4>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="row col-12">
                            <asp:Label runat="server" BorderStyle="None" class="col form-control col-6"><strong>Código de version de software: </strong></asp:Label>
                            <asp:Label runat="server" BorderStyle="None" ID="lbcodversionATM" class="col form-control col-6"></asp:Label>
                        </div>
                        <div class="row col-12">
                            <asp:Label runat="server" BorderStyle="None" class="col form-control col-6"><strong>Versión de software: </strong></asp:Label>
                            <asp:Label runat="server" BorderStyle="None" ID="lbNombreversionATM" class="col form-control col-6"></asp:Label>
                        </div>
                        <div class="row col-12">
                            <asp:Label runat="server" BorderStyle="None" class="col form-control col-6"><strong>*Nueva versión de software: </strong></asp:Label>
                            <asp:TextBox runat="server" ID="txtModalNewVersionATM" CssClass="form-control col-6"></asp:TextBox>
                        </div><br />
                        <%--<div class="col-md-8 align-self-center" style="margin-left: auto; margin-right: auto">
                            <br />
                            <h6 runat="server" visible="false" id="H5Alerta1" class="text-danger col-12" style="text-align: center;">Los campos con(*) son obligatorios.</h6>
                        </div>--%>
                        <div class="col-md-12 align-self-center" style="margin-left: auto; margin-right: auto">
                            <asp:TextBox runat="server" Enabled="false" Text="Ingrese nueva versión de software." Visible="false" ID="txtAlerta1" CssClass="form-control" Style="background-color: red; color: white; text-align: center;" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <div class="modal-footer col-12">
                            <asp:Button runat="server" ID="btnModalCerrarVersionATM" OnClick="btnModalCerrarVersionATM_Click" CssClass="btn btn-secundary mr-2" Text="Cancelar" />
                                <asp:Button runat="server" ID="btnModalEnviarVersionATM" OnClick="btnModalEnviarVersionATM_Click" CssClass="btn btn-success mr-2" Text="Modificar" />
                            
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
            <!-- /.modal-content -->
        </div>
        <!--/.modal-dialog -->
    </div>

    <!--MODAL NUEVA MODELOATM -->
    <div class="modal fade bs-example-modal-lg" id="modalversion2ATM" tabindex="-1" data-backdrop="static" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myLargeModalLabel1">Nueva versión de software</h4>
                </div>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <br />
                        <div class="row col-12">
                            <asp:Label runat="server" BorderStyle="None" class="col form-control col-6"><strong>*Versión de software: </strong></asp:Label>
                            <asp:TextBox runat="server" ID="txtNewVersionATM" CssClass="form-control col-6"></asp:TextBox>
                        </div><br />
                       <%-- <div class="col-md-8 align-self-center" style="margin-left: auto; margin-right: auto">
                            <br />
                            <h6 runat="server" visible="false" id="H5Alerta2" class="text-danger col-12" style="text-align: center;">Los campos con(*) son obligatorios.</h6>
                        </div>--%>
                        <div class="col-md-12 align-self-center" style="margin-left: auto; margin-right: auto">
                            <asp:TextBox runat="server" Enabled="false" Text="Ingrese nueva versión de software." Visible="false" ID="txtAlerta2" CssClass="form-control" Style="background-color: red; color: white; text-align: center;" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <div class="modal-footer col-12">
                            <asp:Button runat="server" ID="btnModalCerrarNueviVersionATM" OnClick="btnModalCerrarNueviVersionATM_Click" CssClass="btn btn-secundary mr-2" Text="Cancelar" />
                                <asp:Button runat="server" ID="btnModalNueviVersionATM" OnClick="btnModalNueviVersionATM_Click" CssClass="btn btn-success mr-2" Text="Agregar" />
                            
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
            <!-- /.modal-content -->
        </div>
        <!--/.modal-dialog -->
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
