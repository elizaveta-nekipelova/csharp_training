using NUnit.Framework;
using System.Collections.Generic;

namespace addressbook_web_tests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            ContactData newData = new ContactData("Test2", "User2");
            newData.Email = "test2@user.com";
            newData.Company = "aaa2";
            newData.Address = "bbb2";
            newData.Homepage = "ccc2";
            newData.BirthDay = "2";
            newData.BirthMonth = "May";
            newData.BirthYear = "2010";
            newData.Notes = "Test2";
            
            int index = 1;

            if (!app.Contacts.ContactExists(index))
            {
                ContactData contact = new ContactData("Test", "User");
                app.Contacts.Create(contact);
            }

            List<ContactData> oldContacts = app.Contacts.GetContactsList();

            app.Contacts.Modify(index, newData);

            List<ContactData> newContacts = app.Contacts.GetContactsList();

            oldContacts[index].Firstname = newData.Firstname;
            oldContacts[index].Lastname = newData.Lastname;
            oldContacts.Sort();
            oldContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
