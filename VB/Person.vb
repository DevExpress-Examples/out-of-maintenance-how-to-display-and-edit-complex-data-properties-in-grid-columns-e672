Imports Microsoft.VisualBasic
Imports System

Namespace TypeConverterTest
	Public Class Person
		Private name_ As String
		Private address_ As Address

		Public Sub New(ByVal name As String, ByVal address As Address)
			name_ = name
			address_ = address
		End Sub

		Public Property Name() As String
			Get
				Return name_
			End Get
			Set(ByVal value As String)
				name_ = value
			End Set
		End Property

		Public Property Address() As Address
			Get
				Return address_
			End Get
			Set(ByVal value As Address)
				address_ = value
			End Set
		End Property
	End Class
End Namespace
