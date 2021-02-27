<%@ Control Language="VB" AutoEventWireup="false" CodeFile="CatalogoAvisosEtiquetas.ascx.vb" Inherits="M_Administrador_CatalogoAvisosEtiquetas" %>

<div class="w3-container w3-margin-bottom">
    <label>Tabla:</label>
    <telerik:RadComboBox ID="comboTablas" runat="server" AutoPostBack="True" EmptyMessage="Seleccione" Width="100%">
       <%-- <Items>
            <telerik:RadComboBoxItem Value="VI_JUDICIAL" Text="Datos Judicial" />
            <telerik:RadComboBoxItem Value="CAT_PARTICIPANTE_CREDITO" Text="Datos Participantes" />
            <telerik:RadComboBoxItem Value="VI_DEMO" Text="Datos Demograficos" />
            <telerik:RadComboBoxItem Value="VI_FINAN" Text="Datos Financieros" />
            <telerik:RadComboBoxItem Value="VI_AGENCY" Text="Otros Datos" />
            <telerik:RadComboBoxItem Value="PR_MC_GRAL" Text="Datos Sistema" />
        </Items>--%>
    </telerik:RadComboBox>
</div>
<div class="w3-container w3-margin-bottom">
    <label>Campo:</label>
    <telerik:RadComboBox ID="comboCampos" runat="server" EmptyMessage="Seleccione" Width="100%">
    </telerik:RadComboBox>
</div>
<div class="w3-container w3-margin-bottom">
    <label>Descripcion:</label>
    <telerik:radTextBox ID="txtEtiqueta" runat="server" CssClass="fuenteTxt" MaxLength="21" Width="100%" EmptyMessage="Descripcion"></telerik:radTextBox>
</div>


<telerik:RadTextBox runat="server" ID="txtID" Visible="false"></telerik:RadTextBox>

<telerik:RadButton runat="server" ID="btnGuardar" Text="Guardar" SingleClick="true" SingleClickText="Guardando..." CommandName='<%# IIf((TypeOf (Container) Is GridEditFormInsertItem), "PerformInsert", "Update")%>'></telerik:RadButton>
<telerik:RadButton runat="server" ID="btnCancelar" Text="Cancelar" CommandName="Cancel" CausesValidation="false"></telerik:RadButton>
