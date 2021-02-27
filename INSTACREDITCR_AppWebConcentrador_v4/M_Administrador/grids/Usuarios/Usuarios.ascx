<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Usuarios.ascx.vb" Inherits="Usuarios" %>

<telerik:RadFormDecorator RenderMode="Lightweight" ID="FormDecorator1" runat="server" DecoratedControls="all" DecorationZoneID="decorationZone" EnableAjaxSkinRendering="false"></telerik:RadFormDecorator>
<div class="w3-panel">
    <telerik:RadLabel runat="server" ID="lblFechaAlta"></telerik:RadLabel>
</div>
<telerik:RadAjaxPanel runat="server" CssClass="w3-panel" ID="decorationZone">
    <fieldset class="w3-container w3-white">
        <legend>Configuración de usuario</legend>
        <div class="w3-row-padding">
            <div class="w3-col s12 m6 l3">
                <label>Usuario</label>
                <asp:TextBox runat="server" ID="txtUsuario" Width="100%" CssClass="form-control" autoComplete="new-password"></asp:TextBox>
            </div>
            <div class="w3-col s12 m6 l3">
                <label>Nombre</label>
                <asp:TextBox runat="server" ID="txtNombre" Width="100%" CssClass="form-control" autoComplete="new-password"></asp:TextBox>
                <asp:Label runat="server" ID="LBLoldpassword" Visible="false"></asp:Label>
            </div>
            <div class="w3-col s12 m6 l3">
                <label>Contraseña</label>
                <telerik:RadTextBox runat="server" ID="txtContrasena" Width="100%" TextMode="Password" onkeyup="checkPassword()" CssClass="form-control" ClientEvents-OnBlur="hideTooltip" ClientEvents-OnFocus="showTooltip" autoComplete="new-password"></telerik:RadTextBox>
                <telerik:RadToolTip runat="server" ID="TooltipPassword" ShowEvent="FromCode" HideEvent="FromCode" RelativeTo="Element" TargetControlID="txtContrasena" Position="MiddleRight" AutoCloseDelay="0" ShowDelay="0" Style="z-index: 99999999999">
                    <!--&#10006;Tache &#10004;Paloma -->
                    <p id="pwdMayus" class="w3-text-red">
                        <span id="pwdMayusIcon">&#10006;</span> Al menos
                        <asp:Label runat="server" ID="pwdMayusLbl" Text="1"></asp:Label>
                        mayúscula(s)
                    </p>
                    <p id="pwdMinus" class="w3-text-red">
                        <span id="pwdMinusIcon">&#10006;</span> Al menos
                        <asp:Label runat="server" ID="pwdMinusLbl" Text="1"></asp:Label>
                        minúscula(s) 
                    </p>
                    <p id="pwdNums" class="w3-text-red">
                        <span id="pwdNumsIcon">&#10006;</span> Al menos
                        <asp:Label runat="server" ID="pwdNumsLbl" Text="1"></asp:Label>
                        número(s)
                    </p>
                    <p id="pwdSpecial" class="w3-text-red">
                        <span id="pwdSpecialIcon">&#10006;</span> Al menos
                        <asp:Label runat="server" ID="pwdSpecialLbl" Text="1"></asp:Label>
                        caracter(es) especial(es).
                        <br />
                        <abbr title="| ! · $ % & / ( ) = ? * , + - @ < > #">Caracteres Especiales Aceptados</abbr>
                    </p>
                    <p id="pwdMinlong" class="w3-text-red">
                        <span id="pwdMinlongIcon">&#10006;</span> Mínimo
                        <asp:Label runat="server" ID="pwdMinLongLbl" Text="1"></asp:Label>
                        caracter(es)
                    </p>
                </telerik:RadToolTip>
            </div>
            <div class="w3-col s12 m6 l3">
                <label>Repite contraseña</label><span id="passStatus"></span>
                <telerik:RadTextBox runat="server" ID="txtRepiteContrasena" Width="100%" TextMode="Password" onkeyup="checkPasswordMatch()" CssClass="form-control" autoComplete="new-password"></telerik:RadTextBox>
            </div>
        </div>
        <telerik:RadAjaxPanel runat="server" CssClass="w3-row-padding">
            <div class="w3-col s12 m6 l3">
                <label>Hora de entrada</label>
                <telerik:RadTimePicker runat="server" ID="dtpHoraEntrada" Width="100%" EnableTyping="false">
                    <TimeView runat="server" Caption="Entrada:" HeaderText="Entrada:"></TimeView>
                </telerik:RadTimePicker>
            </div>
            <div class="w3-col s12 m6 l3">
                <label>Hora de salida</label>
                <telerik:RadTimePicker runat="server" ID="dtpHoraSalida" Width="100%" EnableTyping="false">
                    <TimeView runat="server" Caption="Salida:" HeaderText="Salida:"></TimeView>
                </telerik:RadTimePicker>
            </div>
            <div class="w3-col s12 m6 l3">
                <label>Estatus</label>
                <telerik:RadDropDownList runat="server" ID="ddlEstatus" DefaultMessage="Estatus" Width="100%" AutoPostBack="true">
                    <Items>
                        <telerik:DropDownListItem Value="Activo" Text="Activo" />
                        <telerik:DropDownListItem Value="Expirado" Text="Expirado" />
                        <telerik:DropDownListItem Value="Cancelado" Text="Cancelado" />
                    </Items>
                </telerik:RadDropDownList>
            </div>
            <div class="w3-col s12 m6 l3">
                <telerik:RadLabel runat="server" ID="LblMotivo" Text="Motivo" Visible="false"></telerik:RadLabel>
                <asp:TextBox runat="server" ID="txtMotivo" TextMode="MultiLine" Resize="Vertical" Width="100%" MaxLength="150" Visible="false" CssClass="form-control"></asp:TextBox>
            </div>
        </telerik:RadAjaxPanel>
        <telerik:RadAjaxPanel runat="server" CssClass="w3-row-padding">
            <div class="w3-col s12 m6 l3">
                <label>Agencia</label>
                <telerik:RadDropDownList runat="server" ID="ddlAgencia" DefaultMessage="Seleccione" Width="100%" AutoPostBack="true"></telerik:RadDropDownList>
            </div>
            <div class="w3-col s12 m6 l3">
                <label>Ver agencias</label>
                <telerik:RadComboBox runat="server" ID="cbVerAgencias" EmptyMessage="Seleccione" Width="100%" CheckBoxes="true" CheckedItemsTexts="FitInInput" EnableCheckAllItemsCheckBox="true" Localization-CheckAllString="Todas" Localization-AllItemsCheckedString="Todas"></telerik:RadComboBox>
            </div>
            <div class="w3-col s12 m6 l3">
                <label>Supervisor</label>
                <telerik:RadDropDownList runat="server" ID="ddlSupervisor" DefaultMessage="Seleccione" Width="100%"></telerik:RadDropDownList>
            </div>
            <div class="w3-col s12 m6 l3">
                <label>Rol</label>
                <telerik:RadDropDownList runat="server" ID="ddlPerfil" DefaultMessage="Seleccione" Width="100%"></telerik:RadDropDownList>
            </div>
        </telerik:RadAjaxPanel>
        <telerik:RadAjaxPanel runat="server" CssClass="w3-row-padding">
            <div class="w3-col s12 m6 l3">
                <telerik:RadCheckBox runat="server" ID="CBAgencia" Width="100%" Text="Usuario Agencia" Visible="false"></telerik:RadCheckBox>
                <label>Número de Empleado</label>
                <telerik:RadTextBox runat="server" ID="TxtCat_Lo_NumEmpleado" Width="100%"/>
            </div>
            <div class="w3-col s12 m6 l3">
                <label>Ver Productos </label>
                <telerik:RadComboBox runat="server" ID="cbVerProductos" EmptyMessage="Seleccione" Width="100%" CheckBoxes="true" CheckedItemsTexts="FitInInput" EnableCheckAllItemsCheckBox="true" Localization-CheckAllString="Todas" Localization-AllItemsCheckedString="Todas"></telerik:RadComboBox>
            </div>
            <div class="w3-col s12 m6 l3">
                <label>Correo Electronico</label>
                <telerik:RadTextBox runat="server" ID="TxtCat_Lo_EMail" EmptyMessage="@" Width="100%" AutoCompleteType="Email" />
            </div>
            <div class="w3-col s12 m6 l3">
                <label>Meta </label>
                <telerik:RadNumericTextBox runat="server" ID="TxtMeta" EmptyMessage="Ingrese Meta" Width="100%" MinValue="0"></telerik:RadNumericTextBox>
            </div>
        </telerik:RadAjaxPanel>
        <br />
        <telerik:RadAjaxPanel runat="server" CssClass="w3-row-padding">
            <div class="w3-col s12 m6 l3">
                <label>Aplicaciones bloqueadas</label>
                <telerik:RadComboBox runat="server" ID="RCBAppsBloqueadas" EmptyMessage="Seleccione" Width="100%" CheckBoxes="true" CheckedItemsTexts="FitInInput" EnableCheckAllItemsCheckBox="true" Localization-CheckAllString="Todas" Localization-AllItemsCheckedString="Todas"></telerik:RadComboBox>
            </div>
            <div class="w3-col s12 m6 l3">
                <label>Instancia</label>
                <telerik:RadDropDownList runat="server" ID="rDdlInstancia" DefaultMessage="Seleccione" Width="100%">
                    <Items>
                        <telerik:DropDownListItem Value="1" Text="Domiciliación" />
                        <telerik:DropDownListItem Value="2" Text="Nómina" />
                        <telerik:DropDownListItem Value="3" Text="Extrajudicial" />
                        <telerik:DropDownListItem Value="4" Text="Judicial" />
                    </Items>
                </telerik:RadDropDownList>
            </div>
            <div class="w3-col s12 m6 l3">
                <label>Capacidad de Cuentas</label>
                <asp:TextBox runat="server" ID="rTxtCapacidadCuentas" Width="100%" CssClass="form-control" MaxLength="5" TextMode="Number"></asp:TextBox>
            </div>
            <div class="w3-col s12 m6 l3">
                <label>Tipo de Usuario</label>
                <telerik:RadDropDownList runat="server" ID="rDdlTipoUsuario" DefaultMessage="Seleccione" Width="100%">
                    <Items>
                        <telerik:DropDownListItem Value="Administrativo" Text="Administrativo" />
                        <telerik:DropDownListItem Value="Operativo" Text="Operativo" />
                    </Items>
                </telerik:RadDropDownList>
            </div>
        </telerik:RadAjaxPanel>
        <br />
        <telerik:RadAjaxPanel runat="server" CssClass="w3-row-padding">
        <div class="w3-col s12 m6 l3">
            <label>Nivel Quitas</label>
                <telerik:RadDropDownList runat="server" ID="dllNivelQuitas" DefaultMessage="Seleccione" Width="100%">
                    <Items>
                        <telerik:DropDownListItem Value="0" Text="N/A" />
                        <telerik:DropDownListItem Value="1" Text="1" />
                        <telerik:DropDownListItem Value="2" Text="2" />
                        <telerik:DropDownListItem Value="3" Text="3" />
                    </Items>
                </telerik:RadDropDownList>
            </div>
            <div class="w3-col s12 m6 l3">
                <label>Cobertura Visitas Cobranza</label>
                <telerik:RadComboBox runat="server" ID="rDdlCat_Lo_CoberturaCobranza" EmptyMessage="Seleccione" Width="100%" CheckBoxes="true" CheckedItemsTexts="FitInInput" EnableCheckAllItemsCheckBox="true" Localization-CheckAllString="Todas" Localization-AllItemsCheckedString="Todas"></telerik:RadComboBox>
            </div>
            <div class="w3-col s12 m6 l3">
                    <center><label>Empleado Interno</label> </center>
                <telerik:RadCheckBox runat="server" ID="rChbxInterno" Width="100%" Checked="false"></telerik:RadCheckBox>
            </div>
            <div class="w3-col s12 m6 l3">
                <label style="font-size: x-small">- Administrativo, no recibe cuentas de asignación.</label>
                <label style="font-size: x-small">- Operativo, si recibe cuentas en el proceso de asignación.</label>
            </div>
        </telerik:RadAjaxPanel>
        <br />
        <telerik:RadInputManager runat="server" ID="inputManagerTI">
            <telerik:RegExpTextBoxSetting BehaviorID="RagExpUSR" Validation-IsRequired="true"
                ValidationExpression="[A-Za-z0-9._]+" ErrorMessage="Usuario no inválido">
                <TargetControls>
                    <telerik:TargetInput ControlID="txtUsuario"></telerik:TargetInput>
                </TargetControls>
            </telerik:RegExpTextBoxSetting>
            <telerik:RegExpTextBoxSetting BehaviorID="RagExpnombre" Validation-IsRequired="true"
                ValidationExpression="[A-Za-z0-9 áéíóúüÁÉÍÓÚÜ]+" ErrorMessage="Nombre no inválido">
                <TargetControls>
                    <telerik:TargetInput ControlID="txtNombre"></telerik:TargetInput>
                </TargetControls>
            </telerik:RegExpTextBoxSetting>
            <telerik:RegExpTextBoxSetting BehaviorID="RagExpMOTIVO" Validation-IsRequired="false"
                ValidationExpression="[ a-zA-Z0-9\,\'\\\-\+\*\/\$\%\(\)\.]+" ErrorMessage="No se admiten caracteres especiales" ClearValueOnError="false" SelectionOnFocus="CaretToBeginning">
                <TargetControls>
                    <telerik:TargetInput ControlID="txtMotivo"></telerik:TargetInput>
                </TargetControls>
            </telerik:RegExpTextBoxSetting>
        </telerik:RadInputManager>
    </fieldset>
