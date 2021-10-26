using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tests
{
    public class RemovalContactFromGroup : AuthTestBase
    {
        [Test]
        public void TestRemovalContactFromGroup()
        {
            List<GroupData> allGroups = GroupData.GetAll();
            List<ContactData> allContacts = ContactData.GetAll();
            GroupData groupToRemoveFrom = new GroupData();

            foreach (GroupData group in allGroups)
            {
                if (group.GetContacts().Count > 0)
                {
                    groupToRemoveFrom = group;
                    break;
                }
            }

            if (groupToRemoveFrom.Name == null) // in case there are no groups with assigned contacts
            {
                app.Contacts.AddContactToGroup(allContacts.First(), groupToRemoveFrom = allGroups.First());
            }

            List<ContactData> oldList = groupToRemoveFrom.GetContacts();
            ContactData contact = groupToRemoveFrom.GetContacts().First();

            app.Contacts.RemoveContactFromGroup(contact, groupToRemoveFrom);

            List<ContactData> newList = groupToRemoveFrom.GetContacts();
            newList.Add(contact);
            oldList.Sort();
            newList.Sort();
            Assert.AreEqual(oldList, newList);
        }
    }
}
