using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressBookTests
{
    public class ContactHelper : HelperBase
    {
        private string baseURL;

        public ContactHelper(ApplicationManager manager) : base(manager)
        { }

        public ContactHelper CreationNewContact(ContactData contact)
        {
            manager.Navigator.GoToEditContactPage();
            FillContactForm(contact);
            SubmitContactCreation();
            GoToContactListPage();
            return this;
        }

        public ContactHelper FillContactForm(ContactData contact)
        {
            driver.FindElement(By.Name("firstname")).Clear();
            driver.FindElement(By.Name("firstname")).SendKeys(contact.Name);
            driver.FindElement(By.Name("lastname")).Clear();
            driver.FindElement(By.Name("lastname")).SendKeys(contact.LastName);
            driver.FindElement(By.Name("middlename")).Clear();
            driver.FindElement(By.Name("middlename")).SendKeys(contact.SureName);
            return this;
        }

        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.CssSelector("input[type=\"submit\"]")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper GoToAddContactPage()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }

        public ContactHelper GoToContactListPage()
        {
            driver.FindElement(By.LinkText("home")).Click();
            return this;
        }

        public ContactHelper DeleteContact(int rowNumber)
        {

            rowNumber++;
            manager.Navigator.GoToHomePage();

            SelectContact(rowNumber);

            Delete();
            return this;
        }

        public ContactHelper DeleteAllContacts()
        {
            manager.Navigator.GoToHomePage();
            if (IsExistContact(2))
            {
                SelectAllContacts();
            }
            else
            {
                ContactsTests cont = new ContactsTests();
                cont.AddNewContact();
                SelectAllContacts();
            }
            Delete();
            return this;
        }

        private ContactHelper InfillNewContactData(ContactData contact)
        {
            Type(By.Name("firstname"), contact.Name);
            Type(By.Name("middlename"), contact.SureName);
            Type(By.Name("lastname"), contact.LastName);
            return this;
        }

        private ContactHelper StartEditContact(int rowNumber)
        {
            if (IsElementPresent(By.XPath("(//table//tr[" + rowNumber + "]//td[8]//a)")))
            {
                driver.FindElement(By.XPath("(//table//tr[" + rowNumber + "]//td[8]//a)")).Click();
            }
            else
            {
                ContactData contact = new ContactData("AAAA");
                contact.SureName = "VVVV";
                contact.LastName = "FFFFF";
                CreationNewContact(contact);
            }

            return this;
        }

        public ContactHelper EditContact(int rowNumber, ContactData contact)
        {
            manager.Navigator.GoToHomePage();

            StartEditContact(rowNumber + 1);
            InfillNewContactData(contact);
            SubmitContactCreation();
            manager.Navigator.GoToHomePage();
            return this;
        }

        private ContactHelper SelectAllContacts()
        {
            if (IsElementPresent(By.CssSelector("input[name = 'selected[]']")))
            {
                driver.FindElement(By.XPath("(//input[@id='MassCB'])")).Click();
            }
            else
            {
                ContactData contact = new ContactData("AAAA");
                contact.SureName = "VVVV";
                contact.LastName = "FFFFF";
                CreationNewContact(contact);
                driver.FindElement(By.XPath("(//input[@id='MassCB'])")).Click();
            }
            return this;
        }

        private ContactHelper Delete()
        {
            driver.FindElement(By.CssSelector("input[value ='Delete']")).Click();
            contactCache = null;
            driver.SwitchTo().Alert().Accept();
            return this;
        }

        private ContactHelper SelectContact(int rowNumber)
        {
            rowNumber++;
            driver.FindElement(By.XPath("//table//tr[" + rowNumber + "]//input")).Click();
            return this;
        }

        public bool IsExistContact(int rowNumber)
        {
            rowNumber++;
            return IsElementPresent(By.XPath("//table//tr[" + rowNumber + "]//input"));
        }

        private List<ContactData> contactCache = null;

        public List<ContactData> GetContactList()
        {
            if (contactCache == null)
            {
                contactCache = new List<ContactData>();
                manager.Navigator.GoToHomePage();

                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("tr[name='entry']"));
                foreach (IWebElement element in elements)
                {
                    IList<IWebElement> cells = element.FindElements(By.TagName("td"));
                    string name = cells[2].Text;
                    string lastName = cells[1].Text;
                    contactCache.Add(new ContactData(name, lastName));
                }
            }
            return new List<ContactData>(contactCache);
        }

        public ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.GoToHomePage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index].FindElements(By.TagName("td"));
            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allPhones = cells[5].Text;

            return new ContactData(firstName, lastName)
            {
                Address = address,
                AllPhones = allPhones
            };
        }

        public ContactData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.GoToHomePage();
            InitContactModification(0);

            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");

            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");

            return new ContactData(firstName, lastName)
            {
                Address = address,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone
            };
        }

        public void InitContactModification(int index)
        {
            driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[7]
                .FindElement(By.TagName("a")).Click();
        }

        public int GetNumberOfSearchResults()
        {
            manager.Navigator.GoToHomePage();
            string text = driver.FindElement(By.TagName("label")).Text;
            Match m = new Regex(@"\d+").Match(text);
            return Int32.Parse(m.Value);
        }
    }
}
