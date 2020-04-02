<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="creacionNotificacion.aspx.cs" Inherits="Infatlan_STEI_Agencias.pages.CreacionNotificacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <!--PARA LLAMAR MODAL-->
    <script type="text/javascript">
        function openModal() { $('#modalnotoficacion').modal('show'); }
        function closeModal() { $('#modalnotoficacion').modal('hide'); }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">

    <div class="row page-titles">
        <div class="col-md-5 align-self-center">
            <h2 class="text-themecolor">Creación de Notificación</h2>
            <div class="mr-md-3 mr-xl-5">
                <p class="mb-md-0">Soporte Técnico y Comunicaciones</p>
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
                                <hr/>
                                <div class="row p-t-20">
                                    <div class="col-md-6">
                                        <label class="control-label   text-danger">*</label><label class="control-label">Fecha Mantenimiento:</label></label>
                                        <asp:TextBox ID="TextBox1" AutoPostBack="true" runat="server" TextMode="Date" OnTextChanged="TextBox1_TextChanged" class="form-control"></asp:TextBox>
                                    </div>

                                    <div class="col-md-6">
                                        <label class="control-label   text-danger">*</label><label class="control-label">Código/Lugar Agencia:</label></label>    
                                        <asp:DropDownList ID="DDLCodigoAgencia" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="DDLCodigoAgencia_SelectedIndexChanged" Enabled="false"></asp:DropDownList>
                                    </div>
                                </div>
                                <!--Fin Fila 1-->

                                <!--Inicio Fila 2-->
                                <div class="row p-t-20">
                                    <div class="col-md-6">
                                        <label class="control-label   text-danger">*</label><label class="control-label">Número SysAid:</label></label>                                        
                                        <asp:TextBox ID="TxSysAid" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label class="control-label">Area:</label>
                                        <asp:TextBox ID="TxArea" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <!--Fin Fila 2-->

                                <!--Inicio Fila 3-->
                                <div class="row p-t-20">
                                    <div class="col-md-6">
                                        <label class="control-label">Departamento:</label>
                                        <asp:TextBox ID="TxDepartamento" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>

                                    <div class="col-md-6">
                                        <label class="control-label">Mant. Equipo Comunicacion:</label>
                                        <asp:TextBox ID="TxMantEquipoComu" class="form-control" runat="server" ReadOnly="true" Text=""></asp:TextBox>
                                    </div>
                                </div>
                                <!--Fin Fila 3-->

                                <!--Inicio Fila 4-->
                                <div class="row p-t-20">
                                    <div class="col-md-6">
                                        <label class="control-label">Direccion:</label>
                                        <asp:TextBox ID="TxDireccion" class="form-control" runat="server" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
                                    </div>

                                    <div class="col-md-6">
                                        <label class="control-label">Telefono:</label>
                                        <asp:TextBox ID="TxTelefono" class="form-control" runat="server" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <!--Fin Fila 4-->

                                <!--Inicio Fila 5-->
                                <div class="row p-t-20">
                                    <div class="col-md-6">
                                         <label class="control-label  text-danger">*</label><label class="control-label">Hora Inicio Mantenimiento:</label>
                                        <asp:TextBox ID="TxHoraInicio" class="form-control" runat="server" TextMode="Time" Text="" ReadOnly="true"></asp:TextBox>
                                    </div>

                                    <div class="col-md-6">
                                        <label class="control-label  text-danger">*</label> <label class="control-label">Hora Fin Mantenimiento:</label>
                                        <asp:TextBox ID="TxHoraFin" class="form-control" runat="server" TextMode="Time" Text="" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <!--Fin Fila 5-->

                                <!--Inicio Fila 6-->
                                <div class="row p-t-20">
                                    <div class="col-md-12">
                                        <label class="control-label">Motivo:</label>
                                        <asp:TextBox ID="TxMotivo" class="form-control" runat="server" Text="Realizar acciones pro activas para prevenir la falla de equipos críticos." ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <!--Fin Fila 6-->

                                <!--Inicio Fila 7-->
                                <div class="row p-t-20">
                                    <div class="col-md-12">
                                        <label class="control-label">Impacto:</label>
                                        <asp:TextBox ID="TxImpacto" class="form-control" runat="server" Text="Durante la ventana de mantenimiento la agencia estará cerrada para el público en general." ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <!--Fin Fila 7-->
                                <br>                              
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- (FIN)Tarjeta Datos Generales -->

    <div class="row">
        <div class="col-lg-12">
            <div class="card">
                <div class="card-body">
                    <div class="form-body">
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                            <ContentTemplate>
                            <h3 class="card-title">Personal Encargado</h3>                             
                                <br>
                                <h5 class="card-title">-Técnico Responsable</h5>
                                <div class="row p-t-20">
                                    <div class="col-md-6">
                                         <label class="control-label  text-danger">*</label><label class="control-label">Nombre:</label>
                                        <asp:DropDownList ID="DDLNombreResponsable" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="DDLNombreResponsable_SelectedIndexChanged" Enabled="false"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-6">
                                        <label class="control-label">Identidad:</label>
                                        <asp:TextBox ID="TxIdentidadResponsable" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <br>
                                <br>

                                <h5 class="card-title">-Técnicos Participantes </h5>
                                <label class="control-label text-danger">*</label><label class="control-label">Nombre:</label>
                                <asp:DropDownList ID="DDLNombreParticipantes" AutoPostBack="true" runat="server" CssClass="fstdropdown-select form-control" OnTextChanged="DDLNombreParticipantes_TextChanged" Enabled="false"></asp:DropDownList>
                                <br>
                                <br>
                                <div class="row col-12">
                                    <div class="col-12 grid-margin stretch-card">
                                        <div class="table-responsive">
                                            <asp:GridView ID="GVBusqueda" runat="server"
                                                CssClass="table table-bordered"
                                                PagerStyle-CssClass="pgr"
                                                HeaderStyle-CssClass="table" HeaderStyle-HorizontalAlign="center"
                                                RowStyle-CssClass="rows"
                                                AutoGenerateColumns="false"
                                                AllowPaging="true"
                                                GridLines="None"
                                                PageSize="10" OnPageIndexChanging="GVBusqueda_PageIndexChanging" OnRowCommand="GVBusqueda_RowCommand">

                                                <Columns>
                                                    <asp:TemplateField HeaderText="Acción" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LBEliminarTecnicoParticipantes" runat="server"  CommandArgument='<%# Eval("idUsuario") %>' CommandName="Eliminar">
                                                                <img src="https://img.icons8.com/color/23/000000/delete-property.png"/>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="5%" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="idUsuario" HeaderText="Usuario" ItemStyle-Width="25%" />
                                                    <asp:BoundField DataField="nombre" HeaderText="Nombre" ItemStyle-Width="25%"/>
                                                    <asp:BoundField DataField="identidad" HeaderText="Identidad" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="center" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                                <br>
                                <br>

                               <%-- <h5 class="card-title">-Jefes de Agencia </h5>--%>
                               
                                <div class="row col-12" runat="server" visible="false">                                                                    
                                    <div class="col-md-6">
                                         <asp:TextBox runat="server"  ID="txtbuscarJefeNotif"   CssClass="form-control"></asp:TextBox>
                                    </div>                                   
                                    <div class="col-md-6">
                                         <asp:LinkButton runat="server" ID="btnBuscarJefe" OnClick="btnBuscarJefe_Click" CssClass="btn btn-info mr-2"><i class="fa fa-search"></i></asp:LinkButton>
                                    </div>
                                </div>
                                <br><br>
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
                                                GridLines="None" OnRowCommand="GVJefesAD_RowCommand"
                                                PageSize="10" OnPageIndexChanging="GVJefesAD_PageIndexChanging">
                                                <%--Style="margin: 30px 0px 20px 0px"--%>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Acción" ItemStyle-HorizontalAlign="center" >
                                                        <ItemTemplate>
                                                            <asp:LinkButton runat="server" ID="btnCorreoJefe" CssClass="btn btn-cyan" CommandName="correos"  CommandArgument='<%# Eval("mail") %>'>
                                                                <i class="icon-plus">
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


                                <asp:GridView ID="GVjefesAgencias" runat="server"
                                    CssClass="table table-bordered"
                                    PagerStyle-CssClass="pgr"
                                    HeaderStyle-CssClass="table"
                                    RowStyle-CssClass="rows"
                                    AutoGenerateColumns="true"
                                    AllowPaging="true"
                                    GridLines="None"
                                    PageSize="10"  OnRowCommand="GVjefesAgencias_RowCommand"
                                    Style="margin: 30px 0px 20px 0px">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-Width="60px">
                                            <ItemTemplate>
                                                <!--<button id="btnBorrarGrid" class="btn btn-danger waves-effect waves-light" type="button"><span class="btn-label"><i class="fa fa-trash"></i></span></button>
                                                <!-- <button type="button" class="btn btn-rounded btn-block btn-danger btn-sm"><i class="fa fa-minus-circle"></i></button>-->
                                                <asp:LinkButton ID="Btnseleccionar" OnClick="Btnseleccionar_Click" Enabled="true" runat="server" Text="" class="btn btn-danger mr-2" CommandArgument='<%# Eval("Correo") %>' CommandName="eliminar"><i class="icon-trash"></i></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>









                                </div>


