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

            List<ContactData> oldContacts = app.Contacts.GetContactList();

            app.Contacts.EditContact(1, contact);
            oldContacts.RemoveAt(0);
            oldContacts.Add(contact);
            oldContacts.Sort();

            List<ContactData> newContacts = app.Contacts.GetContactList();
            
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
