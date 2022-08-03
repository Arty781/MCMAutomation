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
            var count = selectPhaseBtn.Where(x=>x.Displayed).Count();

            return count;
        }

        [AllureStep("Get Workouts count")]
        public int GetWorkoutsCount()
        {
            var count = workoutBtn.Where(x => x.Displayed).Count();

            return count;
        }

        [AllureStep("Verify added weight")]
        public void VerifyAddedWeight(List<string> addedWeight)
        {
            WaitUntil.CustomElevemtIsVisible(inputAddedWeightElem);
            List<string> list = new List<string>();
            var addedWeightList = inputAddedWeight.Where(x => x.Enabled).ToList();
            foreach (var weight in addedWeightList)
            {
                list.Add(weight.Text);
            }
            List<string> list1 = list.Except(addedWeight).ToList();
            var list2 = addedWeight.Except(list).ToList();
            Console.WriteLine("List 1: " + list1.Count);
            Console.WriteLine("List 2: " + list2.Count);
            Assert.IsTrue(list1.Any() == list2.Any());
        }
    }
}
