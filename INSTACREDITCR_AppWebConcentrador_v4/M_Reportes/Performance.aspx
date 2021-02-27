<%@ Page Title="" Language="VB" MasterPageFile="~/M_Reportes/MasterPage.master" AutoEventWireup="false" CodeFile="Performance.aspx.vb" Inherits="Performance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMaster" runat="Server">
    <h1 class="text-center">Performance Agencias y Plazas</h1>
    <telerik:RadSpreadsheet runat="server" ID="ssPerformance" OnClientChangeFormat="c">
    </telerik:RadSpreadsheet>
    <script type="text/javascript">
        function c(a, b, c, d) {
            console.log(
                b.get_range().get_format()
            )


        }
    </script>
</asp:Content>
