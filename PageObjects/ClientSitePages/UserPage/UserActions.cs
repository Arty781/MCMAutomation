
using MCMAutomation.Helpers;
using NUnit.Allure.Steps;
using OpenQA.Selenium;
using RimuTec.Faker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.PageObjects.ClientSitePages
{
    public partial class UserProfile
    {
        [AllureStep("Add First Name")]
        public UserProfile AddFirstName()
        {
            
            InputBox.ElementCtrlA(inputFirstName, 10, Name.FirstName());

            return this;
        }

        [AllureStep("Add Last Name")]
        public UserProfile AddLastName()
        {

            InputBox.ElementCtrlA(inputLastName, 10, Name.LastName());

            return this;
        }

        [AllureStep("Enter DOB")]
        public UserProfile EnterDOB()
        {

            InputBox.ElementCtrlA(inputBirthDate, 10, RandomHelper.RandomAge() + Keys.Enter);

            return this;
        }

        [AllureStep("Enter Calories")]
        public UserProfile EnterCalories()
        {

            InputBox.ElementCtrlA(inputCalories, 10, RandomHelper.RandomNumber(2000));

            return this;
        }

        [AllureStep("Enter Maintenance Calories")]
        public UserProfile EnterMaintenanceCalories()
        {

            InputBox.ElementCtrlA(inputMaintenanceCalories, 10, RandomHelper.RandomNumber(2000));

            return this;
        }

        [AllureStep("Enter Proteins")]
        public UserProfile EnterProteins()
        {

            InputBox.ElementCtrlA(inputProtein, 10, RandomHelper.RandomNumber(200));

            return this;
        }

        [AllureStep("Enter Carbs")]
        public UserProfile EnterCarbs()
        {

            InputBox.ElementCtrlA(inputCarbs, 10, RandomHelper.RandomNumber(200));

            return this;
        }

        [AllureStep("Enter Fats")]
        public UserProfile EnterFats()
        {

            InputBox.ElementCtrlA(inputFats, 10, "10");

            return this;
        }

        [AllureStep("Enter Height")]
        public UserProfile EnterHeight()
        {

            Button.Click(inputHeight);
            List<string> selectedConversionSystem = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Preferred Conversion System");
            if (selectedConversionSystem[0] == "Imperial")
            {
                string activeElem = itemHeightActive.Text;
                IWebElement heightSlider = Browser._Driver.FindElement(By.XPath("//div[@class='swiper-wrapper']"));
                IList<IWebElement> heightsAfterActive = heightSlider.FindElements(By.XPath(".//div[@class='swiper-slide swiper-slide-active']/following::div[contains(@class,'swiper-slide')]"));
                for (int i = 0; i < heightsAfterActive.Count; i++)
                {
                    WaitUntil.WaitSomeInterval(200);
                    activeElem = itemHeightActive.Text;
                    if(activeElem == "5 ft 9 in")
                    {
                        break;
                    }
                    itemHeightNext.Click();
                }
                if (activeElem != "5 ft 9 in")
                {
                    IList<IWebElement> heightsBeforeActive = heightSlider.FindElements(By.XPath(".//div[@class='swiper-slide swiper-slide-active']/preceding::div[contains(@class,'swiper-slide')]"));
                    for (int i = 0; i < heightsBeforeActive.Count; i++)
                    {
                        WaitUntil.WaitSomeInterval(200);
                        activeElem = itemHeightActive.Text;
                        if (activeElem == "5 ft 9 in")
                        {
                            break;
                        }
                        itemHeightPrev.Click();
                    }
                        
                }

            }
            else if(selectedConversionSystem[0] == "Metric")
            {
                string activeElem = itemHeightActive.Text;
                IWebElement heightSlider = Browser._Driver.FindElement(By.XPath("//div[@class='swiper-wrapper']"));
                IList<IWebElement> heightsAfterActive = heightSlider.FindElements(By.XPath(".//div[@class='swiper-slide swiper-slide-active']/following::div[contains(@class,'swiper-slide')]"));
                for (int i = 0; i < heightsAfterActive.Count; i++)
                {
                    WaitUntil.WaitSomeInterval(200);
                    itemHeightNext.Click();
                    activeElem = itemHeightActive.Text;
                    if (activeElem == "175 cm")
                    {
                        break;
                    }
                }
                if (activeElem != "175 cm")
                {
                    IList<IWebElement> heightsBeforeActive = heightSlider.FindElements(By.XPath(".//div[@class='swiper-slide swiper-slide-active']/preceding::div[contains(@class,'swiper-slide')]"));
                    for (int i = 0; i < heightsBeforeActive.Count; i++)
                    {
                        WaitUntil.WaitSomeInterval(200);
                        itemHeightPrev.Click();
                        activeElem = itemHeightActive.Text;
                        if (activeElem == "175 cm")
                        {
                            break;
                        }
                    }

                }

            }

            btnOk.Click();

            return this;
        }

        [AllureStep("Enter Weight")]
        public UserProfile EnterWeight()
        {

            InputBox.ElementCtrlA(inputWeight, 10, "70");

            return this;
        }

        [AllureStep("Enter Estimated Body Fat (%)")]
        public UserProfile EnterEstimatedBodyFat()
        {
            InputBox.ElementCtrlA(inputBodyfat, 10, "15");

            return this;
        }

        [AllureStep("Enter New Email")]
        public UserProfile EnterNewEmail()
        {

            InputBox.ElementCtrlA(inputEmail, 10, RandomHelper.RandomEmail());

            return this;
        }

        [AllureStep("Enter Old Pass")]
        public UserProfile EnterOldPass()
        {

            InputBox.ElementCtrlA(inputOldPassword, 10, "Qaz11111!");

            return this;
        }

        [AllureStep("Enter New Pass")]
        public UserProfile EnterNewPass()
        {

            InputBox.ElementCtrlA(inputChangePassword, 10, "Qaz11111!");

            return this;
        }

        [AllureStep("Enter Confirm Pass")]
        public UserProfile EnterConfirmPass()
        {

            InputBox.ElementCtrlA(inputConfirmPassword, 10, "Qaz11111!");

            return this;
        }

        
    }
}
