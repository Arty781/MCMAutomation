using NUnit.Allure.Steps;
using OpenQA.Selenium;
using MCMAutomation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.PageObjects
{
    public partial class Sidebar
    {
        #region Opening sidebar menu's tabs

        public Sidebar OpenMemberShipPage()
        {
            /*WaitUntil.ElementIsInvisible(_membershipCard);*/
            membershipTb.Click();

            return this;
        }

        

        #endregion

       
    }
}
