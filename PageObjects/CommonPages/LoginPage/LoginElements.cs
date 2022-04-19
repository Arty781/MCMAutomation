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
       
        public IWebElement loginInput => Browser._Driver.FindElement(_loginInput);
        public readonly By _loginInput = By.XPath("//input[@name='email']");

        public IWebElement passwordInput => Browser._Driver.FindElement(_passwordInput);
        public readonly By _passwordInput = By.XPath("//input[@name='password']");

        public IWebElement signInBtn => Browser._Driver.FindElement(_signInBtn);
        public readonly By _signInBtn = By.XPath("//button[@type='submit']");

        public IWebElement adminLogOutBtn => Browser._Driver.FindElement(_adminLogOutBtn);
        public readonly By _adminLogOutBtn = By.XPath("//div[@class='sidebar-btn']");

        public IWebElement userContextMenu => Browser._Driver.FindElement(_userContextMenu);
        public readonly By _userContextMenu = By.XPath("//h3[@class='sidebar-info_name']");

        public IWebElement userLogoutBtn => Browser._Driver.FindElement(_userLogoutBtn);
        public readonly By _userLogoutBtn = By.XPath("//div[@class='logout']");


    }
}
