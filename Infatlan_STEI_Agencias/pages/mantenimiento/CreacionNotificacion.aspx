<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="creacionNotificacion.aspx.cs" Inherits="Infatlan_STEI_Agencias.pages.CreacionNotificacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <!--PARA LLAMAR MODAL-->
    <script type="text/javascript">

        var updateProgress = null;
        function postbackButtonClick() {
            updateProgress = $find("<%= UpdateProgress1.ClientID %>");
            window.setTimeout("updateProgress.set_visible(true)", updateProgress.get_displayAfter());
            return true;
        }

        function openModal() { $('#modalnotoficacion').modal('show'); }
        function closeModal() { $('#modalnotoficacion').modal('hide'); }
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
                    <li class="breadcrumb-item active">Crear Notificación</li>
                </ol>
            </div> 
        </div>
    </div>

    <!-- (INICIO)Tarjeta Datos Generales -->
    <div class="row p-t-20">
        <div class="col-lg-12">
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Notificaciones</h4>
                    <h6 class="card-subtitle">Creación de notificaciones de mantenimiento de agencias</h6>
                    <div class="form-body col-md-12">
                       <h5 class="card-subtitle m-t-10"><i class="fa fa-book"></i><b> DATOS GENERALES</b></h5>
                        <asp:UpdatePanel runat="server" ID="UPFormulario">
                            <ContentTemplate>
                                <!--Inicio Fila 1-->                                
                                <div class="row p-t-20">
                                    <div class="col-6">
                                        <label class="control-label   text-danger">*</label><label class="control-label">Fecha Mantenimiento:</label></label>
                                        <asp:TextBox ID="TextBox1" AutoPostBack="true" runat="server" TextMode="Date" OnTextChanged="TextBox1_TextChanged" class="form-control"></asp:TextBox>
                                    </div>

                                    <div class="col-6">
                                        <label class="control-label   text-danger">*</label><label class="control-label">Código/Lugar Agencia:</label></label>    
                                        <asp:DropDownList ID="DDLCodigoAgencia" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="DDLCodigoAgencia_SelectedIndexChanged" Enabled="false"></asp:DropDownList>
                                    </div>
                                </div>
                                <!--Fin Fila 1-->

                                <!--Inicio Fila 2-->
                                <div class="row p-t-20">
                                    <div class="col-6">
                                        <label class="control-label   text-danger">*</label><label class="control-label">Número SysAid:</label></label>                                        
                                        <asp:TextBox ID="TxSysAid" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div class="col-6">
                                        <label class="control-label">Area:</label>
                                        <asp:TextBox ID="TxArea" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <!--Fin Fila 2-->

                                <!--Inicio Fila 3-->
                                <div class="row p-t-20">
                                    <div class="col-6">
                                        <label class="control-label">Departamento:</label>
                                        <asp:TextBox ID="TxDepartamento" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>

                                    <div class="col-6">
                                        <label class="control-label">Mant. Equipo Comunicacion:</label>
                                        <asp:TextBox ID="TxMantEquipoComu" class="form-control" runat="server" ReadOnly="true" Text=""></asp:TextBox>
                                    </div>
                                </div>
                                <!--Fin Fila 3-->

                                <!--Inicio Fila 4-->
                                <div class="row p-t-20">
                                    <div class="col-6">
                                        <label class="control-label">Direccion:</label>
                                        <asp:TextBox ID="TxDireccion" class="form-control" runat="server" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
                                    </div>

                                    <div class="col-6">
                                        <label class="control-label">Telefono:</label>
                                        <asp:TextBox ID="TxTelefono" class="form-control" runat="server" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <!--Fin Fila 4-->

                                <!--Inicio Fila 5-->
                                <div class="row p-t-20">
                                    <div class="col-6">
                                         <label class="control-label  text-danger">*</label><label class="control-label">Hora Inicio Mantenimiento:</label>
                                        <asp:TextBox ID="TxHoraInicio" class="form-control" runat="server" TextMode="Time" Text="" ReadOnly="true"></asp:TextBox>
                                    </div>

                                    <div class="col-6">
                                        <label class="control-label  text-danger">*</label> <label class="control-label">Hora Fin Mantenimiento:</label>
                                        <asp:TextBox ID="TxHoraFin" class="form-control" runat="server" TextMode="Time" Text="" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <!--Fin Fila 5-->

                                <!--Inicio Fila 6-->
                                <div class="row p-t-20">
                                    <div class="col-12">
                                        <label class="control-label">Motivo:</label>
                                        <asp:TextBox ID="TxMotivo" class="form-control" runat="server" Text="Realizar acciones pro activas para prevenir la falla de equipos críticos." ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <!--Fin Fila 6-->

                                <!--Inicio Fila 7-->
                                <div class="row p-t-20">
                                    <div class="col-12">
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
                    <div class="form-body col-12">
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                            <ContentTemplate>
                            <h5 class="card-subtitle m-t-20"><i class="fa fa-user"></i><b> PERSONAL ENCARGADO</b></h5>                           
                                <br>
                               <%-- <h5 class="card-title">Técnico Responsable</h5>--%>
                                <div class="row 20">

                                    <div class="col-6">
                                       <label class="control-label">Técnico Responsable:</label>
                                        <asp:DropDownList ID="DDLNombreResponsable" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="DDLNombreResponsable_SelectedIndexChanged" Enabled="false"></asp:DropDownList>
                                    </div>
                    
                                    <div class="col-6">
                                        <label class="control-label">Identidad:</label>
                                        <asp:TextBox ID="TxIdentidadResponsable" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <br>
                                <br>

                               <%-- <h5 class="card-title">Técnicos Participantes </h5>--%>
                                <label class="control-label">Técnicos Participantes:</label>
                                <asp:DropDownList ID="DDLNombreParticipantes" AutoPostBack="true" runat="server" CssClass="fstdropdown-select form-control" OnTextChanged="DDLNombreParticipantes_TextChanged" Enabled="false"></asp:DropDownList>
                                <br>
                             
                                        <div class="table-responsive m-t-40">
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
                                                    <asp:TemplateField HeaderText="Acción" ItemStyle-HorizontalAlign="center"  HeaderStyle-Width="60px">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LBEliminarTecnicoParticipantes"  class="btn btn-danger mr-2" runat="server" Title="Eliminar" CommandName="Eliminar" CommandArgument='<%# Eval("idUsuario") %>'>
  
                                                                <i class="icon-close"></i>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <%--<ItemStyle Width="5%" />--%>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="idUsuario" HeaderText="Usuario" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="center" />
                                                    <asp:BoundField DataField="nombre" HeaderText="Nombre" ItemStyle-Width="30%"/>
                                                    <asp:BoundField DataField="identidad" HeaderText="Identidad" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="center" />
                                                     <asp:BoundField DataField="correo" HeaderText="Correo" Visible="false" ItemStyle-Width="30%" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                <br>
            

                                <h5 class="card-subtitle m-t-20" runat="server" id="H3JefeAgencia"><i class="fa fa-user"></i><b> JEFES DE AGENCIA</b></h5>
                                <div class="row">
                                    <div class="col-6">
                                        <asp:TextBox runat="server" ID="txtbuscarJefeNotif" placeholder="Búsqueda por nombre, apellido o correo, luego dar clic en el boton" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-6">
                                        <asp:LinkButton runat="server" ID="btnBuscarJefe" OnClick="btnBuscarJefe_Click" CssClass="btn btn-info mr-2"><i class="fa fa-search"></i></asp:LinkButton>
                                    </div>
                                </div>

                                        <div class="table-responsive m-t-40">
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
                                                            <asp:LinkButton runat="server" ID="btnCorreoJefe" Title="Seleccionar" CssClass="btn btn-cyan" CommandName="correos"  CommandArgument='<%# Eval("mail") %>'>
                                                                <i class="icon-plus"></i>
                                                            </asp:LinkButton>
                                                        
                                                        </ItemTemplate>                                                       
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="givenName" HeaderText="Nombres" />
                                                    <asp:BoundField DataField="sn" HeaderText="Apellidos"  />
                                                    <asp:BoundField DataField="mail" HeaderText="Correo" ItemStyle-HorizontalAlign="center"/>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
    
                                        <div class="table-responsive m-t-40">
                                            <asp:GridView ID="GVjefesAgencias" runat="server"
                                                CssClass="table table-bordered"
                                                PagerStyle-CssClass="pgr"
                                                HeaderStyle-CssClass="table" HeaderStyle-HorizontalAlign="center"
                                                RowStyle-CssClass="rows"
                                                AutoGenerateColumns="true"
                                                AllowPaging="true"
                                                GridLines="None" OnPageIndexChanging="GVjefesAgencias_PageIndexChanging"
                                                PageSize="10" OnRowCommand="GVjefesAgencias_RowCommand">
                                                <%--Style="margin: 30px 0px 20px 0px"--%>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Acción" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <!--<button id="btnBorrarGrid" class="btn btn-danger waves-effect waves-light" type="button"><span class="btn-label"><i class="fa fa-trash"></i></span></button>
                                                <!-- <button type="button" class="btn btn-rounded btn-block btn-danger btn-sm"><i class="fa fa-minus-circle"></i></button>-->
                                                            <asp:LinkButton ID="Btnseleccionar" Enabled="true" runat="server" Text="" class="btn btn-danger mr-2" CommandArgument='<%# Eval("Correo") %>' CommandName="eliminar">
                                                    <i class="icon-trash"></i>

                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>

                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <asp:UpdatePanel ID="UpdatePrincipalBotones" runat="server">
                            <ContentTemplate>
                               
                     <%--       <div class="row p-t-20 col-md-12">
                                <div class="col-md-4">
                                    <asp:Button ID="BtnEnviarNotificacion" class="btn btn-block btn-success" runat="server" Text="Enviar" OnClick="BtnEnviarNotificacion_Click" />
                                </div>

                                <div class="col-md-4">
                                    <asp:Button ID="BtnCancelarNotificacion" class="btn btn-block  btn-danger" runat="server" Text="Cancelar" OnClick="BtnCancelarNotificacion_Click" />
                                </div>

                                <div class="col-md-4">
                                      <a href="../../default.aspx"" class="btn  btn-block btn-primary">Volver</a>
                                </div>--%>

                                  <br />
                            <div class="modal-footer">
                                <asp:UpdatePanel ID="UpdateModificacionBotones" runat="server">
                                    <ContentTemplate>
                                        <%--<button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>--%>
                                        <asp:Button ID="BtnCancelarNotificacion" runat="server" Text="Cancelar" class="btn  btn-dark" OnClick="BtnCancelarNotificacion_Click" />
                                        <asp:Button ID="BtnEnviarNotificacion" runat="server" Text="Enviar" class="btn btn-success" OnClick="BtnEnviarNotificacion_Click" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <br />


                           <%-- </div>--%>
                            <br />


                            </ContentTemplate>
                        </asp:UpdatePanel>

