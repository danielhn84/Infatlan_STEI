<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="material.aspx.cs" Inherits="Infatlan_STEI_ATM.pages.material.material" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
     <!--PARA LLAMAR MODAL-->
    <script type="text/javascript">
        function openModal() { $('#modalEquipo').modal('show'); }
        function openModal2() { $('#modalEquipoR').modal('show'); }
    </script>
    <!--PARA CERRAR MODAL-->
    <script type="text/javascript">
        function closeModal() { $('#modalEquipo').modal('hide'); }
        function closeModal2() { $('#modalEquipoR').modal('hide'); }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div class="row page-titles">
        <div class="col-md-5 align-self-center">
            <h4 class="text-themecolor">Solicitud de materiales</h4>
        </div>
        <div class="col-md-7 align-self-center text-right">
            <div class="d-flex justify-content-end align-items-center">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Inicio</a></li>
                    <li class="breadcrumb-item active">Solicitud de materiales</li>
                </ol>
            </div>
        </div>
    </div>
    <div class="card">
        <br />
        <div class="row col-12" style="margin-left: 10px; margin-left: 10px;">
            <h3 class="text-themecolor" style="color: #808080;"><i class="fa fa-book"></i>Solicitar materiales</h3>
        </div>
         <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="UPtotalATM">
            <ContentTemplate>
                <!--PRIMERA FILA-->
                <div class="row col-12">
                    <div class="row col-4">
                        <label class="col-form-label col-12">Nombre</label>
                        <div class="col-12">
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtNom" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row col-4">
                        <label class="col-form-label col-12">Nombre de ATM</label>
                        <div class="col-12">
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtnombreATM" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row col-4">
                        <label class="col-form-label col-12">Sucursal de ATM</label>
                        <div class="col-12">
                           <asp:TextBox runat="server" CssClass="form-control" ID="txtSucursal" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <!--/PRIMERA FILA-->
                <br />
                <br />
                <%--SEGUNDA FILA--%>
                <div class="col-md-12 align-self-center" style="margin-left: auto; margin-right: auto">
                 <table class="tablesaw table-bordered table-hover table no-wrap" data-tablesaw-mode="swipe"
                        data-tablesaw-sortable data-tablesaw-sortable-switch data-tablesaw-minimap
                        data-tablesaw-mode-switch>
                        <thead>
                            <tr>
                                <th scope="col" style="background-color:#5D6D7E;color:#D5DBDB;" data-tablesaw-sortable-col data-tablesaw-priority="persist" class="border">Artículo</th>                              
                                <th scope="col" style="background-color:#5D6D7E;color:#D5DBDB;" data-tablesaw-sortable-col data-tablesaw-priority="2" class="border">Marca </th>
                                <th scope="col" style="background-color:#5D6D7E;color:#D5DBDB;" data-tablesaw-sortable-col data-tablesaw-priority="2" class="border">Cantidad</th>
                                <th scope="col" style="background-color:#5D6D7E;color:#D5DBDB;" data-tablesaw-sortable-col data-tablesaw-priority="2" class="border">Aceptar</th>


                            </tr>
                        </thead>
                        <tbody>  
                           
                            <tr>                               
                                <td><asp:DropDownList ID="DDLStock" runat="server" AutoPostBack="true" OnTextChanged="DDLStock_TextChanged" CssClass="form-control"></asp:DropDownList></td>   
                                <td><asp:TextBox ID="txtmarca" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox></td>                                 
                                <td><asp:TextBox ID="txtcantidad" runat="server" CssClass="form-control" TextMode="Number" AutoPostBack="true" OnTextChanged="txtcantidad_TextChanged"></asp:TextBox></td> 
                                <td> <asp:LinkButton runat="server" ID="btnVerifATM" OnClick="btnVerifATM_Click" Text="" CssClass="btn btn-success  ti-check-box mr-2"></asp:LinkButton></td>
                            </tr>                              
                           
                        </tbody>
                    </table>
                    </div>
                <%--/SEGUNDA FILA--%>
                
                <asp:UpdatePanel runat="server" ID="UPMateriales" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="col-md-12 align-self-center" style="margin-left: auto; margin-right: auto">
                                        <div class="table-responsive">
                                            <!--<table id="bootstrap-data-table" class="table table-striped table-bordered"> -->
                                            <asp:GridView ID="GVNewMateriales" runat="server"
                                                CssClass="table table-bordered"
                                                PagerStyle-CssClass="pgr"
                                                HeaderStyle-CssClass="table"
                                                RowStyle-CssClass="rows"
                                                AutoGenerateColumns="false"
                                                AllowPaging="true"
                                                GridLines="None"
                                                HeaderStyle-HorizontalAlign="center"
                                                PageSize="10" OnRowCommand="GVNewMateriales_RowCommand"
                                                Style="margin: 30px 0px 20px 0px">
                                                <Columns>
                                                   <asp:BoundField DataField="idMantenimiento" HeaderText="Código" Visible="false" ItemStyle-HorizontalAlign="center" />
                                                   <asp:BoundField DataField="idStock" HeaderText="Stock" Visible="false" ItemStyle-HorizontalAlign="center" />
                                                   <asp:BoundField DataField="nombre" HeaderText="Material" ItemStyle-HorizontalAlign="center" />
                                                   <asp:BoundField DataField="marca" HeaderText="Marca" ItemStyle-HorizontalAlign="center" />
                                                   <asp:BoundField DataField="cantidad" HeaderText="Cantidad" ItemStyle-HorizontalAlign="center" />
                                                    <asp:TemplateField HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>                                                           
                                                            <asp:LinkButton ID="Btnseleccionar" Enabled="true" runat="server" Text="" class="btn btn-danger mr-2" CommandArgument='<%# Eval("idStock") %>' CommandName="eliminar"><i class="icon-trash"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                
                 <%--CUARTA FILA--%>
                 <div class="col-12">
                   <label class="col-form-label col-12" style="color:red;" runat="server" id="LBComentario"></label>
                 </div>
                <hr />
                <div class="col-md-12 align-self-center" style="margin-left: auto; margin-right: auto">
                    <div class="row col-12">
                        <label class="col-form-label col-12" runat="server" id="LBMotivo">Motivo por el que solicita equipo</label>
                        <div class="col-12">
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtmotivo" TextMode="MultiLine" Rows="3"></asp:TextBox>
                        </div>
                    </div>
                    </div>
                 <%--/CUARTA FILA--%>
                <br />
                <br />
                <%--QUINTA FILA--%>
                 <asp:UpdatePanel ID="UPEnviarVerif" runat="server">
                    <ContentTemplate>
                        <div class="col-md-12 align-self-center" style="margin-left: auto; margin-right: auto">
                            <div class=" row col-12">
                            <div class="col-md-6 align-self-center" style="margin-left: auto; margin-right: auto">
                            <asp:Button runat="server" ID="btnguardar" OnClick="btnguardar_Click"  CssClass="btn btn-rounded btn-block btn-outline-success" Text="Enviar" />
                            </div>
                            <div class="col-6" runat="server" id="DIVbtnRechazo" visible="false">
                            <asp:Button runat="server" ID="btnRechazar" OnClick="btnRechazar_Click" CssClass="btn btn-rounded btn-block btn-outline-danger" Text="Devolver" />
                            </div>
                            </div>
                       </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <%--/QUINTA FILA--%>
                <br />
                <br />
                </ContentTemplate>
             </asp:UpdatePanel>

        </div>

     <!--MODAL MATERIALES -->
        <div class="modal bs-example-modal-lg" id="modalEquipo" tabindex="-1" data-backdrop="static" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" style="display: none;">
            <div class="modal-dialog modal-xl">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title" id="myLargeModalLabel1">¿Desea enviar la solicitud?</h4>
                    </div>
                   
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <div class="modal-footer col-12">
                                <div class="row col-3">
                                    <asp:Button runat="server" ID="btnModalEnviar" OnClick="btnModalEnviar_Click" CssClass="btn btn-success mr-2" Text="Enviar" />
                                </div>
                                <div class="row col-3">
                                    <asp:Button runat="server" ID="btnModalCerrar" OnClick="btnModalCerrar_Click" CssClass="btn btn-danger mr-2" Text="Cancelar" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
                <!-- /.modal-content -->
            </div>
            <!--/.modal-dialog -->
        </div>
        <!-- /MODAL MATERIALES -->

     <!--MODAL RECHAZADO -->
        <div class="modal bs-example-modal-lg" id="modalEquipoR" tabindex="-1" data-backdrop="static" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" style="display: none;">
            <div class="modal-dialog modal-xl">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title" id="myLargeModalLabel2">¿Desea devolver la solicitud?</h4>
                    </div>
                   
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="modal-footer col-12">
                                <div class="row col-3">
                                    <asp:Button runat="server" ID="btnModarDevolver" OnClick="btnModarDevolver_Click"  CssClass="btn btn-success mr-2" Text="Enviar" />
                                </div>
                                <div class="row col-3">
                                    <asp:Button runat="server" ID="btnModalCerrarRechazo" OnClick="btnModalCerrarRechazo_Click"  CssClass="btn btn-danger mr-2" Text="Cancelar" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
                <!-- /.modal-content -->
            </div>
            <!--/.modal-dialog -->
        </div>
        <!-- /MODAL RECHAZADO -->

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
