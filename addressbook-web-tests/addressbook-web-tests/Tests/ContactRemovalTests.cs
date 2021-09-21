using NUnit.Framework;

namespace addressbook_web_tests
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            int index = 1;

            if (!app.Contacts.ContactExists(index))
            {
                ContactData contact = new ContactData("Test", "User");
                app.Contacts.Create(contact);
            }

            app.Contacts.Remove(index);
        }
    }
}
