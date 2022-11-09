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
    public partial class UsersAdmin
    {
        [AllureStep("Search User")]

        public UsersAdmin SearchUser(string email)
        {
            WaitUntil.CustomElevemtIsVisible(inputSearch);
            inputSearch.SendKeys(email + Keys.Enter);
            return this;
        }

        [AllureStep("Edit User")]

        public UsersAdmin ClickEditUser(string email)
        {
            WaitUntil.CustomElevemtIsVisible(SwitcherHelper.GetTextForUserEmail(email), 10);
            SwitcherHelper.ClickEditUserBtn(email, inputEmail);

            return this;
        }

        [AllureStep("Add membership to user")]
        public UsersAdmin AddMembershipToUser(string membershipName)
        {
            WaitUntil.CustomElevemtIsInvisible(Pages.Common.loader, 120);
            InputBox.CbbxElement(cbbxAddUserMembership, 30, membershipName);
            WaitUntil.WaitSomeInterval(500);
            Button.Click(btnAddUserMembership);
           
            return this;
        }

        [AllureStep("Add membership to user")]
        public UsersAdmin SelectActiveMembership(string membershipName)
        {
            WaitUntil.CustomElevemtIsInvisible(Pages.Common.loader, 60);
            WaitUntil.CustomElevemtIsVisible(btnDeleteAddedMembershipsElem);
            InputBox.CbbxElement(cbbxSelectUserActiveMembership, 5, membershipName);

            return this;
        }

        [AllureStep("Remove added memberships")]
        public UsersAdmin RemoveAddedMembership()
        {
            WaitUntil.CustomElevemtIsInvisible(Pages.Common.loader, 60);
            try
            {
                if(btnDeleteAddedMembershipsElem.Enabled == true)
                {
                    btnDeleteAddedMembershipsElem.Click();
                    WaitUntil.CustomElevemtIsInvisible(Pages.Common.loader, 60);
                }
                WaitUntil.CustomElevemtIsInvisible(Pages.Common.loader, 60);
            }
            catch (Exception) { return this; } 
            

            return this;
        }

        [AllureStep("Delete Progress")]
        public UsersAdmin DeleteProgressFromUser()
        {
            WaitUntil.CustomElevemtIsInvisible(Pages.Common.loader, 60);
            WaitUntil.WaitSomeInterval(500);
            var progressList = btnDeleteProgress.Where(x => x.Enabled).ToList();
            for (int i = 0; i < progressList.Count; i++)
            {
                WaitUntil.WaitSomeInterval(1000);
                Button.Click(btnDeleteProgress[0]);
                Button.Click(Pages.Common.btnConfirmationYes);
            }

            return this;
        }

        [AllureStep("Delete User")]
        public UsersAdmin DeleteUser(string email)
        {
            WaitUntil.CustomElevemtIsVisible(SwitcherHelper.GetTextForUserEmail(email));
            SwitcherHelper.ClickDeleteUserBtn(email);
            
            WaitUntil.WaitSomeInterval(2500);

            return this;
        }

        [AllureStep("Delete membership from User")]
        public UsersAdmin DeleteMemebershipFromUser(string email)
        {
            WaitUntil.CustomElevemtIsVisible(SwitcherHelper.GetTextForUserEmail(email));
            SwitcherHelper.ClickEditUserBtn(email, inputEmail);
            if (itemMembership.Count >= 1)
            {
                var listOfMemberships = btnDeleteAddedMemberships.Where(x => x.Displayed).ToList();
                for (int i = 0; i < listOfMemberships.Count; i++)
                {
                    WaitUntil.WaitSomeInterval(1000);
                    Button.Click(btnDeleteAddedMemberships[0]);
                    WaitUntil.WaitSomeInterval(250);
                    WaitUntil.CustomElevemtIsInvisible(Pages.Common.loader, 60);
                }
                WaitUntil.CustomElevemtIsInvisible(Pages.Common.loader, 60);
                WaitUntil.CustomElevemtIsVisible(Pages.Common.itemsNoData);
                Assert.IsTrue(Pages.Common.itemsNoData.Text == "No Data");
            }
            return this;
        }
    }
}