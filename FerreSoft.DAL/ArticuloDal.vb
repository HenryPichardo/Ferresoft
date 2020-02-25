Imports System.Data.SqlClient
Imports FerreSoft.Entities

Public Class ArticuloDAL
    Inherits BaseDal

    Sub New()

    End Sub

    'Metodos CRUD
    Public Shared Function Create(articulo As ArticuloEntity) As ArticuloEntity
        'Creamos la conexion
        Using conex As New SqlConnection(m_CadenaConexion)
            conex.Open()

            'Creamos la sentencia SQL para agregar un registro
            Dim sql As String = "INSERT INTO Articulo (IdCategoria, Nombre, Descripcion, PrecioCompra, PrecioVenta, Stock)" &
                               "Values(@idCategoria, @nombre, @descripcion, @precioCompra, @precioVenta, @stock)" &
                               "SELECT SCOPE_IDENTITY()"
            Dim cmd As New SqlCommand(sql, conex)
            'Agregamos los parametros
            cmd.Parameters.AddWithValue("@nombre", articulo.Nombre)
            cmd.Parameters.AddWithValue("@descripcion", articulo.Descripcion)
            cmd.Parameters.AddWithValue("@precioCompra", articulo.PrecioCompra)
            cmd.Parameters.AddWithValue("@precioVenta", articulo.PrecioVenta)
            cmd.Parameters.AddWithValue("@stock", articulo.Stock)
            cmd.Parameters.AddWithValue("@idCategoria", articulo.IdCategoria)
            articulo.IdArticulo = Convert.ToInt32(cmd.ExecuteScalar())
        End Using

        Return articulo

    End Function
    Public Shared Function Update(articulo As ArticuloEntity) As ArticuloEntity
        Using conex As New SqlConnection(m_CadenaConexion)
            conex.Open()

            'Creamos la sentencia SQL para actualizar un registro
            Dim sql As String = "UPDATE Articulo Set 
                                IdCategoria=@idCategoria, 
                                Nombre=@nombre, 
                                Descripcion=@descripcion, 
                                PrecioCompra=@precioCompra, 
                                PrecioVenta=@precioVenta, 
                                Stock=@stock WHERE ID=@idArticulo"

            'Creamos el comando para ejecutar la sentencia SQL con sus parametros
            Dim cmd As New SqlCommand(sql, conex)
            cmd.Parameters.AddWithValue("@nombre", articulo.Nombre)
            cmd.Parameters.AddWithValue("@descripcion", articulo.Descripcion)
            cmd.Parameters.AddWithValue("@precioCompra", articulo.PrecioCompra)
            cmd.Parameters.AddWithValue("@precioVenta", articulo.PrecioVenta)
            cmd.Parameters.AddWithValue("@stock", articulo.Stock)
            cmd.Parameters.AddWithValue("@idArticulo", articulo.IdArticulo)
            cmd.Parameters.AddWithValue("@idCategoria", articulo.IdCategoria)
            'Ejecutamos el comando
            cmd.ExecuteNonQuery()
        End Using

        Return articulo
    End Function
    Public Shared Function Delete(id As Integer) As Boolean
        Dim SeElimino As Boolean

        Using conex As New SqlConnection(m_CadenaConexion)
            conex.Open()

            'Creamos la sentencia SQL
            Dim sql As String = "DELETE FROM Articulo WHERE ID=@IdArticulo"
            'Creamos el comando
            Dim cmd As New SqlCommand(sql, conex)
            cmd.Parameters.AddWithValue("@IdArticulo", id)

            SeElimino = cmd.ExecuteNonQuery() > 0
        End Using

        Return SeElimino
    End Function
    Public Shared Function GetAll() As List(Of ArticuloEntity)
        Dim list As New List(Of ArticuloEntity)

        Using conex As New SqlConnection(m_CadenaConexion)
            conex.Open()

            'Creamos la sentencia SQL para obtener todos los articulos
            Dim sql As String = "SELECT * FROM Articulo ORDER BY Nombre"
            Dim cmd As New SqlCommand(sql, conex)
            Dim reader As SqlDataReader = cmd.ExecuteReader()

            While reader.Read()
                list.Add(ConvertToObject(reader))
            End While
        End Using

        Return list
    End Function
    Public Shared Function GetByValor(valor As String) As List(Of ArticuloEntity)
        Dim list As New List(Of ArticuloEntity)()

        Using conex As New SqlConnection(m_CadenaConexion)
            conex.Open()

            Dim sql As String = "SELECT * FROM Articulo
                                WHERE Nombre Like '%' + @valor + '%' or Descripcion Like '%' + @valor + '%' 
                                ORDER BY Nombre"
            Dim cmd As New SqlCommand(sql, conex)
            cmd.Parameters.AddWithValue("@valor", valor)

            Dim reader As SqlDataReader = cmd.ExecuteReader()

            While reader.Read()
                list.Add(ConvertToObject(reader))
            End While
        End Using

        Return list
    End Function
    Public Shared Function GetById(id As Integer) As ArticuloEntity
        Dim articulo As ArticuloEntity = Nothing

        Using conex As New SqlConnection(m_CadenaConexion)
            conex.Open()

            Dim sql As String = "SELECT * FROM Articulo Where ID=@IdArticulo"
            Dim cmd As New SqlCommand(sql, conex)
            cmd.Parameters.AddWithValue("@IdArticulo", id)
            Dim reader As SqlDataReader = cmd.ExecuteReader()
            If reader.Read() Then
                articulo = ConvertToObject(reader)
            End If
        End Using

        Return articulo

    End Function
    Public Shared Function Exist(id As Integer) As Boolean
        Dim numRegistros As Integer

        Using conex As New SqlConnection(m_CadenaConexion)
            conex.Open()

            Dim sql As String = "SELECT COUNT(*) FROM Articulo WHERE ID=@IdArticulo"
            Dim cmd As New SqlCommand(sql, conex)
            cmd.Parameters.AddWithValue("@IdArticulo", id)
            numRegistros = Convert.ToInt32(cmd.ExecuteScalar())
        End Using

        Return numRegistros > 0

    End Function

    'Metodo para convertir los datos en objectos
    Private Shared Function ConvertToObject(reader As IDataReader) As ArticuloEntity
        Dim articulo As New ArticuloEntity()

        articulo.IdArticulo = Convert.ToInt32(reader("ID"))
        articulo.IdCategoria = Convert.ToInt32(reader("IdCategoria"))
        articulo.Nombre = Convert.ToString(reader("Nombre"))
        articulo.Descripcion = Convert.ToString(reader("Descripcion"))
        articulo.PrecioCompra = Convert.ToDouble(reader("PrecioCompra"))
        articulo.PrecioVenta = Convert.ToDouble(reader("PrecioVenta"))
        articulo.Stock = Convert.ToInt32(reader("Stock"))

        Return articulo
    End Function
End Class
