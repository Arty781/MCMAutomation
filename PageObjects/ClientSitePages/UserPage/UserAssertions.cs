using MCMAutomation.Helpers;
using NUnit.Allure.Steps;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.PageObjects.ClientSitePages
{
    public partial class UserProfile
    {
        [AllureStep("Get User data before saving")]
        public string[] GetUserDataBeforeSaving()
        {
            var userData = new List<string>();

            userData.Add(TextBox.GetAttribute(inputFirstName, "value"));
            userData.Add(TextBox.GetAttribute(inputLastName, "value"));
            userData.Add(TextBox.GetAttribute(inputBirthDate, "value"));
            userData.Add(TextBox.GetAttribute(inputProtein, "value"));
            userData.Add(TextBox.GetAttribute(inputCalories, "value"));
            userData.Add(TextBox.GetAttribute(inputMaintenanceCalories, "value"));
            userData.Add(TextBox.GetAttribute(inputCarbs, "value"));
            userData.Add(TextBox.GetAttribute(inputFats, "value"));
            userData.Add(TextBox.GetAttribute(inputHeight, "value"));
            userData.Add(TextBox.GetAttribute(inputWeight, "value"));
            userData.Add(TextBox.GetAttribute(inputEmail, "value"));

            string[] data1 = userData.ToArray();

            return data1;
        }

        [AllureStep("Verify User data before saving")]
        public UserProfile VerifyUserDataBeforeSaving(string[] dataBeforeSaving)
        {
            var userData = new List<string>();

            userData.Add(TextBox.GetAttribute(inputFirstName, "value"));
            userData.Add(TextBox.GetAttribute(inputLastName, "value"));
            userData.Add(TextBox.GetAttribute(inputBirthDate, "value"));
            userData.Add(TextBox.GetAttribute(inputProtein, "value"));
            userData.Add(TextBox.GetAttribute(inputCalories, "value"));
            userData.Add(TextBox.GetAttribute(inputMaintenanceCalories, "value"));
            userData.Add(TextBox.GetAttribute(inputCarbs, "value"));
            userData.Add(TextBox.GetAttribute(inputFats, "value"));
            userData.Add(TextBox.GetAttribute(inputHeight, "value"));
            userData.Add(TextBox.GetAttribute(inputWeight, "value"));
            userData.Add(TextBox.GetAttribute(inputEmail, "value"));

            string[] data2 = userData.ToArray();

            var list1 = dataBeforeSaving.Except(data2);
            var list2 = data2.Except(dataBeforeSaving);

            Assert.IsTrue(!list1.Any() && !list2.Any());

            return this;
        }
    }
}
