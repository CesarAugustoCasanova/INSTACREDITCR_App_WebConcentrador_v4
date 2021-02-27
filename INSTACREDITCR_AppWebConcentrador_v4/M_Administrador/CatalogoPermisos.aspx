<%@ Page Title="" Language="VB" MasterPageFile="~/M_Administrador/MasterPage.master" AutoEventWireup="false" CodeFile="CatalogoPermisos.aspx.vb" Inherits="_CatalogoPermisos" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPHMaster" runat="Server">
    <div class="w3-container">
        <telerik:RadAjaxManager runat="server" ID="AjaxManager1">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="btnCrearPerfil">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="pnlPerfiles" />
                        <telerik:AjaxUpdatedControl ControlID="pnlPerfil" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="gridPerfiles">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="pnlPerfiles" />
                        <telerik:AjaxUpdatedControl ControlID="pnlPerfil" />
                        <telerik:AjaxUpdatedControl ControlID="pnlCondonaciones" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnPerfilCancelar">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="pnlPerfiles" />
                        <telerik:AjaxUpdatedControl ControlID="pnlPerfil" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnPerfilGuardar">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="pnlPerfiles" />
                        <telerik:AjaxUpdatedControl ControlID="pnlPerfil" />
                        <telerik:AjaxUpdatedControl ControlID="gridPerfiles" />
                        <telerik:AjaxUpdatedControl ControlID="RadNotification1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnCancelarCondonacion">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="pnlPerfiles" />
                        <telerik:AjaxUpdatedControl ControlID="pnlCondonaciones" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnGuardarCondonacion">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="pnlPerfiles" />
                        <telerik:AjaxUpdatedControl ControlID="pnlCondonaciones" />
                        <telerik:AjaxUpdatedControl ControlID="gridPerfiles" />
                        <telerik:AjaxUpdatedControl ControlID="RadNotification1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel runat="server" ID="LoadingPanel"></telerik:RadAjaxLoadingPanel>
        <telerik:RadAjaxPanel runat="server" ID="pnlPerfiles" LoadingPanelID="LoadingPanel">
            <div class="Titulos">Perfiles</div>
            <telerik:RadGrid runat="server" ID="gridPerfiles" AutoGenerateColumns="false" AllowPaging="true" PageSize="5" AllowFilteringByColumn="true">
              <GroupingSettings CaseSensitive="false"></GroupingSettings>
                  <MasterTableView CommandItemDisplay="Top">
                    <CommandItemSettings ShowAddNewRecordButton="true" ShowCancelChangesButton="false" ShowSaveChangesButton="false" ShowRefreshButton="true" RefreshText="Actualizar" AddNewRecordText="Nuevo Perfil" />
                    <PagerStyle Mode="NextPrev" FirstPageToolTip="Primer página" NextPageToolTip="Siguiente" PrevPageToolTip="Anterior" LastPageToolTip="Última página" />
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="FontIconButton" HeaderTooltip="Editar" HeaderText="Editar"></telerik:GridEditCommandColumn>
                        <telerik:GridButtonColumn ButtonType="PushButton" Text="Eliminar" CommandName="Eliminar" HeaderTooltip="Eliminar" HeaderText="Eliminar"  ConfirmDialogType="RadWindow" ConfirmTextFormatString="¿Seguro que desea eliminar el perfil {0}?" ConfirmTextFields="USUARIO" ConfirmTitle="Eliminar perfil" ></telerik:GridButtonColumn>
                        <telerik:GridBoundColumn HeaderText="ID" DataField="CAT_PE_ID" AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Perfil" DataField="CAT_PE_PERFIL" AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"></telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadAjaxPanel>
        <telerik:RadAjaxPanel runat="server" ID="pnlPerfil" LoadingPanelID="LoadingPanel" Visible="false" CssClass="w3-small">

            <div class="card mt-2">
                <div class="card-header">
                    <div class="input-group">
                        <div class="input-group-prepend">
                            <span class="input-group-text">Permisos del perfil
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ControlToValidate="txtNombrePerfil" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </span>
                        </div>
                        <asp:TextBox runat="server" ID="txtNombrePerfil" CssClass="form-control" placeholder="Nombre del nuevo perfil"></asp:TextBox>
                    </div>
                </div>
                <div class="card-body">


                    <div class="w3-row-padding">
                        <div class="w3-col s12 m4">
                            <div class="w3-container w3-blue w3-center">
                                <b>Administrador</b>
                            </div>
                            <div class="w3-container">
                                <label>Permisos:</label>
                                <telerik:RadComboBox RenderMode="Lightweight" ID="comboAdmin" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" Width="100%" EmptyMessage="Seleccione" Localization-AllItemsCheckedString="Todos los permisos" Localization-CheckAllString="Todos" Localization-ItemsCheckedString="Permisos" CheckedItemsTexts="FitInInput"></telerik:RadComboBox>
                            </div>
                        </div>
                        <div class="w3-col s12 m4">
                            <div class="w3-container w3-blue w3-center">
                                <b>Call Center</b>
                            </div>
                            <div class="w3-container">
                                <label>Menu:</label>
                                <telerik:RadComboBox RenderMode="Lightweight" ID="comboAGestion" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" Width="100%" EmptyMessage="Seleccione" Localization-AllItemsCheckedString="Todos los permisos" Localization-CheckAllString="Todos" Localization-ItemsCheckedString="Permisos" CheckedItemsTexts="FitInInput"></telerik:RadComboBox>
                                <label>Permisos:</label>
                                <telerik:RadComboBox RenderMode="Lightweight" ID="comboPGestion" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" Width="100%" EmptyMessage="Seleccione" Localization-AllItemsCheckedString="Todos los permisos" Localization-CheckAllString="Todos" Localization-ItemsCheckedString="Permisos" CheckedItemsTexts="FitInInput"></telerik:RadComboBox>
                            </div>
                        </div>
                        <div class="w3-col s12 m4">
                            <div class="w3-container w3-blue w3-center">
                                <b>Judicial</b>
                            </div>
                            <div class="w3-container">
                                <label>Permisos:</label>
                                <telerik:RadComboBox RenderMode="Lightweight" ID="comboJudicial" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" Width="100%" EmptyMessage="Seleccione" Localization-AllItemsCheckedString="Todos los permisos" Localization-CheckAllString="Todos" Localization-ItemsCheckedString="Permisos" CheckedItemsTexts="FitInInput"></telerik:RadComboBox>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="w3-row-padding">
                        <div class="w3-col s12 m4">
                            <div class="w3-container w3-blue w3-center">
                                <b>Móvil</b>
                            </div>
                            <div class="w3-container">
                                <label>Permisos:</label>
                                <telerik:RadComboBox RenderMode="Lightweight" ID="comboMovil" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" Width="100%" EmptyMessage="Seleccione" Localization-AllItemsCheckedString="Todos los permisos" Localization-CheckAllString="Todos" Localization-ItemsCheckedString="Permisos" CheckedItemsTexts="FitInInput"></telerik:RadComboBox>
                            </div>
                        </div>
                        <div class="w3-col s12 m4">
                            <div class="w3-container w3-blue w3-center">
                                <b>Back office</b>
                            </div>
                            <div class="w3-container">
                                <label>Permisos:</label>
                                <telerik:RadComboBox RenderMode="Lightweight" ID="comboBackOffice" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" Width="100%" EmptyMessage="Seleccione" Localization-AllItemsCheckedString="Todos los permisos" Localization-CheckAllString="Todos" Localization-ItemsCheckedString="Permisos" CheckedItemsTexts="FitInInput"></telerik:RadComboBox>
                            </div>
                        </div>
                        <div class="w3-col s12 m4">
                            <div class="w3-container w3-blue w3-center">
                                <b>Reportes</b>
                            </div>
                            <div class="w3-container">
                                <label>Permisos:</label>
                                <telerik:RadComboBox RenderMode="Lightweight" ID="comboReportes" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" Width="100%" EmptyMessage="Seleccione" Localization-AllItemsCheckedString="Todos los permisos" Localization-CheckAllString="Todos" Localization-ItemsCheckedString="Permisos" CheckedItemsTexts="FitInInput"></telerik:RadComboBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card-footer">
                    <div class="container">
                        <div class="row">
                            <div class="col-6 px-1">
                                <telerik:RadButton runat="server" ID="btnPerfilGuardar" Text="Guardar" CssClass="bg-success text-white border-0 w3-block"></telerik:RadButton>
                            </div>
                            <div class="col-6 px-1">
                                <telerik:RadButton runat="server" ID="btnPerfilCancelar" Text="Cancelar" CssClass="bg-danger text-white border-0 w3-block"></telerik:RadButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </telerik:RadAjaxPanel>

        <telerik:RadAjaxPanel runat="server" ID="pnlCondonaciones" LoadingPanelID="LoadingPanel" Visible="false">
            <div class="w3-panel w3-center w3-margin w3-text-black ">
                Perfil: <b>
                    <telerik:RadLabel runat="server" ID="lblNombrePerfil"></telerik:RadLabel>
                </b>
            </div>
            <hr />
            <div class="w3-container Titulos w3-center">
                <b>Facultades de condonación</b>
            </div>
            <hr />
            <div class="w3-panel w3-teal w3-text-white w3-center">
                <b>Instancia administrativa y extrajudicial</b>
            </div>
            <div class="w3-row-padding">
                <div class="w3-col s12 m4">
                    <div class="w3-container w3-green w3-text-white w3-center">
                        <b>Capital</b>
                    </div>
                    <div class="w3-container w3-text-black">
                        <label>Porcentaje:</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" ControlToValidate="NtxtCapital1" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <telerik:RadNumericTextBox ID="NtxtCapital1" runat="server" CssClass="fuenteTxt" MinValue="0" MaxValue="100" Type="Percent" NumberFormat-DecimalSeparator="." NumberFormat-GroupSeparator="," Width="100%"></telerik:RadNumericTextBox>
                    </div>
                </div>
                <div class="w3-col s12 m4">
                    <div class="w3-container w3-green w3-text-white w3-center">
                        <b>Interes moratorio</b>
                    </div>
                    <div class="w3-container w3-text-black">
                        <label>Porcentaje:</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="NtxtMoratorio" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <telerik:RadNumericTextBox ID="NtxtMoratorio" runat="server" CssClass="fuenteTxt" MinValue="0" MaxValue="100" Type="Percent" NumberFormat-DecimalSeparator="." NumberFormat-GroupSeparator="," Width="100%"></telerik:RadNumericTextBox>
                    </div>
                </div>
                <div class="w3-col s12 m4">
                    <div class="w3-container w3-green w3-text-white w3-center">
                        <b>Comisiones por Impago</b>
                    </div>
                    <div class="w3-container w3-text-black">
                        <label>Porcentaje:</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="NtxtComImpago" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <telerik:RadNumericTextBox ID="NtxtComImpago" runat="server" CssClass="fuenteTxt" MinValue="0" MaxValue="100" Type="Percent" NumberFormat-DecimalSeparator="." NumberFormat-GroupSeparator="," Width="100%"></telerik:RadNumericTextBox>
                    </div>
                </div>
                <div class="w3-col s12 m4">
                    <div class="w3-container w3-green w3-text-white w3-center">
                        <b>Honorarios</b>
                    </div>
                    <div class="w3-container w3-text-black">
                        <label>Porcentaje:</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="NtxtHonorarios" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <telerik:RadNumericTextBox ID="NtxtHonorarios" runat="server" CssClass="fuenteTxt" MinValue="0" MaxValue="100" Type="Percent" NumberFormat-DecimalSeparator="." NumberFormat-GroupSeparator="," Width="100%"></telerik:RadNumericTextBox>
                    </div>
                </div>
                <div class="w3-col s12 m4">
                    <div class="w3-container w3-green w3-text-white w3-center">
                        <b>Interes ordinario</b>
                    </div>
                    <div class="w3-container w3-text-black">
                        <label>Porcentaje:</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="NtxtIntOrdinario" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <telerik:RadNumericTextBox ID="NtxtIntOrdinario" runat="server" CssClass="fuenteTxt" MinValue="0" MaxValue="100" Type="Percent" NumberFormat-DecimalSeparator="." NumberFormat-GroupSeparator="," Width="100%"></telerik:RadNumericTextBox>
                    </div>
                </div>
                <div class="w3-col s12 m4">
                    <div class="w3-container w3-green w3-text-white w3-center">
                        <b>Gastos cobranza</b>
                    </div>
                    <div class="w3-container w3-text-black">
                        <label>Porcentaje:</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator35" runat="server" ControlToValidate="NtxtCobranza1" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <telerik:RadNumericTextBox ID="NtxtCobranza1" runat="server" CssClass="fuenteTxt" MinValue="0" MaxValue="100" Type="Percent" NumberFormat-DecimalSeparator="." NumberFormat-GroupSeparator="," Width="100%"></telerik:RadNumericTextBox>
                    </div>
                </div>
            </div>
            <hr />
            <div class="w3-panel w3-teal w3-text-white w3-center">
                <b>Intancia judicial cartera activa</b>
            </div>
            <div class="w3-row-padding">
                <div class="w3-col s12 m4">
                    <div class="w3-container w3-green w3-text-white w3-center">
                        <b>Capital</b>
                    </div>
                    <div class="w3-container w3-text-black">
                        <label>Porcentaje:</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" ControlToValidate="NtxtCapital2" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <telerik:RadNumericTextBox ID="NtxtCapital2" runat="server" CssClass="fuenteTxt" MinValue="0" MaxValue="100" Type="Percent" NumberFormat-DecimalSeparator="." NumberFormat-GroupSeparator="," Width="100%"></telerik:RadNumericTextBox>
                    </div>
                </div>
                <div class="w3-col s12 m4">
                    <div class="w3-container w3-green w3-text-white w3-center">
                        <b>Interes moratorio</b>
                    </div>
                    <div class="w3-container w3-text-black">
                        <label>Porcentaje:</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="NtxtMoratorio2" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <telerik:RadNumericTextBox ID="NtxtMoratorio2" runat="server" CssClass="fuenteTxt" MinValue="0" MaxValue="100" Type="Percent" NumberFormat-DecimalSeparator="." NumberFormat-GroupSeparator="," Width="100%"></telerik:RadNumericTextBox>
                    </div>
                </div>
                <div class="w3-col s12 m4">
                    <div class="w3-container w3-green w3-text-white w3-center">
                        <b>Comisiones por Impago</b>
                    </div>
                    <div class="w3-container w3-text-black">
                        <label>Porcentaje:</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="NtxtComImpago2" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <telerik:RadNumericTextBox ID="NtxtComImpago2" runat="server" CssClass="fuenteTxt" MinValue="0" MaxValue="100" Type="Percent" NumberFormat-DecimalSeparator="." NumberFormat-GroupSeparator="," Width="100%"></telerik:RadNumericTextBox>
                    </div>
                </div>
                <div class="w3-col s12 m4">
                    <div class="w3-container w3-green w3-text-white w3-center">
                        <b>Honorarios</b>
                    </div>
                    <div class="w3-container w3-text-black">
                        <label>Porcentaje:</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="NtxtHonorarios2" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <telerik:RadNumericTextBox ID="NtxtHonorarios2" runat="server" CssClass="fuenteTxt" MinValue="0" MaxValue="100" Type="Percent" NumberFormat-DecimalSeparator="." NumberFormat-GroupSeparator="," Width="100%"></telerik:RadNumericTextBox>
                    </div>
                </div>
                <div class="w3-col s12 m4">
                    <div class="w3-container w3-green w3-text-white w3-center">
                        <b>Interes ordinario</b>
                    </div>
                    <div class="w3-container w3-text-black">
                        <label>Porcentaje:</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="NtxtIntOrdinario2" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <telerik:RadNumericTextBox ID="NtxtIntOrdinario2" runat="server" CssClass="fuenteTxt" MinValue="0" MaxValue="100" Type="Percent" NumberFormat-DecimalSeparator="." NumberFormat-GroupSeparator="," Width="100%"></telerik:RadNumericTextBox>
                    </div>
                </div>
                <div class="w3-col s12 m4">
                    <div class="w3-container w3-green w3-text-white w3-center">
                        <b>Gastos cobranza</b>
                    </div>
                    <div class="w3-container w3-text-black">
                        <label>Porcentaje:</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server" ControlToValidate="NtxtCobranza2" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <telerik:RadNumericTextBox ID="NtxtCobranza2" runat="server" CssClass="fuenteTxt" MinValue="0" MaxValue="100" Type="Percent" NumberFormat-DecimalSeparator="." NumberFormat-GroupSeparator="," Width="100%"></telerik:RadNumericTextBox>
                    </div>
                </div>
            </div>
            <hr />
            <div class="w3-panel w3-teal w3-text-white w3-center">
                <b>Intancia judicial cartera inactiva</b>
            </div>
            <div class="w3-row-padding">
                <div class="w3-col s12 m4">
                    <div class="w3-container w3-green w3-text-white w3-center">
                        <b>Capital</b>
                    </div>
                    <div class="w3-container w3-text-black">
                        <label>Porcentaje:</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ControlToValidate="NtxtCapital3" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <telerik:RadNumericTextBox ID="NtxtCapital3" runat="server" CssClass="fuenteTxt" MinValue="0" MaxValue="100" Type="Percent" NumberFormat-DecimalSeparator="." NumberFormat-GroupSeparator="," Width="100%"></telerik:RadNumericTextBox>
                    </div>
                </div>
                <div class="w3-col s12 m4">
                    <div class="w3-container w3-green w3-text-white w3-center">
                        <b>Interes moratorio</b>
                    </div>
                    <div class="w3-container w3-text-black">
                        <label>Porcentaje:</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="NtxtMoratorio3" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <telerik:RadNumericTextBox ID="NtxtMoratorio3" runat="server" CssClass="fuenteTxt" MinValue="0" MaxValue="100" Type="Percent" NumberFormat-DecimalSeparator="." NumberFormat-GroupSeparator="," Width="100%"></telerik:RadNumericTextBox>
                    </div>
                </div>
                <div class="w3-col s12 m4">
                    <div class="w3-container w3-green w3-text-white w3-center">
                        <b>Comisiones por Impago</b>
                    </div>
                    <div class="w3-container w3-text-black">
                        <label>Porcentaje:</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="NtxtComImpago3" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <telerik:RadNumericTextBox ID="NtxtComImpago3" runat="server" CssClass="fuenteTxt" MinValue="0" MaxValue="100" Type="Percent" NumberFormat-DecimalSeparator="." NumberFormat-GroupSeparator="," Width="100%"></telerik:RadNumericTextBox>
                    </div>
                </div>
                <div class="w3-col s12 m4">
                    <div class="w3-container w3-green w3-text-white w3-center">
                        <b>Honorarios</b>
                    </div>
                    <div class="w3-container w3-text-black">
                        <label>Porcentaje:</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="NtxtHonorarios3" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <telerik:RadNumericTextBox ID="NtxtHonorarios3" runat="server" CssClass="fuenteTxt" MinValue="0" MaxValue="100" Type="Percent" NumberFormat-DecimalSeparator="." NumberFormat-GroupSeparator="," Width="100%"></telerik:RadNumericTextBox>
                    </div>
                </div>
                <div class="w3-col s12 m4">
                    <div class="w3-container w3-green w3-text-white w3-center">
                        <b>Interes ordinario</b>
                    </div>
                    <div class="w3-container w3-text-black">
                        <label>Porcentaje:</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="NtxtIntOrdinario3" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <telerik:RadNumericTextBox ID="NtxtIntOrdinario3" runat="server" CssClass="fuenteTxt" MinValue="0" MaxValue="100" Type="Percent" NumberFormat-DecimalSeparator="." NumberFormat-GroupSeparator="," Width="100%"></telerik:RadNumericTextBox>
                    </div>
                </div>
                <div class="w3-col s12 m4">
                    <div class="w3-container w3-green w3-text-white w3-center">
                        <b>Gastos cobranza</b>
                    </div>
                    <div class="w3-container w3-text-black">
                        <label>Porcentaje:</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator38" runat="server" ControlToValidate="NtxtCobranza3" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <telerik:RadNumericTextBox ID="NtxtCobranza3" runat="server" CssClass="fuenteTxt" MinValue="0" MaxValue="100" Type="Percent" NumberFormat-DecimalSeparator="." NumberFormat-GroupSeparator="," Width="100%"></telerik:RadNumericTextBox>
                    </div>
                </div>
            </div>
            <hr />
            <div class="w3-panel w3-teal w3-text-white w3-center">
                <b>Caso 3</b>
            </div>
            <div class="w3-row-padding">
                <div class="w3-col s12 m4">
                    <div class="w3-container w3-green w3-text-white w3-center">
                        <b>Capital</b>
                    </div>
                    <div class="w3-container w3-text-black">
                        <label>Porcentaje:</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="NtxtCapital4" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <telerik:RadNumericTextBox ID="NtxtCapital4" runat="server" CssClass="fuenteTxt" MinValue="0" MaxValue="100" Type="Percent" NumberFormat-DecimalSeparator="." NumberFormat-GroupSeparator="," Width="100%"></telerik:RadNumericTextBox>
                    </div>
                </div>
                <div class="w3-col s12 m4">
                    <div class="w3-container w3-green w3-text-white w3-center">
                        <b>Interes moratorio</b>
                    </div>
                    <div class="w3-container w3-text-black">
                        <label>Porcentaje:</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="NtxtMoratorio4" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <telerik:RadNumericTextBox ID="NtxtMoratorio4" runat="server" CssClass="fuenteTxt" MinValue="0" MaxValue="100" Type="Percent" NumberFormat-DecimalSeparator="." NumberFormat-GroupSeparator="," Width="100%"></telerik:RadNumericTextBox>
                    </div>
                </div>
                <div class="w3-col s12 m4">
                    <div class="w3-container w3-green w3-text-white w3-center">
                        <b>Comisiones por Impago</b>
                    </div>
                    <div class="w3-container w3-text-black">
                        <label>Porcentaje:</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="NtxtComImpago4" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <telerik:RadNumericTextBox ID="NtxtComImpago4" runat="server" CssClass="fuenteTxt" MinValue="0" MaxValue="100" Type="Percent" NumberFormat-DecimalSeparator="." NumberFormat-GroupSeparator="," Width="100%"></telerik:RadNumericTextBox>
                    </div>
                </div>
                <div class="w3-col s12 m4">
                    <div class="w3-container w3-green w3-text-white w3-center">
                        <b>Honorarios</b>
                    </div>
                    <div class="w3-container w3-text-black">
                        <label>Porcentaje:</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="NtxtHonorarios4" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <telerik:RadNumericTextBox ID="NtxtHonorarios4" runat="server" CssClass="fuenteTxt" MinValue="0" MaxValue="100" Type="Percent" NumberFormat-DecimalSeparator="." NumberFormat-GroupSeparator="," Width="100%"></telerik:RadNumericTextBox>
                    </div>
                </div>
                <div class="w3-col s12 m4">
                    <div class="w3-container w3-green w3-text-white w3-center">
                        <b>Interes ordinario</b>
                    </div>
                    <div class="w3-container w3-text-black">
                        <label>Porcentaje:</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="NtxtIntOrdinario4" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <telerik:RadNumericTextBox ID="NtxtIntOrdinario4" runat="server" CssClass="fuenteTxt" MinValue="0" MaxValue="100" Type="Percent" NumberFormat-DecimalSeparator="." NumberFormat-GroupSeparator="," Width="100%"></telerik:RadNumericTextBox>
                    </div>
                </div>
                <div class="w3-col s12 m4">
                    <div class="w3-container w3-green w3-text-white w3-center">
                        <b>Gastos cobranza</b>
                    </div>
                    <div class="w3-container w3-text-black">
                        <label>Porcentaje:</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ControlToValidate="NtxtCobranza4" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <telerik:RadNumericTextBox ID="NtxtCobranza4" runat="server" CssClass="fuenteTxt" MinValue="0" MaxValue="100" Type="Percent" NumberFormat-DecimalSeparator="." NumberFormat-GroupSeparator="," Width="100%"></telerik:RadNumericTextBox>
                    </div>
                </div>
            </div>
            <hr />
            <div class="w3-panel w3-teal w3-text-white w3-center">
                <b>Caso 4</b>
            </div>
            <div class="w3-row-padding">
                <div class="w3-col s12 m4">
                    <div class="w3-container w3-green w3-text-white w3-center">
                        <b>Capital</b>
                    </div>
                    <div class="w3-container w3-text-black">
                        <label>Porcentaje:</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="NtxtCapital5" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <telerik:RadNumericTextBox ID="NtxtCapital5" runat="server" CssClass="fuenteTxt" MinValue="0" MaxValue="100" Type="Percent" NumberFormat-DecimalSeparator="." NumberFormat-GroupSeparator="," Width="100%"></telerik:RadNumericTextBox>
                    </div>
                </div>
                <div class="w3-col s12 m4">
                    <div class="w3-container w3-green w3-text-white w3-center">
                        <b>Interes moratorio</b>
                    </div>
                    <div class="w3-container w3-text-black">
                        <label>Porcentaje:</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="NtxtMoratorio5" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <telerik:RadNumericTextBox ID="NtxtMoratorio5" runat="server" CssClass="fuenteTxt" MinValue="0" MaxValue="100" Type="Percent" NumberFormat-DecimalSeparator="." NumberFormat-GroupSeparator="," Width="100%"></telerik:RadNumericTextBox>
                    </div>
                </div>
                <div class="w3-col s12 m4">
                    <div class="w3-container w3-green w3-text-white w3-center">
                        <b>Comisiones por Impago</b>
                    </div>
                    <div class="w3-container w3-text-black">
                        <label>Porcentaje:</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="NtxtComImpago5" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <telerik:RadNumericTextBox ID="NtxtComImpago5" runat="server" CssClass="fuenteTxt" MinValue="0" MaxValue="100" Type="Percent" NumberFormat-DecimalSeparator="." NumberFormat-GroupSeparator="," Width="100%"></telerik:RadNumericTextBox>
                    </div>
                </div>
                <div class="w3-col s12 m4">
                    <div class="w3-container w3-green w3-text-white w3-center">
                        <b>Honorarios</b>
                    </div>
                    <div class="w3-container w3-text-black">
                        <label>Porcentaje:</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="NtxtHonorarios5" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <telerik:RadNumericTextBox ID="NtxtHonorarios5" runat="server" CssClass="fuenteTxt" MinValue="0" MaxValue="100" Type="Percent" NumberFormat-DecimalSeparator="." NumberFormat-GroupSeparator="," Width="100%"></telerik:RadNumericTextBox>
                    </div>
                </div>
                <div class="w3-col s12 m4">
                    <div class="w3-container w3-green w3-text-white w3-center">
                        <b>Interes ordinario</b>
                    </div>
                    <div class="w3-container w3-text-black">
                        <label>Porcentaje:</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="NtxtIntOrdinario5" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <telerik:RadNumericTextBox ID="NtxtIntOrdinario5" runat="server" CssClass="fuenteTxt" MinValue="0" MaxValue="100" Type="Percent" NumberFormat-DecimalSeparator="." NumberFormat-GroupSeparator="," Width="100%"></telerik:RadNumericTextBox>
                    </div>
                </div>
                <div class="w3-col s12 m4">
                    <div class="w3-container w3-green w3-text-white w3-center">
                        <b>Gastos cobranza</b>
                    </div>
                    <div class="w3-container w3-text-black">
                        <label>Porcentaje:</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator39" runat="server" ControlToValidate="NtxtCobranza5" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <telerik:RadNumericTextBox ID="NtxtCobranza5" runat="server" CssClass="fuenteTxt" MinValue="0" MaxValue="100" Type="Percent" NumberFormat-DecimalSeparator="." NumberFormat-GroupSeparator="," Width="100%"></telerik:RadNumericTextBox>
                    </div>
                </div>
            </div>
            <hr />
            <div class="w3-panel w3-orange w3-text-white w3-center">
                <b>
                    <telerik:RadLabel CssClass="w3-text-white" runat="server" ID="HonorarioVariable" Text="Porcentaje de honorarios" Visible="false"></telerik:RadLabel>
                </b>
            </div>
            <div class="w3-row-padding" style="display: none">
                <div class="w3-col s12 m4 w3-center w3-margin">
                    <label class="w3-text-black">Tipo de Instancia: </label>
                    <telerik:RadDropDownList ID="RdlInstancia" runat="server" AutoPostBack="true" CausesValidation="false">
                        <Items>
                            <telerik:DropDownListItem Text="Ambas" Value="Ambas" />
                            <telerik:DropDownListItem Text="Única" Value="Única" Selected="true" />
                        </Items>
                    </telerik:RadDropDownList>
                </div>
            </div>
            <div class="w3-row-padding">
                <div class="w3-col s12 m3">
                    <div class="w3-container w3-orange w3-text-white w3-center">
                        <b>Puesta al corriente</b>
                    </div>
                    <div class="w3-container w3-text-black">
                        <label>Porcentaje:</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="NtxtPuestaCorriente" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <telerik:RadNumericTextBox ID="NtxtPuestaCorriente" runat="server" CssClass="fuenteTxt" MinValue="0" MaxValue="100" Type="Percent" NumberFormat-DecimalSeparator="." NumberFormat-GroupSeparator="," Width="100%"></telerik:RadNumericTextBox>
                    </div>
                </div>
                <div class="w3-col s12 m3">
                    <div class="w3-container w3-orange w3-text-white w3-center">
                        <b>Liquidaciones</b>
                    </div>
                    <div class="w3-container w3-text-black">
                        <label>Porcentaje:</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="NtxtLiquidaciones" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <telerik:RadNumericTextBox ID="NtxtLiquidaciones" runat="server" CssClass="fuenteTxt" MinValue="0" MaxValue="100" Type="Percent" NumberFormat-DecimalSeparator="." NumberFormat-GroupSeparator="," Width="100%"></telerik:RadNumericTextBox>
                    </div>
                </div>
                <div class="w3-col s12 m3">
                    <div class="w3-container w3-orange w3-text-white w3-center">
                        <b>Pagos parciales</b>
                    </div>
                    <div class="w3-container w3-text-black">
                        <label>Porcentaje:</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="NtxtPagosParciales" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <telerik:RadNumericTextBox ID="NtxtPagosParciales" runat="server" CssClass="fuenteTxt" MinValue="0" MaxValue="100" Type="Percent" NumberFormat-DecimalSeparator="." NumberFormat-GroupSeparator="," Width="100%"></telerik:RadNumericTextBox>
                    </div>
                </div>
                <div class="w3-col s12 m3">
                    <div class="w3-container w3-orange w3-text-white w3-center">
                        <b>Reestructuras</b>
                    </div>
                    <div class="w3-container w3-text-black">
                        <label>Porcentaje:</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ControlToValidate="NtxtReestructuras" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <telerik:RadNumericTextBox ID="NtxtReestructuras" runat="server" CssClass="fuenteTxt" MinValue="0" MaxValue="100" Type="Percent" NumberFormat-DecimalSeparator="." NumberFormat-GroupSeparator="," Width="100%"></telerik:RadNumericTextBox>
                    </div>
                </div>
            </div>
            <asp:Panel runat="server" ID="PnlAmbas" Visible="false">
                <div class="w3-panel w3-orange w3-text-white w3-center">
                    <b>
                        <telerik:RadLabel CssClass="w3-text-white" runat="server" Text="Porcentaje de honorarios Instancia Extrajudicial"></telerik:RadLabel>
                    </b>
                </div>
                <div class="w3-row-padding">
                    <div class="w3-col s12 m3">
                        <div class="w3-container w3-orange w3-text-white w3-center">
                            <b>Puesta al corriente</b>
                        </div>
                        <div class="w3-container w3-text-black">
                            <label>Porcentaje:</label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ControlToValidate="NtxtPuestaCorriente2" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <telerik:RadNumericTextBox ID="NtxtPuestaCorriente2" runat="server" CssClass="fuenteTxt" MinValue="0" MaxValue="100" Type="Percent" NumberFormat-DecimalSeparator="." NumberFormat-GroupSeparator="," Width="100%"></telerik:RadNumericTextBox>
                        </div>
                    </div>
                    <div class="w3-col s12 m3">
                        <div class="w3-container w3-orange w3-text-white w3-center">
                            <b>Liquidaciones</b>
                        </div>
                        <div class="w3-container w3-text-black">
                            <label>Porcentaje:</label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ControlToValidate="NtxtLiquidaciones2" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <telerik:RadNumericTextBox ID="NtxtLiquidaciones2" runat="server" CssClass="fuenteTxt" MinValue="0" MaxValue="100" Type="Percent" NumberFormat-DecimalSeparator="." NumberFormat-GroupSeparator="," Width="100%"></telerik:RadNumericTextBox>
                        </div>
                    </div>
                    <div class="w3-col s12 m3">
                        <div class="w3-container w3-orange w3-text-white w3-center">
                            <b>Pagos parciales</b>
                        </div>
                        <div class="w3-container w3-text-black">
                            <label>Porcentaje:</label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ControlToValidate="NtxtPagosParciales2" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <telerik:RadNumericTextBox ID="NtxtPagosParciales2" runat="server" CssClass="fuenteTxt" MinValue="0" MaxValue="100" Type="Percent" NumberFormat-DecimalSeparator="." NumberFormat-GroupSeparator="," Width="100%"></telerik:RadNumericTextBox>
                        </div>
                    </div>
                    <div class="w3-col s12 m3">
                        <div class="w3-container w3-orange w3-text-white w3-center">
                            <b>Reestructuras</b>
                        </div>
                        <div class="w3-container w3-text-black">
                            <label>Porcentaje:</label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ControlToValidate="NtxtReestructuras2" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <telerik:RadNumericTextBox ID="NtxtReestructuras2" runat="server" CssClass="fuenteTxt" MinValue="0" MaxValue="100" Type="Percent" NumberFormat-DecimalSeparator="." NumberFormat-GroupSeparator="," Width="100%"></telerik:RadNumericTextBox>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <div class="w3-row w3-margin-bottom ">
                <telerik:RadButton runat="server" ID="btnGuardarCondonacion" Text="Guardar"></telerik:RadButton>
                <telerik:RadButton runat="server" ID="btnCancelarCondonacion" Text="Cancelar" BackColor="Red"></telerik:RadButton>
            </div>
        </telerik:RadAjaxPanel>

        <telerik:RadNotification RenderMode="Lightweight" ID="RadNotification1" runat="server" Height="140px" Animation="Fade" EnableRoundedCorners="true" EnableShadow="true" AutoCloseDelay="3500" Position="Center" OffsetX="-30" OffsetY="-70" ShowCloseButton="true" VisibleOnPageLoad="false" LoadContentOn="EveryShow" KeepOnMouseOver="true">
        </telerik:RadNotification>
    </div>
</asp:Content>

