<%@ Page Title="" Language="VB" MasterPageFile="MasterPage.master" AutoEventWireup="false" CodeFile="Datos.aspx.vb" Inherits="Datos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHMaster" runat="Server">
      <telerik:RadScriptBlock runat="server">
        <script type="text/javascript">
            gotoEndPage = () => setTimeout(() => {
                document.getElementById('endPage').scrollIntoView(true);
            }, 0)
            </script>
    </telerik:RadScriptBlock>
    <telerik:RadAjaxLoadingPanel runat="server" ID="Log1"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel runat="server" CssClass="container" LoadingPanelID="Log1">
        <telerik:RadFormDecorator ID="FormDecorator1" runat="server" DecoratedControls="all" DecorationZoneID="optionsDecoration"></telerik:RadFormDecorator>

        <a href="./Configuracion.aspx" class="btn" style="font-size: 1.2em"><i class="material-icons" style="font-size: 1em">&#xe5c4;</i>Regresar</a>

        <div class="container" id="optionsDecoration">
            <h3>Configuraci&oacute;n de reporte</h3>
            <fieldset class="my-1">
                <legend>Informaci&oacute;n</legend>
                <div class="row">
                    <div class="col-sm-6 col-md-3 mt-2">
                        <asp:Label ID="LblCAT_RED_NOMBRE" runat="server" Text="Nombre del reporte:"></asp:Label>
                        <telerik:RadTextBox ID="TxtCAT_RED_NOMBRE" runat="server" Width="100%" MaxLength="50" placeholder="Nombre del reporte" ></telerik:RadTextBox>
                        <asp:RegularExpressionValidator runat="server" Display="Dynamic" ErrorMessage="No se admiten caracteres especiales" ValidationExpression="[a-zA-Z]+" ControlToValidate="TxtCAT_RED_NOMBRE" ForeColor="Red" SetFocusOnError="true">
                        </asp:RegularExpressionValidator>
                    </div>
                    <div class="col-sm-6 col-md-3 mt-2">
                        <asp:Label ID="LblCAT_RED_DESCRIPCION" runat="server" Text="Descripción del reporte:"></asp:Label>
                        <telerik:RadTextBox ID="TxtCAT_RED_DESCRIPCION" runat="server" MaxLength="100" Width="100%" placeholder="Descripción del reporte"></telerik:RadTextBox>
                    </div>
                    <div class="col-sm-6 col-md-3 mt-2">
                        <asp:Label ID="LblCAT_RED_BLOQUE" runat="server" Text="Grupo al que pertenece:"></asp:Label>
                        <telerik:RadDropDownList ID="DdlCAT_RED_BLOQUE" runat="server" Width="100%" DefaultMessage="Seleccione">
                        </telerik:RadDropDownList>
                    </div>
                    <div class="col-sm-6 col-md-3 mt-2">
                        <div class="custom-control custom-switch">
                            <input type="checkbox" class="custom-control-input" runat="server" id="switch1" />
                            <label class="custom-control-label" runat="server" for="CPHMaster_switch1">
                                Mostrar toda la informacion 
                                <i class="material-icons" id="helpIcon" style="font-size: 1em">&#xe8fd;</i>
                            </label>
                            <telerik:RadToolTip runat="server" Position="BottomCenter" IsClientID="true" TargetControlID="helpIcon" ShowEvent="OnMouseOver" AutoCloseDelay="6000" HideEvent="LeaveTargetAndToolTip" Width="150px" RelativeTo="Element">
                                En algunos casos, un crédito no tendrá información
                                en un catálogo por lo que este crédito no será visible
                                a menos de que esta opción esté activada.
                            </telerik:RadToolTip>
                        </div>
                    </div>
                </div>
            </fieldset>
            <fieldset class="my-1">
                <legend>Campos para generaci&oacute;n de reporte</legend>
                <div class="row">
                    <div class="col-sm-6 pt-2">
                        <div class="font-weight-bold">Cat&aacute;logos disponibles:</div>
                        <telerik:RadListBox runat="server" ID="listBoxCatalogosDisponibles" Height="200px" Width="100%" AllowTransfer="true" TransferToID="listBoxCatalogosSeleccionados" ButtonSettings-AreaWidth="35px" AutoPostBackOnTransfer="true" AllowTransferOnDoubleClick="true" SelectionMode="Multiple" TransferMode="Move">
                        </telerik:RadListBox>
                    </div>
                    <div class="col-sm-6 pt-2">
                        <div class="font-weight-bold">Cat&aacute;logos seleccionados: </div>
                        <telerik:RadListBox runat="server" ID="listBoxCatalogosSeleccionados" Height="200px" Width="100%" ButtonSettings-AreaWidth="35px" AllowTransferOnDoubleClick="true" SelectionMode="Multiple" AllowTransferDuplicates="false">
                        </telerik:RadListBox>
                    </div>
                </div>
                <hr />
                <div class="row">
                    <div class="col-sm-6 pt-2">
                        <div class="font-weight-bold">Campos disponibles:</div>
                        <telerik:RadListBox runat="server" ID="listBoxCamposDisponibles" Height="300px" Width="100%" AllowTransfer="true" TransferToID="listBoxCamposSeleccionados" ButtonSettings-AreaWidth="35px" AutoPostBack="false" AutoPostBackOnTransfer="true" AllowTransferOnDoubleClick="true" SelectionMode="Multiple" TransferMode="Move">
                        </telerik:RadListBox>
                    </div>
                    <div class="col-sm-6 pt-2">
                        <div class="font-weight-bold">Campos seleccionados:</div>
                        <telerik:RadListBox runat="server" ID="listBoxCamposSeleccionados" Height="300px" Width="100%" ButtonSettings-AreaWidth="35px" AutoPostBack="false" AllowTransferOnDoubleClick="false" SelectionMode="Multiple" AllowReorder="true" AllowTransferDuplicates="false">
                            <ButtonSettings ShowReorder="true" />
                            <ItemTemplate>
                                <div class="m-2 p-1 shadow-sm bg-light w3-text-black rounded">
                                    <h5 class="text-truncate"><%# DataBinder.Eval(Container, "Text")%><small class="text-muted"> (<%# DataBinder.Eval(Container, "Attributes['TipoDisplay']") %>)</small></h5>
                                    <div class="row">
                                        <div class="col-5">
                                            <telerik:RadCheckBox runat="server" ID="checkCondicional" Text="¿Campo Condicional?" AutoPostBack="false"></telerik:RadCheckBox>
                                        </div>
                                        <div class="col-7">
                                            <label>Formato:</label><br />
                                            <telerik:RadDropDownList runat="server" ID="ddlFormato" Width="100%" DefaultMessage="Seleccione..." OnDataBinding="ddlFormato_DataBinding"></telerik:RadDropDownList>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </telerik:RadListBox>
                    </div>
                </div>
                <div class="text-right w3-block mt-2">
                    <telerik:RadButton runat="server" ID="btnConfirmar" Text="Siguiente" CssClass="bg-success text-white border-0" Width="150px"></telerik:RadButton>
                </div>
            </fieldset>
        </div>
        <telerik:RadNotification ID="RadNotification1" runat="server" EnableRoundedCorners="true" EnableShadow="true" Position="Center"  Width="300" Height="100" AutoCloseDelay="5100">
        </telerik:RadNotification>
    </telerik:RadAjaxPanel>
    <div id="endPage"></div>
</asp:Content>

