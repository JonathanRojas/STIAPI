Imports System.Data
Imports CamaraDeDiputadosLibrary
Imports CamaraDeDiputadosLibrary.Application
Imports CamaraDeDiputadosLibrary.Commons
Imports CamaraDeDiputadosLibrary.DB
Imports Oracle.ManagedDataAccess.Client
Namespace STI
    Public Class Requerimiento
        Public Property Id As Integer
        Public Property Descripcion As String
        Public Property FechaInicio As Date?
        Public Property FechaTermino As Date?
        Public Property TipoDesarrollador As Tipo
        Public Property TipoRequerimiento As Tipo
        Public Property TipoEstado As Tipo
        Public Shared Function getRequerimientos(prmId As Integer) As Response(Of List(Of Requerimiento))
            Dim vServidor As New AdministrativoServer

            Dim vResult As Application.Response(Of List(Of Requerimiento)) = Nothing
            Dim vDa As OracleDataAdapter = Nothing
            Try
                vDa = New OracleDataAdapter(New OracleCommand("SISTEMASCAMARA.PCK_INFORMATICA.""Sistema.GetRequerimientos"""))
                vDa.SelectCommand.Parameters.Add("prmId", OracleDbType.Int32, Nothing, prmId, Data.ParameterDirection.Input)
                vDa.SelectCommand.Parameters.Add("prmItems", OracleDbType.RefCursor, Data.ParameterDirection.Output)

                Dim vDataTable As New DataTable
                If vServidor.getData(vDa, vDataTable, vResult) Then
                    Dim vRequerimientosList As New List(Of Requerimiento)

                    For Each vRow As DataRow In vDataTable.Rows
                        Dim vRequerimiento As Requerimiento = DBUtilities.Mapeo(Of Requerimiento)(vRow)
                        If vRow.Table.Columns.Contains("FechaInicio") AndAlso Not IsDBNull(vRow("FechaInicio")) Then vRequerimiento.FechaInicio = vRow("FechaInicio")
                        If vRow.Table.Columns.Contains("FechaTermino") AndAlso Not IsDBNull(vRow("FechaTermino")) Then vRequerimiento.FechaTermino = vRow("FechaTermino")
                        vRequerimientosList.Add(vRequerimiento)
                    Next

                    vResult.Data = vRequerimientosList
                End If
            Catch ex As Exception
                vResult = Response(Of List(Of Requerimiento)).GetResponseErrorDB(ex)
            Finally
                If vDa IsNot Nothing Then vDa.Dispose()
            End Try

            Return vResult
        End Function
    End Class
End Namespace
