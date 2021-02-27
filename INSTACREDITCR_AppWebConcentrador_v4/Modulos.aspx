<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Modulos.aspx.vb" Inherits="Modulos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <link href="Estilos/Modulos.css" rel="stylesheet" />
    <link href="Estilos/General.css" rel="stylesheet" />
    <link href="Estilos/w3.css" rel="stylesheet" />
    <link href="Estilos/bootstrap.min.css" rel="stylesheet" />
    <title>MCCollect:Módulos</title>
</head>
<body>
    <form id="FrmModulos" runat="server">
        <!-- Navbar -->
        <div class="row w3-padding" style="height: 60px">
            <div class="col-2 w3-hide-small">
                <img src="Imagenes/ImgLogo_Cliente.png" alt="MC Collect S.A. de C.V." class="img-fluid" style="max-height: 50px;" />
            </div>
            <div class="col-7 w3-xlarge w3-center">
                <span class="font-weight-light">MC</span><span class="font-weight-bold">Collect</span>
            </div>
            <div class="col-2">
                <asp:Button runat="server" ID="btnCerrarSesion" Text="Cerrar sesión" CssClass="w3-red w3-text-white btn w3-hover-shadow"></asp:Button>
            </div>
            <div class="col-1 w3-hide-small">
                <img src="Imagenes/ImgLogo_Mc.png" alt="MC Collect S.A. de C.V." style="max-height: 50px;" class="img-fluid w3-right" />
            </div>
        </div>
        <br />

        <asp:Panel runat="server" ID="pnlModulos" CssClass="w3-container w3-center">
            <h2><%# saludo() & ", " & tmpUSUARIO("CAT_LO_NOMBRE") %>. Seleccione un módulo para comenzar</h2>
            <br />
            <br />
            <div class="row p-0 m-0">
                <div class="col mc-container" style='display:<%# iif(tmpUsuario("CAT_PE_A_ADMINISTRADOR"),"initial","none")%>'>
                    <div class="mc-middle">
                        <span class="w3-center m-auto font-weight-bold" >Administrador</span>
                    </div>
                    <asp:ImageButton runat="server" ID="imgAd" src="Imagenes/Imglogo_Ad.png" alt="Avatar" style="width: 100%; height: 500px; object-fit: cover;" cssclass="mc-image" OnClick="imgAd_Click"/>
                </div>
                <div class="col mc-container" style='display:<%# iif(tmpUsuario("CAT_PE_A_BACKOFFICE"),"initial","none")%>'>
                    <div class="mc-middle">
                        <span class="w3-center m-auto font-weight-bold">Back Office</span>
                    </div>
                    <asp:ImageButton runat="server" ID="imgBo" src="Imagenes/Imglogo_Bo.png" alt="Avatar" style="width: 100%; height: 500px; object-fit: cover;" cssclass="mc-image" OnClick="imgBo_Click"/>
                </div>
                <div class="col mc-container" style='display:<%# iif(tmpUsuario("CAT_PE_A_GESTION").toString.Contains("1"),"initial","none")%>'>
                    <div class="mc-middle">
                        <span class="w3-center m-auto font-weight-bold">Gestión</span>
                    </div>
                    <asp:ImageButton runat="server" ID="imgGe" src="Imagenes/Imglogo_Ge.png" alt="Avatar" style="width: 100%; height: 500px; object-fit: cover;" cssclass="mc-image" OnClick="imgGe_Click"/>
                </div>
                <div class="col mc-container" style='display:<%# iif(tmpUsuario("CAT_PE_A_REPORTES"),"initial","none")%>'>
                    <div class="mc-middle">
                        <span class="w3-center m-auto font-weight-bold">Reportes</span>
                    </div>
                    <asp:ImageButton runat="server" ID="imgRe" src="Imagenes/Imglogo_Re.png" alt="Avatar" style="width: 100%; height: 500px; object-fit: cover;" cssclass="mc-image" OnClick="imgRe_Click"/>
                </div>
                <div>
                     <div class="mc-middle">
                        <telerik:RadLabel runat="server" ID="LBLSinPermisos" Text="No cuentas con ningun permiso. Consulta a tu administrador" Font-Size="XX-Large" Visible="false"></telerik:RadLabel>
                    </div>
                </div>
               <%-- <div class="col mc-container" style='display:<%# iif(tmpUsuario("CAT_PE_A_JUDICIAL"),"initial","none")%>'>
                    <div class="mc-middle">
                        <span class="w3-center m-auto font-weight-bold">Judicial</span>
                    </div>
                    <asp:ImageButton runat="server" ID="imgLe"  src="Imagenes/Imglogo_Le.png" alt="Avatar" style="width: 100%; height: 500px; object-fit: cover;" cssclass="mc-image" OnClick="imgLe_Click"/>
                </div>--%>
            </div>
        </asp:Panel>
    </form>
    <style>
        .mc-container {
  position: relative;
  width: 50%;
}
        .mc-image {
            opacity: 1;
            display: block;
            width: 100%;
            height: auto;
            transition: .5s ease;
            backface-visibility: hidden;            
        }

        .mc-middle {
            transition: .5s ease-in-out;
            opacity: 0;
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            -ms-transform: translate(-50%, -50%);
            text-align: center;            
        }

        .mc-container:hover .mc-image {
            opacity: 0.3;
        }

        .mc-container:hover .mc-middle {
            opacity: 1;
        }
    </style>
    <script src="Scripts/PestanasMejorado.js?v=1.1" type="text/javascript"></script>
</body>

</html>
