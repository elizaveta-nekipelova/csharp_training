using NUnit.Framework;
using System.Collections.Generic;

namespace addressbook_web_tests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            ContactData contact = new ContactData("Test", "User");
            contact.Email = "test@user.com";
            contact.Company = "aaa";
            contact.Address = "bbb";
            contact.Homepage = "ccc";
            contact.BirthDay = "1";
            contact.BirthMonth = "May";
            contact.BirthYear = "2000";
            contact.Notes = "Test";

            List<ContactData> oldContacts = app.Contacts.GetContactsList();

            app.Contacts.Create(contact);

            List<ContactData> newContacts = app.Contacts.GetContactsList();

            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
