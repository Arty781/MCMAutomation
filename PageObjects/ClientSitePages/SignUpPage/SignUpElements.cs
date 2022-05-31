using MCMAutomation.Helpers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.PageObjects
{
    public partial class SignUpUser
    {
        public IWebElement firstNameInput => Browser._Driver.FindElement(_firstNameInput);
        public readonly By _firstNameInput = By.XPath("//input[@name='firstName']");

        public IWebElement lastNameInput => Browser._Driver.FindElement(_lastNameInput);
        public readonly By _lastNameInput = By.XPath("//input[@name='lastName']");

        public IWebElement emailInput => Browser._Driver.FindElement(_emailInput);
        public readonly By _emailInput = By.XPath("//input[@name='email']");


        public IWebElement confirmEmailInput => Browser._Driver.FindElement(_confirmEmailInput);
        public readonly By _confirmEmailInput = By.XPath("//input[@name='confirmEmail']");

        public IWebElement passwordInput => Browser._Driver.FindElement(_passwordInput);
        public readonly By _passwordInput = By.XPath("//input[@name='password']");

        public IWebElement confirmPasswordInput => Browser._Driver.FindElement(_confirmPasswordInput);
        public readonly By _confirmPasswordInput = By.XPath("//input[@name='confirmPassword']");

        public IWebElement signUpLink => Browser._Driver.FindElement(_signUpLink);
        public readonly By _signUpLink = By.XPath("//a[@class='signin-form_signup-link']");

        public IWebElement signUpBtn => Browser._Driver.FindElement(_signUpBtn);
        public readonly By _signUpBtn = By.XPath("//button[@type='submit']");

        public IWebElement loginBtn => Browser._Driver.FindElement(_loginBtn);
        public readonly By _loginBtn = By.XPath("//button[@type='button']/a[@href='/auth/login']");

        public IWebElement confirmPopUp => Browser._Driver.FindElement(_confirmPopUp);
        public readonly By _confirmPopUp = By.XPath("//div[@class='confirmation-wrapper']/h2");
    }
}
