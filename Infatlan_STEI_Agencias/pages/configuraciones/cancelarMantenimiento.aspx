<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="cancelarMantenimiento.aspx.cs" Inherits="Infatlan_STEI_Agencias.pages.configuraciones.cancelarMantenimiento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <script type="text/javascript">
        function openModal() { $('#modalCancelar').modal('show'); }
        function closeModal() { $('#modalCancelar').modal('hide'); }
        function openVerMotivo() { $('#modalVerCancelar').modal('show'); }
        function closeVerMotivo() { $('#modalVerCancelar').modal('hide'); }
    </script>
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
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Agencias</a></li>
                    <li class="breadcrumb-item active">Cancelar Mantenimiento</li>
                </ol>
            </div>
        </div>
    </div>

    <asp:UpdatePanel runat="server" ID="UpdatePanel">
        <ContentTemplate>
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Cancelar mantenimiento</h4>
                    <h6 class="card-subtitle">Cancelar mantenimientos ya ingresados o atrasados.</h6>
                    <br />

                    <div class="col-lg-12">
                        <div class="card">
                             <div class="card-header">
                                    <h4>Mantenimientos de Agencias</h4>
                                </div>
                            <div class="card-body">
                             
                                   <div class="custom-tab">

                                    <nav>
                                        <div class="nav nav-tabs" id="nav-tab" role="tablist">
                                            <a class="nav-item nav-link active" id="custom-nav-home-tab" data-toggle="tab" href="#custom-nav-home" role="tab" aria-controls="custom-nav-home" aria-selected="true">Cancelar Mantenimiento</a>
                                            <a class="nav-item nav-link" id="custom-nav-profile-tab" data-toggle="tab" href="#custom-nav-profile" role="tab" aria-controls="custom-nav-profile" aria-selected="false">Mantenimientos Cancelados</a>
                                        </div>
                                    </nav>
                                    <div class="tab-content pl-3 pt-2" id="nav-tabContent">
                                        <div class="tab-pane fade show active" id="custom-nav-home" role="tabpanel" aria-labelledby="custom-nav-home-tab">
                                            <br />
                                            <div class="col-md-6">
                                                <div class="form-group row">
                                                    <label class="col-sm-3 col-form-label">Buscar</label>
                                                    <div class="col-sm-9">
                                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                            <ContentTemplate>
                                                                <asp:TextBox ID="TxBuscar" OnTextChanged="TxBuscar_TextChanged" runat="server" placeholder="ingrese Agencia - Presione afuera para proceder" class="form-control" AutoPostBack="true"></asp:TextBox>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-12 align-self-center" style="margin-left: auto; margin-right: auto">
                                                <div class="table-responsive">
                                                    <asp:UpdatePanel runat="server">
                                                        <ContentTemplate>
                                                        <asp:GridView ID="GVCancelar" runat="server"
                                                        CssClass="table table-bordered"
                                                        PagerStyle-CssClass="pgr"
                                                        HeaderStyle-CssClass="table"
                                                        RowStyle-CssClass="rows"
                                                        AutoGenerateColumns="false"
                                                        AllowPaging="true"
                                                        GridLines="None"
                                                        HeaderStyle-HorizontalAlign="center"
                                                        PageSize="10" OnPageIndexChanging="GVCancelar_PageIndexChanging"
                                                        OnRowCommand="GVCancelar_RowCommand"
                                                        Style="margin: 30px 0px 20px 0px">
                                                        <Columns>
                                                            <asp:BoundField DataField="ID" HeaderText="Código" Visible="false" ItemStyle-HorizontalAlign="center" />
                                                            <asp:BoundField DataField="Agencia" HeaderText="Agencia" ItemStyle-HorizontalAlign="center" />                                                            
                                                            <asp:BoundField DataField="Fecha" HeaderText="Fecha Mantenimiento" HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="center" />
                                                            <asp:BoundField DataField="Avance" HeaderText="Avance" ItemStyle-HorizontalAlign="center" />
                                                            <asp:TemplateField HeaderStyle-Width="60px">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" ID="btnCancelar" Text="" CssClass="btn btn-info ti-pencil-alt mr-2" CommandArgument='<%# Eval("ID") %>' CommandName="cancelar"></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="10%" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                   
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade" id="custom-nav-profile" role="tabpanel" aria-labelledby="custom-nav-profile-tab">
                                            <br />
                                            <div class="col-md-6">
                                                <div class="form-group row">
                                                    <label class="col-sm-3 col-form-label">Buscar</label>
                                                    <div class="col-sm-9">
                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                            <ContentTemplate>
                                                                <asp:TextBox ID="txtBuscarCancelado" OnTextChanged="txtBuscarCancelado_TextChanged" runat="server" placeholder="ingrese Agencia - Presione afuera para proceder" class="form-control" AutoPostBack="true"></asp:TextBox>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-12 align-self-center" style="margin-left: auto; margin-right: auto">
                                                <div class="table-responsive">
                                                    <asp:UpdatePanel runat="server">
                                                        <ContentTemplate>
                                                        <asp:GridView ID="GVMantenimientoCancelado" runat="server"
                                                        CssClass="table table-bordered"
                                                        PagerStyle-CssClass="pgr"
                                                        HeaderStyle-CssClass="table"
                                                        RowStyle-CssClass="rows"
                                                        AutoGenerateColumns="false"
                                                        AllowPaging="true"
                                                        GridLines="None"
                                                        HeaderStyle-HorizontalAlign="center"
                                                        PageSize="10" OnPageIndexChanging="GVMantenimientoCancelado_PageIndexChanging"
                                                        OnRowCommand="GVMantenimientoCancelado_RowCommand"
                                                        Style="margin: 30px 0px 20px 0px">
                                                        <Columns>
                                                            <asp:BoundField DataField="ID" HeaderText="Código" Visible="false" ItemStyle-HorizontalAlign="center" />
                                                            <asp:BoundField DataField="Agencia" HeaderText="Agencia" ItemStyle-HorizontalAlign="center" />                                                            
                                                            <asp:BoundField DataField="Fecha" HeaderText="Fecha Mantenimiento" HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="center" />
                                                            <asp:BoundField DataField="Avance" HeaderText="Avance" ItemStyle-HorizontalAlign="center" />
                                                            <asp:TemplateField HeaderStyle-Width="60px">
                                                                <ItemTemplate>
                                                                    
                                                                                  <asp:LinkButton runat="server" ID="btnCancelar" Text="" CssClass="btn btn-info ti-pencil-alt mr-2" CommandArgument='<%# Eval("ID") %>' CommandName="ver"></asp:LinkButton>
                                                                       
                                                                </ItemTemplate>
                                                                <ItemStyle Width="10%" />
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
                        </div>
                    </div>
                    <!-- /# column -->
                </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

     <!-- MODAL CANCELAR -->
    <div class="modal fade bs-example-modal-lg" id="modalCancelar" tabindex="-1" data-backdrop="static" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <h4 class="modal-title" runat="server" id="LBTitleModal"></h4>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="row col-12">
                            <asp:Label runat="server" BorderStyle="None" class="col form-control col-3"><strong>Agencia: </strong></asp:Label>                          
                                <asp:Label runat="server" ID="LbAgencia" CssClass="col-form-label col-9"/>
                        </div>   
                        <div class="row col-12">
                            <asp:Label runat="server" BorderStyle="none" class="col form-control col-12"><strong>Motivo por el que cancela mantenimiento: </strong></asp:Label>
                            <div class="col-md-12 align-self-center" style="margin-left: auto; margin-right: auto">
                                <asp:TextBox runat="server" ID="txtMotivo" CssClass="form-control col-12" TextMode="MultiLine" Rows="4" />
                            </div>
                        </div><br />
                         <div class="col-md-12 align-self-center" style="margin-left: auto; margin-right: auto">
                            <asp:TextBox runat="server" Enabled="false" Text="Ingrese motivo por el que cancela mantenimiento." CssClass="form-control" ID="txtAlerta" Visible="false" Style="background-color: red; color: white; text-align: center;" />
                        </div><br />
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <div class="modal-footer col-12">
                           <asp:Button runat="server" ID="btnModalCerrar" OnClick="btnModalCerrar_Click" CssClass="btn btn-secundary  mr-3" Text="Cancelar" />
                                <asp:Button runat="server" ID="btnModalCncelar" OnClick="btnModalCncelar_Click" CssClass="btn btn-success  mr-3" Text="Enviar" />
                            
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
            <!-- /.modal-content -->
        </div>
        <!--/.modal-dialog -->
    </div>

    <!-- MODAL VER MOTIVO DE CANCELAR -->
    <div class="modal fade bs-example-modal-lg" id="modalVerCancelar" tabindex="-1" data-backdrop="static" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <h4 class="modal-title" runat="server" id="H1VerMotivo"></h4>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    
                </div>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="row col-12">
                            <asp:Label runat="server" BorderStyle="None" class="col form-control col-3"><strong>Agencia: </strong></asp:Label>                          
                                <asp:Label runat="server" ID="LbAgenciaCancelado" CssClass="col-form-label col-9"/>
                        </div>   
                        <div class="row col-12">
                            <asp:Label runat="server" BorderStyle="none" class="col form-control col-12"><strong>Motivo por el que cancela mantenimiento: </strong></asp:Label>
                            <div class="col-md-12 align-self-center" style="margin-left: auto; margin-right: auto">
                                <asp:TextBox runat="server" ID="txtMotivoCancelado" Enabled="false" CssClass="form-control col-12" TextMode="MultiLine" Rows="4" />
                            </div>
                        </div><br />                       
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                        <div class="modal-footer col-12">
                           <asp:Button runat="server" ID="btnCerrar" OnClick="btnCerrar_Click" CssClass="btn btn-secundary  mr-3" Text="Listo" />                            
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
