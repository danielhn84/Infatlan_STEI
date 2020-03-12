<%@ Page Title="" Language="C#" MasterPageFile="~/mainAgencia.Master" AutoEventWireup="true" CodeBehind="creacionNotificacion.aspx.cs" Inherits="Infatlan_STEI_Agencias.paginasAgencia.CreacionNotificacion" %>

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
    <!-- Row -->
    <div class="row">
        <div class="col-lg-12">
            <div class="card">

                <div class="card-body">
                    <div class="form-body">
                        <h3 class="card-title">Datos Generales</h3>
                        <hr>
                        <!--Inicio Fila 1-->
                        <asp:UpdatePanel runat="server" ID="UPFormulario">
                            <ContentTemplate>
                                <div class="row p-t-20">
                                    <div class="col-md-6">
                                        <label class="control-label   text-danger">*</label><label class="control-label">Fecha Mantenimiento:</label></label>
                                        <asp:TextBox ID="TextBox1" AutoPostBack="true" runat="server" TextMode="Date" OnTextChanged="TextBox1_TextChanged" class="form-control"></asp:TextBox>
                                    </div>

                                    <div class="col-md-6">
                                        <label class="control-label   text-danger">*</label><label class="control-label">Código/Lugar Agencia:</label></label>
     
                                        <asp:DropDownList ID="DDLCodigoAgencia" runat="server" AutoPostBack="true"  CssClass ="form-control"  OnSelectedIndexChanged="DDLCodigoAgencia_SelectedIndexChanged" Enabled="false" ></asp:DropDownList>

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
                                        <asp:TextBox ID="TxMantEquipoComu" class="form-control" runat="server" ReadOnly="true" Text="Si"></asp:TextBox>
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
                                        <label class="control-label">Hora Inicio Mantenimiento:</label>
                                        <asp:TextBox ID="TxHoraInicio" class="form-control" runat="server" TextMode="Time" Text="07:00" ReadOnly="true"></asp:TextBox>
                                    </div>

                                    <div class="col-md-6">
                                        <label class="control-label">Hora Fin Mantenimiento:</label>
                                        <asp:TextBox ID="TxHoraFin" class="form-control" runat="server" TextMode="Time" Text="08:30" ReadOnly="true"></asp:TextBox>
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
                                <!--Fin Fila 7-->

                                <!--Inicio Fila 6-->
                                <div class="row p-t-20">
                                    <div class="col-md-12">
                                        <label class="control-label">Impacto:</label>
                                        <asp:TextBox ID="TxImpacto" class="form-control" runat="server" Text="Durante la ventana de mantenimiento la agencia estará cerrada para el público en general." ReadOnly="true"></asp:TextBox>
                                    </div>

                                </div>
                                <!--Fin Fila 7-->

                                <br>
                                <br>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>

    </div>


    <div class="row">
        <div class="col-lg-12">
            <div class="card">
                <div class="card-body">
                    <div class="form-body">
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                            <ContentTemplate>
                                <h3 class="card-title">Personal Encargado</h3>
                                <hr>
                                <h5 class="card-title">-Técnico Responsable</h5>
                                <!--Inicio Fila 4-->
                                <div class="row p-t-20">
                                    <div class="col-md-6">
                                        <label class="control-label">Nombre:</label>
                                        <asp:DropDownList ID="DDLNombreResponsable" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="DDLNombreResponsable_SelectedIndexChanged" Enabled="false"></asp:DropDownList>
                                    </div>

                                    <div class="col-md-6">
                                        <label class="control-label">Identidad:</label>
                                        <asp:TextBox ID="TxIdentidadResponsable" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>

                                </div>

                                <br>
                                <br>
                                <br>


                                <h5 class="card-title">-Técnicos Participantes </h5>
                                <!--Fin Fila 4-->
                                <label class="control-label">*Nombre:</label>
                                <asp:DropDownList ID="DDLNombreParticipantes" AutoPostBack="true" runat="server" CssClass="fstdropdown-select form-control" OnTextChanged="DDLNombreParticipantes_TextChanged" Enabled="false"  ></asp:DropDownList>
                                <br>
                                <br>
                                <div class="row col-12">
                                    <div class="col-12 grid-margin stretch-card">
                                        <div class="table-responsive">
                                            <asp:GridView ID="GVBusqueda" runat="server"
                                                CssClass="table table-bordered"
                                                PagerStyle-CssClass="pgr"
                                                HeaderStyle-CssClass="table"
                                                RowStyle-CssClass="rows"
                                                AutoGenerateColumns="false"
                                                AllowPaging="true"
                                                GridLines="None"
                                                PageSize="10" OnPageIndexChanging="GVBusqueda_PageIndexChanging" OnRowCommand="GVBusqueda_RowCommand">

                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LBEliminarTecnicoParticipantes" runat="server" CssClass="btn btn-danger" CommandArgument='<%# Eval("idUsuario") %>' CommandName="Eliminar">
                                                                <i class="icon-trash"></i>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="idUsuario" HeaderText="Usuario" ItemStyle-Width="25%" />
                                                    <asp:BoundField DataField="nombre" HeaderText="Nombre" ItemStyle-Width="25%" />
                                                    <asp:BoundField DataField="identidad" HeaderText="Identidad" ItemStyle-Width="25%" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>

                                <br>


                                <br>
                                <h5 class="card-title">-Jefes de Agencia </h5>

                                <div class="col-md-12">
                                    <div class="form-group row">
                                      <%--  <label class="col-sm-1 col-form-label">Correo:</label>--%>
                                        <div class="col-sm-11">
                                            <%--<asp:TextBox ID="TxCorreo" runat="server" placeholder="usuario@bancatlan.hn" class="form-control" AutoPostBack="true" ReadOnly="true"></asp:TextBox>--%>
                                            <div class="input-group mb-3">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text"><i class="ti-email"></i></span>
                                                </div>
                                                <asp:TextBox ID="TxCorreo" runat="server" placeholder="usuario@bancatlan.hn" class="form-control" AutoPostBack="true" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-1">
                                            <%--<asp:Button ID="btnAgregar" runat="server" Text="Agregar" class="btn btn-success" OnClick="btnAgregar_Click" />--%>
                                            <asp:LinkButton ID="LBAgregar" runat="server" CssClass="btn btn-success" OnClick="btnAgregar_Click" Enabled="false" >
                                                                <i class="icon-user-follow"></i>
                                            </asp:LinkButton>


                                        </div>
                                    </div>
                                </div>


                                <div class="row col-12">
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
                                </div>

                                <br>
                                <br>
                            </ContentTemplate>
                        </asp:UpdatePanel>




                        <asp:UpdatePanel ID="UpdatePrincipalBotones" runat="server">
                            <ContentTemplate>
                                <div class="row col-12 text-center">
                                    <div class="col text-center">
                                        <asp:Button ID="BtnEnviarNotificacion" class="btn btn-block btn-lg btn-info" runat="server" Text="Enviar " OnClick="BtnEnviarNotificacion_Click" />
                                    </div>
                                    <div class="col text-center">
                                        <asp:Button ID="BtnCancelarNotificacion" class="btn btn-block btn-lg btn-danger " runat="server" Text="Cancelar" OnClick="BtnCancelarNotificacion_Click" />
                                    </div>





                                </div>
                                <%--<div class="col text-center">
                                    <asp:Button ID="BtnEnviarNotificacion" class="btn btn-block btn-lg btn-info" runat="server" Text="Enviar " OnClick="BtnEnviarNotificacion_Click" />
                                <asp:Button ID="BtnCancelarNotificacion" class="btn btn-block btn-lg btn-info " runat="server"  Text ="Cancelar"/>
                                
                                </div>--%>
                            </ContentTemplate>
                        </asp:UpdatePanel>



                   

                        <!-- Modal asegurar notificacion -->
                        <div class="modal bs-example-modal-lg" id="modalnotoficacion" tabindex="-1" data-backdrop="static" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" style="display: none;">
                            <div class="modal-dialog modal-xl">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h4 class="modal-title" id="myLargeModalLabel">¿Esta seguro que desea enviar la notificación?</h4>
                                    </div>

                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <div class="modal-footer col-12">

                                                <div class="row col-6">
                                                    <asp:Button runat="server" ID="btnModalEnviarNotificacion" OnClick="btnModalEnviarNotificacion_Click" CssClass="btn btn-block btn-lg btn-info" Text="Si" />
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
                        </div>
                        <!-- /asegurar notificacion -->

                    </div>
                </div>
            </div>
        </div>

    </div>

    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
