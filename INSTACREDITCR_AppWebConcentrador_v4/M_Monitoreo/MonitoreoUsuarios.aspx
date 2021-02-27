<%@ Page Title="" Language="VB" MasterPageFile="~/M_Monitoreo/MasterPage.master" AutoEventWireup="false" CodeFile="MonitoreoUsuarios.aspx.vb" Inherits="M_Monitoreo_MonitoreoUsuarios" UICulture="es-MX" Culture="es-MX" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Monitoreo Usuarios</title>
    <link href="styles/General.css" rel="stylesheet" />
    <script type='text/javascript' src='https://maps.googleapis.com/maps/api/js?key=AIzaSyB4XOxMMENmanihA9hOqfPVAcWPR3eUW3I'></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadFormDecorator ID="FormDecorator1" runat="server" DecoratedControls="all" DecorationZoneID="optionsDecoration"></telerik:RadFormDecorator>
    <div class="container" id="optionsDecoration">
        <h3>Monitoreo De Usarios</h3>
        <fieldset>
            <legend>Configuraci&#243;n</legend>
            <div class="w3-row-padding">
                <div class="w3-col m12 l3">
                    <div class="custom-control custom-switch">
                        <input type="checkbox" class="custom-control-input" id="switch1" name="mostrarNombres" />
                        <label class="custom-control-label" for="switch1">Mostrar Nombres</label>
                    </div>
                </div>
                <div class="w3-col m12 l3">
                    <div class="custom-control custom-switch">
                        <input type="checkbox" class="custom-control-input disabled" id="CBSimular" name="simularRuta" />
                        <label class="custom-control-label" for="CBSimular">
                            Simular Ruta
                                <br />
                            (Solo al Mostrar Ruta)</label>
                    </div>
                </div>
                <div class="w3-col m12 l3">
                    <asp:Label runat="server" Text="Usuario"></asp:Label><br />
                    <telerik:RadComboBox DropDownAutoWidth="Enabled" CheckBoxes="true" ID="RcbCat_lo_usuario" runat="server" EmptyMessage="Seleccione Un Usuario" Width="100%" EnableCheckAllItemsCheckBox="true" AutoPostBack="false" OnClientItemChecked="comboCheckedChanged" OnClientCheckAllChecked="comboCheckedChanged">
                        <Localization AllItemsCheckedString="Todos Los Elementos Seleccionados" CheckAllString="Todos" />
                    </telerik:RadComboBox>
                </div>
                <div id="TRFecha" class="w3-col m12 l3">
                    <asp:Label runat="server" Text="Fecha Ruta"></asp:Label>
                    <telerik:RadDatePicker ID="DPRuta" runat="server" AutoPostBack="false" Width="100%" Enabled="false">
                        <ClientEvents OnDateSelected="selectedDate" />
                    </telerik:RadDatePicker>
                </div>
            </div>

            <div id="PnlBuscar" class="d-flex justify-content-center mt-2">
                <telerik:RadButton ID="RadBtnObtener" runat="server" Text="Comenzar Monitoreo" AutoPostBack="false" Width="175px" OnClientClicked="Obtener" CssClass="bg-success border-0 text-white mx-2" Enabled="false">
                </telerik:RadButton>
                <telerik:RadButton ID="BtnRuta" runat="server" Text="Mostrar Ruta" AutoPostBack="false" Width="175px" OnClientClicked="Obtener3" Enabled="false" CssClass="bg-primary border-0 text-white mx-2">
                </telerik:RadButton>
            </div>

        </fieldset>

    </div>
    <div class="container w3-hide mt-2" id="Monitoreo">
        <div class="row">
            <div class="col-md-4 invisible" id ="InfoUser" style="max-height:500px;overflow:auto;z-index:8000">
            </div>
            <div class="col-md-12" id="DivMapa">
                <div id="map" style="height: 500px;" class="w-100 shadow"></div>
            </div>
        </div>
        
    </div>
    <%--<telerik:RadToolTip runat="server" ID="TTCartera" ShowEvent="OnMouseOver" TargetControlID="BtnCartera" Animation="Slide" Visible="true">
                <p>Selecciona una agrupación</p>
            </telerik:RadToolTip>--%>
    <div class="modal fade" id="MdlEspere" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header w3-center">
                    <h4 class="modal-title">Procesando...</h4>
                </div>
                <div class="modal-body" style="text-align: center">
                    <div class="spinner-border text-warning"></div>
                </div>
            </div>
        </div>
    </div>
    <div id="MdlMsj" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Aviso</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <p><span id="LblMsj"></span></p>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="HidenUrs" runat="server" />
    <telerik:RadScriptBlock runat="server" ID="jsBlock">
        <script>
            var RcbCat_lo_usuario
            var dtPicker
            var btnObtener
            var btnRuta

            Sys.Application.add_load(loadPage)

            function loadPage() {
                RcbCat_lo_usuario = $find("<%=RcbCat_lo_usuario.ClientID %>");
                dtPicker = $find("<%=DPRuta.ClientID %>");
                btnObtener = $find("<%=RadBtnObtener.ClientID %>");
                btnRuta = $find("<%=BtnRuta.ClientID %>");
            }

        </script>
    </telerik:RadScriptBlock>
    <script type='text/javascript' src='./scripts/MonitoreoUsuarios.min.js?V=1.4'></script>
</asp:Content>
