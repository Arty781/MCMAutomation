using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using MCMAutomation.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using System.Drawing;

namespace MCMAutomation.Helpers
{
    public class Browser
    {
        private static IWebDriver driver;

        public static void Initialize()
        {
            AllureConfigFilesHelper.CreateJsonConfigFile();
            new DriverManager().SetUpDriver(new ChromeConfig());
            var options = new ChromeOptions();
            //options.AddArgument("--headless=new");
            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            driver.Manage().Cookies.DeleteAllCookies();
            //driver.Manage().Window.Size = new Size(1900, 990);
            //driver.Manage().Window.Minimize();
            Assert.NotNull(driver);
        }

        public static ISearchContext Driver => driver;
        public static IWebDriver _Driver => driver;
        public static string RootPath()
        {
            return Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\"));
        }

        public static string RootPathReport()
        {
            return Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));
        }

        public static void GoToUrl(string url)
        {
            _Driver.Navigate().GoToUrl(url);
        }

        public static IWebElement FindElement(string by)
        {
            return _Driver.FindElement(By.XPath(by));
        }

        public static void Close()
        {
            _Driver.Close();
        }

        public static void Quit()
        {
            _Driver.Quit();
        }
    }
}
