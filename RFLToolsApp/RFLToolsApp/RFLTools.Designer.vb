<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RFLTools
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.ButtonLoadHor = New System.Windows.Forms.Button()
        Me.TextHor = New System.Windows.Forms.TextBox()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.LabelLength = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxOffset = New System.Windows.Forms.TextBox()
        Me.TextBoxSta = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxX = New System.Windows.Forms.TextBox()
        Me.TextBoxY = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ButtonLoadFile = New System.Windows.Forms.Button()
        Me.TextConvertFile = New System.Windows.Forms.TextBox()
        Me.ButtonToXY = New System.Windows.Forms.Button()
        Me.ButtonToStaOff = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'ButtonLoadHor
        '
        Me.ButtonLoadHor.Location = New System.Drawing.Point(6, 11)
        Me.ButtonLoadHor.Name = "ButtonLoadHor"
        Me.ButtonLoadHor.Size = New System.Drawing.Size(66, 26)
        Me.ButtonLoadHor.TabIndex = 0
        Me.ButtonLoadHor.Text = "Load Hor"
        Me.ButtonLoadHor.UseVisualStyleBackColor = True
        '
        'TextHor
        '
        Me.TextHor.Location = New System.Drawing.Point(78, 16)
        Me.TextHor.Name = "TextHor"
        Me.TextHor.Size = New System.Drawing.Size(370, 20)
        Me.TextHor.TabIndex = 1
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'LabelLength
        '
        Me.LabelLength.AutoSize = True
        Me.LabelLength.Location = New System.Drawing.Point(17, 46)
        Me.LabelLength.Name = "LabelLength"
        Me.LabelLength.Size = New System.Drawing.Size(121, 13)
        Me.LabelLength.TabIndex = 2
        Me.LabelLength.Text = "Alignment Length = N/A"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.TextBoxOffset)
        Me.GroupBox1.Controls.Add(Me.TextBoxSta)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(10, 71)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(216, 63)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Station / Offset"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(107, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Offset:"
        '
        'TextBoxOffset
        '
        Me.TextBoxOffset.Location = New System.Drawing.Point(110, 36)
        Me.TextBoxOffset.Name = "TextBoxOffset"
        Me.TextBoxOffset.Size = New System.Drawing.Size(96, 20)
        Me.TextBoxOffset.TabIndex = 2
        '
        'TextBoxSta
        '
        Me.TextBoxSta.Location = New System.Drawing.Point(8, 36)
        Me.TextBoxSta.Name = "TextBoxSta"
        Me.TextBoxSta.Size = New System.Drawing.Size(96, 20)
        Me.TextBoxSta.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(5, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(26, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Sta:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.TextBoxX)
        Me.GroupBox2.Controls.Add(Me.TextBoxY)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Location = New System.Drawing.Point(232, 71)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(216, 63)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Coordinates"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(107, 20)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(17, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Y:"
        '
        'TextBoxX
        '
        Me.TextBoxX.Location = New System.Drawing.Point(8, 37)
        Me.TextBoxX.Name = "TextBoxX"
        Me.TextBoxX.Size = New System.Drawing.Size(96, 20)
        Me.TextBoxX.TabIndex = 6
        '
        'TextBoxY
        '
        Me.TextBoxY.Location = New System.Drawing.Point(110, 36)
        Me.TextBoxY.Name = "TextBoxY"
        Me.TextBoxY.Size = New System.Drawing.Size(96, 20)
        Me.TextBoxY.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(5, 20)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(17, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "X:"
        '
        'ButtonLoadFile
        '
        Me.ButtonLoadFile.Location = New System.Drawing.Point(6, 140)
        Me.ButtonLoadFile.Name = "ButtonLoadFile"
        Me.ButtonLoadFile.Size = New System.Drawing.Size(66, 26)
        Me.ButtonLoadFile.TabIndex = 5
        Me.ButtonLoadFile.Text = "Load File"
        Me.ButtonLoadFile.UseVisualStyleBackColor = True
        '
        'TextConvertFile
        '
        Me.TextConvertFile.Location = New System.Drawing.Point(78, 144)
        Me.TextConvertFile.Name = "TextConvertFile"
        Me.TextConvertFile.Size = New System.Drawing.Size(370, 20)
        Me.TextConvertFile.TabIndex = 6
        '
        'ButtonToXY
        '
        Me.ButtonToXY.Location = New System.Drawing.Point(6, 172)
        Me.ButtonToXY.Name = "ButtonToXY"
        Me.ButtonToXY.Size = New System.Drawing.Size(220, 26)
        Me.ButtonToXY.TabIndex = 7
        Me.ButtonToXY.Text = "To X/Y --->"
        Me.ButtonToXY.UseVisualStyleBackColor = True
        '
        'ButtonToStaOff
        '
        Me.ButtonToStaOff.Location = New System.Drawing.Point(228, 172)
        Me.ButtonToStaOff.Name = "ButtonToStaOff"
        Me.ButtonToStaOff.Size = New System.Drawing.Size(220, 26)
        Me.ButtonToStaOff.TabIndex = 8
        Me.ButtonToStaOff.Text = "<--- To Sta/Offset"
        Me.ButtonToStaOff.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(15, 201)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(354, 39)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "File must be comma delimited with one point per line." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Format must be ""STATION,OF" &
    "FSET"" or ""X,Y""." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Converted file will be same name and same location with extensi" &
    "on "".out""."
        '
        'RFLTools
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(455, 244)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.ButtonToStaOff)
        Me.Controls.Add(Me.ButtonToXY)
        Me.Controls.Add(Me.TextConvertFile)
        Me.Controls.Add(Me.ButtonLoadFile)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.LabelLength)
        Me.Controls.Add(Me.TextHor)
        Me.Controls.Add(Me.ButtonLoadHor)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Name = "RFLTools"
        Me.Text = "RFLTools"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ButtonLoadHor As Button
    Friend WithEvents TextHor As TextBox
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents LabelLength As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBoxOffset As TextBox
    Friend WithEvents TextBoxSta As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBoxX As TextBox
    Friend WithEvents TextBoxY As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents ButtonLoadFile As Button
    Friend WithEvents TextConvertFile As TextBox
    Friend WithEvents ButtonToXY As Button
    Friend WithEvents ButtonToStaOff As Button
    Friend WithEvents Label5 As Label
End Class
