using MCMAutomation.Helpers;
using NUnit.Allure.Steps;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.PageObjects
{
    public partial class MembershipUser
    {
        [AllureStep("Verify is login successfully")]
        public MembershipUser VerifyIsBuyBtnDisplayed()
        {
            WaitUntil.VisibilityOfAllElementsLocatedBy(_buyBtn, 20);
            Assert.IsTrue(buyBtn.Displayed);

            return this;
        }
    }
}
