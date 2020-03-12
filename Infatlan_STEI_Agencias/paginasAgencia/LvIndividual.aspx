<%@ Page Title="" Language="C#" MasterPageFile="~/mainAgencia.Master" AutoEventWireup="true" CodeBehind="lvIndividual.aspx.cs" Inherits="Infatlan_STEI_Agencias.paginasAgencia.LvIndividual" %>

<%@ Register Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>


<%--<%@ Page Title="" Language="C#" MasterPageFile="~/mainAgencia.Master" AutoEventWireup="true" CodeBehind="LvIndividual.aspx.cs" Inherits="Infatlan_STEI_Agencias.paginasAgencia.LvIndividual" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <title></title>
    <script src="http://code.jquery.com/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function showpreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#imgpreview').css('visibility', 'visible');
                    $('#imgpreview').attr('src', e.target.result);

                    $('#imgpreview1').css('visibility', 'visible');
                    $('#imgpreview1').attr('src', e.target.result);

                    $('#ImgPreviewUPS').css('visibility', 'visible');
                    $('#ImgPreviewUPS').attr('src', e.target.result);


                    $('#ImgPreviewRack').css('visibility', 'visible');
                    $('#ImgPreviewRack').attr('src', e.target.result);

                    $('#ImgPreviewPolvoSuciedad').css('visibility', 'visible');
                    $('#ImgPreviewPolvoSuciedad').attr('src', e.target.result);
                    

                }
                reader.readAsDataURL(input.files[0]);
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">

    <div class="row page-titles">
        <div class="col-md-5 align-self-center">
            <h2 class="text-themecolor">Completar Lista de Verificación</h2>
            <div class="mr-md-3 mr-xl-5">
                <%-- <h2>Creación de Notificación</h2>--%>
                <p class="mb-md-0">Soporte Técnico y Comunicaciones</p>
            </div>
        </div>
    </div>

    <%--(INICIO) Targeta Datos Generales --%>
    <div class="card">
        <div class="card-body">
            <h3 class="card-title">Datos Generales</h3>
            <hr>
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
                    <label class="control-label">Hora Salida de Infatlan:</label>
                    <asp:TextBox ID="TxHoraSalidaINFA" class="form-control" runat="server" TextMode="Time"></asp:TextBox>
                </div>

                <div class="col-md-6">
                    <label class="control-label">Hora Llegada Infatlan:</label>
                    <asp:TextBox ID="TxHoraLlegadaINFA" class="form-control" runat="server" TextMode="Time"></asp:TextBox>
                </div>
            </div>


            <div class="row p-t-20">
                <div class="col-md-6">
                    <label class="control-label">Hora Comienzo del Mantenimiento:</label>
                    <asp:TextBox ID="TxHoraInicioMant" class="form-control" runat="server" TextMode="Time"></asp:TextBox>
                </div>

                <div class="col-md-6">
                    <label class="control-label">Hora Finalización del Mantenimiento:</label>
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
            <hr>
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
            <h5 class="card-title">-Técnicos Participantes </h5>

            <div class="col-md-12 text-center">
                <b>
                    <asp:Label ID="LbNumeroVaciones" Visible="false" runat="server" Text="No hay tecnicos participantes asignados al mantenimiento"></asp:Label></b>
            </div>


            <div class="row col-12">
                <div class="col-12 grid-margin stretch-card">
                    <div class="table-responsive">
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
            <hr>

            <div class="row p-t-20">
                <div class="col-md-2">
                    <label class="control-label">Cant Maquinas:</label>
                </div>

                <div class="col-md-4">
                    <asp:TextBox ID="TxCantMaquinas" TextMode="Number" class="form-control" runat="server"></asp:TextBox>
                </div>

                <div class="col-md-2">
                    <label class="control-label">Cant Impresoras Financieras:</label>
                </div>

                <div class="col-md-4">
                    <asp:TextBox ID="TextBox11" TextMode="Number" class="form-control" runat="server"></asp:TextBox>
                </div>
            </div>

            <div class="row p-t-20">
                <div class="col-md-2">
                    <label class="control-label">Cant Escaner Fenix:</label>
                </div>

                <div class="col-md-4">
                    <asp:TextBox ID="TextBox2c" class="form-control" runat="server" TextMode="Number"></asp:TextBox>
                </div>

                <div class="col-md-2">
                    <label class="control-label">Cant Datacards:</label>
                </div>

                <div class="col-md-4">
                    <asp:TextBox ID="TextBox3W" TextMode="Number" class="form-control" runat="server"></asp:TextBox>
                </div>
            </div>

            <br>
            <br>
            <div class="col-md-12  text-center">
                <label class="control-label">-----------------------------------------------------------------------------</label>
            </div>

            <div class="row p-t-20">
                <div class="col-md-7  text-right">
                    <label class="control-label">¿Realizó mantenimiento al equipo de comunicación? </label>
                </div>

                <div class="col-md-5">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel1W">
                        <ContentTemplate>

                            <asp:RadioButtonList ID="RBLManEquipoComu" RepeatDirection="Horizontal" Width="90px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RBLManEquipoComu_SelectedIndexChanged1">
                                <asp:ListItem Value="Si">Si</asp:ListItem>
                                <asp:ListItem Value="No">No</asp:ListItem>
                            </asp:RadioButtonList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>


            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>

                    <div class="form-group row" runat="server" visible="false" id="DivMotivoMantEquipoComu">
                        <div class="row p-t-20 col-12">
                            <%-- <div class="col-md-2 col-12">--%>
                            <label class="control-label col-md-2">Motivo:</label>
                            <%-- </div>--%>

                            <div class="col-md-10">
                                <asp:TextBox ID="TextBox4" class="form-control" runat="server" TextMode="MultiLine" Rows="9"></asp:TextBox>
                            </div>
                        </div>
                    </div>


                    <div class="form-group row" runat="server" visible="false" id="DivImagenNoMantEquipoComu">
                        <div class="row p-t-20 col-12">
                            <div class="col-md-5">
                                <asp:FileUpload ID="fuimage" runat="server" onchange="showpreview(this);" />
                            </div>
                            <div class="col-md-6">
                                <img id="imgpreview" height="350" width="450" src="/assets/images/vistaPrevia.JPG" style="border-width: 0px;" />
                            </div>
                        </div>
                    </div>


                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </div>
    <%--(FIN) Targeta Datos Tecnicos --%>

    <%--(INICIO) Targeta Pruebas --%>
    <div class="card">
        <div class="card-body">
            <h3 class="card-title">Pruebas de PC</h3>
            <hr>
            <asp:UpdatePanel runat="server" ID="UpNoProbaronEquipo" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="row p-t-20">
                        <div class="col-md-3">
                            <label class="control-label">¿Se probaron todos los equipos?</label>
                        </div>
                        <div class="col-md-3">
                            <asp:RadioButtonList ID="RBProbaronEquipo" RepeatDirection="Horizontal" Width="90px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RBProbaronEquipo_SelectedIndexChanged">
                                <asp:ListItem Value="Si">Si</asp:ListItem>
                                <asp:ListItem Value="No">No</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>

                        <div class="col-md-2">
                            <label runat="server" visible="false" class="control-label" id="LbMotivoNoProbaronEquipo">Motivo:</label>
                        </div>

                        <div class="col-md-4">
                            <asp:TextBox ID="TxMotivoNoProbaronEquipo" Visible="false" class="form-control" runat="server" TextMode="MultiLine" Rows="3"></asp:TextBox>
                        </div>
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
            <hr>

            <asp:UpdatePanel runat="server" ID="UpClimatizacion" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="row p-t-20">
                        <div class="col-md-6">
                            <label class="control-label">¿El equipo de comunicación cuenta con una climatización adecuada(Aire Acondicionado)?</label>
                        </div>
                        <div class="col-md-2">
                            <asp:RadioButtonList ID="RblClimatizacionAdecuada" RepeatDirection="Horizontal" Width="90px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RblClimatizacionAdecuada_SelectedIndexChanged">
                                <asp:ListItem Value="Si">Si</asp:ListItem>
                                <asp:ListItem Value="No">No</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>

                        <div class="col-md-4 ">
                            <asp:FileUpload ID="FuClimatizacion" runat="server" Visible="false" onchange="showpreview(this);" />
                        </div>
                    </div>
                    <div class="form-group row" runat="server" visible="false" id="DivAireAcondicionado" >
                        <div class="col-md-4   align-self-center" style="margin-left:auto; margin-right:auto">
                                <img id="imgpreview1"   height="350" width="450" src="/assets/images/vistaPrevia.JPG" style="border-width: 0px;" /> 
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <%-- Pregunta 2 --%>
            <asp:UpdatePanel runat="server" ID="UpUPS" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="row p-t-20">
                        <div class="col-md-6">
                            <label class="control-label">¿El equipo de comunicación cuenta con protección de energía eléctrica (UPS)?</label>
                        </div>
                        <div class="col-md-2">
                            <asp:RadioButtonList ID="RblUPS" RepeatDirection="Horizontal" Width="90px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RblUPS_SelectedIndexChanged">
                                <asp:ListItem Value="Si">Si</asp:ListItem>
                                <asp:ListItem Value="No">No</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>

                        <div class="col-md-4 ">
                            <asp:FileUpload ID="FuUPS" runat="server" Visible="false" onchange="showpreview(this);" />
                        </div>
                    </div>
                    <div class="form-group row" runat="server" visible="false" id="DivUPS">
                        <div class="col-md-4   align-self-center" style="margin-left: auto; margin-right: auto">
                            <img id="ImgPreviewUPS" height="350" width="450" src="/assets/images/vistaPrevia.JPG" style="border-width: 0px;" />
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br>

         <%-- Pregunta 3 --%>
            <div class="row p-t-20">
                <div class="col-md-6 text-right">
                    <label class="control-label">Subir Imagen Rack Comunicaciones</label>
                </div>

                <div class="col-md-6 ">
                    <asp:FileUpload ID="FuRack" runat="server" Visible="true" onchange="showpreview(this);" />
                </div>
            </div>
            <br>
            <div class="col-md-4   align-self-center" style="margin-left: auto; margin-right: auto">
                <img id="ImgPreviewRack" height="350" width="450" src="/assets/images/vistaPrevia.JPG" style="border-width: 0px;" />
            </div>

        </div>
    </div>


    <%--(FIN) Targeta Equipo de Comunicacion --%>

    <%--(INICIO) Targeta Entorno Cuarto de Comunicaciones --%>
    <div class="card">
        <div class="card-body">
            <h3 class="card-title">Entorno Cuarto de Comunicaciones</h3>
            <hr>
            <%-- Pregunta 1 --%>
            <asp:UpdatePanel runat="server" ID="UpPolvoSuciedad" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="row p-t-20">
                        <div class="col-md-6">
                            <label class="control-label">¿El equipo de comunicaciones esta expuesto a polvo o suciedad?</label>
                        </div>
                        <div class="col-md-2">
                            <asp:RadioButtonList ID="RbPolvoSuciedad" RepeatDirection="Horizontal" Width="90px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RbPolvoSuciedad_SelectedIndexChanged">
                                <asp:ListItem Value="Si">Si</asp:ListItem>
                                <asp:ListItem Value="No">No</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>

                        <div class="col-md-4 ">
                            <asp:FileUpload ID="FuPolvoSuciedad" runat="server" Visible="false" onchange="showpreview(this);" />
                        </div>
                    </div>
                    <br>
                    <div class="form-group row" runat="server" visible="false" id="DivPolvoSuciedad">
                        <div class="col-md-4   align-self-center" style="margin-left: auto; margin-right: auto">
                            <img id="ImgPreviewPolvoSuciedad" height="350" width="450" src="/assets/images/vistaPrevia.JPG" style="border-width: 0px;" />
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <%-- Pregunta 2  --%>
            <asp:UpdatePanel runat="server" ID="UpHumedadSustancias" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="row p-t-20">
                        <div class="col-md-6">
                            <label class="control-label">¿Se observan rastros de humedad u otras substancias en las paredes o piso?</label>
                        </div>
                        <div class="col-md-2">
                            <asp:RadioButtonList ID="RblHumedadSustancias" RepeatDirection="Horizontal" Width="90px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RblHumedadSustancias_SelectedIndexChanged">
                                <asp:ListItem Value="Si">Si</asp:ListItem>
                                <asp:ListItem Value="No">No</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>

                        <div class="col-md-4 ">
                            <asp:FileUpload ID="FuHumedadSustancias" runat="server" Visible="false" onchange="showpreview(this);" />
                        </div>
                    </div>
                    <br>
                    <div class="form-group row" runat="server" visible="false" id="DivHumedadSustancias">
                        <div class="col-md-4   align-self-center" style="margin-left: auto; margin-right: auto">
                            <img id="ImgPreviewHumedadSustancias" height="350" width="450" src="/assets/images/vistaPrevia.JPG" style="border-width: 0px;" />
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <%-- Pregunta 3  --%>
            <asp:UpdatePanel runat="server" ID="UpRastrosSustancias" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="row p-t-20">
                        <div class="col-md-6">
                            <label class="control-label">¿Se observan rastros de humedad u otras substancias en las paredes o piso?</label>
                        </div>
                        <div class="col-md-2">
                            <asp:RadioButtonList ID="RadioButtonList1" RepeatDirection="Horizontal" Width="90px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RblHumedadSustancias_SelectedIndexChanged">
                                <asp:ListItem Value="Si">Si</asp:ListItem>
                                <asp:ListItem Value="No">No</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>

                        <div class="col-md-4 ">
                            <asp:FileUpload ID="FileUpload1" runat="server" Visible="false" onchange="showpreview(this);" />
                        </div>
                    </div>
                    <br>
                    <div class="form-group row" runat="server" visible="false" id="Div1">
                        <div class="col-md-4   align-self-center" style="margin-left: auto; margin-right: auto">
                            <img id="ImgPreviewHumedadSustancias" height="350" width="450" src="/assets/images/vistaPrevia.JPG" style="border-width: 0px;" />
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>





        </div>
    </div>
    <%--(FIN) Targeta Entorno Cuarto de Comunicaciones --%>




    <div class="row">
        <div class="col-lg-12">
            <div class="card">
                <%--<asp:PlaceHolder runat="server" ID="PHCampos"></asp:PlaceHolder>--%>

                <div class="card-body">
                    <h3 class="card-title">Datos Técnicos</h3>
                    <hr>
                    <div class="row col-12">
                        <div class="col-12 grid-margin stretch-card">
                            <div class="table-responsive">

                                <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:GridView ID="GVDatosTecnicos" runat="server"
                                            CssClass="table table-bordered"
                                            PagerStyle-CssClass="pgr"
                                            HeaderStyle-CssClass="table"
                                            RowStyle-CssClass="rows"
                                            AutoGenerateColumns="false"
                                            AllowPaging="true"
                                            GridLines="None" 
                                            PageSize="10">

                                            <Columns>
                                                <asp:BoundField DataField="idTipoControl" HeaderText="TipoControl" ItemStyle-Width="5%" Visible="true" />
                                                <asp:BoundField DataField="idPregunta" HeaderText="IdPregunta" ItemStyle-Width="5%" Visible="false" />
                                                <asp:BoundField DataField="nombre" HeaderText="Pregunta" ItemStyle-Width="40%" />

                                                <asp:TemplateField HeaderText="Cantidad" ItemStyle-Width="60%">
                                                    <ItemTemplate>

                                                        <asp:TextBox ID="TextBox2" class="form-control" ClientIDMode="AutoID" runat="server" Visible="false" TextMode="Number"></asp:TextBox>

                                                        <asp:LinkButton runat="server" ID="LBRadio" Visible="false" CommandArgument='<%# Eval("idPregunta") %>' CommandName="Preguntas">
                                                            <asp:RadioButtonList ID="RadioButtonList1" RepeatDirection="Horizontal" Width="90px" runat="server" AutoPostBack="True" BorderWidth="0" >
                                                                <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </asp:LinkButton>


                                                        <asp:TextBox ID="TextBox4" class="form-control" ClientIDMode="AutoID" runat="server" TextMode="MultiLine" Rows="5" Visible="false"></asp:TextBox>
                                                        <asp:Image ID="Image1" runat="server" />
                                                        <asp:FileUpload ID="FileUpload5" runat="server" Visible="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>


                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>


                            </div>
                        </div>
                    </div>

                </div>

            </div>
        </div>
    </div>


    

   




</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
