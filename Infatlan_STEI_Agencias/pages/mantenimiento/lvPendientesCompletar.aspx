<%@ Page Title="" Language="C#" MasterPageFile="~/mainAgencia.Master" AutoEventWireup="true" CodeBehind="lvPendientesCompletar.aspx.cs" Inherits="Infatlan_STEI_Agencias.pages.LvCompletar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">

    
        <script type="text/javascript">
            function openModalCancelarLV() { $('#ModalCancelacionLV').modal('show'); }
            function closeModalCancelarLV() { $('#ModalCancelacionLV').modal('hide'); }


    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">

    <div class="row page-titles">
        <div class="col-md-7 align-self-center">
            <h2 class="text-themecolor">Listas de Verificación Pendientes Completar</h2>
            <div class="mr-md-3 mr-xl-5">
                <%-- <h2>Creación de Notificación</h2>--%>
                <p class="mb-md-0">Soporte Técnico y Comunicaciones</p>
            </div>
        </div>

        
    </div>

    <div class="card">
        <div class="card-body">
            <h4 class="card-title">LV Pendientes</h4>
            <p>Listas de verificación pendientes de completar que estan asignadas a su persona.</p>
            <div class="col-md-12">
                <div class="form-group row">
                    <div class="col-sm-12">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>

                                <div class="row p-t-20">
                                    <div class="col-md-1">
                                        <label class="control-label   text-danger">*</label><label class="control-label">Buscar:</label></label>                                      
                                    </div>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="TxBuscarAgencia" runat="server" placeholder="Búsqueda por  Agencia o Codigo, luego presione Enter" class="form-control" AutoPostBack="true" OnTextChanged="TxBuscarAgencia_TextChanged"></asp:TextBox>
                                    </div>

                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>

        <div class="row col-12">
            <div class="col-12 grid-margin stretch-card">
                <div class="table-responsive">
                    <asp:UpdatePanel runat="server" ID="UPListaVerificacion">
                        <ContentTemplate>
                            <asp:GridView ID="GVListaVerificacion" runat="server"
                                CssClass="table table-bordered"
                                PagerStyle-CssClass="pgr"
                                HeaderStyle-CssClass="table" HeaderStyle-HorizontalAlign="center"
                                RowStyle-CssClass="rows"
                                AutoGenerateColumns="false"
                                AllowPaging="true"
                                GridLines="None" OnPageIndexChanging="GVListaVerificacion_PageIndexChanging"
                                PageSize="10" OnRowCommand="GVListaVerificacion_RowCommand">

                                <Columns>
                                    <asp:TemplateField HeaderText="Acción" ItemStyle-HorizontalAlign="center" >
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LBAprobar" runat="server"  CommandName="Completar" CommandArgument='<%# Eval("id_Mantenimiento") %>'>
                                                       <img src="https://img.icons8.com/color/23/000000/check-file.png"/>
                                            </asp:LinkButton>

                                            <asp:LinkButton ID="LBCancelar" runat="server" CommandName="Cancelar" CommandArgument='<%# Eval("id_Mantenimiento") %>'>
                                                          <img src="https://img.icons8.com/color/23/000000/file-delete--v1.png"/>
                                            </asp:LinkButton>
                                             
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="id_Mantenimiento" HeaderText="Id"  ControlStyle-Width="10%" />
                                    <asp:BoundField DataField="fecha" HeaderText="Fecha" ControlStyle-Width="10%" />
                                    <asp:BoundField DataField="Cod_Agencia" HeaderText="Cod. Agencia" ControlStyle-Width="10%" />
                                    <asp:BoundField DataField="Lugar" HeaderText="Lugar" ControlStyle-Width="10%" />
                                    <asp:BoundField DataField="Area" HeaderText="Area" ControlStyle-Width="10%" />                                                                       
                                    <asp:BoundField DataField="sysAid" HeaderText="No. SysAid" ControlStyle-Width="10%" />
                                    <asp:BoundField DataField="Responsable" HeaderText="Responsable" ControlStyle-Width="10%" />
                                   <asp:BoundField DataField="idUsuario" HeaderText="idUsuariob" ControlStyle-Width="10%"  Visible="false" />
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>


    </div>


    <!-- Modal asegurar notificacion Modal2 -->
    <div class="modal fade" id="ModalCancelacionLV" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="width: 600px; top: 320px; left: 50%; transform: translate(-50%, -50%);">
                <div class="modal-header  bg-dark" >
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <h3 class="modal-title" style="color: white" id="exampleModalLabel" >Cancelar LV  
                                <asp:Label ID="Titulo" runat="server" Text="" Style="margin-left: auto; margin-right: auto"></asp:Label></h3>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <button type="button" class="close" style="color: white" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>

                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="form-group row">
                                 <div class="col-md-12" runat="server">
                                    <div class="form-group row">
                                        <div class="col-md-3">
                                            <label class="control-label">Id Mantenimiento:</label></label>
                                        </div>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="TxIdMantenimiento" AutoPostBack="true" runat="server"  class="form-control" ReadOnly="true"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12" runat="server">
                                    <div class="form-group row">
                                        <div class="col-md-3">
                                            <label class="control-label text-danger" runat="server" id="lbMotivo" >*</label><label class="control-label">Motivo:</label></label>                                      
                                        </div>
                                        <div class="col-sm-9">
                                            <asp:DropDownList ID="DDLMotivo" runat="server" class="form-control" OnSelectedIndexChanged="DDLMotivo_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0" Text="Seleccione motivo..."></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Personal de agencia canceló mantenimiento"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Personal de agencia no se presentó al mantenimiento"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="Técnico no se presentó/ no cumplió el horario."></asp:ListItem>
                                                <asp:ListItem Value="4" Text="Cambio Técnico Responsable"></asp:ListItem>
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                </div>

                               

                                <div class="col-md-12" runat="server">
                                    <div class="form-group row">
                                        <div class="col-md-3">
                                            <label class="control-label   text-danger" runat="server" visible="false" id="asterisco">*</label><label class="control-label" runat="server" visible="false" id="etiqueta">Nuevo Técnico:</label></label>
                                        <%--<asp:TextBox ID="TextBox1" AutoPostBack="true" runat="server" TextMode="Date" OnTextChanged="TextBox1_TextChanged" class="form-control"></asp:TextBox>--%>
                                        </div>


                                        <div class="col-sm-9">
                                            <asp:DropDownList ID="DDLNombreResponsable" runat="server" AutoPostBack="true" CssClass="form-control" Visible="false" OnSelectedIndexChanged="DDLNombreResponsable_SelectedIndexChanged"></asp:DropDownList>

                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12" runat="server">
                                    <div class="form-group row">

                                        <div class="col-md-3">
                                            <label class="control-label   text-danger">*</label><label class="control-label">Detalle motivo:</label></label>                                    
                                        </div>


                                        <div class="col-sm-9">
                                            <asp:TextBox ID="TxDetalle" class="form-control" runat="server" TextMode="MultiLine" Rows="5" OnTextChanged="TxDetalle_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <asp:UpdatePanel ID="UpdateModal" runat="server" UpdateMode="Conditional" Visible="false">
                                    <ContentTemplate>
                                        <div class="col-md-10" style="margin-left: auto; margin-right: auto" runat="server">
                                            <div class="alert alert-danger   align-content-md-center" style="align-self: auto">
                                                <h3 class="text-danger" style="text-align: center"><i class="fa fa-exclamation-triangle"></i></h3>
                                                <asp:Label ID="LbMensajeModalErrorReprogramar" runat="server" Text="" Style="margin-left: auto; margin-right: auto"></asp:Label>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>


                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>



                    <div class="col-md-12" style="margin-left: auto; margin-right: auto" id="Div1" runat="server">
                        <div class="alert alert-success  alert-dismissible align-content-md-center" style="align-self: auto">
                            <div class="row">
                                <div class="col-3">          
                                    <p class="text-center"><img src="https://img.icons8.com/color/70/000000/accept-database.png"/><span class="dashicons dashicons-admin-home"></span></i></p>
                                </div>
                                <div class="col-9" style="text-align: center">
                                    <br>
                                    <h4><strong>¿Desea cancelar la LV?</strong></h4>
                                    <p>Si esta seguro dar clic en el botón "Cancelar LV"</p>
                                </div>
                                <br>
                                <p style="text-align: justify">Si en el motivo selecciono la opción cambio técnico responsable se hara el respectivo cambio, caso contrario el jefe o suplente recibira un correo para que haga la respectiva reprogramación. </p>
                                <asp:Label ID="Label1" runat="server" Text="" Width="100%"></asp:Label>
                            </div>
                        </div>
                    </div>



                </div>
                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <button type="button" class="btn btn-light" data-dismiss="modal">Cerrar</button>
                            <asp:Button ID="BtnCancelarLV" runat="server" Text="Cancelar LV" class="btn btn-dark" OnClick="BtnCancelarLV_Click"/>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
