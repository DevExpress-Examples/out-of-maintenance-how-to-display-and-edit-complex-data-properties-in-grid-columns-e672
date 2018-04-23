Imports Microsoft.VisualBasic
Imports System

Namespace TypeConverterTest

	Public Class PropertyDescriptor
		Inherits System.ComponentModel.PropertyDescriptor
		Public Sub New(ByVal name As String)
			MyBase.New(name, getPropertyDescriptorAttributes(name))
		End Sub
		Public Overrides Sub SetValue(ByVal component As Object, ByVal value As Object)
			Dim person As Person = TryCast(component, Person)
			If (Not IsReadOnly) AndAlso person IsNot Nothing Then
				Select Case Me.Name
					Case "Name"
						person.Name = CStr(value)
					Case "Address"
						person.Address = CType(value, Address)
					Case "Address_City"
						person.Address.City = CStr(value)
				End Select
			End If
		End Sub

		Public Overrides Function GetValue(ByVal component As Object) As Object
			Dim person As Person = TryCast(component, Person)
			If person IsNot Nothing Then
				Select Case Me.Name
					Case "Name"
						Return person.Name
					Case "Address"
						Return person.Address
					Case "Address_City"
						If person.Address IsNot Nothing Then
							Return person.Address.City
						End If
				End Select
			End If
			Return Nothing
		End Function

		Public Overrides Function CanResetValue(ByVal component As Object) As Boolean
			Return False
		End Function
		Public Overrides Sub ResetValue(ByVal component As Object)
		End Sub
		Public Overrides ReadOnly Property PropertyType() As System.Type
			Get
				Select Case Me.Name
					Case "Name"
						Return GetType(String)
					Case "Address"
						Return GetType(Address)
					Case "Address_City"
						Return GetType(String)
				End Select
				Return Nothing
			End Get
		End Property
		Public Overrides ReadOnly Property IsReadOnly() As Boolean
			Get
				Return False
			End Get
		End Property
		Public Overrides ReadOnly Property ComponentType() As System.Type
			Get
				Return GetType(Person)
			End Get
		End Property
		Public Overrides Function ShouldSerializeValue(ByVal component As Object) As Boolean
			Return False
		End Function
		Private Shared Function getPropertyDescriptor(ByVal name As String) As System.ComponentModel.PropertyDescriptor
			Return System.ComponentModel.TypeDescriptor.GetProperties(GetType(Person)).Find(name, False)
		End Function

		Private Shared Function getPropertyDescriptorAttributes(ByVal name As String) As System.Attribute()
			Dim desc As System.ComponentModel.PropertyDescriptor = getPropertyDescriptor(name)
			If desc Is Nothing Then
				Return Nothing
			End If

			Dim attributes(desc.Attributes.Count - 1) As System.Attribute
			desc.Attributes.CopyTo(attributes, 0)

			Return attributes
		End Function

		Public Overrides ReadOnly Property Converter() As System.ComponentModel.TypeConverter
			Get
				If Me.Name = "Address" Then
					Return New AddressTypeConverter()
				Else
					Return MyBase.Converter
				End If
			End Get
		End Property
	End Class

	Friend Class AddressTypeConverter
		Inherits System.ComponentModel.TypeConverter

		Friend Sub New()
		End Sub
		Public Overrides Overloads Function ConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object, ByVal destinationType As System.Type) As Object
			If destinationType.Equals(GetType(String)) Then
				Dim address As Address = TryCast(value, Address)
				Return address.City
			Else
				Return MyBase.ConvertTo(context, culture, value, destinationType)
			End If
		End Function

		Public Overrides Overloads Function ConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object) As Object
			Return Address.Find(CStr(value))
		End Function

		Public Overrides Overloads Function CanConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal sourceType As System.Type) As Boolean
			If sourceType Is GetType(String) Then
				Return True
			End If
			Return False
		End Function
	End Class
End Namespace
