using Allure.Commons;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.Helpers
{
    class AllureServe
    {
        [AllureTag("Regression")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureOwner("Sukharevsky Artem")]
        [AllureSuite("Report")]
        [AllureSubSuite("AllureReport")]
        [Test]

        
        public void GoToAllureResults()
        {
            AllureConfigFilesHelper.CreateBatFile();
            WaitUntil.WaitSomeInterval(1000);
            Process.Start(Browser.RootPathReport() + "allure serve.bat");
        }
        
    }

    public static class ForceCloseWebDriver
    {
        [AllureSeverity(SeverityLevel.critical)]
        [AllureOwner("Sukharevsky Artem")]
        [AllureSuite("DriverLevel")]
        [AllureSubSuite("ForceCloseDriver")]
        [Test]


        public static void ForceClose()
        {
            ForceCloseDriver.CreateBatFile();
            WaitUntil.WaitSomeInterval(1000);
            Process.Start(Browser.RootPathReport() + "_!CloseOpenWith.bat");
        }

        public static void RemoveBatFile()
        {
            string path = Browser.RootPathReport() + "_!CloseOpenWith.bat";
            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists == true)
            {
                fileInf.Delete();
            }
        }
    }
}
