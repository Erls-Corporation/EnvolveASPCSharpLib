<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EnvolveDemo.DefaultWebForm" %>
<%@ Register src="Envolve.ascx" tagname="Envolve" tagprefix="envolve" %>
<%@ Register src="EnvolveEmbeddedChat.ascx" tagname="EnvolveEmbeddedChat" tagprefix="envolve" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form" runat="server">
    <div>
        <envolve:EnvolveEmbeddedChat ID="EnvolveEmbeddedChat" Name="Embedded Chat" Tag="embedded" 
            Style="width: 330px; height: 400px; border: 2px solid green;" runat="server" />
        <envolve:Envolve ID="Envolve" APIKey="24829-3AYdg7PNS0Ob1a41YaqGCxSzDeKspFB2" runat="server" />
    </div>
    </form>
</body>
</html>
