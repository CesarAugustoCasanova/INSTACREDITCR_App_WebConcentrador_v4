<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ModuloMails.ascx.vb" Inherits="M_Administrador_ModuloMails" %>


<telerik:RadFormDecorator RenderMode="Lightweight" ID="FormDecorator1" runat="server" DecoratedControls="all" DecorationZoneID="decorationZone" EnableAjaxSkinRendering="false"></telerik:RadFormDecorator>
<!--<div class="w3-panel">
    <telerik:RadLabel runat="server" ID="lblFechaAlta"></telerik:RadLabel>
</div>-->
<telerik:RadAjaxPanel runat="server" CssClass="w3-panel" ID="decorationZone">
    <fieldset class="w3-container w3-white">
        <legend>Configuración de Perfiles de Correo (EMAILS)</legend>
         <div class="text-center mb-2">LOS CAMPOS CON UN * SON IMPORTANTES PARA SU REGISTRO.</div>
        <div class="w3-row-padding">
            <!--ACCOUNT NAME-->
                <asp:TextBox runat="server" ID="txtid" Width="100%" CssClass="form-control" Visible="false" autoComplete="new-password"></asp:TextBox>
          
              <div class="w3-col s12 m6 l3">
                <label>USUARIO *</label>
                <asp:TextBox runat="server" ID="txtUsuario" Width="100%" CssClass="form-control" autoComplete="new-password"></asp:TextBox>
            </div>
            <!--DISPLAY NAME--> 
            <div class="w3-col s12 m6 l3">
                <label>NOMBRE *</label>
                <asp:TextBox runat="server" ID="txtNombre" Width="100%" CssClass="form-control" autoComplete="new-password"></asp:TextBox>
                
            </div>

            <!--EMAIL ADDRESS-->
            <!--USERNAME-->
            <div class="w3-col s12 m6 l3">
                <label>CORREO *</label>
                <asp:TextBox runat="server" ID="txtCorreo" AutoCompleteType="Email" Width="100%" CssClass="form-control" autoComplete="new-password"></asp:TextBox>
            </div>
            <!--PASSWORD-->
            <div class="w3-col s12 m6 l3">
                <label>CONTRASEÑA DEL CORREO ELECTRONICO *</label>
                <telerik:RadTextBox runat="server" ID="txtContrasena" Width="100%" TextMode="Password" onkeyup="checkPassword()" CssClass="form-control" ClientEvents-OnBlur="hideTooltip" ClientEvents-OnFocus="showTooltip" autoComplete="new-password"></telerik:RadTextBox>
                <telerik:RadToolTip runat="server" ID="TooltipPassword" ShowEvent="FromCode" HideEvent="FromCode" RelativeTo="Element" TargetControlID="txtContrasena" Position="MiddleRight" AutoCloseDelay="0" ShowDelay="0" Style="z-index: 99999999999">
                 
                </telerik:RadToolTip>
            </div>
        </div>
        <div class="w3-row-padding">
           <div class="w3-col s12 m6 l3">
              <label>DESCRIPCCION DE LA CUENTA * </label>
                <asp:TextBox runat="server" ID="txtdescripcion" CssClass="form-control"  autoComplete="new-password" Width="100%"></asp:TextBox>
               </div> 
        
          <div class="w3-col s12 m6 l3">
                <label>NOMBRE DEL SERVIDOR * (Ejemplo: gmail: smtp.gmail.com o outlook: smtp.office365.com)</label>
                <asp:TextBox runat="server" ID="txtnserver" Width="100%" CssClass="form-control" autoComplete="new-password"></asp:TextBox>
                
            </div>

            <div class="w3-col s12 m6 l3"> 
                <label>PUERTO DEL SERVIDOR * (Ejemplo: 587 <--gmail)</label>
                <asp:TextBox runat="server" ID="txtport" Width="100%" CssClass="form-control" autoComplete="new-password"></asp:TextBox>
                
            </div>
        </div>
       
       
        <br />
        <telerik:RadInputManager runat="server" ID="inputManagerTI">
            <telerik:RegExpTextBoxSetting BehaviorID="RagExpUSR" Validation-IsRequired="true"
                ValidationExpression="[A-Z0-9.]+" ErrorMessage="Solo mayúsculas y números.">
                <TargetControls>
                    <telerik:TargetInput ControlID="txtUsuario"></telerik:TargetInput>
                </TargetControls>
            </telerik:RegExpTextBoxSetting>
            <telerik:RegExpTextBoxSetting BehaviorID="RagExpnombre" Validation-IsRequired="true"
                ValidationExpression="[A-Za-z áéíóúüÁÉÍÓÚÜ]+" ErrorMessage="Nombre inválido">
                <TargetControls>
                    <telerik:TargetInput ControlID="txtNombre"></telerik:TargetInput>
                </TargetControls>
            </telerik:RegExpTextBoxSetting>
              <telerik:RegExpTextBoxSetting BehaviorID="RagExpDES" Validation-IsRequired="true"
                   ValidationExpression="[A-Za-z áéíóúüÁÉÍÓÚÜ]+" ErrorMessage="No se permite campo Vacio">
                <TargetControls>
                    <telerik:TargetInput ControlID="txtdescripcion"></telerik:TargetInput>
                </TargetControls>
            </telerik:RegExpTextBoxSetting>
            <telerik:RegExpTextBoxSetting BehaviorID="RagExpMOTIVO" Validation-IsRequired="false"
                ValidationExpression="[ 0-9]+" ErrorMessage="No se admiten Letras o caracteres especiales" ClearValueOnError="false" SelectionOnFocus="CaretToBeginning">
                <TargetControls>
                    <telerik:TargetInput ControlID="txtport"></telerik:TargetInput>
                </TargetControls>
            </telerik:RegExpTextBoxSetting>
         
        </telerik:RadInputManager>
    </fieldset>
