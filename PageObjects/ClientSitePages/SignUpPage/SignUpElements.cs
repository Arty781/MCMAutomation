using MCMAutomation.Helpers;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.PageObjects
{
    public partial class SignUpUser
    {
        [FindsBy(How=How.Name,Using = "firstName")]
        public IWebElement inputFirstName;

        [FindsBy(How = How.Name, Using = "lastName")]
        public IWebElement inputLastName;

        [FindsBy(How = How.Name, Using = "email")]
        public IWebElement inputEmail;

        [FindsBy(How = How.Name, Using = "confirmEmail")]
        public IWebElement inputConfirmEmail;

        [FindsBy(How = How.Name, Using = "password")]
        public IWebElement inputPassword;

        [FindsBy(How = How.Name, Using = "confirmPassword")]
        public IWebElement inputConfirmPassword;

        [FindsBy(How = How.XPath, Using = "//a[@class='signin-form_signup-link']")]
        public IWebElement linkSignUp;

        [FindsBy(How = How.XPath, Using = "//button[@type='submit']")]
        public IWebElement btnSignUp;

        [FindsBy(How = How.XPath, Using = "//button[@type='button']/a[@href='/auth/login']")]
        public IWebElement btnLogin;

        [FindsBy(How = How.XPath, Using = "//div[@class='confirmation-wrapper']/h2")]
        public IWebElement popupConfirm;
    }
}
