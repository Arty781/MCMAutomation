using OpenQA.Selenium;
using MCMAutomation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.PageObjects
{
    public partial class Login
    {
       
        public IWebElement loginFld => Browser._Driver.FindElement(_loginFld);
        public readonly By _loginFld = By.XPath("//input[@name='email']");

        public IWebElement passwordFld => Browser._Driver.FindElement(_passwordFld);
        public readonly By _passwordFld = By.XPath("//input[@name='password']");

        public IWebElement signInBtn => Browser._Driver.FindElement(_signInBtn);
        public readonly By _signInBtn = By.XPath("//button[@type='submit']");


    }
}
