<%@ Page Title="" Language="VB" MasterPageFile="~/M_Administrador/MasterPage.master" AutoEventWireup="false" CodeFile="Rutas.aspx.vb" Inherits="Rutas" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPHMaster" runat="Server">
    <div class="Titulos w3-margin-bottom">
        <b>Rutas</b>
    </div>
    <telerik:RadAjaxPanel runat="server" ID="pnl" CssClass="w3-row-padding w3-text-black">
        <div class="w3-col s12 m4">
            <label>Carga archivo de rutas (.csv):</label><br />
            <label>Layout: C.P.,Ruta</label><br />
            <telerik:RadAsyncUpload runat="server" ID="uploadArchivo" AllowedFileExtensions=".csv" MultipleFileSelection="Disabled" PostbackTriggers="btnCargar" MaxFileInputsCount="1" Width="100%" Culture="es-MX"></telerik:RadAsyncUpload>
            <br />
            <telerik:RadButton runat="server" ID="btnCargar" Text="Cargar"></telerik:RadButton>
            <br />
            <telerik:RadListView ID="rl1" runat="server" ItemPlaceholderID="CategoryItemsHolder" Width="100%">
                <LayoutTemplate>
                    <fieldset>
                        <legend>Estado:</legend>
                        <asp:Panel ID="CategoryItemsHolder" runat="server" Style="max-height: 200px; width: 100%; overflow: auto;">
                        </asp:Panel>
                    </fieldset>
                </LayoutTemplate>
                <ItemTemplate>
                    <span>
                        <%# Eval("msg") %>
                    </span>
                    <br />
                </ItemTemplate>
            </telerik:RadListView>
        </div>
        <div class="w3-col s12 m8">
            <telerik:RadGrid runat="server" ID="gridRutas" AllowPaging="true" PageSize="10" AutoGenerateColumns="false" AllowMultiRowSelection="True" ClientSettings-EnableRowHoverStyle="true" >
                <MasterTableView CommandItemDisplay="Top">
                    <CommandItemSettings AddNewRecordText="Añadir ruta" RefreshText="Actualizar" />
                    <Columns>
                        <telerik:GridBoundColumn HeaderText="ID" AllowSorting="true" DataField="ID"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="RUTA" AllowSorting="true" DataField="RUTA"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="C.P Asignados" AllowSorting="true" DataField="cuantos"></telerik:GridBoundColumn>
                    </Columns>
                    <EditFormSettings EditFormType="Template">
                        <FormTemplate>
                            <div class="w3-row-padding">
                                <div class="w3-col s12 m6">
                                    <label>Nombre de la ruta:</label>
                                    <telerik:RadTextBox runat="server" ID="tbNombre" Text='<%# Bind("RUTA") %>' EmptyMessage="Nombre..."></telerik:RadTextBox>
                                </div>
                            </div>
                            <telerik:RadButton ID="btnUpdate" Text="Añadir" runat="server" CommandName="PerformInsert"></telerik:RadButton>
                            <telerik:RadButton ID="btnCancel" Text="Cancelar" runat="server" CausesValidation="False" CommandName="Cancel"></telerik:RadButton>
                        </FormTemplate>
                    </EditFormSettings>
                    <NestedViewTemplate>
                        <div class="w3-block w3-blue w3-text-white w3-center"><b>Códigos Postales Registrados en <%#Eval("RUTA") %> </b></div>
                        <div class="w3-border w3-border-blue">
                            <%# Eval("CPs").ToString.TrimEnd(", ") %>
                        </div>
                    </NestedViewTemplate>
                </MasterTableView>

            </telerik:RadGrid>
        </div>
        <telerik:RadWindowManager ID="RadAviso" runat="server"></telerik:RadWindowManager>

    </telerik:RadAjaxPanel>

</asp:Content>

