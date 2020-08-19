<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="pendienteAsignarResponsable.aspx.cs" Inherits="Infatlan_STEI_Comunicacion.pages.mantenimiento.pendienteAsignarResponsable" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <script type="text/javascript">
        var updateProgress = null;
        function postbackButtonClick() {
            updateProgress = $find("<%= UpdateProgress1.ClientID %>");
            window.setTimeout("updateProgress.set_visible(true)", updateProgress.get_displayAfter());
            return true;
        }
    </script>
    <script type="text/javascript">
         function openModalConfirmar() { $('#ModalConfirmar').modal('show'); }
         function closeModalConfirmar() { $('#ModalConfirmar').modal('hide'); }
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
                    <li class="breadcrumb-item active">Asignar Responsable</li>
                </ol>
            </div>
        </div>
    </div>

    <div class="card">
        <div class="card-body">
            <h4 class="card-title">Asignacion de Responsable</h4>
            <h6 class="card-subtitle">Ingeniero responsable para llevar a cabo el mantenimiento</h6>
            <nav>
                <div class="nav nav-pills " id="nav-tab" role="tablist">
                    <a class="nav-item nav-link active" id="nav_cargar_tab" data-toggle="tab" href="#navAsignacion" role="tab" aria-controls="nav-profile" aria-selected="false"><i class="icon-plus"></i>Asignación</a>
                    <%--<a runat="server" visible="true" class="nav-item nav-link" id="Registros" data-toggle="tab" href="#navEDC" role="tab" aria-controls="nav-profile" aria-selected="false"><i style="margin-right: 5px" class="icon-puzzle"></i>Modificar</a>--%>
                </div>
            </nav>
            <hr />
            <div class="tab-content" id="nav-tabContent">
                <div class="tab-pane fade show active" id="navAsignacion" role="tabpanel" aria-labelledby="nav-cargar-tab">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="card-body">
                                <div class="table-responsive m-t-20">
                                    <asp:GridView ID="GVAsignacion" runat="server"
                                        CssClass="table table-bordered"
                                        PagerStyle-CssClass="pgr"
                                        HeaderStyle-CssClass="table"
                                        RowStyle-CssClass="rows"
                                        AutoGenerateColumns="false"
                                        AllowPaging="true"
                                        GridLines="None" OnPageIndexChanging="GVAsignacion_PageIndexChanging"
                                        PageSize="10" OnRowDataBound="GVAsignacion_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="idMantenimiento" HeaderText="Id" />
                                            <asp:BoundField DataField="nombreNodo" HeaderText="Nodo" />
                                            <asp:BoundField DataField="serie" HeaderText="Serie" />
                                            <asp:BoundField DataField="ip" HeaderText="Ip" />
                                            <asp:BoundField DataField="regiones" HeaderText="Region" />
                                            <asp:BoundField DataField="tipoStock" HeaderText="Tipo" />
                                            <asp:BoundField DataField="direccion" HeaderText="Direccion" />
                                            <asp:BoundField DataField="fechaMantenimiento" HeaderText="Mantenimiento" />

                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="240">
                                                <HeaderTemplate>Responsable</HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:DropDownList runat="server" ID="DDLResponsable" CssClass="form-control"></asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="col-12">
                                    <asp:Button runat="server" ID="BtnEnviar" CssClass="btn btn-success" Text="Enviar" OnClick="BtnEnviar_Click" />
                                    <asp:Button runat="server" ID="BtnCancelar" CssClass="btn btn-danger" Text="Cancelar" />
                                </div>
                            </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <%--MODAL DE CONFIRMACION--%>
    <div class="modal fade" id="ModalConfirmar" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="ModalLabelConfirmar">
                        <b>
                            <asp:Label Text="Guardar Responsables" runat="server" ID="LbTitulo" CssClass="col-form-label"></asp:Label></b>
                    </h4>
                </div>

                <div class="modal-body">
                    <p>Si esta seguro dar clic en el botón "Enviar"</p>
                </div>

                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                            <asp:Button ID="BtnConfirmar" runat="server" Text="Enviar" class="btn btn-danger" OnClick="BtnConfirmar_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
