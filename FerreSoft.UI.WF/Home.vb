Public Class Home
    Dim mysound As Media.SoundPlayer




#Region " Move Form "

    ' [ Move Form ]
    '
    ' // By Elektro 

    Public MoveForm As Boolean
        Public MoveForm_MousePosition As Point

        Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
        MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

            If e.Button = MouseButtons.Left Then
                MoveForm = True
                Me.Cursor = Cursors.NoMove2D
                MoveForm_MousePosition = e.Location
            End If

        End Sub

        Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
        MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

            If MoveForm Then
                Me.Location = Me.Location + (e.Location - MoveForm_MousePosition)
            End If

        End Sub

        Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
        MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)


            If e.Button = MouseButtons.Left Then
                MoveForm = False
                Me.Cursor = Cursors.Default
            End If

        End Sub

#End Region


    Private Sub BtnCerrar_Click(sender As Object, e As EventArgs) Handles BtnCerrar.Click
        mysound = New Media.SoundPlayer(My.Resources.button_1)
        mysound.Play()
        Me.Close()
    End Sub

    Private Sub BtnMinimizar_Click(sender As Object, e As EventArgs) Handles BtnMinimizar.Click
        mysound = New Media.SoundPlayer(My.Resources.button_2)
        mysound.Play()
        Me.WindowState = FormWindowState.Minimized
    End Sub


    Private Sub Home_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        mysound = New Media.SoundPlayer(My.Resources.button_3)
        mysound.Play()
        Panel2.Visible = False
    End Sub

    Private Sub BtnMaximizar_Click(sender As Object, e As EventArgs) Handles BtnMaximizar.Click
        mysound = New Media.SoundPlayer(My.Resources.button_4)
        mysound.Play()
    End Sub



    Private Sub BtnMenuShow_Click(sender As Object, e As EventArgs) Handles BtnMenuShow.Click
        mysound = New Media.SoundPlayer(My.Resources.button_5)
        mysound.Play()
        Panel2.Visible = True
    End Sub


    Private Sub Panel1_MouseClick(sender As Object, e As MouseEventArgs) Handles Panel1.MouseClick
        mysound = New Media.SoundPlayer(My.Resources.button_6)
        mysound.Play()
        Panel2.Visible = False
    End Sub

    Private Sub Panel2_MouseClick(sender As Object, e As MouseEventArgs) Handles Panel2.MouseClick, BtnMenuHide.MouseClick
        mysound = New Media.SoundPlayer(My.Resources.button_7)
        mysound.Play()
        Panel2.Visible = False
    End Sub


    Private Sub BtnMenuHide_Click(sender As Object, e As EventArgs) Handles BtnMenuHide.Click
        mysound = New Media.SoundPlayer(My.Resources.button_8)
        mysound.Play()
        Panel2.Visible = False
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        mysound = New Media.SoundPlayer(My.Resources.button_10)
        mysound.Play()
    End Sub


End Class
