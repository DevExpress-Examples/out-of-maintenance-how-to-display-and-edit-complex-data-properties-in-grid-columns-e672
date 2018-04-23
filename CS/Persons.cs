using System;
using System.ComponentModel;

namespace TypeConverterTest {
	/// <summary>
	/// Summary description for DataSource.
	/// </summary>
	public class Persons : System.Collections.CollectionBase, System.ComponentModel.ITypedList {
		public Persons() {
		}

		public void Add(Person person) {
			this.InnerList.Add(person);
		}
		public Person this[string name] {
			get {
				foreach(Person person in this.InnerList)
					if (person.Name == name)
						return person;
				return null;
			}
		}

		PropertyDescriptorCollection ITypedList.GetItemProperties(System.ComponentModel.PropertyDescriptor[] listAccessors) {		
			System.Collections.ArrayList descriptors = new System.Collections.ArrayList();
			descriptors.Add(new PropertyDescriptor("Name"));
			descriptors.Add(new PropertyDescriptor("Address"));
			descriptors.Add(new PropertyDescriptor("Address_City"));

			System.ComponentModel.PropertyDescriptor[] propertyDescriptors = new System.ComponentModel.PropertyDescriptor[descriptors.Count];
			descriptors.CopyTo(propertyDescriptors, 0);
			return new PropertyDescriptorCollection(propertyDescriptors);
		}

		string ITypedList.GetListName(System.ComponentModel.PropertyDescriptor[] listAccessors) {
			return "";
		}	
	}
}