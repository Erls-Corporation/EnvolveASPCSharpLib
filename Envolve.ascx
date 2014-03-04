<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Envolve.ascx.cs" Inherits="EnvolveAPI.EnvolveControl" %>

<script type="text/javascript">
    <asp:Literal ID="embeddedJavascript" runat="server" />
    var envProtoType = (("https:" == document.location.protocol) ? "https://" : "http://");
    document.write(unescape("%3Cscript src='" + envProtoType + "d.envolve.com/env.nocache.js' type='text/javascript'%3E%3C/script%3E"));
</script>
