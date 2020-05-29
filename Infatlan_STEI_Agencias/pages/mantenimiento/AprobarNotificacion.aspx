<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="aprobarNotificacion.aspx.cs" Inherits="Infatlan_STEI_Agencias.pages.AprobarNotificacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">

    <script type="text/javascript">

        var updateProgress = null;
        function postbackButtonClick() {
            updateProgress = $find("<%= UpdateProgress1.ClientID %>");
            window.setTimeout("updateProgress.set_visible(true)", updateProgress.get_displayAfter());
            return true;
        }

        function openModal() { $('#ModalAprobacionNotificacion').modal('show'); }
        function closeModal() { $('#ModalAprobacionNotificacion').modal('hide'); }

        function openModalCancelacion() { $('#ModalCancelacionNotificacion').modal('show'); }
        function closeModalCancelacion() { $('#ModalCancelacionNotificacion').modal('hide'); }
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
            <h2 class="text-themecolor">Aprobación de Notificaciones</h2>
            <div class="mr-md-3 mr-xl-5">
                <%-- <h2>Creación de Notificación</h2>--%>
                <p class="mb-md-0">Soporte Técnico y Comunicaciones</p>
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
                                <h4 class="card-title">Aprobaciones Pendientes</h4>
                                <p>Notificaciones que no han sido aprobadas por jefe, suplentes o coordinadores de mantenimiento.</p>
                                <div class="col-md-12">
                                    <div class="form-group row">
       
                                        <div class="col-sm-12">
                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                <ContentTemplate>
                                                    <div class="row p-t-20">
                                                        <div class="col-md-1">
                                                            <label class="control-label">Buscar:</label></label>                                      
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:TextBox ID="TxBuscarAgencia" runat="server" placeholder="Búsqueda por agencia o codigo, luego presione Enter..." class="form-control" AutoPostBack="true" OnTextChanged="TxBuscarAgencia_TextChanged"></asp:TextBox>
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
                                        <asp:TemplateField HeaderText="Acción" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LBAprobar"   class="btn btn-success mr-2"  runat="server"  CommandName="Aprobar" CommandArgument='<%# Eval("id_Mantenimiento") %>'>
                                                        <i class="icon-check" ></i> 
                                                </asp:LinkButton>

                                                <asp:LinkButton ID="LBCancelar"  class="btn btn-primary mr-2" runat="server"  CommandName="Cancelar" CommandArgument='<%# Eval("id_Mantenimiento") %>'>
                                                        <i class="icon-close" ></i> 
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="13%" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="id_Mantenimiento" HeaderText="Id" />
                                        <asp:BoundField DataField="fecha" HeaderText="Fecha"  />
                                        <asp:BoundField DataField="Hr_Inicio" HeaderText="Hora Inicio" />
                                        <asp:BoundField DataField="Hr_Fin" HeaderText="Hora Fin" />
                                        <asp:BoundField DataField="Lugar" HeaderText="Lugar" />
                                        <asp:BoundField DataField="Cod_Agencia" HeaderText="Codigo Agencia" />
                                        <asp:BoundField DataField="Responsable" HeaderText="Responsable" />
                                        <asp:BoundField DataField="Area" HeaderText="Area" />

                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                    <div class="row p-t-20 col-md-12">
                        <div class="col-md-4 " style="margin-left: auto; margin-right: auto">
                              <a href="../../default.aspx"" class="btn  btn-block btn-primary">Volver</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <!-- Modal asegurar notificacion -->
    <div class="modal fade" id="ModalAprobacionNotificacion" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="width: 600px; top: 320px; left: 50%; transform: translate(-50%, -50%);">
                <div class="modal-header bg-dark">
                   
                     <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <h3 class="modal-title" style="color: white" >
                                <asp:Label ID="lbTitulo" runat="server" Text="" Style="margin-left: auto; margin-right: auto"></asp:Label></h3>
                        </ContentTemplate>
                    </asp:UpdatePanel>

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
                                        <label class="col-sm-3 col-form-label">Id Mantenimiento:</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="TxIdMant" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>


                                <div class="col-md-12" runat="server">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">Area:</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="TxArea" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>


                                <div class="col-md-12" runat="server">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">Fecha:</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="TxFecha" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>


                                <div class="col-md-12" runat="server">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">Técnico Responsable:</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="TxTecnicoResponsable" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12" runat="server">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">Técnicos Participantes:</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="TxParticipantes" class="form-control" runat="server" ReadOnly="true" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>


                    <div class="col-md-12" runat="server">
                        <%--<div class="form-group row">--%>
                            <div class="col-md-12" runat="server" id="DivAlerta" visible="true" style="display: flex; background-color: green; justify-content: center">
                                <p style="color: white; text-align:justify">Una vez aprobada la notificación por el coordinador o jefe, el técnico responsable recibirá un correo para que complete la solicitud de materiales.</p>
                            </div>
                        <%--</div>--%>
                    </div>


                </div>
                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdateUsuarioBotones" runat="server">
                        <ContentTemplate>

                             <button type="button" class="btn btn-light" data-dismiss="modal">Close</button> 
                            <asp:Button ID="btnModalAprobarNotificacion" runat="server" Text="Aprobar"  class="btn btn-dark" OnClick="btnModalAprobarNotificacion_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal asegurar notificacion Modal2 -->
    <div class="modal fade" id="ModalCancelacionNotificacion" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="width: 600px; top: 320px; left: 50%; transform: translate(-50%, -50%);">
                 <div class="modal-header bg-dark">
                 

                     <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                         <ContentTemplate>
                             <h3 class="modal-title" style="color: white">
                                 <asp:Label ID="lbTituloCancelar" runat="server" Text="" Style="margin-left: auto; margin-right: auto"></asp:Label></h3>
                         </ContentTemplate>
                     </asp:UpdatePanel>

                    <button type="button" class="close" style="color: white"  data-dismiss="modal" aria-label="Close">
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
                                            <label class="control-label   text-danger">*</label><label class="control-label">Motivo:</label></label>
                                        <%--<asp:TextBox ID="TextBox1" AutoPostBack="true" runat="server" TextMode="Date" OnTextChanged="TextBox1_TextChanged" class="form-control"></asp:TextBox>--%>
                                        </div>

                                        
                                        <div class="col-sm-9">
                                      <%--      <asp:DropDownList ID="DDLMotivo" runat="server" class="form-control"  AutoPostBack="true">
                                                <asp:ListItem Value="0" Text="Seleccione motivo..."></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Personal de agencia canceló mantenimiento"></asp:ListItem>
                                            </asp:DropDownList>--%>

                                            <asp:DropDownList ID="DDLMotivo" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="DDLMotivo_SelectedIndexChanged"></asp:DropDownList>

                                        </div>
                                    </div>
                                </div>


                                <div class="col-md-12" runat="server">
                                    <div class="form-group row">

                                        <div class="col-md-3">
                                            <label class="control-label   text-danger">*</label><label class="control-label">Detalle:</label></label>
                                        <%--<asp:TextBox ID="TextBox1" AutoPostBack="true" runat="server" TextMode="Date" OnTextChanged="TextBox1_TextChanged" class="form-control"></asp:TextBox>--%>
                                        </div>

                                        
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="TxDetalle" class="form-control" runat="server" TextMode="MultiLine" Rows="5"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>


                                <div class="col-md-12" style="text-align: center">
                                    <label class="control-label" style="text-align: center; color: tomato">Los campos con (*) son obligatorios</label>
                                </div>

                                <asp:UpdatePanel ID="UpdateModal" runat="server" UpdateMode="Conditional" >
                                    <ContentTemplate>
                                        <div class="col-md-12" runat="server">
                                            <div class="col-md-12" runat="server" id="Div2" visible="false" style="display: flex; background-color: tomato; justify-content: center">
                                                <asp:Label runat="server" CssClass="col-form-label text-white" ID="LbMensajeModalError"></asp:Label>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>









                                <%--                                <div class="col-md-12" style="margin-left: auto; margin-right: auto" id="Div1" runat="server">
                                    <div class="alert alert-success  alert-dismissible align-content-md-center" style="align-self: auto">
                                        <div class="row">
                                            <div class="col-3">
                                               
                                                <p class="text-center"><img src="https://img.icons8.com/color/70/000000/accept-database.png"/></i></p>
                                            </div>

                                            <div class="col-9" style="text-align: center">
                                                <br>
                                                <h4><strong>¿Desea cancelar la notificacion?</strong></h4>
                                                <p>Si esta seguro dar clic en el botón "Cancelar Notificación"</p>
                                            </div>

                                            <p style="text-align: justify">El jefe y suplente recibirán un correo para que procedan a reprogramar el mantenimiento preventivo. </p> 
                                        </div>
                                    </div>
                                </div>--%>
                            </div> 
                        </ContentTemplate>
                    </asp:UpdatePanel>


                </div>
                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <button type="button" class="btn btn-light"data-dismiss="modal">Cerrar</button>
                            <asp:Button ID="BtnCancelarNoti" runat="server" Text="Cancelar Notificación" class="btn btn-dark"  OnClick="BtnCancelarNoti_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

            </div>
        </div>
    </div>
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
