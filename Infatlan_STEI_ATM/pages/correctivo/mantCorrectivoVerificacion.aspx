<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="mantCorrectivoVerificacion.aspx.cs" Inherits="Infatlan_STEI_ATM.pages.correctivo.mantCorrectivoVerificacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
      <!--PARA LLAMAR MODAL-->
    <script type="text/javascript">
        function openModal() { $('#modalVerifATM').modal('show'); }
        function openModal2() { $('#modalRechazar').modal('show'); }
        function closeModal() { $('#modalVerifATM').modal('hide'); }
        function closeModal2() { $('#modalRechazar').modal('hide'); }
        function openModal3() { $('#modalMaterial').modal('show'); }
        function closeModal3() { $('#modalMaterial').modal('hide'); }
    </script>

     <script type="text/javascript">
        //IMAGEN1        
        function imgC1(input) {
            if (input.files && input.files[0]) {
                //PRIMERA IMAGEN              
                var reader = new FileReader();
                reader.onload = function (e) {
                    //$('#imgDiscoDuro').css('visibility', 'visible');
                    //$('#imgDiscoDuro').attr('src', e.target.result);  
                    var ruta1 = e.target.result;
                    document.getElementById('<%=imgPickerLimpio.ClientID%>').src = ruta1;
                    document.getElementById('<%=HFPicker.ClientID%>').value = 'si';
                }
                reader.readAsDataURL(input.files[0]);
                //PRIMERA IMAGEN              
            }
        }
        //IMAGEN1
        //IMAGEN2
        function imgC2(input) {

            if (input.files && input.files[0]) {
                //PRIMERA IMAGEN
                var reader = new FileReader();
                reader.onload = function (e) {
                    //$('#imgATMDesarmadoPS').css('visibility', 'visible');
                    //$('#imgATMDesarmadoPS').attr('src', e.target.result);
                    var ruta2 = e.target.result;
                    document.getElementById('<%=imgPresentadorLimpio.ClientID%>').src = ruta2;
                    document.getElementById('<%=HFPresentador.ClientID%>').value = 'si';
                }
                reader.readAsDataURL(input.files[0]);
                //PRIMERA IMAGEN              
            }
        }
        //IMAGEN2
        //IMAGEN3
        function imgC3(input) {

            if (input.files && input.files[0]) {
                //PRIMERA IMAGEN
                var reader = new FileReader();
                reader.onload = function (e) {
                    //$('#imgATMDesarmadoPI').css('visibility', 'visible');
                    //$('#imgATMDesarmadoPI').attr('src', e.target.result);
                    var ruta3 = e.target.result;
                    document.getElementById('<%=imgTMSinDesarmarPS.ClientID%>').src = ruta3;
                    document.getElementById('<%=HFATMSinDesarmar.ClientID%>').value = 'si';
                }
                reader.readAsDataURL(input.files[0]);
                //PRIMERA IMAGEN              
            }
        }
        //IMAGEN3
        //IMAGEN4
        function imgC4(input) {

            if (input.files && input.files[0]) {
                //PRIMERA IMAGEN
                var reader = new FileReader();
                reader.onload = function (e) {
                    //$('#imgDispositivoVendor').css('visibility', 'visible');
                    //$('#imgDispositivoVendor').attr('src', e.target.result);
                    var ruta4 = e.target.result;
                    document.getElementById('<%=imgBandas.ClientID%>').src = ruta4;
                    document.getElementById('<%=HFBandas.ClientID%>').value = 'si';
                }
                reader.readAsDataURL(input.files[0]);
                //PRIMERA IMAGEN              
            }
        }
        //IMAGEN4
        //IMAGEN5
        function imgC5(input) {

            if (input.files && input.files[0]) {
                //PRIMERA IMAGEN
                var reader = new FileReader();
                reader.onload = function (e) {
                    //$('#imgSYSTEMINFO').css('visibility', 'visible');
                    //$('#imgSYSTEMINFO').attr('src', e.target.result);
                    var ruta5 = e.target.result;
                    document.getElementById('<%=imgDiscoDuro.ClientID%>').src = ruta5;
                    document.getElementById('<%=HFDiscoDuro.ClientID%>').value = 'si';
                }
                reader.readAsDataURL(input.files[0]);
                //PRIMERA IMAGEN              
            }
        }
        //IMAGEN5   
        //IMAGEN6
        function imgC6(input) {

            if (input.files && input.files[0]) {
                //PRIMERA IMAGEN
                var reader = new FileReader();
                reader.onload = function (e) {
                    //$('#imgAntiskimmin').css('visibility', 'visible');
                    //$('#imgAntiskimmin').attr('src', e.target.result);
                    var ruta6 = e.target.result;
                    document.getElementById('<%=imgMapaATM.ClientID%>').src = ruta6;
                    document.getElementById('<%=HFMapa.ClientID%>').value = 'si';
                }
                reader.readAsDataURL(input.files[0]);
                //PRIMERA IMAGEN              
            }
        }
        //IMAGEN6  
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
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Mantenimiento Correctivo</a></li>
                    <li class="breadcrumb-item active">Lista de verificación</li>
                </ol>
            </div>
        </div>
    </div>

    <div class="card">
        <div class="card-body">
            <h4 class="card-title">Lista de verificación</h4>
            <h6 class="card-subtitle">Ingresar datos de mantenimiento correctivo de ATM</h6>
            <div class="row col-12">
                <div class="col-12 grid-margin stretch-card">
                    <h5 class="card-subtitle m-t-10"><i class="fa fa-book"></i><b> DATOS GENERALES</b></h5>
                    <hr />
                    <!--SEGUNDA FILA-->
                    <div class="row col-12 align-self-center" style="margin-left: auto; margin-right: auto">
                        <div class="row col-6 align-self-center" style="margin-left: auto; margin-right: auto">
                            <label class="col-form-label">Hora Salida de Infatlan</label>
                            <asp:TextBox ID="txthsalidaInfa" placeholder="00:00:00" CssClass="form-control" runat="server" TextMode="Time"></asp:TextBox>
                        </div>
                        <div class="row col-6 align-self-center" style="margin-left: auto; margin-right: auto">
                            <label class="col-form-label">Hora llegada a Infatlan</label>
                            <asp:TextBox ID="txtHllegadaInfatlan" placeholder="00:00:00" CssClass="form-control" runat="server" TextMode="Time"></asp:TextBox>
                        </div>
                    </div>

                    <!--TERCERA FILA-->
                    <div class="row col-12 align-self-center" style="margin-left: auto; margin-right: auto">
                        <div class="row col-6 align-self-center" style="margin-left: auto; margin-right: auto">
                            <label class="col-form-label">Inicio de mantenimiento</label>
                            <asp:TextBox ID="TxFechaInicio" placeholder="00:00:00" CssClass="form-control" runat="server" TextMode="Time"></asp:TextBox>
                        </div>
                        <div class="row col-6 align-self-center" style="margin-left: auto; margin-right: auto">
                            <label class=" col-form-label">Finaliza mantenimiento</label>
                            <asp:TextBox ID="TxFechaRegreso" placeholder="00:00:00" CssClass="form-control" runat="server" TextMode="Time"></asp:TextBox>
                        </div>
                    </div>

                    <!--CUARTA FILA-->
                    <div class="row col-12 align-self-center" style="margin-left: auto; margin-right: auto">
                        <div class="row col-12 align-self-center" style="margin-left: auto; margin-right: auto">
                            <label class="col-form-label">Lugar(Nombre)</label>
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtnomATM" Enabled="false"></asp:TextBox>
                        </div>
                       
                    </div>

                    <!--QUINTA FILA-->
                    <div class="row col-12 align-self-center" style="margin-left: auto; margin-right: auto">
                        <div class="row col-6 align-self-center" style="margin-left: auto; margin-right: auto">
                            <label class="col-form-label">Ubicación de ATM</label>
                            <asp:TextBox CssClass="form-control" ID="txtUbicacionATM" runat="server" Enabled="false"></asp:TextBox>
                        </div>
                         <div class="row col-6 align-self-center" style="margin-left: auto; margin-right: auto">
                            <label class="col-form-label">SysAid</label>
                            <asp:TextBox CssClass="form-control" ID="txtsysaid" runat="server" Enabled="false"></asp:TextBox>
                        </div>
                    </div>

                    <!--SEXTA FILA-->
                    <div class="row col-12 align-self-center" style="margin-left: auto; margin-right: auto">
                        <div class="row col-6 align-self-center" style="margin-left: auto; margin-right: auto">
                            <label class="col-form-label">Dirección</label>
                            <asp:TextBox CssClass="form-control" ID="txtdireccion" runat="server" Enabled="false"></asp:TextBox>
                        </div>
                        <div class="row col-6 align-self-center" style="margin-left: auto; margin-right: auto">
                            <label class="col-form-label">Sucursal</label>
                            <asp:TextBox CssClass="form-control" ID="txtsucursal" runat="server" Enabled="false"></asp:TextBox>
                        </div>
                    </div>

                    <!--SEPTIMA FILA-->
                    <div class="row col-12 align-self-center" style="margin-left: auto; margin-right: auto">
                        <div class="row col-6 align-self-center" style="margin-left: auto; margin-right: auto">
                            <label class="col-form-label">Dirección IP</label>
                            <asp:TextBox CssClass="form-control" ID="txtip" runat="server" Enabled="false"></asp:TextBox>
                        </div>
                        <div class="row col-6 align-self-center" style="margin-left: auto; margin-right: auto">
                            <label class="col-form-label">Zona</label>
                            <asp:TextBox CssClass="form-control" ID="txtzonaVerif" runat="server" Enabled="false"></asp:TextBox>
                        </div>
                    </div>

                    <!--OCTAVA FILA-->
                    <div class="row col-12 align-self-center" style="margin-left: auto; margin-right: auto">
                        <div class="row col-6 align-self-center" style="margin-left: auto; margin-right: auto">
                            <label class="col-form-label">Impacto</label>
                            <asp:TextBox CssClass="form-control" ID="txtimpacto" runat="server" Enabled="false" Text="Durante la ventana de mantenimiento el ATM estará fuera de linea"></asp:TextBox>
                        </div>
                        <div class="row col-6 align-self-center" style="margin-left: auto; margin-right: auto">
                            <label class="col-form-label">Motivo</label>
                            <asp:TextBox CssClass="form-control" ID="txtmotivoVerif" runat="server" Enabled="false" Text="Realizar acciones pro activas para prevenir la falla"></asp:TextBox>
                        </div>
                    </div>

                    <h5 class="card-subtitle m-t-30"><i class="fa fa-user"></i><b> TECNICO RESPONSABLE</b></h5>
                    <hr />
                    <!--PRIMERA FILA-->
                    <div class="row col-12 align-self-center" style="margin-left: auto; margin-right: auto">
                        <div class="row col-6 align-self-center" style="margin-left: auto; margin-right: auto">
                            <label class="col-form-label">Técnico Responsable</label>
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtTecnicoResponsable" Enabled="false"></asp:TextBox>
                        </div>

                        <div class="row col-6 align-self-center" style="margin-left: auto; margin-right: auto">
                            <label class=" col-form-label">Identidad</label>
                            <asp:TextBox CssClass="form-control" ID="txtidentidad" runat="server" Enabled="false"></asp:TextBox>
                        </div>
                    </div>

                    <h5 class="card-subtitle m-t-20"><i class="fa fa-check-square"></i><b> DATOS DE ATM</b></h5>
                    <hr />
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <!--PRIMERA FILA DESHABILITADA-->
                            <div class="row col-12 align-self-center" style="margin-left: auto; margin-right: auto">
                                <div class="row col-4 align-self-center" style="margin-left: auto; margin-right: auto">
                                    <label class="col-form-label">Sistema Operativo</label>
                                    <%--<asp:TextBox runat="server" ID="txtsoVerif" CssClass="form-control" Enabled="false"></asp:TextBox>--%>
                                     <asp:DropDownList ID="DDLSo" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>

                                <div class="row col-4 align-self-center" style="margin-left: auto; margin-right: auto">
                                    <label class="col-form-label">Versión del software</label>
                                    <%--<asp:TextBox runat="server" ID="txtversionswVerif" CssClass="form-control" Enabled="false"></asp:TextBox>--%>
                                     <asp:DropDownList ID="DDLVersionSW" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>

                                <div class="row col-4 align-self-center" style="margin-left: auto; margin-right: auto">
                                    <label class="col-form-label">Puerto</label>
                                    <asp:TextBox runat="server" ID="txtpuertoVerif" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                            <!--PRIMERA FILA-->
                            <div class="row col-12 align-self-center" style="margin-left: auto; margin-right: auto">
                                <br />
                                <div class="row col-4 align-self-center" style="margin-left: auto; margin-right: auto">
                                    <label class="col-form-label">Tipo de Teclado</label>
                                    <asp:DropDownList ID="DDLtipoTeclado" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <div class="row col-4 align-self-center" style="margin-left: auto; margin-right: auto">
                                    <label class="col-form-label">Tipo de Procesador</label>
                                    <asp:DropDownList ID="DDLtipoProc" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <div class="row col-4 align-self-center" style="margin-left: auto; margin-right: auto">
                                    <label class="col-form-label">Tipo de Carga</label>
                                    <asp:DropDownList ID="DDLtipoCargaVerif" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <!--SEGUNDA FILA-->
                            <div class="row col-12 align-self-center" style="margin-left: auto; margin-right: auto">
                                <div class="row col-4 align-self-center" style="margin-left: auto; margin-right: auto">
                                    <label class="col-form-label">Marca del disco duro</label>
                                    <asp:DropDownList AutoPostBack="true" ID="DDLmarcaDiscoDuro" CssClass="form-control col-12" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <div class="row col-4 align-self-center" style="margin-left: auto; margin-right: auto">
                                    <label class="col-form-label">Número Serie de Disco Duro</label>
                                    <asp:TextBox runat="server" ID="txtSerieDiscoDuro" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="row col-4 align-self-center" style="margin-left: auto; margin-right: auto">
                                    <label class="col-form-label">Capacidad de disco duro(GB)</label>
                                    <asp:TextBox runat="server" ID="txtcapacidadDiscoVerif" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                </div>
                            </div>

                            <!--QUINTA FILA-->
                            <div class="row col-12 align-self-center" style="margin-left: auto; margin-right: auto">
                                <div class="row col-4 align-self-center" style="margin-left: auto; margin-right: auto">
                                    <label class="col-form-label">Serie ATM</label>
                                    <asp:TextBox runat="server" ID="txtserieATM" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="row col-4 align-self-center" style="margin-left: auto; margin-right: auto">
                                    <label class="col-form-label">Inventario ATM</label>
                                    <asp:TextBox runat="server" ID="txtinventarioVerif" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="row col-4 align-self-center" style="margin-left: auto; margin-right: auto">
                                    <label class="col-form-label">RAM(GB)</label>
                                    <asp:TextBox runat="server" ID="txtramVerif" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                </div>
                            </div>

                            <!--SEXTA FILA-->
                            <div class="row col-12 align-self-center" style="margin-left: auto; margin-right: auto">
                                <div class="row col-4 align-self-center" style="margin-left: auto; margin-right: auto">
                                    <label class="col-form-label">Latitud ATM</label>
                                    <asp:TextBox runat="server" ID="txtlatitudATM" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="row col-4 align-self-center" style="margin-left: auto; margin-right: auto">
                                    <label class="col-form-label">Longitud ATM</label>
                                    <asp:TextBox runat="server" ID="txtlongitudATM" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="row col-4 align-self-center" style="margin-left: auto; margin-right: auto">
                                </div>
                            </div><br />

                            <!--TERCERA FILA-->
                            <div class="row col-12 align-self-center" style="margin-left: auto; margin-right: auto">
                                <div class="row col-4 align-self-center" style="margin-left: auto; margin-right: auto">
                                    <label class="col-form-label">¿Se cambió antiskimming?</label>
                                    <asp:DropDownList AutoPostBack="true" OnTextChanged="dropantiskimming_TextChanged" ID="dropantiskimming" CssClass="form-control col-12" runat="server">
                                        <asp:ListItem Value="0" Text="Seleccione opción..."></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="No"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="row col-8 align-self-center" style="margin-left: auto; margin-right: auto">
                                    <%--<label class="col-form-label">Comentario por lectora sin antiskimming</label>--%>
                                    <asp:TextBox runat="server" ID="txtantiSkimming" PlaceHolder="Ingrese comentario..." CssClass="form-control" Enabled="false" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                </div>
                            </div><br />
                            <div class="row col-12 align-self-center" style="margin-left: auto; margin-right: auto">
                                <div class="row col-4 align-self-center" style="margin-left: auto; margin-right: auto">
                                    <label class="col-form-label">¿Se cambiaron piezas?</label>
                                    <asp:DropDownList AutoPostBack="true" OnTextChanged="DDLCambioPiezas_TextChanged" ID="DDLCambioPiezas" CssClass="form-control col-12" runat="server">
                                       <asp:ListItem Value="0" Text="Seleccione opción..."></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="No"></asp:ListItem>
                                    </asp:DropDownList>
                                </div><br /><br />
                                 <%--</div>--%>
                                 <%-- <div class="row col-12 align-self-center" style="margin-left: auto; margin-right: auto">--%>
                                <div class="row col-8 align-self-center" style="margin-left: auto; margin-right: auto">
                                   <%-- <label class="col-form-label">Comentario por lectora sin antiskimming</label>--%>
                                    <asp:TextBox runat="server" ID="txtCambioMateriales" PlaceHolder="Ingrese piezas que se usaron en mantenimiento..."  CssClass="form-control" Enabled="false" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                </div>
                            </div>
                             <div class="row col-12 align-self-center" style="margin-left: auto; margin-right: auto">
                                <div class="row col-12 align-self-center" style="margin-left: auto; margin-right: auto">
                                    <label class="col-form-label">¿Se cambió disco duro?</label>
                                    <asp:DropDownList AutoPostBack="true" OnTextChanged="DDLCambioDiscoDuro_TextChanged" ID="DDLCambioDiscoDuro" CssClass="form-control col-12" runat="server">
                                        <asp:ListItem Value="0" Text="No"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Sí, y otras piezas"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <%--MATERIALES--%>
                    <br />
                     <h5 runat="server" visible="false" class="card-subtitle m-t-30"><i class="fa fa-tasks"></i><b> MATERIALES</b></h5>
                    <hr />
                    <asp:UpdatePanel runat="server" Visible="false">
                        <ContentTemplate>
                            <div class="col-md-12 align-self-center" style="margin-left: auto; margin-right: auto">
                                <div class="table-responsive">
                                    <!--<table id="bootstrap-data-table" class="table table-striped table-bordered"> -->
                                    <asp:GridView ID="GVNewMateriales" runat="server"
                                        CssClass="table table-bordered"
                                        PagerStyle-CssClass="pgr"
                                        HeaderStyle-CssClass="table"
                                        RowStyle-CssClass="rows"
                                        AutoGenerateColumns="false"
                                        AllowPaging="true"
                                        GridLines="None"
                                        HeaderStyle-HorizontalAlign="center"
                                        PageSize="10" 
                                        Style="margin: 30px 0px 20px 0px">
                                        <Columns>
                                            <asp:BoundField DataField="idMantenimiento" HeaderText="Código" Visible="false" ItemStyle-HorizontalAlign="center" />
                                            <asp:BoundField DataField="idStock" HeaderText="Stock" Visible="false" ItemStyle-HorizontalAlign="center" />
                                            <asp:BoundField DataField="nombre" HeaderText="Material" ItemStyle-HorizontalAlign="center" />
                                            <asp:BoundField DataField="cantidad" HeaderText="Solicitado" ItemStyle-HorizontalAlign="center" />
                                            <asp:BoundField DataField="devolver" HeaderText="Utilizado" ItemStyle-HorizontalAlign="center" />
                                            <%-- <asp:BoundField DataField="IDUbi" HeaderText="Ubi" Visible="true" ItemStyle-HorizontalAlign="center" />--%>
                                            <asp:TemplateField HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="Btnseleccionar" Enabled="true" runat="server" Text="" class="btn btn-info mr-2" CommandArgument='<%# Eval("idStock") %>' CommandName="ver"><i class="ti-check-box"></i></asp:LinkButton>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <%--MATERIALES--%>

                    <h5 class="card-subtitle m-t-30"><i class="fa fa-image"></i><b> COMPROBACION</b></h5>
                    <hr />
                    <asp:UpdatePanel runat="server" ID="UPIMG" UpdateMode="Conditional">
                        <ContentTemplate>

                            <div class="card-body">
                                <h5 class="card-subtitle">Subir imagenes de lo solicitado de mantenimiento</h5>
                                <h6 class="card-subtitle" style="color: red;">Campos con * son obligatorios</h6>
                                <table class="tablesaw table-bordered table-hover table no-wrap" data-tablesaw-mode="swipe"
                                    data-tablesaw-sortable data-tablesaw-sortable-switch data-tablesaw-minimap
                                    data-tablesaw-mode-switch>
                                    <thead>
                                        <tr>
                                            <th scope="col" style="background-color: #5D6D7E; color: #D5DBDB;" data-tablesaw-sortable-col data-tablesaw-priority="persist" class="border">Imagen a subir</th>
                                            <%--<th scope="col" style="background-color: #5D6D7E; color: #D5DBDB;" data-tablesaw-sortable-col data-tablesaw-priority="2" class="border">Seleccione imagen </th>--%>
                                            <th scope="col" style="background-color: #5D6D7E; color: #D5DBDB;" data-tablesaw-sortable-col data-tablesaw-priority="1" class="border">
                                                <abbr title="Imagenes del mantenimiento">Mostrar imagen</abbr>
                                            </th>


                                        </tr>
                                    </thead>
                                    <tbody>

                                        <tr runat="server" id="TRPicker">
                                            <td class="title"><a class="link" href="javascript:void(0)">*Picker con rodo limpio</a><br />
                                                <asp:FileUpload ID="FUPickerLimpio" runat="server" onchange="imgC1(this);" />
                                            </td>
                                            <%--<td runat="server" id="td1img1" style="display:none"><img id="imgDiscoDuro" runat="server" height="150" width="150" src="" style="border-width: 0px;" /></td>--%>
                                            <td>
                                                <img id="imgPickerLimpio" runat="server" height="600" width="550" src="../../assets/images/vistaPrevia1.JPG" style="border-width: 0px;" /></td>
                                        </tr>
                                        <tr runat="server" id="TRPresentador">
                                            <td class="title"><a class="link" href="javascript:void(0)">*Presentador limpio</a><br />
                                                <asp:FileUpload ID="FUPresentadorLimpio" runat="server" onchange="imgC2(this);" />
                                            </td>
                                            <td>
                                                <img id="imgPresentadorLimpio" runat="server" height="600" width="550" src="../../assets/images/vistaPrevia1.JPG" style="border-width: 0px;" /></td>
                                        </tr>
                                        <tr runat="server" id="TRParteSuperior">
                                            <td class="title"><a class="link" href="javascript:void(0)">*Parte superior sin desarmar limpio</a><br />
                                                <asp:FileUpload ID="FUATMSinDesarmarPS" runat="server" onchange="imgC3(this);" />
                                            </td>
                                            <td>
                                                <img id="imgTMSinDesarmarPS" runat="server" height="600" width="550" src="../../assets/images/vistaPrevia1.JPG" style="border-width: 0px;" /></td>
                                        </tr>
                                        <tr runat="server" id="TRBandas">
                                            <td class="title"><a class="link" href="javascript:void(0)">*Bandas</a><br />
                                                <asp:FileUpload ID="FUBandas" runat="server" onchange="imgC4(this);" />
                                            </td>
                                            <td>
                                                <img id="imgBandas" runat="server" height="600" width="550" src="../../assets/images/vistaPrevia1.JPG" style="border-width: 0px;" /></td>
                                        </tr>
                                        <tr runat="server" id="TRDiscoDuro" visible="true">
                                            <td class="title"><a class="link" href="javascript:void(0)">*Disco Duro</a><br />
                                                <asp:FileUpload ID="FUDiscoDuro" runat="server" onchange="imgC5(this);" />
                                            </td>
                                            <td>
                                                <img id="imgDiscoDuro" runat="server" height="600" width="550" src="../../assets/images/vistaPrevia1.JPG" style="border-width: 0px;" /></td>
                                        </tr>

                                        <tr>
                                            <td class="title" colspan="3"><a class="link" href="javascript:void(0)">Ubicación del ATM(Mapa)</a><%--</td>--%>
                                                <%-- <td>--%>
                                                <asp:FileUpload ID="FUmapaATM" runat="server" onchange="imgC6(this);" /><br />
                                                <br />
                                                <%--</td>--%>
                                                <%-- <td>--%>
                                                <img id="imgMapaATM" runat="server" src="../../assets/images/vistaPrevia1.JPG" style="border-width: 0px; height: auto; width: 100%" /></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <div class="col-12">
                        <label class="col-form-label">Observaciones y Comentarios</label>
                        <asp:TextBox runat="server" ID="txtobseracionesVerif" TextMode="MultiLine" Rows="5" PlaceHolder="Ingrese su comentario..." CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
            
            <div class="row m-t-20">
                <div class="col-12">
                    <asp:UpdatePanel ID="UPEnviarVerif" runat="server">
                        <ContentTemplate>
                            <div class="col-12">
                                <asp:Button runat="server" OnClick="btnEnviarVerif_Click" ID="btnEnviarVerif" CssClass="btn btn-success" Text="Guardar verificación" />
                                <asp:Button runat="server" ID="btnRechazarVerif" Visible="false" OnClick="btnRechazarVerif_Click" CssClass="btn btn-danger" Text="Devolver" />                                
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="HFDiscoDuro" runat="server" />
            <asp:HiddenField ID="HFPicker" runat="server" />
            <asp:HiddenField ID="HFATMSinDesarmar" runat="server" />
            <asp:HiddenField ID="HFPresentador" runat="server" />
            <asp:HiddenField ID="HFBandas" runat="server" />
            <asp:HiddenField ID="HFMapa" runat="server" />
            
        </ContentTemplate>
    </asp:UpdatePanel>

    <!--MODAL VERIF ATM -->
    <div class="modal fade bs-example-modal-lg" id="modalVerifATM" tabindex="-1" data-backdrop="static" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myLargeModalLabel">¿Seguro que guardará lista de verificación?</h4>
                </div>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="row col-12">
                            <asp:Label runat="server" BorderStyle="None" class="col form-control col-4"><strong>ATM: </strong></asp:Label>
                            <asp:Label runat="server" BorderStyle="None" ID="lbcodATM" class="col form-control col-8"></asp:Label>
                        </div>                     
                        <div class="row col-12">
                            <asp:Label runat="server" BorderStyle="None" class="col form-control col-4"><strong>Sucursal de ATM: </strong></asp:Label>
                            <asp:Label runat="server" BorderStyle="None" ID="lbsucursalATM" class="col form-control col-8"></asp:Label>
                        </div>

                        <div class="row col-12">
                            <asp:Label runat="server" BorderStyle="None" class="col form-control col-4"><strong>Inventario de ATM: </strong>  </asp:Label>
                            <asp:Label runat="server" BorderStyle="None" ID="lbInventarioATM" class="col form-control col-8"></asp:Label>
                        </div>
                        <div class="row col-12">
                            <asp:Label runat="server" BorderStyle="None" class="col form-control col-4"><strong>Responsable: </strong>  </asp:Label>
                            <asp:Label runat="server" BorderStyle="None" ID="lbtecnico" class="col form-control col-8"></asp:Label>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <div class="modal-footer col-12">
                            <asp:Button runat="server" ID="btnModalCerrarVerif" OnClick="btnModalCerrarVerif_Click" CssClass="btn btn-secundary mr-3" Text="Cancelar" />
                                <asp:Button runat="server" ID="btnModalVerif" UseSubmitBehavior="false" OnClick="btnModalVerif_Click" CssClass="btn btn-success mr-3" Text="Enviar" />
                 
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

    <!--MODAL RECHAZAR VERIF ATM -->
    <div class="modal fade bs-example-modal-lg" id="modalRechazar" tabindex="-1" data-backdrop="static" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myLargeModalLabel1">¿Seguro que devolverá lista de verificación?</h4>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="row col-12">
                            <asp:Label runat="server" BorderStyle="None" class="col form-control col-4"><strong>ATM: </strong></asp:Label>
                            <asp:Label runat="server" BorderStyle="None" ID="lbcodATM2" class="col form-control col-8"></asp:Label>
                        </div>
                       
                        <div class="row col-12">
                            <asp:Label runat="server" BorderStyle="None" class="col form-control col-4"><strong>Sucursal de ATM: </strong></asp:Label>
                            <asp:Label runat="server" BorderStyle="None" ID="lbsucursalATM2" class="col form-control col-8"></asp:Label>
                        </div>

                        <div class="row col-12">
                            <asp:Label runat="server" BorderStyle="None" class="col form-control col-4"><strong>Inventario de ATM: </strong>  </asp:Label>
                            <asp:Label runat="server" BorderStyle="None" ID="lbInventarioATM2" class="col form-control col-8"></asp:Label>
                        </div>
                        <div class="row col-12">
                            <asp:Label runat="server" BorderStyle="None" class="col form-control col-4"><strong>Responsable: </strong>  </asp:Label>
                            <asp:Label runat="server" BorderStyle="None" ID="lbtecnico2" class="col form-control col-8"></asp:Label>
                        </div>
                        <div class="row col-12">
                            <asp:Label runat="server" BorderStyle="None" class="col form-control col-6"><strong>Motivo de rechazo: </strong>  </asp:Label>
                            <div class="col-md-12 align-self-center" style="margin-left: auto; margin-right: auto">
                            <asp:TextBox runat="server" ID="txtmotivoRechazo" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                </div>
                        </div>
                       
                        <div class="col-md-12 align-self-center" style="margin-left: auto; margin-right: auto">
                            <asp:TextBox runat="server" Enabled="false" Text="Ingrese motivo por el que devuelve verificación." Visible="false" ID="txtAlerta2" CssClass="form-control" Style="background-color: red; color: white; text-align: center;" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <div class="modal-footer col-12">
                            <asp:Button runat="server" ID="btnCerrarRechazoModal" OnClick="btnCerrarRechazoModal_Click" CssClass="btn btn-secundary mr-3" Text="Cancelar" />
                                <asp:Button runat="server" ID="btnRechazarModal" OnClick="btnRechazarModal_Click" CssClass="btn btn-success mr-3" Text="Devolver" />
                            
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

    <!--MODAL MATERIALES -->
    <div class="modal fade bs-example-modal-lg" id="modalMaterial" tabindex="-1" data-backdrop="static" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title" runat="server" id="titulo"></h4>
                        </div>
                        <br />
                        <div class="row col-12">
                            <asp:Label runat="server" ID="LBMensajeCantidad" BorderStyle="None" class="col form-control col-6"><strong>Cantidad de material utilizado</strong></asp:Label>
                            <asp:TextBox runat="server" ID="txtUsadoModal" CssClass="form-control col-6" TextMode="Number"></asp:TextBox>
                        </div>
                        <br />
                        <div class="col-md-12 align-self-center" style="margin-left: auto; margin-right: auto">
                            <asp:TextBox runat="server" Enabled="false" Text="Ingrese una cantidad válida." Visible="false" ID="txtAlerta3" CssClass="form-control" Style="background-color: red; color: white; text-align: center;" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                        <div class="modal-footer col-12">
                           <asp:Button runat="server" ID="btnModalCerrarMaterial" CssClass="btn btn-secundary mr-2" Text="Cancelar" />
                                <asp:Button runat="server" ID="btnModarDevolverMaterial"  CssClass="btn btn-success mr-2" Text="Enviar" />
                           
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
