using MCMAutomation.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.PageObjects.ClientSitePages
{
    public partial class Progress
    {
        public List<string> GetProgressData()
        {
            WaitUntil.CustomElevemtIsVisible(inputWeight);
            var progressList = new List<string>();
            
            progressList.Add(inputWeight.GetAttribute("value"));
            progressList.Add(inputWaist.GetAttribute("value"));
            progressList.Add(inputChest.GetAttribute("value"));
            progressList.Add(inputArm.GetAttribute("value"));
            progressList.Add(inputHip.GetAttribute("value"));
            progressList.Add(inputThigh.GetAttribute("value"));

            return progressList;
        }

        public Progress VerifyAddedProgress(List<string> progressData)
        {
            WaitUntil.CustomElevemtIsVisible(titleProgressPage, 60);
            WaitUntil.WaitSomeInterval(1500);
            var progressList = new List<string>();
            progressList.Add(TextBox.GetText(valueWeight).Trim(new char[] { 'c', 'm', 'k', 'g', 'i', 'n', 'l', 'b', 's' }));
            progressList.Add(TextBox.GetText(valueWaist).Trim(new char[] { 'c', 'm', 'k', 'g', 'i', 'n', 'l', 'b', 's' }));
            progressList.Add(TextBox.GetText(valueChest).Trim(new char[] { 'c', 'm', 'k', 'g', 'i', 'n', 'l', 'b', 's' }));
            progressList.Add(TextBox.GetText(valueArm).Trim(new char[] { 'c', 'm', 'k', 'g', 'i', 'n', 'l', 'b', 's' }));
            progressList.Add(TextBox.GetText(valueHips).Trim(new char[] { 'c', 'm', 'k', 'g', 'i', 'n', 'l', 'b', 's' }));
            progressList.Add(TextBox.GetText(valueThigh).Trim(new char[] { 'c', 'm', 'k', 'g', 'i', 'n', 'l', 'b', 's' }));
            var verifyList = progressData.Except(progressList);

            Assert.IsTrue(verifyList.Count() == 0);

            return this;
        }
    }
}