using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressBookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string name;
        private string sureName = "";
        private string lastName = "";

        public ContactData(string name)
        {
            this.name = name;
        }
        public ContactData(string name, string lastName)
        {
            this.name = name;
            this.lastName = lastName;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string SureName
        {
            get { return sureName; }
            set { sureName = value; }
        }
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return "name=" + Name;
        }

        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return Name == other.Name && LastName == other.LastName;
        }

        public int CompareTo(ContactData other)
        {
            int result = Name.CompareTo(other.Name);
            if (result == 0)
            {
                result = LastName.CompareTo(other.LastName);
            }
            return result;
        }
    }
}
