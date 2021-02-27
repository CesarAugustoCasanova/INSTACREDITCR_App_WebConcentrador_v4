<%@ Page Title="" Language="VB" MasterPageFile="MasterPage.master" AutoEventWireup="false" CodeFile="CatalogosJudiciales.aspx.vb" Inherits="CatalogosJudiciales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHMaster" runat="Server">
    <script languaje="javascript" type="text/javascript">

        function confirmCallbackFn(arg) {
            if (arg) {

                __doPostBack('ctl00$CPHMaster$BtnAceptarConfirmacion', '')

            }
        }
    </script>
    <link href="Estilos/ObjAjax.css" rel="stylesheet" />
    <script src="Scripts/scripts.js" type="text/javascript"></script>
    <telerik:RadAjaxLoadingPanel ID="Radpanelcarga" runat="server"></telerik:RadAjaxLoadingPanel>

    <telerik:RadAjaxPanel ID="Pnlgen" runat="server" LoadingPanelID="Radpanelcarga">
        <table class="Table">
            <tr class="Titulos">
                <td>Catálogos Judiciales
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="PnlNuevo" runat="server" Visible="true">
                        <table class="Table">
                            <tr>

                                <td>
                                    <telerik:RadLabel runat="server" ID="lblTipo" Text="Catalogo: "></telerik:RadLabel>
                                    <telerik:RadDropDownList ID="DdlTipo" runat="server" AutoPostBack="true" DefaultMessage="Seleccione">
                                        <Items>
                                            <telerik:DropDownListItem Value="CAT_ETAPAS_JUDICIALES" Text="Etapas Judiciales"></telerik:DropDownListItem>
                                            <telerik:DropDownListItem Value="CAT_PROMOCIONES_JUDICIALES" Text="Promociones"></telerik:DropDownListItem>
                                            <telerik:DropDownListItem Value="CAT_RESULTADOS_JUDICIALES" Text="Resultados Judiciales"></telerik:DropDownListItem>
                                            <telerik:DropDownListItem Value="CAT_TIPOS_JUICIOS" Text="Tipos de Juicios"></telerik:DropDownListItem>
                                            <telerik:DropDownListItem Value="CAT_DOCUMENTOS_BASE" Text="Documentos Base"></telerik:DropDownListItem>
                                            <telerik:DropDownListItem Value="CAT_DILIGENCIAS" Text="Diligencias"></telerik:DropDownListItem>
                                            <telerik:DropDownListItem Value="CAT_JUZGADOS" Text="Juzgados"></telerik:DropDownListItem>
                                            <telerik:DropDownListItem Value="CAT_CIERRE_JUICIOS" Text="Cierre Juicios"></telerik:DropDownListItem>
                                            <telerik:DropDownListItem Value="CAT_TRAMITES_JUDICIALES" Text="Tramites Judiciales"></telerik:DropDownListItem>
                                           <%-- <telerik:DropDownListItem Value="CAT_CASTIGOS" Text="Castigos"></telerik:DropDownListItem>
                                            <telerik:DropDownListItem Value="CAT_MOTIVOS_CASTIGO" Text="Motivos Castigo"></telerik:DropDownListItem>--%>
                                            <telerik:DropDownListItem Value="CAT_ETIQUETAS_JUDICIALES" Text="Etiquetas Judiciales"></telerik:DropDownListItem>
                                            <telerik:DropDownListItem Value="CAT_ACUERDOS_JUDICIALES" Text="Acuerdos"></telerik:DropDownListItem>
                                            <telerik:DropDownListItem Value="CAT_DIAS_INHABILES" Text="Dias Inhabiles"></telerik:DropDownListItem>
                                        </Items>
                                    </telerik:RadDropDownList>
                                    <asp:HiddenField ID="VALORID" runat="server" />
                                </td>
                            </tr>
                            <tr align="center">
                                <td>
                                    <%--<telerik:RadGrid ID="RGVCatalogosJudiciales" runat="server"  RenderMode="Lightweight" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="True"
                                        ClientSettings-EnableRowHoverStyle="true" OnNeedDataSource="RGVCatalogosJudiciales_NeedDataSource">

                                        <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                        <ClientSettings EnableRowHoverStyle="True">
                                        </ClientSettings>
                                        <MasterTableView CommandItemDisplay="Top" DataKeyNames="ID" EditFormSettings-PopUpSettings-KeepInScreenBounds="true" Width="100%">
                                            <CommandItemSettings AddNewRecordText="Agregar registro" CancelChangesText="Cancelar" RefreshText="Actualizar" SaveChangesText="Guardar" />
                                            <Columns>
                                                <telerik:GridEditCommandColumn ItemStyle-Width="5px" UniqueName="Expandible">
                                                    <ItemStyle Width="5px" />
                                                </telerik:GridEditCommandColumn>
                                                <telerik:GridBoundColumn DataField="ID" HeaderText="ID" UniqueName="ID">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NOMBRE" HeaderText="NOMBRE" UniqueName="NOMBRE">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="COMODIN" HeaderText="COMODIN" UniqueName="COMODIN" Visible="false">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                            <EditFormSettings EditFormType="WebUserControl" UserControlName="CatJudiciales.ascx">
                                                <EditColumn UniqueName="EditCommandColumn1">
                                                </EditColumn>
                                                <PopUpSettings KeepInScreenBounds="True" />
                                            </EditFormSettings>
                                        </MasterTableView>
                                        <FilterMenu RenderMode="Lightweight">
                                        </FilterMenu>
                                        <HeaderContextMenu RenderMode="Lightweight">
                                        </HeaderContextMenu>
                                    </telerik:RadGrid>--%>

                                </td>
                            </tr>
                            <tr align="center">
                                <td>
                                    <%--<telerik:RadAjaxPanel ID="Panelejemplo" runat="server">
                                        <telerik:RadGrid ID="RGVPerfiles" runat="server"  RenderMode="Lightweight" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true"
                                            ClientSettings-EnableRowHoverStyle="true" OnNeedDataSource="RGVPerfiles_NeedDataSource" Culture="es-MX">

                                            <MasterTableView Width="100%" CommandItemDisplay="Top" EditFormSettings-PopUpSettings-KeepInScreenBounds="true" DataKeyNames="Id">

                                                <Columns>

                                                    <telerik:GridEditCommandColumn ItemStyle-Width="5px" UniqueName="Expandible">
                                                    </telerik:GridEditCommandColumn>
                                                    <telerik:GridBoundColumn UniqueName="Id" HeaderText="Id" DataField="Id">
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn UniqueName="Descripcion" HeaderText="Descripcion" DataField="Descripcion">
                                                    </telerik:GridBoundColumn>


                                                </Columns>
                                                <EditFormSettings UserControlName="Sistema.ascx" EditFormType="WebUserControl">
                                                    <EditColumn UniqueName="EditCommandColumn1">
                                                    </EditColumn>
                                                </EditFormSettings>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                     </telerik:RadAjaxPanel>--%>

                                    <telerik:RadGrid ID="RGVEtapas" runat="server" RenderMode="Lightweight" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true"
                                        ClientSettings-EnableRowHoverStyle="true" OnNeedDataSource="RGVEtapas_NeedDataSource" Culture="es-MX">
                                        <MasterTableView Width="100%" CommandItemDisplay="Top" EditFormSettings-PopUpSettings-KeepInScreenBounds="true" DataKeyNames="Id">
                                            <CommandItemSettings AddNewRecordText="Agregar registro" CancelChangesText="Cancelar" RefreshText="Actualizar" SaveChangesText="Guardar" />
                                            <Columns>
                                                <telerik:GridEditCommandColumn ItemStyle-Width="5px" UniqueName="Expandible">
                                                </telerik:GridEditCommandColumn>
                                                <telerik:GridBoundColumn UniqueName="Id" HeaderText="Id" DataField="Id">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="NombreEtapa" HeaderText="Nombre Etapa" DataField="NombreEtapa">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="TipoJuicio" HeaderText="Tipo Juicio" DataField="TipoJuicio">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="Descripcion" HeaderText="Descripcion" DataField="Descripcion">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="Importe" HeaderText="Importe" DataField="Importe">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                            <EditFormSettings UserControlName="Etapas.ascx" EditFormType="WebUserControl">
                                                <EditColumn UniqueName="EditCommandColumn1">
                                                </EditColumn>
                                            </EditFormSettings>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                    <telerik:RadGrid ID="gridTipoJuicio" runat="server" RenderMode="Lightweight" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true"
                                        ClientSettings-EnableRowHoverStyle="true" OnNeedDataSource="gridTipoJuicio_NeedDataSource" Culture="es-MX">
                                        <MasterTableView Width="100%" CommandItemDisplay="Top" EditFormSettings-PopUpSettings-KeepInScreenBounds="true" DataKeyNames="Id">
                                            <CommandItemSettings AddNewRecordText="Agregar registro" CancelChangesText="Cancelar" RefreshText="Actualizar" SaveChangesText="Guardar" />
                                            <Columns>
                                                <telerik:GridEditCommandColumn ItemStyle-Width="5px" UniqueName="Expandible">
                                                </telerik:GridEditCommandColumn>
                                                <telerik:GridBoundColumn UniqueName="Id" HeaderText="Id" DataField="Id">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="NombreTipo" HeaderText="Nombre Tipo" DataField="NombreTipo">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="Descripcion" HeaderText="Descripcion" DataField="Descripcion">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="TiposDocumentos" HeaderText="Tipos Documentos" DataField="TiposDocumentos">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="Caducidad" HeaderText="Caducidad" DataField="Caducidad">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="TipoCaducidad" HeaderText="Tipo Dias Caducidad" DataField="TipoCaducidad">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="DiasPrescripcion" HeaderText="Dias Prescripcion" DataField="DiasPrescripcion">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="TipoPrescripcion" HeaderText="Tipo Prescripcion" DataField="TipoPrescripcion">
                                                </telerik:GridBoundColumn>
                                                 <telerik:GridTemplateColumn HeaderText="Activar/Desactivar" AllowFiltering="false">
                                                    <ItemTemplate>
                                                        <telerik:RadButton runat="server" CommandName='<%#Eval("Comando")%>' Text='<%#Eval("Texto")%>'>
                                                        </telerik:RadButton>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                            <EditFormSettings UserControlName="gridTipoJuicio.ascx" EditFormType="WebUserControl">
                                                <EditColumn UniqueName="EditCommandColumn1">
                                                </EditColumn>
                                            </EditFormSettings>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                    <%-- <telerik:RadGrid ID="RGVTipoJuicio" runat="server"  RenderMode="Lightweight" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true" 
                                            ClientSettings-EnableRowHoverStyle="true" OnNeedDataSource="RGVTipoJuicio_NeedDataSource" Culture="es-MX">
                                            <MasterTableView Width="100%" CommandItemDisplay="Top" EditFormSettings-PopUpSettings-KeepInScreenBounds="true" DataKeyNames="Id">
                                                <CommandItemSettings AddNewRecordText="Agregar registro" CancelChangesText="Cancelar" RefreshText="Actualizar" SaveChangesText="Guardar" />
                                                <Columns>
                                                    <telerik:GridEditCommandColumn ItemStyle-Width="5px" UniqueName="Expandible">
                                                    </telerik:GridEditCommandColumn>
                                                    <telerik:GridBoundColumn UniqueName="Id" HeaderText="Id" DataField="Id">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn UniqueName="NombreTipo" HeaderText="Nombre Tipo" DataField="NombreTipo">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn UniqueName="Descripcion" HeaderText="Descripcion" DataField="Descripcion">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn UniqueName="TiposDocumentos" HeaderText="Tipos Documentos" DataField="TiposDocumentos">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn UniqueName="Caducidad" HeaderText="Caducidad" DataField="Caducidad">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn UniqueName="TipoCaducidad" HeaderText="Tipo Dias Caducidad" DataField="TipoCaducidad">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn UniqueName="DiasPrescripcion" HeaderText="Dias Prescripcion" DataField="DiasPrescripcion">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn UniqueName="TipoPrescripcion" HeaderText="Tipo Prescripcion" DataField="TipoPrescripcion">
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                                <EditFormSettings UserControlName="TiposJuicio.ascx" EditFormType="WebUserControl">
                                                    <EditColumn UniqueName="EditCommandColumn1">
                                                    </EditColumn>
                                                </EditFormSettings>
                                            </MasterTableView>
                                        </telerik:RadGrid>--%>
                                    <telerik:RadGrid ID="RGVDocumentosBase" runat="server" RenderMode="Lightweight" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true"
                                        ClientSettings-EnableRowHoverStyle="true" OnNeedDataSource="RGVDocumentosBase_NeedDataSource" Culture="es-MX">
                                        <MasterTableView Width="100%" CommandItemDisplay="Top" EditFormSettings-PopUpSettings-KeepInScreenBounds="true" DataKeyNames="Id">
                                            <CommandItemSettings AddNewRecordText="Agregar registro" CancelChangesText="Cancelar" RefreshText="Actualizar" SaveChangesText="Guardar" />
                                            <Columns>
                                                <telerik:GridEditCommandColumn ItemStyle-Width="5px" UniqueName="Expandible">
                                                </telerik:GridEditCommandColumn>
                                                <telerik:GridBoundColumn UniqueName="Id" HeaderText="Id" DataField="Id">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="NombreDocumento" HeaderText="Nombre Documento" DataField="NombreDocumento">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="Descripcion" HeaderText="Descripcion" DataField="Descripcion">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="FechaEvento" HeaderText="Fecha Evento Inicio" DataField="FechaEvento">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="TiempoDias" HeaderText="Tiempo en Dias" DataField="TiempoDias">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="TipoDias" HeaderText="Tipo Dias" DataField="TipoDias">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                            <EditFormSettings UserControlName="DocumentosBase.ascx" EditFormType="WebUserControl">
                                                <EditColumn UniqueName="EditCommandColumn1">
                                                </EditColumn>
                                            </EditFormSettings>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                    <telerik:RadGrid ID="RGVDiligencias" runat="server" RenderMode="Lightweight" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true"
                                        ClientSettings-EnableRowHoverStyle="true" OnNeedDataSource="RGVDiligencias_NeedDataSource" Culture="es-MX">
                                        <MasterTableView Width="100%" CommandItemDisplay="Top" EditFormSettings-PopUpSettings-KeepInScreenBounds="true" DataKeyNames="ID">
                                            <CommandItemSettings AddNewRecordText="Agregar registro" CancelChangesText="Cancelar" RefreshText="Actualizar" SaveChangesText="Guardar" />
                                            <Columns>
                                                <telerik:GridEditCommandColumn ItemStyle-Width="5px" UniqueName="Expandible">
                                                </telerik:GridEditCommandColumn>
                                                <telerik:GridBoundColumn UniqueName="ID" HeaderText="ID" DataField="ID">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="DILIGENCIA" HeaderText="Tipo de Diligencia" DataField="DILIGENCIA">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="RESULTADO" HeaderText="Tipo de Resultado" DataField="RESULTADO">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="SUBRESULTADO" HeaderText="Sub Resultado" DataField="SUBRESULTADO">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="MPARTICIPANTE" HeaderText="Marcar Participante" DataField="MPARTICIPANTE">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="REMUEBLE" HeaderText="Registro Embargo Mueble" DataField="REMUEBLE">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="REINMUEBLE" HeaderText="Registro Embargo Inmueble" DataField="REINMUEBLE">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="RCDEPOSITARIO" HeaderText="Registro Cambio de Depositario" DataField="RCDEPOSITARIO">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="RIPROCESAL" HeaderText="Registro Impulso Procesal" DataField="RIPROCESAL">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="PROMOCION" HeaderText="Nombre de la Promoción" DataField="PROMOCION">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="NOTIFCORREO" HeaderText="Notificación a Supervisor por Correo" DataField="NOTIFCORREO">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                            <EditFormSettings UserControlName="Diligencias.ascx" EditFormType="WebUserControl">
                                                <EditColumn UniqueName="EditCommandColumn1">
                                                </EditColumn>
                                            </EditFormSettings>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                    <telerik:RadGrid ID="RGVJuzgados" runat="server" RenderMode="Lightweight" AllowPaging="True" Visible="false" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true"
                                        ClientSettings-EnableRowHoverStyle="true" OnNeedDataSource="RGVJuzgados_NeedDataSource" Culture="es-MX">
                                        <MasterTableView Width="100%" CommandItemDisplay="Top" EditFormSettings-PopUpSettings-KeepInScreenBounds="true" DataKeyNames="Id" AllowFilteringByColumn="true">
                                            <CommandItemSettings AddNewRecordText="Agregar registro" CancelChangesText="Cancelar" RefreshText="Actualizar" SaveChangesText="Guardar" />
                                            <Columns>
                                                <telerik:GridEditCommandColumn ItemStyle-Width="5px" UniqueName="Expandible">
                                                </telerik:GridEditCommandColumn>
                                                <telerik:GridBoundColumn UniqueName="Id" HeaderText="Id" DataField="Id">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="NombreJuzgado" HeaderText="Nombre juzgado" DataField="NombreJuzgado">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="Estado" HeaderText="Estado" DataField="Estado">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="Municipio" HeaderText="Municipio" DataField="Municipio">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="Estatus" HeaderText="Estatus" DataField="Estatus">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                            <EditFormSettings UserControlName="Juzgados.ascx" EditFormType="WebUserControl">
                                                <EditColumn UniqueName="EditCommandColumn1">
                                                </EditColumn>
                                            </EditFormSettings>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                    <telerik:RadGrid ID="RGVDiasInhabiles" runat="server" RenderMode="Lightweight" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true"
                                        ClientSettings-EnableRowHoverStyle="true" OnNeedDataSource="RGVDiasInhabiles_NeedDataSource" Culture="es-MX">
                                        <MasterTableView Width="100%" CommandItemDisplay="Top" EditFormSettings-PopUpSettings-KeepInScreenBounds="true" DataKeyNames="NOMBRE">
                                            <CommandItemSettings AddNewRecordText="Agregar registro" CancelChangesText="Cancelar" RefreshText="Actualizar" SaveChangesText="Guardar" />
                                            <Columns>
                                                <telerik:GridEditCommandColumn ItemStyle-Width="5px" UniqueName="Expandible">
                                                </telerik:GridEditCommandColumn>
                                                <telerik:GridBoundColumn UniqueName="NombrePeriodo" HeaderText="Nombre Periodo" DataField="NOMBRE">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="FechaInicio" HeaderText="Fecha Inicio" DataField="INICIO">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="FechaFin" HeaderText="Nombre Fin" DataField="FIN">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="Estado" HeaderText="Estado" DataField="ESTADO">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                            <EditFormSettings UserControlName="DiasInhabiles.ascx" EditFormType="WebUserControl">
                                                <EditColumn UniqueName="EditCommandColumn1">
                                                </EditColumn>
                                            </EditFormSettings>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                    <telerik:RadGrid ID="RGVCierreJuicios" runat="server" RenderMode="Lightweight" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true"
                                        ClientSettings-EnableRowHoverStyle="true" OnNeedDataSource="RGVCierreJuicios_NeedDataSource" Culture="es-MX">
                                        <MasterTableView Width="100%" CommandItemDisplay="Top" EditFormSettings-PopUpSettings-KeepInScreenBounds="true" DataKeyNames="Id">
                                            <CommandItemSettings AddNewRecordText="Agregar registro" CancelChangesText="Cancelar" RefreshText="Actualizar" SaveChangesText="Guardar" />
                                            <Columns>
                                                <telerik:GridEditCommandColumn ItemStyle-Width="5px" UniqueName="Expandible">
                                                </telerik:GridEditCommandColumn>
                                                <telerik:GridBoundColumn UniqueName="Id" HeaderText="Id" DataField="Id">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="NombreCierre" HeaderText="Nombre Cierre" DataField="NombreCierre">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="ValidaPromocion" HeaderText="Valida Promocion" DataField="ValidaPromocion">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="NombrePromocion" HeaderText="Nombre Promocion" DataField="NombrePromocion">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="ValidaMora" HeaderText="Valida Dias Mora" DataField="ValidaMora">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="ValidaSuperior" HeaderText="Validacion por superior" DataField="superior">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                            <EditFormSettings UserControlName="CierreJuicios.ascx" EditFormType="WebUserControl">
                                                <EditColumn UniqueName="EditCommandColumn1">
                                                </EditColumn>
                                            </EditFormSettings>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                    <telerik:RadGrid ID="RGVTramitesJudiciales" runat="server" RenderMode="Lightweight" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true"
                                        ClientSettings-EnableRowHoverStyle="true" OnNeedDataSource="RGVTramitesJudiciales_NeedDataSource" Culture="es-MX">
                                        <MasterTableView Width="100%" CommandItemDisplay="Top" EditFormSettings-PopUpSettings-KeepInScreenBounds="true" DataKeyNames="Id">
                                            <CommandItemSettings AddNewRecordText="Agregar registro" CancelChangesText="Cancelar" RefreshText="Actualizar" SaveChangesText="Guardar" />
                                            <Columns>
                                                <telerik:GridEditCommandColumn ItemStyle-Width="5px" UniqueName="Expandible">
                                                </telerik:GridEditCommandColumn>
                                                <telerik:GridBoundColumn UniqueName="Id" HeaderText="Id" DataField="Id">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="Tipo" HeaderText="Tipo" DataField="Tipo">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="TipoInscripcion" HeaderText="Tipo Inscripcion" DataField="TipoInscripcion">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="BienEmbargado" HeaderText="Bien Embargado" DataField="BienEmbargado">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="FolioInmobiliario" HeaderText="Folio Inmobiliario" DataField="FolioInmobiliario">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="FechaEvento" HeaderText="Fecha Evento" DataField="FechaEvento">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="HoraEvento" HeaderText="Hora Evento" DataField="HoraEvento">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="Posicion" HeaderText="Posicion" DataField="Posicion">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="Observaciones" HeaderText="Observaciones" DataField="Observaciones">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="FechaAvaluo" HeaderText="Fecha Avaluo" DataField="FechaAvaluo">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="ValorComercial" HeaderText="Valor Comercial" DataField="ValorComercial">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="Garantia" HeaderText="Garantia" DataField="Garantia">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="OrigenGarantia" HeaderText="Origen Garantia" DataField="OrigenGarantia">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                            <EditFormSettings UserControlName="TramitesJudiciales.ascx" EditFormType="WebUserControl">
                                                <EditColumn UniqueName="EditCommandColumn1">
                                                </EditColumn>
                                            </EditFormSettings>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                    <telerik:RadGrid ID="RGVCastigo" runat="server" RenderMode="Lightweight" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true" PageSize="15"
                                        ClientSettings-EnableRowHoverStyle="true" OnNeedDataSource="RGVCastigo_NeedDataSource" Culture="es-MX">
                                        <MasterTableView Width="100%" CommandItemDisplay="Top" EditFormSettings-PopUpSettings-KeepInScreenBounds="true" DataKeyNames="Id">
                                            <CommandItemSettings AddNewRecordText="Agregar registro" CancelChangesText="Cancelar" RefreshText="Actualizar" SaveChangesText="Guardar" />
                                            <Columns>
                                                <telerik:GridEditCommandColumn ItemStyle-Width="5px" UniqueName="Expandible">
                                                </telerik:GridEditCommandColumn>
                                                <telerik:GridBoundColumn UniqueName="Id" HeaderText="Id" DataField="Id">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="Instancia" HeaderText="Instancia" DataField="Instancia">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="Usuario" HeaderText="Usuario" DataField="Usuario">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="Orden" HeaderText="Orden" DataField="Orden">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="Estado" HeaderText="Estado" DataField="Estado">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="Correos" HeaderText="Correos" DataField="Correos">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Cancelar">
                                                    <ItemTemplate>
                                                        <telerik:RadButton runat="server" CommandName="Cancelar" Text="Cancelar" Enabled='<%# IIf(Eval("cancela") <> 1, True, False) %>'>
                                                        </telerik:RadButton>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="ReActivar">
                                                    <ItemTemplate>
                                                        <telerik:RadButton runat="server" CommandName="Activar" Text="Activar" Enabled='<%# IIf(Eval("cancela") <> 0, True, False) %>'>
                                                        </telerik:RadButton>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                            <EditFormSettings UserControlName="Castigo.ascx" EditFormType="WebUserControl">
                                                <EditColumn UniqueName="EditCommandColumn1">
                                                </EditColumn>
                                            </EditFormSettings>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                    <telerik:RadGrid ID="RGVMotivos" runat="server" RenderMode="Lightweight" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true"
                                        ClientSettings-EnableRowHoverStyle="true" OnNeedDataSource="RGVMotivos_NeedDataSource" Culture="es-MX">
                                        <MasterTableView Width="100%" CommandItemDisplay="Top" EditFormSettings-PopUpSettings-KeepInScreenBounds="true" DataKeyNames="Id">
                                            <CommandItemSettings AddNewRecordText="Agregar registro" CancelChangesText="Cancelar" RefreshText="Actualizar" SaveChangesText="Guardar" />
                                            <Columns>
                                                <telerik:GridEditCommandColumn ItemStyle-Width="5px" UniqueName="Expandible">
                                                </telerik:GridEditCommandColumn>
                                                <telerik:GridBoundColumn UniqueName="Id" HeaderText="Id" DataField="Id">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="Tipo" HeaderText="Tipo" DataField="Tipo">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="Estatus" HeaderText="Estatus" DataField="Estatus">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="Descripcion" HeaderText="Descripcion" DataField="Descripcion">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                            <EditFormSettings UserControlName="Motivos.ascx" EditFormType="WebUserControl">
                                                <EditColumn UniqueName="EditCommandColumn1">
                                                </EditColumn>
                                            </EditFormSettings>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                    <telerik:RadGrid ID="RGVEtiquetas" runat="server" RenderMode="Lightweight" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="True"
                                        ClientSettings-EnableRowHoverStyle="true" OnNeedDataSource="RGVEtiquetas_NeedDataSource" Culture="es-MX">
                                        <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                        <ClientSettings EnableRowHoverStyle="True">
                                        </ClientSettings>
                                        <MasterTableView CommandItemDisplay="Top" DataKeyNames="Id" EditFormSettings-PopUpSettings-KeepInScreenBounds="true" Width="100%">
                                            <CommandItemSettings AddNewRecordText="Agregar registro" CancelChangesText="Cancelar" RefreshText="Actualizar" SaveChangesText="Guardar" ShowAddNewRecordButton="False" />
                                            <Columns>
                                                <telerik:GridEditCommandColumn ItemStyle-Width="5px" UniqueName="Expandible">
                                                    <ItemStyle Width="5px" />
                                                </telerik:GridEditCommandColumn>
                                                <telerik:GridBoundColumn DataField="Id" HeaderText="Id" UniqueName="Id">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Ancho" HeaderText="Ancho" UniqueName="Ancho" DataType="System.Int32" DataFormatString="{0:##,#0.000}">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Alto" HeaderText="Alto" UniqueName="Alto" DataType="System.Int32">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TipoLetra" HeaderText="Tipo Letra" UniqueName="TipoLetra">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Tamano1" HeaderText="Tamaño1" UniqueName="Tamano1" DataType="System.Int32">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Tamano2" HeaderText="Tamaño2" UniqueName="Tamano2" DataType="System.Int32">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Tamano3" HeaderText="Tamaño3" UniqueName="Tamano3" DataType="System.Int32">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                            <EditFormSettings EditFormType="WebUserControl" UserControlName="EtiquetasJudiciales.ascx">
                                                <EditColumn UniqueName="EditCommandColumn1">
                                                </EditColumn>
                                                <PopUpSettings KeepInScreenBounds="True" />
                                            </EditFormSettings>
                                        </MasterTableView>
                                        <FilterMenu RenderMode="Lightweight">
                                        </FilterMenu>
                                        <HeaderContextMenu RenderMode="Lightweight">
                                        </HeaderContextMenu>
                                    </telerik:RadGrid>
                                    <telerik:RadGrid ID="RGVPromociones" runat="server" RenderMode="Lightweight" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true"
                                        ClientSettings-EnableRowHoverStyle="true" OnNeedDataSource="RGVPromociones_NeedDataSource" Culture="es-MX" PageSize="20">
                                        <MasterTableView Width="100%" CommandItemDisplay="Top" EditFormSettings-PopUpSettings-KeepInScreenBounds="true" DataKeyNames="Id">
                                            <CommandItemSettings AddNewRecordText="Agregar registro" CancelChangesText="Cancelar" RefreshText="Actualizar" SaveChangesText="Guardar" />
                                            <Columns>
                                                <telerik:GridEditCommandColumn ItemStyle-Width="5px" UniqueName="Expandible">
                                                </telerik:GridEditCommandColumn>
                                                <telerik:GridBoundColumn UniqueName="Id" HeaderText="Id" DataField="Id">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="Comodin" HeaderText="Comodin" DataField="Comodin">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="Nombre" HeaderText="Nombre" DataField="Nombre">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="CambiaEtapa" HeaderText="Impulsa Etapa" DataField="CambiaEtapa">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="DiasRespuestas" HeaderText="Dias Respuestas" DataField="DiasRespuestas">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="TipoDias" HeaderText="Tipo Dias" DataField="TipoDias">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Activar/Desactivar" AllowFiltering="false">
                                                    <ItemTemplate>
                                                        <telerik:RadButton runat="server" CommandName='<%#Eval("Comando")%>' Text='<%#Eval("Texto")%>'>
                                                        </telerik:RadButton>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                            <EditFormSettings UserControlName="Promociones.ascx" EditFormType="WebUserControl">
                                                <EditColumn UniqueName="EditCommandColumn1">
                                                </EditColumn>
                                            </EditFormSettings>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                    <telerik:RadGrid ID="RGVResultados" runat="server" RenderMode="Lightweight" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true"
                                        ClientSettings-EnableRowHoverStyle="true" OnNeedDataSource="RGVResultados_NeedDataSource" Culture="es-MX">
                                        <MasterTableView Width="100%" CommandItemDisplay="Top" EditFormSettings-PopUpSettings-KeepInScreenBounds="true" DataKeyNames="Id">
                                            <CommandItemSettings AddNewRecordText="Agregar registro" CancelChangesText="Cancelar" RefreshText="Actualizar" SaveChangesText="Guardar" />
                                            <Columns>
                                                <telerik:GridEditCommandColumn ItemStyle-Width="5px" UniqueName="Expandible">
                                                </telerik:GridEditCommandColumn>
                                                <telerik:GridBoundColumn UniqueName="Id" HeaderText="Id" DataField="Id">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="Nombre" HeaderText="Nombre" DataField="Nombre">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="CambiaEtapa" HeaderText="Cambia Etapa" DataField="CambiaEtapa">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="DisminucionJuicio" HeaderText="Disminucion Contador Juicio" DataField="DisminucionJuicio">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Activar/Desactivar" AllowFiltering="false">
                                                    <ItemTemplate>
                                                        <telerik:RadButton runat="server" CommandName='<%#Eval("Comando")%>' Text='<%#Eval("Texto")%>'>
                                                        </telerik:RadButton>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                            <EditFormSettings UserControlName="Resultados.ascx" EditFormType="WebUserControl">
                                                <EditColumn UniqueName="EditCommandColumn1">
                                                </EditColumn>
                                            </EditFormSettings>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                    <telerik:RadGrid ID="RGVAcuerdos" runat="server" RenderMode="Lightweight" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true"
                                        ClientSettings-EnableRowHoverStyle="true" OnNeedDataSource="RGVAcuerdos_NeedDataSource" Culture="es-MX">
                                        <MasterTableView Width="100%" CommandItemDisplay="Top" EditFormSettings-PopUpSettings-KeepInScreenBounds="true" DataKeyNames="Id">
                                            <CommandItemSettings AddNewRecordText="Agregar registro" CancelChangesText="Cancelar" RefreshText="Actualizar" SaveChangesText="Guardar" />
                                            <Columns>
                                                <telerik:GridEditCommandColumn ItemStyle-Width="5px" UniqueName="Expandible">
                                                </telerik:GridEditCommandColumn>
                                                <telerik:GridBoundColumn UniqueName="Id" HeaderText="Id" DataField="Id">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="Nombre" HeaderText="Nombre" DataField="Nombre">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="ImpulsaJuicio" HeaderText="Impulsa Juicio" DataField="ImpulsaJuicio">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="CambiaEtapa" HeaderText="Cambia Etapa" DataField="CambiaEtapa">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="EtapaDestino" HeaderText="Etapa Destino" DataField="EtapaDestino">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="PagoAvance" HeaderText="Pago de Avance " DataField="PagoHonorarios">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="ConceptoPAg" HeaderText="Concepto Pago" DataField="ConceptoPag">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="MontoPagar" HeaderText="Monto a Pagar" DataField="MontoPagar">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="PagoExterno" HeaderText="Pago Externo" DataField="PagoExterno">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="IncluyeIVA" HeaderText="Incluye IVA" DataField="IncluyeIVA">
                                                </telerik:GridBoundColumn>

                                                <telerik:GridTemplateColumn HeaderText="Activar/Desactivar" AllowFiltering="false">
                                                    <ItemTemplate>
                                                        <telerik:RadButton runat="server" CommandName='<%#Eval("Comando")%>' Text='<%#Eval("Texto")%>'>
                                                        </telerik:RadButton>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>

                                            </Columns>
                                            <EditFormSettings UserControlName="Acuerdos.ascx" EditFormType="WebUserControl">
                                                <EditColumn UniqueName="EditCommandColumn1">
                                                </EditColumn>
                                            </EditFormSettings>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
        <telerik:RadWindowManager ID="RadAviso" runat="server">
            <Localization OK="Aceptar" />
        </telerik:RadWindowManager>
        <asp:Button runat="server" ID="BtnAceptarConfirmacion" Visible="true" Height="1px" Style="display: none;" />
    </telerik:RadAjaxPanel>
    <asp:HiddenField ID="HidenUrs" runat="server" />
    <asp:Label runat="server" ID="LblUsuario" Visible="false"></asp:Label>
    <telerik:RadNotification ID="Notificacion" runat="server" Position="Center" Width="330" Height="160" Animation="Fade" EnableRoundedCorners="true" EnableShadow="true" Style="z-index: 100000">
    </telerik:RadNotification>





</asp:Content>


