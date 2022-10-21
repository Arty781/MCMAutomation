using MCMAutomation.Helpers;
using NUnit.Allure.Steps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.PageObjects
{
    public partial class PopUp
    {
        [AllureStep("Close PopUp")]
        public PopUp ClosePopUp()
        {
            
            Browser._Driver.Navigate().Refresh();
            WaitUntil.VisibilityOfAllElementsLocatedBy(_cancelBtn, 60);
            popupYesNoBtnLinq.Click();
            WaitUntil.WaitSomeInterval(250);

            return this;
        }
    }
}
