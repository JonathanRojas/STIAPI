Imports CamaraDeDiputadosLibrary.Application
Imports CamaraDeDiputadosLibrary.Commons
Imports CamaraDeDiputadosLibrary.DB

Public Module TipoFactory


    Public Function TipoSistemaGetItems() As Response(Of List(Of Tipo))
        Dim vServidor As New CamaraDeDiputadosLibrary.DB.AdministrativoServer

        Dim vReponse As Response(Of List(Of Tipo)) = CamaraDeDiputadosLibrary.DB.DBUtilities.GetItems(Of Tipo)(vServidor, "SISTEMASCAMARA.PCK_INFORMATICA.""TipoSistemas.GetItems""")

        Return vReponse
    End Function

    Public Function TipoEscritorioVirtualGetItems() As Response(Of List(Of Tipo))
        Dim vServidor As New CamaraDeDiputadosLibrary.DB.AdministrativoServer

        Dim vReponse As Response(Of List(Of Tipo)) = CamaraDeDiputadosLibrary.DB.DBUtilities.GetItems(Of Tipo)(vServidor, "SISTEMASCAMARA.PCK_INFORMATICA.""TipoEscritorioVirtual.GetItems""")

        Return vReponse
    End Function

    Public Function TipoBaseDatosGetItems() As Response(Of List(Of Tipo))
        Dim vServidor As New CamaraDeDiputadosLibrary.DB.AdministrativoServer

        Dim vReponse As Response(Of List(Of Tipo)) = CamaraDeDiputadosLibrary.DB.DBUtilities.GetItems(Of Tipo)(vServidor, "SISTEMASCAMARA.PCK_INFORMATICA.""TipoBaseDatos.GetItems""")

        Return vReponse
    End Function

    Public Function TipoEstadoDesarrolloGetItems() As Response(Of List(Of Tipo))
        Dim vServidor As New CamaraDeDiputadosLibrary.DB.AdministrativoServer

        Dim vReponse As Response(Of List(Of Tipo)) = CamaraDeDiputadosLibrary.DB.DBUtilities.GetItems(Of Tipo)(vServidor, "SISTEMASCAMARA.PCK_INFORMATICA.""TipoEstadoDesarrollo.GetItems""")

        Return vReponse
    End Function

    Public Function TipoUsuarioGetItems() As Response(Of List(Of Tipo))
        Dim vServidor As New CamaraDeDiputadosLibrary.DB.AdministrativoServer

        Dim vReponse As Response(Of List(Of Tipo)) = CamaraDeDiputadosLibrary.DB.DBUtilities.GetItems(Of Tipo)(vServidor, "SISTEMASCAMARA.PCK_INFORMATICA.""TipoUsuario.GetItems""")

        Return vReponse
    End Function

    Public Function TipoIISGetItems() As Response(Of List(Of Tipo))
        Dim vServidor As New CamaraDeDiputadosLibrary.DB.AdministrativoServer

        Dim vReponse As Response(Of List(Of Tipo)) = CamaraDeDiputadosLibrary.DB.DBUtilities.GetItems(Of Tipo)(vServidor, "SISTEMASCAMARA.PCK_INFORMATICA.""TipoIIS.GetItems""")

        Return vReponse
    End Function

    Public Function TipoFrameworkWebGetItems() As Response(Of List(Of Tipo))
        Dim vServidor As New CamaraDeDiputadosLibrary.DB.AdministrativoServer

        Dim vReponse As Response(Of List(Of Tipo)) = CamaraDeDiputadosLibrary.DB.DBUtilities.GetItems(Of Tipo)(vServidor, "SISTEMASCAMARA.PCK_INFORMATICA.""TipoFrameworkWeb.GetItems""")

        Return vReponse
    End Function

    Public Function TipoFrameworkNetGetItems() As Response(Of List(Of Tipo))
        Dim vServidor As New CamaraDeDiputadosLibrary.DB.AdministrativoServer

        Dim vReponse As Response(Of List(Of Tipo)) = CamaraDeDiputadosLibrary.DB.DBUtilities.GetItems(Of Tipo)(vServidor, "SISTEMASCAMARA.PCK_INFORMATICA.""TipoFrameworkNet.GetItems""")

        Return vReponse
    End Function
End Module
