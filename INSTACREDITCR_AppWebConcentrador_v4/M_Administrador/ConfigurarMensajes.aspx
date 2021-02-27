<%@ Page Title="" Language="VB" MasterPageFile="./MasterPage.master" AutoEventWireup="false" CodeFile="ConfigurarMensajes.aspx.vb" Inherits="_ConfigurarMensajes" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPHMaster" runat="Server">
    <telerik:RadAjaxManager runat="server" ID="ajaxManager">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="gridMensajes">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlMensaje" />
                    <telerik:AjaxUpdatedControl ControlID="pnlMensajes" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlMensaje" />
                    <telerik:AjaxUpdatedControl ControlID="pnlMensajes" />
                    <telerik:AjaxUpdatedControl ControlID="RadNotification1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnCancelar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlMensaje" />
                    <telerik:AjaxUpdatedControl ControlID="pnlMensajes" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnNuevoMensaje">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlMensaje" />
                    <telerik:AjaxUpdatedControl ControlID="pnlMensajes" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel runat="server" ID="pnlLoading"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel runat="server" ID="pnlMensajes" LoadingPanelID="pnlLoading">
        <telerik:RadButton runat="server" ID="btnNuevoMensaje" Text="Crear mensaje"></telerik:RadButton>
        <telerik:RadGrid runat="server" ID="gridMensajes" AutoGenerateColumns="false">
            <MasterTableView>
                <Columns>
                    <telerik:GridButtonColumn ButtonType="PushButton" Text="Editar" CommandName="onEdit" HeaderStyle-Width="100px"></telerik:GridButtonColumn>
                    <telerik:GridButtonColumn ButtonType="PushButton" Text="Eliminar" CommandName="onDelete" ConfirmDialogType="RadWindow" ConfirmTitle="¿Eliminar?" ConfirmTextFormatString="¿Eliminar '{0}'? Esta acción no se puede deshacer" ConfirmTextFields="nombre" HeaderStyle-Width="100px"></telerik:GridButtonColumn>
                    <telerik:GridBoundColumn HeaderText="Mensaje" DataField="nombre"></telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
        <telerik:RadWindowManager runat="server" ID="windowManager"></telerik:RadWindowManager>
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxPanel runat="server" ID="pnlMensaje" LoadingPanelID="pnlLoading" Visible="false" CssClass="w3-text-black">
        <label>Nombre del mensaje</label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtNombre" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
        <telerik:RadTextBox runat="server" ID="txtNombre"></telerik:RadTextBox>
        <div class="w3-row">
            <div class="w3-col s12 m4">
                <label>Tipo de mensaje</label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="comboTipo" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                <telerik:RadComboBox runat="server" ID="comboTipo" Width="100%" EmptyMessage="Seleccione" >
                    <Items>
                        <telerik:RadComboBoxItem Text="Texto" Value="T" />
                        <telerik:RadComboBoxItem Text="Voz" Value="V" />
                    </Items>
                </telerik:RadComboBox>
            </div>
            <div class="w3-col s12 m4">
                <label>Camapaña</label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="comboCampana" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                <telerik:RadComboBox runat="server" ID="comboCampana" EmptyMessage="Seleccione" Width="100%"></telerik:RadComboBox>
            </div>
            <div class="w3-col s12 m4">
                <label>Rol:</label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="comboRol" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                <telerik:RadComboBox runat="server" ID="comboRol" EmptyMessage="Seleccione" Width="100%"></telerik:RadComboBox>
            </div>
        </div>
        <div class="w3-row">
            <div class="w3-col s12 m4">
                <label>Referencias</label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="comboReferencias" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                <telerik:RadComboBox runat="server" ID="comboReferencias" CheckBoxes="true" EmptyMessage="Seleccione" Width="100%"></telerik:RadComboBox>
            </div>
            <div class="w3-col s12 m4">
                <label>Mensaje</label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtMensaje" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                <!-- NO borrar la clase del TextBox ni el ClientEvent-->
                <telerik:RadTextBox runat="server" ID="txtMensaje" InputType="Text" TextMode="MultiLine" CssClass="txtMensaje" ClientEvents-OnLoad="contar" Width="100%" MaxLength="4000" Resize="Vertical"></telerik:RadTextBox>
            </div>
            <div class="w3-col s12 m4">
                <label>Caracteres: </label>
                <!-- NO borrar la clase del Label -->
                <telerik:RadLabel runat="server" ID="lblCaracteres" Text="0" CssClass="contador" Width="100%"></telerik:RadLabel>
            </div>
        </div>
        <telerik:RadButton runat="server" ID="btnGuardar" Text="Guardar"></telerik:RadButton>
        <telerik:RadButton runat="server" ID="btnCancelar" Text="Cancelar"></telerik:RadButton>

    </telerik:RadAjaxPanel>
    <telerik:RadNotification RenderMode="Lightweight" ID="RadNotification1" runat="server" Height="140px" Animation="Fade" EnableRoundedCorners="true" EnableShadow="true" AutoCloseDelay="3500" Position="Center" OffsetX="-30" OffsetY="-70" ShowCloseButton="true" VisibleOnPageLoad="false" LoadContentOn="EveryShow" KeepOnMouseOver="false">
    </telerik:RadNotification>
    <script type="text/javascript">
        function contar(e) {
            var texto = e._element.value;
            var len = texto.length;
            if (Math.floor(len / 200) > 0) document.getElementsByClassName("contador")[0].innerText = len + " (Mensaje dividido en " + Math.ceil(len / 200) + " partes)";
            else document.getElementsByClassName("contador")[0].innerText = len;
            document.getElementsByClassName("txtMensaje")[0].onkeyup = function () {
                var texto = e._element.value;
                var len = texto.length;
                if (Math.floor(len/200) > 0) document.getElementsByClassName("contador")[0].innerText = len + " (Mensaje dividido en " + Math.ceil(len/200) + " partes)";
                else document.getElementsByClassName("contador")[0].innerText = len;
            }
        }
    </script>
</asp:Content>