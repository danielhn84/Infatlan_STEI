<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="principalVisitaTecnica.aspx.cs" Inherits="Infatlan_STEI_CableadoEstructurado.page.visita.principalVisitaTecnica" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div class="row page-titles">
        <div class="col-md-5 align-self-center">
            <h4 class="text-themecolor">STEI</h4>
        </div>
        <div class="col-md-7 align-self-center text-right">
            <div class="d-flex justify-content-end align-items-center">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Módulos</a></li>
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Cableado</a></li>
                    <li class="breadcrumb-item active">Visita Técnica</li>
                </ol>
            </div>
        </div>
    </div>
    <div class="card">
        <div class="card-body">
            
            <div class="card-body">
                <h4 class="card-title">Visita técnica</h4>
                <br />
                <h6 class="card-subtitle"><asp:Label runat="server" ID="Label2" Text="Busqueda de estudios pendientes por modificar."></asp:Label></h6>

                <div class="row">
                    <label class="col-md-2 col-form-label">Búsqueda</label>
                    <div class="col-7">
                        <asp:TextBox ID="TxBuscarVisita" runat="server" placeholder="Ej. Agencia - Presione afuera para proceder" class="form-control" AutoPostBack="true" OnTextChanged="TxBuscarVisita_TextChanged"></asp:TextBox>
                    </div>
                    <asp:Button ID="btnNuevo" Visible="false" runat="server" Text="Nuevo Estudio" class="btn btn-primary" OnClick="btnNuevo_Click" />
                </div>
                <br />
                <asp:UpdatePanel ID="udpPrincipalVisita" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <p class="m-t-20"><asp:Label runat="server" ID="LbDescripcionEdicion" Text="Estudios pendientes por realizar modificación"></asp:Label></p>
                        <div class="table-responsive">
                            <asp:GridView ID="GVPrincipalVisita" runat="server"
                                CssClass="table table-bordered"
                                AutoPostBack="true"
                                PagerStyle-CssClass="pgr"
                                HeaderStyle-CssClass="table"
                                RowStyle-CssClass="rows"
                                AutoGenerateColumns="false"
                                AllowPaging="true"
                                GridLines="None"
                                PageSize="10"
                                OnPageIndexChanging="GVPrincipalVisita_PageIndexChanging"
                                OnRowCommand="GVPrincipalVisita_RowCommand">
                                <Columns>
                                    <asp:BoundField DataField="idEstudio" HeaderText="Estudio" Visible="false" />
                                    <asp:BoundField DataField="nombre" HeaderText="Estudio" />
                                    <asp:BoundField DataField="agencia" HeaderText="Nombre" />
                                    <asp:BoundField DataField="responsable" HeaderText="Observación" />
                                    <asp:BoundField DataField="fechaCreacion" HeaderText="Creación" />
                                    <asp:TemplateField HeaderText="" HeaderStyle-Width="10%" Visible="true">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="BtnEntrar" Title="Entrar" runat="server" Text="Entrar" class="btn btn-primary mr-2" CommandArgument='<%#Eval("idEstudio")%>' CommandName="Entrar"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
