﻿<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Infatlan_STEI_Agencias._default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">   
    <div class="row page-titles">
        <div class="col-md-5 align-self-center">
            <h4 class="text-themecolor">STEI</h4>
        </div>
        <div class="col-md-7 align-self-center text-right">
            <div class="d-flex justify-content-end align-items-center">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Módulos</a></li>
                    <li class="breadcrumb-item active">Agencias</li>
                </ol>
            </div> 
        </div>
    </div>
     <div class="card-group">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="d-flex no-block align-items-center">
                                        <div>
                                            <h3><i class="icon-screen-desktop"></i></h3>
                                            <p class="text-muted">Agencias</p>
                                        </div>
                                        <div class="ml-auto">
                                            <h2 runat="server" id="H2Agencias" class="counter text-primary"></h2>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="progress">
                                        <div class="progress-bar bg-primary" role="progressbar" style="width: 85%; height: 6px;" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- Column -->
                   
                    <!-- Column -->
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="d-flex no-block align-items-center">
                                        <div>
                                            <h3><i class="icon-doc"></i></h3>
                                            <p class="text-muted">Asignados</p>
                                        </div>
                                        <div class="ml-auto">
                                            <h2 runat="server" id="H2Asignados" class="counter text-purple"></h2>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="progress">
                                        <div class="progress-bar bg-purple" role="progressbar" style="width: 85%; height: 6px;" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- Column -->
                    <!-- Column -->
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="d-flex no-block align-items-center">
                                        <div>
                                            <h3><i class="icon-bag"></i></h3>
                                            <p class="text-muted">Finalizados</p>
                                        </div>
                                        <div class="ml-auto">
                                            <h2 runat="server" id="H2Finalizados" class="counter text-success"></h2>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="progress">
                                        <div class="progress-bar bg-success" role="progressbar" style="width: 85%; height: 6px;" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
     <div class="card">
        <br />

        <div class="col-md-12 align-self-center" style="margin-left: auto; margin-right: auto">
            <div class="table-responsive">
                <asp:GridView ID="GVMantenimiento" runat="server"
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
                        <asp:BoundField DataField="AGENCIA" HeaderText="Agencia" />
                        <asp:BoundField DataField="TECNICO" HeaderText="Responsable"  />
                        <asp:BoundField DataField="FECHA" HeaderText="Fecha"  />
                        <asp:BoundField DataField="ESTADO" HeaderText="Estado"  />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
