<%@ Page Title="" Language="C#" MasterPageFile="~/mainAgencia.Master" AutoEventWireup="true" CodeBehind="defaultAgencia.aspx.cs" Inherits="Infatlan_STEI_Agencias.pages.defaulAgencia" %>

<%@ Register Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <style>
        .hatm{
            color: #A9A9F5;
        }
       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row page-titles">
        <div class="col-md-5 align-self-center">
            <h4 class="text-themecolor">Completar Lista de Mantenimiento de ATMs</h4>
        </div>
        <div class="col-md-7 align-self-center text-right">
            <div class="d-flex justify-content-end align-items-center">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Inicio</a></li>
                    <li class="breadcrumb-item active">Completar Lista de Mantenimiento de ATMs</li>
                </ol>
               
            </div>
        </div>
    </div>
   
         <div class="card" >
             <br />
    <div class="row col-12" style="margin-left:10px; margin-left:10px;">
            <h3 class="text-themecolor" style="color: #808080;"><i class="fa fa-book"></i> Datos Generales</h3>
        </div>    
             <br />
           <div class="row col-12" style="margin:10px 10px 10px 10px">
              <!--PRIMERA FILA-->
                <div class="row col-12">
               <div class="row col-6">        
                <label Class="col-form-label col-6">Zona</label>
                   <div class="row col-12">
                      <asp:UpdatePanel runat="server" ID="PANEL1">
                           <ContentTemplate> 
                   <asp:DropDownList ID="dropzonaATM" AutoPostBack="true" OnSelectedIndexChanged="dropzonaATM_SelectedIndexChanged" CssClass="form-control" runat="server" >
                       <asp:ListItem Value="0" Text="Seleccione zona..."></asp:ListItem>
                       <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                       <asp:ListItem Value="2" Text="No"></asp:ListItem>
                   </asp:DropDownList>
                              
                       <asp:TextBox runat="server" ID="txtprueba" Enabled="false"></asp:TextBox>
                                </ContentTemplate>
                           </asp:UpdatePanel>
                       </div>
               </div>
             
               <div class="row col-6">          
                <label class=" col-form-label col-6">Coordinador de Mantenimiento</label>
                   <div class="row col-12">
               <asp:DropDownList runat="server" ID="dropcoordinador" CssClass="form-control">
                   <asp:ListItem Value="0" Text="Seleccione el coordinador..."></asp:ListItem>
               </asp:DropDownList>
                       </div>
               </div>
               </div>
               <!--/PRIMERA FILA-->
               <!--SEGUNDA FILA-->
               <div class="row col-12">
                   <div class="row col-6">
                       <label class="col col-form-label col-6">Hora Salida de Infatlan</label>
                       <div class="row col-12">
                           <asp:TextBox ID="txthsalidaInfa" placeholder="00:00:00" class="form-control" runat="server" TextMode="Time"></asp:TextBox>
                       </div>
                   </div>
                   <div class="row col-6">
                       <label class="col col-form-label col-6">Hora llegada a Infatlan</label>
                       <div class="row col-12">
                             <asp:TextBox ID="txtHllegadaInfatlan" placeholder="00:00:00" class="form-control" runat="server" TextMode="Time"></asp:TextBox>
                       </div>
                   </div>
               </div>
               <!--/SEGUNDA FILA-->
               <!--TERCERA FILA-->
    <div class="row col-12">
        <div class="row col-6">
         
                <label Class="col-form-label col-6">Inicio de mantenimiento</label>
                <div class="row col-12">
                    <asp:TextBox ID="TxFechaInicio" placeholder="1900-12-31 00:00:00" class="form-control" runat="server" TextMode="DateTimeLocal"></asp:TextBox>              
            </div>
        </div>
        <div class="row col-6">          
                <label class=" col-form-label col-6">Finaliza mantenimiento</label>
                <div class="row col-12">
                    <asp:TextBox ID="TxFechaRegreso" placeholder="1900-12-31 00:00:00" class="form-control" runat="server" TextMode="DateTimeLocal"></asp:TextBox>
                </div>        
        </div>
    </div>
    <!--FIN TERCERA FILA-->
               <!--CUARTA FILA-->
               <div class="row col-12">
                   <div class="row col-6">
                       <label class="col-form-label col-6">Lugar</label>
                       <div class="row col-12">
                           <asp:DropDownList runat="server" ID="droplugar" CssClass="form-control">
                               <asp:ListItem Value="0" Text="Seleccione el lugar..."></asp:ListItem>
                           </asp:DropDownList>
                       </div>
                   </div>
                   <div class="row col-6">
                       <label class="col-form-label col-6">SysAid</label>
                       <div class="row col-12">
                           <asp:TextBox CssClass="form-control" ID="txtsysaid" runat="server"></asp:TextBox>
                       </div>
                   </div>
               </div>
               <!--/CUARTA FILA-->
                 <!--QUINTA FILA-->
               <div class="row col-12">
                   <div class="row col-6">
                        <label class="col-form-label col-6">Ubicación de ATM</label>
                       <div class="row col-12">
                           <asp:TextBox CssClass="form-control" ID="txtUbicacionATM" runat="server" Enabled="false"></asp:TextBox>
                       </div>
                   </div>
                   <div class="row col-6">
                       <label class="col-form-label col-6">Código de ATM</label>
                       <div class="row col-12">
                           <asp:TextBox CssClass="form-control" ID="txtcodATM" runat="server" Enabled="false"></asp:TextBox>
                       </div>
                   </div>
               </div>
               <!--/QUINTA FILA-->
               <!--SEXTA FILA-->
               <div class="row col-12">
                   <div class="row col-6">
                        <label class="col-form-label col-6">Dirección</label>
                       <div class="row col-12">
                           <asp:TextBox CssClass="form-control" ID="txtdireccion" runat="server" Enabled="false"></asp:TextBox>
                       </div>
                   </div>
                   <div class="row col-6">
                       <label class="col-form-label col-6">Departamento</label>
                       <div class="row col-12">
                           <asp:TextBox CssClass="form-control" ID="txtdepto" runat="server" Enabled="false"></asp:TextBox>
                       </div>
                   </div>
               </div>
               <!--/SEXTA FILA-->
                <!--SEPTIMA FILA-->
               <div class="row col-12">
                   <div class="row col-6">
                        <label class="col-form-label col-6">Dirección IP</label>
                       <div class="row col-12">
                           <asp:TextBox CssClass="form-control" ID="txtip" runat="server" Enabled="false"></asp:TextBox>
                       </div>
                   </div>
                   <div class="row col-6">
                       <label class="col-form-label col-6">Motivo</label>
                       <div class="row col-12">
                           <asp:TextBox CssClass="form-control" ID="txtmotivo" runat="server" Enabled="false" Text="Realizar acciones pro activas para prevenir la falla"></asp:TextBox>
                       </div>
                   </div>
               </div>
               <!--/SEPTIMA FILA-->
                <!--OCTAVA FILA-->
               <div class="row col-12">
                 
                        <label class="col-form-label col-6">Impacto</label>
                       <div class="row col-12">
                           <asp:TextBox CssClass="form-control" ID="txtimpacto" runat="server" Enabled="false" Text="Durante la ventana de mantenimiento el ATM estará fuera de linea"></asp:TextBox>
                       </div>
                   </div>                            
               <!--/OCTAVA FILA-->                              
         </div>
             <br />
               <br />
             <div class="row col-12" style="margin-left:10px; margin-left:10px;">
            <h3 class="text-themecolor" style="color: #808080;"><i class="fa fa-user" style=" margin-left:10px"></i> Técnico Responsable</h3>
        </div>
             <br />            
              <div class="row col-12" style="margin:10px 10px 10px 10px">
              <!--PRIMERA FILA-->
                <div class="row col-12">
               <div class="row col-6">        
                <label Class="col-form-label col-6">Zona</label>
                   <div class="row col-12">
                   <asp:DropDownList ID="DropDownList1" CssClass="form-control" runat="server" >
                       <asp:ListItem Value="0" Text="Seleccione técnico..."></asp:ListItem>
                   </asp:DropDownList>
                       </div>
               </div>
             
               <div class="row col-6">          
                <label class=" col-form-label col-6">Identidad</label>
                   <div class="row col-12">
                <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" Enabled="false"></asp:TextBox>
                       </div>
               </div>
               </div>
                  </div>
               <!--/PRIMERA FILA-->
             <br />
              <div class="col-md-4 align-self-center">
             <asp:Button runat="server" ID="btnEnviarNotificacion" CssClass="btn btn-rounded btn-block btn-outline-success" Text="Enviar"/>
              </div>
              <br />
             <br />
             </div>
   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>

