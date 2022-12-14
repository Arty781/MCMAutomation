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
            WaitUntil.CustomElevemtIsVisible(btnSignUp);

            Button.Click(linkSignUp);

            return this;
        }

        [AllureStep("Enter user data")]
        public SignUpUser EnterData(string email)
        {
            InputBox.ElementCtrlA(inputFirstName,10, Name.FirstName());
            InputBox.ElementCtrlA(inputLastName, 10, Name.LastName());
            InputBox.ElementCtrlA(inputEmail, 10, email);
            InputBox.ElementCtrlA(inputConfirmEmail, 10, email);
            InputBox.ElementCtrlA(inputPassword, 10, "Qaz11111!");
            InputBox.ElementCtrlA(inputConfirmPassword, 10, "Qaz11111!");
            

            return this;
        }

        [AllureStep("Click on \"SignUp\" button")]
        public SignUpUser ClickOnSignUpBtn()
        {
            Button.Click(btnSignUp);

            return this;
        }

        [AllureStep("Go to Login page")]

        public SignUpUser GoToLoginPage()
        {
            Button.Click(btnLogin);

            return this;
        }
    }
}
