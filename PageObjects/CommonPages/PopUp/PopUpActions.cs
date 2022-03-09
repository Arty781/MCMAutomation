using MCMAutomation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.PageObjects
{
    public partial class PopUp
    {
        public PopUp ClosePopUp()
        {
            WaitUntil.VisibilityOfAllElementsLocatedBy(_installBtn);
            cancelBtn.Click();

            return this;
        }
    }
}
