using OpenQA.Selenium;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Drawing;

namespace MCMAutomation.Helpers
{
    public class ScreenShotHelper
    {
        public static string TakeScreenshot()
        {
            ITakesScreenshot ssdriver = Browser._Driver as ITakesScreenshot;
            Screenshot screenshot = ssdriver.GetScreenshot();
            string timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd_HH-mm-ss");
            string path = Path.Combine(Browser.RootPath(), "ErrorImages", timestamp);
            string name = $"Exception-{timestamp}.jpeg";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string fullPath = Path.Combine(path, name);
            screenshot.SaveAsFile(fullPath, ScreenshotImageFormat.Jpeg);
            WaitUntil.WaitSomeInterval(2000);

            return fullPath;
        }

    }
}
