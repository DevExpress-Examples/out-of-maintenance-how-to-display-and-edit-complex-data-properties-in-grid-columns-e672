using System;

namespace TypeConverterTest {
	public class Address : IComparable {
		public static implicit operator Address(string addressString) {
			return Find(addressString);
		}
		public Address(string street, string city, string zip) {
			Street = street;
			City = city;
			Zip = zip;
			instances_.Add(this.City, this);
		}
		public override string ToString() {
			return Street + ", " + Zip + " " + City;
		}

		public string Street;
		public string City;
		public string Zip;
	
		static System.Collections.Hashtable instances_ = new System.Collections.Hashtable();
		public static Address Find(string addressString) {
			if (instances_.Contains(addressString))
				return instances_[addressString] as Address;
			return null;
		}
		public int CompareTo(object obj) {
			Address addr = obj as Address;
			if(addr == null) return -1;
			return this.City.CompareTo(addr.City);
		}
		public override bool Equals(object obj) {
			return CompareTo(obj) == 0;
		}
		public override int GetHashCode() {
			return base.GetHashCode();
		}
	}
}
