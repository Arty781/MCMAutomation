using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.PageObjects.ClientSitePages
{
    public partial class UserProfile
    {
        [FindsBy(How = How.XPath, Using = "//input[@placeholder='First Name']")]
        public IWebElement inputFirstName;

        [FindsBy(How = How.XPath, Using = "//input[@placeholder='Last Name']")]
        public IWebElement inputLastName;

        [FindsBy(How = How.XPath, Using = "//input[@placeholder='Birth Date']")]
        public IWebElement inputBirthDate;

        [FindsBy(How = How.Name, Using = "calories")]
        public IWebElement inputCalories;

        [FindsBy(How = How.Name, Using = "maintenanceCalories")]
        public IWebElement inputMaintenanceCalories;

        [FindsBy(How = How.Name, Using = "protein")]
        public IWebElement inputProtein;

        [FindsBy(How = How.Name, Using = "carbs")]
        public IWebElement inputCarbs;

        [FindsBy(How = How.Name, Using = "fats")]
        public IWebElement inputFats;

        [FindsBy(How = How.Name, Using = "photo")]
        public IWebElement inputPhoto;

        [FindsBy(How = How.Name, Using = "height")]
        public IWebElement inputHeight;

        [FindsBy(How=How.XPath,Using = "//div[contains(@class,'swiper-slide-prev')]")]
        public IWebElement itemHeightPrev;

        [FindsBy(How = How.XPath, Using = "//div[contains(@class,'swiper-slide-active')]")]
        public IWebElement itemHeightActive;

        [FindsBy(How = How.XPath, Using = "//div[contains(@class,'swiper-slide-next')]")]
        public IWebElement itemHeightNext;

        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'Ok')]")]
        public IWebElement btnOk;

        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'Cancel')]")]
        public IWebElement btnCancel;

        [FindsBy(How = How.Name, Using = "weight")]
        public IWebElement inputWeight;

        [FindsBy(How = How.XPath, Using = "//span[contains(@class,'ant-select-selection')]/parent::div//input")]
        public IWebElement inputActivityLevel;

        [FindsBy(How = How.Name, Using = "bodyfat")]
        public IWebElement inputBodyfat;

        [FindsBy(How = How.Name, Using = "email")]
        public IWebElement inputEmail;

        [FindsBy(How = How.Name, Using = "oldPassword")]
        public IWebElement inputOldPassword;

        [FindsBy(How = How.Name, Using = "changePassword")]
        public IWebElement inputChangePassword;

        [FindsBy(How = How.Name, Using = "confirmPassword")]
        public IWebElement inputConfirmPassword;
    }
}
