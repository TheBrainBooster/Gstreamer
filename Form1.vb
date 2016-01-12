Imports System.IO

Public Class Form1
    Public Shared WorkingDirectory As String = System.AppDomain.CurrentDomain.BaseDirectory()
    Public Shared mdown(2) As Integer
    Public Shared mup(2) As Integer
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        ' MessageBox.Show("WorkingDirectory=" & WorkingDirectory, "azz", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
    End Sub

    Function adb(ByVal Arguments As String) As String
        Try

            Dim My_Process As New Process()
            Dim My_Process_Info As New ProcessStartInfo()

            My_Process_Info.FileName = "cmd.exe" ' Process filename
            My_Process_Info.Arguments = Arguments ' Process arguments
            My_Process_Info.WorkingDirectory = WorkingDirectory 'this directory can be different in your case.
            My_Process_Info.CreateNoWindow = True  ' Show or hide the process Window
            My_Process_Info.UseShellExecute = False ' Don't use system shell to execute the process
            My_Process_Info.RedirectStandardOutput = True  '  Redirect (1) Output
            My_Process_Info.RedirectStandardError = True  ' Redirect non (1) Output

            My_Process.EnableRaisingEvents = True ' Raise events
            My_Process.StartInfo = My_Process_Info
            My_Process.Start() ' Run the process NOW

            Dim Process_ErrorOutput As String = My_Process.StandardOutput.ReadToEnd() ' Stores the Error Output (If any)
            Dim Process_StandardOutput As String = My_Process.StandardOutput.ReadToEnd() ' Stores the Standard Output (If any)
            My_Process.WaitForExit()
            ' Return output by priority
            If Process_ErrorOutput IsNot Nothing Then Return Process_ErrorOutput ' Returns the ErrorOutput (if any)
            If Process_StandardOutput IsNot Nothing Then Return Process_StandardOutput ' Returns the StandardOutput (if any)

        Catch ex As Exception
            Return ex.Message
        End Try

        Return "OK"

    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        RadioButton1.Checked = True
        RadioButton1.Text = "busy"
        'Usage:

        'Get the list of connected devices.

        Dim listit As String
        Dim takescreen As String
        Dim pullit As String
        Dim clean As String
        If System.IO.File.Exists(WorkingDirectory & "screenshot.png") = True Then
            ' On Error Resume Next
            System.IO.File.Delete(WorkingDirectory & "screenshot.png")
            '    MessageBox.Show("File Deleted")

        End If
        listit = (adb("/c adb devices"))
        RichTextBox1.AppendText(listit)
        'Connect your phone wirelessly using wifi (required phone I.P)
        takescreen = (adb("/c adb shell screencap -p /sdcard/screenshot.png"))
        RichTextBox1.AppendText(takescreen)
        'Get the list of connected devices.
        pullit = (adb("/c adb pull /sdcard/screenshot.png"))
        RichTextBox1.AppendText(pullit)

        'Put your phone on airplane mode. 
        clean = (adb("/c adb shell rm /sdcard/screenshot.png"))
        RichTextBox1.AppendText(clean)
        Dim xx As Image
        If System.IO.File.Exists(WorkingDirectory & "screenshot.png") = True Then
            On Error Resume Next
            Using str As IO.Stream = File.OpenRead(WorkingDirectory & "screenshot.png")
                xx = Image.FromStream(str)
            End Using
            PictureBox1.Image = xx
        End If
        ' PictureBox1.Image = System.Drawing.Image.FromFile("C:\Users\root\Desktop\Amaster001\adbGUI\tools\screenshot.png")
        RadioButton1.Checked = False
        RadioButton1.Text = "done"

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Me.Button1.PerformClick()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If CheckBox1.Checked = True Then
            Timer1.Enabled = False
            CheckBox1.Checked = False
        Else
            Timer1.Enabled = True
            CheckBox1.Checked = True
        End If
    End Sub

    Private Sub RichTextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox1.TextChanged
        Dim lung As Integer
        lung = RichTextBox1.TextLength
        RichTextBox1.ScrollToCaret()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim results As String
        results = (adb("/c adb backup -all -apk -shared -f MyEmergencyBackup.ab"))
        RichTextBox1.AppendText(results)
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim results As String
        results = (adb("/c adb pull /sdcard/DCIM"))
        RichTextBox1.AppendText(results)
        results = (adb("/c adb pull /sdcard0/DCIM"))
        RichTextBox1.AppendText(results)
        results = (adb("/c adb pull /sdcard1/DCIM"))
        RichTextBox1.AppendText(results)
        results = (adb("/c adb pull /sdcard2/DCIM"))
        RichTextBox1.AppendText(results)
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim results As String
        results = (adb("/c adb kill-server"))
        RichTextBox1.AppendText(results)
        results = (adb("/c adb start-server"))
        RichTextBox1.AppendText(results)
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Dim results As String
        results = (adb("/c adb shell logcat -d"))
        Form2.Show()
        Form2.RichTextBox1.AppendText(results)
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Dim results As String
        results = (adb("/c adb reboot"))
        RichTextBox1.AppendText(results)
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Dim results As String
        results = (adb("/c adb reboot-recoery"))
        RichTextBox1.AppendText(results)
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Dim results As String
        Dim openfilepath As String
        openfilepath = WorkingDirectory & "build.prop"
        results = (adb("/c adb pull /system/build.prop"))
        RichTextBox1.AppendText(results)
        Form4.Show()
        Form4.RichTextBox1.Text = System.IO.File.ReadAllText(openfilepath)
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Dim results As String
        Dim openfilepath As String
        openfilepath = WorkingDirectory & "build.prop"
        results = (adb("/c adb shell getprop"))
        Form5.RichTextBox1.AppendText(results)
        Form5.Show()
        ' Form3.RichTextBox1.Text = System.IO.File.ReadAllText(openfilepath)
    End Sub

    Private Sub PictureBox1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
        Dim LocalMousePosition As Point
        Dim X As Integer
        Dim Y As Integer
        Dim myX As Integer = PictureBox1.Width  '210
        Dim myY As Integer =  PictureBox1.Height '370
        LocalMousePosition = PictureBox1.PointToClient(Cursor.Position)
        Y = LocalMousePosition.Y()
        X = LocalMousePosition.X()
        Dim urX As Integer = Val(TextBox2.Text)
        Dim urY As Integer = Val(TextBox3.Text)
        Dim results As String
        Dim scaleX As Integer = (urX / myX)
        Dim scaley As Integer = (urY / myY)
        X = (X * scaleX) + 40 ' ATTENZIONE AGGINTO OFFSET FISSO  ! ! !
        Y = (Y * scaley) + 140 ' ATTENZIONE AGGINTO OFFSET FISSO ! ! !
        '     MessageBox.Show("send click to: X=" & X & " Y=" & Y, "Send click", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
        ' mdown(0) = X
        ' mdown(1) = Y
        results = (adb("/c adb shell sendevent /dev/input/event3 3 24 128")) 'ABS_Pressure
        RichTextBox1.AppendText(results)
        results = (adb("/c adb shell sendevent /dev/input/event3 1 330 1"))  'BTN_touch
        RichTextBox1.AppendText(results)
        'touch routine
        results = (adb("/c adb shell sendevent /dev/input/event3 3 48 128"))  'Mt_touch_mj
        RichTextBox1.AppendText(results)
        results = (adb("/c adb shell sendevent /dev/input/event3 3 53 " & X)) 'x 
        RichTextBox1.AppendText(results)
        results = (adb("/c adb shell sendevent /dev/input/event3 3 54 " & Y)) 'y
        RichTextBox1.AppendText(results)
        results = (adb("/c adb shell sendevent /dev/input/event3 3 57 0"))    'abs_trk_ID
        RichTextBox1.AppendText(results)
        results = (adb("/c adb shell sendevent /dev/input/event3 0 2 0"))     'Syn_mt_rep
        RichTextBox1.AppendText(results)
        results = (adb("/c adb shell sendevent /dev/input/event3 0 0 0"))     ' Syn_rep
        RichTextBox1.AppendText(results)
        results = (adb("/c adb shell sendevent /dev/input/event3 3 24 0")) 'ABS_Pressure 0
        RichTextBox1.AppendText(results)
        results = (adb("/c adb shell sendevent /dev/input/event3 1 330 0"))  'UP
        RichTextBox1.AppendText(results)
        results = (adb("/c adb shell sendevent /dev/input/event3 3 48 0"))  'Mt_touch_mj 0
        RichTextBox1.AppendText(results)
        results = (adb("/c adb shell sendevent /dev/input/event3 3 53 " & X)) 'x 
        RichTextBox1.AppendText(results)
        results = (adb("/c adb shell sendevent /dev/input/event3 3 54 " & Y)) 'y
        RichTextBox1.AppendText(results)
        results = (adb("/c adb shell sendevent /dev/input/event3 3 57 0"))    'abs_trk_ID
        RichTextBox1.AppendText(results)
        results = (adb("/c adb shell sendevent /dev/input/event3 0 2 0"))     'Syn_mt_rep
        RichTextBox1.AppendText(results)
        results = (adb("/c adb shell sendevent /dev/input/event3 0 0 0"))     ' Syn_rep
        RichTextBox1.AppendText(results)
        results = (adb("/c adb shell input tap " & X & " " & Y))     ' Syn_rep
        RichTextBox1.AppendText(results)
    End Sub

    Private Sub PictureBox1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseDown
        Dim LocalMousePosition As Point
        Dim X As Integer
        Dim Y As Integer
        Dim myX As Integer = PictureBox1.Width  '210
        Dim myY As Integer = PictureBox1.Height '370
        LocalMousePosition = PictureBox1.PointToClient(Cursor.Position)
        Y = LocalMousePosition.Y()
        X = LocalMousePosition.X()
        Dim urX As Integer = Val(TextBox2.Text)
        Dim urY As Integer = Val(TextBox3.Text)
        'Dim results As String
        Dim scaleX As Integer = (urX / myX)
        Dim scaley As Integer = (urY / myY)
        X = (X * scaleX) + 40 ' ATTENZIONE AGGINTO OFFSET FISSO  ! ! !
        Y = (Y * scaley) + 140 ' ATTENZIONE AGGINTO OFFSET FISSO ! ! !
        mdown(0) = X
        mdown(1) = Y
    End Sub

    

    Private Sub PictureBox1_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox1.MouseHover
        Dim LocalMousePosition As Point
        LocalMousePosition = PictureBox1.PointToClient(Cursor.Position)
        Label1.Text = "X=" & LocalMousePosition.X & "," & "Y= " & LocalMousePosition.Y
    End Sub

    Private Sub PictureBox1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseMove
        Dim LocalMousePosition As Point
        LocalMousePosition = PictureBox1.PointToClient(Cursor.Position)
        Label1.Text = "X=" & LocalMousePosition.X & "," & "Y= " & LocalMousePosition.Y
    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        Dim results As String
        results = (adb("/c adb shell dumpsys"))
        Form6.RichTextBox1.AppendText(results)
        Form6.Show()
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        RichTextBox1.Text = ""
        Dim results As String
        results = (adb("/c adb shell dumpsys window "))
        RichTextBox1.AppendText(results)
        Dim findCount As Integer = 0
        Dim textToFind As String
        textToFind = " SurfaceWidth: "
        With Me.RichTextBox1
            .SelectionStart = 0
            .SelectionLength = 0
            Dim findFromIndex As Integer = 0
            While findCount <> -1
                findCount = .Find(textToFind, findFromIndex, RichTextBoxFinds.MatchCase)
                If findCount <> -1 Then
                    .SelectionFont = New Font("Verdana", 12, FontStyle.Bold)
                End If
                findFromIndex = findCount + textToFind.Length

            End While

            ''''
            textToFind = "  SurfaceHeight: "
            findCount = 0
            .SelectionStart = 0
            .SelectionLength = 0
            findFromIndex = 0
            While findCount <> -1
                findCount = .Find(textToFind, findFromIndex, RichTextBoxFinds.MatchCase)
                If findCount <> -1 Then
                    .SelectionFont = New Font("Verdana", 12, FontStyle.Bold)
                End If
                findFromIndex = findCount + textToFind.Length

            End While
            textToFind = "mUnrestrictedScreen"
            findCount = 0
            .SelectionStart = 0
            .SelectionLength = 0
            findFromIndex = 0
            While findCount <> -1
                findCount = .Find(textToFind, findFromIndex, RichTextBoxFinds.MatchCase)
                If findCount <> -1 Then
                    .SelectionFont = New Font("Verdana", 12, FontStyle.Bold)
                End If
                findFromIndex = findCount + textToFind.Length

            End While
        End With
    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        Dim results As String
        results = (adb("/c adb shell input keyevent 3 "))
        RichTextBox1.AppendText(results)
    End Sub

    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        Dim results As String
        results = (adb("/c adb shell input keyevent 4 "))
        RichTextBox1.AppendText(results)
    End Sub

    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click
        Dim results As String
        results = (adb("/c adb shell input keyevent 82 "))
        RichTextBox1.AppendText(results)
    End Sub

    Private Sub Button16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button16.Click
        Dim results As String
        results = (adb("/c adb shell input text " & TextBox1.Text))
        RichTextBox1.AppendText(results)
        RichTextBox1.Text = ""
    End Sub

    Private Sub Button17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button17.Click
        Dim results As String
        results = (adb("/c adb.exe " & TextBox4.Text))
        RichTextBox1.AppendText(results)

    End Sub

    Private Sub Button18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button18.Click
        Dim results As String
        results = (adb("/c adb shell reboot -p"))
        RichTextBox1.AppendText(results)
    End Sub

    Private Sub Button19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button19.Click
        Dim results As String
        results = (adb("/c adb shell dumpsys battery"))
        MessageBox.Show(results, "Battery status", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
    End Sub

    Private Sub Button20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button20.Click
        Dim results As String
        results = (adb("/c adb devices"))
        Dim ReplaceString = results
        Dim NewString As String

        ' NewString = "This is Another String"
        NewString = ReplaceString.Replace("List of devices attached", "")
        NewString = NewString.Replace("device", "")
        If Len(Trim(NewString)) < 5 Then
            MessageBox.Show("Please try to reconnect your phone", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            TabControl1.TabPages(0).Text = NewString
            RichTextBox2.Text = "Phone serial number: " & NewString
            '---------------------------------------------------------
            results = ""
            results = (adb("/c adb shell dumpsys battery "))
            TabControl1.TabPages(1).Text = "Battery info"
            RichTextBox3.Text = results
            Dim SplitArr(10) As String
            Dim i As Integer

            SplitArr = results.Split(vbLf)

            For i = 0 To SplitArr.Length - 1
                RichTextBox3.Text = RichTextBox3.Text & SplitArr(i) & vbCrLf
            Next
            '--------------------------------------------------------
            results = ""
            results = (adb("/c adb shell getevent -p"))
            RichTextBox4.Text = results
            '--------------dumpsyses------------------------------
            results = ""

            results = (adb("/c adb shell dumpsys -l"))


            results = results.Replace("Currently running services:   ", " ")
            Dim arr As String() = results.Split(New Char() {"   "})
            Dim iL As Integer
            For iL = 1 To arr.Length - 1
                Trim(arr(iL))
                If arr(iL).Length > 0 Then ListBox1.Items.Add(arr(iL))
            Next iL
            Button1.PerformClick()
            results = ""

            results = (adb("/c adb shell dumpsys connectivity"))
            RichTextBox6.Text = results

        End If

    End Sub

    Private Sub PictureBox1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseUp
        Dim LocalMousePosition As Point
        Dim X As Integer
        Dim Y As Integer
        Dim myX As Integer = PictureBox1.Width  '210
        Dim myY As Integer = PictureBox1.Height '370
        LocalMousePosition = PictureBox1.PointToClient(Cursor.Position)
        Y = LocalMousePosition.Y()
        X = LocalMousePosition.X()
        Dim urX As Integer = Val(TextBox2.Text)
        Dim urY As Integer = Val(TextBox3.Text)
        'Dim results As String
        Dim scaleX As Integer = (urX / myX)
        Dim scaley As Integer = (urY / myY)
        X = (X * scaleX) + 40 ' ATTENZIONE AGGINTO OFFSET FISSO  ! ! !
        Y = (Y * scaley) + 140 ' ATTENZIONE AGGINTO OFFSET FISSO ! ! !
        '  MessageBox.Show("mouse up to: X=" & X & " Y=" & Y, "Send swipe", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
        mup(0) = X
        mup(1) = Y
        If mup(0) <> mdown(0) And mup(1) <> mdown(1) Then
            Dim results As String
            results = (adb("/c adb shell input swipe " & mdown(0) & " " & mdown(1) & " " & mup(0) & " " & mup(1)))
            RichTextBox1.Text = results
        End If
    End Sub
    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        If Len(ListBox1.SelectedItem.ToString) > 2 Then
            Dim results As String

            results = (adb("/c adb shell dumpsys " & ListBox1.SelectedItem.ToString))
            RichTextBox5.Text = results
        End If
    End Sub

    Private Sub Button21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button21.Click
        'adbmount everything mount -o rw,remount /system
        Dim results As String
        results = (adb("/c adb shell mount -o rw,remount /system"))
        RichTextBox1.AppendText(results)
        'enable adb stuff echo "persist.service.adb.enable=1" >> default.prop 
        results = (adb("/c adb shell echo 'persist.service.debuggable=1' >> default.prop"))
        RichTextBox1.AppendText(results)
        'echo "persist.service.debuggable=1" >> default.prop
        results = (adb("/c adb shell echo 'persist.sys.usb.config=mtp,adb' >> default.prop"))
        RichTextBox1.AppendText(results)
        'echo "persist.sys.usb.config=mtp,adb" >> default.prop
        results = (adb("/c adb shell echo 'persist.service.adb.enable=1' >> /system/build.prop"))
        RichTextBox1.AppendText(results)
        'echo "persist.service.adb.enable=1" >> /system/build.prop 
        results = (adb("/c adb shell echo 'persist.service.debuggable=1' >> /system/build.prop"))
        RichTextBox1.AppendText(results)
        'echo "persist.service.debuggable=1" >> /system/build.prop
        results = (adb("/c adb shell echo 'persist.sys.usb.config=mtp,adb' >> /system/build.prop"))
        RichTextBox1.AppendText(results)
        'echo "persist.sys.usb.config=mtp,adb" >> /system/build.prop
        'injectrsakey
        results = (adb("/c adb shell touch /data/misc/adb/adb_keys"))
        RichTextBox1.AppendText(results)
        results = (adb("/c adb shell cat ./adbkey.pub >> /data/misc/adb/adb_keys"))
        RichTextBox1.AppendText(results)
        'C:\Users\root\.android  cat /data/.android/adbkey.pub >> /data/misc/adb/adb_keys
        'echo "" >> /data/misc/adb/adb_keys # Add a blank line at the end of the file
        'reboot
    End Sub

    Private Sub MakeAFullStandardBackupToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MakeAFullStandardBackupToolStripMenuItem.Click
        Button3.PerformClick()
    End Sub

    Private Sub TestPhoneConnectionToPcToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TestPhoneConnectionToPcToolStripMenuItem.Click
        Button20.PerformClick()
    End Sub

    Private Sub Button22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button22.Click
        Dim results As String
        results = (adb("/c adb logcat -d -v thread *:E"))
        Form7.Show()
        Form7.RichTextBox1.AppendText(results)
    End Sub

    Private Sub InfoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InfoToolStripMenuItem.Click
        Form8.Show()
    End Sub
End Class