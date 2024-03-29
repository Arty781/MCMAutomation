﻿using OpenQA.Selenium;
using MCMAutomation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumExtras.PageObjects;

namespace MCMAutomation.PageObjects
{
    public partial class MembershipAdmin
    {
        [FindsBy(How = How.XPath, Using = "//div[@class='btn_back']")]
        public IWebElement backBtn;

        #region Membership Page

        [FindsBy(How = How.XPath, Using = "//a[text()='Create Membership']")]
        public IWebElement btnCreateMembership;

        [FindsBy(How = How.XPath, Using = "//input[@placeholder='Search']")]
        public IWebElement membershipSearchInput;

        [FindsBy(How = How.XPath, Using = "//button[contains(@class, 'ant-input-search-button')]")]
        public IWebElement membershipSearchBtn;

        [FindsBy(How = How.XPath, Using = "//h2[@class='membership-item_title']")]
        public IList<IWebElement> membershipTitle;

        [FindsBy(How = How.XPath, Using = "//h2[@class='membership-item_title']")]
        public IWebElement membershipTitleElem;

        [FindsBy(How = How.XPath, Using = "//div[@class='membership-item_add']")]
        public IWebElement membershipAddProgramsBtn;

        [FindsBy(How = How.XPath, Using = "//div[@class='membership-item_add-user ']")]
        public IWebElement membershipAddUserBtn;

        [FindsBy(How = How.XPath, Using = "//div[@class='membership-item_edit']")]
        public IWebElement membershipEditBtn;

        [FindsBy(How = How.XPath, Using = "//div[@class='membership-item_delete']")]
        public IWebElement membershipDeleteBtn;

        [FindsBy(How = How.XPath, Using = "//button/span[text()='Yes']")]
        public IWebElement membershipConfirmYesBtn;

        [FindsBy(How = How.XPath, Using = "//button/span[text()='Cancel']")]
        public IWebElement membershipConfirmCancelBtn;


        #endregion

        #region Create Membership Page

        [FindsBy(How = How.XPath, Using = "//input[@name='sku']")]
        public IWebElement skuInput;

        [FindsBy(How = How.XPath, Using = "//input[@name='name']")]
        public IWebElement membershipNameInput;

        [FindsBy(How = How.XPath, Using = "//div[@class='jodit-wysiwyg']")]
        public IWebElement membershipDescriptionInput;

        [FindsBy(How = How.Name, Using = "accessWeekLength")]
        public IWebElement inputAccessWeek;

        [FindsBy(How = How.XPath, Using = "//input[@placeholder='Choose Start Date']")]
        public IWebElement startDateInput;

        [FindsBy(How = How.XPath, Using = "//input[@placeholder='Choose End Date']")]
        public IWebElement endDateInput;

        [FindsBy(How = How.XPath, Using = "//label//span[text()='Male']")]
        public IWebElement genderMaleToggle;

        [FindsBy(How = How.XPath, Using = "//label//span[text()='Female']")]
        public IWebElement genderFemaleToggle;

        [FindsBy(How = How.XPath, Using = "//label//span[text()='Both']")]
        public IWebElement genderBothToggle;

        [FindsBy(How = How.XPath, Using = "//label/span[text()='Subscription']")]
        public IWebElement toggleSubscriptionType;

        [FindsBy(How = How.XPath, Using = "//label/span[text()='Product']")]
        public IWebElement toggleProductType;

        [FindsBy(How = How.XPath, Using = "//label/span[text()='Multilevel']")]
        public IWebElement toggleMultilevelType;

        [FindsBy(How = How.XPath, Using = "//input[@name='price']")]
        public IWebElement priceInput;

        [FindsBy(How = How.XPath, Using = "//input[@name='url']")]
        public IWebElement urlInput;

        [FindsBy(How = How.XPath, Using = "//input[@name='image']")]
        public IWebElement addPhotoInput;

        [FindsBy(How = How.XPath, Using = "//input[@type='search']")]
        public IWebElement relatedmemberInput;

        [FindsBy(How = How.XPath, Using = "//input[@type='checkbox']")]
        public IWebElement availableForPurchaseCheckbox;

