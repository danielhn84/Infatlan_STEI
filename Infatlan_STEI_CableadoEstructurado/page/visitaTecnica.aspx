<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="visitaTecnica.aspx.cs" Inherits="Infatlan_STEI_CableadoEstructurado.paginas.estudioEstructurado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">

   
    
    <script src="http://code.jquery.com/jquery-1.10.2.min.js" type="text/javascript"></script>

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
                    document.getElementById('<%=imgPlano.ClientID%>').src = ruta7;
                    document.getElementById('<%=HFPlano.ClientID%>').value = 'si';
                }
                reader.readAsDataURL(input.files[0]);
            }
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

    <link href="../assets/node_modules/select2/dist/css/select2.min.css" rel="stylesheet"  type="text/css"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>


    <div class="row page-titles">
        <div class="col-md-12 align-self-center">
            <h4 class="text-themecolor">Visita Técnica</h4>
        </div>
    </div>

            <%-- Inicio Secciones--%>
            <div class="row">
                <div class="col-md-12">
                    <div class="card">
                        <div class="card-body">

                            <ul class="nav nav-tabs" role="tablist">
                                <li class="nav-item"><a class="nav-link active" data-toggle="tab" href="#nav-Compensatorio" role="tab"><span class="hidden-sm-up"><i class="ti-home"></i></span><span class="hidden-xs-down">Datos Generales</span></a> </li>

                                <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#nav_EstudioPrevio" role="tab"><span class="hidden-sm-up"><i class="ti-pencil-alt"></i></span><span class="hidden-xs-down">Estudio Previo</span></a> </li>

                                <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#nav_Materiales" role="tab"><span class="hidden-sm-up"><i class="ti-settings"></i></span><span class="hidden-xs-down">Materiales</span></a> </li>

                                <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#nav_Estimacion" role="tab"><span class="hidden-sm-up"><i class="ti-money"></i></span><span class="hidden-xs-down">Estimación de Recursos</span></a> </li>

                                <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#nav_Aprobacion" role="tab"><span class="hidden-sm-up"><i class="ti-check-box"></i></span><span class="hidden-xs-down" >Autorización Jefe</span></a> </li>

                            </ul>

                            <div class="tab-content tabcontent-border">
                                <%-- Sección 1 --%>
                                <div class="tab-pane active" id="nav-Compensatorio" role="tabpanel">
                                    <div class="p-20">
                                        <div class="row">
                                            <div class="col-12 grid-margin stretch-card">
                                                <div class="card">
                                                    <div class="card-body">
                                                        <h4 class="card-title">Registro de Datos Generales</h4>
                                                        <br />
                                                        <br />

                                                        <asp:UpdatePanel runat="server" ID="udpResposable" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <div class="form-group">
                                                                    <label class="control-label"><b>Técnico Responsable</b></label>
                                                                    <div class="col-sm-12">
                                                                        <asp:DropDownList ID="ddlResponsable" runat="server" CssClass="select2 form-control custom-select" style="width: 100%" OnSelectedIndexChanged="ddlResponsable_SelectedIndexChanged" class="form-control" AutoPostBack="true"></asp:DropDownList>
                                                                    </div>
                                                                </div>

                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <div class="form-group">
                                                                            <label class="control-label"><b>Identidad</b></label>
                                                                            <asp:TextBox ID="txtIdentidad" runat="server" type="text" class="form-control" ReadOnly="true"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <div class="form-group">
                                                                            <label class="control-label"><b>Zona</b></label>
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
                                                                            <label class="control-label"><b>Agencia</b></label>
                                                                            <div class="col-sm-12">
                                                                                <asp:DropDownList ID="ddlAgencia" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlAgencia_SelectedIndexChanged" CssClass="select2 form-control custom-select" style="width: 100%"></asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-6">
                                                                        <div class="form-group">
                                                                            <label class="control-label"><b>Dirreción</b></label>
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
                                                                            <label for="example-datetime-local-input" class="col-6 col-form-label"><b>Fecha Estudio</b></label>

                                                                            <div class="col-12">
                                                                                <asp:TextBox value="2011-08-19T13:45:00" ID="txtFechaEstudio" runat="server" type="date" class="form-control"></asp:TextBox>
                                                                            </div>

                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-6">
                                                                        <div class="form-group ">
                                                                            <label for="example-datetime-local-input" class="col-6 col-form-label"><b>Fecha Envío</b></label>
                                                                            <div class="col-12">
                                                                                <asp:TextBox value="2011-08-19T13:45:00" ID="txtFechaEnvio" runat="server" type="date" class="form-control" AutoPostBack="true"></asp:TextBox>
                                                                            </div>
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

                                                    <h4 class="card-title">Estudio Previo</h4>
                                                    <br />
                                                        <br />
                                                    <%-- Pregunta 1 --%>

                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel6" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <div class="row p-t-20">
                                                                <div class="col-md-4">
                                                                    <label class="control-label text-danger" runat="server" visible="false" id="Label11">*</label>
                                                                    <label class="control-label">Cuarto de telecomunicaciones antes de la inspección</label>
                                                                </div>

                                                                <div class="col-md-4">
                                                                    <asp:FileUpload ID="fuCuartoTelecomunicaciones" runat="server" onchange="img1(this);" Autoposback="true" />
                                                                </div>

                                                                <div class="col-md-4">
                                                                    <img runat="server" id="imgCuartoTelecomunicaciones" height="250" width="300" src="/assets/images/image_not_available.png" style="border-width: 0px;" />
                                                                </div>

                                                            </div>
                                                            <hr />
                                                            <hr />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>

                                                    <%-- Pregunta 2 --%>

                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel7" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <div class="row p-t-20">
                                                                <div class="col-md-3">
                                                                    <label class="control-label text-danger" runat="server" visible="false" id="Label12">*</label>
                                                                    <label class="control-label">¿El cableado se encuentra etiquetado?</label>
                                                                </div>

                                                                <div class="col-md-4">
                                                                    <asp:RadioButtonList ID="rblEtiquetado" runat="server" CssClass="custom-checkbox" BorderStyle="None" RepeatDirection="Horizontal" AutoPostBack="true">
                                                                        <asp:ListItem Value="si" Text="Si"></asp:ListItem>
                                                                        <asp:ListItem Value="no" Text="No"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </div>

                                                            </div>
                                                            <hr />
                                                            <hr />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>



                                                    <%-- Pregunta 3 --%>

                                                    <asp:UpdatePanel runat="server" ID="UpUPS" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <div class="row p-t-20">
                                                                <div class="col-md-3">
                                                                    <label class="control-label text-danger" runat="server" visible="false" id="Label10">*</label>
                                                                    <label class="control-label">¿Es necesario re-ubicar el equipo de telecomunicaciones?</label>
                                                                </div>

                                                                <div class="col-md-2">
                                                                    <asp:RadioButtonList ID="rblReubicar" runat="server" CssClass="custom-checkbox" BorderStyle="None" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblReubicar_TextChanged" AutoPostBack="true">
                                                                        <asp:ListItem Value="si" Text="Si"></asp:ListItem>
                                                                        <asp:ListItem Value="no" Text="No"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </div>

                                                                <div class="col-md-3">
                                                                    <asp:FileUpload ID="fuReubicar" runat="server" onchange="img2(this);" Visible="false" />
                                                                </div>

                                                                <div class="col-md-4">
                                                                    <img runat="server" id="imgReubicar" height="250" width="300" src="/assets/images/image_not_available.png" style="border-width: 0px;" visible="false" />
                                                                </div>
                                                            </div>
                                                            <hr />
                                                            <hr />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>

                                                    <asp:UpdatePanel runat="server" ID="udpVisible" UpdateMode="Conditional">
                                                        <ContentTemplate>

                                                            <div class="col-md-12" runat="server" id="Div1" visible="false">

                                                                <%-- Pregunta 4 --%>
                                                                <asp:UpdatePanel runat="server" ID="udpDesordenado" UpdateMode="Conditional">
                                                                    <ContentTemplate>

                                                                        <div class="row p-t-20">
                                                                            <div class="col-md-3">
                                                                                <label class="control-label text-danger" runat="server" id="lbUps">*</label>
                                                                                <label class="control-label">Cableado desordenado</label>
                                                                            </div>

                                                                            <div class="col-md-2">
                                                                                <asp:RadioButtonList ID="rblDesordenado" runat="server" CssClass="custom-checkbox" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblDesordenado_TextChanged" AutoPostBack="true">
                                                                                    <asp:ListItem Value="si" Text="Si"></asp:ListItem>
                                                                                    <asp:ListItem Value="no" Text="No"></asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                            </div>

                                                                            <div class="col-md-3">
                                                                                <asp:FileUpload ID="fuDesordenado" runat="server" onchange="img3(this);" Visible="false" />
                                                                            </div>

                                                                            <div class="col-md-4">

                                                                                <img runat="server" id="imgDesordenado" height="250" width="300" src="/assets/images/image_not_available.png" style="border-width: 0px;" visible="false" />
                                                                            </div>
                                                                        </div>
                                                                        <hr />
                                                                        <hr />
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>

                                                                <%-- Pregunta 5 --%>

                                                                <asp:UpdatePanel runat="server" ID="udpExpuestoHumedo" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <div class="row p-t-20">
                                                                            <div class="col-md-3">
                                                                                <label class="control-label text-danger" runat="server" visible="false" id="Label4">*</label><label class="control-label">Equipo expuesto a humedad o polvo</label>
                                                                            </div>

                                                                            <div class="col-md-2">
                                                                                <asp:RadioButtonList ID="rblExpuestoHumedo" runat="server" CssClass="custom-checkbox" BorderStyle="None" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblExpuestoHumedad_TextChanged">
                                                                                    <asp:ListItem Value="si" Text="Si"></asp:ListItem>
                                                                                    <asp:ListItem Value="no" Text="No"></asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                            </div>

                                                                            <div class="col-md-3">
                                                                                <asp:FileUpload ID="fuExpuestoHumedo" runat="server" onchange="img4(this);" Visible="false" />
                                                                            </div>

                                                                            <div class="col-md-4">
                                                                                <img runat="server" id="imgExpuestoHumedo" height="250" width="300" src="/assets/images/image_not_available.png" style="border-width: 0px;" visible="false" />
                                                                            </div>
                                                                        </div>
                                                                        <hr />
                                                                        <hr />
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>

                                                                <%-- Pregunta 6 --%>

                                                                <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <div class="row p-t-20">
                                                                            <div class="col-md-3">
                                                                                <label class="control-label text-danger" runat="server" visible="false" id="Label5">*</label>
                                                                                <label class="control-label">Equipo expuesto a robo o daño</label>
                                                                            </div>

                                                                            <div class="col-md-2">

                                                                                <asp:RadioButtonList ID="rblExpuestoRobo" runat="server" CssClass="custom-checkbox" BorderStyle="None" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblExpuestoRobo_TextChanged">
                                                                                    <asp:ListItem Value="si" Text="Si"></asp:ListItem>
                                                                                    <asp:ListItem Value="no" Text="No"></asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                            </div>

                                                                            <div class="col-md-3">
                                                                                <asp:FileUpload ID="fuExpuestoRobo" runat="server" onchange="img5(this);" Visible="false" />
                                                                            </div>

                                                                            <div class="col-md-4">
                                                                                <img runat="server" id="imgExpuestoRobo" height="250" width="300" src="/assets/images/image_not_available.png" style="border-width: 0px;" visible="false" />
                                                                            </div>
                                                                        </div>
                                                                        <hr />
                                                                        <hr />
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>

                                                                <%-- Pregunta 7 --%>
                                                                <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <div class="row p-t-20">
                                                                            <div class="col-md-3">
                                                                                <label class="control-label text-danger" runat="server" visible="false" id="Label6">*</label>
                                                                                <label class="control-label">Ajenos al equipo de comunicación</label>
                                                                            </div>

                                                                            <div class="col-md-2">
                                                                                <asp:RadioButtonList ID="rblElementoAjenos" runat="server" CssClass="custom-checkbox" BorderStyle="None" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblElementosAjenos_TextChanged">
                                                                                    <asp:ListItem Value="si" Text="Si"></asp:ListItem>
                                                                                    <asp:ListItem Value="no" Text="No"></asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                            </div>

                                                                            <div class="col-md-3">
                                                                                <asp:FileUpload ID="fuElemetoAjenos" runat="server" onchange="img6(this);" Visible="false" />
                                                                            </div>

                                                                            <div class="col-md-4">
                                                                                <img runat="server" id="imgElementoAjeno" height="250" width="300" src="/assets/images/image_not_available.png" style="border-width: 0px;" visible="false" />
                                                                            </div>
                                                                        </div>
                                                                        <hr />
                                                                        <hr />
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>

                                                                <%-- Pregunta 8 y 9 --%>

                                                                <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <div class="row p-t-20">
                                                                            <div class="col-md-3">
                                                                                <label class="control-label text-danger" runat="server" visible="false" id="Label7">*</label>
                                                                                <label class="control-label">¿Cuenta con UPS?</label>
                                                                            </div>

                                                                            <div class="col-md-3">
                                                                                <asp:RadioButtonList ID="rblUps" runat="server" CssClass="custom-checkbox" BorderStyle="None" RepeatDirection="Vertical">
                                                                                    <asp:ListItem Value="si" Text="Si"></asp:ListItem>
                                                                                    <asp:ListItem Value="no" Text="No"></asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                            </div>

                                                                            <div class="col-md-4">
                                                                                <label class="control-label text-danger" runat="server" visible="false" id="Label8">*</label>
                                                                                <label class="control-label">¿Cuenta con aire acondicionado?</label>
                                                                            </div>

                                                                            <div class="col-md-2">
                                                                                <asp:RadioButtonList ID="rblAire" runat="server" CssClass="custom-checkbox" BorderStyle="None" RepeatDirection="Vertical">
                                                                                    <asp:ListItem Value="si" Text="Si"></asp:ListItem>
                                                                                    <asp:ListItem Value="no" Text="No"></asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                            </div>
                                                                        </div>
                                                                        <hr />
                                                                        <hr />
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>

                                                                <%-- Pregunta 10 y 11 --%>

                                                                <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <div class="row p-t-20">
                                                                            <div class="col-md-3">
                                                                                <label class="control-label text-danger" runat="server" visible="false" id="Label9">*</label>
                                                                                <label class="control-label">Categoria de cables</label>
                                                                            </div>

                                                                            <div class="col-md-3">
                                                                                <asp:RadioButtonList ID="rblCategoria" runat="server" CssClass="custom-checkbox" BorderStyle="None" RepeatDirection="Vertical">
                                                                                    <asp:ListItem Value="5" Text="Categoria 5"></asp:ListItem>
                                                                                    <asp:ListItem Value="6" Text="Categoria 6"></asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                            </div>

                                                                            <div class="col-md-4">
                                                                                <label class="control-label text-danger" runat="server" visible="false" id="Labe">*</label>
                                                                                <label class="control-label">¿Cuenta con estandares de rotulación?</label>
                                                                            </div>

                                                                            <div class="col-md-2">
                                                                                <asp:RadioButtonList ID="rblRotulacion" runat="server" CssClass="custom-checkbox" BorderStyle="None" RepeatDirection="Vertical">
                                                                                    <asp:ListItem Value="si" Text="Si"></asp:ListItem>
                                                                                    <asp:ListItem Value="no" Text="No"></asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                            </div>
                                                                        </div>
                                                                        <hr />
                                                                        <hr />
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>

                                                            </div>


                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>

                                                    <asp:UpdatePanel runat="server" ID="udpImgPlano" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <br />
                                                            <br />

                                                            <div class="row ">
                                                                <div class="col-md-12 align-content-center">
                                                                    <div class="form-group">
                                                                        <asp:Label runat="server" ID="lbPlano"><h4><b>Plano del Estudio a Realizar</b></h4> </asp:Label>
                                                                       
                                                                    </div>
                                                                </div>

                                                          
                                                                <div class="col-md-4">
                                                                    <div class="form-group" style="align-content: center">
                                                                        <asp:FileUpload ID="fuPlano" runat="server" onchange="img7(this);" />

                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="align-content: center">

                                                                        <img runat="server" id="imgPlano" height="300" width="450" src="/assets/images/image_not_available.png" style="border-width: 0px;" />
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

                                <%-- Sección 3 --%>
                                <div class="tab-pane p-20" id="nav_Materiales" role="tabpanel">
                                    <div class="row">
                                        <div class="col-12 grid-margin stretch-card">
                                            <div class="card">
                                                <div class="card-body">
                                                    <h4 class="card-title">Materiales a Solicitar según estudio</h4>
                                                    <br />
                                                        <br />

                                                    <div class="row">
                                                        <div class=" col-5">
                                                            <asp:UpdatePanel runat="server" ID="udpMetariales" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <div class="col-md-12">
                                                                        <div class="form-group">
                                                                            <label class="control-label"><b>Materiales</b></label>
                                                                            <div class="col-sm-12">
                                                                                <asp:DropDownList ID="ddlMateriales" runat="server" AutoPostBack="true" class="form-control" Style="width: 100%" OnSelectedIndexChanged="ddlMateriales_SelectedIndexChanged" CssClass="select2 form-control custom-select"></asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                          </div>


                                                            <div class="col-md-2">
                                                                <div class="form-group">
                                                                    <asp:UpdatePanel runat="server" ID="udpCantidad" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <label class="control-label"><b>Cantidad</b></label>
                                                                            <asp:TextBox ID="txtCantidad" runat="server" class="form-control" TextMode="Number" min="0"></asp:TextBox>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-3">
                                                                <div class="form-group ">
                                                                    <label class="control-label"><b>Unidades</b></label>
                                                                    <asp:UpdatePanel runat="server" ID="udpUnidades" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <div class="col-sm-12">
                                                                                <asp:DropDownList ID="ddlMedidas" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlMedidas_SelectedIndexChanged" CssClass="select2 form-control custom-select" Style="width: 100%"></asp:DropDownList>
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>

                                                                </div>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <br />
                                                                <asp:UpdatePanel runat="server" ID="udpAgregar" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <asp:Button ID="btnAgregar" runat="server" Text="ADD" class="btn  btn-block btn-success" OnClick="btnAgregar_Click" />
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
                                                        <h4 class="card-title">Materiales Agregados</h4>
                                                        <p>Ordenados por fecha de ingreso</p>
                                                        <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
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
                                                                                <asp:BoundField DataField="numero" HeaderText="Numero" Visible="false" />
                                                                                <asp:BoundField DataField="nombre" HeaderText="Material" />
                                                                                <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
                                                                                <asp:BoundField DataField="medida" HeaderText="Unidades" />
                                                                                <asp:TemplateField HeaderStyle-Width="60px" Visible="true">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="BtnBorrar" runat="server" Text="Eliminar" class="btn btn-primary mr-2" CommandArgument='<%# Eval("numero") %>' CommandName="Eliminar">
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
                                                    <h4 class="card-title">Estimaciones de Recursos</h4>
                                                    <br />
                                                        <br />
                                                    <div class="row">

                                                        <asp:UpdatePanel runat="server" ID="udpDuracion" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <div class="col-md-12">
                                                                    <div class="form-group">
                                                                        <label class="control-label"><b>Duración del trabajo (Horas)</b></label>
                                                                        <asp:TextBox ID="txtHorasTrabajo" runat="server" TextMode="Number" class="form-control" min="0"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>

                                                        <asp:UpdatePanel runat="server" ID="udpParticipantes" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <div class="col-md-12">
                                                                    <div class="form-group">
                                                                        <label class="control-label"><b>Número de participantes</b></label>
                                                                        <asp:TextBox ID="txtParticipantes" runat="server" type="Number" class="form-control" min="0"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>

                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <asp:UpdatePanel runat="server" ID="udpTransporte" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <label class="control-label"><b>Transporte</b></label>
                                                                        <br />
                                                                        <asp:RadioButtonList ID="rblTransporte" runat="server">
                                                                            <asp:ListItem Text="Si" Value="si"></asp:ListItem>
                                                                            <asp:ListItem Text="No" Value="no"></asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>




                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <asp:UpdatePanel runat="server" ID="updAlimentacion" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <label class="control-label"><b>Alimentación</b></label>
                                                                        <br />
                                                                        <asp:RadioButtonList ID="rblALimentación" runat="server">
                                                                            <asp:ListItem Text="Si" Value="si"></asp:ListItem>
                                                                            <asp:ListItem Text="No" Value="no"></asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-12">
                                                            <div class="form-group">
                                                                <asp:UpdatePanel runat="server" ID="udpObservaciones" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <label class="control-label"><b>Observaciones</b></label>
                                                                        <asp:TextBox ID="txtObservaciones" TextMode="MultiLine" Rows="5" runat="server" type="text" class="form-control"></asp:TextBox>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-12">
                                                            <div class="form-group">
                                                                <asp:UpdatePanel runat="server" ID="udpGuardar" UpdateMode="Conditional">
                                                                    <ContentTemplate>

                                                                        <div style="margin-left: auto; margin-right: auto; text-align: center; width: 40%;">
                                                                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" type="button" class="btn btn-block btn-success" OnClick="btnGuardar_Click" />
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

                                                    <h4 class="card-title">Autorización del estudio</h4>
                                                    <br />
                                                        <br />
                                                    <div class="row">

                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label class="control-label"><b>¿La información ingresada es aprobada como valida?</b></label>
                                                                <br />
                                                                <asp:UpdatePanel runat="server" ID="udpAprobado" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <asp:RadioButtonList ID="rblAprobada" runat="server">
                                                                            <asp:ListItem Text="Si" Value="si"></asp:ListItem>
                                                                            <asp:ListItem Text="No" Value="no"></asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>

                                                        <div class="row col-6">
                                                            <div class="form-group">
                                                                <asp:UpdatePanel runat="server" ID="udpObservacionesAprobacion" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <label class="control-label"><b>Observaciones </b></label>
                                                                        <asp:TextBox TextMode="MultiLine" Rows="5" Columns="50" ID="txtObservacionesAprobacion" runat="server" type="text" class="form-control"></asp:TextBox>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>


                                                        <div class="col-md-12">
                                                            <div class="form-group">
                                                                <asp:UpdatePanel runat="server" ID="udpEnviar" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <div style="margin-left: auto; margin-right: auto; text-align: center; width: 40%;">
                                                                            <asp:Button ID="btnEnviarAprobacion" runat="server" Text="Enviar" type="button" class="btn btn-block btn-success" OnClick="btnEnviar_Click" />
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
                <div class="modal-body">
                    <asp:Label ID="Label3" runat="server" CssClass="col-form-label text-white"><b></b></asp:Label>
                </div>

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


    <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="HFCuartoTelecomunicaciones" runat="server" />
            <asp:HiddenField ID="HFReubicar" runat="server" />
            <asp:HiddenField ID="HFDesordenado" runat="server" />
            <asp:HiddenField ID="HFExpuestoHumedo" runat="server" />
            <asp:HiddenField ID="HFExpuestoRobo" runat="server" />
            <asp:HiddenField ID="HFEquiposAjeno" runat="server" />
            <asp:HiddenField ID="HFPlano" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">

    <script src="../assets/node_modules/select2/dist/js/select2.full.min.js" type="text/javascript"></script>
    <style>
        .select2-selection__rendered {line-height: 31px !important;}
        .select2-container .select2-selection--single {height: 35px !important;}
        .select2-selection__arrow {height: 34px !important;}
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
