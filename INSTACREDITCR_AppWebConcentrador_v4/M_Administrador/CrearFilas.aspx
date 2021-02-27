<%@ Page Title="Filas de trabajo" Language="VB" MasterPageFile="MasterPage.master" CodeFile="CrearFilas.aspx.vb" Inherits="CrearFilas" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<asp:Content ID="Content2" ContentPlaceHolderID="CPHMaster" runat="Server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="Edicion" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">

        <asp:HiddenField ID="HidenUrs" runat="server" />
         
        <div class="container align-content-center" runat="server" id="Filas">
            <div class="Titulos">Crear filas de trabajo</div>
            <div>
                <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" Font-Bold="true" Text="PRB"></asp:Label>
            </div>
            <div class="w3-center">

                <asp:Button ID="CrearFila" CssClass="w3-btn w3-round-xlarge w3-blue" runat="server" Text="Crear fila de trabajo" OnClick="CrearFila_Click" />
            </div>
            <br />
            <div>

                <telerik:RadGrid runat="server" ID="GrVwFilasCreadas" Width="100%" AutoGenerateColumns="false">
                    <MasterTableView>
                        <Columns>
                            <telerik:GridBoundColumn HeaderText="#" DataField="ID"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Fila" DataField="Fila"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Descripción" DataField="Descripcion"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Estado" DataField="Estado"></telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="Editar">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImgModificar" runat="server" CommandName="Editar" ImageUrl="~/M_Administrador/Imagenes/Editar.png" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
        </div>

        <div class="container" runat="server" id="Filas2">
            <asp:Button runat="server" ID="btnRegresar" Text="Regresar" CssClass="btn btn-link"/>
            <div class="Titulos">Editar fila de trabajo</div>
            <div class="w3-row-padding">
                <div class="w3-col l3">
                    <label class="LblDesc">Nombre de fila: </label>
                    <asp:TextBox ID="txtBxNombreFila" runat="server" Width="100%" ToolTip="No mayor de 50 caracteres" MaxLength="50"></asp:TextBox>
                </div>
                <div class="w3-col l3">
                    <label class="LblDesc">Descripción: </label>
                    <asp:TextBox ID="txtBx1DescrpcionFila" runat="server" Width="100%" ToolTip="No mayor de 150 caracteres" MaxLength="150"></asp:TextBox>
                </div>
                <div class="w3-col l3">
                     <label class="LblDesc">Gestores: </label>
                     <telerik:RadComboBox ID="CbGestores" runat="server" CheckBoxes="True" Width="100%" Localization-AllItemsCheckedString="Todos" EnableCheckAllItemsCheckBox="true" Culture="mx-ES"></telerik:RadComboBox>
                </div>
                
                 <div class="w3-col l3" runat="server" id="MUESTRA_COLOR">
                     <asp:Label ID="lblcolor" runat="server" class="LblDesc">Seleccione el color que se reflejara en el calendario</asp:Label>
                    <telerik:RadColorPicker ID="RCPColorfila" runat="server" ShowEmptyColor="false" AutoPostBack="true" KeepInScreenBounds="true" ShowIcon="true" PickColorText="Color seleccionado" PaletteModes="All"></telerik:RadColorPicker>
                </div>
            </div>
            <div class="w3-row-padding">
                <div class="w3-col l3">
                    <asp:Label ID="lblConteo1" runat="server" CssClass="LblDesc" Text="Total de cuentas seleccionadas: "></asp:Label>
                    <asp:Label ID="lblConteo2" runat="server" Width="100%" CssClass="LblDesc" ForeColor="Red"></asp:Label>
                </div>
                <div class="w3-col l3">
                    <asp:CheckBox ID="chbxHabilitado" Width="100%" runat="server" Text="Habilitado" CssClass="LblDesc" Checked="true" Visible="false" />
                </div>
                <div class="w3-col l3">
                    <asp:Label ID="Label1" runat="server" class="LblDesc">Fila Numero</asp:Label>
                     <telerik:RadLabel runat="server" Width="100%" ID="LblId" Enabled="false"></telerik:RadLabel>
                </div>
                <%--<div class="w3-col l3">
                     <asp:ImageButton ID="imgbtnSave" runat="server" ImageUrl="~/M_Administrador/Imagenes/Guardar.png" Height="30px" Width="36px" OnClick="imgbtnSave_Click" Visible="false" />
                    <asp:ImageButton ID="imgbtnActualiza" runat="server" ImageUrl="~/M_Administrador/Imagenes/Check.png" Visible="false" Height="30px" Width="36px" OnClick="imgbtnActualiza_Click" />
                </div>--%>
            </div>
                   <asp:Panel runat="server" ID="PnlDatos"  CssClass="w3-container">
                        <div class="w3-panel Titulos">
                            <b>Parametrización</b>
                        </div>
                        <div class="w3-row w3-text-black" style="overflow: auto; max-height: 400px; max-width: 100%">
                            <telerik:RadGrid runat="server" ID="gridDispersion">
                                <MasterTableView AutoGenerateColumns="false" CommandItemDisplay="top">
                                    <CommandItemSettings AddNewRecordText="Agregar Parametro" CancelChangesText="Cancelar" SaveChangesText="Guardar" RefreshText="Actualizar" />
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderText="Eliminar">
                                            <ItemTemplate>
                                                <telerik:RadButton runat="server" Text="Eliminar" CommandName="onDelete"></telerik:RadButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn UniqueName="No" HeaderText="#" DataField="ORDEN">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="Tabla" HeaderText="Tabla" DataField="DESCRIPCIONTABLA">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="Campo" HeaderText="Campo" DataField="DESCRIPCIONCAMPO">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="Condicion" HeaderText="Condicion" DataField="DESCRIPCIONOPERADOR">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="Valor" HeaderText="Valor" DataField="Valor">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="Conector" HeaderText="Conector" DataField="DESCRIPCIONCONECTOR">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridEditCommandColumn ButtonType="FontIconButton" HeaderText="Editar"></telerik:GridEditCommandColumn>
                                    </Columns>
                                    <EditFormSettings UserControlName="grids/configReglas/Reglas.ascx" EditFormType="WebUserControl">
                                        <EditColumn UniqueName="EditCommandColumn1">
                                        </EditColumn>
                                    </EditFormSettings>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>
                    </asp:Panel>
            <br />
            <br />
            <div class="w3-center">

                <asp:Button ID="BtnAceptar" CssClass="w3-btn w3-round-xlarge w3-blue-gray" runat="server" Text="Aceptar" OnClick="CrearFila_Click" />
            </div>
        </div>


      <%--  <table runat="server" id="" aling="center" class="Table">
            <tr class="Titulos">
                <td></td>
            </tr>
            <tr>
                <td>&nbsp;</td>

            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr aling="center">
                <td>&nbsp;</td>
            </tr>
            <tr aling="center">
                <td>
                   
                </td>
            </tr>
            <tr aling="right">
                <td>

                    <table class="Table2">
                        <tr>
                            <td>
                                
                            </td>
                            <td>
                                
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>
                                
                            </td>
                            <td>
                                
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>
                               
                            </td>
                            <td>
                               

                            </td>
                        </tr>
                        <tr>
                            <td runat="server" id="">
                                
                            </td>
                            <td>
                                

                            </td>
                        </tr>
                    </table>
                    


                </td>
                <td></td>
            </tr>
            <tr>
                <td style="text-align: left">
                   
                </td>
            </tr>
            <tr aling="center">
                <td colspan="2">
                    
                </td>
            </tr>
            <tr>
                <td>
             
                </td>
            </tr>
            <tr aling="center">
                <td colspan="2">&nbsp;</td>
            </tr>
            <tr aling="center">
                <td colspan="2">
                    <table class="Table" runat="server" visible="false">
                        <tr>
                            <td class="Titulos">Categoria</td>
                            <td runat="server" id="Subcat" class="Titulos" visible="false">Subcategoria</td>
                        </tr>
                        <tr>

                            <td style="text-align: center">
                                <telerik:RadListBox ID="RLBFilasOrigen" runat="server" AutoPostBack="true" AllowTransferDuplicates="true" ButtonSettings-ShowTransferAll="false" ButtonSettings-ShowDelete="false" ButtonSettings-ShowTransfer="true" ButtonSettings-ShowReorder="false"></telerik:RadListBox>


                            </td>
                            <td>
                                <telerik:RadGrid ID="RGVValores" runat="server" RenderMode="Lightweight" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true" PageSize="20"
                                    ClientSettings-EnableRowHoverStyle="true" OnNeedDataSource="RGVValores_NeedDataSource" Culture="es-MX" Visible="false" AllowMultiRowSelection="false" MasterTableView-RowIndicatorColumn-AutoPostBackOnFilter="true">

                                    <MasterTableView Width="100%" CommandItemDisplay="None" EditFormSettings-PopUpSettings-KeepInScreenBounds="true" DataKeyNames="Comentario">

                                        <Columns>

                                            <telerik:GridButtonColumn Text="Seleccionar" ItemStyle-Width="15px" CommandName="Select" ButtonType="LinkButton">
                                            </telerik:GridButtonColumn>
                                            <telerik:GridBoundColumn UniqueName="Comentario" HeaderText="Comentario" DataField="Comentario">
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn UniqueName="Campo" HeaderText="Categoria" DataField="Campo">
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn UniqueName="Tabla" HeaderText="Tabla" DataField="Tabla">
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn UniqueName="Tipo" HeaderText="Tipo" DataField="Tipo">
                                            </telerik:GridBoundColumn>
                                        </Columns>

                                    </MasterTableView>
                                    <ClientSettings EnableRowHoverStyle="true">
                                        <Selecting AllowRowSelect="True"></Selecting>

                                    </ClientSettings>
                                </telerik:RadGrid>


                            </td>
                            <td>

                                <div id="Div2" runat="server" class="ScroolCamposReportes2">
                                <table>
                                    <tr>
                                        <td>

                                            <asp:GridView ID="grVwFiltrosFin" runat="server" Style="margin-right: 0px" CssClass="mGrid" AutoGenerateColumns="true" Visible="false" OnRowDataBound="grVwFiltrosFin_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField ShowHeader="false">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnQuitFintroFin" runat="server" ImageUrl="~/M_Administrador/Imagenes/Quitar.ico" OnClick="btnQuitFintroFin_Click" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>

                                        </td>
                                    </tr>
                                    <tr>


                                        <td>
                                            <asp:GridView ID="grVwFiltrosNormales" runat="server" Style="margin-right: 0px" CssClass="mGrid" OnRowDataBound="grVwFiltros_RowDataBound" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:TemplateField ShowHeader="false">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnQuitFintroNorm" runat="server" ImageUrl="~/M_Administrador/Imagenes/Quitar.ico" OnClick="btnQuitFintroNorm_Click" />
                                                            <asp:ImageButton ID="btnAddFiltroNorm" runat="server" ImageUrl="~/M_Administrador/Imagenes/Agregar.png" OnClick="btnAddFiltroNorm_Click" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="Campo" HeaderText="Campo" />
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="drpdwlstCondicion0" runat="server">
                                                                <asp:ListItem Selected="True">Condición</asp:ListItem>
                                                                <asp:ListItem Value="IN">Igual a</asp:ListItem>
                                                                <asp:ListItem Value="NOT IN">Diferente de</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Filtro">
                                                        <ItemTemplate>
                                                            <telerik:RadComboBox ID="CBListaDe" runat="server" CheckBoxes="True" AutoPostBack="true" OnItemChecked="CBListaDe_ItemChecked"></telerik:RadComboBox>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Valores">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtbxFiltroCheckbx" runat="server" ReadOnly="true"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="field" HeaderText="CampoTabla" />

                                                    <asp:BoundField DataField="tabla" HeaderText="tablaBase" />
                                                    <asp:TemplateField HeaderText="Ordenamiento">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="drpdwlstOrden" runat="server">
                                                                <asp:ListItem Selected="True" Value="asc">Ascendente</asp:ListItem>
                                                                <asp:ListItem Value="desc">Descendente</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>


                                            <asp:GridView ID="grVwFiltrosFechas" runat="server" Style="margin-right: 0px" CssClass="mGrid" AutoGenerateColumns="false" OnRowDataBound="grVwFiltrosFechas_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField ShowHeader="false">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnQuitFintroCal" runat="server" ImageUrl="~/M_Administrador/Imagenes/Quitar.ico" OnClick="btnQuitFintroCal_Click" />
                                                            <asp:ImageButton ID="btnAddFiltroCal" runat="server" ImageUrl="~/M_Administrador/Imagenes/Agregar.png" OnClick="btnAddFiltroCal_Click" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Campo" HeaderText="Campo" />
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="drpdwlstCondicion" runat="server">
                                                                <asp:ListItem Selected="True">Condición</asp:ListItem>
                                                                <asp:ListItem Value=">">Mayor que</asp:ListItem>
                                                                <asp:ListItem Value=">=">Mayor o igual que</asp:ListItem>
                                                                <asp:ListItem Value="=">Igual</asp:ListItem>
                                                                <asp:ListItem Value="<=">Menor o igual que</asp:ListItem>
                                                                <asp:ListItem Value="<">Menor que</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fecha">
                                                        <ItemTemplate>

                                                            <telerik:RadDateTimePicker ID="RDPFintroCal" runat="server"></telerik:RadDateTimePicker>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="field" HeaderText="CampoTabla" />
                                                    <asp:BoundField DataField="tabla" HeaderText="tablaBase" />
                                                    <asp:TemplateField HeaderText="Ordenamiento">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="drpdwlstOrdenFech" runat="server">
                                                                <asp:ListItem Selected="True" Value="asc">Ascendente</asp:ListItem>
                                                                <asp:ListItem Value="desc">Descendente</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:GridView ID="grdvwFiltrosNumericos" runat="server" Style="margin-right: 0px" CssClass="mGrid" AutoGenerateColumns="false" OnRowDataBound="grdvwFiltrosNumericos_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField ShowHeader="false">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnQuitFiltroNum" runat="server" ImageUrl="~/M_Administrador/Imagenes/Quitar.ico" OnClick="btnQuitFiltroNum_Click" />
                                                            <asp:ImageButton ID="btnAddFiltroNum" runat="server" ImageUrl="~/M_Administrador/Imagenes/Agregar.png" OnClick="btnAddFiltroNum_Click" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Campo" HeaderText="Campo" />
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="drpdwlstCondicionNum" runat="server">
                                                                <asp:ListItem Selected="True">Condición</asp:ListItem>
                                                                <asp:ListItem Value=">">Mayor que</asp:ListItem>
                                                                <asp:ListItem Value=">=">Mayor o igual que</asp:ListItem>
                                                                <asp:ListItem Value="=">Igual</asp:ListItem>
                                                                <asp:ListItem Value="<=">Menor o igual que</asp:ListItem>
                                                                <asp:ListItem Value="<">Menor que</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Monto">
                                                        <ItemTemplate>
                                                            <telerik:RadNumericTextBox ID="txtbxValoresNumericos" runat="server" ShowSpinButtons="true" MinValue="0"></telerik:RadNumericTextBox>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="field" HeaderText="CampoTabla" />
                                                    <asp:BoundField DataField="tabla" HeaderText="tablaBase" />
                                                    <asp:TemplateField HeaderText="Ordenamiento">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="drpdwlstOrdenNum" runat="server">
                                                                <asp:ListItem Selected="True" Value="asc">Ascendente</asp:ListItem>
                                                                <asp:ListItem Value="desc">Descendente</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    
                                </table>

                            </td>

                        </tr>

                    </table>
                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
            <tr>
                <td></td>
            </tr>
            <tr>
                <td></td>
            </tr>
            <tr>
                <td></td>
            </tr>
        </table>--%>

        <telerik:RadWindowManager ID="RadAviso" runat="server">
            <Localization OK="Aceptar" />
        </telerik:RadWindowManager>
    </telerik:RadAjaxPanel>
</asp:Content>

