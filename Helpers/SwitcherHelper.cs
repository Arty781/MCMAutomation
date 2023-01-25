using MCMAutomation.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.Helpers
{
    public class SwitcherHelper
    {
        #region TDEE actions User

        private static IWebElement _element;
        public static IList<IWebElement> NutritionSelector(string title)
        {
            WaitUntil.WaitSomeInterval(1500);
            return Element.FindElementsByXpath($"//label[@title='{title}']/ancestor::div[@class='ant-row ant-form-item radio']//div[contains(@class,'ant-radio-group')]//label"); 
        }

        public static string GetTexOfSelectedtNutritionSelector(string title)
        {
            WaitUntil.WaitSomeInterval(1500);
            return Element.FindElementByXpath($"//label[@title='{title}']/ancestor::div[@class='ant-row ant-form-item radio']//div[contains(@class,'ant-radio-group')]//label[contains(@class,'checked')]").Text;
        }

        public static void SelectNumberOfWeekForARD(string week)
        {
            WaitUntil.WaitSomeInterval(1500);
            Element.FindElementByXpath($"//p[contains(text(), '{week}')]/ancestor::div[contains(@class,'week  ')]").Click();
        }

        public static string GetTextOfSelectNumberOfWeekForARD()
        {
            return Element.FindElementByXpath("//div[contains(@class,'week  active')]//p[2]").Text;
        }

        #endregion

        #region User actions Admin
        public static void ClickEditUserBtn(string email, IWebElement element)
        {
            WebDriverWait wait = new WebDriverWait(Browser._Driver, TimeSpan.FromSeconds(10));
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
            try
            {
                var list = Browser._Driver.FindElements(By.XPath($".//td[@title='{email}']"));
                wait.Until(e =>
                {
                    try { return Browser._Driver.FindElement(By.XPath($".//td[@title='{email}']")).Enabled; }
                    catch (Exception) { return false; }

                });
            }
            catch (Exception) { }

            _element = Browser._Driver.FindElement(By.XPath($".//td[@title='{email}']/parent::tr/td//div[@class='edit-btn']"));
            _element.Click();
            WaitUntil.LoaderIsInvisible(Pages.Common.loader, 120);
            WaitUntil.CustomElevemtIsVisible(element, 60);

        }

        public static IWebElement GetTextForUserEmail(string email)
        {
            WebDriverWait wait = new WebDriverWait(Browser._Driver, TimeSpan.FromSeconds(10));
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
            try
            {
                wait.Until(e =>
                {
                    try { return Browser._Driver.FindElement(By.XPath($".//td[@title='{email}']")).Enabled; }
                    catch (Exception) { return false; }

                });
            }
            catch (Exception) { }

            _element = Browser._Driver.FindElement(By.XPath($".//td[@title='{email}']"));


            return _element;
        }

        public static void ClickDeleteUserBtn(string email)
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

            _element = Browser._Driver.FindElement(By.XPath($".//td[@title='{email}']/parent::tr/td//div[@class='delete-btn']"));
            _element.Click();
            Button.Click(Pages.Common.btnConfirmationYes);

        }

        #endregion

        #region Membership actions Admin
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
            WaitUntil.CustomElevemtIsVisible(Element.FindElementByXpath($"//h2[text()='{title}']/parent::div//div[@class='membership-item_add']"));

            var btnAddUsers = Element.FindElementByXpath($"//h2[text()='{title}']/parent::div//div[@class='membership-item_add']");
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

        public static void ClickDeleteProgramBtn(string programName)
        {
            WaitUntil.WaitSomeInterval(300);
            IWebElement btnDeleteProgram = Element.FindElementByXpath($"//div[text()='{programName}']/parent::div/child::div/div[@class='delete']");
            WaitUntil.CustomElevemtIsVisible(btnDeleteProgram, 60);
            Button.Click(btnDeleteProgram);
            Button.Click(Pages.Common.btnConfirmationYes);
            WaitUntil.CustomElevemtIsInvisible(Element.FindElementByXpath($"//div[text()='{programName}']"), 30);
        }

        public static void SelectCopyMembership(string membershipName, string currentMembership)
        {
            WaitUntil.WaitSomeInterval(250);
            WaitUntil.CustomElevemtIsVisible(Element.FindElementByXpath("//h4[text()='Copy exercises from']"), 30);
            var membership = Element.FindElementByXpath("//h4[text()='Copy exercises from']/parent::div//h3[text()='Membership']/parent::div//input");
            membership.SendKeys(membershipName + Keys.Enter);
            WaitUntil.WaitSomeInterval(500);
            membership.SendKeys(currentMembership + Keys.Enter);
            WaitUntil.WaitSomeInterval(500);
            membership.SendKeys(membershipName + Keys.Enter);
            WaitUntil.CustomElevemtIsVisible(Browser._Driver.FindElement(By.XPath($"//h3[text()='Membership']/parent::div//span[@title='{membershipName}']")));
        }

        public static void SelectCopyProgram(string programName)
        {
            WaitUntil.WaitSomeInterval(250);
            WaitUntil.CustomElevemtIsVisible(Element.FindElementByXpath("//h4[text()='Copy exercises from']"), 30);
            var program = Element.FindElementByXpath("//h4[text()='Copy exercises from']/parent::div//h3[text()='Program']/parent::div//input");
            program.SendKeys(programName + Keys.Enter);
            WaitUntil.WaitSomeInterval(450);
            WaitUntil.CustomElevemtIsVisible(Element.FindElementByXpath($"//h3[text()='Program']/parent::div//span[@title='{programName}']"));
        }

        public static void SelectCopyWorkout(string workoutName)
        {
            WaitUntil.WaitSomeInterval(250);
            WaitUntil.CustomElevemtIsVisible(Element.FindElementByXpath("//h4[text()='Copy exercises from']"), 30);
            var membership = Element.FindElementByXpath("//h4[text()='Copy exercises from']/parent::div//h3[text()='Workout']/parent::div//input");
            membership.SendKeys(workoutName + Keys.Enter);
            WaitUntil.CustomElevemtIsVisible(Element.FindElementByXpath($"//h3[text()='Workout']/parent::div//span[@title='{workoutName}']"));
        }


        #endregion

        #region Membership actions User

        public static void ActivateMembership (string membershipTitle)
        {
            WaitUntil.WaitSomeInterval(200);

            IWebElement btnSelectProgram = Browser._Driver.FindElement(By.XPath($"//h2[text()='{membershipTitle}']/parent::div//button[@class='ant-btn program-switch-btn']"));
            WaitUntil.CustomElevemtIsVisible(btnSelectProgram, 60);

            btnSelectProgram.Click();
        }

        #endregion

        #region Exercise actions Admin

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

            IWebElement btnEdit = Browser._Driver.FindElement(By.XPath($"//p[text()='{exercise}']/ancestor::div[@class='table-item']//div[@class='edit']"));
            WaitUntil.CustomElevemtIsVisible(btnEdit);

            btnEdit.Click();
        }

        #endregion

        #region Sidebar 

        

        #endregion
    }

}
