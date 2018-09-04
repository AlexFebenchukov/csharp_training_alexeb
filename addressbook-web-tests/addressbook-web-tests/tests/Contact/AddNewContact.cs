using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;


namespace WebAddressBookTests
{
    [TestFixture]
    public class ContactsTests : TestBase
    {
        [Test]
        public void AddNewContact()
        {

            ContactData contact = new ContactData("AAAA");
            contact.SureName = "VVVV";
            contact.LastName = "FFFFF";

            app.Contacts.CreationNewContact(contact);
        }
    }
}
