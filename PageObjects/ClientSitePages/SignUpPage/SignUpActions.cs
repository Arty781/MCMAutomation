using MCMAutomation.Helpers;
using NUnit.Allure.Steps;
using RimuTec.Faker;
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
            InputBox.ElementCtrlA(firstNameInput,10, Name.FirstName());
            InputBox.ElementCtrlA(lastNameInput, 10, Name.LastName());
            InputBox.ElementCtrlA(emailInput, 10, email);
            TextBox.CopyTextToBuffer(emailInput);
            InputBox.ElementCtrlA(confirmEmailInput, 10, email);
            InputBox.ElementCtrlA(passwordInput, 10, "Qaz11111!");
            InputBox.ElementCtrlA(confirmPasswordInput, 10, "Qaz11111!");
            

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
