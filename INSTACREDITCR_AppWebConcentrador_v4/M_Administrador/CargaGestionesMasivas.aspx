<%@ Page Title="" Language="VB" MasterPageFile="./MasterPage.master" AutoEventWireup="false" CodeFile="CargaGestionesMasivas.aspx.vb" Inherits="CargaGestionesMasivas" %>




<asp:Content ID="Content1" ContentPlaceHolderID="CPHMaster" runat="Server">

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" ></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="UPNLGeneral" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <table class="Table" align="Center">
            <tr>
                <td class="Titulos">
                    <asp:Label ID="LblTitulo" runat="server" Text="Carga Gestiones Masivas"></asp:Label>

                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <telerik:RadLabel runat="server" ID="lbl" Text="Archivo"></telerik:RadLabel>
                                <telerik:RadAsyncUpload ID="RadAsyncUpload1" runat="server" AllowedFileExtensions=".CSV,.txt">
                                </telerik:RadAsyncUpload>
                            </td>
                            <td>
                                <asp:Label ID="LblTipo" runat="server" Text="Tipo" CssClass="LblDesc"></asp:Label></td>
                            <td>
                                <asp:DropDownList ID="DdlTipo" runat="server" AutoPostBack="True">
                                    <asp:ListItem Selected="True">Seleccione</asp:ListItem>
                                    <asp:ListItem>Gestiones</asp:ListItem>
                                    <asp:ListItem>Visitas</asp:ListItem>
                                </asp:DropDownList></td>
                            <td>
                                <asp:Label ID="LblDelimitador" runat="server" Text="Delimitador" CssClass="LblDesc"></asp:Label></td>
                            <td>
                                <asp:DropDownList ID="DdlDelimitador" runat="server">
                                </asp:DropDownList></td>
                            <td>
                                <telerik:RadButton ID="BtnCargar" runat="server" CssClass="Botones" Text="Cargar"  />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <telerik:RadToolTip ID="RadToolTip1" runat="server" RelativeTo="Element"  TargetControlID="BtnCargar" Animation="Resize" HideEvent="LeaveTargetAndToolTip">
                                    <asp:Panel ID="PnlGestiones" runat="server" Visible="false">
                                        <table class="Table" style="color: black">
                                            <tr align="justify">
                                                <td class="Titulos" colspan="2">Layout Carga Gestiones</td>
                                            </tr>
                                            <tr class="Izquierda">
                                                <td><strong>Campo</strong> </td>
                                                <td><strong>Observaciones</strong> </td>
                                            </tr>
                                            <tr align="justify">
                                                <td>Número De Crédito:</td>
                                                <td>Hasta 25 Caracteres</td>
                                            </tr>
                                            <tr align="justify">
                                                <td>Usuario:</td>
                                                <td>Hasta 25 Caracteres</td>
                                            </tr>
                                            <tr align="justify">
                                                <td>Códigos: <br /> Accion + Resultado</td>
                                                <td>Hasta 10 Caracteres</td>
                                            </tr>
                                            <tr align="justify">
                                                <td>Comentario:</td>
                                                <td>Hasta 500 Caracteres</td>
                                            </tr>
                                            <tr align="justify">
                                                <td>Fecha De Actividad:</td>
                                                <td>DD/MM/AAAA HH24:MI:SS</td>
                                            </tr>
                                            <tr align="justify">
                                                <td>&nbsp;</td>
                                                <td style="color: red">Archivo En Formato en CSV Con Encabezado</td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel ID="PnlVisitas" runat="server" Visible="false">
                                        <table class="Table" style="color: black">
                                            <tr align="justify">
                                                <td class="Titulos" colspan="2">Layout Carga Visitas</td>
                                            </tr>
                                            <tr class="Izquierda">
                                                <td><strong>Campo</strong> </td>
                                                <td><strong>Observaciones</strong> </td>
                                            </tr>
                                            <tr align="justify">
                                                <td>Número De Crédito:</td>
                                                <td>Hasta 25 Caracteres</td>
                                            </tr>
                                            <tr align="justify">
                                                <td>Usuario que gestiono:</td>
                                                <td>Hasta 25 Caracteres</td>
                                            </tr>
                                            <tr align="justify">
                                                <td>Capturista:</td>
                                                <td>Hasta 25 Caracteres</td>
                                            </tr>
                                            <tr align="justify">
                                                <td>Fecha Visita:</td>
                                                <td>Hasta 19 Caracteres Ejemplo (25/07/2014 15:30:00)</td>
                                            </tr>
                                            <tr align="justify">
                                                <td>Fecha Captura:</td>
                                                <td>Hasta 19 Caracteres Ejemplo (25/07/2014 15:30:00)</td>
                                            </tr>
                                            <tr align="justify">
                                                <td>Códigos:</td>
                                                <td>Solo 6 Caracteres</td>
                                            </tr>
                                            <tr align="justify">
                                                <td>Comentario:</td>
                                                <td>Hasta 500 Caracteres</td>
                                            </tr>
                                            <tr align="justify">
                                                <td>Parentesco:</td>
                                                <td>Hasta 50 Caracteres Ejemplo(Cliente ,Conyuge, Familiar)</td>
                                            </tr>
                                            <tr align="justify">
                                                <td>Nombre Parentesco:</td>
                                                <td>Hasta 50 Caracteres</td>
                                            </tr>
                                            <tr align="justify">
                                                <td>Tipo De Domicilio:</td>
                                                <td>Ejemplo (Casa,Departamento,Otro)</td>
                                            </tr>
                                            <tr align="justify">
                                                <td>Nivel Socioeconómico:</td>
                                                <td>Ejemplo (Alto,Medio,Bajo, Otro)</td>
                                            </tr>
                                            <tr align="justify">
                                                <td>Número de Niveles:</td>
                                                <td>Hasta 2 Caracteres</td>
                                            </tr>
                                            <tr align="justify">
                                                <td>Caracteristicas:</td>
                                                <td>Ejemplo (Propia,Rentada, Otro)</td>
                                            </tr>
                                            <tr align="justify">
                                                <td>Color Fachada:</td>
                                                <td>Ejemplo (Rojo)</td>
                                            </tr>
                                            <tr align="justify">
                                                <td>Color Puerta:</td>
                                                <td>Ejemplo (Azul)</td>
                                            </tr>
                                            <tr align="justify">
                                                <td>Horario De Contacto:</td>
                                                <td>Hasta 10 Caracteres Ejemplo (12:2112:21)</td>
                                            </tr>
                                            <tr align="justify">
                                                <td>Dias De Contacto:</td>
                                                <td>Hasta 7 Caracteres Ejemplo (1111100)</td>
                                            </tr>
                                            <tr align="justify">
                                                <td>Punto De Referencia</td>
                                                <td>Hasta 200 Caracteres</td>
                                            </tr>
                                            <tr align="justify">
                                                <td>Entre Calle 1:</td>
                                                <td>Hasta 50 Caracteres</td>
                                            </tr>
                                            <tr align="justify">
                                                <td>Entre Calle 2:</td>
                                                <td>Hasta 50 Caracteres</td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </telerik:RadToolTip>

                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <telerik:RadGrid ID="GvCargaAsignacion" runat="server"  Width="251px" Visible="false">
                                </telerik:RadGrid>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:Label ID="LblMensaje" runat="server" Text="" CssClass="LblMsj"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </telerik:RadAjaxPanel>
    <asp:HiddenField ID="HidenUrs" runat="server" />

    <telerik:RadWindowManager ID="RadAviso" runat="server" >
        <Localization OK="Aceptar" />
    </telerik:RadWindowManager>
</asp:Content>

