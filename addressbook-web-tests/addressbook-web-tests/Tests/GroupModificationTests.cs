﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tests
{
    [TestFixture]
    public class GroupModificationTests : AuthTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            GroupData newData = new GroupData("aaa");
            newData.Header = null;
            newData.Footer = null;

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

            app.Groups.Modify(index, newData);

            Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupsCount());

            List<GroupData> newGroups = app.Groups.GetGroupsList();

            oldGroups[index].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
            
            foreach (GroupData group in newGroups)
            {
                if (group.Id == oldData.Id)
                {
                    Assert.AreEqual(newData.Name, group.Name);
                }
            }

        }
    }
}
