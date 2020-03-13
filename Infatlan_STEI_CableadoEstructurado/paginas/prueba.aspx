<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="prueba.aspx.cs" Inherits="Infatlan_STEI_CableadoEstructurado.paginas.prueba" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">




     <asp:GridView  ID="GVContabilidad" runat="server"
        AutoGenerateColumns="False" DataKeyNames="Id, Type" 
        OnRowCancelingEdit="GVContabilidad_RowCancelingEdit" 
        OnRowDataBound="GVContabilidad_RowDataBound" 
        OnRowEditing="GVContabilidad_RowEditing" 
        OnRowUpdating="GVContabilidad_RowUpdating" 
        OnRowDeleting="GVContabilidad_RowDeleting" 
        OnRowCommand="GVContabilidad_RowCommand">

        <Columns>
            <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left">
                <EditItemTemplate>
                    <%--<asp:TextBox ID="txtName" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>--%>
                    <asp:TextBox ID="txtName" runat="server" Tex="Hola"></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtNewName" runat="server"></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblName" runat="server" Tex="Hola2"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>




</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
