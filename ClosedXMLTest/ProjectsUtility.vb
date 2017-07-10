Imports System.IO

Public Class ProjectsUtility
    Private Const PROJECTS_PATH As String = "projects/"

    Private projectScreens As New Dictionary(Of String, Dictionary(Of String, String))

    Public Sub New()
        Dim projectDirectory As New System.IO.DirectoryInfo(PROJECTS_PATH)
        For Each file As FileInfo In projectDirectory.GetFiles("*.txt")
            Dim projectName As String = file.Name.Replace(".txt", String.Empty)
            projectScreens.Add(projectName, New Dictionary(Of String, String))

            Using sr As StreamReader = file.OpenText
                While sr.EndOfStream = False
                    Dim s = sr.ReadLine().Split(",")
                    projectScreens(projectName).Add(s(0), s(1))
                End While
            End Using
        Next
    End Sub

    Public Function GetScreenId(ByVal projectName As String, ByVal screenName As String) As String
        If Not projectScreens.ContainsKey(projectName) Then
            Return String.Empty
        End If
        If Not projectScreens(projectName).ContainsKey(screenName) Then
            Return String.Empty
        End If

        Return projectScreens(projectName)(screenName)
    End Function
End Class
