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
    public partial class MembershipUser
    {
        [AllureStep("Verify is login successfully")]
        public MembershipUser VerifyIsBuyBtnDisplayed()
        {
            WaitUntil.CustomElevemtIsVisible(buyBtn, 20);
            Assert.IsTrue(buyBtn.Displayed);

            return this;
        }

        [AllureStep("Get entered weight")]
        public string[] GetEnteredWeight()
        {
            WaitUntil.CustomElevemtIsVisible(outputWeight[0], 20);

            var firstList = new List<string>();
            foreach(var item in outputWeight)
            {
                firstList.Add(item.Text);
            }
            

            return firstList.ToArray();
        }


        [AllureStep("Verify saving weight")]
        public MembershipUser VerifySavingWeight(string[] firstList)
        {
            WaitUntil.WaitSomeInterval(1000);
            WaitUntil.CustomElevemtIsVisible(outputWeight[0], 20);

            var secondList = new List<string>();
            foreach (var item in outputWeight)
            {
                secondList.Add(item.Text);
            }
            var list = secondList.ToArray();

           var listOne = firstList.Except(list).ToList();
           var listTwo = list.Except(firstList).ToList();


            Assert.IsTrue(!listOne.Any() && !listTwo.Any());
            return this;
        }

        [AllureStep("Get Phases count")]
        public int GetPhasesCount()
        {
            WaitUntil.CustomElevemtIsVisible(selectPhaseBtn[0]);
            var count = selectPhaseBtn.Where(x=>x.Displayed).Count();

            return count;
        }

        [AllureStep("Get Workouts count")]
        public int GetWorkoutsCount()
        {
            WaitUntil.CustomElevemtIsVisible(workoutBtn[0]);
            var count = workoutBtn.Where(x => x.Displayed).Count();

            return count;
        }

        [AllureStep("Verify added weight")]
        public void VerifyAddedWeight(List<string> addedWeight)
        {
            WaitUntil.CustomElevemtIsVisible(inputAddedWeightElem);
            var addedWeightList = inputAddedWeight.Where(x => x.Enabled).Select(x => x.Text).ToList();
            var checkList = addedWeightList.Except(addedWeight).ToList();
            Assert.IsTrue(checkList.Count() == 0);
        }
    }
}
