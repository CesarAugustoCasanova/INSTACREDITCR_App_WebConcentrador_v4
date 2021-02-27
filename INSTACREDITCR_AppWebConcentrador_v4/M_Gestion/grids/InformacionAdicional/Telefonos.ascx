<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Telefonos.ascx.vb" Inherits="M_Gestion_grids_InformacionAdicional_Telefonos" %>
<telerik:RadLabel runat="server" ID="LblConsecutivo" Visible="false" Text='<%#IIf(String.IsNullOrEmpty(Eval("CONSECUTIVO").ToString) Or String.IsNullOrWhiteSpace(Eval("CONSECUTIVO").ToString), -1, Eval("CONSECUTIVO").ToString) %>'></telerik:RadLabel>
<script>

    function ValidaLongitud(campo, longitudMaxima) {
        //valor = document.getElementById('RGTelefono_ctl00_ctl02_ctl03_EditFormControl_TB2').value;
        try {
            if (campo.value.length > (longitudMaxima - 1))
                return false;
            else
                return true;
        } catch (e) {
            return false;
        }
    }
</script>
<div class="w3-row-padding">
    <div class="w3-third">
        <label>Teléfono:</label>
        <telerik:RadTextBox runat="server" onkeypress="return ValidaLongitud(this, 10);" CssClass="w3-input" Width="100%"  MaxLength="10" ID="TB2" InputType="Number" AutoCompleteType="disabled" Text='<%# Eval("TELEFONO") %>' Enabled='<%# IIf((TypeOf (Container) Is GridEditFormInsertItem), True, False) %>'></telerik:RadTextBox>
    </div>
    <div class="w3-third">
        <label>Extensión:</label>
        <telerik:RadTextBox runat="server" CssClass="w3-input" Width="100%" ID="TB1" InputType="Number" AutoCompleteType="disabled" Text='<%# Eval("EXTENSION") %>'></telerik:RadTextBox>
    </div>
    <div class="w3-third">
        <label>Tipo:</label>
        <asp:DropDownList runat="server" CssClass="w3-select" ID="DDTipo" SelectedValue='<%#IIf(Eval("TIPO").ToString = "Domicilio o casa", "Casa", IIf(Eval("TIPO").ToString = "Celular", "Celular", IIf(Eval("TIPO").ToString = "Recados", "Recados", "N"))) %>'>
            <asp:ListItem Value="N" Text="N/A" Selected="True"></asp:ListItem>
            <asp:ListItem Value="Casa" Text="Casa"></asp:ListItem>
            <asp:ListItem Value="Celular" Text="Celular"></asp:ListItem>
            <asp:ListItem Value="Recados" Text="Recados"></asp:ListItem>
            <asp:ListItem Value="Empleo" Text="Empleo"></asp:ListItem>
            <asp:ListItem Value="Referencia" Text="Referencia"></asp:ListItem> 
        </asp:DropDownList>
    </div>
</div>
<div class="w3-row-padding w3-margin">
    <div class="w3-half">
        <label>Parentesco:</label>
        <asp:DropDownList runat="server" CssClass="w3-select" ID="DDParentesco" SelectedValue='<%# revisa2(Eval("PARENTESCO").ToString) %>' DataSource='<%# LlenarDrops(2) %>' TabIndex="7" AppendDataBoundItems="True">
            <asp:ListItem Selected="True" Text="Seleccione" Value="">
            </asp:ListItem>
        </asp:DropDownList>
    </div>
