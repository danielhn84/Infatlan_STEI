﻿<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="crearVerifCorrectivo.aspx.cs" Inherits="Infatlan_STEI_ATM.pages.correctivo.crearVerifCorrectivo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <!--PARA LLAMAR MODAL-->
    <script type="text/javascript">
        function openModal() { $('#modalCancelarverificacion').modal('show'); }
        function closeModal() { $('#modalCancelarverificacion').modal('hide'); }
    </script>
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
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Mantenimiento Correctivo</a></li>
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Verificación</a></li>
                    <li class="breadcrumb-item active">Crear</li>
                </ol>
            </div>
        </div>
    </div>

    <div class="card">
        <div class="card-body">
            <h4 class="card-title">Verificación de mantenimiento</h4>
            <h6 class="card-subtitle">Crear lista de verificación de mantenimiento correctivo de ATM</h6>
            <div class="row col-12">
                <div class="col-12 grid-margin stretch-card">
                    <!--DATAGRID-->
                    <asp:UpdatePanel ID="UpdateDivBusquedas" runat="server">
                        <ContentTemplate>
                            <div class="col-md-6">
                                <div class="form-group row">
                                    <label class="col-sm-3 col-form-label">Buscar</label>
                                    <div class="col-sm-9">
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="TxBuscarTecnicoATM" OnTextChanged="TxBuscarTecnicoATM_TextChanged" runat="server" placeholder="ingrese empleado responsable - Presione afuera para proceder" class="form-control" AutoPostBack="true"></asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>

                            <div class="table-responsive m-t-20">
                                <asp:UpdatePanel ID="UpdateGridView" runat="server">
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
                                                <asp:BoundField DataField="ID" HeaderText="Código" Visible="false" ItemStyle-HorizontalAlign="center" />
                                                <asp:BoundField DataField="Codigo" HeaderText="Código de ATM" ItemStyle-HorizontalAlign="center" />
                                                <asp:BoundField DataField="NomATM" HeaderText="Nombre" ItemStyle-HorizontalAlign="center" />
                                                <asp:BoundField DataField="Tecnico" HeaderText="Técnico Responsable" ItemStyle-HorizontalAlign="center" />
                                                <asp:BoundField DataField="Sucursal" HeaderText="Sucursal" ItemStyle-HorizontalAlign="center" />
                                                <asp:BoundField DataField="Ubicacion" HeaderText="Ubicación" ItemStyle-HorizontalAlign="center" />
                                                <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="btnAprobarATM" CssClass="btn btn-info ti-pencil-alt mr-2" CommandArgument='<%# Eval("ID") %>' CommandName="Aprobar"></asp:LinkButton>
                                                        <asp:LinkButton ID="BtnCancelar" runat="server" CssClass="btn btn-danger ti-na mr-2" CommandArgument='<%# Eval("ID") %>' CommandName="Cancelar"></asp:LinkButton>
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
     <!-- Modal al no realizar trabajo -->
    <div class="modal fade bs-example-modal-lg" id="modalCancelarverificacion" tabindex="-1" data-backdrop="static" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <h4 class="modal-title" id="myLargeModalLabel" runat="server"></h4>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    
                    <button type="button" id="btnexitModal" style="color: black;" class="close" data-dismiss="modal" aria-hidden="true">X</button>
                </div>

                <asp:UpdatePanel runat="server" ID="UPModal">
                    <ContentTemplate>
                        <div class="modal-body">
                            <div>
                                <asp:Label runat="server">Mantenimiento</asp:Label>
                            </div>
                            <div>
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtModalATM" Enabled="false"></asp:TextBox>
                            </div>
                            <br />
                            <div>
                                <asp:Label runat="server">Motivo de Cancelación de Mantenimiento</asp:Label>
                            </div>
                            <div>
                                <asp:TextBox ID="txtMotivo" runat="server" TextMode="Multiline" Rows="5" CssClass="form-control col-12"></asp:TextBox>
                            </div>
                           
                            <div class="col-md-12 align-self-center" style="margin-left: auto; margin-right: auto">
                                <asp:TextBox runat="server" Enabled="false" Text="Ingrese motivo por el que cancela verificación." Visible="false" ID="txtAlerta" CssClass="form-control" Style="background-color: red; color: white; text-align: center;" />
                            </div>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>

                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <div class="modal-footer">
                            <asp:Button runat="server" ID="btnMantSinRealizar" OnClick="btnMantSinRealizar_Click" CssClass="btn btn-success col-3" Text="Enviar" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
            <!-- /.modal-content -->
        </div>
        <!--/.modal-dialog -->
    </div>
    <!-- /Modal al no realizar trabajo -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>