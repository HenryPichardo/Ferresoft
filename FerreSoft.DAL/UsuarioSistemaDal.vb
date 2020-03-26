Imports FerreSoft.Entities
Imports System.Data
Imports System.Data.SqlClient
Public Class UsuarioSistemaDal
    Inherits BaseDal
    Public Function Login(user As String, pass As String) As Boolean
        Using connection = GetConnection()
            connection.Open()
            Using Command = New SqlCommand()
                Command.Connection = connection
                Command.CommandText = "select * from UsuarioSistema where Usuario=@user and Password=@pass"
                Command.Parameters.AddWithValue("@user", user)
                Command.Parameters.AddWithValue("@pass", pass)
                Command.CommandType = CommandType.Text
                Dim reader = Command.ExecuteReader()
                If reader.HasRows Then
                    While reader.Read()
                        UsuarioActivo.IdUsuarioSistema = reader.GetInt32(0)
                        UsuarioActivo.Nombre = reader.GetString(4)
                        UsuarioActivo.Apellido = reader.GetString(5)
                        UsuarioActivo.Posicion = reader.GetString(6)
                        UsuarioActivo.Email = reader.GetString(7)

                    End While
                    reader.Dispose()
                    Return True
                Else
                    Return False
                    End If

            End Using
        End Using

    End Function
End Class
