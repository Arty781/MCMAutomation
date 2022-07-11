using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.PageObjects.ClientSitePages
{
    public partial class Progress
    {
        [FindsBy(How = How.XPath, Using = "//span[text()='All']/parent::div//input")]
        public IWebElement filterTypeOptions;

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Month')]/parent::div//input")]
        public IWebElement filterDateRange;

        [FindsBy(How = How.XPath, Using = "//div/p[text()='Chest']/parent::div/p[@class='block-number']")]
        public IWebElement valueChest;

        [FindsBy(How = How.XPath, Using = "//div/p[text()='Hips']/parent::div/p[@class='block-number']")]
        public IWebElement valueHips;

        [FindsBy(How = How.XPath, Using = "//div/p[text()='Thigh']/parent::div/p[@class='block-number']")]
        public IWebElement valueThigh;

        [FindsBy(How = How.XPath, Using = "//div/p[text()='Waist']/parent::div/p[@class='block-number']")]
        public IWebElement valueWaist;

        [FindsBy(How = How.XPath, Using = "//div/p[text()='Arm']/parent::div/p[@class='block-number']")]
        public IWebElement valueArm;

        [FindsBy(How = How.XPath, Using = "//div/p[text()='Weight']/parent::div/p[@class='block-number']")]
        public IWebElement valueWeight;

        [FindsBy(How = How.XPath, Using = "//div[@class='your-progress-block ']//div[contains(@class,'edit')]")]
        public IList<IWebElement> btnEditProgress;

        [FindsBy(How = How.XPath, Using = "//div[@class='your-progress-block ']//div[contains(@class,'delete')]")]
        public IList<IWebElement> btnDeleteProgress;

        [FindsBy(How = How.XPath, Using = "//a[@class='your-progress-btn']")]
        public IWebElement btnAddProgressA;

        [FindsBy(How = How.XPath, Using = "//div[@class='your-progress-btn']")]
        public IWebElement btnAddProgressDiv;

        [FindsBy(How = How.XPath, Using = "//h2[text()='Your Progress']")]
        public IWebElement titleProgressPage;

        #region FINDING YOUR ESTIMATED TDEE

        [FindsBy(How = How.Name, Using = "weight")]
        public IWebElement inputWeight;

        [FindsBy(How = How.Name, Using = "waist")]
        public IWebElement inputWaist;

        [FindsBy(How = How.Name, Using = "chest")]
        public IWebElement inputChest;

        [FindsBy(How = How.Name, Using = "arm")]
        public IWebElement inputArm;

        [FindsBy(How = How.Name, Using = "hip")]
        public IWebElement inputHip;

        [FindsBy(How = How.Name, Using = "thigh")]
        public IWebElement inputThigh;

        [FindsBy(How = How.XPath, Using = "//p[text()='Front']/parent::div//input")]
        public IWebElement inputUploadPhotoFront;

        [FindsBy(How = How.XPath, Using = "//p[text()='Back']/parent::div//input")]
        public IWebElement inputUploadPhotoBack;

        [FindsBy(How = How.XPath, Using = "//p[text()='Side']/parent::div//input")]
        public IWebElement inputUploadPhotoSide;

        [FindsBy(How = How.XPath, Using = "//span[text()='Submit']/parent::button")]
        public IWebElement btnSubmit;

        [FindsBy(How = How.XPath, Using = "//div[text()='Cancel']/parent::button")]
        public IWebElement btnCancel;

        #endregion
    }
}