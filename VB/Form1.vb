Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data

Namespace TypeConverterTest
	''' <summary>
	''' Summary description for Form1.
	''' </summary>
	Public Class Form1
		Inherits System.Windows.Forms.Form
		Private gridControl1 As DevExpress.XtraGrid.GridControl
		Private gridView1 As DevExpress.XtraGrid.Views.Grid.GridView
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.Container = Nothing

		Public Sub New()
			'
			' Required for Windows Form Designer support
			'
			InitializeComponent()

			Dim persons As New Persons()

			Dim address As New Address("Obere Str. 57", "Berlin", "12209")
			persons.Add(New Person("Maria Anderss", address))
			address = New Address("120 Hanover Sq.", "London", "WA1 1DP")
			persons.Add(New Person("Thomas Hardy", address))
			address = New Address("12, rue des Bouchers", "Marseille", "13008")
			persons.Add(New Person("Laurence Lebihan", address))

			gridControl1.DataSource = persons
			gridControl1.MainView.PopulateColumns()

			' implentent ConvertFrom, if you wish to edit Address
			Dim gridView As DevExpress.XtraGrid.Views.Grid.GridView = TryCast(gridControl1.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
			Dim c As DevExpress.XtraGrid.Columns.GridColumn = gridView.Columns("Address")
			c.OptionsColumn.ReadOnly = True
		End Sub

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		Protected Overrides Overloads Sub Dispose(ByVal disposing As Boolean)
			If disposing Then
				If components IsNot Nothing Then
					components.Dispose()
				End If
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"
		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.gridControl1 = New DevExpress.XtraGrid.GridControl()
			Me.gridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
			CType(Me.gridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.gridView1, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' gridControl1
			' 
			Me.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.gridControl1.Location = New System.Drawing.Point(0, 0)
			Me.gridControl1.MainView = Me.gridView1
			Me.gridControl1.Name = "gridControl1"
			Me.gridControl1.Size = New System.Drawing.Size(671, 317)
			Me.gridControl1.TabIndex = 0
			Me.gridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() { Me.gridView1})
			' 
			' gridView1
			' 
			Me.gridView1.GridControl = Me.gridControl1
			Me.gridView1.Name = "gridView1"
			' 
			' Form1
			' 
			Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
			Me.ClientSize = New System.Drawing.Size(671, 317)
			Me.Controls.Add(Me.gridControl1)
			Me.Name = "Form1"
			Me.Text = "How to display and edit complex data properties in grid columns"
			CType(Me.gridControl1, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.gridView1, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)

		End Sub
		#End Region

		''' <summary>
		''' The main entry point for the application.
		''' </summary>
		<STAThread> _
		Shared Sub Main()
			Application.Run(New Form1())
		End Sub
	End Class
End Namespace
