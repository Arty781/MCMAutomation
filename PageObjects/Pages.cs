﻿using OpenQA.Selenium;
using MCMAutomation.Helpers;
using SeleniumExtras.PageObjects;
using MCMAutomation.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static Membership Membership => GetPage<Membership>();
        public static Login Login => GetPage<Login>();
        public static Sidebar Sidebar => GetPage<Sidebar>();
        public static PopUp PopUp => GetPage<PopUp>();

    }
}
