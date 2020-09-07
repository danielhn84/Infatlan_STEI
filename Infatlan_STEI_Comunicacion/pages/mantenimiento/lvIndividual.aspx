<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="lvIndividual.aspx.cs" Inherits="Infatlan_STEI_Comunicacion.pages.mantenimiento.lvIndividual" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <!-- Custom CSS -->
    <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
    <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>


    <script type="text/javascript">
        var updateProgress = null;
        function postbackButtonClick() {
            updateProgress = $find("<%= UpdateProgress1.ClientID %>");
            window.setTimeout("updateProgress.set_visible(true)", updateProgress.get_displayAfter());
            return true;
        }
    </script>

    <script type="text/javascript">

        function abrirModal() { $('#ModalConfirmar').modal('show'); }
        function cerrarModal() { $('#ModalConfirmar').modal('hide'); }


        function showpreview1(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    var ruta1 = e.target.result;
                    document.getElementById('<%=ImgPreviewVersionRecomendada.ClientID%>').src = ruta1;
                    document.getElementById('<%=TxVersionRecomendada.ClientID%>').value = 'si';
                }
                reader.readAsDataURL(input.files[0]);
            }
        }

        function showpreview2(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    var ruta2 = e.target.result;
                    document.getElementById('<%=ImgPreviewEDC_ACS.ClientID%>').src = ruta2;
                    document.getElementById('<%=TxEDC_ACS.ClientID%>').value = 'si';
                }
                reader.readAsDataURL(input.files[0]);
            }
        }

        function showpreview3(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    var ruta3 = e.target.result;
                    document.getElementById('<%=ImgPreviewTablaARP.ClientID%>').src = ruta3;
                    document.getElementById('<%=TxTablaARP.ClientID%>').value = 'si';
                }
                reader.readAsDataURL(input.files[0]);
            }
        }

        function showpreview4(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    var ruta4 = e.target.result;
                    document.getElementById('<%=ImgPreviewTablaMAC.ClientID%>').src = ruta4;
                    document.getElementById('<%=TxTablaMAC.ClientID%>').value = 'si';
                }
                reader.readAsDataURL(input.files[0]);
            }
        }

        function showpreview5(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    var ruta5 = e.target.result;
                    document.getElementById('<%=ImgPreviewVersionEquipo.ClientID%>').src = ruta5;
                    document.getElementById('<%=TxVersionEquipo.ClientID%>').value = 'si';
                }
                reader.readAsDataURL(input.files[0]);
            }
        }

        function showpreview6(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    var ruta6 = e.target.result;
                    document.getElementById('<%=ImgPreviewInterfaces.ClientID%>').src = ruta6;
                    document.getElementById('<%=TxInterfaces.ClientID%>').value = 'si';
                }
                reader.readAsDataURL(input.files[0]);
            }
        }

        function showpreview7(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    var ruta7 = e.target.result;
                    document.getElementById('<%=ImgPreviewDMVPN_Activos.ClientID%>').src = ruta7;
                    document.getElementById('<%=TxDMVPN_Activos.ClientID%>').value = 'si';
                }
                reader.readAsDataURL(input.files[0]);
            }
        }

        function showpreview8(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    var ruta8 = e.target.result;
                    document.getElementById('<%=ImgPreviewVerify.ClientID%>').src = ruta8;
                    document.getElementById('<%=TxVerify.ClientID%>').value = 'si';
                }
                reader.readAsDataURL(input.files[0]);
            }
        }

        function showpreview9(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    var ruta9 = e.target.result;
                    document.getElementById('<%=ImgPreviewVersionEquipoActualizacion.ClientID%>').src = ruta9;
                    document.getElementById('<%=TxVersionEquipoActualizacion.ClientID%>').value = 'si';
                }
                reader.readAsDataURL(input.files[0]);
            }
        }

        function showpreview10(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    var ruta10 = e.target.result;
                    document.getElementById('<%=ImgPreviewShowAuthentication.ClientID%>').src = ruta10;
                    document.getElementById('<%=TxShowAuthentication.ClientID%>').value = 'si';
                }
                reader.readAsDataURL(input.files[0]);
            }
        }



        function showpreview11(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    var ruta11 = e.target.result;
                    document.getElementById('<%=ImgPreviewEquipoAgregadoSolarwinds.ClientID%>').src = ruta11;
                    document.getElementById('<%=TxEquipoAgregadoSolarwinds.ClientID%>').value = 'si';
                }
                reader.readAsDataURL(input.files[0]);
            }
        }

        function showpreview12(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    var ruta12 = e.target.result;
                    document.getElementById('<%=ImgPreviewConfiGuardadaSolarwinds.ClientID%>').src = ruta12;
                    document.getElementById('<%=TxConfiGuardadaSolarwinds.ClientID%>').value = 'si';
                }
                reader.readAsDataURL(input.files[0]);
            }
        }

        function showpreview13(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    var ruta13 = e.target.result;
                    document.getElementById('<%=ImgPreviewAltaDisponibilidad.ClientID%>').src = ruta13;

                    var res = ruta13.split("/");
                    var res = res[0];

                    if (res == "data:image") {
                        $("#" + "<%=ImgPreviewAltaDisponibilidad.ClientID%>").show();                     
                    } else {
                        $("#" + "<%=ImgPreviewAltaDisponibilidad.ClientID%>").hide();                   
                    }
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
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Comunicación</a></li>
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Mantenimiento</a></li>
                    <li class="breadcrumb-item active">Lista de Verificación</li>
                </ol>
            </div>
        </div>
    </div>

    <!-- (INICIO)Tarjeta Datos Generales -->
    <div class="card">
        <div class="card-body">
            <div class="form-body col-12">
                <h3 class="card-title">Datos Generales</h3>
                <asp:UpdatePanel runat="server" ID="UPFormulario">
                    <ContentTemplate>
                        <!--Inicio Fila 1-->
                        <div class="row">
                            <div class="col-4">
                                <label class="control-label">Fecha Mantenimiento:</label>
                                <asp:TextBox ID="TxFechaMantenimiento" AutoPostBack="true" runat="server" ReadOnly="true" class="form-control"></asp:TextBox>
                            </div>

                            <div class="col-4">
                                <label class="control-label">Fecha Ultimo Mantenimiento:</label>
                                <asp:TextBox ID="TxUltimoMantenimiento" AutoPostBack="true" runat="server" ReadOnly="true" class="form-control"></asp:TextBox>
                            </div>

                            <div class="col-4">
                                <label class="control-label">Nodo:</label>
                                <asp:TextBox ID="TxNodo" AutoPostBack="true" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        <br>
                        <!--Fin Fila 1-->

                        <!--Inicio Fila 2-->
                        <div class="row">
                            <div class="col-4">
                                <label class="control-label">Número SysAid:</label></label>                                        
                                        <asp:TextBox ID="TxSysAid" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="col-4">
                                <label class="control-label">Id Control Cambio:</label></label>             
                                          <asp:TextBox ID="TxControlCambio" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="col-4">
                                <label class="control-label">Zona:</label>
                                <asp:TextBox ID="TxZona" class="form-control" runat="server" ReadOnly="true" Text=""></asp:TextBox>
                            </div>
                        </div>
                        <br>
                        <!--Fin Fila 2-->

                        <!--Inicio Fila 3-->
                        <div class="row">
                            <div class="col-4">
                                <label class="control-label">Ip:</label>
                                <asp:TextBox ID="TxIp" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="col-4">
                                <label class="control-label">IOS Image:</label>
                                <asp:TextBox ID="TxImagen" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="col-4">
                                <label class="control-label">IOS Version:</label>
                                <asp:TextBox ID="TXVersion" class="form-control" runat="server" ReadOnly="true" Text=""></asp:TextBox>
                            </div>
                        </div>
                        <br>
                        <!--Fin Fila 3-->

                        <!--Inicio Fila 6-->
                        <div class="row">
                            <div class="col-4">
                                <label class="control-label">Hora Inicio Mantenimiento:</label>
                                <asp:TextBox ID="TxHoraInicio" class="form-control" runat="server" TextMode="Time" Text="" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="col-4">
                                <label class="control-label">Hora Fin Mantenimiento:</label>
                                <asp:TextBox ID="TxHoraFin" class="form-control" runat="server" TextMode="Time" Text="" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="col-4">
                                <label class="control-label">Duración Mantenimiento:</label>
                                <asp:TextBox ID="TxDuracionMan" class="form-control" runat="server" TextMode="Time" Text="" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        <br>
                        <!--Fin Fila 6-->

                        <!--Inicio Fila 4-->
                        <div class="row">
                            <div class="col-4">
                                <label class="control-label">Tipo:</label>
                                <asp:TextBox ID="TxTipo" class="form-control" runat="server" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="col-4">
                                <label class="control-label">Dirección:</label>
                                <asp:TextBox ID="TxDirección" class="form-control" runat="server" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="col-4">
                                <label class="control-label">Motivo:</label>
                                <asp:TextBox ID="TxMotivo" class="form-control" runat="server" Text="Mantenimiento preventivo y actualización de IOS" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        <br>
                        <!--Fin Fila 4-->

                        <!--Inicio Fila 8-->
                        <div class="row">
                            <div class="col-12">
                                <label class="control-label">Impacto:</label>
                                <asp:TextBox ID="TxImpacto" class="form-control" ReadOnly="true" runat="server" TextMode="MultiLine" Rows="3"></asp:TextBox>
                            </div>
                        </div>
                        <br>
                        <!--Fin Fila 8-->
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <!-- (FIN)Tarjeta Datos Generales -->


    <!-- (INICIO)Tarjeta Datos Generales -->
    <div class="card">
        <div class="card-body">
            <div class="form-body col-12">
                <h3 class="card-title">Datos Generales</h3>
                <%--<div class="card-body">--%>
                <h4 class="card-title">Antes del mantenimiento</h4>
                <hr>
                <%--<h6 class="card-subtitle" style="color: red;">Campos con * son obligatorios</h6>--%>
                <table class="tablesaw table-bordered table-hover table no-wrap" data-tablesaw-mode="swipe"
                    data-tablesaw-sortable data-tablesaw-sortable-switch data-tablesaw-minimap
                    data-tablesaw-mode-switch>
                    <thead>
                        <tr>
                            <th scope="col" style="background-color: #5D6D7E; color: #D5DBDB;" data-tablesaw-sortable-col data-tablesaw-priority="persist" class="border">Imagen a subir</th>
                            <th scope="col" style="background-color: #5D6D7E; color: #D5DBDB;" data-tablesaw-sortable-col data-tablesaw-priority="2" class="border">Seleccione imagen </th>
                        </tr>
                    </thead>
                    <tbody>
                        <div class="row" runat="server" visible="false" id="RowVersionRecomendada">
                            <tr>
                                <td><label class="control-label text-danger">*</label>Evidencia de que versión instaslada es la recomendada
                                            <br>
                                    por el fabricante al momento de la instalación                                           
                                </td>
                                <td>
                                    <div class="row p-t-20">
                                        <div class="col-md-1" style="text-align: center">
                                        </div>
                                        <div class="col-md-10" style="text-align: center">
                                            <asp:FileUpload ID="FuVersionRecomendada" runat="server" Visible="true" onchange="showpreview1(this);" class="form-control" /><br>
                                            <img id="ImgPreviewVersionRecomendada" height="300" width="530" src="../../assets/images/vistaPrevia1.jpg"  style="border-width: 0px;" runat="server" visible="true" />
                                        </div>
                                        <div class="col-md-1" style="text-align: center">
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </div>

                        <div class="row" runat="server" visible="false" id="RowEDC_ACS">
                            <tr>
                                <td><label class="control-label text-danger">*</label>Validar equipo de comunicación adicionado a ACS                                         
                                </td>
                                <td>
                                    <div class="row p-t-20">
                                        <div class="col-md-1" style="text-align: center">
                                        </div>
                                        <div class="col-md-10" style="text-align: center">
                                            <asp:FileUpload ID="FuEDC_ACS" runat="server" Visible="true" onchange="showpreview2(this);" class="form-control" /><br>
                                            <img id="ImgPreviewEDC_ACS" height="300" width="530" src="../../assets/images/vistaPrevia1.jpg"  style="border-width: 0px;" runat="server" visible="true" />
                                        </div>
                                        <div class="col-md-1" style="text-align: center">
                                        </div>
                                    </div>

                                </td>
                            </tr>
                        </div>

                        <div class="row" runat="server" visible="false" id="RowTablaARP">
                            <tr>
                                <td><label class="control-label text-danger">*</label>Información capturada de la tabla ARP                                                                                    
                                </td>
                                <td>
                                    <div class="row p-t-20">
                                        <div class="col-md-1" style="text-align: center">
                                        </div>
                                        <div class="col-md-10" style="text-align: center">
                                            <asp:FileUpload ID="FuTablaARP" runat="server" Visible="true" onchange="showpreview3(this);" class="form-control" /><br>
                                            <img id="ImgPreviewTablaARP" height="300" width="530" src="../../assets/images/vistaPrevia1.jpg"  style="border-width: 0px;" runat="server" visible="true" />
                                        </div>
                                        <div class="col-md-1" style="text-align: center">
                                        </div>
                                    </div>

                                </td>
                            </tr>
                        </div>

                        <div class="row" runat="server" visible="false" id="RowTablaMAC">
                            <tr>
                                <td><label class="control-label text-danger">*</label>Información capturada de la tabla MAC                                                                                
                                </td>
                                <td>
                                    <div class="row p-t-20">
                                        <div class="col-md-1" style="text-align: center">
                                        </div>
                                        <div class="col-md-10" style="text-align: center">
                                            <asp:FileUpload ID="FuTablaMAC" runat="server" Visible="true" onchange="showpreview4(this);" class="form-control" /><br>
                                            <img id="ImgPreviewTablaMAC" height="300" width="530" src="../../assets/images/vistaPrevia1.jpg"  style="border-width: 0px;" runat="server" visible="true" />
                                        </div>
                                        <div class="col-md-1" style="text-align: center">
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </div>

                        <div class="row" runat="server" visible="false" id="RowVersionEquipo">
                            <tr>
                                <td><label class="control-label text-danger">*</label>Imagen de versión del equipo </td>
                                <td>
                                    <div class="row p-t-20">
                                        <div class="col-md-1" style="text-align: center">
                                        </div>
                                        <div class="col-md-10" style="text-align: center">
                                            <asp:FileUpload ID="FuVersionEquipo" runat="server" Visible="true" onchange="showpreview5(this);" class="form-control" /><br>
                                            <img id="ImgPreviewVersionEquipo" height="300" width="530" src="../../assets/images/vistaPrevia1.jpg"  style="border-width: 0px;" runat="server" visible="true" />
                                        </div>
                                        <div class="col-md-1" style="text-align: center">
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </div>

                        <div class="row" runat="server" visible="false" id="RowInterfaces">
                            <tr>
                                <td><label class="control-label text-danger">*</label>Información capturada de las interfaces</td>
                                <td>
                                    <div class="row p-t-20">
                                        <div class="col-md-1" style="text-align: center">
                                        </div>
                                        <div class="col-md-10" style="text-align: center">
                                            <asp:FileUpload ID="FuInterfaces" runat="server" Visible="true" onchange="showpreview6(this);" class="form-control" /><br>
                                            <img id="ImgPreviewInterfaces" height="300" width="530" src="../../assets/images/vistaPrevia1.jpg"  style="border-width: 0px;" runat="server" visible="true" />
                                        </div>
                                        <div class="col-md-1" style="text-align: center">
                                        </div>
                                    </div>

                                </td>
                            </tr>
                        </div>

                        <div class="row" runat="server" visible="false" id="RowDMVPN_Activos">
                            <tr>
                                <td><label class="control-label text-danger">*</label>Imagen de los túneles DMVPN activos                                                                                  
                                </td>
                                <td>
                                    <div class="row p-t-20">
                                        <div class="col-md-1" style="text-align: center">
                                        </div>
                                        <div class="col-md-10" style="text-align: center">
                                            <asp:FileUpload ID="FuDMVPN_Activos" runat="server" Visible="true" onchange="showpreview7(this);" class="form-control" /><br>
                                            <img id="ImgPreviewDMVPN_Activos" height="300" width="530" src="../../assets/images/vistaPrevia1.jpg"  style="border-width: 0px;" runat="server" visible="true" />
                                        </div>
                                        <div class="col-md-1" style="text-align: center">
                                        </div>
                                    </div>

                                </td>
                            </tr>
                        </div>

                        <div class="row" runat="server" visible="false" id="RowVerify">
                            <tr>
                                <td><label class="control-label text-danger">*</label>Imagen de verify de sistema operativo previo
                                                <br>
                                    a la instalación                                                                                 
                                </td>
                                <td>
                                    <div class="row p-t-20">
                                        <div class="col-md-1" style="text-align: center">
                                        </div>
                                        <div class="col-md-10" style="text-align: center">
                                            <asp:FileUpload ID="FuVerify" runat="server" Visible="true" onchange="showpreview8(this);" class="form-control" /><br>
                                            <img id="ImgPreviewVerify" height="300" width="530" src="../../assets/images/vistaPrevia1.jpg"  style="border-width: 0px;" runat="server" visible="true" />
                                        </div>
                                        <div class="col-md-1" style="text-align: center">
                                        </div>
                                    </div>

                                </td>
                            </tr>
                        </div>
                    </tbody>
                </table>

                <br>
                <h4 class="card-title">Despúes de la Actualización</h4>
                <hr>
                <table class="tablesaw table-bordered table-hover table no-wrap" data-tablesaw-mode="swipe"
                    data-tablesaw-sortable data-tablesaw-sortable-switch data-tablesaw-minimap
                    data-tablesaw-mode-switch>
                    <thead>
                        <tr>
                            <th scope="col" style="background-color: #5D6D7E; color: #D5DBDB;" data-tablesaw-sortable-col data-tablesaw-priority="persist" class="border">Imagen a subir</th>
                            <th scope="col" style="background-color: #5D6D7E; color: #D5DBDB;" data-tablesaw-sortable-col data-tablesaw-priority="2" class="border">Seleccione imagen </th>
                        </tr>
                    </thead>
                    <tbody>
                        <div class="row" runat="server" visible="false" id="RowVersionEquipoActualizacion">
                            <tr>
                                <td><label class="control-label text-danger">*</label>Imagen de versión del equipo</td>
                                <td>
                                    <div class="row p-t-20">
                                        <div class="col-md-1" style="text-align: center">
                                        </div>
                                        <div class="col-md-10" style="text-align: center">
                                            <asp:FileUpload ID="FuVersionEquipoActualizacion" runat="server" Visible="true" onchange="showpreview9(this);" class="form-control" /><br>
                                            <img id="ImgPreviewVersionEquipoActualizacion" height="300" width="530" src="../../assets/images/vistaPrevia1.jpg"  style="border-width: 0px;" runat="server" visible="true" />
                                        </div>
                                        <div class="col-md-1" style="text-align: center">
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </div>

                        <div class="row" runat="server" visible="false" id="RowShowAuthentication">
                            <tr>
                                <td><label class="control-label text-danger">*</label>Ejecutar comando “show authentication sessions”
                                                <br>
                                    en equipo de comunicación </td>
                                <td>
                                    <div class="row p-t-20">
                                        <div class="col-md-1" style="text-align: center">
                                        </div>
                                        <div class="col-md-10" style="text-align: center">
                                            <asp:FileUpload ID="FuShowAuthentication" runat="server" Visible="true" onchange="showpreview10(this);" class="form-control" /><br>
                                            <img id="ImgPreviewShowAuthentication" height="300" width="530" src="../../assets/images/vistaPrevia1.jpg"  style="border-width: 0px;" runat="server" visible="true" />
                                        </div>
                                        <div class="col-md-1" style="text-align: center">
                                        </div>
                                    </div>

                                </td>
                            </tr>
                        </div>

                        <div class="row" runat="server" visible="false" id="RowEquipoAgregadoSolarwinds">
                            <tr>
                                <td><label class="control-label text-danger">*</label>Imagen equipo agregado a Solarwinds</td>
                                <td>
                                    <div class="row p-t-20">
                                        <div class="col-md-1" style="text-align: center">
                                        </div>
                                        <div class="col-md-10" style="text-align: center">
                                            <asp:FileUpload ID="FuEquipoAgregadoSolarwinds" runat="server" Visible="true" onchange="showpreview11(this);" class="form-control" /><br>
                                            <img id="ImgPreviewEquipoAgregadoSolarwinds" height="300" width="530" src="../../assets/images/vistaPrevia1.jpg"  style="border-width: 0px;" runat="server" visible="true" />
                                        </div>
                                        <div class="col-md-1" style="text-align: center">
                                        </div>
                                    </div>

                                </td>
                            </tr>
                        </div>

                        <div class="row" runat="server" visible="false" id="RowConfiGuardadaSolarwinds">
                            <tr>
                                <td><label class="control-label text-danger">*</label>Imagen de configuración guardada en Solarwinds
                                                <br>
                                    (startup-config/running-config) </td>
                                <td>
                                    <div class="row p-t-20">
                                        <div class="col-md-1" style="text-align: center">
                                        </div>
                                        <div class="col-md-10" style="text-align: center">
                                            <asp:FileUpload ID="FuConfiGuardadaSolarwinds" runat="server" Visible="true" onchange="showpreview12(this);" class="form-control" /><br>
                                            <img id="ImgPreviewConfiGuardadaSolarwinds" height="300" width="530" src="../../assets/images/vistaPrevia1.jpg"  style="border-width: 0px;" runat="server" visible="true" />
                                        </div>
                                        <div class="col-md-1" style="text-align: center">
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </div>

                        <div class="row" runat="server" visible="false" id="RowAltaDisponibilidad">
                            <tr>
                                <td><label class="control-label text-danger">*</label>Evidencia de prueba de alta disponibilidad
                                                <br>
                                    para equipo CORE</td>
                                <td>
                                    <div class="row p-t-20">
                                        <div class="col-md-1" style="text-align: center">
                                        </div>
                                        <div class="col-md-10" style="text-align: center">
                                            <asp:FileUpload ID="FuAltaDisponibilidad" runat="server" Visible="true" onchange="showpreview13(this);" class="form-control" /><br>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Always">
                                                <ContentTemplate>
                                                    <img id="ImgPreviewAltaDisponibilidad" height="300" width="530" src="../../assets/images/vistaPrevia1.jpg"  style="border-width: 0px;" runat="server" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-md-1" style="text-align: center">
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </div>

                        <div class="row" runat="server" visible="false" id="RowPregunta">
                            <tr>
                                <td><label class="control-label text-danger">*</label>He verficado que los scripts iniciales y finales concuerdan<br>
                                    y he realizado las pruebas necesarias para garantizar<br>
                                    el funcionamiento normal del equipo.                                                                                       
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="row p-t-20">
                                                <div class="col-md-1" style="text-align: center">
                                                </div>
                                                <div class="col-md-10" style="text-align: center">
                                                    <asp:RadioButtonList ID="RbVerificacionScrip" RepeatDirection="Horizontal" BorderStyle="Solid" BorderColor="Transparent" Width="90px" runat="server" AutoPostBack="True">
                                                        <asp:ListItem Value="1">Si</asp:ListItem>
                                                        <asp:ListItem Value="0">No</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                                <div class="col-md-1" style="text-align: center">
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                </td>
                            </tr>
                        </div>

                    </tbody>
                </table>
                <br>
                <div class="row">
                    <div class="col-6">
                        <label class="control-label text-danger">*</label><label class="control-label">IOS Versión:</label>
                        <asp:TextBox ID="TxNewIOSVersion" class="form-control" runat="server" ReadOnly="false"></asp:TextBox>
                    </div>
                    <div class="col-6">
                        <label class="control-label text-danger">*</label><label class="control-label">IOS Image:</label>
                        <asp:TextBox ID="TxNewIOSImagen" class="form-control" runat="server" ReadOnly="false"></asp:TextBox>
                    </div>
                </div>
                <br>

                <div class="row">
                    <div class="col-12">
                        <label class="control-label">Observaciones:</label>
                        <asp:TextBox ID="TxObservaciones" class="form-control" ReadOnly="false" runat="server" TextMode="MultiLine" Rows="3"></asp:TextBox>
                    </div>
                </div>
                <br>
                <br>
                <div class="col-md-12" style="text-align: center">
                    <label class="control-label text-danger" style="text-align: center">Los campos con (*) son obligatorios</label>
                </div>
            </div>
        </div>
    </div>
    <!-- (FIN)Tarjeta Datos Generales -->
 
    <!-- (INICIO)Tarjeta Envio LV -->
    <div class="card" runat="server" id="DivEnviarLV">
        <div class="card-body">
            <div class="form-body col-12">
                <h3 class="card-title">Envio LV</h3>
                <div class="col-12">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel7" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Button Text="Enviar" class="btn btn-success" ID="BtnEnviar" OnClick="BtnEnviar_Click" runat="server" />
                            <button type="button" class="btn btn-primary">Cancelar</button>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <!-- (Fin)Tarjeta Envio LV -->


    <!-- (INICIO)Tarjeta Aprobar LV -->
    <div class="card" runat="server" id="DivAprobarLV" visible="false">
        <div class="card-body">
            <div class="form-body col-12">
                <h3 class="card-title">Aprobar LV</h3>

                <asp:UpdatePanel runat="server" ID="UpdatePanel9" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-2">
                                <label class="control-label text-danger">*</label><label class="control-label">¿Desea Aprobar LV?</label>
                            </div>
                            <div class="col-2">
                                <asp:RadioButtonList ID="RblAprobarLV"  OnSelectedIndexChanged="RblAprobarLV_SelectedIndexChanged" RepeatDirection="Horizontal" Width="90px" runat="server" AutoPostBack="True" >
                                    <asp:ListItem Value="1">Si</asp:ListItem>
                                    <asp:ListItem Value="0">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>

                            <div class="col-1" id="DivEtiqueta" runat="server" visible="false">
                                <label class="control-label text-danger">*</label><label class="control-label">Motivo:</label>
                            </div>

                            <div class="col-7" id="DivTexto" runat="server" visible="false">
                                <asp:TextBox ID="TxMotivoCancelacionLV" class="form-control" runat="server" TextMode="MultiLine" Rows="5"></asp:TextBox>
                            </div>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>

                <br />
                <br />
                <div class="col-12">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Button Text="Enviar" class="btn btn-success" ID="BtnEnviarAprobacion"  OnClick="BtnEnviarAprobacion_Click" runat="server" />
                            <button type="button" class="btn btn-primary">Cancelar</button>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <!-- (Fin)Tarjeta Aprobar LV -->

    <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:HiddenField ID="TxVersionRecomendada" runat="server" />
            <asp:HiddenField ID="TxEDC_ACS" runat="server" />
            <asp:HiddenField ID="TxTablaARP" runat="server" />
            <asp:HiddenField ID="TxTablaMAC" runat="server" />
            <asp:HiddenField ID="TxVersionEquipo" runat="server" />
            <asp:HiddenField ID="TxDMVPN_Activos" runat="server" />
            <asp:HiddenField ID="TxInterfaces" runat="server" />
            <asp:HiddenField ID="TxVerify" runat="server" />
            <asp:HiddenField ID="TxVersionEquipoActualizacion" runat="server" />
            <asp:HiddenField ID="TxShowAuthentication" runat="server" />
            <asp:HiddenField ID="TxEquipoAgregadoSolarwinds" runat="server" />
            <asp:HiddenField ID="TxConfiGuardadaSolarwinds" runat="server" />
            <asp:HiddenField ID="TxAltaDisponibilidad" runat="server" />       
        </ContentTemplate>
    </asp:UpdatePanel>

    <%-- Inicio Modal  --%>
    <div class="modal fade" id="ModalConfirmar" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title" id="ModalTitulo">
                                <b>
                                    <asp:Label runat="server" ID="LbTitulo" CssClass="col-form-label"></asp:Label></b>
                            </h4>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="modal-body">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel6" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="col-12" style="text-align: center">
                                <br>
                                <h4><strong>
                                    <asp:Label runat="server" ID="LbCuerpo" CssClass="col-form-label"></asp:Label></strong></h4>
                                <p>Si esta seguro dar clic en el botón "Enviar"</p>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <br>

                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                            <asp:Button ID="BtnConfirmar" runat="server" Text="Enviar" class="btn btn-success" OnClick="BtnConfirmar_Click"/>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="BtnConfirmar" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
