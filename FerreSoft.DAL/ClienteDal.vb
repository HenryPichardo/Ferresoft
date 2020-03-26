Imports System.Data.SqlClient
Imports FerreSoft.Entities

Public Class ClienteDAL
    Inherits BaseDal

    Private Sub New()
    End Sub

    'Metodos CRUD (Create, Read, Update, Delete)
    Public Shared Function Create(cliente As ClienteEntity) As ClienteEntity
        'Creamos la Conexion:
        'La instrucción Using nos sirve para asegurarnos que cuando finalice la conexion
        '(cuando se ejecute la instrucción End Using) se ejecutará el método Dispose 
        'del objeto SqlConnection liberando los recursos utilizados.
        Using conex As New SqlConnection(m_CadenaConexion)
            conex.Open()
            'Creamos la sentencia SQL para insercion del registro
            Dim sql As String = "INSERT INTO Cliente (Nombre, Apellido, Cedula, Direccion, Telefono, Whatsapp, Email) " &
                                "VALUES (@nombre, @apellido, @cedula, @direccion, @telefono, @whatsapp, @email) " &
                                "SELECT SCOPE_IDENTITY()"

            'Creamos el comando que ejecutara la sentencia SQL con sus correspondientes parametros
            Dim cmd As New SqlCommand(sql, conex)
            cmd.Parameters.AddWithValue("@nombre", cliente.Nombre)
            cmd.Parameters.AddWithValue("@apellido", cliente.Apellido)
            cmd.Parameters.AddWithValue("@cedula", cliente.Cedula)
            cmd.Parameters.AddWithValue("@direccion", cliente.Direccion)
            cmd.Parameters.AddWithValue("@telefono", cliente.Telefono)
            cmd.Parameters.AddWithValue("@whatsapp", cliente.Whatsapp)
            cmd.Parameters.AddWithValue("@email", cliente.Email)

            cliente.IdCliente = Convert.ToInt32(cmd.ExecuteScalar()) 'Obtenemos el ID generado por SqlServer
        End Using

        Return cliente
    End Function
    Public Shared Function Update(cliente As ClienteEntity) As ClienteEntity
        Using conex As New SqlConnection(m_CadenaConexion)
            conex.Open()

            Dim sql As String = "UPDATE Cliente SET  " &
                                "Nombre = @nombre, " &
                                "Apellido = @apellido, " &
                                "Cedula = @cedula, " &
                                "Direccion = @direccion, " &
                                "Telefono = @telefono, " &
                                "Whatsapp = @whatsapp, " &
                                "Email = @email " &
                                "WHERE IdCliente = @idCliente"

            Dim cmd As New SqlCommand(sql, conex)
            cmd.Parameters.AddWithValue("@nombre", cliente.Nombre)
            cmd.Parameters.AddWithValue("@apellido", cliente.Apellido)
            cmd.Parameters.AddWithValue("@cedula", cliente.Cedula)
            cmd.Parameters.AddWithValue("@direccion", cliente.Direccion)
            cmd.Parameters.AddWithValue("@telefono", cliente.Telefono)
            cmd.Parameters.AddWithValue("@whatsapp", cliente.Whatsapp)
            cmd.Parameters.AddWithValue("@email", cliente.Email)
            cmd.Parameters.AddWithValue("@idCliente", cliente.IdCliente)

            cmd.ExecuteNonQuery()
        End Using

        Return cliente
    End Function
    Public Shared Function Delete(id As Integer) As Boolean
        Dim SeElimino As Boolean

        'Creamos la conexion 
        Using conex As New SqlConnection(m_CadenaConexion)
            conex.Open()
            'Creamos la sentencia SQL
            Dim sql As String = "DELETE FROM Cliente WHERE id=@idCliente"
            'Creamos el comando con sus parametros
            Dim cmd As New SqlCommand(sql, conex)
            cmd.Parameters.AddWithValue("@idCliente", id)

            'Ejecutamos la sentencia SQL y almacenamos el resultado de la operación
            SeElimino = cmd.ExecuteNonQuery() > 0
        End Using

        Return SeElimino

    End Function
    Public Shared Function GetAll() As List(Of ClienteEntity)
        Dim list As New List(Of ClienteEntity)()

        Using conex As New SqlConnection(m_CadenaConexion)
            conex.Open()

            Dim sql As String = "SELECT IdCliente, Nombre, Apellido, Cedula, Direccion, Telefono, Whatsapp, Email FROM Cliente ORDER BY Apellido"
            Dim cmd As New SqlCommand(sql, conex)
            Dim reader As SqlDataReader = cmd.ExecuteReader()

            'Leemos cada fila devuelta por la BD y la convertimos en un objeto cliente para
            'luego devolverla como una lista
            While reader.Read()
                list.Add(ConvertToObject(reader))
            End While
        End Using

        Return list
    End Function
    Public Shared Function GetByValor(valor As String) As List(Of ClienteEntity)
        Dim list As New List(Of ClienteEntity)()

        Using conex As New SqlConnection(m_CadenaConexion)
            conex.Open()

            Dim sql As String = "SELECT IdCliente, Nombre, Apellido, Cedula, Direccion, Telefono, Whatsapp, Email FROM Cliente
                                WHERE Cedula = @valor or Nombre Like '%' + @valor + '%' or Apellidos Like '%' + @valor + '%' 
                                ORDER BY Apellidos"
            Dim cmd As New SqlCommand(sql, conex)
            cmd.Parameters.AddWithValue("@valor", valor)

            Dim reader As SqlDataReader = cmd.ExecuteReader()

            While reader.Read()
                list.Add(ConvertToObject(reader))
            End While
        End Using

        Return list
    End Function
    Public Shared Function GetById(id As Integer) As ClienteEntity
        Dim cliente As ClienteEntity = Nothing

        Using conex As New SqlConnection(m_CadenaConexion)
            conex.Open()

            Dim sql As String = "SELECT IdCliente, Nombre, Apellidos, Cedula, Direccion, Telefono, Whatsapp, Email FROM Cliente " &
                                "WHERE Id = @IdCliente"

            Dim cmd As New SqlCommand(sql, conex)
            cmd.Parameters.AddWithValue("@IdCliente", id)

            Dim reader As SqlDataReader = cmd.ExecuteReader()
            'Leemos la unica fila posible que puede devolver esta sentencia
            'y la convertimos en un objeto cliente, el cual es devuelto.
            If reader.Read() Then
                cliente = ConvertToObject(reader)
            End If
        End Using

        Return cliente

    End Function
    Public Shared Function Exist(id As Integer) As Boolean
        'Esta funcion nos permite saber si un objeto existe antes de guardarlo.
        'Vea su uso en el metodo Save() de la capa BLL
        Dim NumRegistros As Integer = 0

        Using conex As New SqlConnection(m_CadenaConexion)
            conex.Open()

            Dim sql As String = "SELECT Count(*) " &
                                "FROM Cliente " &
                                "WHERE IdCliente = @IdCliente"

            Dim cmd As New SqlCommand(sql, conex)
            cmd.Parameters.AddWithValue("@IdCliente", id)

            NumRegistros = Convert.ToInt32(cmd.ExecuteScalar())
        End Using

        Return NumRegistros > 0

    End Function

    'Metodo para convertir los datos en objetos
    Private Shared Function ConvertToObject(reader As IDataReader) As ClienteEntity
        Dim cliente As New ClienteEntity()

        cliente.IdCliente = Convert.ToInt32(reader("IdCliente"))
        cliente.Nombre = Convert.ToString(reader("Nombre"))
        cliente.Cedula = Convert.ToString(reader("Cedula"))
        cliente.Apellido = Convert.ToString(reader("Apellidos"))
        cliente.Direccion = Convert.ToString(reader("Direccion"))
        cliente.Telefono = Convert.ToString(reader("Telefono"))
        cliente.Email = Convert.ToString(reader("Email"))

        Return cliente 'retornamos un objeto ClienteEntity
    End Function

End Class