</telerik:RadAjaxPanel>
<div class="container">
    <div class="row">
        <div class="col-6 px-2">
        <telerik:radButton runat="server" ID="btnGuardar" Text="Guardar" SingleClick="true" SingleClickText="Guardando..." CommandName='<%# IIf((TypeOf (Container) Is GridEditFormInsertItem), "PerformInsert", "Update")%>' CssClass="bg-success text-white border-0 w3-block" />
        </div>
        <div class="col-6 px-2">
        <telerik:radButton runat="server" ID="btnCancelar" Text="Cancelar" CommandName="Cancel" CausesValidation="false" CssClass="bg-danger text-white border-0 w3-block"></telerik:radButton>
        </div>
    </div>
</div>
<telerik:RadScriptBlock runat="server">
    <script>
        checkPasswordMatch = function () {
            var text1 = $find("<%=txtContrasena.ClientID %>");
         
            if (text2.get_textBoxValue() == "") {
                document.getElementById('passStatus').style.color = 'black';
                document.getElementById('passStatus').innerHTML = "";
            }
            else if (text1.get_textBoxValue() == text2.get_textBoxValue()) {
                document.getElementById('passStatus').style.color = 'green';
                document.getElementById('passStatus').innerHTML = " Coinciden";
            }
            else {
                document.getElementById('passStatus').style.color = 'red';
                document.getElementById('passStatus').innerHTML = " No coinciden";
            }
        }
        ChangeToAccepted = id => {
            if ($(id).hasClass("w3-text-red")) {
                $(id).fadeOut(500, () => {
                    $(id).removeClass("w3-text-red");
                    $(id).addClass("w3-text-green");
                    $(id + "Icon").html("&#10004;");
                });
                $(id).fadeIn(500);
            }
        }
        ChangeToUnaccepted = id => {
            if ($(id).hasClass("w3-text-green")) {
                $(id).fadeOut(500, () => {
                    $(id).removeClass("w3-text-green");
                    $(id).addClass("w3-text-red");
                    $(id + "Icon").html("&#10006;");
                });
                $(id).fadeIn(500);
            }
        }
        passwordValidation = () => {
            var pass = $find("<%=txtContrasena.ClientID %>").get_textBoxValue();
            var regexMayus = new RegExp("[A-Z]{" + minMayus + ",}").test(pass);
            var regexMinus = new RegExp("[a-z]{" + minMinus + ",}").test(pass);
            var regexNums = new RegExp("[0-9]{" + minNums + ",}").test(pass);
            var regexSpecial = new RegExp("[^0-9^A-Z^a-z^\ ]{" + minSpecial + ",}").test(pass);
            var validLong = (minLong <= pass.length) ? true : false;
            if (regexMayus) ChangeToAccepted("#pwdMayus"); else ChangeToUnaccepted("#pwdMayus");
            if (regexMinus) ChangeToAccepted("#pwdMinus"); else ChangeToUnaccepted("#pwdMinus");
            if (regexNums) ChangeToAccepted("#pwdNums"); else ChangeToUnaccepted("#pwdNums");
            if (regexSpecial) ChangeToAccepted("#pwdSpecial"); else ChangeToUnaccepted("#pwdSpecial");
            if (validLong) ChangeToAccepted("#pwdMinlong"); else ChangeToUnaccepted("#pwdMinlong");
        }
        checkPassword = function () {
            checkPasswordMatch()
            passwordValidation()
        }
        showTooltip = function () {
            var radToolTip = $find("<%= TooltipPassword.ClientID %>");
            radToolTip.show()
        }
        hideTooltip = function () {
            var radToolTip = $find("<%= TooltipPassword.ClientID %>");
            radToolTip.hide()
        }
    </script>

     <asp:HiddenField ID="HidenUrs" runat="server" />

     <telerik:RadWindowManager ID="RadAviso" runat="server">
        <Localization OK="Aceptar" />
    </telerik:RadWindowManager>
</telerik:RadScriptBlock>