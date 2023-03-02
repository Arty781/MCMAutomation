using OpenQA.Selenium;
using MCMAutomation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumExtras.PageObjects;

namespace MCMAutomation.PageObjects
{
    public partial class Sidebar
    {
        #region SideBarMenu Admin Panel

        [FindsBy(How = How.XPath, Using = "//div[@class='sidebar-logo']")]
        public IWebElement sideBarLogo;

        [FindsBy(How = How.XPath, Using = "//ul/li/a[@href='/admin/memberships']")]
        public IWebElement membershipTb;

        [FindsBy(How = How.XPath, Using = "//ul//a[@href='/admin/exercises-database']")]
        public IWebElement exercisesTb;

        [FindsBy(How = How.XPath, Using = "//ul//a[@href='/admin/users']")]
        public IWebElement usersTb;

        [FindsBy(How = How.XPath, Using = "//ul//a[@href='/admin/pages']")]
        public IWebElement educationTb;

        [FindsBy(How = How.XPath, Using = "//ul//a[@href='/admin/pop-up-managment']")]
        public IWebElement popUpTb;

        [FindsBy(How = How.XPath, Using = "//div/a[@href='/admin/memberships']")]
        public IWebElement membershipCard;

        [FindsBy(How = How.XPath, Using = "//main//a[@href='/admin/exercises-database']")]
        public IWebElement exercisesCard;

        [FindsBy(How = How.XPath, Using = "//main//a[@href='/admin/users']")]
        public IWebElement usersCard;

        [FindsBy(How = How.XPath, Using = "//main//a[@href='/admin/pages']")]
        public IWebElement educationCard;

        [FindsBy(How = How.XPath, Using = "//main//a[@href='/admin/pop-up-managment']")]
        public IWebElement popUpCard;

        #endregion

        #region SideBarMenu User Panel

        [FindsBy(How = How.XPath, Using = "//ul/li/a[@href='/programs/all']")]
        public IWebElement trainingProgramTb;

        [FindsBy(How = How.XPath, Using = "//ul/li/a[@href='/nutrition/finding-tree']")]
        public IWebElement nutritionTb;

        [FindsBy(How = How.XPath, Using = "//ul/li/a[@href='/progress/all']")]
        public IWebElement progressTb;

        [FindsBy(How = How.XPath, Using = "//ul/li/a[@href='/help-videos']")]
        public IWebElement videosTb;

        [FindsBy(How = How.XPath, Using = "//a[@href='/programs/training-program']")]
        public IWebElement myProgramBtn;

        [FindsBy(How = How.XPath, Using = "//h3[@class='sidebar-info_name']")]
        public IWebElement btnUserName;

        [FindsBy(How = How.XPath, Using = "//a[@class='my-account']")]
        public IWebElement btnMyAccount;

        [FindsBy(How=How.XPath,Using = "//div[@class='sidebar-info']//p")]
        public IList<IWebElement> textEmail;

        #endregion


    }
}
