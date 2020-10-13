<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="lvIndividual.aspx.cs" Inherits="Infatlan_STEI_Agencias.pages.LvIndividual" %>


<%@ Register Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>


<%--<%@ Page Title="" Language="C#" MasterPageFile="~/mainAgencia.Master" AutoEventWireup="true" CodeBehind="LvIndividual.aspx.cs" Inherits="Infatlan_STEI_Agencias.paginasAgencia.LvIndividual" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <title></title>
    <script type="text/javascript">

        var updateProgress = null;
        function postbackButtonClick() {
            updateProgress = $find("<%= UpdateProgress1.ClientID %>");
            window.setTimeout("updateProgress.set_visible(true)", updateProgress.get_displayAfter());
            return true;
        }

        function openModalEnvioLv() { $('#modalEnviarLv').modal('show'); }
        function closeModalEnvioLv() { $('#modalEnviarLv').modal('hide'); }

        function openModalAprobacionLv() { $('#modalAprobarLv').modal('show'); }
        function closeModalAprobacionLv() { $('#modalAprobarLv').modal('hide'); }


       
        function showpreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    //$('#ImgPreviewNoMantEquipoComu1').css('visibility', 'visible');
                    //$('#ImgPreviewNoMantEquipoComu1').attr('src', e.target.result);

                    var ruta0 = e.target.result;
                    document.getElementById('<%=ImgPreviewNoMantEquipoComu.ClientID%>').src = ruta0;
                   
                    document.getElementById('<%=TxResNoManEquipoComu1.ClientID%>').value = 'si';
                

                }
                reader.readAsDataURL(input.files[0]);

            }
        }
        

        function showpreview1(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    //$('#ImgPreviewClimatizacion1').css('visibility', 'visible');
                    //$('#ImgPreviewClimatizacion1').attr('src', e.target.result);  

                    var ruta1 = e.target.result;
                    document.getElementById('<%=ImgPreviewClimatizacion.ClientID%>').src = ruta1;
                    
                    document.getElementById('<%=TxClimatizacion1.ClientID%>').value = 'si'; 

                }
                reader.readAsDataURL(input.files[0]);               
                 
            }
        }


        function showpreview2(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    //$('#ImgPreviewUPS1').css('visibility', 'visible');
                    //$('#ImgPreviewUPS1').attr('src', e.target.result);

                    var ruta2 = e.target.result;
                    document.getElementById('<%=ImgPreviewUPS.ClientID%>').src = ruta2;
                  
                    document.getElementById('<%=TxUPS1.ClientID%>').value = 'si'; 

                }
                reader.readAsDataURL(input.files[0]);
             
            }
        }


        function showpreview3(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    //$('#ImgPreviewRack1').css('visibility', 'visible');
                    //$('#ImgPreviewRack1').attr('src', e.target.result);
                    var ruta3 = e.target.result;
                    document.getElementById('<%=ImgPreviewRack.ClientID%>').src = ruta3;
                   
                    document.getElementById('<%=TxRack1.ClientID%>').value = 'si';

                }
                reader.readAsDataURL(input.files[0]);            
            }
        }

        function showpreview4(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    //$('#ImgPreviewPolvoSuciedad1').css('visibility', 'visible');
                    //$('#ImgPreviewPolvoSuciedad1').attr('src', e.target.result);


                    var ruta4 = e.target.result;
                    document.getElementById('<%=ImgPreviewPolvoSuciedad.ClientID%>').src = ruta4;
                   
                    document.getElementById('<%=TxPolvoSuciedad1.ClientID%>').value = 'si';
                }
                reader.readAsDataURL(input.files[0]);               
            }
        }

        function showpreview5(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {

                    //$('#ImgPreviewHumedadSustancias1').css('visibility', 'visible');
                    //$('#ImgPreviewHumedadSustancias1').attr('src', e.target.result);

                    var ruta5 = e.target.result;
                    document.getElementById('<%=ImgPreviewHumedadSustancias.ClientID%>').src = ruta5;
              
                    document.getElementById('<%=TxHumedadSustancias1.ClientID%>').value = 'si';
                }
                reader.readAsDataURL(input.files[0]);             
            }
        }

        function showpreview6(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    //$('#ImgPreviewRoboDaño1').css('visibility', 'visible');
                    //$('#ImgPreviewRoboDaño1').attr('src', e.target.result);

                    var ruta6 = e.target.result;
                    document.getElementById('<%=ImgPreviewRoboDaño.ClientID%>').src = ruta6;
                  
                    document.getElementById('<%=TxRoboDaño1.ClientID%>').value = 'si';
                }
                reader.readAsDataURL(input.files[0]);        
            }
        }

        function showpreview7(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    //$('#ImgPreviewElementosAjenos1').css('visibility', 'visible');
                    //$('#ImgPreviewElementosAjenos1').attr('src', e.target.result);
                    var ruta7 = e.target.result;
                    document.getElementById('<%=ImgPreviewElementosAjenos.ClientID%>').src = ruta7;
            
                    document.getElementById('<%=TxElementosAjenos1.ClientID%>').value = 'si';

                }
                reader.readAsDataURL(input.files[0]);
            }
        }


        function showpreview8(input) {           
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    var ruta8 = e.target.result;
                    document.getElementById('<%=ImgPreviewEspacioFisico.ClientID%>').src = ruta8; 

                    document.getElementById('<%=TxEspacioFisico1.ClientID%>').value = 'si';
                }
                reader.readAsDataURL(input.files[0]);
            }
        }


       

       
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #ffffff; opacity: 0.7; margin: 0;">
                <span style="display: inline-block; height: 100%; vertical-align: middle;"></span>
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="/assets/images/loading.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="display: inline-block; vertical-align: middle;" />
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
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Agencias</a></li>
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Mantenimiento</a></li>
                    <li class="breadcrumb-item active">Individual</li>
                </ol>
            </div> 
        </div>
    </div>

    <%--(INICIO) Targeta Datos Generales --%>
    <div class="card">
        <div class="card-body">
            <asp:UpdatePanel ID="UpTitulo" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <h2 class="text-themecolor">
                        <asp:Label ID="TituloPagina" runat="server" Text="" Style="margin-left: auto; margin-right: auto"></asp:Label>
                    </h2>
                </ContentTemplate>
            </asp:UpdatePanel>
            <h6 class="card-title">Datos Generales</h6>

            <div class="row p-t-20">
                <div class="col-md-6">
                    <label class="control-label">Fecha Mantenimiento:</label>
                    <asp:TextBox ID="TxFechaMant" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <label class="control-label">No. SysAid:</label>
                    <asp:TextBox ID="TxSysAid" class="form-control" runat="server" ReadOnly="true" Text="Si"></asp:TextBox>
                </div>
            </div>

            <div class="row p-t-20">
                <div class="col-md-6">
                    <label class="control-label">Lugar:</label>
                    <asp:TextBox ID="TxLugar" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <label class="control-label">Area:</label>
                    <asp:TextBox ID="TxArea" class="form-control" runat="server" ReadOnly="true" Text="Si"></asp:TextBox>
                </div>
            </div>

            <div class="row p-t-20">
                <div class="col-md-6">
                    <label class="control-label">Código Agencia:</label>
                    <asp:TextBox ID="TxCodigoAgencia" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <label class="control-label">Mant. Equipo Comunicación:</label>
                    <asp:TextBox ID="TxManEquipoComunicacion" class="form-control" runat="server" ReadOnly="true" Text="Si"></asp:TextBox>
                </div>
            </div>

            <div class="row p-t-20">
                <div class="col-md-6">
                    <label class="control-label">Motivo:</label>
                    <asp:TextBox ID="TxMotivo" class="form-control" runat="server" Text="Realizar acciones pro activas para prevenir la falla de equipos críticos." ReadOnly="true" TextMode="MultiLine" Rows="2"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <label class="control-label">Impacto:</label>
                    <asp:TextBox ID="TxImpacto" class="form-control" runat="server" Text="Durante la ventana de mantenimiento la agencia estará cerrada para el público en general." ReadOnly="true" TextMode="MultiLine" Rows="2"></asp:TextBox>
                </div>
            </div>

            <div class="row p-t-20">
                <div class="col-md-6">
                    <label class="control-label text-danger" runat="server" visible="false" id="lbHoraSalida">*</label><label class="control-label">Hora Salida de Infatlan:</label>
                    <asp:TextBox ID="TxHoraSalidaINFA" class="form-control" runat="server" TextMode="Time"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <label class="control-label text-danger" runat="server" visible="false" id="lbHoraLlegada">*</label><label class="control-label">Hora Llegada a Infatlan:</label>
                    <asp:TextBox ID="TxHoraLlegadaINFA" class="form-control" runat="server" TextMode="Time"></asp:TextBox>
                </div>
            </div>

            <div class="row p-t-20">
                <div class="col-md-6">
                    <label class="control-label text-danger" runat="server" visible="false" id="lbInicioMant">*</label><label class="control-label">Hora Comienzo del Mantenimiento:</label>
                    <asp:TextBox ID="TxHoraInicioMant" class="form-control" runat="server" TextMode="Time"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <label class="control-label text-danger" runat="server" visible="false" id="lbFinMant">*</label><label class="control-label">Hora Finalización del Mantenimiento:</label>
                    <asp:TextBox ID="TxHoraFinMant" class="form-control" runat="server" TextMode="Time"></asp:TextBox>

                </div>
            </div>
        </div>
    </div>
    <%--(FIN) Targeta Datos Generales --%>

    <%--(INICIO) Targeta Personal Encargado --%>
    <div class="card">
        <div class="card-body">        
          <h3 class="card-title">Personal Encargado</h3>
           <br>
            <h5 class="card-title">-Técnico Responsable</h5>
            <div class="row p-t-20">
                <div class="col-md-6">
                    <label class="control-label">Nombre:</label>
                    <asp:TextBox ID="TxNombreTecnicoResponsable" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <label class="control-label">Identidad:</label>
                    <asp:TextBox ID="TxIdentidadTecnicoResponsable" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
            <br>
            <br>

            <div class="col-md-12" id="DivTecnicosParticipantes" runat="server" visible="false">
                <h5 class="card-title">-Técnicos Participantes </h5>
            </div>
            <div class="col-md-6" style="margin-left: auto; margin-right: auto" id="DivAlertaTecnicosParticipantes" runat="server" visible="false">
                <div class="alert alert-info   align-content-md-center" style="align-self: auto">
                    <h3 class="text-info" style="text-align: center"><i class="fa fa-exclamation-triangle"></i></h3>
                    <h5 class="text-info" style="text-align: center">No hay técnicos participantes asignados para este mantenimiento</h5>
                </div>
            </div>

            <%--TECNICOS PARTICIPANTES--%>
            <div class="row col-12">
                <div class="col-12 grid-margin stretch-card">
                    <div class="table-responsive">
                        <asp:UpdatePanel runat="server" ID="UPTecnicosParticipantes" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:GridView ID="GVTecnicosParticipantes" runat="server"
                                    CssClass="table table-bordered"
                                    PagerStyle-CssClass="pgr"
                                    HeaderStyle-CssClass="table"
                                    RowStyle-CssClass="rows"
                                    AutoGenerateColumns="false"
                                    AllowPaging="true"
                                    GridLines="None"
                                    PageSize="10">

                                    <Columns>
                                        <asp:BoundField DataField="nombre" HeaderText="Nombre" ItemStyle-Width="50%" />
                                        <asp:BoundField DataField="identidad" HeaderText="Identidad" ItemStyle-Width="50%" />
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>

    </div>
    <%--(FIN) Targeta Personal Encargado --%>

    <%--(INICIO) Targeta Datos Tecnicos --%>
    <div class="card">
        <div class="card-body">
            <h3 class="card-title">Datos Técnicos</h3>

            <div class="row p-t-20">
                <div class="col-md-2">
                    <label class="control-label text-danger" runat="server" visible="false" id="lbCantMaquinas">*</label><label class="control-label">Cant Maquinas:</label>
                </div>

                <div class="col-md-4">
                    <asp:TextBox ID="TxCantMaquinas" TextMode="Number" class="form-control" runat="server"></asp:TextBox>
                </div>

                <div class="col-md-2">
                    <label class="control-label text-danger" runat="server" visible="false" id="lbCantImpresora">*</label><label class="control-label">Impresora Financiera:</label>
                </div>

                <div class="col-md-4">
                    <asp:TextBox ID="TxCantImpresoraFinanciera" TextMode="Number" class="form-control" runat="server"></asp:TextBox>
                </div>
            </div>

            <div class="row p-t-20">
                <div class="col-md-2">
                    <label class="control-label text-danger" runat="server" visible="false" id="lbCantEscaner">*</label><label class="control-label">Cant Escaner Fenix:</label>
                </div>

                <div class="col-md-4">
                    <asp:TextBox ID="TxCantEscaner" class="form-control" runat="server" TextMode="Number"></asp:TextBox>
                </div>

                <div class="col-md-2">
                    <label class="control-label text-danger" runat="server" visible="false" id="lbCantDatacard">*</label><label class="control-label">Cant Datacards:</label>
                </div>

                <div class="col-md-4">
                    <asp:TextBox ID="TxCantDatacard" TextMode="Number" class="form-control" runat="server"></asp:TextBox>
                </div>
            </div>

            <br>
            <br>
            <hr>

            <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="row p-t-20">
                        <div class="col-md-6">
                            <label class="control-label text-danger" runat="server" visible="false" id="lbRealizoMantEquipoComu">*</label><label class="control-label">¿Realizó mantenimiento al equipo de comunicación?</label>
                        </div>
                        <div class="col-md-2">
                            <asp:RadioButtonList ID="RBLManEquipoComu" RepeatDirection="Horizontal" Width="90px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RBLManEquipoComu_SelectedIndexChanged1">
                                <asp:ListItem Value="1">Si</asp:ListItem>
                                <asp:ListItem Value="0">No</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>

                        <div class="col-md-4 ">
                            <asp:FileUpload ID="FuImageNoMantEquipoComu" runat="server" Visible="false" onchange="showpreview(this); " class="form-control"   />
                            <img id="ImgPreviewNoMantEquipoComu" height="250" width="382" src="../../assets/images/vistaPrevia1.JPG" style="border-width: 0px;" runat="server" visible="false" />                           
                        </div>
               
                    </div>
                    <asp:TextBox ID="TxMotivoNoMantEquipoComu" class="form-control" runat="server" TextMode="MultiLine" Rows="5" placeholder="Ingrese motivo por el cual no realizo el mantenimiento al equipo de comunicación..." Visible="false"></asp:TextBox>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <%--(FIN) Targeta Datos Tecnicos --%>

    <%--(INICIO) Targeta Pruebas --%>
    <div class="card">
        <div class="card-body">
            <h3 class="card-title">Pruebas de PC</h3>
            <asp:UpdatePanel runat="server" ID="UpNoProbaronEquipo" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="row p-t-20">
                        <div class="col-md-6">
                            <label class="control-label text-danger" runat="server" visible="false" id="lbProbaronEquipo">*</label><label class="control-label">¿Se probaron todos los equipos?</label>
                        </div>
                        <div class="col-md-6">
                            <asp:RadioButtonList ID="RBProbaronEquipo" RepeatDirection="Horizontal" Width="90px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RBProbaronEquipo_SelectedIndexChanged">
                                <asp:ListItem Value="1">Si</asp:ListItem>
                                <asp:ListItem Value="0">No</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <asp:TextBox ID="TxMotivoNoProbaronEquipo" Visible="false" class="form-control" runat="server" TextMode="MultiLine" Rows="5" placeholder="Ingrese motivo por el cual no probo todo el equipo..."></asp:TextBox>
                    </div> 
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <%--(FIN) Targeta Datos Tecnicos --%>
             
    <%--(INICIO) Targeta Equipo de Comunicacion --%>
    <div class="card">
        <div class="card-body">
            <h3 class="card-title">Equipo de Comunicación</h3>

            <%-- Pregunta 1 --%>
            <asp:UpdatePanel runat="server" ID="UpClimatizacion" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="row p-t-20">
                        <div class="col-md-6">
                            <label class="control-label text-danger" runat="server" visible="false" id="lbClimatizacion">*</label><label class="control-label">¿El equipo de comunicación cuenta con una climatización adecuada?</label>
                            (Aire Acondicionado)
                        </div>
                        <div class="col-md-2">
                            <asp:RadioButtonList ID="RblClimatizacionAdecuada" RepeatDirection="Horizontal" Width="90px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RblClimatizacionAdecuada_SelectedIndexChanged">
                                <asp:ListItem Value="1">Si</asp:ListItem>
                                <asp:ListItem Value="0">No</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>

                        <div class="col-md-4 ">
                            <asp:FileUpload ID="FuClimatizacion" runat="server" Visible="false" onchange="showpreview1(this);" class="form-control" />
                            <img id="ImgPreviewClimatizacion" height="250" width="460" src="../../assets/images/vistaPrevia1.JPG" style="border-width: 0px;" runat="server" visible="false" />                          
                        </div>

                    </div>
                    
                </ContentTemplate>
            </asp:UpdatePanel>

            <%-- Pregunta 2 --%>
            <asp:UpdatePanel runat="server" ID="UpUPS" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="row p-t-20">
                        <div class="col-md-6">
                            <label class="control-label text-danger" runat="server" visible="false" id="lbUps">*</label><label class="control-label">¿El equipo de comunicación cuenta con protección de energía eléctrica (UPS)?</label>
                        </div>
                        <div class="col-md-2">
                            <asp:RadioButtonList ID="RblUPS" RepeatDirection="Horizontal" Width="90px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RblUPS_SelectedIndexChanged">
                                <asp:ListItem Value="1">Si</asp:ListItem>
                                <asp:ListItem Value="0">No</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>

                        <div class="col-md-4 ">
                            <asp:FileUpload ID="FuUPS" runat="server" Visible="false" onchange="showpreview2(this); " class="form-control" />
                            <img id="ImgPreviewUPS" height="250" width="460" src="../../assets/images/vistaPrevia1.JPG" style="border-width: 0px;" runat="server" visible="false" />                           
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </div>
    <%--(FIN) Targeta Equipo de Comunicacion --%>

    <%--(INICIO) Targeta Entorno Cuarto de Comunicaciones --%>
    <div class="card">
        <div class="card-body">           
             <h3 class="card-title"> Entorno Cuarto de Comunicaciones</h3>
         
            <%-- Pregunta 1 --%>
            <asp:UpdatePanel runat="server" ID="UpPolvoSuciedad" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="row p-t-20">
                        <div class="col-md-6">
                            <label class="control-label text-danger" runat="server" visible="false" id="lbPolvo">*</label><label class="control-label">¿El equipo de comunicaciones esta expuesto a polvo o suciedad?</label>
                        </div>
                        <div class="col-md-2">
                            <asp:RadioButtonList ID="RbPolvoSuciedad" RepeatDirection="Horizontal" Width="90px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RbPolvoSuciedad_SelectedIndexChanged">
                                <asp:ListItem Value="1">Si</asp:ListItem>
                                <asp:ListItem Value="0">No</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>

                        <div class="col-md-4 ">
                            <asp:FileUpload ID="FuPolvoSuciedad" runat="server" Visible="false" onchange="showpreview4(this);" class="form-control" />
                            <img id="ImgPreviewPolvoSuciedad" height="250" width="460" src="../../assets/images/vistaPrevia1.JPG" style="border-width: 0px;" runat="server" visible="false" />
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            
            <%-- Pregunta 2  --%>
            <asp:UpdatePanel runat="server" ID="UpHumedadSustancias" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="row p-t-20">
                        <div class="col-md-6">
                            <label class="control-label text-danger" runat="server" visible="false" id="lbHumedad">*</label><label class="control-label">¿Observan rastros de humedad u otras substancias en las paredes o piso?</label>
                        </div>
                        <div class="col-md-2">
                            <asp:RadioButtonList ID="RblHumedadSustancias" RepeatDirection="Horizontal" Width="90px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RblHumedadSustancias_SelectedIndexChanged">
                                <asp:ListItem Value="1">Si</asp:ListItem>
                                <asp:ListItem Value="0">No</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="col-md-4 ">
                            <asp:FileUpload ID="FuHumedadSustancias" runat="server" Visible="false" onchange="showpreview5(this);" class="form-control" />
                            <img id="ImgPreviewHumedadSustancias" height="250" width="460" src="../../assets/images/vistaPrevia1.JPG" style="border-width: 0px;" runat="server" visible="false" />                            
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
           
            <%-- Pregunta 3  --%>
            <asp:UpdatePanel runat="server" ID="UpRoboDaño" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="row p-t-20">
                        <div class="col-md-6">
                            <label class="control-label text-danger" runat="server" visible="false" id="lbRobo">*</label><label class="control-label">¿El equipo de comunicaciones esta expuesto a robos o daño?</label>
                        </div>
                        <div class="col-md-2">
                            <asp:RadioButtonList ID="RblRoboDaño" RepeatDirection="Horizontal" Width="90px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RblRoboDaño_SelectedIndexChanged">
                                <asp:ListItem Value="1">Si</asp:ListItem>
                                <asp:ListItem Value="0">No</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="col-md-4 ">
                            <asp:FileUpload ID="FuRoboDaño" runat="server" Visible="false" onchange="showpreview6(this);" class="form-control" />
                            <img id="ImgPreviewRoboDaño" height="250" width="460" src="../../assets/images/vistaPrevia1.JPG" style="border-width: 0px;" runat="server" visible="false" />                           
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <%-- Pregunta 4  --%>
            <asp:UpdatePanel runat="server" ID="UpElementosAjenos" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="row p-t-20">
                        <div class="col-md-6">
                            <label class="control-label text-danger" runat="server" visible="false" id="lbElementos">*</label><label class="control-label">¿Encontro elementos ajenos en el cuarto de comunicaciones?</label>
                            (Ejemplo: sillas, papeles, basura, electrodomesticos, etc)
                        </div>
                        <div class="col-md-2">
                            <asp:RadioButtonList ID="RblElementosAjenos" RepeatDirection="Horizontal" Width="90px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RblElementosAjenos_SelectedIndexChanged">
                                <asp:ListItem Value="1">Si</asp:ListItem>
                                <asp:ListItem Value="0">No</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="col-md-4">
                            <asp:FileUpload ID="FuElementosAjenos" runat="server" Visible="false" onchange="showpreview7(this);" class="form-control" />
                            <img id="ImgPreviewElementosAjenos" height="250" width="460" src="../../assets/images/vistaPrevia1.JPG" style="border-width: 0px;" runat="server" visible="false" />                          
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
                       
        </div>
    </div>
    <%--(FIN) Targeta Entorno Cuarto de Comunicaciones --%>

    <%--(INICIO) Targeta Imagenes Obligatorias--%>
    <div class="card">
        <div class="card-body">
            <h3 class="card-title">Entorno y Rack de Comunicaciones</h3>          
            <%-- Pregunta 1 --%>
            <div class="row p-t-20">
                <div class="col-md-8">
                    <label class="control-label text-danger" runat="server" visible="false" id="lbRack">*</label><label class="control-label">Subir Imagen Rack Comunicaciones</label>
                </div>
                <div class="col-md-4" style="vertical-align: middle; text-align: right">
                    <asp:FileUpload ID="FuRack" runat="server" Visible="true" onchange="showpreview3(this);" class="form-control" />
                    <img id="ImgPreviewRack" height="250" width="460" src="../../assets/images/vistaPrevia1.JPG" style="border-width: 0px;" runat="server" visible="true" />
                </div>
            </div>

            <%-- Pregunta 2 --%>
            <div class="row p-t-20">
                <div class="col-md-8">
                    <label class="control-label text-danger" runat="server" visible="false" id="lbEntorno">*</label><label class="control-label">Foto del espacio fisico en donde se encuentra el equipo de comunicaciones (Entorno)</label>
                </div>
                <div class="col-md-4" style="vertical-align: middle; text-align: right">
                    <asp:FileUpload ID="FuEspacioFisico" runat="server" Visible="true" onchange="showpreview8(this);" class="form-control" />
                    <img id="ImgPreviewEspacioFisico" height="250" width="460" src="../../assets/images/vistaPrevia1.JPG" style="border-width: 0px;" runat="server" visible="true" />
                </div>
            </div>
        </div>
    </div>
    <%--(FIN) Targeta Imagenes Obligatorias--%>

    <%--(INICIO) Targeta Imagenes Observaciones--%>
    <div class="card">
        <div class="card-body">
            <h3 class="card-title">Observaciones Generales</h3>
            <div class="form-group row" runat="server">
                <div class="row p-t-20 col-12">
                    <label class="control-label col-md-2">Observaciones:</label>
                    <div class="col-md-10">
                        <asp:TextBox ID="TxObservacionesGenerales" class="form-control" runat="server" TextMode="MultiLine" Rows="5"></asp:TextBox>
                    </div>
                </div>
            </div>

            <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="row p-t-20 col-md-12">
                        <div class="col-md-4">
                                <asp:Button ID="BtnEnviarLv" class="btn btn-block  btn-success" runat="server" Text="Enviar LV " OnClick="BtnEnviarLv_Click" />
                        </div>
                        <div class="col-md-4">
                            <asp:Button ID="BtnRegresarCompletarLV" class="btn btn-block  btn-danger" runat="server" Text="Cancelar" OnClick="BtnRegresarCompletarLV_Click" />
                        </div>

                        <div class="col-md-4" id="ocultarBotonVolver1" runat="server" visible="false">                  
                            <a href="../../default.aspx"" class="btn  btn-block btn-primary">Volver</a>
                        </div>
                    </div>


                                      

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <%--(FIN) Targeta Imagenes Observaciones--%>


     <%--(INICIO) Aprobacion--%>
    <div class="card" runat="server" id="DivAprobacion" visible="false">
        <div class="card-body">
            <h3 class="card-title">Aprobación</h3>
            <asp:UpdatePanel runat="server" ID="UpdatePanel9" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="row p-t-20">
                        <div class="col-md-2">
                            <label class="control-label text-danger">*</label><label class="control-label">¿Desea Aprobar LV?</label>
                        </div>
                        <div class="col-md-2">
                            <asp:RadioButtonList ID="RblAprobarLV" RepeatDirection="Horizontal" Width="90px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RblAprobarLV_SelectedIndexChanged">
                                <asp:ListItem Value="1">Si</asp:ListItem>
                                <asp:ListItem Value="0">No</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>

                        <div class="col-md-1" id="DivEtiqueta" runat="server" visible="false">
                            <label class="control-label text-danger">*</label><label class="control-label">Motivo:</label>
                        </div>

                        <div class="col-md-7" id="DivTexto" runat="server" visible="false">
                            <asp:TextBox ID="TxMotivoCancelacionLV" class="form-control" runat="server" TextMode="MultiLine" Rows="5"></asp:TextBox>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>

            <br><br><br>
            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                <ContentTemplate>
                    <div class="row p-t-20 col-md-12">
                        <div class="col-md-4">
                             <asp:Button ID="BtnEnviarAprobacion" class="btn btn-block btn-success" runat="server" Text="Enviar"  OnClick="BtnEnviarAprobacion_Click"/>
                        </div>              
                  
                        <div class="col-md-4">
                         <asp:Button ID="BtnRegresarPendienteAprobar" class="btn btn-block   btn-danger " runat="server" Text="Cancelar"  OnClick="BtnRegresarPendienteAprobar_Click" />   
                        </div>

                        <div class="col-md-4" id="ocultarBotonVolver" runat="server" >                  
                            <a href="../../default.aspx"" class="btn  btn-block btn-primary">Volver</a>
                        </div>
                    </div>

                   
                    <br />
                    <br />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
     <%--(FIN) Respuesta--%>

    <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
