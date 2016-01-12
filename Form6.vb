Public Class Form6

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        SaveFileDialog1.ShowDialog()
        System.IO.File.WriteAllText(SaveFileDialog1.FileName, RichTextBox1.Text)
    End Sub
End Class