using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tests
{
    public class AddingContactToGroupTests : AuthTestBase
    {
        [Test]
        public void TestAddingContactToGroup()
        {
            if (GroupData.GetAll().Count() == 0)
            {
                GroupData newGroup = new GroupData("TestAddingContactToGroupTest");
                newGroup.Header = "header";
                newGroup.Footer = "footer";

                app.Groups.Create(newGroup);
            }

            if (ContactData.GetAll().Count() == 0)
            {
                ContactData newContact = new ContactData("Test", "User");
                app.Contacts.Create(newContact);
            }

            List<GroupData> groups = GroupData.GetAll();
            List<ContactData> oldList = new List<ContactData>();

            // this is the check for the case when all contacts are assigned to every group
            // I go throught all groups and check if there are some missing contact
            // if no then new contact created
            int index = -1;
            for (int i = 0; i < groups.Count(); i++)
            {
                oldList = groups[i].GetContacts();

                if (ContactData.GetAll().Except(oldList).Count() != 0)
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
            {
                ContactData newContact = new ContactData("User", GenerateRandomString(5));
                app.Contacts.Create(newContact);
                index = 0;
            }
            // end of checking

            GroupData groupToAddContact = groups[index];
            ContactData contact = ContactData.GetAll().Except(oldList).First();

            app.Contacts.AddContactToGroup(contact, groupToAddContact);

            List<ContactData> newList = groupToAddContact.GetContacts();
            oldList.Add(contact);
            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);
        }


        [Test]
        public void TestAddingContactToGroup2()
        {
            if (GroupData.GetAll().Count() == 0)
            {
                GroupData newGroup = new GroupData("TestAddingContactToGroupTest");
                newGroup.Header = "header";
                newGroup.Footer = "footer";

                app.Groups.Create(newGroup);
            }

            if (ContactData.GetAll().Count() == 0)
            {
                ContactData newContact = new ContactData("Test", "User");
                app.Contacts.Create(newContact);
            }

            var groups = GroupData.GetAll().Where(g => ContactData.GetAll().Except(g.GetContacts()).Count() != 0).ToList();

            if (groups.Count() == 0)
            {
                ContactData newContact = new ContactData("User", GenerateRandomString(5));
                app.Contacts.Create(newContact);
                groups.Add(GroupData.GetAll().First());
            }

            GroupData groupToAddContact = groups.First();
            List<ContactData> oldList = groupToAddContact.GetContacts();
            ContactData contact = ContactData.GetAll().Except(oldList).First();

            app.Contacts.AddContactToGroup(contact, groupToAddContact);

            List<ContactData> newList = groupToAddContact.GetContacts();
            oldList.Add(contact);
            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);
        }
    }
}
