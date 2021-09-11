using NUnit.Framework;

namespace addressbook_web_tests
{
    [TestFixture]
    public class GroupCreationTests : TestBase
    {
        [Test]
        public void GroupCreationTest()
        {
            OpenHomePage();
            Login(new AccountData("admin", "secret"));
            GoToGroupsPage();
            InitNewGroupCreation();
            GroupData group = new GroupData("name");
            group.Header = "header";
            group.Footer = "footer";
            FillGroupForm(group);
            SubmitGroupCreation();
            ReturnToGroupsPage();
        }
    }
}
