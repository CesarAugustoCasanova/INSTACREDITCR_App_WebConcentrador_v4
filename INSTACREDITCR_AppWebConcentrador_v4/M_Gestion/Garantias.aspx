<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Garantias.aspx.vb" Inherits="M_Gestion_Garantias" %>

<link href="Estilos/ObjHtmlS.css" rel="stylesheet" />
<link href="Estilos/ObjHtmlNoS.css" rel="stylesheet" />
<link href="Estilos/ObjAjax.css" rel="stylesheet" />
<link href="Estilos/Modal.css" rel="stylesheet" />
<link href="Estilos/HTML.css" rel="stylesheet" />
<link rel="stylesheet" href="https://www.w3schools.com/w3css/4/w3.css" />
<link href="Estilos/Telerik.css" rel="stylesheet" />
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="JS/iFrameMejorado.js?v=2" type="text/javascript"></script>
    <meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />
    <link rel="stylesheet" href="Estilos/w3.css" />
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
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPnlGeneral" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <%-- <telerik:RadAjaxPanel runat="server" ID="RPNLGral">--%>
          
    <div class="w3-container w3-blue w3-center">
        <label>Registro de Garantia</label>
    </div>
        <div class="w3-container">
    <%--<div class="w3-row" >
            <div class="w3-col m6">
                <div class="w3-padding">       
                    <div class="w3-col m6">  --%>
                        <%--<div class="w3-container w3-center w3-col m6">--%>
                            <label>Tipo de Garantia</label>
                        <%--</div>--%>
                    <%--</div>--%>
                                    
                    <telerik:RadDropDownList runat="server" DefaultMessage="Seleccione" ID="ComboBox1" AutoPostBack="true" OnSelectedIndexChanged="ComboBox1_SelectedIndexChanged" CssClass="w3-input">
                        <Items>
                            <telerik:DropDownListItem Text="Seleccione" Selected="True" Value="Seleccione" />
                            <telerik:DropDownListItem Text="Cuenta" Value="Cuenta" />
                            <telerik:DropDownListItem Text="Salario" Value="Salario" />
                        </Items>
                    </telerik:RadDropDownList>
              <%--  </div>
            </div>--%>


           <%-- <div class="w3-padding">
                <div class="w3-col m6">
                    <div class="w3-col m12">--%>
                        <%--<div class="w3-col m6">--%>
                            <asp:Label ID="Label3" Font-Names="Verdana" Font-Size="10pt" runat="server" />
                        <%--</div>--%>
                        <%--<div class="w3-col m6">--%>
                            <asp:Label ID="Label7" Font-Names="Verdana" Font-Size="10pt" runat="server" />
                        <%--</div>--%>
                      
                        <telerik:RadDropDownList runat="server" Visible="False" DefaultMessage="Seleccione" ID="ddlBanco" AutoPostBack="true" OnSelectedIndexChanged="ddlBanco_SelectedIndexChanged" CssClass="w3-input">
                            <Items>
                                <telerik:DropDownListItem Value="Otro"></telerik:DropDownListItem>
                            </Items>
                        </telerik:RadDropDownList>
                              
                        <%--<div class="w3-col m6">--%>
                            <asp:Label ID="Label4" Font-Names="Verdana" Font-Size="10pt" runat="server" />
                            <telerik:RadTextBox runat="server" ID="RTxtBanco" Visible="False"></telerik:RadTextBox>
                        <%--</div>--%>
                    <%--</div>
                </div>
            </div>
         --%>


<%--            <div class="w3-padding">
                <div class="w3-col m6">
                    <div class="w3-col m12">--%>
                                  
                    <%--<div class="w3-col m6">--%>
                        <asp:Label ID="Label5" Font-Names="Verdana" Font-Size="10pt" runat="server" />
                    <%--</div>--%>
                                        
                    <telerik:RadDropDownList runat="server" DefaultMessage="Seleccione" ID="ComboTipo" AutoPostBack="true" OnSelectedIndexChanged="ComboTipo_SelectedIndexChanged" Visible="false" CssClass="w3-input">
                        <Items>
                            <telerik:DropDownListItem Text="Seleccione" Selected="True" Value="Seleccione" />
                            <telerik:DropDownListItem Text="Publico" Value="Publico" />
                            <telerik:DropDownListItem Text="Privado" Value="Privado" />
                        </Items>
                    </telerik:RadDropDownList>
                                           
                     
                    <%--<div class="w3-col m6">--%>
                        <asp:Label ID="Label6" Font-Names="Verdana" Font-Size="10pt" runat="server" />
                        <telerik:RadTextBox runat="server" ID="RTBDependencia" Visible="False"></telerik:RadTextBox>
                    <%--</div>--%>
                   
                    <%-- <telerik:RadButton ID="BtnGuardarC" runat="server" Text="Guardar" Visible="false" />--%>
                    <%--<div class="w3-col m6">--%>
                        <asp:Label ID="Label1" Font-Names="Verdana" Font-Size="10pt" runat="server" />
                    <%--</div>--%>
                   <%-- </div>
            
                </div>
                        
            </div>
              --%>
            <br /><br />
            <telerik:RadButton Text="Registrar" OnClick="SubmitBtn_Click" ID="regis" Visible="false" runat="server" />
         
            <telerik:RadNotification ID="noti113" runat="server" Position="Center" Width="330" Height="160" Animation="Fade" EnableRoundedCorners="true" EnableShadow="true" Style="z-index: 100000">
            </telerik:RadNotification>
            <%--   </telerik:RadAjaxPanel>--%>

            <telerik:RadNotification ID="Notificacion13" runat="server" Position="Center" Width="330" Height="160" Animation="Fade" EnableRoundedCorners="true" EnableShadow="true" Style="z-index: 100000">
            </telerik:RadNotification>
       </div>
     <%--</div>--%>






          <%-- <div class="w3-container w3-center w3-blue w3-margin">
            <b>Ultima Garantia Asignada </b>
        </div>
          <telerik:RadGrid runat="server" ID="GridGarantia" Width="100%" AllowSorting="true" AutoGenerateColumns="true" Visible="false" AllowMultiRowSelection="false" Showaddnewrecodr = "False">
            <MasterTableView Width="100%" >
              
           
              
                <NoRecordsTemplate>
                    <p class="text-center">
                       No Se a Asignado Ninguna Garantia.
                    </p>
                </NoRecordsTemplate>
             </MasterTableView>
  </telerik:RadGrid>

          <telerik:RadGrid runat="server" ID="GridGarantia2" Width="100%" AllowSorting="true" AutoGenerateColumns="true" ClientSettings-Selecting-AllowRowSelect="true" Visible="False" AllowMultiRowSelection="false">
            <MasterTableView Width="100%" CommandItemDisplay="Top" AllowPaging="false" PageSize="10">
              
                <CommandItemSettings  RefreshText="Actualizar" />
              
                <NoRecordsTemplate>
                    <p class="text-center">
                       No Se a Asignado Ninguna Garantia.
                    </p>
                </NoRecordsTemplate>
             </MasterTableView>
  </telerik:RadGrid>--%>
</form>
</body>
</html>


