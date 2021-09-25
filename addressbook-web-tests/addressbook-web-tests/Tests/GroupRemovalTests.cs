﻿using NUnit.Framework;
using System.Collections.Generic;

namespace addressbook_web_tests
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            int index = 0;

            if (!app.Groups.GroupExists(index))
            {
                GroupData group = new GroupData("name");
                group.Header = "header";
                group.Footer = "footer";

                app.Groups.Create(group);
            }

            List<GroupData> oldGroups = app.Groups.GetGroupsList();
            GroupData oldData = oldGroups[index];

            app.Groups.Remove(index);

            Assert.AreEqual(oldGroups.Count - 1, app.Groups.GetGroupsCount());

            List<GroupData> newGroups = app.Groups.GetGroupsList();

            oldGroups.RemoveAt(index);
            Assert.AreEqual(newGroups, oldGroups);

            foreach (GroupData group in newGroups)
            {
                Assert.AreNotEqual(group.Id, oldData.Id);
            }
        }
    }
}
