<%@ Page Title="" Language="VB" MasterPageFile="./MasterPage.master" AutoEventWireup="false" CodeFile="CatalogoPoliticas.aspx.vb" Inherits="CatalogoPoliticas" %>


<asp:Content ID="CCatalogos" ContentPlaceHolderID="CPHMaster" runat="Server">
    <script type="text/javascript">

        function confirmCallbackFn(arg) {
            if (arg) {

                __doPostBack('ctl00$CPHMaster$BtnAceptarConfirmacion', '')

            }
        }

        verifyRange = e => {
            console.log(e)
            console.log(e.value)
            if (e.value < 0) e.value = 0
            if (e.value > 99) e.value = 99
        }
    </script>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="UPNLCatPoliticas" runat="server" UpdatePanelCssClass="w3-center" CssClass="container" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">
        <div class="container mb-2 text-center">
            <h1>Cat&aacute;logo De Pol&iacute;ticas</h1>
            <p>
                Cambia los est&aacute;ndares de seguridad de la contraseña. Al presionar el bot&oacute;n de guardar, todos los usuarios tendr&aacute;n que cambiar su contraseña en su pr&oacute;ximo inicio de sesi&oacute;n para validar que su contraseña cumple con los est&aacute;ndares de seguridad m&aacute;s recientes.
            </p>
        </div>
        <div class="row">
            <div class="col-md-12 col-lg-6">
                <div class="input-group mb-3">
                    <div class="input-group-prepend w-50">
                        <span class="input-group-text w-100">Longitud Mínima</span>
                    </div>
                    <asp:TextBox runat="server" CssClass="form-control" placeholder="Longitud Mínima" ID="TXTCAT_DESC_LONG_MIN" TextMode="Number" onchange="verifyRange(this)"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-12 col-lg-6">
                <div class="input-group mb-3">
                    <div class="input-group-prepend w-50">
                        <span class="input-group-text w-100">Longitud Máxima</span>
                    </div>
                    <asp:TextBox runat="server" CssClass="form-control" placeholder="Longitud Máxima" ID="TXTCAT_DESC_LONG_MAX" TextMode="Number" onchange="verifyRange(this)"></asp:TextBox>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12 col-lg-6">
                <div class="input-group mb-3">
                    <div class="input-group-prepend w-50">
                        <span class="input-group-text w-100">Cuantas Mayusculas</span>
                    </div>
                    <asp:TextBox runat="server" CssClass="form-control" placeholder="Cuantas Mayusculas" ID="TXTCAT_DESC_MAYUSC" TextMode="Number" onchange="verifyRange(this)"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-12 col-lg-6">
                <div class="input-group mb-3">
                    <div class="input-group-prepend w-50">
                        <span class="input-group-text w-100">Cuantas Minusculas</span>
                    </div>
                    <asp:TextBox runat="server" CssClass="form-control" placeholder="Cuantas Minusculas" ID="TXTCAT_DESC_MINUSC" TextMode="Number" onchange="verifyRange(this)"></asp:TextBox>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12 col-lg-6">
                <div class="input-group mb-3">
                    <div class="input-group-prepend w-50">
                        <span class="input-group-text w-100">Cuantos Números</span>
                    </div>
                    <asp:TextBox runat="server" CssClass="form-control" placeholder="Cuantos Números" ID="TXTCAT_DESC_NUMEROS" TextMode="Number" onchange="verifyRange(this)"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-12 col-lg-6">
                <div class="input-group mb-3">
                    <div class="input-group-prepend w-50">
                        <span class="input-group-text w-100">Cuantos Caracteres Especiales</span>
                    </div>
                    <asp:TextBox runat="server" CssClass="form-control" placeholder="Caracteres Especiales" ID="TXTCAT_DESC_ESPECIALES" TextMode="Number" onchange="verifyRange(this)"></asp:TextBox>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12 col-lg-6">
                <div class="input-group mb-3">
                    <div class="input-group-prepend w-50">
                        <span class="input-group-text w-100">Historial De Contraseñas</span>
                    </div>
                    <asp:TextBox runat="server" CssClass="form-control" placeholder="Historial De Contraseñas" ID="TXTCAT_DESC_HISTORIA" TextMode="Number" onchange="verifyRange(this)"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-12 col-lg-6">
                <div class="input-group mb-3">
                    <div class="input-group-prepend w-50">
                        <span class="input-group-text w-100">Dias Vigencia De La Contraseña</span>
                    </div>
                    <asp:TextBox runat="server" CssClass="form-control" placeholder="Días" ID="TXTCAT_DESC_VIGENCIA" TextMode="Number" onchange="verifyRange(this)"></asp:TextBox>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12 col-lg-6">
                <div class="input-group mb-3">
                    <div class="input-group-prepend w-50">
                        <span class="input-group-text w-100">Cancelar Por Dias Inactividad</span>
                    </div>
                    <asp:TextBox runat="server" CssClass="form-control" placeholder="Días" ID="TXTCAT_DESC_INACTIVIDAD" TextMode="Number" onchange="verifyRange(this)"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-12 col-lg-6">
                <div class="input-group mb-3">
                    <div class="input-group-prepend w-50">
                        <span class="input-group-text w-100">Intentos de conexión</span>
                    </div>
                    <asp:TextBox runat="server" CssClass="form-control" placeholder="Longitud Máxima" ID="TXTCAT_DESC_INTENTOS" TextMode="Number" onchange="verifyRange(this)"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="container">
            <div class="w-50 mx-auto">

                <asp:Button ID="BtnAceptar" runat="server" Text="Guardar" CssClass="btn w3-block btn-success" />
            </div>
        </div>
        <asp:Button runat="server" ID="BtnAceptarConfirmacion" Visible="true" Height="1px" Style="display: none;" />

        <telerik:RadWindowManager ID="RadAviso" runat="server"></telerik:RadWindowManager>

        <asp:HiddenField ID="HidenUrs" runat="server" />
    </telerik:RadAjaxPanel>
</asp:Content>
