using OpenQA.Selenium;
using MCMAutomation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.PageObjects
{
    public partial class Sidebar
    {
        #region SideBarMenu Admin Panel
        IWebElement sideBarLogo => Browser._Driver.FindElement(_sideBarLogo);
        public readonly By _sideBarLogo = By.XPath("//img[@class='sidebar-logo-img']");

        IWebElement membershipTb => Browser._Driver.FindElement(_membershipTb);
        public readonly By _membershipTb = By.XPath("//ul/li/a[@href='/admin/memberships']");

        IWebElement exercisesTb => Browser._Driver.FindElement(_exercisesTb);
        public readonly By _exercisesTb = By.XPath("//ul//a[@href='/admin/exercises-database']");


        IWebElement usersTb => Browser._Driver.FindElement(_usersTb);
        public readonly By _usersTb = By.XPath("//ul//a[@href='/admin/users']");

        IWebElement educationTb => Browser._Driver.FindElement(_educationTb);
        public readonly By _educationTb = By.XPath("//ul//a[@href='/admin/pages']");

        IWebElement popUpTb => Browser._Driver.FindElement(_popUpTb);
        public readonly By _popUpTb = By.XPath("//ul//a[@href='/admin/pop-up-managment']");

        IWebElement membershipCard => Browser._Driver.FindElement(_membershipCard);
        public readonly By _membershipCard = By.XPath("//div/a[@href='/admin/memberships']");

        IWebElement exercisesCard => Browser._Driver.FindElement(_exercisesCard);
        public readonly By _exercisesCard = By.XPath("//main//a[@href='/admin/exercises-database']");

        IWebElement usersCard => Browser._Driver.FindElement(_usersCard);
        public readonly By _usersCard = By.XPath("//main//a[@href='/admin/users']");

        IWebElement educationCard => Browser._Driver.FindElement(_educationCard);
        public readonly By _educationCard = By.XPath("//main//a[@href='/admin/pages']");

        IWebElement popUpCard => Browser._Driver.FindElement(_popUpCard);
        public readonly By _popUpCard = By.XPath("//main//a[@href='/admin/pop-up-managment']");

        #endregion

        #region SideBarMenu User Panel

        IWebElement trainingProgramTb => Browser._Driver.FindElement(_trainingProgramTb);
        public readonly By _trainingProgramTb = By.XPath("//ul/li/a[@href='/programs/all']");

        IWebElement nutritionTb => Browser._Driver.FindElement(_nutritionTb);
        public readonly By _nutritionTb = By.XPath("//ul/li/a[@href='/nutrition/finding-tree']");

        IWebElement progressTb => Browser._Driver.FindElement(_progressTb);
        public readonly By _progressTb = By.XPath("//ul/li/a[@href='/progress/all']");

        IWebElement videosTb => Browser._Driver.FindElement(_videosTb);
        public readonly By _videosTb = By.XPath("//ul/li/a[@href='/help-videos']");

        IWebElement myProgramBtn => Browser._Driver.FindElement(_myProgramBtn);
        public readonly By _myProgramBtn = By.XPath("//a[@href='/programs/training-program']");


        #endregion


    }
}
