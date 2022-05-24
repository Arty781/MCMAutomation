using MCMAutomation.Helpers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.PageObjects
{
    public partial class MembershipUser
    {
        #region Client > Membership page

        #region YourNextProgram

        public IWebElement buyBtn => Browser._Driver.FindElement(_buyBtn);
        public readonly By _buyBtn = By.XPath("//a[@class='program-info_btn']");

        #endregion

        #region MainMembershipPage
        public IWebElement programTitle => Browser._Driver.FindElement(_programTitle);
        public readonly By _programTitle = By.XPath("//a[@class='program-info_title']");

        public IWebElement selectProgramBtn => Browser._Driver.FindElement(_selectProgramBtn);
        public readonly By _selectProgramBtn = By.XPath("//button[@class='program-button']");

        public IWebElement popupYesBtn => Browser._Driver.FindElement(_popupYesBtn);
        public readonly By _popupYesBtn = By.XPath("//button[contains(@class,'modal-controls-btn_yes')]");

        public IWebElement popupNoBtn => Browser._Driver.FindElement(_popupNoBtn);
        public readonly By _popupNoBtn = By.XPath("//button[contains(@class,'modal-controls-btn_no')]");


        #endregion

        #region Phase page

        public IWebElement selectPhaseBtn => Browser._Driver.FindElement(_selectPhaseBtn);
        public readonly By _selectPhaseBtn = By.XPath("//div[@class='phase']");

        public IWebElement weekSelectorInput => Browser._Driver.FindElement(_weekSelectorInput);
        public readonly By _weekSelectorInput = By.XPath("//input[@type='search']");

        public IWebElement viewTrainingProgramBtn => Browser._Driver.FindElement(_viewTrainingProgramBtn);
        public readonly By _viewTrainingProgramBtn = By.XPath("//button[@class='ant-btn modal-controls-btn_view']");



        #endregion

        #region Workouts page

        public IWebElement workoutBtn => Browser._Driver.FindElement(_workoutBtn);
        public readonly By _workoutBtn = By.XPath("//div[@class='program-overview_days ']//div[@class='program-overview_days-btn ']");

        public IWebElement weekSelectorInputEx => Browser._Driver.FindElement(_weekSelectorInputEx);
        public readonly By _weekSelectorInputEx = By.XPath("//input[@type='search']");

        public IWebElement weekSelector => Browser._Driver.FindElement(_weekSelector);
        public readonly By _weekSelector = By.XPath("//span[@class='ant-select-selection-item']");

        public IWebElement weekSelectorCbbx => Browser._Driver.FindElement(_weekSelectorCbbx);
        public readonly By _weekSelectorCbbx = By.XPath("//div[@class='ant-select-selector']");


        #endregion

        #region Exercises page

        public IWebElement weightInput => Browser._Driver.FindElement(_weightInput);
        public readonly By _weightInput = By.XPath("//tr//td[4]//input[@class='view-workout_table_input']");

        public IWebElement repsInput => Browser._Driver.FindElement(_repsInput);
        public readonly By _repsInput = By.XPath("//tr//td[5]//input[@class='view-workout_table_input']");

        public IWebElement checkboxInput => Browser._Driver.FindElement(_checkboxInput);
        public readonly By _checkboxInput = By.XPath("//tr//td[6]//input[@type='checkbox']");

        public IWebElement openNotesBtn => Browser._Driver.FindElement(_openNotesBtn);
        public readonly By _openNotesBtn = By.XPath("//div[@class='open-btn']");

        public IWebElement editNotesBtn => Browser._Driver.FindElement(_editNotesBtn);
        public readonly By _editNotesBtn = By.XPath("//div[@class='open-btn']");

        public IWebElement notesInput => Browser._Driver.FindElement(_notesInput);
        public readonly By _notesInput = By.XPath("//textarea[@class='ant-input']");

        public IWebElement saveNotesBtn => Browser._Driver.FindElement(_saveNotesBtn);
        public readonly By _saveNotesBtn = By.XPath("//div[@class='save-btn']");

        public IWebElement cancelNotesBtn => Browser._Driver.FindElement(_cancelNotesBtn);
        public readonly By _cancelNotesBtn = By.XPath("//div[@class='cancel-btn']");

        public IWebElement completeWorkoutBtn => Browser._Driver.FindElement(_completeWorkoutBtn);
        public readonly By _completeWorkoutBtn = By.XPath("//div[@class='workout-btn']");
        #endregion


        #endregion
    }
}
