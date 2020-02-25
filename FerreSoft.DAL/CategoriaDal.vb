Imports System.Data.SqlClient
Imports FerreSoft.Entities

Public Class CategoriaDAL
    Inherits BaseDal

    Sub New()

    End Sub

    'Metodos CRUD
    Public Shared Function Create(categoria As CategoriaEntity) As CategoriaEntity
        'Creamos la conexion
        Using conex As New SqlConnection(m_CadenaConexion)
            conex.Open()

            'Creamos la sentencia SQL para agregar un registro
            Dim sql As String = "INSERT INTO Categoria (Nombre) Values(@nombre) SELECT SCOPE_IDENTITY()"
            Dim cmd As New SqlCommand(sql, conex)
            'Agregamos los parametros
            cmd.Parameters.AddWithValue("@nombre", categoria.Nombre)
            categoria.IdCategoria = Convert.ToInt32(cmd.ExecuteScalar())
        End Using

        Return categoria

    End Function
    Public Shared Function Update(categoria As CategoriaEntity) As CategoriaEntity
        Using conex As New SqlConnection(m_CadenaConexion)
            conex.Open()

            'Creamos la sentencia SQL para actualizar un registro
            Dim sql As String = "UPDATE Categoria Set Nombre=@nombre " &
                                "WHERE ID=@idCategoria"
            'Creamos el comando para ejecutar la sentencia SQL con sus parametros
            Dim cmd As New SqlCommand(sql, conex)
            cmd.Parameters.AddWithValue("@nombre", categoria.Nombre)
            cmd.Parameters.AddWithValue("@idCategoria", categoria.IdCategoria)
            'Ejecutamos el comando
            cmd.ExecuteNonQuery()
        End Using

        Return categoria
    End Function
    Public Shared Function Delete(id As Integer) As Boolean
        Dim SeElimino As Boolean

        Using conex As New SqlConnection(m_CadenaConexion)
            conex.Open()

            'Creamos la sentencia SQL
            Dim sql As String = "DELETE FROM Categoria WHERE ID=@idCategoria"
            'Creamos el comando
            Dim cmd As New SqlCommand(sql, conex)
            cmd.Parameters.AddWithValue("@idCategoria", id)

            SeElimino = cmd.ExecuteNonQuery() > 0
        End Using

        Return SeElimino
    End Function
    Public Shared Function GetAll() As List(Of CategoriaEntity)
        Dim list As New List(Of CategoriaEntity)

        Using conex As New SqlConnection(m_CadenaConexion)
            conex.Open()

            'Creamos la sentencia SQL para obtener todos los articulos
            Dim sql As String = "SELECT * FROM Categoria ORDER BY Nombre"
            Dim cmd As New SqlCommand(sql, conex)
            Dim reader As SqlDataReader = cmd.ExecuteReader()

            While reader.Read()
                list.Add(ConvertToObject(reader))
            End While
        End Using

        Return list
    End Function
    Public Shared Function GetByValor(valor As String) As List(Of CategoriaEntity)
        Dim list As New List(Of CategoriaEntity)()

        Using conex As New SqlConnection(m_CadenaConexion)
            conex.Open()

            Dim sql As String = "SELECT * FROM Categoria
                                WHERE Nombre Like '%' + @valor + '%' ORDER BY Nombre"

            Dim cmd As New SqlCommand(sql, conex)
            cmd.Parameters.AddWithValue("@valor", valor)

            Dim reader As SqlDataReader = cmd.ExecuteReader()

            While reader.Read()
                list.Add(ConvertToObject(reader))
            End While
        End Using

        Return list
    End Function
    Public Shared Function GetById(id As Integer) As CategoriaEntity
        Dim categoria As CategoriaEntity = Nothing

        Using conex As New SqlConnection(m_CadenaConexion)
            conex.Open()

            Dim sql As String = "SELECT * FROM Categoria Where ID=@idCategoria"
            Dim cmd As New SqlCommand(sql, conex)
            cmd.Parameters.AddWithValue("@idCategoria", id)
            Dim reader As SqlDataReader = cmd.ExecuteReader()
            If reader.Read() Then
                categoria = ConvertToObject(reader)
            End If
        End Using

        Return categoria

    End Function
    Public Shared Function Exist(id As Integer) As Boolean
        Dim numRegistros As Integer

        Using conex As New SqlConnection(m_CadenaConexion)
            conex.Open()

            Dim sql As String = "SELECT COUNT(*) FROM Categoria WHERE ID=@idCategoria"
            Dim cmd As New SqlCommand(sql, conex)
            cmd.Parameters.AddWithValue("@idCategoria", id)
            numRegistros = Convert.ToInt32(cmd.ExecuteScalar())
        End Using

        Return numRegistros > 0

    End Function

    'Metodo para convertir los datos en objectos
    Private Shared Function ConvertToObject(reader As IDataReader) As CategoriaEntity
        Dim categoria As New CategoriaEntity()

        categoria.IdCategoria = Convert.ToInt32(reader("ID"))
        categoria.Nombre = Convert.ToString(reader("Nombre"))

        Return categoria
    End Function
End Class

