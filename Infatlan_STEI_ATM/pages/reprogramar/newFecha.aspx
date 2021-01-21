<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="newFecha.aspx.cs" Inherits="Infatlan_STEI_ATM.pages.reprogramar.newFecha" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
     <!--PARA LLAMAR MODAL-->
    <script type="text/javascript">
        function openModal() { $('#modalCambiarFecha').modal('show'); }
    </script>
    <!--PARA CERRAR MODAL-->
    <script type="text/javascript">
        function closeModal() { $('#modalCambiarFecha').modal('hide'); }
    </script>
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
                    <li class="breadcrumb-item"><a href="javascript:void(0)">ATM</a></li>
                    <li class="breadcrumb-item active">Cambiar Fecha</li>
                </ol>
            </div>
        </div>
    </div>

    <div class="card">
        <div class="card-body">
            <h4 class="card-title">Avances</h4>
            <h6 class="card-subtitle">Lista de mantenimientos programados</h6><br />
           
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="form-group row col-12">
                       <%-- <label class="col-sm-3 col-form-label">Buscar ATM</label>--%>
                        <div class="col-sm-9">
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="TxBuscarATM" OnTextChanged="TxBuscarATM_TextChanged" runat="server" placeholder="ingrese ATM - Presione afuera para proceder" class="form-control" AutoPostBack="true"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                     <div class="col-md-12 align-self-center" style="margin-left: auto; margin-right: auto">
                                <div class="table-responsive">
                                    <asp:GridView ID="GVMantenimientos" runat="server"
                                        CssClass="table table-bordered"
                                        PagerStyle-CssClass="pgr"
                                        HeaderStyle-CssClass="table"
                                        RowStyle-CssClass="rows"
                                        AutoGenerateColumns="false"
                                        AllowPaging="true"
                                        GridLines="None"
                                        HeaderStyle-HorizontalAlign="center"
                                        PageSize="10" OnPageIndexChanging="GVMantenimientos_PageIndexChanging" 
                                        OnRowCommand="GVMantenimientos_RowCommand"
                                        Style="margin: 30px 0px 20px 0px">
                                        <Columns>                                        
                                            <asp:BoundField DataField="idMantenimiento" HeaderText="Código" Visible="false" ItemStyle-HorizontalAlign="center" />
                                            <asp:BoundField DataField="codATM" HeaderText="ATM" ItemStyle-HorizontalAlign="center" />                                                                            
                                            <asp:BoundField DataField="fechaMantenimiento" HeaderText="Fecha Mantenimiento" HtmlEncode=False DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="center" />
                                             <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="BtnSeleccionar" Visible="true" runat="server" class="btn btn-info ti-pencil-alt mr-2" Text="" CommandArgument='<%# Eval("idMantenimiento") %>' CommandName="Select"></asp:LinkButton>
                                                        <%--<asp:Button ID="BtnUsuarioPassword" runat="server" Text="De baja" CssClass="btn btn-block btn-outline-danger" CommandArgument='<%# Eval("codATM") %>' CommandName="Baja" />--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
     <!--MODAL CAMBIAR FECHA -->
    <div class="modal fade bs-example-modal-lg" id="modalCambiarFecha" tabindex="-1" data-backdrop="static" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                   <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                <div class="modal-header" style="background-color:darkslategrey; color:white;">           
                      <h4 class="modal-title" runat="server" id="H4Titulo"></h4>
                </div>
             
                        <br />
                        <div class="row col-12">
                        <div class="col-12">
                            <asp:Label runat="server" BorderStyle="None" class="col form-control"><strong>*Motivo de cambio: </strong></asp:Label>
                            <asp:TextBox runat="server" ID="txtMotivoCambio" TextMode="MultiLine" Rows="2" CssClass="form-control col-12 "></asp:TextBox>
                        </div>
                         <div class="col-12">
                            <asp:Label runat="server" BorderStyle="None" class="col form-control"><strong>*Nueva Fecha: </strong></asp:Label>
                            <asp:TextBox runat="server" ID="txtNewFecha" TextMode="Date" CssClass="form-control col-12 "></asp:TextBox>
                        </div>
                            </div><br />
              
                            <div class="col-md-12 align-self-center" style="margin-left: auto; margin-right: auto">
                            <asp:TextBox runat="server" Enabled="false" Text="No deje campos vacíos." Visible="false" ID="txtAlerta2" CssClass="form-control" style="background-color:red; color:white; text-align:center;"/>
                            </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="modal-footer col-12">
                                <asp:Button runat="server" ID="btnCerrar" OnClick="btnCerrar_Click" CssClass="btn btn-secundary mr-2" Text="Cancelar" />
                                <asp:Button runat="server" ID="btnNuevaFecha" OnClick="btnNuevaFecha_Click" CssClass="btn btn-dark mr-2" Text="Agregar" /> 
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
            <!-- /.modal-content -->
        </div>
        <!--/.modal-dialog -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
