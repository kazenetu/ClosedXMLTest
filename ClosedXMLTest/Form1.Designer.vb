﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
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

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Load = New System.Windows.Forms.Button()
        Me.LoadNoID = New System.Windows.Forms.Button()
        Me.SaveLoad = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(12, 92)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowTemplate.Height = 21
        Me.DataGridView1.Size = New System.Drawing.Size(547, 272)
        Me.DataGridView1.TabIndex = 0
        '
        'Load
        '
        Me.Load.Location = New System.Drawing.Point(24, 27)
        Me.Load.Name = "Load"
        Me.Load.Size = New System.Drawing.Size(151, 23)
        Me.Load.TabIndex = 1
        Me.Load.Text = "読み込み(リソースID設定)"
        Me.Load.UseVisualStyleBackColor = True
        '
        'LoadNoID
        '
        Me.LoadNoID.Location = New System.Drawing.Point(24, 57)
        Me.LoadNoID.Name = "LoadNoID"
        Me.LoadNoID.Size = New System.Drawing.Size(151, 23)
        Me.LoadNoID.TabIndex = 2
        Me.LoadNoID.Text = "読み込み"
        Me.LoadNoID.UseVisualStyleBackColor = True
        '
        'SaveLoad
        '
        Me.SaveLoad.Location = New System.Drawing.Point(404, 27)
        Me.SaveLoad.Name = "SaveLoad"
        Me.SaveLoad.Size = New System.Drawing.Size(133, 23)
        Me.SaveLoad.TabIndex = 3
        Me.SaveLoad.Text = "書き込みと読み込み"
        Me.SaveLoad.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(604, 397)
        Me.Controls.Add(Me.SaveLoad)
        Me.Controls.Add(Me.LoadNoID)
        Me.Controls.Add(Me.Load)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Load As Button
    Friend WithEvents LoadNoID As Button
    Friend WithEvents SaveLoad As Button
End Class
