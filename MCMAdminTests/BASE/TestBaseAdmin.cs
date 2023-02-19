
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
            Browser.Initialize();
            //Browser._Driver.Navigate().GoToUrl("chrome://settings/accessibility");
            Browser.GoToUrl(Endpoints.WEBSITE_HOST);
        }
        
    }
}