<%-- Inicio Modal --%>
    <div class="modal fade" id="modalnotoficacion" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="width: 600px; top: 320px; left: 50%; transform: translate(-50%, -50%);">
                 <div class="modal-header">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <h4 class="modal-title" ><asp:Label ID="lbTitulo" runat="server" Text="" Style="margin-left: auto; margin-right: auto"></asp:Label></h4>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <%--<button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>--%>

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

  <%--                  <div class="col-md-12" style="margin-left: auto; margin-right: auto" id="Div3" runat="server">
                        <div class="alert alert-success  alert-dismissible align-content-md-center" style="align-self: auto">
                            <div class="row">
                                <div class="col-3">
                                    <br>
                                    <p class="text-center"><img src="https://img.icons8.com/color/70/000000/accept-database.png"/><span class="dashicons dashicons-admin-home"></span></p>
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
                    </div>--%>



                </div>
                <br />
                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                             <button type="button" class="btn btn-light"  data-dismiss="modal">Close</button>
          
                            <asp:Button ID="btnModalEnviarNotificacion" runat="server" Text="Crear" class="btn btn-success"  OnClick="btnModalEnviarNotificacion_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>




            </div>
        </div>
    </div>
                   


                    </div>
                </div>
            </div>
        </div>

    </div>

    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
