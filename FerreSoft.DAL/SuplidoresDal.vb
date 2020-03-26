Imports System.Data.SqlClient
Imports FerreSoft.Entities
Public Class SuplidoresDal
    Inherits BaseDal

    Private Sub New()
    End Sub

    'Metodos CRUD (Create, Read, Update, Delete)
    Public Shared Function Create(suplidor As SuplidoresEntity) As SuplidoresEntity
        'Creamos la Conexion:
        'La instrucción Using nos sirve para asegurarnos que cuando finalice la conexion
        '(cuando se ejecute la instrucción End Using) se ejecutará el método Dispose 
        'del objeto SqlConnection liberando los recursos utilizados.
        Using conex As New SqlConnection(m_CadenaConexion)
            conex.Open()
            'Creamos la sentencia SQL para insercion del registro
            Dim sql As String = "INSERT INTO suplidores (Nombre, RNC, Direccion, Telefono) " &
                                "VALUES (@nombre, @rnc, @direccion, @telefono) " &
                                "SELECT SCOPE_IDENTITY()"

            'Creamos el comando que ejecutara la sentencia SQL con sus correspondientes parametros
            Dim cmd As New SqlCommand(sql, conex)
            cmd.Parameters.AddWithValue("@nombre", suplidor.Nombre)
            cmd.Parameters.AddWithValue("@rnc", suplidor.RNC)
            cmd.Parameters.AddWithValue("@direccion", suplidor.Direccion)
            cmd.Parameters.AddWithValue("@telefono", suplidor.Telefono)


            suplidor.IdSuplidor = Convert.ToInt32(cmd.ExecuteScalar()) 'Obtenemos el ID generado por SqlServer
        End Using

        Return suplidor
    End Function
    Public Shared Function Update(suplidor As SuplidoresEntity) As SuplidoresEntity
        Using conex As New SqlConnection(m_CadenaConexion)
            conex.Open()

            Dim sql As String = "UPDATE Suplidores SET  " &
                                "Nombre = @nombre, " &
                                "RNC = @rnc, " &
                                "Direccion = @direccion, " &
                                "Telefono = @telefono, " &
                                "WHERE IdSuplidor = @IdSuplidor"

            Dim cmd As New SqlCommand(sql, conex)
            cmd.Parameters.AddWithValue("@nombre", suplidor.Nombre)
            cmd.Parameters.AddWithValue("@rnc", suplidor.RNC)
            cmd.Parameters.AddWithValue("@direccion", suplidor.Direccion)
            cmd.Parameters.AddWithValue("@telefono", suplidor.Telefono)
            cmd.Parameters.AddWithValue("@IdSuplidor", suplidor.IdSuplidor)

            cmd.ExecuteNonQuery()
        End Using

        Return suplidor
    End Function
    Public Shared Function Delete(id As Integer) As Boolean
        Dim SeElimino As Boolean

        'Creamos la conexion 
        Using conex As New SqlConnection(m_CadenaConexion)
            conex.Open()
            'Creamos la sentencia SQL
            Dim sql As String = "DELETE FROM Suplidores WHERE id=@IdSuplidor"
            'Creamos el comando con sus parametros
            Dim cmd As New SqlCommand(sql, conex)
            cmd.Parameters.AddWithValue("@IdSuplidor", id)

            'Ejecutamos la sentencia SQL y almacenamos el resultado de la operación
            SeElimino = cmd.ExecuteNonQuery() > 0
        End Using

        Return SeElimino

    End Function
    Public Shared Function GetAll() As List(Of SuplidoresEntity)
        Dim list As New List(Of SuplidoresEntity)()

        Using conex As New SqlConnection(m_CadenaConexion)
            conex.Open()

            Dim sql As String = "SELECT IdSuplidor, Nombre, RNC, Direccion, Telefono, FROM Suplidores ORDER BY RNC"
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
    Public Shared Function GetByValor(valor As String) As List(Of SuplidoresEntity)
        Dim list As New List(Of SuplidoresEntity)()

        Using conex As New SqlConnection(m_CadenaConexion)
            conex.Open()

            Dim sql As String = "SELECT IdSuplidor, Nombre, RNC, Direccion, Telefono,  FROM Suplidores
                                WHERE RNC = @valor or Nombre Like '%' + @valor + '%' or telefono Like '%' + @valor + '%' 
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
    Public Shared Function GetById(id As Integer) As SuplidoresEntity
        Dim suplidor As SuplidoresEntity = Nothing

        Using conex As New SqlConnection(m_CadenaConexion)
            conex.Open()

            Dim sql As String = "SELECT IdSuplidor, Nombre, RNC, Direccion, Telefono,  FROM Suplidores " &
                                "WHERE Id = @IdSuplidor"

            Dim cmd As New SqlCommand(sql, conex)
            cmd.Parameters.AddWithValue("@IdSuplidor", id)

            Dim reader As SqlDataReader = cmd.ExecuteReader()
            'Leemos la unica fila posible que puede devolver esta sentencia
            'y la convertimos en un objeto cliente, el cual es devuelto.
            If reader.Read() Then
                suplidor = ConvertToObject(reader)
            End If
        End Using

        Return suplidor

    End Function
    Public Shared Function Exist(id As Integer) As Boolean
        'Esta funcion nos permite saber si un objeto existe antes de guardarlo.
        'Vea su uso en el metodo Save() de la capa BLL
        Dim NumRegistros As Integer = 0

        Using conex As New SqlConnection(m_CadenaConexion)
            conex.Open()

            Dim sql As String = "SELECT Count(*) " &
                                "FROM Suplidores " &
                                "WHERE IdSuplidor = @IdSuplidor"

            Dim cmd As New SqlCommand(sql, conex)
            cmd.Parameters.AddWithValue("@IdSuplidor", id)

            NumRegistros = Convert.ToInt32(cmd.ExecuteScalar())
        End Using

        Return NumRegistros > 0

    End Function

    'Metodo para convertir los datos en objetos
    Private Shared Function ConvertToObject(reader As IDataReader) As SuplidoresEntity
        Dim suplidor As New SuplidoresEntity()

        suplidor.IdSuplidor = Convert.ToInt32(reader("ID"))
        suplidor.Nombre = Convert.ToString(reader("Nombre"))
        suplidor.RNC = Convert.ToString(reader("RNC"))
        suplidor.Direccion = Convert.ToString(reader("Direccion"))
        suplidor.Telefono = Convert.ToString(reader("Telefono"))


        Return cliente 'retornamos un objeto ClienteEntity
    End Function
End Class
