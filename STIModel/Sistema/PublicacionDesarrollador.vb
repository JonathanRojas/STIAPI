Imports System.Data
Imports CamaraDeDiputadosLibrary
Imports CamaraDeDiputadosLibrary.Application
Imports CamaraDeDiputadosLibrary.Commons
Imports CamaraDeDiputadosLibrary.DB
Imports Oracle.ManagedDataAccess.Client
Namespace STI
    Public Class PublicacionDesarrollador
        Public Property Tipo As Integer
        Public Property Desarrollador As String
        Public Property Imagen As String
        Public Property Fecha As Date
        Public Property Codigo As String
        Public Property Sistema As String
        Public Shared Function getPublicaciones() As Response(Of List(Of PublicacionDesarrollador))
            Dim vServidor As New AdministrativoServer

            Dim vResult As Application.Response(Of List(Of PublicacionDesarrollador)) = Nothing
            Dim vDa As OracleDataAdapter = Nothing
            Try
                vDa = New OracleDataAdapter(New OracleCommand("SISTEMASCAMARA.PCK_INFORMATICA.""Sistema.GetPublicaciones"""))
                vDa.SelectCommand.Parameters.Add("prmItems", OracleDbType.RefCursor, Data.ParameterDirection.Output)

                Dim vDataTable As New DataTable
                If vServidor.getData(vDa, vDataTable, vResult) Then
                    Dim vPublicacionList As New List(Of PublicacionDesarrollador)

                    For Each vRow As DataRow In vDataTable.Rows
                        Dim vPublicacion As PublicacionDesarrollador = DBUtilities.Mapeo(Of PublicacionDesarrollador)(vRow)
                        If vRow.Table.Columns.Contains("Imagen") AndAlso Not IsDBNull(vRow("Imagen")) Then vPublicacion.Imagen = Convert.ToBase64String(vRow("Imagen"))
                        vPublicacionList.Add(vPublicacion)
                    Next

                    vResult.Data = vPublicacionList
                End If
            Catch ex As Exception
                vResult = Response(Of List(Of PublicacionDesarrollador)).GetResponseErrorDB(ex)
            Finally
                If vDa IsNot Nothing Then vDa.Dispose()
            End Try

            Return vResult
        End Function
    End Class
End Namespace
