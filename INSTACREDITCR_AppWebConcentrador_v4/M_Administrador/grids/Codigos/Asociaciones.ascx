<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Asociaciones.ascx.vb" Inherits="M_Administrador_grids_Codigos_Asociaciones" %>
<style>
    .custom-switch .custom-control-label::after {
        background-color: #494849;
        border: #494849 solid 1px;
    }
</style>
<div class="text-center">
    <h2>Configurar Asociación</h2>
    <p class="text-muted">Agregar / Editar Asociación</p>
</div>
<div>
    <telerik:RadFormDecorator runat="server" DecoratedControls="Fieldset" DecorationZoneID="Decoration" />
    <telerik:RadAjaxLoadingPanel runat="server" ID="pnlLoading"></telerik:RadAjaxLoadingPanel>
    <telerik:RadLabel runat="server" ID="lblID" Visible="false" Text='<%# Eval("id") %>'></telerik:RadLabel>
    <div class="container" id="Decoration">
        <telerik:RadAjaxPanel runat="server" LoadingPanelID="pnlLoading">
            <asp:Panel runat="server" ID="pnlTipoAsociacion">
                <hr />
                <div class="text-center">
                    <telerik:RadButton runat="server" ID="btnAccionResultados" ButtonType="StandardButton" AutoPostBack="true" ToggleType="Radio" GroupName="Radio123" Text="Asociar Acción con Resultado" Checked="true">
                        <ToggleStates>
                            <telerik:RadButtonToggleState CssClass="bg-primary text-white border-0 rounded mx-2" />
                            <telerik:RadButtonToggleState CssClass="bg-transparent text-dark border rounded mx-2" />
                        </ToggleStates>
                    </telerik:RadButton>
                    <telerik:RadButton runat="server" ButtonType="StandardButton" ToggleType="Radio" GroupName="Radio123" Text="Asociar Resultado con Acciones">
                        <ToggleStates>
                            <telerik:RadButtonToggleState CssClass="bg-primary text-white border-0 rounded mx-2" />
                            <telerik:RadButtonToggleState CssClass="bg-transparent text-dark border rounded mx-2" />
                        </ToggleStates>
                    </telerik:RadButton>
                </div>
            </asp:Panel>
            <fieldset>
                <legend>Acción y Resultado</legend>
                <div class="row justify-content-center">
                    <div class="col-12 col-md-6 my-2">
                        <span>
                            <telerik:RadLabel runat="server" ID="lblTipoCodigo1" Text="Código de Acción"></telerik:RadLabel>
                        </span>
                        <br />
                        <telerik:RadAutoCompleteBox runat="server" ID="acbAccion" EmptyMessage="Buscar por descripción" InputType="Token" Width="100%" DropDownWidth="300px" HighlightFirstMatch="true" DropDownHeight="300px">
                        </telerik:RadAutoCompleteBox>
                        <asp:Panel runat="server" ID="pnlSelectedAccion" Visible="false" CssClass="p-2 border rounded">
                            <telerik:RadButton runat="server" ID="btnCancelSelectedAccion" CssClass="border-0 bg-transparent text-danger p-0 m-0" ToolTip="Eliminar" Width="32px" Height="16px">
                                <ContentTemplate>
                                    <span style="font-size: 1.2em" class="p-0 m-0">&times;</span>
                                </ContentTemplate>
                            </telerik:RadButton>
                            <telerik:RadLabel runat="server" ID="lblSelectedAccion"></telerik:RadLabel>
                        </asp:Panel>
                    </div>
                    <div class="col-12 col-md-6 my-2">
                        <span>
                            <telerik:RadLabel runat="server" ID="lblTipoCodigo2" Text="Código de Resulado"></telerik:RadLabel>
                        </span>
                        <telerik:RadComboBox runat="server" ID="comboResultados" Width="100%" EmptyMessage="Seleccione..." CheckBoxes="true" Enabled="false">
                            <Localization />
                        </telerik:RadComboBox>
                    </div>
                </div>
            </fieldset>
        </telerik:RadAjaxPanel>
        <fieldset>
            <legend>Parámetros</legend>
            <div class="row justify-content-center">
                <div class="col-12 col-md-4">
                    <div class="custom-control custom-switch">
                        <input runat="server" type="checkbox" class="custom-control-input" id="cbPromesa">
                        <label class="custom-control-label" for='<%# cbPromesa.ClientID %>'>
                            Promesa de pago
                             <i class="material-icons" id="helpIconPromesa" style="font-size: 1em">&#xe8fd;</i>
                        </label>
                        <telerik:RadToolTip runat="server" Position="MiddleRight" IsClientID="true" TargetControlID="helpIconPromesa" ShowEvent="OnMouseOver" AutoCloseDelay="6000" HideEvent="LeaveTargetAndToolTip" Width="150px" RelativeTo="Element">
                            Esta configuración de asosciación se tomará como promesa de pago en el sistema.
                        </telerik:RadToolTip>
                    </div>
                    <div class="custom-control custom-switch">
                        <input runat="server" type="checkbox" class="custom-control-input" id="cbSignificativo">
                        <label class="custom-control-label" for='<%# cbSignificativo.ClientID %>'>
                            Significativo
                             <i class="material-icons" id="helpIconSignificativo" style="font-size: 1em">&#xe8fd;</i>
                        </label>
                        <telerik:RadToolTip runat="server" Position="MiddleRight" IsClientID="true" TargetControlID="helpIconSignificativo" ShowEvent="OnMouseOver" AutoCloseDelay="6000" HideEvent="LeaveTargetAndToolTip" Width="150px" RelativeTo="Element">
                            Esta asociación contará como contacto con el cliente.
                        </telerik:RadToolTip>
                    </div>
                    <div class="custom-control custom-switch">
                        <input runat="server" type="checkbox" class="custom-control-input" id="cbTelefonico">
                        <label class="custom-control-label" for='<%# cbTelefonico.ClientID %>'>
                            Telefónico
                             <i class="material-icons" id="helpIconTelefonico" style="font-size: 1em">&#xe8fd;</i>
                        </label>
                        <telerik:RadToolTip runat="server" Position="MiddleRight" IsClientID="true" TargetControlID="helpIconTelefonico" ShowEvent="OnMouseOver" AutoCloseDelay="6000" HideEvent="LeaveTargetAndToolTip" Width="150px" RelativeTo="Element">
                            Habilita esta asociacion para call center.
                        </telerik:RadToolTip>
                    </div>
                    <div class="custom-control custom-switch">
                        <input runat="server" type="checkbox" class="custom-control-input" id="cbVisitador">
                        <label class="custom-control-label" for='<%# cbVisitador.ClientID %>'>
                            Visitador
                             <i class="material-icons" id="helpIconVisitador" style="font-size: 1em">&#xe8fd;</i>
                        </label>
                        <telerik:RadToolTip runat="server" Position="MiddleRight" IsClientID="true" TargetControlID="helpIconVisitador" ShowEvent="OnMouseOver" AutoCloseDelay="6000" HideEvent="LeaveTargetAndToolTip" Width="150px" RelativeTo="Element">
                            Habilita esta asociacion en la app móvil.
                        </telerik:RadToolTip>
                    </div>
                </div>
                <div class="col-12 col-md-8">
                    <div class="container">
                        <div class="row">
                            <div class="col-12 col-md-6 my-1">
                                <span>Semaforo Verde
                             <i class="material-icons" id="helpIconSemVerde" style="font-size: 1em">&#xe8fd;</i>

                                </span>
                                <telerik:RadToolTip runat="server" Position="MiddleRight" IsClientID="true" TargetControlID="helpIconSemVerde" ShowEvent="OnMouseOver" AutoCloseDelay="6000" HideEvent="LeaveTargetAndToolTip" Width="150px" RelativeTo="Element">
                                    Días que tardará el crédito/cuenta en cambiar de semaforo verde a semaforo amarillo (a partir de la gestion).
                                </telerik:RadToolTip>
                                <telerik:RadNumericTextBox runat="server" ID="txtSemaforoVerde" Width="100%" MinValue="1" MaxValue="99" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" EmptyMessage="Días"></telerik:RadNumericTextBox>
                            </div>
                            <div class="col-12 col-md-6 my-1">
                                <span>Semaforo Amarillo
                             <i class="material-icons" id="helpIconSemAma" style="font-size: 1em">&#xe8fd;</i>

                                </span>
                                <telerik:RadToolTip runat="server" Position="MiddleRight" IsClientID="true" TargetControlID="helpIconSemAma" ShowEvent="OnMouseOver" AutoCloseDelay="6000" HideEvent="LeaveTargetAndToolTip" Width="150px" RelativeTo="Element">
                                    Días que tardará el crédito/cuenta en cambiar de semaforo amarillo a semaforo rojo (a partir de la gestion).
                                </telerik:RadToolTip>
                                <telerik:RadNumericTextBox runat="server" ID="txtSemaforoAmarillo" Width="100%" MinValue="1" MaxValue="99" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" EmptyMessage="Días"></telerik:RadNumericTextBox>
                            </div>
                            <div class="col-12 col-md-6 my-1">
                                <span>Perfiles
                             <i class="material-icons" id="helpIconPerfiles" style="font-size: 1em">&#xe8fd;</i>

                                </span>
                                <telerik:RadToolTip runat="server" Position="MiddleRight" IsClientID="true" TargetControlID="helpIconPerfiles" ShowEvent="OnMouseOver" AutoCloseDelay="6000" HideEvent="LeaveTargetAndToolTip" Width="150px" RelativeTo="Element">
                                    Perfiles que verán esta asociación.
                                </telerik:RadToolTip>
                                <telerik:RadComboBox runat="server" ID="comboPerfiles" CheckBoxes="true" Width="100%" EmptyMessage="Seleccione..."></telerik:RadComboBox>
                            </div>
                            <div class="col-12 col-md-6 my-1">
                                <span>Productos
                             <i class="material-icons" id="helpIconProductos" style="font-size: 1em">&#xe8fd;</i>

                                </span>
                                <telerik:RadToolTip runat="server" Position="MiddleRight" IsClientID="true" TargetControlID="helpIconProductos" ShowEvent="OnMouseOver" AutoCloseDelay="6000" HideEvent="LeaveTargetAndToolTip" Width="150px" RelativeTo="Element">
                                    Productos que verán esta asociación.
                                </telerik:RadToolTip>
                                <telerik:RadComboBox runat="server" ID="comboProductos" CheckBoxes="true" Width="100%" EmptyMessage="Seleccione..."></telerik:RadComboBox>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </fieldset>
    </div>
    <div class="container">
        <div class="row justify-content-center">
            <div class="col-3 my-2">
                <telerik:RadButton runat="server" Text="Añadir" Visible='<%# (TypeOf DataItem Is Telerik.Web.UI.GridInsertionObject) %>' CssClass="bg-success text-white border-0" Width="100%" CommandName="PerformInsert">
                    <ConfirmSettings ConfirmText="Los parámetros seleccionados se usarán para las asociaciones creadas <br> ¿Continuar?" UseRadConfirm="true" />
                </telerik:RadButton>
                <asp:Button ID="btnUpdate" Text="Actualizar" runat="server" CommandName="Update" Visible='<%# Not (TypeOf DataItem Is Telerik.Web.UI.GridInsertionObject) %>' CssClass="btn btn-success rounded" Width="100%"></asp:Button>
            </div>
        </div>
    </div>
</div>
<telerik:RadWindowManager runat="server"></telerik:RadWindowManager>
