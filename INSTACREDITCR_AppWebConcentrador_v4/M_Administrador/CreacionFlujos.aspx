<%@ Page Title="" Language="VB" MasterPageFile="./MasterPage.master" AutoEventWireup="false" CodeFile="CreacionFlujos.aspx.vb" Inherits="CreacionFlujos" %>

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


    <telerik:RadAjaxLoadingPanel ID="Radpanelcarga" runat="server" ></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RAPContenedor1" runat="server" LoadingPanelID="Radpanelcarga">
        <table class="Table">
            <tr class="Titulos">
                <td colspan="3">Creación de flujos Judiciales
                    </td>
            </tr>
            <tr>
                <td>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  </td>
                <td>
                    <telerik:RadWizard RenderMode="Lightweight" ID="RadWizard1" runat="server" Height="90%" Width="100%" BorderStyle="Groove" DisplayProgressBar="True" NavigationBarPosition="Left" ProgressBarPosition="Left"  Localization-Next="Siguente" Localization-Finish="Finalizar" Localization-Previous="Anterior">
                        <WizardSteps>
                            <telerik:RadWizardStep ID="PromocionesWizardStep" Title="Asignar Promociones" runat="server" StepType="Start" ValidationGroup="EncabezadoInfo" CausesValidation="false" SpriteCssClass="accountInfo">
                                <table>
                                   
                                    <tr class="inputWapper first">
                                        <td class="t" colspan="2">
                                            <telerik:RadLabel ID="LblEtapa" Text="Etapa: " runat="server" AssociatedControlID="RdDlEtapa" Width="100%" />

                                        </td>
                                        <td class="t">
                                            <telerik:RadLabel ID="LblPromocion" Text="Promociones: " runat="server" AssociatedControlID="RcbEtapa" Width="100%" Visible="false" />
                                        </td>
                                        <td rowspan="2">
                                            <telerik:RadButton ID="RdBtnGuardar1" runat="server" Visible="false" Text="Guardar" ></telerik:RadButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="i" colspan="2">
                                            <telerik:RadListBox RenderMode="Lightweight" ID="RLBEtapa0" runat="server" Width="256px" Height="320" 
                                                AutoPostBack="true" DataKeyField="Valor" DataValueField="Valor"
                                                DataTextField="Texto">
                                            </telerik:RadListBox>
                                        </td>
                                        <td class="i">
                                            <telerik:RadGrid ID="RgdPromociones" runat="server" Visible="false" AllowMultiRowSelection="True" >
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
                                </table>
                            </telerik:RadWizardStep>

                            <telerik:RadWizardStep ID="ResultadosWizardStep" Title="Asignar Resultados" runat="server" StepType="Step" ValidationGroup="EncabezadoInfo" CausesValidation="false" SpriteCssClass="accountInfo">
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
                                                AutoPostBack="true" DataKeyField="Valor" DataValueField="Valor" label="Etapa"
                                                DataTextField="Texto">
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
                            </telerik:RadWizardStep>

                            <telerik:RadWizardStep ID="EspecificacionesWizardStep" Title="Asignar Especificaciones" runat="server" StepType="Finish" ValidationGroup="EncabezadoInfo" CausesValidation="false" SpriteCssClass="accountInfo">
                                <table>
                                  
                                    <tr>
                                        <td colspan="2">
                                            <telerik:RadLabel ID="LblEtapa3" runat="server" AssociatedControlID="RdDlEtapa3" Text="Etapa: " Width="100%" />
                                        </td>
                                        <td colspan="2">
                                            <telerik:RadLabel ID="LblPromocion3" Text="Promociones: " runat="server" AssociatedControlID="RcbPromocion3" Width="100%" Visible="false" />
                                        </td>
                                        <td colspan="2">
                                            <telerik:RadLabel ID="LblResultado3" Text="Resultados: " runat="server" AssociatedControlID="RcbResultado3" Width="100%" Visible="false" />
                                        </td>
                                    </tr>
                                    <tr class="inputWapper first">
                                        <td colspan="2">
                                            <telerik:RadListBox RenderMode="Lightweight" ID="RLBEtapa2" runat="server" Width="175px" Height="220" 
                                                AutoPostBack="true" DataKeyField="Valor" DataValueField="Valor"
                                                DataTextField="Texto">
                                            </telerik:RadListBox>
                                        </td>
                                        <td colspan="2">
                                            <telerik:RadListBox RenderMode="Lightweight" ID="RLBPromocion2" runat="server" Width="175px" Height="220" 
                                                AutoPostBack="true" DataKeyField="Valor" DataValueField="Valor" Visible="false"
                                                DataTextField="Texto">
                                            </telerik:RadListBox>
                                        </td>
                                        <td colspan="2">
                                            <telerik:RadListBox RenderMode="Lightweight" ID="RLBResultados" runat="server" Width="175px" Height="220" 
                                                AutoPostBack="true" DataKeyField="Valor" DataValueField="Valor" Visible="false"
                                                DataTextField="Texto">
                                            </telerik:RadListBox>
                                        </td>
                                    </tr>
                                    <tr class="inputWapper first">

                                        <td class="t"></td>
                                        <td>
                                            <telerik:RadLabel ID="LblAvanza" runat="server" AssociatedControlID="RdDlEtapa3" Text="Avanza: " Visible="false" Width="100%" />
                                        </td>
                                        <td>
                                            <telerik:RadDropDownList ID="RdDlAvanza" runat="server" AutoPostBack="true" RenderMode="Lightweight"  Visible="false" Width="100%">
                                                <Items>
                                                    <telerik:DropDownListItem Selected="true" Text="NO" Value="0" />
                                                    <telerik:DropDownListItem Text="SI" Value="1" />
                                                </Items>
                                            </telerik:RadDropDownList>
                                        </td>
                                        <td>
                                            <telerik:RadLabel ID="LblEtapaAvanza" runat="server" AssociatedControlID="RdDlEtapa3" Text="A: " Visible="false" Width="100%" />
                                        </td>
                                        <td>
                                            <telerik:RadDropDownList ID="RdDlEtapaAvanza" runat="server" RenderMode="Lightweight"  Visible="false" Width="100%" DataTextField="Texto" DataValueField="Valor" Enabled="false">
                                            </telerik:RadDropDownList>
                                        </td>
                                        <td></td>

                                    </tr>
                                    <tr>
                                        <td class="i">
                                            <%--<telerik:RadDropDownList RenderMode="Lightweight" ID="RdDlEtapa3" runat="server" Width="100%" AutoPostBack="true" ></telerik:RadDropDownList>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="t"></td>
                                        <td rowspan="2">
                                            <telerik:RadLabel ID="LblFecha" Text="Fecha: " runat="server" AssociatedControlID="RdDlEtapa3" Width="100%" Visible="false" />
                                        </td>
                                        <td rowspan="2">
                                            <telerik:RadDropDownList RenderMode="Lightweight" ID="RdDlFecha" runat="server" Width="100%" AutoPostBack="true" Visible="false" >
                                                <Items>
                                                    <telerik:DropDownListItem Text="NO" Value="0" Selected="true" />
                                                    <telerik:DropDownListItem Text="SI" Value="1" />
                                                </Items>
                                            </telerik:RadDropDownList>
                                        </td>
                                        <td rowspan="2">
                                            <telerik:RadLabel ID="LblFechaOb" Text="Obligatorio: " runat="server" AssociatedControlID="RdDlEtapa3" Width="100%" Visible="false" />
                                        </td>
                                        <td rowspan="2">
                                            <telerik:RadDropDownList RenderMode="Lightweight" ID="RdDlFechaOb" runat="server" Width="100%" AutoPostBack="true" Visible="false"  Enabled="false">
                                                <Items>
                                                    <telerik:DropDownListItem Text="NO" Value="0" Selected="true" />
                                                    <telerik:DropDownListItem Text="SI" Value="1" />
                                                </Items>
                                            </telerik:RadDropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="i">
                                            <%-- <telerik:RadDropDownList RenderMode="Lightweight" ID="RcbPromocion3" runat="server" Width="100%" AutoPostBack="true" Visible="false" ></telerik:RadDropDownList>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="t"></td>
                                        <td rowspan="2">
                                            <telerik:RadLabel ID="LblNota" Text="Nota: " runat="server" AssociatedControlID="RdDlEtapa3" Width="100%" Visible="false" />
                                        </td>
                                        <td rowspan="2">
                                            <telerik:RadDropDownList RenderMode="Lightweight" ID="RcbNota" runat="server" Width="100%" AutoPostBack="true" Visible="false" >
                                                <Items>
                                                    <telerik:DropDownListItem Text="NO" Value="0" Selected="true" />
                                                    <telerik:DropDownListItem Text="SI" Value="1" />
                                                </Items>
                                            </telerik:RadDropDownList>
                                        </td>
                                        <td rowspan="2">
                                            <telerik:RadLabel ID="LblNotaOb" Text="Obligatorio: " runat="server" AssociatedControlID="RdDlEtapa3" Width="100%" Visible="false" />
                                        </td>
                                        <td rowspan="2">
                                            <telerik:RadDropDownList RenderMode="Lightweight" ID="RcbNotaOb" runat="server" Width="100%" AutoPostBack="true" Visible="false"  Enabled="false">
                                                <Items>
                                                    <telerik:DropDownListItem Text="NO" Value="0" Selected="true" />
                                                    <telerik:DropDownListItem Text="SI" Value="1" />
                                                </Items>
                                            </telerik:RadDropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="i">
                                            <%--<telerik:RadDropDownList RenderMode="Lightweight" ID="RcbResultado3" runat="server" Width="100%" AutoPostBack="true" Visible="false" ></telerik:RadDropDownList>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td rowspan="2">
                                            <telerik:RadLabel ID="LblDocumentos" Text="Documentos: " runat="server" AssociatedControlID="RdDlEtapa3" Width="100%" Visible="false" />
                                        </td>
                                        <td rowspan="2">
                                            <telerik:RadDropDownList RenderMode="Lightweight" ID="RcbDocumentos" runat="server" Width="100%" AutoPostBack="true" Visible="false" >
                                                <Items>
                                                    <telerik:DropDownListItem Text="NO" Value="0" Selected="true" />
                                                    <telerik:DropDownListItem Text="SI" Value="1" />
                                                </Items>
                                            </telerik:RadDropDownList>
                                        </td>
                                        <td rowspan="2">
                                            <telerik:RadLabel ID="LblDocumentosOb" Text="Obligatorio: " runat="server" AssociatedControlID="RdDlEtapa3" Width="100%" Visible="false" />
                                        </td>
                                        <td rowspan="2">
                                            <telerik:RadDropDownList RenderMode="Lightweight" ID="RcbDocumentosOb" runat="server" Width="100%" AutoPostBack="true" Visible="false"  Enabled="false">
                                                <Items>
                                                    <telerik:DropDownListItem Text="NO" Value="0" Selected="true" />
                                                    <telerik:DropDownListItem Text="SI" Value="1" />
                                                </Items>
                                            </telerik:RadDropDownList>
                                        </td>
                                        <td rowspan="2">
                                            <telerik:RadLabel ID="LblDocumentosNum" Text="Cuantos: " runat="server" AssociatedControlID="RdDlEtapa3" Width="100%" Visible="false" />
                                        </td>
                                        <td rowspan="2">
                                            <telerik:RadNumericTextBox runat="server" ID="RNTBCuantos" ShowSpinButtons="true"  NumberFormat-DecimalDigits="0" Height="22px" Width="71px" Enabled="false"></telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>
                                            <telerik:RadButton ID="RdBtnGuardar3" runat="server"  Text="Guardar" Visible="false" CssClass="auto-style1" Width="104px">
                                            </telerik:RadButton>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                            </telerik:RadWizardStep>
                        </WizardSteps>
                    </telerik:RadWizard>
                    <asp:Label ID="LError" runat="server" Text="" ForeColor="Red"></asp:Label>
                    <telerik:RadWindowManager ID="RadAviso" runat="server" ></telerik:RadWindowManager>
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



