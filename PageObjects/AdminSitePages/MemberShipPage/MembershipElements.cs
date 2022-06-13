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
    public partial class MembershipAdmin
    {
        [FindsBy(How = How.XPath, Using = "//div[@class='btn_back']")]
        public IWebElement backBtn;

        #region Membership Page

        public IWebElement btnCreate => Browser._Driver.FindElement(_CreateBtn);
        public readonly By _CreateBtn = By.XPath("//a[text()='Create Membership']");

        [FindsBy(How = How.XPath, Using = "//a[text()='Create Membership']")]
        public IWebElement membershipCreateBtn;

        [FindsBy(How = How.XPath, Using = "//input[@placeholder='Search']")]
        public IWebElement membershipSearchInput;

        [FindsBy(How = How.XPath, Using = "//button[contains(@class, 'ant-input-search-button')]")]
        public IWebElement membershipSearchBtn;

        [FindsBy(How = How.XPath, Using = "//h2[@class='membership-item_title']")]
        public IList<IWebElement> membershipTitle;

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

        [FindsBy(How = How.XPath, Using = "//textarea[@name='description']")]
        public IWebElement membershipDescriptionInput;

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
        public IWebElement subscriptionToggleType;

        [FindsBy(How = How.XPath, Using = "//label/span[text()='Product']")]
        public IWebElement productToggleType;

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

        [FindsBy(How = How.XPath, Using = "//div[@class='delete']")]
        public IList<IWebElement> btnExerciseDelete;
        
        [FindsBy(How=How.XPath,Using = "//div[text()='Add Exercises']")]
        public IList<IWebElement> btnAddExercises;

        [FindsBy(How = How.XPath, Using = "//div[text()='Add Exercises']")]
        public IWebElement btnAddExercisesElement;





        #endregion

        #region Add Exercise page

        [FindsBy(How = How.XPath, Using = "//h3[text()='Workout']/following::input[@type='search']")]
        public IWebElement cbbxWorkoutsTitle;

        [FindsBy(How = How.XPath, Using = "//div[@class='exercises-btn']")]
        public IWebElement addExerciseBtn;

        [FindsBy(How = How.XPath, Using = "//input[@name='series']")]
        public IWebElement seriesExerciseInput;

        [FindsBy(How = How.XPath, Using = "//input[@id='exercises-form-items_exerciseId']")]
        public IWebElement exercisesCbbx;

        public IWebElement exercisesCbbxElem => Browser._Driver.FindElement(By.XPath("//input[@id='exercises-form-items_exerciseId']"));

        [FindsBy(How = How.XPath, Using = "//div[@class='top-inputs']//span[@class='ant-select-selection-item']")]
        public IWebElement exercisesTitle;

        [FindsBy(How = How.XPath, Using = "//input[@id='exercises-form-items_weekWorkoutExercises_0_sets']")]
        public IWebElement setsExerciseInput;

        [FindsBy(How = How.XPath, Using = "//input[@id='exercises-form-items_weekWorkoutExercises_0_reps']")]
        public IWebElement repsExerciseInput;

        [FindsBy(How = How.XPath, Using = "//input[@id='exercises-form-items_weekWorkoutExercises_0_rest']")]
        public IWebElement restExerciseInput;

        [FindsBy(How = How.XPath, Using = "//input[@id='exercises-form-items_weekWorkoutExercises_0_tempo']")]
        public IWebElement tempoExerciseInput;

        [FindsBy(How = How.XPath, Using = "//button[contains(@class, 'ex-add-btn')]/span[contains(text(), 'Add row')]")]
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
