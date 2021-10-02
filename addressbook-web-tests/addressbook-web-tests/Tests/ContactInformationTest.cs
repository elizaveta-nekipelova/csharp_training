using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tests.Tests
{
    [TestFixture]
    public class ContactInformationTest : AuthTestBase
    {
        [Test]
        public void TestContactInformationFormAndEdit()
        {
            ContactData fromTable = app.Contacts.GetContactInformationFromTable(0);
            ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(0);

            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
            Assert.AreEqual(fromTable.AllEmails, fromForm.AllEmails);
        }

        [Test]
        public void TestContactInformationDetailsAndEdit()
        {
            ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(0);
            ContactData fromDetails = app.Contacts.GetContactInformationFromDetails(0);

            Assert.AreEqual(fromDetails, fromForm);
            Assert.AreEqual(fromDetails.PersonalBlock, fromForm.PersonalBlock);
            Assert.AreEqual(fromDetails.PhoneBlock, fromForm.PhoneBlock);
            Assert.AreEqual(fromDetails.EmailBlock, fromForm.EmailBlock);
            Assert.AreEqual(fromDetails.SummaryBlock, fromForm.SummaryBlock);
        }
    }
}
