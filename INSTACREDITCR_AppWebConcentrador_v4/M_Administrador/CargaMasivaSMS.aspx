<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CargaMasivaSMS.aspx.vb" MasterPageFile="~/M_Administrador/MasterPage.master" Inherits="M_Administrador_CargaMasivaSMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHMaster" runat="Server">
      <telerik:RadAjaxPanel ID="UPNLGeneral" runat="server">
        <div class="Titulos">Carga Masiva SMS</div>
          <label>Producto:</label>
            <telerik:RadDropDownList runat="server" ID="DDLProducto" DefaultMessage="Seleccione" AutoPostBack="true">
                </telerik:RadDropDownList>
        <div class="container">
            
            <div class="w-100">
                Layout para carga de cartera. El archivo debe ser CSV o TXT con encabezados.
            </div>
            
            <div class="table-responsive">
                <table class="table">
                    <tr>
                        <th>Campo</th>
                        <td>Telefono Celular</td>
                        <td>Credito</td>
                        <td>Plantilla</td> 
                        <td>Estatus</td>                    
                    </tr>
                    <tr>
                        <th>Descripcion</th>
                        <td>Hasta 10 caracteres</td>
                        <td>Hasta 20 caracteres</td>
                        <td>Hasta 50 caracteres</td>                        
                    </tr>
                </table>
            </div>
            <div class="d-flex justify-content-center mt-2">
                <h4>Subir archivo CSV o TXT</h4>
            </div>
            <div class="d-flex justify-content-center mt-2">
                <label>Separador:</label><br />
                <telerik:RadDropDownList runat="server" ID="DDLSeparador" DefaultMessage="Seleccione" AutoPostBack="false">
                    <Items>
                        <telerik:DropDownListItem Text="Tabulador" Value="0" Selected="true" />
                        <telerik:DropDownListItem Text="Coma" Value="1" />
                    </Items>
                </telerik:RadDropDownList>
            </div>
            <div class="d-flex justify-content-center mt-2">
                <telerik:RadAsyncUpload ID="RadAsyncUpload1" runat="server" AllowedFileExtensions=".CSV,.txt" MultipleFileSelection="Disabled" OnClientFileUploading="focusDown">
                </telerik:RadAsyncUpload>
            </div>
            <div class="d-flex justify-content-center mt-2">
                 <telerik:RadButton ID="BtnPre" runat="server" Text="Preview" SingleClick="true" SingleClickText="Procesando..." OnClientClicking="focusDown"/>
                <telerik:RadButton ID="BtnCargar" runat="server" Text="Enviar" SingleClick="true" SingleClickText="Procesando..." OnClientClicking="focusDown" enabled="false"/>
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
          <telerik:RadGrid ID="Grid_RESULTADO" runat="server"  style="width:50%" >
              <MasterTableView></MasterTableView>
                </telerik:RadGrid>
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
