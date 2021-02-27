<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PrintGrafica.aspx.vb" Inherits="M_Reportes_PrintGrafica" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../Estilos/w3.css" rel="stylesheet" />
    <link href="../Estilos/bootstrap.min.css" rel="stylesheet" />
    <script src="../Scripts/jQuery.min.js"></script>
    <script src="../Scripts/popper.min.js?v=1"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager runat="server"></telerik:RadScriptManager>
        <telerik:RadSkinManager runat="server" Skin="Bootstrap"></telerik:RadSkinManager>
        <%-- El el div de abajo hay que insertar la gráfica --%>
        <asp:label runat="server" id="lblReporteListo" text="0"></asp:label>
        <div class="d-flex justify-content-between mb-2">
            <img alt="Cliente" src="../Imagenes/ImgLogo_Cliente.png" class="img-fluid" style="max-height:75px"/>
            <div style="max-width:75%" class="text-center">
                <h3><asp:label runat="server" ID="lblReporte" Text="Nombre del reporte"></asp:label></h3>
                <p><asp:label runat="server" ID="lblDescripcion" Text="Reporte sin descripción"></asp:label></p>
            </div>
            <img alt="MC Collect" src="../Imagenes/mccollect_logo.png" class="img-fluid" style="max-height:75px"/>
        </div>
        <div class="container">
            <telerik:RadHtmlChart runat="server" ID="PrintChart"></telerik:RadHtmlChart>
        </div>
        <telerik:RadScriptBlock runat="server">
            <script>
                $(document).ready(() => {
                    console.log("ready!");
                })
            </script>
        </telerik:RadScriptBlock>
    </form>
</body>
</html>
