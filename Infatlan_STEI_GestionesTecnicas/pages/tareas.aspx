<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="tareas.aspx.cs" Inherits="Infatlan_STEI_GestionesTecnicas.pages.tareas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <script type="text/javascript">
        var updateProgress = null;
        function postbackButtonClick() {
            updateProgress = $find("<%= UpdateProgress1.ClientID %>");
            window.setTimeout("updateProgress.set_visible(true)", updateProgress.get_displayAfter());
            return true;
        }
    </script>

    <script type="text/javascript">
        function openModalAddTarea() { $('#modalMasInfo').modal('show'); }
        function cerrarModalAddTarea() { $('#modalMasInfo').modal('hide'); }
    </script>

    <link href="../dist/css/pages/ribbon-page.css" rel="stylesheet" />
    <link href="../assets/node_modules/select2/dist/css/select2.min.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #ffffff; opacity: 0.7; margin: 0;">
                <span style="display: inline-block; height: 100%; vertical-align: middle;"></span>
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="/images/loading.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="display: inline-block; vertical-align: middle;" />
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
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Gestiones Técnicas</a></li>
                    <li class="breadcrumb-item active">Tareas</li>
                </ol>
            </div>
        </div>
    </div>

<%--    <asp:UpdatePanel ID="UpdateModificacionBotones" runat="server">
        <ContentTemplate>
            <asp:Button ID="BtnAddTarea" Text="+" CssClass="btn btn-primary" runat="server" OnClick="BtnAddTarea_Click" />
        </ContentTemplate>
    </asp:UpdatePanel>--%>

<%--    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-header" role="tab" id="headingOneIni">
                    <a class="link" data-toggle="collapse" data-parent="#accordion2" href="#collapseOneIni" aria-expanded="true" aria-controls="collapseOneIni">
                        <h4 class="card-title">Creación Tarea</h4>
                    </a>
                </div>
                <div id="collapseOneIni" class="collapse show" role="tabpanel" aria-labelledby="headingOneIni">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="card">
                                <div class="card-body">
                                    <ul class="nav nav-tabs" role="tablist">
                                        <li class="nav-item"><a class="nav-link active" data-toggle="tab" href="#home" role="tab"><span class="hidden-sm-up"><i class="fa fa-list"></i></span><span class="hidden-xs-down">Tarea</span></a> </li>
                                        <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#adjunto" role="tab"><span class="hidden-sm-up"><i class="fa fa-paperclip"></i></span><span class="hidden-xs-down">Adjunto</span></a> </li>
                                        <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#profile" role="tab"><span class="hidden-sm-up"><i class="fa fa-comment"></i></span><span class="hidden-xs-down">Comentarios</span></a> </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>



            </div>
        </div>

    </div>--%>


    <div class="row">
        <div class="col-md-4">
            <div class="card">
                <div class="card-header" role="tab" id="headingOne12">
                    <a class="link" data-toggle="collapse" data-parent="#accordion2" href="#collapseOne12" aria-expanded="true" aria-controls="collapseOne12">
                        <%--   <h4 class="card-title">En Curso</h4>--%>
                        <div class="card">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="d-flex no-block align-items-center">
                                            <div>
                                                <h3><i class="icon-screen-desktop"></i></h3>
                                                <h4 class="card-title">En Curso</h4>
                                            </div>
                                            <div class="ml-auto">
                                                <h2 class="counter text-cyan">3</h2>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12">
                                        <div class="progress">
                                            <div class="progress-bar bg-cyan " role="progressbar" style="width: 85%; height: 6px;" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
                <div id="collapseOne12" class="collapse show" role="tabpanel" aria-labelledby="headingOne12">
                    <div class="card-body">
                        <div class="card-body">

                            <asp:Literal Text="" ID="LitEnCurso" runat="server" />
                            <div class="ribbon-wrapper card">
                                <div class="ribbon ribbon-info ribbon-right">Baja</div>