        public IWebElement availableForPurchaseCheckboxElem => Browser._Driver.FindElement(By.XPath("//input[@type='checkbox']"));

        [FindsBy(How = How.XPath, Using = "//button[contains(@class, 'canel')]")]
        public IWebElement cancelMembershipBtn;

        [FindsBy(How = How.XPath, Using = "//button[text()='Add Membership Level']")]
        public IWebElement btnAddLevel;

        [FindsBy(How = How.XPath, Using = "//div[text()='Level Name']/parent::div/input")]
        public IWebElement inputLevelName;

        [FindsBy(How = How.XPath, Using = "//div[text()='Level Membership']/parent::div//input")]
        public IWebElement inputLevelMembership;

        [FindsBy(How = How.XPath, Using = "//div[contains(@class, 'ant-select-item ant-select-item-option')]")]
        public IList<IWebElement> listLevelMembership;

        [FindsBy(How = How.XPath, Using = "//span[text()='Save']/parent::button")]
        public IWebElement btnSaveLevel;

        #endregion

        #region Add User page

        [FindsBy(How = How.XPath, Using = "//input[@id='rc_select_0']")]
        public IWebElement membershipCbbx;

        [FindsBy(How = How.XPath, Using = "//div[contains(@class, 'select-users')]//input[@type='search']")]
        public IWebElement userCbbx;

        [FindsBy(How = How.XPath, Using = "//div[@class='add-user-btn']")]
        public IWebElement addUserBtn;

        [FindsBy(How = How.XPath, Using = "//input[@placeholder='Search']")]
        public IWebElement searchUserInput;

        [FindsBy(How = How.XPath, Using = "//tr/td[3]")]
        public IWebElement emailColumn;

        [FindsBy(How = How.XPath, Using = "//div[@class='delete-btn']")]
        public IList<IWebElement> deleteUserBtn;

        #endregion

        #region Add Program page

        [FindsBy(How = How.XPath, Using = "//span[@class='ant-select-selection-item']")]
        public IWebElement cbbxMembershipName;

        [FindsBy(How = How.XPath, Using = "//div[@class='add-programs-btn']")]
        public IWebElement btnAddProgram;

        [FindsBy(How = How.XPath, Using = "//input[@name='programName']")]
        public IWebElement inputProgramName;

        [FindsBy(How = How.XPath, Using = "//input[@name='numberOfWeeks']")]
        public IWebElement inputProgramNumOfWeeks;

        [FindsBy(How = How.XPath, Using = "//input[@name='steps']")]
        public IWebElement inputProgramSteps;

        [FindsBy(How = How.XPath, Using = "//input[@placeholder]")]
        public IList<IWebElement> inputProgramAvailableDate;

        [FindsBy(How = How.XPath, Using = "//input[@type='file']")]
        public IWebElement inputProgramUploadFile;

        [FindsBy(How = How.XPath, Using = "//div[@class='table-item-name']")]
        public IList<IWebElement> nameProgramTitle;

        [FindsBy(How = How.XPath, Using = "//div[@class='table-item-name']")]
        public IWebElement nameProgramTitleElem;

        [FindsBy(How = How.XPath, Using = "//h3[text()='Next Phase']/parent::div//input")]
        public IWebElement inputNextPhase;

        [FindsBy(How = How.XPath, Using = "//div[@class='table-item_controls']//div[text()='Add Workouts']")]
        public IList<IWebElement> btnProgramAddWorkouts;
        [FindsBy(How = How.XPath, Using = "//div[@class='table-item_controls']//div[text()='Add Workouts']")]
        public IWebElement btnProgramAddWorkoutsElem;

        [FindsBy(How = How.XPath, Using = "//div[@class='delete']")]
        public IList<IWebElement> btnProgramDelete;



        #endregion

        #region Add Workout page

        [FindsBy(How = How.XPath, Using = "//h3[text()='Program']/following::input[@type='search']")]
        public IWebElement cbbxPhaseName;

        [FindsBy(How = How.XPath, Using = "//div[@class='add-workouts-btn']")]
        public IWebElement btnAddWorkout;

