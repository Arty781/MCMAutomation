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
        #region Admin login

        [AllureStep("Login as admin")]
        public Login GetLogin(string login, string password)
        {
            
                WaitUntil.VisibilityOfAllElementsLocatedBy(_loginInput, 30);
                loginInput.SendKeys(login);
                passwordInput.SendKeys(password);
                WaitUntil.WaitSomeInterval(1);
                signInBtn.Click();
            
            return this;
        }

        [AllureStep("Logout as admin")]
        public Login GetAdminLogout()
        {
            WaitUntil.VisibilityOfAllElementsLocatedBy(_adminLogOutBtn, 30);
            adminLogOutBtn.Click();

            return this;
        }


        #endregion

        #region User login

        [AllureStep("Login as user")]
        public Login GetUserLogin(List<User> login, string password)
        {
            foreach (var user in login)
            {
                string log = user.Email.ToString();
                WaitUntil.VisibilityOfAllElementsLocatedBy(_loginInput, 30);
                loginInput.SendKeys(log);
                passwordInput.SendKeys(password);
                WaitUntil.WaitSomeInterval(1);
                signInBtn.Click();

            }
            return this;
        }

        [AllureStep("Logout as user")]
        public Login GetUserLogout()
        {
            WaitUntil.VisibilityOfAllElementsLocatedBy (_userContextMenu, 30);
            userContextMenu.Click();

            WaitUntil.VisibilityOfAllElementsLocatedBy(_adminLogOutBtn, 30);
            adminLogOutBtn.Click();

            return this;
        }


        #endregion




    }
}
