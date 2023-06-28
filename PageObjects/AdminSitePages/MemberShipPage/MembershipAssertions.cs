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
            string membershipName = TextBox.GetAttribute(membershipNameInput, "value");
            return membershipName;
        }

        [AllureStep("Get Program names")]
        public List<string> GetProgramNames()
        {
            var programNames = new List<string>();
            if (nameProgramTitle.Count != 0)
            {
                WaitUntil.WaitForElementToAppear(nameProgramTitleElem);
                programNames = nameProgramTitle.Where(x => x.Displayed).Select(x => x.Text).ToList();
            }
            
            return programNames;
        }

        [AllureStep("Verify deleting of programs")]
        public MembershipAdmin VerifyDeletePrograms()
        {
            Assert.Throws<NoSuchElementException>(() => Browser._Driver.FindElement(By.XPath("//div[@class='table-item-name']")));

            return this;
        }

        [AllureStep("Verify displaying membership name")]
        public MembershipAdmin VerifyMembershipName(string membershipBeforeEdit, string membershipAfterEdit)
        {
            WaitUntil.WaitForElementToAppear(Browser.FindElement($"//h2[@class='membership-item_title' and contains(text(),'{membershipAfterEdit}')]"));
            Assert.IsTrue(membershipBeforeEdit != membershipTitleElem.Text, $"Membership {membershipAfterEdit}is not found");

            return this;
        }

        [AllureStep("Verify displaying membership name")]
        public MembershipAdmin VerifyMembershipName(string membershipName)
        {
            Assert.IsTrue(membershipName == membershipTitleElem.Text, $"Membership {membershipName}is not found");

            return this;
        }

        [AllureStep("Verify deleting membership")]
        public MembershipAdmin VerifyDeletingMembership(string membershipName)
        {
            const int inputBoxTimeout = 30;
            WaitUntil.WaitForElementToAppear(Pages.CommonPages.Common.messageDeleted);
            InputBox.ElementCtrlA(membershipSearchInput, inputBoxTimeout, membershipName);
            bool isMembershipPresent = PresenceOfElement.IsElementPresent(By.Name(membershipName));
            Assert.IsFalse(isMembershipPresent, $"Membership '{membershipName}' was not deleted.");

            return this;
        }

        [AllureStep("Verify membership name in combobox")]
        public MembershipAdmin VerifyMembershipNameCbbx(string membership)
        {
            TextBox.GetText(cbbxMembershipName);
            Assert.AreEqual(membership, cbbxMembershipName.Text, $"Membership '{membership}' is not found");
            
            return this;
        }

        [AllureStep("Verify that assign user is displayed in table")]
        public MembershipAdmin VerifyAssignUser()
        {
            WaitUntil.WaitForElementToAppear(emailColumn, 60);
            Assert.AreEqual("qatester92311@xitroo.com", emailColumn.Text);
            return this;
        }
        
    }
}
