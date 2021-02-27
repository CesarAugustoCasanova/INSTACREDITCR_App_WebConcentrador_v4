<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="Login" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta http-equiv="Cache-Control" content="no-cache, no-store, must-revalidate" />
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Expires" content="0" />
    <title>MC Collect : Inicio de sesión</title>
    <link href="https://fonts.googleapis.com/css?family=Prata|Roboto&display=swap" rel="stylesheet" />
    <!-- Critical css  -->
    <style type="text/css">
        html {
            box-sizing: border-box
        }

        *, *:before, *:after {
            box-sizing: inherit
        }

        html {
            -ms-text-size-adjust: 100%;
            -webkit-text-size-adjust: 100%
        }

        body {
            margin: 0
        }

        a {
            background-color: transparent;
            -webkit-text-decoration-skip: objects
        }

        img {
            border-style: none
        }

        button, input {
            font: inherit;
            margin: 0
        }

        button, input {
            overflow: visible
        }

        button {
            text-transform: none
        }

        button, html [type=button], [type=submit] {
            -webkit-appearance: button
        }

            button::-moz-focus-inner, [type=button]::-moz-focus-inner, [type=submit]::-moz-focus-inner {
                border-style: none;
                padding: 0
            }

            button:-moz-focusring, [type=button]:-moz-focusring, [type=submit]:-moz-focusring {
                outline: 1px dotted ButtonText
            }

        ::-webkit-input-placeholder {
            color: inherit;
            opacity: 0.54
        }

        ::-webkit-file-upload-button {
            -webkit-appearance: button;
            font: inherit
        }

        html, body {
            font-family: Verdana,sans-serif;
            font-size: 15px;
            line-height: 1.5
        }

        html {
            overflow-x: hidden
        }

        h1 {
            font-size: 36px
        }

        h4 {
            font-size: 20px
        }

        h1, h4 {
            font-family: "Segoe UI",Arial,sans-serif;
            font-weight: 400;
            margin: 10px 0
        }

        img {
            vertical-align: middle
        }

        a {
            color: inherit
        }

        .w3-display-container {
            position: relative
        }

        .w3-container:after, .w3-container:before, .w3-row:after, .w3-row:before, .w3-row-padding:after, .w3-row-padding:before {
            content: "";
            display: table;
            clear: both
        }

        .w3-col {
            float: left;
            width: 100%
        }

            .w3-col.s12 {
                width: 99.99999%
            }

        @media (min-width:601px) {
            .w3-col.m4 {
                width: 33.33333%
            }
        }

        @media (min-width:993px) {
            .w3-col.l4 {
                width: 33.33333%
            }

            .w3-col.l8 {
                width: 66.66666%
            }
        }

        @media (max-width:600px) {
            .w3-hide-small {
                display: none !important
            }
        }

        .w3-display-bottommiddle {
            position: absolute;
            left: 50%;
            bottom: 0;
            transform: translate(-50%,0%);
            -ms-transform: translate(-50%,0%)
        }

        .w3-row-padding, .w3-row-padding > .w3-col {
            padding: 0 8px
        }

        .w3-container {
            padding: 0.01em 16px
        }

        .w3-xlarge {
            font-size: 24px !important
        }

        .w3-center {
            text-align: center !important
        }

        .w3-padding {
            padding: 8px 16px !important
        }

        .w3-left {
            float: left !important
        }

        .w3-right {
            float: right !important
        }

        .w3-blue {
            color: #fff !important;
            background-color: #2196F3 !important
        }

        .w3-text-white {
            color: #fff !important
        }

        :root {
            --blue: #007bff;
            --indigo: #6610f2;
            --purple: #6f42c1;
            --pink: #e83e8c;
            --red: #dc3545;
            --orange: #fd7e14;
            --yellow: #ffc107;
            --green: #28a745;
            --teal: #20c997;
            --cyan: #17a2b8;
            --white: #fff;
            --gray: #6c757d;
            --gray-dark: #343a40;
            --primary: #007bff;
            --secondary: #6c757d;
            --success: #28a745;
            --info: #17a2b8;
            --warning: #ffc107;
            --danger: #dc3545;
            --light: #f8f9fa;
            --dark: #343a40;
            --breakpoint-xs: 0;
            --breakpoint-sm: 576px;
            --breakpoint-md: 768px;
            --breakpoint-lg: 992px;
            --breakpoint-xl: 1200px;
            --font-family-sans-serif: -apple-system,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,"Noto Sans",sans-serif,"Apple Color Emoji","Segoe UI Emoji","Segoe UI Symbol","Noto Color Emoji";
            --font-family-monospace: SFMono-Regular,Menlo,Monaco,Consolas,"Liberation Mono","Courier New",monospace
        }

        *, ::after, ::before {
            box-sizing: border-box
        }

        html {
            font-family: sans-serif;
            line-height: 1.15;
            -webkit-text-size-adjust: 100%
        }

        body {
            margin: 0;
            font-family: -apple-system,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,"Noto Sans",sans-serif,"Apple Color Emoji","Segoe UI Emoji","Segoe UI Symbol","Noto Color Emoji";
            font-size: 1rem;
            font-weight: 400;
            line-height: 1.5;
            color: #212529;
            text-align: left;
            background-color: #fff
        }

        h1, h4 {
            margin-top: 0;
            margin-bottom: .5rem
        }

        p {
            margin-top: 0;
            margin-bottom: 1rem
        }

        ul {
            margin-top: 0;
            margin-bottom: 1rem
        }

        b {
            font-weight: bolder
        }

        a {
            color: #007bff;
            text-decoration: none;
            background-color: transparent
        }

        img {
            vertical-align: middle;
            border-style: none
        }

        label {
            display: inline-block;
            margin-bottom: .5rem
        }

        button {
            border-radius: 0
        }

        button, input {
            margin: 0;
            font-family: inherit;
            font-size: inherit;
            line-height: inherit
        }

        button, input {
            overflow: visible
        }

        button {
            text-transform: none
        }

        [type=button], [type=submit], button {
            -webkit-appearance: button
        }

            [type=button]::-moz-focus-inner, [type=submit]::-moz-focus-inner, button::-moz-focus-inner {
                padding: 0;
                border-style: none
            }

        ::-webkit-file-upload-button {
            font: inherit;
            -webkit-appearance: button
        }

        .h3, h1, h4 {
            margin-bottom: .5rem;
            font-weight: 500;
            line-height: 1.2
        }

        h1 {
            font-size: 2.5rem
        }

        .h3 {
            font-size: 1.75rem
        }

        h4 {
            font-size: 1.5rem
        }

        .img-fluid {
            max-width: 100%;
            height: auto
        }

        .form-control {
            display: block;
            width: 100%;
            height: calc(1.5em + .75rem + 2px);
            padding: .375rem .75rem;
            font-size: 1rem;
            font-weight: 400;
            line-height: 1.5;
            color: #495057;
            background-color: #fff;
            background-clip: padding-box;
            border: 1px solid #ced4da;
            border-radius: .25rem
        }

            .form-control::-ms-expand {
                background-color: transparent;
                border: 0
            }

            .form-control:focus {
                color: #495057;
                background-color: #fff;
                border-color: #80bdff;
                outline: 0;
                box-shadow: 0 0 0 .2rem rgba(0,123,255,.25)
            }

            .form-control::-webkit-input-placeholder {
                color: #6c757d;
                opacity: 1
            }

            .form-control::-moz-placeholder {
                color: #6c757d;
                opacity: 1
            }

            .form-control:-ms-input-placeholder {
                color: #6c757d;
                opacity: 1
            }

            .form-control::-ms-input-placeholder {
                color: #6c757d;
                opacity: 1
            }

        .btn {
            display: inline-block;
            font-weight: 400;
            color: #212529;
            text-align: center;
            vertical-align: middle;
            background-color: transparent;
            border: 1px solid transparent;
            padding: .375rem .75rem;
            font-size: 1rem;
            line-height: 1.5;
            border-radius: .25rem
        }

        .btn-primary {
            color: #fff;
            background-color: #007bff;
            border-color: #007bff
        }

        .btn-lg {
            padding: .5rem 1rem;
            font-size: 1.25rem;
            line-height: 1.5;
            border-radius: .3rem
        }

        .btn-block {
            display: block;
            width: 100%
        }

        input[type=button].btn-block {
            width: 100%
        }

        .modal {
            position: fixed;
            top: 0;
            left: 0;
            z-index: 1050;
            display: none;
            width: 100%;
            height: 100%;
            overflow: hidden;
            outline: 0
        }

        .modal-dialog {
            position: relative;
            width: auto;
            margin: .5rem
        }

        .modal-content {
            position: relative;
            display: -ms-flexbox;
            display: flex;
            -ms-flex-direction: column;
            flex-direction: column;
            width: 100%;
            background-color: #fff;
            background-clip: padding-box;
            border: 1px solid rgba(0,0,0,.2);
            border-radius: .3rem;
            outline: 0
        }

        .modal-header {
            display: -ms-flexbox;
            display: flex;
            -ms-flex-align: start;
            align-items: flex-start;
            -ms-flex-pack: justify;
            justify-content: space-between;
            padding: 1rem 1rem;
            border-bottom: 1px solid #dee2e6;
            border-top-left-radius: .3rem;
            border-top-right-radius: .3rem
        }

        .modal-title {
            margin-bottom: 0;
            line-height: 1.5
        }

        .modal-body {
            position: relative;
            -ms-flex: 1 1 auto;
            flex: 1 1 auto;
            padding: 1rem
        }

        .modal-footer {
            display: -ms-flexbox;
            display: flex;
            -ms-flex-align: center;
            align-items: center;
            -ms-flex-pack: end;
            justify-content: flex-end;
            padding: 1rem;
            border-top: 1px solid #dee2e6;
            border-bottom-right-radius: .3rem;
            border-bottom-left-radius: .3rem
        }

        @media (min-width:576px) {
            .modal-dialog {
                max-width: 500px;
                margin: 1.75rem auto
            }
        }

        .d-flex {
            display: -ms-flexbox !important;
            display: flex !important
        }

        .flex-column {
            -ms-flex-direction: column !important;
            flex-direction: column !important
        }

        .justify-content-center {
            -ms-flex-pack: center !important;
            justify-content: center !important
        }

        .mb-3 {
            margin-bottom: 1rem !important
        }

        .mt-5 {
            margin-top: 3rem !important
        }

        .mx-auto {
            margin-right: auto !important
        }

        .mx-auto {
            margin-left: auto !important
        }

        .font-weight-light {
            font-weight: 300 !important
        }

        .font-weight-normal {
            font-weight: 400 !important
        }

        .font-weight-bold {
            font-weight: 700 !important
        }

        .text-muted {
            color: #6c757d !important
        }
    </style>


