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
            DateTime localDate = DateTime.Now;

            ContactData contact = new ContactData("aaBBB" + localDate.Millisecond);
            contact.SureName = "aaCCCC" + localDate.Minute;
            contact.LastName = "aaDDDD" + localDate.Second;
            if (!app.Contacts.IsExistContact(1))
            {
                ContactsTests cont = new ContactsTests();
                cont.AddNewContact();
            }

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
