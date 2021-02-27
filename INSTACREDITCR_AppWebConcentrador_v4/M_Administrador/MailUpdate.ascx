<%@ Control Language="VB" AutoEventWireup="false" CodeFile="MailUpdate.ascx.vb" Inherits="M_Administrador_MailUpdate" %>

<telerik:RadFormDecorator RenderMode="Lightweight" ID="FormDecorator1" runat="server" DecoratedControls="all" DecorationZoneID="decorationZone" EnableAjaxSkinRendering="false"></telerik:RadFormDecorator>
<!--<div class="w3-panel">
    <telerik:RadLabel runat="server" ID="lblFechaAlta"></telerik:RadLabel>
</div>-->
<telerik:RadAjaxPanel runat="server" CssClass="w3-panel" ID="decorationZone">
     <telerik:RadWindowManager ID="RadAviso1" runat="server">
        <Localization OK="Aceptar" />
    </telerik:RadWindowManager>
    <fieldset class="w3-container w3-white">
        <legend>ENVIO DE CORREOS (EMAILS)</legend>
          <div class="text-center mb-2">LOS CAMPOS CON UN * SON IMPORTANTES PARA SU REGISTRO.</div>
        <div class="w3-row-padding">
             <div class="w3-col s12 m6 l3">
               <telerik:RadTextBox runat="server" ID="txtid" Width="100%" CssClass="form-control" Visible="false"></telerik:RadTextBox>
            </div>
            <!--DESTINATARIO-->
             <div class="w3-col s12 m6 l3">
                <label>PARA: *</label>
               <telerik:RadTextBox runat="server" ID="txtCorreo" Width="100%" CssClass="form-control"></telerik:RadTextBox>
            </div>
          
            <!--DISPLAY NAME QUIEN LO ENVIA--> 
            <div class="w3-col s12 m6 l3">
                <label>PERFIL DE: *</label>
               <telerik:RadTextBox runat="server" ID="txtUsuario" ReadOnly="true" Width="100%" CssClass="form-control"></telerik:RadTextBox>
                
            </div>

             <!--ASUNTO DEL MENSAJE-->
            <div class="w3-col s12 m6 l3">
                <label>ASUNTO </label>
               <telerik:RadTextBox runat="server" ID="txtAsunto" Width="100%" CssClass="form-control"></telerik:RadTextBox>
            </div>
        </div>

        <telerik:RadAjaxPanel runat="server" CssClass="w3-row-padding">
        <!--CUERPO DEL MENSAJE-->
               <div class="w3-col s12 m6 l3">
              <label>MENSAJE *</label>
             <telerik:RadTextBox runat="server" ID="txtdescripcion" CssClass="form-control"  Width="100%"></telerik:RadTextBox>
               </div>
        
         
        </telerik:RadAjaxPanel>
       
       
        <br />
       <telerik:RadInputManager runat="server" ID="inputManagerTI">
            <telerik:RegExpTextBoxSetting BehaviorID="RagExpUSR" Validation-IsRequired="true"
                ValidationExpression="[A-Z0-9.]+" ErrorMessage="Solo mayúsculas y números.">
                <TargetControls>
                    <telerik:TargetInput ControlID="txtUsuario"></telerik:TargetInput>
                </TargetControls>
            </telerik:RegExpTextBoxSetting>
            <telerik:RegExpTextBoxSetting BehaviorID="RagExpASU" Validation-IsRequired="true"
                   ValidationExpression="[A-Za-z áéíóúüÁÉÍÓÚÜ]+" ErrorMessage="No se permite campo Vacio">
                <TargetControls>
                    <telerik:TargetInput ControlID="txtdescripcion"></telerik:TargetInput>
                </TargetControls>
            </telerik:RegExpTextBoxSetting>
              <telerik:RegExpTextBoxSetting BehaviorID="RagExpDES" Validation-IsRequired="true"
                   ValidationExpression="[A-Za-z áéíóúüÁÉÍÓÚÜ]+" ErrorMessage="No se permite campo Vacio">
                <TargetControls>
                    <telerik:TargetInput ControlID="txtdescripcion"></telerik:TargetInput>
                </TargetControls>
            </telerik:RegExpTextBoxSetting>

            <telerik:RegExpTextBoxSetting BehaviorID="RagExpCORREO" Validation-IsRequired="false"
                ValidationExpression="[ @\.]+" ErrorMessage="Necesita un @" ClearValueOnError="false" SelectionOnFocus="CaretToBeginning">
                <TargetControls>
                    <telerik:TargetInput ControlID="txtCorreo"></telerik:TargetInput>
                </TargetControls>
            </telerik:RegExpTextBoxSetting>
        </telerik:RadInputManager>
    </fieldset>
</telerik:RadAjaxPanel>
<div class="container">
    <div class="row">
        <div class="col-6 px-2">
          <telerik:radButton runat="server" ID="btnEnviar" Text="Enviar" SingleClick="true" SingleClickText="Enviando..." CommandName='<%# "Update"%>' CssClass="bg-success text-white border-0 w3-block" />
         </div>
        <div class="col-6 px-2">
        <telerik:radButton runat="server" ID="btnCancelar" Text="Cancelar" CommandName="Cancel" CausesValidation="false" CssClass="bg-danger text-white border-0 w3-block"></telerik:radButton>
        </div>
    </div>
</div>
<telerik:RadScriptBlock runat="server">
     <script>
        checkPasswordMatch = function () {
   
         
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
    
            radToolTip.show()
        }
        hideTooltip = function () {
       
            radToolTip.hide()
        }
    </script>
    <asp:HiddenField ID="HidenUrs" runat="server" />
   
</telerik:RadScriptBlock>