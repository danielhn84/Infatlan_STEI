<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="cargaRegistros.aspx.cs" Inherits="Infatlan_STEI_Inventario.pages.Configuracion.cargaRegistros" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <script type="text/javascript">
        function openModalCarga() { $('#ModalCarga').modal('show'); }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <br /> 
    <div class="card">
        <div class="card-body">
            <h4 class="card-title">Carga de archivos</h4>
            <h6 class="card-subtitle">Descargue la plantilla respectiva.</h6>
            <asp:UpdatePanel runat="server" ID="UpdatePanel10" >
                <ContentTemplate>
                    <div class="table-responsive m-t-40">
                        <asp:GridView ID="GvBusqueda" runat="server"
                            CssClass="table table-bordered embed-responsive"
                            PagerStyle-CssClass="pgr"
                            HeaderStyle-CssClass="table"
                            RowStyle-CssClass="rows"
                            AutoGenerateColumns="false"
                            AllowPaging="true"
                            GridLines="None" OnRowCommand="GvBusqueda_RowCommand"
                            PageSize="10">
                            <Columns>
                                <asp:BoundField DataField="id" HeaderText="No." />
                                <asp:BoundField DataField="proceso" HeaderText="Proceso" />
                                <asp:TemplateField HeaderText="Seleccione">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="BtnEditar" runat="server" class="btn btn-primary mr-2" Title="Descargar Planilla" CommandArgument='<%# Eval("id") %>' CommandName="DescargarPlantilla">
                                                    <i class="icon-arrow-down-circle"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="BtnInfo" runat="server" class="btn btn-success mr-2" Title="Cargar Registros" CommandArgument='<%# Eval("id") %>' CommandName="CargarRegistros">
                                                    <i class="icon-arrow-up-circle"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </div>

    <%--MODAL DE CARGA ARCHIVO--%>
    <div class="modal fade" id="ModalCarga" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                        <ContentTemplate>
                            <h4 class="modal-title" id="ModalLabelCargar">
                                <b><asp:Label runat="server" ID="LbTituloCarga" CssClass="col-form-label" Text="Cargar Archivo"></asp:Label></b>
                            </h4>
                            <asp:Label runat="server" ID="Label4" CssClass="col-form-label"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label class="control-label col-12">Carga</label>
                        <div class="col-9">
                            <asp:FileUpload ID="FUCarga" CssClass="form-control" runat="server"/>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12" runat="server" id="DivMensajeCarga" visible="false" style="display: flex; background-color:tomato; justify-content:center">
                            <asp:Label runat="server" CssClass="col-form-label text-white" ID="LbAdvertenciaCarga"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel30" runat="server">
                        <ContentTemplate>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                            <asp:Button ID="BtnCargar" runat="server" Text="Aceptar" class="btn btn-info" OnClick="BtnCargar_Click"/>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
