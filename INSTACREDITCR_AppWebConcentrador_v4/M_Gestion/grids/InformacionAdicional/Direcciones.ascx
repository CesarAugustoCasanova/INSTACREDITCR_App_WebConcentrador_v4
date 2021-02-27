<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Direcciones.ascx.vb" Inherits="M_Gestion_grids_InformacionAdicional_Direcciones" %>

<telerik:RadAjaxLoadingPanel runat="server" ID="ldng"></telerik:RadAjaxLoadingPanel>
<telerik:RadAjaxPanel runat="server" CssClass="w3-row-padding" LoadingPanelID="ldng">
    <telerik:RadLabel runat="server" ID="txtID" Text='<%#Eval("DIRECCIONID")%>' Visible="false"></telerik:RadLabel>
    <div class="w3-quarter">
        <div class="w3-block">Código postal:</div>
        <telerik:RadTextBox runat="server" CssClass="w3-input" ID="TBCP" InputType="Number" AutoCompleteType="disabled" Text='<%#Eval("CP")%>' ReadOnly='<%# IIf((TypeOf (Container) Is GridEditFormInsertItem), False, True) %>' Width="49%"></telerik:RadTextBox>
        <telerik:RadButton runat="server" ID="btnValidaCP" Text="Validar CP" Visible='<%# IIf((TypeOf (Container) Is GridEditFormInsertItem), True, False) %>' AutoPostBack='<%# IIf((TypeOf (Container) Is GridEditFormInsertItem), True, False) %>' Width="49%" CausesValidation="false"></telerik:RadButton>
    </div>
    <div class="w3-quarter">
        <label>Ciudad:</label>
        <telerik:RadTextBox runat="server" CssClass="w3-input w3-gray" ID="TBCiudad" ReadOnly="true" Enabled="false" Text='<%#Eval("CIUDAD")%>' Width="100%"></telerik:RadTextBox>
    </div>
    <div class="w3-quarter">
        <label>Estado:</label>
        <telerik:RadTextBox runat="server" CssClass="w3-input w3-gray" ID="TBEstado" ReadOnly="true" Enabled="false" Text='<%#Eval("ESTADO")%>' Width="100%"></telerik:RadTextBox>
    </div>
 <%--   <div class="w3-quarter">
        <label>Municipio:</label>
        <telerik:RadTextBox runat="server" CssClass="w3-input w3-gray" ID="TBMunicipio" ReadOnly="true" Enabled="false" Text='<%#Eval("MUNICIPIO")%>' Width="100%"></telerik:RadTextBox>
    </div>--%>
    <div class="w3-quarter">
        <label>Colonia:</label>
        <telerik:RadTextBox runat="server" ID="TBCol" Text='<%#Eval("COLONIA") %>' Visible='<%# IIf((TypeOf (Container) Is GridEditFormInsertItem), False, True) %>' ReadOnly="true" Width="100%" MaxLength="5"></telerik:RadTextBox>
        <telerik:RadDropDownList runat="server" ID="DDCol" Visible='<%# IIf((TypeOf (Container) Is GridEditFormInsertItem), True, False) %>' Width="100%">
        </telerik:RadDropDownList>
    </div>
    <div class="w3-quarter">
        <label>
            Calle:
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TBCalle" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </label>
        <telerik:RadTextBox runat="server" CssClass="w3-input" ID="TBCalle" AutoCompleteType="disabled" Text='<%#Eval("CALLE")%>' ReadOnly='<%# IIf((TypeOf (Container) Is GridEditFormInsertItem), False, True) %>' Width="100%"></telerik:RadTextBox>
    </div>
    <%--<div class="w3-quarter">
        <label>Núm. interior:</label>
        <telerik:RadTextBox runat="server" CssClass="w3-input" ID="TBNumInt" AutoCompleteType="disabled" Text='<%#Eval("NUMINT")%>' ReadOnly='<%# IIf((TypeOf (Container) Is GridEditFormInsertItem), False, True) %>' Width="100%"></telerik:RadTextBox>
    </div>
    <div class="w3-quarter">
        <label>
            Núm. exterior:
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TBNumExt" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </label>
        <telerik:RadTextBox runat="server" CssClass="w3-input" ID="TBNumExt" AutoCompleteType="disabled" Text='<%#Eval("NUMEXT")%>' ReadOnly='<%# IIf((TypeOf (Container) Is GridEditFormInsertItem), False, True) %>' Width="100%"></telerik:RadTextBox>
    </div>--%>
    <%--<div class="w3-col s12 m2">
        <label>Manzana:</label>
        <telerik:RadTextBox runat="server" CssClass="w3-input" ID="TBManzana" AutoCompleteType="disabled" Text='<%#Eval("MZ")%>' Width="100%"></telerik:RadTextBox>
    </div>
    <div class="w3-col s12 m2">
        <label>Lote:</label>
        <telerik:RadTextBox runat="server" CssClass="w3-input" ID="TBLote" AutoCompleteType="disabled" Text='<%#Eval("LT")%>' Width="100%"></telerik:RadTextBox>
    </div>
    <div class="w3-col s12 m2">
        <label>Piso:</label>
        <telerik:RadTextBox runat="server" CssClass="w3-input" ID="TBPiso" AutoCompleteType="disabled" Text='<%#Eval("Piso")%>' Width="100%"></telerik:RadTextBox>
    </div>--%>
    <div class="w3-quarter">
        <label>Entre calles</label>
        <telerik:RadTextBox runat="server" ID="TBCalle1" Text='<%#Eval("ENTRECALLE1")%>' MaxLength="52" Width="100%"></telerik:RadTextBox>
    </div>
    <%--<div class="w3-quarter">
        <label>Y</label>
        <telerik:RadTextBox runat="server" ID="TBCalle2" Text='<%#Eval("ENTRECALLE2")%>' MaxLength="52" Width="100%"></telerik:RadTextBox>
    </div>--%>
    <%--<div class="w3-half">
        <label>Descripcion del domicilio</label>
        <telerik:RadTextBox runat="server" CssClass="w3-input" ID="TBDescripcion" AutoCompleteType="disabled" Width="100%" TextMode="MultiLine" Resize="Vertical" Text='<%#Eval("descripcion")%>'></telerik:RadTextBox>
    </div>--%>
    <div class="w3-quarter">
        <telerik:RadCheckBox runat="server" AutoPostBack="false" Width="100%" CssClass="w3-chcekbox" Text="¿Domicilio vigente?" ID="CBVigente" Checked='<%#quitaNull(Eval("Vigente"))%>'></telerik:RadCheckBox>
    </div>
    <div class="w3-quarter">
        <telerik:RadCheckBox runat="server" AutoPostBack="false" Width="100%" CssClass="w3-chcekbox" Text="¿Contactar?" ID="RadCheckBox1" Checked='<%#quitaNull(Eval("CONTACTO"))%>'></telerik:RadCheckBox>
    </div>
    <br />
    <div class="w3-half">
        <div class="w3-center w3-block">Días:</div>
        <telerik:RadCheckBox runat="server" AutoPostBack="false" CssClass="w3-chcekbox" Text="Lunes" ID="RadCheckBox2" Checked='<%#quitaNull(Eval("LUN"))%>'></telerik:RadCheckBox>
        <telerik:RadCheckBox runat="server" AutoPostBack="false" CssClass="w3-chcekbox" Text="Martes" ID="RadCheckBox3" Checked='<%#quitaNull(Eval("MAR"))%>'></telerik:RadCheckBox>
        <telerik:RadCheckBox runat="server" AutoPostBack="false" CssClass="w3-chcekbox" Text="Miercoles" ID="RadCheckBox4" Checked='<%#quitaNull(Eval("MIE"))%>'></telerik:RadCheckBox>
        <telerik:RadCheckBox runat="server" AutoPostBack="false" CssClass="w3-chcekbox" Text="Jueves" ID="RadCheckBox5" Checked='<%#quitaNull(Eval("JUE"))%>'></telerik:RadCheckBox>
        <telerik:RadCheckBox runat="server" AutoPostBack="false" CssClass="w3-chcekbox" Text="Viernes" ID="RadCheckBox6" Checked='<%#quitaNull(Eval("VIE"))%>'></telerik:RadCheckBox>
        <telerik:RadCheckBox runat="server" AutoPostBack="false" CssClass="w3-chcekbox" Text="Sábado" ID="RadCheckBox7" Checked='<%#quitaNull(Eval("SAB"))%>'></telerik:RadCheckBox>
        <telerik:RadCheckBox runat="server" AutoPostBack="false" CssClass="w3-chcekbox" Text="Domingo" ID="RadCheckBox8" Checked='<%#quitaNull(Eval("DOM"))%>'></telerik:RadCheckBox>
    </div>
    <div class="w3-quarter">
        <label>Horario de:</label>
        <telerik:RadTimePicker ID="TB14" runat="server" EnableTyping="false" DbSelectedDate='<%# Eval("HORARIO1") %>' Width="100%" AutoPostBack="true"></telerik:RadTimePicker>
    </div>
    <div class="w3-quarter">
        <label>a:</label>
        <telerik:RadTimePicker ID="TB15" runat="server" EnableTyping="false" DbSelectedDate='<%# Eval("HORARIO2") %>' Width="100%"></telerik:RadTimePicker>
    </div>
   <%-- <div class="w3-quarter">
        <label>Tipo domicilio:</label>
        <telerik:RadComboBox runat="server" ID="CBTipoDomicilio" SelectedValue='<%# revisa2(Eval("TIPODOMICILIO")) %>' Width="100%" EmptyMessage="Seleccione">
            <Items>
                <telerik:RadComboBoxItem Text="Casa" Value="1" />
                <telerik:RadComboBoxItem Text="Negocio" Value="2" />
                <telerik:RadComboBoxItem Text="Trabajo" Value="3" />
                <telerik:RadComboBoxItem Text="Ubicada por gestión" Value="4" />
                <telerik:RadComboBoxItem Text="Referencia" Value="5" />
                <telerik:RadComboBoxItem Text="Alterno" Value="0" />
            </Items>
        </telerik:RadComboBox>
    </div>--%>
    <%--<div class="w3-quarter">
        <label>Tipo vivienda</label>
        <telerik:RadComboBox runat="server" ID="CBTipoVivienda" SelectedValue='<%# revisa2(Eval("TIPOVIVIENDA")) %>' Width="100%" EmptyMessage="Seleccione">
            <Items>
                <telerik:RadComboBoxItem Text="Departamento" Value="D" />
                <telerik:RadComboBoxItem Text="Casa" Value="C" />
                <telerik:RadComboBoxItem Text="Unidad habitacional" Value="U" />
                <telerik:RadComboBoxItem Text="Rural" Value="R" />
                <telerik:RadComboBoxItem Text="Multifamiliar" Value="M" />
            </Items>
        </telerik:RadComboBox>
    </div>--%>
  <%--  <div class="w3-quarter">
        <label>Vive en casa:</label>
        <telerik:RadComboBox runat="server" ID="CBViveEnCasa" SelectedValue='<%# revisa2(Eval("VIVEENCASA")) %>' Width="100%" EmptyMessage="Seleccione">
            <Items>
                <telerik:RadComboBoxItem Text="Familiar" Value="FM" />
                <telerik:RadComboBoxItem Text="Prestada" Value="PR" />
                <telerik:RadComboBoxItem Text="Rentada" Value="RN" />
                <telerik:RadComboBoxItem Text="Propia" Value="PP" />
                <telerik:RadComboBoxItem Text="Traspaso" Value="TS" />
                <telerik:RadComboBoxItem Text="Propia sin hipoteca" Value="PS" />
                <telerik:RadComboBoxItem Text="Propia pagandose" Value="PG" />
                <telerik:RadComboBoxItem Text="Cónyuge" Value="CN" />
            </Items>
        </telerik:RadComboBox>
    </div>--%>
    <%--<div class="w3-quarter">
        <telerik:RadCheckBox runat="server" AutoPostBack="false" Width="100%" CssClass="w3-chcekbox" Text="¿Domicilio fiscal?" ID="CBFiscal" Checked='<%#quitaNull(Eval("FISCAL"))%>'></telerik:RadCheckBox>
    </div>--%>
    <div class="w3-quarter">
        <label>
            Parentesco:
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DDParentesco" ErrorMessage="* Seleccione Parentesco" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator></label>
        <telerik:RadComboBox runat="server" CssClass="w3-select" ID="DDParentesco" SelectedValue='<%# revisa2(Eval("PARENTESCO")) %>' EmptyMessage="Seleccione" DataSource='<%# LlenarDrops(2) %>'>
        </telerik:RadComboBox>
    </div>
    <div class="w3-quarter">
        <label>Nombre:</label>
        <asp:TextBox runat="server" CssClass="w3-input" Width="100%" ID="TBNombre" AutoCompleteType="disabled" Text='<%# Eval("NOMBRE") %>'></asp:TextBox>
    </div>
    <div class="w3-quarter">
        <label>Nombre quien proporciona:</label>
        <asp:TextBox runat="server" CssClass="w3-input" Width="100%" AutoCompleteType="disabled" Text='<%# Eval("PROPORCIONA") %>' ID="TBProporciona"></asp:TextBox>
    </div>
    <telerik:RadInputManager runat="server">
        <telerik:RegExpTextBoxSetting BehaviorID="RagExpnombre" Validation-IsRequired="true"
            ValidationExpression="[A-Za-z áéíóúü]+" ErrorMessage="Nombre inválido">
            <TargetControls>
                <telerik:TargetInput ControlID="TBNombre"></telerik:TargetInput>
                <telerik:TargetInput ControlID="TBProporciona"></telerik:TargetInput>
            </TargetControls>
        </telerik:RegExpTextBoxSetting>
    </telerik:RadInputManager>
</telerik:RadAjaxPanel>
<div class="w3-row-padding w3-margin">
    <div class="w3-half W3-center">
        <telerik:RadButton ID="btnUpdate" Text='<%# IIf((TypeOf (Container) Is GridEditFormInsertItem), "Agregar", "Actualizar") %>' runat="server" CommandName='<%# IIf((TypeOf (Container) Is GridEditFormInsertItem), "PerformInsert", "Update")%>'></telerik:RadButton>
    </div>
    <div class="w3-half W3-center">
        <telerik:RadButton ID="btnCancel" Text="Cancelar" runat="server" CausesValidation="False" CommandName="Cancel"></telerik:RadButton>
    </div>
</div>
