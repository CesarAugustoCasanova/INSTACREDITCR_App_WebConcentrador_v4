<%@ Page Title="" Language="VB" MasterPageFile="./MasterPage.master" AutoEventWireup="false" CodeFile="CargaActSaldos.aspx.vb" Inherits="CargaActSaldos" %>


<asp:Content ID="Content1" ContentPlaceHolderID="CPHMaster" runat="Server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" ></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="UPNLGeneral" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <table class="Table" align="Center">
            <tr>
                <td class="Titulos">
                    <telerik:RadLabel ID="LblTitulo" runat="server" Text="Carga Actualizacion De Saldos"></telerik:RadLabel>
                    <%--<telerik:RadLabel ID="LblTitulo" runat="server" Text="Actualización de cartera"></telerik:RadLabel>--%>
                </td>
            </tr>
            <tr align="Center">
                <td>
                    <telerik:RadLabel ID="LblMenInfor" runat="server" Text="" CssClass="LblDesc" Visible="false" ForeColor="Red"></telerik:RadLabel>
                </td>
            </tr>
            <tr>
                <td>1.- 
            <telerik:RadLabel ID="LblProducto" runat="server" Text="Producto" CssClass="LblDesc" ></telerik:RadLabel>
                    <telerik:RadDropDownList ID="DdlProducto" runat="server" CssClass="DdlDesc" AutoPostBack="true">
                        <Items>
                            <telerik:DropDownListItem Selected="True" Text="Seleccione" />
                            <telerik:DropDownListItem Value="AZUL" Text="Azul" />
                            <telerik:DropDownListItem Value="NARANJA" Text="Naranja" />
                            <telerik:DropDownListItem Value="ROJO" Text="Rojo" />
                        </Items>
                    </telerik:RadDropDownList>

                </td>
            </tr>

            <%--<asp:UpdatePanel ID="UP_Carga" runat="server">
            <ContentTemplate>--%>
            <tr>
                <td>&nbsp;
                   <telerik:RadLabel runat="server" ID="lbl" Text="Archivo"></telerik:RadLabel>  <telerik:RadAsyncUpload ID="RadAsyncUpload1" runat="server" AllowedFileExtensions=".CSV,.txt"></telerik:RadAsyncUpload>
                    <%--<asp:AsyncFileUpload ID="AsyncFileUpload1" runat="server" FailedValidation="False" OnClientUploadComplete="uploadComplete" OnClientUploadError="uploadError" ThrobberID="imgcarga" UploadingBackColor="#CCFFFF" Visible="True" Width="400px" CompleteBackColor="Green" UploaderStyle="Modern" />--%>
                &nbsp;
                        

                        &nbsp;
                        &nbsp;
                        
                </td>
            </tr>
            <tr>
                <td>

                    <telerik:RadLabel ID="LblDelimitador" runat="server" Text="Delimitador" CssClass="LblDesc"></telerik:RadLabel>
                    &nbsp;
              
                <asp:DropDownList ID="DdlDelimitador" runat="server" CssClass="DdlDesc">
                </asp:DropDownList>
                    &nbsp;
            &nbsp;
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                        <telerik:RadLabel ID="LblMensaje" runat="server" Text="" CssClass="LblDesc"></telerik:RadLabel>
                </td>
            </tr>
            <tr aling="Center">
                <td>
                    <telerik:RadLabel ID="LblLayOut" runat="server" Text="" Font-Size="XX-Small" CssClass="LblDesc"></telerik:RadLabel>
                </td>
            </tr>
            <tr>
                <td>
                    <telerik:RadButton ID="BtnCargar" runat="server" CssClass="Btn_Aceptar" Text="Cargar"  />
                    <asp:Panel ID="PnlLayout" runat="server">
                    </asp:Panel>

                    <telerik:RadToolTip ID="RadToolTip1" runat="server" RelativeTo="Element"  TargetControlID="BtnCargar" Animation="Resize" HideEvent="LeaveTargetAndToolTip">
                        <table width="100%">
                            <tr align="center">
                                <td class="Titulo" colspan="14">Layout actualización saldos</td>
                            </tr>
                            <tr align="center">

                                <td>
                                    <strong>Campos:</strong>
                                </td>
                                <td>Número De Crédito</td>
                                <td>Estatus</td>
                                <td>Saldo total</td>
                                <td>Saldo vencido</td>
                                <td>Late Fee</td>
                                <td>Número LateFee</td>
                                <td>SV + LF</td>
                                <td>Bucket</td>
                                <td>Fondos de contingencia</td>
                                <td>Saldo total + Fondos</td>
                                <td>Fecha de ultimo pago</td>
                                <td>Monto de ultimo pago</td>
                                <td>Producto</td>
                                <td>Campaña</td>
                            </tr>
                            <tr align="center">
                                <td>
                                    <strong>Observaciones:</strong>
                                </td>
                                <td>25 Caracteres</td>
                                <td>1 Caracteres</td>
                                <td>22 Caracteres</td>
                                <td>22 Caracteres</td>
                                <td>22 Caracteres</td>
                                <td>22 Caracteres</td>
                                <td>22 Caracteres</td>
                                <td>15 Caracteres</td>
                                <td>22 Caracteres</td>
                                <td>22 Caracteres</td>
                                <td>25 Caracteres</td>
                                <td>22 Caracteres</td>
                                <td>25 Caracteres</td>
                                <td>25 Caracteres</td>

                            </tr>

                        </table>
                    </telerik:RadToolTip>
                    <%--<asp:BalloonPopupExtender BalloonSize="Large" ID="BtnAyuda_BalloonPopup" runat="server"
                    DisplayOnMouseOver="true" CustomCssUrl="" Enabled="True"
                    ExtenderControlID="BtnCargar" TargetControlID="BtnCargar"
                    BalloonPopupControlID="PnlLayout" UseShadow="true" BalloonStyle="Cloud" Position="BottomRight">
                </asp:BalloonPopupExtender>
                <asp:Panel ID="PnlLayout" runat="server">
                    <table width="100%">
                        <tr align="center">
                            <td class="Titulo" colspan="14">Layout actualización saldos</td>
                        </tr>
                        <tr align="center">

                            <td>
                                <strong>Campos:</strong>
                            </td>
                            <td>Número De Crédito</td>
                            <td>Estatus</td>
                            <td>Saldo total</td>
                            <td>Saldo vencido</td>
                            <td>Late Fee</td>
                            <td>Número LateFee</td>
                            <td>SV + LF</td>
                            <td>Bucket</td>
                            <td>Fondos de contingencia</td>
                            <td>Saldo total + Fondos</td>
                            <td>Fecha de ultimo pago</td>
                            <td>Monto de ultimo pago</td>
                            <td>Producto</td>
                            <td>Campaña</td>
                        </tr>
                        <tr align="center">
                            <td>
                                <strong>Observaciones:</strong>
                            </td>
                            <td>25 Caracteres</td>
                            <td>1 Caracteres</td>
                            <td>22 Caracteres</td>
                            <td>22 Caracteres</td>
                            <td>22 Caracteres</td>
                            <td>22 Caracteres</td>
                            <td>22 Caracteres</td>
                            <td>15 Caracteres</td>
                            <td>22 Caracteres</td>
                            <td>22 Caracteres</td>
                            <td>25 Caracteres</td>
                            <td>22 Caracteres</td>
                            <td>25 Caracteres</td>
                            <td>25 Caracteres</td>

                        </tr>

                    </table>
                </asp:Panel>--%>
                </td>
            </tr>
            <tr>
                <td></td>
            </tr>

            <tr>
                <td align="Center">
                    <telerik:RadGrid ID="GvCargaAsignacion" runat="server"  Width="251px" Visible="false">
                    </telerik:RadGrid>

                </td>
            </tr>
            <%--</ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="LnkLog" />
                <asp:PostBackTrigger ControlID="LnkBad" />
                <asp:PostBackTrigger ControlID="BtnCargar" />
            </Triggers>
        </asp:UpdatePanel>--%>
        </table>

    </telerik:RadAjaxPanel>
    <asp:HiddenField ID="HidenUrs" runat="server" />


    <telerik:RadWindowManager ID="RadAviso" runat="server" >
        <Localization OK="Aceptar" />
    </telerik:RadWindowManager>
</asp:Content>