<%--            <asp:TextBox ID="TxResNoManEquipoComu" runat="server"  ></asp:TextBox>
            <asp:TextBox ID="TxClimatizacion" runat="server" ></asp:TextBox>
            <asp:TextBox ID="TxUPS" runat="server" ></asp:TextBox>
            <asp:TextBox ID="TxRack" runat="server" ></asp:TextBox>
            <asp:TextBox ID="TxPolvoSuciedad" runat="server" ></asp:TextBox>
            <asp:TextBox ID="TxHumedadSustancias" runat="server" ></asp:TextBox>
            <asp:TextBox ID="TxRoboDaño" runat="server" ></asp:TextBox>
            <asp:TextBox ID="TxElementosAjenos" runat="server" ></asp:TextBox>
            <asp:TextBox ID="TxEspacioFisico" runat="server" ></asp:TextBox>--%>

            <asp:HiddenField ID="TxResNoManEquipoComu1" runat="server" />
            <asp:HiddenField ID="TxClimatizacion1" runat="server" />
             <asp:HiddenField ID="TxUPS1" runat="server" />
            <asp:HiddenField ID="TxRack1" runat="server" />
            <asp:HiddenField ID="TxPolvoSuciedad1" runat="server" />
            <asp:HiddenField ID="TxHumedadSustancias1" runat="server" />
            <asp:HiddenField ID="TxRoboDaño1" runat="server" />
            <asp:HiddenField ID="TxElementosAjenos1" runat="server" />
            <asp:HiddenField ID="TxEspacioFisico1" runat="server" />



        </ContentTemplate>
    </asp:UpdatePanel>

    <%--MODALES--%>
    <%--INICIO MODAL APROBAR LV--%>
    <div class="modal fade" id="modalAprobarLv" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="width: 600px; top: 320px; left: 50%; transform: translate(-50%, -50%);">
                <div class="modal-header bg-dark">
                    <asp:UpdatePanel ID="UpTituloAprobar" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <h3 class="modal-title" style="color: white" id="exampleModalLabel">
                                <asp:Label ID="Titulo" runat="server" Text="" Style="margin-left: auto; margin-right: auto"></asp:Label>
                                <asp:Label ID="Lugar" runat="server" Text="" Style="margin-left: auto; margin-right: auto"></asp:Label></h3>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <button type="button" class="close" style="color: white" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>

                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>

                            <div class="col-md-12" runat="server">
                                <div class="form-group row">
                                    <label class="col-md-3 col-form-label">Id Mantenimiento:</label>
                                    <div class="col-md-9">
                                        <asp:TextBox ID="TxIdMantenimiento" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12" runat="server">
                                <div class="form-group row">
                                    <label class="col-md-3 col-form-label">Fecha:</label>
                                    <div class="col-md-9">
                                        <asp:TextBox ID="TxFechaModal" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12" runat="server">
                                <div class="form-group row">
                                    <label class="col-md-3 col-form-label">Area:</label>
                                    <div class="col-md-9">
                                        <asp:TextBox ID="TxAreaModal" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>

                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12" runat="server">
                                <div class="form-group row">
                                    <label class="col-md-3 col-form-label">Responsable:</label>
                                    <div class="col-md-9">
                                        <asp:TextBox ID="TxResponsableModal" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12" runat="server" id="DivMotivo" visible="false">
                                <div class="form-group row">
                                    <label class="col-md-3 col-form-label">Motivo cancelación:</label>
                                    <div class="col-md-9">
                                        <asp:TextBox ID="TxMotivoRegreso" class="form-control" runat="server" ReadOnly="true" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12" style="margin-left: auto; margin-right: auto" id="DivAprobarLV" runat="server" visible="false">
                                <div class="alert alert-success  alert-dismissible align-content-md-center" style="align-self: auto">
                                    <div class="row">
                                        <div class="col-3">
                                            <p class="text-center"><img src="https://img.icons8.com/color/70/000000/accept-database.png"/><span class="dashicons dashicons-admin-home"></span></i></p>
                                        </div>
                                        <div class="col-9" style="text-align: center">

                                            <h4><strong>¿Está seguro que desea Aprobar la LV?
                                                <asp:Label ID="Label2" runat="server" Text="" Style="margin-left: auto; margin-right: auto"></asp:Label>
                                            </strong></h4>
                                            <p>Si esta seguro dar clic en el botón "Enviar"</p>
                                        </div>
                                        <br>
                                        <p style="text-align: justify">Todos los involucrados recibiran un correo con un adjunto de la lista de verificación. </p>
                                        <asp:Label ID="Label1" runat="server" Text="" Width="100%"></asp:Label>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12" style="margin-left: auto; margin-right: auto" id="DivRegresarLV" runat="server" visible="false">
                                <div class="alert alert-success  alert-dismissible align-content-md-center" style="align-self: auto">
                                    <div class="row">
                                        <div class="col-3">
                                            <p class="text-center"><img src="https://img.icons8.com/color/70/000000/accept-database.png"/><span class="dashicons dashicons-admin-home"></span></i></p>
                                        </div>
                                        <div class="col-9" style="text-align: center">

                                            <h4><strong>¿Está seguro que desea Regresa la LV?
                                        <asp:Label ID="Label3" runat="server" Text="" Style="margin-left: auto; margin-right: auto"></asp:Label>
                                            </strong></h4>
                                            <p>Si esta seguro dar clic en el botón "Enviar"</p>
                                        </div>
                                        <br>
                                        <p style="text-align: justify">El técnico responsable recibira un correo para que proceda a realizar las correcciones que se le indico.</p>
                                        <asp:Label ID="Label4" runat="server" Text="" Width="100%"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <button type="button" class="btn btn-light"
                                data-dismiss="modal">
                                Close</button>
                            <asp:Button ID="btnModalAprobarLV" runat="server" Text="Enviar" class="btn btn-dark" OnClick="btnModalAprobarLV_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>


    </div>
    <%--FIN MODAL APROBAR LV--%>
   
    <%--INICIO MODAL ENVIAR LV--%>
    <div class="modal fade" id="modalEnviarLv" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="width: 600px; top: 320px; left: 50%; transform: translate(-50%, -50%);">
                <div class="modal-header">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <h3 class="modal-title"  >
                                <asp:Label ID="TituloModalEnviarLV" runat="server" Text="" Style="margin-left: auto; margin-right: auto"></asp:Label></h3>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <button type="button" class="close"  data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>

                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                        <ContentTemplate>

                            <div class="col-md-12" runat="server">
                                <div class="form-group row">
                                    <label class="col-md-3 col-form-label">Id Mantenimiento:</label>
                                    <div class="col-md-9">
                                        <asp:TextBox ID="TxIdMantenimientoModalEnviarLV" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12" runat="server">
                                <div class="form-group row">
                                    <label class="col-md-3 col-form-label">Fecha:</label>
                                    <div class="col-md-9">
                                        <asp:TextBox ID="TxFechaModalEnviarLV" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12" runat="server">
                                <div class="form-group row">
                                    <label class="col-md-3 col-form-label">Area:</label>
                                    <div class="col-md-9">
                                        <asp:TextBox ID="TxAreaModalEnviarLV" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12" runat="server">
                                <div class="form-group row">
                                    <label class="col-md-3 col-form-label">Lugar:</label>
                                    <div class="col-md-9">
                                        <asp:TextBox ID="TxLugarModalEnviarLV" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12" runat="server">
                                <div class="form-group row">
                                    <label class="col-md-3 col-form-label">Responsable:</label>
                                    <div class="col-md-9">
                                        <asp:TextBox ID="TxResponsableModalEnviarLV" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

