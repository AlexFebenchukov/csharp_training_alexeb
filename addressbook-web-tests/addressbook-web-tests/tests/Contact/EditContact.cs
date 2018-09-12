using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressBookTests
{
    class EditContacts : AuthTestBase
    {
        [Test]
        public void EditContact()
        {
            ContactData contact = new ContactData("aaBBB");
            contact.SureName = "aaCCCC";
            contact.LastName = "aaDDDD";

            app.Contacts.EditContact(1, contact);
        }
    }
}
