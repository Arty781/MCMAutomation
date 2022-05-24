using NUnit.Allure.Steps;
using OpenQA.Selenium;
using MCMAutomation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MCMAutomation.PageObjects
{
    public partial class MembershipAdmin
    {
        #region User page
        public IWebElement searchInput => Browser._Driver.FindElement(_searchInput);
        public readonly By _searchInput = By.XPath("//input[@placeholder='Search']");

        public IWebElement searchBtn => Browser._Driver.FindElement(_searchBtn);
        public readonly By _searchBtn = By.XPath("//button[contains(@class, 'ant-input-search-button')]");

        public IWebElement emailRow => Browser._Driver.FindElement(_emailRow);
        public readonly By _emailRow = By.XPath("//tr//td[3]");

        public IWebElement editBtn => Browser._Driver.FindElement(_editBtn);
        public readonly By _editBtn = By.XPath("//div[@class='edit-btn']");

        #endregion
        #region EditUser page

        public IWebElement firstNameInput => Browser._Driver.FindElement(_firstNameInput);
        public readonly By _firstNameInput = By.XPath("//input[@name='firstName']");

        public IWebElement lastNameInput => Browser._Driver.FindElement(_lastNameInput);
        public readonly By _lastNameInput = By.XPath("//input[@name='lastName']");

        public IWebElement emailInput => Browser._Driver.FindElement(_emailInput);
        public readonly By _emailInput = By.XPath("//input[@name='email']");

        public IWebElement photoInput => Browser._Driver.FindElement(_photoInput);
        public readonly By _photoInput = By.XPath("//input[@name='photo']");

        public IWebElement passwordInput => Browser._Driver.FindElement(_passwordInput);
        public readonly By _passwordInput = By.XPath("//input[@type='password']");

        public IWebElement passwordBtn => Browser._Driver.FindElement(_passwordBtn);
        public readonly By _passwordBtn = By.XPath("//div[@class='pwd-container_btn']");

        public IWebElement userStatusCbbx => Browser._Driver.FindElement(_userStatusCbbx);
        public readonly By _userStatusCbbx = By.XPath("//div[@class='user-membership-info'][1]//input[@type='search']");

        public IWebElement userRoleCbbx => Browser._Driver.FindElement(_userRoleCbbx);
        public readonly By _userRoleCbbx = By.XPath("//div[@class='user-membership-info'][2]//input[@type='search']");

        public IWebElement selectUserActiveMembershipCbbx => Browser._Driver.FindElement(_selectUserActiveMembershipCbbx);
        public readonly By _selectUserActiveMembershipCbbx = By.XPath("//div[@class='user-membership-info'][3]//input[@type='search']");

        public IWebElement addUserMembershipCbbx => Browser._Driver.FindElement(_addUserMembershipCbbx);
        public readonly By _addUserMembershipCbbx = By.XPath("//div[@class='user-membership-info'][4]//input[@type='search']");

        public IWebElement addUserMembershipBtn => Browser._Driver.FindElement(_addUserMembershipBtn);
        public readonly By _addUserMembershipBtn = By.XPath("//div[@class='add-membership_btn']");

        public IWebElement membershipItem => Browser._Driver.FindElement(_membershipItem);
        public readonly By _membershipItem = By.XPath("//div[@class='user-memberships-item']");



        #endregion






    }
}