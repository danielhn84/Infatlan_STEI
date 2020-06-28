<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Infatlan_STEI_CableadoEstructurado._default" %>

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
                    <li class="breadcrumb-item active">Cableado</li>
                </ol>
            </div>
        </div>
    </div>

    <div class="card-group">
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="d-flex no-block align-items-center">
                            <div>
                                <h4 class="text-muted">Fecha</h4>
                                <div class="ml-auto">
                                    <i class="icon-lg mr-3 text-primary"></i>
                                    <h2 class="counter text-cyan">
                                        <asp:Label runat="server" ID="LbFechaDashboard"></asp:Label>
                                    </h2>
                                </div>
                            </div>
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
                                <h3><i class="icon-chart"></i></h3>
                                <p class="text-muted">
                                    <asp:Label ID="txtCreadas" runat="server" Text=""></asp:Label></p>
                            </div>
                            <div class="ml-auto">
                                <h2 class="counter text-primary">
                                    <asp:Label runat="server" ID="lbCreadas"></asp:Label>
                                </h2>
                            </div>
                        </div>
                    </div>
                    <div class="col-12">
                        <div class="progress">
                            <div class="progress-bar bg-primary" id="CSSCotizacion" runat="server" role="progressbar" style="width: 20%; height: 6px;" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
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
                                <p class="text-muted">
                                    <asp:Label ID="txtPendientes" runat="server" Text=""></asp:Label></p>
                            </div>
                            <div class="ml-auto">
                                <h2 class="counter text-cyan">
                                    <asp:Label runat="server" ID="lbPendientes"></asp:Label>
                                </h2>
                            </div>
                        </div>
                    </div>
                    <div class="col-12">
                        <div class="progress">
                            <div class="progress-bar bg-cyan" role="progressbar" style="width: 25%; height: 6px;" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="card">
        <div class="card-body">
            <h4 class="card-title">
                <asp:Label runat="server" ID="LbTituloDashb" Text="Lista de Estudios "></asp:Label>
            </h4>
            <h6 class="card-subtitle"><asp:Label runat="server" ID="LbDescripcionDashb" Text="Estudios ingresados por el técnico"></asp:Label></h6>
            <div class="card-body">
                <asp:UpdatePanel ID="udpGVDashboard" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
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
                                OnRowCommand="GVPrincipal_RowCommand"
                                OnRowDataBound="GVPrincipal_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="idEstudio" HeaderText="Estudio" Visible="false" />
                                    <asp:BoundField DataField="nombre" HeaderText="Estudio" />
                                    <asp:BoundField DataField="agencia" HeaderText="Nombre" />
                                    <asp:BoundField DataField="responsable" HeaderText="Técnico Responsable" />
                                    <asp:BoundField DataField="fechaCreacion" HeaderText="Creación" />
                                    <%--      <asp:TemplateField HeaderText="" HeaderStyle-Width="10%" Visible="true">
                                            <ItemTemplate>--%>

                                    <%--<asp:LinkButton ID="BtnDescargar" Title="Descargar" runat="server" Text="Eliminar" class="btn btn-inverse-primary" CommandArgument='<%#Eval("idEstudio")%>' CommandName="Descargar"><i class="ti-import" ></i>
                                                    </asp:LinkButton>--%>

                                    <%--<asp:LinkButton ID="BtnEntrar" Title="Entrar" runat="server" Text="Entrar" class="btn btn-primary mr-2" CommandArgument='<%#Eval("idEstudio") + ";" +Eval("nombre")%>' CommandName="Entrar"></asp:LinkButton>--%>

                                    <%-- </ItemTemplate>
                                        </asp:TemplateField>--%>
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
