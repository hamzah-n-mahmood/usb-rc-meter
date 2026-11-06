<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main_frm
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
        Me.components = New System.ComponentModel.Container()
        Me.Measure_btn = New System.Windows.Forms.Button()
        Me.About_btn = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.msrdval_txt = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txt_Unit = New System.Windows.Forms.TextBox()
        Me.SerialPort = New System.IO.Ports.SerialPort(Me.components)
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.btn_Disconnect = New System.Windows.Forms.Button()
        Me.btn_connect = New System.Windows.Forms.Button()
        Me.GrpBox_CurMeas = New System.Windows.Forms.GroupBox()
        Me.RdoBtn_Capacitance = New System.Windows.Forms.RadioButton()
        Me.RdoBtn_Resistance = New System.Windows.Forms.RadioButton()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GrpBox_CurMeas.SuspendLayout()
        Me.SuspendLayout()
        '
        'Measure_btn
        '
        Me.Measure_btn.Location = New System.Drawing.Point(159, 212)
        Me.Measure_btn.Name = "Measure_btn"
        Me.Measure_btn.Size = New System.Drawing.Size(118, 37)
        Me.Measure_btn.TabIndex = 1
        Me.Measure_btn.Text = "&Perform Measurement"
        Me.Measure_btn.UseVisualStyleBackColor = True
        '
        'About_btn
        '
        Me.About_btn.Location = New System.Drawing.Point(2, 281)
        Me.About_btn.Name = "About_btn"
        Me.About_btn.Size = New System.Drawing.Size(75, 23)
        Me.About_btn.TabIndex = 4
        Me.About_btn.Text = "&About"
        Me.About_btn.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.msrdval_txt)
        Me.GroupBox1.ForeColor = System.Drawing.Color.White
        Me.GroupBox1.Location = New System.Drawing.Point(76, 116)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(195, 79)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Measured Value"
        '
        'msrdval_txt
        '
        Me.msrdval_txt.BackColor = System.Drawing.Color.Black
        Me.msrdval_txt.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.msrdval_txt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.msrdval_txt.Location = New System.Drawing.Point(20, 20)
        Me.msrdval_txt.MaxLength = 6
        Me.msrdval_txt.Name = "msrdval_txt"
        Me.msrdval_txt.ReadOnly = True
        Me.msrdval_txt.Size = New System.Drawing.Size(160, 44)
        Me.msrdval_txt.TabIndex = 6
        Me.msrdval_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.txt_Unit)
        Me.GroupBox2.ForeColor = System.Drawing.Color.White
        Me.GroupBox2.Location = New System.Drawing.Point(270, 116)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(82, 79)
        Me.GroupBox2.TabIndex = 6
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Unit"
        '
        'txt_Unit
        '
        Me.txt_Unit.Font = New System.Drawing.Font("Times New Roman", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_Unit.Location = New System.Drawing.Point(9, 20)
        Me.txt_Unit.Name = "txt_Unit"
        Me.txt_Unit.Size = New System.Drawing.Size(61, 39)
        Me.txt_Unit.TabIndex = 7
        Me.txt_Unit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GroupBox4
        '
        Me.GroupBox4.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox4.Controls.Add(Me.btn_Disconnect)
        Me.GroupBox4.Controls.Add(Me.btn_connect)
        Me.GroupBox4.ForeColor = System.Drawing.Color.White
        Me.GroupBox4.Location = New System.Drawing.Point(323, 0)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(157, 45)
        Me.GroupBox4.TabIndex = 12
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Device Connection Options"
        '
        'btn_Disconnect
        '
        Me.btn_Disconnect.ForeColor = System.Drawing.Color.Black
        Me.btn_Disconnect.Location = New System.Drawing.Point(76, 16)
        Me.btn_Disconnect.Name = "btn_Disconnect"
        Me.btn_Disconnect.Size = New System.Drawing.Size(75, 23)
        Me.btn_Disconnect.TabIndex = 13
        Me.btn_Disconnect.Text = "&Disconnect"
        Me.btn_Disconnect.UseVisualStyleBackColor = True
        '
        'btn_connect
        '
        Me.btn_connect.ForeColor = System.Drawing.Color.Black
        Me.btn_connect.Location = New System.Drawing.Point(6, 16)
        Me.btn_connect.Name = "btn_connect"
        Me.btn_connect.Size = New System.Drawing.Size(75, 23)
        Me.btn_connect.TabIndex = 14
        Me.btn_connect.Text = "&Connect"
        Me.btn_connect.UseVisualStyleBackColor = True
        '
        'GrpBox_CurMeas
        '
        Me.GrpBox_CurMeas.BackColor = System.Drawing.Color.Transparent
        Me.GrpBox_CurMeas.Controls.Add(Me.RdoBtn_Capacitance)
        Me.GrpBox_CurMeas.Controls.Add(Me.RdoBtn_Resistance)
        Me.GrpBox_CurMeas.ForeColor = System.Drawing.Color.White
        Me.GrpBox_CurMeas.Location = New System.Drawing.Point(12, 0)
        Me.GrpBox_CurMeas.Name = "GrpBox_CurMeas"
        Me.GrpBox_CurMeas.Size = New System.Drawing.Size(182, 45)
        Me.GrpBox_CurMeas.TabIndex = 12
        Me.GrpBox_CurMeas.TabStop = False
        Me.GrpBox_CurMeas.Text = "Currently Measuring"
        '
        'RdoBtn_Capacitance
        '
        Me.RdoBtn_Capacitance.AutoSize = True
        Me.RdoBtn_Capacitance.Location = New System.Drawing.Point(95, 19)
        Me.RdoBtn_Capacitance.Name = "RdoBtn_Capacitance"
        Me.RdoBtn_Capacitance.Size = New System.Drawing.Size(85, 17)
        Me.RdoBtn_Capacitance.TabIndex = 14
        Me.RdoBtn_Capacitance.TabStop = True
        Me.RdoBtn_Capacitance.Text = "C&apacitance"
        Me.RdoBtn_Capacitance.UseVisualStyleBackColor = True
        '
        'RdoBtn_Resistance
        '
        Me.RdoBtn_Resistance.AutoSize = True
        Me.RdoBtn_Resistance.Location = New System.Drawing.Point(9, 19)
        Me.RdoBtn_Resistance.Name = "RdoBtn_Resistance"
        Me.RdoBtn_Resistance.Size = New System.Drawing.Size(78, 17)
        Me.RdoBtn_Resistance.TabIndex = 13
        Me.RdoBtn_Resistance.TabStop = True
        Me.RdoBtn_Resistance.Text = "&Resistance"
        Me.RdoBtn_Resistance.UseVisualStyleBackColor = True
        '
        'Main_frm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Highlight
        Me.BackgroundImage = Global.RC_Measurement.My.Resources.Resources.cool_background_photos_2_2_s_307x512
        Me.ClientSize = New System.Drawing.Size(492, 308)
        Me.Controls.Add(Me.GrpBox_CurMeas)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.About_btn)
        Me.Controls.Add(Me.Measure_btn)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.HelpButton = True
        Me.Name = "Main_frm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "RC Measurement"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GrpBox_CurMeas.ResumeLayout(False)
        Me.GrpBox_CurMeas.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Measure_btn As System.Windows.Forms.Button
    Friend WithEvents About_btn As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents msrdval_txt As System.Windows.Forms.TextBox
    Friend WithEvents SerialPort As System.IO.Ports.SerialPort
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txt_Unit As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents btn_Disconnect As System.Windows.Forms.Button
    Friend WithEvents btn_connect As System.Windows.Forms.Button
    Friend WithEvents GrpBox_CurMeas As System.Windows.Forms.GroupBox
    Friend WithEvents RdoBtn_Capacitance As System.Windows.Forms.RadioButton
    Friend WithEvents RdoBtn_Resistance As System.Windows.Forms.RadioButton

End Class
