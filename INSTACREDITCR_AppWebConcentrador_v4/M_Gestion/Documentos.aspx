<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Documentos.aspx.vb" Inherits="Documentos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="JS/iFrameMejorado.js?v=2" type="text/javascript"></script>
    <meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />
    <link rel="stylesheet" href="Estilos/w3.css" />
    <title></title>
    <script>
        function ClearUploads() {
            var upload = $find("RadAsyncUpload1");
 var inputs = upload.getUploadedFiles().length;
  for (i = 0; i <= inputs; i++)
 {
  upload.deleteFileInputAt(i);
 }
        }
    </script>
</head>

<body style="font-size: .7em" class="scroll">
    <form id="form1" runat="server" onmousemove="window.parent.movement();">
        <noscript>
            <div class="w3-modal" style="display: block">
                <div class="w3-modal-content">
                    <div class="w3-container w3-red w3-center w3-padding-24 w3-jumbo">
                        JavaScript deshabilitado
                    </div>
                    <div class="w3-container w3-center w3-xlarge">
                        Javascript está deshabilitado en su navegador web. Por favor, para ver correctamente este sitio, <b><i>habilite javascript</i></b>.<br />
                        <br />
                        Para ver las instrucciones para habilitar javascript en su navegador, haga click <a href="http://www.enable-javascript.com/es/">aquí</a>.
                    </div>
                </div>
            </div>
        </noscript>

        <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
        <telerik:RadAjaxManager runat="server" ID="RadAjaxManager1">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="GridArchivos">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="GridArchivos" />
                        <telerik:AjaxUpdatedControl ControlID="Notificacion" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="RadAsyncUpload1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="GridConvenios" />
                        <telerik:AjaxUpdatedControl ControlID="GridArchivos" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="GridConvenios">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="GridConvenios" />
                        <telerik:AjaxUpdatedControl ControlID="Notificacion" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="BtnCargar">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="GridArchivos" />
                        <telerik:AjaxUpdatedControl ControlID="GridConvenios" />
                        <telerik:AjaxUpdatedControl ControlID="Notificacion" />
                        <telerik:AjaxUpdatedControl ControlID="txtDescripcion" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <asp:Panel ID="PnlCargar" runat="server" Visible='<%# tmpPermisos("DOCUMENTOS_CARGA") %>' CssClass="w3-container w3-center w3-margin">
            <div class="w3-container w3-center w3-blue">
                <b>Carga de Documentos</b>
            </div>
            <div class="w3-row w3-panel" style="overflow: auto">
                <div class="w3-third">
                    <br />
                    <div class="w3-col s12 m12 l2">
                  
                   <telerik:RadButton runat="server" ID="BtnPopUp" Text="Preview"></telerik:RadButton>
                        <script type="text/javascript">
                                function OpenWindows(url,config_ventana){
                                    window.open(url, "titulo", config_ventana);
                                }
                        </script>

                    <%--<asp:Button runat="server" ID="btnPopUp"  Text="Imprimir" />--%>
                </div>
                </div>

                <div class="w3-third">
                    <label>Capture un comentario</label>
                    <telerik:RadTextBox ID="txtDescripcion" runat="server" Height="150px" MaxLength="500" TextMode="MultiLine" Width="100%"></telerik:RadTextBox>
                    <br />
                    <br />
                    <label>Selección de Archivo</label>
                    <telerik:RadAsyncUpload ID="RadAsyncUpload1" runat="server" AllowedFileExtensions=".pdf" MultipleFileSelection="Disabled" MaxFileSize="4000000" RenderMode="Classic" CssClass="w3-center">
                    </telerik:RadAsyncUpload>
                    <br />
                    <br />
                    <div class="w3-center">
                        <asp:Button ID="BtnCargar" runat="server" CssClass="w3-btn w3-green" Text="Cargar"/>
                    </div>
                </div>
                <div class="w3-third">
                    <br />
                </div>
            </div>
            
             <%--   <telerik:RadWindow RenderMode="Lightweight" ID="RadWindowprueba" VisibleOnPageLoad="false" Behaviors="maximize" Title="Credifiel" 
                                   IconUrl="wikiFavicon.ico" runat="server">
                </telerik:RadWindow>--%>
             
        </asp:Panel>
        <div class="w3-container w3-center w3-blue w3-margin">
            <b>Historico de Documentos</b>
        </div>
        <div class="w3-container w3-center" style="overflow: auto">
            <telerik:RadLabel ID="lbmsj" runat="server" CssClass="LblDesc"></telerik:RadLabel>
            <telerik:RadGrid ID="GridArchivos" runat="server" Visible="true" HeaderStyle-HorizontalAlign="Center" Style="overflow: visible;" OnItemCommand="RadGrid1_ItemCommand" OnNeedDataSource="GridArchivos_NeedDataSource">
                <MasterTableView>
                    <Columns>
                        <telerik:GridTemplateColumn HeaderText="Eliminar" UniqueName="column" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" Enabled='<%# tmpPermisos("DOCUMENTOS_ELIMINAR") %>' CommandName="delete_file"><img alt="ELIMINAR" src="Imagenes/icons8-eliminar-50-2.png"/></asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridButtonColumn ButtonType="ImageButton" UniqueName="column2" HeaderText="VISUALIZAR" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" CommandName="download_file" ImageUrl="Imagenes/icons8-pdf-2-50.png"></telerik:GridButtonColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </div>

        <div class="w3-container w3-center w3-blue w3-margin">
            <b>Historico de Convenios</b>
        </div>
        <div class="w3-container w3-center" style="overflow: auto">
            <telerik:RadLabel ID="Lbmsj2" runat="server" CssClass="LblDesc"></telerik:RadLabel>
            <telerik:RadGrid ID="GridConvenios" runat="server" Visible="true" HeaderStyle-HorizontalAlign="Center" Style="overflow: visible;" OnItemCommand="RadGrid2_ItemCommand" OnNeedDataSource="GridConvenios_NeedDataSource">
                <MasterTableView>
                    <Columns>
                        <telerik:GridButtonColumn ButtonType="ImageButton" UniqueName="column2" HeaderText="VISUALIZAR" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" CommandName="download_file" ImageUrl="Imagenes/icons8-pdf-2-50.png"></telerik:GridButtonColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </div>
        <telerik:RadNotification ID="Notificacion" runat="server" Position="Center" Width="330" Height="160" Animation="Fade" EnableRoundedCorners="true" EnableShadow="true" Style="z-index: 100000">
        </telerik:RadNotification>

        <telerik:RadLabel ID="LblCat_Lo_Usuario" runat="server" Visible="false"></telerik:RadLabel>
    </form>
</body>
</html>
