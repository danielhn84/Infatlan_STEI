<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="creacionAgencia.aspx.cs" Inherits="Infatlan_STEI_Agencias.pages.configuraciones.creacionAgencia" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">

    <script type="text/javascript">
        var updateProgress = null;
        function postbackButtonClick() {
            updateProgress = $find("<%= UpdateProgress1.ClientID %>");
            window.setTimeout("updateProgress.set_visible(true)", updateProgress.get_displayAfter());
            return true;
        }

        function openModalModificarAgencia() { $('#modalModificarAgencia').modal('show'); }
        function closeModalModificarAgencia() { $('#modalModificarAgencia').modal('hide'); }
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
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Configuraciones</a></li>
                    <li class="breadcrumb-item active">Crear Agencia</li>
                </ol>
            </div> 
        </div>
    </div>

    <div class="card">
        <div class="card-body">
            <h4 class="card-title">Agencias</h4>
            <p>Información general de cada agencia</p>

            <ul class="nav nav-tabs" role="tablist">
                <li class="nav-item"><a class="nav-link active" data-toggle="tab" href="#home" role="tab"><span class="hidden-sm-up"><i class="fa fa-save"></i></span><span class="hidden-xs-down"> Guardar</span></a> </li>
                <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#profile" role="tab"><span class="hidden-sm-up"><i class="icon-pencil"></i></span><span class="hidden-xs-down"> Modificar</span></a> </li>
            </ul>
            <div class="tab-content tabcontent-border">

                <!--PRIMER CONTENIDO-->
                <div class="tab-pane active p-20" id="home" role="tabpanel">
                    <asp:UpdatePanel runat="server" ID="UPprimercontenido">
                        <ContentTemplate>
                            <!--Inicio Fila 1-->
                            <div class="row p-t-20">
                                <div class="col-md-6">
                                    <label class="control-label   text-danger">*</label><label class="control-label">Agencia:</label></label>
                                        <asp:TextBox ID="TxAgencia" AutoPostBack="true" runat="server" class="form-control" MaxLength="100"></asp:TextBox>
                                </div>

                                <div class="col-md-6">
                                    <label class="control-label   text-danger">*</label><label class="control-label">Código Agencia:</label></label>    
                                        <asp:TextBox ID="TxCodigo" AutoPostBack="true" runat="server" TextMode="Number" class="form-control" MaxLength="10"></asp:TextBox>
                                </div>
                            </div>
                            <!--Fin Fila 1-->

                            <!--Inicio Fila 2-->
                            <div class="row p-t-20">
                                <div class="col-md-6">
                                    <label class="control-label   text-danger">*</label><label class="control-label">Dirección:</label></label>
                                        <asp:TextBox ID="TxDireccion" AutoPostBack="true" runat="server" TextMode="MultiLine" Rows="3" class="form-control" MaxLength="500"></asp:TextBox>
                                </div>

                                <div class="col-md-6">
                                        <label class="control-label">Telefono:</label></label>    
                                        <asp:TextBox ID="TxTelefono" AutoPostBack="true" runat="server" TextMode="MultiLine" Rows="3" class="form-control" MaxLength="20"></asp:TextBox>
                                </div>
                            </div>
                            <!--Fin Fila 2-->

                            <!--Inicio Fila 3-->
                            <div class="row p-t-20">
                                <div class="col-md-6">
                                    <label class="control-label   text-danger">*</label><label class="control-label">Latitud:</label></label>
                                        <asp:TextBox ID="TxLatitud" AutoPostBack="true" runat="server" TextMode="MultiLine" Rows="3" class="form-control" MaxLength="20"></asp:TextBox>
                                </div>

                                <div class="col-md-6">
                                    <label class="control-label   text-danger">*</label><label class="control-label">Longitud:</label></label>    
                                        <asp:TextBox ID="TxLongitud" AutoPostBack="true" runat="server" TextMode="MultiLine" Rows="3" class="form-control" MaxLength="20"></asp:TextBox>
                                </div>
                            </div>
                            <!--Fin Fila 3-->

                            <!--Inicio Fila 4-->
                            <div class="row p-t-20">
                                <div class="col-md-6">
                                    <label class="control-label   text-danger">*</label><label class="control-label">Tipo Agencia:</label></label>
                                    <asp:DropDownList ID="DDLTipoAgencia" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                </div>

                                <div class="col-md-6">
                                    <label class="control-label   text-danger">*</label><label class="control-label">Departamento:</label></label>    
                                    <asp:DropDownList ID="DDLDepartamento" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="DDLDepartamento_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                            <!--Fin Fila 4-->
                            <br>
                            <!--Fin Fila 5-->
                            <div class="row p-t-20">
                                <div class="col-md-6">
                                    <label class="control-label   text-danger">*</label><label class="control-label">Requiere Conductor asignado por Contabilidad:</label></label>                                    
                            <%--    </div>

                                <div class="col-md-2">--%>
                                    <asp:RadioButtonList ID="RblConductor" RepeatDirection="Horizontal" Width="90px" runat="server" AutoPostBack="True" Enabled="false">
                                        <asp:ListItem Value="1">Si</asp:ListItem>
                                        <asp:ListItem Value="0">No</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>

                                <div class="col-md-6">
                                    <label class="control-label   text-danger">*</label><label class="control-label">Municipio:</label></label>                                    
                                <asp:DropDownList ID="DDLMunicipio" runat="server" class="form-control"></asp:DropDownList>
                                </div>



                            </div>
                            <!--Fin Fila 5-->

                            <br />
                            <br />
                            <div class="col-md-12" style="text-align: center">
                                <label class="control-label text-danger" style="text-align: center">Los campos con (*) son obligatorios</label>
                            </div>




                            <div class="row p-t-20 col-md-12">
                                <div class="col-md-4">
                                    <asp:Button ID="BtnEnviar" class="btn  btn-block btn-success" runat="server" Text="Enviar" OnClick="BtnEnviar_Click" />
                                </div>
                                <div class=" col-md-4">
                                    <asp:Button ID="BtnCancelar" class="btn  btn-block btn-danger" runat="server" Text="Cancelar" OnClick="BtnCancelar_Click" />
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
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
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

                    <div class="row col-12">
                        <div class="col-12 grid-margin stretch-card">
                            <div class="table-responsive">
                                <asp:UpdatePanel runat="server" ID="UPAgencias" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:GridView ID="GVAgencias" runat="server"
                                            CssClass="table table-bordered"
                                            PagerStyle-CssClass="pgr"
                                            HeaderStyle-CssClass="table" HeaderStyle-HorizontalAlign="center"
                                            RowStyle-CssClass="rows"
                                            AutoGenerateColumns="false"
                                            AllowPaging="true"
                                            GridLines="None"
                                            PageSize="10" OnPageIndexChanging="GVAgencias_PageIndexChanging" OnRowCommand="GVAgencias_RowCommand" >
                                            <Columns>
                                                <asp:TemplateField HeaderText="Acción" ItemStyle-HorizontalAlign="center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LbModificar"  class="btn btn-info mr-2"  runat="server"  CommandName="Modifcar" CommandArgument='<%# Eval("idAgencia") %>'>
                                                       <i class="icon-pencil" ></i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="idAgencia" HeaderText="Id" ControlStyle-Width="10%" />
                                                <asp:BoundField DataField="nombre" HeaderText="Agencia" ControlStyle-Width="50%" />
                                                <asp:BoundField DataField="codigoAgencia" HeaderText="Codigo" ControlStyle-Width="30%" />
                                                <asp:BoundField DataField="direccion" HeaderText="Direccion" ControlStyle-Width="30%" />
                                                <asp:BoundField DataField="telefono" HeaderText="Telefono" ControlStyle-Width="30%" />
                                                <asp:BoundField DataField="TipoAgencia" HeaderText="Tipo" ControlStyle-Width="30%" />
                                                <asp:BoundField DataField="departamento" HeaderText="Departamento" ControlStyle-Width="30%" />
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

    <%--INICIO MODAL ENVIAR LV--%>
    <div class="modal bs-example-modal-lg" id="modalModificarAgencia" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-xl" role="document">
            <div class="modal-content" style="width: 930px; top: 320px; left: 50%; transform: translate(-50%, -50%);">
                <div class="modal-header bg-dark">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <h3 class="modal-title" style="color: white">
                                <asp:Label ID="TituloModalCrearAgencia" runat="server" Text="" Style="margin-left: auto; margin-right: auto"></asp:Label></h3>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <button type="button" class="close" style="color: white" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>

                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                        <ContentTemplate>
                            <div class="col-md-12" runat="server">
                                <div class="form-group row">
                                    <label class="col-md-2 col-form-label">Codigo:</label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="TxCodigoModificar" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2  ">
                                        <label class="control-label text-danger">*</label><label class="col-form-label">Agencia:</label>
                                    </div>

                                    <div class="col-md-4">
                                        <asp:TextBox ID="TxAgenciaModificar" class="form-control" runat="server" OnTextChanged="TxAgenciaModificar_TextChanged" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12" runat="server">
                                <div class="form-group row">
                                    <div class="col-md-2  ">
                                        <label class="control-label text-danger">*</label><label class="col-form-label">Dirección:</label>
                                    </div>

                                    <div class="col-md-4">
                                        <asp:TextBox ID="TxDireccionModificar" class="form-control" runat="server" TextMode="MultiLine" Rows="2" MaxLength="500"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2  ">
                                        <label class="control-label text-danger">*</label><label class="col-form-label">Telefono:</label>
                                    </div>

                                    <div class="col-md-4">
                                        <asp:TextBox ID="TxTelefonoModificar" class="form-control" runat="server" TextMode="MultiLine" Rows="2" MaxLength="20"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12" runat="server">
                                <div class="form-group row">
                                    <div class="col-md-2  ">
                                        <label class="control-label text-danger">*</label><label class="col-form-label">Latitud:</label>
                                    </div>

                                    <div class="col-md-4">
                                        <asp:TextBox ID="TxLatitudModificar" class="form-control" runat="server" TextMode="MultiLine" Rows="2" MaxLength="20"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2  ">
                                        <label class="control-label text-danger">*</label><label class="col-form-label">Longitud:</label>
                                    </div>

                                    <div class="col-md-4">
                                        <asp:TextBox ID="TxLongitudModificar" class="form-control" runat="server" TextMode="MultiLine" Rows="2" MaxLength="20"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12" runat="server">
                                <div class="form-group row">
                                    <div class="col-md-2  ">
                                        <label class="control-label text-danger">*</label><label class="col-form-label">Tipo Agencia:</label>
                                    </div>

                                    <div class="col-md-4">
                                        <asp:DropDownList ID="DDLTipoAgenciaModificar" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                    </div>

                                    <div class="col-md-2">
                                        <label class="control-label text-danger">*</label><label class="col-form-label">Depto.:</label>
                                    </div>

                                    <div class="col-md-4">
                                        <asp:DropDownList ID="DDLDepartamentoModificar" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="DDLDepartamentoModificar_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12" runat="server">
                                <div class="form-group row">
                                    <div class="col-md-2">
                                        <label class="control-label text-danger">*</label><label class="col-form-label">Municipio:</label>
                                    </div>

                                    <div class="col-md-4">
                                        <asp:DropDownList ID="DDLMunicipioModificar" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                    </div>

                                    <div class="col-md-2  ">
                                        <label class="control-label text-danger">*</label><label class="col-form-label">Cod Ubicacion:</label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtcodUbicacion" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12" runat="server">
                                <div class="form-group row">

                                    <div class="col-md-3">
                                        <label class="control-label text-danger">*</label><label class="col-form-label">Requiere Conductor:</label>
                                    </div>

                                    <div class="col-md-3">
                                        <asp:RadioButtonList ID="RbConductorModificar" RepeatDirection="Horizontal" Width="90px" runat="server" AutoPostBack="True">
                                            <asp:ListItem Value="1">Si</asp:ListItem>
                                            <asp:ListItem Value="0">No</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>

                                    <div class="col-md-2  ">
                                        <label class="control-label text-danger">*</label><label class="col-form-label">Estado:</label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="DDLEstado" runat="server" AutoPostBack="true" CssClass="form-control">
                                            <asp:ListItem Value="0" Text="Inactivo"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Activo"></asp:ListItem>

                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>



                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <div class="col-md-12" style="text-align: center">
                        <label class="control-label text-danger" style="text-align: center">Los campos con (*) son obligatorios</label>
                    </div>


