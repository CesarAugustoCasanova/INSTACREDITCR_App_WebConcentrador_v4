<%@ Page Title="" Language="VB" MasterPageFile="~/M_Administrador/MasterPage.master" AutoEventWireup="false" CodeFile="CargaMasivaGestiones.aspx.vb" Inherits="M_Administrador_CargaMasivaGestiones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMaster" runat="Server">
    <telerik:RadAjaxPanel ID="UPNLGeneral" runat="server">
        <div class="Titulos">Carga Masiva Gestiones</div>
        <div class="container">

            <div class="d-flex justify-content-center">
                <label>Tipo:</label><br />
                <telerik:RadDropDownList runat="server" ID="DDLTipo" DefaultMessage="Seleccione" AutoPostBack="true">
                    <Items>
                        <telerik:DropDownListItem Text="Seleccione" Value="0" Selected="true" />
                        <telerik:DropDownListItem Text="Gestiones" Value="1" />
                        <%--<telerik:DropDownListItem Text="Visitas" Value="2" />--%>
                    <%--    <telerik:DropDownListItem Text="Asignacion" Value="3" />--%>
                    </Items>
                </telerik:RadDropDownList>
                <label>Separador:</label><br />
                <telerik:RadDropDownList runat="server" ID="DDLSeparador" DefaultMessage="Seleccione" AutoPostBack="false">
                    <Items>
                        <telerik:DropDownListItem Text="Seleccione" Value="0" Selected="true" />
                        <telerik:DropDownListItem Text="Tabulador" Value="1" />
                        <telerik:DropDownListItem Text="Coma" Value="2" />
                    </Items>
                </telerik:RadDropDownList>
                <label>Archivo:</label><br />
                <telerik:RadAsyncUpload ID="RadAsyncUpload1" runat="server" AllowedFileExtensions=".CSV,.txt" MultipleFileSelection="Disabled" OnClientFileUploading="focusDown">
                </telerik:RadAsyncUpload>

            </div>
             <div class="d-flex justify-content-center">
                <br />
                 <telerik:RadButton ID="BtnPreview" runat="server" Text="Preview" SingleClick="true" SingleClickText="Procesando..." OnClientClicking="focusDown" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 <telerik:RadButton ID="BtnCargar" runat="server" Text="Cargar" SingleClick="true" SingleClickText="Procesando..." OnClientClicking="focusDown" enabled="false"/>
                 </div>
            <br />
            <div class="w-100">
                El archivo debe ser .Csv o .Txt con encabezados.
            </div>
            <div class="table-responsive">
                <table class="table" runat="server" id="LOGestion" visible="false">
                    <tr>
                        <th>Campo</th>
                        <td>Número De Crédito:</td>
                        <td>Usuario:</td>
                        <td>Códigos: <br /> Accion "|" Resultado </td>
                        <td>Comentario:</td>
                        <td>Fecha De Actividad:</td>
                        
                    </tr>
                    <tr>
                        <th>Descripcion</th>
                        <td>Hasta 25 Caracteres</td>
                        <td>Hasta 25 Caracteres</td>
                        <td>Hasta 10 Caracteres</td>
                        <td>Hasta 500 Caracteres</td>
                        <td>DD/MM/AAAA</td>
                        
                    </tr>
                </table>

                <table class="table" runat="server" id="LOVisitas" visible="false">
                    <tr>
                        <th>Campo</th>
                        <td>Número De Crédito:</td>
                        <td>Visitador:</td>
                        <td>Capturista:</td>
                        <td>Fecha Visita:</td>
                        <td>Fecha Captura:</td>
                        <td>Código:</td>
                        <td>Comentario:</td>
                        <td>Parentesco:</td>
                        <td>Nombre Parentesco:</td>
                        <td>Tipo De Domicilio:</td>
                        <td>Nivel Socioeconómico:</td>
                        <td>Número de Niveles:</td>
                        <td>Caracteristicas:</td>
                        <td>Color Fachada:</td>
                        <td>Color Puerta:</td>
                        <td>Horario De Contacto:</td>
                        <td>Dias De Contacto:</td>
                        <td>Punto De Referencia</td>
                        <td>Entre Calle 1:</td>
                        <td>Entre Calle 2:</td>
                    </tr>
                    <tr>
                        <th>Descripcion</th>
                        <td>Hasta 25 Caracteres</td>
                        <td>Hasta 25 Caracteres</td>
                        <td>Hasta 25 Caracteres</td>
                        <td>Hasta 19 Caracteres Ejemplo (25/07/2014 15:30:00)</td>
                        <td>Hasta 19 Caracteres Ejemplo (25/07/2014 15:30:00)</td>
                        <td>Hasta 6 Caracteres</td>
                        <td>Hasta 500 Caracteres</td>
                        <td>Hasta 50 Caracteres Ejemplo(Cliente ,Conyuge, Familiar)</td>
                        <td>Hasta 50 Caracteres</td>
                        <td>Ejemplo (Casa,Departamento,Otro)</td>
                        <td>Ejemplo (Alto,Medio,Bajo, Otro)</td>
                        <td>Hasta 2 Caracteres</td>
                        <td>Ejemplo (Propia,Rentada, Otro)</td>
                        <td>Ejemplo (Rojo)</td>
                        <td>Ejemplo (Azul)</td>
                        <td>Hasta 10 Caracteres Ejemplo (12:2112:21)</td>
                        <td>Hasta 7 Caracteres Ejemplo (1111100)</td>
                        <td>Hasta 200 Caracteres</td>
                        <td>Hasta 50 Caracteres</td>
                        <td>Hasta 50 Caracteres</td>
                    </tr>
                </table>

                <table class="table" runat="server" id="LOAsignacion" visible="false">
                    <tr>
                        <th>ejemplo</th>
                       
                    </tr>
                    <tr>
                        <th>Descripcion</th>
                        
                    </tr>
                </table>
            </div>
        </div>
        <div class="d-flex justify-content-center mt-2">
            <telerik:RadLabel runat="server" ID="LBLResultado" CssClass="w3-large"></telerik:RadLabel>
        </div>
        <telerik:RadGrid ID="Grid_RESULTADO" runat="server"  style="width:50%" >
              <MasterTableView></MasterTableView>
                </telerik:RadGrid>
        <div class="d-flex justify-content-center mt-2">
                <asp:Label ID="LblMensaje" runat="server"></asp:Label>
            </div>
        <div class="d-flex justify-content-center mt-2">
        <telerik:RadLabel id="LblReglasNegocio" runat="server" Font-Size="X-Small"> </telerik:RadLabel>    
        </div>
    </telerik:RadAjaxPanel>
    <div id="down"></div>
    <script>
        focusDown = () => {
            $([document.documentElement, document.body]).animate({
                scrollTop: $("#down").offset().top
            }, 1000);
        }

    </script>
</asp:Content>

