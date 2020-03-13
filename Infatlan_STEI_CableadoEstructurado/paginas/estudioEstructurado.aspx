<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="estudioEstructurado.aspx.cs" Inherits="Infatlan_STEI_CableadoEstructurado.paginas.estudioEstructurado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">

    <script type="text/javascript">
        //Abrir modal
        function openModal() {
            $('#ModificarMaterialModal').modal('show');
        }

        // Cerrar modal
        function closeModal() {
            $('#ModificarMaterialModal').modal('hide');
        }
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <%-- Inicio Secciones--%>
    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-body">

                    <ul class="nav nav-tabs" role="tablist">
                        <li class="nav-item"><a class="nav-link active" data-toggle="tab" href="#nav-Compensatorio" role="tab"><span class="hidden-sm-up"><i class="ti-home"></i></span><span class="hidden-xs-down">Datos Generales</span></a> </li>
                        <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#nav_EstudioPrevio" role="tab"><span class="hidden-sm-up"><i class="ti-user"></i></span><span class="hidden-xs-down">Estudio Previo</span></a> </li>
                        <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#nav_Materiales" role="tab"><span class="hidden-sm-up"><i class="ti-email"></i></span><span class="hidden-xs-down">Materiales</span></a> </li>
                        <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#nav_Estimacion" role="tab"><span class="hidden-sm-up"><i class="ti-email"></i></span><span class="hidden-xs-down">Estimación de Recursos</span></a> </li>
                        <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#nav_Aprobacion" role="tab"><span class="hidden-sm-up"><i class="ti-email"></i></span><span class="hidden-xs-down">Aprobaciones</span></a> </li>

                        <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#nav_Contabilidad" role="tab"><span class="hidden-sm-up"><i class="ti-email"></i></span><span class="hidden-xs-down">Contabilidad</span></a> </li>
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

                                                <div class="form-group">
                                                    <label class="control-label"><b>Técnico Responsable</b></label>
                                                    <div class="col-sm-12">
                                                        <asp:DropDownList ID="ddlResponsable" runat="server" AutoPostBack="true" class="form-control" OnSelectedIndexChanged="ddlResponsable_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <asp:UpdatePanel runat="server" ID="UpdatePanel1" AutoPostBack="true">
                                                    <ContentTemplate>
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


                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group ">
                                                                    <label class="control-label"><b>Agencia</b></label>
                                                                    <div class="col-sm-12">
                                                                        <asp:DropDownList ID="ddlAgencia" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlAgencia_SelectedIndexChanged"></asp:DropDownList>
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

                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group ">
                                                            <label for="example-datetime-local-input"  class="col-6 col-form-label"><b>Fecha Estudio</b></label>
                                                            <div class="col-12">
                                                                 <asp:TextBox  value="2011-08-19T13:45:00" id="txtFechaEstudio" runat="server" type="date" class="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-6">
                                                        <div class="form-group ">
                                                            <label for="example-datetime-local-input" class="col-6 col-form-label"><b>Fecha Envío</b></label>
                                                            <div class="col-12">
                                                                <asp:TextBox  value="2011-08-19T13:45:00" id="txtFechaEnvio" runat="server" type="date" class="form-control"></asp:TextBox>
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

                        <%-- Sección 2 --%>
                        <div class="tab-pane  p-20" id="nav_EstudioPrevio" role="tabpanel">
                            <div class="row">
                                <div class="col-12 grid-margin stretch-card">
                                    <div class="card">
                                        <div class="card-body">
                                            <h4 class="card-title">Estudio Previo</h4>
                                            <br />

                                            <div class="row col-12">
                                                <div class="col-lg-10 col-md-3">
                                                    <label class="control-label"><b>Fotografía del cuarto de telecomunicaciones antes de la inspección</b></label>
                                                    <div class="card">
                                                        <div class="card-body">
                                                            <input type="file" id="input-file-now2" class="dropify" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <asp:UpdatePanel runat="server" ID="UpdatePanel2" AutoPostBack="true" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label class="control-label"><b>¿El cableado se encuentra etiquetado?</b></label>
                                                                <br />
                                                                <asp:RadioButtonList ID="rdlEtiquetado" runat="server" CssClass="form-check">
                                                                    <asp:ListItem Value="si"> Si</asp:ListItem>
                                                                    <asp:ListItem Value="no"> No</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label class="control-label"><b>¿Es necesario re-ubicar el equipo de telecomunicaciones?</b></label>

                                                                <asp:RadioButtonList ID="rdlReubicar" runat="server" CssClass="form-check" AutoPostBack="true" OnTextChanged="rdlReubicar_TextChanged1">
                                                                    <asp:ListItem Text="Si" Value="si"></asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="no"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <%-- Sección 2.2 --%>
                                                    <div class="row" runat="server" id="DivFormEstudio" visible="false">
                                                        <div class="col-12 grid-margin stretch-card">
                                                            <div class="card">
                                                                <div class="card-body">

                                                                    <div class="row col-12">
                                                                        <div class="col-lg-10 col-md-3">
                                                                            <label class="control-label"><b>Fotografía del lugar que se propone para la re-ubicación del equipo</b></label>
                                                                        </div>
                                                                        <div class="card-body">
                                                                            <input type="file" id="input-file-now1" class="dropify" />
                                                                        </div>
                                                                    </div>

                                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel3" AutoPostBack="true">
                                                                        <ContentTemplate>

                                                                            <div class="row">
                                                                                <div class="col-md-6">
                                                                                    <div class="form-group">
                                                                                        <label class="control-label"><b>¿El cableado se encuentra desordenado?</b></label>
                                                                                        <br />
                                                                                        <asp:RadioButtonList ID="rblDesordenado" runat="server" AutoPostBack="true" OnTextChanged="rblDesordenado_TextChanged">
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
                                                                                                <input type="file" id="imgDesordenado" class="dropify" runat="server" disabled />
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                            <div class="row">
                                                                                <div class="col-md-6">
                                                                                    <div class="form-group">
                                                                                        <label class="control-label"><b>¿El equipo se encuentra expuesto a humedad o polvo?</b></label>
                                                                                        <br />
                                                                                        <asp:RadioButtonList ID="rblExpuestoHumedad" runat="server" OnTextChanged="rblExpuestoHumedad_TextChanged" AutoPostBack="true">
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
                                                                                                <input type="file" id="imgExpuestoHumedad" class="dropify" runat="server" disabled />
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                            <div class="row">
                                                                                <div class="col-md-6">
                                                                                    <div class="form-group">
                                                                                        <label class="control-label"><b>¿El equipo se encuentra expuesto a robo o daño?</b></label>
                                                                                        <br />
                                                                                        <asp:RadioButtonList ID="rblExpuestoRobo" runat="server" AutoPostBack="true" OnTextChanged="rblExpuestoRobo_TextChanged">
                                                                                            <asp:ListItem Text="Si" Value="si"></asp:ListItem>
                                                                                            <asp:ListItem Text="No" Value="no"></asp:ListItem>
                                                                                        </asp:RadioButtonList>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row col-6">
                                                                                    <div class="col-lg-10 col-md-3">
                                                                                        <label class="control-label"><b>Fotografía del equipo expuesto a robo o daño</b></label>
                                                                                        <div class="card">
                                                                                            <div class="card-body">
                                                                                                <input type="file" id="imgExpuestoRobo" class="dropify" runat="server" disabled />
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                            <div class="row">
                                                                                <div class="col-md-6">
                                                                                    <div class="form-group">
                                                                                        <label class="control-label"><b>¿Se encuentran elementos ajenos al equipo de comunicación?</b></label>
                                                                                        <br />
                                                                                        <asp:RadioButtonList ID="rblElementosAjenos" runat="server" AutoPostBack="true" OnTextChanged="rblElementosAjenos_TextChanged">
                                                                                            <asp:ListItem Text="Si" Value="si"></asp:ListItem>
                                                                                            <asp:ListItem Text="No" Value="no"></asp:ListItem>
                                                                                        </asp:RadioButtonList>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row col-6">
                                                                                    <div class="col-lg-10 col-md-3">
                                                                                        <label class="control-label"><b>Fotografía de elementos ajenos a equipo de comunicación</b></label>
                                                                                        <div class="card">
                                                                                            <div class="card-body">
                                                                                                <input type="file" id="imgElementosAjenos" class="dropify" runat="server" disabled />
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                            <div class="row">
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
                                                                                        <br />
                                                                                        <asp:RadioButtonList ID="rblInstalacion" runat="server">
                                                                                            <asp:ListItem Text="Si" Value="si"></asp:ListItem>
                                                                                            <asp:ListItem Text="No" Value="no"></asp:ListItem>
                                                                                        </asp:RadioButtonList>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-md-6">
                                                                                    <div class="form-group">
                                                                                        <label class="control-label"><b>¿Cuenta con los estandares de rotulación?</b></label>
                                                                                        <br />
                                                                                        <asp:RadioButtonList ID="rblRotulacion" runat="server">
                                                                                            <asp:ListItem Text="Si" Value="si"></asp:ListItem>
                                                                                            <asp:ListItem Text="No" Value="no"></asp:ListItem>
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
                                                </ContentTemplate>
                                            </asp:UpdatePanel>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <%-- Sección 3 --%>
                        <div class="tab-pane p-20" id="nav_Materiales" role="tabpanel">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel4" AutoPostBack="true">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-12 grid-margin stretch-card">
                                            <div class="card">
                                                <div class="card-body">
                                                    <h4 class="card-title">Materiales a Solicitar según estudio</h4>
                                                    <br />

                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label class="control-label"><b>Materiales</b></label>
                                                                <div class="col-sm-12">
                                                                    <asp:DropDownList ID="ddlMateriales" runat="server" AutoPostBack="true" class="form-control"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-2">
                                                            <div class="form-group">
                                                                <label class="control-label"><b>Cantidad</b></label>
                                                                <asp:TextBox ID="txtCantidad" runat="server" class="form-control" TextMode="Number"></asp:TextBox>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-2">
                                                            <div class="form-group ">
                                                                <label class="control-label"><b>Unidades</b></label>
                                                                <asp:UpdatePanel runat="server" ID="updnidades" AutoPostBack="true">
                                                                    <ContentTemplate>
                                                                        <div class="col-sm-12">
                                                                            <asp:DropDownList ID="ddlMedidas" runat="server" class="form-control" AutoPostBack="true"></asp:DropDownList>
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>

                                                            </div>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <br />
                                                            <asp:Button ID="btnAgregar" runat="server" Text="ADD" class="btn  btn-block btn-success" OnClick="btnAgregar_Click" />

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <asp:UpdatePanel ID="UpdateDivMateriales" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="col-12 grid-margin stretch-card">
                                        <div class="card">
                                            <div class="card-body">
                                                <h4 class="card-title">Materiales Agregados</h4>
                                                <p>Ordenados por fecha de ingreso</p>
                                                <div class="row">
                                                    <div class="table-responsive">
                                                        <asp:GridView ID="GVMateriales" runat="server"
                                                            CssClass="table table-bordered"
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
                                                                <asp:TemplateField HeaderStyle-Width="60px" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Button ID="BtnModificar" runat="server" Text="Modificar" class="btn btn-inverse-primary  mr-2" CommandArgument='<%# Eval("idInventario") %>' CommandName="Modificar" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="numero" HeaderText="Numero" Visible="true" />
                                                                <asp:BoundField DataField="nombre" HeaderText="Material" />
                                                                <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>


                        <%-- Sección 4 --%>
                        <div class="tab-pane p-20" id="nav_Estimacion" role="tabpanel">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel5" AutoPostBack="true">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-12 grid-margin stretch-card">
                                            <div class="card">
                                                <div class="card-body">
                                                    <h4 class="card-title">Estimaciones de Recursos</h4>
                                                    <br />
                                                    <div class="row">

                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label class="control-label"><b>Duración del trabajo (Horas)</b></label>
                                                                <asp:TextBox ID="txtHorasTrabajo" runat="server" TextMode="Time" class="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label class="control-label"><b>Número de participantes</b></label>
                                                                <asp:TextBox ID="txtParticipantes" runat="server" type="Number" class="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label class="control-label"><b>Transporte</b></label>
                                                                <br />
                                                                <asp:RadioButtonList ID="rblTranporte" runat="server">
                                                                    <asp:ListItem Text="Si" Value="si"></asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="no"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>


                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label class="control-label"><b>Alimentación</b></label>
                                                                <br />
                                                                <asp:RadioButtonList ID="rblALimentación" runat="server">
                                                                    <asp:ListItem Text="Si" Value="si"></asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="no"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-12">
                                                            <div class="form-group">
                                                                <label class="control-label"><b>Observaciones</b></label>
                                                                <asp:TextBox ID="txtObservaciones" TextMode="MultiLine" Rows="5" runat="server" type="text" class="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>

                                                        <div class="row col-12">
                                                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" type="button" class="waves-light btn-outline-info" />
                                                        </div>

                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>

                        <%-- Sección 5 --%>
                        <div class="tab-pane p-20" id="nav_Aprobacion" role="tabpanel">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel6" AutoPostBack="true">
                                <ContentTemplate>

                                    <div class="row">
                                        <div class="col-12 grid-margin stretch-card">
                                            <div class="card">
                                                <div class="card-body">
                                                    <h4 class="card-title">Aprobaciones</h4>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label class="control-label"><b>¿La información ingresada es aprobada como valida?</b></label>
                                                                <br />
                                                                <asp:RadioButtonList ID="rblAprobada" runat="server">
                                                                    <asp:ListItem Text="Si" Value="si"></asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="no"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>

                                                        <div class="row col-6">
                                                            <div class="form-group">
                                                                <label class="control-label"><b>Observaciones </b></label>
                                                                <asp:TextBox TextMode="MultiLine" Rows="5" Columns="50" ID="txtObservacionesAprobacion" runat="server" type="text" class="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>

                                                        <asp:UpdatePanel ID="updAprobacion" runat="server">
                                                            <ContentTemplate>
                                                                <div class="row col-12">
                                                                    <asp:Button ID="btnEnviar" runat="server" Text="Enviar" type="button" class="waves-light btn-outline-info" OnClick="btnEnviar_Click" />
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                        

                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>


                        <%-- Prueba de tabla editable --%>

                        <div class="tab-pane p-20" id="nav_Contabilidad" role="tabpanel">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel7" AutoPostBack="true">
                                <ContentTemplate>

                                    <div class="row">

                                        <div class="col-md-12">
                                            <div class="form-group row">
                                                <label class="col-sm-3 col-form-label">Buscar</label>
                                                <div class="col-sm-9">
                                                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtBuscarProceso" runat="server" placeholder="Proveedor o Codigo - Presione afuera para proceder" class="form-control" AutoPostBack="true" OnTextChanged="txtBuscarProceso_TextChanged"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="col-md-12">
                                            <asp:UpdatePanel ID="udpContabilidad" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div class="col-12 grid-margin stretch-card">
                                                        <div class="card">
                                                            <div class="card-body">
                                                                <h4 class="card-title">Materiales Agregados</h4>
                                                                <p>Ordenados por fecha de ingreso</p>
                                                                <div class="row">
                                                                    <div class="table-responsive">
                                                                        <asp:GridView ID="GVContabilidad" runat="server"
                                                                            CssClass="table table-bordered"
                                                                            PagerStyle-CssClass="pgr"
                                                                            HeaderStyle-CssClass="table"
                                                                            RowStyle-CssClass="rows"
                                                                            AutoGenerateColumns="false"
                                                                            AllowPaging="true"
                                                                            GridLines="None"
                                                                            PageSize="10"
                                                                            OnPageIndexChanging="GVContabilidad_PageIndexChanging"
                                                                            OnRowCommand="GVContabilidad_RowCommand">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderStyle-Width="60px" Visible="true">
                                                                                    <ItemTemplate>
                                                                                        <asp:Button ID="BtnModificar" runat="server" Text="Modificar" class="btn btn-inverse-primary  mr-2" CommandArgument='<%# Eval("idInventario") %>' CommandName="Modificar" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="proveedor" HeaderText="Proveedor" Visible="true" />
                                                                                <asp:BoundField DataField="marca" HeaderText="Codigo" />
                                                                                <%--                                                                <asp:BoundField DataField="material" HeaderText="Material" />--%>
                                                                                <%--                                                                <asp:BoundField DataField="cantidad" HeaderText="Q" />--%>
                                                                                <asp:BoundField DataField="precio" HeaderText="Costo U" />
                                                                                <%--                                                                <asp:BoundField DataField="costo" HeaderText="Costo Total" />--%>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </div>
                                                                </div>
                                                            </div>
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





                    </div>
                </div>
            </div>
        </div>
    </div>




    <%--MODAL DE PASSWORD EMPLEADO--%>
    <div class="modal fade" id="ModificarMaterialModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="width: 600px; top: 320px; left: 50%; transform: translate(-50%, -50%);">
                <div class="modal-header">
                    <h4 class="modal-title" id="ModalLabelQA">
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>
                                Modificar Material
                                <asp:Label ID="LbModificarMaterial" runat="server" Text=""></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <asp:UpdatePanel ID="UpdateModalContabilidad" runat="server">
                            <ContentTemplate>

                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtModIdInventario" placeholder="" class="form-control" runat="server" Visible="true"></asp:TextBox>
                                </div>

                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group row">
                                            <label class="col-sm-3 col-form-label">Proveedor</label>
                                            <div class="col-sm-9">
                                                <asp:TextBox runat="server" ID="txtModProveedor" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="form-group row">
                                            <label class="col-sm-3 col-form-label">Marca</label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtModMarca" placeholder="" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group row">
                                            <label class="col-sm-3 col-form-label">Costo Unitario</label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtModCostoUnitario" placeholder="" class="form-control" runat="server" TextMode="Number"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <asp:UpdatePanel ID="UpdateUsuarioMensaje" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="form-group row">
                                <asp:Label ID="LbUsuarioMensaje" runat="server" Text="" Class="col-sm-12" Style="color: indianred; text-align: center;"></asp:Label>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                        <ContentTemplate>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                            <asp:Button ID="BtnModCambiarMaterial" runat="server" Text="Modificar" class="btn btn-primary" OnClick="BtnModCambiarMaterial_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
