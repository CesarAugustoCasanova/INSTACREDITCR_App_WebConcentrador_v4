<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MasterPage.aspx.vb" Inherits="MGestion_MasterPage" EnableEventValidation="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Modulo Gestión</title>
    <!--Critical CSS-->
    <style>
        .rtsLI > div {
            padding: 7px 5px !important;
        }

        .w3-tiny {
            font-size: 10px !important;
        }

        .w3-left-align {
            text-align: left !important;
        }

        html {
            box-sizing: border-box;
        }

        *, *:before, *:after {
            box-sizing: inherit;
        }

        html {
            -ms-text-size-adjust: 100%;
            -webkit-text-size-adjust: 100%;
        }

        body {
            margin: 0;
        }

        .scroll::-webkit-scrollbar {
            width: 8px;
            height: 8px;
        }

        .scroll::-webkit-scrollbar-thumb {
            background-image: linear-gradient(#23098c,#379ae8);
            border-radius: 4px;
        }

            .scroll::-webkit-scrollbar-thumb:hover {
                background-image: linear-gradient(#379ae8,#23098c);
            }

        .scroll::-webkit-scrollbar-track {
            background: #e1e1e1;
            border-radius: 4px;
        }

            .scroll::-webkit-scrollbar-track:hover {
                background: #d4d4d4;
            }

        a {
            background-color: transparent;
            -webkit-text-decoration-skip: objects;
        }

        small {
            font-size: 80%;
        }

        img {
            border-style: none;
        }

        hr {
            box-sizing: content-box;
            height: 0;
            overflow: visible;
        }

        button, input, textarea {
            font: inherit;
            margin: 0;
        }

        button, input {
            overflow: visible;
        }

        button {
            text-transform: none;
        }

        button, html [type=button], [type=submit] {
            -webkit-appearance: button;
        }

            button::-moz-focus-inner, [type=button]::-moz-focus-inner, [type=submit]::-moz-focus-inner {
                border-style: none;
                padding: 0;
            }

            button:-moz-focusring, [type=button]:-moz-focusring, [type=submit]:-moz-focusring {
                outline: 1px dotted ButtonText;
            }

        textarea {
            overflow: auto;
        }

        ::-webkit-input-placeholder {
            color: inherit;
            opacity: 0.54;
        }

        ::-webkit-file-upload-button {
            -webkit-appearance: button;
            font: inherit;
        }

        html, body {
            font-family: Verdana,sans-serif;
            font-size: 15px;
            line-height: 1.5;
        }

        html {
            overflow-x: hidden;
        }

        h2 {
            font-size: 30px;
        }

        h2 {
            font-family: "Segoe UI",Arial,sans-serif;
            font-weight: 400;
            margin: 10px 0;
        }

        hr {
            border: 0;
            border-top: 1px solid #eee;
            margin: 20px 0;
        }

        img {
            vertical-align: middle;
        }

        a {
            color: inherit;
        }

        .w3-table-all {
            border-collapse: collapse;
            border-spacing: 0;
            width: 100%;
            display: table;
        }

        .w3-table-all {
            border: 1px solid #ccc;
        }

        .w3-btn, .w3-button {
            border: none;
            display: inline-block;
            padding: 8px 16px;
            vertical-align: middle;
            overflow: hidden;
            text-decoration: none;
            color: inherit;
            background-color: inherit;
            text-align: center;
            white-space: nowrap;
        }

        .w3-btn, .w3-button {
            -webkit-touch-callout: none;
        }

        .w3-tag {
            background-color: #000;
            color: #fff;
            display: inline-block;
            padding-left: 8px;
            padding-right: 8px;
            text-align: center;
        }

        .w3-input {
            padding: 8px;
            display: block;
            border: none;
            border-bottom: 1px solid #ccc;
            width: 100%;
        }

        .w3-modal {
            z-index: 3;
            display: none;
            padding-top: 100px;
            position: fixed;
            left: 0;
            top: 0;
            width: 100%;
            height: 100%;
            overflow: auto;
            background-color: rgb(0,0,0);
            background-color: rgba(0,0,0,0.4);
        }

        .w3-modal-content {
            margin: auto;
            background-color: #fff;
            position: relative;
            padding: 0;
            outline: 0;
            width: 600px;
        }

        .w3-bar {
            width: 100%;
            overflow: hidden;
        }

        .w3-center .w3-bar {
            display: inline-block;
            width: auto;
        }

        .w3-responsive {
            display: block;
            overflow-x: auto;
        }

        .w3-container:after, .w3-container:before, .w3-panel:after, .w3-panel:before, .w3-row:after, .w3-row:before, .w3-row-padding:after, .w3-row-padding:before, .w3-bar:before, .w3-bar:after {
            content: "";
            display: table;
            clear: both;
        }

        .w3-col, .w3-half, .w3-third {
            float: left;
            width: 100%;
        }

            .w3-col.s1 {
                width: 8.33333%;
            }

        @media (min-width:601px) {
            .w3-third {
                width: 33.33333%;
            }

            .w3-half {
                width: 49.99999%;
            }
        }

        .w3-rest {
            overflow: hidden;
        }

        .w3-hide {
            display: none !important;
        }

        @media (max-width:600px) {
            .w3-modal-content {
                margin: 0 10px;
                width: auto !important;
            }

            .w3-modal {
                padding-top: 30px;
            }
        }

        @media (max-width:768px) {
            .w3-modal-content {
                width: 500px;
            }

            .w3-modal {
                padding-top: 50px;
            }
        }

        @media (min-width:993px) {
            .w3-modal-content {
                width: 900px;
            }
        }

        .w3-display-topright {
            position: absolute;
            right: 0;
            top: 0;
        }

        .w3-round-large {
            border-radius: 8px;
        }

        .w3-row-padding, .w3-row-padding > .w3-half, .w3-row-padding > .w3-third {
            padding: 0 8px;
        }

        .w3-container, .w3-panel {
            padding: 0.01em 16px;
        }

        .w3-panel {
            margin-top: 16px;
            margin-bottom: 16px;
        }

        .w3-card {
            box-shadow: 0 2px 5px 0 rgba(0,0,0,0.16),0 2px 10px 0 rgba(0,0,0,0.12);
        }

        .w3-card-4 {
            box-shadow: 0 4px 10px 0 rgba(0,0,0,0.2),0 4px 20px 0 rgba(0,0,0,0.19);
        }

        .w3-large {
            font-size: 18px !important;
        }

        .w3-xlarge {
            font-size: 24px !important;
        }

        .w3-jumbo {
            font-size: 64px !important;
        }

        .w3-center {
            text-align: center !important;
        }

        .w3-border-0 {
            border: 0 !important;
        }

        .w3-border {
            border: 1px solid #ccc !important;
        }

        .w3-margin {
            margin: 2px !important;
        }

        .w3-margin-top {
            margin-top: 2px !important;
        }

        .w3-padding {
            padding: 6px 12px !important;
        }

        .w3-padding-24 {
            padding-top: 24px !important;
            padding-bottom: 24px !important;
        }

        .w3-left {
            float: left !important;
        }

        .w3-right {
            float: right !important;
        }

        .w3-blue {
            color: #fff !important;
            background-color: #2196F3 !important;
        }

        .w3-green {
            color: #fff !important;
            background-color: #4CAF50 !important;
        }

        .w3-red {
            color: #fff !important;
            background-color: #f44336 !important;
        }

        .w3-white {
            color: #000 !important;
            background-color: #fff !important;
        }

        .w3-dark-gray {
            color: #fff !important;
            background-color: #616161 !important;
        }

        .w3-text-green {
            color: #4CAF50 !important;
        }
    </style>
    
    <link rel="Shortcut Icon" type="image/jpg" href="Imagenes/favicon.ico" />
    <meta name="viewport" content="width=device-width, user-scalable=no" />
    <meta charset="UTF-8" />
    <script src="JS/jquery.js" type="text/javascript"></script>
</head>

<body style="font-size: .7em" class="scroll">

    <form id="FrmMasterPage" runat="server" onmousemove="movement();">
        <noscript>
            <div class="w3-modal" style="display: block">
                <div class="w3-modal-content">
                    <div class="w3-container w3-red w3-center w3-padding-24 w3-jumbo">
                        JavaScript deshabilitado
                   
                    </div>
                    <div class="w3-container w3-center w3-xlarge">
                        Javascript está deshabilitado en su navegador web. Por favor, para ver correctamente este sitio, <b><i>habilite javascript</i></b>.<br />
                        <br />
                        Para ver las instrucciones para habilitar javascript en su navegador, haga click <a href="http://www.enable-javascript.com/es/">aquí</a>.
                   
                    </div>
                </div>
            </div>
        </noscript>
        <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
        <asp:UpdatePanel runat="server" ID="PRueb">
            <ContentTemplate>
                <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
                    <AjaxSettings>
                        <telerik:AjaxSetting AjaxControlID="btnGestion">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID="pnlGestion" />
                            </UpdatedControls>
                        </telerik:AjaxSetting>
                    </AjaxSettings>
                </telerik:RadAjaxManager>


                <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPnlGeneral" runat="server">
                </telerik:RadAjaxLoadingPanel>
                <telerik:RadPersistenceManager runat="server" ID="RadPersistenceManager1">
                    <PersistenceSettings>
                        <telerik:PersistenceSetting ControlID="RadTabStrip1" />
                    </PersistenceSettings>
                </telerik:RadPersistenceManager>
                <!-- Mensajes -->
                <div style="position: fixed; right: 0px; bottom: 0%; width: 20%">
                    <div id="Avisos" class="w3-block w3-hide w3-light-grey w3-card-4" style="height: 500px; max-height: 70%; max-width: 100%;">
                        <div class="w3-block w3-margin-bottom w3-button w3-hover-indigo w3-text-white w3-blue" onclick="desplegarAvisos()">
                            &#9660; <b>Avisos</b>
                        </div>
                        <div style="overflow: auto; max-width: 100%; height: 500px; max-height: 90%;" class="scroll">

                            <telerik:RadListView runat="server" ID="listViewAvisos">
                                <EmptyDataTemplate>
                                    <div class="w3-panel w3-white w3-center">
                                        Sin Avisos nuevos
                                   
                                    </div>
                                </EmptyDataTemplate>
                                <ItemTemplate>
                                    <div class="w3-panel w3-white w3-card">
                                        <div class="w3-row">
                                            <b>
                                                <%# Eval("Tipo") %>
                                            </b>
                                        <telerik:RadButton runat="server" ID="BtnIr"  CssClass="w3-border-0 w3-bar w3-margin" OnClick="BtnIr_Click" Height="20px" Width="20px" Visible='true' ToolTip='<%# Eval("Mensaje") %>'>
                                            <Image ImageUrl="Imagenes/icon-navbar.png" />
                                        </telerik:RadButton>
                                        </div>

                                          <div class="w3-row w3-text-grey w3-small">
                                          <%--<span><%# Eval("Fecha de creacion") %> a --%>
                                              <%# Eval("Fecha de vigencia") %></span>
                                        </div>
                                        <div class="w3-row">
                                            <%# Eval("Mensaje") %>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </telerik:RadListView>
                        </div>
                    </div>
                    <div id="PestanaAvisos" class="w3-button w3-block w3-hover-indigo w3-blue w3-text-white w3-center w3-card-4 w3-padding" onclick="desplegarAvisos()">
                        <b>Avisos
                           
                            <asp:Image runat="server" ID="imgAvisos" Style="max-height: 20px; height: 100%" AlternateText="Mensaje Nuevo" ImageUrl="Imagenes/mensaje.png" />
                            <button class="w3-border-0 w3-blue" onclick="document.getElementById('PnlAvisosGral').style.display='block';return false;">
                                <img src="Imagenes/menuavisos.png" />
                            </button>
                        </b>
                    </div>
                   
                    <telerik:RadScriptBlock runat="server">
                        <script>
                            function desplegarAvisos() {
                                $("#Avisos").toggleClass("w3-show");
                                $("#PestanaAvisos").toggleClass("w3-hide");

                                //Si se abren los #Avisos y la #imgAvisos está visible
                                //La hace invisible y pone los avisos como "vistos"
                                if ($("#PestanaAvisos").hasClass("w3-hide"))
                                    if (!$('#imgAvisos').hasClass("w3-hide")) {
                                        $('#imgAvisos').addClass("w3-hide")
                                        $.ajax({
                                            type: "POST",
                                            url: "MasterPage.aspx/setSeen",
                                            data: "{'v_usuario':'<%= tmpUSUARIO("CAT_LO_USUARIO") %>'}",
                                            contentType: "application/json; charset=utf-8",
                                            dataType: "json",
                                            async: true,
                                            success: function (msg)
                                            { }
                                        });
                                    };
                            };
                        </script>
                        <script type="text/jscript">
                            console.log("entra1");
                            function SessionKeepALive() {
                                console.log("entra");

                                $.ajax({
                                    type: "POST",
                                    url: "MasterPage.aspx/KeepActiveSession",
                                    data: "{'Usuario':'<%= tmpUSUARIO("CAT_LO_USUARIO") %>'}",
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    async: true,
                                    success: function (msg) {
                                        if (msg.d == "Bye") {
                                            window.location.href = "../SesionExpirada.aspx";
                                            console.log("expiro")
                                        }
                                    }
                                });

                                setTimeout(SessionKeepALive, 100000);
                            };
                            SessionKeepALive();
                        </script>
                    </telerik:RadScriptBlock>
                    
                </div>
                <!-- Siguente credito -->
                <div style="position: fixed; right: 10px; top: 50%;">
                    <telerik:RadButton CssClass="w3-btn w3-border w3-hover-light-gray w3-block" runat="server" ID="imgNext" Style="border: 0px solid black; padding: 0px; margin: 0px">
                        <ContentTemplate>
                            <img src="Imagenes/ImgNext.png" style="max-height: 40px; height: 100%" alt="Next" />
                        </ContentTemplate>
                    </telerik:RadButton>

                    <telerik:RadToolTip runat="server" RelativeTo="Element" ShowEvent="OnMouseOver" TargetControlID="imgNext" Position="MiddleLeft" ShowDelay="0" HideDelay="0" Font-Size="Large" AutoCloseDelay="0">
                        <telerik:RadLabel ID="lblNomFila" runat="server" Visible="false" CssClass="w3-block"></telerik:RadLabel>
                    </telerik:RadToolTip>
                </div>
                <div class="w3-row">
                    <!-- nav -->
                    <div class="w3-col s1 w3-center">
                        <br />
                        <img src="Imagenes/ImgLogo_Cliente.png" style="width: 100%;" alt="Logo" />
                        <br />
                        <br />
                        <button class="w3-border-0 w3-white" onclick="document.getElementById('PnlUsuario').style.display='block';return false;">
                            <img src="Imagenes/usr.png" alt="usuario" />
                        </button>
                        <div>
                            <telerik:RadLabel runat="server" ID="lblUsr"></telerik:RadLabel>
                        </div>
                        <hr />
                        <telerik:RadAjaxPanel runat="server" ID="tooltipoGestion">
                            <telerik:RadButton CssClass="w3-btn" runat="server" ID="btnGestion" Style="border: 0px solid black; padding: 0px; margin: 0px">
                                <ContentTemplate>
                                    <img src="Imagenes/gestion.png" alt="Gestion" />
                                </ContentTemplate>
                            </telerik:RadButton>
                            <telerik:RadToolTip runat="server" RelativeTo="Element" ShowEvent="OnMouseOver" TargetControlID="btnGestion" Position="MiddleRight" ShowDelay="0" HideDelay="0" Font-Size="Large" AutoCloseDelay="0">
                                Captura gestión
                           
                           
                           
                           
                            </telerik:RadToolTip>
                            <%--<button id="BtnGestion" class="w3-white w3-btn" onclick="openGestion();return false;">
                                <img src="Imagenes/gestion.png" alt="Gestion" />
                            </button>--%>
                        </telerik:RadAjaxPanel>
                        <hr />
                        <div class="scroll" style="max-height: 300px; overflow-x: hidden; overflow-y: scroll">
                            <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1" SelectedIndex="2" Orientation="VerticalLeft">
                                <Tabs>
                                    <telerik:RadTab Visible="false">
                                        <TabTemplate>
                                            <div id="rt1">
                                            </div>
                                        </TabTemplate>
                                    </telerik:RadTab>
                                    <telerik:RadTab Value="1" Visible='<%# tmpPermisos("MENU_INF_GENERAL") %>' SelectedCssClass="w3-text-blue">
                                        <TabTemplate>
                                            <div runat="server" id="rt1" class="w3-block w3-left-align w3-tiny">
                                                <img src="Imagenes/icon-navbar.png" alt="img" />
                                                General
                                           
                                            </div>
                                            <telerik:RadToolTip runat="server" RelativeTo="Element" ShowEvent="OnMouseOver" TargetControlID="rt1" Position="MiddleRight" ShowDelay="0" HideDelay="0" Font-Size="Large" AutoCloseDelay="0">
                                                Información general
                                           
                                            </telerik:RadToolTip>

                                        </TabTemplate>
                                    </telerik:RadTab>
                                    <telerik:RadTab Value="3" Visible='<%# tmpPermisos("MENU_INF_ADICIONAL") %>' SelectedCssClass="w3-text-blue" Selected="True">
                                        <TabTemplate>
                                            <div runat="server" id="rt1" class="w3-block w3-left-align w3-tiny">
                                                <img src="Imagenes/icon-navbar.png" alt="img" />
                                                Adicional
                                           
                                            </div>
                                            <telerik:RadToolTip runat="server" RelativeTo="Element" ShowEvent="OnMouseOver" TargetControlID="rt1" Position="MiddleRight" ShowDelay="0" HideDelay="0" Font-Size="Large" AutoCloseDelay="0">
                                                Información adicional
                                           
                                            </telerik:RadToolTip>
                                        </TabTemplate>
                                    </telerik:RadTab>
                                    <telerik:RadTab Value="13" Visible='<%# tmpPermisos("MENU_MENU_DATOS_DEMOGRAFICOS") %>' SelectedCssClass="w3-text-blue">
                                        <TabTemplate>
                                            <div runat="server" id="rt1" class="w3-block w3-left-align w3-tiny">
                                                <img src="Imagenes/icon-navbar.png" alt="img" />
                                                Demográfico
                                           
                                            </div>
                                            <telerik:RadToolTip runat="server" RelativeTo="Element" ShowEvent="OnMouseOver" TargetControlID="rt1" Position="MiddleRight" ShowDelay="0" HideDelay="0" Font-Size="Large" AutoCloseDelay="0">
                                                Datos demográficos
                                           
                                            </telerik:RadToolTip>
                                        </TabTemplate>
                                    </telerik:RadTab>
                                    <telerik:RadTab Value="4" Visible='<%# tmpPermisos("MENU_HIST_ACTIVIDADES") %>' SelectedCssClass="w3-text-blue">
                                        <TabTemplate>
                                            <div runat="server" id="rt1" class="w3-block w3-left-align w3-tiny">
                                                <img src="Imagenes/icon-navbar.png" alt="img" />
                                                Actividad
                                           
                                            </div>
                                            <telerik:RadToolTip runat="server" RelativeTo="Element" ShowEvent="OnMouseOver" TargetControlID="rt1" Position="MiddleRight" ShowDelay="0" HideDelay="0" Font-Size="Large" AutoCloseDelay="0">
                                                Historico de actividades
                                           
                                            </telerik:RadToolTip>
                                        </TabTemplate>
                                    </telerik:RadTab>
                                    <telerik:RadTab Value="5" Visible='<%#tmpPermisos("MENU_HIST_PAGOS") %>' SelectedCssClass="w3-text-blue">
                                        <TabTemplate>
                                            <div runat="server" id="rt1" class="w3-block w3-left-align w3-tiny">
                                                <img src="Imagenes/icon-navbar.png" alt="img" />
                                                Pagos
                                           
                                            </div>
                                            <telerik:RadToolTip runat="server" RelativeTo="Element" ShowEvent="OnMouseOver" TargetControlID="rt1" Position="MiddleRight" ShowDelay="0" HideDelay="0" Font-Size="Large" AutoCloseDelay="0">
                                                Historico de pagos
                                           
                                            </telerik:RadToolTip>
                                        </TabTemplate>
                                    </telerik:RadTab>
                                    <telerik:RadTab Value="6" SelectedIndex="6" Visible='<%# tmpPermisos("MENU_NEGOCIACIONES") %>' SelectedCssClass="w3-text-blue">
                                        <TabTemplate>
                                            <div runat="server" id="rt1" class="w3-block w3-left-align w3-tiny">
                                                <img src="Imagenes/icon-navbar.png" alt="img" />
                                                Negociación
                                           
                                            </div>
                                            <telerik:RadToolTip runat="server" RelativeTo="Element" ShowEvent="OnMouseOver" TargetControlID="rt1" Position="MiddleRight" ShowDelay="0" HideDelay="0" Font-Size="Large" AutoCloseDelay="0">
                                                Negociaciones
                                           
                                            </telerik:RadToolTip>
                                        </TabTemplate>
                                    </telerik:RadTab>
                                    <telerik:RadTab Value="7" Visible='<%# tmpPermisos("MENU_DOCUMENTOS") %>' SelectedCssClass="w3-text-blue">
                                        <TabTemplate>
                                            <div runat="server" id="rt1" class="w3-block w3-left-align w3-tiny">
                                                <img src="Imagenes/icon-navbar.png" alt="img" />
                                                Documentos
                                           
                                            </div>
                                            <telerik:RadToolTip runat="server" RelativeTo="Element" ShowEvent="OnMouseOver" TargetControlID="rt1" Position="MiddleRight" ShowDelay="0" HideDelay="0" Font-Size="Large" AutoCloseDelay="0">
                                                Documentos
                                           
                                            </telerik:RadToolTip>
                                        </TabTemplate>
                                    </telerik:RadTab>
                                    <telerik:RadTab Value="12" Visible='<%# tmpPermisos("MENU_INVENTARIOS") %>' SelectedCssClass="w3-text-blue">
                                        <TabTemplate>
                                            <div runat="server" id="rt1" class="w3-block w3-left-align w3-tiny">
                                                <img src="Imagenes/icon-navbar.png" alt="img" />
                                                Inventarios
                                           
                                            </div>
                                            <telerik:RadToolTip runat="server" RelativeTo="Element" ShowEvent="OnMouseOver" TargetControlID="rt1" Position="MiddleRight" ShowDelay="0" HideDelay="0" Font-Size="Large" AutoCloseDelay="0">
                                                Inventarios
                                           
                                            </telerik:RadToolTip>
                                        </TabTemplate>
                                    </telerik:RadTab>
                                    <telerik:RadTab Value="8" Visible='<%# tmpPermisos("MENU_RELACIONADOS") %>' SelectedCssClass="w3-text-blue">
                                        <TabTemplate>
                                            <div runat="server" id="rt1" class="w3-block w3-left-align w3-tiny">
                                                <img src="Imagenes/icon-navbar.png" alt="img" />
                                                Relacionados
                                           
                                            </div>
                                            <telerik:RadToolTip runat="server" RelativeTo="Element" ShowEvent="OnMouseOver" TargetControlID="rt1" Position="MiddleRight" ShowDelay="0" HideDelay="0" Font-Size="Large" AutoCloseDelay="0">
                                                Créditos relacionados
                                           
                                            </telerik:RadToolTip>
                                        </TabTemplate>
                                    </telerik:RadTab>
                                    <telerik:RadTab Value="9" Visible='<%# tmpPermisos("MENU_CORREOS") %>' SelectedCssClass="w3-text-blue">
                                        <TabTemplate>
                                            <div runat="server" id="rt1" class="w3-block w3-left-align w3-tiny">
                                                <img src="Imagenes/icon-navbar.png" alt="img" />
                                                Correos
                                           
                                            </div>
                                            <telerik:RadToolTip runat="server" RelativeTo="Element" ShowEvent="OnMouseOver" TargetControlID="rt1" Position="MiddleRight" ShowDelay="0" HideDelay="0" Font-Size="Large" AutoCloseDelay="0">
                                                Envío de correos
                                           
                                            </telerik:RadToolTip>
                                        </TabTemplate>
                                    </telerik:RadTab>
                                    <telerik:RadTab Value="15" Visible='<%# tmpPermisos("MENU_JUDICIAL") %>' SelectedCssClass="w3-text-blue">
                                        <TabTemplate>
                                            <div runat="server" id="rt1" class="w3-block w3-left-align w3-tiny">
                                                <img src="Imagenes/icon-navbar.png" alt="img" />
                                                Judicial
                                           
                                            </div>
                                            <telerik:RadToolTip runat="server" RelativeTo="Element" ShowEvent="OnMouseOver" TargetControlID="rt1" Position="MiddleRight" ShowDelay="0" HideDelay="0" Font-Size="Large" AutoCloseDelay="0">
                                                Judicial
                                           
                                            </telerik:RadToolTip>
                                        </TabTemplate>
                                    </telerik:RadTab>
                                    <telerik:RadTab Value="11" Visible='True'>
                                        <TabTemplate>
                                            <div runat="server" id="rt1" class="w3-block w3-left-align w3-tiny">
                                                <img src="Imagenes/icon-navbar.png" alt="img" />
                                                Garantias
                                            </div>
                                            <telerik:RadToolTip runat="server" RelativeTo="Element" ShowEvent="OnMouseOver" TargetControlID="rt1" Position="MiddleRight" ShowDelay="0" HideDelay="0" Font-Size="Large" AutoCloseDelay="0">
                                                Garantias
                                           
                                            </telerik:RadToolTip>
                                        </TabTemplate>
                                    </telerik:RadTab>
                                    <telerik:RadTab Value="10" Visible='<%# tmpPermisos("MENU_MANTENIMIENTO") %>'>
                                        <TabTemplate>
                                            <div runat="server" id="rt1">
                                                <img src="Imagenes/Mantenimiento.png" alt="img" />
                                            </div>
                                            <telerik:RadToolTip runat="server" RelativeTo="Element" ShowEvent="OnMouseOver" TargetControlID="rt1" Position="MiddleRight" ShowDelay="0" HideDelay="0" Font-Size="Large" AutoCloseDelay="0">
                                                Mantenimiento
                                           
                                            </telerik:RadToolTip>
                                        </TabTemplate>
                                    </telerik:RadTab>

                                </Tabs>
                            </telerik:RadTabStrip>
                        </div>
                        <hr />
                        <asp:Panel runat="server" ID="PnlAccesorios">
                            <telerik:RadButton runat="server" ID="BtnChat" CssClass="w3-border-0 w3-bar w3-margin" Height="40px" Width="40px" Visible="false"><%--<%# tmpPermisos("CHAT") %>--%>
                                <Image ImageUrl="Imagenes/msj.png" />
                            </telerik:RadButton>
                            <telerik:RadToolTip runat="server" RelativeTo="Element" ShowEvent="OnMouseOver" TargetControlID="BtnChat" Position="MiddleRight" ShowDelay="0" HideDelay="0" Font-Size="Large" AutoCloseDelay="0">
                                Mensajes
                           
                           
                           
                           
                            </telerik:RadToolTip>
                            <telerik:RadButton runat="server" ID="BtnCalculadora" CssClass="w3-border-0 w3-bar w3-margin" Height="35px" Width="35px" Visible='<%# tmpPermisos("CALCULADORA") %>'>
                                <Image ImageUrl="Imagenes/calc.png" />
                            </telerik:RadButton>
                            <telerik:RadToolTip runat="server" RelativeTo="Element" ShowEvent="OnMouseOver" TargetControlID="BtnCalculadora" Position="MiddleRight" ShowDelay="0" HideDelay="0" Font-Size="Large" AutoCloseDelay="0">
                                Calculadora
                           
                           
                           
                           
                            </telerik:RadToolTip>
                            <telerik:RadButton runat="server" ID="BtnChatWhat"  CssClass="w3-border-0 w3-bar w3-margin" Height="35px" Width="35px" Visible='<%# tmpPermisos("CHAT_WHATSAPP") %>'>
                                <Image ImageUrl="Imagenes/LogoWhatsapp.png" />
                            </telerik:RadButton>
                            <telerik:RadToolTip runat="server" RelativeTo="Element" ShowEvent="OnMouseOver" TargetControlID="BtnChatWhat" Position="MiddleRight" ShowDelay="0" HideDelay="0" Font-Size="Large" AutoCloseDelay="0">
                                Chat Whatsapp
                                
                            </telerik:RadToolTip>
                         
                             <%--<asp:Image runat="server" ID="Image1" Style="max-height: 20px; height: 100%" AlternateText="Mensaje Nuevo" ImageUrl="Imagenes/LogoWhatsapp.png" />--%>
                            <%--<button id="WHATSAPP" class="w3-border-0 w3-bar w3-margin" onclick="document.getElementById('PnlChatWhat').style.display='block';return false;" >
                                <img src="Imagenes/LogoWhatsapp.png" height="35px" width="35px"/>
                            </button>--%>
                            

                            <telerik:RadButton runat="server" ID="BtnNota" CssClass="w3-border-0 w3-bar w3-margin" Height="40px" Width="40px" Visible='<%# tmpPermisos("NOTAS") %>'>
                                <Image ImageUrl="Imagenes/nota.png" />
                            </telerik:RadButton>
                            <telerik:RadToolTip runat="server" RelativeTo="Element" ShowEvent="OnMouseOver" TargetControlID="BtnNota" Position="MiddleRight" ShowDelay="0" HideDelay="0" Font-Size="Large" AutoCloseDelay="0">
                                Notas
                           
                           
                           
                           
                            </telerik:RadToolTip>
                        </asp:Panel>
                    </div>
                    <div class="w3-rest">
                        <!-- jeder -->

                        <div class="w3-row-padding w3-container w3-margin-top">
                            <div class="w3-third">
                                <!-- La etiqueta #= # sirve para ejecutar instrucciones de javascript.
                                            Básicamente funciona como un dropdown que tiene Text y Value.
                                            En el Text viene toda la informacion separada por "$$" y se separa para mostrarla
                                            ordenadamente.
                                            En value viene solamente el credito.
                                            -->
                                <telerik:RadSearchBox runat="server" ID="rsb1" EmptyMessage="Buscar..." OnSearch="rsb1_Search" MaxResultCount="15" ShowSearchButton="true" Style="width: 100%">
                                    <WebServiceSettings Path="MasterPage.aspx" Method="GetResults" />
                                    <DropDownSettings Width="350px" Height="450px" CssClass="w3-card-4">
                                        <ClientTemplate>                                        
                                        <div class="w3-card-4">
                                            <div class="w3-container w3-blue w3-center">
                                                <b>#= Text.split("$$")[1] #</b>
                                            </div>
                                            <div class="w3-container w3-center">
                                                <b>Crédito:</b> #= Text.split("$$")[0] #
                                                <br/>
                                                <b>RFC:</b> #= Text.split("$$")[2] #
                                                <br />
                                                <b>Producto:</b> #= Text.split("$$")[4] #
                                                <br />
                                                <b>Teléfono(s):</b> #= Text.split("$$")[3] #
                                                <br />
                                                <b>Folio:</b> #= Text.split("$$")[5] #
                                                <br />
                                                <b>No. Cliente:</b> #= Text.split("$$")[6] #
                                                <br />
                                                <b>Expediente Judicial</b> #= Text.split("$$")[7] #
                                            </div>
                                        </div>
                                        <br/>
                                        </ClientTemplate>
                                    </DropDownSettings>
                                </telerik:RadSearchBox>
                            </div>
                            <div class="w3-third">
                                <div class="w3-center"><b style="font-size: 2em">Módulo de Gestión</b></div>
                            </div>
                            <div class="w3-third">

                                <asp:Panel ID="pnllbls" runat="server" CssClass="w3-left w3-text-large">
                                    <telerik:RadLabel runat="server" Text="Gestiones:" CssClass="w3-right">
                                        <telerik:RadLabel runat="server" ID="LblGESTIONES" CssClass="w3-text-green  w3-right"></telerik:RadLabel>
                                    </telerik:RadLabel>
                                    <telerik:RadLabel runat="server" Text="Promesas:" CssClass="w3-right">
                                        <telerik:RadLabel runat="server" ID="LblCUANTASPP" CssClass="w3-text-green w3-right"></telerik:RadLabel>
                                    </telerik:RadLabel>
                                    <telerik:RadLabel runat="server" Text="Monto:" CssClass="w3-right">
                                        <telerik:RadLabel runat="server" ID="LblMONTOPP" CssClass="w3-text-green w3-right"></telerik:RadLabel>
                                    </telerik:RadLabel>
                                </asp:Panel>
                                <img src="Imagenes/MC_Logo.png" class="w3-right" style="max-height: 40px; height: 100%" alt="LogoMC" />
                            </div>



                            <%--<a href="../PDFs/">http://localhost:25095/PDFs/</a>--%></div>

                        <!-- Barra de información rápida -->
                        <asp:Panel runat="server" CssClass="w3-container w3-center w3-text-large" ID="PnlInfoRapida">
                            <label>Nombre:</label>
                            <telerik:RadLabel runat="server" CssClass="w3-center" ID="LblName" Style="display: inline"></telerik:RadLabel>
                            <label style="display: inline">Crédito:</label>
                            <telerik:RadLabel runat="server" CssClass="w3-center" ID="LblCredit" Style="display: inline"></telerik:RadLabel>
                            <label style="display: inline">Folio:</label>
                            <telerik:RadLabel runat="server" CssClass="w3-center" ID="LblFolio" Style="display: inline"></telerik:RadLabel>
                            <label style="display: inline">Asignacion:</label>
                            <telerik:RadLabel runat="server" CssClass="w3-center" ID="LblDteAsignacion" Style="display: inline"></telerik:RadLabel>
                            <label style="display: inline">Despacho:</label>
                            <telerik:RadLabel runat="server" CssClass="w3-center" ID="LblDespacho" Style="display: inline"></telerik:RadLabel>
                            <label style="display: inline">Asignado a:</label>
                            <telerik:RadLabel runat="server" CssClass="w3-center" ID="LblAbogado" Style="display: inline"></telerik:RadLabel>
                            <label style="display: inline">Exclusión:</label>
                            <telerik:RadLabel runat="server" CssClass="w3-center" ID="LblExclusion" Style="display: inline"></telerik:RadLabel>
                        </asp:Panel>
                        <!-- Barra de credito retirado -->
                        <div class="w3-container w3-center">
                            <telerik:RadLabel runat="server" CssClass="w3-red w3-center w3-block" ID="LblRetirado" Text="Crédito retirado"></telerik:RadLabel>
                        </div>
                        <!--<div class="w3-container w3-card w3-margin w3-padding w3-hide" id="divGestion">-->
                        <telerik:RadAjaxPanel runat="server" CssClass="w3-card w3-margin w3-row-padding" ID="pnlGestion" Visible="false">
                            <div class="w3-half">
                                <!-- Div de Captura de gestion -->
                                <div class="w3-row-padding">
                                    <div class="w3-half">
                                        <%-- Combo Accion --%>
                                        <label>Acción:</label>
                                        <telerik:RadComboBox ID="DdlHist_Ge_Accion" runat="server" EmptyMessage="Seleccione..." Width="100%" AutoPostBack="true" Enabled="true"></telerik:RadComboBox>

                                        <%-- Combo Resultado --%>
                                        <label>Resultado:</label>
                                        <telerik:RadComboBox ID="DdlHist_Ge_Resultado" runat="server" EmptyMessage="Seleccione..." AutoPostBack="true" Width="100%" Enabled="false"></telerik:RadComboBox>

                                        <%-- No Pago --%>
                                        <label style="display: none">No. Pago:</label>
                                        <telerik:RadDropDownList ID="DdlHist_Ge_NoPago" runat="server" DefaultMessage="Seleccione..." Width="100%" AutoPostBack="true" Enabled="false" Visible="false"></telerik:RadDropDownList>

                                        <%-- Fecha Siguiente contacto --%>
                                            <telerik:RadLabel ID="LblHist_Pr_Dtepromesa" runat="server" Text="Siguiente Contacto"></telerik:RadLabel>
                                            <telerik:RadDateTimePicker runat="server" ID="TxtHist_Pr_Dtepromesa" DateInput-DateFormat="dd/MMM/yyyy hh:mm tt" Width="100%" EnableTyping="false" TimeView-HeaderText="Hora" TimeView-StartTime="07:00" TimeView-EndTime="22:00"></telerik:RadDateTimePicker>
                                        
                                        <asp:Panel runat="server" ID="PnlSigContacto" CssClass="w3-container">
                                            <telerik:RadLabel ID="LblHist_Parentesco" runat="server" Text="Parentesco"></telerik:RadLabel>
                                            <telerik:RadDropDownList ID="DDLHist_Parentesco" runat="server" DefaultMessage="Seleccione..." Width="100%" AutoPostBack="true"></telerik:RadDropDownList>
                                        </asp:Panel>

                                        <%-- panel monto pago y tipo pago --%>
                                        <asp:Panel runat="server" ID="PnlMontoPP" CssClass="w3-container">
                                            <label>Monto:</label>
                                            <telerik:RadNumericTextBox ID="TxtHist_Pr_Montopp" runat="server" MaxLength="10" Width="100%" Type="Currency" MinValue="0" autocomplete="false" AutoCompleteType="Disabled"></telerik:RadNumericTextBox>
                                            <div class="w3-container">
                                                <label style="display: block">Tipo de pago :</label>
                                                 <telerik:RadComboBox ID="DDLTipoPago" runat="server" Width="100%" EmptyMessage="Seleccione" AutoPostBack="true">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="Seleccione" Value="Seleccione" />
                                                        <telerik:RadComboBoxItem Text="Parcial" Value="Parcial" />
                                                        <telerik:RadComboBoxItem Text="Liquidacion Pago Unico" Value="Liquidacion Pago Unico" />
                                                        <telerik:RadComboBoxItem Text="Liquidacion En Exhibiciones" Value="Liquidacion En Exhibiciones" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </div>
                                        </asp:Panel>

                                        <%-- panel Crendenciales --%>
                                        <asp:Panel runat="server" ID="PnlCredenciales" CssClass="w3-container" Visible="false">
                                            <label style="display: block">Usuario:</label>
                                            <telerik:RadTextBox ID="TXTusr" runat="server" MaxLength="10" Width="100%"></telerik:RadTextBox>
                                            <label style="display: block">Contraseña:</label>
                                            <telerik:RadTextBox ID="TXTpwd" runat="server" MaxLength="25" TextMode="Password" Width="100%"></telerik:RadTextBox>
                                        </asp:Panel>

                                        <%-- check Llamada de entrada --%>
                                        <telerik:RadCheckBox ID="CbxHist_Ge_Inoutbound" runat="server" Width="100%" Text="Llamada de entrada" AutoPostBack="false">
                                        </telerik:RadCheckBox>

                                        <%-- telefono de contacto --%>
                                        <label style="display: block">Teléfono de contacto:</label>
                                        <telerik:RadTextBox ID="TxtHist_Ge_Telefono" runat="server" ReadOnly="true" CssClass="w3-input" Width="100%" EmptyMessage="Selecciona un telefono de la tabla"></telerik:RadTextBox>

                                        <%-- Combo Participante --%>
                                        <%--<label style="display: block">Participante:</label>
                                        <telerik:RadComboBox ID="DDLParticipante" runat="server" Width="100%" EmptyMessage="Seleccione"></telerik:RadComboBox>--%>
                                    </div>
                                    <div class="w3-half">
                                        <label style="display: block">Comentario:</label>
                                        <telerik:RadTextBox ID="TxtHist_Ge_Comentario" runat="server" Height="155px" MaxLength="500" TextMode="MultiLine" CssClass="w3-input" Width="100%"></telerik:RadTextBox>
                                        <br />
                                        <telerik:RadGrid runat="server" ID="rGvReferencias">
                                            <MasterTableView Font-Size="X-Small">
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                </div>

                                <!-- div de boton Guardar -->
                                <div class="w3-row w3-center w3-padding-24">
                                    <asp:Button ID="BtnGuardar" runat="server" CssClass="w3-btn w3-green" Text="Guardar" />
                                </div>
                            </div>
                            <!-- Div de Calificacion telefonica -->
                            <div class="w3-half">
                                <div class="w3-container w3-blue w3-center">
                                    <b>Calificación telefónica</b>
                                </div>
                                <div class="w3-container w3-responsive" style="overflow: auto;">
                                    <telerik:RadGrid ID="GvCalTelefonos" runat="server" OnNeedDataSource="GvCalTelefonos_NeedDataSource" AutoGenerateColumns="False" ClientSettings-EnableRowHoverStyle="true" Visible="True" PageSize="3" AllowPaging="True" PagerStyle-Mode="NextPrev" GridLines="none" CssClass="w3-table-all w3-hoverable">
                                        <MasterTableView>
                                            <Columns>
                                                <telerik:GridTemplateColumn UniqueName="T0" HeaderText="Calif.">
                                                    <ItemStyle CssClass="w3-padding-0 w3-margin-0 w3-center" />
                                                    <ItemTemplate>
                                                        <telerik:RadButton runat="server" ID="BtnValido" CssClass="w3-btn" Style="border-radius: 0px; border: 0px solid black; margin: 0px; padding: 0px;" OnClick="Valido_Click">
                                                            <ContentTemplate>
                                                                <img alt="OK" src="Imagenes/icons8-buena-calidad.png" class="w3-right" />
                                                            </ContentTemplate>
                                                        </telerik:RadButton>
                                                        <telerik:RadToolTip runat="server" RelativeTo="Element" ShowEvent="OnMouseOver" TargetControlID="BtnValido" Position="MiddleRight" ShowDelay="0" HideDelay="0" Font-Size="Large" AutoCloseDelay="0">
                                                            <%# Eval("CALPOS") & ", Último contacto: " & Eval("DTECALPOS")%>
                                                        </telerik:RadToolTip>
                                                        <telerik:RadButton runat="server" ID="BtnNoValido" CssClass="w3-btn" Style="border-radius: 0px; border: 0px solid black; margin: 0px; padding: 0px;" OnClick="NoValido_Click">
                                                            <ContentTemplate>
                                                                <img alt="OK" src="Imagenes/icons8-mala-calidad.png" class="w3-center" />
                                                            </ContentTemplate>
                                                        </telerik:RadButton>
                                                        <telerik:RadToolTip runat="server" RelativeTo="Element" ShowEvent="OnMouseOver" TargetControlID="BtnNoValido" Position="MiddleRight" ShowDelay="0" HideDelay="0" Font-Size="Large" AutoCloseDelay="0">
                                                            <%# Eval("CALNE") & ", Último contacto: " & Eval("DTECALNE")%>
                                                        </telerik:RadToolTip>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn SortExpression="TELEFONO" UniqueName="T1" HeaderText="Teléfono">
                                                    <ItemTemplate>
                                                        <telerik:RadLabel runat="server" ID="LblTELEFONO" Text='<%#Eval("TELEFONO")%>'></telerik:RadLabel>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn SortExpression="NOMBRE" UniqueName="T4" HeaderText="Nombre">
                                                    <ItemTemplate>
                                                        <div style="height: 1.4em; overflow: auto">
                                                            <telerik:RadLabel runat="server" ID="LblNOMBRE" Text='<%#Eval("NOMBRE")%>'></telerik:RadLabel>
                                                        </div>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn UniqueName="Msj" HeaderText="Mensaje" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="w3-center">
                                                    <ItemTemplate>
                                                        <telerik:RadButton runat="server" ID="BtnMsj" Enabled='<%#validarSMS(1, Eval("TELEFONO"))%>' Style="border-radius: 0px; border: 0px solid black; margin: 0px; padding: 0px;" OnClick="validaSMS_Click" CssClass="w3-btn">
                                                            <ContentTemplate>
                                                                <img alt="Msj" src='<%#validarSMS(0, Eval("TELEFONO"))%>' style="width: auto;" class="w3-center" />
                                                            </ContentTemplate>
                                                        </telerik:RadButton>
                                                        <telerik:RadToolTip runat="server" RelativeTo="Element" ShowEvent="OnMouseOver" TargetControlID="BtnMsj" Position="MiddleRight" ShowDelay="0" HideDelay="0" Font-Size="Large" AutoCloseDelay="0">
                                                            <%#validarSMS(2, Eval("TELEFONO"))%>
                                                        </telerik:RadToolTip>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn Visible="false">
                                                    <ItemTemplate>
                                                        <telerik:RadLabel runat="server" ID="LblTIPO" Text='<%#Eval("TIPO")%>'></telerik:RadLabel>
                                                        <telerik:RadLabel runat="server" ID="LblFUENTE" Text='<%#Eval("FUENTE")%>'></telerik:RadLabel>
                                                        <telerik:RadLabel runat="server" ID="LblVALIDO" Text='<%#Eval("VALIDO")%>'></telerik:RadLabel>
                                                        <telerik:RadLabel runat="server" ID="LblCAMPOPOS" Text='<%#Eval("CAMPOPOS")%>'></telerik:RadLabel>
                                                        <telerik:RadLabel runat="server" ID="LblCAMPONES" Text='<%#Eval("CAMPONES")%>'></telerik:RadLabel>
                                                        <telerik:RadLabel runat="server" ID="LblCAMPODTEPOS" Text='<%#Eval("CAMPODTEPOS")%>'></telerik:RadLabel>
                                                        <telerik:RadLabel runat="server" ID="LblCAMPODTENES" Text='<%#Eval("CAMPODTENES")%>'></telerik:RadLabel>
                                                        <telerik:RadLabel runat="server" ID="LblTABLA" Text='<%#Eval("TABLA")%>'></telerik:RadLabel>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </div>
                                <div style="display: block">
                                    <asp:Button ID="BtnRecargar" runat="server" Text="Recargar telefonos" />
                                </div>
                            </div>
                        </telerik:RadAjaxPanel>
                        <!--</div>-->
                        <!-- principal -->
                        <div>
                            <telerik:RadWindow runat="server" ID="WindowAcciones" MinWidth="500px" MinHeight="200px" RestrictionZoneID="ContentTemplateZone" Modal="false" Behaviors="None" AutoSize="true">
                                <ContentTemplate>
                                    <telerik:RadAjaxPanel ID="PnlModalAcciones" runat="server" CssClass="ModalAcciones" LoadingPanelID="RadUPNL">
                                        <telerik:RadFormDecorator runat="server" DecoratedControls="all" DecorationZoneID="AddResInAsoc"></telerik:RadFormDecorator>
                                        <div class="Titulos">
                                            <asp:Label ID="LblTitulo" runat="server"></asp:Label>
                                        </div>
                                        <div class="container" id="AddResInAsoc" style="width: 400px;">
                                            <div class="card-columns">
                                                <div class="card" style="width: 300px">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <telerik:RadLabel runat="server" ID="teltext" Text="Telefono:"></telerik:RadLabel>
                                                                <telerik:RadLabel runat="server" ID="telefonoselect" Text=""></telerik:RadLabel>
                                                                <telerik:RadLabel runat="server" ID="RLBPlantillaSMS" Text="Seleccione plantilla SMS"></telerik:RadLabel>
                                                                <telerik:RadComboBox runat="server" ID="RCBPlantillaSMS" AutoPostBack="true" EmptyMessage="Seleccione Plantilla">
                                                                </telerik:RadComboBox>
                                                                <br />
                                                                <telerik:RadLabel runat="server" ID="RLBMessageSMS" Text="Vista De SMS Preliminar"></telerik:RadLabel>
                                                                <telerik:RadTextBox runat="server" ID="RTBMessageSMS" Enabled="false" TextMode="MultiLine" Rows="10"></telerik:RadTextBox>

                                                            </td>
                                                            <td>

                                                                <telerik:RadButton runat="server" ID="RBTEnviarSMS" Text="Enviar SMS" Enabled="false"></telerik:RadButton>


                                                            </td>
                                                            <td>

                                                                <br />


                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div class="center">

                                                    <asp:Button ID="BtnCancelarAcciones" runat="server" CssClass="btn btn-danger" Text="Cancelar" />
                                                </div>
                                            </div>
                                        </div>
                                    </telerik:RadAjaxPanel>
                                </ContentTemplate>
                            </telerik:RadWindow>
                            </div>
                        <div class="w3-panel w3-margin w3-padding w3-card-4">
                            <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="2">
                                <telerik:RadPageView ID="RadPageViewA" runat="server" Height="650px">
                                    <div class="w3-container w3-center">
                                        <img src="Imagenes/MC_Logo.png" alt="MC Collect" />
                                        <telerik:RadButton runat="server" ID="BtnDescargar" Text="Descargar PDF" AutoPostBack="true" Visible="false"></telerik:RadButton>
                                    </div>
                                </telerik:RadPageView>
                                <telerik:RadPageView ID="RadPageViewB" runat="server" Height="600px" ContentUrl="InformacionGeneral.aspx"></telerik:RadPageView>
                                <telerik:RadPageView ID="RadPageViewD" runat="server" Height="600px" ContentUrl="InformacionAdicional.aspx"></telerik:RadPageView>
                                <telerik:RadPageView ID="RadPageViewI" runat="server" Height="600px" ContentUrl="DatosEmpleo.aspx"></telerik:RadPageView>
                                <telerik:RadPageView ID="RadPageViewE" runat="server" Height="600px" ContentUrl="Historicos_De_Actividades.aspx"></telerik:RadPageView>
                                <telerik:RadPageView ID="RadPageViewF" runat="server" Height="600px" ContentUrl="Hist_Pagos.aspx"></telerik:RadPageView>
                                <telerik:RadPageView ID="RadPageViewG" runat="server" Height="600px" ContentUrl="Negociaciones.aspx"></telerik:RadPageView>
                                <telerik:RadPageView ID="RadPageView2" runat="server" Height="600px" ContentUrl="Documentos.aspx"></telerik:RadPageView>
                                <telerik:RadPageView ID="RadPageViewJ" runat="server" Height="600px" ContentUrl="Inventarios.aspx"></telerik:RadPageView>
                                <telerik:RadPageView ID="RadPageViewK" runat="server" Height="600px" ContentUrl="Relacionados.aspx"></telerik:RadPageView>
                                <telerik:RadPageView ID="RadPageViewL" runat="server" Height="600px" ContentUrl="EnvioCorreos.aspx"></telerik:RadPageView>
                                <telerik:RadPageView ID="RadPageViewP" runat="server" Height="600px" ContentUrl="Judicial.aspx"></telerik:RadPageView>
                                <telerik:RadPageView ID="RadPageViewZ" runat="server" Height="600px" ContentUrl="Garantias.aspx"></telerik:RadPageView>
                                <telerik:RadPageView ID="RadPageViewQ" runat="server" Height="600px" ContentUrl="Mantenimiento.aspx"></telerik:RadPageView>
                            </telerik:RadMultiPage>
                        </div>
                    </div>
                </div>

                <div id="PnlUsuario" style="display: none" class="w3-modal">
                    <div class="w3-modal-content w3-card-4">
                        <div class="w3-panel w3-blue w3-center">
                            <span onclick="document.getElementById('PnlUsuario').style.display='none'"
                                class="w3-button w3-display-topright w3-hover-red" style="font-size: 150%">&times;</span>
                            <h2>
                                <label>
                                    Perfil de
                                   
                                    <telerik:RadLabel runat="server" ID="LblCat_Lo_Usuario" CssClass="w3-text-white"></telerik:RadLabel>
                                </label>
                            </h2>
                        </div>
                        <div class="w3-container w3-center w3-white">
                            <div class="w3-panel">
                                <img src="Imagenes/ImgLogo_Cliente.png" style="height: 100%;" alt="Logo" />
                            </div>
                            <div class="w3-panel w3-round-large w3-border">
                                <div class="w3-panel">
                                    <div class="w3-half"><b>Nombre</b></div>
                                    <div class="w3-half">
                                        <telerik:RadLabel runat="server" ID="LblCat_Lo_Nombre" CssClass="w3-hover-text-blue"></telerik:RadLabel>
                                    </div>
                                </div>
                                <div class="w3-panel">
                                    <div class="w3-half"><b>Perfil</b></div>
                                    <div class="w3-half">
                                        <telerik:RadLabel runat="server" ID="LblCat_Pe_Perfil" CssClass="w3-hover-text-blue"></telerik:RadLabel>
                                    </div>
                                </div>
                                <div class="w3-panel">
                                    <div class="w3-half"><b>Horario</b></div>
                                    <div class="w3-half">
                                        <telerik:RadLabel runat="server" ID="LblHorario" CssClass="w3-hover-text-blue"></telerik:RadLabel>
                                    </div>
                                </div>
                                <div class="w3-panel">
                                    <div class="w3-half"><b>Supervisor</b></div>
                                    <div class="w3-half">
                                        <telerik:RadLabel runat="server" ID="LblCat_Lo_Supervisor" CssClass="w3-hover-text-blue"></telerik:RadLabel>
                                    </div>
                                </div>
                              
                            </div>
                            <div class="w3-panel">
                                <telerik:RadButton ID="BtnModulo" runat="server" Text="Cerrar Módulo" Skin="" CssClass="w3-button w3-orange w3-hover-shadow"></telerik:RadButton>
                                <telerik:RadButton ID="BtnCerrar" runat="server" Text="Cerrar Sesión" Skin="" CssClass="w3-button w3-red w3-hover-shadow"></telerik:RadButton>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="PnlAvisosGral" style="display: none" class="w3-modal" runat="server">
                    <div class="w3-modal-content w3-card-4">
                        <div class="w3-panel w3-blue w3-center">
                            <%--<span onclick="document.getElementById('PnlAvisosGral').style.display='none'"
                                class="w3-button w3-display-topright w3-hover-red" style="font-size: 150%">&times;</span> --%>
                            <h1>
                                <label>
                                    Visualización de alertas                                    
                               
                                </label>
                            </h1>
                        </div>
                        <div class="w3-container">
                            <div class="w3-row-padding">
                                <div class="w3-col s12 m6 l3">
                                    <label>Tipo</label>
                                    <telerik:RadComboBox runat="server" ID="RCBTipoAvisos" CheckBoxes="false"></telerik:RadComboBox>
                                </div>
                                <div class="w3-col s12 m6 l2">
                                    <label>Tipo fecha</label>
                                    <telerik:RadComboBox runat="server" ID="RCBTipoFechaAvisos" CheckBoxes="false">
                                        <Items>
                                            <telerik:RadComboBoxItem Value="0" Text="Creación" Selected="true" />
                                            <telerik:RadComboBoxItem Value="1" Text="Lectura" />
                                            <telerik:RadComboBoxItem Value="2" Text="Vigencia" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>
                                <div class="w3-col s12 m6 l3">
                                    <label>Fecha inicio</label>
                                    <telerik:RadDatePicker ID="RDPFechaInicioAvisos" runat="server" CssClass="w3-input" Width="100%"></telerik:RadDatePicker>
                                </div>
                                <div class="w3-col s12 m6 l3">
                                    <label>Fecha fin</label>
                                    <telerik:RadDatePicker ID="RDPFechaFinAvisos" runat="server" CssClass="w3-input" Width="100%"></telerik:RadDatePicker>
                                </div>
                                <div class="w3-col s12 m12 l1">
                                    <telerik:RadButton ID="RBTraeAlertas" Text="Buscar" runat="server" SingleClick="true" SingleClickText="Buscando..."></telerik:RadButton>
                                </div>
                                <div class="w3-col s12 m12 l12">
                                    <telerik:RadGrid runat="server" ID="RGAlertasGral"></telerik:RadGrid>
                                </div>
                                <div class="w3-col s12 m12 l12">
                                    <telerik:RadButton ID="RBCerrarAlertas" Text="Cerrar" runat="server" SingleClick="true" SingleClickText="Cerrando..."></telerik:RadButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                
               
               
                <asp:Panel runat="server" ID="PnlWindow">
                    <div id="RestrictionZone"></div>
                    <telerik:RadWindowManager EnableShadow="true" OnClientResizeEnd="SetWindowBehavior" ID="RadWindowManager" Opacity="2" runat="server" Width="450" Height="400" KeepInScreenBounds="true" VisibleStatusbar="false">
                        <Windows>
                            <telerik:RadWindow ID="RadWindow1" Title="Notas" Behaviors="Close, Move, Resize,Maximize,Minimize" NavigateUrl="Nota.aspx" runat="server"></telerik:RadWindow>
                        </Windows>
                        <Windows>
                            <telerik:RadWindow ID="RadWindow2" Title="Calculadora" Behaviors="Close, Move, Resize ,Minimize" NavigateUrl="Calculadora.html" runat="server" Width="240" Height="320"></telerik:RadWindow>
                        </Windows>
                        <Windows>
                            <telerik:RadWindow ID="RadWindow3" Title="Gestion" Behaviors="Move, Resize,Maximize,Minimize" NavigateUrl="CapturaGestion.aspx" runat="server" Width="1000px" Height="350px"></telerik:RadWindow>
                        </Windows>
                        <Windows>
                            <telerik:RadWindow ID="RadWindow4" Title="Avisos" Behaviors="Move,Maximize,Close" NavigateUrl="Avisos.aspx" runat="server" Width="700px" Height="400px">
                            </telerik:RadWindow>
                        </Windows>
                        <Windows>
                            <telerik:RadWindow ID="RadWindow5" Title="ChatWhatsapp" Behaviors="Move,Maximize,Close" NavigateUrl="ChatWhatsapp.aspx" runat="server" Width="650px" Height="630px">
                            </telerik:RadWindow>
                        </Windows>
                    </telerik:RadWindowManager>
                </asp:Panel>
                <div>
                    <span id="mainLbl">1800</span>
                </div>
                <telerik:RadNotification ID="RadNotification1" runat="server" OnCallbackUpdate="OnCallbackUpdate2" LoadContentOn="PageLoad" AutoCloseDelay="60000" OnClientShowing="notification_showing" OnClientHidden="notification_hidden" Position="Center" Width="270" Height="100" Title="¿Continuar?" EnableRoundedCorners="true" ShowCloseButton="false" KeepOnMouseOver="false" CssClass="w3-card-4">
                    <ContentTemplate>
                        <div class="infoIcon">
                            <i class='fas fa-exclamation-triangle' style='color: #adaf00'></i>
                        </div>
                        <div class="notificationContent">
                            Tiempo para cerrar sesión:&nbsp; <span id="timeLbl">60</span>
                            <telerik:RadButton RenderMode="Lightweight" ID="continueSession" runat="server" Text="Continuar" CssClass="w3-btn w3-green" AutoPostBack="false" OnClientClicked="ContinueSession" Skin="">
                            </telerik:RadButton>
                        </div>
                    </ContentTemplate>
                </telerik:RadNotification>
                <telerik:RadNotification ID="Notificacion" runat="server" Position="Center" Width="330" Height="160" Animation="Fade" EnableRoundedCorners="true" EnableShadow="true" Style="z-index: 100000">
                </telerik:RadNotification>
                <script type="text/javascript" src="JS/sesionMejorado.js?v=1.1"></script>
                <script type="text/javascript">
                    //<![CDATA[
                    serverIDs({ notificationID: 'RadNotification1' });
                    //]]>
                </script>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
    <link rel="stylesheet" href="Estilos/w3.css" />
    <script src="JS/RadWindowMejorado.js?v=1" type="text/javascript" defer></script>
    <script src="../Scripts/PestanasMejorado.js?v=1.1" type="text/javascript" async></script>
    <script src="JS/FuncionesMejorado.js?v=1.3" type="text/javascript" async></script>
    <div class="w3-footer w3-large w3-center w3-dark-gray">
        <a href="#">Aviso de privacidad</a>
        <small>Powered by © 2019 MC Collect &reg</small>
    </div>
</body>
</html>
