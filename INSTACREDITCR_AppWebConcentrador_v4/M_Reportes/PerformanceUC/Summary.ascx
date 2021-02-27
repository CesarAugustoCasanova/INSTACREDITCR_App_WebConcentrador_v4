<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Summary.ascx.vb" Inherits="M_Reportes_PerformanceUC_Summary" %>

<telerik:RadGrid runat="server" ID="rg1">
    <MasterTableView>
        <ColumnGroups>
            <telerik:GridColumnGroup Name="General" HeaderText="Plazas"></telerik:GridColumnGroup>
            <telerik:GridColumnGroup Name="Asignacion" ParentGroupName="General" HeaderText="Asignacion"></telerik:GridColumnGroup>
            <telerik:GridColumnGroup Name="Recuperacion" ParentGroupName="General" HeaderText="Recuperacion"></telerik:GridColumnGroup>
        </ColumnGroups>
    </MasterTableView>
</telerik:RadGrid>