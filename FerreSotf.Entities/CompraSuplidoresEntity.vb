Public Class CompraSuplidoresEntity
    Public Property IdCompraSuplidores As Integer
    Public Property IdSuplidor As Integer
    Public Property IdUsuarioSistema As Integer
    Public Property Fecha As Date
    Public Property vencimiento As Date
    Public Property Estado As String

    'Propiedad para almacenar los detalles del pedido
    Private _detalles As List(Of DetalleCompraSuplidoresEntity)
    Public Property Detalles() As List(Of DetalleCompraSuplidoresEntity)
        Get
            Return _detalles
        End Get
        Set(ByVal value As List(Of DetalleCompraSuplidoresEntity))
            _detalles = value
        End Set
    End Property

End Class
