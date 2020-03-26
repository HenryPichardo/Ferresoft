Imports System.Data.SqlClient
Imports System.Transactions
Imports FerreSoft.Entities
Public Class FacturaDal
    Inherits BaseDal

    Sub New()

    End Sub

    Public Shared Function Create(factura As FacturaEntity) As FacturaEntity

        'Inicializamos las transacciones
        Using scope As New TransactionScope()

            Using conex As New SqlConnection(m_CadenaConexion)
                conex.Open()

                '1. Creamos el maestro del pedido
                Dim sqlPedido As String = "INSERT INTO Factura (IdCliente, IdUsuarioSistema, Fecha, SubTotal, TotalDescuento," &
                "TotalImpuesto, Total)" &
                "VALUES(@idCliente, IdUsuarioSistema, @fecha, @subTotal, @totalDescuento, @totalImpuesto, @total)" &
                "SELECT SCOPE_IDENTITY()"

                Using cmd As New SqlCommand(sqlPedido, conex)
                    cmd.Parameters.AddWithValue("@idCliente", factura.IdCliente)
                    cmd.Parameters.AddWithValue("@idUsuarioSistema", factura.IdUsuarioSistema)
                    cmd.Parameters.AddWithValue("@fecha", factura.Fecha)
                    cmd.Parameters.AddWithValue("@subTotal", factura.SubTotal)
                    cmd.Parameters.AddWithValue("@totalDescuento", factura.TotalDescuento)
                    cmd.Parameters.AddWithValue("@totalImpuesto", factura.TotalImpuesto)
                    cmd.Parameters.AddWithValue("@total", factura.Total)
                    factura.IdFactura = Convert.ToInt32(cmd.ExecuteScalar())
                End Using

                '2. Creamos los detalles de pedido
                Dim sqlDetallePedido As String = "INSERT INTO DetalleFactura (IdFactura, IdArticulo, Cantidad, " &
                                            "Precio, SubTotal, Descuento, Impuesto, Total)" &
                                            "VALUES(@idFactura, @idArticulo, @cantidad, @precio, @subTotal," &
                                            "@descuento, @impuesto, @total)" &
                                            "SELECT SCOPE_IDENTITY()"

                Using cmd As New SqlCommand(sqlDetallePedido, conex)
                    For Each detalle In factura.Detalles
                        ' Como se reutiliza el mismo objeto SqlCommand es necesario limpiar los parametros
                        ' de la operacion previa, sino estos se iran agregando en la coleccion, generando un fallo
                        cmd.Parameters.Clear()

                        cmd.Parameters.AddWithValue("@idFactura", detalle.IdFactura)
                        cmd.Parameters.AddWithValue("@idArticulo", detalle.IdArticulo)
                        cmd.Parameters.AddWithValue("@cantidad", detalle.Cantidad)
                        cmd.Parameters.AddWithValue("@precio", detalle.Precio)
                        cmd.Parameters.AddWithValue("@subTotal", detalle.SubTotal)
                        cmd.Parameters.AddWithValue("@descuento", detalle.Descuento)
                        cmd.Parameters.AddWithValue("@impuesto", detalle.Impuesto)
                        cmd.Parameters.AddWithValue("@total", detalle.Total)
                        detalle.IdDetalleFactura = Convert.ToInt32(cmd.ExecuteScalar())
                    Next
                End Using
            End Using

            'Indicamos que se inicien todas las transacciones
            scope.Complete()

        End Using

        Return factura
    End Function
    Public Shared Function Update(factura As FacturaEntity) As FacturaEntity

        'Inicializamos las transacciones
        Using scope As New TransactionScope()

            Using conex As New SqlConnection(m_CadenaConexion)
                conex.Open()

                '1. Creamos el maestro del pedido
                Dim sql As String = "UPDATE Factura SET 
                                        IdCliente=@idCliente, 
                                        Fecha=@fecha, 
                                        SubTotal=@subTotal, 
                                        TotalDescuento=@totalDescuento,
                                        TotalImpuesto=@totalImpuesto, 
                                        Total=@total
                                        WHERE IdFactura=@idFactura"

                Using cmd As New SqlCommand(sql, conex)
                    cmd.Parameters.AddWithValue("@idCliente", factura.IdCliente)
                    cmd.Parameters.AddWithValue("@idUsuarioSistema", factura.IdUsuarioSistema)
                    cmd.Parameters.AddWithValue("@fecha", factura.Fecha)
                    cmd.Parameters.AddWithValue("@subTotal", factura.SubTotal)
                    cmd.Parameters.AddWithValue("@totalDescuento", factura.TotalDescuento)
                    cmd.Parameters.AddWithValue("@totalImpuesto", factura.TotalImpuesto)
                    cmd.Parameters.AddWithValue("@total", factura.Total)
                    cmd.Parameters.AddWithValue("@idFactura", factura.IdFactura)
                    cmd.ExecuteNonQuery()
                End Using

                '2. Creamos o actualizamos los detalles de pedido
                For Each detalle In factura.Detalles

                    'Si es un nuevo detalle creamos la sentencia SQL para insertarlo,
                    'sino creamos la sentencia SQL para actualizarlo. Esto lo podemos 
                    'saber por el ID.
                    If detalle.IdDetalleFactura = 0 Then
                        'Si el ID es cero es porque es un nuevo detalle
                        sql = "INSERT INTO DetalleFactura (IdFactura, IdArticulo, Cantidad, " &
                                        "Precio, SubTotal, Descuento, Impuesto, Total)" &
                                        "VALUES(@idFactura, @idArticulo, @cantidad, @precio, @subTotal," &
                                        "@descuento, @impuesto, @total)" &
                                        "SELECT SCOPE_IDENTITY()"
                    Else
                        'Si no es cero es un detalle que existe en la BD, lo actualizamos
                        sql = "UPDATE DetalleFactura SET
                                            IdFactura=@idFactura,
                                            IdArticulo=@idArticulo, 
                                            Cantidad=@cantidad, 
                                            Precio=@precio, 
                                            SubTotal=@subTotal,
                                            Descuento=@descuento,
                                            Impuesto=@impuesto,
                                            Total=@total
                                            WHERE ID=@idDetalle"

                    End If

                    Using cmd As New SqlCommand(sql, conex)
                        ' como se reutiliza el mismo objeto SqlCommand es necesario limpiar los parametros
                        ' de la operacion previa, sino estos se iran agregando en la coleccion, generando un fallo
                        cmd.Parameters.Clear()

                        cmd.Parameters.AddWithValue("@idFactura", factura.IdFactura)
                        cmd.Parameters.AddWithValue("@idArticulo", detalle.IdArticulo)
                        cmd.Parameters.AddWithValue("@cantidad", detalle.Cantidad)
                        cmd.Parameters.AddWithValue("@precio", detalle.Precio)
                        cmd.Parameters.AddWithValue("@subTotal", detalle.SubTotal)
                        cmd.Parameters.AddWithValue("@descuento", detalle.Descuento)
                        cmd.Parameters.AddWithValue("@impuesto", detalle.Impuesto)
                        cmd.Parameters.AddWithValue("@total", detalle.Total)
                        cmd.Parameters.AddWithValue("@idDetalle", detalle.IdDetalleFactura)

                        If detalle.IdDetalleFactura = 0 Then
                            'Es un nuevo detalle, por tanto obtenemos el ID
                            detalle.IdDetalleFactura = Convert.ToInt32(cmd.ExecuteScalar())
                        Else
                            cmd.ExecuteNonQuery() 'Es una actualizacion
                        End If
                    End Using
                Next

            End Using

            'Indicamos que se inicien todas las transacciones
            scope.Complete()

        End Using

        Return factura
    End Function
    Public Shared Function GetAll() As List(Of FacturaEntity)
        Dim list As New List(Of FacturaEntity)()

        Using conex As New SqlConnection(m_CadenaConexion)
            conex.Open()

            Dim sql As String = "SELECT * FROM Factura ORDER BY Fecha Desc"
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
    Public Shared Function GetById(id As Integer) As FacturaEntity
        Dim factura As FacturaEntity = Nothing

        Using conex As New SqlConnection(m_CadenaConexion)
            conex.Open()

            Dim sql As String = "SELECT * FROM Factura " &
                                "WHERE Id = @idFactura"

            Dim cmd As New SqlCommand(sql, conex)
            cmd.Parameters.AddWithValue("@idFactura", id)

            Dim reader As SqlDataReader = cmd.ExecuteReader()
            '1. Leemos la unica fila posible que puede devolver esta sentencia
            '   y la convertimos en un objeto
            If reader.Read() Then
                factura = ConvertToObject(reader)

                '2. Recuperamos todos los detalles del pedido desde la BD, por esto
                '   establecemos la nueva sentencia SQL
                sql = "Select * From DetalleFactura Where IdFactura=@idFactura"
                cmd.CommandText = sql
                reader.Close()
                reader = cmd.ExecuteReader()
                While reader.Read
                    'Convertimos cada detalle en un objeto y lo agregamos la lista de detalles(saco de coco). 
                    'Con esto el objeto pedido contendra todos sus 
                    'detalles en la propiedad Detalles.
                    Dim det As DetalleFacturaEntity = ConvertToObjectDetalle(reader)
                    factura.Detalles.Add(det)
                End While
            End If
        End Using

        Return factura

    End Function
    Public Shared Function Exist(id As Integer) As Boolean
        'Esta funcion nos permite saber si un objeto existe antes de guardarlo.
        'Vea su uso en el metodo Save() de la capa BLL
        Dim NumRegistros As Integer = 0

        Using conex As New SqlConnection(m_CadenaConexion)
            conex.Open()

            Dim sql As String = "SELECT Count(*) " &
                                "FROM Factura " &
                                "WHERE IdFactura = @idFactura"

            Dim cmd As New SqlCommand(sql, conex)
            cmd.Parameters.AddWithValue("@idFactura", id)

            NumRegistros = Convert.ToInt32(cmd.ExecuteScalar())
        End Using

        Return NumRegistros > 0

    End Function

    'Metodos para convertir la fila del DataReader en un objeto
    Private Shared Function ConvertToObject(reader As IDataReader) As FacturaEntity
        Dim factura As New FacturaEntity()

        factura.IdFactura = Convert.ToInt32(reader("IdFactura"))
        factura.IdCliente = Convert.ToInt32(reader("IdCliente"))
        factura.Fecha = Convert.ToDateTime(reader("Fecha"))

        Return factura 'retornamos un objeto pedido
    End Function
    Private Shared Function ConvertToObjectDetalle(reader As IDataReader) As DetalleFacturaEntity
        Dim detalle As New DetalleFacturaEntity()

        detalle.IdDetalleFactura = Convert.ToInt32(reader("IdDetalleFactura"))
        detalle.IdFactura = Convert.ToInt32(reader("IdFactura"))
        detalle.IdArticulo = Convert.ToInt32(reader("IdArticulo"))
        detalle.Cantidad = Convert.ToDecimal(reader("Cantidad"))
        detalle.Precio = Convert.ToDecimal(reader("Precio"))
        detalle.SubTotal = Convert.ToDecimal(reader("SubTotal"))
        detalle.Descuento = Convert.ToDecimal(reader("Descuento"))
        detalle.Impuesto = Convert.ToDecimal(reader("Impuesto"))
        Return detalle 'retornamos el objeto
    End Function
End Class

