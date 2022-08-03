﻿using MCMAutomation.APIHelpers;
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
            SignInRequestHelper.MakeSignIn(Credentials.loginAdmin, Credentials.passwordAdmin);
        }
    }
}
