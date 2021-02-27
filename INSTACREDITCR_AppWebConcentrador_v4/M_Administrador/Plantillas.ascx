﻿<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Plantillas.ascx.vb" Inherits="Plantillas" %>

<script type="text/javascript">
    function Count() {

        var i = document.getElementById("ctl00_CPHMaster_RGVPlantillas_ctl00_ctl02_ctl03_EditFormControl_TxtCAT_PL_MENSAJE").value.length;
        document.getElementById("ctl00_CPHMaster_RGVPlantillas_ctl00_ctl02_ctl03_EditFormControl_LblContar").innerHTML = '<b> Caracteres: </b>' + i + '  <b>SMS: </b>' + Math.ceil(i/160) ;

    }
</script>


<div class="container">
    <h3>Configuración de plantilla
    </h3>
    <small class="text-muted">Arrastra las etiquetas que necesites al recuadro de texto.</small>
    <br />
    <telerik:RadLabel ID="LblCAT_PL_NOMBRE" runat="server" Text="Nombre De la Plantilla"></telerik:RadLabel>
    <telerik:RadTextBox ID="TxtCAT_PL_NOMBRE" runat="server" Width="100%" MaxLength="99" Text='<%# Bind("Nombre") %>'></telerik:RadTextBox>
    <div class="row my-2">
        <div class="col-md-4">
            Etiquetas
            <telerik:RadListBox ID="RLBEtiquetas" runat="server" EnableDragAndDrop="true" OnDropped="RLBEtiquetas_Dropped" AllowTransferDuplicates="true" ButtonSettings-ShowTransferAll="false" ButtonSettings-ShowDelete="false" ButtonSettings-ShowTransfer="true" ButtonSettings-ShowReorder="false" Height="96px" Width="100%"></telerik:RadListBox>
        </div>
        <div class="col-md-8">
            Mensaje de texto
            <telerik:RadTextBox ID="TxtCAT_PL_MENSAJE" runat="server" Height="96px" MaxLength="480" TextMode="MultiLine" onkeyup="Count()"  Width="100%" Text='<%# Bind("Mensaje") %>'></telerik:RadTextBox>
            <asp:Label runat="server" ID="LblContar" ></asp:Label>
        </div>
    </div>
    <div class="row justify-content-center">
        <div class="col">
            <telerik:RadButton ID="BtnAccion" runat="server" Text="Actualizar" CommandName="Update" Visible='<%# Not (TypeOf DataItem Is Telerik.Web.UI.GridInsertionObject) %>' CssClass="bg-success text-white border-0"></telerik:RadButton>
            <telerik:RadButton ID="btnInsert" Text="INSERTAR" runat="server" CommandName="PerformInsert"
                Visible='<%# (TypeOf DataItem Is Telerik.Web.UI.GridInsertionObject) %>' CssClass="bg-success text-white border-0">
            </telerik:RadButton>
            <telerik:RadButton ID="RBtnCacelar" runat="server" Text="Cancelar" CommandName="Cancel" CssClass="bg-danger text-white border-0"></telerik:RadButton>

        </div>
    </div>
</div>