<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Resultado.ascx.vb" Inherits="M_Administrador_grids_Codigos_Resultado" %>
<div class="text-center">
    <h2>Configurar Código de Resultado</h2>
    <p class="text-muted">Agregar / Editar Código de Resultado</p>
    <telerik:RadFormDecorator runat="server" DecoratedControls="Fieldset" DecorationZoneID="Decoration" />
    <telerik:RadLabel runat="server" ID="lblID" Visible="false" Text='<%# Eval("id") %>'></telerik:RadLabel>
    <div class="container" id="Decoration">
        <fieldset>
            <div class="row justify-content-center">
                <div class="col-12 col-md-6 my-2">
                    <span>Código de Resultado *</span>
                    <telerik:RadTextBox runat="server" ID="txtCod" Width="100%" EmptyMessage="Código de Resultado" MaxLength="6" Text='<%# Eval("Codigo") %>'></telerik:RadTextBox>
                </div>
                <div class="col-12 col-md-6 my-2">
                    <span>Descripción *</span>
                    <telerik:RadTextBox runat="server" ID="txtDesc" Width="100%" EmptyMessage="Descripción" MaxLength="30" Text='<%# Eval("Descripcion") %>'></telerik:RadTextBox>
                </div>
                <%--<div class="col-12 col-md-6 my-2">
                    <span>Alias Código</span>
                    <telerik:RadTextBox runat="server" ID="txtAliasCod" Width="100%" EmptyMessage="Alias Código" MaxLength="4" Text='<%# Eval("AliasCodigo") %>'></telerik:RadTextBox>
                </div>
                <div class="col-12 col-md-6 my-2">
                    <span>Alias Descripcion</span>
                    <telerik:RadTextBox runat="server" ID="txtAliasDesc" Width="100%" EmptyMessage="Alias Descripcion" MaxLength="30" Text='<%# Eval("AliasDescripcion") %>'></telerik:RadTextBox>
                </div>--%>
                <div class="col-12"></div>
                <div class="col-4 my-2">
                    <asp:Button ID="btnUpdate" Text="Actualizar" runat="server" CommandName="Update" Visible='<%# Not (TypeOf DataItem Is Telerik.Web.UI.GridInsertionObject) %>' CssClass="btn btn-success border"></asp:Button>
                    <asp:Button ID="btnInsert" Text="Añadir" runat="server" CommandName="PerformInsert" Visible='<%# (TypeOf DataItem Is Telerik.Web.UI.GridInsertionObject) %>' CssClass="btn btn-success border"></asp:Button>
                </div>
            </div>
        </fieldset>
    </div>
</div>