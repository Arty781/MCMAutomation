using MCMAutomation.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMClientTests.BASE
{
    public class TestBaseClient : BaseWeb
    {

        [SetUp]

        public void SetUp()
        {

            Browser._Driver.Navigate().GoToUrl(Endpoints.websiteHost);
        }

    }
    
}
