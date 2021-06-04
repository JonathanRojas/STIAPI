Imports System.Data
Imports CamaraDeDiputadosLibrary
Imports CamaraDeDiputadosLibrary.Application
Imports CamaraDeDiputadosLibrary.Commons
Imports CamaraDeDiputadosLibrary.DB
Imports Oracle.ManagedDataAccess.Client
Public Class Sistema
    Public Property Id As Integer
    Public Property Nombre As String
    Public Property TipoSistema As Tipo
    Public Property Url As String
    Public Property Descripcion As String
    Public Shared Function getSistemas() As Response(Of List(Of Sistema))
        Dim vServidor As New AdministrativoServer

        Dim vResult As Application.Response(Of List(Of Sistema))
        Dim vDa As OracleDataAdapter
        Try
            vDa = New OracleDataAdapter(New OracleCommand("SISTEMASCAMARA.PCK_INFORMATICA.""Sistema.GetSistemas"""))
            vDa.SelectCommand.Parameters.Add("prmItems", OracleDbType.RefCursor, Data.ParameterDirection.Output)

            Dim vDataTable As New DataTable
            If vServidor.getData(vDa, vDataTable, vResult) Then
                Dim vSistemasList As New List(Of Sistema)

                For Each vRow As DataRow In vDataTable.Rows
                    Dim vSistema As Sistema = DBUtilities.Mapeo(Of Sistema)(vRow)
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

        Dim vResult As Application.Response(Of String)
        Dim vDa As OracleCommand
        Try
            vDa = New OracleCommand("SISTEMASCAMARA.PCK_INFORMATICA.""Sistema.SetSistema""")
            vDa.Parameters.Add("prmId", OracleDbType.Int32, Nothing, prmSistema.Id, Data.ParameterDirection.Input)
            vDa.Parameters.Add("prmDescripcion", OracleDbType.Varchar2, 1000, prmSistema.Descripcion, Data.ParameterDirection.Input)
            vDa.Parameters.Add("prmError", OracleDbType.Varchar2, 100, Nothing, Data.ParameterDirection.Output)
            Dim vDataRow As DataRow

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
