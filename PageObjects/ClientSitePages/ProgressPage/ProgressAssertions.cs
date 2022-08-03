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
        public string[] GetProgressData()
        {
            WaitUntil.WaitSomeInterval(5000);
            var progressList = new List<string>();
            
            progressList.Add(TextBox.GetAttribute(inputWeight, "value"));
            progressList.Add(TextBox.GetAttribute(inputWaist, "value"));
            progressList.Add(TextBox.GetAttribute(inputChest, "value"));
            progressList.Add(TextBox.GetAttribute(inputArm, "value"));
            progressList.Add(TextBox.GetAttribute(inputHip, "value"));
            progressList.Add(TextBox.GetAttribute(inputThigh, "value"));

            string[] progressData = progressList.ToArray();

            return progressData;
        }

        public Progress VerifyAddedProgress(string[] progressData)
        {
            WaitUntil.CustomElevemtIsVisible(titleProgressPage, 60);
            WaitUntil.WaitSomeInterval(1500);
            var progressList = new List<string>();

            progressList.Add(TextBox.GetText(valueWeight).Trim(new char[] { 'c', 'm', 'k', 'g', 'i', 'n', 'l', 'b', 's'}));
            progressList.Add(TextBox.GetText(valueWaist).Trim(new char[] { 'c', 'm', 'k', 'g', 'i', 'n', 'l', 'b', 's' }));
            progressList.Add(TextBox.GetText(valueChest).Trim(new char[] { 'c', 'm', 'k', 'g', 'i', 'n', 'l', 'b', 's' }));
            progressList.Add(TextBox.GetText(valueArm).Trim(new char[] { 'c', 'm', 'k', 'g', 'i', 'n', 'l', 'b', 's' }));
            progressList.Add(TextBox.GetText(valueHips).Trim(new char[] { 'c', 'm', 'k', 'g', 'i', 'n', 'l', 'b', 's' }));
            progressList.Add(TextBox.GetText(valueThigh).Trim(new char[] { 'c', 'm', 'k', 'g', 'i', 'n', 'l', 'b', 's' }));

            string[] progressListOne = progressList.ToArray();
            
            var list1 = progressData.Except(progressListOne);
            var list2 = progressListOne.Except(progressData);

            Assert.IsTrue(!list1.Any() && !list2.Any());

            return this;
        }
    }
}