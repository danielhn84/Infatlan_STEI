<%@ Page Title="" Language="C#" MasterPageFile="~/mainAgencia.Master" AutoEventWireup="true" CodeBehind="Cableado.aspx.cs" Inherits="Infatlan_STEI_Agencias.paginasAgencia.Cableado" %>


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
    <br />
    <%-- Card 1 --%>
    <div class="row">
        <div class="col-lg-12">
            <div class="card">
                <div class="card-header bg-info">
                    <h4 class="m-b-0 text-white"><b>Datos Generales</b></h4>
                </div>
                <div class="card-body">
                    <form action="#">
                        <div class="form-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group has-success">
                                        <label class="control-label"><b>Técnico Responsable</b></label>
                                        <select id="cmbResponsable" class="form-control custom-select">
                                            <option value="">Carlos Ramirez</option>
                                            <option value="">Carlos </option>
                                        </select>
                                        <small class="form-control-feedback"></small>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label"><b>Identidad</label>
                                        <input type="text" id="txtIdentidad" class="form-control" placeholder="0000-0000-00000">
                                        <small class="form-control-feedback"></small>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group has-success">
                                        <label class="control-label"><b>Zona</label>
                                        <select id="cmbZona" class="form-control custom-select">
                                            <option value="">Centro</option>
                                            <option value="">Sur </option>
                                        </select>
                                        <small class="form-control-feedback"></small>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label"><b>Fecha Actual</label>
                                        <input id="txtFechaActual" type="date" class="form-control" placeholder="dd/mm/yyyy">
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group has-success">
                                        <label class="control-label"><b>Agencia Visitada</label>
                                        <select id="cmbAgenciaVisitada" class="form-control custom-select">
                                            <option value="">Miraflores</option>
                                            <option value="">Kenendy</option>
                                        </select>
                                        <small class="form-control-feedback"></small>
                                    </div>
                                </div>
                               <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label"><b>Dirección</label>
                                        <input type="text" id="txtDireccion" class="form-control" >
                                        <small class="form-control-feedback"></small>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label"><b>Fecha Calendario</label>
                                        <input id="txtFechaCalendario" type="date" class="form-control" placeholder="dd/mm/yyyy">
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label"><b>Fecha Realizado</label>
                                        <input id="txtFechaRealizado" type="date" class="form-control" placeholder="dd/mm/yyyy">
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

    <%-- Card 2--%>
    <div class="row">
        <div class="col-lg-12">
            <div class="card">
                <div class="card-header bg-info">
                    <h4 class="m-b-0 text-white"><b>Estudio Previo</b></h4>
                </div>
                <div class="card-body">
                    <form action="#">
                        <div class="form-body">
                            <div class="row p-t-20">

                                <div class="row col-6">
                                    <div class="col-lg-10 col-md-3">
                                        <label class="control-label"><b>Fotografía del cuarto de telecomunicaciones antes de la inspección</b></label>
                                        <div class="card">
                                            <div class="card-body">

                                                <input type="file" id="input-file-now" class="dropify" />
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label"><b>¿El cableado se encuentra etiquetado?</b></label>
                                        <div class="custom-control custom-radio">
                                            <input type="radio" id="customRadio1" name="customRadio" class="custom-control-input">
                                            <label class="custom-control-label" for="customRadio1">Si</label>
                                        </div>
                                        <br />
                                        <div class="custom-control custom-radio">
                                            <input type="radio" id="customRadio2" name="customRadio" class="custom-control-input">
                                            <label class="custom-control-label" for="customRadio2">No</label>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label class="control-label"><b>¿Es necesario re-ubicar el equipo de telcomunicaciones?</b></label>
                                        <div class="custom-control custom-radio">
                                            <input type="radio" id="customRadio1" name="customRadio" class="custom-control-input">
                                            <label class="custom-control-label" for="customRadio1">Si</label>
                                        </div>
                                        <br />
                                        <div class="custom-control custom-radio">
                                            <input type="radio" id="customRadio2" name="customRadio" class="custom-control-input">
                                            <label class="custom-control-label" for="customRadio2">No</label>
                                        </div>
                                        <br />
                                        <textarea name="comentarios" rows="4" class="form-control" cols="15">¿Porque?</textarea>
                                    </div>
                                </div>

                                <div class="col-2">
                                    <div class="form-group">
                                    </div>
                                </div>

                                <div class="row col-6">
                                    <div class="col-lg-10 col-md-3">
                                        <label class="control-label"><b>Fotografía del lugar que se propone para la re-ubicación del equipo</b></label>
                                        <div class="card">
                                            <div class="card-body">
                                                <input type="file" id="input-file-now" class="dropify" />
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label"><b>¿El cableado se encuentra desordenado?</b></label>
                                        <div class="custom-control custom-radio">
                                            <input type="radio" id="customRadio1" name="customRadio" class="custom-control-input">
                                            <label class="custom-control-label" for="customRadio1">Si</label>
                                        </div>
                                        <br />
                                        <div class="custom-control custom-radio">
                                            <input type="radio" id="customRadio2" name="customRadio" class="custom-control-input">
                                            <label class="custom-control-label" for="customRadio2">No</label>
                                        </div>
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
                                        <div class="custom-control custom-radio">
                                            <input type="radio" id="customRadio1" name="customRadio" class="custom-control-input">
                                            <label class="custom-control-label" for="customRadio1">Si</label>
                                        </div>
                                        <br />
                                        <div class="custom-control custom-radio">
                                            <input type="radio" id="customRadio2" name="customRadio" class="custom-control-input">
                                            <label class="custom-control-label" for="customRadio2">No</label>
                                        </div>
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
                                        <div class="custom-control custom-radio">

                                            <input type="radio" id="customRadio1" name="customRadio" class="custom-control-input">
                                            <label class="custom-control-label" for="customRadio1">Si</label>
                                        </div>
                                        <br />
                                        <div class="custom-control custom-radio">
                                            <input type="radio" id="customRadio2" name="customRadio" class="custom-control-input">
                                            <label class="custom-control-label" for="customRadio2">No</label>
                                        </div>
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

                                            <input type="radio" id="customRadio1" name="customRadio" class="custom-control-input">
                                            <label class="custom-control-label" for="customRadio1">Si</label>
                                        </div>
                                        <br />
                                        <div class="custom-control custom-radio">
                                            <input type="radio" id="customRadio2" name="customRadio" class="custom-control-input">
                                            <label class="custom-control-label" for="customRadio2">No</label>
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
                                        <div class="custom-control custom-radio">

                                            <input type="radio" id="customRadio1" name="customRadio" class="custom-control-input">
                                            <label class="custom-control-label" for="customRadio1">Si</label>
                                        </div>
                                        <br />
                                        <div class="custom-control custom-radio">
                                            <input type="radio" id="customRadio2" name="customRadio" class="custom-control-input">
                                            <label class="custom-control-label" for="customRadio2">No</label>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label"><b>¿Cuenta con aire acondicionado?</b></label>
                                        <div class="custom-control custom-radio">

                                            <input type="radio" id="customRadio1" name="customRadio" class="custom-control-input">
                                            <label class="custom-control-label" for="customRadio1">Si</label>
                                        </div>
                                        <br />
                                        <div class="custom-control custom-radio">
                                            <input type="radio" id="customRadio2" name="customRadio" class="custom-control-input">
                                            <label class="custom-control-label" for="customRadio2">No</label>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label"><b>¿Categoria de cables instalados en Agencia?</b></label>
                                        <div class="custom-control custom-radio">

                                            <input type="radio" id="customRadio1" name="customRadio" class="custom-control-input">
                                            <label class="custom-control-label" for="customRadio1">Categoria 5</label>
                                        </div>
                                        <br />
                                        <div class="custom-control custom-radio">
                                            <input type="radio" id="customRadio2" name="customRadio" class="custom-control-input">
                                            <label class="custom-control-label" for="customRadio2">Categoria 6</label>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label"><b>¿Cuenta con los estandares de rotulación?</b></label>
                                        <div class="custom-control custom-radio">

                                            <input type="radio" id="customRadio1" name="customRadio" class="custom-control-input">
                                            <label class="custom-control-label" for="customRadio1">Si</label>
                                        </div>
                                        <br />
                                        <div class="custom-control custom-radio">
                                            <input type="radio" id="customRadio2" name="customRadio" class="custom-control-input">
                                            <label class="custom-control-label" for="customRadio2">No</label>
                                        </div>
                                    </div>
                                </div>
                                <hr />

                            </div>
                    </form>
                </div>

            </div>
        </div>
    </div>

    <%-- Card 3--%>
    <div class="col-lg-12">
        <div class="card">
            <div class="card-header bg-info">
                <h4 class="m-b-0 text-white"><b>Materiales a solicitar según estudio</b></h4>
            </div>
            <div class="card-body">
                <form action="#">
                    <div class="form-body">
                        <div class="row p-t-20">

                            <div class="col-md-2">
                                <div class="form-group">
                                    <label class="control-label"><b>Patchord</b></label>
                                    <div class="custom-control custom-radio">
                                        <input type="radio" id="customRadio1" name="customRadio" class="custom-control-input">
                                        <label class="custom-control-label" for="customRadio1">Si</label>
                                    </div>
                                    <br />
                                    <div class="custom-control custom-radio">
                                        <input type="radio" id="customRadio2" name="customRadio" class="custom-control-input">
                                        <label class="custom-control-label" for="customRadio2">No</label>
                                    </div>
                                    <br />
                                </div>
                            </div>

                            <div class="col-md-2">
                                <div class="form-group">
                                    <div class="form-check form-check-inline" style="color: black">
                                        <input class="form-check-input" type="checkbox" id="inlineCheckbox1" value="option1">
                                        <label class="form-check-label" for="inlineCheckbox1">3 pies</label>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-1">
                                <label class="control-label">Cantidad (3 pies)</label>
                            </div>

                            <div class="col-md-1">
                                <input id="txt1" type="text" class="form-control">
                            </div>

                            <div class="col-md-1">
                                <label class="control-label"></label>
                            </div>

                            <div class="col-md-2">
                                <div class="form-group">
                                    <div class="form-check form-check-inline" style="color: black">
                                        <input class="form-check-input" type="checkbox" id="inlineCheckbox1" value="option1">
                                        <label class="form-check-label" for="inlineCheckbox1">7 pies</label>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-1">
                                <label class="control-label">Cantidad (7 pies)</label>
                            </div>

                            <div class="col-md-1">
                                <input id="txt1" type="text" class="form-control">
                            </div>

                        </div>
                        <hr />

                        <div class="row col-12">
                            <div class="col-2">
                                <label class="control-label"><b>Cable UTP</b></label>
                                <br />
                                <div class="custom-control custom-radio">
                                    <input type="radio" id="rdUTP1" name="customRadio" class="custom-control-input">
                                    <label class="custom-control-label" for="customRadio1">Si</label>
                                </div>
                                <br />
                                <div class="custom-control custom-radio">
                                    <input type="radio" id="rdUTP2" name="customRadio" class="custom-control-input">
                                    <label class="custom-control-label" for="customRadio2">No</label>
                                </div>
                            </div>

                            <div class="col-4">
                                <label class="control-label">Cantidad  </label>
                                <asp:TextBox runat="server" CssClass="form-control col-3" ID="txtCantidadUTP" />
                            </div>

                            <div class="col-2">
                                <label class="control-label"><b>Organizador(es)</b></label>
                                <br />
                                <div class="custom-control custom-radio">
                                    <input type="radio" id="rdOrganizador1" name="customRadio" class="custom-control-input">
                                    <label class="custom-control-label" for="customRadio1">Si</label>
                                </div>
                                <br />
                                <div class="custom-control custom-radio">
                                    <input type="radio" id="rdOrganizador2" name="customRadio" class="custom-control-input">
                                    <label class="custom-control-label" for="customRadio2">No</label>
                                </div>
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
                                    <input type="radio" id="rdUTP1" name="customRadio" class="custom-control-input">
                                    <label class="custom-control-label" for="customRadio1">Si</label>
                                </div>
                                <br />
                                <div class="custom-control custom-radio">
                                    <input type="radio" id="rdUTP2" name="customRadio" class="custom-control-input">
                                    <label class="custom-control-label" for="customRadio2">No</label>
                                </div>
                            </div>

                            <div class="col-4">
                                <label class="control-label">Cantidad  </label>
                                <asp:TextBox runat="server" CssClass="form-control col-3" ID="TextBox1" />
                            </div>

                            <div class="col-2">
                                <label class="control-label"><b>Tapadera</b></label>
                                <br />
                                <div class="custom-control custom-radio">
                                    <input type="radio" id="rdOrganizador1" name="customRadio" class="custom-control-input">
                                    <label class="custom-control-label" for="customRadio1">Si</label>
                                </div>
                                <br />
                                <div class="custom-control custom-radio">
                                    <input type="radio" id="rdOrganizador2" name="customRadio" class="custom-control-input">
                                    <label class="custom-control-label" for="customRadio2">No</label>
                                </div>
                            </div>

                            <div class="col-4">
                                <label class="control-label">Cantidad  </label>
                                <asp:TextBox runat="server" ID="TextBox2" CssClass="form-control col-3" />
                            </div>

                        </div>
                        <hr />

                        <div class="row col-12">
                            <div class="col-2">
                                <label class="control-label"><b>Bandeja para Rack</b></label>
                                <br />
                                <div class="custom-control custom-radio">
                                    <input type="radio" id="rdUTP1" name="customRadio" class="custom-control-input">
                                    <label class="custom-control-label" for="customRadio1">Si</label>
                                </div>
                                <br />
                                <div class="custom-control custom-radio">
                                    <input type="radio" id="rdUTP2" name="customRadio" class="custom-control-input">
                                    <label class="custom-control-label" for="customRadio2">No</label>
                                </div>
                            </div>

                            <div class="col-4">
                                <label class="control-label">Cantidad  </label>
                                <asp:TextBox runat="server" CssClass="form-control col-3" ID="TextBox3" />
                            </div>

                            <div class="col-2">
                                <label class="control-label"><b>Jack<</b>/label>
                                <br />
                                <div class="custom-control custom-radio">
                                    <input type="radio" id="rdOrganizador1" name="customRadio" class="custom-control-input">
                                    <label class="custom-control-label" for="customRadio1">Si</label>
                                </div>
                                <br />
                                <div class="custom-control custom-radio">
                                    <input type="radio" id="rdOrganizador2" name="customRadio" class="custom-control-input">
                                    <label class="custom-control-label" for="customRadio2">No</label>
                                </div>
                            </div>

                            <div class="col-4">
                                <label class="control-label">Cantidad  </label>
                                <asp:TextBox runat="server" ID="TextBox4" CssClass="form-control col-3" />
                            </div>

                        </div>
                        <hr />

                        <div class="row col-12">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label class="control-label"><b>Velcro</b></label>
                                    <div class="custom-control custom-radio">
                                        <input type="radio" id="customRadio1" name="customRadio" class="custom-control-input">
                                        <label class="custom-control-label" for="customRadio1">Si</label>
                                    </div>
                                    <br />
                                    <div class="custom-control custom-radio">
                                        <input type="radio" id="customRadio2" name="customRadio" class="custom-control-input">
                                        <label class="custom-control-label" for="customRadio2">No</label>
                                    </div>
                                    <br />
                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="form-group">
                                    <label class="control-label"><b>Cinta Rotadora</b></label>
                                    <div class="custom-control custom-radio">
                                        <input type="radio" id="customRadio1" name="customRadio" class="custom-control-input">
                                        <label class="custom-control-label" for="customRadio1">Si</label>
                                    </div>
                                    <br />
                                    <div class="custom-control custom-radio">
                                        <input type="radio" id="customRadio2" name="customRadio" class="custom-control-input">
                                        <label class="custom-control-label" for="customRadio2">No</label>
                                    </div>
                                    <br />
                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="form-group">
                                    <label class="control-label"><b>Gabinete/Rack</b></label>
                                    <div class="custom-control custom-radio">
                                        <input type="radio" id="customRadio1" name="customRadio" class="custom-control-input">
                                        <label class="custom-control-label" for="customRadio1">Si</label>
                                    </div>
                                    <br />
                                    <div class="custom-control custom-radio">
                                        <input type="radio" id="customRadio2" name="customRadio" class="custom-control-input">
                                        <label class="custom-control-label" for="customRadio2">No</label>
                                    </div>
                                    <br />
                                </div>
                            </div>
                        </div>
                        <hr />
                </form>
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
                                            <input type="radio" id="rdUTP1" name="customRadio" class="custom-control-input">
                                            <label class="custom-control-label" for="customRadio1">Si</label>
                                        </div>
                                        <br />
                                        <div class="custom-control custom-radio">
                                            <input type="radio" id="rdUTP2" name="customRadio" class="custom-control-input">
                                            <label class="custom-control-label" for="customRadio2">No</label>
                                        </div>
                                    </div>

                                    <div class="col-2">
                                        <label class="control-label"><b>Alimentación</b></label>
                                        <br />
                                        <div class="custom-control custom-radio">
                                            <input type="radio" id="rdUTP1" name="customRadio" class="custom-control-input">
                                            <label class="custom-control-label" for="customRadio1">Si</label>
                                        </div>
                                        <br />
                                        <div class="custom-control custom-radio">
                                            <input type="radio" id="rdUTP2" name="customRadio" class="custom-control-input">
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
                    <h4 class="m-b-0 text-white"<b>Observaciones</b></h4>
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
                                                <input type="radio" id="rdInfoValida" name="customRadio" class="custom-control-input">
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
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>

    <%-- Card 6 --%>
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
    <!-- ============================================================== -->
    <!-- Plugins for this page -->
    <!-- ============================================================== -->
    <!-- jQuery file upload -->
    <script src="/assets/node_modules/dropify/dist/js/dropify.min.js"></script>
    <script>
        $(document).ready(function () {
            // Basic
            $('.dropify').dropify();

            // Translated
            //$('.dropify-fr').dropify({
            //    messages: {
            //        default: 'Glissez-déposez un fichier ici ou cliquez',
            //        replace: 'Glissez-déposez un fichier ou cliquez pour remplacer',
            //        remove: 'Supprimer',
            //        error: 'Désolé, le fichier trop volumineux'
            //    }
            //});

            // Used events
            //var drEvent = $('#input-file-events').dropify();

            //drEvent.on('dropify.beforeClear', function(event, element) {
            //    return confirm("Do you really want to delete \"" + element.file.name + "\" ?");
            //});

            //drEvent.on('dropify.afterClear', function(event, element) {
            //    alert('File deleted');
            //});

            //drEvent.on('dropify.errors', function(event, element) {
            //    console.log('Has Errors');
            //});

            //var drDestroy = $('#input-file-to-destroy').dropify();
            //drDestroy = drDestroy.data('dropify')
            //$('#toggleDropify').on('click', function(e) {
            //    e.preventDefault();
            //    if (drDestroy.isDropified()) {
            //        drDestroy.destroy();
            //    } else {
            //        drDestroy.init();
            //    }
            //})
        });
    </script>
</asp:Content>

