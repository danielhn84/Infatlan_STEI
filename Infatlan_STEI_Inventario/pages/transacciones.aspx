<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="transacciones.aspx.cs" Inherits="Infatlan_STEI_Inventario.pages.transacciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <script type="text/javascript">
        var updateProgress = null;

        function postbackButtonClick() {
            updateProgress = $find("<%= UpdateProgress1.ClientID %>");
            window.setTimeout("updateProgress.set_visible(true)", updateProgress.get_displayAfter());
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #ffffff; opacity: 0.7; margin: 0;">
                <span style="display: inline-block; height: 100%; vertical-align: middle;"></span>
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="/images/loading.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="display: inline-block; vertical-align: middle;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <br />
    <asp:UpdatePanel runat="server" ID="UpdatePanel">
        <ContentTemplate>
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Transacciones</h4>
                    <h6 class="card-subtitle">Historial de transacciones realizadas.</h6>
                    <br />
                    <div class="row col-7"> 
                        <label class="col-2 col-form-label">Búsqueda</label>
                        <div class="col-8">
                            <asp:TextBox runat="server" PlaceHolder="Ingrese texto y presione Enter" ID="TxBusqueda" AutoPostBack="true" OnTextChanged="TxBusqueda_TextChanged" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="table-responsive m-t-40">
                        <asp:GridView ID="GVBusqueda" runat="server"
                            CssClass="table table-bordered"
                            PagerStyle-CssClass="pgr"
                            HeaderStyle-CssClass="table"
                            RowStyle-CssClass="rows"
                            AutoGenerateColumns="false"
                            AllowPaging="true"
                            GridLines="None"
                            PageSize="10" OnPageIndexChanging="GVBusqueda_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="idTransaccion" HeaderText="No." />
                                <asp:BoundField DataField="Tipo" HeaderText="Tipo" />
                                <asp:BoundField DataField="Articulo" HeaderText="Articulo" />
                                <asp:BoundField DataField="ubicacionAnterior" HeaderText="UbicacionAntes"/>
                                <asp:BoundField DataField="ubicacionActual" HeaderText="UbicacionActual"/>
                                <asp:BoundField DataField="fechaRegistro" HeaderText="Fecha"/>
                                
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
