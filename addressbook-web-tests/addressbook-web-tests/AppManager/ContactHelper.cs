using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace addressbook_web_tests
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager)
            : base(manager) { }

        private List<ContactData> contactsCache = null;

        public ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.OpenHomePage();

            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index].FindElements(By.TagName("td"));
            string lastname = cells[1].Text;
            string firstname = cells[2].Text;
            string address = cells[3].Text;
            string allEmails = cells[4].Text;
            string allPhones = cells[5].Text;

            return new ContactData(firstname, lastname)
            {
                Address = address,
                AllPhones = allPhones,
                AllEmails = allEmails
            };
        }

        public string GetContactInformationFromDetails(int index)
        {
            manager.Navigator.OpenHomePage();
            driver.FindElements(By.Name("entry"))[index].FindElements(By.TagName("td"))[6].FindElement(By.TagName("a")).Click();
            return driver.FindElement(By.Id("container")).FindElement(By.Id("content")).Text;
        }

        public ContactData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.OpenHomePage();
            InitContactModification(index);

            string firstname = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string middlename = driver.FindElement(By.Name("middlename")).GetAttribute("value");
            string lastname = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string nickname = driver.FindElement(By.Name("nickname")).GetAttribute("value");
            string company = driver.FindElement(By.Name("company")).GetAttribute("value");
            string title = driver.FindElement(By.Name("title")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");

            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");
            string fax = driver.FindElement(By.Name("fax")).GetAttribute("value");

            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");

            string homepage = driver.FindElement(By.Name("homepage")).GetAttribute("value");
            string address2 = driver.FindElement(By.Name("address2")).Text;
            string phone2 = driver.FindElement(By.Name("phone2")).GetAttribute("value");
            string notes = driver.FindElement(By.Name("notes")).Text;

            return new ContactData(firstname, lastname)
            {
                Middlename = middlename,
                Nickname = nickname,
                Company = company,
                Title = title,
                Address = address,
                Home = homePhone,
                Mobile = mobilePhone,
                Work = workPhone,
                Fax = fax,
                Email = email,
                Email2 = email2,
                Email3 = email3,
                Homepage = homepage,
                Address2 = address2,
                Phone2 = phone2,
                Notes = notes
            };
        }

        public string GetContactInformationFromEditFormAsString(int index)
        {
            manager.Navigator.OpenHomePage();
            InitContactModification(index);

            string firstname = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string middlename = driver.FindElement(By.Name("middlename")).GetAttribute("value");
            string lastname = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string nickname = driver.FindElement(By.Name("nickname")).GetAttribute("value");
            string company = driver.FindElement(By.Name("company")).GetAttribute("value");
            string title = driver.FindElement(By.Name("title")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");

            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");
            string fax = driver.FindElement(By.Name("fax")).GetAttribute("value");

            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");

            string homepage = driver.FindElement(By.Name("homepage")).GetAttribute("value");
            string address2 = driver.FindElement(By.Name("address2")).Text;
            string phone2 = driver.FindElement(By.Name("phone2")).GetAttribute("value");
            string notes = driver.FindElement(By.Name("notes")).Text;


            string names = Regex.Replace($"{firstname} {middlename} {lastname}".Trim(), @"\s+", " ");
            string personal = Regex.Replace($"{nickname}\r\n{title}\r\n{company}\r\n{address}".Trim(), @"[\r\n]+", "\r\n");

            string homePhoneTemp = (homePhone == "") ? "" : $"H: {homePhone}";
            string mobilePhoneTemp = (mobilePhone == "") ? "" : $"M: {mobilePhone}";
            string workPhoneTemp = (workPhone == "") ? "" : $"W: {workPhone}";
            string faxTemp = (fax == "") ? "" : $"F: {fax}";
            string phones = Regex.Replace($"{homePhoneTemp}\r\n{mobilePhoneTemp}\r\n{workPhoneTemp}\r\n{faxTemp}".Trim(), @"[\r\n]+", "\r\n");

            string homepageTemp = (homepage == "") ? "" : $"Homepage:\r\n{homepage}";
            string emails = Regex.Replace($"{email}\r\n{email2}\r\n{email3}\r\n{homepageTemp}".Trim(), @"[\r\n]+", "\r\n");

            string phone2Temp = (phone2 == "") ? "" : $"P: {phone2}";
            string secondary = Regex.Replace($"{address2}\r\n\r\n{phone2Temp}\r\n\r\n{notes}".Trim(), @"[\r\n\r\n\r\n\r\n]+", "\r\n\r\n");

            string emailsTemp = (emails == "") ? "" : $"\r\n\r\n{emails}";
            string phonesTemp = (phones == "") ? "" : $"\r\n\r\n{phones}";
            
            return $"{names}\r\n{personal}{phonesTemp}{emailsTemp}\r\n\r\n\r\n{secondary}".Trim();
        }

        public ContactHelper Create(ContactData contact)
        {
            InitNewContactCreation();
            FillContactForm(contact);
            SubmitContactCreation();
            manager.Navigator.ReturnToHomePage();
            contactsCache = null;
            return this;
        }

        public ContactHelper Modify(int index, ContactData newData)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]")).Click();
            InitContactModification(index);
            FillContactForm(newData);
            driver.FindElement(By.Name("update")).Click();
            manager.Navigator.ReturnToHomePage();
            contactsCache = null;
            return this;
        }

        public ContactHelper Modify(ContactData contact, ContactData newData)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]' and @value='" + contact.Id + "'])/../..//img[@title='Edit']")).Click();
            FillContactForm(newData);
            driver.FindElement(By.Name("update")).Click();
            manager.Navigator.ReturnToHomePage();
            contactsCache = null;
            return this;
        }

        public ContactHelper Remove(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]")).Click();
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            driver.SwitchTo().Alert().Accept();
            manager.Navigator.ReturnToHomePage();
            contactsCache = null;
            return this;
        }

        public ContactHelper Remove(ContactData contact)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]' and @value='" + contact.Id + "'])")).Click();
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            driver.SwitchTo().Alert().Accept();
            manager.Navigator.ReturnToHomePage();
            contactsCache = null;
            return this;
        }

        public ContactHelper InitNewContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }

        public ContactHelper InitContactModification(int index)
        {
            driver.FindElement(By.XPath("(//img[@title='Edit'])[" + (index + 1) + "]")).Click();
            return this;
        }

        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.Firstname);
            Type(By.Name("middlename"), contact.Middlename);
            Type(By.Name("lastname"), contact.Lastname);
            Type(By.Name("nickname"), contact.Nickname);
            Type(By.Name("title"), contact.Title);
            Type(By.Name("company"), contact.Company);
            Type(By.Name("address"), contact.Address);
            Type(By.Name("home"), contact.Home);
            Type(By.Name("mobile"), contact.Mobile);
            Type(By.Name("work"), contact.Work);
            Type(By.Name("fax"), contact.Fax);
            Type(By.Name("email"), contact.Email);
            Type(By.Name("email2"), contact.Email2);
            Type(By.Name("email3"), contact.Email3);
            Type(By.Name("homepage"), contact.Homepage);
            new SelectElement(driver.FindElement(By.Name("bday"))).SelectByText(contact.BirthDay);
            new SelectElement(driver.FindElement(By.Name("bmonth"))).SelectByText(contact.BirthMonth);
            Type(By.Name("byear"), contact.BirthYear);
            new SelectElement(driver.FindElement(By.Name("aday"))).SelectByText(contact.AnniversaryDay);
            new SelectElement(driver.FindElement(By.Name("amonth"))).SelectByText(contact.AnniversaryMonth);
            Type(By.Name("ayear"), contact.AnniversaryYear);
            Type(By.Name("address2"), contact.Address2);
            Type(By.Name("phone2"), contact.Phone2);
            Type(By.Name("notes"), contact.Notes);
            return this;
        }

        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.XPath("//div[@id='content']/form/input[21]")).Click();
            return this;
        }

        public bool ContactExists(int index)
        {
            return IsElementPresent(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]"));
        }

        public List<ContactData> GetContactsList()
        {
            if (contactsCache == null)
            {
                contactsCache = new List<ContactData>();
                manager.Navigator.OpenHomePage();
                ICollection<IWebElement> contactList = driver.FindElements(By.Name("entry"));

                foreach (IWebElement element in contactList)
                {
                    contactsCache.Add(new ContactData(
                        element.FindElements(By.TagName("td")).ElementAt(2).Text,
                        element.FindElements(By.TagName("td")).ElementAt(1).Text));
                }
            }
            return contactsCache;
        }

        public void AddContactToGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.OpenHomePage();
            ClearGroupFilter();
            SelectContact(contact.Id);
            SelectGroupToAdd(group.Name);
            CommitAddingContactToGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }

        public void ClearGroupFilter()
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText("[all]");
        }

        public void SelectContact(string id)
        {
            driver.FindElement(By.Id(id)).Click();
        }

        public void SelectGroupToAdd(string name)
        {
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByText(name);
        }

        public void CommitAddingContactToGroup()
        {
            driver.FindElement(By.Name("add")).Click();
        }

        public void RemoveContactFromGroup(ContactData contact, GroupData groupToRemoveFrom)
        {
            manager.Navigator.OpenHomePage();
            SelectGroupToRemoveContactFrom(groupToRemoveFrom.Name);
            SelectContact(contact.Id);
            CommitRemovalContactFromGroup();
        }

        public void SelectGroupToRemoveContactFrom(string name)
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText(name);
        }

        public void CommitRemovalContactFromGroup()
        {
            driver.FindElement(By.Name("remove")).Click();
        }
    }
}
