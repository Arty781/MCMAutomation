using NUnit.Allure.Steps;
using OpenQA.Selenium;
using MCMAutomation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SeleniumExtras.PageObjects;

namespace MCMAutomation.PageObjects
{
    public partial class MembershipAdmin
    {
        #region User page

        [FindsBy(How = How.XPath, Using = "//input[@placeholder='Search']")]
        public IWebElement searchInput;

        [FindsBy(How = How.XPath, Using = "//button[contains(@class, 'ant-input-search-button')]")]
        public IWebElement searchBtn;

        [FindsBy(How = How.XPath, Using = "//tr//td[3]")]
        public IWebElement emailRow;

        [FindsBy(How = How.XPath, Using = "//div[@class='edit-btn']")]
        public IWebElement editBtn;





        #endregion

        #region EditUser page

        [FindsBy(How = How.XPath, Using = "//input[@name='firstName']")]
        public IWebElement firstNameInput;

        [FindsBy(How = How.XPath, Using = "//input[@name='lastName']")]
        public IWebElement lastNameInput;

        [FindsBy(How = How.XPath, Using = "//input[@name='email']")]
        public IWebElement emailInput;

        [FindsBy(How = How.XPath, Using = "//input[@name='photo']")]
        public IWebElement photoInput;

        [FindsBy(How = How.XPath, Using = "//input[@type='password']")]
        public IWebElement passwordInput;

        [FindsBy(How = How.XPath, Using = "//div[@class='pwd-container_btn']")]
        public IWebElement passwordBtn;

        [FindsBy(How = How.XPath, Using = "//div[@class='user-membership-info'][1]//input[@type='search']")]
        public IWebElement userStatusCbbx;

        [FindsBy(How = How.XPath, Using = "//div[@class='user-membership-info'][2]//input[@type='search']")]
        public IWebElement userRoleCbbx;

        [FindsBy(How = How.XPath, Using = "//div[@class='user-membership-info'][3]//input[@type='search']")]
        public IWebElement selectUserActiveMembershipCbbx;

        [FindsBy(How = How.XPath, Using = "//div[@class='user-membership-info'][4]//input[@type='search']")]
        public IWebElement addUserMembershipCbbx;

        [FindsBy(How = How.XPath, Using = "//div[@class='add-membership_btn']")]
        public IWebElement addUserMembershipBtn;

        [FindsBy(How = How.XPath, Using = "//div[@class='user-memberships-item']")]
        public IWebElement membershipItem;

        [FindsBy(How=How.XPath,Using = "//p/parent::div[@class='user-memberships-item']/img")]
        public IList<IWebElement> btnDeleteAddedMemberships;




        #endregion



    }
}