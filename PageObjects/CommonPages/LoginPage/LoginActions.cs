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

            InputBox.Element(loginInput, 30, login);
            InputBox.Element(passwordInput, 30, password);

            Button.Click(signInBtn);
            var responseLogin = SignInRequest.MakeAdminSignIn(Credentials.loginAdmin, Credentials.passwordAdmin);
            MembershipsWithUsersRequest.GetMembershipsWithUsersList(responseLogin);

            return this;
        }

        [AllureStep("Login as admin")]
        public Login CopyUserEmail(string login)
        {

            InputBox.Element(loginInput, 30, login);

            TextBox.CopyTextToBuffer(loginInput);

            return this;
        }

        [AllureStep("Logout as admin")]
        public Login GetAdminLogout()
        {
            WaitUntil.CustomElevemtIsVisible(adminLogOutBtn, 30);
            Button.Click(adminLogOutBtn);
            WaitUntil.CustomElevemtIsVisible(signInBtn, 30);

            return this;
        }


        #endregion

        #region User login

        [AllureStep("Login as user")]
        public Login GetUserLoginForTdee(string login, string password)
        {
           
                InputBox.Element(loginInput, 30, login);
                InputBox.Element(passwordInput, 30, password);

                Button.Click(signInBtn);

           
            return this;
        }

        [AllureStep("Login as user")]
        public Login GetUserLogin(string login, string password)
        {
            InputBox.Element(loginInput, 30, login);
            InputBox.Element(passwordInput, 30, password);
            
            Button.Click(signInBtn);
            return this;
        }

        [AllureStep("Logout as user")]
        public Login GetUserLogout()
        {
            WaitUntil.CustomElevemtIsVisible(userContextMenu, 30);
            Button.Click(userContextMenu);

            WaitUntil.CustomElevemtIsVisible(userLogoutBtn, 30);
            Button.Click(userLogoutBtn);
            WaitUntil.CustomElevemtIsVisible(signInBtn, 30);

            return this;
        }


        #endregion




    }
}
