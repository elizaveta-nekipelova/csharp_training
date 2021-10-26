using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tests
{
    public class TestBase
    {
        public static bool PERFORM_LONG_UI_CHECKS = false;

        protected ApplicationManager app;
        public static Random rnd = new Random();

        [SetUp]
        public void SetupApplicationManager()
        {
            app = ApplicationManager.GetInstance();
        }

        public static string GenerateRandomString(int max)
        {
            int l = Convert.ToInt32(rnd.NextDouble() * max);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < l; i++)
            {
                builder.Append(Convert.ToChar(32 + Convert.ToInt32(rnd.NextDouble() * 65)));
            }
            return builder.ToString();
        }

        public static string GenerateRandomEmail()
        {
            StringBuilder email = new StringBuilder();
            for (int i = 0; i < 10; i++)
            {
                email.Append(Convert.ToChar(32 + Convert.ToInt32(rnd.NextDouble() * 65)));
            }

            StringBuilder domain = new StringBuilder();
            for (int i = 0; i < 5; i++)
            {
                domain.Append(Convert.ToChar(32 + Convert.ToInt32(rnd.NextDouble() * 65)));
            }

            return email.ToString() + "@" + domain.ToString() + ".com";
        }

        public static string GenerateRandomPhone()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < 10; i++)
            {
                builder.Append(rnd.Next(0, 9));
            }

            return builder.ToString();
        }
    }
}