        [FindsBy(How = How.XPath, Using = "//input[@name='name']")]
        public IWebElement inputWorkoutName;

        [FindsBy(How = How.XPath, Using = "//div[@class='add-workouts-form_wrapper']//input[@role='combobox']")]
        public IWebElement btnWeekDay;

        public IWebElement btnWeekDayElem => Browser._Driver.FindElement(By.XPath("//div[@class='add-workouts-form_wrapper']//input[@role='combobox']"));


        [FindsBy(How = How.XPath, Using = "//div[@class='table-item-name']")]
        public IList<IWebElement> nameWorkoutTitle;

        [FindsBy(How = How.XPath, Using = "//div[@class='table-item-name']")]
        public IWebElement nameWorkoutTitleElem;

        [FindsBy(How = How.XPath, Using = "//div[@class='delete']")]
        public IList<IWebElement> btnExerciseDelete;
        
        [FindsBy(How=How.XPath,Using = "//div[@class='membership-item_add add-workout'][text()='Add Exercises']")]
        public IList<IWebElement> btnAddExercises;

        [FindsBy(How = How.XPath, Using = "//div[@class='membership-item_add add-workout'][text()='Add Exercises']")]
        public IWebElement btnAddExercisesElement;





        #endregion

        #region Add Exercise page

        [FindsBy(How = How.XPath, Using = "//h3[text()='Workout']/following::input[@type='search']")]
        public IWebElement cbbxWorkoutsTitle;

        [FindsBy(How = How.XPath, Using = "//div[@class='copy-form_btn']")]
        public IWebElement btnCopy;

        [FindsBy(How = How.XPath, Using = "//div[@class='exercises-btn']")]
        public IWebElement addExerciseBtn;

        [FindsBy(How = How.XPath, Using = "//input[@name='series']")]
        public IWebElement seriesExerciseInput;

        [FindsBy(How = How.XPath, Using = "//input[@id='exercises-form-items_exerciseId']")]
        public IWebElement exercisesCbbx;

        [FindsBy(How = How.XPath, Using = "//h3[text()='Membership']/parent::div//input")]
        public IWebElement cbbxMembership;

        [FindsBy(How = How.XPath, Using = "//h3[text()='Program']/parent::div//input")]
        public IWebElement cbbxProgram;

        [FindsBy(How = How.XPath, Using = "//h3[text()='Workout']/parent::div//input")]
        public IWebElement cbbxWorkout;

        public IWebElement exercisesCbbxElem => Browser._Driver.FindElement(By.XPath("//input[@id='exercises-form-items_exerciseId']"));

        [FindsBy(How = How.XPath, Using = "//div[@class='top-inputs']//span[@class='ant-select-selection-item']")]
        public IWebElement exercisesTitle;

        [FindsBy(How = How.XPath, Using = "//input[contains(@id,'sets')]")]
        public IList<IWebElement> setsExerciseInput;

        [FindsBy(How = How.XPath, Using = "//input[contains(@id,'reps')]")]
        public IList<IWebElement> repsExerciseInput;

        [FindsBy(How = How.XPath, Using = "//input[contains(@id,'rest')]")]
        public IList<IWebElement> restExerciseInput;

        [FindsBy(How = How.XPath, Using = "//input[contains(@id,'tempo')]")]
        public IList<IWebElement> tempoExerciseInput;

        [FindsBy(How = How.XPath, Using = "//span[contains(text(), 'Add row')]/parent::button[contains(@class, 'ex-add-btn')]")]
        public IWebElement addExerciseRowBtn;

        [FindsBy(How = How.XPath, Using = "//button[contains(@class, 'ex-add-btn')]/span[contains(text(), 'Delete row')]")]
        public IWebElement deleteExerciseRowBtn;

        [FindsBy(How = How.XPath, Using = "//input[@id='exercises-form-items_notes']")]
        public IWebElement notesExerciseInput;

        [FindsBy(How = How.XPath, Using = "//p[@class='name']")]
        public IWebElement exerciseTitleRow;



        #endregion


        
    }
}
