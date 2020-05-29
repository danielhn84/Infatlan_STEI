<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="tipo.aspx.cs" Inherits="Infatlan_STEI_ATM.pages.ATM.tipo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <!--PARA LLAMAR MODAL-->
    <script type="text/javascript">
        function openModal() { $('#modalcreartipoATM').modal('show'); }
        function openModal2() { $('#modaltipoATM').modal('show'); }
    </script>
    <!--PARA CERRAR MODAL-->
    <script type="text/javascript">
        function closeModal() { $('#modalcreartipoATM').modal('hide'); }
        function closeModal2() { $('#modaltipoATM').modal('hide'); }
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
            <h3 class="text-themecolor col-12">Tipo de ATM</h3>
            <h6 class="text-themecolor col-12">Ingresar nuevo tipo de ATM</h6>
        </div>
        <div class="col-md-7 align-self-center text-right">
            <div class="d-flex justify-content-end align-items-center">
            </div>
        </div>
    </div>
    <!--/ENCABEZADO-->

    <div class="card">
        <br />
       
        <div class=" col-12 align-self-center" style="margin-left: auto; margin-right: auto">
            <div class="row">
                <div class="col-12 grid-margin stretch-card">
                    <div class="card">
                        <div class="card-body">
                            <asp:UpdatePanel ID="UpdateDivBusquedas" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="row col-12">
                                        <label class="col-3 col-form-label">Tipos de ATM creados</label>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel5">
                                            <ContentTemplate>
                                                <asp:LinkButton runat="server" ID="btnguardartipoATM" OnClick="btnguardartipoATM_Click" class="btn btn-info icon-plus mr-2"></asp:LinkButton>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                   
                                    <div class="row">
                                        <div class="col-12 grid-margin stretch-card">
                                            <div class="card" id="212">
                                                <div class="card-body">
                                                    <div class="row">
                                                        <div class="table-responsive">
                                                            <asp:UpdatePanel ID="UpdateGridView" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <asp:GridView ID="GVBusqueda" runat="server"
                                                                        CssClass=" table table-bordered " HeaderStyle-HorizontalAlign="center"
                                                                        PagerStyle-CssClass="pgr"
                                                                        HeaderStyle-CssClass="table"
                                                                        RowStyle-CssClass="rows"
                                                                        AutoGenerateColumns="false"
                                                                        AllowPaging="true"
                                                                        GridLines="None" OnPageIndexChanging="GVBusqueda_PageIndexChanging"
                                                                        PageSize="10" OnRowCommand="GVBusqueda_RowCommand">
                                                                        <Columns>

                                                                            <asp:BoundField DataField="idTipoATM" HeaderText="Código de tipo ATM" ItemStyle-HorizontalAlign="center" />
                                                                            <asp:BoundField DataField="nombreTipoATM" HeaderText="Tipo de ATM" ItemStyle-HorizontalAlign="center" />
                                                                            <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="center">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="btnbajaATM" runat="server" class="btn btn-info ti-pencil-alt mr-2" Text="" CommandArgument='<%# Eval("idTipoATM") %>' CommandName="Codigo"></asp:LinkButton>
                                                                                    <%--<asp:Button ID="BtnUsuarioPassword" runat="server" Text="De baja" CssClass="btn btn-block btn-outline-danger" CommandArgument='<%# Eval("codATM") %>' CommandName="Baja" />--%>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
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
        <!--/DATAGRID-->

       
        <!--MODAL GUARDAR tipoATM -->
        <div class="modal bs-example-modal-lg" id="modalcreartipoATM" tabindex="-1" data-backdrop="static" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" style="display: none;">
            <div class="modal-dialog modal-xl">
                <div class="modal-content">
                    <div class="modal-header" style="background-color:darkslategrey; color:white;">
                        <h4 class="modal-title" id="myLargeModalLabel">¿Seguro que modificará tipo de ATM?</h4>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <br />
                            <div class="row col-12">
                                <asp:Label runat="server" BorderStyle="None" class="col form-control col-6"><strong>Código de tipo ATM: </strong></asp:Label>
                                <asp:Label runat="server" BorderStyle="None" ID="lbcodtipoATM" class="col form-control col-6"></asp:Label>
                            </div>
                            <div class="row col-12">
                                <asp:Label runat="server" BorderStyle="None" class="col form-control col-6"><strong>Nombre de tipo ATM: </strong></asp:Label>
                                <asp:Label runat="server" BorderStyle="None" ID="lbNombretipoATM" class="col form-control col-6"></asp:Label>
                            </div>
                            <div class="row col-12">
                                <asp:Label runat="server" BorderStyle="None" class="col form-control col-6"><strong>*Nuevo nombre de tipo ATM: </strong></asp:Label>
                                <asp:TextBox runat="server" ID="txtModalNewTipoATM" CssClass="form-control col-6"></asp:TextBox>
                            </div>
                           <div class="col-md-8 align-self-center" style="margin-left: auto; margin-right: auto">
                                <br />
                                <h6 runat="server" visible="false" id="H5Alerta1" class="text-danger col-12" style="text-align:center;">Los campos con(*) son obligatorios.</h6>
                            </div>
                             <div class="col-md-12 align-self-center" style="margin-left: auto; margin-right: auto">
                               <asp:TextBox runat="server" Enabled="false" Text="Ingrese nuevo tipo ATM." Visible="false" ID="txtAlerta1" CssClass="form-control" style="background-color:red; color:white; text-align:center;"/>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <div class="modal-footer col-12">
                                <div class="row col-3">
                                    <asp:Button runat="server" ID="btnModalEnviartipoATM" OnClick="btnModalEnviartipoATM_Click" CssClass="btn btn-dark mr-2" Text="Modificar" />
                                </div>
                                <div class="row col-3">
                                    <asp:Button runat="server" ID="btnModalCerrartipoATM" OnClick="btnModalCerrartipoATM_Click" CssClass="btn btn-secundary mr-2" Text="Cancelar" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
                <!-- /.modal-content -->
            </div>
            <!--/.modal-dialog -->
        </div>
        <!-- /MODAL GUARDAR tipoATM -->

        <!--MODAL BUEVA tipoATM -->
        <div class="modal bs-example-modal-lg" id="modaltipoATM" tabindex="-1" data-backdrop="static" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" style="display: none;">
            <div class="modal-dialog modal-xl">
                <div class="modal-content">
                    <div class="modal-header" style="background-color:darkslategrey; color:white;">
                        <h4 class="modal-title" id="myLargeModalLabel1">Nuevo tipo de ATM</h4>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <br />
                            <div class="row col-12">
                                <asp:Label runat="server" BorderStyle="None" class="col form-control col-6"><strong>*Nombre de tipo ATM: </strong></asp:Label>
                                <asp:TextBox runat="server" ID="txtNewTipoATM" CssClass="form-control col-6 "></asp:TextBox>
                            </div>
                           <div class="col-md-8 align-self-center" style="margin-left: auto; margin-right: auto">
                                <br />
                                <h6 runat="server" visible="false" id="H5Alerta2" class="text-danger col-12" style="text-align:center;">Los campos con(*) son obligatorios.</h6>
                            </div>
                             <div class="col-md-12 align-self-center" style="margin-left: auto; margin-right: auto">
                               <asp:TextBox runat="server" Enabled="false" Text="Ingrese nuevo tipo de ATM." Visible="false" ID="txtAlerta2" CssClass="form-control" style="background-color:red; color:white; text-align:center;"/>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <div class="modal-footer col-12">
                                <div class="row col-3">
                                    <asp:Button runat="server" ID="btnModalNueviTipoATM" OnClick="btnModalNueviTipoATM_Click" CssClass="btn btn-dark mr-2" Text="Agregar" />
                                </div>
                                <div class="row col-3">
                                    <asp:Button runat="server" ID="btnModalCerrarNueviTipoATM" OnClick="btnModalCerrarNueviTipoATM_Click1" CssClass="btn btn-secundary mr-2" Text="Cancelar" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
                <!-- /.modal-content -->
            </div>
            <!--/.modal-dialog -->
        </div>
        <!-- /MODAL NUEVA tipoATM -->




    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
