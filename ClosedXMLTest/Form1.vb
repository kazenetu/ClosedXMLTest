Imports ClosedXML.Excel

Public Class Form1
    Private Sub Load_Click(sender As Object, e As EventArgs) Handles Load.Click

        Dim columnIndexes As New Dictionary(Of String, Integer)
        Dim rows As New List(Of Dictionary(Of String, String))

        ' xlsx読み込み
        Using book As New XLWorkbook("dummy.xlsx")

            ' targetシート取得
            Dim target As IXLWorksheet = book.Worksheet("target")

            ' カラム名の取得
            For Each column As IXLColumn In target.Columns()
                Dim columnName As String = target.Cell(1, column.ColumnNumber).GetString()
                If Not String.IsNullOrEmpty(columnName) Then
                    columnIndexes.Add(columnName, column.ColumnNumber)
                End If
            Next

            ' データ行を取得
            For Each row As IXLRow In target.Rows(2, target.Rows().LongCount)
                Dim rowData As New Dictionary(Of String, String)()

                ' セルデータを収集
                For Each columnName As String In columnIndexes.Keys
                    Dim value As String = row.Cell(columnIndexes(columnName)).GetString()
                    rowData(columnName) = value
                Next

                rows.Add(rowData)
            Next

        End Using

        ' Datatableに格納
        Dim result As New DataTable()
        ' 列の追加
        For Each columnName As String In columnIndexes.Keys
            result.Columns.Add(columnName)
        Next

        ' 行の追加
        For Each row As Dictionary(Of String, String) In rows
            Dim newRow As DataRow = result.NewRow

            For Each columnName As String In row.Keys
                Dim value As String = row(columnName)
                newRow(columnName) = value
            Next

            result.Rows.Add(newRow)
        Next

        ' DataGridViewに設定
        Me.DataGridView1.DataSource = result

    End Sub
End Class
