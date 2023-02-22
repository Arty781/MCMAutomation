using OpenQA.Selenium;
using MCMAutomation.Helpers;
using SeleniumExtras.PageObjects;
using MCMAutomation.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCMAutomation.PageObjects.ClientSitePages;
using System.Collections.Concurrent;

namespace MCMAutomation.PageObjects
{
    public static class Pages
    {
        public static T GetPage<T>() where T : new()
        {
            var page = new T();
            IWebDriver driver = Browser._Driver;
            PageFactory.InitElements(driver, page);
            return page;
        }

        public static class CommonPages
        {
            public static Common Common => GetPage<Common>();
            public static Login Login => GetPage<Login>();
            public static Sidebar Sidebar => GetPage<Sidebar>();
            public static PopUp PopUp => GetPage<PopUp>();
        }

        public static class AdminPages
        {
            public static MembershipAdmin MembershipAdmin => GetPage<MembershipAdmin>();
            public static ExercisesAdmin ExercisesAdmin => GetPage<ExercisesAdmin>();
            public static UsersAdmin UsersAdmin => GetPage<UsersAdmin>();
        }

        public static class WebPages
        {
            public static MembershipUser MembershipUser => GetPage<MembershipUser>();
            public static SignUpUser SignUpUser => GetPage<SignUpUser>();
            public static Nutrition Nutrition => GetPage<Nutrition>();
            public static UserProfile UserProfile => GetPage<UserProfile>();
            public static Progress Progress => GetPage<Progress>();
        }
    }


}
