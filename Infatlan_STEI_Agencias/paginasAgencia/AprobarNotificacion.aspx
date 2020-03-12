﻿<%@ Page Title="" Language="C#" MasterPageFile="~/mainAgencia.Master" AutoEventWireup="true" CodeBehind="aprobarNotificacion.aspx.cs" Inherits="Infatlan_STEI_Agencias.paginasAgencia.AprobarNotificacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">

    <script type="text/javascript">
        function openModal() { $('#ModalAprobacionNotificacion').modal('show'); }
        function closeModal() { $('#ModalAprobacionNotificacion').modal('hide'); }

        function openModalCancelacion() { $('#ModalCancelacionNotificacion').modal('show'); }
        function closeModalCancelacion() { $('#ModalCancelacionNotificacion').modal('hide'); }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div class="row page-titles">
        <div class="col-md-5 align-self-center">
            <h2 class="text-themecolor">Aprobación de Notificaciones</h2>
            <div class="mr-md-3 mr-xl-5">
                <%-- <h2>Creación de Notificación</h2>--%>
                <p class="mb-md-0">Soporte Técnico y Comunicaciones</p>
            </div>
        </div>

        <%--        <div class="col-md-7 align-self-center text-right">
            <div class="d-flex justify-content-end align-items-center">
                <button type="button" class="btn btn-info d-none d-lg-block m-l-15"><i class="fa fa-plus-circle"></i>Cancelar Notificación</button>
            </div>
        </div>--%>
    </div>


    <div class="card">
        <asp:UpdatePanel ID="UpdateDivBusquedas" runat="server">
            <ContentTemplate>
                <div class="row" id="DivBusqueda" runat="server">
                    <div class="col-12 grid-margin stretch-card">
                        <div class="card">
                            <div class="card-body">
                                <h4 class="card-title">Aprobaciones Pendientes</h4>
                                <p>Notificaciones que no han sido aprobadas por Jefe, suplentes o coordinadores de mantenimiento.</p>
                                <div class="col-md-6">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">Buscar</label>
                                        <div class="col-sm-9">
                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                <ContentTemplate>
                                                    <div class="input-group mb-3">
                                                        <div class="input-group-prepend">
                                                            <span class="input-group-text" id="basic-addon1"><i class="ti-search"></i></span>
                                                        </div>
                                                        <asp:TextBox ID="TxBuscarAgencia" runat="server" placeholder="Ingrese nombre de agencia" class="form-control" AutoPostBack="true" OnTextChanged="TxBuscarAgencia_TextChanged"></asp:TextBox>
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
                                    HeaderStyle-CssClass="table"
                                    RowStyle-CssClass="rows"
                                    AutoGenerateColumns="false"
                                    AllowPaging="true"
                                    GridLines="None"
                                    PageSize="10" OnRowCommand="GVBusqueda_RowCommand">

                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LBAprobar" runat="server" CssClass="btn btn-success" CommandName="Aprobar" CommandArgument='<%# Eval("id_Mantenimiento") %>'>
                                                        <i class="icon-check"></i>
                                                </asp:LinkButton>

                                                <asp:LinkButton ID="LBCancelar" runat="server" CssClass="btn btn-danger" CommandName="Cancelar" CommandArgument='<%# Eval("id_Mantenimiento") %>'>
                                                        <i class="icon-close"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="id_Mantenimiento" HeaderText="Id Mantenimiento" />
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
                </div>
            </div>
        </div>
    </div>


    <!-- Modal asegurar notificacion -->
    <div class="modal fade" id="ModalAprobacionNotificacion" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="width: 600px; top: 320px; left: 50%; transform: translate(-50%, -50%);">
                <div class="modal-header">
                    <asp:UpdatePanel ID="UpdateLabelPermiso" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <h4 class="modal-title" id="ModalLabelUsuario">Aprobar Notificación</h4>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>

                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <%--                            <div class="form-group row">
                                <div class="col-sm-12">
                                    <asp:Label ID="LbInformacionNotificación" AutoPostBack="true" runat="server"></asp:Label>

                                </div>
                            </div>--%>

                            <div class="form-group row">
                                <div class="col-md-12" runat="server">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">Lugar:</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="TxLugar" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
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
                                        <label class="col-sm-3 col-form-label">Tecnico Responsable:</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="TxTecnicoResponsable" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12">                                   
                                    <label style="color:firebrick; text-align:center";  ><strong>Nota:Una vez aprobada la notificación el tecnico responsable recibirá un correo para que complete la lista de verificación. </strong></label>                                                                                         
                                </div>  

                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>


                </div>
                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdateUsuarioBotones" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="btnModalAprobarNotificacion" runat="server" Text="Aprobar Notificación" class="btn btn-success" OnClick="btnModalAprobarNotificacion_Click" />
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
                <div class="modal-header">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <h4 class="modal-title">Cancelar Notificación</h4>
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
                                        <label class="col-sm-3 col-form-label">*Motivo:</label>
                                        <div class="col-sm-9">
                                            <asp:DropDownList ID="DDLMotivo" runat="server" class="form-control">
                                                <asp:ListItem Value="0" Text="Seleccione motivo..."></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Personal de agencia canceló mantenimiento"></asp:ListItem>
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                </div>


                                <div class="col-md-12" runat="server">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">*Detalle:</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="TxDetalle" class="form-control" runat="server" TextMode="MultiLine" Rows="5"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12">                                   
                                    <label style="color:firebrick; text-align:center";  ><strong>Nota: El jefe y suplente recibirán un correo para que procedan a reprogramar el mantenimiento preventivo. </strong></label>                                                                                         
                               
                                 
                                </div>

                                <asp:UpdatePanel ID="UpdateModal" runat="server" UpdateMode="Always">
                                    <ContentTemplate>
                                        <div class="col-md-12   align-self-center" style="margin-left: auto; margin-right: auto">
                                            <asp:Label ID="LbMensajeModalError" runat="server" Text="" Style="color: indianred; display:inline-block" CssClass="StrongText"></asp:Label>
                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>


                </div>
                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>

                            <asp:Button ID="BtnCancelarNoti" runat="server" Text="Enviar" class="btn btn-success" OnClick="BtnCancelarNoti_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>