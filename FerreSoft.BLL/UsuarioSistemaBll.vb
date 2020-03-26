Imports FerreSoft.DAL
Public Class UsuarioSistemaBll
    Dim usuarioSistemaDal As New UsuarioSistemaDal()
    Public Function Login(user As String, pass As String) As Boolean
        Return usuarioSistemaDal.Login(user, pass)
    End Function

    Public Sub Asegurar()

    End Sub
End Class
