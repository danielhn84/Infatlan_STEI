<%@ Page Title="" Language="C#" MasterPageFile="~/mainAgencia.Master" AutoEventWireup="true" CodeBehind="ListaVerificacion.aspx.cs" Inherits="Infatlan_STEI_Agencias.paginasAgencia.ListaVerificacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">

    <div class="card">

     <!--MENU DE SELECCION-->
         
          <ul class="nav nav-tabs" role="tablist">
                                    <li class="nav-item"> <a class="nav-link active" data-toggle="tab" href="#home" role="tab"><span class="hidden-sm-up"><i class="fa fa-save"></i></span> <span class="hidden-xs-down">Guardar</span></a> </li>
                                    <li class="nav-item"> <a class="nav-link" data-toggle="tab" href="#profile" role="tab"><span class="hidden-sm-up"><i class=" icon-refresh"></i></span> <span class="hidden-xs-down">Modificar</span></a> </li>
                                   
                                </ul>
                                <!-- Tab panes -->
                                <div class="tab-content tabcontent-border">
                                    <div class="tab-pane active p-20" id="home" role="tabpanel">
                                        <!--PRIMER CONTENIDO-->
                                        <asp:UpdatePanel runat="server" ID="UPprimercontenido">
                                            <ContentTemplate>
                                        <div class="row col-12">
                                            <label class="form-control-label col-12">Nuevo procesador ATM</label>
                                            <asp:TextBox runat="server" ID="txtNewProcesadorATM" CssClass="form-control col-6"></asp:TextBox>
                                        </div>
                                        <br />
                                        <div class="row col-6">
                                            <asp:Button runat="server" ID="btnguardarProcesadorATM" CssClass="btn btn-rounded btn-block btn-outline-success" Text="Guardar" />
                                        </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        <!--/PRIMER CONTENIDO-->
                                    </div>
                                    <div class="tab-pane  p-20" id="profile" role="tabpanel">
                                        <!--SEGUNDO CONTENIDO-->
                                       <!--DATAGRID-->
        <asp:UpdatePanel ID="UpdateDivBusquedas" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
               
               <h4 class="card-title">Procesador ATM creados</h4>                                                        
                <div class="row">
                    <div class="col-12 grid-margin stretch-card">
                        <div class="card" id="212">
                            <div class="card-body">
                                <div class="row">
                                    <div class="table-responsive">
                                        <asp:UpdatePanel ID="UpdateGridView" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:GridView ID="GVBusqueda" runat="server"
                                                    CssClass="mydatagrid"
                                                    PagerStyle-CssClass="pgr"
                                                    HeaderStyle-CssClass="header"
                                                    RowStyle-CssClass="rows"
                                                    AutoGenerateColumns="false"
                                                    AllowPaging="true"
                                                    GridLines="None"
                                                    PageSize="10" >
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Select" HeaderStyle-Width="60px">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnprocesador" runat="server" CssClass="btn btn-primary" Text="Modificar" CommandArgument='<%# Eval("idProcesadorATM") %>' CommandName="Codigo"></asp:LinkButton>
                                                                <%--<asp:Button ID="BtnUsuarioPassword" runat="server" Text="De baja" CssClass="btn btn-block btn-outline-danger" CommandArgument='<%# Eval("codATM") %>' CommandName="Baja" />--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>                                                    
                                                        <asp:BoundField DataField="idProcesadorATM" HeaderText="Código procesador ATM" />
                                                        <asp:BoundField DataField="nombreProcesadorATM" HeaderText="Procesador ATM" />
                                       
                                                           
                                                    </Columns>
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <!--/DATAGRID-->
                                        <!--/SEGUNDO CONTENIDO-->
                                    </div>
                                   
                                </div>                  
         <!--/MENU DE SELECCION-->

    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
