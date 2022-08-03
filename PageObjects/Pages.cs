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

namespace MCMAutomation.PageObjects
{
    public class Pages
    {

        private static T GetPage<T>() where T : new()
        {
            var page = new T();
            IWebDriver driver = Browser._Driver;
            PageFactory.InitElements(driver, page);

            return page;
        }

        public static Common Common => GetPage<Common>();
        public static MembershipAdmin MembershipAdmin => GetPage<MembershipAdmin>();
        public static MembershipUser MembershipUser => GetPage<MembershipUser>();
        public static Login Login => GetPage<Login>();
        public static Sidebar Sidebar => GetPage<Sidebar>();
        public static PopUp PopUp => GetPage<PopUp>();
        public static SignUpUser SignUpUser => GetPage<SignUpUser>();
        public static ExercisesAdmin ExercisesAdmin => GetPage<ExercisesAdmin>();
        public static Nutrition Nutrition => GetPage<Nutrition>();
        public static UserProfile UserProfile => GetPage<UserProfile>();
        public static Progress Progress => GetPage<Progress>();

    }
}
