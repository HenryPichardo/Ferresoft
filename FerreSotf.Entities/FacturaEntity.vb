Public Class FacturaEntity
    Public Property IdFactura As Integer
    Public Property IdCliente As Integer
    Public Property IdUsuarioSistema As Integer
    Public Property Fecha As Date

    Public Property SubTotal As Decimal
    Public Property TotalDescuento As Decimal
    Public Property TotalImpuesto As Decimal
    Public Property Total As Decimal
End Class
