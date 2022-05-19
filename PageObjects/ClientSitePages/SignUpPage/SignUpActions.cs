using MCMAutomation.Helpers;
using NUnit.Allure.Steps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.PageObjects
{
    public partial class SignUpUser
    {
        [AllureStep("Go to SignUp page")]
        public SignUpUser GoToSignUpPage()
        {
            WaitUntil.VisibilityOfAllElementsLocatedBy(_signUpBtn);

            signUpLink.Click();

            return this;
        }

        [AllureStep("Enter user data")]
        public SignUpUser EnterData(string email)
        {
            WaitUntil.VisibilityOfAllElementsLocatedBy(_firstNameInput);

            firstNameInput.Clear();
            firstNameInput.SendKeys("Jane");
            lastNameInput.Clear();
            lastNameInput.SendKeys("Doe");
            emailInput.Clear();
            emailInput.SendKeys(email);
            confirmEmailInput.Clear();
            confirmEmailInput.SendKeys(email);
            passwordInput.Clear();
            passwordInput.SendKeys("Qaz11111!");
            confirmPasswordInput.Clear();
            confirmPasswordInput.SendKeys("Qaz11111!");

            return this;
        }

        [AllureStep("Click on \"SignUp\" button")]
        public SignUpUser ClickOnSignUpBtn()
        {
            WaitUntil.VisibilityOfAllElementsLocatedBy(_signUpBtn);
            signUpBtn.Click();

            return this;
        }

        [AllureStep("Go to Login page")]

        public SignUpUser GoToLoginPage()
        {
            WaitUntil.VisibilityOfAllElementsLocatedBy(_loginBtn);

            loginBtn.Click();

            return this;
        }
    }
}
