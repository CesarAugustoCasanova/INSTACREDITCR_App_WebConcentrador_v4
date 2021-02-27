<%@ Page Title="" Language="VB" MasterPageFile="~/M_Administrador/MasterPage.master" AutoEventWireup="false" CodeFile="CatalogoMail.aspx.vb" Inherits="CatalogoMail" %>



<asp:Content ID="Content2" ContentPlaceHolderID="CPHMaster" runat="Server">
    <div class="container">
     



        <div class="text-center">
            <h2>PERFILES</h2>
            <div class="text-center mb-2">REGISTRO DE PERFILES MAIL.</div>
        </div>
        <telerik:RadGrid runat="server" ID="gridMails" Width="100%" AllowSorting="true" AutoGenerateColumns="true" ClientSettings-Selecting-AllowRowSelect="true" AllowMultiRowSelection="false">
            <MasterTableView CommandItemDisplay="Top" AllowPaging="true" PageSize="10" EditMode="PopUp">
                <Columns>
                 
                   
                     <telerik:GridButtonColumn ButtonType="FontIconButton" HeaderText="Eliminar" CommandName="Delete" HeaderStyle-Width="50px" ConfirmText="Esta acción no se puede deshacer" ConfirmTitle="¿Eliminar?" ConfirmDialogType="RadWindow" ConfirmDialogWidth="600px"></telerik:GridButtonColumn>
               
                     </Columns>
                <CommandItemSettings AddNewRecordText="Nuevo Perfil" RefreshText="Actualizar" />
                <PagerStyle FirstPageText="Inicio" LastPageText="Fin" NextPageText="Siguiente" Mode="NextPrevAndNumeric" Wrap="true" />
                <EditFormSettings UserControlName="PruebaModuloMails.ascx" EditFormType="WebUserControl" >
                    <PopUpSettings CloseButtonToolTip="Cerrar" Modal="true" KeepInScreenBounds="true" Width="90%" OverflowPosition="Center" Height="90%" ScrollBars="Auto"/>
                    <EditColumn UniqueName="Update">
                    </EditColumn>
                </EditFormSettings>
                <NoRecordsTemplate>
                    <p class="text-center">
                        Sin Perfiles. Por Favor, agregue una perfil de correo para continuar.
                    </p>
                </NoRecordsTemplate>
                
                        <Columns>
                            <%--<telerik:GridEditCommandColumn ItemStyle-Width="5px"  HeaderText="Actualizar" UniqueName="Expandible">
                            </telerik:GridEditCommandColumn>--%>
                           
                        </Columns>
                        <EditFormSettings UserControlName="ModuloMail.ascx" EditFormType="WebUserControl" >
                            <EditColumn UniqueName="Update">
                            </EditColumn>
                        </EditFormSettings>
            </MasterTableView>






        </telerik:RadGrid>
        </div>

</asp:Content>