<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="tiposAreas.aspx.cs" Inherits="Infatlan_STEI_Agencias.pages.configuraciones.tiposAreas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">

    <script type="text/javascript">
        function openModalModificarArea() { $('#modalModificarArea').modal('show'); }
        function closeModalModificarArea() { $('#modalModificarArea').modal('hide'); }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">

    <div class="row page-titles">
        <div class="col-md-7 align-self-center">
            <h2 class="text-themecolor">Areas de Mantenimiento </h2>
            <div class="mr-md-3 mr-xl-5">
                <%-- <h2>Creación de Notificación</h2>--%>
                <p class="mb-md-0">Soporte Técnico y Comunicaciones</p>
            </div>
        </div>
    </div>

    <div class="card">
        <div class="card-body">
            <h4 class="card-title">Tipos de areas</h4>
            <p>Areas del banco donde se realizara mantenimiento</p>


            <ul class="nav nav-tabs" role="tablist">
                <li class="nav-item"><a class="nav-link active" data-toggle="tab" href="#home" role="tab"><span class="hidden-sm-up"><i class="fa fa-save"></i></span><span class="hidden-xs-down"> Guardar</span></a> </li>
                <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#profile" role="tab"><span class="hidden-sm-up"><i class="icon-pencil"></i></span><span class="hidden-xs-down"> Modificar</span></a> </li>
            </ul>

            <div class="tab-content tabcontent-border">
                <!--PRIMER CONTENIDO-->
                <div class="tab-pane active p-20" id="home" role="tabpanel">
                    <asp:UpdatePanel runat="server" ID="UPprimercontenido">
                        <ContentTemplate>
                            <div class="row p-t-20 col-md-12">
                                <div class="col-md-2  ">
                                    <label class="control-label text-danger">*</label><label class="control-label ">Area:</label>
                                </div>
                                <div class="col-md-10">
                                    <asp:TextBox runat="server" ID="TxArea" TextMode="MultiLine" Rows="3" CssClass="form-control" placeholder="Ingrese una area.."></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <br />
                            <div class="row p-t-20 col-md-12">
                                 <div class="col-md-4">
                                    <asp:Button ID="BtnEnviar" class="btn btn-block btn-success" runat="server" Text="Enviar" OnClick="BtnEnviar_Click"/>
                                </div>

                                 <div class="col-md-4">
                                    <asp:Button ID="BtnCancelar" class="btn btn-block  btn-danger " runat="server" Text="Cancelar"  OnClick="BtnCancelar_Click"/>
                                </div>

                                 <div class="col-md-4">
                    
                                  <a href="../../default.aspx"" class="btn  btn-block btn-primary">Volver</a>
                                </div>

                            </div>
                            <br />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <!--/PRIMER CONTENIDO-->

                                <!--SEGUNDO CONTENIDO-->
                <div class="tab-pane  p-20" id="profile" role="tabpanel">

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
                                       <asp:TextBox ID="TxBuscarArea" runat="server" placeholder="Búsqueda por area o id, luego presione Enter" class="form-control" AutoPostBack="true" OnTextChanged="TxBuscarArea_TextChanged"></asp:TextBox>
                                    </div>

                                </div>

                           </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>

                    <div class="row col-12">
                        <div class="col-12 grid-margin stretch-card">
                            <div class="table-responsive">
                                <asp:UpdatePanel runat="server" ID="UPAreas">
                                    <ContentTemplate>
                                        <asp:GridView ID="GVAreas" runat="server"
                                            CssClass="table table-bordered"
                                            PagerStyle-CssClass="pgr"
                                            HeaderStyle-CssClass="table" HeaderStyle-HorizontalAlign="center"
                                            RowStyle-CssClass="rows"
                                            AutoGenerateColumns="false"
                                            AllowPaging="true"
                                            GridLines="None" OnPageIndexChanging="GVAreas_PageIndexChanging"
                                            PageSize="10" OnRowCommand="GVAreas_RowCommand">

                                            <Columns>
                                                <asp:TemplateField HeaderText="Acción" ItemStyle-HorizontalAlign="center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LbModificar" class="btn btn-info mr-2" runat="server"  CommandName="Modifcar" CommandArgument='<%# Eval("idAreaAgencia") %>'>
                                                         <i class="icon-pencil" ></i>
                                                        </asp:LinkButton>    

                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="idAreaAgencia" HeaderText="Id" ControlStyle-Width="10%" />
                                                <asp:BoundField DataField="nombre" HeaderText="Area" ControlStyle-Width="50%" />
                                                <asp:BoundField DataField="estado" HeaderText="Estado" ControlStyle-Width="30%" />                                                                          
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
                <!--/SEGUNDO CONTENIDO-->


            </div>
        </div>
    </div>

    <%--MODALES--%>
    <%--INICIO MODAL --%>
    <div class="modal fade" id="modalModificarArea" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="width: 600px; top: 320px; left: 50%; transform: translate(-50%, -50%);">
                <div class="modal-header bg-dark">

                    <h3 class="modal-title" style="color: white" id="exampleModalLabel">
                        <asp:Label ID="Titulo" runat="server" Text="Modificar Area" Style="margin-left: auto; margin-right: auto"></asp:Label>
                    </h3>

                    <button type="button" class="close" style="color: white" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>

                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>

                            <div class="col-md-12" runat="server">
                                <div class="form-group row">
                                    <label class="col-md-3 col-form-label">Id Area:</label>
                                    <div class="col-md-9">
                                        <asp:TextBox ID="TxIdAreaModal" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12" runat="server">
                                <div class="form-group row">
                                    <label class="col-md-3 col-form-label">Area:</label>
                                    <div class="col-md-9">
                                        <asp:TextBox ID="TxAreaModal" class="form-control" runat="server"  TextMode="MultiLine" Rows="3"></asp:TextBox>
                                    </div>
                                </div>
                            </div>                          

                            <div class="col-md-12" runat="server">
                                <div class="form-group row">
                                    <label class="col-md-3 col-form-label">Estado:</label>
                                    <div class="col-md-9">
                                       <asp:DropDownList ID="DdlEstadoArea" runat="server" class="form-control" AutoPostBack="true">                                              
                                                <asp:ListItem Value="False" Text="Inactivo"></asp:ListItem>
                                                <asp:ListItem Value="True" Text="Activo"></asp:ListItem>                                                                                              
                                            </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                           
                            <div class="col-md-12" style="margin-left: auto; margin-right: auto" id="DivAprobarLV" runat="server" visible="true">
                                <div class="alert alert-success  alert-dismissible align-content-md-center" style="align-self: auto">
                                    <div class="row">
                                        <div class="col-3">
                                            <p class="text-center"><img src="https://img.icons8.com/color/70/000000/accept-database.png"/><span class="dashicons dashicons-admin-home"></span></i></p>
                                        </div>
                                        <div class="col-9" style="text-align: center">

                                            <h4><strong>¿Está seguro que desea modificar el area?
                                                <asp:Label ID="Label2" runat="server" Text="" Style="margin-left: auto; margin-right: auto"></asp:Label>
                                            </strong></h4>
                                            <p>Si esta seguro dar clic en el botón "Enviar"</p>
                                        </div>                              
                               
                                    </div>
                                </div>
                            </div>   
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <button type="button" class="btn btn-light"
                                data-dismiss="modal">
                                Close</button>
                            <asp:Button ID="btnModalModificarEstado" runat="server" Text="Enviar" class="btn btn-dark" OnClick="btnModalModificarEstado_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>


    </div>
    <%--FIN MODAL APROBAR LV--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
