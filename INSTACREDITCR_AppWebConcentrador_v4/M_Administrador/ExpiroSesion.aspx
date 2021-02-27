<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ExpiroSesion.aspx.vb" Inherits="ExpiroSesion"
    UICulture="es" Culture="es-MX" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Estilos/HTML.css" rel="stylesheet" />
    <link href="Estilos/Modal.css" rel="stylesheet" />
    <link href="Estilos/ObjHtmlS.css" rel="stylesheet" />
      <script languaje="javascript" type="text/javascript">

        function confirmCallbackFn(arg) {
            if (arg) {

                __doPostBack('BtnAceptarConfirmacion', '')

            }
        }
    </script>
</head>
<body>
    
    <form id="form1" runat="server">
        <telerik:RadScriptManager runat="server" ID="Rsm1"></telerik:RadScriptManager>
        <div class="Pagina">
            <table class="Table">
                <tr>
                    <td class="Izquierda" rowspan="2">
                        <asp:Image ID="ImgLogo_Ca" runat="server" ImageUrl="~/M_Administrador/Imagenes/ImgLogo_Ca.png" Width="40%"/>
                    </td>
                    <td class="TituloP" rowspan="2">Administrador
                    </td>
                    <td class="Derecha" style="width:30%">
                        <asp:Image ID="ImgLogo_Cl" runat="server" ImageUrl="~/M_Administrador/Imagenes/ImgLogo_Cl.png" Width="45%"/>
                    </td>
                </tr>
            </table>
            <div class="Espacio">
            </div>
        </div>
        <telerik:RadWindowManager ID="RadAviso" runat="server" >
            <Localization OK="Aceptar" />
        </telerik:RadWindowManager>
      <asp:Button runat="server" ID="BtnAceptarConfirmacion" Visible="true" Height="1px" Text="Hola" /><%--Style="display: none;--%>
    </form>
</body>
</html>
