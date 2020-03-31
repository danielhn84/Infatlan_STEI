<%@ Page Title="" Language="C#" MasterPageFile="~/mainAgencia.Master" AutoEventWireup="true" CodeBehind="creacionAgencia.aspx.cs" Inherits="Infatlan_STEI_Agencias.pages.configuraciones.creacionAgencia" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">

    <div class="row page-titles">
        <div class="col-md-7 align-self-center">
            <h2 class="text-themecolor">Agencias BASA</h2>
            <div class="mr-md-3 mr-xl-5">
                <%-- <h2>Creación Agencia</h2>--%>
                <p class="mb-md-0">Soporte Técnico y Comunicaciones</p>
            </div>
        </div>
    </div>

    <div class="card">
        <div class="card-body">
            <h4 class="card-title">Agencias</h4>
            <p>Información general de cada agencia</p>

            <ul class="nav nav-tabs" role="tablist">
                <li class="nav-item"><a class="nav-link active" data-toggle="tab" href="#home" role="tab"><span class="hidden-sm-up"><i class="fa fa-save"></i></span><span class="hidden-xs-down"> Guardar</span></a> </li>
                <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#profile" role="tab"><span class="hidden-sm-up"><i class=" icon-refresh"></i></span><span class="hidden-xs-down"> Modificar</span></a> </li>
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
                                        <asp:TextBox ID="TxAgencia" AutoPostBack="true" runat="server" class="form-control"></asp:TextBox>
                                </div>

                                <div class="col-md-6">
                                    <label class="control-label   text-danger">*</label><label class="control-label">Código Agencia:</label></label>    
                                        <asp:TextBox ID="TxCodigo" AutoPostBack="true" runat="server" TextMode="Number" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <!--Fin Fila 1-->

                            <!--Inicio Fila 2-->
                            <div class="row p-t-20">
                                <div class="col-md-6">
                                    <label class="control-label   text-danger">*</label><label class="control-label">Dirección:</label></label>
                                        <asp:TextBox ID="TxDireccion" AutoPostBack="true" runat="server" TextMode="MultiLine" Rows="3" class="form-control"></asp:TextBox>
                                </div>

                                <div class="col-md-6">
                                    <label class="control-label   text-danger">*</label><label class="control-label">Telefono:</label></label>    
                                        <asp:TextBox ID="TxTelefono" AutoPostBack="true" runat="server" TextMode="MultiLine" Rows="3" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <!--Fin Fila 2-->

                            <!--Inicio Fila 3-->
                            <div class="row p-t-20">
                                <div class="col-md-6">
                                    <label class="control-label   text-danger">*</label><label class="control-label">Latitud:</label></label>
                                        <asp:TextBox ID="TxLatitud" AutoPostBack="true" runat="server" TextMode="MultiLine" Rows="3" class="form-control"></asp:TextBox>
                                </div>

                                <div class="col-md-6">
                                    <label class="control-label   text-danger">*</label><label class="control-label">Longitud:</label></label>    
                                        <asp:TextBox ID="TxLongitud" AutoPostBack="true" runat="server" TextMode="MultiLine" Rows="3" class="form-control"></asp:TextBox>
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
                                      <asp:DropDownList ID="DDLDepartamento" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <!--Fin Fila 4-->
                            <br>
                            <!--Fin Fila 5-->
                            <div class="row p-t-20">
                                <div class="col-md-5">
                                    <label class="control-label   text-danger">*</label><label class="control-label">Requiere Conductor:</label></label>                                    
                                </div>

                                <div class="col-md-2">
                                    <asp:RadioButtonList ID="RblConductor" RepeatDirection="Horizontal" Width="90px" runat="server" AutoPostBack="True">
                                        <asp:ListItem Value="1">Si</asp:ListItem>
                                        <asp:ListItem Value="0">No</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <!--Fin Fila 5-->

                            <br />
                            <br />
                            <div class="row p-t-20 col-md-12">
                                <div class="col-md-5" style="margin-left: auto; margin-left: auto">
                                    <asp:Button ID="BtnEnviar" class="btn btn-block btn-lg btn-success" runat="server" Text="Enviar" OnClick="BtnEnviar_Click" />
                                </div>

                                <div class="col-md-5 " style="margin-left: auto; margin-right: auto">
                                    <asp:Button ID="BtnCancelar" class="btn btn-block btn-lg  btn-info " runat="server" Text="Cancelar" OnClick="BtnCancelar_Click" />
                                </div>
                            </div>
                            <br />

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <!--/PRIMER CONTENIDO-->

                <!--SEGUNDO CONTENIDO-->
                <div class="tab-pane  p-20" id="profile" role="tabpanel">

                    <div class="col-md-6">
                        <div class="form-group row">
                            <label class="col-sm-3 col-form-label">Buscar</label>
                            <div class="col-sm-9">
                                <%--             <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>--%>
                                <div class="input-group mb-3">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text" id="basic-addon1"><i class="ti-search"></i></span>
                                    </div>
                                    <asp:TextBox ID="TxBuscarAgencia" runat="server" placeholder="Búsqueda por agencia" class="form-control" AutoPostBack="true"></asp:TextBox>
                                </div>
                                <%--            </ContentTemplate>
                        </asp:UpdatePanel>--%>
                            </div>
                        </div>
                    </div>

                    <div class="row col-12">
                        <div class="col-12 grid-margin stretch-card">
                            <div class="table-responsive">
                                <asp:UpdatePanel runat="server" ID="UPAgencias">
                                    <ContentTemplate>
                                        <asp:GridView ID="GVAgencias" runat="server"
                                            CssClass="table table-bordered"
                                            PagerStyle-CssClass="pgr"
                                            HeaderStyle-CssClass="table" HeaderStyle-HorizontalAlign="center"
                                            RowStyle-CssClass="rows"
                                            AutoGenerateColumns="false"
                                            AllowPaging="true"
                                            GridLines="None"
                                            PageSize="10" >

                                            <Columns>
                                                <asp:TemplateField HeaderText="Acción" ItemStyle-HorizontalAlign="center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LbModificar" runat="server" CssClass="btn btn-cyan" CommandName="Modifcar" CommandArgument='<%# Eval("idAgencia") %>'>
                                                        <i class="icon-pencil"></i>
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
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
                <!--/SEGUNDO CONTENIDO-->


            </div>

        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
