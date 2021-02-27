<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ConfiguracionPermisos.aspx.vb" Inherits="Permisos_ConfiguracionPermisos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Configuración Permisos</title>
    <link href="../Estilos/bootstrap.min.css" rel="stylesheet" />
    <link href="Imagenes/IcoLogo_Mc.ico" rel="Shortcut icon" />
    <script src="../Scripts/jQuery.min.js"></script>
    <script src="../Scripts/popper.min.js?v=1"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <link href="//fonts.googleapis.com/css?family=Roboto:300,400,500,700|Google+Sans:300,400,500,700" rel="stylesheet" type="text/css" />
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager runat="server"></telerik:RadScriptManager>
        <asp:Panel runat="server" ID="pnlInicial">
            <div class="jumbotron bg-danger">
                <h1 class="display-4">¡AVISO!</h1>
                <p class="lead">ESTAS ENTRANDO A UN MÓDULO ESPECIFICO PARA DESARROLLADORES. SI NO TIENES PERMISO DE ENTRAR O LLEGASTE AQUÍ POR EQUIVOCACIÓN, POR FAVOR REGRESA A EL <a runat="server" href="~/login">INICIO DE SESION</a></p>
                <hr class="my-4" />
                <p>Para continuar, ingresa la clave para acceder a este módulo</p>
                <p class="lead">
                    <telerik:RadTextBox runat="server" ID="txtPassword" TextMode="Password" AutoPostBack="true" Label="Contraseña"></telerik:RadTextBox>
                </p>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlDesarrollador" Visible="false">
            <telerik:RadAjaxPanel runat="server">
                <div class="alert alert-danger fixed-top" role="alert">
                    Eliminar un permiso, apagará todos los permisos de todos los perfiles del modulo correspondiente.
                </div>
                <div class="container-fluid mt-5">
                    <telerik:RadFormDecorator runat="server" DecoratedControls="Fieldset" DecorationZoneID="config" />
                    <div class="text-center" id="config">
                        <fieldset>
                            <legend>Reinicio</legend>
                            <p>Los botones a continuación restaurarán la cadena de bits de los permisos de todos los perfiles. (todo en 1´s y longitud correcta)</p>
                            <telerik:RadCheckBoxList runat="server" ID="cblModulosRestart" Layout="Flow" Columns="6">
                                <Items>
                                    <telerik:ButtonListItem Value="2" Text="Administrador" />
                                    <telerik:ButtonListItem Value="4" Text="Backoffice" />
                                    <telerik:ButtonListItem Value="1" Text="Gestion" />
                                    <telerik:ButtonListItem Value="3" Text="Reportes" />
                                    <telerik:ButtonListItem Value="5" Text="Movil" />
                                    <telerik:ButtonListItem Value="6" Text="Judicial" />
                                </Items>
                            </telerik:RadCheckBoxList>
                            <br />
                            <telerik:RadButton runat="server" ID="btnRestart" Text="Reiniciar Permisos" CssClass="mx-2">
                            </telerik:RadButton>
                        </fieldset>
                        <br class="my-2" />
                        <fieldset>
                            <legend>Configuración</legend>
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-12 col-md-12 col-lg-6 my-2">
                                        <h3>Administrador</h3>
                                        <telerik:RadGrid runat="server" ID="gridAdministrador" AllowPaging="true" PageSize="6" AllowAutomaticDeletes="true" AllowAutomaticInserts="true" AllowAutomaticUpdates="true" AutoGenerateColumns="false" Width="100%" modulo='<%# Modulo.Administrador %>' OnNeedDataSource="grid_NeedDataSource" OnItemCreated="grid_ItemCreated" OnItemCommand="grid_ItemCommand">
                                            <MasterTableView CommandItemDisplay="Top">
                                                <Columns>
                                                    <telerik:GridEditCommandColumn></telerik:GridEditCommandColumn>
                                                    <telerik:GridButtonColumn Text="Delete" CommandName="Delete" />
                                                    <telerik:GridBoundColumn HeaderText="Permiso" DataField="CAT_PE_PERMISO" UniqueName="Permiso"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="Menu" DataField="CAT_PE_MENU" UniqueName="Menu"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="Habilitado" DataField="CAT_PE_enabled" UniqueName="Habilitado"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="Idm" DataField="CAT_PE_idm" Display="false" UniqueName="Idm" DefaultInsertValue="2" InsertVisiblityMode="AlwaysHidden"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="Modulo" DataField="CAT_PE_modulo" Display="false" UniqueName="Modulo" DefaultInsertValue="Administrador" InsertVisiblityMode="AlwaysHidden"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="Orden" DataField="CAT_PE_orden" Display="false" UniqueName="Orden" InsertVisiblityMode="AlwaysHidden"></telerik:GridBoundColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                    <div class="col-12 col-md-12 col-lg-6 my-2">
                                        <h3>BackOffice</h3>
                                        <telerik:RadGrid runat="server" ID="gridBackO" AllowPaging="true" PageSize="6" AllowAutomaticDeletes="true" AllowAutomaticInserts="true" AllowAutomaticUpdates="true" AutoGenerateColumns="false" Width="100%" modulo='<%# Modulo.BackOffice %>' OnNeedDataSource="grid_NeedDataSource" OnItemCreated="grid_ItemCreated" OnItemCommand="grid_ItemCommand">
                                            <MasterTableView CommandItemDisplay="Top">
                                                <Columns>
                                                    <telerik:GridEditCommandColumn></telerik:GridEditCommandColumn>
                                                    <telerik:GridButtonColumn Text="Delete" CommandName="Delete" />
                                                    <telerik:GridBoundColumn HeaderText="Permiso" DataField="CAT_PE_PERMISO" UniqueName="Permiso"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="Menu" DataField="CAT_PE_MENU" UniqueName="Menu"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="Habilitado" DataField="CAT_PE_enabled" UniqueName="Habilitado"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="Idm" DataField="CAT_PE_idm" Display="false" UniqueName="Idm" DefaultInsertValue="2" InsertVisiblityMode="AlwaysHidden"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="Modulo" DataField="CAT_PE_modulo" Display="false" UniqueName="Modulo" DefaultInsertValue="Administrador" InsertVisiblityMode="AlwaysHidden"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="Orden" DataField="CAT_PE_orden" Display="false" UniqueName="Orden" InsertVisiblityMode="AlwaysHidden"></telerik:GridBoundColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                    <div class="col-12 col-md-12 col-lg-6 my-2">
                                        <h3>Gestion</h3>
                                        <telerik:RadGrid runat="server" ID="gridGestion" AllowPaging="true" PageSize="6" AllowAutomaticDeletes="true" AllowAutomaticInserts="true" AllowAutomaticUpdates="true" AutoGenerateColumns="false" Width="100%" modulo='<%# Modulo.Gestion %>' OnNeedDataSource="grid_NeedDataSource" OnItemCreated="grid_ItemCreated" OnItemCommand="grid_ItemCommand">
                                            <MasterTableView CommandItemDisplay="Top">
                                                <Columns>
                                                    <telerik:GridEditCommandColumn></telerik:GridEditCommandColumn>
                                                    <telerik:GridButtonColumn Text="Delete" CommandName="Delete" />
                                                    <telerik:GridBoundColumn HeaderText="Permiso" DataField="CAT_PE_PERMISO" UniqueName="Permiso"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="Menu" DataField="CAT_PE_MENU" UniqueName="Menu"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="Habilitado" DataField="CAT_PE_enabled" UniqueName="Habilitado"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="Idm" DataField="CAT_PE_idm" Display="false" UniqueName="Idm" DefaultInsertValue="2" InsertVisiblityMode="AlwaysHidden"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="Modulo" DataField="CAT_PE_modulo" Display="false" UniqueName="Modulo" DefaultInsertValue="Administrador" InsertVisiblityMode="AlwaysHidden"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="Orden" DataField="CAT_PE_orden" Display="false" UniqueName="Orden" InsertVisiblityMode="AlwaysHidden"></telerik:GridBoundColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                    <div class="col-12 col-md-12 col-lg-6 my-2">
                                        <h3>Reportes</h3>
                                        <telerik:RadGrid runat="server" ID="gridReportes" AllowPaging="true" PageSize="6" AllowAutomaticDeletes="true" AllowAutomaticInserts="true" AllowAutomaticUpdates="true" AutoGenerateColumns="false" Width="100%" modulo='<%# Modulo.Reportes %>' OnNeedDataSource="grid_NeedDataSource" OnItemCreated="grid_ItemCreated" OnItemCommand="grid_ItemCommand">
                                            <MasterTableView CommandItemDisplay="Top">
                                                <Columns>
                                                    <telerik:GridEditCommandColumn></telerik:GridEditCommandColumn>
                                                    <telerik:GridButtonColumn Text="Delete" CommandName="Delete" />
                                                    <telerik:GridBoundColumn HeaderText="Permiso" DataField="CAT_PE_PERMISO" UniqueName="Permiso"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="Menu" DataField="CAT_PE_MENU" UniqueName="Menu"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="Habilitado" DataField="CAT_PE_enabled" UniqueName="Habilitado"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="Idm" DataField="CAT_PE_idm" Display="false" UniqueName="Idm" DefaultInsertValue="2" InsertVisiblityMode="AlwaysHidden"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="Modulo" DataField="CAT_PE_modulo" Display="false" UniqueName="Modulo" DefaultInsertValue="Administrador" InsertVisiblityMode="AlwaysHidden"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="Orden" DataField="CAT_PE_orden" Display="false" UniqueName="Orden" InsertVisiblityMode="AlwaysHidden"></telerik:GridBoundColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                    <div class="col-12 col-md-12 col-lg-6 my-2">
                                        <h3>Movil</h3>
                                        <telerik:RadGrid runat="server" ID="gridMovil" AllowPaging="true" PageSize="6" AllowAutomaticDeletes="true" AllowAutomaticInserts="true" AllowAutomaticUpdates="true" AutoGenerateColumns="false" Width="100%" modulo='<%# Modulo.Movil %>' OnNeedDataSource="grid_NeedDataSource" OnItemCreated="grid_ItemCreated" OnItemCommand="grid_ItemCommand">
                                            <MasterTableView CommandItemDisplay="Top">
                                                <Columns>
                                                    <telerik:GridEditCommandColumn></telerik:GridEditCommandColumn>
                                                    <telerik:GridButtonColumn Text="Delete" CommandName="Delete" />
                                                    <telerik:GridBoundColumn HeaderText="Permiso" DataField="CAT_PE_PERMISO" UniqueName="Permiso"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="Menu" DataField="CAT_PE_MENU" UniqueName="Menu"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="Habilitado" DataField="CAT_PE_enabled" UniqueName="Habilitado"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="Idm" DataField="CAT_PE_idm" Display="false" UniqueName="Idm" DefaultInsertValue="2" InsertVisiblityMode="AlwaysHidden"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="Modulo" DataField="CAT_PE_modulo" Display="false" UniqueName="Modulo" DefaultInsertValue="Administrador" InsertVisiblityMode="AlwaysHidden"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="Orden" DataField="CAT_PE_orden" Display="false" UniqueName="Orden" InsertVisiblityMode="AlwaysHidden"></telerik:GridBoundColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                    <div class="col-12 col-md-12 col-lg-6 my-2">
                                        <h3>Judicial</h3>
                                        <telerik:RadGrid runat="server" ID="gridJudicial" AllowPaging="true" PageSize="6" AllowAutomaticDeletes="true" AllowAutomaticInserts="true" AllowAutomaticUpdates="true" AutoGenerateColumns="false" Width="100%" modulo='<%# Modulo.Judicial %>' OnNeedDataSource="grid_NeedDataSource" OnItemCreated="grid_ItemCreated" OnItemCommand="grid_ItemCommand">
                                            <MasterTableView CommandItemDisplay="Top">
                                                <Columns>
                                                    <telerik:GridEditCommandColumn></telerik:GridEditCommandColumn>
                                                    <telerik:GridButtonColumn Text="Delete" CommandName="Delete" />
                                                    <telerik:GridBoundColumn HeaderText="Permiso" DataField="CAT_PE_PERMISO" UniqueName="Permiso"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="Menu" DataField="CAT_PE_MENU" UniqueName="Menu"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="Habilitado" DataField="CAT_PE_enabled" UniqueName="Habilitado"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="Idm" DataField="CAT_PE_idm" Display="false" UniqueName="Idm" DefaultInsertValue="2" InsertVisiblityMode="AlwaysHidden"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="Modulo" DataField="CAT_PE_modulo" Display="false" UniqueName="Modulo" DefaultInsertValue="Administrador" InsertVisiblityMode="AlwaysHidden"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="Orden" DataField="CAT_PE_orden" Display="false" UniqueName="Orden" InsertVisiblityMode="AlwaysHidden"></telerik:GridBoundColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                </div>
                            </div>

                        </fieldset>
                    </div>
                </div>
                <telerik:RadWindowManager runat="server"></telerik:RadWindowManager>
            </telerik:RadAjaxPanel>
        </asp:Panel>
    </form>
</body>
</html>
