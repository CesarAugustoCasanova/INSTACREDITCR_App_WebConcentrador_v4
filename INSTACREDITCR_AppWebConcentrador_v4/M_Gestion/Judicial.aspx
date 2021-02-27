<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Judicial.aspx.vb" Inherits="M_Gestion_Judicial" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mc :: Modulo Gestión</title>
    <link rel="stylesheet" href="Estilos/w3.css" />
    <meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />
</head>
<body>
    <form id="form1" runat="server">
        <noscript>
            <div class="w3-modal" style="display: block">
                <div class="w3-modal-content">
                    <div class="w3-container w3-red w3-center w3-padding-24 w3-jumbo">
                        JavaScript deshabilitado
                    </div>
                    <div class="w3-container w3-center w3-xlarge">
                        Javascript estÃ¡ deshabilitado en su navegador web. Por favor, para ver correctamente este sitio,
                        <b><i>habilite javascript</i></b>.<br />
                        <br />
                        Para ver las instrucciones para habilitar javascript en su navegador, haga click <a
                            href="http://www.enable-javascript.com/es/">aquÃ­</a>.
                    </div>
                </div>
            </div>
        </noscript>
        <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
        <telerik:RadAjaxManager runat="server" ID="RadAjaxManager1">
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel runat="server" ID="UPD1"></telerik:RadAjaxLoadingPanel>
        <telerik:RadAjaxPanel runat="server" ID="UpnGen" LoadingPanelID="UPD1">
            <div class=" w3-col w3-container s12">
                <div class="w3-container w3-blue w3-center">
                    <b>Inscripción del Expediente Judicial </b>
                </div>
                <div class="w3-container" style="overflow: auto">
                    <div class="w3-row-padding">
                        <div class="w3-col s12 m4">
                            <telerik:RadLabel runat="server" Text="Expediente Judicial"></telerik:RadLabel>
                            <telerik:RadMaskedTextBox runat="server" ID="TxtNoExpediente" Mask="####/####" Width="100%"></telerik:RadMaskedTextBox>
                        </div>
                        <div class="w3-col s12 m4">
                            <telerik:RadLabel runat="server" Text="Juzgado"></telerik:RadLabel>
                            <telerik:RadComboBox runat="server" ID="CbJuzgado" Filter="Contains" DataTextField="tex" DataValueField="val" Width="100%"></telerik:RadComboBox>
                        </div>
                        <div class="w3-col s12 m4">
                            <telerik:RadLabel runat="server" Text="Fecha Presentación"></telerik:RadLabel>
                            <telerik:RadDatePicker runat="server" ID="DPRegistro" Width="100%"></telerik:RadDatePicker>
                        </div>
                    </div>
                    <div class="w3-row-padding">
                        <div class="w3-col s12 m4">
                            <telerik:RadLabel runat="server" Text="Etapa Procesal"></telerik:RadLabel>
                            <telerik:RadTextBox runat="server" ID="TxtEtapaProc" ReadOnly="true" Width="100%"></telerik:RadTextBox>
                        </div>
                        <div class="w3-col s12 m4">
                            <telerik:RadLabel runat="server" Text="Tipo Juicio"></telerik:RadLabel>
                            <telerik:RadComboBox runat="server" ID="CbTipoJuicio" Filter="Contains" DataTextField="tex" DataValueField="val" Width="100%"></telerik:RadComboBox>
                        </div>
                        <div class="w3-col s12 m4">
                            <telerik:RadButton runat="server" Text="Guardar" ID="BtnGuardar" SingleClick="true" SingleClickText="Guardando"></telerik:RadButton>
                            <telerik:RadButton runat="server" Text="Retroceder Etapa" ID="btnRetroceder">
                            </telerik:RadButton>
                            <telerik:RadButton runat="server" Text="Editar" ID="BtnEditar"  Visible='<%# tmpPermisos("EDITAR_JUICIO") %>' ></telerik:RadButton>
                        </div>
                    </div>

                </div>
                <div class="w3-container w3-blue w3-center">
                    <b>Alerta </b>
                </div>
                <div class="w3-container" style="overflow: auto">
                    <asp:Image runat="server" ID="ImgAlerta" ImageUrl="~/M_Gestion/Imagenes/alerta.gif" Height="50px" /><telerik:RadLabel runat="server" ID="LblAlerta" Text="Msj de Alerta" ForeColor="Red"></telerik:RadLabel>
                </div>
                <div class="w3-container w3-blue w3-center">
                    <b>Resumen Judicial </b>
                </div>
                <div class="w3-responsive" style="overflow: auto">
                    <table class="w3-table w3-centered">
                        <tr>
                            <td>Fecha Ultima Actuación</td>
                            <td>Tipo Actuación Impulso</td>
                            <td>Días Juicio Activo</td>
                            <td runat="server" id ="Tddias">Días para caducar</td>
                            <td>Estatus del Expediente</td>
                            <td>Gastos de Cobranza</td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadLabel runat="server" Text="" ID="lblDteUltimaAc"></telerik:RadLabel>
                            </td>
                            <td>
                                <telerik:RadLabel runat="server" ID="lblTipoAct"></telerik:RadLabel>
                            </td>
                            <td>
                                <telerik:RadLabel runat="server" ID="LblDiasActivo"></telerik:RadLabel>
                            </td>
                            <td>
                                <telerik:RadLabel runat="server" ID="LblDiasFalta"></telerik:RadLabel>
                            </td>
                            <td>
                                <telerik:RadLabel runat="server" ID="LblEstatus"></telerik:RadLabel>
                            </td>
                            <td>
                                <telerik:RadLabel runat="server" ID="LblGastosCobranza"></telerik:RadLabel>
                            </td>
                        </tr>
                        <tr>
                            <td runat="server" id="TDGarantias" >
                                <telerik:RadButton runat="server" ID="BtnGaranias" Text="Garantías"></telerik:RadButton>
                            </td>
                            <td  runat="server" id="TDConvenios">
                                <telerik:RadButton runat="server" ID="BtnConvenios" Text="Convenios"></telerik:RadButton>
                            </td>
                            <td  runat="server" id="TDTramites">
                                <telerik:RadButton runat="server" ID="BtnTramites" Text="Tramites"></telerik:RadButton>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="w3-container w3-blue w3-center">
                    <b>Impulso Procesal </b>
                </div>
                <div class="w3-container" style="overflow: auto">
                    <telerik:RadGrid runat="server" ID="GvInpulsos" AutoGenerateColumns="false" AllowSorting="true" AllowPaging="true" OnNeedDataSource="GvInpulsos_NeedDataSource">

                        <MasterTableView CommandItemDisplay="Top" Caption="" DataKeyNames="ID" EditFormSettings-PopUpSettings-KeepInScreenBounds="true">
                            <CommandItemTemplate>
                                <div class="w3-col s10 w3-right" style="height: 100%;">
                                    <asp:LinkButton ID="btnEditSelected" runat="server" CommandName="Terminar" Style="text-decoration: none;" CssClass="w3-hover-shadow w3-padding w3-round"><img style="border:0px;vertical-align:middle;height:20px;" alt="Editar" src="Imagenes/editar.png?v=1.2"/>Terminar</asp:LinkButton>&nbsp;&nbsp;
                       
                                   

                                    <asp:LinkButton ID="LinkButton2" runat="server" CommandName="InitInsert" Style="text-decoration: none" CssClass="w3-hover-shadow w3-padding w3-round"><img style="border:0px;vertical-align:middle;height:20px;" alt="actualizar" src="Imagenes/anadir.png?v=1.2"/>Agregar Petición</asp:LinkButton>&nbsp;&nbsp;
                       
                                   

                                    <asp:LinkButton ID="LinkButton4" runat="server" CommandName="RebindGrid" Style="text-decoration: none" CssClass="w3-hover-shadow w3-padding w3-round"><img style="border:0px;vertical-align:middle;height:20px;" alt="" src="Imagenes/recargar.png?v=1.2"/>Recargar lista</asp:LinkButton>
                                </div>
                            </CommandItemTemplate>
                            <Columns>
                                <telerik:GridEditCommandColumn UniqueName="EditCommandColumn1">
                                    <ItemStyle Width="15px" />
                                </telerik:GridEditCommandColumn>
                                <telerik:GridBoundColumn HeaderText="ID" UniqueName="ID" DataField="ID"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Fecha Peticion" UniqueName="DtePeticion" DataField="DtePeticion"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Nombre Peticion" UniqueName="NombreP" DataField="NombreP"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Estado Peticion" UniqueName="EstadoP" DataField="EstadoP"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Nombre Auto" UniqueName="NombreA" DataField="NombreA"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Fecha Actuacion" UniqueName="DteAtuacion" DataField="DteAtuacion"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Resultado Auto" UniqueName="ResultadoA" DataField="ResultadoA"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Impulsa Etapa" UniqueName="ImpulsaE" DataField="ImpulsaE"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Cambia Etapa" UniqueName="CambiaE" DataField="CambiaE"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Dias Caducar" UniqueName="Dias" DataField="Dias"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="observaciones" DataField="observaciones"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="consecutivo" DataField="consecutivo"></telerik:GridBoundColumn>
                            </Columns>

                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                <div class="w3-container w3-blue w3-center">
                    <b>Participantes </b>
                </div>
                <div class="w3-container" style="overflow: auto">
                    <telerik:RadGrid runat="server" ID="GVParticipantes" AllowSorting="true" AllowPaging="true" OnNeedDataSource="GVParticipantes_NeedDataSource">
                    </telerik:RadGrid>
                </div>
                 <div class="w3-container w3-blue w3-center">
                    <b>Juicios Anteriores </b>
                </div>
                <div class="w3-container" style="overflow: auto">
                    <telerik:RadGrid runat="server" ID="GVJuiciospasados" AllowSorting="true" AllowPaging="true" OnNeedDataSource="GVJuiciospasados_NeedDataSource">
                    </telerik:RadGrid>
                </div>
            </div>
            <telerik:RadWindow runat="server" ID="WinJudicial" Behaviors="Resize" Modal="true" Title="Registro de Impulso Procesal" VisibleStatusbar="false" Width="770px" Height="500px">
                <ContentTemplate>
                    <telerik:RadAjaxPanel runat="server" ID="PnlWin" LoadingPanelID="UPD1" CssClass="w3-container">

                        <div class="w3-center">
                            <telerik:RadComboBox runat="server" ID="CbAvnRt" Filter="Contains" Label="Seleciona Etapa" DataTextField="tex" DataValueField="val" Width="100%" Visible="false" AutoPostBack="true"></telerik:RadComboBox>
                        </div>
                        <div runat="server" id="GenVen">
                            <div class="w3-half w3-center">
                                <telerik:RadLabel runat="server" Text="Nombre de la promoción" Width="100%"></telerik:RadLabel>
                                <telerik:RadComboBox runat="server" Filter="Contains" ID="CbPromocion" DataTextField="tex" DataValueField="val" Width="100%" AutoPostBack="true"></telerik:RadComboBox>
                                <telerik:RadLabel runat="server" Text="Acuerdo (Auto)" Width="100%"></telerik:RadLabel>
                                <telerik:RadComboBox runat="server" Filter="Contains" ID="CbAcuerdo" DataTextField="tex" DataValueField="val" Width="100%" AutoPostBack="true"></telerik:RadComboBox>
                                <telerik:RadLabel runat="server" Text="Resultado del Acuerdo (Auto)" Width="100%"></telerik:RadLabel>
                                <telerik:RadComboBox runat="server" Filter="Contains" ID="CbResultado" DataTextField="tex" DataValueField="val" Width="100%"></telerik:RadComboBox>
                            </div>
                            <div class="w3-half w3-center">
                                <telerik:RadLabel runat="server" Text="Fecha Actuación de la promoción" Width="100%"></telerik:RadLabel>
                                <telerik:RadDatePicker runat="server" Width="100%" ID="DpPromocion"></telerik:RadDatePicker>
                                <telerik:RadLabel runat="server" Text="Fecha Actuación del Acuerdo" Width="100%"></telerik:RadLabel>
                                <telerik:RadDatePicker runat="server" Width="100%" ID="DpAcuerdo"></telerik:RadDatePicker>
                                <telerik:RadLabel runat="server" Text="Observaciones" Width="100%"></telerik:RadLabel>
                                <telerik:RadTextBox runat="server" ID="TxtObservaciones" Width="100%" TextMode="MultiLine" Resize="Both"></telerik:RadTextBox>
                            </div>
                        </div>
                        <div runat="server" id="ExoVen" visible="false">
                            <div class="w3-half w3-center">
                                <telerik:RadLabel runat="server" Text="Nombre de la Persona Exhortada" Width="100%"></telerik:RadLabel>
                                <telerik:RadComboBox runat="server" Filter="Contains" ID="CbPersona" DataTextField="tex" DataValueField="val" Width="100%"></telerik:RadComboBox>
                                <telerik:RadLabel runat="server" Text="Ciudad" Width="100%"></telerik:RadLabel>
                                <telerik:RadComboBox runat="server" Filter="Contains" ID="CbCiudad" DataTextField="tex" DataValueField="val" Width="100%" AutoPostBack="true"></telerik:RadComboBox>
                                <telerik:RadLabel runat="server" Text="Fecha de Radicación del Exhorto" Width="100%"></telerik:RadLabel>
                                <telerik:RadDatePicker runat="server" Width="100%" ID="DPRadicacion"></telerik:RadDatePicker>
                                <telerik:RadLabel runat="server" Text="Observaciones" Width="100%"></telerik:RadLabel>
                                <telerik:RadTextBox runat="server" ID="TxtObserEx" Width="100%" TextMode="MultiLine" Resize="Both"></telerik:RadTextBox>
                            </div>
                            <div class="w3-half w3-center">
                                <telerik:RadLabel runat="server" Text="Número de Exhorto" Width="100%"></telerik:RadLabel>
                                <telerik:RadMaskedTextBox runat="server" ID="MTBNumEx" Mask="####/####" Width="100%"></telerik:RadMaskedTextBox>
                                <telerik:RadLabel runat="server" Text="Juzgado" Width="100%"></telerik:RadLabel>
                                <telerik:RadComboBox runat="server" Filter="Contains" ID="CbJuzgadoEx" DataTextField="tex" DataValueField="val" Width="100%"></telerik:RadComboBox>
                                <telerik:RadLabel runat="server" Text="Fecha de Regreso del Exhorto" Width="100%"></telerik:RadLabel>
                                <telerik:RadDatePicker runat="server" Width="100%" ID="DpRegresaEX"></telerik:RadDatePicker>
                                <telerik:RadLabel runat="server" Text=" " Width="100%"></telerik:RadLabel>
                            </div>
                        </div>
                        <div runat="server" id="DiliVen" visible="false">
                            <div class="w3-half w3-center">
                                <telerik:RadLabel runat="server" Text="Participante" Width="100%"></telerik:RadLabel>
                                <telerik:RadComboBox runat="server" Filter="Contains" ID="cbParticipante" DataTextField="tex" DataValueField="val" Width="100%"></telerik:RadComboBox>
                                <telerik:RadLabel runat="server" Text="Fecha de programación diligencia" Width="100%"></telerik:RadLabel>
                                <telerik:RadDatePicker runat="server" Width="100%" ID="DPDteDiligencia"></telerik:RadDatePicker>
                                <telerik:RadLabel runat="server" ID="lbllbsub" Text="Subtipo de resultado" Width="100%"></telerik:RadLabel>
                                <telerik:RadComboBox runat="server" Filter="Contains" ID="CbSubResultadodili" DataTextField="tex" DataValueField="val" Width="100%" ></telerik:RadComboBox>

                            </div>
                            <div class="w3-half w3-center">
                                <telerik:RadLabel runat="server" Text="Tipo Diligencia" Width="100%"></telerik:RadLabel>
                                <telerik:RadComboBox runat="server" Filter="Contains" ID="cbdiligencia" DataTextField="tex" DataValueField="val" Width="100%" AutoPostBack="true"></telerik:RadComboBox>
                                <telerik:RadLabel runat="server" ID="lblgarantia" Text="Garantía" Width="100%" Visible="false"></telerik:RadLabel>
                                <telerik:RadComboBox runat="server" Filter="Contains" ID="CbGarantiaJud" DataTextField="tex" DataValueField="val" Width="100%"  Visible="false"></telerik:RadComboBox>
                                <telerik:RadLabel runat="server" ID="LblTipoREs" Text="Tipo Resultado" Width="100%"></telerik:RadLabel>
                                <telerik:RadComboBox runat="server" Filter="Contains" ID="CbResultadodili" DataTextField="tex" DataValueField="val" Width="100%" AutoPostBack="true"></telerik:RadComboBox>
                                <telerik:RadLabel runat="server" Text="Observaciones" Width="100%"></telerik:RadLabel>
                                <telerik:RadTextBox runat="server" ID="txtobserDili" Width="100%" TextMode="MultiLine" Resize="Both"></telerik:RadTextBox>
                            </div>
                        </div>
                        
                        <div class="w3-center">
                            <telerik:RadButton runat="server" ID="BtnAceptar" Text="Aceptar"></telerik:RadButton>
                            <telerik:RadButton runat="server" ID="BtnCancelar" Text="Cancelar"></telerik:RadButton>
                            <br />
                            <telerik:RadLabel runat="server" ID="lblconse" Visible="true"></telerik:RadLabel>
                            <telerik:RadLabel runat="server" ID="lbltope" Visible="true"></telerik:RadLabel>
                        </div>
                        <div class="w3-center">
                             <telerik:RadGrid runat="server" ID="gridregresa" Visible="false" AutoGenerateColumns="false" AllowSorting="true" AllowPaging="true" OnNeedDataSource="gridregresa_NeedDataSource">

                        <MasterTableView CommandItemDisplay="None" Caption="" DataKeyNames="ID" EditFormSettings-PopUpSettings-KeepInScreenBounds="true">
                            
                            <Columns>
                                <telerik:GridBoundColumn HeaderText="ID" UniqueName="ID" DataField="ID"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Fecha Peticion" UniqueName="DtePeticion" DataField="DtePeticion"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Nombre Peticion" UniqueName="NombreP" DataField="NombreP"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Estado Peticion" UniqueName="EstadoP" DataField="EstadoP"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Nombre Auto" UniqueName="NombreA" DataField="NombreA"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Fecha Actuacion" UniqueName="DteAtuacion" DataField="DteAtuacion"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Resultado Auto" UniqueName="ResultadoA" DataField="ResultadoA"></telerik:GridBoundColumn>
                                <%--<telerik:GridBoundColumn HeaderText="Impulsa Etapa" UniqueName="ImpulsaE" DataField="ImpulsaE"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Cambia Etapa" UniqueName="CambiaE" DataField="CambiaE"></telerik:GridBoundColumn>
                               --%>
                            </Columns>

                        </MasterTableView>
                    </telerik:RadGrid>
                        </div>
                        <telerik:RadNotification ID="Notificacion2" runat="server" Position="Center" Width="330" Height="160"
                            Animation="Fade" EnableRoundedCorners="true" EnableShadow="true" Style="z-index: 100000">
                        </telerik:RadNotification>
                    </telerik:RadAjaxPanel>
                </ContentTemplate>
            </telerik:RadWindow>
            <telerik:RadWindow runat="server" ID="wincierre" Behaviors="Resize" Modal="true" Title="Cierre Juicios" VisibleStatusbar="false" Width="770px" Height="500px">
                <ContentTemplate>
                    <telerik:RadAjaxPanel runat="server" ID="RadAjaxPanel1" LoadingPanelID="UPD1" CssClass="w3-container">

                        <div class="w3-center">
                            <telerik:RadLabel runat="server" Text="Fecha Cierre Juicio" Width="100%"></telerik:RadLabel>
                            <telerik:RadDatePicker runat="server" Width="100%" ID="DpCierre"></telerik:RadDatePicker>
                            <telerik:RadLabel runat="server" Text="Motivo Cierre" Width="100%"></telerik:RadLabel>
                            <telerik:RadComboBox runat="server" Filter="Contains" ID="CbMotivo" DataTextField="tex" DataValueField="val" Width="100%"></telerik:RadComboBox>
                            <telerik:RadLabel runat="server" Text="Descripción" Width="100%"></telerik:RadLabel>
                            <telerik:RadTextBox runat="server" ID="TxtDescrip" Width="100%" TextMode="MultiLine" Resize="Both"></telerik:RadTextBox>

                            <div class="w3-center">
                                <telerik:RadButton runat="server" ID="BtnAceptarCierre" Text="Aceptar"></telerik:RadButton>
                                <telerik:RadButton runat="server" ID="BtnCancelarCierre" Text="Cancelar"></telerik:RadButton>
                                  <telerik:RadButton runat="server" ID="BtnConfirmarCierre" Text="Confirmar" Visible="false"></telerik:RadButton>
                                <br />
                            </div>
                        </div>
                        <telerik:RadNotification ID="Notification3" runat="server" Position="Center" Width="330" Height="160"
                            Animation="Fade" EnableRoundedCorners="true" EnableShadow="true" Style="z-index: 100000">
                        </telerik:RadNotification>
                    </telerik:RadAjaxPanel>
                </ContentTemplate>
            </telerik:RadWindow>

            <telerik:RadWindow runat="server" ID="Garantias" Behaviors="Resize,Close" Modal="true" Title="Garantias" VisibleStatusbar="false" Width="770px" Height="500px">
                <ContentTemplate>
                    <telerik:RadAjaxPanel runat="server" ID="RadAjaxPanel2" LoadingPanelID="UPD1" CssClass="w3-container">
                        <telerik:RadGrid runat="server" ID="GvGarantias" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" OnNeedDataSource="GvGarantias_NeedDataSource">
                            <MasterTableView CommandItemDisplay="Top" Caption="" DataKeyNames="ID" EditFormSettings-PopUpSettings-KeepInScreenBounds="true">
                                <CommandItemTemplate>
                                    <div class="w3-col s10 w3-right" style="height: 100%;">
                                        

                                    <asp:LinkButton ID="LinkButton2" runat="server" CommandName="InitInsert" Style="text-decoration: none" CssClass="w3-hover-shadow w3-padding w3-round"><img style="border:0px;vertical-align:middle;height:20px;" alt="actualizar" src="Imagenes/anadir.png?v=1.2"/>Agregar Petición</asp:LinkButton>&nbsp;&nbsp;
                       
                                    <asp:LinkButton ID="LinkButton4" runat="server" CommandName="RebindGrid" Style="text-decoration: none" CssClass="w3-hover-shadow w3-padding w3-round"><img style="border:0px;vertical-align:middle;height:20px;" alt="" src="Imagenes/recargar.png?v=1.2"/>Recargar lista</asp:LinkButton>
                                    </div>
                                </CommandItemTemplate>
                                <Columns>
                                    <telerik:GridEditCommandColumn UniqueName="EditCommandColumn1">
                                        <ItemStyle Width="15px" />
                                    </telerik:GridEditCommandColumn>
                                    <telerik:GridBoundColumn HeaderText="ID" DataField="ID" UniqueName="ID"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Tipo Garantia" DataField="muebleinmueble" UniqueName="muebleinmueble"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Nombre" DataField="Nombre" UniqueName="Nombre"></telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                        <telerik:RadLabel runat="server" ID="lblEmbargo" Text="Tipo Embargo" Width="100%" Visible="false"></telerik:RadLabel>
                        <telerik:RadComboBox runat="server" Filter="Contains" ID="cbTembargo" AutoPostBack="true" Width="100%" Visible="false">
                                    <Items>
                                        <telerik:RadComboBoxItem Value="0" Text="Seleccione" />
                                        <telerik:RadComboBoxItem Value="1" Text="Mueble" />
                                        <telerik:RadComboBoxItem Value="2" Text="Inmueble" />
                                    </Items>
                                </telerik:RadComboBox>
                        <div runat="server" id="divmueble" visible="false">
                            <div class="w3-half w3-center">
                                <telerik:RadLabel runat="server" Text="Tipo de Mueble" Width="100%"></telerik:RadLabel>
                                <telerik:RadTextBox runat="server" ID="txtipo" Width="100%" TextMode="MultiLine" Resize="Both"></telerik:RadTextBox>
                                <telerik:RadLabel runat="server" Text="Modelo" Width="100%"></telerik:RadLabel>
                                <telerik:RadTextBox runat="server" ID="txtmodelo" Width="100%" TextMode="MultiLine" Resize="Both"></telerik:RadTextBox>
                                <telerik:RadLabel runat="server" Text="Serie" Width="100%"></telerik:RadLabel>
                                <telerik:RadTextBox runat="server" ID="txtserie" Width="100%" TextMode="MultiLine" Resize="Both"></telerik:RadTextBox>
                                <telerik:RadLabel runat="server" Text="Depositario" Width="100%"></telerik:RadLabel>
                                <telerik:RadTextBox runat="server" ID="txtDepositario" Width="100%" TextMode="MultiLine" Resize="Both"></telerik:RadTextBox>
                                <telerik:RadLabel runat="server" Text="Fecha entrada" Width="100%"></telerik:RadLabel>
                                <telerik:RadDatePicker runat="server" Width="100%" ID="DPEntrada"></telerik:RadDatePicker>
                                <div>
                                    <telerik:RadLabel runat="server" Text="Color exterior" Width="100%"></telerik:RadLabel>

                                    <telerik:RadColorPicker runat="server" Width="30px" ID="CpExterior" AutoPostBack="false"></telerik:RadColorPicker>
                                </div>
                                <telerik:RadLabel runat="server" Text="Versión" Width="100%"></telerik:RadLabel>
                                <telerik:RadTextBox runat="server" ID="Txtversion" Width="100%" TextMode="MultiLine" Resize="Both"></telerik:RadTextBox>

                            </div>

                            <div class="w3-half w3-center">
                               
                                <telerik:RadLabel runat="server" Text="Marca" Width="100%"></telerik:RadLabel>
                                <telerik:RadTextBox runat="server" ID="txtmarca" Width="100%" TextMode="MultiLine" Resize="Both"></telerik:RadTextBox>
                                <telerik:RadLabel runat="server" Text="Descripción" Width="100%"></telerik:RadLabel>
                                <telerik:RadTextBox runat="server" ID="txtdescripmueble" Width="100%" TextMode="MultiLine" Resize="Both"></telerik:RadTextBox>
                                <telerik:RadLabel runat="server" Text="Almacén" Width="100%"></telerik:RadLabel>
                                <telerik:RadTextBox runat="server" ID="TxtAlmacen" Width="100%" TextMode="MultiLine" Resize="Both"></telerik:RadTextBox>
                                <telerik:RadLabel runat="server" Text="Linea" Width="100%"></telerik:RadLabel>
                                <telerik:RadTextBox runat="server" ID="txtLinea" Width="100%" TextMode="MultiLine" Resize="Both"></telerik:RadTextBox>
                                <telerik:RadLabel runat="server" Text="Uso" Width="100%"></telerik:RadLabel>
                                <telerik:RadTextBox runat="server" ID="txtUso" Width="100%" TextMode="MultiLine" Resize="Both"></telerik:RadTextBox>
                                <telerik:RadLabel runat="server" Text="Clave Vehicular" Width="100%"></telerik:RadLabel>
                                <telerik:RadTextBox runat="server" ID="TxtVehicular" Width="100%" TextMode="MultiLine" Resize="Both"></telerik:RadTextBox>
                                <telerik:RadLabel runat="server" Text="No. Motor" Width="100%"></telerik:RadLabel>
                                <telerik:RadTextBox runat="server" ID="TxtMotor" Width="100%" TextMode="MultiLine" Resize="Both"></telerik:RadTextBox>
                            </div>

                        </div>
                        <div runat="server" id="divInmuele" visible="false">
                            <div class="w3-half w3-center">
                                <telerik:RadLabel runat="server" Text="Tipo de Inmueble" Width="100%"></telerik:RadLabel>
                                <telerik:RadTextBox runat="server" ID="TxtInmueble" Width="100%" TextMode="MultiLine" Resize="Both"></telerik:RadTextBox>
                              
                                <telerik:RadLabel runat="server" Text="Numero de escritura" Width="100%"></telerik:RadLabel>
                                <telerik:RadTextBox runat="server" ID="TxtEscritura" Width="100%" TextMode="MultiLine" Resize="Both"></telerik:RadTextBox>
                                <telerik:RadLabel runat="server" Text="Calle" Width="100%"></telerik:RadLabel>
                                <telerik:RadTextBox runat="server" ID="TxtDomicilio" Width="100%" TextMode="MultiLine" Resize="Both"></telerik:RadTextBox>
                                <telerik:RadLabel runat="server" Text="Municipio" Width="100%"></telerik:RadLabel>
                                <telerik:RadTextBox runat="server" ID="TxtMunicipio" Width="100%" TextMode="MultiLine" Resize="Both"></telerik:RadTextBox>
                                <telerik:RadLabel runat="server" Text="Numero exterior" Width="100%"></telerik:RadLabel>
                                <telerik:RadTextBox runat="server" ID="Txtnoexterior" Width="100%" TextMode="MultiLine" Resize="Both"></telerik:RadTextBox>

                            </div>
                            <div class="w3-half w3-center">
                                <telerik:RadLabel runat="server" Text="Ubicación inmueble" Width="100%"></telerik:RadLabel>
                                <telerik:RadTextBox runat="server" ID="Txtubicacion" Width="100%" TextMode="MultiLine" Resize="Both"></telerik:RadTextBox>
                                <telerik:RadLabel runat="server" Text="Datos registrales" Width="100%"></telerik:RadLabel>
                                <telerik:RadTextBox runat="server" ID="TxtRegistrales" Width="100%" TextMode="MultiLine" Resize="Both"></telerik:RadTextBox>
                                <telerik:RadLabel runat="server" Text="Folio Real" Width="100%"></telerik:RadLabel>
                                <telerik:RadTextBox runat="server" ID="TxtFolio" Width="100%" TextMode="MultiLine" Resize="Both"></telerik:RadTextBox>
                                <telerik:RadLabel runat="server" Text="Estado" Width="100%"></telerik:RadLabel>
                                <telerik:RadTextBox runat="server" ID="TxtEstado" Width="100%" TextMode="MultiLine" Resize="Both"></telerik:RadTextBox>
                                <telerik:RadLabel runat="server" Text="Colonia" Width="100%"></telerik:RadLabel>
                                <telerik:RadTextBox runat="server" ID="txtcolonia" Width="100%" TextMode="MultiLine" Resize="Both"></telerik:RadTextBox>
                                <telerik:RadLabel runat="server" Text="Numero interior" Width="100%"></telerik:RadLabel>
                                <telerik:RadTextBox runat="server" ID="txtinterior" Width="100%" TextMode="MultiLine" Resize="Both"></telerik:RadTextBox>

                            </div>
                            
                           
                        </div>
                         <div class="w3-center">
                                <telerik:RadButton runat="server" ID="BtnAceptarGara" Text="Aceptar" Visible="false"></telerik:RadButton>
                                <telerik:RadButton runat="server" ID="BtnCancelarGara" Text="Cancelar" Visible="false"></telerik:RadButton>
                               <telerik:RadLabel runat="server" Text="" Width="100%" ID="LblConsegara"></telerik:RadLabel>
                                <br />
                            </div>
                    </telerik:RadAjaxPanel>
                </ContentTemplate>
            </telerik:RadWindow>
                <telerik:RadWindow runat="server" ID="wintramites" Behaviors="Resize,Close" Modal="true" Title="Tramites Judiciales" VisibleStatusbar="false" Width="770px" Height="500px">
                <ContentTemplate>
                    <telerik:RadAjaxPanel runat="server" ID="RadAjaxPanel3" LoadingPanelID="UPD1" CssClass="w3-container">
                         <div runat="server">
                             <telerik:RadGrid runat="server" ID="gridtramites" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" OnNeedDataSource="gridtramites_NeedDataSource">
                            <MasterTableView CommandItemDisplay="Top" Caption="" DataKeyNames="ID" EditFormSettings-PopUpSettings-KeepInScreenBounds="true">
                                <CommandItemTemplate>
                                    <div class="w3-col s10 w3-right" style="height: 100%;">
                                        

                                    <asp:LinkButton ID="LinkButton2" runat="server" CommandName="InitInsert" Style="text-decoration: none" CssClass="w3-hover-shadow w3-padding w3-round"><img style="border:0px;vertical-align:middle;height:20px;" alt="actualizar" src="Imagenes/anadir.png?v=1.2"/>Agregar Tramite</asp:LinkButton>&nbsp;&nbsp;
                       
                                    <asp:LinkButton ID="LinkButton4" runat="server" CommandName="RebindGrid" Style="text-decoration: none" CssClass="w3-hover-shadow w3-padding w3-round"><img style="border:0px;vertical-align:middle;height:20px;" alt="" src="Imagenes/recargar.png?v=1.2"/>Recargar lista</asp:LinkButton>
                                    </div>
                                </CommandItemTemplate>
                                <Columns>
                                    <telerik:GridEditCommandColumn UniqueName="EditCommandColumn1">
                                        <ItemStyle Width="15px" />
                                    </telerik:GridEditCommandColumn>
                                    <telerik:GridBoundColumn HeaderText="ID" DataField="ID" UniqueName="ID"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Tipo tramite" DataField="tramite" UniqueName="tramite"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Observacion" DataField="Observacion" UniqueName="Observacion"></telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                             <div runat="server" id="tramites" visible="false">
                            <div class="w3-half w3-center">
                                <telerik:RadLabel runat="server" Text="Tipo tramite" Width="100%"></telerik:RadLabel>
                                <telerik:RadComboBox runat="server" Filter="Contains" ID="cbtramite" DataTextField="tex" DataValueField="val" Width="100%" AutoPostBack="true"></telerik:RadComboBox>
                                
                            </div>
                            <div class="w3-half w3-center">
                                
                                <telerik:RadLabel runat="server" Text="Observaciones" Width="100%"></telerik:RadLabel>
                                <telerik:RadTextBox runat="server" ID="txtobservacionestrami" Width="100%" TextMode="MultiLine" Resize="Both"></telerik:RadTextBox>
                            </div>
                                 <div class="w3-center">
                                <telerik:RadButton runat="server" ID="btnaceptartrami" Text="Aceptar" Visible="false"></telerik:RadButton>
                                <telerik:RadButton runat="server" ID="btncancelartrami" Text="Cancelar" Visible="false"></telerik:RadButton>
                                <br />
                                      <telerik:RadLabel runat="server" Text="" Width="100%" ID="lblconsetra"></telerik:RadLabel>
                            </div>
                                 </div>
                        </div>
                        </telerik:RadAjaxPanel>
                        </ContentTemplate>
                    </telerik:RadWindow>
             <telerik:RadWindow runat="server" ID="winConvenios" Behaviors="Resize,Close" Modal="true" Title="Convenios Judiciales" DestroyOnClose="true" VisibleStatusbar="false" Width="770px" Height="500px">
                <ContentTemplate>
                    <telerik:RadAjaxPanel runat="server" ID="RadAjaxPanel4" LoadingPanelID="UPD1" CssClass="w3-container">
                         <div runat="server">
                            
                             <div runat="server" id="convenios" >
                            <div class="w3-half w3-center">
                                <telerik:RadLabel runat="server" Text="Fecha Convenio" Width="100%"></telerik:RadLabel>
                                <telerik:RadDatePicker runat="server" ID="DPFechaConvenio" Width="100%"></telerik:RadDatePicker>
                                <telerik:RadLabel runat="server" Text="Total por pago" Width="100%"></telerik:RadLabel>
                                <telerik:RadNumericTextBox runat="server" ID="txttotal" ShowSpinButtons="true" Type="Currency"  Width="100%" MinValue="0" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>
                                  <telerik:RadLabel runat="server" Text="Observaciones" Width="100%"></telerik:RadLabel>
                                <telerik:RadTextBox runat="server" ID="TxtObservacionesconve" Width="100%" TextMode="MultiLine" Resize="Both"></telerik:RadTextBox>
                                  <telerik:RadLabel runat="server" Text="En caso de incumplimiento" Width="100%"></telerik:RadLabel>
                                <telerik:RadTextBox runat="server" ID="txtincumplimiento" Width="100%" TextMode="MultiLine" Resize="Both"></telerik:RadTextBox>
                            </div>
                            <div class="w3-half w3-center">
                                <telerik:RadLabel runat="server" Text="Pagos a realizar" Width="100%"></telerik:RadLabel>
                                <telerik:RadNumericTextBox runat="server" ID="TxtPagos" ShowSpinButtons="true"  Width="100%" MinValue="0" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>
                                <telerik:RadLabel runat="server" Text="Total a Pagar" Width="100%"></telerik:RadLabel>
                                <telerik:RadNumericTextBox runat="server" ID="txttotaltotal" ShowSpinButtons="true" Type="Currency"  Width="100%" MinValue="0" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>
                                <telerik:RadLabel runat="server" Text="Garantía" Width="100%"></telerik:RadLabel>
                                <telerik:RadComboBox runat="server" Filter="Contains" ID="CbGarantiaconve" DataTextField="tex" DataValueField="val" Width="100%" AutoPostBack="true"></telerik:RadComboBox>

                            </div>
                                 <div class="w3-center">
                                <telerik:RadButton runat="server" ID="btnaceptarconve" Text="Aceptar" ></telerik:RadButton>
                                <telerik:RadButton runat="server" ID="btncancelarconve" Text="Editar" Visible='<%# tmpPermisos("EDITAR_JUICIO") %>'></telerik:RadButton>
                                <br />
                                      <telerik:RadLabel runat="server" Text="" Width="100%" ID="lblconseconv"></telerik:RadLabel>
                            </div>
                                 </div>
                        </div>
                        </telerik:RadAjaxPanel>
                        </ContentTemplate>
                    </telerik:RadWindow>
            
            <telerik:RadNotification ID="Notificacion" runat="server" Position="Center" Width="330" Height="160"
                Animation="Fade" EnableRoundedCorners="true" EnableShadow="true" Style="z-index: 100000">
            </telerik:RadNotification>
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
