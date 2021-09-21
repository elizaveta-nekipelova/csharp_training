using NUnit.Framework;

namespace addressbook_web_tests
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            int index = 1;

            if (!app.Groups.GroupExists(index))
            {
                GroupData group = new GroupData("name");
                group.Header = "header";
                group.Footer = "footer";

                app.Groups.Create(group);
            }

            app.Groups.Remove(index);
        }
    }
}
