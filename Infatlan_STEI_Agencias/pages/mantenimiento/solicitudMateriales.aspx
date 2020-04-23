<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="solicitudMateriales.aspx.cs" Inherits="Infatlan_STEI_Agencias.pages.mantenimiento.solicitudMateriales" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
        <div class="row page-titles">
        <div class="col-md-7 align-self-center">
            <h2 class="text-themecolor">Material que saldrá del almacén</h2>
            <div class="mr-md-3 mr-xl-5">
                <%-- <h2>Creación de Notificación</h2>--%>
                <p class="mb-md-0">Soporte Técnico y Comunicaciones</p>
            </div>
        </div>
    </div>


           <div class="form-body col-12">
                                    <div class="row">
                                        <div class="col-6">
                                            <div class="form-group row">
                                                <label class="col-form-label col-12">Código</label>
                                                <div class="col-12">
                                                    <asp:TextBox runat="server" ID="TxCodigo" CssClass="form-control"></asp:TextBox>                                        
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-6">
                                            <div class="form-group row">
                                                <label class="col-form-label col-12">Artículo del almacén</label>
                                                <div class="col-12">
                                                    <asp:DropDownList runat="server" ID="DDLArticulo" CssClass="select2 form-control custom-select" style="width: 100%"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-6">
                                            <div class="form-group row">
                                                <label class="col-form-label col-12">Cantidad</label>
                                                <div class="col-12">
                                                    <asp:TextBox runat="server" ID="TxCantidad" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-6">
                                            <div class="form-group row">
                                                <label class="col-form-label col-12">Tipo de Transaccion</label>
                                                <div class="col-12">
                                                    <asp:DropDownList runat="server" ID="DDLTipoTransaccion" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-6">
                                            <div class="form-group row">
                                                <label class="col-form-label col-12">Ubicación de Destino</label>
                                                <div class="col-10">
                                                    <asp:DropDownList runat="server" ID="DDLUbicacion" CssClass="select2 form-control custom-select" style="width: 100%"></asp:DropDownList>
                                                </div>
                                                <div class="col-1">
                                                    <asp:Button ID="BtnAddUbicacion" Text="+" CssClass="btn btn-primary" runat="server"/>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-6">
                                            <div class="form-group row">
                                                <label class="col-form-label col-12">Descripción</label>
                                                <div class="col-12">
                                                    <asp:TextBox runat="server" ID="TxDescripcion" Rows="3" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
