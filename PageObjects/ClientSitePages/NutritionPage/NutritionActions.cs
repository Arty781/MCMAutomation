using Chilkat;
using MCMAutomation.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using RimuTec.Faker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using static MCMAutomation.Helpers.TDEE;

namespace MCMAutomation.PageObjects.ClientSitePages
{
    public partial class Nutrition
    {
        #region FINDING YOUR ESTIMATED TDEE page

        public string SelectedLevel()
        {
            return Pages.WebPages.Nutrition.cbbxActivitylevel.Text;
        }

        public string Selectedgender()
        {
            return SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
        }
        public Nutrition ClickCalculateBtn(out double maintanceCalories)
        {

            Button.Click(btnCalculate);
            WaitUntil.WaitSomeInterval(250);
            maintanceCalories = GetCalories();

            return this;
        }

        public Nutrition SelectActivityLevel(int levelNumber, out string level)
        {
            WaitUntil.WaitSomeInterval(250);
            Button.Click(cbbxActivitylevel);
            Button.Click(listActivityLevel[levelNumber]);
            WaitUntil.WaitSomeInterval(3000);
            level = Pages.WebPages.Nutrition.cbbxActivitylevel.Text;
            return this;
        }

        public Nutrition ClickNextBtn()
        {
            Button.Click(btnNext);
            WaitUntil.WaitSomeInterval(250);
            return this;
        }

        public Nutrition SelectMale(out string selectedGender)
        {
            Button.Click(SwitcherHelper.GenderSelector().FirstOrDefault());
            WaitUntil.WaitSomeInterval(250);
            selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");

            return this;
        }

        public Nutrition SelectFemale(out string selectedGender)
        {
            Button.Click(SwitcherHelper.GenderSelector().LastOrDefault());
            WaitUntil.WaitSomeInterval(250);
            selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");

            return this;
        }

        public int GetNumberOfWeeks(string email)
        {
            DateTime? endDate = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(email).EndDate;
            DateTime? startDate = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(email).StartDate;

            if (endDate.HasValue && startDate.HasValue)
            {
                TimeSpan duration = endDate.Value - startDate.Value;
                int numberOfWeeks = (int)(duration.TotalDays / 7);
                return numberOfWeeks;
            }

            return 0; // Or any other default value if dates are null
        }


        public Nutrition SelectImperial()
        {
            Button.Click(SwitcherHelper.ConversionSystemSelector().FirstOrDefault());
            WaitUntil.WaitSomeInterval(250);

            return this;
        }

        public Nutrition SelectMetric()
        {
            Button.Click(SwitcherHelper.ConversionSystemSelector().LastOrDefault());
            WaitUntil.WaitSomeInterval(250);

            return this;
        }

        public Nutrition SelectYesOfAdditionalOptions(string title, out string textSelectedAdditionalOptions)
        {
            var togglesList = Element.FindElementsByXpath($"//label[@title='{title}']/ancestor::div[@class='ant-row ant-form-item radio']//div[contains(@class,'ant-radio-group')]//label");
            Button.Click(togglesList.FirstOrDefault());
            WaitUntil.WaitSomeInterval(250);
            textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(title);

            return this;
        }

        public Nutrition SelectNoOfAdditionalOptions(string title, out string textSelectedAdditionalOptions)
        {
            var togglesList = Element.FindElementsByXpath($"//label[@title='{title}']/ancestor::div[@class='ant-row ant-form-item radio']//div[contains(@class,'ant-radio-group')]//label");
            Button.Click(togglesList.LastOrDefault());
            WaitUntil.WaitSomeInterval(250);
            textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(title);

            return this;
        }

        public Nutrition EnterAge(string age)
        {
            if(inputAge.GetAttribute("value") == "")
            {
                InputBox.ElementCtrlA(inputAge, 10, age);
            }
            
            return this;
        }

        public Nutrition SelectHeight()
        {
            string conversionSystem = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Preferred Conversion System");
            string targetHeight = conversionSystem == "Imperial" ? "5 ft 9 in" : "175 cm";

            Button.ClickJS(inputHeight);
            WaitUntil.WaitForElementToAppear(itemHeightActive);
            string activeElem = itemHeightActive.Text;
            var heightsAfterActive = Element.FindElementsByXpath("//div[@class='swiper-wrapper']//div[@class='swiper-slide swiper-slide-active']/following::div[contains(@class,'swiper-slide')]");
            if (activeElem != targetHeight)
            {
                for (int i = 0; i < heightsAfterActive.Count; i++)
                {
                    WaitUntil.WaitSomeInterval(200);
                    activeElem = itemHeightActive.Text;
                    if (activeElem == targetHeight)
                    {
                        break;
                    }
                    itemHeightNext.Click();
                }

                var heightsBeforeActive = Element.FindElementsByXpath("//div[@class='swiper-wrapper']//div[@class='swiper-slide swiper-slide-active']/preceding::div[contains(@class,'swiper-slide')]");
                for (int i = 0; i < heightsBeforeActive.Count; i++)
                {
                    WaitUntil.WaitSomeInterval(200);
                    itemHeightPrev.Click();
                    activeElem = itemHeightActive.Text;
                    if (activeElem == targetHeight)
                    {
                        break;
                    }
                }
            }

            btnOk.Click();

            return this;

        }

        public Nutrition EnterWeight(string weight)
        {
            if(inputWeight.GetAttribute("value") == "")
            {
                InputBox.ElementCtrlA(inputWeight, 10, weight);
            }
            
            return this;
        }

