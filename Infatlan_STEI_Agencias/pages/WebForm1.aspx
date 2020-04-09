<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Infatlan_STEI_Agencias.pages.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
        <script src="http://code.jquery.com/jquery-1.10.2.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        //Imagen 1
      <%--  function img1(input) {

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
        }--%>

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
        <%--function img7(input) {

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
        }--%>


</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">

    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-body">

                    <ul class="nav nav-tabs" role="tablist">

                        <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#nav_EstudioPrevio" role="tab"><span class="hidden-sm-up"><i class="ti-pencil-alt"></i></span><span class="hidden-xs-down">Estudio Previo</span></a> </li>

                    </ul>

                    <div class="tab-content tabcontent-border">

                        <div class="tab-pane  p-20" id="nav_EstudioPrevio" role="tabpanel">
                            <div class="row">
                                <div class="col-12 grid-margin stretch-card">
                                    <div class="card">
                                        <div class="card-body">
                                            <h4 class="card-title">Estudio Previo</h4>
                                            <br />


                                            <asp:UpdatePanel runat="server" ID="udpReubicar" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <%-- TABLA 2 --%>
                                                    <table class="table color-bordered-table muted-bordered-table" style="border-collapse: separate" data-tablesaw-mode="swipe">
                                                        <thead>
                                                            <tr>
                                                                <th scope="col" data-tablesaw-sortable-coldata-tablesaw-priority="persist" class="border text-xl-center">Pregunta</th>
                                                                <th scope="col" data-tablesaw-sortable-coldata-tablesaw-priority="2" class="border text-xl-center">Si / No
                                                                </th>
                                                            </tr>
                                                        </thead>

                                                        <tbody>

                                                            <tr>
                                                                <td class="title"><a class="link" href="javascript:void(0)">¿El cableado se encuentra etiquetado?</a></td>
                                                                <td>
                                                                    <asp:UpdatePanel runat="server" ID="udpEtiquetado" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:RadioButtonList ID="rblEtiquetado" runat="server" CssClass="custom-checkbox" BorderStyle="None" RepeatDirection="Horizontal" AutoPostBack="true">
                                                                                <asp:ListItem Value="si" Text="Si"></asp:ListItem>
                                                                                <asp:ListItem Value="no" Text="No"></asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>


                                                            <tr>
                                                                <td class="title"><a class="link" href="javascript:void(0)">¿Es necesario re-ubicar el equipo de telecomunicaciones?</a></td>

                                                                <td>
                                                                    <%-- <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
                                                                        <ContentTemplate>--%>
                                                                    <asp:RadioButtonList ID="rblReubicar" runat="server" CssClass="custom-checkbox" BorderStyle="None" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblReubicar_TextChanged" AutoPostBack="true">
                                                                        <asp:ListItem Value="si" Text="Si"></asp:ListItem>
                                                                        <asp:ListItem Value="no" Text="No"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                    <%--  </ContentTemplate>
                                                                    </asp:UpdatePanel>--%>
                                                                </td>

                                                            </tr>


                                                        </tbody>

                                                    </table>

                                                    <%-- TABLE VISIBLE --%>
                                                    <div class="row" runat="server" id="Div1" visible="false">

                                                        <%-- TABLA 3 --%>
                                                        <table class="table color-bordered-table muted-bordered-table" style="border-collapse: separate" data-tablesaw-mode="swipe">
                                                            <thead>
                                                                <tr>
                                                                    <th scope="col" data-tablesaw-sortable-coldata-tablesaw-priority="persist" class="border text-xl-center" style="width: 30%">Pregunta</th>
                                                                    <th scope="col" data-tablesaw-sortable-coldata-tablesaw-priority="2" class="border text-xl-center" style="width: 25%">Si / No </th>
                                                                    <th scope="col" data-tablesaw-sortable-coldata-tablesaw-priority="3" class="border text-xl-center" style="width: 5%">Seleccionar Fotografía
                                                                    </th>
                                                                    <th scope="col" data-tablesaw-sortable-coldata-tablesaw-priority="1" class="border text-xl-center">
                                                                        <abbr title="Rotten Tomato Rating">Fotografía</abbr>
                                                                    </th>
                                                                </tr>
                                                            </thead>

                                                            <tbody>
                                                         <%--       <asp:UpdatePanel runat="server" ID="udpImgReubicar" UpdateMode="Conditional">
                                                                    <ContentTemplate>--%>
                                                                        <tr>
                                                                            <td class="title"><a class="link" href="javascript:void(0)">Fotografia Reubicar</a></td>
                                                                            <td></td>
                                                                            <td>
                                                                                <asp:FileUpload ID="fuReubicar" runat="server" onchange="img2(this);" />
                                                                            </td>
                                                                            <td>
                                                                                <img runat="server" id="imgReubicar" height="250" width="250" src="/assets/images/image_not_available.png" style="border-width: 0px;" />
                                                                            </td>
                                                                        </tr>
                                                           <%--         </ContentTemplate>
                                                                </asp:UpdatePanel>--%>

                                                                <tr>
                                                                    <td class="title"><a class="link" href="javascript:void(0)">Cableado desordenado</a></td>

                                                                   <%-- <asp:UpdatePanel runat="server" ID="udpDesordenado" UpdateMode="Conditional">
                                                                        <ContentTemplate>--%>
                                                                            <td>
                                                                                <asp:RadioButtonList ID="rblDesordenado" runat="server" CssClass="custom-checkbox" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblDesordenado_TextChanged" AutoPostBack="true">
                                                                                    <asp:ListItem Value="si" Text="Si"></asp:ListItem>
                                                                                    <asp:ListItem Value="no" Text="No"></asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                            </td>

                                                                            <td>
                                                                                <asp:FileUpload ID="fuDesordenado" runat="server" onchange="img3(this);" Visible="false" />
                                                                            </td>
                                                                            <td>
                                                                                <img runat="server" id="imgDesordenado" height="250" width="250" src="/assets/images/image_not_available.png" style="border-width: 0px;" visible="false" />

                                                                            </td>
                                                                     <%--   </ContentTemplate>
                                                                    </asp:UpdatePanel>--%>


                                                                     <asp:UpdatePanel runat="server" ID="UpUPS" UpdateMode="Conditional">
            
            </asp:UpdatePanel>
                                                                </tr>

                                                                <tr>
                                                                    <td class="title"><a class="link" href="javascript:void(0)">Equipo expuesto a humedad o polvo</a></td>
                                                                    <asp:UpdatePanel runat="server" ID="udpExpuestpHumedo" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <td>
                                                                                <asp:RadioButtonList ID="rblExpuestoHumedo" runat="server" CssClass="custom-checkbox" BorderStyle="None" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblExpuestoHumedad_TextChanged">
                                                                                    <asp:ListItem Value="si" Text="Si"></asp:ListItem>
                                                                                    <asp:ListItem Value="no" Text="No"></asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                            </td>
                                                                            <td>
                                                                                <asp:FileUpload ID="fuExpuestoHumedo" runat="server" onchange="img4(this);" Visible="false" />
                                                                            </td>
                                                                            <td>
                                                                                <img runat="server" id="imgExpuestoHumedo" height="250" width="250" src="/assets/images/image_not_available.png" style="border-width: 0px;" visible="false" />

                                                                            </td>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </tr>

                                                                <tr>
                                                                    <td class="title"><a class="link" href="javascript:void(0)">Equipo expuesto a robo o daño</a></td>

                                                                    <asp:UpdatePanel runat="server" ID="udpExpuestoRobo" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <td>

                                                                                <asp:RadioButtonList ID="rblExpuestoRobo" runat="server" CssClass="custom-checkbox" BorderStyle="None" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblExpuestoRobo_TextChanged">
                                                                                    <asp:ListItem Value="si" Text="Si"></asp:ListItem>
                                                                                    <asp:ListItem Value="no" Text="No"></asp:ListItem>
                                                                                </asp:RadioButtonList>

                                                                            </td>
                                                                            <td>
                                                                                <asp:FileUpload ID="fuExpuestoRobo" runat="server" onchange="img5(this);" Visible="false" />
                                                                            </td>
                                                                            <td>
                                                                                <img runat="server" id="imgExpuestoRobo" height="250" width="250" src="/assets/images/image_not_available.png" style="border-width: 0px;" visible="false" />
                                                                            </td>

                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </tr>

                                                                <tr>
                                                                    <td class="title"><a class="link" href="javascript:void(0)">Ajenos al equipo de comunicación</a></td>
                                                                    <asp:UpdatePanel runat="server" ID="udpElemetoAjeno" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <td>

                                                                                <asp:RadioButtonList ID="rblElementoAjenos" runat="server" CssClass="custom-checkbox" BorderStyle="None" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblElementosAjenos_TextChanged">
                                                                                    <asp:ListItem Value="si" Text="Si"></asp:ListItem>
                                                                                    <asp:ListItem Value="no" Text="No"></asp:ListItem>
                                                                                </asp:RadioButtonList>

                                                                            </td>
                                                                            <td>
                                                                                <asp:FileUpload ID="fuElemetoAjenos" runat="server" onchange="img6(this);" Visible="false" />
                                                                            </td>
                                                                            <td>
                                                                                <img runat="server" id="imgElementoAjeno" height="250" width="250" src="/assets/images/image_not_available.png" style="border-width: 0px;" visible="false" />

                                                                            </td>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </tr>


                                                            </tbody>

                                                        </table>


                                                    
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



    <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <%--         <asp:HiddenField ID="HFCuartoTelecomunicaciones" runat="server" />--%>
            <asp:HiddenField ID="HFReubicar" runat="server" />
            <asp:HiddenField ID="HFDesordenado" runat="server" />
            <asp:HiddenField ID="HFExpuestoHumedo" runat="server" />
            <asp:HiddenField ID="HFExpuestoRobo" runat="server" />
            <asp:HiddenField ID="HFEquiposAjeno" runat="server" />
            <%--<asp:HiddenField ID="HFPlano" runat="server" />--%>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
