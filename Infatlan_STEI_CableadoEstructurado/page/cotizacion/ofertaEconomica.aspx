<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="ofertaEconomica.aspx.cs" Inherits="Infatlan_STEI_CableadoEstructurado.page.cotizacion.ofertaEconomica" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <style>
        .aspNetDisabled {
            cursor: not-allowed !important;
        }

        .hidden {
            display: none;
        }
    </style>
    <script type="text/javascript">
        function closeModal() { $('#ModalOferta').modal('show'); }
        function openModal() { $('#ModalOferta').modal('show'); }
    </script>

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
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Cableado</a></li>
                    <li class="breadcrumb-item active">Oferta Económica</li>
                </ol>
            </div>
        </div>
    </div>

    <div class="card">
        <div class="card-body">
            
            <div class="card-body">

                <h4 class="card-title">Oferta Económica</h4>
                <br />
                <h6 class="card-subtitle" >
                    <asp:Label runat="server" ID="LbDescripcion" Text="Busqueda de ofertas económica existentes." Visible="false"></asp:Label></h6>

                <asp:UpdatePanel ID="updBuscarAprobacion" runat="server" Visible="false">
                    <ContentTemplate>
                        <div class="row">
                            <label class="col-2 col-form-label">Búsqueda</label>
                            <div class="col-7">
                                <asp:TextBox ID="TxBuscarOferta" runat="server" placeholder="Ej. Ag Junior- Presione afuera para proceder" class="form-control" AutoPostBack="true" OnTextChanged="TxBuscarOferta_TextChanged"></asp:TextBox>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

               
                <asp:UpdatePanel ID="udpContabilidad" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <br />
                          <br />
                        <p><asp:Label runat="server" ID="LbDescripcionOferta" Text="Detalle de las ofertas económicas."></asp:Label></p>
                        <div class="table-responsive">
                            <asp:GridView ID="GVOfertaEconomica" runat="server"
                                CssClass="table table-bordered"
                                AutoPostBack="true"
                                PagerStyle-CssClass="pgr"
                                HeaderStyle-CssClass="table"
                                RowStyle-CssClass="rows"
                                AutoGenerateColumns="false"
                                AllowPaging="true"
                                GridLines="None"
                                PageSize="10"
                                OnPageIndexChanging="GVOfertaEconomica_PageIndexChanging"
                                OnRowCommand="GVOfertaEconomica_RowCommand">

                                <Columns>
                                    <asp:BoundField DataField="idEstudio" HeaderText="Estudio" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                    <asp:BoundField DataField="nombre" HeaderText="Estudio" />
                                    <asp:BoundField DataField="agencia" HeaderText="Nombre" />
                                    <asp:BoundField DataField="responsable" HeaderText="Técnico Responsable" />
                                    <asp:BoundField DataField="fechaCreacion" HeaderText="Creación" />
                                    <asp:TemplateField HeaderText="Seleccione" HeaderStyle-Width="18%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="BtnDescargar" Title="Descargar" runat="server" class="btn btn-primary" CommandArgument='<%#Eval("idEstudio")%>' CommandName="Descargar"><i class="ti-import" ></i>
                                            </asp:LinkButton>

                                            <asp:LinkButton ID="BtnModificar" Visible="false" name="Modificar" Title="Modificar" runat="server" class="btn btn-info" CommandArgument='<%#Eval("idEstudio")%>' CommandName="Modificar"><i class="ti-pencil" ></i>
                                            </asp:LinkButton>

                                            <asp:LinkButton ID="BtnAprobar" Visible="false" name="Aprobar" Title="Aprobar" runat="server" class="btn btn-success" CommandArgument='<%#Eval("idEstudio")%>' CommandName="Aprobar"><i class="ti-check" ></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <div class="modal fade" id="ModalOferta" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4>
                        <asp:Label ID="LbModTitutlo" runat="server" Text="Datos oferta económica" CssClass="align-content-center"></asp:Label></h4>

                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <asp:UpdatePanel ID="UdpDatosOferta" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-body">
                            <div class="row">
                                <b>
                                    <asp:Label runat="server" Text="&nbsp &nbspEstudio: &nbsp"></asp:Label></b>
                                <asp:Label ID="LbModNombreEstudio" Text="" runat="server" AutoPostBack="true"></asp:Label>
                            </div>
                            <br />
                            <div class="row">
                                <b>
                                    <asp:Label runat="server" Text="&nbsp &nbspTotal cotización: &nbsp "></asp:Label></b>
                                <asp:Label ID="LbModValorCotizacion" Text="" runat="server" AutoPostBack="true"></asp:Label>
                            </div>

                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Button ID="BtnAprobar" runat="server" Text="Aprobar" class="btn btn-success" AutoPostBack="true" OnClick="BtnAprobar_Click"></asp:Button>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="BtnAprobar" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
