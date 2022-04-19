using OpenQA.Selenium;
using MCMAutomation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.PageObjects
{
    public partial class MembershipAdmin
    {
        public IWebElement backBtn => Browser._Driver.FindElement(_backBtn);
        public readonly By _backBtn = By.XPath("//div[@class='btn_back']");

        #region Membership Page
        public IWebElement membershipCreateBtn => Browser._Driver.FindElement(_membershipCreateBtn);
        public readonly By _membershipCreateBtn = By.XPath("//a[@href='/admin/create-membership'][contains(text(), 'Create Membership')]");

        public IWebElement membershipSearchInput => Browser._Driver.FindElement(_membershipSearchInput);
        public readonly By _membershipSearchInput = By.XPath("//input[@placeholder='Search']");

        public IWebElement membershipSearchBtn => Browser._Driver.FindElement(_membershipSearchBtn);
        public readonly By _membershipSearchBtn = By.XPath("//button[contains(@class, 'ant-input-search-button')]");

        public IWebElement membershipTitle => Browser._Driver.FindElement(_membershipTitle);
        public readonly By _membershipTitle = By.XPath("//h2[@class='membership-item_title']");

        public IWebElement membershipAddProgramsBtn => Browser._Driver.FindElement(_membershipAddProgramsBtn);
        public readonly By _membershipAddProgramsBtn = By.XPath("//div[@class='membership-item_add']");

        public IWebElement membershipAddUserBtn => Browser._Driver.FindElement(_membershipAddUserBtn);
        public readonly By _membershipAddUserBtn = By.XPath("//div[@class='membership-item_add-user ']");

        public IWebElement membershipEditBtn => Browser._Driver.FindElement(_membershipEditBtn);
        public readonly By _membershipEditBtn = By.XPath("//div[@class='membership-item_edit']");

        public IWebElement membershipDeleteBtn => Browser._Driver.FindElement(_membershipDeleteBtn);
        public readonly By _membershipDeleteBtn = By.XPath("//div[@class='membership-item_delete']");

        public IWebElement membershipConfirmYesBtn => Browser._Driver.FindElement(_membershipConfirmYesBtn);
        public readonly By _membershipConfirmYesBtn = By.XPath("//button/span[contains(text(), 'Yes')]");

        public IWebElement membershipConfirmCancelBtn => Browser._Driver.FindElement(_membershipConfirmCancelBtn);
        public readonly By _membershipConfirmCancelBtn = By.XPath("//button/span[contains(text(), 'Cancel')]");

        #endregion

        #region Create Membership Page

        public IWebElement skuInput => Browser._Driver.FindElement(_skuInput);
        public readonly By _skuInput = By.XPath("//input[@name='sku']");

        public IWebElement nameInput => Browser._Driver.FindElement(_nameInput);
        public readonly By _nameInput = By.XPath("//input[@name='name']");

        public IWebElement descriptionInput => Browser._Driver.FindElement(_descriptionInput);
        public readonly By _descriptionInput = By.XPath("//textarea[@name='description']");

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

        public IWebElement subscriptionToggleType => Browser._Driver.FindElement(_subscriptionToggleType);
        public readonly By _subscriptionToggleType = By.XPath("//label/span[contains(text(), 'Subscription')]");

        public IWebElement productToggleType => Browser._Driver.FindElement(_productToggleType);
        public readonly By _productToggleType = By.XPath("//label/span[contains(text(), 'Product')]");

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

        

        public IWebElement cancelMembershipBtn => Browser._Driver.FindElement(_cancelMembershipBtn);
        public readonly By _cancelMembershipBtn = By.XPath("//button[contains(@class, 'canel')]");


        #endregion

        #region Add User page

        public IWebElement membershipCbbx => Browser._Driver.FindElement(_membershipCbbx);
        public readonly By _membershipCbbx = By.XPath("//input[@id='rc_select_0']");

        public IWebElement userCbbx => Browser._Driver.FindElement(_userCbbx);
        public readonly By _userCbbx = By.XPath("//div[contains(@class, 'select-users')]//input[@type='search']");

        public IWebElement addUserBtn => Browser._Driver.FindElement(_addUserBtn);
        public readonly By _addUserBtn = By.XPath("//div[@class='add-user-btn']");

        public IWebElement searchUserInput => Browser._Driver.FindElement(_searchUserInput);
        public readonly By _searchUserInput = By.XPath("//input[@placeholder='Search']");

        public IWebElement emailColumn => Browser._Driver.FindElement(_emailColumn);
        public readonly By _emailColumn = By.XPath("//tr/td[3]");

        public IWebElement deleteUserBtn => Browser._Driver.FindElement(_deleteUserBtn);
        public readonly By _deleteUserBtn = By.XPath("//div[@class='delete-btn']");







        #endregion

        #region Add Program page

        public IWebElement membershipNameCbbx => Browser._Driver.FindElement(_membershipNameCbbx);
        public readonly By _membershipNameCbbx = By.XPath("//span[@class='ant-select-selection-item']");

        public IWebElement addProgramBtn => Browser._Driver.FindElement(_addProgramBtn);
        public readonly By _addProgramBtn = By.XPath("//div[@class='add-programs-btn']");

        public IWebElement programNameInput => Browser._Driver.FindElement(_programNameInput);
        public readonly By _programNameInput = By.XPath("//input[@name='programName']");

        public IWebElement programNumOfWeeksInput => Browser._Driver.FindElement(_programNumOfWeeksInput);
        public readonly By _programNumOfWeeksInput = By.XPath("//input[@name='numberOfWeeks']");

        public IWebElement programStepsInput => Browser._Driver.FindElement(_programStepsInput);
        public readonly By _programStepsInput = By.XPath("//input[@name='steps']");

        public IWebElement programAvailableDateInput => Browser._Driver.FindElement(_programAvailableDateInput);
        public readonly By _programAvailableDateInput = By.XPath("//form/div/div/div[1]//input[@placeholder]");

        public IWebElement programExpiryDateInput => Browser._Driver.FindElement(_programExpiryDateInput);
        public readonly By _programExpiryDateInput = By.XPath("//form/div/div/div[2]//input[@placeholder]");

        public IWebElement programUploadFileInput => Browser._Driver.FindElement(_programUploadFileInput);
        public readonly By _programUploadFileInput = By.XPath("//input[@type='file']");

        public IWebElement programNameRow => Browser._Driver.FindElement(_programNameRow);
        public readonly By _programNameRow = By.XPath("//div[@class='table-item-name']");

        public IWebElement programAddWorkoutsBtn => Browser._Driver.FindElement(_programAddWorkoutsBtn);
        public readonly By _programAddWorkoutsBtn = By.XPath("//div[contains(@class,'table-item_controls')]/div[contains(@class,'add-workout')]");

        public IWebElement programDeleteBtn => Browser._Driver.FindElement(_programDeleteBtn);
        public readonly By _programDeleteBtn = By.XPath("//div[@class='delete']");


        #endregion

        #region Add Workout page

        public IWebElement addWorkoutBtn => Browser._Driver.FindElement(_addWorkoutBtn);
        public readonly By _addWorkoutBtn = By.XPath("//div[@class='add-workouts-btn']");

        public IWebElement workoutNameInput => Browser._Driver.FindElement(_workoutNameInput);
        public readonly By _workoutNameInput = By.XPath("//input[@name='name']");

        public IWebElement weekDayBtn => Browser._Driver.FindElement(_weekDayBtn);
        public readonly By _weekDayBtn = By.XPath("//div[@class='add-workouts-form_items']//input[@role='combobox']");

        public IWebElement workoutNameRow => Browser._Driver.FindElement(_workoutNameRow);
        public readonly By _workoutNameRow = By.XPath("//p[@class='table-item-name']");

        public IWebElement addExercisesBtn => Browser._Driver.FindElement(_addExercisesBtn);
        public readonly By _addExercisesBtn = By.XPath("//div[@class='membership-item_add add-workout']");

        public IWebElement exerciseDeleteBtn => Browser._Driver.FindElement(_exerciseDeleteBtn);
        public readonly By _exerciseDeleteBtn = By.XPath("//div[@class='delete']");

        



        #endregion

        #region Add Exercise page

        public IWebElement addExerciseBtn => Browser._Driver.FindElement(_addExerciseBtn);
        public readonly By _addExerciseBtn = By.XPath("//div[@class='exercises-btn']");

        public IWebElement seriesExerciseInput => Browser._Driver.FindElement(_seriesInput);
        public readonly By _seriesInput = By.XPath("//input[@name='series']");

        public IWebElement exercisesCbbx => Browser._Driver.FindElement(_exersizeCbbx);
        public readonly By _exersizeCbbx = By.XPath("//input[@id='exercises-form-items_exerciseId']");

        public IWebElement setsExerciseInput => Browser._Driver.FindElement(_setsInput);
        public readonly By _setsInput = By.XPath("//input[@id='exercises-form-items_weekWorkoutExercises_0_sets']");

        public IWebElement repsExerciseInput => Browser._Driver.FindElement(_repsInput);
        public readonly By _repsInput = By.XPath("//input[@id='exercises-form-items_weekWorkoutExercises_0_reps']");

        public IWebElement restExerciseInput => Browser._Driver.FindElement(_restInput);
        public readonly By _restInput = By.XPath("//input[@id='exercises-form-items_weekWorkoutExercises_0_rest']");

        public IWebElement tempoExerciseInput => Browser._Driver.FindElement(_tempoInput);
        public readonly By _tempoInput = By.XPath("//input[@id='exercises-form-items_weekWorkoutExercises_0_tempo']");

        public IWebElement addExerciseRowBtn => Browser._Driver.FindElement(_addRowBtn);
        public readonly By _addRowBtn = By.XPath("//button[contains(@class, 'ex-add-btn')]/span[contains(text(), 'Add row')]");

        public IWebElement deleteExerciseRowBtn => Browser._Driver.FindElement(_deleteRowBtn);
        public readonly By _deleteRowBtn = By.XPath("//button[contains(@class, 'ex-add-btn')]/span[contains(text(), 'Delete row')]");

        public IWebElement notesExerciseInput => Browser._Driver.FindElement(_notesInput);
        public readonly By _notesInput = By.XPath("//input[@id='exercises-form-items_notes']");

        public IWebElement exerciseTitleRow => Browser._Driver.FindElement(_exerciseTitleRow);
        public readonly By _exerciseTitleRow = By.XPath("//p[@class='name']");


        #endregion


        
    }
}
