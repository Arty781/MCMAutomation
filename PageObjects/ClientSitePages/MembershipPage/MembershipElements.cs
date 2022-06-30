using MCMAutomation.Helpers;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.PageObjects.ClientSitePages
{
    public partial class MembershipUser
    {
        #region Client > Membership page

        #region YourNextProgram

        [FindsBy(How = How.XPath, Using = "//a[@class='program-info_btn']")]
        public IWebElement buyBtn;

        #endregion

        #region MainMembershipPage

        [FindsBy(How = How.XPath, Using = "//a[@class='program-info_title']")]
        public IWebElement programTitle;

        [FindsBy(How = How.XPath, Using = "//button[@class='program-button']")]
        public IWebElement selectProgramBtn;

        [FindsBy(How = How.XPath, Using = "//button[contains(@class,'modal-controls-btn_yes')]")]
        public IWebElement popupYesBtn;

        [FindsBy(How = How.XPath, Using = "//button[contains(@class,'modal-controls-btn_no')]")]
        public IWebElement popupNoBtn;



        #endregion

        #region Phase page

        [FindsBy(How = How.XPath, Using = "//div[@class='phase']")]
        public IList<IWebElement> selectPhaseBtn;
        [FindsBy(How = How.XPath, Using = "//div[@class='phase']")]
        public IWebElement selectPhaseBtnElem;

        [FindsBy(How = How.XPath, Using = "//input[@type='search']")]
        public IWebElement weekSelectorInput;

        [FindsBy(How = How.XPath, Using = "//button[@class='ant-btn modal-controls-btn_view']")]
        public IWebElement viewTrainingProgramBtn;



        #endregion

        #region Workouts page

        [FindsBy(How = How.XPath, Using = "//div[@class='program-overview_days ']//div[@class='program-overview_days-btn ']")]
        public IList<IWebElement> workoutBtn;

        [FindsBy(How = How.XPath, Using = "//div[contains(@class,'completed')]")]
        public IList<IWebElement> btnCompletedWorkouts;

        [FindsBy(How = How.XPath, Using = "//div[@class='program-overview_days ']//div[@class='program-overview_days-btn ']")]
        public IWebElement workoutBtnelem;

        [FindsBy(How = How.XPath, Using = "//input[@type='search']")]
        public IWebElement weekSelectorInputEx;

        [FindsBy(How = How.XPath, Using = "//span[@class='ant-select-selection-item']")]
        public IWebElement weekSelector;

        [FindsBy(How = How.XPath, Using = "//div[@class='ant-select-selector']")]
        public IWebElement weekSelectorCbbx;


        #endregion

        #region Exercises page

        [FindsBy(How = How.XPath, Using = "//tr//td[4]//input[@class='view-workout_table_input']")]
        public IList<IWebElement> weightInput;

        [FindsBy(How = How.XPath, Using = "//tr//td[4]//input[@class='view-workout_table_input']")]
        public IWebElement weightInputElem;

        [FindsBy(How=How.XPath,Using = "//tbody//td//span[@class='view-workout_table_readonly']")]
        public IList<IWebElement> outputWeight;

        [FindsBy(How = How.XPath, Using = "//tr//td[5]//input[@class='view-workout_table_input']")]
        public IList<IWebElement> repsInput;
        [FindsBy(How = How.XPath, Using = "//tr//td[5]//input[@class='view-workout_table_input']")]
        public IWebElement repsInputElem;

        [FindsBy(How = How.XPath, Using = "//tr//td[6]//input[@type='checkbox']")]
        public IList<IWebElement> checkboxInput;

        [FindsBy(How=How.XPath,Using = "//span[@class='view-workout_table_readonly']")]
        public IList<IWebElement> inputWeightReadonly;

        [FindsBy(How = How.XPath, Using = "//div[@class='open-btn']")]
        public IList<IWebElement> openNotesBtn;
        public IWebElement openNotesBtnelem => Browser._Driver.FindElement(By.XPath("//div[@class='open-btn']"));

        [FindsBy(How = How.XPath, Using = "//div[@class='open-btn']")]
        public IList<IWebElement> editNotesBtn;
        [FindsBy(How = How.XPath, Using = "//div[@class='open-btn']")]
        public IWebElement editNotesBtnelem;

        [FindsBy(How = How.XPath, Using = "//textarea[@class='ant-input']")]
        public IList <IWebElement> notesInput;
        [FindsBy(How = How.XPath, Using = "//textarea[@class='ant-input']")]
        public IWebElement notesInputelem;

        [FindsBy(How = How.XPath, Using = "//div[@class='save-btn']")]
        public IList<IWebElement> saveNotesBtn;
        [FindsBy(How = How.XPath, Using = "//div[@class='save-btn']")]
        public IWebElement saveNotesBtnElem;

        [FindsBy(How = How.XPath, Using = "//div[@class='cancel-btn']")]
        public IList<IWebElement> cancelNotesBtn;
        [FindsBy(How = How.XPath, Using = "//div[@class='cancel-btn']")]
        public IWebElement cancelNotesBtnElem;

        [FindsBy(How = How.XPath, Using = "//div[@class='workout-btn']")]        
        public IWebElement completeWorkoutBtn;

        #endregion


        #endregion
    }
}
