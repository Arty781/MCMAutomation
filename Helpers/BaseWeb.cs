using System;
using System.Diagnostics;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Allure.Steps;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;

namespace MCMAutomation.Helpers
{

    public class BaseWeb
    {
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
                AllureConfigFilesHelper.RemoveBatFile();
                ForceCloseWebDriver.ForceClose();
                ForceCloseWebDriver.RemoveBatFile();
            }
            
        }

        [TearDown]
        public static void TearDown()
        {

            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {

                TelegramHelper.SendMessage();
                AppDbContext.User.DeleteUser($"qatester{DateTime.Now:yyyy-MM}%@xitroo.com");
                AppDbContext.Memberships.DeleteMembership($"00Created New Membership {DateTime.Now:yyyy-MM}%");
                AppDbContext.Exercises.DeleteExercises($"Test Exercise{DateTime.Now:yyyy-MM}%");
                Browser.Close();
            }
            else if (Browser._Driver != null)
            {
                AppDbContext.User.DeleteUser($"qatester{DateTime.Now:yyyy-MM}%@xitroo.com");
                AppDbContext.Memberships.DeleteMembership($"00Created New Membership {DateTime.Now:yyyy-MM}%");
                AppDbContext.Exercises.DeleteExercises($"Test Exercise{DateTime.Now:yyyy-MM}%");
                Browser.Close();
            }



        }
    }
}
