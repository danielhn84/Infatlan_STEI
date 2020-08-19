<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="crearNotificacion.aspx.cs" Inherits="Infatlan_STEI_Comunicacion.pages.mantenimiento.crearNotificacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <script type="text/javascript">
        var updateProgress = null;
        function postbackButtonClick() {
            updateProgress = $find("<%= UpdateProgress1.ClientID %>");
            window.setTimeout("updateProgress.set_visible(true)", updateProgress.get_displayAfter());
            return true;
        }
    </script>

    <script type="text/javascript">
        function abrirModal() { $('#ModalConfirmar').modal('show'); }
        function cerrarModal() { $('#ModalConfirmar').modal('hide'); }
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
            <h4 class="text-themecolor">Crear Notificación</h4>
        </div>
        <div class="col-md-7 align-self-center text-right">
            <div class="d-flex justify-content-end align-items-center">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Módulos</a></li>
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Comunicación</a></li>
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Mantenimiento</a></li>
                    <li class="breadcrumb-item active">Crear Notificación</li>
                </ol>
            </div>
        </div>
    </div>
    <!-- (INICIO)Tarjeta Datos Generales -->
    <div class="row">
        <div class="col-lg-12">
            <div class="card">
                <div class="card-body">
                    <div class="form-body">
                        <h3 class="card-title">Datos Generales</h3>
                        <asp:UpdatePanel runat="server" ID="UPFormulario">
                            <ContentTemplate>
                                <!--Inicio Fila 1-->
                                <div class="row col-12">
                                    <div class="col-md-6">
                                        <label class="control-label   text-danger">*</label><label class="control-label">Fecha Mantenimiento:</label>
                                        <asp:TextBox ID="TxFechaMantenimiento" AutoPostBack="true" runat="server" ReadOnly="true" class="form-control"></asp:TextBox>
                                    </div>

                                    <div class="col-md-6">
                                        <label class="control-label text-danger">*</label><label class="control-label">Nodo:</label>
                                        <asp:TextBox ID="TxNodo" AutoPostBack="true" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <br>
                                <!--Fin Fila 1-->

                                <!--Inicio Fila 2-->
                                <div class="row col-12">
                                    <div class="col-md-6">
                                        <label class="control-label text-danger">*</label><label class="control-label">Número SysAid:</label></label>                                        
                                        <asp:TextBox ID="TxSysAid" class="form-control" runat="server" ReadOnly="false"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label class="control-label text-danger">*</label><label class="control-label">Id Control Cambio:</label></label>             
                                          <asp:TextBox ID="TxControlCambio" class="form-control" runat="server" ReadOnly="false"></asp:TextBox>
                                    </div>
                                </div>
                                <br>
                                <!--Fin Fila 2-->

                                <!--Inicio Fila 3-->
                                <div class="row col-12">
                                    <div class="col-md-6">
                                        <label class="control-label">Ip:</label>
                                        <asp:TextBox ID="TxIp" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>

                                    <div class="col-md-6">
                                        <label class="control-label">Zona:</label>
                                        <asp:TextBox ID="TxZona" class="form-control" runat="server" ReadOnly="true" Text=""></asp:TextBox>
                                    </div>
                                </div>
                                <br>
                                <!--Fin Fila 3-->

                                <!--Inicio Fila 4-->
                                <div class="row col-12">
                                    <div class="col-md-6">
                                        <label class="control-label">IOS Image:</label>
                                        <asp:TextBox ID="TxImagen" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>

                                    <div class="col-md-6">
                                        <label class="control-label">IOS Version:</label>
                                        <asp:TextBox ID="TXVersion" class="form-control" runat="server" ReadOnly="true" Text=""></asp:TextBox>
                                    </div>
                                </div>
                                <br>
                                <!--Fin Fila 4-->

                                <!--Inicio Fila 5-->
                                <div class="row col-12">
                                    <div class="col-md-6">
                                        <label class="control-label">Tipo:</label>
                                        <asp:TextBox ID="TxTipo" class="form-control" runat="server" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
                                    </div>

                                    <div class="col-md-6">
                                        <label class="control-label">Dirección:</label>
                                        <asp:TextBox ID="TxDirección" class="form-control" runat="server" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <br>
                                <!--Fin Fila 5-->

                                <!--Inicio Fila 6-->
                                <div class="row col-12">
                                    <div class="col-md-6">
                                        <label class="control-label  text-danger">*</label><label class="control-label">Hora Inicio Mantenimiento:</label>
                                        <asp:TextBox ID="TxHoraInicio" class="form-control" runat="server" TextMode="Time" Text="" ReadOnly="false"></asp:TextBox>
                                    </div>

                                    <div class="col-md-6">
                                        <label class="control-label  text-danger">*</label>
                                        <label class="control-label">Hora Fin Mantenimiento:</label>
                                        <asp:TextBox ID="TxHoraFin" class="form-control" runat="server" TextMode="Time" Text="" ReadOnly="false"></asp:TextBox>
                                    </div>
                                </div>
                                <br>
                                <!--Fin Fila 6-->

                                <!--Inicio Fila 7-->
                                <div class="row col-12">
                                    <div class="col-md-12">
                                        <label class="control-label">Motivo:</label>
                                        <asp:TextBox ID="TxMotivo" class="form-control" runat="server" Text="Mantenimiento preventivo y actualización de IOS" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <br>
                                <!--Fin Fila 7-->

                                <!--Inicio Fila 8-->
                                <div class="row col-12">
                                    <div class="col-md-12">
                                        <label class="control-label  text-danger">*</label><label class="control-label">Impacto:</label>
                                        <asp:TextBox ID="TxImpacto" class="form-control" runat="server" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                    </div>
                                </div>
                                <br>
                                <!--Fin Fila 8-->
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- (FIN)Tarjeta Datos Generales -->

    <!-- (INICIO)Tarjeta Personal Encargado -->
    <div class="row">
        <div class="col-lg-12">
            <div class="card">
                <div class="card-body">
                    <div class="form-body">
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                            <ContentTemplate>
                                <h3 class="card-title">Personal Encargado</h3>
                                <h5 class="card-title">Responsable</h5>
                                <hr>
                                <div class="row col-12" runat="server">
                                    <div class="col-6">
                                        <label class="control-label">Nombre:</label>
                                        <asp:TextBox ID="TxResponsable" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div class="col-6">
                                        <label class="control-label">Identidad:</label>
                                        <asp:TextBox ID="TxIdentidadResponsable" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <br>
                                <br>
                                <h5 class="card-title">Participantes Internos</h5>
                                <hr>
                                <div class="row col-12">
                                    <div class="col-1">
                                        <label class="control-label col-form-label">Nombre:</label>

                                    </div>
                                    <div class="col-5">
                                        <asp:DropDownList ID="DDLNombreParticipantes" AutoPostBack="true" runat="server" CssClass="fstdropdown-select form-control" Enabled="true" OnSelectedIndexChanged="DDLNombreParticipantes_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <br>
                                <div class="row col-12">
                                    <div class="col-12 grid-margin stretch-card">
                                        <div class="table-responsive">
                                            <asp:GridView ID="GVParticipantes" runat="server"
                                                CssClass="table table-bordered"
                                                PagerStyle-CssClass="pgr"
                                                HeaderStyle-CssClass="table" HeaderStyle-HorizontalAlign="center"
                                                RowStyle-CssClass="rows"
                                                AutoGenerateColumns="false"
                                                AllowPaging="true" OnRowCommand="GVParticipantes_RowCommand"
                                                GridLines="None" OnPageIndexChanging="GVParticipantes_PageIndexChanging"
                                                PageSize="10">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Acción" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LBEliminarTecnicoParticipantes" class="btn btn-danger mr-2" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("idUsuario") %>'>  
                                                                <i class="icon-close"></i>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="idUsuario" HeaderText="Usuario" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="center" />
                                                    <asp:BoundField DataField="nombre" HeaderText="Nombre" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="center" />
                                                    <asp:BoundField DataField="identidad" HeaderText="Identidad" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="center" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                                <br>
                                <br>
                                <h5 class="card-title">Participantes Externos</h5>
                                <hr>
                                <div class="row col-12">
                                    <div class="col-1">
                                        <label class="control-label col-form-label">Nombre:</label>
                                    </div>
                                    <div class="col-5">
                                        <asp:DropDownList ID="DdlExternos" AutoPostBack="true" runat="server" CssClass="fstdropdown-select form-control" Enabled="true" OnSelectedIndexChanged="DdlExternos_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <br>
                                <div class="row col-12">
                                    <div class="col-12 grid-margin stretch-card">
                                        <div class="table-responsive">
                                            <asp:GridView ID="GvExterno" runat="server"
                                                CssClass="table table-bordered"
                                                PagerStyle-CssClass="pgr"
                                                HeaderStyle-CssClass="table" HeaderStyle-HorizontalAlign="center"
                                                RowStyle-CssClass="rows"
                                                AutoGenerateColumns="false"
                                                AllowPaging="true" OnRowCommand="GvExterno_RowCommand"
                                                GridLines="None" OnPageIndexChanging="GvExterno_PageIndexChanging"
                                                PageSize="10">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Acción" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LBEliminarUsuarioExterno" class="btn btn-danger mr-2" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("idUsuarioExterno") %>'>  
                                                                <i class="icon-close"></i>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="idUsuarioExterno" HeaderText="Usuario" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="center" Visible="false" />
                                                    <asp:BoundField DataField="nombre" HeaderText="Nombre" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="center" />
                                                    <asp:BoundField DataField="identidad" HeaderText="Identidad" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="center" />
                                                    <asp:BoundField DataField="empresa" HeaderText="Empresa" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="center" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                                <br>
                                <br>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- (FIN)Tarjeta Personal Encargado -->


    <!-- (INICIO)Tarjeta Notificar a -->
    <div class="row">
        <div class="col-lg-12">
            <div class="card">
                <div class="card-body">
                    <div class="form-body">
                        <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Conditional">
                            <ContentTemplate>
                                <h3 class="card-title">Personal a Notificar</h3>
                                <h5 class="card-title">Personal del Banco</h5>
                                <hr>
                                <div class="row col-12" runat="server" visible="true">
                                    <div class="col-md-6">
                                        <asp:TextBox runat="server" ID="txBuscarJefe" placeholder="Búsqueda por nombre, apellido o correo, luego dar clic en el boton" ReadOnly="false" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:LinkButton runat="server" ID="btnBuscarJefe" OnClick="btnBuscarJefe_Click" CssClass="btn btn-info mr-2"><i class="fa fa-search"></i></asp:LinkButton>
                                    </div>
                                </div>
                                <br>
                                <br>
                                <div class="row col-12">
                                    <div class="col-12 grid-margin stretch-card">
                                        <div class="table-responsive">
                                            <asp:GridView ID="GVJefesAD" runat="server"
                                                CssClass="table table-bordered"
                                                PagerStyle-CssClass="pgr"
                                                HeaderStyle-CssClass="table" HeaderStyle-HorizontalAlign="center"
                                                RowStyle-CssClass="rows"
                                                AutoGenerateColumns="false"
                                                AllowPaging="true"
                                                GridLines="None"
                                                PageSize="10" OnRowCommand="GVJefesAD_RowCommand"
                                                Style="margin: 30px 0px 20px 0px" OnPageIndexChanging="GVJefesAD_PageIndexChanging">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Acción" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton runat="server" ID="btnCorreoJefe" CssClass="btn btn-cyan" CommandName="correos" CommandArgument='<%# Eval("mail") %>'>
                                                                <i class="icon-plus"></i>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="givenName" HeaderText="Nombres" />
                                                    <asp:BoundField DataField="sn" HeaderText="Apellidos" />
                                                    <asp:BoundField DataField="mail" HeaderText="Correo" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>

                                <div class="row col-12">
                                    <div class="col-12 grid-margin stretch-card">
                                        <div class="table-responsive">
                                            <asp:GridView ID="GVjefesAgencias" runat="server"
                                                CssClass="table table-bordered"
                                                PagerStyle-CssClass="pgr"
                                                HeaderStyle-CssClass="table" HeaderStyle-HorizontalAlign="center"
                                                RowStyle-CssClass="rows"
                                                AutoGenerateColumns="true"
                                                AllowPaging="true"
                                                GridLines="None" OnPageIndexChanging="GVjefesAgencias_PageIndexChanging"
                                                PageSize="10" OnRowCommand="GVjefesAgencias_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Acción" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="Btnseleccionar" Enabled="true" runat="server" Text="" class="btn btn-danger mr-2" CommandArgument='<%# Eval("Correo") %>' CommandName="eliminar">
                                                    <i class="icon-close"></i>

                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="25%" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>


                                <br>
                                <br>
                                <h5 class="card-title">Personal de la filiares</h5>
                                <hr>
                                <div class="row col-12" runat="server" visible="true">
                                    <div class="col-md-6">
                                        <asp:TextBox runat="server" ID="TxCorreo" placeholder="Ingrese correo, luego dar clic en el botón"  CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:LinkButton runat="server" ID="LbAñadirCorreoFiliar" OnClick="LbAñadirCorreoFiliar_Click" CssClass="btn btn-info mr-2"><i class="fa fa-plus"></i></asp:LinkButton>
                                    </div>
                                </div>
                                <br>
                                <br>
                                <div class="row col-12">
                                    <div class="col-12 grid-margin stretch-card">
                                        <div class="table-responsive">
                                            <asp:GridView ID="GvFiliares" runat="server"
                                                CssClass="table table-bordered"
                                                PagerStyle-CssClass="pgr"
                                                HeaderStyle-CssClass="table" HeaderStyle-HorizontalAlign="center"
                                                RowStyle-CssClass="rows"
                                                AutoGenerateColumns="true"
                                                AllowPaging="true"
                                                GridLines="None" 
                                                PageSize="10"  >
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Acción" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="Btnseleccionar" Enabled="true" runat="server" Text="" class="btn btn-danger mr-2" CommandArgument='<%# Eval("correo") %>' CommandName="eliminar">
                                                    <i class="icon-close"></i>

                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="25%" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br>
                        
                        <br>
                        <br>
                        <div class="col-md-12" style="text-align: center">
                            <label class="control-label text-danger" style="text-align: center">Los campos con (*) son obligatorios</label>
                        </div>

                        <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Button Text="Enviar" class="btn btn-success" ID="BtnEnviar" OnClick="BtnEnviar_Click" runat="server" />
                                <asp:Button Text="Cancelar" class="btn btn-danger" ID="BtnCancelar" OnClick="BtnCancelar_Click" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>



    <%-- Inicio Modal --%>
    <div class="modal fade" id="ModalConfirmar" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title" id="ModalLabelConfirmar">
                                <b>
                                    <asp:Label runat="server" ID="LbTitulo" CssClass="col-form-label"></asp:Label></b>
                            </h4>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="modal-body">
                    <div class="col-12" style="text-align: center">
                        <br>
                        <h4><strong>¿Está seguro que desea enviar la notifiacion?</strong></h4>
                        <p>Si esta seguro dar clic en el botón "Crear"</p>
                    </div>
                </div>
                <br>

                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                            <asp:Button ID="BtnConfirmar" runat="server" Text="Enviar" class="btn btn-danger" OnClick="BtnConfirmar_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