<%--                                <span><i class="fa fa-clock-o"></i>9 hours</span>
                                <span><i class="fa fa-list"></i>Ticket #</span>--%>
                                <br>
                                <h4 class="card-title">Creación Manual Datacenter</h4>
                                <h6 class="card-subtitle mb-2 text-muted">Gestiones Técnicas</h6>
                                <h6 class="card-subtitle mb-2 text-muted">Ticket # 11</h6>

                                <br>
                                <div class="col-md-12 text-center">
                                    <button type="button" class="btn btn-circle btn-info"><i class="fa fa-info"></i></button>
                                </div>
                            </div>

                            <div class="ribbon-wrapper card">
                                <div class="ribbon ribbon-warning ribbon-right">Media</div>
                                <br>
                                <h4 class="card-title">Creación Manual Datacenter</h4>
                                <h6 class="card-subtitle mb-2 text-muted">Gestiones Técnicas</h6>
                                <h6 class="card-subtitle mb-2 text-muted">Ticket # 12</h6>
                                <br>
                                <div class="col-md-12 text-center">
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <contenttemplate>
                                            <asp:LinkButton ID="LbMasInfo" class="btn btn-circle btn-warning" runat="server" OnClick="Modal_Click" CommandName="11">
                                                        <i class="fa fa-info" ></i> 
                                            </asp:LinkButton>
                                        </contenttemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>

                            <div class="ribbon-wrapper card">
                                <div class="ribbon ribbon-danger ribbon-right"> Alta </div>
                                <br>
                                <h4 class="card-title">Creación Manual Datacenter</h4>
                                <h6 class="card-subtitle mb-2 text-muted">Gestiones Técnicas</h6>
                                <h6 class="card-subtitle mb-2 text-muted">Ticket # 13</h6>
                                <br>
                                <div class="col-md-12 text-center">
                                    <button type="button" class="btn btn-circle btn-danger"><i class="fa fa-info"></i></button>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-md-4">
            <div class="card">
                <div class="card-header" role="tab" id="headingOne13">
                    <a class="link" data-toggle="collapse" data-parent="#accordion2" href="#collapseOne13" aria-expanded="true" aria-controls="collapseOne13">
                        <%-- <h4 class="card-title">Atrasados</h4>--%>

                        <div class="card">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="d-flex no-block align-items-center">
                                            <div>
                                                <h3><i class="icon-note"></i></h3>
                                                <h4 class="card-title">Atrasados</h4>
                                            </div>
                                            <div class="ml-auto">
                                                <h2 class="counter text-primary">0</h2>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12">
                                        <div class="progress">
                                            <div class="progress-bar bg-primary" role="progressbar" style="width: 85%; height: 6px;" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </a>
                </div>
                <div id="collapseOne13" class="collapse show" role="tabpanel" aria-labelledby="headingOne13">
                    <div class="card-body">
                        <div class="card-body">
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-md-4">
            <div class="card">
                <div class="card-header" role="tab" id="headingOne14">
                    <a class="link" data-toggle="collapse" data-parent="#accordion2" href="#collapseOne14" aria-expanded="true" aria-controls="collapseOne14">
                        <%-- <h4 class="card-title">Completados Hoy</h4>--%>
                        <div class="card">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="d-flex no-block align-items-center">
                                            <div>
                                                <h3><i class="icon-doc"></i></h3>
                                                <h4 class="card-title">Completados Hoy</h4>
                                            </div>
                                            <div class="ml-auto">
                                                <h2 class="counter text-purple">0</h2>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12">
                                        <div class="progress">
                                            <div class="progress-bar bg-purple" role="progressbar" style="width: 85%; height: 6px;" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
                <div id="collapseOne14" class="collapse show" role="tabpanel" aria-labelledby="headingOne14">
                    <div class="card-body">
                        <div class="card-body">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--INICIO MODAL --%>
    <div class="modal bs-example-modal-lg" id="modalMasInfo" tabindex="-1" data-backdrop="static" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog modal-xl">
            <div class="modal-content" style="width: 1000px; top: 365px; left: 50%; transform: translate(-50%, -50%);">
                <div class="modal-header">
                    <h4 class="modal-title" id="myLargeModalLabel">Tarea</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                </div>

                <div class="modal-body">
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="nav-item"><a class="nav-link active" data-toggle="tab" href="#home" role="tab"><span class="hidden-sm-up"><i class="fa fa-list"></i></span><span class="hidden-xs-down"> Datos Generales</span></a> </li>
                        <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#adjunto" role="tab"><span class="hidden-sm-up"><i class="fa fa-paperclip"></i></span><span class="hidden-xs-down"> Adjunto</span></a> </li>
                        <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#comentarios" role="tab"><span class="hidden-sm-up"><i class="fa fa-comment"></i></span><span class="hidden-xs-down"> Comentarios</span></a> </li>
                        <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#solucion" role="tab"><span class="hidden-sm-up"><i class="fa fa-plus-circle"></i></span><span class="hidden-xs-down"> Solución</span></a> </li>
                    </ul>
                    <div class="tab-content tabcontent-border" style="height: 500px">
                        <!--PRIMER CONTENIDO-->
                        <div class="tab-pane active p-20" id="home" role="tabpanel">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <!--Inicio Fila 1-->
                                    <div class="row p-t-20">
                                        <div class="col-6">
                                            <label class="control-label">Título:</label></label>
                                            <asp:TextBox ID="TxTitulo" ReadOnly="true" runat="server" class="form-control"></asp:TextBox>
                                        </div>

                                        <div class="col-6">
                                            <label class="control-label">Fecha Solicitud:</label></label>    
                                            <asp:TextBox ID="TxFechaSolicitud" ReadOnly="true" runat="server" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <!--Fin Fila 1-->
                                    <!--Inicio Fila 2-->
                                    <div class="row p-t-20">
                                        <div class="col-12">
                                          <label class="control-label">Descripción:</label></label>
                                          <asp:TextBox ID="TxDescripcion" ReadOnly="true" TextMode="MultiLine" Rows="4" runat="server" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <!--Fin Fila 2-->
                                    <!--Inicio Fila 3-->
                                    <div class="row p-t-20">
                                        <div class="col-6">
                                            <label class="control-label">Responsable:</label></label>
                                            <asp:TextBox ID="TxResponsable" ReadOnly="true" runat="server" class="form-control"></asp:TextBox>
                                        </div>

                                        <div class="col-6">
                                                <label class="control-label">Prioridad:</label></label>    
                                                <asp:DropDownList ID="DdlPrioridad" Enabled="false"  runat="server" AutoPostBack="true" CssClass="form-control">
                                                    <asp:ListItem Value="0" Text="Seleccione una opción"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Alta"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Normal"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Baja"></asp:ListItem>
                                                </asp:DropDownList>
                                        </div>
                                    </div>
                                    <!--Fin Fila 3-->
                                    <div class="row p-t-20">
                                        <div class="col-6">
                                            <label class="control-label">Tipo Gestión:</label></label>
                                            <asp:TextBox ID="TxTipoGestion" ReadOnly="true" runat="server" class="form-control"></asp:TextBox>
                                        </div>

                                        <div class="col-6">
                                            <label class="control-label">Entrega:</label></label>    
                                            <asp:TextBox ID="TxFechaEntrega"  ReadOnly="true" runat="server" TextMode="DateTimeLocal" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row p-t-20">
                                        <div class="col-6">
                                            <label class="control-label">Tiempo Productivo(min):</label>
                                            <asp:TextBox ID="TxMinProductivo" AutoPostBack="true" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>

                                        <div class="col-6">
                                            <label class="control-label">Estado:</label> 
                                            <asp:DropDownList runat="server" ID="DdlEstado" CssClass="select2 form-control custom-select" Style="width: 100%"></asp:DropDownList>
                                        </div>
                                    </div>



                                    <br>                                    
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>

                        <!--SEGUNDO CONTENIDO-->
                        <div class="tab-pane p-20" id="adjunto" role="tabpanel">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                     <div class="col-md-12"  runat="server" id="divAdjunto" visible="false">
                                                <div class="row col-12 mt-3">
                                                    <div class="table table-bordered">
                                                        <asp:GridView ID="GvAdjunto" runat="server"
                                                            CssClass="mydatagrid"
                                                            PagerStyle-CssClass="pgr"
                                                            HeaderStyle-CssClass="header"
                                                            RowStyle-CssClass="rows"
                                                            AutoGenerateColumns="false"
                                                            AllowPaging="true"
                                                            GridLines="None"
                                                            PageSize="3">
                                                            <Columns>
                                                              <%--  <asp:BoundField DataField="idAdjunto" Visible="false" ItemStyle-Width="27%" /--%>
                                                                <asp:BoundField DataField="nombre" Visible="true" ItemStyle-Width="95%" />
                                                                <asp:BoundField DataField="archivo" Visible="false" ItemStyle-Width="27%" />
                                                                <asp:TemplateField HeaderText="Seleccione" HeaderStyle-Width="">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="BtnDescargar" runat="server" title="Eliminar" Style="background-color: #d9534f" class="btn" CommandArgument='<%# Eval("nombre") %>' CommandName="Descargar">
                                                                <i class="mdi mdi-delete text-white"></i>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>


                        <!--TERCER CONTENIDO-->
                        <div class="tab-pane p-20" id="comentarios" role="tabpanel">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="col-md-12" runat="server" id="divComentario" visible="false">
                                        <div class="row col-12 mt-3">
                                            <div class="table table-bordered">
                                                <asp:GridView ID="GvComentario" runat="server"
                                                    CssClass="mydatagrid"
                                                    PagerStyle-CssClass="pgr"
                                                    HeaderStyle-CssClass="header"
                                                    RowStyle-CssClass="rows"
                                                    AutoGenerateColumns="false"
                                                    AllowPaging="true"
                                                    GridLines="None"
                                                    PageSize="3">
                                                    <Columns>                      
                                                        <asp:BoundField DataField="usuarioComentario" Visible="true"  />
                                                        <asp:BoundField DataField="comentario" Visible="true" ItemStyle-Width="100%" />                                                        
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>

                    </div>



                </div>
                <div class="modal-footer">
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                            <ContentTemplate>
                                                <button type="button" class="btn btn-light"
                                                    data-dismiss="modal">
                                                    Close</button>
                                                <asp:Button ID="btnModalModificarTipoAgencia" runat="server" Text="Enviar" class="btn btn-dark" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
            </div>


        </div>

    </div>

    <%--FIN MODAL APROBAR LV--%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <%--COMBO BUSCADOR--%>
    <script src="../assets/node_modules/select2/dist/js/select2.full.min.js" type="text/javascript"></script>
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
