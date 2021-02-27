<%@ Page Title="" Language="VB" MasterPageFile="./MasterPage.master" AutoEventWireup="false" CodeFile="CargaPagos.aspx.vb" Inherits="MAdministrador_CargaPagos" %>


<asp:Content ID="Content1" ContentPlaceHolderID="CPHMaster" runat="Server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="UPNLGeneral" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <div class="Titulos">Carga Pagos</div>

        <div class="container">
            <div class="d-flex justify-content-center mt-2">
            <telerik:RadLabel runat="server" ID="lbl" Text="Archivo"></telerik:RadLabel>
            <telerik:RadAsyncUpload ID="RadAsyncUpload1" runat="server" AllowedFileExtensions=".CSV,.txt">
            </telerik:RadAsyncUpload>

            &nbsp;
            <asp:Label ID="LblDelimitador" runat="server" Text="Delimitador" CssClass="LblDesc"></asp:Label>
            <telerik:RadDropDownList ID="DdlDelimitador" runat="server" DefaultMessage="Seleccione" AutoPostBack="false">
                <Items>
                    <telerik:DropDownListItem Text="Seleccione" Value="0" Selected="true" />
                    <telerik:DropDownListItem Text="Tabulador" Value="1" />
                    <telerik:DropDownListItem Text="Coma" Value="2" />
                </Items>
            </telerik:RadDropDownList>
            <telerik:RadButton ID="BtnCargar" runat="server" CssClass="Botones" Text="Cargar" />
                       </div>
            <div class="w-100">
                Layout para carga de cartera. El archivo debe ser CSV o TXT con encabezados.
            </div>

            <div class="table-responsive">
                <asp:panel runat="server" ID="PnlCredifiel" Visible="true">
                    <table class="table" runat="server">
                        <tr>
                            <th>Campo</th>
                            <td>ID PAGO </td>
                            <td>FECHA</td>
                            <td>CUENTA</td>
                            <td>CONTO 1</td>
                            <td>CONTO 2</td>
                            <td>CONTO 3</td>
                            <td>REFERENCIA PANET</td>
                            <td>DV</td>
                            <td>IMPORTE</td>
                            <td>RFC</td>
                            <td>DAP</td>
                            <td>FOLIO</td>
                            <td>CREDITO</td>
                            <td>NOMBRE</td>
                            <td>PAGO CERO 360</td>
                            <td>PAGO CERO INDICADORES</td>
                            <td>PRODUCTO </td>
                            <td>DEPENDENCIA</td>
                            <td>LINEA CREDITO</td>
                            <td>ESTATUS CREDITO</td>
                            <td>TIPO PAGO</td>
                            <td>TIPO PAGO EXTJ</td>
                            <td>OBSERVACIONES</td>
                            <td>SUB ESTATUS</td>
                            <td>ASIGNACION</td>
                            <td>DETALLE</td>
                            <td>CAMPANA</td>
                            <td>CAMPANA AVANCES</td>
                            <td>EJECUTIVO</td>
                            <td>TURNO</td>
                            <td>JEFE INMEDIATO</td>
                            <td>TIPO GESTION</td>
                            <td>REASIGNAR PAGO</td>
                            <td>OBSERVACION</td>
                            <td>BANCO</td>
                            <td>HOMOLOGADA </td>
                            <td>ESTADO DEPENDENCIA </td>
                            <td>SUCURSAL</td>
                            <td>BASE EXTRAJUDICIAL</td>

                        </tr>
                        <tr>
                            <th>Descripcion</th>
                            <td>Hasta 25 Caracteres</td>
                            <td>Fecha con Formato DD/MM/AAAA</td>
                            <td>Hasta 25 Caracteres</td>
                            <td>Hasta 300 Caracteres</td>
                            <td>Hasta 300 Caracteres</td>
                            <td>Hasta 300 Caracteres</td>
                            <td>Hasta 25 Caracteres</td>
                            <td>Hasta 19 Numeros</td>
                            <td>Hasta 19 Numeros</td>
                            <td>Hasta 25 Caracteres</td>
                            <td>Hasta 25 Caracteres</td>
                            <td>Hasta 25 Caracteres</td>
                            <td>Hasta 25 Caracteres</td>
                            <td>Hasta 100 Caracteres</td>
                            <td>Hasta 25 Caracteres</td>
                            <td>Hasta 25 Caracteres</td>
                            <td>Hasta 25 Caracteres</td>
                            <td>Hasta 25 Caracteres</td>
                            <td>Hasta 25 Caracteres</td>
                            <td>Hasta 25 Caracteres</td>
                            <td>Hasta 25 Caracteres</td>
                            <td>Hasta 25 Caracteres</td>
                            <td>Hasta 25 Caracteres</td>
                            <td>Hasta 25 Caracteres</td>
                            <td>Hasta 25 Caracteres</td>
                            <td>Hasta 25 Caracteres</td>
                            <td>Hasta 25 Caracteres</td>
                            <td>Hasta 25 Caracteres</td>
                            <td>Hasta 25 Caracteres</td>
                            <td>Hasta 25 Caracteres</td>
                            <td>Hasta 25 Caracteres</td>
                            <td>Hasta 25 Caracteres</td>
                            <td>Hasta 25 Caracteres</td>
                            <td>Hasta 25 Caracteres</td>
                            <td>Hasta 25 Caracteres</td>
                            <td>Hasta 25 Caracteres</td>
                            <td>Hasta 25 Caracteres</td>
                            <td>Hasta 25 Caracteres</td>
                            <td>Hasta 25 Caracteres</td>
                        </tr>
                    </table>
                </asp:panel>

            </div>

            <telerik:RadGrid ID="GvCargaAsignacion" runat="server" Width="251px" Visible="false">

            </telerik:RadGrid>

            <telerik:RadButton ID="LnkLog" runat="server" Visible="false" Text="Archivo Log" ButtonType="LinkButton" />
            <br />
            <telerik:RadButton ID="LnkBad" runat="server" Visible="false" Text="Archivo Bad" ButtonType="LinkButton" />
            <br />
            <div class="d-flex justify-content-center mt-2">

                <asp:Label ID="LblMensaje" runat="server" Text="" CssClass="LblDesc"></asp:Label>
            </div>
        </div>
    </telerik:RadAjaxPanel>

    <asp:HiddenField ID="HidenUrs" runat="server" />

    <telerik:RadWindowManager ID="RadAviso" runat="server">
        <Localization OK="Aceptar" />
    </telerik:RadWindowManager>

</asp:Content>

