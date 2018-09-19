using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;


namespace WebAddressBookTests
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase
    {
       
        [Test]
        public void GroupRemovalTest()
        {
            List<GroupData> oldGroups = app.Groups.GetGroupList();

            if (!app.Groups.IsExistGroup(1))
            {
                CreateNewGroups n = new CreateNewGroups();
                n.CreateNewGroup();
            }
            app.Groups.Remove(0);

            List<GroupData> newGroups = app.Groups.GetGroupList();
            GroupData toBeRemoved = oldGroups[0];
            oldGroups.RemoveAt(0);
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups)
            {
                Assert.AreNotEqual(group.Id, toBeRemoved.Id);
            }
        }
    }
}
