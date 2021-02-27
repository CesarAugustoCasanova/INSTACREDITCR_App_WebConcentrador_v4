<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Reglas.ascx.vb" Inherits="M_Monitoreo_Reglas" %>

<telerik:RadLabel runat="server" ID="lblConsecutivo" Visible="false" Text="0"></telerik:RadLabel>
<div class="w3-row-padding">
    <div class="w3-col s12 m3">
        <label>Campo<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="comboCampo" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator></label>
        <telerik:RadComboBox runat="server" ID="comboCampo" AutoPostBack="true" ShowWhileLoading="false" EmptyMessage="Selecciona un campo" Width="100%" CausesValidation="false"></telerik:RadComboBox>
    </div>
    <div class="w3-col s12 m3">
        <label>Condicion<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DdlOperador" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator></label>
        <telerik:RadComboBox ID="DdlOperador" runat="server" EmptyMessage="Seleccione..." Width="100%" CausesValidation="false" DataValueField="Value" DataTextField="Text">
        </telerik:RadComboBox>
    </div>
    <div class="w3-col s12 m3">
        <label>Valor para la condicion</label>
        <telerik:RadTextBox ID="TxtValores" runat="server" Width="100%"></telerik:RadTextBox>
        <telerik:RadDatePicker runat="server" ID="DteValores" Width="100%" Visible="false"></telerik:RadDatePicker>
        <telerik:RadNumericTextBox runat="server" ID="NumValores" Width="100%" Visible="false" AllowOutOfRangeAutoCorrect="true" MaxLength="10" NumberFormat-DecimalDigits="2"></telerik:RadNumericTextBox>
        <telerik:RadComboBox CheckBoxes="true" ID="RadMultiple" Visible="false" runat="server" DefaultMessage="Seleccione..." Width="100%" DataTextField="CAMPO" DataValueField="CAMPO">
        </telerik:RadComboBox>
    </div>
    <div class="w3-col s12 m3">
        <label>Seleccione un conector:</label>
        <telerik:RadComboBox ID="DdlConector" runat="server" AutoPostBack="false" EmptyMessage="Seleccione..." Width="100%">
            <Items>
                <telerik:RadComboBoxItem Value="AND" Text="Y" />
                <telerik:RadComboBoxItem Value="OR" Text="O" />
            </Items>
        </telerik:RadComboBox>
    </div>
</div>
<div class="w3-row-padding">
    <div class="w3-col s12 m3">
        <label>Seleccione Inicio o Fin de grupo:</label>
       <telerik:RadButton runat="server" ID="BtnInicioFin" ButtonType="ToggleButton" ToggleType="CustomToggle" AutoPostBack="false">
           <ToggleStates>
               <telerik:RadButtonToggleState Text="Ninguno" Value="0" />
               <telerik:RadButtonToggleState Text="Inicio" Value="1" />
               <telerik:RadButtonToggleState Text="Fin" Value="2" />
           </ToggleStates>
       </telerik:RadButton>
    </div>
     <div class="w3-col s12 m3">
        <label>Nivel de agrupacion:</label>
       <telerik:RadNumericTextBox runat="server" ID="NumNivel" MinValue="1" Type="Number" NumberFormat-DecimalDigits="0" ShowSpinButtons="true" DisplayText="1" Value="1"></telerik:RadNumericTextBox>
    </div>
</div>
<div class="container mt-2">
    <telerik:RadButton ID="btnInsert" Text="Guardar" runat="server" CommandName='<%# IIf((TypeOf(Container) is GridEditFormInsertItem), "PerformInsert", "Update")%>' AutoPostBack="true" CssClass="bg-primary text-white border-0" Width="150px"></telerik:RadButton>
    <telerik:RadButton ID="RBtnCacelar" runat="server" Text="Cancelar" CommandName="Cancel" CausesValidation="false" CssClass="bg-info text-white border-0" Width="150px"></telerik:RadButton>
</div>
