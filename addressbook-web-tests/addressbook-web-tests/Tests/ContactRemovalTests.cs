using NUnit.Framework;
using System.Collections.Generic;

namespace addressbook_web_tests
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            int index = 0;

            if (!app.Contacts.ContactExists(index))
            {
                ContactData contact = new ContactData("Test", "User");
                app.Contacts.Create(contact);
            }

            List<ContactData> oldContacts = app.Contacts.GetContactsList();

            app.Contacts.Remove(index);

            List<ContactData> newContacts = app.Contacts.GetContactsList();

            oldContacts.RemoveAt(index);
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
