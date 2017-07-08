Imports ClosedXML.Excel

Public Class Form1
    Private Sub Load_Click(sender As Object, e As EventArgs) Handles Load.Click

        ' DataGridViewに設定
        Me.DataGridView1.DataSource = Me.getExcel("dummy.xlsx", "target")

    End Sub

    ''' <summary>
    ''' Excelファイルのシートを読み込む
    ''' </summary>
    ''' <param name="fileName">Excelファイル名</param>
    ''' <param name="sheetName">シート名</param>
    ''' <returns>読み取ったシートのデータ</returns>
    Private Function getExcel(ByVal fileName As String, ByVal sheetName As String) As DataTable

        ' 戻り値のDatatable
        Dim result As New DataTable()

        ' xlsx読み込み
        Using book As New XLWorkbook(fileName)

            ' targetシート取得
            Dim target As IXLWorksheet = book.Worksheet(sheetName)

            ' カラム名と列indexの情報
            Dim columnIndexes As New Dictionary(Of String, Integer)

            ' カラム名の取得
            For Each column As IXLColumn In target.Columns()
                Dim columnName As String = target.Cell(1, column.ColumnNumber).GetString()
                If Not String.IsNullOrEmpty(columnName) Then
                    ' カラム名と列indexの情報を格納
                    columnIndexes.Add(columnName, column.ColumnNumber)

                    ' DataTableのカラムを追加
                    result.Columns.Add(columnName, GetType(String))
                End If
            Next

            ' データ行を取得
            For Each row As IXLRow In target.Rows(2, target.Rows().LongCount)
                Dim newRow As DataRow = result.NewRow()

                ' セルデータを収集
                For Each columnName As String In columnIndexes.Keys
                    Dim value As String = row.Cell(columnIndexes(columnName)).GetString()
                    newRow(columnName) = value
                Next

                result.Rows.Add(newRow)
            Next

        End Using

        Return result
    End Function
End Class
