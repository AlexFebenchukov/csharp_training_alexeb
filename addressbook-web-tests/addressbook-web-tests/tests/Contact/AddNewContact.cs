﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;


namespace WebAddressBookTests
{
    [TestFixture]
    public class ContactsTests : AuthTestBase
    {
        [Test]
        public void AddNewContact()
        {
            ContactData contact = new ContactData("sash5444432a", "SS2343323SS");

            List<ContactData> oldContacts = app.Contacts.GetContactList();
            app.Contacts.CreationNewContact(contact);
            
            oldContacts.Add(contact);
            oldContacts.Sort();

            List<ContactData> newContacts = app.Contacts.GetContactList();
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
