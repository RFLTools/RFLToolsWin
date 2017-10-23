Imports System.IO

Public Class RFLTools
    Dim Alignment As New AlignList

    Private Sub RFLTools_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub ButtonLoadHor_Click(sender As Object, e As EventArgs) Handles ButtonLoadHor.Click
        LoadHorFile()
    End Sub

    Private Sub LoadHorFile()
        With OpenFileDialog1
            .FilterIndex = 1
            .Title = "Please Select a Horizontal File"
            .InitialDirectory = "%USERPROFILE%\My Documents"
            .Filter = "RFL Alignment Files (*.hor)|*.hor"
            .FileName = ""
            .CheckFileExists = True
            .ShowDialog()
        End With
        TextHor.Text = OpenFileDialog1.FileName
        If (OpenFileDialog1.FileName.CompareTo("") = 0) Then
            LabelLength.Text = "Alignment Length : N/A"
        Else
            Alignment.GetAlign(OpenFileDialog1.FileName)
            LabelLength.Text = "Alignment Length : " & Format(Alignment.Length, "#0.000")
            TextBoxSta.Text = Format(Alignment.StaStart, "#0.000")
            TextBoxOffset.Text = Format(0.0, "#0.000")
            UpdateXY()
        End If
    End Sub

    Private Sub UpdateXY()
        Dim PStaOffset, PXY As DataTypes.Point2d
        Double.TryParse(TextBoxSta.Text, PStaOffset.X)
        Double.TryParse(TextBoxOffset.Text, PStaOffset.Y)
        If Alignment.XY(PStaOffset, PXY) Then
            TextBoxX.Text = Format(PXY.X, "#0.000")
            TextBoxY.Text = Format(PXY.Y, "#0.000")
        Else
            TextBoxX.Text = "N/A"
            TextBoxY.Text = "N/A"
        End If
    End Sub

    Private Sub UpdateStaOff()
        Dim PStaOffset, PXY As DataTypes.Point2d
        Double.TryParse(TextBoxX.Text, PXY.X)
        Double.TryParse(TextBoxY.Text, PXY.Y)
        If Alignment.StaOff(PXY, PStaOffset) Then
            TextBoxSta.Text = Format(PStaOffset.X, "#0.000")
            TextBoxOffset.Text = Format(PStaOffset.Y, "#0.000")
        Else
            TextBoxSta.Text = "N/A"
            TextBoxOffset.Text = "N/A"
        End If
    End Sub

    Private Sub TextBoxSta_TextChanged(sender As Object, e As EventArgs) Handles TextBoxSta.TextChanged

    End Sub

    Private Sub TextBoxOffset_TextChanged(sender As Object, e As EventArgs) Handles TextBoxOffset.TextChanged

    End Sub

    Private Sub TextBoxSta_LostFocus(sender As Object, e As EventArgs) Handles TextBoxSta.LostFocus
        UpdateXY()
    End Sub

    Private Sub TextBoxOffset_LostFocus(sender As Object, e As EventArgs) Handles TextBoxOffset.LostFocus
        UpdateXY()
    End Sub

    Private Sub TextBoxX_TextChanged(sender As Object, e As EventArgs) Handles TextBoxX.TextChanged

    End Sub

    Private Sub TextBoxY_TextChanged(sender As Object, e As EventArgs) Handles TextBoxY.TextChanged

    End Sub

    Private Sub TextBoxX_LostFocus(sender As Object, e As EventArgs) Handles TextBoxX.LostFocus
        UpdateStaOff()
    End Sub

    Private Sub TextBoxY_LostFocus(sender As Object, e As EventArgs) Handles TextBoxY.LostFocus
        UpdateStaOff()
    End Sub

    Private Sub RFLTools_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        'If Control.ModifierKeys = Keys.Alt Then
        If e.KeyCode = Keys.F1 Then
            e.Handled = True
            TextBoxSta.Focus()
            TextBoxSta.SelectAll()
        End If
        If e.KeyCode = Keys.F2 Then
            e.Handled = True
            TextBoxOffset.Focus()
            TextBoxOffset.SelectAll()
        End If
        If e.KeyCode = Keys.F3 Then
            e.Handled = True
            TextBoxX.Focus()
            TextBoxX.SelectAll()
        End If
        If e.KeyCode = Keys.F4 Then
            e.Handled = True
            TextBoxY.Focus()
            TextBoxY.SelectAll()
        End If
        If e.KeyCode = Keys.F5 Then
            e.Handled = True
            TextConvertFile.Focus()
            TextConvertFile.SelectAll()
        End If
        If e.KeyCode = Keys.F6 Then
            e.Handled = True
            ToXY()
        End If
        If e.KeyCode = Keys.F7 Then
            e.Handled = True
            ToStaOff()
        End If
        'End If
    End Sub

    Private Sub ButtonLoadFile_Click(sender As Object, e As EventArgs) Handles ButtonLoadFile.Click
        LoadDataFile()
    End Sub

    Private Sub LoadDataFile()
        With OpenFileDialog1
            .FilterIndex = 1
            .Title = "Please select a data file"
            .InitialDirectory = "%USERPROFILE%\My Documents"
            .Filter = "Excel CSV|*.csv|Text|*.txt|All|*.*"
            .FileName = ""
            .CheckFileExists = True
            .ShowDialog()
        End With
        TextConvertFile.Text = OpenFileDialog1.FileName
    End Sub

    Private Function Line2PNT(ByVal Line As String, ByRef P As DataTypes.Point2d) As Boolean
        Dim Delim As String = ","
        Dim FoundX As Boolean = False
        Dim FoundY As Boolean = False
        Dim C As Integer

        Try
            C = Line.IndexOf(Delim)
            If IsNumeric(Line.Substring(0, C)) Then
                FoundX = True
            End If
            If IsNumeric(Line.Substring(C + 1)) Then
                FoundY = True
            End If
        Catch ex As Exception

        End Try

        If FoundX And FoundY Then
            Double.TryParse(Line.Substring(0, C), P.X)
            Double.TryParse(Line.Substring(C + 1), P.Y)
            Return True
        Else
            Return False
        End If

    End Function

    Private Sub ButtonToXY_Click(sender As Object, e As EventArgs) Handles ButtonToXY.Click
        ToXY()
    End Sub

    Private Sub ToXY()
        Try
            Dim oReader = File.OpenText(TextConvertFile.Text)
            Dim line As String
            Dim PIN, POUT As DataTypes.Point2d

            Try
                Dim oWriter = My.Computer.FileSystem.OpenTextFileWriter(TextConvertFile.Text & ".tmp", False, System.Text.Encoding.ASCII)

                While (oReader.EndOfStream = False)
                    line = oReader.ReadLine()
                    If Line2PNT(line, PIN) And Alignment.XY(PIN, POUT) Then
                        oWriter.WriteLine(POUT.X.ToString & "," & POUT.Y.ToString)
                    Else
                        oWriter.WriteLine("N/A,N/A")
                    End If
                End While

                oWriter.Close()

                Dim newName As String = Path.ChangeExtension(TextConvertFile.Text & ".tmp", "out")
                File.Move(TextConvertFile.Text & ".tmp", newName)
            Catch ex As Exception

            End Try
            oReader.Close()
        Catch ex As Exception
            MsgBox("File not found")
        End Try
    End Sub

    Private Sub ButtonToStaOff_Click(sender As Object, e As EventArgs) Handles ButtonToStaOff.Click
        ToStaOff()
    End Sub

    Private Sub ToStaOff()
        Try
            Dim oReader = File.OpenText(TextConvertFile.Text)
            Dim line As String
            Dim PIN, POUT As DataTypes.Point2d

            Try
                Dim oWriter = My.Computer.FileSystem.OpenTextFileWriter(TextConvertFile.Text & ".tmp", False, System.Text.Encoding.ASCII)

                While (oReader.EndOfStream = False)
                    line = oReader.ReadLine()
                    If Line2PNT(line, PIN) And Alignment.StaOff(PIN, POUT) Then
                        oWriter.WriteLine(POUT.X.ToString & "," & POUT.Y.ToString)
                    Else
                        oWriter.WriteLine("N/A,N/A")
                    End If
                End While

                oWriter.Close()

                Dim newName As String = Path.ChangeExtension(TextConvertFile.Text & ".tmp", "out")
                File.Move(TextConvertFile.Text & ".tmp", newName)
            Catch ex As Exception

            End Try
            oReader.Close()
        Catch ex As Exception
            MsgBox("File not found")
        End Try
    End Sub

End Class
