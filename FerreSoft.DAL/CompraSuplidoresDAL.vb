Imports System.Data.SqlClient
Imports System.Transactions
Imports FerreSoft.Entities
Public Class CompraSuplidoresDAL
    Inherits BaseDal
    Sub New()

    End Sub

    Public Shared Function Create(compraSuplidores As CompraSuplidoresEntity) As CompraSuplidoresEntity

        'Inicializamos las transacciones
        Using scope As New TransactionScope()

            Using conex As New SqlConnection(m_CadenaConexion)
                conex.Open()

                '1. Creamos el maestro de la CompraSuplidores
                Dim sqlCompraSuplidores As String = "INSERT INTO CompraSuplidores (IdSuplidor, IdUsuarioSistema, Fecha, Vencimiento," &
                "Estado)" &
                "VALUES(@idSuplidor, @IdUsuarioSistema, @fecha, @Vencimiento, @Estado)" &
                "SELECT SCOPE_IDENTITY()"

                Using cmd As New SqlCommand(sqlCompraSuplidores, conex)
                    cmd.Parameters.AddWithValue("@idSuplidor", compraSuplidores.IdSuplidor)
                    cmd.Parameters.AddWithValue("@IdUsuarioSistema", compraSuplidores.IdUsuarioSistema)
                    cmd.Parameters.AddWithValue("@fecha", compraSuplidores.Fecha)
                    cmd.Parameters.AddWithValue("@vencimiento", compraSuplidores.vencimiento)
                    cmd.Parameters.AddWithValue("@estado", compraSuplidores.Estado)
                    compraSuplidores.IdCompraSuplidores = Convert.ToInt32(cmd.ExecuteScalar())
                End Using

                '2. Creamos los detalles de pedido
                Dim sqlDetalleCompraSuplidores As String = "INSERT INTO DetalleCompraSuplidores (IdCompraSuplidores, IdArticulo, Cantidad, " &
                                            "Precio, SubTotal, Descuento, Impuesto, Total)" &
                                            "VALUES(@idPedido, @idArticulo, @cantidad, @precio, @subTotal," &
                                            "@descuento, @impuesto, @total)" &
                                            "SELECT SCOPE_IDENTITY()"

                Using cmd As New SqlCommand(sqlDetalleCompraSuplidores, conex)
                    For Each detalle In compraSuplidores.Detalles
                        ' Como se reutiliza el mismo objeto SqlCommand es necesario limpiar los parametros
                        ' de la operacion previa, sino estos se iran agregando en la coleccion, generando un fallo
                        cmd.Parameters.Clear()

                        cmd.Parameters.AddWithValue("@idCompraSuplidores", compraSuplidores.IdCompraSuplidores)
                        cmd.Parameters.AddWithValue("@idArticulo", detalle.IdArticulo)
                        cmd.Parameters.AddWithValue("@cantidad", detalle.Cantidad)

                        detalle.IdDetalleCompraSuplidores = Convert.ToInt32(cmd.ExecuteScalar())
                    Next
                End Using
            End Using

            'Indicamos que se inicien todas las transacciones
            scope.Complete()

        End Using

        Return compraSuplidores
    End Function
    Public Shared Function Update(compraSuplidores As CompraSuplidoresEntity) As CompraSuplidoresEntity

        'Inicializamos las transacciones
        Using scope As New TransactionScope()

            Using conex As New SqlConnection(m_CadenaConexion)
                conex.Open()

                '1. Creamos el maestro del pedido
                Dim sql As String = "UPDATE CompraSuplidores SET 
                                        IdSuplidor=@idSuplidor, 
                                        IdUsuarioSistema=@idUsuarioSistema, 
                                        Fecha=@fecha, 
                                        Vencimiento=@vencimiento,
                                        Estado=@estado, 
                                        
                                        WHERE IdCompraSuplidores=@idCompraSuplidores"

                Using cmd As New SqlCommand(sql, conex)
                    cmd.Parameters.AddWithValue("@idSuplidor", compraSuplidores.IdSuplidor)
                    cmd.Parameters.AddWithValue("@idUsuarioSistema", compraSuplidores.IdUsuarioSistema)
                    cmd.Parameters.AddWithValue("@fecha", compraSuplidores.Fecha)
                    cmd.Parameters.AddWithValue("@vencimiento", compraSuplidores.vencimiento)
                    cmd.Parameters.AddWithValue("@estado", compraSuplidores.Estado)

                    cmd.Parameters.AddWithValue("@idCompraSuplidores", compraSuplidores.IdCompraSuplidores)
                    cmd.ExecuteNonQuery()
                End Using

                '2. Creamos o actualizamos los detalles de pedido
                For Each detalle In compraSuplidores.Detalles

                    'Si es un nuevo detalle creamos la sentencia SQL para insertarlo,
                    'sino creamos la sentencia SQL para actualizarlo. Esto lo podemos 
                    'saber por el ID.
                    If detalle.IdDetalleCompraSuplidores = 0 Then
                        'Si el ID es cero es porque es un nuevo detalle
                        sql = "INSERT INTO DetalleCompraSuplidores (IdCompraSuplidores, IdArticulo, Cantidad) " &
                                        "VALUES(@idCompraSuplidores, @idArticulo, @cantidad)" &
                                        "SELECT SCOPE_IDENTITY()"
                    Else
                        'Si no es cero es un detalle que existe en la BD, lo actualizamos
                        sql = "UPDATE DetalleCompraSuplidores SET
                                            IdCompraSuplidores=@idComprasuplidores,
                                            IdArticulo=@idArticulo, 
                                            Cantidad=@cantidad, 
                                            
                                            WHERE IdDetalleCompraSuplidores=@idDetalle"

                    End If

                    Using cmd As New SqlCommand(sql, conex)
                        ' como se reutiliza el mismo objeto SqlCommand es necesario limpiar los parametros
                        ' de la operacion previa, sino estos se iran agregando en la coleccion, generando un fallo
                        cmd.Parameters.Clear()

                        cmd.Parameters.AddWithValue("@idCompraSuplidores", compraSuplidores.IdCompraSuplidores)
                        cmd.Parameters.AddWithValue("@idArticulo", detalle.IdArticulo)
                        cmd.Parameters.AddWithValue("@cantidad", detalle.Cantidad)
                        cmd.Parameters.AddWithValue("@idDetalle", detalle.IdDetalleCompraSuplidores)

                        If detalle.IdDetalleCompraSuplidores = 0 Then
                            'Es un nuevo detalle, por tanto obtenemos el ID
                            detalle.IdDetalleCompraSuplidores = Convert.ToInt32(cmd.ExecuteScalar())
                        Else
                            cmd.ExecuteNonQuery() 'Es una actualizacion
                        End If
                    End Using
                Next

            End Using

            'Indicamos que se inicien todas las transacciones
            scope.Complete()

        End Using

        Return compraSuplidores
    End Function
    Public Shared Function GetAll() As List(Of CompraSuplidoresEntity)
        Dim list As New List(Of CompraSuplidoresEntity)()

        Using conex As New SqlConnection(m_CadenaConexion)
            conex.Open()

            Dim sql As String = "SELECT * FROM CompraSuplidore ORDER BY Fecha Desc"
            Dim cmd As New SqlCommand(sql, conex)
            Dim reader As SqlDataReader = cmd.ExecuteReader()

            'Leemos cada fila devuelta por la BD y la convertimos en un objeto para
            'luego devolverla como una lista
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

            Dim sql As String = "SELECT * FROM CompraSuplidores " &
                                "WHERE Id = @idCompraSuplidores"

            Dim cmd As New SqlCommand(sql, conex)
            cmd.Parameters.AddWithValue("@idPedido", id)

            Dim reader As SqlDataReader = cmd.ExecuteReader()
            '1. Leemos la unica fila posible que puede devolver esta sentencia
            '   y la convertimos en un objeto
            If reader.Read() Then
                compraSuplidores = ConvertToObject(reader)

                '2. Recuperamos todos los detalles del pedido desde la BD, por esto
                '   establecemos la nueva sentencia SQL
                sql = "Select * From DetalleCompraSuplidores Where IdCompraSuplidores=@idCompraSuplidores"
                cmd.CommandText = sql
                reader.Close()
                reader = cmd.ExecuteReader()
                While reader.Read
                    'Convertimos cada detalle en un objeto y lo agregamos la lista de detalles(saco de coco). 
                    'Con esto el objeto pedido contendra todos sus 
                    'detalles en la propiedad Detalles.
                    Dim det As DetalleCompraSuplidoresEntity = ConvertToObjectDetalle(reader)
                    compraSuplidores.Detalles.Add(det)
                End While
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
                                "FROM CompraSuplidores " &
                                "WHERE ID = @idCompraSuplidores"

            Dim cmd As New SqlCommand(sql, conex)
            cmd.Parameters.AddWithValue("@idCompraSuplidores", id)

            NumRegistros = Convert.ToInt32(cmd.ExecuteScalar())
        End Using

        Return NumRegistros > 0

    End Function

    'Metodos para convertir la fila del DataReader en un objeto
    Private Shared Function ConvertToObject(reader As IDataReader) As CompraSuplidoresEntity
        Dim compraSuplidores As New CompraSuplidoresEntity()

        compraSuplidores.IdCompraSuplidores = Convert.ToInt32(reader("IdCompraSuplidores"))
        compraSuplidores.IdSuplidor = Convert.ToInt32(reader("IdSuplidor"))
        compraSuplidores.Fecha = Convert.ToDateTime(reader("Fecha"))

        Return compraSuplidores 'retornamos un objeto pedido
    End Function
    Private Shared Function ConvertToObjectDetalle(reader As IDataReader) As DetalleCompraSuplidoresEntity
        Dim detalle As New DetalleCompraSuplidoresEntity()

        detalle.IdDetalleCompraSuplidores = Convert.ToInt32(reader("IdDetalleCompraSuplidor"))
        detalle.IdCompraSuplidores = Convert.ToInt32(reader("IdCompraSuplidores"))
        detalle.IdArticulo = Convert.ToInt32(reader("IdArticulo"))
        detalle.Cantidad = Convert.ToDecimal(reader("Cantidad"))

        Return detalle 'retornamos el objeto
    End Function
End Class
