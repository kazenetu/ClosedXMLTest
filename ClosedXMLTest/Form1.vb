Imports ClosedXML.Excel

Public Class Form1

    ''' <summary>
    ''' リソースID設定付ロード
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Load_Click(sender As Object, e As EventArgs) Handles Load.Click

        Dim excelData As DataTable = Me.getExcel("dummy.xlsx", "target")

        ' DataGridViewに設定
        Me.DataGridView1.DataSource = Me.setResouceId(excelData)

    End Sub

    ''' <summary>
    ''' リソースID設定なしロード
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub LoadNoID_Click(sender As Object, e As EventArgs) Handles LoadNoID.Click
        ' DataGridViewに設定
        Me.DataGridView1.DataSource = Me.getExcel("dummy.xlsx", "target")
    End Sub

    ''' <summary>
    ''' 書き込みと読み込み
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub SaveLoad_Click(sender As Object, e As EventArgs) Handles SaveLoad.Click
        Dim excelData As DataTable = Me.setResouceId(Me.getExcel("dummy.xlsx", "target"))

        ' リソースIDを設定したxlsxファイルを別名保存
        Me.saveExcel("dummy.xlsx", "target", excelData, "dummycopy.xlsx", New List(Of String)() From {"リソースID"})

        ' 別名保存したファイルをDataGridViewに設定
        Me.DataGridView1.DataSource = Me.getExcel("dummycopy.xlsx", "target")

    End Sub

    ''' <summary>
    ''' リソースIDの設定
    ''' </summary>
    ''' <param name="src">対象データ</param>
    ''' <returns>リソースID付データ</returns>
    Private Function setResouceId(ByVal src As DataTable) As DataTable
        Dim excelData As DataTable = src.Copy()

        Dim projUtil As New ProjectsUtility()

        ' プロジェクトID+画面名ごとのプロパティのカウント
        Dim properties As New Dictionary(Of String, Integer)

        For Each row As DataRow In excelData.Rows

            ' 画面IDを取得
            Dim screenId As String = projUtil.GetScreenId(row("プロジェクト"), row("画面名"))
            If String.IsNullOrEmpty(screenId) Then
                Continue For
            End If

            ' 画面ごとのプロパティ最終番号がない場合は追加
            If Not properties.ContainsKey(screenId) Then
                properties.Add(screenId, 0)
            End If

            ' コントロールIDを取得
            Dim propertyId As String = String.Format("P{0:0000}", properties(screenId))
            properties(screenId) = properties(screenId) + 1

            ' リソースIDの設定
            row("リソースID") = screenId & propertyId
        Next

        Return excelData
    End Function

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

    Public Function saveExcel(ByVal fileName As String, ByVal sheetName As String, ByVal src As DataTable, ByVal newFileName As String, ByVal targetColumns As List(Of String)) As Boolean
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
                End If
            Next

            ' データ行を取得
            For Each row As IXLRow In target.Rows(2, target.Rows().LongCount)
                Dim srcIndex As Integer = row.RowNumber - 2

                Dim srcRow As DataRow = src(srcIndex)
                For Each columnName As String In targetColumns
                    row.Cell(columnIndexes(columnName)).Value = srcRow(columnName)
                Next
            Next

            book.SaveAs(newFileName)
        End Using

        Return True
    End Function
End Class
