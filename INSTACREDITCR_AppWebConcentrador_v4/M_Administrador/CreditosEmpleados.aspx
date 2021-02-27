<%@ Page Title="" Language="VB" MasterPageFile="~/M_Administrador/MasterPage.master" AutoEventWireup="false" CodeFile="CreditosEmpleados.aspx.vb" Inherits="M_Administrador_CreditosEmpleados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHMaster" runat="Server">

    <telerik:RadAjaxPanel ID="UPNLGeneral" runat="server">
        <div class="Titulos">Carga De Creditos De Empleados</div>
       
            <div class="container">
                
                <div class="w-100">
                    Layout para carga de Creditos de Empleados. El archivo debe ser CSV o TXT con encabezados.
                </div>
                
                <div class="table-responsive">
                    <asp:panel runat="server" ID="PnlCredifiel" Visible="true">
                    <table class="table">
                        <tr>
                            <th>Etiqueta Layout</th>
                            <td>Id De Empleado (Credito)*</td>
                            <td>Nombre Del Exempleado*</td>

                            <td>Fecha De Ingreso</td>
                            <td>Fecha De Baja</td>
                            <td>Monto Credito</td>
                            <td>Plazo</td>
                            <td>Valor Descuento</td>
                            <td>Pagare*</td>
                            <td>Frecuencia</td>
                            <td>Saldo Pagado</td>
                            <td>Saldo Actual*</td>
                            <td>Saldo Vencido*</td>
                            <td>Fecha Incumplimiento</td>
                            <td>Expediente</td>
                            <td>Juzgado</td>
                        </tr>
                        <tr>
                            <th>Descripcion Layout</th>
                            <td>Hasta 25 caracteres</td>
                            <td>Hasta 50 caracteres</td>

                            <td>Fecha (dd/mm/yyyy)</td>
                            <td>Fecha (dd/mm/yyyy)</td>
                            <td>Decimal</td>
                            <td>Hasta 50 caracteres</td>
                            <td>Hasta 50 caracteres</td>
                            <td>Decimal</td>
                            <td>Hasta 50 caracteres</td>
                            <td>Decimal</td>
                            <td>Decimal</td>
                            <td>Decimal</td>
                            <td>Fecha (dd/mm/yyyy)</td>
                            <td>9 caracteres</td>
                            <td>Hasta 700 caracteres</td>
                            </tr>
                    </table>
                    </asp:panel>
                   
                </div>
           
                <div class="d-flex justify-content-center mt-2">
                    <h4>Subir archivo CSV, TXT o UNL</h4>
                </div>
                <div class="d-flex justify-content-center mt-2">
                    <label>Separador:</label><br />
                    <telerik:RadDropDownList runat="server" ID="DDLSeparador" DefaultMessage="Seleccione" AutoPostBack="false">
                        <Items>
                            <telerik:DropDownListItem Text="Tabulador" Value="0" />
                            <telerik:DropDownListItem Text="Coma"  Selected="true" Value="1" />
                            <telerik:DropDownListItem Text="Pipe" Value="2" />
                        </Items>
                    </telerik:RadDropDownList>
                </div>
                <div class="d-flex justify-content-center mt-2">
                    <telerik:RadAsyncUpload ID="RadAsyncUpload1" runat="server" AllowedFileExtensions=".CSV,.txt,.unl" MultipleFileSelection="Disabled" OnClientFileUploading="focusDown">
                    </telerik:RadAsyncUpload>
                </div>
                <div class="d-flex justify-content-center mt-2">
                    <telerik:RadButton ID="BtnCargar" runat="server" Text="Cargar" SingleClick="true" SingleClickText="Procesando..." OnClientClicking="focusDown" />
                </div>
                <div class="d-flex justify-content-center my-2">
                    <telerik:RadProgressManager ID="RadProgressManager1" runat="server" />
                    <telerik:RadProgressArea RenderMode="Lightweight" ID="RadProgressArea1" runat="server" Width="50%" />
                </div>
                <div class="d-flex justify-content-center mt-2">
                    <asp:Label ID="LblMensaje" runat="server"></asp:Label>
                </div>
                <div class="d-flex justify-content-center my-2">
                    <telerik:RadGrid ID="GvCargaAsignacion" runat="server" Visible="false"></telerik:RadGrid>
                </div>
            </div>

    </telerik:RadAjaxPanel>
    <telerik:RadWindowManager ID="RadAviso" runat="server">
        <Localization OK="Aceptar" />
    </telerik:RadWindowManager>
    <asp:HiddenField ID="HidenUrs" runat="server" />

    <!-- Truco para ir hasta abajo de la pagina ;) -->
    <div id="down"></div>
    <script>
        focusDown = () => {
            $([document.documentElement, document.body]).animate({
                scrollTop: $("#down").offset().top
            }, 1000);
        }

    </script>
</asp:Content>

