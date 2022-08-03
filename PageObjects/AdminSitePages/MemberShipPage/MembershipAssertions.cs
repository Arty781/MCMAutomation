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
        public string[] GetProgramNames()
        {
            WaitUntil.CustomElevemtIsVisible(nameProgramTitleElem);
            var list = new List<string>();
            
            var programNames = nameProgramTitle.Where(x => x.Displayed).ToList();
            for(int i=0; i<programNames.Count; i++)
            {
                string programName = TextBox.GetText(programNames[i]);
                list.Add(programName);
            }
            
            string[] namesList = list.ToArray();
            return namesList;
        }

        [AllureStep("Verify deleting of programs")]
        public MembershipAdmin VerifyDeletePrograms()
        {
            Assert.Throws<NoSuchElementException>(() => Element.webElem("//div[@class='table-item-name']"));

            return this;
        }

        [AllureStep("Get Workout names")]
        public string[] GetWorkoutNames()
        {
            WaitUntil.CustomElevemtIsVisible(nameWorkoutTitleElem, 30);
            var list = new List<string>();
            var workoutNames = nameWorkoutTitle.Where(x => x.Displayed).ToList();
            for (int i = 0; i < workoutNames.Count; i++)
            {
                string workoutName = TextBox.GetText(workoutNames[i]);
                list.Add(workoutName);
            }

            string[] namesList = list.ToArray();
            return namesList;
        }

        [AllureStep("Verify displaying membership name")]
        public MembershipAdmin VerifyMembershipName(string membership)
        {
            TextBox.GetText(membershipTitle[0]);
            if (membership != membershipTitle[0].Text)
            {
                Console.WriteLine("Membership \"" + membership[0] + "\" is not found");
            }
            

            return this;
        }

        [AllureStep("Verify deleting membership")]
        public MembershipAdmin VerifyDeletingMembership(string membership)
        {
            WaitUntil.CustomElevemtIsVisible(Pages.Common.messageDeleted);

            InputBox.Element(membershipSearchInput,30, membership);
            Assert.AreEqual(false, PresenceOfElement.IsElementPresent(By.Name(membership)));
            

            return this;
        }

        [AllureStep("Verify membership name in combobox")]
        public MembershipAdmin VerifyMembershipNameCbbx(string membership)
        {
            TextBox.GetText(cbbxMembershipName);
            Assert.AreEqual(membership, cbbxMembershipName.Text);
            if (membership != cbbxMembershipName.Text)
            {
                Console.WriteLine("Membership \"" + membership + "\" is not found");
            }


            return this;
        }

        [AllureStep("Verify that assign user is displayed in table")]
        public MembershipAdmin VerifyAssignUser()
        {
            WaitUntil.CustomElevemtIsVisible(emailColumn, 60);
            Assert.AreEqual("qatester92311@xitroo.com", emailColumn.Text);


            return this;
        }
        
    }
}
