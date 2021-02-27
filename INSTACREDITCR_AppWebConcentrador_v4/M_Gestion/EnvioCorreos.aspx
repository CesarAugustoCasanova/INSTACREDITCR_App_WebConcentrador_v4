<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EnvioCorreos.aspx.vb" Inherits="EnvioCorreos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mc :: Modulo Gestión</title>
    <script src="JS/iFrameMejorado.js?v=2" type="text/javascript"></script>
    <meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />
    <link rel="stylesheet" href="Estilos/w3.css" />
</head>
<body style="font-size: .7em" class="scroll">
    <form id="form1" runat="server" onmousemove="window.parent.movement();">
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
        <telerik:RadScriptManager runat="server" ID="RadScriptManager1"></telerik:RadScriptManager>
        <telerik:RadAjaxManager runat="server" ID="RadAjaxManager1">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="DdlPlantilla">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="TxtPlantilla" />


                        <telerik:AjaxUpdatedControl ControlID="Notificacion" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>

        <div class="w3-container w3-center w3-blue">
            <b>Envio de Correos</b>
        </div>
        <div class="w3-center w3-block">
            <label>Plantilla</label>
            <telerik:RadDropDownList ID="DdlPlantilla" runat="server" AutoPostBack="true">
                <Items>
                    <telerik:DropDownListItem Text="Seleccione" Value="Seleccione" />
                </Items>
            </telerik:RadDropDownList>
            <label>Perfil de envío</label>
            <telerik:RadDropDownList ID="DDLPerfilMail" runat="server" >
                <Items>
                    <telerik:DropDownListItem Text="Seleccione" Value="Seleccione" />
                </Items>
            </telerik:RadDropDownList>
        </div>
        <telerik:RadEditor ID="TxtPlantilla" runat="server" Width="100%" Language="es-Es" EditModes="Preview" ContentFilters="DefaultFilters,MakeUrlsAbsolute">
            <ImageManager ViewPaths="~/AhorrosBienestarImagenes" UploadPaths="~/AhorrosBienestarImagenes" DeletePaths="~/AhorrosBienestarImagenes" EnableAsyncUpload="true"></ImageManager>
            <FontNames>
                <telerik:EditorFont Value="Arial" />
                <telerik:EditorFont Value="Tahoma" />
                <telerik:EditorFont Value="Calibri" />
                <telerik:EditorFont Value="Garamonhd" />
                <telerik:EditorFont Value="Georgia" />
                <telerik:EditorFont Value="MS Sans Serif" />
                <telerik:EditorFont Value="Times New Roman" />
                <telerik:EditorFont Value="Verdana" />
                <telerik:EditorFont Value="Courier" />
                <telerik:EditorFont Value="Broadway" />
                <telerik:EditorFont Value="Comic Sans MS" />
                <telerik:EditorFont Value="Constantia" />
                <telerik:EditorFont Value="Cambria" />
                <telerik:EditorFont Value="Calisto MT" />
                <telerik:EditorFont Value="Onyx Normal" />
                <telerik:EditorFont Value="Playbill Normal" />
                <telerik:EditorFont Value="Old English Text MT Normal" />
                <telerik:EditorFont Value="Rockwell" />
                <telerik:EditorFont Value="Stardust Adventure" />
            </FontNames>
            <Modules>
                <telerik:EditorModule Name="RadEditorNodeInspector" Enabled="true" Visible="true" />
            </Modules>
            <Tools>
                <telerik:EditorToolGroup Tag="MainToolbar">
                    <telerik:EditorTool Name="PageProperties" Visible="False" />
                    <telerik:EditorTool Name="StyleBuilder" Visible="False" />
                    <telerik:EditorTool Name="FormatCodeBlock" Visible="False" />
                    <telerik:EditorSeparator />
                    <telerik:EditorTool Name="InsertImage" />
                    <telerik:EditorTool Name="InsertLink" />
                    <telerik:EditorTool Name="InsertTableLight" />
                    <telerik:EditorSeparator />
                    <telerik:EditorToolStrip Name="InsertFormElement" Visible="False">
                    </telerik:EditorToolStrip>
                    <telerik:EditorTool Name="InsertFormForm" ShowIcon="False" Visible="False" />
                    <telerik:EditorTool Name="InsertFormButton" ShowIcon="False" Visible="False" />
                    <telerik:EditorTool Name="InsertFormCheckbox" ShowIcon="False" Visible="False" />
                    <telerik:EditorTool Name="InsertFormHidden" ShowIcon="False" Visible="False" />
                    <telerik:EditorTool Name="InsertFormPassword" ShowIcon="False" Visible="False" />
                    <telerik:EditorTool Name="InsertFormRadio" ShowIcon="False" Visible="False" />
                    <telerik:EditorTool Name="InsertFormReset" ShowIcon="False" Visible="False" />
                    <telerik:EditorTool Name="InsertFormSelect" ShowIcon="False" Visible="False" />
                    <telerik:EditorTool Name="InsertFormSubmit" ShowIcon="False" Visible="False" />
                    <telerik:EditorTool Name="InsertFormTextarea" />
                    <telerik:EditorTool Name="InsertFormText" />
                    <telerik:EditorSeparator />
                    <telerik:EditorTool Name="StripAll" Visible="False" />
                    <telerik:EditorTool Name="StripCss" Visible="False" />
                    <telerik:EditorTool Name="StripFont" Visible="False" />
                    <telerik:EditorTool Name="StripSpan" Visible="False" />
                    <telerik:EditorTool Name="StripWord" Visible="False" />
                </telerik:EditorToolGroup>
                <telerik:EditorToolGroup Tag="InsertToolbar">
                    <telerik:EditorTool Name="AjaxSpellCheck" Visible="False" />
                    <telerik:EditorTool Name="ImageManager" ShortCut="CTRL+M" />
                    <telerik:EditorTool Name="SetImageProperties" />
                    <telerik:EditorTool Name="ImageMapDialog" />
                    <telerik:EditorSeparator />
                    <telerik:EditorTool Name="FlashManager" Visible="False" />
                    <telerik:EditorTool Name="MediaManager" Visible="False" />
                    <telerik:EditorTool Name="InsertExternalVideo" Visible="False" />
                    <telerik:EditorSeparator />
                    <telerik:EditorTool Name="DocumentManager" Visible="False" />
                    <telerik:EditorTool Name="TemplateManager" Visible="False" />
                    <telerik:EditorTool Name="SilverlightManager" Visible="False" />
                    <telerik:EditorSeparator />
                    <telerik:EditorToolStrip Name="InsertTable">
                        <telerik:EditorTool Name="TableWizard" />
                    </telerik:EditorToolStrip>
                    <telerik:EditorTool Name="InsertRowAbove" />
                    <telerik:EditorTool Name="InsertRowBelow" />
                    <telerik:EditorTool Name="DeleteRow" />
                    <telerik:EditorTool Name="InsertColumnLeft" />
                    <telerik:EditorTool Name="InsertColumnRight" />
                    <telerik:EditorTool Name="DeleteColumn" />
                    <telerik:EditorSeparator />
                    <telerik:EditorTool Name="MergeColumns" />
                    <telerik:EditorTool Name="MergeRows" />
                    <telerik:EditorSeparator />
                    <telerik:EditorTool Name="SplitCell" />
                    <telerik:EditorTool Name="SplitCellHorizontal" />
                    <telerik:EditorTool Name="DeleteCell" />
                    <telerik:EditorTool Name="SetCellProperties" />
                    <telerik:EditorTool Name="SetTableProperties" />
                    <telerik:EditorSeparator />
                    <telerik:EditorSplitButton Name="InsertSymbol">
                    </telerik:EditorSplitButton>
                </telerik:EditorToolGroup>
                <telerik:EditorToolGroup>
                    <telerik:EditorSplitButton Name="Undo">
                    </telerik:EditorSplitButton>
                    <telerik:EditorSplitButton Name="Redo">
                    </telerik:EditorSplitButton>
                    <telerik:EditorSeparator />
                    <telerik:EditorTool Name="Cut" />
                    <telerik:EditorTool Name="Copy" />
                    <telerik:EditorTool Name="Paste" ShortCut="CTRL+!" />
                    <telerik:EditorTool Name="PasteMarkdown" />
                    <telerik:EditorSeparator />
                    <telerik:EditorTool Name="PasteFromWord" />
                    <telerik:EditorTool Name="PasteFromWordNoFontsNoSizes" />
                    <telerik:EditorTool Name="PastePlainText" />
                    <telerik:EditorTool Name="PasteAsHtml" ShowIcon="False" Visible="False" />
                    <telerik:EditorTool Name="PasteHtml" ShowIcon="False" Visible="False" />
                    <telerik:EditorSeparator />
                    <telerik:EditorTool Name="Print" />
                    <telerik:EditorTool Name="FindAndReplace" />
                    <telerik:EditorTool Name="SelectAll" />
                    <telerik:EditorSeparator />
                    <telerik:EditorTool Name="InsertGroupbox" />
                    <telerik:EditorTool Name="InsertParagraph" />
                    <telerik:EditorTool Name="InsertHorizontalRule" />
                    <telerik:EditorSeparator />
                    <telerik:EditorTool Name="InsertDate" />
                    <telerik:EditorTool Name="InsertTime" />
                    <telerik:EditorSeparator />
                    <telerik:EditorTool Name="AboutDialog" Visible="False" />
                    <telerik:EditorTool Name="Help" Visible="False" />
                    <telerik:EditorTool Name="ToggleScreenMode" />
                    <telerik:EditorTool Name="CSDialog" Visible="False" />
                </telerik:EditorToolGroup>
                <telerik:EditorToolGroup Tag="Formatting">
                    <telerik:EditorTool Name="Bold" />
                    <telerik:EditorTool Name="Italic" />
                    <telerik:EditorTool Name="Underline" />
                    <telerik:EditorTool Name="StrikeThrough" />
                    <telerik:EditorSplitButton Name="ForeColor">
                    </telerik:EditorSplitButton>
                    <telerik:EditorSplitButton Name="BackColor">
                    </telerik:EditorSplitButton>
                    <telerik:EditorTool Name="FormatPainter" />
                    <telerik:EditorSeparator />
                    <telerik:EditorTool Name="JustifyLeft" />
                    <telerik:EditorTool Name="JustifyCenter" />
                    <telerik:EditorTool Name="JustifyRight" />
                    <telerik:EditorTool Name="JustifyFull" />
                    <telerik:EditorTool Name="JustifyNone" />
                    <telerik:EditorSeparator />
                    <telerik:EditorTool Name="Superscript" />
                    <telerik:EditorTool Name="Subscript" />
                    <telerik:EditorSeparator />
                    <telerik:EditorTool Name="ConvertToLower" />
                    <telerik:EditorTool Name="ConvertToUpper" />
                    <telerik:EditorTool Name="Indent" />
                    <telerik:EditorTool Name="Outdent" />
                    <telerik:EditorTool Name="InsertOrderedList" />
                    <telerik:EditorTool Name="InsertUnorderedList" />
                    <telerik:EditorTool Name="AbsolutePosition" />
                    <telerik:EditorTool Name="LinkManager" />
                    <telerik:EditorTool Name="Unlink" />
                    <telerik:EditorTool Name="SetLinkProperties" />
                    <telerik:EditorTool Name="ToggleTableBorder" />
                </telerik:EditorToolGroup>
                <telerik:EditorToolGroup Tag="DropdownToolbar">
                    <telerik:EditorDropDown Name="FontName">
                    </telerik:EditorDropDown>
                    <telerik:EditorDropDown Name="FontSize">
                    </telerik:EditorDropDown>
                    <telerik:EditorDropDown Name="RealFontSize">
                    </telerik:EditorDropDown>
                    <telerik:EditorDropDown Name="ApplyClass" Visible="False" ShowIcon="False">
                    </telerik:EditorDropDown>
                    <telerik:EditorDropDown Name="FormatBlock">
                    </telerik:EditorDropDown>
                    <telerik:EditorTool Name="FormatSets" ShowIcon="False" Visible="False" />
                    <telerik:EditorDropDown Name="Zoom">
                    </telerik:EditorDropDown>
                </telerik:EditorToolGroup>
            </Tools>
            <Content>
                    
                    
                    

            </Content>
            <TrackChangesSettings CanAcceptTrackChanges="False" />
        </telerik:RadEditor>
        <br />

        <div class="w3-row" style="overflow: auto">
            <div class="w3-third"><hr /></div>
            <div class="w3-third w3-center">
                <telerik:RadGrid ID="GVCorreosCredito" runat="server" OnNeedDataSource="GVCorreosCredito_NeedDataSource" Visible="true" Width="500px" HeaderStyle-HorizontalAlign="Center">
                    <MasterTableView  CommandItemDisplay="Top" AllowPaging="true" PageSize="10" EditMode="PopUp">
                         
                             <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="true" />
                    
                        <Columns>
                            <telerik:GridTemplateColumn> 
                                <ItemTemplate>
                                    <telerik:RadButton ID="BtnEnviar" CssClass="w3-btn w3-hover-green" runat="server" Text="Enviar" OnClick="EnviarRes">
                                    </telerik:RadButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid></div>
            <div class="w3-third"><hr /></div>
            
        </div>
        <telerik:RadNotification ID="Notificacion" runat="server" Position="Center" Width="330" Height="300" Animation="Fade" EnableRoundedCorners="true" EnableShadow="true" Style="z-index: 100000">
        </telerik:RadNotification>
    </form>
</body>
</html>
