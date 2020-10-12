<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="lvPendientesCompletar.aspx.cs" Inherits="Infatlan_STEI_Agencias.pages.LvCompletar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">


    <script type="text/javascript">

        var updateProgress = null;
        function postbackButtonClick() {
            updateProgress = $find("<%= UpdateProgress1.ClientID %>");
            window.setTimeout("updateProgress.set_visible(true)", updateProgress.get_displayAfter());
            return true;
        }

        function openModalCancelarLV() { $('#ModalCancelacionLV').modal('show'); }
        function closeModalCancelarLV() { $('#ModalCancelacionLV').modal('hide'); }
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
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Agencias</a></li>
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Mantenimiento</a></li>
                    <li class="breadcrumb-item active">Completar LV</li>
                </ol>
            </div> 
        </div>
    </div>

    <div class="card">
        <div class="card-body">
            <h4 class="card-title">Listas Pendientes Completar</h4>
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
                                            <asp:LinkButton ID="LBAprobar" class="btn btn-success mr-2"  runat="server"  CommandName="Completar" CommandArgument='<%# Eval("id_Mantenimiento") %>'>
                                                        <i class="icon-check" ></i>
                                            </asp:LinkButton>

                                            <asp:LinkButton ID="LBCancelar" class="btn btn-primary mr-2" runat="server" CommandName="Cancelar" CommandArgument='<%# Eval("id_Mantenimiento") %>'>
                                                          <i class="icon-close" ></i>
                                            </asp:LinkButton>
                                             
                                        </ItemTemplate>
                                        <ItemStyle Width="12%" />
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
<%--                <div class="row p-t-20 col-md-12">
                                <div class="col-md-4 " style="margin-left: auto; margin-right: auto">
                                    <a href="../../default.aspx"" class="btn  btn-block btn-primary">Volver</a>
                                </div>
                            </div>--%>
                <br>
            </div>
        </div>


    </div>


    <!-- Modal asegurar notificacion Modal2 -->
    <div class="modal fade" id="ModalCancelacionLV" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="width: 600px; top: 320px; left: 50%; transform: translate(-50%, -50%);">
                <div class="modal-header" >
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <h3 class="modal-title"  id="exampleModalLabel" >Cancelar LV  
                                <asp:Label ID="Titulo" runat="server" Text="" Style="margin-left: auto; margin-right: auto"></asp:Label></h3>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
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



<%--                    <div class="col-md-12" style="margin-left: auto; margin-right: auto" id="Div1" runat="server">
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
                    </div>--%>



                </div>
             <%--   <div class="modal-footer">--%>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <button type="button" class="btn btn-light" data-dismiss="modal">Cerrar</button>
                            <asp:Button ID="BtnCancelarLV" runat="server" Text="Cancelar LV" class="btn btn-dark" OnClick="BtnCancelarLV_Click"/>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
<%--    </div>--%>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
