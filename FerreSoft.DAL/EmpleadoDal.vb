Imports System.Data.SqlClient
Imports FerreSoft.Entities

Public Class EmpleadoDal
    Inherits BaseDal

    Private Sub New()
    End Sub

    'Metodos CRUD (Create, Read, Update, Delete)
    Public Shared Function Create(empleado As EmpleadoEntity) As EmpleadoEntity
        'Creamos la Conexion:
        'La instrucción Using nos sirve para asegurarnos que cuando finalice la conexion
        '(cuando se ejecute la instrucción End Using) se ejecutará el método Dispose 
        'del objeto SqlConnection liberando los recursos utilizados.
        Using conex As New SqlConnection(m_CadenaConexion)
            conex.Open()
            'Creamos la sentencia SQL para insercion del registro
            Dim sql As String = "INSERT INTO Empleado (Nombre, Apellido, Cedula, Direccion, Telefono, Sexo, Departamento,Nacionalidad,FechaNac,FechaIngreso) " &
                                "VALUES (@nombre, @apellidos, @cedula, @direccion, @telefono, @sexo, @departamento, @nacionalidad, @fechaNac, @fechaIngreso) " &
                                "SELECT SCOPE_IDENTITY()"

            'Creamos el comando que ejecutara la sentencia SQL con sus correspondientes parametros
            Dim cmd As New SqlCommand(sql, conex)
            cmd.Parameters.AddWithValue("@nombre", empleado.Nombre)
            cmd.Parameters.AddWithValue("@apellidos", empleado.Apellido)
            cmd.Parameters.AddWithValue("@cedula", empleado.Cedula)
            cmd.Parameters.AddWithValue("@direccion", empleado.Direccion)
            cmd.Parameters.AddWithValue("@telefono", empleado.Telefono)
            cmd.Parameters.AddWithValue("@sexo", empleado.Sexo)
            cmd.Parameters.AddWithValue("@departamento", empleado.Departamento)
            cmd.Parameters.AddWithValue("@nacionalidad", empleado.Nacionalidad)
            cmd.Parameters.AddWithValue("@fechaNac", empleado.FechaNac)
            cmd.Parameters.AddWithValue("@fechaIngreso", empleado.FechaIngreso)

            empleado.IdEmpleado = Convert.ToInt32(cmd.ExecuteScalar()) 'Obtenemos el ID generado por SqlServer
        End Using

        Return empleado
    End Function
    Public Shared Function Update(empleado As EmpleadoEntity) As EmpleadoEntity
        Using conex As New SqlConnection(m_CadenaConexion)
            conex.Open()

            Dim sql As String = "UPDATE Empleado SET  " &
                                "Nombre = @nombre, " &
                                "Apellido = @apellido, " &
                                "Cedula = @cedula, " &
                                "Direccion = @direccion, " &
                                "Telefono = @telefono, " &
                                "Sexo = @email " &
                                "Departamento = @departamento, " &
                                "Nacionalidad = @nacionalidad, " &
                                "FechaNac = @fechaNac, " &
                                "FechaIngreso = @fechaIngreso " &
                                "WHERE IdEmpleado = @IdEmpleado"

            Dim cmd As New SqlCommand(sql, conex)
            cmd.Parameters.AddWithValue("@nombre", empleado.Nombre)
            cmd.Parameters.AddWithValue("@apellidos", empleado.Apellido)
            cmd.Parameters.AddWithValue("@cedula", empleado.Cedula)
            cmd.Parameters.AddWithValue("@direccion", empleado.Direccion)
            cmd.Parameters.AddWithValue("@telefono", empleado.Telefono)
            cmd.Parameters.AddWithValue("@sexo", empleado.Sexo)
            cmd.Parameters.AddWithValue("@departamento", empleado.Departamento)
            cmd.Parameters.AddWithValue("@nacionalidad", empleado.Nacionalidad)
            cmd.Parameters.AddWithValue("@fechaNac", empleado.FechaNac)
            cmd.Parameters.AddWithValue("@fechaIngreso", empleado.FechaIngreso)
            cmd.Parameters.AddWithValue("@IdEmpleado", empleado.IdEmpleado)

            cmd.ExecuteNonQuery()
        End Using

        Return empleado
    End Function
    Public Shared Function Delete(id As Integer) As Boolean
        Dim SeElimino As Boolean

        'Creamos la conexion 
        Using conex As New SqlConnection(m_CadenaConexion)
            conex.Open()
            'Creamos la sentencia SQL
            Dim sql As String = "DELETE FROM Empleado WHERE id=@IdEmpleado"
            'Creamos el comando con sus parametros
            Dim cmd As New SqlCommand(sql, conex)
            cmd.Parameters.AddWithValue("@IdEmpleado", id)

            'Ejecutamos la sentencia SQL y almacenamos el resultado de la operación
            SeElimino = cmd.ExecuteNonQuery() > 0
        End Using

        Return SeElimino

    End Function
    Public Shared Function GetAll() As List(Of EmpleadoEntity)
        Dim list As New List(Of EmpleadoEntity)()

        Using conex As New SqlConnection(m_CadenaConexion)
            conex.Open()

            Dim sql As String = "SELECT IdEmpleado, Nombre, Apellido, Cedula, Direccion, Telefono, Sexo, Departamento, Nacionalidad, FechaNac, FechaIngreso FROM Empleado ORDER BY Apellido"
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

    Public Shared Function GetById(id As Integer) As EmpleadoEntity
        Dim empleado As EmpleadoEntity = Nothing

        Using conex As New SqlConnection(m_CadenaConexion)
            conex.Open()

            Dim sql As String = "SELECT IdEmpleado, Nombre, Apellido, Cedula, Direccion, Telefono, Sexo, Departamento, Nacionalidad, FechaNac, FechaIngreso FROM Empleado " &
                                "WHERE Id = @IdEmpleado"

            Dim cmd As New SqlCommand(sql, conex)
            cmd.Parameters.AddWithValue("@IdEmpleado", id)

            Dim reader As SqlDataReader = cmd.ExecuteReader()
            'Leemos la unica fila posible que puede devolver esta sentencia
            'y la convertimos en un objeto cliente, el cual es devuelto.
            If reader.Read() Then
                empleado = ConvertToObject(reader)
            End If
        End Using

        Return empleado

    End Function
    Public Shared Function Exist(id As Integer) As Boolean
        'Esta funcion nos permite saber si un objeto existe antes de guardarlo.
        'Vea su uso en el metodo Save() de la capa BLL
        Dim NumRegistros As Integer = 0

        Using conex As New SqlConnection(m_CadenaConexion)
            conex.Open()

            Dim sql As String = "SELECT Count(*) " &
                                "FROM Empleado " &
                                "WHERE IdEmpleado = @IdEmpleado"

            Dim cmd As New SqlCommand(sql, conex)
            cmd.Parameters.AddWithValue("@IdEmpleado", id)

            NumRegistros = Convert.ToInt32(cmd.ExecuteScalar())
        End Using

        Return NumRegistros > 0

    End Function

    'Metodo para convertir los datos en objetos
    Private Shared Function ConvertToObject(reader As IDataReader) As EmpleadoEntity
        Dim empleado As New EmpleadoEntity()

        empleado.IdEmpleado = Convert.ToInt32(reader("IdEmpleado"))
        empleado.Nombre = Convert.ToString(reader("Nombre"))
        empleado.Apellido = Convert.ToString(reader("Apellidos"))
        empleado.Cedula = Convert.ToString(reader("Cedula"))
        empleado.Direccion = Convert.ToString(reader("Direccion"))
        empleado.Telefono = Convert.ToString(reader("Telefono"))
        empleado.Sexo = Convert.ToString(reader("Sexo"))
        empleado.Departamento = Convert.ToString(reader("Departamento"))
        empleado.Nacionalidad = Convert.ToString(reader("Nacionalidad"))
        empleado.FechaNac = Convert.ToString(reader("FechaNac"))
        empleado.FechaIngreso = Convert.ToString(reader("FechaIngreso"))

        Return empleado 'retornamos un objeto ClienteEntity
    End Function

End Class

