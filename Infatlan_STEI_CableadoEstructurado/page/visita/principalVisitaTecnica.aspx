<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="principalVisitaTecnica.aspx.cs" Inherits="Infatlan_STEI_CableadoEstructurado.page.visita.principalVisitaTecnica" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

     <div class="row page-titles">
        <div class="col-md-12 align-self-center">
            <h3 class="text-themecolor">
                <asp:Label ID="Label5" runat="server" Text="Creación Estudios" Style="margin-left: auto; margin-right: auto"></asp:Label>
            </h3>
            <div class="mr-md-3 mr-xl-5">
                <p class="mb-md-0">Soporte Técnico y Comunicaciones</p>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-body">

                    <div class="card-group">
                        <div class="card">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="d-flex no-block align-items-center">
                                            <div>
                                                <h3><i class="icon-screen-desktop"></i></h3>
                                                <p class="text-muted">Estudios Creados</p>
                                            </div>
                                            <div class="ml-auto">
                                                <h2 class="counter text-primary">
                                                    <asp:Label runat="server" ID="lbEstudiosCreados"></asp:Label>
                                                </h2>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12">
                                        <div class="progress">
                                            <div class="progress-bar bg-primary" role="progressbar" style="width: 85%; height: 6px;" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="card">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="d-flex no-block align-items-center">
                                            <div>
                                                <h3><i class="icon-note"></i></h3>
                                                <p class="text-muted">Estudios en Proceso de Edición</p>
                                            </div>
                                            <div class="ml-auto">
                                                <h2 class="counter text-cyan">
                                                    <asp:Label runat="server" ID="lbEstudiosEdicion"></asp:Label>
                                                </h2>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12">
                                        <div class="progress">
                                            <div class="progress-bar bg-cyan" role="progressbar" style="width: 85%; height: 6px;" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>

                </div>
            </div>
        </div>
    </div>


    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Estudios </h4>
                    <p>Estudios existentes.</p>
                    <div class="col-md-12">
                        <div class="form-group row">
                            <label class="col-sm-1 col-form-label">Buscar</label>
                            <div class="col-sm-8">
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="TxBuscarVisita" runat="server" placeholder="Ej. Agencia - Presione afuera para proceder" class="form-control" AutoPostBack="true" OnTextChanged="TxBuscarVisita_TextChanged"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-sm-3">
                                <asp:Button ID="btnNuevo" runat="server" Text="Nuevo Estudio" class="btn btn-primary" OnClick="btnNuevo_Click" />                                        
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

      <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-body">
                    <div class="row">

                        <div class="col-md-12">
                            <asp:UpdatePanel ID="udpPrincipalVisita" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="col-12 grid-margin stretch-card">
                                        <div class="card">
                                            <div class="card-body">
                                                <h3 class="card-title">Lista de Estudios</h3>
                                                <p>Estudios pendientes por realizar modificación</p>
                                                <div class="row">
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
                                                                <asp:BoundField DataField="observacion" HeaderText="Observación" />
                                                                <asp:BoundField DataField="fechaCreacion" HeaderText="Creación" />
                                                                 <asp:TemplateField HeaderText="Seleccione" HeaderStyle-Width="10%" Visible="true">
                                                                    <ItemTemplate>

                                                                        <asp:LinkButton ID="BtnEntrar" Title="Entrar" runat="server" Text="Entrar" class="btn btn-inverse-primary" CommandArgument='<%#Eval("idEstudio")%>' CommandName="Entrar"></asp:LinkButton>

                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
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
