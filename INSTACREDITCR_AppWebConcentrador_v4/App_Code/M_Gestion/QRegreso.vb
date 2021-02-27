Imports Microsoft.VisualBasic

Public Class QRegreso
    Public Shared Function QCampo(ByVal V_CadenaXML As String, ByVal V_Papa As String, ByVal V_Hijo As String) As String
        Dim V_Valor As String = ""
        Dim document As XDocument = XDocument.Parse(V_CadenaXML)
        Dim Nodos = From Nodo In document.Descendants(V_Papa) _
                     Select New With _
                    { _
                     .Name = Nodo.Element(V_Hijo).Value
                   }
        For Each Nodo In Nodos
            V_Valor = Nodo.Name
        Next
        Return V_Valor
    End Function
End Class

