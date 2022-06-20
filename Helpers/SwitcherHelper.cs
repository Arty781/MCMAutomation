using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.Helpers
{
    public class SwitcherHelper
    {
        
        private static IWebElement _element;
        public static IList<IWebElement> NutritionSelector(string title)
        {

            WaitUntil.WaitSomeInterval(1500);
            var str = $".//label[@title='{title}']/ancestor::div[@class='ant-row ant-form-item radio']";
            _element = Browser._Driver.FindElement(By.XPath(str));
           

            return _element.FindElements(By.XPath(".//input[@type='radio']/ancestor::label")); ;
        }

        public static string[] GetTexOfSelectedtNutritionSelector(string title)
        {
            var list = new List<string>();
            WaitUntil.WaitSomeInterval(1500);
            var str = $".//label[@title='{title}']/ancestor::div[@class='ant-row ant-form-item radio']";
            _element = Browser._Driver.FindElement(By.XPath(str));
            IWebElement gender = _element.FindElement(By.XPath(".//input[@type='radio']/ancestor::label[contains(@class,'checked')]/span[2]"));

            list.Add(gender.Text);

            string[] gen = list.ToArray();

            return gen;
        }
    }
}
