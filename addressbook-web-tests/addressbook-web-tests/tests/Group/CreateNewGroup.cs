using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;


namespace WebAddressBookTests
{
    [TestFixture]
    public class CreateNewGroups : AuthTestBase
    {
       
        [Test]
        public void CreateNewGroup()
        {
            GroupData group = new GroupData("aaa");
            group.Header = "bbbb";
            group.Footer = "cccc";
            app.Groups.Create(group);
        }

        [Test]
        public void EmptyCreateNewGroup()
        {
            GroupData group = new GroupData("");
            group.Header = "";
            group.Footer = "";
            app.Groups.Create(group);
          //  app.Auth.LogOut();
        }







    }
}
