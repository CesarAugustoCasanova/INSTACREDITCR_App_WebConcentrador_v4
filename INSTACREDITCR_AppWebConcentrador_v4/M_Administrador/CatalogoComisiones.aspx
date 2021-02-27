<%@ Page Title="" Language="VB" MasterPageFile="~/M_Administrador/MasterPage.master" AutoEventWireup="false" CodeFile="CatalogoComisiones.aspx.vb" Inherits="M_Administrador_CatalogoComisiones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMaster" runat="Server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <br />
    <div class="w3-container Titulos w3-margin-bottom">
        <b>Comisiones</b>
    </div>
    <br />
    <asp:Panel ID="PNLComisiones" runat="server" Width="100%">
        <br />
        <div class="container">
            <telerik:RadGrid runat="server" ID="RGComisiones" OnNeedDataSource="RGComisiones_NeedDataSource" Width="100%" AllowPaging="false">
                <MasterTableView>
                    <Columns>
                        <%-- <telerik:GridEditCommandColumn ItemStyle-Width="5px"  HeaderText="VER" commandname="Ver" ButtonType="ImageButton" editImageUrl="Imagenes/lupa.png">
                                
                            </telerik:GridEditCommandColumn>--%>
                        <telerik:GridButtonColumn UniqueName="Ver" HeaderText="VER" Text="VER" CommandName="Ver" ButtonType="ImageButton" ImageUrl="Imagenes/lupa.png"></telerik:GridButtonColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid><br />
        </div>
    </asp:Panel>
    <asp:Panel ID="PnlRangos" runat="server" Width="100%" Visible="false">
        <br />
        <div class="container">
            <fieldset>
                <legend>
                    <telerik:RadLabel runat="server" ID="rLblIdTipo" CssClass="col-form-label" Font-Size="Small" />
                    <telerik:RadLabel runat="server" ID="rLblTipo" CssClass="col-form-label" Font-Size="Smaller"/>
                </legend>
                <telerik:RadGrid runat="server" ID="RGCRangos" OnNeedDataSource="RGComisiones_NeedDataSource" Width="100%" AllowPaging="false">
                    <MasterTableView>
                    </MasterTableView>
                </telerik:RadGrid>
            </fieldset>
            <br />
        </div>
        <br />
        <div class="container">
            <telerik:RadButton runat="server" Text="Calcular" CssClass="alert-primary" ID="BtnCalcular"></telerik:RadButton>
        </div>
    </asp:Panel>
    <div class="container">
        <div style="overflow:scroll;max-height:580px">
            <telerik:RadGrid runat="server" ID="RadGrid1" Width="100%" AllowPaging="false">
            <MasterTableView></MasterTableView>
        </telerik:RadGrid>
        </div>
    </div>
    <%--    </telerik:RadAjaxPanel>--%>
    <%--<div class="w3-container  text-left text-center">
     <telerik:RadButton runat="server" ID="rBtnAceptar" CssClass="btn-green" Text="Aceptar" />
     </div>--%>
    <telerik:RadNotification RenderMode="Lightweight" ID="RadNotification" runat="server" Height="140px" Animation="Fade" EnableRoundedCorners="true" EnableShadow="true" AutoCloseDelay="5500" Position="Center" OffsetX="-30" OffsetY="-70" ShowCloseButton="true" VisibleOnPageLoad="false" LoadContentOn="EveryShow" KeepOnMouseOver="false">
    </telerik:RadNotification>
</asp:Content>

