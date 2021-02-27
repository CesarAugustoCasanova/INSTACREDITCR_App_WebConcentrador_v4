<%@ Page Title="Programación mensual" Language="VB" MasterPageFile="./MasterPage.master" AutoEventWireup="false" CodeFile="ProgramacionMensual.aspx.vb" Inherits="ProgramacionMensual" EnableViewState="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ Register TagPrefix="sds" Namespace="Telerik.Web.SessionDS" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <script language="javascript" type="text/javascript">
        function fnAceptar(y) {
            alert('El Contenido del TextBox es: ' + document.getElementById(y).value);
            document.getElementById("txtbxFintroCal").value = 'hola';
        }
        function OnClientAppointmentMoveStart(sender, eventArgs) {
            eventArgs.set_cancel(true);
        }
        function OnClientAppointmentResizeEnd(sender, args) {
            args.set_cancel(true);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMaster" runat="Server">
    <%--  <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        <Scripts>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
        </Scripts>

    </telerik:RadScriptManager>--%>

    <div class="Pagina">
        <div id="Carga" runat="server" visible="false">

            <table align="center" class="Table">
                
                <tr class="Titulos">
                    <td colspan="2">Carga mensual de filas de trabajo
                    </td>
                </tr>
                <tr>
                    <td>

                        <telerik:RadAsyncUpload RenderMode="Lightweight"  runat="server"  ID="AsyncUpload1" HideFileInput="true" AllowedFileExtensions=".csv" MaxFileInputsCount="1" OnFileUploaded="AsyncUpload1_FileUploaded" />
                        <telerik:RadProgressManager runat="server" ID="RadProgressManager1" />
                        <telerik:RadProgressArea RenderMode="Lightweight" runat="server" ID="RadProgressArea1" HeaderText="Subiendo archivo" />
                    </td>

                    <td>
                        <telerik:RadButton ID="BtnCargar" runat="server" Text="Cargar" CssClass="Botones" ToolTip="Asegurese que el formato del archivo es correcto"></telerik:RadButton>
                        <%--<asp:Button ID="BtnCargar" runat="server" CssClass="Botones" Text="Cargar" />--%>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblMsj" runat="server" Text="" CssClass="LblDesc" Visible="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">

                        <table class="Table" style="color: black">
                            <tr align="justify">
                                <td class="Titulos" colspan="6">Layout Calendario Filas</td>
                            </tr>
                            <tr class="Izquierda">
                                <td><strong>Campo</strong> </td>
                                <td>Nombre de fila</td>
                                <td>Descripción</td>
                                <td>Día</td>
                                <td>Hora inicio</td>
                                <td>Hora fin</td>
                            </tr>
                            <tr class="Izquierda">
                                <td><strong>Observaciones</strong> </td>
                                <td>Hasta 50 Carácteres</td>
                                <td>Hasta 150 Carácteres</td>
                                <td>DD/MM/AAAA</td>
                                <td>HH24:MM</td>
                                <td>HH24:MM</td>
                            </tr>
                            <tr>
                                <td style="color: red; text-align: center" colspan="6">Archivo En Formato en CSV Con Encabezado</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>


        <div runat="server" id="Calendario" visible="false">
            <table align="center">
                <tr class="Titulos">
                    <td>
                        <br />
                        <br />
                        <b>Calendario</b>
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
                            <AjaxSettings>
                                <telerik:AjaxSetting AjaxControlID="RadScheduler1">
                                    <UpdatedControls>
                                        <telerik:AjaxUpdatedControl ControlID="RadScheduler1" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                                    </UpdatedControls>
                                </telerik:AjaxSetting>
                            </AjaxSettings>
                        </telerik:RadAjaxManager>
                        <telerik:RadWindowManager RenderMode="Lightweight" ID="RadWindowManager1" runat="server" EnableShadow="true" >
                        </telerik:RadWindowManager>
                        <telerik:RadScheduler ID="RadScheduler1" runat="server" OnLoad="RadScheduler1_Load" OnAppointmentCreated="RadScheduler1_AppointmentCreated" AppointmentStyleMode="Default" ShowFooter="False" OverflowBehavior="Auto" DayStartTime="07:00:00" DayEndTime="22:00:00" EnableDescriptionField="false" Width="100%" Height="100%" OnTimeSlotCreated="RadScheduler1_TimeSlotCreated" DayView-ShowAllDayInsertArea="false" ShowAllDayRow="false" OnAppointmentUpdate="RadScheduler1_AppointmentUpdate" EnableRecurrenceSupport="false" StartInsertingInAdvancedForm="true" OnAppointmentDelete="RadScheduler1_AppointmentDelete" OnAppointmentInsert="RadScheduler1_AppointmentInsert" OnClientAppointmentMoveStart="OnClientAppointmentMoveStart" OnClientAppointmentMoveEnd="OnClientAppointmentMoveStart" WorkDayEndTime="22:00:00" WorkDayStartTime="07:00:00" OnClientAppointmentResizeEnd="OnClientAppointmentResizeEnd">
                            <AdvancedForm Modal="true" />
                            <TimelineView UserSelectable="false"></TimelineView>

                            <AppointmentTemplate>
                                <b>
                                    <%# Eval("Subject") %>
                                </b>
                            </AppointmentTemplate>

                        </telerik:RadScheduler>
                    </td>
                </tr>
            </table>
        </div>
        <asp:HiddenField ID="HidenUrs" runat="server" />
    </div>
    <telerik:RadWindowManager runat="server" ID="Aviso" Width="420px" ></telerik:RadWindowManager>
</asp:Content>



