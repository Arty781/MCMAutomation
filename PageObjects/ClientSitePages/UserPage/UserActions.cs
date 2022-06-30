
using MCMAutomation.Helpers;
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
        public UserProfile AddFirstName()
        {
            
            InputBox.Element(inputFirstName, 10, Name.FirstName());

            return this;
        }

        public UserProfile AddLastName()
        {

            InputBox.Element(inputFirstName, 10, Name.LastName());

            return this;
        }

        public UserProfile EnterDOB()
        {

            InputBox.Element(inputBirthDate, 10, RandomHelper.RandomAge());

            return this;
        }

        public UserProfile EnterCalories()
        {

            InputBox.Element(inputCalories, 10, RandomHelper.RandomNumber(2000));

            return this;
        }

        public UserProfile EnterMaintenanceCalories()
        {

            InputBox.Element(inputMaintenanceCalories, 10, RandomHelper.RandomNumber(2000));

            return this;
        }

        public UserProfile EnterProteins()
        {

            InputBox.Element(inputProtein, 10, RandomHelper.RandomNumber(200));

            return this;
        }

        public UserProfile EnterCarbs()
        {

            InputBox.Element(inputCarbs, 10, RandomHelper.RandomNumber(200));

            return this;
        }

        public UserProfile EnterFats()
        {

            InputBox.Element(inputFats, 10, RandomHelper.RandomNumber(200));

            return this;
        }

        public UserProfile EnterHeight()
        {

            Button.Click(inputHeight);
            

            string[] selectedConversionSystem = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Preferred Conversion System");
            if (selectedConversionSystem[0] == "Imperial")
            {
                var i = inputOldPassword.Location.Y;
                Button.ScrollTo(0, i);

                WaitUntil.WaitSomeInterval(10000);
            }
            else if(selectedConversionSystem[0] == "Metric")
            {
                var i = inputOldPassword.Location.Y;
                Button.ScrollTo(0, i);

                WaitUntil.WaitSomeInterval(10000);
            }

            return this;
        }
    }
}
