<%@ Page Language="VB" MasterPageFile="./MasterPage.master" AutoEventWireup="false" CodeFile="CatalogoPuestos.aspx.vb" Inherits="CatalogoPuestos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHMaster" runat="Server">
    <script languaje="javascript" type="text/javascript">

        function confirmCallbackFn(arg) {
            if (arg) {

                __doPostBack('ctl00$CPHMaster$BtnAceptarConfirmacion', '')

            }
        }
    </script>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" ></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="UPNLGeneral" runat="server" UpdatePanelCssClass="w3-center" CssClass="w3-center" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">

        <table class="Table">
            <tr class="Titulos">
                <td>Puestos
                </td>
            </tr>
            <tr align="center">
                <td>
                        <telerik:RadTreeList RenderMode="Lightweight" ID="RadTreeList1" OnNeedDataSource="RadTreeList1_NeedDataSource" runat="server" DataKeyNames="PuestoID" ParentDataKeyNames="ParentPuestoID" EditMode="InPlace" AllowPaging="true" PageSize="10" 
                            AutoGenerateColumns="false"  AllowRecursiveDelete="true" OnItemDeleted="RadTreeList1_ItemDeleted" OnItemInserted="RadTreeList1_ItemInserted" OnItemUpdated="RadTreeList1_ItemUpdated">
            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
            <Columns>
                <telerik:TreeListBoundColumn DataField="PuestoID" UniqueName="PuestoID" HeaderText="ID Puesto" ForceExtractValue="Always" ReadOnly="true" ></telerik:TreeListBoundColumn>
                <telerik:TreeListBoundColumn DataField="Puesto" UniqueName="Puesto" HeaderText="Puesto" ItemStyle-HorizontalAlign="Center"></telerik:TreeListBoundColumn>                
                <telerik:TreeListBoundColumn DataField="ParentPuestoID" UniqueName="ParentPuestoID" HeaderText="IDPadre" ReadOnly="true" ForceExtractValue="Always"></telerik:TreeListBoundColumn>
                <telerik:TreeListEditCommandColumn UniqueName="EditCommandColumn" ButtonType="FontIconButton"></telerik:TreeListEditCommandColumn>
                <telerik:TreeListButtonColumn UniqueName="DeleteCommandColumn" Text="Delete" CommandName="Delete" ButtonType="FontIconButton"></telerik:TreeListButtonColumn>
            </Columns>
        </telerik:RadTreeList>
                </td>
            </tr>
        </table>

        <asp:HiddenField ID="HidenUrs" runat="server" />
        <asp:Button runat="server" ID="BtnAceptarConfirmacion" Visible="true" Height="1px" Style="display: none;" />


        <telerik:RadWindowManager ID="RadAviso" runat="server" >
            <Localization OK="Aceptar" />
        </telerik:RadWindowManager>

    </telerik:RadAjaxPanel>
</asp:Content>