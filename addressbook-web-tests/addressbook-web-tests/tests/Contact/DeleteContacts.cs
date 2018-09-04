using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressBookTests
{
    public class DeleteContacts : TestBase
    {
        [Test]
        public void DeleteContact()
        {
            app.Contacts.DeleteContact(7);
        }

        [Test]
        public void DeleteAllContacts()
        {
            app.Contacts.DeleteAllContacts();
        }
    }
}