<%--                    <asp:UpdatePanel ID="UpdateModal" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="col-md-12" style="align-self: center" runat="server" id="DivAlerta" visible="false">
                                <div class="alert alert-danger   align-content-md-center">
                                    <h3 class="text-danger" style="text-align: center"><i class="fa fa-exclamation-triangle"></i>Warning</h3>
                                    <asp:Label ID="LbMensajeModalError" runat="server" Text="" Width="100%"></asp:Label>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>--%>

                            <asp:UpdatePanel ID="UpdateModal" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="col-md-12" runat="server" id="DivAlerta" visible="false" style="display: flex; background-color: tomato; justify-content: center">
                                        <asp:Label runat="server" CssClass="col-form-label text-white" ID="LbMensajeModalError"></asp:Label>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

<%--                    <div class="col-md-12" style="margin-left: auto; margin-right: auto" id="Div3" runat="server">
                        <div class="alert alert-success  alert-dismissible align-content-md-center" style="align-self: auto">
                            <div class="row">
                                <div class="col-3">
                                    <p class="text-center">
                                        <img src="https://img.icons8.com/color/70/000000/accept-database.png" /><span class="dashicons dashicons-admin-home"></span></p>
                                </div>
                                <div class="col-9" style="text-align: center">
                                    <br>
                                    <h4><strong>¿Está seguro que desea modificar la agencia?</strong></h4>
                                    <p>Si esta seguro dar clic en el botón "Modificar"</p>
                                </div>
                                <br>
                            </div>
                        </div>
                    </div>--%>

                </div>

                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <button type="button" class="btn btn-light" data-dismiss="modal">
                                Close</button>
                            <asp:Button ID="btnModalModificar" runat="server" Text="Modificar" class="btn btn-dark" OnClick="btnModalModificar_Click" />
                        </ContentTemplate>

                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <%--FIN MODAL ENVIAR LV--%>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
