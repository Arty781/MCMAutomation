﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.Helpers
{
    public class SwitcherHelper
    {
        #region TDEE actions

        private static IWebElement _element;
        public static IList<IWebElement> NutritionSelector(string title)
        {

            WaitUntil.WaitSomeInterval(1500);
            _element = Browser._Driver.FindElement(By.XPath($".//label[@title='{title}']/ancestor::div[@class='ant-row ant-form-item radio']"));
           

            return _element.FindElements(By.XPath(".//input[@type='radio']/ancestor::label")); 
        }

        public static string[] GetTexOfSelectedtNutritionSelector(string title)
        {
            var list = new List<string>();
            WaitUntil.WaitSomeInterval(1500);
            var str = $".//label[@title='{title}']/ancestor::div[@class='ant-row ant-form-item radio']";
            _element = Browser._Driver.FindElement(By.XPath(str));
            IWebElement element = _element.FindElement(By.XPath(".//input[@type='radio']/ancestor::label[contains(@class,'checked')]/span[2]"));

            list.Add(element.Text);

            string[] selectedElement = list.ToArray();

            return selectedElement;
        }

        public static void SelectNumberOfWeekForARD(string week)
        {
            WaitUntil.WaitSomeInterval(1500);
            IList<IWebElement> btnNumberOfWeek = Browser._Driver.FindElements(By.XPath($"//p[contains(text(), '{week}')]/ancestor::div[contains(@class,'week  ')]"));
            btnNumberOfWeek[0].Click();
        }

        public static string GetTextOfSelectNumberOfWeekForARD()
        {

            string str = Browser._Driver.FindElement(By.XPath("//div[contains(@class,'week  active')]//p[2]")).Text.ToString();

            return str;
        }

        #endregion

        #region User actions
        public static void ClickEditUserBtn(string email)
        {
            WebDriverWait wait = new WebDriverWait(Browser._Driver, TimeSpan.FromSeconds(10));
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
            try
            {
                var list = Browser._Driver.FindElements(By.XPath($".//td[@title='{email}']"));
                wait.Until(e =>
                {
                    
                    foreach (var element in list)
                    {
                        if (!element.Displayed && element.Text == email)
                            return null;
                        else
                            return element;
                    }
                    return Browser._Driver.FindElement(By.XPath($".//td[@title='{email}']"));

                });
            }
            catch (Exception) { }

            _element = Browser._Driver.FindElement(By.XPath($".//td[@title='{email}']/parent::tr/td//div[@class='edit-btn']"));
            _element.Click();

        }

        public static IWebElement GetTextForUserEmail(string email)
        {
            WebDriverWait wait = new WebDriverWait(Browser._Driver, TimeSpan.FromSeconds(10));
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
            try
            {
                var list = Browser._Driver.FindElements(By.XPath($".//td[@title='{email}']"));
                wait.Until(e =>
                {

                    foreach (var element in list)
                    {
                        if (!element.Displayed && element.Text == email)
                            return null;
                        else
                            return element;
                    }
                    return Browser._Driver.FindElement(By.XPath($".//td[@title='{email}']"));

                });
            }
            catch (Exception) { }

            _element = Browser._Driver.FindElement(By.XPath($".//td[@title='{email}']"));


            return _element;
        }

        #endregion

        #region Membership actions
        public static void ClickEditMembershipBtn(string title)
        {
            WaitUntil.WaitSomeInterval(200);

            IWebElement btnEditMember = Browser._Driver.FindElement(By.XPath($"//h2[text()='{title}']/parent::div//div[@class='membership-item_edit']"));
            WaitUntil.CustomElevemtIsVisible(btnEditMember, 60);

            btnEditMember.Click();
        }

        public static void ClickAddUserBtn(string title)
        {
            WaitUntil.WaitSomeInterval(200);

            IWebElement btnAddUsers = Browser._Driver.FindElement(By.XPath($"//h2[text()='{title}']/parent::div//div[@class='membership-item_add-user ']"));
            WaitUntil.CustomElevemtIsVisible(btnAddUsers, 60);

            btnAddUsers.Click();
        }

        public static void ClickAddProgramBtn(string title)
        {
            WaitUntil.WaitSomeInterval(200);

            IWebElement btnAddUsers = Browser._Driver.FindElement(By.XPath($"//h2[text()='{title}']/parent::div//div[@class='membership-item_add']"));
            WaitUntil.CustomElevemtIsVisible(btnAddUsers, 60);

            btnAddUsers.Click();
        }

        public static void ClickDeleteBtn(string title)
        {
            WaitUntil.WaitSomeInterval(200);

            IWebElement btnAddUsers = Browser._Driver.FindElement(By.XPath($"//h2[text()='{title}']/parent::div//div[@class='membership-item_delete']"));
            WaitUntil.CustomElevemtIsVisible(btnAddUsers, 60);

            btnAddUsers.Click();
        }

        #endregion

        #region Exercise actions

        public static void ClickRemoveExerciseBtn(string exercise)
        {
            WaitUntil.CustomElevemtIsVisible(Browser._Driver.FindElement(By.XPath("//div[@class='delete']")));

            IWebElement btnRemove = Browser._Driver.FindElement(By.XPath($"//p[text()='{exercise}']/ancestor::div[@class='table-item']//div[@class='delete']"));
            WaitUntil.CustomElevemtIsVisible(btnRemove);

            btnRemove.Click();
        }

        public static void ClickEditExerciseBtn(string exercise)
        {
            WaitUntil.CustomElevemtIsVisible(Browser._Driver.FindElement(By.XPath("//div[@class='edit']")));

            IWebElement btnRemove = Browser._Driver.FindElement(By.XPath($"//p[text()='{exercise}']/ancestor::div[@class='table-item']//div[@class='edit']"));
            WaitUntil.CustomElevemtIsVisible(btnRemove);

            btnRemove.Click();
        }

        #endregion
    }
}