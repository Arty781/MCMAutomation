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

            Button.Click(signUpLink);

            return this;
        }

        [AllureStep("Enter user data")]
        public SignUpUser EnterData(string email)
        {
            InputBox.Element(firstNameInput,10, "Jane");
            InputBox.Element(lastNameInput, 10, "Doe");
            InputBox.Element(emailInput, 10, email);
            InputBox.Element(confirmEmailInput, 10, email);
            InputBox.Element(passwordInput, 10, "Qaz11111!");
            InputBox.Element(confirmPasswordInput, 10, "Qaz11111!");
            

            return this;
        }

        [AllureStep("Click on \"SignUp\" button")]
        public SignUpUser ClickOnSignUpBtn()
        {
            WaitUntil.VisibilityOfAllElementsLocatedBy(_signUpBtn);
            Button.Click(signUpBtn);

            return this;
        }

        [AllureStep("Go to Login page")]

        public SignUpUser GoToLoginPage()
        {
            WaitUntil.VisibilityOfAllElementsLocatedBy(_loginBtn);

            Button.Click(loginBtn);

            return this;
        }
    }
}
