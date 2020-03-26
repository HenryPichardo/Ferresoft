Imports System.Data.SqlClient
Imports FerreSoft.Entities
Public Class CompraSuplidoresDAL
    Inherits BaseDal

    Private Sub New()
    End Sub

    'Metodos CRUD (Create, Read, Update, Delete)
    Public Shared Function Create(compraSuplidores As CompraSuplidoresEntity) As CompraSuplidoresEntity
        'Creamos la Conexion:
        'La instrucción Using nos sirve para asegurarnos que cuando finalice la conexion
        '(cuando se ejecute la instrucción End Using) se ejecutará el método Dispose 
        'del objeto SqlConnection liberando los recursos utilizados.
        Using conex As New SqlConnection(m_CadenaConexion)
            conex.Open()
            'Creamos la sentencia SQL para insercion del registro
            Dim sql As String = "INSERT INTO CompraSuplidores (IdSuplidor, IdUsuarioSistema, Fecha, Vencimiento, Estado) " &
                                "VALUES (@idSuplidor, @idUsuarioSistema, @fecha, @vencimiento, @estado) " &
                                "SELECT SCOPE_IDENTITY()"

            'Creamos el comando que ejecutara la sentencia SQL con sus correspondientes parametros
            Dim cmd As New SqlCommand(sql, conex)
            cmd.Parameters.AddWithValue("@nombre", compraSuplidores.IdSuplidor)
            cmd.Parameters.AddWithValue("@apellidos", compraSuplidores.IdUsuarioSistema)
            cmd.Parameters.AddWithValue("@cedula", compraSuplidores.Fecha)
            cmd.Parameters.AddWithValue("@direccion", compraSuplidores.vencimiento)
            cmd.Parameters.AddWithValue("@telefono", compraSuplidores.Estado)


            compraSuplidores.IdCompraSuplidores = Convert.ToInt32(cmd.ExecuteScalar()) 'Obtenemos el ID generado por SqlServer
        End Using

        Return compraSuplidores
    End Function
    Public Shared Function Update(compraSuplidores As CompraSuplidoresEntity) As CompraSuplidoresEntity
        Using conex As New SqlConnection(m_CadenaConexion)
            conex.Open()

            Dim sql As String = "UPDATE CompraSuplidores SET  " &
                                "Nombre = @idSuplidor, " &
                                "Apellidos = @idUsuarioSistema, " &
                                "Cedula = @fecha, " &
                                "Direccion = @vencimiento, " &
                                "Telefono = @estado " &
                                "WHERE IdCompraSuplidores = @idCompraSuplidores"

            Dim cmd As New SqlCommand(sql, conex)
            cmd.Parameters.AddWithValue("@nombre", compraSuplidores.IdSuplidor)
            cmd.Parameters.AddWithValue("@apellidos", compraSuplidores.IdUsuarioSistema)
            cmd.Parameters.AddWithValue("@cedula", compraSuplidores.Fecha)
            cmd.Parameters.AddWithValue("@direccion", compraSuplidores.vencimiento)
            cmd.Parameters.AddWithValue("@telefono", compraSuplidores.Estado)

            cmd.Parameters.AddWithValue("@idCompraSuplidores", compraSuplidores.IdCompraSuplidores)

            cmd.ExecuteNonQuery()
        End Using

        Return compraSuplidores
    End Function
    Public Shared Function Delete(id As Integer) As Boolean
        Dim SeElimino As Boolean

        'Creamos la conexion 
        Using conex As New SqlConnection(m_CadenaConexion)
            conex.Open()
            'Creamos la sentencia SQL
            Dim sql As String = "DELETE FROM Cliente WHERE id=@IdCliente"
            'Creamos el comando con sus parametros
            Dim cmd As New SqlCommand(sql, conex)
            cmd.Parameters.AddWithValue("@IdCliente", id)

            'Ejecutamos la sentencia SQL y almacenamos el resultado de la operación
            SeElimino = cmd.ExecuteNonQuery() > 0
        End Using

        Return SeElimino

    End Function
    Public Shared Function GetAll() As List(Of CompraSuplidoresEntity)
        Dim list As New List(Of CompraSuplidoresEntity)()

        Using conex As New SqlConnection(m_CadenaConexion)
            conex.Open()

            Dim sql As String = "SELECT ID, Nombre, Apellidos, Cedula, Direccion, Telefono, Email FROM Cliente ORDER BY Apellidos"
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
    Public Shared Function GetByValor(valor As String) As List(Of CompraSuplidoresEntity)
        Dim list As New List(Of CompraSuplidoresEntity)()

        Using conex As New SqlConnection(m_CadenaConexion)
            conex.Open()

            Dim sql As String = "SELECT ID, Nombre, Apellidos, Cedula, Direccion, Telefono, Email FROM Cliente
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
    Public Shared Function GetById(id As Integer) As CompraSuplidoresEntity
        Dim compraSuplidores As CompraSuplidoresEntity = Nothing

        Using conex As New SqlConnection(m_CadenaConexion)
            conex.Open()

            Dim sql As String = "SELECT ID, Nombre, Apellidos, Cedula, Direccion, Telefono, Email FROM Cliente " &
                                "WHERE Id = @IdCliente"

            Dim cmd As New SqlCommand(sql, conex)
            cmd.Parameters.AddWithValue("@IdCliente", id)

            Dim reader As SqlDataReader = cmd.ExecuteReader()
            'Leemos la unica fila posible que puede devolver esta sentencia
            'y la convertimos en un objeto cliente, el cual es devuelto.
            If reader.Read() Then
                compraSuplidores = ConvertToObject(reader)
            End If
        End Using

        Return compraSuplidores

    End Function
    Public Shared Function Exist(id As Integer) As Boolean
        'Esta funcion nos permite saber si un objeto existe antes de guardarlo.
        'Vea su uso en el metodo Save() de la capa BLL
        Dim NumRegistros As Integer = 0

        Using conex As New SqlConnection(m_CadenaConexion)
            conex.Open()

            Dim sql As String = "SELECT Count(*) " &
                                "FROM Cliente " &
                                "WHERE ID = @IdCliente"

            Dim cmd As New SqlCommand(sql, conex)
            cmd.Parameters.AddWithValue("@IdCliente", id)

            NumRegistros = Convert.ToInt32(cmd.ExecuteScalar())
        End Using

        Return NumRegistros > 0

    End Function

    'Metodo para convertir los datos en objetos
    Private Shared Function ConvertToObject(reader As IDataReader) As CompraSuplidoresEntity
        Dim compraSuplidores As New CompraSuplidoresEntity()

        compraSuplidores.IdCompraSuplidores = Convert.ToInt32(reader("IdCompraSuplidores"))
        compraSuplidores.IdSuplidor = Convert.ToString(reader("IdSuplidor"))
        compraSuplidores.IdUsuarioSistema = Convert.ToString(reader("IdUsuarioSistema"))
        compraSuplidores.Fecha = Convert.ToString(reader("Fecha"))
        compraSuplidores.vencimiento = Convert.ToString(reader("Vencimiento"))
        compraSuplidores.Estado = Convert.ToString(reader("Estado"))

        Return compraSuplidores 'retornamos un objeto ClienteEntity
    End Function

End Class