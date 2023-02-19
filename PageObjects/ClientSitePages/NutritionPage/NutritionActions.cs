using MCMAutomation.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using RimuTec.Faker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.PageObjects.ClientSitePages
{
    public partial class Nutrition
    {
        #region FINDING YOUR ESTIMATED TDEE page
        public Nutrition ClickCalculateBtn()
        {

            Button.Click(btnCalculate);
            WaitUntil.WaitSomeInterval(250);

            return this;
        }

        public Nutrition SelectActivityLevel(int levelNumber)
        {
            if (levelNumber == 0)
            {
                WaitUntil.WaitSomeInterval(250);
                Button.Click(cbbxActivitylevel);
                Button.Click(listActivityLevel[levelNumber]);
            }
            else if (levelNumber == 1)
            {
                WaitUntil.WaitSomeInterval(250);
                Button.Click(cbbxActivitylevel);
                Button.Click(listActivityLevel[levelNumber]);
            }
            else if (levelNumber == 2)
            {
                WaitUntil.WaitSomeInterval(250);
                Button.Click(cbbxActivitylevel);
                Button.Click(listActivityLevel[levelNumber]);
            }
            else if (levelNumber == 3)
            {
                WaitUntil.WaitSomeInterval(250);
                Button.Click(cbbxActivitylevel);
                Button.Click(listActivityLevel[levelNumber]);
            }
            else if (levelNumber == 4)
            {
                WaitUntil.WaitSomeInterval(250);
                Button.Click(cbbxActivitylevel);
                Button.Click(listActivityLevel[levelNumber]);
            }

            WaitUntil.WaitSomeInterval(3000);
            return this;
        }

        public Nutrition ClickNextBtn()
        {
            Button.Click(btnNext);
            WaitUntil.WaitSomeInterval(250);
            return this;
        }

        public Nutrition SelectMale()
        {
            Button.Click(SwitcherHelper.NutritionSelector("Gender")[0]);
            WaitUntil.WaitSomeInterval(250);

            return this;
        }

        public Nutrition SelectFemale()
        {
            Button.Click(SwitcherHelper.NutritionSelector("Gender")[1]);
            WaitUntil.WaitSomeInterval(250);

            return this;
        }

        public Nutrition SelectImperial()
        {
            Button.Click(SwitcherHelper.NutritionSelector("Preferred Conversion System")[0]);
            WaitUntil.WaitSomeInterval(250);

            return this;
        }

        public Nutrition SelectMetric()
        {
            Button.Click(SwitcherHelper.NutritionSelector("Preferred Conversion System")[1]);
            WaitUntil.WaitSomeInterval(250);

            return this;
        }

        public Nutrition SelectYesOfAdditionalOptions(string title)
        {
            var togglesList = Element.FindElementsByXpath($"//label[@title='{title}']/ancestor::div[@class='ant-row ant-form-item radio']//div[contains(@class,'ant-radio-group')]//label");
            Button.Click(togglesList.FirstOrDefault());
            WaitUntil.WaitSomeInterval(250);

            return this;
        }

        public Nutrition SelectNoOfAdditionalOptions(string title)
        {
            var togglesList = Element.FindElementsByXpath($"//label[@title='{title}']/ancestor::div[@class='ant-row ant-form-item radio']//div[contains(@class,'ant-radio-group')]//label");
            Button.Click(togglesList.LastOrDefault());
            WaitUntil.WaitSomeInterval(250);

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
            WaitUntil.CustomElevemtIsVisible(itemHeightActive);
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

        public Nutrition Step02SelectCut()
        {
            WaitUntil.CustomElevemtIsVisible(btnCut);
            btnCut.Click();
            btnCut.Click();
            WaitUntil.WaitSomeInterval(250);
            return this;
        }

        public Nutrition Step02SelectMainTain()
        {
            try
            {
                if (btnCut.Displayed == true)
                {
                    WaitUntil.CustomElevemtIsVisible(btnMaintain);
                    btnMaintain.Click();
                    WaitUntil.WaitSomeInterval(250);
                }
            }
            catch (NoSuchElementException)
            {
                WaitUntil.CustomElevemtIsVisible(btnMaintain);
                btnMaintain.Click();
                btnMaintain.Click();
                WaitUntil.WaitSomeInterval(250);
            }
            

            return this;
        }

        public Nutrition Step02SelectBuild()
        {
            WaitUntil.CustomElevemtIsVisible(btnBuild);
            btnBuild.Click();
            WaitUntil.WaitSomeInterval(250);
            return this;
        }
        public Nutrition Step02SelectReverse()
        {
            WaitUntil.CustomElevemtIsVisible(btnReverse);
            btnReverse.Click();
            WaitUntil.WaitSomeInterval(250);
            return this;
        }
        #endregion

        #region Step03

        public Nutrition Step03SelectTier1()
        {
            WaitUntil.CustomElevemtIsVisible(btnTier1);
            btnTier1.Click();
            btnTier1.Click();
            WaitUntil.WaitSomeInterval(250);
            return this;
        }

        public Nutrition Step03SelectTier2()
        {
            WaitUntil.CustomElevemtIsVisible(btnTier2);
            btnTier2.Click();
            WaitUntil.WaitSomeInterval(250);
            return this;
        }

        public Nutrition Step03SelectTier3()
        {
            WaitUntil.CustomElevemtIsVisible(btnTier3);
            btnTier3.Click();
            WaitUntil.WaitSomeInterval(250);
            return this;
        }

        #endregion

        #region Step04

        public Nutrition Step04SelectPhase1()
        {
            WaitUntil.CustomElevemtIsVisible(btnPhase1);
            btnPhase1.Click();
            btnPhase1.Click();
            WaitUntil.WaitSomeInterval(250);
            return this;
        }

        public Nutrition Step04SelectPhase2()
        {
            WaitUntil.CustomElevemtIsVisible(btnPhase2);
            btnPhase2.Click();
            WaitUntil.WaitSomeInterval(250);
            return this;
        }

        public Nutrition Step04SelectPhase3()
        {
            WaitUntil.CustomElevemtIsVisible(btnPhase3);
            btnPhase3.Click();
            WaitUntil.WaitSomeInterval(250);
            return this;
        }

        #endregion

        #region Step05

        public Nutrition Step05SelectDiet1()
        {
            WaitUntil.CustomElevemtIsVisible(btnDiet1);
            btnDiet1.Click();
            btnDiet1.Click();
            WaitUntil.WaitSomeInterval(250);
            return this;
        }

        public Nutrition Step05SelectDiet2()
        {
            WaitUntil.CustomElevemtIsVisible(btnDiet2);
            btnDiet2.Click();
            WaitUntil.WaitSomeInterval(250);
            return this;
        }

        public Nutrition Step05SelectDiet3()
        {
            WaitUntil.CustomElevemtIsVisible(btnDiet3);
            btnDiet3.Click();
            WaitUntil.WaitSomeInterval(250);
            return this;
        }

        #region Step05 for ARD

        public double GetPreviousCalories(double previousCalories, double maintanceCalories)
        {
            
            if (inputPrevCalories.GetAttribute("value") == "")
            {
                InputBox.ElementCtrlA(inputPrevCalories, 5, RandomNumber.Next(1000, (int)maintanceCalories).ToString());
                previousCalories = double.Parse(TextBox.GetAttribute(inputPrevCalories, "value"));
                Console.WriteLine("previousCalories are " + previousCalories);
            }
            previousCalories = double.Parse(TextBox.GetAttribute(inputPrevCalories, "value"));
            Console.WriteLine("previousCalories are " + previousCalories);

            return previousCalories;
        }

        public string GetTextOnStep06()
        {
            string selectedText = Browser._Driver.FindElement(By.XPath("//label[contains(@class,'checked')]/span[2]")).Text;

            return selectedText;
        }

        #endregion

        #endregion



    }
}
