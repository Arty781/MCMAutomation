using OpenQA.Selenium;
using MCMAutomation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumExtras.PageObjects;

namespace MCMAutomation.PageObjects
{
    public partial class Login
    {
        [FindsBy(How = How.XPath, Using = "//input[@name='email']")]
        public IWebElement loginInput;

        [FindsBy(How = How.XPath, Using = "//input[@name='password']")]
        public IWebElement passwordInput;

        [FindsBy(How = How.XPath, Using = "//button[@type='submit']")]
        public IWebElement signInBtn;

        [FindsBy(How = How.XPath, Using = "//div[@class='sidebar-btn']")]
        public IWebElement adminLogOutBtn;

        [FindsBy(How = How.XPath, Using = "//h3[@class='sidebar-info_name']")]
        public IWebElement userContextMenu;

        [FindsBy(How = How.XPath, Using = "//div[@class='logout']")]
        public IWebElement userLogoutBtn;

        [FindsBy(How = How.XPath, Using = "//a[@class='signin-form_signup-link']")]
        public IWebElement userSignUpBtn;

        


    }
}
