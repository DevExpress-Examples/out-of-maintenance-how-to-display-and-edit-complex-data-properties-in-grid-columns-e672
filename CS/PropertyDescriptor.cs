using System;

namespace TypeConverterTest {

	public class PropertyDescriptor : System.ComponentModel.PropertyDescriptor {
		public PropertyDescriptor(string name)
			: base(name, getPropertyDescriptorAttributes(name)) {
		}
		public override void SetValue(object component, object value) {
			Person person = component as Person;
			if(!IsReadOnly && person != null) {	
				switch (this.Name) {
					case "Name":
						person.Name = (string)value;
						break;
					case "Address":
						person.Address = (Address)value;
						break;
					case "Address_City":
						person.Address.City = (string)value;
						break;
				}
			}
		}

		public override object GetValue(object component) {
			Person person = component as Person;
			if(person != null) {	
				switch (this.Name) {
					case "Name":
						return person.Name;
					case "Address":
						return person.Address;
					case "Address_City":
						if(person.Address != null)
							return person.Address.City;
						break;
				}
			}
			return null;
		}

		public override bool CanResetValue(object component) {
			return false;
		}
		public override void ResetValue(object component) {
		}
		public override System.Type PropertyType {
			get {
				switch (this.Name) {
					case "Name":
						return typeof(string);
					case "Address":
						return typeof(Address);
					case "Address_City":
						return typeof(string);
				}
				return null;
			}
		}
		public override bool IsReadOnly {
			get {
				return false;
			}
		}
		public override System.Type ComponentType {
			get {
				return typeof(Person);
			}
		}
		public override bool ShouldSerializeValue(object component) {
			return false;
		}	
		private static System.ComponentModel.PropertyDescriptor getPropertyDescriptor(string name) {
            return System.ComponentModel.TypeDescriptor.GetProperties(typeof(Person)).Find(name, false);
		}

		private static System.Attribute[] getPropertyDescriptorAttributes(string name) {
			System.ComponentModel.PropertyDescriptor desc = getPropertyDescriptor(name);
			if(desc == null)
				return null;

			System.Attribute[] attributes = new System.Attribute[desc.Attributes.Count];
			desc.Attributes.CopyTo(attributes, 0);

			return attributes;
		}

		public override System.ComponentModel.TypeConverter Converter {
			get {
				if (this.Name == "Address")
					return new AddressTypeConverter();
				else
					return base.Converter;
			}
		}
	}

	internal class AddressTypeConverter : System.ComponentModel.TypeConverter {

		internal AddressTypeConverter() {
		}
		public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, System.Type destinationType) {
			if (destinationType.Equals(typeof(string))) {
				Address address = value as Address;
				return address.City;
			}
			else
				return base.ConvertTo(context, culture, value, destinationType);
		}

		public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value) {
			return Address.Find((string) value);
		}

		public override bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Type sourceType) {
			if (sourceType == typeof(string))
				return true;
			return false;
		}
	}
}
