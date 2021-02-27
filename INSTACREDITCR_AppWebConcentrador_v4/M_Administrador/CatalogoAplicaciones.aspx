<%@ Page Title="" Language="VB" MasterPageFile="~/M_Administrador/MasterPage.master" AutoEventWireup="false" CodeFile="CatalogoAplicaciones.aspx.vb" Inherits="CatalogoAplicaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMaster" Runat="Server">
     <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            var uploadedFilesCount = 0;
            var isEditMode;
            function validateRadUpload(source, e) {
 
                if (isEditMode == null || isEditMode == undefined) {
                    e.IsValid = false;
 
                    if (uploadedFilesCount > 0) {
                        e.IsValid = true;
                    }
                }
                isEditMode = null;
            }
 
            function OnClientFileUploaded(sender, eventArgs) {
                uploadedFilesCount++;
            }
 
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadGrid RenderMode="Lightweight" runat="server" ID="RadGrid1" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" Width="100%" GridLines="None" 
            OnItemCreated="RadGrid1_ItemCreated" PageSize="10" OnInsertCommand="RadGrid1_InsertCommand"
            OnNeedDataSource="RadGrid1_NeedDataSource"
            OnUpdateCommand="RadGrid1_UpdateCommand" OnItemCommand="RadGrid1_ItemCommand">
            <PagerStyle Mode="NumericPages" AlwaysVisible="true"></PagerStyle>
            <MasterTableView Width="100%" CommandItemDisplay="Top" DataKeyNames="ID">
                <CommandItemSettings AddNewRecordText="Nueva Aplicacion" RefreshText="Recargar" />
                <Columns>
                    <telerik:GridEditCommandColumn>
                        <HeaderStyle Width="36px"></HeaderStyle>
                    </telerik:GridEditCommandColumn>
                    <telerik:GridTemplateColumn HeaderText="ID" UniqueName="AppID" DataField="ID" HeaderStyle-Width="100px">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# TrimDescription(DirectCast(IIf(Eval("ID").ToString IsNot DBNull.Value, Eval("ID").ToString, String.Empty), String)) %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                           <asp:Label ID="lblID2" runat="server" Text='<%# TrimDescription(DirectCast(IIf(Eval("ID").ToString IsNot DBNull.Value, Eval("ID").ToString, String.Empty), String)) %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemStyle VerticalAlign="Top"></ItemStyle>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="ICONO" HeaderText="ICONO" UniqueName="AppIcon" HeaderStyle-Width="100px">
                        <ItemTemplate>
                            <telerik:RadBinaryImage runat="server" ID="RadBinaryImage1" DataValue='<%# IIf(Eval("ICONO") IsNot DBNull.Value, Eval("ICONO"), New System.Byte(-1) {})%>'
                                AutoAdjustImageControlSize="false" Height="80px" Width="80px" ToolTip='<%#Eval("NOMBRE", "Photo of {0}") %>'
                                AlternateText='<%#Eval("NOMBRE", "Photo of {0}") %>'></telerik:RadBinaryImage>
                        </ItemTemplate>
                        <InsertItemTemplate>
                            <telerik:RadAsyncUpload RenderMode="Lightweight" runat="server" ID="AsyncUpload1" OnClientFileUploaded="OnClientFileUploaded"
                                AllowedFileExtensions="jpg,jpeg,png,gif,webp" MaxFileSize="1048576" OnFileUploaded="AsyncUpload1_FileUploaded">
                            </telerik:RadAsyncUpload>
                        </InsertItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="NOMBRE" UniqueName="AppName" DataField="NOMBRE">
                        <ItemTemplate>
                            <asp:Label ID="lblNombre" runat="server" Text='<%# TrimDescription(DirectCast(IIf(Eval("NOMBRE") IsNot DBNull.Value, Eval("NOMBRE"), String.Empty), String)) %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <telerik:RadTextBox RenderMode="Lightweight" ID="txtNombre" Width="300px" runat="server" Text='<%# Eval("NOMBRE") %>'>
                            </telerik:RadTextBox>
                        </EditItemTemplate>
                        <ItemStyle VerticalAlign="Top"></ItemStyle>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="PAQUETE" UniqueName="AppPakage" DataField="PAQUETE">
                        <ItemTemplate>
                            <asp:Label ID="lblPaquete" runat="server" Text='<%# TrimDescription(DirectCast(IIf(Eval("PAQUETE") IsNot DBNull.Value, Eval("PAQUETE"), String.Empty), String)) %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <telerik:RadTextBox RenderMode="Lightweight" ID="txtPaquete" Width="300px" runat="server" Text='<%# Eval("PAQUETE") %>'>
                            </telerik:RadTextBox>
                        </EditItemTemplate>
                        <ItemStyle VerticalAlign="Top"></ItemStyle>
                    </telerik:GridTemplateColumn>    
                     <telerik:GridTemplateColumn HeaderText="RESTRICTIVA" UniqueName="Bloqueo" DataField="BLOQUEO">
                        <ItemTemplate>
                            
                            <telerik:RadCheckBox runat="server" ID="ChbBloqueo" Enabled="false" Checked='<%# IIf(Eval("BLOQUEO") = 1, True, False) %>'></telerik:RadCheckBox>
                        </ItemTemplate>
                        <EditItemTemplate>
                           <telerik:RadCheckBox runat="server" ID="ChbBloqueoE"  Checked='<%# If(Eval("BLOQUEO") IsNot DBNull.Value, IIf(Eval("BLOQUEO") = 1, True, False), False) %>'></telerik:RadCheckBox>

                        </EditItemTemplate>
                        <ItemStyle VerticalAlign="Middle"></ItemStyle>
                    </telerik:GridTemplateColumn>                 
                    <%--<telerik:GridButtonColumn Text="Delete" CommandName="Delete" ButtonType="FontIconButton">
                        <HeaderStyle Width="36px"></HeaderStyle>
                    </telerik:GridButtonColumn>--%>
                </Columns>
                <EditFormSettings>
                    <EditColumn ButtonType="FontIconButton">
                    </EditColumn>
                </EditFormSettings>
                <PagerStyle AlwaysVisible="True"></PagerStyle>
            </MasterTableView>
        </telerik:RadGrid>
</asp:Content>

