<%@ Page Title="CSV file download" Language="C#" MasterPageFile="~/Site.Master" 
AutoEventWireup="true" CodeBehind="CsvFile.aspx.cs" Inherits="WebApplication1.CsvFile" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    
    <style>
        div.form-section { margin-top: 10px; }
    </style>

    <div class="form-section">
        Tabulka v databázi: 
        <asp:DropDownList runat="server" ID="dlDbTables">
            <asp:listitem text="Message" value="Message"></asp:listitem>
            <asp:listitem text="ServisniOrganizace" value="ServisniOrganizace"></asp:listitem>
        </asp:DropDownList>
    </div>
    
    <div class="form-section">
        Jméno souboru:
        <asp:TextBox runat="server" ID="tbFileName" Text="Exportovaný CSV soubor.csv" Width="197px"></asp:TextBox>
    </div>
    
    <div class="form-section">
        <asp:Button ID="btnDownload" runat="server" Text="Download" OnClick="btnDownload_OnClick" />
    </div>
</asp:Content>
