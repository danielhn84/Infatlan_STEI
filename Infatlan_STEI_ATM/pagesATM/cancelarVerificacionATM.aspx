﻿<%@ Page Title="" Language="C#" MasterPageFile="~/mainATM.Master" AutoEventWireup="true" CodeBehind="cancelarVerificacionATM.aspx.cs" Inherits="Infatlan_STEI_ATM.pagesATM.cancelarVerificacionATM" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <!--PARA LLAMAR MODAL-->
    <script type="text/javascript">
        function openModal() { $('#modalcancelarATM').modal('show'); }
        function openModal2() { $('#modalcancelar2ATM').modal('show'); }
    </script>
     <!--PARA CERRAR MODAL-->
    <script type="text/javascript">
        function closeModal() { $('#modalcancelarATM').modal('hide'); }
        function closeModal2() { $('#modalcancelar2ATM').modal('hide'); }
    </script>

    <link href="/css/GridStyle.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    
     <div class="row page-titles">
        <div class="col-md-5 align-self-center">
            <h4 class="text-themecolor"> Ingresar nuevo motivo de cancelación</h4>
        </div>
        <div class="col-md-7 align-self-center text-right">
            <div class="d-flex justify-content-end align-items-center">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Inicio</a></li>
                    <li class="breadcrumb-item active"> Ingresar nuevo motivo de cancelación</li>
                </ol>
               
            </div> 
        </div>
    </div>
    <!--/ENCABEZADO-->

     <div class="card" >
             <br />
    <div class="row col-12" style="margin-left:10px; margin-left:10px;">
            <h3 class="text-themecolor" style="color: #808080;"><i class="fa fa-save"></i> Ingresar nuevo motivo de cancelación</h3>
        </div>
         <br />
         <hr />
         <br />
                                           
                                       <!--DATAGRID-->
        <asp:UpdatePanel ID="UpdateDivBusquedas" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
               <div class="row col-12">
               <label class="col-3 col-form-label">Motivos de cancelación creados</label>
                   <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                       <ContentTemplate>
                           <asp:Button runat="server" ID="btnCancelarVerif" OnClick="btnCancelarVerif_Click" CssClass="btn btn-info mr-2" Text="Nuevo" />
                       </ContentTemplate>
                   </asp:UpdatePanel>
                </div>
                 <br />
         <hr />
         <br />
                <div class="row">
                    <div class="col-12 grid-margin stretch-card">
                        <div class="card" id="212">
                            <div class="card-body">
                                <div class="row">
                                    <div class="table-responsive">
                                        <asp:UpdatePanel ID="UpdateGridView" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:GridView ID="GVBusqueda" runat="server"
                                                    CssClass="table table-bordered"
                                                    PagerStyle-CssClass="pgr"
                                                    HeaderStyle-CssClass="table"  HeaderStyle-HorizontalAlign="center"
                                                    RowStyle-CssClass="rows"
                                                    AutoGenerateColumns="false"
                                                    AllowPaging="true"
                                                    GridLines="None"
                                                    PageSize="10" OnRowCommand="GVBusqueda_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Selección" ItemStyle-HorizontalAlign="center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnmarca" runat="server" CssClass="btn btn-info mr-2" Text="Modificar" CommandArgument='<%# Eval("idCancelMant") %>' CommandName="Codigo"></asp:LinkButton>
                                                                <%--<asp:Button ID="BtnUsuarioPassword" runat="server" Text="De baja" CssClass="btn btn-block btn-outline-danger" CommandArgument='<%# Eval("codATM") %>' CommandName="Baja" />--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>                                                     
                                                        <asp:BoundField DataField="idCancelMant" HeaderText="Código motivo cancelar"  ItemStyle-HorizontalAlign="center" />
                                                        <asp:BoundField DataField="nombreCancelar" HeaderText="Motivo cancelar" ItemStyle-HorizontalAlign="center"/>
                                        
                                                            
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
        <!--/DATAGRID-->
         <br />
         <hr />
         <br />
          <!--MODAL GUARDAR tipoATM -->
        <div class="modal bs-example-modal-lg" id="modalcancelarATM" tabindex="-1" data-backdrop="static" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" style="display: none;">
            <div class="modal-dialog modal-xl">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title" id="myLargeModalLabel">¿Seguro que modificará la marca?</h4>                       
                    </div>
                     <asp:UpdatePanel ID="UpdatePanel1" Runat="server">
            <ContentTemplate>
                    <div class="row col-12">
                        <asp:label runat="server" BorderStyle="None" class="col form-control col-6"><strong>Nueva motivo de cancelación: </strong></asp:label>
                        <asp:label runat="server" BorderStyle="None" ID="lbcodmotivoATM" class="col form-control col-6"></asp:label>
                    </div>
                 <div class="row col-12">
                        <asp:label runat="server" BorderStyle="None" class="col form-control col-6"><strong>Motivo cancelación: </strong></asp:label>
                        <asp:label runat="server" BorderStyle="None" ID="lbNombremotivoATM" class="col form-control col-12"></asp:label>
                    </div>   
                <div class="row col-12">
                        <asp:label runat="server" BorderStyle="None" class="col form-control col-6"><strong>Nueva motivo cancelar: </strong></asp:label>
                        <asp:TextBox runat="server" ID="txtModalNewmotivoATM" CssClass="form-control col-12" TextMode="MultiLine" Rows="2"></asp:TextBox>
                    </div>   
                <div class="col-md-8 align-self-center" style="margin-left:auto; margin-right:auto">
                    <asp:label runat="server" style="color:red;" Visible="false"  borderstyle="none" ID="lbmotivo1" CssClass="col form-control" ><strong></strong></asp:label>
                    </div>
                </ContentTemplate>
                         </asp:UpdatePanel>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <div class="modal-footer col-12">
                                <div class="row col-3">
                                <asp:Button runat="server" ID="btnModalEnviarCancelarATM" OnClick="btnModalEnviarCancelarATM_Click" CssClass="btn btn-success mr-2" Text="Modificar" />
                                </div>
                                 <div class="row col-3">
                                <asp:Button runat="server" ID="btnModalCerrarCancelarATM" OnClick="btnModalCerrarCancelarATM_Click"  CssClass="btn btn-danger mr-2" Text="Cancelar" />
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

          <!--MODAL NUEVA MODELOATM -->
        <div class="modal bs-example-modal-lg" id="modalcancelar2ATM" tabindex="-1" data-backdrop="static" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" style="display: none;">
            <div class="modal-dialog modal-xl">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title" id="myLargeModalLabel1">Nueva marca</h4>                       
                    </div>
                     <asp:UpdatePanel ID="UpdatePanel3" Runat="server">
            <ContentTemplate>                   
                 <div class="row col-12">
                        <asp:label runat="server" BorderStyle="None" class="col form-control col-6"><strong>Nueva motivo de cancelación: </strong></asp:label>
                        <asp:TextBox runat="server" ID="txtNewMotivoCancelATM" CssClass="form-control col-12"  TextMode="MultiLine" Rows="2"></asp:TextBox>
                    </div>     
                <div class="col-md-8 align-self-center" style="margin-left:auto; margin-right:auto">
                    <asp:label runat="server" style="color:red;" Visible="false"  borderstyle="none" ID="lbmotivo2" CssClass="col form-control" ><strong></strong></asp:label>
                    </div>
                </ContentTemplate>
                         </asp:UpdatePanel>
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <div class="modal-footer col-12">
                                <div class="row col-3">
                                <asp:Button runat="server" ID="btnModalCancelarMotivoATM" OnClick="btnModalCancelarMotivoATM_Click"  CssClass="btn btn-success mr-2" Text="Agregar" />
                                </div>
                                 <div class="row col-3">
                                <asp:Button runat="server" ID="btnModalCerrarCancelarMotivoATM" OnClick="btnModalCerrarCancelarMotivoATM_Click" CssClass="btn btn-danger mr-2" Text="Cancelar" />
                                </div>
                                </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
                <!-- /.modal-content -->
            </div>
            <!--/.modal-dialog -->
        </div>
        <!-- /MODAL NUEVA MODELOATM -->


         </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
