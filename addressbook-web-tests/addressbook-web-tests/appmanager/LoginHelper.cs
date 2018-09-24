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
    
    public class LoginHelper : HelperBase
    {
        private string baseURL;

        public LoginHelper(ApplicationManager manager) : base(manager)
        {

        }


        public void Login(AccountData account)
        {
            if (IsLoggedIn())
            {
                if (IsLoggedIn(account))
                {
                    return;
                }
                LogOut();
            }
            Type(By.Name("user"), account.Username);
            Type(By.Name("pass"), account.Password);

            driver.FindElement(By.Name("pass")).Clear();
            driver.FindElement(By.Name("pass")).SendKeys(account.Password);
            driver.FindElement(By.CssSelector("input[type=\"submit\"]")).Click();
        }

        public void LoginAsUser(AccountData account)
        {
            driver.FindElement(By.Name("user")).Clear();
            driver.FindElement(By.Name("user")).SendKeys("user");
            driver.FindElement(By.Name("pass")).Clear();
            driver.FindElement(By.Name("pass")).SendKeys("qwerty");
            driver.FindElement(By.CssSelector("input[type=\"submit\"]")).Click();
        }
        public bool IsLoggedIn(AccountData account)
        {
            return IsLoggedIn() 
                   && GetLoggedUserName() == account.Username;
        }

        public string GetLoggedUserName()
        {
            string name = driver.FindElement(By.Name("logout")).FindElement(By.TagName("b")).Text;
            return name.Substring(1, name.Length - 2);
        }

        public bool IsLoggedIn()
        {
            return IsElementPresent(By.Name("logout"));
        }

        public void LogOut()
        {
            if (IsLoggedIn())
            {
                driver.FindElement(By.LinkText("Logout")).Click();
            }
            
        }
    }
}
