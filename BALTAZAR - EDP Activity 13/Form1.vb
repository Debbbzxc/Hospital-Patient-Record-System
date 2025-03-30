Imports MySql.Data.MySqlClient

Public Class Form1
    Dim conn As MySqlConnection
    Dim COMMAND As MySqlCommand
    Dim buttonClicked As String = ""
    Private Sub btnConnect_Click(sender As Object, e As EventArgs) Handles btnConnect.Click
        conn = New MySqlConnection
        conn.ConnectionString = "server=localhost; userid=root; password=root; database=patients_db;"
        Try
            conn.Open()
            MessageBox.Show("Connection to the database was successful!")
            btnNew.Enabled = True
            btnUpdate.Enabled = True
            btnShowRecords.Enabled = True
            btnConnect.Enabled = False
        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()
        End Try
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        buttonClicked = "New Record"
        btnSave.Text = "Add"
        btnNew.Enabled = False
        btnUpdate.Enabled = True
        txtID.Enabled = False
        txtName.Enabled = True
        txtAge.Enabled = True
        txtDiagnosis.Enabled = True
        txtRoom.Enabled = True
        btnSave.Enabled = True
        btnDelete.Enabled = False
        txtID.Text = ""
        txtName.Text = ""
        txtAge.Text = ""
        txtDiagnosis.Text = ""
        txtRoom.Text = ""
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        buttonClicked = "Update Record"
        btnSave.Text = "Update"
        btnUpdate.Enabled = False
        btnNew.Enabled = True
        txtID.Enabled = True
        txtName.Enabled = True
        txtAge.Enabled = True
        txtDiagnosis.Enabled = True
        txtRoom.Enabled = True
        btnSave.Enabled = True
        btnDelete.Enabled = True
        txtID.Text = ""
        txtName.Text = ""
        txtAge.Text = ""
        txtDiagnosis.Text = ""
        txtRoom.Text = ""
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If buttonClicked = "New Record" Then
            Dim query As String = "INSERT INTO patients_tbl (name, age, diagnosis, room) VALUES (@name, @age, @diagnosis, @room)"
            Try
                If txtName.Text = "" Or txtAge.Text = "" Or txtDiagnosis.Text = "" Or txtRoom.Text = "" Then
                    Throw (New Exception("Input field/s cannot be empty!"))
                End If
                Using conn As New MySqlConnection("server=localhost; userid=root; password=root; database=patients_db;")
                    conn.Open()
                    Using cmd As New MySqlCommand(query, conn)
                        cmd.Parameters.AddWithValue("@name", txtName.Text)
                        cmd.Parameters.AddWithValue("@age", Convert.ToInt32(txtAge.Text))
                        cmd.Parameters.AddWithValue("@diagnosis", txtDiagnosis.Text)
                        cmd.Parameters.AddWithValue("@room", Convert.ToInt32(txtRoom.Text))
                        cmd.ExecuteNonQuery()
                        MessageBox.Show("New patient has been successfully added!")
                        btnNew.Enabled = True
                        btnUpdate.Enabled = True
                        txtID.Enabled = False
                        txtName.Enabled = False
                        txtAge.Enabled = False
                        txtDiagnosis.Enabled = False
                        txtRoom.Enabled = False
                        btnSave.Enabled = False
                        btnDelete.Enabled = False
                    End Using
                End Using
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        ElseIf buttonClicked = "Update Record" Then
            Dim query As String = "UPDATE patients_tbl SET name=@name, age=@age, diagnosis=@diagnosis, room=@room WHERE id=@id"
            Try
                If txtName.Text = "" Or txtAge.Text = "" Or txtDiagnosis.Text = "" Or txtRoom.Text = "" Or txtID.Text = "" Then
                    Throw (New Exception("Input field/s cannot be empty!"))
                End If
                Using conn As New MySqlConnection("server=localhost; userid=root; password=root; database=patients_db;")
                    conn.Open()
                    Using cmd As New MySqlCommand(query, conn)
                        cmd.Parameters.AddWithValue("@name", txtName.Text)
                        cmd.Parameters.AddWithValue("@age", Convert.ToInt32(txtAge.Text))
                        cmd.Parameters.AddWithValue("@diagnosis", txtDiagnosis.Text)
                        cmd.Parameters.AddWithValue("@room", Convert.ToInt32(txtRoom.Text))
                        cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtID.Text))
                        cmd.ExecuteNonQuery()
                        MessageBox.Show("Patient's record has been successfully updated!")
                        btnNew.Enabled = True
                        btnUpdate.Enabled = True
                        txtID.Enabled = False
                        txtName.Enabled = False
                        txtAge.Enabled = False
                        txtDiagnosis.Enabled = False
                        txtRoom.Enabled = False
                        btnSave.Enabled = False
                        btnDelete.Enabled = False
                    End Using
                End Using
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        Else
            MessageBox.Show("Choose an option first!")
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If buttonClicked = "Update Record" Then
            Dim query As String = "DELETE FROM patients_tbl WHERE id=@id"
            Try
                If txtID.Text = "" Then
                    Throw (New Exception("ID input field cannot be empty!"))
                End If
                Using conn As New MySqlConnection("server=localhost; userid=root; password=root; database=patients_db;")
                    conn.Open()
                    Using cmd As New MySqlCommand(query, conn)
                        cmd.Parameters.AddWithValue("id", Convert.ToInt32(txtID.Text))
                        cmd.ExecuteNonQuery()
                        MessageBox.Show("Patient's record has been successfully deleted!")
                        btnNew.Enabled = True
                        btnUpdate.Enabled = True
                        txtID.Enabled = False
                        txtName.Enabled = False
                        txtAge.Enabled = False
                        txtDiagnosis.Enabled = False
                        txtRoom.Enabled = False
                        btnSave.Enabled = False
                        btnDelete.Enabled = False
                    End Using
                End Using
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub btnShowRecords_Click(sender As Object, e As EventArgs) Handles btnShowRecords.Click
        Dim query As String = "SELECT id, name, age, diagnosis, room FROM `patients_db`.`patients_tbl`"
        Try
            Using conn As New MySqlConnection("server=localhost; userid=root; password=root; database=patients_db;")
                Dim adapter As New MySqlDataAdapter(query, conn)
                Dim table As New DataTable()
                adapter.Fill(table)
                dgvPatients.DataSource = table
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        btnNew.Enabled = False
        btnUpdate.Enabled = False
        btnSave.Enabled = False
        btnDelete.Enabled = False
        btnShowRecords.Enabled = False
        txtID.Enabled = False
        txtName.Enabled = False
        txtAge.Enabled = False
        txtDiagnosis.Enabled = False
        txtRoom.Enabled = False
    End Sub
End Class
