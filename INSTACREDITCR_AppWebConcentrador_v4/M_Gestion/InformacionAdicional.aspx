<%@ page language="VB" autoeventwireup="false" CodeFile="~/M_Gestion/InformacionAdicional.aspx.vb" inherits="InformacionAdicional"  %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mc :: Modulo Gestion</title>
    <script src="JS/iFrameMejorado.js?v=2" type="text/javascript"></script>
    <meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />
    <link rel="stylesheet" href="Estilos/w3.css" />
</head>
<body style="font-size: .7em" class="scroll">
    <form id="form1" runat="server" onmousemove="window.parent.movement();">
        <noscript>
            <div class="w3-modal" style="display: block">
                <div class="w3-modal-content">
                    <div class="w3-container w3-red w3-center w3-padding-24 w3-jumbo">
                        JavaScript deshabilitado
                    </div>
                    <div class="w3-container w3-center w3-xlarge">
                        Javascript está deshabilitado en su navegador web. Por favor, para ver correctamente este sitio, <b><i>habilite javascript</i></b>.<br />
                        <br />
                        Para ver las instrucciones para habilitar javascript en su navegador, haga click <a href="http://www.enable-javascript.com/es/">aquí</a>.
                    </div>
                </div>
            </div>
        </noscript>
        <telerik:RadScriptManager runat="server" ID="RadScriptManager2" />
        <telerik:RadAjaxManagerProxy runat="server" ID="RadAjaxManager2">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="RGTelefono">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="Notificacion" />
                        <telerik:AjaxUpdatedControl ControlID="RGTelefono" LoadingPanelID="RadAjaxLoadingPnlGeneral" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="RGCorreo">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="Notificacion" />
                        <telerik:AjaxUpdatedControl ControlID="RGCorreo" LoadingPanelID="RadAjaxLoadingPnlGeneral" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="RGDirecciones">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="Notificacion" />
                        <telerik:AjaxUpdatedControl ControlID="RGDirecciones" LoadingPanelID="RadAjaxLoadingPnlGeneral" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManagerProxy>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPnlGeneral" runat="server">
        </telerik:RadAjaxLoadingPanel>

        <div class="w3-container w3-blue w3-center">
            <b>Información Adicional</b>
        </div>
        <br />
        <!-- Grid telefonos
        <div class="w3-container w3-blue w3-center w3-margin">
            <b>Teléfonos</b>
        </div -->
        <div class="w3-container w3-margin" style="overflow: auto">
            <telerik:RadGrid ID="RGTelefono" runat="server" AutoGenerateColumns="false" HeaderStyle-HorizontalAlign="Center" PageSize="5" AllowPaging="True">
                <ClientSettings>
                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True"></Selecting>
                </ClientSettings>
                <MasterTableView EditMode="EditForms" DataKeyNames="TELEFONO" CommandItemDisplay="Top">
                    <CommandItemTemplate>
                        <div class="w3-row">
                            <div class="w3-col s2 w3-text-blue w3-medium">
                                <b>Teléfonos</b>
                            </div>
                            <div class="w3-col s10 w3-right" style="height: 100%;">
                                <asp:LinkButton ID="btnEditSelected" runat="server" CommandName="EditSelected" Visible='<%# tmpPermisos("INF_ADICIONAL_TELEFONOS") And (RGTelefono.EditIndexes.Count = 0)%>' Style="text-decoration: none;" CssClass="w3-hover-shadow w3-padding w3-round"><img style="border:0px;vertical-align:middle;height:20px;" alt="Editar" src="Imagenes/editar.png?v=1.2"/>Editar seleccionado</asp:LinkButton>&nbsp;&nbsp;
                        <asp:LinkButton ID="LinkButton2" runat="server" CommandName="InitInsert" Visible='<%# tmpPermisos("INF_ADICIONAL_TELEFONOS") And Not RGTelefono.MasterTableView.IsItemInserted%>' Style="text-decoration: none" CssClass="w3-hover-shadow w3-padding w3-round"><img style="border:0px;vertical-align:middle;height:20px;" alt="actualizar" src="Imagenes/anadir.png?v=1.2"/>Añadir nuevo</asp:LinkButton>&nbsp;&nbsp;
                        <asp:LinkButton ID="LinkButton4" runat="server" CommandName="RebindGrid" Style="text-decoration: none" CssClass="w3-hover-shadow w3-padding w3-round"><img style="border:0px;vertical-align:middle;height:20px;" alt="" src="Imagenes/recargar.png?v=1.2"/>Recargar lista</asp:LinkButton>
                            </div>
                        </div>
                    </CommandItemTemplate>
                    <Columns>
                        <telerik:GridBoundColumn DataField="TELEFONO" HeaderText="Teléfono" SortExpression="TELEFONO" UniqueName="TELEFONO"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="EXTENSION" HeaderText="Extensión" SortExpression="EXTENSION" UniqueName="EXTENSION"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Registro" DataField="REGISTRO" SortExpression="REGISTRO" UniqueName="REGISTRO"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="TIPO" HeaderText="Tipo" SortExpression="TIPO" UniqueName="TIPO"></telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Días" SortExpression="DIAS" UniqueName="DIAS">
                            <ItemTemplate>
                                <telerik:RadLabel runat="server" ID="RadLabel1" CssClass='<%#IIf(Eval("LUN") = "1", "w3-text-blue", "w3-Text-black")%>' Text="L"></telerik:RadLabel>
                                <telerik:RadLabel runat="server" ID="RadLabel2" CssClass='<%#IIf(Eval("MAR") = "1", "w3-text-blue", "w3-Text-black")%>' Text="M"></telerik:RadLabel>
                                <telerik:RadLabel runat="server" ID="RadLabel3" CssClass='<%#IIf(Eval("MIE") = "1", "w3-text-blue", "w3-Text-black")%>' Text="X"></telerik:RadLabel>
                                <telerik:RadLabel runat="server" ID="RadLabel4" CssClass='<%#IIf(Eval("JUE") = "1", "w3-text-blue", "w3-Text-black")%>' Text="J"></telerik:RadLabel>
                                <telerik:RadLabel runat="server" ID="RadLabel5" CssClass='<%#IIf(Eval("VIE") = "1", "w3-text-blue", "w3-Text-black")%>' Text="V"></telerik:RadLabel>
                                <telerik:RadLabel runat="server" ID="RadLabel6" CssClass='<%#IIf(Eval("SAB") = "1", "w3-text-blue", "w3-Text-black")%>' Text="S"></telerik:RadLabel>
                                <telerik:RadLabel runat="server" ID="RadLabel7" CssClass='<%#IIf(Eval("DOM") = "1", "w3-text-blue", "w3-Text-black")%>' Text="D"></telerik:RadLabel>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="HORA1" HeaderText="Hora inicio" SortExpression="HORA1" UniqueName="HORA1"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="HORA2" HeaderText="Hora fin" SortExpression="HORA2" UniqueName="HORA2"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PARENTESCO" HeaderText="Parentesco" SortExpression="PARENTESCO" UniqueName="PARENTESCO"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="NOMBRE" HeaderText="Nombre" SortExpression="NOMBRE" UniqueName="NOMBRE"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CONTACTO" HeaderText="Estatus" SortExpression="CONTACTO" UniqueName="CONTACTO"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="MODIFICACION" HeaderText="Modificado por" SortExpression="MODIFICACION" UniqueName="MODIFICACION"></telerik:GridBoundColumn>
                        
                    </Columns>
                    <EditFormSettings EditFormType="WebUserControl" UserControlName="./grids/InformacionAdicional/Telefonos.ascx">
                    </EditFormSettings>
                </MasterTableView>
            </telerik:RadGrid>
        </div>
        <!-- Grid correos 
        <div class="w3-container w3-blue w3-center w3-margin">
            <b>Correos</b>
        </div>-->
        <hr />
        <div class="w3-container w3-margin" style="overflow: auto">
            <telerik:RadGrid ID="RGCorreo" runat="server" AutoGenerateColumns="false" HeaderStyle-HorizontalAlign="Center" GridLines="None" PageSize="5" AllowPaging="True">
                <MasterTableView EditMode="EditForms" DataKeyNames="CORREO" CommandItemDisplay="Top">
                    <CommandItemTemplate>
                        <div class="w3-row">
                            <div class="w3-col s2 w3-text-blue w3-medium">
                                <b>Correos</b>
                            </div>
                            <div class="w3-col s10 w3-right" style="height: 100%;">
                                <asp:LinkButton ID="btnEditSelected" runat="server" CommandName="EditSelected" Visible='<%# tmpPermisos("INF_ADICIONAL_CORREOS") And (RGCorreo.EditIndexes.Count = 0)%>' Style="text-decoration: none;" CssClass="w3-hover-shadow w3-padding w3-round"><img style="border:0px;vertical-align:middle;height:20px;" alt="Editar" src="Imagenes/editar.png?v=1.2"/>Editar seleccionado</asp:LinkButton>&nbsp;&nbsp;
                        <asp:LinkButton ID="LinkButton2" runat="server" CommandName="InitInsert" Visible='<%# tmpPermisos("INF_ADICIONAL_CORREOS") And Not RGCorreo.MasterTableView.IsItemInserted%>' Style="text-decoration: none" CssClass="w3-hover-shadow w3-padding w3-round"><img style="border:0px;vertical-align:middle;height:20px;" alt="actualizar" src="Imagenes/anadir.png?v=1.2"/>Añadir nuevo</asp:LinkButton>&nbsp;&nbsp;
                        <asp:LinkButton ID="LinkButton4" runat="server" CommandName="RebindGrid" Style="text-decoration: none" CssClass="w3-hover-shadow w3-padding w3-round"><img style="border:0px;vertical-align:middle;height:20px;" alt="" src="Imagenes/recargar.png?v=1.2"/>Recargar lista</asp:LinkButton>
                            </div>
                        </div>
                    </CommandItemTemplate>
                    <Columns>
                        <%--<telerik:GridEditCommandColumn UniqueName="EditCommandColumn" HeaderText="Editar"></telerik:GridEditCommandColumn>--%>
                        <telerik:GridBoundColumn HeaderText="Correo" DataField="CORREO" SortExpression="CORREO" UniqueName="CORREO"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Registro" DataField="Registro" SortExpression="Registro" UniqueName="Registro"></telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Contacto" SortExpression="CONTACTO" UniqueName="CONTACTO">
                            <ItemTemplate>
                                <telerik:RadCheckBox runat="server" Enabled="false" CssClass="w3-chcekbox" Text="Contactar" ID="RadCheckBox2" Checked='<%#quitaNull(Eval("CONTACTO"))%>'></telerik:RadCheckBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="PARENTESCO" HeaderText="Parentesco" SortExpression="PARENTESCO" UniqueName="PARENTESCO"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="NOMBRE" HeaderText="Nombre" SortExpression="NOMBRE" UniqueName="NOMBRE"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="TIPO" HeaderText="Tipo" SortExpression="TIPO" UniqueName="TIPO"></telerik:GridBoundColumn>
                    </Columns>
                    <EditFormSettings EditFormType="WebUserControl" UserControlName="./grids/InformacionAdicional/Correos.ascx">
                    </EditFormSettings>
                </MasterTableView>
                <ClientSettings>
                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True"></Selecting>
                </ClientSettings>
            </telerik:RadGrid>
        </div>
        <!-- Grid direcciones
        <div class="w3-container w3-blue w3-center w3-margin">
            <b>Direcciones</b>
        </div> -->
        <hr />
        <div class="w3-container w3-margin" style="overflow: auto">
            <telerik:RadGrid ID="RGDirecciones" runat="server" AutoGenerateColumns="false" HeaderStyle-HorizontalAlign="Center" GridLines="None" PageSize="5" AllowPaging="True">
                <MasterTableView EditMode="EditForms" CommandItemDisplay="Top" >
                    <CommandItemTemplate>
                        <div class="w3-row">
                            <div class="w3-col s2 w3-text-blue w3-medium">
                                <b>Direcciones</b>
                            </div>
                            <div class="w3-col s10 w3-right" style="height: 100%;">
                                <asp:LinkButton ID="btnEditSelected" runat="server" CommandName="EditSelected" Visible='<%# tmpPermisos("INF_ADICIONAL_DIRECCIONES") And (RGDirecciones.EditIndexes.Count = 0)%>' Style="text-decoration: none;" CssClass="w3-hover-shadow w3-padding w3-round"><img style="border:0px;vertical-align:middle;height:20px;" alt="Editar" src="Imagenes/editar.png?v=1.2"/>Editar seleccionado</asp:LinkButton>&nbsp;&nbsp;
                        <asp:LinkButton ID="LinkButton2" runat="server" CommandName="InitInsert" Visible='<%# tmpPermisos("INF_ADICIONAL_DIRECCIONES") And Not RGDirecciones.MasterTableView.IsItemInserted%>' Style="text-decoration: none" CssClass="w3-hover-shadow w3-padding w3-round"><img style="border:0px;vertical-align:middle;height:20px;" alt="actualizar" src="Imagenes/anadir.png?v=1.2"/>Añadir nuevo</asp:LinkButton>&nbsp;&nbsp;
                        <asp:LinkButton ID="LinkButton4" runat="server" CommandName="RebindGrid" Style="text-decoration: none" CssClass="w3-hover-shadow w3-padding w3-round"><img style="border:0px;vertical-align:middle;height:20px;" alt="" src="Imagenes/recargar.png?v=1.2"/>Recargar lista</asp:LinkButton>
                            </div>
                        </div>
                    </CommandItemTemplate>
                    <Columns>
                        <%--<telerik:GridEditCommandColumn UniqueName="EditCommandColumn" HeaderText="Editar"></telerik:GridEditCommandColumn>--%>
                        <telerik:GridBoundColumn HeaderText="Registro" DataField="Registro" SortExpression="Registro" UniqueName="Registro"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="C.P." DataField="CP" SortExpression="CP" UniqueName="CP"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Ciudad" DataField="CIUDAD" SortExpression="ciudad" UniqueName="ciudad"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Estado" DataField="ESTADO" SortExpression="estado" UniqueName="estado"></telerik:GridBoundColumn>
                        <%--<telerik:GridBoundColumn HeaderText="Municipio" DataField="MUNICIPIO" SortExpression="municipio" UniqueName="municipio"></telerik:GridBoundColumn>--%>
                        <telerik:GridBoundColumn HeaderText="Colonia" DataField="COLONIA" SortExpression="colonia" UniqueName="colonia"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Calle" DataField="CALLE" SortExpression="calle" UniqueName="calle"></telerik:GridBoundColumn>
                        <%--<telerik:GridBoundColumn HeaderText="Num. Ext." DataField="NUMEXT" SortExpression="numext" UniqueName="numext"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Num. Int." DataField="NUMINT" SortExpression="numint" UniqueName="numint"></telerik:GridBoundColumn>--%>
                        <telerik:GridBoundColumn HeaderText="Nombre" SortExpression="NOMBRE" UniqueName="NOMBRE" DataField="NOMBRE"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Parentesco" DataField="PARENTESCO" SortExpression="parentesco" UniqueName="parentesco"></telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Contacto" SortExpression="CONTACTO" UniqueName="CONTACTO">
                            <ItemTemplate>
                                <telerik:RadCheckBox runat="server" Enabled="false" CssClass="w3-chcekbox" Text="Contactar" ID="RadCheckBox2" Checked='<%#quitaNull(Eval("CONTACTO"))%>'></telerik:RadCheckBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Días" SortExpression="DIAS" UniqueName="DIAS">
                            <ItemTemplate>
                                <telerik:RadLabel runat="server" ID="RadLabel1" CssClass='<%#IIf(Eval("LUN") = "1", "w3-text-blue", "w3-Text-balck")%>' Text="L"></telerik:RadLabel>
                                <telerik:RadLabel runat="server" ID="RadLabel2" CssClass='<%#IIf(Eval("MAR") = "1", "w3-text-blue", "w3-Text-balck")%>' Text="M"></telerik:RadLabel>
                                <telerik:RadLabel runat="server" ID="RadLabel3" CssClass='<%#IIf(Eval("MIE") = "1", "w3-text-blue", "w3-Text-balck")%>' Text="X"></telerik:RadLabel>
                                <telerik:RadLabel runat="server" ID="RadLabel4" CssClass='<%#IIf(Eval("JUE") = "1", "w3-text-blue", "w3-Text-balck")%>' Text="J"></telerik:RadLabel>
                                <telerik:RadLabel runat="server" ID="RadLabel5" CssClass='<%#IIf(Eval("VIE") = "1", "w3-text-blue", "w3-Text-balck")%>' Text="V"></telerik:RadLabel>
                                <telerik:RadLabel runat="server" ID="RadLabel6" CssClass='<%#IIf(Eval("SAB") = "1", "w3-text-blue", "w3-Text-balck")%>' Text="S"></telerik:RadLabel>
                                <telerik:RadLabel runat="server" ID="RadLabel7" CssClass='<%#IIf(Eval("DOM") = "1", "w3-text-blue", "w3-Text-balck")%>' Text="D"></telerik:RadLabel>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderText="Horario inicio" DataField="HORARIO1" SortExpression="horario1" UniqueName="horario1"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Horario fin" DataField="HORARIO2" SortExpression="horario2" UniqueName="horario2"></telerik:GridBoundColumn>
                    </Columns>
                    <EditFormSettings EditFormType="WebUserControl" UserControlName="./grids/InformacionAdicional/direcciones.ascx">
                    </EditFormSettings>
                </MasterTableView>
                <ClientSettings>
                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True"></Selecting>
                </ClientSettings>
            </telerik:RadGrid>
        </div>

        <hr />
 <%--       <div class="w3-container w3-margin" style="overflow: auto">
            <telerik:RadGrid ID="RGRelaciones" runat="server" AutoGenerateColumns="true" HeaderStyle-HorizontalAlign="Center" GridLines="None" PageSize="5" AllowPaging="True">
                <MasterTableView CommandItemDisplay="Top" >
                                        <CommandItemTemplate>
                        <div class="w3-row">
                            <div class="w3-col s2 w3-text-blue w3-medium">
                                <b>Relaciones Personales</b>
                            </div>
                        </div>
                    </CommandItemTemplate>
                </MasterTableView>
            </telerik:RadGrid>
        </div>--%>
        <telerik:RadNotification ID="Notificacion" runat="server" Position="Center" Width="330" Height="160" Animation="Fade" EnableRoundedCorners="true" EnableShadow="true" Style="z-index: 100000">
        </telerik:RadNotification>
    </form>
</body>
</html>
