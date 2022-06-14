﻿using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.PageObjects.ClientSitePages
{
    public partial class Nutrition
    {
        #region Nutrition main page
        [FindsBy(How = How.XPath, Using = "//button[@type='submit']")]
        public IWebElement btnCalculate;

        [FindsBy(How = How.XPath, Using = "//label/span[text()='Male']")]
        public IWebElement btnMale;

        [FindsBy(How = How.XPath, Using = "//label/span[text()='Female']")]
        public IWebElement btnFemale;

        [FindsBy(How = How.XPath, Using = "//label/span[text()='Imperial']")]
        public IWebElement btnImperial;

        [FindsBy(How = How.XPath, Using = "//label/span[text()='Metric']")]
        public IWebElement btnMetric;

        [FindsBy(How = How.XPath, Using = "//label[@title='Have you been dieting long term?']/ancestor::div[@class='ant-row ant-form-item radio']//span[text()='Yes']")]
        public IWebElement btnYesDietingLongTerm;

        [FindsBy(How = How.XPath, Using = "//label[@title='Have you been dieting long term?']/ancestor::div[@class='ant-row ant-form-item radio']//span[text()='No']")]
        public IWebElement btnNoDietingLongTerm;

        [FindsBy(How = How.XPath, Using = "//label[@title='Are you breastfeeding (less than 5 months postpartum)?']/ancestor::div[@class='ant-row ant-form-item radio']//span[text()='Yes']")]
        public IWebElement btnYesBrstFeedLess5Month;

        [FindsBy(How = How.XPath, Using = "//label[@title='Are you breastfeeding (less than 5 months postpartum)?']/ancestor::div[@class='ant-row ant-form-item radio']//span[text()='No']")]
        public IWebElement btnNoBrstFeedLess5Month;

        [FindsBy(How = How.XPath, Using = "//label[@title='Are you breastfeeding (less than 5 months postpartum)?']/ancestor::div[@class='ant-row ant-form-item radio']//span[text()='Yes']")]
        public IWebElement btnYesBrstFeedMore5Month;

        [FindsBy(How = How.XPath, Using = "//label[@title='Are you breastfeeding (5-12 months postpartum)?']/ancestor::div[@class='ant-row ant-form-item radio']//span[text()='No']")]
        public IWebElement btnNoBrstFeedMore5Month;

        [FindsBy(How = How.XPath, Using = "//input[@name='age']")]
        public IWebElement inputAge;

        [FindsBy(How = How.XPath, Using = "//input[@name='height']")]
        public IWebElement inputHeight;

        [FindsBy(How = How.XPath, Using = "//input[@name='weight']")]
        public IWebElement inputWeight;

        [FindsBy(How = How.XPath, Using = "//input[@name='bodyfat']")]
        public IWebElement inputBodyFat;

        [FindsBy(How = How.XPath, Using = "//span[@class='ant-select-selection-item']/parent::div")]
        public IWebElement cbbxActivitylevel;
        #endregion

        #region Step01

        [FindsBy(How = How.XPath, Using = "//input[@type='number']")]
        public IWebElement inputCalories;

        [FindsBy(How = How.XPath, Using = "//div[@class='next']")]
        public IWebElement btnNext;

        [FindsBy(How = How.XPath, Using = "//a[@class='previous']")]
        public IWebElement btnPreviousStep1;

        #endregion

        #region Step02

        [FindsBy(How = How.XPath, Using = "//div[contains(@class,'cut')]")]
        public IWebElement btnCut;

        [FindsBy(How = How.XPath, Using = "//div[contains(@class,'maintain')]")]
        public IWebElement btnMaintain;

        [FindsBy(How = How.XPath, Using = "//div[contains(@class,'build')]")]
        public IWebElement btnBuild;

        [FindsBy(How = How.XPath, Using = "//div[contains(@class,'reverse')]")]
        public IWebElement btnReverse;
        
        [FindsBy(How = How.XPath, Using = "//div[@class='previous']")]
        public IWebElement btnPreviousStep2AndMore;

        #endregion

        #region Step03

        [FindsBy(How = How.XPath, Using = "//div[contains(@class,'tier-one')]")]
        public IWebElement btnTier1;

        [FindsBy(How = How.XPath, Using = "//div[contains(@class,'tier-two')]")]
        public IWebElement btnTier2;

        [FindsBy(How = How.XPath, Using = "//div[contains(@class,'tier-three')]")]
        public IWebElement btnTier3;


        #endregion

        #region Step04

        [FindsBy(How = How.XPath, Using = "//div[contains(@class,'phase-one')]")]
        public IWebElement btnPhase1;

        [FindsBy(How = How.XPath, Using = "//div[contains(@class,'phase-two')]")]
        public IWebElement btnPhase2;

        [FindsBy(How = How.XPath, Using = "//div[contains(@class,'phase-three')]")]
        public IWebElement btnPhase3;


        #endregion

        #region Step05

        [FindsBy(How = How.XPath, Using = "//div[contains(@class,'diet-one')]")]
        public IWebElement btnDiet1;

        [FindsBy(How = How.XPath, Using = "//div[contains(@class,'diet-two')]")]
        public IWebElement btnDiet2;

        [FindsBy(How = How.XPath, Using = "//div[contains(@class,'diet-three')]")]
        public IWebElement btnDiet3;



        #endregion

        #region Step06
        [FindsBy(How = How.XPath, Using = "//div[@class='last-step-content_top_block']/p[2]")]
        public IWebElement valueCalories;

        [FindsBy(How = How.XPath, Using = "last-step-content_bottom_block-weight")]
        public IList<IWebElement> valueOfProteinCarbsFat;

        #endregion

        


    }
}
