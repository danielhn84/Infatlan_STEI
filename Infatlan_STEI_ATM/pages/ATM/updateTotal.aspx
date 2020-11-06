<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="updateTotal.aspx.cs" Inherits="Infatlan_STEI_ATM.pages.ATM.updateTotal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <!--PARA LLAMAR MODAL-->
    <script type="text/javascript">
        function openModal() { $('#modalcrearATM').modal('show'); }
    </script>
    <!--PARA CERRAR MODAL-->
    <script type="text/javascript">
        function closeModal() { $('#modalcrearATM').modal('hide'); }
    </script>
    <script type="text/javascript">
        //IMAGEN1        
        function img1(input) {
            if (input.files && input.files[0]) {
                //PRIMERA IMAGEN              
                var reader = new FileReader();
                reader.onload = function (e) {
                    //$('#imgDiscoDuro').css('visibility', 'visible');
                    //$('#imgDiscoDuro').attr('src', e.target.result);  
                    var ruta1 = e.target.result;
                    document.getElementById('<%=imgMapaATM.ClientID%>').src = ruta1;
                    document.getElementById('<%=HFMapa.ClientID%>').value = 'si';
                }
                reader.readAsDataURL(input.files[0]);
                //PRIMERA IMAGEN              
            }
        }
        //IMAGEN1
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
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Modificar</a></li>
                    <li class="breadcrumb-item active">Modificar ATM</li>
                </ol>
            </div>
        </div>
    </div>

    <div class="card">
        <div class="card-body">
            <h4 class="card-title">Modificar ATM</h4>
            <h6 class="card-subtitle">Modificar información general de ATM</h6>
            <div class="row col-12">
                <div class="col-12 grid-margin stretch-card">
                    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="UPtotalATM">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-12 grid-margin stretch-card">
                                    <!--PRIMERA FILA-->
                                    <div class="row col-12">
                                        <div class="row col-4 align-self-center" style="margin-left: auto; margin-right: auto">
                                            <label class="col-form-label">Código de ATM</label>
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtcodATM" Enabled="false"></asp:TextBox>
                                        </div>
                                        <div class="row col-4 align-self-center" style="margin-left: auto; margin-right: auto">
                                            <label class="col-form-label">Nombre de ATM</label>
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtnombreATM" Enabled="false"></asp:TextBox>
                                        </div>
                                         <div class="row col-4 align-self-center" style="margin-left: auto; margin-right: auto">
                                            <label class="col-form-label">Dirección IP</label>
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtIP" Enabled="false"></asp:TextBox>
                                        </div>
                                       
                                    </div>
                                    <!--/PRIMERA FILA-->
                                    <!--SEGUNDA FILA-->
                                     <div class="row col-12">
                                           <div class="row col-4 align-self-center" style="margin-left: auto; margin-right: auto">
                                            <label class="col-form-label">Puerto</label>
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtpuerto" Enabled="false"></asp:TextBox>
                                        </div>
                                           <div class="row col-4 align-self-center" style="margin-left: auto; margin-right: auto">
                                            <label class="col-form-label">Estado de ATM</label>
                                            <asp:DropDownList runat="server" ID="DDLestado" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                        </div>
                                          <div class="row col-4 align-self-center" style="margin-left: auto; margin-right: auto">
                                            <label class="col-form-label">Tipo de ATM</label>
                                            <asp:DropDownList runat="server" ID="DDLTipoATM" Enabled="false" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                        
                                      </div>
                                    <hr />
                                    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="UPSegFila">
                                        <ContentTemplate>
                                            <div class="row col-12">
                                                <div class="row col-4 align-self-center" style="margin-left: auto; margin-right: auto">
                                                    <label class="col-form-label">Ubicación de ATM</label>
                                                    <asp:DropDownList runat="server" ID="DDLUbicacionATM" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                                <div class="row col-4 align-self-center" style="margin-left: auto; margin-right: auto">
                                                    <label class="col-form-label">Modelo de ATM</label>
                                                    <asp:DropDownList runat="server" AutoPostBack="true" ID="DDLModeloATM" OnTextChanged="DDLModeloATM_TextChanged" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                                <div class="row col-4 align-self-center" style="margin-left: auto; margin-right: auto">
                                                    <label class="col-form-label">Tipo de modelo de ATM</label>
                                                    <asp:DropDownList runat="server" ID="DDLDetalleModelo" CssClass="form-control col-12"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <!--/SEGUNDA FILA-->
                                    
                                    <!--TERCERA FILA-->
                                    <div class="row col-12">
                                        <div class="row col-4 align-self-center" style="margin-left: auto; margin-right: auto">
                                            <label class="col-form-label">Tipo de Carga</label>
                                            <asp:DropDownList runat="server" ID="DDLTipoCarga" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                        <div class="row col-4 align-self-center" style="margin-left: auto; margin-right: auto">
                                            <label class="col-form-label">Procesador de ATM</label>
                                            <asp:DropDownList runat="server" ID="DDLProcesadorATM" CssClass="form-control"></asp:DropDownList>
                                        </div>

                                        <div class="row col-4 align-self-center" style="margin-left: auto; margin-right: auto">
                                            <label class="col-form-label">Teclado de ATM</label>
                                            <asp:DropDownList runat="server" ID="DDLTecladoATM" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <!--/TERCERA FILA-->
                                    <!--CUARTA FILA-->
                                    <div class="row col-12">
                                        <div class="row col-4 align-self-center" style="margin-left: auto; margin-right: auto">
                                            <label class="col-form-label">Serie del ATM</label>
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtserieATM"></asp:TextBox>
                                        </div>
                                        <div class="row col-4 align-self-center" style="margin-left: auto; margin-right: auto">
                                            <label class="col-form-label">Memoria RAM(GB)</label>
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtramATM" TextMode="Number"></asp:TextBox>
                                        </div>
                                        <div class="row col-4 align-self-center" style="margin-left: auto; margin-right: auto">
                                            <label class="col-form-label">N/S de Disco Duro</label>
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtserieDisco"></asp:TextBox>
                                        </div>
                                    </div>
                                    <!--/CUARTA FILA-->

                                    <!--QUINTA FILA-->
                                    <div class="row col-12">
                                        <div class="row col-4 align-self-center" style="margin-left: auto; margin-right: auto">
                                            <label class="col-form-label">Sistema Operativo</label>
                                            <asp:DropDownList runat="server" ID="DDLso" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                        <div class="row col-4 align-self-center" style="margin-left: auto; margin-right: auto">
                                            <label class="col-form-label">Version del software</label>
                                            <asp:DropDownList runat="server" ID="DDLversionSw" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                        <div class="row col-4 align-self-center" style="margin-left: auto; margin-right: auto">
                                            <label class="col-form-label">Sucursal de ATM</label>
                                            <asp:DropDownList runat="server" ClientIDMode="AutoID" ID="DDLsucursalATM" AutoPostBack="true" OnTextChanged="DDLsucursalATM_TextChanged" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <!--/QUINTA FILA-->
                                    <!--SEXTA FILA-->
                                    <div class="row col-12">
                                        <div class="row col-4 align-self-center" style="margin-left: auto; margin-right: auto">
                                            <label class="col-form-label">Marca del Disco Duro</label>
                                            <asp:DropDownList runat="server" ID="DDLmarca" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                        <div class="row col-4 align-self-center" style="margin-left: auto; margin-right: auto">
                                            <label class="col-form-label">Capacidad del disco(GB)</label>
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtcapacidadDisco" TextMode="Number"></asp:TextBox>
                                        </div>
                                           <div class="row col-4 align-self-center" style="margin-left: auto; margin-right: auto">
                                            <label class="col-form-label">Inventario de ATM</label>
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtinventarioATM"></asp:TextBox>
                                        </div>
                                       
                                    </div>
                                    <!--/SEXTA FILA-->

                                    <!--SEPTIMA FILA-->
                                    <div class="row col-12">
                                      
                                          <div class="row col-4 align-self-center" style="margin-left: auto; margin-right: auto">
                                            <label class="col-form-label">Código de ubicación</label>
                                            <asp:TextBox runat="server" CssClass="form-control" Enabled="false" ID="txtcodUbicacion"></asp:TextBox>
                                        </div>
                                        <div class="row col-4 align-self-center" style="margin-left: auto; margin-right: auto">
                                            <label class="col-form-label">Latitud</label>
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtlatitud"></asp:TextBox>
                                        </div>

                                        <div class="row col-4 align-self-center" style="margin-left: auto; margin-right: auto">
                                            <label class="col-form-label">Longitud</label>
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtlongitud"></asp:TextBox>
                                        </div>
                                    </div>
                                    <!--/SEPTIMA FILA-->
                                    
                                    <!--NOVENA FILA-->
                                    <div class="row col-12">
                                        <label class="col-form-label col-12">Dirección</label>
                                        <div class="col-12">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtdireccion" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                        </div>          
                                    </div>
                                    <!--NOVENA FILA-->
                                    <!--DECIMA FILA-->
                                    <div class=" row col-12">
                                        <label class="col-form-label col-12">Ubicación del ATM(Mapa)</label>                                        
                                        <div class="col-12">
                                            <asp:FileUpload ID="FUMapaATM" runat="server" onchange="img1(this);" /><br /><br />
                                            <img id="imgMapaATM" runat="server" src="/assets/images/vistaPrevia1.JPG" style="border-width: 0px; height: auto; width: 100%" />
                                        </div>
                                        </div>
                                    <!--DECIMA FILA-->
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    <asp:UpdatePanel ID="UPEnviarVerif" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-12">
                                    <asp:Button runat="server" ID="btnModificarATM" OnClick="btnModificarATM_Click" CssClass="btn btn-success" Text="Modificar ATM" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <!--MODAL GUARDAR ATM -->
    <div class="modal fade bs-example-modal-lg" id="modalcrearATM" tabindex="-1" data-backdrop="static" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myLargeModalLabel">¿Seguro que actualizará ATM?</h4>
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
                            <asp:Button runat="server" ID="btnModalCerrarModificarATM" OnClick="btnModalCerrarModificarATM_Click" CssClass="btn btn-secondary mr-2" Text="Cancelar" />
                                <asp:Button runat="server" ID="btnModalModificarATM" OnClick="btnModalModificarATM_Click" CssClass="btn btn-success mr-2" Text="Modificar" />                          
                        </div>
                    </ContentTemplate>
                     <Triggers>
                        <asp:PostBackTrigger ControlID="btnModalModificarATM" />
                    </Triggers>
                </asp:UpdatePanel>

            </div>
            <!-- /.modal-content -->
        </div>
        <!--/.modal-dialog -->
    </div>
    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="HFMapa" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
