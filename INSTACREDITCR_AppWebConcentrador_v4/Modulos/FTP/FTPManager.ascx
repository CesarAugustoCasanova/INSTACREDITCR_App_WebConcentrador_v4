<%@ Control Language="VB" AutoEventWireup="false" CodeFile="FTPManager.ascx.vb" Inherits="Modulos_FTP_FTPManager" %>

<div class="container">
    <div class="row">
        <div class="col-md-6">
            <h3>Carpetas</h3>
            <asp:Label runat="server" ID="lblRuta" CssClass="mb-2" ></asp:Label>
            <asp:LinkButton runat="server" Text="Carpeta anterior" OnClick="MoveUpDirectory"></asp:LinkButton>
            <telerik:RadGrid runat="server" ID="gvDirectorio" AutoGenerateColumns="false" Width="100%" AllowSorting="false" AllowFilteringByColumn="true">
                <MasterTableView AllowFilteringByColumn="True">
                    <Columns>
                        <telerik:GridBoundColumn DataField="Name" HeaderText="Carpeta" AllowFiltering="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlAltText="Buscar..." AutoPostBackOnFilter="true"  >
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn AllowFiltering="false" AllowSorting="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDownload" runat="server" Text="Ir" OnClick="MoveDownDirectory"
                                    CommandArgument='<%# Eval("Name") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                    <NoRecordsTemplate>
                        No encontramos carpetas en este nivel
                    </NoRecordsTemplate>
                </MasterTableView>
            </telerik:RadGrid>
        </div>
        <div class="col-md-6">
            <h3>Archivos</h3>
            <telerik:RadGrid runat="server" ID="gvFiles" AutoGenerateColumns="false" Width="100%" AllowSorting="false" AllowFilteringByColumn="true">
                <MasterTableView AllowFilteringByColumn="True">
                    <Columns>
                        <telerik:GridBoundColumn DataField="Name" HeaderText="Archivo" AllowFiltering="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlAltText="Buscar..." AutoPostBackOnFilter="true" >
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Size" HeaderText="Tamaño (KB)" AllowFiltering="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Date" HeaderText="Fecha de creación" AllowFiltering="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn AllowFiltering="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDownload" runat="server" Text="Descargar" OnClick="DownloadFile"
                                    CommandArgument='<%# Eval("Name") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                    <NoRecordsTemplate>
                        No encontramos archivos en esta carpeta
                    </NoRecordsTemplate>
                </MasterTableView>
            </telerik:RadGrid>
        </div>
    </div>
</div>


