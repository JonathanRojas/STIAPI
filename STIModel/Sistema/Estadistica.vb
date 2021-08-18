Imports System.Data
Imports CamaraDeDiputadosLibrary
Imports CamaraDeDiputadosLibrary.Application
Imports CamaraDeDiputadosLibrary.Commons
Imports CamaraDeDiputadosLibrary.DB
Imports Oracle.ManagedDataAccess.Client
Namespace STI
    Public Class Estadistica
        Public Property Adm As Integer
        Public Property Leg As Integer
        Public Property AdmLast As Integer
        Public Property LegLast As Integer
        Public Property Bd As Integer
        Public Property Sis As Integer
        Public Property BdLast As Integer
        Public Property SisLast As Integer
        Public Property BdGraph As String
        Public Property SisGraph As String
        Public Property Meses As String
        Public Shared Function getEstadisticas() As Response(Of Estadistica)
            Dim vServidor As New AdministrativoServer

            Dim vResult As Application.Response(Of Estadistica) = Nothing
            Dim vDa As OracleDataAdapter = Nothing
            Try
                vDa = New OracleDataAdapter(New OracleCommand("SISTEMASCAMARA.PCK_INFORMATICA.""Sistema.GetEstadisticas"""))
                vDa.SelectCommand.Parameters.Add("prmItems", OracleDbType.RefCursor, Data.ParameterDirection.Output)

                Dim vDataRow As DataRow = Nothing
                If vServidor.getData(vDa, vDataRow, vResult) Then
                    Dim vRequerimientosList As New List(Of Estadistica)

                    Dim vEstadistica As Estadistica = DBUtilities.Mapeo(Of Estadistica)(vDataRow)
                    vResult.Data = vEstadistica
                End If
            Catch ex As Exception
                vResult = Response(Of Estadistica).GetResponseErrorDB(ex)
            Finally
                If vDa IsNot Nothing Then vDa.Dispose()
            End Try

            Return vResult
        End Function
    End Class
End Namespace
