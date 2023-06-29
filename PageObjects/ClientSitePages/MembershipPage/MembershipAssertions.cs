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
            WaitUntil.WaitForElementToAppear(buyBtn, 20);
            Assert.IsTrue(buyBtn.Displayed);

            return this;
        }

        [AllureStep("Get entered weight")]
        public string[] GetEnteredWeight()
        {
            WaitUntil.WaitForElementToAppear(outputWeight[0], 20);

            var firstList = new List<string>();
            foreach(var item in outputWeight)
            {
                firstList.Add(item.Text);
            }
            

            return firstList.ToArray();
        }


        [AllureStep("Verify saving weight")]
        public MembershipUser VerifySavingWeight(string[] expectedOutput)
        {
            WaitUntil.WaitSomeInterval(1000);
            WaitUntil.WaitForElementToAppear(outputWeight[0], 20);

            var actualOutput = outputWeight.Select(item => item.Text).ToArray();

            // Use CollectionAssert.AreEqual to check if both arrays have the same elements
            CollectionAssert.AreEqual(expectedOutput, actualOutput);

            return this;
        }

        [AllureStep("Get Phases count")]
        public int GetPhasesCount()
        {
            WaitUntil.WaitForElementToAppear(selectPhaseBtn[0]);
            var count = selectPhaseBtn.Where(x=>x.Displayed).Count();

            return count;
        }

        [AllureStep("Get Workouts count")]
        public int GetWorkoutsCount()
        {
            WaitUntil.WaitSomeInterval(1500);
            WaitUntil.WaitForElementToAppear(workoutBtn.FirstOrDefault());
            return workoutBtn.Where(x => x.Enabled).Count();
        }

        [AllureStep("Verify added weight")]
        public MembershipUser VerifyAddedWeights(List<string> expectedWeights)
        {
            if (expectedWeights == null)
            {
                throw new ArgumentNullException(nameof(expectedWeights));
            }

            WaitUntil.WaitForElementToAppear(inputAddedWeightElem);
            List<string> expectedText = expectedWeights;
            List<string> actualText = GetInputWeights();

            Assert.Multiple(() =>
            {
                Assert.That(actualText, Is.EqualTo(expectedText), "Texts don't match");
                Assert.That(expectedText.Count, Is.EqualTo(actualText.Count), "Number of elements doesn't match");

                var mismatchedIndices = expectedText.Select((text, index) => new { text, index })
                    .Where(item => !actualText[item.index].Equals(item.text))
                    .Select(item => item.index)
                    .ToList();

                if (mismatchedIndices.Count > 0)
                {
                    string errorMessage = $"Expected text does not match the actual text at index(es): {string.Join(", ", mismatchedIndices)}";
                    Assert.Fail(errorMessage);
                }
            });
            return this;
        }

        private List<string> GetInputWeights()
        {
            return inputAddedWeight.Where(x => x.Enabled).Select(x => x.Text).ToList();
        }

        [AllureStep("Verify added weight")]
        public void VerifyDisplayedDownloadBtn()
        {
            WaitUntil.WaitForElementToAppear(btnDownloadProgram);
            Assert.IsTrue(btnDownloadProgram.Displayed, "Download button is not displayed");
        }
    }
}
