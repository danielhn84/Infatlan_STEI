<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="faqs.aspx.cs" Inherits="Infatlan_STEI.paginas.faqs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
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
                    <li class="breadcrumb-item active">FAQs</li>
                </ol>
            </div>
        </div>
    </div>
    <div class="card">
        <div class="card-body">
            <h4 class="card-title">Preguntas Frecuentes</h4>
            <h6 class="card-subtitle">Preguntas frecuentes para usuarios nuevos.</h6>
            <div class="card-body">
                <h5><b>¿Qué es STEI?</b></h5>
                STEI es un sistema de administración y gestión de multiples procesos realizados por Infatlan para el control de activos internos y externos.

                <h5 class="m-t-20"><b>¿Qué significa STEI?</b></h5>
                Por sus siglas Sistema de Telecomunicaciones E Inventarios

                <h5 class="m-t-20"><b>¿Qué procesos tiene STEI?</b></h5>
                Mantenimientos de Agencias, ATMs, Cableado Estructurado y control de Inventarios.
                
                <h5 class="m-t-20"><b>¿Cómo puedo enviar una notificación?</b></h5>
                Entrando a la sección de Mensajes en la menú de la izquierda, seleccione el destinatario, asunto, aplicación, escriba el mensaje y presione Enviar.

                <h5 class="m-t-20"><b>¿Dónde puedo ver las notificaciones recibidas?</b></h5>
                En la parte superior derecha de la página, en el ícono de sobre cerrado.

                <h5 class="m-t-20"><b>¿Cómo puedo enviar una sugerencia sobre el desarrollo del sistema?</b></h5>
                En el botón de Bugs de la página principal, especifique la aplicación y presione Enviar.
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
