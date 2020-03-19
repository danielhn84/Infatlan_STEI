<%@ Page Title="" Language="C#" MasterPageFile="~/mainATM.Master" AutoEventWireup="true" CodeBehind="PRUEBAIMG.aspx.cs" Inherits="Infatlan_STEI_ATM.PRUEBAIMG" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <script type="text/javascript">
        //IMAGEN1
        function img2(input) {

            if (input.files && input.files[0]) {
                //PRIMERA IMAGEN
                var reader = new FileReader();
                reader.onload = function (e) {                  
                    $('#imgATMDesarmadoPS').css('visibility', 'visible');
                    $('#imgATMDesarmadoPS').attr('src', e.target.result);
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
<img id="imgATMDesarmadoPS" height="300" width="300" src="" style="border-width: 0px; visibility: hidden;" />
                        <asp:FileUpload ID="FUATMDesarmadoPS" runat="server" onchange="img2(this);" />
   
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
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