<%--                                <div class="row col-12">
                                    <div class="col-12 grid-margin stretch-card">
                                        <div class="table-responsive">
                                            <asp:GridView ID="GVCorreoJefeAgencia" runat="server"
                                                CssClass="table table-bordered"
                                                PagerStyle-CssClass="pgr"
                                                HeaderStyle-CssClass="table"
                                                RowStyle-CssClass="rows"
                                                AutoGenerateColumns="false"
                                                AllowPaging="true"
                                                GridLines="None"
                                                PageSize="10" OnPageIndexChanging="GVCorreoJefeAgencia_PageIndexChanging" OnRowCommand="GVCorreoJefeAgencia_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LBEliminar" runat="server" CssClass="btn btn-danger" CommandArgument='<%# Eval("CorreoJefeAgencia") %>' CommandName="Eliminar">
                                                                <i class="icon-trash"></i>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="CorreoJefeAgencia" HeaderText="Correo" ItemStyle-Width="90%" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>--%>

                         
                            </ContentTemplate>
                        </asp:UpdatePanel>




                        <asp:UpdatePanel ID="UpdatePrincipalBotones" runat="server">
                            <ContentTemplate>

   <%--                     
                                <div class="row col-8 ">
                                    <div class="col text-center col-lg-2 col-md-4">
                                        <asp:Button ID="BtnEnviarNotificacion" class="btn btn-block btn-lg btn-success" runat="server" Text="Enviar " OnClick="BtnEnviarNotificacion_Click" />

                                    </div>
                                    <div class="col text-center col-lg-2 col-md-4">
                                        <asp:Button ID="BtnCancelarNotificacion" class="btn btn-block btn-lg btn-danger " runat="server" Text="Cancelar" OnClick="BtnCancelarNotificacion_Click" />
                                    </div>

                                </div>
                            </div>--%>

                      
                            <div class="row p-t-20 col-md-12">
                                <div class="col-md-4">
                                    <asp:Button ID="BtnEnviarNotificacion" class="btn btn-block btn-success" runat="server" Text="Enviar" OnClick="BtnEnviarNotificacion_Click" />
                                </div>

                                <div class="col-md-4">
                                    <asp:Button ID="BtnCancelarNotificacion" class="btn btn-block  btn-danger" runat="server" Text="Cancelar" OnClick="BtnCancelarNotificacion_Click" />
                                </div>

                                <div class="col-md-4">
                                      <a href="../../default.aspx"" class="btn  btn-block btn-primary">Volver</a>
                                </div>

                            </div>
                            <br />


                            </ContentTemplate>
                        </asp:UpdatePanel>

