Imports System.Data.SqlClient

Public Class Cumpleanios

    Dim valor As String = ""

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        recargar()
    End Sub

    Function recargar()
        ListView1.Clear() 'Limpiamos el ListView
        ListView1.View = View.Details 'Tipo de vista
        ListView1.FullRowSelect = True 'Al seleccionar un elemento, seleccionar la línea completa
        ListView1.GridLines = True 'Mostrar las líneas de la cuadrícula
        ListView1.LabelEdit = False 'No permitir la edición automática del texto
        ListView1.MultiSelect = False 'Permitir múltiple selección
        ListView1.HideSelection = False 'Para que al perder el foco, se siga viendo el que está seleccionado
        ListView1.ShowGroups = False 'Listado NO Agrupado

        ListView1.Columns.Add("Nombre", 250, HorizontalAlignment.Left)
        ListView1.Columns.Add("Fecha", 90, HorizontalAlignment.Left)
        ListView1.View = View.Details

        cargarTabla()
        Return Nothing
    End Function

    Function agregaTabla(ByVal nombre As String, ByVal fecha As String)
        Dim Dia As Integer = Now.Date.Day
        Dim mes As Integer = Now.Date.Month
        Dim anio As Integer = Now.Date.Year
        Dim mesC As Integer = DateTimePicker1.Value.Month
        Dim DiaC As Integer = DateTimePicker1.Value.Day
        Dim anioC As Integer = DateTimePicker1.Value.Year
        Dim fechaC As Date
        If mesC < mes Then
            fechaC = "" & DiaC & "/" & mesC & "/" & (anio + 1) & ""
        Else
            fechaC = "" & DiaC & "/" & mesC & "/" & anio & ""
        End If
        Try
            Using cnx = New SqlConnection("Data Source=192.168.3.254;Initial Catalog=Vistas;User ID=sa;Password=$3rv3r5q10621%")
                cnx.Open()
                Using cmd As New SqlCommand()
                    cmd.Connection = cnx
                    cmd.CommandText = "SET DATEFORMAT dmy   insert into RH_cumpleanios (Nombre,FechaN,FechaC) values (@nombre,@fecha,@fCumple)"
                    cmd.Parameters.AddWithValue("@nombre", nombre)
                    cmd.Parameters.AddWithValue("@fecha", fecha)
                    cmd.Parameters.AddWithValue("@fCumple", fechaC)
                    cmd.ExecuteNonQuery()

                    Dim Litems As New ListViewItem

                    Litems = ListView1.Items.Add(nombre)
                    Litems.SubItems.Add(fechaC)
                End Using
                cnx.Close()
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message, "HA ocurrido un error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return Nothing
    End Function

    Function AgregaLista(ByVal nombre As String, ByVal fecha As String)

        Dim Litems As New ListViewItem

        Litems = ListView1.Items.Add(nombre)
        Litems.SubItems.Add(CDate(fecha))

        Return Nothing
    End Function

    Function cargarTabla()
        Dim nombre As String = ""
        Dim fecha As String = ""
        Dim lector As SqlDataReader
        Try
            Using cnx = New SqlConnection("Data Source=192.168.3.254;Initial Catalog=Vistas;User ID=sa;Password=$3rv3r5q10621%")
                cnx.Open()
                Using cmd As New SqlCommand()
                    cmd.Connection = cnx
                    cmd.CommandText = "SET DATEFORMAT dmy   select Nombre, CONVERT(CHAR(10), FechaC, 111) as FechaC from vistas.dbo.RH_Cumpleanios order by FechaC"
                    'cmd.ExecuteNonQuery()

                    lector = cmd.ExecuteReader
                    While lector.Read

                        nombre = CStr(lector(0).ToString)
                        fecha = CStr(lector(1).ToString)


                        AgregaLista(nombre, fecha)
                    End While
                    lector.Close()

                End Using
                cnx.Close()
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message, "HA ocurrido un error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return Nothing
    End Function

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text <> "" Then
            agregaTabla(TextBox1.Text, CDate(DateTimePicker1.Text))
            TextBox1.Text = ""
            DateTimePicker1.Value = Today
        End If


    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ListView1.SelectedIndexChanged
        Try
            valor = ListView1.SelectedItems(0).Text
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        eliminarTabla(valor)
    End Sub

    Function eliminarTabla(ByVal nombre As String)
        Try
            Using cnx = New SqlConnection("Data Source=192.168.3.254;Initial Catalog=Vistas;User ID=sa;Password=$3rv3r5q10621%")
                cnx.Open()
                Using cmd As New SqlCommand()
                    cmd.Connection = cnx
                    cmd.CommandText = "delete from RH_Cumpleanios where Nombre='" & nombre & "'"
                    cmd.ExecuteNonQuery()

                    recargar()
                End Using
                cnx.Close()
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message, "HA ocurrido un error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return Nothing
    End Function


    Function recargarbusca()
        ListView1.Clear() 'Limpiamos el ListView
        ListView1.View = View.Details 'Tipo de vista
        ListView1.FullRowSelect = True 'Al seleccionar un elemento, seleccionar la línea completa
        ListView1.GridLines = True 'Mostrar las líneas de la cuadrícula
        ListView1.LabelEdit = False 'No permitir la edición automática del texto
        ListView1.MultiSelect = False 'Permitir múltiple selección
        ListView1.HideSelection = False 'Para que al perder el foco, se siga viendo el que está seleccionado
        ListView1.ShowGroups = False 'Listado NO Agrupado

        ListView1.Columns.Add("Nombre", 250, HorizontalAlignment.Left)
        ListView1.Columns.Add("Fecha", 90, HorizontalAlignment.Left)
        ListView1.View = View.Details

        cargarTablabusca()
        Return Nothing
    End Function

    Function cargarTablabusca()
        Dim nombre As String = ""
        Dim fecha As String = ""
        Dim lector As SqlDataReader
        Try
            Using cnx = New SqlConnection("Data Source=192.168.3.254;Initial Catalog=Vistas;User ID=sa;Password=$3rv3r5q10621%")
                cnx.Open()
                Using cmd As New SqlCommand()
                    cmd.Connection = cnx
                    cmd.CommandText = "SET DATEFORMAT dmy   select Nombre, CONVERT(CHAR(10), FechaC, 111) as FechaC from vistas.dbo.RH_Cumpleanios where Nombre LIKE '" & TextBox2.Text & "%' Order by FechaC"
                    'cmd.ExecuteNonQuery()

                    lector = cmd.ExecuteReader
                    While lector.Read

                        nombre = CStr(lector(0).ToString)
                        fecha = CStr(lector(1).ToString)


                        AgregaLista(nombre, fecha)
                    End While
                    lector.Close()

                End Using
                cnx.Close()
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message, "HA ocurrido un error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return Nothing
    End Function

    Function recargarmes(ByVal mes As Integer, ByVal anio As Integer)
        ListView1.Clear() 'Limpiamos el ListView
        ListView1.View = View.Details 'Tipo de vista
        ListView1.FullRowSelect = True 'Al seleccionar un elemento, seleccionar la línea completa
        ListView1.GridLines = True 'Mostrar las líneas de la cuadrícula
        ListView1.LabelEdit = False 'No permitir la edición automática del texto
        ListView1.MultiSelect = False 'Permitir múltiple selección
        ListView1.HideSelection = False 'Para que al perder el foco, se siga viendo el que está seleccionado
        ListView1.ShowGroups = False 'Listado NO Agrupado

        ListView1.Columns.Add("Nombre", 250, HorizontalAlignment.Left)
        ListView1.Columns.Add("Fecha", 90, HorizontalAlignment.Left)
        ListView1.View = View.Details

        cargarTablames(mes, anio)
        Return Nothing
    End Function

    Function cargarTablames(ByVal mes As Integer, ByVal anio As Integer)
        Dim nombre As String = ""
        Dim fecha As String = ""
        Dim lector As SqlDataReader
        Try
            Using cnx = New SqlConnection("Data Source=192.168.3.254;Initial Catalog=Vistas;User ID=sa;Password=$3rv3r5q10621%")
                cnx.Open()
                Using cmd As New SqlCommand()
                    cmd.Connection = cnx
                    Dim messig, aniosig As Integer
                    If mes < 12 Then
                        messig = mes + 1
                        aniosig = anio
                    End If
                    If mes = 12 Then
                        messig = 1

                        aniosig = anio + 1
                    End If

                    cmd.CommandText = "SET DATEFORMAT dmy   select Nombre, CONVERT(CHAR(10), FechaC, 111) as FechaC from vistas.dbo.RH_Cumpleanios where FechaC>='01-" & mes & "-" & anio & "' and FechaC<'01-" & messig & "-" & aniosig & "' Order by FechaC"
                    'cmd.ExecuteNonQuery()

                    lector = cmd.ExecuteReader
                    While lector.Read

                        nombre = CStr(lector(0).ToString)
                        fecha = CStr(lector(1).ToString)


                        AgregaLista(nombre, fecha)
                    End While
                    lector.Close()

                End Using
                cnx.Close()
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message, "HA ocurrido un error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return Nothing
    End Function

    Private Sub TextBox2_GotFocus(sender As Object, e As System.EventArgs) Handles TextBox2.GotFocus
        If TextBox2.Text = "Buscar por nombre" Then
            TextBox2.ForeColor = Color.Black
            TextBox2.Text = ""
        End If
    End Sub

    Private Sub TextBox2_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyUp
        recargarbusca()
        If TextBox2.Text <> "" Then
            Label4.Visible = True
        Else
            Label4.Visible = False
        End If
    End Sub

    Private Sub Label4_Click(sender As System.Object, e As System.EventArgs) Handles Label4.Click
        TextBox2.Text = ""
        recargar()
        Label4.Visible = False
    End Sub

    Private Sub TextBox2_LostFocus(sender As Object, e As System.EventArgs) Handles TextBox2.LostFocus
        TextBox2.ForeColor = Color.Gray
        TextBox2.Text = "Buscar por nombre"

    End Sub

    
    Private Sub ComboBox1_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedIndex = 0 Then
            recargarmes(1, Now.Date.Year)
        End If
        If ComboBox1.SelectedIndex = 1 Then
            recargarmes(2, Now.Date.Year)
        End If
        If ComboBox1.SelectedIndex = 2 Then
            recargarmes(3, Now.Date.Year)
        End If
        If ComboBox1.SelectedIndex = 3 Then
            recargarmes(4, Now.Date.Year)
        End If
        If ComboBox1.SelectedIndex = 4 Then
            recargarmes(5, Now.Date.Year)
        End If
        If ComboBox1.SelectedIndex = 5 Then
            recargarmes(6, Now.Date.Year)
        End If
        If ComboBox1.SelectedIndex = 6 Then
            recargarmes(7, Now.Date.Year)
        End If
        If ComboBox1.SelectedIndex = 7 Then
            recargarmes(8, Now.Date.Year)
        End If
        If ComboBox1.SelectedIndex = 8 Then
            recargarmes(9, Now.Date.Year)
        End If
        If ComboBox1.SelectedIndex = 9 Then
            recargarmes(10, Now.Date.Year)
        End If
        If ComboBox1.SelectedIndex = 10 Then
            recargarmes(11, Now.Date.Year)
        End If
        If ComboBox1.SelectedIndex = 11 Then
            recargarmes(12, Now.Date.Year)
        End If
        If ComboBox1.SelectedIndex = 12 Then
            recargar()
        End If
    End Sub
End Class
