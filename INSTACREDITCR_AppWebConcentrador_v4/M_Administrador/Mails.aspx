<%@ Page Title="" Language="VB" MasterPageFile="~/M_Administrador/MasterPage.master" AutoEventWireup="false" CodeFile="Mails.aspx.vb" Inherits="Mails" %>



<asp:Content ID="Content2" ContentPlaceHolderID="CPHMaster" runat="Server">
    <div >
     



        <div class="text-center">
            <h2>PERFILES</h2>
            <div class="text-center mb-2">CONFIGURACION DE MAIL.</div>
        </div>
        <telerik:RadGrid runat="server" ID="gridMails" Width="100%" AllowSorting="true" AutoGenerateColumns="true" ClientSettings-Selecting-AllowRowSelect="true" AllowMultiRowSelection="false">
            <MasterTableView Width="100%" CommandItemDisplay="Top" AllowPaging="false" PageSize="10" EditMode="PopUp">
                <Columns>
                     <telerik:GridButtonColumn ButtonType="FontIconButton" HeaderText="Eliminar" CommandName="Delete" HeaderStyle-Width="50px" ConfirmText="Esta acción no se puede deshacer" ConfirmTitle="¿Eliminar?" ConfirmDialogType="RadWindow" ConfirmDialogWidth="600px"></telerik:GridButtonColumn>
               
                     </Columns>
                <CommandItemSettings AddNewRecordText="Nuevo Perfil" RefreshText="Actualizar" />
                <PagerStyle FirstPageText="Inicio" LastPageText="Fin" NextPageText="Siguiente" Mode="NextPrevAndNumeric" Wrap="true" />
                <EditFormSettings UserControlName="ModuloMails.ascx" EditFormType="WebUserControl" >
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
                            <telerik:GridEditCommandColumn ItemStyle-Width="5px"  HeaderText="Actualizar" UniqueName="Expandible">
                            </telerik:GridEditCommandColumn>
                          
                        </Columns>
                        <EditFormSettings UserControlName="ModuloMails.ascx" EditFormType="WebUserControl" >
                            <EditColumn UniqueName="Update">
                            </EditColumn>
                            
                        </EditFormSettings>
               </MasterTableView>






        </telerik:RadGrid>
        </div>
     <div class="container">
     <div>

         </div>



        <div class="text-center">
        
            <h2>MENSAJES DE PRUEBA.</h2>
        </div>
        <telerik:RadGrid runat="server" ID="GridEnvio" Width="100%" AllowSorting="true" AutoGenerateColumns="true" show ClientSettings-Selecting-AllowRowSelect="true" AllowMultiRowSelection="false">
            <MasterTableView CommandItemDisplay="Top" AllowPaging="true" PageSize="10" EditMode="PopUp">
           
                <%--<PagerStyle FirstPageText="Inicio" LastPageText="Fin" NextPageText="Siguiente" Mode="NextPrevAndNumeric" Wrap="true" />--%>
                <EditFormSettings UserControlName="MailUpdate.ascx" EditFormType="WebUserControl"  >
                    <PopUpSettings CloseButtonToolTip="Cerrar" Modal="true" KeepInScreenBounds="true" Width="90%" OverflowPosition="Center" Height="90%" ScrollBars="Auto"/>

                    <EditColumn UniqueName="Update" >
                    </EditColumn>
                </EditFormSettings>
                <NoRecordsTemplate>
                    <p class="text-center">
                        Sin Correos. Por Favor, agregue un perfil con correo para Realizar un envio.
                    </p>
                </NoRecordsTemplate>
                 <CommandItemSettings  ShowAddNewRecordButton="false" ShowRefreshButton="true" />
                        <Columns>
                            <telerik:GridEditCommandColumn ItemStyle-Width="5px"  HeaderText="TEST" UniqueName="Expandible" ButtonType="ImageButton" editImageUrl="Imagenes/mail.png">
                                
                            </telerik:GridEditCommandColumn>
                         
                        </Columns>
                        <EditFormSettings UserControlName="MailUpdate.ascx" EditFormType="WebUserControl" >
                            
                               <EditColumn UniqueName="Send">
                            </EditColumn>
                        </EditFormSettings>
             </MasterTableView>






        </telerik:RadGrid>
        </div>

     <asp:HiddenField ID="HidenUrs" runat="server" />

    <telerik:RadWindowManager ID="RadAviso" runat="server">
        <Localization OK="Aceptar" />
    </telerik:RadWindowManager>
<style>
    .rbPredefinedIcons {
    display: block;
    float: left;
    width: 16px;
    height: 16px;
    background-image: url(images/rbPredefinedIcons.png);
</style>
</asp:Content>




<%--<asp:Content ID="Content1" ContentPlaceHolderID="CPHMaster1" runat="Server">
   

</asp:Content>--%>