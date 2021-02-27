<%@ Control Language="VB" AutoEventWireup="false" CodeFile="CatalogoAvisosPlantillas.ascx.vb" Inherits="M_Administrador_CatalogoAvisosPlantillas" %>
<div style="max-width: 100%; max-height: 90%; overflow: auto">
    <telerik:RadTextBox runat="server" ID="txtID" Visible="false"></telerik:RadTextBox>
    <div class="w3-row s6 m4">
        <label>
            Plantilla: 
           
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtNombre" ErrorMessage="* Inserte Nombre de la plantilla" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </label>
        <telerik:RadTextBox runat="server" ID="txtNombre" Width="70%"></telerik:RadTextBox>
    </div>
    <div class="w3-row-padding">
        <div class="w3-col s12 m6">
            <label>
                Instancia: 
           
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DdlInstancia" ErrorMessage="* Seleccione instancia" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
            </label>
            <telerik:RadDropDownList ID="DdlInstancia" runat="server" DefaultMessage="Seleccione" Width="100%">
                <Items>
                    <telerik:DropDownListItem Text="Administrativa" />
                    <telerik:DropDownListItem Text="Extrajudicial" />
                    <telerik:DropDownListItem Text="Judicial" />
                </Items>
            </telerik:RadDropDownList>
        </div>
        <div class="w3-col s12 m6">
            <label>Participante:
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="DdlRolParticipante" ErrorMessage="*Selecciona participante" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator></label>
            <telerik:RadDropDownList ID="DdlRolParticipante" runat="server" DefaultMessage="Seleccione" Width="100%">
                <Items>
                    <telerik:DropDownListItem Text="Deudor" />
                    <telerik:DropDownListItem Text="Codeudor" />
                    <telerik:DropDownListItem Text="Aval" />
                    <telerik:DropDownListItem Text="Representante Legal" />
                    <telerik:DropDownListItem Text="Garante Prendario" />
                    <telerik:DropDownListItem Text="Garante Hipotecario" />
                    <telerik:DropDownListItem Text="Principal Accionista" />
                    <telerik:DropDownListItem Text="Genérico" />
                </Items>
            </telerik:RadDropDownList>

        </div>
    </div>
    <telerik:RadComboBox runat="server" ID="comboEtiquetas" EmptyMessage="Seleccione" Label="Insertar Etiqueta:" OnClientSelectedIndexChanged="pasteAtCursorPos" DropDownAutoWidth="Enabled"></telerik:RadComboBox>
    <br />
    <label>Nota: La tabla de encabezado se encuentra por default en todas las plantillas de avisos y es fija. Aunque se elimine de la edición, aparacerá al momento de imprimirla.</label>
    <telerik:RadEditor ID="editor" runat="server" Width="100%" Language="es-ES" OnClientSelectionChange="OnClientSelectionChange" OnClientModeChange="OnClientModeChange" ContentFilters="DefaultFilters,MakeUrlsAbsolute" EditModes="Design, Html">
        <CssFiles>
            <telerik:EditorCssFile Value="../Estilos/w3.css" />
        </CssFiles>
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
      <asp:Button ID="exportpdf" Text="Vista Preliminar" runat="server"  OnClick="exportpdf_clic" visible="true" Enabled="true"/>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="editor" ErrorMessage="* La Plantilla no puede estar vacía" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
    
    <br />
    <telerik:RadButton runat="server" ID="btnGuardar" Text="Guardar" SingleClick="true" SingleClickText="Guardando..." CommandName='<%# IIf((TypeOf(Container) is GridEditFormInsertItem), "PerformInsert", "Update")%>'></telerik:RadButton>
    <telerik:RadButton runat="server" ID="btnCancelar" Text="Cancelar" CommandName="Cancel" CausesValidation="false"></telerik:RadButton>

    <telerik:RadScriptBlock runat="server">
        <script>

            var contenido = "<table cellspacing='0' cellpadding='0' style='border: none;' width='100%' border ='0'>" +
               "<tr>" +
               "<td><b><font size='1'>ID:</font></b></td>" +
               "<td><b><font size='1'>SALDO VENCIDO:</font></b></td>" +
               "<td><b><font size='1'>NÚMERO DE EMPLEADO:</font></b></td>" +
               "</tr>" +
               "<tr>" +
               "<td><b><font size='1'>NOMBRE:</font></b></td>" +
               "<td><b><font size='1'>DÍAS MORA:</font></b></td>" +
               "<td><b><font size='1'>FECHA DE IMPRESIÓN:</font></b></td>" +
               "</tr>" +
               "<tr>" +
               "<td><b><font size='1'>ROL:</font></b></td>" +
               "<td><b><font size='1'>FACTURAS:</font></b></td>" +
               "<td><b><font size='1'>NOMBRE USUARIO:</font></b></td>" +
               "</tr>" +
               "<tr>" +
               "<td><b><font size='1'>ACUERDO:</font></b></td>" +
               "<td><b><font size='1'>FRECUENCIA DE FACTURA:</font></b></td>" +
               "<td><b><font size='1'>JEFE DE AREA:</font></b></td>" +
               "</tr>" +
               "<tr>" +
               "<td><b><font size='1'>DOMICILIO:</font></b></td>" +
               "<td><b><font size='1'>MONTO PRÓXIMA FACTURA:</font></b></td>" +
               "<td><b><font size='1'>ÚLTIMA GESTIÓN:</font></b></td>" +
               "</tr>" +
               "<tr>" +
               "<td><b>&nbsp;</b></td>" +
               "<td><b><font size='1'>FECHA PRÓXIMA FACTURA:</font></b></td>" +
               "<td><b>&nbsp;</b></td>" +
               "</tr>" +
               "<tr>" +
               "<td><b><font size='1'>TITULAR:</font></b></td>" +
               "<td><b>&nbsp;</b></td>" +
               "<td><b>&nbsp;</b></td>" +
               "</tr>" +
               "<tr>" +
               "<td><b><font size='1'>OBSERVACIONES:</font></b></td>" +
               "<td><b>&nbsp;</b></td>" +
               "<td><b>&nbsp;</b></td>" +
               "</tr>" +
               "</table><br/>";
            var originalText = "/";
            

            function pasteAtCursorPos() {
                var combo = $find('<%= comboEtiquetas.ClientID %>');
            var data
            try {
                data = combo.get_selectedItem().get_text();//get the desired content
                combo.clearSelection();
            } catch (ex) {
                data = "--";
            }

            if (data != "--") {
                var editor = $find('<%= editor.ClientID %>');
                if (currentRange) {
                    currentRange.select(); //restore the selection
                }
                editor.pasteHtml(" &lt;&lt;" + data + "&gt;&gt; "); //paste content

            }
        }

        var currentRange = null;

        function OnClientSelectionChange(sender, args) {
            currentRange = sender.getDomRange(); //store current range/cursor position
        }

        function OnClientModeChange(sender, args) {
            var btnGuardar = $find('<%= btnGuardar.ClientID %>');
            var editor = $find('<%= editor.ClientID %>');
            var mode = sender.get_mode();
            var modeEnum = Telerik.Web.UI.EditModes;
            switch (mode) {
                case modeEnum.Html:
                    if (originalText != "/") {
                        editor.set_html(originalText);
                        originalText = "/";
                    }
                    btnGuardar.set_enabled(true);
                    break;
                case modeEnum.Design:
                    if (originalText != "/") {
                        editor.set_html(originalText);
                        originalText = "/";
                    }
                    btnGuardar.set_enabled(true);
                    break;
                case modeEnum.Preview:
                    originalText = editor.get_html();
                    editor.set_html(contenido + originalText);
                    btnGuardar.set_enabled(false);
                    break;
                default:
                    editor.set_html("");
                    btnGuardar.set_enabled(false);
                    break;
            }
        }
    </script>
    </telerik:RadScriptBlock>
</div>
