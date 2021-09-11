using NUnit.Framework;

namespace addressbook_web_tests
{
    [TestFixture]
    public class ContactCreationTests : TestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            OpenHomePage();
            Login(new AccountData("admin", "secret"));
            InitNewContactCreation();
            ContactData contact = new ContactData("Test", "User");
            contact.Email = "test@user.com";
            contact.Company = "aaa";
            contact.Address = "bbb";
            contact.Homepage = "ccc";
            contact.BirthDay = "1";
            contact.BirthMonth = "May";
            contact.BirthYear = "2000";
            contact.Notes = "Test";
            FillContactForm(contact);
            SubmitContactCreation();
            ReturnToHomePage();
        }
    }
}
