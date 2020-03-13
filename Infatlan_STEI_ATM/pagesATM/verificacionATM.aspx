<%@ Page Title="" Language="C#" MasterPageFile="~/mainATM.Master" AutoEventWireup="true" CodeBehind="verificacionATM.aspx.cs" Inherits="Infatlan_STEI_ATM.defaultATM" %>

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
        function openModal() { $('#modalverificacion').modal('show'); }
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div class="row page-titles">
        <div class="col-md-5 align-self-center">
            <h4 class="text-themecolor">Completar lista de verificación de ATMs</h4>
        </div>       
        <div class="col-md-7 align-self-center text-right">
            <div class="d-flex justify-content-end align-items-center">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <!--   <button type="button" class="btn btn-info d-none d-lg-block m-l-15" data-toggle="modal" data-target=".bs-example-modal-lg"><i class="fa fa-minus-circle"></i>Mantenimiento ATMs sin realizar</button>-->
                        <asp:Button runat="server" ID="btnmodal" OnClick="btnmodal_Click" class="btn btn-info d-none d-lg-block m-l-15" Text="Mantenimiento ATMs sin realizar"></asp:Button>

                        

                    </ContentTemplate>
                </asp:UpdatePanel>

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
                        <asp:TextBox ID="txthsalidaInfa" placeholder="00:00:00" class="form-control" runat="server" TextMode="Time"></asp:TextBox>
                    </div>
                </div>
                <div class="row col-6">
                    <label class="col col-form-label col-6">Hora llegada a Infatlan</label>
                    <div class="row col-12">
                        <asp:TextBox ID="txtHllegadaInfatlan" placeholder="00:00:00" class="form-control" runat="server" TextMode="Time"></asp:TextBox>
                    </div>
                </div>
            </div>
            <!--/SEGUNDA FILA-->
            <!--TERCERA FILA-->
            <div class="row col-12">
                <div class="row col-6">

                    <label class="col-form-label col-6">Inicio de mantenimiento</label>
                    <div class="row col-12">
                        <asp:TextBox ID="TxFechaInicio" placeholder="1900-12-31" class="form-control" runat="server" TextMode="Time"></asp:TextBox>
                    </div>
                </div>
                <div class="row col-6">
                    <label class=" col-form-label col-6">Finaliza mantenimiento</label>
                    <div class="row col-12">
                        <asp:TextBox ID="TxFechaRegreso" placeholder="1900-12-31" class="form-control" runat="server" TextMode="Time"></asp:TextBox>
                    </div>
                </div>
            </div>
            <!--FIN TERCERA FILA-->
            <!--CUARTA FILA-->
            <div class="row col-12">
                <div class="row col-6">
                    <label class="col-form-label col-6">Lugar(Nombre)</label>
                    <div class="row col-12">
                        <asp:DropDownList runat="server" ID="droplugar" CssClass="form-control">                         
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row col-6">
                    <label class="col-form-label col-6">SysAid</label>
                    <div class="row col-12">
                        <asp:TextBox CssClass="form-control" ID="txtsysaid" runat="server"></asp:TextBox>
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
                    <label class="col-form-label col-6">Departamento</label>
                    <div class="row col-12">
                        <asp:TextBox CssClass="form-control" ID="txtdepto" runat="server" Enabled="false"></asp:TextBox>
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
                        <asp:TextBox CssClass="form-control" ID="tztzonaVerif" runat="server" Enabled="false"></asp:TextBox>
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
                        <asp:DropDownList ID="DropDownList1" CssClass="form-control" runat="server">
                            <asp:ListItem Value="0" Text="Seleccione técnico..."></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="row col-6">
                    <label class=" col-form-label col-6">Identidad</label>
                    <div class="row col-12">
                        <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" Enabled="false"></asp:TextBox>
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
                    <asp:CheckBoxList ID="ckpasos1" runat="server" class="check green" data-checkbox="icheckbox_flat-green">
                        <asp:ListItem Value="0" Text="1. Verificar que ATM este en servicio antes de apagarlo"></asp:ListItem>
                        <asp:ListItem Value="1" Text="2. Solicitar al encargado del ATM sacar contadores"></asp:ListItem>
                        <asp:ListItem Value="2" Text="3. Solicitar al encargado del ATM sacar maleta"></asp:ListItem>
                        <asp:ListItem Value="3" Text="4. Apagar ATM de forma correcta"></asp:ListItem>
                        <asp:ListItem Value="4" Text="5. Retirar CPU"></asp:ListItem>
                        <asp:ListItem Value="5" Text="6. Retirar Monitor"></asp:ListItem>
                        <asp:ListItem Value="6" Text="7. Retirar la lectora"></asp:ListItem>
                        <asp:ListItem Value="7" Text="8. Retirar presentador"></asp:ListItem>
                        <asp:ListItem Value="8" Text="9. Limpieza de stacker"></asp:ListItem>
                        <asp:ListItem Value="9" Text="10. Limpieza de pickers"></asp:ListItem>
                    </asp:CheckBoxList>
                </div>
                <div class="row col-7">
                    <asp:CheckBoxList ID="CheckBoxList1" runat="server" class="check green" data-checkbox="icheckbox_flat-green">
                        <asp:ListItem Value="10" Text="11. Sopletear todos los dispositivos"></asp:ListItem>
                        <asp:ListItem Value="11" Text="12. Limpieza general por cada dispositivo"></asp:ListItem>
                        <asp:ListItem Value="12" Text="13. Limpieza de pantalla con espuma"></asp:ListItem>
                        <asp:ListItem Value="13" Text="14. Realizar cambios de repuesto"></asp:ListItem>
                        <asp:ListItem Value="14" Text="15. Armar ATM"></asp:ListItem>
                        <asp:ListItem Value="15" Text="16. Encender ATM"></asp:ListItem>
                        <asp:ListItem Value="16" Text="17. Solicitar que ingresen maletas"></asp:ListItem>
                        <asp:ListItem Value="17" Text="18. Pruebas con los dospositivos(en el vendor)"></asp:ListItem>
                        <asp:ListItem Value="18" Text="19. Dejar en servicio el ATM"></asp:ListItem>
                        <asp:ListItem Value="19" Text="20. Contractarse con personal de ATM para verificar que camaras están en linea"></asp:ListItem>
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
                                <asp:DropDownList ID="droptipoTeclado" CssClass="form-control" runat="server">
                                  
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row col-4">
                            <label class="col-form-label col-12">Tipo de Procesador</label>
                            <div class="row col-12">
                                <asp:DropDownList ID="droptipoProc" CssClass="form-control" runat="server">
                                    
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
                                <asp:TextBox runat="server" ID="txtiddiscoDuro" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                          <div class="row col-4">
                            <label class="col-form-label col-12">Capacidad de disco duro</label>
                            <div class="row col-12">
                                <asp:TextBox runat="server" ID="txtcapacidadDiscoVerif" CssClass="form-control" ></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <!--/SEGUNDA FILA-->                  
                    <!--QUINTA FILA-->
                     <div class="row col-12">
                       

                        <div class="row col-4">
                            <label class="col-form-label col-12">Serie ATM</label>
                            <div class="row col-12">
                                <asp:TextBox runat="server" ID="txtserieATM" CssClass="form-control" ></asp:TextBox>
                            </div>
                        </div>
                          <div class="row col-4">
                            <label class="col-form-label col-12">Inventario ATM</label>
                            <div class="row col-12">
                               <asp:TextBox runat="server" ID="txtinventarioVerif" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                          <div class="row col-4">
                            <label class="col-form-label col-12">RAM</label>
                            <div class="row col-12">
                                <asp:TextBox runat="server" ID="txtramVerif" CssClass="form-control" ></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <!--/QUINTA FILA-->
                    <!--SEXTA FILA-->
                     <div class="row col-12">
                         
                        <div class="row col-4">
                            <label class="col-form-label col-12">Latitud ATM</label>
                            <div class="row col-12">
                               <asp:TextBox runat="server" ID="txtlatitudATM" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row col-4">
                            <label class="col-form-label col-12">Longitud ATM</label>
                            <div class="row col-12">
                                <asp:TextBox runat="server" ID="txtlongitudATM" CssClass="form-control" ></asp:TextBox>
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
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <br />
        <div class="row col-12" style="margin-left: 10px; margin-left: 10px;">
            <h3 class="text-themecolor" style="color: #808080;"><i class="fa fa-image" style="margin-left: 10px"></i>Comprobación</h3>
        </div>
        <br />
        <hr />
        <br />
        <div class="row col-12" style="margin-left: 10px; margin-left: 10px;">
            <div class="row col-12">

                <div class="card row col-4">
                    <div class="card-body">
                        <label for="input-file-now">Disco Duro</label>
                        <asp:FileUpload ID="imgdiscoDuro" runat="server" class="dropify" />
                    </div>
                </div>


                <div class="card row col-4">
                    <div class="card-body">
                        <label for="input-file-now">ATM desarmado parte superior (limpiar)</label>
                        <asp:FileUpload ID="imgATMdesarmado" runat="server" class="dropify" />
                    </div>
                </div>


                <div class="card row col-4">
                    <div class="card-body">
                        <label for="input-file-now">ATM desarmado parte inferior (limpiar)</label>
                        <asp:FileUpload ID="imgATMinferior" runat="server" class="dropify" />
                    </div>
                </div>

            </div>
        </div>
        <!--PRIMERA FILA DE IMAGENES-->

        <div class="row col-12" style="margin-left: 10px; margin-left: 10px;">
            <div class="row col-12">
                <div class="card row col-4">
                    <div class="card-body">
                        <label for="input-file-now">Dispositivo en modo diagnostico de vendor que esten en linea</label>
                        <asp:FileUpload ID="imgdispVendor" runat="server" class="dropify" />
                    </div>
                </div>
                <div class="card row col-4">
                    <div class="card-body">
                        <label for="input-file-now">Tipo de procesador con el comando "SYSTEMINFO"</label><br />
                        <asp:FileUpload ID="imgSysteminfo" runat="server" class="dropify" />
                    </div>
                </div>
                <div class="card row col-4">
                    <div class="card-body">
                        <label for="input-file-now">Lectora con el antiskimming desarmado y limpio</label>
                        <asp:FileUpload ID="imgantiskimming" runat="server" class="dropify" />
                    </div>
                </div>
            </div>
        </div>
        <!--SEGUNDA FILA DE IMAGENES-->

        <div class="row col-12" style="margin-left: 10px; margin-left: 10px;">
            <div class="row col-12">
                <div class="card row col-4">
                    <div class="card-body">
                        <label for="input-file-now">Monitor con el filtro</label>
                        <asp:FileUpload ID="imgmonitorFiltro" runat="server" class="dropify" />
                    </div>
                </div>
                <div class="card row col-4">
                    <div class="card-body">
                        <label for="input-file-now">PadleWheel(rueda de paletas)</label><br />
                        <asp:FileUpload ID="FileUpload5" runat="server" class="dropify" />
                    </div>
                </div>
                <div class="card row col-4">
                    <div class="card-body">
                        <label for="input-file-now">Dispositivos desarmado</label>
                        <asp:FileUpload ID="imgDispDesarmados" runat="server" class="dropify" />
                    </div>
                </div>

            </div>
        </div>
        <!--TERCERA FILA DE IMAGENES-->
        <br />
        <div class="row col-12" style="margin-left: 10px; margin-left: 10px;">
            <h3 class="text-themecolor" style="color: #808080;"><i class="fa fa-fax" style="margin-left: 10px"></i>Condiciones de Sítio</h3>
        </div>
        <br />
        <hr />
        <br />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row col-12" style="margin-left: 10px; margin-left: 10px;">
                    <!--PRIMERA FILA-->
                    <div class="row col-12">
                        <!--PRIMERA COLUMNA-->
                        <div class="row col-6">
                            <label class="col-form-label col-12">¿Cuenta con Climatización adecuada?</label>
                            <div class="row col-12">
                                <asp:DropDownList ID="ddlclimatizacion" AutoPostBack="true" OnSelectedIndexChanged="ddlclimatizacion_SelectedIndexChanged" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="0" Text="Seleccione respuesta..."></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="No"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <!--SEGUNDA COLUMNA-->
                        <div class="row col-6">
                            <label class="col-form-label col-12">¿Cuenta con protección de energía eléctrica?</label>
                            <div class="row col-12">
                                <asp:DropDownList ID="ddlenergia" AutoPostBack="true" OnTextChanged="ddlenergia_TextChanged" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="0" Text="Seleccione respuesta..."></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="No"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <!--SEGUNDA FILA-->
                    <div class="row col-12">
                        <div class="card row col-6">
                            <div class="card-body">
                                <asp:FileUpload ID="imgclimatizacion" runat="server"  Visible="false" />
                            </div>
                        </div>
                        <div class="card row col-6">
                            <div class="card-body">
                                <asp:FileUpload ID="imgproteccionEnergia" runat="server" Visible="false"/>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div class="row col-12" style="margin-left: 10px; margin-left: 10px;">
            <label class="col-form-label">Observaciones y Comentarios</label>
            <br />
            <div class="row col-11 align-self-center">
                <asp:TextBox runat="server" ID="txtobseracionesVerif" TextMode="MultiLine" Rows="3" PlaceHolder="Ingrese su comentario..." CssClass="form-control col-12"></asp:TextBox>
            </div>
        </div>
        <br />
        <br />
        <asp:UpdatePanel ID="UPEnviarVerif" Runat="server">
            <ContentTemplate>
        <div class="col-md-4 align-self-center" style="margin-left:auto; margin-right:auto">           
            <asp:Button runat="server" OnClick="btnEnviarVerif_Click" ID="btnEnviarVerif" CssClass="btn btn-rounded btn-block btn-outline-success" Text="Enviar" />
        </div>
                 </ContentTemplate>
            </asp:UpdatePanel>
        <br />
        <br />
    </div>

    <!-- Modal al no realizar trabajo -->
    <div class="modal bs-example-modal-lg" id="modalverificacion" tabindex="-1" data-backdrop="static" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myLargeModalLabel">Motivo por el que se canceló el mantenimiento</h4>
                    <button type="button" id="btnexitModal" class="close" data-dismiss="modal" aria-hidden="true">X</button>
                </div>

                <asp:UpdatePanel runat="server" ID="UPModal">
                    <ContentTemplate>
                        <div class="modal-body">
                            <div>
                                <asp:Label runat="server">Mantenimientos Pendientes</asp:Label>
                            </div>
                            <div>
                                <asp:DropDownList ID="dropMantPendiente" runat="server" CssClass="form-control col-12">
                                    <asp:ListItem Value="0" Text="Seleccione mantnimiento pendiente...."></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <br />
                            <div>
                                <asp:Label runat="server">Motivo de Cancelación de Mantenimiento</asp:Label>
                            </div>
                            <div>
                                <asp:DropDownList ID="dropMotivo" runat="server" CssClass="form-control col-12">
                                    <asp:ListItem Value="0" Text="Seleccione motivo de cancelación...."></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <br />
                            <div>Cambio realizado por</div>
                            <div>
                                <asp:DropDownList ID="dropcambioPor" runat="server" CssClass="form-control col-12">
                                    <asp:ListItem Value="0" Text="Seleccione personal responsable...."></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Técnico Responsable"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Jefe de soporte de zona"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Suplente jefe de zona"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <br />
                            <div>
                                <label>Nuevo Técnico Responsable</label>
                            </div>
                            
                            <div>
                                <asp:DropDownList ID="dropNewTecnico" runat="server" CssClass="form-control col-12">
                                    <asp:ListItem Value="0" Text="Seleccione nuevo personal responsable...."></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <br />
                            <div>
                                <asp:Label runat="server">Detalle de motivo de cancelación</asp:Label>
                            </div>
                            <div>
                                <asp:TextBox ID="txtdetalleCancela" runat="server" TextMode="Multiline" Rows="5" CssClass="form-control col-12"></asp:TextBox>
                            </div>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>

                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <div class="modal-footer">
                            <asp:Button runat="server" ID="btnMantSinRealizar" OnClick="btnMantSinRealizar_Click" CssClass="btn btn-rounded btn-block btn-outline-success" Text="Enviar" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
            <!-- /.modal-content -->
        </div>
        <!--/.modal-dialog -->
    </div>
    <!-- /Modal al no realizar trabajo -->
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
