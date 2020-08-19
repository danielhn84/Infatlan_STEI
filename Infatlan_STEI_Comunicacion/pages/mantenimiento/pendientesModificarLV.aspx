<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="pendientesModificarLV.aspx.cs" Inherits="Infatlan_STEI_Comunicacion.pages.mantenimiento.pendientesModificarLV" %>
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
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="/assets/images/loading.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="display: inline-block; vertical-align: middle;" />
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
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Comunicación</a></li>
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Mantenimiento</a></li>
                    <li class="breadcrumb-item active">Pendientes Modificar LV</li>
                </ol>
            </div>
        </div>
    </div>

    <asp:UpdatePanel runat="server" ID="UpPendientesModificarLV" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Mantenimientos Pendientes Modificar LV</h4>
                    <h6 class="card-subtitle">Mantenimientos pendientes que estan asignadas a su persona.</h6>
                    <div class="card-body">
                        <div class="row col-7">
                            <label class="col-2 col-form-label">Búsqueda</label>
                            <div class="col-8">
                                <asp:TextBox runat="server" PlaceHolder="Ingrese texto y presione Enter" ID="TxBusqueda" AutoPostBack="true" CssClass="form-control form-control-line"></asp:TextBox>
                            </div>
                        </div>

                        <div class="table-responsive m-t-20">
                            <asp:GridView ID="GvPendientesModificarLV" runat="server"
                                CssClass="table table-bordered"
                                PagerStyle-CssClass="pgr"
                                HeaderStyle-CssClass="table"
                                RowStyle-CssClass="rows"
                                AutoGenerateColumns="false"
                                AllowPaging="true"
                                GridLines="None"
                                PageSize="10">
                                <Columns>
                                    <asp:BoundField DataField="idMantenimiento" HeaderText="Id" />
                                    <asp:BoundField DataField="nombreNodo" HeaderText="Nodo" />
                                    <asp:BoundField DataField="serie" HeaderText="Serie" />
                                    <asp:BoundField DataField="ip" HeaderText="Ip" />
                                    <asp:BoundField DataField="regiones" HeaderText="Region" />
                                    <asp:BoundField DataField="tipoStock" HeaderText="Tipo" />
                                    <asp:BoundField DataField="direccion" HeaderText="Direccion" />
                                    <asp:BoundField DataField="fechaMantenimiento" HeaderText="Mantenimiento" />
                                    <asp:BoundField DataField="motivoCanceloJefe" HeaderText="Observación" />
                                    <asp:TemplateField HeaderText="Seleccione" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LbModificar" runat="server" class="btn btn-info mr-2" CommandName="Modificar" CommandArgument='<%# Eval("idMantenimiento") %>'>
                                                       <i class="icon-pencil" ></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>




</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
