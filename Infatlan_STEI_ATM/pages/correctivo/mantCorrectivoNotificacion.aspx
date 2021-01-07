<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="mantCorrectivoNotificacion.aspx.cs" Inherits="Infatlan_STEI_ATM.pages.correctivo.mantCorrectivoNotificacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <!--PARA LLAMAR MODAL-->
    <script type="text/javascript">
        function openModal() { $('#modalNoti').modal('show'); }
        function closeModal() { $('#modalNoti').modal('hide'); }
    </script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #ffffff; opacity: 0.7; margin: 0;">
                <span style="display: inline-block; height: 100%; vertical-align: middle;"></span>
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="../../assets/images/loading.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="display: inline-block; vertical-align: middle;" />
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
                    <li class="breadcrumb-item"><a href="javascript:void(0)">ATM</a></li>
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Mantenimiento Correctivo</a></li>
                    <li class="breadcrumb-item active">Notificación</li>
                </ol>
            </div>
        </div>
    </div>

    <div class="card">
        <div class="card-body">
            <h4 class="card-title">Notificaciones</h4>
            <h6 class="card-subtitle">Creación de notificaciones de mantenimiento correctivo de ATM</h6>
            <div class="row col-12">
                <div class="col-12 grid-margin stretch-card">
                    <asp:UpdatePanel runat="server" ID="UpNotif">
                        <ContentTemplate>
                            <h5 class="card-subtitle m-t-10"><i class="fa fa-book"></i><b> DATOS GENERALES</b></h5>
                            <!--PRIMERA FILA-->
                            <div class="row col-12 align-self-center" style="margin-left: auto; margin-right: auto">
                                <div class="row col-12 align-self-center" style="margin-left: auto; margin-right: auto">
                                    <label class="col-form-label">ATM en mantenimiento correctivo</label>
                                    <asp:TextBox ID="txtATM" CssClass="form-control col-12" Enabled="false" runat="server"></asp:TextBox>
                                </div>
                                
                            </div>
                            <!--/PRIMERA FILA-->
                           
                            <!--SEGUNDA FILA-->
                            <div class="row col-12 align-self-center" style="margin-left: auto; margin-right: auto">
                                <div class="row col-6 align-self-center" style="margin-left: auto; margin-right: auto">
                                    <label class="col-form-label">Hora Inicio de mantenimiento</label>
                                    <asp:TextBox ID="txtHrInicioMant" placeholder="00:00:00" CssClass="form-control" runat="server" TextMode="Time"></asp:TextBox>
                                </div>
                                <div class="row col-6 align-self-center" style="margin-left: auto; margin-right: auto">
                                    <label class="col-form-label">Hora finaliza mantenimiento</label>
                                    <asp:TextBox ID="txtHrFinMant" placeholder="00:00:00" CssClass="form-control" runat="server" TextMode="Time"></asp:TextBox>
                                </div>
                            </div>
                            <!--FIN SEGUNDA FILA-->
                            <!--CUARTA FILA-->
                            <div class="row col-12 align-self-center" style="margin-left: auto; margin-right: auto">
                                <div class="row col-6 align-self-center" style="margin-left: auto; margin-right: auto">
                                    <label class="col-form-label">SysAid</label>
                                    <asp:TextBox CssClass="form-control" ID="txtsysaid" runat="server"></asp:TextBox>
                                </div>
                              <div class="row col-6 align-self-center" style="margin-left: auto; margin-right: auto">
                                    <label class="col-form-label">Ubicación de ATM</label>
                                    <asp:TextBox CssClass="form-control" ID="txtUbicacionATM" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <!--/CUARTA FILA-->
                           
                            <!--SEXTA FILA-->
                            <div class="row col-12 align-self-center" style="margin-left: auto; margin-right: auto">
                                <div class="row col-6 align-self-center" style="margin-left: auto; margin-right: auto">
                                    <label class="col-form-label">Dirección</label>
                                    <asp:TextBox CssClass="form-control" ID="txtdireccion" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="row col-6 align-self-center" style="margin-left: auto; margin-right: auto">
                                    <label class="col-form-label">Sucursal</label>
                                    <asp:TextBox CssClass="form-control" ID="txtsucursalNotif" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <!--/SEXTA FILA-->
                            <!--SEPTIMA FILA-->
                            <div class="row col-12 align-self-center" style="margin-left: auto; margin-right: auto">
                                <div class="row col-6 align-self-center" style="margin-left: auto; margin-right: auto">
                                    <label class="col-form-label">Dirección IP</label>
                                    <asp:TextBox CssClass="form-control" ID="txtipNotif" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="row col-6 align-self-center" style="margin-left: auto; margin-right: auto">
                                    <label class="col-form-label">Zona</label>
                                    <asp:TextBox CssClass="form-control" ID="txtzonaNotif" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <!--/SEPTIMA FILA-->
                            <!--OCTAVA FILA-->
                            <div class="row col-12 align-self-center" style="margin-left: auto; margin-right: auto">
                                <div class="row col-6 align-self-center" style="margin-left: auto; margin-right: auto">
                                    <label class="col-form-label">Impacto</label>
                                    <asp:TextBox CssClass="form-control" ID="txtimpacto" runat="server" Enabled="false" TextMode="MultiLine" Rows="2" Text="Durante la ventana de mantenimiento el ATM estará fuera de linea"></asp:TextBox>
                                </div>
                                <div class="row col-6 align-self-center" style="margin-left: auto; margin-right: auto">
                                    <label class="col-form-label">Motivo</label>
                                    <asp:TextBox CssClass="form-control" ID="txtmotivoNotif" runat="server" Enabled="false" TextMode="MultiLine" Rows="2" Text="Realizar acciones pro activas para prevenir la falla"></asp:TextBox>
                                </div>
                            </div>
                            <!--/OCTAVA FILA-->

                            <h5 class="card-subtitle m-t-20"><i class="fa fa-user"></i><b> PERSONAL ENCARGADO</b></h5>
                            <!--PRIMERA FILA-->
                            <div class="row col-12 align-self-center" style="margin-left: auto; margin-right: auto">
                                <div class="row col-6 align-self-center" style="margin-left: auto; margin-right: auto">
                                    <label class="col-form-label">Técnico responsable</label>
                                    <asp:DropDownList ID="DLLtecResponsable" AutoPostBack="true" OnTextChanged="DLLtecResponsable_TextChanged" CssClass="form-control" runat="server"></asp:DropDownList>
                                </div>

                                <div class="col-6">
                                    <label class=" col-form-label">Identidad</label>
                                    <asp:TextBox CssClass="form-control" ID="txtidentidadTecResponsable" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div runat="server" class="row col-12 align-self-center" style="margin-left: auto; margin-right: auto" id="DIVBuscarJefes" visible="false">
                                <h5 class="card-subtitle m-t-20 col-12" runat="server" id="H3JefeAgencia"><i class="fa fa-user"></i><b>ENCARGADO DE AGENCIA</b></h5><br /><br />
                                <!--TERCERA FILA-->
                                <asp:UpdatePanel ID="UpdateTest" UpdateMode="Always" runat="server">
                                    <ContentTemplate>                            
                                                    <div class="col-12 grid-margin stretch-card">
                                                            <div runat="server" class="row col-12 align-self-center" style="margin-left: auto; margin-right: auto" >
                                                                <asp:Label runat="server" Visible="false" class="col-form-label col-12" ID="lbJefeAgencia">Jefes de agencias</asp:Label>
                                                                <asp:TextBox runat="server" UseSubmitBehavior="False" ID="txtbuscarJefeNotif" CssClass="form-control col-8"></asp:TextBox>
                                                                <asp:LinkButton runat="server" ID="btnBuscarJefe" OnClick="btnBuscarJefe_Click" CssClass="btn btn-info mr-2"><i class="fa fa-search"></i></asp:LinkButton>
                                                                <label class="row col-12" runat="server" id="lbSelectJefeAge">Seleccionar los jefes de agencia</label>
                                                            </div>
                                                    </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-12 grid-margin stretch-card">
                                            <div class="card">
                                                <!--GRID CON TODO PERSONAL BANCATLAN DEL ACTIVE DIRECTORY-->
                                                <div class="row col-11 align-self-center" style="margin-left: auto; margin-right: auto">
                                                    <div class="table-responsive">
                                                        <asp:GridView ID="GVJefesAD" runat="server"
                                                            CssClass="table table-bordered"
                                                            PagerStyle-CssClass="pgr"
                                                            HeaderStyle-CssClass="table"
                                                            RowStyle-CssClass="rows"
                                                            AutoGenerateColumns="false"
                                                            AllowPaging="true"
                                                            GridLines="None" OnRowCommand="GVJefesAD_RowCommand"
                                                            PageSize="10" OnPageIndexChanging="GVJefesAD_PageIndexChanging"
                                                            Style="margin: 30px 0px 20px 0px">
                                                            <Columns>
                                                                <asp:TemplateField HeaderStyle-Width="60px">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton runat="server" ID="btnCorreoJefe" Text="" CssClass="btn btn-info fa fa-plus mr-2" CommandArgument='<%# Eval("mail") %>' CommandName="correos"></asp:LinkButton>
                                                                        <%-- <asp:Button ID="BtnUsuarioModificar" runat="server" Text="Modificar" CssClass="btn btn-rounded btn-block btn-success" CommandArgument='<%# Eval("codATM") %>' CommandName="Modificar" />--%>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="10%" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="givenName" HeaderText="Nombre" />
                                                                <asp:BoundField DataField="sn" HeaderText="Apellido" />
                                                                <asp:BoundField DataField="mail" HeaderText="Correo" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                                <!--GRID CON TODO PERSONAL BANCATLAN DEL ACTIVE DIRECTORY-->

                                                <!--LLENA JEFES-->
                                                <div class="row col-11 align-self-center" style="margin-left: auto; margin-right: auto">
                                                    <div class="table-responsive">
                                                        <asp:UpdatePanel runat="server" ID="UPJefes">
                                                            <ContentTemplate>
                                                                <asp:GridView ID="GVLlenaJefeApruebaNotif" runat="server"
                                                                    CssClass="table table-bordered"
                                                                    PagerStyle-CssClass="pgr"
                                                                    HeaderStyle-CssClass="table"
                                                                    RowStyle-CssClass="rows"
                                                                    AutoGenerateColumns="false"
                                                                    AllowPaging="true"
                                                                    GridLines="None"
                                                                    PageSize="10"
                                                                    Style="margin: 30px 0px 20px 0px">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="Correo" HeaderText="Correo" ItemStyle-Width="30%" />
                                                                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" ItemStyle-Width="30%" />
                                                                        <asp:BoundField DataField="Apellido" HeaderText="Apellido" ItemStyle-Width="30%" />
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                                <!--LLENA JEFES-->

                                                <!--GRID QUE ES LLENADA POR SELECCION-->
                                                <div class="row col-11 align-self-center" style="margin-left: auto; margin-right: auto">
                                                    <div class="table-responsive">
                                                        <asp:GridView ID="GVjefesAgencias" runat="server"
                                                            CssClass="table table-bordered"
                                                            PagerStyle-CssClass="pgr"
                                                            HeaderStyle-CssClass="table"
                                                            RowStyle-CssClass="rows"
                                                            AutoGenerateColumns="true"
                                                            AllowPaging="true"
                                                            GridLines="None"
                                                            PageSize="10" OnRowCommand="GVjefesAgencias_RowCommand"
                                                            Style="margin: 30px 0px 20px 0px">
                                                            <Columns>
                                                                <asp:TemplateField HeaderStyle-Width="60px">
                                                                    <ItemTemplate>
                                                                        <!--<button id="btnBorrarGrid" class="btn btn-danger waves-effect waves-light" type="button"><span class="btn-label"><i class="fa fa-trash"></i></span></button>
                                <!-- <button type="button" class="btn btn-rounded btn-block btn-danger btn-sm"><i class="fa fa-minus-circle"></i></button>-->
                                                                        <asp:LinkButton ID="btnEliminar" Enabled="true" runat="server" Text="" class="btn btn-danger mr-2" CommandArgument='<%# Eval("Correo") %>' CommandName="eliminar"><i class="icon-trash"></i></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="10%" />
                                                                </asp:TemplateField>

                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                                <!--GRID QUE ES LLENADA POR SELECCION-->
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <!--/TERCERA FILA-->
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <div class="row">
                        <div class="col-12">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <div class="col-6">
                                        <asp:Button runat="server" ID="btnEnviarNotificacion" OnClick="btnEnviarNotificacion_Click" CssClass="btn btn-success" Text="Enviar notificación" />                                       
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal asegurar notificacion -->
    <div class="modal fade bs-example-modal-lg" id="modalNoti" tabindex="-1" data-backdrop="static" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myLargeModalLabel">¿Seguro que creará notificación?</h4>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                       
                        <div class="row col-12">
                            <asp:Label runat="server" BorderStyle="None" class="col form-control col-4"><strong>ATM: </strong></asp:Label>
                            <asp:Label runat="server" BorderStyle="None" ID="lbcodATM" class="col form-control col-8"></asp:Label>
                        </div>                     
                        <div class="row col-12">
                            <asp:Label runat="server" BorderStyle="None" class="col form-control col-4"><strong>Sucursal de ATM: </strong></asp:Label>
                            <asp:Label runat="server" BorderStyle="None" ID="lbsucursalATM" class="col form-control col-8"></asp:Label>
                        </div>
                        <div class="row col-12">
                            <asp:Label runat="server" BorderStyle="None" class="col form-control col-4"><strong>Responsable: </strong>  </asp:Label>
                            <asp:Label runat="server" BorderStyle="None" ID="lbTecnicoResp" class="col form-control col-8"></asp:Label>
                        </div>
                        <div class="row col-12">
                            <asp:Label runat="server" BorderStyle="none" class="col form-control col-4"><strong>Duración: </strong></asp:Label>
                            <asp:Label runat="server" BorderStyle="none" ID="lbHrMantenimiento" class="col form-control col-8"></asp:Label>
                        </div>

                        <div class="col-md-6 align-self-center" style="margin-left: auto; margin-right: auto">
                            <asp:Label runat="server" Style="color: red;" Visible="false" BorderStyle="none" ID="lbNoNotif" class="col form-control col-6"><strong>No se ha podido guardar la notificación </strong></asp:Label>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <div class="modal-footer col-12">
                           <asp:Button runat="server" ID="btnModalCerrarNotificacion" OnClick="btnModalCerrarNotificacion_Click" CssClass="btn btn-secundary  mr-3" Text="Cancelar" />
                                <asp:Button runat="server" ID="btnModalEnviarNotificacion" OnClick="btnModalEnviarNotificacion_Click" CssClass="btn btn-success  mr-3" Text="Enviar" />
                            
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
            <!-- /.modal-content -->
        </div>
        <!--/.modal-dialog -->
    </div>
    <!-- /asegurar notificacion -->

  


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
