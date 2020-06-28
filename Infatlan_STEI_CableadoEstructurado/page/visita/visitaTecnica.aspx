<%@ Page Title="" Language="C#"  MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="visitaTecnica.aspx.cs" Inherits="Infatlan_STEI_CableadoEstructurado.paginas.estudioEstructurado" Culture="es-ES" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">


    <meta lang="es-es">
    <style> 
        .hidden{
            display:none;
        }
    </style>
    <script src="http://code.jquery.com/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="js/execute.js"></script>

    <script type="text/javascript">
        //Imagen 1
        function img1(input) {

            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    //$('#imgCuartoTelecomunicaciones').css('visibility', 'visible');
                    //$('#imgCuartoTelecomunicaciones').attr('src', e.target.result);
                    var ruta1 = e.target.result;
                    document.getElementById('<%=imgCuartoTelecomunicaciones.ClientID%>').src = ruta1;
                    document.getElementById('<%=HFCuartoTelecomunicaciones.ClientID%>').value = 'si';
                }
                reader.readAsDataURL(input.files[0]);
            }
        }

        //Imagen 2
        function img2(input) {

            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    //$('#imgReubicar').css('visibility', 'visible');
                    //$('#imgReubicar').attr('src', e.target.result);
                    var ruta2 = e.target.result;
                    document.getElementById('<%=imgReubicar.ClientID%>').src = ruta2;
                    document.getElementById('<%=HFReubicar.ClientID%>').value = 'si';
                }
                reader.readAsDataURL(input.files[0]);
            }
        }

        //Imagen 3
        function img3(input) {

            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    //$('#imgDesordenado').css('visibility', 'visible');
                    //$('#imgDesordenado').attr('src', e.target.result);
                    var ruta3 = e.target.result;
                    document.getElementById('<%=imgDesordenado.ClientID%>').src = ruta3;
                    document.getElementById('<%=HFDesordenado.ClientID%>').value = 'si';
                }
                reader.readAsDataURL(input.files[0]);
            }
        }

        //Imagen 4
        function img4(input) {

            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    //$('#imgExpuestoHumedo').css('visibility', 'visible');
                    //$('#imgExpuestoHumedo').attr('src', e.target.result);
                    var ruta4 = e.target.result;
                    document.getElementById('<%=imgExpuestoHumedo.ClientID%>').src = ruta4;
                    document.getElementById('<%=HFExpuestoHumedo.ClientID%>').value = 'si';
                }
                reader.readAsDataURL(input.files[0]);
            }
        }

        //Imagen 5
        function img5(input) {

            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    //$('#imgExpuestoRobo').css('visibility', 'visible');
                    //$('#imgExpuestoRobo').attr('src', e.target.result);
                    var ruta5 = e.target.result;
                    document.getElementById('<%=imgExpuestoRobo.ClientID%>').src = ruta5;
                    document.getElementById('<%=HFExpuestoRobo.ClientID%>').value = 'si';
                }
                reader.readAsDataURL(input.files[0]);
            }
        }

        //Imagen 6
        function img6(input) {

            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    //$('#imgElementoAjeno').css('visibility', 'visible');
                    //$('#imgElementoAjeno').attr('src', e.target.result);
                    var ruta6 = e.target.result;
                    document.getElementById('<%=imgElementoAjeno.ClientID%>').src = ruta6;
                    document.getElementById('<%=HFEquiposAjeno.ClientID%>').value = 'si';
                }
                reader.readAsDataURL(input.files[0]);
            }
        }

        //Imagen 7
        function img7(input) {
            if (input.files && input.files[0]) {

                var reader = new FileReader();
                reader.onload = function (e) {
                    //$('#imgPlano').css('visibility', 'visible');
                    //$('#imgPlano').attr('src', e.target.result);
                    var ruta7 = e.target.result;
                    document.getElementById('<%=IFramePDF.ClientID%>').src = ruta7;
                    document.getElementById('<%=HFPlano.ClientID%>').value = 'si';
                    // document.getElementById("IFramePDF").style.visibility = "hidden";


                }
                reader.readAsDataURL(input.files[0]);
            }
        }

        //Imagen 8
        function img8(input) {

            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    //$('#imgElementoAjeno').css('visibility', 'visible');
                    //$('#imgElementoAjeno').attr('src', e.target.result);
                    var ruta8 = e.target.result;
                    document.getElementById('<%=imgUPS.ClientID%>').src = ruta8;
                    document.getElementById('<%=HFUPS.ClientID%>').value = 'si';
                }
                reader.readAsDataURL(input.files[0]);
            }
        }

        //Imagen 9
        function img9(input) {

            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    //$('#imgElementoAjeno').css('visibility', 'visible');
                    //$('#imgElementoAjeno').attr('src', e.target.result);
                    var ruta9 = e.target.result;
                    document.getElementById('<%=imgAire.ClientID%>').src = ruta9;
                    document.getElementById('<%=HFAire.ClientID%>').value = 'si';
                }
                reader.readAsDataURL(input.files[0]);
            }
        }
    </script>

    <script type="text/javascript">

        function mostrarIframePDF() {
            document.getElementById('IFramePDF').style.display = 'block';
            //document.getElementById("IFramePDF").style.display= "block";
            return true;
        }

    </script>



    <script type="text/javascript">
        //Abrir modal Datos Generales
        function openModalDatosGenerales() {
            $('#MensajeAceptacionModal').modal('show');
        }

        // Cerrar modal
        function closeModalDatosGenerales() {
            $('#MensajeAceptacionModal').modal('hide');
        }

        //Abrir modal Aprobacion
        function openModalAprobacion() {
            $('#MensajeAceptacionModalApro').modal('show');
        }

        // Cerrar modal
        function closeModalAprobacion() {
            $('#MensajeAceptacionModalApro').modal('hide');
        }
    </script>

    <link href="../../assets/node_module/select2/dist/css/select2.min.css" rel="stylesheet" type="text/css" />
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
                    <li class="breadcrumb-item"><a href="javascript:void(0)"><asp:Label ID="Label12" runat="server"></asp:Label></a></li>
                    <li class="breadcrumb-item active">
                        <asp:Label ID="Label11" runat="server"></asp:Label>
                    </li>
                </ol>
            </div>
        </div>
    </div>

    <%-- Inicio Secciones--%>
    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title"><asp:Label ID="lbTituloVisita" runat="server" Text="Creación Estudio"></asp:Label></h4>
                    <h6 class="card-subtitle">Empresas que abastecen el stock de inventario.</h6>
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="nav-item"><a class="nav-link active" data-toggle="tab" href="#nav-Compensatorio" role="tab"><span class="hidden-sm-up"><i class="ti-home"></i></span><span class="hidden-xs-down">  Datos Generales</span></a> </li>
                        <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#nav_EstudioPrevio" role="tab"><span class="hidden-sm-up"><i class="ti-pencil-alt"></i></span><span class="hidden-xs-down">  Estudio Previo</span></a> </li>
                        <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#nav_Materiales" role="tab"><span class="hidden-sm-up"><i class="ti-settings"></i></span><span class="hidden-xs-down">  Materiales</span></a> </li>
                        <li class="nav-item"><a runat="server" id="nav" class="nav-link" data-toggle="tab" href="#nav_Estimacion" role="tab"><span class="hidden-sm-up"><i class="icon-calculator"></i></span><span class="hidden-xs-down">  Estimación de Recursos</span></a> </li>
                        <li class="nav-item"><a runat="server" id="navAprobacion" class="nav-link" data-toggle="tab" href="#nav_Aprobacion" role="tab"><span class="hidden-sm-up"><i class="ti-check-box"></i></span><span class="hidden-xs-down">  Aprobación Jefe</span></a> </li>
                    </ul>

                    <div class="tab-content tabcontent-border">
                        <%-- Sección 1 --%>
                        <div class="tab-pane active" id="nav-Compensatorio" role="tabpanel">
                            <div class="p-20">
                                <div class="row">
                                    <div class="col-12 grid-margin stretch-card">
                                        <div class="card">
                                            <div class="card-body">
                                                <h4 class="card-title">Registro de datos generales</h4>
                                                <br />
                                                <br />

                                                <asp:UpdatePanel runat="server" ID="udpResposable" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Técnico responsable</label>
                                                                    <asp:DropDownList ID="ddlResponsable" runat="server" CssClass="select2 form-control custom-select" Style="width: 100%" OnSelectedIndexChanged="ddlResponsable_SelectedIndexChanged" AutoPostBack="true" Enabled="true"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label class="control-label">Identidad</label>
                                                                    <asp:TextBox ID="txtIdentidad" runat="server" type="text" class="form-control" ReadOnly="true"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label class="control-label">Zona</label>
                                                                    <asp:TextBox ID="txtZona" runat="server" type="text" class="form-control" ReadOnly="true"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                                <asp:UpdatePanel runat="server" ID="udpAgencia" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group ">
                                                                    <label class="control-label">Agencia</label>
                                                                        <asp:DropDownList ID="ddlAgencia" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlAgencia_SelectedIndexChanged" CssClass="select2 form-control custom-select" Style="width: 100%" Enable="false"></asp:DropDownList>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group ">
                                                                    <label class="control-label">Área</label>
                                                                    <asp:DropDownList ID="ddlArea" runat="server" class="form-control" AutoPostBack="true" CssClass="select2 form-control custom-select" Style="width: 100%" Enable="false"></asp:DropDownList>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Dirreción</label>
                                                                    <asp:TextBox ID="txtDireccion" runat="server" type="text" class="form-control" ReadOnly="true"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>


                                                <asp:UpdatePanel runat="server" ID="udpFechasEstudio" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group ">
                                                                    <label for="example-datetime-local-input">Fecha estudio</label>
                                                                    <asp:TextBox value="2011-08-19T13:45:00" ID="txtFechaEstudio" runat="server" type="date" class="form-control" ReadOnly="false"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group ">
                                                                    <label for="example-datetime-local-input">Fecha envío</label>
                                                                    <asp:TextBox value="2011-08-19T13:45:00" ID="txtFechaEnvio" runat="server" type="date" class="form-control" ReadOnly="false" AutoPostBack="true"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                                <asp:UpdatePanel runat="server" ID="udpNuevoRemodelacion" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div class="row">
                                                            <div class="col-md-2">
                                                                <div class="form-group">
                                                                    <label class="control-label text-danger" runat="server" visible="false" id="Label1">*</label>
                                                                    <label class="control-label">Estudio a presentar:</label>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <div class="form-group">
                                                                    <asp:RadioButtonList ID="rblNuevoRemodelacion" runat="server" CssClass="custom-checkbox" BorderStyle="None" RepeatDirection="Horizontal" AutoPostBack="true" OnTextChanged="rblNuevoRemodelacion_TextChanged">
                                                                        <asp:ListItem Value="nuevo" Text="&nbsp Instalación Nueva &nbsp &nbsp"></asp:ListItem>
                                                                        <asp:ListItem Value="remodelacion" Text="&nbsp Remodelación"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <%-- Sección 2 --%>
                        <div class="tab-pane  p-20" id="nav_EstudioPrevio" role="tabpanel">
                            <div class="row">
                                <div class="col-12 grid-margin stretch-card">
                                    <div class="card">
                                        <div class="card-body">



                                            <h4 class="card-title">Estudio previo</h4>
                                            <br />
                                            <br />
                                            <%-- Pregunta Telecomunicaciones --%>

                                            <asp:UpdatePanel runat="server" ID="UpdatePanel6" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div class="row ">
                                                        <div class="col-md-12 align-content-center">
                                                            <div class="form-group">
                                                                <asp:Label runat="server" ID="Label3"><h5><b>Fotografía cuarto telecomunicaciones </b></h5> </asp:Label>
                                                                <h6>Ingresar fotografía del cuarto telecomunicaciones del área o agencia a trabajar</h6>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-7">
                                                            <asp:FileUpload ID="fuCuartoTelecomunicaciones" accept=".png,.jpg,.jpeg"  runat="server" AllowMultiple="false" ClientIDMode="AutoID" onchange="img1(this);" Autoposback="true" Enabled="true" />
                                                        </div>

                                                        <div class="col-md-5">
                                                            <img runat="server" id="imgCuartoTelecomunicaciones" height="400" width="400" src="../../assets/images/image_not_available.png" style="border-width: 0px;" />
                                                        </div>

                                                    </div>
                                                    <%-- <hr />
                                                            <hr />--%>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <asp:UpdatePanel runat="server" ID="udpOcultarEstudioPrevio" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <%-- Pregunta Desordenado --%>
                                                    <asp:UpdatePanel runat="server" ID="udpDesordenado" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <div class="row p-t-20">

                                                                <div class="col-md-6">
                                                                    <label class="control-label text-danger" runat="server" id="id">*</label>
                                                                    <label class="control-label">¿El cableado se encuentra  desordenado?</label>
                                                                </div>


                                                                <div class="col-md-1">
                                                                    <asp:RadioButtonList ID="rblDesordenado" runat="server" CssClass="custom-checkbox" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblDesordenado_TextChanged" AutoPostBack="true" Enabled="true">
                                                                        <asp:ListItem Value="si" Text="Sí"></asp:ListItem>
                                                                        <asp:ListItem Value="no" Text="No"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </div>

                                                                <div class="col-md-4">
                                                                    <div class="form-group">
                                                                        <asp:FileUpload ID="fuDesordenado" accept=".png,.jpg,.jpeg" runat="server" AllowMultiple="false" ClientIDMode="AutoID" onchange="img3(this);" Visible="true" />
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <img runat="server" id="imgDesordenado" height="400" width="400" src="../../assets/images/image_not_available.png" style="border-width: 0px;" visible="true" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <%-- <hr />
                                                                <hr />--%>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>

                                                    <%-- Pregunta Reubicar --%>

                                                    <asp:UpdatePanel runat="server" ID="udpReubicar" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <div class="row p-t-20">
                                                                <div class="col-md-6">
                                                                    <label class="control-label text-danger" runat="server" id="lbReubicarAs">*</label>
                                                                    <label class="control-label" runat="server" id="lbReubicar" visible="true">¿Es necesario reubicar el equipo de telecomunicaciones?</label>
                                                                </div>

                                                                <div class="col-md-1">
                                                                    <asp:RadioButtonList ID="rblReubicar" runat="server" CssClass="custom-checkbox" BorderStyle="None" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblReubicar_TextChanged" AutoPostBack="true" Enabled="true">
                                                                          <%--<asp:ListItem Value="si" Text="&nbspSí"></asp:ListItem>--%>
                                                                        <asp:ListItem Value="si" Text="Sí"></asp:ListItem>
                                                                        <asp:ListItem Value="no" Text="No"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </div>

                                                                <div class="col-md-4">
                                                                    <div class="form-group">
                                                                        <asp:FileUpload ID="fuReubicar" accept=".png,.jpg,.jpeg" runat="server" onchange="img2(this);" Visible="false" Enabled="true" />
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <img runat="server" id="imgReubicar"  height="400" width="400" src="../../assets/images/image_not_available.png" style="border-width: 0px;" visible="false" />
                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <%-- <hr />
                                                            <hr />--%>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>

                                                    <asp:UpdatePanel runat="server" ID="udpVisible" UpdateMode="Conditional">
                                                        <ContentTemplate>

                                                            <%--   <div class="col-md-12" runat="server" id="Div1" visible="true">--%>

                                                            <%-- Pregunta Expuesto Humedad --%>

                                                            <asp:UpdatePanel runat="server" ID="udpExpuestoHumedo" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <div class="row p-t-20">
                                                                        <div class="col-md-6">
                                                                            <label class="control-label text-danger" runat="server" id="Label4">*</label>
                                                                            <label class="control-label">¿El equipo se encuentra expuesto  a humedad o polvo?</label>
                                                                        </div>

                                                                        <div class="col-md-1">
                                                                            <asp:RadioButtonList ID="rblExpuestoHumedo" runat="server" CssClass="custom-checkbox" BorderStyle="None" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblExpuestoHumedad_TextChanged" Enabled="true">
                                                                                <asp:ListItem Value="si" Text="Sí"></asp:ListItem>
                                                                                <asp:ListItem Value="no" Text="No"></asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </div>

                                                                        <div class="col-md-4">
                                                                            <div class="form-group">
                                                                                <asp:FileUpload ID="fuExpuestoHumedo" accept=".png,.jpg,.jpeg" runat="server" onchange="img4(this);" Visible="false" Enabled="true" />
                                                                            </div>

                                                                            <div class="form-group">
                                                                                <img runat="server" id="imgExpuestoHumedo" height="400" width="400" src="../../assets/images/image_not_available.png" style="border-width: 0px;" visible="false" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <%--<hr />
                                                                        <hr />--%>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>

                                                            <%-- Pregunta Expuesto Robo --%>

                                                            <asp:UpdatePanel runat="server" ID="udpExpuestoRobo" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <div class="row p-t-20">
                                                                        <div class="col-md-6">
                                                                            <label class="control-label text-danger" runat="server" id="Label5">*</label>
                                                                            <label class="control-label">¿El equipo se encuentra expuesto  a robo o daño?</label>
                                                                        </div>

                                                                        <div class="col-md-1">

                                                                            <asp:RadioButtonList ID="rblExpuestoRobo" runat="server" CssClass="custom-checkbox" BorderStyle="None" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblExpuestoRobo_TextChanged" Enabled="true">
                                                                                <asp:ListItem Value="si" Text="Sí"></asp:ListItem>
                                                                                <asp:ListItem Value="no" Text="No"></asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </div>

                                                                        <div class="col-md-4">
                                                                            <div class="form-group">
                                                                                <asp:FileUpload ID="fuExpuestoRobo" accept=".png,.jpg,.jpeg" runat="server" onchange="img5(this);" Visible="false" Enabled="true" />
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <img runat="server" id="imgExpuestoRobo" height="400" width="400" src="../../assets/images/image_not_available.png" style="border-width: 0px;" visible="false" />
                                                                            </div>
                                                                        </div>

                                                                    </div>
                                                                    <%--<hr />
                                                                        <hr />--%>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>

                                                            <%-- Pregunta Equipos Ajenos --%>

                                                            <asp:UpdatePanel runat="server" ID="udpExpuestoAjenos" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <div class="row p-t-20">
                                                                        <div class="col-md-6">
                                                                            <label class="control-label text-danger" runat="server" id="Label6">*</label>
                                                                            <label class="control-label">¿Se encuentra elementos ajenos al equipo de comunicaciones?</label>
                                                                        </div>

                                                                        <div class="col-md-1">
                                                                            <asp:RadioButtonList ID="rblElementoAjenos" runat="server" CssClass="custom-checkbox" BorderStyle="None" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblElementosAjenos_TextChanged" Enabled="true">
                                                                                <asp:ListItem Value="si" Text="Sí"></asp:ListItem>
                                                                                <asp:ListItem Value="no" Text="No"></asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </div>

                                                                        <div class="col-md-4">
                                                                            <div class="form-group">
                                                                                <asp:FileUpload ID="fuElemetoAjenos" accept=".png,.jpg,.jpeg" runat="server" onchange="img6(this);" Visible="false" Enabled="true" />
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <img runat="server" id="imgElementoAjeno" height="400" width="400" src="../../assets/images/image_not_available.png" style="border-width: 0px;" visible="false" />
                                                                            </div>
                                                                        </div>

                                                                    </div>
                                                                    <%--<hr />
                                                                        <hr />--%>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>

                                                            <%-- PreguntaS UPS --%>

                                                            <asp:UpdatePanel runat="server" ID="udpUPS" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <div class="row p-t-20">
                                                                        <div class="col-md-6">
                                                                            <%--<div class="form-group">--%>
                                                                                <label class="control-label text-danger" runat="server" id="Label7">*</label>
                                                                                <label class="control-label">¿Cuenta con UPS?</label>
                                                                            <%--</div>--%>
                                                                        </div>

                                                                        <div class="col-md-1">
                                                                            <asp:RadioButtonList ID="rblUps" runat="server" CssClass="custom-checkbox" BorderStyle="None" RepeatDirection="Horizontal" Enabled="true" OnSelectedIndexChanged="rblUps_TextChanged" AutoPostBack="true">
                                                                                <asp:ListItem Value="si" Text="Sí"></asp:ListItem>
                                                                                <asp:ListItem Value="no" Text="No"></asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </div>

                                                                        <div class="col-md-4">
                                                                            <div class="form-group">
                                                                                <asp:FileUpload ID="fuUPS" accept=".png,.jpg,.jpeg" runat="server" onchange="img8(this);" Visible="false" Enabled="true" />
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <img runat="server" id="imgUPS" height="400" width="400" src="../../assets/images/image_not_available.png" style="border-width: 0px;" visible="false" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>

                                                            <%-- Preguntas Aire Acondicionado --%>

                                                            <asp:UpdatePanel runat="server" ID="udpAireAcondicionado" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <div class="row p-t-20">
                                                                        <div class="col-md-6">
                                                                           <%-- <div class="form-group">--%>
                                                                                <label class="control-label text-danger" runat="server" id="Label8">*</label>
                                                                                <label class="control-label">¿Cuenta con aire acondicionado?</label>
                                                                           <%-- </div>--%>
                                                                        </div>

                                                                        <div class="col-md-1">
                                                                            <asp:RadioButtonList ID="rblAire" runat="server" CssClass="custom-checkbox" BorderStyle="None" RepeatDirection="Horizontal" Enabled="true" OnSelectedIndexChanged="rblAire_TextChanged" AutoPostBack="true">
                                                                                <asp:ListItem Value="si" Text="Sí"></asp:ListItem>
                                                                                <asp:ListItem Value="no" Text="No"></asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </div>

                                                                        <div class="col-md-4">
                                                                            <div class="form-group">
                                                                                <asp:FileUpload ID="fuAire" accept=".png,.jpg,.jpeg" runat="server" onchange="img9(this);" Visible="false" Enabled="true" />
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <img runat="server" id="imgAire" height="400" width="400" src="../../assets/images/image_not_available.png" style="border-width: 0px;" visible="false" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                            <%--</div>--%>
                                                            <%-- <hr />
                                                                 <hr />--%>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>


                                                    <%-- Pregunta Etiquetado --%>

                                                    <asp:UpdatePanel runat="server" ID="udpEtiquetado" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <div class="row p-t-20">
                                                                <div class="col-md-6">
                                                                    <label class="control-label text-danger" runat="server" id="lbEtiquetadoAs">*</label>
                                                                    <label class="control-label" id="lbEtiquetado" runat="server" visible="true">¿La red instalada se encuentra etiquetada?</label>
                                                                </div>

                                                                <div class="col-md-4">
                                                                    <asp:RadioButtonList ID="rblEtiquetado" runat="server" CssClass="custom-checkbox" BorderStyle="None" RepeatDirection="Horizontal" AutoPostBack="true" Enabled="true">
                                                                        <asp:ListItem Value="si" Text="Sí"></asp:ListItem>
                                                                        <asp:ListItem Value="no" Text="No"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </div>

                                                            </div>
                                                            <%--   <hr />
                                                            <hr />--%>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>

                                                    <%-- Pregunta Categoria --%>

                                                    <asp:UpdatePanel runat="server" ID="udpCategoria" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <div class="row p-t-20">
                                                                <div class="col-md-6">
                                                                    <div class="form-group">
                                                                        <label class="control-label text-danger" runat="server" id="Label9">*</label>
                                                                        <label class="control-label">¿Categorías de cables instalados en Agencia?</label>
                                                                    </div>

                                                                </div>

                                                                <div class="col-md-1">
                                                                    <div class="form-group">
                                                                        <label class="control-label" runat="server" id="Label10">Categoría</label>
                                                                    </div>
                                                                </div>

                                                                <div class="col-md-2">
                                                                    <div class="form-group">
                                                                        <asp:TextBox runat="server" ID="txtCategoria" class="form-control" Enabled="true"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                                <%--  <div class="col-md-6">
                                                                                <div class="form-group">
                                                                                    <label class="control-label text-danger" runat="server" id="Label11">*</label>
                                                                                    <label class="control-label">¿Cuenta con los estandares de rotulación?</label>
                                                                                </div>
                                                                            </div>

                                                                            <div class="col-md-2">
                                                                                <asp:RadioButtonList ID="rblRotulacion" runat="server" CssClass="custom-checkbox" BorderStyle="None" RepeatDirection="Horizontal" Enabled="true">
                                                                                    <asp:ListItem Value="si" Text="Si"></asp:ListItem>
                                                                                    <asp:ListItem Value="no" Text="No"></asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                            </div>--%>
                                                            </div>
                                                            <%--<hr />
                                                                        <hr />--%>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>

                                                    <%-- </div>--%>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>

                                            <br />
                                            <%-- PDF Plano --%>
                                            <asp:UpdatePanel runat="server" ID="udpPDFplano" UpdateMode="conditional">
                                                <ContentTemplate>

                                                    <div class="row ">
                                                        <div class="col-md-12 align-content-center">
                                                            <div class="form-group">
                                                                <asp:Label runat="server" ID="lbplano"><h5><b>Plano </b></h5> </asp:Label>
                                                                <h6><asp:Label runat="server" ID="lbIngresarPlano" Text="Ingresar plano del área o agencia a trabajar"></asp:Label></h6>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <div style="align-content: center">
                                                                    <asp:FileUpload ID="fuPlano" runat="server" Accept=".pdf" onchange="img7(this);" Enabled="true" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                         <iframe runat="server" id="IFramePDF" style="width: 100%; height: 500px; border: none;"></iframe>
                                                        
                                                        <br />
                                                           
                                                        

                                                        <%-- <div class="col-md-6">
                                                            <div class="form-group">
                                                                <asp:UpdatePanel runat="server" ID="udpLbPlano" UpdateMode="Conditional">
                                                                    <ContentTemplate>

                                                                        <asp:LinkButton runat="server" Title="Ver PDF" ID="LbtnPlano" OnClick="LbtnPlano_Click" AutoPostBack="true" >Ver</asp:LinkButton>

                                                                        <%--<div id="vidPreview" runat="server" visible="false">--%>

                                                        <%--</div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>--%>
                                                      


                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <%-- Sección 3 --%>
                        <div class="tab-pane p-20" id="nav_Materiales" role="tabpanel">
                            <div class="row">
                                <div class="col-12 grid-margin stretch-card">
                                    <div class="card">
                                        <div class="card-body" runat="server" id="bodyMateriales">
                                            <h4 class="card-title"><asp:Label runat="server" ID="LbTituloMaterialesSoli" Text="Materiales a solicitar según estudio"></asp:Label></h4>
                                            <br />
                                            <br />

                                            <div class="row" id="DivMateriales" runat="server">
                                                <div class="col-md-4">
                                                    <asp:UpdatePanel runat="server" ID="udpMetariales" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <div class="form-group">
                                                                  <asp:label class="control-label" runat="server" ID="LBddlMaterial">Materiales</asp:label>
                                                                  <asp:DropDownList ID="ddlMateriales" runat="server" CssClass="select2 form-control custom-select" Style="width: 100%" OnSelectedIndexChanged="ddlMateriales_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>

                                                                <%--<asp:Label class="control-label" Style="margin-bottom:10px" runat="server" ID="LBddlMaterial">Materiales</asp:Label>
                                                                <asp:DropDownList ID="ddlMateriales" runat="server" AutoPostBack="true" class="form-control" Style="width: 100%" OnSelectedIndexChanged="ddlMateriales_SelectedIndexChanged" CssClass="select2 form-control custom-select"></asp:DropDownList>--%>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>

                                                <%--<div class="row">
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <label class="control-label">Técnico responsable</label>
                                                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="select2 form-control custom-select" Style="width: 100%" OnSelectedIndexChanged="ddlResponsable_SelectedIndexChanged" AutoPostBack="true" Enabled="true"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>--%>


                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <asp:UpdatePanel runat="server" ID="udpCantidad" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <asp:Label class="control-label" runat="server" ID="lbCantidadMaterial">Cantidad</asp:Label>
                                                                <asp:TextBox ID="txtCantidad" runat="server" class="form-control" TextMode="Number" min="0" ReadOnly="false"></asp:TextBox>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>

                                                <div class="col-md-4">
                                                    <asp:UpdatePanel runat="server" ID="udpUnidades" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <div class="form-group ">
                                                                 <asp:label class="control-label" runat="server" ID="LbunidadMaterial">Medidas</asp:label>
                                                                <asp:DropDownList ID="ddlMedidas" runat="server" CssClass="select2 form-control custom-select" Style="width: 100%" OnSelectedIndexChanged="ddlMedidas_SelectedIndexChanged" AutoPostBack="true" Enabled="true"></asp:DropDownList>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>

                                                <div class="col-md-2">
                                                    <asp:UpdatePanel runat="server" ID="udpAgregar" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <br />
                                                            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" class="btn  btn-block btn-success" OnClick="btnAgregar_Click" Enabled="true" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>

                            <asp:UpdatePanel ID="UpdateDivMateriales" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="col-12 grid-margin stretch-card">
                                        <div class="card">
                                            <div class="card-body">
                                                <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div class="row">
                                                            <div class="form-group">
                                                                <h4 class="card-title" ><asp:Label runat="server" ID="LbTituloMaterial" Text=""></asp:Label></h4>
                                                                <p><asp:Label runat="server" ID="LbDescrpcionMaterial" Text=""></asp:Label></p>
                                                            </div>
                                                         </div>
                                                        <div class="row">
                                                            <div class="table-responsive">
                                                                <asp:GridView ID="GVMateriales" runat="server"
                                                                    CssClass="table table-bordered"
                                                                    AutoPostBack="true"
                                                                    PagerStyle-CssClass="pgr"
                                                                    HeaderStyle-CssClass="table"
                                                                    RowStyle-CssClass="rows"
                                                                    AutoGenerateColumns="false"
                                                                    AllowPaging="true"
                                                                    GridLines="None"
                                                                    PageSize="10"
                                                                    OnPageIndexChanging="GVMateriales_PageIndexChanging"
                                                                    OnRowCommand="GVMateriales_RowCommand">
                                                                    <Columns>
                                                                        <%--<asp:BoundField DataField="idEstudio" HeaderText="idEstudio" Visible="false" />--%>
                                                                        <asp:BoundField DataField="numero" HeaderText="Numero" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                                        <asp:BoundField DataField="nombre" HeaderText="Material" />
                                                                        <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
                                                                        <asp:BoundField DataField="medida" HeaderText="Medidas" />
                                                                        <asp:TemplateField  HeaderStyle-Width="60px" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="BtnBorrar" runat="server" class="btn btn-primary mr-2" CommandArgument='<%# Eval("numero") %>' CommandName="Eliminar" Title="Borrar">
                                                                        <i class="icon-trash" ></i>
                                                                                </asp:LinkButton>
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
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>

                        <%-- Sección 4 --%>
                        <div class="tab-pane p-20" id="nav_Estimacion" role="tabpanel">
                            <div class="row">
                                <div class="col-12 grid-margin stretch-card">
                                    <div class="card">
                                        <div class="card-body">
                                            <h4 class="card-title">Estimación de recursos</h4>
                                            <br />
                                            <br />
                                            <div class="row">

                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <asp:UpdatePanel runat="server" ID="udpDuracion" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <label class="control-label">Duración del trabajo (Horas)</label>
                                                                <asp:TextBox ID="txtHorasTrabajo" runat="server" TextMode="Number" class="form-control" min="0" ReadOnly="false"></asp:TextBox>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>

                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <asp:UpdatePanel runat="server" ID="udpParticipantes" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <label class="control-label">Número de participantes</label>
                                                                <asp:TextBox ID="txtParticipantes" runat="server" type="Number" class="form-control" min="0" ReadOnly="false"></asp:TextBox>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>

                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <asp:UpdatePanel runat="server" ID="udpTransporte" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <label class="control-label">Transporte</label>
                                                                <br />
                                                                <asp:RadioButtonList ID="rblTransporte" runat="server" Enabled="true">
                                                                    <asp:ListItem Text="&nbspSí" Value="si"></asp:ListItem>
                                                                    <asp:ListItem Text="&nbspNo" Value="no"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>

                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <asp:UpdatePanel runat="server" ID="updAlimentacion" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <label class="control-label" style="align-content:center">Alimentación</label>
                                                                <br />
                                                                <asp:RadioButtonList ID="rblALimentación" runat="server" Enabled="true">
                                                                    <asp:ListItem Text="&nbspSí" Value="si"></asp:ListItem>
                                                                    <asp:ListItem Text="&nbspNo" Value="no"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>

                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <asp:UpdatePanel runat="server" ID="udpObservaciones" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <label class="control-label">Observaciones</label>
                                                                <asp:TextBox ID="txtObservaciones" TextMode="MultiLine" Rows="5" runat="server" type="text" class="form-control" ReadOnly="false"></asp:TextBox>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>

                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <asp:UpdatePanel runat="server" ID="udpGuardar" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <div style="margin-left: auto; margin-right: auto; text-align: center; width: 40%;">
                                                                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" type="button" class="btn btn-block btn-success" OnClick="btnGuardar_Click" Enabled="true" />
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>

                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <%-- Sección 5 --%>
                        <div class="tab-pane p-20" id="nav_Aprobacion" role="tabpanel">
                            <div class="row">
                                <div class="col-12 grid-margin stretch-card">
                                    <div class="card">
                                        <div class="card-body">
                                            <h4 class="card-title">Autorización</h4>
                                            <br />
                                            <br />

                                            <div class="row">

                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label class="control-label">¿La información ingresada es aprobada como valida?</label>
                                                        <br />
                                                        <asp:UpdatePanel runat="server" ID="udpAprobado" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <asp:RadioButtonList ID="rblAprobada" runat="server" Enabled="true" OnTextChanged="rblAprobada_TextChanged">
                                                                    <asp:ListItem Text="&nbspSí" Value="si"></asp:ListItem>
                                                                    <asp:ListItem Text="&nbspNo" Value="no"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>


                                                <div class="row col-6">
                                                    <div class="form-group">
                                                        <asp:UpdatePanel runat="server" ID="udpObservacionesAprobacion" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <label class="control-label">Observaciones </label>
                                                                <asp:TextBox TextMode="MultiLine" Rows="5" Columns="50" ID="txtObservacionesAprobacion" runat="server" type="text" class="form-control" ReadOnly="false"></asp:TextBox>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>


                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <asp:UpdatePanel runat="server" ID="udpEnviar" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <div style="margin-left: auto; margin-right: auto; text-align: center; width: 40%;">
                                                                    <asp:Button ID="btnEnviarAprobacion" runat="server" Text="Enviar" type="button" class="btn btn-block btn-success" OnClick="btnEnviar_Click" Enabled="true" />
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>

                                            </div>



                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="HFCuartoTelecomunicaciones" runat="server" />
            <asp:HiddenField ID="HFReubicar" runat="server" />
            <asp:HiddenField ID="HFDesordenado" runat="server" />
            <asp:HiddenField ID="HFExpuestoHumedo" runat="server" />
            <asp:HiddenField ID="HFExpuestoRobo" runat="server" />
            <asp:HiddenField ID="HFEquiposAjeno" runat="server" />
            <asp:HiddenField ID="HFUPS" runat="server" />
            <asp:HiddenField ID="HFAire" runat="server" />
            <asp:HiddenField ID="HFPlano" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <%-- MODAL DE MENSAJE DE CONFIRMACIÓN  DATOS GENERALES --%>
    <div class="modal fade" id="MensajeAceptacionModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:Label ID="lbMensaje" runat="server" CssClass="align-content-center"><b>¿Está seguro de que desea guardar los cambios efectuados? </b></asp:Label>
                </div>
                <div class="modal-body">
                    <asp:Label ID="lbAlerta" runat="server" CssClass="col-form-label text-white"><b></b></asp:Label>
                </div>
                <div class="col-md-12">
                    <div class="form-group row">
                        <div class="col-sm-12" runat="server" id="DivAlerta" visible="false" style="display: flex; background-color: tomato; justify-content: center">
                            <asp:Label runat="server" CssClass="col-form-label text-white" ID="LbAlertaGenerales"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:UpdatePanel ID="udpModGuardar" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                            <asp:Button ID="BtnModGuardar" runat="server" Text="Guardar" class="btn btn-primary" AutoPostBack="true" OnClick="BtnModGuardar_Click" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="BtnModGuardar" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>

            </div>
        </div>
    </div>

    <%-- MODAL DE MENSAJE DE CONFIRMACIÓN APROBACION --%>
    <div class="modal fade" id="MensajeAceptacionModalApro" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>

                <div class="modal-body">
                    <asp:Label ID="Label2" runat="server" CssClass="align-content-center"><b>¿Está seguro de que desea guardar los cambios efectuados? </b></asp:Label>
                </div>

                <%--<div class="modal-body">
                    <%--<asp:TextBox ID="txtCorreo" Text="" runat="server" ReadOnly="false"  Visible="false"></asp:TextBox>--%>
                <%--<asp:Label ID="LBCorreo" Text="" runat="server" CssClass="col-form-label"><b></b>Correo Técnico Responsable: Sharonvalle@gmail.com </asp:Label>
            </div>--%>

                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                            <asp:Button ID="btnModAproGuardar" runat="server" Text="Guardar" class="btn btn-primary" AutoPostBack="true" OnClick="btnModAproGuardar_Click" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnModAproGuardar" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>

            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <script src="../../assets/node_module/select2/dist/js/select2.full.min.js" type="text/javascript"></script>
    <style>
        .select2-selection__rendered {
            line-height: 31px !important;
        }

        .select2-container .select2-selection--single {
            height: 35px !important;
        }

        .select2-selection__arrow {
            height: 34px !important;
        }
    </style>
    <script>
        $(function () {
            $(".select2").select2();
            $(".ajax").select2({
                ajax: {
                    url: "https://api.github.com/search/repositories",
                    dataType: 'json',
                    delay: 250,
                    data: function (params) {
                        return {
                            q: params.term, // search term
                            page: params.page
                        };
                    },
                    processResults: function (data, params) {
                        params.page = params.page || 1;
                        return {
                            results: data.items,
                            pagination: {
                                more: (params.page * 30) < data.total_count
                            }
                        };
                    },
                    cache: true
                },
                escapeMarkup: function (markup) {
                    return markup;
                },
                minimumInputLength: 1,
            });
        });
    </script>
</asp:Content>
