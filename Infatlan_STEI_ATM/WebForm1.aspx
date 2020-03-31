<%@ Page Title="" Language="C#" MasterPageFile="~/mainATM.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Infatlan_STEI_ATM.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <asp:TextBox ID="txtcoreoBody" runat="server"></asp:TextBox>
    <asp:Button ID="btncorreo" runat="server" OnClick="btncorreo_Click" Text="Button" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
