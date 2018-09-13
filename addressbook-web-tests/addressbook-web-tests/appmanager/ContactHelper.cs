using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            driver.FindElement(By.Name("middlename")).Clear();
            driver.FindElement(By.Name("middlename")).SendKeys(contact.SureName);
            driver.FindElement(By.Name("lastname")).Clear();
            driver.FindElement(By.Name("lastname")).SendKeys(contact.LastName);
            return this;
        }

        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.CssSelector("input[type=\"submit\"]")).Click();
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
            manager.Navigator.GoToContactsListPage();
            SelectContact(rowNumber + 1);
            Delete();
            return this;
        }
        public ContactHelper DeleteAllContacts()
        {
            manager.Navigator.GoToContactsListPage();
            SelectAllContacts();
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
                driver.FindElement(By.XPath("(//table//tr[" + rowNumber + "]//td[8]//a)")).Click();
            }

            return this;
        }

        public ContactHelper EditContact(int rowNumber, ContactData contact)
        {
            manager.Navigator.GoToContactsListPage();

            StartEditContact(rowNumber + 1);
            InfillNewContactData(contact);
            SubmitContactCreation();
            manager.Navigator.GoToContactsListPage();
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
            driver.SwitchTo().Alert().Accept();
            return this;
        }

        private ContactHelper SelectContact(int rowNumber)
        {
            if (IsElementPresent(By.XPath("//table//tr[" + rowNumber + "]//input")))
            {
                driver.FindElement(By.XPath("//table//tr[" + rowNumber + "]//input")).Click();
            }
            else
            {
                ContactData contact = new ContactData("AAAA");
                contact.SureName = "VVVV";
                contact.LastName = "FFFFF";
                CreationNewContact(contact);
                driver.FindElement(By.XPath("//table//tr[" + rowNumber + "]//input")).Click();
            }
            return this;
        }

        
    }
}
