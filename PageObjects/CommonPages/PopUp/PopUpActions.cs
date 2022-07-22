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
            
            WaitUntil.VisibilityOfAllElementsLocatedBy(_cancelBtn, 60);
            popupYesNoBtnLinq.Click();

            return this;
        }
    }
}
