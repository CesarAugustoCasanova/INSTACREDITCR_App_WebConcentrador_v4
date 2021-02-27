<%@ Page Language="VB" MasterPageFile="./MasterPage.master" AutoEventWireup="false" CodeFile="CreacionJuicios.aspx.vb" Inherits="CreacionJuicios" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHMaster" runat="Server">

    <script type="text/javascript" id="telerikClientEvents1">
        //<![CDATA[

        function RadWizard1_Load(sender, args) {
            for (var i = 1; i < sender.get_wizardSteps().get_count() ; i++) {
                sender.get_wizardSteps().getWizardStep(i).set_enabled(false);
            }
        }
        //]]>
    </script>

    <script type="text/javascript" id="telerikClientEvents2">
        function RadWizard1_ButtonClicking(sender, args) {
            if (!args.get_nextActiveStep().get_enabled()) {
                args.get_nextActiveStep().set_enabled(true);
            }
        }
    </script>


    <telerik:RadAjaxLoadingPanel ID="Radpanelcarga" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RAPContenedor1" runat="server" LoadingPanelID="Radpanelcarga">
        <table class="Table">
            <tr class="Titulos">
                <td colspan="3">Creación de Flujo de Juicios
                </td>
            </tr>
            <tr>
                <td>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  </td>
                <td>
                    <telerik:RadWizard RenderMode="Lightweight" ID="RadWizard1" runat="server" Height="90%" Width="100%" BorderStyle="Groove" DisplayProgressBar="True" NavigationBarPosition="Top" ProgressBarPosition="Top" Localization-Next="Siguente" Localization-Finish="Finalizar" Localization-Previous="Anterior">
                        <WizardSteps>
                            <telerik:RadWizardStep ID="NombreJuicioWizardStep" Title="Juicio" runat="server" StepType="Start" ValidationGroup="EncabezadoInfo" CausesValidation="false" SpriteCssClass="accountInfo">
                                <table align="Center">
                                    <tr class="inputWapper first">
                                        <td class="t" colspan="2">
                                            <telerik:RadLabel ID="LblNombre" Text="Nombre del Juicio: " runat="server" AssociatedControlID="RdDlNombre" Width="100%" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="i" colspan="2">
                                            <telerik:RadListBox RenderMode="Lightweight" ID="RLBNombres" runat="server" Width="256px" Height="320"
                                                AutoPostBack="true" DataKeyField="ID" DataValueField="ID"
                                                DataTextField="Nombres">
                                            </telerik:RadListBox>
                                        </td>
                                    </tr>
                                </table>
                            </telerik:RadWizardStep>

                            <telerik:RadWizardStep ID="EtapasWizardStep" Title="Asignar Etapas" runat="server" StepType="Step" ValidationGroup="EncabezadoInfo" CausesValidation="false" SpriteCssClass="accountInfo">
                                <table align="Center">
                                    <tr class="inputWapper first">
                                        <td style="width: 50%;">
                                            <label>Disponibles:</label>
                                            <telerik:RadListBox RenderMode="Lightweight" runat="server" ID="RadListBoxSource" Height="320"
                                                AllowTransfer="true" TransferToID="RadListBoxDestination" DataKeyField="Valor" DataValueField="Valor"
                                                DataTextField="Texto"
                                                ButtonSettings-AreaWidth="35px" AutoPostBackOnTransfer="true">
                                            </telerik:RadListBox>
                                        </td>
                                        <td style="width: 50%;">
                                            <label>Seleccionadas:</label>
                                            <telerik:RadListBox RenderMode="Lightweight" runat="server" ID="RadListBoxDestination" Height="320"
                                                ButtonSettings-AreaWidth="35px" DataKeyField="CAT_EJ_IDETAPA" DataValueField="CAT_EJ_IDETAPA"
                                                DataTextField="Nombre" AutoPostBackOnDelete="true" AutoPostBackOnReorder="true">
                                            </telerik:RadListBox>
                                        </td>

                                    </tr>
                                </table>
                            </telerik:RadWizardStep>

                            <telerik:RadWizardStep ID="PromoWizardStep" Title="Asignar Promociones" runat="server" StepType="Step" ValidationGroup="EncabezadoInfo" CausesValidation="false" SpriteCssClass="accountInfo">
                                <table align="Center">
                                    <tr>
                                        <td style="width: 33%;">
                                            <telerik:RadLabel ID="RadLabel1" Text="Etapa: " runat="server" AssociatedControlID="RdDlEtapa" Width="100%" />
                                            <telerik:RadListBox RenderMode="Lightweight" ID="RLBEtapas" runat="server" Width="256px" Height="320"
                                                AutoPostBack="true" DataKeyField="CAT_EJ_IDETAPA" DataValueField="CAT_EJ_IDETAPA"
                                                DataTextField="Nombre">
                                            </telerik:RadListBox>
                                        </td>
                                        <td style="width: 33%;">
                                            <label>Disponibles:</label>
                                            <telerik:RadListBox RenderMode="Lightweight" runat="server" ID="RadListBoxSource2" Height="320"
                                                AllowTransfer="true" TransferToID="RadListBoxDestination2" DataKeyField="CAT_PJ_IDPROMOCION" DataValueField="CAT_PJ_IDPROMOCION"
                                                DataTextField="Nombre"
                                                ButtonSettings-AreaWidth="35px" AutoPostBackOnTransfer="true" AutoPostBackOnReorder="true">
                                            </telerik:RadListBox>
                                        </td>
                                        <td style="width: 33%;">
                                            <label>Seleccionadas:</label>
                                            <telerik:RadListBox RenderMode="Lightweight" runat="server" ID="RadListBoxDestination2" Height="320"
                                                ButtonSettings-AreaWidth="35px" DataKeyField="CAT_PJ_IDPROMOCION" DataValueField="CAT_PJ_IDPROMOCION"
                                                DataTextField="NOMBRE" AutoPostBackOnDelete="true">
                                            </telerik:RadListBox>
                                        </td>
                                    </tr>
                                </table>
                            </telerik:RadWizardStep>

                            <telerik:RadWizardStep ID="AcuerdosWizardStep" Title="Asignar Acuerdos" runat="server" StepType="Step" ValidationGroup="EncabezadoInfo" CausesValidation="false" SpriteCssClass="accountInfo">
                                <table align="Center">
                                   
                                    <tr>
                                        <td  style="width: 33%;">
                                            <telerik:RadLabel ID="RadLabel2" Text="Promocion: " runat="server" AssociatedControlID="RdDlEtapa" Width="100%" />
                                            <telerik:RadListBox RenderMode="Lightweight" ID="RLBEtapas2" runat="server"  Height="320"
                                                AutoPostBack="true" DataKeyField="CAT_PJ_IDPROMOCION" DataValueField="CAT_PJ_IDPROMOCION"
                                                DataTextField="Nombre">
                                            </telerik:RadListBox>
                                        </td>
                                        <td  style="width: 33%;">
                                            <label>Disponibles:</label>
                                            <telerik:RadListBox RenderMode="Lightweight" runat="server" ID="RadListBoxSource3" Height="320"
                                                AllowTransfer="true" TransferToID="RadListBoxDestination3" DataKeyField="CAT_AJ_ID" DataValueField="CAT_AJ_ID"
                                                DataTextField="Nombre"
                                                ButtonSettings-AreaWidth="35px" AutoPostBackOnTransfer="true" AutoPostBackOnReorder="true">
                                            </telerik:RadListBox>
                                        </td>
                                        <td style="width: 33%;">
                                            <label>Seleccionadas:</label>
                                            <telerik:RadListBox RenderMode="Lightweight" runat="server" ID="RadListBoxDestination3" Height="320"
                                                ButtonSettings-AreaWidth="35px" DataKeyField="CAT_AJ_ID" DataValueField="CAT_AJ_ID"
                                                DataTextField="NOMBRE" AutoPostBackOnDelete="true">
                                            </telerik:RadListBox>
                                        </td>
                                    </tr>
                                </table>
                            </telerik:RadWizardStep>
                            <telerik:RadWizardStep ID="ResultadosWizardStep" Title="Asignar Resultados" runat="server" StepType="Finish" ValidationGroup="EncabezadoInfo" CausesValidation="false" SpriteCssClass="accountInfo">
                                <table align="Center">
                                  
                                    <tr>
                                        <td  style="width: 33%;">
                                            <telerik:RadLabel ID="RadLabel3" Text="Acuerdo: " runat="server" AssociatedControlID="RdDlResultado" Width="100%" />
                                            <telerik:RadListBox RenderMode="Lightweight" ID="RLBEtapas3" runat="server" Height="320"
                                                AutoPostBack="true" DataKeyField="CAT_AJ_ID" DataValueField="CAT_AJ_ID"
                                                DataTextField="Nombre">
                                            </telerik:RadListBox>
                                        </td>
                                        <td  style="width: 33%;">
                                            <label>Disponibles:</label>
                                            <telerik:RadListBox RenderMode="Lightweight" runat="server" ID="RadListBoxSource4" Height="320"
                                                AllowTransfer="true" TransferToID="RadListBoxDestination4" DataKeyField="CAT_RJ_IDRESULTADO" DataValueField="CAT_RJ_IDRESULTADO"
                                                DataTextField="Nombre"
                                                ButtonSettings-AreaWidth="35px" AutoPostBackOnTransfer="true" AutoPostBackOnReorder="true">
                                            </telerik:RadListBox>
                                          </td>
                                        <td  style="width: 33%;">
                                            <label>Seleccionadas:</label>
                                            <telerik:RadListBox RenderMode="Lightweight" runat="server" ID="RadListBoxDestination4" Height="320"
                                                ButtonSettings-AreaWidth="35px" DataKeyField="CAT_RJ_IDRESULTADO" DataValueField="CAT_RJ_IDRESULTADO"
                                                DataTextField="NOMBRE" AutoPostBackOnDelete="true">
                                            </telerik:RadListBox>
                                        </td>
                                    </tr>
                                </table>
                            </telerik:RadWizardStep>
                            <%-- <telerik:RadWizardStep ID="ResultadosWizardStep" Title="Asignar Resultados" runat="server" StepType="Step" ValidationGroup="EncabezadoInfo" CausesValidation="false" SpriteCssClass="accountInfo">
                                <table>

                                    <tr class="inputWapper first">
                                        <td class="t">
                                            <telerik:RadLabel ID="LblEtapa2" Text="Etapa: " runat="server" AssociatedControlID="RdDlEtapa2" Width="100%" />
                                        </td>
                                        <td class="t">
                                            <telerik:RadLabel ID="LblPromocion2" Text="Promocion: " runat="server" AssociatedControlID="RdDlEtapa2" Width="100%" Visible="false" />
                                        </td>
                                        <td class="t">
                                            <telerik:RadLabel ID="LblResultado2" Text="Resultados: " runat="server" Width="100%" Visible="false" />
                                        </td>
                                        <td rowspan="2">
                                            <telerik:RadButton ID="RdBtnGuardar2" runat="server" Visible="false" Text="Guardar" ></telerik:RadButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="i">
                                            <telerik:RadListBox RenderMode="Lightweight" ID="RLBEtapa1" runat="server" Width="256px" Height="320" 
                                                AutoPostBack="true" DataKeyField="Nombre" DataValueField="Nombre" label="Etapa"
                                                DataTextField="Nombre">
                                            </telerik:RadListBox>
                                        </td>
                                        <td class="i">
                                            <telerik:RadListBox RenderMode="Lightweight" ID="RLBPromociones1" runat="server" Width="256px" Height="320" 
                                                AutoPostBack="true" DataKeyField="Valor" DataValueField="Valor" Visible="false"
                                                DataTextField="Texto">
                                            </telerik:RadListBox>
                                        </td>
                                        <td class="i">
                                            <telerik:RadGrid ID="RgdResultados" runat="server" Visible="false" AllowMultiRowSelection="True" >
                                                <MasterTableView>
                                                    <Columns>
                                                        <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn">
                                                        </telerik:GridClientSelectColumn>
                                                    </Columns>
                                                </MasterTableView>
                                                <ClientSettings EnableRowHoverStyle="true">
                                                    <Selecting AllowRowSelect="True" />
                                                </ClientSettings>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="t">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="i">&nbsp;</td>
                                    </tr>
                                </table>
                            </telerik:RadWizardStep>--%>
                        </WizardSteps>
                    </telerik:RadWizard>
                    <asp:Label ID="LError" runat="server" Text="" ForeColor="Red"></asp:Label>
                    <telerik:RadWindowManager ID="RadAviso" runat="server"></telerik:RadWindowManager>
                    <asp:HiddenField ID="HidenUrs" runat="server" />
                </td>
                <td>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</td>
            </tr>
        </table>
    </telerik:RadAjaxPanel>



</asp:Content>


<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .auto-style1 {
            left: 0px;
            top: 6px;
        }
    </style>
</asp:Content>
