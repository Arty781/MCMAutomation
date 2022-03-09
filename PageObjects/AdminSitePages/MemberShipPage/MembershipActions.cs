using NUnit.Allure.Steps;
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
       
        [AllureStep("Create new membership")]
        public Membership CreateMembership()
        {
            
            WaitUntil.ElementIsVisible(_membershipCreateBtn);
            membershipCreateBtn.Click();
            WaitUntil.ElementIsVisible(_skuInput);
            skuInput.Clear();
            skuInput.SendKeys("PP-1");
            nameInput.Clear();
            nameInput.SendKeys("Created New Membership " + DateTime.Now.ToString("yyyy-mm-dd hh:mm:ss"));
            descriptionInput.Clear();
            descriptionInput.SendKeys("Lorem ipsum dolor");
            startDateInput.Clear();
            startDateInput.SendKeys(DateTime.Now.ToString("yyyy-mm-dd"));
            endDateInput.Clear();
            endDateInput.SendKeys(DateTime.Now.AddMonths(2).ToString("yyyy-mm-dd"));
            genderMaleToggle.Click();
            priceInput.Clear();
            priceInput.SendKeys("2");
            urlInput.Clear();
            urlInput.SendKeys(Endpoints.websiteHost);
            addPhotoInput.SendKeys(Browser.RootPath()+UploadedImages.CreateMemberImg);
            availableForPurchaseCheckbox.Click();
            saveMembershipBtn.Click();
            WaitUntil.ElementIsVisible(_membershipCreateBtn);
            return this;
        }

       


    }
}
