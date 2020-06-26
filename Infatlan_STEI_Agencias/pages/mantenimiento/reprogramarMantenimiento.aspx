<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="reprogramarMantenimiento.aspx.cs" Inherits="Infatlan_STEI_Agencias.pages.reprogramarMantenimiento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <script type="text/javascript">
        var updateProgress = null;
        function postbackButtonClick() {
            updateProgress = $find("<%= UpdateProgress1.ClientID %>");
            window.setTimeout("updateProgress.set_visible(true)", updateProgress.get_displayAfter());
            return true;
        }

        function openModalReprogramarMantenimiento() { $('#ModalReprogramarMantenimiento').modal('show'); }
        function closeModalReprogramarMantenimiento() { $('#ModalReprogramarMantenimiento').modal('hide'); }
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
                    <li class="breadcrumb-item active">Reprogramar</li>
                </ol>
            </div> 
        </div>
    </div>

    <div class="card">
        <asp:UpdatePanel ID="UpdateDivBusquedas" runat="server">
            <ContentTemplate>
                <div class="row" id="DivBusqueda" runat="server">
                    <div class="col-12 grid-margin stretch-card">
                        <div class="card">
                            <div class="card-body">
                                <h4 class="card-title">Reprogramación de Mantenimientos</h4>
                                <p>Mantenimientos que han sido cancelados y estan pendientes de reprogramar porn parte de jefe o suplente.</p>
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
                                                            <asp:TextBox ID="TxBuscarAgencia" runat="server" placeholder="Búsqueda por agencia o codigo, luego presione Enter" class="form-control" AutoPostBack="true"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div class="card">
            <div class="row col-12">
                <div class="col-12 grid-margin stretch-card">
                    <div class="table-responsive">
                        <asp:UpdatePanel runat="server" ID="UPGvBusqueda">
                            <ContentTemplate>
                                <asp:GridView ID="GvMantPendientesReprogramar" runat="server"
                                    CssClass="table table-bordered"
                                    PagerStyle-CssClass="pgr"
                                    HeaderStyle-CssClass="table" HeaderStyle-HorizontalAlign="center"
                                    RowStyle-CssClass="rows"
                                    AutoGenerateColumns="false"
                                    AllowPaging="true"
                                    GridLines="None"
                                    PageSize="10" OnRowCommand="GvMantPendientesReprogramar_RowCommand">

                                    <Columns>
                                        <asp:TemplateField  HeaderText="Acción" ItemStyle-HorizontalAlign="center" >
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LbReprogramar" class="btn  btn-cyan mr-2" runat="server"  CommandName="Reprogramar" CommandArgument='<%# Eval("id_Mantenimiento") %>'>
                                                    <i class="icon-calender" ></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="id_Mantenimiento" HeaderText="Id" HeaderStyle-Width="5%" />
                                        <asp:BoundField DataField="fecha" HeaderText="Fecha" HeaderStyle-Width="10%" />
                                        <asp:BoundField DataField="Cod_Agencia" HeaderText="Codigo Agencia" HeaderStyle-Width="10%" />
                                        <asp:BoundField DataField="Lugar" HeaderText="Lugar" HeaderStyle-Width="15%" />
                                        <asp:BoundField DataField="Area" HeaderText="Area" HeaderStyle-Width="15%" />
                                        <asp:BoundField DataField="motivoCancelacion" HeaderText="Motivo" HeaderStyle-Width="10%" />
                                        <asp:BoundField DataField="detalleCancelación" HeaderText="Detalle" HeaderStyle-Width="25%" />

                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="row p-t-20 col-md-12">
                                <div class="col-md-4 " style="margin-left: auto; margin-right: auto">
                                    <a href="../../default.aspx" class="btn  btn-block btn-primary">Volver</a>
                                </div>
                            </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="ModalReprogramarMantenimiento" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="width: 600px; top: 320px; left: 50%; transform: translate(-50%, -50%);">
                <div class="modal-header bg-dark">

                    <asp:UpdatePanel ID="UpTituloReprogramar" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <h3 class="modal-title" style="color: white">Reprogramar 
                                <asp:Label ID="TituloModalReprogramar" runat="server" Text="" Style="margin-left: auto; margin-right: auto"></asp:Label></h3>
                        </ContentTemplate>
                    </asp:UpdatePanel>


                    <%--<h4 class="modal-title" id="ModalLabelUsuario" style="color: white"> Reprogramar Mantenimiento</h4>--%>
                    <button type="button" class="close" style="color: white" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>

                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <div class="form-group row">
                                <div class="col-md-12" runat="server">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">Id:</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="TxIdMant" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <label class="col-sm-2 col-form-label" style="text-align: right">Lugar:</label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="TxLugar" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12" runat="server">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">Area:</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="TxArea" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                        </div>

                                        <label class="col-sm-2 col-form-label" style="text-align: right">Fecha:</label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="TxFecha" class="form-control" runat="server" ReadOnly="true" ></asp:TextBox>
                                       </div>
                                    </div>
                                </div>

                                <div class="col-md-12" runat="server">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">Motivo Cancelación:</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="TxMotivo" class="form-control" runat="server" ReadOnly="true" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12" runat="server">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">Detalle Cancelación:</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="TxDetalle" class="form-control" runat="server" ReadOnly="true" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12" runat="server">
                                    <div class="form-group row">
                                        <div class="col-md-3">
                                            <label class="control-label   text-danger">*</label><label class="control-label">Nueva fecha:</label></label>
                                        </div>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="TxNuevaFecha" class="form-control" runat="server" TextMode="Date" OnTextChanged="TxNuevaFecha_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                     
                                <asp:UpdatePanel ID="UpdateModal" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="col-md-12" style="align-self: center" runat="server" id="DivAlerta" visible="false">
                                            <div class="alert alert-danger   align-content-md-center">
                                                <h3 class="text-danger" style="text-align: center"><i class="fa fa-exclamation-triangle"></i>Warning</h3>
                                                <asp:Label ID="LbMensajeModalErrorReprogramar" runat="server" Text="" Width="100%"></asp:Label>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                                <div class="col-md-12" style="margin-left: auto; margin-right: auto" id="Div1" runat="server">
                                    <div class="alert alert-success  alert-dismissible align-content-md-center" style="align-self: auto">
                                        <div class="row">
                                            <div class="col-3">
                                                <br>
                                                <p class="text-center"><img src="https://img.icons8.com/color/70/000000/accept-database.png"/></p>
                                            </div>                        

                                            <div class="col-9" style="text-align: center">
                                                <br>
                                                <h4><strong>¿Está seguro que desea reprogramar el mantenimiento?</strong></h4>
                                                <p>Si esta seguro dar clic en el botón "Reprogramar"</p>
                                            </div>

                                           <%-- <asp:Label ID="Label1" runat="server" Text="" Width="100%"></asp:Label>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdateUsuarioBotones" runat="server">
                        <ContentTemplate>
                            <button type="button" class="btn btn-light" data-dismiss="modal">Cerrar</button>
                            <asp:Button ID="btnModalReprogramarMantenimiento" runat="server" Text="Reprogramar" class="btn btn-dark" OnClick="btnModalReprogramarMantenimiento_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

  
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
