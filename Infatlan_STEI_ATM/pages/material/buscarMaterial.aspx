<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="buscarMaterial.aspx.cs" Inherits="Infatlan_STEI_ATM.pages.material.buscarMaterial" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
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
            <h3 class="text-themecolor col-12">Mantenimientos de ATM</h3>
            <h6 class="text-themecolor col-12">Seleccione mantenimiento al que solicitará materiales</h6>
        </div>
        <div class="col-md-7 align-self-center text-right">
            <div class="d-flex justify-content-end align-items-center">
            </div>
        </div>
    </div>
    <!--/ENCABEZADO-->

    <div class="card">
        <br />
      
        <div class=" col-12 align-self-center" style="margin-left: auto; margin-right: auto">
            <div class="row">
                <div class="col-12 grid-margin stretch-card">
                    <div class="card">
                        <div class="card-body">
        <!--DATAGRID-->
        <asp:UpdatePanel ID="UpdateDivBusquedas" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="row">
                    <div class="col-12 grid-margin stretch-card">
                        <div class="card" id="21">
                            <div class="card-body">
                                <%--<h4 class="card-title">Lista de verificación</h4>--%>

                                <div class="col-md-6">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">Buscar</label>
                                        <div class="col-sm-9">
                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="TxBuscarMantenimientoATM" OnTextChanged="TxBuscarMantenimientoATM_TextChanged" runat="server" placeholder="ingrese nombre de ATM - Presione afuera para proceder" class="form-control" AutoPostBack="true"></asp:TextBox>
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
                                                       
                                                        <asp:BoundField DataField="ID" HeaderText="Código" Visible="false" ItemStyle-HorizontalAlign="center" />
                                                        <asp:BoundField DataField="Codigo" HeaderText="Código de ATM" ItemStyle-HorizontalAlign="center" />
                                                        <asp:BoundField DataField="NomATM" HeaderText="Nombre" ItemStyle-HorizontalAlign="center" />
                                                        <asp:BoundField DataField="Tecnico" HeaderText="Técnico Responsable" ItemStyle-HorizontalAlign="center" />
                                                        <asp:BoundField DataField="Sucursal" HeaderText="Sucursal" ItemStyle-HorizontalAlign="center" />
                                                        <asp:BoundField DataField="Ubicacion" HeaderText="Ubicación" ItemStyle-HorizontalAlign="center" />
                                                        <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="btnVerifATM" Text="" CssClass="btn btn-info ti-pencil-alt mr-2" CommandArgument='<%# Eval("ID") %>' CommandName="Aprobar"></asp:LinkButton>
                                                                
                                                                <%-- <asp:Button ID="BtnUsuarioModificar" runat="server" Text="Modificar" CssClass="btn btn-rounded btn-block btn-success" CommandArgument='<%# Eval("codATM") %>' CommandName="Modificar" />--%>
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
        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <br />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
