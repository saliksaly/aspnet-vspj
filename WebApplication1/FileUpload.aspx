<%@ Page Title="File upload" Language="C#" MasterPageFile="~/Site.Master" 
AutoEventWireup="true" CodeBehind="FileUpload.aspx.cs" Inherits="WebApplication1.FileUpload" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    
    <style>
        div.form-section { margin-top: 10px; }
    </style>
    
    <div class="form-section">
        Uploaded files path: <asp:Label runat="server" ID="lbUploadedFilesPath"></asp:Label>
        <br/>
        Already uploaded files:
        <asp:ListView ID="lvwUploadedFiles" runat="server">
            <LayoutTemplate>
                <ul>
                    <li runat="server" id="itemPlaceholder" />
                </ul>
            </LayoutTemplate>
            <ItemTemplate>
                <li runat="server">
                    <%# Container.DataItem %>
                </li>
            </ItemTemplate>
            <EmptyDataTemplate>none yet</EmptyDataTemplate>
        </asp:ListView>
    </div>
    
    <div class="form-section">
        <asp:FileUpload ID="FileUpload1" runat="server" />
    </div>
    
    <div class="form-section">
        <asp:Button ID="btnUpload" runat="server" Text="Upload file" OnClick="btnUpload_Click" />
    </div>
    
    <div class="form-section">
        <asp:Label id="lbUploadStatus" runat="server"/>
    </div>
    
</asp:Content>