</head>
<body>

    <form id="FrmLogin" runat="server">
        <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="btnLogin">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="Notificacion" />
                        <telerik:AjaxUpdatedControl ControlID="lblExpirado" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnExpirado">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="Notificacion" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPnlGeneral" runat="server">
        </telerik:RadAjaxLoadingPanel>
        
        <!-- Navbar -->
        <div class="w3-row w3-padding" style="height: 60px">
            <div class="w3-col m4 w3-hide-small">
                <img src="Imagenes/ImgLogo_Cliente.png" alt="MC Collect S.A. de C.V." class="img-fluid" style="max-height: 50px;" />
            </div>
            <div class="w3-col m4 s12 w3-xxlarge w3-center">
                <span style="font-family: 'Prata', serif; font-size: xx-large;">MC Collect</span>
            </div>
            <div class="w3-col m4 w3-hide-small">
                <img src="Imagenes/ImgLogo_Mc.png" alt="MC Collect S.A. de C.V." style="max-height: 50px;" class="img-fluid w3-right" />
            </div>
        </div>
        <br />
        <div class="w3-row-padding">
            <!-- Login -->
            <div id="divLogin" class="w3-col s12 l4 mx-auto">
                <div class="d-flex flex-column" style="width: 100%">
                    <telerik:RadAjaxPanel runat="server" ID="pnlLogin" CssClass="mx-auto" LoadingPanelID="RadAjaxLoadingPnlGeneral">
                        <h1 class="h3 mb-3 font-weight-normal">Inicio de sesión</h1>
                        <label>Usuario</label>
                        <asp:TextBox runat="server" type="text" ID="txtUsr" Style="width: 100%" CssClass="form-control" placeholder="Usuario" required="true" autocomplete="username"></asp:TextBox>
                        <label>Contraseña</label>
                        <asp:TextBox runat="server" type="password" ID="txtPwd" Style="width: 100%" CssClass="form-control" placeholder="Contraseña" required="true" autocomplete="current-password"></asp:TextBox>
                        <br />
                        <asp:Button runat="server" ID="btnLogin" class="btn btn-lg btn-primary btn-block" Text="Iniciar sesión" />
                        <p class="mt-5 mb-3 text-muted">&copy; 2017-2019
                            <telerik:RadLabel runat="server" ID="LblAmbiente" ForeColor="Red" Font-Bold="true"></telerik:RadLabel>
                        </p>
                    </telerik:RadAjaxPanel>
                </div>
            </div>
            <!-- Avisos -->
            <div class="w3-col s12 l8">
                <div class="d-flex flex-column" style="width: 100%">
                    <div class="mx-auto">
                        <br />
                        <br />
                        <br />
                        <%-- <h1 class="h3 mb-3 font-weight-normal">Avisos</h1>--%>
                        <telerik:RadRotator RenderMode="Lightweight" runat="server" ID="RadRotator1" DataSourceID="LinqDataSource1" ScrollDuration="4000" FrameDuration="4000" ScrollDirection="Left, Right" Width="850px" ItemWidth="850px" Height="400px" ItemHeight="400px" RotatorType="AutomaticAdvance" EnableDragScrolling="true">
                            <ItemTemplate>
                                <div class="w3-display-container" style="height: 100%">
                                    <asp:Image ID="CustomerImage" runat="server" AlternateText="Customer image" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "ImgURL")%>' Style="height: 400px; width: 850px; object-fit: cover;"></asp:Image>
                                    <div class="w3-display-bottommiddle w3-container w3-text-white w3-padding" style="width: 100%; opacity: 0.8; background-color: #5d5e60">
                                        <b><%# DataBinder.Eval(Container.DataItem, "Titulo") %></b>
                                        <br />
                                        <%# DataBinder.Eval(Container.DataItem, "Descripcion")%>
                                    </div>
                                </div>
                            </ItemTemplate>
                            <ControlButtons LeftButtonID="imgRotatorLeft" RightButtonID="imgRotatorRight"></ControlButtons>
                        </telerik:RadRotator>
                        <asp:LinqDataSource ID="LinqDataSource1" runat="server" OnSelecting="LinqDataSource1_Selecting">
                        </asp:LinqDataSource>
                        <div class="w3-container justify-content-center">
                            <div class="w3-left">
                                <asp:Image runat="server" ID="imgRotatorLeft" AlternateText="Left" ImageUrl="Imagenes/atras.png" />
                            </div>
                            <div class="w3-right">
                                <asp:Image runat="server" ID="imgRotatorRight" AlternateText="Right" ImageUrl="Imagenes/adelante.png" />
                            </div>
                        </div>
                    </div>


                </div>
            </div>

        </div>
        <!-- Usuario expirado -->
        <div id="pnlExpirado" class="modal">
            <div class="modal-dialog">
                <div class="modal-content">

                    <!-- Modal Header -->
                    <div class="modal-header">
                        <h4 class="modal-title">
                            <telerik:RadLabel runat="server" ID="lblExpirado"></telerik:RadLabel>
                        </h4>
                    </div>

                    <!-- Modal body -->
                    <div class="modal-body">
                        <telerik:RadAjaxPanel runat="server" ID="RadAjaxPanel1" CssClass="mx-auto" LoadingPanelID="RadAjaxLoadingPnlGeneral">
                            <label>Nueva contraseña:</label>
                            <asp:TextBox runat="server" type="password" ID="txtNuevaContrasena" Style="width: 100%" CssClass="form-control" placeholder="Nueva contraseña" required="true" autocomplete="new-password"></asp:TextBox>
                            <label>Contraseña</label>
                            <asp:TextBox runat="server" type="password" ID="txtRepiteContrasena" Style="width: 100%" CssClass="form-control" placeholder="Repite la contraseña" required="true" autocomplete="new-password"></asp:TextBox>
                        </telerik:RadAjaxPanel>
                    </div>

                    <!-- Modal footer -->
                    <div class="modal-footer">
                        <telerik:RadButton runat="server" ID="btnExpirado" Text="Continuar"></telerik:RadButton>
                    </div>
                </div>
            </div>
        </div>
        <telerik:RadNotification ID="Notificacion" runat="server" Position="Center" Width="330" Height="160" Animation="Fade" EnableRoundedCorners="true" EnableShadow="true" Style="z-index: 100000">
        </telerik:RadNotification>
        <asp:PlaceHolder runat="server" ID="phTest"></asp:PlaceHolder>
    </form>
    <script src="Scripts/jQuery.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <link href="Estilos/w3.css" rel="stylesheet" />
    <link href="Estilos/bootstrap.min.css" rel="stylesheet" />
    <script type="text/javascript" lang="js">
        $("#divLogin").keypress(function () {
            if (arguments[0].key == "Enter") {
                __doPostBack('btnLogin', '')
            }
        });
        $("#pnlExpirado").keypress(function () {
            if (arguments[0].key == "Enter") {
                __doPostBack('btnExpirado', '')
            }
        });
        //$("#txtUsr").blur(function () {
        //    $(this).val($(this).val().toUpperCase());
        //});
    </script>
</body>
</html>
