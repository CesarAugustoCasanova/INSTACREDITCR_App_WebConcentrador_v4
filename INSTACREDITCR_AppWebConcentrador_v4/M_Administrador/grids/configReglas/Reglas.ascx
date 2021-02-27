<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Reglas.ascx.vb" Inherits="M_Administrador_grids_configReglas_Reglas" %>
    <telerik:RadLabel runat="server" ID="lblConsecutivo" Visible="false" text="NA"></telerik:RadLabel>
    <div class="w3-row-padding">
        <div class="w3-col s12 m6">
            <label>Tabla
                <asp:RequiredFieldValidator ID="RequiredFieldValidator0" runat="server" ControlToValidate="comboTablas" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
            </label>
            <telerik:RadComboBox runat="server" ID="comboTablas" AutoPostBack="true" ShowWhileLoading="false" EmptyMessage="Selecciona una tabla" Width="100%"  CausesValidation="false"></telerik:RadComboBox>

        </div>
        <div class="w3-col s12 m6">
            <label>Campo<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="comboCampo" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator></label>
            <telerik:RadComboBox runat="server" ID="comboCampo" AutoPostBack="true" ShowWhileLoading="false" Enabled="false" EmptyMessage="Selecciona un campo" Width="100%"  CausesValidation="false"></telerik:RadComboBox>
        </div>
    </div>
    <div class="w3-row-padding">
        <div class="w3-col s12 m4">
            <label>Condicion<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DdlOperador" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator></label>
            <telerik:RadComboBox ID="DdlOperador" runat="server" EmptyMessage="Seleccione..." Width="100%"  CausesValidation="false">                
            </telerik:RadComboBox>
        </div>
        <div class="w3-col s12 m4">
            <label>Valor para la condicion</label>
            <telerik:RadTextBox ID="TxtValores" runat="server" Width="100%"></telerik:RadTextBox>
            <telerik:RadDatePicker runat="server" ID="DteValores" Width="100%" Visible="false"></telerik:RadDatePicker>
            <telerik:RadNumericTextBox runat="server" ID="NumValores" Width="100%" Visible="false" AllowOutOfRangeAutoCorrect="true" MaxLength="10" NumberFormat-DecimalDigits="2"></telerik:RadNumericTextBox>
            <telerik:RadComboBox  CheckBoxes="true" ID="RadMultiple"  Visible="false" runat="server"   DefaultMessage="Seleccione..." Width="100%">
        </telerik:RadComboBox>
        </div>
        <div class="w3-col s12 m4">
            <label>Seleccione un conector:</label>
            <telerik:RadComboBox ID="DdlConector" runat="server" AutoPostBack="false" EmptyMessage="Seleccione..." Width="100%">
                <Items>
                    <telerik:RadComboBoxItem Value="AND" Text="Y" />
                    <telerik:RadComboBoxItem Value="OR" Text="O" />
                </Items>
            </telerik:RadComboBox>
        </div>
    </div>
<telerik:RadButton ID="btnInsert" Text="Guardar" runat="server" CommandName='<%# IIf((TypeOf(Container) is GridEditFormInsertItem), "PerformInsert", "Update")%>' AutoPostBack="true">
</telerik:RadButton>
<telerik:RadButton ID="RBtnCacelar" runat="server" Text="Cancelar" CommandName="Cancel" CausesValidation="false"></telerik:RadButton>
