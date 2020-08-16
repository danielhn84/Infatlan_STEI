<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="aprobacion.aspx.cs" Inherits="Infatlan_STEI_CableadoEstructurado.page.visitaTecnica.aprobacion" %>

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
                    <li class="breadcrumb-item active">Revisión</li>
                </ol>
            </div>
        </div>
    </div>

    <div class="card">
        <div class="card-body">
            
            <div class="card-body">
                <h4 class="card-title">Aprobación visita técnica</h4>
                <br />
                <h6 class="card-subtitle">Busqueda de estudios pendientes.</h6>

                <div class="row">
                    <label class="col-2 col-form-label">Búsqueda</label>
                    <div class="col-7">
                        <asp:TextBox ID="TxBuscarEstudio" runat="server" placeholder="Ej. Agencia- Presione afuera para proceder" class="form-control" AutoPostBack="true" OnTextChanged="TxBuscarEstudio_TextChanged"></asp:TextBox>
                    </div>
                </div>
            <br />
                <asp:UpdatePanel ID="udpAprobacion" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <p class="m-t-20"><asp:Label runat="server" ID="LbDescripcionGV" Text="Estudios ingresados por el técnico pendientes de realizar revisión"></asp:Label></p>

                        <div class="table-responsive">
                            <asp:GridView ID="GVAprobacion" runat="server"
                                CssClass="table table-bordered"
                                AutoPostBack="true"
                                PagerStyle-CssClass="pgr"
                                HeaderStyle-CssClass="table"
                                RowStyle-CssClass="rows"
                                AutoGenerateColumns="false"
                                AllowPaging="true"
                                GridLines="None"
                                PageSize="10"
                                OnPageIndexChanging="GVAprobacion_PageIndexChanging"
                                OnRowCommand="GVAprobacion_RowCommand">

                                <Columns>
                                    <asp:BoundField DataField="idEstudio" HeaderText="Estudio" Visible="false" />
                                    <asp:BoundField DataField="nombre" HeaderText="Estudio" />
                                    <asp:BoundField DataField="agencia" HeaderText="Agencia" />
                                    <asp:BoundField DataField="responsable" HeaderText="Técnico Responsable" />
                                    <asp:BoundField DataField="fechaCreacion" HeaderText="Creación" />
                                    <asp:TemplateField HeaderText="" HeaderStyle-Width="10%" Visible="true">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="BtnEditar" Visible="false" runat="server" Title="Aprobar" class="btn btn-primary mr-2" CommandArgument='<%#Eval("idEstudio") %>' CommandName="Aprobar"><i class="ti-check-box" style="align-content:center"></i></asp:LinkButton>
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
