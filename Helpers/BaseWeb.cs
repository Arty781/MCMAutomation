using System;
using System.Diagnostics;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;

namespace MCMAutomation.Helpers
{

    public class BaseWeb
    {
       
        public static Process? _process;

        [OneTimeSetUp]
        public static void OneTimeSetUp()
        {
            AllureConfigFilesHelper.CreateBatFile();
            
        } 



        [OneTimeTearDown]
        public static void OneTimeTearDown()
        {
            
            if (Browser._Driver != null)
            {
                Browser.Quit();
            }
            
        }

        [TearDown]
        public static void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                _ = TelegramHelper.SendMessage();
                _ = TelegramHelper.SendImage();

                Browser.Close();
            }
        }
    }
}
