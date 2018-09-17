using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressBookTests
{
    public class DeleteContacts : AuthTestBase
    {
        [Test]
        public void DeleteContact()
        {
            List<ContactData> oldContacts = app.Contacts.GetContactList();

            app.Contacts.DeleteContact(0);

            List<ContactData> newContacts = app.Contacts.GetContactList();
            if(oldContacts.Count != 0) { oldContacts.RemoveAt(0); }
            Assert.AreEqual(oldContacts, newContacts);
        }

        [Test]
        public void DeleteAllContacts()
        {
            List<ContactData> oldContacts = app.Contacts.GetContactList();

            app.Contacts.DeleteAllContacts();

            List<ContactData> newContacts = app.Contacts.GetContactList();
            if (oldContacts.Count != 0) { oldContacts.RemoveAt(0); }
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
