<%@ Page Title="" Language="VB" MasterPageFile="./MasterPage.master" AutoEventWireup="false" CodeFile="SMS.aspx.vb" Inherits="SMS" %>



<asp:Content ID="Content2" ContentPlaceHolderID="CPHMaster" runat="Server">
    <div class="container">
        <h2 class="text-center">CONFIGURACIÓN DE SMS</h2>
        <div class="row">
            <div class="col-md-6 p-1">
                <h4 class="text-center">Etiquetas</h4>
                <telerik:RadGrid ID="RGVEtiquetas" runat="server" RenderMode="Lightweight" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true"
                    ClientSettings-EnableRowHoverStyle="true" OnNeedDataSource="RGVEtiquetas_NeedDataSource" Culture="es-MX">
                    <MasterTableView Width="100%" CommandItemDisplay="Top" EditFormSettings-PopUpSettings-KeepInScreenBounds="true" DataKeyNames="Id" Caption="">
                        <CommandItemSettings AddNewRecordText="Nueva etiqueta" />
                        <Columns>
                            <telerik:GridEditCommandColumn ItemStyle-Width="5px" UniqueName="Expandible">
                            </telerik:GridEditCommandColumn>
                            <telerik:GridBoundColumn UniqueName="Etiqueta" HeaderText="Etiqueta" DataField="Etiqueta">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="Campo" HeaderText="Campo" DataField="Campo">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="Id" HeaderText="Id" DataField="Id" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="Tabla" HeaderText="Tabla" DataField="Tabla" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CampoReal" HeaderText="CampoReal" DataField="CampoReal" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete" HeaderText="Eliminar"></telerik:GridButtonColumn>
                        </Columns>
                        <EditFormSettings UserControlName="Etiquetas.ascx" EditFormType="WebUserControl">
                            <EditColumn UniqueName="EditCommandColumn1">
                            </EditColumn>
                        </EditFormSettings>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
            <div class="col-md-6 p-1">
                <h4 class="text-center">Plantillas</h4>
                <telerik:RadGrid ID="RGVPlantillas" runat="server" RenderMode="Lightweight" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true"
                    ClientSettings-EnableRowHoverStyle="true" OnNeedDataSource="RGVPlantillas_NeedDataSource" Culture="es-MX">
                    <MasterTableView Width="100%" CommandItemDisplay="Top" EditFormSettings-PopUpSettings-KeepInScreenBounds="true" DataKeyNames="Nombre" Caption="">
                        <CommandItemSettings AddNewRecordText="Nueva plantilla" />
                        <Columns>
                            <%-- <telerik:GridButtonColumn UniqueName="Editar" HeaderText="Editar" Text="Editar" CommandName="Editar" ButtonType="ImageButton" ImageUrl="~/M_Administrador/Imagenes/ImgModificar.png"></telerik:GridButtonColumn>--%>
                            <telerik:GridEditCommandColumn ItemStyle-Width="5px" UniqueName="Expandible">
                            </telerik:GridEditCommandColumn>
                            <telerik:GridBoundColumn UniqueName="Nombre" HeaderText="Nombre" DataField="Nombre">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="Mensaje" HeaderText="Mensaje" DataField="Mensaje">
                            </telerik:GridBoundColumn>
                            <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete" HeaderText="Eliminar"></telerik:GridButtonColumn>
                        </Columns>
                        <EditFormSettings UserControlName="Plantillas.ascx" EditFormType="WebUserControl">
                            <EditColumn UniqueName="EditCommandColumn1">
                            </EditColumn>
                        </EditFormSettings>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="HidenUrs" runat="server" />
    <telerik:RadWindowManager ID="RadAviso" runat="server">
        <Localization OK="Aceptar" />
    </telerik:RadWindowManager>

</asp:Content>

