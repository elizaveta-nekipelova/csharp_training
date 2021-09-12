﻿using NUnit.Framework;

namespace addressbook_web_tests
{
    [TestFixture]
    public class ContactCreationTests : TestBase
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

            app.Contacts.Create(contact);
        }
    }
}