using MCMAutomation.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.PageObjects.ClientSitePages
{
    public partial class Nutrition
    {
        public Nutrition ClickCalculateBtn()
        {

            Button.Click(btnCalculate);

            return this;
        }

        public Nutrition SelectActivityLevel(int levelNumber)
        {
            if (levelNumber == 1)
            {
                WaitUntil.WaitSomeInterval(1500);
                cbbxActivitylevel.Click();
                new Actions(Browser._Driver)
                 .SendKeys(Keys.ArrowUp +Keys.ArrowUp +Keys.Enter)
                 .Build()
                 .Perform();
            }
            if (levelNumber == 2)
            {
                WaitUntil.WaitSomeInterval(1500);
                cbbxActivitylevel.Click();
                new Actions(Browser._Driver)
                 .SendKeys(Keys.ArrowUp + Keys.Enter)
                 .Build()
                 .Perform();
            }
            if (levelNumber == 3)
            {
                WaitUntil.WaitSomeInterval(1500);
                cbbxActivitylevel.Click();
                new Actions(Browser._Driver)
                 .SendKeys(Keys.Enter)
                 .Build()
                 .Perform();
            }
            if (levelNumber == 4)
            {
                WaitUntil.WaitSomeInterval(1500);
                cbbxActivitylevel.Click();
                new Actions(Browser._Driver)
                 .SendKeys(Keys.ArrowDown + Keys.Enter)
                 .Build()
                 .Perform();
            }
            if (levelNumber == 5)
            {
                WaitUntil.WaitSomeInterval(1500);
                cbbxActivitylevel.Click();
                new Actions(Browser._Driver)
                 .SendKeys(Keys.ArrowDown + Keys.ArrowDown + Keys.Enter)
                 .Build()
                 .Perform();
            }
            WaitUntil.WaitSomeInterval(3000);
            return this;
        }

        public Nutrition ClickNextBtn()
        {
            Button.Click(btnNext);
            return this;
        }

        public Nutrition SelectMale(IList<IWebElement> genderBtns)
        {
            Button.Click(genderBtns[0]);

            return this;
        }

        public Nutrition SelectFemale(IList<IWebElement> genderBtns)
        {
            Button.Click(genderBtns[1]);

            return this;
        }

        public Nutrition SelectImperial(IList<IWebElement> conversionSystem)
        {
            Button.Click(conversionSystem[0]);

            return this;
        }

        public Nutrition SelectMetric(IList<IWebElement> conversionSystem)
        {
            Button.Click(conversionSystem[1]);

            return this;
        }

        public Nutrition SelectYesOfAdditionalOptions(IList<IWebElement> additionalOptions)
        {
            Button.Click(additionalOptions[0]);

            return this;
        }

        public Nutrition SelectNoOfAdditionalOptions(IList<IWebElement> additionalOptions)
        {
            Button.Click(additionalOptions[1]);

            return this;
        }

        #region Step02

        public Nutrition Step02SelectCut()
        {
            WaitUntil.CustomElevemtIsVisible(btnCut);
            btnCut.Click();
            return this;
        }

        public Nutrition Step02SelectMainTain()
        {
            WaitUntil.CustomElevemtIsVisible(btnMaintain);
            btnMaintain.Click();
            return this;
        }

        public Nutrition Step02SelectBuild()
        {
            WaitUntil.CustomElevemtIsVisible(btnBuild);
            btnBuild.Click();
            return this;
        }
        public Nutrition Step02SelectReverse()
        {
            WaitUntil.CustomElevemtIsVisible(btnReverse);
            btnReverse.Click();
            return this;
        }
        #endregion

        #region Step03

        public Nutrition Step03SelectTier1()
        {
            WaitUntil.CustomElevemtIsVisible(btnTier1);
            btnTier1.Click();
            return this;
        }

        public Nutrition Step03SelectTier2()
        {
            WaitUntil.CustomElevemtIsVisible(btnTier2);
            btnTier2.Click();
            return this;
        }

        public Nutrition Step03SelectTier3()
        {
            WaitUntil.CustomElevemtIsVisible(btnTier3);
            btnTier3.Click();
            return this;
        }

        #endregion

        #region Step04

        public Nutrition Step04SelectPhase1()
        {
            WaitUntil.CustomElevemtIsVisible(btnPhase1);
            btnPhase1.Click();
            return this;
        }

        public Nutrition Step04SelectPhase2()
        {
            WaitUntil.CustomElevemtIsVisible(btnPhase2);
            btnPhase2.Click();
            return this;
        }

        public Nutrition Step04SelectPhase3()
        {
            WaitUntil.CustomElevemtIsVisible(btnPhase3);
            btnPhase3.Click();
            return this;
        }

        #endregion

        #region Step05

        public Nutrition Step05SelectDiet1()
        {
            WaitUntil.CustomElevemtIsVisible(btnDiet1);
            btnDiet1.Click();
            return this;
        }

        public Nutrition Step05SelectDiet2()
        {
            WaitUntil.CustomElevemtIsVisible(btnDiet2);
            btnDiet2.Click();
            return this;
        }

        public Nutrition Step05SelectDiet3()
        {
            WaitUntil.CustomElevemtIsVisible(btnDiet3);
            btnDiet3.Click();
            return this;
        }

        #endregion

        

    }
}
