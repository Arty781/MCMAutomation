
using NUnit.Allure.Core;
using NUnit.Framework;
using MCMAutomation.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminSiteTests.BASE
{
    
    public class TestBaseAdmin : BaseWeb
    {

        [SetUp]

        public void SetUp()
        {
            AllureConfigFilesHelper.CopyJsonConfigFile();
            Browser._Driver.Navigate().GoToUrl(Endpoints.websiteHost);
        }
        
    }
}
