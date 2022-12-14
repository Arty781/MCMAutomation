using MCMAutomation.APIHelpers;
using MCMAutomation.APIHelpers.Client.EditUser;
using MCMAutomation.APIHelpers.Client.SignUp;
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
        public void MakeSignIn()
        {
            var responseLogin = SignInRequest.MakeAdminSignIn(Credentials.loginAdmin, Credentials.passwordAdmin);
            SignInAssertions
                .VerifyIsAdminSignInSuccesfull(responseLogin);
        }

        [Test]
        //[Repeat(4)]
        public void Demo()
        {
            var responseLogin = SignInRequest.MakeAdminSignIn(Credentials.loginAdmin, Credentials.passwordAdmin);
            MembershipsWithUsersRequest.CreateMembership(responseLogin);
            int memberId = int.Parse(AppDbContext.GetLastMembership().FirstOrDefault());
            for(int i =0; i<3; i++)
            {
                MembershipsWithUsersRequest.CreatePrograms(responseLogin, memberId);
            }
            List<int> programs = AppDbContext.GetLastPrograms();
            foreach(var program in programs)
            {
                MembershipsWithUsersRequest.CreateWorkouts(responseLogin, program);
                MembershipsWithUsersRequest.CreateWorkouts(responseLogin, program);
                MembershipsWithUsersRequest.CreateWorkouts(responseLogin, program);
            }
            
        }
    }
}
