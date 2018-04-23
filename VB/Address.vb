Imports Microsoft.VisualBasic
Imports System

Namespace TypeConverterTest
	Public Class Address
		Implements IComparable
		Public Shared Widening Operator CType(ByVal addressString As String) As Address
			Return Find(addressString)
		End Operator
		Public Sub New(ByVal street As String, ByVal city As String, ByVal zip As String)
			Me.Street = street
			Me.City = city
			Me.Zip = zip
			instances_.Add(Me.City, Me)
		End Sub
		Public Overrides Function ToString() As String
			Return Street & ", " & Zip & " " & City
		End Function

		Public Street As String
		Public City As String
		Public Zip As String

		Private Shared instances_ As New System.Collections.Hashtable()
		Public Shared Function Find(ByVal addressString As String) As Address
			If instances_.Contains(addressString) Then
				Return TryCast(instances_(addressString), Address)
			End If
			Return Nothing
		End Function
		Public Function CompareTo(ByVal obj As Object) As Integer Implements IComparable.CompareTo
			Dim addr As Address = TryCast(obj, Address)
			If addr Is Nothing Then
				Return -1
			End If
			Return Me.City.CompareTo(addr.City)
		End Function
		Public Overrides Overloads Function Equals(ByVal obj As Object) As Boolean
			Return CompareTo(obj) = 0
		End Function
		Public Overrides Function GetHashCode() As Integer
			Return MyBase.GetHashCode()
		End Function
	End Class
End Namespace
