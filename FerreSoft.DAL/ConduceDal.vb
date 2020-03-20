Imports System.Data.SqlClient
Imports FerreSoft.Entities
Public Class ConduceDal
    Inherits BaseDal

    'Metodos CRUD
    Public Shared Function Create(conduce As ConduceEntity) As ConduceEntity
        'Creamos la conexion
        Using conex As New SqlConnection(m_CadenaConexion)
            conex.Open()

            'Creamos la sentencia SQL para agregar un registro
            Dim sql As String = "INSERT INTO Conduce (IdFactura, Estado) Values(@IdFactura, @Estado) SELECT SCOPE_IDENTITY()"
            Dim cmd As New SqlCommand(sql, conex)
            'Agregamos los parametros
            cmd.Parameters.AddWithValue("@IdFactura", conduce.IdFactura)
            cmd.Parameters.AddWithValue("@estado", conduce.Estado)
            conduce.IdConduce = Convert.ToInt32(cmd.ExecuteScalar())
        End Using

        Return conduce

    End Function
    Public Shared Function Update(conduce As ConduceEntity) As ConduceEntity
        Using conex As New SqlConnection(m_CadenaConexion)
            conex.Open()

            'Creamos la sentencia SQL para actualizar un registro
            Dim sql As String = "UPDATE Conduce Set IdFactura=@IdFactura,Estado=@estado " &
                                "WHERE IdConduce=@idConduce"
            'Creamos el comando para ejecutar la sentencia SQL con sus parametros
            Dim cmd As New SqlCommand(sql, conex)
            cmd.Parameters.AddWithValue("@IdFactura", conduce.IdFactura)
            cmd.Parameters.AddWithValue("@estado", conduce.Estado)
            cmd.Parameters.AddWithValue("@idConduce", conduce.IdConduce)
            'Ejecutamos el comando
            cmd.ExecuteNonQuery()
        End Using

        Return conduce
    End Function
    Public Shared Function Delete(id As Integer) As Boolean
        Dim SeElimino As Boolean

        Using conex As New SqlConnection(m_CadenaConexion)
            conex.Open()

            'Creamos la sentencia SQL
            Dim sql As String = "DELETE FROM Conduce WHERE IdConduce=@idConduce"
            'Creamos el comando
            Dim cmd As New SqlCommand(sql, conex)
            cmd.Parameters.AddWithValue("@idConduce", id)

            SeElimino = cmd.ExecuteNonQuery() > 0
        End Using

        Return SeElimino
    End Function
    Public Shared Function GetAll() As List(Of ConduceEntity)
        Dim list As New List(Of ConduceEntity)

        Using conex As New SqlConnection(m_CadenaConexion)
            conex.Open()

            'Creamos la sentencia SQL para obtener todos los articulos
            Dim sql As String = "SELECT * FROM Conduce ORDER BY IdConduce"
            Dim cmd As New SqlCommand(sql, conex)
            Dim reader As SqlDataReader = cmd.ExecuteReader()

            While reader.Read()
                list.Add(ConvertToObject(reader))
            End While
        End Using

        Return list
    End Function

    Public Shared Function GetById(id As Integer) As ConduceEntity
        Dim conduce As ConduceEntity = Nothing

        Using conex As New SqlConnection(m_CadenaConexion)
            conex.Open()

            Dim sql As String = "SELECT * FROM Conduce Where IdConduce=@idConduce"
            Dim cmd As New SqlCommand(sql, conex)
            cmd.Parameters.AddWithValue("@idConduce", id)
            Dim reader As SqlDataReader = cmd.ExecuteReader()
            If reader.Read() Then
                conduce = ConvertToObject(reader)
            End If
        End Using

        Return conduce

    End Function
    Public Shared Function Exist(id As Integer) As Boolean
        Dim numRegistros As Integer

        Using conex As New SqlConnection(m_CadenaConexion)
            conex.Open()

            Dim sql As String = "SELECT COUNT(*) FROM Conduce WHERE IdConduce=@idConduce"
            Dim cmd As New SqlCommand(sql, conex)
            cmd.Parameters.AddWithValue("@idConduce", id)
            numRegistros = Convert.ToInt32(cmd.ExecuteScalar())
        End Using

        Return numRegistros > 0

    End Function

    'Metodo para convertir los datos en objectos
    Private Shared Function ConvertToObject(reader As IDataReader) As ConduceEntity
        Dim Conduce As New ConduceEntity()

        Conduce.IdConduce = Convert.ToInt32(reader("IdConduce"))
        Conduce.IdFactura = Convert.ToString(reader("IdFactura"))
        Conduce.Estado = Convert.ToString(reader("Estado"))

        Return Conduce
    End Function
End Class

