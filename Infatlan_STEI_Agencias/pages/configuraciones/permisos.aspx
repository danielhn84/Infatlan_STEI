<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="permisos.aspx.cs" Inherits="Infatlan_STEI_Agencias.pages.configuraciones.permisos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
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
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Agencias</a></li>
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
                    <h6 class="card-subtitle">Habilitar o deshabilitar permisos a usuarios del módulo Agencias.</h6>
                    <br />
                    <div class="row col-7"> 
                        <label class="col-2 col-form-label">Usuario</label>
                        <div class="col-8">
                            <asp:dropdownlist AutoPostBack="true" id="DDLUsuarios" CssClass="form-control" runat="server" OnSelectedIndexChanged="DDLUsuarios_SelectedIndexChanged"></asp:dropdownlist>
                        </div>
                    </div>
                    <div class="table-responsive m-t-40" runat="server" visible="false" id="DIVTable">
                         <table runat="server" id="TBLPermisos" visible="true" class="tablesaw table-bordered table-hover table no-wrap" data-tablesaw-mode="swipe"
                data-tablesaw-sortable data-tablesaw-sortable-switch data-tablesaw-minimap
                data-tablesaw-mode-switch>
                <thead>
                    <tr>
                        <th scope="col" style="text-align:left; background-color: #89A9E6; color: black;"  class="border">Permisos</th>
                        <th scope="col" style="text-align:left; background-color: #89A9E6; color: black;" class="border">Mantenimiento </th>
                        <th scope="col" style="text-align:left; background-color: #89A9E6; color: black;"  class="border">Notificación </th>
                        <%-- <th scope="col" style="text-align:left; background-color: #89A9E6; color: black;"  class="border">Aprobar Notificación </th>--%>
                        <th scope="col" style="text-align:left; background-color: #89A9E6; color: black;"  class="border">Crear Verificación </th>
                         <th scope="col" style="text-align:left; background-color: #89A9E6; color: black;"  class="border">Aprobar Verificación</th>
                        <th scope="col" style="text-align:left; background-color: #89A9E6; color: black;"  class="border"> Reprogramar </th>
                         <th scope="col" style="text-align:left; background-color: #89A9E6; color: black;"  class="border"> Calendario</th>
                        <th scope="col" style="text-align:left; background-color: #89A9E6; color: black;"  class="border"> Avance </th>

                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td class="title" style="text-align:center;"> <asp:CheckBox ID="CBPermisos" runat="server" AutoPostBack="true" /></td>
                        <td class="title" style="text-align:center;"> <asp:CheckBox ID="CBMantenimiento" runat="server" AutoPostBack="true" /></td>
                        <td class="title" style="text-align:center;"> <asp:CheckBox ID="CBCreaNotif" runat="server" AutoPostBack="true" /></td>
                         <%--<td class="title" style="text-align:center;"> <asp:CheckBox ID="CBAprovarNotif" runat="server" AutoPostBack="true" /></td>--%>
                         <td class="title" style="text-align:center;"> <asp:CheckBox ID="CBCreaVerif" runat="server" AutoPostBack="true" /></td>
                         <td class="title" style="text-align:center;"> <asp:CheckBox ID="CBAprobarVerif" runat="server" AutoPostBack="true" /></td>
                         <td class="title" style="text-align:center;"> <asp:CheckBox ID="CBReprogramar" runat="server" AutoPostBack="true" /></td>
                         <td class="title" style="text-align:center;"> <asp:CheckBox ID="CBCalendario" runat="server" AutoPostBack="true" /></td>
                         <td class="title" style="text-align:center;"> <asp:CheckBox ID="CBAvance" runat="server" AutoPostBack="true" /></td>
                    </tr>
                   
                </tbody>
            </table>
                    </div>
                    <div class="table-responsive m-t-40">
                       
                    </div>

                    <div class="row col-12">
                        <asp:Button runat="server" Visible="false" ID="BtnAceptar" CssClass="btn btn-success" Text="Guardar" OnClick="BtnAceptar_Click"/>
                    </div>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
