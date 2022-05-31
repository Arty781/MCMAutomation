using MCMAutomation.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.PageObjects
{
    public partial class SignUpUser
    {
        public SignUpUser VerifyDisplayingPopUp()
        {
            WaitUntil.VisibilityOfAllElementsLocatedBy(_confirmPopUp, 20);
            Assert.AreEqual("We sent a confirmation link to your email", confirmPopUp.Text);

            return this;
        }
    }
}
