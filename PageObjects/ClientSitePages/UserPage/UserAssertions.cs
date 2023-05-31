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
        public List<string> GetUserDataBeforeSaving()
        {
            var userData = new List<string>();

            userData.Add(TextBox.GetAttribute(inputFirstName, "value"));
            userData.Add(TextBox.GetAttribute(inputLastName, "value"));
            userData.Add(DateTime.Parse(TextBox.GetAttribute(inputBirthDate, "value")).AddDays(-1).ToString("yyyy-MM-dd"));
            userData.Add(TextBox.GetAttribute(inputProtein, "value"));
            userData.Add(TextBox.GetAttribute(inputCalories, "value"));
            userData.Add(TextBox.GetAttribute(inputMaintenanceCalories, "value"));
            userData.Add(TextBox.GetAttribute(inputCarbs, "value"));
            userData.Add(TextBox.GetAttribute(inputFats, "value"));
            userData.Add(TextBox.GetAttribute(inputHeight, "value"));
            userData.Add(TextBox.GetAttribute(inputWeight, "value"));
            userData.Add(TextBox.GetAttribute(inputEmail, "value"));

            

            return userData;
        }

        [AllureStep("Verify User data after saving")]
        public List<string> GetUserDataAfterSaving()
        {
            WaitUntil.WaitSomeInterval(500);
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

            return userData;
        }

        [AllureStep("Verify User data")]
        public void VerifyUserData(IEnumerable<string> expectedData, IEnumerable<string> actualData)
        {
            if (expectedData == null)
            {
                throw new ArgumentNullException(nameof(expectedData));
            }

            if (actualData == null)
            {
                throw new ArgumentNullException(nameof(actualData));
            }

            CollectionAssert.AreEquivalent(expectedData, actualData);
        }

        public void VerifyDisplayingReferringBtn()
        {
            WaitUntil.WaitForElementToAppear(linkReferring);
            Assert.IsTrue(linkReferring.Displayed);
        }
        
    }
}