</div>
<div class="w3-row-padding w3-margin">
    <div class="w3-half">
        <label>Nombre:</label>
        <asp:TextBox runat="server" CssClass="w3-input" Width="100%" ID="TB12" Text='<%# Eval("NOMBRE") %>' AutoCompleteType="disabled"></asp:TextBox>
    </div>
    <div class="w3-half">
        <label>Nombre quien proporciona:</label>
        <asp:TextBox runat="server" CssClass="w3-input" Width="100%" ID="TB13" Text='<%# Eval("PROPORCIONA") %>' AutoCompleteType="disabled"></asp:TextBox>
    </div>
    <telerik:RadInputManager runat="server">
        <telerik:RegExpTextBoxSetting BehaviorID="RagExpnombre" Validation-IsRequired="true"
            ValidationExpression="[A-Za-z áéíóúü]+" ErrorMessage="Nombre inválido">
            <TargetControls>
                <telerik:TargetInput ControlID="TB12"></telerik:TargetInput>
                <telerik:TargetInput ControlID="TB13"></telerik:TargetInput>
            </TargetControls>
        </telerik:RegExpTextBoxSetting>
    </telerik:RadInputManager>
</div>
<div class="w3-row-padding w3-margin">
    <div class="w3-container">
        <label>Días:</label>
        <telerik:RadCheckBox runat="server" AutoPostBack="false" CssClass="w3-chcekbox" Text="Lunes" ID="RadCheckBox1" Checked='<%#quitaNull(Eval("LUN"))%>'></telerik:RadCheckBox>
        <telerik:RadCheckBox runat="server" AutoPostBack="false" CssClass="w3-chcekbox" Text="Martes" ID="RadCheckBox2" Checked='<%#quitaNull(Eval("MAR"))%>'></telerik:RadCheckBox>
        <telerik:RadCheckBox runat="server" AutoPostBack="false" CssClass="w3-chcekbox" Text="Miercoles" ID="RadCheckBox3" Checked='<%#quitaNull(Eval("MIE"))%>'></telerik:RadCheckBox>
        <telerik:RadCheckBox runat="server" AutoPostBack="false" CssClass="w3-chcekbox" Text="Jueves" ID="RadCheckBox4" Checked='<%#quitaNull(Eval("JUE"))%>'></telerik:RadCheckBox>
        <telerik:RadCheckBox runat="server" AutoPostBack="false" CssClass="w3-chcekbox" Text="Viernes" ID="RadCheckBox5" Checked='<%#quitaNull(Eval("VIE"))%>'></telerik:RadCheckBox>
        <telerik:RadCheckBox runat="server" AutoPostBack="false" CssClass="w3-chcekbox" Text="Sábado" ID="RadCheckBox6" Checked='<%#quitaNull(Eval("SAB"))%>'></telerik:RadCheckBox>
        <telerik:RadCheckBox runat="server" AutoPostBack="false" CssClass="w3-chcekbox" Text="Domingo" ID="RadCheckBox7" Checked='<%#quitaNull(Eval("DOM"))%>'></telerik:RadCheckBox>
        <label>Horario:</label>
        <telerik:RadTimePicker ID="TB14" runat="server" EnableTyping="false" DbSelectedDate='<%# Eval("HORA1") %>'></telerik:RadTimePicker>
        <label>a:</label>
        <telerik:RadTimePicker ID="TB15" runat="server" EnableTyping="false" DbSelectedDate='<%# Eval("HORA2") %>'></telerik:RadTimePicker>
        <label>¿Activo?</label>
        <telerik:RadCheckBox ID="RCBContacto" AutoPostBack="false" runat="server" Checked='<%#quitaNull2(Eval("CONTACTO"))%>'></telerik:RadCheckBox>
    </div>
</div>
<div class="w3-row-padding w3-margin">
    <div class="w3-half W3-center">
        <telerik:RadButton ID="btnUpdate" Text='<%# IIf((TypeOf (Container) Is GridEditFormInsertItem), "Agregar", "Actualizar") %>' runat="server" CommandName='<%# IIf((TypeOf (Container) Is GridEditFormInsertItem), "PerformInsert", "Update")%>'></telerik:RadButton>
    </div>
    <div class="w3-half W3-center">
        <telerik:RadButton ID="btnCancel" Text="Cancelar" runat="server" CausesValidation="False" CommandName="Cancel"></telerik:RadButton>
    </div>
</div>
