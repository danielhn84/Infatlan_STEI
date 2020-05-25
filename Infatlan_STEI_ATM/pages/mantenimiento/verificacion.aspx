<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="verificacion.aspx.cs" Inherits="Infatlan_STEI_ATM.pages.mantenimiento.verificacion" %>

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
        function openModal2() { $('#modalRechazar').modal('show'); }
    </script>
    <!--PARA CERRAR MODAL-->
    <script type="text/javascript">
        function closeModal() { $('#modalVerifATM').modal('hide'); }
        function closeModal2() { $('#modalRechazar').modal('hide'); }
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
                    //$('#imgDiscoDuro').css('visibility', 'visible');
                    //$('#imgDiscoDuro').attr('src', e.target.result);  
                    var ruta1 = e.target.result;
                    document.getElementById('<%=imgDiscoDuro.ClientID%>').src = ruta1;
                    document.getElementById('<%=HFDiscoDuro.ClientID%>').value = 'si';
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
                    //$('#imgATMDesarmadoPS').css('visibility', 'visible');
                    //$('#imgATMDesarmadoPS').attr('src', e.target.result);
                    var ruta2 = e.target.result;
                    document.getElementById('<%=imgATMDesarmadoPS.ClientID%>').src = ruta2;
                    document.getElementById('<%=HFATMDesarmadoPS.ClientID%>').value = 'si';
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
                    //$('#imgATMDesarmadoPI').css('visibility', 'visible');
                    //$('#imgATMDesarmadoPI').attr('src', e.target.result);
                    var ruta3 = e.target.result;
                    document.getElementById('<%=imgATMDesarmadoPI.ClientID%>').src = ruta3;
                    document.getElementById('<%=HFATMDesarmadoPI.ClientID%>').value = 'si';
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
                    //$('#imgDispositivoVendor').css('visibility', 'visible');
                    //$('#imgDispositivoVendor').attr('src', e.target.result);
                    var ruta4 = e.target.result;
                    document.getElementById('<%=imgDispositivoVendor.ClientID%>').src = ruta4;
                    document.getElementById('<%=HFDispositivoVendor.ClientID%>').value = 'si';
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
                    //$('#imgSYSTEMINFO').css('visibility', 'visible');
                    //$('#imgSYSTEMINFO').attr('src', e.target.result);
                    var ruta5 = e.target.result;
                    document.getElementById('<%=imgSYSTEMINFO.ClientID%>').src = ruta5;
                    document.getElementById('<%=HFSYSTEMINFO.ClientID%>').value = 'si';
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
                    //$('#imgAntiskimmin').css('visibility', 'visible');
                    //$('#imgAntiskimmin').attr('src', e.target.result);
                    var ruta6 = e.target.result;
                    document.getElementById('<%=imgAntiskimmin.ClientID%>').src = ruta6;
                    document.getElementById('<%=HFAntiskimmin.ClientID%>').value = 'si';
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
                    //$('#imgMonitorFiltro').css('visibility', 'visible');
                    //$('#imgMonitorFiltro').attr('src', e.target.result);
                    var ruta7 = e.target.result;
                    document.getElementById('<%=imgMonitorFiltro.ClientID%>').src = ruta7;
                    document.getElementById('<%=HFMonitorFiltro.ClientID%>').value = 'si';
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
                    //$('#imgPadlewheel').css('visibility', 'visible');
                    //$('#imgPadlewheel').attr('src', e.target.result);
                    var ruta8 = e.target.result;
                    document.getElementById('<%=imgPadlewheel.ClientID%>').src = ruta8;
                    document.getElementById('<%=HFPadlewheel.ClientID%>').value = 'si';
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
                    //$('#imgDispDesarmado').css('visibility', 'visible');
                    //$('#imgDispDesarmado').attr('src', e.target.result);
                    var ruta9 = e.target.result;
                    document.getElementById('<%=imgDispDesarmado.ClientID%>').src = ruta9;
                    document.getElementById('<%=HFDispDesarmado.ClientID%>').value = 'si';
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
                    //$('#imgTeclado').css('visibility', 'visible');
                    //$('#imgTeclado').attr('src', e.target.result);
                    var ruta10 = e.target.result;
                    document.getElementById('<%=imgTeclado.ClientID%>').src = ruta10;
                    document.getElementById('<%=HFTeclado.ClientID%>').value = 'si';
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
                    //$('#imgClimatizacion').css('visibility', 'visible');
                    //$('#imgClimatizacion').attr('src', e.target.result);
                    var ruta11 = e.target.result;
                    document.getElementById('<%=imgClimatizacion.ClientID%>').src = ruta11;  
                    document.getElementById('<%=HFClima.ClientID%>').value = 'si';
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
                    //$('#imgEnergia').css('visibility', 'visible');
                    //$('#imgEnergia').attr('src', e.target.result);
                    var ruta12 = e.target.result;
                    document.getElementById('<%=imgEnergia.ClientID%>').src = ruta12;
                    document.getElementById('<%=HFEnergia.ClientID%>').value = 'si';
                }
                reader.readAsDataURL(input.files[0]);
                //PRIMERA IMAGEN              
            }
        }
        //IMAGEN12
        //IMAGEN13
        function img13(input) {

            if (input.files && input.files[0]) {
                //PRIMERA IMAGEN
                var reader = new FileReader();
                reader.onload = function (e) {
                    //$('#imgTeclado').css('visibility', 'visible');
                    //$('#imgTeclado').attr('src', e.target.result);
                    var ruta13 = e.target.result;
                    document.getElementById('<%=imgATMLinea.ClientID%>').src = ruta13;
                    document.getElementById('<%=HFATMLinea.ClientID%>').value = 'si';
                }
                reader.readAsDataURL(input.files[0]);
                //PRIMERA IMAGEN              
            }
        }
        //IMAGEN13

    </script>
    <%--IMAGENES--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #ffffff; opacity: 0.7; margin: 0;">
                <span style="display: inline-block; height: 100%; vertical-align: middle;"></span>
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="/images/loading.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="display: inline-block; vertical-align: middle;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

     <div class="row page-titles">
        <div class="col-md-5 align-self-center">
            <h3 class="text-themecolor col-12">Lista de verificación</h3>
            <h6 class="text-themecolor col-12">Ingresar datos de mantenimiento de ATM</h6>
        </div>
        <div class="col-md-7 align-self-center text-right">
            <div class="d-flex justify-content-end align-items-center">
            </div>
        </div>
    </div>

    <div class="card">
        <br />
             
         <div class=" col-12 align-self-center" style="margin-left: auto; margin-right: auto">
            <div class="row">
                <div class="col-12 grid-margin stretch-card">
                    <div class="card">
                        <div class="card-body">
                           <div class="row col-12" style="margin-left: 10px; margin-left: 10px;">
                              <h3 class="text-themecolor" style="color: #808080;"><i class="fa fa-book"></i>Datos Generales</h3>
                           </div>                         
            <!--SEGUNDA FILA-->
            <div class="row col-12">
                <div class="row col-6">
                    <label class="col col-form-label col-6">Hora Salida de Infatlan</label>
                    <div class="row col-12">
                        <asp:TextBox ID="txthsalidaInfa" placeholder="00:00:00" CssClass="form-control" runat="server" TextMode="Time"></asp:TextBox>
                    </div>
                </div>
                <div class="row col-6">
                    <label class="col col-form-label col-6">Hora llegada a Infatlan</label>
                    <div class="row col-12">
                        <asp:TextBox ID="txtHllegadaInfatlan" placeholder="00:00:00" CssClass="form-control" runat="server" TextMode="Time"></asp:TextBox>
                    </div>
                </div>
            </div>
            <!--/SEGUNDA FILA-->
            <!--TERCERA FILA-->
            <div class="row col-12">
                <div class="row col-6">
                    <label class="col-form-label col-6">Inicio de mantenimiento</label>
                    <div class="row col-12">
                        <asp:TextBox ID="TxFechaInicio" placeholder="00:00:00" CssClass="form-control" runat="server" TextMode="Time"></asp:TextBox>
                    </div>
                </div>
                <div class="row col-6">
                    <label class=" col-form-label col-6">Finaliza mantenimiento</label>
                    <div class="row col-12">
                        <asp:TextBox ID="TxFechaRegreso" placeholder="00:00:00" CssClass="form-control" runat="server" TextMode="Time"></asp:TextBox>
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
                    </div>
                </div>
            </div>
        </div>
         <div class=" col-12 align-self-center" style="margin-left: auto; margin-right: auto">
            <div class="row">
                <div class="col-12 grid-margin stretch-card">
                    <div class="card">
                        <div class="card-body">
                           <div class="row col-12" style="margin-left: 10px; margin-left: 10px;">
                               <h3 class="text-themecolor" style="color: #808080;"><i class="fa fa-user" style="margin-left: 10px"></i>Técnico Responsable</h3>
                           </div>  
        <!--PRIMERA FILA-->
        
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
        
        <!--/PRIMERA FILA-->
                       </div>
                    </div>
                </div>
            </div>
        </div>

        <div class=" col-12 align-self-center" style="margin-left: auto; margin-right: auto">
            <div class="row">
                <div class="col-12 grid-margin stretch-card">
                    <div class="card">
                        <div class="card-body">
                            <div class="row col-12" style="margin-left: 10px; margin-left: 10px;">
                                <h3 class="text-themecolor" style="color: #808080;"><i class=" fa fa-wrench" style="margin-left: 10px"></i>Proceso de Mantenimiento</h3>
                            </div>
                            
                                <div class="row col-12" runat="server" id="DIVPreguntas">
                                    <%--<h5 class="text-themecolor col-12" style="color: #808080;"><i class="fa fa-check-square" style="margin-left: 10px"></i>Pasos del mantenimiento</h5>--%>
                                    <br />
                                    <br />
                                    <div class="row col-5">
                                        <asp:CheckBoxList ID="ckpasos1" runat="server" CssClass="check green col-12" data-checkbox="icheckbox_flat-green">
                                            <asp:ListItem Value="1" Text="1. Verificar ATM este en servicio antes de apagarlo"></asp:ListItem>
                                        </asp:CheckBoxList>
                                        <asp:CheckBoxList ID="ckpasos2" runat="server" CssClass="check green col-12" data-checkbox="icheckbox_flat-green">
                                            <asp:ListItem Value="2" Text="2. Solicitar al encargado del ATM sacar contadores"></asp:ListItem>
                                        </asp:CheckBoxList>
                                        <asp:CheckBoxList ID="ckpasos3" runat="server" CssClass="check green col-12" data-checkbox="icheckbox_flat-green">
                                            <asp:ListItem Value="3" Text="3. Solicitar al encargado del ATM sacar maleta"></asp:ListItem>
                                        </asp:CheckBoxList>
                                        <asp:CheckBoxList ID="ckpasos4" runat="server" CssClass="check green col-12" data-checkbox="icheckbox_flat-green">
                                            <asp:ListItem Value="4" Text="4. Apagar ATM de forma correcta"></asp:ListItem>
                                        </asp:CheckBoxList>
                                        <asp:CheckBoxList ID="ckpasos5" runat="server" CssClass="check green col-12" data-checkbox="icheckbox_flat-green">
                                            <asp:ListItem Value="5" Text="5. Retirar CPU"></asp:ListItem>
                                        </asp:CheckBoxList>
                                        <asp:CheckBoxList ID="ckpasos6" runat="server" CssClass="check green col-12" data-checkbox="icheckbox_flat-green">
                                            <asp:ListItem Value="6" Text="6. Retirar Monitor"></asp:ListItem>
                                        </asp:CheckBoxList>
                                        <asp:CheckBoxList ID="ckpasos7" runat="server" CssClass="check green col-12" data-checkbox="icheckbox_flat-green">
                                            <asp:ListItem Value="7" Text="7. Retirar la lectora"></asp:ListItem>
                                        </asp:CheckBoxList>
                                        <asp:CheckBoxList ID="ckpasos8" runat="server" CssClass="check green col-12" data-checkbox="icheckbox_flat-green">
                                            <asp:ListItem Value="8" Text="8. Retirar presentador"></asp:ListItem>
                                        </asp:CheckBoxList>
                                        <asp:CheckBoxList ID="ckpasos9" runat="server" CssClass="check green col-12" data-checkbox="icheckbox_flat-green">
                                            <asp:ListItem Value="9" Text="9. Limpieza de stacker"></asp:ListItem>
                                        </asp:CheckBoxList>
                                        <asp:CheckBoxList ID="ckpasos10" runat="server" CssClass="check green col-12" data-checkbox="icheckbox_flat-green">
                                            <asp:ListItem Value="10" Text="10. Limpieza de pickers"></asp:ListItem>
                                        </asp:CheckBoxList>
                                    </div>
                                    <div class="row col-7">
                                        <asp:CheckBoxList ID="ckpasos11" runat="server" CssClass="check green col-12" data-checkbox="icheckbox_flat-green">
                                            <asp:ListItem Value="11" Text="11. Sopletear todos los dispositivos"></asp:ListItem>
                                        </asp:CheckBoxList>
                                        <asp:CheckBoxList ID="ckpasos12" runat="server" CssClass="check green col-12" data-checkbox="icheckbox_flat-green">
                                            <asp:ListItem Value="12" Text="12. Limpieza general por cada dispositivo"></asp:ListItem>
                                        </asp:CheckBoxList>
                                        <asp:CheckBoxList ID="ckpasos13" runat="server" CssClass="check green col-12" data-checkbox="icheckbox_flat-green">
                                            <asp:ListItem Value="13" Text="13. Limpieza de pantalla con espuma"></asp:ListItem>
                                        </asp:CheckBoxList>
                                        <asp:CheckBoxList ID="ckpasos14" runat="server" CssClass="check green col-12" data-checkbox="icheckbox_flat-green">
                                            <asp:ListItem Value="14" Text="14. Realizar cambios de repuesto"></asp:ListItem>
                                        </asp:CheckBoxList>
                                        <asp:CheckBoxList ID="ckpasos15" runat="server" CssClass="check green col-12" data-checkbox="icheckbox_flat-green">
                                            <asp:ListItem Value="15" Text="15. Armar ATM"></asp:ListItem>
                                        </asp:CheckBoxList>
                                        <asp:CheckBoxList ID="ckpasos16" runat="server" CssClass="check green col-12" data-checkbox="icheckbox_flat-green">
                                            <asp:ListItem Value="16" Text="16. Encender ATM"></asp:ListItem>
                                        </asp:CheckBoxList>
                                        <asp:CheckBoxList ID="ckpasos17" runat="server" CssClass="check green col-12" data-checkbox="icheckbox_flat-green">
                                            <asp:ListItem Value="17" Text="17. Solicitar que ingresen maletas"></asp:ListItem>
                                        </asp:CheckBoxList>
                                        <asp:CheckBoxList ID="ckpasos18" runat="server" CssClass="check green col-12" data-checkbox="icheckbox_flat-green">
                                            <asp:ListItem Value="18" Text="18. Pruebas con los dospositivos(en el vendor)"></asp:ListItem>
                                        </asp:CheckBoxList>
                                        <asp:CheckBoxList ID="ckpasos19" runat="server" CssClass="check green col-12" data-checkbox="icheckbox_flat-green">
                                            <asp:ListItem Value="19" Text="19. Dejar en servicio el ATM"></asp:ListItem>
                                        </asp:CheckBoxList>
                                        <asp:CheckBoxList ID="ckpasos20" runat="server" CssClass="check green col-12" data-checkbox="icheckbox_flat-green">
                                            <asp:ListItem Value="20" Text="20. Contractarse con personal de ATM y verificar que camaras están en linea"></asp:ListItem>
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                            
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
        
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class=" col-12 align-self-center" style="margin-left: auto; margin-right: auto">
                    <div class="row">
                        <div class="col-12 grid-margin stretch-card">
                            <div class="card">
                                <div class="card-body">
                    <h3 class="text-themecolor" style="color: #808080;"><i class="fa fa-check-square" style="margin-left: 10px"></i>Datos de ATM</h3>
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
                                <asp:DropDownList ID="DDLtipoTeclado" CssClass="form-control" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row col-4">
                            <label class="col-form-label col-12">Tipo de Procesador</label>
                            <div class="row col-12">
                                <asp:DropDownList ID="DDLtipoProc" CssClass="form-control" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row col-4">
                            <label class="col-form-label col-12">Tipo de Carga</label>
                            <div class="row col-12">
                                <asp:DropDownList ID="DDLtipoCargaVerif" CssClass="form-control" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <!--/PRIMERA FILA-->
                    <!--SEGUNDA FILA-->
                    <div class="row col-12">
                        <div class="row col-4">
                            <label class="col-form-label col-12">Marca del disco duro</label>
                            <div class="row col-12">
                                <asp:DropDownList AutoPostBack="true" ID="DDLmarcaDiscoDuro" CssClass="form-control col-12" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row col-4">
                            <label class="col-form-label col-12">Número Serie de Disco Duro</label>
                            <div class="row col-12">
                                <asp:TextBox runat="server" ID="txtSerieDiscoDuro" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row col-4">
                            <label class="col-form-label col-12">Capacidad de disco duro(GB)</label>
                            <div class="row col-12">
                                <asp:TextBox runat="server" ID="txtcapacidadDiscoVerif" CssClass="form-control" TextMode="Number"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <!--/SEGUNDA FILA-->
                    <!--QUINTA FILA-->
                    <div class="row col-12">


                        <div class="row col-4">
                            <label class="col-form-label col-12">Serie ATM</label>
                            <div class="row col-12">
                                <asp:TextBox runat="server" ID="txtserieATM" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row col-4">
                            <label class="col-form-label col-12">Inventario ATM</label>
                            <div class="row col-12">
                                <asp:TextBox runat="server" ID="txtinventarioVerif" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row col-4">
                            <label class="col-form-label col-12">RAM(GB)</label>
                            <div class="row col-12">
                                <asp:TextBox runat="server" ID="txtramVerif" CssClass="form-control" TextMode="Number"></asp:TextBox>
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
                                <asp:DropDownList AutoPostBack="true" OnTextChanged="dropantiskimming_TextChanged" OnSelectedIndexChanged="dropantiskimming_SelectedIndexChanged" ID="dropantiskimming" CssClass="form-control col-12" runat="server">
                                    <asp:ListItem Value="0" Text="Seleccione opción..."></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="No"></asp:ListItem>
                                </asp:DropDownList>
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
                    </div>
                </div>
            </div>
        </div>
               
            </ContentTemplate>
        </asp:UpdatePanel>

       
         <div class=" col-12 align-self-center" style="margin-left: auto; margin-right: auto">
            <div class="row">
                <div class="col-12 grid-margin stretch-card">
                    <div class="card">
                        <div class="card-body">
                <div class="card-body">
                    <h3 class="card-title" style="color: #808080;"><i class="fa fa-image" style="margin-left: 10px"></i>Comprobación</h3>
                    <h5 class="card-subtitle">Subir imagenes de lo solicitado de mantenimiento</h5>
                    <h6 class="card-subtitle" style="color:red;">Campos con * son obligatorios</h6>
                    <table class="tablesaw table-bordered table-hover table no-wrap" data-tablesaw-mode="swipe"
                        data-tablesaw-sortable data-tablesaw-sortable-switch data-tablesaw-minimap
                        data-tablesaw-mode-switch>
                        <thead>
                            <tr>
                                <th scope="col" style="background-color:#5D6D7E;color:#D5DBDB;" data-tablesaw-sortable-col data-tablesaw-priority="persist" class="border">Imagen a subir</th>                              
                                <th scope="col" style="background-color:#5D6D7E;color:#D5DBDB;" data-tablesaw-sortable-col data-tablesaw-priority="2" class="border">Seleccione imagen </th>
                                <th scope="col" style="background-color:#5D6D7E;color:#D5DBDB;" data-tablesaw-sortable-col data-tablesaw-priority="1" class="border">
                                    <abbr title="Haga click a la imagen para verla más grande">Mostrar imagen</abbr> </th>
                                

                            </tr>
                        </thead>
                        <tbody>  
                           
                            <tr>                               
                                <td class="title"><a class="link" href="javascript:void(0)">*Disco duro</a></td>   
                                <td> <asp:FileUpload ID="FUDiscoDuro" runat="server" onchange="img1(this);" /></td>                                
                                <%--<td runat="server" id="td1img1" style="display:none"><img id="imgDiscoDuro" runat="server" height="150" width="150" src="" style="border-width: 0px;" /></td>--%>
                                <td><img id="imgDiscoDuro" runat="server" height="150" width="150" src="../../assets/images/vistaPrevia1.JPG" style="border-width: 0px;" /></td>                                                          
                            </tr>                              
                            <tr>
                                <td class="title"><a class="link" href="javascript:void(0)">*ATM desarmado parte superior (limpiar)</a></td>                               
                                <td> <asp:FileUpload ID="FUATMDesarmadoPS" runat="server" onchange="img2(this);" /></td>
                                <td> <img id="imgATMDesarmadoPS" runat="server" height="150" width="150" src="../../assets/images/vistaPrevia1.JPG" style="border-width: 0px;" /></td>
                            </tr>
                            <tr>
                                <td class="title"><a class="link" href="javascript:void(0)">*ATM desarmado parte inferior (limpiar)</a> </td>    
                                <td><asp:FileUpload ID="FUATMDesarmadoPI" runat="server" onchange="img3(this);" /></td>
                                <td> <img id="imgATMDesarmadoPI" runat="server" height="150" width="150" src="../../assets/images/vistaPrevia1.JPG" style="border-width: 0px;" /></td>
                            </tr>
                            <tr>
                                <td class="title"><a class="link" href="javascript:void(0)">*Dispositivo modo diagnostico de vendor en linea</a></td>                              
                                <td><asp:FileUpload ID="FUDispositivoVendor" runat="server" onchange="img4(this);" /></td>
                                <td><img id="imgDispositivoVendor" runat="server" height="150" width="150" src="../../assets/images/vistaPrevia1.JPG" style="border-width: 0px;" /></td>
                            </tr>
                            <tr>
                                <td class="title"><a class="link" href="javascript:void(0)">*Tipo de procesador con el comando "SYSTEMINFO"</a></td>                               
                                <td><asp:FileUpload ID="FUSYSTEMINFO" runat="server" onchange="img5(this);" /></td>
                                <td><img id="imgSYSTEMINFO" runat="server" height="150" width="150" src="../../assets/images/vistaPrevia1.JPG" style="border-width: 0px;" /></td>
                            </tr>
                            <tr>
                                <td class="title"><a class="link" href="javascript:void(0)">*Lectora con el antiskimming desarmado y limpio</a></td>           
                                <td><asp:FileUpload ID="FUAntiskimmin" runat="server" onchange="img6(this);" /></td>
                                <td><img id="imgAntiskimmin" runat="server" height="150" width="150" src="../../assets/images/vistaPrevia1.JPG" style="border-width: 0px;" /></td>
                            </tr>
                            <tr>
                                <td class="title"><a class="link" href="javascript:void(0)">*Monitor con el filtro</a></td>                              
                                <td><asp:FileUpload ID="FUMonitorFiltro" runat="server" onchange="img7(this);" /></td>
                                <td><img id="imgMonitorFiltro" runat="server" height="150" width="150" src="../../assets/images/vistaPrevia1.JPG" style="border-width: 0px;" /></td>
                            </tr>
                            <tr>
                                <td class="title"><a class="link" href="javascript:void(0)">*PadleWheel(rueda de paletas)</a></td>                              
                                <td><asp:FileUpload ID="FUPadlewheel" runat="server" onchange="img8(this);" /></td>
                                <td><img id="imgPadlewheel" runat="server" height="150" width="150" src="../../assets/images/vistaPrevia1.JPG" style="border-width: 0px;" /></td>
                            </tr>
                            <tr>
                                <td class="title"><a class="link" href="javascript:void(0)">*Dispositivos desarmado</a></td>
                                <td> <asp:FileUpload ID="FUDispDesarmado" runat="server" onchange="img9(this);" /></td>
                                <td><img id="imgDispDesarmado" runat="server" height="150" width="150" src="../../assets/images/vistaPrevia1.JPG" style="border-width: 0px;" /></td>
                            </tr>
                            <tr>
                                <td class="title"><a class="link" href="javascript:void(0)">*Teclado</a></td>                                
                                <td><asp:FileUpload ID="FUTeclado" runat="server" onchange="img10(this);" /></td>
                                <td><img id="imgTeclado" runat="server" height="150" width="150" src="../../assets/images/vistaPrevia1.JPG" style="border-width: 0px;" /></td>
                            </tr>
                            <tr>                                                            
                                <td class="title"><a class="link" href="javascript:void(0)">¿Cuenta con Climatización adecuada?<br /> </a>
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>                             
                                     <asp:RadioButtonList runat="server" ID="RBClima" AutoPostBack="true" RepeatDirection="Horizontal" CssClass="form-check">
                                        <asp:ListItem Value="1" Text="Si" />
                                        <asp:ListItem Value="2" Text="No" />
                                    </asp:RadioButtonList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>                                
                                <td><asp:FileUpload ID="FUClimatizacion" runat="server" onchange="img11(this);" /></td>                                
                                    <td><img id="imgClimatizacion" runat="server" height="150" width="150" src="../../assets/images/vistaPrevia1.JPG" style="border-width: 0px;" /></td>
                                  </tr>
                            <tr>
                                <td class="title"><a class="link" href="javascript:void(0)">¿Cuenta con protección de energía eléctrica?<br /></a>
                                     <asp:UpdatePanel runat="server">
                                        <ContentTemplate>   
                                    <asp:RadioButtonList runat="server" ID="RBEnergias" AutoPostBack="true" RepeatDirection="Horizontal" CssClass="form-check" style="border-color:transparent">
                                        <asp:ListItem Value="1" Text="Si" />
                                        <asp:ListItem Value="2" Text="No" />
                                    </asp:RadioButtonList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td><asp:FileUpload ID="FUEnergia" runat="server" onchange="img12(this);" /></td>                                     
                                <td><img id="imgEnergia" runat="server" height="150" width="150" src="../../assets/images/vistaPrevia1.JPG" style="border-width: 0px;" /></td>                                
                            </tr>
                            <tr>                                                               
                                <td class="title"><a class="link" href="javascript:void(0)">*ATM en linea</a><br /><asp:TextBox runat="server" ID="txtcomentarioATMLinea" placeholder="Comentario sobre ATM en línea..." TextMode="MultiLine" Rows="3" CssClass="form-control"/> </td>                            
                                <td><asp:FileUpload ID="FUATMLinea" runat="server" onchange="img13(this);" /></td>                                     
                                <td><img id="imgATMLinea" runat="server" height="150" width="150" src="../../assets/images/vistaPrevia1.JPG" style="border-width: 0px;" /></td>                                
                            </tr>
                        </tbody>
                    </table>
                </div>
                             </div>
                    </div>
                </div>
            </div>
        </div>
            
       <div class=" col-12 align-self-center" style="margin-left: auto; margin-right: auto">
            <div class="row">
                <div class="col-12 grid-margin stretch-card">
                    <div class="card">
                        <div class="card-body">
            <label class="col-form-label">Observaciones y Comentarios</label>
            <br />
            <div class="row col-11 align-self-center">
                <asp:TextBox runat="server" ID="txtobseracionesVerif" TextMode="MultiLine" Rows="3" PlaceHolder="Ingrese su comentario..." CssClass="form-control col-12"></asp:TextBox>
            </div>
                       </div>
                    </div>
                </div>
            </div>
        </div>
        
        
            <div class="row">
                <div class="col-12 grid-margin stretch-card">
                    <div class="card">
                        <div class="card-body">
        <asp:UpdatePanel ID="UPEnviarVerif" runat="server">
            <ContentTemplate>
               
                <div class="row col-6">
                <div class=" row col-6">
                    <asp:Button runat="server" OnClick="btnEnviarVerif_Click" ID="btnEnviarVerif" CssClass="btn btn-success" Text="Guardar verificación" />
                </div>
                <div class="row col-6" runat="server" id="DIVbtnRechazo" visible="false">
                    <asp:Button runat="server" ID="btnRechazarVerif" OnClick="btnRechazarVerif_Click" CssClass="btn btn-danger" Text="Devolver" />
                </div>
               </div>
                    

            </ContentTemplate>                   
        </asp:UpdatePanel>
                       </div>
                    </div>
                </div>
            </div>
        
        <br />

    </div>
    <!--MODAL VERIF ATM -->
    <div class="modal bs-example-modal-lg" id="modalVerifATM" tabindex="-1" data-backdrop="static" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header" style="background-color:darkslategrey; color:white;">
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
                                <asp:Button runat="server" ID="btnModalVerif" UseSubmitBehavior="false" OnClick="btnModalVerif_Click" CssClass="btn btn-dark mr-3" Text="Enviar" />
                            </div>
                            <div class="row col-3">
                                <asp:Button runat="server" ID="btnModalCerrarVerif" OnClick="btnModalCerrarVerif_Click" CssClass="btn btn-secundary mr-3" Text="Cancelar" />
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

     <!--MODAL RECHAZAR VERIF ATM -->
    <div class="modal bs-example-modal-lg" id="modalRechazar" tabindex="-1" data-backdrop="static" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header" style="background-color:darkslategrey; color:white;">
                    <h4 class="modal-title" id="myLargeModalLabel1">¿Seguro que devolverá lista de verificación?</h4>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="row col-12">
                            <asp:Label runat="server" BorderStyle="None" class="col form-control col-6"><strong>Código de ATM: </strong></asp:Label>
                            <asp:Label runat="server" BorderStyle="None" ID="lbcodATM2" class="col form-control col-6"></asp:Label>
                        </div>
                        <div class="row col-12">
                            <asp:Label runat="server" BorderStyle="None" class="col form-control col-6"><strong>Nombre de ATM: </strong></asp:Label>
                            <asp:Label runat="server" BorderStyle="None" ID="lbNombreATM2" class="col form-control col-6"></asp:Label>
                        </div>
                        <div class="row col-12">
                            <asp:Label runat="server" BorderStyle="None" class="col form-control col-6"><strong>Sucursal de ATM: </strong></asp:Label>
                            <asp:Label runat="server" BorderStyle="None" ID="lbsucursalATM2" class="col form-control col-6"></asp:Label>
                        </div>

                        <div class="row col-12">
                            <asp:Label runat="server" BorderStyle="None" class="col form-control col-6"><strong>Inventario de ATM: </strong>  </asp:Label>
                            <asp:Label runat="server" BorderStyle="None" ID="lbInventarioATM2" class="col form-control col-6"></asp:Label>
                        </div>
                        <div class="row col-12">
                            <asp:Label runat="server" BorderStyle="None" class="col form-control col-6"><strong>Técnico responsable: </strong>  </asp:Label>
                            <asp:Label runat="server" BorderStyle="None" ID="lbtecnico2" class="col form-control col-6"></asp:Label>
                        </div>
                        <div class="row col-12">
                            <asp:Label runat="server" BorderStyle="None" class="col form-control col-6"><strong>Motivo de rechazo: </strong>  </asp:Label>
                            <asp:TextBox runat="server" ID="txtmotivoRechazo" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                        </div>
                         <div class="col-md-12 align-self-center" style="margin-left: auto; margin-right: auto">
                                <br />
                                <h6 runat="server" visible="false" id="H5Alerta" class="text-danger col-12" style="text-align:center;">Los campos con(*) son obligatorios.</h6>
                            </div>
                             <div class="col-md-12 align-self-center" style="margin-left: auto; margin-right: auto">
                               <asp:TextBox runat="server" Enabled="false" Text="Ingrese motivo por el que devuelve verificación." Visible="false" ID="txtAlerta2" CssClass="form-control" style="background-color:red; color:white; text-align:center;"/>
                            </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <div class="modal-footer col-12">
                            <div class="row col-3">
                                <asp:Button runat="server" ID="btnRechazarModal" OnClick="btnRechazarModal_Click" CssClass="btn btn-dark mr-3" Text="Devolver" />
                            </div>
                            <div class="row col-3">
                                <asp:Button runat="server" ID="btnCerrarRechazoModal" OnClick="btnCerrarRechazoModal_Click"  CssClass="btn btn-secundary mr-3" Text="Cancelar" />
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
    <!-- /MODAL RECHAZAR VERIF ATM -->

    <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="HFDiscoDuro" runat="server" />
            <asp:HiddenField ID="HFATMDesarmadoPS" runat="server" />
            <asp:HiddenField ID="HFATMDesarmadoPI" runat="server" />
            <asp:HiddenField ID="HFDispositivoVendor" runat="server" />
            <asp:HiddenField ID="HFSYSTEMINFO" runat="server" />
            <asp:HiddenField ID="HFMonitorFiltro" runat="server" />
            <asp:HiddenField ID="HFAntiskimmin" runat="server" />
            <asp:HiddenField ID="HFPadlewheel" runat="server" />
            <asp:HiddenField ID="HFDispDesarmado" runat="server" />
            <asp:HiddenField ID="HFTeclado" runat="server" />
            <asp:HiddenField ID="HFATMLinea" runat="server" />
             <asp:HiddenField ID="HFEnergia" runat="server" />
            <asp:HiddenField ID="HFClima" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>

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
