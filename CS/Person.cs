using System;

namespace TypeConverterTest {
	public class Person {
		private string name_;
		private Address address_;

		public Person(string name, Address address) {
			name_ = name;
			address_ = address;
		}

		public string Name {
			get {
				return name_;
			}
			set {
				name_ = value;
			}
		}

		public Address Address {
			get {
				return address_;
			}
			set {
				address_ = value;
			}
		}
	}
}
