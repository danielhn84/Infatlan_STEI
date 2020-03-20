<%@ Page Title="" Language="C#" MasterPageFile="~/mainATM.Master" AutoEventWireup="true" CodeBehind="PRUEBAIMG.aspx.cs" Inherits="Infatlan_STEI_ATM.PRUEBAIMG" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <script type="text/javascript">
        //IMAGEN1
        function img1(input) {

            if (input.files && input.files[0]) {
                //PRIMERA IMAGEN
                var reader = new FileReader();
                reader.onload = function (e) {                  
                    $('#imgDiscoDuro').css('visibility', 'visible');
                    $('#imgDiscoDuro').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
                 //PRIMERA IMAGEN              
            }
        }
        //IMAGEN1
        </script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
        <ContentTemplate>   
<%--<img id="imgATMDesarmadoPS" height="300" width="300" src="" style="border-width: 0px; visibility: hidden;" />
                        <asp:FileUpload ID="FUATMDesarmadoPS" runat="server" onchange="img2(this);" />--%>
   
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
 <asp:Button runat="server" ID="btnguardar" OnClick="btnguardar_Click" />
        </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnguardar" />
                </Triggers>
    </asp:UpdatePanel>




     <div class="row">

         <asp:Label runat="server" ID="lbimg"></asp:Label>

                    <div class="col-12">
                        <!-- Column -->
                        <div class="card">
                            <div class="card-body">
                                <h3 class="card-title" style="color: #808080;"><i class="fa fa-image" style="margin-left: 10px"></i>Comprobación</h3>                               
                                <h5 class="card-subtitle">Subir imagenes de lo solicitado de mantenimiento</h5>
                                <table class="tablesaw table-bordered table-hover table no-wrap" data-tablesaw-mode="swipe"
                                    data-tablesaw-sortable data-tablesaw-sortable-switch data-tablesaw-minimap
                                    data-tablesaw-mode-switch>
                                    <thead>
                                        <tr>
                                            <th scope="col" data-tablesaw-sortable-col data-tablesaw-priority="persist" class="border">
                                                Imagen a subir</th>
                                            <th scope="col" data-tablesaw-sortable-col data-tablesaw-sortable-default-col
                                                data-tablesaw-priority="3" class="border">¿Subir imagen?</th>
                                            <th scope="col" data-tablesaw-sortable-col data-tablesaw-priority="2" class="border">Seleccione imagen
                                            </th>
                                            <th scope="col" data-tablesaw-sortable-col data-tablesaw-priority="1" class="border">
                                                <abbr title="Rotten Tomato Rating">Mostrar imagen</abbr>
                                            </th>
                                            
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td class="title"><a class="link" href="javascript:void(0)">Disco duro</a></td>
                                            <td>   
                                                <asp:RadioButtonList ID="RBLDiscoDuro" runat="server" CssClass="custom-checkbox"  BorderStyle="None" Enabled="false" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="No"></asp:ListItem>                  
                                                </asp:RadioButtonList>
                                            </td>
                                            <td><asp:FileUpload ID="FUDiscoDuro" runat="server" onchange="img1(this);" /></td>
                                            <td><img id="imgDiscoDuro" height="100" width="100" src="" style="border-width: 0px; visibility: hidden;" /></td>
                                            
                                        </tr>
                                        <tr>
                                            <td class="title"><a class="link" href="javascript:void(0)">ATM desarmado parte superior (limpiar)</a></td>
                                            <td>
                                                <asp:RadioButtonList ID="RBLATMDesarmadoPS" runat="server" CssClass="custom-checkbox"  BorderStyle="None" Enabled="false" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="No"></asp:ListItem>                  
                                                </asp:RadioButtonList>
                                            </td>
                                            <td> <asp:FileUpload ID="FUATMDesarmadoPS" runat="server" onchange="img2(this);" /></td>
                                            <td><img id="imgATMDesarmadoPS" height="100" width="100" src="" style="border-width: 0px; visibility: hidden;" /></td>
                                            
                                        </tr>
                                        <tr>
                                            <td class="title"><a class="link" href="javascript:void(0)">ATM desarmado parte inferior (limpiar)</a>
                                            </td>
                                            <td>
                                                 <asp:RadioButtonList ID="RBLATMDesarmadoPI" runat="server" CssClass="custom-checkbox"  BorderStyle="None" Enabled="false" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="No"></asp:ListItem>                  
                                                </asp:RadioButtonList>
                                            </td>
                                            <td><asp:FileUpload ID="FUATMDesarmadoPI" runat="server" onchange="img3(this);" /></td>                     
                                            <td><img id="imgATMDesarmadoPI" height="100" width="100" src="" style="border-width: 0px; visibility: hidden;" /></td>
                                            
                                        </tr>
                                        <tr>
                                            <td class="title"><a class="link" href="javascript:void(0)">Dispositivo modo <br />diagnostico de vendor en linea
</a></td>
                                            <td>
                                                <asp:RadioButtonList ID="RBLVendor" runat="server" CssClass="custom-checkbox"  BorderStyle="None" Enabled="false" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="No"></asp:ListItem>                  
                                                </asp:RadioButtonList>
                                            </td>
                                            <td> <asp:FileUpload ID="FUDispositivoVendor" runat="server" onchange="img4(this);" /></td>                 
                                            <td><img id="imgDispositivoVendor" height="100" width="100" src="" style="border-width: 0px; visibility: hidden;" /></td>
                                            
                                        </tr>
                                        <tr>
                                            <td class="title"><a class="link" href="javascript:void(0)">Tipo de procesador con<br /> el comando "SYSTEMINFO"</a></td>
                                            <td>
                                                <asp:RadioButtonList ID="RBLSystemInfo" runat="server" CssClass="custom-checkbox"  BorderStyle="None" Enabled="false" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="No"></asp:ListItem>                  
                                                </asp:RadioButtonList>
                                            </td>
                                            <td><asp:FileUpload ID="FUSYSTEMINFO" runat="server" onchange="img5(this);" /></td>                       
                                            <td><img id="imgSYSTEMINFO" height="100" width="100" src="" style="border-width: 0px; visibility: hidden;" /></td>
                                            
                                        </tr>
                                        <tr>
                                            <td class="title"><a class="link" href="javascript:void(0)">Lectora con el antiskimming <br />desarmado y limpio</a>
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="RBLAntiSkimming" runat="server" CssClass="custom-checkbox"  BorderStyle="None" Enabled="false" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="No"></asp:ListItem>                  
                                                </asp:RadioButtonList>
                                            </td>
                                            <td><asp:FileUpload ID="FUAntiskimmin" runat="server" onchange="img6(this);" /></td>                        
                                            <td><img id="imgAntiskimmin" height="100" width="100" src="" style="border-width: 0px; visibility: hidden;" /></td>
                                            
                                        </tr>
                                        <tr>
                                            <td class="title"><a class="link" href="javascript:void(0)">Monitor con el filtro
                                                    </a></td>
                                            <td> 
                                                <asp:RadioButtonList ID="RBLMonitorFiltro" runat="server" CssClass="custom-checkbox"  BorderStyle="None" Enabled="false" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="No"></asp:ListItem>                  
                                                </asp:RadioButtonList>
                                            </td>
                                            <td> <asp:FileUpload ID="FUMonitorFiltro" runat="server" onchange="img7(this);" /></td>                        
                                            <td><img id="imgMonitorFiltro" height="100" width="100" src="" style="border-width: 0px; visibility: hidden;" /></td>
                                            
                                        </tr>
                                        <tr>
                                            <td class="title"><a class="link" href="javascript:void(0)">PadleWheel(rueda de paletas)</a></td>
                                            <td>
                                                 <asp:RadioButtonList ID="RBLPadleWheel" runat="server" CssClass="custom-checkbox"  BorderStyle="None" Enabled="false" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="No"></asp:ListItem>                  
                                                </asp:RadioButtonList>
                                            </td>
                                            <td><asp:FileUpload ID="FUPadlewheel" runat="server" onchange="img8(this);" /></td>                        
                                            <td> <img id="imgPadlewheel" height="100" width="100" src="" style="border-width: 0px; visibility: hidden;" /></td>
                                            
                                        </tr>
                                        <tr>
                                            <td class="title"><a class="link" href="javascript:void(0)">Dispositivos desarmado</a></td>
                                            <td>
                                                <asp:RadioButtonList ID="RBLDispDesarmado" runat="server" CssClass="custom-checkbox"  BorderStyle="None" Enabled="false" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="No"></asp:ListItem>                  
                                                </asp:RadioButtonList>
                                            </td>
                                            <td><asp:FileUpload ID="FUDispDesarmado" runat="server" onchange="img9(this);" /></td>       
                                            <td><img id="imgDispDesarmado" height="100" width="100" src="" style="border-width: 0px; visibility: hidden;" /></td>
                                            
                                        </tr>
                                        <tr>
                                            <td class="title"><a class="link" href="javascript:void(0)">Teclado
                                                   </a></td>
                                            <td>
                                                 <asp:RadioButtonList ID="RBLTeclado" runat="server" CssClass="custom-checkbox"  BorderStyle="None" Enabled="false" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="No"></asp:ListItem>                  
                                                </asp:RadioButtonList>
                                            </td>
                                            <td><asp:FileUpload ID="FUTeclado" runat="server" onchange="img10(this);" /></td>                      
                                            <td><img id="imgTeclado" height="100" width="100" src="" style="border-width: 0px; visibility: hidden;" /></td>
                                            
                                        </tr>
                                         <tr>
                                            <td class="title"><a class="link" href="javascript:void(0)">¿Cuenta con Climatización adecuada?
                                                   </a></td>
                                            <td>
                                                 <asp:RadioButtonList ID="RBLClimatizacion" runat="server" AutoPostBack="true" CssClass="custom-checkbox"  BorderStyle="None" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="No"></asp:ListItem>                  
                                                </asp:RadioButtonList>
                                            </td>                                
                                            <td><asp:FileUpload ID="FUClimatizacion" runat="server" onchange="img11(this);" /></td>                      
                                            <td><img id="imgClimatizacion" height="100" width="100" src="" style="border-width: 0px; visibility: hidden;" /></td>
                                            
                                        </tr>
                                         <tr>
                                            <td class="title"><a class="link" href="javascript:void(0)">¿Cuenta con protección de energía<br /> eléctrica?
                                                   </a></td>
                                            <td>
                                                 <asp:RadioButtonList ID="RBLEnergiaElectrica" runat="server" AutoPostBack="true" CssClass="custom-checkbox"  BorderStyle="None" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="No"></asp:ListItem>                  
                                                </asp:RadioButtonList>
                                            </td>                                
                                            <td><asp:FileUpload ID="FUEnergia" runat="server" onchange="img12(this);" /></td>                                                      
                                            <td><img id="imgEnergia" class="col row-6" height="100" width="100" src="" style="border-width: 0px; visibility: hidden;" /></td>
                                            
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                       </div>
         </div>
    
    
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
