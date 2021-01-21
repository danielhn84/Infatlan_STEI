<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="lvPendientesModificar.aspx.cs" Inherits="Infatlan_STEI_Agencias.pages.lvPendientesModificar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <script type="text/javascript">

        var updateProgress = null;
        function postbackButtonClick() {
            updateProgress = $find("<%= UpdateProgress1.ClientID %>");
            window.setTimeout("updateProgress.set_visible(true)", updateProgress.get_displayAfter());
            return true;
        }
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
                    <li class="breadcrumb-item active">Modificar LV</li>
                </ol>
            </div> 
        </div>
    </div>

   <div class="card">
        <div class="card-body">
            <h4 class="card-title">Listas Pendientes de Modificar</h4>
            <p>Listas de verificación pendientes de modificar que estan asignadas a su persona.</p>
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
                                        <asp:TextBox ID="TxBuscarAgencia" runat="server" placeholder="Búsqueda por agencia o codigo, luego presione Enter" class="form-control" AutoPostBack="true" ></asp:TextBox>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>


        <div class="row col-12">
            <div class="col-12 grid-margin stretch-card">
                <div class="table-responsive">
                    <asp:UpdatePanel runat="server" ID="UpLvPendientesModificar">
                        <ContentTemplate>
                            <asp:GridView ID="GvLvPendentesModificar" runat="server"
                                CssClass="table table-bordered"
                                PagerStyle-CssClass="pgr"
                                HeaderStyle-CssClass="table" HeaderStyle-HorizontalAlign="center"
                                RowStyle-CssClass="rows"
                                AutoGenerateColumns="false"
                                AllowPaging="true"
                                GridLines="None" OnPageIndexChanging="GvLvPendentesModificar_PageIndexChanging"
                                PageSize="10"  OnRowCommand="GvLvPendentesModificar_RowCommand">

                                <Columns>
                                    <asp:TemplateField HeaderText="Acción" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LBModificar" runat="server" class="btn btn-info mr-2"  CommandName="Modificar" CommandArgument='<%# Eval("id_Mantenimiento") %>'>
                                                       <i class="icon-pencil" ></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="id_Mantenimiento" HeaderText="Id" Visible="false" ControlStyle-Width="10%" />
                                    <asp:BoundField DataField="fecha" HeaderText="Fecha" ControlStyle-Width="10%" />
                                    <asp:BoundField DataField="Cod_Agencia" HeaderText="Cod. Agencia" Visible="false" ControlStyle-Width="10%" />
                                    <asp:BoundField DataField="Lugar" HeaderText="Lugar" ControlStyle-Width="10%" />
                                    <asp:BoundField DataField="Area" HeaderText="Area" ControlStyle-Width="10%" />
                                    <asp:BoundField DataField="sysAid" HeaderText="No. SysAid" ControlStyle-Width="10%" Visible="false" />
                                    <asp:BoundField DataField="Responsable" HeaderText="Responsable" ControlStyle-Width="10%" />
                                    <asp:BoundField DataField="idUsuario" HeaderText="idUsuariob" ControlStyle-Width="10%" Visible="false" />
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
               
                <br>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
