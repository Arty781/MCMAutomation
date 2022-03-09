using NUnit.Allure.Steps;
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
        #region Opening sidebar menu's tabs

        [AllureStep("Login")]
        public Login GetLogin(string login, string password)
        {
            WaitUntil.ElementIsVisible(_loginFld);
            loginFld.SendKeys(login);
            passwordFld.SendKeys(password);
            WaitUntil.WaitSomeInterval(1);
            signInBtn.Click();
            return this;
        }

       


        #endregion

       


    }
}
