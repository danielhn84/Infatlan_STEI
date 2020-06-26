<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="add.aspx.cs" Inherits="Infatlan_STEI_ATM.pages.ATM.add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <!--PARA LLAMAR MODAL-->
    <script type="text/javascript">
        function openModal() { $('#modalcrearATM').modal('show'); }
    </script>
    <!--PARA CERRAR MODAL-->
    <script type="text/javascript">
        function closeModal() { $('#modalcrearATM').modal('hide'); }
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
                    <li class="breadcrumb-item"><a href="javascript:void(0)">ATM</a></li>
                    <li class="breadcrumb-item active">Agregar</li>
                </ol>
            </div>
        </div>
    </div>

    <div class="card">
        <div class="card-body">
            <h4 class="card-title">Ingresar nuevo ATM</h4>
            <h6 class="card-subtitle">Información general de ATM</h6>
            
            <div class="row">
                <div class="col-md-12">
                    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="UPtotalATM">
                        <ContentTemplate>
                            <!--PRIMERA FILA-->
                            <div class="row col-12 mb-2">
                                <div class="col-4">
                                    <label class="col-form-label">Código de ATM</label>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtcodATM"></asp:TextBox>
                                </div>
                                <div class="col-4">
                                    <label class="col-form-label">Nombre de ATM</label>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtnombreATM"></asp:TextBox>
                                </div>
                                <div class="col-4">
                                    <label class="col-form-label">Sucursal de ATM</label>
                                    <asp:DropDownList runat="server" OnTextChanged="DDLsucursalATM_TextChanged" ClientIDMode="AutoID" AutoPostBack="true" ID="DDLsucursalATM" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            
                            <!--SEGUNDA FILA-->
                            <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="UPSegFila">
                                <ContentTemplate>
                                    <div class="row col-12 mb-2">
                                        <div class="col-4">
                                            <label class="col-form-label">Ubicación de ATM</label>
                                            <asp:DropDownList runat="server" ID="DDLUbicacionATM" CssClass="form-control"></asp:DropDownList>
                                        </div>

                                        <div class="col-4">
                                            <label class="col-form-label">Modelo de ATM</label>
                                            <asp:DropDownList runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDLModeloATM_SelectedIndexChanged" ID="DDLModeloATM" CssClass="form-control"></asp:DropDownList>
                                        </div>

                                        <div class="col-4">
                                            <label class="col-form-label">Tipo de modelo de ATM</label>
                                            <asp:DropDownList runat="server" ID="DDLDetalleModelo" CssClass="form-control col-12"></asp:DropDownList>
                                        </div>

                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            
                            <!--TERCERA FILA-->
                            <div class="row col-12 mb-2">
                                <div class="col-4">
                                    <label class="col-form-label">Tipo de Carga</label>
                                    <asp:DropDownList runat="server" ID="DDLTipoCarga" CssClass="form-control"></asp:DropDownList>
                                </div>

                                <div class="col-4">
                                    <label class="col-form-label">Procesador de ATM</label>
                                    <asp:DropDownList runat="server" ID="DDLProcesadorATM" CssClass="form-control"></asp:DropDownList>
                                </div>

                                <div class="col-4">
                                    <label class="col-form-label">Teclado de ATM</label>
                                    <asp:DropDownList runat="server" ID="DDLTecladoATM" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            
                            <!--CUARTA FILA-->
                            <div class="row col-12 mb-2">
                                <div class="col-4">
                                    <label class="col-form-label">Serie del ATM</label>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtserieATM"></asp:TextBox>
                                </div>

                                <div class="col-4">
                                    <label class="col-form-label">Memoria RAM de ATM</label>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtramATM" TextMode="Number"></asp:TextBox>
                                </div>

                                <div class="col-4">
                                    <label class="col-form-label">N/S de Disco Duro</label>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtserieDisco"></asp:TextBox>
                                </div>
                            </div>

                            <!--QUINTA FILA-->
                            <div class="row col-12 mb-2">
                                <div class="col-4">
                                    <label class="col-form-label">Sistema Operativo</label>
                                    <asp:DropDownList runat="server" ID="DDLso" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-4">
                                    <label class="col-form-label">Version del software</label>
                                    <asp:DropDownList runat="server" ID="DDLversionSw" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-4">
                                    <label class="col-form-label">Tipo de ATM</label>
                                    <asp:DropDownList runat="server" ID="DDLTipoATM" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            
                            <!--SEXTA FILA-->
                            <div class="row col-12 mb-2">
                                <div class="col-4">
                                    <label class="col-form-label">Marca del Disco Duro</label>
                                    <asp:DropDownList runat="server" ID="DDLmarca" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-4">
                                    <label class="col-form-label">Capacidad del disco</label>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtcapacidadDisco" TextMode="Number"></asp:TextBox>
                                </div>
                                <div class="col-4">
                                    <label class="col-form-label">Dirección IP</label>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtIP"></asp:TextBox>
                                </div>
                            </div>

                            <!--SEPTIMA FILA-->
                            <div class="row col-12 mb-2">
                                <div class="col-4">
                                    <label class="col-form-label">Puerto</label>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtpuerto"></asp:TextBox>
                                </div>
                                <div class="col-4">
                                    <label class="col-form-label">Latitud</label>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtlatitud"></asp:TextBox>
                                </div>
                                <div class="col-4">
                                    <label class="col-form-label">Longitud</label>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtlongitud"></asp:TextBox>
                                </div>
                            </div>

                            <!--OCTAVA FILA-->
                            <div class="row col-12 mb-2">
                                <div class="col-4">
                                    <label class="col-form-label">Estado de ATM</label>
                                    <asp:DropDownList runat="server" ID="DDLestado" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-4">
                                    <label class="col-form-label">Inventario de ATM</label>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtinventarioATM"></asp:TextBox>
                                </div>
                                <div class="col-4">
                                    <label class="col-form-label">Código de ubicación</label>
                                    <asp:TextBox runat="server" CssClass="form-control" Enabled="false" ID="txtcodUbicacion"></asp:TextBox>
                                </div>
                            </div>

                            <!--NOVENA FILA-->
                            <div class="row col-12 mb-4">
                                <div class="col-12">
                                    <label class="col-form-label">Dirección</label>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtdireccion" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            
            <asp:UpdatePanel ID="UPEnviarVerif" runat="server">
                <ContentTemplate>
                    <div class="row col-md-12">
                        <asp:Button runat="server" ID="btnguardarATM" OnClick="btnguardarATM_Click" CssClass="btn btn-success" Text="Agregar ATM" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <!--MODAL GUARDAR ATM -->
    <div class="modal bs-example-modal-lg" id="modalcrearATM" tabindex="-1" data-backdrop="static" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header" style="background-color: darkslategrey; color: white;">
                    <h4 class="modal-title" id="myLargeModalLabel">¿Seguro que guardará ATM?</h4>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="row col-12">
                            <asp:Label runat="server" BorderStyle="None" class="col form-control col-6"><strong>Código de ATM: </strong></asp:Label>
                            <asp:Label runat="server" BorderStyle="None" ID="lbcodATM" class="col form-control col-6"></asp:Label>
                        </div>
                        <div class="row col-12">
                            <asp:Label runat="server" BorderStyle="None" class="col form-control col-6"><strong>Nombre de ATM: </strong></asp:Label>
                            <asp:Label runat="server" BorderStyle="None" ID="lbNombreATM" class="col form-control col-6"></asp:Label>
                        </div>
                        <div class="row col-12">
                            <asp:Label runat="server" BorderStyle="None" class="col form-control col-6"><strong>Sucursal de ATM: </strong></asp:Label>
                            <asp:Label runat="server" BorderStyle="None" ID="lbsucursalATM" class="col form-control col-6"></asp:Label>
                        </div>
                        <div class="row col-12">
                            <asp:Label runat="server" BorderStyle="None" class="col form-control col-6"><strong>Tipo de ATM: </strong>  </asp:Label>
                            <asp:Label runat="server" BorderStyle="None" ID="lbTipoATM" class="col form-control col-6"></asp:Label>
                        </div>
                        <div class="row col-12">
                            <asp:Label runat="server" BorderStyle="None" class="col form-control col-6"><strong>Inventario de ATM: </strong>  </asp:Label>
                            <asp:Label runat="server" BorderStyle="None" ID="lbInventarioATM" class="col form-control col-6"></asp:Label>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <div class="modal-footer col-12">
                            <div class="row col-3">
                                <asp:Button runat="server" ID="btnModalEnviarNotificacion" OnClick="btnModalEnviarNotificacion_Click" CssClass="btn btn-dark" Text="Guardar" />
                            </div>
                            <div class="row col-3">
                                <asp:Button runat="server" ID="btnModalCerrarNotificacion" OnClick="btnModalCerrarNotificacion_Click" CssClass="btn btn-secondary" Text="Cancelar" />
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
