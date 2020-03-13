<%@ Page Title="" Language="C#" MasterPageFile="~/mainATM.Master" AutoEventWireup="true" CodeBehind="reprogramacionATM.aspx.cs" Inherits="Infatlan_STEI_ATM.pagesATM.reprogramacionATM" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    
     <div class="row page-titles">
        <div class="col-md-5 align-self-center">
            <h4 class="text-themecolor">Reprogramar mantenimientos de ATMs</h4>
        </div>
        <div class="col-md-7 align-self-center text-right">
            <div class="d-flex justify-content-end align-items-center">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Inicio</a></li>
                    <li class="breadcrumb-item active">Reprogramar mantenimientos de ATMs</li>
                </ol>
               
            </div> 
        </div>
    </div>
    <!--/ENCABEZADO-->

     <div class="card" >
             <br />
    <div class="row col-12" style="margin-left:10px; margin-left:10px;">
            <h3 class="text-themecolor" style="color: #808080;"><i class="fa fa-calendar"></i> Reprogramación mantenimiento preventivo programado en ATM</h3>
        </div>    
             <br />
         <asp:UpdatePanel runat="server" ID="UPenviarReprogramacion">
             <ContentTemplate>
           <div class="row col-12" style="margin:10px 10px 10px 10px"> 
               <label class="col-form-label col-12">Mantenimientos pendientes a reprogramar</label>
               <div class="row col-6">
                    <asp:DropDownList ID="dllmantPendientesReprogramado" CssClass="form-control" runat="server" >
                       <asp:ListItem Value="0" Text="Seleccione mantenimiento..."></asp:ListItem>
                   </asp:DropDownList>
               </div>
               <br />

               <div class="row col-12">
                   <div class="row col-6">
                       <label class="col-form-label col-6">Zona</label>
                       <div class="row col-12">
                          <asp:TextBox ID="txtzonaReprogramar" Enabled="false"  cssclass="form-control" runat="server"></asp:TextBox>
                       </div>
                   </div>
                    <div class="row col-6">
                       <label class="col col-form-label col-6">Departamento</label>
                       <div class="row col-12">
                           <asp:TextBox CssClass="form-control" ID="txtdepReprogramar" runat="server" Enabled="false"></asp:TextBox> 
                       </div>
                   </div>
                   </div>
               <div class="row col-12">
                   <div class="row col-6">
                       <label class="col col-form-label col-12">Fecha de mantenimiento</label>
                       <div class="row col-12">
                           <asp:TextBox ID="txtfechaMantReprogramado" Enabled="false" placeholder="1900-12-31" cssclass="form-control" runat="server" TextMode="Date"></asp:TextBox>
                       </div>
                   </div>
                    <div class="row col-6">
                       <label class="col-form-label col-6">Dirección IP</label>
                       <div class="row col-12">
                           <asp:TextBox ID="txtipReprogramado" Enabled="false"  cssclass="form-control" runat="server"></asp:TextBox>
                       </div>
                   </div>
               </div>
                <div class="row col-12">
                   <div class="row col-6">
                       <label class="col col-form-label col-12">Lugar</label>
                       <div class="row col-12">
                           <asp:TextBox ID="txtlugarReprogramar" Enabled="false"  cssclass="form-control" runat="server"></asp:TextBox>
                       </div>
                   </div>
                    <div class="row col-6">
                       <label class="col-form-label col-6">Código ATM</label>
                       <div class="row col-12">
                           <asp:TextBox ID="txtcodATMReprogramar" Enabled="false"  cssclass="form-control" runat="server"></asp:TextBox>
                       </div>
                   </div>
               </div>
               <div class="row col-12">
                   <div class="row col-6">
                       <label class="col col-form-label col-12">Ubicación de ATM</label>
                       <div class="row col-12">
                           <asp:TextBox ID="txtubicacionATMReprogramar" Enabled="false" TextMode="MultiLine" Rows="2"  cssclass="form-control" runat="server"></asp:TextBox>
                       </div>
                   </div>
                    <div class="row col-6">
                       <label class="col-form-label col-6">Dirección</label>
                       <div class="row col-12">
                           <asp:TextBox ID="txtdireccionMReprogramar" Enabled="false" TextMode="MultiLine" Rows="2" cssclass="form-control" runat="server"></asp:TextBox>
                       </div>
                   </div>
               </div>
                 <div class="row col-12">
                   <div class="row col-6">
                       <label class="col col-form-label col-12">Motivo de cancelación</label>
                       <div class="row col-12">
                           <asp:TextBox ID="txtmotivoReprogramado" Enabled="false"  cssclass="form-control" runat="server"></asp:TextBox>
                       </div>
                   </div>
                    <div class="row col-6">
                       <label class="col-form-label col-6">Detalle motivo de cancelación</label>
                       <div class="row col-12">
                           <asp:TextBox ID="txtdetMotivoReprogramado"  Enabled="false"  cssclass="form-control" runat="server"></asp:TextBox>
                       </div>
                   </div>
               </div>
                <div class="row col-12">
                   <div class="row col-6">
                       <label class="col col-form-label col-12">Persona que reprograma</label>
                       <div class="row col-12">
                           <asp:TextBox ID="txtpersonaReprograma" Enabled="false" cssclass="form-control" runat="server"></asp:TextBox>
                       </div>
                   </div>
                    <div class="row col-6">
                       <label class="col-form-label col-6">Nueva fecha de reprogramación</label>
                       <div class="row col-12">
                           <asp:TextBox ID="txtfechaNuevaReprogramacion" placeholder="1900-12-31" cssclass="form-control" runat="server" TextMode="Date"></asp:TextBox>
                       </div>
                   </div>
               </div>
               </div>
         <br />
         <br />
         
                 
                     <asp:Button runat="server" OnClick="btnEnviarReprogramacion_Click" ID="btnEnviarReprogramacion" CssClass="btn btn-rounded btn-block btn-outline-success align-self-center col-6" Text="Enviar" />
                 
             </ContentTemplate>
         </asp:UpdatePanel>
         <br />
         </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
