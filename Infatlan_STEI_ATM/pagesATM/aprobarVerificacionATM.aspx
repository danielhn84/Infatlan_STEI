<%@ Page Title="" Language="C#" MasterPageFile="~/mainATM.Master" AutoEventWireup="true" CodeBehind="aprobarVerificacionATM.aspx.cs" Inherits="Infatlan_STEI_ATM.pagesATM.aprobarVerificacionATM" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <link href="/assets/node_modules/icheck/skins/all.css" rel="stylesheet">
    <link href="/css/pages/form-icheck.css" rel="stylesheet">
    <!-- Favicon icon -->
    <link rel="icon" type="image/png" sizes="16x16" href="/assets/images/favicon.png">
    <title>Elite Admin Template - The Ultimate Multipurpose admin template</title>
    <link rel="stylesheet" href="/assets/node_modules/dropify/dist/css/dropify.min.css">
    <!-- Custom CSS -->
    <link href="/css/style.min.css" rel="stylesheet">
    <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
    <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>

    <!--PARA LLAMAR MODAL-->
    <script type="text/javascript">
        function openModal() { $('#modalVerifATM').modal('show'); }
    </script>
    <!--PARA CERRAR MODAL-->
    <script type="text/javascript">
        function closeModal() { $('#modalVerifATM').modal('hide'); }
    </script>

    <script src="http://code.jquery.com/jquery-1.10.2.min.js" type="text/javascript"></script>
    <%--IMAGENES--%>
    <script type="text/javascript">
        //IMAGEN1
        function img1(input) {

            if (input.files && input.files[0]) {
                //PRIMERA IMAGEN
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#imgDiscoDuro').css('visibility', 'visible');
                    $('#imgDiscoDuro').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
                //PRIMERA IMAGEN              
            }
        }
        //IMAGEN1
        //IMAGEN2
        function img2(input) {

            if (input.files && input.files[0]) {
                //PRIMERA IMAGEN
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#imgATMDesarmadoPS').css('visibility', 'visible');
                    $('#imgATMDesarmadoPS').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
                //PRIMERA IMAGEN              
            }
        }
        //IMAGEN2
        //IMAGEN3
        function img3(input) {

            if (input.files && input.files[0]) {
                //PRIMERA IMAGEN
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#imgATMDesarmadoPI').css('visibility', 'visible');
                    $('#imgATMDesarmadoPI').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
                //PRIMERA IMAGEN              
            }
        }
        //IMAGEN3
        //IMAGEN4
        function img4(input) {

            if (input.files && input.files[0]) {
                //PRIMERA IMAGEN
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#imgDispositivoVendor').css('visibility', 'visible');
                    $('#imgDispositivoVendor').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
                //PRIMERA IMAGEN              
            }
        }
        //IMAGEN4
        //IMAGEN5
        function img5(input) {

            if (input.files && input.files[0]) {
                //PRIMERA IMAGEN
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#imgSYSTEMINFO').css('visibility', 'visible');
                    $('#imgSYSTEMINFO').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
                //PRIMERA IMAGEN              
            }
        }
        //IMAGEN5   
        //IMAGEN6
        function img6(input) {

            if (input.files && input.files[0]) {
                //PRIMERA IMAGEN
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#imgAntiskimmin').css('visibility', 'visible');
                    $('#imgAntiskimmin').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
                //PRIMERA IMAGEN              
            }
        }
        //IMAGEN6  
        //IMAGEN7
        function img7(input) {

            if (input.files && input.files[0]) {
                //PRIMERA IMAGEN
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#imgMonitorFiltro').css('visibility', 'visible');
                    $('#imgMonitorFiltro').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
                //PRIMERA IMAGEN              
            }
        }
        //IMAGEN7
        //IMAGEN8
        function img8(input) {

            if (input.files && input.files[0]) {
                //PRIMERA IMAGEN
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#imgPadlewheel').css('visibility', 'visible');
                    $('#imgPadlewheel').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
                //PRIMERA IMAGEN              
            }
        }
        //IMAGEN8
        //IMAGEN9
        function img9(input) {

            if (input.files && input.files[0]) {
                //PRIMERA IMAGEN
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#imgDispDesarmado').css('visibility', 'visible');
                    $('#imgDispDesarmado').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
                //PRIMERA IMAGEN              
            }
        }
        //IMAGEN9
        //IMAGEN10
        function img10(input) {

            if (input.files && input.files[0]) {
                //PRIMERA IMAGEN
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#imgTeclado').css('visibility', 'visible');
                    $('#imgTeclado').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
                //PRIMERA IMAGEN              
            }
        }
        //IMAGEN10
        //IMAGEN11
        function img11(input) {

            if (input.files && input.files[0]) {
                //PRIMERA IMAGEN
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#imgClimatizacion').css('visibility', 'visible');
                    $('#imgClimatizacion').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
                //PRIMERA IMAGEN              
            }
        }
        //IMAGEN11
        //IMAGEN12
        function img12(input) {

            if (input.files && input.files[0]) {
                //PRIMERA IMAGEN
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#imgEnergia').css('visibility', 'visible');
                    $('#imgEnergia').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
                //PRIMERA IMAGEN              
            }
        }
        //IMAGEN12

    </script>
    <%--IMAGENES--%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div class="row page-titles">
        <div class="col-md-5 align-self-center">
            <h4 class="text-themecolor">Aprobar lista de verificación de ATMs</h4>
        </div>
        <div class="col-md-7 align-self-center text-right">
            <div class="d-flex justify-content-end align-items-center">
            </div>
        </div>
    </div>

    <div class="card">
        <br />
        <div class="row col-12" style="margin-left: 10px; margin-left: 10px;">
            <h3 class="text-themecolor" style="color: #808080;"><i class="fa fa-book"></i>Datos Generales</h3>
        </div>
        <hr />
        <br />
        <div class="row col-12" style="margin: 10px 10px 10px 10px">

            <!--SEGUNDA FILA-->
            <div class="row col-12">
                <div class="row col-6">
                    <label class="col col-form-label col-6">Hora Salida de Infatlan</label>
                    <div class="row col-12">
                        <asp:TextBox ID="txthsalidaInfa" placeholder="00:00:00" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="row col-6">
                    <label class="col col-form-label col-6">Hora llegada a Infatlan</label>
                    <div class="row col-12">
                        <asp:TextBox ID="txtHllegadaInfatlan" placeholder="00:00:00" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                </div>
            </div>
            <!--/SEGUNDA FILA-->
            <!--TERCERA FILA-->
            <div class="row col-12">
                <div class="row col-6">

                    <label class="col-form-label col-6">Inicio de mantenimiento</label>
                    <div class="row col-12">
                        <asp:TextBox ID="TxFechaInicio" placeholder="00:00:00" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="row col-6">
                    <label class=" col-form-label col-6">Finaliza mantenimiento</label>
                    <div class="row col-12">
                        <asp:TextBox ID="TxFechaRegreso" placeholder="00:00:00" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                </div>
            </div>
            <!--FIN TERCERA FILA-->
            <!--CUARTA FILA-->
            <div class="row col-12">
                <div class="row col-6">
                    <label class="col-form-label col-6">Lugar(Nombre)</label>
                    <div class="row col-12">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtnomATM" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="row col-6">
                    <label class="col-form-label col-6">SysAid</label>
                    <div class="row col-12">
                        <asp:TextBox CssClass="form-control" ID="txtsysaid" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                </div>
            </div>
            <!--/CUARTA FILA-->
            <!--QUINTA FILA-->
            <div class="row col-12">
                <div class="row col-6">
                    <label class="col-form-label col-6">Ubicación de ATM</label>
                    <div class="row col-12">
                        <asp:TextBox CssClass="form-control" ID="txtUbicacionATM" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="row col-6">
                    <label class="col-form-label col-6">Código de ATM</label>
                    <div class="row col-12">
                        <asp:TextBox CssClass="form-control" ID="txtcodATM" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                </div>
            </div>
            <!--/QUINTA FILA-->
            <!--SEXTA FILA-->
            <div class="row col-12">
                <div class="row col-6">
                    <label class="col-form-label col-6">Dirección</label>
                    <div class="row col-12">
                        <asp:TextBox CssClass="form-control" ID="txtdireccion" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="row col-6">
                    <label class="col-form-label col-6">Sucursal</label>
                    <div class="row col-12">
                        <asp:TextBox CssClass="form-control" ID="txtsucursal" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                </div>
            </div>
            <!--/SEXTA FILA-->
            <!--SEPTIMA FILA-->
            <div class="row col-12">
                <div class="row col-6">
                    <label class="col-form-label col-6">Dirección IP</label>
                    <div class="row col-12">
                        <asp:TextBox CssClass="form-control" ID="txtip" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="row col-6">
                    <label class="col-form-label col-6">Zona</label>
                    <div class="row col-12">
                        <asp:TextBox CssClass="form-control" ID="txtzonaVerif" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                </div>
            </div>
            <!--/SEPTIMA FILA-->
            <!--OCTAVA FILA-->
            <div class="row col-12">
                <div class="row col-6">
                    <label class="col-form-label col-6">Impacto</label>
                    <div class="row col-12">
                        <asp:TextBox CssClass="form-control" ID="txtimpacto" runat="server" Enabled="false" Text="Durante la ventana de mantenimiento el ATM estará fuera de linea"></asp:TextBox>
                    </div>
                </div>
                <div class="row col-6">
                    <label class="col-form-label col-6">Motivo</label>
                    <div class="row col-12">
                        <asp:TextBox CssClass="form-control" ID="txtmotivoVerif" runat="server" Enabled="false" Text="Realizar acciones pro activas para prevenir la falla"></asp:TextBox>
                    </div>
                </div>
            </div>
            <!--/OCTAVA FILA-->
        </div>
        <br />
        <br />
        <div class="row col-12" style="margin-left: 10px; margin-left: 10px;">
            <h3 class="text-themecolor" style="color: #808080;"><i class="fa fa-user" style="margin-left: 10px"></i>Técnico Responsable</h3>
        </div>
        <br />
        <hr />
        <!--PRIMERA FILA-->

        <div class="row col-12" style="margin: 10px 10px 10px 10px">
            <div class="row col-12">
                <div class="row col-6">
                    <label class="col-form-label col-6">Técnico Responsable</label>
                    <div class="row col-12">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtTecnicoResponsable" Enabled="false"></asp:TextBox>

                    </div>
                </div>

                <div class="row col-6">
                    <label class=" col-form-label col-6">Identidad</label>
                    <div class="row col-12">
                        <asp:TextBox CssClass="form-control" ID="txtidentidad" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
        <!--/PRIMERA FILA-->
        <br />
        <br />
        <div class="row col-12" style="margin-left: 10px; margin-left: 10px;">
            <h3 class="text-themecolor" style="color: #808080;"><i class=" fa fa-wrench" style="margin-left: 10px"></i>Proceso de Mantenimiento</h3>
        </div>
        <br />
        <hr />
       <div class="row col-12" style="margin: 10px 10px 10px 10px">
            <div class="row col-12">
                <h5 class="text-themecolor col-12" style="color: #808080;"><i class="fa fa-check-square" style="margin-left: 10px"></i>Pasos del mantenimiento</h5>
                <br />
                <br />
                <div class="row col-5">
                    <asp:CheckBoxList ID="ckpasos1" runat="server" Enabled="false" CssClass="check green col-12" data-checkbox="icheckbox_flat-green">
                        <asp:ListItem Value="1" Text="1. Verificar que ATM este en servicio antes de apagarlo"></asp:ListItem>                       
                    </asp:CheckBoxList>
                    <asp:CheckBoxList ID="ckpasos2" runat="server" Enabled="false" CssClass="check green col-12" data-checkbox="icheckbox_flat-green">                       
                        <asp:ListItem Value="2" Text="2. Solicitar al encargado del ATM sacar contadores"></asp:ListItem>                       
                    </asp:CheckBoxList>
                    <asp:CheckBoxList ID="ckpasos3" runat="server" Enabled="false" CssClass="check green col-12" data-checkbox="icheckbox_flat-green">                       
                        <asp:ListItem Value="3" Text="3. Solicitar al encargado del ATM sacar maleta"></asp:ListItem>                       
                    </asp:CheckBoxList>
                     <asp:CheckBoxList ID="ckpasos4" runat="server" Enabled="false" CssClass="check green col-12" data-checkbox="icheckbox_flat-green">                                               
                        <asp:ListItem Value="4" Text="4. Apagar ATM de forma correcta"></asp:ListItem>                   
                    </asp:CheckBoxList>
                    <asp:CheckBoxList ID="ckpasos5" runat="server" Enabled="false" CssClass="check green col-12" data-checkbox="icheckbox_flat-green">                                                                      
                        <asp:ListItem Value="5" Text="5. Retirar CPU"></asp:ListItem>                       
                    </asp:CheckBoxList>
                     <asp:CheckBoxList ID="ckpasos6" runat="server" Enabled="false" CssClass="check green col-12" data-checkbox="icheckbox_flat-green">                                                                                             
                        <asp:ListItem Value="6" Text="6. Retirar Monitor"></asp:ListItem>                       
                    </asp:CheckBoxList>
                     <asp:CheckBoxList ID="ckpasos7" runat="server" Enabled="false" CssClass="check green col-12" data-checkbox="icheckbox_flat-green">                                                                                                                     
                        <asp:ListItem Value="7" Text="7. Retirar la lectora"></asp:ListItem>                       
                    </asp:CheckBoxList>
                    <asp:CheckBoxList ID="ckpasos8" runat="server" Enabled="false" CssClass="check green col-12" data-checkbox="icheckbox_flat-green">                                                                                                                     
                        <asp:ListItem Value="8" Text="8. Retirar presentador"></asp:ListItem>                      
                    </asp:CheckBoxList>
                    <asp:CheckBoxList ID="ckpasos9" runat="server" Enabled="false" CssClass="check green col-12" data-checkbox="icheckbox_flat-green">                                                                                                                                            
                        <asp:ListItem Value="9" Text="9. Limpieza de stacker"></asp:ListItem>                    
                    </asp:CheckBoxList>
                     <asp:CheckBoxList ID="ckpasos10" runat="server" Enabled="false" CssClass="check green col-12" data-checkbox="icheckbox_flat-green">                                                                                                                                                                   
                        <asp:ListItem Value="10" Text="10. Limpieza de pickers"></asp:ListItem>
                    </asp:CheckBoxList>
                </div>
                <div class="row col-7">
                    <asp:CheckBoxList ID="ckpasos11" runat="server" Enabled="false" CssClass="check green col-12" data-checkbox="icheckbox_flat-green">
                        <asp:ListItem Value="11" Text="11. Sopletear todos los dispositivos"></asp:ListItem>                      
                    </asp:CheckBoxList>
                     <asp:CheckBoxList ID="ckpasos12" runat="server" Enabled="false" CssClass="check green col-12" data-checkbox="icheckbox_flat-green">                       
                        <asp:ListItem Value="12" Text="12. Limpieza general por cada dispositivo"></asp:ListItem>
                    </asp:CheckBoxList>
                     <asp:CheckBoxList ID="ckpasos13" runat="server" Enabled="false" CssClass="check green col-12" data-checkbox="icheckbox_flat-green">                                               
                        <asp:ListItem Value="13" Text="13. Limpieza de pantalla con espuma"></asp:ListItem>                       
                    </asp:CheckBoxList>
                     <asp:CheckBoxList ID="ckpasos14" runat="server" Enabled="false" CssClass="check green col-12" data-checkbox="icheckbox_flat-green">                                                                      
                        <asp:ListItem Value="14" Text="14. Realizar cambios de repuesto"></asp:ListItem>
                    </asp:CheckBoxList>
                    <asp:CheckBoxList ID="ckpasos15" runat="server" Enabled="false" CssClass="check green col-12" data-checkbox="icheckbox_flat-green">                                                                                              
                        <asp:ListItem Value="15" Text="15. Armar ATM"></asp:ListItem>                       
                    </asp:CheckBoxList>
                    <asp:CheckBoxList ID="ckpasos16" runat="server" Enabled="false" CssClass="check green col-12" data-checkbox="icheckbox_flat-green">                                                                                                                      
                        <asp:ListItem Value="16" Text="16. Encender ATM"></asp:ListItem>                      
                    </asp:CheckBoxList>
                    <asp:CheckBoxList ID="ckpasos17" runat="server" Enabled="false" CssClass="check green col-12" data-checkbox="icheckbox_flat-green">                                                                                                                                              
                        <asp:ListItem Value="17" Text="17. Solicitar que ingresen maletas"></asp:ListItem>
                    </asp:CheckBoxList>
                     <asp:CheckBoxList ID="ckpasos18" runat="server" Enabled="false" CssClass="check green col-12" data-checkbox="icheckbox_flat-green">                                                                                                                                                                     
                        <asp:ListItem Value="18" Text="18. Pruebas con los dospositivos(en el vendor)"></asp:ListItem>                     
                    </asp:CheckBoxList>
                     <asp:CheckBoxList ID="ckpasos19" runat="server" Enabled="false" CssClass="check green col-12" data-checkbox="icheckbox_flat-green">                                                                                                                                                                                            
                        <asp:ListItem Value="19" Text="19. Dejar en servicio el ATM"></asp:ListItem>                       
                    </asp:CheckBoxList>
                     <asp:CheckBoxList ID="ckpasos20" runat="server" Enabled="false" CssClass="check green col-12" data-checkbox="icheckbox_flat-green">                                                                                                                                                                                             
                        <asp:ListItem Value="20" Text="20. Contractarse con personal de ATM y verificar que camaras están en linea"></asp:ListItem>
                    </asp:CheckBoxList>
                </div>
            </div>
        </div>
        <br />





        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="row col-12" style="margin: 10px 10px 10px 10px">
                    <h5 class="text-themecolor" style="color: #808080;"><i class="fa fa-check-square" style="margin-left: 10px"></i>Datos de ATM</h5>
                    <br />
                    <!--PRIMERA FILA DESHABILITADA-->
                    <div class="row col-12">
                        <div class="row col-4">
                            <label class="col-form-label col-12">Sistema Operativo</label>
                            <div class="row col-12">
                                <asp:TextBox runat="server" ID="txtsoVerif" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row col-4">
                            <label class="col-form-label col-12">Versión del software</label>
                            <div class="row col-12">
                                <asp:TextBox runat="server" ID="txtversionswVerif" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row col-4">
                            <label class="col-form-label col-12">Puerto</label>
                            <div class="row col-12">
                                <asp:TextBox runat="server" ID="txtpuertoVerif" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <!--/PRIMERA FILA DESHABILITADA-->
                    <!--PRIMERA FILA-->
                    <div class="row col-12">
                        <br />
                        <div class="row col-4">
                            <label class="col-form-label col-12">Tipo de Teclado</label>
                            <div class="row col-12">
                               <asp:TextBox runat="server" CssClass="form-control" ID="txtTecladoVerif" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row col-4">
                            <label class="col-form-label col-12">Tipo de Procesador</label>
                            <div class="row col-12">
                               <asp:TextBox runat="server" CssClass="form-control" ID="txtTipoProcesadorVerif" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row col-4">
                            <label class="col-form-label col-12">Tipo de Carga</label>
                            <div class="row col-12">
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtTipoCargaVerif" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <!--/PRIMERA FILA-->
                    <!--SEGUNDA FILA-->
                    <div class="row col-12">
                        <div class="row col-4">
                            <label class="col-form-label col-12">Marca del disco duro</label>
                            <div class="row col-12">
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtmarcaVerif" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row col-4">
                            <label class="col-form-label col-12">Número Serie de Disco Duro</label>
                            <div class="row col-12">
                                <asp:TextBox runat="server" ID="txtSerieDiscoDuro" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row col-4">
                            <label class="col-form-label col-12">Capacidad de disco duro(GB)</label>
                            <div class="row col-12">
                                <asp:TextBox runat="server" ID="txtcapacidadDiscoVerif" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <!--/SEGUNDA FILA-->
                    <!--QUINTA FILA-->
                    <div class="row col-12">


                        <div class="row col-4">
                            <label class="col-form-label col-12">Serie ATM</label>
                            <div class="row col-12">
                                <asp:TextBox runat="server" ID="txtserieATM" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row col-4">
                            <label class="col-form-label col-12">Inventario ATM</label>
                            <div class="row col-12">
                                <asp:TextBox runat="server" ID="txtinventarioVerif" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row col-4">
                            <label class="col-form-label col-12">RAM(GB)</label>
                            <div class="row col-12">
                                <asp:TextBox runat="server" ID="txtramVerif" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <!--/QUINTA FILA-->
                    <!--SEXTA FILA-->
                    <div class="row col-12">

                        <div class="row col-4">
                            <label class="col-form-label col-12">Latitud ATM</label>
                            <div class="row col-12">
                                <asp:TextBox runat="server" ID="txtlatitudATM" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row col-4">
                            <label class="col-form-label col-12">Longitud ATM</label>
                            <div class="row col-12">
                                <asp:TextBox runat="server" ID="txtlongitudATM" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <!--/SEXTA FILA-->
                    <!--TERCERA FILA-->
                    <div class="row col-12">
                        <div class="row col-4">
                            <label class="col-form-label col-12">¿Lectora tiene antiskimming?</label>
                            <div class="row col-12">
                                <asp:TextBox runat="server" ID="txtPreguntaAntiskimming" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row col-8">
                            <label class="col-form-label col-12">Comentario por lectora sin antiskimming</label>
                            <div class="row col-12">
                                <asp:TextBox runat="server" ID="txtantiSkimming" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <!--/TERCERA FILA-->

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <br />
        <hr />
        <br />
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="card-body">
                    <h3 class="card-title" style="color: #808080;"><i class="fa fa-image" style="margin-left: 10px"></i>Comprobación</h3>
                    <h5 class="card-subtitle">Subir imagenes de lo solicitado de mantenimiento</h5>
                    <table class="tablesaw table-bordered table-hover table no-wrap" data-tablesaw-mode="swipe"
                        data-tablesaw-sortable data-tablesaw-sortable-switch data-tablesaw-minimap
                        data-tablesaw-mode-switch>
                        <thead>
                            <tr>
                                <th scope="col" data-tablesaw-sortable-col data-tablesaw-priority="persist" class="border">Imagen a subir</th>
                                <th scope="col" data-tablesaw-sortable-col data-tablesaw-sortable-default-col
                                    data-tablesaw-priority="3" class="border">¿Subir imagen?</th>
                                <th scope="col" data-tablesaw-sortable-col data-tablesaw-priority="2" class="border">Seleccione imagen
                                </th>
                                <th scope="col" data-tablesaw-sortable-col data-tablesaw-priority="1" class="border">
                                    <abbr title="Rotten Tomato Rating">Mostrar imagen</abbr>
                                </th>

                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td class="title"><a class="link" href="javascript:void(0)">Disco duro</a></td>
                                <td>
                                    <asp:RadioButtonList ID="RBLDiscoDuro" runat="server" CssClass="custom-checkbox" BorderStyle="None" Enabled="false" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="No"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    <asp:FileUpload ID="FUDiscoDuro" Enabled="false" runat="server" onchange="img1(this);" /></td>
                                <td>
                                    <%--<img id="imgDiscoDuro" height="100" width="100" src="" style="border-width: 0px; visibility: hidden;" />--%>
                                    <asp:Image ID="imgDiscoDuro" runat="server" height="100" width="100" src="" style="border-width: 0px; visibility: hidden;"></asp:Image>
                                </td>
                                
                            </tr>
                            <tr>
                                <td class="title"><a class="link" href="javascript:void(0)">ATM desarmado parte superior (limpiar)</a></td>
                                <td>
                                    <asp:RadioButtonList ID="RBLATMDesarmadoPS" runat="server" CssClass="custom-checkbox" BorderStyle="None" Enabled="false" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="No"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    <asp:FileUpload ID="FUATMDesarmadoPS" Enabled="false" runat="server" onchange="img2(this);" /></td>
                                <td>
                                    <img id="imgATMDesarmadoPS" height="100" width="100" src="" style="border-width: 0px; visibility: hidden;" /></td>

                            </tr>
                            <tr>
                                <td class="title"><a class="link" href="javascript:void(0)">ATM desarmado parte inferior (limpiar)</a>
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="RBLATMDesarmadoPI" runat="server" CssClass="custom-checkbox" BorderStyle="None" Enabled="false" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="No"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    <asp:FileUpload ID="FUATMDesarmadoPI" Enabled="false" runat="server" onchange="img3(this);" /></td>
                                <td>
                                    <img id="imgATMDesarmadoPI" height="100" width="100" src="" style="border-width: 0px; visibility: hidden;" /></td>

                            </tr>
                            <tr>
                                <td class="title"><a class="link" href="javascript:void(0)">Dispositivo modo
                                    <br />
                                    diagnostico de vendor en linea</a></td>
                                <td>
                                    <asp:RadioButtonList ID="RBLVendor" runat="server" CssClass="custom-checkbox" BorderStyle="None" Enabled="false" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="No"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    <asp:FileUpload ID="FUDispositivoVendor" Enabled="false" runat="server" onchange="img4(this);" /></td>
                                <td>
                                    <img id="imgDispositivoVendor" height="100" width="100" src="" style="border-width: 0px; visibility: hidden;" /></td>

                            </tr>
                            <tr>
                                <td class="title"><a class="link" href="javascript:void(0)">Tipo de procesador con<br />
                                    el comando "SYSTEMINFO"</a></td>
                                <td>
                                    <asp:RadioButtonList ID="RBLSystemInfo" runat="server" CssClass="custom-checkbox" BorderStyle="None" Enabled="false" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="No"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    <asp:FileUpload ID="FUSYSTEMINFO" Enabled="false" runat="server" onchange="img5(this);" /></td>
                                <td>
                                    <img id="imgSYSTEMINFO" height="100" width="100" src="" style="border-width: 0px; visibility: hidden;" /></td>

                            </tr>
                            <tr>
                                <td class="title"><a class="link" href="javascript:void(0)">Lectora con el antiskimming
                                    <br />
                                    desarmado y limpio</a>
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="RBLAntiSkimming" runat="server" CssClass="custom-checkbox" BorderStyle="None" Enabled="false" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="No"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    <asp:FileUpload ID="FUAntiskimmin" Enabled="false" runat="server" onchange="img6(this);" /></td>
                                <td>
                                    <img id="imgAntiskimmin" height="100" width="100" src="" style="border-width: 0px; visibility: hidden;" /></td>

                            </tr>
                            <tr>
                                <td class="title"><a class="link" href="javascript:void(0)">Monitor con el filtro
                                </a></td>
                                <td>
                                    <asp:RadioButtonList ID="RBLMonitorFiltro" runat="server" CssClass="custom-checkbox" BorderStyle="None" Enabled="false" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="No"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    <asp:FileUpload ID="FUMonitorFiltro" Enabled="false" runat="server" onchange="img7(this);" /></td>
                                <td>
                                    <img id="imgMonitorFiltro" height="100" width="100" src="" style="border-width: 0px; visibility: hidden;" /></td>

                            </tr>
                            <tr>
                                <td class="title"><a class="link" href="javascript:void(0)">PadleWheel(rueda de paletas)</a></td>
                                <td>
                                    <asp:RadioButtonList ID="RBLPadleWheel" runat="server" CssClass="custom-checkbox" BorderStyle="None" Enabled="false" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="No"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    <asp:FileUpload ID="FUPadlewheel" Enabled="false" runat="server" onchange="img8(this);" /></td>
                                <td>
                                    <img id="imgPadlewheel" height="100" width="100" src="" style="border-width: 0px; visibility: hidden;" /></td>

                            </tr>
                            <tr>
                                <td class="title"><a class="link" href="javascript:void(0)">Dispositivos desarmado</a></td>
                                <td>
                                    <asp:RadioButtonList ID="RBLDispDesarmado" runat="server" CssClass="custom-checkbox" BorderStyle="None" Enabled="false" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="No"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    <asp:FileUpload ID="FUDispDesarmado" Enabled="false" runat="server" onchange="img9(this);" /></td>
                                <td>
                                    <img id="imgDispDesarmado" height="100" width="100" src="" style="border-width: 0px; visibility: hidden;" /></td>

                            </tr>
                            <tr>
                                <td class="title"><a class="link" href="javascript:void(0)">Teclado
                                </a></td>
                                <td>
                                    <asp:RadioButtonList ID="RBLTeclado" runat="server" CssClass="custom-checkbox" BorderStyle="None" Enabled="false" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="No"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    <asp:FileUpload ID="FUTeclado" Enabled="false" runat="server" onchange="img10(this);" /></td>
                                <td>
                                    <img id="imgTeclado" height="100" width="100" src="" style="border-width: 0px; visibility: hidden;" /></td>

                            </tr>
                            <tr>
                                <td class="title"><a class="link" href="javascript:void(0)">¿Cuenta con Climatización adecuada?
                                </a></td>
                                <td>
                                    <asp:RadioButtonList ID="RBLClimatizacion" Enabled="false" runat="server" AutoPostBack="true" CssClass="custom-checkbox" BorderStyle="None" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="No"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    <asp:FileUpload ID="FUClimatizacion" runat="server" Enabled="false" onchange="img11(this);" /></td>
                                <td>
                                    <img id="imgClimatizacion" height="100" width="100" src="" style="border-width: 0px; visibility: hidden;" /></td>

                            </tr>
                            <tr>
                                <td class="title"><a class="link" href="javascript:void(0)">¿Cuenta con protección de energía<br />
                                    eléctrica?
                                </a></td>
                                <td>
                                    <asp:RadioButtonList ID="RBLEnergiaElectrica" Enabled="false" runat="server" AutoPostBack="true" CssClass="custom-checkbox" BorderStyle="None" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="No"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    <asp:FileUpload ID="FUEnergia" runat="server" Enabled="false" onchange="img12(this);" /></td>
                                <td>
                                    <img id="imgEnergia" class="col row-6" height="100" width="100" src="" style="border-width: 0px; visibility: hidden;" /></td>

                            </tr>
                        </tbody>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="row col-12" style="margin-left: 10px; margin-left: 10px;">
            <label class="col-form-label">Observaciones y Comentarios</label>
            <br />
            <div class="row col-11 align-self-center">
                <asp:TextBox runat="server" ID="txtobseracionesVerif" Enabled="false" TextMode="MultiLine" Rows="3" PlaceHolder="Ingrese su comentario..." CssClass="form-control col-12"></asp:TextBox>
            </div>
        </div>
        <br />
        <br />
        <asp:UpdatePanel ID="UPEnviarVerif" runat="server">
            <ContentTemplate>
                <div class="col-md-4 align-self-center" style="margin-left: auto; margin-right: auto">
                    <asp:Button runat="server" ID="btnEnviarVerif" OnClick="btnEnviarVerif_Click" CssClass="btn btn-rounded btn-block btn-outline-success" Text="Enviar" />
                </div>

            </ContentTemplate>
                    <%--<Triggers>
                        <asp:PostBackTrigger ControlID="btnEnviarVerif" />
                    </Triggers>--%>
        </asp:UpdatePanel>
        <br />
        <br />

    </div>
    <!--MODAL VERIF ATM -->
    <div class="modal bs-example-modal-lg" id="modalVerifATM" tabindex="-1" data-backdrop="static" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myLargeModalLabel">¿Seguro que guardará lista de verificación?</h4>
                </div>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
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
                            <asp:Label runat="server" BorderStyle="None" class="col form-control col-6"><strong>Inventario de ATM: </strong>  </asp:Label>
                            <asp:Label runat="server" BorderStyle="None" ID="lbInventarioATM" class="col form-control col-6"></asp:Label>
                        </div>
                        <div class="row col-12">
                            <asp:Label runat="server" BorderStyle="None" class="col form-control col-6"><strong>Técnico responsable: </strong>  </asp:Label>
                            <asp:Label runat="server" BorderStyle="None" ID="lbtecnico" class="col form-control col-6"></asp:Label>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <div class="modal-footer col-12">
                            <div class="row col-3">
                                <asp:Button runat="server" ID="btnModalVerif" UseSubmitBehavior="false" OnClick="btnModalVerif_Click"  CssClass="btn btn-success mr-3" Text="Enviar" />
                            </div>
                            <div class="row col-3">
                                <asp:Button runat="server" ID="btnModalCerrarVerif" OnClick="btnModalCerrarVerif_Click"  CssClass="btn btn-danger mr-3" Text="Cancelar" />
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnModalVerif" />
                    </Triggers>
                </asp:UpdatePanel>

            </div>
            <!-- /.modal-content -->
        </div>
        <!--/.modal-dialog -->
    </div>
    <!-- /MODAL VERIF ATM -->

    <!--MODAL IMAGENES-->
    <div class="modal bs-example-modal-lg" id="modalVerifIMGATM" tabindex="-1" data-backdrop="static" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 runat="server" class="modal-title" id="h4Pregunta"></h4>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="row col-12">
                            <img id="imgModal" height="400" width="400" src="" style="border-width: 0px; visibility: hidden;" /></td>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <div class="modal-footer col-12">                          
                                <asp:Button runat="server" ID="btnCerrarImg" CssClass="btn btn-danger mr-3" Text="Cerrar" />
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnModalVerif" />
                    </Triggers>
                </asp:UpdatePanel>

            </div>
            <!-- /.modal-content -->
        </div>
        <!--/.modal-dialog -->
    </div>
    <!-- /MODAL IMAGENES -->

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <script src="/assets/node_modules/icheck/icheck.min.js"></script>
    <script src="/assets/node_modules/icheck/icheck.init.js"></script>
    <script src="/assets/node_modules/dropify/dist/js/dropify.min.js"></script>
    <script>
        $(document).ready(function () {
            // Basic
            $('.dropify').dropify();

            // Translated
            $('.dropify-fr').dropify({
                messages: {
                    default: 'Glissez-déposez un fichier ici ou cliquez',
                    replace: 'Glissez-déposez un fichier ou cliquez pour remplacer',
                    remove: 'Supprimer',
                    error: 'Désolé, le fichier trop volumineux'
                }
            });

            // Used events
            var drEvent = $('#input-file-events').dropify();

            drEvent.on('dropify.beforeClear', function (event, element) {
                return confirm("Do you really want to delete \"" + element.file.name + "\" ?");
            });

            drEvent.on('dropify.afterClear', function (event, element) {
                alert('File deleted');
            });

            drEvent.on('dropify.errors', function (event, element) {
                console.log('Has Errors');
            });

            var drDestroy = $('#input-file-to-destroy').dropify();
            drDestroy = drDestroy.data('dropify')
            $('#toggleDropify').on('click', function (e) {
                e.preventDefault();
                if (drDestroy.isDropified()) {
                    drDestroy.destroy();
                } else {
                    drDestroy.init();
                }
            })
        });
    </script>
    <script src="/assets/node_modules/sticky-kit-master/dist/sticky-kit.min.js"></script>


    <%--    <script src="/assets/node_modules/jquery/jquery-3.2.1.min.js"></script>
    <!-- Bootstrap tether Core JavaScript -->
    <script src="/assets/node_modules/popper/popper.min.js"></script>
    <script src="/assets/node_modules/bootstrap/dist/js/bootstrap.min.js"></script>
    <!-- slimscrollbar scrollbar JavaScript -->
    <script src="/js/perfect-scrollbar.jquery.min.js"></script>
    <!--Wave Effects -->
    <script src="/js/waves.js"></script>
    <!--Menu sidebar -->
    <script src="/js/sidebarmenu.js"></script>
    <!--stickey kit -->
    <script src="/assets/node_modules/sparkline/jquery.sparkline.min.js"></script>
    <!--Custom JavaScript -->
    <script src="/js/custom.min.js"></script>
    <!-- icheck -->--%>


    <!--FILEUP-->

    <!--Custom JavaScript -->
    <script src="/js/custom.min.js"></script>
    <!-- ============================================================== -->
    <!-- Plugins for this page -->
    <!-- ============================================================== -->
    <!-- jQuery file upload -->

    <!--FILEUP-->
</asp:Content>
