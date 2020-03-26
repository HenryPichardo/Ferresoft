Public Class FacturaEntity
    Sub New()
        Detalles = New List(Of DetalleFacturaEntity)
    End Sub
    Public Property IdFactura As Integer
    Public Property IdCliente As Integer
    Public Property IdUsuarioSistema As Integer
    Public Property Fecha As Date


    'Creamos Propiedades de solo lectura para los totales
    Public ReadOnly Property SubTotal() As Decimal
        Get
            Return Detalles.Sum(Function(detalle As DetalleFacturaEntity) detalle.SubTotal)
        End Get
    End Property
    Public ReadOnly Property TotalDescuento() As Decimal
        Get
            Return (From d In Detalles Select d.Descuento).Sum()
        End Get
    End Property
    Public ReadOnly Property TotalImpuesto() As Decimal
        Get
            Return (From d In Detalles Select d.Impuesto).Sum()
        End Get
    End Property
    Public ReadOnly Property Total() As Decimal
        Get
            Return SubTotal - TotalDescuento + TotalImpuesto
        End Get
    End Property

    'Propiedad para almacenar los detalles del pedido
    Private _detalles As List(Of DetalleFacturaEntity)
    Public Property Detalles() As List(Of DetalleFacturaEntity)
        Get
            Return _Detalles
        End Get
        Set(ByVal value As List(Of DetalleFacturaEntity))
            _Detalles = value
        End Set
    End Property
End Class
