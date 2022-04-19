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
            AllureConfigFilesHelper.CopyJsonConfigFile();
            new DriverManager().SetUpDriver(new ChromeConfig());
            windowsDriver = new ChromeDriver();
            _Driver.Manage().Cookies.DeleteAllCookies();
            _Driver.Manage().Window.Maximize();
            
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
        /*public static ISearchContext Driver { get { return windowsDriver; } }*/
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