<%--                    <div class="col-md-12" style="margin-left: auto; margin-right: auto" id="Div3" runat="server">
                        <div class="alert alert-success  alert-dismissible align-content-md-center" style="align-self: auto">
                            <div class="row">
                                <div class="col-3">
                                    <p class="text-center"><img src="https://img.icons8.com/color/70/000000/accept-database.png"/><span class="dashicons dashicons-admin-home"></span></i></p>
                                </div>
                                <div class="col-9" style="text-align: center">
                                    <br>
                                    <h4><strong>¿Está seguro que desea enviar la LV?</strong></h4>
                                    <p>Si esta seguro dar clic en el botón "Enviar LV"</p>
                                </div>
                                <br>
                                <p style="text-align: justify">El jefe o suplente recibirá un correo para que proceda aprobar o rechazar la lista de verificación. </p>
                                <asp:Label ID="Label6" runat="server" Text="" Width="100%"></asp:Label>
                            </div>
                        </div>
                    </div>--%>

                </div>

                <div class="modal-footer">
                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                            <ContentTemplate>
                                <button type="button" class="btn btn-light" data-dismiss="modal">
                                    Close</button>                            
                                <asp:Button ID="btnModalEnviarLv" runat="server" Text="Enviar LV" class="btn btn-da" OnClick="btnModalEnviarLv_Click" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnModalEnviarLv" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>              
            </div>
        </div>
    </div>
    <%--FIN MODAL ENVIAR LV--%>

   

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