</telerik:RadAjaxPanel>
<div class="container">
    <div class="row">
        <div class="col-6 px-2">
            <telerik:RadButton runat="server" ID="btnGuardar" Text="Guardar" SingleClick="true" SingleClickText="Guardando..." CommandName='<%# IIf((TypeOf (Container) Is GridEditFormInsertItem), "PerformInsert", "Update")%>' CssClass="bg-success text-white border-0 w3-block" />
        </div>
        <div class="col-6 px-2">
            <telerik:RadButton runat="server" ID="btnCancelar" Text="Cancelar" CommandName="Cancel" CausesValidation="false" CssClass="bg-danger text-white border-0 w3-block"></telerik:RadButton>
        </div>
    </div>
</div>
<telerik:RadScriptBlock runat="server">
    <script>
        checkPasswordMatch = function () {
            var text1 = $find("<%=txtContrasena.ClientID %>");
            var text2 = $find("<%=txtRepiteContrasena.ClientID %>");
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
            var minMayus = parseInt('<%=pwdMayusLbl.Text %>');
            var minMinus = parseInt('<%=pwdMinusLbl.Text %>');
            var minNums = parseInt('<%=pwdNumsLbl.Text %>');
            var minSpecial = parseInt('<%=pwdSpecialLbl.Text %>');
            var minLong = parseInt('<%=pwdMinLongLbl.Text %>');
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
</telerik:RadScriptBlock>
