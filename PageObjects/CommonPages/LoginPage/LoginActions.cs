using NUnit.Allure.Steps;
using OpenQA.Selenium;
using MCMAutomation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCMAutomation.APIHelpers;

namespace MCMAutomation.PageObjects
{
    public partial class Login
    {
        #region Admin login

        [AllureStep("Login as admin")]
        public Login GetLogin(string login, string password)
        {

            InputBox.ElementCtrlA(loginInput, 30, login);
            InputBox.ElementCtrlA(passwordInput, 30, password);

            Button.Click(signInBtn);
            var responseLogin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            MembershipRequest.GetMembershipsWithUsersList(responseLogin);

            return this;
        }

        [AllureStep("Login as admin")]
        public Login CopyUserEmail(string login)
        {

            InputBox.ElementClear(loginInput, 30, login);

            TextBox.CopyTextToBuffer(loginInput);

            return this;
        }

        [AllureStep("Logout as admin")]
        public Login GetAdminLogout()
        {
            WaitUntil.WaitForElementToAppear(adminLogOutBtn, 30);
            Button.Click(adminLogOutBtn);
            WaitUntil.WaitForElementToAppear(signInBtn, 30);

            return this;
        }


        #endregion

        #region User login

        [AllureStep("Login as user")]
        public Login GetUserLoginForTdee(string login, string password)
        {
           
                InputBox.ElementClear(loginInput, 30, login);
                InputBox.ElementClear(passwordInput, 30, password);

                Button.Click(signInBtn);

           
            return this;
        }

        [AllureStep("Login as user")]
        public Login GetUserLogin(string login, string password)
        {
            InputBox.ElementClear(loginInput, 30, login);
            InputBox.ElementClear(passwordInput, 30, password);
            
            Button.Click(signInBtn);
            return this;
        }

        [AllureStep("Logout as user")]
        public Login GetUserLogout()
        {
            WaitUntil.WaitForElementToAppear(userContextMenu, 30);
            Button.Click(userContextMenu);

            WaitUntil.WaitForElementToAppear(userLogoutBtn, 30);
            Button.Click(userLogoutBtn);
            WaitUntil.WaitForElementToAppear(signInBtn, 30);

            return this;
        }


        #endregion




    }
}
