﻿<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="updateTotal.aspx.cs" Inherits="Infatlan_STEI_ATM.pages.ATM.updateTotal" %>
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
    <div class="row page-titles">
        <div class="col-md-5 align-self-center">
            <h3 class="text-themecolor col-12">Modificar ATM</h3>
            <h6 class="text-themecolor col-12">Modificar información general de ATM</h6>
        </div>
        <div class="col-md-7 align-self-center text-right">
            <div class="d-flex justify-content-end align-items-center">
               

            </div>
        </div>
    </div>
    <!--/ENCABEZADO-->

    <div class="card">
        <br />
       
        <!--<div class="row col-12" style="margin: 10px 10px 10px 10px">-->
        <div class="row">
            <div class="col-12 grid-margin stretch-card">
                <div class="card">
                    <div class="card-body">
        <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="UPtotalATM">
            <ContentTemplate>
                <!--PRIMERA FILA-->
                <div class="row col-12">
                    <div class="row col-4">
                        <label class="col-form-label col-12">Código de ATM</label>
                        <div class="col-12">
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtcodATM" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row col-4">
                        <label class="col-form-label col-12">Nombre de ATM</label>
                        <div class="col-12">
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtnombreATM" Enabled="false"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row col-4">
                        <label class="col-form-label col-12">Sucursal de ATM</label>
                        <div class="col-12">
                            <asp:DropDownList runat="server" ClientIDMode="AutoID" ID="DDLsucursalATM" AutoPostBack="true" OnTextChanged="DDLsucursalATM_TextChanged" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>


                </div>
                <!--/PRIMERA FILA-->
                <!--SEGUNDA FILA-->
                <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="UPSegFila">
                    <ContentTemplate>
                        <div class="row col-12">
                            <div class="row col-4">
                                <label class="col-form-label col-12">Ubicación de ATM</label>
                                <div class="col-12">
                                    <asp:DropDownList runat="server" ID="DDLUbicacionATM" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="row col-4">
                                <label class="col-form-label col-12">Modelo de ATM</label>
                                <div class="col-12">
                                    <asp:DropDownList runat="server" AutoPostBack="true" ID="DDLModeloATM" OnTextChanged="DDLModeloATM_TextChanged" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="row col-4">
                                <label class="col-form-label col-12">Tipo de modelo de ATM</label>

                                <div class="col-12">
                                    <asp:DropDownList runat="server" ID="DDLDetalleModelo" CssClass="form-control col-12"></asp:DropDownList>
                                </div>
                            </div>

                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <!--/SEGUNDA FILA-->
                
                <!--TERCERA FILA-->
                <div class="row col-12">
                    <div class="row col-4">
                        <label class="col-form-label col-12">Tipo de Carga</label>
                        <div class="col-12">
                            <asp:DropDownList runat="server" ID="DDLTipoCarga" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>

                    <div class="row col-4">
                        <label class="col-form-label col-12">Procesador de ATM</label>
                        <div class="col-12">
                            <asp:DropDownList runat="server" ID="DDLProcesadorATM" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>

                    <div class="row col-4">
                        <label class="col-form-label col-12">Teclado de ATM</label>
                        <div class="col-12">
                            <asp:DropDownList runat="server" ID="DDLTecladoATM" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <!--/TERCERA FILA-->
                <!--CUARTA FILA-->
                <div class="row col-12">
                    <div class="row col-4">
                        <label class="col-form-label col-12">Serie del ATM</label>
                        <div class="col-12">
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtserieATM"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row col-4">
                        <label class="col-form-label col-12">Memoria RAM de ATM</label>
                        <div class="col-12">
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtramATM" TextMode="Number"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row col-4">
                        <label class="col-form-label col-12">N/S de Disco Duro</label>
                        <div class="col-12">
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtserieDisco"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <!--/CUARTA FILA-->
                
                <!--QUINTA FILA-->
                <div class="row col-12">
                    <div class="row col-4">
                        <label class="col-form-label col-12">Sistema Operativo</label>
                        <div class="col-12">
                            <asp:DropDownList runat="server" ID="DDLso" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>

                    <div class="row col-4">
                        <label class="col-form-label col-12">Version del software</label>
                        <div class="col-12">
                            <asp:DropDownList runat="server" ID="DDLversionSw" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row col-4">
                        <label class="col-form-label col-12">Tipo de ATM</label>
                        <div class="col-12">
                            <asp:DropDownList runat="server" ID="DDLTipoATM" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>

                </div>
                <!--/QUINTA FILA-->
                <!--SEXTA FILA-->
                <div class="row col-12">

                    <div class="row col-4">
                        <label class="col-form-label col-12">Marca del Disco Duro</label>
                        <div class="col-12">
                            <asp:DropDownList runat="server" ID="DDLmarca" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row col-4">
                        <label class="col-form-label col-12">Capacidad del disco</label>
                        <div class="col-12">
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtcapacidadDisco" TextMode="Number"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row col-4">
                        <label class="col-form-label col-12">Dirección IP</label>
                        <div class="col-12">
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtIP"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <!--/SEXTA FILA-->
                
                <!--SEPTIMA FILA-->
                <div class="row col-12">
                    <div class="row col-4">
                        <label class="col-form-label col-12">Puerto</label>
                        <div class="col-12">
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtpuerto"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row col-4">
                        <label class="col-form-label col-12">Latitud</label>
                        <div class="col-12">
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtlatitud"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row col-4">
                        <label class="col-form-label col-12">Longitud</label>
                        <div class="col-12">
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtlongitud"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <!--/SEPTIMA FILA-->
                <!--OCTAVA FILA-->
                <div class="row col-12">
                    <div class="row col-4">
                        <label class="col-form-label col-12">Estado de ATM</label>
                        <div class="col-12">
                            <asp:DropDownList runat="server" ID="DDLestado" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row col-4">
                        <label class="col-form-label col-12">Inventario de ATM</label>
                        <div class="col-12">
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtinventarioATM"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row col-4">
                        <label class="col-form-label col-12">Código de ubicación</label>
                        <div class="col-12">
                            <asp:TextBox runat="server" CssClass="form-control" Enabled="false" ID="txtcodUbicacion"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <!--/OCTAVA FILA-->
                <!--NOVENA FILA-->
                <div class="row col-12">
                    <label class="col-form-label col-12">Dirección</label>
                    <div class="col-12">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtdireccion" TextMode="MultiLine" Rows="3"></asp:TextBox>
                    </div>
                </div>
                <!--NOVENA FILA-->
                 </ContentTemplate>
                     </asp:UpdatePanel>
                           </div>
                        </div>
                    </div>
                </div>
                      
                <br />
                <asp:UpdatePanel ID="UPEnviarVerif" runat="server">
                    <ContentTemplate>
                         <div class=" col-12 align-self-center" style="margin-left: auto; margin-right: auto">
                        <div class="row">
                            <div class="col-12 grid-margin stretch-card">
                                <div class="card">
                                    <div class="card-body">

                                         <asp:Button runat="server" ID="btnModificarATM" OnClick="btnModificarATM_Click" CssClass="btn btn-success" Text="Modificar ATM" />
                                          
                                    </div>
                                  </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
          
        <br />
        <br />
        <!--</div>-->
        <!--MODAL GUARDAR ATM -->
        <div class="modal bs-example-modal-lg" id="modalcrearATM" tabindex="-1" data-backdrop="static" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" style="display: none;">
            <div class="modal-dialog modal-xl">
                <div class="modal-content">
                    <div class="modal-header" style="background-color:darkslategrey; color:white;">
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
                                <div class="row col-3">
                                    <asp:Button runat="server" ID="btnModalModificarATM" OnClick="btnModalModificarATM_Click" CssClass="btn btn-dark mr-2" Text="Modificar" />
                                </div>
                                <div class="row col-3">
                                    <asp:Button runat="server" ID="btnModalCerrarModificarATM" OnClick="btnModalCerrarModificarATM_Click" CssClass="btn btn-secondary mr-2" Text="Cancelar" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
                <!-- /.modal-content -->
            </div>
            <!--/.modal-dialog -->
        </div>
        <!-- /MODAL GUARDAR ATM -->
    </div>
    <br />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
