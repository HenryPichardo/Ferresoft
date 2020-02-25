Public MustInherit Class BaseDal
    'Esta clase es utilizada para contener las propiedades y metodos comunes a todas las clases DAL
    'y solo se puede utilizar a traves de la herencia ==> MustInherit
    'Protected Shared m_CadenaConexion As String = "Data Source=L17-31-151;Initial Catalog=PedidosBD;User Id=sa; Password=Uca.1234"
    Protected Shared m_CadenaConexion As String = "Data Source=LAPTOP-ACER\SQLEXPRESS;Initial Catalog=PedidosBD; Integrated Security= True"
End Class
