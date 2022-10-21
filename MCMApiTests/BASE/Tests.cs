using MCMAutomation.APIHelpers;
using MCMAutomation.APIHelpers.SignInPage;
using MCMAutomation.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMApiTests
{
    [TestFixture]
    
    public class Tests
    {
        [Test]
        [Retry(4)]
        public void MakeSignIn()
        {
            var responseLogin = SignInRequest.MakeAdminSignIn(Credentials.loginAdmin, Credentials.passwordAdmin);
            SignInAssertions
                .VerifyIsAdminSignInSuccesfull(responseLogin);
        }

        [Test]
        [Repeat(4)]
        public void Demo()
        {

            var responseLogin = SignInRequest.MakeAdminSignIn(Credentials.loginAdmin, Credentials.passwordAdmin);
            var dateBefore = DateTime.Now;
            MembershipsWithUsersRequest.GetMembershipsWithUsersList(responseLogin);
            var dateAfter = DateTime.Now;
            Console.WriteLine($"Load time is: " + (dateAfter - dateBefore));
        }
    }
}
