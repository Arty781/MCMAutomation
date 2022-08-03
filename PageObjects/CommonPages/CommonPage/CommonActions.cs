using MCMAutomation.Helpers;
using NUnit.Allure.Steps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.PageObjects
{
    public partial class Common
    {
        [AllureStep("Click \"Save\" button")]
        public Common ClickSaveBtn()
        {
            Button.Click(saveBtn);
            return this;
        }



    }
}
