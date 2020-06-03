<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="agenciaTipo.aspx.cs" Inherits="Infatlan_STEI_Agencias.pages.configuraciones.agenciaTipo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <script type="text/javascript">

        var updateProgress = null;
        function postbackButtonClick() {
            updateProgress = $find("<%= UpdateProgress1.ClientID %>");
            window.setTimeout("updateProgress.set_visible(true)", updateProgress.get_displayAfter());
            return true;
        }

        function openModalModificarTipoAgencia() { $('#modalModificarTipoAgencia').modal('show'); }
        function closeModalModificarTipoAgencia() { $('#modalModificarTipoAgencia').modal('hide'); }
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
        <div class="col-md-7 align-self-center">
            <h2 class="text-themecolor">Tipos de Agencia</h2>
            <div class="mr-md-3 mr-xl-5">
                <p class="mb-md-0">Soporte Técnico y Comunicaciones</p>
            </div>
        </div>
    </div>

    <div class="card">
        <div class="card-body">
            <h4 class="card-title">Categorías de agencias</h4>
            <p>Tipo de agencias que se le da mantenimiento preventivo</p>

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
                                    <label class="control-label text-danger">*</label><label class="control-label ">Tipo de Agencia:</label>
                                </div>
                                <div class="col-md-10">
                                    <asp:TextBox runat="server" ID="TxAgencia" TextMode="MultiLine" Rows="3" MaxLength="250" CssClass="form-control" placeholder="Ingrese tipo de agencia.."></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <div class="col-md-12"  style="text-align:center">
                                <label class="control-label text-danger" style="text-align:center" >Los campos con (*) son obligatorios</label>
                            </div>

                            <div class="row p-t-20 col-md-12">
                                <div class="col-md-4">
                                    <asp:Button ID="BtnEnviar" class="btn  btn-block btn-success" runat="server" Text="Enviar" OnClick="BtnEnviar_Click" />
                                </div>
                                <div class="col-md-4">
                                    <asp:Button ID="BtnCancelar" class="btn  btn-block btn-danger" runat="server" Text="Cancelar" OnClick="BtnCancelar_Click" />
                                </div>

                                <div class="col-md-4">
                                    <a href="../../default.aspx"" class="btn  btn-block btn-primary">Volver</a>
                                </div>

                            </div>


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
                                                <label class="control-label">Buscar:</label></label>                                      
                                            </div>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="TxBuscarArea" runat="server" placeholder="Búsqueda por Tipo Agencia o Id, luego presione Enter" class="form-control" AutoPostBack="true" OnTextChanged="TxBuscarArea_TextChanged"></asp:TextBox>
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
                                <asp:UpdatePanel runat="server" ID="UPTipoAgencias" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:GridView ID="GVTipoAgencias" runat="server"
                                            CssClass="table table-bordered"
                                            PagerStyle-CssClass="pgr"
                                            HeaderStyle-CssClass="table" HeaderStyle-HorizontalAlign="center"
                                            RowStyle-CssClass="rows"
                                            AutoGenerateColumns="false"
                                            AllowPaging="true"
                                            GridLines="None" OnPageIndexChanging="GVTipoAgencias_PageIndexChanging"
                                            PageSize="10" OnRowCommand="GVTipoAgencias_RowCommand">

                                            <Columns>
                                                <asp:TemplateField HeaderText="Acción" ItemStyle-HorizontalAlign="center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LbModificar" class="btn btn-info mr-2"  Title="Modificar" runat="server" CommandName="Modifcar" CommandArgument='<%# Eval("idTipoAgencia") %>'>
                                                        <i class="icon-pencil" ></i>
                                                        </asp:LinkButton>

                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="idTipoAgencia" HeaderText="Id" ControlStyle-Width="10%" />
                                                <asp:BoundField DataField="nombre" HeaderText="Tipo Agencia" ControlStyle-Width="50%" />
                                                <asp:BoundField DataField="estado" HeaderText="Estado" ControlStyle-Width="30%" />
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                        <div class="row p-t-20 col-md-12">
                            <div class="col-md-4 " style="margin-left: auto; margin-right: auto">
                                <a href="../../default.aspx"" class="btn  btn-block btn-primary">Volver</a>
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
    <div class="modal fade" id="modalModificarTipoAgencia" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="width: 600px; top: 320px; left: 50%; transform: translate(-50%, -50%);">
                <div class="modal-header bg-dark">

                    <h3 class="modal-title" style="color: white" id="exampleModalLabel">
                        <asp:Label ID="Titulo" runat="server" Text="Modificar Tipo de Agencia" Style="margin-left: auto; margin-right: auto"></asp:Label>
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
                                    <label class="col-md-3 col-form-label">Id Tipo Agencia:</label>
                                    <div class="col-md-9">
                                        <asp:TextBox ID="TxIdTipoAgenciaModal" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12" runat="server">
                                <div class="form-group row">
                                    <div class="col-md-3">
                                        <label class="control-label text-danger">*</label><label class="col-form-label">Tipo Agencia:</label>
                                    </div>
                                    <div class="col-md-9">
                                        <asp:TextBox ID="TxTipoAgenciaModal" class="form-control" MaxLength="250" runat="server" TextMode="MultiLine" Rows="3" OnTextChanged="TxTipoAgenciaModal_TextChanged"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12" runat="server">
                                <div class="form-group row">
                                    <div class="col-md-3">
                                        <label class="control-label text-danger">*</label><label class="col-form-label">Estado:</label>
                                    </div>
                                    <div class="col-md-9">
                                        <asp:DropDownList ID="DdlEstadoTipoAgencia" runat="server" class="form-control" AutoPostBack="true">
                                            <asp:ListItem Value="False" Text="Inactivo"></asp:ListItem>
                                            <asp:ListItem Value="True" Text="Activo"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12" style="text-align: center">
                                <label class="control-label" style="text-align:center; color:tomato">Los campos con (*) son obligatorios</label>
                            </div>


                            <asp:UpdatePanel ID="UpdateModal" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="col-md-12" runat="server" id="DivAlerta" visible="false" style="display: flex; background-color: tomato; justify-content: center">
                                        <asp:Label runat="server" CssClass="col-form-label text-white" ID="LbMensajeModalError"></asp:Label>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>





                            <%--                            <div class="col-md-12" style="margin-left: auto; margin-right: auto" runat="server" visible="true">
                                <div class="alert alert-success  alert-dismissible align-content-md-center" style="align-self: auto">
                                    <div class="row">
                                        <div class="col-3">                                           
                                            <p class="text-center"><img src="https://img.icons8.com/color/70/000000/accept-database.png"/><span class="dashicons dashicons-admin-home"></span></i></p>
                                        </div>
                                        <div class="col-9" style="text-align: center">
                                            <br>
                                            <h4><strong>¿Desea modificar el tipo de agencia?
                                                <asp:Label ID="Label2" runat="server" Text="" Style="margin-left: auto; margin-right: auto"></asp:Label>
                                            </strong></h4>
                                            <p>Si esta seguro dar clic en el botón "Enviar"</p>
                                        </div>

                                    </div>
                                </div>
                            </div>--%>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <button type="button" class="btn btn-light"
                                data-dismiss="modal">
                                Close</button>
                            <asp:Button ID="btnModalModificarTipoAgencia" runat="server" Text="Enviar" class="btn btn-dark" OnClick="btnModalModificarTipoAgencia_Click" />
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
