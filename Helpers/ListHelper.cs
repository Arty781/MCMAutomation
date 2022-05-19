using MCMAutomation.PageObjects;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.Helpers
{
    public class ListHelper
    {
        public static List<string> DefineProgramList(string programsUrl)
        {
            Browser._Driver.Navigate().GoToUrl(programsUrl);
            Pages.PopUp.ClosePopUp();

            WaitUntil.VisibilityOfAllElementsLocatedBy(By.XPath("//div[@class='membership-item_add add-workout']"), 30);

            IReadOnlyCollection<IWebElement> programList = Browser._Driver.FindElements(By.XPath("//div[@class='membership-item_add add-workout']"));

            List<string> urlList = new List<string>();
            for (int items = 0; items < programList.Count;)
            {
                ++items;
                string addBtn = "//div[@class='table-item'][" + items + "]//div[@class='membership-item_add add-workout']";
                WaitUntil.VisibilityOfAllElementsLocatedBy(By.XPath(addBtn));
                IWebElement AddWorkoutsBtn = Browser._Driver.FindElement(By.XPath(addBtn));
                AddWorkoutsBtn.Click();

                urlList.Add(Browser._Driver.Url);


                WaitUntil.WaitSomeInterval(3);
                WaitUntil.VisibilityOfAllElementsLocatedBy(By.XPath("//div[@class='btn_back']"));
                IWebElement backBtn = Browser._Driver.FindElement(By.XPath("//div[@class='btn_back']"));
                backBtn.Click();
            }
            Console.WriteLine(urlList);
            return urlList;
        }

        public static List<string> DefineWorkoutList(IList<string> workoutLinks)
        {
            List<string> workoutsList = new List<string>();
            foreach (var link in workoutLinks) 
            {
                Browser._Driver.Navigate().GoToUrl(link);
                Pages.PopUp.ClosePopUp();
                WaitUntil.VisibilityOfAllElementsLocatedBy(By.XPath("//div[@class='membership-item_add add-workout']"), 30);

                IReadOnlyCollection<IWebElement> workoutList = Browser._Driver.FindElements(By.XPath("//div[@class='membership-item_add add-workout']"));

                List<string> urlList = new List<string>();
                for (int items = 0; items < workoutList.Count;)
                {
                    ++items;
                    string addBtn = "//div[@class='table-items']/div[" + items + "]//div[@class='membership-item_add add-workout']";
                    WaitUntil.VisibilityOfAllElementsLocatedBy(By.XPath(addBtn));
                    IWebElement AddWorkoutsBtn = Browser._Driver.FindElement(By.XPath(addBtn));
                    AddWorkoutsBtn.Click();

                    urlList.Add(Browser._Driver.Url);


                    WaitUntil.WaitSomeInterval(3);
                    WaitUntil.VisibilityOfAllElementsLocatedBy(By.XPath("//div[@class='btn_back']"));
                    IWebElement backBtn = Browser._Driver.FindElement(By.XPath("//div[@class='btn_back']"));
                    backBtn.Click();
                }

                workoutsList.AddRange(urlList);
                
                
            }
            Console.WriteLine(workoutsList);
            return workoutsList;

        }
    }
}
