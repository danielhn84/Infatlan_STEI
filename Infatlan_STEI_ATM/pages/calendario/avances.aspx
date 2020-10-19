<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="avances.aspx.cs" Inherits="Infatlan_STEI_ATM.pages.calendario.avances" %>
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
            <h4 class="text-themecolor">STEI</h4>
        </div>
        <div class="col-md-7 align-self-center text-right">
            <div class="d-flex justify-content-end align-items-center">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Módulos</a></li>
                    <li class="breadcrumb-item"><a href="javascript:void(0)">ATM</a></li>
                    <li class="breadcrumb-item active">Avances</li>
                </ol>
            </div>
        </div>
    </div>

    <div class="card">
        <div class="card-body">
            <h4 class="card-title">Avances</h4>
            <h6 class="card-subtitle">Lista de mantenimientos y su progreso</h6><br />
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <asp:DropDownList AutoPostBack="true" ID="DDLFiltroEstado" OnTextChanged="DDLFiltroEstado_TextChanged" CssClass="form-control col-4" runat="server"> </asp:DropDownList>
                     <div class="col-md-12 align-self-center" style="margin-left: auto; margin-right: auto">
                                <div class="table-responsive">
                                    <asp:GridView ID="GVAvances" runat="server"
                                        CssClass="table table-bordered"
                                        PagerStyle-CssClass="pgr"
                                        HeaderStyle-CssClass="table"
                                        RowStyle-CssClass="rows"
                                        AutoGenerateColumns="false"
                                        AllowPaging="true"
                                        GridLines="None"
                                        HeaderStyle-HorizontalAlign="center"
                                        PageSize="10" OnPageIndexChanging="GVAvances_PageIndexChanging"
                                        Style="margin: 30px 0px 20px 0px">
                                        <Columns>
                                            <asp:BoundField DataField="ID" HeaderText="Código" Visible="false" ItemStyle-HorizontalAlign="center" />
                                            <asp:BoundField DataField="CodATM" HeaderText="Código" ItemStyle-HorizontalAlign="center" />
                                            <asp:BoundField DataField="Nombre" HeaderText="ATM" ItemStyle-HorizontalAlign="center" />
                                            <asp:BoundField DataField="Fecha" HeaderText="Fecha Mantenimiento" HtmlEncode=False DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="center" />
                                            <asp:BoundField DataField="Avance" HeaderText="Avance" ItemStyle-HorizontalAlign="center" />                                           
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