<%-- Inicio Modal --%>
    <div class="modal fade" id="modalnotoficacion" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="width: 600px; top: 320px; left: 50%; transform: translate(-50%, -50%);">
                 <div class="modal-header bg-dark">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <h3 class="modal-title" style="color: white" >
                                <asp:Label ID="lbTitulo" runat="server" Text="" Style="margin-left: auto; margin-right: auto"></asp:Label></h3>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <button type="button" class="close" style="color: white"  data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>

                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <div class="form-group row">
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
                                        <label class="col-sm-3 col-form-label">Lugar:</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="TxLugar" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12" runat="server">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">Responsable:</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="TxResponsable" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12" runat="server">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">Hora Inicio:</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="TxHrInicioModal" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12" runat="server">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">Hora Fin:</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="TxHrFinModal" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <div class="col-md-12" style="margin-left: auto; margin-right: auto" id="Div3" runat="server">
                        <div class="alert alert-success  alert-dismissible align-content-md-center" style="align-self: auto">
                            <div class="row">
                                <div class="col-3">
                                    <br>
                                    <p class="text-center"><img src="https://img.icons8.com/color/70/000000/accept-database.png"/><span class="dashicons dashicons-admin-home"></span></i></p>
                                </div>
                                <div class="col-9" style="text-align: center">
                                    <br>
                                    <h4><strong>¿Está seguro que desea enviar la notifiacion?</strong></h4>
                                    <p>Si esta seguro dar clic en el botón "Crear"</p>
                                </div>
                                <br>
                              
                                <asp:Label ID="Label6" runat="server" Text="" Width="100%"></asp:Label>
                            </div>
                        </div>
                    </div>



                </div>
                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                             <button type="button" class="btn btn-light"
                    data-dismiss="modal">Close</button>
                            <%--<asp:Button runat="server" ID="btnModalEnviarNotificacion" OnClick="btnModalEnviarNotificacion_Click" CssClass="btn btn-block btn-lg  btn-success" Text="Si" />--%>
                            <asp:Button ID="btnModalEnviarNotificacion" runat="server" Text="Crear" class="btn btn-dark"  OnClick="btnModalEnviarNotificacion_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>




            </div>
        </div>
    </div>
                   

                        <!-- Modal asegurar notificacion -->
                      <%--  <div class="modal bs-example-modal-lg" id="modalnotoficacion" tabindex="-1" data-backdrop="static" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" style="display: none;">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header modal-colored-header  bg-success">
                                        <h4 class="modal-title" id="myLargeModalLabel">¿Esta seguro que desea enviar la notificación?</h4>
                                    </div>

                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <div class="modal-footer col-12">

                                                <div class="row col-6">
                                                    <asp:Button runat="server" ID="btnModalEnviarNotificacion" OnClick="btnModalEnviarNotificacion_Click" CssClass="btn btn-block btn-lg  btn-success" Text="Si" />
                                                </div>
                                                <div class="row col-6">
                                                    <asp:Button runat="server" ID="btnModalCerrarNotificacion" OnClick="btnModalCerrarNotificacion_Click" CssClass="btn btn-block btn-lg btn-danger" Text="No" />

                                                </div>
                                            </div>


                                           
                                        </ContentTemplate>
                                    </asp:UpdatePanel>


                                </div>
                                <!-- /.modal-content -->
                            </div>
                            <!--/.modal-dialog -->
                        </div>--%>
                        <!-- /asegurar notificacion -->

                    </div>
                </div>
            </div>
        </div>

    </div>

    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
