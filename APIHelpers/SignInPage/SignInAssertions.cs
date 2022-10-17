using MCMAutomation.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.APIHelpers.SignInPage
{
    public class SignInAssertions
    {
        
        public static void VerifyIsAdminSignInSuccesfull(SignInResponseModel response)
        {
            Assert.IsTrue(Credentials.loginAdmin == response.Email);
        }
    }
}
