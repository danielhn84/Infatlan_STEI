﻿<%@ Page Title="" Language="C#" MasterPageFile="~/mainAgencia.Master" AutoEventWireup="true" CodeBehind="lvPendientesModificar.aspx.cs" Inherits="Infatlan_STEI_Agencias.pages.lvPendientesModificar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">    
    <div class="row page-titles">
        <div class="col-md-7 align-self-center">
            <h2 class="text-themecolor">Listas de Verificación Pendientes de Modificar</h2>
            <div class="mr-md-3 mr-xl-5">
                <%-- <h2>Creación de Notificación</h2>--%>
                <p class="mb-md-0">Soporte Técnico y Comunicaciones</p>
            </div>
        </div>
    </div>

   <div class="card">
        <div class="card-body">
            <h4 class="card-title">LV Pendientes Modificar</h4>
            <p>Listas de verificación pendientes de modificar que estan asignadas a su persona.</p>
            <div class="col-md-6">
                <div class="form-group row">
                    <label class="col-sm-3 col-form-label">Buscar</label>
                    <div class="col-sm-9">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <div class="input-group mb-3">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text" id="basic-addon1"><i class="ti-search"></i></span>
                                    </div>
                                    <asp:TextBox ID="TxBuscarAgencia" runat="server" placeholder="Búsqueda por nombre de agencia" class="form-control" AutoPostBack="true"></asp:TextBox>
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
                                GridLines="None"
                                PageSize="10"  OnRowCommand="GvLvPendentesModificar_RowCommand">

                                <Columns>
                                    <asp:TemplateField HeaderText="Acción" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LBModificar" runat="server" CssClass="btn btn-info" CommandName="Modificar" CommandArgument='<%# Eval("id_Mantenimiento") %>'>
                                                        <i class=" icon-pencil"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="id_Mantenimiento" HeaderText="Id" ControlStyle-Width="10%" />
                                    <asp:BoundField DataField="fecha" HeaderText="Fecha" ControlStyle-Width="10%" />
                                    <asp:BoundField DataField="Cod_Agencia" HeaderText="Cod. Agencia" ControlStyle-Width="10%" />
                                    <asp:BoundField DataField="Lugar" HeaderText="Lugar" ControlStyle-Width="10%" />
                                    <asp:BoundField DataField="Area" HeaderText="Area" ControlStyle-Width="10%" />
                                    <asp:BoundField DataField="sysAid" HeaderText="No. SysAid" ControlStyle-Width="10%" />
                                    <asp:BoundField DataField="Responsable" HeaderText="Responsable" ControlStyle-Width="10%" />
                                    <asp:BoundField DataField="idUsuario" HeaderText="idUsuariob" ControlStyle-Width="10%" Visible="false" />
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
