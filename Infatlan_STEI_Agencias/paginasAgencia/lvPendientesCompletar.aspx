<%@ Page Title="" Language="C#" MasterPageFile="~/mainAgencia.Master" AutoEventWireup="true" CodeBehind="lvPendientesCompletar.aspx.cs" Inherits="Infatlan_STEI_Agencias.paginasAgencia.LvCompletar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">

    <div class="row page-titles">
        <div class="col-md-5 align-self-center">
            <h2 class="text-themecolor">Listas de Verificación Pendientes Completar</h2>
            <div class="mr-md-3 mr-xl-5">
                <%-- <h2>Creación de Notificación</h2>--%>
                <p class="mb-md-0">Soporte Técnico y Comunicaciones</p>
            </div>
        </div>

        <div class="col-md-7 align-self-center text-right">
            <div class="d-flex justify-content-end align-items-center">
                <button type="button" class="btn btn-danger d-none d-lg-block m-l-15"><i class="fa fa-plus-circle"></i> Cancelar LV</button>
            </div>
        </div>
    </div>

    <div class="card">
        <div class="card-body">
            <h4 class="card-title">LV Pendientes</h4>
            <p>Listas de verificación pendientes de completar que estan asignadas a su persona.</p>
            <div class="col-md-6">
                <div class="form-group row">
                    <label class="col-sm-3 col-form-label">Buscar</label>
                    <div class="col-sm-9">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <div class="input-group mb-3">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text" id="basic-addon1"><i class="ti-search"></i></span>
                                    </div>
                                    <asp:TextBox ID="TxBuscarAgencia" runat="server" placeholder="Búsqueda por nombre de agencia" class="form-control" AutoPostBack="true"></asp:TextBox>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>

        <div class="row col-12">
            <div class="col-12 grid-margin stretch-card">
                <div class="table-responsive">
                    <asp:UpdatePanel runat="server" ID="UPListaVerificacion">
                        <ContentTemplate>
                            <asp:GridView ID="GVListaVerificacion" runat="server"
                                CssClass="table table-bordered"
                                PagerStyle-CssClass="pgr"
                                HeaderStyle-CssClass="table"
                                RowStyle-CssClass="rows"
                                AutoGenerateColumns="false"
                                AllowPaging="true"
                                GridLines="None"
                                PageSize="10" OnRowCommand="GVListaVerificacion_RowCommand">

                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LBAprobar" runat="server" CssClass="btn btn-cyan" CommandName="Completar" CommandArgument='<%# Eval("id_Mantenimiento") %>'>
                                                        <i class="icon-docs"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="id_Mantenimiento" HeaderText="Id Mantenimiento" />
                                    <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                                    <asp:BoundField DataField="Lugar" HeaderText="Lugar" />
                                    <asp:BoundField DataField="Cod_Agencia" HeaderText="Cod. Agencia" />
                                    <asp:BoundField DataField="Area" HeaderText="Area" />
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>


    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
