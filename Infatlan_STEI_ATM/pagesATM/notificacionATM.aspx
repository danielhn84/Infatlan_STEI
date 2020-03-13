﻿<%@ Page Title="" Language="C#" MasterPageFile="~/mainATM.Master" AutoEventWireup="true" CodeBehind="notificacionATM.aspx.cs" Inherits="Infatlan_STEI_ATM.pagesATM.solicitudATM" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <style>
        .hatm {
            color: #A9A9F5;
        }
    </style>
    <!--PARA LLAMAR MODAL-->
    <script type="text/javascript">
        function openModal() { $('#modalnotoficacion').modal('show'); }
    </script>
     <!--PARA CERRAR MODAL-->
    <script type="text/javascript">
        function closeModal() { $('#modalnotoficacion').modal('hide'); }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    
    <div class="row page-titles">
        <div class="col-md-5 align-self-center">
            <h4 class="text-themecolor">Completar Notificación de ATMs</h4>
        </div>
        <div class="col-md-7 align-self-center text-right">
            <div class="d-flex justify-content-end align-items-center">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Inicio</a></li>
                    <li class="breadcrumb-item active">Completar Notificación de ATMs</li>
                </ol>

            </div>
        </div>
    </div>

    <div class="card">
        <br />
        <div class="row col-12" style="margin-left: 10px; margin-left: 10px;">
            <h3 class="text-themecolor" style="color: #808080;"><i class="fa fa-book"></i>Datos Generales</h3>
        </div>
        <br />
         <asp:UpdatePanel runat="server" ID="UpNotif" UpdateMode="Conditional">
            <ContentTemplate>
        <div class="row col-12" style="margin: 10px 10px 10px 10px">
           
            <!--PRIMERA FILA-->
             <div class="row col-12">
                <div class="row col-6">
                    <label class="col-form-label col-6">Fecha de mantenimiento</label>
                    <div class="row col-12">
                        <asp:TextBox ID="txtFechaInicio" OnTextChanged="txtFechaInicio_TextChanged" AutoPostBack="true" placeholder="1900-12-31" CssClass="form-control col-12" runat="server" TextMode="Date"></asp:TextBox>
                    </div>
                </div>  
                  <div class="row col-6">
                    <label class="col-form-label col-6">Realizar mantenimiento</label>
                    <div class="row col-12">
                       <asp:DropDownList runat="server" ID="DDLrealizarMant" Enabled="false" OnSelectedIndexChanged="DDLrealizarMant_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control col-12">
                           <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                           <asp:ListItem Value="2" Text="No"></asp:ListItem>
                       </asp:DropDownList>
                    </div>
                </div> 
            </div>
           
            <!--/PRIMERA FILA-->
            <!--SEGUNDA FILA-->
            <div runat="server" class="row col-12" Visible="false" id="DivCancelaNotif">               
                    <label class="col-form-label col-6">Motivo de cancelación</label>
                    <div class="row col-12">
                        <asp:TextBox ID="txtcancelarNotif" placeholder="Ingrese su motivo de cancelación..." CssClass="form-control col-12" runat="server" TextMode="MultiLine" Rows="2"></asp:TextBox>
                    </div>                 
                </div>
            <!--/SEGUNDA FILA-->
            <!--TERCERA FILA-->
            <div class="row col-12">
                <div class="row col-6">
                    <label class="col col-form-label col-6">Hora Inicio de mantenimiento</label>
                    <div class="row col-12">
                        <asp:TextBox ID="txtHrInicioMant" placeholder="00:00:00" class="form-control" runat="server" TextMode="Time"></asp:TextBox>
                    </div>
                </div>
                <div class="row col-6">
                    <label class="col col-form-label col-6">Hora finaliza mantenimiento</label>
                    <div class="row col-12">
                        <asp:TextBox ID="txtHrFinMant" placeholder="00:00:00" class="form-control" runat="server" TextMode="Time"></asp:TextBox>
                    </div>
                </div>
            </div>
            <!--FIN TERCERA FILA-->
            <!--CUARTA FILA-->
            <div class="row col-12">              
                <div class="row col-6">
                    <label class="col-form-label col-6">SysAid</label>
                    <div class="row col-12">
                        <asp:TextBox CssClass="form-control" ID="txtsysaid" runat="server"></asp:TextBox>
                    </div>
                </div>
                 <div class="row col-6">
                    <label class="col-form-label col-6">ATM</label>
                    <div class="row col-12">
                       <asp:DropDownList runat="server" ID="DDLmantemientoPendiente" AutoPostBack="true" OnSelectedIndexChanged="DDLmantemientoPendiente_SelectedIndexChanged"  CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <!--/CUARTA FILA-->
            <!--QUINTA FILA-->
            <div class="row col-12">
                 <div class="row col-6">
                    <label class="col-form-label col-6">Código de ATM</label>
                    <div class="row col-12">
                        <asp:TextBox CssClass="form-control" ID="txtcodATMNotif" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                </div>  
                <div class="row col-6">
                    <label class="col-form-label col-6">Ubicación de ATM</label>
                    <div class="row col-12">
                        <asp:TextBox CssClass="form-control" ID="txtUbicacionATM" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                </div>              
            </div>
            <!--/QUINTA FILA-->
            <!--SEXTA FILA-->
            <div class="row col-12">
                <div class="row col-6">
                    <label class="col-form-label col-6">Dirección</label>
                    <div class="row col-12">
                        <asp:TextBox CssClass="form-control" ID="txtdireccion" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="row col-6">
                    <label class="col-form-label col-6">Sucursal</label>
                    <div class="row col-12">
                        <asp:TextBox CssClass="form-control" ID="txtsucursalNotif" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                </div>
            </div>
            <!--/SEXTA FILA-->
            <!--SEPTIMA FILA-->
            <div class="row col-12">
                <div class="row col-6">
                    <label class="col-form-label col-6">Dirección IP</label>
                    <div class="row col-12">
                        <asp:TextBox CssClass="form-control" ID="txtipNotif" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="row col-6">
                    <label class="col-form-label col-6">Zona</label>
                    <div class="row col-12">
                        <asp:TextBox CssClass="form-control" ID="txtzonaNotif" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                </div>
            </div>
            <!--/SEPTIMA FILA-->
            <!--OCTAVA FILA-->
            <div class="row col-12">
                <div class="row col-6">
                <label class="col-form-label col-6">Impacto</label>
                <div class="row col-12">
                    <asp:TextBox CssClass="form-control" ID="txtimpacto" runat="server" Enabled="false" TextMode="MultiLine" Rows="2" Text="Durante la ventana de mantenimiento el ATM estará fuera de linea"></asp:TextBox>
                </div>
                    </div>
                 <div class="row col-6">
                    <label class="col-form-label col-6">Motivo</label>
                    <div class="row col-12">
                        <asp:TextBox CssClass="form-control" ID="txtmotivoNotif" runat="server" Enabled="false" TextMode="MultiLine" Rows="2" Text="Realizar acciones pro activas para prevenir la falla"></asp:TextBox>
                    </div>
                </div>
            </div>
            <!--/OCTAVA FILA-->
        </div>
        <br />
        <br />
        <div class="row col-12" style="margin-left: 10px; margin-left: 10px;">
            <h3 class="text-themecolor" style="color: #808080;"><i class="fa fa-user" style="margin-left: 10px"></i>Personal Encargado</h3>
        </div>
        <br />
       
                <div class="row col-12" style="margin: 10px 10px 10px 10px">
                    <!--PRIMERA FILA-->
                    <div class="row col-12">
                        <div class="row col-6">
                            <label class="col-form-label col-6">Técnico responsable</label>
                            <div class="row col-12">
                                <asp:DropDownList ID="DLLtecResponsable" AutoPostBack="true" OnTextChanged="DLLtecResponsable_TextChanged" CssClass="form-control" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="row col-6">
                            <label class=" col-form-label col-6">Identidad</label>
                            <div class="row col-12">
                                <asp:TextBox CssClass="form-control" ID="txtidentidadTecResponsable" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
           
        <!--/PRIMERA FILA-->
        <!--SEGUNDA FILA-->
       <%-- <asp:UpdatePanel ID="UpdateGridView" runat="server">
            <ContentTemplate>--%>
                <div class="row col-12" style="margin: 10px 10px 10px 10px">
                    <div class="row col-12">

                        <label class="col-form-label col-12">Técnicos Participantes</label>
                        <div class="row col-12">
                            <asp:DropDownList ID="DLLTecnicoParticipante" AutoPostBack="true" OnTextChanged="DLLTecnicoParticiante_TextChanged" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                        </div>
                        
                        </div>
                       

                        <div class="row col-12">
                            <div class="table-responsive">

                                <!--<table id="bootstrap-data-table" class="table table-striped table-bordered"> -->
                                <asp:GridView ID="GVBusqueda" runat="server"
                                    CssClass="table table-bordered"
                                    PagerStyle-CssClass="pgr"
                                    HeaderStyle-CssClass="table"
                                    RowStyle-CssClass="rows"
                                    AutoGenerateColumns="false"
                                    AllowPaging="true"
                                    GridLines="None"
                                    PageSize="10"
                                    style="margin: 30px 0px 20px 0px">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-Width="60px">
                                            <ItemTemplate>
                                                <!--<button id="btnBorrarGrid" class="btn btn-danger waves-effect waves-light" type="button"><span class="btn-label"><i class="fa fa-trash"></i></span></button>
                                                <!-- <button type="button" class="btn btn-rounded btn-block btn-danger btn-sm"><i class="fa fa-minus-circle"></i></button>-->
                                                           <asp:Button ID="Btnseleccionar" OnClick="Btnseleccionar_Click" Enabled="true" runat="server" Text="Eliminar" class="btn btn-danger mr-2" />
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="idUsuario" HeaderText="Usuario" ItemStyle-Width="30%" />
                                        <asp:BoundField DataField="nombre" HeaderText="Nombre" ItemStyle-Width="30%" />
                                        <asp:BoundField DataField="identidad" HeaderText="Identidad" ItemStyle-Width="30%" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    

                </div>


          <%--  </ContentTemplate>
        </asp:UpdatePanel>--%>
        <!--/SEGUNDA FILA-->

                <!--TERCERA FILA-->
       <%-- <asp:UpdatePanel ID="UpdateGridView" runat="server">
            <ContentTemplate>--%>
                <div class="row col-12" style="margin: 10px 10px 10px 10px">
                    <asp:UpdatePanel ID="UpdateTest" UpdateMode="Always" runat="server">
                        <ContentTemplate>
                            <div runat="server" class="row col-12" id="DIVBuscarJefes" visible="false">
                                <asp:Label runat="server" Visible="false" class="col-form-label col-12" ID="lbJefeAgencia">Jefes de agencias</asp:Label>
                                <div class="row col-12">
                                    <asp:TextBox runat="server" UseSubmitBehavior="False" ID="txtbuscarJefeNotif" OnTextChanged="txtbuscarJefeNotif_TextChanged" CssClass="form-control col-6"></asp:TextBox>
                                    <asp:Button runat="server" ID="btnBuscarJefe" OnClick="btnBuscarJefe_Click" CssClass="btn btn-info mr-2" Text="Buscar" />
                                </div>
                            </div>
                            <br />
                            <label class="row col-12" runat="server" id="lbSelectJefeAge" visible="false">Seleccionar los jefes de agencia</label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <!--GRID CON TODO PERSONAL BANCATLAN DEL ACTIVE DIRECTORY-->
                     <div class="row col-12">
                            <div class="table-responsive">

                                <asp:UpdatePanel id="UpdatePanel2" UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                                       
                                    
                                <!--<table id="bootstrap-data-table" class="table table-striped table-bordered"> -->
                                <asp:GridView ID="GVJefesAD" runat="server"
                                    CssClass="table table-bordered"
                                    PagerStyle-CssClass="pgr"
                                    HeaderStyle-CssClass="table"
                                    RowStyle-CssClass="rows"
                                    AutoGenerateColumns="false"
                                    AllowPaging="true"
                                    GridLines="None" OnRowCommand="GVJefesAD_RowCommand"
                                    PageSize="10" OnPageIndexChanging="GVJefesAD_PageIndexChanging"
                                    style="margin: 30px 0px 20px 0px">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-Width="60px">
                                               <ItemTemplate>                                                               
                                                       <asp:LinkButton runat="server" ID="btnCorreoJefe" Text="Seleccionar" CssClass="btn btn-info mr-2" CommandArgument='<%# Eval("mail") %>' CommandName="Aprobar"></asp:LinkButton>
                                                                <%-- <asp:Button ID="BtnUsuarioModificar" runat="server" Text="Modificar" CssClass="btn btn-rounded btn-block btn-success" CommandArgument='<%# Eval("codATM") %>' CommandName="Modificar" />--%>
                                               </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="givenName" HeaderText="Nombre" />
                                        <asp:BoundField DataField="sn" HeaderText="Apellido"  />
                                        <asp:BoundField DataField="mail" HeaderText="Correo" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                      
                    <!--GRID QUE ES LLENADA POR SELECCION-->
                        <div class="row col-12">
                            
                            <div class="table-responsive">

                                <!--<table id="bootstrap-data-table" class="table table-striped table-bordered"> -->
                                <asp:GridView ID="GVjefesAgencias" runat="server"
                                    CssClass="table table-bordered"
                                    PagerStyle-CssClass="pgr"
                                    HeaderStyle-CssClass="table"
                                    RowStyle-CssClass="rows"
                                    AutoGenerateColumns="true"
                                    AllowPaging="true"
                                    GridLines="None"
                                    PageSize="10"
                                    style="margin: 30px 0px 20px 0px">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-Width="60px">
                                            <ItemTemplate>
                                                <!--<button id="btnBorrarGrid" class="btn btn-danger waves-effect waves-light" type="button"><span class="btn-label"><i class="fa fa-trash"></i></span></button>
                                                <!-- <button type="button" class="btn btn-rounded btn-block btn-danger btn-sm"><i class="fa fa-minus-circle"></i></button>-->
                                                           <asp:Button ID="BtnseleccionarJefes" Enabled="true" runat="server" Text="Eliminar" class="btn btn-danger mr-2" />
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                                                               
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    

                </div>

 </ContentTemplate>
                                </asp:UpdatePanel>
          <%--  </ContentTemplate>
        </asp:UpdatePanel>--%>
        <!--/TERCERA FILA-->
                
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="col-md-4 align-self-center" style="margin-left:auto; margin-right:auto">
                    <asp:Button runat="server" ID="btnEnviarNotificacion" OnClick="btnEnviarNotificacion_Click" CssClass="btn btn-rounded btn-block btn-outline-success" class="btn btn-info d-none d-lg-block m-l-15" Text="Enviar" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <br />
                </ContentTemplate>
             

        </asp:UpdatePanel>
        <!-- Modal asegurar notificacion -->
        <div class="modal bs-example-modal-lg" id="modalnotoficacion" tabindex="-1" data-backdrop="static" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" style="display: none;">
            <div class="modal-dialog modal-xl">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title" id="myLargeModalLabel">¿Seguro que creará notificación?</h4>                       
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel1" Runat="server">
            <ContentTemplate>
                 <div class="row col-12">
                        <asp:label runat="server" BorderStyle="None" class="col form-control col-6"><strong>Fecha mantenimiento: </strong></asp:label>
                        <asp:label runat="server" BorderStyle="None" ID="lbFechaInicia" class="col form-control col-6"></asp:label>
                    </div>              
                    <div class="row col-12">
                        <asp:label runat="server" BorderStyle="None" class="col form-control col-6"><strong>Código de ATM: </strong></asp:label>
                        <asp:label runat="server" BorderStyle="None" ID="lbcodATM" class="col form-control col-6"></asp:label>
                    </div>
                 <div class="row col-12">
                        <asp:label runat="server" BorderStyle="None" class="col form-control col-6"><strong>Nombre de ATM: </strong></asp:label>
                        <asp:label runat="server" BorderStyle="None" ID="lbNombreATM" class="col form-control col-6"></asp:label>
                    </div>
                <div class="row col-12">
                        <asp:label runat="server" BorderStyle="None" class="col form-control col-6"><strong>Sucursal de ATM: </strong></asp:label>
                        <asp:label runat="server" BorderStyle="None" ID="lbsucursalATM" class="col form-control col-6"></asp:label>
                    </div>
                <div class="row col-12">
                        <asp:label runat="server" BorderStyle="None" class="col form-control col-6"><strong>Técnico Responsable: </strong>  </asp:label>
                        <asp:label runat="server" BorderStyle="None" ID="lbTecnicoResp" class="col form-control col-6"></asp:label>
                    </div>
                  <div class="row col-12">
                        <asp:label runat="server" borderstyle="none" class="col form-control col-6"><strong>tiempo de mantenimiento: </strong></asp:label>
                        <asp:label runat="server" borderstyle="none" id="lbHrMantenimiento" class="col form-control col-6"></asp:label>
                    </div>
                                    
                <div class="col-md-6 align-self-center" style="margin-left:auto; margin-right:auto">
                    <asp:label runat="server" style="color:red;" Visible="false" borderstyle="none" ID="lbNoNotif" class="col form-control col-6" ><strong>No se ha podido guardar la notificación </strong></asp:label>
                    </div>
                </ContentTemplate>
                         </asp:UpdatePanel>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <div class="modal-footer col-12">
                                <div class="row col-6">
                                <asp:Button runat="server" ID="btnModalEnviarNotificacion" OnClick="btnModalEnviarNotificacion_Click" CssClass="btn btn-rounded btn-block btn-outline-success" Text="Enviar" />
                                </div>
                                 <div class="row col-6">
                                <asp:Button runat="server" ID="btnModalCerrarNotificacion" OnClick="btnModalCerrarNotificacion_Click" CssClass="btn btn-rounded btn-block btn-outline-danger" Text="Cancelar" />
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

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>