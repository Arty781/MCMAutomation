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

        [AllureStep("Add images")]
        public Progress AddImages()
        {
            inputUploadPhotoFront.SendKeys(Browser.RootPath() + UploadedImages.CREATE_MEMBER_IMG);
            inputUploadPhotoBack.SendKeys(Browser.RootPath() + UploadedImages.PHASE_IMG_1);
            inputUploadPhotoSide.SendKeys(Browser.RootPath() + UploadedImages.PHASE_IMG_2);

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
            InputBox.ElementCtrlA(inputWeight, 10, RandomHelper.RandomProgressData(ProgressBodyPart.WEIGHT).ToString());

            return this;
        }

        [AllureStep("Enter Waist")]
        public Progress EnterWaist()
        {
            InputBox.ElementCtrlA(inputWaist, 10, RandomHelper.RandomProgressData(ProgressBodyPart.WAIST).ToString());

            return this;
        }

        [AllureStep("Enter Chest")]
        public Progress EnterChest()
        {
            InputBox.ElementCtrlA(inputChest, 10, RandomHelper.RandomProgressData(ProgressBodyPart.CHEST).ToString());

            return this;
        }

        [AllureStep("Enter Arm")]
        public Progress EnterArm()
        {
            InputBox.ElementCtrlA(inputArm, 10, RandomHelper.RandomProgressData(ProgressBodyPart.ARM).ToString());

            return this;
        }

        [AllureStep("Enter Hips")]
        public Progress EnterHips()
        {
            InputBox.ElementCtrlA(inputHip, 10, RandomHelper.RandomProgressData(ProgressBodyPart.HIP).ToString());

            return this;
        }

        [AllureStep("Enter Thigh")]
        public Progress EnterThigh()
        {
            InputBox.ElementCtrlA(inputThigh, 10, RandomHelper.RandomProgressData(ProgressBodyPart.THIGH).ToString());

            return this;
        }

        [AllureStep("Click Submit btn")]
        public Progress ClickSubmitBtn()
        {
            Button.Click(btnSubmit);

            WaitUntil.WaitForElementToAppear(titleProgressPage, 60);

            return this;
        }

    }
}
