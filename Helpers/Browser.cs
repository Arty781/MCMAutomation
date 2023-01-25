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
        public IWebDriver WindowsDriver { get; set; }
        private static IWebDriver windowsDriver;

        public Browser(IWebDriver windowsDriver)
        {
            WindowsDriver = windowsDriver;
        }

        public static void Initialize()
        {
            AllureConfigFilesHelper.CreateJsonConfigFile();
            new DriverManager().SetUpDriver(new ChromeConfig());
            var options = new ChromeOptions();
            windowsDriver = new ChromeDriver(options);
            windowsDriver.Manage().Window.Maximize();
            //windowsDriver.Manage().Window.Size = new Size(1900, 990);
            //windowsDriver.Manage().Window.Minimize();
            windowsDriver.Manage().Cookies.DeleteAllCookies();
            
            Assert.NotNull(windowsDriver);
            
        }

        public static string RootPath()
        {
            string mainpath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\"));
            return mainpath;
        }

        public static string RootPathReport()
        {
            string mainpath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));
            return mainpath;
        }

        public static void GoToUrl(string url)
        {
            windowsDriver.Navigate().GoToUrl(url);
        }

        public static IWebDriver _Driver { get { return windowsDriver; } }
        public static void Close()
        {
            windowsDriver.Close();
        }
        public static void Quit()
        {
            windowsDriver.Quit();
        }

    }
}
