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
        [AllureSuite("WebSite")]
        [AllureSubSuite("Client")]
        [Test]

        
        public void GoToAllureResults()
        {
            AllureConfigFilesHelper.CreateBatFile();
            WaitUntil.WaitSomeInterval(1000);
            Process.Start(Browser.RootPathReport() + "allure serve.bat");
        }
        
    }
}
