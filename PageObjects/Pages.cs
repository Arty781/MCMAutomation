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
        private static readonly IDictionary<Type, object> _pageCache = new Dictionary<Type, object>();

        public static T GetPage<T>() where T : new()
        {
            var type = typeof(T);
            if (!_pageCache.ContainsKey(type))
            {
                var page = new T();
                IWebDriver driver = Browser._Driver;
                PageFactory.InitElements(driver, page);
                _pageCache.Add(type, page);
            }

            return (T)_pageCache[type];
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
