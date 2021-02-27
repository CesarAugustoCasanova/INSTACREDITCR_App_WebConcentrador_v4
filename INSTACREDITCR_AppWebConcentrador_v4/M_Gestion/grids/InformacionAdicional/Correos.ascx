<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Correos.ascx.vb" Inherits="M_Gestion_grids_InformacionAdicional_Correos" %>
<div class="w3-row-padding">
    <div class="w3-third">
        <hr />
    </div>
    <div class="w3-third">
        <label>Correo:</label>
        <telerik:RadTextBox runat="server" CssClass="w3-input" Width="100%" ID="TB3" AutoCompleteType="disabled" Text='<%# Eval("CORREO") %>' Enabled='<%# IIf((TypeOf (Container) Is GridEditFormInsertItem), True, False) %>'></telerik:RadTextBox>
    </div>
    <div class="w3-third">
        <hr />
    </div>
</div>
<div class="w3-row-padding w3-margin">
    <div class="w3-half">
        <label>Tipo:</label>
        <asp:DropDownList runat="server" CssClass="w3-select" ID="DDTipo" SelectedValue='<%# Eval("TIPO") %>' AppendDataBoundItems="True">
            <asp:ListItem Selected="True" Text="Seleccione" Value="">
            </asp:ListItem>
            <asp:ListItem Text="Personal" Value="Personal">
            </asp:ListItem>
            <asp:ListItem Text="Trabajo" Value="Trabajo">
            </asp:ListItem>
            <asp:ListItem Text="Adicional" Value="Adicional">
            </asp:ListItem>
        </asp:DropDownList>
    </div>
    <div class="w3-half">
        <label>Parentesco:</label>
        <asp:DropDownList runat="server" CssClass="w3-select" ID="DDParentesco" AutoCompleteType="disabled" SelectedValue='<%# revisa2(Eval("PARENTESCO")) %>' DataSource='<%# LlenarDrops(2) %>' AppendDataBoundItems="True">
            <asp:ListItem Selected="True" Text="Seleccione" Value="">
            </asp:ListItem>
        </asp:DropDownList>
    </div>
</div>
<div class="w3-row-padding w3-margin">
    <div class="w3-half">
        <label>Nombre:</label>
        <asp:TextBox runat="server" CssClass="w3-input" Width="100%" ID="TB12" AutoCompleteType="disabled" Text='<%# Eval("NOMBRE") %>'></asp:TextBox>
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
        <telerik:RadCheckBox runat="server" AutoPostBack="false" CssClass="w3-chcekbox" Text="Contactar" ID="RadCheckBox1" Checked='<%#quitaNull(Eval("CONTACTO"))%>'></telerik:RadCheckBox>
    </div>
</div>
<div class="w3-row-padding w3-margin">
    <div class="w3-half W3-center">
        <telerik:RadButton ID="btnUpdate" CssClass="w3-btn w3-green w3-round-large" Text='<%# IIf((TypeOf (Container) Is GridEditFormInsertItem), "Agregar", "Actualizar") %>' runat="server" CommandName='<%# IIf((TypeOf (Container) Is GridEditFormInsertItem), "PerformInsert", "Update")%>'></telerik:RadButton>
    </div>
    <div class="w3-half W3-center">
        <telerik:RadButton ID="btnCancel" CssClass="w3-btn w3-red w3-round-large" Text="Cancelar" runat="server" CausesValidation="False" CommandName="Cancel"></telerik:RadButton>
    </div>
</div>
