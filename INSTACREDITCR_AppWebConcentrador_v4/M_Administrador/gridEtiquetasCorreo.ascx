<%@ Control Language="VB" AutoEventWireup="false" CodeFile="gridEtiquetasCorreo.ascx.vb" Inherits="M_Administrador_gridEtiquetasCorreo" %>
    <div class="w3-container w3-padding-bottom">
        <telerik:RadComboBox runat="server" ID="comboTablas" Label="Tabla:" EmptyMessage="Seleccione" Width="100%" AutoPostBack="true" CausesValidation="false">
            <Items>
                    <%--<telerik:RadComboBoxItem Value="CAT_PARTICIPANTE_CREDITO" text="Datos Participantes" />--%>
                    <telerik:RadComboBoxItem Value="PR_CREDIFIEL" text="Datos Generales" />
                    <telerik:RadComboBoxItem Value="PR_MC_GRAL" text="Datos Sistema" />
               <%-- <telerik:RadComboBoxItem Value="VI_JUDICIAL" Text="Datos Judicial" />
                <telerik:RadComboBoxItem Value="VI_DEMO" Text="Datos Demograficos" />
                <telerik:RadComboBoxItem Value="VI_FINAN" Text="Datos Financieros" />
                <telerik:RadComboBoxItem Value="VI_AGENCY" Text="Otros Datos" />
                <telerik:RadComboBoxItem Value="PR_MC_GRAL" Text="Datos Sistema" />
                <telerik:RadComboBoxItem Value="VI_PARTICIPANTES" Text="Datos Participantes" />--%>
            </Items>
        </telerik:RadComboBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="comboTablas" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
    </div>
    <div class="w3-container w3-padding-bottom">
        <telerik:RadComboBox runat="server" ID="comboCampos" Label="Campos:" EmptyMessage="Seleccione" Enabled="false" Width="100%"></telerik:RadComboBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="comboCampos" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
    </div>

    <div class="w3-container w3-padding-bottom">
        <telerik:RadTextBox runat="server" ID="txtEtiqueta" Label="Etiqueta:" Width="100%"></telerik:RadTextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEtiqueta" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
    </div>


    <telerik:RadTextBox runat="server" ID="txtID" Visible="false"></telerik:RadTextBox>

    <telerik:RadButton runat="server" ID="btnGuardar" Text="Guardar" SingleClick="true" SingleClickText="Guardando..." CommandName='<%# IIf((TypeOf(Container) is GridEditFormInsertItem), "PerformInsert", "Update")%>'></telerik:RadButton>
    <telerik:RadButton runat="server" ID="btnCancelar" Text="Cancelar" CommandName="Cancel" CausesValidation="false"></telerik:RadButton>