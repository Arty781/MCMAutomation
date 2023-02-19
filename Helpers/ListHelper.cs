using MCMAutomation.PageObjects;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.Helpers
{
    //public class ListHelper
    //{
    //    //public static List<string> DefineProgramList(string programsUrl)
    //    //{
    //    //    Browser._Driver.Navigate().GoToUrl(programsUrl);
    //    //    Pages.CommonPages.PopUp.ClosePopUp();

    //    //    WaitUntil.VisibilityOfAllElementsLocatedBy(By.XPath("//div[@class='membership-item_add add-workout']"), 30);

    //    //    IReadOnlyCollection<IWebElement> programList = Browser._Driver.FindElements(By.XPath("//div[@class='membership-item_add add-workout']"));

    //    //    List<string> urlList = new List<string>();
    //    //    for (int items = 0; items < programList.Count;)
    //    //    {
    //    //        ++items;
    //    //        string addBtn = "//div[@class='table-item'][" + items + "]//div[@class='membership-item_add add-workout']";
    //    //        WaitUntil.VisibilityOfAllElementsLocatedBy(By.XPath(addBtn));
    //    //        IWebElement AddWorkoutsBtn = Browser._Driver.FindElement(By.XPath(addBtn));
    //    //        AddWorkoutsBtn.Click();

    //    //        urlList.Add(Browser._Driver.Url);


    //    //        WaitUntil.WaitSomeInterval(3);
    //    //        WaitUntil.VisibilityOfAllElementsLocatedBy(By.XPath("//div[@class='btn_back']"));
    //    //        IWebElement backBtn = Browser._Driver.FindElement(By.XPath("//div[@class='btn_back']"));
    //    //        backBtn.Click();
    //    //    }
    //    //    Console.WriteLine(urlList);
    //    //    return urlList;
    //    //}

    //    //public static List<string> DefineWorkoutList(IList<IWebElement> workoutLinks)
    //    //{
    //    //    List<string> workoutsList = new List<string>();
    //    //    foreach (var link in workoutLinks) 
    //    //    {
    //    //        Browser._Driver.Navigate().GoToUrl(link);
    //    //        Pages.CommonPages.PopUp.ClosePopUp();
    //    //        WaitUntil.VisibilityOfAllElementsLocatedBy(By.XPath("//div[@class='membership-item_add add-workout']"), 30);

    //    //        IReadOnlyCollection<IWebElement> workoutList = Browser._Driver.FindElements(By.XPath("//div[@class='membership-item_add add-workout']"));

    //    //        List<string> urlList = new List<string>();
    //    //        for (int items = 0; items < workoutList.Count;)
    //    //        {
    //    //            ++items;
    //    //            string addBtn = "//div[@class='table-items']/div[" + items + "]//div[@class='membership-item_add add-workout']";
    //    //            WaitUntil.VisibilityOfAllElementsLocatedBy(By.XPath(addBtn));
    //    //            IWebElement AddWorkoutsBtn = Browser._Driver.FindElement(By.XPath(addBtn));
    //    //            AddWorkoutsBtn.Click();

    //    //            urlList.Add(Browser._Driver.Url);


    //    //            WaitUntil.WaitSomeInterval(3);
    //    //            WaitUntil.VisibilityOfAllElementsLocatedBy(By.XPath("//div[@class='btn_back']"));
    //    //            IWebElement backBtn = Browser._Driver.FindElement(By.XPath("//div[@class='btn_back']"));
    //    //            backBtn.Click();
    //    //        }

    //    //        workoutsList.AddRange(urlList);
                
                
    //    //    }
    //    //    Console.WriteLine(workoutsList);
    //    //    return workoutsList;

    //    //}
    //}

    public class JsonUserExercises
    {
        public object? Id { get; set; } // this might be another data type
        public object? SetDescription { get; set; } 
        public object? WorkoutExerciseId { get; set; }
        public object? UserId { get; set; }
        public object? IsDone { get; set; }
        public object? CreationDate { get; set; }
        public object? IsDeleted { get; set; }
        public object? UpdatedDate { get; set; }
    }

    public class User
    {
        public object? Id { get; set; } // this might be another data type
        public object? FirstName { get; set; }
        public object? LastName { get; set; }
        public object? Email { get; set; }
        public object? ConversionSystem { get; set; }
        public object? Gender { get; set; }
        public object? BirthDate { get; set; }
        public object? Weight { get; set; }
        public object? Height { get; set; }
        public object? ActivityLevel { get; set; }
        public object? Bodyfat { get; set; }
        public object? Calories { get; set; }
        public object? Active { get; set; }
        public object? DateTime { get; set; }
        public object? UserName { get; set; }
        public object? NormalizedUserName { get; set; }
        public object? NormalizedEmail { get; set; }
        public object? EmailConfirmed { get; set; }
        public object? PasswordHash { get; set; }
        public object? SecurityStamp { get; set; }
        public object? ConcurrencyStamp { get; set; }
        public object? PhoneNumber { get; set; }
        public object? PhoneNumberConfirmed { get; set; }
        public object? TwoFactorEnabled { get; set; }
        public object? LockoutEnd { get; set; }
        public object? LockoutEnabled { get; set; }
        public object? AccessFailedCount { get; set; }
        public object? IsDeleted { get; set; }
        public object? IsMainAdmin { get; set; }
        public object? LastGeneratedIdentityToken { get; set; }
        public object? Carbs { get; set; }
        public object? Fats { get; set; }
        public object? MaintenanceCalories { get; set; }
        public object? Protein { get; set; }
    }

    public class Exercises
    {
        public object? Id { get; set; }
        public string? Name { get; set; }
        public object? CreationDate { get; set; }
        public object? IsDeleted { get; set; }
        public object? VideoURL { get; set; }
        public object? TempoBold { get; set; }
        public object? Type { get; set; }

    }

    public class RemoveExerciseBtn
    {
        public IList<IWebElement> Title { get; set; }
        public IList<IWebElement> Btn { get; set; }
    }

}
