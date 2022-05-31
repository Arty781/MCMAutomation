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
        #region Opening sidebar menu's tabs

        [AllureStep("Open Membership page")]
        public Sidebar OpenMemberShipPage()
        {
            
            Button.Click(membershipTb);

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


    }
}
