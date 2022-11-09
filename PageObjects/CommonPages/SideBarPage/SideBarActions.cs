using NUnit.Allure.Steps;
using OpenQA.Selenium;
using MCMAutomation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.PageObjects
{
    public partial class Sidebar
    {
        #region Opening Admin sidebar menu's tabs

        [AllureStep("Open Membership page")]
        public Sidebar OpenMemberShipPage()
        {
            var dateBefore = DateTime.Now;
            Button.Click(membershipTb);

            WaitUntil.CustomElevemtIsVisible(Pages.MembershipAdmin.btnCreateMembership, 90);
            var lastMembership = Pages.MembershipAdmin.membershipTitle.Last();
            WaitUntil.CustomElevemtIsVisible(lastMembership, 30);
            var dateAfter = DateTime.Now;
            Console.WriteLine($"Load time for {Browser._Driver.Url} is: " + (dateAfter - dateBefore));

            return this;
        }

        [AllureStep("Open Membership page")]
        public Sidebar OpenMemberShipPageUser()
        {
            
            Button.Click(trainingProgramTb);

            WaitUntil.CustomElevemtIsVisible(Pages.MembershipUser.programTitle, 30);
            

            return this;
        }

        [AllureStep("Open Users page")]
        public Sidebar OpenUsersPage()
        {
            
            Button.Click(usersTb);

            return this;
        }

        [AllureStep("Open Exercises page")]
        public Sidebar OpenExercisesPage()
        {
            
            Button.Click(exercisesTb);

            return this;
        }

        #endregion

        #region Opening User sidebar menu's tabs

        [AllureStep("Open Nutrition page")]
        public Sidebar OpenNutritionPage()
        {
            var dateBefore = DateTime.Now;
            Button.Click(nutritionTb);

            WaitUntil.CustomElevemtIsVisible(Pages.Nutrition.btnCalculate, 30);
            var dateAfter = DateTime.Now;
            Console.WriteLine("Load time is: " + (dateAfter - dateBefore));

            return this;
        }

        [AllureStep("Open MyAccount")]
        public Sidebar OpenMyAccount()
        {
            WaitUntil.WaitSomeInterval(3000);
            Button.Click(btnUserName);

            WaitUntil.CustomElevemtIsVisible(btnMyAccount, 30);
            Button.Click(btnMyAccount);
            WaitUntil.CustomElevemtIsVisible(Pages.UserProfile.inputFirstName, 30);

            return this;
        }

        [AllureStep("Open Progress")]
        public Sidebar OpenProgressPage()
        {

            Button.Click(progressTb);

            //WaitUntil.CustomElevemtIsVisible(Pages.Progress.titleProgressPage, 30);

            return this;
        }


        #endregion


    }
}
