<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="permisos.aspx.cs" Inherits="Infatlan_STEI.paginas.configuraciones.permisos" %>
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
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="/images/loading.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="display: inline-block; vertical-align: middle;" />
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
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Configuración</a></li>
                    <li class="breadcrumb-item active">Permisos</li>
                </ol>
            </div> 
        </div>
    </div>

    <asp:UpdatePanel runat="server" ID="UpdatePanel">
        <ContentTemplate>
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Permisos</h4>
                    <h6 class="card-subtitle">Habilitar o deshabilitar permisos a usuarios del sistema.</h6>
                    <br />
                    <div class="row col-7"> 
                        <label class="col-2 col-form-label">Usuario</label>
                        <div class="col-8">
                            <asp:dropdownlist AutoPostBack="true" id="DDLUsuarios" CssClass="form-control" runat="server" OnSelectedIndexChanged="DDLUsuarios_SelectedIndexChanged"></asp:dropdownlist>
                        </div>
                    </div>

                    <div class="table-responsive m-t-40">
                        <asp:GridView ID="GVBusqueda" runat="server"
                            CssClass="table table-bordered"
                            PagerStyle-CssClass="pgr"
                            HeaderStyle-CssClass="table"
                            RowStyle-CssClass="rows"
                            AutoGenerateColumns="false"
                            AllowPaging="true"
                            GridLines="None"
                            PageSize="10">
                            <Columns>
                                <asp:BoundField DataField="idAplicacion" HeaderText="No."/>
                                <asp:BoundField DataField="nombre" HeaderText="Aplicación"/>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Right">
                                    <HeaderTemplate>Consultar</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CBxConsulta" runat="server" Checked='<%# Convert.ToBoolean(Eval("consulta")) %>'/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>Crear</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CBxCrear" runat="server" Checked='<%# Convert.ToBoolean(Eval("escritura")) %>'/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>Editar</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CBxEditar" runat="server" Checked='<%# Convert.ToBoolean(Eval("edicion")) %>'/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>Borrar</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CBxBorrar" runat="server" Checked='<%# Convert.ToBoolean(Eval("borrar")) %>'/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>

                    <div class="row col-12">
                        <asp:Button runat="server" Visible="false" ID="BtnAceptar" CssClass="btn btn-success" Text="Guardar" OnClick="BtnAceptar_Click" />
                    </div>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