        public Nutrition EnterBodyFat(string fat)
        {
            if(inputBodyFat.GetAttribute("value") == "")
            {
                InputBox.ElementCtrlA(inputBodyFat, 10, fat);
            }
            else if (inputBodyFat.GetAttribute("value") != "")
            {
                InputBox.ElementCtrlA(inputBodyFat, 10, fat);
            }

            return this;
        }

        #endregion

        #region Step02

        public Nutrition Step02SelectCut(out string goal)
        {
            Button.Click(btnCut);
            Button.Click(btnCut);
            WaitUntil.WaitSomeInterval(250);
            goal = Pages.WebPages.Nutrition.textActiveGoal.Text;

            return this;
        }

        public Nutrition Step02SelectMainTain(out string goal)
        {
            try
            {
                if (btnCut.Displayed == true)
                {
                    Button.Click(btnMaintain);
                    WaitUntil.WaitSomeInterval(250);
                }
            }
            catch (NoSuchElementException)
            {
                Button.Click(btnMaintain);
                Button.Click(btnMaintain);
                WaitUntil.WaitSomeInterval(250);
            }
            goal = Pages.WebPages.Nutrition.textActiveGoal.Text;

            return this;
        }

        public Nutrition Step02SelectBuild(out string goal)
        {
            Button.Click(btnBuild);
            WaitUntil.WaitSomeInterval(250);
            goal = Pages.WebPages.Nutrition.textActiveGoal.Text;
            return this;
        }
        public Nutrition Step02SelectReverse(out string goal)
        {
            Button.Click(btnReverse);
            WaitUntil.WaitSomeInterval(250);

            goal = Pages.WebPages.Nutrition.textActiveGoal.Text;
            return this;
        }
        #endregion

        #region Step03

        public Nutrition Step03SelectTier1(out string tier)
        {
            Button.Click(btnTier1);
            Button.Click(btnTier1);
            WaitUntil.WaitSomeInterval(250);
            tier = Pages.WebPages.Nutrition.textActiveTier.Text;
            return this;
        }

        public Nutrition Step03SelectTier2(out string tier)
        {
            Button.Click(btnTier2);
            WaitUntil.WaitSomeInterval(250);
            tier = Pages.WebPages.Nutrition.textActiveTier.Text;
            return this;
        }

        public Nutrition Step03SelectTier3(out string tier)
        {
            Button.Click(btnTier3);
            WaitUntil.WaitSomeInterval(250);
            tier = Pages.WebPages.Nutrition.textActiveTier.Text;
            return this;
        }

        #endregion

        #region Step04

        public Nutrition Step04SelectPhase1(out string phase)
        {
            Button.Click(btnPhase1);
            Button.Click(btnPhase1);
            WaitUntil.WaitSomeInterval(250);
            phase = Pages.WebPages.Nutrition.textActivePhase.Text;
            return this;
        }

        public Nutrition Step04SelectPhase2(out string phase)
        {
            Button.Click(btnPhase2);
            WaitUntil.WaitSomeInterval(250);
            phase = Pages.WebPages.Nutrition.textActivePhase.Text;
            return this;
        }

        public Nutrition Step04SelectPhase3(out string phase)
        {
            Button.Click(btnPhase3);
            WaitUntil.WaitSomeInterval(250);
            phase = Pages.WebPages.Nutrition.textActivePhase.Text;
            return this;
        }

        #endregion

        #region Step05

        public Nutrition Step05SelectDiet1(out string diet)
        {
            Button.Click(btnDiet1);
            Button.Click(btnDiet1);
            WaitUntil.WaitSomeInterval(250);
            diet = Pages.WebPages.Nutrition.textActiveDiet.Text;
            return this;
        }

        public Nutrition Step05SelectDiet2(out string diet)
        {
            Button.Click(btnDiet2);
            WaitUntil.WaitSomeInterval(250);
            diet = Pages.WebPages.Nutrition.textActiveDiet.Text;
            return this;
        }

        public Nutrition Step05SelectDiet3(out string diet)
        {
            Button.Click(btnDiet3);
            WaitUntil.WaitSomeInterval(250);
            diet = Pages.WebPages.Nutrition.textActiveDiet.Text;
            return this;
        }

        #region Step05 for ARD

        public Nutrition GetPreviousCalories(double maintanceCalories, out double previousCalories)
        {
            
            if (inputPrevCalories.GetAttribute("value") == "")
            {
                InputBox.ElementCtrlA(inputPrevCalories, 5, RandomNumber.Next(1000, (int)maintanceCalories).ToString());
                previousCalories = double.Parse(TextBox.GetAttribute(inputPrevCalories, "value"));
                Console.WriteLine("previousCalories are " + previousCalories);
            }
            previousCalories = double.Parse(TextBox.GetAttribute(inputPrevCalories, "value"));
            Console.WriteLine("previousCalories are " + previousCalories);

            return this;
        }

        public Nutrition GetTextOnStep06(out string selectedText)
        {
            WaitUntil.WaitForElementToAppear(Browser._Driver.FindElement(By.XPath("//label[contains(@class,'checked')]/span[2]")));
            selectedText = Browser._Driver.FindElement(By.XPath("//label[contains(@class,'checked')]/span[2]")).Text;

            return this;
        }

        #endregion

        #endregion



    }
}
