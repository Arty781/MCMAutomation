using NUnit.Allure.Steps;
using NUnit.Framework;
using OpenQA.Selenium;
using MCMAutomation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.PageObjects
{
    public partial class MembershipAdmin
    {
        [AllureStep("Get membership name")]
        public string GetMembershipName()
        {
            WaitUntil.VisibilityOfAllElementsLocatedBy(_nameInput,30);
            string membershipName = nameInput.GetAttribute("value");
            return membershipName;
        }

        [AllureStep("Verify displaying membership name")]
        public MembershipAdmin VerifyMembershipName(string membership)
        {
            WaitUntil.VisibilityOfAllElementsLocatedBy(_membershipTitle);
            Assert.AreEqual(membership, membershipTitle.Text);
            if (membership != membershipTitle.Text)
            {
                Console.WriteLine("Membership \"" + membership + "\" is not found");
            }
            

            return this;
        }

        [AllureStep("Verify deleting membership")]
        public MembershipAdmin VerifyDeletingMembership(string membership)
        {
            WaitUntil.VisibilityOfAllElementsLocatedBy(Pages.Common._deleteMessage);
            WaitUntil.ElementIsInvisible(Pages.Common._deleteMessage, 20);

            WaitUntil.VisibilityOfAllElementsLocatedBy(_membershipTitle, 90);
            membershipSearchInput.Clear();
            membershipSearchInput.SendKeys(membership);

            Assert.AreEqual(false, PresenceOfElement.IsElementPresent(By.Name(membership)));
            

            return this;
        }

        [AllureStep("Verify membership name in combobox")]
        public MembershipAdmin VerifyMembershipNameCbbx(string membership)
        {
            WaitUntil.VisibilityOfAllElementsLocatedBy(_membershipNameCbbx);
            Assert.AreEqual(membership, membershipNameCbbx.Text);
            if (membership != membershipNameCbbx.Text)
            {
                Console.WriteLine("Membership \"" + membership + "\" is not found");
            }


            return this;
        }

        [AllureStep("Verify that assign user is displayed in table")]
        public MembershipAdmin VerifyAssignUser()
        {
            WaitUntil.VisibilityOfAllElementsLocatedBy(_emailColumn, 60);
            Assert.AreEqual("qatester92311@xitroo.com", emailColumn.Text);


            return this;
        }
        
    }
}
