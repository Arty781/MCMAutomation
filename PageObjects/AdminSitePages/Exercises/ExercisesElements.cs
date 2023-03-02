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
    public partial class ExercisesAdmin
    {
        [FindsBy(How = How.XPath, Using = "//div[@class='table-item-row']/p")]
        public IList<IWebElement> nameExerciseTitle;

        [FindsBy(How = How.XPath, Using = "//div[@class='table-item-row']/p")]
        public IWebElement nameExerciseTitleElem;

        [FindsBy(How = How.XPath, Using = "//div[@class='exercises-database-btn']")]
        public IWebElement btnAddExercise;

        [FindsBy(How = How.XPath, Using = "//input[@placeholder='Search by name']")]
        public IWebElement fieldSearchExercise;

        [FindsBy(How = How.XPath, Using = "//button[contains(@class,'ant-input-search-button')]")]
        public IWebElement btnSearchExercise;

        [FindsBy(How = How.XPath, Using = "//div[@class='edit']")]
        public IList<IWebElement> btnEditExercise;

        [FindsBy(How = How.XPath, Using = "//div[@class='delete']")]
        public IList<IWebElement> btnDdeleteExercise;

        [FindsBy(How = How.XPath, Using = "//button[text()='Add Related Gym Exercise']")]
        public IWebElement btnAddRelatedGymExercise;

        [FindsBy(How = How.XPath, Using = "//button[text()='Add Related Home Exercise']")]
        public IWebElement btnAddRelatedHomeExercise;

        [FindsBy(How = How.Id, Using = "name")]
        public IWebElement fieldExerciseName;

        [FindsBy(How = How.Id, Using = "videoURL")]
        public IWebElement fieldExerciseUrl;

        [FindsBy(How = How.XPath, Using = "//span[text()='Select Related Exercise']/parent::div//input")]
        public IList<IWebElement> fieldRelatedGymExercise;

        [FindsBy(How = How.XPath, Using = "//span[text()='Select Home Exercise']/parent::div//input")]
        public IList<IWebElement> fieldRelatedHomeExercise;

        [FindsBy(How = How.XPath, Using = "//button[@title='Remove Relataed Exercise']")]
        public IList<IWebElement> btnRemoveRelatedExecise;

        [FindsBy(How = How.XPath, Using = "//button[@title='Remove Home Exercise']")]
        public IList<IWebElement> btnRemoveHomeExecise;

        [FindsBy(How = How.XPath, Using = "//button[@title='Remove Relataed Exercise']")]
        public IWebElement btnRemoveRelatedExeciseElem;

        [FindsBy(How = How.XPath, Using = "//input[@type='radio']")]
        public IList<IWebElement> btnTempoStart;

        [FindsBy(How = How.XPath, Using = "//span[text()='Yes']")]
        public IWebElement btnConfirmationYes;

    }
}