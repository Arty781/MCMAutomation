
using MCMAutomation.Helpers;
using NUnit.Allure.Steps;
using OpenQA.Selenium;
using RimuTec.Faker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.PageObjects.ClientSitePages
{
    public partial class UserProfile
    {
        [AllureStep("Add First Name")]
        public UserProfile AddFirstName()
        {
            
            InputBox.Element(inputFirstName, 10, Name.FirstName());

            return this;
        }

        [AllureStep("Add Last Name")]
        public UserProfile AddLastName()
        {

            InputBox.Element(inputLastName, 10, Name.LastName());

            return this;
        }

        [AllureStep("Enter DOB")]
        public UserProfile EnterDOB()
        {

            InputBox.Element(inputBirthDate, 10, RandomHelper.RandomAge());

            return this;
        }

        [AllureStep("Enter Calories")]
        public UserProfile EnterCalories()
        {

            InputBox.Element(inputCalories, 10, RandomHelper.RandomNumber(2000));

            return this;
        }

        [AllureStep("Enter Maintenance Calories")]
        public UserProfile EnterMaintenanceCalories()
        {

            InputBox.Element(inputMaintenanceCalories, 10, RandomHelper.RandomNumber(2000));

            return this;
        }

        [AllureStep("Enter Proteins")]
        public UserProfile EnterProteins()
        {

            InputBox.Element(inputProtein, 10, RandomHelper.RandomNumber(200));

            return this;
        }

        [AllureStep("Enter Carbs")]
        public UserProfile EnterCarbs()
        {

            InputBox.Element(inputCarbs, 10, RandomHelper.RandomNumber(200));

            return this;
        }

        [AllureStep("Enter Fats")]
        public UserProfile EnterFats()
        {

            InputBox.Element(inputFats, 10, RandomHelper.RandomNumber(200));

            return this;
        }

        [AllureStep("Enter Height")]
        public UserProfile EnterHeight()
        {

            Button.Click(inputHeight);
            

            string[] selectedConversionSystem = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Preferred Conversion System");
            if (selectedConversionSystem[0] == "Imperial")
            {
                string activeElem = itemHeightActive.Text;
                while (activeElem != "4 ft 9 in")
                {
                    WaitUntil.WaitSomeInterval(200);
                    itemHeightNext.Click();
                    activeElem = itemHeightActive.Text;
                }
            }
            else if(selectedConversionSystem[0] == "Metric")
            {
                string activeElem = itemHeightActive.Text;
                while (activeElem != "150 cm")
                {
                    WaitUntil.WaitSomeInterval(200);
                    itemHeightNext.Click();
                    activeElem = itemHeightActive.Text;
                }
            }

            btnOk.Click();

            return this;
        }

        [AllureStep("Enter Weight")]
        public UserProfile EnterWeight()
        {

            InputBox.Element(inputWeight, 10, RandomHelper.RandomNumber(250));

            return this;
        }

        [AllureStep("Enter New Email")]
        public UserProfile EnterNewEmail()
        {

            InputBox.Element(inputEmail, 10, RandomHelper.RandomEmail());

            return this;
        }

        [AllureStep("Enter Old Pass")]
        public UserProfile EnterOldPass()
        {

            InputBox.Element(inputOldPassword, 10, "Qaz11111!");

            return this;
        }

        [AllureStep("Enter New Pass")]
        public UserProfile EnterNewPass()
        {

            InputBox.Element(inputChangePassword, 10, "Qaz11111!");

            return this;
        }

        [AllureStep("Enter Confirm Pass")]
        public UserProfile EnterConfirmPass()
        {

            InputBox.Element(inputConfirmPassword, 10, "Qaz11111!");

            return this;
        }

        
    }
}
