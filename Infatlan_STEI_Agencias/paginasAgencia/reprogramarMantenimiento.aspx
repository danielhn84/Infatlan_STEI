<%@ Page Title="" Language="C#" MasterPageFile="~/mainAgencia.Master" AutoEventWireup="true" CodeBehind="reprogramarMantenimiento.aspx.cs" Inherits="Infatlan_STEI_Agencias.paginasAgencia.reprogramarMantenimiento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">

    <div class="row page-titles">
        <div class="col-md-5 align-self-center">
            <h2 class="text-themecolor">Reprogramar Mantenimientos Preventivos</h2>
            <div class="mr-md-3 mr-xl-5">
                <p class="mb-md-0">Soporte Técnico y Comunicaciones</p>
            </div>
        </div>
    </div>

   <div class="card">
        <asp:UpdatePanel ID="UpdateDivBusquedas" runat="server">
            <ContentTemplate>
                <div class="row" id="DivBusqueda" runat="server">
                    <div class="col-12 grid-margin stretch-card">
                        <div class="card">
                            <div class="card-body">
                                <h4 class="card-title">Reprogramaciones Pendientes</h4>
                                <p>Lista de mantenimientos que han sido cancelados y estan pendientes de reprogramar por jefe o suplente.</p>
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
                                                        <asp:TextBox ID="TxBuscarAgencia" runat="server" placeholder="Ingrese nombre de agencia" class="form-control" AutoPostBack="true" OnTextChanged="TxBuscarAgencia_TextChanged"></asp:TextBox>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

       <div class="card">
           <div class="row col-12">
               <div class="col-12 grid-margin stretch-card">
                   <div class="table-responsive">
                       <asp:UpdatePanel runat="server" ID="UPGvBusqueda">
                           <ContentTemplate>
                               <asp:GridView ID="GvPendientesReprogramar" runat="server"
                                   CssClass="table table-bordered"
                                   PagerStyle-CssClass="pgr"
                                   HeaderStyle-CssClass="table"
                                   RowStyle-CssClass="rows"
                                   AutoGenerateColumns="false"
                                   AllowPaging="true"
                                   GridLines="None"
                                   PageSize="10">

                                   <Columns>
                                       <asp:TemplateField>
                                           <ItemTemplate>
                                               <asp:LinkButton ID="LBAprobar" runat="server" CssClass="btn btn-success" CommandName="Aprobar" CommandArgument='<%# Eval("id_Mantenimiento") %>'>
                                                        <i class="icon-check"></i>
                                               </asp:LinkButton>

                                               <asp:LinkButton ID="LBCancelar" runat="server" CssClass="btn btn-danger" CommandName="Cancelar" CommandArgument='<%# Eval("id_Mantenimiento") %>'>
                                                        <i class="icon-close"></i>
                                               </asp:LinkButton>
                                           </ItemTemplate>
                                           <ItemStyle Width="10%"/>
                                       </asp:TemplateField>
                                       <asp:BoundField DataField="id_Mantenimiento" HeaderText="Id Mantenimiento" />
                                       <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                                       <asp:BoundField DataField="Cod_Agencia" HeaderText="Codigo Agencia" />
                                       <asp:BoundField DataField="Responsable" HeaderText="Responsable" />
                                       <asp:BoundField DataField="Lugar" HeaderText="Lugar" />
                                       <asp:BoundField DataField="Area" HeaderText="Area" />
                                       <asp:BoundField DataField="motivoCancelacion" HeaderText="Motivo" />
                                       <asp:BoundField DataField="detalleCancelación" HeaderText="Detalle" />                                       
                                   </Columns>
                               </asp:GridView>
                           </ContentTemplate>
                       </asp:UpdatePanel>
                   </div>
               </div>
           </div>
       </div>
   </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
