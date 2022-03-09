using OpenQA.Selenium;
using MCMAutomation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.PageObjects
{
    public partial class Membership
    {
        #region Membership Page
        public IWebElement membershipCreateBtn => Browser._Driver.FindElement(_membershipCreateBtn);
        public readonly By _membershipCreateBtn = By.XPath("//a[@class='create-membership-btn_desctop']");

        public IWebElement membershipSearchInput => Browser._Driver.FindElement(_membershipSearchInput);
        public readonly By _membershipSearchInput = By.XPath("//input[@placeholder='Search']");

        public IWebElement membershipSearchBtn => Browser._Driver.FindElement(_membershipSearchBtn);
        public readonly By _membershipSearchBtn = By.XPath("//button[contains(@class, 'ant-input-search-button')]");

        public IWebElement membershipTitle => Browser._Driver.FindElement(_membershipTitle);
        public readonly By _membershipTitle = By.XPath("//h2[@class='membership-item_title']");

        public IWebElement membershipAddBtn => Browser._Driver.FindElement(_membershipAddBtn);
        public readonly By _membershipAddBtn = By.XPath("//div[@class='membership-item_add']");

        public IWebElement membershipAddUserBtn => Browser._Driver.FindElement(_membershipAddUserBtn);
        public readonly By _membershipAddUserBtn = By.XPath("//div[@class='membership-item_add-user ']");

        public IWebElement membershipEditBtn => Browser._Driver.FindElement(_membershipEditBtn);
        public readonly By _membershipEditBtn = By.XPath("//div[@class='membership-item_edit']");

        public IWebElement membershipDeleteBtn => Browser._Driver.FindElement(_membershipDeleteBtn);
        public readonly By _membershipDeleteBtn = By.XPath("//div[@class='membership-item_delete']");

        #endregion

        #region Create Membership Page

        public IWebElement skuInput => Browser._Driver.FindElement(_skuInput);
        public readonly By _skuInput = By.XPath("//input[@name='sku']");

        public IWebElement nameInput => Browser._Driver.FindElement(_nameInput);
        public readonly By _nameInput = By.XPath("//input[@name='name']");

        public IWebElement descriptionInput => Browser._Driver.FindElement(_descriptionInput);
        public readonly By _descriptionInput = By.XPath("//input[@name='description']");

        public IWebElement startDateInput => Browser._Driver.FindElement(_startDateInput);
        public readonly By _startDateInput = By.XPath("//input[@placeholder='Choose Start Date']");

        public IWebElement endDateInput => Browser._Driver.FindElement(_endDateInput);
        public readonly By _endDateInput = By.XPath("//input[@placeholder='Choose End Date']");

        public IWebElement genderMaleToggle => Browser._Driver.FindElement(_genderMaleToggle);
        public readonly By _genderMaleToggle = By.XPath("//label//span[contains(text(), 'Male')]");

        public IWebElement genderFemaleToggle => Browser._Driver.FindElement(_genderFemaleToggle);
        public readonly By _genderFemaleToggle = By.XPath("//label//span[contains(text(), 'Female')]");

        public IWebElement genderBothToggle => Browser._Driver.FindElement(_genderBothToggle);
        public readonly By _genderBothToggle = By.XPath("//label//span[contains(text(), 'Both')]");

        public IWebElement priceInput => Browser._Driver.FindElement(_priceInput);
        public readonly By _priceInput = By.XPath("//input[@name='price']");

        public IWebElement urlInput => Browser._Driver.FindElement(_urlInput);
        public readonly By _urlInput = By.XPath("//input[@name='url']");

        public IWebElement addPhotoInput => Browser._Driver.FindElement(_addPhotoInput);
        public readonly By _addPhotoInput = By.XPath("//input[@name='image']");

        public IWebElement relatedmemberInput => Browser._Driver.FindElement(_relatedmemberInput);
        public readonly By _relatedmemberInput = By.XPath("//input[@type='search']");

        public IWebElement availableForPurchaseCheckbox => Browser._Driver.FindElement(_availableForPurchaseCheckbox);
        public readonly By _availableForPurchaseCheckbox = By.XPath("//input[@type='checkbox']");

        public IWebElement saveMembershipBtn => Browser._Driver.FindElement(_saveMembershipBtn);
        public readonly By _saveMembershipBtn = By.XPath("//button[@type='submit']");

        public IWebElement cancelMembershipBtn => Browser._Driver.FindElement(_cancelMembershipBtn);
        public readonly By _cancelMembershipBtn = By.XPath("//button[contains(@class, 'canel')]");


        #endregion

    }
}
