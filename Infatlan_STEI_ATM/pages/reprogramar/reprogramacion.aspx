<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="reprogramacion.aspx.cs" Inherits="Infatlan_STEI_ATM.pages.reprogramar.reprogramacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
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
                    <li class="breadcrumb-item"><a href="javascript:void(0)">ATM</a></li>
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Reprogramar</a></li>
                    <li class="breadcrumb-item active">Nuevo</li>
                </ol>
            </div>
        </div>
    </div>

    <div class="card">
        <div class="card-body">
            <h4 class="card-title">Reprogramar mantenimientos de ATM</h4>
            <h6 class="card-subtitle">Reprogramación mantenimiento preventivo programado en ATM</h6>
            <div class="row col-12">
                <div class="col-12 grid-margin stretch-card">
                    <asp:UpdatePanel runat="server" ID="UPenviarReprogramacion">
                        <ContentTemplate>
                            <div class="row col-12" style="margin: 0px 0px 0px 10px">
                                <div class="row col-12">
                                    <div class="row col-6">
                                        <label class="col-form-label">Mantenimientos pendientes</label>
                                        <div class="row col-12">
                                            <asp:DropDownList ID="dllmantPendientesReprogramado" CssClass="form-control" runat="server">
                                                <asp:ListItem Value="0" Text="Seleccione mantenimiento..."></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="row col-12">
                                    <div class="row col-6">
                                        <label class="col-form-label">Zona</label>
                                        <div class="row col-12">
                                            <asp:TextBox ID="txtzonaReprogramar" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row col-6">
                                        <label class="col-form-label">Departamento</label>
                                        <div class="row col-12">
                                            <asp:TextBox CssClass="form-control" ID="txtdepReprogramar" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row col-12">
                                    <div class="row col-6">
                                        <label class="col-form-label">Fecha de mantenimiento</label>
                                        <div class="row col-12">
                                            <asp:TextBox ID="txtfechaMantReprogramado" Enabled="false" placeholder="1900-12-31" CssClass="form-control" runat="server" TextMode="Date"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row col-6">
                                        <label class="col-form-label">Dirección IP</label>
                                        <div class="row col-12">
                                            <asp:TextBox ID="txtipReprogramado" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row col-12">
                                    <div class="row col-6">
                                        <label class="col-form-label">Lugar</label>
                                        <div class="row col-12">
                                            <asp:TextBox ID="txtlugarReprogramar" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row col-6">
                                        <label class="col-form-label">Código ATM</label>
                                        <div class="row col-12">
                                            <asp:TextBox ID="txtcodATMReprogramar" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row col-12">
                                    <div class="row col-6">
                                        <label class="col-form-label">Ubicación de ATM</label>
                                        <div class="row col-12">
                                            <asp:TextBox ID="txtubicacionATMReprogramar" Enabled="false" TextMode="MultiLine" Rows="2" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row col-6">
                                        <label class="col-form-label">Dirección</label>
                                        <div class="row col-12">
                                            <asp:TextBox ID="txtdireccionMReprogramar" Enabled="false" TextMode="MultiLine" Rows="2" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row col-12">
                                    <div class="row col-6">
                                        <label class="col-form-label">Motivo de cancelación</label>
                                        <div class="row col-12">
                                            <asp:TextBox ID="txtmotivoReprogramado" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row col-6">
                                        <label class="col-form-label">Detalle motivo de cancelación</label>
                                        <div class="row col-12">
                                            <asp:TextBox ID="txtdetMotivoReprogramado" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row col-12">
                                    <div class="row col-6">
                                        <label class="col-form-label">Persona que reprograma</label>
                                        <div class="row col-12">
                                            <asp:TextBox ID="txtpersonaReprograma" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row col-6">
                                        <label class="col-form-label">Nueva fecha de reprogramación</label>
                                        <div class="row col-12">
                                            <asp:TextBox ID="txtfechaNuevaReprogramacion" placeholder="1900-12-31" CssClass="form-control" runat="server" TextMode="Date"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <asp:Button runat="server" OnClick="btnEnviarReprogramacion_Click" ID="btnEnviarReprogramacion" CssClass="btn btn-success" Text="Enviar" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
