<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="principalPresupuestos.aspx.cs" Inherits="Infatlan_STEI_CableadoEstructurado.paginas.principalPresupuestos" %>

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
                    <li class="breadcrumb-item active">Cotización</li>
                </ol>
            </div>
        </div>
    </div>

    <div class="card">
        <div class="card-body">
            <h4 class="card-title">Cotización Estudios</h4>
            <h6 class="card-subtitle">Contabilidad</h6>
            <div class="card-body">
                <div class="row col-8">
                    <label class="col-2 col-form-label">Búsqueda</label>
                    <div class="col-7">
                        <asp:TextBox ID="TxBuscarEstudio" runat="server" placeholder="Ej. Ag.Junior- Presione afuera para proceder" class="form-control" AutoPostBack="true" OnTextChanged="TxBuscarEstudio_TextChanged"></asp:TextBox>
                    </div>
                </div>

                <asp:UpdatePanel ID="udpContabilidad" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <p class="m-t-20"><asp:Label runat="server" ID="LbDescripcionAprobacion" Text="Estudios revisados pendientes de realizar cotización"></asp:Label></p>
                        <div class="table-responsive">
                            <asp:GridView ID="GVPrincipal" runat="server"
                                CssClass="table table-bordered"
                                AutoPostBack="true"
                                PagerStyle-CssClass="pgr"
                                HeaderStyle-CssClass="table"
                                RowStyle-CssClass="rows"
                                AutoGenerateColumns="false"
                                AllowPaging="true"
                                GridLines="None"
                                PageSize="10"
                                OnPageIndexChanging="GVPrincipal_PageIndexChanging"
                                OnRowCommand="GVPrincipal_RowCommand">
                                <Columns>
                                    <asp:BoundField DataField="idEstudio" HeaderText="Estudio" Visible="false" />
                                    <asp:BoundField DataField="nombre" HeaderText="Estudio" />
                                    <asp:BoundField DataField="agencia" HeaderText="Nombre" />
                                    <asp:BoundField DataField="responsable" HeaderText="Técnico Responsable" />
                                    <asp:BoundField DataField="fechaCreacion" HeaderText="Creación" />
                                    <asp:TemplateField HeaderText="" HeaderStyle-Width="10%" Visible="true">
                                        <ItemTemplate>

                                            <%--<asp:LinkButton ID="BtnDescargar" Title="Descargar" runat="server" Text="Eliminar" class="btn btn-inverse-primary" CommandArgument='<%#Eval("idEstudio")%>' CommandName="Descargar"><i class="ti-import" ></i>
                                                            </asp:LinkButton>--%>

                                            <asp:LinkButton ID="BtnEntrar" Title="Entrar" runat="server" Text="Entrar" class="btn btn-primary mr-2" CommandArgument='<%#Eval("idEstudio") + ";" +Eval("nombre")%>' CommandName="Entrar"></asp:LinkButton>

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
