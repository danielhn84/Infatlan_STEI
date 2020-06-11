<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="ofertaEconomica.aspx.cs" Inherits="Infatlan_STEI_CableadoEstructurado.page.cotizacion.ofertaEconomica" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div class="row page-titles">
        <div class="col-md-12 align-self-center">
            <h3 class="text-themecolor">
                <asp:Label ID="Label5" runat="server" Text="Oferta Económica" Style="margin-left: auto; margin-right: auto"></asp:Label>
            </h3>
            <div class="mr-md-3 mr-xl-5">
                <p class="mb-md-0">Contabilidad </p>
            </div>
        </div>
    </div>

    <asp:UpdatePanel ID="updBuscarAprobacion" runat="server" Visible="false">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="card">
                        <div class="card-body">

                            <h4 class="card-title">Oferta Económica</h4>
                            <p>Busqueda de documento de ofertas económicas existentes.</p>
                            <div class="col-md-12">
                                <div class="form-group row">
                                    <%--<label class="col-sm-1 col-form-label">Buscar</label>--%>
                                    <div class="col-sm-12">
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="TxBuscarOferta" runat="server" placeholder="Ej. Agencia- Presione afuera para proceder" class="form-control" AutoPostBack="true" OnTextChanged="TxBuscarOferta_TextChanged"></asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <%--<div class="col-sm-3">
                                        <asp:Button ID="btnNuevo" runat="server" Text="Crear Puesto" class="btn btn-primary" OnClick="btnNuevo_Click" />                                        
                                    </div>--%>
                                </div>
                            </div>


                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-body">
                    <div class="row">

                        <div class="col-md-12">
                            <asp:UpdatePanel ID="udpContabilidad" runat="server" UpdateMode="Conditional">
                                <contenttemplate>
                                    <div class="col-12 grid-margin stretch-card">
                                        <div class="card">
                                            <div class="card-body">
                                                <h3 class="card-title">Estudio</h3>
                                                <p>Datos de Oferta Económica</p>
                                                <div class="row">
                                                    <div class="table-responsive">
                                                        <asp:GridView ID="GVOfertaEconomica" runat="server"
                                                            CssClass="table table-bordered"
                                                            AutoPostBack="true"
                                                            PagerStyle-CssClass="pgr"
                                                            HeaderStyle-CssClass="table"
                                                            RowStyle-CssClass="rows"
                                                            AutoGenerateColumns="false"
                                                            AllowPaging="true"
                                                            GridLines="None"
                                                            PageSize="10" 
                                                            OnPageIndexChanging="GVOfertaEconomica_PageIndexChanging" 
                                                            OnRowCommand="GVOfertaEconomica_RowCommand">
                                                         
                                                            <Columns>
                                                                <asp:BoundField DataField="idEstudio" HeaderText="Estudio" Visible="false" />
                                                                <asp:BoundField DataField="nombre" HeaderText="Estudio" />
                                                                <asp:BoundField DataField="agencia" HeaderText="Agencia" />
                                                                <asp:BoundField DataField="responsable" HeaderText="Técnico Responsable" />
                                                                <asp:BoundField DataField="fechaCreacion" HeaderText="Creación" />
                                                                 <asp:TemplateField HeaderText="Seleccione" HeaderStyle-Width="10%" Visible="true">
                                                                    <ItemTemplate>

                                                                        <asp:LinkButton ID="BtnDescargar" Title="Descargar" runat="server"  class="btn btn-inverse-primary" CommandArgument='<%#Eval("idEstudio")%>' CommandName="Descargar"><i class="ti-import" ></i>
                                                                        </asp:LinkButton>

                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </contenttemplate>
                            </asp:UpdatePanel>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>




</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
