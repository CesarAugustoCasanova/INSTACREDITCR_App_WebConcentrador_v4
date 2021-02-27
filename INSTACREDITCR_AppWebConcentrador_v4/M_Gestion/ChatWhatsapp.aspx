<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ChatWhatsapp.vb" Inherits="M_Gestion_ChatWhatsapp" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="JS/iFrameMejorado.js?v=2" type="text/javascript"></script>
    <link rel="stylesheet" href="Estilos/w3.css" />
    <title></title>
    <meta charset="utf-8" />
    <script type="text/javascript">
        function GridCreated(sender, eventArgs) {
            var scrollArea = document.getElementById(sender.get_element().id + "_GridData");
            var length = sender.get_masterTableView().get_dataItems().length;
            var row = sender.get_masterTableView().get_dataItems()[length - 1];
            //console.log(row.get_element().offsetTop)
            scrollArea.scrollTop = row.get_element().offsetTop;
        }
    </script>
</head>
<body style="font-size: .7em" class="scroll">
    <form id="form1" runat="server" onmousemove="window.parent.movement();">
        <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />

        <telerik:RadButton ID="BtnRegresar" Text="<--" runat="server"></telerik:RadButton>
        <telerik:RadButton ID="BtnOtrosChats" Text="Conversaciones" runat="server"></telerik:RadButton>
        <asp:Panel runat="server" ID="PnlChat">
            <h1>
                <label>
                    Chat Whatsapp                                                   
                </label>
                <%-- <telerik:Radtextbox ID="lbltelwhatsapp" runat="server" Text="" MaxLength="10" Width="250"></telerik:Radtextbox>
                <label>
                    )                                                 
                </label>--%>
            </h1>
            <label>
                Credito:
            </label>
            <telerik:RadLabel runat="server" ID="lblCredito"></telerik:RadLabel>
            <label>
                Telefono:
            </label>
            <telerik:RadLabel runat="server" ID="lblTelefono"></telerik:RadLabel>
            <telerik:RadDropDownList runat="server" ID="RDLTelefonos" DefaultMessage="Seleccione..." Width="20%" AutoPostBack="true"></telerik:RadDropDownList>
            <div>
                <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                    </Triggers>
                    <ContentTemplate>
                        <telerik:RadGrid runat="server" ID="GrdWhatsapp" OnNeedDataSource="GrdWhatsapp_NeedDataSource" AutoGenerateColumns="false" ClientSettings-Scrolling-UseStaticHeaders="true" ClientSettings-Scrolling-SaveScrollPosition="false" MasterTableView-AllowPaging="false" ClientSettings-Scrolling-AllowScroll="true" ClientSettings-Scrolling-ScrollHeight="240" EnableAllOptionInPagerComboBox="true" Visible="True" AllowPaging="True" GridLines="none">
                            <PagerStyle EnableAllOptionInPagerComboBox="true" />
                            <ClientSettings ClientEvents-OnGridCreated="GridCreated"></ClientSettings>
                            <MasterTableView>
                                <Columns>
                                    <telerik:GridTemplateColumn HeaderText="Whats" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <telerik:RadLabel runat="server" Text='<%#Eval("whats") %> ' CssClass="w3-round-medium w3-green"></telerik:RadLabel>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="MC">
                                        <ItemTemplate>
                                            <telerik:RadLabel runat="server" Text='<%#Eval("mc") %> ' CssClass="w3-round-medium w3-blue"></telerik:RadLabel>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <%--<telerik:GridBoundColumn DataField="whats"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="MC" DataField="mc"></telerik:GridBoundColumn>--%>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
            <div>
                <asp:Timer ID="Timer1" OnTick="Timer1_Tick" runat="server" Interval="3000">
                </asp:Timer>
            </div>
            <br />
            <br />
            <label>Plantilla:</label>
            <telerik:RadDropDownList ID="RDLPlWhatsapp" runat="server" DefaultMessage="Seleccione..." Width="50%" AutoPostBack="true"></telerik:RadDropDownList>
            <br />
            <br />
            <label>Mensaje:</label>
            <telerik:RadTextBox ID="TxtMensaje" runat="server" AutoPostBack="false" CssClass="w3-input" Width="100%"></telerik:RadTextBox>
            <br />
            <br />
            <telerik:RadButton ID="RBEnviar" Text="Enviar" runat="server"></telerik:RadButton>
            <asp:Label runat="server" ID="lblmsj" Style="color: red"></asp:Label>
        </asp:Panel>
        <asp:Panel runat="server" ID="PnlOtrosChats">
            <telerik:RadGrid runat="server" ID="RGChats" OnNeedDataSource="RGChats_NeedDataSource" AutoGenerateColumns="false" ClientSettings-Scrolling-UseStaticHeaders="true" MasterTableView-AllowPaging="false" ClientSettings-Scrolling-AllowScroll="true" ClientSettings-Scrolling-ScrollHeight="240" ClientSettings-EnableRowHoverStyle="true" EnableAllOptionInPagerComboBox="true" Visible="True" AllowPaging="True" GridLines="none" CssClass="w3-table-all w3-hoverable">
                <PagerStyle EnableAllOptionInPagerComboBox="true" />
                <MasterTableView>
                    <Columns>
                        <telerik:GridButtonColumn UniqueName="irChat" HeaderText="irChat" Text="irChat" CommandName="irChat" ButtonType="ImageButton" ImageUrl="Imagenes/gestion.png"></telerik:GridButtonColumn>
                        <telerik:GridBoundColumn HeaderText="Whats1" DataField="whats"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="MC1" DataField="mc"></telerik:GridBoundColumn>


                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>

        </asp:Panel>
    </form>

</body>
</html>
<div id="down"></div>
<script>
    focusDown = () => {
        $([document.documentElement, document.body]).animate({
            scrollTop: $("#down").offset().top
        }, 1000);
    }

</script>
