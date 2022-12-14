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
    public partial class UsersAdmin
    {
        #region User page

        [FindsBy(How = How.XPath, Using = "//input[@placeholder='Search']")]
        public IWebElement inputSearch;

        [FindsBy(How = How.XPath, Using = "//button[contains(@class, 'ant-input-search-button')]")]
        public IWebElement btnSearch;

        [FindsBy(How = How.XPath, Using = "//tr//td[3]")]
        public IWebElement rowEmail;

        [FindsBy(How = How.XPath, Using = "//div[@class='edit-btn']")]
        public IWebElement btnEdit;





        #endregion

        #region EditUser page

        [FindsBy(How = How.XPath, Using = "//input[@name='firstName']")]
        public IWebElement inputFirstName;

        [FindsBy(How = How.XPath, Using = "//input[@name='lastName']")]
        public IWebElement inputLastName;

        [FindsBy(How = How.XPath, Using = "//input[@name='email']")]
        public IWebElement inputEmail;

        [FindsBy(How = How.XPath, Using = "//input[@name='photo']")]
        public IWebElement inputPhoto;

        [FindsBy(How = How.XPath, Using = "//input[@type='password']")]
        public IWebElement inputPassword;

        [FindsBy(How = How.XPath, Using = "//div[@class='pwd-container_btn']")]
        public IWebElement btnPassword;

        [FindsBy(How = How.XPath, Using = "//p[text()='User Status']/parent::div[@class='user-membership-info']//input[@type='search']")]
        public IWebElement cbbxUserStatus;

        [FindsBy(How = How.XPath, Using = "//p[text()='User Role']/parent::div[@class='user-membership-info']//input[@type='search']")]
        public IWebElement cbbxUserRole;

        [FindsBy(How = How.XPath, Using = "//p[text()='Active Membership']/parent::div[@class='user-membership-info']//input[@type='search']")]
        public IWebElement cbbxSelectUserActiveMembership;

        [FindsBy(How = How.XPath, Using = "//p[text()='Add User to Membership']/parent::div[@class='user-membership-info']//input[@type='search']")]
        public IWebElement cbbxAddUserMembership;

        [FindsBy(How = How.XPath, Using = "//div[@class='add-membership_btn']")]
        public IWebElement btnAddUserMembership;

        [FindsBy(How = How.XPath, Using = "//div[@class='user-memberships-item']/p")]
        public IList<IWebElement> itemMembership;

        [FindsBy(How = How.XPath, Using = "//p[contains(text(),'Active Membership')]/parent::div//div[@class='user-membership-select']//span[@title]")]
        public IWebElement titleActiveMembership;

        [FindsBy(How=How.XPath,Using = "//div[@class='user-memberships-item']/img")]
        public IList<IWebElement> btnDeleteAddedMemberships;

        [FindsBy(How = How.XPath, Using = "//div[@class='user-memberships-item']/img")]
        public IWebElement btnDeleteAddedMembershipsElem;

        [FindsBy(How = How.XPath, Using = "//h2[text()='Custom Programs']/parent::div//div[@class='custom-programs_btn']")]
        public IWebElement btnCreateCustommembership;

        #region ProgramData section

        [FindsBy(How = How.XPath, Using = "//h2[text()='Progress Data']/parent::div//div[@class='your-progress-block_control edit']")]
        public IList<IWebElement> btnEditProgress;

        [FindsBy(How = How.XPath, Using = "//h2[text()='Progress Data']/parent::div//div[@class='your-progress-block_control delete']")]
        public IList<IWebElement> btnDeleteProgress;

        [FindsBy(How = How.XPath, Using = "//h2[text()='Progress Data']/parent::div//div[@class='restore']")]
        public IList<IWebElement> btnRestoreProgress;



        #endregion




        #endregion



    }
}