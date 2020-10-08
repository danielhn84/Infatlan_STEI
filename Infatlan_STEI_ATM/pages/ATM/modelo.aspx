<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="modelo.aspx.cs" Inherits="Infatlan_STEI_ATM.pages.ATM.modelo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <!--PARA LLAMAR MODAL-->
    <script type="text/javascript">
        function openModal() { $('#modalcrearmodeloATM').modal('show'); }
        function openModal2() { $('#modalModeloATM').modal('show'); }
    </script>
    <!--PARA CERRAR MODAL-->
    <script type="text/javascript">
        function closeModal() { $('#modalcrearmodeloATM').modal('hide'); }
        function closeModal2() { $('#modalModeloATM').modal('hide'); }
    </script>

    <link href="/css/GridStyle.css" rel="stylesheet" />
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
                    <li class="breadcrumb-item"><a href="javascript:void(0)">ATM</a></li>
                    <li class="breadcrumb-item active">Modelo</li>
                </ol>
            </div>
        </div>
    </div>

    <div class="card">
        <div class="card-body">
            <h4 class="card-title">Modelo de ATM</h4>
            <h6 class="card-subtitle">Ingresar nuevo modelo de ATM</h6>
            <div class="row col-12">
                <div class="col-12 grid-margin stretch-card">
                    <asp:UpdatePanel ID="UpdateDivBusquedas" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="row col-12">
                                <label class="col-3 col-form-label">Modelos de ATM creados</label>
                                <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                    <ContentTemplate>
                                        <asp:LinkButton runat="server" Visible="false" ID="btnguardarmodeloATM" OnClick="btnguardarmodeloATM_Click" class="btn btn-info icon-plus mr-2" Text=""></asp:LinkButton>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                            <div class="table-responsive m-t-20">
                                <asp:UpdatePanel ID="UpdateGridView" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:GridView ID="GVBusqueda" runat="server"
                                            CssClass="table table-bordered"
                                            PagerStyle-CssClass="pgr"
                                            HeaderStyle-CssClass="table" HeaderStyle-HorizontalAlign="center"
                                            RowStyle-CssClass="rows"
                                            AutoGenerateColumns="false"
                                            AllowPaging="true"
                                            GridLines="None" OnPageIndexChanging="GVBusqueda_PageIndexChanging"
                                            PageSize="10" OnRowCommand="GVBusqueda_RowCommand">
                                            <Columns>

                                                <asp:BoundField DataField="Id_Modelo_ATM" HeaderText="Código de modelo ATM" ItemStyle-HorizontalAlign="center" />
                                                <asp:BoundField DataField="Descripcion" HeaderText="Modelo de ATM" ItemStyle-HorizontalAlign="center" />
                                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="BtnEditar" Visible="false" runat="server" CssClass="btn btn-info ti-pencil-alt mr-2" Text="" CommandArgument='<%# Eval("Id_Modelo_ATM") %>' CommandName="Codigo"></asp:LinkButton>
                                                        <%--<asp:Button ID="BtnUsuarioPassword" runat="server" Text="De baja" CssClass="btn btn-block btn-outline-danger" CommandArgument='<%# Eval("codATM") %>' CommandName="Baja" />--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>            
    </div>

    <!--MODAL GUARDAR tipoATM -->
    <div class="modal fade bs-example-modal-lg" id="modalcrearmodeloATM" tabindex="-1" data-backdrop="static" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myLargeModalLabel">¿Seguro que modificará modelo de ATM?</h4>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <br />
                        <div class="row col-12">
                            <asp:Label runat="server" BorderStyle="None" class="col form-control col-6"><strong>Código de modelo ATM: </strong></asp:Label>
                            <asp:Label runat="server" BorderStyle="None" ID="lbcodmodeloATM" class="col form-control col-6"></asp:Label>
                        </div>
                        <div class="row col-12">
                            <asp:Label runat="server" BorderStyle="None" class="col form-control col-6"><strong>Nombre de modelo ATM: </strong></asp:Label>
                            <asp:Label runat="server" BorderStyle="None" ID="lbNombremodeloATM" class="col form-control col-6"></asp:Label>
                        </div>
                        <div class="row col-12">
                            <asp:Label runat="server" BorderStyle="None" class="col form-control col-6"><strong>*Nuevo nombre modelo ATM: </strong></asp:Label>
                            <asp:TextBox runat="server" ID="txtModalNewModeloATM" CssClass="form-control col-6"></asp:TextBox>
                        </div><br />
                       <%-- <div class="col-md-8 align-self-center" style="margin-left: auto; margin-right: auto">
                            <br />
                            <h6 runat="server" visible="false" id="H5Alerta1" class="text-danger col-12" style="text-align: center;">Los campos con(*) son obligatorios.</h6>
                        </div>--%>
                        <div class="col-md-12 align-self-center" style="margin-left: auto; margin-right: auto">
                            <asp:TextBox runat="server" Enabled="false" Text="Ingrese nuevo modelo." CssClass="form-control" ID="txtAlerta1" Visible="false" Style="background-color: red; color: white; text-align: center;" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <div class="modal-footer col-12">
                            <asp:Button runat="server" ID="btnModalCerrarModeloATM" OnClick="btnModalCerrarModeloATM_Click" CssClass="btn btn-secundary mr-2" Text="Cancelar" />
                                <asp:Button runat="server" ID="btnModalEnviarModeloATM" OnClick="btnModalEnviarModeloATM_Click" CssClass="btn btn-success mr-2" Text="Modificar" />
                           
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
            <!-- /.modal-content -->
        </div>
        <!--/.modal-dialog -->
    </div>

    <!--MODAL NUEVA MODELOATM -->
    <div class="modal fade bs-example-modal-lg" id="modalModeloATM" tabindex="-1" data-backdrop="static" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myLargeModalLabel1">Nuevo tipo de ATM</h4>
                </div>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <br />
                        <div class="row col-12">
                            <asp:Label runat="server" BorderStyle="None" class="col form-control col-6"><strong>Nombre de tipo ATM: </strong></asp:Label>
                            <asp:TextBox runat="server" ID="txtNewModeloATM" CssClass="form-control col-6"></asp:TextBox>
                        </div><br />
                      <%--  <div class="col-md-8 align-self-center" style="margin-left: auto; margin-right: auto">
                            <br />
                            <h6 runat="server" visible="false" id="H5Alerta2" class="text-danger col-12" style="text-align: center;">Los campos con(*) son obligatorios.</h6>
                        </div>--%>
                        <div class="col-md-12 align-self-center" style="margin-left: auto; margin-right: auto">
                            <asp:TextBox runat="server" Enabled="false" Text="Ingrese nuevo modelo." Visible="false" ID="txtAlerta2" CssClass="form-control" Style="background-color: red; color: white; text-align: center;" />
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <div class="modal-footer col-12">
                            <asp:Button runat="server" ID="btnModalCerrarNueviModeloATM" OnClick="btnModalCerrarNueviModeloATM_Click" CssClass="btn btn-secundary mr-2" Text="Cancelar" />
                                <asp:Button runat="server" ID="btnModalNueviModeloATM" OnClick="btnModalNueviModeloATM_Click" CssClass="btn btn-success mr-2" Text="Agregar" />
                            
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
            <!-- /.modal-content -->
        </div>
        <!--/.modal-dialog -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
