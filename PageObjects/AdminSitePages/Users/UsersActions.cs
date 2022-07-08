using NUnit.Allure.Steps;
using OpenQA.Selenium;
using MCMAutomation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MCMAutomation.PageObjects
{
    public partial class MembershipAdmin
    {
        [AllureStep("Search User")]

        public MembershipAdmin SearchUser(string email)
        {
            WaitUntil.CustomElevemtIsVisible(searchInput);
            searchInput.SendKeys(email + Keys.Enter);
            return this;
        }

        [AllureStep("Edit User")]

        public MembershipAdmin EditUser(string[] membershipName, string email)
        {
            WaitUntil.CustomElevemtIsVisible(SwitcherHelper.GetTextForUserEmail(email));
            SwitcherHelper.ClickEditUserBtn(email);
            WaitUntil.CustomElevemtIsVisible(emailInput);
            InputBox.CbbxElement(addUserMembershipCbbx, 30, membershipName[0] + Keys.Enter);
            Button.Click(addUserMembershipBtn);

            WaitUntil.CustomElevemtIsVisible(membershipItem, 20);

            selectUserActiveMembershipCbbx.SendKeys(membershipName[0] + Keys.Enter);
            WaitUntil.WaitSomeInterval(2500);

            return this;
        }

        [AllureStep("Delete membership from User")]

        public MembershipAdmin DeleteMemebershipFromUser()
        {
            editBtn.Click();
            WaitUntil.CustomElevemtIsVisible(emailInput);

            WaitUntil.CustomElevemtIsVisible(membershipItem, 20);
            var listAddedmemberships = btnDeleteAddedMemberships.Where(x=>x.Displayed).ToList();
            for(int i = 0; i <= listAddedmemberships.Count; i++)
            {
                
                Button.Click(listAddedmemberships[0]);
                WaitUntil.WaitSomeInterval(1000);
                WaitUntil.CustomElevemtIsVisible(membershipItem, 20);
                listAddedmemberships = btnDeleteAddedMemberships.Where(x => x.Displayed).ToList();

                
            }

            WaitUntil.InvisibilityOfAllElementsLocatedBy(By.XPath("//p/parent::div[@class='user-memberships-item']/img"));
            Assert.Throws<NoSuchElementException>(() => Browser._Driver.FindElement(By.XPath($"//p/parent::div[@class='user-memberships-item']/img")));


            return this;
        }
    }
}