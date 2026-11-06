Imports System.Management
Imports System.IO.Ports

Public Class Main_frm
    ' Public declarations
    Private readBuffer As String = String.Empty
    Private WithEvents myComPort As New SerialPort

    Private Sub Main_frm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        ' User will be reminded upon closing the application
        'to disconnect from the device next time (if hasn't), then disconnects automatically
        If myComPort.IsOpen = True Then
            myComPort.DiscardInBuffer()
            myComPort.Close()
            MessageBox.Show("Please make sure to click the disconnect button next time!",
                            "Port Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        'This setting is to ignore exceptions that would normally result from the recieve event thread
        Windows.Forms.Control.CheckForIllegalCrossThreadCalls = False
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'On application load, disable measurement performing,
        'measurement types selection and device disconnect buttons 
        btn_Disconnect.Enabled = False
        RdoBtn_Resistance.Enabled = False
        RdoBtn_Capacitance.Enabled = False
        Measure_btn.Enabled = False

    End Sub

    Private Sub About_btn_Click(sender As Object, e As EventArgs) Handles About_btn.Click
        ' About button
        MessageBox.Show("This program has been designed and coded as requirement for" +
                        vbCr + "     Northumbria University (KDU College/ Penang Campus)" +
                        vbCr + "                   In Electrical & Electronics Engineering" +
                        vbCr + "                                By Hamzah Naser",
                        "About", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub
    Private Sub btn_connect_Click(sender As Object, e As EventArgs) Handles btn_connect.Click
        ' Dictionary to store PnP port names & COM numbers
        Dim PnPPorts As New Dictionary(Of String, String)
        ' Declarations as String along with the device actual name "USB Serial Port"
        Dim PortFullName, PortName, COMNumber, Index_COMPort As String
        Dim NothingFound As Boolean = True
        Dim Device_Name = "USB Serial Port", Device_PortNumber As String

        'This "Try" & "Catch" routine will loop through all avaliable (P)lug a(n)d (P)lay devices
        Try
            ' Retrieves a management object based on a specified query (in this case, PnP entities)
            Dim portSearcher As New ManagementObjectSearcher("\root\CIMV2", "SELECT * FROM Win32_PnPEntity")
            For Each port As System.Management.ManagementObject In portSearcher.Get()
                ' if port's name is empty or null value, then don't process
                If String.IsNullOrEmpty(port("Name")) = False Then

                    ' capitalize the name, then check if the port has "(COM" in it
                    If port("Name").ToString.ToUpper.Contains("(COM") Then
                        'Get the port's full name, make sure it's in string format
                        PortFullName = port("Name").ToString
                        'Determine the starting index of the "(COM" for further process
                        Index_COMPort = PortFullName.IndexOf("(COM")
                        'Obtain value after "(COM", appended with ")"
                        COMNumber = PortFullName.Substring(Index_COMPort + 4)
                        ' Trim the ")" at the end, to get just a number
                        COMNumber = COMNumber.TrimEnd(")")
                        ' Extract the port's name only, from index 0 to {Space} + begin of "(COMXXX)"
                        PortName = PortFullName.Substring(0, (Index_COMPort - 1))
                        'Add Port's name and its corresponding COM number to PnPPorts dictionary
                        PnPPorts.Add(COMNumber, PortName)


                    End If
                End If
            Next
        Catch err As ManagementException
            ' show technical error if for any reason an error occurred
            MessageBox.Show("An error occurred while querying for WMI data: " & err.Message)
        End Try

        ' compare the stored port names witht the device's name
        For Each COMPort_Compare As KeyValuePair(Of String, String) In PnPPorts
            ' if device's name matched, then proceed to connect function
            If Device_Name.Equals(COMPort_Compare.Value) Then
                ' get the COM port number, set Found nothing to false (indicating that port number found)
                ' and call Connect to port function
                Device_PortNumber = COMPort_Compare.Key
                NothingFound = False
                ConnectToPort(Device_PortNumber)

            End If
        Next

        ' if device not found, then inform the user
        If NothingFound.Equals(True) Then
            MessageBox.Show("Device not found!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub
    Sub ConnectToPort(ByVal COMNumber As String)
        ' Setting up the COM port parameters 
        With myComPort
            .PortName = "COM" + COMNumber ' COM port number
            .BaudRate = 4800              ' baud rate speed
            .Parity = Parity.None         ' no parity needed
            .DataBits = 8                 ' length of data bits
            .StopBits = StopBits.One      ' one stop bit
            .ReceivedBytesThreshold = 1   ' recieve event (input buffer) is triggered at 1 byte of threshold
            .NewLine = vbCr               ' CR (to terminate the SerialPort.readLine operation)
            .ReadTimeout = 10000          ' read time-out after 10 seconds when read operation hasn't finished
        End With

        ' Try opening the device's port, if open-able then enable buttons else show an error
        Try
            myComPort.Open()
            If myComPort.IsOpen = True Then
                btn_connect.Enabled = False ' Connect button
                btn_Disconnect.Enabled = True ' Disconnect button
                RdoBtn_Resistance.Enabled = True ' Resisatnce Radio button
                RdoBtn_Capacitance.Enabled = True ' Capacitance Radio button
                Measure_btn.Enabled = True ' Perform Measurement button
            End If
        Catch ex As Exception
            MessageBox.Show("Error encountered while connecting to the device: " + ex.Message)
        End Try


    End Sub

    Private Sub btn_Disconnect_Click(sender As Object, e As EventArgs) Handles btn_Disconnect.Click
        If myComPort.IsOpen = True Then

            myComPort.DiscardInBuffer() ' clear whatever in the recieve buffer
            myComPort.Close()           ' close the device's port
            ' Disable buttons (disconnect, perform measurement and measurement radio buttons)
            ' and enable Connect button
            btn_Disconnect.Enabled = False
            RdoBtn_Resistance.Enabled = False
            RdoBtn_Capacitance.Enabled = False
            Measure_btn.Enabled = False
            btn_connect.Enabled = True
            ' inform user of successfull device disconnection
            MessageBox.Show("Disconnected from the device successfully!", "Port Info", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Else
            ' else, inform user if the device is already disconnected
            MessageBox.Show("The device is already disconnected!", "Port Info", MessageBoxButtons.OK, MessageBoxIcon.Information)

        End If
    End Sub
    Private Sub SerialPort_DataReceived(sender As Object, e As SerialDataReceivedEventArgs) Handles myComPort.DataReceived

        If myComPort.IsOpen = True Then
            Try
                readBuffer = myComPort.ReadLine() ' read the input buffer until vbCr is encountered

                'update or send data recieved to UI thread
                Me.Invoke(New EventHandler(AddressOf UpdateUI))
                ' display an error when read operation is timed-out 
            Catch ex As Exception
                MsgBox("Read " & ex.Message)
            End Try
        End If

    End Sub
    Private Sub UpdateUI()
        ' Pass read data to another locally decalred variable
        Dim DataToProcess As String = String.Empty
        DataToProcess = readBuffer
        'Discard IN and OUT buffer
        myComPort.DiscardInBuffer()
        myComPort.DiscardOutBuffer()
        ' needed after the conversion from string to integer
        Dim DataToInteger As Integer
        ' Array for currents and the stage multiplication factor to adjust the decimal point
        Dim ArrayForCurrents() As Single = {0.00149, 0.00015, 0.00001688, 0.00000181,
                                            1, 0.001, 0.001, 0.000001}
        ' Array to store units
        Dim ArrayForUnits() As String = {Chr(87), "k" + Chr(87), "k" + Chr(87), "M" + Chr(87)}
        ' Array for strings represents the different current stages
        Dim ArrayForStages() As String = {"1stS", "2ndS", "3rdS", "4thS"}
        ' make sure that the recieved data has no gaps or spaces between them
        DataToProcess = DataToProcess.Replace(" ", "")

        ' check if the recieved data is meant for resistance measurement
        If DataToProcess.Contains("1stS") = True OrElse DataToProcess.Contains("2ndS") = True _
            OrElse DataToProcess.Contains("3rdS") = True OrElse DataToProcess.Contains("4thS") = True Then

            For stg As Integer = 0 To 3
                If DataToProcess.Substring(0, 4).CompareTo(ArrayForStages(stg)) = 0 Then

                    ' convert the recieved sampled voltage into integer, then perform necessary calcs. then display resistance value
                    DataToInteger = Convert.ToInt32(DataToProcess.Substring(DataToProcess.IndexOf(ArrayForStages(stg)) + 4))
                    msrdval_txt.Text = Convert.ToString(Math.Round((DataToInteger * 0.003711) / ArrayForCurrents(stg) * ArrayForCurrents(stg + 4), 3))
                    ' set font type to display "Ω" symbol, and display the corresponding unit with it
                    txt_Unit.Font = New System.Drawing.Font("Symbol", 20)
                    txt_Unit.Text = ArrayForUnits(stg)
                End If
            Next

            ' else check if the recieved data is meant for capacitance measurement
        ElseIf DataToProcess.Contains("F") = True AndAlso DataToProcess.Contains("R") = True Then
            ' Locate the index from received data for overflows and the index for Timer1's register value
            Dim start_index As Integer = DataToProcess.IndexOf("F") + 1
            Dim O_F As Single = Convert.ToUInt16(DataToProcess.Substring(start_index, DataToProcess.IndexOf("R") - start_index))
            Dim REG As Single = Convert.ToUInt16(DataToProcess.Substring(DataToProcess.IndexOf("R") + 1))

            ' Calibration purposes, if no capcitor is present the mcriocntroller will still read timer value
            If REG < 6 Then
                msrdval_txt.Text = "OL"
                txt_Unit.Text = ""
            Else
                    ' pass extracted values to find the capacitance value and its corresponding unit in farads
                    Capacitance_with_prefix(O_F, REG - 5)

                End If

        ElseIf DataToProcess.Equals("OutofRange!") = True Then
                ' display over-load "OL"
                msrdval_txt.Text = "OL"
                txt_Unit.Text = ""
        Else
                ' display "Error!"
                msrdval_txt.Text = "Error!"
                txt_Unit.Text = ""
        End If


    End Sub

    Private Sub Measure_btn_Click(sender As Object, e As EventArgs) Handles Measure_btn.Click
        ' check if port is open before proceeding to any other activity
        If myComPort.IsOpen = True Then
            ' if measurement type radio button capacitance, then send to device so, appended by vbCr
            If RdoBtn_Capacitance.Checked = True Then
                myComPort.WriteLine("Capacitance" + vbCr)
                ' else if measurement type radio button resisatnce, then send to device so, appended by vbCr
            ElseIf RdoBtn_Resistance.Checked = True Then
                myComPort.WriteLine("Resistance" + vbCr)

            Else
                ' inform user if measurement type is not selected when perform measurement is clicked
                MessageBox.Show("Please select a measurement type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Else
            ' inform user if hasn't connected to the device for some reason
            MessageBox.Show("Please make sure that the device is in plugged," +
                            vbCr + "you may need to restart the application afterwards!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
    Sub Capacitance_with_prefix(O_F As UShort, REG_Value As UShort)
        ' Capacitance units
        Dim prefixes As String() = {"pF", "nF", "μF", "mF", String.Empty}
        ' Charging time formula
        Dim Charge_Time As Single = ((O_F * 0.032768) + (REG_Value * 0.0000005)) / (1.09861 * 30000)
            Charge_Time = Charge_Time * 2


            ' get scientific exponent, 10^(exponent), ...
            Dim scientific_exponent As Integer = CInt(Math.Floor(Math.Log(Charge_Time, 10))) - 1

            ' Adjust the secientific component to match the avaliable prefixes
            Select Case scientific_exponent
                Case -1, -2, -4
                    scientific_exponent = -3
                Case -5, -7
                    scientific_exponent = -6
                Case -8, -10
                    scientific_exponent = -9
            End Select

            ' scale number to exponent found
            Charge_Time = Charge_Time * Math.Pow(10, -scientific_exponent)
            ' find number of digits to the left of the decimal
            Dim digits_left As Integer = CInt(Math.Floor(Math.Log(Charge_Time, 10))) + 1
            ' adjust decimals to display 5 digits overall
            Dim decimals As Integer = Math.Min(5 - digits_left, 15)
            ' format for the digits after the decimal
            Dim format_digits As New String("0"c, decimals)
            ' find index for prefix. every 3 of sci is a new index
            Dim index As Integer = scientific_exponent / 3 + 4
            If index >= 0 AndAlso index < prefixes.Length Then
                ' display capacitance value with the proper formating
                msrdval_txt.Text = String.Format((Convert.ToString("{0}{1:0.") & format_digits) + "}", String.Empty, Math.Round(Charge_Time, decimals))
                ' set font type to display the prefixes correctly
                txt_Unit.Font = New System.Drawing.Font("Times New Roman", 20)
                txt_Unit.Text = prefixes(index)
            Else
                msrdval_txt.Text = "Error!"
            End If


    End Sub
End Class
