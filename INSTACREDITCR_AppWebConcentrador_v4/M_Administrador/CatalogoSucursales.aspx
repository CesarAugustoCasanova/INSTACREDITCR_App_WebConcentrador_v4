<%@ Page Title="" Language="VB" MasterPageFile="./MasterPage.master" AutoEventWireup="false" CodeFile="CatalogoSucursales.aspx.vb" Inherits="_CatalogoSucursales" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPHMaster" runat="Server">
    <telerik:RadAjaxManager runat="server" ID="ajaxManager">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="gridSucursales">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlSucursales" />
                    <telerik:AjaxUpdatedControl ControlID="pnlSucursal" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnCancelar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlSucursales" />
                    <telerik:AjaxUpdatedControl ControlID="pnlSucursal" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAgregarSucursal">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlSucursales" />
                    <telerik:AjaxUpdatedControl ControlID="pnlSucursal" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnGuardar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlSucursales" />
                    <telerik:AjaxUpdatedControl ControlID="pnlSucursal" />
                    <telerik:AjaxUpdatedControl ControlID="RadNotification1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel runat="server" ID="pnlLoading"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel runat="server" ID="pnlSucursales" LoadingPanelID="pnlLoading">
        <telerik:RadButton runat="server" ID="btnAgregarSucursal" Text="Agregar Sucursal" ></telerik:RadButton>
        <telerik:RadGrid runat="server" ID="gridSucursales" AutoGenerateColumns="false" >
            <MasterTableView>
                <Columns>
                    <telerik:GridButtonColumn ButtonType="PushButton" Text="Editar" CommandName="onEdit" HeaderStyle-Width="100px"></telerik:GridButtonColumn>
                    <telerik:GridButtonColumn ButtonType="PushButton" Text="Eliminar" CommandName="onDelete" HeaderStyle-Width="100px"></telerik:GridButtonColumn>
                    <telerik:GridBoundColumn HeaderText="ID" HeaderStyle-Width="150px" DataField="CAT_SUC_ID"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Sucursal" DataField="CAT_SUC_NOMBRE"></telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxPanel runat="server" ID="pnlSucursal" LoadingPanelID="pnlLoading" Visible="false">
        <div class="w3-row-padding">
            <div class="w3-col s12 m4">
                <div class="w3-container w3-blue w3-text-white w3-center">
                    <b>Clave sucursal</b>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="tbClave" ErrorMessage="*" ForeColor="Yellow" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                <telerik:RadTextBox runat="server" ID="tbClave" Width="100%" EmptyMessage="Clave sucursal" ></telerik:RadTextBox>
            </div>
            <div class="w3-col s12 m4">
                <div class="w3-container w3-blue w3-text-white w3-center">
                    <b>Nombre</b>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbNombre" ErrorMessage="*" ForeColor="Yellow" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                <telerik:RadTextBox runat="server" ID="tbNombre" Width="100%" EmptyMessage="Nombre" ></telerik:RadTextBox>

            </div>
            <div class="w3-col s12 m4">
                <div class="w3-container w3-blue w3-text-white w3-center">
                    <b>Direccion</b>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbDireccion" ErrorMessage="*" ForeColor="Yellow" SetFocusOnError="true" ></asp:RequiredFieldValidator>
                </div>
                <telerik:RadTextBox runat="server" ID="tbDireccion" Width="100%" MaxLength="150" EmptyMessage="Direccion de sucursal" ></telerik:RadTextBox>
            </div>
        </div>
        <br />
        <div class="w3-row-padding">
            <div class="w3-col s12 m4">
                <div class="w3-container w3-blue w3-text-white w3-center">
                    <b>Código Postal</b>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="acbCP" ErrorMessage="*" ForeColor="Yellow" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                <telerik:RadAutoCompleteBox runat="server" ID="acbCP" InputType="text" TokensSettings-AllowTokenEditing="false" AllowCustomEntry="false" Width="100%" DropDownHeight="300px" Localization-ShowAllResults="Ver más resultados" MaxResultCount="10" TextSettings-SelectionMode="Single" EmptyMessage="Código Postal">
                    <WebServiceSettings Method="webServiceCP" Path="CatalogoSucursales.aspx" />
                </telerik:RadAutoCompleteBox>                
            </div>
            <div class="w3-col s12 m4">
                <div class="w3-container w3-blue w3-text-white w3-center">
                    <b>Estado</b>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tbEstado" ErrorMessage="*" ForeColor="Yellow"></asp:RequiredFieldValidator>
                </div>
                <telerik:RadTextBox runat="server" ID="tbEstado" Width="100%" Enabled="false" EmptyMessage="Estado" ></telerik:RadTextBox>
            </div>
            <div class="w3-col s12 m4">
                <div class="w3-container w3-blue w3-text-white w3-center">
                    <b>Municipio</b>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="tbMunicipio" ErrorMessage="*" ForeColor="Yellow"></asp:RequiredFieldValidator>
                </div>
                <telerik:RadTextBox runat="server" ID="tbMunicipio" Width="100%" Enabled="false" EmptyMessage="Municipio" ></telerik:RadTextBox>
            </div>
        </div>
        <br />
        <div class="w3-row-padding">
            <div class="w3-col s12 m4">
                <div class="w3-container w3-blue w3-text-white w3-center">
                    <b>Localidad</b>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="tbLocalidad" ErrorMessage="*" ForeColor="Yellow"></asp:RequiredFieldValidator>
                </div>
                <telerik:RadTextBox runat="server" ID="tbLocalidad" Width="100%"  Enabled="false" EmptyMessage="Localidad" ></telerik:RadTextBox>
            </div>
            <div class="w3-col s12 m4">
                <div class="w3-container w3-blue w3-text-white w3-center">
                    <b>Telefonos</b>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="acbTelefonos" ErrorMessage="*" ForeColor="Yellow" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                <telerik:RadAutoCompleteBox runat="server" ID="acbTelefonos" InputType="Token" TokensSettings-AllowTokenEditing="true" AllowCustomEntry="true" Width="100%" EmptyMessage="Telefonos" Delimiter="|">
                    <WebServiceSettings Method="webServiceDummy" Path="CatalogoSucursales.aspx" />
                </telerik:RadAutoCompleteBox>
            </div>
            <div class="w3-col s12 m4">
            </div>
        </div>
        <hr />
        <telerik:RadButton runat="server" ID="btnGuardar" Text="Guardar" ></telerik:RadButton>
        <telerik:RadButton runat="server" ID="btnCancelar" Text="Cancelar" ></telerik:RadButton>
    </telerik:RadAjaxPanel>
    <telerik:RadNotification RenderMode="Lightweight" ID="RadNotification1" runat="server" Height="140px" Animation="Fade" EnableRoundedCorners="true" EnableShadow="true" AutoCloseDelay="3500" Position="Center" OffsetX="-30" OffsetY="-70" ShowCloseButton="true" VisibleOnPageLoad="false" LoadContentOn="EveryShow" KeepOnMouseOver="false">
    </telerik:RadNotification>
</asp:Content>
