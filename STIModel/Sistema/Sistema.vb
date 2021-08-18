Imports System.Data
Imports CamaraDeDiputadosLibrary
Imports CamaraDeDiputadosLibrary.Application
Imports CamaraDeDiputadosLibrary.Commons
Imports CamaraDeDiputadosLibrary.DB
Imports Oracle.ManagedDataAccess.Client
Namespace STI
    Public Class Sistema
#Region "Propiedades"
        Public Property Id As Integer
        Public Property Codigo As String
        ReadOnly Property Area As String
            Get
                If Codigo.Substring(0, 1) = "A" Then
                    Return "Administrativo"
                Else
                    Return "Legislativo"
                End If
            End Get
        End Property
        Public Property Nombre As String
        Public Property Url As String
        Public Property UbicacionTeam As String
        Public Property Descripcion As String
        Public Property Observacion As String
        Public Property Desarrollador As String
        Public Property FechaPublicacion As Date?
        Public Property TipoVersionTeam As Tipo
        Public Property TipoSistema As Tipo
        Public Property TipoEscritorioVirtual As Tipo
        Public Property TipoBaseDatos As Tipo
        Public Property TipoEstadoDesarrollo As Tipo
        Public Property TipoUsuario As Tipo
        Public Property TipoIIS As Tipo
        Public Property TipoFrameworkWeb As Tipo
        Public Property TipoFrameworkNet As Tipo
#End Region
        Public Shared Function getSistemas() As Response(Of List(Of Sistema))
            Dim vServidor As New AdministrativoServer

            Dim vResult As Application.Response(Of List(Of Sistema)) = Nothing
            Dim vDa As OracleDataAdapter = Nothing
            Try
                vDa = New OracleDataAdapter(New OracleCommand("SISTEMASCAMARA.PCK_INFORMATICA.""Sistema.GetSistemas"""))
                vDa.SelectCommand.Parameters.Add("prmItems", OracleDbType.RefCursor, Data.ParameterDirection.Output)

                Dim vDataTable As New DataTable
                If vServidor.getData(vDa, vDataTable, vResult) Then
                    Dim vSistemasList As New List(Of Sistema)

                    For Each vRow As DataRow In vDataTable.Rows
                        Dim vSistema As Sistema = DBUtilities.Mapeo(Of Sistema)(vRow)
                        If vRow.Table.Columns.Contains("FechaPublicacion") AndAlso Not IsDBNull(vRow("FechaPublicacion")) Then vSistema.FechaPublicacion = vRow("FechaPublicacion")
                        vSistemasList.Add(vSistema)
                    Next

                    vResult.Data = vSistemasList
                End If
            Catch ex As Exception
                vResult = Response(Of List(Of Sistema)).GetResponseErrorDB(ex)
            Finally
                If vDa IsNot Nothing Then vDa.Dispose()
            End Try

            Return vResult
        End Function
        Public Shared Function setSistema(ByVal prmSistema As Sistema) As Response(Of String)
            Dim vServidor As New CamaraDeDiputadosLibrary.DB.AdministrativoServer
            Dim vResult As Application.Response(Of String) = Nothing
            Dim vDa As OracleCommand = Nothing
            Dim vXML As String = CamaraDeDiputadosLibrary.DB.DBUtilities.ToXml(prmSistema)
            Try
                vDa = New OracleCommand("SISTEMASCAMARA.PCK_INFORMATICA.""Sistema.SetSistema""")
                vDa.Parameters.Add("prmSistema", OracleDbType.XmlType, Nothing, vXML, Data.ParameterDirection.Input)
                vDa.Parameters.Add("prmError", OracleDbType.Varchar2, 100, Nothing, Data.ParameterDirection.Output)
                Dim vDataRow As DataRow = Nothing

                If vServidor.ExecuteCommand(vDa, vResult) Then
                    vResult.Data = vDa.Parameters("prmError").Value.ToString
                End If
            Catch ex As Exception
                vResult = Response(Of String).GetResponseErrorDB(ex)
            Finally
                If vDa IsNot Nothing Then vDa.Dispose()
            End Try
            Return vResult
        End Function
    End Class
End Namespace