<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Negociaciones.ascx.vb" Inherits="Negociaciones" %>
<link href="Estilos/ObjHtmlS.css" rel="stylesheet" />
<link href="Estilos/ObjHtmlNoS.css" rel="stylesheet" />
<link href="Estilos/ObjAjax.css" rel="stylesheet" />
<link href="Estilos/Modal.css" rel="stylesheet" />
<link href="Estilos/HTML.css" rel="stylesheet" />
<link rel="stylesheet" href="https://www.w3schools.com/w3css/4/w3.css" />
<link href="Estilos/Telerik.css" rel="stylesheet" />

<style type="text/css">
    .auto-style1 {
        left: 0px;
        top: -22px;
    }
</style>

<table>
    <tr align="center">
        <td>
            <table class="Izquierda">
                 <tr>
                    <td>
                        <telerik:RadLabel ID="LblID" runat="server" CssClass="LblDesc" Text="ID:" Visible="true"></telerik:RadLabel>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="TxtID" runat="server" CssClass="TxtDesc" MaxLength="100" Visible="true" Width="300px" Enabled="false"></telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadLabel ID="LblNombre" runat="server" CssClass="LblDesc" Text="Nombre de Negociación:" Visible="true"></telerik:RadLabel>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="TxtNombre" runat="server" CssClass="TxtDesc" MaxLength="100" Visible="true" Width="300px" Text='<%# Bind("Negociacion") %>'></telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadLabel ID="LblCAT_NE_DESC_MAX1" runat="server" CssClass="LblDesc" Text="Descuento Máximo 1:" Visible="true"></telerik:RadLabel>
                    </td>
                    <td>
                        <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="TxtCAT_NE_DESC_MAX1" Value="0" MinValue="0" MaxValue="100" ShowSpinButtons="True" NumberFormat-DecimalDigits="0"  Width="112px" Culture="es-MX" DbValueFactor="1" LabelWidth="44px" Type="Percent">
                            <NegativeStyle Resize="None"></NegativeStyle>

                            <NumberFormat DecimalDigits="0" ZeroPattern="n %"></NumberFormat>

                            <EmptyMessageStyle Resize="None"></EmptyMessageStyle>

                            <ReadOnlyStyle Resize="None"></ReadOnlyStyle>

                            <FocusedStyle Resize="None"></FocusedStyle>

                            <DisabledStyle Resize="None"></DisabledStyle>

                            <InvalidStyle Resize="None"></InvalidStyle>

                            <HoveredStyle Resize="None"></HoveredStyle>

                            <EnabledStyle Resize="None"></EnabledStyle>
                        </telerik:RadNumericTextBox>
                        <telerik:RadDropDownList ID="DdlCampoNego1" runat="server" CssClass="DdlDesc" DataTextField="Campo_Nombre" DataValueField="Campo" Visible="true" >
                            <Items>
                                <telerik:DropDownListItem Value="PR_CAPITAL" Text="Capital" />
                            </Items>
                        </telerik:RadDropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadLabel ID="LblCAT_NE_DESC_MAX0" runat="server" CssClass="LblDesc" Text="Descuento Máximo 2:" Visible="true"></telerik:RadLabel>
                    </td>
                    <td>

                        <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="TxtCAT_NE_DESC_MAX2" Value="0" MinValue="0" MaxValue="100" ShowSpinButtons="True" NumberFormat-DecimalDigits="0"  Width="112px" Culture="es-MX" DbValueFactor="1" LabelWidth="44px" Type="Percent">
                            <NegativeStyle Resize="None"></NegativeStyle>

                            <NumberFormat DecimalDigits="0" ZeroPattern="n %"></NumberFormat>

                            <EmptyMessageStyle Resize="None"></EmptyMessageStyle>

                            <ReadOnlyStyle Resize="None"></ReadOnlyStyle>

                            <FocusedStyle Resize="None"></FocusedStyle>

                            <DisabledStyle Resize="None"></DisabledStyle>

                            <InvalidStyle Resize="None"></InvalidStyle>

                            <HoveredStyle Resize="None"></HoveredStyle>

                            <EnabledStyle Resize="None"></EnabledStyle>
                        </telerik:RadNumericTextBox>
                        <telerik:RadDropDownList ID="DdlCampoNego2" runat="server" CssClass="DdlDesc" Visible="true" DataTextField="Campo_Nombre" DataValueField="Campo"  >
                            <Items>
                                <telerik:DropDownListItem Value="PR_INTERES" Text="Intereses" />
                            </Items>
                        </telerik:RadDropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadLabel ID="LblCAT_NE_DESC_MAX" runat="server" CssClass="LblDesc" Text="Descuento Máximo 3:" Visible="true"></telerik:RadLabel>
                    </td>
                    <td>
                        <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="TxtCAT_NE_DESC_MAX3" Value="0" MinValue="0" MaxValue="100" ShowSpinButtons="True" NumberFormat-DecimalDigits="0"  Width="112px" Culture="es-MX" DbValueFactor="1" LabelWidth="44px" Type="Percent">
                            <NegativeStyle Resize="None"></NegativeStyle>

                            <NumberFormat DecimalDigits="0" ZeroPattern="n %"></NumberFormat>

                            <EmptyMessageStyle Resize="None"></EmptyMessageStyle>

                            <ReadOnlyStyle Resize="None"></ReadOnlyStyle>

                            <FocusedStyle Resize="None"></FocusedStyle>

                            <DisabledStyle Resize="None"></DisabledStyle>

                            <InvalidStyle Resize="None"></InvalidStyle>

                            <HoveredStyle Resize="None"></HoveredStyle>

                            <EnabledStyle Resize="None"></EnabledStyle>
                        </telerik:RadNumericTextBox>
                        <telerik:RadDropDownList ID="DdlCampoNego3" runat="server" CssClass="DdlDesc" Visible="true" DataTextField="Campo_Nombre" DataValueField="Campo"  >
                            <Items>
                                <telerik:DropDownListItem Value="PR_MORATORIO" Text="Moratorios" />
                            </Items>
                        </telerik:RadDropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadLabel ID="LblCAT_NE_NUM_PAGOS" runat="server" CssClass="LblDesc" Text="Número De Pagos:" Visible="true"></telerik:RadLabel>
                    </td>
                    <td>
                        <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="TxtCAT_NE_NUM_PAGOS" Value="1" MinValue="1" MaxValue="12" ShowSpinButtons="true" NumberFormat-DecimalDigits="0"  Width="112px"></telerik:RadNumericTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadLabel ID="Lbl_nivel" runat="server" CssClass="LblDesc" Text="Nivel:" Visible="true"></telerik:RadLabel>
                    </td>
                    <td>
                        <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="NTxtNivel" MinValue="1" Value="1" MaxValue="3" ShowSpinButtons="true" NumberFormat-DecimalDigits="0"  Width="112px"></telerik:RadNumericTextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        
                     <asp:Panel runat="server" ID="PnlDatos"  CssClass="w3-container">
            <div class="w3-panel Titulos">
                <b>Parametrización Nego</b>
            </div>
            <div class="w3-row w3-text-black" style="overflow: auto; max-height: 400px; max-width: 100%">
                <telerik:RadGrid runat="server" ID="gridNego">
                    <MasterTableView AutoGenerateColumns="false" CommandItemDisplay="top">
                        <CommandItemSettings AddNewRecordText="Agregar Parametro" CancelChangesText="Cancelar" SaveChangesText="Guardar" RefreshText="Actualizar" />
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="Eliminar">
                                <ItemTemplate>
                                    <telerik:RadButton runat="server" Text="Eliminar" CommandName="onDelete"></telerik:RadButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="No" HeaderText="#" DataField="ORDEN">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="Tabla" HeaderText="Tabla" DataField="DESCRIPCIONTABLA">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="Campo" HeaderText="Campo" DataField="DESCRIPCIONCAMPO">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="Condicion" HeaderText="Condicion" DataField="DESCRIPCIONOPERADOR">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="Valor" HeaderText="Valor" DataField="Valor">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="Conector" HeaderText="Conector" DataField="DESCRIPCIONCONECTOR">
                            </telerik:GridBoundColumn>
                            <telerik:GridEditCommandColumn ButtonType="FontIconButton" HeaderText="Editar"></telerik:GridEditCommandColumn>
                        </Columns>
                        <EditFormSettings UserControlName="grids/configReglas/Reglas.ascx" EditFormType="WebUserControl">
                            <EditColumn UniqueName="EditCommandColumn1">
                            </EditColumn>
                        </EditFormSettings>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
        </asp:Panel>
                    </td>
                    </tr>
                <tr>
                    <td>
                        <telerik:RadButton ID="Btncancelar" runat="server" Text="Cancelar"  CommandName="Cancel" CausesValidation="false"></telerik:RadButton>
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadButton ID="BtnNuevo" runat="server"  Text="Agregar" CommandName="PerformInsert"
                            Visible='<%# (TypeOf DataItem Is Telerik.Web.UI.GridInsertionObject) %>' >
                        </telerik:RadButton>
                        <telerik:RadButton ID="BtnAccion" runat="server" Text="Actualizar" CommandName="Update" Visible='<%# Not (TypeOf DataItem Is Telerik.Web.UI.GridInsertionObject) %>'></telerik:RadButton>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                       <telerik:RadLabel ID="RLBError" runat="server" CssClass="LblError"  Visible="true"></telerik:RadLabel>
                    </td>

                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>

        </td>
    </tr>
    <telerik:RadWindowManager ID="RadAviso" runat="server" >
            <Localization OK="Aceptar" />
        </telerik:RadWindowManager>
</table>
