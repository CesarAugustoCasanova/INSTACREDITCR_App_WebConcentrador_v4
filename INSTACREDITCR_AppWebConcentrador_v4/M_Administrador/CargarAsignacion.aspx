﻿<%@ Page Title="" Language="VB" MasterPageFile="MasterPage.master" AutoEventWireup="false" CodeFile="CargarAsignacion.aspx.vb" Inherits="MAdministrador_CargarAsignacion" %>



<asp:Content ID="Content1" ContentPlaceHolderID="CPHMaster" runat="Server">
    <link rel="stylesheet" href="https://www.w3schools.com/w3css/4/w3.css" />


    <%--<div class="Pagina">--%>
    <telerik:RadAjaxLoadingPanel ID="Radpanelcarga" runat="server" ></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="Pnlgen" runat="server" HorizontalAlign="NotSet" LoadingPanelID="Radpanelcarga">
        <table class="Table">
         <tr>
                <td class="Titulos">
                    <asp:Label ID="LblTitulo" runat="server" Text="Carga Asignación"></asp:Label>
                </td>
            </tr>
               <tr>
                   <td class="LblDesc">
                       <telerik:RadLabel runat="server" ID="RadLabel1" Text="Seleccione una opción de asignación"></telerik:RadLabel>
                       
                      <%-- <asp:DropDownList ID="DdltipoAsig" runat="server" CssClass="DdlDesc" AutoPostBack="true">
                           <asp:ListItem Selected="True" Value="Seleccione">Seleccione</asp:ListItem>
                           <asp:ListItem >Cuenta</asp:ListItem>
                           <asp:ListItem >Archivo</asp:ListItem>
                       </asp:DropDownList>--%>
                   </td>
               </tr>
            <tr>
                <td class="LblDesc">
                    <telerik:RadComboBox ID="DdltipoAsig" runat="server" AutoPostBack="True" Culture="es-ES" EmptyMessage="Selecciona un tipo de asignación" Width="400px">
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Selected="True" Text="Seleccione" Value="Seleccione" />
                            <telerik:RadComboBoxItem runat="server" Text="Crédito" Value="Credito" />
                            <telerik:RadComboBoxItem runat="server" Text="Archivo Simple" Value="Archivo" />
                            <telerik:RadComboBoxItem runat="server" Text="Archivo Compuesto" Value="ArchivoC" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="PnlArchivo" runat="server" Visible="false">
                        <table align="left" class="Table">
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <telerik:RadLabel ID="LblInstancia" runat="server" Text="Instancia">
                                                </telerik:RadLabel>
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="DdlInstanciaA" runat="server" AutoPostBack="true" EmptyMessage="Selecciona una instancia" Width="400px">
                                                </telerik:RadComboBox>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <telerik:RadLabel ID="LblUsuarioA" runat="server" Text="Usuario">
                                                </telerik:RadLabel>
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="DdlUsuarioA" runat="server" AutoPostBack="true" EmptyMessage="Selecciona una usuario" Visible="False" Width="400px">
                                                </telerik:RadComboBox>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <telerik:RadLabel ID="lbl0" runat="server" Text="Archivo">
                                                </telerik:RadLabel>
                                            </td>
                                            <td>
                                                <telerik:RadAsyncUpload ID="RadAsyncUpload1" runat="server" AllowedFileExtensions=".CSV,.txt" MaxFileInputsCount="1" RenderMode="Lightweight" Width="500px">
                                                </telerik:RadAsyncUpload>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>
                                                <telerik:RadButton ID="BtnCargar" runat="server" CssClass="Botones" Text="Cargar" Visible="False" />
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <telerik:RadToolTip ID="RadToolTip1" runat="server" Animation="Resize" HideEvent="LeaveTargetAndToolTip" RelativeTo="Element" TargetControlID="BtnCargar">
                                                    <table class="Table" style="color: black">
                                                        <tr align="justify">
                                                            <td class="Titulos" colspan="2">Layout Asignación De Créditos</td>
                                                        </tr>
                                                        <tr class="Izquierda">
                                                            <td><strong>Campo</strong> </td>
                                                            <td><strong>Observaciones</strong> </td>
                                                        </tr>
                                                        <tr class="Izquierda">
                                                            <td>Número De Crédito:</td>
                                                            <td>Hasta 25 Caracteres</td>
                                                        </tr>
                                                        <tr class="Izquierda">
                                                            <td><telerik:RadLabel ID="RLayoutU" runat="server" Text="Usuario">
                                                </telerik:RadLabel></td>
                                                            <td><telerik:RadLabel ID="RLayoutUD" runat="server" Text="Login De Usuario">
                                                </telerik:RadLabel></td>
                                                        </tr>
                                                        <tr class="Izquierda">
                                                            <td><telerik:RadLabel ID="RLayoutI" runat="server" Text="Instancia">
                                                </telerik:RadLabel></td>
                                                            <td><telerik:RadLabel ID="RLayoutID"  runat="server" Text="Instancia A Asignar">
                                                </telerik:RadLabel></td>
                                                        </tr>
                                                        <tr class="Izquierda">
                                                            <td>&nbsp;</td>
                                                            <td style="color: red">Archivo En Formato en CSV Con Encabezado</td>
                                                        </tr>
                                                    </table>
                                                </telerik:RadToolTip>
                                            </td>
                                            
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="PnlCredito" runat="server" Visible="false">
                        <table class="LblDesc">
                            <tr>
                                <td>
                                    <telerik:RadLabel ID="RadLabel2" runat="server" Text="Crédito">
                                    </telerik:RadLabel>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadLabel ID="RadLabel4" runat="server" Text="Instancia">
                                    </telerik:RadLabel>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="DdlInstanciaC" runat="server" AutoPostBack="true" EmptyMessage="Selecciona una instancia" Width="400px">
                                    </telerik:RadComboBox>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadLabel ID="LblUsuarioC" runat="server" Text="Usuario">
                                    </telerik:RadLabel>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="DdlUsuarioC" runat="server" AutoPostBack="true" EmptyMessage="Selecciona un usuario" Visible="False" Width="400px">
                                    </telerik:RadComboBox>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>
                                    <%--<asp:Button ID="BtnAcptar" runat="server" CssClass="Botones" Text="Asignar" Visible="False" />--%>
                                    <telerik:RadButton ID="BtnAcptar" runat="server" CssClass="Botones" Text="Cargar" Visible="False" />
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>&nbsp; <%--<asp:Label ID="LblMensaje" runat="server" Text="" CssClass="LblDesc"></asp:Label>--%>
                    <telerik:RadLabel ID="LblMensaje" runat="server" Text="">
                    </telerik:RadLabel>
                </td>
            </tr>
            <tr>
                <td align="Left">
                    <telerik:RadGrid ID="GvCargaAsignacion2" runat="server"  Width="600px" Visible="false">
                    </telerik:RadGrid>
                    <br />
                </td>
            </tr>

        </table>
         

        
    </telerik:RadAjaxPanel>
    <%--</div>--%>
    <asp:HiddenField ID="HidenUrs" runat="server" />
    <telerik:RadWindowManager ID="WinMsj" runat="server" ></telerik:RadWindowManager>
</asp:Content>

