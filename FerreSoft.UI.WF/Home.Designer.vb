<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Home
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Home))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.WebBrowser1 = New System.Windows.Forms.WebBrowser()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.PictureBox6 = New System.Windows.Forms.PictureBox()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.BtnMenuHide = New System.Windows.Forms.PictureBox()
        Me.BtnCerrar = New System.Windows.Forms.PictureBox()
        Me.BtnMaximizar = New System.Windows.Forms.PictureBox()
        Me.BtnMinimizar = New System.Windows.Forms.PictureBox()
        Me.BtnMenuShow = New System.Windows.Forms.PictureBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnMenuHide, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnCerrar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnMaximizar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnMinimizar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtnMenuShow, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel1.Controls.Add(Me.WebBrowser1)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(2, 66)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1339, 790)
        Me.Panel1.TabIndex = 0
        '
        'WebBrowser1
        '
        Me.WebBrowser1.Location = New System.Drawing.Point(-3, 0)
        Me.WebBrowser1.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebBrowser1.Name = "WebBrowser1"
        Me.WebBrowser1.ScrollBarsEnabled = False
        Me.WebBrowser1.Size = New System.Drawing.Size(1339, 752)
        Me.WebBrowser1.TabIndex = 1
        Me.WebBrowser1.Url = New System.Uri("file:///C:/Users/Henry%20Pichardo/Documents/Universidad/Cuatrimestre%20enero-abri" &
        "l%202020/Proyecto%20II/Ferresoft/RepDig/index.html", System.UriKind.Absolute)
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MV Boli", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(1160, 755)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(170, 28)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "FerreSoft v1.0"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel2.Controls.Add(Me.PictureBox6)
        Me.Panel2.Controls.Add(Me.Button4)
        Me.Panel2.Controls.Add(Me.Button5)
        Me.Panel2.Controls.Add(Me.Button6)
        Me.Panel2.Controls.Add(Me.Button3)
        Me.Panel2.Controls.Add(Me.Button2)
        Me.Panel2.Controls.Add(Me.Button1)
        Me.Panel2.Controls.Add(Me.BtnMenuHide)
        Me.Panel2.Location = New System.Drawing.Point(2, -3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(355, 859)
        Me.Panel2.TabIndex = 0
        '
        'PictureBox6
        '
        Me.PictureBox6.BackgroundImage = CType(resources.GetObject("PictureBox6.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.PictureBox6.Location = New System.Drawing.Point(77, 132)
        Me.PictureBox6.Name = "PictureBox6"
        Me.PictureBox6.Size = New System.Drawing.Size(206, 217)
        Me.PictureBox6.TabIndex = 7
        Me.PictureBox6.TabStop = False
        '
        'Button4
        '
        Me.Button4.BackColor = System.Drawing.Color.Blue
        Me.Button4.Font = New System.Drawing.Font("MV Boli", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button4.ForeColor = System.Drawing.Color.White
        Me.Button4.Location = New System.Drawing.Point(-3, 601)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(356, 69)
        Me.Button4.TabIndex = 6
        Me.Button4.Text = "Informes"
        Me.Button4.UseVisualStyleBackColor = False
        '
        'Button5
        '
        Me.Button5.BackColor = System.Drawing.Color.Blue
        Me.Button5.Font = New System.Drawing.Font("MV Boli", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button5.ForeColor = System.Drawing.Color.White
        Me.Button5.Location = New System.Drawing.Point(-3, 670)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(356, 69)
        Me.Button5.TabIndex = 5
        Me.Button5.Text = "Herramientas"
        Me.Button5.UseVisualStyleBackColor = False
        '
        'Button6
        '
        Me.Button6.BackColor = System.Drawing.Color.Blue
        Me.Button6.Font = New System.Drawing.Font("MV Boli", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button6.ForeColor = System.Drawing.Color.White
        Me.Button6.Location = New System.Drawing.Point(-3, 739)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(356, 69)
        Me.Button6.TabIndex = 4
        Me.Button6.Text = "Ayuda"
        Me.Button6.UseVisualStyleBackColor = False
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.Blue
        Me.Button3.Font = New System.Drawing.Font("MV Boli", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.ForeColor = System.Drawing.Color.White
        Me.Button3.Location = New System.Drawing.Point(-3, 532)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(356, 69)
        Me.Button3.TabIndex = 3
        Me.Button3.Text = "Consultas"
        Me.Button3.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.Blue
        Me.Button2.Font = New System.Drawing.Font("MV Boli", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.Color.White
        Me.Button2.Location = New System.Drawing.Point(-3, 463)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(356, 69)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "Procesos"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.Blue
        Me.Button1.Font = New System.Drawing.Font("MV Boli", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(-3, 394)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(356, 69)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Mantenimiento"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'BtnMenuHide
        '
        Me.BtnMenuHide.BackgroundImage = CType(resources.GetObject("BtnMenuHide.BackgroundImage"), System.Drawing.Image)
        Me.BtnMenuHide.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.BtnMenuHide.Location = New System.Drawing.Point(282, 10)
        Me.BtnMenuHide.Name = "BtnMenuHide"
        Me.BtnMenuHide.Size = New System.Drawing.Size(52, 50)
        Me.BtnMenuHide.TabIndex = 0
        Me.BtnMenuHide.TabStop = False
        '
        'BtnCerrar
        '
        Me.BtnCerrar.BackgroundImage = CType(resources.GetObject("BtnCerrar.BackgroundImage"), System.Drawing.Image)
        Me.BtnCerrar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.BtnCerrar.Location = New System.Drawing.Point(1279, 12)
        Me.BtnCerrar.Name = "BtnCerrar"
        Me.BtnCerrar.Size = New System.Drawing.Size(50, 50)
        Me.BtnCerrar.TabIndex = 1
        Me.BtnCerrar.TabStop = False
        '
        'BtnMaximizar
        '
        Me.BtnMaximizar.BackgroundImage = CType(resources.GetObject("BtnMaximizar.BackgroundImage"), System.Drawing.Image)
        Me.BtnMaximizar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.BtnMaximizar.Location = New System.Drawing.Point(1223, 12)
        Me.BtnMaximizar.Name = "BtnMaximizar"
        Me.BtnMaximizar.Size = New System.Drawing.Size(50, 50)
        Me.BtnMaximizar.TabIndex = 2
        Me.BtnMaximizar.TabStop = False
        '
        'BtnMinimizar
        '
        Me.BtnMinimizar.BackgroundImage = CType(resources.GetObject("BtnMinimizar.BackgroundImage"), System.Drawing.Image)
        Me.BtnMinimizar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.BtnMinimizar.Location = New System.Drawing.Point(1167, 12)
        Me.BtnMinimizar.Name = "BtnMinimizar"
        Me.BtnMinimizar.Size = New System.Drawing.Size(50, 50)
        Me.BtnMinimizar.TabIndex = 2
        Me.BtnMinimizar.TabStop = False
        '
        'BtnMenuShow
        '
        Me.BtnMenuShow.BackgroundImage = CType(resources.GetObject("BtnMenuShow.BackgroundImage"), System.Drawing.Image)
        Me.BtnMenuShow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.BtnMenuShow.Location = New System.Drawing.Point(12, 13)
        Me.BtnMenuShow.Name = "BtnMenuShow"
        Me.BtnMenuShow.Size = New System.Drawing.Size(52, 50)
        Me.BtnMenuShow.TabIndex = 1
        Me.BtnMenuShow.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("MV Boli", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(72, 13)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(98, 41)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Inicio"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Home
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Blue
        Me.ClientSize = New System.Drawing.Size(1343, 858)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.BtnMinimizar)
        Me.Controls.Add(Me.BtnMaximizar)
        Me.Controls.Add(Me.BtnCerrar)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.BtnMenuShow)
        Me.Controls.Add(Me.Label2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Home"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnMenuHide, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnCerrar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnMaximizar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnMinimizar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtnMenuShow, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents BtnMenuHide As PictureBox
    Friend WithEvents BtnCerrar As PictureBox
    Friend WithEvents BtnMaximizar As PictureBox
    Friend WithEvents BtnMinimizar As PictureBox
    Friend WithEvents BtnMenuShow As PictureBox
    Friend WithEvents Button4 As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents PictureBox6 As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents WebBrowser1 As WebBrowser
    Friend WithEvents Label2 As Label
End Class
