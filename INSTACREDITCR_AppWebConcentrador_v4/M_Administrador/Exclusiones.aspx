<%@ Page Title="" Language="VB" MasterPageFile="./MasterPage.master" AutoEventWireup="false" CodeFile="Exclusiones.aspx.vb" Inherits="_Exclusiones" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHMaster" runat="Server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnAddExclusion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlExclusiones" LoadingPanelID="LoadingPnl" />
                    <telerik:AjaxUpdatedControl ControlID="pnlExclusion" LoadingPanelID="LoadingPnl" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnCancelarExclusion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlExclusiones" LoadingPanelID="LoadingPnl" />
                    <telerik:AjaxUpdatedControl ControlID="pnlExclusion" LoadingPanelID="LoadingPnl" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnGuardarExclusion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlExclusiones" LoadingPanelID="LoadingPnl" />
                    <telerik:AjaxUpdatedControl ControlID="pnlExclusion" LoadingPanelID="LoadingPnl" />
                    <telerik:AjaxUpdatedControl ControlID="RadNotification1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAnadir">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadNotification1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="gridExclusiones">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlExclusiones" LoadingPanelID="LoadingPnl" />
                    <telerik:AjaxUpdatedControl ControlID="pnlExclusion" LoadingPanelID="LoadingPnl" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <br />
    <br />
    <telerik:RadAjaxLoadingPanel runat="server" RenderMode="Lightweight"  ID="LoadingPnl"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel runat="server" ID="pnlExclusiones" LoadingPanelID="LoadingPnl">
        <telerik:RadButton runat="server" ID="btnAddExclusion" Text="Nueva exclusión" ></telerik:RadButton>
        <br />
        <telerik:RadGrid runat="server" ID="gridExclusiones" AutoGenerateColumns="false" >
            <MasterTableView>
                <Columns>
                    <telerik:GridButtonColumn Text="Seleccionar" CommandName="onSelected" ItemStyle-Width="120px"></telerik:GridButtonColumn>
                    <telerik:GridBoundColumn HeaderText="Identificador" DataField="IDENTIFICADOR" ItemStyle-Width="80px"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Exclusión" DataField="Titulo"></telerik:GridBoundColumn>
                    <telerik:GridButtonColumn Text="Eliminar" CommandName="onDelete" ConfirmDialogType="RadWindow" ConfirmText="Confirma la eliminacion" ItemStyle-Width="120px"></telerik:GridButtonColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </telerik:RadAjaxPanel>

    <telerik:RadAjaxPanel runat="server" CssClass="w3-container w3-text-black" ID="pnlExclusion" LoadingPanelID="LoadingPnl">
        <div class="w3-row-padding">
            <label>Nombre de la Exclusion</label>
            <telerik:RadTextBox runat="server" ID="txtNombreExclusion"></telerik:RadTextBox>
            <br />
            <div class="w3-col s12 m3">
                <telerik:RadListBox ID="lbTablas" runat="server" Width="256px" Height="320" AutoPostBack="true" DataKeyField="ID" DataValueField="ID" DataTextField="Name" Style="width: 100%">
                    <HeaderTemplate>
                        <div class="w3-center">
                            TABLAS
                        </div>
                    </HeaderTemplate>
                    <ClientItemTemplate>
                        <div>
                            #= Text #
                        </div>
                    </ClientItemTemplate>
                    <WebServiceSettings Method="GetTablas" Path="Exclusiones.aspx" />
                </telerik:RadListBox>

            </div>
            <div class="w3-col s12 m5">
                <telerik:RadListBox ID="lbCampos" runat="server" Height="320" AutoPostBack="true"  Style="width: 100%" DataKeyField="Campo" Visible="false">
                    <HeaderTemplate>
                        <div class="w3-center">
                            CAMPOS
                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div>
                            <telerik:RadLabel runat="server" ID="Campo_Nombre" Text='<%# Eval("Campo_Nombre").ToString.Split("|")(0) %>'></telerik:RadLabel>
                            <telerik:RadLabel runat="server" ID="Campo" Text='<%# Eval("Campo").ToString %>' Visible="false"></telerik:RadLabel>
                            <telerik:RadLabel runat="server" ID="Tabla" Text='<%# Eval("Tabla").ToString %>' Visible="false"></telerik:RadLabel>
                            <telerik:RadLabel runat="server" ID="Tipo" Text='<%# Eval("Tipo").ToString %>' Visible="false"></telerik:RadLabel>
                            <telerik:RadLabel runat="server" ID="Nombre" Text='<%# Eval("Nombre").ToString %>' Visible="false"></telerik:RadLabel>
                        </div>
                    </ItemTemplate>
                </telerik:RadListBox>
            </div>
            <div class="w3-col s12 m4">
                <asp:Panel runat="server" ID="pnlDatosCampo" Visible="false">
                    <div>
                        <label>Campo seleccionado:</label>
                        <telerik:RadLabel runat="server" ID="lblCampo" CssClass="w3-center" Width="100%" Font-Bold="true"></telerik:RadLabel>
                    </div>
                    <div>
                        <label>Seleccione una condición:</label>
                        <telerik:RadDropDownList ID="DdlOperador" runat="server" DefaultMessage="Seleccione..." Width="100%">
                            <Items>
                                <telerik:DropDownListItem Text="Mayor Que" Value=">" />
                                <telerik:DropDownListItem Text="Menor Que" Value="<" />
                                <telerik:DropDownListItem Text="Igual" Value="=" />
                                <telerik:DropDownListItem Text="Mayor O Igual" Value=">=" />
                                <telerik:DropDownListItem Text="Menor O Igual" Value="<=" />
                                <telerik:DropDownListItem Text="Distinto" Value="!=" />
                                <telerik:DropDownListItem Text="Que Contenga" Value="In" />
                                <telerik:DropDownListItem Text="Que No Contenga" Value="Not In" />
                            </Items>
                        </telerik:RadDropDownList>
                    </div>
                    <div>
                        <label>Escriba valor para la condición:</label>
                        <telerik:RadTextBox ID="TxtValores" runat="server" Width="100%"></telerik:RadTextBox>
                        <telerik:RadDatePicker runat="server" ID="DteValores" Width="100%" Visible="false"></telerik:RadDatePicker>
                        <telerik:RadNumericTextBox runat="server" ID="NumValores" Width="100%" Visible="false" AllowOutOfRangeAutoCorrect="true" MaxLength="10" NumberFormat-DecimalDigits="2"></telerik:RadNumericTextBox>
                    </div>
                    <div>
                        <label>Seleccione un conector (Dejar en blanco si es el último campo que se agrega):</label>
                        <telerik:RadDropDownList ID="DdlConector" runat="server" AutoPostBack="false" DefaultMessage="Seleccione..." Width="100%">
                            <Items>
                                <telerik:DropDownListItem Value="AND" Text="Y" />
                                <telerik:DropDownListItem Value="OR" Text="O" />
                            </Items>
                        </telerik:RadDropDownList>
                    </div>
                    <br />
                    <div>
                        <telerik:RadButton runat="server" ID="btnAnadir" Text="Añadir regla" CssClass="w3-btn w3-hover-green"></telerik:RadButton>
                        <telerik:RadButton runat="server" ID="btnCancelar" Text="Cancelar" CssClass="w3-btn w3-hover-red"></telerik:RadButton>
                    </div>
                </asp:Panel>
            </div>
        </div>
        <br />
        <div class="w3-row">
            <telerik:RadGrid runat="server" ID="progressGrid" AutoGenerateColumns="false">
                <MasterTableView>
                    <Columns>
                        <telerik:GridButtonColumn ButtonType="PushButton" CommandName="onDelete" CommandArgument="" Text="Eliminar"></telerik:GridButtonColumn>
                        <telerik:GridBoundColumn DataField="Cat_Ta_Desc" HeaderText="Tabla"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Campo_Nombre" HeaderText="Campo"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CondicionText" HeaderText="Condicion"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Valor" HeaderText="Valor"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ConectorText" HeaderText="Conector"></telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn Visible="false">
                            <ItemTemplate>
                                <telerik:RadLabel runat="server" ID="Cat_Ta_Tabla" Text='<%# Eval("Cat_Ta_Tabla") %>'></telerik:RadLabel>
                                <telerik:RadLabel runat="server" ID="Cat_Ta_Pk" Text='<%# Eval("Cat_Ta_Pk") %>'></telerik:RadLabel>
                                <telerik:RadLabel runat="server" ID="Campo" Text='<%# Eval("Campo") %>'></telerik:RadLabel>
                                <telerik:RadLabel runat="server" ID="CampoTabla" Text='<%# Eval("Tabla") %>'></telerik:RadLabel>
                                <telerik:RadLabel runat="server" ID="CampoTipo" Text='<%# Eval("Tipo") %>'></telerik:RadLabel>
                                <telerik:RadLabel runat="server" ID="Nombre" Text='<%# Eval("Nombre") %>'></telerik:RadLabel>
                                <telerik:RadLabel runat="server" ID="CondicionValue" Text='<%# Eval("CondicionValue") %>'></telerik:RadLabel>
                                <telerik:RadLabel runat="server" ID="ConectorValue" Text='<%# Eval("ConectorValue") %>'></telerik:RadLabel>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </div>
        <br />
        <div class="w3-row">
            <div class="w3-col s12 m3">
                <label>Motivo</label>
                <telerik:RadTextBox runat="server" ID="txtMotivo" EmptyMessage="Motivo de la exclusion" TextMode="MultiLine" MaxLength="200" InputType="Text" AutoCompleteType="None" Width="100%"></telerik:RadTextBox>
            </div>
            <div class="w3-col s12 m3">
                <label>¿Activar exclusión?</label>
                <telerik:RadCheckBox runat="server" ID="cbActivo"></telerik:RadCheckBox>
                <%--<telerik:RadButton runat="server" ID="btnActivo" ToggleType="CheckBox" ForeColor="White" Checked="true" Skin="" CssClass="w3-btn">
                    <ToggleStates>
                        <telerik:RadButtonToggleState Text="Activa" CssClass="w3-green w3-text-white"></telerik:RadButtonToggleState>
                        <telerik:RadButtonToggleState Text="Inactiva" CssClass="w3-red w3-text-white"></telerik:RadButtonToggleState>
                    </ToggleStates>
                </telerik:RadButton>--%>
            </div>
            <div class="w3-col s12 m3">
                <label>¿Temporal?</label>
                <telerik:RadCheckBox runat="server" ID="cbVigencia"></telerik:RadCheckBox>
                <%--<telerik:RadButton runat="server" ID="btnVigencia" ToggleType="CheckBox" ForeColor="White" Checked="true" Skin="" CssClass="w3-btn">
                    <ToggleStates>
                        <telerik:RadButtonToggleState Text="Temporal" CssClass="w3-blue w3-text-white"></telerik:RadButtonToggleState>
                        <telerik:RadButtonToggleState Text="Permanente" CssClass="w3-green w3-text-white"></telerik:RadButtonToggleState>
                    </ToggleStates>
                </telerik:RadButton>--%>
            </div>
            <div class="w3-col s12 m3">
                <label>Fecha de vigencia</label>
                <telerik:RadDatePicker runat="server" ID="dpVigencia" DateInput-Enabled="false" DateInput-EmptyMessage="Seleccione" Width="100%"></telerik:RadDatePicker>
            </div>
        </div>
        <br />
        <div class="w3-row">
            <telerik:RadButton runat="server" ID="btnGuardarExclusion" Text="Guardar exclusión"></telerik:RadButton>
            <telerik:RadButton runat="server" ID="btnCancelarExclusion" Text="Cancelar cambios"></telerik:RadButton>
        </div>
    </telerik:RadAjaxPanel>
    <telerik:RadNotification RenderMode="Lightweight" ID="RadNotification1" runat="server" Height="140px" Animation="Fade" EnableRoundedCorners="true" EnableShadow="true" AutoCloseDelay="3500" Position="Center" OffsetX="-30" OffsetY="-70" ShowCloseButton="true" VisibleOnPageLoad="false" LoadContentOn="EveryShow" KeepOnMouseOver="false">
    </telerik:RadNotification>
</asp:Content>
