<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DescargaAPK.aspx.vb" Inherits="DescargaAPK" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Download</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="styles/ObjHtmlNoS.css" rel="stylesheet" />
    <script src="scripts/Bootstapcdn.js"></script>
    <link href="styles/Boot.css" rel="stylesheet" />
    <link href="styles/General.css" rel="stylesheet" />
    <link href="styles/footable.min.css" rel="stylesheet" />
    <script src="scripts/jquery.min.js"></script>
</head>
<body style="background-color: white;">
    <form id="form1" runat="server">
        <div>
            <nav class="navbar navbar-inverse">
                <div class="container-fluid">
                    <div class="navbar-header">
                        <a class="navbar-brand" href="#">MC Móvil</a>
                    </div>
                    <ul class="nav navbar-nav">
                        <li><a href="Login.aspx">Salir</a></li>
                    </ul>
                </div>
            </nav>
        </div>
        <div class="container">
            <link href="styles/HTML.css" rel="stylesheet" />
            <table>
                <tr class="Titulos">
                    <td>Aplicación Movil</td>
                    <td>&nbsp;</td>
                    <td>Aplicacion Movil V2</td>
                    <td></td>
                    <td>Manual de Usuario e instalación</td>
                    <td>&nbsp;</td>
                    <td>Manual De Re-instalación </td>
                </tr>
                <tr style="text-align: center">
                    <td>
                        <%--<a href="https://play.google.com/store/apps/details?id=com.gabssa">
                            <asp:Image ID="ImgApp" runat="server" ImageUrl="Images/Img_Download_APK.png" Height="100px" />
                        </a>--%>
                        <asp:ImageButton ID="ImgApp" runat="server" ImageUrl="Images/APK.png" Height="100px" />
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        <asp:ImageButton ID="ImgApp2" runat="server" ImageUrl="Images/APK.png" Height="100px" />
                    </td>
                    <td></td>
                    <td>
                        <asp:ImageButton ID="ImageManual" runat="server" ImageUrl="Images/ImgManual.png" Height="100px" />
                    </td>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                    <td>
                        <asp:ImageButton ID="ImageManual0" runat="server" ImageUrl="Images/ImgManual.png" Height="100px" />
                    </td>
                </tr>
                <tr style="text-align: center">
                    <td>
                        <asp:Label ID="lbmsjapk" runat="server" CssClass="LblDescNS"></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>&nbsp;&nbsp;&nbsp;<asp:Label ID="lbmsjapk2" runat="server" CssClass="LblDescNS"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Label ID="lbmsjmanual" runat="server" CssClass="LblDescNS"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Label ID="lbmsjmanual0" runat="server" CssClass="LblDescNS"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
