Imports Microsoft.VisualBasic
Imports System
Imports System.ComponentModel

Namespace TypeConverterTest
	''' <summary>
	''' Summary description for DataSource.
	''' </summary>
	Public Class Persons
		Inherits System.Collections.CollectionBase
		Implements System.ComponentModel.ITypedList
		Public Sub New()
		End Sub

		Public Sub Add(ByVal person As Person)
			Me.InnerList.Add(person)
		End Sub
		Default Public ReadOnly Property Item(ByVal name As String) As Person
			Get
				For Each person As Person In Me.InnerList
					If person.Name = name Then
						Return person
					End If
				Next person
				Return Nothing
			End Get
		End Property

		Private Function GetItemProperties(ByVal listAccessors() As System.ComponentModel.PropertyDescriptor) As PropertyDescriptorCollection Implements ITypedList.GetItemProperties
			Dim descriptors As New System.Collections.ArrayList()
			descriptors.Add(New PropertyDescriptor("Name"))
			descriptors.Add(New PropertyDescriptor("Address"))
			descriptors.Add(New PropertyDescriptor("Address_City"))

			Dim propertyDescriptors(descriptors.Count - 1) As System.ComponentModel.PropertyDescriptor
			descriptors.CopyTo(propertyDescriptors, 0)
			Return New PropertyDescriptorCollection(propertyDescriptors)
		End Function

		Private Function GetListName(ByVal listAccessors() As System.ComponentModel.PropertyDescriptor) As String Implements ITypedList.GetListName
			Return ""
		End Function
	End Class
End Namespace