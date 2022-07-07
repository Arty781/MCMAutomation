﻿using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.Helpers
{
    public class Button
    {
        public static void Click(IWebElement element)
        {
            try
            {
                WaitUntil.WaitSomeInterval(250);
                WaitUntil.CustomElevemtIsVisible(element, 20);

                element.Click();
            }
            catch (InvalidElementStateException) { }
            
        }

        public static void ScrollTo(int xPosition, int yPosition)
        {
            try
            {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Browser._Driver;
            js.ExecuteScript("window.scrollTo({0}, {1})", xPosition, yPosition);
            }
            catch (Exception) { }
        }


    }

    public class InputBox
    {
        public static IWebElement Element(IWebElement element, int seconds, string data)
        {
            WaitUntil.CustomElevemtIsVisible(element, seconds);
            try 
            {
                
                element.SendKeys(Keys.Control + "A" + Keys.Delete);
                WaitUntil.WaitSomeInterval(200);
                element.SendKeys(data);
            }
            catch(Exception) { }


            return element;
        }
        public static IWebElement CbbxElement(IWebElement element, int seconds, string data)
        {
            WaitUntil.CustomElevemtIsVisible(element, seconds);
            try
            {
                element.SendKeys(data);
            }
            catch (Exception) { }


            return element;
        }

       
    }
    public class TextBox
    {
        public static string GetText(IWebElement element, int seconds = 30)
        {
            WaitUntil.CustomElevemtIsVisible(element, seconds);

            return element.Text;
        }

        public static string GetAttribute(IWebElement element, string attribute, int seconds = 30)
        {
            WaitUntil.CustomElevemtIsVisible(element, seconds);

            return element.GetAttribute(attribute);
        }
    }

    public class RemoveBtn
    {
        public static void ClickRemoveExerciseBtn(string name, int seconds = 10)
        {
            WaitUntil.WaitSomeInterval(2500);

            IWebElement btnRemove = Browser._Driver.FindElement(By.XPath($"//p[text()='{name}']/parent::div/following::div/div[@class='delete']"));
            WaitUntil.CustomElevemtIsVisible(btnRemove, seconds);

            btnRemove.Click();
        }

        
    }

    public class Membership
    {
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
    }
}
