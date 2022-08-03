using MCMAutomation.Helpers;
using NUnit.Allure.Steps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.PageObjects.ClientSitePages
{
    public partial class Progress
    {
        [AllureStep("Click Add Progress btn")]
        public Progress ClickAddProgressBtnA()
        {
            Button.Click(btnAddProgressA);
            return this;
        }

        [AllureStep("Click Edit Progress btn")]
        public Progress ClickEditProgressBtnA()
        {
            Button.Click(btnEditProgress[0]);
            return this;
        }

        [AllureStep("Click Add Progress btn")]
        public Progress ClickAddProgressBtnDiv()
        {
           Button.Click(btnAddProgressDiv);
          
            return this;
        }

        [AllureStep("Enter Weight")]
        public Progress EnterWeight()
        {
            InputBox.Element(inputWeight, 10, RandomHelper.RandomNumber(2000));

            return this;
        }

        [AllureStep("Enter Waist")]
        public Progress EnterWaist()
        {
            InputBox.Element(inputWaist, 10, RandomHelper.RandomNumber(400));

            return this;
        }

        [AllureStep("Enter Chest")]
        public Progress EnterChest()
        {
            InputBox.Element(inputChest, 10, RandomHelper.RandomNumber(400));

            return this;
        }

        [AllureStep("Enter Arm")]
        public Progress EnterArm()
        {
            InputBox.Element(inputArm, 10, RandomHelper.RandomNumber(400));

            return this;
        }

        [AllureStep("Enter Hips")]
        public Progress EnterHips()
        {
            InputBox.Element(inputHip, 10, RandomHelper.RandomNumber(400));

            return this;
        }

        [AllureStep("Enter Thigh")]
        public Progress EnterThigh()
        {
            InputBox.Element(inputThigh, 10, RandomHelper.RandomNumber(400));

            return this;
        }

        [AllureStep("Click Submit btn")]
        public Progress ClickSubmitBtn()
        {
            Button.Click(btnSubmit);

            WaitUntil.CustomElevemtIsVisible(titleProgressPage, 60);

            return this;
        }

    }
}
