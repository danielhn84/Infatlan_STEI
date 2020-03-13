<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="visitaTecnico.aspx.cs" Inherits="Infatlan_STEI_CableadoEstructurado.visitaTecnico" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <!-- Tell the browser to be responsive to screen width -->
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">
    <!-- Favicon icon -->
    <link rel="icon" type="image/png" sizes="16x16" href="/assets/images/favicon.png">
    <link rel="stylesheet" href="/assets/node_modules/dropify/dist/css/dropify.min.css">
    <!-- Custom CSS -->
    <link href="/css/style.min.css" rel="stylesheet">
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
    <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
    <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
<![endif]-->
   

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">


    <%-- Card 1 --%>

    <div class="row">
        <div class="col-lg-12">
            <div class="card">
                <div class="card-header bg-info">
                    <h4 class="m-b-0 text-white"><b>Estimaciones de Recursos</b></h4>
                </div>
                <div class="card-body">
                    <form action="#">
                        <div class="form-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label"><b>Técnico Responsable</b></label>
                                        <div class="col-sm-13">
                                            <asp:DropDownList ID="ddlResponsable" runat="server" AutoPostBack="true" class="form-control" OnSelectedIndexChanged="ddlResponsable_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label"><b>Identidad</label>
                                        <asp:TextBox ID="txtIdentidad" runat="server" type="text" class="form-control"></asp:TextBox>
                                        <small class="form-control-feedback"></small>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group ">
                                        <label class="control-label"><b>Zona</label>
                                        <div class="col-sm-13">
                                            <asp:DropDownList ID="DropDownList2" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label"><b>Fecha Estudio</label>
                                        <input id="txtFechaActual" type="date" class="form-control">
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group ">
                                        <label class="control-label"><b>Agencia Visitada</label>
                                        <div class="col-sm-12">
                                            <asp:DropDownList ID="ddlAgencia" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label"><b>Fecha Envío</label>
                                        <input type="text" id="txtFechaEnvio" class="form-control">
                                        <small class="form-control-feedback"></small>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label"><b>Dirección</label>
                                        <input type="text" id="txtDireccion" class="form-control">
                                        <small class="form-control-feedback"></small>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>

    <%-- Card 1--%>

    <%-- Card 2.1--%>

    <div class="row">
        <div class="col-lg-12">
            <div class="card">
                <div class="card-header bg-info">
                    <h4 class="m-b-0 text-white"><b>Estimaciones de Recursos</b></h4>
                </div>
                <div class="card-body">
                    <form action="#">
                        <div class="form-body">
                            <div class="row p-t-20">
                                <div class="row col-12">

                                    <div class="row col-6">
                                        <div class="col-lg-10 col-md-3">
                                            <label class="control-label"><b>Fotografía del cuarto de telecomunicaciones antes de la inspección</b></label>
                                            <div class="card">
                                                <div class="card-body">
                                                    <input type="file" id="input-file-now2" class="dropify" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="control-label"><b>¿El cableado se encuentra etiquetado?</b></label>
                                            <br />
                                            <asp:RadioButtonList ID="rdlEtiquetado" runat="server" CssClass="form-check">
                                                <asp:ListItem Value="si"> Si</asp:ListItem>
                                                <asp:ListItem Value="no"> No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>

                                        <asp:ScriptManager ID="ScriptManager1" runat="server" />
                                        <asp:UpdatePanel runat="server" ID="upRadio" AutoPostBack="true">
                                            <ContentTemplate>
                                                <label class="control-label"><b>¿Es necesario re-ubicar el equipo de telecomunicaciones?</b></label>

                                                <div class="custom-control custom-radio">
                                                    <asp:RadioButtonList runat="server" CssClass="custom-control">
                                                        <asp:ListItem Text="Si" Value="si"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="no"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>

    <%-- Card 2.1--%>

    <%-- Card 2.2  style="display: none; --%>

    <div class="row" id="FormularioRecursos">
        <div class="col-lg-12">
            <div class="card">
                <div class="card-body">
                    <form action="#">
                        <div class="form-body">
                            <div class="row p-t-20">
                                <div class="row col-12">
                                    <div class="col-2">
                                        <div class="form-group">
                                        </div>
                                    </div>

                                    <div class="row col-12">
                                        <div class="row col-6">
                                            <div class="col-lg-10 col-md-3">
                                                <label class="control-label"><b>Fotografía del lugar que se propone para la re-ubicación del equipo</b></label>
                                            </div>
                                        </div>

                                        <div class="row col-6">
                                            <div class="card-body">
                                                <input type="file" id="input-file-now1" class="dropify" />
                                            </div>
                                        </div>                                       
                                     </div>
                                    <br />
                                    
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="control-label"><b>¿El cableado se encuentra desordenado?</b></label>
                                            <br />
                                            <asp:RadioButtonList ID="rblDesordenado" runat="server">
                                                <asp:ListItem Text="Si" Value="si"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="no"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>

                                    <div class="row col-6">
                                        <div class="col-lg-10 col-md-3">
                                            <label class="control-label"><b>Fotografía del cableado desordenado</b></label>
                                            <div class="card">
                                                <div class="card-body">
                                                    <input type="file" id="input-file-now" class="dropify" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="control-label"><b>¿El equipo se encuentra expuesto a humedad o polvo?</b></label>
                                            <br />
                                            <asp:RadioButtonList ID="rblExpuestoHumedad" runat="server">
                                                <asp:ListItem Text="Si" Value="si"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="no"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>

                                    <div class="row col-6">
                                        <div class="col-lg-10 col-md-3">
                                            <label class="control-label"><b>Fotografía del equipo expuesto a humedad o polvo</b></label>
                                            <div class="card">
                                                <div class="card-body">
                                                    <input type="file" id="input-file-now" class="dropify" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="control-label"><b>¿El equipo se encuentra expuesto a robo o daño?</b></label>
                                            <br />
                                            <asp:RadioButtonList ID="rblExpuestoRobo" runat="server">
                                                <asp:ListItem Text="Si" Value="si"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="no"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>

                                    <div class="row col-6">
                                        <div class="col-lg-10 col-md-3">
                                            <label class="control-label"><b>Fotografía del equipo expuesto a robo o dañado</b></label>
                                            <div class="card">
                                                <div class="card-body">
                                                    <input type="file" id="input-file-now" class="dropify" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="control-label"><b>¿Se encuentran elementos ajenos al equipo de comunicación?</b></label>
                                            <div class="custom-control custom-radio">
                                                <br />
                                                <asp:RadioButtonList ID="rblElementosAjenos" runat="server">
                                                    <asp:ListItem Text="Si" Value="si"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="no"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row col-6">
                                        <div class="col-lg-10 col-md-3">
                                            <label class="control-label"><b>Fotografía de elementos ajenos a equipo de comunicación</b></label>
                                            <div class="card">
                                                <div class="card-body">
                                                    <input type="file" id="input-file-now" class="dropify" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="control-label"><b>¿Cuenta con UPS?</b></label>
                                            <br />
                                            <asp:RadioButtonList ID="rblUps" runat="server">
                                                <asp:ListItem Text="Si" Value="si"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="no"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="control-label"><b>¿Cuenta con aire acondicionado?</b></label>
                                            <br />
                                            <asp:RadioButtonList ID="rdlAire" runat="server">
                                                <asp:ListItem Text="Si" Value="si"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="no"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="control-label"><b>¿Categoria de cables instalados en Agencia?</b></label>
                                            <div class="custom-control custom-radio">
                                                <br />
                                                <asp:RadioButtonList ID="rblInstalacion" runat="server">
                                                    <asp:ListItem Text="Si" Value="si"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="no"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="control-label"><b>¿Cuenta con los estandares de rotulación?</b></label>
                                            <div class="custom-control custom-radio">
                                                <br />
                                                <asp:RadioButtonList ID="rblRotulacion" runat="server">
                                                    <asp:ListItem Text="Si" Value="si"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="no"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>

    <%-- Card 2 --%>

    <%-- Card 3 " --%>
    <div class="row">
        <div class="col-lg-12">
            <div class="card">
                <div class="card-header bg-info">
                    <h4 class="m-b-0 text-white"><b>Estimaciones de Recursos</b></h4>
                </div>
                <div class="card-body">
                    <form action="#">
                        <div class="form-body">
                            <div class="row p-t-20">
                                <div class="row col-12">

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="control-label"><b>Patchord</b></label>
                                            <div class="custom-control custom-radio">
                                                <input type="radio" id="rdPatchord1" name="customRadio" class="custom-control-input">
                                                <label class="custom-control-label" for="customRadio1">Si</label>
                                            </div>
                                            <br />
                                            <div class="custom-control custom-radio">
                                                <input type="radio" id="rdPatchord2" name="customRadio" class="custom-control-input">
                                                <label class="custom-control-label" for="customRadio2">No</label>
                                            </div>
                                            <br />
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <div class="form-check form-check-inline" style="color: black">
                                                <input class="form-check-input" type="checkbox" id="ck3pies" value="option1">
                                                <label class="form-check-label" for="inlineCheckbox1">3 pies</label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-1">
                                        <label class="control-label">Cantidad (3 pies)</label>
                                    </div>

                                    <div class="col-md-1">
                                        <input id="txtCantidad3pies" type="text" class="form-control">
                                    </div>

                                    <div class="col-md-1">
                                        <label class="control-label"></label>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <div class="form-check form-check-inline" style="color: black">
                                                <input class="form-check-input" type="checkbox" id="ck7pies" value="option1">
                                                <label class="form-check-label" for="inlineCheckbox1">7 pies</label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-1">
                                        <label class="control-label">Cantidad (7 pies)</label>
                                    </div>

                                    <div class="col-md-1">
                                        <input id="txtCantidad7pies" type="text" class="form-control">
                                    </div>

                                </div>
                                <hr />

                                <div class="row col-12">
                                    <div class="col-2">
                                        <label class="control-label"><b>Cable UTP</b></label>
                                        <br />
                                        <asp:RadioButtonList ID="rblUtp" runat="server">
                                            <asp:ListItem Text="Si" Value="si"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="no"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>

                                    <div class="col-4">
                                        <label class="control-label">Cantidad  </label>
                                        <asp:TextBox runat="server" CssClass="form-control col-3" ID="txtCantidadUTP" />
                                    </div>

                                    <div class="col-2">
                                        <label class="control-label"><b>Organizador(es)</b></label>
                                        <br />
                                        <asp:RadioButtonList ID="rblOrganizadores" runat="server">
                                            <asp:ListItem Text=" Si" Value="si"></asp:ListItem>
                                            <asp:ListItem Text=" No" Value="no"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>

                                    <div class="col-4">
                                        <label class="control-label">Cantidad  </label>
                                        <asp:TextBox runat="server" ID="txtCantidadOrganizador" CssClass="form-control col-3" />
                                    </div>

                                </div>
                                <hr />
                                <div class="row col-12">
                                    <div class="col-2">
                                        <label class="control-label"><b>Panel Flex</b></label>
                                        <br />
                                        <div class="custom-control custom-radio">
                                            <input type="radio" id="rdPanle1" name="customRadio" class="custom-control-input">
                                            <label class="custom-control-label" for="customRadio1">Si</label>
                                        </div>
                                        <br />
                                        <div class="custom-control custom-radio">
                                            <input type="radio" id="rdPanel2" name="customRadio" class="custom-control-input">
                                            <label class="custom-control-label" for="customRadio2">No</label>
                                        </div>
                                    </div>

                                    <div class="col-4">
                                        <label class="control-label">Cantidad  </label>
                                        <asp:TextBox runat="server" CssClass="form-control col-3" ID="txtCantidadPanel" />
                                    </div>

                                    <div class="col-2">
                                        <label class="control-label"><b>Tapadera</b></label>
                                        <br />
                                        <div class="custom-control custom-radio">
                                            <input type="radio" id="rdTpaderar1" name="customRadio" class="custom-control-input">
                                            <label class="custom-control-label" for="customRadio1">Si</label>
                                        </div>
                                        <br />
                                        <div class="custom-control custom-radio">
                                            <input type="radio" id="rdTapadera2" name="customRadio" class="custom-control-input">
                                            <label class="custom-control-label" for="customRadio2">No</label>
                                        </div>
                                    </div>

                                    <div class="col-4">
                                        <label class="control-label">Cantidad  </label>
                                        <asp:TextBox runat="server" ID="txtCantidadTapadera" CssClass="form-control col-3" />
                                    </div>

                                </div>
                                <hr />

                                <div class="row col-12">
                                    <div class="col-2">
                                        <label class="control-label"><b>Bandeja para Rack</b></label>
                                        <br />
                                        <div class="custom-control custom-radio">
                                            <input type="radio" id="rdRack1" name="customRadio" class="custom-control-input">
                                            <label class="custom-control-label" for="customRadio1">Si</label>
                                        </div>
                                        <br />
                                        <div class="custom-control custom-radio">
                                            <input type="radio" id="rdRack2" name="customRadio" class="custom-control-input">
                                            <label class="custom-control-label" for="customRadio2">No</label>
                                        </div>
                                    </div>

                                    <div class="col-4">
                                        <label class="control-label">Cantidad  </label>
                                        <asp:TextBox runat="server" CssClass="form-control col-3" ID="txtCantidadRack" />
                                    </div>

                                    <div class="col-2">
                                        <label class="control-label">
                                            <b>Jack<</b></label>
                                <br />
                                            <div class="custom-control custom-radio">
                                                <input type="radio" id="rdJack1" name="customRadio" class="custom-control-input">
                                                <label class="custom-control-label" for="customRadio1">Si</label>
                                            </div>
                                            <br />
                                            <div class="custom-control custom-radio">
                                                <input type="radio" id="rdJack2" name="customRadio" class="custom-control-input">
                                                <label class="custom-control-label" for="customRadio2">No</label>
                                            </div>
                                    </div>

                                    <div class="col-4">
                                        <label class="control-label">Cantidad  </label>
                                        <asp:TextBox runat="server" ID="txtCantidadJack" CssClass="form-control col-3" />
                                    </div>

                                </div>
                                <hr />

                                <div class="row col-12">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label class="control-label"><b>Velcro</b></label>
                                            <div class="custom-control custom-radio">
                                                <input type="radio" id="rdVelcro1" name="customRadio" class="custom-control-input">
                                                <label class="custom-control-label" for="customRadio1">Si</label>
                                            </div>
                                            <br />
                                            <div class="custom-control custom-radio">
                                                <input type="radio" id="rdVelcro2" name="customRadio" class="custom-control-input">
                                                <label class="custom-control-label" for="customRadio2">No</label>
                                            </div>
                                            <br />
                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label class="control-label"><b>Cinta Rotadora</b></label>
                                            <div class="custom-control custom-radio">
                                                <input type="radio" id="rdCinta1" name="customRadio" class="custom-control-input">
                                                <label class="custom-control-label" for="customRadio1">Si</label>
                                            </div>
                                            <br />
                                            <div class="custom-control custom-radio">
                                                <input type="radio" id="rdCnta2" name="customRadio" class="custom-control-input">
                                                <label class="custom-control-label" for="customRadio2">No</label>
                                            </div>
                                            <br />
                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label class="control-label"><b>Gabinete/Rack</b></label>
                                            <div class="custom-control custom-radio">
                                                <input type="radio" id="rdGabinete1" name="customRadio" class="custom-control-input">
                                                <label class="custom-control-label" for="customRadio1">Si</label>
                                            </div>
                                            <br />
                                            <div class="custom-control custom-radio">
                                                <input type="radio" id="rdGabinete2" name="customRadio" class="custom-control-input">
                                                <label class="custom-control-label" for="customRadio2">No</label>
                                            </div>
                                            <br />
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
    <%-- Card 3 --%>

    <%-- Card 4 --%>
    <div class="row">
        <div class="col-lg-12">
            <div class="card">
                <div class="card-header bg-info">
                    <h4 class="m-b-0 text-white"><b>Estimaciones de Recursos</b></h4>
                </div>
                <div class="card-body">
                    <form action="#">
                        <div class="form-body">
                            <div class="row p-t-20">

                                <div class="row col-12">

                                    <div class="col-4">
                                        <label class="control-label"><b>Duración del trabajo (Horas)</b></label>
                                        <asp:TextBox runat="server" CssClass="form-control col-3" ID="TextBox5" />
                                    </div>

                                    <div class="col-4">
                                        <label class="control-label"><b>Número de participantes</b></label>
                                        <asp:TextBox runat="server" CssClass="form-control col-3" ID="TextBox6" />
                                    </div>

                                    <div class="col-2">
                                        <label class="control-label"><b>Transporte</b></label>
                                        <br />
                                        <div class="custom-control custom-radio">
                                            <input type="radio" id="rdTransporte1" name="customRadio" class="custom-control-input">
                                            <label class="custom-control-label" for="customRadio1">Si</label>
                                        </div>
                                        <br />
                                        <div class="custom-control custom-radio">
                                            <input type="radio" id="rdTransporte2" name="customRadio" class="custom-control-input">
                                            <label class="custom-control-label" for="customRadio2">No</label>
                                        </div>
                                    </div>

                                    <div class="col-2">
                                        <label class="control-label"><b>Alimentación</b></label>
                                        <br />
                                        <div class="custom-control custom-radio">
                                            <input type="radio" id="rdAlimentacion1" name="customRadio" class="custom-control-input">
                                            <label class="custom-control-label" for="customRadio1">Si</label>
                                        </div>
                                        <br />
                                        <div class="custom-control custom-radio">
                                            <input type="radio" id="rdAlimentacion2" name="customRadio" class="custom-control-input">
                                            <label class="custom-control-label" for="customRadio2">No</label>
                                        </div>
                                    </div>

                                </div>

                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
    <%-- Card 4 --%>

    <%-- Card 5 --%>

    <div class="row">
        <div class="col-lg-12">
            <div class="card">
                <div class="card-header bg-info">
                    <h4 class="m-b-0 text-white"><b>Observaciones</b></h4>
                </div>
                <div class="card-body">
                    <form action="#">
                        <div class="form-body">
                            <div class="row p-t-20">

                                <hr />
                                <div class="row col-12">
                                    <div class="row col-6">
                                        <div class="form-group">
                                            <label class="control-label"><b>Observaciones </b></label>
                                            <textarea id="txtObservaciones" rows="5" cols="135"></textarea>
                                        </div>
                                    </div>
                                </div>

                                <hr />

                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>

    <%-- Card 5 --%>

    <%-- Card 6 --%>

    <div class="row">
        <div class="col-lg-12">
            <div class="card">
                <div class="card-header bg-info">
                    <h4 class="m-b-0 text-white"><b>Aprobaciones</b></h4>
                </div>
                <div class="card-body">
                    <form action="#">
                        <div class="form-body">
                            <div class="row p-t-20">

                                <div class="row col-12">
                                    <div class="row col-6">
                                        <div class="col-12">
                                            <label class="control-label"><b>¿La información ingresada es aprobada como valida?</b></label>
                                            <br />
                                            <div class="custom-control custom-radio">
                                                <input type="radio" id="rdInfoValida1" name="customRadio" class="custom-control-input">
                                                <label class="custom-control-label" for="customRadio1">Si</label>
                                            </div>
                                            <br />
                                            <div class="custom-control custom-radio">
                                                <input type="radio" id="rdInfoValida2" name="customRadio" class="custom-control-input">
                                                <label class="custom-control-label" for="customRadio2">No</label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row col-6">
                                        <div class="form-group">
                                            <label class="control-label"><b>Observaciones </b></label>
                                            <textarea id="txtObservacionesValida" rows="5" cols="63"></textarea>
                                        </div>
                                    </div>
                                </div>

                                <div class="row col-12">
                                    <asp:Button ID="Button1" runat="server" Text="Enviar Aprobación" type="button" class="boton-estado btn-ssucess" />
                                </div>
                            </div>

                        </div>
                    </form>
                </div>

            </div>
        </div>
    </div>

    <%-- Card 6 --%>
    <div class="col-md-12">
        <div class="form-group row">
            <label class="col-sm-3 col-form-label">Departamento</label>
            <div class="col-sm-9">
                <asp:DropDownList ID="DDLDepto" runat="server" class="form-control"></asp:DropDownList>
            </div>
        </div>
    </div>



</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">

    <script src="/assets/node_modules/jquery/jquery-3.2.1.min.js"></script>
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
    <script src="/assets/node_modules/sticky-kit-master/dist/sticky-kit.min.js"></script>
    <script src="/assets/node_modules/sparkline/jquery.sparkline.min.js"></script>
    <!--Custom JavaScript -->
    <script src="/js/custom.min.js"></script>
    <!-- icheck -->
    <script src="/assets/node_modules/icheck/icheck.min.js"></script>
    <script src="/assets/node_modules/icheck/icheck.init.js"></script>
    <script src="jquery-1.3.2.min.js" type="text/javascript"></script>
    <script>
        $(document).ready(function () {
            $("#rblReubicar").click(function (evento) {
                if ($("#rblReubicar").attr("checked")) {
                    $("#FormularioMateriales").css("display", "block");
                } else {
                    $("#FormularioMateriales").css("display", "none");
                }
            });
        });
    </script>
</asp:Content>
